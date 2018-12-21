using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Dacb.CodeAnalysis.Binding;
using Dacb.CodeAnalysis.Syntax;

namespace Dacb.CodeAnalysis.Lowering
{
    internal sealed class Lowerer : BoundTreeRewriter
    {
        private Lowerer()
        {
            
        }

        private int _labelCount;

        private LabelSymbol GenerateLabel()
        {
            var name = $"Label_{++_labelCount}";
            return new LabelSymbol(name);
        }
        public static BoundBlockStatement Lower(BoundStatement statement)
        {
            var lowerer = new Lowerer();
            var result = lowerer.RewriteStatement(statement);
            return Flatten(result);
        }

        private static BoundBlockStatement Flatten(BoundStatement statement)
        {
            var builder = ImmutableArray.CreateBuilder<BoundStatement>();
            var stack = new Stack<BoundStatement>();

            stack.Push(statement);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current is BoundBlockStatement b)
                {
                    foreach(var s in b.Statements.Reverse())
                        stack.Push(s);
                }
                else 
                {
                    builder.Add(current);
                }
            }

            return new BoundBlockStatement(builder.ToImmutable());
        }

        protected override BoundStatement RewriteIfStatement(BoundIfStatement node)
        {
            if (node.ElseStatement == null)
            {
                // if <condtion> 
                //      <then>
                //
                // ----->
                // 
                // goToIfFalse <condition> end;
                // <body>
                // end;
                var endLabel = GenerateLabel();
                var goToFalse = new BoundConditionalGoToStatement(endLabel, node.Condition, true);
                var endLabelStatement = new BoundLabelStatement(endLabel);
                
                var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(goToFalse, node.ThenStatement, endLabelStatement));
                return RewriteStatement(result);
            }
            else
            {
                // if <condtion> 
                //      <then>
                // else
                //      <else>
                //
                // ----->
                //
                // goToIfFalse <condition> else;
                // <then>
                // goTo end
                // else;
                // <else>
                // end;

                var elseLabel = GenerateLabel();
                var endLabel = GenerateLabel();
                
                var goToFalse = new BoundConditionalGoToStatement(elseLabel, node.Condition, true);
                var goToEndStatment = new BoundGoToStatement(endLabel);
                var elseLabelStatement = new BoundLabelStatement(elseLabel);
                var endLabelStatement = new BoundLabelStatement(endLabel);

                var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
                    goToFalse, 
                    node.ThenStatement, 
                    goToEndStatment,
                    elseLabelStatement,
                    node.ElseStatement,
                    endLabelStatement)
                );
                return RewriteStatement(result);
            }

        }
        protected override BoundStatement RewriteWhileStatement(BoundWhileStatement node)
        {
            // while <condition>
            //    <body>
            //
            // ------>
            //
            // gotoCheck
            // continue;
            // <body>
            // check
            // gotoTrue <condition> continue;
            // end; 

            var continueLabel = GenerateLabel();
            var checkLabel = GenerateLabel();
            var endLabel = GenerateLabel();
            
            var goToCheck = new BoundGoToStatement(checkLabel);
            var continueLabelStatement = new BoundLabelStatement(continueLabel);
            var checkLabelStatement = new BoundLabelStatement(checkLabel);
            var goToContinue = new BoundConditionalGoToStatement(continueLabel, node.Condition, false);
            var endLabelStatement = new BoundLabelStatement(endLabel);

            var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
                goToCheck,
                continueLabelStatement,
                node.Body,
                checkLabelStatement,
                goToContinue,
                endLabelStatement
                )
            );
            return RewriteStatement(result);

        }
        protected override BoundStatement RewriteForStatement(BoundForStatement node)
        {
            // for <var> = <lower> to <upper>
            //  <body>
            //  
            //  ----->
            // {
            //      var <var> = <lower>
            //      while (<var> <= <upper>)
            //      {
            //          <body>
            //          <var> = <var> + 1    
            //      }
            // }

            var variableDeclaration = new BoundVariableDeclaration(node.Variable, node.LowerBound);
            var variableExpression = new BoundVariableExpression(node.Variable);
            var condition = new BoundBinaryExpression(
                variableExpression, 
                BoundBinaryOperator.Bind(SyntaxKind.LessOrEqualsToken, typeof(int), typeof(int)),
                node.UpperBound
            );

            var increment = new BoundExpressionStatement(
                new BoundAssignmentExpression(
                    node.Variable, 
                    new BoundBinaryExpression(
                        variableExpression,
                        BoundBinaryOperator.Bind(SyntaxKind.PlusToken, typeof(int), typeof(int)),
                        new BoundLiteralExpression(1)
                    )
                )
            );
            
            var whileBody = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(node.Body, increment) );
            var whileStatement = new BoundWhileStatement(condition, whileBody);

            var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(variableDeclaration, whileStatement) );
            return RewriteStatement(result);
        }

    }
}