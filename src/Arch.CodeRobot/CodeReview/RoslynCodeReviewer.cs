// Copyright (c) yingtingxu(徐应庭). All rights reserved

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace Arch.CodeRobot
{
    /// <summary>
    /// Represents the Roslyn implementation of <see cref="ICodeReviewer"/> interface.
    /// </summary>
    public class RoslynCodeReviewer : ICodeReviewer
    {
        private List<Document> _documents;
        private readonly string _slnPath;

        /// <summary>
        /// Initializes a new instance of <see cref="RoslynCodeReviewer"/> class.
        /// </summary>
        /// <param name="slnPath">The path of the *.sln file.</param>
        public RoslynCodeReviewer(string slnPath)
        {
            _slnPath = slnPath;
        }

        /// <summary>
        /// Ensures using whether order system first.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        public bool EnsureUsingAreOrderedSystemFirst()
        {
            foreach (var sourcePath in GetSourcePaths())
            {
                var source = File.ReadAllText(sourcePath);
                var syntaxTree = CSharpSyntaxTree.ParseText(source);
                var usings = ((CompilationUnitSyntax)syntaxTree.GetRoot()).Usings
                    .Select(u => u.Name.ToString());

                var sorted = usings.OrderByDescending(u => u.StartsWith("System"));

                if (!usings.SequenceEqual(sorted))
                {
                    throw new Exception("Usings ordered incorrectly in '" + sourcePath + "'");
                }
            }

            return true;
        }

        /// <summary>
        /// Ensures source code doesn't contains tabs.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        public bool EnsureSourceCodeDoesNotContainTabs()
        {
            foreach (var sourcePath in GetSourcePaths())
            {
                var source = File.ReadAllText(sourcePath);
                var syntaxTree = CSharpSyntaxTree.ParseText(source);

                var hasTabs =
                    syntaxTree.GetRoot()
                    .DescendantTrivia(descendIntoTrivia: true)
                    .Any(node => node.IsKind(SyntaxKind.WhitespaceTrivia)
                         && node.ToString().IndexOf('\t') >= 0);

                if (hasTabs)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Ensures interfaces should be prefixed with capital I.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        public bool EnsureInterfacesShouldBePrefixedWithI()
        {
            EnsureDocumentsInitialized();

            var interfaces = _documents.SelectMany(x => x.GetSyntaxRootAsync().Result.DescendantNodes().OfType<InterfaceDeclarationSyntax>()).ToList();

            return interfaces.All(x => x.Identifier.ToString().StartsWith("I"));
        }

        /// <summary>
        /// Ensures #region are not allowed.
        /// </summary>
        /// <returns><c>True</c> if check succeed.</returns>
        public bool EnsureRegionAreNotAllowed()
        {
            EnsureDocumentsInitialized();

            var regions = _documents.SelectMany(x =>
                x.GetSyntaxRootAsync().Result.DescendantNodesAndTokens().
                Where(n => n.HasLeadingTrivia).SelectMany(n => n.GetLeadingTrivia().
                    Where(t => t.Kind() == SyntaxKind.RegionDirectiveTrivia))).ToList();

            return regions.Count == 0;
        }

        private IEnumerable<string> GetSourcePaths()
        {
            return Directory.GetFiles(Path.GetDirectoryName(_slnPath), "*.cs", SearchOption.AllDirectories);
        }

        private void EnsureDocumentsInitialized()
        {
            if (_documents == null)
            {
                var workspace = MSBuildWorkspace.Create();
                var solution = workspace.OpenSolutionAsync(_slnPath).Result;

                _documents = new List<Document>();

                foreach (var projectId in solution.ProjectIds)
                {
                    var project = solution.GetProject(projectId);
                    foreach (var documentId in project.DocumentIds)
                    {
                        var document = solution.GetDocument(documentId);
                        if (document.SupportsSyntaxTree)
                        {
                            _documents.Add(document);
                        }
                    }
                }
            }
        }
    }
}
