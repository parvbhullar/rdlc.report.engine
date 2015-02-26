using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Skybound.VisualTips.Rendering;

namespace Skybound.VisualTips
{

    [System.ComponentModel.ProvideProperty("VisualTip", typeof(System.Object))]
    [System.ComponentModel.DefaultEvent("AccessKeyPressed")]
    public class VisualTipProvider : System.ComponentModel.Component, System.ComponentModel.IExtenderProvider
    {

        private delegate void ShowTipCoreMethod(System.Windows.Forms.Control control, object component, Skybound.VisualTips.VisualTip tip, System.Drawing.Rectangle toolArea, Skybound.VisualTips.VisualTipDisplayOptions options);

        private class NotifyImages
        {

            public static readonly System.Drawing.Image Error;
            public static readonly System.Drawing.Image Information;
            public static readonly System.Drawing.Image Warning;

            static NotifyImages()
            {
                System.Reflection.Assembly assembly = typeof(Skybound.VisualTips.VisualTipProvider.NotifyImages).Assembly;
                System.Type type = typeof(Skybound.VisualTips.VisualTipProvider.NotifyImages);
                Skybound.VisualTips.VisualTipProvider.NotifyImages.Error = System.Drawing.Image.FromStream(assembly.GetManifestResourceStream(type, "NotifyError.png"));
                Skybound.VisualTips.VisualTipProvider.NotifyImages.Warning = System.Drawing.Image.FromStream(assembly.GetManifestResourceStream(type, "NotifyWarning.png"));
                Skybound.VisualTips.VisualTipProvider.NotifyImages.Information = System.Drawing.Image.FromStream(assembly.GetManifestResourceStream(type, "NotifyInformation.png"));
            }

            public NotifyImages()
            {
            }

            public static System.Drawing.Image FromIcon(Skybound.VisualTips.VisualTipNotifyIcon icon)
            {
                switch (icon)
                {
                    case Skybound.VisualTips.VisualTipNotifyIcon.Error:
                        return Skybound.VisualTips.VisualTipProvider.NotifyImages.Error;

                    case Skybound.VisualTips.VisualTipNotifyIcon.Warning:
                        return Skybound.VisualTips.VisualTipProvider.NotifyImages.Warning;

                    case Skybound.VisualTips.VisualTipNotifyIcon.Information:
                        return Skybound.VisualTips.VisualTipProvider.NotifyImages.Information;
                }
                return null;
            }

        } // class NotifyImages

        private class StatusBarExtender : Skybound.VisualTips.IVisualTipExtender
        {

            private const int SB_GETRECT = 1034;

            public StatusBarExtender()
            {
            }

            public System.Drawing.Rectangle GetBounds(object component)
            {
                System.Windows.Forms.StatusBar statusBar = (component as System.Windows.Forms.StatusBarPanel).Parent;
                int[] iArr = new int[4];
                Skybound.VisualTips.VisualTipProvider.StatusBarExtender.SendMessage(statusBar.Handle, 1034, new System.IntPtr(statusBar.Panels.IndexOf(component as System.Windows.Forms.StatusBarPanel)), iArr);
                return System.Drawing.Rectangle.FromLTRB(iArr[0], iArr[1], iArr[2], iArr[3]);
            }

            public object GetChildAtPoint(System.Windows.Forms.Control control, int x, int y)
            {
                object obj;

                System.Windows.Forms.StatusBar statusBar = control as System.Windows.Forms.StatusBar;
                System.Drawing.Size size = System.Windows.Forms.SystemInformation.Border3DSize;
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(size.Width, size.Height, 0, statusBar.Height - (size.Height * 2));
                foreach (System.Windows.Forms.StatusBarPanel statusBarPanel in statusBar.Panels)
                {
                    rectangle.Width = statusBarPanel.Width;
                    if (rectangle.Contains(x, y))
                    {
                        return statusBarPanel;
                    }
                    rectangle.X += rectangle.Width + size.Width;
                }
                return null;
            }

            public System.Type[] GetChildTypes()
            {
                return new System.Type[] { typeof(System.Windows.Forms.StatusBarPanel) };
            }

            public object GetParent(object component)
            {
                return (component as System.Windows.Forms.StatusBarPanel).Parent;
            }

            [System.Runtime.InteropServices.PreserveSig]
            [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
            private static extern System.IntPtr SendMessage(System.IntPtr hwnd, int wMsg, System.IntPtr wParam, int[] lParam);

        } // class StatusBarExtender

        private class ToolBarExtender : Skybound.VisualTips.IVisualTipExtender
        {

            public ToolBarExtender()
            {
            }

            public System.Drawing.Rectangle GetBounds(object component)
            {
                return (component as System.Windows.Forms.ToolBarButton).Rectangle;
            }

            public object GetChildAtPoint(System.Windows.Forms.Control control, int x, int y)
            {
                object obj;

                System.Windows.Forms.ToolBar toolBar = control as System.Windows.Forms.ToolBar;
                foreach (System.Windows.Forms.ToolBarButton toolBarButton in toolBar.Buttons)
                {
                    System.Drawing.Rectangle rectangle = toolBarButton.Rectangle;
                    if (rectangle.Contains(x, y))
                    {
                        return toolBarButton;
                    }
                }
                return null;
            }

            public System.Type[] GetChildTypes()
            {
                return new System.Type[] { typeof(System.Windows.Forms.ToolBarButton) };
            }

            public object GetParent(object component)
            {
                return (component as System.Windows.Forms.ToolBarButton).Parent;
            }

        } // class ToolBarExtender

        private class ToolStripExtender : Skybound.VisualTips.IVisualTipExtender
        {

            public ToolStripExtender()
            {
            }

            public System.Drawing.Rectangle GetBounds(object component)
            {
                return (System.Drawing.Rectangle)component.GetType().GetMethod("get_Bounds", System.Type.EmptyTypes).Invoke(component, null);
            }

            public object GetChildAtPoint(System.Windows.Forms.Control control, int x, int y)
            {
                System.Type[] typeArr = new System.Type[] {
                                                            typeof(int), 
                                                            typeof(int) };
                object[] objArr = new object[] {
                                                 x, 
                                                 y };
                return control.GetType().GetMethod("GetItemAt", typeArr).Invoke(control, objArr);
            }

            public System.Type[] GetChildTypes()
            {
                return new System.Type[] { Skybound.VisualTips.Version2Types.ToolStripItem };
            }

            public object GetParent(object component)
            {
                return component.GetType().GetMethod("GetCurrentParent", System.Type.EmptyTypes).Invoke(component, null);
            }

        } // class ToolStripExtender

        internal class VisualTipWindowStack : System.Collections.ReadOnlyCollectionBase
        {

            private struct WindowStackItem
            {

                public System.Windows.Forms.Control Control;
                public Skybound.VisualTips.VisualTipWindow Window;

            }

