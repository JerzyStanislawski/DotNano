using DotNano.CodeGeneration;
using System.Net.Http;

namespace DotNano.RpcApi.Tests
{
    public class NanoRpcClientTests
    {
        NanoRpcClient _nanoRpcClient;

        public NanoRpcClientTests()
        {
            var httpMessageHandler = new TestHttpMessageHandler();
            var httpClient = new HttpClient(httpMessageHandler); //Remove httpMessageHandler parameter to run live tests based on local node.
            _nanoRpcClient = new NanoRpcClient("localhost", 7076, httpClient);
        }
    }
}
