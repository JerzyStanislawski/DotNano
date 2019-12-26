using DotNano.CodeGeneration;
using System.Linq;
using System.Net;

namespace DotNano.RpcDocParser
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webClient = new WebClient())
            {
                var mdDoc = webClient.DownloadString("https://raw.githubusercontent.com/nanocurrency/nano-docs/master/docs/commands/rpc-protocol.md");

                var parser = new RpcMdParser();
                var result = parser.Parse(mdDoc);
                
                var docAnalyzer = new DocAnalyzer(new JsonAnalyzer());
                var rpcCalls = docAnalyzer.Analyze(result).ToList();

                var codeGenerator = new CodeGenerator();
                codeGenerator.Generate(rpcCalls);
            }
        }
    }
}
