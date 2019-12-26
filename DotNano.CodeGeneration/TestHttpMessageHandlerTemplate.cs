using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            {
                Content = new StringContent(jsonResponse)
            });
        }

        private string GetResponse(string action)
        {
            throw new NotImplementedException();
        }
    }
}
