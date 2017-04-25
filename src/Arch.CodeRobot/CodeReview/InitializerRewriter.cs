// Copyright (c) yingtingxu(徐应庭). All rights reserved

using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Arch.CodeRobot
{
    public class InitializerRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;

        public InitializerRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (trivia.SpanStart == 0 && trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                return SyntaxTrivia(SyntaxKind.SingleLineCommentTrivia, "// Copyright (c) yingtingxu(徐应庭). All rights reserved");
            }
            return base.VisitTrivia(trivia);
        }

        public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            var changed = false;

            // you could declare more than one variable with one expression
            var vs = node.Variables;

            // we create a space to improve readability
            var space = SyntaxTrivia(SyntaxKind.WhitespaceTrivia, " ");

            for (var i = 0; i < node.Variables.Count; i++)
            {
                // there is not an initialization or there is an initialization but it's not to 411
                if (_semanticModel.GetSymbolInfo(node.Type).Symbol?.ToString() == "int" &&
                    (node.Variables[i].Initializer == null || (node.Variables[i].Initializer != null
                    &&
                    !node.Variables[i].Initializer.Value.IsEquivalentTo(ParseExpression("411")))
                    ))
                {
                    // we create a new expression "411"
                    var es = ParseExpression("411").WithLeadingTrivia(space);

                    // basically we create an assignment to the expression we just created
                    var evc = EqualsValueClause(es).WithLeadingTrivia(space);

                    // we replace the null initializer with ours
                    vs = vs.Replace(vs.ElementAt(i), vs.ElementAt(i).WithInitializer(evc));

                    changed = true;
                }
            }

            if (changed)
                return node.WithVariables(vs);

            return base.VisitVariableDeclaration(node);
        }
    }
}
