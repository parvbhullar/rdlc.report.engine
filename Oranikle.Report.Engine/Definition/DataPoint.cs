/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// DataPoint definition and processing.
	///</summary>
	[Serializable]
	public class DataPoint : ReportLink
	{
		DataValues _DataValues;	//Data value set for the Y axis.
		DataLabel _DataLabel;	// Indicates the values should be marked with data labels.
		Action _Action;			// Action to execute.
		Style _Style;			// Defines border and background style
								// properties for the data point.
		Marker _Marker;			// Defines marker properties. Markers do
								//	not apply to data points of pie, doughnut
								//	and any stacked chart types.
		string _DataElementName;	// The name to use for the data element for
									//	this data point.
									//	Default: Name of corresponding static
									//	series or category. If there is no static
									//	series or categories, “Value”
		DataElementOutputEnum _DataElementOutput;	// Indicates whether the data point should
									// appear in a data rendering.
	
		public DataPoint(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DataValues=null;
			_DataLabel=null;
			_Action=null;
			_Style=null;
			_Marker=null;
			_DataElementName=null;
			_DataElementOutput=DataElementOutputEnum.Output;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataValues":
						_DataValues = new DataValues(r, this, xNodeLoop);
						break;
					case "DataLabel":
						_DataLabel = new DataLabel(r, this, xNodeLoop);
						break;
					case "Action":
						_Action = new Action(r, this, xNodeLoop);
						break;
					case "Style":
						_Style = new Style(r, this, xNodeLoop);
						break;
					case "Marker":
						_Marker = new Marker(r, this, xNodeLoop);
						break;
					case "DataElementName":
						_DataElementName = xNodeLoop.InnerText;
						break;
					case "DataElementOutput":
						_DataElementOutput = Oranikle.Report.Engine.DataElementOutput.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown DataPoint element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_DataValues == null)
				OwnerReport.rl.LogError(8, "DataPoint requires the DataValues element.");
		}
		
		override public void FinalPass()
		{
			if (_DataValues != null)
				_DataValues.FinalPass();
			if (_DataLabel != null)
				_DataLabel.FinalPass();
			if (_Action != null)
				_Action.FinalPass();
			if (_Style != null)
				_Style.FinalPass();
			if (_Marker != null)
				_Marker.FinalPass();
			return;
		}


		public DataValues DataValues
		{
			get { return  _DataValues; }
			set {  _DataValues = value; }
		}

		public DataLabel DataLabel
		{
			get { return  _DataLabel; }
			set {  _DataLabel = value; }
		}

		public Action Action
		{
			get { return  _Action; }
			set {  _Action = value; }
		}

		public Style Style
		{
			get { return  _Style; }
			set {  _Style = value; }
		}

		public Marker Marker
		{
			get { return  _Marker; }
			set {  _Marker = value; }
		}

		public string DataElementName
		{
			get { return  _DataElementName; }
			set {  _DataElementName = value; }
		}

		public DataElementOutputEnum DataElementOutput
		{
			get { return  _DataElementOutput; }
			set {  _DataElementOutput = value; }
		}
	}
	
}
