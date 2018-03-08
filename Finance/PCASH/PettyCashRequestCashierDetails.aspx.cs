using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using STIeForms;
using HRMS;

public partial class Finance_PCASH_PettyCashRequestCashierDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (clsPCASRequest objPCASRequest = new clsPCASRequest())
            {
                objPCASRequest.PCASCode = Request.QueryString["pcascode"].ToString();
                objPCASRequest.Fill();
                lblPCASCode.Text = Request.QueryString["pcascode"].ToString();
                lblReason.Text = objPCASRequest.Reason;
                lblRequestor.Text = clsEmployee.GetName(objPCASRequest.CreatedBy);
                lblPayeeName.Text = clsEmployee.GetName(objPCASRequest.RequestedBy);
                lblDataFundsNeeded.Text = objPCASRequest.DateNeeded.ToString("MM/dd/yyyy");
                lblClassification.Text = clsPCASClassfication.GetName(objPCASRequest.Classification);
                lblFiledOB.Text = objPCASRequest.OBCode.ToString().Trim().Length == 0 ? "Not Applicable" : objPCASRequest.OBCode;
                lblChargeType.Text = clsPCASChargeType.GetName(objPCASRequest.ChargeTypeCode);
                if (objPCASRequest.ChargeTypeCode == "1")
                {
                    lblChargeTo.Text = clsRC.GetRCName(objPCASRequest.RCCode);
                }
                else if (objPCASRequest.ChargeTypeCode == "2")
                {
                    lblChargeTo.Text = clsRC.GetRCName(objPCASRequest.RCCode);
                }
                else if (objPCASRequest.ChargeTypeCode == "3")
                {
                    //lblChargeTo.Text = clsDepartment.GetName(objPCASRequest.SchoolCode);
                    lblChargeTo.Text = clsSchool.GetSchoolName(objPCASRequest.SchoolCode);
                }
                else if (objPCASRequest.ChargeTypeCode == "4")
                {
                    lblChargeTo.Text = objPCASRequest.Others;
                }

            }

            if (clsPCASRequest.GetPCASStatus(Request.QueryString["pcascode"].ToString()) == "A" && clsPCASRequest.GetPCASIsIssued(Request.QueryString["pcascode"].ToString()) == "F")
            {
                btnTagasIssued.Visible = false;
                btnTagASReady.Visible = true;
                btnPrint.Visible = false;
            }
            else if (clsPCASRequest.GetPCASStatus(Request.QueryString["pcascode"].ToString()) == "A" && clsPCASRequest.GetPCASIsIssued(Request.QueryString["pcascode"].ToString()) == "R")
            {
                btnTagasIssued.Visible = true;
                btnTagASReady.Visible = false;
                btnPrint.Visible = false;
            }
            else if (clsPCASRequest.GetPCASStatus(Request.QueryString["pcascode"].ToString()) == "A" && clsPCASRequest.GetPCASIsIssued(Request.QueryString["pcascode"].ToString()) == "I")
            {
                btnTagasIssued.Visible = false;
                btnTagASReady.Visible = false;
                btnPrint.Visible = true;
            }

            if (clsPCASRequest.GetPCASIsIssued(Request.QueryString["pcascode"].ToString()) == "F")
            {
                trCustodian.Visible = true;
            }
            else 
            {
                trCustodian.Visible = false;
            }
            ddlCustodian.DataSource = clsPCASCustodianFPC.GetDSL();
            ddlCustodian.DataValueField = "pvalue";
            ddlCustodian.DataTextField = "ptext";
            ddlCustodian.DataBind();

            try
            {
                ddlCustodian.SelectedValue = clsPCASRequestCustodian.GetUsername(Request.QueryString["pcascode"].ToString());
            }
            catch
            { }


        }
    }



    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (clsPCASApproval.TagApprovedOrNot(Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), "1") > 0)
        {
            Response.Redirect("PettyCashRequestMenu.aspx");
        }
    }
    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        if (clsPCASApproval.TagApprovedOrNot(Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), "2") > 0)
        {
            Response.Redirect("PettyCashRequestMenu.aspx");
        }
    }

    public void LoadPCASDetails()
    {
        string strWrite = "";
        double dbltotal = 0.0;
        string strChargeTo = "";
        DataTable tblDetails = clsPCASRequestDetails.GetDSGMainForm(Request.QueryString["pcascode"].ToString());
        //DataTable tblDetails = clsRFPRequestDetails.GetDSG(Request.QueryString["ControlNumber"]);
        foreach (DataRow drw in tblDetails.Rows)
        {
            //if (drw["schlcode"].ToString().Trim() != "")
            //{ strChargeTo = clsSchool.GetSchoolName(drw["schlcode"].ToString().Trim()); }
            //if (drw["rccode"].ToString().Trim() != "")
            //{ strChargeTo = clsRC.GetRCName(drw["rccode"].ToString().Trim()); }
            //if (drw["others"].ToString().Trim() != "")
            //{ strChargeTo = drw["others"].ToString().Trim(); }

            double dblAmount = Double.Parse(drw["amount"].ToString());
            strWrite += "<tr>" +
                          "<td colspan='2' class='GridRows'>&nbsp;" + drw["itemname"].ToString() + "</td>" +
                //"<td class='GridRows'>&nbsp;" + strChargeTo + "</td>" +
                          "<td align='right' class='GridRows'>" + string.Format("{0:0,0.00}", dblAmount) + "</td>" +
                        "</tr>";
            dbltotal += double.Parse(drw["amount"].ToString());
        }

        strWrite += "<tr>" +
                         "<td colspan='3'align='right' class='GridRows'><b>Total Amount:    P " + string.Format("{0:0,0.00}", dbltotal) + "</b></td>" +
                    "</tr>";

        Response.Write(strWrite);
    }

    public void LoadPCASAllocationDetails()
    {
        string strWrite = "";
        double dbltotal = 0.0;
        string strChargeTo = "";
        DataTable tblDetails = clsPCASRequestAllocation.GetDSGMainForm(Request.QueryString["pcascode"].ToString());
        //DataTable tblDetails = clsRFPRequestDetails.GetDSG(Request.QueryString["ControlNumber"]);
        foreach (DataRow drw in tblDetails.Rows)
        {

            double dblAmount = Double.Parse(drw["amount"].ToString());
            strWrite += "<tr>" +
                          "<td colspan='2' class='GridRows'>&nbsp;" + drw["aexpname"].ToString() + "</td>" +
                //"<td class='GridRows'>&nbsp;" + strChargeTo + "</td>" +
                          "<td align='right' class='GridRows'>" + string.Format("{0:0,0.00}", dblAmount) + "</td>" +
                        "</tr>";
            dbltotal += double.Parse(drw["amount"].ToString());
        }

        strWrite += "<tr>" +
                         "<td colspan='3'align='right' class='GridRows'><b>Total Amount:    P " + string.Format("{0:0,0.00}", dbltotal) + "</b></td>" +
                    "</tr>";

        Response.Write(strWrite);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PettyCashRequestCashierMenu.aspx");
    }
    protected void btnTagasIssued_Click(object sender, EventArgs e)
    {
        if (clsPCASRequest.TagAsIssued(Request.QueryString["pcascode"].ToString()) > 0)
        {
            clsPCASRequestCustodian.UpdateDateIssued(Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString());
            clsPCASRequest.SendEmailNotification("RequestorTagAsIssued", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), Request.Cookies["Speedo"]["UserName"].ToString());
            clsPCASRequest.SendEmailNotification("CashierTagAsIssued", Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString());
            Response.Redirect("PettyCashRequestCashierMenu.aspx");
        }
    }
    protected void btnTagASReady_Click(object sender, EventArgs e)
    {
        if (clsPCASRequest.TagAsReady(Request.QueryString["pcascode"].ToString()) > 0)
        {
            clsPCASRequest.SendEmailNotification("RequestorTagAsReady", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), Request.Cookies["Speedo"]["UserName"].ToString());
            clsPCASRequest.SendEmailNotification("CashierTagAsReady", Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString());
            Response.Redirect("PettyCashRequestCashierMenu.aspx");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCASReport.aspx?pcascode=" + Request.QueryString["pcascode"].ToString());
    }
    protected void btnForwardRequest_Click(object sender, EventArgs e)
    {
        if (ddlCustodian.SelectedValue.ToString() != Request.Cookies["Speedo"]["UserName"].ToString())
        {
            //clsPCASRequestCustodian.ChangeCustodian(Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString());
            clsPCASRequestCustodian.ChangeCustodian(Request.QueryString["pcascode"].ToString(), ddlCustodian.SelectedValue.ToString());
            clsPCASRequest.SendEmailNotification("CashierApprover", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), ddlCustodian.SelectedValue.ToString());
            //Response.Redirect(Request.RawUrl);
            Response.Redirect("PettyCashRequestCashierMenu.aspx");
        }
    }
}