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
using HRMS;
public partial class Admin_SpeedoModules : System.Web.UI.Page
{

    protected void LoadModules()
    {
        string strWrite = "";
        int intCtr = 0;

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                //cmd.CommandText = "SELECT modlcode, modldesc FROM Speedo.Modules WHERE modlcat IN ('1','2','3') ORDER BY modldesc"; <- remove by calvin feb 9, 2017 Reason: it limit only 3 table to be fetch which if you have more than 3 tb then the first 3 tb only is being fetch 
                cmd.CommandText = "SELECT modlcode, modldesc FROM Speedo.Modules";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + dr["modlcode"].ToString() + "</td>" +
                                        "<td class='GridRows'>" + dr["modldesc"].ToString() + "</td>" +
                                        "<td class='GridRows'  align='center'><a href='SpeedoModulesDelete.aspx?modlcode=" + dr["modlcode"].ToString() + "'><img src=../Support/Disapproved.png /></a></td>" +
                                  "</tr>";
                    intCtr++;
                }
            }
        }
        

        Response.Write(strWrite);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
        if (!IsPostBack)
        { 
        
        }
    }

    private int Save()
    {
        int intReturn = 0;
        string strModuleCode = txtModuleCode.Text;
        string strModuleDescription = txtModuleName.Text;
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Speedo.Modules VALUES(@modulecode, @moduledesc,null)";
                    cmd.Parameters.Add(new SqlParameter("@modulecode", strModuleCode));
                    cmd.Parameters.Add(new SqlParameter("@moduledesc", strModuleDescription));
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    txtModuleCode.Text = "";
                    txtModuleName.Text = "";
                    intReturn = 1;
                }
            }

        }
        catch
        {

        }
        return intReturn;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save() >= 1)
        {
            Response.Redirect("SpeedoModules.aspx");
            txtModuleCode.Text = "";
            txtModuleName.Text = "";
            divSuccess.Visible = true;
            lblSuccessMsg.Visible = true;
            lblSuccessMsg.Text = txtModuleName.Text;                   
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Visible = true;
            lblErrMsg.Text = "Duplicate Module Code has been already defined in the system.";
        }
    }
}