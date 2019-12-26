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
    public class WalletExportResponseTest
    {
        string _json = "{\n  \"json\" : \"{\\\"0000000000000000000000000000000000000000000000000000000000000000\\\": \\\"0000000000000000000000000000000000000000000000000000000000000001\\\"}\"\n}";
        WalletExportResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletExportResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("{\"0000000000000000000000000000000000000000000000000000000000000000\": \"0000000000000000000000000000000000000000000000000000000000000001\"}", _responseObject.Json);
        }
    }
}