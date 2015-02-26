/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Text; 
using System.Reflection;
using Oranikle.Report.Engine;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// DialogListOfStrings: puts up a dialog that lets a user enter a list of strings
	/// </summary>
	public class DialogExprEditor : System.Windows.Forms.Form
	{
        Type[] BASE_TYPES = new Type[] 
      { 
         typeof(string), 
         typeof(double), 
         typeof(Single), 
         typeof(decimal), 
         typeof(DateTime), 
         typeof(char), 
         typeof(bool), 
         typeof(int), 
         typeof(short), 
         typeof(long), 
         typeof(byte), 
         typeof(UInt16), 
         typeof(UInt32), 
         typeof(UInt64) 
      }; 

		private DesignXmlDraw _Draw;		// design draw 
        private bool _Color;				// true if color list should be displayed
        private SplitContainer splitContainer1;
        private Oranikle.Studio.Controls.StyledButton bCopy;
        private Label lOp;
        private Oranikle.Studio.Controls.CustomTextControl tbExpr;
        private Label lExpr;
        private TreeView tvOp;
        private Panel panel1;
        private Oranikle.Studio.Controls.StyledButton bCancel;
        private Oranikle.Studio.Controls.StyledButton bOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DialogExprEditor(DesignXmlDraw dxDraw, string expr, XmlNode node) : 
			this(dxDraw, expr, node, false)
		{
		}

		internal DialogExprEditor(DesignXmlDraw dxDraw, string expr, XmlNode node, bool bColor)
		{
			_Draw = dxDraw;
			_Color = bColor;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			tbExpr.Text = expr;

			// Fill out the fields list 
			string[] fields = null;
			// Find the dataregion that contains the item (if any)
			for (XmlNode pNode = node; pNode != null; pNode = pNode.ParentNode)
			{
				if (pNode.Name == "List" ||
					pNode.Name == "Table" ||
					pNode.Name == "Matrix" ||
					pNode.Name == "Chart")
				{
					string dsname = _Draw.GetDataSetNameValue(pNode);
					if (dsname != null)	// found it
					{
						fields = _Draw.GetFields(dsname, true);
					}
				}
			}
			BuildTree(fields);

			return;
		}

		void BuildTree(string[] flds)
		{
			// suppress redraw until tree view is complete
			tvOp.BeginUpdate();
			//AJM GJL Adding Missing 'User' Menu
            // Handle the user
            TreeNode ndRoot = new TreeNode("User");
            tvOp.Nodes.Add(ndRoot);
            foreach (string item in StaticLists.UserList)
            {
                // Add the node to the tree
                TreeNode aRoot = new TreeNode(item.StartsWith("=") ? item.Substring(1) : item);
                ndRoot.Nodes.Add(aRoot);
            }

			// Handle the globals
			ndRoot = new TreeNode("Globals");
			tvOp.Nodes.Add(ndRoot);
			foreach (string item in StaticLists.GlobalList)
			{
				// Add the node to the tree
				TreeNode aRoot = new TreeNode(item.StartsWith("=")? item.Substring(1): item);
				ndRoot.Nodes.Add(aRoot);
			}

			// Fields - only when a dataset is specified
			if (flds != null && flds.Length > 0)
			{
				ndRoot = new TreeNode("Fields");
				tvOp.Nodes.Add(ndRoot);

				foreach (string f in flds)
				{	
					TreeNode aRoot = new TreeNode(f.StartsWith("=")? f.Substring(1): f);
					ndRoot.Nodes.Add(aRoot);
				}
			}

			// Report parameters
			InitReportParameters();

			// Handle the functions
			ndRoot = new TreeNode("Functions");
			tvOp.Nodes.Add(ndRoot);
			InitFunctions(ndRoot);

			// Aggregate functions
			ndRoot = new TreeNode("Aggregate Functions");
			tvOp.Nodes.Add(ndRoot);
			foreach (string item in StaticLists.AggrFunctionList)
			{
				// Add the node to the tree
				TreeNode aRoot = new TreeNode(item);
				ndRoot.Nodes.Add(aRoot);
			}

			// Operators
			ndRoot = new TreeNode("Operators");
			tvOp.Nodes.Add(ndRoot);
			foreach (string item in StaticLists.OperatorList)
			{
				// Add the node to the tree
				TreeNode aRoot = new TreeNode(item);
				ndRoot.Nodes.Add(aRoot);
			}

			// Colors (if requested)
			if (_Color)
			{
				ndRoot = new TreeNode("Colors");
				tvOp.Nodes.Add(ndRoot);
				foreach (string item in StaticLists.ColorList)
				{
					// Add the node to the tree
					TreeNode aRoot = new TreeNode(item);
					ndRoot.Nodes.Add(aRoot);
				}
			}


			tvOp.EndUpdate();

		}

		/// <summary>
		/// Populate tree view with the report parameters (if any)
		/// </summary>
		void InitReportParameters()
		{
			string[] ps = _Draw.GetReportParameters(true);
			
			if (ps == null || ps.Length == 0)
				return;

			TreeNode ndRoot = new TreeNode("Parameters");
			tvOp.Nodes.Add(ndRoot);

			foreach (string p in ps)
			{
				TreeNode aRoot = new TreeNode(p.StartsWith("=")?p.Substring(1): p);
				ndRoot.Nodes.Add(aRoot);
			}

			return;
		}

		void InitFunctions(TreeNode ndRoot)
		{
            List<string> ar = new List<string>();
			
			ar.AddRange(StaticLists.FunctionList);

			// Build list of methods in the  VBFunctions class
			Oranikle.Report.Engine.FontStyleEnum fsi = FontStyleEnum.Italic;	// just want a class from RdlEngine.dll assembly
			Assembly a = Assembly.GetAssembly(fsi.GetType());
			if (a == null)
				return;
			Type ft = a.GetType("Oranikle.Report.Engine.VBFunctions");	 
			BuildMethods(ar, ft, "");

			// build list of financial methods in Financial class
			ft = a.GetType("Oranikle.Report.Engine.Financial");
			BuildMethods(ar, ft, "Financial.");

			a = Assembly.GetAssembly("".GetType());
			ft = a.GetType("System.Math");
			BuildMethods(ar, ft, "Math.");

			ft = a.GetType("System.Convert");
			BuildMethods(ar, ft, "Convert.");

			ft = a.GetType("System.String");
			BuildMethods(ar, ft, "String.");

			ar.Sort();
			string previous="";
			foreach (string item in ar)
			{
				if (item != previous)	// don't add duplicates
				{
					// Add the node to the tree
					TreeNode aRoot = new TreeNode(item);
					ndRoot.Nodes.Add(aRoot);
				}
				previous = item;
			}

		}

        void BuildMethods(List<string> ar, Type ft, string prefix)
		{
			if (ft == null)
				return;
			MethodInfo[] mis = ft.GetMethods(BindingFlags.Static | BindingFlags.Public);
			foreach (MethodInfo mi in mis)
			{
				// Add the node to the tree
				string name = BuildMethodName(mi);
				if (name != null)
					ar.Add(prefix + name);
			}
		}

		string BuildMethodName(MethodInfo mi)
		{
			StringBuilder sb = new StringBuilder(mi.Name);
			sb.Append("(");
			ParameterInfo[] pis = mi.GetParameters();
			bool bFirst=true;
			foreach (ParameterInfo pi in pis)
			{
				if (!IsBaseType(pi.ParameterType))
					return null;
				if (bFirst)
					bFirst = false;
				else
					sb.Append(", ");
				sb.Append(pi.Name);
			}
			sb.Append(")");
			return sb.ToString();
		}

		// Determines if underlying type is a primitive
		bool IsBaseType(Type t)
		{
			foreach (Type bt in BASE_TYPES)
			{
				if (bt == t)
					return true;
			}

			return false;
		}

		public string Expression
		{
			get	{return tbExpr.Text; }
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvOp = new System.Windows.Forms.TreeView();
            this.bCopy = new Oranikle.Studio.Controls.StyledButton();
            this.lOp = new System.Windows.Forms.Label();
            this.tbExpr = new Oranikle.Studio.Controls.CustomTextControl();
            this.lExpr = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.bCancel);
            this.panel1.Controls.Add(this.bOK);
            this.panel1.Location = new System.Drawing.Point(0, 407);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 40);
            this.panel1.TabIndex = 15;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCancel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCancel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.bCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(768, 9);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 21);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Font = new System.Drawing.Font("Arial", 9F);
            this.bOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOK.Location = new System.Drawing.Point(687, 9);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 21);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvOp);
            this.splitContainer1.Panel1.Controls.Add(this.bCopy);
            this.splitContainer1.Panel1.Controls.Add(this.lOp);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbExpr);
            this.splitContainer1.Panel2.Controls.Add(this.lExpr);
            this.splitContainer1.Size = new System.Drawing.Size(857, 402);
            this.splitContainer1.SplitterDistance = 285;
            this.splitContainer1.TabIndex = 14;
            // 
            // tvOp
            // 
            this.tvOp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvOp.Location = new System.Drawing.Point(0, 29);
            this.tvOp.Name = "tvOp";
            this.tvOp.Size = new System.Drawing.Size(282, 370);
            this.tvOp.TabIndex = 1;
            // 
            // bCopy
            // 
            this.bCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCopy.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCopy.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCopy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCopy.Font = new System.Drawing.Font("Arial", 9F);
            this.bCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCopy.Location = new System.Drawing.Point(250, 4);
            this.bCopy.Name = "bCopy";
            this.bCopy.OverriddenSize = null;
            this.bCopy.Size = new System.Drawing.Size(32, 21);
            this.bCopy.TabIndex = 2;
            this.bCopy.Text = ">>";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // lOp
            // 
            this.lOp.Location = new System.Drawing.Point(0, 4);
            this.lOp.Name = "lOp";
            this.lOp.Size = new System.Drawing.Size(106, 23);
            this.lOp.TabIndex = 14;
            this.lOp.Text = "Select and hit \'>>\'";
            // 
            // tbExpr
            // 
            this.tbExpr.AcceptsReturn = true;
            this.tbExpr.AcceptsTab = true;
            this.tbExpr.AddX = 0;
            this.tbExpr.AddY = 0;
            this.tbExpr.AllowSpace = false;
            this.tbExpr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExpr.BorderColor = System.Drawing.Color.LightGray;
            this.tbExpr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExpr.ChangeVisibility = false;
            this.tbExpr.ChildControl = null;
            this.tbExpr.ConvertEnterToTab = true;
            this.tbExpr.ConvertEnterToTabForDialogs = false;
            this.tbExpr.Decimals = 0;
            this.tbExpr.DisplayList = new object[0];
            this.tbExpr.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbExpr.Location = new System.Drawing.Point(6, 32);
            this.tbExpr.Multiline = true;
            this.tbExpr.Name = "tbExpr";
            this.tbExpr.OnDropDownCloseFocus = true;
            this.tbExpr.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbExpr.SelectType = 0;
            this.tbExpr.Size = new System.Drawing.Size(559, 367);
            this.tbExpr.TabIndex = 0;
            this.tbExpr.UseValueForChildsVisibilty = false;
            this.tbExpr.Value = true;
            this.tbExpr.WordWrap = false;
            // 
            // lExpr
            // 
            this.lExpr.Location = new System.Drawing.Point(3, 6);
            this.lExpr.Name = "lExpr";
            this.lExpr.Size = new System.Drawing.Size(134, 20);
            this.lExpr.TabIndex = 13;
            this.lExpr.Text = "Expressions start with \'=\'";
            // 
            // DialogExprEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(857, 447);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogExprEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Expression";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void bCopy_Click(object sender, System.EventArgs e)
		{
			if (tvOp.SelectedNode == null ||
				tvOp.SelectedNode.Parent == null)
				return;		// this is the top level nodes (Fields, Parameters, ...)

			TreeNode node = tvOp.SelectedNode;
			string t = node.Text;
            if (tbExpr.Text.Length == 0)
                t = "=" + t;
			tbExpr.SelectedText = t;
		}

	}

}
