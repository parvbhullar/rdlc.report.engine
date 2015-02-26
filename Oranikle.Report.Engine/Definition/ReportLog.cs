/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// Error logging (parse and runtime) within report.
	///</summary>
	[Serializable]
	public class ReportLog
	{
		List<string> _ErrorItems=null;			// list of report items
		int _MaxSeverity=0;				// maximum severity encountered				

		public ReportLog()
		{
		}

		public ReportLog(ReportLog rl)
		{
			if (rl != null && rl.ErrorItems != null)
			{
				_MaxSeverity = rl.MaxSeverity;
                _ErrorItems = new List<string>(rl.ErrorItems);
			}
		}

		public void LogError(ReportLog rl)
		{
			if (rl.ErrorItems.Count == 0)
				return;
			LogError(rl.MaxSeverity, rl.ErrorItems);
		}

		public void LogError(int severity, string item)
		{
			if (_ErrorItems == null)			// create log if first time
                _ErrorItems = new List<string>();

			if (severity > _MaxSeverity)
				_MaxSeverity = severity;

			string msg = "Severity: " + Convert.ToString(severity) + " - " + item;

			_ErrorItems.Add(msg);

			if (severity >= 12)		
				throw new Exception(msg);		// terminate the processing

			return;
		}

		public void LogError(int severity, List<string> list)
		{
			if (_ErrorItems == null)			// create log if first time
                _ErrorItems = new List<string>();

			if (severity > _MaxSeverity)
				_MaxSeverity = severity;

			_ErrorItems.AddRange(list);

			return;
		}

		public void Reset()
		{
			_ErrorItems=null;
			if (_MaxSeverity < 8)				// we keep the severity to indicate we can't run report
				_MaxSeverity=0;
		}

        public List<string> ErrorItems
		{
			get { return  _ErrorItems; }
		}


		public int MaxSeverity
		{
			get { return  _MaxSeverity; }
			set {  _MaxSeverity = value; }
		}
	}
}
