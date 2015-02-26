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
	internal class StyleBorderCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		// flags for controlling whether syntax changed for a particular property
		private bool fStyleDefault, fStyleLeft, fStyleRight, fStyleTop, fStyleBottom;
		private bool fColorDefault, fColorLeft, fColorRight, fColorTop, fColorBottom;
		private bool fWidthDefault, fWidthLeft, fWidthRight, fWidthTop, fWidthBottom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private Oranikle.Studio.Controls.StyledComboBox cbStyleLeft;
		private System.Windows.Forms.Label label8;
		private Oranikle.Studio.Controls.StyledComboBox cbStyleBottom;
		private Oranikle.Studio.Controls.StyledComboBox cbStyleTop;
		private Oranikle.Studio.Controls.StyledComboBox cbStyleRight;
		private Oranikle.Studio.Controls.StyledButton bColorLeft;
		private Oranikle.Studio.Controls.StyledComboBox cbColorLeft;
		private Oranikle.Studio.Controls.StyledButton bColorRight;
		private Oranikle.Studio.Controls.StyledComboBox cbColorRight;
		private Oranikle.Studio.Controls.StyledButton bColorTop;
		private Oranikle.Studio.Controls.StyledComboBox cbColorTop;
		private Oranikle.Studio.Controls.StyledButton bColorBottom;
		private Oranikle.Studio.Controls.StyledComboBox cbColorBottom;
		private Oranikle.Studio.Controls.CustomTextControl tbWidthLeft;
		private Oranikle.Studio.Controls.CustomTextControl tbWidthRight;
		private Oranikle.Studio.Controls.CustomTextControl tbWidthTop;
		private Oranikle.Studio.Controls.CustomTextControl tbWidthBottom;
		private Oranikle.Studio.Controls.CustomTextControl tbWidthDefault;
		private Oranikle.Studio.Controls.StyledButton bColorDefault;
		private Oranikle.Studio.Controls.StyledComboBox cbColorDefault;
		private Oranikle.Studio.Controls.StyledComboBox cbStyleDefault;
		private System.Windows.Forms.Label lLeft;
		private System.Windows.Forms.Label lBottom;
		private System.Windows.Forms.Label lTop;
		private System.Windows.Forms.Label lRight;
		private Oranikle.Studio.Controls.StyledButton bSD;
		private Oranikle.Studio.Controls.StyledButton bSL;
		private Oranikle.Studio.Controls.StyledButton bSR;
		private Oranikle.Studio.Controls.StyledButton bST;
		private Oranikle.Studio.Controls.StyledButton bSB;
		private Oranikle.Studio.Controls.StyledButton bCD;
		private Oranikle.Studio.Controls.StyledButton bCT;
		private Oranikle.Studio.Controls.StyledButton bCB;
		private Oranikle.Studio.Controls.StyledButton bWB;
		private Oranikle.Studio.Controls.StyledButton bWT;
		private Oranikle.Studio.Controls.StyledButton bWR;
		private Oranikle.Studio.Controls.StyledButton bCR;
		private Oranikle.Studio.Controls.StyledButton bWL;
		private Oranikle.Studio.Controls.StyledButton bWD;
		private Oranikle.Studio.Controls.StyledButton bCL;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private string[] _names;

        internal StyleBorderCtl(DesignXmlDraw dxDraw, string[] names, List<XmlNode> reportItems)
		{
			_ReportItems = reportItems;
			_Draw = dxDraw;
            _names = names;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitBorders(reportItems[0]);			
		}

		private void InitBorders(XmlNode node)
		{
            cbColorDefault.Items.AddRange(StaticLists.ColorList);
            cbColorLeft.Items.AddRange(StaticLists.ColorList);
            cbColorRight.Items.AddRange(StaticLists.ColorList);
            cbColorTop.Items.AddRange(StaticLists.ColorList);
            cbColorBottom.Items.AddRange(StaticLists.ColorList);

            if (_names != null)
            {
                node = _Draw.FindCreateNextInHierarchy(node, _names);
            }

            XmlNode sNode = _Draw.GetCreateNamedChildNode(node, "Style");

			// Handle BorderStyle
			XmlNode bsNode = _Draw.SetElement(sNode, "BorderStyle", null);
			cbStyleDefault.Text = _Draw.GetElementValue(bsNode, "Default", "None");
			cbStyleLeft.Text = _Draw.GetElementValue(bsNode, "Left", cbStyleDefault.Text);
			cbStyleRight.Text = _Draw.GetElementValue(bsNode, "Right", cbStyleDefault.Text);
			cbStyleTop.Text = _Draw.GetElementValue(bsNode, "Top", cbStyleDefault.Text);
			cbStyleBottom.Text = _Draw.GetElementValue(bsNode, "Bottom", cbStyleDefault.Text);

			// Handle BorderColor
			XmlNode bcNode = _Draw.SetElement(sNode, "BorderColor", null);
			cbColorDefault.Text = _Draw.GetElementValue(bcNode, "Default", "Black");
			cbColorLeft.Text = _Draw.GetElementValue(bcNode, "Left", cbColorDefault.Text);
			cbColorRight.Text = _Draw.GetElementValue(bcNode, "Right", cbColorDefault.Text);
			cbColorTop.Text = _Draw.GetElementValue(bcNode, "Top", cbColorDefault.Text);
			cbColorBottom.Text = _Draw.GetElementValue(bcNode, "Bottom", cbColorDefault.Text);

			// Handle BorderWidth
			XmlNode bwNode = _Draw.SetElement(sNode, "BorderWidth", null);
			tbWidthDefault.Text = _Draw.GetElementValue(bwNode, "Default", "1pt");
			tbWidthLeft.Text = _Draw.GetElementValue(bwNode, "Left", tbWidthDefault.Text);
			tbWidthRight.Text = _Draw.GetElementValue(bwNode, "Right", tbWidthDefault.Text);
			tbWidthTop.Text = _Draw.GetElementValue(bwNode, "Top", tbWidthDefault.Text);
			tbWidthBottom.Text = _Draw.GetElementValue(bwNode, "Bottom", tbWidthDefault.Text);
		
			if (node.Name == "Line")
			{
				cbColorLeft.Visible =
					cbColorRight.Visible =
					cbColorTop.Visible =
					cbColorBottom.Visible =
					bColorLeft.Visible =
					bColorRight.Visible =
					bColorTop.Visible =
					bColorBottom.Visible =
					cbStyleLeft.Visible =
					cbStyleRight.Visible =
					cbStyleTop.Visible =
					cbStyleBottom.Visible =
					lLeft.Visible =
					lRight.Visible =
					lTop.Visible =
					lBottom.Visible =
					tbWidthLeft.Visible =
					tbWidthRight.Visible =
					tbWidthTop.Visible =
					tbWidthBottom.Visible = 
					bCR.Visible = bCL.Visible = bCT.Visible = bCB.Visible =
					bSR.Visible = bSL.Visible = bST.Visible = bSB.Visible =
					bWR.Visible = bWL.Visible = bWT.Visible = bWB.Visible =
					false;
			}
			fStyleDefault = fStyleLeft = fStyleRight = fStyleTop = fStyleBottom =
				fColorDefault = fColorLeft = fColorRight = fColorTop = fColorBottom =
				fWidthDefault = fWidthLeft = fWidthRight = fWidthTop = fWidthBottom= false;
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
            this.lLeft = new System.Windows.Forms.Label();
            this.lBottom = new System.Windows.Forms.Label();
            this.lTop = new System.Windows.Forms.Label();
            this.lRight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbStyleLeft = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbStyleBottom = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbStyleTop = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbStyleRight = new Oranikle.Studio.Controls.StyledComboBox();
            this.bColorLeft = new Oranikle.Studio.Controls.StyledButton();
            this.cbColorLeft = new Oranikle.Studio.Controls.StyledComboBox();
            this.bColorRight = new Oranikle.Studio.Controls.StyledButton();
            this.cbColorRight = new Oranikle.Studio.Controls.StyledComboBox();
            this.bColorTop = new Oranikle.Studio.Controls.StyledButton();
            this.cbColorTop = new Oranikle.Studio.Controls.StyledComboBox();
            this.bColorBottom = new Oranikle.Studio.Controls.StyledButton();
            this.cbColorBottom = new Oranikle.Studio.Controls.StyledComboBox();
            this.tbWidthLeft = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbWidthRight = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbWidthTop = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbWidthBottom = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbWidthDefault = new Oranikle.Studio.Controls.CustomTextControl();
            this.bColorDefault = new Oranikle.Studio.Controls.StyledButton();
            this.cbColorDefault = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbStyleDefault = new Oranikle.Studio.Controls.StyledComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bSD = new Oranikle.Studio.Controls.StyledButton();
            this.bSL = new Oranikle.Studio.Controls.StyledButton();
            this.bSR = new Oranikle.Studio.Controls.StyledButton();
            this.bST = new Oranikle.Studio.Controls.StyledButton();
            this.bSB = new Oranikle.Studio.Controls.StyledButton();
            this.bCD = new Oranikle.Studio.Controls.StyledButton();
            this.bCT = new Oranikle.Studio.Controls.StyledButton();
            this.bCB = new Oranikle.Studio.Controls.StyledButton();
            this.bWB = new Oranikle.Studio.Controls.StyledButton();
            this.bWT = new Oranikle.Studio.Controls.StyledButton();
            this.bWR = new Oranikle.Studio.Controls.StyledButton();
            this.bCR = new Oranikle.Studio.Controls.StyledButton();
            this.bWL = new Oranikle.Studio.Controls.StyledButton();
            this.bWD = new Oranikle.Studio.Controls.StyledButton();
            this.bCL = new Oranikle.Studio.Controls.StyledButton();
            this.SuspendLayout();
            // 
            // lLeft
            // 
            this.lLeft.Location = new System.Drawing.Point(12, 77);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(42, 16);
            this.lLeft.TabIndex = 0;
            this.lLeft.Text = "Left";
            this.lLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lBottom
            // 
            this.lBottom.Location = new System.Drawing.Point(12, 172);
            this.lBottom.Name = "lBottom";
            this.lBottom.Size = new System.Drawing.Size(42, 16);
            this.lBottom.TabIndex = 2;
            this.lBottom.Text = "Bottom";
            this.lBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lTop
            // 
            this.lTop.Location = new System.Drawing.Point(12, 140);
            this.lTop.Name = "lTop";
            this.lTop.Size = new System.Drawing.Size(42, 16);
            this.lTop.TabIndex = 3;
            this.lTop.Text = "Top";
            this.lTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lRight
            // 
            this.lRight.Location = new System.Drawing.Point(12, 110);
            this.lRight.Name = "lRight";
            this.lRight.Size = new System.Drawing.Size(42, 16);
            this.lRight.TabIndex = 4;
            this.lRight.Text = "Right";
            this.lRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(60, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Style";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(336, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Width";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(180, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Color";
            // 
            // cbStyleLeft
            // 
            this.cbStyleLeft.AutoAdjustItemHeight = false;
            this.cbStyleLeft.BorderColor = System.Drawing.Color.LightGray;
            this.cbStyleLeft.ConvertEnterToTabForDialogs = false;
            this.cbStyleLeft.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbStyleLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStyleLeft.Items.AddRange(new object[] {
            "None",
            "Dotted",
            "Dashed",
            "Solid",
            "Double",
            "Groove",
            "Ridge",
            "Inset",
            "WindowInset",
            "Outset"});
            this.cbStyleLeft.Location = new System.Drawing.Point(56, 75);
            this.cbStyleLeft.Name = "cbStyleLeft";
            this.cbStyleLeft.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbStyleLeft.SeparatorMargin = 1;
            this.cbStyleLeft.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbStyleLeft.SeparatorWidth = 1;
            this.cbStyleLeft.Size = new System.Drawing.Size(88, 21);
            this.cbStyleLeft.TabIndex = 7;
            this.cbStyleLeft.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
            // 
            // cbStyleBottom
            // 
            this.cbStyleBottom.AutoAdjustItemHeight = false;
            this.cbStyleBottom.BorderColor = System.Drawing.Color.LightGray;
            this.cbStyleBottom.ConvertEnterToTabForDialogs = false;
            this.cbStyleBottom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbStyleBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStyleBottom.Items.AddRange(new object[] {
            "None",
            "Dotted",
            "Dashed",
            "Solid",
            "Double",
            "Groove",
            "Ridge",
            "Inset",
            "WindowInset",
            "Outset"});
            this.cbStyleBottom.Location = new System.Drawing.Point(56, 170);
            this.cbStyleBottom.Name = "cbStyleBottom";
            this.cbStyleBottom.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbStyleBottom.SeparatorMargin = 1;
            this.cbStyleBottom.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbStyleBottom.SeparatorWidth = 1;
            this.cbStyleBottom.Size = new System.Drawing.Size(88, 21);
            this.cbStyleBottom.TabIndex = 28;
            this.cbStyleBottom.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
            // 
            // cbStyleTop
            // 
            this.cbStyleTop.AutoAdjustItemHeight = false;
            this.cbStyleTop.BorderColor = System.Drawing.Color.LightGray;
            this.cbStyleTop.ConvertEnterToTabForDialogs = false;
            this.cbStyleTop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbStyleTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStyleTop.Items.AddRange(new object[] {
            "None",
            "Dotted",
            "Dashed",
            "Solid",
            "Double",
            "Groove",
            "Ridge",
            "Inset",
            "WindowInset",
            "Outset"});
            this.cbStyleTop.Location = new System.Drawing.Point(56, 138);
            this.cbStyleTop.Name = "cbStyleTop";
            this.cbStyleTop.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbStyleTop.SeparatorMargin = 1;
            this.cbStyleTop.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbStyleTop.SeparatorWidth = 1;
            this.cbStyleTop.Size = new System.Drawing.Size(88, 21);
            this.cbStyleTop.TabIndex = 21;
            this.cbStyleTop.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
            // 
            // cbStyleRight
            // 
            this.cbStyleRight.AutoAdjustItemHeight = false;
            this.cbStyleRight.BorderColor = System.Drawing.Color.LightGray;
            this.cbStyleRight.ConvertEnterToTabForDialogs = false;
            this.cbStyleRight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbStyleRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStyleRight.Items.AddRange(new object[] {
            "None",
            "Dotted",
            "Dashed",
            "Solid",
            "Double",
            "Groove",
            "Ridge",
            "Inset",
            "WindowInset",
            "Outset"});
            this.cbStyleRight.Location = new System.Drawing.Point(56, 108);
            this.cbStyleRight.Name = "cbStyleRight";
            this.cbStyleRight.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbStyleRight.SeparatorMargin = 1;
            this.cbStyleRight.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbStyleRight.SeparatorWidth = 1;
            this.cbStyleRight.Size = new System.Drawing.Size(88, 21);
            this.cbStyleRight.TabIndex = 14;
            this.cbStyleRight.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
            // 
            // bColorLeft
            // 
            this.bColorLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bColorLeft.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bColorLeft.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bColorLeft.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bColorLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColorLeft.Font = new System.Drawing.Font("Arial", 9F);
            this.bColorLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bColorLeft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColorLeft.Location = new System.Drawing.Point(306, 75);
            this.bColorLeft.Name = "bColorLeft";
            this.bColorLeft.OverriddenSize = null;
            this.bColorLeft.Size = new System.Drawing.Size(22, 21);
            this.bColorLeft.TabIndex = 11;
            this.bColorLeft.Text = "...";
            this.bColorLeft.UseVisualStyleBackColor = true;
            this.bColorLeft.Click += new System.EventHandler(this.bColor_Click);
            // 
            // cbColorLeft
            // 
            this.cbColorLeft.AutoAdjustItemHeight = false;
            this.cbColorLeft.BorderColor = System.Drawing.Color.LightGray;
            this.cbColorLeft.ConvertEnterToTabForDialogs = false;
            this.cbColorLeft.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbColorLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbColorLeft.Location = new System.Drawing.Point(176, 75);
            this.cbColorLeft.Name = "cbColorLeft";
            this.cbColorLeft.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbColorLeft.SeparatorMargin = 1;
            this.cbColorLeft.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbColorLeft.SeparatorWidth = 1;
            this.cbColorLeft.Size = new System.Drawing.Size(96, 21);
            this.cbColorLeft.TabIndex = 9;
            this.cbColorLeft.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // bColorRight
            // 
            this.bColorRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bColorRight.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bColorRight.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bColorRight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bColorRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColorRight.Font = new System.Drawing.Font("Arial", 9F);
            this.bColorRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bColorRight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColorRight.Location = new System.Drawing.Point(306, 108);
            this.bColorRight.Name = "bColorRight";
            this.bColorRight.OverriddenSize = null;
            this.bColorRight.Size = new System.Drawing.Size(22, 21);
            this.bColorRight.TabIndex = 18;
            this.bColorRight.Text = "...";
            this.bColorRight.UseVisualStyleBackColor = true;
            this.bColorRight.Click += new System.EventHandler(this.bColor_Click);
            // 
            // cbColorRight
            // 
            this.cbColorRight.AutoAdjustItemHeight = false;
            this.cbColorRight.BorderColor = System.Drawing.Color.LightGray;
            this.cbColorRight.ConvertEnterToTabForDialogs = false;
            this.cbColorRight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbColorRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbColorRight.Location = new System.Drawing.Point(176, 108);
            this.cbColorRight.Name = "cbColorRight";
            this.cbColorRight.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbColorRight.SeparatorMargin = 1;
            this.cbColorRight.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbColorRight.SeparatorWidth = 1;
            this.cbColorRight.Size = new System.Drawing.Size(96, 21);
            this.cbColorRight.TabIndex = 16;
            this.cbColorRight.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // bColorTop
            // 
            this.bColorTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bColorTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bColorTop.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bColorTop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bColorTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColorTop.Font = new System.Drawing.Font("Arial", 9F);
            this.bColorTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bColorTop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColorTop.Location = new System.Drawing.Point(306, 138);
            this.bColorTop.Name = "bColorTop";
            this.bColorTop.OverriddenSize = null;
            this.bColorTop.Size = new System.Drawing.Size(22, 21);
            this.bColorTop.TabIndex = 25;
            this.bColorTop.Text = "...";
            this.bColorTop.UseVisualStyleBackColor = true;
            this.bColorTop.Click += new System.EventHandler(this.bColor_Click);
            // 
            // cbColorTop
            // 
            this.cbColorTop.AutoAdjustItemHeight = false;
            this.cbColorTop.BorderColor = System.Drawing.Color.LightGray;
            this.cbColorTop.ConvertEnterToTabForDialogs = false;
            this.cbColorTop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbColorTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbColorTop.Location = new System.Drawing.Point(176, 138);
            this.cbColorTop.Name = "cbColorTop";
            this.cbColorTop.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbColorTop.SeparatorMargin = 1;
            this.cbColorTop.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbColorTop.SeparatorWidth = 1;
            this.cbColorTop.Size = new System.Drawing.Size(96, 21);
            this.cbColorTop.TabIndex = 23;
            this.cbColorTop.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // bColorBottom
            // 
            this.bColorBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bColorBottom.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bColorBottom.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bColorBottom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bColorBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColorBottom.Font = new System.Drawing.Font("Arial", 9F);
            this.bColorBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bColorBottom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColorBottom.Location = new System.Drawing.Point(306, 170);
            this.bColorBottom.Name = "bColorBottom";
            this.bColorBottom.OverriddenSize = null;
            this.bColorBottom.Size = new System.Drawing.Size(22, 21);
            this.bColorBottom.TabIndex = 32;
            this.bColorBottom.Text = "...";
            this.bColorBottom.UseVisualStyleBackColor = true;
            this.bColorBottom.Click += new System.EventHandler(this.bColor_Click);
            // 
            // cbColorBottom
            // 
            this.cbColorBottom.AutoAdjustItemHeight = false;
            this.cbColorBottom.BorderColor = System.Drawing.Color.LightGray;
            this.cbColorBottom.ConvertEnterToTabForDialogs = false;
            this.cbColorBottom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbColorBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbColorBottom.Location = new System.Drawing.Point(176, 170);
            this.cbColorBottom.Name = "cbColorBottom";
            this.cbColorBottom.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbColorBottom.SeparatorMargin = 1;
            this.cbColorBottom.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbColorBottom.SeparatorWidth = 1;
            this.cbColorBottom.Size = new System.Drawing.Size(96, 21);
            this.cbColorBottom.TabIndex = 30;
            this.cbColorBottom.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // tbWidthLeft
            // 
            this.tbWidthLeft.AddX = 0;
            this.tbWidthLeft.AddY = 0;
            this.tbWidthLeft.AllowSpace = false;
            this.tbWidthLeft.BorderColor = System.Drawing.Color.LightGray;
            this.tbWidthLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWidthLeft.ChangeVisibility = false;
            this.tbWidthLeft.ChildControl = null;
            this.tbWidthLeft.ConvertEnterToTab = true;
            this.tbWidthLeft.ConvertEnterToTabForDialogs = false;
            this.tbWidthLeft.Decimals = 0;
            this.tbWidthLeft.DisplayList = new object[0];
            this.tbWidthLeft.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbWidthLeft.Location = new System.Drawing.Point(334, 75);
            this.tbWidthLeft.Name = "tbWidthLeft";
            this.tbWidthLeft.OnDropDownCloseFocus = true;
            this.tbWidthLeft.SelectType = 0;
            this.tbWidthLeft.Size = new System.Drawing.Size(64, 20);
            this.tbWidthLeft.TabIndex = 12;
            this.tbWidthLeft.UseValueForChildsVisibilty = false;
            this.tbWidthLeft.Value = true;
            this.tbWidthLeft.TextChanged += new System.EventHandler(this.tbWidth_Changed);
            // 
            // tbWidthRight
            // 
            this.tbWidthRight.AddX = 0;
            this.tbWidthRight.AddY = 0;
            this.tbWidthRight.AllowSpace = false;
            this.tbWidthRight.BorderColor = System.Drawing.Color.LightGray;
            this.tbWidthRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWidthRight.ChangeVisibility = false;
            this.tbWidthRight.ChildControl = null;
            this.tbWidthRight.ConvertEnterToTab = true;
            this.tbWidthRight.ConvertEnterToTabForDialogs = false;
            this.tbWidthRight.Decimals = 0;
            this.tbWidthRight.DisplayList = new object[0];
            this.tbWidthRight.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbWidthRight.Location = new System.Drawing.Point(334, 108);
            this.tbWidthRight.Name = "tbWidthRight";
            this.tbWidthRight.OnDropDownCloseFocus = true;
            this.tbWidthRight.SelectType = 0;
            this.tbWidthRight.Size = new System.Drawing.Size(64, 20);
            this.tbWidthRight.TabIndex = 19;
            this.tbWidthRight.UseValueForChildsVisibilty = false;
            this.tbWidthRight.Value = true;
            this.tbWidthRight.TextChanged += new System.EventHandler(this.tbWidth_Changed);
            // 
            // tbWidthTop
            // 
            this.tbWidthTop.AddX = 0;
            this.tbWidthTop.AddY = 0;
            this.tbWidthTop.AllowSpace = false;
            this.tbWidthTop.BorderColor = System.Drawing.Color.LightGray;
            this.tbWidthTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWidthTop.ChangeVisibility = false;
            this.tbWidthTop.ChildControl = null;
            this.tbWidthTop.ConvertEnterToTab = true;
            this.tbWidthTop.ConvertEnterToTabForDialogs = false;
            this.tbWidthTop.Decimals = 0;
            this.tbWidthTop.DisplayList = new object[0];
            this.tbWidthTop.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbWidthTop.Location = new System.Drawing.Point(334, 138);
            this.tbWidthTop.Name = "tbWidthTop";
            this.tbWidthTop.OnDropDownCloseFocus = true;
            this.tbWidthTop.SelectType = 0;
            this.tbWidthTop.Size = new System.Drawing.Size(64, 20);
            this.tbWidthTop.TabIndex = 26;
            this.tbWidthTop.UseValueForChildsVisibilty = false;
            this.tbWidthTop.Value = true;
            this.tbWidthTop.TextChanged += new System.EventHandler(this.tbWidth_Changed);
            // 
            // tbWidthBottom
            // 
            this.tbWidthBottom.AddX = 0;
            this.tbWidthBottom.AddY = 0;
            this.tbWidthBottom.AllowSpace = false;
            this.tbWidthBottom.BorderColor = System.Drawing.Color.LightGray;
            this.tbWidthBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWidthBottom.ChangeVisibility = false;
            this.tbWidthBottom.ChildControl = null;
            this.tbWidthBottom.ConvertEnterToTab = true;
            this.tbWidthBottom.ConvertEnterToTabForDialogs = false;
            this.tbWidthBottom.Decimals = 0;
            this.tbWidthBottom.DisplayList = new object[0];
            this.tbWidthBottom.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbWidthBottom.Location = new System.Drawing.Point(334, 170);
            this.tbWidthBottom.Name = "tbWidthBottom";
            this.tbWidthBottom.OnDropDownCloseFocus = true;
            this.tbWidthBottom.SelectType = 0;
            this.tbWidthBottom.Size = new System.Drawing.Size(64, 20);
            this.tbWidthBottom.TabIndex = 33;
            this.tbWidthBottom.UseValueForChildsVisibilty = false;
            this.tbWidthBottom.Value = true;
            this.tbWidthBottom.TextChanged += new System.EventHandler(this.tbWidth_Changed);
            // 
            // tbWidthDefault
            // 
            this.tbWidthDefault.AddX = 0;
            this.tbWidthDefault.AddY = 0;
            this.tbWidthDefault.AllowSpace = false;
            this.tbWidthDefault.BorderColor = System.Drawing.Color.LightGray;
            this.tbWidthDefault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWidthDefault.ChangeVisibility = false;
            this.tbWidthDefault.ChildControl = null;
            this.tbWidthDefault.ConvertEnterToTab = true;
            this.tbWidthDefault.ConvertEnterToTabForDialogs = false;
            this.tbWidthDefault.Decimals = 0;
            this.tbWidthDefault.DisplayList = new object[0];
            this.tbWidthDefault.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbWidthDefault.Location = new System.Drawing.Point(334, 42);
            this.tbWidthDefault.Name = "tbWidthDefault";
            this.tbWidthDefault.OnDropDownCloseFocus = true;
            this.tbWidthDefault.SelectType = 0;
            this.tbWidthDefault.Size = new System.Drawing.Size(64, 20);
            this.tbWidthDefault.TabIndex = 5;
            this.tbWidthDefault.UseValueForChildsVisibilty = false;
            this.tbWidthDefault.Value = true;
            this.tbWidthDefault.TextChanged += new System.EventHandler(this.tbWidthDefault_TextChanged);
            // 
            // bColorDefault
            // 
            this.bColorDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bColorDefault.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bColorDefault.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bColorDefault.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bColorDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bColorDefault.Font = new System.Drawing.Font("Arial", 9F);
            this.bColorDefault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bColorDefault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bColorDefault.Location = new System.Drawing.Point(306, 42);
            this.bColorDefault.Name = "bColorDefault";
            this.bColorDefault.OverriddenSize = null;
            this.bColorDefault.Size = new System.Drawing.Size(22, 21);
            this.bColorDefault.TabIndex = 4;
            this.bColorDefault.Text = "...";
            this.bColorDefault.UseVisualStyleBackColor = true;
            this.bColorDefault.Click += new System.EventHandler(this.bColor_Click);
            // 
            // cbColorDefault
            // 
            this.cbColorDefault.AutoAdjustItemHeight = false;
            this.cbColorDefault.BorderColor = System.Drawing.Color.LightGray;
            this.cbColorDefault.ConvertEnterToTabForDialogs = false;
            this.cbColorDefault.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbColorDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbColorDefault.Location = new System.Drawing.Point(176, 42);
            this.cbColorDefault.Name = "cbColorDefault";
            this.cbColorDefault.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbColorDefault.SeparatorMargin = 1;
            this.cbColorDefault.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbColorDefault.SeparatorWidth = 1;
            this.cbColorDefault.Size = new System.Drawing.Size(96, 21);
            this.cbColorDefault.TabIndex = 2;
            this.cbColorDefault.SelectedIndexChanged += new System.EventHandler(this.cbColorDefault_SelectedIndexChanged);
            // 
            // cbStyleDefault
            // 
            this.cbStyleDefault.AutoAdjustItemHeight = false;
            this.cbStyleDefault.BorderColor = System.Drawing.Color.LightGray;
            this.cbStyleDefault.ConvertEnterToTabForDialogs = false;
            this.cbStyleDefault.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbStyleDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStyleDefault.Items.AddRange(new object[] {
            "None",
            "Dotted",
            "Dashed",
            "Solid",
            "Double",
            "Groove",
            "Ridge",
            "Inset",
            "WindowInset",
            "Outset"});
            this.cbStyleDefault.Location = new System.Drawing.Point(56, 42);
            this.cbStyleDefault.Name = "cbStyleDefault";
            this.cbStyleDefault.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbStyleDefault.SeparatorMargin = 1;
            this.cbStyleDefault.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbStyleDefault.SeparatorWidth = 1;
            this.cbStyleDefault.Size = new System.Drawing.Size(88, 21);
            this.cbStyleDefault.TabIndex = 0;
            this.cbStyleDefault.SelectedIndexChanged += new System.EventHandler(this.cbStyleDefault_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(12, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 16);
            this.label8.TabIndex = 36;
            this.label8.Text = "Default";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bSD
            // 
            this.bSD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSD.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSD.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSD.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSD.Location = new System.Drawing.Point(148, 42);
            this.bSD.Name = "bSD";
            this.bSD.OverriddenSize = null;
            this.bSD.Size = new System.Drawing.Size(22, 21);
            this.bSD.TabIndex = 1;
            this.bSD.Tag = "sd";
            this.bSD.Text = "fx";
            this.bSD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSD.UseVisualStyleBackColor = true;
            this.bSD.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bSL
            // 
            this.bSL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSL.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSL.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSL.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSL.Location = new System.Drawing.Point(148, 75);
            this.bSL.Name = "bSL";
            this.bSL.OverriddenSize = null;
            this.bSL.Size = new System.Drawing.Size(22, 21);
            this.bSL.TabIndex = 8;
            this.bSL.Tag = "sl";
            this.bSL.Text = "fx";
            this.bSL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSL.UseVisualStyleBackColor = true;
            this.bSL.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bSR
            // 
            this.bSR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSR.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSR.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSR.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSR.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSR.Location = new System.Drawing.Point(148, 108);
            this.bSR.Name = "bSR";
            this.bSR.OverriddenSize = null;
            this.bSR.Size = new System.Drawing.Size(22, 21);
            this.bSR.TabIndex = 15;
            this.bSR.Tag = "sr";
            this.bSR.Text = "fx";
            this.bSR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSR.UseVisualStyleBackColor = true;
            this.bSR.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bST
            // 
            this.bST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bST.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bST.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bST.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bST.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bST.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bST.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bST.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bST.Location = new System.Drawing.Point(148, 138);
            this.bST.Name = "bST";
            this.bST.OverriddenSize = null;
            this.bST.Size = new System.Drawing.Size(22, 21);
            this.bST.TabIndex = 22;
            this.bST.Tag = "st";
            this.bST.Text = "fx";
            this.bST.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bST.UseVisualStyleBackColor = true;
            this.bST.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bSB
            // 
            this.bSB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSB.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSB.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSB.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSB.Location = new System.Drawing.Point(148, 170);
            this.bSB.Name = "bSB";
            this.bSB.OverriddenSize = null;
            this.bSB.Size = new System.Drawing.Size(22, 21);
            this.bSB.TabIndex = 29;
            this.bSB.Tag = "sb";
            this.bSB.Text = "fx";
            this.bSB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSB.UseVisualStyleBackColor = true;
            this.bSB.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bCD
            // 
            this.bCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCD.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCD.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCD.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCD.Location = new System.Drawing.Point(276, 42);
            this.bCD.Name = "bCD";
            this.bCD.OverriddenSize = null;
            this.bCD.Size = new System.Drawing.Size(22, 21);
            this.bCD.TabIndex = 3;
            this.bCD.Tag = "cd";
            this.bCD.Text = "fx";
            this.bCD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCD.UseVisualStyleBackColor = true;
            this.bCD.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bCT
            // 
            this.bCT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCT.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCT.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCT.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCT.Location = new System.Drawing.Point(276, 138);
            this.bCT.Name = "bCT";
            this.bCT.OverriddenSize = null;
            this.bCT.Size = new System.Drawing.Size(22, 21);
            this.bCT.TabIndex = 24;
            this.bCT.Tag = "ct";
            this.bCT.Text = "fx";
            this.bCT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCT.UseVisualStyleBackColor = true;
            this.bCT.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bCB
            // 
            this.bCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCB.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCB.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCB.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCB.Location = new System.Drawing.Point(276, 170);
            this.bCB.Name = "bCB";
            this.bCB.OverriddenSize = null;
            this.bCB.Size = new System.Drawing.Size(22, 21);
            this.bCB.TabIndex = 31;
            this.bCB.Tag = "cb";
            this.bCB.Text = "fx";
            this.bCB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCB.UseVisualStyleBackColor = true;
            this.bCB.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bWB
            // 
            this.bWB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bWB.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bWB.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bWB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bWB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWB.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bWB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWB.Location = new System.Drawing.Point(403, 170);
            this.bWB.Name = "bWB";
            this.bWB.OverriddenSize = null;
            this.bWB.Size = new System.Drawing.Size(22, 21);
            this.bWB.TabIndex = 34;
            this.bWB.Tag = "wb";
            this.bWB.Text = "fx";
            this.bWB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWB.UseVisualStyleBackColor = true;
            this.bWB.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bWT
            // 
            this.bWT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bWT.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bWT.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bWT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bWT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWT.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bWT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWT.Location = new System.Drawing.Point(403, 138);
            this.bWT.Name = "bWT";
            this.bWT.OverriddenSize = null;
            this.bWT.Size = new System.Drawing.Size(22, 21);
            this.bWT.TabIndex = 27;
            this.bWT.Tag = "wt";
            this.bWT.Text = "fx";
            this.bWT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWT.UseVisualStyleBackColor = true;
            this.bWT.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bWR
            // 
            this.bWR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bWR.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bWR.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bWR.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bWR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWR.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bWR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWR.Location = new System.Drawing.Point(403, 108);
            this.bWR.Name = "bWR";
            this.bWR.OverriddenSize = null;
            this.bWR.Size = new System.Drawing.Size(22, 21);
            this.bWR.TabIndex = 20;
            this.bWR.Tag = "wr";
            this.bWR.Text = "fx";
            this.bWR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWR.UseVisualStyleBackColor = true;
            this.bWR.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bCR
            // 
            this.bCR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCR.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCR.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCR.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCR.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCR.Location = new System.Drawing.Point(276, 108);
            this.bCR.Name = "bCR";
            this.bCR.OverriddenSize = null;
            this.bCR.Size = new System.Drawing.Size(22, 21);
            this.bCR.TabIndex = 17;
            this.bCR.Tag = "cr";
            this.bCR.Text = "fx";
            this.bCR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCR.UseVisualStyleBackColor = true;
            this.bCR.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bWL
            // 
            this.bWL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bWL.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bWL.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bWL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bWL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWL.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bWL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWL.Location = new System.Drawing.Point(403, 75);
            this.bWL.Name = "bWL";
            this.bWL.OverriddenSize = null;
            this.bWL.Size = new System.Drawing.Size(22, 21);
            this.bWL.TabIndex = 13;
            this.bWL.Tag = "wl";
            this.bWL.Text = "fx";
            this.bWL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWL.UseVisualStyleBackColor = true;
            this.bWL.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bWD
            // 
            this.bWD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bWD.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bWD.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bWD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bWD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bWD.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bWD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bWD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWD.Location = new System.Drawing.Point(403, 42);
            this.bWD.Name = "bWD";
            this.bWD.OverriddenSize = null;
            this.bWD.Size = new System.Drawing.Size(22, 21);
            this.bWD.TabIndex = 6;
            this.bWD.Tag = "wd";
            this.bWD.Text = "fx";
            this.bWD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bWD.UseVisualStyleBackColor = true;
            this.bWD.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bCL
            // 
            this.bCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCL.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCL.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCL.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCL.Location = new System.Drawing.Point(276, 75);
            this.bCL.Name = "bCL";
            this.bCL.OverriddenSize = null;
            this.bCL.Size = new System.Drawing.Size(22, 21);
            this.bCL.TabIndex = 10;
            this.bCL.Tag = "cl";
            this.bCL.Text = "fx";
            this.bCL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCL.UseVisualStyleBackColor = true;
            this.bCL.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // StyleBorderCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.bCL);
            this.Controls.Add(this.bWD);
            this.Controls.Add(this.bWL);
            this.Controls.Add(this.bCR);
            this.Controls.Add(this.bWR);
            this.Controls.Add(this.bWT);
            this.Controls.Add(this.bWB);
            this.Controls.Add(this.bCB);
            this.Controls.Add(this.bCT);
            this.Controls.Add(this.bCD);
            this.Controls.Add(this.bSB);
            this.Controls.Add(this.bST);
            this.Controls.Add(this.bSR);
            this.Controls.Add(this.bSL);
            this.Controls.Add(this.bSD);
            this.Controls.Add(this.tbWidthDefault);
            this.Controls.Add(this.bColorDefault);
            this.Controls.Add(this.cbColorDefault);
            this.Controls.Add(this.cbStyleDefault);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbWidthBottom);
            this.Controls.Add(this.tbWidthTop);
            this.Controls.Add(this.tbWidthRight);
            this.Controls.Add(this.tbWidthLeft);
            this.Controls.Add(this.bColorBottom);
            this.Controls.Add(this.cbColorBottom);
            this.Controls.Add(this.bColorTop);
            this.Controls.Add(this.cbColorTop);
            this.Controls.Add(this.bColorRight);
            this.Controls.Add(this.cbColorRight);
            this.Controls.Add(this.bColorLeft);
            this.Controls.Add(this.cbColorLeft);
            this.Controls.Add(this.cbStyleRight);
            this.Controls.Add(this.cbStyleTop);
            this.Controls.Add(this.cbStyleBottom);
            this.Controls.Add(this.cbStyleLeft);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lRight);
            this.Controls.Add(this.lTop);
            this.Controls.Add(this.lBottom);
            this.Controls.Add(this.lLeft);
            this.Name = "StyleBorderCtl";
            this.Size = new System.Drawing.Size(472, 312);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
  
		public bool IsValid()
		{
			string name="";
			try
			{
				if (fWidthDefault && !this.tbWidthDefault.Text.StartsWith("="))
				{
					name = "Default Width";
					DesignerUtility.ValidateSize(this.tbWidthDefault.Text, true, false);
				}
				if (fWidthLeft && !this.tbWidthLeft.Text.StartsWith("="))
				{
					name = "Left Width";
					DesignerUtility.ValidateSize(this.tbWidthLeft.Text, true, false);
				}
				if (fWidthTop && !this.tbWidthTop.Text.StartsWith("="))
				{
					name = "Top Width";
					DesignerUtility.ValidateSize(this.tbWidthTop.Text, true, false);
				}
				if (fWidthBottom && !this.tbWidthBottom.Text.StartsWith("="))
				{
					name = "Bottom Width";
					DesignerUtility.ValidateSize(this.tbWidthBottom.Text, true, false);
				}
				if (fWidthRight && !this.tbWidthRight.Text.StartsWith("="))
				{
					name = "Right Width";
					DesignerUtility.ValidateSize(this.tbWidthRight.Text, true, false);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, name + " Size Invalid");
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

			fStyleDefault = fStyleLeft = fStyleRight = fStyleTop = fStyleBottom =
				fColorDefault = fColorLeft = fColorRight = fColorTop = fColorBottom =
				fWidthDefault = fWidthLeft = fWidthRight = fWidthTop = fWidthBottom= false;
		}

		private void ApplyChanges(XmlNode xNode)
		{
            if (_names != null)
            {
                xNode = _Draw.FindCreateNextInHierarchy(xNode, _names);
            }

            bool bLine = xNode.Name == "Line";
			XmlNode sNode = _Draw.GetCreateNamedChildNode(xNode, "Style");

			// Handle BorderStyle
			XmlNode bsNode = _Draw.SetElement(sNode, "BorderStyle", null);
			if (fStyleDefault)
				_Draw.SetElement(bsNode, "Default", cbStyleDefault.Text);
			if (fStyleLeft && !bLine)
				_Draw.SetElement(bsNode, "Left", cbStyleLeft.Text);
			if (fStyleRight && !bLine)
				_Draw.SetElement(bsNode, "Right", cbStyleRight.Text);
			if (fStyleTop && !bLine)
				_Draw.SetElement(bsNode, "Top", cbStyleTop.Text);
			if (fStyleBottom && !bLine)
				_Draw.SetElement(bsNode, "Bottom", cbStyleBottom.Text);

			// Handle BorderColor
			XmlNode csNode = _Draw.SetElement(sNode, "BorderColor", null);
			if (fColorDefault)
				_Draw.SetElement(csNode, "Default", cbColorDefault.Text);
			if (fColorLeft && !bLine)
				_Draw.SetElement(csNode, "Left", cbColorLeft.Text);
			if (fColorRight && !bLine)
				_Draw.SetElement(csNode, "Right", cbColorRight.Text);
			if (fColorTop && !bLine)
				_Draw.SetElement(csNode, "Top", cbColorTop.Text);
			if (fColorBottom && !bLine)
				_Draw.SetElement(csNode, "Bottom", cbColorBottom.Text);

			// Handle BorderWidth
			XmlNode bwNode = _Draw.SetElement(sNode, "BorderWidth", null);
			if (fWidthDefault)
				_Draw.SetElement(bwNode, "Default", GetSize(tbWidthDefault.Text));
			if (fWidthLeft && !bLine)
				_Draw.SetElement(bwNode, "Left", GetSize(tbWidthLeft.Text));
			if (fWidthRight && !bLine)
				_Draw.SetElement(bwNode, "Right", GetSize(tbWidthRight.Text));
			if (fWidthTop && !bLine)
				_Draw.SetElement(bwNode, "Top", GetSize(tbWidthTop.Text));
			if (fWidthBottom && !bLine)
				_Draw.SetElement(bwNode, "Bottom", GetSize(tbWidthBottom.Text));
		}

		private string GetSize(string sz)
		{
			if (sz.Trim().StartsWith("="))		// Don't mess with expressions
				return sz;

			float size = DesignXmlDraw.GetSize(sz);
			if (size <= 0)
			{
				size = DesignXmlDraw.GetSize(sz+"pt");	// Try assuming pt
				if (size <= 0)	// still no good
					size = 10;	// just set default value
			}
			string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}pt", size);
			return rs;
		}

		private void bColor_Click(object sender, System.EventArgs e)
		{
            using (ColorDialog cd = new ColorDialog())
            {
                cd.AnyColor = true;
                cd.FullOpen = true;

                cd.CustomColors = RdlDesigner.GetCustomColors();

                if (cd.ShowDialog() != DialogResult.OK)
                    return;

                RdlDesigner.SetCustomColors(cd.CustomColors);
                if (sender == this.bColorDefault)
                {
                    cbColorDefault.Text = ColorTranslator.ToHtml(cd.Color);
                    cbColorLeft.Text = ColorTranslator.ToHtml(cd.Color);
                    cbColorRight.Text = ColorTranslator.ToHtml(cd.Color);
                    cbColorTop.Text = ColorTranslator.ToHtml(cd.Color);
                    cbColorBottom.Text = ColorTranslator.ToHtml(cd.Color);
                }
                else if (sender == this.bColorLeft)
                    cbColorLeft.Text = ColorTranslator.ToHtml(cd.Color);
                else if (sender == this.bColorRight)
                    cbColorRight.Text = ColorTranslator.ToHtml(cd.Color);
                else if (sender == this.bColorTop)
                    cbColorTop.Text = ColorTranslator.ToHtml(cd.Color);
                else if (sender == this.bColorBottom)
                    cbColorBottom.Text = ColorTranslator.ToHtml(cd.Color);
            }
		
			return;
		}

		private void cbStyleDefault_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cbStyleLeft.Text = cbStyleRight.Text = 
				cbStyleTop.Text = cbStyleBottom.Text = cbStyleDefault.Text;
			fStyleDefault = fStyleLeft = fStyleRight = fStyleTop = fStyleBottom = true;
		}

		private void cbColorDefault_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cbColorLeft.Text = cbColorRight.Text = 
				cbColorTop.Text = cbColorBottom.Text = cbColorDefault.Text;
			fColorDefault = fColorLeft = fColorRight = fColorTop = fColorBottom = true;
		}

		private void tbWidthDefault_TextChanged(object sender, System.EventArgs e)
		{
			tbWidthLeft.Text = tbWidthRight.Text = 
				tbWidthTop.Text = tbWidthBottom.Text = tbWidthDefault.Text;
			fWidthDefault = fWidthLeft = fWidthRight = fWidthTop = fWidthBottom = true;
		}

		private void cbStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (sender == cbStyleLeft)
				fStyleLeft = true;
			else if (sender == cbStyleRight)
				fStyleRight = true;
			else if (sender == cbStyleTop)
				fStyleTop = true;
			else if (sender == cbStyleBottom)
				fStyleBottom = true;
		}

		private void cbColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (sender == cbColorLeft)
				fColorLeft = true;
			else if (sender == cbColorRight)
				fColorRight = true;
			else if (sender == cbColorTop)
				fColorTop = true;
			else if (sender == cbColorBottom)
				fColorBottom = true;
		}

		private void tbWidth_Changed(object sender, System.EventArgs e)
		{
			if (sender == tbWidthLeft)
				fWidthLeft = true;
			else if (sender == tbWidthRight)
				fWidthRight = true;
			else if (sender == tbWidthTop)
				fWidthTop = true;
			else if (sender == tbWidthBottom)
				fWidthBottom = true;
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
				case "sd":
					c = cbStyleDefault;
					break;
				case "cd":
					c = cbColorDefault;
					bColor = true;
					break;
				case "wd":
					c = tbWidthDefault;
					break;
				case "sl":
					c = cbStyleLeft;
					break;
				case "cl":
					c = cbColorLeft;
					bColor = true;
					break;
				case "wl":
					c = tbWidthLeft;
					break;
				case "sr":
					c = cbStyleRight;
					break;
				case "cr":
					c = cbColorRight;
					bColor = true;
					break;
				case "wr":
					c = tbWidthRight;
					break;
				case "st":
					c = cbStyleTop;
					break;
				case "ct":
					c = cbColorTop;
					bColor = true;
					break;
				case "wt":
					c = tbWidthTop;
					break;
				case "sb":
					c = cbStyleBottom;
					break;
				case "cb":
					c = cbColorBottom;
					bColor = true;
					break;
				case "wb":
					c = tbWidthBottom;
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
