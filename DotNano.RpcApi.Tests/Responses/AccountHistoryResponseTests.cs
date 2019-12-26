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
    public class AccountHistoryResponseTest
    {
        string _json = "{\n  \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n  \"history\": [\n    {\n      \"type\": \"send\",\n      \"account\": \"nano_38ztgpejb7yrm7rr586nenkn597s3a1sqiy3m3uyqjicht7kzuhnihdk6zpz\",\n      \"amount\": \"80000000000000000000000000000000000\",\n      \"local_timestamp\": \"1551532723\",\n      \"height\": \"60\",\n      \"hash\": \"80392607E85E73CC3E94B4126F24488EBDFEB174944B890C97E8F36D89591DC5\"\n    }\n  ],\n  \"previous\": \"8D3AB98B301224253750D448B4BD997132400CEDD0A8432F775724F2D9821C72\"\n}";
        AccountHistoryResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<AccountHistoryResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est", _responseObject.Account);
            Assert.Equal("8D3AB98B301224253750D448B4BD997132400CEDD0A8432F775724F2D9821C72", _responseObject.Previous);
        }
    }
}