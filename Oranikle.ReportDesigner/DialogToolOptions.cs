/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.IO;

namespace Oranikle.ReportDesigner
{
	/// <summary>
	/// Summary description for DialogAbout.
	/// </summary>
	public class DialogToolOptions : System.Windows.Forms.Form
	{
		DesignerForm _RdlDesigner;
		bool bDesktop=false;
		bool bToolbar=false;
        bool bMaps = false;

		// Desktop server configuration
		XmlDocument _DesktopDocument;
		XmlNode _DesktopPort;
		XmlNode _DesktopDirectory;
		XmlNode _DesktopLocal;

		private Oranikle.Studio.Controls.StyledButton bOK;
		private Oranikle.Studio.Controls.StyledButton bCancel;
		private Oranikle.Studio.Controls.CtrlStyledTab tabControl1;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.TabPage tpToolbar;
		private System.Windows.Forms.Label label1;
		private Oranikle.Studio.Controls.CustomTextControl tbRecentFilesMax;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private Oranikle.Studio.Controls.CustomTextControl tbHelpUrl;
		private System.Windows.Forms.ListBox lbOperation;
		private System.Windows.Forms.ListBox lbToolbar;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private Oranikle.Studio.Controls.StyledButton bCopyItem;
		private Oranikle.Studio.Controls.StyledButton bUp;
		private Oranikle.Studio.Controls.StyledButton bDown;
		private Oranikle.Studio.Controls.StyledButton bReset;
		private Oranikle.Studio.Controls.StyledButton bRemove;
		private Oranikle.Studio.Controls.StyledButton bApply;
		private System.Windows.Forms.TabPage tpDesktop;
		private System.Windows.Forms.Label label6;
		private Oranikle.Studio.Controls.CustomTextControl tbPort;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private Oranikle.Studio.Controls.CustomTextControl tbDirectory;
		private Oranikle.Studio.Controls.StyledCheckBox ckLocal;
        private Oranikle.Studio.Controls.StyledButton bBrowse;
        private Oranikle.Studio.Controls.StyledCheckBox cbEditLines;
        private Oranikle.Studio.Controls.StyledCheckBox cbOutline;
        private Oranikle.Studio.Controls.StyledCheckBox cbTabInterface;
        private GroupBox gbPropLoc;
        private RadioButton rbPBBottom;
        private RadioButton rbPBTop;
        private RadioButton rbPBLeft;
        private RadioButton rbPBRight;
        private Oranikle.Studio.Controls.StyledCheckBox chkPBAutoHide;
        private TabPage tabPage1;
        private Oranikle.Studio.Controls.StyledButton bRemoveMap;
        private Oranikle.Studio.Controls.StyledButton bAddMap;
        private ListBox lbMaps;
        private Label label10;
        private Oranikle.Studio.Controls.StyledCheckBox cbShowReportWaitDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DialogToolOptions(DesignerForm rdl)
		{
			_RdlDesigner = rdl;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Init();
			return;
		}

		private void Init()
		{
			this.tbRecentFilesMax.Text = _RdlDesigner.RecentFilesMax.ToString();
			this.tbHelpUrl.Text = _RdlDesigner.HelpUrl;

			// init the toolbar

			// list of items in current toolbar
			foreach (string ti in _RdlDesigner.Toolbar)
			{
				this.lbToolbar.Items.Add(ti);
			}

            this.cbEditLines.Checked = _RdlDesigner.ShowEditLines;
            this.cbOutline.Checked = _RdlDesigner.ShowReportItemOutline;
            this.cbTabInterface.Checked = _RdlDesigner.ShowTabbedInterface;
            chkPBAutoHide.Checked = _RdlDesigner.PropertiesAutoHide;
            this.cbShowReportWaitDialog.Checked = _RdlDesigner.ShowPreviewWaitDialog;

            switch (_RdlDesigner.PropertiesLocation)
            {
                case DockStyle.Top:
                    this.rbPBTop.Checked = true;
                    break;
                case DockStyle.Bottom:
                    this.rbPBBottom.Checked = true;
                    break;
                case DockStyle.Right:
                    this.rbPBRight.Checked = true;
                    break;
                case DockStyle.Left:
                default:
                    this.rbPBLeft.Checked = true;
                    break;
            }


			InitOperations();

			InitDesktop();

            InitMaps();
			bDesktop = bToolbar = bMaps = false;			// start with no changes
		}

