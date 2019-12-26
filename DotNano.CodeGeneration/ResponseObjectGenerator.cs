using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNano.Shared;
using DotNano.Shared.DataTypes;
using DotNano.Shared.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNano.CodeGeneration.Tests")]
namespace DotNano.CodeGeneration
{
    class ResponseObjectGenerator
    {
        public NamespaceDeclarationSyntax Generate(string name, SimpleJson simpleJson)
        {
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("DotNano.RpcApi.Responses"));

            var classes = new List<ClassDeclarationSyntax>();
            MakeClass($"{name}Response", simpleJson, classes);
            
            @namespace = @namespace.AddMembers(classes.Last());
            classes.Take(classes.Count() - 1).ToList().ForEach(x => @namespace = @namespace.AddMembers(x));

            return @namespace;
        }

        private TypeSyntax MakeClass(string name, SimpleJson simpleJson, List<ClassDeclarationSyntax> classes, bool nested = false)
        {
            var innerJsonTypes = simpleJson.InnerJsons.Select(x => GetTypeFromValue(x.Key)).Distinct();
            if (innerJsonTypes.Count() == 1)
            {
                var innerJsonType = innerJsonTypes.Single();
                if (IsSpecialType(innerJsonType))
                {
                    var valueType = MakeClass(name, simpleJson.InnerJsons.First().Value, classes, false);
                    return MakeDictionaryType(SyntaxFactory.ParseTypeName(innerJsonType.Name), valueType);
                }
            }

            var fieldTypes = simpleJson.Fields.Select(x => GetTypeFromValue(x.Key)).Distinct();
            if (fieldTypes.Count() == 1)
            {
                var fieldType = fieldTypes.Single();
                if (IsSpecialType(fieldType))
                {
                    var valueType = simpleJson.Fields.First().Value;
                    return MakeDictionaryType(GetTypeName(fieldType), GetTypeName(valueType));
                }
            }
            
            var @class = SyntaxFactory.ClassDeclaration($"{name}").AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            foreach (var property in simpleJson.Fields)
            {
                @class = AddProperty(@class, Tools.ToPascalCase(property.Key), property.Value.AsTypeSyntax());
            }
            foreach (var innerJson in simpleJson.InnerJsons)
            {
                var innerClassName = $"{name.Replace("Response", "")}{Tools.ToPascalCase(innerJson.Key.TrimEnd('s'))}";
                var innerClassType = MakeClass(innerClassName, innerJson.Value, classes, true);
                @class = AddProperty(@class, Tools.ToPascalCase(innerJson.Key), innerClassType);
            }
            foreach (var innerJson in simpleJson.ArraysOfObjects)
            {
                var innerClassName = $"{name.Replace("Response", "")}{Tools.ToPascalCase(innerJson.Key.TrimEnd('s'))}";
                var innerClassType = MakeClass(innerClassName, innerJson.Value, classes, true);
                var enumerableType = SyntaxFactory.GenericName(SyntaxFactory.Identifier("IEnumerable"),
                    SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(new List<TypeSyntax>
                    {
                        innerClassType
                    })));
                @class = AddProperty(@class, Tools.ToPascalCase(innerJson.Key), enumerableType);
            }

            if (!nested)
            {
                @class = AddProperty(@class, "Error", typeof(string).AsTypeSyntax());
                @class = AddIsSuccessfulProperty(@class);
            }

            classes.Add(@class);
            return SyntaxFactory.ParseTypeName(@class.Identifier.ValueText);
        }

        private ClassDeclarationSyntax AddIsSuccessfulProperty(ClassDeclarationSyntax @class)
        {
            var isSuccessful = SyntaxFactory.PropertyDeclaration(
                                   SyntaxFactory.PredefinedType(
                                       SyntaxFactory.Token(SyntaxKind.BoolKeyword)),
                                   SyntaxFactory.Identifier("IsSuccessful"))
                               .WithModifiers(
                                   SyntaxFactory.TokenList(
                                       SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                               .WithExpressionBody(
                                   SyntaxFactory.ArrowExpressionClause(
                                       SyntaxFactory.InvocationExpression(
                                           SyntaxFactory.MemberAccessExpression(
                                               SyntaxKind.SimpleMemberAccessExpression,
                                               SyntaxFactory.IdentifierName("String"),
                                               SyntaxFactory.IdentifierName("IsNullOrEmpty")))
                                       .WithArgumentList(
                                           SyntaxFactory.ArgumentList(
                                               SyntaxFactory.SingletonSeparatedList(
                                                   SyntaxFactory.Argument(
                                                       SyntaxFactory.IdentifierName("Error")))))))
                                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

            return @class.AddMembers(isSuccessful);
        }

        private string GetTypeName(Type type)
        {
            string friendlyName = type.Name;
            if (type.IsGenericType)
            {
                var backtick = friendlyName.IndexOf('`');
                if (backtick > 0)
                {
                    friendlyName = friendlyName.Remove(backtick);
                }
                friendlyName += $"<{string.Join(",", type.GetGenericArguments().Select(p => p.Name))}>";
            }
            return friendlyName;
        }

        private bool IsSpecialType(Type type)
        {
            return type == typeof(HexKey64) || type == typeof(HexKey128) || type == typeof(PublicAddress) || type == typeof(PeerAddress);
        }

        private TypeSyntax MakeDictionaryType(string keyType, string valueType)
        {
            return MakeDictionaryType(SyntaxFactory.ParseTypeName(keyType), SyntaxFactory.ParseTypeName(valueType));
        }

        private TypeSyntax MakeDictionaryType(TypeSyntax keyType, TypeSyntax valueType)
        {
            return SyntaxFactory.GenericName(SyntaxFactory.Identifier("Dictionary"),
                SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(new List<TypeSyntax>
                {
                    keyType, valueType
                })));
        }
                
        private ClassDeclarationSyntax AddProperty(ClassDeclarationSyntax @class, string propertyName, TypeSyntax propertyType)
        {
            @class = @class.AddMembers(SyntaxFactory.PropertyDeclaration(propertyType, propertyName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))));

            return @class;
        }

        private Type GetTypeFromValue(string value)
        {
            if (HexKey64.IsHexKey64(value))
                return typeof(HexKey64);
            if (HexKey128.IsHexKey128(value))
                return typeof(HexKey128);
            if (PublicAddress.IsPublicAddress(value))
                return typeof(PublicAddress);
            if (PeerAddress.TryParse(value, out _))
                return typeof(PeerAddress);

            return null;
        }
    }
}
