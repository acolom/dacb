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
        VariableDeclaration,

        //expressions
        VariableExpression,
        AssignmentExpression,
        UnaryExpression,
        LiteralExpression,
        BinaryExpression,
        
    }
    
}