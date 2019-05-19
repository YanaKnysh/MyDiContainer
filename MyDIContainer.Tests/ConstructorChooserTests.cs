using MyDIContainer.Tests.TestClasses.ConstructorSetter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Reflection;

namespace MyDIContainer.Tests
{
    [TestFixture]
    public class ConstructorChooserTests
    {
        ConstructorChooser constructorChooser;

        [SetUp]
        public void Init()
        {
            constructorChooser = new ConstructorChooser();
        }

        [TearDown]
        public void Cleanup()
        {
            ConstructorsBindingsStore.Reset();
        }

        [Test]
        public void Test_GetValidConstructor_ShouldBeException()
        {
            Assert.Catch<Exception>(() => constructorChooser.GetValidConstructor(typeof(CtorSword)));
        }

        [Test]
        public void Test_GetValidConstructor_ShouldBeDefaultCtor()
        {
            ConstructorInfo constructor = constructorChooser.GetValidConstructor(typeof(CtorKnife));
            string ctorName = constructor.Name;
            Assert.AreEqual(".ctor", ctorName);
        }

        [Test]
        public void Test_GetValidConstructor_ShouldBeCtorMaterial()
        {
            ConstructorsBindingsStore.Add(typeof(CtorMaterial), typeof(CtorMaterial));
            ConstructorInfo constructor = constructorChooser.GetValidConstructor(typeof(CtorSword));
            string ctorName = constructor.ToString();
            string expectedResult = "Void .ctor(MyDIContainer.Tests.TestClasses.ConstructorSetter.CtorMaterial)";
            Assert.AreEqual(expectedResult, ctorName);
        }
    }
}
