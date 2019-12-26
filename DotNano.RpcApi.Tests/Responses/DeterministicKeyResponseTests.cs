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
    public class DeterministicKeyResponseTest
    {
        string _json = "{\n  \"private\": \"9F0E444C69F77A49BD0BE89DB92C38FE713E0963165CCA12FAF5712D7657120F\",\n  \"public\": \"C008B814A7D269A1FA3C6528B19201A24D797912DB9996FF02A1FF356E45552B\",\n  \"account\": \"nano_3i1aq1cchnmbn9x5rsbap8b15akfh7wj7pwskuzi7ahz8oq6cobd99d4r3b7\"\n}";
        DeterministicKeyResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<DeterministicKeyResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("9F0E444C69F77A49BD0BE89DB92C38FE713E0963165CCA12FAF5712D7657120F", _responseObject.Private);
            Assert.Equal("C008B814A7D269A1FA3C6528B19201A24D797912DB9996FF02A1FF356E45552B", _responseObject.Public);
            Assert.Equal("nano_3i1aq1cchnmbn9x5rsbap8b15akfh7wj7pwskuzi7ahz8oq6cobd99d4r3b7", _responseObject.Account);
        }
    }
}