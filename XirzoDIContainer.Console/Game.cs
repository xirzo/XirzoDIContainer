using XirzoDIContainer.Container;
using XirzoResult;

namespace XirzoDIContainer.Console;

public class Game
{
    private readonly ContainerDi _container;
    private readonly ILogger _logger;

    public Game(ContainerDi container)
    {
        _container = container;
        Result<ILogger> logger_result = _container.Resolve<ILogger>();
        
        if (logger_result.IsSuccess == false)
            throw new ArgumentException("ILogger was not set up");

        _logger = logger_result.Value;
    }

    public void Run()
    {
        _logger.Log("Run");
    }
}