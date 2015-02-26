namespace Oranikle.ReportDesigner
{
    partial class StaticSeriesCtl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lbDataSeries = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkShowLabels = new Oranikle.Studio.Controls.StyledCheckBox();
            this.txtSeriesName = new Oranikle.Studio.Controls.CustomTextControl();
            this.txtLabelValue = new Oranikle.Studio.Controls.CustomTextControl();
            this.btnAdd = new Oranikle.Studio.Controls.StyledButton();
            this.btnDel = new Oranikle.Studio.Controls.StyledButton();
            this.btnLabelValue = new Oranikle.Studio.Controls.StyledButton();
            this.btnDataValue = new Oranikle.Studio.Controls.StyledButton();
            this.btnSeriesName = new Oranikle.Studio.Controls.StyledButton();
            this.txtDataValue = new Oranikle.Studio.Controls.CustomTextControl();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPlotType = new Oranikle.Studio.Controls.StyledComboBox();
            this.chkLeft = new System.Windows.Forms.RadioButton();
            this.chkRight = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUp = new Oranikle.Studio.Controls.StyledButton();
            this.btnDown = new Oranikle.Studio.Controls.StyledButton();
            this.txtX = new Oranikle.Studio.Controls.CustomTextControl();
            this.label6 = new System.Windows.Forms.Label();
            this.btnX = new Oranikle.Studio.Controls.StyledButton();
            this.chkMarker = new Oranikle.Studio.Controls.StyledCheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbLine = new Oranikle.Studio.Controls.StyledComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.colorPicker1 = new Oranikle.ReportDesigner.ColorPicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Series";
            // 
            // lbDataSeries
            // 
            this.lbDataSeries.FormattingEnabled = true;
            this.lbDataSeries.Location = new System.Drawing.Point(19, 28);
            this.lbDataSeries.Name = "lbDataSeries";
            this.lbDataSeries.Size = new System.Drawing.Size(120, 134);
            this.lbDataSeries.TabIndex = 1;
            this.lbDataSeries.SelectedIndexChanged += new System.EventHandler(this.lbDataSeries_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Series Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data Value";
            // 
            // chkShowLabels
            // 
            this.chkShowLabels.AutoSize = true;
            this.chkShowLabels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowLabels.ForeColor = System.Drawing.Color.Black;
            this.chkShowLabels.Location = new System.Drawing.Point(148, 132);
            this.chkShowLabels.Name = "chkShowLabels";
            this.chkShowLabels.Size = new System.Drawing.Size(90, 17);
            this.chkShowLabels.TabIndex = 4;
            this.chkShowLabels.Text = "Show Labels?";
            this.chkShowLabels.UseVisualStyleBackColor = true;
            this.chkShowLabels.CheckedChanged += new System.EventHandler(this.chkShowLabels_CheckedChanged);
            // 
            // txtSeriesName
            // 
            this.txtSeriesName.AddX = 0;
            this.txtSeriesName.AddY = 0;
            this.txtSeriesName.AllowSpace = false;
            this.txtSeriesName.BorderColor = System.Drawing.Color.LightGray;
            this.txtSeriesName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSeriesName.ChangeVisibility = false;
            this.txtSeriesName.ChildControl = null;
            this.txtSeriesName.ConvertEnterToTab = true;
            this.txtSeriesName.ConvertEnterToTabForDialogs = false;
            this.txtSeriesName.Decimals = 0;
            this.txtSeriesName.DisplayList = new object[0];
            this.txtSeriesName.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtSeriesName.Location = new System.Drawing.Point(145, 28);
            this.txtSeriesName.Name = "txtSeriesName";
            this.txtSeriesName.OnDropDownCloseFocus = true;
            this.txtSeriesName.SelectType = 0;
            this.txtSeriesName.Size = new System.Drawing.Size(184, 20);
            this.txtSeriesName.TabIndex = 5;
            this.txtSeriesName.UseValueForChildsVisibilty = false;
            this.txtSeriesName.Value = true;
            this.txtSeriesName.TextChanged += new System.EventHandler(this.txtSeriesName_TextChanged);
            // 
            // txtLabelValue
            // 
            this.txtLabelValue.AddX = 0;
            this.txtLabelValue.AddY = 0;
            this.txtLabelValue.AllowSpace = false;
            this.txtLabelValue.BorderColor = System.Drawing.Color.LightGray;
            this.txtLabelValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLabelValue.ChangeVisibility = false;
            this.txtLabelValue.ChildControl = null;
            this.txtLabelValue.ConvertEnterToTab = true;
            this.txtLabelValue.ConvertEnterToTabForDialogs = false;
            this.txtLabelValue.Decimals = 0;
            this.txtLabelValue.DisplayList = new object[0];
            this.txtLabelValue.Enabled = false;
            this.txtLabelValue.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtLabelValue.Location = new System.Drawing.Point(145, 152);
            this.txtLabelValue.Name = "txtLabelValue";
            this.txtLabelValue.OnDropDownCloseFocus = true;
            this.txtLabelValue.SelectType = 0;
            this.txtLabelValue.Size = new System.Drawing.Size(184, 20);
            this.txtLabelValue.TabIndex = 7;
            this.txtLabelValue.UseValueForChildsVisibilty = false;
            this.txtLabelValue.Value = true;
            this.txtLabelValue.TextChanged += new System.EventHandler(this.txtLabelValue_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnAdd.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnAdd.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnAdd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 9F);
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(89, 168);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OverriddenSize = null;
            this.btnAdd.Size = new System.Drawing.Size(50, 21);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnDel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnDel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnDel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("Arial", 9F);
            this.btnDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(19, 168);
            this.btnDel.Name = "btnDel";
            this.btnDel.OverriddenSize = null;
            this.btnDel.Size = new System.Drawing.Size(50, 21);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnLabelValue
            // 
            this.btnLabelValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnLabelValue.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnLabelValue.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnLabelValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnLabelValue.Enabled = false;
            this.btnLabelValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLabelValue.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnLabelValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnLabelValue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLabelValue.Location = new System.Drawing.Point(335, 152);
            this.btnLabelValue.Name = "btnLabelValue";
            this.btnLabelValue.OverriddenSize = null;
            this.btnLabelValue.Size = new System.Drawing.Size(22, 21);
            this.btnLabelValue.TabIndex = 21;
            this.btnLabelValue.Text = "fx";
            this.btnLabelValue.UseVisualStyleBackColor = true;
            this.btnLabelValue.Click += new System.EventHandler(this.FunctionButtonClick);
            // 
            // btnDataValue
            // 
            this.btnDataValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnDataValue.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnDataValue.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnDataValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnDataValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataValue.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnDataValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDataValue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataValue.Location = new System.Drawing.Point(335, 67);
            this.btnDataValue.Name = "btnDataValue";
            this.btnDataValue.OverriddenSize = null;
            this.btnDataValue.Size = new System.Drawing.Size(22, 21);
            this.btnDataValue.TabIndex = 22;
            this.btnDataValue.Text = "fx";
            this.btnDataValue.UseVisualStyleBackColor = true;
            this.btnDataValue.Click += new System.EventHandler(this.FunctionButtonClick);
            // 
            // btnSeriesName
            // 
            this.btnSeriesName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnSeriesName.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnSeriesName.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnSeriesName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnSeriesName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeriesName.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnSeriesName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnSeriesName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeriesName.Location = new System.Drawing.Point(335, 27);
            this.btnSeriesName.Name = "btnSeriesName";
            this.btnSeriesName.OverriddenSize = null;
            this.btnSeriesName.Size = new System.Drawing.Size(22, 21);
            this.btnSeriesName.TabIndex = 23;
            this.btnSeriesName.Text = "fx";
            this.btnSeriesName.UseVisualStyleBackColor = true;
            this.btnSeriesName.Click += new System.EventHandler(this.FunctionButtonClick);
            // 
            // txtDataValue
            // 
            this.txtDataValue.AddX = 0;
            this.txtDataValue.AddY = 0;
            this.txtDataValue.AllowSpace = false;
            this.txtDataValue.BorderColor = System.Drawing.Color.LightGray;
            this.txtDataValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataValue.ChangeVisibility = false;
            this.txtDataValue.ChildControl = null;
            this.txtDataValue.ConvertEnterToTab = true;
            this.txtDataValue.ConvertEnterToTabForDialogs = false;
            this.txtDataValue.Decimals = 0;
            this.txtDataValue.DisplayList = new object[0];
            this.txtDataValue.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtDataValue.Location = new System.Drawing.Point(145, 67);
            this.txtDataValue.Name = "txtDataValue";
            this.txtDataValue.OnDropDownCloseFocus = true;
            this.txtDataValue.SelectType = 0;
            this.txtDataValue.Size = new System.Drawing.Size(184, 20);
            this.txtDataValue.TabIndex = 24;
            this.txtDataValue.UseValueForChildsVisibilty = false;
            this.txtDataValue.Value = true;
            this.txtDataValue.TextChanged += new System.EventHandler(this.txtDataValue_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Series Plot Type";
            // 
            // cbPlotType
            // 
            this.cbPlotType.AutoAdjustItemHeight = false;
            this.cbPlotType.BorderColor = System.Drawing.Color.LightGray;
            this.cbPlotType.ConvertEnterToTabForDialogs = false;
            this.cbPlotType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbPlotType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPlotType.FormattingEnabled = true;
            this.cbPlotType.Items.AddRange(new object[] {
            "Auto",
            "Line"});
            this.cbPlotType.Location = new System.Drawing.Point(145, 191);
            this.cbPlotType.Name = "cbPlotType";
            this.cbPlotType.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbPlotType.SeparatorMargin = 1;
            this.cbPlotType.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbPlotType.SeparatorWidth = 1;
            this.cbPlotType.Size = new System.Drawing.Size(134, 21);
            this.cbPlotType.TabIndex = 26;
            this.cbPlotType.SelectedIndexChanged += new System.EventHandler(this.cbPlotType_SelectedIndexChanged);
            // 
            // chkLeft
            // 
            this.chkLeft.AutoSize = true;
            this.chkLeft.Location = new System.Drawing.Point(249, 229);
            this.chkLeft.Name = "chkLeft";
            this.chkLeft.Size = new System.Drawing.Size(43, 17);
            this.chkLeft.TabIndex = 27;
            this.chkLeft.TabStop = true;
            this.chkLeft.Text = "Left";
            this.chkLeft.UseVisualStyleBackColor = true;
            this.chkLeft.CheckedChanged += new System.EventHandler(this.chkLeft_CheckedChanged);
            // 
            // chkRight
            // 
            this.chkRight.AutoSize = true;
            this.chkRight.Location = new System.Drawing.Point(312, 229);
            this.chkRight.Name = "chkRight";
            this.chkRight.Size = new System.Drawing.Size(50, 17);
            this.chkRight.TabIndex = 28;
            this.chkRight.TabStop = true;
            this.chkRight.Text = "Right";
            this.chkRight.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(283, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Y Axis";
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnUp.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnUp.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Arial", 9F);
            this.btnUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUp.Location = new System.Drawing.Point(1, 28);
            this.btnUp.Name = "btnUp";
            this.btnUp.OverriddenSize = null;
            this.btnUp.Size = new System.Drawing.Size(17, 21);
            this.btnUp.TabIndex = 30;
            this.btnUp.Text = "^";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnDown.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnDown.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Arial", 9F);
            this.btnDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDown.Location = new System.Drawing.Point(1, 139);
            this.btnDown.Name = "btnDown";
            this.btnDown.OverriddenSize = null;
            this.btnDown.Size = new System.Drawing.Size(17, 21);
            this.btnDown.TabIndex = 31;
            this.btnDown.Text = "v";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // txtX
            // 
            this.txtX.AddX = 0;
            this.txtX.AddY = 0;
            this.txtX.AllowSpace = false;
            this.txtX.BorderColor = System.Drawing.Color.LightGray;
            this.txtX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtX.ChangeVisibility = false;
            this.txtX.ChildControl = null;
            this.txtX.ConvertEnterToTab = true;
            this.txtX.ConvertEnterToTabForDialogs = false;
            this.txtX.Decimals = 0;
            this.txtX.DisplayList = new object[0];
            this.txtX.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtX.Location = new System.Drawing.Point(145, 106);
            this.txtX.Name = "txtX";
            this.txtX.OnDropDownCloseFocus = true;
            this.txtX.SelectType = 0;
            this.txtX.Size = new System.Drawing.Size(184, 20);
            this.txtX.TabIndex = 32;
            this.txtX.UseValueForChildsVisibilty = false;
            this.txtX.Value = true;
            this.txtX.TextChanged += new System.EventHandler(this.txtX_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "X Value(Scatter only)";
            // 
            // btnX
            // 
            this.btnX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnX.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnX.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnX.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnX.Enabled = false;
            this.btnX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnX.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnX.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnX.Location = new System.Drawing.Point(335, 103);
            this.btnX.Name = "btnX";
            this.btnX.OverriddenSize = null;
            this.btnX.Size = new System.Drawing.Size(22, 21);
            this.btnX.TabIndex = 34;
            this.btnX.Text = "fx";
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.FunctionButtonClick);
            // 
            // chkMarker
            // 
            this.chkMarker.AutoSize = true;
            this.chkMarker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMarker.ForeColor = System.Drawing.Color.Black;
            this.chkMarker.Location = new System.Drawing.Point(247, 132);
            this.chkMarker.Name = "chkMarker";
            this.chkMarker.Size = new System.Drawing.Size(97, 17);
            this.chkMarker.TabIndex = 35;
            this.chkMarker.Text = "Show Markers?";
            this.chkMarker.UseVisualStyleBackColor = true;
            this.chkMarker.CheckedChanged += new System.EventHandler(this.chkMarker_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Line Width";
            // 
            // cbLine
            // 
            this.cbLine.AutoAdjustItemHeight = false;
            this.cbLine.BorderColor = System.Drawing.Color.LightGray;
            this.cbLine.ConvertEnterToTabForDialogs = false;
            this.cbLine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLine.FormattingEnabled = true;
            this.cbLine.Items.AddRange(new object[] {
            "Small",
            "Regular",
            "Large",
            "Extra Large",
            "Super Size"});
            this.cbLine.Location = new System.Drawing.Point(286, 191);
            this.cbLine.Name = "cbLine";
            this.cbLine.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cbLine.SeparatorMargin = 1;
            this.cbLine.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cbLine.SeparatorWidth = 1;
            this.cbLine.Size = new System.Drawing.Size(75, 21);
            this.cbLine.TabIndex = 37;
            this.cbLine.SelectedIndexChanged += new System.EventHandler(this.cbLine_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Series Colour";
            // 
            // colorPicker1
            // 
            this.colorPicker1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorPicker1.DropDownHeight = 1;
            this.colorPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorPicker1.Font = new System.Drawing.Font("Arial", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.colorPicker1.FormattingEnabled = true;
            this.colorPicker1.IntegralHeight = false;
            this.colorPicker1.Items.AddRange(new object[] {
            "Aliceblue",
            "Antiquewhite",
            "Aqua",
            "Aquamarine",
            "Azure",
            "Beige",
            "Bisque",
            "Black",
            "Blanchedalmond",
            "Blue",
            "Blueviolet",
            "Brown",
            "Burlywood",
            "Cadetblue",
            "Chartreuse",
            "Chocolate",
            "Coral",
            "Cornflowerblue",
            "Cornsilk",
            "Crimson",
            "Cyan",
            "Darkblue",
            "Darkcyan",
            "Darkgoldenrod",
            "Darkgray",
            "Darkgreen",
            "Darkkhaki",
            "Darkmagenta",
            "Darkolivegreen",
            "Darkorange",
            "Darkorchid",
            "Darkred",
            "Darksalmon",
            "Darkseagreen",
            "Darkslateblue",
            "Darkslategray",
            "Darkturquoise",
            "Darkviolet",
            "Deeppink",
            "Deepskyblue",
            "Dimgray",
            "Dodgerblue",
            "Firebrick",
            "Floralwhite",
            "Forestgreen",
            "Fuchsia",
            "Gainsboro",
            "Ghostwhite",
            "Gold",
            "Goldenrod",
            "Gray",
            "Green",
            "Greenyellow",
            "Honeydew",
            "Hotpink",
            "Indianred",
            "Indigo",
            "Ivory",
            "Khaki",
            "Lavender",
            "Lavenderblush",
            "Lawngreen",
            "Lemonchiffon",
            "Lightblue",
            "Lightcoral",
            "Lightcyan",
            "Lightgoldenrodyellow",
            "Lightgreen",
            "Lightgrey",
            "Lightpink",
            "Lightsalmon",
            "Lightseagreen",
            "Lightskyblue",
            "Lightslategrey",
            "Lightsteelblue",
            "Lightyellow",
            "Lime",
            "Limegreen",
            "Linen",
            "Magenta",
            "Maroon",
            "Mediumaquamarine",
            "Mediumblue",
            "Mediumorchid",
            "Mediumpurple",
            "Mediumseagreen",
            "Mediumslateblue",
            "Mediumspringgreen",
            "Mediumturquoise",
            "Mediumvioletred",
            "Midnightblue",
            "Mintcream",
            "Mistyrose",
            "Moccasin",
            "Navajowhite",
            "Navy",
            "Oldlace",
            "Olive",
            "Olivedrab",
            "Orange",
            "Orangered",
            "Orchid",
            "Palegoldenrod",
            "Palegreen",
            "Paleturquoise",
            "Palevioletred",
            "Papayawhip",
            "Peachpuff",
            "Peru",
            "Pink",
            "Plum",
            "Powderblue",
            "Purple",
            "Red",
            "Rosybrown",
            "Royalblue",
            "Saddlebrown",
            "Salmon",
            "Sandybrown",
            "Seagreen",
            "Seashell",
            "Sienna",
            "Silver",
            "Skyblue",
            "Slateblue",
            "Slategray",
            "Snow",
            "Springgreen",
            "Steelblue",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato",
            "Turquoise",
            "Violet",
            "Wheat",
            "White",
            "Whitesmoke",
            "Yellow",
            "Yellowgreen",
            "Aliceblue",
            "Antiquewhite",
            "Aqua",
            "Aquamarine",
            "Azure",
            "Beige",
            "Bisque",
            "Black",
            "Blanchedalmond",
            "Blue",
            "Blueviolet",
            "Brown",
            "Burlywood",
            "Cadetblue",
            "Chartreuse",
            "Chocolate",
            "Coral",
            "Cornflowerblue",
            "Cornsilk",
            "Crimson",
            "Cyan",
            "Darkblue",
            "Darkcyan",
            "Darkgoldenrod",
            "Darkgray",
            "Darkgreen",
            "Darkkhaki",
            "Darkmagenta",
            "Darkolivegreen",
            "Darkorange",
            "Darkorchid",
            "Darkred",
            "Darksalmon",
            "Darkseagreen",
            "Darkslateblue",
            "Darkslategray",
            "Darkturquoise",
            "Darkviolet",
            "Deeppink",
            "Deepskyblue",
            "Dimgray",
            "Dodgerblue",
            "Firebrick",
            "Floralwhite",
            "Forestgreen",
            "Fuchsia",
            "Gainsboro",
            "Ghostwhite",
            "Gold",
            "Goldenrod",
            "Gray",
            "Green",
            "Greenyellow",
            "Honeydew",
            "Hotpink",
            "Indianred",
            "Indigo",
            "Ivory",
            "Khaki",
            "Lavender",
            "Lavenderblush",
            "Lawngreen",
            "Lemonchiffon",
            "Lightblue",
            "Lightcoral",
            "Lightcyan",
            "Lightgoldenrodyellow",
            "Lightgreen",
            "Lightgrey",
            "Lightpink",
            "Lightsalmon",
            "Lightseagreen",
            "Lightskyblue",
            "Lightslategrey",
            "Lightsteelblue",
            "Lightyellow",
            "Lime",
            "Limegreen",
            "Linen",
            "Magenta",
            "Maroon",
            "Mediumaquamarine",
            "Mediumblue",
            "Mediumorchid",
            "Mediumpurple",
            "Mediumseagreen",
            "Mediumslateblue",
            "Mediumspringgreen",
            "Mediumturquoise",
            "Mediumvioletred",
            "Midnightblue",
            "Mintcream",
            "Mistyrose",
            "Moccasin",
            "Navajowhite",
            "Navy",
            "Oldlace",
            "Olive",
            "Olivedrab",
            "Orange",
            "Orangered",
            "Orchid",
            "Palegoldenrod",
            "Palegreen",
            "Paleturquoise",
            "Palevioletred",
            "Papayawhip",
            "Peachpuff",
            "Peru",
            "Pink",
            "Plum",
            "Powderblue",
            "Purple",
            "Red",
            "Rosybrown",
            "Royalblue",
            "Saddlebrown",
            "Salmon",
            "Sandybrown",
            "Seagreen",
            "Seashell",
            "Sienna",
            "Silver",
            "Skyblue",
            "Slateblue",
            "Slategray",
            "Snow",
            "Springgreen",
            "Steelblue",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato",
            "Turquoise",
            "Violet",
            "Wheat",
            "White",
            "Whitesmoke",
            "Yellow",
            "Yellowgreen",
            "Aliceblue",
            "Antiquewhite",
            "Aqua",
            "Aquamarine",
            "Azure",
            "Beige",
            "Bisque",
            "Black",
            "Blanchedalmond",
            "Blue",
            "Blueviolet",
            "Brown",
            "Burlywood",
            "Cadetblue",
            "Chartreuse",
            "Chocolate",
            "Coral",
            "Cornflowerblue",
            "Cornsilk",
            "Crimson",
            "Cyan",
            "Darkblue",
            "Darkcyan",
            "Darkgoldenrod",
            "Darkgray",
            "Darkgreen",
            "Darkkhaki",
            "Darkmagenta",
            "Darkolivegreen",
            "Darkorange",
            "Darkorchid",
            "Darkred",
            "Darksalmon",
            "Darkseagreen",
            "Darkslateblue",
            "Darkslategray",
            "Darkturquoise",
            "Darkviolet",
            "Deeppink",
            "Deepskyblue",
            "Dimgray",
            "Dodgerblue",
            "Firebrick",
            "Floralwhite",
            "Forestgreen",
            "Fuchsia",
            "Gainsboro",
            "Ghostwhite",
            "Gold",
            "Goldenrod",
            "Gray",
            "Green",
            "Greenyellow",
            "Honeydew",
            "Hotpink",
            "Indianred",
            "Indigo",
            "Ivory",
            "Khaki",
            "Lavender",
            "Lavenderblush",
            "Lawngreen",
            "Lemonchiffon",
            "Lightblue",
            "Lightcoral",
            "Lightcyan",
            "Lightgoldenrodyellow",
            "Lightgreen",
            "Lightgrey",
            "Lightpink",
            "Lightsalmon",
            "Lightseagreen",
            "Lightskyblue",
            "Lightslategrey",
            "Lightsteelblue",
            "Lightyellow",
            "Lime",
            "Limegreen",
            "Linen",
            "Magenta",
            "Maroon",
            "Mediumaquamarine",
            "Mediumblue",
            "Mediumorchid",
            "Mediumpurple",
            "Mediumseagreen",
            "Mediumslateblue",
            "Mediumspringgreen",
            "Mediumturquoise",
            "Mediumvioletred",
            "Midnightblue",
            "Mintcream",
            "Mistyrose",
            "Moccasin",
            "Navajowhite",
            "Navy",
            "Oldlace",
            "Olive",
            "Olivedrab",
            "Orange",
            "Orangered",
            "Orchid",
            "Palegoldenrod",
            "Palegreen",
            "Paleturquoise",
            "Palevioletred",
            "Papayawhip",
            "Peachpuff",
            "Peru",
            "Pink",
            "Plum",
            "Powderblue",
            "Purple",
            "Red",
            "Rosybrown",
            "Royalblue",
            "Saddlebrown",
            "Salmon",
            "Sandybrown",
            "Seagreen",
            "Seashell",
            "Sienna",
            "Silver",
            "Skyblue",
            "Slateblue",
            "Slategray",
            "Snow",
            "Springgreen",
            "Steelblue",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato",
            "Turquoise",
            "Violet",
            "Wheat",
            "White",
            "Whitesmoke",
            "Yellow",
            "Yellowgreen",
            "Aliceblue",
            "Antiquewhite",
            "Aqua",
            "Aquamarine",
            "Azure",
            "Beige",
            "Bisque",
            "Black",
            "Blanchedalmond",
            "Blue",
            "Blueviolet",
            "Brown",
            "Burlywood",
            "Cadetblue",
            "Chartreuse",
            "Chocolate",
            "Coral",
            "Cornflowerblue",
            "Cornsilk",
            "Crimson",
            "Cyan",
            "Darkblue",
            "Darkcyan",
            "Darkgoldenrod",
            "Darkgray",
            "Darkgreen",
            "Darkkhaki",
            "Darkmagenta",
            "Darkolivegreen",
            "Darkorange",
            "Darkorchid",
            "Darkred",
            "Darksalmon",
            "Darkseagreen",
            "Darkslateblue",
            "Darkslategray",
            "Darkturquoise",
            "Darkviolet",
            "Deeppink",
            "Deepskyblue",
            "Dimgray",
            "Dodgerblue",
            "Firebrick",
            "Floralwhite",
            "Forestgreen",
            "Fuchsia",
            "Gainsboro",
            "Ghostwhite",
            "Gold",
            "Goldenrod",
            "Gray",
            "Green",
            "Greenyellow",
            "Honeydew",
            "Hotpink",
            "Indianred",
            "Indigo",
            "Ivory",
            "Khaki",
            "Lavender",
            "Lavenderblush",
            "Lawngreen",
            "Lemonchiffon",
            "Lightblue",
            "Lightcoral",
            "Lightcyan",
            "Lightgoldenrodyellow",
            "Lightgreen",
            "Lightgrey",
            "Lightpink",
            "Lightsalmon",
            "Lightseagreen",
            "Lightskyblue",
            "Lightslategrey",
            "Lightsteelblue",
            "Lightyellow",
            "Lime",
            "Limegreen",
            "Linen",
            "Magenta",
            "Maroon",
            "Mediumaquamarine",
            "Mediumblue",
            "Mediumorchid",
            "Mediumpurple",
            "Mediumseagreen",
            "Mediumslateblue",
            "Mediumspringgreen",
            "Mediumturquoise",
            "Mediumvioletred",
            "Midnightblue",
            "Mintcream",
            "Mistyrose",
            "Moccasin",
            "Navajowhite",
            "Navy",
            "Oldlace",
            "Olive",
            "Olivedrab",
            "Orange",
            "Orangered",
            "Orchid",
            "Palegoldenrod",
            "Palegreen",
            "Paleturquoise",
            "Palevioletred",
            "Papayawhip",
            "Peachpuff",
            "Peru",
            "Pink",
            "Plum",
            "Powderblue",
            "Purple",
            "Red",
            "Rosybrown",
            "Royalblue",
            "Saddlebrown",
            "Salmon",
            "Sandybrown",
            "Seagreen",
            "Seashell",
            "Sienna",
            "Silver",
            "Skyblue",
            "Slateblue",
            "Slategray",
            "Snow",
            "Springgreen",
            "Steelblue",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato",
            "Turquoise",
            "Violet",
            "Wheat",
            "White",
            "Whitesmoke",
            "Yellow",
            "Yellowgreen",
            "Aliceblue",
            "Antiquewhite",
            "Aqua",
            "Aquamarine",
            "Azure",
            "Beige",
            "Bisque",
            "Black",
            "Blanchedalmond",
            "Blue",
            "Blueviolet",
            "Brown",
            "Burlywood",
            "Cadetblue",
            "Chartreuse",
            "Chocolate",
            "Coral",
            "Cornflowerblue",
            "Cornsilk",
            "Crimson",
            "Cyan",
            "Darkblue",
            "Darkcyan",
            "Darkgoldenrod",
            "Darkgray",
            "Darkgreen",
            "Darkkhaki",
            "Darkmagenta",
            "Darkolivegreen",
            "Darkorange",
            "Darkorchid",
            "Darkred",
            "Darksalmon",
            "Darkseagreen",
            "Darkslateblue",
            "Darkslategray",
            "Darkturquoise",
            "Darkviolet",
            "Deeppink",
            "Deepskyblue",
            "Dimgray",
            "Dodgerblue",
            "Firebrick",
            "Floralwhite",
            "Forestgreen",
            "Fuchsia",
            "Gainsboro",
            "Ghostwhite",
            "Gold",
            "Goldenrod",
            "Gray",
            "Green",
            "Greenyellow",
            "Honeydew",
            "Hotpink",
            "Indianred",
            "Indigo",
            "Ivory",
            "Khaki",
            "Lavender",
            "Lavenderblush",
            "Lawngreen",
            "Lemonchiffon",
            "Lightblue",
            "Lightcoral",
            "Lightcyan",
            "Lightgoldenrodyellow",
            "Lightgreen",
            "Lightgrey",
            "Lightpink",
            "Lightsalmon",
            "Lightseagreen",
            "Lightskyblue",
            "Lightslategrey",
            "Lightsteelblue",
            "Lightyellow",
            "Lime",
            "Limegreen",
            "Linen",
            "Magenta",
            "Maroon",
            "Mediumaquamarine",
            "Mediumblue",
            "Mediumorchid",
            "Mediumpurple",
            "Mediumseagreen",
            "Mediumslateblue",
            "Mediumspringgreen",
            "Mediumturquoise",
            "Mediumvioletred",
            "Midnightblue",
            "Mintcream",
            "Mistyrose",
            "Moccasin",
            "Navajowhite",
            "Navy",
            "Oldlace",
            "Olive",
            "Olivedrab",
            "Orange",
            "Orangered",
            "Orchid",
            "Palegoldenrod",
            "Palegreen",
            "Paleturquoise",
            "Palevioletred",
            "Papayawhip",
            "Peachpuff",
            "Peru",
            "Pink",
            "Plum",
            "Powderblue",
            "Purple",
            "Red",
            "Rosybrown",
            "Royalblue",
            "Saddlebrown",
            "Salmon",
            "Sandybrown",
            "Seagreen",
            "Seashell",
            "Sienna",
            "Silver",
            "Skyblue",
            "Slateblue",
            "Slategray",
            "Snow",
            "Springgreen",
            "Steelblue",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato",
            "Turquoise",
            "Violet",
            "Wheat",
            "White",
            "Whitesmoke",
            "Yellow",
            "Yellowgreen",
            "colorPicker1",
            ""});
            this.colorPicker1.Location = new System.Drawing.Point(145, 236);
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(98, 21);
            this.colorPicker1.TabIndex = 38;
            this.colorPicker1.SelectedIndexChanged += new System.EventHandler(this.colorPicker1_SelectedIndexChanged);
            // 
            // StaticSeriesCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.label8);
            this.Controls.Add(this.colorPicker1);
            this.Controls.Add(this.cbLine);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkMarker);
            this.Controls.Add(this.btnX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkRight);
            this.Controls.Add(this.chkLeft);
            this.Controls.Add(this.cbPlotType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDataValue);
            this.Controls.Add(this.btnSeriesName);
            this.Controls.Add(this.btnDataValue);
            this.Controls.Add(this.btnLabelValue);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtLabelValue);
            this.Controls.Add(this.txtSeriesName);
            this.Controls.Add(this.chkShowLabels);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbDataSeries);
            this.Controls.Add(this.label1);
            this.Name = "StaticSeriesCtl";
            this.Size = new System.Drawing.Size(366, 260);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbDataSeries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Oranikle.Studio.Controls.StyledCheckBox chkShowLabels;
        private Oranikle.Studio.Controls.CustomTextControl txtSeriesName;
        private Oranikle.Studio.Controls.CustomTextControl txtLabelValue;
        private Oranikle.Studio.Controls.StyledButton btnAdd;
        private Oranikle.Studio.Controls.StyledButton btnDel;
        private Oranikle.Studio.Controls.StyledButton btnLabelValue;
        private Oranikle.Studio.Controls.StyledButton btnDataValue;
        private Oranikle.Studio.Controls.StyledButton btnSeriesName;
        private Oranikle.Studio.Controls.CustomTextControl txtDataValue;
        private System.Windows.Forms.Label label4;
        private Oranikle.Studio.Controls.StyledComboBox cbPlotType;
        private System.Windows.Forms.RadioButton chkLeft;
        private System.Windows.Forms.RadioButton chkRight;
        private System.Windows.Forms.Label label5;
        private Oranikle.Studio.Controls.StyledButton btnUp;
        private Oranikle.Studio.Controls.StyledButton btnDown;
        private Oranikle.Studio.Controls.CustomTextControl txtX;
        private System.Windows.Forms.Label label6;
        private Oranikle.Studio.Controls.StyledButton btnX;
        private Oranikle.Studio.Controls.StyledCheckBox chkMarker;
        private System.Windows.Forms.Label label7;
        private Oranikle.Studio.Controls.StyledComboBox cbLine;
        private ColorPicker colorPicker1;
        private System.Windows.Forms.Label label8;
    }
}
