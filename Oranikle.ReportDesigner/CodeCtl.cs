/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.VisualBasic;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for CodeCtl.
	/// </summary>
	internal class CodeCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		static internal long Counter;			// counter used for unique expression count
		private DesignXmlDraw _Draw;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.StyledButton bCheckSyntax;
		private System.Windows.Forms.Panel panel1;
		private Oranikle.Studio.Controls.CustomTextControl tbCode;
		private System.Windows.Forms.ListBox lbErrors;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal CodeCtl(DesignXmlDraw dxDraw)
		{
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			XmlNode rNode = _Draw.GetReportNode();
			XmlNode cNode = _Draw.GetNamedChildNode(rNode, "Code");
			tbCode.Text = "";
			if (cNode == null)
				return;

			StringReader tr = new StringReader(cNode.InnerText);
			List<string> ar = new List<string>();
			while (tr.Peek() >= 0)
			{
				string line = tr.ReadLine();
				ar.Add(line);
			}
			tr.Close();

        //    tbCode.Lines = ar.ToArray("".GetType()) as string[];
            tbCode.Lines = ar.ToArray();
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
            this.bCheckSyntax = new Oranikle.Studio.Controls.StyledButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbCode = new Oranikle.Studio.Controls.CustomTextControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lbErrors = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(98, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Visual Basic Function Code";
            // 
            // bCheckSyntax
            // 
            this.bCheckSyntax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCheckSyntax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCheckSyntax.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCheckSyntax.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCheckSyntax.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCheckSyntax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCheckSyntax.Font = new System.Drawing.Font("Arial", 9F);
            this.bCheckSyntax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCheckSyntax.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCheckSyntax.Location = new System.Drawing.Point(365, 4);
            this.bCheckSyntax.Name = "bCheckSyntax";
            this.bCheckSyntax.OverriddenSize = null;
            this.bCheckSyntax.Size = new System.Drawing.Size(82, 21);
            this.bCheckSyntax.TabIndex = 2;
            this.bCheckSyntax.Text = "Check Syntax";
            this.bCheckSyntax.UseVisualStyleBackColor = true;
            this.bCheckSyntax.Click += new System.EventHandler(this.bCheckSyntax_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tbCode);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.lbErrors);
            this.panel1.Location = new System.Drawing.Point(13, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 252);
            this.panel1.TabIndex = 4;
            // 
            // tbCode
            // 
            this.tbCode.AcceptsReturn = true;
            this.tbCode.AcceptsTab = true;
            this.tbCode.AddX = 0;
            this.tbCode.AddY = 0;
            this.tbCode.AllowSpace = false;
            this.tbCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCode.BorderColor = System.Drawing.Color.LightGray;
            this.tbCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCode.ChangeVisibility = false;
            this.tbCode.ChildControl = null;
            this.tbCode.ConvertEnterToTab = true;
            this.tbCode.ConvertEnterToTabForDialogs = false;
            this.tbCode.Decimals = 0;
            this.tbCode.DisplayList = new object[0];
            this.tbCode.HideSelection = false;
            this.tbCode.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbCode.Location = new System.Drawing.Point(87, 0);
            this.tbCode.Multiline = true;
            this.tbCode.Name = "tbCode";
            this.tbCode.OnDropDownCloseFocus = true;
            this.tbCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCode.SelectType = 0;
            this.tbCode.Size = new System.Drawing.Size(352, 252);
            this.tbCode.TabIndex = 2;
            this.tbCode.UseValueForChildsVisibilty = false;
            this.tbCode.Value = true;
            this.tbCode.WordWrap = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 252);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // lbErrors
            // 
            this.lbErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbErrors.HorizontalScrollbar = true;
            this.lbErrors.IntegralHeight = false;
            this.lbErrors.Location = new System.Drawing.Point(0, 0);
            this.lbErrors.Name = "lbErrors";
            this.lbErrors.Size = new System.Drawing.Size(82, 252);
            this.lbErrors.TabIndex = 0;
            this.lbErrors.SelectedIndexChanged += new System.EventHandler(this.lbErrors_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Msgs";
            // 
            // CodeCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bCheckSyntax);
            this.Controls.Add(this.label1);
            this.Name = "CodeCtl";
            this.Size = new System.Drawing.Size(472, 288);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		public bool IsValid()
		{
			return true;
		}

		public void Apply()
		{
			XmlNode rNode = _Draw.GetReportNode(); 
			if (tbCode.Text.Trim().Length > 0)
				_Draw.SetElement(rNode, "Code", tbCode.Text);
			else
				_Draw.RemoveElement(rNode, "Code");
		}

		private void bCheckSyntax_Click(object sender, System.EventArgs e)
		{
			CheckAssembly();	
		}
		
		private void CheckAssembly()
		{
			lbErrors.Items.Clear();					// clear out existing items

			// Generate the proxy source code
			List<string> lines = new List<string>();		// hold lines in array in case of error

			VBCodeProvider vbcp =  new VBCodeProvider();
			StringBuilder sb = new StringBuilder();
			//  Generate code with the following general form

			//Imports System
            //Imports Microsoft.VisualBasic
            //Imports System.Convert
            //Imports System.Math 
            //Namespace Oranikle Reporting.vbgen
			//Public Class MyClassn	   // where n is a uniquely generated integer
			//Sub New()
			//End Sub
			//  ' this is the code in the <Code> tag
			//End Class
			//End Namespace
			string unique = Interlocked.Increment(ref CodeCtl.Counter).ToString();
            lines.Add("Imports System");
            lines.Add("Imports Microsoft.VisualBasic");
            lines.Add("Imports System.Convert");
            lines.Add("Imports System.Math");
            lines.Add("Imports Oranikle.Report.Engine");
            lines.Add("Namespace Oranikle Reporting.vbgen");
            string classname = "MyClass" + unique;
            lines.Add("Public Class " + classname);
            lines.Add("Private Shared _report As CodeReport");
            lines.Add("Sub New()");
            lines.Add("End Sub");
            lines.Add("Sub New(byVal def As Report)");
            lines.Add(classname + "._report = New CodeReport(def)");
            lines.Add("End Sub");
            lines.Add("Public Shared ReadOnly Property Report As CodeReport");
            lines.Add("Get");
            lines.Add("Return " + classname + "._report");
            lines.Add("End Get");
            lines.Add("End Property");
            int pre_lines = lines.Count;            // number of lines prior to user VB code

            // Read and write code as lines
			StringReader tr = new StringReader(this.tbCode.Text);
			while (tr.Peek() >= 0)
			{
				string line = tr.ReadLine();
				lines.Add(line);
			}
			tr.Close();
			lines.Add("End Class");
			lines.Add("End Namespace");
            foreach (string l in lines)
            {
                sb.Append(l);
                sb.Append(Environment.NewLine);
            }
			string vbcode = sb.ToString();

			// Create Assembly
			CompilerParameters cp = new CompilerParameters();
			cp.ReferencedAssemblies.Add("System.dll");
            string re = AppDomain.CurrentDomain.BaseDirectory + "RdlEngine.dll";
            cp.ReferencedAssemblies.Add(re);

            // also allow access to classes that have been added to report
            XmlNode rNode = _Draw.GetReportNode();
            XmlNode cNode = _Draw.GetNamedChildNode(rNode, "CodeModules");
            if (cNode != null)
            {
                foreach (XmlNode xn in cNode.ChildNodes)
                {
                    if (xn.Name != "CodeModule")
                        continue;
                    cp.ReferencedAssemblies.Add(xn.InnerText);
                }
            }

            cp.GenerateExecutable = false;
			cp.GenerateInMemory = true;
			cp.IncludeDebugInformation = false; 
			
			CompilerResults cr = vbcp.CompileAssemblyFromSource(cp, vbcode);
			if(cr.Errors.Count > 0)
			{
				StringBuilder err = new StringBuilder(string.Format("Code has {0} error(s).", cr.Errors.Count));
				foreach (CompilerError ce in cr.Errors)
				{
					lbErrors.Items.Add(string.Format("Ln {0}- {1}", ce.Line - pre_lines, ce.ErrorText));
				}
			}
			else
				MessageBox.Show("No errors", "Code Verification");

			return ;
		}

		private void lbErrors_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lbErrors.Items.Count == 0)
				return;
						 
			string line = lbErrors.Items[lbErrors.SelectedIndex] as string;
			if (!line.StartsWith("Ln"))
				return;

			int l = line.IndexOf('-');
			if (l < 0)
				return;
			line = line.Substring(3, l-3);
			try
			{
				int i = Convert.ToInt32(line);
				Goto(i);
			}
			catch {}		// we don't care about the error
			return;
		}

		public void Goto(int nLine)
		{	
			int offset = 0; 
			nLine = Math.Min(nLine, tbCode.Lines.Length);		// don't go off the end

			for ( int i = 0; i < nLine - 1 && i < tbCode.Lines.Length; ++i ) 
				offset += (this.tbCode.Lines[i].Length + 2); 

			Control savectl = this.ActiveControl;
			tbCode.Focus(); 
			tbCode.Select( offset, this.tbCode.Lines[nLine > 0? nLine-1: 0].Length);
			this.ActiveControl = savectl;
		}


	}
}
