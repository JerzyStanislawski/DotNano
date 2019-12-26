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
    public class UncheckedGetResponseTest
    {
        string _json = "{\n  \"modified_timestamp\": \"1565856525\",\n  \"contents\": {\n    \"type\": \"state\",\n    \"account\": \"nano_1hmqzugsmsn4jxtzo5yrm4rsysftkh9343363hctgrjch1984d8ey9zoyqex\",\n    \"previous\": \"009C587914611E83EE7F75BD9C000C430C720D0364D032E84F37678D7D012911\",\n    \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n    \"balance\": \"189012679592109992600249228\",\n    \"link\": \"0000000000000000000000000000000000000000000000000000000000000000\",\n    \"link_as_account\": \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\",\n    \"signature\": \"845C8660750895843C013CE33E31B80EF0A7A69E52DDAF74A5F1BDFAA9A52E4D9EA2C3BE1AB0BD5790FCC1AD9B7A3D2F4B44EECE4279A8184D414A30A1B4620F\",\n    \"work\": \"0dfb32653e189699\"\n  }\n}";
        UncheckedGetResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<UncheckedGetResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(1565856525, _responseObject.ModifiedTimestamp);
        }
    }
}