using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;

public partial class CIS_MRCF_clsGLAccountChecking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadEmp();
        LoadEmp1();
    }

    public void LoadEmp()
    {
        string strWrite = "";

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT     (SELECT TransactionTypeName FROM Oracle.MrcfTransactionType WHERE Oracle.MrcfTransactionType.TransactionTypeCode = Oracle.MrcfGLAccount.TransactionTypeCode) AS TransactionTypeName,divicode, deptcode,(SELECT deptname FROM HR.Department WHERE (deptcode = Oracle.MrcfGLAccount.deptcode)) AS deptname, GLAccount FROM Oracle.MrcfGLAccount ORDER BY divicode";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string strStatus = "";
                    if (IsGLAccountExist(dr["GLAccount"].ToString()))
                    {
                        strStatus = "YES";

                    }
                    else
                    {
                        strStatus = "NO";
                    }

                    strWrite = strWrite + "<tr>" +
                                           "<td class='GridRows'>" + dr["TransactionTypeName"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + dr["divicode"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + dr["deptcode"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + dr["deptname"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + dr["GLAccount"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + strStatus + "</td>" +
                                           "</tr>";
                }
            }
        }

        lblEMps.Text = strWrite;
        //Response.Write(strWrite);
    }

    public void LoadEmp1()
    {
        string strWrite = "";

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT CategoryName, SubCategoryName, GLAccount FROM Oracle.MrcfItems";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string strStatus = "";
                    if (IsGLAccountExist(dr["GLAccount"].ToString()))
                    {
                        strStatus = "YES";

                    }
                    else
                    {
                        strStatus = "NO";
                    }

                    strWrite = strWrite + "<tr>" +
                                           "<td class='GridRows'>" + dr["CategoryName"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + dr["SubCategoryName"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + dr["GLAccount"].ToString() + "</td>" +
                                           "<td class='GridRows'>" + strStatus + "</td>" +
                                           "</tr>";
                }
            }
        }

        Label1.Text = strWrite;
        //Response.Write(strWrite);
    }

    public bool IsGLAccountExist(string pGLAccountCombination)
    {
        bool blnReturn = false;
        using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
        {
            using (OracleCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT CODE_COMBINATION_ID FROM GL_CODE_COMBINATIONS_KFV WHERE CONCATENATED_SEGMENTS=:pCONCATENATED_SEGMENTS";
                cmd.Parameters.Add(new OracleParameter("pCONCATENATED_SEGMENTS", pGLAccountCombination));
                cn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    blnReturn = true;
                }
            }
        }
        return blnReturn;
    }
}