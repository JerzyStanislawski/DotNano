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
    public class RepublishResponseTest
    {
        string _json = "{\n  \"success\": \"\",\n  \"blocks\": [\n    \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n    \"A170D51B94E00371ACE76E35AC81DC9405D5D04D4CEBC399AEACE07AE05DD293\"\n  ]\n}";
        RepublishResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<RepublishResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("", _responseObject.Success);
        }
    }

    public class RepublishResponseTest1
    {
        string _json = "{\n  \"blocks\": [\n    \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n    \"A170D51B94E00371ACE76E35AC81DC9405D5D04D4CEBC399AEACE07AE05DD293\",\n    \"90D0C16AC92DD35814E84BFBCC739A039615D0A42A76EF44ADAEF1D99E9F8A35\"\n  ]\n}";
        RepublishResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<RepublishResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class RepublishResponseTest2
    {
        string _json = "{\n  \"blocks\": [\n    \"A170D51B94E00371ACE76E35AC81DC9405D5D04D4CEBC399AEACE07AE05DD293\",\n    \"90D0C16AC92DD35814E84BFBCC739A039615D0A42A76EF44ADAEF1D99E9F8A35\",\n    \"18563C814A54535B7C12BF76A0E23291BA3769536634AB90AD0305776A533E8E\"\n  ]\n}";
        RepublishResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<RepublishResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}