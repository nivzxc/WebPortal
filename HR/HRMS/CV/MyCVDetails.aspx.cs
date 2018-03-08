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
using System.Text;
using GrayMatterSoft;
using HRMS;
using System.IO;

public partial class HR_HRMS_CV_MyCVDetails : System.Web.UI.Page
{
 private void BindDependentList()
 {
  dgDependent.DataSource = clsEmployeeDependent.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
  dgDependent.DataBind();

  if (dgDependent.Items.Count == 0)
  {
   divDependent.Visible = false;
   lblDependent.Visible = true;
   btnDependentSave.Visible = false;
   btnDependentReset.Visible = false;
  }
  else
  {
   divDependent.Visible = true;
   lblDependent.Visible = false;
   btnDependentSave.Visible = true;
   btnDependentReset.Visible = true;
  }
 }

 private void BindChildrenList()
 {
  dgChildren.DataSource = clsEmployeeChildren.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
  dgChildren.DataBind();

  if (dgChildren.Items.Count == 0)
  {
   divChildren.Visible = false;
   lblChildren.Visible = true;
   btnChildSave.Visible = false;
   btnChildReset.Visible = false;
  }
  else
  {
   divChildren.Visible = true;
   lblChildren.Visible = false;
   btnChildSave.Visible = true;
   btnChildReset.Visible = true;
  }
 }

 private void BindTitle()
 {
  ddlTitle.DataSource = clsEmployeeTitle.GetDdlSource();
  ddlTitle.DataValueField = "pvalue";
  ddlTitle.DataTextField = "ptext";
  ddlTitle.DataBind();
 }

 private void BindSuffix()
 {
  ddlSuffix.DataSource = clsEmployeeSuffix.GetDdlSource();
  ddlSuffix.DataValueField = "pvalue";
  ddlSuffix.DataTextField = "ptext";
  ddlSuffix.DataBind();
 }

