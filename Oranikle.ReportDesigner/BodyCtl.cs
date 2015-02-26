/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for BodyCtl.
	/// </summary>
	internal class BodyCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		private DesignXmlDraw _Draw;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.CustomTextControl tbHeight;
		private Oranikle.Studio.Controls.CustomTextControl tbColumns;
		private Oranikle.Studio.Controls.CustomTextControl tbColumnSpacing;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal BodyCtl(DesignXmlDraw dxDraw)
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
			XmlNode bNode = _Draw.GetNamedChildNode(rNode, "Body");
			tbHeight.Text = _Draw.GetElementValue(bNode, "Height", "");
			tbColumns.Text = _Draw.GetElementValue(bNode, "Columns", "1");
			tbColumnSpacing.Text = _Draw.GetElementValue(bNode, "ColumnSpacing", "");
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHeight = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbColumns = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbColumnSpacing = new Oranikle.Studio.Controls.CustomTextControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Height";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Columns";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Column Spacing";
            // 
            // tbHeight
            // 
            this.tbHeight.AddX = 0;
            this.tbHeight.AddY = 0;
            this.tbHeight.AllowSpace = false;
            this.tbHeight.BorderColor = System.Drawing.Color.LightGray;
            this.tbHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHeight.ChangeVisibility = false;
            this.tbHeight.ChildControl = null;
            this.tbHeight.ConvertEnterToTab = true;
            this.tbHeight.ConvertEnterToTabForDialogs = false;
            this.tbHeight.Decimals = 0;
            this.tbHeight.DisplayList = new object[0];
            this.tbHeight.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbHeight.Location = new System.Drawing.Point(104, 25);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.OnDropDownCloseFocus = true;
            this.tbHeight.SelectType = 0;
            this.tbHeight.Size = new System.Drawing.Size(100, 20);
            this.tbHeight.TabIndex = 0;
            this.tbHeight.UseValueForChildsVisibilty = false;
            this.tbHeight.Value = true;
            // 
            // tbColumns
            // 
            this.tbColumns.AddX = 0;
            this.tbColumns.AddY = 0;
            this.tbColumns.AllowSpace = false;
            this.tbColumns.BorderColor = System.Drawing.Color.LightGray;
            this.tbColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbColumns.ChangeVisibility = false;
            this.tbColumns.ChildControl = null;
            this.tbColumns.ConvertEnterToTab = true;
            this.tbColumns.ConvertEnterToTabForDialogs = false;
            this.tbColumns.Decimals = 0;
            this.tbColumns.DisplayList = new object[0];
            this.tbColumns.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbColumns.Location = new System.Drawing.Point(104, 61);
            this.tbColumns.Name = "tbColumns";
            this.tbColumns.OnDropDownCloseFocus = true;
            this.tbColumns.SelectType = 0;
            this.tbColumns.Size = new System.Drawing.Size(100, 20);
            this.tbColumns.TabIndex = 1;
            this.tbColumns.UseValueForChildsVisibilty = false;
            this.tbColumns.Value = true;
            // 
            // tbColumnSpacing
            // 
            this.tbColumnSpacing.AddX = 0;
            this.tbColumnSpacing.AddY = 0;
            this.tbColumnSpacing.AllowSpace = false;
            this.tbColumnSpacing.BorderColor = System.Drawing.Color.LightGray;
            this.tbColumnSpacing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbColumnSpacing.ChangeVisibility = false;
            this.tbColumnSpacing.ChildControl = null;
            this.tbColumnSpacing.ConvertEnterToTab = true;
            this.tbColumnSpacing.ConvertEnterToTabForDialogs = false;
            this.tbColumnSpacing.Decimals = 0;
            this.tbColumnSpacing.DisplayList = new object[0];
            this.tbColumnSpacing.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbColumnSpacing.Location = new System.Drawing.Point(104, 96);
            this.tbColumnSpacing.Name = "tbColumnSpacing";
            this.tbColumnSpacing.OnDropDownCloseFocus = true;
            this.tbColumnSpacing.SelectType = 0;
            this.tbColumnSpacing.Size = new System.Drawing.Size(100, 20);
            this.tbColumnSpacing.TabIndex = 2;
            this.tbColumnSpacing.UseValueForChildsVisibilty = false;
            this.tbColumnSpacing.Value = true;
            // 
            // BodyCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tbColumnSpacing);
            this.Controls.Add(this.tbColumns);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BodyCtl";
            this.Size = new System.Drawing.Size(472, 288);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        
		public bool IsValid()
		{
			return true;
		}

		public void Apply()
		{
			XmlNode rNode = _Draw.GetReportNode();
			XmlNode bNode = _Draw.GetNamedChildNode(rNode, "Body");
			_Draw.SetElement(bNode, "Height", tbHeight.Text);
			_Draw.SetElement(bNode, "Columns", tbColumns.Text);
			if (tbColumnSpacing.Text.Length > 0)
				_Draw.SetElement(bNode, "ColumnSpacing", tbColumnSpacing.Text);
			else
				_Draw.RemoveElement(bNode, "ColumnSpacing");
		}
	}
}