		private void InitDesktop()
		{
			string optFileName = AppDomain.CurrentDomain.BaseDirectory + "config.xml";
			
			try
			{
				XmlDocument xDoc = _DesktopDocument = new XmlDocument();
				xDoc.PreserveWhitespace = true;
				xDoc.Load(optFileName);
				XmlNode xNode;
				xNode = xDoc.SelectSingleNode("//config");

				// Loop thru all the child nodes
				foreach(XmlNode xNodeLoop in xNode.ChildNodes)
				{
					if (xNodeLoop.NodeType != XmlNodeType.Element)
						continue;
					switch (xNodeLoop.Name.ToLower())
					{
						case "port":
							this.tbPort.Text = xNodeLoop.InnerText;
							_DesktopPort = xNodeLoop;
							break;
						case "localhostonly":
							string tf = xNodeLoop.InnerText.ToLower();
							this.ckLocal.Checked = !(tf == "false");
							_DesktopLocal = xNodeLoop;
							break;
						case "serverroot":
							this.tbDirectory.Text = xNodeLoop.InnerText;
							_DesktopDirectory = xNodeLoop;
							break;
						case "cachedirectory":
							// wd = xNodeLoop.InnerText;
							break;
						case "tracelevel":
							break;
						case "maxreadcache":
							break;
						case "maxreadcacheentrysize":
							break;
						case "mimetypes":
							break;
						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{		// Didn't sucessfully get the startup state: use defaults
				MessageBox.Show(string.Format("Error processing Desktop Configuration; using defaults.\n{0}", ex.Message), "Options");
				this.tbPort.Text = "8080";
				this.ckLocal.Checked = true;
				this.tbDirectory.Text = "Examples";
			}

		}
        
        private void InitMaps()
        {
            lbMaps.Items.Clear();
            lbMaps.Items.AddRange(RdlDesigner.MapSubtypes);
        }

		private void InitOperations()
		{
			// list of operations; 
			lbOperation.Items.Clear();

			List<string> dups = _RdlDesigner.ToolbarAllowDups;
			foreach (string ti in _RdlDesigner.ToolbarOperations)
			{
				// if item is allowed to be duplicated or if
				//   item has not already been used we add to operation list
				if (dups.Contains(ti) || !lbToolbar.Items.Contains(ti))
					this.lbOperation.Items.Add(ti);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogToolOptions));
            this.bOK = new Oranikle.Studio.Controls.StyledButton();
            this.bCancel = new Oranikle.Studio.Controls.StyledButton();
            this.tabControl1 = new Oranikle.Studio.Controls.CtrlStyledTab();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.cbShowReportWaitDialog = new Oranikle.Studio.Controls.StyledCheckBox();
            this.gbPropLoc = new System.Windows.Forms.GroupBox();
            this.chkPBAutoHide = new Oranikle.Studio.Controls.StyledCheckBox();
            this.rbPBBottom = new System.Windows.Forms.RadioButton();
            this.rbPBTop = new System.Windows.Forms.RadioButton();
            this.rbPBLeft = new System.Windows.Forms.RadioButton();
            this.rbPBRight = new System.Windows.Forms.RadioButton();
            this.cbTabInterface = new Oranikle.Studio.Controls.StyledCheckBox();
            this.cbOutline = new Oranikle.Studio.Controls.StyledCheckBox();
            this.cbEditLines = new Oranikle.Studio.Controls.StyledCheckBox();
            this.tbHelpUrl = new Oranikle.Studio.Controls.CustomTextControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRecentFilesMax = new Oranikle.Studio.Controls.CustomTextControl();
            this.label1 = new System.Windows.Forms.Label();
            this.tpToolbar = new System.Windows.Forms.TabPage();
            this.bRemove = new Oranikle.Studio.Controls.StyledButton();
            this.bReset = new Oranikle.Studio.Controls.StyledButton();
            this.bDown = new Oranikle.Studio.Controls.StyledButton();
            this.bUp = new Oranikle.Studio.Controls.StyledButton();
            this.bCopyItem = new Oranikle.Studio.Controls.StyledButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbToolbar = new System.Windows.Forms.ListBox();
            this.lbOperation = new System.Windows.Forms.ListBox();
            this.tpDesktop = new System.Windows.Forms.TabPage();
            this.bBrowse = new Oranikle.Studio.Controls.StyledButton();
            this.tbDirectory = new Oranikle.Studio.Controls.CustomTextControl();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ckLocal = new Oranikle.Studio.Controls.StyledCheckBox();
            this.tbPort = new Oranikle.Studio.Controls.CustomTextControl();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.bRemoveMap = new Oranikle.Studio.Controls.StyledButton();
            this.bAddMap = new Oranikle.Studio.Controls.StyledButton();
            this.lbMaps = new System.Windows.Forms.ListBox();
            this.bApply = new Oranikle.Studio.Controls.StyledButton();
            this.tabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.gbPropLoc.SuspendLayout();
            this.tpToolbar.SuspendLayout();
            this.tpDesktop.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.bOK.Location = new System.Drawing.Point(210, 275);
            this.bOK.Name = "bOK";
            this.bOK.OverriddenSize = null;
            this.bOK.Size = new System.Drawing.Size(75, 21);
            this.bOK.TabIndex = 1;
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
            this.bCancel.CausesValidation = false;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.bCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(298, 275);
            this.bCancel.Name = "bCancel";
            this.bCancel.OverriddenSize = null;
            this.bCancel.Size = new System.Drawing.Size(75, 21);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.BorderColor = System.Drawing.Color.DarkGray;
            this.tabControl1.Controls.Add(this.tpGeneral);
            this.tabControl1.Controls.Add(this.tpToolbar);
            this.tabControl1.Controls.Add(this.tpDesktop);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.DontSlantMiddle = false;
            this.tabControl1.LeftSpacing = 0;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.myBackColor = System.Drawing.Color.Transparent;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(466, 269);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabSlant = 2;
            this.tabControl1.TabStop = false;
            this.tabControl1.TabTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabControl1.TabTextHAlignment = System.Drawing.StringAlignment.Near;
            this.tabControl1.TabTextVAlignment = System.Drawing.StringAlignment.Center;
            this.tabControl1.TagPageSelectedColor = System.Drawing.Color.White;
            this.tabControl1.TagPageUnselectedColor = System.Drawing.Color.LightGray;
            // 
            // tpGeneral
            // 
            this.tpGeneral.BackColor = System.Drawing.Color.White;
            this.tpGeneral.Controls.Add(this.cbShowReportWaitDialog);
            this.tpGeneral.Controls.Add(this.gbPropLoc);
            this.tpGeneral.Controls.Add(this.cbTabInterface);
            this.tpGeneral.Controls.Add(this.cbOutline);
            this.tpGeneral.Controls.Add(this.cbEditLines);
            this.tpGeneral.Controls.Add(this.tbHelpUrl);
            this.tpGeneral.Controls.Add(this.label3);
            this.tpGeneral.Controls.Add(this.label2);
            this.tpGeneral.Controls.Add(this.tbRecentFilesMax);
            this.tpGeneral.Controls.Add(this.label1);
            this.tpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(458, 240);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Tag = "general";
            this.tpGeneral.Text = "General";
            // 
            // cbShowReportWaitDialog
            // 
            this.cbShowReportWaitDialog.AutoSize = true;
            this.cbShowReportWaitDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbShowReportWaitDialog.ForeColor = System.Drawing.Color.Black;
            this.cbShowReportWaitDialog.Location = new System.Drawing.Point(14, 155);
            this.cbShowReportWaitDialog.Name = "cbShowReportWaitDialog";
            this.cbShowReportWaitDialog.Size = new System.Drawing.Size(226, 17);
            this.cbShowReportWaitDialog.TabIndex = 8;
            this.cbShowReportWaitDialog.Text = "Show Rendering Report Dialog on Preview";
            this.cbShowReportWaitDialog.UseVisualStyleBackColor = true;
            // 
            // gbPropLoc
            // 
            this.gbPropLoc.Controls.Add(this.chkPBAutoHide);
            this.gbPropLoc.Controls.Add(this.rbPBBottom);
            this.gbPropLoc.Controls.Add(this.rbPBTop);
            this.gbPropLoc.Controls.Add(this.rbPBLeft);
            this.gbPropLoc.Controls.Add(this.rbPBRight);
            this.gbPropLoc.Location = new System.Drawing.Point(14, 174);
            this.gbPropLoc.Name = "gbPropLoc";
            this.gbPropLoc.Size = new System.Drawing.Size(401, 61);
            this.gbPropLoc.TabIndex = 9;
            this.gbPropLoc.TabStop = false;
            this.gbPropLoc.Text = "Properties Window";
            // 
            // chkPBAutoHide
            // 
            this.chkPBAutoHide.AutoSize = true;
            this.chkPBAutoHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPBAutoHide.ForeColor = System.Drawing.Color.Black;
            this.chkPBAutoHide.Location = new System.Drawing.Point(13, 42);
            this.chkPBAutoHide.Name = "chkPBAutoHide";
            this.chkPBAutoHide.Size = new System.Drawing.Size(70, 17);
            this.chkPBAutoHide.TabIndex = 4;
            this.chkPBAutoHide.Text = "Auto Hide";
            this.chkPBAutoHide.UseVisualStyleBackColor = true;
            // 
            // rbPBBottom
            // 
            this.rbPBBottom.AutoSize = true;
            this.rbPBBottom.Location = new System.Drawing.Point(301, 19);
            this.rbPBBottom.Name = "rbPBBottom";
            this.rbPBBottom.Size = new System.Drawing.Size(58, 17);
            this.rbPBBottom.TabIndex = 3;
            this.rbPBBottom.TabStop = true;
            this.rbPBBottom.Text = "Bottom";
            this.rbPBBottom.UseVisualStyleBackColor = true;
            // 
            // rbPBTop
            // 
            this.rbPBTop.AutoSize = true;
            this.rbPBTop.Location = new System.Drawing.Point(205, 19);
            this.rbPBTop.Name = "rbPBTop";
            this.rbPBTop.Size = new System.Drawing.Size(44, 17);
            this.rbPBTop.TabIndex = 2;
            this.rbPBTop.TabStop = true;
            this.rbPBTop.Text = "Top";
            this.rbPBTop.UseVisualStyleBackColor = true;
            // 
            // rbPBLeft
            // 
            this.rbPBLeft.AutoSize = true;
            this.rbPBLeft.Location = new System.Drawing.Point(109, 19);
            this.rbPBLeft.Name = "rbPBLeft";
            this.rbPBLeft.Size = new System.Drawing.Size(43, 17);
            this.rbPBLeft.TabIndex = 1;
            this.rbPBLeft.TabStop = true;
            this.rbPBLeft.Text = "Left";
            this.rbPBLeft.UseVisualStyleBackColor = true;
            // 
            // rbPBRight
            // 
            this.rbPBRight.AutoSize = true;
            this.rbPBRight.Location = new System.Drawing.Point(13, 19);
            this.rbPBRight.Name = "rbPBRight";
            this.rbPBRight.Size = new System.Drawing.Size(50, 17);
            this.rbPBRight.TabIndex = 0;
            this.rbPBRight.TabStop = true;
            this.rbPBRight.Text = "Right";
            this.rbPBRight.UseVisualStyleBackColor = true;
            // 
            // cbTabInterface
            // 
            this.cbTabInterface.AutoSize = true;
            this.cbTabInterface.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTabInterface.ForeColor = System.Drawing.Color.Black;
            this.cbTabInterface.Location = new System.Drawing.Point(14, 132);
            this.cbTabInterface.Name = "cbTabInterface";
            this.cbTabInterface.Size = new System.Drawing.Size(130, 17);
            this.cbTabInterface.TabIndex = 7;
            this.cbTabInterface.Text = "Show tabbed interface";
            this.cbTabInterface.UseVisualStyleBackColor = true;
            this.cbTabInterface.CheckedChanged += new System.EventHandler(this.cbTabInterface_CheckedChanged);
            // 
            // cbOutline
            // 
            this.cbOutline.AutoSize = true;
            this.cbOutline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOutline.ForeColor = System.Drawing.Color.Black;
            this.cbOutline.Location = new System.Drawing.Point(14, 109);
            this.cbOutline.Name = "cbOutline";
            this.cbOutline.Size = new System.Drawing.Size(169, 17);
            this.cbOutline.TabIndex = 6;
            this.cbOutline.Text = "Outline report items in Designer";
            this.cbOutline.UseVisualStyleBackColor = true;
            // 
            // cbEditLines
            // 
            this.cbEditLines.AutoSize = true;
            this.cbEditLines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEditLines.ForeColor = System.Drawing.Color.Black;
            this.cbEditLines.Location = new System.Drawing.Point(14, 86);
            this.cbEditLines.Name = "cbEditLines";
            this.cbEditLines.Size = new System.Drawing.Size(172, 17);
            this.cbEditLines.TabIndex = 5;
            this.cbEditLines.Text = "Show line numbers in RDL Text";
            this.cbEditLines.UseVisualStyleBackColor = true;
            // 
            // tbHelpUrl
            // 
            this.tbHelpUrl.AddX = 0;
            this.tbHelpUrl.AddY = 0;
            this.tbHelpUrl.AllowSpace = false;
            this.tbHelpUrl.BorderColor = System.Drawing.Color.LightGray;
            this.tbHelpUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHelpUrl.ChangeVisibility = false;
            this.tbHelpUrl.ChildControl = null;
            this.tbHelpUrl.ConvertEnterToTab = true;
            this.tbHelpUrl.ConvertEnterToTabForDialogs = false;
            this.tbHelpUrl.Decimals = 0;
            this.tbHelpUrl.DisplayList = new object[0];
            this.tbHelpUrl.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbHelpUrl.Location = new System.Drawing.Point(27, 60);
            this.tbHelpUrl.Name = "tbHelpUrl";
            this.tbHelpUrl.OnDropDownCloseFocus = true;
            this.tbHelpUrl.SelectType = 0;
            this.tbHelpUrl.Size = new System.Drawing.Size(404, 20);
            this.tbHelpUrl.TabIndex = 4;
            this.tbHelpUrl.Text = "tbHelpUrl";
            this.tbHelpUrl.UseValueForChildsVisibilty = false;
            this.tbHelpUrl.Value = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(404, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Help System URL  (Empty string means use default help url.)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(100, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "items in most recently used lists.";
            // 
            // tbRecentFilesMax
            // 
            this.tbRecentFilesMax.AddX = 0;
            this.tbRecentFilesMax.AddY = 0;
            this.tbRecentFilesMax.AllowSpace = false;
            this.tbRecentFilesMax.BorderColor = System.Drawing.Color.LightGray;
            this.tbRecentFilesMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRecentFilesMax.ChangeVisibility = false;
            this.tbRecentFilesMax.ChildControl = null;
            this.tbRecentFilesMax.ConvertEnterToTab = true;
            this.tbRecentFilesMax.ConvertEnterToTabForDialogs = false;
            this.tbRecentFilesMax.Decimals = 0;
            this.tbRecentFilesMax.DisplayList = new object[0];
            this.tbRecentFilesMax.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbRecentFilesMax.Location = new System.Drawing.Point(58, 11);
            this.tbRecentFilesMax.Name = "tbRecentFilesMax";
            this.tbRecentFilesMax.OnDropDownCloseFocus = true;
            this.tbRecentFilesMax.SelectType = 0;
            this.tbRecentFilesMax.Size = new System.Drawing.Size(31, 20);
            this.tbRecentFilesMax.TabIndex = 1;
            this.tbRecentFilesMax.UseValueForChildsVisibilty = false;
            this.tbRecentFilesMax.Value = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display";
            // 
            // tpToolbar
            // 
            this.tpToolbar.BackColor = System.Drawing.Color.White;
            this.tpToolbar.Controls.Add(this.bRemove);
            this.tpToolbar.Controls.Add(this.bReset);
            this.tpToolbar.Controls.Add(this.bDown);
            this.tpToolbar.Controls.Add(this.bUp);
            this.tpToolbar.Controls.Add(this.bCopyItem);
            this.tpToolbar.Controls.Add(this.label5);
            this.tpToolbar.Controls.Add(this.label4);
            this.tpToolbar.Controls.Add(this.lbToolbar);
            this.tpToolbar.Controls.Add(this.lbOperation);
            this.tpToolbar.Location = new System.Drawing.Point(4, 25);
            this.tpToolbar.Name = "tpToolbar";
            this.tpToolbar.Size = new System.Drawing.Size(458, 240);
            this.tpToolbar.TabIndex = 1;
            this.tpToolbar.Tag = "toolbar";
            this.tpToolbar.Text = "Toolbar";
            // 
            // bRemove
            // 
            this.bRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bRemove.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bRemove.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bRemove.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRemove.Font = new System.Drawing.Font("Arial", 9F);
            this.bRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRemove.Location = new System.Drawing.Point(179, 74);
            this.bRemove.Name = "bRemove";
            this.bRemove.OverriddenSize = null;
            this.bRemove.Size = new System.Drawing.Size(23, 21);
            this.bRemove.TabIndex = 2;
            this.bRemove.Text = "<";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // bReset
            // 
            this.bReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bReset.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bReset.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bReset.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bReset.Font = new System.Drawing.Font("Arial", 9F);
            this.bReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bReset.Location = new System.Drawing.Point(374, 104);
            this.bReset.Name = "bReset";
            this.bReset.OverriddenSize = null;
            this.bReset.Size = new System.Drawing.Size(53, 21);
            this.bReset.TabIndex = 6;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bDown
            // 
            this.bDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bDown.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bDown.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDown.Font = new System.Drawing.Font("Arial", 9F);
            this.bDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDown.Location = new System.Drawing.Point(374, 65);
            this.bDown.Name = "bDown";
            this.bDown.OverriddenSize = null;
            this.bDown.Size = new System.Drawing.Size(53, 21);
            this.bDown.TabIndex = 5;
            this.bDown.Text = "Down";
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // bUp
            // 
            this.bUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bUp.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bUp.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUp.Font = new System.Drawing.Font("Arial", 9F);
            this.bUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bUp.Location = new System.Drawing.Point(374, 35);
            this.bUp.Name = "bUp";
            this.bUp.OverriddenSize = null;
            this.bUp.Size = new System.Drawing.Size(53, 21);
            this.bUp.TabIndex = 4;
            this.bUp.Text = "Up";
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bCopyItem
            // 
            this.bCopyItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCopyItem.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCopyItem.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCopyItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCopyItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCopyItem.Font = new System.Drawing.Font("Arial", 9F);
            this.bCopyItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCopyItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCopyItem.Location = new System.Drawing.Point(179, 40);
            this.bCopyItem.Name = "bCopyItem";
            this.bCopyItem.OverriddenSize = null;
            this.bCopyItem.Size = new System.Drawing.Size(23, 21);
            this.bCopyItem.TabIndex = 1;
            this.bCopyItem.Text = ">";
            this.bCopyItem.UseVisualStyleBackColor = true;
            this.bCopyItem.Click += new System.EventHandler(this.bCopyItem_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(213, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Toolbar Layout";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "ToolBar Operations";
            // 
            // lbToolbar
            // 
            this.lbToolbar.Location = new System.Drawing.Point(213, 33);
            this.lbToolbar.Name = "lbToolbar";
            this.lbToolbar.Size = new System.Drawing.Size(152, 199);
            this.lbToolbar.TabIndex = 3;
            // 
            // lbOperation
            // 
            this.lbOperation.Location = new System.Drawing.Point(14, 33);
            this.lbOperation.Name = "lbOperation";
            this.lbOperation.Size = new System.Drawing.Size(152, 199);
            this.lbOperation.TabIndex = 0;
            // 
            // tpDesktop
            // 
            this.tpDesktop.BackColor = System.Drawing.Color.White;
            this.tpDesktop.Controls.Add(this.bBrowse);
            this.tpDesktop.Controls.Add(this.tbDirectory);
            this.tpDesktop.Controls.Add(this.label9);
            this.tpDesktop.Controls.Add(this.label8);
            this.tpDesktop.Controls.Add(this.label7);
            this.tpDesktop.Controls.Add(this.ckLocal);
            this.tpDesktop.Controls.Add(this.tbPort);
            this.tpDesktop.Controls.Add(this.label6);
            this.tpDesktop.Location = new System.Drawing.Point(4, 25);
            this.tpDesktop.Name = "tpDesktop";
            this.tpDesktop.Size = new System.Drawing.Size(458, 240);
            this.tpDesktop.TabIndex = 2;
            this.tpDesktop.Tag = "desktop";
            this.tpDesktop.Text = "Desktop Server";
            // 
            // bBrowse
            // 
            this.bBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bBrowse.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bBrowse.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bBrowse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowse.Font = new System.Drawing.Font("Arial", 9F);
            this.bBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBrowse.Location = new System.Drawing.Point(411, 102);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.OverriddenSize = null;
            this.bBrowse.Size = new System.Drawing.Size(26, 21);
            this.bBrowse.TabIndex = 2;
            this.bBrowse.Text = "...";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tbDirectory
            // 
            this.tbDirectory.AddX = 0;
            this.tbDirectory.AddY = 0;
            this.tbDirectory.AllowSpace = false;
            this.tbDirectory.BorderColor = System.Drawing.Color.LightGray;
            this.tbDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDirectory.ChangeVisibility = false;
            this.tbDirectory.ChildControl = null;
            this.tbDirectory.ConvertEnterToTab = true;
            this.tbDirectory.ConvertEnterToTabForDialogs = false;
            this.tbDirectory.Decimals = 0;
            this.tbDirectory.DisplayList = new object[0];
            this.tbDirectory.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbDirectory.Location = new System.Drawing.Point(68, 100);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.OnDropDownCloseFocus = true;
            this.tbDirectory.SelectType = 0;
            this.tbDirectory.Size = new System.Drawing.Size(334, 20);
            this.tbDirectory.TabIndex = 1;
            this.tbDirectory.Text = "textBox1";
            this.tbDirectory.UseValueForChildsVisibilty = false;
            this.tbDirectory.Value = true;
            this.tbDirectory.TextChanged += new System.EventHandler(this.Desktop_Changed);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(13, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 23);
            this.label9.TabIndex = 5;
            this.label9.Text = "Directory:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(33, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(414, 26);
            this.label8.TabIndex = 4;
            this.label8.Text = "Important: Desktop server is not intended to be used as a production web server. " +
                " Use an ASP enabled server for anything other than local desktop use.";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(435, 57);
            this.label7.TabIndex = 3;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // ckLocal
            // 
            this.ckLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckLocal.ForeColor = System.Drawing.Color.Black;
            this.ckLocal.Location = new System.Drawing.Point(15, 131);
            this.ckLocal.Name = "ckLocal";
            this.ckLocal.Size = new System.Drawing.Size(190, 24);
            this.ckLocal.TabIndex = 3;
            this.ckLocal.Text = "Restrict access to local machine";
            this.ckLocal.CheckedChanged += new System.EventHandler(this.Desktop_Changed);
            // 
            // tbPort
            // 
            this.tbPort.AddX = 0;
            this.tbPort.AddY = 0;
            this.tbPort.AllowSpace = false;
            this.tbPort.BorderColor = System.Drawing.Color.LightGray;
            this.tbPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPort.ChangeVisibility = false;
            this.tbPort.ChildControl = null;
            this.tbPort.ConvertEnterToTab = true;
            this.tbPort.ConvertEnterToTabForDialogs = false;
            this.tbPort.Decimals = 0;
            this.tbPort.DisplayList = new object[0];
            this.tbPort.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbPort.Location = new System.Drawing.Point(44, 66);
            this.tbPort.Name = "tbPort";
            this.tbPort.OnDropDownCloseFocus = true;
            this.tbPort.SelectType = 0;
            this.tbPort.Size = new System.Drawing.Size(50, 20);
            this.tbPort.TabIndex = 0;
            this.tbPort.UseValueForChildsVisibilty = false;
            this.tbPort.Value = true;
            this.tbPort.TextChanged += new System.EventHandler(this.Desktop_Changed);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Port";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.bRemoveMap);
            this.tabPage1.Controls.Add(this.bAddMap);
            this.tabPage1.Controls.Add(this.lbMaps);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(458, 240);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Maps";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(435, 62);
            this.label10.TabIndex = 3;
            this.label10.Text = resources.GetString("label10.Text");
            // 
            // bRemoveMap
            // 
            this.bRemoveMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bRemoveMap.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bRemoveMap.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bRemoveMap.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bRemoveMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRemoveMap.Font = new System.Drawing.Font("Arial", 9F);
            this.bRemoveMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bRemoveMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRemoveMap.Location = new System.Drawing.Point(226, 110);
            this.bRemoveMap.Name = "bRemoveMap";
            this.bRemoveMap.OverriddenSize = null;
            this.bRemoveMap.Size = new System.Drawing.Size(75, 21);
            this.bRemoveMap.TabIndex = 2;
            this.bRemoveMap.Text = "Remove";
            this.bRemoveMap.UseVisualStyleBackColor = true;
            this.bRemoveMap.Click += new System.EventHandler(this.bRemoveMap_Click);
            // 
            // bAddMap
            // 
            this.bAddMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bAddMap.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bAddMap.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bAddMap.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bAddMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddMap.Font = new System.Drawing.Font("Arial", 9F);
            this.bAddMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bAddMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bAddMap.Location = new System.Drawing.Point(226, 81);
            this.bAddMap.Name = "bAddMap";
            this.bAddMap.OverriddenSize = null;
            this.bAddMap.Size = new System.Drawing.Size(75, 21);
            this.bAddMap.TabIndex = 1;
            this.bAddMap.Text = "Add...";
            this.bAddMap.UseVisualStyleBackColor = true;
            this.bAddMap.Click += new System.EventHandler(this.bAddMap_Click);
            // 
            // lbMaps
            // 
            this.lbMaps.FormattingEnabled = true;
            this.lbMaps.Location = new System.Drawing.Point(15, 80);
            this.lbMaps.Name = "lbMaps";
            this.lbMaps.Size = new System.Drawing.Size(188, 147);
            this.lbMaps.TabIndex = 0;
            // 
            // bApply
            // 
            this.bApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bApply.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bApply.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bApply.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bApply.Font = new System.Drawing.Font("Arial", 9F);
            this.bApply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bApply.Location = new System.Drawing.Point(386, 275);
            this.bApply.Name = "bApply";
            this.bApply.OverriddenSize = null;
            this.bApply.Size = new System.Drawing.Size(75, 21);
            this.bApply.TabIndex = 3;
            this.bApply.Text = "Apply";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // DialogToolOptions
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(466, 304);
            this.Controls.Add(this.bApply);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogToolOptions";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.gbPropLoc.ResumeLayout(false);
            this.gbPropLoc.PerformLayout();
            this.tpToolbar.ResumeLayout(false);
            this.tpDesktop.ResumeLayout(false);
            this.tpDesktop.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private bool Verify()
		{
			try
			{
				int i = Convert.ToInt32(this.tbRecentFilesMax.Text);
				return (i >= 1 || i <= 50);
			}
			catch
			{
				MessageBox.Show("Recent files maximum must be an integer between 1 and 50", "Options");
				return false;
			}
		}

		private void bOK_Click(object sender, System.EventArgs e)
		{
			if (DoApply())
			{
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private bool DoApply()
		{
			lock(this)
			{
				try
				{
					if (!Verify())
						return false;
					HandleRecentFilesMax();
					_RdlDesigner.HelpUrl = this.tbHelpUrl.Text;
                    HandleShows();
                    HandleProperties();
					if (bToolbar)
						HandleToolbar();
					if (bDesktop)
						HandleDesktop();
                    if (bMaps)
                        HandleMaps();
					bToolbar = bDesktop = false;		// no changes now
					return true;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Options");
					return false;
				}
			}
		}

        private void HandleProperties()
        {
            DockStyle ds = DockStyle.Right;
            if (this.rbPBTop.Checked)
                ds = DockStyle.Top;
            else if (this.rbPBBottom.Checked)
                ds = DockStyle.Bottom;
            else if (this.rbPBLeft.Checked)
                ds = DockStyle.Left;

            _RdlDesigner.PropertiesLocation = ds;
            _RdlDesigner.PropertiesAutoHide = chkPBAutoHide.Checked;
        }

		private void HandleDesktop()
		{
			if (_DesktopDocument == null)
			{
				_DesktopDocument = new XmlDocument();
				XmlProcessingInstruction xPI;
				xPI = _DesktopDocument.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
				_DesktopDocument.AppendChild(xPI);
			}

			if (_DesktopPort == null)
			{
				_DesktopPort = _DesktopDocument.CreateElement("port");
				_DesktopDocument.AppendChild(_DesktopPort);
			}
			_DesktopPort.InnerText = this.tbPort.Text;

			if (_DesktopDirectory == null)
			{
				_DesktopDirectory = _DesktopDocument.CreateElement("serverroot");
				_DesktopDocument.AppendChild(_DesktopDirectory);
			}
			_DesktopDirectory.InnerText = this.tbDirectory.Text;

			if (_DesktopLocal == null)
			{
				_DesktopLocal = _DesktopDocument.CreateElement("localhostonly");
				_DesktopDocument.AppendChild(_DesktopLocal);
			}
			_DesktopLocal.InnerText = this.ckLocal.Checked? "true": "false";

			string optFileName = AppDomain.CurrentDomain.BaseDirectory + "config.xml";

			_DesktopDocument.Save(optFileName);
			this._RdlDesigner.tsmToolsCloseProcess(false);		// close the server
		}

        private void HandleMaps()
        {
            string[] maps = new string[lbMaps.Items.Count];
            for (int i = 0; i < lbMaps.Items.Count; i++)
            {
                maps[i] = lbMaps.Items[i] as string;
            }
            RdlDesigner.MapSubtypes = maps;
        }

        private void HandleRecentFilesMax()
		{
			// Handle the RecentFilesMax
			int i = Convert.ToInt32(this.tbRecentFilesMax.Text);
			if (i < 1 || i > 50)
				throw new Exception("Recent files maximum must be an integer between 1 and 50");
			if (this._RdlDesigner.RecentFilesMax == i)	// if not different we don't need to do anything
				return;

			this._RdlDesigner.RecentFilesMax = i;

			// Make the list match the maximum size
			bool bChangeMenu=false;
			while (_RdlDesigner.RecentFiles.Count > _RdlDesigner.RecentFilesMax)
			{
				_RdlDesigner.RecentFiles.RemoveAt(0);	// remove the first entry
				bChangeMenu = true;
			}

			if (bChangeMenu)
				_RdlDesigner.RecentFilesMenu();			// reset the menu since the list changed
			return;
		}

		private void HandleToolbar()
		{
            List<string> ar = new List<string>();
			foreach (string item in this.lbToolbar.Items)
				ar.Add(item);
			this._RdlDesigner.Toolbar = ar;
		}

        private void HandleShows()
        {
            _RdlDesigner.ShowEditLines = this.cbEditLines.Checked;
            _RdlDesigner.ShowReportItemOutline = this.cbOutline.Checked;
            _RdlDesigner.ShowTabbedInterface = this.cbTabInterface.Checked;
            _RdlDesigner.ShowPreviewWaitDialog = this.cbShowReportWaitDialog.Checked;

		    foreach (MDIChild mc in _RdlDesigner.MdiChildren)
			{
                mc.ShowEditLines(this.cbEditLines.Checked);
                mc.ShowReportItemOutline = this.cbOutline.Checked;
                mc.ShowPreviewWaitDialog(this.cbShowReportWaitDialog.Checked);
			}

        }

		private void bCopyItem_Click(object sender, System.EventArgs e)
		{
			bToolbar=true;
			int i = this.lbOperation.SelectedIndex;
			if (i < 0)
				return;
			string itm = lbOperation.Items[i] as String;
			lbToolbar.Items.Add(itm);
			// Remove from list if not allowed to be duplicated in toolbar
			if (!_RdlDesigner.ToolbarAllowDups.Contains(itm))
				lbOperation.Items.RemoveAt(i);
		}

		private void bRemove_Click(object sender, System.EventArgs e)
		{
			bToolbar = true;
			int i = this.lbToolbar.SelectedIndex;
			if (i < 0)
				return;
			string itm = lbToolbar.Items[i] as String;
			if (itm == "Newline" || itm == "Space") 
				{}
			else
				lbOperation.Items.Add(itm);

			lbToolbar.Items.RemoveAt(i);
		}

		private void bUp_Click(object sender, System.EventArgs e)
		{
			int i = this.lbToolbar.SelectedIndex;
			if (i <= 0)
				return;

			Swap(i-1, i);
		}

		private void bDown_Click(object sender, System.EventArgs e)
		{
			int i = this.lbToolbar.SelectedIndex;
			if (i < 0 || i == lbToolbar.Items.Count-1)
				return;

			Swap(i, i+1);
		}

		/// <summary>
		/// Swap items in the toolbar listbox.  i1 should always be less than i2
		/// </summary>
		/// <param name="i1"></param>
		/// <param name="i2"></param>
		private void Swap(int i1, int i2)
		{
			bToolbar = true;
			bool b1 = (i1 == lbToolbar.SelectedIndex);

			string s1 = lbToolbar.Items[i1] as string;
			string s2 = lbToolbar.Items[i2] as string;
			lbToolbar.SuspendLayout();
			lbToolbar.Items.RemoveAt(i2);
			lbToolbar.Items.RemoveAt(i1);
			lbToolbar.Items.Insert(i1, s2);
			lbToolbar.Items.Insert(i2, s1);
			lbToolbar.SelectedIndex = b1? i2: i1;
			lbToolbar.ResumeLayout(true);
		}

		private void bReset_Click(object sender, System.EventArgs e)
		{
			bToolbar = true;

			this.lbToolbar.Items.Clear();
			List<string> ar =   this._RdlDesigner.ToolbarDefault;
			foreach (string itm in ar)
				this.lbToolbar.Items.Add(itm);

			InitOperations();
		}

		private void bApply_Click(object sender, System.EventArgs e)
		{
			DoApply();		
		}

		private void Desktop_Changed(object sender, System.EventArgs e)
		{
			bDesktop = true;
		}

		private void bBrowse_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			// Set the help text description for the FolderBrowserDialog.
			fbd.Description = 
				"Select the directory that will contain reports.";

			// Do not allow the user to create new files via the FolderBrowserDialog.
			fbd.ShowNewFolderButton = false;
			//			fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
			fbd.SelectedPath = this.tbDirectory.Text.Length == 0?
				"Examples": tbDirectory.Text;

            try
            {
                if (fbd.ShowDialog(this) == DialogResult.Cancel)
                    return;

                tbDirectory.Text = fbd.SelectedPath;
                bDesktop = true;		// we modified Desktop settings

            }
            finally
            {
                fbd.Dispose();
            }

            return;
		}

		static internal DesktopConfig DesktopConfiguration
		{
			get 
			{
				string optFileName = AppDomain.CurrentDomain.BaseDirectory + "config.xml";
			
				DesktopConfig dc = new DesktopConfig();
				try
				{
					XmlDocument xDoc = new XmlDocument();
					xDoc.Load(optFileName);
					XmlNode xNode;
					xNode = xDoc.SelectSingleNode("//config");

					// Loop thru all the child nodes
					foreach(XmlNode xNodeLoop in xNode.ChildNodes)
					{
						if (xNodeLoop.NodeType != XmlNodeType.Element)
							continue;
						switch (xNodeLoop.Name.ToLower())
						{
							case "serverroot":
								dc.Directory = xNodeLoop.InnerText;
								break;
							case "port":
								dc.Port = xNodeLoop.InnerText;
								break;
						}	
					}
					return dc;
				}
				catch (Exception ex)
				{	
					throw new Exception(string.Format("Unable to obtain Desktop config information.\n{0}", ex.Message));
				}
			}
		}

        private void cbTabInterface_CheckedChanged(object sender, EventArgs e)
        {
            this.bToolbar = true;   // tabbed interface is part of the toolbar
        }

        private void bAddMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ofd.DefaultExt = "rdl";
            ofd.Filter = "Map files (*.xml)|*.xml|" +
                "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.CheckFileExists = true;
            ofd.Multiselect = true;
            try
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        string nm = Path.GetFileNameWithoutExtension(file);
                        if (!lbMaps.Items.Contains(nm))
                        {
                            lbMaps.Items.Add(nm);
                            bMaps = true;
                        }
                    }
                }
            }
            finally
            {
                ofd.Dispose();
            }
        }

        private void bRemoveMap_Click(object sender, EventArgs e)
        {
            if (lbMaps.SelectedIndex < 0)
                return;
            lbMaps.Items.RemoveAt(lbMaps.SelectedIndex);
            bMaps = true;
            return;
        }
	}

	internal class DesktopConfig
	{
		internal string Directory;
		internal string Port;
	}
}
