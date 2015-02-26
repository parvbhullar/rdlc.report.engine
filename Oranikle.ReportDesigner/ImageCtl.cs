/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for ReportCtl.
	/// </summary>
	internal class ImageCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		bool fSource, fValue, fSizing, fMIMEType;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbExternal;
		private System.Windows.Forms.RadioButton rbDatabase;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.StyledComboBox cbSizing;
		private Oranikle.Studio.Controls.StyledComboBox cbValueEmbedded;
		private Oranikle.Studio.Controls.StyledComboBox cbMIMEType;
		private Oranikle.Studio.Controls.StyledComboBox cbValueDatabase;
		private Oranikle.Studio.Controls.CustomTextControl tbValueExternal;
		private Oranikle.Studio.Controls.StyledButton bExternal;
		private System.Windows.Forms.RadioButton rbEmbedded;
		private Oranikle.Studio.Controls.StyledButton bEmbedded;
		private Oranikle.Studio.Controls.StyledButton bDatabaseExpr;
		private Oranikle.Studio.Controls.StyledButton bMimeExpr;
		private Oranikle.Studio.Controls.StyledButton bEmbeddedExpr;
		private Oranikle.Studio.Controls.StyledButton bExternalExpr;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal ImageCtl(DesignXmlDraw dxDraw, List<XmlNode> ris)
		{
			_ReportItems = ris;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			XmlNode iNode =  _ReportItems[0];

			// Populate the EmbeddedImage names
			cbValueEmbedded.Items.AddRange(_Draw.ReportNames.EmbeddedImageNames);
			string[] flds = _Draw.GetReportItemDataRegionFields(iNode, true);
			if (flds != null)
				this.cbValueDatabase.Items.AddRange(flds);

			string source = _Draw.GetElementValue(iNode, "Source", "Embedded");
			string val =  _Draw.GetElementValue(iNode, "Value", "");
			switch (source)
			{
				case "Embedded":
					this.rbEmbedded.Checked = true;
					this.cbValueEmbedded.Text = val;
					break;
				case "Database":
					this.rbDatabase.Checked = true;
					this.cbValueDatabase.Text = val;
					this.cbMIMEType.Text = _Draw.GetElementValue(iNode, "MIMEType", "image/png");
					break;
				case "External":
				default:
					this.rbExternal.Checked = true;
					this.tbValueExternal.Text = val;
					break;
			}
			this.cbSizing.Text = _Draw.GetElementValue(iNode, "Sizing", "AutoSize");

			fSource = fValue = fSizing = fMIMEType = false;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bExternalExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bEmbeddedExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bMimeExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bDatabaseExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bEmbedded = new Oranikle.Studio.Controls.StyledButton();
            this.bExternal = new Oranikle.Studio.Controls.StyledButton();
            this.tbValueExternal = new Oranikle.Studio.Controls.CustomTextControl();
            this.cbValueDatabase = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbMIMEType = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbValueEmbedded = new Oranikle.Studio.Controls.StyledComboBox();
            this.rbDatabase = new System.Windows.Forms.RadioButton();
            this.rbEmbedded = new System.Windows.Forms.RadioButton();
            this.rbExternal = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSizing = new Oranikle.Studio.Controls.StyledComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bExternalExpr);
            this.groupBox1.Controls.Add(this.bEmbeddedExpr);
            this.groupBox1.Controls.Add(this.bMimeExpr);
            this.groupBox1.Controls.Add(this.bDatabaseExpr);
            this.groupBox1.Controls.Add(this.bEmbedded);
            this.groupBox1.Controls.Add(this.bExternal);
            this.groupBox1.Controls.Add(this.tbValueExternal);
            this.groupBox1.Controls.Add(this.cbValueDatabase);
            this.groupBox1.Controls.Add(this.cbMIMEType);
            this.groupBox1.Controls.Add(this.cbValueEmbedded);
            this.groupBox1.Controls.Add(this.rbDatabase);
            this.groupBox1.Controls.Add(this.rbEmbedded);
            this.groupBox1.Controls.Add(this.rbExternal);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // bExternalExpr
            // 
            this.bExternalExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bExternalExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bExternalExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bExternalExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bExternalExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExternalExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bExternalExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bExternalExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExternalExpr.Location = new System.Drawing.Point(352, 26);
            this.bExternalExpr.Name = "bExternalExpr";
            this.bExternalExpr.OverriddenSize = null;
            this.bExternalExpr.Size = new System.Drawing.Size(22, 21);
            this.bExternalExpr.TabIndex = 12;
            this.bExternalExpr.Tag = "external";
            this.bExternalExpr.Text = "fx";
            this.bExternalExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExternalExpr.UseVisualStyleBackColor = true;
            this.bExternalExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bEmbeddedExpr
            // 
            this.bEmbeddedExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bEmbeddedExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bEmbeddedExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bEmbeddedExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bEmbeddedExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEmbeddedExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEmbeddedExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bEmbeddedExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEmbeddedExpr.Location = new System.Drawing.Point(352, 58);
            this.bEmbeddedExpr.Name = "bEmbeddedExpr";
            this.bEmbeddedExpr.OverriddenSize = null;
            this.bEmbeddedExpr.Size = new System.Drawing.Size(22, 21);
            this.bEmbeddedExpr.TabIndex = 11;
            this.bEmbeddedExpr.Tag = "embedded";
            this.bEmbeddedExpr.Text = "fx";
            this.bEmbeddedExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEmbeddedExpr.UseVisualStyleBackColor = true;
            this.bEmbeddedExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bMimeExpr
            // 
            this.bMimeExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMimeExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMimeExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMimeExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMimeExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMimeExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMimeExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMimeExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMimeExpr.Location = new System.Drawing.Point(184, 90);
            this.bMimeExpr.Name = "bMimeExpr";
            this.bMimeExpr.OverriddenSize = null;
            this.bMimeExpr.Size = new System.Drawing.Size(22, 21);
            this.bMimeExpr.TabIndex = 10;
            this.bMimeExpr.Tag = "mime";
            this.bMimeExpr.Text = "fx";
            this.bMimeExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMimeExpr.UseVisualStyleBackColor = true;
            this.bMimeExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bDatabaseExpr
            // 
            this.bDatabaseExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDatabaseExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDatabaseExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDatabaseExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDatabaseExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDatabaseExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDatabaseExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDatabaseExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDatabaseExpr.Location = new System.Drawing.Point(352, 122);
            this.bDatabaseExpr.Name = "bDatabaseExpr";
            this.bDatabaseExpr.OverriddenSize = null;
            this.bDatabaseExpr.Size = new System.Drawing.Size(22, 21);
            this.bDatabaseExpr.TabIndex = 9;
            this.bDatabaseExpr.Tag = "database";
            this.bDatabaseExpr.Text = "fx";
            this.bDatabaseExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDatabaseExpr.UseVisualStyleBackColor = true;
            this.bDatabaseExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bEmbedded
            // 
            this.bEmbedded.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bEmbedded.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bEmbedded.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bEmbedded.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bEmbedded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEmbedded.Font = new System.Drawing.Font("Arial", 9F);
            this.bEmbedded.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bEmbedded.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEmbedded.Location = new System.Drawing.Point(378, 58);
            this.bEmbedded.Name = "bEmbedded";
            this.bEmbedded.OverriddenSize = null;
            this.bEmbedded.Size = new System.Drawing.Size(22, 21);
            this.bEmbedded.TabIndex = 8;
            this.bEmbedded.Text = "...";
            this.bEmbedded.UseVisualStyleBackColor = true;
            this.bEmbedded.Click += new System.EventHandler(this.bEmbedded_Click);
            // 
            // bExternal
            // 
            this.bExternal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bExternal.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bExternal.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bExternal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bExternal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExternal.Font = new System.Drawing.Font("Arial", 9F);
            this.bExternal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bExternal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExternal.Location = new System.Drawing.Point(378, 26);
            this.bExternal.Name = "bExternal";
            this.bExternal.OverriddenSize = null;
            this.bExternal.Size = new System.Drawing.Size(22, 21);
            this.bExternal.TabIndex = 7;
            this.bExternal.Text = "...";
            this.bExternal.UseVisualStyleBackColor = true;
            this.bExternal.Click += new System.EventHandler(this.bExternal_Click);
            // 
            // tbValueExternal
            // 
            this.tbValueExternal.AddX = 0;
            this.tbValueExternal.AddY = 0;
            this.tbValueExternal.AllowSpace = false;
            this.tbValueExternal.BorderColor = System.Drawing.Color.LightGray;
            this.tbValueExternal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValueExternal.ChangeVisibility = false;
            this.tbValueExternal.ChildControl = null;
            this.tbValueExternal.ConvertEnterToTab = true;
            this.tbValueExternal.ConvertEnterToTabForDialogs = false;
            this.tbValueExternal.Decimals = 0;
            this.tbValueExternal.DisplayList = new object[0];
            this.tbValueExternal.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbValueExternal.Location = new System.Drawing.Point(88, 26);
            this.tbValueExternal.Name = "tbValueExternal";
            this.tbValueExternal.OnDropDownCloseFocus = true;
            this.tbValueExternal.SelectType = 0;
            this.tbValueExternal.Size = new System.Drawing.Size(256, 20);
            this.tbValueExternal.TabIndex = 6;
            this.tbValueExternal.UseValueForChildsVisibilty = false;
            this.tbValueExternal.Value = true;
            this.tbValueExternal.TextChanged += new System.EventHandler(this.Value_TextChanged);
            // 
            // cbValueDatabase
            // 
            this.cbValueDatabase.AutoAdjustItemHeight = false;
            this.cbValueDatabase.BorderColor = System.Drawing.Color.LightGray;
            this.cbValueDatabase.ConvertEnterToTabForDialogs = false;
            this.cbValueDatabase.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValueDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValueDatabase.Location = new System.Drawing.Point(88, 122);
            this.cbValueDatabase.Name = "cbValueDatabase";
            this.cbValueDatabase.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValueDatabase.SeparatorMargin = 1;
            this.cbValueDatabase.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValueDatabase.SeparatorWidth = 1;
            this.cbValueDatabase.Size = new System.Drawing.Size(256, 21);
            this.cbValueDatabase.TabIndex = 5;
            this.cbValueDatabase.TextChanged += new System.EventHandler(this.Value_TextChanged);
            // 
            // cbMIMEType
            // 
            this.cbMIMEType.AutoAdjustItemHeight = false;
            this.cbMIMEType.BorderColor = System.Drawing.Color.LightGray;
            this.cbMIMEType.ConvertEnterToTabForDialogs = false;
            this.cbMIMEType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMIMEType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMIMEType.Items.AddRange(new object[] {
            "image/bmp",
            "image/jpeg",
            "image/gif",
            "image/png",
            "image/x-png"});
            this.cbMIMEType.Location = new System.Drawing.Point(88, 90);
            this.cbMIMEType.Name = "cbMIMEType";
            this.cbMIMEType.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMIMEType.SeparatorMargin = 1;
            this.cbMIMEType.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMIMEType.SeparatorWidth = 1;
            this.cbMIMEType.Size = new System.Drawing.Size(88, 21);
            this.cbMIMEType.TabIndex = 4;
            this.cbMIMEType.Text = "image/jpeg";
            this.cbMIMEType.SelectedIndexChanged += new System.EventHandler(this.cbMIMEType_SelectedIndexChanged);
            // 
            // cbValueEmbedded
            // 
            this.cbValueEmbedded.AutoAdjustItemHeight = false;
            this.cbValueEmbedded.BorderColor = System.Drawing.Color.LightGray;
            this.cbValueEmbedded.ConvertEnterToTabForDialogs = false;
            this.cbValueEmbedded.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValueEmbedded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValueEmbedded.Location = new System.Drawing.Point(88, 58);
            this.cbValueEmbedded.Name = "cbValueEmbedded";
            this.cbValueEmbedded.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValueEmbedded.SeparatorMargin = 1;
            this.cbValueEmbedded.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValueEmbedded.SeparatorWidth = 1;
            this.cbValueEmbedded.Size = new System.Drawing.Size(256, 21);
            this.cbValueEmbedded.TabIndex = 3;
            this.cbValueEmbedded.TextChanged += new System.EventHandler(this.Value_TextChanged);
            // 
            // rbDatabase
            // 
            this.rbDatabase.Location = new System.Drawing.Point(8, 88);
            this.rbDatabase.Name = "rbDatabase";
            this.rbDatabase.Size = new System.Drawing.Size(80, 24);
            this.rbDatabase.TabIndex = 2;
            this.rbDatabase.Text = "Database";
            this.rbDatabase.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbEmbedded
            // 
            this.rbEmbedded.Location = new System.Drawing.Point(8, 56);
            this.rbEmbedded.Name = "rbEmbedded";
            this.rbEmbedded.Size = new System.Drawing.Size(80, 24);
            this.rbEmbedded.TabIndex = 1;
            this.rbEmbedded.Text = "Embedded";
            this.rbEmbedded.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbExternal
            // 
            this.rbExternal.Location = new System.Drawing.Point(8, 24);
            this.rbExternal.Name = "rbExternal";
            this.rbExternal.Size = new System.Drawing.Size(80, 24);
            this.rbExternal.TabIndex = 0;
            this.rbExternal.Text = "External";
            this.rbExternal.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sizing";
            // 
            // cbSizing
            // 
            this.cbSizing.AutoAdjustItemHeight = false;
            this.cbSizing.BorderColor = System.Drawing.Color.LightGray;
            this.cbSizing.ConvertEnterToTabForDialogs = false;
            this.cbSizing.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbSizing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSizing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSizing.Items.AddRange(new object[] {
            "AutoSize",
            "Fit",
            "FitProportional",
            "Clip"});
            this.cbSizing.Location = new System.Drawing.Point(72, 184);
            this.cbSizing.Name = "cbSizing";
            this.cbSizing.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbSizing.SeparatorMargin = 1;
            this.cbSizing.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbSizing.SeparatorWidth = 1;
            this.cbSizing.Size = new System.Drawing.Size(96, 21);
            this.cbSizing.TabIndex = 2;
            this.cbSizing.SelectedIndexChanged += new System.EventHandler(this.cbSizing_SelectedIndexChanged);
            // 
            // ImageCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.cbSizing);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ImageCtl";
            this.Size = new System.Drawing.Size(472, 288);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
       	public bool IsValid()
		{
			return true;
		}

		public void Apply()
		{
			// take information in control and apply to all the style nodes
			//  Only change information that has been marked as modified;
			//   this way when group is selected it is possible to change just
			//   the items you want and keep the rest the same.
				
			foreach (XmlNode riNode in this._ReportItems)
				ApplyChanges(riNode);

			// No more changes
			fSource = fValue = fSizing = fMIMEType = false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (fSource || fValue || fMIMEType)
			{
				string source="";
				string val="";
				if (rbEmbedded.Checked)
				{
					val = cbValueEmbedded.Text;
					source = "Embedded";
				}
				else if (rbDatabase.Checked)
				{
					source = "Database";
					val = cbValueDatabase.Text;
					_Draw.SetElement(node, "MIMEType", this.cbMIMEType.Text);
				}
				else 
				{	// must be external
					source = "External";
					val = tbValueExternal.Text;
				}
				_Draw.SetElement(node, "Source", source);
				_Draw.SetElement(node, "Value", val);
			}
			if (fSizing)
				_Draw.SetElement(node, "Sizing", this.cbSizing.Text);
		}

		private void Value_TextChanged(object sender, System.EventArgs e)
		{
			fValue = true;
		}

		private void cbMIMEType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMIMEType = true;
		}

		private void rbSource_CheckedChanged(object sender, System.EventArgs e)
		{
			fSource = true;
			this.cbValueDatabase.Enabled = this.cbMIMEType.Enabled = 
				this.bDatabaseExpr.Enabled = this.rbDatabase.Checked;
			this.cbValueEmbedded.Enabled = this.bEmbeddedExpr.Enabled = 
				this.bEmbedded.Enabled = this.rbEmbedded.Checked;
			this.tbValueExternal.Enabled = this.bExternalExpr.Enabled = 
				this.bExternal.Enabled = this.rbExternal.Checked;
		}

		private void cbSizing_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fSizing = true;
		}

		private void bExternal_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Bitmap Files (*.bmp)|*.bmp" +
				"|JPEG (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif" +
				"|GIF (*.gif)|*.gif" +
				"|TIFF (*.tif;*.tiff)|*.tif;*.tiff" +
				"|PNG (*.png)|*.png" +
				"|All Picture Files|*.bmp;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.tif;*.tiff;*.png" +
				"|All files (*.*)|*.*";
			ofd.FilterIndex = 6;
			ofd.CheckFileExists = true;
            try
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    tbValueExternal.Text = ofd.FileName;
                }
            }
            finally
            {
                ofd.Dispose();
            }
		}

		private void bEmbedded_Click(object sender, System.EventArgs e)
		{
			DialogEmbeddedImages dlgEI = new DialogEmbeddedImages(this._Draw);
			dlgEI.StartPosition = FormStartPosition.CenterParent;
            try
            {
                DialogResult dr = dlgEI.ShowDialog();
                if (dr != DialogResult.OK)
                    return;
            }
            finally
            {
                dlgEI.Dispose();
            }
			// Populate the EmbeddedImage names
			cbValueEmbedded.Items.Clear();
			cbValueEmbedded.Items.AddRange(_Draw.ReportNames.EmbeddedImageNames);

		}
		private void bExpr_Click(object sender, System.EventArgs e)
		{
			Button b = sender as Button;
			if (b == null)
				return;
			Control c = null;
			switch (b.Tag as string)
			{
				case "external":
					c = tbValueExternal;
					break;
				case "embedded":
					c = cbValueEmbedded;
					break;
				case "mime":
					c = cbMIMEType;
					break;
				case "database":
					c = cbValueDatabase;
					break;
			}

			if (c == null)
				return;

			XmlNode sNode = _ReportItems[0];

			DialogExprEditor ee = new DialogExprEditor(_Draw, c.Text, sNode);
            try
            {
                DialogResult dr = ee.ShowDialog();
                if (dr == DialogResult.OK)
                    c.Text = ee.Expression;
            }
            finally
            {
                ee.Dispose();
            }
            
            return;
		}

	}
}
