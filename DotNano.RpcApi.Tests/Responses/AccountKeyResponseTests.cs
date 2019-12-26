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
    public class AccountKeyResponseTest
    {
        string _json = "{\n  \"key\": \"3068BB1CA04525BB0E416C485FE6A67FD52540227D267CC8B6E8DA958A7FA039\"\n}";
        AccountKeyResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<AccountKeyResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("3068BB1CA04525BB0E416C485FE6A67FD52540227D267CC8B6E8DA958A7FA039", _responseObject.Key);
        }
    }
}