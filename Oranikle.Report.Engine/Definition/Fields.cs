/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Collection of fields for a DataSet.
	///</summary>
	[Serializable]
	public class Fields : ReportLink, ICollection
	{
		IDictionary _Items;			// dictionary of items

		public Fields(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			Field f;
			if (xNode.ChildNodes.Count < 10)
				_Items = new ListDictionary();	// Hashtable is overkill for small lists
			else
				_Items = new Hashtable(xNode.ChildNodes.Count);

			// Loop thru all the child nodes
			int iCol=0;
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Field":
						f = new Field(r, this, xNodeLoop);
						f.ColumnNumber = iCol++;			// Assign the column number
						break;
					default:	
						f=null;	
						r.rl.LogError(4, "Unknown element '" + xNodeLoop.Name + "' in fields list."); 
						break;
				}
				if (f != null)
				{
					if (_Items.Contains(f.Name.Nm))
					{
						r.rl.LogError(4, "Field " + f.Name + " has duplicates."); 
					}
					else	
						_Items.Add(f.Name.Nm, f);
				}
			}
		}

		public Field this[string s]
		{
			get 
			{
				return _Items[s] as Field;
			}
		}
		
		override public void FinalPass()
		{
			foreach (Field f in _Items.Values)
			{
				f.FinalPass();
			}
			return;
		}

		public IDictionary Items
		{
			get { return  _Items; }
		}
		#region ICollection Members

		public bool IsSynchronized
		{
			get
			{
				return _Items.Values.IsSynchronized;
			}
		}

		public int Count
		{
			get
			{
				return _Items.Values.Count;
			}
		}

		public void CopyTo(Array array, int index)
		{
			_Items.Values.CopyTo(array, index);
		}

		public object SyncRoot
		{
			get
			{
				return _Items.Values.SyncRoot;
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _Items.Values.GetEnumerator();
		}

		#endregion

	}
}
