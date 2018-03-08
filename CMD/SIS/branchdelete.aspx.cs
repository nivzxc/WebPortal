using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using System.Data.SqlClient;

public partial class CMD_SIS_branchdelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString())) {
            Response.Redirect("~/AccessDenied.aspx");
        }
        if (Request.QueryString["branchcode"] != "")
        {
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM dbo.Branches WHERE branchcode = @branchcode";
                    cn.Open();
                    cmd.Parameters.Add(new SqlParameter("@branchcode", Request.QueryString["branchcode"]));
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModalDelete').modal('show');</script>", false);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "alert('branch has been successfully deleted'); window.location='" + Request.ApplicationPath + "CMD/SIS/BranchesSettings.aspx';", true);

            }
        }
    }
    private void delete()
    {
       

    }
}