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
    public class ReceiveResponseTest
    {
        string _json = "{\n  \"block\": \"EE5286AB32F580AB65FD84A69E107C69FBEB571DEC4D99297E19E3FA5529547B\"\n}";
        ReceiveResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ReceiveResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("EE5286AB32F580AB65FD84A69E107C69FBEB571DEC4D99297E19E3FA5529547B", _responseObject.Block);
        }
    }
}