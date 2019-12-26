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
    public class RepresentativesOnlineResponseTest
    {
        string _json = "{\n  \"representatives\": [\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\",\n    \"nano_1111111111111111111111111111111111111111111111111awsq94gtecn\",\n    \"nano_114nk4rwjctu6n6tr6g6ps61g1w3hdpjxfas4xj1tq6i8jyomc5d858xr1xi\"\n  ]\n}";
        RepresentativesOnlineBasicResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<RepresentativesOnlineBasicResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class RepresentativesOnlineResponseTest1
    {
        string _json = "{\n  \"representatives\" : {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": \"\",\n    \"nano_1111111111111111111111111111111111111111111111111awsq94gtecn\": \"\",\n    \"nano_114nk4rwjctu6n6tr6g6ps61g1w3hdpjxfas4xj1tq6i8jyomc5d858xr1xi\": \"\"\n  }\n}";
        RepresentativesOnlineResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<RepresentativesOnlineResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }

    public class RepresentativesOnlineResponseTest2
    {
        string _json = "{\n  \"representatives\": {\n    \"nano_114nk4rwjctu6n6tr6g6ps61g1w3hdpjxfas4xj1tq6i8jyomc5d858xr1xi\": {\n      \"weight\": \"150462654614686936429917024683496890\"\n    }\n  }\n}";
        RepresentativesOnlineResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<RepresentativesOnlineResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}