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
    public class PeersResponseTest
    {
        string _json = "{\n  \"peers\": {\n    \"[::ffff:172.17.0.1]:32841\": \"16\"\n  }\n}";
        PeersResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<PeersResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
    
    public class PeersResponseTest2
    {
        string _json = "{\n  \"peers\": {\n    \"[::ffff:172.17.0.1]:32841\": {\n      \"protocol_version\": \"16\",\n      \"node_id\": \"node_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3\",\n      \"type\": \"udp\"\n    }\n  }\n}";
        PeersResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<PeersResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}