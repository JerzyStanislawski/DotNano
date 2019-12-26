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
    public class WalletPendingResponseTest
    {
        string _json = "{\n  \"blocks\": {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": [\"142A538F36833D1CC78B94E11C766F75818F8B940771335C6C1B8AB880C5BB1D\"],\n    \"nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3\": [\"4C1FEEF0BEA7F50BE35489A1233FE002B212DEA554B55B1B470D78BD8F210C74\"]\n  }\n}";
        WalletPendingBasicResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletPendingBasicResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class WalletPendingResponseTest1
    {
        string _json = "{\n  \"blocks\": {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": {\n      \"142A538F36833D1CC78B94E11C766F75818F8B940771335C6C1B8AB880C5BB1D\": \"6000000000000000000000000000000\"\n    },\n    \"nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3\": {\n      \"4C1FEEF0BEA7F50BE35489A1233FE002B212DEA554B55B1B470D78BD8F210C74\": \"106370018000000000000000000000000\"\n    }\n  }\n}";
        WalletPendingResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletPendingResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class WalletPendingResponseTest2
    {
        string _json = "{\n  \"blocks\": {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": {\n      \"142A538F36833D1CC78B94E11C766F75818F8B940771335C6C1B8AB880C5BB1D\": {\n        \"amount\": \"6000000000000000000000000000000\",\n        \"source\": \"nano_3dcfozsmekr1tr9skf1oa5wbgmxt81qepfdnt7zicq5x3hk65fg4fqj58mbr\"\n      }\n    },\n    \"nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3\": {\n      \"4C1FEEF0BEA7F50BE35489A1233FE002B212DEA554B55B1B470D78BD8F210C74\": {\n        \"amount\": \"106370018000000000000000000000000\",\n        \"source\": \"nano_13ezf4od79h1tgj9aiu4djzcmmguendtjfuhwfukhuucboua8cpoihmh8byo\"\n      }\n    }\n  }\n}";
        WalletPendingResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletPendingResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}