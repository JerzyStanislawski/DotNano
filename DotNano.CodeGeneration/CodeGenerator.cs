using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNano.Shared;
using DotNano.Shared.Model;

namespace DotNano.CodeGeneration
{
    public class CodeGenerator
    {
        public void Generate(IEnumerable<RpcCallCodeDefinition> rpcCalls)
        {
            var rpcClientClass = ParseClassTemplate("NanoRpcClientTemplate");
            var rpcClientTestClass = ParseClassTemplate("NanoRpcClientTestsTemplate");
            var testHttpMessageHandlerClass = ParseClassTemplate("TestHttpMessageHandlerTemplate");

            var rpcMethodGenerator = new RpcMethodGenerator();
            var rpcMethodTestGenerator = new RpcMethodTestGenerator();
            var testHttpMessageHandlerGenerator = new TestHttpMessageHandlerGenerator();

            testHttpMessageHandlerClass = testHttpMessageHandlerGenerator.ClearGetResponseMethodBody(testHttpMessageHandlerClass);

            var responseGenerator = new ResponseObjectGenerator();
            var responseTestGenerator = new ResponsesTestsGenerator();
            foreach (var rpcCall in rpcCalls)
            {
                GenerateResponseClass(responseGenerator, rpcCall);
                GenerateResponseTestClass(responseTestGenerator, rpcCall);

                rpcClientClass = rpcMethodGenerator.AddMethod(rpcClientClass, rpcCall);
                rpcClientTestClass = rpcMethodTestGenerator.AddTestMethod(rpcClientTestClass, rpcCall);
                testHttpMessageHandlerClass = testHttpMessageHandlerGenerator.AddGetResponsePart(testHttpMessageHandlerClass, rpcCall);
            }

            testHttpMessageHandlerClass = testHttpMessageHandlerGenerator.AppendGetResponseReturnStatement(testHttpMessageHandlerClass);

            GenerateClassFile(rpcClientClass, "DotNano.RpcApi", $"NanoRpcClient.cs",
                new [] { "System", "System.Collections.Generic", "System.Net.Http", "System.Numerics", "System.Threading.Tasks", "DotNano.RpcApi.Responses", "DotNano.Shared", "DotNano.Shared.DataTypes", "Newtonsoft.Json", "Newtonsoft.Json.Linq" });
            GenerateClassFile(rpcClientTestClass, "DotNano.RpcApi.Tests", $"NanoRpcClientTests.cs",
                new[] { "System", "System.Net.Http", "System.Numerics", "DotNano.Shared.DataTypes", "Xunit" });
            GenerateClassFile(testHttpMessageHandlerClass, "DotNano.RpcApi.Tests", $"TestHttpMessageHandler.cs",
                new[] { "System", "System.Net.Http", "System.Threading", "System.Threading.Tasks", "Newtonsoft.Json.Linq" });            
        }

        private ClassDeclarationSyntax ParseClassTemplate(string fileName)
        {
            var code = new StreamReader($"..\\..\\..\\..\\DotNano.CodeGeneration\\{fileName}.cs").ReadToEnd();
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);
            var @namespace = (NamespaceDeclarationSyntax)compilationUnit.Members.Single();
            return (ClassDeclarationSyntax)@namespace.Members.Single();
        }

        private void GenerateClassFile(ClassDeclarationSyntax @class, string projectName, string fileLocation, IEnumerable<string> usings)
        {
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(projectName));
            @namespace = @namespace.AddMembers(@class);
            GenerateClassFile(@namespace, $"{projectName}\\{fileLocation}", usings);
        }

        private void GenerateClassFile(NamespaceDeclarationSyntax @namespace, string fileLocation, IEnumerable<string> usings)
        {
            var compilationUnit = SyntaxFactory.CompilationUnit();
            foreach (var @using in usings)
            {
                compilationUnit = compilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(@using)));
            }

            compilationUnit = compilationUnit.AddMembers(@namespace);

            var code = compilationUnit
                .NormalizeWhitespace()
                .ToFullString();
            File.WriteAllText($"..\\..\\..\\..\\{fileLocation}", code);
        }

        private void GenerateResponseClass(ResponseObjectGenerator responseGenerator, RpcCallCodeDefinition rpcCall)
        {
            var name = Tools.ToPascalCase(rpcCall.MethodName);
            if (File.Exists($"..\\..\\..\\..\\DotNano.RpcApi\\Responses\\_{name}Response.cs"))
                return;

            var @namespace = responseGenerator.Generate(name, rpcCall.Response);

            GenerateClassFile(@namespace, $"DotNano.RpcApi\\Responses\\{name}Response.cs", new[] { "System", "System.Collections.Generic", "System.Numerics", "DotNano.Shared.DataTypes" });
        }

        private void GenerateResponseTestClass(ResponsesTestsGenerator responsesTestGenerator, RpcCallCodeDefinition rpcCall)
        {
            var name = Tools.ToPascalCase(rpcCall.MethodName);
            if (File.Exists($"..\\..\\..\\..\\DotNano.RpcApi.Tests\\Responses\\_{name}ResponseTests.cs"))
                return;

            var @namespace = responsesTestGenerator.Generate(name, rpcCall.RpcCallDoc.JsonResponses);

            GenerateClassFile(@namespace, $"DotNano.RpcApi.Tests\\Responses\\{name}ResponseTests.cs", 
                new[] { "System", "System.Collections.Generic", "System.Numerics", "DotNano.Shared", "DotNano.Shared.DataTypes", "DotNano.RpcApi.Responses", "Newtonsoft.Json", "Xunit" });
        }
    }


}
