/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Text;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Grouping specification: used for DataRegions (List, Chart, Table, Matrix), DataSets, group instances
	/// </summary>
	internal class GroupingCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		private DesignXmlDraw _Draw;
		private XmlNode _GroupingParent;
		private DataTable _DataTable;
//		private DGCBColumn dgtbGE;
		private DataGridTextBoxColumn dgtbGE;

		private Oranikle.Studio.Controls.StyledButton bDelete;
		private System.Windows.Forms.DataGridTableStyle dgTableStyle;
		private Oranikle.Studio.Controls.StyledButton bUp;
		private Oranikle.Studio.Controls.StyledButton bDown;
		private System.Windows.Forms.DataGrid dgGroup;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.StyledComboBox cbLabelExpr;
		private Oranikle.Studio.Controls.StyledComboBox cbParentExpr;
		private Oranikle.Studio.Controls.StyledCheckBox chkPBS;
		private Oranikle.Studio.Controls.StyledCheckBox chkPBE;
		private Oranikle.Studio.Controls.StyledCheckBox chkRepeatHeader;
		private Oranikle.Studio.Controls.StyledCheckBox chkGrpHeader;
		private Oranikle.Studio.Controls.StyledCheckBox chkRepeatFooter;
		private Oranikle.Studio.Controls.StyledCheckBox chkGrpFooter;
		private System.Windows.Forms.Label lParent;
		private Oranikle.Studio.Controls.StyledButton bValueExpr;
		private Oranikle.Studio.Controls.StyledButton bLabelExpr;
		private Oranikle.Studio.Controls.StyledButton bParentExpr;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal GroupingCtl(DesignXmlDraw dxDraw, XmlNode groupingParent)
		{
			_Draw = dxDraw;
			_GroupingParent = groupingParent;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			// Initialize the DataGrid columns
			//			dgtbGE = new DGCBColumn(ComboBoxStyle.DropDown);
			dgtbGE = new DataGridTextBoxColumn();

			this.dgTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] {
															this.dgtbGE});
			// 
			// dgtbGE
			// 
			dgtbGE.HeaderText = "Expression";
			dgtbGE.MappingName = "Expression";
			dgtbGE.Width = 175;
			// Get the parent's dataset name
