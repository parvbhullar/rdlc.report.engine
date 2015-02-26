/* ====================================================================

*/

using System;
using System.Xml;
using System.Collections;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Runtime Information about a set of data; public interface to the definition
	///</summary>
	[Serializable]
	public class DataSet
	{
		Report _rpt;		//	the runtime report
	    DataSetDefn _dsd;	//  the true definition of the DataSet
	
		public DataSet(Report rpt, DataSetDefn dsd)
		{
			_rpt = rpt;
			_dsd = dsd;
		}
        public DataSetDefn DataSetDefn
        {
            get { return _dsd; }
        }

        public string GetName()
        {
            return _dsd.Name.Nm;
        }

		public void SetData(IDataReader dr)
		{
			_dsd.Query.SetData(_rpt, dr, _dsd.Fields, _dsd.Filters);		// get the data (and apply the filters
		}

		public void SetData(DataTable dt)
		{
			_dsd.Query.SetData(_rpt, dt, _dsd.Fields, _dsd.Filters);
		}

		public void SetData(XmlDocument xmlDoc)
		{
			_dsd.Query.SetData(_rpt, xmlDoc, _dsd.Fields, _dsd.Filters);
		}

		public void SetData(IEnumerable ie)
		{
			_dsd.Query.SetData(_rpt, ie, _dsd.Fields, _dsd.Filters);
		}

	}
}
