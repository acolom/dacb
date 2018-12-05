using System;
using Dacb.CodeAnalysis.Syntax;

namespace Dacb.CodeAnalysis
{
    public sealed class Evaluator
    {
        public Evaluator(ExpressionSyntax root)
        {
            Root = root;
        }

        public ExpressionSyntax Root { get; }

        public int Evaluate()
        {
            return EvaluateExression(Root);
        }

        private int EvaluateExression(ExpressionSyntax node)
        {
            if (node is LiteralExpressionSyntax n)
            {
                return (int)n.LiteralToken.Value;
            }
                
            if (node is UnaryExpressionSyntax u)
            {
                var operand = EvaluateExression(u.Operand);
                if (u.OperatorToken.Kind == SyntaxKind.MinusToken)
                    return -operand;
                else if (u.OperatorToken.Kind == SyntaxKind.PlusToken)
                    return operand;
                else    
                    throw new Exception($"Unexpected unary operator '{u.OperatorToken.Kind}'");

            }
            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExression(b.Left);
                var right = EvaluateExression(b.Right);
                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                    return left + right;
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                    return left - right;
                else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
                    return left * right;
                else if (b.OperatorToken.Kind == SyntaxKind.SlashToken)
                    return left / right;
                else 
                    throw new Exception($"Unexpected operator: {b.OperatorToken.Kind}");
            }

            if (node is ParenthesizedExpressionSyntax p)
                return EvaluateExression(p.Expression);

            throw new Exception($"Unexpected node: {node.Kind}");
        }
    }
}