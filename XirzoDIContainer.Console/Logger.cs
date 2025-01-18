namespace XirzoDIContainer.Console;

public class Logger : ILogger
{
    public void Log(string text)
    {
        System.Console.WriteLine("Log: "+ text);
    }
}