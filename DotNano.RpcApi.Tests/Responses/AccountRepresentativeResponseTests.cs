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
    public class AccountRepresentativeResponseTest
    {
        string _json = "{\n  \"representative\" : \"nano_16u1uufyoig8777y6r8iqjtrw8sg8maqrm36zzcm95jmbd9i9aj5i8abr8u5\"\n}";
        AccountRepresentativeResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<AccountRepresentativeResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("nano_16u1uufyoig8777y6r8iqjtrw8sg8maqrm36zzcm95jmbd9i9aj5i8abr8u5", _responseObject.Representative);
        }
    }
}