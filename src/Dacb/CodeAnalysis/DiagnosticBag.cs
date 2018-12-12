using System;
using System.Collections;
using System.Collections.Generic;
using Dacb.CodeAnalysis.Syntax;
using Dacb.CodeAnalysis.Text;

namespace Dacb.CodeAnalysis
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public IEnumerator<Diagnostic> GetEnumerator() =>  _diagnostics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics);
        }

        private void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diagnostics.Add(diagnostic);
        }

        public void ReportInvalidNumber(TextSpan span, string text, Type type)
        {
            var message =  $"The number '{text}' is not a valid {type}";
            Report(span, message);
        }

        public void ReportBadCharacter(int postion, char character)
        {
            var span = new TextSpan(postion, 1);
            var message = $"ERROR: Bad character input: '{character}'";
            Report(span, message);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
        {
            var message = $"ERROR: Unexpected token <{actualKind}>, expected <{expectedKind}>";
            Report(span, message);
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
        {
            var message =  $"Unary operator '{operatorText}' is not defined for types {operandType}.";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
        {
            var message = $"Binary operator '{operatorText}' is not defined for types {leftType} and {rightType}.";
            Report(span, message);
        }

        public void ReportUndefinedName(TextSpan span, string name)
        {
            var message = $"Variable '{name}' does not exists.";
            Report(span, message);
        }

        public void ReportVariableAlreadyDeclared(TextSpan span, string name)
        {
            var message = $"Variable {name} is already declared.";
            Report(span, message);
        }

        public void ReportCannotConvert(TextSpan span, Type fromType, Type toType)
        {
            var message = $"Cannot convert type '{fromType}' to type '{toType}'.";
            Report(span, message);
        }

        internal void ReportCannotAssign(TextSpan span, string name)
        {
            var message = $"Variable {name} is read-only and cannot be assigned to.";
            Report(span, message);
        }
    }
}