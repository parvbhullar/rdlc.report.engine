/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Information about how to connect to the data source.
	///</summary>
	[Serializable]
	public class ConnectionProperties : ReportLink
	{
		string _DataProvider;	// The type of the data source. This will determine
								// the syntax of the Connectstring and
								// CommandText. Supported types are SQL, OLEDB, ODBC, Oracle
		Expression _ConnectString;	// The connection string for the data source
		bool _IntegratedSecurity;	// Indicates that this data source should connected
									// to using integrated security
		string _Prompt;			// The prompt displayed to the user when
								// prompting for database credentials for this data source.
	
		public ConnectionProperties(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DataProvider=null;
			_ConnectString=null;
			_IntegratedSecurity=false;
			_Prompt=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataProvider":
						_DataProvider = xNodeLoop.InnerText;
						break;
					case "ConnectString":
						_ConnectString = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "IntegratedSecurity":
						_IntegratedSecurity = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "Prompt":
						_Prompt = xNodeLoop.InnerText;
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown ConnectionProperties element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_DataProvider == null)
				OwnerReport.rl.LogError(8, "ConnectionProperties DataProvider is required.");
			if (_ConnectString == null)
				OwnerReport.rl.LogError(8, "ConnectionProperties ConnectString is required.");
		}
		
		override public void FinalPass()
		{
			if (_ConnectString != null)
				_ConnectString.FinalPass();
			return;
		}

		public string DataProvider
		{
			get { return  _DataProvider; }
			set {  _DataProvider = value; }
		}

		public string Connectstring(Report rpt)
		{
			return _ConnectString.EvaluateString(rpt, null);
		}

		public string ConnectstringValue
		{
			get {return _ConnectString==null?null:_ConnectString.Source; }
		}

		public bool IntegratedSecurity
		{
			get { return  _IntegratedSecurity; }
			set {  _IntegratedSecurity = value; }
		}

		public string Prompt
		{
			get { return  _Prompt; }
			set {  _Prompt = value; }
		}
	}
}