            public VisualTipWindowStack()
            {
            }

            public void Add(Skybound.VisualTips.VisualTipWindow window, System.Windows.Forms.Control control)
            {
                Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack.WindowStackItem windowStackItem;

                UndisplayWindow(control);
                windowStackItem = new Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack.WindowStackItem();
                windowStackItem.Window = window;
                windowStackItem.Control = control;
                InnerList.Add(windowStackItem);
            }

            public bool ProcessKeyDown(System.Windows.Forms.Keys key)
            {
                if (key == System.Windows.Forms.Keys.Escape)
                    return UndisplayWindow(null);
                if (InnerList.Count > 0)
                {
                    Skybound.VisualTips.VisualTip visualTip = ((Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack.WindowStackItem)InnerList[InnerList.Count - 1]).Window.DisplayedTip;
                    if (((System.Windows.Forms.Shortcut)visualTip.AccessKey) == ((System.Windows.Forms.Shortcut)key))
                    {
                        Skybound.VisualTips.VisualTipEventArgs visualTipEventArgs = new Skybound.VisualTips.VisualTipEventArgs(visualTip.Provider.CurrentTip, visualTip.Provider.CurrentComponent != null ? visualTip.Provider.CurrentComponent : visualTip.Provider.CurrentControl);
                        UndisplayWindow(null);
                        visualTip.Provider.OnAccessKeyPressed(visualTipEventArgs);
                        return true;
                    }
                }
                return false;
            }

            public void Remove(Skybound.VisualTips.VisualTipWindow window)
            {
                for (int i = InnerList.Count - 1; i >= 0; i--)
                {
                    if (((Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack.WindowStackItem)InnerList[InnerList.Count - 1]).Window == window)
                    {
                        InnerList.RemoveAt(i);
                        return;
                    }
                }
            }

            private bool UndisplayWindow(System.Windows.Forms.Control control)
            {
                for (int i = InnerList.Count - 1; i >= 0; i--)
                {
                    Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack.WindowStackItem windowStackItem = (Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack.WindowStackItem)InnerList[i];
                    if (windowStackItem.Window.IsDisplayed)
                    {
                        if ((control != null) && (windowStackItem.Control != control))
                            goto label_1;
                        bool flag = (windowStackItem.Window.Options & Skybound.VisualTips.VisualTipDisplayOptions.ForwardEscapeKey) == Skybound.VisualTips.VisualTipDisplayOptions.Default;
                        windowStackItem.Window.Undisplay();
                        return flag;
                    }
                    InnerList.RemoveAt(i);
                label_1:;
                }
                return false;
            }

        } // class VisualTipWindowStack

        private const System.Windows.Forms.Shortcut AccessKeyDefault = (System.Windows.Forms.Shortcut.Ins | System.Windows.Forms.Shortcut.CtrlB);
        private const int EM_POSFROMCHAR = 214;

        private System.Windows.Forms.Shortcut _AccessKey;
        private Skybound.VisualTips.VisualTipAnimation _Animation;
        private Skybound.VisualTips.VisualTip _CurrentTip;
        private string _DisabledMessage;
        private bool _DisplayAtMousePosition;
        private Skybound.VisualTips.VisualTipDisplayMode _DisplayMode;
        private Skybound.VisualTips.VisualTipDisplayPosition _DisplayPosition;
        private System.Drawing.Image _FooterImage;
        private string _FooterText;
        private int _InitialDelay;
        private int _MaximumWidth;
        private double _Opacity;
        private Skybound.VisualTips.Rendering.VisualTipRenderer _Renderer;
        private int _ReshowDelay;
        private Skybound.VisualTips.VisualTipShadow _Shadow;
        private bool _ShowAlways;
        private System.Drawing.Image _TitleImage;

        private object CurrentComponent;
        private System.Windows.Forms.Control CurrentControl;
        private Skybound.VisualTips.VisualTipWindow CurrentTipWindow;

        private System.Collections.Hashtable VisualTipMap;

        private static Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack _WindowStack;
        private static object DisplayModeChangedEvent;
        private static object DisplayPositionChangedEvent;
        private static System.Collections.Hashtable Extenders;
        private static System.Collections.ArrayList Instances;
        private static Skybound.VisualTips.VisualTipProvider NotifyProvider;
        private static Skybound.VisualTips.VisualTip PreventDisplayTip;
        private static object RendererChangedEvent;
        private static Skybound.VisualTips.VisualTipWindow TrackedTipWindow;

        [System.ComponentModel.Description("Occurs when the access key is pressed for a VisualTip.")]
        [System.ComponentModel.Category("Key")]
        public event Skybound.VisualTips.VisualTipEventHandler AccessKeyPressed;
        [System.ComponentModel.Category("Property Changed")]
        [System.ComponentModel.Description("Occurs when the value of the DisplayMode property is changed.")]
        public event System.EventHandler DisplayModeChanged
        {
            add
            {
                Events.AddHandler(Skybound.VisualTips.VisualTipProvider.DisplayModeChangedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(Skybound.VisualTips.VisualTipProvider.DisplayModeChangedEvent, value);
            }
        }
        [System.ComponentModel.Description("Occurs when the value of the DisplayPosition property is changed.")]
        [System.ComponentModel.Category("Property Changed")]
        public event System.EventHandler DisplayPositionChanged
        {
            add
            {
                Events.AddHandler(Skybound.VisualTips.VisualTipProvider.DisplayPositionChangedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(Skybound.VisualTips.VisualTipProvider.DisplayPositionChangedEvent, value);
            }
        }
        [System.ComponentModel.Description("Occurs when the value of the Renderer property is changed.")]
        [System.ComponentModel.Category("Property Changed")]
        public event System.EventHandler RendererChanged
        {
            add
            {
                Events.AddHandler(Skybound.VisualTips.VisualTipProvider.RendererChangedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(Skybound.VisualTips.VisualTipProvider.RendererChangedEvent, value);
            }
        }
        [System.ComponentModel.Description("Occurs before a VisualTip is displayed.")]
        public event Skybound.VisualTips.VisualTipEventHandler TipPopup;

        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.Description("The keyboard shortcut that may be pressed to raise the AccessKeyPressed event when a tip is displayed.")]
        [System.ComponentModel.Category("Behavior")]
        public System.Windows.Forms.Shortcut AccessKey
        {
            get
            {
                if (!ShouldSerializeAccessKey())
                {
                    if (DisplayMode != Skybound.VisualTips.VisualTipDisplayMode.MouseHover)
                        return System.Windows.Forms.Shortcut.None;
                    return System.Windows.Forms.Shortcut.F1;
                }
                return _AccessKey;
            }
            set
            {
                _AccessKey = value;
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Determines if and how a VisualTip is animated when it is displayed.")]
        [System.ComponentModel.DefaultValue(Skybound.VisualTips.VisualTipAnimation.SystemDefault)]
        public Skybound.VisualTips.VisualTipAnimation Animation
        {
            get
            {
                return _Animation;
            }
            set
            {
                if (!System.Enum.IsDefined(typeof(Skybound.VisualTips.VisualTipAnimation), value))
                    throw new System.ComponentModel.InvalidEnumArgumentException("Animation", (int)value, typeof(Skybound.VisualTips.VisualTipAnimation));
                _Animation = value;
            }
        }

        private Skybound.VisualTips.VisualTip CurrentTip
        {
            get
            {
                return _CurrentTip;
            }
        }

        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.Description("The title message written in bold above the text when the control is disabled.")]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Category("Appearance")]
        public string DisabledMessage
        {
            get
            {
                if (_DisabledMessage != null)
                    return _DisabledMessage;
                return "";
            }
            set
            {
                _DisabledMessage = value;
            }
        }

        [System.ComponentModel.Description("Whether VisualTips are displayed at the mouse position.")]
        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.DefaultValue(true)]
        public bool DisplayAtMousePosition
        {
            get
            {
                return _DisplayAtMousePosition;
            }
            set
            {
                _DisplayAtMousePosition = value;
            }
        }

        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.DefaultValue(Skybound.VisualTips.VisualTipDisplayMode.MouseHover)]
        [System.ComponentModel.Description("Determines when a visual tip is displayed.")]
        public Skybound.VisualTips.VisualTipDisplayMode DisplayMode
        {
            get
            {
                return _DisplayMode;
            }
            set
            {
                if (!System.Enum.IsDefined(typeof(Skybound.VisualTips.VisualTipDisplayMode), value))
                    throw new System.ComponentModel.InvalidEnumArgumentException("DisplayMode", (int)value, typeof(Skybound.VisualTips.VisualTipDisplayMode));
                if (_DisplayMode != value)
                {
                    _DisplayMode = value;
                    foreach (Skybound.VisualTips.VisualTip visualTip in VisualTipMap.Values)
                    {
                        if (visualTip != null)
                            visualTip.OnProviderDisplayModeChanged(System.EventArgs.Empty);
                    }
                    OnDisplayModeChanged(System.EventArgs.Empty);
                }
            }
        }

