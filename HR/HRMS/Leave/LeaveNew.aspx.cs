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
using System.Data.SqlClient;
using HRMS;

public partial class HR_HRMS_Leave_LeaveNew : System.Web.UI.Page
{

 private string _strUsername;

 private void ComputeUnits()
 {
  try
  {
      txtUnits.Text = clsLeave.GetLeaveUnits(dtpFrom.SelectedDate, dtpTo.SelectedDate, clsValidator.CheckFloat(ddlFromTime.SelectedValue), clsValidator.CheckFloat(ddlToTime.SelectedValue), Request.Cookies["Speedo"]["UserName"]).ToString();
  }
  catch { }
 }

 protected void LoadLeaveBalance()
 {
  string strWrite = "";
  DataTable tblLeaveBalance = clsLeaveBalance.GetActiveLeaveBalance(Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drow in tblLeaveBalance.Rows)
  {
   strWrite = strWrite + "<tr>" +
    "<td class='GridRows'><img src='../../../Support/" + (drow["hasbal"].ToString() == "1" ? "flgApp2.png" : "flgCancel.png") + "' alt='' /></td>" +
                          "<td class='GridRows'>" + drow["ltdesc"] + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["maxbal"] : "-") + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["pused"] : "-") + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["pbalance"] : "-") + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
 }

 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();

    btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
  _strUsername = Request.Cookies["Speedo"]["UserName"].ToString();
  if (!Page.IsPostBack)
  {   
   lblRequestorName.Text = clsUsers.GetName(_strUsername);
   ddlType.DataSource = clsLeaveBalance.GetDdlDSUsersLeaveTypes(_strUsername);
   ddlType.DataValueField = "pvalue";
   ddlType.DataTextField = "ptext";
   ddlType.DataBind();

   txtBalance.Text = clsLeaveBalance.GetRemainingBalance(ddlType.SelectedValue, _strUsername).ToString();

   ddlApprover.DataSource = clsDepartmentApprover.DSLApproverEmployee(_strUsername, EFormType.Leave);
   ddlApprover.DataValueField = "pvalue";
   ddlApprover.DataTextField = "ptext";
   ddlApprover.DataBind();

   dtpFrom.SelectedDate = DateTime.Now;
   txtDateFrom_TextChanged(null, null);
   dtpTo.SelectedDate = DateTime.Now;
   txtDateTo_TextChanged(null, null);
   ddlType_SelectedIndexChanged(null, null);
   txtUnits.Text = "0.5";
   //ComputeUnits();
  }
 }

 protected void txtDateFrom_TextChanged(object sender, EventArgs e)
 {
  ddlFromTime.Items.Clear();
  using (clsShift shift = new clsShift())
  {
      shift.ShiftCode = clsShift.GetDayShiftCode(_strUsername, Convert.ToDateTime(dtpFrom.SelectedDate));
   shift.Fill();
   ddlFromTime.Items.Add(new ListItem(shift.TimeStart.ToString("hh:mm tt"), "0"));
   ddlFromTime.Items.Add(new ListItem(shift.TimeHalf.ToString("hh:mm tt"), ".5"));
  }
  ComputeUnits();
 }

 protected void txtDateTo_TextChanged(object sender, EventArgs e)
 {
  ddlToTime.Items.Clear();
  clsShift shift = new clsShift();

  shift.ShiftCode = clsShift.GetDayShiftCode(_strUsername, Convert.ToDateTime(dtpTo.SelectedDate));
  shift.Fill();
  ddlToTime.Items.Add(new ListItem(shift.TimeHalf.ToString("hh:mm tt"), ".5"));
  ddlToTime.Items.Add(new ListItem(shift.TimeEnd.ToString("hh:mm tt"), "1"));
  ComputeUnits();
 }

 protected void btnSend_Click(object sender, EventArgs e)
 {
  string strErrorMessage = "";
  DateTime dteFrom = Convert.ToDateTime(dtpFrom.SelectedDate.ToString("M/d/yyyy") + " " + ddlFromTime.SelectedItem.Text);
  DateTime dteTo = Convert.ToDateTime(dtpTo.SelectedDate.ToString("M/d/yyyy") + " " + ddlToTime.SelectedItem.Text);

  if (dteFrom >= dteTo)
   strErrorMessage += "<br>You entered an invalid date bracket.";

  if (ddlType.Items.Count == 0)
   strErrorMessage += "<br>Please select leave type.";

  if (clsLeave.HasExistingApplication(Request.Cookies["Speedo"]["UserName"], dteFrom, dteTo))
   strErrorMessage += "<br>You already filed an application within this date bracket.";

  if (!clsLeave.HasEnoughBalance(clsValidator.CheckFloat(txtUnits.Text), _strUsername, ddlType.SelectedValue))
   strErrorMessage += "<br>Not enough balance.";

  if (strErrorMessage.Length == 0)
  {
   clsLeave leave = new clsLeave();
   leave.LeaveType = ddlType.SelectedValue;
   leave.UserName = _strUsername;
   leave.DateFile = DateTime.Now;
   leave.DateStart = Convert.ToDateTime(dtpFrom.SelectedDate.ToString("M/d/yyyy") + " " + ddlFromTime.SelectedItem.Text);
   leave.DateEnd = Convert.ToDateTime(dtpTo.SelectedDate.ToString("M/d/yyyy") + " " + ddlToTime.SelectedItem.Text);
   leave.Units = float.Parse(txtUnits.Text);
   leave.Reason = txtReason.Text;
   leave.ApproverName = ddlApprover.SelectedValue;
   leave.Insert();

            //ADDED by CALVIN CAVITE FEB 15, 2018
            ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ModalSuccess();", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "alert('Your Leave has been filed and under for approval'); window.location='" + Request.ApplicationPath + "HR/HRMS/Leave/LeaveMenu.aspx';", true);
   
   //Response.Redirect("LeaveMenu.aspx");
  }
  else
  {
   divError.Visible = true;
   lblErrMsg.Text = strErrorMessage;
  }
 }

 protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
 {
  if (clsLeaveType.IsHasBalance(ddlType.SelectedValue))
   txtBalance.Text = clsLeaveBalance.GetRemainingBalance(ddlType.SelectedValue, _strUsername).ToString();
  else
   txtBalance.Text = "NA";
 }

 protected void btnClear_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("LeaveNew.aspx");
 }

 protected void ddlToTime_SelectedIndexChanged(object sender, EventArgs e)
 {
  ComputeUnits();
 }

 protected void ddlFromTime_SelectedIndexChanged(object sender, EventArgs e)
 {
  ComputeUnits();
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("LeaveMenu.aspx");
 }

 protected void btnFrom_Click(object sender, ImageClickEventArgs e)
 {

 }
 protected void btnTo_Click(object sender, ImageClickEventArgs e)
 {

 }
 protected void dtpFrom_DateChanged(object sender,EventArgs e)
 {
     ddlFromTime.Items.Clear();
     using (clsShift shift = new clsShift())
     {
         shift.ShiftCode = clsShift.GetDayShiftCode(_strUsername, Convert.ToDateTime(dtpFrom.SelectedDate));
         shift.Fill();
         ddlFromTime.Items.Add(new ListItem(shift.TimeStart.ToString("hh:mm tt"), "0"));
         ddlFromTime.Items.Add(new ListItem(shift.TimeHalf.ToString("hh:mm tt"), ".5"));
     }
     ComputeUnits();
 }
 protected void dtpTo_DateChanged(object sender, EventArgs e)
 {
     ddlToTime.Items.Clear();
     clsShift shift = new clsShift();

     shift.ShiftCode = clsShift.GetDayShiftCode(_strUsername, Convert.ToDateTime(dtpTo.SelectedDate));
     shift.Fill();
     ddlToTime.Items.Add(new ListItem(shift.TimeHalf.ToString("hh:mm tt"), ".5"));
     ddlToTime.Items.Add(new ListItem(shift.TimeEnd.ToString("hh:mm tt"), "1"));

     ComputeUnits();
 }
}