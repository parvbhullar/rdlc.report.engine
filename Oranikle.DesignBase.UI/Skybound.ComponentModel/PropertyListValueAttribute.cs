using System;

namespace Skybound.ComponentModel
{

    [System.AttributeUsage(System.AttributeTargets.Property)]
    internal sealed class PropertyListValueAttribute : System.Attribute
    {

        public static readonly Skybound.ComponentModel.PropertyListValueAttribute Value;

        public PropertyListValueAttribute()
        {
        }

        static PropertyListValueAttribute()
        {
            Skybound.ComponentModel.PropertyListValueAttribute.Value = new Skybound.ComponentModel.PropertyListValueAttribute();
        }

    } // class PropertyListValueAttribute

}

