using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simplic.PlugIn.Boilerplate.Server;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Simplic.Plugin.Boilerplate.Server.Test
{
    public class CustomJsonConverterTests
    {
        private static readonly string user = "Test";
        private static readonly string baseUser = "SuperUser";
        private CustomJsonConverter jsonConverter;

        public CustomJsonConverterTests()
        {
            jsonConverter = new CustomJsonConverter(baseUser);
        }

        class Person
        {
            public string Name { get; set; }
        }

        private readonly JObject simpleJsonObject =
            new JObject(
                new JProperty("Name",
                    new JArray(
                        new JObject(
                            new JProperty("id", "0"),
                            new JProperty("user", baseUser),
                            new JProperty("state", "0"),
                            new JProperty("value", "Peter")
                            )
                        )
                    )
                );

        [Fact]
        public void Serialize_SimpleObject()
        {
            var contact = new Person
            {
                Name = "Peter",
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be(simpleJsonObject.ToString());
        }

        [Fact]
        public void Deserialize_SimpleObject_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Person>(simpleJsonObject.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.Name.Should().Be("Peter");
        }

        [Fact]
        public void Deserialize_SimpleObject_WithChanges()
        {
            var jsonObject = new JObject(
                new JProperty("Name",
                    new JArray(
                        new JObject(
                            new JProperty("id", "0"),
                            new JProperty("user", baseUser),
                            new JProperty("state", "0"),
                            new JProperty("value", "Peter")
                            ),
                        new JObject(
                            new JProperty("id", "1"),
                            new JProperty("user", user),
                            new JProperty("state", "0"),
                            new JProperty("value", "Gunther")
                            )
                        )
                    )
                );
            var result = JsonConvertWrapper.DeserializeObject<Person>(jsonObject.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.Name.Should().Be("Gunther");
        }

        class Contact
        {
            public Phone Phone { get; set; }
        }

        class Phone
        {
            public string Type { get; set; }
            public string Number { get; set; }
        }

        private readonly JObject nestedJsonObject =
            new JObject(
                new JProperty("Phone",
                    new JObject(
                        new JProperty("Type",
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "Home")
                                    )
                                )
                            ),
                        new JProperty("Number",
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "123456789")
                                    )
                                )
                            )
                        )
                    )
                );

        [Fact]
        public void Serialize_NestedObject()
        {
            var contact = new Contact
            {
                Phone = new Phone
                {
                    Type = "Home",
                    Number = "123456789"
                },
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be(nestedJsonObject.ToString());
        }

        [Fact]
        public void Deserialize_NestedObject_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Contact>(nestedJsonObject.ToString(), user, baseUser);
            result.Should().NotBeNull();
            result.Phone.Should().NotBeNull();
            result.Phone.Number.Should().Be("123456789");
            result.Phone.Type.Should().Be("Home");
        }

        [Fact]
        public void Deserialize_NestedObject_WithChanges()
        {
            var json = new JObject(
                new JProperty("Phone",
                    new JObject(
                        new JProperty("Type",
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "Home")
                                    ),
                                new JObject(
                                    new JProperty("id", "1"),
                                    new JProperty("user", user),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "Zuhause")
                                    )
                                )
                            ),
                        new JProperty("Number",
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "123456789")
                                    ),
                                new JObject(
                                    new JProperty("id", "1"),
                                    new JProperty("user", user),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "9999")
                                    )
                                )
                            )
                        )
                    )
                );

            var result = JsonConvertWrapper.DeserializeObject<Contact>(json.ToString(), user, baseUser);
            result.Should().NotBeNull();
            result.Phone.Should().NotBeNull();
            result.Phone.Number.Should().Be("9999");
            result.Phone.Type.Should().Be("Zuhause");
        }

        class Adress
        {
            public State State { get; set; }
        }

        class State
        {
            public Town Town { get; set; }
            public string Name { get; set; }
        }

        class Town
        {
            public string Name { get; set; }
            public string Zip { get; set; }
        }

        private readonly JObject doubleNestedJsonObject = new JObject(
                new JProperty("State",
                    new JObject(
                        new JProperty("Town",
                            new JObject(
                                new JProperty("Name",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Hildesheim")
                                            )
                                        )
                                    ),
                                new JProperty("Zip",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "31134")
                                            )
                                        )
                                    )
                                )
                            ),
                        new JProperty("Name",
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "Niedersachsen")
                                    )
                                )
                            )
                        )
                    )
                );

        [Fact]
        public void Serialize_DoubleNestedObject()
        {
            var adress = new Adress
            {
                State = new State
                {
                    Town = new Town
                    {
                        Name = "Hildesheim",
                        Zip = "31134"
                    },
                    Name = "Niedersachsen",
                },
            };

            var result = JsonConvert.SerializeObject(adress, Formatting.Indented, jsonConverter);

            result.Should().Be(doubleNestedJsonObject.ToString());
        }

        [Fact]
        public void Deserialize_DoubleNestedObject_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Adress>(doubleNestedJsonObject.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.State.Should().NotBeNull();
            result.State.Name.Should().Be("Niedersachsen");
            result.State.Town.Should().NotBeNull();
            result.State.Town.Name.Should().Be("Hildesheim");
            result.State.Town.Zip.Should().Be("31134");
        }

        [Fact]
        public void Deserialize_DoubleNestedObject_WithChanges()
        {
            var json = new JObject(
                new JProperty("State",
                    new JObject(
                        new JProperty("Town",
                            new JObject(
                                new JProperty("Name",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Hildesheim")
                                            ),
                                        new JObject(
                                            new JProperty("id", "1"),
                                            new JProperty("user", user),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Hannover")
                                            )
                                        )
                                    ),
                                new JProperty("Zip",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "31134")
                                            ),
                                        new JObject(
                                            new JProperty("id", "1"),
                                            new JProperty("user", user),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "30159")
                                            )
                                        )
                                    )
                                )
                            ),
                        new JProperty("Name",
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "Niedersachsen")
                                    ),
                                new JObject(
                                    new JProperty("id", "1"),
                                    new JProperty("user", user),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "Sachsen")
                                    )
                                )
                            )
                        )
                    )
                );

            var result = JsonConvertWrapper.DeserializeObject<Adress>(json.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.State.Should().NotBeNull();
            result.State.Name.Should().Be("Sachsen");
            result.State.Town.Should().NotBeNull();
            result.State.Town.Name.Should().Be("Hannover");
            result.State.Town.Zip.Should().Be("30159");
        }

        class Folder
        {
            public IEnumerable<string> Tags { get; set; }
        }

        private readonly JObject simpleListJsonObject =
            new JObject(
                new JProperty("Tags",
                    new JArray(
                        new JArray(
                            new JObject(
                                new JProperty("id", "0"),
                                new JProperty("user", baseUser),
                                new JProperty("state", "0"),
                                new JProperty("value", "TestTag")
                                )
                            ),
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "DemoTag")
                                    )
                                ),
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "AnotherTag")
                                    )
                                )
                        )
                    )
                );

        [Fact]
        public void Serialize_SimpleList()
        {
            var contact = new Folder
            {
                Tags = new List<string>
                {
                    "TestTag",
                    "DemoTag",
                    "AnotherTag"
                },
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be(simpleListJsonObject.ToString());
        }

        [Fact]
        public void Deserialize_SimpleList_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Folder>(simpleListJsonObject.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.Tags.Should().NotBeNullOrEmpty();
            result.Tags.Should().HaveCount(3);
            result.Tags.Should().Contain("TestTag");
            result.Tags.Should().Contain("DemoTag");
            result.Tags.Should().Contain("AnotherTag");
        }

        [Fact]
        public void Deserialize_SimpleList_WithChanges()
        {
            var simpleListJsonObject = new JObject(
                new JProperty("Tags",
                    new JArray(
                        new JArray(
                            new JObject(
                                new JProperty("id", "0"),
                                new JProperty("user", baseUser),
                                new JProperty("state", "0"),
                                new JProperty("value", "TestTag")
                                ),
                            new JObject(
                                new JProperty("id", "1"),
                                new JProperty("user", user),
                                new JProperty("state", "-1"),
                                new JProperty("value", "TestTag")
                                )
                            ),
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "DemoTag")
                                    )
                                ),
                            new JArray(
                                new JObject(
                                    new JProperty("id", "0"),
                                    new JProperty("user", baseUser),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "AnotherTag")
                                    ),
                                new JObject(
                                    new JProperty("id", "1"),
                                    new JProperty("user", user),
                                    new JProperty("state", "0"),
                                    new JProperty("value", "JustAnotherTag")
                                    )
                                )
                        )
                    )
                );

            var result = JsonConvertWrapper.DeserializeObject<Folder>(simpleListJsonObject.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.Tags.Should().NotBeNullOrEmpty();
            result.Tags.Should().HaveCount(2);
            result.Tags.Should().Contain("DemoTag");
            result.Tags.Should().Contain("JustAnotherTag");
        }

        private readonly JObject nestedObjectListJsonObject = new JObject(
                new JProperty("Name",
                    new JArray(
                        new JObject(
                            new JProperty("id", "0"),
                            new JProperty("user", baseUser),
                            new JProperty("state", "0"),
                            new JProperty("value", "Crystal Lake")
                            )
                        )
                    ),
                    new JProperty("Songs",
                        new JArray(
                            new JObject(
                                new JProperty("Genre",
                                    new JObject(
                                        new JProperty("Name",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metalcore")
                                                    )
                                                )
                                            ),
                                        new JProperty("RootGenre",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metal")
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                new JProperty("Title",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Watch Me Burn")
                                            )
                                        )
                                    )
                                ),
                            new JObject(                                
                                new JProperty("Genre",
                                    new JObject(
                                        new JProperty("Name",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metalcore")
                                                    )
                                                )
                                            ),
                                        new JProperty("RootGenre",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metal")
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                new JProperty("Title",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Apollo")
                                            )
                                        )
                                    )
                                )
                            )                    
                        )         
            );

        class Artist
        {
            public string Name { get; set; }
            public Song[] Songs { get; set; }
        }

        class Song
        {
            public Genre Genre { get; set; }
            public string Title { get; set; }
        }

        class Genre
        {
            public string Name { get; set; }
            public string RootGenre { get; set; }
        }

        [Fact]
        public void Serialize_NestedObjectInList()
        {
            var genre = new Genre
            {
                Name = "Metalcore",
                RootGenre = "Metal"
            };
            var artist = new Artist
            {
                Name = "Crystal Lake",
                Songs = new Song[]
                {
                    new Song{
                        Genre = genre,
                        Title = "Watch Me Burn",
                    },
                    new Song{
                        Genre = genre,
                        Title = "Apollo",
                    }
                },
            };

            var result = JsonConvert.SerializeObject(artist, Formatting.Indented, jsonConverter);

            var n = nestedObjectListJsonObject.ToString();

            result.Should().Be(nestedObjectListJsonObject.ToString());
        }

        [Fact]
        public void Deserialize_NestedObjectInList_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Artist>(nestedObjectListJsonObject.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.Name.Should().Be("Crystal Lake");
            result.Songs.Should().NotBeNullOrEmpty();
            result.Songs.Should().HaveCount(2);
            result.Songs.Any(s => s.Title == "Watch Me Burn").Should().BeTrue();
            result.Songs.Any(s => s.Title == "Apollo").Should().BeTrue();
        }

        [Fact]
        public void Deserialize_NestedObjectInList_WithChanges()
        {
            var json = new JObject(
                new JProperty("Name",
                    new JArray(
                        new JObject(
                            new JProperty("id", "0"),
                            new JProperty("user", baseUser),
                            new JProperty("state", "0"),
                            new JProperty("value", "Crystal Lake")
                            ),
                        new JObject(
                            new JProperty("id", "1"),
                            new JProperty("user", user),
                            new JProperty("state", "0"),
                            new JProperty("value", "Nora En Pure")
                            )
                        )
                    ),
                    new JProperty("Songs",
                        new JArray(
                            new JObject(
                                new JProperty("Genre",
                                    new JObject(
                                        new JProperty("Name",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metalcore")
                                                    ),
                                                new JObject(
                                                    new JProperty("id", "1"),
                                                    new JProperty("user", user),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Electronic")
                                                    )
                                                )
                                            ),
                                        new JProperty("RootGenre",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metal")
                                                    ),
                                                new JObject(
                                                    new JProperty("id", "1"),
                                                    new JProperty("user", user),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "EDM")
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                new JProperty("Title",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Watch Me Burn")
                                            ),
                                        new JObject(
                                            new JProperty("id", "1"),
                                            new JProperty("user", user),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Wetlands")
                                            )
                                        )
                                    )
                                ),
                            new JObject(
                                new JProperty("Genre",
                                    new JObject(
                                        new JProperty("Name",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metalcore")
                                                    ),
                                                new JObject(
                                                    new JProperty("id", "1"),
                                                    new JProperty("user", user),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Electronic")
                                                    )
                                                )
                                            ),
                                        new JProperty("RootGenre",
                                            new JArray(
                                                new JObject(
                                                    new JProperty("id", "0"),
                                                    new JProperty("user", baseUser),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "Metal")
                                                    ),
                                                new JObject(
                                                    new JProperty("id", "1"),
                                                    new JProperty("user", user),
                                                    new JProperty("state", "0"),
                                                    new JProperty("value", "EDM")
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                new JProperty("Title",
                                    new JArray(
                                        new JObject(
                                            new JProperty("id", "0"),
                                            new JProperty("user", baseUser),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Apollo")
                                            ),
                                        new JObject(
                                            new JProperty("id", "1"),
                                            new JProperty("user", user),
                                            new JProperty("state", "0"),
                                            new JProperty("value", "Bartok")
                                            )
                                        )
                                    )
                                )
                            )
                        )
            );
            var result = JsonConvertWrapper.DeserializeObject<Artist>(json.ToString(), user, baseUser);

            result.Should().NotBeNull();
            result.Name.Should().Be("Nora En Pure");
            result.Songs.Should().NotBeNullOrEmpty();
            result.Songs.Should().HaveCount(2);
            result.Songs.Any(s => s.Title == "Wetlands").Should().BeTrue();
            result.Songs.Any(s => s.Title == "Bartok").Should().BeTrue();
        }
    }
}
