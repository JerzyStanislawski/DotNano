using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNano.Shared;
using DotNano.Shared.DataTypes;
using DotNano.Shared.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DotNano.CodeGeneration
{
    public class ResponsesTestsGenerator
    {
        public NamespaceDeclarationSyntax Generate(string name, List<string> jsonResponses)
        {
            var testClasses = MakeTestClasses(name, jsonResponses);

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("DotNano.RpcApi.Tests.Responses"));
            @namespace = @namespace.AddMembers(testClasses.ToArray());

            return @namespace;
        }

        private IEnumerable<ClassDeclarationSyntax> MakeTestClasses(string name, List<string> jsonResponses)
        {
            int? suffix = null;

            foreach (var jsonResponse in jsonResponses)
            {
                var className = $"{name}ResponseTest{suffix}";
                suffix = suffix == null ? 1 : ++suffix;

                var @class = SyntaxFactory.ClassDeclaration(className).WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

                // string _json = "...";
                var jsonField = SyntaxFactory.FieldDeclaration(SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName("string")).AddVariables(SyntaxFactory.VariableDeclarator("_json").WithInitializer(
                        SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(jsonResponse))))));
                @class = @class.AddMembers(jsonField);

                //...Response _responseObject;
                var responseField = SyntaxFactory.FieldDeclaration(SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName($"{name}Response")).AddVariables(SyntaxFactory.VariableDeclarator("_responseObject")));
                @class = @class.AddMembers(responseField);

                //CreateResponseObject method
                var createResponseMethodBody = MakeJsonConvertStatement(name);
                var createResponseMethod = MakeMethod("CreateResponseObject", createResponseMethodBody, SyntaxKind.PrivateKeyword);
                @class = @class.AddMembers(createResponseMethod);

                //ShouldProperlyConvertJsonToResponseObject method
                var createObjectInvocation = SyntaxFactory.ExpressionStatement(SyntaxFactory.InvocationExpression(SyntaxFactory.IdentifierName("CreateResponseObject")));
                var shouldConvertMethod = MakeTestMethod("ShouldProperlyConvertJsonToResponseObject", createObjectInvocation);
                @class = @class.AddMembers(shouldConvertMethod);

                //ShouldPopulateResponseObjectValues method
                var response = JObject.Parse(jsonResponse);
                MethodDeclarationSyntax shouldPopulateValuesMethod = null;
                foreach (var jToken in response)
                {
                    if (jToken.Value is JValue jValue)
                    {
                        var stringValue = jValue.Value<string>();
                        var type = Tools.TypeFromValue(stringValue);                                                
                        var assertEqualStatement = MakeAssertEqualStatement(stringValue, type, Tools.ToPascalCase(jToken.Key));

                        if (shouldPopulateValuesMethod == null)
                            shouldPopulateValuesMethod = MakeTestMethod("ShouldPopulateResponseObjectValues", createObjectInvocation);
                        
                        shouldPopulateValuesMethod = shouldPopulateValuesMethod.AddBodyStatements(assertEqualStatement);
                    }
                }

                if (shouldPopulateValuesMethod != null)
                    @class = @class.AddMembers(shouldPopulateValuesMethod);

                yield return @class;
            }
        }

        private ExpressionStatementSyntax MakeAssertEqualStatement(string stringValue, Type type, string property)
        {   
            var expressionToParse = PrepareAssertExpressionArgument(stringValue, type);
            var assertEqualArgument = SyntaxFactory.Argument(SyntaxFactory.ParseExpression(expressionToParse));
            return MakeAssertEqualStatement(assertEqualArgument, property);
        }

        private string PrepareAssertExpressionArgument(string stringValue, Type type)
        {
            if (type == typeof(BigInteger))
                return $"BigInteger.Parse(\"{stringValue}\")";

            var isStringValue = type == typeof(string) || type == typeof(HexKey64) || type == typeof(HexKey128) || type == typeof(PublicAddress) || type == typeof(PeerAddress);
            return isStringValue ? $"\"{stringValue.Replace("\"", "\\\"")}\"" : stringValue;
        }
        
        private ExpressionStatementSyntax MakeAssertEqualStatement(ArgumentSyntax assertEqualFirstArgument, string property)
        {
            return  SyntaxFactory.ExpressionStatement(
                           SyntaxFactory.InvocationExpression(
                               SyntaxFactory.MemberAccessExpression(
                                   SyntaxKind.SimpleMemberAccessExpression,
                                   SyntaxFactory.IdentifierName("Assert"),
                                   SyntaxFactory.IdentifierName("Equal")))
                           .WithArgumentList(
                               SyntaxFactory.ArgumentList(
                                   SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                       new SyntaxNodeOrToken[]
                                       {
                                           assertEqualFirstArgument,
                                            SyntaxFactory.Token(SyntaxKind.CommaToken),
                                            SyntaxFactory.Argument(
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.IdentifierName("_responseObject"),
                                                    SyntaxFactory.IdentifierName(property)))
                                       }
                                       ))));
        }

        private ExpressionStatementSyntax MakeJsonConvertStatement(string responseClassName)
        {
            return SyntaxFactory.ExpressionStatement(
                                SyntaxFactory.AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    SyntaxFactory.IdentifierName("_responseObject"),
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.IdentifierName("JsonConvert"),
                                            SyntaxFactory.GenericName(
                                                SyntaxFactory.Identifier("DeserializeObject"))
                                            .WithTypeArgumentList(
                                                SyntaxFactory.TypeArgumentList(
                                                    SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                        SyntaxFactory.IdentifierName($"{responseClassName}Response"))))))
                                    .WithArgumentList(
                                        SyntaxFactory.ArgumentList(
                                            SyntaxFactory.SeparatedList(new List<ArgumentSyntax> {
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.IdentifierName("_json")),
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                                        SyntaxFactory.IdentifierName("JsonSerializationSettings"),
                                                        SyntaxFactory.IdentifierName("PascalCaseSettings")))
                                            }
                                       )))));
        }

        private MethodDeclarationSyntax MakeMethod(string methodName, ExpressionStatementSyntax statement, SyntaxKind modifier = SyntaxKind.PublicKeyword)
        {
            return SyntaxFactory.MethodDeclaration(
                                        SyntaxFactory.PredefinedType(
                                            SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                                        SyntaxFactory.Identifier(methodName))                                    
                                    .WithModifiers(
                                        SyntaxFactory.TokenList(
                                            SyntaxFactory.Token(modifier)))
                                    .WithBody(SyntaxFactory.Block(statement));
        }

        private MethodDeclarationSyntax MakeTestMethod(string methodName, ExpressionStatementSyntax statement)
        {
            return MakeMethod(methodName, statement).WithAttributeLists(
                                        SyntaxFactory.SingletonList(
                                            SyntaxFactory.AttributeList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.Attribute(
                                                        SyntaxFactory.IdentifierName("Fact"))))));
        }
    }
}