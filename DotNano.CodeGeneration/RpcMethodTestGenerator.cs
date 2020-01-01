using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNano.Shared;
using DotNano.Shared.DataTypes;
using DotNano.Shared.Model;
using Newtonsoft.Json.Linq;

namespace DotNano.CodeGeneration
{
    class RpcMethodTestGenerator
    {
        internal ClassDeclarationSyntax AddTestMethod(ClassDeclarationSyntax rpcClientTestClass, RpcCallCodeDefinition rpcCall)
        {
            var methodDeclaration = SyntaxFactory.MethodDeclaration(
                                        SyntaxFactory.PredefinedType(
                                            SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                                        SyntaxFactory.Identifier($"{Tools.ToPascalCase(rpcCall.MethodName)}Test"))
                                    .WithAttributeLists(
                                        SyntaxFactory.SingletonList(
                                            SyntaxFactory.AttributeList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.Attribute(
                                                        SyntaxFactory.IdentifierName("Fact"))))))
                                    .WithModifiers(
                                        SyntaxFactory.TokenList(
                                            SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

            var parameters = GetTestParameters(rpcCall.RequiredParameters, rpcCall.RpcCallDoc.JsonRequests.Last());

            var responseStatement = SyntaxFactory.LocalDeclarationStatement(
                        SyntaxFactory.VariableDeclaration(
                            SyntaxFactory.IdentifierName("var"))
                        .WithVariables(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("response"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.InvocationExpression(
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.IdentifierName("_nanoRpcClient"),
                                                    SyntaxFactory.IdentifierName(Tools.ToPascalCase(rpcCall.MethodName))))
                                            .WithArgumentList(
                                                SyntaxFactory.ArgumentList(
                                                    SyntaxFactory.SeparatedList(parameters))),
                                            SyntaxFactory.IdentifierName("Result")))))));
            var assertStatement = SyntaxFactory.ExpressionStatement(
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.IdentifierName("Assert"),
                                            SyntaxFactory.IdentifierName("NotNull")))
                                    .WithArgumentList(
                                        SyntaxFactory.ArgumentList(
                                            SyntaxFactory.SingletonSeparatedList(
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.IdentifierName("response"))))));

            methodDeclaration = methodDeclaration.WithBody(SyntaxFactory.Block(responseStatement, assertStatement));

            return rpcClientTestClass.AddMembers(methodDeclaration);
        }

        private IEnumerable<ArgumentSyntax> GetTestParameters(Dictionary<string, Type> requiredParameters, string jsonRequest)
        {
            var request = JObject.Parse(jsonRequest);
            foreach (var param in requiredParameters.Where(x => x.Key != "json_block"))
            {
                if (param.Value.Name == "IEnumerable`1")
                    yield return ArgumentSyntaxArray(param.Value.GetGenericArguments().Single());
                else if (param.Value == typeof(BlockContent))
                    yield return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(SyntaxFactory.IdentifierName("BlockContent")).WithArgumentList(SyntaxFactory.ArgumentList()));
                else
                    yield return ArgumentSyntaxFromStringValue(request.Value<string>(param.Key));
            }
        }

        private ArgumentSyntax ArgumentSyntaxArray(Type type)
        {
            return SyntaxFactory.Argument(
                     SyntaxFactory.ArrayCreationExpression(
                        SyntaxFactory.ArrayType(SyntaxFactory.ParseTypeName(type.Name))
                        .WithRankSpecifiers(
                            SyntaxFactory.SingletonList(
                                SyntaxFactory.ArrayRankSpecifier(
                                    SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                                        SyntaxFactory.LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            SyntaxFactory.Literal(0))))))));
        }

        private ArgumentSyntax ArgumentSyntaxFromStringValue(string value)
        {
            if (Boolean.TryParse(value, out var boolValue))
                return SyntaxFactory.Argument(
                          SyntaxFactory.LiteralExpression(
                          boolValue ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression));
            else if (HexKey64.IsHexKey64(value))
                return SyntaxFactory.Argument(NewObjectExpression("HexKey64", value));
            else if (HexKey128.IsHexKey128(value))
                return SyntaxFactory.Argument(NewObjectExpression("HexKey128", value));
            if (Int32.TryParse(value, out var intValue))
                return SyntaxFactory.Argument(
                          SyntaxFactory.LiteralExpression(
                              SyntaxKind.NumericLiteralExpression, 
                              SyntaxFactory.Literal(intValue)));
            else if (Int64.TryParse(value, out var longValue))
                return SyntaxFactory.Argument(
                          SyntaxFactory.LiteralExpression(
                              SyntaxKind.NumericLiteralExpression,
                              SyntaxFactory.Literal(longValue)));
            else if (BigInteger.TryParse(value, out var bigIntValue))
                return SyntaxFactory.Argument(
                    SyntaxFactory.InvocationExpression(
                        SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("BigInteger"),
                            SyntaxFactory.IdentifierName("Parse")))
                    .WithArgumentList(
                        SyntaxFactory.ArgumentList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal(value)))))));
            else if (Double.TryParse(value, out var doubleValue))
                return SyntaxFactory.Argument(
                          SyntaxFactory.LiteralExpression(
                              SyntaxKind.NumericLiteralExpression,
                              SyntaxFactory.Literal(doubleValue)));
            if (PublicAddress.IsPublicAddress(value))
                return SyntaxFactory.Argument(NewObjectExpression("PublicAddress", value));
            else
                return SyntaxFactory.Argument(
                          SyntaxFactory.LiteralExpression(
                              SyntaxKind.StringLiteralExpression,
                              SyntaxFactory.Literal(value)));
        }

        private ExpressionSyntax NewObjectExpression(string className, string value)
        {
            return SyntaxFactory.ObjectCreationExpression(
                                SyntaxFactory.IdentifierName(className))
                            .WithArgumentList(
                                SyntaxFactory.ArgumentList(
                                    SyntaxFactory.SingletonSeparatedList(
                                        SyntaxFactory.Argument(
                                            SyntaxFactory.LiteralExpression(
                                                  SyntaxKind.StringLiteralExpression,
                                                  SyntaxFactory.Literal(value))))));
        }
    }
}
