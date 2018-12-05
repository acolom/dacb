using System;
using Dacb.CodeAnalysis.Binding;
using Dacb.CodeAnalysis.Syntax;

namespace Dacb.CodeAnalysis
{
    internal sealed class Evaluator
    {
        private readonly BoundExpression _root;

        public Evaluator(BoundExpression root)
        {
            _root = root;
        }

        

        public int Evaluate()
        {
            return EvaluateExression(_root);
        }

        private int EvaluateExression(BoundExpression node)
        {
            if (node is BoundLiteralExpression n)
            {
                return (int)n.Value;
            }
                
            if (node is BoundUnaryExpression u)
            {
                var operand = EvaluateExression(u.Operand);
                switch (u.OperatorKind)
                {
                    case BoundUnaryOperatorKind.Negation:
                        return -operand;
                    case BoundUnaryOperatorKind.Identity:
                        return operand;
                    default:
                        throw new Exception($"Unexpected unary operator '{u.OperatorKind}'");
                }

            }
            if (node is BoundBinaryExpression b)
            {
                var left = EvaluateExression(b.Left);
                var right = EvaluateExression(b.Right);
                switch (b.OperatorKind)
                {
                    case BoundBinaryOperatorKind.Addition:
                        return left + right;
                    case BoundBinaryOperatorKind.Substraction:
                        return left - right;
                    case BoundBinaryOperatorKind.Multiplication:
                        return left * right;
                    case BoundBinaryOperatorKind.Division:
                        return left / right;
                    default:
                        throw new Exception($"Unexpected operator: {b.OperatorKind}");
                }
            }
            throw new Exception($"Unexpected node: {node.Kind}");
        }
    }
}