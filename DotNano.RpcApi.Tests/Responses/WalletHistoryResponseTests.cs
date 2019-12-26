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
    public class WalletHistoryResponseTest
    {
        string _json = "{\n  \"history\":\n  [\n    {\n      \"type\": \"send\",\n      \"account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n      \"amount\": \"30000000000000000000000000000000000\",\n      \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"hash\": \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\",\n      \"local_timestamp\": \"1527698508\"\n    },\n    {\n      \"type\": \"send\",\n      \"account\": \"nano_38ztgpejb7yrm7rr586nenkn597s3a1sqiy3m3uyqjicht7kzuhnihdk6zpz\",\n      \"amount\": \"40000000000000000000000000000000000\",\n      \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"hash\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n      \"local_timestamp\": \"1527698492\"\n    }\n  ]\n}";
        WalletHistoryResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletHistoryResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}