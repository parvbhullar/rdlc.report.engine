using System.Windows.Forms;
using System.Drawing;
namespace Oranikle.ReportDesigner
{
    partial class DesignerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignerForm));
            this.msDesigner = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUserManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCloseOrganization = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmChequeBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDefaultNarration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSalePurchaseForm = new System.Windows.Forms.ToolStripMenuItem();
            this.tssGroupbts = new System.Windows.Forms.ToolStripSeparator();
            this.tsmBroker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDesigner = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmPropertyWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDataSet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmDataSources = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmEmbeddedImages = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.bExcel = new Oranikle.Studio.Controls.StyledButton();
            this.bSelectTool = new Oranikle.Studio.Controls.StyledButton();
            this.bLAlign = new Oranikle.Studio.Controls.StyledButton();
            this.bRAlign = new Oranikle.Studio.Controls.StyledButton();
            this.bCAlign = new Oranikle.Studio.Controls.StyledButton();
            this.bCsv = new Oranikle.Studio.Controls.StyledButton();
            this.bXml = new Oranikle.Studio.Controls.StyledButton();
            this.bUndo = new Oranikle.Studio.Controls.StyledButton();
            this.bNew = new Oranikle.Studio.Controls.StyledButton();
            this.bCut = new Oranikle.Studio.Controls.StyledButton();
            this.bCopy = new Oranikle.Studio.Controls.StyledButton();
            this.bPaste = new Oranikle.Studio.Controls.StyledButton();
            this.bOpen = new Oranikle.Studio.Controls.StyledButton();
            this.bSave = new Oranikle.Studio.Controls.StyledButton();
            this.bPrint = new Oranikle.Studio.Controls.StyledButton();
            this.bMatrix = new Oranikle.Studio.Controls.StyledButton();
            this.bText = new Oranikle.Studio.Controls.StyledButton();
            this.bChart = new Oranikle.Studio.Controls.StyledButton();
            this.bList = new Oranikle.Studio.Controls.StyledButton();
            this.bSubreport = new Oranikle.Studio.Controls.StyledButton();
            this.bRectangle = new Oranikle.Studio.Controls.StyledButton();
            this.bImage = new Oranikle.Studio.Controls.StyledButton();
            this.bLine = new Oranikle.Studio.Controls.StyledButton();
            this.bTable = new Oranikle.Studio.Controls.StyledButton();
            this.bTif = new Oranikle.Studio.Controls.StyledButton();
            this.msDesigner.SuspendLayout();
            this.SuspendLayout();
            // 
            // msDesigner
            // 
            this.msDesigner.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmEdit,
            this.tsmView,
            this.tsmData,
            this.tsmTools,
            this.tsmHelp});
            this.msDesigner.Location = new System.Drawing.Point(0, 0);
            this.msDesigner.Name = "msDesigner";
            this.msDesigner.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.msDesigner.Size = new System.Drawing.Size(830, 24);
            this.msDesigner.TabIndex = 3;
            this.msDesigner.Text = "Studio Menu";
            this.msDesigner.Visible = false;
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUserManagement,
            this.tsmOpen,
            this.tsmCloseOrganization,
            this.toolStripSeparator1,
            this.tsmSave,
            this.tsmSaveAs,
            this.toolStripSeparator6,
            this.tsmRecentFiles,
            this.tsmExit});
            this.tsmFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(38, 20);
            this.tsmFile.Text = "&File";
            // 
            // tsmUserManagement
            // 
            this.tsmUserManagement.Name = "tsmUserManagement";
            this.tsmUserManagement.Size = new System.Drawing.Size(141, 22);
            this.tsmUserManagement.Text = "&New";
            this.tsmUserManagement.Visible = false;
            // 
            // tsmOpen
            // 
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.Size = new System.Drawing.Size(141, 22);
            this.tsmOpen.Text = "&Open";
            this.tsmOpen.Visible = false;
            // 
            // tsmCloseOrganization
            // 
            this.tsmCloseOrganization.Name = "tsmCloseOrganization";
            this.tsmCloseOrganization.Size = new System.Drawing.Size(141, 22);
            this.tsmCloseOrganization.Text = "&Close";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // tsmSave
            // 
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.Size = new System.Drawing.Size(141, 22);
            this.tsmSave.Text = "Save";
            // 
            // tsmSaveAs
            // 
            this.tsmSaveAs.Name = "tsmSaveAs";
            this.tsmSaveAs.Size = new System.Drawing.Size(141, 22);
            this.tsmSaveAs.Text = "Save As";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(138, 6);
            // 
            // tsmRecentFiles
            // 
            this.tsmRecentFiles.Name = "tsmRecentFiles";
            this.tsmRecentFiles.Size = new System.Drawing.Size(141, 22);
            this.tsmRecentFiles.Text = "&Recent Files";
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(141, 22);
            this.tsmExit.Text = "&Exit";
            // 
            // tsmEdit
            // 
            this.tsmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmChequeBook,
            this.tsmDefaultNarration,
            this.tsmSalePurchaseForm,
            this.tssGroupbts,
            this.tsmBroker});
            this.tsmEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(40, 20);
            this.tsmEdit.Text = "&Edit";
            // 
            // tsmChequeBook
            // 
            this.tsmChequeBook.Name = "tsmChequeBook";
            this.tsmChequeBook.Size = new System.Drawing.Size(126, 22);
            this.tsmChequeBook.Text = "Cut";
            // 
            // tsmDefaultNarration
            // 
            this.tsmDefaultNarration.Name = "tsmDefaultNarration";
            this.tsmDefaultNarration.Size = new System.Drawing.Size(126, 22);
            this.tsmDefaultNarration.Text = "Copy";
            // 
            // tsmSalePurchaseForm
            // 
            this.tsmSalePurchaseForm.Name = "tsmSalePurchaseForm";
            this.tsmSalePurchaseForm.Size = new System.Drawing.Size(126, 22);
            this.tsmSalePurchaseForm.Text = "Paste";
            // 
            // tssGroupbts
            // 
            this.tssGroupbts.Name = "tssGroupbts";
            this.tssGroupbts.Size = new System.Drawing.Size(123, 6);
            // 
            // tsmBroker
            // 
            this.tsmBroker.Name = "tsmBroker";
            this.tsmBroker.Size = new System.Drawing.Size(126, 22);
            this.tsmBroker.Text = "Select All";
            // 
            // tsmView
            // 
            this.tsmView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDesigner,
            this.tsmCode,
            this.tsmPreview,
            this.toolStripMenuItem5,
            this.tsmPropertyWindows,
            this.toolboxToolStripMenuItem});
            this.tsmView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsmView.Name = "tsmView";
            this.tsmView.Size = new System.Drawing.Size(47, 20);
            this.tsmView.Text = "&View";
            // 
            // tsmDesigner
            // 
            this.tsmDesigner.Name = "tsmDesigner";
            this.tsmDesigner.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmDesigner.Size = new System.Drawing.Size(192, 22);
            this.tsmDesigner.Tag = "report_view";
            this.tsmDesigner.Text = "Designer";
            // 
            // tsmCode
            // 
            this.tsmCode.Name = "tsmCode";
            this.tsmCode.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.tsmCode.Size = new System.Drawing.Size(192, 22);
            this.tsmCode.Text = "Code";
            // 
            // tsmPreview
            // 
            this.tsmPreview.Name = "tsmPreview";
            this.tsmPreview.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmPreview.Size = new System.Drawing.Size(192, 22);
            this.tsmPreview.Tag = "report_view";
            this.tsmPreview.Text = "Preview";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(189, 6);
            // 
            // tsmPropertyWindows
            // 
            this.tsmPropertyWindows.Name = "tsmPropertyWindows";
            this.tsmPropertyWindows.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.tsmPropertyWindows.Size = new System.Drawing.Size(192, 22);
            this.tsmPropertyWindows.Text = "Property Window";
            // 
            // toolboxToolStripMenuItem
            // 
            this.toolboxToolStripMenuItem.Name = "toolboxToolStripMenuItem";
            this.toolboxToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.toolboxToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.toolboxToolStripMenuItem.Text = "Toolbox";
            // 
            // tsmData
            // 
            this.tsmData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDataSet,
            this.toolStripMenuItem3,
            this.tsmDataSources,
            this.toolStripSeparator2,
            this.tsmEmbeddedImages});
            this.tsmData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsmData.Name = "tsmData";
            this.tsmData.Size = new System.Drawing.Size(45, 20);
            this.tsmData.Text = "&Data";
            // 
            // tsmDataSet
            // 
            this.tsmDataSet.Name = "tsmDataSet";
            this.tsmDataSet.Size = new System.Drawing.Size(176, 22);
            this.tsmDataSet.Text = "Data Set";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmDataSources
            // 
            this.tsmDataSources.Name = "tsmDataSources";
            this.tsmDataSources.Size = new System.Drawing.Size(176, 22);
            this.tsmDataSources.Tag = "website";
            this.tsmDataSources.Text = "Data Sources";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmEmbeddedImages
            // 
            this.tsmEmbeddedImages.Name = "tsmEmbeddedImages";
            this.tsmEmbeddedImages.Size = new System.Drawing.Size(176, 22);
            this.tsmEmbeddedImages.Text = "Embedded Images";
            // 
            // tsmTools
            // 
            this.tsmTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmConfiguration});
            this.tsmTools.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsmTools.Name = "tsmTools";
            this.tsmTools.Size = new System.Drawing.Size(48, 20);
            this.tsmTools.Text = "T&ools";
            // 
            // tsmConfiguration
            // 
            this.tsmConfiguration.Name = "tsmConfiguration";
            this.tsmConfiguration.Size = new System.Drawing.Size(150, 22);
            this.tsmConfiguration.Text = "Configuration";
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAboutUs});
            this.tsmHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(45, 20);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmAboutUs
            // 
            this.tsmAboutUs.Name = "tsmAboutUs";
            this.tsmAboutUs.Size = new System.Drawing.Size(125, 22);
            this.tsmAboutUs.Text = "About Us";
            // 
            // bExcel
            // 
            this.bExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bExcel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bExcel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bExcel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.bExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bExcel.Image = global::Oranikle.ReportDesigner.Properties.Resources.excel;
            this.bExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bExcel.Location = new System.Drawing.Point(531, 61);
            this.bExcel.Name = "bExcel";
            this.bExcel.OverriddenSize = null;
            this.bExcel.Size = new System.Drawing.Size(75, 21);
            this.bExcel.TabIndex = 27;
            this.bExcel.Text = "XLS";
            this.bExcel.UseVisualStyleBackColor = true;
            this.bExcel.Visible = false;
            // 
            // bSelectTool
            // 
            this.bSelectTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSelectTool.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSelectTool.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSelectTool.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSelectTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSelectTool.Font = new System.Drawing.Font("Arial", 9F);
            this.bSelectTool.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSelectTool.Image = global::Oranikle.ReportDesigner.Properties.Resources.selecttool;
            this.bSelectTool.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSelectTool.Location = new System.Drawing.Point(531, 32);
            this.bSelectTool.Name = "bSelectTool";
            this.bSelectTool.OverriddenSize = null;
            this.bSelectTool.Size = new System.Drawing.Size(75, 21);
            this.bSelectTool.TabIndex = 26;
            this.bSelectTool.Text = "SelectTool";
            this.bSelectTool.UseVisualStyleBackColor = true;
            this.bSelectTool.Visible = false;
            // 
            // bLAlign
            // 
            this.bLAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bLAlign.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bLAlign.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bLAlign.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bLAlign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLAlign.Font = new System.Drawing.Font("Arial", 9F);
            this.bLAlign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bLAlign.Image = global::Oranikle.ReportDesigner.Properties.Resources.left;
            this.bLAlign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLAlign.Location = new System.Drawing.Point(625, 389);
            this.bLAlign.Name = "bLAlign";
            this.bLAlign.OverriddenSize = null;
            this.bLAlign.Size = new System.Drawing.Size(75, 21);
            this.bLAlign.TabIndex = 24;
            this.bLAlign.Text = "L Align";
            this.bLAlign.UseVisualStyleBackColor = true;
            this.bLAlign.Visible = false;
            // 
            // bRAlign
            // 
            this.bRAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bRAlign.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bRAlign.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bRAlign.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bRAlign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRAlign.Font = new System.Drawing.Font("Arial", 9F);
            this.bRAlign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bRAlign.Image = global::Oranikle.ReportDesigner.Properties.Resources.right;
            this.bRAlign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRAlign.Location = new System.Drawing.Point(729, 389);
            this.bRAlign.Name = "bRAlign";
            this.bRAlign.OverriddenSize = null;
            this.bRAlign.Size = new System.Drawing.Size(75, 21);
            this.bRAlign.TabIndex = 23;
            this.bRAlign.Text = "R Align";
            this.bRAlign.UseVisualStyleBackColor = true;
            this.bRAlign.Visible = false;
            // 
            // bCAlign
            // 
            this.bCAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCAlign.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCAlign.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCAlign.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCAlign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCAlign.Font = new System.Drawing.Font("Arial", 9F);
            this.bCAlign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCAlign.Image = global::Oranikle.ReportDesigner.Properties.Resources.centre;
            this.bCAlign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCAlign.Location = new System.Drawing.Point(729, 418);
            this.bCAlign.Name = "bCAlign";
            this.bCAlign.OverriddenSize = null;
            this.bCAlign.Size = new System.Drawing.Size(75, 21);
            this.bCAlign.TabIndex = 22;
            this.bCAlign.Text = "C Align";
            this.bCAlign.UseVisualStyleBackColor = true;
            this.bCAlign.Visible = false;
            // 
            // bCsv
            // 
            this.bCsv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCsv.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCsv.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCsv.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCsv.Font = new System.Drawing.Font("Arial", 9F);
            this.bCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCsv.Image = global::Oranikle.ReportDesigner.Properties.Resources.csv;
            this.bCsv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCsv.Location = new System.Drawing.Point(729, 360);
            this.bCsv.Name = "bCsv";
            this.bCsv.OverriddenSize = null;
            this.bCsv.Size = new System.Drawing.Size(75, 21);
            this.bCsv.TabIndex = 21;
            this.bCsv.Text = "CSV";
            this.bCsv.UseVisualStyleBackColor = true;
            this.bCsv.Visible = false;
            // 
            // bXml
            // 
            this.bXml.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bXml.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bXml.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bXml.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bXml.Font = new System.Drawing.Font("Arial", 9F);
            this.bXml.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bXml.Image = global::Oranikle.ReportDesigner.Properties.Resources.source;
            this.bXml.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bXml.Location = new System.Drawing.Point(625, 288);
            this.bXml.Name = "bXml";
            this.bXml.OverriddenSize = null;
            this.bXml.Size = new System.Drawing.Size(75, 21);
            this.bXml.TabIndex = 18;
            this.bXml.Text = "XML";
            this.bXml.UseVisualStyleBackColor = true;
            this.bXml.Visible = false;
            // 
            // bUndo
            // 
            this.bUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bUndo.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bUndo.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bUndo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUndo.Font = new System.Drawing.Font("Arial", 9F);
            this.bUndo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bUndo.Image = global::Oranikle.ReportDesigner.Properties.Resources.undo1;
            this.bUndo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bUndo.Location = new System.Drawing.Point(625, 256);
            this.bUndo.Name = "bUndo";
            this.bUndo.OverriddenSize = null;
            this.bUndo.Size = new System.Drawing.Size(75, 21);
            this.bUndo.TabIndex = 16;
            this.bUndo.Text = "Undo";
            this.bUndo.UseVisualStyleBackColor = true;
            this.bUndo.Visible = false;
            // 
            // bNew
            // 
            this.bNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bNew.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bNew.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bNew.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNew.Font = new System.Drawing.Font("Arial", 9F);
            this.bNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bNew.Image = global::Oranikle.ReportDesigner.Properties.Resources.data_add;
            this.bNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bNew.Location = new System.Drawing.Point(625, 224);
            this.bNew.Name = "bNew";
            this.bNew.OverriddenSize = null;
            this.bNew.Size = new System.Drawing.Size(75, 21);
            this.bNew.TabIndex = 15;
            this.bNew.Text = "New";
            this.bNew.UseVisualStyleBackColor = true;
            this.bNew.Visible = false;
            // 
            // bCut
            // 
            this.bCut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCut.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCut.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCut.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCut.Font = new System.Drawing.Font("Arial", 9F);
            this.bCut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCut.Image = global::Oranikle.ReportDesigner.Properties.Resources.CutNew;
            this.bCut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCut.Location = new System.Drawing.Point(625, 32);
            this.bCut.Name = "bCut";
            this.bCut.OverriddenSize = null;
            this.bCut.Size = new System.Drawing.Size(75, 21);
            this.bCut.TabIndex = 14;
            this.bCut.Text = "Cut";
            this.bCut.UseVisualStyleBackColor = true;
            this.bCut.Visible = false;
            // 
            // bCopy
            // 
            this.bCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bCopy.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bCopy.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bCopy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCopy.Font = new System.Drawing.Font("Arial", 9F);
            this.bCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bCopy.Image = global::Oranikle.ReportDesigner.Properties.Resources.CopyNew;
            this.bCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCopy.Location = new System.Drawing.Point(625, 64);
            this.bCopy.Name = "bCopy";
            this.bCopy.OverriddenSize = null;
            this.bCopy.Size = new System.Drawing.Size(75, 21);
            this.bCopy.TabIndex = 13;
            this.bCopy.Text = "Copy";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Visible = false;
            // 
            // bPaste
            // 
            this.bPaste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bPaste.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bPaste.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bPaste.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPaste.Font = new System.Drawing.Font("Arial", 9F);
            this.bPaste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bPaste.Image = global::Oranikle.ReportDesigner.Properties.Resources.paste1;
            this.bPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bPaste.Location = new System.Drawing.Point(625, 96);
            this.bPaste.Name = "bPaste";
            this.bPaste.OverriddenSize = null;
            this.bPaste.Size = new System.Drawing.Size(75, 21);
            this.bPaste.TabIndex = 12;
            this.bPaste.Text = "Paste";
            this.bPaste.UseVisualStyleBackColor = true;
            this.bPaste.Visible = false;
            // 
            // bOpen
            // 
            this.bOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bOpen.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bOpen.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bOpen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpen.Font = new System.Drawing.Font("Arial", 9F);
            this.bOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bOpen.Image = global::Oranikle.ReportDesigner.Properties.Resources.open;
            this.bOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOpen.Location = new System.Drawing.Point(625, 128);
            this.bOpen.Name = "bOpen";
            this.bOpen.OverriddenSize = null;
            this.bOpen.Size = new System.Drawing.Size(75, 21);
            this.bOpen.TabIndex = 11;
            this.bOpen.Text = "Open";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Visible = false;
            // 
            // bSave
            // 
            this.bSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSave.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSave.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSave.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Font = new System.Drawing.Font("Arial", 9F);
            this.bSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSave.Image = global::Oranikle.ReportDesigner.Properties.Resources.saveHS;
            this.bSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSave.Location = new System.Drawing.Point(625, 160);
            this.bSave.Name = "bSave";
            this.bSave.OverriddenSize = null;
            this.bSave.Size = new System.Drawing.Size(75, 21);
            this.bSave.TabIndex = 10;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Visible = false;
            // 
            // bPrint
            // 
            this.bPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bPrint.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bPrint.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bPrint.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPrint.Font = new System.Drawing.Font("Arial", 9F);
            this.bPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bPrint.Image = global::Oranikle.ReportDesigner.Properties.Resources.print1;
            this.bPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bPrint.Location = new System.Drawing.Point(625, 192);
            this.bPrint.Name = "bPrint";
            this.bPrint.OverriddenSize = null;
            this.bPrint.Size = new System.Drawing.Size(75, 21);
            this.bPrint.TabIndex = 9;
            this.bPrint.Text = "Print";
            this.bPrint.UseVisualStyleBackColor = true;
            this.bPrint.Visible = false;
            // 
            // bMatrix
            // 
            this.bMatrix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bMatrix.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bMatrix.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bMatrix.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bMatrix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMatrix.Font = new System.Drawing.Font("Arial", 9F);
            this.bMatrix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bMatrix.Image = global::Oranikle.ReportDesigner.Properties.Resources.matrix;
            this.bMatrix.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMatrix.Location = new System.Drawing.Point(729, 64);
            this.bMatrix.Name = "bMatrix";
            this.bMatrix.OverriddenSize = null;
            this.bMatrix.Size = new System.Drawing.Size(75, 21);
            this.bMatrix.TabIndex = 8;
            this.bMatrix.Text = "Matrix";
            this.bMatrix.UseVisualStyleBackColor = true;
            this.bMatrix.Visible = false;
            // 
            // bText
            // 
            this.bText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bText.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bText.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bText.Font = new System.Drawing.Font("Arial", 9F);
            this.bText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bText.Image = global::Oranikle.ReportDesigner.Properties.Resources.text;
            this.bText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bText.Location = new System.Drawing.Point(729, 96);
            this.bText.Name = "bText";
            this.bText.OverriddenSize = null;
            this.bText.Size = new System.Drawing.Size(75, 21);
            this.bText.TabIndex = 7;
            this.bText.Text = "Text";
            this.bText.UseVisualStyleBackColor = true;
            this.bText.Visible = false;
            // 
            // bChart
            // 
            this.bChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bChart.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bChart.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bChart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bChart.Font = new System.Drawing.Font("Arial", 9F);
            this.bChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bChart.Image = global::Oranikle.ReportDesigner.Properties.Resources.chart;
            this.bChart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bChart.Location = new System.Drawing.Point(729, 128);
            this.bChart.Name = "bChart";
            this.bChart.OverriddenSize = null;
            this.bChart.Size = new System.Drawing.Size(75, 21);
            this.bChart.TabIndex = 6;
            this.bChart.Text = "Chart";
            this.bChart.UseVisualStyleBackColor = true;
            this.bChart.Visible = false;
            // 
            // bList
            // 
            this.bList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bList.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bList.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bList.Font = new System.Drawing.Font("Arial", 9F);
            this.bList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bList.Image = global::Oranikle.ReportDesigner.Properties.Resources.list;
            this.bList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bList.Location = new System.Drawing.Point(729, 160);
            this.bList.Name = "bList";
            this.bList.OverriddenSize = null;
            this.bList.Size = new System.Drawing.Size(75, 21);
            this.bList.TabIndex = 5;
            this.bList.Text = "List";
            this.bList.UseVisualStyleBackColor = true;
            this.bList.Visible = false;
            // 
            // bSubreport
            // 
            this.bSubreport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bSubreport.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bSubreport.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bSubreport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bSubreport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSubreport.Font = new System.Drawing.Font("Arial", 9F);
            this.bSubreport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bSubreport.Image = global::Oranikle.ReportDesigner.Properties.Resources.subreport;
            this.bSubreport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSubreport.Location = new System.Drawing.Point(729, 192);
            this.bSubreport.Name = "bSubreport";
            this.bSubreport.OverriddenSize = null;
            this.bSubreport.Size = new System.Drawing.Size(75, 21);
            this.bSubreport.TabIndex = 4;
            this.bSubreport.Text = "Subreport";
            this.bSubreport.UseVisualStyleBackColor = true;
            this.bSubreport.Visible = false;
            // 
            // bRectangle
            // 
            this.bRectangle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bRectangle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bRectangle.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bRectangle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRectangle.Font = new System.Drawing.Font("Arial", 9F);
            this.bRectangle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bRectangle.Image = global::Oranikle.ReportDesigner.Properties.Resources.rectangle;
            this.bRectangle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRectangle.Location = new System.Drawing.Point(729, 224);
            this.bRectangle.Name = "bRectangle";
            this.bRectangle.OverriddenSize = null;
            this.bRectangle.Size = new System.Drawing.Size(75, 21);
            this.bRectangle.TabIndex = 3;
            this.bRectangle.Text = "Rect";
            this.bRectangle.UseVisualStyleBackColor = true;
            this.bRectangle.Visible = false;
            // 
            // bImage
            // 
            this.bImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bImage.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bImage.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bImage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bImage.Font = new System.Drawing.Font("Arial", 9F);
            this.bImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bImage.Image = global::Oranikle.ReportDesigner.Properties.Resources.image1;
            this.bImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bImage.Location = new System.Drawing.Point(729, 256);
            this.bImage.Name = "bImage";
            this.bImage.OverriddenSize = null;
            this.bImage.Size = new System.Drawing.Size(75, 21);
            this.bImage.TabIndex = 2;
            this.bImage.Text = "System.Drawing.Image";
            this.bImage.UseVisualStyleBackColor = true;
            this.bImage.Visible = false;
            // 
            // bLine
            // 
            this.bLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bLine.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bLine.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bLine.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLine.Font = new System.Drawing.Font("Arial", 9F);
            this.bLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bLine.Image = global::Oranikle.ReportDesigner.Properties.Resources.line1;
            this.bLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLine.Location = new System.Drawing.Point(729, 288);
            this.bLine.Name = "bLine";
            this.bLine.OverriddenSize = null;
            this.bLine.Size = new System.Drawing.Size(75, 21);
            this.bLine.TabIndex = 1;
            this.bLine.Text = "Line";
            this.bLine.UseVisualStyleBackColor = true;
            this.bLine.Visible = false;
            // 
            // bTable
            // 
            this.bTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bTable.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bTable.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTable.Font = new System.Drawing.Font("Arial", 9F);
            this.bTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bTable.Image = global::Oranikle.ReportDesigner.Properties.Resources.datatable;
            this.bTable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTable.Location = new System.Drawing.Point(729, 32);
            this.bTable.Name = "bTable";
            this.bTable.OverriddenSize = null;
            this.bTable.Size = new System.Drawing.Size(75, 21);
            this.bTable.TabIndex = 0;
            this.bTable.Text = "Table";
            this.bTable.UseVisualStyleBackColor = true;
            this.bTable.Visible = false;
            // 
            // bTif
            // 
            this.bTif.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bTif.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bTif.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bTif.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bTif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTif.Font = new System.Drawing.Font("Arial", 9F);
            this.bTif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bTif.Image = global::Oranikle.ReportDesigner.Properties.Resources.tif;
            this.bTif.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTif.Location = new System.Drawing.Point(531, 96);
            this.bTif.Name = "bTif";
            this.bTif.OverriddenSize = null;
            this.bTif.Size = new System.Drawing.Size(75, 21);
            this.bTif.TabIndex = 28;
            this.bTif.Text = "TIF";
            this.bTif.UseVisualStyleBackColor = true;
            this.bTif.Visible = false;
            // 
            // DesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(830, 465);
            this.Controls.Add(this.msDesigner);
            this.Controls.Add(this.bTif);
            this.Controls.Add(this.bExcel);
            this.Controls.Add(this.bSelectTool);
            this.Controls.Add(this.bLAlign);
            this.Controls.Add(this.bRAlign);
            this.Controls.Add(this.bCAlign);
            this.Controls.Add(this.bCsv);
            this.Controls.Add(this.bXml);
            this.Controls.Add(this.bUndo);
            this.Controls.Add(this.bNew);
            this.Controls.Add(this.bCut);
            this.Controls.Add(this.bCopy);
            this.Controls.Add(this.bPaste);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bPrint);
            this.Controls.Add(this.bMatrix);
            this.Controls.Add(this.bText);
            this.Controls.Add(this.bChart);
            this.Controls.Add(this.bList);
            this.Controls.Add(this.bSubreport);
            this.Controls.Add(this.bRectangle);
            this.Controls.Add(this.bImage);
            this.Controls.Add(this.bLine);
            this.Controls.Add(this.bTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "DesignerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oranikle Report Designer";
            this.msDesigner.ResumeLayout(false);
            this.msDesigner.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        // Status bar
        private System.Windows.Forms.StatusBar mainSB;
        private StatusBarPanel statusPrimary;
        private StatusBarPanel statusSelected;
        private StatusBarPanel statusPosition;

        // Tool bar  --- if you add to this list LOOK AT INIT TOOLBAR FIRST
        bool bSuppressChange = false;
        private System.Windows.Forms.ToolBar mainTB;
        private PropertyCtl mainProperties;
        private Splitter mainSp;
        internal Oranikle.Studio.Controls.CtrlStyledTab mainTC;
        private SimpleToggle ctlBold = null;
        private SimpleToggle ctlItalic = null;
        private SimpleToggle ctlUnderline = null;
        private SimpleToggle ctlLAlign = null;
        private SimpleToggle ctlRAlign = null;
        private SimpleToggle ctlCAlign = null;
        private ComboBox ctlFont = null;
        private ComboBox ctlFontSize = null;
        private ColorPicker ctlForeColor = null;
        private ColorPicker ctlBackColor = null;
        private Button ctlNew = null;
        private Button ctlOpen = null;
        private Button ctlSave = null;
        private Button ctlCut = null;
        private Button ctlCopy = null;
        private Button ctlUndo = null;
        private Button ctlPaste = null;
        private Button ctlPrint = null;
        private Button ctlPdf = null;
        private Button ctlTif = null;
        private Button ctlXml = null;
        private Button ctlHtml = null;
        private Button ctlMht = null;
        private Button ctlCsv = null;
        private Button ctlRtf = null;
        private Button ctlExcel = null;
        private ComboBox ctlZoom = null;
        private SimpleToggle ctlInsertCurrent = null;
        private SimpleToggle ctlInsertTextbox = null;
        private SimpleToggle ctlInsertChart = null;
        private SimpleToggle ctlInsertRectangle = null;
        private SimpleToggle ctlInsertTable = null;
        private SimpleToggle ctlInsertMatrix = null;
        private SimpleToggle ctlInsertList = null;
        private SimpleToggle ctlInsertLine = null;
        private SimpleToggle ctlInsertImage = null;
        private SimpleToggle ctlInsertSubreport = null;
        private SimpleToggle ctlSelectTool = null;

        // Edit items
        private Oranikle.Studio.Controls.CustomTextControl ctlEditTextbox = null;			// when you're editting textbox's
        private System.Windows.Forms.Label ctlEditLabel = null;
        private Color _SaveExprBackColor = Color.LightGray;

        private Oranikle.Studio.Controls.StyledButton bTable;
        private Oranikle.Studio.Controls.StyledButton bLine;
        private Oranikle.Studio.Controls.StyledButton bImage;
        private Oranikle.Studio.Controls.StyledButton bRectangle;
        private Oranikle.Studio.Controls.StyledButton bSubreport;
        private Oranikle.Studio.Controls.StyledButton bList;
        private Oranikle.Studio.Controls.StyledButton bChart;
        private Oranikle.Studio.Controls.StyledButton bText;
        private Oranikle.Studio.Controls.StyledButton bMatrix;
        private Oranikle.Studio.Controls.StyledButton bPrint;
        private Oranikle.Studio.Controls.StyledButton bSave;
        private Oranikle.Studio.Controls.StyledButton bOpen;
        private Oranikle.Studio.Controls.StyledButton bPaste;
        private Oranikle.Studio.Controls.StyledButton bCopy;
        private Oranikle.Studio.Controls.StyledButton bCut;
        private Oranikle.Studio.Controls.StyledButton bNew;
        private Oranikle.Studio.Controls.StyledButton bUndo;
        private Oranikle.Studio.Controls.StyledButton bXml;
        private Oranikle.Studio.Controls.StyledButton bCsv;
        private Oranikle.Studio.Controls.StyledButton bCAlign;
        private Oranikle.Studio.Controls.StyledButton bRAlign;
        private Oranikle.Studio.Controls.StyledButton bLAlign;
        private Oranikle.Studio.Controls.StyledButton bSelectTool;
        private Oranikle.Studio.Controls.StyledButton bExcel;
        private Oranikle.Studio.Controls.StyledButton bTif;
        MenuItem menuCloseAll;
        private System.Windows.Forms.MenuStrip msDesigner;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmUserManagement;
        private System.Windows.Forms.ToolStripMenuItem tsmCloseOrganization;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmChequeBook;
        private System.Windows.Forms.ToolStripMenuItem tsmDefaultNarration;
        private System.Windows.Forms.ToolStripMenuItem tsmSalePurchaseForm;
        private System.Windows.Forms.ToolStripSeparator tssGroupbts;
        private System.Windows.Forms.ToolStripMenuItem tsmBroker;
        private System.Windows.Forms.ToolStripMenuItem tsmView;
        private System.Windows.Forms.ToolStripMenuItem tsmDesigner;
        private System.Windows.Forms.ToolStripMenuItem tsmCode;
        private System.Windows.Forms.ToolStripMenuItem tsmPreview;
        private System.Windows.Forms.ToolStripMenuItem tsmPropertyWindows;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmData;
        private System.Windows.Forms.ToolStripMenuItem tsmDataSources;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmTools;
        private System.Windows.Forms.ToolStripMenuItem tsmConfiguration;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmAboutUs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmRecentFiles;
        private System.Windows.Forms.ToolStripMenuItem toolboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmDataSet;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmSave;
        private ToolStripMenuItem tsmSaveAs;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmEmbeddedImages;
    }
}