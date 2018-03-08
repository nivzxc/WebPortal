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

public partial class HR_HRMS_OB_OBDetails : System.Web.UI.Page
{

 protected void LoadSchedule()
 {
  string strWrite = "";
  DataTable tblOBDetails = clsOBDetails.GetDataTable(Request.QueryString["obcode"]);
  foreach (DataRow drw in tblOBDetails.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows' style='text-align:center;'><img src='../../../Support/" + (drw["pstatus"].ToString() == "1" ? "check16.png" : "close16.png") + "' /></td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["keyin"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["keyout"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + drw["updateby"].ToString() + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsOB.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["obcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsOB.AuthenticateAccessForm(OBUsers.Requestor, Request.Cookies["Speedo"]["UserName"], Request.QueryString["obcode"].ToString());

   clsOB ob = new clsOB(Request.QueryString["obcode"].ToString());
   ob.Fill();
   txtOBCode.Text = ob.OBCode;
   txtOBType.Text = clsOB.GetOBTypeDesc(ob.OBType);
   txtRenderedTo.Text = clsDepartment.GetName(ob.DepartmentCode);
   txtDateFiled.Text = ob.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(ob.Username);
   txtReason.Text = ob.Reason;
   txtStatus.Text = clsOB.ToOBStatus(ob.Status);
   txtHApprover.Text = clsUsers.GetName(ob.ApproverHeadName);
   txtHStatus.Text = clsOB.ToOBStatus(ob.ApproverHeadStatus);
   txtHRemarks.Text = ob.ApproverHeadRemarks;
   if (ob.OBType == "1")
   {    
    txtRApprover.Text = clsUsers.GetName(ob.ApproverRequestorName);
    txtRStatus.Text = clsOB.ToOBStatus(ob.ApproverRequestorStatus);
    txtRRemarks.Text = ob.ApproverRequestorRemarks;

    trRApprover.Visible = true;
    trRRemarks.Visible = true;
   }
   btnCancel.Visible = clsOB.ToOBStatusDesc(ob.Status) == OBStatus.ForApproval;
  }
 }

 protected void btnCancel_Click(object sender, EventArgs e)
 {
  clsOB ob = new clsOB(Request.QueryString["obcode"].ToString());
  ob.Cancel();
  Response.Redirect("OBMenu.aspx");
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OBMenu.aspx");
 }

}