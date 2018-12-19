namespace Dacb.CodeAnalysis.Binding
{
    internal sealed class BoundWhileStatement : BoundStatement
    {
        public BoundWhileStatement(BoundExpression condition, BoundStatement bodyStatement)
        {
            Condition = condition;
            BodyStatement = bodyStatement;
        }

        public BoundExpression Condition { get; }
        public BoundStatement BodyStatement { get; }

        public override BoundNodeKind Kind => BoundNodeKind.WhileStatement;
    }
}