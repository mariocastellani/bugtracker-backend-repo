using System.Reflection;

namespace SharedKernel.DependencyInjection
{
    internal static class AssemblyHelper
    {
        private readonly static string[] _excludedNames;

        static AssemblyHelper()
        {
            _excludedNames = new[] 
            { 
                "System.", "Microsoft.", "netstandar", "MediatR", "Serilog", "Swashbuckle", "SharedKernel" 
            };
        }

        public static IEnumerable<Assembly> GetApplicationAssemblies()
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(Assembly.GetEntryAssembly());

            do
            {
                var assembly = stack.Pop();

                yield return assembly;

                var references = assembly.GetReferencedAssemblies()
                    .Where(x => !_excludedNames.Any(name => x.FullName.Contains(name)));

                foreach (var reference in references)
                    if (!list.Contains(reference.FullName))
                    {
                        stack.Push(Assembly.Load(reference));
                        list.Add(reference.FullName);
                    }
            }
            while (stack.Count > 0);
        }
    }
}