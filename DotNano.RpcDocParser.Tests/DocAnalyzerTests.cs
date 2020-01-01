using DotNano.Shared.DataTypes;
using DotNano.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Xunit;

namespace DotNano.RpcDocParser.Tests
{
    public class DocAnalyzerTests_Simple_Call
    {
        private DocAnalyzer _docAnalyzer;
        private RpcCallDocDefinition _rpcCallDocDefinition;
        private IEnumerable<RpcCallCodeDefinition> _result;

        public DocAnalyzerTests_Simple_Call()
        {
            _docAnalyzer = new DocAnalyzer(new JsonAnalyzer());

            _rpcCallDocDefinition = new RpcCallDocDefinition("some_method_name");
            _rpcCallDocDefinition.JsonRequests.Add(@"{
                                                      ""action"": ""account_block_count"",
                                                      ""account"": ""nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3""
                                                    }");
            _rpcCallDocDefinition.JsonResponses.Add(@"{
                                                      ""block_count"" : ""19""
                                                    }");

            _result = _docAnalyzer.Analyze(new List<RpcCallDocDefinition> { _rpcCallDocDefinition });
        }

        [Fact]
        public void Analyze_RpcCall_ShouldReturnOneObject()
        {
            Assert.Single(_result);
        }

        [Fact]
        public void Analyze_RpcCall_ShouldAnalyzeMethodName()
        {
            var rpcCallCodeDefinition = _result.Single();
            Assert.Equal("some_method_name", rpcCallCodeDefinition.MethodName);
        }

        [Fact]
        public void Analyze_RpcCall_ShouldAnalyzeRequest()
        {
            var rpcCallCodeDefinition = _result.Single();
            Assert.Equal(typeof(PublicAddress), rpcCallCodeDefinition.RequiredParameters["account"]);
        }

        [Fact]
        public void Analyze_RpcCall_ShouldAnalyzeResponse()
        {
            var rpcCallCodeDefinition = _result.Single();
            Assert.Equal(typeof(long), rpcCallCodeDefinition.Response.Fields["block_count"]);
            Assert.Empty(rpcCallCodeDefinition.Response.InnerJsons);
        }
    }


    public class DocAnalyzerTests_Complex_Call
    {
        private DocAnalyzer _docAnalyzer;
        private RpcCallDocDefinition _rpcCallDocDefinition;
        private IEnumerable<RpcCallCodeDefinition> _result;

        public DocAnalyzerTests_Complex_Call()
        {
            _docAnalyzer = new DocAnalyzer(new JsonAnalyzer());

            _rpcCallDocDefinition = new RpcCallDocDefinition("some_method_name");
            _rpcCallDocDefinition.JsonRequests.Add(@"{
                                                      ""action"": ""blocks_info"",
                                                      ""json_block"": ""true"",
                                                      ""hashes"": [""87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9""]
                                                    }");
            _rpcCallDocDefinition.JsonRequests.Add(@"{
                                                      ""action"": ""blocks_info"",
                                                      ""hashes"": [""87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9""],
                                                      ""pending"": ""true"",
                                                      ""source"": ""true"",
                                                      ""balance"": ""true""
                                                    }");
            _rpcCallDocDefinition.JsonRequests.Add(@"{
                                                      ""action"": ""blocks_info"",
                                                      ""include_not_found"": ""true"",
                                                      ""hashes"": [""87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9"", ""0000000000000000000000000000000000000000000000000000000000000001""]
                                                    }");
            _rpcCallDocDefinition.OptionalParameters.Add("pending", "Booleans, false by default");
            _rpcCallDocDefinition.OptionalParameters.Add("source", "Booleans, false by default");
            _rpcCallDocDefinition.OptionalParameters.Add("balance", "Booleans, false by default");
            _rpcCallDocDefinition.OptionalParameters.Add("json_block", "Booleans, true by default");
            _rpcCallDocDefinition.OptionalParameters.Add("include_not_found", "Booleans, false by default");
            _rpcCallDocDefinition.JsonResponses.Add(@"{
                                                      ""blocks"": {
                                                        ""87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9"": {
                                                          ""block_account"": ""nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est"",
                                                          ""amount"": ""30000000000000000000000000000000000"",
                                                          ""balance"": ""5606157000000000000000000000000000000"",
                                                          ""height"": ""58"",
                                                          ""local_timestamp"": ""0"",
                                                          ""confirmed"": ""true"",
                                                          ""contents"": {
                                                                ""type"": ""state"",
                                                                ""account"": ""nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est"",
                                                                ""previous"": ""CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E"",
                                                                ""representative"": ""nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou"",
                                                                ""balance"": ""5606157000000000000000000000000000000"",
                                                                ""link"": ""5D1AA8A45F8736519D707FCB375976A7F9AF795091021D7E9C7548D6F45DD8D5"",
                                                                ""link_as_account"": ""nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z"",
                                                                ""signature"": ""82D41BC16F313E4B2243D14DFFA2FB04679C540C2095FEE7EAE0F2F26880AD56DD48D87A7CC5DD760C5B2D76EE2C205506AA557BF00B60D8DEE312EC7343A501"",
                                                                ""work"": ""8a142e07a10996d5""
                                                              },
                                                              ""subtype"": ""send""
                                                            }
                                                          }
                                                        }");
            _rpcCallDocDefinition.JsonResponses.Add(@"{
                                                        ""blocks"" : {
                                                            ""87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9"": {
                                                                ""block_account"": ""nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z"",
                                                                  ""amount"": ""30000000000000000000000000000000000"",
                                                                  ""contents"": {
                                                                   },
                                                                  ""pending"": ""0"",
                                                                  ""source_account"": ""nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est"",
                                                                  ""balance"": ""40200000001000000000000000000000000""
                                                                }
                                                            }
                                                        }");
            _rpcCallDocDefinition.JsonResponses.Add(@"{
                                                      ""blocks"" : {
                                                           ""87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9"": {
                                                                  ""block_account"": ""nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est"",
                                                                  ""amount"": ""30000000000000000000000000000000000"",
                                                                  ""balance"": ""5606157000000000000000000000000000000"",
                                                                  ""height"": ""58"",
                                                                  ""local_timestamp"": ""0"",
                                                                  ""confirmed"": ""false"",
                                                                  ""contents"": {
                                                                  }
                                                                }
                                                            },
                                                          ""blocks_not_found"": [
                                                            ""0000000000000000000000000000000000000000000000000000000000000001""
                                                          ]
                                                        }");

            _result = _docAnalyzer.Analyze(new List<RpcCallDocDefinition> { _rpcCallDocDefinition });
        }

