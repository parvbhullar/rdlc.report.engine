/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Text;
using Oranikle.Report.Engine;

namespace Oranikle.Report.Viewer
{
	/// <summary>
	/// RdlViewerFind finds text inside of the RdlViewer control
	/// </summary>
	public class RdlViewerFind : System.Windows.Forms.UserControl
    {
        private Oranikle.Studio.Controls.StyledButton bClose;
        private Oranikle.Studio.Controls.StyledButton bFindNext;
        private Oranikle.Studio.Controls.StyledButton bFindPrevious;
        private CheckBox ckHighlightAll;
        private CheckBox ckMatchCase;
        private Label lFind;
        private Label lStatus;
        private Oranikle.Studio.Controls.CustomTextControl tbFind;
        private PageItem position = null;

        private RdlViewer _Viewer;

        public RdlViewer Viewer
        {
            get { return _Viewer; }
            set { _Viewer = value; }
        }

        public RdlViewerFind()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.bClose = new Oranikle.Studio.Controls.StyledButton();
            this.tbFind = new Oranikle.Studio.Controls.CustomTextControl();
            this.bFindNext = new Oranikle.Studio.Controls.StyledButton();
            this.bFindPrevious = new Oranikle.Studio.Controls.StyledButton();
            this.ckHighlightAll = new CheckBox();
            this.ckMatchCase = new CheckBox();
            this.lFind = new System.Windows.Forms.Label();
            this.lStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bClose
            // 
            this.bClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bClose.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bClose.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bClose.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bClose.FlatAppearance.BorderSize = 0;
            this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClose.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bClose.Location = new System.Drawing.Point(2, 4);
            this.bClose.Margin = new System.Windows.Forms.Padding(0);
            this.bClose.Name = "bClose";
            this.bClose.OverriddenSize = null;
            this.bClose.Size = new System.Drawing.Size(18, 21);
            this.bClose.TabIndex = 0;
            this.bClose.Text = "X";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // tbFind
            // 
            this.tbFind.AddX = 0;
            this.tbFind.AddY = 0;
            this.tbFind.AllowSpace = false;
            this.tbFind.BorderColor = System.Drawing.Color.LightGray;
            this.tbFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFind.ChangeVisibility = false;
            this.tbFind.ChildControl = null;
            this.tbFind.ConvertEnterToTab = true;
            this.tbFind.ConvertEnterToTabForDialogs = false;
            this.tbFind.Decimals = 0;
            this.tbFind.DisplayList = new object[0];
            this.tbFind.HitText = Oranikle.Studio.Controls.HitText.String;
            this.tbFind.Location = new System.Drawing.Point(53, 4);
            this.tbFind.Name = "tbFind";
            this.tbFind.OnDropDownCloseFocus = true;
            this.tbFind.SelectType = 0;
            this.tbFind.Size = new System.Drawing.Size(118, 20);
            this.tbFind.TabIndex = 1;
            this.tbFind.UseValueForChildsVisibilty = false;
            this.tbFind.Value = true;
            this.tbFind.TextChanged += new System.EventHandler(this.tbFind_TextChanged);
            // 
            // bFindNext
            // 
            this.bFindNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bFindNext.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bFindNext.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bFindNext.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bFindNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFindNext.Font = new System.Drawing.Font("Arial", 9F);
            this.bFindNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bFindNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFindNext.Location = new System.Drawing.Point(177, 2);
            this.bFindNext.Name = "bFindNext";
            this.bFindNext.OverriddenSize = null;
            this.bFindNext.Size = new System.Drawing.Size(61, 21);
            this.bFindNext.TabIndex = 2;
            this.bFindNext.Text = "Find Next";
            this.bFindNext.UseVisualStyleBackColor = true;
            this.bFindNext.Click += new System.EventHandler(this.bFindNext_Click);
            // 
            // bFindPrevious
            // 
            this.bFindPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bFindPrevious.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.bFindPrevious.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.bFindPrevious.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bFindPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFindPrevious.Font = new System.Drawing.Font("Arial", 9F);
            this.bFindPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.bFindPrevious.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFindPrevious.Location = new System.Drawing.Point(242, 2);
            this.bFindPrevious.Name = "bFindPrevious";
            this.bFindPrevious.OverriddenSize = null;
            this.bFindPrevious.Size = new System.Drawing.Size(82, 21);
            this.bFindPrevious.TabIndex = 3;
            this.bFindPrevious.Text = "Find Previous";
            this.bFindPrevious.UseVisualStyleBackColor = true;
            this.bFindPrevious.Click += new System.EventHandler(this.bFindPrevious_Click);
            // 
            // ckHighlightAll
            // 
            this.ckHighlightAll.AutoSize = true;
            this.ckHighlightAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckHighlightAll.ForeColor = System.Drawing.Color.Black;
            this.ckHighlightAll.Location = new System.Drawing.Point(330, 5);
            this.ckHighlightAll.Name = "ckHighlightAll";
            this.ckHighlightAll.Size = new System.Drawing.Size(78, 17);
            this.ckHighlightAll.TabIndex = 4;
            this.ckHighlightAll.Text = "Highlight All";
            this.ckHighlightAll.UseVisualStyleBackColor = true;
            this.ckHighlightAll.CheckedChanged += new System.EventHandler(this.ckHighlightAll_CheckedChanged);
            // 
            // ckMatchCase
            // 
            this.ckMatchCase.AutoSize = true;
            this.ckMatchCase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckMatchCase.ForeColor = System.Drawing.Color.Black;
            this.ckMatchCase.Location = new System.Drawing.Point(415, 6);
            this.ckMatchCase.Name = "ckMatchCase";
            this.ckMatchCase.Size = new System.Drawing.Size(80, 17);
            this.ckMatchCase.TabIndex = 5;
            this.ckMatchCase.Text = "Match Case";
            this.ckMatchCase.UseVisualStyleBackColor = true;
            this.ckMatchCase.CheckedChanged += new System.EventHandler(this.ckMatchCase_CheckedChanged);
            // 
            // lFind
            // 
            this.lFind.AutoSize = true;
            this.lFind.Location = new System.Drawing.Point(20, 7);
            this.lFind.Name = "lFind";
            this.lFind.Size = new System.Drawing.Size(30, 13);
            this.lFind.TabIndex = 6;
            this.lFind.Text = "Find:";
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.ForeColor = System.Drawing.Color.Salmon;
            this.lStatus.Location = new System.Drawing.Point(501, 7);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(0, 13);
            this.lStatus.TabIndex = 7;
            // 
            // RdlViewerFind
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.lFind);
            this.Controls.Add(this.ckMatchCase);
            this.Controls.Add(this.ckHighlightAll);
            this.Controls.Add(this.bFindPrevious);
            this.Controls.Add(this.bFindNext);
            this.Controls.Add(this.tbFind);
            this.Controls.Add(this.bClose);
            this.Name = "RdlViewerFind";
            this.Size = new System.Drawing.Size(740, 27);
            this.VisibleChanged += new System.EventHandler(this.RdlViewerFind_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void bFindNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        public void FindNext()
        {
            if (_Viewer == null)
                throw new ApplicationException("Viewer property must be set prior to issuing FindNext.");

            if (tbFind.Text.Length == 0)    // must have something to find
                return;

            RdlViewerFinds findOptions =
                ckMatchCase.Checked ?
                RdlViewerFinds.MatchCase :
                RdlViewerFinds.None;

            bool begin = position == null;
            position = _Viewer.Find(tbFind.Text, position, findOptions);
            if (position == null)
            {   
                if (!begin)     // if we didn't start from beginning already; try from beginning
                    position = _Viewer.Find(tbFind.Text, position, findOptions);

                lStatus.Text = position == null ? 
                    "Phrase not found" : "Reached end of report, continued from top";

                _Viewer.HighlightPageItem = position;
                if (position != null)
                    _Viewer.ScrollToPageItem(position);
            }
            else
            {
                lStatus.Text = "";
                _Viewer.HighlightPageItem = position;
                _Viewer.ScrollToPageItem(position);
            }
        }

        private void bFindPrevious_Click(object sender, EventArgs e)
        {
            FindPrevious();
        }

        public void FindPrevious()
        {
            if (_Viewer == null)
                throw new ApplicationException("Viewer property must be set prior to issuing FindPrevious.");

            if (tbFind.Text.Length == 0)    // must have something to find
                return;

            RdlViewerFinds findOptions = RdlViewerFinds.Backward |
                (ckMatchCase.Checked ? RdlViewerFinds.MatchCase : RdlViewerFinds.None);

            bool begin = position == null;
            position = _Viewer.Find(tbFind.Text, position, findOptions);
            if (position == null)
            {
                if (!begin)     // if we didn't start from beginning already; try from bottom
                    position = _Viewer.Find(tbFind.Text, position, findOptions);

                lStatus.Text = position == null ?
                    "Phrase not found" : "Reached top of report, continued from end";

                _Viewer.HighlightPageItem = position;
                if (position != null)
                    _Viewer.ScrollToPageItem(position);
            }
            else
            {
                lStatus.Text = "";
                _Viewer.HighlightPageItem = position;
                _Viewer.ScrollToPageItem(position);
            }
        }

        private void RdlViewerFind_VisibleChanged(object sender, EventArgs e)
        {
            lStatus.Text = "";
            if (this.Visible)
            {
                _Viewer.HighlightText = tbFind.Text;
                tbFind.Focus();
                FindNext();         // and go find the contents of the textbox
            }
            else
            {   // turn off any highlighting when find control not visible
                _Viewer.HighlightPageItem = position = null;
                _Viewer.HighlightText = null;
                _Viewer.HighlightAll = false;
                ckHighlightAll.Checked = false;
            }
        }

        private void tbFind_TextChanged(object sender, EventArgs e)
        {
            lStatus.Text = "";
            position = null;        // reset position when edit changes?? todo not really
            _Viewer.HighlightText = tbFind.Text;
            ckHighlightAll.Enabled = bFindNext.Enabled = bFindPrevious.Enabled =
                    tbFind.Text.Length > 0;
            if (tbFind.Text.Length > 0)
                FindNext();
        }

        private void ckHighlightAll_CheckedChanged(object sender, EventArgs e)
        {
            _Viewer.HighlightAll = ckHighlightAll.Checked;
        }

        private void ckMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            _Viewer.HighlightCaseSensitive = ckMatchCase.Checked;
        }
    }
}
