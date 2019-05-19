using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyDIContainer
{
    public class PropertiesSetter
    {
        public void SetProperties(Type type, object objKey)
        {
            List<PropertyInfo> properties = GetValidProperties(type);

            if(properties == null)
            {
                return;
            }

            foreach (var prop in properties)
            {
                SetValue(prop, objKey);
            }
        }

        private List<PropertyInfo> GetValidProperties(Type type)
        {
            PropertyInfo[] allProperties = type.GetProperties();
            List<PropertyInfo> validProperties = new List<PropertyInfo>();

            foreach (var prop in allProperties)
            {
                IEnumerable<Attribute> injectAttributes = prop.GetCustomAttributes(typeof(InjectAttribute));

                if (injectAttributes != null)
                {
                    foreach (var injAttr in injectAttributes)
                    {
                        validProperties.Add(prop);
                    }
                }
            }

            return validProperties;
        }

        private void SetValue(PropertyInfo property, object objKey)
        {
            object value = null;

            if (PropertiesBindingsStore.Bindings.ContainsKey(property))
            {
                value = PropertiesBindingsStore.Bindings[property];
            }
            
            property.SetValue(objKey, value);
        }
    }
}
