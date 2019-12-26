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
    public class WalletInfoResponseTest
    {
        string _json = "{\n  \"balance\": \"10000\",\n  \"pending\": \"10000\",\n  \"accounts_count\": \"3\",\n  \"adhoc_count\": \"1\",\n  \"deterministic_count\": \"2\",\n  \"deterministic_index\": \"2\"\n}";
        WalletInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(10000, _responseObject.Balance);
            Assert.Equal(10000, _responseObject.Pending);
            Assert.Equal(3, _responseObject.AccountsCount);
            Assert.Equal(1, _responseObject.AdhocCount);
            Assert.Equal(2, _responseObject.DeterministicCount);
            Assert.Equal(2, _responseObject.DeterministicIndex);
        }
    }
}