/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using Oranikle.Report.Engine;
using Oranikle.Report.Viewer;

namespace Oranikle.Report.Reader
{
	/// <summary>
	/// RdlReader is a application for displaying reports based on RDL.
	/// </summary>
	public class MDIChild : Form
	{
		private Oranikle.Report.Viewer.RdlViewer rdlViewer1;

		public MDIChild(int width, int height)
		{
			this.rdlViewer1 = new Oranikle.Report.Viewer.RdlViewer();
			this.SuspendLayout();
			// 
			// rdlViewer1
			// 
			this.rdlViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rdlViewer1.Location = new System.Drawing.Point(0, 0);
			this.rdlViewer1.Name = "rdlViewer1";
			this.rdlViewer1.Size = new System.Drawing.Size(width, height);
			this.rdlViewer1.TabIndex = 0;
			// 
			// RdlReader
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(width, height);
			this.Controls.Add(this.rdlViewer1);
			this.Name = "";
			this.Text = "";
			this.ResumeLayout(false);
		}

		/// <summary>
		/// The RDL file that should be displayed.
		/// </summary>
		public string SourceFile
		{
			get {return this.rdlViewer1.SourceFile;}
			set 
			{
				this.rdlViewer1.SourceFile = value;
				this.rdlViewer1.Refresh();		// force the repaint
			}
		}

		public RdlViewer Viewer
		{
			get {return this.rdlViewer1;}
		}
	}
}