        [System.ComponentModel.DefaultValue(Skybound.VisualTips.VisualTipDisplayPosition.Bottom)]
        [System.ComponentModel.Description("Determines where a visual tip is displayed in relation to the tool area.")]
        [System.ComponentModel.Category("Behavior")]
        public Skybound.VisualTips.VisualTipDisplayPosition DisplayPosition
        {
            get
            {
                return _DisplayPosition;
            }
            set
            {
                if (!System.Enum.IsDefined(typeof(Skybound.VisualTips.VisualTipDisplayPosition), value))
                    throw new System.ComponentModel.InvalidEnumArgumentException("DisplayPosition", (int)value, typeof(Skybound.VisualTips.VisualTipDisplayPosition));
                if (_DisplayPosition != value)
                {
                    _DisplayPosition = value;
                    OnDisplayPositionChanged(System.EventArgs.Empty);
                }
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.DefaultValue(null)]
        [System.ComponentModel.Editor(typeof(Skybound.Drawing.Design.IconImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.Description("The default image displayed beside the footer text on a VisualTip.")]
        public System.Drawing.Image FooterImage
        {
            get
            {
                return _FooterImage;
            }
            set
            {
                if ((value != null) && ((value.Width > 128) || (value.Height > 128)))
                    throw new System.InvalidOperationException("The maximum image size is 128x128.");
                _FooterImage = value;
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Description("The default footer text on a VisualTip.")]
        public string FooterText
        {
            get
            {
                if (_FooterText != null)
                    return _FooterText;
                return "";
            }
            set
            {
                _FooterText = value;
            }
        }

        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.Description("The amount of time which passes, in milliseconds, before a tip is displayed.")]
        public int InitialDelay
        {
            get
            {
                return _InitialDelay;
            }
            set
            {
                if (_InitialDelay != value)
                {
                    Skybound.VisualTips.VisualTipTracker.RemoveHandler(_InitialDelay, new System.EventHandler(OnMouseHover));
                    _InitialDelay = value;
                    Skybound.VisualTips.VisualTipTracker.AddHandler(_InitialDelay, new System.EventHandler(OnMouseHover));
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsTipDisplayed
        {
            get
            {
                if (CurrentTipWindow != null)
                    return CurrentTipWindow.IsDisplayed;
                return false;
            }
        }

        [System.ComponentModel.Description("The maximum width of a VisualTip.")]
        [System.ComponentModel.DefaultValue(256)]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.Category("Appearance")]
        public int MaximumWidth
        {
            get
            {
                return _MaximumWidth;
            }
            set
            {
                _MaximumWidth = System.Math.Max(value, 192);
            }
        }

        [System.ComponentModel.TypeConverter(typeof(System.Windows.Forms.OpacityConverter))]
        [System.ComponentModel.DefaultValue(0.94)]
        [System.ComponentModel.Description("The opacity level of the tip, ranging from 0% (transparent) to 100% (opaque).")]
        [System.ComponentModel.Category("Appearance")]
        public double Opacity
        {
            get
            {
                return _Opacity;
            }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                else if (value > 1.0)
                    value = 1.0;
                _Opacity = value;
            }
        }

        [System.ComponentModel.Description("The renderer used to draw and measure a tip.")]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Category("Appearance")]
        public Skybound.VisualTips.Rendering.VisualTipRenderer Renderer
        {
            get
            {
                return _Renderer;
            }
            set
            {
                if (value == null)
                    value = Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRenderer;
                if (_Renderer != value)
                {
                    _Renderer = value;
                    OnRendererChanged(System.EventArgs.Empty);
                }
            }
        }

        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.Description("The amount of time which must pass, in milliseconds, before a tip is displayed when the mouse is moved from one component to another.")]
        public int ReshowDelay
        {
            get
            {
                return _ReshowDelay;
            }
            set
            {
                _ReshowDelay = value;
            }
        }

        [System.ComponentModel.DefaultValue(Skybound.VisualTips.VisualTipShadow.SystemDefault)]
        [System.ComponentModel.Description("Determines whether a VisualTip has a shadow.")]
        [System.ComponentModel.Category("Appearance")]
        public Skybound.VisualTips.VisualTipShadow Shadow
        {
            get
            {
                return _Shadow;
            }
            set
            {
                if (!System.Enum.IsDefined(typeof(Skybound.VisualTips.VisualTipShadow), value))
                    throw new System.ComponentModel.InvalidEnumArgumentException("Shadow", (int)value, typeof(Skybound.VisualTips.VisualTipShadow));
                _Shadow = value;
            }
        }

        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.Description("Whether VisualTips are displayed even when the form does not have the input focus.")]
        public bool ShowAlways
        {
            get
            {
                return _ShowAlways;
            }
            set
            {
                _ShowAlways = value;
            }
        }

        [System.ComponentModel.Editor(typeof(Skybound.Drawing.Design.IconImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.DefaultValue(null)]
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The image displayed beside the title of a visual tip which does not have another image assigned.")]
        [System.ComponentModel.Localizable(true)]
        public System.Drawing.Image TitleImage
        {
            get
            {
                return _TitleImage;
            }
            set
            {
                if ((value != null) && ((value.Width > 128) || (value.Height > 128)))
                    throw new System.InvalidOperationException("The maximum image size is 128x128.");
                _TitleImage = value;
            }
        }

        public override System.ComponentModel.ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                if ((value != null) && value.DesignMode)
                    RemoveInstance();
                base.Site = value;
            }
        }

        internal static System.Windows.Forms.Control TrackedControl
        {
            get
            {
                if (Skybound.VisualTips.VisualTipTracker.TrackingProvider != null)
                    return Skybound.VisualTips.VisualTipTracker.TrackingProvider.CurrentControl;
                return null;
            }
        }

        public static Skybound.VisualTips.VisualTip TrackedTip
        {
            get
            {
                if (Skybound.VisualTips.VisualTipTracker.TrackingProvider != null)
                    return Skybound.VisualTips.VisualTipTracker.TrackingProvider.CurrentTip;
                return null;
            }
        }

        internal static Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack WindowStack
        {
            get
            {
                return Skybound.VisualTips.VisualTipProvider._WindowStack;
            }
        }

        public VisualTipProvider()
        {
            _Renderer = Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRenderer;
            _Opacity = 0.94;
            _MaximumWidth = 256;
            _AccessKey = System.Windows.Forms.Shortcut.Ins | System.Windows.Forms.Shortcut.CtrlB;
            _InitialDelay = System.Windows.Forms.SystemInformation.DoubleClickTime * 2;
            _ReshowDelay = System.Windows.Forms.SystemInformation.DoubleClickTime;
            _DisplayAtMousePosition = true;
            VisualTipMap = new System.Collections.Hashtable();
            Skybound.VisualTips.VisualTipTracker.AddHandler(InitialDelay, new System.EventHandler(OnMouseHover));
            Skybound.VisualTips.VisualTipProvider.Instances.Add(this);
            Skybound.VisualTips.VisualTipTracker.Enable();
        }

        public VisualTipProvider(System.ComponentModel.IContainer container)
        {
            _Renderer = Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRenderer;
            _Opacity = 0.94;
            _MaximumWidth = 256;
            _AccessKey = System.Windows.Forms.Shortcut.Ins | System.Windows.Forms.Shortcut.CtrlB;
            _InitialDelay = System.Windows.Forms.SystemInformation.DoubleClickTime * 2;
            _ReshowDelay = System.Windows.Forms.SystemInformation.DoubleClickTime;
            _DisplayAtMousePosition = true;
            VisualTipMap = new System.Collections.Hashtable();
            Skybound.VisualTips.VisualTipTracker.AddHandler(InitialDelay, new System.EventHandler(OnMouseHover));
            Skybound.VisualTips.VisualTipProvider.Instances.Add(this);
            Skybound.VisualTips.VisualTipTracker.Enable();
            if (container == null)
                throw new System.ArgumentNullException("container");
            container.Add(this);
        }

        static VisualTipProvider()
        {
            Skybound.VisualTips.VisualTipProvider.Instances = new System.Collections.ArrayList();
            Skybound.VisualTips.VisualTipProvider.Extenders = new System.Collections.Hashtable();
            Skybound.VisualTips.VisualTipProvider.RendererChangedEvent = new System.Object();
            Skybound.VisualTips.VisualTipProvider.DisplayModeChangedEvent = new System.Object();
            Skybound.VisualTips.VisualTipProvider.DisplayPositionChangedEvent = new System.Object();
            Skybound.VisualTips.VisualTipProvider._WindowStack = new Skybound.VisualTips.VisualTipProvider.VisualTipWindowStack();
            Skybound.VisualTips.VisualTipProvider.SetExtender(typeof(System.Windows.Forms.ToolBar), new Skybound.VisualTips.VisualTipProvider.ToolBarExtender());
            Skybound.VisualTips.VisualTipProvider.SetExtender(typeof(System.Windows.Forms.StatusBar), new Skybound.VisualTips.VisualTipProvider.StatusBarExtender());
            if (System.Environment.Version.Major >= 2)
                Skybound.VisualTips.VisualTipProvider.SetExtender(Skybound.VisualTips.Version2Types.ToolStrip, new Skybound.VisualTips.VisualTipProvider.ToolStripExtender());
        }

        public bool CanExtend(object extendee)
        {
            if (extendee == null)
                return false;
            System.Type type = extendee.GetType();
            if (type.IsSubclassOf(typeof(System.Windows.Forms.Control)))
                return true;
            if (typeof(System.Windows.Forms.ToolBarButton).IsAssignableFrom(type))
                return true;
            if (typeof(System.Windows.Forms.StatusBarPanel).IsAssignableFrom(type))
                return true;
            if ((type.GetEvent("MouseEnter") == null) || (type.GetEvent("MouseLeave") == null))
                return type.GetField("IsVisualTipComponent", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) != null;
            return true;
        }

        private Skybound.VisualTips.VisualTip GetEffectiveVisualTip(System.Windows.Forms.Control control, object component)
        {
            Skybound.VisualTips.VisualTip visualTip = null;
            if (component != null)
                visualTip = VisualTipMap[component] as Skybound.VisualTips.VisualTip;
            if (visualTip == null)
                visualTip = VisualTipMap[control] as Skybound.VisualTips.VisualTip;
            if ((visualTip != null) && !visualTip.Active)
                visualTip = null;
            return visualTip;
        }

        private System.Drawing.Rectangle GetToolArea(System.Windows.Forms.Control control, object component)
        {
            System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.Empty;
            if (!DisplayAtMousePosition)
            {
                if (component == null)
                {
                    if (control is System.Windows.Forms.Form)
                    {
                        rectangle = System.Drawing.Rectangle.Empty;
                        goto label_1;
                    }
                    return control.Parent.RectangleToScreen(control.Bounds);
                }
                Skybound.VisualTips.IVisualTipExtender ivisualTipExtender = Skybound.VisualTips.VisualTipProvider.GetExtender(control.GetType());
                rectangle = ivisualTipExtender != null ? ivisualTipExtender.GetBounds(component) : System.Drawing.Rectangle.Empty;
            }
        label_1:
            if (rectangle != System.Drawing.Rectangle.Empty)
                return control.RectangleToScreen(rectangle);
            return new System.Drawing.Rectangle(System.Windows.Forms.Cursor.Position, new System.Drawing.Size(16, 16));
        }

        [System.ComponentModel.Description("The VisualTip displayed when the mouse hovers over the current component.")]
        [System.ComponentModel.MergableProperty(false)]
        public Skybound.VisualTips.VisualTip GetVisualTip(object component)
        {
            Skybound.VisualTips.VisualTip visualTip = VisualTipMap[component] as Skybound.VisualTips.VisualTip;
            if (visualTip == null)
            {
                visualTip = new Skybound.VisualTips.VisualTip();
                VisualTipMap[component] = new Skybound.VisualTips.VisualTip();
                visualTip.SetProvider(this);
                Skybound.VisualTips.VisualTipProvider.UpdateTipTarget(visualTip, component);
            }
            return visualTip;
        }

        [System.ComponentModel.Description("All of the VisualTip.")]
        [System.ComponentModel.MergableProperty(false)]
        public Skybound.VisualTips.VisualTip[] GetVisualTips()
        {
            System.Collections.Generic.List<Skybound.VisualTips.VisualTip> list = new System.Collections.Generic.List<Skybound.VisualTips.VisualTip>();
            foreach (object obj in VisualTipMap.Values)
            {
                list.Add((Skybound.VisualTips.VisualTip)obj);
            }
            return list.ToArray();
        }

        private void HideShowTip(System.Windows.Forms.Control control)
        {
            control.MouseDown -= new System.Windows.Forms.MouseEventHandler(ShowTipControl_MouseDown);
            control.LostFocus -= new System.EventHandler(ShowTipControl_LostFocus);
            control.KeyDown -= new System.Windows.Forms.KeyEventHandler(ShowTipControl_KeyDown);
            control.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(ShowTipControl_KeyPress);
            control.HandleDestroyed -= new System.EventHandler(ShowTipControl_HandleDestroyed);
            System.Windows.Forms.Form form = control.FindForm();
            if (form != null)
                form.Deactivate -= new System.EventHandler(ShowTipControl_Form_Deactivate);
            if (CurrentControl == control)
                HideTip();
        }

        public void HideTip()
        {
            if (CurrentTipWindow != null)
                CurrentTipWindow.Undisplay();
        }

        internal void OnMouseHover(object sender, System.EventArgs e)
        {
            if ((DisplayMode == Skybound.VisualTips.VisualTipDisplayMode.MouseHover) && (CurrentTip == null))
            {
                if ((Skybound.VisualTips.VisualTipTracker.TrackingProvider != null) && (Skybound.VisualTips.VisualTipTracker.TrackingProvider != this))
                    return;
                System.Windows.Forms.Control control = Skybound.VisualTips.VisualTipTracker.LastMouseEventControl;
                if ((control != null) && !control.IsDisposed)
                {
                    System.Windows.Forms.Form form1 = System.Windows.Forms.Form.ActiveForm;
                    if (form1 == null)
                        return;
                    if (!ShowAlways)
                    {
                        System.Windows.Forms.Form form2 = control.FindForm();
                        if ((form2 == null) || (form2 == form1) || (form2.IsMdiChild && (form2.MdiParent == form1) && (form2.MdiParent.ActiveMdiChild != form2)))
                            return;
                    }
                    object obj = Skybound.VisualTips.VisualTipProvider.GetComponentAtPoint(control, control.PointToClient(System.Windows.Forms.Cursor.Position));
                    Skybound.VisualTips.VisualTip visualTip = GetEffectiveVisualTip(control, obj);
                    if (visualTip != null)
                    {
                        if (visualTip == Skybound.VisualTips.VisualTipProvider.PreventDisplayTip)
                            return;
                        if (visualTip != CurrentTip)
                            ShowTipCore(control, obj, visualTip, GetToolArea(control, obj), (Skybound.VisualTips.VisualTipDisplayOptions)(72 | (int)visualTip.DisplayPosition));
                    }
                }
            }
        }

        internal void OnUndisplay(Skybound.VisualTips.VisualTipWindow window)
        {
            if ((window == Skybound.VisualTips.VisualTipProvider.TrackedTipWindow) && (((Skybound.VisualTips.VisualTipProvider)Skybound.VisualTips.VisualTipTracker.TrackingProvider) == this))
                Skybound.VisualTips.VisualTipTracker.TrackingProvider = null;
            _CurrentTip = null;
            CurrentControl = null;
            CurrentComponent = null;
            Skybound.VisualTips.VisualTipProvider.WindowStack.Remove(window);
        }

        private void RemoveInstance()
        {
            if (Skybound.VisualTips.VisualTipProvider.Instances.Contains(this))
            {
                Skybound.VisualTips.VisualTipProvider.Instances.Remove(this);
                if (Skybound.VisualTips.VisualTipProvider.Instances.Count == 0)
                    Skybound.VisualTips.VisualTipTracker.Disable();
            }
        }

        private void ResetAccessKey()
        {
            _AccessKey = System.Windows.Forms.Shortcut.Ins | System.Windows.Forms.Shortcut.CtrlB;
        }

        private void ResetInitialDelay()
        {
            _InitialDelay = System.Windows.Forms.SystemInformation.DoubleClickTime * 2;
        }

        private void ResetRenderer()
        {
            Renderer = Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRenderer;
        }

        private void ResetReshowDelay()
        {
            _ReshowDelay = System.Windows.Forms.SystemInformation.DoubleClickTime;
        }

        private void ResetVisualTip(object component)
        {
            if (VisualTipMap.Contains(component))
                GetVisualTip(component).Reset();
        }

        public void SetVisualTip(object component, Skybound.VisualTips.VisualTip visualTip)
        {
            Skybound.VisualTips.VisualTip visualTip1 = VisualTipMap[component] as Skybound.VisualTips.VisualTip;
            if (visualTip1 != null)
                visualTip1.SetProvider(null);
            if ((visualTip != null) && (visualTip.Provider != null))
                visualTip.Provider.SetVisualTip(component, null);
            VisualTipMap[component] = visualTip;
            if (visualTip != null)
            {
                visualTip.SetProvider(this);
                Skybound.VisualTips.VisualTipProvider.UpdateTipTarget(visualTip, component);
            }
        }

        private bool ShouldSerializeAccessKey()
        {
            return _AccessKey != (System.Windows.Forms.Shortcut.Ins | System.Windows.Forms.Shortcut.CtrlB);
        }

        private bool ShouldSerializeInitialDelay()
        {
            return _InitialDelay != (System.Windows.Forms.SystemInformation.DoubleClickTime * 2);
        }

        private bool ShouldSerializeRenderer()
        {
            return _Renderer != Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRenderer;
        }

        private bool ShouldSerializeReshowDelay()
        {
            return _ReshowDelay != System.Windows.Forms.SystemInformation.DoubleClickTime;
        }

        private bool ShouldSerializeVisualTip(object component)
        {
            if (!VisualTipMap.Contains(component))
                return false;
            return GetVisualTip(component).ShouldSerialize();
        }

        public void ShowTip(System.Windows.Forms.Control control, Skybound.VisualTips.VisualTipDisplayOptions options, System.Drawing.Rectangle exclude)
        {
            if (control == null)
                throw new System.ArgumentNullException("control");
            Skybound.VisualTips.VisualTip visualTip = GetEffectiveVisualTip(control, null);
            if (visualTip != null)
                ShowTip(visualTip, exclude, control, options);
        }

        public void ShowTip(Skybound.VisualTips.VisualTip tip, System.Drawing.Rectangle exclude)
        {
            ShowTip(tip, exclude, null, (Skybound.VisualTips.VisualTipDisplayOptions)tip.DisplayPosition);
        }

        public void ShowTip(Skybound.VisualTips.VisualTip tip, System.Drawing.Rectangle exclude, System.Windows.Forms.Control sourceControl, Skybound.VisualTips.VisualTipDisplayOptions options)
        {
            if (tip == null)
                throw new System.ArgumentNullException("tip", "The tip parameter may not be null.");
            if ((sourceControl != null) && !sourceControl.IsDisposed)
            {
                if ((options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnKeyDown) == Skybound.VisualTips.VisualTipDisplayOptions.HideOnKeyDown)
                    sourceControl.KeyDown += new System.Windows.Forms.KeyEventHandler(ShowTipControl_KeyDown);
                if ((options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnKeyPress) == Skybound.VisualTips.VisualTipDisplayOptions.HideOnKeyPress)
                    sourceControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(ShowTipControl_KeyPress);
                if ((options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnLostFocus) == Skybound.VisualTips.VisualTipDisplayOptions.HideOnLostFocus)
                    sourceControl.LostFocus += new System.EventHandler(ShowTipControl_LostFocus);
                if ((options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnMouseDown) == Skybound.VisualTips.VisualTipDisplayOptions.HideOnMouseDown)
                    sourceControl.MouseDown += new System.Windows.Forms.MouseEventHandler(ShowTipControl_MouseDown);
                if ((options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnTextChanged) == Skybound.VisualTips.VisualTipDisplayOptions.HideOnTextChanged)
                    sourceControl.TextChanged += new System.EventHandler(ShowTipControl_TextChanged);
                sourceControl.HandleDestroyed += new System.EventHandler(ShowTipControl_HandleDestroyed);
                System.Windows.Forms.Form form = sourceControl.FindForm();
                if (form != null)
                    form.Deactivate += new System.EventHandler(ShowTipControl_Form_Deactivate);
            }
            ShowTipCore(sourceControl, null, tip, exclude, options);
        }

        public void ShowTip(System.Windows.Forms.Control control, Skybound.VisualTips.VisualTipDisplayOptions options)
        {
            if (control == null)
                throw new System.ArgumentNullException("control");
            ShowTip(control, options, control.Parent == null ? control.Bounds : control.Parent.RectangleToScreen(control.Bounds));
        }

        private void ShowTipControl_Form_Deactivate(object sender, System.EventArgs e)
        {
            if (CurrentControl != null)
                HideShowTip(CurrentControl);
        }

        private void ShowTipControl_HandleDestroyed(object sender, System.EventArgs e)
        {
            HideShowTip(sender as System.Windows.Forms.Control);
        }

        private void ShowTipControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            HideShowTip(sender as System.Windows.Forms.Control);
        }

