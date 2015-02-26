using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Skybound.ComponentModel;

namespace Skybound.VisualTips
{

    [System.ComponentModel.TypeConverter(typeof(Skybound.VisualTips.VisualTipConverter))]
    [System.ComponentModel.Editor(typeof(Skybound.VisualTips.VisualTipEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class VisualTip : Skybound.ComponentModel.IPropertyListContainer, System.ComponentModel.ICustomTypeDescriptor
    {

        private bool _DefaultLanguageShouldSerialize;
        private Skybound.VisualTips.VisualTipProvider _Provider;
        private System.Drawing.Rectangle _RelativeToolArea;
        private object CurrentComponent;
        private System.Windows.Forms.Control CurrentControl;
        private bool IsHelpRequestedHandled;
        private Skybound.ComponentModel.PropertyList Properties;

        [System.ComponentModel.AmbientValue(System.Windows.Forms.Shortcut.CtrlShiftX)]
        [System.ComponentModel.Description("The keyboard shortcut that may be pressed to raise the AccessKeyPressed event when the tip is displayed.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Localizable(true)]
        [Skybound.ComponentModel.PropertyListValue]
        public System.Windows.Forms.Shortcut AccessKey
        {
            get
            {
                return (System.Windows.Forms.Shortcut)Properties.GetValue("AccessKey", Provider == null ? 112 : (int)Provider.AccessKey);
            }
            set
            {
                Properties.SetValue("AccessKey", value, Provider == null ? 112 : (int)Provider.AccessKey);
            }
        }

        [System.ComponentModel.Localizable(true)]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("Specifies whether the tip will be displayed.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        public bool Active
        {
            get
            {
                return (bool)Properties.GetValue("Active", true);
            }
            set
            {
                Properties.SetValue("Active", value, true);
            }
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        private bool DefaultLanguageShouldSerialize
        {
            get
            {
                return _DefaultLanguageShouldSerialize;
            }
            set
            {
                _DefaultLanguageShouldSerialize = value;
            }
        }

        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.Description("The message displayed above the text when the tool is disabled.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.AmbientValue(null)]
        public string DisabledMessage
        {
            get
            {
                return (string)Properties.GetValue("DisabledMessage", Provider == null ? "" : Provider.DisabledMessage);
            }
            set
            {
                Properties.SetValue("DisabledMessage", value == null ? "" : value, Provider == null ? "" : Provider.DisabledMessage);
            }
        }

        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.AmbientValue(null)]
        [System.ComponentModel.Description("The alternate text displayed when the tool is disabled.  When this property is blank, the regular text is always used.")]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Localizable(true)]
        public string DisabledText
        {
            get
            {
                return (string)Properties.GetValue("DisabledText", "");
            }
            set
            {
                Properties.SetValue("DisabledText", value == null ? "" : value, "");
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Description("Determines where a visual tip is displayed in relation to the tool area.")]
        [System.ComponentModel.AmbientValue(Skybound.VisualTips.VisualTipDisplayPosition.Bottom)]
        public Skybound.VisualTips.VisualTipDisplayPosition DisplayPosition
        {
            get
            {
                return (Skybound.VisualTips.VisualTipDisplayPosition)Properties.GetValue("DisplayPosition", VisualTipDisplayPosition.Bottom); //(Provider != null) && Provider.DisplayPosition != null);
            }
            set
            {
                Properties.SetValue("DisplayPosition", value, (VisualTipDisplayPosition) (-1));
            }
        }

        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("When this property is false, the disabled message and text and displayed on the tip.")]
        public bool Enabled
        {
            get
            {
                return (bool)Properties.GetValue("Enabled", EnabledDefaultValue);
            }
            set
            {
                Properties.SetValue("Enabled", value, EnabledDefaultValue);
            }
        }

        private bool EnabledDefaultValue
        {
            get
            {
                object obj;

                if (TryGetValue("Enabled", typeof(bool), out obj))
                    return (bool)obj;
                if (CurrentControl == null)
                    return true;
                return CurrentControl.Enabled;
            }
        }

        [System.ComponentModel.Description("The font used to display the tip text.")]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.AmbientValue(null)]
        public System.Drawing.Font Font
        {
            get
            {
                return (System.Drawing.Font)Properties.GetValue("Font", FontDefaultValue);
            }
            set
            {
                Properties.SetValue("Font", value == null ? FontDefaultValue : value, FontDefaultValue);
            }
        }

        private System.Drawing.Font FontDefaultValue
        {
            get
            {
                object obj;

                if (TryGetValue("Font", typeof(System.Drawing.Font), out obj))
                    return (System.Drawing.Font)obj;
                if (CurrentControl == null)
                    return System.Windows.Forms.Control.DefaultFont;
                return CurrentControl.Font;
            }
        }

        [System.ComponentModel.Editor(typeof(Skybound.Drawing.Design.IconImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The image displayed in the footer.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.AmbientValue(null)]
        public System.Drawing.Image FooterImage
        {
            get
            {
                return (System.Drawing.Image)Properties.GetValue("FooterImage", Provider == null ? null : Provider.FooterImage);
            }
            set
            {
                Properties.SetValue("FooterImage", value, Provider == null ? null : Provider.FooterImage);
            }
        }

        [System.ComponentModel.AmbientValue(null)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Localizable(true)]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The text displayed in the footer.")]
        public string FooterText
        {
            get
            {
                return (string)Properties.GetValue("FooterText", Provider == null ? "" : Provider.FooterText);
            }
            set
            {
                Properties.SetValue("FooterText", value == null ? "" : value, Provider == null ? "" : Provider.FooterText);
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.Description("Specifies whether the footer text and image are hidden on this tip.")]
        public bool HideFooter
        {
            get
            {
                return (bool)Properties.GetValue("HideFooter", false);
            }
            set
            {
                Properties.SetValue("HideFooter", value, false);
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The image displayed beside the text.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Editor(typeof(Skybound.Drawing.Design.IconImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.Localizable(true)]
        public System.Drawing.Image Image
        {
            get
            {
                return (System.Drawing.Image)Properties.GetValue("Image", null);
            }
            set
            {
                if ((value != null) && ((value.Width > 128) || (value.Height > 128)))
                    throw new System.InvalidOperationException("The maximum image size is 128x128.");
                Properties.SetValue("Image", value, null);
            }
        }

        private bool IsDefaultLanguage
        {
            get
            {
                return Skybound.VisualTips.VisualTip.GetDesignModeCulture(Provider) == System.Globalization.CultureInfo.InvariantCulture;
            }
        }

        [System.ComponentModel.Description("The maximum width of the tip.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.AmbientValue(256)]
        [Skybound.ComponentModel.PropertyListValue]
        public int MaximumWidth
        {
            get
            {
                return (int)Properties.GetValue("MaximumWidth", Provider == null ? 256 : Provider.MaximumWidth);
            }
            set
            {
                Properties.SetValue("MaximumWidth", System.Math.Max(value, 192), Provider == null ? 256 : Provider.MaximumWidth);
            }
        }

        [System.ComponentModel.Browsable(false)]
        public Skybound.VisualTips.VisualTipProvider Provider
        {
            get
            {
                return _Provider;
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Description("Whether the tip text is displayed using a right-to-left reading order.")]
        [System.ComponentModel.Localizable(true)]
        public System.Windows.Forms.RightToLeft RightToLeft
        {
            get
            {
                return (System.Windows.Forms.RightToLeft)Properties.GetValue("RightToLeft", RightToLeftDefaultValue);
            }
            set
            {
                Properties.SetValue("RightToLeft", value, RightToLeftDefaultValue);
            }
        }

        private System.Windows.Forms.RightToLeft RightToLeftDefaultValue
        {
            get
            {
                object obj;

                if (TryGetValue("RightToLeft", typeof(System.Windows.Forms.RightToLeft), out obj))
                    return (System.Windows.Forms.RightToLeft)obj;
                if (CurrentControl == null)
                    return System.Windows.Forms.RightToLeft.No;
                return CurrentControl.RightToLeft;
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.Description("The shortcut key displayed beside the title, if it is not Shortcut.None.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        public System.Windows.Forms.Shortcut Shortcut
        {
            get
            {
                return (System.Windows.Forms.Shortcut)Properties.GetValue("Shortcut", 0);
            }
            set
            {
                Properties.SetValue("Shortcut", value, 0);
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The text displayed on the tooltip.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Localizable(true)]
        public string Text
        {
            get
            {
                return (string)Properties.GetValue("Text", "");
            }
            set
            {
                Properties.SetValue("Text", value == null ? "" : value, "");
            }
        }

        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The title displayed in bold at the top of the tip.")]
        public string Title
        {
            get
            {
                return (string)Properties.GetValue("Title", "");
            }
            set
            {
                Properties.SetValue("Title", value == null ? "" : value, "");
            }
        }

        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.AmbientValue(null)]
        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The image displayed beside the title.")]
        [System.ComponentModel.Editor(typeof(Skybound.Drawing.Design.IconImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public System.Drawing.Image TitleImage
        {
            get
            {
                return (System.Drawing.Image)Properties.GetValue("TitleImage", Provider == null ? null : Provider.TitleImage);
            }
            set
            {
                if ((value != null) && ((value.Width > 128) || (value.Height > 128)))
                    throw new System.InvalidOperationException("The maximum image size is 128x128.");
                Properties.SetValue("TitleImage", value, Provider == null ? null : Provider.TitleImage);
            }
        }

        public VisualTip()
        {
            Properties = new Skybound.ComponentModel.PropertyList();
            Properties.PropertyChanging += new System.ComponentModel.PropertyChangedEventHandler(Properties_PropertyChanging);
            Properties.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Properties_PropertyChanged);
        }

        public VisualTip(string text) : this(text, null, null, null, System.Windows.Forms.Shortcut.None, false)
        {
        }

        public VisualTip(string text, string title) : this(text, title, null, null, System.Windows.Forms.Shortcut.None, false)
        {
        }

        public VisualTip(string text, string title, System.Drawing.Image image) : this(text, title, image, null, System.Windows.Forms.Shortcut.None, false)
        {
        }

        public VisualTip(string text, string title, System.Drawing.Image image, string disabledText, System.Windows.Forms.Shortcut shortcut, bool hideFooter)
        {
            Properties = new Skybound.ComponentModel.PropertyList();
            Properties.PropertyChanging += new System.ComponentModel.PropertyChangedEventHandler(Properties_PropertyChanging);
            Properties.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Properties_PropertyChanged);
            Text = text;
            Title = title;
            Image = image;
            DisabledText = disabledText;
            Shortcut = shortcut;
            HideFooter = hideFooter;
        }

        private void AddEventHandlers()
        {
            if ((Provider != null) && (CurrentControl != null) && (CurrentComponent == null) && (Provider.DisplayMode == Skybound.VisualTips.VisualTipDisplayMode.HelpRequested))
            {
                CurrentControl.KeyDown += new System.Windows.Forms.KeyEventHandler(CurrentControl_KeyDown);
                CurrentControl.HelpRequested += new System.Windows.Forms.HelpEventHandler(CurrentControl_HelpRequested);
                IsHelpRequestedHandled = true;
            }
        }

        private void CurrentControl_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs e)
        {
            if (System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
                Provider.ShowTip(CurrentControl, Skybound.VisualTips.VisualTipDisplayOptions.HideOnMouseLeave | Skybound.VisualTips.VisualTipDisplayOptions.HideOnLostFocus);
            e.Handled = true;
        }

        private void CurrentControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.F1)
            {
                Provider.ShowTip(CurrentControl, Skybound.VisualTips.VisualTipDisplayOptions.HideOnLostFocus);
                e.Handled = true;
            }
        }

        System.ComponentModel.AttributeCollection System.ComponentModel.ICustomTypeDescriptor.GetAttributes()
        {
            return System.ComponentModel.TypeDescriptor.GetAttributes(this, true);
        }

        string System.ComponentModel.ICustomTypeDescriptor.GetClassName()
        {
            return System.ComponentModel.TypeDescriptor.GetClassName(this, true);
        }

        string System.ComponentModel.ICustomTypeDescriptor.GetComponentName()
        {
            return System.ComponentModel.TypeDescriptor.GetComponentName(this, true);
        }

        System.ComponentModel.TypeConverter System.ComponentModel.ICustomTypeDescriptor.GetConverter()
        {
            return System.ComponentModel.TypeDescriptor.GetConverter(this, true);
        }

        System.ComponentModel.EventDescriptor System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent()
        {
            return System.ComponentModel.TypeDescriptor.GetDefaultEvent(this, true);
        }

        System.ComponentModel.PropertyDescriptor System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty()
        {
            return System.ComponentModel.TypeDescriptor.GetDefaultProperty(this, true);
        }

        object System.ComponentModel.ICustomTypeDescriptor.GetEditor(System.Type editorBaseType)
        {
            return System.ComponentModel.TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        System.ComponentModel.EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
        {
            return System.ComponentModel.TypeDescriptor.GetEvents(this, true);
        }

        System.ComponentModel.EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents(System.Attribute[] attributes)
        {
            return System.ComponentModel.TypeDescriptor.GetEvents(this, attributes, true);
        }

        System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
        {
            return null;// GetProperties(null);
        }

        System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[] attributes)
        {
            return Skybound.ComponentModel.PropertyList.GetProperties(this, attributes);
        }

        Skybound.ComponentModel.PropertyList Skybound.ComponentModel.IPropertyListContainer.GetPropertyList()
        {
            return Properties;
        }

        object System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            return this;
        }

        internal System.Drawing.Rectangle GetRelativeToolArea()
        {
            return _RelativeToolArea;
        }

        internal void OnProviderDisplayModeChanged(System.EventArgs e)
        {
            RemoveEventHandlers();
            AddEventHandlers();
        }

        private void Properties_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (IsDefaultLanguage)
                DefaultLanguageShouldSerialize = Properties.ShouldSerialize();
        }

        private void Properties_PropertyChanging(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!IsDefaultLanguage && !DefaultLanguageShouldSerialize)
                throw new System.InvalidOperationException("New visual tips may not be created while the form is being localized. Set the Language property of the Form or UserControl to (Default) and create a tip in the default language before localizing it.");
        }

        private void RemoveEventHandlers()
        {
            if (IsHelpRequestedHandled)
            {
                CurrentControl.KeyDown -= new System.Windows.Forms.KeyEventHandler(CurrentControl_KeyDown);
                CurrentControl.HelpRequested -= new System.Windows.Forms.HelpEventHandler(CurrentControl_HelpRequested);
                IsHelpRequestedHandled = false;
            }
        }

        internal void Reset()
        {
            Properties.Reset();
        }

        internal void SetProvider(Skybound.VisualTips.VisualTipProvider value)
        {
            RemoveEventHandlers();
            _Provider = value;
            CurrentControl = null;
            CurrentComponent = null;
        }

        internal void SetRelativeToolArea(System.Drawing.Rectangle toolArea)
        {
            _RelativeToolArea = toolArea;
        }

        internal void SetTarget(System.Windows.Forms.Control control, object component)
        {
            CurrentControl = control;
            CurrentComponent = component;
            AddEventHandlers();
        }

        internal bool ShouldSerialize()
        {
            if (!DefaultLanguageShouldSerialize)
                return Properties.ShouldSerialize();
            return true;
        }

        private bool TryGetValue(string propertyName, System.Type propertyType, out object value)
        {
            if (CurrentComponent != null)
            {
                System.Reflection.PropertyInfo propertyInfo = CurrentComponent.GetType().GetProperty(propertyName);
                if ((propertyInfo != null) && (propertyInfo.PropertyType == propertyType))
                {
                    value = propertyInfo.GetValue(CurrentComponent, null);
                    return true;
                }
            }
            value = null;
            return false;
        }

        internal static System.Globalization.CultureInfo GetDesignModeCulture(System.ComponentModel.IComponent component)
        {
            if ((component != null) && (component.Site != null) && component.Site.DesignMode)
            {
                System.ComponentModel.Design.IDesignerHost idesignerHost = (System.ComponentModel.Design.IDesignerHost)component.Site.GetService(typeof(System.ComponentModel.Design.IDesignerHost));
                System.ComponentModel.PropertyDescriptor propertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(idesignerHost.RootComponent)["Language"];
                if ((propertyDescriptor != null) && (propertyDescriptor.PropertyType == typeof(System.Globalization.CultureInfo)))
                    return propertyDescriptor.GetValue(idesignerHost.RootComponent) as System.Globalization.CultureInfo;
            }
            return System.Globalization.CultureInfo.InvariantCulture;
        }

    } // class VisualTip

}

