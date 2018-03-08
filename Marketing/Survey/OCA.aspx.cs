using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Survey_OCA : System.Web.UI.Page
{

 protected bool IsAnswered()
 {
  bool blnReturn = true;
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT username FROM survey_users_answers WHERE username=@username AND srvycode='1'";
   cmd.Parameters.Add(new SqlParameter("@username", Request.Cookies["Speedo"]["UserName"].ToString()));
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   blnReturn = dr.Read();
   dr.Close();
  }
  return blnReturn;
 }

 protected void CheckItems()
 {
  string strMessage = "";

  foreach (DataGridItem ditmCategory in dgSurveyCategory.Items)
  {
   DataGrid pdgItems = (DataGrid)ditmCategory.FindControl("dgItems");
   foreach (DataGridItem ditm in pdgItems.Items)
   {
    Label plblItemNumber = (Label)ditm.FindControl("lblItemNumber");
    RadioButton pradOption1 = (RadioButton)ditm.FindControl("radOption1");
    RadioButton pradOption2 = (RadioButton)ditm.FindControl("radOption2");
    RadioButton pradOption3 = (RadioButton)ditm.FindControl("radOption3");
    RadioButton pradOption4 = (RadioButton)ditm.FindControl("radOption4");
    RadioButton pradOption5 = (RadioButton)ditm.FindControl("radOption5");
    if (!pradOption1.Checked && !pradOption2.Checked && !pradOption3.Checked && !pradOption4.Checked && !pradOption5.Checked)
    {
     strMessage = strMessage + "<br>Please answer item #" + plblItemNumber.Text;
    }
   }
  }
 
  lblMessage.Text = strMessage;
  trError.Visible = (strMessage != "");
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   if (this.IsAnswered())
   {
    Response.Redirect("SurveyMessage.aspx");
   }
   else
   {
    DataTable tblCategory = new DataTable();
    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT scatcode,scatname FROM survey_category WHERE srvycode='1' ORDER BY scatordr";
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblCategory);
    }
    dgSurveyCategory.DataSource = tblCategory;
    dgSurveyCategory.DataBind();

    foreach (DataGridItem ditm in dgSurveyCategory.Items)
    {
     DataTable tblQuestions = new DataTable();
     HiddenField phdnCategoryCode = (HiddenField)ditm.FindControl("hdnCategoryCode");
     HiddenField phdnCategoryName = (HiddenField)ditm.FindControl("hdnCategoryName");
     DataGrid pdgItems = (DataGrid)ditm.FindControl("dgItems");
     using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
     {
      SqlCommand cmd = cn.CreateCommand();
      cmd.CommandText = "SELECT itemcode,itemnmbr,itemdesc FROM survey_items WHERE srvycode='1' AND scatcode=@scatcode ORDER BY itemnmbr";
      cmd.Parameters.Add(new SqlParameter("@scatcode", phdnCategoryCode.Value));
      SqlDataAdapter da = new SqlDataAdapter(cmd);
      da.Fill(tblQuestions);
     }
     pdgItems.Columns[1].HeaderText = "&nbsp;" + phdnCategoryName.Value;
     pdgItems.DataSource = tblQuestions;
     pdgItems.DataBind();
    }
   }
  }
 }

 protected void btnSave_Click(object sender, ImageClickEventArgs e)
 {
  this.CheckItems();
  if (!trError.Visible)
  {
   string strUsername = Request.Cookies["Speedo"]["UserName"].ToString();
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString());
   cn.Open();
   SqlTransaction trn = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   cmd.Transaction = trn;
   cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + strUsername + "',@answer,'1',@itemcode)";
   cmd.Parameters.Add(new SqlParameter("@answer", ""));
   cmd.Parameters.Add(new SqlParameter("@itemcode", ""));
   try
   {
    foreach (DataGridItem ditmCategory in dgSurveyCategory.Items)
    {
     DataGrid pdgItems = (DataGrid)ditmCategory.FindControl("dgItems");
     foreach (DataGridItem ditm in pdgItems.Items)
     {
      Label plblItemNumber = (Label)ditm.FindControl("lblItemNumber");
      HiddenField phdnItemCode = (HiddenField)ditm.FindControl("hdnItemCode");
      RadioButton pradOption1 = (RadioButton)ditm.FindControl("radOption1");
      RadioButton pradOption2 = (RadioButton)ditm.FindControl("radOption2");
      RadioButton pradOption3 = (RadioButton)ditm.FindControl("radOption3");
      RadioButton pradOption4 = (RadioButton)ditm.FindControl("radOption4");
      RadioButton pradOption5 = (RadioButton)ditm.FindControl("radOption5");

      cmd.Parameters["@answer"].Value = (pradOption1.Checked ? "1" : (pradOption2.Checked ? "2" : (pradOption3.Checked ? "3" : (pradOption4.Checked ? "4" : "5"))));
      cmd.Parameters["@itemcode"].Value = phdnItemCode.Value;
      cmd.ExecuteNonQuery();
     }
    }
    trn.Commit();
   }
   catch
   {
    trn.Rollback();
   }
   finally
   {
    cn.Close();
   }
   Response.Redirect("TeamAssessment.aspx");
  }
 }

}