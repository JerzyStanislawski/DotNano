using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNano.Shared.Model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Xunit;
using Assert = Xunit.Assert;

namespace DotNano.RpcDocParser.Tests
{
    public class RpcMdParserTests
    {
        private RpcMdParser _parser;
        private string _rpcDoc;
        private IEnumerable<RpcCallDocDefinition> _result;

        public RpcMdParserTests()
        {
            _parser = new RpcMdParser();
            _rpcDoc = File.ReadAllText("RpcDoc.txt");

            _result = _parser.Parse(_rpcDoc);
        }

        [Fact]
        public void Parse_RpcDoc_ShouldReturnRpcCalls()
        {
            Assert.NotEmpty(_result);
        }

        [Fact]
        public void Parse_RpcDoc_ShouldParseAllCommands()
        {
            var commandsFile = File.ReadAllText("AllRpcCommands.txt");
            var commands = commandsFile.Split("\r\n");

           // Assert.Equal(commands.Count(), _result.Count());
            CollectionAssert.AreEquivalent(commands, _result.Select(x => x.MethodName));
        }

        [Fact]
        public void Parse_RpcDoc_ShouldReturnJsonRequestsForAllCommands()
        {
            Assert.True(_result.All(x => x.JsonRequests.Any()));
        }

        [Fact]
        public void Parse_RpcDoc_ShouldReturnJsonResponsesForAllCommands()
        {
            Assert.True(_result.All(x => x.JsonResponses.Any()));
        }

        [Fact]
        public void Parse_RpcDoc_ShouldAllJsonRequestsBeValid()
        {
            foreach (var call in _result)
            {
                call.JsonRequests.All(x => JObject.Parse(x) != null);
            }
        }

        [Fact]
        public void Parse_RpcDoc_ShouldAllJsonResponsesBeValid()
        {
            foreach (var call in _result)
            {
                call.JsonResponses.All(x => JObject.Parse(x) != null);
            }
        }
    }
}
