using XirzoDIContainer.Bind;
using XirzoResult;

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

    public Result<TInterface> Resolve<TInterface>()
    {
        if (_registrations.TryGetValue(typeof(TInterface), out var registration) == false)
        {
            return ContainerErrors.RegistrationNotFound;
        }

        switch (registration.Scope)
        {
            case Scope.Singleton:
            {
                if (registration.Instance is TInterface instance)
                {
                    return instance;
                }

                if (registration.Factory is Func<object>)
                {
                    return (TInterface)registration.Factory();
                }

                break;
            }

            case Scope.Transient:
            {
                if (registration.Factory is Func<object>)
                {
                    return (TInterface)registration.Factory();
                }

                break;
            }
        }
        
        return ContainerErrors.NoInstanceOrFactory;
    }
}
