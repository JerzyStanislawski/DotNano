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
    public class BlockHashResponseTest
    {
        string _json = "{\n  \"hash\": \"FF0144381CFF0B2C079A115E7ADA7E96F43FD219446E7524C48D1CC9900C4F17\"\n}";
        BlockHashResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlockHashResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("FF0144381CFF0B2C079A115E7ADA7E96F43FD219446E7524C48D1CC9900C4F17", _responseObject.Hash);
        }
    }
}