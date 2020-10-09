using Campaign.AppLayer;
using System;
using System.Windows.Input;

namespace Campaign.PresentationLayer
{
   class Program
    {
        static void Main(string[] args)
        {
            DependencyLoader dependencyLoader = new DependencyLoader();
            var provider = dependencyLoader.Init();
            var commandParser = (ICommandManager)provider.GetService(typeof(ICommandManager));

        make:

            var keys = Read();

            commandParser.Execute(keys.command, keys.objs);

        goto make;

        }

        public static (string command, string[] objs) Read()
        {
            var input = Console.ReadLine().Split(' ');

            return (input[0], input[1..]);
        }
    }
}
