using XirzoDIContainer.Container;
using XirzoResult;

namespace XirzoDIContainerConsole
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
                Console.WriteLine("Hello, World!");
            }
        }

        private static void Main()
        {
            var _container = new ContainerDi();

            // _container.Bind<IMock>()
            //     .Factory(() => new Mock())
            //     .AsSingleton();

            Result<IMock> resolveResult1 = _container.Resolve<IMock>();
            Console.WriteLine(resolveResult1.Value);
            // Result<IMock> resolveResult2 = _container.Resolve<IMock>();

            // Console.WriteLine(resolveResult1.Value == resolveResult2.Value);
        }
    }
}
