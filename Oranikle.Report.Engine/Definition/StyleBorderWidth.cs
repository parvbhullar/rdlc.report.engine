/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.Text;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// The width of the border.  Expressions for all sides as well as default expression.
	///</summary>
	[Serializable]
	public class StyleBorderWidth : ReportLink
	{
		Expression _Default;	//(Size) Width of the border (unless overridden for a specific side)
								// Borders are centered on the edge of the object
								// Default: 1 pt Max: 20 pt Min: 0.25 pt
		Expression _Left;	//(Size) Width of the left border. Max: 20 pt Min: 0.25 pt
		Expression _Right;	//(Size) Width of the right border. Max: 20 pt Min: 0.25 pt
		Expression _Top;	//(Size) Width of the top border. Max: 20 pt Min: 0.25 pt
		Expression _Bottom;	//(Size) Width of the bottom border. Max: 20 pt Min: 0.25 pt
	
		public StyleBorderWidth(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Default=null;
			_Left=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Default":
						_Default = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "Left":
						_Left = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "Right":
						_Right = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "Top":
						_Top = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "Bottom":
						_Bottom = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown BorderWidth element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Default != null)
				_Default.FinalPass();
			if (_Left != null)
				_Left.FinalPass();
			if (_Right != null)
				_Right.FinalPass();
			if (_Top != null)
				_Top.FinalPass();
			if (_Bottom != null)
				_Bottom.FinalPass();
			return;
		}

		// Generate a CSS string from the specified styles
		public string GetCSS(Report rpt, Row row, bool bDefaults)
		{
			StringBuilder sb = new StringBuilder();

			if (_Default != null)
				sb.AppendFormat("border-width:{0};",_Default.EvaluateString(rpt, row));
			else if (bDefaults)
				sb.Append("border-width:1pt;");

			if (_Left != null)
				sb.AppendFormat("border-left-width:{0};",_Left.EvaluateString(rpt, row));

			if (_Right != null)
				sb.AppendFormat("border-right-width:{0};",_Right.EvaluateString(rpt, row));

			if (_Top != null)
				sb.AppendFormat("border-top-width:{0};",_Top.EvaluateString(rpt, row));

			if (_Bottom != null)
				sb.AppendFormat("border-bottom-width:{0};",_Bottom.EvaluateString(rpt, row));

			return sb.ToString();
		}

		public bool IsConstant()
		{
			bool rc = true;

			if (_Default != null)
				rc = _Default.IsConstant();

			if (!rc)
				return false;

			if (_Left != null)
				rc = _Left.IsConstant();

			if (!rc)
				return false;

			if (_Right != null)
				rc = _Right.IsConstant();

			if (!rc)
				return false;

			if (_Top != null)
				rc = _Top.IsConstant();

			if (!rc)
				return false;

			if (_Bottom != null)
				rc = _Bottom.IsConstant();

			return rc;
		}

		static public string GetCSSDefaults()
		{
			return "border-width:1pt;";
		}

		public Expression Default
		{
			get { return  _Default; }
			set {  _Default = value; }
		}

		public float EvalDefault(Report rpt, Row r)	// return points
		{
			if (_Default == null)
				return 1;

			string sw;
			sw = _Default.EvaluateString(rpt, r);

			RSize rs = new RSize(this.OwnerReport, sw);
			return rs.Points;
		}

		public Expression Left
		{
			get { return  _Left; }
			set {  _Left = value; }
		}

		public float EvalLeft(Report rpt, Row r)	// return points
		{
			if (_Left == null)
				return EvalDefault(rpt, r);

			string sw = _Left.EvaluateString(rpt, r);
			RSize rs = new RSize(this.OwnerReport, sw);
			return rs.Points;
		}

		public Expression Right
		{
			get { return  _Right; }
			set {  _Right = value; }
		}

		public float EvalRight(Report rpt, Row r)	// return points
		{
			if (_Right == null)
				return EvalDefault(rpt, r);

			string sw = _Right.EvaluateString(rpt, r);
			RSize rs = new RSize(this.OwnerReport, sw);
			return rs.Points;
		}

		public Expression Top
		{
			get { return  _Top; }
			set {  _Top = value; }
		}

		public float EvalTop(Report rpt, Row r)	// return points
		{
			if (_Top == null)
				return EvalDefault(rpt, r);

			string sw = _Top.EvaluateString(rpt, r);
			RSize rs = new RSize(this.OwnerReport, sw);
			return rs.Points;
		}

		public Expression Bottom
		{
			get { return  _Bottom; }
			set {  _Bottom = value; }
		}

		public float EvalBottom(Report rpt, Row r)	// return points
		{
			if (_Bottom == null)
				return EvalDefault(rpt, r);

			string sw = _Bottom.EvaluateString(rpt, r);
			RSize rs = new RSize(this.OwnerReport, sw);
			return rs.Points;
		}
	}
}
