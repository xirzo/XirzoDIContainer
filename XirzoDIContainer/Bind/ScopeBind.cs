
using XirzoDIContainer.Container;

namespace XirzoDIContainer.Bind;

public class ScopeBind<T> where T : notnull
{
    public Scope Scope { get; private set; } = Scope.Singleton;

    public void AsSingleton()
    {
        Scope = Scope.Singleton;
    }

    public void AsTransient()
    {
        Scope = Scope.Transient;
    }
}
