namespace Dacb.CodeAnalysis.Binding
{
    internal enum BoundNodeKind
    {
        //statements
        BlockStatement,
        ExpressionStatement,
        IfStatement,
        WhileStatement,
        VariableDeclaration,

        //expressions
        VariableExpression,
        AssignmentExpression,
        UnaryExpression,
        LiteralExpression,
        BinaryExpression,
        
    }
    
}