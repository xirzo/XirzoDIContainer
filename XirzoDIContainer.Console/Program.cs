using XirzoDIContainer.Container;

var container = new ContainerDi();

container.Bind<IGreetingService>()
    .To<GreetingService>()
    .AsSingleton();

var service = container.Resolve<IGreetingService>();

service.Greet();

public interface IGreetingService
{
    void Greet();
}

public class GreetingService : IGreetingService
{
    public void Greet()
    {
        Console.WriteLine("Hello!");
    }
}
