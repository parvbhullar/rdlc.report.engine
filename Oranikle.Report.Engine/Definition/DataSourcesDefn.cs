/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// Contains list of DataSource about how to connect to sources of data used by the DataSets.
	///</summary>
	[Serializable]
	public class DataSourcesDefn : ReportLink
	{
		ListDictionary _Items;			// list of report items

		public DataSourcesDefn(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			// Run thru the attributes
//			foreach(XmlAttribute xAttr in xNode.Attributes)
//			{
//			}
			_Items = new ListDictionary();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				if (xNodeLoop.Name == "DataSource")
				{
					DataSourceDefn ds = new DataSourceDefn(r, this, xNodeLoop);
					if (ds.Name != null)
						_Items.Add(ds.Name.Nm, ds);
				}
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For DataSources at least one DataSource is required.");
		}

		public DataSourceDefn this[string name]
		{
			get 
			{
				return _Items[name] as DataSourceDefn;
			}
		}

		public void CleanUp(Report rpt)		// closes any connections
		{
			foreach (DataSourceDefn ds in _Items.Values)
			{
				ds.CleanUp(rpt);
			}
		}
		
		override public void FinalPass()
		{
			foreach (DataSourceDefn ds in _Items.Values)
			{
				ds.FinalPass();
			}
			return;
		}

		public bool ConnectDataSources(Report rpt)
		{
			// Handle any parent connections if any	(ie we're in a subreport and want to use parent report connections
			if (rpt.ParentConnections != null && rpt.ParentConnections.Items != null)
			{	// we treat subreport merged transaction connections as set by the User 
				foreach (DataSourceDefn ds in _Items.Values)
				{
					foreach (DataSourceDefn dsp in rpt.ParentConnections.Items.Values)
					{
						if (ds.AreSameDataSource(dsp))
						{
							ds.SetUserConnection(rpt, dsp.GetConnection(rpt));
							break;
						}
					}
				}
			}

			foreach (DataSourceDefn ds in _Items.Values)
			{
				ds.ConnectDataSource(rpt);
			}
			return true;
		}


		public ListDictionary Items
		{
			get { return  _Items; }
		}
	}
}
