/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.Reflection;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// ReportClass represents the Class report element. 
	///</summary>
	[Serializable]
	public class ReportClass : ReportLink
	{
		string _ClassName;		// The name of the class
		Name _InstanceName;		// The name of the variable to assign the class to.
								// This variable can be used in expressions
								// throughout the report.
	
		public ReportClass(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_ClassName=null;
			_InstanceName = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ClassName":
						_ClassName = xNodeLoop.InnerText;
						break;
					case "InstanceName":
						_InstanceName = new Name(xNodeLoop.InnerText);
						break;
					default:
						break;
				}
			}
			if (_ClassName == null)
				OwnerReport.rl.LogError(8, "Class ClassName is required but not specified.");

			if (_InstanceName == null)
				OwnerReport.rl.LogError(8, "Class InstanceName is required but not specified or invalid for " + _ClassName==null? "<unknown name>": _ClassName);
		}
		
		override public void FinalPass()
		{
			return;
		}

		public object Load(Report rpt)
		{
			WorkClass wc = GetWC(rpt);
			if (wc.bCreateFailed)		// We only try to create once.
				return wc.Instance;

			if (wc.Instance != null)	// Already loaded
				return wc.Instance;

			if (OwnerReport.CodeModules == null)	// nothing to load against
				return null;

			// Load an instance of the object
			string err="";
			try
			{
				Type tp = OwnerReport.CodeModules[_ClassName];
				if (tp != null)
				{
					Assembly asm = tp.Assembly;
					wc.Instance = asm.CreateInstance(_ClassName, false);
				}
				else
					err = "Class not found.";
			}
			catch (Exception e)
			{
				wc.Instance = null;
				err = e.Message;
			}

			if (wc.Instance == null)
			{
				string e = String.Format("Unable to create instance of class {0}.  {1}",
					_ClassName, err);
				if (rpt == null)
					OwnerReport.rl.LogError(4, e);
				else
					rpt.rl.LogError(4, e);
				wc.bCreateFailed = true;
			}
			return wc.Instance;			
		}

		public string ClassName
		{
			get { return  _ClassName; }
		}

		public Name InstanceName
		{
			get { return  _InstanceName; }
		}

		public object Instance(Report rpt)
		{
			return Load(rpt);			// load if necessary
		}
		private WorkClass GetWC(Report rpt)
		{
			if (rpt == null)
				return new WorkClass();

			WorkClass wc = rpt.Cache.Get(this, "wc") as WorkClass;
			if (wc == null)
			{
				wc = new WorkClass();
				rpt.Cache.Add(this, "wc", wc);
			}
			return wc;
		}

		private void RemoveWC(Report rpt)
		{
			rpt.Cache.Remove(this, "wc");
		}

		class WorkClass
		{
			public object Instance;
			public bool bCreateFailed;
			public WorkClass()
			{
				Instance=null;	// 
				bCreateFailed=false;
			}
		}
	}
}
