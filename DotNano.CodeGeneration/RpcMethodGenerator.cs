using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNano.Shared;
using DotNano.Shared.Model;

namespace DotNano.CodeGeneration
{
    class RpcMethodGenerator
    {
        internal ClassDeclarationSyntax AddMethod(ClassDeclarationSyntax rpcClientClass, RpcCallCodeDefinition rpcCall)
        {
            var methodName = Tools.ToPascalCase(rpcCall.MethodName);
            if (rpcClientClass.Members.Where(x => x.Kind() == SyntaxKind.MethodDeclaration).Cast<MethodDeclarationSyntax>().Any(x => x.Identifier.ValueText.Equals(methodName)))
                return rpcClientClass;

            var methodBlock = CreateMethodCode(rpcCall);
            var methodDefinition = CreateMethod(rpcCall).WithBody(methodBlock);

            rpcClientClass = rpcClientClass.AddMembers(methodDefinition);
            return rpcClientClass;
        }

        private BlockSyntax CreateMethodCode(RpcCallCodeDefinition rpcCall)
        {
            var jsonObject = CreateJsonObject(rpcCall);
            var callMethodStatement = CreateCallRpcMethodStatement();
            var returnStatement = CreateReturnStatement($"{Tools.ToPascalCase(rpcCall.MethodName)}Response");

            var statements = jsonObject.ToList();
            statements.Add(callMethodStatement);
            statements.Add(returnStatement);

            return SyntaxFactory.Block(statements.ToArray());
        }

