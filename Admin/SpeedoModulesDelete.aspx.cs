using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using System.Data.SqlClient;
public partial class Admin_SpeedoModulesDelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
        if (HasApprover() == false)
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Speedo.Modules WHERE modlcode=@modlcode";
                    cn.Open();
                    cmd.Parameters.Add(new SqlParameter("@modlcode", Request.QueryString["modlcode"]));
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            Response.Redirect("SpeedoModules.aspx");
        }

        else
        {
            lblStatus.Text = "Cannot delete. An approver has been defined. Press backspace. Follow this <a href='SpeedoModules.aspx'>link</a> to return.";
        }
    }

    private bool HasApprover()
    {
        string strModuleCode = Request.QueryString["modlcode"].ToString();
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT modlcode FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND penabled='1'";
                cmd.Parameters.Add(new SqlParameter("@modlcode", strModuleCode));
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
}