using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Skybound.Windows.Forms;

namespace Skybound.ComponentModel
{

    internal class EnumTypeEditor : System.Drawing.Design.UITypeEditor
    {

        private class EnumValue
        {

            private string _Description;
            private System.Drawing.Image _Image;
            private string _Name;

            public string Description
            {
                get
                {
                    return _Description;
                }
            }

            public System.Drawing.Image Image
            {
                get
                {
                    return _Image;
                }
            }

            public string Name
            {
                get
                {
                    return _Name;
                }
            }

            public EnumValue(string name, string description, System.Drawing.Image image)
            {
                _Name = name;
                _Description = description;
                _Image = image;
            }

            public System.Drawing.Size Measure(System.Drawing.Graphics g, System.Drawing.Font font)
            {
                System.Drawing.Size size1 = Image == null ? System.Drawing.Size.Empty : Image.Size + (new System.Drawing.Size(8, 4));
                System.Drawing.Size size2 = System.Drawing.Size.Round(g.MeasureString(Name, font));
                System.Drawing.Size size3 = System.Drawing.Size.Round(g.MeasureString(Description, font, 2147483647, System.Drawing.StringFormat.GenericTypographic));
                return new System.Drawing.Size(size1.Width + System.Math.Max(size2.Width, size3.Width), System.Math.Max(size2.Height + size3.Height + 5, size1.Height));
            }

        } // class EnumValue

        private class EnumValueList : System.Windows.Forms.ListBox
        {

            private string _CurrentValue;

            public string CurrentValue
            {
                get
                {
                    return _CurrentValue;
                }
                set
                {
                    _CurrentValue = value;
                }
            }

            public EnumValueList()
            {
                BorderStyle = System.Windows.Forms.BorderStyle.None;
                Dock = System.Windows.Forms.DockStyle.Fill;
                DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            }

            protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
            {
                Skybound.ComponentModel.EnumTypeEditor.EnumValue enumValue = e.Index == -1 ? null : Items[e.Index] as Skybound.ComponentModel.EnumTypeEditor.EnumValue;
                e.DrawBackground();
                System.Drawing.Rectangle rectangle1 = e.Bounds;
                rectangle1.X++;
                rectangle1.Width -= 2;
                if (enumValue.Description.Length > 0)
                    rectangle1.Height >>= 1;
                if (enumValue.Image != null)
                {
                    e.Graphics.DrawImage(enumValue.Image, rectangle1.X + 3, rectangle1.Y + 2, enumValue.Image.Width, enumValue.Image.Height);
                    rectangle1.X += enumValue.Image.Width + 8;
                    rectangle1.Width -= enumValue.Image.Width + 8;
                }
                using (System.Drawing.Font font = new System.Drawing.Font(e.Font, CurrentValue == enumValue.Name ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular))
                {
                    Skybound.Windows.Forms.TextRenderer.DrawText(e.Graphics, enumValue.Name, font, e.ForeColor, System.Drawing.Color.Transparent, rectangle1, Skybound.Windows.Forms.TextFormatFlags.SingleLine | Skybound.Windows.Forms.TextFormatFlags.VerticalCenter);
                }
                if (enumValue.Description.Length > 0)
                {
                    rectangle1.Y += rectangle1.Height;
                    Skybound.Windows.Forms.TextRenderer.DrawText(e.Graphics, enumValue.Description, e.Font, System.Windows.Forms.ControlPaint.LightLight(e.ForeColor), System.Drawing.Color.Transparent, rectangle1, Skybound.Windows.Forms.TextFormatFlags.EndEllipsis | Skybound.Windows.Forms.TextFormatFlags.SingleLine | Skybound.Windows.Forms.TextFormatFlags.VerticalCenter);
                }
                System.Drawing.Rectangle rectangle2 = e.Bounds;
                System.Drawing.Rectangle rectangle3 = e.Bounds;
                System.Drawing.Rectangle rectangle4 = e.Bounds;
                System.Drawing.Rectangle rectangle5 = e.Bounds;
                e.Graphics.DrawLine(System.Drawing.SystemPens.Control, rectangle2.X, rectangle3.Bottom - 1, rectangle4.Right - 1, rectangle5.Bottom - 1);
            }

            protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
            {
                Skybound.ComponentModel.EnumTypeEditor.EnumValue enumValue = e.Index == -1 ? null : Items[e.Index] as Skybound.ComponentModel.EnumTypeEditor.EnumValue;
                System.Drawing.Size size = enumValue.Measure(e.Graphics, Font);
                e.ItemWidth = size.Width;
                e.ItemHeight = size.Height;
            }

        } // class EnumValueList

        private class ResizableDropDown : System.Windows.Forms.ContainerControl
        {

            private class ParentWindowSubclass : System.Windows.Forms.NativeWindow
            {

                private Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown Owner;

                private static System.Collections.Hashtable SubclassTable;

                private ParentWindowSubclass(System.Windows.Forms.Form parentForm)
                {
                    AssignHandle(parentForm.Handle);
                    parentForm.Disposed += new System.EventHandler(OnParentFormDisposed);
                }

                static ParentWindowSubclass()
                {
                    Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass.SubclassTable = new System.Collections.Hashtable();
                }

                private void OnParentFormDisposed(object sender, System.EventArgs e)
                {
                    if (Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass.SubclassTable.Contains(this))
                        Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass.SubclassTable.Remove(this);
                }

                protected override void WndProc(ref System.Windows.Forms.Message m)
                {
                    if ((Owner != null) && !Owner.IsDisposed && (m.Msg == 132) && Owner.HasSizeGrip)
                    {
                        System.IntPtr intPtr = m.LParam;
                        System.Drawing.Point point = Owner.PointToClient(new System.Drawing.Point(intPtr.ToInt32()));
                        System.Drawing.Rectangle rectangle = Owner.SizeGripBounds;
                        if (rectangle.Contains(point))
                        {
                            m.Result = Owner.SizeGripAlignment == System.Windows.Forms.LeftRightAlignment.Left ? new System.IntPtr(16) : new System.IntPtr(17);
                            return;
                        }
                    }
                    base.WndProc(ref m);
                }

                public static void EnsureSubclassed(Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown owner)
                {
                    System.Windows.Forms.Form form = owner.FindForm();
                    Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass parentWindowSubclass = Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass.SubclassTable[form] as Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass;
                    if (parentWindowSubclass == null)
                    {
                        parentWindowSubclass = new Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass(form);
                        Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass.SubclassTable[form] = new Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass(form);
                    }
                    parentWindowSubclass.Owner = owner;
                }

            } // class ParentWindowSubclass

            private const int HTBOTTOMLEFT = 16;
            private const int HTBOTTOMRIGHT = 17;
            private const int HTTRANSPARENT = -1;
            private const int WM_NCHITTEST = 132;

            private bool _HasSizeGrip;
            private System.Windows.Forms.LeftRightAlignment _SizeGripAlignment;

            public bool HasSizeGrip
            {
                get
                {
                    return _HasSizeGrip;
                }
                set
                {
                    _HasSizeGrip = value;
                    DockPadding.Bottom = value ? 16 : 0;
                }
            }

            public System.Windows.Forms.LeftRightAlignment SizeGripAlignment
            {
                get
                {
                    return _SizeGripAlignment;
                }
            }

            public System.Drawing.Rectangle SizeGripBounds
            {
                get
                {
                    if (SizeGripAlignment == System.Windows.Forms.LeftRightAlignment.Left)
                        return new System.Drawing.Rectangle(0, Height - 14, 14, 14);
                    return new System.Drawing.Rectangle(Width - 14, Height - 14, 14, 14);
                }
            }

