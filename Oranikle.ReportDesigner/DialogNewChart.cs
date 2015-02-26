using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using Oranikle.Report.Engine;


namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for DialogDataSourceRef.
	/// </summary>
	internal class DialogNewChart : System.Windows.Forms.Form
	{
		private DesignXmlDraw _Draw;
		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.StyledComboBox cbDataSets;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox lbFields;
		private System.Windows.Forms.ListBox lbChartCategories;
		private Oranikle.Studio.Controls.StyledButton bCategoryUp;
		private Oranikle.Studio.Controls.StyledButton bCategoryDown;
		private Oranikle.Studio.Controls.StyledButton bCategory;
		private Oranikle.Studio.Controls.StyledButton bSeries;
		private System.Windows.Forms.ListBox lbChartSeries;
		private Oranikle.Studio.Controls.StyledButton bCategoryDelete;
		private Oranikle.Studio.Controls.StyledButton bSeriesDelete;
		private Oranikle.Studio.Controls.StyledButton bSeriesDown;
		private Oranikle.Studio.Controls.StyledButton bSeriesUp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lChartData;
		private Oranikle.Studio.Controls.StyledComboBox cbChartData;
		private System.Windows.Forms.Label label6;
		private Oranikle.Studio.Controls.StyledComboBox cbSubType;
		private Oranikle.Studio.Controls.StyledComboBox cbChartType;
        private System.Windows.Forms.Label label7;
        private Oranikle.Studio.Controls.StyledComboBox cbChartData2;
        private Label lChartData2;
        private Oranikle.Studio.Controls.StyledComboBox cbChartData3;
        private Label lChartData3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DialogNewChart(DesignXmlDraw dxDraw, XmlNode container)
		{
			_Draw = dxDraw;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitValues(container);
		}

		private void InitValues(XmlNode container)
		{
			this.bOK.Enabled = false;		
			//
			// Obtain the existing DataSets info
			//
			object[] datasets = _Draw.DataSetNames;
			if (datasets == null)
				return;		// not much to do if no DataSets

			if (_Draw.IsDataRegion(container))
			{
				string s = _Draw.GetDataSetNameValue(container);
				if (s == null)
					return;
				this.cbDataSets.Items.Add(s);
				this.cbDataSets.Enabled = false;
			}
			else
				this.cbDataSets.Items.AddRange(datasets);
			cbDataSets.SelectedIndex = 0;

			this.cbChartType.SelectedIndex = 2;
		}

		internal string ChartXml
		{
			get 
			{
				StringBuilder chart = new StringBuilder("<Chart><Height>2in</Height><Width>4in</Width>");
				chart.AppendFormat("<DataSetName>{0}</DataSetName>", this.cbDataSets.Text);
				chart.Append("<NoRows>Query returned no rows!</NoRows><Style>"+
					"<BorderStyle><Default>Solid</Default></BorderStyle>"+
					"<BackgroundColor>White</BackgroundColor>"+
					"<BackgroundGradientType>LeftRight</BackgroundGradientType>"+
					"<BackgroundGradientEndColor>Azure</BackgroundGradientEndColor>"+
					"</Style>");
				chart.AppendFormat("<Type>{0}</Type><Subtype>{1}</Subtype>",
					this.cbChartType.Text, this.cbSubType.Text);
				// do the categories
				string tcat="";
				if (this.lbChartCategories.Items.Count > 0)
				{
					chart.Append("<CategoryGroupings>");
					foreach (string cname in this.lbChartCategories.Items)
					{
						if (tcat == "")
							tcat = cname;
						chart.Append("<CategoryGrouping>");
						chart.Append("<DynamicCategories>");
						chart.AppendFormat("<Grouping><GroupExpressions>"+
							"<GroupExpression>=Fields!{0}.Value</GroupExpression>"+
							"</GroupExpressions></Grouping>", cname);

						chart.Append("</DynamicCategories>");
						chart.Append("</CategoryGrouping>");
					}
					chart.Append("</CategoryGroupings>");
					// Do the category axis
					chart.AppendFormat("<CategoryAxis><Axis><Visible>true</Visible>"+
						"<MajorTickMarks>Inside</MajorTickMarks>"+
						"<MajorGridLines><ShowGridLines>true</ShowGridLines>"+
						"<Style><BorderStyle><Default>Solid</Default></BorderStyle>"+
						"</Style></MajorGridLines>" +
						"<MinorGridLines><ShowGridLines>true</ShowGridLines>"+
						"<Style><BorderStyle><Default>Solid</Default></BorderStyle>"+
						"</Style></MinorGridLines>"+
			            "<Title><Caption>{0}</Caption>"+
						"</Title></Axis></CategoryAxis>",tcat);

				}
				// do the series
				string	tser="";
				if (this.lbChartSeries.Items.Count > 0)
				{
					chart.Append("<SeriesGroupings>");
					//If we have chartData Set then we want dynamic series GJL
                    if (this.cbChartData.Text.Length > 0)
                    {
                        foreach (string sname in this.lbChartSeries.Items)
                        {
                            if (tser == "")
                                tser = sname;
                            chart.Append("<SeriesGrouping>");
                            chart.Append("<DynamicSeries>");
                            chart.AppendFormat("<Grouping><GroupExpressions>" +
                                "<GroupExpression>=Fields!{0}.Value</GroupExpression>" +
                                "</GroupExpressions></Grouping>", sname);
                            chart.AppendFormat("<Label>=Fields!{0}.Value</Label>", sname);
                            chart.Append("</DynamicSeries>");
                            chart.Append("</SeriesGrouping>");
                        }
                    }
                    //If we don't have chart data set we want static series GJL
                    else
                    {
                        chart.Append("<SeriesGrouping>");
                        chart.Append("<StaticSeries>");
                        foreach (string sname in this.lbChartSeries.Items)
                        {
                            chart.Append("<StaticMember>");
                            chart.AppendFormat("<Label>{0}</Label>", sname);
                            chart.AppendFormat("<Value>=Fields!{0}.Value</Value>",sname);
                            chart.Append("</StaticMember>");

                        }
                        
                        chart.Append("</StaticSeries>");
                        chart.Append("</SeriesGrouping>");
                        
                    }
					chart.Append("</SeriesGroupings>");
				}
				// Chart Data
                string vtitle; 
				if (this.cbChartData.Text.Length > 0)
				{
                    chart.Append("<ChartData><ChartSeries><DataPoints><DataPoint>" +
                        "<DataValues>");
                    chart.AppendFormat("<DataValue><Value>{0}</Value></DataValue>",
                        this.cbChartData.Text);
                    string ctype = this.cbChartType.Text.ToLowerInvariant();

                    if (ctype == "scatter" || ctype == "bubble")
                    {
                        chart.AppendFormat("<DataValue><Value>{0}</Value></DataValue>",
                            this.cbChartData2.Text);
                        if (ctype == "bubble")
                        {
                            chart.AppendFormat("<DataValue><Value>{0}</Value></DataValue>",
                                this.cbChartData3.Text);
                        }
                    }
                    chart.Append("</DataValues>" +
                        "</DataPoint></DataPoints></ChartSeries></ChartData>");

                    // Do the value axis
				
					int start = this.cbChartData.Text.LastIndexOf("!");
					if (start > 0)
					{
						int end = this.cbChartData.Text.LastIndexOf(".Value");
						if (end < 0 || end <= start+1)
							vtitle = this.cbChartData.Text.Substring(start+1);
						else
							vtitle = this.cbChartData.Text.Substring(start+1, end-start-1);
					}
					else 
						vtitle = "Values";					
				}
                else
                {
                	//If we don't have chartData then use the items in the series box
                	//to create Static Series
                    chart.Append("<ChartData>");
                    foreach (string sname in this.lbChartSeries.Items)
                    {
                        chart.Append("<ChartSeries>");
                        chart.Append("<DataPoints>");
                        chart.Append("<DataPoint>");
                        chart.Append("<DataValues>");
                       
                        if (cbChartType.SelectedItem.Equals("Scatter"))
                        {
                            //we need a y datavalue as well...
                            string xname = (string)lbChartCategories.Items[0];
                            chart.Append("<DataValue>");
                            chart.AppendFormat("<Value>=Fields!{0}.Value</Value>", xname);
                            chart.Append("</DataValue>");
                        chart.Append("<DataValue>");                       
                        chart.AppendFormat("<Value>=Fields!{0}.Value</Value>", sname);
                        chart.Append("</DataValue>");
                        }
                        else
                        {
                            chart.Append("<DataValue>");
                            chart.AppendFormat("<Value>=Fields!{0}.Value</Value>", sname);
                            chart.Append("</DataValue>");
                        }
                        chart.Append("</DataValues>");
                        chart.Append("</DataPoint>");
                        chart.Append("</DataPoints>");
                        chart.Append("</ChartSeries>");
                    }
                    chart.Append("</ChartData>");
                    vtitle = "Values";		

                }

                chart.AppendFormat("<ValueAxis><Axis><Visible>true</Visible>" +
                        "<MajorTickMarks>Inside</MajorTickMarks>" +
                        "<MajorGridLines><ShowGridLines>true</ShowGridLines>" +
                        "<Style><BorderStyle><Default>Solid</Default></BorderStyle>" +
                        "<FontSize>8pt</FontSize>" +
                        "</Style></MajorGridLines>" +
                        "<MinorGridLines><ShowGridLines>true</ShowGridLines>" +
                        "<Style><BorderStyle><Default>Solid</Default></BorderStyle>" +
                        "</Style></MinorGridLines>" +
                        "<Title><Caption>{0}</Caption>" +
                        "<Style><WritingMode>tb-rl</WritingMode></Style>" +
                        "</Title></Axis></ValueAxis>", vtitle);

				// Legend
				chart.Append("<Legend><Style><BorderStyle><Default>Solid</Default>"+
					"</BorderStyle><PaddingLeft>5pt</PaddingLeft>"+
					"<FontSize>8pt</FontSize></Style><Visible>true</Visible>"+
					"<Position>RightCenter</Position></Legend>");

				// Title
				chart.AppendFormat("<Title><Style><FontWeight>Bold</FontWeight>"+
					"<FontSize>14pt</FontSize><TextAlign>Center</TextAlign>"+
					"</Style><Caption>{0} {1} Chart</Caption></Title>", tcat, tser);

				// end of Chart defintion
				chart.Append("</Chart>");

				return chart.ToString();
			}
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbDataSets = new Oranikle.Studio.Controls.StyledComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbFields = new System.Windows.Forms.ListBox();
            this.lbChartCategories = new System.Windows.Forms.ListBox();
            this.bCategoryUp = new Oranikle.Studio.Controls.StyledButton();
            this.bCategoryDown = new Oranikle.Studio.Controls.StyledButton();
            this.bCategory = new Oranikle.Studio.Controls.StyledButton();
            this.bSeries = new Oranikle.Studio.Controls.StyledButton();
            this.lbChartSeries = new System.Windows.Forms.ListBox();
            this.bCategoryDelete = new Oranikle.Studio.Controls.StyledButton();
            this.bSeriesDelete = new Oranikle.Studio.Controls.StyledButton();
            this.bSeriesDown = new Oranikle.Studio.Controls.StyledButton();
            this.bSeriesUp = new Oranikle.Studio.Controls.StyledButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lChartData = new System.Windows.Forms.Label();
            this.cbChartData = new Oranikle.Studio.Controls.StyledComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSubType = new Oranikle.Studio.Controls.StyledComboBox();
            this.cbChartType = new Oranikle.Studio.Controls.StyledComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbChartData2 = new Oranikle.Studio.Controls.StyledComboBox();
            this.lChartData2 = new System.Windows.Forms.Label();
            this.cbChartData3 = new Oranikle.Studio.Controls.StyledComboBox();
            this.lChartData3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOK.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOK.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOK.Font = new System.Drawing.Font("Arial", 9F);
            this.bOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOK.Location = new System.Drawing.Point(272, 392);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 21);
            this.bOK.TabIndex = 16;
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
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.bCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(368, 392);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 21);
            this.bCancel.TabIndex = 17;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "DataSet";
            // 
            // cbDataSets
            // 
            this.cbDataSets.AutoAdjustItemHeight = false;
            this.cbDataSets.BorderColor = System.Drawing.Color.LightGray;
            this.cbDataSets.ConvertEnterToTabForDialogs = false;
            this.cbDataSets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDataSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDataSets.Location = new System.Drawing.Point(80, 16);
            this.cbDataSets.Name = "cbDataSets";
            this.cbDataSets.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbDataSets.SeparatorMargin = 1;
            this.cbDataSets.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbDataSets.SeparatorWidth = 1;
            this.cbDataSets.Size = new System.Drawing.Size(360, 21);
            this.cbDataSets.TabIndex = 0;
            this.cbDataSets.SelectedIndexChanged += new System.EventHandler(this.cbDataSets_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "DataSet Fields";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(224, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 24);
            this.label3.TabIndex = 14;
            this.label3.Text = "Chart Categories (X) Groupings";
            // 
            // lbFields
            // 
            this.lbFields.Location = new System.Drawing.Point(16, 104);
            this.lbFields.Name = "lbFields";
            this.lbFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFields.Size = new System.Drawing.Size(152, 160);
            this.lbFields.TabIndex = 3;
            // 
            // lbChartCategories
            // 
            this.lbChartCategories.Location = new System.Drawing.Point(232, 104);
            this.lbChartCategories.Name = "lbChartCategories";
            this.lbChartCategories.Size = new System.Drawing.Size(152, 69);
            this.lbChartCategories.TabIndex = 4;
            // 
            // bCategoryUp
            // 
            this.bCategoryUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCategoryUp.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCategoryUp.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCategoryUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCategoryUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCategoryUp.Font = new System.Drawing.Font("Arial", 9F);
            this.bCategoryUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCategoryUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCategoryUp.Location = new System.Drawing.Point(398, 94);
            this.bCategoryUp.Name = "bCategoryUp";
            this.bCategoryUp.OverriddenSize = null;
            this.bCategoryUp.Size = new System.Drawing.Size(49, 21);
            this.bCategoryUp.TabIndex = 5;
            this.bCategoryUp.Text = "Up";
            this.bCategoryUp.UseVisualStyleBackColor = true;
            this.bCategoryUp.Click += new System.EventHandler(this.bCategoryUp_Click);
            // 
            // bCategoryDown
            // 
            this.bCategoryDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCategoryDown.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCategoryDown.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCategoryDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCategoryDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCategoryDown.Font = new System.Drawing.Font("Arial", 9F);
            this.bCategoryDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCategoryDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCategoryDown.Location = new System.Drawing.Point(398, 124);
            this.bCategoryDown.Name = "bCategoryDown";
            this.bCategoryDown.OverriddenSize = null;
            this.bCategoryDown.Size = new System.Drawing.Size(49, 21);
            this.bCategoryDown.TabIndex = 6;
            this.bCategoryDown.Text = "Down";
            this.bCategoryDown.UseVisualStyleBackColor = true;
            this.bCategoryDown.Click += new System.EventHandler(this.bCategoryDown_Click);
            // 
            // bCategory
            // 
            this.bCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCategory.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCategory.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCategory.Font = new System.Drawing.Font("Arial", 9F);
            this.bCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCategory.Location = new System.Drawing.Point(184, 112);
            this.bCategory.Name = "bCategory";
            this.bCategory.OverriddenSize = null;
            this.bCategory.Size = new System.Drawing.Size(32, 21);
            this.bCategory.TabIndex = 2;
            this.bCategory.Text = ">";
            this.bCategory.UseVisualStyleBackColor = true;
            this.bCategory.Click += new System.EventHandler(this.bCategory_Click);
            // 
            // bSeries
            // 
            this.bSeries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSeries.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSeries.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSeries.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSeries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSeries.Font = new System.Drawing.Font("Arial", 9F);
            this.bSeries.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSeries.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSeries.Location = new System.Drawing.Point(185, 199);
            this.bSeries.Name = "bSeries";
            this.bSeries.OverriddenSize = null;
            this.bSeries.Size = new System.Drawing.Size(32, 21);
            this.bSeries.TabIndex = 8;
            this.bSeries.Text = ">";
            this.bSeries.UseVisualStyleBackColor = true;
            this.bSeries.Click += new System.EventHandler(this.bSeries_Click);
            // 
            // lbChartSeries
            // 
            this.lbChartSeries.Location = new System.Drawing.Point(232, 201);
            this.lbChartSeries.Name = "lbChartSeries";
            this.lbChartSeries.Size = new System.Drawing.Size(152, 69);
            this.lbChartSeries.TabIndex = 9;
            // 
            // bCategoryDelete
            // 
            this.bCategoryDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCategoryDelete.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCategoryDelete.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCategoryDelete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCategoryDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCategoryDelete.Font = new System.Drawing.Font("Arial", 9F);
            this.bCategoryDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCategoryDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCategoryDelete.Location = new System.Drawing.Point(398, 153);
            this.bCategoryDelete.Name = "bCategoryDelete";
            this.bCategoryDelete.OverriddenSize = null;
            this.bCategoryDelete.Size = new System.Drawing.Size(49, 21);
            this.bCategoryDelete.TabIndex = 7;
            this.bCategoryDelete.Text = "Delete";
            this.bCategoryDelete.UseVisualStyleBackColor = true;
            this.bCategoryDelete.Click += new System.EventHandler(this.bCategoryDelete_Click);
            // 
            // bSeriesDelete
            // 
            this.bSeriesDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSeriesDelete.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSeriesDelete.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSeriesDelete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSeriesDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSeriesDelete.Font = new System.Drawing.Font("Arial", 9F);
            this.bSeriesDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSeriesDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSeriesDelete.Location = new System.Drawing.Point(398, 258);
            this.bSeriesDelete.Name = "bSeriesDelete";
            this.bSeriesDelete.OverriddenSize = null;
            this.bSeriesDelete.Size = new System.Drawing.Size(49, 21);
            this.bSeriesDelete.TabIndex = 12;
            this.bSeriesDelete.Text = "Delete";
            this.bSeriesDelete.UseVisualStyleBackColor = true;
            this.bSeriesDelete.Click += new System.EventHandler(this.bSeriesDelete_Click);
            // 
            // bSeriesDown
            // 
            this.bSeriesDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSeriesDown.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSeriesDown.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSeriesDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSeriesDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSeriesDown.Font = new System.Drawing.Font("Arial", 9F);
            this.bSeriesDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSeriesDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSeriesDown.Location = new System.Drawing.Point(398, 229);
            this.bSeriesDown.Name = "bSeriesDown";
            this.bSeriesDown.OverriddenSize = null;
            this.bSeriesDown.Size = new System.Drawing.Size(49, 21);
            this.bSeriesDown.TabIndex = 11;
            this.bSeriesDown.Text = "Down";
            this.bSeriesDown.UseVisualStyleBackColor = true;
            this.bSeriesDown.Click += new System.EventHandler(this.bSeriesDown_Click);
            // 
            // bSeriesUp
            // 
            this.bSeriesUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSeriesUp.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSeriesUp.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSeriesUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSeriesUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSeriesUp.Font = new System.Drawing.Font("Arial", 9F);
            this.bSeriesUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSeriesUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSeriesUp.Location = new System.Drawing.Point(398, 199);
            this.bSeriesUp.Name = "bSeriesUp";
            this.bSeriesUp.OverriddenSize = null;
            this.bSeriesUp.Size = new System.Drawing.Size(49, 21);
            this.bSeriesUp.TabIndex = 10;
            this.bSeriesUp.Text = "Up";
            this.bSeriesUp.UseVisualStyleBackColor = true;
            this.bSeriesUp.Click += new System.EventHandler(this.bSeriesUp_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(224, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 14);
            this.label4.TabIndex = 31;
            this.label4.Text = "Chart Series";
            // 
            // lChartData
            // 
            this.lChartData.Location = new System.Drawing.Point(16, 315);
            this.lChartData.Name = "lChartData";
            this.lChartData.Size = new System.Drawing.Size(112, 23);
            this.lChartData.TabIndex = 32;
            this.lChartData.Text = "Data Expression";
            // 
            // cbChartData
            // 
            this.cbChartData.AutoAdjustItemHeight = false;
            this.cbChartData.BorderColor = System.Drawing.Color.LightGray;
            this.cbChartData.ConvertEnterToTabForDialogs = false;
            this.cbChartData.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbChartData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbChartData.Location = new System.Drawing.Point(137, 312);
            this.cbChartData.Name = "cbChartData";
            this.cbChartData.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbChartData.SeparatorMargin = 1;
            this.cbChartData.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbChartData.SeparatorWidth = 1;
            this.cbChartData.Size = new System.Drawing.Size(247, 21);
            this.cbChartData.TabIndex = 13;
            this.cbChartData.Enter += new System.EventHandler(this.cbChartData_Enter);
            this.cbChartData.TextChanged += new System.EventHandler(this.cbChartData_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(232, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 23);
            this.label6.TabIndex = 36;
            this.label6.Text = "Sub-type";
            // 
            // cbSubType
            // 
            this.cbSubType.AutoAdjustItemHeight = false;
            this.cbSubType.BorderColor = System.Drawing.Color.LightGray;
            this.cbSubType.ConvertEnterToTabForDialogs = false;
            this.cbSubType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSubType.Location = new System.Drawing.Point(312, 48);
            this.cbSubType.Name = "cbSubType";
            this.cbSubType.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbSubType.SeparatorMargin = 1;
            this.cbSubType.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbSubType.SeparatorWidth = 1;
            this.cbSubType.Size = new System.Drawing.Size(80, 21);
            this.cbSubType.TabIndex = 2;
            // 
            // cbChartType
            // 
            this.cbChartType.AutoAdjustItemHeight = false;
            this.cbChartType.BorderColor = System.Drawing.Color.LightGray;
            this.cbChartType.ConvertEnterToTabForDialogs = false;
            this.cbChartType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChartType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbChartType.Items.AddRange(new object[] {
            "Area",
            "Bar",
            "Column",
            "Doughnut",
            "Line",
            "Pie",
            "Bubble",
            "Scatter"});
            this.cbChartType.Location = new System.Drawing.Point(96, 48);
            this.cbChartType.Name = "cbChartType";
            this.cbChartType.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbChartType.SeparatorMargin = 1;
            this.cbChartType.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbChartType.SeparatorWidth = 1;
            this.cbChartType.Size = new System.Drawing.Size(121, 21);
            this.cbChartType.TabIndex = 1;
            this.cbChartType.SelectedIndexChanged += new System.EventHandler(this.cbChartType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 33;
            this.label7.Text = "Chart Type";
            // 
            // cbChartData2
            // 
            this.cbChartData2.AutoAdjustItemHeight = false;
            this.cbChartData2.BorderColor = System.Drawing.Color.LightGray;
            this.cbChartData2.ConvertEnterToTabForDialogs = false;
            this.cbChartData2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbChartData2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbChartData2.Location = new System.Drawing.Point(137, 339);
            this.cbChartData2.Name = "cbChartData2";
            this.cbChartData2.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbChartData2.SeparatorMargin = 1;
            this.cbChartData2.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbChartData2.SeparatorWidth = 1;
            this.cbChartData2.Size = new System.Drawing.Size(247, 21);
            this.cbChartData2.TabIndex = 14;
            this.cbChartData2.Enter += new System.EventHandler(this.cbChartData_Enter);
            this.cbChartData2.TextChanged += new System.EventHandler(this.cbChartData_TextChanged);
            // 
            // lChartData2
            // 
            this.lChartData2.Location = new System.Drawing.Point(18, 342);
            this.lChartData2.Name = "lChartData2";
            this.lChartData2.Size = new System.Drawing.Size(112, 23);
            this.lChartData2.TabIndex = 38;
            this.lChartData2.Text = "Y Coordinate";
            // 
            // cbChartData3
            // 
            this.cbChartData3.AutoAdjustItemHeight = false;
            this.cbChartData3.BorderColor = System.Drawing.Color.LightGray;
            this.cbChartData3.ConvertEnterToTabForDialogs = false;
            this.cbChartData3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbChartData3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbChartData3.Location = new System.Drawing.Point(137, 366);
            this.cbChartData3.Name = "cbChartData3";
            this.cbChartData3.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbChartData3.SeparatorMargin = 1;
            this.cbChartData3.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbChartData3.SeparatorWidth = 1;
            this.cbChartData3.Size = new System.Drawing.Size(247, 21);
            this.cbChartData3.TabIndex = 15;
            this.cbChartData3.Enter += new System.EventHandler(this.cbChartData_Enter);
            this.cbChartData3.TextChanged += new System.EventHandler(this.cbChartData_TextChanged);
            // 
            // lChartData3
            // 
            this.lChartData3.Location = new System.Drawing.Point(18, 369);
            this.lChartData3.Name = "lChartData3";
            this.lChartData3.Size = new System.Drawing.Size(112, 23);
            this.lChartData3.TabIndex = 40;
            this.lChartData3.Text = "Bubble Size";
            // 
            // DialogNewChart
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(457, 425);
            this.Controls.Add(this.cbChartData3);
            this.Controls.Add(this.lChartData3);
            this.Controls.Add(this.cbChartData2);
            this.Controls.Add(this.lChartData2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSubType);
            this.Controls.Add(this.cbChartType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbChartData);
            this.Controls.Add(this.lChartData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bSeriesDelete);
            this.Controls.Add(this.bSeriesDown);
            this.Controls.Add(this.bSeriesUp);
            this.Controls.Add(this.bCategoryDelete);
            this.Controls.Add(this.lbChartSeries);
            this.Controls.Add(this.bSeries);
            this.Controls.Add(this.bCategory);
            this.Controls.Add(this.bCategoryDown);
            this.Controls.Add(this.bCategoryUp);
            this.Controls.Add(this.lbChartCategories);
            this.Controls.Add(this.lbFields);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDataSets);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogNewChart";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Chart";
            this.ResumeLayout(false);

		}
		#endregion

		private void bOK_Click(object sender, System.EventArgs e)
		{
            bool bFail = false;
            string ctype = cbChartType.Text.ToLowerInvariant();
            if (cbChartData.Text.Length == 0 && lbChartSeries.Items.Count == 0) //Added second condition 05122007GJL
            {
                MessageBox.Show("Please fill out the chart data expression, or include a series.");
                bFail = true;
            }
            else if (ctype == "scatter" && cbChartData2.Text.Length == 0 && lbChartSeries.Items.Count == 0)
            {
                MessageBox.Show("Please fill out the chart Y coordinate data expression.");
                bFail = true;
                
            }
            else if (ctype == "bubble" && (cbChartData2.Text.Length == 0 || cbChartData3.Text.Length == 0))
            {
                MessageBox.Show("Please fill out the chart Y coordinate and Bubble width expressions.");
                bFail = true;
            }
            if (bFail)
                return;
			// apply the result
			DialogResult = DialogResult.OK;
		}

		private void cbDataSets_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.lbChartCategories.Items.Clear();
			this.lbChartSeries.Items.Clear();
			bOK.Enabled = false;
			this.lbFields.Items.Clear();
			string [] fields = _Draw.GetFields(cbDataSets.Text, false);
			if (fields != null)
				lbFields.Items.AddRange(fields);
		}

		private void bCategory_Click(object sender, System.EventArgs e)
		{
			ICollection sic = lbFields.SelectedIndices;
			int count=sic.Count;
			foreach (int i in sic)
			{
				string fname = (string) lbFields.Items[i];
				if (this.lbChartCategories.Items.IndexOf(fname) < 0)
					lbChartCategories.Items.Add(fname);
			}
			OkEnable();
		}

		private void bSeries_Click(object sender, System.EventArgs e)
		{
			ICollection sic = lbFields.SelectedIndices;
			int count=sic.Count;
			foreach (int i in sic)
			{
				string fname = (string) lbFields.Items[i];
				if (this.lbChartSeries.Items.IndexOf(fname) < 0 || cbChartType.SelectedItem.Equals("Scatter"))
					lbChartSeries.Items.Add(fname);
			}
			OkEnable();
		}

		private void bCategoryUp_Click(object sender, System.EventArgs e)
		{
			int index = lbChartCategories.SelectedIndex;
			if (index <= 0)
				return;

			string prename = (string) lbChartCategories.Items[index-1];
			lbChartCategories.Items.RemoveAt(index-1);
			lbChartCategories.Items.Insert(index, prename);
		}

		private void bCategoryDown_Click(object sender, System.EventArgs e)
		{
			int index = lbChartCategories.SelectedIndex;
			if (index < 0 || index + 1 == lbChartCategories.Items.Count)
				return;

			string postname = (string) lbChartCategories.Items[index+1];
			lbChartCategories.Items.RemoveAt(index+1);
			lbChartCategories.Items.Insert(index, postname);
		}

		private void bCategoryDelete_Click(object sender, System.EventArgs e)
		{
			int index = lbChartCategories.SelectedIndex;
			if (index < 0)
				return;

			lbChartCategories.Items.RemoveAt(index);
			OkEnable();
		}

		private void bSeriesUp_Click(object sender, System.EventArgs e)
		{
			int index = lbChartSeries.SelectedIndex;
			if (index <= 0)
				return;

			string prename = (string) lbChartSeries.Items[index-1];
			lbChartSeries.Items.RemoveAt(index-1);
			lbChartSeries.Items.Insert(index, prename);
		}

		private void bSeriesDown_Click(object sender, System.EventArgs e)
		{
			int index = lbChartSeries.SelectedIndex;
			if (index < 0 || index + 1 == lbChartSeries.Items.Count)
				return;

			string postname = (string) lbChartSeries.Items[index+1];
			lbChartSeries.Items.RemoveAt(index+1);
			lbChartSeries.Items.Insert(index, postname);
		}

		private void bSeriesDelete_Click(object sender, System.EventArgs e)
		{
			int index = lbChartSeries.SelectedIndex;
			if (index < 0)
				return;

			lbChartSeries.Items.RemoveAt(index);
			OkEnable();
		}

		private void OkEnable()
		{
			// We need values in datasets and Categories or Series for OK to work correctly
            bool bEnable = (this.lbChartCategories.Items.Count > 0 ||
                          this.lbChartSeries.Items.Count > 0) &&
                        this.cbDataSets.Text != null &&
                        this.cbDataSets.Text.Length > 0;
                       // && this.cbChartData.Text.Length > 0; Not needed with static series 05122007GJL
            string ctype = cbChartType.Text.ToLowerInvariant();
            if (ctype == "scatter")
                bEnable = bEnable && (this.cbChartData2.Text.Length > 0 || lbChartSeries.Items.Count > 0);
            else if (ctype == "bubble")
                bEnable = bEnable && (this.cbChartData2.Text.Length > 0 && this.cbChartData3.Text.Length > 0);
            
            bOK.Enabled = bEnable;
        }

		private void cbChartData_Enter(object sender, System.EventArgs e)
		{
            ComboBox cb = sender as ComboBox;
            if (cb == null)
                return;
			cb.Items.Clear();
			foreach (string field in this.lbFields.Items)
			{
				if (this.lbChartCategories.Items.IndexOf(field) >= 0 ||
					this.lbChartSeries.Items.IndexOf(field) >= 0)
					continue;
				// Field selected in columns and rows
				cb.Items.Add(string.Format("=Sum(Fields!{0}.Value)", field));
			}
		}

		private void cbChartData_TextChanged(object sender, System.EventArgs e)
		{
			OkEnable();
		}

		private void cbChartType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Change the potential sub-types
			string savesub = cbSubType.Text;
			string[] subItems;
            bool bEnableData2 = false;
            bool bEnableData3 = false;
			switch (cbChartType.Text)
			{
				case "Column":
					subItems = new string [] {"Plain", "Stacked", "PercentStacked"};
					break;
				case "Bar":
					subItems = new string [] {"Plain", "Stacked", "PercentStacked"};
					break;
				case "Line":
					subItems = new string [] {"Plain", "Smooth"};
					break;
				case "Pie":
					subItems = new string [] {"Plain", "Exploded"};
					break;
				case "Area":
					subItems = new string [] {"Plain", "Stacked"};
					break;
				case "Doughnut":
					subItems = new string [] {"Plain"};
					break;
				case "Scatter":
					subItems = new string [] {"Plain", "Line", "SmoothLine"};
                    bEnableData2 = true;
                    break;
				case "Bubble":
                    subItems = new string[] { "Plain" };
                    bEnableData2 = bEnableData3 = true;
                    break;
                case "Stock":
                default:
					subItems = new string [] {"Plain"};
					break;
			}
            lChartData2.Enabled = cbChartData2.Enabled = bEnableData2;
            lChartData3.Enabled = cbChartData3.Enabled = bEnableData3;
            
            // handle the subtype
            cbSubType.Items.Clear();
			cbSubType.Items.AddRange(subItems);
			int i=0;
			foreach (string s in subItems)
			{
				if (s == savesub)
				{
					cbSubType.SelectedIndex = i;
					break;
				}
				i++;
			}
			// Didn't match old style
            if (i >= subItems.Length)
                i = 0;
            cbSubType.SelectedIndex = i;
        }

      
	}
}
