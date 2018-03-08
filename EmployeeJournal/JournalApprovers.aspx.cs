using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data.SqlClient;

public partial class EmployeeJournal_JournalApprovers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlReviewer.DataSource = clsModuleApprover.DSLApprover("EJS", "1");
            ddlReviewer.DataValueField = "pvalue";
            ddlReviewer.DataTextField = "ptext";
            ddlReviewer.DataBind();

            lblReviewer.Text = ddlReviewer.SelectedValue.ToString();

            ddlEmployee.DataSource = EmployeeReviewer.DSLEmployeeList();
            ddlEmployee.DataValueField = "pvalue";
            ddlEmployee.DataTextField = "ptext";
            ddlEmployee.DataBind();

            LoadItems();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strUsername = Request.Cookies["Speedo"]["UserName"].ToString();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {

                cmd.CommandText = "INSERT INTO Portal.JournalReviewer VALUES(@username,@happrover)";
                cmd.Parameters.Add(new SqlParameter("@username", ddlEmployee.SelectedValue.ToString()));
                cmd.Parameters.Add(new SqlParameter("@happrover", ddlReviewer.SelectedValue.ToString()));
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                LoadItems();


                ddlEmployee.DataSource = EmployeeReviewer.DSLEmployeeList();
                ddlEmployee.DataValueField = "pvalue";
                ddlEmployee.DataTextField = "ptext";
                ddlEmployee.DataBind();
            }
        }
    }

    protected void LoadItems()
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT JournalApproverCode,(SELECT lastname + ', ' + firname FROM Users.Users WHERE username=Portal.JournalReviewer.happrover) AS happrover,(SELECT lastname + ', ' + firname FROM Users.Users WHERE username=Portal.JournalReviewer.username) AS username, (SELECT deptname FROM HR.Department WHERE deptcode=(SELECT deptcode FROM HR.Employees WHERE username=Portal.JournalReviewer.username)) AS Deptname FROM Portal.JournalReviewer WHERE happrover=@happrover ORDER BY username";
                cmd.Parameters.Add(new SqlParameter("@happrover", ddlReviewer.SelectedValue.ToString()));

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strReturn = strReturn + "<tr>" +
                                        "<td class='GridRows'><a href='JournalApproversDetails.aspx?JournalApproverCode=" + dr["JournalApproverCode"].ToString() + "'>" + dr["username"].ToString() + "</a></td>" +
                                        "<td class='GridRows'>" + dr["deptname"].ToString() + "</td>" +
                                        "</tr>";
                }
            }
        }

        if (strReturn != string.Empty)
        {
            lblItems.Visible = true;
            lblItems.Text = strReturn;
        }
        else
        {
            lblItems.Visible = false;
        }
    }


    protected void ddlReviewer_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblReviewer.Text = ddlReviewer.SelectedValue.ToString();
        LoadItems();
    }
}