            public ResizableDropDown()
            {
                BackColor = System.Drawing.SystemColors.Control;
                SetStyle(System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.ResizeRedraw | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            }

            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                if (!HasSizeGrip)
                    return;
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, Height - DockPadding.Bottom, Width, DockPadding.Bottom);
                using (System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(System.Drawing.Rectangle.Inflate(rectangle, 1, 1), System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.Control, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(linearGradientBrush, rectangle);
                }
                if (SizeGripAlignment == System.Windows.Forms.LeftRightAlignment.Left)
                {
                    for (int i1 = 0; i1 < 3; i1++)
                    {
                        for (int i2 = 0; i2 <= i1; i2++)
                        {
                            e.Graphics.FillRectangle(System.Drawing.SystemBrushes.ControlLightLight, 4 + (i2 * 4), Height - 11 + (i1 * 4), 2, 2);
                            e.Graphics.FillRectangle(System.Drawing.SystemBrushes.ControlDark, 3 + (i2 * 4), Height - 12 + (i1 * 4), 2, 2);
                        }
                    }
                    return;
                }
                for (int i3 = 0; i3 < 3; i3++)
                {
                    for (int i4 = 0; i4 <= i3; i4++)
                    {
                        e.Graphics.FillRectangle(System.Drawing.SystemBrushes.ControlLightLight, Width - 4 - (i4 * 4), Height - 11 + (i3 * 4), 2, 2);
                        e.Graphics.FillRectangle(System.Drawing.SystemBrushes.ControlDark, Width - 3 - (i4 * 4), Height - 12 + (i3 * 4), 2, 2);
                    }
                }
            }

            protected override void OnVisibleChanged(System.EventArgs e)
            {
                base.OnVisibleChanged(e);
                if (Visible && (FindForm() != null))
                {
                    Dock = System.Windows.Forms.DockStyle.Fill;
                    System.Drawing.Point point = PointToScreen(new System.Drawing.Point(0, 0));
                    System.Drawing.Rectangle rectangle = System.Windows.Forms.Screen.GetBounds(this);
                    _SizeGripAlignment = point.X < (rectangle.Width / 2) ? System.Windows.Forms.LeftRightAlignment.Right : System.Windows.Forms.LeftRightAlignment.Left;
                    Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown.ParentWindowSubclass.EnsureSubclassed(this);
                }
            }

            protected override void WndProc(ref System.Windows.Forms.Message m)
            {
                if ((m.Msg == 132) && HasSizeGrip)
                {
                    System.IntPtr intPtr = m.LParam;
                    System.Drawing.Point point = PointToClient(new System.Drawing.Point(intPtr.ToInt32()));
                    System.Drawing.Rectangle rectangle = SizeGripBounds;
                    if (rectangle.Contains(point))
                    {
                        m.Result = new System.IntPtr(-1);
                        return;
                    }
                }
                base.WndProc(ref m);
            }

        } // class ResizableDropDown

        private System.Windows.Forms.Design.IWindowsFormsEditorService EditorService;

        public EnumTypeEditor()
        {
        }

