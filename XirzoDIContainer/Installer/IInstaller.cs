using XirzoDIContainer.Container;

namespace XirzoDIContainer.Installer;

public interface IInstaller
{
    void Bind(ContainerDi container);
}