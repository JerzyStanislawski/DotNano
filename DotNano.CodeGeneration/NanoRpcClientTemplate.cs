using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Numerics;
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

        private string CallRpcMethod(string json)
        {
            var content = new StringContent(json);
            var responseMessage = _httpClient.PostAsync(_rpcUri, content).Result;
            
            if (!responseMessage.IsSuccessStatusCode)
                throw new HttpRequestException($"Http status code: {responseMessage.StatusCode}");
            
            return responseMessage.Content.ReadAsStringAsync().Result;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public AccountsPendingResponseBase AccountsPending(IEnumerable<PublicAddress> accounts, Int64 count, BigInteger? threshold = null, Boolean? source = null, Boolean? includeActive = null, Boolean? sorting = null, Boolean? includeOnlyConfirmed = null)
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
            var response = CallRpcMethod(jobject.ToString());

            try
            {
                return JsonConvert.DeserializeObject<AccountsPendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<AccountsPendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }

        public PendingResponseBase Pending(PublicAddress account, Int64? count = null, BigInteger? threshold = null, Boolean? source = null, Boolean? includeActive = null, Boolean? minVersion = null, Boolean? sorting = null, Boolean? includeOnlyConfirmed = null)
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
            var response = CallRpcMethod(jobject.ToString());

            try
            {
                return JsonConvert.DeserializeObject<PendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<PendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }

        public RepresentativesOnlineResponseBase RepresentativesOnline(Boolean? weight = null)
        {
            var jobject = new JObject();
            jobject["action"] = "representatives_online";
            if (weight != null)
                jobject["weight"] = weight.ToString();
            var response = CallRpcMethod(jobject.ToString());

            try
            {
                return JsonConvert.DeserializeObject<RepresentativesOnlineBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<RepresentativesOnlineResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }

        public WalletPendingResponseBase WalletPending(HexKey64 wallet, Int64 count, BigInteger? threshold = null, Boolean? source = null, Boolean? includeActive = null, Boolean? minVersion = null, Boolean? includeOnlyConfirmed = null)
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
            var response = CallRpcMethod(jobject.ToString());

            try
            {
                return JsonConvert.DeserializeObject<WalletPendingBasicResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
            catch
            {
                return JsonConvert.DeserializeObject<WalletPendingResponse>(response, JsonSerializationSettings.PascalCaseSettings);
            }
        }
    }
}
