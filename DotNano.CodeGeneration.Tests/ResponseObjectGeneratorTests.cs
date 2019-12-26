using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNano.RpcApi.Responses;
using DotNano.RpcDocParser;
using DotNano.Shared.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Xunit;

namespace DotNano.CodeGeneration.Tests
{

    public class ResponseObjectGeneratorTests_SimpleObject
    {
        private ResponseObjectGenerator _responseObjectGenerator;
        private ClassDeclarationSyntax _responseObject;

        public ResponseObjectGeneratorTests_SimpleObject()
        {
            var simpleObject = new SimpleJson();
            simpleObject.Fields.Add("balance", typeof(long));
            simpleObject.Fields.Add("pending", typeof(long));

            _responseObjectGenerator = new ResponseObjectGenerator();
            _responseObject = (ClassDeclarationSyntax)_responseObjectGenerator.Generate("AccountBalance", simpleObject).Members.First();
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectName()
        {
            Assert.Equal("AccountBalanceResponse", _responseObject.Identifier.ValueText);
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectProperties()
        {
            var properties = _responseObject.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();

            var balanceProperty = properties.Single(x => x.Identifier.ValueText.Equals("Balance"));
            Assert.Equal("Balance", balanceProperty.Identifier.ValueText);
            Assert.Equal("Int64", balanceProperty.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)balanceProperty.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, balanceProperty.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, balanceProperty.AccessorList.Accessors.Last().Kind());

            var pendingProperty = properties.Single(x => x.Identifier.ValueText.Equals("Pending"));
            Assert.Equal("Pending", pendingProperty.Identifier.ValueText);
            Assert.Equal("Int64", pendingProperty.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)pendingProperty.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, pendingProperty.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, pendingProperty.AccessorList.Accessors.Last().Kind());
        }
    }

    public class ResponseObjectGeneratorTests_NestedObject
    {
        const string JsonResponse = @"{
          ""blocks"": {
            ""nano_1111111111111111111111111111111111111111111111111117353trpda"": {
              ""142A538F36833D1CC78B94E11C766F75818F8B940771335C6C1B8AB880C5BB1D"": {
                ""amount"": ""6000000000000000000000000000000"",
                ""source"": ""nano_3dcfozsmekr1tr9skf1oa5wbgmxt81qepfdnt7zicq5x3hk65fg4fqj58mbr""
              }
            },
            ""nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3"": {
              ""4C1FEEF0BEA7F50BE35489A1233FE002B212DEA554B55B1B470D78BD8F210C74"": {
                ""amount"": ""106370018000000000000000000000000"",
                ""source"": ""nano_13ezf4od79h1tgj9aiu4djzcmmguendtjfuhwfukhuucboua8cpoihmh8byo""
              }
            }
          }
        }";

        private ResponseObjectGenerator _responseObjectGenerator;
        private ClassDeclarationSyntax _responseObject;
        private ClassDeclarationSyntax _innerClassDefinition;

