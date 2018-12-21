using Dacb.CodeAnalysis.Binding;

namespace dacbCompiler
{

    internal class Program
    {
        private static void Main()
        {
            var repl = new DacbRepl();
            repl.Run();
        }
    }
}
