/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using System.Reflection;
using Oranikle.Report.Engine;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// DialogFilterOperator: puts up a dialog that lets a user pick a Filter Operator
	/// </summary>
	public class DialogFilterOperator : System.Windows.Forms.Form
	{
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		private System.Windows.Forms.Label lOp;
		private Oranikle.Studio.Controls.StyledComboBox cbOperator;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DialogFilterOperator(string op)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if (op != null && op.Length > 0)
				cbOperator.Text = op;

			return;
		}

		public string Operator
		{
			get	{return this.cbOperator.Text; }
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
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.lOp = new System.Windows.Forms.Label();
            this.cbOperator = new Oranikle.Studio.Controls.StyledComboBox();
            this.SuspendLayout();
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
            this.bOK.Location = new System.Drawing.Point(88, 168);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
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
            this.bCancel.Location = new System.Drawing.Point(176, 168);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // lOp
            // 
            this.lOp.Location = new System.Drawing.Point(8, 8);
            this.lOp.Name = "lOp";
            this.lOp.Size = new System.Drawing.Size(112, 16);
            this.lOp.TabIndex = 13;
            this.lOp.Text = "Select Filter Operator";
            // 
            // cbOperator
            // 
            this.cbOperator.AutoAdjustItemHeight = false;
            this.cbOperator.BorderColor = System.Drawing.Color.LightGray;
            this.cbOperator.ConvertEnterToTabForDialogs = false;
            this.cbOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbOperator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOperator.Items.AddRange(new object[] {
            "Equal",
            "Like",
            "NotEqual",
            "GreaterThan",
            "GreaterThanOrEqual",
            "LessThan",
            "LessThanOrEqual",
            "TopN",
            "BottomN",
            "TopPercent",
            "BottomPercent",
            "In",
            "Between"});
            this.cbOperator.Location = new System.Drawing.Point(120, 8);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbOperator.SeparatorMargin = 1;
            this.cbOperator.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbOperator.SeparatorWidth = 1;
            this.cbOperator.Size = new System.Drawing.Size(128, 150);
            this.cbOperator.TabIndex = 14;
            this.cbOperator.Text = "Equal";
            this.cbOperator.Validating += new System.ComponentModel.CancelEventHandler(this.DialogFilterOperator_Validating);
            // 
            // DialogFilterOperator
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(256, 198);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.lOp);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogFilterOperator";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pick Filter Operator";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.DialogFilterOperator_Validating);
            this.ResumeLayout(false);

		}
		#endregion

		private void DialogFilterOperator_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach (string op in cbOperator.Items)
			{
				if (op == cbOperator.Text)
					return;
			}
			MessageBox.Show(string.Format("Operator '{0}' must be in the operator list", cbOperator.Text), "Pick Filter Operator");
			e.Cancel = true;
		}

	}

}
