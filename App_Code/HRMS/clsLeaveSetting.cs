using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;




namespace HRMS
{
 /// <summary>
 /// Summary description for clsLeaveSetting
 /// </summary>
 public class clsLeaveSetting
 {
  public clsLeaveSetting()
  {
   //
   // TODO: Add constructor logic here
   //
  }
  public static string GetDescription(string pLeaveTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavname FROM HR.LeaveSetting WHERE leavtype='" + pLeaveTypeCode + "'";
    cn.Open();
    try
    { strReturn = cmd.ExecuteScalar().ToString(); }
    catch
    { }
   }
   return strReturn;
  }

  public static string GetCode(string pLeaveDescription)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavtype FROM HR.LeaveSetting WHERE leavname = @leavname";
    cmd.Parameters.AddWithValue("@leavname", pLeaveDescription);
    cn.Open();
    try
    { strReturn = cmd.ExecuteScalar().ToString(); }
    catch
    { }
    }
   return strReturn;
  }
 }
}
