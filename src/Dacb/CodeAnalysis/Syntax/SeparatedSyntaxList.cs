using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Dacb.CodeAnalysis.Syntax
{
    // print("heloo")
    // add(1,2)
    public abstract class SeparatedSyntaxList
    {
        public abstract ImmutableArray<SyntaxNode> GetWithSeparators();
    }
    public sealed class SeparatedSyntaxList<T> : SeparatedSyntaxList, IEnumerable<T>
        where T : SyntaxNode
    {
        private readonly ImmutableArray<SyntaxNode> _nodeAndSeparator;

        public SeparatedSyntaxList(ImmutableArray<SyntaxNode> nodeAndSeparator)
        {
            _nodeAndSeparator = nodeAndSeparator;
        }

        public int Count => (_nodeAndSeparator.Length + 1) / 2;
        public T this[int index] => (T)_nodeAndSeparator[index * 2];

        public SyntaxToken GetSeparator(int index) 
        {
            if (index == Count - 1)
                return null;
            return (SyntaxToken)_nodeAndSeparator[(index * 2) + 1];
        } 

        public override ImmutableArray<SyntaxNode> GetWithSeparators() => _nodeAndSeparator;
        public IEnumerator<T> GetEnumerator()
        {
            for(var i =0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}