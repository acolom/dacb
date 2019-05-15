namespace Dacb.CodeAnalysis.Symbols
{
    public sealed class ParameterSymbol : VariableSymbol
    {
        public ParameterSymbol(string name, TypeSymbol type) 
            : base(name, isReadOnly: true, type)
        {
        }

        internal ParameterSymbol(string name, bool isReadOnly, TypeSymbol type) : base(name, isReadOnly, type)
        {
        }

        public override SymbolKind Kind => SymbolKind.Parameter;
    }
}