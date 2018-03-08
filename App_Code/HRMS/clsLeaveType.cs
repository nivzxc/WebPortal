using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsLeaveType : IDisposable
 {
  private string _strLeaveTypeCode;
  private string _strLeaveType;
  private string _strWithPay;
  private float _fltMaximumBalance;
  private string _strHasBalance;
  private string _strStatus;

  public clsLeaveType() { }

  public string LeaveTypeCode { get { return _strLeaveTypeCode; } set { _strLeaveTypeCode = value; } }
  public string LeaveType { get { return _strLeaveType; } set { _strLeaveType = value; } }
  public string WithPay { get { return _strWithPay; } set { _strWithPay = value; } }
  public float MaximumBalance { get { return _fltMaximumBalance; } set { _fltMaximumBalance = value; } }
  public string HasBalance { get { return _strHasBalance; } set { _strHasBalance = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.LeaveTypes WHERE leavtype='" + _strLeaveTypeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strLeaveTypeCode = dr["leavtype"].ToString();
     _strLeaveType = dr["ltdesc"].ToString();
     _strWithPay = dr["withpay"].ToString();
     _fltMaximumBalance = clsValidator.CheckFloat((dr["maxbal"].ToString()));
     _strHasBalance = dr["hasbal"].ToString();
     _strStatus = dr["pstatus"].ToString();
    }
    dr.Close();
   }
  }

  public int Insert()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.LeaveTypes VALUES(@leavtype,@ltdesc,@withpay,@maxbal,@hasbal,'1')";
    cmd.Parameters.Add("@leavtype", SqlDbType.Char, 3);
    cmd.Parameters.Add("@ltdesc", SqlDbType.VarChar, 50);
    cmd.Parameters.Add("@withpay", SqlDbType.Char, 1);
    cmd.Parameters.Add("@maxbal", SqlDbType.Float);
    cmd.Parameters.Add("@hasbal", SqlDbType.Char, 1);
    cmd.Parameters["@leavtype"].Value = GenerateCode();
    cmd.Parameters["@ltdesc"].Value = _strLeaveType;
    cmd.Parameters["@withpay"].Value = _strWithPay;
    cmd.Parameters["@maxbal"].Value = _fltMaximumBalance;
    cmd.Parameters["@hasbal"].Value = _strHasBalance;
    cn.Open();
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
    cmd.CommandText = "UPDATE HR.LeaveTypes SET ltdesc=@ltdesc, withpay=@withpay, maxbal=@maxbal, hasbal=@hasbal, pstatus=@pstatus WHERE leavtype=@leavtype";
    cmd.Parameters.Add("@leavtype", SqlDbType.Char, 3);
    cmd.Parameters.Add("@ltdesc", SqlDbType.VarChar, 50);
    cmd.Parameters.Add("@withpay", SqlDbType.Char, 1);
    cmd.Parameters.Add("@maxbal", SqlDbType.Float);
    cmd.Parameters.Add("@hasbal", SqlDbType.Char, 1);
    cmd.Parameters.Add("@pstatus", SqlDbType.Char, 1);
    cmd.Parameters["@leavtype"].Value = _strLeaveTypeCode;
    cmd.Parameters["@ltdesc"].Value = _strLeaveType;
    cmd.Parameters["@withpay"].Value = _strWithPay;
    cmd.Parameters["@maxbal"].Value = _fltMaximumBalance;
    cmd.Parameters["@hasbal"].Value = _strHasBalance;
    cmd.Parameters["@pstatus"].Value = _strStatus;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Delete()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "DELETE FROM HR.LeaveTypes WHERE leavtype='" + _strLeaveTypeCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////  
  //////////////////////////////////

  public static string GetDescription(string pLeaveTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ltdesc FROM HR.LeaveTypes WHERE leavtype='" + pLeaveTypeCode + "'";
    cn.Open();
    try
    { strReturn = cmd.ExecuteScalar().ToString(); }
    catch
    { }
   }
   return strReturn;
  }

  public static string GetMaxBalance(string pLeaveTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT maxbal FROM HR.LeaveTypes WHERE leavtype='" + pLeaveTypeCode + "'";
    cn.Open();
    try
    { strReturn = cmd.ExecuteScalar().ToString(); }
    catch
    { }
   }
   return strReturn;
  }

  public static string GetWithPay(string pLeaveTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT withpay FROM HR.LeaveTypes WHERE leavtype='" + pLeaveTypeCode + "'";
    cn.Open();
    try
    { strReturn = cmd.ExecuteScalar().ToString(); }
    catch
    { }
   }
   return strReturn;
  }

  public static string GetHasBalance(string pLeaveTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT hasbal FROM HR.LeaveTypes WHERE leavtype='" + pLeaveTypeCode + "'";
    cn.Open();
    try
    { strReturn = cmd.ExecuteScalar().ToString(); }
    catch
    { }
   }
   return strReturn;
  }

  public static bool IsHasBalance(string pLeaveTypeCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT hasbal FROM HR.LeaveTypes WHERE leavtype='" + pLeaveTypeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     blnReturn = dr["hasbal"].ToString() == "1";
    dr.Close();
   }
   return blnReturn;
  }

  public static bool IsWithPay(string pLeaveType)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT withpay FROM HR.LeaveTypes WHERE leavtype='" + pLeaveType + "'";
    cn.Open();
    try
    { blnReturn = (cmd.ExecuteScalar().ToString() == "1"); }
    catch
    { blnReturn = false; }
   }
   return blnReturn;
  }

  public static DataTable GetDataTable(string pOrderBy)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.LeaveTypes ORDER BY " + pOrderBy;
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetActiveLeaveTypes()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.LeaveTypes ORDER BY leavtype";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetActiveLeaveTypes(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.LeaveTypes WHERE leavtype NOT IN (SELECT leavtype FROM HR.LeaveBalance WHERE username='" + pUsername + "') ORDER BY leavtype";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DdlDs()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavtype AS pvalue, ltdesc AS ptext FROM HR.LeaveTypes WHERE pstatus='1' ORDER BY ltdesc";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DdlDsAll()
  {
   DataTable tblReturn = new DataTable();

   tblReturn.Columns.Add(new DataColumn("pvalue"));
   tblReturn.Columns.Add(new DataColumn("ptext"));
   DataRow drw = tblReturn.NewRow();
   drw["pvalue"] = "ALL";
   drw["ptext"] = "All";
   tblReturn.Rows.Add(drw);
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavtype, ltdesc FROM HR.LeaveTypes ORDER BY ltdesc";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     drw = tblReturn.NewRow();
     drw["pvalue"] = dr["leavtype"].ToString();
     drw["ptext"] = dr["ltdesc"].ToString();
     tblReturn.Rows.Add(drw);
    }
    dr.Close();
   }

   return tblReturn;
  }

  public static DataTable DdlDsAllActive()
  {
   DataTable tblReturn = new DataTable();

   tblReturn.Columns.Add(new DataColumn("pvalue"));
   tblReturn.Columns.Add(new DataColumn("ptext"));
   DataRow drw = tblReturn.NewRow();
   drw["pvalue"] = "ALL";
   drw["ptext"] = "All";
   tblReturn.Rows.Add(drw);
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavtype, ltdesc FROM HR.LeaveTypes WHERE pstatus='1' ORDER BY ltdesc";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     drw = tblReturn.NewRow();
     drw["pvalue"] = dr["leavtype"].ToString();
     drw["ptext"] = dr["ltdesc"].ToString();
     tblReturn.Rows.Add(drw);
    }
    dr.Close();
   }

   return tblReturn;
  }

  ///////////////////////////////////
  ///////// Helper Methods //////////
  ///////////////////////////////////

  public static string GenerateCode()
  {
   string strReturn = "";
   int intSeed = 0;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 leavtype FROM HR.LeaveTypes ORDER BY leavtype DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     intSeed = clsValidator.CheckInteger(dr["leavtype"].ToString());
    dr.Close();
   }

   intSeed += 1;
   strReturn = ("000" + intSeed.ToString()).Substring(intSeed.ToString().Length);

   return strReturn;
  }

  // Added by Charlie 
  // March 21, 2011
  // Get Leave Type
  public static string getLeaveType(string pLeaveDescription)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 leavtype FROM HR.LeaveTypes WHERE ltdesc like '%" + pLeaveDescription + "%' AND pstatus='1' ORDER BY leavtype DESC";
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