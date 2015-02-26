/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// TableGroup definition and processing.
	///</summary>
	[Serializable]
	public class TableGroup : ReportLink
	{
		Grouping _Grouping;		// The expressions to group the data by.
		Sorting _Sorting;		// The expressions to sort the data by.
		Header _Header;			// A group header row.
		Footer _Footer;			// A group footer row.
		Visibility _Visibility;	// Indicates if the group (and all groups embedded
								// within it) should be hidden.		
		Textbox _ToggleTextbox;	//  resolved TextBox for toggling visibility
	
		public TableGroup(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Grouping=null;
			_Sorting=null;
			_Header=null;
			_Footer=null;
			_Visibility=null;
			_ToggleTextbox=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Grouping":
						_Grouping = new Grouping(r, this, xNodeLoop);
						break;
					case "Sorting":
						_Sorting = new Sorting(r, this, xNodeLoop);
						break;
					case "Header":
						_Header = new Header(r, this, xNodeLoop);
						break;
					case "Footer":
						_Footer = new Footer(r, this, xNodeLoop);
						break;
					case "Visibility":
						_Visibility = new Visibility(r, this, xNodeLoop);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown TableGroup element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Grouping == null)
				OwnerReport.rl.LogError(8, "TableGroup requires the Grouping element.");
		}
		
		override public void FinalPass()
		{
			if (_Grouping != null)
				_Grouping.FinalPass();
			if (_Sorting != null)
				_Sorting.FinalPass();
			if (_Header != null)
				_Header.FinalPass();
			if (_Footer != null)
				_Footer.FinalPass();
			if (_Visibility != null)
			{
				_Visibility.FinalPass();
				if (_Visibility.ToggleItem != null)
				{
					_ToggleTextbox = (Textbox) (OwnerReport.LUReportItems[_Visibility.ToggleItem]);
					if (_ToggleTextbox != null)
						_ToggleTextbox.IsToggle = true;
				}
			}
			return;
		}

		public float DefnHeight()
		{
			float height=0;
			if (_Header != null)
				height += _Header.TableRows.DefnHeight();

			if (_Footer != null)
				height += _Footer.TableRows.DefnHeight();

			return height;
		}

		public Grouping Grouping
		{
			get { return  _Grouping; }
			set {  _Grouping = value; }
		}

		public Sorting Sorting
		{
			get { return  _Sorting; }
			set {  _Sorting = value; }
		}

		public Header Header
		{
			get { return  _Header; }
			set {  _Header = value; }
		}

		public int HeaderCount
		{
			get 
			{
				if (_Header == null)
					return 0;
				else
					return _Header.TableRows.Items.Count;
			}
		}

		public Footer Footer
		{
			get { return  _Footer; }
			set {  _Footer = value; }
		}

		public int FooterCount
		{
			get 
			{
				if (_Footer == null)
					return 0;
				else
					return _Footer.TableRows.Items.Count;
			}
		}

		public Visibility Visibility
		{
			get { return  _Visibility; }
			set {  _Visibility = value; }
		}

		public Textbox ToggleTextbox
		{
			get { return  _ToggleTextbox; }
		}
	}
}
