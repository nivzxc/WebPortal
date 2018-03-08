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
using HRMS;

namespace HRMS
{
 public class clsOBDetails : IDisposable
 {

  private string _strOBCode;
  private DateTime _dteFocusDate;
  private DateTime _dteKeyIn;
  private DateTime _dteKeyOut;
  private string _strStatus;
  private string _strUpdateBy;
  private DateTime _dteUpdateOn;

  public clsOBDetails() { }

  public string OBCode { get { return _strOBCode; } set { _strOBCode = value; } }
  public DateTime FocusDate { get { return _dteFocusDate; } set { _dteFocusDate = value; } }
  public DateTime KeyIn { get { return _dteKeyIn; } set { _dteKeyIn = value; } }
  public DateTime KeyOut { get { return _dteKeyOut; } set { _dteKeyOut = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }
  public string UpdateBy { get { return _strUpdateBy; } set { _strUpdateBy = value; } }
  public DateTime UpdateOn { get { return _dteUpdateOn; } set { _dteUpdateOn = value; } }

  public int Add()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    cn.Open();
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.OBDetails VALUES(@obcode,@focsdate,@keyin,@keyout,@pstatus,@updateby,@updateon)";
    cmd.Parameters.Add("@obcode", SqlDbType.Char, 9);
    cmd.Parameters.Add("@focsdate", SqlDbType.DateTime);
    cmd.Parameters.Add("@keyin", SqlDbType.DateTime);
    cmd.Parameters.Add("@keyout", SqlDbType.DateTime);
    cmd.Parameters.Add("@pstatus", SqlDbType.Char, 1);
    cmd.Parameters.Add("@updateby", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@updateon", SqlDbType.DateTime);
    cmd.Parameters["@obcode"].Value = _strOBCode;
    cmd.Parameters["@focsdate"].Value = _dteFocusDate;
    cmd.Parameters["@keyin"].Value = _dteKeyIn;
    cmd.Parameters["@keyout"].Value = _dteKeyOut;
    cmd.Parameters["@pstatus"].Value = _strStatus;
    cmd.Parameters["@updateby"].Value = _strUpdateBy;
    cmd.Parameters["@updateon"].Value = _dteUpdateOn;
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int UpdateStatus()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.OBDetails SET pstatus=@pstatus, updateby=@updateby, updateon=@updateon WHERE obcode=@obcode AND focsdate=@focsdate AND keyin=@keyin AND keyout=@keyout";
    cmd.Parameters.Add("@obcode", SqlDbType.Char, 9);
    cmd.Parameters.Add("@focsdate", SqlDbType.DateTime);
    cmd.Parameters.Add("@keyin", SqlDbType.DateTime);
    cmd.Parameters.Add("@keyout", SqlDbType.DateTime);
    cmd.Parameters.Add("@pstatus", SqlDbType.Char, 1);
    cmd.Parameters.Add("@updateby", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@updateon", SqlDbType.DateTime);
    cmd.Parameters["@obcode"].Value = _strOBCode;
    cmd.Parameters["@focsdate"].Value = _dteFocusDate;
    cmd.Parameters["@keyin"].Value = _dteKeyIn;
    cmd.Parameters["@keyout"].Value = _dteKeyOut;
    cmd.Parameters["@pstatus"].Value = _strStatus;
    cmd.Parameters["@updateby"].Value = _strUpdateBy;
    cmd.Parameters["@updateon"].Value = _dteUpdateOn;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DataTable DSGApproved(string pOBCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT focsdate,keyin,keyout FROM HR.OBDetails WHERE obcode='" + pOBCode + "' AND pstatus='1' ORDER BY focsdate,keyin";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTable(string pOBCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT focsdate,keyin,keyout,pstatus,updateby FROM HR.OBDetails WHERE obcode='" + pOBCode + "' ORDER BY focsdate,keyin";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetHTMLTable(string pOBCode)
  {
   string strReturn = "<table border='1'><tr><td>Focus Date</td><td>Time In</td><td>Time Out</td></tr>";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT focsdate,keyin,keyout,pstatus FROM HR.OBDetails WHERE obcode='" + pOBCode + "' ORDER BY focsdate,keyin";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
     strReturn += "<tr><td>" + clsValidator.CheckDate(dr["focsdate"].ToString()).ToString("ddd MMM dd, yyyy") + "</td><td>" + clsValidator.CheckDate(dr["keyin"].ToString()).ToString("ddd MMM dd, yyyy hh:mm tt") + "</td><td>" + clsValidator.CheckDate(dr["keyout"].ToString()).ToString("ddd MMM dd, yyyy hh:mm tt") + "</td></tr>";
    dr.Close();
   }
   strReturn += "</table>";
   return strReturn;
  }

  public static bool IsValid(string pOBCode)
  {
      bool blnReturn = false;
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          using (SqlCommand cmd = cn.CreateCommand())
          {
              cmd.CommandText = "SELECT TOP(1) obcode FROM HR.OBDetails as hrob WHERE obcode=@obcode AND (SELECT TOP(1)focsdate FROM HR.OBDetails WHERE obcode=@obcode) >= @datenow  ORDER BY focsdate ASC";
              cmd.Parameters.Add(new SqlParameter("@obcode", pOBCode));
              cmd.Parameters.Add(new SqlParameter("@datenow", DateTime.Parse(DateTime.Now.ToShortDateString() + " 12:00 AM")));
              cn.Open();
              SqlDataReader dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  blnReturn = true;
              }
          }
      }
      return blnReturn;
  }

  public static DataTable GetStartEndDate(string pObCode)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          using (SqlCommand cmd = cn.CreateCommand())
          {
              cmd.CommandText = "SELECT TOP(1) (SELECT TOP(1)focsdate FROM HR.OBDetails WHERE obcode=hrob.obcode ORDER BY focsdate ASC) AS DateStart, (SELECT TOP(1) focsdate FROM HR.OBDetails WHERE obcode=hrob.obcode ORDER BY focsdate DESC) AS DateEnd FROM HR.OBDetails AS hrob WHERE obcode=@obcode";
              cmd.Parameters.Add(new SqlParameter("@obcode", pObCode));
              cn.Open();
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              da.Fill(tblReturn);
          }
      }
      return tblReturn;
  
  }

     //Added by Gerard Bautista
     //6-4-2012 for CATA
  public static DataTable GetDSLDate(string pOBCode)
  {
      DataTable tblReturn = new DataTable();
      tblReturn.Columns.Add("pValue");
      tblReturn.Columns.Add("pText");
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT CONVERT(VARCHAR(12),focsdate,101) AS pValue, CONVERT(VARCHAR(12),focsdate,101) AS pText FROM HR.OBDetails WHERE obcode = @obcode ORDER BY focsdate ASC";
          cmd.Parameters.Add(new SqlParameter("@obcode", pOBCode));
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }
 }
}