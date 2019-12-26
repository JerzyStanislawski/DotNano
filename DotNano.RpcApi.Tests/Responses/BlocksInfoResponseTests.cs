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
    public class BlocksInfoResponseTest
    {
        string _json = "{\n  \"blocks\": {\n    \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\": {\n      \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"amount\": \"30000000000000000000000000000000000\",\n      \"balance\": \"5606157000000000000000000000000000000\",\n      \"height\": \"58\",\n      \"local_timestamp\": \"0\",\n      \"confirmed\": \"true\",\n      \"contents\": {\n        \"type\": \"state\",\n        \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n        \"previous\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n        \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n        \"balance\": \"5606157000000000000000000000000000000\",\n        \"link\": \"5D1AA8A45F8736519D707FCB375976A7F9AF795091021D7E9C7548D6F45DD8D5\",\n        \"link_as_account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n        \"signature\": \"82D41BC16F313E4B2243D14DFFA2FB04679C540C2095FEE7EAE0F2F26880AD56DD48D87A7CC5DD760C5B2D76EE2C205506AA557BF00B60D8DEE312EC7343A501\",\n        \"work\": \"8a142e07a10996d5\"\n      },\n      \"subtype\": \"send\"\n    }\n  }\n}";
        BlocksInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlocksInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class BlocksInfoResponseTest1
    {
        string _json = "{\n  \"blocks\" : {\n    \"E2FB233EF4554077A7BF1AA85851D5BF0B36965D2B0FB504B2BC778AB89917D3\": {\n      \"block_account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n      \"amount\": \"30000000000000000000000000000000000\",\n      \"contents\": {\n        \n      },\n      \"pending\": \"0\",\n      \"source_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"balance\": \"40200000001000000000000000000000000\"\n    }\n  }\n}";
        BlocksInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlocksInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class BlocksInfoResponseTest2
    {
        string _json = "{\n  \"blocks\" : {\n    \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\": {\n      \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"amount\": \"30000000000000000000000000000000000\",\n      \"balance\": \"5606157000000000000000000000000000000\",\n      \"height\": \"58\",\n      \"local_timestamp\": \"0\",\n      \"confirmed\": \"false\",\n      \"contents\": {\n        \n      }\n    }\n  },\n  \"blocks_not_found\": [\n    \"0000000000000000000000000000000000000000000000000000000000000001\"\n  ]\n}";
        BlocksInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlocksInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}