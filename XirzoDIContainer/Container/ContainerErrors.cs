using XirzoResult;

public static class ContainerErrors
{
    public static Error RegistationNotFound => new Error("REGISTATION_NOT_FOUND", "DI Container did not find registation assigned to type");
    public static Error NoInstanceOrFactory => new Error("NO_INSTANCE_OR_FACTORY", "DI Container did find the registation, but there is no factory or instance");
}
