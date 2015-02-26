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
using System.Globalization;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for StyleCtl.
	/// </summary>
	internal class StyleTextCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		private string _DataSetName;
		private bool fHorzAlign, fFormat, fDirection, fWritingMode, fTextDecoration;
		private bool fColor, fVerticalAlign, fFontStyle, fFontWeight, fFontSize, fFontFamily;
		private bool fValue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lFont;
		private Oranikle.Studio.Controls.StyledButton bFont;
		private Oranikle.Studio.Controls.StyledComboBox cbHorzAlign;
		private Oranikle.Studio.Controls.StyledComboBox cbFormat;
		private Oranikle.Studio.Controls.StyledComboBox cbDirection;
		private Oranikle.Studio.Controls.StyledComboBox cbWritingMode;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.StyledComboBox cbTextDecoration;
		private Oranikle.Studio.Controls.StyledButton bColor;
		private System.Windows.Forms.Label label9;
		private Oranikle.Studio.Controls.StyledComboBox cbColor;
		private Oranikle.Studio.Controls.StyledComboBox cbVerticalAlign;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.StyledComboBox cbFontStyle;
		private Oranikle.Studio.Controls.StyledComboBox cbFontWeight;
		private System.Windows.Forms.Label label10;
		private Oranikle.Studio.Controls.StyledComboBox cbFontSize;
		private System.Windows.Forms.Label label11;
		private Oranikle.Studio.Controls.StyledComboBox cbFontFamily;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblValue;
		private Oranikle.Studio.Controls.StyledComboBox cbValue;
		private Oranikle.Studio.Controls.StyledButton bValueExpr;
		private Oranikle.Studio.Controls.StyledButton bFamily;
		private Oranikle.Studio.Controls.StyledButton bStyle;
		private Oranikle.Studio.Controls.StyledButton bColorEx;
		private Oranikle.Studio.Controls.StyledButton bSize;
		private Oranikle.Studio.Controls.StyledButton bWeight;
		private Oranikle.Studio.Controls.StyledButton button2;
		private Oranikle.Studio.Controls.StyledButton bAlignment;
		private Oranikle.Studio.Controls.StyledButton bDirection;
		private Oranikle.Studio.Controls.StyledButton bVertical;
		private Oranikle.Studio.Controls.StyledButton bWrtMode;
		private Oranikle.Studio.Controls.StyledButton bFormat;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal StyleTextCtl(DesignXmlDraw dxDraw, List<XmlNode> styles)
		{
			_ReportItems = styles;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitTextStyles();
		}

		private void InitTextStyles()
		{
            cbColor.Items.AddRange(StaticLists.ColorList);

			XmlNode sNode = _ReportItems[0];
			if (_ReportItems.Count > 1)
			{
				cbValue.Text = "Group Selected";
				cbValue.Enabled = false;
				lblValue.Enabled = false;
			}
			else if (sNode.Name == "Textbox")
			{
				XmlNode vNode = _Draw.GetNamedChildNode(sNode, "Value");
				if (vNode != null)
					cbValue.Text = vNode.InnerText;
				// now populate the combo box
				// Find the dataregion that contains the Textbox (if any)
				for (XmlNode pNode = sNode.ParentNode; pNode != null; pNode = pNode.ParentNode)
				{
					if (pNode.Name == "List" ||
						pNode.Name == "Table" ||
						pNode.Name == "Matrix" ||
						pNode.Name == "Chart")
					{
						_DataSetName = _Draw.GetDataSetNameValue(pNode);
						if (_DataSetName != null)	// found it
						{
							string[] f = _Draw.GetFields(_DataSetName, true);
                            if (f != null)
							    cbValue.Items.AddRange(f);
						}
					}
				}
				// parameters
				string[] ps = _Draw.GetReportParameters(true);
				if (ps != null)
					cbValue.Items.AddRange(ps);
				// globals
				cbValue.Items.AddRange(StaticLists.GlobalList);
			}
			else if (sNode.Name == "Title" || sNode.Name == "fyi:Title2" || sNode.Name == "Title2")// 20022008 AJM GJL
			{
				lblValue.Text = "Caption";		// Note: label needs to equal the element name
				XmlNode vNode = _Draw.GetNamedChildNode(sNode, "Caption");
				if (vNode != null)
					cbValue.Text = vNode.InnerText;
			}
			else
			{
				lblValue.Visible = false;
				cbValue.Visible = false;
                bValueExpr.Visible = false;
			}

			sNode = _Draw.GetNamedChildNode(sNode, "Style");

			string sFontStyle="Normal";
			string sFontFamily="Arial";
			string sFontWeight="Normal";
			string sFontSize="10pt";
			string sTextDecoration="None";
			string sHorzAlign="General";
			string sVerticalAlign="Top";
			string sColor="Black";
			string sFormat="";
			string sDirection="LTR";
			string sWritingMode="lr-tb";
			foreach (XmlNode lNode in sNode)
			{
				if (lNode.NodeType != XmlNodeType.Element)
					continue;
				switch (lNode.Name)
				{
					case "FontStyle":
						sFontStyle = lNode.InnerText;
						break;
					case "FontFamily":
						sFontFamily = lNode.InnerText;
						break;
					case "FontWeight":
						sFontWeight = lNode.InnerText;
						break;
					case "FontSize":
						sFontSize = lNode.InnerText;
						break;
					case "TextDecoration":
						sTextDecoration = lNode.InnerText;
						break;
					case "TextAlign":
						sHorzAlign = lNode.InnerText;
						break;
					case "VerticalAlign":
						sVerticalAlign = lNode.InnerText;
						break;
					case "Color":
						sColor = lNode.InnerText;
						break;
					case "Format":
						sFormat = lNode.InnerText;
						break;
					case "Direction":
						sDirection = lNode.InnerText;
						break;
					case "WritingMode":
						sWritingMode = lNode.InnerText;
						break;
				}
			}

			// Population Font Family dropdown
			foreach (FontFamily ff in FontFamily.Families)
			{
				cbFontFamily.Items.Add(ff.Name);
			}

			this.cbFontStyle.Text = sFontStyle;
			this.cbFontFamily.Text = sFontFamily;
			this.cbFontWeight.Text = sFontWeight;
			this.cbFontSize.Text = sFontSize;
			this.cbTextDecoration.Text = sTextDecoration;
			this.cbHorzAlign.Text = sHorzAlign;
			this.cbVerticalAlign.Text = sVerticalAlign;
			this.cbColor.Text = sColor;
			this.cbFormat.Text = sFormat;
			this.cbDirection.Text = sDirection;
			this.cbWritingMode.Text = sWritingMode;

			fHorzAlign = fFormat = fDirection = fWritingMode = fTextDecoration =
				fColor = fVerticalAlign = fFontStyle = fFontWeight = fFontSize = fFontFamily =
				fValue = false;

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lFont = new System.Windows.Forms.Label();
            this.bFont = new Oranikle.Studio.Controls.StyledButton();
            this.cbVerticalAlign = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbHorzAlign = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbFormat = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbDirection = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbWritingMode = new Oranikle.Studio.Controls.StyledComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTextDecoration = new Oranikle.Studio.Controls.StyledComboBox();
            this.bColor = new Oranikle.Studio.Controls.StyledButton();
            this.label9 = new System.Windows.Forms.Label();
            this.cbColor = new Oranikle.Studio.Controls.StyledComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFontStyle = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbFontWeight = new Oranikle.Studio.Controls.StyledComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbFontSize = new Oranikle.Studio.Controls.StyledComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbFontFamily = new Oranikle.Studio.Controls.StyledComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new Oranikle.Studio.Controls.StyledButton();
            this.bWeight = new Oranikle.Studio.Controls.StyledButton();
            this.bSize = new Oranikle.Studio.Controls.StyledButton();
            this.bColorEx = new Oranikle.Studio.Controls.StyledButton();
            this.bStyle = new Oranikle.Studio.Controls.StyledButton();
            this.bFamily = new Oranikle.Studio.Controls.StyledButton();
            this.cbValue = new Oranikle.Studio.Controls.StyledComboBox();
            this.bValueExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bAlignment = new Oranikle.Studio.Controls.StyledButton();
            this.bDirection = new Oranikle.Studio.Controls.StyledButton();
            this.bVertical = new Oranikle.Studio.Controls.StyledButton();
            this.bWrtMode = new Oranikle.Studio.Controls.StyledButton();
            this.bFormat = new Oranikle.Studio.Controls.StyledButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Format";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(216, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Vertical";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Alignment";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Direction";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(216, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Writing Mode";
            // 
            // lFont
            // 
            this.lFont.Location = new System.Drawing.Point(16, 17);
            this.lFont.Name = "lFont";
            this.lFont.Size = new System.Drawing.Size(40, 16);
            this.lFont.TabIndex = 8;
            this.lFont.Text = "Family";
            // 
            // bFont
            // 
            this.bFont.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bFont.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bFont.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bFont.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFont.Font = new System.Drawing.Font("Arial", 9F);
            this.bFont.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bFont.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFont.Location = new System.Drawing.Point(401, 15);
            this.bFont.Name = "bFont";
            this.bFont.OverriddenSize = null;
            this.bFont.Size = new System.Drawing.Size(22, 21);
            this.bFont.TabIndex = 4;
            this.bFont.Text = "...";
            this.bFont.UseVisualStyleBackColor = true;
            this.bFont.Click += new System.EventHandler(this.bFont_Click);
            // 
            // cbVerticalAlign
            // 
            this.cbVerticalAlign.AutoAdjustItemHeight = false;
            this.cbVerticalAlign.BorderColor = System.Drawing.Color.LightGray;
            this.cbVerticalAlign.ConvertEnterToTabForDialogs = false;
            this.cbVerticalAlign.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbVerticalAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVerticalAlign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVerticalAlign.Items.AddRange(new object[] {
            "Top",
            "Middle",
            "Bottom"});
            this.cbVerticalAlign.Location = new System.Drawing.Point(304, 170);
            this.cbVerticalAlign.Name = "cbVerticalAlign";
            this.cbVerticalAlign.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbVerticalAlign.SeparatorMargin = 1;
            this.cbVerticalAlign.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbVerticalAlign.SeparatorWidth = 1;
            this.cbVerticalAlign.Size = new System.Drawing.Size(72, 21);
            this.cbVerticalAlign.TabIndex = 5;
            this.cbVerticalAlign.SelectedIndexChanged += new System.EventHandler(this.cbVerticalAlign_TextChanged);
            this.cbVerticalAlign.TextChanged += new System.EventHandler(this.cbVerticalAlign_TextChanged);
            // 
            // cbHorzAlign
            // 
            this.cbHorzAlign.AutoAdjustItemHeight = false;
            this.cbHorzAlign.BorderColor = System.Drawing.Color.LightGray;
            this.cbHorzAlign.ConvertEnterToTabForDialogs = false;
            this.cbHorzAlign.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbHorzAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHorzAlign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbHorzAlign.Items.AddRange(new object[] {
            "Left",
            "Center",
            "Right",
            "General"});
            this.cbHorzAlign.Location = new System.Drawing.Point(80, 170);
            this.cbHorzAlign.Name = "cbHorzAlign";
            this.cbHorzAlign.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbHorzAlign.SeparatorMargin = 1;
            this.cbHorzAlign.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbHorzAlign.SeparatorWidth = 1;
            this.cbHorzAlign.Size = new System.Drawing.Size(64, 21);
            this.cbHorzAlign.TabIndex = 3;
            this.cbHorzAlign.SelectedIndexChanged += new System.EventHandler(this.cbHorzAlign_TextChanged);
            this.cbHorzAlign.TextChanged += new System.EventHandler(this.cbHorzAlign_TextChanged);
            // 
            // cbFormat
            // 
            this.cbFormat.AutoAdjustItemHeight = false;
            this.cbFormat.BorderColor = System.Drawing.Color.LightGray;
            this.cbFormat.ConvertEnterToTabForDialogs = false;
            this.cbFormat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFormat.Items.AddRange(new object[] {
            "None",
            "",
            "#,##0",
            "#,##0.00",
            "0",
            "0.00",
            "",
            "MM/dd/yyyy",
            "dddd, MMMM dd, yyyy",
            "dddd, MMMM dd, yyyy HH:mm",
            "dddd, MMMM dd, yyyy HH:mm:ss",
            "MM/dd/yyyy HH:mm",
            "MM/dd/yyyy HH:mm:ss",
            "MMMM dd",
            "Ddd, dd MMM yyyy HH\':\'mm\'\"ss \'GMT\'",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss GMT",
            "HH:mm",
            "HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss"});
            this.cbFormat.Location = new System.Drawing.Point(80, 234);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbFormat.SeparatorMargin = 1;
            this.cbFormat.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbFormat.SeparatorWidth = 1;
            this.cbFormat.Size = new System.Drawing.Size(296, 21);
            this.cbFormat.TabIndex = 11;
            this.cbFormat.TextChanged += new System.EventHandler(this.cbFormat_TextChanged);
            // 
            // cbDirection
            // 
            this.cbDirection.AutoAdjustItemHeight = false;
            this.cbDirection.BorderColor = System.Drawing.Color.LightGray;
            this.cbDirection.ConvertEnterToTabForDialogs = false;
            this.cbDirection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDirection.Items.AddRange(new object[] {
            "LTR",
            "RTL"});
            this.cbDirection.Location = new System.Drawing.Point(80, 202);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDirection.SeparatorMargin = 1;
            this.cbDirection.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDirection.SeparatorWidth = 1;
            this.cbDirection.Size = new System.Drawing.Size(64, 21);
            this.cbDirection.TabIndex = 7;
            this.cbDirection.SelectedIndexChanged += new System.EventHandler(this.cbDirection_TextChanged);
            this.cbDirection.TextChanged += new System.EventHandler(this.cbDirection_TextChanged);
            // 
            // cbWritingMode
            // 
            this.cbWritingMode.AutoAdjustItemHeight = false;
            this.cbWritingMode.BorderColor = System.Drawing.Color.LightGray;
            this.cbWritingMode.ConvertEnterToTabForDialogs = false;
            this.cbWritingMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbWritingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWritingMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbWritingMode.Items.AddRange(new object[] {
            "lr-tb",
            "tb-rl"});
            this.cbWritingMode.Location = new System.Drawing.Point(304, 202);
            this.cbWritingMode.Name = "cbWritingMode";
            this.cbWritingMode.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbWritingMode.SeparatorMargin = 1;
            this.cbWritingMode.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbWritingMode.SeparatorWidth = 1;
            this.cbWritingMode.Size = new System.Drawing.Size(72, 21);
            this.cbWritingMode.TabIndex = 9;
            this.cbWritingMode.SelectedIndexChanged += new System.EventHandler(this.cbWritingMode_TextChanged);
            this.cbWritingMode.TextChanged += new System.EventHandler(this.cbWritingMode_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(224, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 19;
            this.label2.Text = "Decoration";
            // 
            // cbTextDecoration
            // 
            this.cbTextDecoration.AutoAdjustItemHeight = false;
            this.cbTextDecoration.BorderColor = System.Drawing.Color.LightGray;
            this.cbTextDecoration.ConvertEnterToTabForDialogs = false;
            this.cbTextDecoration.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbTextDecoration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTextDecoration.Items.AddRange(new object[] {
            "None",
            "Underline",
            "Overline",
            "LineThrough"});
            this.cbTextDecoration.Location = new System.Drawing.Point(288, 79);
            this.cbTextDecoration.Name = "cbTextDecoration";
            this.cbTextDecoration.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbTextDecoration.SeparatorMargin = 1;
            this.cbTextDecoration.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbTextDecoration.SeparatorWidth = 1;
            this.cbTextDecoration.Size = new System.Drawing.Size(80, 21);
            this.cbTextDecoration.TabIndex = 12;
            this.cbTextDecoration.SelectedIndexChanged += new System.EventHandler(this.cbTextDecoration_TextChanged);
            this.cbTextDecoration.TextChanged += new System.EventHandler(this.cbTextDecoration_TextChanged);
            // 
            // bColor
            // 
            this.bColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bColor.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bColor.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColor.Font = new System.Drawing.Font("Arial", 9F);
            this.bColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColor.Location = new System.Drawing.Point(200, 79);
            this.bColor.Name = "bColor";
            this.bColor.OverriddenSize = null;
            this.bColor.Size = new System.Drawing.Size(22, 21);
            this.bColor.TabIndex = 11;
            this.bColor.Text = "...";
            this.bColor.UseVisualStyleBackColor = true;
            this.bColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 16);
            this.label9.TabIndex = 22;
            this.label9.Text = "Color";
            // 
            // cbColor
            // 
            this.cbColor.AutoAdjustItemHeight = false;
            this.cbColor.BorderColor = System.Drawing.Color.LightGray;
            this.cbColor.ConvertEnterToTabForDialogs = false;
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbColor.Location = new System.Drawing.Point(72, 79);
            this.cbColor.Name = "cbColor";
            this.cbColor.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbColor.SeparatorMargin = 1;
            this.cbColor.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbColor.SeparatorWidth = 1;
            this.cbColor.Size = new System.Drawing.Size(96, 21);
            this.cbColor.TabIndex = 9;
            this.cbColor.TextChanged += new System.EventHandler(this.cbColor_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Style";
            // 
            // cbFontStyle
            // 
            this.cbFontStyle.AutoAdjustItemHeight = false;
            this.cbFontStyle.BorderColor = System.Drawing.Color.LightGray;
            this.cbFontStyle.ConvertEnterToTabForDialogs = false;
            this.cbFontStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFontStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFontStyle.Items.AddRange(new object[] {
            "Normal",
            "Italic"});
            this.cbFontStyle.Location = new System.Drawing.Point(72, 46);
            this.cbFontStyle.Name = "cbFontStyle";
            this.cbFontStyle.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbFontStyle.SeparatorMargin = 1;
            this.cbFontStyle.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbFontStyle.SeparatorWidth = 1;
            this.cbFontStyle.Size = new System.Drawing.Size(96, 21);
            this.cbFontStyle.TabIndex = 5;
            this.cbFontStyle.TextChanged += new System.EventHandler(this.cbFontStyle_TextChanged);
            // 
            // cbFontWeight
            // 
            this.cbFontWeight.AutoAdjustItemHeight = false;
            this.cbFontWeight.BorderColor = System.Drawing.Color.LightGray;
            this.cbFontWeight.ConvertEnterToTabForDialogs = false;
            this.cbFontWeight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFontWeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFontWeight.Items.AddRange(new object[] {
            "Lighter",
            "Normal",
            "Bold",
            "Bolder",
            "100",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900"});
            this.cbFontWeight.Location = new System.Drawing.Point(264, 46);
            this.cbFontWeight.Name = "cbFontWeight";
            this.cbFontWeight.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbFontWeight.SeparatorMargin = 1;
            this.cbFontWeight.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbFontWeight.SeparatorWidth = 1;
            this.cbFontWeight.Size = new System.Drawing.Size(104, 21);
            this.cbFontWeight.TabIndex = 7;
            this.cbFontWeight.TextChanged += new System.EventHandler(this.cbFontWeight_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(224, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 27;
            this.label10.Text = "Weight";
            // 
            // cbFontSize
            // 
            this.cbFontSize.AutoAdjustItemHeight = false;
            this.cbFontSize.BorderColor = System.Drawing.Color.LightGray;
            this.cbFontSize.ConvertEnterToTabForDialogs = false;
            this.cbFontSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFontSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFontSize.Items.AddRange(new object[] {
            "8pt",
            "9pt",
            "10pt",
            "11pt",
            "12pt",
            "14pt",
            "16pt",
            "18pt",
            "20pt",
            "22pt",
            "24pt",
            "26pt",
            "28pt",
            "36pt",
            "48pt",
            "72pt"});
            this.cbFontSize.Location = new System.Drawing.Point(264, 15);
            this.cbFontSize.Name = "cbFontSize";
            this.cbFontSize.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbFontSize.SeparatorMargin = 1;
            this.cbFontSize.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbFontSize.SeparatorWidth = 1;
            this.cbFontSize.Size = new System.Drawing.Size(104, 21);
            this.cbFontSize.TabIndex = 2;
            this.cbFontSize.TextChanged += new System.EventHandler(this.cbFontSize_TextChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(224, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "Size";
            // 
            // cbFontFamily
            // 
            this.cbFontFamily.AutoAdjustItemHeight = false;
            this.cbFontFamily.BorderColor = System.Drawing.Color.LightGray;
            this.cbFontFamily.ConvertEnterToTabForDialogs = false;
            this.cbFontFamily.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFontFamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFontFamily.Items.AddRange(new object[] {
            "Arial"});
            this.cbFontFamily.Location = new System.Drawing.Point(72, 15);
            this.cbFontFamily.Name = "cbFontFamily";
            this.cbFontFamily.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbFontFamily.SeparatorMargin = 1;
            this.cbFontFamily.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbFontFamily.SeparatorWidth = 1;
            this.cbFontFamily.Size = new System.Drawing.Size(96, 21);
            this.cbFontFamily.TabIndex = 0;
            this.cbFontFamily.TextChanged += new System.EventHandler(this.cbFontFamily_TextChanged);
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(8, 18);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(56, 23);
            this.lblValue.TabIndex = 30;
            this.lblValue.Text = "Value";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.bWeight);
            this.groupBox1.Controls.Add(this.bSize);
            this.groupBox1.Controls.Add(this.bColorEx);
            this.groupBox1.Controls.Add(this.bStyle);
            this.groupBox1.Controls.Add(this.bFamily);
            this.groupBox1.Controls.Add(this.lFont);
            this.groupBox1.Controls.Add(this.bFont);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbTextDecoration);
            this.groupBox1.Controls.Add(this.bColor);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cbColor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbFontStyle);
            this.groupBox1.Controls.Add(this.cbFontWeight);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbFontSize);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cbFontFamily);
            this.groupBox1.Location = new System.Drawing.Point(8, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 112);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.button2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.button2.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(376, 79);
            this.button2.Name = "button2";
            this.button2.OverriddenSize = null;
            this.button2.Size = new System.Drawing.Size(22, 21);
            this.button2.TabIndex = 13;
            this.button2.Tag = "decoration";
            this.button2.Text = "fx";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bWeight
            // 
            this.bWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bWeight.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bWeight.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bWeight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bWeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWeight.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bWeight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWeight.Location = new System.Drawing.Point(376, 46);
            this.bWeight.Name = "bWeight";
            this.bWeight.OverriddenSize = null;
            this.bWeight.Size = new System.Drawing.Size(22, 21);
            this.bWeight.TabIndex = 8;
            this.bWeight.Tag = "weight";
            this.bWeight.Text = "fx";
            this.bWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWeight.UseVisualStyleBackColor = true;
            this.bWeight.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bSize
            // 
            this.bSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSize.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSize.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSize.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSize.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSize.Location = new System.Drawing.Point(376, 15);
            this.bSize.Name = "bSize";
            this.bSize.OverriddenSize = null;
            this.bSize.Size = new System.Drawing.Size(22, 21);
            this.bSize.TabIndex = 3;
            this.bSize.Tag = "size";
            this.bSize.Text = "fx";
            this.bSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSize.UseVisualStyleBackColor = true;
            this.bSize.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bColorEx
            // 
            this.bColorEx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bColorEx.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bColorEx.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bColorEx.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bColorEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColorEx.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bColorEx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bColorEx.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColorEx.Location = new System.Drawing.Point(176, 79);
            this.bColorEx.Name = "bColorEx";
            this.bColorEx.OverriddenSize = null;
            this.bColorEx.Size = new System.Drawing.Size(22, 21);
            this.bColorEx.TabIndex = 10;
            this.bColorEx.Tag = "color";
            this.bColorEx.Text = "fx";
            this.bColorEx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColorEx.UseVisualStyleBackColor = true;
            this.bColorEx.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bStyle
            // 
            this.bStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bStyle.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStyle.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bStyle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bStyle.Location = new System.Drawing.Point(176, 46);
            this.bStyle.Name = "bStyle";
            this.bStyle.OverriddenSize = null;
            this.bStyle.Size = new System.Drawing.Size(22, 21);
            this.bStyle.TabIndex = 6;
            this.bStyle.Tag = "style";
            this.bStyle.Text = "fx";
            this.bStyle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bStyle.UseVisualStyleBackColor = true;
            this.bStyle.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bFamily
            // 
            this.bFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bFamily.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bFamily.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bFamily.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bFamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFamily.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bFamily.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bFamily.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFamily.Location = new System.Drawing.Point(176, 15);
            this.bFamily.Name = "bFamily";
            this.bFamily.OverriddenSize = null;
            this.bFamily.Size = new System.Drawing.Size(22, 21);
            this.bFamily.TabIndex = 1;
            this.bFamily.Tag = "family";
            this.bFamily.Text = "fx";
            this.bFamily.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFamily.UseVisualStyleBackColor = true;
            this.bFamily.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // cbValue
            // 
            this.cbValue.AutoAdjustItemHeight = false;
            this.cbValue.BorderColor = System.Drawing.Color.LightGray;
            this.cbValue.ConvertEnterToTabForDialogs = false;
            this.cbValue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValue.Location = new System.Drawing.Point(64, 19);
            this.cbValue.Name = "cbValue";
            this.cbValue.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValue.SeparatorMargin = 1;
            this.cbValue.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValue.SeparatorWidth = 1;
            this.cbValue.Size = new System.Drawing.Size(344, 21);
            this.cbValue.TabIndex = 0;
            this.cbValue.Text = "comboBox1";
            this.cbValue.TextChanged += new System.EventHandler(this.cbValue_TextChanged);
            // 
            // bValueExpr
            // 
            this.bValueExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bValueExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bValueExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bValueExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bValueExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValueExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bValueExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bValueExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bValueExpr.Location = new System.Drawing.Point(414, 19);
            this.bValueExpr.Name = "bValueExpr";
            this.bValueExpr.OverriddenSize = null;
            this.bValueExpr.Size = new System.Drawing.Size(22, 21);
            this.bValueExpr.TabIndex = 1;
            this.bValueExpr.Tag = "value";
            this.bValueExpr.Text = "fx";
            this.bValueExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bValueExpr.UseVisualStyleBackColor = true;
            this.bValueExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bAlignment
            // 
            this.bAlignment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bAlignment.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bAlignment.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bAlignment.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bAlignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAlignment.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAlignment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bAlignment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bAlignment.Location = new System.Drawing.Point(152, 170);
            this.bAlignment.Name = "bAlignment";
            this.bAlignment.OverriddenSize = null;
            this.bAlignment.Size = new System.Drawing.Size(22, 21);
            this.bAlignment.TabIndex = 4;
            this.bAlignment.Tag = "halign";
            this.bAlignment.Text = "fx";
            this.bAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bAlignment.UseVisualStyleBackColor = true;
            this.bAlignment.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bDirection
            // 
            this.bDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDirection.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDirection.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDirection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDirection.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDirection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDirection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDirection.Location = new System.Drawing.Point(152, 202);
            this.bDirection.Name = "bDirection";
            this.bDirection.OverriddenSize = null;
            this.bDirection.Size = new System.Drawing.Size(22, 21);
            this.bDirection.TabIndex = 8;
            this.bDirection.Tag = "direction";
            this.bDirection.Text = "fx";
            this.bDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDirection.UseVisualStyleBackColor = true;
            this.bDirection.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bVertical
            // 
            this.bVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bVertical.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bVertical.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bVertical.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bVertical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bVertical.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bVertical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bVertical.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bVertical.Location = new System.Drawing.Point(384, 170);
            this.bVertical.Name = "bVertical";
            this.bVertical.OverriddenSize = null;
            this.bVertical.Size = new System.Drawing.Size(22, 21);
            this.bVertical.TabIndex = 6;
            this.bVertical.Tag = "valign";
            this.bVertical.Text = "fx";
            this.bVertical.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bVertical.UseVisualStyleBackColor = true;
            this.bVertical.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bWrtMode
            // 
            this.bWrtMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bWrtMode.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bWrtMode.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bWrtMode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bWrtMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWrtMode.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWrtMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bWrtMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWrtMode.Location = new System.Drawing.Point(384, 202);
            this.bWrtMode.Name = "bWrtMode";
            this.bWrtMode.OverriddenSize = null;
            this.bWrtMode.Size = new System.Drawing.Size(22, 21);
            this.bWrtMode.TabIndex = 10;
            this.bWrtMode.Tag = "writing";
            this.bWrtMode.Text = "fx";
            this.bWrtMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWrtMode.UseVisualStyleBackColor = true;
            this.bWrtMode.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bFormat
            // 
            this.bFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bFormat.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bFormat.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bFormat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFormat.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bFormat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bFormat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFormat.Location = new System.Drawing.Point(384, 234);
            this.bFormat.Name = "bFormat";
            this.bFormat.OverriddenSize = null;
            this.bFormat.Size = new System.Drawing.Size(22, 21);
            this.bFormat.TabIndex = 12;
            this.bFormat.Tag = "format";
            this.bFormat.Text = "fx";
            this.bFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFormat.UseVisualStyleBackColor = true;
            this.bFormat.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // StyleTextCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.bFormat);
            this.Controls.Add(this.bWrtMode);
            this.Controls.Add(this.bVertical);
            this.Controls.Add(this.bDirection);
            this.Controls.Add(this.bAlignment);
            this.Controls.Add(this.bValueExpr);
            this.Controls.Add(this.cbValue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cbWritingMode);
            this.Controls.Add(this.cbDirection);
            this.Controls.Add(this.cbFormat);
            this.Controls.Add(this.cbHorzAlign);
            this.Controls.Add(this.cbVerticalAlign);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "StyleTextCtl";
            this.Size = new System.Drawing.Size(456, 280);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
      

		public bool IsValid()
		{
			if (fFontSize)
			{
				try 
				{
					if (!this.cbFontSize.Text.Trim().StartsWith("="))
						DesignerUtility.ValidateSize(this.cbFontSize.Text, false, false);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Invalid Font Size");
					return false;
				}

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

			fHorzAlign = fFormat = fDirection = fWritingMode = fTextDecoration =
				fColor = fVerticalAlign = fFontStyle = fFontWeight = fFontSize = fFontFamily =
				fValue = false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (cbValue.Enabled)
			{
				if (fValue)
					_Draw.SetElement(node, lblValue.Text, cbValue.Text);		// only adjust value when single item selected
			}

			XmlNode sNode = _Draw.GetNamedChildNode(node, "Style");

			if (fFontStyle)
				_Draw.SetElement(sNode, "FontStyle", cbFontStyle.Text);
			if (fFontFamily)
				_Draw.SetElement(sNode, "FontFamily", cbFontFamily.Text);
			if (fFontWeight)
				_Draw.SetElement(sNode, "FontWeight", cbFontWeight.Text);

			if (fFontSize)
			{
				float size=10;
				size = DesignXmlDraw.GetSize(cbFontSize.Text);
				if (size <= 0)
				{
					size = DesignXmlDraw.GetSize(cbFontSize.Text+"pt");	// Try assuming pt
					if (size <= 0)	// still no good
						size = 10;	// just set default value
				}
				string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}pt", size);

				_Draw.SetElement(sNode, "FontSize", rs);	// force to string
			}
			if (fTextDecoration)
				_Draw.SetElement(sNode, "TextDecoration", cbTextDecoration.Text);    
			if (fHorzAlign)
				_Draw.SetElement(sNode, "TextAlign", cbHorzAlign.Text);
			if (fVerticalAlign)
				_Draw.SetElement(sNode, "VerticalAlign", cbVerticalAlign.Text);
			if (fColor)
				_Draw.SetElement(sNode, "Color", cbColor.Text);
			if (fFormat)
			{
				if (cbFormat.Text.Length == 0)		// Don't put out a format if no format value
					_Draw.RemoveElement(sNode, "Format");
				else
					_Draw.SetElement(sNode, "Format", cbFormat.Text);
			}
			if (fDirection)
				_Draw.SetElement(sNode, "Direction", cbDirection.Text);
			if (fWritingMode)
				_Draw.SetElement(sNode, "WritingMode", cbWritingMode.Text);
			
			return;
		}

		private void bFont_Click(object sender, System.EventArgs e)
		{
			FontDialog fd = new FontDialog();
			fd.ShowColor = true;

			// STYLE
			System.Drawing.FontStyle fs = 0;
			if (cbFontStyle.Text == "Italic")
				fs |= System.Drawing.FontStyle.Italic;

			if (cbTextDecoration.Text == "Underline")
				fs |= FontStyle.Underline;
			else if (cbTextDecoration.Text == "LineThrough")
				fs |= FontStyle.Strikeout;

			// WEIGHT
			switch (cbFontWeight.Text)
			{
				case "Bold":
				case "Bolder":
				case "500":
				case "600":
				case "700":
				case "800":
				case "900":
					fs |= System.Drawing.FontStyle.Bold;
					break;
				default:
					break;
			}
			float size=10;
			size = DesignXmlDraw.GetSize(cbFontSize.Text);
			if (size <= 0)
			{
				size = DesignXmlDraw.GetSize(cbFontSize.Text+"pt");	// Try assuming pt
				if (size <= 0)	// still no good
					size = 10;	// just set default value
			}
			Font drawFont = new Font(cbFontFamily.Text, size, fs);	// si.FontSize already in points


			fd.Font = drawFont;
			fd.Color = 
				DesignerUtility.ColorFromHtml(cbColor.Text, System.Drawing.Color.Black);
            try
            {
                DialogResult dr = fd.ShowDialog();
                if (dr != DialogResult.OK)
                {
                    drawFont.Dispose();
                    return;
                }

                // Apply all the font info
                cbFontWeight.Text = fd.Font.Bold ? "Bold" : "Normal";
                cbFontStyle.Text = fd.Font.Italic ? "Italic" : "Normal";
                cbFontFamily.Text = fd.Font.FontFamily.Name;
                cbFontSize.Text = fd.Font.Size.ToString() + "pt";
                cbColor.Text = ColorTranslator.ToHtml(fd.Color);
                if (fd.Font.Underline)
                    this.cbTextDecoration.Text = "Underline";
                else if (fd.Font.Strikeout)
                    this.cbTextDecoration.Text = "LineThrough";
                else
                    this.cbTextDecoration.Text = "None";
                drawFont.Dispose();
            }
            finally
            {
                fd.Dispose();
            }
			return;
		}

		private void bColor_Click(object sender, System.EventArgs e)
		{
            using (ColorDialog cd = new ColorDialog())
            {
                cd.AnyColor = true;
                cd.FullOpen = true;

                cd.CustomColors = RdlDesigner.GetCustomColors();
                cd.Color =
                    DesignerUtility.ColorFromHtml(cbColor.Text, System.Drawing.Color.Black);

                if (cd.ShowDialog() != DialogResult.OK)
                    return;

                RdlDesigner.SetCustomColors(cd.CustomColors);
                if (sender == this.bColor)
                    cbColor.Text = ColorTranslator.ToHtml(cd.Color);
            }		
			return;
		}

		private void cbValue_TextChanged(object sender, System.EventArgs e)
		{
			fValue = true;
		}

		private void cbFontFamily_TextChanged(object sender, System.EventArgs e)
		{
			fFontFamily = true;
		}

		private void cbFontSize_TextChanged(object sender, System.EventArgs e)
		{
			fFontSize = true;
		}

		private void cbFontStyle_TextChanged(object sender, System.EventArgs e)
		{
			fFontStyle = true;
		}

		private void cbFontWeight_TextChanged(object sender, System.EventArgs e)
		{
			fFontWeight = true;
		}

		private void cbColor_TextChanged(object sender, System.EventArgs e)
		{
			fColor = true;
		}

		private void cbTextDecoration_TextChanged(object sender, System.EventArgs e)
		{
			fTextDecoration = true;
		}

		private void cbHorzAlign_TextChanged(object sender, System.EventArgs e)
		{
			fHorzAlign = true;
		}

		private void cbVerticalAlign_TextChanged(object sender, System.EventArgs e)
		{
			fVerticalAlign = true;
		}

		private void cbDirection_TextChanged(object sender, System.EventArgs e)
		{
			fDirection = true;
		}

		private void cbWritingMode_TextChanged(object sender, System.EventArgs e)
		{
			fWritingMode = true;
		}

		private void cbFormat_TextChanged(object sender, System.EventArgs e)
		{
			fFormat = true;
		}

		private void bExpr_Click(object sender, System.EventArgs e)
		{
			Button b = sender as Button;
			if (b == null)
				return;
			Control c = null;
			bool bColor=false;
			switch (b.Tag as string)
			{
				case "value":
					c = cbValue;
					break;
				case "family":
					c = cbFontFamily;
					break;
				case "style":
					c = cbFontStyle;
					break;
				case "color":
					c = cbColor;
					bColor = true;
					break;
				case "size":
					c = cbFontSize;
					break;
				case "weight":
					c = cbFontWeight;
					break;
				case "decoration":
					c = cbTextDecoration;
					break;
				case "halign":
					c = cbHorzAlign;
					break;
				case "valign":
					c = cbVerticalAlign;
					break;
				case "direction":
					c = cbDirection;
					break;
				case "writing":
					c = cbWritingMode;
					break;
				case "format":
					c = cbFormat;
					break;
			}

			if (c == null)
				return;

			XmlNode sNode = _ReportItems[0];

            using (DialogExprEditor ee = new DialogExprEditor(_Draw, c.Text, sNode, bColor))
            {
                DialogResult dr = ee.ShowDialog();
                if (dr == DialogResult.OK)
                    c.Text = ee.Expression;
            }
            return;
		}

	}
}
