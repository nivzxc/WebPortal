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

public partial class HR_HRMS_CV_MyCVResearch : System.Web.UI.Page
{

 private void ClearFields()
 {
  txtTitle.Text = "";
  txtDateCompleted.Text = "";
  txtDetails.Text = "";
 }

 private void BindResearchList()
 {
  dgResearch.DataSource = clsEmployeeResearch.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
  dgResearch.DataBind();

  if (dgResearch.Items.Count == 0)
  {
   divResearch.Visible = false;
   lblResearchNoRec.Visible = true;
   btnReset.Visible = false;
   btnSave.Visible = false;
  }
  else
  {
   divResearch.Visible = true;
   lblResearchNoRec.Visible = false;
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
   BindResearchList();
  }
 }

 protected void dgResearch_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  HiddenField phdnReseCode = (HiddenField)e.Item.FindControl("hdnReseCode");
  clsEmployeeResearch er = new clsEmployeeResearch(phdnReseCode.Value);
  er.Delete();
  BindResearchList();
 }

 protected void btnSave_Click(object sender, EventArgs e)
 {
  foreach (DataGridItem itm in dgResearch.Items)
  {
   HiddenField phdnReseCode = (HiddenField)itm.FindControl("hdnReseCode");
   TextBox ptxtTitle = (TextBox)itm.FindControl("txtTitle");
   TextBox ptxtDateCompleted = (TextBox)itm.FindControl("txtDateCompleted");
   TextBox ptxtDetails = (TextBox)itm.FindControl("txtRemarks");

   clsEmployeeResearch er = new clsEmployeeResearch();
   er.ResearchCode = phdnReseCode.Value;
   er.Title = (ptxtTitle.Text.Length > 100 ? ptxtTitle.Text.Substring(0, 100) : ptxtTitle.Text);
   er.DateCompleted = (ptxtDateCompleted.Text.Length > 50 ? ptxtDateCompleted.Text.Substring(0, 50) : ptxtDateCompleted.Text);
   er.Remarks = (ptxtDetails.Text.Length > 255 ? ptxtDetails.Text.Substring(0, 255) : ptxtDetails.Text);
   er.Edit();
  }
  BindResearchList();
 }

 protected void btnReset_Click(object sender, EventArgs e)
 {
  BindResearchList();
 }

 protected void btnAdd_Click(object sender, EventArgs e)
 {
  clsEmployeeResearch er = new clsEmployeeResearch();
  er.Username = Request.Cookies["Speedo"]["UserName"].ToString();
  er.Title = (txtTitle.Text.Length > 100 ? txtTitle.Text.Substring(0, 100) : txtTitle.Text);
  er.DateCompleted = (txtDateCompleted.Text.Length > 50 ? txtDateCompleted.Text.Substring(0, 50) : txtDateCompleted.Text);
  er.Remarks = (txtDetails.Text.Length > 255 ? txtDetails.Text.Substring(0, 255) : txtDetails.Text);
  er.Add();

  ClearFields();
  BindResearchList();
 }

}