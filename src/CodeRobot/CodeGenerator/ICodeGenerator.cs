// Copyright (c) rigofunc (xuyingting). All rights reserved.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using CodeRobot.Database.RevEng;

namespace CodeRobot.Generator
{
    /// <summary>
    /// Defines the interfaces for database reverse engineering code generator.
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Generates source code from <paramref name="databaseModel"/> with optional namespace, <seealso cref="DatabaseModel"/>
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="namespace">The namespace for the generated class, if null, will use the namespace of the this tools.</param>
        /// <returns>A range of syntax node and its corresponded object name.</returns>
        IEnumerable<(string name, SyntaxNode syntaxNode)> Generate(DatabaseModel databaseModel, string @namespace = null);
    }
}
