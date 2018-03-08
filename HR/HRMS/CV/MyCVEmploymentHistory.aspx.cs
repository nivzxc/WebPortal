using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HRMS;

public partial class HR_HRMS_CV_MyCVEmploymentHistory : System.Web.UI.Page
{

 private void ClearFields()
 {
  txtPosition.Text = "";
  txtResponsibility.Text = "";
  txtInclusiveDates.Text = "";
  txtCompanyName.Text = "";
  txtCompanyAddress.Text = "";
  txtCompanyContact.Text = "";

  ddlStatus.DataSource = clsEmploymentStatus.GetDdlSource();
  ddlStatus.DataValueField = "pvalue";
  ddlStatus.DataTextField = "ptext";
  ddlStatus.DataBind();
 }

 private void BindHistoryList()
 {
  dgHistory.DataSource = clsEmployeeEmploymentHistory.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
  dgHistory.DataBind();

  foreach (DataGridItem itm in dgHistory.Items)
  {
   HiddenField phdnEsttCode = (HiddenField)itm.FindControl("hdnEsttCode");
   DropDownList pddlStatus = (DropDownList)itm.FindControl("ddlStatus");

   pddlStatus.DataSource = clsEmploymentStatus.GetDdlSource();
   pddlStatus.DataValueField = "pvalue";
   pddlStatus.DataTextField = "ptext";
   pddlStatus.DataBind();
   pddlStatus.SelectedValue = phdnEsttCode.Value;
  }

  if (dgHistory.Items.Count == 0)
  {
   divHistory.Visible = false;
   lblHistoryNoRec.Visible = true;
   btnReset.Visible = false;
   btnSave.Visible = false;
  }
  else
  {
   divHistory.Visible = true;
   lblHistoryNoRec.Visible = false;
   btnReset.Visible = true;
   btnSave.Visible = true;
  }
 }

 ///////// Form Events /////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
  if (!Page.IsPostBack)
  {
   ClearFields();
   BindHistoryList();
  }
 }

 protected void dgHistory_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  HiddenField phdnEmhsCode = (HiddenField)e.Item.FindControl("hdnEmhsCode");
  clsEmployeeEmploymentHistory eh = new clsEmployeeEmploymentHistory(phdnEmhsCode.Value);
  eh.Delete();
  BindHistoryList();
 }

 protected void btnSave_Click(object sender, EventArgs e)
 {
  foreach (DataGridItem itm in dgHistory.Items)
  {
   HiddenField phdnEmhsCode = (HiddenField)itm.FindControl("hdnEmhsCode");
   HiddenField phdnEsttCode = (HiddenField)itm.FindControl("hdnEsttCode");
   TextBox ptxtPosition = (TextBox)itm.FindControl("txtPosition");
   TextBox ptxtResponsibility = (TextBox)itm.FindControl("txtResponsibility");
   TextBox ptxtInclusiveDates = (TextBox)itm.FindControl("txtInclusiveDates");
   TextBox ptxtCompany = (TextBox)itm.FindControl("txtCompany");
   TextBox ptxtContact = (TextBox)itm.FindControl("txtContact");
   TextBox ptxtAddress = (TextBox)itm.FindControl("txtAddress");
   DropDownList pddlStatus = (DropDownList)itm.FindControl("ddlStatus");
   ptxtAddress.MaxLength = 100;

   clsEmployeeEmploymentHistory eh = new clsEmployeeEmploymentHistory();
   eh.EmploymentHistoryCode = phdnEmhsCode.Value;
   eh.Position = ptxtPosition.Text;
   eh.Responsibility = ptxtResponsibility.Text;
   eh.InclusiveDates = ptxtInclusiveDates.Text;
   eh.CompanyName = ptxtCompany.Text;
   eh.CompanyContactNumber = (ptxtContact.Text.Length > 50 ? ptxtContact.Text.Substring(0, 50) : ptxtContact.Text);
   eh.CompanyAddress = ptxtAddress.Text;
   eh.EmploymentStatusCode = pddlStatus.SelectedValue;
   eh.Edit();
  }
  BindHistoryList();
 }

 protected void btnReset_Click(object sender, EventArgs e)
 {
  BindHistoryList();
 }

 protected void btnAdd_Click(object sender, EventArgs e)
 {
  clsEmployeeEmploymentHistory eh = new clsEmployeeEmploymentHistory();
  eh.Username = Request.Cookies["Speedo"]["UserName"].ToString();
  eh.Position = txtPosition.Text;
  eh.Responsibility = txtResponsibility.Text;
  eh.InclusiveDates = txtInclusiveDates.Text;
  eh.CompanyName = txtCompanyName.Text;
  eh.CompanyContactNumber = (txtCompanyContact.Text.Length > 50 ? txtCompanyContact.Text.Substring(0, 50) : txtCompanyContact.Text);
  eh.CompanyAddress = txtCompanyAddress.Text;
  eh.EmploymentStatusCode = ddlStatus.SelectedValue;
  eh.Add();

  ClearFields();
  BindHistoryList();
 }

}