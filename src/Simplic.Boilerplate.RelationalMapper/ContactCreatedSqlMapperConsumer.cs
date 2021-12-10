using MassTransit;
using Simplic.Boilerplate.SchemaRegistry;
using System;
using System.Threading.Tasks;
using Simplic.MessageBroker;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using Simplic.Sql;
using Dapper;

namespace Simplic.Boilerplate.RelationalMapper
{
    public interface ITableConfiguration
    {
        Type Owner { get; set; }
        Type Type { get; }
        string Table { get; set; }
        IList<string> PrimaryKeys { get; }
        IList<ForeignKey> ForeignKeys { get; set; }
        bool DeleteIfNotExisting { get; set; }
    }

    public class TableConfiguration<T> : ITableConfiguration
    {
        public Type Owner { get; set; }
        public Type Type => GetType().GenericTypeArguments[0];
        public string Table { get; set; }
        public IList<string> PrimaryKeys { get; } = new List<string>();
        public IList<ForeignKey> ForeignKeys { get; set; } = new List<ForeignKey>();
        public bool DeleteIfNotExisting { get; set; }
    }

    public class ForeignKey
    {
        public Type Source { get; set; }
        public string PrimaryKeyName { get; set; }
        public string ForeignKeyName { get; set; }
    }

    public static class RelationalSqlMapperExtension
    {
        public static TableConfiguration<T> PrimaryKey<T, F>(this TableConfiguration<T> cfg, Expression<Func<T, F>> primaryKeyField)
        {
            if (primaryKeyField == null)
                throw new ArgumentNullException(nameof(primaryKeyField));

            MemberExpression expr = null;

            if (primaryKeyField.Body is MemberExpression)
            {
                expr = (MemberExpression)primaryKeyField.Body;
            }
            else if (primaryKeyField.Body is UnaryExpression)
            {
                expr = (MemberExpression)((UnaryExpression)primaryKeyField.Body).Operand;
            }
            else
            {
                const string Format = "Expression '{0}' not supported.";
                string message = string.Format(Format, primaryKeyField);

                throw new ArgumentException(message, "Field");
            }

            cfg.PrimaryKeys.Add(expr.Member.Name);

            return cfg;
        }

        public static TableConfiguration<T> ForeignKey<T, O, F>(this TableConfiguration<T> cfg, string column, Expression<Func<O, F>> primaryKeyField)
        {
            if (primaryKeyField == null)
                throw new ArgumentNullException(nameof(primaryKeyField));

            MemberExpression expr = null;

            if (primaryKeyField.Body is MemberExpression)
            {
                expr = (MemberExpression)primaryKeyField.Body;
            }
            else if (primaryKeyField.Body is UnaryExpression)
            {
                expr = (MemberExpression)((UnaryExpression)primaryKeyField.Body).Operand;
            }
            else
            {
                const string Format = "Expression '{0}' not supported.";
                string message = string.Format(Format, primaryKeyField);

                throw new ArgumentException(message, "Field");
            }

            cfg.ForeignKeys.Add(new ForeignKey
            {
                Source = typeof(O),
                PrimaryKeyName = expr.Member.Name,
                ForeignKeyName = column
            });

            return cfg;
        }

        public static TableConfiguration<T> DeleteIfNotExisting<T>(this TableConfiguration<T> cfg)
        {
            cfg.DeleteIfNotExisting = true;

            return cfg;
        }
    }

    public abstract class RelationalMapperConsumer<E, T> : IConsumer<E> where E : class where T : class
    {
        private readonly ISqlService sqlService;
        private readonly ISqlColumnService sqlColumnService;
        private readonly IList<ITableConfiguration> configurations = new List<ITableConfiguration>();

        public RelationalMapperConsumer(ISqlService sqlService, ISqlColumnService sqlColumnService)
        {
            this.sqlService = sqlService;
            this.sqlColumnService = sqlColumnService;
        }

