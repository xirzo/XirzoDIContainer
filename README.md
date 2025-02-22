# XirzoDIContainer 📦

![C#](https://img.shields.io/badge/C%23-100%25-blue)
[![Last Commit](https://img.shields.io/github/last-commit/xirzo/XirzoDIContainer)](https://github.com/xirzo/XirzoDIContainer/commits/main)

Welcome to **XirzoDIContainer**! This is a lightweight Dependency Injection (DI) library designed to simplify dependency management in your C# applications. 🚀

## Features ✨

- **Type Binding**: Bind concrete types directly with `BindType<T>()` 🎯
- **Interface Binding**: Map interfaces to implementations using `Bind<T>().To<TImplementation>()` 🔄
- **Instance Binding**: Bind existing instances using `ToInstance()` 📦
- **Factory Binding**: Create custom instantiation logic with `ToFactory()` 🏭
- **Lifetime Management**:
  - Singleton: One instance for all resolutions
  - Transient: New instance per resolution
- **Fluent API**: Intuitive and chainable configuration methods 🔗

## Getting Started 🌟

### Basic Usage

1. Create your container:

```csharp
var container = new ContainerDi();
```

2. Register your dependencies:

#### Direct Type Binding

```csharp
// Singleton
container.BindType<MyService>()
    .AsSingleton();

// Transient
container.BindType<MyService>()
    .AsTransient();
```

#### Interface to Implementation Binding

```csharp
// Singleton
container.Bind<IMyService>()
    .To<MyService>()
    .AsSingleton();

// Transient
container.Bind<IMyService>()
    .To<MyService>()
    .AsTransient();
```

#### Instance Binding

```csharp
var myInstance = new MyService();
container.Bind<IMyService>()
    .ToInstance(myInstance);
```

#### Factory Binding

```csharp
container.Bind<IMyService>()
    .ToFactory(() => new MyService());
```

### Resolving Dependencies

```csharp
// Resolve your service
var service = container.Resolve<IMyService>();
```

## Best Practices 🎯

1. **Singleton vs Transient**:
   - Use `AsSingleton()` when you need the same instance throughout your application
   - Use `AsTransient()` when you need a new instance each time

2. **Instance Binding**:
   - Use `ToInstance()` when you have pre-configured instances
   - Note: You cannot bind multiple instances to the same type

3. **Factory Binding**:
   - Use `ToFactory()` when you need custom instantiation logic
   - Factories always create new instances

## Example Scenario 📝

```csharp
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

// Setup container
var container = new ContainerDi();

// Register as singleton
container.Bind<IGreetingService>()
    .To<GreetingService>()
    .AsSingleton();

// Resolve and use
var service = container.Resolve<IGreetingService>();
service.Greet();
```
