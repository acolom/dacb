
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
        [InlineData("(10)", 10)]
        [InlineData("12 == 12", true)]
        [InlineData("12 == 3", false)]
        [InlineData("12 != 12", false)]
        [InlineData("12 != 3", true)]
        [InlineData("false == false", true)]
        [InlineData("true == false", false)]
        [InlineData("false != false", false)]
        [InlineData("true != false", true)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("!true", false)]
        [InlineData("!false", true)]
        [InlineData("(a = 10) * a", 100)]
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