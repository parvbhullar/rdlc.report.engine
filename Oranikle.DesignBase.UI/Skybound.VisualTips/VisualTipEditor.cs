using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Skybound.VisualTips.Rendering;
using Skybound.Windows.Forms;

namespace Skybound.VisualTips
{

    internal class VisualTipEditor : System.Drawing.Design.UITypeEditor
    {

        private Skybound.Windows.Forms.BufferedGraphics Buffer;
        private Skybound.VisualTips.VisualTip CurrentTip;
        private Skybound.VisualTips.Rendering.VisualTipLayout Layout;
        private System.Windows.Forms.Design.IWindowsFormsEditorService WinForms;

        public VisualTipEditor()
        {
            Buffer = new Skybound.Windows.Forms.BufferedGraphics();
        }

        private void OnDropDownMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WinForms.CloseDropDown();
        }

        private void OnDropDownPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Buffer.SetTarget(e.Graphics, e.ClipRectangle);
            Buffer.Graphics.Clear(System.Drawing.Color.White);
            CurrentTip.Provider.Renderer.Draw(new System.Windows.Forms.PaintEventArgs(Buffer.Graphics, e.ClipRectangle), CurrentTip, Layout);
            Buffer.Render();
        }

        private void OnEmptyDropDownPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Buffer.SetTarget(e.Graphics, e.ClipRectangle);
            Buffer.Graphics.Clear(System.Drawing.SystemColors.Control);
            using (System.Drawing.Font font = new System.Drawing.Font("Tahoma", 8.0F))
            {
                Skybound.Windows.Forms.TextRenderer.DrawText(Buffer.Graphics, "No VisualTip will be displayed\r\nfor this component.", font, System.Drawing.SystemColors.ControlText, System.Drawing.SystemColors.Control, (sender as System.Windows.Forms.Control).ClientRectangle, Skybound.Windows.Forms.TextFormatFlags.HorizontalCenter);
            }
            Buffer.Render();
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            WinForms = provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService)) as System.Windows.Forms.Design.IWindowsFormsEditorService;
            Skybound.VisualTips.VisualTip visualTip = value as Skybound.VisualTips.VisualTip;
            System.Windows.Forms.Control control = new System.Windows.Forms.Control();
            if (visualTip.ShouldSerialize())
            {
                Layout = visualTip.Provider.Renderer.CreateLayout(visualTip);
                Layout.Offset(8, 8);
                CurrentTip = visualTip;
                control.Size = Layout.GetSize() + (new System.Drawing.Size(16, 16));
                control.Paint += new System.Windows.Forms.PaintEventHandler(OnDropDownPaint);
                control.MouseDown += new System.Windows.Forms.MouseEventHandler(OnDropDownMouseDown);
            }
            else
            {
                control.Size = new System.Drawing.Size(144, 28);
                control.Paint += new System.Windows.Forms.PaintEventHandler(OnEmptyDropDownPaint);
            }
            WinForms.DropDownControl(control);
            return value;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
        }

    } // class VisualTipEditor

}

