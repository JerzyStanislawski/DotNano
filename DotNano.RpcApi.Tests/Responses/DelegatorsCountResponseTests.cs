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
    public class DelegatorsCountResponseTest
    {
        string _json = "{\n  \"count\": \"2\"\n}";
        DelegatorsCountResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<DelegatorsCountResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(2, _responseObject.Count);
        }
    }
}