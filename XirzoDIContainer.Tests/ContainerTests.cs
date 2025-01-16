using XirzoDIContainer.Container;
using XirzoResult;

namespace XirzoDIContainer.Tests
{
    internal interface IMock
    {
    }

    internal class Mock : IMock
    {
    }

    [TestFixture]
    internal class ContainerDiTests
    {
        private ContainerDi _container;

        [SetUp]
        public void Setup()
        {
            _container = new ContainerDi();
        }

        // ------------------------------------------------------------- Singleton with instance

        [Test]
        public void Bind_SingleInstance_ReturnsSameInstance()
        {
            var instance = new Mock();
            _container.Bind<Mock>(instance);

            Result<Mock> resolveResult = _container.Resolve<Mock>();

            Assert.That(resolveResult.Value, Is.EqualTo(instance), "Resolved instance should match original instance");
        }

        [Test]
        public void Bind_SingleInstanceToInterface_ReturnsSameInstance()
        {
            var instance = new Mock();
            _container.Bind<IMock>(instance);

            Result<IMock> resolveResult = _container.Resolve<IMock>();

            Assert.That(resolveResult.Value, Is.EqualTo(instance), "Resolved instance should match original instance");
        }

        [Test]
        public void Bind_SingleInstancesMultipleTimes_ReturnsError()
        {
            var instance1 = new Mock();
            var instance2 = new Mock();

            _container.Bind<IMock>(instance1);
            _container.Bind<IMock>(instance2);

            Result<IMock> resolveResult = _container.Resolve<IMock>();

            Assert.That(resolveResult.Error, Does.Contain("Mock"),
                "Error should mention the type that couldn't be resolved");
        }

        // ------------------------------------------------------------- Singleton without instance


        [Test]
        public void Bind_Singleton_ReturnsSameInstance()
        {
            _container.Bind<Mock>(() => new Mock()).AsSingleton();

            Result<Mock> resolveResult1 = _container.Resolve<Mock>();
            Result<Mock> resolveResult2 = _container.Resolve<Mock>();

            Assert.That(resolveResult1.Value, Is.EqualTo(resolveResult2.Value), "Resolved instance should match each other");
        }

        [Test]
        public void Bind_SingletonToInterface_ReturnsSameInstance()
        {
            _container.Bind<IMock, Mock>(() => new Mock()).AsSingleton();

            Result<IMock> resolveResult1 = _container.Resolve<IMock>();
            Result<IMock> resolveResult2 = _container.Resolve<IMock>();

            Assert.That(resolveResult1.Value, Is.EqualTo(resolveResult2.Value), "Resolved instance should match each other");
        }


        // ------------------------------------------------------------- Transient

        [Test]
        public void Bind_Transient_ReturnsDifferentInstances()
        {
            _container.Bind<Mock>(() => new Mock()).AsTransient();

            Result<Mock> resolveResult1 = _container.Resolve<Mock>();
            Result<Mock> resolveResult2 = _container.Resolve<Mock>();

            Assert.That(resolveResult1.Value, Is.Not.EqualTo(resolveResult2.Value), "Resolved instance should not match each other");
        }

        [Test]
        public void Bind_TransientToInterface_ReturnsDifferentInstances()
        {
            _container.Bind<IMock, Mock>(() => new Mock()).AsTransient();

            Result<IMock> resolveResult1 = _container.Resolve<IMock>();
            Result<IMock> resolveResult2 = _container.Resolve<IMock>();

            Assert.That(resolveResult1.Value, Is.Not.EqualTo(resolveResult2.Value), "Resolved instance should not match each other");
        }


        // ------------------------------------------------------------- Non typical errors


        [Test]
        public void Resolve_UnregisteredType_ReturnsMeaningfulError()
        {
            Result<IMock> resolveResult = _container.Resolve<IMock>();

            Assert.That(resolveResult.Error, Does.Contain("IMock"),
                "Error should mention the type that couldn't be resolved");
        }
    }
}
