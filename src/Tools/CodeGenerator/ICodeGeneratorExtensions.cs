// Copyright (c) rigofunc (xuyingting). All rights reserved.

using System.IO;
using Tools.Database.RevEng;

namespace Tools.Generator
{
    /// <summary>
    /// Provides some extensions for <see cref="ICodeGenerator"/> interfaces.
    /// </summary>
    public static class ICodeGeneratorExtensions
    {
        /// <summary>
        /// Generates source code from <paramref name="databaseModel"/> to the specified <paramref name="toFolder"/>, <seealso cref="DatabaseModel"/>
        /// </summary>
        /// <param name="codeGenerator">The <see cref="ICodeGenerator"/>.</param>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="toFolder">The folder to save the source code.</param>
        /// <param name="namespace">The namespace for the generated class, if null, will use the namespace of the this tools.</param>
        public static void EmitSourceCode(this ICodeGenerator codeGenerator, DatabaseModel databaseModel, string toFolder, string @namespace = null)
        {
            foreach (var item in codeGenerator.Generate(databaseModel, @namespace))
            {
                var fileName = $@"{toFolder}\{item.name}.cs";

                // write to disk.
                File.WriteAllText(fileName, item.syntaxNode.ToFullString());
            }
        }
    }
}
