
using System.Data;
using XirzoDIContainer.Container;

namespace XirzoDIContainer.Bind;

public class ScopeBind<T> where T : notnull
{
    private readonly Action<Type, Registration> _register;
    private readonly object? _instance;
    private readonly Func<object>? _factory;


    internal ScopeBind(Action<Type, Registration> register, object? instance, Func<object>? factory = null)
    {
        _register = register;
        _instance = instance;
        _factory = factory;
    }

    public Scope Scope { get; private set; } = Scope.Singleton;

    public void AsSingleton()
    {
        if (_instance == null)
        {
            throw new ConstraintException("AsSingleton mustn`t be used if instance is null");
        }

        var registration = new Registration(
            typeof(T),
            _instance as Type,
            _instance,
            _factory,
            Scope.Singleton
        );

        _register(typeof(T), registration);
    }

    public void AsTransient()
    {

        if (_factory == null)
        {
            throw new ConstraintException("AsTransient mustn`t be used if factory is null");
        }

        var registration = new Registration(
            typeof(T),
            typeof(T),
            _instance,
            _factory,
            Scope.Transient
        );

        _register(typeof(T), registration);
    }
}
