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
    public class BlockCreateResponseTest
    {
        string _json = "{\n  \"hash\": \"FF0144381CFF0B2C079A115E7ADA7E96F43FD219446E7524C48D1CC9900C4F17\",\n  \"block\": {\n    \"type\": \"state\",\n    \"account\": \"nano_3qgmh14nwztqw4wmcdzy4xpqeejey68chx6nciczwn9abji7ihhum9qtpmdr\",\n    \"previous\": \"F47B23107E5F34B2CE06F562B5C435DF72A533251CB414C51B2B62A8F63A00E4\",\n    \"representative\": \"nano_1hza3f7wiiqa7ig3jczyxj5yo86yegcmqk3criaz838j91sxcckpfhbhhra1\",\n    \"balance\": \"1000000000000000000000\",\n    \"link\": \"19D3D919475DEED4696B5D13018151D1AF88B2BD3BCFF048B45031C1F36D1858\",\n    \"link_as_account\": \"nano_18gmu6engqhgtjnppqam181o5nfhj4sdtgyhy36dan3jr9spt84rzwmktafc\",\n    \"signature\": \"3BFBA64A775550E6D49DF1EB8EEC2136DCD74F090E2ED658FBD9E80F17CB1C9F9F7BDE2B93D95558EC2F277FFF15FD11E6E2162A1714731B743D1E941FA4560A\",\n    \"work\": \"cab7404f0b5449d0\"\n  }\n}";
        BlockCreateResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlockCreateResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("FF0144381CFF0B2C079A115E7ADA7E96F43FD219446E7524C48D1CC9900C4F17", _responseObject.Hash);
        }
    }
}