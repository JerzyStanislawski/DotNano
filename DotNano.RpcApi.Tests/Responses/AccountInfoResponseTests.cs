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
    public class AccountInfoResponseTest
    {
        string _json = "{\n  \"frontier\": \"FF84533A571D953A596EA401FD41743AC85D04F406E76FDE4408EAED50B473C5\",\n  \"open_block\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n  \"representative_block\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n  \"balance\": \"235580100176034320859259343606608761791\",\n  \"modified_timestamp\": \"1501793775\",\n  \"block_count\": \"33\",\n  \"confirmation_height\" : \"28\",\n  \"account_version\": \"1\"\n}";
        AccountInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<AccountInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("FF84533A571D953A596EA401FD41743AC85D04F406E76FDE4408EAED50B473C5", _responseObject.Frontier);
            Assert.Equal("991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948", _responseObject.OpenBlock);
            Assert.Equal("991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948", _responseObject.RepresentativeBlock);
            Assert.Equal(BigInteger.Parse("235580100176034320859259343606608761791"), _responseObject.Balance);
            Assert.Equal(1501793775, _responseObject.ModifiedTimestamp);
            Assert.Equal(33, _responseObject.BlockCount);
            Assert.Equal(28, _responseObject.ConfirmationHeight);
            Assert.Equal(1, _responseObject.AccountVersion);
        }
    }

    public class AccountInfoResponseTest1
    {
        string _json = "{\n  \"frontier\": \"FF84533A571D953A596EA401FD41743AC85D04F406E76FDE4408EAED50B473C5\",\n  \"open_block\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n  \"representative_block\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n  \"balance\": \"235580100176034320859259343606608761791\",\n  \"modified_timestamp\": \"1501793775\",\n  \"block_count\": \"33\",\n  \"representative\": \"nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3\",\n  \"weight\": \"1105577030935649664609129644855132177\",\n  \"pending\": \"2309370929000000000000000000000000\"\n}";
        AccountInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<AccountInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("FF84533A571D953A596EA401FD41743AC85D04F406E76FDE4408EAED50B473C5", _responseObject.Frontier);
            Assert.Equal("991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948", _responseObject.OpenBlock);
            Assert.Equal("991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948", _responseObject.RepresentativeBlock);
            Assert.Equal(BigInteger.Parse("235580100176034320859259343606608761791"), _responseObject.Balance);
            Assert.Equal(1501793775, _responseObject.ModifiedTimestamp);
            Assert.Equal(33, _responseObject.BlockCount);
            Assert.Equal("nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3", _responseObject.Representative);
            Assert.Equal(BigInteger.Parse("1105577030935649664609129644855132177"), _responseObject.Weight);
            Assert.Equal(BigInteger.Parse("2309370929000000000000000000000000"), _responseObject.Pending);
        }
    }
}