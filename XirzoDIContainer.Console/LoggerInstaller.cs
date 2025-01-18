using XirzoDIContainer.Container;

namespace XirzoDIContainer.Console;

public class LoggerInstaller
{
    private readonly ContainerDi _container;

    public LoggerInstaller(ContainerDi container)
    {
        _container = container;
    }

    public void Bind()
    {
        _container.Bind<ILogger>().Factory(() => new Logger()).AsSingleton();
    }
}