using System;

namespace Skybound.ComponentModel
{

    [System.AttributeUsage(System.AttributeTargets.Property)]
    internal sealed class StandardValueAttribute : System.Attribute
    {

        public string Name;

        public StandardValueAttribute()
        {
        }

        public StandardValueAttribute(string name)
        {
            Name = name;
        }

    } // class StandardValueAttribute

}

