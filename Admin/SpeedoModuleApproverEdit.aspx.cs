using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using System.Data.SqlClient;

public partial class Admin_SpeedoModuleApproverEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
        if (!IsPostBack)
        {
            CheckVisible();
            LoadItems();
        }
    }

    protected void CheckVisible()
    {
        string strValidate =  clsModule.ValidateModule(clsModuleApprover.GetModule(Request.QueryString["mappcode"]));
        if (strValidate == "1")
        {
            trDepartment.Visible = true;
            trDivision.Visible = true;
            trRcCOde.Visible = false;
            
        }
        else if (strValidate == "2")
        {
            trDepartment.Visible = false;
            trDivision.Visible = false;
            trRcCOde.Visible = true;
        }
        else if (strValidate == "3")
        {
            trDepartment.Visible = false;
            trDivision.Visible = false;
            trRcCOde.Visible = false;
        }
    }

    private void LoadItems()
    {
        string strMapCode = Request.QueryString["mappcode"];
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {


                string strValidate = clsModule.ValidateModule(clsModuleApprover.GetModule(strMapCode));
                if (strValidate == "1")
                {
                    cmd.CommandText = "SELECT modlcode, (SELECT modldesc FROM Speedo.Modules WHERE modlcode = Speedo.ModuleApprover.modlcode) AS modulename, username, applevel, divicode, deptcode, pemail, rapprove, penabled FROM Speedo.ModuleApprover WHERE mappcode=@mappcode";
                    cmd.Parameters.Add(new SqlParameter("@mappcode", strMapCode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        lblLevel.Text = (dr["applevel"].ToString() == "1" ? "Dept. Head" : "Div. Head");
                        lblDepartment.Text = clsDepartment.GetName(dr["deptcode"].ToString());
                        lblDivision.Text = clsDivision.GetDivisionName(dr["divicode"].ToString());
                        lblName.Text = clsEmployee.GetName(dr["username"].ToString());
                        lblModule.Text = dr["modulename"].ToString();
                        chkbEmail.Checked = (dr["pemail"].ToString() == "1" ? true : false);
                        chkbApprove.Checked = (dr["rapprove"].ToString() == "1" ? true : false);
                        chkbEnable.Checked = (dr["pemail"].ToString() == "1" ? true : false);
                    }

                }
                else if (strValidate == "2")
                {
                    cmd.CommandText = "SELECT modlcode, (SELECT modldesc FROM Speedo.Modules WHERE modlcode = Speedo.ModuleApprover.modlcode) AS modulename, username, applevel, rccode, deptcode, pemail, rapprove, penabled FROM Speedo.ModuleApprover WHERE mappcode=@mappcode";
                    cmd.Parameters.Add(new SqlParameter("@mappcode", strMapCode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        lblLevel.Text = (dr["applevel"].ToString() == "1" ? "Dept. Head" : "Div. Head");
                        lblDepartment.Text = clsDepartment.GetName(dr["deptcode"].ToString());
                        lblRcCode.Text = clsRC.GetRCName(dr["rccode"].ToString());
                        lblName.Text = clsEmployee.GetName(dr["username"].ToString());
                        lblModule.Text = dr["modulename"].ToString();
                        chkbEmail.Checked = (dr["pemail"].ToString() == "1" ? true : false);
                        chkbApprove.Checked = (dr["rapprove"].ToString() == "1" ? true : false);
                        chkbEnable.Checked = (dr["pemail"].ToString() == "1" ? true : false);
                    }
                }
                else if (strValidate == "3")
                {
                    cmd.CommandText = "SELECT modlcode, (SELECT modldesc FROM Speedo.Modules WHERE modlcode = Speedo.ModuleApprover.modlcode) AS modulename, username, applevel, pemail, rapprove, penabled FROM Speedo.ModuleApprover WHERE mappcode=@mappcode";
                    cmd.Parameters.Add(new SqlParameter("@mappcode", strMapCode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        lblLevel.Text = (dr["applevel"].ToString() == "1" ? "Dept. Head" : "Div. Head");
                        //lblDepartment.Text = clsDepartment.GetName(dr["deptcode"].ToString());
                        //lblDivision.Text = clsDivision.GetDivisionName(dr["divicode"].ToString());
                        lblName.Text = clsEmployee.GetName(dr["username"].ToString());
                        lblModule.Text = dr["modulename"].ToString();
                        chkbEmail.Checked = (dr["pemail"].ToString() == "1" ? true : false);
                        chkbApprove.Checked = (dr["rapprove"].ToString() == "1" ? true : false);
                        chkbEnable.Checked = (dr["pemail"].ToString() == "1" ? true : false);
                    }
                }

                cn.Close();
            }
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "UPDATE Speedo.ModuleApprover SET pemail=@pemail, rapprove=@rapprove, penabled=@penabled WHERE mappcode=@mappcode";
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@mappcode", Request.QueryString["mappcode"]));
                cmd.Parameters.Add(new SqlParameter("@pemail", (chkbEmail.Checked == true ? "1" : "0")));
                cmd.Parameters.Add(new SqlParameter("@rapprove", (chkbApprove.Checked == true ? "1" : "0")));
                cmd.Parameters.Add(new SqlParameter("@penabled", (chkbEnable.Checked == true ? "1" : "0")));
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        Response.Redirect("SpeedoModuleApprover.aspx");
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Speedo.ModuleApprover WHERE mappcode=@mappcode";
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@mappcode", Request.QueryString["mappcode"]));
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        Response.Redirect("SpeedoModuleApprover.aspx");
    }
}