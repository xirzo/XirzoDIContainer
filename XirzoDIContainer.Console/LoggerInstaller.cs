using XirzoDIContainer.Container;
using XirzoDIContainer.Installer;

namespace XirzoDIContainer.Console;

public class LoggerInstaller : IInstaller
{
    public void Bind(ContainerDi container)
    {
        container.Bind<ILogger>().Factory(() => new Logger()).AsSingleton();
    }
}