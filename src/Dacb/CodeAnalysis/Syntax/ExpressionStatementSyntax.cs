namespace Dacb.CodeAnalysis.Syntax
{
    public sealed class ExpressionStatementSyntax : StatementSyntax
    {
        //a = 10
        //
        public ExpressionStatementSyntax(ExpressionSyntax expression)
        {
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.ExpressionStatement;
        public ExpressionSyntax Expression { get; }

        
    }
}