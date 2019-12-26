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
    public class WalletLedgerResponseTest
    {
        string _json = "{\n  \"accounts\": {\n    \"nano_11119gbh8hb4hj1duf7fdtfyf5s75okzxdgupgpgm1bj78ex3kgy7frt3s9n\": {\n      \"frontier\": \"E71AF3E9DD86BBD8B4620EFA63E065B34D358CFC091ACB4E103B965F95783321\",\n      \"open_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"representative_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"balance\": \"0\",\n      \"modified_timestamp\": \"1511476234\",\n      \"block_count\": \"2\"\n    }\n  }\n}";
        WalletLedgerResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletLedgerResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class WalletLedgerResponseTest1
    {
        string _json = "{\n  \"accounts\": {\n    \"nano_11119gbh8hb4hj1duf7fdtfyf5s75okzxdgupgpgm1bj78ex3kgy7frt3s9n\": {\n      \"frontier\": \"E71AF3E9DD86BBD8B4620EFA63E065B34D358CFC091ACB4E103B965F95783321\",\n      \"open_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"representative_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"balance\": \"0\",\n      \"modified_timestamp\": \"1511476234\",\n      \"block_count\": \"2\",\n      \"representative\": \"nano_1anrzcuwe64rwxzcco8dkhpyxpi8kd7zsjc1oeimpc3ppca4mrjtwnqposrs\",\n      \"weight\": \"0\",\n      \"pending\": \"0\"\n    }\n  }\n}";
        WalletLedgerResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<WalletLedgerResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}