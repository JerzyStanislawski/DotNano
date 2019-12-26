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
    public class DatabaseTxnTrackerResponseTest
    {
        string _json = "{\n  \"txn_tracking\": [\n    {\n      \"thread\": \"Blck processing\",  // Which thread held the transaction\n      \"time_held_open\": \"2\",        // Seconds the transaction has currently been held open for\n      \"write\": \"true\",              // If true it is a write lock, otherwise false.\n      \"stacktrace\": [\n        {\n          \"name\": \"nano::mdb_store::tx_begin_write\",\n          \"address\": \"00007FF7142C5F86\",\n          \"source_file\": \"c:\\\\users\\\\wesley\\\\documents\\\\raiblocks\\\\nano\\\\node\\\\lmdb.cpp\",\n          \"source_line\": \"825\"\n        },\n        {\n          \"name\": \"nano::block_processor::process_batch\",\n          \"address\": \"00007FF714121EEA\",\n          \"source_file\": \"c:\\\\users\\\\wesley\\\\documents\\\\raiblocks\\\\nano\\\\node\\\\blockprocessor.cpp\",\n          \"source_line\": \"243\"\n        },\n        {\n          \"name\": \"nano::block_processor::process_blocks\",\n          \"address\": \"00007FF71411F8A6\",\n          \"source_file\": \"c:\\\\users\\\\wesley\\\\documents\\\\raiblocks\\\\nano\\\\node\\\\blockprocessor.cpp\",\n          \"source_line\": \"103\"\n        },\n        \n      ]\n    }\n     // other threads\n  ]\n}";
        DatabaseTxnTrackerResponse _responseObject;
        private void CreateResponseObject()
        {
            _responseObject = JsonConvert.DeserializeObject<DatabaseTxnTrackerResponse>(_json, JsonSerializationSettings.PascalCaseSettings);
        }

        [Fact]
        public void ShouldProperlyConvertJsonToResponseObject()
        {
            CreateResponseObject();
        }
    }
}