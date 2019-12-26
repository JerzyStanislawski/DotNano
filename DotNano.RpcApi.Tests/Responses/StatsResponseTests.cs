using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared;
using DotNano.Shared.DataTypes;
using DotNano.RpcApi.Responses;
using Newtonsoft.Json;
using Xunit;

namespace DotNano.RpcApi.Tests.Responses
{
    public class StatsResponseTest
    {
        string _json = "{\n  \"type\": \"counters\",\n  \"created\": \"2018.03.29 01:46:36\",\n  \"entries\": [\n    {\n      \"time\": \"01:46:36\",\n      \"type\": \"traffic_tcp\",\n      \"detail\": \"all\",\n      \"dir\": \"in\",\n      \"value\": \"3122792\"\n    },\n    {\n      \"time\": \"01:46:36\",\n      \"type\": \"traffic_tcp\",\n      \"detail\": \"all\",\n      \"dir\": \"out\",\n      \"value\": \"203184\"\n    }\n    \n  ]\n}";
        StatsResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<StatsResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }

        [Fact]
        public void ShouldPopulateResponseObjectValues()
        {
            CreateResponseObject();
            Assert.Equal("counters", _responseObject.Type);
            Assert.Equal("2018.03.29 01:46:36", _responseObject.Created);
        }
    }

    public class StatsResponseTest1
    {
        string _json = "{\n  \"type\": \"samples\",\n  \"created\": \"2018.03.29 01:47:08\",\n  \"entries\": [\n    {\n      \"time\": \"01:47:04\",\n      \"type\": \"traffic_tcp\",\n      \"detail\": \"all\",\n      \"dir\": \"in\",\n      \"value\": \"59480\"\n    },\n    {\n      \"time\": \"01:47:05\",\n      \"type\": \"traffic_tcp\",\n      \"detail\": \"all\",\n      \"dir\": \"in\",\n      \"value\": \"44496\"\n    }\n    \n   ]\n}";
        StatsResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<StatsResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }

        [Fact]
        public void ShouldPopulateResponseObjectValues()
        {
            CreateResponseObject();
            Assert.Equal("samples", _responseObject.Type);
            Assert.Equal("2018.03.29 01:47:08", _responseObject.Created);
        }
    }

    public class StatsResponseTest2
    {
        string _json = "{\n  \"node\": {\n    \"ledger\": {\n      \"bootstrap_weights\": {\n        \"count\": \"125\",\n        \"size\": \"7000\"\n      }\n    },\n    \"peers\": {\n      \"peers\": {\n        \"count\": \"38\",\n        \"size\": \"7296\"\n      },\n      \"attempts\": {\n        \"count\": \"95\",\n        \"size\": \"3800\"\n      },\n    },\n    \n  }\n}";
        StatsResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<StatsResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}