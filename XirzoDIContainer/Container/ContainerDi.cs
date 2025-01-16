﻿using XirzoDIContainer.Bind;
using XirzoResult;

namespace XirzoDIContainer.Container;

public class ContainerDi
{
    private readonly Dictionary<Type, Registration> _registrations;

    public ContainerDi()
    {
        _registrations = new();
    }

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
        if (_registrations.TryGetValue(typeof(TInterface), out Registration? registration) == false)
        {
            return ContainerErrors.RegistrationNotFound;
        }

        if (registration.Instance is TInterface instance)
        {
            return instance;
        }

        if (registration.Factory is Func<object> factory)
        {
            return (TInterface)registration.Factory();
        }

        Console.WriteLine(registration.Instance);
        Console.WriteLine(registration.Factory);

        return ContainerErrors.NoInstanceOrFactory;
    }
}
