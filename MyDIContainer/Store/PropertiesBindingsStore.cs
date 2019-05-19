using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyDIContainer
{
    public static class PropertiesBindingsStore
    {
        public static Dictionary<PropertyInfo, object> Bindings { get; private set; }

        static PropertiesBindingsStore()
        {
            Bindings = new Dictionary<PropertyInfo, object>();
        }

        public static void Add<TImplementation>(string propertyName, object value)
        {
            Type type = typeof(TImplementation);
            PropertyInfo property = type.GetProperty(propertyName);
            
            if (property == null)
            {
                throw new ArgumentException($"Type {type} doesn't contain property {propertyName}");
            }

            if (Bindings.ContainsKey(property))
            {
                throw new Exception($"Key {property.GetType()} already exists");
            }

            if (property.PropertyType == value.GetType())
            {
                Bindings.Add(property, value);
            }
            else
            {
                throw new ArgumentException($"Value is of type {value.GetType()} but should be of type {property.PropertyType}");
            }
        }
        
        public static void Reset()
        {
            Bindings = new Dictionary<PropertyInfo, object>();
        }
    }
}
