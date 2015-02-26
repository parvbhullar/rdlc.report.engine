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
	/// Summary description for StyleCtl.
	/// </summary>
	internal class ReportXmlCtl : Oranikle.ReportDesigner.Base.BaseControl, IProperty
	{
		private DesignXmlDraw _Draw;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbDataTransform;
		private Oranikle.Studio.Controls.CustomTextControl tbDataSchema;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.CustomTextControl tbDataElementName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private Oranikle.Studio.Controls.StyledComboBox cbElementStyle;
		private Oranikle.Studio.Controls.StyledButton bOpenXsl;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal ReportXmlCtl(DesignXmlDraw dxDraw)
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
			tbDataTransform.Text = _Draw.GetElementValue(rNode, "DataTransform", "");
			tbDataSchema.Text = _Draw.GetElementValue(rNode, "DataSchema", "");
			tbDataElementName.Text = _Draw.GetElementValue(rNode, "DataElementName", "Report");
			cbElementStyle.Text = _Draw.GetElementValue(rNode, "DataElementStyle", "AttributeNormal");
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
            this.tbDataTransform = new Oranikle.Studio.Controls.CustomTextControl();
            this.tbDataSchema = new Oranikle.Studio.Controls.CustomTextControl();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDataElementName = new Oranikle.Studio.Controls.CustomTextControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbElementStyle = new Oranikle.Studio.Controls.StyledComboBox();
            this.bOpenXsl = new Oranikle.Studio.Controls.StyledButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "XSL Data Transform";
            // 
            // tbDataTransform
            // 
            this.tbDataTransform.AddX = 0;
            this.tbDataTransform.AddY = 0;
            this.tbDataTransform.AllowSpace = false;
            this.tbDataTransform.BorderColor = System.Drawing.Color.LightGray;
            this.tbDataTransform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDataTransform.ChangeVisibility = false;
            this.tbDataTransform.ChildControl = null;
            this.tbDataTransform.ConvertEnterToTab = true;
            this.tbDataTransform.ConvertEnterToTabForDialogs = false;
            this.tbDataTransform.Decimals = 0;
            this.tbDataTransform.DisplayList = new object[0];
            this.tbDataTransform.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDataTransform.Location = new System.Drawing.Point(136, 32);
            this.tbDataTransform.Name = "tbDataTransform";
            this.tbDataTransform.OnDropDownCloseFocus = true;
            this.tbDataTransform.SelectType = 0;
            this.tbDataTransform.Size = new System.Drawing.Size(248, 20);
            this.tbDataTransform.TabIndex = 1;
            this.tbDataTransform.Text = "textBox1";
            this.tbDataTransform.UseValueForChildsVisibilty = false;
            this.tbDataTransform.Value = true;
            // 
            // tbDataSchema
            // 
            this.tbDataSchema.AddX = 0;
            this.tbDataSchema.AddY = 0;
            this.tbDataSchema.AllowSpace = false;
            this.tbDataSchema.BorderColor = System.Drawing.Color.LightGray;
            this.tbDataSchema.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDataSchema.ChangeVisibility = false;
            this.tbDataSchema.ChildControl = null;
            this.tbDataSchema.ConvertEnterToTab = true;
            this.tbDataSchema.ConvertEnterToTabForDialogs = false;
            this.tbDataSchema.Decimals = 0;
            this.tbDataSchema.DisplayList = new object[0];
            this.tbDataSchema.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDataSchema.Location = new System.Drawing.Point(136, 72);
            this.tbDataSchema.Name = "tbDataSchema";
            this.tbDataSchema.OnDropDownCloseFocus = true;
            this.tbDataSchema.SelectType = 0;
            this.tbDataSchema.Size = new System.Drawing.Size(248, 20);
            this.tbDataSchema.TabIndex = 3;
            this.tbDataSchema.Text = "textBox1";
            this.tbDataSchema.UseValueForChildsVisibilty = false;
            this.tbDataSchema.Value = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Schema";
            // 
            // tbDataElementName
            // 
            this.tbDataElementName.AddX = 0;
            this.tbDataElementName.AddY = 0;
            this.tbDataElementName.AllowSpace = false;
            this.tbDataElementName.BorderColor = System.Drawing.Color.LightGray;
            this.tbDataElementName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDataElementName.ChangeVisibility = false;
            this.tbDataElementName.ChildControl = null;
            this.tbDataElementName.ConvertEnterToTab = true;
            this.tbDataElementName.ConvertEnterToTabForDialogs = false;
            this.tbDataElementName.Decimals = 0;
            this.tbDataElementName.DisplayList = new object[0];
            this.tbDataElementName.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDataElementName.Location = new System.Drawing.Point(136, 112);
            this.tbDataElementName.Name = "tbDataElementName";
            this.tbDataElementName.OnDropDownCloseFocus = true;
            this.tbDataElementName.SelectType = 0;
            this.tbDataElementName.Size = new System.Drawing.Size(248, 20);
            this.tbDataElementName.TabIndex = 5;
            this.tbDataElementName.Text = "textBox1";
            this.tbDataElementName.UseValueForChildsVisibilty = false;
            this.tbDataElementName.Value = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Top Element Name";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Element Style";
            // 
            // cbElementStyle
            // 
            this.cbElementStyle.AutoAdjustItemHeight = false;
            this.cbElementStyle.BorderColor = System.Drawing.Color.LightGray;
            this.cbElementStyle.ConvertEnterToTabForDialogs = false;
            this.cbElementStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbElementStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbElementStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbElementStyle.Items.AddRange(new object[] {
            "AttributeNormal",
            "ElementNormal"});
            this.cbElementStyle.Location = new System.Drawing.Point(136, 152);
            this.cbElementStyle.Name = "cbElementStyle";
            this.cbElementStyle.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbElementStyle.SeparatorMargin = 1;
            this.cbElementStyle.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbElementStyle.SeparatorWidth = 1;
            this.cbElementStyle.Size = new System.Drawing.Size(144, 21);
            this.cbElementStyle.TabIndex = 7;
            // 
            // bOpenXsl
            // 
            this.bOpenXsl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOpenXsl.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOpenXsl.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOpenXsl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOpenXsl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpenXsl.Font = new System.Drawing.Font("Arial", 9F);
            this.bOpenXsl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOpenXsl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOpenXsl.Location = new System.Drawing.Point(400, 32);
            this.bOpenXsl.Name = "bOpenXsl";
            this.bOpenXsl.OverriddenSize = null;
            this.bOpenXsl.Size = new System.Drawing.Size(24, 21);
            this.bOpenXsl.TabIndex = 8;
            this.bOpenXsl.Text = "...";
            this.bOpenXsl.UseVisualStyleBackColor = true;
            this.bOpenXsl.Click += new System.EventHandler(this.bOpenXsl_Click);
            // 
            // ReportXmlCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.bOpenXsl);
            this.Controls.Add(this.cbElementStyle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDataElementName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDataSchema);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDataTransform);
            this.Controls.Add(this.label1);
            this.Name = "ReportXmlCtl";
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

			if (tbDataTransform.Text.Length > 0)
				_Draw.SetElement(rNode, "DataTransform", tbDataTransform.Text);
			else
				_Draw.RemoveElement(rNode, "DataTransform");
			
			if (tbDataSchema.Text.Length > 0)
				_Draw.SetElement(rNode, "DataSchema", tbDataSchema.Text);
			else
				_Draw.RemoveElement(rNode, "DataSchema");

			if (tbDataElementName.Text.Length > 0)
				_Draw.SetElement(rNode, "DataElementName", tbDataElementName.Text);
			else
				_Draw.RemoveElement(rNode, "DataElementName");

			_Draw.SetElement(rNode, "DataElementStyle", cbElementStyle.Text);
		}

		private void bOpenXsl_Click(object sender, System.EventArgs e)
		{
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "XSL files (*.xsl)|*.xsl" +
                    "|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.FileName = "*.xsl";

                ofd.Title = "Specify DataTransform File Name";
                //			ofd.DefaultExt = "xsl";
                //			ofd.AddExtension = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string file = Path.GetFileName(ofd.FileName);

                    tbDataTransform.Text = file;
                }
            }
		}
	}
}
