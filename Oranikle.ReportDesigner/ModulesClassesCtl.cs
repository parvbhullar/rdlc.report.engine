/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for ModulesClassesCtl.
	/// </summary>
	internal class ModulesClassesCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		private DesignXmlDraw _Draw;
		private DataTable _DTCM;
		private DataTable _DTCL;

		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.StyledButton bDeleteCM;
		private System.Windows.Forms.DataGrid dgCodeModules;
		private Oranikle.Studio.Controls.StyledButton bDeleteClass;
		private System.Windows.Forms.DataGrid dgClasses;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridTableStyle dgTableStyleCM;
		private System.Windows.Forms.DataGridTableStyle dgTableStyleCL;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal ModulesClassesCtl(DesignXmlDraw dxDraw)
		{
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			BuildCodeModules();
			BuildClasses();
		}

		private void BuildCodeModules()
		{
			XmlNode rNode = _Draw.GetReportNode();

			// Initialize the DataGrid columns
			DataGridTextBoxColumn dgtbCM = new DataGridTextBoxColumn();

			this.dgTableStyleCM.GridColumnStyles.AddRange(new DataGridColumnStyle[] {dgtbCM});
			// 
			// dgtbGE
			// 
			dgtbCM.HeaderText = "Code Module";
			dgtbCM.MappingName = "Code Module";
			dgtbCM.Width = 175;

			// Initialize the DataTable
			_DTCM = new DataTable();
			_DTCM.Columns.Add(new DataColumn("Code Module", typeof(string)));

			string[] rowValues = new string[1];
			XmlNode cmsNode = _Draw.GetNamedChildNode(rNode, "CodeModules");

			if (cmsNode != null)
				foreach (XmlNode cmNode in cmsNode.ChildNodes)
				{
					if (cmNode.NodeType != XmlNodeType.Element || 
						cmNode.Name != "CodeModule")
						continue;
					rowValues[0] = cmNode.InnerText;

					_DTCM.Rows.Add(rowValues);
				}
			this.dgCodeModules.DataSource = _DTCM;
			DataGridTableStyle ts = dgCodeModules.TableStyles[0];
			ts.GridColumnStyles[0].Width = 330;
		}

		private void BuildClasses()
		{
			XmlNode rNode = _Draw.GetReportNode();

			// Initialize the DataGrid columns
			DataGridTextBoxColumn dgtbCL = new DataGridTextBoxColumn();
			DataGridTextBoxColumn dgtbIn = new DataGridTextBoxColumn();

			this.dgTableStyleCL.GridColumnStyles.AddRange(new DataGridColumnStyle[] {dgtbCL, dgtbIn});

			dgtbCL.HeaderText = "Class Name";
			dgtbCL.MappingName = "Class Name";
			dgtbCL.Width = 80;

			dgtbIn.HeaderText = "Instance Name";
			dgtbIn.MappingName = "Instance Name";
			dgtbIn.Width = 80;

			// Initialize the DataTable
			_DTCL = new DataTable();
			_DTCL.Columns.Add(new DataColumn("Class Name", typeof(string)));
			_DTCL.Columns.Add(new DataColumn("Instance Name", typeof(string)));

			string[] rowValues = new string[2];
			XmlNode clsNode = _Draw.GetNamedChildNode(rNode, "Classes");

			if (clsNode != null)
				foreach (XmlNode clNode in clsNode.ChildNodes)
				{
					if (clNode.NodeType != XmlNodeType.Element || 
						clNode.Name != "Class")
						continue;

					XmlNode node = _Draw.GetNamedChildNode(clNode, "ClassName");
					if (node != null)
						rowValues[0] = node.InnerText;

					node = _Draw.GetNamedChildNode(clNode, "InstanceName");
					if (node != null)
						rowValues[1] = node.InnerText;

					_DTCL.Rows.Add(rowValues);
				}
			this.dgClasses.DataSource = _DTCL;
			DataGridTableStyle ts = dgClasses.TableStyles[0];
			ts.GridColumnStyles[0].Width = 200;
			ts.GridColumnStyles[1].Width = 130;
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
            this.bDeleteCM = new Oranikle.Studio.Controls.StyledButton();
            this.dgCodeModules = new System.Windows.Forms.DataGrid();
            this.dgTableStyleCM = new System.Windows.Forms.DataGridTableStyle();
            this.bDeleteClass = new Oranikle.Studio.Controls.StyledButton();
            this.dgClasses = new System.Windows.Forms.DataGrid();
            this.dgTableStyleCL = new System.Windows.Forms.DataGridTableStyle();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCodeModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(448, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the names of the code module for use in expressions (e.g. MyRoutines.dll)";
            // 
            // bDeleteCM
            // 
            this.bDeleteCM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDeleteCM.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDeleteCM.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDeleteCM.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDeleteCM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDeleteCM.Font = new System.Drawing.Font("Arial", 9F);
            this.bDeleteCM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDeleteCM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDeleteCM.Location = new System.Drawing.Point(400, 32);
            this.bDeleteCM.Name = "bDeleteCM";
            this.bDeleteCM.OverriddenSize = null;
            this.bDeleteCM.Size = new System.Drawing.Size(48, 21);
            this.bDeleteCM.TabIndex = 4;
            this.bDeleteCM.Text = "Delete";
            this.bDeleteCM.UseVisualStyleBackColor = true;
            this.bDeleteCM.Click += new System.EventHandler(this.bDeleteCM_Click);
            // 
            // dgCodeModules
            // 
            this.dgCodeModules.BackgroundColor = System.Drawing.Color.White;
            this.dgCodeModules.CaptionVisible = false;
            this.dgCodeModules.DataMember = "";
            this.dgCodeModules.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgCodeModules.Location = new System.Drawing.Point(16, 32);
            this.dgCodeModules.Name = "dgCodeModules";
            this.dgCodeModules.Size = new System.Drawing.Size(376, 88);
            this.dgCodeModules.TabIndex = 3;
            this.dgCodeModules.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgTableStyleCM});
            // 
            // dgTableStyleCM
            // 
            this.dgTableStyleCM.DataGrid = this.dgCodeModules;
            this.dgTableStyleCM.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // bDeleteClass
            // 
            this.bDeleteClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDeleteClass.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDeleteClass.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDeleteClass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDeleteClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDeleteClass.Font = new System.Drawing.Font("Arial", 9F);
            this.bDeleteClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDeleteClass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDeleteClass.Location = new System.Drawing.Point(400, 160);
            this.bDeleteClass.Name = "bDeleteClass";
            this.bDeleteClass.OverriddenSize = null;
            this.bDeleteClass.Size = new System.Drawing.Size(48, 21);
            this.bDeleteClass.TabIndex = 7;
            this.bDeleteClass.Text = "Delete";
            this.bDeleteClass.UseVisualStyleBackColor = true;
            this.bDeleteClass.Click += new System.EventHandler(this.bDeleteClass_Click);
            // 
            // dgClasses
            // 
            this.dgClasses.BackgroundColor = System.Drawing.Color.White;
            this.dgClasses.CaptionVisible = false;
            this.dgClasses.DataMember = "";
            this.dgClasses.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgClasses.Location = new System.Drawing.Point(16, 160);
            this.dgClasses.Name = "dgClasses";
            this.dgClasses.Size = new System.Drawing.Size(376, 88);
            this.dgClasses.TabIndex = 6;
            this.dgClasses.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgTableStyleCL});
            // 
            // dgTableStyleCL
            // 
            this.dgTableStyleCL.DataGrid = this.dgClasses;
            this.dgTableStyleCL.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(448, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter the classes with names that will be instantiated for use in expressions";
            // 
            // ModulesClassesCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.bDeleteClass);
            this.Controls.Add(this.dgClasses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bDeleteCM);
            this.Controls.Add(this.dgCodeModules);
            this.Controls.Add(this.label1);
            this.Name = "ModulesClassesCtl";
            this.Size = new System.Drawing.Size(472, 288);
            ((System.ComponentModel.ISupportInitialize)(this.dgCodeModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgClasses)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		public bool IsValid()
		{
			return true;
		}

		public void Apply()
		{
			ApplyCodeModules();
			ApplyClasses();
		}
		
		private void ApplyCodeModules()
		{
			XmlNode rNode = _Draw.GetReportNode(); 
			_Draw.RemoveElement(rNode, "CodeModules");
			if (!HasRows(this._DTCM, 1))
				return;				

			// Set the CodeModules
			XmlNode cms = _Draw.CreateElement(rNode, "CodeModules", null);
			foreach (DataRow dr in _DTCM.Rows)
			{
				if (dr[0] == DBNull.Value ||
					dr[0].ToString().Trim().Length <= 0)
					continue;

				_Draw.CreateElement(cms, "CodeModule", dr[0].ToString());
			}
		}

		private void ApplyClasses()
		{
			XmlNode rNode = _Draw.GetReportNode(); 
			_Draw.RemoveElement(rNode, "Classes");
			if (!HasRows(this._DTCL, 2))
				return;				

			// Set the classes
			XmlNode cs = _Draw.CreateElement(rNode, "Classes", null);
			foreach (DataRow dr in _DTCL.Rows)
			{
				if (dr[0] == DBNull.Value ||
					dr[1] == DBNull.Value ||
					dr[0].ToString().Trim().Length <= 0 ||
					dr[1].ToString().Trim().Length <= 0)
					continue;

				XmlNode c = _Draw.CreateElement(cs, "Class", null);
				_Draw.CreateElement(c, "ClassName", dr[0].ToString());
				_Draw.CreateElement(c, "InstanceName", dr[1].ToString());
			}

		}

		private bool HasRows(DataTable dt, int columns)
		{
			foreach (DataRow dr in dt.Rows)
			{
				bool bCompleteRow = true;
				for (int i=0; i < columns; i++)
				{
					if (dr[i] == DBNull.Value)
					{
						bCompleteRow = false;
						break;
					}
					string ge = (string) dr[i];
					if (ge.Length <= 0)
					{
						bCompleteRow = false;
						break;
					}
				}
				if (bCompleteRow)
					return true;
			}
			return false;
		}

		private void bDeleteCM_Click(object sender, System.EventArgs e)
		{
			int cr = this.dgCodeModules.CurrentRowIndex;
			if (cr < 0)		// already at the top
				return;

			_DTCM.Rows.RemoveAt(cr);

		}

		private void bDeleteClass_Click(object sender, System.EventArgs e)
		{
			int cr = this.dgClasses.CurrentRowIndex;
			if (cr < 0)		// already at the top
				return;

			_DTCL.Rows.RemoveAt(cr);
		}
	}
}
