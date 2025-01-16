using XirzoDIContainer.Container;

namespace XirzoDIContainer.Tests
{
    internal interface IMock
    {
        void Foo();
    }

    internal class Mock : IMock
    {
        public void Foo()
        {
        }
    }
    
    [TestFixture]
    public class ContainerDiTests
    {
        private ContainerDi _container;

        [SetUp]
        public void Setup()
        {
            _container = new ContainerDi();
        }

        [Test]
        public void Bind_SingleClassInstance_ReturnsSameInstance()
        {
            var instance = new Mock();
            
            _container.Bind<Mock>(instance);
            var resolved = _container.Resolve<Mock>();
            
           Assert.That(instance, Is.EqualTo(resolved), "Instances do not match");
        }
        

        [Test]
        public void Bind_SingleClassInstanceTwoTimes_ReturnsError()
        {
            var instance1 = new Mock();
            var instance2 = new Mock();
            
            _container.Bind<Mock>(instance1);
            _container.Bind<Mock>(instance2);
            var resolved = _container.Resolve<Mock>();
            
           Assert.That(instance1, Is.EqualTo(resolved), "Instances do not match");
        }
    }
}