        private StatementSyntax CreateReturnStatement(string responseTypeName)
        {
            return SyntaxFactory.ReturnStatement(
                        SyntaxFactory.InvocationExpression(
                            SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                SyntaxFactory.IdentifierName("JsonConvert"),
                                SyntaxFactory.GenericName(
                                    SyntaxFactory.Identifier("DeserializeObject"))
                                .WithTypeArgumentList(
                                    SyntaxFactory.TypeArgumentList(
                                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                            SyntaxFactory.IdentifierName(responseTypeName))))))
                        .WithArgumentList(
                            SyntaxFactory.ArgumentList(
                                SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]{
                                    SyntaxFactory.Argument(
                                        SyntaxFactory.IdentifierName("response")),
                                    SyntaxFactory.Token(SyntaxKind.CommaToken),
                                    SyntaxFactory.Argument(
                                         SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                              SyntaxFactory.IdentifierName("JsonSerializationSettings"),
                                              SyntaxFactory.IdentifierName("PascalCaseSettings")))}))));
        }

        private StatementSyntax CreateCallRpcMethodStatement()
        {
            return SyntaxFactory.LocalDeclarationStatement(
                        SyntaxFactory.VariableDeclaration(
                            SyntaxFactory.IdentifierName("var"))
                        .WithVariables(
                            SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("response"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.AwaitExpression(
                                            SyntaxFactory.InvocationExpression(
                                                SyntaxFactory.IdentifierName("CallRpcMethod"))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.Argument(
                                                        SyntaxFactory.InvocationExpression(
                                                            SyntaxFactory.MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                SyntaxFactory.IdentifierName("jobject"),
                                                                SyntaxFactory.IdentifierName("ToString")))))))))))));
        }

        private IEnumerable<StatementSyntax> CreateJsonObject(RpcCallCodeDefinition rpcCall)
        {
            yield return InitializeJObject();

            var action = CreateJObjectAssignment("action", rpcCall.MethodName, true, typeof(string));
            yield return action;

            foreach (var parameter in rpcCall.RequiredParameters)
            {
                yield return CreateJObjectAssignment(parameter.Key, Tools.ReplaceFirstCharacterToLowerVariant(Tools.ToPascalCase(parameter.Key)), false, parameter.Value);
            }

            foreach (var parameter in rpcCall.OptionalParameters)
            {
                var jobjectAssignment = CreateJObjectAssignment(parameter.Key, Tools.ReplaceFirstCharacterToLowerVariant(Tools.ToPascalCase(parameter.Key)), false, parameter.Value);
                yield return IsSpecialParameter(parameter.Key) ?
                    jobjectAssignment :
                    SyntaxFactory.IfStatement(
                        SyntaxFactory.BinaryExpression(
                            SyntaxKind.NotEqualsExpression,
                            SyntaxFactory.IdentifierName(Tools.ReplaceFirstCharacterToLowerVariant(Tools.ToPascalCase(parameter.Key))),
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.NullLiteralExpression)),
                        jobjectAssignment);
            }
        }

        private bool IsSpecialParameter(string paramName)
        {
            return paramName == "json_block";
        }

        private StatementSyntax InitializeJObject()
        {
            return SyntaxFactory.LocalDeclarationStatement(
                        SyntaxFactory.VariableDeclaration(
                            SyntaxFactory.IdentifierName("var"))
                        .WithVariables(
                            SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("jobject"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.ObjectCreationExpression(
                                            SyntaxFactory.IdentifierName("JObject"))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList()))))));
        }

        private StatementSyntax CreateJObjectAssignment(string propertyName, string value, bool isLiteral, Type type)
        {
            #region Special cases
            if (IsSpecialParameter(propertyName))
            {
                value = "true";
                isLiteral = true;
            }
            #endregion

            ExpressionSyntax expressionValue;
            if (isLiteral)
                expressionValue = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(value));
            else if (type == typeof(string))
                expressionValue = SyntaxFactory.IdentifierName(value);
            else if (type.Name == "IEnumerable`1")
                expressionValue = SyntaxFactory.InvocationExpression(
                                    SyntaxFactory.MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        SyntaxFactory.IdentifierName("JArray"),
                                        SyntaxFactory.IdentifierName("FromObject")))
                                .WithArgumentList(
                                    SyntaxFactory.ArgumentList(
                                        SyntaxFactory.SingletonSeparatedList(
                                            SyntaxFactory.Argument(
                                                SyntaxFactory.IdentifierName(value)))));
            else
                expressionValue = SyntaxFactory.InvocationExpression(
                                    SyntaxFactory.MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        SyntaxFactory.IdentifierName(value),
                                        SyntaxFactory.IdentifierName("ToString")));

            return SyntaxFactory.ExpressionStatement(
                    SyntaxFactory.AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        SyntaxFactory.ElementAccessExpression(
                            SyntaxFactory.IdentifierName("jobject"))
                        .WithArgumentList(
                            SyntaxFactory.BracketedArgumentList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Argument(
                                        SyntaxFactory.LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            SyntaxFactory.Literal(propertyName)))))),
                        expressionValue));
        }

        private MethodDeclarationSyntax CreateMethod(RpcCallCodeDefinition rpcCall)
        {
            var methodName = Tools.ToPascalCase(rpcCall.MethodName);
            var parameters = SyntaxFactory.SeparatedList<ParameterSyntax>();

            foreach (var param in rpcCall.RequiredParameters.Where(x => !IsSpecialParameter(x.Key)))
            {
                parameters = parameters.Add(SyntaxFactory.Parameter(SyntaxFactory.Identifier(Tools.ReplaceFirstCharacterToLowerVariant(Tools.ToPascalCase(param.Key))))
                    .WithType(param.Value.Name == "IEnumerable`1" ? SyntaxFactory.ParseTypeName($"IEnumerable<{param.Value.GetGenericArguments().Single().Name}>") : SyntaxFactory.ParseTypeName(param.Value.Name)));
            }

            foreach (var param in rpcCall.OptionalParameters.Where(x => !IsSpecialParameter(x.Key)))
            {
                parameters = parameters.Add(SyntaxFactory.Parameter(SyntaxFactory.Identifier(Tools.ReplaceFirstCharacterToLowerVariant(Tools.ToPascalCase(param.Key))))
                    .WithType(param.Value.IsValueType ? SyntaxFactory.NullableType(SyntaxFactory.ParseTypeName(param.Value.Name)) : SyntaxFactory.ParseTypeName(param.Value.Name))
                    .WithDefault(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression))));
            }

            return SyntaxFactory.MethodDeclaration(
                            SyntaxFactory.IdentifierName($"Task<{methodName}Response>"),
                            SyntaxFactory.Identifier(methodName))
                        .WithModifiers(
                            SyntaxFactory.TokenList(
                                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                                SyntaxFactory.Token(SyntaxKind.AsyncKeyword)))
                        .WithParameterList(
                            SyntaxFactory.ParameterList(parameters));
        }
    }
}
