using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using STIeForms;

public partial class Finance_PCASH_PCASIssuedSummaryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //clsSpeedo.Authenticate();
        btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");


        if (!Page.IsPostBack)
        {
            //Report
            CrystalReportViewer1.Height = 600;
            ConnectionInfo ConnInfo = new ConnectionInfo();
            {
                //ConnInfo.ServerName = "192.168.0.124";
                //ConnInfo.DatabaseName = "projMySTIHQ";
                //ConnInfo.UserID = "sa";
                //ConnInfo.Password = "masterkey";

                ConnInfo.ServerName = "hades";
                ConnInfo.DatabaseName = "MySTIHQ";
                ConnInfo.UserID = "usermystihq";
                ConnInfo.Password = "F0r3v3rho";
            }

            foreach (TableLogOnInfo cnInfo in this.CrystalReportViewer1.LogOnInfo)
            {
                cnInfo.ConnectionInfo = ConnInfo;
            }
            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PettyCashRequestFinanceMenu.aspx");
    }
}