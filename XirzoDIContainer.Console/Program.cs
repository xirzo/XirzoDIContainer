using XirzoDIContainer.Container;

namespace XirzoDIContainer.Console
{
    internal class Program
    {
        private static void Main()
        {
            var container = new ContainerDi();

            container.Add(new LoggerInstaller());

            var game = new Game(container);
            game.Run();
        }
    }
}
