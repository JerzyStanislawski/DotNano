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
    public class RepresentativesResponseTest
    {
        string _json = "{\n  \"representatives\": {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": \"3822372327060170000000000000000000000\",\n    \"nano_1111111111111111111111111111111111111111111111111awsq94gtecn\": \"30999999999999999999999999000000\",\n    \"nano_114nk4rwjctu6n6tr6g6ps61g1w3hdpjxfas4xj1tq6i8jyomc5d858xr1xi\": \"0\"\n  }\n}";
        RepresentativesResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<RepresentativesResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}