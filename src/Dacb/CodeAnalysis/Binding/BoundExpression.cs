using System;

namespace Dacb.CodeAnalysis.Binding
{
    internal abstract class BoundExpression : BoundNode 
    {
        public BoundExpression()
        {
        }

        public abstract Type Type { get; }
    }


    
}