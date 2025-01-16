namespace XirzoDIContainer.Container;

public class Registration
{
    internal Type InterfaceType { get; }
    internal Type ImplementationType { get; }
    internal object? Instance { get; }
    internal Func<object>? Factory { get; }
    internal Scope Scope { get; private set; }

    internal Registration(Type interfaceType, Type implementationType, object? instance = null, Func<object>? factory = null, Scope scope = Scope.Singleton)
    {
        InterfaceType = interfaceType;
        ImplementationType = implementationType;
        Instance = instance;
        Factory = factory;
        Scope = scope;
    }

    public void AsSingleton()
    {
        Scope = Scope.Singleton;
    }

    public void AsTransient()
    {
        Scope = Scope.Transient;
    }
}
