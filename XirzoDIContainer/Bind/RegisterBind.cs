using XirzoDIContainer.Container;

namespace XirzoDIContainer.Bind;

public class RegisterBind<TInterface> where TInterface : notnull
{
    private readonly Action<Type, Registration> _register;

    public RegisterBind(Action<Type, Registration> register)
    {
        _register = register;
    }

    public void ToInstance<TConcrete>(TConcrete instance) where TConcrete : TInterface
    {
        var registration = new Registration(
            Scope.Singleton,
            typeof(TInterface),
            typeof(TConcrete),
            instance,
            null
        );

        _register(typeof(TInterface), registration);
    }

    public void ToFactory<TConcrete>(Func<TConcrete> factory) where TConcrete : TInterface
    {
        Func<object> objectFactory = () =>
        {
            TConcrete val = factory();
            return (object)val;
        };

        var registration = new Registration(
            Scope.Transient,
            typeof(TInterface),
            typeof(TConcrete),
            null,
            objectFactory
        );

        _register(typeof(TInterface), registration);
    }
}
