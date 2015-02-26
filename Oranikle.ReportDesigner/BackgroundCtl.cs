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
	internal class BackgroundCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		// flags for controlling whether syntax changed for a particular property
		private bool fEndColor, fBackColor, fGradient, fBackImage;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.StyledButton bBackColor;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label15;
		private Oranikle.Studio.Controls.StyledComboBox cbEndColor;
		private Oranikle.Studio.Controls.StyledComboBox cbBackColor;
		private Oranikle.Studio.Controls.StyledButton bEndColor;
        private Oranikle.Studio.Controls.StyledComboBox cbGradient;
		private Oranikle.Studio.Controls.StyledButton bGradient;
		private Oranikle.Studio.Controls.StyledButton bExprBackColor;
        private Oranikle.Studio.Controls.StyledButton bExprEndColor;
        private GroupBox groupBox2;
        private Oranikle.Studio.Controls.StyledButton bExternalExpr;
        private Oranikle.Studio.Controls.StyledButton bEmbeddedExpr;
        private Oranikle.Studio.Controls.StyledButton bMimeExpr;
        private Oranikle.Studio.Controls.StyledButton bDatabaseExpr;
        private Oranikle.Studio.Controls.StyledButton bEmbedded;
        private Oranikle.Studio.Controls.StyledButton bExternal;
        private Oranikle.Studio.Controls.CustomTextControl tbValueExternal;
        private Oranikle.Studio.Controls.StyledComboBox cbValueDatabase;
        private Oranikle.Studio.Controls.StyledComboBox cbMIMEType;
        private Oranikle.Studio.Controls.StyledComboBox cbValueEmbedded;
        private RadioButton rbDatabase;
        private RadioButton rbEmbedded;
        private RadioButton rbExternal;
        private RadioButton rbNone;
        private Oranikle.Studio.Controls.StyledComboBox cbRepeat;
        private Label label1;
        private Oranikle.Studio.Controls.StyledButton bRepeatExpr;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private string[] _names;

        internal BackgroundCtl(DesignXmlDraw dxDraw, string[] names, List<XmlNode> reportItems)
		{
			_ReportItems = reportItems;
			_Draw = dxDraw;
            _names = names;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues(_ReportItems[0]);			
		}

		private void InitValues(XmlNode node)
		{
            cbEndColor.Items.AddRange(StaticLists.ColorList);
            cbBackColor.Items.AddRange(StaticLists.ColorList);
            cbGradient.Items.AddRange(StaticLists.GradientList);

            if (_names != null)
            {
                node = _Draw.FindCreateNextInHierarchy(node, _names);
            }

            XmlNode sNode = _Draw.GetNamedChildNode(node, "Style");

			this.cbBackColor.Text = _Draw.GetElementValue(sNode, "BackgroundColor", "");
			this.cbEndColor.Text = _Draw.GetElementValue(sNode, "BackgroundGradientEndColor", "");
			this.cbGradient.Text = _Draw.GetElementValue(sNode, "BackgroundGradientType", "None");
			if (node.Name != "Chart")
			{   // only chart support gradients
				this.cbEndColor.Enabled = bExprEndColor.Enabled =
					cbGradient.Enabled = bGradient.Enabled = 
					this.bEndColor.Enabled = bExprEndColor.Enabled = false;
			}

            cbValueEmbedded.Items.AddRange(_Draw.ReportNames.EmbeddedImageNames);
            string[] flds = _Draw.GetReportItemDataRegionFields(node, true);
            if (flds != null)
                this.cbValueDatabase.Items.AddRange(flds);

            XmlNode iNode = _Draw.GetNamedChildNode(sNode, "BackgroundImage");
            if (iNode != null)
            {
                string source = _Draw.GetElementValue(iNode, "Source", "Embedded");
                string val = _Draw.GetElementValue(iNode, "Value", "");
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
                this.cbRepeat.Text = _Draw.GetElementValue(iNode, "BackgroundRepeat", "Repeat");
            }
            else
                this.rbNone.Checked = true;

            rbSource_CheckedChanged(null, null);

			// nothing has changed now
			fEndColor = fBackColor = fGradient = fBackImage = false;
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bRepeatExpr = new Oranikle.Studio.Controls.StyledButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.cbRepeat = new Oranikle.Studio.Controls.StyledComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bRepeatExpr);
            this.groupBox2.Controls.Add(this.rbNone);
            this.groupBox2.Controls.Add(this.cbRepeat);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.bExternalExpr);
            this.groupBox2.Controls.Add(this.bEmbeddedExpr);
            this.groupBox2.Controls.Add(this.bMimeExpr);
            this.groupBox2.Controls.Add(this.bDatabaseExpr);
            this.groupBox2.Controls.Add(this.bEmbedded);
            this.groupBox2.Controls.Add(this.bExternal);
            this.groupBox2.Controls.Add(this.tbValueExternal);
            this.groupBox2.Controls.Add(this.cbValueDatabase);
            this.groupBox2.Controls.Add(this.cbMIMEType);
            this.groupBox2.Controls.Add(this.cbValueEmbedded);
            this.groupBox2.Controls.Add(this.rbDatabase);
            this.groupBox2.Controls.Add(this.rbEmbedded);
            this.groupBox2.Controls.Add(this.rbExternal);
            this.groupBox2.Location = new System.Drawing.Point(16, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 219);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Background System.Drawing.Image Source";
            // 
            // bRepeatExpr
            // 
            this.bRepeatExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bRepeatExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bRepeatExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bRepeatExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bRepeatExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRepeatExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRepeatExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bRepeatExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRepeatExpr.Location = new System.Drawing.Point(213, 184);
            this.bRepeatExpr.Name = "bRepeatExpr";
            this.bRepeatExpr.OverriddenSize = null;
            this.bRepeatExpr.Size = new System.Drawing.Size(22, 21);
            this.bRepeatExpr.TabIndex = 16;
            this.bRepeatExpr.Tag = "repeat";
            this.bRepeatExpr.Text = "fx";
            this.bRepeatExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRepeatExpr.UseVisualStyleBackColor = true;
            // 
            // rbNone
            // 
            this.rbNone.Location = new System.Drawing.Point(6, 25);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(80, 24);
            this.rbNone.TabIndex = 15;
            this.rbNone.Text = "None";
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // cbRepeat
            // 
            this.cbRepeat.AutoAdjustItemHeight = false;
            this.cbRepeat.BorderColor = System.Drawing.Color.LightGray;
            this.cbRepeat.ConvertEnterToTabForDialogs = false;
            this.cbRepeat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbRepeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRepeat.Items.AddRange(new object[] {
            "Repeat",
            "NoRepeat",
            "RepeatX",
            "RepeatY"});
            this.cbRepeat.Location = new System.Drawing.Point(87, 184);
            this.cbRepeat.Name = "cbRepeat";
            this.cbRepeat.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbRepeat.SeparatorMargin = 1;
            this.cbRepeat.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbRepeat.SeparatorWidth = 1;
            this.cbRepeat.Size = new System.Drawing.Size(120, 21);
            this.cbRepeat.TabIndex = 14;
            this.cbRepeat.SelectedIndexChanged += new System.EventHandler(this.BackImage_Changed);
            this.cbRepeat.TextChanged += new System.EventHandler(this.BackImage_Changed);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Repeat";
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
            this.bExternalExpr.Location = new System.Drawing.Point(376, 56);
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
            this.bEmbeddedExpr.Location = new System.Drawing.Point(376, 88);
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
            this.bMimeExpr.Location = new System.Drawing.Point(182, 121);
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
            this.bDatabaseExpr.Location = new System.Drawing.Point(376, 152);
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
            this.bEmbedded.Location = new System.Drawing.Point(402, 88);
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
            this.bExternal.Location = new System.Drawing.Point(402, 56);
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
            this.tbValueExternal.Location = new System.Drawing.Point(86, 56);
            this.tbValueExternal.Name = "tbValueExternal";
            this.tbValueExternal.OnDropDownCloseFocus = true;
            this.tbValueExternal.SelectType = 0;
            this.tbValueExternal.Size = new System.Drawing.Size(284, 20);
            this.tbValueExternal.TabIndex = 6;
            this.tbValueExternal.UseValueForChildsVisibilty = false;
            this.tbValueExternal.Value = true;
            this.tbValueExternal.TextChanged += new System.EventHandler(this.BackImage_Changed);
            // 
            // cbValueDatabase
            // 
            this.cbValueDatabase.AutoAdjustItemHeight = false;
            this.cbValueDatabase.BorderColor = System.Drawing.Color.LightGray;
            this.cbValueDatabase.ConvertEnterToTabForDialogs = false;
            this.cbValueDatabase.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValueDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValueDatabase.Location = new System.Drawing.Point(86, 152);
            this.cbValueDatabase.Name = "cbValueDatabase";
            this.cbValueDatabase.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValueDatabase.SeparatorMargin = 1;
            this.cbValueDatabase.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValueDatabase.SeparatorWidth = 1;
            this.cbValueDatabase.Size = new System.Drawing.Size(284, 21);
            this.cbValueDatabase.TabIndex = 5;
            this.cbValueDatabase.SelectedIndexChanged += new System.EventHandler(this.BackImage_Changed);
            this.cbValueDatabase.TextChanged += new System.EventHandler(this.BackImage_Changed);
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
            this.cbMIMEType.Location = new System.Drawing.Point(86, 121);
            this.cbMIMEType.Name = "cbMIMEType";
            this.cbMIMEType.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbMIMEType.SeparatorMargin = 1;
            this.cbMIMEType.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbMIMEType.SeparatorWidth = 1;
            this.cbMIMEType.Size = new System.Drawing.Size(88, 21);
            this.cbMIMEType.TabIndex = 4;
            this.cbMIMEType.Text = "image/jpeg";
            this.cbMIMEType.SelectedIndexChanged += new System.EventHandler(this.BackImage_Changed);
            this.cbMIMEType.TextChanged += new System.EventHandler(this.BackImage_Changed);
            // 
            // cbValueEmbedded
            // 
            this.cbValueEmbedded.AutoAdjustItemHeight = false;
            this.cbValueEmbedded.BorderColor = System.Drawing.Color.LightGray;
            this.cbValueEmbedded.ConvertEnterToTabForDialogs = false;
            this.cbValueEmbedded.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValueEmbedded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValueEmbedded.Location = new System.Drawing.Point(86, 88);
            this.cbValueEmbedded.Name = "cbValueEmbedded";
            this.cbValueEmbedded.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValueEmbedded.SeparatorMargin = 1;
            this.cbValueEmbedded.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValueEmbedded.SeparatorWidth = 1;
            this.cbValueEmbedded.Size = new System.Drawing.Size(284, 21);
            this.cbValueEmbedded.TabIndex = 3;
            this.cbValueEmbedded.SelectedIndexChanged += new System.EventHandler(this.BackImage_Changed);
            this.cbValueEmbedded.TextChanged += new System.EventHandler(this.BackImage_Changed);
            // 
            // rbDatabase
            // 
            this.rbDatabase.Location = new System.Drawing.Point(6, 119);
            this.rbDatabase.Name = "rbDatabase";
            this.rbDatabase.Size = new System.Drawing.Size(80, 24);
            this.rbDatabase.TabIndex = 2;
            this.rbDatabase.Text = "Database";
            this.rbDatabase.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbEmbedded
            // 
            this.rbEmbedded.Location = new System.Drawing.Point(6, 86);
            this.rbEmbedded.Name = "rbEmbedded";
            this.rbEmbedded.Size = new System.Drawing.Size(80, 24);
            this.rbEmbedded.TabIndex = 1;
            this.rbEmbedded.Text = "Embedded";
            this.rbEmbedded.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbExternal
            // 
            this.rbExternal.Location = new System.Drawing.Point(6, 54);
            this.rbExternal.Name = "rbExternal";
            this.rbExternal.Size = new System.Drawing.Size(80, 24);
            this.rbExternal.TabIndex = 0;
            this.rbExternal.Text = "External";
            this.rbExternal.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // BackgroundCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BackgroundCtl";
            this.Size = new System.Drawing.Size(472, 351);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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

			// nothing has changed now
            fEndColor = fBackColor = fGradient = fBackImage = false;
		}

		private void bColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog cd = new ColorDialog();
			cd.AnyColor = true;
			cd.FullOpen = true;
			cd.CustomColors = RdlDesigner.GetCustomColors();

            try
            {
                if (cd.ShowDialog() != DialogResult.OK)
                    return;

                RdlDesigner.SetCustomColors(cd.CustomColors);
                if (sender == this.bEndColor)
                    cbEndColor.Text = ColorTranslator.ToHtml(cd.Color);
                else if (sender == this.bBackColor)
                    cbBackColor.Text = ColorTranslator.ToHtml(cd.Color);
            }
            finally
            {
                cd.Dispose();
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

        private void BackImage_Changed(object sender, System.EventArgs e)
        {
            fBackImage = true;
        }

        private void rbSource_CheckedChanged(object sender, System.EventArgs e)
        {
            fBackImage = true;
            this.cbValueDatabase.Enabled = this.cbMIMEType.Enabled =
                this.bDatabaseExpr.Enabled = this.rbDatabase.Checked;
            this.cbValueEmbedded.Enabled = this.bEmbeddedExpr.Enabled =
                this.bEmbedded.Enabled = this.rbEmbedded.Checked;
            this.tbValueExternal.Enabled = this.bExternalExpr.Enabled =
                this.bExternal.Enabled = this.rbExternal.Checked;
        }
		
		private void ApplyChanges(XmlNode rNode)
		{
            if (_names != null)
            {
                rNode = _Draw.FindCreateNextInHierarchy(rNode, _names);
            }
            
            XmlNode xNode = _Draw.GetNamedChildNode(rNode, "Style");

			if (fEndColor)
			{ _Draw.SetElement(xNode, "BackgroundGradientEndColor", cbEndColor.Text); }
			if (fBackColor)
			{ _Draw.SetElement(xNode, "BackgroundColor", cbBackColor.Text); }
			if (fGradient)
			{ _Draw.SetElement(xNode, "BackgroundGradientType", cbGradient.Text); }
            if (fBackImage)
            {
                _Draw.RemoveElement(xNode, "BackgroundImage");
                if (!rbNone.Checked)
                {
                    XmlNode bi = _Draw.CreateElement(xNode, "BackgroundImage", null);
                    if (rbDatabase.Checked)
                    {
                        _Draw.SetElement(bi, "Source", "Database");
                        _Draw.SetElement(bi, "Value", cbValueDatabase.Text);
                        _Draw.SetElement(bi, "MIMEType", cbMIMEType.Text);
                    }
                    else if (rbExternal.Checked)
                    {
                        _Draw.SetElement(bi, "Source", "External");
                        _Draw.SetElement(bi, "Value", tbValueExternal.Text);
                    }
                    else if (rbEmbedded.Checked)
                    {
                        _Draw.SetElement(bi, "Source", "Embedded");
                        _Draw.SetElement(bi, "Value", cbValueEmbedded.Text);
                    }
                    _Draw.SetElement(bi, "BackgroundRepeat", cbRepeat.Text);
                }
            }
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

                // Populate the EmbeddedImage names
                cbValueEmbedded.Items.Clear();
                cbValueEmbedded.Items.AddRange(_Draw.ReportNames.EmbeddedImageNames);
            }
            finally
            {
                dlgEI.Dispose();
            }
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
                case "database":
                    c = cbValueDatabase;
                    break;
                case "embedded":
                    c = cbValueEmbedded;
                    break;
                case "external":
                    c = tbValueExternal;
                    break;
                case "repeat":
                    c = cbRepeat;
                    break;
                case "mime":
                    c = cbMIMEType;
                    break;
            }

			if (c == null)
				return;

			XmlNode sNode = _ReportItems[0];

			DialogExprEditor ee = new DialogExprEditor(_Draw, c.Text, sNode, bColor);
			DialogResult dr = ee.ShowDialog();
			if (dr == DialogResult.OK)
				c.Text = ee.Expression;
			return;
		}

	}
}
