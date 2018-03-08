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

public partial class Admin_SpeedoModuleApprover : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }


        if (!Page.IsPostBack)
        {
            LoadDropDownList();

            LoadItems();

        }
    }

    protected void LoadItems()
    {
        string strReturn = "";
        string strValidate = clsModule.ValidateModule(ddlModule.SelectedValue.ToString());
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                if (strValidate == "1")
                {
                    cmd.CommandText = "SELECT mappcode,username,(SELECT lastname + ', ' + firname FROM Users.Users WHERE username=Speedo.ModuleApprover.username) AS name, applevel, pemail,rapprove,penabled FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND divicode=@divicode AND deptcode=@deptcode ORDER BY name";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@divicode", ddlDivision.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@deptcode", ddlDepartment.SelectedValue.ToString()));
                }
                else if (strValidate == "2")
                {
                    cmd.CommandText = "SELECT mappcode,username,(SELECT lastname + ', ' + firname FROM Users.Users WHERE username=Speedo.ModuleApprover.username) AS name, applevel, pemail,rapprove,penabled FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND rccode=@rccode ORDER BY name";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@rccode", ddlRC.SelectedValue.ToString()));
                }
                else if (strValidate == "3")
                {
                    cmd.CommandText = "SELECT mappcode,username,(SELECT lastname + ', ' + firname FROM Users.Users WHERE username=Speedo.ModuleApprover.username) AS name, applevel, pemail,rapprove,penabled FROM Speedo.ModuleApprover WHERE modlcode=@modlcode ORDER BY name";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                }
                else
                {
                    strReturn = "";
                }

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strReturn = strReturn + "<tr>" +
                                        "<td class='GridRows'><a href='SpeedoModuleApproverEdit.aspx?mappcode=" + dr["mappcode"].ToString() + "'>" + dr["name"].ToString() + "</a></td>" +
                                        "<td class='GridRows'>" + (dr["applevel"].ToString() == "1" ? "Dept. Head" : "Div. Head") + "</td>" +
                                        "<td class='GridRows'  align='center'>" + (dr["pemail"].ToString() == "1" ? "YES" : "NO") + "</td>" +
                                        "<td class='GridRows'  align='center'>" + (dr["rapprove"].ToString() == "1" ? "YES" : "NO") + "</td>" +
                                        "<td class='GridRows'  align='center'>" + (dr["penabled"].ToString() == "1" ? "YES" : "NO") + "</td>" +
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

    protected static bool IsExisting(string pModuleCode, string pUsername, string pAppLevel, string pDiviCode, string pRcCode, string pDeptCode)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                string strValidate = clsModule.ValidateModule(pModuleCode);
                if (strValidate == "1")
                {
                    cmd.CommandText = "SELECT  mappcode FROM  Speedo.ModuleApprover WHERE  modlcode=@modlcode AND  username=@username AND applevel=@applevel AND divicode=@divicode AND deptcode=@deptcode";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@applevel", pAppLevel));
                    cmd.Parameters.Add(new SqlParameter("@divicode", pDiviCode));
                    cmd.Parameters.Add(new SqlParameter("@deptcode", pDeptCode));
                }
                else if (strValidate == "2")
                {
                    cmd.CommandText = "SELECT  mappcode FROM  Speedo.ModuleApprover WHERE  modlcode=@modlcode AND  username=@username AND applevel=@applevel AND rccode=@rccode";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@applevel", pAppLevel));
                    cmd.Parameters.Add(new SqlParameter("@rccode", pRcCode));
                }
                else if (strValidate == "3")
                {
                    cmd.CommandText = "SELECT  mappcode FROM  Speedo.ModuleApprover WHERE  modlcode=@modlcode AND  username=@username AND applevel=@applevel";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@applevel", pAppLevel));
                }

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    blnReturn = true;
                }
                cn.Close();
            }
        }
        return blnReturn;
    }

    protected void LoadDropDownList()
    {
        DataTable tblModule = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT modlcode AS pValue, modldesc AS pText FROM Speedo.Modules WHERE  modlcat IN ('1','2','3') ORDER BY pText"; 
                //cmd.CommandText = "SELECT modlcode as pValue, modldesc as pText from Speedo.Modules order by pText"; // added by calvin feb 12, 2018
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblModule);
                cn.Close();
            }
        }
        //Drop down Module Name
        ddlModule.DataSource = tblModule;
        ddlModule.DataValueField = "pvalue";
        ddlModule.DataTextField = "ptext";
        ddlModule.DataBind();

        ddlDivision.DataSource = clsDivision.GetDdlDs();
        ddlDivision.DataValueField = "pvalue";
        ddlDivision.DataTextField = "ptext";
        ddlDivision.DataBind();

        ddlDepartment.DataSource = clsDepartment.GetDdlDs(ddlDivision.SelectedValue.ToString());
        ddlDepartment.DataValueField = "pvalue";
        ddlDepartment.DataTextField = "ptext";
        ddlDepartment.DataBind();

        ddlRC.DataSource = clsRC.GetDdlDs();
        ddlRC.DataValueField = "pValue";
        ddlRC.DataTextField = "pText";
        ddlRC.DataBind();

        ddlName.DataSource = clsEmployee.DSLEmployeeList();
        ddlName.DataValueField = "pvalue";
        ddlName.DataTextField = "ptext";
        ddlName.DataBind();

        CheckVisible();
    }

    protected void CheckVisible()
    {
        string strValidate = clsModule.ValidateModule(ddlModule.SelectedValue.ToString());
        if (strValidate == "1")
        {
            trDepartment.Visible = true;
            trDivision.Visible = true;
            trRc.Visible = false;
        }
        else if (strValidate == "2")
        {
            trDepartment.Visible = false;
            trDivision.Visible = false;
            trRc.Visible = true;
        }
        else if (strValidate == "3")
        {
            trDepartment.Visible = false;
            trDivision.Visible = false;
            trRc.Visible = false;
        }
    }

    protected void LoadDDLDepartment()
    {
        ddlDepartment.DataSource = clsDepartment.GetDdlDs(ddlDivision.SelectedValue.ToString());
        ddlDepartment.DataValueField = "pvalue";
        ddlDepartment.DataTextField = "ptext";
        ddlDepartment.DataBind();

        LoadItems();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (IsExisting(ddlModule.SelectedValue.ToString(), ddlName.SelectedValue.ToString(), (ddlLevel.SelectedValue.ToString() == "Group Head" ? "1" : "2"), ddlDivision.SelectedValue.ToString(), ddlRC.SelectedValue,ddlDepartment.SelectedValue.ToString()) == false)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        //        {
        //            using (SqlCommand cmd = cn.CreateCommand())
        //            {
        //                cmd.CommandText = "INSERT INTO Speedo.ModuleApprover VALUES(@modulecode,@username,@applevel,@divicode,Null,@deptcode,@flagemail,@flagapprove,@flagenable,@createby,@createon,@modifyby,@modifyon)";
        //                cmd.Parameters.Add(new SqlParameter("@modulecode", ddlModule.SelectedValue.ToString()));
        //                cmd.Parameters.Add(new SqlParameter("@username", ddlName.SelectedValue.ToString()));
        //                cmd.Parameters.Add(new SqlParameter("@applevel", (ddlLevel.SelectedValue.ToString() == "Group Head" ? "1" : "2")));
        //                cmd.Parameters.Add(new SqlParameter("@divicode", ddlDivision.SelectedValue.ToString()));
        //                cmd.Parameters.Add(new SqlParameter("@deptcode", ddlDepartment.SelectedValue.ToString()));
        //                cmd.Parameters.Add(new SqlParameter("@flagemail", (chkbEmail.Checked == true ? "1" : "0")));
        //                cmd.Parameters.Add(new SqlParameter("@flagapprove", (chkbApprove.Checked == true ? "1" : "0")));
        //                cmd.Parameters.Add(new SqlParameter("@flagenable", (chkbEnable.Checked == true ? "1" : "0")));
        //                cmd.Parameters.Add(new SqlParameter("@createby", "progra.mer"));
        //                cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now));
        //                cmd.Parameters.Add(new SqlParameter("@modifyby", "progra.mer"));
        //                cmd.Parameters.Add(new SqlParameter("@modifyon", DateTime.Now));
        //                cn.Open();
        //                cmd.ExecuteNonQuery();
        //                LoadItems();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        divError.Visible = true;
        //        lblErrMsg.Text = "Error";
        //    }
        //}
        //else
        //{
        //    divError.Visible = true;
        //    lblErrMsg.Text = "User already exist.";
        //}
        if (!CheckExisting(ddlModule.SelectedValue.ToString(), ddlName.SelectedValue.ToString(), ddlDivision.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), ddlRC.SelectedValue.ToString()))
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"].ToString();
            string strValidate = clsModule.ValidateModule(ddlModule.SelectedValue.ToString());
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    if (strValidate == "1")
                    {
                        cmd.CommandText = "INSERT INTO Speedo.ModuleApprover VALUES(@modlcode,@username,@applevel,@divicode,Null,@deptcode,@flagemail,@flagapprove,@flagenable,@createby,@createon,@modifyby,@modifyon)";
                        cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@divicode", ddlDivision.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@deptcode", ddlDepartment.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@username", ddlName.SelectedValue.ToString()));
                    }
                    else if (strValidate == "2")
                    {
                        cmd.CommandText = "INSERT INTO Speedo.ModuleApprover VALUES(@modlcode,@username,@applevel,@divicode,@rccode,Null,@flagemail,@flagapprove,@flagenable,@createby,@createon,@modifyby,@modifyon)";
                        cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@rccode", ddlRC.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@divicode", ddlDivision.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@username", ddlName.SelectedValue.ToString()));
                    }
                    else if (strValidate == "3")
                    {
                        cmd.CommandText = "INSERT INTO Speedo.ModuleApprover VALUES(@modlcode,@username,@applevel,Null,Null,Null,@flagemail,@flagapprove,@flagenable,@createby,@createon,@modifyby,@modifyon)";
                        cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@username", ddlName.SelectedValue.ToString()));
                    }

                    cmd.Parameters.Add(new SqlParameter("@applevel", (ddlLevel.SelectedValue.ToString() == "Group Head" ? "1" : "2")));
                    cmd.Parameters.Add(new SqlParameter("@flagemail", (chkbEmail.Checked == true ? "1" : "0")));
                    cmd.Parameters.Add(new SqlParameter("@flagapprove", (chkbApprove.Checked == true ? "1" : "0")));
                    cmd.Parameters.Add(new SqlParameter("@flagenable", (chkbEnable.Checked == true ? "1" : "0")));
                    cmd.Parameters.Add(new SqlParameter("@createby", strUsername));
                    cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@modifyby", strUsername));
                    cmd.Parameters.Add(new SqlParameter("@modifyon", DateTime.Now));
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    LoadItems();
                }
            }
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = "User already exist.";
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckVisible();
        LoadDDLDepartment();
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    lblItems.Visible = true;
    //    lblItems.Text = LoadItems();
    //}

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckVisible();
        LoadItems();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckVisible();
        LoadItems();
    }

    protected bool CheckExisting(string pModuleCode, string pUsername, string pDivision, string pDepartment, string pRCCode)
    {
        bool blnReturn = false;

        string strUsername = ddlName.SelectedValue.ToString();
        string strValidate = clsModule.ValidateModule(ddlModule.SelectedValue.ToString());
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                if (strValidate == "1")
                {
                    cmd.CommandText = "SELECT mappcode FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND divicode=@divicode AND deptcode=@deptcode AND username=@username AND applevel=@applevel";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@divicode", ddlDivision.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@deptcode", ddlDepartment.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@username", ddlName.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@applevel", (ddlLevel.SelectedValue.ToString() == "Group Head" ? "1" : "2")));
                }
                else if (strValidate == "2")
                {
                    cmd.CommandText = "SELECT mappcode FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND rccode=@rccode AND username=@username AND applevel=@applevel";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@rccode", ddlRC.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@username", ddlName.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@applevel", (ddlLevel.SelectedValue.ToString() == "Group Head" ? "1" : "2")));
                }
                else if (strValidate == "3")
                {
                    cmd.CommandText = "SELECT mappcode FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND username=@username AND applevel=@applevel";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", ddlModule.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@username", ddlName.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@applevel", (ddlLevel.SelectedValue.ToString() == "Group Head" ? "1" : "2")));
                }

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    blnReturn = true;
                }
                cn.Close();
                LoadItems();
            }
        }

        return blnReturn;

    }

    protected void ddlRC_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckVisible();
        LoadItems();
    }
}