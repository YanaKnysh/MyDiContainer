using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyDIContainer
{
    public class ConstructorChooser
    {
        public ConstructorInfo GetValidConstructor(Type type)
        {
            ConstructorInfo validConstructor = null;

            var constructors = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .ToArray(); // add public constructors ordered by descending

            foreach (var constructor in constructors)
            {
                if (IsConstructorValid(constructor))
                {
                    validConstructor = constructor;
                    break;
                }
            }

            if (validConstructor == null)
            {
                throw new Exception($"There are no valid constructors of type {type.GetType()}");
            }
            else
            {
                return validConstructor;
            }

        }

        private bool AreConstructorParametersValid(ParameterInfo[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.IsPrimitive ||
                    parameters[i].ParameterType == typeof(string) ||
                    parameters[i].ParameterType == typeof(decimal) ||
                    !ConstructorsBindingsStore.Bindings.ContainsKey(parameters[i].ParameterType))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsConstructorValid(ConstructorInfo constructor)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            List<Type> types = new List<Type>();

            if (AreConstructorParametersValid(parameters))
            {
                return true;
            }

            return false;
        }
    }
}
