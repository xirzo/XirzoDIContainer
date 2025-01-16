using XirzoDIContainer.Container;

namespace XirzoDIContainer.Bind;

public class RegisterBind<TInterface> where TInterface : notnull
{
    private Action<Type, Registration> _register;

    public RegisterBind(Action<Type, Registration> register)
    {
        _register = register;
    }

    public ScopeBind<TInterface> Instance<TConcrete>(TConcrete instance) where TConcrete : TInterface
    {
        return new ScopeBind<TInterface>(_register, instance);
    }

    public ScopeBind<TInterface> Factory<TConcrete>(Func<TConcrete> factory) where TConcrete : TInterface
    {
        Func<object> objectFactory = () =>
        {
            var result = factory();
            return result;
        };

        return new ScopeBind<TInterface>(_register, objectFactory);
    }
}
