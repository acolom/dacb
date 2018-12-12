namespace Dacb.CodeAnalysis.Binding
{
    internal enum BoundNodeKind
    {
        //statements
        BlockStatement,
        ExpressionStatement,
        VariableDeclaration,

        //expressions
        VariableExpression,
        AssignmentExpression,
        UnaryExpression,
        LiteralExpression,
        BinaryExpression,
        
    }
    
}