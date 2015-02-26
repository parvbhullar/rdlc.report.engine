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
using System.Text;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for StyleCtl.
	/// </summary>
	internal class ReportParameterCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		private DesignXmlDraw _Draw;
		private Oranikle.Studio.Controls.StyledButton bParmDown;
		private Oranikle.Studio.Controls.StyledButton bParmUp;
		private Oranikle.Studio.Controls.CustomTextControl tbParmValidValues;
		private Oranikle.Studio.Controls.StyledCheckBox ckbParmAllowBlank;
		private Oranikle.Studio.Controls.CustomTextControl tbParmPrompt;
		private System.Windows.Forms.Label lParmPrompt;
		private Oranikle.Studio.Controls.StyledComboBox cbParmType;
		private System.Windows.Forms.Label lParmType;
		private Oranikle.Studio.Controls.CustomTextControl tbParmName;
		private System.Windows.Forms.Label lParmName;
		private Oranikle.Studio.Controls.StyledButton bRemove;
		private Oranikle.Studio.Controls.StyledButton bAdd;
		private System.Windows.Forms.ListBox lbParameters;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbDataSet;
		private System.Windows.Forms.RadioButton rbValues;
		private Oranikle.Studio.Controls.StyledComboBox cbValidDataSets;
		private Oranikle.Studio.Controls.StyledComboBox cbValidFields;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.StyledComboBox cbValidDisplayField;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private Oranikle.Studio.Controls.StyledComboBox cbDefaultValueField;
		private Oranikle.Studio.Controls.StyledComboBox cbDefaultDataSets;
		private System.Windows.Forms.RadioButton rbDefaultValues;
		private System.Windows.Forms.RadioButton rbDefaultDataSetName;
		private Oranikle.Studio.Controls.StyledCheckBox ckbParmAllowNull;
		private Oranikle.Studio.Controls.StyledButton bDefaultValues;
		private Oranikle.Studio.Controls.CustomTextControl tbParmDefaultValue;
        private Oranikle.Studio.Controls.StyledButton bValidValues;
        private Oranikle.Studio.Controls.StyledCheckBox ckbParmMultiValue;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal ReportParameterCtl(DesignXmlDraw dxDraw)
		{
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			// Populate datasets combos
			object[] datasets = _Draw.DataSetNames;
			this.cbDefaultDataSets.Items.AddRange(datasets);
			this.cbValidDataSets.Items.AddRange(datasets);

			XmlNode rNode = _Draw.GetReportNode();
			XmlNode rpsNode = _Draw.GetNamedChildNode(rNode, "ReportParameters");
			if (rpsNode == null)
				return;
			foreach (XmlNode repNode in rpsNode)
			{	
				XmlAttribute nAttr = repNode.Attributes["Name"];
				if (nAttr == null)	// shouldn't really happen
					continue;
				ReportParm repParm = new ReportParm(nAttr.Value);
				repParm.DataType = _Draw.GetElementValue(repNode, "DataType", "String");
				// Get default values
				InitDefaultValues(repNode, repParm);

				string nullable  = _Draw.GetElementValue(repNode, "Nullable", "false");
				repParm.AllowNull = (nullable.ToLower() == "true");
				string allowBlank  = _Draw.GetElementValue(repNode, "AllowBlank", "false");
				repParm.AllowBlank = (allowBlank.ToLower() == "true");
                string mvalue = _Draw.GetElementValue(repNode, "MultiValue", "false");
                repParm.MultiValue = (mvalue.ToLower() == "true");
                repParm.Prompt = _Draw.GetElementValue(repNode, "Prompt", "");

				InitValidValues(repNode, repParm);

				this.lbParameters.Items.Add(repParm);
			}
			if (lbParameters.Items.Count > 0)
				lbParameters.SelectedIndex = 0;
		}

		void InitDefaultValues(XmlNode reportParameterNode, ReportParm repParm)
		{
			repParm.Default = true;
			XmlNode dfNode = _Draw.GetNamedChildNode(reportParameterNode, "DefaultValue");
			if (dfNode == null)
				return;

			XmlNode vNodes = _Draw.GetNamedChildNode(dfNode, "Values");
			if (vNodes != null)
			{
				List<string> al = new List<string>();
				foreach (XmlNode v in vNodes.ChildNodes)
				{
					if (v.InnerText.Length <= 0)
						continue;
					al.Add(v.InnerText);
				}
				if (al.Count >= 1)
					repParm.DefaultValue  = al;
			}
			XmlNode dsNodes = _Draw.GetNamedChildNode(dfNode, "DataSetReference");
			if (dsNodes != null)
			{
				repParm.Default = false;
				repParm.DefaultDSRDataSetName = _Draw.GetElementValue(dsNodes, "DataSetName", "");
				repParm.DefaultDSRValueField = _Draw.GetElementValue(dsNodes, "ValueField", "");
			}
		}

		void InitValidValues(XmlNode reportParameterNode, ReportParm repParm)
		{
			repParm.Valid = true;
			XmlNode vvsNode = _Draw.GetNamedChildNode(reportParameterNode, "ValidValues");
			if (vvsNode == null)
				return;

			XmlNode vNodes = _Draw.GetNamedChildNode(vvsNode, "ParameterValues");
			if (vNodes != null)
			{
                List<ParameterValueItem> pvs = new List<ParameterValueItem>();
				foreach (XmlNode v in vNodes.ChildNodes)
				{
					if (v.Name != "ParameterValue")
						continue;
					XmlNode pv = _Draw.GetNamedChildNode(v, "Value");
					if (pv == null)
						continue;
					if (pv == null || pv.InnerText.Length <= 0)
						continue;
					ParameterValueItem pvi = new ParameterValueItem();
					pvi.Value = pv.InnerText;
					pvi.Label = _Draw.GetElementValue(v, "Label", null);
					pvs.Add(pvi);
				}
				if (pvs.Count > 0)
				{
					repParm.ValidValues = pvs;
				}
			}
			XmlNode dsNodes = _Draw.GetNamedChildNode(vvsNode, "DataSetReference");
			if (dsNodes != null)
			{
				repParm.Valid = false;
				repParm.ValidValuesDSRDataSetName = _Draw.GetElementValue(dsNodes, "DataSetName", "");
				repParm.ValidValuesDSRValueField = _Draw.GetElementValue(dsNodes, "ValueField", "");
				repParm.ValidValuesDSRLabelField = _Draw.GetElementValue(dsNodes, "LabelField", "");
			}
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
            this.bParmDown = new Oranikle.Studio.Controls.StyledButton();
            this.bParmUp = new Oranikle.Studio.Controls.StyledButton();
            this.tbParmValidValues = new Oranikle.Studio.Controls.CustomTextControl();
            this.ckbParmAllowBlank = new Oranikle.Studio.Controls.StyledCheckBox();
            this.tbParmPrompt = new Oranikle.Studio.Controls.CustomTextControl();
            this.lParmPrompt = new System.Windows.Forms.Label();
            this.cbParmType = new Oranikle.Studio.Controls.StyledComboBox();
            this.lParmType = new System.Windows.Forms.Label();
            this.tbParmName = new Oranikle.Studio.Controls.CustomTextControl();
            this.lParmName = new System.Windows.Forms.Label();
            this.bRemove = new Oranikle.Studio.Controls.StyledButton();
            this.bAdd = new Oranikle.Studio.Controls.StyledButton();
            this.lbParameters = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bValidValues = new Oranikle.Studio.Controls.StyledButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cbValidDisplayField = new Oranikle.Studio.Controls.StyledComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbValidFields = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbValidDataSets = new Oranikle.Studio.Controls.StyledComboBox();
            this.rbValues = new System.Windows.Forms.RadioButton();
            this.rbDataSet = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbParmDefaultValue = new Oranikle.Studio.Controls.CustomTextControl();
            this.bDefaultValues = new Oranikle.Studio.Controls.StyledButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDefaultValueField = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbDefaultDataSets = new Oranikle.Studio.Controls.StyledComboBox();
            this.rbDefaultValues = new System.Windows.Forms.RadioButton();
            this.rbDefaultDataSetName = new System.Windows.Forms.RadioButton();
            this.ckbParmAllowNull = new Oranikle.Studio.Controls.StyledCheckBox();
            this.ckbParmMultiValue = new Oranikle.Studio.Controls.StyledCheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bParmDown
            // 
            this.bParmDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bParmDown.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bParmDown.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bParmDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bParmDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParmDown.Font = new System.Drawing.Font("Wingdings", 7.25F, System.Drawing.FontStyle.Bold);
            this.bParmDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bParmDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bParmDown.Location = new System.Drawing.Point(128, 40);
            this.bParmDown.Name = "bParmDown";
            this.bParmDown.OverriddenSize = null;
            this.bParmDown.Size = new System.Drawing.Size(20, 21);
            this.bParmDown.TabIndex = 4;
            this.bParmDown.Text = "";
            this.bParmDown.UseVisualStyleBackColor = true;
            this.bParmDown.Click += new System.EventHandler(this.bParmDown_Click);
            // 
            // bParmUp
            // 
            this.bParmUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bParmUp.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bParmUp.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bParmUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bParmUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParmUp.Font = new System.Drawing.Font("Wingdings", 7.25F, System.Drawing.FontStyle.Bold);
            this.bParmUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bParmUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bParmUp.Location = new System.Drawing.Point(128, 8);
            this.bParmUp.Name = "bParmUp";
            this.bParmUp.OverriddenSize = null;
            this.bParmUp.Size = new System.Drawing.Size(20, 21);
            this.bParmUp.TabIndex = 3;
            this.bParmUp.Text = "";
            this.bParmUp.UseVisualStyleBackColor = true;
            this.bParmUp.Click += new System.EventHandler(this.bParmUp_Click);
            // 
            // tbParmValidValues
            // 
            this.tbParmValidValues.AddX = 0;
            this.tbParmValidValues.AddY = 0;
            this.tbParmValidValues.AllowSpace = false;
            this.tbParmValidValues.BorderColor = System.Drawing.Color.LightGray;
            this.tbParmValidValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbParmValidValues.ChangeVisibility = false;
            this.tbParmValidValues.ChildControl = null;
            this.tbParmValidValues.ConvertEnterToTab = true;
            this.tbParmValidValues.ConvertEnterToTabForDialogs = false;
            this.tbParmValidValues.Decimals = 0;
            this.tbParmValidValues.DisplayList = new object[0];
            this.tbParmValidValues.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbParmValidValues.Location = new System.Drawing.Point(72, 16);
            this.tbParmValidValues.Name = "tbParmValidValues";
            this.tbParmValidValues.OnDropDownCloseFocus = true;
            this.tbParmValidValues.ReadOnly = true;
            this.tbParmValidValues.SelectType = 0;
            this.tbParmValidValues.Size = new System.Drawing.Size(328, 20);
            this.tbParmValidValues.TabIndex = 1;
            this.tbParmValidValues.UseValueForChildsVisibilty = false;
            this.tbParmValidValues.Value = true;
            // 
            // ckbParmAllowBlank
            // 
            this.ckbParmAllowBlank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckbParmAllowBlank.ForeColor = System.Drawing.Color.Black;
            this.ckbParmAllowBlank.Location = new System.Drawing.Point(222, 72);
            this.ckbParmAllowBlank.Name = "ckbParmAllowBlank";
            this.ckbParmAllowBlank.Size = new System.Drawing.Size(148, 24);
            this.ckbParmAllowBlank.TabIndex = 9;
            this.ckbParmAllowBlank.Text = "Allow blank (strings only)";
            this.ckbParmAllowBlank.CheckedChanged += new System.EventHandler(this.ckbParmAllowBlank_CheckedChanged);
            // 
            // tbParmPrompt
            // 
            this.tbParmPrompt.AddX = 0;
            this.tbParmPrompt.AddY = 0;
            this.tbParmPrompt.AllowSpace = false;
            this.tbParmPrompt.BorderColor = System.Drawing.Color.LightGray;
            this.tbParmPrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbParmPrompt.ChangeVisibility = false;
            this.tbParmPrompt.ChildControl = null;
            this.tbParmPrompt.ConvertEnterToTab = true;
            this.tbParmPrompt.ConvertEnterToTabForDialogs = false;
            this.tbParmPrompt.Decimals = 0;
            this.tbParmPrompt.DisplayList = new object[0];
            this.tbParmPrompt.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbParmPrompt.Location = new System.Drawing.Point(208, 40);
            this.tbParmPrompt.Name = "tbParmPrompt";
            this.tbParmPrompt.OnDropDownCloseFocus = true;
            this.tbParmPrompt.SelectType = 0;
            this.tbParmPrompt.Size = new System.Drawing.Size(240, 20);
            this.tbParmPrompt.TabIndex = 7;
            this.tbParmPrompt.UseValueForChildsVisibilty = false;
            this.tbParmPrompt.Value = true;
            this.tbParmPrompt.TextChanged += new System.EventHandler(this.tbParmPrompt_TextChanged);
            // 
            // lParmPrompt
            // 
            this.lParmPrompt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lParmPrompt.Location = new System.Drawing.Point(160, 40);
            this.lParmPrompt.Name = "lParmPrompt";
            this.lParmPrompt.Size = new System.Drawing.Size(48, 16);
            this.lParmPrompt.TabIndex = 23;
            this.lParmPrompt.Text = "Prompt";
            this.lParmPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbParmType
            // 
            this.cbParmType.AutoAdjustItemHeight = false;
            this.cbParmType.BorderColor = System.Drawing.Color.LightGray;
            this.cbParmType.ConvertEnterToTabForDialogs = false;
            this.cbParmType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbParmType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParmType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbParmType.Items.AddRange(new object[] {
            "Boolean",
            "DateTime",
            "Integer",
            "Float",
            "String"});
            this.cbParmType.Location = new System.Drawing.Point(368, 16);
            this.cbParmType.Name = "cbParmType";
            this.cbParmType.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbParmType.SeparatorMargin = 1;
            this.cbParmType.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbParmType.SeparatorWidth = 1;
            this.cbParmType.Size = new System.Drawing.Size(80, 21);
            this.cbParmType.TabIndex = 6;
            this.cbParmType.SelectedIndexChanged += new System.EventHandler(this.cbParmType_SelectedIndexChanged);
            // 
            // lParmType
            // 
            this.lParmType.Location = new System.Drawing.Point(304, 16);
            this.lParmType.Name = "lParmType";
            this.lParmType.Size = new System.Drawing.Size(56, 23);
            this.lParmType.TabIndex = 21;
            this.lParmType.Text = "Datatype";
            this.lParmType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbParmName
            // 
            this.tbParmName.AddX = 0;
            this.tbParmName.AddY = 0;
            this.tbParmName.AllowSpace = false;
            this.tbParmName.BorderColor = System.Drawing.Color.LightGray;
            this.tbParmName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbParmName.ChangeVisibility = false;
            this.tbParmName.ChildControl = null;
            this.tbParmName.ConvertEnterToTab = true;
            this.tbParmName.ConvertEnterToTabForDialogs = false;
            this.tbParmName.Decimals = 0;
            this.tbParmName.DisplayList = new object[0];
            this.tbParmName.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbParmName.Location = new System.Drawing.Point(208, 16);
            this.tbParmName.Name = "tbParmName";
            this.tbParmName.OnDropDownCloseFocus = true;
            this.tbParmName.SelectType = 0;
            this.tbParmName.Size = new System.Drawing.Size(104, 20);
            this.tbParmName.TabIndex = 5;
            this.tbParmName.UseValueForChildsVisibilty = false;
            this.tbParmName.Value = true;
            this.tbParmName.TextChanged += new System.EventHandler(this.tbParmName_TextChanged);
            // 
            // lParmName
            // 
            this.lParmName.Location = new System.Drawing.Point(160, 16);
            this.lParmName.Name = "lParmName";
            this.lParmName.Size = new System.Drawing.Size(40, 16);
            this.lParmName.TabIndex = 19;
            this.lParmName.Text = "Name";
            this.lParmName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bRemove
            // 
            this.bRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bRemove.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bRemove.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bRemove.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRemove.Font = new System.Drawing.Font("Arial", 9F);
            this.bRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRemove.Location = new System.Drawing.Point(68, 74);
            this.bRemove.Name = "bRemove";
            this.bRemove.OverriddenSize = null;
            this.bRemove.Size = new System.Drawing.Size(54, 21);
            this.bRemove.TabIndex = 2;
            this.bRemove.Text = "Remove";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // bAdd
            // 
            this.bAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bAdd.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bAdd.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bAdd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd.Font = new System.Drawing.Font("Arial", 9F);
            this.bAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bAdd.Location = new System.Drawing.Point(8, 74);
            this.bAdd.Name = "bAdd";
            this.bAdd.OverriddenSize = null;
            this.bAdd.Size = new System.Drawing.Size(54, 21);
            this.bAdd.TabIndex = 1;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // lbParameters
            // 
            this.lbParameters.Location = new System.Drawing.Point(8, 8);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(112, 56);
            this.lbParameters.TabIndex = 0;
            this.lbParameters.SelectedIndexChanged += new System.EventHandler(this.lbParameters_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bValidValues);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbValidDisplayField);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbValidFields);
            this.groupBox1.Controls.Add(this.cbValidDataSets);
            this.groupBox1.Controls.Add(this.rbValues);
            this.groupBox1.Controls.Add(this.rbDataSet);
            this.groupBox1.Controls.Add(this.tbParmValidValues);
            this.groupBox1.Location = new System.Drawing.Point(8, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 96);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Valid Values";
            // 
            // bValidValues
            // 
            this.bValidValues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bValidValues.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bValidValues.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bValidValues.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bValidValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValidValues.Font = new System.Drawing.Font("Arial", 9F);
            this.bValidValues.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bValidValues.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bValidValues.Location = new System.Drawing.Point(408, 16);
            this.bValidValues.Name = "bValidValues";
            this.bValidValues.OverriddenSize = null;
            this.bValidValues.Size = new System.Drawing.Size(24, 21);
            this.bValidValues.TabIndex = 2;
            this.bValidValues.Text = "...";
            this.bValidValues.UseVisualStyleBackColor = true;
            this.bValidValues.Click += new System.EventHandler(this.bValidValues_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(240, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "Display Field";
            // 
            // cbValidDisplayField
            // 
            this.cbValidDisplayField.AutoAdjustItemHeight = false;
            this.cbValidDisplayField.BorderColor = System.Drawing.Color.LightGray;
            this.cbValidDisplayField.ConvertEnterToTabForDialogs = false;
            this.cbValidDisplayField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValidDisplayField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValidDisplayField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValidDisplayField.Location = new System.Drawing.Point(312, 70);
            this.cbValidDisplayField.Name = "cbValidDisplayField";
            this.cbValidDisplayField.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValidDisplayField.SeparatorMargin = 1;
            this.cbValidDisplayField.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValidDisplayField.SeparatorWidth = 1;
            this.cbValidDisplayField.Size = new System.Drawing.Size(112, 21);
            this.cbValidDisplayField.TabIndex = 6;
            this.cbValidDisplayField.SelectedIndexChanged += new System.EventHandler(this.cbValidDisplayField_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(240, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Value Field";
            // 
            // cbValidFields
            // 
            this.cbValidFields.AutoAdjustItemHeight = false;
            this.cbValidFields.BorderColor = System.Drawing.Color.LightGray;
            this.cbValidFields.ConvertEnterToTabForDialogs = false;
            this.cbValidFields.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValidFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValidFields.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValidFields.Location = new System.Drawing.Point(312, 44);
            this.cbValidFields.Name = "cbValidFields";
            this.cbValidFields.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValidFields.SeparatorMargin = 1;
            this.cbValidFields.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValidFields.SeparatorWidth = 1;
            this.cbValidFields.Size = new System.Drawing.Size(112, 21);
            this.cbValidFields.TabIndex = 5;
            this.cbValidFields.SelectedIndexChanged += new System.EventHandler(this.cbValidFields_SelectedIndexChanged);
            // 
            // cbValidDataSets
            // 
            this.cbValidDataSets.AutoAdjustItemHeight = false;
            this.cbValidDataSets.BorderColor = System.Drawing.Color.LightGray;
            this.cbValidDataSets.ConvertEnterToTabForDialogs = false;
            this.cbValidDataSets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbValidDataSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValidDataSets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbValidDataSets.Location = new System.Drawing.Point(112, 44);
            this.cbValidDataSets.Name = "cbValidDataSets";
            this.cbValidDataSets.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbValidDataSets.SeparatorMargin = 1;
            this.cbValidDataSets.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbValidDataSets.SeparatorWidth = 1;
            this.cbValidDataSets.Size = new System.Drawing.Size(112, 21);
            this.cbValidDataSets.TabIndex = 4;
            this.cbValidDataSets.SelectedIndexChanged += new System.EventHandler(this.cbValidDataSets_SelectedIndexChanged);
            // 
            // rbValues
            // 
            this.rbValues.Location = new System.Drawing.Point(8, 14);
            this.rbValues.Name = "rbValues";
            this.rbValues.Size = new System.Drawing.Size(64, 24);
            this.rbValues.TabIndex = 0;
            this.rbValues.Text = "Values";
            this.rbValues.CheckedChanged += new System.EventHandler(this.rbValues_CheckedChanged);
            // 
            // rbDataSet
            // 
            this.rbDataSet.Location = new System.Drawing.Point(8, 42);
            this.rbDataSet.Name = "rbDataSet";
            this.rbDataSet.Size = new System.Drawing.Size(104, 24);
            this.rbDataSet.TabIndex = 3;
            this.rbDataSet.Text = "Data Set Name";
            this.rbDataSet.CheckedChanged += new System.EventHandler(this.rbDataSet_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbParmDefaultValue);
            this.groupBox2.Controls.Add(this.bDefaultValues);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbDefaultValueField);
            this.groupBox2.Controls.Add(this.cbDefaultDataSets);
            this.groupBox2.Controls.Add(this.rbDefaultValues);
            this.groupBox2.Controls.Add(this.rbDefaultDataSetName);
            this.groupBox2.Location = new System.Drawing.Point(8, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 72);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Values";
            // 
            // tbParmDefaultValue
            // 
            this.tbParmDefaultValue.AddX = 0;
            this.tbParmDefaultValue.AddY = 0;
            this.tbParmDefaultValue.AllowSpace = false;
            this.tbParmDefaultValue.BorderColor = System.Drawing.Color.LightGray;
            this.tbParmDefaultValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbParmDefaultValue.ChangeVisibility = false;
            this.tbParmDefaultValue.ChildControl = null;
            this.tbParmDefaultValue.ConvertEnterToTab = true;
            this.tbParmDefaultValue.ConvertEnterToTabForDialogs = false;
            this.tbParmDefaultValue.Decimals = 0;
            this.tbParmDefaultValue.DisplayList = new object[0];
            this.tbParmDefaultValue.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbParmDefaultValue.Location = new System.Drawing.Point(72, 16);
            this.tbParmDefaultValue.Name = "tbParmDefaultValue";
            this.tbParmDefaultValue.OnDropDownCloseFocus = true;
            this.tbParmDefaultValue.ReadOnly = true;
            this.tbParmDefaultValue.SelectType = 0;
            this.tbParmDefaultValue.Size = new System.Drawing.Size(328, 20);
            this.tbParmDefaultValue.TabIndex = 1;
            this.tbParmDefaultValue.UseValueForChildsVisibilty = false;
            this.tbParmDefaultValue.Value = true;
            // 
            // bDefaultValues
            // 
            this.bDefaultValues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDefaultValues.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDefaultValues.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDefaultValues.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDefaultValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDefaultValues.Font = new System.Drawing.Font("Arial", 9F);
            this.bDefaultValues.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDefaultValues.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDefaultValues.Location = new System.Drawing.Point(407, 16);
            this.bDefaultValues.Name = "bDefaultValues";
            this.bDefaultValues.OverriddenSize = null;
            this.bDefaultValues.Size = new System.Drawing.Size(23, 21);
            this.bDefaultValues.TabIndex = 2;
            this.bDefaultValues.Text = "...";
            this.bDefaultValues.UseVisualStyleBackColor = true;
            this.bDefaultValues.Click += new System.EventHandler(this.bDefaultValues_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(240, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 23);
            this.label4.TabIndex = 35;
            this.label4.Text = "Value Field";
            // 
            // cbDefaultValueField
            // 
            this.cbDefaultValueField.AutoAdjustItemHeight = false;
            this.cbDefaultValueField.BorderColor = System.Drawing.Color.LightGray;
            this.cbDefaultValueField.ConvertEnterToTabForDialogs = false;
            this.cbDefaultValueField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDefaultValueField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDefaultValueField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDefaultValueField.Location = new System.Drawing.Point(312, 43);
            this.cbDefaultValueField.Name = "cbDefaultValueField";
            this.cbDefaultValueField.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDefaultValueField.SeparatorMargin = 1;
            this.cbDefaultValueField.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDefaultValueField.SeparatorWidth = 1;
            this.cbDefaultValueField.Size = new System.Drawing.Size(112, 21);
            this.cbDefaultValueField.TabIndex = 5;
            this.cbDefaultValueField.SelectedIndexChanged += new System.EventHandler(this.cbDefaultValueField_SelectedIndexChanged);
            // 
            // cbDefaultDataSets
            // 
            this.cbDefaultDataSets.AutoAdjustItemHeight = false;
            this.cbDefaultDataSets.BorderColor = System.Drawing.Color.LightGray;
            this.cbDefaultDataSets.ConvertEnterToTabForDialogs = false;
            this.cbDefaultDataSets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDefaultDataSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDefaultDataSets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDefaultDataSets.Location = new System.Drawing.Point(112, 43);
            this.cbDefaultDataSets.Name = "cbDefaultDataSets";
            this.cbDefaultDataSets.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDefaultDataSets.SeparatorMargin = 1;
            this.cbDefaultDataSets.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDefaultDataSets.SeparatorWidth = 1;
            this.cbDefaultDataSets.Size = new System.Drawing.Size(112, 21);
            this.cbDefaultDataSets.TabIndex = 4;
            this.cbDefaultDataSets.SelectedIndexChanged += new System.EventHandler(this.cbDefaultDataSets_SelectedIndexChanged);
            // 
            // rbDefaultValues
            // 
            this.rbDefaultValues.Location = new System.Drawing.Point(8, 14);
            this.rbDefaultValues.Name = "rbDefaultValues";
            this.rbDefaultValues.Size = new System.Drawing.Size(64, 24);
            this.rbDefaultValues.TabIndex = 0;
            this.rbDefaultValues.Text = "Values";
            this.rbDefaultValues.CheckedChanged += new System.EventHandler(this.rbDefaultValues_CheckedChanged);
            // 
            // rbDefaultDataSetName
            // 
            this.rbDefaultDataSetName.Location = new System.Drawing.Point(8, 41);
            this.rbDefaultDataSetName.Name = "rbDefaultDataSetName";
            this.rbDefaultDataSetName.Size = new System.Drawing.Size(104, 24);
            this.rbDefaultDataSetName.TabIndex = 3;
            this.rbDefaultDataSetName.Text = "Data Set Name";
            this.rbDefaultDataSetName.CheckedChanged += new System.EventHandler(this.rbDefaultDataSetName_CheckedChanged);
            // 
            // ckbParmAllowNull
            // 
            this.ckbParmAllowNull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckbParmAllowNull.ForeColor = System.Drawing.Color.Black;
            this.ckbParmAllowNull.Location = new System.Drawing.Point(150, 72);
            this.ckbParmAllowNull.Name = "ckbParmAllowNull";
            this.ckbParmAllowNull.Size = new System.Drawing.Size(72, 24);
            this.ckbParmAllowNull.TabIndex = 8;
            this.ckbParmAllowNull.Text = "Allow null";
            this.ckbParmAllowNull.CheckedChanged += new System.EventHandler(this.ckbParmAllowNull_CheckedChanged);
            // 
            // ckbParmMultiValue
            // 
            this.ckbParmMultiValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckbParmMultiValue.ForeColor = System.Drawing.Color.Black;
            this.ckbParmMultiValue.Location = new System.Drawing.Point(376, 72);
            this.ckbParmMultiValue.Name = "ckbParmMultiValue";
            this.ckbParmMultiValue.Size = new System.Drawing.Size(79, 24);
            this.ckbParmMultiValue.TabIndex = 24;
            this.ckbParmMultiValue.Text = "MultiValue";
            this.ckbParmMultiValue.CheckedChanged += new System.EventHandler(this.ckbParmMultiValue_CheckedChanged);
            // 
            // ReportParameterCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.ckbParmMultiValue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bParmDown);
            this.Controls.Add(this.bParmUp);
            this.Controls.Add(this.ckbParmAllowBlank);
            this.Controls.Add(this.ckbParmAllowNull);
            this.Controls.Add(this.tbParmPrompt);
            this.Controls.Add(this.lParmPrompt);
            this.Controls.Add(this.cbParmType);
            this.Controls.Add(this.lParmType);
            this.Controls.Add(this.tbParmName);
            this.Controls.Add(this.lParmName);
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.lbParameters);
            this.Controls.Add(this.groupBox2);
            this.Name = "ReportParameterCtl";
            this.Size = new System.Drawing.Size(456, 296);
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
			XmlNode rNode = _Draw.GetReportNode();
			_Draw.RemoveElement(rNode, "ReportParameters");	// remove old ReportParameters
			if (this.lbParameters.Items.Count <= 0)
				return;			// nothing in list?  all done

			XmlNode rpsNode = _Draw.SetElement(rNode, "ReportParameters", null);
			foreach (ReportParm repParm in lbParameters.Items)
			{
				if (repParm.Name == null || repParm.Name.Length <= 0)
					continue;	// shouldn't really happen
				XmlNode repNode = _Draw.CreateElement(rpsNode, "ReportParameter", null);
				// Create the name attribute
				_Draw.SetElementAttribute(repNode, "Name", repParm.Name);

				_Draw.SetElement(repNode, "DataType", repParm.DataType);
				// Handle default values
				ApplyDefaultValues(repNode, repParm);
				
				_Draw.SetElement(repNode, "Nullable", repParm.AllowNull? "true": "false");
				_Draw.SetElement(repNode, "AllowBlank", repParm.AllowBlank? "true": "false");
                _Draw.SetElement(repNode, "MultiValue", repParm.MultiValue ? "true" : "false");
                _Draw.SetElement(repNode, "Prompt", repParm.Prompt);

				// Handle ValidValues
				ApplyValidValues(repNode, repParm);
			}
		}

		private void ApplyDefaultValues(XmlNode repNode, ReportParm repParm)
		{
			_Draw.RemoveElement(repNode, "DefaultValue");
			if (repParm.Default)
			{
				if (repParm.DefaultValue == null || repParm.DefaultValue.Count == 0)
					return;

				XmlNode defNode = _Draw.GetCreateNamedChildNode(repNode, "DefaultValue");
				XmlNode vNodes = _Draw.GetCreateNamedChildNode(defNode, "Values");
				foreach (string dv in repParm.DefaultValue)
				{
					_Draw.CreateElement(vNodes, "Value", dv);
				}
			}
			else
			{
				if (repParm.DefaultDSRDataSetName == null || repParm.DefaultDSRDataSetName.Length == 0 ||
					repParm.DefaultDSRValueField == null || repParm.DefaultDSRValueField.Length == 0)
					return;
				XmlNode defNode = _Draw.GetCreateNamedChildNode(repNode, "DefaultValue");
				XmlNode dsrNode = _Draw.GetCreateNamedChildNode(defNode, "DataSetReference");
				_Draw.CreateElement(dsrNode, "DataSetName", repParm.DefaultDSRDataSetName);
				_Draw.CreateElement(dsrNode, "ValueField", repParm.DefaultDSRValueField);
			}
		}

		private void ApplyValidValues(XmlNode repNode, ReportParm repParm)
		{
			_Draw.RemoveElement(repNode, "ValidValues");
			if (repParm.Valid)
			{
				if (repParm.ValidValues == null || repParm.ValidValues.Count == 0)
					return;

				XmlNode vvNode = _Draw.GetCreateNamedChildNode(repNode, "ValidValues");
				XmlNode vNodes = _Draw.GetCreateNamedChildNode(vvNode, "ParameterValues");
				// put out the parameter values
				foreach (ParameterValueItem dvi in repParm.ValidValues)
				{
					XmlNode pvNode = _Draw.CreateElement(vNodes, "ParameterValue", null);
					_Draw.CreateElement(pvNode, "Value", dvi.Value);
					if (dvi.Label != null)
						_Draw.CreateElement(pvNode, "Label", dvi.Label);
				}
			}
			else
			{
				if (repParm.ValidValuesDSRDataSetName == null || repParm.ValidValuesDSRDataSetName.Length == 0 ||
					repParm.ValidValuesDSRValueField == null || repParm.ValidValuesDSRValueField.Length == 0)
					return;
				XmlNode defNode = _Draw.GetCreateNamedChildNode(repNode, "ValidValues");
				XmlNode dsrNode = _Draw.GetCreateNamedChildNode(defNode, "DataSetReference");
				_Draw.CreateElement(dsrNode, "DataSetName", repParm.ValidValuesDSRDataSetName);
				_Draw.CreateElement(dsrNode, "ValueField", repParm.ValidValuesDSRValueField);
				if (repParm.ValidValuesDSRLabelField != null && repParm.ValidValuesDSRLabelField.Length > 0)
					_Draw.CreateElement(dsrNode, "LabelField", repParm.ValidValuesDSRLabelField);
			}
		}

		private void bAdd_Click(object sender, System.EventArgs e)
		{
			ReportParm rp = new ReportParm("newparm");
			int cur = this.lbParameters.Items.Add(rp);
			lbParameters.SelectedIndex = cur;
			this.tbParmName.Focus();
		}

		private void bRemove_Click(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;
			lbParameters.Items.RemoveAt(cur);
			if (lbParameters.Items.Count <= 0)
				return;
			cur--;
			if (cur < 0)
				cur = 0;
			lbParameters.SelectedIndex = cur;
		}

		private void lbParameters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			tbParmName.Text = rp.Name;
			cbParmType.Text = rp.DataType;
			tbParmPrompt.Text = rp.Prompt;
			ckbParmAllowBlank.Checked = rp.AllowBlank;
            ckbParmMultiValue.Checked = rp.MultiValue;
            ckbParmAllowNull.Checked = rp.AllowNull;
			// Handle default values
			if (rp.Default)
			{
				this.rbDefaultValues.Checked = true;
				tbParmDefaultValue.Text = rp.DefaultValueDisplay; 			

				tbParmDefaultValue.Enabled = bDefaultValues.Enabled = true;
				this.cbDefaultDataSets.Enabled = false;
				this.cbDefaultValueField.Enabled = false;
				this.cbDefaultDataSets.SelectedIndex = -1;
				this.cbDefaultValueField.SelectedIndex = -1;
			}
			else
			{
				this.rbDefaultDataSetName.Checked = true;
				this.cbDefaultDataSets.Text = rp.DefaultDSRDataSetName;
				this.cbDefaultValueField.Text = rp.DefaultDSRValueField;

				tbParmDefaultValue.Enabled = bDefaultValues.Enabled =false;
				tbParmDefaultValue.Text = "";
				this.cbDefaultDataSets.Enabled = true;
				this.cbDefaultValueField.Enabled = true;
			}
			// Handle Valid Values
			if (rp.Valid)
			{
				this.rbValues.Checked = true;
				tbParmValidValues.Text = rp.ValidValuesDisplay;

				tbParmValidValues.Enabled = bValidValues.Enabled = true;
				this.cbValidDataSets.Enabled =
					this.cbValidFields.Enabled =
					this.cbValidDisplayField.Enabled = false;
				this.cbValidDataSets.SelectedIndex   =
					this.cbValidFields.SelectedIndex =
					this.cbValidDisplayField.SelectedIndex = -1;
			}
			else
			{
				this.rbDataSet.Checked = true;
				this.cbValidDataSets.Text = rp.ValidValuesDSRDataSetName;
				this.cbValidFields.Text = rp.ValidValuesDSRValueField;
				this.cbValidDisplayField.Text = rp.ValidValuesDSRLabelField == null? "":rp.ValidValuesDSRLabelField;

				this.cbValidDataSets.Enabled =
						this.cbValidFields.Enabled =
						this.cbValidDisplayField.Enabled = true;
				tbParmValidValues.Enabled = bValidValues.Enabled = false;
				tbParmValidValues.Text = "";
			}
		}

		private void lbParameters_MoveItem(int curloc, int newloc)
		{
			ReportParm rp = lbParameters.Items[curloc] as ReportParm;
			if (rp == null)
				return;

			lbParameters.BeginUpdate();
			lbParameters.Items.RemoveAt(curloc);
			lbParameters.Items.Insert(newloc, rp);
			lbParameters.SelectedIndex = newloc;
			lbParameters.EndUpdate();
		}

		private void tbParmName_TextChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			if (rp.Name == tbParmName.Text)
				return;

			rp.Name = tbParmName.Text;
			// text doesn't change in listbox; force change by removing and re-adding item
			lbParameters_MoveItem(cur, cur);
		}

		private void cbParmType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.DataType = cbParmType.Text;
		}

		private void tbParmPrompt_TextChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.Prompt = tbParmPrompt.Text;
		}

		private void ckbParmAllowNull_CheckedChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.AllowNull = ckbParmAllowNull.Checked;
		}

		private void ckbParmAllowBlank_CheckedChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.AllowBlank = ckbParmAllowBlank.Checked;
		}

		private void bParmUp_Click(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur <= 0)
				return;
		
			lbParameters_MoveItem(cur, cur-1);
		}

		private void bParmDown_Click(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur+1 >= lbParameters.Items.Count)
				return;
		
			lbParameters_MoveItem(cur, cur+1);
		}

		private void cbDefaultDataSets_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.DefaultDSRDataSetName = cbDefaultDataSets.Text;

			// Populate the fields from the selected dataset
			this.cbDefaultValueField.Items.Clear();
			string[] fields = _Draw.GetFields(cbDefaultDataSets.Text, false);
            if (fields != null)
			    this.cbDefaultValueField.Items.AddRange(fields);
		}

		private void cbValidDataSets_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.ValidValuesDSRDataSetName = cbValidDataSets.Text;

			// Populate the fields from the selected dataset
			this.cbValidFields.Items.Clear();
			string[] fields = _Draw.GetFields(cbValidDataSets.Text, false);
            if (fields != null)
			    this.cbValidFields.Items.AddRange(fields);
			this.cbValidDisplayField.Items.Clear();
			this.cbValidDisplayField.Items.Add("");
            if (fields != null)
			    this.cbValidDisplayField.Items.AddRange(fields);
		}

		private void cbDefaultValueField_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.DefaultDSRValueField = cbDefaultValueField.Text;
		}

		private void cbValidFields_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.ValidValuesDSRValueField = cbValidFields.Text;
		}

		private void cbValidDisplayField_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.ValidValuesDSRLabelField = cbValidDisplayField.Text;
		}

		private void rbDefaultValues_CheckedChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.Default = rbDefaultValues.Checked;

			tbParmDefaultValue.Enabled = bDefaultValues.Enabled = rbDefaultValues.Checked;
			this.cbDefaultDataSets.Enabled = 
				this.cbDefaultValueField.Enabled = !rbDefaultValues.Checked;
		}

		private void rbDefaultDataSetName_CheckedChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.Default = !rbDefaultDataSetName.Checked;

			tbParmDefaultValue.Enabled = bDefaultValues.Enabled = !rbDefaultDataSetName.Checked;
			this.cbDefaultDataSets.Enabled = 
				this.cbDefaultValueField.Enabled = rbDefaultDataSetName.Checked;
		}

		private void rbValues_CheckedChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			this.tbParmValidValues.Enabled = bValidValues.Enabled =  rbValues.Checked;
			rp.Valid = rbValues.Checked;

			this.cbValidDisplayField.Enabled = 
				this.cbValidFields.Enabled =
				this.cbValidDataSets.Enabled = !rbValues.Checked;
		}

		private void rbDataSet_CheckedChanged(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

			rp.Valid = !rbDataSet.Checked;
			this.tbParmValidValues.Enabled = bValidValues.Enabled = !rbDataSet.Checked;
			this.cbValidDisplayField.Enabled = 
				this.cbValidFields.Enabled =
				this.cbValidDataSets.Enabled = rbDataSet.Checked;
		}

		private void bDefaultValues_Click(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

            using (DialogListOfStrings dlos = new DialogListOfStrings(rp.DefaultValue))
            {
                dlos.Text = "Default Values";
                if (dlos.ShowDialog() != DialogResult.OK)
                    return;
                rp.DefaultValue = dlos.ListOfStrings;
                this.tbParmDefaultValue.Text = rp.DefaultValueDisplay;
            }
		}

		private void bValidValues_Click(object sender, System.EventArgs e)
		{
			int cur = lbParameters.SelectedIndex;
			if (cur < 0)
				return;

			ReportParm rp = lbParameters.Items[cur] as ReportParm;
			if (rp == null)
				return;

            using (DialogValidValues dvv = new DialogValidValues(rp.ValidValues))
            {
                if (dvv.ShowDialog() != DialogResult.OK)
                    return;
                rp.ValidValues = dvv.ValidValues;
                this.tbParmValidValues.Text = rp.ValidValuesDisplay;
            }
		}

        private void ckbParmMultiValue_CheckedChanged(object sender, EventArgs e)
        {
            int cur = lbParameters.SelectedIndex;
            if (cur < 0)
                return;

            ReportParm rp = lbParameters.Items[cur] as ReportParm;
            if (rp == null)
                return;

            rp.MultiValue = ckbParmMultiValue.Checked;

        }

	}
}
