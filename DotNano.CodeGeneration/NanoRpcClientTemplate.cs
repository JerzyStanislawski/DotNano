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

namespace DotNano.CodeGeneration
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
            json.Replace("\"True\"", "\"true\"");
            json.Replace("\"False\"", "\"false\"");
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
                return JsonConvert.DeserializeObject<AccountsPendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<AccountsPendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
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
                return JsonConvert.DeserializeObject<PendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<PendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
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
                return JsonConvert.DeserializeObject<RepresentativesOnlineResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<RepresentativesOnlineBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
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
                return JsonConvert.DeserializeObject<WalletPendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<WalletPendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }
    }
}
