namespace Dacb.CodeAnalysis.Syntax
{
    public enum SyntaxKind 
    {

        //tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        StringToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        PercentageToken,
        BangToken,
        EqualsToken,
        AmpersandToken,
        AmpersandAmpersandToken,
        PipeToken,
        PipePipeToken,
        TildeToken,
        HatToken,
        EqualsEqualsToken,
        BangEqualsToken,
        LessToken,
        LessOrEqualsToken,
        GreaterToken,
        GreaterOrEqualsToken,
        OpenParanthesisToken,
        CloseParanthesisToken,
        OpenBraceToken,
        CloseBraceToken,
        IdentifierToken,
        ColonToken,
        CommaToken,
       
        //keywords
        DoKeyword,
        ElseKeyword,
        FalseKeyword,
        ForKeyword,
        IfKeyword,
        LetKeyword,
        ToKeyword,
        TrueKeyword,
        VarKeyword,      
        WhileKeyword,

        
        //Nodes
        CompilationUnit,
        ElseClause,
        TypeClause,

        //Statements
        BlockStatement,
        VariableDeclaration,
        ExpressionStatement,
        IfStatement,
        WhileStatement,
        DoWhileStatement,
        ForStatement,
        
        //Expressions     
        LiteralExpression,
        NameExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        AssignmentExpression,
        CallExpression,
        
    }
}