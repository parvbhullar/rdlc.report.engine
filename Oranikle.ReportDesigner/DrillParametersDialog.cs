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
	/// Drillthrough reports; pick report and specify parameters
	/// </summary>
	internal class DrillParametersDialog : System.Windows.Forms.Form
	{
		private string _DrillReport;
		private DataTable _DataTable;
		private DataGridTextBoxColumn dgtbName;
		private DataGridTextBoxColumn dgtbValue;
		private DataGridTextBoxColumn dgtbOmit;
		private System.Windows.Forms.DataGridTableStyle dgTableStyle;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.StyledButton bFile;
		private Oranikle.Studio.Controls.CustomTextControl tbReportFile;
		private System.Windows.Forms.DataGrid dgParms;
		private Oranikle.Studio.Controls.StyledButton bRefreshParms;
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal DrillParametersDialog(string report, List<DrillParameter> parameters)
		{
			_DrillReport = report;
			
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues(parameters);
		}

        private void InitValues(List<DrillParameter> parameters)
		{
			this.tbReportFile.Text = _DrillReport;

			// Initialize the DataGrid columns
			dgtbName = new DataGridTextBoxColumn();
			dgtbValue = new DataGridTextBoxColumn();
			dgtbOmit = new DataGridTextBoxColumn();

			this.dgTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] {
															this.dgtbName,
															this.dgtbValue,
															this.dgtbOmit});
			// 
			// dgtbFE
			// 
			dgtbName.HeaderText = "Parameter Name";
			dgtbName.MappingName = "ParameterName";
			dgtbName.Width = 75;
			// 
			// dgtbValue
			// 
			this.dgtbValue.HeaderText = "Value";
			this.dgtbValue.MappingName = "Value";
			this.dgtbValue.Width = 75;
			// 
			// dgtbOmit
			// 
			this.dgtbOmit.HeaderText = "Omit";
			this.dgtbOmit.MappingName = "Omit";
			this.dgtbOmit.Width = 75;

			// Initialize the DataTable
			_DataTable = new DataTable();	  
			
			_DataTable.Columns.Add(new DataColumn("ParameterName", typeof(string)));
			_DataTable.Columns.Add(new DataColumn("Value", typeof(string)));
			_DataTable.Columns.Add(new DataColumn("Omit", typeof(string)));

			string[] rowValues = new string[3];

			if (parameters != null)
			foreach (DrillParameter dp in parameters)
			{
				rowValues[0] = dp.ParameterName;
				rowValues[1] = dp.ParameterValue;
				rowValues[2] = dp.ParameterOmit;

				_DataTable.Rows.Add(rowValues);
			}
			// Don't allow new rows; do this by creating a DataView over the DataTable
