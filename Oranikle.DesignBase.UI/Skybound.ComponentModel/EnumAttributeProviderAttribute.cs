using System;

namespace Skybound.ComponentModel
{

    [System.AttributeUsage(System.AttributeTargets.Property)]
    internal sealed class EnumAttributeProviderAttribute : System.Attribute
    {

        private string _ProviderTypeName;

        public System.Type ProviderType
        {
            get
            {
                if ((_ProviderTypeName != null) && (_ProviderTypeName.Length != 0))
                    return System.Type.GetType(_ProviderTypeName);
                return null;
            }
        }

        public EnumAttributeProviderAttribute(System.Type providerType) : this(providerType.FullName)
        {
        }

        public EnumAttributeProviderAttribute(string providerTypeName)
        {
            _ProviderTypeName = providerTypeName;
        }

    } // class EnumAttributeProviderAttribute

}

