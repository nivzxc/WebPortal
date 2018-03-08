using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using STIeForms;

public partial class Finance_FinanceRequestMenu : System.Web.UI.Page
{
 //protected void LoadMenuRFP()
 //{
 // if (!IsPostBack)
 // {
 //  string strWhere = "";
 //  //if (txtControlNumber.Text != "")
 //  // strWhere += "AND ctrlnmbr LIKE '%" + txtControlNumber.Text.Trim() + "%'";

 //  //if (ddlRequestType.SelectedValue.ToString() != "ALL")
 //  // strWhere += "AND rqstcode = '" + ddlRequestType.SelectedValue.ToString() + "'";

 //  string strWrite = "";
 //  int intCtr = 0;
 //  DataTable tblRFP = clsFinanceRequest.GetDSGMainFormPerUser("TOP 10 (ctrlnmbr), rqstcode, rqstfor, projttle, dateneed, payename, createby, createon", Request.Cookies["Speedo"]["UserName"], strWhere,1,1);
 //  foreach (DataRow drw in tblRFP.Rows)
 //  {
 //   //Session["ControlNumber"] = drw["ControlNumber"].ToString();
 //   strWrite = strWrite + "<tr>" +
 //                          "<td class='GridRows'></td>" +
 //                          "<td class='GridRows' colspan=2'>" +
 //                           "<a href='RFPPrint.aspx?ControlNumber=" + drw["ControlNumber"].ToString() + " ' style='font-size:small;'>" + CheckLength(drw["ProjectTitle"].ToString()) + "</a>" +
 //                           "<br>Request For: " + clsFinanceRequestType.GetRequestTypeName(drw["RequestCode"].ToString()) +
 //                           "<br>Control Number: " + drw["ControlNumber"].ToString() +
 //                           "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
 //                           "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
 //                          "</td>" +
 //                         "</tr>";
 //   intCtr++;
 //  }

 //  Response.Write(strWrite);
 //  if (intCtr == 0)
 //   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
 //  else
 //   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
 // }
 //}
 protected void LoadMenuRFP()
 {
  if (!IsPostBack)
  {
   string strWhere = "";

   string strWrite = "";
   int intCtr = 0;
   int intStart =  1;
   int intEnd = 10;

   DataTable tblRFP = clsFinanceRequest.GetDSGMainFormPerUser(" TOP 10 (ctrlnmbr), rqstcode, rqstfor, projttle, dateneed, payename, createby, createon", Request.Cookies["Speedo"]["UserName"], strWhere, intStart, intEnd);
   foreach (DataRow drw in tblRFP.Rows)
   {
    //Session["ControlNumber"] = drw["ControlNumber"].ToString();
    strWrite = strWrite + "<tr>" +
                           "<td class='GridRows'></td>" +
                           "<td class='GridRows' colspan=2'>" +
                            "<a href='RFPPrint.aspx?ControlNumber=" + drw["ControlNumber"].ToString() + " ' style='font-size:small;'>" + CheckLength(drw["ProjectTitle"].ToString()) + "</a>" +
                             "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                            "<br>Request For: " + clsFinanceRequestType.GetRequestTypeName(drw["RequestCode"].ToString()) +
                            "<br>Control Number: " + drw["ControlNumber"].ToString() +
                            "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                            "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
                           "</td>" +
                          "</tr>";
    intCtr++;
   }

   Response.Write(strWrite);
   if (intCtr == 0)
    Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
   else
    Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
  }
 }
 protected void LoadMenuRFPFinance()
 {
  if (!IsPostBack)
  {
   if (clsSystemModule.HasAccess("023", Request.Cookies["Speedo"]["UserName"].ToString()))
   {
    string strWhere = "";
    string strWrite = "";
    int intCtr = 0;
    int intPage = Convert.ToInt32(Request.QueryString["page"]);
    int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
    int intStart =  1;
    int intEnd = 10;

    DataTable tblRFP = clsFinanceRequest.GetDSGMainForm("TOP 10 (ctrlnmbr), rqstcode, rqstfor, projttle, dateneed, payename, createby, createon", strWhere, intStart, intEnd);
    foreach (DataRow drw in tblRFP.Rows)
    {
     //Session["ControlNumber"] = drw["ControlNumber"].ToString();
     strWrite = strWrite + "<tr>" +
                            "<td class='GridRows'></td>" +
                            "<td class='GridRows' colspan=2'>" +
                             "<a href='RFPPrint.aspx?ControlNumber=" + drw["ControlNumber"].ToString() + " ' style='font-size:small;'>" + CheckLength(drw["ProjectTitle"].ToString()) + "</a>" +
                             "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                             "<br>Request For: " + clsFinanceRequestType.GetRequestTypeName(drw["RequestCode"].ToString()) +
                             "<br>Control Number: " + drw["ControlNumber"].ToString() +
                             "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                             "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
                            "</td>" +
                           "</tr>";
     intCtr++;
    }

    Response.Write(strWrite);
    if (intCtr == 0)
     Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
    else
     Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
   }
  }
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
 }
 protected void btnNewRequest_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("FinanceNewRequest.aspx");
 }

 public string CheckLength(string pProjectTitle)
 {
  string strReturn = "";
  var intLength = 50;
  if ((pProjectTitle.Length > intLength))
  {
   strReturn = pProjectTitle.Substring(0, intLength) + "...";
  }
  else
  {
   strReturn = pProjectTitle;
  }
  return strReturn;
 }

 //protected void btnSearch_Click(object sender, ImageClickEventArgs e)
 //{
 // LoadMenuRFP();
 //}
}
