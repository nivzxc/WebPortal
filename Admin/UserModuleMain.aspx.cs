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

public partial class Admin_UserModuleMain : System.Web.UI.Page
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
        
        DataTable tblUser = clsModule.GetDSGUser(ddlModule.SelectedValue.ToString());
        foreach (DataRow drNew in tblUser.Rows)
        {
            strReturn = strReturn + "<tr>" +
                                    "<td class='GridRows'>" + drNew["name"].ToString() + "</td>" +
                                    "<td class='GridRows'><a href='UserModuleEdit.aspx?username=" + drNew["username"].ToString() + "&modlcode=" + ddlModule.SelectedValue.ToString() + "'>DELETE</a></td>" +
                                    "</tr>";
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

    protected void LoadDropDownList()
    {
        DataTable tblModule = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                //cmd.CommandText = "SELECT modlcode AS pValue, modldesc AS pText FROM Speedo.Modules WHERE  modlcat IN ('1','2','3') ORDER BY pText";
                cmd.CommandText = "SELECT modlcode AS pValue, modldesc AS pText FROM Speedo.Modules";
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

        ddlName.DataSource = clsEmployee.DSLEmployeeList();
        ddlName.DataValueField = "pvalue";
        ddlName.DataTextField = "ptext";
        ddlName.DataBind();

    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadItems();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        if (!clsModule.IsExistUserModule(ddlModule.SelectedValue.ToString(), ddlName.SelectedValue.ToString()))
        {
            clsModule.InserUserModule(ddlModule.SelectedValue.ToString(), ddlName.SelectedValue.ToString());
            LoadItems();
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = "User already exist.";
        }
    }
}