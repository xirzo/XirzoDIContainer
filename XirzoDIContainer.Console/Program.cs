using XirzoDIContainer.Container;

namespace XirzoDIContainer.Console
{
    internal interface ILogger
    {
        void Log(string text);
    }

    internal class Logger : ILogger
    {
        public void Log(string text)
        {
            System.Console.WriteLine("Log: " + text);
        }
    }

    internal class Game
    {
        private readonly ILogger _logger;

        public Game(ILogger logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.Log("Game running");
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var container = new ContainerDi();

            container.Bind<ILogger>()
                .ToFactory(() => new Logger());

            container.Bind<Game>().AsSingleton();

            var game = container.Resolve<Game>();

            game.Run();
        }
    }
}
