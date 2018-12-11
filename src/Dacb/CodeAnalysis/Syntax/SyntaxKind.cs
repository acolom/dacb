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
        IdentifierToken,
        //keywords
        TrueKeyword,
        FalseKeyword,


        //Expressions     
        LiteralExpression,
        NameExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        AssignmentExpression,
        
    }
}