using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using DotNano.RpcApi.Responses;
using DotNano.Shared;
using DotNano.Shared.DataTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNano.RpcApi
{
    public class NanoRpcClient : IDisposable
    {
        private Uri _rpcUri;
        private HttpClient _httpClient;
        public NanoRpcClient(string ipAddress, int port, HttpClient httpClient = null)
        {
            _rpcUri = new Uri($"http://{ipAddress}:{port}/");
            _httpClient = httpClient ?? new HttpClient();
        }

        private async Task<string> CallRpcMethod(string json)
        {
            var content = new StringContent(json);
            var responseMessage = await _httpClient.PostAsync(_rpcUri, content);
            if (!responseMessage.IsSuccessStatusCode)
                throw new HttpRequestException($"Http status code: {responseMessage.StatusCode}");
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<AccountsPendingResponseBase> AccountsPending(IEnumerable<PublicAddress> accounts, Int64 count, BigInteger? threshold = null, Boolean? source = null, Boolean? includeActive = null, Boolean? sorting = null, Boolean? includeOnlyConfirmed = null)
        {
            var jobject = new JObject();
            jobject["action"] = "accounts_pending";
            jobject["accounts"] = JArray.FromObject(accounts);
            jobject["count"] = count.ToString();
            if (threshold != null)
                jobject["threshold"] = threshold.ToString();
            if (source != null)
                jobject["source"] = source.ToString();
            if (includeActive != null)
                jobject["include_active"] = includeActive.ToString();
            if (sorting != null)
                jobject["sorting"] = sorting.ToString();
            if (includeOnlyConfirmed != null)
                jobject["include_only_confirmed"] = includeOnlyConfirmed.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            try
            {
                return JsonConvert.DeserializeObject<AccountsPendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<AccountsPendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }

        public async Task<PendingResponseBase> Pending(PublicAddress account, Int64? count = null, BigInteger? threshold = null, Boolean? source = null, Boolean? includeActive = null, Boolean? minVersion = null, Boolean? sorting = null, Boolean? includeOnlyConfirmed = null)
        {
            var jobject = new JObject();
            jobject["action"] = "pending";
            jobject["account"] = account.ToString();
            if (count != null)
                jobject["count"] = count.ToString();
            if (threshold != null)
                jobject["threshold"] = threshold.ToString();
            if (source != null)
                jobject["source"] = source.ToString();
            if (includeActive != null)
                jobject["include_active"] = includeActive.ToString();
            if (minVersion != null)
                jobject["min_version"] = minVersion.ToString();
            if (sorting != null)
                jobject["sorting"] = sorting.ToString();
            if (includeOnlyConfirmed != null)
                jobject["include_only_confirmed"] = includeOnlyConfirmed.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            try
            {
                return JsonConvert.DeserializeObject<PendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<PendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }

        public async Task<RepresentativesOnlineResponseBase> RepresentativesOnline(Boolean? weight = null)
        {
            var jobject = new JObject();
            jobject["action"] = "representatives_online";
            if (weight != null)
                jobject["weight"] = weight.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            try
            {
                return JsonConvert.DeserializeObject<RepresentativesOnlineBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<RepresentativesOnlineResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }

        public async Task<WalletPendingResponseBase> WalletPending(HexKey64 wallet, Int64 count, BigInteger? threshold = null, Boolean? source = null, Boolean? includeActive = null, Boolean? minVersion = null, Boolean? includeOnlyConfirmed = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_pending";
            jobject["wallet"] = wallet.ToString();
            jobject["count"] = count.ToString();
            if (threshold != null)
                jobject["threshold"] = threshold.ToString();
            if (source != null)
                jobject["source"] = source.ToString();
            if (includeActive != null)
                jobject["include_active"] = includeActive.ToString();
            if (minVersion != null)
                jobject["min_version"] = minVersion.ToString();
            if (includeOnlyConfirmed != null)
                jobject["include_only_confirmed"] = includeOnlyConfirmed.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            try
            {
                return JsonConvert.DeserializeObject<WalletPendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<WalletPendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }

        public async Task<AccountBalanceResponse> AccountBalance(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "account_balance";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountBalanceResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountBlockCountResponse> AccountBlockCount(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "account_block_count";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountBlockCountResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountGetResponse> AccountGet(HexKey64 key)
        {
            var jobject = new JObject();
            jobject["action"] = "account_get";
            jobject["key"] = key.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountGetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountHistoryResponse> AccountHistory(PublicAddress account, Int64 count, Boolean? raw = null, HexKey64 head = null, BigInteger? offset = null, Boolean? reverse = null, PublicAddress accountfilter = null)
        {
            var jobject = new JObject();
            jobject["action"] = "account_history";
            jobject["account"] = account.ToString();
            jobject["count"] = count.ToString();
            if (raw != null)
                jobject["raw"] = raw.ToString();
            if (head != null)
                jobject["head"] = head.ToString();
            if (offset != null)
                jobject["offset"] = offset.ToString();
            if (reverse != null)
                jobject["reverse"] = reverse.ToString();
            if (accountfilter != null)
                jobject["accountfilter"] = accountfilter.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountHistoryResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountInfoResponse> AccountInfo(PublicAddress account, Boolean? representative = null, Boolean? weight = null, Boolean? pending = null)
        {
            var jobject = new JObject();
            jobject["action"] = "account_info";
            jobject["account"] = account.ToString();
            if (representative != null)
                jobject["representative"] = representative.ToString();
            if (weight != null)
                jobject["weight"] = weight.ToString();
            if (pending != null)
                jobject["pending"] = pending.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountInfoResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountKeyResponse> AccountKey(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "account_key";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountKeyResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountRepresentativeResponse> AccountRepresentative(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "account_representative";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountRepresentativeResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountWeightResponse> AccountWeight(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "account_weight";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountWeightResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountsBalancesResponse> AccountsBalances(IEnumerable<PublicAddress> accounts)
        {
            var jobject = new JObject();
            jobject["action"] = "accounts_balances";
            jobject["accounts"] = JArray.FromObject(accounts);
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountsBalancesResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountsFrontiersResponse> AccountsFrontiers(IEnumerable<PublicAddress> accounts)
        {
            var jobject = new JObject();
            jobject["action"] = "accounts_frontiers";
            jobject["accounts"] = JArray.FromObject(accounts);
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountsFrontiersResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ActiveDifficultyResponse> ActiveDifficulty(Boolean? includeTrend = null)
        {
            var jobject = new JObject();
            jobject["action"] = "active_difficulty";
            if (includeTrend != null)
                jobject["include_trend"] = includeTrend.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ActiveDifficultyResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AvailableSupplyResponse> AvailableSupply()
        {
            var jobject = new JObject();
            jobject["action"] = "available_supply";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AvailableSupplyResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlockAccountResponse> BlockAccount(HexKey64 hash)
        {
            var jobject = new JObject();
            jobject["action"] = "block_account";
            jobject["hash"] = hash.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlockAccountResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlockConfirmResponse> BlockConfirm(HexKey64 hash)
        {
            var jobject = new JObject();
            jobject["action"] = "block_confirm";
            jobject["hash"] = hash.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlockConfirmResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlockCountResponse> BlockCount(Boolean? includeCemented = null)
        {
            var jobject = new JObject();
            jobject["action"] = "block_count";
            if (includeCemented != null)
                jobject["include_cemented"] = includeCemented.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlockCountResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlockCountTypeResponse> BlockCountType()
        {
            var jobject = new JObject();
            jobject["action"] = "block_count_type";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlockCountTypeResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlockCreateResponse> BlockCreate(String type, BigInteger? balance = null, HexKey64 key = null, PublicAddress representative = null, HexKey64 link = null, HexKey64 previous = null, PublicAddress wallet = null, PublicAddress account = null, HexKey64 source = null, PublicAddress destination = null, String work = null)
        {
            var jobject = new JObject();
            jobject["action"] = "block_create";
            jobject["type"] = type;
            jobject["json_block"] = "true";
            if (balance != null)
                jobject["balance"] = balance.ToString();
            if (key != null)
                jobject["key"] = key.ToString();
            if (representative != null)
                jobject["representative"] = representative.ToString();
            if (link != null)
                jobject["link"] = link.ToString();
            if (previous != null)
                jobject["previous"] = previous.ToString();
            if (wallet != null)
                jobject["wallet"] = wallet.ToString();
            if (account != null)
                jobject["account"] = account.ToString();
            if (source != null)
                jobject["source"] = source.ToString();
            if (destination != null)
                jobject["destination"] = destination.ToString();
            if (work != null)
                jobject["work"] = work;
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlockCreateResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlockHashResponse> BlockHash(BlockContent block)
        {
            var jobject = new JObject();
            jobject["action"] = "block_hash";
            jobject["block"] = block.ToString();
            jobject["json_block"] = "true";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlockHashResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlockInfoResponse> BlockInfo(HexKey64 hash)
        {
            var jobject = new JObject();
            jobject["action"] = "block_info";
            jobject["hash"] = hash.ToString();
            jobject["json_block"] = "true";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlockInfoResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlocksResponse> Blocks(IEnumerable<HexKey64> hashes)
        {
            var jobject = new JObject();
            jobject["action"] = "blocks";
            jobject["hashes"] = JArray.FromObject(hashes);
            jobject["json_block"] = "true";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlocksResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BlocksInfoResponse> BlocksInfo(IEnumerable<HexKey64> hashes, Boolean? pending = null, Boolean? source = null, Boolean? balance = null, Boolean? includeNotFound = null)
        {
            var jobject = new JObject();
            jobject["action"] = "blocks_info";
            jobject["hashes"] = JArray.FromObject(hashes);
            jobject["json_block"] = "true";
            if (pending != null)
                jobject["pending"] = pending.ToString();
            if (source != null)
                jobject["source"] = source.ToString();
            if (balance != null)
                jobject["balance"] = balance.ToString();
            if (includeNotFound != null)
                jobject["include_not_found"] = includeNotFound.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BlocksInfoResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BootstrapResponse> Bootstrap(String address, Int64 port, Boolean? bypassFrontierConfirmation = null)
        {
            var jobject = new JObject();
            jobject["action"] = "bootstrap";
            jobject["address"] = address;
            jobject["port"] = port.ToString();
            if (bypassFrontierConfirmation != null)
                jobject["bypass_frontier_confirmation"] = bypassFrontierConfirmation.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BootstrapResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BootstrapAnyResponse> BootstrapAny(Boolean? force = null)
        {
            var jobject = new JObject();
            jobject["action"] = "bootstrap_any";
            if (force != null)
                jobject["force"] = force.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BootstrapAnyResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BootstrapLazyResponse> BootstrapLazy(HexKey64 hash, Boolean? force = null)
        {
            var jobject = new JObject();
            jobject["action"] = "bootstrap_lazy";
            jobject["hash"] = hash.ToString();
            if (force != null)
                jobject["force"] = force.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BootstrapLazyResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<BootstrapStatusResponse> BootstrapStatus()
        {
            var jobject = new JObject();
            jobject["action"] = "bootstrap_status";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<BootstrapStatusResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ChainResponse> Chain(HexKey64 block, Int64 count, HexKey64 offset = null, Boolean? reverse = null)
        {
            var jobject = new JObject();
            jobject["action"] = "chain";
            jobject["block"] = block.ToString();
            jobject["count"] = count.ToString();
            if (offset != null)
                jobject["offset"] = offset.ToString();
            if (reverse != null)
                jobject["reverse"] = reverse.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ChainResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ConfirmationActiveResponse> ConfirmationActive(BigInteger? announcements = null)
        {
            var jobject = new JObject();
            jobject["action"] = "confirmation_active";
            if (announcements != null)
                jobject["announcements"] = announcements.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ConfirmationActiveResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ConfirmationHeightCurrentlyProcessingResponse> ConfirmationHeightCurrentlyProcessing()
        {
            var jobject = new JObject();
            jobject["action"] = "confirmation_height_currently_processing";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ConfirmationHeightCurrentlyProcessingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ConfirmationHistoryResponse> ConfirmationHistory(HexKey64 hash = null)
        {
            var jobject = new JObject();
            jobject["action"] = "confirmation_history";
            if (hash != null)
                jobject["hash"] = hash.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ConfirmationHistoryResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ConfirmationInfoResponse> ConfirmationInfo(HexKey64 root, Boolean? representatives = null, Boolean? contents = null)
        {
            var jobject = new JObject();
            jobject["action"] = "confirmation_info";
            jobject["root"] = root.ToString();
            jobject["json_block"] = "true";
            if (representatives != null)
                jobject["representatives"] = representatives.ToString();
            if (contents != null)
                jobject["contents"] = contents.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ConfirmationInfoResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ConfirmationQuorumResponse> ConfirmationQuorum(Boolean? peerDetails = null)
        {
            var jobject = new JObject();
            jobject["action"] = "confirmation_quorum";
            if (peerDetails != null)
                jobject["peer_details"] = peerDetails.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ConfirmationQuorumResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<DatabaseTxnTrackerResponse> DatabaseTxnTracker(Int64 minReadTime, Int64 minWriteTime)
        {
            var jobject = new JObject();
            jobject["action"] = "database_txn_tracker";
            jobject["min_read_time"] = minReadTime.ToString();
            jobject["min_write_time"] = minWriteTime.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<DatabaseTxnTrackerResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<DelegatorsResponse> Delegators(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "delegators";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<DelegatorsResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<DelegatorsCountResponse> DelegatorsCount(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "delegators_count";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<DelegatorsCountResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<DeterministicKeyResponse> DeterministicKey(HexKey64 seed, Int64 index)
        {
            var jobject = new JObject();
            jobject["action"] = "deterministic_key";
            jobject["seed"] = seed.ToString();
            jobject["index"] = index.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<DeterministicKeyResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<EpochUpgradeResponse> EpochUpgrade(Int64 epoch, HexKey64 key, BigInteger? count = null)
        {
            var jobject = new JObject();
            jobject["action"] = "epoch_upgrade";
            jobject["epoch"] = epoch.ToString();
            jobject["key"] = key.ToString();
            if (count != null)
                jobject["count"] = count.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<EpochUpgradeResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<FrontierCountResponse> FrontierCount()
        {
            var jobject = new JObject();
            jobject["action"] = "frontier_count";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<FrontierCountResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<FrontiersResponse> Frontiers(PublicAddress account, Int64 count)
        {
            var jobject = new JObject();
            jobject["action"] = "frontiers";
            jobject["account"] = account.ToString();
            jobject["count"] = count.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<FrontiersResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<KeepaliveResponse> Keepalive(String address, Int64 port)
        {
            var jobject = new JObject();
            jobject["action"] = "keepalive";
            jobject["address"] = address;
            jobject["port"] = port.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<KeepaliveResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<KeyCreateResponse> KeyCreate()
        {
            var jobject = new JObject();
            jobject["action"] = "key_create";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<KeyCreateResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<KeyExpandResponse> KeyExpand(HexKey64 key)
        {
            var jobject = new JObject();
            jobject["action"] = "key_expand";
            jobject["key"] = key.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<KeyExpandResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<LedgerResponse> Ledger(PublicAddress account, Int64 count, Boolean? representative = null, Boolean? weight = null, Boolean? pending = null, BigInteger? modifiedSince = null, Boolean? sorting = null, BigInteger? threshold = null)
        {
            var jobject = new JObject();
            jobject["action"] = "ledger";
            jobject["account"] = account.ToString();
            jobject["count"] = count.ToString();
            if (representative != null)
                jobject["representative"] = representative.ToString();
            if (weight != null)
                jobject["weight"] = weight.ToString();
            if (pending != null)
                jobject["pending"] = pending.ToString();
            if (modifiedSince != null)
                jobject["modified_since"] = modifiedSince.ToString();
            if (sorting != null)
                jobject["sorting"] = sorting.ToString();
            if (threshold != null)
                jobject["threshold"] = threshold.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<LedgerResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<NodeIdResponse> NodeId()
        {
            var jobject = new JObject();
            jobject["action"] = "node_id";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<NodeIdResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<NodeIdDeleteResponse> NodeIdDelete()
        {
            var jobject = new JObject();
            jobject["action"] = "node_id_delete";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<NodeIdDeleteResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PeersResponse> Peers(Boolean? peerDetails = null)
        {
            var jobject = new JObject();
            jobject["action"] = "peers";
            if (peerDetails != null)
                jobject["peer_details"] = peerDetails.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PeersResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PendingExistsResponse> PendingExists(HexKey64 hash, Boolean? includeActive = null, Boolean? includeOnlyConfirmed = null)
        {
            var jobject = new JObject();
            jobject["action"] = "pending_exists";
            jobject["hash"] = hash.ToString();
            if (includeActive != null)
                jobject["include_active"] = includeActive.ToString();
            if (includeOnlyConfirmed != null)
                jobject["include_only_confirmed"] = includeOnlyConfirmed.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PendingExistsResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ProcessResponse> Process(BlockContent block, Boolean? force = null, PublicAddress subtype = null, Boolean? watchWork = null)
        {
            var jobject = new JObject();
            jobject["action"] = "process";
            jobject["block"] = block.ToString();
            jobject["json_block"] = "true";
            if (force != null)
                jobject["force"] = force.ToString();
            if (subtype != null)
                jobject["subtype"] = subtype.ToString();
            if (watchWork != null)
                jobject["watch_work"] = watchWork.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ProcessResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<RepresentativesResponse> Representatives(BigInteger? count = null, Boolean? sorting = null)
        {
            var jobject = new JObject();
            jobject["action"] = "representatives";
            if (count != null)
                jobject["count"] = count.ToString();
            if (sorting != null)
                jobject["sorting"] = sorting.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<RepresentativesResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<RepublishResponse> Republish(HexKey64 hash, Int64? count = null, Int64? sources = null, Int64? destinations = null)
        {
            var jobject = new JObject();
            jobject["action"] = "republish";
            jobject["hash"] = hash.ToString();
            if (count != null)
                jobject["count"] = count.ToString();
            if (sources != null)
                jobject["sources"] = sources.ToString();
            if (destinations != null)
                jobject["destinations"] = destinations.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<RepublishResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<SignResponse> Sign(HexKey64 key = null, BlockContent block = null, HexKey64 wallet = null, PublicAddress account = null, HexKey64 hash = null)
        {
            var jobject = new JObject();
            jobject["action"] = "sign";
            jobject["json_block"] = "true";
            if (key != null)
                jobject["key"] = key.ToString();
            if (block != null)
                jobject["block"] = block.ToString();
            if (wallet != null)
                jobject["wallet"] = wallet.ToString();
            if (account != null)
                jobject["account"] = account.ToString();
            if (hash != null)
                jobject["hash"] = hash.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<SignResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<StatsResponse> Stats(String type)
        {
            var jobject = new JObject();
            jobject["action"] = "stats";
            jobject["type"] = type;
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<StatsResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<StatsClearResponse> StatsClear()
        {
            var jobject = new JObject();
            jobject["action"] = "stats_clear";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<StatsClearResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<StopResponse> Stop()
        {
            var jobject = new JObject();
            jobject["action"] = "stop";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<StopResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<SuccessorsResponse> Successors(HexKey64 block, Int64 count, HexKey64 offset = null, Boolean? reverse = null)
        {
            var jobject = new JObject();
            jobject["action"] = "successors";
            jobject["block"] = block.ToString();
            jobject["count"] = count.ToString();
            if (offset != null)
                jobject["offset"] = offset.ToString();
            if (reverse != null)
                jobject["reverse"] = reverse.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<SuccessorsResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ValidateAccountNumberResponse> ValidateAccountNumber(PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "validate_account_number";
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ValidateAccountNumberResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<VersionResponse> Version()
        {
            var jobject = new JObject();
            jobject["action"] = "version";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<VersionResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<UncheckedResponse> Unchecked(Int64 count)
        {
            var jobject = new JObject();
            jobject["action"] = "unchecked";
            jobject["json_block"] = "true";
            jobject["count"] = count.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<UncheckedResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<UncheckedClearResponse> UncheckedClear()
        {
            var jobject = new JObject();
            jobject["action"] = "unchecked_clear";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<UncheckedClearResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<UncheckedGetResponse> UncheckedGet(HexKey64 hash)
        {
            var jobject = new JObject();
            jobject["action"] = "unchecked_get";
            jobject["hash"] = hash.ToString();
            jobject["json_block"] = "true";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<UncheckedGetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<UncheckedKeysResponse> UncheckedKeys(HexKey64 key, Int64 count)
        {
            var jobject = new JObject();
            jobject["action"] = "unchecked_keys";
            jobject["key"] = key.ToString();
            jobject["count"] = count.ToString();
            jobject["json_block"] = "true";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<UncheckedKeysResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<UnopenedResponse> Unopened(PublicAddress account, Int64 count, BigInteger? threshold = null)
        {
            var jobject = new JObject();
            jobject["action"] = "unopened";
            jobject["account"] = account.ToString();
            jobject["count"] = count.ToString();
            if (threshold != null)
                jobject["threshold"] = threshold.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<UnopenedResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<UptimeResponse> Uptime()
        {
            var jobject = new JObject();
            jobject["action"] = "uptime";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<UptimeResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkCancelResponse> WorkCancel(HexKey64 hash)
        {
            var jobject = new JObject();
            jobject["action"] = "work_cancel";
            jobject["hash"] = hash.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkCancelResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkGenerateResponse> WorkGenerate(HexKey64 hash, String difficulty = null, Boolean? usePeers = null, BigInteger? multiplier = null, Boolean? account = null)
        {
            var jobject = new JObject();
            jobject["action"] = "work_generate";
            jobject["hash"] = hash.ToString();
            if (difficulty != null)
                jobject["difficulty"] = difficulty;
            if (usePeers != null)
                jobject["use_peers"] = usePeers.ToString();
            if (multiplier != null)
                jobject["multiplier"] = multiplier.ToString();
            if (account != null)
                jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkGenerateResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkPeerAddResponse> WorkPeerAdd(String address, Int64 port)
        {
            var jobject = new JObject();
            jobject["action"] = "work_peer_add";
            jobject["address"] = address;
            jobject["port"] = port.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkPeerAddResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkPeersResponse> WorkPeers()
        {
            var jobject = new JObject();
            jobject["action"] = "work_peers";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkPeersResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkPeersClearResponse> WorkPeersClear()
        {
            var jobject = new JObject();
            jobject["action"] = "work_peers_clear";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkPeersClearResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkValidateResponse> WorkValidate(String work, HexKey64 hash, String difficulty = null, BigInteger? multiplier = null)
        {
            var jobject = new JObject();
            jobject["action"] = "work_validate";
            jobject["work"] = work;
            jobject["hash"] = hash.ToString();
            if (difficulty != null)
                jobject["difficulty"] = difficulty;
            if (multiplier != null)
                jobject["multiplier"] = multiplier.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkValidateResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountCreateResponse> AccountCreate(HexKey64 wallet, Int64? index = null, Boolean? work = null)
        {
            var jobject = new JObject();
            jobject["action"] = "account_create";
            jobject["wallet"] = wallet.ToString();
            if (index != null)
                jobject["index"] = index.ToString();
            if (work != null)
                jobject["work"] = work.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountCreateResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountListResponse> AccountList(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "account_list";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountListResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountMoveResponse> AccountMove(HexKey64 wallet, HexKey64 source, IEnumerable<PublicAddress> accounts)
        {
            var jobject = new JObject();
            jobject["action"] = "account_move";
            jobject["wallet"] = wallet.ToString();
            jobject["source"] = source.ToString();
            jobject["accounts"] = JArray.FromObject(accounts);
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountMoveResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountRemoveResponse> AccountRemove(HexKey64 wallet, PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "account_remove";
            jobject["wallet"] = wallet.ToString();
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountRemoveResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountRepresentativeSetResponse> AccountRepresentativeSet(HexKey64 wallet, PublicAddress account, PublicAddress representative, PublicAddress work = null)
        {
            var jobject = new JObject();
            jobject["action"] = "account_representative_set";
            jobject["wallet"] = wallet.ToString();
            jobject["account"] = account.ToString();
            jobject["representative"] = representative.ToString();
            if (work != null)
                jobject["work"] = work.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountRepresentativeSetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<AccountsCreateResponse> AccountsCreate(HexKey64 wallet, Int64 count, Boolean? work = null)
        {
            var jobject = new JObject();
            jobject["action"] = "accounts_create";
            jobject["wallet"] = wallet.ToString();
            jobject["count"] = count.ToString();
            if (work != null)
                jobject["work"] = work.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<AccountsCreateResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PasswordChangeResponse> PasswordChange(HexKey64 wallet, String password)
        {
            var jobject = new JObject();
            jobject["action"] = "password_change";
            jobject["wallet"] = wallet.ToString();
            jobject["password"] = password;
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PasswordChangeResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PasswordEnterResponse> PasswordEnter(HexKey64 wallet, String password)
        {
            var jobject = new JObject();
            jobject["action"] = "password_enter";
            jobject["wallet"] = wallet.ToString();
            jobject["password"] = password;
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PasswordEnterResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PasswordValidResponse> PasswordValid(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "password_valid";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PasswordValidResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ReceiveResponse> Receive(HexKey64 wallet, PublicAddress account, HexKey64 block, PublicAddress work = null)
        {
            var jobject = new JObject();
            jobject["action"] = "receive";
            jobject["wallet"] = wallet.ToString();
            jobject["account"] = account.ToString();
            jobject["block"] = block.ToString();
            if (work != null)
                jobject["work"] = work.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ReceiveResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ReceiveMinimumResponse> ReceiveMinimum()
        {
            var jobject = new JObject();
            jobject["action"] = "receive_minimum";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ReceiveMinimumResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<ReceiveMinimumSetResponse> ReceiveMinimumSet(BigInteger amount)
        {
            var jobject = new JObject();
            jobject["action"] = "receive_minimum_set";
            jobject["amount"] = amount.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<ReceiveMinimumSetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<SearchPendingResponse> SearchPending(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "search_pending";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<SearchPendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<SearchPendingAllResponse> SearchPendingAll()
        {
            var jobject = new JObject();
            jobject["action"] = "search_pending_all";
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<SearchPendingAllResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<SendResponse> Send(HexKey64 wallet, PublicAddress source, PublicAddress destination, Int64 amount, String id = null, String work = null)
        {
            var jobject = new JObject();
            jobject["action"] = "send";
            jobject["wallet"] = wallet.ToString();
            jobject["source"] = source.ToString();
            jobject["destination"] = destination.ToString();
            jobject["amount"] = amount.ToString();
            if (id != null)
                jobject["id"] = id;
            if (work != null)
                jobject["work"] = work;
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<SendResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletAddResponse> WalletAdd(HexKey64 wallet, HexKey64 key, Boolean? work = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_add";
            jobject["wallet"] = wallet.ToString();
            jobject["key"] = key.ToString();
            if (work != null)
                jobject["work"] = work.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletAddResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletAddWatchResponse> WalletAddWatch(HexKey64 wallet, IEnumerable<PublicAddress> accounts)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_add_watch";
            jobject["wallet"] = wallet.ToString();
            jobject["accounts"] = JArray.FromObject(accounts);
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletAddWatchResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletBalancesResponse> WalletBalances(HexKey64 wallet, BigInteger? threshold = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_balances";
            jobject["wallet"] = wallet.ToString();
            if (threshold != null)
                jobject["threshold"] = threshold.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletBalancesResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletChangeSeedResponse> WalletChangeSeed(HexKey64 wallet, HexKey64 seed, BigInteger? count = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_change_seed";
            jobject["wallet"] = wallet.ToString();
            jobject["seed"] = seed.ToString();
            if (count != null)
                jobject["count"] = count.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletChangeSeedResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletContainsResponse> WalletContains(HexKey64 wallet, PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_contains";
            jobject["wallet"] = wallet.ToString();
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletContainsResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletCreateResponse> WalletCreate(HexKey64 seed = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_create";
            if (seed != null)
                jobject["seed"] = seed.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletCreateResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletDestroyResponse> WalletDestroy(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_destroy";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletDestroyResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletExportResponse> WalletExport(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_export";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletExportResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletFrontiersResponse> WalletFrontiers(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_frontiers";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletFrontiersResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletHistoryResponse> WalletHistory(HexKey64 wallet, BigInteger? modifiedSince = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_history";
            jobject["wallet"] = wallet.ToString();
            if (modifiedSince != null)
                jobject["modified_since"] = modifiedSince.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletHistoryResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletInfoResponse> WalletInfo(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_info";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletInfoResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletLedgerResponse> WalletLedger(HexKey64 wallet, Boolean? representative = null, Boolean? weight = null, Boolean? pending = null, BigInteger? modifiedSince = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_ledger";
            jobject["wallet"] = wallet.ToString();
            if (representative != null)
                jobject["representative"] = representative.ToString();
            if (weight != null)
                jobject["weight"] = weight.ToString();
            if (pending != null)
                jobject["pending"] = pending.ToString();
            if (modifiedSince != null)
                jobject["modified_since"] = modifiedSince.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletLedgerResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletLockResponse> WalletLock(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_lock";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletLockResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletLockedResponse> WalletLocked(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_locked";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletLockedResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletRepresentativeResponse> WalletRepresentative(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_representative";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletRepresentativeResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletRepresentativeSetResponse> WalletRepresentativeSet(HexKey64 wallet, PublicAddress representative, Boolean? updateExistingAccounts = null)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_representative_set";
            jobject["wallet"] = wallet.ToString();
            jobject["representative"] = representative.ToString();
            if (updateExistingAccounts != null)
                jobject["update_existing_accounts"] = updateExistingAccounts.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletRepresentativeSetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletRepublishResponse> WalletRepublish(HexKey64 wallet, Int64 count)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_republish";
            jobject["wallet"] = wallet.ToString();
            jobject["count"] = count.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletRepublishResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WalletWorkGetResponse> WalletWorkGet(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "wallet_work_get";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WalletWorkGetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkGetResponse> WorkGet(HexKey64 wallet, PublicAddress account)
        {
            var jobject = new JObject();
            jobject["action"] = "work_get";
            jobject["wallet"] = wallet.ToString();
            jobject["account"] = account.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkGetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<WorkSetResponse> WorkSet(HexKey64 wallet, PublicAddress account, Int64 work)
        {
            var jobject = new JObject();
            jobject["action"] = "work_set";
            jobject["wallet"] = wallet.ToString();
            jobject["account"] = account.ToString();
            jobject["work"] = work.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<WorkSetResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<KraiFromRawResponse> KraiFromRaw(BigInteger amount)
        {
            var jobject = new JObject();
            jobject["action"] = "krai_from_raw";
            jobject["amount"] = amount.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<KraiFromRawResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<KraiToRawResponse> KraiToRaw(Int64 amount)
        {
            var jobject = new JObject();
            jobject["action"] = "krai_to_raw";
            jobject["amount"] = amount.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<KraiToRawResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<MraiFromRawResponse> MraiFromRaw(BigInteger amount)
        {
            var jobject = new JObject();
            jobject["action"] = "mrai_from_raw";
            jobject["amount"] = amount.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<MraiFromRawResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<MraiToRawResponse> MraiToRaw(Int64 amount)
        {
            var jobject = new JObject();
            jobject["action"] = "mrai_to_raw";
            jobject["amount"] = amount.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<MraiToRawResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<RaiFromRawResponse> RaiFromRaw(BigInteger amount)
        {
            var jobject = new JObject();
            jobject["action"] = "rai_from_raw";
            jobject["amount"] = amount.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<RaiFromRawResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<RaiToRawResponse> RaiToRaw(Int64 amount)
        {
            var jobject = new JObject();
            jobject["action"] = "rai_to_raw";
            jobject["amount"] = amount.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<RaiToRawResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PaymentBeginResponse> PaymentBegin(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "payment_begin";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PaymentBeginResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PaymentEndResponse> PaymentEnd(PublicAddress account, HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "payment_end";
            jobject["account"] = account.ToString();
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PaymentEndResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PaymentInitResponse> PaymentInit(HexKey64 wallet)
        {
            var jobject = new JObject();
            jobject["action"] = "payment_init";
            jobject["wallet"] = wallet.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PaymentInitResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }

        public async Task<PaymentWaitResponse> PaymentWait(PublicAddress account, Int64 amount, Int64 timeout)
        {
            var jobject = new JObject();
            jobject["action"] = "payment_wait";
            jobject["account"] = account.ToString();
            jobject["amount"] = amount.ToString();
            jobject["timeout"] = timeout.ToString();
            var response = await CallRpcMethod(jobject.ToString());
            return JsonConvert.DeserializeObject<PaymentWaitResponse>(response, JsonSerializationSettings.PascalCaseSettings);
        }
    }
}