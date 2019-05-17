namespace Dacb.CodeAnalysis.Syntax
{
    internal sealed class DoWhileStatementSyntax : StatementSyntax
    {
        public DoWhileStatementSyntax(SyntaxToken doKeyword, StatementSyntax bodyStatement, SyntaxToken whileKeyword, ExpressionSyntax condition)
        {
            DoKeyword = doKeyword;
            BodyStatement = bodyStatement;
            WhileKeyword = whileKeyword;
            Condition = condition;
        }
        public override SyntaxKind Kind => SyntaxKind.DoWhileStatement;
        public SyntaxToken DoKeyword { get; }
        public StatementSyntax BodyStatement { get; }
        public SyntaxToken WhileKeyword { get; }
        public ExpressionSyntax Condition { get; }

        
    }
}