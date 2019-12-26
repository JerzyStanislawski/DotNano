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
    public class WalletChangeSeedResponseTest
    {
        string _json = "{\n  \"success\" : \"\",\n  \"last_restored_account\": \"nano_1mhdfre3zczr86mp44jd3xft1g1jg66jwkjtjqixmh6eajfexxti7nxcot9c\",\n  \"restored_count\": \"1\"\n}";
        WalletChangeSeedResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletChangeSeedResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("nano_1mhdfre3zczr86mp44jd3xft1g1jg66jwkjtjqixmh6eajfexxti7nxcot9c", _responseObject.LastRestoredAccount);
            Assert.Equal(1, _responseObject.RestoredCount);
        }
    }
}