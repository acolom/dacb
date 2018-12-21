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
        VariableExpression,
        AssignmentExpression,
        UnaryExpression,
        LiteralExpression,
        BinaryExpression,
        
    }
    
}