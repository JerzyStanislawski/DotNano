using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNano.Shared.Model;

namespace DotNano.CodeGeneration
{
     class TestHttpMessageHandlerGenerator
    {
        internal ClassDeclarationSyntax AddGetResponsePart(ClassDeclarationSyntax testHttpMessageHandlerClass, RpcCallCodeDefinition rpcCall)
        {
            var getResponseMethod = GetGetResponseMethod(testHttpMessageHandlerClass);

            var statement = SyntaxFactory.IfStatement(
                                SyntaxFactory.BinaryExpression(
                                    SyntaxKind.EqualsExpression,
                                    SyntaxFactory.IdentifierName("action"),
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal(rpcCall.MethodName))),
                                SyntaxFactory.ReturnStatement(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal(rpcCall.RpcCallDoc.JsonResponses.First()))));

            return testHttpMessageHandlerClass.ReplaceNode(getResponseMethod, getResponseMethod.AddBodyStatements(statement));
        }

        internal ClassDeclarationSyntax ClearGetResponseMethodBody(ClassDeclarationSyntax testHttpMessageHandlerClass)
        {
            var getResponseMethod = GetGetResponseMethod(testHttpMessageHandlerClass);
            return testHttpMessageHandlerClass.ReplaceNode(getResponseMethod, getResponseMethod.WithBody(SyntaxFactory.Block()));
        }

        internal ClassDeclarationSyntax AppendGetResponseReturnStatement(ClassDeclarationSyntax testHttpMessageHandlerClass)
        {
            var getResponseMethod = GetGetResponseMethod(testHttpMessageHandlerClass);
            return testHttpMessageHandlerClass.ReplaceNode(getResponseMethod, getResponseMethod.AddBodyStatements(
                SyntaxFactory.ReturnStatement(
                    SyntaxFactory.LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal("{}")))));
        }

        private MethodDeclarationSyntax GetGetResponseMethod(ClassDeclarationSyntax testHttpMessageHandlerClass)
        {
            return (MethodDeclarationSyntax)testHttpMessageHandlerClass.Members.Single(x => x.Kind() == SyntaxKind.MethodDeclaration && ((MethodDeclarationSyntax)x).Identifier.ValueText == "GetResponse");
        }
    }
}