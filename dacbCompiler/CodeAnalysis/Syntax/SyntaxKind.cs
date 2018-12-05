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

        //Expressions     
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        
    }
}