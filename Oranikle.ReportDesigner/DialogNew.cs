/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for DialogNew.
	/// </summary>
	public class DialogNew : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listNewChoices;
		private Oranikle.Studio.Controls.StyledButton btnOK;
		private Oranikle.Studio.Controls.StyledButton btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private string _resultType;

		public DialogNew()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			listNewChoices.Clear();
			listNewChoices.BeginUpdate();

			ListViewItem lvi = new ListViewItem("Blank");
			listNewChoices.Items.Add(lvi);

			lvi = new ListViewItem("Data Base");
			listNewChoices.Items.Add(lvi);

			listNewChoices.LabelWrap = true;
			listNewChoices.Select();
			listNewChoices.EndUpdate();

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
            this.listNewChoices = new System.Windows.Forms.ListView();
            this.btnOK = new Oranikle.Studio.Controls.StyledButton();
            this.btnCancel = new Oranikle.Studio.Controls.StyledButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listNewChoices
            // 
            this.listNewChoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listNewChoices.HideSelection = false;
            this.listNewChoices.Location = new System.Drawing.Point(0, 0);
            this.listNewChoices.MultiSelect = false;
            this.listNewChoices.Name = "listNewChoices";
            this.listNewChoices.Size = new System.Drawing.Size(542, 403);
            this.listNewChoices.TabIndex = 0;
            this.listNewChoices.UseCompatibleStateImageBehavior = false;
            this.listNewChoices.ItemActivate += new System.EventHandler(this.listNewChoices_ItemActivate);
            this.listNewChoices.SelectedIndexChanged += new System.EventHandler(this.listNewChoices_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnOK.Enabled = false;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Arial", 9F);
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(374, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.OverriddenSize = null;
            this.btnOK.Size = new System.Drawing.Size(75, 21);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnCancel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnCancel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(462, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OverriddenSize = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 371);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 32);
            this.panel1.TabIndex = 3;
            // 
            // DialogNew
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(542, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listNewChoices);
            this.Name = "DialogNew";
            this.Text = "New";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void listNewChoices_ItemActivate(object sender, System.EventArgs e)
		{
			foreach (ListViewItem lvi in listNewChoices.SelectedItems)
			{
				_resultType = lvi.Text;
				break;		
			}
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();		
		}

		private void listNewChoices_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			foreach (ListViewItem lvi in listNewChoices.SelectedItems)
			{
				_resultType = lvi.Text;
				break;		
			}
			btnOK.Enabled = true;
		}

		public string ResultType
		{
			get {return _resultType;}
		}
	}
}
