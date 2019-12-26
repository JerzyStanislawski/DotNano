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
    public class WalletCreateResponseTest
    {
        string _json = "{\n  \"wallet\": \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n}";
        WalletCreateResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletCreateResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F", _responseObject.Wallet);
        }
    }
}