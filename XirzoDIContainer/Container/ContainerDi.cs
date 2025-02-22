using XirzoDIContainer.Bind;

namespace XirzoDIContainer.Container;

public class ContainerDi
{
    private readonly Dictionary<Type, Registration> _registrations = new();

    public RegisterBind<TInterface> Bind<TInterface>() where TInterface : notnull
    {
        Action<Type, Registration> register = (type, registration) =>
        {
            _registrations.Add(type, registration);
        };

        return new RegisterBind<TInterface>(register);
    }

    public TInterface Resolve<TInterface>()
    {
        if (_registrations.TryGetValue(typeof(TInterface), out var registration) == false)
        {
            throw new InvalidOperationException($"{typeof(TInterface)} has not been registered.");
        }

        switch (registration.Scope)
        {
            case Scope.Singleton:
                {
                    if (registration.Instance is TInterface instance)
                    {
                        return instance;
                    }

                    if (registration.Factory is not null)
                    {
                        return (TInterface)registration.Factory();
                    }

                    break;
                }

            case Scope.Transient:
                {
                    if (registration.Factory is not null)
                    {
                        return (TInterface)registration.Factory();
                    }

                    break;
                }
        }

        throw new InvalidOperationException($"No instance or factory available for type {typeof(TInterface).FullName}. Registration scope: {registration.Scope}");
    }
}
