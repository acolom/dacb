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

        public static BoundStatement Lower(BoundStatement statement)
        {
            var lowerer = new Lowerer();
            return lowerer.RewriteStatement(statement);
        }
    }
}