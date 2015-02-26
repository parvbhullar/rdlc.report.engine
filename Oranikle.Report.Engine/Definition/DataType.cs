/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	///<summary>
	///Data types
	///</summary>
	public class DataType
	{
        static public Type GetStyleType(string s)
        {
            TypeCode t = GetStyle(s, (ReportDefn)null);
            return XmlUtil.GetTypeFromTypeCode(t);
        }

		static public TypeCode GetStyle(string s, ReportDefn r)
		{
			TypeCode rs;

			if (s.StartsWith("System."))
				s = s.Substring(7);

			switch (s)
			{		
				case "Boolean":
					rs = TypeCode.Boolean;
					break;
				case "DateTime":
					rs = TypeCode.DateTime;
					break;
				case "Decimal":
					rs = TypeCode.Decimal;
					break;
                case "Byte":
				case "Integer":
				case "Int16":
				case "Int32":
					rs = TypeCode.Int32;
					break;   
				case "Int64":
					rs = TypeCode.Int64;
					break;   
				case "Float":
				case "Single":
				case "Double":
					rs = TypeCode.Double;
					break;
				case "String":
				case "Char":
					rs = TypeCode.String;
					break;
				default:		// user error
					rs = TypeCode.Object;
                    if (r != null)
					    r.rl.LogError(4, string.Format("'{0}' is not a recognized type, assuming System.Object.", s));
					break;
			}
			return rs;
		}

		static public bool IsNumeric(TypeCode tc)
		{
			switch (tc)
			{
		        case TypeCode.Byte:
				case TypeCode.Int64:
				case TypeCode.Int32:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				default:		// user error
					return false;
			}
		}
	}

}
