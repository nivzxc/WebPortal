using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using STIeForms;

public partial class Finance_RFPPrint : System.Web.UI.Page
{
 protected void Page_Load(object sender, EventArgs e)
 {

  if (!Page.IsPostBack)
  {
   //Report
   CrystalReportViewer1.Height = 400;
   ConnectionInfo ConnInfo = new ConnectionInfo();
   {
    ConnInfo.ServerName = "carlos";
    ConnInfo.DatabaseName = "MySTIHQ";
    ConnInfo.UserID = "mystihq";
    ConnInfo.Password = "sp33do";
   }

   foreach (TableLogOnInfo cnInfo in this.CrystalReportViewer1.LogOnInfo)
   {
    cnInfo.ConnectionInfo = ConnInfo;
   }

   ParameterFields paramFields = new ParameterFields();
   ParameterField paramField = new ParameterField();
   ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();

   paramField.ParameterFieldName = "ControlNumber";
   if (Request.QueryString["ControlNumber"] != null || Request.QueryString["ControlNumber"] != "")
   {
    discreteVal.Value = Request.QueryString["ControlNumber"];
   }
   paramField.CurrentValues.Add(discreteVal);
   paramFields.Add(paramField);

   CrystalReportViewer1.ParameterFieldInfo = paramFields;

   CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;

   //   CrystalReportSource1.ReportDocument.PrintToPrinter(1, true, 1, 1);
  }
 }
}
