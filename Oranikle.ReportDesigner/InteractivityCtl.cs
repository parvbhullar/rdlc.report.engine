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
using System.IO;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for StyleCtl.
	/// </summary>
	internal class InteractivityCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
        private List<DrillParameter> _DrillParameters;
		// flags for controlling whether syntax changed for a particular property
		private bool fBookmark, fAction, fHidden, fToggle;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox grpBoxVisibility;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbBookmark;
		private System.Windows.Forms.RadioButton rbHyperlink;
		private System.Windows.Forms.RadioButton rbBookmarkLink;
		private System.Windows.Forms.RadioButton rbDrillthrough;
		private Oranikle.Studio.Controls.CustomTextControl tbHyperlink;
		private Oranikle.Studio.Controls.CustomTextControl tbBookmarkLink;
		private Oranikle.Studio.Controls.CustomTextControl tbDrillthrough;
		private Oranikle.Studio.Controls.StyledButton bParameters;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.CustomTextControl tbHidden;
		private Oranikle.Studio.Controls.StyledComboBox cbToggle;
		private System.Windows.Forms.RadioButton rbNoAction;
		private Oranikle.Studio.Controls.StyledButton bDrillthrough;
		private Oranikle.Studio.Controls.StyledButton bHidden;
		private Oranikle.Studio.Controls.StyledButton bBookmarkLink;
		private Oranikle.Studio.Controls.StyledButton bHyperlink;
		private Oranikle.Studio.Controls.StyledButton bBookmark;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal InteractivityCtl(DesignXmlDraw dxDraw, List<XmlNode> reportItems)
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
			
			tbBookmark.Text = _Draw.GetElementValue(node, "Bookmark", "");
			// Handle Action definition
			XmlNode aNode = _Draw.GetNamedChildNode(node, "Action");
			if (aNode == null)
				rbNoAction.Checked = true;
			else
			{
				XmlNode vLink = _Draw.GetNamedChildNode(aNode, "Hyperlink");
				if (vLink != null)
				{	// Hyperlink specified
					rbHyperlink.Checked = true;
					tbHyperlink.Text = vLink.InnerText;
				}
				else
				{
					vLink = _Draw.GetNamedChildNode(aNode, "Drillthrough");
					if (vLink != null)
					{	// Drillthrough specified
						rbDrillthrough.Checked = true;
						tbDrillthrough.Text =  _Draw.GetElementValue(vLink, "ReportName", "");
                        _DrillParameters = new List<DrillParameter>();
						XmlNode pNodes = _Draw.GetNamedChildNode(vLink, "Parameters");
						if (pNodes != null)
						{
							foreach (XmlNode pNode in pNodes.ChildNodes)
							{
								if (pNode.Name != "Parameter")
									continue;
								string name = _Draw.GetElementAttribute(pNode, "Name", "");
								string pvalue = _Draw.GetElementValue(pNode, "Value", "");
								string omit = _Draw.GetElementValue(pNode, "Omit", "false");
								DrillParameter dp = new DrillParameter(name, pvalue, omit);
								_DrillParameters.Add(dp);
							}
						}
					}
					else
					{	
						vLink = _Draw.GetNamedChildNode(aNode, "BookmarkLink");
						if (vLink != null)
						{	// BookmarkLink specified
							rbBookmarkLink.Checked = true;
							this.tbBookmarkLink.Text = vLink.InnerText;
						}
					}
				}
			}
			
			// Handle Visiblity definition
			XmlNode visNode = _Draw.GetNamedChildNode(node, "Visibility");
			if (visNode != null)
			{
				XmlNode hNode = _Draw.GetNamedChildNode(node, "Visibility");
				this.tbHidden.Text = _Draw.GetElementValue(visNode, "Hidden", "");
				this.cbToggle.Text = _Draw.GetElementValue(visNode, "ToggleItem", "");
			}
			IEnumerable list = _Draw.GetReportItems("//Textbox");
			if (list != null)
			{
				foreach (XmlNode tNode in list)
				{
					XmlAttribute name = tNode.Attributes["Name"];
					if (name != null && name.Value != null && name.Value.Length > 0)
						cbToggle.Items.Add(name.Value);
				}
			}
			// nothing has changed now
			fBookmark = fAction = fHidden = fToggle = false;
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
            this.grpBoxVisibility = new System.Windows.Forms.GroupBox();
            this.bHidden = new Oranikle.Studio.Controls.StyledButton();
            this.cbToggle = new Oranikle.Studio.Controls.StyledComboBox();
            this.tbHidden = new Oranikle.Studio.Controls.CustomTextControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bBookmarkLink = new Oranikle.Studio.Controls.StyledButton();
            this.bHyperlink = new Oranikle.Studio.Controls.StyledButton();
            this.rbNoAction = new System.Windows.Forms.RadioButton();
            this.bParameters = new Oranikle.Studio.Controls.StyledButton();
            this.bDrillthrough = new Oranikle.Studio.Controls.StyledButton();
            this.tbDrillthrough = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbBookmarkLink = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbHyperlink = new Oranikle.Studio.Controls.CustomTextControl();
            this.rbDrillthrough = new System.Windows.Forms.RadioButton();
            this.rbBookmarkLink = new System.Windows.Forms.RadioButton();
            this.rbHyperlink = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBookmark = new Oranikle.Studio.Controls.CustomTextControl();
            this.bBookmark = new Oranikle.Studio.Controls.StyledButton();
            this.grpBoxVisibility.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxVisibility
            // 
            this.grpBoxVisibility.Controls.Add(this.bHidden);
            this.grpBoxVisibility.Controls.Add(this.cbToggle);
            this.grpBoxVisibility.Controls.Add(this.tbHidden);
            this.grpBoxVisibility.Controls.Add(this.label3);
            this.grpBoxVisibility.Controls.Add(this.label2);
            this.grpBoxVisibility.Location = new System.Drawing.Point(8, 152);
            this.grpBoxVisibility.Name = "grpBoxVisibility";
            this.grpBoxVisibility.Size = new System.Drawing.Size(432, 80);
            this.grpBoxVisibility.TabIndex = 1;
            this.grpBoxVisibility.TabStop = false;
            this.grpBoxVisibility.Text = "Visibility";
            // 
            // bHidden
            // 
            this.bHidden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bHidden.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bHidden.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bHidden.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bHidden.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bHidden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bHidden.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHidden.Location = new System.Drawing.Point(400, 24);
            this.bHidden.Name = "bHidden";
            this.bHidden.OverriddenSize = null;
            this.bHidden.Size = new System.Drawing.Size(22, 21);
            this.bHidden.TabIndex = 1;
            this.bHidden.Tag = "visibility";
            this.bHidden.Text = "fx";
            this.bHidden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHidden.UseVisualStyleBackColor = true;
            this.bHidden.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // cbToggle
            // 
            this.cbToggle.AutoAdjustItemHeight = false;
            this.cbToggle.BorderColor = System.Drawing.Color.LightGray;
            this.cbToggle.ConvertEnterToTabForDialogs = false;
            this.cbToggle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbToggle.Location = new System.Drawing.Point(168, 48);
            this.cbToggle.Name = "cbToggle";
            this.cbToggle.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbToggle.SeparatorMargin = 1;
            this.cbToggle.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbToggle.SeparatorWidth = 1;
            this.cbToggle.Size = new System.Drawing.Size(152, 21);
            this.cbToggle.TabIndex = 2;
            this.cbToggle.SelectedIndexChanged += new System.EventHandler(this.cbToggle_SelectedIndexChanged);
            this.cbToggle.TextChanged += new System.EventHandler(this.cbToggle_SelectedIndexChanged);
            // 
            // tbHidden
            // 
            this.tbHidden.AddX = 0;
            this.tbHidden.AddY = 0;
            this.tbHidden.AllowSpace = false;
            this.tbHidden.BorderColor = System.Drawing.Color.LightGray;
            this.tbHidden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHidden.ChangeVisibility = false;
            this.tbHidden.ChildControl = null;
            this.tbHidden.ConvertEnterToTab = true;
            this.tbHidden.ConvertEnterToTabForDialogs = false;
            this.tbHidden.Decimals = 0;
            this.tbHidden.DisplayList = new object[0];
            this.tbHidden.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbHidden.Location = new System.Drawing.Point(168, 24);
            this.tbHidden.Name = "tbHidden";
            this.tbHidden.OnDropDownCloseFocus = true;
            this.tbHidden.SelectType = 0;
            this.tbHidden.Size = new System.Drawing.Size(224, 20);
            this.tbHidden.TabIndex = 0;
            this.tbHidden.UseValueForChildsVisibilty = false;
            this.tbHidden.Value = true;
            this.tbHidden.TextChanged += new System.EventHandler(this.tbHidden_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Toggle Item (Textbox name)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Hidden (initial visibility)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bBookmarkLink);
            this.groupBox1.Controls.Add(this.bHyperlink);
            this.groupBox1.Controls.Add(this.rbNoAction);
            this.groupBox1.Controls.Add(this.bParameters);
            this.groupBox1.Controls.Add(this.bDrillthrough);
            this.groupBox1.Controls.Add(this.tbDrillthrough);
            this.groupBox1.Controls.Add(this.tbBookmarkLink);
            this.groupBox1.Controls.Add(this.tbHyperlink);
            this.groupBox1.Controls.Add(this.rbDrillthrough);
            this.groupBox1.Controls.Add(this.rbBookmarkLink);
            this.groupBox1.Controls.Add(this.rbHyperlink);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action";
            // 
            // bBookmarkLink
            // 
            this.bBookmarkLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bBookmarkLink.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bBookmarkLink.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bBookmarkLink.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bBookmarkLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBookmarkLink.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bBookmarkLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bBookmarkLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBookmarkLink.Location = new System.Drawing.Point(400, 74);
            this.bBookmarkLink.Name = "bBookmarkLink";
            this.bBookmarkLink.OverriddenSize = null;
            this.bBookmarkLink.Size = new System.Drawing.Size(22, 21);
            this.bBookmarkLink.TabIndex = 3;
            this.bBookmarkLink.Tag = "bookmarklink";
            this.bBookmarkLink.Text = "fx";
            this.bBookmarkLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBookmarkLink.UseVisualStyleBackColor = true;
            this.bBookmarkLink.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bHyperlink
            // 
            this.bHyperlink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bHyperlink.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bHyperlink.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bHyperlink.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bHyperlink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bHyperlink.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bHyperlink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bHyperlink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHyperlink.Location = new System.Drawing.Point(400, 44);
            this.bHyperlink.Name = "bHyperlink";
            this.bHyperlink.OverriddenSize = null;
            this.bHyperlink.Size = new System.Drawing.Size(22, 21);
            this.bHyperlink.TabIndex = 1;
            this.bHyperlink.Tag = "hyperlink";
            this.bHyperlink.Text = "fx";
            this.bHyperlink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHyperlink.UseVisualStyleBackColor = true;
            this.bHyperlink.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // rbNoAction
            // 
            this.rbNoAction.Location = new System.Drawing.Point(16, 16);
            this.rbNoAction.Name = "rbNoAction";
            this.rbNoAction.Size = new System.Drawing.Size(104, 24);
            this.rbNoAction.TabIndex = 5;
            this.rbNoAction.Text = "None";
            this.rbNoAction.CheckedChanged += new System.EventHandler(this.rbAction_CheckedChanged);
            // 
            // bParameters
            // 
            this.bParameters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bParameters.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bParameters.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bParameters.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParameters.Font = new System.Drawing.Font("Arial", 9F);
            this.bParameters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bParameters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bParameters.Location = new System.Drawing.Point(344, 104);
            this.bParameters.Name = "bParameters";
            this.bParameters.OverriddenSize = null;
            this.bParameters.Size = new System.Drawing.Size(80, 21);
            this.bParameters.TabIndex = 6;
            this.bParameters.Text = "Parameters...";
            this.bParameters.UseVisualStyleBackColor = true;
            this.bParameters.Click += new System.EventHandler(this.bParameters_Click);
            // 
            // bDrillthrough
            // 
            this.bDrillthrough.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDrillthrough.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDrillthrough.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDrillthrough.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDrillthrough.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDrillthrough.Font = new System.Drawing.Font("Arial", 9F);
            this.bDrillthrough.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDrillthrough.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDrillthrough.Location = new System.Drawing.Point(312, 104);
            this.bDrillthrough.Name = "bDrillthrough";
            this.bDrillthrough.OverriddenSize = null;
            this.bDrillthrough.Size = new System.Drawing.Size(24, 21);
            this.bDrillthrough.TabIndex = 5;
            this.bDrillthrough.Text = "...";
            this.bDrillthrough.UseVisualStyleBackColor = true;
            this.bDrillthrough.Click += new System.EventHandler(this.bDrillthrough_Click);
            // 
            // tbDrillthrough
            // 
            this.tbDrillthrough.AddX = 0;
            this.tbDrillthrough.AddY = 0;
            this.tbDrillthrough.AllowSpace = false;
            this.tbDrillthrough.BorderColor = System.Drawing.Color.LightGray;
            this.tbDrillthrough.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDrillthrough.ChangeVisibility = false;
            this.tbDrillthrough.ChildControl = null;
            this.tbDrillthrough.ConvertEnterToTab = true;
            this.tbDrillthrough.ConvertEnterToTabForDialogs = false;
            this.tbDrillthrough.Decimals = 0;
            this.tbDrillthrough.DisplayList = new object[0];
            this.tbDrillthrough.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDrillthrough.Location = new System.Drawing.Point(128, 104);
            this.tbDrillthrough.Name = "tbDrillthrough";
            this.tbDrillthrough.OnDropDownCloseFocus = true;
            this.tbDrillthrough.SelectType = 0;
            this.tbDrillthrough.Size = new System.Drawing.Size(176, 20);
            this.tbDrillthrough.TabIndex = 4;
            this.tbDrillthrough.UseValueForChildsVisibilty = false;
            this.tbDrillthrough.Value = true;
            this.tbDrillthrough.TextChanged += new System.EventHandler(this.tbAction_TextChanged);
            // 
            // tbBookmarkLink
            // 
            this.tbBookmarkLink.AddX = 0;
            this.tbBookmarkLink.AddY = 0;
            this.tbBookmarkLink.AllowSpace = false;
            this.tbBookmarkLink.BorderColor = System.Drawing.Color.LightGray;
            this.tbBookmarkLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBookmarkLink.ChangeVisibility = false;
            this.tbBookmarkLink.ChildControl = null;
            this.tbBookmarkLink.ConvertEnterToTab = true;
            this.tbBookmarkLink.ConvertEnterToTabForDialogs = false;
            this.tbBookmarkLink.Decimals = 0;
            this.tbBookmarkLink.DisplayList = new object[0];
            this.tbBookmarkLink.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbBookmarkLink.Location = new System.Drawing.Point(128, 74);
            this.tbBookmarkLink.Name = "tbBookmarkLink";
            this.tbBookmarkLink.OnDropDownCloseFocus = true;
            this.tbBookmarkLink.SelectType = 0;
            this.tbBookmarkLink.Size = new System.Drawing.Size(264, 20);
            this.tbBookmarkLink.TabIndex = 2;
            this.tbBookmarkLink.UseValueForChildsVisibilty = false;
            this.tbBookmarkLink.Value = true;
            this.tbBookmarkLink.TextChanged += new System.EventHandler(this.tbAction_TextChanged);
            // 
            // tbHyperlink
            // 
            this.tbHyperlink.AddX = 0;
            this.tbHyperlink.AddY = 0;
            this.tbHyperlink.AllowSpace = false;
            this.tbHyperlink.BorderColor = System.Drawing.Color.LightGray;
            this.tbHyperlink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHyperlink.ChangeVisibility = false;
            this.tbHyperlink.ChildControl = null;
            this.tbHyperlink.ConvertEnterToTab = true;
            this.tbHyperlink.ConvertEnterToTabForDialogs = false;
            this.tbHyperlink.Decimals = 0;
            this.tbHyperlink.DisplayList = new object[0];
            this.tbHyperlink.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbHyperlink.Location = new System.Drawing.Point(128, 44);
            this.tbHyperlink.Name = "tbHyperlink";
            this.tbHyperlink.OnDropDownCloseFocus = true;
            this.tbHyperlink.SelectType = 0;
            this.tbHyperlink.Size = new System.Drawing.Size(264, 20);
            this.tbHyperlink.TabIndex = 0;
            this.tbHyperlink.UseValueForChildsVisibilty = false;
            this.tbHyperlink.Value = true;
            this.tbHyperlink.TextChanged += new System.EventHandler(this.tbAction_TextChanged);
            // 
            // rbDrillthrough
            // 
            this.rbDrillthrough.Location = new System.Drawing.Point(16, 100);
            this.rbDrillthrough.Name = "rbDrillthrough";
            this.rbDrillthrough.Size = new System.Drawing.Size(104, 24);
            this.rbDrillthrough.TabIndex = 2;
            this.rbDrillthrough.Text = "Drill Through";
            this.rbDrillthrough.CheckedChanged += new System.EventHandler(this.rbAction_CheckedChanged);
            // 
            // rbBookmarkLink
            // 
            this.rbBookmarkLink.Location = new System.Drawing.Point(16, 72);
            this.rbBookmarkLink.Name = "rbBookmarkLink";
            this.rbBookmarkLink.Size = new System.Drawing.Size(104, 24);
            this.rbBookmarkLink.TabIndex = 1;
            this.rbBookmarkLink.Text = "Bookmark Link";
            this.rbBookmarkLink.CheckedChanged += new System.EventHandler(this.rbAction_CheckedChanged);
            // 
            // rbHyperlink
            // 
            this.rbHyperlink.Location = new System.Drawing.Point(16, 44);
            this.rbHyperlink.Name = "rbHyperlink";
            this.rbHyperlink.Size = new System.Drawing.Size(104, 24);
            this.rbHyperlink.TabIndex = 0;
            this.rbHyperlink.Text = "Hyperlink";
            this.rbHyperlink.CheckedChanged += new System.EventHandler(this.rbAction_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bookmark";
            // 
            // tbBookmark
            // 
            this.tbBookmark.AddX = 0;
            this.tbBookmark.AddY = 0;
            this.tbBookmark.AllowSpace = false;
            this.tbBookmark.BorderColor = System.Drawing.Color.LightGray;
            this.tbBookmark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBookmark.ChangeVisibility = false;
            this.tbBookmark.ChildControl = null;
            this.tbBookmark.ConvertEnterToTab = true;
            this.tbBookmark.ConvertEnterToTabForDialogs = false;
            this.tbBookmark.Decimals = 0;
            this.tbBookmark.DisplayList = new object[0];
            this.tbBookmark.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbBookmark.Location = new System.Drawing.Point(88, 255);
            this.tbBookmark.Name = "tbBookmark";
            this.tbBookmark.OnDropDownCloseFocus = true;
            this.tbBookmark.SelectType = 0;
            this.tbBookmark.Size = new System.Drawing.Size(312, 20);
            this.tbBookmark.TabIndex = 3;
            this.tbBookmark.UseValueForChildsVisibilty = false;
            this.tbBookmark.Value = true;
            // 
            // bBookmark
            // 
            this.bBookmark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bBookmark.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bBookmark.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bBookmark.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bBookmark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBookmark.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bBookmark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bBookmark.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBookmark.Location = new System.Drawing.Point(408, 255);
            this.bBookmark.Name = "bBookmark";
            this.bBookmark.OverriddenSize = null;
            this.bBookmark.Size = new System.Drawing.Size(22, 21);
            this.bBookmark.TabIndex = 4;
            this.bBookmark.Tag = "bookmark";
            this.bBookmark.Text = "fx";
            this.bBookmark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBookmark.UseVisualStyleBackColor = true;
            this.bBookmark.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // InteractivityCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.bBookmark);
            this.Controls.Add(this.tbBookmark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBoxVisibility);
            this.Name = "InteractivityCtl";
            this.Size = new System.Drawing.Size(472, 288);
            this.grpBoxVisibility.ResumeLayout(false);
            this.grpBoxVisibility.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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

			// nothing has changed now
			fBookmark = fAction = fHidden = fToggle = false;
		}

		private void ApplyChanges(XmlNode rNode)
		{
			if (fBookmark)
				_Draw.SetElement(rNode, "Bookmark", tbBookmark.Text);

			if (fHidden || fToggle)
			{
				XmlNode visNode = _Draw.SetElement(rNode, "Visibility", null);

				if (fHidden)
					_Draw.SetElement(visNode, "Hidden", tbHidden.Text); 
				if (fToggle)
					_Draw.SetElement(visNode, "ToggleItem", this.cbToggle.Text); 
			}

			if (fAction)
			{
				XmlNode aNode;
				if (this.rbHyperlink.Checked)
				{
					aNode = _Draw.SetElement(rNode, "Action", null);
					_Draw.RemoveElement(aNode, "Drillthrough");	
					_Draw.RemoveElement(aNode, "BookmarkLink");	
					_Draw.SetElement(aNode, "Hyperlink", tbHyperlink.Text); 
				}
				else if (this.rbDrillthrough.Checked)
				{
					aNode = _Draw.SetElement(rNode, "Action", null);
					_Draw.RemoveElement(aNode, "Hyperlink");	
					_Draw.RemoveElement(aNode, "BookmarkLink");	
					XmlNode dNode = _Draw.SetElement(aNode, "Drillthrough", null);
					_Draw.SetElement(dNode, "ReportName", tbDrillthrough.Text); 
					// Now do parameters
					_Draw.RemoveElement(dNode, "Parameters");	// Get rid of prior values
					if (this._DrillParameters != null && _DrillParameters.Count > 0)
					{
						XmlNode pNodes = _Draw.SetElement(dNode, "Parameters", null);
						foreach (DrillParameter dp in _DrillParameters)
						{
							XmlNode p = _Draw.SetElement(pNodes, "Parameter", null);
							_Draw.SetElementAttribute(p, "Name", dp.ParameterName);
							_Draw.SetElement(p, "Value", dp.ParameterValue);
							if (dp.ParameterOmit != null && dp.ParameterOmit.Length > 0)
								_Draw.SetElement(p, "Omit", dp.ParameterOmit);
						}
					}
				}
				else if (this.rbBookmarkLink.Checked)
				{
					aNode = _Draw.SetElement(rNode, "Action", null);
					_Draw.RemoveElement(aNode, "Drillthrough");	
					_Draw.RemoveElement(aNode, "Hyperlink");	
					_Draw.SetElement(aNode, "BookmarkLink", tbBookmarkLink.Text); 
				}
				else	// assume no action
				{
					_Draw.RemoveElement(rNode, "Action");	
				}
			}
		}

		private void tbBookmark_TextChanged(object sender, System.EventArgs e)
		{
			fBookmark = true;
		}

		private void rbAction_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.rbHyperlink.Checked)
			{
				tbHyperlink.Enabled = bHyperlink.Enabled = true;
				tbBookmarkLink.Enabled = bBookmarkLink.Enabled = false;
				tbDrillthrough.Enabled = false;
				bDrillthrough.Enabled = false;
				bParameters.Enabled = false;

			}
			else if (this.rbDrillthrough.Checked)
			{
				tbHyperlink.Enabled = bHyperlink.Enabled = false;
				tbBookmarkLink.Enabled = bBookmarkLink.Enabled = false;
				tbDrillthrough.Enabled = true;
				bDrillthrough.Enabled = true;
				bParameters.Enabled = true;
			}
			else if (this.rbBookmarkLink.Checked)
			{
				tbHyperlink.Enabled = bHyperlink.Enabled = false;
				tbBookmarkLink.Enabled = bBookmarkLink.Enabled = true;
				tbDrillthrough.Enabled = false;
				bDrillthrough.Enabled = false;
				bParameters.Enabled = false;
			}
			else	// assume no action
			{
				tbHyperlink.Enabled = bHyperlink.Enabled = false;
				tbBookmarkLink.Enabled = bBookmarkLink.Enabled = false;
				tbDrillthrough.Enabled = false;
				bDrillthrough.Enabled = false;
				bParameters.Enabled = false;
			}
			fAction = true;
		}

		private void tbAction_TextChanged(object sender, System.EventArgs e)
		{
			fAction = true;
		}

		private void tbHidden_TextChanged(object sender, System.EventArgs e)
		{
			fHidden = true;
		}

		private void cbToggle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fToggle = true;
		}

		private void bDrillthrough_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Report files (*.rdl)|*.rdl" +
                "|All files (*.*)|*.*";
			ofd.FilterIndex = 1;
			ofd.FileName = "*.rdl";

			ofd.Title = "Specify Report File Name";
			ofd.DefaultExt = "rdl";
			ofd.AddExtension = true;
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string file = Path.GetFileNameWithoutExtension(ofd.FileName);

                    tbDrillthrough.Text = file;
                }
            }
            finally
            {
                ofd.Dispose();
            }
		}

		private void bParameters_Click(object sender, System.EventArgs e)
		{
			DrillParametersDialog dpd = new DrillParametersDialog(this.tbDrillthrough.Text, _DrillParameters);
            try
            {
                if (dpd.ShowDialog(this) != DialogResult.OK)
                    return;
                tbDrillthrough.Text = dpd.DrillthroughReport;
                _DrillParameters = dpd.DrillParameters;
                fAction = true;
            }
            finally
            {
                dpd.Dispose();
            }
		}

		private void bExpr_Click(object sender, System.EventArgs e)
		{
			Button b = sender as Button;
			if (b == null)
				return;
			Control c = null;
			switch (b.Tag as string)
			{
				case "bookmark":
					c = tbBookmark;
 					break;
				case "bookmarklink":
					c = tbBookmarkLink;
 					break;
				case "hyperlink":
					c = tbHyperlink;
                    break;
				case "visibility":
					c = tbHidden;
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
                {
                    c.Text = ee.Expression;
                    if ((string)(b.Tag) == "bookmark")
                        fBookmark = true;
                    else
                        fAction = true;
                }
            }
            finally
            {
                ee.Dispose();
            }
			return;
		}

	}
}
