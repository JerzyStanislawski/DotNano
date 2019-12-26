using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace DotNano.Shared
{
    //https://stackoverflow.com/a/39070361
    public static class SyntaxExtensions
    {
        /// <summary>
        /// Generates the type syntax.
        /// </summary>
        public static TypeSyntax AsTypeSyntax(this Type type)
        {
            string name = type.Name.Replace('+', '.');

            if (type.IsGenericType)
            {
                // Get the C# representation of the generic type minus its type arguments.
                name = name.Substring(0, name.IndexOf("`"));

                // Generate the name of the generic type.
                var genericArgs = type.GetGenericArguments();
                return SyntaxFactory.GenericName(SyntaxFactory.Identifier(name),
                    SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(genericArgs.Select(AsTypeSyntax)))
                );
            }
            else
                return SyntaxFactory.ParseTypeName(name);
        }
    }
}
