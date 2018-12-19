﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dacb.CodeAnalysis.Syntax;
using Xunit;

namespace Dacb.Tests.CodeAnalysis.Syntax
{
    public class LexerTests
    {
        [Fact]
        public void Lexer_Lexes_AllTokens()
        {
            var tokenKinds = Enum.GetValues(typeof(SyntaxKind))
                                .Cast<SyntaxKind>()
                                .Where(k => k.ToString().EndsWith("Keyword") ||
                                            k.ToString().EndsWith("Token"));
                                

            var testedTokenKinds = GetTokens().Concat(GetSeparators()).Select(t => t.kind);
            
            var untestTokenKinds = new SortedSet<SyntaxKind>(tokenKinds);
            untestTokenKinds.Remove(SyntaxKind.BadToken);
            untestTokenKinds.Remove(SyntaxKind.EndOfFileToken);
            untestTokenKinds.ExceptWith(testedTokenKinds);

            Assert.Empty(untestTokenKinds);
        }

        [Theory]
        [MemberData(nameof(GetTokensData))]
        public void Lexer_Lexes_Token(SyntaxKind kind, string text)
        {
            var tokens = SyntaxTree.ParseTokens(text);

            var token = Assert.Single(tokens);

            Assert.Equal(kind, token.Kind);
            Assert.Equal(text, token.Text);
        }

        [Theory]
        [MemberData(nameof(GetTokenPairsData))]
        public void Lexer_Lexes_Token_Pairs(SyntaxKind t1Kind, string t1Text, SyntaxKind t2Kind, string t2Text)
        {
            var tokens = SyntaxTree.ParseTokens($"{t1Text}{t2Text}").ToArray();

            Assert.Equal(2, tokens.Length);
            
            Assert.Equal(t1Kind, tokens[0].Kind);
            Assert.Equal(t1Text, tokens[0].Text);
            Assert.Equal(t2Kind, tokens[1].Kind);
            Assert.Equal(t2Text, tokens[1].Text);
        }

        [Theory]
        [MemberData(nameof(GetTokenPairsWithSeparatorData))]
        public void Lexer_Lexes_Token_Pairs_With_Separators(SyntaxKind t1Kind, string t1Text, 
                                                           SyntaxKind separatorKind, string separatorText, 
                                                           SyntaxKind t2Kind, string t2Text)
        {
            var tokens = SyntaxTree.ParseTokens($"{t1Text}{separatorText}{t2Text}").ToArray();

            Assert.Equal(3, tokens.Length);

            
            Assert.Equal(t1Kind, tokens[0].Kind);
            Assert.Equal(t1Text, tokens[0].Text);
            Assert.Equal(separatorText, tokens[1].Text);
            Assert.Equal(separatorKind, tokens[1].Kind);
            Assert.Equal(t2Kind, tokens[2].Kind);
            Assert.Equal(t2Text, tokens[2].Text);
        }

        public static IEnumerable<object[]> GetTokensData()
        {
            foreach(var t in GetTokens().Concat(GetSeparators()))
                yield return new object[] { t.kind, t.text };
        }

        public static IEnumerable<object[]> GetTokenPairsData()
        {
            foreach(var t in GetTokenPairs())
                yield return new object[] { t.t1Kind, t.t1Text, t.t2Kind, t.t2Text };
        }

        public static IEnumerable<object[]> GetTokenPairsWithSeparatorData()
        {
            foreach(var t in GetTokenPairsWhithSeparator())
                yield return new object[] { t.t1Kind, t.t1Text, t.separatorKind, t.separatorText, t.t2Kind, t.t2Text };
        }

        private static IEnumerable<(SyntaxKind kind, string text)> GetTokens()
        {
            var fixedTokens = Enum.GetValues(typeof(SyntaxKind))
                                .Cast<SyntaxKind>()
                                .Select(k => (kind: k, text: SyntaxFacts.GetText(k)))
                                .Where(t => t.text != null);

            var dynamicTokens = new []
            {
                (SyntaxKind.NumberToken, "1"),
                (SyntaxKind.NumberToken, "234"),
                (SyntaxKind.IdentifierToken, "a"),
                (SyntaxKind.IdentifierToken, "abc"),
            };
            return fixedTokens.Concat(dynamicTokens);
        }

        private static IEnumerable<(SyntaxKind kind, string text)> GetSeparators()
        {
            return new []
            {
                (SyntaxKind.WhitespaceToken, " "),
                (SyntaxKind.WhitespaceToken, "   "),
                (SyntaxKind.WhitespaceToken, "\r"),
                (SyntaxKind.WhitespaceToken, "\n"),
                (SyntaxKind.WhitespaceToken, "\r\n"),
            };
        }
    
        private static bool RequieresSeparator(SyntaxKind t1Kind, SyntaxKind t2Kind)
        {
            var t1IsKeyword = t1Kind.ToString().EndsWith("Keyword");
            var t2IsKeyword = t2Kind.ToString().EndsWith("Keyword");

            if (t1IsKeyword && t2IsKeyword)
                return true;
            if (t1IsKeyword && t2Kind == SyntaxKind.IdentifierToken)
                return true;
            if (t1Kind == SyntaxKind.IdentifierToken && t2IsKeyword)
                return true;

            if (t1Kind == SyntaxKind.IdentifierToken && t2Kind == SyntaxKind.IdentifierToken)
                return true;
            
            if (t1Kind == SyntaxKind.NumberToken && t2Kind == SyntaxKind.NumberToken)
                return true;
            
            if (t1Kind == SyntaxKind.BangToken && t2Kind == SyntaxKind.EqualsToken)
                return true;
            
            if (t1Kind == SyntaxKind.BangToken && t2Kind == SyntaxKind.EqualsEqualsToken)
                return true;

            if (t1Kind == SyntaxKind.EqualsToken && t2Kind == SyntaxKind.EqualsToken)
                return true;

            if (t1Kind == SyntaxKind.EqualsToken && t2Kind == SyntaxKind.EqualsEqualsToken)
                return true;

            if (t1Kind == SyntaxKind.LessToken && t2Kind == SyntaxKind.EqualsToken)
                return true;
            
            if (t1Kind == SyntaxKind.LessToken && t2Kind == SyntaxKind.EqualsEqualsToken)
                return true;

            if (t1Kind == SyntaxKind.GreaterToken && t2Kind == SyntaxKind.EqualsToken)
                return true;
            
            if (t1Kind == SyntaxKind.GreaterToken && t2Kind == SyntaxKind.EqualsEqualsToken)
                return true;


            return false;
        }
        private static IEnumerable<(SyntaxKind t1Kind, string t1Text, SyntaxKind t2Kind, string t2Text)> GetTokenPairs()
        {
            foreach(var t1 in GetTokens())
            {
                foreach(var t2 in GetTokens())
                {
                    if (!RequieresSeparator(t1.kind, t2.kind))
                        yield return (t1.kind, t1.text, t2.kind, t2.text);
                }
            }
        }

        private static IEnumerable<(SyntaxKind t1Kind, string t1Text, SyntaxKind separatorKind, string separatorText, SyntaxKind t2Kind, string t2Text)> GetTokenPairsWhithSeparator()
        {
            foreach(var t1 in GetTokens())
            {
                foreach(var t2 in GetTokens())
                {
                    if (RequieresSeparator(t1.kind, t2.kind))
                    {
                        foreach(var separator in GetSeparators())
                        {
                            yield return (t1.kind, t1.text, separator.kind, separator.text,  t2.kind, t2.text);
                        }
                    }
                        
                }
            }
        }

    }
}
