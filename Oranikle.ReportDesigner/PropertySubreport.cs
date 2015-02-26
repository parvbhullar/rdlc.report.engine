/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;            // need this for the properties metadata
using System.Xml;
using System.Text.RegularExpressions;
using Oranikle.Report.Engine;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace Oranikle.ReportDesigner
{
    /// <summary>
    /// PropertySubreport - The System.Drawing.Rectangle specific Properties
    /// </summary>
    
    internal class PropertySubreport : PropertyReportItem
    {
        internal PropertySubreport(DesignXmlDraw d, DesignCtl dc, List<XmlNode> ris) : base(d, dc, ris)
        {
        }
        [CategoryAttribute("Subreport"),
        Editor(typeof(PropertySubreportUIEditor), typeof(System.Drawing.Design.UITypeEditor)),
           DescriptionAttribute("The name of the subreport either a full path or a relative path.")]
        public string ReportName
        {
            get { return this.Draw.GetElementValue(this.Node, "ReportName", ""); }
            set
            {
                this.SetValue("ReportName", value);
            }
        }
        [CategoryAttribute("Subreport"),
        Editor(typeof(PropertySubreportParametersUIEditor), typeof(System.Drawing.Design.UITypeEditor)),
           DescriptionAttribute("The subreport parameter expressions.")]
        public string Parameters
        {
            get 
            { 
                XmlNode pn = this.Draw.GetNamedChildNode(this.Node, "Parameters");
                return (pn == null || pn.ChildNodes == null || pn.ChildNodes.Count == 0) ? 
                    "none defined" :
                    string.Format("{0} defined", pn.ChildNodes.Count);
            }
        }

        [CategoryAttribute("Subreport"),
           DescriptionAttribute("The name of the subreport either a full path or a relative path.")]
        public PropertyExpr NoRows
        {
            get { return new PropertyExpr(this.Draw.GetElementValue(this.Node, "NoRows", "")); }
            set
            {
                if (value.Expression == null || value.Expression.Length == 0)
                    this.RemoveValue("NoRows");
                else
                    this.SetValue("NoRows", value.Expression);
            }
        }

        [CategoryAttribute("Subreport"),
           DescriptionAttribute("When true DataSource connections in subreport will reuse parent report connections when possible.")]
        public bool MergeTransactions
        {
            get { return string.Compare(this.Draw.GetElementValue(this.Node, "MergeTransactions", "true"), "true", true)==0; }
            set
            {
                this.SetValue("MergeTransactions", value? "true": "false");
            }
        }

    }

    internal class PropertySubreportUIEditor : UITypeEditor
    {
        internal PropertySubreportUIEditor()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                        IServiceProvider provider,
                                        object value)
        {

            if ((context == null) || (provider == null))
                return base.EditValue(context, provider, value);

            // Access the Property Browser's UI display service
            IWindowsFormsEditorService editorService =
                (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (editorService == null)
                return base.EditValue(context, provider, value);

            // Create an instance of the UI editor form
            PropertySubreport pr = context.Instance as PropertySubreport;
            if (pr == null)
                return base.EditValue(context, provider, value);

            using (SingleCtlDialog scd = new SingleCtlDialog(pr.DesignCtl, pr.Draw, pr.Nodes, SingleCtlTypeEnum.SubreportCtl, null))
            {
                // Display the UI editor dialog
                if (editorService.ShowDialog(scd) == DialogResult.OK)
                {
                    // Return the new property value from the UI editor form
                    return pr.ReportName;
                }

                return base.EditValue(context, provider, value);
            }
        }
    }

    internal class PropertySubreportParametersUIEditor : UITypeEditor
    {
        internal PropertySubreportParametersUIEditor()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                        IServiceProvider provider,
                                        object value)
        {

            if ((context == null) || (provider == null))
                return base.EditValue(context, provider, value);

            // Access the Property Browser's UI display service
            IWindowsFormsEditorService editorService =
                (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (editorService == null)
                return base.EditValue(context, provider, value);

            // Create an instance of the UI editor form
            PropertySubreport pr = context.Instance as PropertySubreport;
            if (pr == null)
                return base.EditValue(context, provider, value);

            using (SingleCtlDialog scd = new SingleCtlDialog(pr.DesignCtl, pr.Draw, pr.Nodes, SingleCtlTypeEnum.SubreportCtl, null))
            {
                // Display the UI editor dialog
                if (editorService.ShowDialog(scd) == DialogResult.OK)
                {
                    // Return the new property value from the UI editor form
                    return pr.Parameters;
                }

                return base.EditValue(context, provider, value);
            }
        }
    }

}
