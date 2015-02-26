using System;
using System.Drawing;
using System.Reflection;

namespace Skybound.ComponentModel
{

    [System.AttributeUsage(System.AttributeTargets.Field)]
    internal sealed class EnumValueImageProviderAttribute : System.Attribute
    {

        private string _PropertyName;
        private string _ProviderTypeName;

        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
        }

        public System.Type ProviderType
        {
            get
            {
                if ((_ProviderTypeName != null) && (_ProviderTypeName.Length != 0))
                    return System.Type.GetType(_ProviderTypeName);
                return null;
            }
        }

        public EnumValueImageProviderAttribute(System.Type providerType, string propertyName) : this(providerType.FullName, propertyName)
        {
        }

        public EnumValueImageProviderAttribute(string providerTypeName, string propertyName)
        {
            _ProviderTypeName = providerTypeName;
            _PropertyName = propertyName;
        }

        public System.Drawing.Image GetImage()
        {
            System.Reflection.PropertyInfo propertyInfo = ProviderType.GetProperty(PropertyName);
            if (propertyInfo != null)
                return propertyInfo.GetValue(null, null) as System.Drawing.Image;
            return null;
        }

    } // class EnumValueImageProviderAttribute

}

