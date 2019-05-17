namespace Dacb.CodeAnalysis.Binding
{
    internal enum BoundNodeKind
    {
        //statements
        BlockStatement,
        ExpressionStatement,
        IfStatement,
        WhileStatement,
        DoWhileStatement,
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
        ConversionExpression,       
    }
    
}