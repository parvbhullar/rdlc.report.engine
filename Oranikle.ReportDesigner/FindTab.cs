/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for FindTab.
	/// </summary>
	internal class FindTab : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabPage tabFind;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl txtFind;
		private System.Windows.Forms.RadioButton radioUp;
		private System.Windows.Forms.RadioButton radioDown;
		private System.Windows.Forms.GroupBox groupBox1;
		private Oranikle.Studio.Controls.StyledCheckBox chkCase;
		public System.Windows.Forms.TabPage tabGoTo;
		private System.Windows.Forms.Label label4;
		private Oranikle.Studio.Controls.CustomTextControl txtLine;
		private Oranikle.Studio.Controls.StyledButton btnNext;
		private RdlEditPreview rdlEdit;
		private Oranikle.Studio.Controls.StyledButton btnGoto;
		private Oranikle.Studio.Controls.StyledButton btnCancel;
		public System.Windows.Forms.TabPage tabReplace;
		private Oranikle.Studio.Controls.StyledButton btnFindNext;
		private Oranikle.Studio.Controls.StyledCheckBox chkMatchCase;
		private Oranikle.Studio.Controls.StyledButton btnReplaceAll;
		private Oranikle.Studio.Controls.StyledButton btnReplace;
		private Oranikle.Studio.Controls.CustomTextControl txtFindR;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private Oranikle.Studio.Controls.CustomTextControl txtReplace;
		private Oranikle.Studio.Controls.StyledButton bCloseReplace;
		private Oranikle.Studio.Controls.StyledButton bCloseGoto;
		public Oranikle.Studio.Controls.CtrlStyledTab tcFRG;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FindTab()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.AcceptButton = btnNext;
			this.CancelButton = btnCancel;
			txtFind.Focus();
		}

		internal FindTab(RdlEditPreview pad)
		{
			rdlEdit = pad; 
			InitializeComponent();
			
			this.AcceptButton = btnNext;
			this.CancelButton = btnCancel;
			txtFind.Focus();
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
            this.tcFRG = new Oranikle.Studio.Controls.CtrlStyledTab();
            this.tabFind = new System.Windows.Forms.TabPage();
            this.btnCancel = new Oranikle.Studio.Controls.StyledButton();
            this.btnNext = new Oranikle.Studio.Controls.StyledButton();
            this.chkCase = new Oranikle.Studio.Controls.StyledCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioUp = new System.Windows.Forms.RadioButton();
            this.radioDown = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFind = new Oranikle.Studio.Controls.CustomTextControl();
            this.tabReplace = new System.Windows.Forms.TabPage();
            this.bCloseReplace = new Oranikle.Studio.Controls.StyledButton();
            this.btnFindNext = new Oranikle.Studio.Controls.StyledButton();
            this.chkMatchCase = new Oranikle.Studio.Controls.StyledCheckBox();
            this.btnReplaceAll = new Oranikle.Studio.Controls.StyledButton();
            this.btnReplace = new Oranikle.Studio.Controls.StyledButton();
            this.txtFindR = new Oranikle.Studio.Controls.CustomTextControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReplace = new Oranikle.Studio.Controls.CustomTextControl();
            this.tabGoTo = new System.Windows.Forms.TabPage();
            this.bCloseGoto = new Oranikle.Studio.Controls.StyledButton();
            this.txtLine = new Oranikle.Studio.Controls.CustomTextControl();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGoto = new Oranikle.Studio.Controls.StyledButton();
            this.tcFRG.SuspendLayout();
            this.tabFind.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabReplace.SuspendLayout();
            this.tabGoTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcFRG
            // 
            this.tcFRG.BorderColor = System.Drawing.Color.DarkGray;
            this.tcFRG.Controls.Add(this.tabFind);
            this.tcFRG.Controls.Add(this.tabReplace);
            this.tcFRG.Controls.Add(this.tabGoTo);
            this.tcFRG.DontSlantMiddle = false;
            this.tcFRG.LeftSpacing = 0;
            this.tcFRG.Location = new System.Drawing.Point(8, 8);
            this.tcFRG.myBackColor = System.Drawing.Color.Transparent;
            this.tcFRG.Name = "tcFRG";
            this.tcFRG.SelectedIndex = 0;
            this.tcFRG.Size = new System.Drawing.Size(432, 192);
            this.tcFRG.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcFRG.TabFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcFRG.TabIndex = 0;
            this.tcFRG.TabSlant = 2;
            this.tcFRG.TabStop = false;
            this.tcFRG.TabTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tcFRG.TabTextHAlignment = System.Drawing.StringAlignment.Near;
            this.tcFRG.TabTextVAlignment = System.Drawing.StringAlignment.Center;
            this.tcFRG.TagPageSelectedColor = System.Drawing.Color.White;
            this.tcFRG.TagPageUnselectedColor = System.Drawing.Color.LightGray;
            this.tcFRG.Enter += new System.EventHandler(this.tcFRG_Enter);
            this.tcFRG.SelectedIndexChanged += new System.EventHandler(this.tcFRG_SelectedIndexChanged);
            // 
            // tabFind
            // 
            this.tabFind.BackColor = System.Drawing.Color.White;
            this.tabFind.Controls.Add(this.btnCancel);
            this.tabFind.Controls.Add(this.btnNext);
            this.tabFind.Controls.Add(this.chkCase);
            this.tabFind.Controls.Add(this.groupBox1);
            this.tabFind.Controls.Add(this.label1);
            this.tabFind.Controls.Add(this.txtFind);
            this.tabFind.Location = new System.Drawing.Point(4, 25);
            this.tabFind.Name = "tabFind";
            this.tabFind.Size = new System.Drawing.Size(424, 163);
            this.tabFind.TabIndex = 0;
            this.tabFind.Tag = "find";
            this.tabFind.Text = "Find";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnCancel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnCancel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(344, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OverriddenSize = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnNext.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnNext.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnNext.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnNext.Enabled = false;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Arial", 9F);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(344, 16);
            this.btnNext.Name = "btnNext";
            this.btnNext.OverriddenSize = null;
            this.btnNext.Size = new System.Drawing.Size(75, 21);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Find Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // chkCase
            // 
            this.chkCase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCase.ForeColor = System.Drawing.Color.Black;
            this.chkCase.Location = new System.Drawing.Point(208, 72);
            this.chkCase.Name = "chkCase";
            this.chkCase.Size = new System.Drawing.Size(88, 24);
            this.chkCase.TabIndex = 2;
            this.chkCase.Text = "Match Case";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioUp);
            this.groupBox1.Controls.Add(this.radioDown);
            this.groupBox1.Location = new System.Drawing.Point(16, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Direction";
            // 
            // radioUp
            // 
            this.radioUp.Location = new System.Drawing.Point(16, 24);
            this.radioUp.Name = "radioUp";
            this.radioUp.Size = new System.Drawing.Size(56, 24);
            this.radioUp.TabIndex = 0;
            this.radioUp.Text = "Up";
            this.radioUp.CheckedChanged += new System.EventHandler(this.radioUp_CheckedChanged);
            // 
            // radioDown
            // 
            this.radioDown.Location = new System.Drawing.Point(104, 24);
            this.radioDown.Name = "radioDown";
            this.radioDown.Size = new System.Drawing.Size(56, 24);
            this.radioDown.TabIndex = 1;
            this.radioDown.Text = "Down";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find";
            // 
            // txtFind
            // 
            this.txtFind.AddX = 0;
            this.txtFind.AddY = 0;
            this.txtFind.AllowSpace = false;
            this.txtFind.BorderColor = System.Drawing.Color.LightGray;
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFind.ChangeVisibility = false;
            this.txtFind.ChildControl = null;
            this.txtFind.ConvertEnterToTab = true;
            this.txtFind.ConvertEnterToTabForDialogs = false;
            this.txtFind.Decimals = 0;
            this.txtFind.DisplayList = new object[0];
            this.txtFind.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtFind.Location = new System.Drawing.Point(96, 16);
            this.txtFind.Name = "txtFind";
            this.txtFind.OnDropDownCloseFocus = true;
            this.txtFind.SelectType = 0;
            this.txtFind.Size = new System.Drawing.Size(216, 20);
            this.txtFind.TabIndex = 0;
            this.txtFind.UseValueForChildsVisibilty = false;
            this.txtFind.Value = true;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // tabReplace
            // 
            this.tabReplace.BackColor = System.Drawing.Color.White;
            this.tabReplace.Controls.Add(this.bCloseReplace);
            this.tabReplace.Controls.Add(this.btnFindNext);
            this.tabReplace.Controls.Add(this.chkMatchCase);
            this.tabReplace.Controls.Add(this.btnReplaceAll);
            this.tabReplace.Controls.Add(this.btnReplace);
            this.tabReplace.Controls.Add(this.txtFindR);
            this.tabReplace.Controls.Add(this.label3);
            this.tabReplace.Controls.Add(this.label2);
            this.tabReplace.Controls.Add(this.txtReplace);
            this.tabReplace.Location = new System.Drawing.Point(4, 25);
            this.tabReplace.Name = "tabReplace";
            this.tabReplace.Size = new System.Drawing.Size(424, 163);
            this.tabReplace.TabIndex = 1;
            this.tabReplace.Tag = "replace";
            this.tabReplace.Text = "Replace";
            // 
            // bCloseReplace
            // 
            this.bCloseReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCloseReplace.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCloseReplace.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCloseReplace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCloseReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCloseReplace.Font = new System.Drawing.Font("Arial", 9F);
            this.bCloseReplace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCloseReplace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCloseReplace.Location = new System.Drawing.Point(344, 128);
            this.bCloseReplace.Name = "bCloseReplace";
            this.bCloseReplace.OverriddenSize = null;
            this.bCloseReplace.Size = new System.Drawing.Size(75, 21);
            this.bCloseReplace.TabIndex = 6;
            this.bCloseReplace.Text = "Close";
            this.bCloseReplace.UseVisualStyleBackColor = true;
            this.bCloseReplace.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFindNext
            // 
            this.btnFindNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnFindNext.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnFindNext.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnFindNext.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnFindNext.Enabled = false;
            this.btnFindNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindNext.Font = new System.Drawing.Font("Arial", 9F);
            this.btnFindNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnFindNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFindNext.Location = new System.Drawing.Point(344, 16);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.OverriddenSize = null;
            this.btnFindNext.Size = new System.Drawing.Size(75, 21);
            this.btnFindNext.TabIndex = 3;
            this.btnFindNext.Text = "FindNext";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMatchCase.ForeColor = System.Drawing.Color.Black;
            this.chkMatchCase.Location = new System.Drawing.Point(8, 88);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(104, 24);
            this.chkMatchCase.TabIndex = 2;
            this.chkMatchCase.Text = "Match Case";
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnReplaceAll.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnReplaceAll.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnReplaceAll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnReplaceAll.Enabled = false;
            this.btnReplaceAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplaceAll.Font = new System.Drawing.Font("Arial", 9F);
            this.btnReplaceAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnReplaceAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReplaceAll.Location = new System.Drawing.Point(344, 80);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.OverriddenSize = null;
            this.btnReplaceAll.Size = new System.Drawing.Size(75, 21);
            this.btnReplaceAll.TabIndex = 5;
            this.btnReplaceAll.Text = "ReplaceAll";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnReplace.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnReplace.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnReplace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnReplace.Enabled = false;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplace.Font = new System.Drawing.Font("Arial", 9F);
            this.btnReplace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnReplace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReplace.Location = new System.Drawing.Point(344, 48);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.OverriddenSize = null;
            this.btnReplace.Size = new System.Drawing.Size(75, 21);
            this.btnReplace.TabIndex = 4;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // txtFindR
            // 
            this.txtFindR.AddX = 0;
            this.txtFindR.AddY = 0;
            this.txtFindR.AllowSpace = false;
            this.txtFindR.BorderColor = System.Drawing.Color.LightGray;
            this.txtFindR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFindR.ChangeVisibility = false;
            this.txtFindR.ChildControl = null;
            this.txtFindR.ConvertEnterToTab = true;
            this.txtFindR.ConvertEnterToTabForDialogs = false;
            this.txtFindR.Decimals = 0;
            this.txtFindR.DisplayList = new object[0];
            this.txtFindR.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtFindR.Location = new System.Drawing.Point(96, 16);
            this.txtFindR.Name = "txtFindR";
            this.txtFindR.OnDropDownCloseFocus = true;
            this.txtFindR.SelectType = 0;
            this.txtFindR.Size = new System.Drawing.Size(224, 20);
            this.txtFindR.TabIndex = 0;
            this.txtFindR.UseValueForChildsVisibilty = false;
            this.txtFindR.Value = true;
            this.txtFindR.TextChanged += new System.EventHandler(this.txtFindR_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Find";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Replace With";
            // 
            // txtReplace
            // 
            this.txtReplace.AddX = 0;
            this.txtReplace.AddY = 0;
            this.txtReplace.AllowSpace = false;
            this.txtReplace.BorderColor = System.Drawing.Color.LightGray;
            this.txtReplace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReplace.ChangeVisibility = false;
            this.txtReplace.ChildControl = null;
            this.txtReplace.ConvertEnterToTab = true;
            this.txtReplace.ConvertEnterToTabForDialogs = false;
            this.txtReplace.Decimals = 0;
            this.txtReplace.DisplayList = new object[0];
            this.txtReplace.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtReplace.Location = new System.Drawing.Point(96, 56);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.OnDropDownCloseFocus = true;
            this.txtReplace.SelectType = 0;
            this.txtReplace.Size = new System.Drawing.Size(224, 20);
            this.txtReplace.TabIndex = 1;
            this.txtReplace.UseValueForChildsVisibilty = false;
            this.txtReplace.Value = true;
            // 
            // tabGoTo
            // 
            this.tabGoTo.BackColor = System.Drawing.Color.White;
            this.tabGoTo.Controls.Add(this.bCloseGoto);
            this.tabGoTo.Controls.Add(this.txtLine);
            this.tabGoTo.Controls.Add(this.label4);
            this.tabGoTo.Controls.Add(this.btnGoto);
            this.tabGoTo.Location = new System.Drawing.Point(4, 25);
            this.tabGoTo.Name = "tabGoTo";
            this.tabGoTo.Size = new System.Drawing.Size(424, 163);
            this.tabGoTo.TabIndex = 2;
            this.tabGoTo.Tag = "goto";
            this.tabGoTo.Text = "GoTo";
            // 
            // bCloseGoto
            // 
            this.bCloseGoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCloseGoto.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCloseGoto.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCloseGoto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCloseGoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCloseGoto.Font = new System.Drawing.Font("Arial", 9F);
            this.bCloseGoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCloseGoto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCloseGoto.Location = new System.Drawing.Point(344, 128);
            this.bCloseGoto.Name = "bCloseGoto";
            this.bCloseGoto.OverriddenSize = null;
            this.bCloseGoto.Size = new System.Drawing.Size(75, 21);
            this.bCloseGoto.TabIndex = 2;
            this.bCloseGoto.Text = "Close";
            this.bCloseGoto.UseVisualStyleBackColor = true;
            this.bCloseGoto.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtLine
            // 
            this.txtLine.AddX = 0;
            this.txtLine.AddY = 0;
            this.txtLine.AllowSpace = false;
            this.txtLine.BorderColor = System.Drawing.Color.LightGray;
            this.txtLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLine.ChangeVisibility = false;
            this.txtLine.ChildControl = null;
            this.txtLine.ConvertEnterToTab = true;
            this.txtLine.ConvertEnterToTabForDialogs = false;
            this.txtLine.Decimals = 0;
            this.txtLine.DisplayList = new object[0];
            this.txtLine.HitText = Oranikle.Studio.Controls.HitText.String;
            this.txtLine.Location = new System.Drawing.Point(96, 16);
            this.txtLine.Name = "txtLine";
            this.txtLine.OnDropDownCloseFocus = true;
            this.txtLine.SelectType = 0;
            this.txtLine.Size = new System.Drawing.Size(100, 20);
            this.txtLine.TabIndex = 0;
            this.txtLine.UseValueForChildsVisibilty = false;
            this.txtLine.Value = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Line Number";
            // 
            // btnGoto
            // 
            this.btnGoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnGoto.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnGoto.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnGoto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnGoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoto.Font = new System.Drawing.Font("Arial", 9F);
            this.btnGoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnGoto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoto.Location = new System.Drawing.Point(344, 16);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.OverriddenSize = null;
            this.btnGoto.Size = new System.Drawing.Size(75, 21);
            this.btnGoto.TabIndex = 1;
            this.btnGoto.Text = "GoTo";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // FindTab
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(445, 206);
            this.Controls.Add(this.tcFRG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindTab";
            this.Text = "Find";
            this.TopMost = true;
            this.tcFRG.ResumeLayout(false);
            this.tabFind.ResumeLayout(false);
            this.tabFind.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabReplace.ResumeLayout(false);
            this.tabReplace.PerformLayout();
            this.tabGoTo.ResumeLayout(false);
            this.tabGoTo.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void radioUp_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			rdlEdit.FindNext(this, txtFind.Text, chkCase.Checked);
		}

		private void txtFind_TextChanged(object sender, System.EventArgs e)
		{
			if (txtFind.Text == "")
				btnNext.Enabled = false;
			else
				btnNext.Enabled = true;
		}

		private void btnFindNext_Click(object sender, System.EventArgs e)
		{
			rdlEdit.FindNext(this, txtFindR.Text, chkCase.Checked);		
			txtFind.Focus();
		}

		private void btnReplace_Click(object sender, System.EventArgs e)
		{
			rdlEdit.FindNext(this, txtFindR.Text, chkCase.Checked);
			rdlEdit.ReplaceNext(this, txtFindR.Text, txtReplace.Text, chkCase.Checked);
			txtFindR.Focus();
		}

		private void txtFindR_TextChanged(object sender, System.EventArgs e)
		{
			bool bEnable = (txtFindR.Text == "") ? false : true;
			btnFindNext.Enabled = bEnable;
			btnReplace.Enabled = bEnable;
			btnReplaceAll.Enabled = bEnable;

		}

		private void btnReplaceAll_Click(object sender, System.EventArgs e)
		{
			
			rdlEdit.ReplaceAll(this, txtFindR.Text, txtReplace.Text, chkCase.Checked);
			txtFindR.Focus();
		}

		private void btnGoto_Click(object sender, System.EventArgs e)
		{
			try
			{
				try
				{
					int nLine = Int32.Parse(txtLine.Text);
					rdlEdit.Goto(this, nLine);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, "Invalid Line Number");
				}

				txtLine.Focus();
				
			}
			catch(Exception er)
			{
				er.ToString();
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void tcFRG_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TabControl tc = (TabControl) sender;
			string tag = (string) tc.TabPages[tc.SelectedIndex].Tag;
			switch (tag)
			{
				case "find":
					this.AcceptButton = btnNext;
					this.CancelButton = btnCancel;
					txtFind.Focus();
					break;
				case "replace":
					this.AcceptButton = this.btnFindNext;
					this.CancelButton = this.bCloseReplace;
					txtFindR.Focus();
					break;
				case "goto":
					this.AcceptButton = btnGoto;
					this.CancelButton = this.bCloseGoto;
					txtLine.Focus();
					break;
			}
		}

		private void tcFRG_Enter(object sender, System.EventArgs e)
		{
			tcFRG_SelectedIndexChanged(this.tcFRG, new EventArgs());
		}
	}
}
