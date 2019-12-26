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
    public class ConfirmationActiveResponseTest
    {
        string _json = "{\n \"confirmations\": [\n   \"8031B600827C5CC05FDC911C28BBAC12A0E096CCB30FA8324F56C123676281B28031B600827C5CC05FDC911C28BBAC12A0E096CCB30FA8324F56C123676281B2\"\n ]\n}";
        ConfirmationActiveResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<ConfirmationActiveResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}