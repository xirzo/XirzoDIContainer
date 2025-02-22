using System.Data;
using XirzoDIContainer.Container;

namespace XirzoDIContainer.Bind;

public class ScopeBind<T> where T : notnull
{
    private readonly Action<Type, Registration> _register;
    private readonly object? _instance;
    private readonly Func<object>? _factory;
    private readonly ContainerDi _container;

    internal ScopeBind(Action<Type, Registration> register, object? instance, ContainerDi container, Func<object>? factory = null)
    {
        _register = register;
        _instance = instance;
        _container = container;
        _factory = factory;

        if (instance == null && factory == null)
        {
            _factory = CreateFactory();
            _instance = _factory();
        }
        else if (instance != null && factory != null)
        {
            throw new ConstraintException("Both instance and factory cannot be non-null");
        }
        else if (_factory != null)
        {
            _instance = _factory();
        }
    }

    private Func<object> CreateFactory()
    {
        return () =>
        {
            var type = typeof(T);
            var constructors = type.GetConstructors();

            var constructor = constructors.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault()
                ?? throw new InvalidOperationException($"No suitable constructor found for type {type.FullName}");

            var parameters = constructor.GetParameters();
            var resolvedParameters = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var parameterType = parameter.ParameterType;

                try
                {
                    var resolveMethod = typeof(ContainerDi).GetMethod("Resolve")?.MakeGenericMethod(parameterType)
                        ?? throw new InvalidOperationException($"Could not find Resolve method for type {parameterType.FullName}");

                    resolvedParameters[i] = resolveMethod.Invoke(_container, null)
                        ?? throw new InvalidOperationException($"Failed to resolve parameter {parameter.Name} of type {parameterType.FullName}");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"Failed to resolve dependency {parameterType.FullName} for type {type.FullName}",
                        ex);
                }
            }

            return constructor.Invoke(resolvedParameters);
        };
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
