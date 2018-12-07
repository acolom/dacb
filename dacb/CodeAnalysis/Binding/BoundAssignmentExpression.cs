using System;

namespace Dacb.CodeAnalysis.Binding
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {

        public BoundAssignmentExpression(string name, BoundExpression expresion)
        {
            Name = name;
            Expresion = expresion;
        }

        public string Name { get; }
        public BoundExpression Expresion { get; }

        public override Type Type => Expresion.Type;

        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    }
}