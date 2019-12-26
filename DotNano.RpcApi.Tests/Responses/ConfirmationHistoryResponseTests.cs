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
    public class ConfirmationHistoryResponseTest
    {
        string _json = "{\n  \"confirmation_stats\": {\n    \"count\": \"2\",\n    \"average\": \"5000\"\n  },\n  \"confirmations\": [\n    {\n      \"hash\": \"EA70B32C55C193345D625F766EEA2FCA52D3F2CCE0B3A30838CC543026BB0FEA\",\n      \"duration\": \"4000\",\n      \"time\": \"1544819986\",\n      \"tally\": \"80394786589602980996311817874549318248\",\n      \"blocks\": \"1\", // since V21.0\n      \"voters\": \"37\", // since V21.0\n      \"request_count\": \"2\" // since V20.0\n    },\n    {\n      \"hash\": \"F2F8DA6D2CA0A4D78EB043A7A29E12BDE5B4CE7DE1B99A93A5210428EE5B8667\",\n      \"duration\": \"6000\",\n      \"time\": \"1544819988\",\n      \"tally\": \"68921714529890443063672782079965877749\",\n      \"blocks\": \"1\", // since V21.0\n      \"voters\": \"64\", // since V21.0\n      \"request_count\": \"7\" // since V20.0\n    }\n  ]\n}";
        ConfirmationHistoryResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ConfirmationHistoryResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}