        private void ShowTipControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            HideShowTip(sender as System.Windows.Forms.Control);
        }

        private void ShowTipControl_LostFocus(object sender, System.EventArgs e)
        {
            HideShowTip(sender as System.Windows.Forms.Control);
        }

        private void ShowTipControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HideShowTip(sender as System.Windows.Forms.Control);
        }

        private void ShowTipControl_TextChanged(object sender, System.EventArgs e)
        {
            HideShowTip(sender as System.Windows.Forms.Control);
        }

        private void ShowTipCore(System.Windows.Forms.Control control, object component, Skybound.VisualTips.VisualTip tip, System.Drawing.Rectangle toolArea, Skybound.VisualTips.VisualTipDisplayOptions options)
        {
            if ((control != null) && control.InvokeRequired)
            {
                object[] objArr = new object[] {
                                                 control, 
                                                 component, 
                                                 tip, 
                                                 toolArea, 
                                                 options };
                control.BeginInvoke(new Skybound.VisualTips.VisualTipProvider.ShowTipCoreMethod(ShowTipCore), objArr);
                return;
            }
            bool flag = (options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnMouseLeave) == Skybound.VisualTips.VisualTipDisplayOptions.HideOnMouseLeave;
            Skybound.VisualTips.VisualTipWindow visualTipWindow1 = flag ? Skybound.VisualTips.VisualTipProvider.TrackedTipWindow : CurrentTipWindow;
            if (visualTipWindow1 == null)
            {
                if (flag)
                {
                    Skybound.VisualTips.VisualTipProvider.TrackedTipWindow = new Skybound.VisualTips.VisualTipWindow();
                    visualTipWindow1 = new Skybound.VisualTips.VisualTipWindow();
                }
                else
                {
                    Skybound.VisualTips.VisualTipWindow visualTipWindow2 = new Skybound.VisualTips.VisualTipWindow();
                    CurrentTipWindow = new Skybound.VisualTips.VisualTipWindow();
                    visualTipWindow1 = visualTipWindow2;
                }
            }
            if (visualTipWindow1.DisplayedTip != tip)
            {
                visualTipWindow1.Undisplay();
                Skybound.VisualTips.VisualTipEventArgs visualTipEventArgs = new Skybound.VisualTips.VisualTipEventArgs(tip, component == null ? control : component);
                tip.SetProvider(this);
                Skybound.VisualTips.VisualTipProvider.UpdateTipTarget(tip, visualTipEventArgs.Instance);
                OnTipPopup(visualTipEventArgs);
                if (visualTipEventArgs.Cancel)
                {
                    Skybound.VisualTips.VisualTipProvider.PreventDisplayTip = tip;
                    return;
                }
                _CurrentTip = tip;
                CurrentControl = control;
                CurrentComponent = component;
                if (flag)
                    Skybound.VisualTips.VisualTipTracker.TrackingProvider = this;
                Skybound.VisualTips.VisualTipProvider.WindowStack.Add(visualTipWindow1, control);
                visualTipWindow1.Display(this, tip, toolArea, options);
                return;
            }
            visualTipWindow1.SetToolArea(toolArea, options);
        }

