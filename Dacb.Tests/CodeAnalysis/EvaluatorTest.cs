
using System.Collections.Generic;
using Dacb.CodeAnalysis;
using Dacb.CodeAnalysis.Syntax;
using Xunit;

namespace Dacb.Tests.CodeAnalysis
{
    public class EvaluationTests
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("-1", -1)]
        [InlineData("+1", 1)]
        [InlineData("14 + 12", 26)]
        [InlineData("12 - 3", 9)]
        [InlineData("4 * 2", 8)]
        [InlineData("9 / 3", 3)]
        [InlineData("-1", -1)]
        [InlineData("(10)", 10)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("!true", false)]
        [InlineData("!false", true)]
        public void SyntaxFacts_RoundTrips(string text, object expectedValue) 
        {
            var syntaxTree = SyntaxTree.Parse(text);
            var compilation = new Compilation(syntaxTree);
            var variables = new Dictionary<VariableSymbol, object>();

            var actualResult = compilation.Evaluate(variables);
            
            Assert.Empty(actualResult.Diagnostics);
            Assert.Equal(expectedValue,actualResult.Value);
        }

    }  
}