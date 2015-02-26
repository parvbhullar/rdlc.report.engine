/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Drawing;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Class for defining chart layout.  For example, the plot area of a chart.
	///</summary>
	public class ChartLayout
	{
		int _Height;			// total width of layout
		int _Width;				// total height
		int _LeftMargin;		// Margins
		int _RightMargin;
		int _TopMargin;
		int _BottomMargin;
		System.Drawing.Rectangle _PlotArea;
	
		public ChartLayout(int width, int height)
		{
			_Width = width;
			_Height = height;
			_LeftMargin = _RightMargin = _TopMargin = _BottomMargin = 0;
			_PlotArea = System.Drawing.Rectangle.Empty;
		}
		
		public int Width
		{
            get { return _Width; }
		}
		public int Height
		{
            get { return _Height; }
		}
		public int LeftMargin
		{
			get { return  _LeftMargin; }
            set { _LeftMargin = value; _PlotArea = System.Drawing.Rectangle.Empty; }
		}
		public int RightMargin
		{
			get { return  _RightMargin; }
            set { _RightMargin = value; _PlotArea = System.Drawing.Rectangle.Empty; }
		}
		public int TopMargin
		{
			get { return  _TopMargin; }
            set { _TopMargin = value; _PlotArea = System.Drawing.Rectangle.Empty; }
		}
		public int BottomMargin
		{
			get { return  _BottomMargin; }
            set { _BottomMargin = value; _PlotArea = System.Drawing.Rectangle.Empty; }
		}
		public System.Drawing.Rectangle PlotArea
		{
			get 
			{ 
				if (_PlotArea == System.Drawing.Rectangle.Empty)
				{
					int w = _Width - _LeftMargin - _RightMargin;
					if (w <= 0)
						throw new Exception("Plot area width is less than or equal to 0");
					int h =_Height - _TopMargin - _BottomMargin;
					if (h <= 0)
						throw new Exception("Plot area height is less than or equal to 0");
				
					_PlotArea = new System.Drawing.Rectangle(_LeftMargin, _TopMargin, w, h); 
				}

				return _PlotArea;
			}
            set
            {
                _PlotArea = value;
            }
		}
	}
}
