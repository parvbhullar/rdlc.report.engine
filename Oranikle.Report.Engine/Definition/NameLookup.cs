/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Collections;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Parsing name lookup.  Fields, parameters, report items, globals, user, aggregate scopes, grouping,...
	///</summary>
	public class NameLookup
	{
		IDictionary fields;
		IDictionary parameters;
		IDictionary reportitems;
		IDictionary globals;
		IDictionary user;
		IDictionary aggrScope;
		Grouping g;					// if expression in a table group or detail group
									//   used to default aggregates to the right group
		Matrix m;					// if expression used in a Matrix
									//   used to default aggregate to the right matrix
		Classes instances;
		CodeModules cms;
		Type codeType;
		DataSetsDefn dsets;
		ReportLink _PageFooterHeader;	// when expression is in page header or footer this is set
		string _ExprName;			// name of the expression; this isn't always set

		public NameLookup(IDictionary f, IDictionary p, IDictionary r, 
			IDictionary gbl, IDictionary u, IDictionary ascope, 
			Grouping ag, Matrix mt, CodeModules cm, Classes i, DataSetsDefn ds, Type ct)
		{
			fields=f;
			parameters=p;
			reportitems=r;
			globals=gbl;
			user=u;
			aggrScope = ascope;
			g=ag;
			m = mt;
			cms = cm;
			instances = i;
			dsets = ds;
			codeType = ct;
		}

		public ReportLink PageFooterHeader
		{
			get {return _PageFooterHeader;}
			set {_PageFooterHeader = value;}
		}
		
		public bool IsPageScope
		{
			get {return _PageFooterHeader != null;}
		}

		public string ExpressionName
		{
			get {return _ExprName;}
			set {_ExprName = value;}
		}

		public IDictionary Fields
		{
			get { return fields; }
		}
		public IDictionary Parameters
		{
			get { return parameters; }
		}
		public IDictionary ReportItems
		{
			get { return reportitems; }
		}
		public IDictionary User
		{
			get { return user; }
		}
		public IDictionary Globals
		{
			get { return globals; }
		}

		public ReportClass LookupInstance(string name)
		{
			if (instances == null)
				return null;
			return instances[name];
		}

		public Field LookupField(string name)
		{	
			if (fields == null)
				return null;

			return (Field) fields[name];
		}

		public ReportParameter LookupParameter(string name)
		{	
			if (parameters == null)
				return null;
			return (ReportParameter) parameters[name];
		}

		public Textbox LookupReportItem(string name)
		{	
			if (reportitems == null)
				return null;
			return (Textbox) reportitems[name];
		}

		public IExpr LookupGlobal(string name)
		{	
			if (globals == null)
				return null;
			return (IExpr) globals[name];
		}

		public Type LookupType(string clsname)
		{
			if (cms == null || clsname == string.Empty)
				return null;
			return cms[clsname];
		}

		public CodeModules CMS
		{
			get{return cms;}
		}

		public Type CodeClassType
		{
			get{return codeType;}
		}

		public IExpr LookupUser(string name)
		{	
			if (user == null)
				return null;
			return (IExpr) user[name];
		}

		public Grouping LookupGrouping()
		{
			return g;
		}

		public Matrix LookupMatrix()
		{
			return m;
		}

		public object LookupScope(string name)
		{
			if (aggrScope == null)
				return null;
			return aggrScope[name];
		}

		public DataSetDefn ScopeDataSet(string name)
		{
			if (name == null)
			{	// Only allowed when there is only one dataset
				if (dsets.Items.Count != 1)
					return null;
                
				foreach (DataSetDefn ds in dsets.Items.Values)	// No easy way to get the item by index
					return ds;
				return null;
			}
			return dsets[name];
		}

	}
}
