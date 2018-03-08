using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using STIeForms;
using HRMS;

public partial class Finance_PCASH_PettyCashRequestDetails : System.Web.UI.Page
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
                    lblChargeTo.Text = clsDepartment.GetName(objPCASRequest.SchoolCode);
                }
                lblRemarks.Text = objPCASRequest.Remarks;

            }
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PettyCashRequestMenu.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

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
}