namespace XirzoDIContainer.Bind;

public class RegisterBind<T> where T : notnull
{
    public ScopeBind<T> Instance<TConcrete>() where TConcrete : T
    {
        throw new NotImplementedException();
    }

    public ScopeBind<T> Factory<TConcrete>() where TConcrete : T
    {
        throw new NotImplementedException();
    }
}
