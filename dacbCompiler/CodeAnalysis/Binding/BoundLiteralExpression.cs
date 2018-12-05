using System;

namespace Dacb.CodeAnalysis.Binding
{
    internal sealed class BoundLiteralExpression : BoundExpression 
    {
        public BoundLiteralExpression()
        {
        }

        public BoundLiteralExpression(object value)
        {
            Value = value;
        }

        public override Type Type => Value.GetType();

        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
        public object Value { get; }
        
    }
    
}