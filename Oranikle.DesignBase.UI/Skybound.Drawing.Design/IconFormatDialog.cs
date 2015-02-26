using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skybound.Drawing.Design
{

    internal class IconFormatDialog : System.Windows.Forms.Form
    {

        private Skybound.Drawing.Design.IconFile _IconFile;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFormats;
        private System.Windows.Forms.PictureBox picIcon;

        public Skybound.Drawing.Design.IconFile IconFile
        {
            get
            {
                return _IconFile;
            }
            set
            {
                _IconFile = value;
                lstFormats.Items.Clear();
                if (_IconFile != null)
                {
                    Skybound.Drawing.Design.IconFormat[] iconFormatArr1 = _IconFile.GetFormats();
                    Skybound.Drawing.Design.IconFormat[] iconFormatArr2 = iconFormatArr1;
                    for (int i = 0; i < iconFormatArr2.Length; i++)
                    {
                        Skybound.Drawing.Design.IconFormat iconFormat = iconFormatArr2[i];
                        lstFormats.Items.Add(iconFormat);
                    }
                    lstFormats.SelectedIndex = 0;
                }
            }
        }

        public Skybound.Drawing.Design.IconFormat SelectedFormat
        {
            get
            {
                if (lstFormats.SelectedItem != null)
                    return (Skybound.Drawing.Design.IconFormat)lstFormats.SelectedItem;
                return Skybound.Drawing.Design.IconFormat.Empty;
            }
        }

        public IconFormatDialog()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void InitializeComponent()
        {
            lstFormats = new System.Windows.Forms.ListBox();
            label1 = new System.Windows.Forms.Label();
            picIcon = new System.Windows.Forms.PictureBox();
            cmdOK = new System.Windows.Forms.Button();
            cmdCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            lstFormats.IntegralHeight = false;
            lstFormats.Location = new System.Drawing.Point(7, 40);
            lstFormats.Name = "lstFormats";
            lstFormats.Size = new System.Drawing.Size(124, 256);
            lstFormats.TabIndex = 0;
            lstFormats.DoubleClick += new System.EventHandler(lstFormats_DoubleClick);
            lstFormats.SelectedIndexChanged += new System.EventHandler(lstFormats_SelectedIndexChanged);
            label1.Location = new System.Drawing.Point(6, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(380, 32);
            label1.TabIndex = 1;
            label1.Text = "This icon file contains more than one size or color depth.  Please choose the size and color depth to load from the list below.";
            picIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            picIcon.Location = new System.Drawing.Point(135, 40);
            picIcon.Name = "picIcon";
            picIcon.Size = new System.Drawing.Size(256, 256);
            picIcon.TabIndex = 2;
            picIcon.TabStop = false;
            picIcon.Paint += new System.Windows.Forms.PaintEventHandler(picIcon_Paint);
            cmdOK.Location = new System.Drawing.Point(317, 304);
            cmdOK.Name = "cmdOK";
            cmdOK.Size = new System.Drawing.Size(74, 22);
            cmdOK.TabIndex = 3;
            cmdOK.Text = "OK";
            cmdOK.Click += new System.EventHandler(cmdOK_Click);
            cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cmdCancel.Location = new System.Drawing.Point(317, 330);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new System.Drawing.Size(74, 22);
            cmdCancel.TabIndex = 4;
            cmdCancel.Text = "Cancel";
            AcceptButton = cmdOK;
            AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            CancelButton = cmdCancel;
            ClientSize = new System.Drawing.Size(400, 361);
            Controls.Add(cmdCancel);
            Controls.Add(cmdOK);
            Controls.Add(picIcon);
            Controls.Add(label1);
            Controls.Add(lstFormats);
            Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IconFormatDialog";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Choose Icon";
            ResumeLayout(false);
        }

        private void lstFormats_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstFormats.SelectedIndex >= 0)
                cmdOK_Click(cmdOK, System.EventArgs.Empty);
        }

        private void lstFormats_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            picIcon.Invalidate();
        }

        private void picIcon_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            using (System.Drawing.Brush brush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.LargeCheckerBoard, System.Drawing.Color.LightGray, System.Drawing.Color.White))
            {
                e.Graphics.FillRectangle(brush, picIcon.ClientRectangle);
            }
            if (lstFormats.SelectedItem != null)
                e.Graphics.DrawImage(IconFile.ToBitmap((Skybound.Drawing.Design.IconFormat)lstFormats.SelectedItem), 0, 0);
        }

    } // class IconFormatDialog

}

