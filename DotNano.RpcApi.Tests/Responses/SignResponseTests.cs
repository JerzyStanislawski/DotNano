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
    public class SignResponseTest
    {
        string _json = "{\n  \"signature\": \"2A71F3877033F5966735F260E906BFCB7FA82CDD543BCD1224F180F85A96FC26CB3F0E4180E662332A0DFE4EE6A0F798A71C401011E635604E532383EC08C70D\",\n  \"block\": {\n    \"type\": \"state\",\n    \"account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n    \"previous\": \"6CDDA48608C7843A0AC1122BDD46D9E20E21190986B19EAC23E7F33F2E6A6766\",\n    \"representative\": \"nano_3pczxuorp48td8645bs3m6c3xotxd3idskrenmi65rbrga5zmkemzhwkaznh\",\n    \"balance\": \"40200000001000000000000000000000000\",\n    \"link\": \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\",\n    \"link_as_account\": \"nano_33t5by1653nt196hfwm5q3wq7oxtaix97r7bhox5zn8eratrzoqsny49ftsd\",\n    \"signature\": \"2A71F3877033F5966735F260E906BFCB7FA82CDD543BCD1224F180F85A96FC26CB3F0E4180E662332A0DFE4EE6A0F798A71C401011E635604E532383EC08C70D\",\n    \"work\": \"000bc55b014e807d\"\n  }\n}";
        SignResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<SignResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("2A71F3877033F5966735F260E906BFCB7FA82CDD543BCD1224F180F85A96FC26CB3F0E4180E662332A0DFE4EE6A0F798A71C401011E635604E532383EC08C70D", _responseObject.Signature);
        }
    }

    public class SignResponseTest1
    {
        string _json = "{\n  \"signature\": \"2A71F3877033F5966735F260E906BFCB7FA82CDD543BCD1224F180F85A96FC26CB3F0E4180E662332A0DFE4EE6A0F798A71C401011E635604E532383EC08C70D\"\n}";
        SignResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<SignResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("2A71F3877033F5966735F260E906BFCB7FA82CDD543BCD1224F180F85A96FC26CB3F0E4180E662332A0DFE4EE6A0F798A71C401011E635604E532383EC08C70D", _responseObject.Signature);
        }
    }
}