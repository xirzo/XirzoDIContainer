using XirzoResult;

namespace XirzoDIContainer.Container;


public class ContainerDi
{
    private readonly Dictionary<Type, Registration> _registrations;

    public ContainerDi()
    {
        _registrations = new();
    }

    public void Bind<TInterface>(TInterface instance) where TInterface : notnull
    {
        var registration = new Registration(typeof(TInterface), typeof(TInterface), instance);
        _registrations.Add(typeof(TInterface), registration);
    }

    public Registration Bind<TInterface>(Func<TInterface> factory) where TInterface : notnull
    {
        Func<object> objectFactory = () =>
        {
            TInterface instance = factory();
            return (object)instance;
        };

        var registration = new Registration(typeof(TInterface), typeof(TInterface), null, objectFactory);
        _registrations.Add(typeof(TInterface), registration);
        return registration;
    }

    public Registration Bind<TInterface, TConcrete>(Func<TConcrete> factory) where TConcrete : TInterface where TInterface : notnull
    {
        Func<object> objectFactory = () =>
        {
            TInterface instance = factory();
            return (object)instance;
        };

        var registration = new Registration(typeof(TInterface), typeof(TConcrete), null, objectFactory);
        _registrations.Add(typeof(TInterface), registration);
        return registration;
    }

    public Result<TInterface> Resolve<TInterface>()
    {
        if (_registrations.TryGetValue(typeof(TInterface), out Registration? registration) == false)
        {
            return ContainerErrors.RegistationNotFound;
        }

        if (registration.Instance is TInterface instance)
        {
            return instance;
        }

        if (registration.Factory is Func<object> factory)
        {
            return (TInterface)registration.Factory();
        }

        return ContainerErrors.NoInstanceOrFactory;
    }
}
