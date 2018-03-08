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
using System.Net.Mail;
using System.IO;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SynergySurvey
/// </summary>
public class SynergySurvey : IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    public int Insert(DataTable pUserAnswer, string pOthers, string pUsername)
    {
        int intReturn = 0;
        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;
        string strusername = "";
        try
        {

            foreach (DataRow drUserAnswer in pUserAnswer.Rows)
            {
                cmd.CommandText = "INSERT INTO UserAnswer (username, itemcode, itemanswer) VALUES(@username, @itemcode, @itemanswer);";
                cmd.Parameters.Add(new SqlParameter("@username", drUserAnswer["username"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@itemcode", drUserAnswer["ItemCode"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@itemanswer", drUserAnswer["ItemAnswer"].ToString()));
                intReturn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }


            if (pOthers.Length > 0)
            {
                cmd.CommandText = "INSERT INTO UserOther (username, others) VALUES(@username, @others);";
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cmd.Parameters.Add(new SqlParameter("@others", pOthers));
                intReturn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            tran.Commit();
        }
        catch { tran.Rollback(); }
        finally { cn.Close(); }

       
        return intReturn;
    }

    public static bool AlreadyAnsweredSurvey(string pusername)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT username FROM UserAnswer WHERE username = @username";
                cmd.Parameters.Add(new SqlParameter("@username", pusername));
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

    public static DataTable GetEvents()
    {
        DataTable tblReturn = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT itemcode,itemdesc FROM Items ORDER BY itemcode";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
                    
            }
        }
        return tblReturn;
    }

    public static DataTable GetEmployee()
    {
        DataTable tblReturn = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT username,(SELECT CASE WHEN (isnull((SELECT DISTINCT username FROM UserAnswer WHERE username =HR.Employees.username),'No'))='No' THEN 'No' ELSE 'Yes' END) as status FROM HR.Employees WHERE pstatus='1'";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);

            }
        }
        return tblReturn;
    }

    public static DataTable GetMostPreferred()
    {
        DataTable tblReturn = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT (ROW_NUMBER() OVER (ORDER BY (SELECT COUNT(itemcode) AS PrefTotal FROM  UserAnswer WHERE itemcode = Items.itemcode AND itemanswer='P') DESC)) AS rank,itemdesc, (SELECT COUNT(itemcode) AS PrefTotal FROM  UserAnswer WHERE itemcode = Items.itemcode AND itemanswer='P') AS count FROM Items ORDER BY (SELECT COUNT(itemcode) AS PrefTotal FROM  UserAnswer WHERE itemcode = Items.itemcode AND itemanswer='P') DESC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);

            }
        }
        return tblReturn;
    }

    public static int Yes(string pGender, int pItemCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(itemcode) AS YesMale FROM  UserAnswer WHERE (SELECT gender FROM HR.Employees  WHERE username = UserAnswer.username)= @gender AND itemcode = @itemcode AND itemanswer='Y'";
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@gender", pGender));
                cmd.Parameters.Add(new SqlParameter("@itemcode", pItemCode));
                intReturn = cmd.ExecuteScalar().ToString().ToInt();
           }
        }
        return intReturn;
    }

    public static int YesTotal(int pItemCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(itemcode) as YesTotal FROM  UserAnswer WHERE itemcode = @itemcode AND itemanswer='Y'";
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@itemcode", pItemCode));
                intReturn = cmd.ExecuteScalar().ToString().ToInt();
            }
        }
        return intReturn;
    }

    public static int Preferred(string pGender, int pItemCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(itemcode) AS PrefMale FROM  UserAnswer WHERE (SELECT gender FROM HR.Employees  WHERE username = UserAnswer.username)= @gender AND itemcode = @itemcode AND itemanswer='P' ";
                cmd.Parameters.Add(new SqlParameter("@gender", pGender));
                cmd.Parameters.Add(new SqlParameter("@itemcode", pItemCode));
                cn.Open();
                intReturn = cmd.ExecuteScalar().ToString().ToInt();
            }
        }
        return intReturn;
    }

    public static int PreferredTotal(int pItemCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(itemcode) AS PrefMale FROM  UserAnswer WHERE itemcode = @itemcode AND itemanswer='P' ";
                cmd.Parameters.Add(new SqlParameter("@itemcode", pItemCode));
                cn.Open();
                intReturn = cmd.ExecuteScalar().ToString().ToInt();
            }
        }
        return intReturn;
    }

    public static int CountRespondend(string pGender)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(DISTINCT username) as usercount FROM UserAnswer WHERE (SELECT gender FROM HR.Employees WHERE username = UserAnswer.username) =@gender";
                cmd.Parameters.Add(new SqlParameter("@gender", pGender));
                cn.Open();
                intReturn = cmd.ExecuteScalar().ToString().ToInt();
            }
        }
        return intReturn;
    }

    public static string UserOthers()
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT others FROM UserOther";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strReturn += dr["others"].ToString() + "; ";
                }
            }
        }
        return strReturn;
    }

    public static bool IsSurveyEnd()
    {
        bool blnReturn = false;

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT GETDATE()";
                cn.Open();
                DateTime dtCurrentDate = Convert.ToDateTime(cmd.ExecuteScalar().ToString());

                if (dtCurrentDate > Convert.ToDateTime("03/09/2014 06:00 PM"))
                {
                    blnReturn = true;
                }
            }
        }
        return blnReturn;
    }
}