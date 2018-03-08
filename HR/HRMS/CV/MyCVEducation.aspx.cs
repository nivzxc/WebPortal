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

public partial class HR_HRMS_CV_MyCVEducation : System.Web.UI.Page
{

 private void ClearFields()
 {
  txtCourse.Text = "";
  txtSchool.Text = "";
  txtSchoolAddress.Text = "";
  txtInclusiveDates.Text = "";
  txtRecognition.Text = "";
  chkComplete.Checked = false;

  ddlLevel.DataSource = clsEducationLevel.GetDdlDs();
  ddlLevel.DataValueField = "pvalue";
  ddlLevel.DataTextField = "ptext";
  ddlLevel.DataBind();
 }

 private void BindEducationList()
 {
  dgEducation.DataSource = clsEmployeeEducation.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
  dgEducation.DataBind();

  foreach (DataGridItem itm in dgEducation.Items)
  {
   DropDownList pddlLevel = (DropDownList)itm.FindControl("ddlLevel");
   HiddenField phdnEducLevel = (HiddenField)itm.FindControl("hdnEducLevel");
   HiddenField phdnEducComplete = (HiddenField)itm.FindControl("hdnEducComplete");
   CheckBox pchkEducComplete = (CheckBox)itm.FindControl("chkEducComplete");

   pddlLevel.DataSource = clsEducationLevel.GetDdlDs();
   pddlLevel.DataValueField = "pvalue";
   pddlLevel.DataTextField = "ptext";
   pddlLevel.DataBind();

   pddlLevel.SelectedValue = phdnEducLevel.Value.ToString();
   pchkEducComplete.Checked = (phdnEducComplete.Value.ToString() == "1" ? true : false);
  }

  if (dgEducation.Items.Count == 0)
  {
   divEducation.Visible = false;
   lblEducationNoRec.Visible = true;
   btnReset.Visible = false;
   btnSave.Visible = false;
  }
  else
  {
   divEducation.Visible = true;
   lblEducationNoRec.Visible = false;
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
   BindEducationList();
   ClearFields();
  }
 }

 protected void dgEducation_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  HiddenField phdnEducCode = (HiddenField)e.Item.FindControl("hdnEducCode");
  clsEmployeeEducation ed = new clsEmployeeEducation(phdnEducCode.Value);
  ed.Delete();
  BindEducationList();
 }

 protected void btnSave_Click(object sender, EventArgs e)
 {
  foreach (DataGridItem itm in dgEducation.Items)
  {
   HiddenField phdnEducCode = (HiddenField)itm.FindControl("hdnEducCode");
   DropDownList pddlLevel = (DropDownList)itm.FindControl("ddlLevel");
   TextBox ptxtEducCourse = (TextBox)itm.FindControl("txtEducCourse");
   TextBox ptxtEducDates = (TextBox)itm.FindControl("txtEducDates");
   TextBox ptxtEducRecognition = (TextBox)itm.FindControl("txtEducRecognition");
   TextBox ptxtSchool = (TextBox)itm.FindControl("txtSchool");
   TextBox ptxtSchoolAddress = (TextBox)itm.FindControl("txtSchoolAddress");
   CheckBox pchkEducComplete = (CheckBox)itm.FindControl("chkEducComplete");

   clsEmployeeEducation ed = new clsEmployeeEducation();
   ed.EducationCode = phdnEducCode.Value;
   ed.EducationLevelCode = pddlLevel.SelectedValue;
   ed.Course = ptxtEducCourse.Text;
   ed.InclusiveDates = ptxtEducDates.Text;
   ed.SchoolName = ptxtSchool.Text;
   ed.SchoolAddress = ptxtSchoolAddress.Text;
   ed.Recognition = ptxtEducRecognition.Text;
   ed.Complete = (pchkEducComplete.Checked ? "1" : "0");
   ed.Edit();
  }
  BindEducationList();
 }

 protected void btnReset_Click(object sender, EventArgs e)
 {
  BindEducationList();
 }

 protected void btnAdd_Click(object sender, EventArgs e)
 {
  clsEmployeeEducation ed = new clsEmployeeEducation();
  ed.Username = Request.Cookies["Speedo"]["UserName"].ToString();
  ed.EducationLevelCode = ddlLevel.SelectedValue.ToString();
  ed.Course = txtCourse.Text;
  ed.SchoolName = txtSchool.Text;
  ed.SchoolAddress = txtSchoolAddress.Text;
  ed.InclusiveDates = txtInclusiveDates.Text;
  ed.Recognition = txtRecognition.Text;
  ed.Complete = (chkComplete.Checked ? "1" : "0");
  ed.Add();

  ClearFields();
  BindEducationList();
 }

}