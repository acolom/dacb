using System;
using Dacb.CodeAnalysis.Symbols;

namespace Dacb.CodeAnalysis.Binding
{
    internal abstract class BoundExpression : BoundNode 
    {
        public abstract TypeSymbol Type { get; }
    }
}