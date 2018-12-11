using System;

namespace Dacb.CodeAnalysis.Binding
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {
        public BoundAssignmentExpression(VariableSymbol variable, BoundExpression expresion)
        {
            Variable = variable;
            Expresion = expresion;
        }
        
        public VariableSymbol Variable { get; }
        public BoundExpression Expresion { get; }
        public override Type Type => Expresion.Type;
        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    }
}