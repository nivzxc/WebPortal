using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using HRMS;

public partial class Survey_SurveyResults : System.Web.UI.Page
{

 protected int GetTotalParticipants()
 {
  int intResult;
  string strWhere = "";

  if (ddlDivision.SelectedValue != "ALL")
  {
   strWhere = " WHERE divicode='" + ddlDivision.SelectedValue + "'";

   if (ddlDepartment.SelectedValue != "ALL")
   {
    strWhere += (strWhere.Length == 0 ? " WHERE" : " AND") + " deptcode='" + ddlDepartment.SelectedValue + "'";
   }
  }

  if (strWhere.Length != 0)
  {
   strWhere = " WHERE username IN (SELECT username FROM HR.Employees " + strWhere + ")";
  }

  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT COUNT(DISTINCT username) FROM survey_users_answers" + strWhere;
   cn.Open();
   try { intResult = (int)cmd.ExecuteScalar(); }
   catch { intResult = 0; }
  }
  return intResult;
 }

 protected void Load_Result(string strSrvyCode)
 {
  int intTEmp = GetTotalParticipants();
  int intTotal;
  int intItemCount;
  string strWrite;

  string strWhere = "";

  if (ddlDivision.SelectedValue != "ALL")
  {
   strWhere = " WHERE divicode='" + ddlDivision.SelectedValue + "'";

   if (ddlDepartment.SelectedValue != "ALL")
   {
    strWhere += (strWhere.Length == 0 ? " WHERE" : " AND") + " deptcode='" + ddlDepartment.SelectedValue + "'";
   }
  }

  if (strWhere.Length != 0)
  {
   strWhere = " AND username IN (SELECT username FROM HR.Employees " + strWhere + ")";
  }

  DataTable tblCategory = new DataTable();
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT scatcode,scatname FROM survey_category WHERE srvycode='" + strSrvyCode + "' ORDER by scatordr";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   SqlDataReader dr;
   cn.Open();
   da.Fill(tblCategory);
   foreach (DataRow drw in tblCategory.Rows)
   {
    intTotal = 0;
    intItemCount = 0;
    strWrite = "<tr>" +
                "<td class='GridRows2' style='font-size:small;'>&nbsp;<b>" + drw["scatname"] + "</b></td>" +
                "<td class='GridRows2' style='font-size:small;'>&nbsp;</td>" +
                "<td class='GridRows2' style='font-size:small;'>&nbsp;</td>" +
               "</tr>";

    Response.Write(strWrite);
    cmd.CommandText = "SELECT itemdesc,itemnmbr,SUM(CAST(answer as INTEGER)) AS tans " +
                      "FROM survey_items INNER JOIN survey_users_answers ON survey_items.itemcode = survey_users_answers.itemcode " +
                      "WHERE survey_items.srvycode='" + strSrvyCode + "' AND scatcode='" + drw["scatcode"] + "'" + strWhere + " " +
                      "GROUP BY itemnmbr,itemdesc " +
                      "ORDER BY CAST(itemnmbr AS INTEGER)";
    dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     strWrite = "<tr>" +
                 "<td class='GridRows' style='font-size:small;'>&nbsp;" + dr["itemnmbr"] + ".&nbsp;" + dr["itemdesc"] + "</td>" +
                 "<td class='GridRows' style='text-align:center;'>" + dr["tans"] + "</td>" +
                 "<td class='GridRows' style='text-align:center;'>" + Convert.ToDouble((Convert.ToDouble(dr["tans"]) / (double)intTEmp)).ToString("##0.00") + "</td>" +
                "</tr>";
     Response.Write(strWrite);
     intTotal += (int)dr["tans"];
     intItemCount++;
    }
    dr.Close();
    strWrite = "<tr>" +
                "<td class='GridRows2' style='font-size:small; text-align:left;'>&nbsp;<b>Category Total</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + intTotal + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + Convert.ToDouble(((double)intTotal / ((double)intTEmp * intItemCount))).ToString("##0.00") + "</b></td>" +
               "</tr>" +
               "<tr><td class='GridRows' colspan='3'>&nbsp;</td></tr>";
    Response.Write(strWrite);
   }
  }
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   DataTable tblDivision = new DataTable();
   tblDivision.Columns.Add("pvalue");
   tblDivision.Columns.Add("ptext");
   DataRow drw = tblDivision.NewRow();
   drw["pvalue"] = "ALL";
   drw["ptext"] = "ALL";
   tblDivision.Rows.Add(drw);
   tblDivision.Merge(clsDivision.GetDdlDs());

   ddlDivision.DataSource = tblDivision;
   ddlDivision.DataValueField = "pvalue";
   ddlDivision.DataTextField = "ptext";
   ddlDivision.DataBind();
  }

 }

 protected void btnApply_Click(object sender, EventArgs e)
 {
  //lblTotalParticipants.Text = this.GetTotalParticipants().ToString();
 }

 protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
 {
   trDepartment.Visible = ddlDivision.SelectedValue != "ALL";

   if (ddlDivision.SelectedValue != "ALL")
   {
    DataTable tblDepartment = new DataTable();
    tblDepartment.Columns.Add("pvalue");
    tblDepartment.Columns.Add("ptext");
    DataRow drwDep = tblDepartment.NewRow();
    drwDep["pvalue"] = "ALL";
    drwDep["ptext"] = "ALL";
    tblDepartment.Rows.Add(drwDep);
    tblDepartment.Merge(clsDepartment.GetDdlDs(ddlDivision.SelectedValue));

    ddlDepartment.DataSource = tblDepartment;
    ddlDepartment.DataValueField = "pvalue";
    ddlDepartment.DataTextField = "ptext";
    ddlDepartment.DataBind();
  }
 }

}