using System;
using Dacb.CodeAnalysis.Symbols;

namespace Dacb.CodeAnalysis.Binding
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {
        public BoundAssignmentExpression(VariableSymbol variable, BoundExpression expresion)
        {
            Variable = variable;
            Expression = expresion;
        }
        
        public override TypeSymbol Type => Expression.Type;
        public VariableSymbol Variable { get; }
        public BoundExpression Expression { get; }
        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    }
}