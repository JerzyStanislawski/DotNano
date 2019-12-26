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
    public class ConfirmationQuorumResponseTest
    {
        string _json = "{\n  \"quorum_delta\": \"41469707173777717318245825935516662250\",\n  \"online_weight_quorum_percent\": \"50\",\n  \"online_weight_minimum\": \"60000000000000000000000000000000000000\",\n  \"online_stake_total\": \"82939414347555434636491651871033324568\",\n  \"peers_stake_total\": \"69026910610720098597176027400951402360\",\n  \"peers_stake_required\": \"60000000000000000000000000000000000000\"\n}";
        ConfirmationQuorumResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ConfirmationQuorumResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
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
            Assert.Equal(BigInteger.Parse("41469707173777717318245825935516662250"), _responseObject.QuorumDelta);
            Assert.Equal(50, _responseObject.OnlineWeightQuorumPercent);
            Assert.Equal(BigInteger.Parse("60000000000000000000000000000000000000"), _responseObject.OnlineWeightMinimum);
            Assert.Equal(BigInteger.Parse("82939414347555434636491651871033324568"), _responseObject.OnlineStakeTotal);
            Assert.Equal(BigInteger.Parse("69026910610720098597176027400951402360"), _responseObject.PeersStakeTotal);
            Assert.Equal(BigInteger.Parse("60000000000000000000000000000000000000"), _responseObject.PeersStakeRequired);
        }
    }
}