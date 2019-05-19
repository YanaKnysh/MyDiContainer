using NUnit.Framework;
using Moq;
using MyDIContainer;
using MyDIContainer.Tests.TestClasses.Kernel;
using System;
using System.Reflection;

namespace Tests
{
    [TestFixture]
    public class KernelTests
    {
        private ConstructorChooser constructorChooser;
        private PropertiesSetter propertiesChooser;
        private Kernel kernel;

        [OneTimeSetUp]
        public void Setup()
        {
            constructorChooser = new ConstructorChooser();
            propertiesChooser = new PropertiesSetter();
            kernel = new Kernel();
        }

        [TearDown]
        public void Cleanup()
        {
            PropertiesBindingsStore.Reset();
            ConstructorsBindingsStore.Reset();
        }
        
        [Test]
        public void Test_Register_RegisteredTypes_ShouldBeTrue()
        {
            kernel.Register<IKernelWeapon, KernelSword>();
            Assert.IsTrue(ConstructorsBindingsStore.Bindings.ContainsKey(typeof(IKernelWeapon)));
        }

        [Test]
        public void Test_Register_NotRegisteredTypes_ShouldBeFalse()
        {
            Assert.IsFalse(ConstructorsBindingsStore.Bindings.ContainsKey(typeof(IKernelWeapon)));
        }

        [Test]
        public void Test_RegisterWithProperty_PropertyNotExists_ShouldBeException()
        {
            Assert.Throws<ArgumentException>(() => kernel.RegisterWithProperty<KernelSword>("asd", 5));
        }

        [Test]
        public void Test_RegisterWithProperty_ValidArgs_ShouldBeFive()
        {
            KernelSword sword = new KernelSword();
            kernel.RegisterWithProperty<KernelSword>("Count", 5);
            PropertyInfo property = typeof(KernelSword).GetProperty("Count");
            Assert.AreEqual(5, PropertiesBindingsStore.Bindings[property]);
        }

        [Test]
        public void Test_RegisterWithProperty_NotMarkedWithAttributesArgs_ShouldBeException()
        {
            Assert.Catch<ArgumentException>(() => kernel.RegisterWithProperty<KernelKnife>("State", "ggg"));
        }

        [Test]
        public void Test_RegisterToSelf_ValidArgs_ShouldBeTrue()
        {
            kernel.RegisterToSelf<KernelSword>();
            Assert.IsTrue(ConstructorsBindingsStore.Bindings.ContainsKey(typeof(KernelSword)));
        }

        [Test]
        public void Test_Get_ValidArgs_ShouldBeTrue()
        {
            kernel.Register<IKernelWeapon, KernelSword>();
            IKernelWeapon result = kernel.Get<IKernelWeapon>();
            Assert.IsTrue(result.GetType() == typeof(KernelSword));
        }

        [Test]
        public void Test_Get_InvalidArgs_ShouldBeException()
        {
            Assert.Throws<InvalidOperationException>(() => kernel.Get<IKernelWeapon>());
        }

        [Test]
        public void Test_Get_ValidArgs_ShoudBeInstance()
        {
            kernel.Register<IKernelWeapon, KernelKnife>();
            kernel.RegisterToSelf<KernelMaterial>();
            IKernelWeapon result = kernel.Get<IKernelWeapon>();
        }
        //check instance of Interface
        //Get
    }
}