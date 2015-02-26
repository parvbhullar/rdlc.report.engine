/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using Oranikle.Report.Engine;
using System.IO;
using System.Collections;

namespace Oranikle.Report.Engine
{
	
	///<summary>
	///The primary class to "run" a report to the supported output presentation types
	///</summary>

	public enum OutputPresentationType
	{
		HTML,
		PDF,
		XML,
		ASPHTML,
		Internal,
		MHTML,
        CSV,
        RTF,
        Excel,
        TIF,
        TIFBW,
        DMP// black and white tif
	}

	[Serializable]
	public class ProcessReport
	{
		Report r;					// report
		IStreamGen _sg;

		public ProcessReport(Report rep, IStreamGen sg)
		{
			if (rep.rl.MaxSeverity > 4)
				throw new Exception("Report has errors.  Cannot be processed.");

			r = rep;
			_sg = sg;
		}

		public ProcessReport(Report rep)
		{
			if (rep.rl.MaxSeverity > 4)
				throw new Exception("Report has errors.  Cannot be processed.");

			r = rep;
			_sg = null;
		}

		// Run the report passing the parameter values and the output
		public void Run(IDictionary parms, OutputPresentationType type)
		{
			r.RunGetData(parms);

			r.RunRender(_sg, type);

			return;
		}

	}
}
