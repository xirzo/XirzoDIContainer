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

            container.Bind<Mock>(() => new Mock()).AsSingleton();

            Result<Mock> result = container.Resolve<Mock>();
        }
    }
}
