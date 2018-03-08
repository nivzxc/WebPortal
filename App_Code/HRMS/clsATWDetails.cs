using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsATWDetails : IDisposable
 {
  private string _strATWDCode;
  private string _strATWCode;
  private DateTime _dteDateStart;
  private DateTime _dteDateEnd;
  private string _strReason;
  private string _strRemarks;
  private string _strStatus;

  public clsATWDetails() { }

  public string ATWDCode { get { return _strATWDCode; } set { _strATWDCode = value; } }
  public string ATWCode { get { return _strATWCode; } set { _strATWCode = value; } }
  public DateTime DateStart { get { return _dteDateStart; } set { _dteDateStart = value; } }
  public DateTime DateEnd { get { return _dteDateEnd; } set { _dteDateEnd = value; } }
  public string Reason { get { return _strReason; } set { _strReason = value; } }
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public int Insert()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    cn.Open();
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.ATWDetails VALUES(@atwdcode,@atwcode,@datestrt,@dateend,@reason,@remarks,@status)";
    cmd.Parameters.Add("@atwdcode", SqlDbType.Char, 11);
    cmd.Parameters.Add("@atwcode", SqlDbType.Char, 9);
    cmd.Parameters.Add("@datestrt", SqlDbType.DateTime);
    cmd.Parameters.Add("@dateend", SqlDbType.DateTime);
    cmd.Parameters.Add("@reason", SqlDbType.VarChar, 500);
    cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 500);
    cmd.Parameters.Add("@status", SqlDbType.Char, 1);
    cmd.Parameters["@atwdcode"].Value = _strATWDCode;
    cmd.Parameters["@atwcode"].Value = _strATWCode;
    cmd.Parameters["@datestrt"].Value = _dteDateStart;
    cmd.Parameters["@dateend"].Value = _dteDateEnd;
    cmd.Parameters["@reason"].Value = _strReason;
    cmd.Parameters["@remarks"].Value = _strRemarks;
    cmd.Parameters["@status"].Value = _strStatus;
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Update()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.ATWDetails SET remarks=@remarks, status=@status WHERE atwdcode=@atwdcode";
    cmd.Parameters.Add("@atwdcode", SqlDbType.Char, 11);
    cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 500);
    cmd.Parameters.Add("@status", SqlDbType.Char, 1);
    cmd.Parameters["@atwdcode"].Value = _strATWDCode;
    cmd.Parameters["@remarks"].Value = _strRemarks;
    cmd.Parameters["@status"].Value = _strStatus;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DataTable GetDSGSchedule(string pATWCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT atwdcode,datestrt,dateend,reason,remarks,status FROM HR.ATWDetails WHERE atwcode='" + pATWCode + "' ORDER BY datestrt";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDSGScheduleHTML(string pATWCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT atwdcode,datestrt,dateend,reason,remarks,status FROM HR.ATWDetails WHERE atwcode='" + pATWCode + "' ORDER BY datestrt,dateend";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetHTMLTable(string pATWCode)
  {
   string strReturn = "<table border='1'>" + 
                        "<tr>" +                          
                         "<td>Date Start</td>" + 
                         "<td>Date End</td>" +
                         "<td>Reason</td>" +
                         "<td>Remarks</td>" + 
                        "</tr>";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT atwdcode,datestrt,dateend,reason,remarks,status FROM HR.ATWDetails WHERE atwcode='" + pATWCode + "' ORDER BY datestrt,dateend";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     strReturn += "<tr>" +
                   "<td>" + clsValidator.CheckDate(dr["datestrt"].ToString()).ToString("MM/dd/yy hh:mm tt") + "</td>" +
                   "<td>" + clsValidator.CheckDate(dr["dateend"].ToString()).ToString("MM/dd/yy hh:mm tt") + "</td>" +
                   "<td>" + dr["reason"].ToString() + "</td>" +
                   "<td>" + dr["remarks"].ToString() + "</td>" + 
                  "</tr>";
    }
    dr.Close();
   }
   strReturn += "</table>";
   return strReturn;
  }

 }
}