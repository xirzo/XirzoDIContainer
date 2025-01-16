using System;
using XirzoDIContainer.Container;
using XirzoResult;

namespace XirzoDIContainer.Console
{
    internal class Program
    {
        private interface IMock
        {
            void Print();
        }

        private class Mock : IMock
        {
            public void Print()
            {
                System.Console.WriteLine("Hello, World!");
            }
        }

        private static void Main(string[] args)
        {
            ContainerDi container = new ContainerDi();

            container.Bind<Mock>()
                .Factory(() => new Mock())
                .AsSingleton();

            Result<Mock> result = container.Resolve<Mock>();

            if (result.IsSuccess == false){
                System.Console.WriteLine(result.Error);
                return;
            }

            System.Console.WriteLine("Success!");
            System.Console.WriteLine(result.Value);
        }
    }
}
