using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeTraining : IDisposable
 {
  private string _strTrainingCode;
  private string _strUsername;
  private DateTime _dteTrainingDate;
  private string _strTraining;
  private string _strDetails;
  private string _strSponsor;

  public clsEmployeeTraining() { }
  public clsEmployeeTraining(string pTrainingCode) { _strTrainingCode = pTrainingCode; }

  public string TrainingCode { get { return _strTrainingCode; } set { _strTrainingCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime TrainingDate { get { return _dteTrainingDate; } set { _dteTrainingDate = value; } }
  public string Training { get { return _strTraining; } set { _strTraining = value; } }
  public string Details { get { return _strDetails; } set { _strDetails = value; } }
  public string Sponsor { get { return _strSponsor; } set { _strSponsor = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeTraining WHERE traicode='" + _strTrainingCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _dteTrainingDate = clsValidator.CheckDate(dr["traidate"].ToString());
     _strTraining = dr["training"].ToString();
     _strDetails = dr["details"].ToString();
     _strSponsor = dr["sponsor"].ToString();
    }
    dr.Close();
   }
  }

  public int Add()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.EmployeeTraining VALUES(@traicode,@username,@traidate,@training,@details,@sponsor)";
    cmd.Parameters.Add(new SqlParameter("@traicode", GenerateCode(_strUsername)));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@traidate", _dteTrainingDate));
    cmd.Parameters.Add(new SqlParameter("@training", _strTraining));
    cmd.Parameters.Add(new SqlParameter("@details", _strDetails));
    cmd.Parameters.Add(new SqlParameter("@sponsor", _strSponsor));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Edit()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    try
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "UPDATE HR.EmployeeTraining SET traidate=@traidate, training=@training, details=@details, sponsor=@sponsor WHERE traicode=@traicode";
     cmd.Parameters.Add(new SqlParameter("@traicode", _strTrainingCode));
     cmd.Parameters.Add(new SqlParameter("@traidate", _dteTrainingDate));
     cmd.Parameters.Add(new SqlParameter("@training", _strTraining));
     cmd.Parameters.Add(new SqlParameter("@details", _strDetails));
     cmd.Parameters.Add(new SqlParameter("@sponsor", _strSponsor));
     cn.Open();
     intReturn = cmd.ExecuteNonQuery();
    }
    catch (Exception) { }
   }
   return intReturn;
  }

  public int Delete()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "DELETE FROM HR.EmployeeTraining WHERE traicode='" + _strTrainingCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  ///////// Static Members /////////

  public static DataTable GetDataTable(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeTraining WHERE username='" + pUsername + "' ORDER BY traidate";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  /////////////////////////////////
  ///////// Helper Class //////////
  /////////////////////////////////

  private static string GenerateCode(string pUsername)
  {
   string strReturn = "";
   string strEmployeeCode = clsEmployee.GetEmployeeNumber(pUsername);
   string strLastCode = "";
   int intSeed = 0;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 traicode FROM HR.EmployeeTraining WHERE username='" + pUsername + "' ORDER BY traicode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["traicode"].ToString();
    dr.Close();
   }

   if (strLastCode == "")
    strReturn = "TR" + strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastCode.Substring(strLastCode.Length - 2)) + 1;
    strReturn = "TR" + strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }

   return strReturn;
  }
 }

}
