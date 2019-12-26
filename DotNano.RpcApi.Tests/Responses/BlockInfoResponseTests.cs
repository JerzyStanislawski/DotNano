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
    public class BlockInfoResponseTest
    {
        string _json = "{\n  \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n  \"amount\": \"30000000000000000000000000000000000\",\n  \"balance\": \"5606157000000000000000000000000000000\",\n  \"height\": \"58\",\n  \"local_timestamp\": \"0\",\n  \"confirmed\": \"true\",\n  \"contents\": {\n    \"type\": \"state\",\n    \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n    \"previous\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n    \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n    \"balance\": \"5606157000000000000000000000000000000\",\n    \"link\": \"5D1AA8A45F8736519D707FCB375976A7F9AF795091021D7E9C7548D6F45DD8D5\",\n    \"link_as_account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n    \"signature\": \"82D41BC16F313E4B2243D14DFFA2FB04679C540C2095FEE7EAE0F2F26880AD56DD48D87A7CC5DD760C5B2D76EE2C205506AA557BF00B60D8DEE312EC7343A501\",\n    \"work\": \"8a142e07a10996d5\"\n  },\n  \"subtype\": \"send\"\n}";
        BlockInfoResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BlockInfoResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est", _responseObject.BlockAccount);
            Assert.Equal(BigInteger.Parse("30000000000000000000000000000000000"), _responseObject.Amount);
            Assert.Equal(BigInteger.Parse("5606157000000000000000000000000000000"), _responseObject.Balance);
            Assert.Equal(58, _responseObject.Height);
            Assert.Equal(0, _responseObject.LocalTimestamp);
            Assert.Equal(true, _responseObject.Confirmed);
            Assert.Equal("send", _responseObject.Subtype);
        }
    }
}