using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STIeForms;
using CrystalDecisions.Shared;

public partial class Finance_PCASH_PCASReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
        if (!clsPCASRequest.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["pcascode"].ToString()))
        {
            if (!clsSystemModule.HasAccess("PETTYC", Request.Cookies["Speedo"]["UserName"].ToString()))
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }

        clsPCASRequest objPCASRequest = new clsPCASRequest();
        objPCASRequest.PCASCode = Request.QueryString["pcascode"];
        objPCASRequest.Fill();

        if (!Page.IsPostBack)
        {
            if (objPCASRequest.PCASStat != "P")
            {
                if (clsPCASApproval.CountDisapprove(objPCASRequest.PCASCode) == 0)
                {
                    if (clsPCASApproval.CountForApproval(objPCASRequest.PCASCode, "E") == 0)
                    {
                        if (clsPCASApproval.CountForApproval(objPCASRequest.PCASCode, "A") == 0)
                        {
                            //Report
                            CrystalReportViewer1.Height = 600;
                            ConnectionInfo ConnInfo = new ConnectionInfo();
                            {
                                ConnInfo.ServerName = "hades";
                                ConnInfo.DatabaseName = "MySTIHQ";
                                ConnInfo.UserID = "usermystihq";
                                ConnInfo.Password = "F0r3v3rho";
                            }

                            foreach (TableLogOnInfo cnInfo in this.CrystalReportViewer1.LogOnInfo)
                            {
                                cnInfo.ConnectionInfo = ConnInfo;
                            }

                            ParameterFields paramFields = new ParameterFields();
                            ParameterField paramField = new ParameterField();
                            ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();

                            paramField.ParameterFieldName = "pcascode";
                            if (Request.QueryString["pcascode"] != null || Request.QueryString["pcascode"] != "")
                            {
                                discreteVal.Value = Request.QueryString["pcascode"];
                            }
                            paramField.CurrentValues.Add(discreteVal);
                            paramFields.Add(paramField);

                            CrystalReportViewer1.ParameterFieldInfo = paramFields;
                            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;
                        }
                        else
                        { Response.Redirect("~/AccessDenied.aspx"); }
                    }
                    else
                    { Response.Redirect("~/AccessDenied.aspx"); }
                }
                else
                { Response.Redirect("~/AccessDenied.aspx"); }
            }
            else
            { Response.Redirect("~/AccessDenied.aspx"); }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PettyCashRequestCashierMenu.aspx");
    }
}