/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// The collection of embedded images in the Report.
	///</summary>
	[Serializable]
	public class EmbeddedImages : ReportLink
	{
        List<EmbeddedImage> _Items;			// list of EmbeddedImage

		public EmbeddedImages(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
            _Items = new List<EmbeddedImage>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				if (xNodeLoop.Name == "EmbeddedImage")
				{
					EmbeddedImage ei = new EmbeddedImage(r, this, xNodeLoop);
					_Items.Add(ei);
				}
				else
					this.OwnerReport.rl.LogError(4, "Unknown Report element '" + xNodeLoop.Name + "' ignored.");
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For EmbeddedImages at least one EmbeddedImage is required.");
			else
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (EmbeddedImage ei in _Items)
			{
				ei.FinalPass();
			}
			return;
		}

        public List<EmbeddedImage> Items
		{
			get { return  _Items; }
		}
	}
}
