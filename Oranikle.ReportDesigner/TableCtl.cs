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
	internal class TableCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		bool fDataSet, fPBBefore, fPBAfter, fNoRows;
		bool fDetailElementName, fDetailCollectionName, fRenderDetails;
		bool fCheckRows;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.StyledComboBox cbDataSet;
		private System.Windows.Forms.GroupBox groupBox1;
		private Oranikle.Studio.Controls.StyledCheckBox chkPBBefore;
		private Oranikle.Studio.Controls.StyledCheckBox chkPBAfter;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbNoRows;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private Oranikle.Studio.Controls.StyledCheckBox chkRenderDetails;
		private Oranikle.Studio.Controls.CustomTextControl tbDetailElementName;
		private Oranikle.Studio.Controls.CustomTextControl tbDetailCollectionName;
		private System.Windows.Forms.GroupBox groupBox3;
		private Oranikle.Studio.Controls.StyledCheckBox chkDetails;
		private Oranikle.Studio.Controls.StyledCheckBox chkHeaderRows;
        private Oranikle.Studio.Controls.StyledCheckBox chkFooterRows;
        private Oranikle.Studio.Controls.StyledCheckBox chkFooterRepeat;
        private Oranikle.Studio.Controls.StyledCheckBox chkHeaderRepeat;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal TableCtl(DesignXmlDraw dxDraw, List<XmlNode> ris)
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

			tbNoRows.Text = _Draw.GetElementValue(riNode, "NoRows", "");
			cbDataSet.Items.AddRange(_Draw.DataSetNames);
			cbDataSet.Text = _Draw.GetDataSetNameValue(riNode);
			if (_Draw.GetReportItemDataRegionContainer(riNode) != null)
				cbDataSet.Enabled = false;
			chkPBBefore.Checked = _Draw.GetElementValue(riNode, "PageBreakAtStart", "false").ToLower()=="true"? true:false;
			chkPBAfter.Checked = _Draw.GetElementValue(riNode, "PageBreakAtEnd", "false").ToLower()=="true"? true:false;

			this.chkRenderDetails.Checked = _Draw.GetElementValue(riNode, "DetailDataElementOutput", "output").ToLower() == "output";
			this.tbDetailElementName.Text = _Draw.GetElementValue(riNode, "DetailDataElementName", "Details");
			this.tbDetailCollectionName.Text = _Draw.GetElementValue(riNode, "DetailDataCollectionName", "Details_Collection");

			this.chkDetails.Checked = _Draw.GetNamedChildNode(riNode, "Details") != null;
            XmlNode fNode = _Draw.GetNamedChildNode(riNode, "Footer");
			this.chkFooterRows.Checked = fNode != null;
            if (fNode != null)
            {
                chkFooterRepeat.Checked = _Draw.GetElementValue(fNode, "RepeatOnNewPage", "false").ToLower() == "true" ? true : false;
            }
            else
                chkFooterRepeat.Enabled = false;

            XmlNode hNode = _Draw.GetNamedChildNode(riNode, "Header");
            this.chkHeaderRows.Checked = hNode != null;
            if (hNode != null)
            {
                chkHeaderRepeat.Checked = _Draw.GetElementValue(hNode, "RepeatOnNewPage", "false").ToLower() == "true" ? true : false;
            }
            else
                chkHeaderRepeat.Enabled = false;

			fNoRows = fDataSet = fPBBefore = fPBAfter = 
				fDetailElementName = fDetailCollectionName = fRenderDetails =
				fCheckRows = false;
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbDataSet = new Oranikle.Studio.Controls.StyledComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPBAfter = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkPBBefore = new Oranikle.Studio.Controls.StyledCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNoRows = new Oranikle.Studio.Controls.CustomTextControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbDetailCollectionName = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbDetailElementName = new Oranikle.Studio.Controls.CustomTextControl();
            this.chkRenderDetails = new Oranikle.Studio.Controls.StyledCheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkFooterRepeat = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkHeaderRepeat = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkFooterRows = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkHeaderRows = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkDetails = new Oranikle.Studio.Controls.StyledCheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "DataSet Name";
            // 
            // cbDataSet
            // 
            this.cbDataSet.AutoAdjustItemHeight = false;
            this.cbDataSet.BorderColor = System.Drawing.Color.LightGray;
            this.cbDataSet.ConvertEnterToTabForDialogs = false;
            this.cbDataSet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDataSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDataSet.Location = new System.Drawing.Point(120, 16);
            this.cbDataSet.Name = "cbDataSet";
            this.cbDataSet.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDataSet.SeparatorMargin = 1;
            this.cbDataSet.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDataSet.SeparatorWidth = 1;
            this.cbDataSet.Size = new System.Drawing.Size(304, 21);
            this.cbDataSet.TabIndex = 0;
            this.cbDataSet.SelectedIndexChanged += new System.EventHandler(this.cbDataSet_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPBAfter);
            this.groupBox1.Controls.Add(this.chkPBBefore);
            this.groupBox1.Location = new System.Drawing.Point(24, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 48);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Page Breaks";
            // 
            // chkPBAfter
            // 
            this.chkPBAfter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPBAfter.ForeColor = System.Drawing.Color.Black;
            this.chkPBAfter.Location = new System.Drawing.Point(192, 16);
            this.chkPBAfter.Name = "chkPBAfter";
            this.chkPBAfter.Size = new System.Drawing.Size(128, 24);
            this.chkPBAfter.TabIndex = 1;
            this.chkPBAfter.Text = "Insert after Table";
            this.chkPBAfter.CheckedChanged += new System.EventHandler(this.chkPBAfter_CheckedChanged);
            // 
            // chkPBBefore
            // 
            this.chkPBBefore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPBBefore.ForeColor = System.Drawing.Color.Black;
            this.chkPBBefore.Location = new System.Drawing.Point(16, 16);
            this.chkPBBefore.Name = "chkPBBefore";
            this.chkPBBefore.Size = new System.Drawing.Size(128, 24);
            this.chkPBBefore.TabIndex = 0;
            this.chkPBBefore.Text = "Insert before Table";
            this.chkPBBefore.CheckedChanged += new System.EventHandler(this.chkPBBefore_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "No rows message";
            // 
            // tbNoRows
            // 
            this.tbNoRows.AddX = 0;
            this.tbNoRows.AddY = 0;
            this.tbNoRows.AllowSpace = false;
            this.tbNoRows.BorderColor = System.Drawing.Color.LightGray;
            this.tbNoRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNoRows.ChangeVisibility = false;
            this.tbNoRows.ChildControl = null;
            this.tbNoRows.ConvertEnterToTab = true;
            this.tbNoRows.ConvertEnterToTabForDialogs = false;
            this.tbNoRows.Decimals = 0;
            this.tbNoRows.DisplayList = new object[0];
            this.tbNoRows.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbNoRows.Location = new System.Drawing.Point(120, 40);
            this.tbNoRows.Name = "tbNoRows";
            this.tbNoRows.OnDropDownCloseFocus = true;
            this.tbNoRows.SelectType = 0;
            this.tbNoRows.Size = new System.Drawing.Size(304, 20);
            this.tbNoRows.TabIndex = 1;
            this.tbNoRows.Text = "textBox1";
            this.tbNoRows.UseValueForChildsVisibilty = false;
            this.tbNoRows.Value = true;
            this.tbNoRows.TextChanged += new System.EventHandler(this.tbNoRows_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbDetailCollectionName);
            this.groupBox2.Controls.Add(this.tbDetailElementName);
            this.groupBox2.Controls.Add(this.chkRenderDetails);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(24, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 92);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XML";
            // 
            // tbDetailCollectionName
            // 
            this.tbDetailCollectionName.AddX = 0;
            this.tbDetailCollectionName.AddY = 0;
            this.tbDetailCollectionName.AllowSpace = false;
            this.tbDetailCollectionName.BorderColor = System.Drawing.Color.LightGray;
            this.tbDetailCollectionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDetailCollectionName.ChangeVisibility = false;
            this.tbDetailCollectionName.ChildControl = null;
            this.tbDetailCollectionName.ConvertEnterToTab = true;
            this.tbDetailCollectionName.ConvertEnterToTabForDialogs = false;
            this.tbDetailCollectionName.Decimals = 0;
            this.tbDetailCollectionName.DisplayList = new object[0];
            this.tbDetailCollectionName.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDetailCollectionName.Location = new System.Drawing.Point(160, 62);
            this.tbDetailCollectionName.Name = "tbDetailCollectionName";
            this.tbDetailCollectionName.OnDropDownCloseFocus = true;
            this.tbDetailCollectionName.SelectType = 0;
            this.tbDetailCollectionName.Size = new System.Drawing.Size(224, 20);
            this.tbDetailCollectionName.TabIndex = 3;
            this.tbDetailCollectionName.Text = "textBox1";
            this.tbDetailCollectionName.UseValueForChildsVisibilty = false;
            this.tbDetailCollectionName.Value = true;
            this.tbDetailCollectionName.TextChanged += new System.EventHandler(this.tbDetailCollectionName_TextChanged);
            // 
            // tbDetailElementName
            // 
            this.tbDetailElementName.AddX = 0;
            this.tbDetailElementName.AddY = 0;
            this.tbDetailElementName.AllowSpace = false;
            this.tbDetailElementName.BorderColor = System.Drawing.Color.LightGray;
            this.tbDetailElementName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDetailElementName.ChangeVisibility = false;
            this.tbDetailElementName.ChildControl = null;
            this.tbDetailElementName.ConvertEnterToTab = true;
            this.tbDetailElementName.ConvertEnterToTabForDialogs = false;
            this.tbDetailElementName.Decimals = 0;
            this.tbDetailElementName.DisplayList = new object[0];
            this.tbDetailElementName.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDetailElementName.Location = new System.Drawing.Point(160, 36);
            this.tbDetailElementName.Name = "tbDetailElementName";
            this.tbDetailElementName.OnDropDownCloseFocus = true;
            this.tbDetailElementName.SelectType = 0;
            this.tbDetailElementName.Size = new System.Drawing.Size(224, 20);
            this.tbDetailElementName.TabIndex = 2;
            this.tbDetailElementName.Text = "textBox1";
            this.tbDetailElementName.UseValueForChildsVisibilty = false;
            this.tbDetailElementName.Value = true;
            this.tbDetailElementName.TextChanged += new System.EventHandler(this.tbDetailElementName_TextChanged);
            // 
            // chkRenderDetails
            // 
            this.chkRenderDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRenderDetails.ForeColor = System.Drawing.Color.Black;
            this.chkRenderDetails.Location = new System.Drawing.Point(16, 12);
            this.chkRenderDetails.Name = "chkRenderDetails";
            this.chkRenderDetails.Size = new System.Drawing.Size(160, 24);
            this.chkRenderDetails.TabIndex = 0;
            this.chkRenderDetails.Text = "Render Details in Output";
            this.chkRenderDetails.CheckedChanged += new System.EventHandler(this.chkRenderDetails_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Detail Collection Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Detail Element Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkFooterRepeat);
            this.groupBox3.Controls.Add(this.chkHeaderRepeat);
            this.groupBox3.Controls.Add(this.chkFooterRows);
            this.groupBox3.Controls.Add(this.chkHeaderRows);
            this.groupBox3.Controls.Add(this.chkDetails);
            this.groupBox3.Location = new System.Drawing.Point(24, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 67);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Include Table Rows";
            // 
            // chkFooterRepeat
            // 
            this.chkFooterRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkFooterRepeat.ForeColor = System.Drawing.Color.Black;
            this.chkFooterRepeat.Location = new System.Drawing.Point(272, 34);
            this.chkFooterRepeat.Name = "chkFooterRepeat";
            this.chkFooterRepeat.Size = new System.Drawing.Size(122, 30);
            this.chkFooterRepeat.TabIndex = 4;
            this.chkFooterRepeat.Text = "Repeat footer on new page";
            this.chkFooterRepeat.CheckedChanged += new System.EventHandler(this.chkRows_CheckedChanged);
            // 
            // chkHeaderRepeat
            // 
            this.chkHeaderRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkHeaderRepeat.ForeColor = System.Drawing.Color.Black;
            this.chkHeaderRepeat.Location = new System.Drawing.Point(144, 34);
            this.chkHeaderRepeat.Name = "chkHeaderRepeat";
            this.chkHeaderRepeat.Size = new System.Drawing.Size(122, 30);
            this.chkHeaderRepeat.TabIndex = 3;
            this.chkHeaderRepeat.Text = "Repeat header on new page";
            this.chkHeaderRepeat.CheckedChanged += new System.EventHandler(this.chkRows_CheckedChanged);
            // 
            // chkFooterRows
            // 
            this.chkFooterRows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkFooterRows.ForeColor = System.Drawing.Color.Black;
            this.chkFooterRows.Location = new System.Drawing.Point(272, 13);
            this.chkFooterRows.Name = "chkFooterRows";
            this.chkFooterRows.Size = new System.Drawing.Size(104, 24);
            this.chkFooterRows.TabIndex = 2;
            this.chkFooterRows.Text = "Footer Rows";
            this.chkFooterRows.CheckedChanged += new System.EventHandler(this.chkRows_CheckedChanged);
            // 
            // chkHeaderRows
            // 
            this.chkHeaderRows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkHeaderRows.ForeColor = System.Drawing.Color.Black;
            this.chkHeaderRows.Location = new System.Drawing.Point(144, 13);
            this.chkHeaderRows.Name = "chkHeaderRows";
            this.chkHeaderRows.Size = new System.Drawing.Size(104, 24);
            this.chkHeaderRows.TabIndex = 1;
            this.chkHeaderRows.Text = "Header Rows";
            this.chkHeaderRows.CheckedChanged += new System.EventHandler(this.chkRows_CheckedChanged);
            // 
            // chkDetails
            // 
            this.chkDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDetails.ForeColor = System.Drawing.Color.Black;
            this.chkDetails.Location = new System.Drawing.Point(16, 13);
            this.chkDetails.Name = "chkDetails";
            this.chkDetails.Size = new System.Drawing.Size(104, 24);
            this.chkDetails.TabIndex = 1;
            this.chkDetails.Text = "Detail Rows";
            this.chkDetails.CheckedChanged += new System.EventHandler(this.chkRows_CheckedChanged);
            // 
            // TableCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tbNoRows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbDataSet);
            this.Controls.Add(this.label2);
            this.Name = "TableCtl";
            this.Size = new System.Drawing.Size(472, 288);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
  
		public bool IsValid()
		{
			if (this.chkDetails.Checked || this.chkFooterRows.Checked || this.chkHeaderRows.Checked)
				return true;

			MessageBox.Show("Table must have at least one Header, Details or Footer row defined.", "Table");

			return false;
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
			fNoRows = fDataSet = fPBBefore = fPBAfter = 
				fDetailElementName = fDetailCollectionName = fRenderDetails =
				fCheckRows = false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (fNoRows)
				_Draw.SetElement(node, "NoRows", this.tbNoRows.Text);
			if (fDataSet)
				_Draw.SetElement(node, "DataSetName", this.cbDataSet.Text);
			if (fPBBefore)
				_Draw.SetElement(node, "PageBreakAtStart", this.chkPBBefore.Checked? "true":"false");
			if (fPBAfter)
				_Draw.SetElement(node, "PageBreakAtEnd", this.chkPBAfter.Checked? "true":"false");
			if (fCheckRows)
			{
				if (this.chkDetails.Checked)
					CreateTableRow(node, "Details", false);
				else
					_Draw.RemoveElement(node, "Details");
				if (this.chkHeaderRows.Checked)
					CreateTableRow(node, "Header", chkHeaderRepeat.Checked);
				else
					_Draw.RemoveElement(node, "Header");
				if (this.chkFooterRows.Checked)
					CreateTableRow(node, "Footer", chkFooterRepeat.Checked);
				else
					_Draw.RemoveElement(node, "Footer");
			}
			if (fRenderDetails)
				_Draw.SetElement(node, "DetailDataElementOutput", this.chkRenderDetails.Checked? "Output":"NoOutput");
			if (this.fDetailElementName)
			{
				if (this.tbDetailElementName.Text.Length > 0)
					_Draw.SetElement(node, "DetailDataElementName", this.tbDetailElementName.Text);
				else
					_Draw.RemoveElement(node, "DetailDataElementName");
			}
			if (this.fDetailCollectionName)
			{
				if (this.tbDetailCollectionName.Text.Length > 0)
					_Draw.SetElement(node, "DetailDataCollectionName", this.tbDetailCollectionName.Text);
				else
					_Draw.RemoveElement(node, "DetailDataCollectionName");
			}
		}

		private void CreateTableRow(XmlNode tblNode, string elementName, bool bRepeatOnNewPage)
		{
			XmlNode node = _Draw.GetNamedChildNode(tblNode, elementName);
			if (node == null)
			{
				node = _Draw.CreateElement(tblNode, elementName, null);
				XmlNode tblRows = _Draw.CreateElement(node, "TableRows", null);
				_Draw.InsertTableRow(tblRows);
			}
            if (bRepeatOnNewPage)
                _Draw.SetElement(node, "RepeatOnNewPage", "true");
            else
                _Draw.RemoveElement(node, "RepeatOnNewPage");

			return;
		}

		private void cbDataSet_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fDataSet = true;
		}

		private void chkPBBefore_CheckedChanged(object sender, System.EventArgs e)
		{
			fPBBefore = true;
		}

		private void chkPBAfter_CheckedChanged(object sender, System.EventArgs e)
		{
			fPBAfter = true;
		}

		private void tbNoRows_TextChanged(object sender, System.EventArgs e)
		{
			fNoRows = true;
		}

		private void chkRows_CheckedChanged(object sender, System.EventArgs e)
		{
			this.fCheckRows = true;
            chkFooterRepeat.Enabled = chkFooterRows.Checked;
            chkHeaderRepeat.Enabled = chkHeaderRows.Checked;
		}

		private void chkRenderDetails_CheckedChanged(object sender, System.EventArgs e)
		{
			fRenderDetails = true;
		}

		private void tbDetailElementName_TextChanged(object sender, System.EventArgs e)
		{
			fDetailElementName = true;
		}

		private void tbDetailCollectionName_TextChanged(object sender, System.EventArgs e)
		{
			fDetailCollectionName = true;
		}

	}
}
