using FluentAssertions;
using Newtonsoft.Json;
using Simplic.Boilerplate.Shared;
using Simplic.PlugIn.Boilerplate.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Simplic.Plugin.Boilerplate.Server.Test
{
    public class CustomJsonConverterTests
    {
        private CustomJsonConverter jsonConverter;

        public CustomJsonConverterTests()
        {
            jsonConverter = new CustomJsonConverter();
        }

        [Fact]
        public void ShouldSerialize_SimpleObject()
        {
            var contact = new
            {
                Name = "Peter",
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be("{\r\n  \"Name\": [\r\n    {\r\n      \"id\": \"0\",\r\n      \"user\": \"SuperUser\",\r\n      \"value\": \"Peter\"\r\n    }\r\n  ]\r\n}");
        }

        [Fact]
        public void ShouldSerialize_NestedObject()
        {
            var contact = new
            {
                Phone = new
                {
                    Type = "Home",
                    Number = "123456789"
                },
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be("{\r\n  \"Phone\": {\r\n    \"Type\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"value\": \"Home\"\r\n      }\r\n    ],\r\n    \"Number\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"value\": \"123456789\"\r\n      }\r\n    ]\r\n  }\r\n}");
        }

        [Fact]
        public void ShouldSerialize_DoubleNestedObject()
        {
            var contact = new
            {
                Phone = new
                {
                    Type = new
                    {
                        Name = "Home",
                        Zip = "11111"
                    },
                    Number = "123456789",
                },
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be("{\r\n  \"Phone\": {\r\n    \"Type\": {\r\n      \"Name\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"value\": \"Home\"\r\n        }\r\n      ],\r\n      \"Zip\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"value\": \"11111\"\r\n        }\r\n      ]\r\n    },\r\n    \"Number\": [\r\n      {\r\n        \"id\": \"0\",\r\n        \"user\": \"SuperUser\",\r\n        \"value\": \"123456789\"\r\n      }\r\n    ]\r\n  }\r\n}");
        }

        [Fact]
        public void ShouldSerialize_NestedObjectInLists()
        {
            var contact = new
            {
                Phone = new[]
                {
                    new{
                        Type = new
                        {
                            Name = "Home",
                            Zip = "11111"
                        },
                        Number = "123456789",
                    },
                    new{
                        Type = new
                        {
                            Name = "Work",
                            Zip = "22222"
                        },
                        Number = "777555333",
                    }
                },
            };

            var result = JsonConvert.SerializeObject(contact, Formatting.Indented, jsonConverter);

            result.Should().Be("{\r\n  \"Phone\": [\r\n    {\r\n      \"Type\": {\r\n        \"Name\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"value\": \"Home\"\r\n          }\r\n        ],\r\n        \"Zip\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"value\": \"11111\"\r\n          }\r\n        ]\r\n      },\r\n      \"Number\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"value\": \"123456789\"\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"Type\": {\r\n        \"Name\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"value\": \"Work\"\r\n          }\r\n        ],\r\n        \"Zip\": [\r\n          {\r\n            \"id\": \"0\",\r\n            \"user\": \"SuperUser\",\r\n            \"value\": \"22222\"\r\n          }\r\n        ]\r\n      },\r\n      \"Number\": [\r\n        {\r\n          \"id\": \"0\",\r\n          \"user\": \"SuperUser\",\r\n          \"value\": \"777555333\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}");
        }
    }
}
