![C#](https://img.shields.io/badge/C%23-100%25-blue)
[![Last Commit](https://img.shields.io/github/last-commit/xirzo/XirzoDIContainer)](https://github.com/xirzo/XirzoDIContainer/commits/main)

This is a lightweight Dependency Injection library that simplifies dependency management in your applications.

## Features

- **Type Binding**: Bind concrete types directly with `BindType<T>()` 
- **Interface Binding**: Map interfaces to implementations using `Bind<T>().To<TImplementation>()` 
- **Instance Binding**: Bind existing instances using `ToInstance()` 
- **Factory Binding**: Create custom instantiation logic with `ToFactory()` 
- **Lifetimes**:
  - Singleton: One instance for all resolutions
  - Transient: New instance per resolution

## Usage

```csharp
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
```
