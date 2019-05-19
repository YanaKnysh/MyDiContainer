using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyDIContainer
{
    public class Kernel
    {
        private ConstructorChooser constructorChooser;
        private PropertiesSetter propertiesSetter;
        
        public Kernel()
        {
            constructorChooser = new ConstructorChooser();
            propertiesSetter = new PropertiesSetter();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class
        {
            Type typeKey = typeof(TInterface);
            Type typeValue = typeof(TImplementation);

            ConstructorsBindingsStore.Add(typeKey, typeValue);
        }

        public void RegisterWithProperty<TImplementation>(string propertyName, object value) where TImplementation : class
        {
            Type type = typeof(TImplementation);
            PropertyInfo property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException($"Type {type} doesn't contain property {propertyName}");
            }

            InjectAttribute injectAttribute = property?.GetCustomAttribute<InjectAttribute>();

            if (injectAttribute == null)
            {
                throw new ArgumentException($"Property{property} of type {type} should be marked with Inject attribute");
            }

            PropertiesBindingsStore.Add<TImplementation>(propertyName, value);
        }

        public void RegisterToSelf<TImlementation>() where TImlementation : class
        {
            Register<TImlementation, TImlementation>();
        }

        public TImplementation Get<TImplementation>() where TImplementation : class
        {
            return (TImplementation)Get(typeof(TImplementation));
        }

        private object Get(Type typeKey)
        {
            if (!ConstructorsBindingsStore.Bindings.ContainsKey(typeKey))
            {
                throw new InvalidOperationException($"Type {typeKey} isn't registered");
            }

            Type typeValue = ConstructorsBindingsStore.Bindings[typeKey];
            object result = GetNewObject(typeValue);
            propertiesSetter.SetProperties(typeKey, result); //set properties if necessary

            return result;
        }

        private object CreateInstance(Type implementationType, ConstructorInfo constructor)
        {
            var parameterTypes = constructor.GetParameters().Select(p => p.ParameterType);
            var dependencies = parameterTypes.Select(t => Get(t)).ToArray();

            //var ctor = Expression.New(constructor); // 3 option
            //return Expression.MemberInit(ctor, dependencies);

            return constructor.Invoke(dependencies); // 2 option
            //return Activator.CreateInstance(implementationType, dependencies); // 1 option - works everything without this string 1) Reflexion Cstr.invoke, 2) expressions
        }
       
        private object GetNewObject(Type type)
        {
            ConstructorInfo validConstructor = constructorChooser.GetValidConstructor(type);
            return CreateInstance(type, validConstructor);
        }
    }
}
