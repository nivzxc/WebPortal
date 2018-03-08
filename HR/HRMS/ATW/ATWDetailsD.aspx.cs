using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class HR_HRMS_ATW_ATWDetailsD : System.Web.UI.Page
{

 private void LoadDetails()
 {
  if (!Page.IsPostBack)
  {
   clsATW atw = new clsATW();
   atw.ATWCode = Request.QueryString["atwcode"].ToString();
   atw.Fill();
   txtATWCode.Text = atw.ATWCode;
   txtDateFiled.Text = atw.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(atw.Username);
   txtReason.Text = atw.Reason;
   txtStatus.Text = clsATW.ToATWStatus(atw.Status);
   hdnStatus.Value = atw.Status;
   txtApproverH.Text = clsUsers.GetName(atw.ApproverHeadName);
   hdnApproverH.Value = atw.ApproverHeadName;
   hdnStatusH.Value = atw.ApproverHeadStatus;
   txtStatusH.Text = clsATW.ToATWStatus(atw.ApproverHeadStatus);
   txtProcessDateH.Text = clsDateTime.CheckMinDate(atw.ApproverHeadDate);
   txtRemarksH.Text = atw.ApproverHeadRemarks;
   hdnApproverD.Value = atw.ApproverDivisionName;
   txtApproverD.Text = clsUsers.GetName(atw.ApproverDivisionName);
   hdnStatusD.Value = atw.ApproverDivisionStatus;
   txtStatusD.Text = clsATW.ToATWStatus(atw.ApproverDivisionStatus);
   txtProcessDateD.Text = clsDateTime.CheckMinDate(atw.ApproverDivisionDate);
   txtRemarksD.Text = atw.ApproverDivisionRemarks;

   if (atw.ApproverDivisionStatus == "F" && atw.Status == "F")
   {
    txtRemarksD.ReadOnly = false;
    txtRemarksD.BackColor = System.Drawing.Color.White;
    btnApprove.Visible = true;
    btnDisapprove.Visible = true;
   }
   else
   {
    btnApprove.Visible = false;
    btnDisapprove.Visible = false;
   }

  }
 }

 protected void BindSchedule()
 {
  DataTable tblSchedule = clsATWDetails.GetDSGSchedule(Request.QueryString["atwcode"].ToString());
  dgSchedule.DataSource = tblSchedule.DefaultView;
  dgSchedule.DataBind();
  foreach (DataGridItem itm in dgSchedule.Items)
  {
   HiddenField phdnStatus = (HiddenField)itm.FindControl("hdnStatus");
   CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");
   TextBox ptxtRemarks = (TextBox)itm.FindControl("txtRemarks");
   pchkApprove.Checked = (phdnStatus.Value == "1" ? true : false);
   if (hdnStatusD.Value == "F")
   {
    pchkApprove.Enabled = true;
    ptxtRemarks.Enabled = true;
    ptxtRemarks.Enabled = true;
    ptxtRemarks.BackColor = System.Drawing.Color.White;
   }
   else
   {
    pchkApprove.Enabled = false;
    ptxtRemarks.Enabled = false;
    ptxtRemarks.Enabled = false;
    ptxtRemarks.BackColor = System.Drawing.Color.AliceBlue;
   }
  }
 }

 private int CountCheckedSchedule()
 {
  int intReturn = 0;
  foreach (DataGridItem itm in dgSchedule.Items)
  {
   CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");
   intReturn += (pchkApprove.Checked ? 1 : 0);
  }
  return intReturn;
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
  if (!clsATW.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["atwcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   LoadDetails();
   BindSchedule();
  }
 }

 protected void btnApprove_Click(object sender, ImageClickEventArgs e)
 {
  
 }

 protected void btnDisapprove_Click(object sender, ImageClickEventArgs e)
 {
 
 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  
 }

 protected void btnApprove_Click1(object sender, EventArgs e)
 {
     string strErrorMessage = "";

     if (CountCheckedSchedule() == 0)
         strErrorMessage += "<br>You should approve atleast 1 schedule.";

     if (strErrorMessage.Length == 0)
     {
         using (clsATW atw = new clsATW())
         {
             atw.ATWCode = Request.QueryString["atwcode"];
             atw.Fill();
             atw.ApproverDivisionDate = DateTime.Now;
             atw.ApproverDivisionRemarks = txtRemarksD.Text;
             atw.ApproveDivision();

             foreach (DataGridItem itm in dgSchedule.Items)
             {
                 HiddenField phdnATWDCode = (HiddenField)itm.FindControl("hdnATWDCode");
                 CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");
                 TextBox ptxtRemarks = (TextBox)itm.FindControl("txtRemarks");

                 using (clsATWDetails atwd = new clsATWDetails())
                 {
                     atwd.ATWDCode = phdnATWDCode.Value;
                     atwd.Status = (pchkApprove.Checked ? "1" : "0");
                     atwd.Remarks = ptxtRemarks.Text + " ";
                     atwd.Update();
                 }
             }
         }
         Response.Redirect("ATWMenu.aspx");
     }
     else
     {
         divError.Visible = true;
         lblErrMsg.Text = strErrorMessage;
     }
 }
 protected void btnDisapprove_Click1(object sender, EventArgs e)
 {
     using (clsATW atw = new clsATW())
     {
         atw.ATWCode = Request.QueryString["atwcode"];
         atw.Fill();
         atw.ApproverDivisionRemarks = txtRemarksD.Text;
         atw.ApproverDivisionDate = DateTime.Now;
         atw.DisapproveDivision();

         foreach (DataGridItem itm in dgSchedule.Items)
         {
             HiddenField phdnATWDCode = (HiddenField)itm.FindControl("hdnATWDCode");
             CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");
             TextBox ptxtRemarks = (TextBox)itm.FindControl("txtRemarks");

             using (clsATWDetails atwd = new clsATWDetails())
             {
                 atwd.ATWDCode = phdnATWDCode.Value;
                 atwd.Status = (pchkApprove.Checked ? "1" : "0");
                 atwd.Remarks = ptxtRemarks.Text + " ";
                 atwd.Update();
             }
         }
     }
     Response.Redirect("ATWMenu.aspx");
 }
 protected void btnBack_Click1(object sender, EventArgs e)
 {
     Response.Redirect("ATWMenu.aspx");
 }
}