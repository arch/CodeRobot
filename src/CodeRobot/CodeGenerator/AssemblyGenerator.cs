// Copyright (c) rigofunc (xuyingting). All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeRobot.Generator
{
    /// <summary>
    /// Represents the assembly generator.
    /// </summary>
    public class AssemblyGenerator
    {
        private readonly IList<MetadataReference> _references = new List<MetadataReference>();

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyGenerator"/> class.
        /// </summary>
        public AssemblyGenerator()
        {
            RefAssemblyContaining<object>();
            RefAssembly(typeof(Enumerable).Assembly);
        }

		/// <summary>
		/// References the specified assembly.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns>The <see cref="AssemblyGenerator"/></returns>
		public AssemblyGenerator RefAssembly(Assembly assembly)
        {
            _references.Add(MetadataReference.CreateFromFile(assembly.Location));

            return this;
        }

		/// <summary>
		/// References the specified assembly containing the specified type.
		/// </summary>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <returns>The <see cref="AssemblyGenerator"/></returns>
		public AssemblyGenerator RefAssemblyContaining<T>()
        {
            RefAssembly(typeof(T).Assembly);

            return this;
        }

        /// <summary>
        /// Generates an assembly by the specified code and optional specified assembly name.
        /// </summary>
        /// <param name="code">The source code.</param>
        /// <param name="assemblyName">The optional assembly name.</param>
        /// <returns>The <see cref="Assembly"/> generated in memory.</returns>
        public Assembly Generate(string code, string assemblyName = null)
        {
            // review: maybe we should used hardcode assembly name.
            if (string.IsNullOrEmpty(assemblyName))
            {
                assemblyName = Path.GetRandomFileName();
            }

            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            var compilation = CSharpCompilation.Create(assemblyName,
                                                       syntaxTrees: new[] { syntaxTree },
                                                       references: _references,
                                                       options: new CSharpCompilationOptions(
                                                                        OutputKind.DynamicallyLinkedLibrary,
                                                                        optimizationLevel: OptimizationLevel.Release));
            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);
                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    var sb = new StringBuilder();
                    foreach (var diagnostic in failures)
                    {
                        sb.AppendFormat("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }

                    throw new Exception(sb.ToString());
                }
                else
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Assembly.Load(stream.ToArray());
                }
            }
        }

        /// <summary>
        /// Generates an assembly by the specified code and optional specified assembly name.
        /// </summary>
        /// <param name="code">The source code.</param>
        /// <param name="assemblyName">The optional assembly name.</param>
        /// <param name="saveDirectory">The directory to save the generated assembly.</param>
        public void Generate(string code, string assemblyName, string saveDirectory)
        {
            // review: maybe we should used hardcode assembly name.
            if (string.IsNullOrEmpty(assemblyName))
            {
                assemblyName = Path.GetRandomFileName();
            }
            if (!assemblyName.EndsWith(".dll"))
            {
                assemblyName += ".dll";
            }

            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            var compilation = CSharpCompilation.Create(assemblyName,
                                                       syntaxTrees: new[] { syntaxTree },
                                                       references: _references,
                                                       options: new CSharpCompilationOptions(
                                                                        OutputKind.DynamicallyLinkedLibrary,
                                                                        optimizationLevel: OptimizationLevel.Release));
            var result = compilation.Emit(Path.Combine(saveDirectory, assemblyName));
            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                var sb = new StringBuilder();
                foreach (var diagnostic in failures)
                {
                    sb.AppendFormat("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                }

                throw new Exception(sb.ToString());
            }
        }

        /// <summary>
        /// Generates an assembly by the specified code generator and optional specified assembly name.
        /// </summary>
        /// <param name="nodes">A range of <see cref="SyntaxNode"/>.</param>
        /// <param name="assemblyName">The optional assembly name.</param>
        /// <returns>The <see cref="Assembly"/> generated in memory.</returns>
        public Assembly Generate(IEnumerable<SyntaxNode> nodes, string assemblyName = null)
        {
            // review: maybe we should used hardcode assembly name.
            if (string.IsNullOrEmpty(assemblyName))
            {
                assemblyName = Path.GetRandomFileName();
            }

            var syntaxTrees = new List<SyntaxTree>();
            foreach (var node in nodes)
            {
                syntaxTrees.Add(node.SyntaxTree);
            }

            var compilation = CSharpCompilation.Create(assemblyName,
                                                       syntaxTrees: syntaxTrees,
                                                       references: _references,
                                                       options: new CSharpCompilationOptions(
                                                                        OutputKind.DynamicallyLinkedLibrary,
                                                                        optimizationLevel: OptimizationLevel.Release));
            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);
                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    var sb = new StringBuilder();
                    foreach (var diagnostic in failures)
                    {
                        sb.AppendFormat("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }

                    throw new Exception(sb.ToString());
                }
                else
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Assembly.Load(stream.ToArray());
                }
            }
        }

        /// <summary>
        /// Generates an assembly by the specified code generator and optional specified assembly name.
        /// </summary>
        /// <param name="nodes">A range of <see cref="SyntaxNode"/>.</param>
        /// <param name="assemblyName">The optional assembly name.</param>
        /// <param name="saveDirectory">The directory to save the generated assembly.</param>
        public void Generate(IEnumerable<SyntaxNode> nodes, string assemblyName, string saveDirectory)
        {
            // review: maybe we should used hardcode assembly name.
            if (string.IsNullOrEmpty(assemblyName))
            {
                assemblyName = Path.GetRandomFileName();
            }
            if (!assemblyName.EndsWith(".dll"))
            {
                assemblyName += ".dll";
            }

            var syntaxTrees = new List<SyntaxTree>();
            foreach (var node in nodes)
            {
                syntaxTrees.Add(node.SyntaxTree);
            }

            var compilation = CSharpCompilation.Create(assemblyName,
                                                       syntaxTrees: syntaxTrees,
                                                       references: _references,
                                                       options: new CSharpCompilationOptions(
                                                                        OutputKind.DynamicallyLinkedLibrary,
                                                                        optimizationLevel: OptimizationLevel.Release));
            var result = compilation.Emit(Path.Combine(saveDirectory, assemblyName));
            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                var sb = new StringBuilder();
                foreach (var diagnostic in failures)
                {
                    sb.AppendFormat("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                }

                throw new Exception(sb.ToString());
            }
        }
    }
}
