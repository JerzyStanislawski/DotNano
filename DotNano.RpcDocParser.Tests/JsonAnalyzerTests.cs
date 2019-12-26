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
    public class JsonAnalyzerTests
    {
        private readonly JsonAnalyzer _jsonAnalyzer;
        private readonly SimpleJson _result;
        private readonly string _json = @"
        {
          ""announcements"": ""5"",
          ""last_winner"": ""B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F"",
          ""total_tally"": ""51145880360792646375807054724596663794"",
          ""blocks"": {
            ""B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F"": {
              ""tally"": ""51145880360792646375807054724596663794"",
              ""contents"": {
                ""type"": ""state"",
                ""account"": ""nano_3fihmbtuod33s4nrbqfczhk9zy9ddqimwjshzg4c3857es8c9631i5rg6h9p"",
                ""previous"": ""EE125B1B1D85D3C24636B3590E1642D9F21B166C0C6CD99C9C6087A1224A0C44"",
                ""representative"": ""nano_3o7uzba8b9e1wqu5ziwpruteyrs3scyqr761x7ke6w1xctohxfh5du75qgaj"",
                ""balance"": ""218195000000000000000000000000"",
                ""link"": ""0000000000000000000000000000000000000000000000000000000000000000"",
                ""link_as_account"": ""nano_1111111111111111111111111111111111111111111111111111hifc8npp"",
                ""signature"": ""B1BD285235C612C5A141FA61793D7C6C762D3F104A85102DED5FBD6B4514971C4D044ACD3EC8C06A9495D8E83B6941B54F8DABA825ADF799412ED9E2C86D7A0C"",
                ""work"": ""05bb28cd8acbe71d""
              },
              ""representatives"": {
                ""nano_3pczxuorp48td8645bs3m6c3xotxd3idskrenmi65rbrga5zmkemzhwkaznh"": ""12617828599372664613607727105312358589"",
                ""nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou"": ""5953738757270291536911559258663615240"",
                ""nano_3i4n5n6c6xssapbdtkdoutm88c5zjmatc5tc77xyzdkpef8akid9errcpjnx"": ""0""
              }
            }
          }
        }";

        public JsonAnalyzerTests()
        {
            _jsonAnalyzer = new JsonAnalyzer();

            _result = _jsonAnalyzer.Convert(_json);
        }

        [Fact]
        public void Convert_Json_ShouldReturnDictionaryOfFields()
        {
            Assert.Equal(3, _result.Fields.Count);
        }

        [Fact]
        public void Convert_Json_ShouldReturnProperFields()
        {
            Assert.Equal(typeof(long), _result.Fields["announcements"]);
            Assert.Equal(typeof(HexKey64), _result.Fields["last_winner"]);
            Assert.Equal(typeof(BigInteger), _result.Fields["total_tally"]);
        }

        [Fact]
        public void Convert_Json_ShouldConvertNestedObjects()
        {
            var blocks = _result.InnerJsons["blocks"];
            Assert.Empty(blocks.Fields);

            var block = blocks.InnerJsons.Single().Value;
            Assert.Equal(typeof(BigInteger), block.Fields["tally"]);

            var blockContent = block.Fields["contents"];
            Assert.Equal(typeof(BlockContent), blockContent);
            //Assert.Equal(typeof(string), blockContent.Type);
            //Assert.Equal(typeof(PublicAddress), blockContent.Fields["account"]);
            //Assert.Equal(typeof(HexKey64), blockContent.Fields["previous"]);
            //Assert.Equal(typeof(PublicAddress), blockContent.Fields["representative"]);
            //Assert.Equal(typeof(BigInteger), blockContent.Fields["balance"]);
            //Assert.Equal(typeof(HexKey64), blockContent.Fields["link"]);
            //Assert.Equal(typeof(PublicAddress), blockContent.Fields["link_as_account"]);
            //Assert.Equal(typeof(HexKey128), blockContent.Fields["signature"]);
            //Assert.Equal(typeof(string), blockContent.Fields["work"]);

            var reps = block.InnerJsons["representatives"];
            Assert.Equal(1, reps.Fields.Count);
        }
    }

    public class JsonAnalyzerTests_JsonInput_WithArray
    {
        private readonly JsonAnalyzer _jsonAnalyzer;
        private readonly SimpleJson _result;
        private readonly string _json = @"{
                                              ""action"": ""blocks_info"",
                                              ""json_block"": ""true"",
                                              ""hashes"": [""87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9""]
                                          }";

        public JsonAnalyzerTests_JsonInput_WithArray()
        {
            _jsonAnalyzer = new JsonAnalyzer();

            _result = _jsonAnalyzer.Convert(_json);
        }

        [Fact]
        public void Convert_Json_ShouldReturnDictionaryOfFields()
        {
            Assert.Equal(2, _result.Fields.Count);
        }

        [Fact]
        public void Convert_Json_ShouldReturnProperFields()
        {
            Assert.Equal(typeof(bool), _result.Fields["json_block"]);
            Assert.Equal(typeof(IEnumerable<HexKey64>), _result.Fields["hashes"]);
        }

    }
}
