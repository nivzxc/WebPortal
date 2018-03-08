using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;
using System.Configuration;

public partial class Synergy_SynergyTeamNew : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]))
            {
                Response.Redirect("~/AccessDenied.aspx");
            }


            ddlCaptain.DataSource = clsEmployee.DSLUsername();
            ddlCaptain.DataValueField = "pvalue";
            ddlCaptain.DataTextField = "ptext";
            ddlCaptain.DataBind();

            ddlViceCaptain.DataSource = clsEmployee.DSLUsername();
            ddlViceCaptain.DataValueField = "pvalue";
            ddlViceCaptain.DataTextField = "ptext";
            ddlViceCaptain.DataBind();

            btnBack.Attributes.Add("onclick", "history.back(); return false");
        }
    }

   
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Save() > 0)
        {
            Response.Redirect("Synergy.aspx");
        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
      
    }

    protected int Save()
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            cmd.CommandText = "INSERT INTO Portal.Team (Name,ColorID,ActivityID,Captain,ViceCaptain,TeamLogo,IsActive,CreatedBy,DateCreated) VALUES(@Name,@ColorID,@ActivityID,@Captain,@ViceCaptain,@TeamLogo,@IsActive,@CreatedBy,GETDATE())";
            cmd.Parameters.Add(new SqlParameter("@Name", txtTeamName.Text));
            cmd.Parameters.Add(new SqlParameter("@ColorID", "1"));
            cmd.Parameters.Add(new SqlParameter("@ActivityID", ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt()));
            cmd.Parameters.Add(new SqlParameter("@Captain", ddlCaptain.SelectedValue.ToString()));
            cmd.Parameters.Add(new SqlParameter("@ViceCaptain", ddlViceCaptain.SelectedValue.ToString()));
            cmd.Parameters.Add(new SqlParameter("@TeamLogo", "http://hq.sti.edu/UploadedFiles/Images/DefaultTeamLogo.png"));
            cmd.Parameters.Add(new SqlParameter("@IsActive", chkActive.Checked));
            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Request.Cookies["Speedo"]["UserName"]));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

        }
        return intReturn;
    }
}