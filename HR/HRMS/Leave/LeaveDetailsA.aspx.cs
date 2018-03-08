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

public partial class HR_HRMS_Leave_LeaveDetailsA : System.Web.UI.Page
{

 protected void LoadLeaveTypeListNoPay() //added by charlie function LoadLeaveTypeListNoPay
 {
  string strPicture = "";
  string strWrite = "";
  string strLeaveDescription = txtLeaveType.Text.ToUpper();
  string strLeaveWithPay = "";

  if (strLeaveDescription.Contains("WITHOUT PAY"))
  {
   if (strLeaveDescription.Contains("EMERGENCY LEAVE"))
   {
    strLeaveWithPay = clsLeaveSetting.GetCode("Emergency Leave");
   }
   else if (strLeaveDescription.Contains("VACATION LEAVE"))


   {
    strLeaveWithPay = clsLeaveSetting.GetCode("Vacation Leave");
   }
   else if (strLeaveDescription.Contains("SICK LEAVE"))
   {
    strLeaveWithPay = clsLeaveSetting.GetCode("Sick Leave");
   }

   else
   {
    strLeaveWithPay = "";
   }

   clsLeave leave = new clsLeave(Request.QueryString["leavcode"].ToString());
   leave.Fill();

   DataTable tblLeaveBalance = clsLeave.GetRecord(leave.UserName, leave.LeaveType);

   foreach (DataRow drow in tblLeaveBalance.Rows)
   {
    if (drow["leavstat"].ToString() == "A")
     strPicture = "approved.png";

    else if (drow["leavstat"].ToString() == "D")
     strPicture = "Disapproved.png";

    else if (drow["leavstat"].ToString() == "C")
     strPicture = "Disapproved.png";

    else if (drow["leavstat"].ToString() == "F")
     strPicture = "Approval.png";

    strWrite = strWrite + "<tr>" +
                          "<td class='GridRows' style='text-align:left;'>" + Convert.ToDateTime(drow["datefile"].ToString()).ToLongDateString() + "</td>" +
                          "<td class='GridRows' style='text-align:center;'>" + drow["units"].ToString() + "</td>" +
                          "<td class='GridRows' style='text-align:left;'>" + clsString.CutString(drow["reason"].ToString(), 70) + "</td>" +
                          "<td class='GridRows' style='text-align:center;'>" + clsUsers.GetName(drow["apphname"].ToString()) + "</td>" +
                          "<td class='GridRows' align='center'><img src='../../../Support/" + strPicture + "' alt='' align='middle' /></td>" +
                          "</tr>";
   }

   DataTable tblLeaveBalance2 = clsLeave.GetRecord(leave.UserName, strLeaveWithPay);
   if (tblLeaveBalance2.Rows.Count == 0)
   {
    //Response.Write(strWrite);
       lblWriteLeaveTypeList.Text = strWrite;
   }
   else
   {
    foreach (DataRow drow2 in tblLeaveBalance2.Rows)
    {
     if (drow2["leavstat"].ToString() == "A")
      strPicture = "approved.png";

     else if (drow2["leavstat"].ToString() == "D")
      strPicture = "Disapproved.png";

     else if (drow2["leavstat"].ToString() == "C")
      strPicture = "Disapproved.png";

     else if (drow2["leavstat"].ToString() == "F")
      strPicture = "Approval.png";

     strWrite = strWrite + "<tr>" +
                           "<td class='GridRows' style='text-align:left;'>" + Convert.ToDateTime(drow2["datefile"].ToString()).ToLongDateString() + "</td>" +
                           "<td class='GridRows' style='text-align:center;'>" + drow2["units"].ToString() + "</td>" +
                           "<td class='GridRows' style='text-align:left;'>" + clsString.CutString(drow2["reason"].ToString(), 70) + "</td>" +
                           "<td class='GridRows' style='text-align:center;'>" + clsUsers.GetName(drow2["apphname"].ToString()) + "</td>" +
                           "<td class='GridRows' align='center'><img src='../../../Support/" + strPicture + "' alt='' align='middle' /></td>" +
                           "</tr>";
    }

   // Response.Write(strWrite);
    lblWriteLeaveTypeList.Text = strWrite;
   }
  }
  //---------------------------------WITH PAY-----------------------------------------------------------------------
  else
  {
   string strLeaveWithPay2 = "";
   string strLeaveTypeAlt = "";

   //if (strLeaveDescription.Contains("EMERGENCY LEAVE"))
   //{
   // strLeaveWithPay2 = clsLeaveSetting.GetCode("Emergency Leave");
   // strLeaveTypeAlt = clsLeaveType.getLeaveType("EMERGENCY LEAVE WITHOUT PAY");
   //}
   //else if (strLeaveDescription.Contains("VACATION LEAVE"))
   //{
   // strLeaveWithPay2 = clsLeaveSetting.GetCode("Vacation Leave");
   // strLeaveTypeAlt = clsLeaveType.getLeaveType("VACATION LEAVE WITHOUT PAY");
   //}
   //else if (strLeaveDescription.Contains("SICK LEAVE"))
   //{
   // strLeaveWithPay2 = clsLeaveSetting.GetCode("Sick Leave");
   // strLeaveTypeAlt = clsLeaveType.getLeaveType("SICK LEAVE WITHOUT PAY");
   //}

   if (strLeaveDescription.Contains("EMERGENCY LEAVE"))
   {
       strLeaveWithPay2 = clsLeaveSetting.GetCode("Emergency Leave");
       strLeaveTypeAlt = clsLeaveType.getLeaveType("EMERGENCY LEAVE WITH PAY");
   }
   else if (strLeaveDescription.Contains("VACATION LEAVE"))
   {
       strLeaveWithPay2 = clsLeaveSetting.GetCode("Vacation Leave");
       strLeaveTypeAlt = clsLeaveType.getLeaveType("VACATION LEAVE WITH PAY");
   }
   else if (strLeaveDescription.Contains("SICK LEAVE"))
   {
       strLeaveWithPay2 = clsLeaveSetting.GetCode("Sick Leave");
       strLeaveTypeAlt = clsLeaveType.getLeaveType("SICK LEAVE WITH PAY");
   }

   else
   {
    strLeaveWithPay2 = "";
   }

   clsLeave leave = new clsLeave(Request.QueryString["leavcode"].ToString());
   leave.Fill();

   string strLeaveWithoutPay = clsLeaveType.GetDescription(leave.LeaveCode).ToUpper();

   DataTable tblLeaveBalance = clsLeave.GetRecord(leave.UserName, strLeaveTypeAlt);

   foreach (DataRow drow in tblLeaveBalance.Rows)
   {
    if (drow["leavstat"].ToString() == "A")
     strPicture = "approved.png";

    else if (drow["leavstat"].ToString() == "D")
     strPicture = "Disapproved.png";

    else if (drow["leavstat"].ToString() == "C")
     strPicture = "Disapproved.png";

    else if (drow["leavstat"].ToString() == "F")
     strPicture = "Approval.png";

    strWrite = strWrite + "<tr>" +
                          "<td class='GridRows' style='text-align:left;'>" + Convert.ToDateTime(drow["datefile"].ToString()).ToLongDateString() + "</td>" +
                          "<td class='GridRows' style='text-align:center;'>" + drow["units"].ToString() + "</td>" +
                          "<td class='GridRows' style='text-align:left;'>" + clsString.CutString(drow["reason"].ToString(), 70) + "</td>" +
                          "<td class='GridRows' style='text-align:center;'>" + clsUsers.GetName(drow["apphname"].ToString()) + "</td>" +
                          "<td class='GridRows' align='center'><img src='../../../Support/" + strPicture + "' alt='' align='middle' /></td>" +
                          "</tr>";
   }
   //Response.Write(strWrite);
   lblWriteLeaveTypeList.Text = strWrite;

   //DataTable tblLeaveBalance2 = clsLeave.GetRecord(leave.UserName, strLeaveWithPay2);
   //if (tblLeaveBalance2.Rows.Count == 0)
   //{
   //    Response.Write(strWrite);
   //}
   //else
   //{
   //    foreach (DataRow drow2 in tblLeaveBalance2.Rows)
   //    {
   //        if (drow2["leavstat"].ToString() == "A")
   //            strPicture = "approved.png";

   //        else if (drow2["leavstat"].ToString() == "D")
   //            strPicture = "Disapproved.png";

   //        else if (drow2["leavstat"].ToString() == "C")
   //            strPicture = "Disapproved.png";

   //        else if (drow2["leavstat"].ToString() == "F")
   //            strPicture = "Approval.png";

   //        strWrite = strWrite + "<tr>" +
   //                              "<td class='GridRows' style='text-align:left;'>" + Convert.ToDateTime(drow2["datefile"].ToString()).ToLongDateString() + "</td>" +
   //                              "<td class='GridRows' style='text-align:center;'>" + drow2["units"].ToString() + "</td>" +
   //                              "<td class='GridRows' style='text-align:left;'>" + clsString.CutString(drow2["reason"].ToString(), 70) + "</td>" +
   //                              "<td class='GridRows' style='text-align:center;'>" + clsUsers.GetName(drow2["apphname"].ToString()) + "</td>" +
   //                              "<td class='GridRows' align='center'><img src='../../../Support/" + strPicture + "' alt='' align='middle' /></td>" +
   //                              "</tr>";
   //    }

   //    Response.Write(strWrite);
   //}
  }



 }
 
