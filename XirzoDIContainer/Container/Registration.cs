namespace XirzoDIContainer.Container;

public class Registration
{
    internal Type InterfaceType { get; }
    internal Type? ImplementationType { get; }
    internal object? Instance { get; }
    internal Func<object>? Factory { get; }
    internal Scope Scope { get; private set; }

    internal Registration(Scope scope, Type interfaceType, Type? implementationType, object? instance = null, Func<object>? factory = null)
    {
        Scope = scope;
        InterfaceType = interfaceType;
        ImplementationType = implementationType;
        Instance = instance;
        Factory = factory;
    }
}
