using System.Data;
using XirzoDIContainer.Container;

namespace XirzoDIContainer.Bind;

public class ScopeBind<TInterface> where TInterface : notnull
{
    private readonly Action<Type, Registration> _register;
    private readonly object? _instance;
    private readonly Func<object>? _factory;
    private readonly ContainerDi _container;
    private readonly Type? _concreteType;

    internal ScopeBind(Action<Type, Registration> register, object? instance, ContainerDi container, Func<object>? factory, Type? concreteType = null)
    {
        _register = register;
        _instance = instance;
        _container = container;
        _factory = factory;
        _concreteType = concreteType;

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
            var type = _concreteType ?? typeof(TInterface);
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
            typeof(TInterface),
            typeof(TInterface),
            _instance,
            _factory
        );

        _register(typeof(TInterface), registration);
    }

    public void AsTransient()
    {
        var registration = new Registration(
            Scope.Transient,
            typeof(TInterface),
            typeof(TInterface),
            _instance,
            _factory
        );

        _register(typeof(TInterface), registration);
    }
}