        internal void TrackKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (CurrentTip == null)
                return;
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                Skybound.VisualTips.VisualTipProvider.PreventDisplayTip = CurrentTip;
                Skybound.VisualTips.VisualTipProvider.HideTrackedTip();
                e.Handled = true;
                return;
            }
            if (((System.Windows.Forms.Shortcut)e.KeyCode) == ((System.Windows.Forms.Shortcut)CurrentTip.AccessKey))
            {
                Skybound.VisualTips.VisualTipEventArgs visualTipEventArgs = new Skybound.VisualTips.VisualTipEventArgs(CurrentTip, CurrentComponent != null ? CurrentComponent : CurrentControl);
                Skybound.VisualTips.VisualTipProvider.PreventDisplayTip = CurrentTip;
                Skybound.VisualTips.VisualTipProvider.HideTrackedTip();
                e.Handled = true;
                OnAccessKeyPressed(visualTipEventArgs);
            }
        }

        internal void TrackMouseDownUp()
        {
            if ((Skybound.VisualTips.VisualTipProvider.TrackedTipWindow.Options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnMouseDown) == Skybound.VisualTips.VisualTipDisplayOptions.Default)
                return;
            if (Skybound.VisualTips.VisualTipProvider.TrackedTip != null)
                Skybound.VisualTips.VisualTipProvider.PreventDisplayTip = Skybound.VisualTips.VisualTipProvider.TrackedTip;
            Skybound.VisualTips.VisualTipProvider.HideTrackedTip();
        }

        internal void TrackMouseLeave(System.EventArgs e)
        {
            if ((Skybound.VisualTips.VisualTipProvider.TrackedTipWindow.Options & Skybound.VisualTips.VisualTipDisplayOptions.HideOnMouseDown) == Skybound.VisualTips.VisualTipDisplayOptions.Default)
                return;
            System.Drawing.Rectangle rectangle = Skybound.VisualTips.VisualTipProvider.TrackedTipWindow.Bounds;
            if (rectangle.Contains(System.Windows.Forms.Cursor.Position))
            {
                Skybound.VisualTips.VisualTipProvider.PreventDisplayTip = CurrentTip;
            }
            else
            {
                Skybound.VisualTips.VisualTipProvider.PreventDisplayTip = null;
                Skybound.VisualTips.VisualTipTracker.SetTimeoutHandler(ReshowDelay, new System.EventHandler(OnMouseHover));
            }
            Skybound.VisualTips.VisualTipProvider.HideTrackedTip();
        }

        internal void TrackMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.Control control = Skybound.VisualTips.VisualTipTracker.LastMouseEventControl;
            if (control != null)
            {
                Skybound.VisualTips.VisualTip visualTip = GetEffectiveVisualTip(control, Skybound.VisualTips.VisualTipProvider.GetComponentAtPoint(control, new System.Drawing.Point(e.X, e.Y)));
                if ((Skybound.VisualTips.VisualTipProvider.PreventDisplayTip != null) && (visualTip != Skybound.VisualTips.VisualTipProvider.PreventDisplayTip))
                {
                    Skybound.VisualTips.VisualTipProvider.PreventDisplayTip = null;
                    return;
                }
                if (visualTip != CurrentTip)
                {
                    Skybound.VisualTips.VisualTipTracker.SetTimeoutHandler(ReshowDelay, new System.EventHandler(OnMouseHover));
                    Skybound.VisualTips.VisualTipProvider.HideTrackedTip();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            Skybound.VisualTips.VisualTipTracker.RemoveHandler(InitialDelay, new System.EventHandler(OnMouseHover));
            RemoveInstance();
            base.Dispose(disposing);
        }

        protected virtual void OnAccessKeyPressed(Skybound.VisualTips.VisualTipEventArgs e)
        {
            if (AccessKeyPressed != null)
                AccessKeyPressed(this, e);
        }

        protected virtual void OnDisplayModeChanged(System.EventArgs e)
        {
            if (((System.EventHandler)Events[Skybound.VisualTips.VisualTipProvider.DisplayModeChangedEvent]) != null)
                ((System.EventHandler)Events[Skybound.VisualTips.VisualTipProvider.DisplayModeChangedEvent])(this, e);
        }

        protected virtual void OnDisplayPositionChanged(System.EventArgs e)
        {
            if (((System.EventHandler)Events[Skybound.VisualTips.VisualTipProvider.DisplayPositionChangedEvent]) != null)
                ((System.EventHandler)Events[Skybound.VisualTips.VisualTipProvider.DisplayPositionChangedEvent])(this, e);
        }

        protected virtual void OnRendererChanged(System.EventArgs e)
        {
            if (((System.EventHandler)Events[Skybound.VisualTips.VisualTipProvider.RendererChangedEvent]) != null)
                ((System.EventHandler)Events[Skybound.VisualTips.VisualTipProvider.RendererChangedEvent])(this, e);
        }

        protected virtual void OnTipPopup(Skybound.VisualTips.VisualTipEventArgs e)
        {
            if (TipPopup != null)
                TipPopup(this, e);
        }

        private static object GetComponentAtPoint(System.Windows.Forms.Control control, System.Drawing.Point point)
        {
            Skybound.VisualTips.IVisualTipExtender ivisualTipExtender = Skybound.VisualTips.VisualTipProvider.GetExtender(control.GetType());
            object obj = ivisualTipExtender != null ? ivisualTipExtender.GetChildAtPoint(control, point.X, point.Y) : null;
            System.Windows.Forms.Control control1 = Skybound.VisualTips.VisualTipProvider.GetNestedChildAtPoint(control, control.PointToScreen(point));
            if (control1 != control)
                return control1;
            return obj;
        }

        internal static Skybound.VisualTips.IVisualTipExtender GetExtender(System.Type controlOrComponentType)
        {
            Skybound.VisualTips.IVisualTipExtender ivisualTipExtender2;

            Skybound.VisualTips.IVisualTipExtender ivisualTipExtender1 = Skybound.VisualTips.VisualTipProvider.Extenders[controlOrComponentType] as Skybound.VisualTips.IVisualTipExtender;
            if (ivisualTipExtender1 == null)
            {
                System.Collections.IDictionaryEnumerator idictionaryEnumerator = Skybound.VisualTips.VisualTipProvider.Extenders.GetEnumerator();
                try
                {
                    while (idictionaryEnumerator.MoveNext())
                    {
                        System.Collections.DictionaryEntry dictionaryEntry = (System.Collections.DictionaryEntry)idictionaryEnumerator.Current;
                        if (controlOrComponentType.IsSubclassOf(dictionaryEntry.Key as System.Type))
                        {
                            ivisualTipExtender2 = dictionaryEntry.Value as Skybound.VisualTips.IVisualTipExtender;
                            return ivisualTipExtender2;
                        }
                    }
                }
                finally
                {
                    System.IDisposable idisposable = idictionaryEnumerator as System.IDisposable;
                    if (idisposable != null)
                        idisposable.Dispose();
                }
            }
            return ivisualTipExtender1;
        }

        private static System.Windows.Forms.Control GetNestedChildAtPoint(System.Windows.Forms.Control control, System.Drawing.Point screenPoint)
        {
            System.Windows.Forms.Control control1 = control.GetChildAtPoint(control.PointToClient(screenPoint), System.Windows.Forms.GetChildAtPointSkip.Invisible);
            if (control1 != null)
                return Skybound.VisualTips.VisualTipProvider.GetNestedChildAtPoint(control1, screenPoint);
            return control;
        }

        public static void HideTrackedTip()
        {
            if (Skybound.VisualTips.VisualTipProvider.TrackedTipWindow != null)
                Skybound.VisualTips.VisualTipProvider.TrackedTipWindow.Undisplay();
        }

        public static void SetExtender(System.Type controlType, Skybound.VisualTips.IVisualTipExtender extender)
        {
            if (controlType == null)
                throw new System.ArgumentNullException("controlType");
            if (extender == null)
                throw new System.ArgumentNullException("extender");
            Skybound.VisualTips.VisualTipProvider.Extenders[controlType] = extender;
            System.Type[] typeArr1 = extender.GetChildTypes();
            if (typeArr1 != null)
            {
                System.Type[] typeArr2 = typeArr1;
                for (int i = 0; i < typeArr2.Length; i++)
                {
                    System.Type type = typeArr2[i];
                    Skybound.VisualTips.VisualTipProvider.Extenders[type] = extender;
                }
            }
        }

        [System.ComponentModel.Description]
        private static void SetExtender(System.Type controlType, object extender)
        {
            if (controlType == null)
                throw new System.ArgumentNullException("controlType");
            if (extender == null)
                throw new System.ArgumentNullException("extender");
            if (!Skybound.VisualTips.VisualTipExtenderAdapter.Validate(extender))
                throw new System.ArgumentException("The object did not provide the required methods.  Extenders must have 2 methods withthe following signatures:\r\n\r\nObject GetChildAtPoint(Control,Int32,Int32)\r\nObject GetParent(Object)", "extender");
            Skybound.VisualTips.VisualTipProvider.SetExtender(controlType, new Skybound.VisualTips.VisualTipExtenderAdapter(extender));
        }

        public static void ShowNotifyTip(System.Windows.Forms.Control control, string text, string title, Skybound.VisualTips.VisualTipNotifyIcon icon, Skybound.VisualTips.VisualTipDisplayOptions options)
        {
            if (control == null)
                throw new System.ArgumentNullException("control");
            Skybound.VisualTips.VisualTipProvider.ShowNotifyTip(control, text, title, icon, options, control.Parent == null ? control.Bounds : control.Parent.RectangleToScreen(control.Bounds));
        }

        public static void ShowNotifyTip(System.Windows.Forms.TextBox textBox, string text, string title, Skybound.VisualTips.VisualTipNotifyIcon icon, int charIndex)
        {
            if ((textBox == null) || textBox.IsDisposed)
                throw new System.ArgumentNullException("textBox", "textBox may not be null or disposed.");
            if ((charIndex < 0) || (charIndex > textBox.TextLength))
                throw new System.ArgumentNullException("charIndex", "charIndex may not be less than zero or greater than the length of the TextBox text.");
            if (charIndex == textBox.TextLength)
                charIndex = System.Math.Max(charIndex - 1, 0);
            System.IntPtr intPtr = Skybound.VisualTips.VisualTipProvider.SendMessage(textBox.Handle, 214, new System.IntPtr(System.Math.Max(charIndex, 0)), System.IntPtr.Zero);
            int i = intPtr.ToInt32();
            System.Drawing.Point point = textBox.PointToScreen(new System.Drawing.Point(i));
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(point, new System.Drawing.Size(1, textBox.Font.Height));
            Skybound.VisualTips.VisualTipProvider.ShowNotifyTip(textBox, text, title, icon, Skybound.VisualTips.VisualTipDisplayOptions.HideOnKeyPress | Skybound.VisualTips.VisualTipDisplayOptions.HideOnLostFocus, rectangle);
        }

        public static void ShowNotifyTip(System.Windows.Forms.Control control, string text, string title, Skybound.VisualTips.VisualTipNotifyIcon icon, Skybound.VisualTips.VisualTipDisplayOptions options, System.Drawing.Rectangle exclude)
        {
            if (control == null)
                throw new System.ArgumentNullException("control");
            if (control.IsDisposed)
                throw new System.ArgumentOutOfRangeException("control", control, "A tip may not be displayed for a control which has already been disposed.");
            if (Skybound.VisualTips.VisualTipProvider.NotifyProvider == null)
            {
                Skybound.VisualTips.VisualTipProvider.NotifyProvider = new Skybound.VisualTips.VisualTipProvider();
                Skybound.VisualTips.VisualTipProvider.NotifyProvider.Renderer = new Skybound.VisualTips.Rendering.VisualTipBalloonRenderer();
            }
            exclude.X = exclude.X - 12;
            Skybound.VisualTips.VisualTip visualTip = new Skybound.VisualTips.VisualTip(text, title);
            visualTip.TitleImage = Skybound.VisualTips.VisualTipProvider.NotifyImages.FromIcon(icon);
            Skybound.VisualTips.VisualTipProvider.NotifyProvider.ShowTip(visualTip, exclude, control, options);
        }

        public static void ShowNotifyTip(System.Windows.Forms.Control control, string text, string title, Skybound.VisualTips.VisualTipNotifyIcon icon, System.Drawing.Rectangle exclude)
        {
            Skybound.VisualTips.VisualTipProvider.ShowNotifyTip(control, text, title, icon, Skybound.VisualTips.VisualTipDisplayOptions.Default, exclude);
        }

        public static void ShowNotifyTip(System.Windows.Forms.Control control, string text, string title)
        {
            Skybound.VisualTips.VisualTipProvider.ShowNotifyTip(control, text, title, Skybound.VisualTips.VisualTipNotifyIcon.None, Skybound.VisualTips.VisualTipDisplayOptions.Default);
        }

        public static void ShowNotifyTip(System.Windows.Forms.Control control, string text, string title, Skybound.VisualTips.VisualTipNotifyIcon icon)
        {
            Skybound.VisualTips.VisualTipProvider.ShowNotifyTip(control, text, title, icon, Skybound.VisualTips.VisualTipDisplayOptions.Default);
        }

        private static void UpdateTipTarget(Skybound.VisualTips.VisualTip tip, object component)
        {
            if (component == null)
            {
                tip.SetTarget(null, null);
                return;
            }
            if ((component is System.Windows.Forms.Control))
            {
                tip.SetTarget(component as System.Windows.Forms.Control, null);
                return;
            }
            Skybound.VisualTips.IVisualTipExtender ivisualTipExtender = Skybound.VisualTips.VisualTipProvider.GetExtender(component.GetType());
            if (ivisualTipExtender != null)
            {
                tip.SetTarget(ivisualTipExtender.GetParent(component) as System.Windows.Forms.Control, component);
                return;
            }
            tip.SetTarget(null, component);
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr SendMessage(System.IntPtr hWnd, int wMsg, System.IntPtr wParam, System.IntPtr lParam);

    } // class VisualTipProvider

}

