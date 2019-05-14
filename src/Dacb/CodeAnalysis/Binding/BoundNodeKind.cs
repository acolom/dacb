namespace Dacb.CodeAnalysis.Binding
{
    internal enum BoundNodeKind
    {
        //statements
        BlockStatement,
        ExpressionStatement,
        IfStatement,
        WhileStatement,
        ForStatement,
        GoToStatement,
        LabelStatement,
        ConditionalGoToStatement,
        VariableDeclaration,

        //expressions
        ErrorExpression,

        VariableExpression,
        AssignmentExpression,
        UnaryExpression,
        LiteralExpression,
        BinaryExpression,
        CallExpression,
    }
    
}