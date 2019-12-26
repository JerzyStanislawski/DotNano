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
    public class UncheckedKeysResponseTest
    {
        string _json = "{\n  \"unchecked\": [\n    {\n      \"key\": \"19BF0C268C2D9AED1A8C02E40961B67EA56B1681DE274CD0C50F3DD972F0655C\",\n      \"hash\": \"A1A8558CBABD3F7C1D70F8CB882355F2EF688E7F30F5FDBD0204CAE157885056\",\n      \"modified_timestamp\": \"1565856744\",\n      \"contents\": {\n        \"type\": \"state\",\n        \"account\": \"nano_1hmqzugsmsn4jxtzo5yrm4rsysftkh9343363hctgrjch1984d8ey9zoyqex\",\n        \"previous\": \"19BF0C268C2D9AED1A8C02E40961B67EA56B1681DE274CD0C50F3DD972F0655C\",\n        \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n        \"balance\": \"189012679592109992600249226\",\n        \"link\": \"0000000000000000000000000000000000000000000000000000000000000000\",\n        \"link_as_account\": \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\",\n        \"signature\": \"FF5D49925AD3C8705E6EEDD993E8C4120E6107D7F1CB53B287773448DEA0B1D32918E67804248FC83609F0D93401D833DFA33127F21B6CD02F75D6E31A00450A\",\n        \"work\": \"8193ddf00947e694\"\n      }\n    }\n  ]\n}";
        UncheckedKeysResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<UncheckedKeysResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}