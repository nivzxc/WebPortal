using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using STIeForms;
using HRMS;

public partial class Finance_RFP_RFPDetailsApprover : System.Web.UI.Page
{
 public void LoadRFPDetails()
 {
  string strWrite = "";
  double dbltotal = 0.0;
  string strChargeTo = "";
  DataTable tblDetails = clsRFPRequestDetails.GetDSG(Request.QueryString["ControlNumber"]);
  foreach( DataRow drw in tblDetails.Rows)
  {
     if (drw["schlcode"].ToString().Trim() != "")
     {strChargeTo = clsSchool.GetSchoolName(drw["schlcode"].ToString().Trim());}
     if (drw["rccode"].ToString().Trim() != "")
     {strChargeTo = clsRC.GetRCName(drw["rccode"].ToString().Trim());}
     if (drw["others"].ToString().Trim() != "")
     {strChargeTo = drw["others"].ToString().Trim();}

     double dblAmount = Double.Parse(drw["amount"].ToString());
     strWrite += "<tr>" +
                   "<td class='GridRows'>&nbsp;" + drw["itemdesc"].ToString() + "</td>" +
                   "<td class='GridRows'>&nbsp;" + strChargeTo + "</td>" +
                   "<td align='right' class='GridRows'>" + string.Format("{0:0,0.00}", dblAmount) + "</td>" +
                 "</tr>";
     dbltotal += double.Parse(drw["amount"].ToString());
  }

  strWrite += "<tr>" +
                   "<td colspan='3'align='right' class='GridRows'><b>Total Amount:    P " + string.Format("{0:0,0.00}", dbltotal) + "</b></td>" +
              "</tr>";

  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsRFPRequest.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["ControlNumber"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   string strapproverType = "";

   if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
   {
    LoadDetails();
    if (clsFinanceApprover.IsEndorder1((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr" ,lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "endrsby1";
    }

    else if (clsFinanceApprover.IsEndorder2((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "endrsby2";
    }

    else if (clsFinanceApprover.IsAuthoritary((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "authrzby";
     btnDocumentRequired2.Visible = false;
     btnDocumentRequired.Visible = true;
    }

    if (clsFinanceApprover.IsCanStillApprove(strapproverType, "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     divButtons.Visible = true;
     divButtons2.Visible = false;
    }
    else
    {
     divButtons.Visible = false;
     divButtons2.Visible = false;
     //lblStatus.Text = "( Already been approved )"; lblStatus.Visible = true;
    }
   }
  }
 }

 private void LoadDetails()
 {
  using(clsRFPRequest objRequest = new clsRFPRequest())
  {
   objRequest.ControlNumber = Request.QueryString["ControlNumber"];
   objRequest.Fill();
   lblControlNumber.Text = objRequest.ControlNumber;
   lblPayee.Text = objRequest.PayeeName;
   lblRequestedBy.Text = clsEmployee.GetName(objRequest.CreatedBy);
   lblRequestType.Text = clsRFPRequestType.GetRequestTypeName(objRequest.RequestCode);
   //lblRequestFor.Text = objRequest.RequestFor;
   lblProjectTitle.Text = objRequest.ProjectTitle;
   lblReferenceRFANo.Text = objRequest.RFANumber;
   lblDateCheckNeeded.Text = objRequest.DateNeeded.ToLongDateString().ToString();
   lblDateCreated.Text = objRequest.CreatedOn.ToLongDateString().ToString();
   lblSupportingDocuments.Text = objRequest.SupportingDoument;
   lblRemarks.Text = objRequest.Remarks;
   lblEndorsers.Text = objRequest.EndorsedStatus1=="1"?clsEmployee.GetName(objRequest.EndorsedBy1):"" + " <br/>" + objRequest.EndorsedStatus2=="1"?clsEmployee.GetName(objRequest.EndorsedBy2):"";
   if (lblEndorsers.Text.Trim().Length == 0)
   {
       trEndorsers.Visible = false;
   }

   if (objRequest.Status == "1")
   { lblStatus.Text = "( Already been approved )"; lblStatus.Visible = true; }
   else if (objRequest.Status == "2")
   { lblStatus.Text = "( For Approval )";  }
   else if (objRequest.Status == "3")
   { lblStatus.Text = "( Already been cancelled )"; lblStatus.Visible = true; }
   else if (objRequest.Status == "M")
   { lblStatus.Text = "( Already been tag for manual approval )"; lblStatus.Visible = true; }
   else if (objRequest.Status == "0")
   {
   }
   else
   { lblStatus.Text = "( Already been disapproved )"; }
  }
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  string strapproverType = "";

  if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
  {
   if (clsFinanceApprover.IsCanChangeRequestStatus("ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
   {
    if (clsFinanceApprover.IsEndorder1((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "endrsby1";
    }

    else if (clsFinanceApprover.IsEndorder2((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "endrsby2";
    }

    else if (clsFinanceApprover.IsAuthoritary((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "authrzby";
    }

    //if (clsRFPRequest.Approve(lblControlNumber.Text, strapproverType, "1", Request.Cookies["Speedo"]["UserName"]) > 0)
    //{
    // Response.Redirect("RFPMenu.aspx");
    //}

    if (clsRFPRequest.ApproveDocuReq(lblControlNumber.Text, strapproverType, "1", Request.Cookies["Speedo"]["UserName"], "0") > 0)
    {
        Response.Redirect("RFPMenu.aspx");
    }
   }

   else
   { 
    divError.Visible = true;
    lblErrMsg.Text = "The request has been approved, disapproved or cancelled by the user";
   }
  }

 }

 protected void btnDisApprove_Click(object sender, EventArgs e)
 {
  string strapproverType = "";

  if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
  {
   if (clsFinanceApprover.IsCanChangeRequestStatus("ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
   {
    if (clsFinanceApprover.IsEndorder1((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "endrsby1";
    }

    else if (clsFinanceApprover.IsEndorder2((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "endrsby2";
    }

    else if (clsFinanceApprover.IsAuthoritary((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
    {
     strapproverType = "authrzby";
    }

    if (clsRFPRequest.Approve(lblControlNumber.Text, strapproverType, "0", Request.Cookies["Speedo"]["UserName"]) > 0)
    {
     Response.Redirect("RFPMenu.aspx");
    }
   }
  }
 }

 protected void btnDocumentRequired_Click(object sender, EventArgs e)
 {
     string strapproverType = "";

     if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
     {
         if (clsFinanceApprover.IsCanChangeRequestStatus("ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
         {
             if (clsFinanceApprover.IsEndorder1((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
             {
                 strapproverType = "endrsby1";
             }

             else if (clsFinanceApprover.IsEndorder2((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
             {
                 strapproverType = "endrsby2";
             }

             else if (clsFinanceApprover.IsAuthoritary((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
             {
                 strapproverType = "authrzby";
             }

             //if (clsRFPRequest.ApproveDocuReq(lblControlNumber.Text, strapproverType, "1", Request.Cookies["Speedo"]["UserName"], "1") > 0)
             //{
             //    Response.Redirect("RFPMenu.aspx");
             //}
             if (clsRFPRequest.TagDocumentRequired(lblControlNumber.Text, strapproverType, "M", Request.Cookies["Speedo"]["UserName"],"1") > 0)
             {
                 Response.Redirect("RFPMenu.aspx");
             }
         }

         else
         {
             divError.Visible = true;
             lblErrMsg.Text = "The request has been approved, disapproved or cancelled by the user";
         }
     }
 }
 protected void btnDocumentRequired2_Click(object sender, EventArgs e)
 {
     string strapproverType = "";

     if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
     {
         if (clsFinanceApprover.IsCanChangeRequestStatus("ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
         {
             if (clsFinanceApprover.IsEndorder1((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
             {
                 strapproverType = "endrsby1";
             }

             else if (clsFinanceApprover.IsEndorder2((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
             {
                 strapproverType = "endrsby2";
             }

             else if (clsFinanceApprover.IsAuthoritary((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", lblControlNumber.Text, "RFPRequest"))
             {
                 strapproverType = "authrzby";
             }

             //if (clsRFPRequest.ApproveDocuReq(lblControlNumber.Text, strapproverType, "1", Request.Cookies["Speedo"]["UserName"],"1") > 0)
             //{
             //    Response.Redirect("RFPMenu.aspx");
             //}
             if (clsRFPRequest.TagDocumentRequired(lblControlNumber.Text, strapproverType, "M", Request.Cookies["Speedo"]["UserName"], "1") > 0)
             {
                 Response.Redirect("RFPMenu.aspx");
             }
         }

         else
         {
             divError.Visible = true;
             lblErrMsg.Text = "The request has been approved, disapproved or cancelled by the user";
         }
     }
 }
}
