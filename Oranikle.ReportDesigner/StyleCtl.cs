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
	/// Summary description for StyleCtl.
	/// </summary>
	internal class StyleCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		// flags for controlling whether syntax changed for a particular property
		private bool fPadLeft, fPadRight, fPadTop, fPadBottom;
		private bool fEndColor, fBackColor, fGradient, fDEName, fDEOutput;

		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private Oranikle.Studio.Controls.CustomTextControl tbPadLeft;
		private Oranikle.Studio.Controls.CustomTextControl tbPadRight;
		private Oranikle.Studio.Controls.CustomTextControl tbPadTop;
		private System.Windows.Forms.GroupBox grpBoxPadding;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.StyledButton bBackColor;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label15;
		private Oranikle.Studio.Controls.StyledComboBox cbEndColor;
		private Oranikle.Studio.Controls.StyledComboBox cbBackColor;
		private Oranikle.Studio.Controls.StyledButton bEndColor;
		private Oranikle.Studio.Controls.StyledComboBox cbGradient;
		private Oranikle.Studio.Controls.CustomTextControl tbPadBottom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.CustomTextControl tbDEName;
		private Oranikle.Studio.Controls.StyledComboBox cbDEOutput;
		private System.Windows.Forms.GroupBox gbXML;
		private Oranikle.Studio.Controls.StyledButton bValueExpr;
		private Oranikle.Studio.Controls.StyledButton button1;
		private Oranikle.Studio.Controls.StyledButton button2;
		private Oranikle.Studio.Controls.StyledButton button3;
		private Oranikle.Studio.Controls.StyledButton bGradient;
		private Oranikle.Studio.Controls.StyledButton bExprBackColor;
		private Oranikle.Studio.Controls.StyledButton bExprEndColor;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal StyleCtl(DesignXmlDraw dxDraw, List<XmlNode> reportItems)
		{
			_ReportItems = reportItems;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues(_ReportItems[0]);			
		}

		private void InitValues(XmlNode node)
		{
            cbEndColor.Items.AddRange(StaticLists.ColorList);
            cbBackColor.Items.AddRange(StaticLists.ColorList);

			XmlNode sNode = _Draw.GetNamedChildNode(node, "Style");

			// Handle padding
			tbPadLeft.Text = _Draw.GetElementValue(sNode, "PaddingLeft", "0pt");
			tbPadRight.Text = _Draw.GetElementValue(sNode, "PaddingRight", "0pt");
			tbPadTop.Text = _Draw.GetElementValue(sNode, "PaddingTop", "0pt");
			tbPadBottom.Text = _Draw.GetElementValue(sNode, "PaddingBottom", "0pt");

			this.cbBackColor.Text = _Draw.GetElementValue(sNode, "BackgroundColor", "");
			this.cbEndColor.Text = _Draw.GetElementValue(sNode, "BackgroundGradientEndColor", "");
			this.cbGradient.Text = _Draw.GetElementValue(sNode, "BackgroundGradientType", "None");
			this.tbDEName.Text = _Draw.GetElementValue(node, "DataElementName", "");
			this.cbDEOutput.Text = _Draw.GetElementValue(node, "DataElementOutput", "Auto");
			if (node.Name != "Chart")
			{   // only chart support gradients
				this.cbEndColor.Enabled = bExprEndColor.Enabled =
					cbGradient.Enabled = bGradient.Enabled = 
					this.bEndColor.Enabled = bExprEndColor.Enabled = false;
			}
			if (node.Name == "Line" || node.Name == "System.Drawing.Image")
			{
				gbXML.Visible = false;
			}

			// nothing has changed now
			fPadLeft = fPadRight = fPadTop = fPadBottom =
				fEndColor = fBackColor = fGradient = fDEName = fDEOutput = false;
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
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbPadLeft = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbPadRight = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbPadTop = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbPadBottom = new Oranikle.Studio.Controls.CustomTextControl();
            this.grpBoxPadding = new System.Windows.Forms.GroupBox();
            this.button3 = new Oranikle.Studio.Controls.StyledButton();
            this.button2 = new Oranikle.Studio.Controls.StyledButton();
            this.button1 = new Oranikle.Studio.Controls.StyledButton();
            this.bValueExpr = new Oranikle.Studio.Controls.StyledButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bGradient = new Oranikle.Studio.Controls.StyledButton();
            this.bExprBackColor = new Oranikle.Studio.Controls.StyledButton();
            this.bExprEndColor = new Oranikle.Studio.Controls.StyledButton();
            this.bEndColor = new Oranikle.Studio.Controls.StyledButton();
            this.cbBackColor = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbEndColor = new Oranikle.Studio.Controls.StyledComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbGradient = new Oranikle.Studio.Controls.StyledComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.bBackColor = new Oranikle.Studio.Controls.StyledButton();
            this.label3 = new System.Windows.Forms.Label();
            this.gbXML = new System.Windows.Forms.GroupBox();
            this.cbDEOutput = new Oranikle.Studio.Controls.StyledComboBox();
            this.tbDEName = new Oranikle.Studio.Controls.CustomTextControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBoxPadding.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbXML.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 16);
            this.label11.TabIndex = 20;
            this.label11.Text = "Left";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(224, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 21;
            this.label12.Text = "Right";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 16);
            this.label13.TabIndex = 22;
            this.label13.Text = "Top";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(216, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 16);
            this.label14.TabIndex = 23;
            this.label14.Text = "Bottom";
            // 
            // tbPadLeft
            // 
            this.tbPadLeft.AddX = 0;
            this.tbPadLeft.AddY = 0;
            this.tbPadLeft.AllowSpace = false;
            this.tbPadLeft.BorderColor = System.Drawing.Color.LightGray;
            this.tbPadLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPadLeft.ChangeVisibility = false;
            this.tbPadLeft.ChildControl = null;
            this.tbPadLeft.ConvertEnterToTab = true;
            this.tbPadLeft.ConvertEnterToTabForDialogs = false;
            this.tbPadLeft.Decimals = 0;
            this.tbPadLeft.DisplayList = new object[0];
            this.tbPadLeft.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPadLeft.Location = new System.Drawing.Point(48, 26);
            this.tbPadLeft.Name = "tbPadLeft";
            this.tbPadLeft.OnDropDownCloseFocus = true;
            this.tbPadLeft.SelectType = 0;
            this.tbPadLeft.Size = new System.Drawing.Size(128, 20);
            this.tbPadLeft.TabIndex = 0;
            this.tbPadLeft.UseValueForChildsVisibilty = false;
            this.tbPadLeft.Value = true;
            this.tbPadLeft.TextChanged += new System.EventHandler(this.tbPadLeft_TextChanged);
            // 
            // tbPadRight
            // 
            this.tbPadRight.AddX = 0;
            this.tbPadRight.AddY = 0;
            this.tbPadRight.AllowSpace = false;
            this.tbPadRight.BorderColor = System.Drawing.Color.LightGray;
            this.tbPadRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPadRight.ChangeVisibility = false;
            this.tbPadRight.ChildControl = null;
            this.tbPadRight.ConvertEnterToTab = true;
            this.tbPadRight.ConvertEnterToTabForDialogs = false;
            this.tbPadRight.Decimals = 0;
            this.tbPadRight.DisplayList = new object[0];
            this.tbPadRight.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPadRight.Location = new System.Drawing.Point(264, 26);
            this.tbPadRight.Name = "tbPadRight";
            this.tbPadRight.OnDropDownCloseFocus = true;
            this.tbPadRight.SelectType = 0;
            this.tbPadRight.Size = new System.Drawing.Size(128, 20);
            this.tbPadRight.TabIndex = 2;
            this.tbPadRight.UseValueForChildsVisibilty = false;
            this.tbPadRight.Value = true;
            this.tbPadRight.TextChanged += new System.EventHandler(this.tbPadRight_TextChanged);
            // 
            // tbPadTop
            // 
            this.tbPadTop.AddX = 0;
            this.tbPadTop.AddY = 0;
            this.tbPadTop.AllowSpace = false;
            this.tbPadTop.BorderColor = System.Drawing.Color.LightGray;
            this.tbPadTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPadTop.ChangeVisibility = false;
            this.tbPadTop.ChildControl = null;
            this.tbPadTop.ConvertEnterToTab = true;
            this.tbPadTop.ConvertEnterToTabForDialogs = false;
            this.tbPadTop.Decimals = 0;
            this.tbPadTop.DisplayList = new object[0];
            this.tbPadTop.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPadTop.Location = new System.Drawing.Point(48, 58);
            this.tbPadTop.Name = "tbPadTop";
            this.tbPadTop.OnDropDownCloseFocus = true;
            this.tbPadTop.SelectType = 0;
            this.tbPadTop.Size = new System.Drawing.Size(128, 20);
            this.tbPadTop.TabIndex = 4;
            this.tbPadTop.UseValueForChildsVisibilty = false;
            this.tbPadTop.Value = true;
            this.tbPadTop.TextChanged += new System.EventHandler(this.tbPadTop_TextChanged);
            // 
            // tbPadBottom
            // 
            this.tbPadBottom.AddX = 0;
            this.tbPadBottom.AddY = 0;
            this.tbPadBottom.AllowSpace = false;
            this.tbPadBottom.BorderColor = System.Drawing.Color.LightGray;
            this.tbPadBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPadBottom.ChangeVisibility = false;
            this.tbPadBottom.ChildControl = null;
            this.tbPadBottom.ConvertEnterToTab = true;
            this.tbPadBottom.ConvertEnterToTabForDialogs = false;
            this.tbPadBottom.Decimals = 0;
            this.tbPadBottom.DisplayList = new object[0];
            this.tbPadBottom.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPadBottom.Location = new System.Drawing.Point(264, 58);
            this.tbPadBottom.Name = "tbPadBottom";
            this.tbPadBottom.OnDropDownCloseFocus = true;
            this.tbPadBottom.SelectType = 0;
            this.tbPadBottom.Size = new System.Drawing.Size(128, 20);
            this.tbPadBottom.TabIndex = 6;
            this.tbPadBottom.UseValueForChildsVisibilty = false;
            this.tbPadBottom.Value = true;
            this.tbPadBottom.TextChanged += new System.EventHandler(this.tbPadBottom_TextChanged);
            // 
            // grpBoxPadding
            // 
            this.grpBoxPadding.Controls.Add(this.button3);
            this.grpBoxPadding.Controls.Add(this.button2);
            this.grpBoxPadding.Controls.Add(this.button1);
            this.grpBoxPadding.Controls.Add(this.bValueExpr);
            this.grpBoxPadding.Controls.Add(this.label13);
            this.grpBoxPadding.Controls.Add(this.tbPadRight);
            this.grpBoxPadding.Controls.Add(this.label14);
            this.grpBoxPadding.Controls.Add(this.label11);
            this.grpBoxPadding.Controls.Add(this.tbPadBottom);
            this.grpBoxPadding.Controls.Add(this.label12);
            this.grpBoxPadding.Controls.Add(this.tbPadTop);
            this.grpBoxPadding.Controls.Add(this.tbPadLeft);
            this.grpBoxPadding.Location = new System.Drawing.Point(16, 96);
            this.grpBoxPadding.Name = "grpBoxPadding";
            this.grpBoxPadding.Size = new System.Drawing.Size(432, 88);
            this.grpBoxPadding.TabIndex = 1;
            this.grpBoxPadding.TabStop = false;
            this.grpBoxPadding.Text = "Padding";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.button3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.button3.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.button3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(400, 26);
            this.button3.Name = "button3";
            this.button3.OverriddenSize = null;
            this.button3.Size = new System.Drawing.Size(22, 21);
            this.button3.TabIndex = 3;
            this.button3.Tag = "pright";
            this.button3.Text = "fx";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.bExpr_Click);
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
            this.button2.Location = new System.Drawing.Point(400, 58);
            this.button2.Name = "button2";
            this.button2.OverriddenSize = null;
            this.button2.Size = new System.Drawing.Size(22, 21);
            this.button2.TabIndex = 7;
            this.button2.Tag = "pbottom";
            this.button2.Text = "fx";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.button1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.button1.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(184, 58);
            this.button1.Name = "button1";
            this.button1.OverriddenSize = null;
            this.button1.Size = new System.Drawing.Size(22, 21);
            this.button1.TabIndex = 5;
            this.button1.Tag = "ptop";
            this.button1.Text = "fx";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.bExpr_Click);
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
            this.bValueExpr.Location = new System.Drawing.Point(184, 26);
            this.bValueExpr.Name = "bValueExpr";
            this.bValueExpr.OverriddenSize = null;
            this.bValueExpr.Size = new System.Drawing.Size(22, 21);
            this.bValueExpr.TabIndex = 1;
            this.bValueExpr.Tag = "pleft";
            this.bValueExpr.Text = "fx";
            this.bValueExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bValueExpr.UseVisualStyleBackColor = true;
            this.bValueExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bGradient);
            this.groupBox1.Controls.Add(this.bExprBackColor);
            this.groupBox1.Controls.Add(this.bExprEndColor);
            this.groupBox1.Controls.Add(this.bEndColor);
            this.groupBox1.Controls.Add(this.cbBackColor);
            this.groupBox1.Controls.Add(this.cbEndColor);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cbGradient);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.bBackColor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Background";
            // 
            // bGradient
            // 
            this.bGradient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bGradient.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bGradient.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bGradient.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bGradient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGradient.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bGradient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bGradient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bGradient.Location = new System.Drawing.Point(253, 42);
            this.bGradient.Name = "bGradient";
            this.bGradient.OverriddenSize = null;
            this.bGradient.Size = new System.Drawing.Size(22, 21);
            this.bGradient.TabIndex = 4;
            this.bGradient.Tag = "bgradient";
            this.bGradient.Text = "fx";
            this.bGradient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bGradient.UseVisualStyleBackColor = true;
            this.bGradient.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bExprBackColor
            // 
            this.bExprBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bExprBackColor.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bExprBackColor.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bExprBackColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bExprBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExprBackColor.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bExprBackColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bExprBackColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExprBackColor.Location = new System.Drawing.Point(102, 42);
            this.bExprBackColor.Name = "bExprBackColor";
            this.bExprBackColor.OverriddenSize = null;
            this.bExprBackColor.Size = new System.Drawing.Size(22, 21);
            this.bExprBackColor.TabIndex = 1;
            this.bExprBackColor.Tag = "bcolor";
            this.bExprBackColor.Text = "fx";
            this.bExprBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExprBackColor.UseVisualStyleBackColor = true;
            this.bExprBackColor.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bExprEndColor
            // 
            this.bExprEndColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bExprEndColor.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bExprEndColor.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bExprEndColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bExprEndColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExprEndColor.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bExprEndColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bExprEndColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExprEndColor.Location = new System.Drawing.Point(377, 42);
            this.bExprEndColor.Name = "bExprEndColor";
            this.bExprEndColor.OverriddenSize = null;
            this.bExprEndColor.Size = new System.Drawing.Size(22, 21);
            this.bExprEndColor.TabIndex = 6;
            this.bExprEndColor.Tag = "bendcolor";
            this.bExprEndColor.Text = "fx";
            this.bExprEndColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExprEndColor.UseVisualStyleBackColor = true;
            this.bExprEndColor.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bEndColor
            // 
            this.bEndColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bEndColor.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bEndColor.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bEndColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bEndColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEndColor.Font = new System.Drawing.Font("Arial", 9F);
            this.bEndColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bEndColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEndColor.Location = new System.Drawing.Point(404, 42);
            this.bEndColor.Name = "bEndColor";
            this.bEndColor.OverriddenSize = null;
            this.bEndColor.Size = new System.Drawing.Size(22, 21);
            this.bEndColor.TabIndex = 7;
            this.bEndColor.Text = "...";
            this.bEndColor.UseVisualStyleBackColor = true;
            this.bEndColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // cbBackColor
            // 
            this.cbBackColor.AutoAdjustItemHeight = false;
            this.cbBackColor.BorderColor = System.Drawing.Color.LightGray;
            this.cbBackColor.ConvertEnterToTabForDialogs = false;
            this.cbBackColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBackColor.Location = new System.Drawing.Point(8, 42);
            this.cbBackColor.Name = "cbBackColor";
            this.cbBackColor.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbBackColor.SeparatorMargin = 1;
            this.cbBackColor.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbBackColor.SeparatorWidth = 1;
            this.cbBackColor.Size = new System.Drawing.Size(88, 21);
            this.cbBackColor.TabIndex = 0;
            this.cbBackColor.SelectedIndexChanged += new System.EventHandler(this.cbBackColor_SelectedIndexChanged);
            this.cbBackColor.TextChanged += new System.EventHandler(this.cbBackColor_SelectedIndexChanged);
            // 
            // cbEndColor
            // 
            this.cbEndColor.AutoAdjustItemHeight = false;
            this.cbEndColor.BorderColor = System.Drawing.Color.LightGray;
            this.cbEndColor.ConvertEnterToTabForDialogs = false;
            this.cbEndColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbEndColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEndColor.Location = new System.Drawing.Point(286, 42);
            this.cbEndColor.Name = "cbEndColor";
            this.cbEndColor.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbEndColor.SeparatorMargin = 1;
            this.cbEndColor.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbEndColor.SeparatorWidth = 1;
            this.cbEndColor.Size = new System.Drawing.Size(88, 21);
            this.cbEndColor.TabIndex = 5;
            this.cbEndColor.SelectedIndexChanged += new System.EventHandler(this.cbEndColor_SelectedIndexChanged);
            this.cbEndColor.TextChanged += new System.EventHandler(this.cbEndColor_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(286, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 5;
            this.label15.Text = "End Color";
            // 
            // cbGradient
            // 
            this.cbGradient.AutoAdjustItemHeight = false;
            this.cbGradient.BorderColor = System.Drawing.Color.LightGray;
            this.cbGradient.ConvertEnterToTabForDialogs = false;
            this.cbGradient.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGradient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGradient.Items.AddRange(new object[] {
            "None",
            "LeftRight",
            "TopBottom",
            "Center",
            "DiagonalLeft",
            "DiagonalRight",
            "HorizontalCenter",
            "VerticalCenter"});
            this.cbGradient.Location = new System.Drawing.Point(161, 42);
            this.cbGradient.Name = "cbGradient";
            this.cbGradient.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbGradient.SeparatorMargin = 1;
            this.cbGradient.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbGradient.SeparatorWidth = 1;
            this.cbGradient.Size = new System.Drawing.Size(88, 21);
            this.cbGradient.TabIndex = 3;
            this.cbGradient.SelectedIndexChanged += new System.EventHandler(this.cbGradient_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(161, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Gradient";
            // 
            // bBackColor
            // 
            this.bBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bBackColor.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bBackColor.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bBackColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBackColor.Font = new System.Drawing.Font("Arial", 9F);
            this.bBackColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bBackColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBackColor.Location = new System.Drawing.Point(128, 42);
            this.bBackColor.Name = "bBackColor";
            this.bBackColor.OverriddenSize = null;
            this.bBackColor.Size = new System.Drawing.Size(22, 21);
            this.bBackColor.TabIndex = 2;
            this.bBackColor.Text = "...";
            this.bBackColor.UseVisualStyleBackColor = true;
            this.bBackColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Color";
            // 
            // gbXML
            // 
            this.gbXML.Controls.Add(this.cbDEOutput);
            this.gbXML.Controls.Add(this.tbDEName);
            this.gbXML.Controls.Add(this.label2);
            this.gbXML.Controls.Add(this.label1);
            this.gbXML.Location = new System.Drawing.Point(16, 200);
            this.gbXML.Name = "gbXML";
            this.gbXML.Size = new System.Drawing.Size(432, 80);
            this.gbXML.TabIndex = 24;
            this.gbXML.TabStop = false;
            this.gbXML.Text = "XML";
            // 
            // cbDEOutput
            // 
            this.cbDEOutput.AutoAdjustItemHeight = false;
            this.cbDEOutput.BorderColor = System.Drawing.Color.LightGray;
            this.cbDEOutput.ConvertEnterToTabForDialogs = false;
            this.cbDEOutput.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDEOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDEOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDEOutput.Items.AddRange(new object[] {
            "Output",
            "NoOutput",
            "ContentsOnly",
            "Auto"});
            this.cbDEOutput.Location = new System.Drawing.Point(138, 43);
            this.cbDEOutput.Name = "cbDEOutput";
            this.cbDEOutput.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDEOutput.SeparatorMargin = 1;
            this.cbDEOutput.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDEOutput.SeparatorWidth = 1;
            this.cbDEOutput.Size = new System.Drawing.Size(117, 21);
            this.cbDEOutput.TabIndex = 3;
            this.cbDEOutput.SelectedIndexChanged += new System.EventHandler(this.cbDEOutput_SelectedIndexChanged);
            // 
            // tbDEName
            // 
            this.tbDEName.AddX = 0;
            this.tbDEName.AddY = 0;
            this.tbDEName.AllowSpace = false;
            this.tbDEName.BorderColor = System.Drawing.Color.LightGray;
            this.tbDEName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDEName.ChangeVisibility = false;
            this.tbDEName.ChildControl = null;
            this.tbDEName.ConvertEnterToTab = true;
            this.tbDEName.ConvertEnterToTabForDialogs = false;
            this.tbDEName.Decimals = 0;
            this.tbDEName.DisplayList = new object[0];
            this.tbDEName.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDEName.Location = new System.Drawing.Point(137, 18);
            this.tbDEName.Name = "tbDEName";
            this.tbDEName.OnDropDownCloseFocus = true;
            this.tbDEName.SelectType = 0;
            this.tbDEName.Size = new System.Drawing.Size(279, 20);
            this.tbDEName.TabIndex = 2;
            this.tbDEName.Text = "textBox1";
            this.tbDEName.UseValueForChildsVisibilty = false;
            this.tbDEName.Value = true;
            this.tbDEName.TextChanged += new System.EventHandler(this.tbDEName_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "DataElementOutput";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "DataElementName";
            // 
            // StyleCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBoxPadding);
            this.Controls.Add(this.gbXML);
            this.Name = "StyleCtl";
            this.Size = new System.Drawing.Size(472, 312);
            this.grpBoxPadding.ResumeLayout(false);
            this.grpBoxPadding.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbXML.ResumeLayout(false);
            this.gbXML.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
     
		public bool IsValid()
		{
			string name="";
			try
			{
				if (fPadLeft && !this.tbPadLeft.Text.StartsWith("="))
				{
					name = "Left";
					DesignerUtility.ValidateSize(this.tbPadLeft.Text, true, false);
				}
				
				if (fPadRight && !this.tbPadRight.Text.StartsWith("="))
				{
					name = "Right";
					DesignerUtility.ValidateSize(this.tbPadRight.Text, true, false);
				}
				
				if (fPadTop && !this.tbPadTop.Text.StartsWith("="))
				{
					name = "Top";
					DesignerUtility.ValidateSize(this.tbPadTop.Text, true, false);
				}
				
				if (fPadBottom && !this.tbPadBottom.Text.StartsWith("="))
				{
					name = "Bottom";
					DesignerUtility.ValidateSize(this.tbPadBottom.Text, true, false);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, name + " Padding Invalid");
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

			// nothing has changed now
			fPadLeft = fPadRight = fPadTop = fPadBottom =
				fEndColor = fBackColor = fGradient = fDEName = fDEOutput = false;
		}

		private void bFont_Click(object sender, System.EventArgs e)
		{
            using (FontDialog fd = new FontDialog())
            {
                fd.ShowColor = true;
                if (fd.ShowDialog() != DialogResult.OK)
                    return;
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

                if (cd.ShowDialog() != DialogResult.OK)
                    return;

                RdlDesigner.SetCustomColors(cd.CustomColors);
                if (sender == this.bEndColor)
                    cbEndColor.Text = ColorTranslator.ToHtml(cd.Color);
                else if (sender == this.bBackColor)
                    cbBackColor.Text = ColorTranslator.ToHtml(cd.Color);
            }
			return;
		}

		private void cbBackColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fBackColor = true;
		}

		private void cbGradient_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fGradient = true;
		}

		private void cbEndColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fEndColor = true;
		}

		private void tbPadLeft_TextChanged(object sender, System.EventArgs e)
		{
			fPadLeft = true;
		}

		private void tbPadRight_TextChanged(object sender, System.EventArgs e)
		{
			fPadRight = true;
		}

		private void tbPadTop_TextChanged(object sender, System.EventArgs e)
		{
			fPadTop = true;
		}

		private void tbPadBottom_TextChanged(object sender, System.EventArgs e)
		{
			fPadBottom = true;
		}
		
		private void ApplyChanges(XmlNode rNode)
		{
			XmlNode xNode = _Draw.GetNamedChildNode(rNode, "Style");

			if (fPadLeft)
			{ _Draw.SetElement(xNode, "PaddingLeft", tbPadLeft.Text); }
			if (fPadRight)
			{ _Draw.SetElement(xNode, "PaddingRight", tbPadRight.Text); }
			if (fPadTop)
			{ _Draw.SetElement(xNode, "PaddingTop", tbPadTop.Text); }
			if (fPadBottom)
			{ _Draw.SetElement(xNode, "PaddingBottom", tbPadBottom.Text); }
			if (fEndColor)
			{ _Draw.SetElement(xNode, "BackgroundGradientEndColor", cbEndColor.Text); }
			if (fBackColor)
			{ _Draw.SetElement(xNode, "BackgroundColor", cbBackColor.Text); }
			if (fGradient)
			{ _Draw.SetElement(xNode, "BackgroundGradientType", cbGradient.Text); }
			if (fDEName)
			{ _Draw.SetElement(rNode, "DataElementName", tbDEName.Text); }
			if (fDEOutput)
			{ _Draw.SetElement(rNode, "DataElementOutput", cbDEOutput.Text); }
		}

		private void cbDEOutput_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fDEOutput = true;
		}

		private void tbDEName_TextChanged(object sender, System.EventArgs e)
		{
			fDEName = true;
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
				case "pleft":
					c = tbPadLeft;
					break;
				case "pright":
					c = tbPadRight;
					break;
				case "ptop":
					c = tbPadTop;
					break;
				case "pbottom":
					c = tbPadBottom;
					break;
				case "bcolor":
					c = cbBackColor;
					bColor = true;
					break;
				case "bgradient":
					c = cbGradient;
					break;
				case "bendcolor":
					c = cbEndColor;
					bColor = true;
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
