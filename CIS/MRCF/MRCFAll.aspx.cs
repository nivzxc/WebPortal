using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using STIeForms;

public partial class CIS_MRCF_MRCFAll : System.Web.UI.Page
{

 protected void LoadMRCF()
 {
  string strWrite = "";
  int intCtr = 0;
  int intPage = Convert.ToInt32(Request.QueryString["page"]);
  int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
  int intStart = ((intPage - 1) * intPageSize) + 1;
  int intEnd = intPage * intPageSize;

  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   if (ddlRequestStatus.SelectedValue == "all")
   {
    if (ddlRequestType.SelectedValue == "all")
     cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,reqtype,intended,datereq,sprvcode,sprvstat,headcode,headstat,proccode,procstat,status,ROW_NUMBER() OVER(ORDER BY datereq) AS RowNum FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    else
     cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,reqtype,intended,datereq,sprvcode,sprvstat,headcode,headstat,proccode,procstat,status,ROW_NUMBER() OVER(ORDER BY datereq) AS RowNum FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND reqtype='" + ddlRequestType.SelectedValue + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
   }
   else
   {
    if (ddlRequestType.SelectedValue == "all")
     cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,reqtype,intended,datereq,sprvcode,sprvstat,headcode,headstat,proccode,procstat,status,ROW_NUMBER() OVER(ORDER BY datereq) AS RowNum FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='" + ddlRequestStatus.SelectedValue + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    else
     cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,reqtype,intended,datereq,sprvcode,sprvstat,headcode,headstat,proccode,procstat,status,ROW_NUMBER() OVER(ORDER BY datereq) AS RowNum FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='" + ddlRequestStatus.SelectedValue + "' AND reqtype='" + ddlRequestType.SelectedValue + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
   }
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   while (dr.Read())
   {
    strWrite = strWrite + "<tr>" +
                                                               "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetails.aspx?mrcfcode=" + dr["mrcfcode"] + "'><img src='../../Support/" + clsMRCF.GetRequestStatusIcon(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "' alt='' /></a>" +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetails.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["intended"] + "</a><br>" +
                                                                                                    "Request Type: " + clsMRCF.GetRequestTypeDesc(dr["reqtype"].ToString()) + "<br>" +
                                                                                                    "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd,yyyy") +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>" + clsMRCF.GetRequestStatusRemarks(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "</td>" +
                                                                                            "</tr>";
    intCtr++;
   }
   dr.Close();
  }

  Response.Write(strWrite);

  if (intCtr == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[" + intCtr + " record(s) found]</td></tr>");
 }

 protected void LoadPaging()
 {
  int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
  int intTRows = 0;
  int intTRowsTemp = 0;
  int intPage = 1;

  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   if (ddlRequestStatus.SelectedValue == "all")
   {
    if (ddlRequestType.SelectedValue == "all")
     cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
    else
     cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND reqtype='" + ddlRequestType.SelectedValue + "'";
   }
   else
   {
    if (ddlRequestType.SelectedValue == "all")
     cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='" + ddlRequestStatus.SelectedValue + "'";
    else
     cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='" + ddlRequestStatus.SelectedValue + "' AND reqtype='" + ddlRequestType.SelectedValue + "'";
   }

   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   dr.Read();
   if (!Convert.IsDBNull(dr["tcount"]))
    intTRows = Convert.ToInt32(dr["tcount"]);
   dr.Close();
  }

  intTRowsTemp = intTRows;

  while (intTRowsTemp > 0)
  {
   if (Convert.ToInt32(Request.QueryString["page"]) == intPage)
    Response.Write((intPage == 1 ? "" : ",") + "&nbsp;" + intPage + "");
   else
    Response.Write("&nbsp;&nbsp;<a href='MRCFAll.aspx?page=" + intPage + "'>" + intPage + "</a>");
   intPage++;
   intTRowsTemp -= intPageSize;
  }
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!Page.IsPostBack)
  {
   DataTable tblRequestType = new DataTable();
   using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT reqtype,typename FROM CIS.MrcfRequestType ORDER BY typename";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblRequestType);
   }

   ddlRequestType.DataSource = tblRequestType;
   ddlRequestType.DataValueField = "reqtype";
   ddlRequestType.DataTextField = "typename";
   ddlRequestType.DataBind();

   ListItem itm = new ListItem("View All", "all");
   itm.Selected = true;
   ddlRequestType.Items.Add(itm);
  }
 }

 protected void btnSearch_Click(object sender, EventArgs e)
 {

 }

}
