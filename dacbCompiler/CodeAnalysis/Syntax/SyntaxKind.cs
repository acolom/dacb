namespace Dacb.CodeAnalysis.Syntax
{
    public enum SyntaxKind 
    {
        //tokens
        BadToken,
        EndOfFileToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParanthesisToken,
        CloseParanthesisToken,
        WhitespaceToken,
        IdentifierToken,
        BangToken,
        AmpsersandAmpsersandToken,
        PipePipeToken,

        //keywords
        TrueKeyword,
        FalseKeyword,


        //Expressions     
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        
    }
}