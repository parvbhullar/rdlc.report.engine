/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Collections;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Runtime data structure representing the group hierarchy
	///</summary>
	public class MatrixCellEntry
	{
		Rows _Data;						// Rows matching this cell entry
		ReportItem _DisplayItem;		// Report item to display in report
		double _Value=double.MinValue;	// used by Charts to optimize data request
		float _XPosition;				// position of cell
		float _Height;					// height of cell
		float _Width;					// width of cell
		MatrixEntry _RowME;				// MatrixEntry for the row that made cell entry
		MatrixEntry _ColumnME;			// MatrixEntry for the column that made cell entry
		int _ColSpan;					// # of columns spanned
	
		public MatrixCellEntry(Rows d, ReportItem ri)
		{
			_Data = d;
			_DisplayItem = ri;
			_ColSpan = 1;
		}

		public Rows Data
		{
			get { return  _Data; }
		}

		public int ColSpan
		{
			get { return _ColSpan;}
			set { _ColSpan = value; }
		}

		public ReportItem DisplayItem
		{
			get { return  _DisplayItem; }
		}

		public float Height
		{
			get { return _Height; }
			set { _Height = value; }
		}

		public MatrixEntry ColumnME
		{
			get { return _ColumnME; }
			set { _ColumnME = value; }
		}

		public MatrixEntry RowME
		{
			get { return _RowME; }
			set { _RowME = value; }
		}

		public double Value
		{
			get { return  _Value; }
			set { _Value = value; }
		}

		public float Width
		{
			get { return _Width; }
			set { _Width = value; }
		}

		public float XPosition
		{
			get { return _XPosition; }
			set { _XPosition = value; }
		}
	}
}
