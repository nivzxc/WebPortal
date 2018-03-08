using System;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsCATAApprover
 {

  public clsCATAApprover() { }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static bool IsApprover(string pUsername)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT  username FROM HR.Employees WHERE pstatus = '1' AND jgcode IN ('MA','MB','MC', 'VP','P','EVP','AVP') AND username=@username";
    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static bool IsEndorder1(string pUsername, string pCataCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT  endrsby1 FROM Finance.CATARequest WHERE endrsby1=@endrsby1 AND catacode=@catacode";
    cmd.Parameters.Add(new SqlParameter("@endrsby1", pUsername));
    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static bool IsEndorder2(string pUsername, string pCataCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT  endrsby2 FROM Finance.CATARequest WHERE endrsby2=@endrsby2 AND catacode=@catacode";
    cmd.Parameters.Add(new SqlParameter("@endrsby2", pUsername));
    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static bool IsAuthoritary(string pUsername, string pCataCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT  authrzby1 FROM Finance.CATARequest WHERE authrzby1=@authrzby1 AND catacode=@catacode";
    cmd.Parameters.Add(new SqlParameter("@authrzby1", pUsername));
    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static bool IsCanStillApprove(string pApproverType, string pCATACode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pApproverType == "endrsby1")
    {
     cmd.CommandText = "SELECT endrstt1 FROM Finance.CATARequest WHERE catacode=@CATACode";
     cmd.Parameters.Add(new SqlParameter("@CATACode", pCATACode));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["endrstt1"].ToString() == "2") { blnReturn = true; }
      else { blnReturn = false; }
     }
    }

    if (pApproverType == "endrsby2")
    {
     cmd.CommandText = "SELECT endrstt2 FROM Finance.CATARequest WHERE catacode=@CATACode";
     cmd.Parameters.Add(new SqlParameter("@CATACode", pCATACode));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["endrstt2"].ToString() == "2") { blnReturn = true; }
      else { blnReturn = false; }
     }
    }

    if (pApproverType == "authrzby")
    {
     cmd.CommandText = "SELECT authstat FROM Finance.CATARequest WHERE catacode=@CATACode";
     cmd.Parameters.Add(new SqlParameter("@CATACode", pCATACode));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["authstat"].ToString() == "2") { blnReturn = true; }
      else { blnReturn = false; }
     }
    }

    cn.Close();
   }
   return blnReturn;
  }

  public static bool IsHave2ndEndorser(string pCATACode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT endrsby2 FROM Finance.CATARequest WHERE catacode=@catacode";
    cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     if (dr["endrsby2"].ToString().Length > 0)
     {
      blnReturn = true;
     }
     else
     {
      blnReturn = false;
     }
    }
    dr.Close();
   }
   return blnReturn;
  }

 }
}
