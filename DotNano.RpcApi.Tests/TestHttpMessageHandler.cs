using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNano.RpcApi.Tests
{
    class TestHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var jobject = JObject.Parse(json);
            var action = jobject["action"].Value<string>();
            var jsonResponse = GetResponse(action);
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {Content = new StringContent(jsonResponse)});
        }

        private string GetResponse(string action)
        {
            if (action == "account_balance")
                return "{\n  \"balance\": \"10000\",\n  \"pending\": \"10000\"\n}";
            if (action == "account_block_count")
                return "{\n  \"block_count\" : \"19\"\n}";
            if (action == "account_get")
                return "{\n  \"account\" : \"nano_1e5aqegc1jb7qe964u4adzmcezyo6o146zb8hm6dft8tkp79za3sxwjym5rx\"\n}";
            if (action == "account_history")
                return "{\n  \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n  \"history\": [\n    {\n      \"type\": \"send\",\n      \"account\": \"nano_38ztgpejb7yrm7rr586nenkn597s3a1sqiy3m3uyqjicht7kzuhnihdk6zpz\",\n      \"amount\": \"80000000000000000000000000000000000\",\n      \"local_timestamp\": \"1551532723\",\n      \"height\": \"60\",\n      \"hash\": \"80392607E85E73CC3E94B4126F24488EBDFEB174944B890C97E8F36D89591DC5\"\n    }\n  ],\n  \"previous\": \"8D3AB98B301224253750D448B4BD997132400CEDD0A8432F775724F2D9821C72\"\n}";
            if (action == "account_info")
                return "{\n  \"frontier\": \"FF84533A571D953A596EA401FD41743AC85D04F406E76FDE4408EAED50B473C5\",\n  \"open_block\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n  \"representative_block\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n  \"balance\": \"235580100176034320859259343606608761791\",\n  \"modified_timestamp\": \"1501793775\",\n  \"block_count\": \"33\",\n  \"confirmation_height\" : \"28\",\n  \"account_version\": \"1\"\n}";
            if (action == "account_key")
                return "{\n  \"key\": \"3068BB1CA04525BB0E416C485FE6A67FD52540227D267CC8B6E8DA958A7FA039\"\n}";
            if (action == "account_representative")
                return "{\n  \"representative\" : \"nano_16u1uufyoig8777y6r8iqjtrw8sg8maqrm36zzcm95jmbd9i9aj5i8abr8u5\"\n}";
            if (action == "account_weight")
                return "{\n  \"weight\": \"10000\"\n}";
            if (action == "accounts_balances")
                return "{\n  \"balances\" : {\n    \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\":\n    {\n      \"balance\": \"10000\",\n      \"pending\": \"10000\"\n    },\n    \"nano_3i1aq1cchnmbn9x5rsbap8b15akfh7wj7pwskuzi7ahz8oq6cobd99d4r3b7\":\n    {\n      \"balance\": \"10000000\",\n      \"pending\": \"0\"\n    }\n  }\n}";
            if (action == "accounts_frontiers")
                return "{\n  \"frontiers\" : {\n    \"nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3\": \"791AF413173EEE674A6FCF633B5DFC0F3C33F397F0DA08E987D9E0741D40D81A\",\n    \"nano_3i1aq1cchnmbn9x5rsbap8b15akfh7wj7pwskuzi7ahz8oq6cobd99d4r3b7\": \"6A32397F4E95AF025DE29D9BF1ACE864D5404362258E06489FABDBA9DCCC046F\"\n  }\n}";
            if (action == "accounts_pending")
                return "{\n  \"blocks\" : {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": [\"142A538F36833D1CC78B94E11C766F75818F8B940771335C6C1B8AB880C5BB1D\"],\n    \"nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3\": [\"4C1FEEF0BEA7F50BE35489A1233FE002B212DEA554B55B1B470D78BD8F210C74\"]\n  }\n}";
            if (action == "active_difficulty")
                return "{\n  \"network_minimum\": \"ffffffc000000000\",\n  \"network_current\": \"ffffffcdbf40aa45\",\n  \"multiplier\": \"1.273557846739298\"\n}";
            if (action == "available_supply")
                return "{\n  \"available\": \"133248061996216572282917317807824970865\"\n}";
            if (action == "block_account")
                return "{\n  \"account\": \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\"\n}";
            if (action == "block_confirm")
                return "{\n  \"started\": \"1\"\n}";
            if (action == "block_count")
                return "{\n  \"count\": \"1000\",\n  \"unchecked\": \"10\",\n  \"cemented\": \"25\"\n}";
            if (action == "block_count_type")
                return "{\n  \"send\": \"5016664\",\n  \"receive\": \"4081228\",\n  \"open\": \"546457\",\n  \"change\": \"24193\",\n  \"state_v0\": \"4216537\",\n  \"state_v1\": \"10653709\",\n  \"state\": \"14870246\"\n}";
            if (action == "block_create")
                return "{\n  \"hash\": \"FF0144381CFF0B2C079A115E7ADA7E96F43FD219446E7524C48D1CC9900C4F17\",\n  \"block\": {\n    \"type\": \"state\",\n    \"account\": \"nano_3qgmh14nwztqw4wmcdzy4xpqeejey68chx6nciczwn9abji7ihhum9qtpmdr\",\n    \"previous\": \"F47B23107E5F34B2CE06F562B5C435DF72A533251CB414C51B2B62A8F63A00E4\",\n    \"representative\": \"nano_1hza3f7wiiqa7ig3jczyxj5yo86yegcmqk3criaz838j91sxcckpfhbhhra1\",\n    \"balance\": \"1000000000000000000000\",\n    \"link\": \"19D3D919475DEED4696B5D13018151D1AF88B2BD3BCFF048B45031C1F36D1858\",\n    \"link_as_account\": \"nano_18gmu6engqhgtjnppqam181o5nfhj4sdtgyhy36dan3jr9spt84rzwmktafc\",\n    \"signature\": \"3BFBA64A775550E6D49DF1EB8EEC2136DCD74F090E2ED658FBD9E80F17CB1C9F9F7BDE2B93D95558EC2F277FFF15FD11E6E2162A1714731B743D1E941FA4560A\",\n    \"work\": \"cab7404f0b5449d0\"\n  }\n}";
            if (action == "block_hash")
                return "{\n  \"hash\": \"FF0144381CFF0B2C079A115E7ADA7E96F43FD219446E7524C48D1CC9900C4F17\"\n}";
            if (action == "block_info")
                return "{\n  \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n  \"amount\": \"30000000000000000000000000000000000\",\n  \"balance\": \"5606157000000000000000000000000000000\",\n  \"height\": \"58\",\n  \"local_timestamp\": \"0\",\n  \"confirmed\": \"true\",\n  \"contents\": {\n    \"type\": \"state\",\n    \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n    \"previous\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n    \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n    \"balance\": \"5606157000000000000000000000000000000\",\n    \"link\": \"5D1AA8A45F8736519D707FCB375976A7F9AF795091021D7E9C7548D6F45DD8D5\",\n    \"link_as_account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n    \"signature\": \"82D41BC16F313E4B2243D14DFFA2FB04679C540C2095FEE7EAE0F2F26880AD56DD48D87A7CC5DD760C5B2D76EE2C205506AA557BF00B60D8DEE312EC7343A501\",\n    \"work\": \"8a142e07a10996d5\"\n  },\n  \"subtype\": \"send\"\n}";
            if (action == "blocks")
                return "{\n  \"blocks\": {\n    \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\": {\n      \"type\": \"state\",\n      \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"previous\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n      \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n      \"balance\": \"5606157000000000000000000000000000000\",\n      \"link\": \"5D1AA8A45F8736519D707FCB375976A7F9AF795091021D7E9C7548D6F45DD8D5\",\n      \"link_as_account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n      \"signature\": \"82D41BC16F313E4B2243D14DFFA2FB04679C540C2095FEE7EAE0F2F26880AD56DD48D87A7CC5DD760C5B2D76EE2C205506AA557BF00B60D8DEE312EC7343A501\",\n      \"work\": \"8a142e07a10996d5\"\n    }\n  }\n}";
            if (action == "blocks_info")
                return "{\n  \"blocks\": {\n    \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\": {\n      \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"amount\": \"30000000000000000000000000000000000\",\n      \"balance\": \"5606157000000000000000000000000000000\",\n      \"height\": \"58\",\n      \"local_timestamp\": \"0\",\n      \"confirmed\": \"true\",\n      \"contents\": {\n        \"type\": \"state\",\n        \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n        \"previous\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n        \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n        \"balance\": \"5606157000000000000000000000000000000\",\n        \"link\": \"5D1AA8A45F8736519D707FCB375976A7F9AF795091021D7E9C7548D6F45DD8D5\",\n        \"link_as_account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n        \"signature\": \"82D41BC16F313E4B2243D14DFFA2FB04679C540C2095FEE7EAE0F2F26880AD56DD48D87A7CC5DD760C5B2D76EE2C205506AA557BF00B60D8DEE312EC7343A501\",\n        \"work\": \"8a142e07a10996d5\"\n      },\n      \"subtype\": \"send\"\n    }\n  }\n}";
            if (action == "bootstrap")
                return "{\n  \"success\": \"\"\n}";
            if (action == "bootstrap_any")
                return "{\n  \"success\": \"\"\n}";
            if (action == "bootstrap_lazy")
                return "{\n  \"started\": \"1\"\n}";
            if (action == "bootstrap_status")
                return "{\n  \"clients\": \"0\",\n  \"pulls\": \"0\",\n  \"pulling\": \"0\",\n  \"connections\": \"31\",\n  \"idle\": \"31\",\n  \"target_connections\": \"16\",\n  \"total_blocks\": \"13558\",\n  \"runs_count\": \"0\",\n  \"requeued_pulls\": \"31\",\n  \"frontiers_received\": \"true\",\n  \"frontiers_confirmed\": \"false\",\n  \"mode\": \"legacy\",\n  \"lazy_blocks\": \"0\",\n  \"lazy_state_backlog\": \"0\",\n  \"lazy_balances\": \"0\",\n  \"lazy_destinations\": \"0\",\n  \"lazy_undefined_links\": \"0\",\n  \"lazy_pulls\": \"32\",\n  \"lazy_keys\": \"32\",\n  \"lazy_key_1\": \"36897874BDA3028DC8544C106BE1394891F23DDDF84DE100FED450F6FBC8122C\",\n  \"duration\": \"29\"\n}";
            if (action == "chain")
                return "{\n  \"blocks\": [\n    \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n  ]\n}";
            if (action == "confirmation_active")
                return "{\n \"confirmations\": [\n   \"8031B600827C5CC05FDC911C28BBAC12A0E096CCB30FA8324F56C123676281B28031B600827C5CC05FDC911C28BBAC12A0E096CCB30FA8324F56C123676281B2\"\n ]\n}";
            if (action == "confirmation_height_currently_processing")
                return "{\n  \"hash\": \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n}";
            if (action == "confirmation_history")
                return "{\n  \"confirmation_stats\": {\n    \"count\": \"2\",\n    \"average\": \"5000\"\n  },\n  \"confirmations\": [\n    {\n      \"hash\": \"EA70B32C55C193345D625F766EEA2FCA52D3F2CCE0B3A30838CC543026BB0FEA\",\n      \"duration\": \"4000\",\n      \"time\": \"1544819986\",\n      \"tally\": \"80394786589602980996311817874549318248\",\n      \"blocks\": \"1\", // since V21.0\n      \"voters\": \"37\", // since V21.0\n      \"request_count\": \"2\" // since V20.0\n    },\n    {\n      \"hash\": \"F2F8DA6D2CA0A4D78EB043A7A29E12BDE5B4CE7DE1B99A93A5210428EE5B8667\",\n      \"duration\": \"6000\",\n      \"time\": \"1544819988\",\n      \"tally\": \"68921714529890443063672782079965877749\",\n      \"blocks\": \"1\", // since V21.0\n      \"voters\": \"64\", // since V21.0\n      \"request_count\": \"7\" // since V20.0\n    }\n  ]\n}";
            if (action == "confirmation_info")
                return "{\n  \"announcements\": \"2\",\n  \"voters\": \"29\",\n  \"last_winner\": \"B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F\",\n  \"total_tally\": \"51145880360832646375807054724596663794\",\n  \"blocks\": {\n    \"B94C505029F04BC057A0486ADA8BD07981B4A8736AE6581F2E98C6D18498146F\": {\n      \"tally\": \"51145880360832646375807054724596663794\",\n      \"contents\": {\n        \"type\": \"state\",\n        \"account\": \"nano_3fihmbtuod33s4nrbqfczhk9zy9ddqimwjshzg4c3857es8c9631i5rg6h9p\",\n        \"previous\": \"EE125B1B1D85D3C24636B3590E1642D9F21B166C0C6CD99C9C6087A1224A0C44\",\n        \"representative\": \"nano_3o7uzba8b9e1wqu5ziwpruteyrs3scyqr761x7ke6w1xctohxfh5du75qgaj\",\n        \"balance\": \"218195000000000000000000000000\",\n        \"link\": \"0000000000000000000000000000000000000000000000000000000000000000\",\n        \"link_as_account\": \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\",\n        \"signature\": \"B1BD285235C612C5A141FA61793D7C6C762D3F104A85102DED5FBD6B4514971C4D044ACD3EC8C06A9495D8E83B6941B54F8DABA825ADF799412ED9E2C86D7A0C\",\n        \"work\": \"05bb28cd8acbe71d\"\n      }\n    }\n  }\n}";
            if (action == "confirmation_quorum")
                return "{\n  \"quorum_delta\": \"41469707173777717318245825935516662250\",\n  \"online_weight_quorum_percent\": \"50\",\n  \"online_weight_minimum\": \"60000000000000000000000000000000000000\",\n  \"online_stake_total\": \"82939414347555434636491651871033324568\",\n  \"peers_stake_total\": \"69026910610720098597176027400951402360\",\n  \"peers_stake_required\": \"60000000000000000000000000000000000000\"\n}";
            if (action == "database_txn_tracker")
                return "{\n  \"txn_tracking\": [\n    {\n      \"thread\": \"Blck processing\",  // Which thread held the transaction\n      \"time_held_open\": \"2\",        // Seconds the transaction has currently been held open for\n      \"write\": \"true\",              // If true it is a write lock, otherwise false.\n      \"stacktrace\": [\n        {\n          \"name\": \"nano::mdb_store::tx_begin_write\",\n          \"address\": \"00007FF7142C5F86\",\n          \"source_file\": \"c:\\\\users\\\\wesley\\\\documents\\\\raiblocks\\\\nano\\\\node\\\\lmdb.cpp\",\n          \"source_line\": \"825\"\n        },\n        {\n          \"name\": \"nano::block_processor::process_batch\",\n          \"address\": \"00007FF714121EEA\",\n          \"source_file\": \"c:\\\\users\\\\wesley\\\\documents\\\\raiblocks\\\\nano\\\\node\\\\blockprocessor.cpp\",\n          \"source_line\": \"243\"\n        },\n        {\n          \"name\": \"nano::block_processor::process_blocks\",\n          \"address\": \"00007FF71411F8A6\",\n          \"source_file\": \"c:\\\\users\\\\wesley\\\\documents\\\\raiblocks\\\\nano\\\\node\\\\blockprocessor.cpp\",\n          \"source_line\": \"103\"\n        },\n        \n      ]\n    }\n     // other threads\n  ]\n}";
            if (action == "delegators")
                return "{\n  \"delegators\": {\n    \"nano_13bqhi1cdqq8yb9szneoc38qk899d58i5rcrgdk5mkdm86hekpoez3zxw5sd\": \"500000000000000000000000000000000000\",\n    \"nano_17k6ug685154an8gri9whhe5kb5z1mf5w6y39gokc1657sh95fegm8ht1zpn\": \"961647970820730000000000000000000000\"\n  }\n}";
            if (action == "delegators_count")
                return "{\n  \"count\": \"2\"\n}";
            if (action == "deterministic_key")
                return "{\n  \"private\": \"9F0E444C69F77A49BD0BE89DB92C38FE713E0963165CCA12FAF5712D7657120F\",\n  \"public\": \"C008B814A7D269A1FA3C6528B19201A24D797912DB9996FF02A1FF356E45552B\",\n  \"account\": \"nano_3i1aq1cchnmbn9x5rsbap8b15akfh7wj7pwskuzi7ahz8oq6cobd99d4r3b7\"\n}";
            if (action == "epoch_upgrade")
                return "{\n  \"started\": \"1\"\n}";
            if (action == "frontier_count")
                return "{\n  \"count\": \"920471\"\n}";
            if (action == "frontiers")
                return "{\n  \"frontiers\" : {\n    \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\": \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n  }\n}";
            if (action == "keepalive")
                return "{\n  \"started\": \"1\"\n}";
            if (action == "key_create")
                return "{\n  \"private\": \"781186FB9EF17DB6E3D1056550D9FAE5D5BBADA6A6BC370E4CBB938B1DC71DA3\",\n  \"public\": \"3068BB1CA04525BB0E416C485FE6A67FD52540227D267CC8B6E8DA958A7FA039\",\n  \"account\": \"nano_1e5aqegc1jb7qe964u4adzmcezyo6o146zb8hm6dft8tkp79za3sxwjym5rx\"\n}";
            if (action == "key_expand")
                return "{\n  \"private\": \"781186FB9EF17DB6E3D1056550D9FAE5D5BBADA6A6BC370E4CBB938B1DC71DA3\",\n  \"public\": \"3068BB1CA04525BB0E416C485FE6A67FD52540227D267CC8B6E8DA958A7FA039\",\n  \"account\": \"nano_1e5aqegc1jb7qe964u4adzmcezyo6o146zb8hm6dft8tkp79za3sxwjym5rx\"\n}";
            if (action == "ledger")
                return "{\n  \"accounts\": {\n    \"nano_11119gbh8hb4hj1duf7fdtfyf5s75okzxdgupgpgm1bj78ex3kgy7frt3s9n\": {\n      \"frontier\": \"E71AF3E9DD86BBD8B4620EFA63E065B34D358CFC091ACB4E103B965F95783321\",\n      \"open_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"representative_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"balance\": \"0\",\n      \"modified_timestamp\": \"1511476234\",\n      \"block_count\": \"2\"\n    }\n  }\n}";
            if (action == "node_id")
                return "{\n  \"private\": \"2AD75C9DC20EA497E41722290C4DC966ECC4D6C75CAA4E447961F918FD73D8C7\",\n  \"public\": \"78B11E1777B8E7DF9090004376C3EDE008E84680A497C0805F68CA5928626E1C\",\n  \"as_account\": \"nano_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3\",\n  \"node_id\": \"node_1y7j5rdqhg99uyab1145gu3yur1ax35a3b6qr417yt8cd6n86uiw3d4whty3\"\n}";
            if (action == "node_id_delete")
                return "{\n  \"deprecated\": \"1\"\n}";
            if (action == "peers")
                return "{\n  \"peers\": {\n    \"[::ffff:172.17.0.1]:32841\": \"16\"\n  }\n}";
            if (action == "pending")
                return "{\n  \"blocks\": [ \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\" ]\n}";
            if (action == "pending_exists")
                return "{\n  \"exists\" : \"1\"\n}";
            if (action == "process")
                return "{\n  \"hash\": \"E2FB233EF4554077A7BF1AA85851D5BF0B36965D2B0FB504B2BC778AB89917D3\"\n}";
            if (action == "representatives")
                return "{\n  \"representatives\": {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": \"3822372327060170000000000000000000000\",\n    \"nano_1111111111111111111111111111111111111111111111111awsq94gtecn\": \"30999999999999999999999999000000\",\n    \"nano_114nk4rwjctu6n6tr6g6ps61g1w3hdpjxfas4xj1tq6i8jyomc5d858xr1xi\": \"0\"\n  }\n}";
            if (action == "representatives_online")
                return "{\n  \"representatives\": [\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\",\n    \"nano_1111111111111111111111111111111111111111111111111awsq94gtecn\",\n    \"nano_114nk4rwjctu6n6tr6g6ps61g1w3hdpjxfas4xj1tq6i8jyomc5d858xr1xi\"\n  ]\n}";
            if (action == "republish")
                return "{\n  \"success\": \"\",\n  \"blocks\": [\n    \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n    \"A170D51B94E00371ACE76E35AC81DC9405D5D04D4CEBC399AEACE07AE05DD293\"\n  ]\n}";
            if (action == "sign")
                return "{\n  \"signature\": \"2A71F3877033F5966735F260E906BFCB7FA82CDD543BCD1224F180F85A96FC26CB3F0E4180E662332A0DFE4EE6A0F798A71C401011E635604E532383EC08C70D\",\n  \"block\": {\n    \"type\": \"state\",\n    \"account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n    \"previous\": \"6CDDA48608C7843A0AC1122BDD46D9E20E21190986B19EAC23E7F33F2E6A6766\",\n    \"representative\": \"nano_3pczxuorp48td8645bs3m6c3xotxd3idskrenmi65rbrga5zmkemzhwkaznh\",\n    \"balance\": \"40200000001000000000000000000000000\",\n    \"link\": \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\",\n    \"link_as_account\": \"nano_33t5by1653nt196hfwm5q3wq7oxtaix97r7bhox5zn8eratrzoqsny49ftsd\",\n    \"signature\": \"2A71F3877033F5966735F260E906BFCB7FA82CDD543BCD1224F180F85A96FC26CB3F0E4180E662332A0DFE4EE6A0F798A71C401011E635604E532383EC08C70D\",\n    \"work\": \"000bc55b014e807d\"\n  }\n}";
            if (action == "stats")
                return "{\n  \"type\": \"counters\",\n  \"created\": \"2018.03.29 01:46:36\",\n  \"entries\": [\n    {\n      \"time\": \"01:46:36\",\n      \"type\": \"traffic_tcp\",\n      \"detail\": \"all\",\n      \"dir\": \"in\",\n      \"value\": \"3122792\"\n    },\n    {\n      \"time\": \"01:46:36\",\n      \"type\": \"traffic_tcp\",\n      \"detail\": \"all\",\n      \"dir\": \"out\",\n      \"value\": \"203184\"\n    }\n    \n  ]\n}";
            if (action == "stats_clear")
                return "{\n  \"success\": \"\"\n}";
            if (action == "stop")
                return "{\n  \"success\": \"\"\n}";
            if (action == "successors")
                return "{\n  \"blocks\" : [\n    \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\"\n  ]\n}";
            if (action == "validate_account_number")
                return "{\n  \"valid\" : \"1\"\n}";
            if (action == "version")
                return "{\n  \"rpc_version\": \"1\",\n  \"store_version\": \"14\",\n  \"protocol_version\": \"17\",\n  \"node_vendor\": \"Nano 20.0\",\n  \"store_vendor\": \"LMDB 0.9.23\", // since V21.0\n  \"network\": \"live\", // since v20.0\n  \"network_identifier\": \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\", // since v20.0\n  \"build_info\": \"Build Info <git hash> \\\"<compiler> version \\\" \\\"<compiler version string>\\\" \\\"BOOST <boost version>\\\" BUILT \\\"<build date>\\\"\" // since v20.0\n}";
            if (action == "unchecked")
                return "{\n  \"blocks\": {\n    \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\": {\n      \"type\": \"state\",\n      \"account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"previous\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n      \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n      \"balance\": \"5606157000000000000000000000000000000\",\n      \"link\": \"5D1AA8A45F8736519D707FCB375976A7F9AF795091021D7E9C7548D6F45DD8D5\",\n      \"link_as_account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n      \"signature\": \"82D41BC16F313E4B2243D14DFFA2FB04679C540C2095FEE7EAE0F2F26880AD56DD48D87A7CC5DD760C5B2D76EE2C205506AA557BF00B60D8DEE312EC7343A501\",\n      \"work\": \"8a142e07a10996d5\"\n    }\n  }\n}";
            if (action == "unchecked_clear")
                return "{\n    \"success\": \"\"\n}";
            if (action == "unchecked_get")
                return "{\n  \"modified_timestamp\": \"1565856525\",\n  \"contents\": {\n    \"type\": \"state\",\n    \"account\": \"nano_1hmqzugsmsn4jxtzo5yrm4rsysftkh9343363hctgrjch1984d8ey9zoyqex\",\n    \"previous\": \"009C587914611E83EE7F75BD9C000C430C720D0364D032E84F37678D7D012911\",\n    \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n    \"balance\": \"189012679592109992600249228\",\n    \"link\": \"0000000000000000000000000000000000000000000000000000000000000000\",\n    \"link_as_account\": \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\",\n    \"signature\": \"845C8660750895843C013CE33E31B80EF0A7A69E52DDAF74A5F1BDFAA9A52E4D9EA2C3BE1AB0BD5790FCC1AD9B7A3D2F4B44EECE4279A8184D414A30A1B4620F\",\n    \"work\": \"0dfb32653e189699\"\n  }\n}";
            if (action == "unchecked_keys")
                return "{\n  \"unchecked\": [\n    {\n      \"key\": \"19BF0C268C2D9AED1A8C02E40961B67EA56B1681DE274CD0C50F3DD972F0655C\",\n      \"hash\": \"A1A8558CBABD3F7C1D70F8CB882355F2EF688E7F30F5FDBD0204CAE157885056\",\n      \"modified_timestamp\": \"1565856744\",\n      \"contents\": {\n        \"type\": \"state\",\n        \"account\": \"nano_1hmqzugsmsn4jxtzo5yrm4rsysftkh9343363hctgrjch1984d8ey9zoyqex\",\n        \"previous\": \"19BF0C268C2D9AED1A8C02E40961B67EA56B1681DE274CD0C50F3DD972F0655C\",\n        \"representative\": \"nano_1stofnrxuz3cai7ze75o174bpm7scwj9jn3nxsn8ntzg784jf1gzn1jjdkou\",\n        \"balance\": \"189012679592109992600249226\",\n        \"link\": \"0000000000000000000000000000000000000000000000000000000000000000\",\n        \"link_as_account\": \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\",\n        \"signature\": \"FF5D49925AD3C8705E6EEDD993E8C4120E6107D7F1CB53B287773448DEA0B1D32918E67804248FC83609F0D93401D833DFA33127F21B6CD02F75D6E31A00450A\",\n        \"work\": \"8193ddf00947e694\"\n      }\n    }\n  ]\n}";
            if (action == "unopened")
                return "{\n  \"accounts\": {\n    \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\": \"207034077034226183413773082289554618448\"\n  }\n}";
            if (action == "uptime")
                return "{\n    \"seconds\": \"6000\"\n}";
            if (action == "work_cancel")
                return "{\n}";
            if (action == "work_generate")
                return "{\n  \"work\": \"2bf29ef00786a6bc\",\n  \"difficulty\": \"ffffffd21c3933f4\",\n  \"multiplier\": \"1.394647\",\n  \"hash\": \"718CC2121C3E641059BC1C2CFC45666C99E8AE922F7A807B7D07B62C995D79E2\" // since v20.0\n}";
            if (action == "work_peer_add")
                return "{\n  \"success\": \"\"\n}";
            if (action == "work_peers")
                return "{\n  \"work_peers\": [\n    \"::ffff:172.17.0.1:7076\"\n  ]\n}";
            if (action == "work_peers_clear")
                return "{\n  \"success\": \"\"\n}";
            if (action == "work_validate")
                return "{\n  \"valid\": \"1\",\n  \"difficulty\": \"ffffffd21c3933f4\",\n  \"multiplier\": \"1.394647\"\n}";
            if (action == "account_create")
                return "{\n  \"account\": \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\"\n}";
            if (action == "account_list")
                return "{\n  \"accounts\": [\n    \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\"\n  ]\n}";
            if (action == "account_move")
                return "{\n  \"moved\" : \"1\"\n}";
            if (action == "account_remove")
                return "{\n  \"removed\": \"1\"\n}";
            if (action == "account_representative_set")
                return "{\n  \"block\": \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n}";
            if (action == "accounts_create")
                return "{\n  \"accounts\": [\n    \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\",\n    \"nano_1e5aqegc1jb7qe964u4adzmcezyo6o146zb8hm6dft8tkp79za3s00000000\"\n  ]\n}";
            if (action == "password_change")
                return "{\n  \"changed\" : \"1\"\n}";
            if (action == "password_enter")
                return "{\n  \"valid\": \"1\"\n}";
            if (action == "password_valid")
                return "{\n  \"valid\" : \"1\"\n}";
            if (action == "receive")
                return "{\n  \"block\": \"EE5286AB32F580AB65FD84A69E107C69FBEB571DEC4D99297E19E3FA5529547B\"\n}";
            if (action == "receive_minimum")
                return "{\n  \"amount\": \"1000000000000000000000000\"\n}";
            if (action == "receive_minimum_set")
                return "{\n  \"success\": \"\"\n}";
            if (action == "search_pending")
                return "{\n  \"started\": \"1\"\n}";
            if (action == "search_pending_all")
                return "{\n  \"success\": \"\"  \n}";
            if (action == "send")
                return "{\n  \"block\": \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n}";
            if (action == "wallet_add")
                return "{\n  \"account\": \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\"\n}";
            if (action == "wallet_add_watch")
                return "{\n  \"success\" : \"\"\n}";
            if (action == "wallet_balances")
                return "{\n  \"balances\" : {\n    \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\": {\n      \"balance\": \"10000\",\n      \"pending\": \"10000\"\n    }\n  }\n}";
            if (action == "wallet_change_seed")
                return "{\n  \"success\" : \"\",\n  \"last_restored_account\": \"nano_1mhdfre3zczr86mp44jd3xft1g1jg66jwkjtjqixmh6eajfexxti7nxcot9c\",\n  \"restored_count\": \"1\"\n}";
            if (action == "wallet_contains")
                return "{\n  \"exists\": \"1\"\n}";
            if (action == "wallet_create")
                return "{\n  \"wallet\": \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n}";
            if (action == "wallet_destroy")
                return "{\n  \"destroyed\": \"1\"\n}";
            if (action == "wallet_export")
                return "{\n  \"json\" : \"{\\\"0000000000000000000000000000000000000000000000000000000000000000\\\": \\\"0000000000000000000000000000000000000000000000000000000000000001\\\"}\"\n}";
            if (action == "wallet_frontiers")
                return "{\n  \"frontiers\": {\n    \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\": \"000D1BAEC8EC208142C99059B393051BAC8380F9B5A2E6B2489A277D81789F3F\"\n  }\n}";
            if (action == "wallet_history")
                return "{\n  \"history\":\n  [\n    {\n      \"type\": \"send\",\n      \"account\": \"nano_1qato4k7z3spc8gq1zyd8xeqfbzsoxwo36a45ozbrxcatut7up8ohyardu1z\",\n      \"amount\": \"30000000000000000000000000000000000\",\n      \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"hash\": \"87434F8041869A01C8F6F263B87972D7BA443A72E0A97D7A3FD0CCC2358FD6F9\",\n      \"local_timestamp\": \"1527698508\"\n    },\n    {\n      \"type\": \"send\",\n      \"account\": \"nano_38ztgpejb7yrm7rr586nenkn597s3a1sqiy3m3uyqjicht7kzuhnihdk6zpz\",\n      \"amount\": \"40000000000000000000000000000000000\",\n      \"block_account\": \"nano_1ipx847tk8o46pwxt5qjdbncjqcbwcc1rrmqnkztrfjy5k7z4imsrata9est\",\n      \"hash\": \"CE898C131AAEE25E05362F247760F8A3ACF34A9796A5AE0D9204E86B0637965E\",\n      \"local_timestamp\": \"1527698492\"\n    }\n  ]\n}";
            if (action == "wallet_info")
                return "{\n  \"balance\": \"10000\",\n  \"pending\": \"10000\",\n  \"accounts_count\": \"3\",\n  \"adhoc_count\": \"1\",\n  \"deterministic_count\": \"2\",\n  \"deterministic_index\": \"2\"\n}";
            if (action == "wallet_ledger")
                return "{\n  \"accounts\": {\n    \"nano_11119gbh8hb4hj1duf7fdtfyf5s75okzxdgupgpgm1bj78ex3kgy7frt3s9n\": {\n      \"frontier\": \"E71AF3E9DD86BBD8B4620EFA63E065B34D358CFC091ACB4E103B965F95783321\",\n      \"open_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"representative_block\": \"643B77F1ECEFBDBE1CC909872964C1DBBE23A6149BD3CEF2B50B76044659B60F\",\n      \"balance\": \"0\",\n      \"modified_timestamp\": \"1511476234\",\n      \"block_count\": \"2\"\n    }\n  }\n}";
            if (action == "wallet_lock")
                return "{\n  \"locked\": \"1\"\n}";
            if (action == "wallet_locked")
                return "{\n  \"locked\": \"0\"\n}";
            if (action == "wallet_pending")
                return "{\n  \"blocks\": {\n    \"nano_1111111111111111111111111111111111111111111111111117353trpda\": [\"142A538F36833D1CC78B94E11C766F75818F8B940771335C6C1B8AB880C5BB1D\"],\n    \"nano_3t6k35gi95xu6tergt6p69ck76ogmitsa8mnijtpxm9fkcm736xtoncuohr3\": [\"4C1FEEF0BEA7F50BE35489A1233FE002B212DEA554B55B1B470D78BD8F210C74\"]\n  }\n}";
            if (action == "wallet_representative")
                return "{\n  \"representative\": \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\"\n}";
            if (action == "wallet_representative_set")
                return "{\n  \"set\": \"1\"\n}";
            if (action == "wallet_republish")
                return "{\n  \"blocks\": [\n    \"991CF190094C00F0B68E2E5F75F6BEE95A2E0BD93CEAA4A6734DB9F19B728948\",\n    \"A170D51B94E00371ACE76E35AC81DC9405D5D04D4CEBC399AEACE07AE05DD293\",\n    \"90D0C16AC92DD35814E84BFBCC739A039615D0A42A76EF44ADAEF1D99E9F8A35\"\n  ]       \n}";
            if (action == "wallet_work_get")
                return "{\n  \"works\": {\n    \"nano_1111111111111111111111111111111111111111111111111111hifc8npp\": \"432e5cf728c90f4f\"\n  }\n}";
            if (action == "work_get")
                return "{\n  \"work\": \"432e5cf728c90f4f\"\n}";
            if (action == "work_set")
                return "{\n    \"success\": \"\"\n}";
            if (action == "krai_from_raw")
                return "{\n  \"amount\": \"1\"\n}";
            if (action == "krai_to_raw")
                return "{\n  \"amount\": \"1000000000000000000000000000\"\n}";
            if (action == "mrai_from_raw")
                return "{\n  \"amount\": \"1\"\n}";
            if (action == "mrai_to_raw")
                return "{\n  \"amount\": \"1000000000000000000000000000000\"\n}";
            if (action == "rai_from_raw")
                return "{\n  \"amount\": \"1\"\n}";
            if (action == "rai_to_raw")
                return "{\n  \"amount\": \"1000000000000000000000000\"\n}";
            if (action == "payment_begin")
                return "{\n  \"account\" : \"nano_3e3j5tkog48pnny9dmfzj1r16pg8t1e76dz5tmac6iq689wyjfpi00000000\"\n}";
            if (action == "payment_end")
                return "{\n}";
            if (action == "payment_init")
                return "{\n  \"status\": \"Ready\"\n}";
            if (action == "payment_wait")
                return "{\n  \"deprecated\": \"1\",\n  \"status\" : \"success\"\n}";
            return "{}";
        }
    }
}