        [Fact]
        public void Analyze_RpcCall_ShouldReturnOneObject()
        {
            Assert.Single(_result);
        }

        [Fact]
        public void Analyze_RpcCall_ShouldAnalyzeMethodName()
        {
            var rpcCallCodeDefinition = _result.Single();
            Assert.Equal("some_method_name", rpcCallCodeDefinition.MethodName);
        }

        [Fact]
        public void Analyze_RpcCall_ShouldAnalyzeRequest()
        {
            var rpcCallCodeDefinition = _result.Single();
            Assert.Equal(typeof(IEnumerable<HexKey64>), rpcCallCodeDefinition.RequiredParameters["hashes"]);
            Assert.Equal(typeof(bool), rpcCallCodeDefinition.OptionalParameters["pending"]);
            Assert.Equal(typeof(bool), rpcCallCodeDefinition.OptionalParameters["source"]);
            Assert.Equal(typeof(bool), rpcCallCodeDefinition.OptionalParameters["balance"]);
            Assert.Equal(typeof(bool), rpcCallCodeDefinition.OptionalParameters["json_block"]);
            Assert.Equal(typeof(bool), rpcCallCodeDefinition.OptionalParameters["include_not_found"]);
        }

        [Fact]
        public void Analyze_RpcCall_ShouldAnalyzeResponse()
        {
            var rpcCallCodeDefinition = _result.Single();
            Assert.Equal(typeof(IEnumerable<HexKey64>), rpcCallCodeDefinition.Response.Fields["blocks_not_found"]);

            Assert.Single(rpcCallCodeDefinition.Response.InnerJsons);
            var blocks = rpcCallCodeDefinition.Response.InnerJsons["blocks"];

            Assert.Single(blocks.InnerJsons);

            var block = blocks.InnerJsons[HexKey64.Default];
            Assert.Empty(block.InnerJsons);
            Assert.Equal(typeof(PublicAddress), block.Fields["block_account"]);
            Assert.Equal(typeof(BigInteger), block.Fields["amount"]);
            Assert.Equal(typeof(BigInteger), block.Fields["balance"]);
            Assert.Equal(typeof(long?), block.Fields["height"]);
            Assert.Equal(typeof(long?), block.Fields["local_timestamp"]);
            Assert.Equal(typeof(bool?), block.Fields["confirmed"]);
            Assert.Equal(typeof(string), block.Fields["subtype"]);
            Assert.Equal(typeof(BigInteger?), block.Fields["pending"]);
            Assert.Equal(typeof(PublicAddress), block.Fields["source_account"]);

            var blockContent = block.Fields["contents"];
            Assert.Equal(typeof(BlockContent), blockContent);
        }
    }
}
