using System;
using System.Collections;
using System.ComponentModel;

namespace Skybound.ComponentModel
{

    internal class PropertyList
    {

        private class PropertyListValueDescriptor : System.ComponentModel.PropertyDescriptor
        {

            private System.ComponentModel.PropertyDescriptor Descriptor;

            public override System.Type ComponentType
            {
                get
                {
                    return Descriptor.ComponentType;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return Descriptor.IsReadOnly;
                }
            }

            public override System.Type PropertyType
            {
                get
                {
                    return Descriptor.PropertyType;
                }
            }

            public PropertyListValueDescriptor(System.ComponentModel.PropertyDescriptor original) : base(original)
            {
                Descriptor = original;
            }

            public override bool CanResetValue(object component)
            {
                return true;
            }

            public override object GetValue(object component)
            {
                return Descriptor.GetValue(component);
            }

            public override void ResetValue(object component)
            {
                (component as Skybound.ComponentModel.IPropertyListContainer).GetPropertyList().ResetProperty(Descriptor.Name);
            }

            public override void SetValue(object component, object value)
            {
                Descriptor.SetValue(component, value);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return (component as Skybound.ComponentModel.IPropertyListContainer).GetPropertyList().ShouldSerializeProperty(Descriptor.Name);
            }

        } // class PropertyListValueDescriptor

        private System.Collections.Hashtable Properties;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanging;

        public PropertyList()
        {
        }

        public object GetValue(string propertyName, object defaultValue)
        {
            if (Properties == null)
                return defaultValue;
            if (!Properties.Contains(propertyName))
                return defaultValue;
            return Properties[propertyName];
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        public void Reset()
        {
            if (Properties != null)
            {
                string[] sArr1 = new string[Properties.Count];
                Properties.Keys.CopyTo(sArr1, 0);
                string[] sArr2 = sArr1;
                for (int i = 0; i < sArr2.Length; i++)
                {
                    string s = sArr2[i];
                    ResetProperty(s);
                }
                Properties = null;
            }
        }

        public void ResetProperty(string propertyName)
        {
            if ((Properties != null) && Properties.Contains(propertyName))
            {
                OnPropertyChanging(propertyName);
                Properties.Remove(propertyName);
                OnPropertyChanged(propertyName);
            }
        }

        public void SetValue(string propertyName, object value, object ambientValue)
        {
            if (System.Object.Equals(value, ambientValue))
            {
                ResetProperty(propertyName);
                return;
            }
            if (Properties == null)
                Properties = new System.Collections.Hashtable();
            OnPropertyChanging(propertyName);
            Properties[propertyName] = value;
            OnPropertyChanged(propertyName);
        }

        public bool ShouldSerialize()
        {
            if (Properties != null)
                return Properties.Count > 0;
            return false;
        }

        public bool ShouldSerializeProperty(string propertyName)
        {
            if (Properties != null)
                return Properties.Contains(propertyName);
            return false;
        }

        public static System.ComponentModel.PropertyDescriptorCollection GetProperties(object container, System.Attribute[] attributes)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor in System.ComponentModel.TypeDescriptor.GetProperties(container, attributes, true))
            {
                if (propertyDescriptor.Attributes.Contains(Skybound.ComponentModel.PropertyListValueAttribute.Value))
                    arrayList.Add(new Skybound.ComponentModel.PropertyList.PropertyListValueDescriptor(propertyDescriptor));
                else
                    arrayList.Add(propertyDescriptor);
            }
            return new System.ComponentModel.PropertyDescriptorCollection((System.ComponentModel.PropertyDescriptor[])arrayList.ToArray(typeof(System.ComponentModel.PropertyDescriptor)));
        }

    } // class PropertyList

}

