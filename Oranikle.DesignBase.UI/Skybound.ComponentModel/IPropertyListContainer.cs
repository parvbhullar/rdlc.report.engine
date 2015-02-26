using System.ComponentModel;

namespace Skybound.ComponentModel
{

    internal interface IPropertyListContainer : System.ComponentModel.ICustomTypeDescriptor
    {

        Skybound.ComponentModel.PropertyList GetPropertyList();

    }

}

