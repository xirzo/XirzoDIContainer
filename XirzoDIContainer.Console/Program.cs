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
            var container = new ContainerDi();

            var installer = new LoggerInstaller(container);
            installer.Bind();

            var game = new Game(container);
            game.Run();
        }
    }
}
