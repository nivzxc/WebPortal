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
public partial class CMD_SIS_SchoolDetails : System.Web.UI.Page
{
 
	protected void Page_Load(object sender, EventArgs e)
    {
      clsSpeedo.Authenticate();

 	    if (!Page.IsPostBack)
        {
			    clsSIS.AuthenticateUser(clsSIS.SISUsers.Encoder, Request.Cookies["Speedo"]["UserName"]);

			    ddlSchoolType.DataSource = clsSchool.GetSchoolCategoryDDLDataSource().DefaultView;
			    ddlSchoolType.DataValueField = "pvalue";
			    ddlSchoolType.DataTextField = "ptext";
			    ddlSchoolType.DataBind();

			    ddlCM.DataSource = clsSchool.GetCMDDLDataSource().DefaultView;
			    ddlCM.DataValueField = "pvalue";
			    ddlCM.DataTextField = "ptext";
			    ddlCM.DataBind();

               using (clsSchool school = new clsSchool())
               {
                school.SchoolCode = Request.QueryString["schlcode"];
                school.Fill();
                txtSchlCode.Text = school.SchoolCode;
                txtSchoolName.Text = school.SchoolName;
                txtSchoolNameAlt.Text = school.SchoolNameAlt;
                ddlSchoolType.SelectedValue = school.SCatCode;
                txtAddress.Text = school.Address;
                txtTelNumber.Text = school.TelNumber;
                txtFaxNumber.Text = school.FaxNumber;
                txtCEO.Text = school.CEO;
                txtCOO.Text = school.COO;
                ddlCM.SelectedValue = school.CM;
                chkHQOwned.Checked = (school.HQOwned == "1" ? true : false);
               }
        }
    }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
          using (clsSchool school = new clsSchool())
          {
           school.SchoolCode = txtSchlCode.Text;
           school.SchoolName = txtSchoolName.Text;
           school.SchoolNameAlt = txtSchoolNameAlt.Text;
           school.SCatCode = ddlSchoolType.SelectedValue;
           school.Address = txtAddress.Text;
           school.TelNumber = txtTelNumber.Text;
           school.FaxNumber = txtFaxNumber.Text;
           school.CEO = txtCEO.Text;
           school.COO = txtCOO.Text;
           school.CM = ddlCM.SelectedValue;
           school.HQOwned = (chkHQOwned.Checked ? "1" : "0");
           school.LastUpdatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
           school.LastUpdatedDate = DateTime.Now;
           school.Update();
          }
	}

}
