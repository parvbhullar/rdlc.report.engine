using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using Oranikle.Report.Engine;


namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for DialogDataSourceRef.
	/// </summary>
	public class DialogDataSourceRef : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbPassword;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.CustomTextControl tbFilename;
		private Oranikle.Studio.Controls.StyledButton bGetFilename;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.StyledComboBox cbDataProvider;
		private System.Windows.Forms.Label label4;
		private Oranikle.Studio.Controls.CustomTextControl tbConnection;
		private Oranikle.Studio.Controls.StyledCheckBox ckbIntSecurity;
		private System.Windows.Forms.Label label5;
		private Oranikle.Studio.Controls.CustomTextControl tbPrompt;
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		private Oranikle.Studio.Controls.CustomTextControl tbPassword2;
		private System.Windows.Forms.Label label6;
		private Oranikle.Studio.Controls.StyledButton bTestConnection;
		private Oranikle.Studio.Controls.StyledComboBox cbOdbcNames;
		private System.Windows.Forms.Label lODBC;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DialogDataSourceRef()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			string[] items = RdlEngineConfig.GetProviders();

			cbDataProvider.Items.AddRange(items);

			this.cbDataProvider.SelectedIndex = 0;
			this.bOK.Enabled = false;		
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
            this.tbPassword = new Oranikle.Studio.Controls.CustomTextControl();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFilename = new Oranikle.Studio.Controls.CustomTextControl();
            this.bGetFilename = new Oranikle.Studio.Controls.StyledButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDataProvider = new Oranikle.Studio.Controls.StyledComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbConnection = new Oranikle.Studio.Controls.CustomTextControl();
            this.ckbIntSecurity = new Oranikle.Studio.Controls.StyledCheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPrompt = new Oranikle.Studio.Controls.CustomTextControl();
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.tbPassword2 = new Oranikle.Studio.Controls.CustomTextControl();
            this.label6 = new System.Windows.Forms.Label();
            this.bTestConnection = new Oranikle.Studio.Controls.StyledButton();
            this.cbOdbcNames = new Oranikle.Studio.Controls.StyledComboBox();
            this.lODBC = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter (twice) a pass phrase used to encrypt the data source reference file.   You" +
                " should use the same password you\'ve used to create previous files, as only one " +
                "can be used.";
            // 
            // tbPassword
            // 
            this.tbPassword.AddX = 0;
            this.tbPassword.AddY = 0;
            this.tbPassword.AllowSpace = false;
            this.tbPassword.BorderColor = System.Drawing.Color.LightGray;
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPassword.ChangeVisibility = false;
            this.tbPassword.ChildControl = null;
            this.tbPassword.ConvertEnterToTab = true;
            this.tbPassword.ConvertEnterToTabForDialogs = false;
            this.tbPassword.Decimals = 0;
            this.tbPassword.DisplayList = new object[0];
            this.tbPassword.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPassword.Location = new System.Drawing.Point(16, 56);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.OnDropDownCloseFocus = true;
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.SelectType = 0;
            this.tbPassword.Size = new System.Drawing.Size(184, 20);
            this.tbPassword.TabIndex = 0;
            this.tbPassword.UseValueForChildsVisibilty = false;
            this.tbPassword.Value = true;
            this.tbPassword.TextChanged += new System.EventHandler(this.validate_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(408, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter the file name that will hold the encrypted data source information";
            // 
            // tbFilename
            // 
            this.tbFilename.AddX = 0;
            this.tbFilename.AddY = 0;
            this.tbFilename.AllowSpace = false;
            this.tbFilename.BorderColor = System.Drawing.Color.LightGray;
            this.tbFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFilename.ChangeVisibility = false;
            this.tbFilename.ChildControl = null;
            this.tbFilename.ConvertEnterToTab = true;
            this.tbFilename.ConvertEnterToTabForDialogs = false;
            this.tbFilename.Decimals = 0;
            this.tbFilename.DisplayList = new object[0];
            this.tbFilename.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbFilename.Location = new System.Drawing.Point(16, 112);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.OnDropDownCloseFocus = true;
            this.tbFilename.SelectType = 0;
            this.tbFilename.Size = new System.Drawing.Size(392, 20);
            this.tbFilename.TabIndex = 2;
            this.tbFilename.UseValueForChildsVisibilty = false;
            this.tbFilename.Value = true;
            this.tbFilename.TextChanged += new System.EventHandler(this.validate_TextChanged);
            // 
            // bGetFilename
            // 
            this.bGetFilename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bGetFilename.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bGetFilename.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bGetFilename.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bGetFilename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGetFilename.Font = new System.Drawing.Font("Arial", 9F);
            this.bGetFilename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bGetFilename.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bGetFilename.Location = new System.Drawing.Point(424, 112);
            this.bGetFilename.Name = "bGetFilename";
            this.bGetFilename.OverriddenSize = null;
            this.bGetFilename.Size = new System.Drawing.Size(24, 21);
            this.bGetFilename.TabIndex = 3;
            this.bGetFilename.Text = "...";
            this.bGetFilename.UseVisualStyleBackColor = true;
            this.bGetFilename.Click += new System.EventHandler(this.bGetFilename_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data provider";
            // 
            // cbDataProvider
            // 
            this.cbDataProvider.AutoAdjustItemHeight = false;
            this.cbDataProvider.BorderColor = System.Drawing.Color.LightGray;
            this.cbDataProvider.ConvertEnterToTabForDialogs = false;
            this.cbDataProvider.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDataProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDataProvider.Location = new System.Drawing.Point(80, 152);
            this.cbDataProvider.Name = "cbDataProvider";
            this.cbDataProvider.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDataProvider.SeparatorMargin = 1;
            this.cbDataProvider.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDataProvider.SeparatorWidth = 1;
            this.cbDataProvider.Size = new System.Drawing.Size(104, 21);
            this.cbDataProvider.TabIndex = 4;
            this.cbDataProvider.SelectedIndexChanged += new System.EventHandler(this.cbDataProvider_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Connection string";
            // 
            // tbConnection
            // 
            this.tbConnection.AddX = 0;
            this.tbConnection.AddY = 0;
            this.tbConnection.AllowSpace = false;
            this.tbConnection.BorderColor = System.Drawing.Color.LightGray;
            this.tbConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbConnection.ChangeVisibility = false;
            this.tbConnection.ChildControl = null;
            this.tbConnection.ConvertEnterToTab = true;
            this.tbConnection.ConvertEnterToTabForDialogs = false;
            this.tbConnection.Decimals = 0;
            this.tbConnection.DisplayList = new object[0];
            this.tbConnection.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbConnection.Location = new System.Drawing.Point(16, 216);
            this.tbConnection.Multiline = true;
            this.tbConnection.Name = "tbConnection";
            this.tbConnection.OnDropDownCloseFocus = true;
            this.tbConnection.SelectType = 0;
            this.tbConnection.Size = new System.Drawing.Size(424, 40);
            this.tbConnection.TabIndex = 7;
            this.tbConnection.UseValueForChildsVisibilty = false;
            this.tbConnection.Value = true;
            this.tbConnection.TextChanged += new System.EventHandler(this.validate_TextChanged);
            // 
            // ckbIntSecurity
            // 
            this.ckbIntSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckbIntSecurity.ForeColor = System.Drawing.Color.Black;
            this.ckbIntSecurity.Location = new System.Drawing.Point(296, 184);
            this.ckbIntSecurity.Name = "ckbIntSecurity";
            this.ckbIntSecurity.Size = new System.Drawing.Size(144, 24);
            this.ckbIntSecurity.TabIndex = 6;
            this.ckbIntSecurity.Text = "Use integrated security";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(432, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "(Optional) Enter the prompt displayed when asking for database credentials ";
            // 
            // tbPrompt
            // 
            this.tbPrompt.AddX = 0;
            this.tbPrompt.AddY = 0;
            this.tbPrompt.AllowSpace = false;
            this.tbPrompt.BorderColor = System.Drawing.Color.LightGray;
            this.tbPrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPrompt.ChangeVisibility = false;
            this.tbPrompt.ChildControl = null;
            this.tbPrompt.ConvertEnterToTab = true;
            this.tbPrompt.ConvertEnterToTabForDialogs = false;
            this.tbPrompt.Decimals = 0;
            this.tbPrompt.DisplayList = new object[0];
            this.tbPrompt.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPrompt.Location = new System.Drawing.Point(16, 296);
            this.tbPrompt.Name = "tbPrompt";
            this.tbPrompt.OnDropDownCloseFocus = true;
            this.tbPrompt.SelectType = 0;
            this.tbPrompt.Size = new System.Drawing.Size(424, 20);
            this.tbPrompt.TabIndex = 8;
            this.tbPrompt.UseValueForChildsVisibilty = false;
            this.tbPrompt.Value = true;
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Font = new System.Drawing.Font("Arial", 9F);
            this.bOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOK.Location = new System.Drawing.Point(272, 344);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 10;
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
            this.bCancel.Location = new System.Drawing.Point(368, 344);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // tbPassword2
            // 
            this.tbPassword2.AddX = 0;
            this.tbPassword2.AddY = 0;
            this.tbPassword2.AllowSpace = false;
            this.tbPassword2.BorderColor = System.Drawing.Color.LightGray;
            this.tbPassword2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPassword2.ChangeVisibility = false;
            this.tbPassword2.ChildControl = null;
            this.tbPassword2.ConvertEnterToTab = true;
            this.tbPassword2.ConvertEnterToTabForDialogs = false;
            this.tbPassword2.Decimals = 0;
            this.tbPassword2.DisplayList = new object[0];
            this.tbPassword2.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPassword2.Location = new System.Drawing.Point(256, 56);
            this.tbPassword2.Name = "tbPassword2";
            this.tbPassword2.OnDropDownCloseFocus = true;
            this.tbPassword2.PasswordChar = '*';
            this.tbPassword2.SelectType = 0;
            this.tbPassword2.Size = new System.Drawing.Size(184, 20);
            this.tbPassword2.TabIndex = 1;
            this.tbPassword2.UseValueForChildsVisibilty = false;
            this.tbPassword2.Value = true;
            this.tbPassword2.TextChanged += new System.EventHandler(this.validate_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(208, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 2;
            this.label6.Text = "Repeat";
            // 
            // bTestConnection
            // 
            this.bTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bTestConnection.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bTestConnection.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bTestConnection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTestConnection.Font = new System.Drawing.Font("Arial", 9F);
            this.bTestConnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bTestConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTestConnection.Location = new System.Drawing.Point(16, 344);
            this.bTestConnection.Name = "bTestConnection";
            this.bTestConnection.OverriddenSize = null;
            this.bTestConnection.Size = new System.Drawing.Size(96, 21);
            this.bTestConnection.TabIndex = 9;
            this.bTestConnection.Text = "Test Connection";
            this.bTestConnection.UseVisualStyleBackColor = true;
            this.bTestConnection.Click += new System.EventHandler(this.bTestConnection_Click);
            // 
            // cbOdbcNames
            // 
            this.cbOdbcNames.AutoAdjustItemHeight = false;
            this.cbOdbcNames.BorderColor = System.Drawing.Color.LightGray;
            this.cbOdbcNames.ConvertEnterToTabForDialogs = false;
            this.cbOdbcNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbOdbcNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOdbcNames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOdbcNames.Location = new System.Drawing.Point(296, 152);
            this.cbOdbcNames.Name = "cbOdbcNames";
            this.cbOdbcNames.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbOdbcNames.SeparatorMargin = 1;
            this.cbOdbcNames.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbOdbcNames.SeparatorWidth = 1;
            this.cbOdbcNames.Size = new System.Drawing.Size(152, 21);
            this.cbOdbcNames.Sorted = true;
            this.cbOdbcNames.TabIndex = 5;
            this.cbOdbcNames.SelectedIndexChanged += new System.EventHandler(this.cbOdbcNames_SelectedIndexChanged);
            // 
            // lODBC
            // 
            this.lODBC.Location = new System.Drawing.Point(184, 152);
            this.lODBC.Name = "lODBC";
            this.lODBC.Size = new System.Drawing.Size(112, 23);
            this.lODBC.TabIndex = 17;
            this.lODBC.Text = "ODBC Data Sources";
            // 
            // DialogDataSourceRef
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(456, 374);
            this.Controls.Add(this.cbOdbcNames);
            this.Controls.Add(this.lODBC);
            this.Controls.Add(this.bTestConnection);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbPassword2);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.tbPrompt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckbIntSecurity);
            this.Controls.Add(this.tbConnection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbDataProvider);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bGetFilename);
            this.Controls.Add(this.tbFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogDataSourceRef";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Data Source Reference File";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void bGetFilename_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Data source reference files (*.dsr)|*.dsr" +
                         "|All files (*.*)|*.*";
			sfd.FilterIndex = 1;
			if (tbFilename.Text.Length > 0)
				sfd.FileName = tbFilename.Text;
			else
				sfd.FileName = "*.dsr";

			sfd.Title = "Specify Data Source Reference File Name";
			sfd.OverwritePrompt = true;
			sfd.DefaultExt = "dsr";
			sfd.AddExtension = true;
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                    tbFilename.Text = sfd.FileName;
            }
            finally
            {
                sfd.Dispose();
            }
		}

		private void validate_TextChanged(object sender, System.EventArgs e)
		{
			if (this.tbFilename.Text.Length > 0 &&
				this.tbPassword.Text.Length > 0 &&
				this.tbPassword.Text == this.tbPassword2.Text &&
				this.tbConnection.Text.Length > 0)
				bOK.Enabled = true;
			else
				bOK.Enabled = false;
			return;
		}

		private void bOK_Click(object sender, System.EventArgs e)
		{
			// Build the ConnectionProperties XML
			StringBuilder sb = new StringBuilder();
			sb.Append("<ConnectionProperties>");
			sb.AppendFormat("<DataProvider>{0}</DataProvider>", 
				this.cbDataProvider.Text);
			sb.AppendFormat("<ConnectString>{0}</ConnectString>", 
				this.tbConnection.Text.Replace("<", "&lt;"));
			sb.AppendFormat("<IntegratedSecurity>{0}</IntegratedSecurity>", 
				this.ckbIntSecurity.Checked? "true": "false");
			if (this.tbPrompt.Text.Length > 0)
				sb.AppendFormat("<Prompt>{0}</Prompt>",
					this.tbPrompt.Text.Replace("<", "&lt;"));
			sb.Append("</ConnectionProperties>");
			try
			{
				DataSourceReference.Create(tbFilename.Text, sb.ToString(), tbPassword.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Unable to create data source reference file");
				return;
			}

			DialogResult = DialogResult.OK;
		}

		private void bTestConnection_Click(object sender, System.EventArgs e)
		{
			if (DesignerUtility.TestConnection(this.cbDataProvider.Text, tbConnection.Text))
				MessageBox.Show("Connection succeeded!", "Test Connection");

		}

		private void cbDataProvider_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbDataProvider.Text == "ODBC")
			{
				lODBC.Visible = cbOdbcNames.Visible = true;
				DesignerUtility.FillOdbcNames(cbOdbcNames);
			}
			else
			{
				lODBC.Visible = cbOdbcNames.Visible = false;
			}

		}

		private void cbOdbcNames_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			tbConnection.Text = "dsn=" + cbOdbcNames.Text + ";";
		}
	}
}