        public async Task Consume(ConsumeContext<E> context)
        {
            var obj = GetObject(context.Message);
            var lastObjects = new Dictionary<Type, object>();
            var stack = new Stack<object>();
            var queue = new Queue<object>();

            ResolveObjects(obj, stack, queue);

            await sqlService.OpenConnection(async (c) =>
                {
                    while (queue.Any())
                    {
                        var currentObj = queue.Dequeue();

                        // Cache last object for other foreign-key references
                        lastObjects[currentObj.GetType()] = currentObj;

                        var configuration = configurations.FirstOrDefault(x => x.Type == currentObj.GetType());

                        if (currentObj is IDictionary<string, object> addon)
                        {
                            lastObjects.TryGetValue(configuration.Owner, out object owner);

                            if (owner != null)
                            {
                                // Set owner foreign-key-value

                                foreach (var foreignKey in configuration.ForeignKeys)
                                { 
                                    
                                }
                            }
                        }
                        else
                        {
                            var columns = sqlColumnService.GetModelDBColumnNames(configuration.Table, configuration.Type, null);
                            var statement = $"INSERT INTO {configuration.Table} ({{0}}) ON EXISTING UPDATE VALUES ({{1}})";

                            // Write to database
                            await c.ExecuteAsync(statement, currentObj);
                        }
                    }
                });
        }

        private void ResolveObjects(object obj, Stack<object> stack, Queue<object> queue, IList<object> checkedObjects = null)
        {
            if (obj == null)
                return;

            // Create object cache if not done yet
            checkedObjects = checkedObjects ?? new List<object>();

            if (checkedObjects.Contains(obj))
                return;

            // Add to already checked objects to prevent stackoverflow exception
            checkedObjects.Add(obj);

            var configuration = configurations.FirstOrDefault(x => x.Type == obj.GetType());

            if (configuration == null)
                return;

            if (!stack.Contains(obj))
                stack.Push(obj);

            if (!queue.Contains(obj))
                queue.Enqueue(obj);

            // Check for possible child stacks
            var properties = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                               .Where(x => x.MemberType == System.Reflection.MemberTypes.Property)

                               // Check whether the type is part of the configurations. If an owner is set, the type must match too
                               .Where(x => configurations.Any(y => x.DeclaringType == y.Type && (y.Owner == null || obj.GetType() == y.Owner))); 

            foreach (var property in properties)
            {
                // Build recursive tree.
                ResolveObjects(property.GetValue(obj), stack, queue, checkedObjects);
            }
        }

        protected abstract T GetObject(E @event);

        protected virtual TableConfiguration<TTableObject> MapTable<TTableObject>(string table) where TTableObject : class
        {
            var configuration = new TableConfiguration<TTableObject>
            {
                Table = table
            };

            configurations.Add(configuration);

            return configuration;
        }

        protected virtual TableConfiguration<TTableObject> MapTable<TTableObject, TObjectOwner>(string table) where TTableObject : class
        {
            var configuration = new TableConfiguration<TTableObject>
            {
                Table = table,
                Owner = typeof(TObjectOwner)
            };

            configurations.Add(configuration);

            return configuration;
        }
    }

    [Queue("boilerplate.contact_created.mapper.event", "Boilerplate")]
    public class ContactCreatedSqlMapperConsumer : RelationalMapperConsumer<ContactCreatedEvent, Contact>
    {
        public ContactCreatedSqlMapperConsumer(ISqlService sqlService, ISqlColumnService sqlColumnService) : base(sqlService, sqlColumnService)
        {
            MapTable<Contact>("IT_Contacts")
                .PrimaryKey(x => x.Id);

            MapTable<IDictionary<string, object>, Contact>("IT_Contacts_Addon")
                .PrimaryKey(x => x["Guid"])
                .ForeignKey("Guid", (Contact x) => x.Id);

            MapTable<Address>("IT_Contacts_Address")
                .PrimaryKey(x => x.Id)
                .ForeignKey("ContactId", (Contact x) => x.Id)
                .DeleteIfNotExisting();
        }

        protected override Contact GetObject(ContactCreatedEvent @event) => @event.Contact;
    }
}