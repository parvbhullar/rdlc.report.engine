/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Oranikle.Report.Viewer
{
	/// <summary>
	/// DialogMessage is used in place of a message box when the text can be large
	/// </summary>
	public class DialogMessages : System.Windows.Forms.Form
	{
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.CustomTextControl tbMessages;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DialogMessages(IList msgs)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			string[] lines = new string[msgs.Count];
			int l=0;
			foreach (string msg in msgs)
			{
				lines[l++] = msg;
			}
			tbMessages.Lines = lines;
			return;
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
            this.tbMessages = new Oranikle.Studio.Controls.CustomTextControl();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Font = new System.Drawing.Font("Arial", 9F);
            this.bOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOK.Location = new System.Drawing.Point(389, 200);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // tbMessages
            // 
            this.tbMessages.AddX = 0;
            this.tbMessages.AddY = 0;
            this.tbMessages.AllowSpace = false;
            this.tbMessages.BackColor = System.Drawing.Color.White;
            this.tbMessages.BorderColor = System.Drawing.Color.LightGray;
            this.tbMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMessages.ChangeVisibility = false;
            this.tbMessages.ChildControl = null;
            this.tbMessages.ConvertEnterToTab = true;
            this.tbMessages.ConvertEnterToTabForDialogs = false;
            this.tbMessages.Decimals = 0;
            this.tbMessages.DisplayList = new object[0];
            this.tbMessages.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbMessages.Location = new System.Drawing.Point(16, 16);
            this.tbMessages.Multiline = true;
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.OnDropDownCloseFocus = true;
            this.tbMessages.ReadOnly = true;
            this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMessages.SelectType = 0;
            this.tbMessages.Size = new System.Drawing.Size(448, 176);
            this.tbMessages.TabIndex = 9;
            this.tbMessages.UseValueForChildsVisibilty = false;
            this.tbMessages.Value = true;
            // 
            // DialogMessages
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bOK;
            this.ClientSize = new System.Drawing.Size(482, 230);
            this.Controls.Add(this.tbMessages);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogMessages";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Warnings";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

	}
}
