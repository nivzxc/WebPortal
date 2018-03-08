using System;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
   public class clsFinanceApprover
   {
      public clsFinanceApprover()
      {

      }

      public static bool IsApprover(string pUsername)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "SELECT  username FROM HR.Employees WHERE pstatus = '1' AND jgcode IN ('JG4','JG5','MA','MB','MC', 'VP','P','EVP','AVP', 'NA') AND username=@username";
            cmd.CommandText = "SELECT  username FROM HR.Employees WHERE pstatus = '1' AND (jgcode IN ('JG4','JG5','MA','MB','MC', 'VP','P','EVP','AVP', 'NA') OR username IN (SELECT username FROM Finance.PCASFPCApprover)) AND username=@username";
             cmd.Parameters.Add(new SqlParameter("@username", pUsername));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
         }
         return blnReturn;
      }

      public static bool IsEndorder1(string pUsername, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  endrsby1 FROM Finance." + pTableName + " WHERE endrsby1=@endrsby1 AND " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@endrsby1", pUsername));
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
         }
         return blnReturn;
      }

      public static bool IsEndorder2(string pUsername, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  endrsby2 FROM Finance." + pTableName + " WHERE endrsby2=@endrsby2 AND " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@endrsby2", pUsername));
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
         }
         return blnReturn;
      }

      public static bool IsAuthoritary1CATA(string pUsername, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  catacode FROM Finance." + pTableName + " WHERE autrzby1=@authrzby AND " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@authrzby", pUsername));
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
         }
         return blnReturn;
      }

      public static bool IsAuthoritary2CATA(string pUsername, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  catacode FROM Finance." + pTableName + " WHERE autrzby2=@authrzby  AND " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@authrzby", pUsername));
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
         }
         return blnReturn;
      }

      public static bool IsAuthoritary(string pUsername, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  authrzby FROM Finance." + pTableName + " WHERE authrzby=@authrzby AND " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@authrzby", pUsername));
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
         }
         return blnReturn;
      }

      public static bool IsAuthoritaryCATA(string pUsername, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  catacode FROM Finance." + pTableName + " WHERE  (autrzby1=@authrzby OR  autrzby2=@authrzby) AND " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@authrzby", pUsername));
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
         }
         return blnReturn;
      }

      public static bool IsCanStillApprove(string pApproverType, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            if (pApproverType == "endrsby1")
            {
               cmd.CommandText = "SELECT endrstt1 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
               cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
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
               cmd.CommandText = "SELECT endrstt2 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
               cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
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
               cmd.CommandText = "SELECT authstat FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
               cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
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

      public static bool IsCanStillApproveCATA(string pApproverType, string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            if (pApproverType == "endrsby1")
            {
               cmd.CommandText = "SELECT endrstt1 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
               cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
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
               cmd.CommandText = "SELECT endrstt2 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
               cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
               cn.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                  if (dr["endrstt2"].ToString() == "2") { blnReturn = true; }
                  else { blnReturn = false; }
               }
            }

            if (pApproverType == "autrzby1")
            {
               cmd.CommandText = "SELECT authstt1 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
               cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
               cn.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                  if (dr["authstt1"].ToString() == "2") { blnReturn = true; }
                  else { blnReturn = false; }
               }
            }

            if (pApproverType == "autrzby2")
            {
               cmd.CommandText = "SELECT authstt2 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
               cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
               cn.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                  if (dr["authstt2"].ToString() == "2") { blnReturn = true; }
                  else { blnReturn = false; }
               }
            }

            cn.Close();
         }
         return blnReturn;
      }

      public static bool IsHave2ndEndorser(string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT endrsby2 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
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

      public static bool IsHave2ndAuthority(string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT autrzby2 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               if (dr["autrzby2"].ToString().Length > 0)
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

      public static bool IsHaveNoEndorser(string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT endrsby1, endrsby2 FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               if (dr["endrsby2"].ToString().Trim().Length == 0 && dr["endrsby1"].ToString().Trim().Length == 0)
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

      public static bool IsCanChangeRequestStatus(string pKey, string pValue, string pTableName)
      {
         bool blnReturn = false;
         using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT statcode FROM Finance." + pTableName + " WHERE " + pKey + "=@" + pKey;
            cmd.Parameters.Add(new SqlParameter("@" + pKey, pValue));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               if (dr["statcode"].ToString() == "1")
               {
                  blnReturn = true;
               }
               else if (dr["statcode"].ToString() == "4")
               {
                   blnReturn = true;
               }
            }
            dr.Close();
         }
         return blnReturn;
      }


      public static string GetRequestStatusIcon(string pCataStatus)
      {
         string strReturn = "";
         if (pCataStatus == "0")
             strReturn = "Pending.png";
         else if (pCataStatus == "1")
             strReturn = "Approval.png";
         else if (pCataStatus == "2")
             strReturn = "Approved.png";
         else if (pCataStatus == "3")
             strReturn = "Disapproved.png";
         else if (pCataStatus == "4")
             strReturn = "Disapproved.png";
         return strReturn;
      }


   }
}
