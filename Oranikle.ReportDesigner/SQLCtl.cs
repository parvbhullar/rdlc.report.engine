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

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for ReportCtl.
	/// </summary>
	internal class SQLCtl : System.Windows.Forms.Form
	{
		DesignXmlDraw _Draw;
		string _DataSource;
        DataTable _QueryParameters;
        private System.Windows.Forms.Panel panel1;
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
        private SplitContainer splitContainer1;
        private TreeView tvTablesColumns;
        private Oranikle.Studio.Controls.CustomTextControl tbSQL;
        private Oranikle.Studio.Controls.StyledButton bMove;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal SQLCtl(DesignXmlDraw dxDraw, string datasource, string sql, DataTable queryParameters)
		{
			_Draw = dxDraw;
			_DataSource = datasource;
			_QueryParameters = queryParameters;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues(sql);			
		}

		private void InitValues(string sql)
		{
			this.tbSQL.Text = sql;

			// Fill out the tables, columns and parameters

			// suppress redraw until tree view is complete
			tvTablesColumns.BeginUpdate();
			
			// Get the schema information
			List<SqlSchemaInfo> si = DesignerUtility.GetSchemaInfo(_Draw, _DataSource);
			if (si != null && si.Count > 0)
			{
				TreeNode ndRoot = new TreeNode("Tables");
				tvTablesColumns.Nodes.Add(ndRoot);
				if (si == null)		// Nothing to initialize
					return;
				bool bView = false;
				foreach (SqlSchemaInfo ssi in si)
				{
					if (!bView && ssi.Type == "VIEW")
					{	// Switch over to views
						ndRoot = new TreeNode("Views");
						tvTablesColumns.Nodes.Add(ndRoot);
						bView=true;
					}

					// Add the node to the tree
					TreeNode aRoot = new TreeNode(ssi.Name);
					ndRoot.Nodes.Add(aRoot);
					aRoot.Nodes.Add("");
				}
			}
			// Now do parameters
			TreeNode qpRoot = null;
			foreach (DataRow dr in _QueryParameters.Rows)
			{
				if (dr[0] == DBNull.Value || dr[1] == null)
					continue;
				string pName = (string) dr[0];
				if (pName.Length == 0)
					continue;
				if (qpRoot == null)
				{
					qpRoot = new TreeNode("Query Parameters");
					tvTablesColumns.Nodes.Add(qpRoot);
				}
				if (pName[0] == '@')
					pName = "@" + pName;
				// Add the node to the tree
				TreeNode aRoot = new TreeNode(pName);
				qpRoot.Nodes.Add(aRoot);
			}

			tvTablesColumns.EndUpdate();
		}

		internal string SQL
		{
			get {return tbSQL.Text;}
			set {tbSQL.Text = value;}
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvTablesColumns = new System.Windows.Forms.TreeView();
            this.tbSQL = new Oranikle.Studio.Controls.CustomTextControl();
            this.bMove = new Oranikle.Studio.Controls.StyledButton();
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
            this.panel1.Controls.Add(this.bOK);
            this.panel1.Controls.Add(this.bCancel);
            this.panel1.Location = new System.Drawing.Point(0, 215);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 40);
            this.panel1.TabIndex = 6;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Font = new System.Drawing.Font("Arial", 9F);
            this.bOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOK.Location = new System.Drawing.Point(300, 8);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 21);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.bCancel.Location = new System.Drawing.Point(388, 8);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 21);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.tvTablesColumns);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbSQL);
            this.splitContainer1.Panel2.Controls.Add(this.bMove);
            this.splitContainer1.Size = new System.Drawing.Size(468, 215);
            this.splitContainer1.SplitterDistance = 123;
            this.splitContainer1.TabIndex = 9;
            // 
            // tvTablesColumns
            // 
            this.tvTablesColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTablesColumns.FullRowSelect = true;
            this.tvTablesColumns.Location = new System.Drawing.Point(0, 0);
            this.tvTablesColumns.Name = "tvTablesColumns";
            this.tvTablesColumns.Size = new System.Drawing.Size(123, 215);
            this.tvTablesColumns.TabIndex = 5;
            this.tvTablesColumns.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvTablesColumns_BeforeExpand);
            // 
            // tbSQL
            // 
            this.tbSQL.AcceptsReturn = true;
            this.tbSQL.AcceptsTab = true;
            this.tbSQL.AddX = 0;
            this.tbSQL.AddY = 0;
            this.tbSQL.AllowDrop = true;
            this.tbSQL.AllowSpace = false;
            this.tbSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSQL.BorderColor = System.Drawing.Color.LightGray;
            this.tbSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSQL.ChangeVisibility = false;
            this.tbSQL.ChildControl = null;
            this.tbSQL.ConvertEnterToTab = true;
            this.tbSQL.ConvertEnterToTabForDialogs = false;
            this.tbSQL.Decimals = 0;
            this.tbSQL.DisplayList = new object[0];
            this.tbSQL.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbSQL.Location = new System.Drawing.Point(37, 0);
            this.tbSQL.Multiline = true;
            this.tbSQL.Name = "tbSQL";
            this.tbSQL.OnDropDownCloseFocus = true;
            this.tbSQL.SelectType = 0;
            this.tbSQL.Size = new System.Drawing.Size(299, 215);
            this.tbSQL.TabIndex = 10;
            this.tbSQL.UseValueForChildsVisibilty = false;
            this.tbSQL.Value = true;
            // 
            // bMove
            // 
            this.bMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMove.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMove.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMove.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMove.Font = new System.Drawing.Font("Arial", 9F);
            this.bMove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMove.Location = new System.Drawing.Point(3, 3);
            this.bMove.Name = "bMove";
            this.bMove.OverriddenSize = null;
            this.bMove.Size = new System.Drawing.Size(32, 21);
            this.bMove.TabIndex = 9;
            this.bMove.Text = ">>";
            this.bMove.UseVisualStyleBackColor = true;
            this.bMove.Click += new System.EventHandler(this.bMove_Click);
            // 
            // SQLCtl
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(468, 255);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SQLCtl";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL Syntax Helper";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void bOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		private void tvTablesColumns_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			tvTablesColumns_ExpandTable(e.Node);
		}

		private void tvTablesColumns_ExpandTable(TreeNode tNode)
		{
			if (tNode.Parent == null)	// Check for Tables or Views
				return;					

			if (tNode.FirstNode.Text != "")	// Have we already filled it out?
				return;

			// Need to obtain the column information for the requested table/view
			// suppress redraw until tree view is complete
			tvTablesColumns.BeginUpdate();

            string sql = "SELECT * FROM " + DesignerUtility.NormalizeSqlName(tNode.Text);
			List<SqlColumn> tColumns = DesignerUtility.GetSqlColumns(_Draw, _DataSource, sql);
			bool bFirstTime=true;
			foreach (SqlColumn sc in tColumns)
			{
				if (bFirstTime)
				{
					bFirstTime = false;
					tNode.FirstNode.Text = sc.Name;
				}
				else
					tNode.Nodes.Add(sc.Name);
			}

			tvTablesColumns.EndUpdate();
		}

		private void bMove_Click(object sender, System.EventArgs e)
		{
			if (tvTablesColumns.SelectedNode == null ||
				tvTablesColumns.SelectedNode.Parent == null)
				return;		// this is the Tables/Views node

			TreeNode node = tvTablesColumns.SelectedNode;
			string t = node.Text;
			if (tbSQL.Text == "")
			{
				if (node.Parent.Parent == null)
				{	// select table; generate full select for table
					tvTablesColumns_ExpandTable(node);	// make sure we've obtained the columns

					StringBuilder sb = new StringBuilder("SELECT ");
					TreeNode next = node.FirstNode;
					while (true)
					{
                        sb.Append(DesignerUtility.NormalizeSqlName(next.Text));
						next = next.NextNode;
						if (next == null)
							break;
						sb.Append(", ");
					}
					sb.Append(" FROM ");
                    sb.Append(DesignerUtility.NormalizeSqlName(node.Text));
					t = sb.ToString();
				}
				else
				{	// select column; generate select of that column	
                    t = "SELECT " + DesignerUtility.NormalizeSqlName(node.Text) + " FROM " + DesignerUtility.NormalizeSqlName(node.Parent.Text);
				}
			}

			tbSQL.SelectedText = t;
		}


	}
}
