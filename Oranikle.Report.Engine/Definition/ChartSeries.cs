/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
				// 20022008 AJM GJL - Added Second Y axis support
using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Chart series definition and processing.
	///</summary>
	[Serializable]
	public class ChartSeries : ReportLink
	{
        String _Colour;
        DataPoints _Datapoints;	// Data points within a series
		PlotTypeEnum _PlotType;		// Indicates whether the series should be plotted
								// as a line in a Column chart. If set to auto,
								// should be plotted per the primary chart type.
								// Auto (Default) | Line	
        String _YAxis;          //Indicates if the series uses the left or right axis. GJL 140208
        bool _NoMarker;          //Indicates if the series should not show its plot markers. GJL 300508
        String _LineSize;
	
		public ChartSeries(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Datapoints=null;
			_PlotType=PlotTypeEnum.Auto;
            _YAxis = "Left";
            _NoMarker = false;
            _LineSize = "Regular";

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataPoints":
						_Datapoints = new DataPoints(r, this, xNodeLoop);
						break;
					case "PlotType":
						_PlotType = Oranikle.Report.Engine.PlotType.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
                    case "YAxis":
                    case "fyi:YAxis":
                        _YAxis = xNodeLoop.InnerText;
                        break;
                    case "NoMarker":
                    case "fyi:NoMarker":
                        _NoMarker = Boolean.Parse(xNodeLoop.InnerText);
                        break;
                    case "LineSize":
                    case "fyi:LineSize":
                        _LineSize = xNodeLoop.InnerText;
                        break;
                    case "fyi:Color":
                    case "Color":
                    case "Colour":
                        _Colour = xNodeLoop.InnerText;
                        break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown ChartSeries element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Datapoints == null)
				OwnerReport.rl.LogError(8, "ChartSeries requires the DataPoints element.");
		}
		
		override public void FinalPass()
		{
			if (_Datapoints != null)
				_Datapoints.FinalPass();
			return;
		}

		public DataPoints Datapoints
		{
			get { return  _Datapoints; }
			set {  _Datapoints = value; }
		}

		public PlotTypeEnum PlotType
		{
			get { return  _PlotType; }
			set {  _PlotType = value; }
		}

        public String Colour
        {
            get { return _Colour; }
            set { _Colour = value; }
        }

        public String YAxis
        {
            get { return _YAxis; }
            set { _YAxis = value; }
        }

        public bool NoMarker
        {
            get { return _NoMarker; }
            set { _NoMarker = value; }
        }

        public string LineSize
        {
            get { return _LineSize; }
            set { _LineSize = value; }
        }
	}
}
