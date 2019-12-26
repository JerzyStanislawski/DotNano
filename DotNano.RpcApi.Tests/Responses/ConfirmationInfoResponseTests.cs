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
    public class ConfirmationInfoResponseTest
    {
        string _json = "{\n  \"announcements\": \"2\",\n  \"voters\": \"29\",\n  \"last_winner\": \"B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F\",\n  \"total_tally\": \"51145880360832646375807054724596663794\",\n  \"blocks\": {\n    \"B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F\": {\n      \"tally\": \"51145880360832646375807054724596663794\",\n      \"contents\": {\n        \"type\": \"state\",\n        \"account\": \"nano_3fihmbtuod33s4nrbqfczhk9zy9ddqimwjshzg4c3857es8c9631i5rg6h9p\",\n        \"previous\": \"EE125B1B1D85D3C24636B3590E1642D9F21B166C0C6CD99C9C6087A1224A0C44\",\n        \"representative\": \"nano_3o7uzba8b9e1wqu5ziwpruteyrs3scyqr761x7ke6w1xctohxfh5du75qgaj\",\n        \"balance\": \"218195000000000000000000000000\",\n        \"link\": \"0000000000000000000000000000000000000000000000000000000000000000\",\n        \"link_as_account\": \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\",\n        \"signature\": \"B1BD285235C612C5A141FA61793D7C6C762D3F104A85102DED5FBD6B4514971C4D044ACD3EC8C06A9495D8E83B6941B54F8DABA825ADF799412ED9E2C86D7A0C\",\n        \"work\": \"05bb28cd8acbe71d\"\n      }\n    }\n  }\n}";
        ConfirmationInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ConfirmationInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(2, _responseObject.Announcements);
            Assert.Equal(29, _responseObject.Voters);
            Assert.Equal("B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F", _responseObject.LastWinner);
            Assert.Equal(BigInteger.Parse("51145880360832646375807054724596663794"), _responseObject.TotalTally);
        }
    }

    public class ConfirmationInfoResponseTest1
    {
        string _json = "{\n  \"announcements\": \"5\",\n  \"last_winner\": \"B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F\",\n  \"total_tally\": \"51145880360792646375807054724596663794\",\n  \"blocks\": {\n    \"B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F\": {\n      \"tally\": \"51145880360792646375807054724596663794\",\n      \"contents\": {\n        \"type\": \"state\",\n        \"account\": \"nano_3fihmbtuod33s4nrbqfczhk9zy9ddqimwjshzg4c3857es8c9631i5rg6h9p\",\n        \"previous\": \"EE125B1B1D85D3C24636B3590E1642D9F21B166C0C6CD99C9C6087A1224A0C44\",\n        \"representative\": \"nano_3o7uzba8b9e1wqu5ziwpruteyrs3scyqr761x7ke6w1xctohxfh5du75qgaj\",\n        \"balance\": \"218195000000000000000000000000\",\n        \"link\": \"0000000000000000000000000000000000000000000000000000000000000000\",\n        \"link_as_account\": \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\",\n        \"signature\": \"B1BD285235C612C5A141FA61793D7C6C762D3F104A85102DED5FBD6B4514971C4D044ACD3EC8C06A9495D8E83B6941B54F8DABA825ADF799412ED9E2C86D7A0C\",\n        \"work\": \"05bb28cd8acbe71d\"\n      },\n      \"representatives\": {\n        \"nano_3pczxuorp48td8645bs3m6c3xotxd3idskrenmi65rbrga5zmkemzhwkaznh\": \"12617828599372664613607727105312358589\",\n        \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\": \"5953738757270291536911559258663615240\",\n        \n        \"nano_3i4n5n6c6xssapbdtkdoutm88c5zjmatc5tc77xyzdkpef8akid9errcpjnx\": \"0\"\n      }\n    }\n  }\n}";
        ConfirmationInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ConfirmationInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(5, _responseObject.Announcements);
            Assert.Equal("B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F", _responseObject.LastWinner);
            Assert.Equal(BigInteger.Parse("51145880360792646375807054724596663794"), _responseObject.TotalTally);
        }
    }
}