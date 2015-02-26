using System;

namespace Skybound.ComponentModel
{

    internal interface IEnumAttributeProvider
    {

        System.Attribute[] GetAttributes(object enumValue);

    }

}

