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
    public class BlockCountResponseTest
    {
        string _json = "{\n  \"count\": \"1000\",\n  \"unchecked\": \"10\",\n  \"cemented\": \"25\"\n}";
        BlockCountResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlockCountResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(1000, _responseObject.Count);
            Assert.Equal(10, _responseObject.Unchecked);
            Assert.Equal(25, _responseObject.Cemented);
        }
    }
}