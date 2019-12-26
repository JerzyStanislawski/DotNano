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
    public class BootstrapStatusResponseTest
    {
        string _json = "{\n  \"clients\": \"0\",\n  \"pulls\": \"0\",\n  \"pulling\": \"0\",\n  \"connections\": \"31\",\n  \"idle\": \"31\",\n  \"target_connections\": \"16\",\n  \"total_blocks\": \"13558\",\n  \"runs_count\": \"0\",\n  \"requeued_pulls\": \"31\",\n  \"frontiers_received\": \"true\",\n  \"frontiers_confirmed\": \"false\",\n  \"mode\": \"legacy\",\n  \"lazy_blocks\": \"0\",\n  \"lazy_state_backlog\": \"0\",\n  \"lazy_balances\": \"0\",\n  \"lazy_destinations\": \"0\",\n  \"lazy_undefined_links\": \"0\",\n  \"lazy_pulls\": \"32\",\n  \"lazy_keys\": \"32\",\n  \"lazy_key_1\": \"36897874BDA3028DC8544C106BE1394891F23DDDF84DE100FED450F6FBC8122C\",\n  \"duration\": \"29\"\n}";
        BootstrapStatusResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<BootstrapStatusResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(0, _responseObject.Clients);
            Assert.Equal(0, _responseObject.Pulls);
            Assert.Equal(0, _responseObject.Pulling);
            Assert.Equal(31, _responseObject.Connections);
            Assert.Equal(31, _responseObject.Idle);
            Assert.Equal(16, _responseObject.TargetConnections);
            Assert.Equal(13558, _responseObject.TotalBlocks);
            Assert.Equal(0, _responseObject.RunsCount);
            Assert.Equal(31, _responseObject.RequeuedPulls);
            Assert.Equal(true, _responseObject.FrontiersReceived);
            Assert.Equal(false, _responseObject.FrontiersConfirmed);
            Assert.Equal("legacy", _responseObject.Mode);
            Assert.Equal(0, _responseObject.LazyBlocks);
            Assert.Equal(0, _responseObject.LazyStateBacklog);
            Assert.Equal(0, _responseObject.LazyBalances);
            Assert.Equal(0, _responseObject.LazyDestinations);
            Assert.Equal(0, _responseObject.LazyUndefinedLinks);
            Assert.Equal(32, _responseObject.LazyPulls);
            Assert.Equal(32, _responseObject.LazyKeys);
            Assert.Equal("36897874BDA3028DC8544C106BE1394891F23DDDF84DE100FED450F6FBC8122C", _responseObject.LazyKey1);
            Assert.Equal(29, _responseObject.Duration);
        }
    }
}