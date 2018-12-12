namespace Dacb.CodeAnalysis.Syntax
{
    public enum SyntaxKind 
    {
        //tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        BangToken,
        EqualsToken,
        AmpsersandAmpsersandToken,
        PipePipeToken,
        EqualsEqualsToken,
        BangEqualsToken,
        OpenParanthesisToken,
        CloseParanthesisToken,
        OpenBraceToken,
        CloseBraceToken,
        IdentifierToken,
       
        //keywords
        TrueKeyword,
        FalseKeyword,
        LetKeyword,
        VarKeyword,

        //Nodes
        CompilationUnit,

        //Statements
        BlockStatement,
        VariableDeclaration,
        ExpressionStatement,
        

        //Expressions     
        LiteralExpression,
        NameExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        AssignmentExpression,
        
    }
}