 private void LoadDetails()
 {
  using (clsEmployee employee = new clsEmployee(Request.Cookies["Speedo"]["UserName"].ToString()))
  {
   employee.Fill();
   txtUsername.Text = employee.Username;
   txtEmployeeNumber.Text = employee.EmployeeNumber;
   txtNameFirst.Text = employee.FirstName;
   txtNameMiddle.Text = employee.MiddleName;
   txtNameLast.Text = employee.LastName;
   txtEmployeeType.Text = clsEmployeeType.GetEmployeeTypeName(employee.EmploymentTypeCode);
   if (employee.Title.Trim() != "") ddlTitle.SelectedValue = employee.Title;
   if (employee.Suffix.Trim() != "") ddlSuffix.SelectedValue = employee.Suffix;
   txtCompany.Text = employee.CompanyCode; /////////
   txtPosition.Text = employee.Position;
   txtUpdateBy.Text = employee.UpdatedBy;
   txtUpdateOn.Text = employee.UpdatedOn.ToString("MMMM dd, yyyy");
   txtNickName.Text = employee.NickName;
   txtGender.Text = employee.Gender;
   txtCivilStatus.Text = (employee.CivilStatus == "S" ? "Single" : "Married");
   txtDateOfBirth.Text = employee.BirthDate.ToString("MM-dd-yyyy");
   txtAge.Text = clsEmployee.ComputeAge(employee.BirthDate).ToString();
   txtPlaceOfBirth.Text = employee.BirthPlace;
   txtCitizenship.Text = employee.Citizenship;
   txtHobby.Text = employee.Hobbies;
   txtHeight.Text = employee.Height;
   txtWeight.Text = employee.Weight;
   txtBloodType.Text = employee.BloodType;
   txtLanguage.Text = employee.Language;
   txtSSS.Text = employee.SssID;
   txtPhilhealth.Text = employee.PhilHealthID;
   txtTIN.Text = employee.TaxID;
   txtHDMF.Text = employee.HdmfID;
   txtHMO.Text = employee.HmoID;
   txtBankAccount.Text = employee.BankAccount;
   txtFatherName.Text = employee.FatherName;
   dtpFatherBirthdate.Date = employee.FatherBirthDate;
   txtFatherAge.Text = clsEmployee.ComputeAge(employee.FatherBirthDate).ToString();
   txtMotherName.Text = employee.MotherName;
   dtpMotherBirthdate.Date = employee.MotherBirthDate;
   txtMotherAge.Text = clsEmployee.ComputeAge(employee.MotherBirthDate).ToString();
   txtSpouseName.Text = employee.SpouseName;
   dtpSpouseBDate.Date = employee.SpouseBirthDate;
   txtSpouseAge.Text = clsEmployee.ComputeAge(employee.SpouseBirthDate).ToString();
   txtAddress1.Text = employee.PermanentAddress;
   txtCity1.Text = employee.PermanentCity;
   txtPhone1.Text = employee.PermanentPhoneNumber;
   txtAddress2.Text = employee.CurrentAddress;
   txtCity2.Text = employee.CurrentCity;
   txtPhone2.Text = employee.CurrentPhoneNumber;
   txtMobile1.Text = employee.PrimaryMobileNumber;
   txtMobile2.Text = employee.AlternativeMobileNumber;
   txtDirectLine.Text = employee.DirectNumber;
   txtLocal.Text = employee.LocalNumber;
   txtFax.Text = employee.FaxNumber;
   txtEmailOfficial.Text = employee.EmailOfficial;
   txtEmailPersonal.Text = employee.EmailPersonal;
   txtEmergencyName.Text = employee.EmergencyPerson;
   txtEmergencyRelation.Text = employee.EmergencyRelation;
   txtEmergencyPhoneNumber.Text = employee.EmergencyPhone;
   txtEmergencyCellNumber.Text = employee.EmergencyCell;
   txtEmergencyAddress.Text = employee.EmergencyAddress;
   txtEmploymentStatus.Text = clsEmploymentStatus.GetEmploymentStatusName(employee.EmploymentStatusCode);
   txtDivision.Text = clsDivision.GetDivisionName(employee.DivisionCode);
   txtGroup.Text = clsGroup.GetGroupName(employee.GroupCode);
   txtDepartment.Text = clsDepartment.GetName(employee.DepartmentCode);
   txtRC.Text = clsRC.GetRCName(employee.RcCode);
   txtJGCode.Text = employee.JGCode;
   txtAssignment.Text = employee.Assignment;
   txtDateHired.Text = employee.DateStart.ToString("MMMM dd, yyyy");
   txtDateRegular.Text = (employee.DateRegular == clsDateTime.SystemMinDate ? "" : employee.DateRegular.ToString("MMMM dd, yyyy"));
   txtSkillPrimary.Text = employee.SkillPrimary;
   txtSkillSecondary.Text = employee.SkillSecondary;

   if (employee.CivilStatus == "S" || employee.CivilStatus == "s")
   {
    trFatherName.Visible = true;
    trFatherBirthDate.Visible = true;
    trMotherName.Visible = true;
    trMotherBirthDate.Visible = true;
    trDependents.Visible = true;
    trSpouseName.Visible = false;
    trSpouseBirthDate.Visible = false;
    trChildren.Visible = false;
   }
   else
   {
    trFatherName.Visible = false;
    trFatherBirthDate.Visible = false;
    trMotherName.Visible = false;
    trMotherBirthDate.Visible = false;
    trDependents.Visible = false;
    trSpouseName.Visible = true;
    trSpouseBirthDate.Visible = true;
    trChildren.Visible = true;
   }
  }
 }


 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
  if (!Page.IsPostBack)
  {
   BindTitle();
   BindSuffix();
   LoadDetails();
   BindDependentList();
   BindChildrenList();
   //if (File.Exists(Server.MapPath("~/pictures/realpicture/") + Request.Cookies["Speedo"]["username"] + ".jpg"))
   // imgRealPic.ImageUrl = "~/pictures/realpicture/" + Request.Cookies["Speedo"]["username"] + ".jpg";
   //else
   // imgRealPic.ImageUrl = "~/pictures/realpicture/default.jpg";
  }
 }

 protected void btnDependentAdd_Click(object sender, EventArgs e)
 {
  clsEmployeeDependent ed = new clsEmployeeDependent();
  ed.Username = Request.Cookies["Speedo"]["UserName"].ToString();
  ed.Name = txtDpndName.Text;
  ed.Birthdate = dtpDpndBirthdate.Date;
  ed.Relation = txtDpndRelation.Text;
  ed.Add();

  txtDpndName.Text = "";
  txtDpndRelation.Text = "";
  dtpDpndBirthdate.Date = DateTime.Now;

  BindDependentList();
 }

 protected void btnDependentSave_Click(object sender, EventArgs e)
 {
  foreach (DataGridItem itm in dgDependent.Items)
  {
   HiddenField phdnListDependentCode = (HiddenField)itm.FindControl("hdnListDependentCode");
   TextBox ptxtListDependentName = (TextBox)itm.FindControl("txtListDependentName");
   GMDatePicker pdtpListDependentBirthdate = (GMDatePicker)itm.FindControl("dtpListDependentBirthdate");
   TextBox ptxtListDependentRelation = (TextBox)itm.FindControl("txtListDependentRelation");

   clsEmployeeDependent ed = new clsEmployeeDependent();
   ed.DependentCode = phdnListDependentCode.Value;
   ed.Name = ptxtListDependentName.Text;
   ed.Birthdate = pdtpListDependentBirthdate.Date;
   ed.Relation = ptxtListDependentRelation.Text;
   ed.Edit();
  }
  BindDependentList();
 }

 protected void dgDependent_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  HiddenField phdnListDependentCode = (HiddenField)e.Item.FindControl("hdnListDependentCode");
  clsEmployeeDependent ed = new clsEmployeeDependent(phdnListDependentCode.Value);
  ed.Delete();
  BindDependentList();
 }

 protected void btnDependentReset_Click(object sender, EventArgs e)
 {
  BindDependentList();
 }

 protected void btnChildAdd_Click(object sender, EventArgs e)
 {
  clsEmployeeChildren ec = new clsEmployeeChildren();
  ec.Username = Request.Cookies["Speedo"]["UserName"].ToString();
  ec.Name = txtChildName.Text;
  ec.Birthdate = dtpChildBirthdate.Date;
  ec.Add();

  txtChildName.Text = "";
  dtpChildBirthdate.Date = DateTime.Now;

  BindChildrenList();
 }

 protected void btnChildSave_Click(object sender, EventArgs e)
 {
  foreach (DataGridItem itm in dgChildren.Items)
  {
   HiddenField phdnListChildCode = (HiddenField)itm.FindControl("hdnListChildCode");
   TextBox ptxtListChildName = (TextBox)itm.FindControl("txtListChildName");
   GMDatePicker pdtpListChildBirthdate = (GMDatePicker)itm.FindControl("dtpListChildBirthdate");

   clsEmployeeChildren ec = new clsEmployeeChildren();
   ec.ChildCode = phdnListChildCode.Value;
   ec.Name = ptxtListChildName.Text.Substring(0, 100);
   ec.Birthdate = pdtpListChildBirthdate.Date;
   ec.Edit();
  }
  BindChildrenList();
 }

 protected void dgChildren_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  HiddenField phdnListChildCode = (HiddenField)e.Item.FindControl("hdnListChildCode");
  clsEmployeeChildren ec = new clsEmployeeChildren(phdnListChildCode.Value);
  ec.Delete();
  BindChildrenList();
 }

 protected void btnChildReset_Click(object sender, EventArgs e)
 {
  BindChildrenList();
 }

 protected void btnSave_Click(object sender, EventArgs e)
 {
  using (clsEmployee employee = new clsEmployee(Request.Cookies["Speedo"]["UserName"].ToString()))
  {
   employee.UpdatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
   employee.UpdatedOn = DateTime.Now;
   employee.Title = ddlTitle.SelectedValue;
   employee.Suffix = ddlSuffix.SelectedValue;
   employee.NickName = txtNickName.Text;
   employee.BirthPlace = txtPlaceOfBirth.Text;
   employee.Citizenship = txtCitizenship.Text;
   employee.Hobbies = txtHobby.Text;
   employee.Height = txtHeight.Text;
   employee.Weight = txtWeight.Text;
   employee.BloodType = txtBloodType.Text;
   employee.Language = txtLanguage.Text;
   employee.FatherName = txtFatherName.Text;
   employee.FatherBirthDate = dtpFatherBirthdate.Date;
   employee.MotherName = txtMotherName.Text;
   employee.MotherBirthDate = dtpMotherBirthdate.Date;
   employee.SpouseName = txtSpouseName.Text;
   employee.SpouseBirthDate = dtpSpouseBDate.Date;
   employee.PermanentAddress = txtAddress1.Text;
   employee.PermanentCity = txtCity1.Text;
   employee.PermanentPhoneNumber = txtPhone1.Text;
   employee.CurrentAddress = txtAddress2.Text;
   employee.CurrentCity = txtCity2.Text;
   employee.CurrentPhoneNumber = txtPhone2.Text;
   employee.PrimaryMobileNumber = txtMobile1.Text;
   employee.AlternativeMobileNumber = txtMobile2.Text;
   employee.DirectNumber = txtDirectLine.Text;
   employee.LocalNumber = txtLocal.Text;
   employee.FaxNumber = txtFax.Text;
   employee.EmailPersonal = txtEmailPersonal.Text;
   employee.EmergencyPerson = txtEmergencyName.Text;
   employee.EmergencyRelation = txtEmergencyRelation.Text;
   employee.EmergencyPhone = txtEmergencyPhoneNumber.Text;
   employee.EmergencyCell = txtEmergencyCellNumber.Text;
   employee.EmergencyAddress = txtEmergencyAddress.Text;
   employee.SkillPrimary = txtSkillPrimary.Text;
   employee.SkillSecondary = txtSkillSecondary.Text;
   employee.EditCV();
   LoadDetails();
  }
 }

 protected void btnReset_Click(object sender, EventArgs e)
 {
  LoadDetails();
 }

}