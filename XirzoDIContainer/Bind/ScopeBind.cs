
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
        if (instance == null && factory == null)
        {
            throw new ConstraintException("Both instance and factory are null");
        }
        
        if (instance != null && factory != null)
        {
            throw new ConstraintException("Both instance and factory are not null");
        }
        
        _register = register;
        _instance = instance;
        _factory = factory;

        if (_factory != null)
        {
            _instance = _factory();
        }
    }
    public void AsSingleton()
    {
        var registration = new Registration(
            Scope.Singleton,
            typeof(T),
            typeof(T),
            _instance,
            _factory
        );

        _register(typeof(T), registration);
    }

    public void AsTransient()
    {
        var registration = new Registration(
            Scope.Transient,
            typeof(T),
            typeof(T),
            _instance,
            _factory
        );

        _register(typeof(T), registration);
    }
}
