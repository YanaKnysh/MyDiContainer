using MyDIContainer.Tests.TestClasses.PropertiesSetter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MyDIContainer.Tests
{
    [TestFixture]
    class PropertiesSetterTests
    {
        PropertiesSetter propertiesSetter;

        [SetUp]
        public void Init()
        {
            propertiesSetter = new PropertiesSetter();
        }

        [TearDown]
        public void Cleanup()
        {
            PropertiesBindingsStore.Reset();
        }

        [Test]
        public void Test_SetProperties_WithExactValue_ShouldBeValue()
        {
            PropSword sword = new PropSword();
            PropMaterial material = new PropMaterial("Value");
            PropertiesBindingsStore.Add<PropSword>("Material", material);
            Kernel kernel = new Kernel();
            propertiesSetter.SetProperties(typeof(PropSword), sword);
            Assert.AreEqual("Value", sword.Material.name);
        }

        [Test]
        public void Test_SetProperties_WithDefaultValue_ShouldBeNull()
        {
            PropSword sword = new PropSword();
            propertiesSetter.SetProperties(typeof(PropSword), sword);
            Assert.AreEqual(null, sword.Material);
        }

        [Test]
        public void Test_SetProperties_WithPrimiteveType_ShouldBeFive()
        {
            PropKnife knife = new PropKnife();
            int n = 5;
            PropertiesBindingsStore.Add<PropKnife>("Count", n);
            propertiesSetter.SetProperties(typeof(PropKnife), knife);
            Assert.AreEqual(5, knife.Count);
        }

        [Test]
        public void Test_SetProperties_WithPrimiteveType_ShouldBeZero()
        {
            PropKnife knife = new PropKnife();
            PropertyInfo property = knife.GetType().GetProperty("Count");
            propertiesSetter.SetProperties(typeof(PropKnife), knife);
            Assert.AreEqual(0, knife.Count);
        }

        [Test]
        public void Test_SetProperties_WithPrimiteveType_ShouldBeException()
        {
            PropKnife knife = new PropKnife();
            string n = "ggg";
            Assert.Catch<ArgumentException>(() => PropertiesBindingsStore.Add<PropKnife>("Count", n));
        }
    }
}
