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
    public class ReceiveMinimumSetResponseTest
    {
        string _json = "{\n  \"success\": \"\"\n}";
        ReceiveMinimumSetResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ReceiveMinimumSetResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("", _responseObject.Success);
        }
    }
}