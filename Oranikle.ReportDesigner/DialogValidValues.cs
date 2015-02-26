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
using System.IO;
using Oranikle.Report.Engine;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// DialogValidValues allow user to provide ValidValues: Value and Label lists
	/// </summary>
	internal class DialogValidValues : System.Windows.Forms.Form
	{
		private DataTable _DataTable;
		private DataGridTextBoxColumn dgtbLabel;
		private DataGridTextBoxColumn dgtbValue;
		private System.Windows.Forms.DataGridTableStyle dgTableStyle;
		private System.Windows.Forms.DataGrid dgParms;
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		private Oranikle.Studio.Controls.StyledButton bDelete;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal DialogValidValues(List<ParameterValueItem> list)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues(list);			
		}

        internal List<ParameterValueItem> ValidValues
		{
			get
			{
                List<ParameterValueItem> list = new List<ParameterValueItem>();
				foreach (DataRow dr in _DataTable.Rows)
				{
					if (dr[0] == DBNull.Value)
						continue;
					string val = (string) dr[0];

					if (val.Length <= 0)
						continue;

					string label;
					if (dr[1] == DBNull.Value)
						label = null;
					else
						label = (string) dr[1];

					ParameterValueItem pvi = new ParameterValueItem();
					pvi.Value = val;
					pvi.Label = label;
					list.Add(pvi);
				}
				return list.Count > 0? list: null;
			}
		}

        private void InitValues(List<ParameterValueItem> list)
		{
			// Initialize the DataGrid columns
			dgtbLabel = new DataGridTextBoxColumn();
			dgtbValue = new DataGridTextBoxColumn();

			this.dgTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] {
															this.dgtbValue,
															this.dgtbLabel});
			// 
			// dgtbFE
			// 
			dgtbValue.HeaderText = "Value";
			dgtbValue.MappingName = "Value";
			dgtbValue.Width = 75;
			// 
			// dgtbValue
			// 
			this.dgtbLabel.HeaderText = "Label";
			this.dgtbLabel.MappingName = "Label";
			this.dgtbLabel.Width = 75;

			// Initialize the DataGrid
			//this.dgParms.DataSource = _dsv.QueryParameters;

			_DataTable = new DataTable();
			_DataTable.Columns.Add(new DataColumn("Value", typeof(string)));
			_DataTable.Columns.Add(new DataColumn("Label", typeof(string)));

			string[] rowValues = new string[2];
			if (list != null)
			foreach (ParameterValueItem pvi in list)
			{
				rowValues[0] = pvi.Value;
				rowValues[1] = pvi.Label;

				_DataTable.Rows.Add(rowValues);
			}

			this.dgParms.DataSource = _DataTable;

			////
			DataGridTableStyle ts = dgParms.TableStyles[0];
			ts.GridColumnStyles[0].Width = 140;
			ts.GridColumnStyles[1].Width = 140;
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
            this.dgParms = new System.Windows.Forms.DataGrid();
            this.dgTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.bDelete = new Oranikle.Studio.Controls.StyledButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgParms)).BeginInit();
            this.SuspendLayout();
            // 
            // dgParms
            // 
            this.dgParms.BackgroundColor = System.Drawing.Color.White;
            this.dgParms.CaptionVisible = false;
            this.dgParms.DataMember = "";
            this.dgParms.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgParms.Location = new System.Drawing.Point(8, 8);
            this.dgParms.Name = "dgParms";
            this.dgParms.Size = new System.Drawing.Size(320, 168);
            this.dgParms.TabIndex = 2;
            this.dgParms.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgTableStyle});
            // 
            // dgTableStyle
            // 
            this.dgTableStyle.AllowSorting = false;
            this.dgTableStyle.DataGrid = this.dgParms;
            this.dgTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Font = new System.Drawing.Font("Arial", 9F);
            this.bOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOK.Location = new System.Drawing.Point(216, 192);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 21);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCancel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCancel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.bCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(312, 192);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 21);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
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
            this.bDelete.Location = new System.Drawing.Point(336, 16);
            this.bDelete.Name = "bDelete";
            this.bDelete.OverriddenSize = null;
            this.bDelete.Size = new System.Drawing.Size(48, 21);
            this.bDelete.TabIndex = 5;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // DialogValidValues
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(400, 228);
            this.ControlBox = false;
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.dgParms);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogValidValues";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Valid Values";
            ((System.ComponentModel.ISupportInitialize)(this.dgParms)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void bDelete_Click(object sender, System.EventArgs e)
		{
			this._DataTable.Rows.RemoveAt(this.dgParms.CurrentRowIndex);
		}


	}
}
