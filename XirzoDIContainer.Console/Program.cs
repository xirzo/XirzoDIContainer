using XirzoDIContainer.Container;
using XirzoResult;

namespace XirzoDIContainer.Console
{
    internal class Program
    {
        private interface ILogger
        {
            void Log(string line);
        }

        private class ConsoleLogger : ILogger
        {
            public void Log(string line)
            {
                System.Console.WriteLine("Hello, World!");
            }
        }

        private static void Main()
        {
            var _container = new ContainerDi();

            var instance = new ConsoleLogger();

            _container.Bind<ILogger>()
                .Instance(instance);

            Result<ILogger> result = _container.Resolve<ILogger>();
            
            System.Console.WriteLine(result.Value);
        }
    }
}
