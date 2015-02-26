/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// DataLabel definition and processing.
	///</summary>
	[Serializable]
	public class DataLabel : ReportLink
	{
		Style _Style;	// Defines text, border and background style
						// properties for the labels
		Expression _Value;	//(Variant) Expression for the value labels. If omitted,
						// values of in the ValueAxis are used for labels.
		bool _Visible;	// Whether the data label is displayed on the
						// chart. Defaults to False.
		DataLabelPositionEnum _Position;	// Position of the label.  Default: auto
		int _Rotation;	// Angle of rotation of the label text		
	
		public DataLabel(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Style=null;
			_Value=null;
			_Visible=false;
			_Position=DataLabelPositionEnum.Auto;
			_Rotation=0;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Style":
						_Style = new Style(r, this, xNodeLoop);
						break;
					case "Value":
						_Value = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					case "Visible":
						_Visible = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "Position":
						_Position = DataLabelPosition.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "Rotation":
						_Rotation = XmlUtil.Integer(xNodeLoop.InnerText);
						break;
					default:
						break;
				}
			}
		

		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Style != null) 
				_Style.FinalPass();
			if (_Value != null) 
				_Value.FinalPass();
			return;
		}


		public Style Style
		{
			get { return  _Style; }
			set {  _Style = value; }
		}

		public Expression Value
		{
			get { return  _Value; }
			set {  _Value = value; }
		}

		public bool Visible
		{
			get { return  _Visible; }
			set {  _Visible = value; }
		}

		public DataLabelPositionEnum Position
		{
			get { return  _Position; }
			set {  _Position = value; }
		}

		public int Rotation
		{
			get { return  _Rotation; }
			set {  _Rotation = value; }
		}
	}
}
