using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Skybound.Drawing.Design
{

    internal class IconImageEditor : System.Drawing.Design.UITypeEditor, System.Windows.Forms.IMessageFilter
    {

        private bool FoundResourcePickerDialog;
        private System.Windows.Forms.Form ResourcePickerDialog;
        private System.Collections.ArrayList TemporaryFiles;
        private System.Windows.Forms.Design.IUIService UIService;

        public IconImageEditor()
        {
            TemporaryFiles = new System.Collections.ArrayList();
        }

        private void AttachEvents(System.Windows.Forms.Form form)
        {
            ResourcePickerDialog = form;
            FoundResourcePickerDialog = true;
            System.Windows.Forms.OpenFileDialog openFileDialog = GetOpenFileDialog(ResourcePickerDialog);
            if (openFileDialog != null)
            {
                openFileDialog.Filter = openFileDialog.Filter.Replace("*.gif", "*.ico;*.gif");
                openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(OpenFileDialog_FileOk);
            }
        }

        private string CreateTempPngFileName(string iconFileName)
        {
            string s = System.IO.Path.GetTempFileName();
            System.IO.File.Delete(s);
            System.IO.DirectoryInfo directoryInfo = System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.GetTempPath(), s));
            return System.IO.Path.Combine(directoryInfo.FullName, System.IO.Path.ChangeExtension(iconFileName, ".png"));
        }

        private void DetachEvents()
        {
            if (ResourcePickerDialog != null)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = GetOpenFileDialog(ResourcePickerDialog);
                if (openFileDialog != null)
                {
                    openFileDialog.Filter = openFileDialog.Filter.Replace("*.ico;*.gif", "*.gif");
                    openFileDialog.FileOk -= new System.ComponentModel.CancelEventHandler(OpenFileDialog_FileOk);
                }
                ResourcePickerDialog = null;
            }
        }

        private object EditValueOpenDialog(object value)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "Images (*.bmp;*.emf;*.gif;*.ico;*.jpg;*.png;*.wmf)|*.bmp;*.emf;*.gif;*.ico;*.jpg;*.png;*.wmf|All Files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            openFileDialog.ShowHelp = false;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.Title = "Open Image File";
            if (openFileDialog.ShowDialog(UIService.GetDialogOwnerWindow()) == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Image image = LoadImageOrIcon(openFileDialog.FileName);
                if (image != null)
                    return image;
            }
            return value;
        }

        private object EditValueResourcePicker(System.Drawing.Design.UITypeEditor defaultEditor, System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            object obj;

            System.Windows.Forms.Application.AddMessageFilter(this);
            FoundResourcePickerDialog = false;
            try
            {
                obj = defaultEditor.EditValue(context, provider, value);
            }
            finally
            {
                System.Windows.Forms.Application.RemoveMessageFilter(this);
                DetachEvents();
            }
            return obj;
        }

        private System.Windows.Forms.OpenFileDialog GetOpenFileDialog(System.Windows.Forms.Form form)
        {
            System.Reflection.PropertyInfo propertyInfo = form.GetType().GetProperty("OpenFileDialog", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (propertyInfo != null)
                return propertyInfo.GetValue(form, null) as System.Windows.Forms.OpenFileDialog;
            return null;
        }

        private System.Drawing.Image LoadFromStream(System.IO.Stream stream)
        {
            byte[] bArr = new byte[(uint)stream.Length];
            stream.Read(bArr, 0, (int)stream.Length);
            return System.Drawing.Image.FromStream(new System.IO.MemoryStream(bArr));
        }

        private System.Drawing.Image LoadImageOrIcon(string fileName)
        {
            System.Drawing.Image image;

            using (System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                if (System.IO.Path.GetExtension(fileName) == ".ico")
                {
                    Skybound.Drawing.Design.IconFile iconFile = new Skybound.Drawing.Design.IconFile(fileStream);
                    if (iconFile.GetFormats().Length == 1)
                    {
                        return iconFile.ToBitmap(iconFile.GetFormats()[0]);
                    }
                    Skybound.Drawing.Design.IconFormatDialog iconFormatDialog = new Skybound.Drawing.Design.IconFormatDialog();
                    iconFormatDialog.IconFile = iconFile;
                    iconFormatDialog.ShowDialog(UIService.GetDialogOwnerWindow());
                    if (iconFormatDialog.DialogResult != System.Windows.Forms.DialogResult.OK)
                        goto label_1;
                    return iconFile.ToBitmap(iconFormatDialog.SelectedFormat);
                }
                return LoadFromStream(fileStream);
            label_1:;
            }
            return null;
        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = sender as System.Windows.Forms.OpenFileDialog;
                string[] sArr = openFileDialog.FileNames;
                for (int i = 0; i < sArr.Length; i++)
                {
                    string s1 = sArr[i];
                    if (System.IO.Path.GetExtension(s1) == ".ico")
                    {
                        System.Drawing.Image image = LoadImageOrIcon(s1);
                        if (image != null)
                        {
                            string s2 = CreateTempPngFileName(System.IO.Path.GetFileName(s1));
                            try
                            {
                                image.Save(s2, System.Drawing.Imaging.ImageFormat.Png);
                                TemporaryFiles.Add(s2);
                                sArr[i] = s2;
                                continue;
                            }
                            catch (System.Exception e1)
                            {
                                UIService.ShowError(e1);
                                e.Cancel = true;
                                return;
                            }
                        }
                        e.Cancel = true;
                        return;
                    }
                }
                if (TemporaryFiles.Count > 0)
                {
                    System.Reflection.FieldInfo fieldInfo = typeof(System.Windows.Forms.FileDialog).GetField("fileNames", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    if (fieldInfo != null)
                        fieldInfo.SetValue(openFileDialog, sArr);
                }
            }
            catch (System.Exception e2)
            {
                UIService.ShowError(e2.ToString());
            }
        }

        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            try
            {
                if (!FoundResourcePickerDialog)
                {
                    System.Windows.Forms.Form form = System.Windows.Forms.Control.FromHandle(m.HWnd) as System.Windows.Forms.Form;
                    if ((form != null) && form.GetType().FullName.StartsWith("Microsoft.VisualStudio.Windows.Forms.ResourcePickerDialog"))
                        AttachEvents(form);
                }
            }
            catch (System.Exception e)
            {
                UIService.ShowError(e);
            }
            return false;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            object obj;

            UIService = (System.Windows.Forms.Design.IUIService)provider.GetService(typeof(System.Windows.Forms.Design.IUIService));
            try
            {
                System.Drawing.Design.UITypeEditor uitypeEditor = (System.Drawing.Design.UITypeEditor)System.ComponentModel.TypeDescriptor.GetEditor(typeof(System.Drawing.Image), typeof(System.Drawing.Design.UITypeEditor));
                if (uitypeEditor.GetType().FullName.StartsWith("Microsoft.VisualStudio"))
                {
                    return EditValueResourcePicker(uitypeEditor, context, provider, value);
                }
                return EditValueOpenDialog(value);
            }
            catch (System.Exception e)
            {
                UIService.ShowError(e);
            }
            return value;
        }

        ~IconImageEditor()
        {
            if (TemporaryFiles.Count > 0)
            {
                foreach (string s in TemporaryFiles)
                {
                    if (System.IO.File.Exists(s))
                        System.IO.File.Delete(s);
                    if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(s)))
                        System.IO.Directory.Delete(System.IO.Path.GetDirectoryName(s));
                }
                TemporaryFiles = null;
            }
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            if (e.Value is System.Drawing.Image)
            {
                System.Drawing.Rectangle rectangle = e.Bounds;
                rectangle.Width--;
                rectangle.Height--;
                e.Graphics.DrawRectangle(System.Drawing.SystemPens.WindowFrame, rectangle);
                e.Graphics.DrawImage((System.Drawing.Image)e.Value, e.Bounds);
            }
        }

    } // class IconImageEditor

}