//			DataView dv = new DataView(_DataTable);	// this has bad side effects
//			dv.AllowNew = false;
			this.dgParms.DataSource = _DataTable;

			DataGridTableStyle ts = dgParms.TableStyles[0];
			
			ts.GridColumnStyles[0].Width = 140;
			ts.GridColumnStyles[0].ReadOnly = true;
			ts.GridColumnStyles[1].Width = 140;
			ts.GridColumnStyles[2].Width = 70;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrillParametersDialog));
            this.dgParms = new System.Windows.Forms.DataGrid();
            this.dgTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tbReportFile = new Oranikle.Studio.Controls.CustomTextControl();
            this.bFile = new Oranikle.Studio.Controls.StyledButton();
            this.bRefreshParms = new Oranikle.Studio.Controls.StyledButton();
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgParms)).BeginInit();
            this.SuspendLayout();
            // 
            // dgParms
            // 
            this.dgParms.BackgroundColor = System.Drawing.Color.White;
            this.dgParms.CaptionVisible = false;
            this.dgParms.DataMember = "";
            this.dgParms.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgParms.Location = new System.Drawing.Point(8, 40);
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Report name";
            // 
            // tbReportFile
            // 
            this.tbReportFile.AddX = 0;
            this.tbReportFile.AddY = 0;
            this.tbReportFile.AllowSpace = false;
            this.tbReportFile.BorderColor = System.Drawing.Color.LightGray;
            this.tbReportFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReportFile.ChangeVisibility = false;
            this.tbReportFile.ChildControl = null;
            this.tbReportFile.ConvertEnterToTab = true;
            this.tbReportFile.ConvertEnterToTabForDialogs = false;
            this.tbReportFile.Decimals = 0;
            this.tbReportFile.DisplayList = new object[0];
            this.tbReportFile.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbReportFile.Location = new System.Drawing.Point(104, 8);
            this.tbReportFile.Name = "tbReportFile";
            this.tbReportFile.OnDropDownCloseFocus = true;
            this.tbReportFile.SelectType = 0;
            this.tbReportFile.Size = new System.Drawing.Size(312, 20);
            this.tbReportFile.TabIndex = 4;
            this.tbReportFile.UseValueForChildsVisibilty = false;
            this.tbReportFile.Value = true;
            // 
            // bFile
            // 
            this.bFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bFile.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bFile.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bFile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFile.Font = new System.Drawing.Font("Arial", 9F);
            this.bFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFile.Location = new System.Drawing.Point(424, 8);
            this.bFile.Name = "bFile";
            this.bFile.OverriddenSize = null;
            this.bFile.Size = new System.Drawing.Size(24, 21);
            this.bFile.TabIndex = 5;
            this.bFile.Text = "...";
            this.bFile.UseVisualStyleBackColor = true;
            this.bFile.Click += new System.EventHandler(this.bFile_Click);
            // 
            // bRefreshParms
            // 
            this.bRefreshParms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bRefreshParms.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bRefreshParms.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bRefreshParms.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bRefreshParms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRefreshParms.Font = new System.Drawing.Font("Arial", 9F);
            this.bRefreshParms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bRefreshParms.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRefreshParms.Location = new System.Drawing.Point(400, 40);
            this.bRefreshParms.Name = "bRefreshParms";
            this.bRefreshParms.OverriddenSize = null;
            this.bRefreshParms.Size = new System.Drawing.Size(56, 21);
            this.bRefreshParms.TabIndex = 10;
            this.bRefreshParms.Text = "Refresh";
            this.bRefreshParms.UseVisualStyleBackColor = true;
            this.bRefreshParms.Click += new System.EventHandler(this.bRefreshParms_Click);
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
            this.bOK.Location = new System.Drawing.Point(288, 216);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 11;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCancel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCancel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCancel.CausesValidation = false;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.bCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(376, 216);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 12;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // DrillParametersDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(464, 248);
            this.ControlBox = false;
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bRefreshParms);
            this.Controls.Add(this.bFile);
            this.Controls.Add(this.tbReportFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgParms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DrillParametersDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Specify Drillthrough Report and Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.dgParms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public string DrillthroughReport
		{
			get {return this._DrillReport;}
		}

        public List<DrillParameter> DrillParameters
		{
			get 
			{
                List<DrillParameter> parms = new List<DrillParameter>();

				// Loop thru and add all the filters
				foreach (DataRow dr in _DataTable.Rows)
				{
					if (dr[0] == DBNull.Value || dr[1] == DBNull.Value)
						continue;
					string name = (string) dr[0];
					string val = (string) dr[1];
					string omit = dr[2] == DBNull.Value? "false": (string) dr[2];
					if (name.Length <= 0 || val.Length <= 0)
						continue;
					DrillParameter dp = new DrillParameter(name, val, omit);
					parms.Add(dp);
				}
				if (parms.Count == 0)
					return null;
				return parms;
			}
		}

		private void bFile_Click(object sender, System.EventArgs e)
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

                    tbReportFile.Text = file;
                }
            }
            finally
            {
                ofd.Dispose();
            }
		}

		private void bRefreshParms_Click(object sender, System.EventArgs e)
		{
			// Obtain the source
			Cursor savec = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;	// this can take some time
			try
			{
				string filename="";
				if (tbReportFile.Text.Length > 0)
					filename = tbReportFile.Text + ".rdl";

				filename = GetFileNameWithPath(filename);

				string source = this.GetSource(filename);
				if (source == null)
					return;						// error: message already displayed

				// Compile the report
                Oranikle.Report.Engine.Report report = this.GetReport(source, filename);
				if (report == null)
					return;					// error: message already displayed
			
				ICollection rps = report.UserReportParameters;
				string[] rowValues = new string[3];
				_DataTable.Rows.Clear();
				foreach (UserReportParameter rp in rps)
				{
					rowValues[0] = rp.Name;
					rowValues[1] = "";
					rowValues[2] = "false";

					_DataTable.Rows.Add(rowValues);
				}
				this.dgParms.Refresh();
				this.dgParms.Focus();
			}
			finally
			{
				Cursor.Current = savec;
			}
		}

		private string GetFileNameWithPath(string file)
		{	// todo: should prefix this with the path of the open file
			
			return file;
		}

		private string GetSource(string file)
		{
			StreamReader fs=null;
			string prog=null;

			try
			{
				fs = new StreamReader(file);
				prog = fs.ReadToEnd();
			}
			catch(Exception e)
			{
				prog = null;
				MessageBox.Show(e.Message, "Error reading report file");
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}
			return prog;
		}

		private Oranikle.Report.Engine.Report GetReport(string prog, string file)
		{
			// Now parse the file
			RDLParser rdlp;
            Oranikle.Report.Engine.Report r;
			try
			{
				rdlp =  new RDLParser(prog);
				string folder = Path.GetDirectoryName(file);
				if (folder == "")
				{
					folder = Environment.CurrentDirectory;
				}
				rdlp.Folder = folder;

				r = rdlp.Parse();
				if (r.ErrorMaxSeverity > 4) 
				{
					MessageBox.Show(string.Format("Report {0} has errors and cannot be processed.", "Report"));
					r = null;			// don't return when severe errors
				}
			}
			catch(Exception e)
			{
				r = null;
				MessageBox.Show(e.Message, "Report load failed");
			}
			return r;
		}

		private void DrillParametersDialog_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach (DataRow dr in _DataTable.Rows)
			{
				if (dr[1] == DBNull.Value)
				{
					e.Cancel = true;
					break;
				}
				string val = (string) dr[1];
				if (val.Length <= 0)
				{
					e.Cancel = true;
					break;
				}
			}
			if (e.Cancel)
			{
				MessageBox.Show("Value must be specified for every parameter", this.Text);
			}
		}

		private void bOK_Click(object sender, System.EventArgs e)
		{
			CancelEventArgs ce = new CancelEventArgs();
			DrillParametersDialog_Validating(this, ce);
			if (ce.Cancel)
			{
				DialogResult = DialogResult.None;
				return;
			}
			DialogResult = DialogResult.OK;
		}

	}
	internal class DrillParameter
	{
		internal string ParameterName;
		internal string ParameterValue;
		internal string ParameterOmit;
		
		internal DrillParameter(string name, string pvalue, string omit)
		{
			ParameterName = name;
			ParameterValue = pvalue;
			ParameterOmit = omit;
		}
	}
}
