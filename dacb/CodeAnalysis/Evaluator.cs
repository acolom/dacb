using System;
using System.Collections.Generic;
using Dacb.CodeAnalysis.Binding;

namespace Dacb.CodeAnalysis
{

    internal sealed class Evaluator
    {
        private readonly BoundExpression _root;
        private readonly Dictionary<VariableSymbol, object> _variables;

        public Evaluator(BoundExpression root, Dictionary<VariableSymbol, object> variables)
        {
            _root = root;
            _variables = variables;
        }

        public object Evaluate()
        {
            return EvaluateExression(_root);
        }

        private object EvaluateExression(BoundExpression node)
        {
            if (node is BoundLiteralExpression)
            {
                return EvaluateLiteralExpression((BoundLiteralExpression)node);
            }

            else if (node is BoundVariableExpression)
            {
                return EvaluateVariableExpression((BoundVariableExpression)node);
            }

            else if (node is BoundAssignmentExpression)
            {
                return EvaluateAssignmentExpression((BoundAssignmentExpression)node);
            }

            else if (node is BoundUnaryExpression)
            {
                return EvaluateUnaryExpression((BoundUnaryExpression)node);

            }
            else if (node is BoundBinaryExpression)
            {
                return EvaluateBinaryExpression((BoundBinaryExpression)node);
            }
            else 
            {
                throw new Exception($"Unexpected node: {node.Kind}");
            }
            
        }

        private static object EvaluateLiteralExpression(BoundLiteralExpression n)
        {
            return n.Value;
        }
        private object EvaluateVariableExpression(BoundVariableExpression v)
        {
            return _variables[v.Variable];
        }
        private object EvaluateAssignmentExpression(BoundAssignmentExpression a)
        {
            var value = EvaluateExression(a.Expresion);
            _variables[a.Variable] = value;
            return value;
        }
        private object EvaluateUnaryExpression(BoundUnaryExpression u)
        {
            var operand = EvaluateExression(u.Operand);
            switch (u.Op.Kind)
            {
                case BoundUnaryOperatorKind.Identity:
                    return (int)operand;
                case BoundUnaryOperatorKind.Negation:
                    return -(int)operand;
                case BoundUnaryOperatorKind.LogicalNegation:
                    return !(bool)operand;

                default:
                    throw new Exception($"Unexpected unary operator '{u.Op}'");
            }
        }
        private object EvaluateBinaryExpression(BoundBinaryExpression b)
        {
            var left = EvaluateExression(b.Left);
            var right = EvaluateExression(b.Right);
            switch (b.Op.Kind)
            {
                case BoundBinaryOperatorKind.Addition:
                    return (int)left + (int)right;
                case BoundBinaryOperatorKind.Substraction:
                    return (int)left - (int)right;
                case BoundBinaryOperatorKind.Multiplication:
                    return (int)left * (int)right;
                case BoundBinaryOperatorKind.Division:
                    return (int)left / (int)right;
                case BoundBinaryOperatorKind.LogicalAnd:
                    return (bool)left && (bool)right;
                case BoundBinaryOperatorKind.LogicalOr:
                    return (bool)left || (bool)right;
                case BoundBinaryOperatorKind.Equals:
                    return Equals(left, right);
                case BoundBinaryOperatorKind.NotEquals:
                    return !Equals(left, right);
                default:
                    throw new Exception($"Unexpected operator: {b.Op.Kind}");
            }
        }
    }
}