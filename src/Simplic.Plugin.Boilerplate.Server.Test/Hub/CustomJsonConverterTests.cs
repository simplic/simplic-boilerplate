using FluentAssertions;
using Newtonsoft.Json;
using Simplic.PlugIn.Boilerplate.Server;
using System.Collections.Generic;
using Xunit;

namespace Simplic.Plugin.Boilerplate.Server.Test
{
    public class CustomJsonConverterTests
    {
        private readonly string user;
        private readonly string baseUser;
        private CustomJsonConverter jsonConverter;

        public CustomJsonConverterTests()
        {
            baseUser = "SuperUser";
            user = "Test";
            jsonConverter = new CustomJsonConverter(baseUser);
        }

        class Person
        {
            public string Name { get; set; }
        }

        private readonly string simpleObjectJson = "{\r\n  \"Name\": [\r\n    {\r\n      \"id\": \"0\",\r\n      \"user\": \"SuperUser\",\r\n      \"state\": \"0\",\r\n      \"value\": \"Peter\"\r\n    }\r\n  ]\r\n}";

        [Fact]
        public void Serialize_SimpleObject()
        {
            var contact = new Person
            {
                Name = "Peter",
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be(simpleObjectJson);
        }

        [Fact]
        public void Deserialize_SimpleObject_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Person>(simpleObjectJson, user, baseUser);

            result.Should().NotBeNull();
            result.Name.Should().Be("Peter");
        }

        [Fact]
        public void Deserialize_SimpleObject_WithChanges()
        {
            var json = "{\r\n  \"Name\": [\r\n    {\r\n      \"id\": \"0\",\r\n      \"user\": \"SuperUser\",\r\n      \"state\": \"0\",\r\n      \"value\": \"Peter\"\r\n    }\r\n,    {\r\n      \"id\": \"1\",\r\n      \"user\": \"Test\",\r\n      \"state\": \"0\",\r\n      \"value\": \"Gunther\"\r\n    }\r\n  ]\r\n}";

            var result = JsonConvertWrapper.DeserializeObject<Person>(json, user, baseUser);

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

        private readonly string nestedObjectJson = "{\r\n  \"Phone\": {\r\n    \"Type\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"Home\"\r\n      }\r\n    ],\r\n    \"Number\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"123456789\"\r\n      }\r\n    ]\r\n  }\r\n}";

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

            result.Should().Be(nestedObjectJson);
        }

        [Fact]
        public void Deserialize_NestedObject_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Contact>(nestedObjectJson, user, baseUser);
            result.Should().NotBeNull();
            result.Phone.Should().NotBeNull();
            result.Phone.Number.Should().Be("123456789");
            result.Phone.Type.Should().Be("Home");
        }

        [Fact]
        public void Deserialize_NestedObject_WithChanges()
        {
            var json = "{\r\n  \"Phone\": {\r\n    \"Type\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"value\": \"Home\"\r\n      }\r\n, \r\n      {\r\n        \"id\": \"1\",\r\n        \"user\": \"Test\",\r\n        \"value\": \"Zuhause\"\r\n      }\r\n    ],\r\n    \"Number\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"value\": \"123456789\"\r\n      }\r\n, {\r\n        \"id\": \"1\",\r\n        \"user\": \"Test\",\r\n        \"value\": \"9999\"\r\n      }\r\n    ]\r\n  }\r\n}";

            var result = JsonConvertWrapper.DeserializeObject<Contact>(json, user, baseUser);
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

        private readonly string doubleNestedObjectJson = "{\r\n  \"State\": {\r\n    \"Town\": {\r\n      \"Name\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"state\": \"0\",\r\n          \"value\": \"Hildesheim\"\r\n        }\r\n      ],\r\n      \"Zip\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"state\": \"0\",\r\n          \"value\": \"31134\"\r\n        }\r\n      ]\r\n    },\r\n    \"Name\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"Niedersachsen\"\r\n      }\r\n    ]\r\n  }\r\n}";

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

            result.Should().Be(doubleNestedObjectJson);
        }

