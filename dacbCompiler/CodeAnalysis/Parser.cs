using System.Collections.Generic;

namespace Dacb.CodeAnalysis
{
    class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private List<string> _diagnostics = new List<string>();
        private int _position;

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;

            do 
            {
                token = lexer.NextToken();

                if (token.Kind != SyntaxKind.WhitespaceToken &&
                    token.Kind != SyntaxKind.BadToken )
                {
                    tokens.Add(token);
                }

            } while(token.Kind != SyntaxKind.EndOfFileToken);

            _diagnostics.AddRange(lexer.Diagnostics);
            _tokens = tokens.ToArray();
        }
        private SyntaxToken Current => Peek(0);
        
        public IEnumerable<string> Diagnostics => _diagnostics;
        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length -1];
            
            return _tokens[index];
        }

        private SyntaxToken NextToken() 
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken Match(SyntaxKind kind) 
        {
            if (Current.Kind == kind)
                return NextToken();
            
            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseTerm();
            var eofToken = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(this.Diagnostics, expression, eofToken);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }


        public ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();
            while(Current.Kind == SyntaxKind.PlusToken ||
                  Current.Kind == SyntaxKind.MinusToken )
            {
                var operatorKind = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorKind, right);
            }

            return left;
        }

        public ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();
            while(Current.Kind == SyntaxKind.StarToken ||
                  Current.Kind == SyntaxKind.DivideToken )
            {
                var operatorKind = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorKind, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParanthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var righ = Match(SyntaxKind.CloseParanthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, righ);
            }
            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}