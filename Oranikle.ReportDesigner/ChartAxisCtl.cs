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
	/// Summary description for ChartCtl.
	/// </summary>
	internal class ChartAxisCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		// change flags
		bool fMonth, fVisible, fMajorTickMarks, fMargin,fReverse,fInterlaced;
		bool fMajorGLWidth,fMajorGLColor,fMajorGLStyle;
		bool fMinorGLWidth,fMinorGLColor,fMinorGLStyle;
		bool fMajorInterval, fMinorInterval,fMax,fMin;
		bool fMinorTickMarks,fScalar,fLogScale,fMajorGLShow, fMinorGLShow, fCanOmit;
		
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private Oranikle.Studio.Controls.StyledCheckBox chkMonth;
		private Oranikle.Studio.Controls.StyledCheckBox chkVisible;
		private Oranikle.Studio.Controls.StyledComboBox cbMajorTickMarks;
		private Oranikle.Studio.Controls.StyledCheckBox chkMargin;
		private Oranikle.Studio.Controls.StyledCheckBox chkReverse;
		private Oranikle.Studio.Controls.StyledCheckBox chkInterlaced;
		private System.Windows.Forms.GroupBox groupBox1;
		private Oranikle.Studio.Controls.CustomTextControl tbMajorGLWidth;
		private Oranikle.Studio.Controls.StyledButton bMajorGLColor;
		private Oranikle.Studio.Controls.StyledComboBox cbMajorGLColor;
		private Oranikle.Studio.Controls.StyledComboBox cbMajorGLStyle;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private Oranikle.Studio.Controls.CustomTextControl tbMinorGLWidth;
		private Oranikle.Studio.Controls.StyledButton bMinorGLColor;
		private Oranikle.Studio.Controls.StyledComboBox cbMinorGLColor;
		private Oranikle.Studio.Controls.StyledComboBox cbMinorGLStyle;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private Oranikle.Studio.Controls.CustomTextControl tbMajorInterval;
		private Oranikle.Studio.Controls.CustomTextControl tbMinorInterval;
		private System.Windows.Forms.Label label10;
		private Oranikle.Studio.Controls.CustomTextControl tbMax;
		private System.Windows.Forms.Label label11;
		private Oranikle.Studio.Controls.CustomTextControl tbMin;
		private System.Windows.Forms.Label label12;
		private Oranikle.Studio.Controls.StyledComboBox cbMinorTickMarks;
		private Oranikle.Studio.Controls.StyledCheckBox chkScalar;
		private Oranikle.Studio.Controls.StyledCheckBox chkLogScale;
		private Oranikle.Studio.Controls.StyledCheckBox chkMajorGLShow;
		private Oranikle.Studio.Controls.StyledCheckBox chkMinorGLShow;
		private Oranikle.Studio.Controls.StyledButton bMinorIntervalExpr;
		private Oranikle.Studio.Controls.StyledButton bMajorIntervalExpr;
		private Oranikle.Studio.Controls.StyledButton bMinExpr;
        private Oranikle.Studio.Controls.StyledButton bMaxExpr;
        private Oranikle.Studio.Controls.StyledCheckBox chkCanOmit;
        
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal ChartAxisCtl(DesignXmlDraw dxDraw, List<XmlNode> ris)
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
            cbMajorGLColor.Items.AddRange(StaticLists.ColorList);
            cbMinorGLColor.Items.AddRange(StaticLists.ColorList);

			XmlNode node = _ReportItems[0];
            chkMonth.Checked = _Draw.GetElementValue(node, "fyi:Month", "false").ToLower() == "true" ? true : false; //added checkbox for month category axis WP 12 may 2008
			chkVisible.Checked = _Draw.GetElementValue(node, "Visible", "false").ToLower() == "true"? true: false;
			chkMargin.Checked = _Draw.GetElementValue(node, "Margin", "false").ToLower() == "true"? true: false;
			chkReverse.Checked = _Draw.GetElementValue(node, "Reverse", "false").ToLower() == "true"? true: false;
			chkInterlaced.Checked = _Draw.GetElementValue(node, "Interlaced", "false").ToLower() == "true"? true: false;
			chkScalar.Checked = _Draw.GetElementValue(node, "Scalar", "false").ToLower() == "true"? true: false;
			chkLogScale.Checked = _Draw.GetElementValue(node, "LogScale", "false").ToLower() == "true"? true: false;
            chkCanOmit.Checked = _Draw.GetElementValue(node, "fyi:CanOmit", "false").ToLower() == "true" ? true : false;
            cbMajorTickMarks.Text = _Draw.GetElementValue(node, "MajorTickMarks", "None");
			cbMinorTickMarks.Text = _Draw.GetElementValue(node, "MinorTickMarks", "None");
			// Major Grid Lines
			InitGridLines(node, "MajorGridLines", chkMajorGLShow, cbMajorGLColor, cbMajorGLStyle, tbMajorGLWidth);
			// Minor Grid Lines
			InitGridLines(node, "MinorGridLines", chkMinorGLShow, cbMinorGLColor, cbMinorGLStyle, tbMinorGLWidth);

			tbMajorInterval.Text = _Draw.GetElementValue(node, "MajorInterval", "");
			tbMinorInterval.Text = _Draw.GetElementValue(node, "MinorInterval", "");
			tbMax.Text = _Draw.GetElementValue(node, "Max", "");
			tbMin.Text = _Draw.GetElementValue(node, "Min", "");

			    fMonth = fVisible = fMajorTickMarks = fMargin=fReverse=fInterlaced=
				fMajorGLWidth=fMajorGLColor=fMajorGLStyle=
				fMinorGLWidth=fMinorGLColor=fMinorGLStyle=
				fMajorInterval= fMinorInterval=fMax=fMin=
				fMinorTickMarks=fScalar=fLogScale=fMajorGLShow=fMinorGLShow=fCanOmit=false;
		}

		private void InitGridLines(XmlNode node, string type, CheckBox show, ComboBox color, ComboBox style, TextBox width)
		{
			XmlNode m = _Draw.GetNamedChildNode(node, type);
			if (m != null)
			{
				show.Checked = _Draw.GetElementValue(m, "ShowGridLines", "false").ToLower() == "true"? true: false;
				XmlNode st = _Draw.GetNamedChildNode(m, "Style");
				if (st != null)
				{
					XmlNode work = _Draw.GetNamedChildNode(st, "BorderColor");
					if (work != null)
						color.Text = _Draw.GetElementValue(work, "Default", "Black");
					work = _Draw.GetNamedChildNode(st, "BorderStyle");
					if (work != null)
						style.Text = _Draw.GetElementValue(work, "Default", "Solid");
					work = _Draw.GetNamedChildNode(st, "BorderWidth");
					if (work != null)
						width.Text = _Draw.GetElementValue(work, "Default", "1pt");
				}
			}
			if (color.Text.Length == 0)
				color.Text = "Black";
			if (style.Text.Length == 0)
				style.Text = "Solid";
			if (width.Text.Length == 0)
				width.Text = "1pt";
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMajorTickMarks = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbMinorTickMarks = new Oranikle.Studio.Controls.StyledComboBox();
            this.chkVisible = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkMargin = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkReverse = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkInterlaced = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkScalar = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkLogScale = new Oranikle.Studio.Controls.StyledCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMajorGLShow = new Oranikle.Studio.Controls.StyledCheckBox();
            this.tbMajorGLWidth = new Oranikle.Studio.Controls.CustomTextControl();
            this.bMajorGLColor = new Oranikle.Studio.Controls.StyledButton();
            this.cbMajorGLColor = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbMajorGLStyle = new Oranikle.Studio.Controls.StyledComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkMinorGLShow = new Oranikle.Studio.Controls.StyledCheckBox();
            this.tbMinorGLWidth = new Oranikle.Studio.Controls.CustomTextControl();
            this.bMinorGLColor = new Oranikle.Studio.Controls.StyledButton();
            this.cbMinorGLColor = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbMinorGLStyle = new Oranikle.Studio.Controls.StyledComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbMajorInterval = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbMinorInterval = new Oranikle.Studio.Controls.CustomTextControl();
            this.label10 = new System.Windows.Forms.Label();
            this.tbMax = new Oranikle.Studio.Controls.CustomTextControl();
            this.label11 = new System.Windows.Forms.Label();
            this.tbMin = new Oranikle.Studio.Controls.CustomTextControl();
            this.label12 = new System.Windows.Forms.Label();
            this.bMinorIntervalExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bMajorIntervalExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bMinExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bMaxExpr = new Oranikle.Studio.Controls.StyledButton();
            this.chkCanOmit = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkMonth = new Oranikle.Studio.Controls.StyledCheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Major Tick Marks";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(224, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Minor Tick Marks";
            // 
            // cbMajorTickMarks
            // 
            this.cbMajorTickMarks.AutoAdjustItemHeight = false;
            this.cbMajorTickMarks.BorderColor = System.Drawing.Color.LightGray;
            this.cbMajorTickMarks.ConvertEnterToTabForDialogs = false;
            this.cbMajorTickMarks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMajorTickMarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMajorTickMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMajorTickMarks.Items.AddRange(new object[] {
            "None",
            "Inside",
            "Outside",
            "Cross"});
            this.cbMajorTickMarks.Location = new System.Drawing.Point(128, 8);
            this.cbMajorTickMarks.Name = "cbMajorTickMarks";
            this.cbMajorTickMarks.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMajorTickMarks.SeparatorMargin = 1;
            this.cbMajorTickMarks.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMajorTickMarks.SeparatorWidth = 1;
            this.cbMajorTickMarks.Size = new System.Drawing.Size(80, 21);
            this.cbMajorTickMarks.TabIndex = 2;
            this.cbMajorTickMarks.SelectedIndexChanged += new System.EventHandler(this.cbMajorTickMarks_SelectedIndexChanged);
            // 
            // cbMinorTickMarks
            // 
            this.cbMinorTickMarks.AutoAdjustItemHeight = false;
            this.cbMinorTickMarks.BorderColor = System.Drawing.Color.LightGray;
            this.cbMinorTickMarks.ConvertEnterToTabForDialogs = false;
            this.cbMinorTickMarks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMinorTickMarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMinorTickMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMinorTickMarks.Items.AddRange(new object[] {
            "None",
            "Inside",
            "Outside",
            "Cross"});
            this.cbMinorTickMarks.Location = new System.Drawing.Point(336, 8);
            this.cbMinorTickMarks.Name = "cbMinorTickMarks";
            this.cbMinorTickMarks.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMinorTickMarks.SeparatorMargin = 1;
            this.cbMinorTickMarks.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMinorTickMarks.SeparatorWidth = 1;
            this.cbMinorTickMarks.Size = new System.Drawing.Size(80, 21);
            this.cbMinorTickMarks.TabIndex = 4;
            this.cbMinorTickMarks.SelectedIndexChanged += new System.EventHandler(this.cbMinorTickMarks_SelectedIndexChanged);
            // 
            // chkVisible
            // 
            this.chkVisible.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkVisible.ForeColor = System.Drawing.Color.Black;
            this.chkVisible.Location = new System.Drawing.Point(24, 224);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(88, 24);
            this.chkVisible.TabIndex = 19;
            this.chkVisible.Text = "Visible";
            this.chkVisible.CheckedChanged += new System.EventHandler(this.chkVisible_CheckedChanged);
            // 
            // chkMargin
            // 
            this.chkMargin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMargin.ForeColor = System.Drawing.Color.Black;
            this.chkMargin.Location = new System.Drawing.Point(240, 224);
            this.chkMargin.Name = "chkMargin";
            this.chkMargin.Size = new System.Drawing.Size(60, 24);
            this.chkMargin.TabIndex = 21;
            this.chkMargin.Text = "Margin";
            this.chkMargin.CheckedChanged += new System.EventHandler(this.chkMargin_CheckedChanged);
            // 
            // chkReverse
            // 
            this.chkReverse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkReverse.ForeColor = System.Drawing.Color.Black;
            this.chkReverse.Location = new System.Drawing.Point(108, 248);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(120, 24);
            this.chkReverse.TabIndex = 23;
            this.chkReverse.Text = "Reverse Direction";
            this.chkReverse.CheckedChanged += new System.EventHandler(this.chkReverse_CheckedChanged);
            // 
            // chkInterlaced
            // 
            this.chkInterlaced.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkInterlaced.ForeColor = System.Drawing.Color.Black;
            this.chkInterlaced.Location = new System.Drawing.Point(240, 248);
            this.chkInterlaced.Name = "chkInterlaced";
            this.chkInterlaced.Size = new System.Drawing.Size(88, 24);
            this.chkInterlaced.TabIndex = 23;
            this.chkInterlaced.Text = "Interlaced";
            this.chkInterlaced.CheckedChanged += new System.EventHandler(this.chkInterlaced_CheckedChanged);
            // 
            // chkScalar
            // 
            this.chkScalar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkScalar.ForeColor = System.Drawing.Color.Black;
            this.chkScalar.Location = new System.Drawing.Point(24, 248);
            this.chkScalar.Name = "chkScalar";
            this.chkScalar.Size = new System.Drawing.Size(72, 24);
            this.chkScalar.TabIndex = 22;
            this.chkScalar.Text = "Scalar";
            this.chkScalar.CheckedChanged += new System.EventHandler(this.chkScalar_CheckedChanged);
            // 
            // chkLogScale
            // 
            this.chkLogScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkLogScale.ForeColor = System.Drawing.Color.Black;
            this.chkLogScale.Location = new System.Drawing.Point(108, 224);
            this.chkLogScale.Name = "chkLogScale";
            this.chkLogScale.Size = new System.Drawing.Size(120, 24);
            this.chkLogScale.TabIndex = 20;
            this.chkLogScale.Text = "Log Scale";
            this.chkLogScale.CheckedChanged += new System.EventHandler(this.chkLogScale_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMajorGLShow);
            this.groupBox1.Controls.Add(this.tbMajorGLWidth);
            this.groupBox1.Controls.Add(this.bMajorGLColor);
            this.groupBox1.Controls.Add(this.cbMajorGLColor);
            this.groupBox1.Controls.Add(this.cbMajorGLStyle);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 48);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Major Grid Lines";
            // 
            // chkMajorGLShow
            // 
            this.chkMajorGLShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMajorGLShow.ForeColor = System.Drawing.Color.Black;
            this.chkMajorGLShow.Location = new System.Drawing.Point(8, 14);
            this.chkMajorGLShow.Name = "chkMajorGLShow";
            this.chkMajorGLShow.Size = new System.Drawing.Size(56, 24);
            this.chkMajorGLShow.TabIndex = 0;
            this.chkMajorGLShow.Text = "Show";
            this.chkMajorGLShow.CheckedChanged += new System.EventHandler(this.chkMajorGLShow_CheckedChanged);
            // 
            // tbMajorGLWidth
            // 
            this.tbMajorGLWidth.AddX = 0;
            this.tbMajorGLWidth.AddY = 0;
            this.tbMajorGLWidth.AllowSpace = false;
            this.tbMajorGLWidth.BorderColor = System.Drawing.Color.LightGray;
            this.tbMajorGLWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMajorGLWidth.ChangeVisibility = false;
            this.tbMajorGLWidth.ChildControl = null;
            this.tbMajorGLWidth.ConvertEnterToTab = true;
            this.tbMajorGLWidth.ConvertEnterToTabForDialogs = false;
            this.tbMajorGLWidth.Decimals = 0;
            this.tbMajorGLWidth.DisplayList = new object[0];
            this.tbMajorGLWidth.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbMajorGLWidth.Location = new System.Drawing.Point(352, 16);
            this.tbMajorGLWidth.Name = "tbMajorGLWidth";
            this.tbMajorGLWidth.OnDropDownCloseFocus = true;
            this.tbMajorGLWidth.SelectType = 0;
            this.tbMajorGLWidth.Size = new System.Drawing.Size(40, 20);
            this.tbMajorGLWidth.TabIndex = 7;
            this.tbMajorGLWidth.UseValueForChildsVisibilty = false;
            this.tbMajorGLWidth.Value = true;
            this.tbMajorGLWidth.TextChanged += new System.EventHandler(this.tbMajorGLWidth_TextChanged);
            // 
            // bMajorGLColor
            // 
            this.bMajorGLColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMajorGLColor.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMajorGLColor.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMajorGLColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMajorGLColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMajorGLColor.Font = new System.Drawing.Font("Arial", 9F);
            this.bMajorGLColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMajorGLColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMajorGLColor.Location = new System.Drawing.Point(288, 16);
            this.bMajorGLColor.Name = "bMajorGLColor";
            this.bMajorGLColor.OverriddenSize = null;
            this.bMajorGLColor.Size = new System.Drawing.Size(24, 21);
            this.bMajorGLColor.TabIndex = 5;
            this.bMajorGLColor.Text = "...";
            this.bMajorGLColor.UseVisualStyleBackColor = true;
            this.bMajorGLColor.Click += new System.EventHandler(this.bMajorGLColor_Click);
            // 
            // cbMajorGLColor
            // 
            this.cbMajorGLColor.AutoAdjustItemHeight = false;
            this.cbMajorGLColor.BorderColor = System.Drawing.Color.LightGray;
            this.cbMajorGLColor.ConvertEnterToTabForDialogs = false;
            this.cbMajorGLColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMajorGLColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMajorGLColor.Location = new System.Drawing.Point(208, 16);
            this.cbMajorGLColor.Name = "cbMajorGLColor";
            this.cbMajorGLColor.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMajorGLColor.SeparatorMargin = 1;
            this.cbMajorGLColor.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMajorGLColor.SeparatorWidth = 1;
            this.cbMajorGLColor.Size = new System.Drawing.Size(72, 21);
            this.cbMajorGLColor.TabIndex = 4;
            this.cbMajorGLColor.SelectedIndexChanged += new System.EventHandler(this.cbMajorGLColor_SelectedIndexChanged);
            // 
            // cbMajorGLStyle
            // 
            this.cbMajorGLStyle.AutoAdjustItemHeight = false;
            this.cbMajorGLStyle.BorderColor = System.Drawing.Color.LightGray;
            this.cbMajorGLStyle.ConvertEnterToTabForDialogs = false;
            this.cbMajorGLStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMajorGLStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMajorGLStyle.Items.AddRange(new object[] {
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
            this.cbMajorGLStyle.Location = new System.Drawing.Point(96, 16);
            this.cbMajorGLStyle.Name = "cbMajorGLStyle";
            this.cbMajorGLStyle.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMajorGLStyle.SeparatorMargin = 1;
            this.cbMajorGLStyle.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMajorGLStyle.SeparatorWidth = 1;
            this.cbMajorGLStyle.Size = new System.Drawing.Size(72, 21);
            this.cbMajorGLStyle.TabIndex = 2;
            this.cbMajorGLStyle.SelectedIndexChanged += new System.EventHandler(this.cbMajorGLStyle_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(176, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Color";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(320, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Width";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(64, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Style";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkMinorGLShow);
            this.groupBox2.Controls.Add(this.tbMinorGLWidth);
            this.groupBox2.Controls.Add(this.bMinorGLColor);
            this.groupBox2.Controls.Add(this.cbMinorGLColor);
            this.groupBox2.Controls.Add(this.cbMinorGLStyle);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(16, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 48);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Minor Grid Lines";
            // 
            // chkMinorGLShow
            // 
            this.chkMinorGLShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMinorGLShow.ForeColor = System.Drawing.Color.Black;
            this.chkMinorGLShow.Location = new System.Drawing.Point(8, 14);
            this.chkMinorGLShow.Name = "chkMinorGLShow";
            this.chkMinorGLShow.Size = new System.Drawing.Size(56, 24);
            this.chkMinorGLShow.TabIndex = 0;
            this.chkMinorGLShow.Text = "Show";
            this.chkMinorGLShow.CheckedChanged += new System.EventHandler(this.chkMinorGLShow_CheckedChanged);
            // 
            // tbMinorGLWidth
            // 
            this.tbMinorGLWidth.AddX = 0;
            this.tbMinorGLWidth.AddY = 0;
            this.tbMinorGLWidth.AllowSpace = false;
            this.tbMinorGLWidth.BorderColor = System.Drawing.Color.LightGray;
            this.tbMinorGLWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMinorGLWidth.ChangeVisibility = false;
            this.tbMinorGLWidth.ChildControl = null;
            this.tbMinorGLWidth.ConvertEnterToTab = true;
            this.tbMinorGLWidth.ConvertEnterToTabForDialogs = false;
            this.tbMinorGLWidth.Decimals = 0;
            this.tbMinorGLWidth.DisplayList = new object[0];
            this.tbMinorGLWidth.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbMinorGLWidth.Location = new System.Drawing.Point(352, 16);
            this.tbMinorGLWidth.Name = "tbMinorGLWidth";
            this.tbMinorGLWidth.OnDropDownCloseFocus = true;
            this.tbMinorGLWidth.SelectType = 0;
            this.tbMinorGLWidth.Size = new System.Drawing.Size(40, 20);
            this.tbMinorGLWidth.TabIndex = 7;
            this.tbMinorGLWidth.UseValueForChildsVisibilty = false;
            this.tbMinorGLWidth.Value = true;
            this.tbMinorGLWidth.TextChanged += new System.EventHandler(this.tbMinorGLWidth_TextChanged);
            // 
            // bMinorGLColor
            // 
            this.bMinorGLColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMinorGLColor.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMinorGLColor.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMinorGLColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMinorGLColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMinorGLColor.Font = new System.Drawing.Font("Arial", 9F);
            this.bMinorGLColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMinorGLColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMinorGLColor.Location = new System.Drawing.Point(288, 16);
            this.bMinorGLColor.Name = "bMinorGLColor";
            this.bMinorGLColor.OverriddenSize = null;
            this.bMinorGLColor.Size = new System.Drawing.Size(24, 21);
            this.bMinorGLColor.TabIndex = 5;
            this.bMinorGLColor.Text = "...";
            this.bMinorGLColor.UseVisualStyleBackColor = true;
            this.bMinorGLColor.Click += new System.EventHandler(this.bMinorGLColor_Click);
            // 
            // cbMinorGLColor
            // 
            this.cbMinorGLColor.AutoAdjustItemHeight = false;
            this.cbMinorGLColor.BorderColor = System.Drawing.Color.LightGray;
            this.cbMinorGLColor.ConvertEnterToTabForDialogs = false;
            this.cbMinorGLColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMinorGLColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMinorGLColor.Location = new System.Drawing.Point(208, 16);
            this.cbMinorGLColor.Name = "cbMinorGLColor";
            this.cbMinorGLColor.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMinorGLColor.SeparatorMargin = 1;
            this.cbMinorGLColor.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMinorGLColor.SeparatorWidth = 1;
            this.cbMinorGLColor.Size = new System.Drawing.Size(72, 21);
            this.cbMinorGLColor.TabIndex = 4;
            this.cbMinorGLColor.SelectedIndexChanged += new System.EventHandler(this.cbMinorGLColor_SelectedIndexChanged);
            // 
            // cbMinorGLStyle
            // 
            this.cbMinorGLStyle.AutoAdjustItemHeight = false;
            this.cbMinorGLStyle.BorderColor = System.Drawing.Color.LightGray;
            this.cbMinorGLStyle.ConvertEnterToTabForDialogs = false;
            this.cbMinorGLStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMinorGLStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMinorGLStyle.Items.AddRange(new object[] {
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
            this.cbMinorGLStyle.Location = new System.Drawing.Point(96, 16);
            this.cbMinorGLStyle.Name = "cbMinorGLStyle";
            this.cbMinorGLStyle.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMinorGLStyle.SeparatorMargin = 1;
            this.cbMinorGLStyle.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMinorGLStyle.SeparatorWidth = 1;
            this.cbMinorGLStyle.Size = new System.Drawing.Size(72, 21);
            this.cbMinorGLStyle.TabIndex = 2;
            this.cbMinorGLStyle.SelectedIndexChanged += new System.EventHandler(this.cbMinorGLStyle_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(176, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Color";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(320, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Width";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(64, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Style";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "Major Interval";
            // 
            // tbMajorInterval
            // 
            this.tbMajorInterval.AddX = 0;
            this.tbMajorInterval.AddY = 0;
            this.tbMajorInterval.AllowSpace = false;
            this.tbMajorInterval.BorderColor = System.Drawing.Color.LightGray;
            this.tbMajorInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMajorInterval.ChangeVisibility = false;
            this.tbMajorInterval.ChildControl = null;
            this.tbMajorInterval.ConvertEnterToTab = true;
            this.tbMajorInterval.ConvertEnterToTabForDialogs = false;
            this.tbMajorInterval.Decimals = 0;
            this.tbMajorInterval.DisplayList = new object[0];
            this.tbMajorInterval.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbMajorInterval.Location = new System.Drawing.Point(104, 154);
            this.tbMajorInterval.Name = "tbMajorInterval";
            this.tbMajorInterval.OnDropDownCloseFocus = true;
            this.tbMajorInterval.SelectType = 0;
            this.tbMajorInterval.Size = new System.Drawing.Size(65, 20);
            this.tbMajorInterval.TabIndex = 8;
            this.tbMajorInterval.UseValueForChildsVisibilty = false;
            this.tbMajorInterval.Value = true;
            this.tbMajorInterval.TextChanged += new System.EventHandler(this.tbMajorInterval_TextChanged);
            // 
            // tbMinorInterval
            // 
            this.tbMinorInterval.AddX = 0;
            this.tbMinorInterval.AddY = 0;
            this.tbMinorInterval.AllowSpace = false;
            this.tbMinorInterval.BorderColor = System.Drawing.Color.LightGray;
            this.tbMinorInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMinorInterval.ChangeVisibility = false;
            this.tbMinorInterval.ChildControl = null;
            this.tbMinorInterval.ConvertEnterToTab = true;
            this.tbMinorInterval.ConvertEnterToTabForDialogs = false;
            this.tbMinorInterval.Decimals = 0;
            this.tbMinorInterval.DisplayList = new object[0];
            this.tbMinorInterval.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbMinorInterval.Location = new System.Drawing.Point(302, 154);
            this.tbMinorInterval.Name = "tbMinorInterval";
            this.tbMinorInterval.OnDropDownCloseFocus = true;
            this.tbMinorInterval.SelectType = 0;
            this.tbMinorInterval.Size = new System.Drawing.Size(65, 20);
            this.tbMinorInterval.TabIndex = 11;
            this.tbMinorInterval.UseValueForChildsVisibilty = false;
            this.tbMinorInterval.Value = true;
            this.tbMinorInterval.TextChanged += new System.EventHandler(this.tbMinorInterval_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(217, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Minor Interval";
            // 
            // tbMax
            // 
            this.tbMax.AddX = 0;
            this.tbMax.AddY = 0;
            this.tbMax.AllowSpace = false;
            this.tbMax.BorderColor = System.Drawing.Color.LightGray;
            this.tbMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMax.ChangeVisibility = false;
            this.tbMax.ChildControl = null;
            this.tbMax.ConvertEnterToTab = true;
            this.tbMax.ConvertEnterToTabForDialogs = false;
            this.tbMax.Decimals = 0;
            this.tbMax.DisplayList = new object[0];
            this.tbMax.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbMax.Location = new System.Drawing.Point(302, 184);
            this.tbMax.Name = "tbMax";
            this.tbMax.OnDropDownCloseFocus = true;
            this.tbMax.SelectType = 0;
            this.tbMax.Size = new System.Drawing.Size(65, 20);
            this.tbMax.TabIndex = 17;
            this.tbMax.UseValueForChildsVisibilty = false;
            this.tbMax.Value = true;
            this.tbMax.TextChanged += new System.EventHandler(this.tbMax_TextChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(216, 186);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 16);
            this.label11.TabIndex = 16;
            this.label11.Text = "Maximum Value";
            // 
            // tbMin
            // 
            this.tbMin.AddX = 0;
            this.tbMin.AddY = 0;
            this.tbMin.AllowSpace = false;
            this.tbMin.BorderColor = System.Drawing.Color.LightGray;
            this.tbMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMin.ChangeVisibility = false;
            this.tbMin.ChildControl = null;
            this.tbMin.ConvertEnterToTab = true;
            this.tbMin.ConvertEnterToTabForDialogs = false;
            this.tbMin.Decimals = 0;
            this.tbMin.DisplayList = new object[0];
            this.tbMin.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbMin.Location = new System.Drawing.Point(104, 184);
            this.tbMin.Name = "tbMin";
            this.tbMin.OnDropDownCloseFocus = true;
            this.tbMin.SelectType = 0;
            this.tbMin.Size = new System.Drawing.Size(65, 20);
            this.tbMin.TabIndex = 14;
            this.tbMin.UseValueForChildsVisibilty = false;
            this.tbMin.Value = true;
            this.tbMin.TextChanged += new System.EventHandler(this.tbMin_TextChanged);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(16, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 16);
            this.label12.TabIndex = 13;
            this.label12.Text = "Minimum Value";
            // 
            // bMinorIntervalExpr
            // 
            this.bMinorIntervalExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMinorIntervalExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMinorIntervalExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMinorIntervalExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMinorIntervalExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMinorIntervalExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMinorIntervalExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMinorIntervalExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMinorIntervalExpr.Location = new System.Drawing.Point(375, 154);
            this.bMinorIntervalExpr.Name = "bMinorIntervalExpr";
            this.bMinorIntervalExpr.OverriddenSize = null;
            this.bMinorIntervalExpr.Size = new System.Drawing.Size(22, 21);
            this.bMinorIntervalExpr.TabIndex = 12;
            this.bMinorIntervalExpr.Tag = "minorinterval";
            this.bMinorIntervalExpr.Text = "fx";
            this.bMinorIntervalExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMinorIntervalExpr.UseVisualStyleBackColor = true;
            this.bMinorIntervalExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bMajorIntervalExpr
            // 
            this.bMajorIntervalExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMajorIntervalExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMajorIntervalExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMajorIntervalExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMajorIntervalExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMajorIntervalExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMajorIntervalExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMajorIntervalExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMajorIntervalExpr.Location = new System.Drawing.Point(177, 154);
            this.bMajorIntervalExpr.Name = "bMajorIntervalExpr";
            this.bMajorIntervalExpr.OverriddenSize = null;
            this.bMajorIntervalExpr.Size = new System.Drawing.Size(22, 21);
            this.bMajorIntervalExpr.TabIndex = 9;
            this.bMajorIntervalExpr.Tag = "majorinterval";
            this.bMajorIntervalExpr.Text = "fx";
            this.bMajorIntervalExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMajorIntervalExpr.UseVisualStyleBackColor = true;
            this.bMajorIntervalExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bMinExpr
            // 
            this.bMinExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMinExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMinExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMinExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMinExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMinExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMinExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMinExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMinExpr.Location = new System.Drawing.Point(177, 184);
            this.bMinExpr.Name = "bMinExpr";
            this.bMinExpr.OverriddenSize = null;
            this.bMinExpr.Size = new System.Drawing.Size(22, 21);
            this.bMinExpr.TabIndex = 15;
            this.bMinExpr.Tag = "min";
            this.bMinExpr.Text = "fx";
            this.bMinExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMinExpr.UseVisualStyleBackColor = true;
            this.bMinExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bMaxExpr
            // 
            this.bMaxExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMaxExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMaxExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMaxExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMaxExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMaxExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMaxExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMaxExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMaxExpr.Location = new System.Drawing.Point(376, 184);
            this.bMaxExpr.Name = "bMaxExpr";
            this.bMaxExpr.OverriddenSize = null;
            this.bMaxExpr.Size = new System.Drawing.Size(22, 21);
            this.bMaxExpr.TabIndex = 18;
            this.bMaxExpr.Tag = "max";
            this.bMaxExpr.Text = "fx";
            this.bMaxExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMaxExpr.UseVisualStyleBackColor = true;
            this.bMaxExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // chkCanOmit
            // 
            this.chkCanOmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCanOmit.ForeColor = System.Drawing.Color.Black;
            this.chkCanOmit.Location = new System.Drawing.Point(334, 224);
            this.chkCanOmit.Name = "chkCanOmit";
            this.chkCanOmit.Size = new System.Drawing.Size(93, 48);
            this.chkCanOmit.TabIndex = 24;
            this.chkCanOmit.Text = "Can Omit Values on Truncation";
            this.chkCanOmit.CheckedChanged += new System.EventHandler(this.chkCanOmit_CheckedChanged);
            // 
            // chkMonth
            // 
            this.chkMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMonth.ForeColor = System.Drawing.Color.Black;
            this.chkMonth.Location = new System.Drawing.Point(24, 272);
            this.chkMonth.Name = "chkMonth";
            this.chkMonth.Size = new System.Drawing.Size(145, 24);
            this.chkMonth.TabIndex = 25;
            this.chkMonth.Text = "Month Category Scale";
            this.chkMonth.CheckedChanged += new System.EventHandler(this.chkMonth_CheckedChanged);
            // 
            // ChartAxisCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.chkMonth);
            this.Controls.Add(this.chkCanOmit);
            this.Controls.Add(this.bMaxExpr);
            this.Controls.Add(this.bMinExpr);
            this.Controls.Add(this.bMajorIntervalExpr);
            this.Controls.Add(this.bMinorIntervalExpr);
            this.Controls.Add(this.tbMax);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbMin);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbMinorInterval);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkLogScale);
            this.Controls.Add(this.chkScalar);
            this.Controls.Add(this.chkInterlaced);
            this.Controls.Add(this.chkReverse);
            this.Controls.Add(this.chkMargin);
            this.Controls.Add(this.chkVisible);
            this.Controls.Add(this.cbMinorTickMarks);
            this.Controls.Add(this.cbMajorTickMarks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMajorInterval);
            this.Controls.Add(this.label9);
            this.Name = "ChartAxisCtl";
            this.Size = new System.Drawing.Size(440, 303);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

			    fMonth = fVisible = fMajorTickMarks = fMargin=fReverse=fInterlaced=
				fMajorGLWidth=fMajorGLColor=fMajorGLStyle=
				fMinorGLWidth=fMinorGLColor=fMinorGLStyle=
				fMajorInterval= fMinorInterval=fMax=fMin=
				fMinorTickMarks=fScalar=fLogScale=fMajorGLShow=fMinorGLShow=fCanOmit=false;
		}

		public void ApplyChanges(XmlNode node)
		{
            if (fMonth)
            {
                _Draw.SetElement(node, "fyi:Month", this.chkMonth.Checked? "true" : "false");
            }
			if (fVisible)
			{
				_Draw.SetElement(node, "Visible", this.chkVisible.Checked? "true": "false");
			}
			if (fMajorTickMarks)
			{
				_Draw.SetElement(node, "MajorTickMarks", this.cbMajorTickMarks.Text);
			}
			if (fMargin)
			{
				_Draw.SetElement(node, "Margin", this.chkMargin.Checked? "true": "false");
			}
			if (fReverse)
			{
				_Draw.SetElement(node, "Reverse", this.chkReverse.Checked? "true": "false");
			}
			if (fInterlaced)
			{
				_Draw.SetElement(node, "Interlaced", this.chkInterlaced.Checked? "true": "false");
			}
			if (fMajorGLShow || fMajorGLWidth || fMajorGLColor || fMajorGLStyle)
			{
				ApplyGridLines(node, "MajorGridLines", chkMajorGLShow, cbMajorGLColor, cbMajorGLStyle, tbMajorGLWidth);
			}
			if (fMinorGLShow || fMinorGLWidth || fMinorGLColor || fMinorGLStyle)
			{
				ApplyGridLines(node, "MinorGridLines", chkMinorGLShow, cbMinorGLColor, cbMinorGLStyle, tbMinorGLWidth);
			}
			if (fMajorInterval)
			{
				_Draw.SetElement(node, "MajorInterval", this.tbMajorInterval.Text);
			}
			if (fMinorInterval)
			{
				_Draw.SetElement(node, "MinorInterval", this.tbMinorInterval.Text);
			}
			if (fMax)
			{
				_Draw.SetElement(node, "Max", this.tbMax.Text);
			}
			if (fMin)
			{
				_Draw.SetElement(node, "Min", this.tbMin.Text);
			}
			if (fMinorTickMarks)
			{
				_Draw.SetElement(node, "MinorTickMarks", this.cbMinorTickMarks.Text);
			}
			if (fScalar)
			{
				_Draw.SetElement(node, "Scalar", this.chkScalar.Checked? "true": "false");
			}
			if (fLogScale)
			{
				_Draw.SetElement(node, "LogScale", this.chkLogScale.Checked? "true": "false");
			}
            if (fCanOmit)
            {
                _Draw.SetElement(node, "fyi:CanOmit", this.chkCanOmit.Checked ? "true" : "false");
            }
        }

		private void ApplyGridLines(XmlNode node, string type, CheckBox show, ComboBox color, ComboBox style, TextBox width)
		{
			XmlNode m = _Draw.GetNamedChildNode(node, type);
			if (m == null)
			{
				m = _Draw.CreateElement(node, type, null);
			}

			_Draw.SetElement(m, "ShowGridLines", show.Checked? "true": "false");
			XmlNode st = _Draw.GetNamedChildNode(m, "Style");
			if (st == null)
				st = _Draw.CreateElement(m, "Style", null);

			XmlNode work = _Draw.GetNamedChildNode(st, "BorderColor");
			if (work == null)
				work = _Draw.CreateElement(st, "BorderColor", null);
			_Draw.SetElement(work, "Default", color.Text);

			work = _Draw.GetNamedChildNode(st, "BorderStyle");
			if (work == null)
				work = _Draw.CreateElement(st, "BorderStyle", null);
			_Draw.SetElement(work, "Default", style.Text);
			
			work = _Draw.GetNamedChildNode(st, "BorderWidth");
			if (work == null)
				work = _Draw.CreateElement(st, "BorderWidth", null);
			_Draw.SetElement(work, "Default", width.Text);
		}

		private void cbMajorTickMarks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMajorTickMarks = true;
		}

		private void cbMinorTickMarks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMinorTickMarks = true;
		}

		private void cbMajorGLStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMajorGLStyle = true;
		}

		private void cbMajorGLColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMajorGLColor = true;
		}

		private void tbMajorGLWidth_TextChanged(object sender, System.EventArgs e)
		{
			fMajorGLWidth = true;
		}

		private void cbMinorGLStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMinorGLStyle = true;
		}

		private void cbMinorGLColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMinorGLColor = true;
		}

		private void tbMinorGLWidth_TextChanged(object sender, System.EventArgs e)
		{
			fMinorGLWidth = true;
		}

		private void tbMajorInterval_TextChanged(object sender, System.EventArgs e)
		{
			fMajorInterval = true;
		}

		private void tbMinorInterval_TextChanged(object sender, System.EventArgs e)
		{
			fMinorInterval = true;
		}

		private void tbMin_TextChanged(object sender, System.EventArgs e)
		{
			fMin = true;
		}

		private void tbMax_TextChanged(object sender, System.EventArgs e)
		{
			fMax = true;
		}

        private void chkMonth_CheckedChanged(object sender, System.EventArgs e)
        {
            fMonth = true;
        }

		private void chkVisible_CheckedChanged(object sender, System.EventArgs e)
		{
			fVisible = true;
		}

		private void chkLogScale_CheckedChanged(object sender, System.EventArgs e)
		{
			fLogScale = true;
		}

        private void chkCanOmit_CheckedChanged(object sender, System.EventArgs e)
        {
            fCanOmit = true;
        }

		private void chkMargin_CheckedChanged(object sender, System.EventArgs e)
		{
			fMargin = true;
		}

		private void chkScalar_CheckedChanged(object sender, System.EventArgs e)
		{
			fScalar = true;
		}

		private void chkReverse_CheckedChanged(object sender, System.EventArgs e)
		{
			fReverse = true;
		}

		private void chkInterlaced_CheckedChanged(object sender, System.EventArgs e)
		{
			fInterlaced = true;
		}

		private void chkMajorGLShow_CheckedChanged(object sender, System.EventArgs e)
		{
			fMajorGLShow = true;
		}

		private void chkMinorGLShow_CheckedChanged(object sender, System.EventArgs e)
		{
			fMinorGLShow = true;
		}

		private void bMajorGLColor_Click(object sender, System.EventArgs e)
		{
			SetColor(this.cbMajorGLColor);		
		}

		private void bMinorGLColor_Click(object sender, System.EventArgs e)
		{
			SetColor(this.cbMinorGLColor);		
		}

		private void SetColor(ComboBox cbColor)
		{
			ColorDialog cd = new ColorDialog();
			cd.AnyColor = true;
			cd.FullOpen = true;
			
			cd.CustomColors = RdlDesigner.GetCustomColors();
			cd.Color = DesignerUtility.ColorFromHtml(cbColor.Text, System.Drawing.Color.Empty);

            try
            {
                if (cd.ShowDialog() != DialogResult.OK)
                    return;

                RdlDesigner.SetCustomColors(cd.CustomColors);
                cbColor.Text = ColorTranslator.ToHtml(cd.Color);
            }
            finally
            {
                cd.Dispose();
            }

			return;
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
				case "min":
					c = this.tbMin;
					break;
				case "max":
					c = this.tbMax;
					break;
				case "majorinterval":
					c = this.tbMajorInterval;
					break;
				case "minorinterval":
					c = this.tbMinorInterval;
					break;
			}

			if (c == null)
				return;

			XmlNode sNode = _ReportItems[0];

			DialogExprEditor ee = new DialogExprEditor(_Draw, c.Text, sNode, bColor);
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
