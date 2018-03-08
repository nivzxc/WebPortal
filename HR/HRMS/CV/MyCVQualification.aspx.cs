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

public partial class HR_HRMS_CV_MyCVQualification : System.Web.UI.Page
{

 private void ClearFields()
 {
  txtQualification.Text = "";
  txtDates.Text = "";
  txtRemarks.Text = "";
 }

 private void BindQualificationList()
 {
  dgQualifications.DataSource = clsEmployeeQualification.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
  dgQualifications.DataBind();

  if (dgQualifications.Items.Count == 0)
  {
   divQualification.Visible = false;
   lblQualificationNoRec.Visible = true;
   btnQualReset.Visible = false;
   btnQualSave.Visible = false;
  }
  else
  {
   divQualification.Visible = true;
   lblQualificationNoRec.Visible = false;
   btnQualReset.Visible = true;
   btnQualSave.Visible = true;
  }
 }

 ///////// Form Events /////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
  if (!Page.IsPostBack)
  {
   BindQualificationList();
  }
 }

 protected void dgQualifications_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  HiddenField phdnQualCode = (HiddenField)e.Item.FindControl("hdnQualCode");
  clsEmployeeQualification eq = new clsEmployeeQualification(phdnQualCode.Value);
  eq.Delete();
  BindQualificationList();
 }

 protected void btnQualSave_Click(object sender, EventArgs e)
 {
  foreach (DataGridItem itm in dgQualifications.Items)
  {
   HiddenField phdnQualCode = (HiddenField)itm.FindControl("hdnQualCode");
   TextBox ptxtQualification = (TextBox)itm.FindControl("txtQualification");
   TextBox ptxtInclusiveDates = (TextBox)itm.FindControl("txtInclusiveDates");
   TextBox ptxtRemarks = (TextBox)itm.FindControl("txtRemarks");

   clsEmployeeQualification eq = new clsEmployeeQualification();
   eq.QualificationCode = phdnQualCode.Value;
   eq.Qualification = ptxtQualification.Text;
   eq.InclusiveDates = ptxtInclusiveDates.Text;
   eq.Remarks = (ptxtRemarks.Text.Length > 255 ? ptxtRemarks.Text.Substring(0, 255) : ptxtRemarks.Text);
   eq.Edit();
  }
  BindQualificationList();
 }

 protected void btnQualReset_Click(object sender, EventArgs e)
 {
  BindQualificationList();
 }

 protected void btnAdd_Click(object sender, EventArgs e)
 {
  clsEmployeeQualification eq = new clsEmployeeQualification();
  eq.Username = Request.Cookies["Speedo"]["UserName"].ToString();
  eq.Qualification = txtQualification.Text;
  eq.InclusiveDates = txtDates.Text;
  eq.Remarks = (txtRemarks.Text.Length > 255 ? txtRemarks.Text.Substring(0, 255) : txtRemarks.Text);
  eq.Add();

  ClearFields();
  BindQualificationList();
 }

}