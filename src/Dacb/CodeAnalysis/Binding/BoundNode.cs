namespace Dacb.CodeAnalysis.Binding
{
    internal abstract class BoundNode 
    {
        public BoundNode()
        {
        }

        public abstract BoundNodeKind Kind { get; }
    }
    
}