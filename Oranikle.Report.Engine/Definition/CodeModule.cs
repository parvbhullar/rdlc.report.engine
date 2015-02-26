/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.Reflection;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// CodeModule definition and processing.
	///</summary>
	[Serializable]
	public class CodeModule : ReportLink
	{
		string _CodeModule;	// Name of the code module to load
		[NonSerialized] Assembly _LoadedAssembly=null;	// 
		[NonSerialized] bool bLoadFailed=false;
	
		public CodeModule(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_CodeModule=xNode.InnerText;
		}

		public Assembly LoadedAssembly()
		{
			if (bLoadFailed)		// We only try to load once.
				return null;

			if (_LoadedAssembly == null)
			{
				try
				{
					_LoadedAssembly = XmlUtil.AssemblyLoadFrom(_CodeModule);
				}
				catch (Exception e)
				{
					OwnerReport.rl.LogError(4, String.Format("CodeModule {0} failed to load.  {1}",
						_CodeModule, e.Message));
					bLoadFailed = true;
				}
			}
			return _LoadedAssembly;
		}

		override public void FinalPass()
		{
			return;
		}

		public string CdModule
		{
			get { return  _CodeModule; }
			set {  _CodeModule = value; }
		}
	}
}
