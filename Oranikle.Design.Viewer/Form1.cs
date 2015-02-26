using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oranikle.ReportDesigner;

namespace Oranikle.Design.Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tsmReportDesigner_Click(object sender, EventArgs e)
        {
            DesignerForm designerForm = new DesignerForm();
            designerForm.Show();
        }
    }
}
