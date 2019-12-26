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
    public class WorkValidateResponseTest
    {
        string _json = "{\n  \"valid\": \"1\",\n  \"difficulty\": \"ffffffd21c3933f4\",\n  \"multiplier\": \"1.394647\"\n}";
        WorkValidateResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WorkValidateResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(1, _responseObject.Valid);
            Assert.Equal("ffffffd21c3933f4", _responseObject.Difficulty);
            Assert.Equal(1.394647, _responseObject.Multiplier);
        }
    }
}