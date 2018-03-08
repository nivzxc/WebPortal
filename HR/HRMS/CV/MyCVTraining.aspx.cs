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
using GrayMatterSoft;
using HRMS;

public partial class HR_HRMS_CV_MyCVTraining : System.Web.UI.Page
{

 private void ClearFields()
 {
  txtTraining.Text = "";
  dtpTrainingDate.Date = DateTime.Now;
  txtSponsor.Text = "";
  txtDetails.Text = "";
 }

 private void BindTrainingList()
 {
  dgTraining.DataSource = clsEmployeeTraining.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
  dgTraining.DataBind();

  if (dgTraining.Items.Count == 0)
  {
   divTraining.Visible = false;
   lblTrainingNoRec.Visible = true;
   btnReset.Visible = false;
   btnSave.Visible = false;
  }
  else
  {
   divTraining.Visible = true;
   lblTrainingNoRec.Visible = false;
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
   BindTrainingList();
  }
 }

 protected void dgTraining_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  HiddenField phdnTraiCode = (HiddenField)e.Item.FindControl("hdnTraiCode");
  clsEmployeeTraining et = new clsEmployeeTraining(phdnTraiCode.Value);
  et.Delete();
  BindTrainingList();
 }

 protected void btnSave_Click(object sender, EventArgs e)
 {
  foreach (DataGridItem itm in dgTraining.Items)
  {
   HiddenField phdnTraiCode = (HiddenField)itm.FindControl("hdnTraiCode");
   GMDatePicker pdtpTrainingDate = (GMDatePicker)itm.FindControl("dtpTrainingDate");
   TextBox ptxtTraining = (TextBox)itm.FindControl("txtTraining");
   TextBox ptxtDetails = (TextBox)itm.FindControl("txtDetails");
   TextBox ptxtSponsor = (TextBox)itm.FindControl("txtSponsor");

   clsEmployeeTraining et = new clsEmployeeTraining();
   et.TrainingCode = phdnTraiCode.Value;
   et.TrainingDate = pdtpTrainingDate.Date;
   et.Training = ptxtTraining.Text;
   et.Sponsor = (ptxtSponsor.Text.Length > 50 ? ptxtSponsor.Text.Substring(0, 50) : ptxtSponsor.Text);
   et.Details = (ptxtDetails.Text.Length > 255 ? ptxtDetails.Text.Substring(0, 255) : ptxtDetails.Text);
   et.Edit();
  }
  BindTrainingList();
 }

 protected void btnReset_Click(object sender, EventArgs e)
 {
  BindTrainingList();
 }

 protected void btnAdd_Click(object sender, EventArgs e)
 {
  clsEmployeeTraining et = new clsEmployeeTraining();
  et.Username = Request.Cookies["Speedo"]["UserName"].ToString();
  et.Training = txtTraining.Text;
  et.TrainingDate = dtpTrainingDate.Date;
  et.Sponsor = (txtSponsor.Text.Length > 50 ? txtSponsor.Text.Substring(0, 50) : txtSponsor.Text);
  et.Details = (txtDetails.Text.Length > 255 ? txtDetails.Text.Substring(0, 255) : txtDetails.Text);
  et.Add();

  ClearFields();
  BindTrainingList();
 }

}