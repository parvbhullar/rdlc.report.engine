using System;

namespace Skybound.ComponentModel
{

    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    internal sealed class DisplayNameAttribute : System.Attribute
    {

        private string _FriendlyName;

        public string FriendlyName
        {
            get
            {
                return _FriendlyName;
            }
        }

        public DisplayNameAttribute(string friendlyName)
        {
            _FriendlyName = friendlyName;
        }

        public static string GetFriendlyName(System.Type type)
        {
            object[] objArr = type.GetCustomAttributes(typeof(Skybound.ComponentModel.DisplayNameAttribute), false);
            if ((objArr == null) || (objArr.Length == 0))
                return type.FullName;
            return (objArr[0] as Skybound.ComponentModel.DisplayNameAttribute).FriendlyName;
        }

    } // class DisplayNameAttribute

}

