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
    public class ActiveDifficultyResponseTest
    {
        string _json = "{\n  \"network_minimum\": \"ffffffc000000000\",\n  \"network_current\": \"ffffffcdbf40aa45\",\n  \"multiplier\": \"1.273557846739298\"\n}";
        ActiveDifficultyResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ActiveDifficultyResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("ffffffc000000000", _responseObject.NetworkMinimum);
            Assert.Equal("ffffffcdbf40aa45", _responseObject.NetworkCurrent);
            Assert.Equal(1.273557846739298, _responseObject.Multiplier);
        }
    }

    public class ActiveDifficultyResponseTest1
    {
        string _json = "{\n  \"network_minimum\": \"ffffffc000000000\",\n  \"network_current\": \"ffffffc1816766f2\",\n  \"multiplier\": \"1.024089858417128\",\n  \"difficulty_trend\": [\n    \"1.156096135149775\",\n    \"1.190133894573061\",\n    \"1.135567138563921\",\n    \"1.000000000000000\",\n    \n    \"1.000000000000000\"\n  ]\n}";
        ActiveDifficultyResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ActiveDifficultyResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("ffffffc000000000", _responseObject.NetworkMinimum);
            Assert.Equal("ffffffc1816766f2", _responseObject.NetworkCurrent);
            Assert.Equal(1.024089858417128, _responseObject.Multiplier);
        }
    }
}