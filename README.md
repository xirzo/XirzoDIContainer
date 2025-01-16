# XirzoDIContainer 📦

![C#](https://img.shields.io/badge/C%23-100%25-blue)
[![Last Commit](https://img.shields.io/github/last-commit/xirzo/XirzoDIContainer)](https://github.com/xirzo/XirzoDIContainer/commits/main)

Welcome to **XirzoDIContainer**! This is a lightweight Dependency Injection (DI) library designed to simplify the process of managing dependencies in your C# applications. 🚀

## Features ✨

- **Singleton Binding with Instance**: Bind a single instance that will always return the same instance. 🪄
- **Singleton Binding with Factory**: Bind a factory method that will return the same instance every time it's resolved. 🔄
- **Transient Binding**: Bind a factory method that will return a new instance every time it's resolved. 🆕
- **Error Handling**: Provides clear error messages for issues like unregistered types or multiple singleton bindings. 🚨

## Getting Started 🌟

Here’s a quick guide to get you started with XirzoDIContainer:

### 1. Define Your Services ✍️

Create interfaces and their implementations.

```csharp
public interface IGreetingService
{
    void Greet(string name);
}

public class GreetingService : IGreetingService
{
    public void Greet(string name)
    {
        Console.WriteLine($"Hello, {name}!");
    }
}
```

### 2. Use the DI Container 🛠️

Register your services in the DI container.

#### Singleton with Instance

```csharp
var container = new ContainerDi();
var instance = new GreetingService();

container.Bind<IGreetingService>()
    .Instance(instance).AsSingleton();

// Resolve the service
var greetingService = container.Resolve<IGreetingService>().Value;
greetingService.Greet("World");
```

#### Singleton with Factory

```csharp
var container = new ContainerDi();

container.Bind<IGreetingService>()
    .Factory(() => new GreetingService()).AsSingleton();

// Resolve the service
var greetingService1 = container.Resolve<IGreetingService>().Value;
var greetingService2 = container.Resolve<IGreetingService>().Value;

// Both instances should be the same
Console.WriteLine(ReferenceEquals(greetingService1, greetingService2)); // True
```

#### Transient

```csharp
var container = new ContainerDi();

container.Bind<IGreetingService>()
    .Factory(() => new GreetingService()).AsTransient();

// Resolve the service
var greetingService1 = container.Resolve<IGreetingService>().Value;
var greetingService2 = container.Resolve<IGreetingService>().Value;

// Both instances should be different
Console.WriteLine(ReferenceEquals(greetingService1, greetingService2)); // False
```

### Error Handling 🎯

Handle errors for unregistered types or other issues using the result object.

```csharp
var container = new ContainerDi();

container.Bind<IMock>()
    .Factory(() => new Mock())
    .AsSingleton();

Result<IMock> resolveResult = container.Resolve<IMock>();

if (resolveResult.IsSuccess) {
    var mock = resolveResult.Value;
    // Use the resolved instance
    mock.Foo();
} else {
    Console.WriteLine("Failed to resolve IMock: " + resolveResult.ErrorMessage);
}
```
