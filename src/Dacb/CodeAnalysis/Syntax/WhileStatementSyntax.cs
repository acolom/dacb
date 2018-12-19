namespace Dacb.CodeAnalysis.Syntax
{
    internal sealed class WhileStatementSyntax : StatementSyntax
    {
        public WhileStatementSyntax(SyntaxToken whileKeyword, ExpressionSyntax condition, StatementSyntax bodyStatement)
        {
            WhileKeyword = whileKeyword;
            Condition = condition;
            BodyStatement = bodyStatement;
        }

        public override SyntaxKind Kind => SyntaxKind.WhileStatement;

        public SyntaxToken WhileKeyword { get; }
        public ExpressionSyntax Condition { get; }
        public StatementSyntax BodyStatement { get; }

        
    }
}