        [Fact]
        public void Deserialize_DoubleNestedObject_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Adress>(doubleNestedObjectJson, user, baseUser);

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
            var json = "{\r\n  \"State\": {\r\n    \"Town\": {\r\n      \"Name\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"value\": \"Hildesheim\"\r\n        }\r\n, {\r\n          \"id\": \"1\",\r\n          \"user\": \"Test\",\r\n          \"value\": \"Hannover\"\r\n        }\r\n      ],\r\n      \"Zip\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"value\": \"31134\"\r\n        }\r\n, {\r\n          \"id\": \"1\",\r\n          \"user\": \"Test\",\r\n          \"value\": \"30159\"\r\n        }\r\n       ]\r\n    },\r\n    \"Name\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"value\": \"Niedersachsen\"\r\n      }\r\n, {\r\n        \"id\": \"1\",\r\n        \"user\": \"Test\",\r\n        \"value\": \"Sachsen\"\r\n      }\r\n    ]\r\n  }\r\n}";

            var result = JsonConvertWrapper.DeserializeObject<Adress>(json, user, baseUser);

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

        private readonly string simpleListJson = "{\r\n  \"Tags\": [\r\n    [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"TestTag\"\r\n      }\r\n    ],\r\n    [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"DemoTag\"\r\n      }\r\n    ],\r\n    [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"AnotherTag\"\r\n      }\r\n    ]\r\n  ]\r\n}";

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

            result.Should().Be(simpleListJson);
        }

        [Fact]
        public void Deserialize_SimpleList_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Folder>(simpleListJson, user, baseUser);

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
            var simpleListJson = "{\r\n  \"Tags\": [\r\n    [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"TestTag\"\r\n      }\r\n, {\r\n        \"id\": \"1\",\r\n        \"user\": \"Test\",\r\n        \"state\": \"-1\",\r\n        \"value\": \"TestTag\"\r\n      }\r\n    ],\r\n    [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"DemoTag\"\r\n      }\r\n    ],\r\n    [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"state\": \"0\",\r\n        \"value\": \"AnotherTag\"\r\n      }\r\n    ]\r\n  ]\r\n}";
            var result = JsonConvertWrapper.DeserializeObject<Folder>(simpleListJson, user, baseUser);

            result.Should().NotBeNull();
            result.Tags.Should().NotBeNullOrEmpty();
            result.Tags.Should().HaveCount(2);
            result.Tags.Should().Contain("DemoTag");
            result.Tags.Should().Contain("AnotherTag");
        }

        private readonly string nestedObjectListJson = "{\r\n  \"Name\": [\r\n    {\r\n      \"id\": \"0\",\r\n      \"user\": \"SuperUser\",\r\n      \"state\": \"0\",\r\n      \"value\": \"Crystal Lake\"\r\n    }\r\n  ],\r\n  \"Songs\": [\r\n    {\r\n      \"Genre\": {\r\n        \"Name\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"state\": \"0\",\r\n            \"value\": \"Metalcore\"\r\n          }\r\n        ],\r\n        \"RootGenre\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"state\": \"0\",\r\n            \"value\": \"Metal\"\r\n          }\r\n        ]\r\n      },\r\n      \"Title\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"state\": \"0\",\r\n          \"value\": \"Watch Me Burn\"\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"Genre\": {\r\n        \"Name\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"state\": \"0\",\r\n            \"value\": \"Metalcore\"\r\n          }\r\n        ],\r\n        \"RootGenre\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"state\": \"0\",\r\n            \"value\": \"Metal\"\r\n          }\r\n        ]\r\n      },\r\n      \"Title\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"state\": \"0\",\r\n          \"value\": \"Apollo\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";
        
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
            var contact = new Artist
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

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be(nestedObjectListJson);
        }

        [Fact]
        public void Deserialize_NestedObjectInList_WithoutChanges()
        {
            var result = JsonConvertWrapper.DeserializeObject<Artist>(nestedObjectListJson, user, baseUser);

            result.Should().NotBeNull();
            result.Name.Should().Be("Crystal Lake");
            result.Songs.Should().NotBeNullOrEmpty();
            result.Songs.Should().HaveCount(2);
        }
    }
}
