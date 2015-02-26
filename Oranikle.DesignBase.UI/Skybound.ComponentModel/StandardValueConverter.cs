using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Skybound.ComponentModel
{

    internal abstract class StandardValueConverter : System.ComponentModel.TypeConverter
    {

        private System.Type StandardType;

        protected StandardValueConverter(System.Type standardType)
        {
            StandardType = standardType;
        }

        private System.Reflection.PropertyInfo FindStandardValueProperty(object value)
        {
            System.Reflection.PropertyInfo propertyInfo2;

            System.Reflection.PropertyInfo[] propertyInfoArr = StandardType.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            for (int i = 0; i < propertyInfoArr.Length; i++)
            {
                System.Reflection.PropertyInfo propertyInfo1 = propertyInfoArr[i];
                if (propertyInfo1.IsDefined(typeof(Skybound.ComponentModel.StandardValueAttribute), true) && System.Object.Equals(propertyInfo1.GetValue(null, null), value))
                {
                    return propertyInfo1;
                }
            }
            return null;
        }

        private string GetStandardValueName(System.Reflection.PropertyInfo property)
        {
            string s = (System.Attribute.GetCustomAttribute(property, typeof(Skybound.ComponentModel.StandardValueAttribute)) as Skybound.ComponentModel.StandardValueAttribute).Name;
            if (s == null)
                return property.Name;
            return s;
        }

        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType != typeof(string))
                return base.CanConvertFrom(context, sourceType);
            return true;
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType)
        {
            if ((destinationType != typeof(string)) && (destinationType != typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor)))
                return base.CanConvertTo(context, destinationType);
            return true;
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            object obj;

            if (value is string)
            {
                System.Reflection.PropertyInfo[] propertyInfoArr = StandardType.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                for (int i = 0; i < propertyInfoArr.Length; i++)
                {
                    System.Reflection.PropertyInfo propertyInfo = propertyInfoArr[i];
                    if (propertyInfo.IsDefined(typeof(Skybound.ComponentModel.StandardValueAttribute), true) && GetStandardValueName(propertyInfo) == ((string)value))
                    {
                        return propertyInfo.GetValue(null, null);
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                System.Reflection.PropertyInfo propertyInfo1 = FindStandardValueProperty(value);
                if (propertyInfo1 != null)
                    return GetStandardValueName(propertyInfo1);
                throw new System.FormatException();
            }
            if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
            {
                System.Reflection.PropertyInfo propertyInfo2 = FindStandardValueProperty(value);
                if (propertyInfo2 != null)
                    return new System.ComponentModel.Design.Serialization.InstanceDescriptor(propertyInfo2, null, true);
                throw new System.FormatException();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            System.Collections.ArrayList arrayList1 = new System.Collections.ArrayList();
            System.Collections.ArrayList arrayList2 = new System.Collections.ArrayList();
            System.Reflection.PropertyInfo[] propertyInfoArr = StandardType.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            for (int i = 0; i < propertyInfoArr.Length; i++)
            {
                System.Reflection.PropertyInfo propertyInfo = propertyInfoArr[i];
                if (propertyInfo.IsDefined(typeof(Skybound.ComponentModel.StandardValueAttribute), true))
                {
                    arrayList1.Add(propertyInfo.GetValue(null, null));
                    arrayList2.Add(propertyInfo.Name);
                }
            }
            System.Array array = arrayList1.ToArray();
            System.Array.Sort(arrayList2.ToArray(), array);
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(array);
        }

        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

    } // class StandardValueConverter

}

