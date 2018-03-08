using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OracleClient;
using HRMS;

public partial class Admin_HQOracleEmployeeChecking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }

        if (!IsPostBack)
        {
            LoadEmp();
        }
    }

    public void LoadEmp()
    {
        string strWrite = "";

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT  username, empnum, lastname + ', ' + firname AS name FROM         HR.Employees WHERE     (jgcode IN ('AVP', 'VP', 'P', 'EVP', 'MA', 'MB', 'MC', 'JG1', 'JG2', 'JG3', 'JG4', 'JG5')) AND (pstatus = '1') ORDER BY name";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!IsExistOracle(dr["empnum"].ToString()))
                    {
                        strWrite = strWrite + "<tr>" +
                                            "<td class='GridRows' style='font-size:small;'><a href='../Userpage/Userpage.aspx?username=" + dr["username"].ToString() + "'>" + dr["username"] + "</a>" +
                                            "<td class='GridRows'>" + dr["empnum"].ToString() + "</td>" +
                                            "<td class='GridRows'>" + dr["name"].ToString() + "</td>" +
                                            "</tr>";
                        
                    }
                }
            }
        }

        lblEMps.Text = strWrite;
        //Response.Write(strWrite);
    }

    public bool IsExistOracle(string pEmployeeNum)
    
    {
        bool blnReturn = false;
        using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
        {
            using (OracleCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT PERSON_ID FROM HR.PER_ALL_PEOPLE_F WHERE EMPLOYEE_NUMBER=:pEMPLOYEE_NUMBER";
                cmd.Parameters.Add(new OracleParameter("pEMPLOYEE_NUMBER", pEmployeeNum));
                cn.Open();
                OracleDataReader oDr = cmd.ExecuteReader();
                if (oDr.Read())
                {
                    blnReturn = true;
                }
            }
        }
        return blnReturn;
    }
}