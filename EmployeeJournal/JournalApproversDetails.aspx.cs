using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class EmployeeJournal_JournalApproversDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            LoadItems();
        }
    }


    private void LoadItems()
    {
        string strJournalApproverCode = Request.QueryString["JournalApproverCode"];
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT JournalApproverCode, (SELECT lastname + ', ' + firname + ' ' + midname FROM HR.Employees WHERE username=Portal.JournalReviewer.username) AS username, (SELECT lastname + ', ' + firname + ' ' + midname FROM HR.Employees WHERE username=Portal.JournalReviewer.happrover) AS happrover FROM Portal.JournalReviewer WHERE JournalApproverCode=@JournalApproverCode";
                cmd.Parameters.Add(new SqlParameter("@JournalApproverCode", strJournalApproverCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblReviewer.Text = dr["happrover"].ToString();
                    lblEmployee.Text = dr["username"].ToString();
                }

                cn.Close();
            }
        }
    }

    protected void tnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalApprovers.aspx");
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Portal.JournalReviewer WHERE JournalApproverCode=@JournalApproverCode";
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@JournalApproverCode", Request.QueryString["JournalApproverCode"]));
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        Response.Redirect("JournalApprovers.aspx");
    }
}