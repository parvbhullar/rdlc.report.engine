/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// DialogListOfStrings: puts up a dialog that lets a user enter a list of strings
	/// </summary>
	public class DialogListOfStrings : System.Windows.Forms.Form
	{
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbStrings;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DialogListOfStrings(List<string> list)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if (list == null || list.Count == 0)
				return;

			// Populate textbox with the list of strings
			string[] sa = new string[list.Count];
			int l=0;
			foreach (string v in list)
			{
				sa[l++] = v;
			}
			tbStrings.Lines = sa;

			return;
		}

		public List<string> ListOfStrings
		{
			get
			{
				if (this.tbStrings.Text.Length == 0)
					return null;
				List<string> l = new List<string>();
				foreach (string v in tbStrings.Lines)
				{
					if (v.Length > 0)
						l.Add(v);
				}
				return l;
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
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.tbStrings = new Oranikle.Studio.Controls.CustomTextControl();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.bOK.Location = new System.Drawing.Point(96, 192);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 21);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // tbStrings
            // 
            this.tbStrings.AddX = 0;
            this.tbStrings.AddY = 0;
            this.tbStrings.AllowSpace = false;
            this.tbStrings.BorderColor = System.Drawing.Color.LightGray;
            this.tbStrings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbStrings.ChangeVisibility = false;
            this.tbStrings.ChildControl = null;
            this.tbStrings.ConvertEnterToTab = true;
            this.tbStrings.ConvertEnterToTabForDialogs = false;
            this.tbStrings.Decimals = 0;
            this.tbStrings.DisplayList = new object[0];
            this.tbStrings.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbStrings.Location = new System.Drawing.Point(8, 40);
            this.tbStrings.Multiline = true;
            this.tbStrings.Name = "tbStrings";
            this.tbStrings.OnDropDownCloseFocus = true;
            this.tbStrings.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbStrings.SelectType = 0;
            this.tbStrings.Size = new System.Drawing.Size(256, 144);
            this.tbStrings.TabIndex = 9;
            this.tbStrings.UseValueForChildsVisibilty = false;
            this.tbStrings.Value = true;
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
            this.bCancel.Location = new System.Drawing.Point(192, 192);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 21);
            this.bCancel.TabIndex = 10;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Enter separate values on multiple lines below";
            // 
            // DialogListOfStrings
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(282, 224);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.tbStrings);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogListOfStrings";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void lnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs ea)
		{
			LinkLabel lnk = (LinkLabel) sender;
			lnk.Links[lnk.Links.IndexOf(ea.Link)].Visited = true;
			System.Diagnostics.Process.Start(lnk.Tag.ToString());
		}
	}

}
