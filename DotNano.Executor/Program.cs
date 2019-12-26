using DotNano.CodeGeneration;
using DotNano.RpcDocParser;
using System.Configuration;
using System.Linq;
using System.Net;

namespace DotNano.Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webClient = new WebClient())
            {
                var docUrl = ConfigurationManager.AppSettings["RpcDocUri"];
                var mdDoc = webClient.DownloadString(docUrl);

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
