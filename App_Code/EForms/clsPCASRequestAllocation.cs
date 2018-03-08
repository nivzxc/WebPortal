using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for clsPCASRequestAllocation
/// </summary>
public class clsPCASRequestAllocation:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    private string _strPcasCode;
    private string _strAccountExpenseCode;
    private string _strSchoolCode;
    private string _strRCCode;
    private string _strOthers;
    private double _dblAmount;

    public string PCascode { get { return _strPcasCode; } set { _strPcasCode = value; } }
    public string AccountExpenseCode { get { return _strAccountExpenseCode; } set { _strAccountExpenseCode = value; } }
    public string Schoolcode { get { return _strSchoolCode; } set { _strSchoolCode = value; } }
    public string RCCode { get { return _strRCCode; } set { _strRCCode = value; } }
    public string Others { get { return _strOthers; } set { _strOthers = value; } }
    public double Amount { get { return _dblAmount; } set { _dblAmount = value; } }

    public int Insert()
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "INSERT INTO Finance.PCASRequestAllocation values(@pcascode,@aexpcode,@schlcode,@rccode,@others,@amount)";
            cmd.Parameters.Add(new SqlParameter("@pcascode", _strPcasCode));
            cmd.Parameters.Add(new SqlParameter("@aexpcode", _strAccountExpenseCode));
            cmd.Parameters.Add(new SqlParameter("@schlcode", _strSchoolCode));
            cmd.Parameters.Add(new SqlParameter("@rccode", _strRCCode));
            cmd.Parameters.Add(new SqlParameter("@others", _strOthers));
            cmd.Parameters.Add(new SqlParameter("@amount", _dblAmount));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }

    public int Delete()
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Finance.PCASRequestAllocation WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", _strPcasCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
            }
        }
        return intReturn;
    }

    public static DataTable GetDSGMainForm(string pPcasCode)
    {
        DataTable tblReturn = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                //cmd.CommandText = "SELECT pcascode,aexpcode,(SELECT aexpname FROM Finance.AccountExpenses WHERE aexpcode=Finance.PCASRequestAllocation.aexpcode) AS aexpname,schlcode,rccode,others,amount,(SELECT CASE WHEN schlcode !='' THEN (SELECT schlname FROM CM.Schools WHERE schlcode =Finance.PCASRequestAllocation.schlcode) WHEN rccode !='' THEN (SELECT rcname FROM HR.Rc WHERE rccode=Finance.PCASRequestAllocation.rccode)  ELSE others END) AS chargeto FROM Finance.PCASRequestAllocation WHERE pcascode=@pcascode";
                cmd.CommandText = "SELECT pcascode,aexpcode,(SELECT aexpname FROM Finance.AccountExpenses WHERE aexpcode=Finance.PCASRequestAllocation.aexpcode) AS aexpname,schlcode,rccode,others,amount,(SELECT CASE WHEN schlcode='' THEN LTRIM(schlcode+rccode+others) ELSE (SELECT schlname FROM CM.Schools WHERE schlcode =Finance.PCASRequestAllocation.schlcode) END) AS chargeto FROM Finance.PCASRequestAllocation WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static double GetAmount(string pPCASCode)
    {
        double dblReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(amount) FROM Finance.PCASRequestAllocation WHERE pcascode=@pcascode";
            cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
            cn.Open();
            try
            {
                dblReturn = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch
            {

            }
        }
        return dblReturn;
    }
}