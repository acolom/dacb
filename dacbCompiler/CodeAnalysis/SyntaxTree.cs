using System.Collections.Generic;
using System.Linq;

namespace Dacb.CodeAnalysis
{
    sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
        }
        blic IReadOnlyList<string> Diagnostics { get; }

        public SyntaxToken EndOfFileToken { get; }
        
        public ExpressionSyntax Root { get; }
        
        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}