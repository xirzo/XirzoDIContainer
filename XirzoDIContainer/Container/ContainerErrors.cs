using XirzoResult;

namespace XirzoDIContainer.Container;

public static class ContainerErrors
{
    public static Error RegistrationNotFound => new("REGISTRATION_NOT_FOUND", "DI Container did not find registration assigned to type");
    public static Error NoInstanceOrFactory => new("NO_INSTANCE_OR_FACTORY", "DI Container found the registration, but there is no factory or instance");
}
