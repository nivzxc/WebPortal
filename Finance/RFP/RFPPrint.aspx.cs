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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HRMS;
using STIeForms;

public partial class Finance_RFP_RFPPrint : System.Web.UI.Page
{
 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!Page.IsPostBack)
  {
      if (clsRFPRequest.GetRFPStatus(Request.QueryString["ControlNumber"].ToString()) == "1" || clsRFPRequest.GetRFPStatus(Request.QueryString["ControlNumber"].ToString()) == "M")
      {
          CrystalReportViewer1.HasExportButton = true;
          CrystalReportViewer1.HasPrintButton = true;
      }
      else
      {
          CrystalReportViewer1.HasExportButton = false;
          CrystalReportViewer1.HasPrintButton = false;     
      }

    //Report
    CrystalReportViewer1.Height = 600;
    ConnectionInfo ConnInfo = new ConnectionInfo();
    {
        //ConnInfo.ServerName = "hades";
        //ConnInfo.DatabaseName = "MySTIHQ";
        //ConnInfo.UserID = "usermystihq";
        //ConnInfo.Password = "F0r3v3rho";

        //ConnInfo.ServerName = "rflores\\Mssql2008r2";
        //ConnInfo.DatabaseName = "projMySTIHQ";
        //ConnInfo.UserID = "sa";
        //ConnInfo.Password = "masterkey";

        ConnInfo.ServerName = "hades";
        ConnInfo.DatabaseName = "MySTIHQ";
        ConnInfo.UserID = "sa";
        ConnInfo.Password = "p@ssw0rd";

        //ConnInfo.ServerName = "medusa";
        //ConnInfo.DatabaseName = "projMySTIHQ";
        //ConnInfo.UserID = "sa";
        //ConnInfo.Password = "masterkey";
    }

    foreach (TableLogOnInfo cnInfo in this.CrystalReportViewer1.LogOnInfo)
    {
     cnInfo.ConnectionInfo = ConnInfo;
    }
    
    ParameterFields paramFields = new ParameterFields();
    ParameterField paramField = new ParameterField();
    ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();

    paramField.ParameterFieldName = "ctrlnmbr";
    if (Request.QueryString["ControlNumber"] != null || Request.QueryString["ControlNumber"] != "")
    {
     discreteVal.Value = Request.QueryString["ControlNumber"];
     paramField.CurrentValues.Add(discreteVal);
     paramFields.Add(paramField);

     CrystalReportViewer1.ParameterFieldInfo = paramFields;
     CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;
    }
    else
    { Response.Redirect("~/AccessDenied.aspx"); }
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("RFPMenu.aspx");
 }
}
