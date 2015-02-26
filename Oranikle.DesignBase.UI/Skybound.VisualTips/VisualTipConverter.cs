using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Skybound.VisualTips
{

    internal class VisualTipConverter : System.ComponentModel.ExpandableObjectConverter
    {

        public VisualTipConverter()
        {
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType)
        {
            if ((destinationType == typeof(string)) || (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor)))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {
            Skybound.VisualTips.VisualTip visualTip = value as Skybound.VisualTips.VisualTip;
            if (destinationType == typeof(string))
            {
                if (!visualTip.ShouldSerialize())
                    return "(none)";
                return "(VisualTip)";
            }
            if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
                return new System.ComponentModel.Design.Serialization.InstanceDescriptor(typeof(Skybound.VisualTips.VisualTip).GetConstructor(System.Type.EmptyTypes), null, false);
            return base.ConvertTo(context, culture, value, destinationType);
        }

    } // class VisualTipConverter

}

