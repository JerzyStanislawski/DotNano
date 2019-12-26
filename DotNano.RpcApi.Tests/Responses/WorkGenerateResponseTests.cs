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
    public class WorkGenerateResponseTest
    {
        string _json = "{\n  \"work\": \"2bf29ef00786a6bc\",\n  \"difficulty\": \"ffffffd21c3933f4\",\n  \"multiplier\": \"1.394647\",\n  \"hash\": \"718CC2121C3E641059BC1C2CFC45666C99E8AE922F7A807B7D07B62C995D79E2\" // since v20.0\n}";
        WorkGenerateResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WorkGenerateResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("2bf29ef00786a6bc", _responseObject.Work);
            Assert.Equal("ffffffd21c3933f4", _responseObject.Difficulty);
            Assert.Equal(1.394647, _responseObject.Multiplier);
            Assert.Equal("718CC2121C3E641059BC1C2CFC45666C99E8AE922F7A807B7D07B62C995D79E2", _responseObject.Hash);
        }
    }
}