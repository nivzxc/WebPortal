using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class CIS_MRCF_MRCFPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {

            //Report
            crvMRCF.Height = 600;
            ConnectionInfo ConnInfo = new ConnectionInfo();
            {
                ConnInfo.ServerName = "hades";
                ConnInfo.DatabaseName = "MySTIHQ";
                ConnInfo.UserID = "usermystihq";
                ConnInfo.Password = "F0r3v3rho";
            }

            foreach (TableLogOnInfo cnInfo in this.crvMRCF.LogOnInfo)
            {
                cnInfo.ConnectionInfo = ConnInfo;
            }

            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField = new ParameterField();
            ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();

            paramField.ParameterFieldName = "MRCF Number";
            if (Request.QueryString["mrcfcode"] != null || Request.QueryString["mrcfcode"] != "")
            {
                discreteVal.Value = Request.QueryString["mrcfcode"];
                paramField.CurrentValues.Add(discreteVal);
                paramFields.Add(paramField);

                crvMRCF.ParameterFieldInfo = paramFields;
                crvMRCF.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;
            }
            else
            { Response.Redirect("~/AccessDenied.aspx"); }
        }
    }
}