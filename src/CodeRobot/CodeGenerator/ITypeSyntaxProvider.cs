// Copyright (c) rigofunc (xuyingting). All rights reserved.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeRobot.Generator
{
    /// <summary>
    /// Defines the interfaces for converting SQL data type to <see cref="TypeSyntax"/>.
    /// </summary>
    public interface ITypeSyntaxProvider
    {
        /// <summary>
        /// Creates the <see cref="TypeSyntax"/> by the specified <paramref name="sqlDataType"/>.
        /// </summary>
        /// <param name="sqlDataType">The SQL data type.</param>
        /// <param name="isNullable">A value indicating whether the value can be null.</param>
        /// <returns>The <see cref="TypeSyntax"/></returns>
        TypeSyntax CreateTypeSyntax(string sqlDataType, bool isNullable);
    }
}
