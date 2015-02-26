/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.IO;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// ChartExpression definition and processing.
	///</summary>
	[Serializable]
	public class ChartExpression : ReportItem
	{
        //Expression _Value;	// (Variant) An expression, the value of which is
        //                    // displayed in the chart
        DataValues _Values;     // all the data values
        DataPoint _DataPoint;	// The data point that generated this
        Expression _ChartLabel;    // Chart Label
        Expression _PlotType; // 05122007 AJM & GJL Added for PlotType Support
        Expression _YAxis; //140208 GJL Added for left/Right YAxis Support
        Expression _NoMarker; //30052008 GJL Added to allow lines with no markers
        Expression _LineSize;
        Expression _Colour;
		public ChartExpression(ReportDefn r, ReportLink p, XmlNode xNode):base(r,p,xNode)
		{
			_Values=null;
		
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
                    //case "Value":
                    //    _Value = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
                    //    break;

                    case "DataValues":
                        _Values = new DataValues(r, p, xNodeLoop);
                        break;
                    case "DataPoint":
						_DataPoint = (DataPoint) this.OwnerReport.LUDynamicNames[xNodeLoop.InnerText];
						break;
                    case "ChartLabel":
                        _ChartLabel = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.Variant);
                        break;
                    // 05122007AJM & GJL Added to store PlotType
                    case "PlotType":
                        _PlotType = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.Variant);
                        break;    
                    //140208 GJL Added for left/Right YAxis Support
                    case "YAxis":
                        _YAxis = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.String);
                        break;
                    case "NoMarker":
                    case "fyi:NoMarker":
                        _NoMarker = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.String);
                        break;
                    case "LineSize":
                    case "fyi:LineSize":
                        _LineSize = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.String);
                        break;
                    case "fyi:Color":
                    case "Color":
                    case "Colour":
                        _Colour = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.String);
                        break;
					default:
						if (ReportItemElement(xNodeLoop))	// try at ReportItem level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Chart element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			base.FinalPass();
			if (_Values != null)
				_Values.FinalPass();
            if (_DataPoint != null)
                _DataPoint.FinalPass();
            if (_ChartLabel != null)
                _ChartLabel.FinalPass();
            if (_PlotType != null)
                _PlotType.FinalPass();
            if (_YAxis != null)
                _YAxis.FinalPass();
            if (_NoMarker != null)
                _NoMarker.FinalPass();
            if (_LineSize != null)
                _LineSize.FinalPass();
            if (_Colour != null)
                _Colour.FinalPass();
            return;
		}

		override public void Run(IPresent ip, Row row)
		{
			return;
		}

		public Expression Value
		{
            get { return _Values != null && _Values.Items.Count > 0? _Values.Items[0].Value: null; }
		}

        public Expression Value2
        {
            get { return _Values != null && _Values.Items.Count > 1 ? _Values.Items[1].Value : null; }
        }

        public Expression Value3
        {
            get { return _Values != null && _Values.Items.Count > 2 ? _Values.Items[2].Value : null; }
        }
 
		public DataPoint DP
		{
			get { return  _DataPoint; }
		}

        public Expression ChartLabel
        {
            get {return _ChartLabel;}
        }
        // 05122007AJM & GJL Added for PlotType support
        public Expression PlotType
        {
            get { return _PlotType; }
        }
		// 20022008 AJM GJL - Added for Second Y axis support
        public Expression YAxis
        {
            get { return _YAxis; }
        }
        //30052008 GJL - Added to allow lines with no markers
        public Expression NoMarker
        {
            get { return _NoMarker; }
        }

        public Expression LineSize
        {
            get { return _LineSize; }
        }

        public Expression Colour
        {
            get { return _Colour; }
        }

	}
}
