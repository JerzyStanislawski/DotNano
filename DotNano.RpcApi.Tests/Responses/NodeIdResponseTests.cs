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
    public class NodeIdResponseTest
    {
        string _json = "{\n  \"private\": \"2AD75C9DC20EA497E41722290C4DC966ECC4D6C75CAA4E447961F918FD73D8C7\",\n  \"public\": \"78B11E1777B8E7DF9090004376C3EDE008E84680A497C0805F68CA5928626E1C\",\n  \"as_account\": \"nano_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3\",\n  \"node_id\": \"node_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3\"\n}";
        NodeIdResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<NodeIdResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal("2AD75C9DC20EA497E41722290C4DC966ECC4D6C75CAA4E447961F918FD73D8C7", _responseObject.Private);
            Assert.Equal("78B11E1777B8E7DF9090004376C3EDE008E84680A497C0805F68CA5928626E1C", _responseObject.Public);
            Assert.Equal("nano_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3", _responseObject.AsAccount);
            Assert.Equal("node_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3", _responseObject.NodeId);
        }
    }
}