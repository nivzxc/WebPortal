//Programmer: Charlie Bachiller 
//Date finished: March 4, 2011

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

public partial class Finance_CATA_CATAReport : System.Web.UI.Page
{
 protected void Page_Load(object sender, EventArgs e)
 {
     clsSpeedo.Authenticate();
     btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
     if (!clsCATARequest.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["catacode"].ToString()))
     {
         if (!clsSystemModule.HasAccess("023", Request.Cookies["Speedo"]["UserName"].ToString()))
         {
             Response.Redirect("~/AccessDenied.aspx");
         }
     }

  clsCATARequest objCataRequest = new clsCATARequest();
  objCataRequest.CataCode = Request.QueryString["catacode"];
  objCataRequest.Fill();

  if (!Page.IsPostBack)
  {
      if (objCataRequest.Status != "0")
      {
          if (clsCATAApproval.CountDisapprove(objCataRequest.CataCode) == 0)
          {
              if (clsCATAApproval.CountForApproval(objCataRequest.CataCode, "E") == 0)
              {
                  if (clsCATAApproval.CountForApproval(objCataRequest.CataCode, "A") == 0)
                  {
                      //Report
                      //CrystalReportViewer1.Height = 600;
                      //ConnectionInfo ConnInfo = new ConnectionInfo();
                      //{
                      //    ConnInfo.ServerName = "hades";
                      //    ConnInfo.DatabaseName = "mystihq";
                      //    ConnInfo.UserID = "usermystihq";
                      //    ConnInfo.Password = "F0r3v3rho";
                      //}


                      //CrystalReportViewer1.Height = 600;
                      //ConnectionInfo ConnInfo = new ConnectionInfo();
                      //{
                      //    ConnInfo.ServerName = "medusa";
                      //    ConnInfo.DatabaseName = "mystihq_20151119";
                      //    ConnInfo.UserID = "sa";
                      //    ConnInfo.Password = "masterkey";
                      //}

                      //Report
                      CrystalReportViewer1.Height = 600;
                      ConnectionInfo ConnInfo = new ConnectionInfo();
                      {
                          ConnInfo.ServerName = "hades";
                          ConnInfo.DatabaseName = "mystihq";
                          ConnInfo.UserID = "sa";
                          ConnInfo.Password = "p@ssw0rd";
                      }

                      foreach (TableLogOnInfo cnInfo in this.CrystalReportViewer1.LogOnInfo)
                      {
                          cnInfo.ConnectionInfo = ConnInfo;
                      }

                      ParameterFields paramFields = new ParameterFields();
                      ParameterField paramField = new ParameterField();
                      ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();

                      paramField.ParameterFieldName = "catacode";
                      if (Request.QueryString["catacode"] != null || Request.QueryString["catacode"] != "")
                      {
                          discreteVal.Value = Request.QueryString["catacode"];
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
  Response.Redirect("FinanceCataMenu.aspx");
 }
}
