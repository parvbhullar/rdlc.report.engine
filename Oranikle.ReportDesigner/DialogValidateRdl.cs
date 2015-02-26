/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for DialogAbout.
	/// </summary>
	public class DialogValidateRdl : System.Windows.Forms.Form
	{
		private readonly string SCHEMA2003 = "http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition";
		private readonly string SCHEMA2003NAME = "http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition/ReportDefinition.xsd";
		private readonly string SCHEMA2005 = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";
		private readonly string SCHEMA2005NAME = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition/ReportDefinition.xsd";
		static internal readonly string MSDESIGNERSCHEMA = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";
		static internal readonly string DESIGNERSCHEMA = "http://www.oranikle.in/schemas";

		private int _ValidationErrorCount;
		private int _ValidationWarningCount;
		private DesignerForm _RdlDesigner;

		private Oranikle.Studio.Controls.StyledButton bClose;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.StyledButton bValidate;
		private System.Windows.Forms.ListBox lbSchemaErrors;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DialogValidateRdl(DesignerForm designer)
		{
			_RdlDesigner = designer;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			return;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogValidateRdl));
            this.bClose = new Oranikle.Studio.Controls.StyledButton();
            this.lbSchemaErrors = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bValidate = new Oranikle.Studio.Controls.StyledButton();
            this.SuspendLayout();
            // 
            // bClose
            // 
            this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bClose.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bClose.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bClose.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bClose.CausesValidation = false;
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClose.Font = new System.Drawing.Font("Arial", 9F);
            this.bClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bClose.Location = new System.Drawing.Point(575, 333);
            this.bClose.Name = "bClose";
            this.bClose.OverriddenSize = null;
            this.bClose.Size = new System.Drawing.Size(75, 21);
            this.bClose.TabIndex = 3;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // lbSchemaErrors
            // 
            this.lbSchemaErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSchemaErrors.HorizontalScrollbar = true;
            this.lbSchemaErrors.Location = new System.Drawing.Point(9, 54);
            this.lbSchemaErrors.Name = "lbSchemaErrors";
            this.lbSchemaErrors.Size = new System.Drawing.Size(646, 264);
            this.lbSchemaErrors.TabIndex = 2;
            this.lbSchemaErrors.DoubleClick += new System.EventHandler(this.lbSchemaErrors_DoubleClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(647, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // bValidate
            // 
            this.bValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bValidate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bValidate.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bValidate.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bValidate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bValidate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValidate.Font = new System.Drawing.Font("Arial", 9F);
            this.bValidate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bValidate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bValidate.Location = new System.Drawing.Point(490, 333);
            this.bValidate.Name = "bValidate";
            this.bValidate.OverriddenSize = null;
            this.bValidate.Size = new System.Drawing.Size(75, 21);
            this.bValidate.TabIndex = 1;
            this.bValidate.Text = "Validate";
            this.bValidate.UseVisualStyleBackColor = true;
            this.bValidate.Click += new System.EventHandler(this.bValidate_Click);
            // 
            // DialogValidateRdl
            // 
            this.AcceptButton = this.bValidate;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bClose;
            this.ClientSize = new System.Drawing.Size(665, 360);
            this.Controls.Add(this.bValidate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbSchemaErrors);
            this.Controls.Add(this.bClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogValidateRdl";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Validate RDL Syntax";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DialogValidateRdl_Closing);
            this.ResumeLayout(false);

		}
		#endregion

		private void bValidate_Click(object sender, System.EventArgs e)
		{
			MDIChild mc = _RdlDesigner.ActiveMdiChild as MDIChild;
			if (mc == null || mc.DesignTab != "edit")
			{
				MessageBox.Show("Select the 'RDL Text' tab before validating.");
				return;
			}
			string syntax = mc.SourceRdl;
			bool bNone = true;
			bool b2005 = true;
			Cursor saveCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			StringReader sr = null;
			XmlTextReader tr = null; 
			XmlReader vr = null; 
            
			try
			{
				// Find the namespace information in the <Report> element.
				//   We could be more precise and really parse it but it doesn't really help
				//   since we don't know the name and location of where the actual .xsd file is
				//   in the general case.  (e.g. xmlns="..." doesn't contain name of the .xsd file.
				int ir = syntax.IndexOf("<Report");
				if (ir >= 0)
				{
					int er = syntax.IndexOf(">", ir);
					if (er >= 0)
					{
						if (syntax.IndexOf("xmlns", ir, er-ir) >= 0)
						{
							bNone = false;
							if (syntax.IndexOf("2005", ir, er-ir) < 0)
								b2005 = false;
						}
					}
				}

				_ValidationErrorCount=0;
				_ValidationWarningCount=0;
				this.lbSchemaErrors.Items.Clear();
				sr = new StringReader(syntax);
				tr = new XmlTextReader(sr);
                XmlReaderSettings xrs = new XmlReaderSettings();
                xrs.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
                xrs.ValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes |
                    XmlSchemaValidationFlags.ProcessIdentityConstraints |
                    XmlSchemaValidationFlags.ProcessSchemaLocation |
                    XmlSchemaValidationFlags.ProcessInlineSchema;

				// add any schemas needed
				if (!bNone)
				{
					if (b2005)
						xrs.Schemas.Add(SCHEMA2005, SCHEMA2005NAME);  
					else
						xrs.Schemas.Add(SCHEMA2003, SCHEMA2003NAME);  
				}
				// we always use the designer schema
				string designerSchema = string.Format("file://{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Designer.xsd");
				xrs.Schemas.Add(DESIGNERSCHEMA, designerSchema);

                vr = XmlReader.Create(tr, xrs);

                while (vr.Read()) ;

				this.lbSchemaErrors.Items.Add(string.Format("Validation completed with {0} warnings and {1} errors.", _ValidationWarningCount, _ValidationErrorCount));
			}
			catch (Exception ex)
			{
				this.lbSchemaErrors.Items.Add(ex.Message + "  Processing terminated.");
			}
			finally
			{
				Cursor.Current=saveCursor;
				if (sr != null)
					sr.Close();
				if (tr != null)
					tr.Close();
				if (vr != null)
					vr.Close();
			}
		}

		public void ValidationHandler(object sender, ValidationEventArgs args)
		{
			if (args.Severity == XmlSeverityType.Error)
				this._ValidationErrorCount++;
			else
				this._ValidationWarningCount++;

			this.lbSchemaErrors.Items.Add(string.Format("{0}: {1} ({2}, {3})", 
                args.Severity, args.Message, args.Exception.LineNumber, args.Exception.LinePosition));
		}

		private void lbSchemaErrors_DoubleClick(object sender, System.EventArgs e)
		{
			RdlEditPreview rep = _RdlDesigner.GetEditor();			

			if (rep == null || this.lbSchemaErrors.SelectedIndex < 0)
				return;
			try
			{
				// line numbers are reported as (line#, character offset) e.g. (110, 32)  
				string v = this.lbSchemaErrors.Items[lbSchemaErrors.SelectedIndex] as string;
				int li = v.LastIndexOf("(");
				if (li < 0)
					return;
				v = v.Substring(li+1);
				li = v.IndexOf(",");	// find the
				v = v.Substring(0, li);

				int nLine = Int32.Parse(v);
				rep.Goto(this, nLine);
				this.BringToFront();
			}
#if DEBUG
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);	// developer might care about this error??
			}
#else
			catch 
			{}		// user doesn't really care if something went wrong
#endif

		}

		private void bClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void DialogValidateRdl_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this._RdlDesigner.ValidateSchemaClosing();
		}
	}

}
