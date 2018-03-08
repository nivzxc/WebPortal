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

public partial class CRM_InquiryDetails : System.Web.UI.Page
{

 private void LoadDetails()
 {
  txtTicketNumber.Text = "999999999";
  txtDateFiled.Text = "September 99, 9999 99:99 AM";
  txtStatus.Text = "Pending";
  txtInquiryCategory.Text = "Course/Program Inquiry";
  txtInquirerName.Text = "Mily Opena";
  txtEmail.Text = "mily.opena@yahoo.com";
  txtContactNumber.Text = "09226034721";
  txtLocation.Text = "Makati City";
  txtCampus.Text = "STI College Global City";
  txtCourse.Text = "Bachelor of Science in Computer Science";
  txtSection.Text = "";
  txtYearGraduated.Text = "2005";
  txtMessage.Text = "Hi STI\n\nI would like to inquire regarding my course because:\n\nthe quick brown fox jumps over the lazy dog\n\nthe quick brown fox jumps over the lazy dog.\n\nthank you";
  txtDesignatedPersons.Text = "Juan Dela Cruz";
  txtAnswerBy.Text = "Juan Dela Cruz";
  txtAnswerOn.Text = "September 99, 9999 99:99 AM";
  txtLastViewedBy.Text = "Juan Dela Cruz";
  txtLastViewedOn.Text = "September 99, 9999 99:99 AM";
  txtLastUpdateBy.Text = "Juan Dela Cruz";
  txtLastUpdateOn.Text = "September 99, 9999 99:99 AM";
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  LoadDetails();
 }

}