 protected void LoadLeaveBalance() //added by charlie function
 {
  string strWrite = "";
  clsLeave leave = new clsLeave(Request.QueryString["leavcode"].ToString());
  leave.Fill();
  DataTable tblLeaveBalance = clsLeaveBalance.GetActiveLeaveBalanceWOP(leave.UserName);
  foreach (DataRow drow in tblLeaveBalance.Rows)
  {

   if (drow["hasbal"].ToString() == "1")
   {
    strWrite = strWrite + "<tr>" +
     "<td class='GridRows'><img src='../../../Support/" + "flgApp2.png" + "' alt='' /></td>" +
                           "<td class='GridRows'>" + drow["ltdesc"] + "</td>" +
                           "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["maxbal"] : "-") + "</td>" +
                           "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["pused"] : "-") + "</td>" +
                           "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["pbalance"] : "-") + "</td>" +
                          "</tr>";
   }
   else
    strWrite = strWrite + "<tr>" +
    "<td class='GridRows'><img src='../../../Support/" + "flgCancel.png" + "' alt='' /></td>" +
                          "<td class='GridRows'>" + drow["ltdesc"] + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + "-" + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + drow["pbalance"].ToString() + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + "-" + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsLeave.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["leavcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
      ddlLeaveType.DataSource = clsLeaveType.DdlDs();
      ddlLeaveType.DataValueField = "pvalue";
      ddlLeaveType.DataTextField = "ptext";
      ddlLeaveType.DataBind();
      try
      {
          ddlLeaveType.SelectedValue = clsLeave.getLeaveType(Request.QueryString["leavcode"].ToString());
      }
      catch { }

   clsLeave.AuthenticateAccessForm(LeaveUsers.Approver, Request.Cookies["Speedo"]["UserName"], Request.QueryString["leavcode"].ToString());
   clsLeave leave = new clsLeave();
   leave.LeaveCode = Request.QueryString["leavcode"].ToString();
   leave.Fill();
   txtLeaveCode.Text = leave.LeaveCode;
   txtRequestorName.Text = clsUsers.GetName(leave.UserName);
   hdnRequestor.Value = leave.UserName;
   txtLeaveType.Text = clsLeaveType.GetDescription(leave.LeaveType);
   hdnLeaveTypeCode.Value = leave.LeaveType;
   txtDateFiled.Text = leave.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtDateFrom.Text = leave.DateStart.ToString("MMM dd, yyyy hh:mm tt");
   txtDateTo.Text = leave.DateEnd.ToString("MMM dd, yyyy hh:mm tt");
   txtUnits.Text = leave.Units.ToString();
   txtReason.Text = leave.Reason;
   txtApproverName.Text = clsUsers.GetName(leave.ApproverName);
   txtApproverRemarks.Text = leave.ApproverRemarks;
   txtStatus.Text = clsLeave.ToLeaveStatusDesc(leave.Status);

   if (clsLeaveType.IsHasBalance(hdnLeaveTypeCode.Value))
    txtBalance.Text = clsLeaveBalance.GetRemainingBalance(hdnLeaveTypeCode.Value, hdnRequestor.Value).ToString();
   else
    txtBalance.Text = "NA";

   btnApprove.Visible = (leave.Status == "F");
   btnDisApprove.Visible = (leave.Status == "F");

   //added by charlie start
   clsDepartmentApprover.AuthenticateAccessFormWOP(leave.UserName, Request.Cookies["Speedo"]["UserName"]);

   string strLeaveDescription = txtLeaveType.Text.ToUpper();
   string strLeaveWithPay = "";

   if (strLeaveDescription.Contains("EMERGENCY LEAVE"))
   {
    strLeaveWithPay = clsLeaveSetting.GetCode("Emergency Leave");
   }
   else if (strLeaveDescription.Contains("VACATION LEAVE"))
   {
    strLeaveWithPay = clsLeaveSetting.GetCode("Vacation Leave");
   }
   else if (strLeaveDescription.Contains("SICK LEAVE"))
   {
    strLeaveWithPay = clsLeaveSetting.GetCode("Sick Leave");
   }

   if (strLeaveWithPay.Length != 0)
   {
    lblLeaveType.Text = clsLeaveSetting.GetDescription(strLeaveWithPay) + " Application";
   }
   else
   {
    lblLeaveType.Text = "Leave of Absence Application";
   }
   //add by charlie end 
   this.LoadLeaveTypeListNoPay();
  }

 }

 public void LoadLeaveTypeByDLL()
 {
     string strPicture = "";
     string strWrite = "";
     string strLeaveDescription = txtLeaveType.Text.ToUpper();

     clsLeave leave = new clsLeave(Request.QueryString["leavcode"].ToString());
     leave.Fill();

     DataTable tblLeaveBalance = clsLeave.GetRecord(leave.UserName, ddlLeaveType.SelectedValue.ToString());

     foreach (DataRow drow in tblLeaveBalance.Rows)
     {
         if (drow["leavstat"].ToString() == "A")
             strPicture = "approved.png";

         else if (drow["leavstat"].ToString() == "D")
             strPicture = "Disapproved.png";

         else if (drow["leavstat"].ToString() == "C")
             strPicture = "Disapproved.png";

         else if (drow["leavstat"].ToString() == "F")
             strPicture = "Approval.png";

         strWrite = strWrite + "<tr>" +
                               "<td class='GridRows' style='text-align:left;'>" + Convert.ToDateTime(drow["datefile"].ToString()).ToLongDateString() + "</td>" +
                               "<td class='GridRows' style='text-align:center;'>" + drow["units"].ToString() + "</td>" +
                               "<td class='GridRows' style='text-align:left;'>" + clsString.CutString(drow["reason"].ToString(), 70) + "</td>" +
                               "<td class='GridRows' style='text-align:center;'>" + clsUsers.GetName(drow["apphname"].ToString()) + "</td>" +
                               "<td class='GridRows' align='center'><img src='../../../Support/" + strPicture + "' alt='' align='middle' /></td>" +
                               "</tr>";
     }
     lblWriteLeaveTypeList.Text = strWrite;
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  float fltRemainingBalance = clsLeaveBalance.GetRemainingBalance(hdnLeaveTypeCode.Value, hdnRequestor.Value);
  bool blnHasBalance = clsLeaveType.IsHasBalance(hdnLeaveTypeCode.Value);
  if (!blnHasBalance || (fltRemainingBalance >= clsValidator.CheckFloat(txtUnits.Text)))
  {
   if (blnHasBalance)
    clsLeaveBalance.DeductLeaveBalance(float.Parse(txtUnits.Text), hdnRequestor.Value, hdnLeaveTypeCode.Value);

   using (clsLeave leave = new clsLeave())
   {
    leave.LeaveCode = Request.QueryString["leavcode"].ToString();
    leave.Fill();
    leave.ApproverRemarks = txtApproverRemarks.Text;
    leave.ApproverDate = DateTime.Now;
    if (leave.Approve())
     //ADDED by CALVIN CAVITE FEB 15, 2018
    ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "ModalSuccess();", true);

   }
  }
  else
  {
   lblErrMsg.Text = "Not enough balance.";
   divError.Visible = true; 
  }
 }

 protected void btnDisApprove_Click(object sender, EventArgs e)
 {
  using (clsLeave leave = new clsLeave())
  {
   leave.LeaveCode = Request.QueryString["leavcode"].ToString();
   leave.Fill();
   leave.ApproverRemarks = txtApproverRemarks.Text;
   leave.ApproverDate = DateTime.Now;
   if (leave.Disapprove())

                //ADDED by CALVIN CAVITE FEB 15, 2018
                ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalDisapprove();", true);
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("LeaveMenu.aspx");
 }

 protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
 {
     this.LoadLeaveTypeByDLL();
 }
}
