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
	internal class PositionCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		bool fName, fLeft, fTop, fWidth, fHeight, fZIndex, fColSpan;
		bool fCanGrow, fCanShrink, fHideDuplicates, fToggleImage, fDataElementStyle;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private Oranikle.Studio.Controls.CustomTextControl tbWidth;
		private Oranikle.Studio.Controls.CustomTextControl tbTop;
		private Oranikle.Studio.Controls.CustomTextControl tbLeft;
		private System.Windows.Forms.NumericUpDown tbZIndex;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbName;
		private Oranikle.Studio.Controls.CustomTextControl tbHeight;
		private System.Windows.Forms.GroupBox gbPosition;
		private System.Windows.Forms.Label lblColSpan;
		private System.Windows.Forms.NumericUpDown tbColSpan;
		private System.Windows.Forms.GroupBox gbText;
		private Oranikle.Studio.Controls.StyledCheckBox chkCanGrow;
		private Oranikle.Studio.Controls.StyledCheckBox chkCanShrink;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.StyledComboBox cbHideDuplicates;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.StyledComboBox cbDataElementStyle;
		private System.Windows.Forms.Label label4;
		private Oranikle.Studio.Controls.StyledComboBox cbToggleImage;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal PositionCtl(DesignXmlDraw dxDraw, List<XmlNode> ris)
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
			XmlNode riNode = _ReportItems[0];
			XmlNode tcell = null;

			if (_ReportItems.Count > 1)
			{
				tbName.Text = "Group Selected";
				tbName.Enabled = false;
				lblColSpan.Visible = tbColSpan.Visible = false;
			}
			else
			{
				XmlAttribute xa = riNode.Attributes["Name"];
				tbName.Text = xa == null? "": xa.Value;
				XmlNode ris = riNode.ParentNode;
				tcell = ris.ParentNode;
				if (tcell.Name != "TableCell")
					tcell = null;
			}
			
			this.tbZIndex.Value = Convert.ToInt32(_Draw.GetElementValue(riNode, "ZIndex", "0"));

			if (tcell != null)
			{
				gbPosition.Visible = false;
				this.gbText.Location = gbPosition.Location;
				string colspan = _Draw.GetElementValue(tcell, "ColSpan", "1");
				tbColSpan.Value = Convert.ToDecimal(colspan);
			}
			else
			{
				lblColSpan.Visible = tbColSpan.Visible = false;
				tbLeft.Text = _Draw.GetElementValue(riNode, "Left", "0pt");
				tbTop.Text = _Draw.GetElementValue(riNode, "Top", "0pt");
				tbWidth.Text = _Draw.GetElementValue(riNode, "Width", "");
				tbHeight.Text = _Draw.GetElementValue(riNode, "Height", "");
			}

			if (riNode.Name == "Textbox")
			{
				this.cbDataElementStyle.Text = _Draw.GetElementValue(riNode, "DataElementStyle", "Auto");
				cbHideDuplicates.Items.Add("");
				object[] dsn = _Draw.DataSetNames;
				if (dsn != null)
					cbHideDuplicates.Items.AddRange(dsn);
				object[] grps = _Draw.GroupingNames;
				if (grps != null)
					cbHideDuplicates.Items.AddRange(grps);
				this.cbHideDuplicates.Text = _Draw.GetElementValue(riNode, "HideDuplicates", "");
				this.chkCanGrow.Checked = _Draw.GetElementValue(riNode, "CanGrow", "false").ToLower() == "true";
				this.chkCanShrink.Checked = _Draw.GetElementValue(riNode, "CanShrink", "false").ToLower() == "true";
				XmlNode initstate = DesignXmlDraw.FindNextInHierarchy(riNode, "ToggleImage", "InitialState");
				this.cbToggleImage.Text = initstate == null? "": initstate.InnerText;
			}
			else
			{
				this.gbText.Visible = false;
			}

			fName = fLeft = fTop = fWidth = fHeight = fZIndex = fColSpan = 
				fCanGrow = fCanShrink = fHideDuplicates = fToggleImage = fDataElementStyle = false;
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
            this.gbPosition = new System.Windows.Forms.GroupBox();
            this.tbHeight = new Oranikle.Studio.Controls.CustomTextControl();
            this.label7 = new System.Windows.Forms.Label();
            this.tbWidth = new Oranikle.Studio.Controls.CustomTextControl();
            this.label8 = new System.Windows.Forms.Label();
            this.tbTop = new Oranikle.Studio.Controls.CustomTextControl();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLeft = new Oranikle.Studio.Controls.CustomTextControl();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbZIndex = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new Oranikle.Studio.Controls.CustomTextControl();
            this.lblColSpan = new System.Windows.Forms.Label();
            this.tbColSpan = new System.Windows.Forms.NumericUpDown();
            this.gbText = new System.Windows.Forms.GroupBox();
            this.cbToggleImage = new Oranikle.Studio.Controls.StyledComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDataElementStyle = new Oranikle.Studio.Controls.StyledComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbHideDuplicates = new Oranikle.Studio.Controls.StyledComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCanShrink = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkCanGrow = new Oranikle.Studio.Controls.StyledCheckBox();
            this.gbPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbColSpan)).BeginInit();
            this.gbText.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPosition
            // 
            this.gbPosition.Controls.Add(this.tbHeight);
            this.gbPosition.Controls.Add(this.label7);
            this.gbPosition.Controls.Add(this.tbWidth);
            this.gbPosition.Controls.Add(this.label8);
            this.gbPosition.Controls.Add(this.tbTop);
            this.gbPosition.Controls.Add(this.label6);
            this.gbPosition.Controls.Add(this.tbLeft);
            this.gbPosition.Controls.Add(this.label5);
            this.gbPosition.Controls.Add(this.label9);
            this.gbPosition.Controls.Add(this.tbZIndex);
            this.gbPosition.Location = new System.Drawing.Point(24, 48);
            this.gbPosition.Name = "gbPosition";
            this.gbPosition.Size = new System.Drawing.Size(384, 112);
            this.gbPosition.TabIndex = 14;
            this.gbPosition.TabStop = false;
            this.gbPosition.Text = "Position";
            // 
            // tbHeight
            // 
            this.tbHeight.AddX = 0;
            this.tbHeight.AddY = 0;
            this.tbHeight.AllowSpace = false;
            this.tbHeight.BorderColor = System.Drawing.Color.LightGray;
            this.tbHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHeight.ChangeVisibility = false;
            this.tbHeight.ChildControl = null;
            this.tbHeight.ConvertEnterToTab = true;
            this.tbHeight.ConvertEnterToTabForDialogs = false;
            this.tbHeight.Decimals = 0;
            this.tbHeight.DisplayList = new object[0];
            this.tbHeight.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbHeight.Location = new System.Drawing.Point(224, 48);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.OnDropDownCloseFocus = true;
            this.tbHeight.SelectType = 0;
            this.tbHeight.Size = new System.Drawing.Size(72, 20);
            this.tbHeight.TabIndex = 5;
            this.tbHeight.UseValueForChildsVisibilty = false;
            this.tbHeight.Value = true;
            this.tbHeight.TextChanged += new System.EventHandler(this.tbHeight_TextChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(168, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Height";
            // 
            // tbWidth
            // 
            this.tbWidth.AddX = 0;
            this.tbWidth.AddY = 0;
            this.tbWidth.AllowSpace = false;
            this.tbWidth.BorderColor = System.Drawing.Color.LightGray;
            this.tbWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWidth.ChangeVisibility = false;
            this.tbWidth.ChildControl = null;
            this.tbWidth.ConvertEnterToTab = true;
            this.tbWidth.ConvertEnterToTabForDialogs = false;
            this.tbWidth.Decimals = 0;
            this.tbWidth.DisplayList = new object[0];
            this.tbWidth.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbWidth.Location = new System.Drawing.Point(72, 48);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.OnDropDownCloseFocus = true;
            this.tbWidth.SelectType = 0;
            this.tbWidth.Size = new System.Drawing.Size(72, 20);
            this.tbWidth.TabIndex = 3;
            this.tbWidth.UseValueForChildsVisibilty = false;
            this.tbWidth.Value = true;
            this.tbWidth.TextChanged += new System.EventHandler(this.tbWidth_TextChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Width";
            // 
            // tbTop
            // 
            this.tbTop.AddX = 0;
            this.tbTop.AddY = 0;
            this.tbTop.AllowSpace = false;
            this.tbTop.BorderColor = System.Drawing.Color.LightGray;
            this.tbTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTop.ChangeVisibility = false;
            this.tbTop.ChildControl = null;
            this.tbTop.ConvertEnterToTab = true;
            this.tbTop.ConvertEnterToTabForDialogs = false;
            this.tbTop.Decimals = 0;
            this.tbTop.DisplayList = new object[0];
            this.tbTop.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbTop.Location = new System.Drawing.Point(224, 16);
            this.tbTop.Name = "tbTop";
            this.tbTop.OnDropDownCloseFocus = true;
            this.tbTop.SelectType = 0;
            this.tbTop.Size = new System.Drawing.Size(72, 20);
            this.tbTop.TabIndex = 2;
            this.tbTop.UseValueForChildsVisibilty = false;
            this.tbTop.Value = true;
            this.tbTop.TextChanged += new System.EventHandler(this.tbTop_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(168, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Top";
            // 
            // tbLeft
            // 
            this.tbLeft.AddX = 0;
            this.tbLeft.AddY = 0;
            this.tbLeft.AllowSpace = false;
            this.tbLeft.BorderColor = System.Drawing.Color.LightGray;
            this.tbLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLeft.ChangeVisibility = false;
            this.tbLeft.ChildControl = null;
            this.tbLeft.ConvertEnterToTab = true;
            this.tbLeft.ConvertEnterToTabForDialogs = false;
            this.tbLeft.Decimals = 0;
            this.tbLeft.DisplayList = new object[0];
            this.tbLeft.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbLeft.Location = new System.Drawing.Point(72, 16);
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.OnDropDownCloseFocus = true;
            this.tbLeft.SelectType = 0;
            this.tbLeft.Size = new System.Drawing.Size(72, 20);
            this.tbLeft.TabIndex = 0;
            this.tbLeft.UseValueForChildsVisibilty = false;
            this.tbLeft.Value = true;
            this.tbLeft.TextChanged += new System.EventHandler(this.tbLeft_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Left";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 23);
            this.label9.TabIndex = 15;
            this.label9.Text = "Z Index";
            // 
            // tbZIndex
            // 
            this.tbZIndex.Location = new System.Drawing.Point(72, 81);
            this.tbZIndex.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.tbZIndex.Name = "tbZIndex";
            this.tbZIndex.Size = new System.Drawing.Size(72, 20);
            this.tbZIndex.TabIndex = 6;
            this.tbZIndex.ValueChanged += new System.EventHandler(this.tbZIndex_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.AddX = 0;
            this.tbName.AddY = 0;
            this.tbName.AllowSpace = false;
            this.tbName.BorderColor = System.Drawing.Color.LightGray;
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.ChangeVisibility = false;
            this.tbName.ChildControl = null;
            this.tbName.ConvertEnterToTab = true;
            this.tbName.ConvertEnterToTabForDialogs = false;
            this.tbName.Decimals = 0;
            this.tbName.DisplayList = new object[0];
            this.tbName.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbName.Location = new System.Drawing.Point(80, 16);
            this.tbName.Name = "tbName";
            this.tbName.OnDropDownCloseFocus = true;
            this.tbName.SelectType = 0;
            this.tbName.Size = new System.Drawing.Size(128, 20);
            this.tbName.TabIndex = 0;
            this.tbName.Text = "textBox1";
            this.tbName.UseValueForChildsVisibilty = false;
            this.tbName.Value = true;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // lblColSpan
            // 
            this.lblColSpan.Location = new System.Drawing.Point(232, 15);
            this.lblColSpan.Name = "lblColSpan";
            this.lblColSpan.Size = new System.Drawing.Size(112, 23);
            this.lblColSpan.TabIndex = 16;
            this.lblColSpan.Text = "Span Table Columns";
            // 
            // tbColSpan
            // 
            this.tbColSpan.Location = new System.Drawing.Point(360, 16);
            this.tbColSpan.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tbColSpan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbColSpan.Name = "tbColSpan";
            this.tbColSpan.Size = new System.Drawing.Size(48, 20);
            this.tbColSpan.TabIndex = 1;
            this.tbColSpan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbColSpan.ValueChanged += new System.EventHandler(this.tbColSpan_ValueChanged);
            // 
            // gbText
            // 
            this.gbText.Controls.Add(this.cbToggleImage);
            this.gbText.Controls.Add(this.label4);
            this.gbText.Controls.Add(this.cbDataElementStyle);
            this.gbText.Controls.Add(this.label3);
            this.gbText.Controls.Add(this.cbHideDuplicates);
            this.gbText.Controls.Add(this.label2);
            this.gbText.Controls.Add(this.chkCanShrink);
            this.gbText.Controls.Add(this.chkCanGrow);
            this.gbText.Location = new System.Drawing.Point(24, 168);
            this.gbText.Name = "gbText";
            this.gbText.Size = new System.Drawing.Size(384, 112);
            this.gbText.TabIndex = 18;
            this.gbText.TabStop = false;
            this.gbText.Text = "Text Processing";
            // 
            // cbToggleImage
            // 
            this.cbToggleImage.AutoAdjustItemHeight = false;
            this.cbToggleImage.BorderColor = System.Drawing.Color.LightGray;
            this.cbToggleImage.ConvertEnterToTabForDialogs = false;
            this.cbToggleImage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbToggleImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbToggleImage.Items.AddRange(new object[] {
            "",
            "true",
            "false"});
            this.cbToggleImage.Location = new System.Drawing.Point(120, 80);
            this.cbToggleImage.Name = "cbToggleImage";
            this.cbToggleImage.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbToggleImage.SeparatorMargin = 1;
            this.cbToggleImage.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbToggleImage.SeparatorWidth = 1;
            this.cbToggleImage.Size = new System.Drawing.Size(256, 21);
            this.cbToggleImage.TabIndex = 7;
            this.cbToggleImage.SelectedIndexChanged += new System.EventHandler(this.cbToggleImage_Changed);
            this.cbToggleImage.TextChanged += new System.EventHandler(this.cbToggleImage_Changed);
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(16, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Toggle System.Drawing.Image";
            // 
            // cbDataElementStyle
            // 
            this.cbDataElementStyle.AutoAdjustItemHeight = false;
            this.cbDataElementStyle.BorderColor = System.Drawing.Color.LightGray;
            this.cbDataElementStyle.ConvertEnterToTabForDialogs = false;
            this.cbDataElementStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDataElementStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataElementStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDataElementStyle.Items.AddRange(new object[] {
            "Auto",
            "AttributeNormal",
            "ElementNormal"});
            this.cbDataElementStyle.Location = new System.Drawing.Point(120, 48);
            this.cbDataElementStyle.Name = "cbDataElementStyle";
            this.cbDataElementStyle.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDataElementStyle.SeparatorMargin = 1;
            this.cbDataElementStyle.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDataElementStyle.SeparatorWidth = 1;
            this.cbDataElementStyle.Size = new System.Drawing.Size(120, 21);
            this.cbDataElementStyle.TabIndex = 5;
            this.cbDataElementStyle.SelectedIndexChanged += new System.EventHandler(this.cbDataElementStyle_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(16, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "XML Element Style";
            // 
            // cbHideDuplicates
            // 
            this.cbHideDuplicates.AutoAdjustItemHeight = false;
            this.cbHideDuplicates.BorderColor = System.Drawing.Color.LightGray;
            this.cbHideDuplicates.ConvertEnterToTabForDialogs = false;
            this.cbHideDuplicates.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbHideDuplicates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHideDuplicates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbHideDuplicates.Location = new System.Drawing.Point(264, 16);
            this.cbHideDuplicates.Name = "cbHideDuplicates";
            this.cbHideDuplicates.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbHideDuplicates.SeparatorMargin = 1;
            this.cbHideDuplicates.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbHideDuplicates.SeparatorWidth = 1;
            this.cbHideDuplicates.Size = new System.Drawing.Size(112, 21);
            this.cbHideDuplicates.TabIndex = 3;
            this.cbHideDuplicates.SelectedIndexChanged += new System.EventHandler(this.cbHideDuplicates_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label2.Location = new System.Drawing.Point(184, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hide Duplicates";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkCanShrink
            // 
            this.chkCanShrink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCanShrink.ForeColor = System.Drawing.Color.Black;
            this.chkCanShrink.Location = new System.Drawing.Point(96, 14);
            this.chkCanShrink.Name = "chkCanShrink";
            this.chkCanShrink.Size = new System.Drawing.Size(104, 24);
            this.chkCanShrink.TabIndex = 1;
            this.chkCanShrink.Text = "Can Shrink";
            this.chkCanShrink.CheckedChanged += new System.EventHandler(this.chkCanShrink_CheckedChanged);
            // 
            // chkCanGrow
            // 
            this.chkCanGrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCanGrow.ForeColor = System.Drawing.Color.Black;
            this.chkCanGrow.Location = new System.Drawing.Point(16, 14);
            this.chkCanGrow.Name = "chkCanGrow";
            this.chkCanGrow.Size = new System.Drawing.Size(80, 24);
            this.chkCanGrow.TabIndex = 0;
            this.chkCanGrow.Text = "Can Grow";
            this.chkCanGrow.CheckedChanged += new System.EventHandler(this.chkCanGrow_CheckedChanged);
            // 
            // PositionCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.gbText);
            this.Controls.Add(this.tbColSpan);
            this.Controls.Add(this.lblColSpan);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbPosition);
            this.Name = "PositionCtl";
            this.Size = new System.Drawing.Size(472, 288);
            this.gbPosition.ResumeLayout(false);
            this.gbPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbColSpan)).EndInit();
            this.gbText.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public bool IsValid()
		{
			XmlNode ri = this._ReportItems[0] as XmlNode;
			if (tbName.Enabled && fName)
			{
				string nerr = _Draw.NameError(ri, this.tbName.Text);
				if (nerr != null)
				{
					MessageBox.Show(nerr, "Name");
					return false;
				}
			}
			string name="";
			try
			{	// allow minus if Line and only one item selected
				bool bMinus= ri.Name == "Line" && tbName.Enabled? true: false;
				if (fLeft)
				{
					name = "Left";
					DesignerUtility.ValidateSize(this.tbLeft.Text, true, false);
				}
				if (fTop)
				{
					name = "Top";
					DesignerUtility.ValidateSize(this.tbTop.Text, true, false);
				}
				if (fWidth)
				{
					name = "Width";
					DesignerUtility.ValidateSize(this.tbWidth.Text, true, bMinus);
				}
				if (fHeight)
				{
					name = "Height";
					DesignerUtility.ValidateSize(this.tbHeight.Text, true, bMinus);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, name + " Size is Invalid");
				return false;
			}

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
			fName = fLeft = fTop = fWidth = fHeight = fZIndex = fColSpan = 
				fCanGrow = fCanShrink = fHideDuplicates = fToggleImage = fDataElementStyle = false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (tbName.Enabled && fName)
			{
				_Draw.SetName(node, tbName.Text.Trim());
			}

			if (fLeft)
				_Draw.SetElement(node, "Left", DesignerUtility.MakeValidSize(tbLeft.Text, true));

			if (fTop)
				_Draw.SetElement(node, "Top", DesignerUtility.MakeValidSize(tbTop.Text, true));

			bool bLine = node.Name == "Line";

			if (fWidth)
				_Draw.SetElement(node, "Width", DesignerUtility.MakeValidSize(tbWidth.Text, bLine, bLine));

			if (fHeight)
				_Draw.SetElement(node, "Height", DesignerUtility.MakeValidSize(tbHeight.Text, bLine, bLine));

			if (fZIndex)
				_Draw.SetElement(node, "ZIndex", tbZIndex.Text);

			if (fColSpan)
			{
				XmlNode ris = node.ParentNode;
				XmlNode tcell = ris.ParentNode;
				if (tcell.Name == "TableCell")
				{	// SetTableCellColSpan does all the heavy lifting; 
					//    ie making sure the # of columns continue to match
					_Draw.SetTableCellColSpan(tcell, tbColSpan.Value.ToString());	 
				}
			}

			if (fDataElementStyle)
				_Draw.SetElement(node, "DataElementStyle", this.cbDataElementStyle.Text);
			if (fHideDuplicates)
				_Draw.SetElement(node, "HideDuplicates", this.cbHideDuplicates.Text);
			if (fCanGrow)
				_Draw.SetElement(node, "CanGrow", this.chkCanGrow.Checked? "true": "false");
			if (fCanShrink)
				_Draw.SetElement(node, "CanShrink", this.chkCanShrink.Checked? "true": "false");
			if (fDataElementStyle)
				_Draw.SetElement(node, "DataElementStyle", this.cbDataElementStyle.Text);
			if (fToggleImage)
			{
				if (cbToggleImage.Text.Length <= 0)
				{
					_Draw.RemoveElement(node, "ToggleImage");
				}
				else
				{
					XmlNode ti = _Draw.SetElement(node, "ToggleImage", null);
					_Draw.SetElement(ti, "InitialState", cbToggleImage.Text);
				}
			}
		}

		private void tbName_TextChanged(object sender, System.EventArgs e)
		{
			fName = true;
		}

		private void tbLeft_TextChanged(object sender, System.EventArgs e)
		{
			fLeft = true;
		}

		private void tbTop_TextChanged(object sender, System.EventArgs e)
		{
			fTop = true;
		}

		private void tbWidth_TextChanged(object sender, System.EventArgs e)
		{
			fWidth = true;
		}

		private void tbHeight_TextChanged(object sender, System.EventArgs e)
		{
			fHeight = true;
		}

		private void tbZIndex_ValueChanged(object sender, System.EventArgs e)
		{
			fZIndex = true;
		}

		private void tbName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			XmlNode xNode = this._ReportItems[0];

			string err = _Draw.NameError(xNode, tbName.Text.Trim());
			if (err != null)
			{
				e.Cancel = true;
				MessageBox.Show(string.Format("{0} is invalid.  {1}", tbName.Text, err), "Name");
				return;
			}
		}

		private void tbColSpan_ValueChanged(object sender, System.EventArgs e)
		{
			fColSpan = true;
		}

		private void cbToggleImage_Changed(object sender, System.EventArgs e)
		{
			fToggleImage = true;
		}

		private void cbDataElementStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fDataElementStyle = true;
		}

		private void cbHideDuplicates_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fHideDuplicates = true;
		}

		private void chkCanShrink_CheckedChanged(object sender, System.EventArgs e)
		{
			fCanShrink = true;
		}

		private void chkCanGrow_CheckedChanged(object sender, System.EventArgs e)
		{
			fCanGrow = true;
		}
	}
}
