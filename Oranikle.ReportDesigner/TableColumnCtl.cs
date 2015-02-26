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
	/// Summary description for TableColumnCtl.
	/// </summary>
	internal class TableColumnCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		private XmlNode _TableColumn;
		private DesignXmlDraw _Draw;
		// flags for controlling whether syntax changed for a particular property
		private bool fHidden, fToggle, fWidth, fFixedHeader;
		private System.Windows.Forms.GroupBox grpBoxVisibility;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.CustomTextControl tbHidden;
		private Oranikle.Studio.Controls.StyledComboBox cbToggle;
		private Oranikle.Studio.Controls.StyledButton bHidden;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbColumnWidth;
		private System.Windows.Forms.Label label4;
		private Oranikle.Studio.Controls.StyledCheckBox chkFixedHeader;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal TableColumnCtl(DesignXmlDraw dxDraw, XmlNode tc)
		{
			_TableColumn = tc;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues(tc);			
		}

		private void InitValues(XmlNode node)
		{
			// Handle Width definition
			this.tbColumnWidth.Text = _Draw.GetElementValue(node, "Width", "");
		
			this.chkFixedHeader.Checked = _Draw.GetElementValue(node, "FixedHeader", "false").ToLower() == "true"? true: false;

			// Handle Visiblity definition
			XmlNode visNode = _Draw.GetNamedChildNode(node, "Visibility");
			if (visNode != null)
			{
				this.tbHidden.Text = _Draw.GetElementValue(visNode, "Hidden", "");
				this.cbToggle.Text = _Draw.GetElementValue(visNode, "ToggleItem", "");
			}
			IEnumerable list = _Draw.GetReportItems("//Textbox");
			if (list != null)
			{
				foreach (XmlNode tNode in list)
				{
					XmlAttribute name = tNode.Attributes["Name"];
					if (name != null && name.Value != null && name.Value.Length > 0)
						cbToggle.Items.Add(name.Value);
				}
			}
			// nothing has changed now
			fWidth = fHidden = fToggle = fFixedHeader = false;
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
            this.grpBoxVisibility = new System.Windows.Forms.GroupBox();
            this.bHidden = new Oranikle.Studio.Controls.StyledButton();
            this.cbToggle = new Oranikle.Studio.Controls.StyledComboBox();
            this.tbHidden = new Oranikle.Studio.Controls.CustomTextControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbColumnWidth = new Oranikle.Studio.Controls.CustomTextControl();
            this.chkFixedHeader = new Oranikle.Studio.Controls.StyledCheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpBoxVisibility.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxVisibility
            // 
            this.grpBoxVisibility.Controls.Add(this.bHidden);
            this.grpBoxVisibility.Controls.Add(this.cbToggle);
            this.grpBoxVisibility.Controls.Add(this.tbHidden);
            this.grpBoxVisibility.Controls.Add(this.label3);
            this.grpBoxVisibility.Controls.Add(this.label2);
            this.grpBoxVisibility.Location = new System.Drawing.Point(8, 8);
            this.grpBoxVisibility.Name = "grpBoxVisibility";
            this.grpBoxVisibility.Size = new System.Drawing.Size(432, 80);
            this.grpBoxVisibility.TabIndex = 1;
            this.grpBoxVisibility.TabStop = false;
            this.grpBoxVisibility.Text = "Visibility";
            // 
            // bHidden
            // 
            this.bHidden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bHidden.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bHidden.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bHidden.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bHidden.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bHidden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bHidden.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHidden.Location = new System.Drawing.Point(400, 24);
            this.bHidden.Name = "bHidden";
            this.bHidden.OverriddenSize = null;
            this.bHidden.Size = new System.Drawing.Size(22, 21);
            this.bHidden.TabIndex = 1;
            this.bHidden.Tag = "visibility";
            this.bHidden.Text = "fx";
            this.bHidden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHidden.UseVisualStyleBackColor = true;
            this.bHidden.Click += new System.EventHandler(this.bExpr_Click);
            // 
            // cbToggle
            // 
            this.cbToggle.AutoAdjustItemHeight = false;
            this.cbToggle.BorderColor = System.Drawing.Color.LightGray;
            this.cbToggle.ConvertEnterToTabForDialogs = false;
            this.cbToggle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbToggle.Location = new System.Drawing.Point(168, 48);
            this.cbToggle.Name = "cbToggle";
            this.cbToggle.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbToggle.SeparatorMargin = 1;
            this.cbToggle.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbToggle.SeparatorWidth = 1;
            this.cbToggle.Size = new System.Drawing.Size(152, 21);
            this.cbToggle.TabIndex = 2;
            this.cbToggle.SelectedIndexChanged += new System.EventHandler(this.cbToggle_SelectedIndexChanged);
            this.cbToggle.TextChanged += new System.EventHandler(this.cbToggle_SelectedIndexChanged);
            // 
            // tbHidden
            // 
            this.tbHidden.AddX = 0;
            this.tbHidden.AddY = 0;
            this.tbHidden.AllowSpace = false;
            this.tbHidden.BorderColor = System.Drawing.Color.LightGray;
            this.tbHidden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHidden.ChangeVisibility = false;
            this.tbHidden.ChildControl = null;
            this.tbHidden.ConvertEnterToTab = true;
            this.tbHidden.ConvertEnterToTabForDialogs = false;
            this.tbHidden.Decimals = 0;
            this.tbHidden.DisplayList = new object[0];
            this.tbHidden.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbHidden.Location = new System.Drawing.Point(168, 24);
            this.tbHidden.Name = "tbHidden";
            this.tbHidden.OnDropDownCloseFocus = true;
            this.tbHidden.SelectType = 0;
            this.tbHidden.Size = new System.Drawing.Size(224, 20);
            this.tbHidden.TabIndex = 0;
            this.tbHidden.UseValueForChildsVisibilty = false;
            this.tbHidden.Value = true;
            this.tbHidden.TextChanged += new System.EventHandler(this.tbHidden_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Toggle Item (Textbox name)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Hidden (initial visibility)";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Column Width";
            // 
            // tbColumnWidth
            // 
            this.tbColumnWidth.AddX = 0;
            this.tbColumnWidth.AddY = 0;
            this.tbColumnWidth.AllowSpace = false;
            this.tbColumnWidth.BorderColor = System.Drawing.Color.LightGray;
            this.tbColumnWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbColumnWidth.ChangeVisibility = false;
            this.tbColumnWidth.ChildControl = null;
            this.tbColumnWidth.ConvertEnterToTab = true;
            this.tbColumnWidth.ConvertEnterToTabForDialogs = false;
            this.tbColumnWidth.Decimals = 0;
            this.tbColumnWidth.DisplayList = new object[0];
            this.tbColumnWidth.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbColumnWidth.Location = new System.Drawing.Point(88, 104);
            this.tbColumnWidth.Name = "tbColumnWidth";
            this.tbColumnWidth.OnDropDownCloseFocus = true;
            this.tbColumnWidth.SelectType = 0;
            this.tbColumnWidth.Size = new System.Drawing.Size(100, 20);
            this.tbColumnWidth.TabIndex = 3;
            this.tbColumnWidth.UseValueForChildsVisibilty = false;
            this.tbColumnWidth.Value = true;
            this.tbColumnWidth.TextChanged += new System.EventHandler(this.tbWidth_TextChanged);
            // 
            // chkFixedHeader
            // 
            this.chkFixedHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkFixedHeader.ForeColor = System.Drawing.Color.Black;
            this.chkFixedHeader.Location = new System.Drawing.Point(8, 136);
            this.chkFixedHeader.Name = "chkFixedHeader";
            this.chkFixedHeader.Size = new System.Drawing.Size(96, 24);
            this.chkFixedHeader.TabIndex = 4;
            this.chkFixedHeader.Text = "Fixed Header";
            this.chkFixedHeader.CheckedChanged += new System.EventHandler(this.cbFixedHeader_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(112, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(336, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Note: Fixed headers must be contiguous and start at either the left or the right " +
                "of the table.  Current renderers ignore this parameter.";
            // 
            // TableColumnCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkFixedHeader);
            this.Controls.Add(this.tbColumnWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpBoxVisibility);
            this.Name = "TableColumnCtl";
            this.Size = new System.Drawing.Size(472, 288);
            this.grpBoxVisibility.ResumeLayout(false);
            this.grpBoxVisibility.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
   
		public bool IsValid()
		{
			try
			{
				if (fWidth)
					DesignerUtility.ValidateSize(this.tbColumnWidth.Text, true, false);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Width is Invalid");
				return false;
			}

			if (fHidden)
			{
				string vh = this.tbHidden.Text.Trim();
				if (vh.Length > 0)
				{
					if (vh.StartsWith("="))
					{}
					else
					{ 
						vh = vh.ToLower();
						switch (vh)
						{
							case "true":
							case "false":
								break;
							default:
								MessageBox.Show(String.Format("{0} must be an expression or 'true' or 'false'", tbHidden.Text), "Hidden is Invalid");
								return false;
						}
					}

				}
			}

			return true;
		}

		public void Apply()
		{
			// take information in control and apply to all the style nodes
			//  Only change information that has been marked as modified;
			//   this way when group is selected it is possible to change just
			//   the items you want and keep the rest the same.

			ApplyChanges(this._TableColumn);

			// nothing has changed now
			fWidth = fHidden = fToggle = false;
		}

		private void ApplyChanges(XmlNode rNode)
		{
			if (fHidden || fToggle)
			{
				XmlNode visNode = _Draw.SetElement(rNode, "Visibility", null);

				if (fHidden)
				{
					string vh = this.tbHidden.Text.Trim();
					if (vh.Length > 0)
						_Draw.SetElement(visNode, "Hidden", vh); 
					else
						_Draw.RemoveElement(visNode, "Hidden");

				}
				if (fToggle)
					_Draw.SetElement(visNode, "ToggleItem", this.cbToggle.Text); 
			}

			if (fWidth)	// already validated
				_Draw.SetElement(rNode, "Width", this.tbColumnWidth.Text); 

			if (fFixedHeader)
			{
				if (this.chkFixedHeader.Checked)
					_Draw.SetElement(rNode, "FixedHeader", "true");
				else
					_Draw.RemoveElement(rNode, "FixedHeader");		// just get rid of it
			}
		}

		private void tbHidden_TextChanged(object sender, System.EventArgs e)
		{
			fHidden = true;
		}

		private void tbWidth_TextChanged(object sender, System.EventArgs e)
		{
			fWidth = true;
		}

		private void cbToggle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fToggle = true;
		}

		private void bExpr_Click(object sender, System.EventArgs e)
		{
			Button b = sender as Button;
			if (b == null)
				return;
			Control c = null;
			switch (b.Tag as string)
			{
				case "visibility":
					c = tbHidden;
					break;
			}

			if (c == null)
				return;

            using (DialogExprEditor ee = new DialogExprEditor(_Draw, c.Text, _TableColumn))
            {
                DialogResult dr = ee.ShowDialog();
                if (dr == DialogResult.OK)
                    c.Text = ee.Expression;
            }
            return;
		}

		private void cbFixedHeader_CheckedChanged(object sender, System.EventArgs e)
		{
			fFixedHeader = true;
		}

	}
}