//			string dataSetName = _Draw.GetDataSetNameValue(_GroupingParent);
//
//			string[] fields = _Draw.GetFields(dataSetName, true);
//			if (fields != null)
//				dgtbGE.CB.Items.AddRange(fields);

			// Initialize the DataTable
			_DataTable = new DataTable();
			_DataTable.Columns.Add(new DataColumn("Expression", typeof(string)));

			string[] rowValues = new string[1];
			XmlNode grouping = _Draw.GetNamedChildNode(_GroupingParent, "Grouping");

			// Handle the group expressions
			XmlNode groupingExs = _Draw.GetNamedChildNode(grouping, "GroupExpressions");

			if (groupingExs != null)
			foreach (XmlNode gNode in groupingExs.ChildNodes)
			{
				if (gNode.NodeType != XmlNodeType.Element || 
						gNode.Name != "GroupExpression")
					continue;
				rowValues[0] = gNode.InnerText;

				_DataTable.Rows.Add(rowValues);
			}
			this.dgGroup.DataSource = _DataTable;
			DataGridTableStyle ts = dgGroup.TableStyles[0];
		//	ts.PreferredRowHeight = dgtbGE.CB.Height;
			ts.GridColumnStyles[0].Width = 330;

			//
			if (grouping == null)
			{
				this.tbName.Text = "";
				this.cbParentExpr.Text =  "";
				this.cbLabelExpr.Text =  "";
			}
			else
			{
				this.chkPBE.Checked = _Draw.GetElementValue(grouping, "PageBreakAtEnd", "false").ToLower() == "true";
				this.chkPBS.Checked = _Draw.GetElementValue(grouping, "PageBreakAtStart", "false").ToLower() == "true";

				this.tbName.Text = _Draw.GetElementAttribute(grouping, "Name", "");
				this.cbParentExpr.Text =  _Draw.GetElementValue(grouping, "Parent", "");
				this.cbLabelExpr.Text =  _Draw.GetElementValue(grouping, "Label", "");
			}

			if (_GroupingParent.Name == "TableGroup")
			{
				XmlNode repeat;
				repeat = DesignXmlDraw.FindNextInHierarchy(_GroupingParent, "Header", "RepeatOnNewPage");
				if (repeat != null)
					this.chkRepeatHeader.Checked = repeat.InnerText.ToLower() == "true";
				repeat = DesignXmlDraw.FindNextInHierarchy(_GroupingParent, "Footer", "RepeatOnNewPage");
				if (repeat != null)
					this.chkRepeatFooter.Checked = repeat.InnerText.ToLower() == "true";
				this.chkGrpHeader.Checked = _Draw.GetNamedChildNode(_GroupingParent, "Header") != null;
				this.chkGrpFooter.Checked = _Draw.GetNamedChildNode(_GroupingParent, "Footer") != null;
			}
			else
			{
				this.chkRepeatFooter.Visible = false;
				this.chkRepeatHeader.Visible = false;
				this.chkGrpFooter.Visible = false;
				this.chkGrpHeader.Visible = false;
			}
			if (_GroupingParent.Name == "DynamicColumns" ||
				_GroupingParent.Name == "DynamicRows")
			{
				this.chkPBE.Visible = this.chkPBS.Visible = false;
			}
			else if (_GroupingParent.Name == "DynamicSeries" ||
				_GroupingParent.Name == "DynamicCategories")
			{
				this.chkPBE.Visible = this.chkPBS.Visible = false;
				this.cbParentExpr.Visible = this.lParent.Visible = false;
				this.cbLabelExpr.Text =  _Draw.GetElementValue(_GroupingParent, "Label", "");
			}

			// load label and parent controls with fields
			string dsn = _Draw.GetDataSetNameValue(_GroupingParent);
			if (dsn != null)	// found it
			{
				string[] f = _Draw.GetFields(dsn, true);
                if (f != null)
                {
                    this.cbParentExpr.Items.AddRange(f);
                    this.cbLabelExpr.Items.AddRange(f);
                }
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
            this.dgGroup = new System.Windows.Forms.DataGrid();
            this.dgTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.bDelete = new Oranikle.Studio.Controls.StyledButton();
            this.bUp = new Oranikle.Studio.Controls.StyledButton();
            this.bDown = new Oranikle.Studio.Controls.StyledButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new Oranikle.Studio.Controls.CustomTextControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLabelExpr = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbParentExpr = new Oranikle.Studio.Controls.StyledComboBox();
            this.lParent = new System.Windows.Forms.Label();
            this.chkPBS = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkPBE = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkRepeatHeader = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkGrpHeader = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkRepeatFooter = new Oranikle.Studio.Controls.StyledCheckBox();
            this.chkGrpFooter = new Oranikle.Studio.Controls.StyledCheckBox();
            this.bValueExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bLabelExpr = new Oranikle.Studio.Controls.StyledButton();
            this.bParentExpr = new Oranikle.Studio.Controls.StyledButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // dgGroup
            // 
            this.dgGroup.BackgroundColor = System.Drawing.Color.White;
            this.dgGroup.CaptionVisible = false;
            this.dgGroup.DataMember = "";
            this.dgGroup.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgGroup.Location = new System.Drawing.Point(8, 48);
            this.dgGroup.Name = "dgGroup";
            this.dgGroup.Size = new System.Drawing.Size(376, 88);
            this.dgGroup.TabIndex = 1;
            this.dgGroup.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgTableStyle});
            // 
            // dgTableStyle
            // 
            this.dgTableStyle.AllowSorting = false;
            this.dgTableStyle.DataGrid = this.dgGroup;
            this.dgTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // bDelete
            // 
            this.bDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDelete.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDelete.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDelete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDelete.Font = new System.Drawing.Font("Arial", 9F);
            this.bDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDelete.Location = new System.Drawing.Point(392, 69);
            this.bDelete.Name = "bDelete";
            this.bDelete.OverriddenSize = null;
            this.bDelete.Size = new System.Drawing.Size(48, 21);
            this.bDelete.TabIndex = 2;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bUp
            // 
            this.bUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bUp.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bUp.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUp.Font = new System.Drawing.Font("Arial", 9F);
            this.bUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bUp.Location = new System.Drawing.Point(392, 94);
            this.bUp.Name = "bUp";
            this.bUp.OverriddenSize = null;
            this.bUp.Size = new System.Drawing.Size(48, 21);
            this.bUp.TabIndex = 3;
            this.bUp.Text = "Up";
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDown.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDown.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDown.Font = new System.Drawing.Font("Arial", 9F);
            this.bDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDown.Location = new System.Drawing.Point(392, 119);
            this.bDown.Name = "bDown";
            this.bDown.OverriddenSize = null;
            this.bDown.Size = new System.Drawing.Size(48, 21);
            this.bDown.TabIndex = 4;
            this.bDown.Text = "Down";
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 5;
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
            this.tbName.Location = new System.Drawing.Point(56, 8);
            this.tbName.Name = "tbName";
            this.tbName.OnDropDownCloseFocus = true;
            this.tbName.SelectType = 0;
            this.tbName.Size = new System.Drawing.Size(328, 20);
            this.tbName.TabIndex = 0;
            this.tbName.Text = "textBox1";
            this.tbName.UseValueForChildsVisibilty = false;
            this.tbName.Value = true;
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Label";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Group By";
            // 
            // cbLabelExpr
            // 
            this.cbLabelExpr.AutoAdjustItemHeight = false;
            this.cbLabelExpr.BorderColor = System.Drawing.Color.LightGray;
            this.cbLabelExpr.ConvertEnterToTabForDialogs = false;
            this.cbLabelExpr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLabelExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLabelExpr.Location = new System.Drawing.Point(48, 147);
            this.cbLabelExpr.Name = "cbLabelExpr";
            this.cbLabelExpr.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbLabelExpr.SeparatorMargin = 1;
            this.cbLabelExpr.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbLabelExpr.SeparatorWidth = 1;
            this.cbLabelExpr.Size = new System.Drawing.Size(336, 21);
            this.cbLabelExpr.TabIndex = 5;
            this.cbLabelExpr.Text = "comboBox1";
            // 
            // cbParentExpr
            // 
            this.cbParentExpr.AutoAdjustItemHeight = false;
            this.cbParentExpr.BorderColor = System.Drawing.Color.LightGray;
            this.cbParentExpr.ConvertEnterToTabForDialogs = false;
            this.cbParentExpr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbParentExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbParentExpr.Location = new System.Drawing.Point(48, 180);
            this.cbParentExpr.Name = "cbParentExpr";
            this.cbParentExpr.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbParentExpr.SeparatorMargin = 1;
            this.cbParentExpr.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbParentExpr.SeparatorWidth = 1;
            this.cbParentExpr.Size = new System.Drawing.Size(336, 21);
            this.cbParentExpr.TabIndex = 6;
            this.cbParentExpr.Text = "comboBox1";
            // 
            // lParent
            // 
            this.lParent.Location = new System.Drawing.Point(8, 179);
            this.lParent.Name = "lParent";
            this.lParent.Size = new System.Drawing.Size(40, 23);
            this.lParent.TabIndex = 11;
            this.lParent.Text = "Parent";
            // 
            // chkPBS
            // 
            this.chkPBS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPBS.ForeColor = System.Drawing.Color.Black;
            this.chkPBS.Location = new System.Drawing.Point(8, 208);
            this.chkPBS.Name = "chkPBS";
            this.chkPBS.Size = new System.Drawing.Size(136, 24);
            this.chkPBS.TabIndex = 7;
            this.chkPBS.Text = "Page Break at Start";
            // 
            // chkPBE
            // 
            this.chkPBE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPBE.ForeColor = System.Drawing.Color.Black;
            this.chkPBE.Location = new System.Drawing.Point(232, 208);
            this.chkPBE.Name = "chkPBE";
            this.chkPBE.Size = new System.Drawing.Size(136, 24);
            this.chkPBE.TabIndex = 8;
            this.chkPBE.Text = "Page Break at End";
            // 
            // chkRepeatHeader
            // 
            this.chkRepeatHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRepeatHeader.ForeColor = System.Drawing.Color.Black;
            this.chkRepeatHeader.Location = new System.Drawing.Point(232, 232);
            this.chkRepeatHeader.Name = "chkRepeatHeader";
            this.chkRepeatHeader.Size = new System.Drawing.Size(136, 24);
            this.chkRepeatHeader.TabIndex = 13;
            this.chkRepeatHeader.Text = "Repeat group header";
            // 
            // chkGrpHeader
            // 
            this.chkGrpHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGrpHeader.ForeColor = System.Drawing.Color.Black;
            this.chkGrpHeader.Location = new System.Drawing.Point(8, 232);
            this.chkGrpHeader.Name = "chkGrpHeader";
            this.chkGrpHeader.Size = new System.Drawing.Size(136, 24);
            this.chkGrpHeader.TabIndex = 12;
            this.chkGrpHeader.Text = "Include group header";
            // 
            // chkRepeatFooter
            // 
            this.chkRepeatFooter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRepeatFooter.ForeColor = System.Drawing.Color.Black;
            this.chkRepeatFooter.Location = new System.Drawing.Point(232, 256);
            this.chkRepeatFooter.Name = "chkRepeatFooter";
            this.chkRepeatFooter.Size = new System.Drawing.Size(136, 24);
            this.chkRepeatFooter.TabIndex = 15;
            this.chkRepeatFooter.Text = "Repeat group footer";
            // 
            // chkGrpFooter
            // 
            this.chkGrpFooter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGrpFooter.ForeColor = System.Drawing.Color.Black;
            this.chkGrpFooter.Location = new System.Drawing.Point(8, 256);
            this.chkGrpFooter.Name = "chkGrpFooter";
            this.chkGrpFooter.Size = new System.Drawing.Size(136, 24);
            this.chkGrpFooter.TabIndex = 14;
            this.chkGrpFooter.Text = "Include group footer";
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
            this.bValueExpr.Location = new System.Drawing.Point(392, 45);
            this.bValueExpr.Name = "bValueExpr";
            this.bValueExpr.OverriddenSize = null;
            this.bValueExpr.Size = new System.Drawing.Size(22, 21);
            this.bValueExpr.TabIndex = 16;
            this.bValueExpr.Tag = "value";
            this.bValueExpr.Text = "fx";
            this.bValueExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bValueExpr.UseVisualStyleBackColor = true;
            this.bValueExpr.Click += new System.EventHandler(this.bValueExpr_Click);
            // 
            // bLabelExpr
            // 
            this.bLabelExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bLabelExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bLabelExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bLabelExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bLabelExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLabelExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bLabelExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bLabelExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLabelExpr.Location = new System.Drawing.Point(392, 147);
            this.bLabelExpr.Name = "bLabelExpr";
            this.bLabelExpr.OverriddenSize = null;
            this.bLabelExpr.Size = new System.Drawing.Size(22, 21);
            this.bLabelExpr.TabIndex = 17;
            this.bLabelExpr.Tag = "label";
            this.bLabelExpr.Text = "fx";
            this.bLabelExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLabelExpr.UseVisualStyleBackColor = true;
            this.bLabelExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // bParentExpr
            // 
            this.bParentExpr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bParentExpr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bParentExpr.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bParentExpr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bParentExpr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParentExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bParentExpr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bParentExpr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bParentExpr.Location = new System.Drawing.Point(392, 180);
            this.bParentExpr.Name = "bParentExpr";
            this.bParentExpr.OverriddenSize = null;
            this.bParentExpr.Size = new System.Drawing.Size(22, 21);
            this.bParentExpr.TabIndex = 18;
            this.bParentExpr.Tag = "parent";
            this.bParentExpr.Text = "fx";
            this.bParentExpr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bParentExpr.UseVisualStyleBackColor = true;
            this.bParentExpr.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // GroupingCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.bParentExpr);
            this.Controls.Add(this.bLabelExpr);
            this.Controls.Add(this.bValueExpr);
            this.Controls.Add(this.chkRepeatFooter);
            this.Controls.Add(this.chkGrpFooter);
            this.Controls.Add(this.chkRepeatHeader);
            this.Controls.Add(this.chkGrpHeader);
            this.Controls.Add(this.chkPBE);
            this.Controls.Add(this.chkPBS);
            this.Controls.Add(this.cbParentExpr);
            this.Controls.Add(this.lParent);
            this.Controls.Add(this.cbLabelExpr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bDown);
            this.Controls.Add(this.bUp);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.dgGroup);
            this.Name = "GroupingCtl";
            this.Size = new System.Drawing.Size(488, 304);
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public bool IsValid()
		{
			// Check to see if we have an expression
			bool bRows=HasRows();

			// If no rows and no data 
			if (!bRows && this.tbName.Text.Trim().Length == 0)
			{
				if (_GroupingParent.Name == "Details" ||
					_GroupingParent.Name == "List")
					return true;

				MessageBox.Show("Group must be defined.", "Grouping");
				return false;
			}

			// Grouping must have name
			XmlNode grouping = _Draw.GetNamedChildNode(_GroupingParent, "Grouping");
			string nerr = _Draw.GroupingNameCheck(grouping, this.tbName.Text);
			if (nerr != null)
			{
				MessageBox.Show(nerr, "Group Name in Error");
				return false;
			}

			if (!bRows)
			{
				MessageBox.Show("No expressions have been defined for the group.", "Group");
				return false;
			}

			if (_GroupingParent.Name != "DynamicSeries")
				return true;
			// DynamicSeries grouping must have a label for the legend
			if (this.cbLabelExpr.Text.Length > 0)
				return true;

			MessageBox.Show("Chart series must have label defined for the legend.", "Chart");

			return false;
		}

		private bool HasRows()
		{
			bool bRows=false;
			foreach (DataRow dr in _DataTable.Rows)
			{
				if (dr[0] == DBNull.Value)
					continue;
				string ge = (string) dr[0];
				if (ge.Length <= 0)
					continue;
				bRows = true;
				break;
			}
			return bRows;
		}

		public void Apply()
		{
			if (!HasRows())		// No expressions in grouping; get rid of grouping
			{
				_Draw.RemoveElement(_GroupingParent, "Grouping");	// can't have a grouping
				return;
			}

			// Get the group
			XmlNode grouping = _Draw.GetCreateNamedChildNode(_GroupingParent, "Grouping");

			_Draw.SetGroupName(grouping, tbName.Text.Trim());
			
			// Handle the label
			if (_GroupingParent.Name == "DynamicSeries" ||
				_GroupingParent.Name == "DynamicCategories")
			{
				if (this.cbLabelExpr.Text.Length > 0)
					_Draw.SetElement(_GroupingParent, "Label", cbLabelExpr.Text);
				else
					_Draw.RemoveElement(_GroupingParent, "Label");
			}
			else
			{
				if (this.cbLabelExpr.Text.Length > 0)
					_Draw.SetElement(grouping, "Label", cbLabelExpr.Text);
				else
					_Draw.RemoveElement(grouping, "Label");

				_Draw.SetElement(grouping, "PageBreakAtStart", this.chkPBS.Checked? "true": "false");
				_Draw.SetElement(grouping, "PageBreakAtEnd", this.chkPBE.Checked? "true": "false");
				if (cbParentExpr.Text.Length > 0)
					_Draw.SetElement(grouping, "Parent", cbParentExpr.Text);
				else
					_Draw.RemoveElement(grouping, "Parent");
			}


			// Loop thru and add all the group expressions
			XmlNode grpExprs = _Draw.GetCreateNamedChildNode(grouping, "GroupExpressions");
			grpExprs.RemoveAll();
			string firstexpr=null;
			foreach (DataRow dr in _DataTable.Rows)
			{
				if (dr[0] == DBNull.Value)
					continue;
				string ge = (string) dr[0];
				if (ge.Length <= 0)
					continue;
				_Draw.CreateElement(grpExprs, "GroupExpression", ge);
				if (firstexpr == null)
					firstexpr = ge;
			}
			if (!grpExprs.HasChildNodes)
			{	// With no group expressions there are no groups
				grouping.RemoveChild(grpExprs);
				grouping.ParentNode.RemoveChild(grouping);
				grouping = null;
			}

			if (_GroupingParent.Name == "TableGroup" && grouping != null)
			{
				if (this.chkGrpHeader.Checked)
				{
					XmlNode header = _Draw.GetCreateNamedChildNode(_GroupingParent, "Header");
					_Draw.SetElement(header, "RepeatOnNewPage", chkRepeatHeader.Checked? "true": "false");
					XmlNode tblRows = _Draw.GetCreateNamedChildNode(header, "TableRows");
					if (!tblRows.HasChildNodes)
					{	// We need to create a row
						_Draw.InsertTableRow(tblRows);
					}
				}
				else
				{
					_Draw.RemoveElement(_GroupingParent, "Header");
				}

				if (this.chkGrpFooter.Checked)
				{
					XmlNode footer = _Draw.GetCreateNamedChildNode(_GroupingParent, "Footer");
					_Draw.SetElement(footer, "RepeatOnNewPage", chkRepeatFooter.Checked? "true": "false");
					XmlNode tblRows = _Draw.GetCreateNamedChildNode(footer, "TableRows");
					if (!tblRows.HasChildNodes)
					{	// We need to create a row
						_Draw.InsertTableRow(tblRows);
					}
				}
				else
				{
					_Draw.RemoveElement(_GroupingParent, "Footer");
				}
			}
			else if (_GroupingParent.Name == "DynamicColumns" ||
					 _GroupingParent.Name == "DynamicRows")
			{
				XmlNode ritems = _Draw.GetNamedChildNode(_GroupingParent, "ReportItems");
				if (ritems == null)
					ritems = _Draw.GetCreateNamedChildNode(_GroupingParent, "ReportItems");
				XmlNode item = ritems.FirstChild;
				if (item == null)
				{
					item = _Draw.GetCreateNamedChildNode(ritems, "Textbox");
					XmlNode vnode = _Draw.GetCreateNamedChildNode(item, "Value");
					vnode.InnerText = firstexpr == null? "": firstexpr;
				}
			}
		}

		private void bDelete_Click(object sender, System.EventArgs e)
		{
			int cr = dgGroup.CurrentRowIndex;
			if (cr < 0)		// already at the top
				return;
			else if (cr == 0)
			{
				DataRow dr = _DataTable.Rows[0];
				dr[0] = null;
			}

			this._DataTable.Rows.RemoveAt(cr);
		}

		private void bUp_Click(object sender, System.EventArgs e)
		{
			int cr = dgGroup.CurrentRowIndex;
			if (cr <= 0)		// already at the top
				return;
			
			SwapRow(_DataTable.Rows[cr-1], _DataTable.Rows[cr]);
			dgGroup.CurrentRowIndex = cr-1;
		}

		private void bDown_Click(object sender, System.EventArgs e)
		{
			int cr = dgGroup.CurrentRowIndex;
			if (cr < 0)			// invalid index
				return;
			if (cr + 1 >= _DataTable.Rows.Count)
				return;			// already at end
			
			SwapRow(_DataTable.Rows[cr+1], _DataTable.Rows[cr]);
			dgGroup.CurrentRowIndex = cr+1;
		}

		private void SwapRow(DataRow tdr, DataRow fdr)
		{
			// column 1
			object save = tdr[0];
			tdr[0] = fdr[0];
			fdr[0] = save;
			// column 2
			save = tdr[1];
			tdr[1] = fdr[1];
			fdr[1] = save;
			// column 3
			save = tdr[2];
			tdr[2] = fdr[2];
			fdr[2] = save;
			return;
		}

		private void tbName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			bool bRows=HasRows();

			// If no rows and no data in name it's ok
			if (!bRows && this.tbName.Text.Trim().Length == 0)
				return;

			if (!ReportNames.IsNameValid(tbName.Text))
			{
				e.Cancel = true;
				MessageBox.Show(string.Format("{0} is an invalid name.", tbName.Text), "Name");
			}
		}

		private void bValueExpr_Click(object sender, System.EventArgs e)
		{
			int cr = dgGroup.CurrentRowIndex;
			if (cr < 0)
			{	// No rows yet; create one
				string[] rowValues = new string[1];
				rowValues[0] = null;

				_DataTable.Rows.Add(rowValues);
				cr = 0;
			}
			DataGridCell dgc = dgGroup.CurrentCell;
			int cc = dgc.ColumnNumber;
			DataRow dr = _DataTable.Rows[cr];
			string cv = dr[cc] as string;

			DialogExprEditor ee = new DialogExprEditor(_Draw, cv, _GroupingParent, false);
            try
            {
                DialogResult dlgr = ee.ShowDialog();
                if (dlgr == DialogResult.OK)
                    dr[cc] = ee.Expression;
            }
            finally
            {
                ee.Dispose();
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
				case "label":
					c = this.cbLabelExpr;
					break;
				case "parent":
					c = this.cbParentExpr;
					break;
			}

			if (c == null)
				return;

			DialogExprEditor ee = new DialogExprEditor(_Draw, c.Text, _GroupingParent, false);
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
