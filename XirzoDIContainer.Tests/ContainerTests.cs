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
    internal class ContainerDiTests
    {
        private ContainerDi _container;

        [SetUp]
        public void Setup()
        {
            _container = new ContainerDi();
        }


        // ------------------------------------------------------------- Default

        [Test]
        public void Bind_ClassAsSingleton_ReturnsSameInstance()
        {
            _container.BindType<Mock>()
                .AsSingleton();

            Mock result1 = _container.Resolve<Mock>();
            Mock result2 = _container.Resolve<Mock>();

            Assert.That(result1, Is.EqualTo(result2), "Resolved instances should match each other");
        }


        [Test]
        public void Bind_ClassAsTransient_ReturnsSameInstance()
        {
            _container.BindType<Mock>()
                .AsTransient();

            Mock result1 = _container.Resolve<Mock>();
            Mock result2 = _container.Resolve<Mock>();

            Assert.That(result1, Is.Not.EqualTo(result2), "Resolved instances should not match each other");
        }

        [Test]
        public void Bind_InterfaceToClassAsSingleton_ReturnsSameInstance()
        {
            _container.Bind<IMock>()
                .To<Mock>()
                .AsSingleton();

            IMock result1 = _container.Resolve<IMock>();
            IMock result2 = _container.Resolve<IMock>();

            Assert.That(result1, Is.EqualTo(result2), "Resolved instances should match each other");
        }

        [Test]
        public void Bind_InterfaceToClassAsTransient_ReturnsDifferentInstances()
        {
            _container.Bind<IMock>()
                .To<Mock>()
                .AsTransient();

            IMock result1 = _container.Resolve<IMock>();
            IMock result2 = _container.Resolve<IMock>();

            Assert.That(result1, Is.Not.EqualTo(result2), "Resolved instances should not match");
        }

        // ------------------------------------------------------------- Instance

        [Test]
        public void Bind_SingleInstance_ReturnsSameInstance()
        {
            var instance = new Mock();

            _container.Bind<Mock>()
                .ToInstance(instance);

            Mock result = _container.Resolve<Mock>();

            Assert.That(result, Is.EqualTo(instance), "Resolved instance should match original instance");
        }

        [Test]
        public void Bind_SingleInstanceToInterface_ReturnsSameInstance()
        {
            var instance = new Mock();

            _container.Bind<IMock>()
                .ToInstance(instance);

            IMock result = _container.Resolve<IMock>();

            Assert.That(result, Is.EqualTo(instance), "Resolved instance should match original instance");
        }

        [Test]
        public void Bind_SingleInstancesMultipleTimes_ReturnsError()
        {
            var instance1 = new Mock();
            var instance2 = new Mock();

            _container.Bind<IMock>()
                .ToInstance(instance1);

            Assert.Throws<ArgumentException>(() => _container.Bind<IMock>().ToInstance(instance2));
        }

        // ------------------------------------------------------------- Factory


        [Test]
        public void Bind_FactoryBindToInterface_ReturnsDifferentInstances()
        {
            _container.Bind<IMock>()
                .ToFactory(() => new Mock());

            IMock result1 = _container.Resolve<IMock>();
            IMock result2 = _container.Resolve<IMock>();

            Assert.That(result1, Is.Not.EqualTo(result2), "Resolved instance should not match each other");
        }

        [Test]
        public void Bind_ClassToFactory_ReturnsDiffrentInstances()
        {
            _container.Bind<Mock>()
                .ToFactory(() => new Mock());

            Mock result1 = _container.Resolve<Mock>();
            Mock result2 = _container.Resolve<Mock>();

            Assert.That(result1, Is.Not.EqualTo(result2), "Resolved instance should not match each other");
        }

        // ------------------------------------------------------------- Non typical errors


        // [Test]
        // public void Resolve_UnregisteredType_ThrowsConstraintException()
        // {
        //     Result<IMock> result = _container.Resolve<IMock>();
        //
        //     Assert.Throws<System.Data.ConstraintException>(() => result.Value.Foo());
        // }
    }
}
