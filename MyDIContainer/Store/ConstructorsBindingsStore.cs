using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDIContainer
{
    public static class ConstructorsBindingsStore
    {
        public static Dictionary<Type, Type> Bindings { get; private set; }
        
        static ConstructorsBindingsStore()
        {
            Bindings = new Dictionary<Type, Type>();
        }
        
        public static void Add(Type typeKey, Type typeValue)
        {
            if (Bindings.ContainsKey(typeKey))
            {
                Bindings[typeKey] = typeValue;
                throw new Exception($"Key {typeKey.GetType()} already exists");
            }
    
            Bindings.Add(typeKey, typeValue);
        }

        public static void Reset()
        {
            Bindings = new Dictionary<Type, Type>();
        }
    }
}

