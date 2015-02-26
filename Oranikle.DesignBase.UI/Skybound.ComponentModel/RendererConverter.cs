using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Skybound.ComponentModel
{

    internal abstract class RendererConverter : System.ComponentModel.ExpandableObjectConverter, System.Collections.IComparer
    {

        private System.ComponentModel.TypeConverter.StandardValuesCollection _StandardValues;
        private System.Collections.Hashtable FriendlyNameToInstanceDescriptorMap;
        private System.Type RendererType;

        protected virtual object DefaultRenderer
        {
            get
            {
                return RendererType.GetProperty("DefaultRenderer", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);
            }
        }

        protected RendererConverter(System.Type rendererType)
        {
            RendererType = rendererType;
        }

        int System.Collections.IComparer.Compare(object x, object y)
        {
            string s1 = Skybound.ComponentModel.DisplayNameAttribute.GetFriendlyName(x.GetType());
            string s2 = Skybound.ComponentModel.DisplayNameAttribute.GetFriendlyName(y.GetType());
            return s1.CompareTo(s2);
        }

        private System.ComponentModel.TypeConverter.StandardValuesCollection GetCachedStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (_StandardValues == null)
            {
                FriendlyNameToInstanceDescriptorMap = new System.Collections.Hashtable();
                System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
                arrayList.Add(DefaultRenderer);
                FriendlyNameToInstanceDescriptorMap["(Default)"] = CreateInstanceDescriptor(DefaultRenderer);
                System.Type[] typeArr = RendererType.Assembly.GetExportedTypes();
                for (int i = 0; i < typeArr.Length; i++)
                {
                    System.Type type = typeArr[i];
                    if (((type == RendererType) || type.IsSubclassOf(RendererType)) && !type.IsAbstract)
                    {
                        string s = Skybound.ComponentModel.DisplayNameAttribute.GetFriendlyName(type);
                        FriendlyNameToInstanceDescriptorMap[s] = new System.ComponentModel.Design.Serialization.InstanceDescriptor(type.GetConstructor(System.Type.EmptyTypes), null, true);
                        arrayList.Add(System.Activator.CreateInstance(type, null));
                    }
                }
                arrayList.Sort(this);
                _StandardValues = new System.ComponentModel.TypeConverter.StandardValuesCollection(arrayList);
            }
            return _StandardValues;
        }

        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType)
        {
            if ((destinationType == typeof(string)) || (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor)))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if ((value is string))
            {
                if (FriendlyNameToInstanceDescriptorMap.ContainsKey(value))
                    return (FriendlyNameToInstanceDescriptorMap[value] as System.ComponentModel.Design.Serialization.InstanceDescriptor).Invoke();
                throw new System.FormatException();
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(string))
                return Skybound.ComponentModel.DisplayNameAttribute.GetFriendlyName(value.GetType());
            if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
                return CreateInstanceDescriptor(value);
            return base.ConvertTo(context, culture, value, destinationType);
        }

        protected virtual System.ComponentModel.Design.Serialization.InstanceDescriptor CreateInstanceDescriptor(object renderer)
        {
            if (renderer == DefaultRenderer)
                return new System.ComponentModel.Design.Serialization.InstanceDescriptor(RendererType.GetProperty("DefaultRenderer", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public), null, true);
            return new System.ComponentModel.Design.Serialization.InstanceDescriptor(renderer.GetType().GetConstructor(System.Type.EmptyTypes), null, renderer.GetType() == RendererType);
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            return GetCachedStandardValues(context);
        }

        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

    } // class RendererConverter

}

