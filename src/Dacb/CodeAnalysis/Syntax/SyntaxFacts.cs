using System;
using System.Collections.Generic;

namespace Dacb.CodeAnalysis.Syntax
{
    public static class SyntaxFacts 
    {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch(kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.BangToken:
                case SyntaxKind.TildeToken:
                    return 8;
                default:
                    return 0;
            }
        }
        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch(kind)
            {
                case SyntaxKind.SlashToken:
                    return 7;
                case SyntaxKind.PercentageToken:
                    return 6;
                case SyntaxKind.StarToken:
                    return 5;
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;
                case SyntaxKind.EqualsEqualsToken:
                case SyntaxKind.BangEqualsToken:
                case SyntaxKind.LessToken:
                case SyntaxKind.LessOrEqualsToken:
                case SyntaxKind.GreaterToken:
                case SyntaxKind.GreaterOrEqualsToken:
                    return 3;
                case SyntaxKind.AmpersandToken:
                case SyntaxKind.AmpersandAmpersandToken:
                    return 2;
                case SyntaxKind.PipeToken:
                case SyntaxKind.PipePipeToken:
                case SyntaxKind.HatToken:
                    return 1;
                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch(text)
            {
                case "else":
                    return SyntaxKind.ElseKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                case "for":
                    return SyntaxKind.ForKeyword;
                case "if":
                    return SyntaxKind.IfKeyword;
                case "let":
                    return SyntaxKind.LetKeyword;
                case "to":
                    return SyntaxKind.ToKeyword;
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "var":
                    return SyntaxKind.VarKeyword;
                case "while":
                    return SyntaxKind.WhileKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }

        public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));

            foreach(var kind in kinds)
            {
                if (GetBinaryOperatorPrecedence(kind) > 0)
                    yield return kind;
            }
        }

        public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));

            foreach(var kind in kinds)
            {
                if (GetUnaryOperatorPrecedence(kind) > 0)
                    yield return kind;
            }
        }

        public static string GetText(SyntaxKind kind)
        {
            switch(kind)
            {
                case SyntaxKind.PlusToken: 
                    return  "+";
                case SyntaxKind.MinusToken: 
                    return  "-";
                case SyntaxKind.StarToken: 
                    return  "*";
                case SyntaxKind.SlashToken: 
                    return  "/";
                case SyntaxKind.PercentageToken: 
                    return  "%";
                case SyntaxKind.BangToken: 
                    return  "!";
                case SyntaxKind.EqualsToken: 
                    return  "=";
                case SyntaxKind.AmpersandToken: 
                    return  "&";
                case SyntaxKind.AmpersandAmpersandToken: 
                    return  "&&";
                case SyntaxKind.PipeToken: 
                    return  "|";
                case SyntaxKind.PipePipeToken: 
                    return  "||";
                case SyntaxKind.TildeToken: 
                    return  "~";
                case SyntaxKind.HatToken: 
                    return  "^";
                case SyntaxKind.EqualsEqualsToken: 
                    return  "==";
                case SyntaxKind.BangEqualsToken: 
                    return  "!=";
                case SyntaxKind.LessToken:
                    return "<";
                case SyntaxKind.LessOrEqualsToken:
                    return "<=";
                case SyntaxKind.GreaterToken:
                    return ">";
                case SyntaxKind.GreaterOrEqualsToken:
                    return ">=";
                case SyntaxKind.OpenParanthesisToken: 
                    return  "(";
                case SyntaxKind.CloseParanthesisToken: 
                    return  ")";
                case SyntaxKind.OpenBraceToken: 
                    return  "{";
                case SyntaxKind.CloseBraceToken: 
                    return  "}";
                case SyntaxKind.IfKeyword:
                    return  "if";
                case SyntaxKind.ElseKeyword:
                    return  "else";
                 case SyntaxKind.WhileKeyword:
                    return  "while";
                case SyntaxKind.ForKeyword:
                    return  "for";
                case SyntaxKind.ToKeyword:
                    return  "to";
                case SyntaxKind.TrueKeyword:
                    return  "true";
                case SyntaxKind.FalseKeyword:
                    return  "false";
                case SyntaxKind.LetKeyword:
                    return  "let";
                case SyntaxKind.VarKeyword:
                    return  "var";
                default:
                    return null;
            }
        }
    }
}