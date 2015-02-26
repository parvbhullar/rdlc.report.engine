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
using System.IO;
using Oranikle.Report.Engine;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// QueryParametersCtl provides values for the DataSet Query QueryParameters rdl elements
	/// </summary>
	internal class QueryParametersCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		private DesignXmlDraw _Draw;
		private DataSetValues _dsv;
		private DataGridTextBoxColumn dgtbName;
		private DataGridTextBoxColumn dgtbValue;
		private System.Windows.Forms.DataGridTableStyle dgTableStyle;
		private System.Windows.Forms.DataGrid dgParms;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal QueryParametersCtl(DesignXmlDraw dxDraw, DataSetValues dsv)
		{
			_Draw = dxDraw;
			_dsv = dsv;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			// Initialize the DataGrid columns
			dgtbName = new DataGridTextBoxColumn();
			dgtbValue = new DataGridTextBoxColumn();

			this.dgTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] {
															this.dgtbName,
															this.dgtbValue});
			// 
			// dgtbFE
			// 
			dgtbName.HeaderText = "Parameter Name";
			dgtbName.MappingName = "Name";
			dgtbName.Width = 75;
			// 
			// dgtbValue
			// 
			this.dgtbValue.HeaderText = "Value";
			this.dgtbValue.MappingName = "Value";
			this.dgtbValue.Width = 75;
//			string[] parms = _Draw.GetReportParameters(true);
//			if (parms != null)
//				dgtbFV.CB.Items.AddRange(parms);

			// Initialize the DataGrid
			this.dgParms.DataSource = _dsv.QueryParameters;

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
            this.dgParms.Size = new System.Drawing.Size(384, 168);
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
            // QueryParametersCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.dgParms);
            this.Name = "QueryParametersCtl";
            this.Size = new System.Drawing.Size(464, 304);
            ((System.ComponentModel.ISupportInitialize)(this.dgParms)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
 

		public bool IsValid()
		{
			return true;
		}

		public void Apply()
		{
			return;			// the apply is done as part of the DataSetsCtl
		}

	}
}
