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
    public class BlockCountTypeResponseTest
    {
        string _json = "{\n  \"send\": \"5016664\",\n  \"receive\": \"4081228\",\n  \"open\": \"546457\",\n  \"change\": \"24193\",\n  \"state_v0\": \"4216537\",\n  \"state_v1\": \"10653709\",\n  \"state\": \"14870246\"\n}";
        BlockCountTypeResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlockCountTypeResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(5016664, _responseObject.Send);
            Assert.Equal(4081228, _responseObject.Receive);
            Assert.Equal(546457, _responseObject.Open);
            Assert.Equal(24193, _responseObject.Change);
            Assert.Equal(4216537, _responseObject.StateV0);
            Assert.Equal(10653709, _responseObject.StateV1);
            Assert.Equal(14870246, _responseObject.State);
        }
    }
}