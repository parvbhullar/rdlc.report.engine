/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Oranikle.Report.Engine;
using Oranikle.Report.Viewer;

namespace Oranikle.Report.Reader
{
	/// <summary>
	/// Summary description for ZoomTo.
	/// </summary>
	public class ZoomTo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.StyledComboBox cbMagnify;
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		private RdlViewer _Viewer;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ZoomTo(RdlViewer viewer)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_Viewer = viewer;
			// set the intial value for magnification
			if (_Viewer.ZoomMode == ZoomEnum.FitPage)
				cbMagnify.Text = "Fit Page";
			else if (_Viewer.ZoomMode == ZoomEnum.FitWidth)
				cbMagnify.Text = "Fit Width";
			else if (_Viewer.Zoom == 1)
				cbMagnify.Text = "Actual Size";
			else
			{
				string formatted = string.Format("{0:#0.##}", _Viewer.Zoom * 100);
				if (formatted[formatted.Length-1] == '.')
					formatted = formatted.Substring(0, formatted.Length-2);
				formatted = formatted + "%";
				cbMagnify.Text = formatted;
			}

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.cbMagnify = new Oranikle.Studio.Controls.StyledComboBox();
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Magnification";
            // 
            // cbMagnify
            // 
            this.cbMagnify.Items.AddRange(new object[] {
            "800%",
            "400%",
            "200%",
            "150%",
            "125%",
            "100%",
            "50%",
            "25%",
            "Fit Page",
            "Actual Size",
            "Fit Width"});
            this.cbMagnify.Location = new System.Drawing.Point(96, 16);
            this.cbMagnify.Name = "cbMagnify";
            this.cbMagnify.Size = new System.Drawing.Size(120, 21);
            this.cbMagnify.TabIndex = 2;
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Font = new System.Drawing.Font("Arial", 9F);
            this.bOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOK.Location = new System.Drawing.Point(24, 64);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCancel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCancel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.bCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(136, 64);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // ZoomTo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(248, 102);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cbMagnify);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoomTo";
            this.ShowInTaskbar = false;
            this.Text = "Zoom To";
            this.ResumeLayout(false);

		}
		#endregion

		private void bOK_Click(object sender, System.EventArgs e)
		{
			switch (cbMagnify.Text)
			{
				case "Fit Page":
					_Viewer.ZoomMode = ZoomEnum.FitPage;
					break;
				case "Actual Size":
					_Viewer.Zoom = 1;
					break;
				case "Fit Width":
					_Viewer.ZoomMode = ZoomEnum.FitWidth;
					break;
				default:
					string z = cbMagnify.Text.Replace("%", "");
					try
					{
						float zfactor = (float)( Convert.ToSingle(z) / 100.0);
						_Viewer.Zoom = zfactor;
					}
					catch 
					{
						MessageBox.Show(this, "Magnification level is an invalid number.");
						return;
					}
					break;
			}
			DialogResult = DialogResult.OK;
		}

		private void bCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