        private Skybound.ComponentModel.EnumTypeEditor.EnumValue GetEnumValue(System.ComponentModel.ITypeDescriptorContext context, System.Type enumType, string name)
        {
            System.Type type = null;
            if (context != null)
            {
                foreach (System.Attribute attribute1 in context.PropertyDescriptor.Attributes)
                {
                    if ((attribute1 is Skybound.ComponentModel.EnumAttributeProviderAttribute))
                    {
                        type = (attribute1 as Skybound.ComponentModel.EnumAttributeProviderAttribute).ProviderType;
                        break;
                    }
                }
            }
            System.Attribute[] attributeArr1 = null;
            if (type != null)
            {
                Skybound.ComponentModel.IEnumAttributeProvider ienumAttributeProvider = System.Activator.CreateInstance(type) as Skybound.ComponentModel.IEnumAttributeProvider;
                if (ienumAttributeProvider != null)
                    attributeArr1 = ienumAttributeProvider.GetAttributes(System.Enum.Parse(enumType, name, false));
            }
            if (attributeArr1 == null)
            {
                System.Reflection.FieldInfo fieldInfo = enumType.GetField(name, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                if (System.Attribute.IsDefined(fieldInfo, typeof(System.ComponentModel.DescriptionAttribute)))
                    attributeArr1 = System.Attribute.GetCustomAttributes(fieldInfo, typeof(System.ComponentModel.DescriptionAttribute));
                if (System.Attribute.IsDefined(fieldInfo, typeof(Skybound.ComponentModel.EnumValueImageProviderAttribute)))
                {
                    System.Attribute[] attributeArr2 = System.Attribute.GetCustomAttributes(fieldInfo, typeof(Skybound.ComponentModel.EnumValueImageProviderAttribute));
                    if (attributeArr1 != null)
                    {
                        System.Attribute[] attributeArr3 = new System.Attribute[(attributeArr1.Length + attributeArr2.Length)];
                        attributeArr1.CopyTo(attributeArr3, 0);
                        attributeArr2.CopyTo(attributeArr3, attributeArr1.Length);
                        attributeArr1 = attributeArr3;
                    }
                }
            }
            string s = System.String.Empty;
            System.Drawing.Image image = null;
            System.Attribute[] attributeArr4 = attributeArr1;
            for (int i = 0; i < attributeArr4.Length; i++)
            {
                System.Attribute attribute2 = attributeArr4[i];
                if ((attribute2 is System.ComponentModel.DescriptionAttribute))
                {
                    s = (attribute2 as System.ComponentModel.DescriptionAttribute).Description;
                }
                else
                {
                    if ((attribute2 is Skybound.ComponentModel.EnumValueImageProviderAttribute))
                        image = (attribute2 as Skybound.ComponentModel.EnumValueImageProviderAttribute).GetImage();
                }
            }
            return new Skybound.ComponentModel.EnumTypeEditor.EnumValue(name, s, image);
        }

        private void list_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                EditorService.CloseDropDown();
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            object obj;

            EditorService = (System.Windows.Forms.Design.IWindowsFormsEditorService)provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
            if (EditorService != null)
            {
                System.Type type = value.GetType();
                string[] sArr1 = System.Enum.GetNames(type);
                using (Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown resizableDropDown = new Skybound.ComponentModel.EnumTypeEditor.ResizableDropDown())
                using (Skybound.ComponentModel.EnumTypeEditor.EnumValueList enumValueList = new Skybound.ComponentModel.EnumTypeEditor.EnumValueList())
                {
                    enumValueList.CurrentValue = value.ToString();
                    resizableDropDown.Controls.Add(enumValueList);
                    int i1 = 0, i2 = 0;
                    using (System.Drawing.Graphics graphics = resizableDropDown.CreateGraphics())
                    {
                        string[] sArr2 = sArr1;
                        for (int i3 = 0; i3 < sArr2.Length; i3++)
                        {
                            string s = sArr2[i3];
                            Skybound.ComponentModel.EnumTypeEditor.EnumValue enumValue = GetEnumValue(context, type, s);
                            enumValueList.Items.Add(enumValue);
                            if (s == value.ToString())
                                enumValueList.SelectedIndex = enumValueList.Items.Count - 1;
                            System.Drawing.Size size = enumValue.Measure(graphics, resizableDropDown.Font);
                            i1 = System.Math.Max(i1, size.Width + 6 + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth);
                            i2 += size.Height;
                        }
                    }
                    System.Drawing.Rectangle rectangle1 = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                    if (i1 < (rectangle1.Width / 4))
                    {
                        resizableDropDown.Width = i1;
                    }
                    else
                    {
                        resizableDropDown.Width = rectangle1.Width / 4;
                        resizableDropDown.HasSizeGrip = true;
                        i2 += 16;
                    }
                    resizableDropDown.Height = System.Math.Min(rectangle1.Height / 4, i2);
                    System.Drawing.Rectangle rectangle2 = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                    resizableDropDown.HasSizeGrip |= i2 > (rectangle2.Height / 4);
                    enumValueList.MouseUp += new System.Windows.Forms.MouseEventHandler(list_MouseUp);
                    EditorService.DropDownControl(resizableDropDown);
                    return enumValueList.SelectedItem == null ? value : System.Enum.Parse(type, (enumValueList.SelectedItem as Skybound.ComponentModel.EnumTypeEditor.EnumValue).Name, false);
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
        }

    } // class EnumTypeEditor

}

