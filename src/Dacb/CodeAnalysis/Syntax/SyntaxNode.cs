using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Dacb.CodeAnalysis.Text;

namespace Dacb.CodeAnalysis.Syntax
{
    public abstract class SyntaxNode 
    {
        public abstract SyntaxKind Kind { get; }

        public virtual TextSpan Span
        {
            get 
            {
                var first = GetChildren().First().Span;
                var last = GetChildren().Last().Span;
                return TextSpan.FromBounds(first.Start, last.End);
            }
        }
        public IEnumerable<SyntaxNode> GetChildren()
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var property in properties)
            {
                if (typeof(SyntaxNode).IsAssignableFrom(property.PropertyType))
                {
                    yield return (SyntaxNode)property.GetValue(this);
                }
                else if (typeof(IEnumerable<SyntaxNode>).IsAssignableFrom(property.PropertyType))
                {
                    var children = (IEnumerable<SyntaxNode>)property.GetValue(this);
                    foreach(var child in children)
                        yield return child;
                }
            }
        }

        public void WriteTo(TextWriter textWriter)
        {
            PrettyPrint(textWriter, this);
        }

        private static void PrettyPrint(TextWriter writer, SyntaxNode node, string indent = "", bool isLast = true)
        {
            // ├──
            // │
            // └──
            var isToConsole = writer == Console.Out;

            var marker = isLast ? "└──" : "├──";
            
            if (isToConsole)
                Console.ForegroundColor = ConsoleColor.DarkGray;
            
            writer.Write(indent);    
            writer.Write(marker);
            
             if (isToConsole)
                Console.ForegroundColor = node is SyntaxToken ? ConsoleColor.Blue : ConsoleColor.Cyan;

            writer.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                writer.Write(" ");
                writer.Write(t.Value);
            }
            if (isToConsole)
                Console.ResetColor();

            writer.WriteLine();
            
            indent += isLast ? "   " : "│  ";
            
            var lastChild = node.GetChildren().LastOrDefault();
            foreach(var child in node.GetChildren())
                PrettyPrint(writer, child, indent, lastChild == child);
        }

        public override string ToString() 
        {
            using(var writer =  new StringWriter())
            {
                this.WriteTo(writer);
                return writer.ToString();
            }
        }
    }
}