        public ResponseObjectGeneratorTests_NestedObject()
        {
            var jsonAnalyzer = new JsonAnalyzer();
            var input = jsonAnalyzer.Convert(JsonResponse);

            _responseObjectGenerator = new ResponseObjectGenerator();

            var namespaceDefinition = _responseObjectGenerator.Generate("WalletPending", input);
            _responseObject = (ClassDeclarationSyntax)namespaceDefinition.Members.First();
            _innerClassDefinition = (ClassDeclarationSyntax)namespaceDefinition.Members.Skip(1).First();
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectName()
        {
            Assert.Equal("WalletPendingResponse", _responseObject.Identifier.ValueText);
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectProperties()
        {
            var properties = _responseObject.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();

            var blocksProperty = properties.Single(x => x.Identifier.ValueText.Equals("Blocks"));
            Assert.Equal("Blocks", blocksProperty.Identifier.ValueText);
            Assert.Equal("Dictionary<PublicAddress,Dictionary<HexKey64,WalletPendingBlock>>", blocksProperty.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)blocksProperty.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, blocksProperty.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, blocksProperty.AccessorList.Accessors.Last().Kind());
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateInnerClass()
        {
            Assert.Equal("WalletPendingBlock", _innerClassDefinition.Identifier.ValueText);
            
            var properties = _innerClassDefinition.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();
            Assert.Equal("Amount", properties.First().Identifier.ValueText);
            Assert.Equal("Source", properties.Skip(1).First().Identifier.ValueText);
        }    
    }

    public class ResponseObjectGeneratorTests_wallet_frontiers
    {
        const string JsonResponse = @"{
          ""frontiers"": {
            ""nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000"": ""000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F""
          }
        }";

        private ResponseObjectGenerator _responseObjectGenerator;
        private ClassDeclarationSyntax _responseObject;

        public ResponseObjectGeneratorTests_wallet_frontiers()
        {
            var jsonAnalyzer = new JsonAnalyzer();
            var input = jsonAnalyzer.Convert(JsonResponse);

            _responseObjectGenerator = new ResponseObjectGenerator();
            _responseObject = (ClassDeclarationSyntax)_responseObjectGenerator.Generate("WalletFrontiers", input).Members.First();
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectName()
        {
            Assert.Equal("WalletFrontiersResponse", _responseObject.Identifier.ValueText);
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectProperties()
        {
            var properties = _responseObject.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();

            var frontiersProperty = properties.Single(x => x.Identifier.ValueText.Equals("Frontiers"));
            Assert.Equal("Frontiers", frontiersProperty.Identifier.ValueText);
            Assert.Equal("Dictionary<PublicAddress,HexKey64>", frontiersProperty.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)frontiersProperty.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, frontiersProperty.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, frontiersProperty.AccessorList.Accessors.Last().Kind());
        }
    }

    public class ResponseObjectGeneratorTests_delegators
    {
        const string JsonResponse = @"{
  ""delegators"": {
    ""nano_13bqhi1cdqq8yb9szneoc38qk899d58i5rcrgdk5mkdm86hekpoez3zxw5sd"": ""500000000000000000000000000000000000"",
    ""nano_17k6ug685154an8gri9whhe5kb5z1mf5w6y39gokc1657sh95fegm8ht1zpn"": ""961647970820730000000000000000000000""
  }
}";

        private ResponseObjectGenerator _responseObjectGenerator;
        private ClassDeclarationSyntax _responseObject;

        public ResponseObjectGeneratorTests_delegators()
        {
            var jsonAnalyzer = new JsonAnalyzer();
            var input = jsonAnalyzer.Convert(JsonResponse);

            _responseObjectGenerator = new ResponseObjectGenerator();
            _responseObject = (ClassDeclarationSyntax)_responseObjectGenerator.Generate("Delegators", input).Members.First();
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectName()
        {
            Assert.Equal("DelegatorsResponse", _responseObject.Identifier.ValueText);
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectProperties()
        {
            var properties = _responseObject.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();

            var frontiersProperty = properties.Single(x => x.Identifier.ValueText.Equals("Delegators"));
            Assert.Equal("Delegators", frontiersProperty.Identifier.ValueText);
            Assert.Equal("Dictionary<PublicAddress,BigInteger>", frontiersProperty.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)frontiersProperty.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, frontiersProperty.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, frontiersProperty.AccessorList.Accessors.Last().Kind());
        }
    }


    public class ResponseObjectGeneratorTests_ledger
    {
        const string JsonResponse = @"{
  ""accounts"": {
    ""nano_11119gbh8hb4hj1duf7fdtfyf5s75okzxdgupgpgm1bj78ex3kgy7frt3s9n"": {
      ""frontier"": ""E71AF3E9DD86BBD8B4620EFA63E065B34D358CFC091ACB4E103B965F95783321"",
      ""open_block"": ""643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F"",
      ""representative_block"": ""643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F"",
      ""balance"": ""0"",
      ""modified_timestamp"": ""1511476234"",
      ""block_count"": ""2""
    }
  }
}";

        private ResponseObjectGenerator _responseObjectGenerator;
        private ClassDeclarationSyntax _responseObject;

        public ResponseObjectGeneratorTests_ledger()
        {
            var jsonAnalyzer = new JsonAnalyzer();
            var input = jsonAnalyzer.Convert(JsonResponse);

            _responseObjectGenerator = new ResponseObjectGenerator();
            _responseObject = (ClassDeclarationSyntax)_responseObjectGenerator.Generate("Ledger", input).Members.First();
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectName()
        {
            Assert.Equal("LedgerResponse", _responseObject.Identifier.ValueText);
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectProperties()
        {
            var properties = _responseObject.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();

            var accountsProperty = properties.Single(x => x.Identifier.ValueText.Equals("Accounts"));
            Assert.Equal("Accounts", accountsProperty.Identifier.ValueText);
            Assert.Equal("Dictionary<PublicAddress,LedgerAccount>", accountsProperty.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)accountsProperty.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, accountsProperty.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, accountsProperty.AccessorList.Accessors.Last().Kind());
        }
    }

    public class ResponseObjectGeneratorTests_ObjectContainingArrays
    {
        const string JsonResponse = @"{
  ""txn_tracking"": [
    {
      ""thread"": ""Blck processing"",
      ""time_held_open"": ""2"",
      ""write"": ""true"",
      ""stacktrace"": [
        {
          ""name"": ""nano::mdb_store::tx_begin_write"",
          ""address"": ""00007FF7142C5F86"",
          ""source_file"": ""c:\\users\\wesley\\documents\\raiblocks\\nano\\node\\lmdb.cpp"",
          ""source_line"": ""825""
        },
        {
          ""name"": ""nano::block_processor::process_batch"",
          ""address"": ""00007FF714121EEA"",
          ""source_file"": ""c:\\users\\wesley\\documents\\raiblocks\\nano\\node\\blockprocessor.cpp"",
          ""source_line"": ""243""
        },
        {
          ""name"": ""nano::block_processor::process_blocks"",
          ""address"": ""00007FF71411F8A6"",
          ""source_file"": ""c:\\users\\wesley\\documents\\raiblocks\\nano\\node\\blockprocessor.cpp"",
          ""source_line"": ""103""
        },
      ]
    }
  ]
}";

        private ResponseObjectGenerator _responseObjectGenerator;
        private ClassDeclarationSyntax _responseObject;
        private ClassDeclarationSyntax _innerClassDefinition1;
        private ClassDeclarationSyntax _innerClassDefinition2;

        public ResponseObjectGeneratorTests_ObjectContainingArrays()
        {
            var jsonAnalyzer = new JsonAnalyzer();
            var input = jsonAnalyzer.Convert(JsonResponse);

            _responseObjectGenerator = new ResponseObjectGenerator();

            var namespaceDefinition = _responseObjectGenerator.Generate("DatabaseTxnTracker", input);
            _responseObject = (ClassDeclarationSyntax)namespaceDefinition.Members.First();
            _innerClassDefinition1 = (ClassDeclarationSyntax)namespaceDefinition.Members.Skip(1).First();
            _innerClassDefinition2 = (ClassDeclarationSyntax)namespaceDefinition.Members.Skip(2).First();
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectName()
        {
            Assert.Equal("DatabaseTxnTrackerResponse", _responseObject.Identifier.ValueText);
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectProperties()
        {
            var properties = _responseObject.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();

            var property = properties.Single(x => x.Identifier.ValueText.Equals("TxnTracking"));
            Assert.Equal("TxnTracking", property.Identifier.ValueText);
            Assert.Equal("IEnumerable<DatabaseTxnTrackerTxnTracking>", property.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)property.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, property.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, property.AccessorList.Accessors.Last().Kind());
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateInnerClassWithStackTraceArray()
        {
            Assert.Equal("DatabaseTxnTrackerTxnTracking", _innerClassDefinition2.Identifier.ValueText);

            var properties = _innerClassDefinition2.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();
            var stackTraceProperty = properties.Single(x => x.Identifier.ValueText == "Stacktrace");
            Assert.Equal("IEnumerable<DatabaseTxnTrackerTxnTrackingStacktrace>", stackTraceProperty.Type.GetText().ToString());
        }
        
        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateInnerStackTraceClass()
        {
            Assert.Equal("DatabaseTxnTrackerTxnTrackingStacktrace", _innerClassDefinition1.Identifier.ValueText);

            var properties = _innerClassDefinition1.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();
            Assert.Equal("Name", properties.First().Identifier.ValueText);
            Assert.Equal("Address", properties.Skip(1).First().Identifier.ValueText);
            Assert.Equal("SourceFile", properties.Skip(2).First().Identifier.ValueText);
            Assert.Equal("SourceLine", properties.Skip(3).First().Identifier.ValueText);
        }
    }


    public class ResponseObjectGeneratorTests_peers
    {
        const string JsonResponse = @"{
  ""peers"": {
    ""[::ffff:172.17.0.1]:32841"": {
      ""protocol_version"": ""16"",
      ""node_id"": ""node_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3"",
      ""type"": ""udp""
    }
  }
}";

        private ResponseObjectGenerator _responseObjectGenerator;
        private ClassDeclarationSyntax _responseObject;

        public ResponseObjectGeneratorTests_peers()
        {
            var jsonAnalyzer = new JsonAnalyzer();
            var input = jsonAnalyzer.Convert(JsonResponse);

            _responseObjectGenerator = new ResponseObjectGenerator();
            _responseObject = (ClassDeclarationSyntax)_responseObjectGenerator.Generate("Peers", input).Members.First();
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectName()
        {
            Assert.Equal("PeersResponse", _responseObject.Identifier.ValueText);
        }

        [Fact]
        public void Generate_SimpleResponse_ShouldGenerateClassWithCorrectProperties()
        {
            var properties = _responseObject.Members.Where(x => x.Kind() == SyntaxKind.PropertyDeclaration).Cast<PropertyDeclarationSyntax>();

            var accountsProperty = properties.Single(x => x.Identifier.ValueText.Equals("Peers"));
            Assert.Equal("Peers", accountsProperty.Identifier.ValueText);
            Assert.Equal("Dictionary<PeerAddress,PeersPeer>", accountsProperty.Type.GetText().ToString());
            Assert.Equal(SyntaxKind.PublicKeyword, (SyntaxKind)accountsProperty.Modifiers.Single().RawKind);
            Assert.Equal(SyntaxKind.GetAccessorDeclaration, accountsProperty.AccessorList.Accessors.First().Kind());
            Assert.Equal(SyntaxKind.SetAccessorDeclaration, accountsProperty.AccessorList.Accessors.Last().Kind());
        }
    }
}
