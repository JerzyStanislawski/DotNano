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
    public class VersionResponseTest
    {
        string _json = "{\n  \"rpc_version\": \"1\",\n  \"store_version\": \"14\",\n  \"protocol_version\": \"17\",\n  \"node_vendor\": \"Nano 20.0\",\n  \"store_vendor\": \"LMDB 0.9.23\", // since V21.0\n  \"network\": \"live\", // since v20.0\n  \"network_identifier\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\", // since v20.0\n  \"build_info\": \"Build Info <git hash> \\\"<compiler> version \\\" \\\"<compiler version string>\\\" \\\"BOOST <boost version>\\\" BUILT \\\"<build date>\\\"\" // since v20.0\n}";
        VersionResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<VersionResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(1, _responseObject.RpcVersion);
            Assert.Equal(14, _responseObject.StoreVersion);
            Assert.Equal(17, _responseObject.ProtocolVersion);
            Assert.Equal("Nano 20.0", _responseObject.NodeVendor);
            Assert.Equal("LMDB 0.9.23", _responseObject.StoreVendor);
            Assert.Equal("live", _responseObject.Network);
            Assert.Equal("991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948", _responseObject.NetworkIdentifier);
            Assert.Equal("Build Info <git hash> \"<compiler> version \" \"<compiler version string>\" \"BOOST <boost version>\" BUILT \"<build date>\"", _responseObject.BuildInfo);
        }
    }
}