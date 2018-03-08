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
using HRMS;
using STIeForms;

public partial class CIS_Transmittal_TranNew : System.Web.UI.Page
{

 protected void ShowSpecialDetailsVisible(bool pShowFlag)
 {
  trChargeTo.Visible = pShowFlag;
  trDateNeeded.Visible = pShowFlag;
  trGrpHead.Visible = pShowFlag;
 }

 protected void LoadApprover(string strRCCode)
 {
  //DataTable tblGroupHead = new DataTable();
  //using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  //{
  // SqlCommand cmd = cn.CreateCommand();
  // cmd.CommandText = "SELECT username,firname + ' ' + lastname AS name FROM Users.Users WHERE username IN (SELECT username FROM CIS.TransmittalApprover WHERE rccode='" + strRCCode + "' AND userlvl='grouphead')";
  // cn.Open();
  // SqlDataAdapter da = new SqlDataAdapter(cmd);
  // da.Fill(tblGroupHead);
  // ddlGrpHead.DataSource = tblGroupHead;
  // ddlGrpHead.DataValueField = "username";
  // ddlGrpHead.DataTextField = "name";
  // ddlGrpHead.DataBind();

  // if (ddlGrpHead.Items.Count == 0)
  // {
  //  ListItem itm = new ListItem("No Approver Defined", "none");
  //  ddlGrpHead.Items.Add(itm);
  // }
  //}
     string strChargeRC = radSpecialSchool.Checked == true ? hdnRCCode.Value : ddlChargeTo.SelectedValue.ToString();

     DataTable tblGroupHeadApprover = clsModuleApprover.DSLRCApprover(clsModule.TransmittalModule, "1", strChargeRC);
     ddlGrpHead.DataSource = tblGroupHeadApprover;
     ddlGrpHead.DataValueField = "pvalue";
     ddlGrpHead.DataTextField = "ptext";
     ddlGrpHead.DataBind();
     if (ddlGrpHead.Items.Count == 0)
     {
         btnSave.Enabled = false;
         ddlGrpHead.Items.Add(new ListItem("No Approver Defined", "none"));
     }
     else
     {
         btnSave.Enabled = true;
     }
 }

 protected void LoadChargeTo(clsTransmittal.DispatchTypes dtDispatchType)
 {
  DataTable tblChargeTo = new DataTable();
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   if (dtDispatchType == clsTransmittal.DispatchTypes.SpecialHQ)
    cmd.CommandText = "SELECT rccode AS pvalue,rcname AS ptext FROM HR.Rc ORDER BY rcname";
   else if (dtDispatchType == clsTransmittal.DispatchTypes.SpecialSchool)
    cmd.CommandText = "SELECT schlcode AS pvalue,schlnam2 AS ptext FROM CM.Schools ORDER BY schlnam2";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   cn.Open();
   da.Fill(tblChargeTo);
  }
  ddlChargeTo.DataSource = tblChargeTo;
  ddlChargeTo.DataValueField = "pvalue";
  ddlChargeTo.DataTextField = "ptext";
  ddlChargeTo.DataBind();

  if (dtDispatchType == clsTransmittal.DispatchTypes.SpecialHQ)
  {
   foreach (ListItem itm in ddlChargeTo.Items)
   {
    if (itm.Value == hdnRCCode.Value)
    {
     itm.Selected = true;
     break;
    }
   }
  }
 }

 protected void LoadSchools()
 {
  DataTable tblSchools = new DataTable();
  DataTable tblIncSchools = new DataTable();
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   if (ddlSchlGroup.SelectedValue == "ALL")
    cmd.CommandText = "SELECT schlcode,schlnam2 FROM CM.Schools WHERE schlcode NOT IN ('" + hdnSelSchl.Value + "') AND pstatus='1' ORDER BY schlnam2";
   else
    cmd.CommandText = "SELECT schlcode,schlnam2 FROM CM.Schools WHERE CM.Schools.schlcode NOT IN ('" + hdnSelSchl.Value + "') AND pstatus='1' AND schlcode IN (SELECT schlcode FROM CM.SchoolGroupingDetails WHERE scgrcode='" + ddlSchlGroup.SelectedValue + "') ORDER BY schlnam2";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   cn.Open();
   da.Fill(tblSchools);
   da.SelectCommand.CommandText = "SELECT schlcode,schlnam2 FROM CM.Schools WHERE schlcode IN ('" + hdnSelSchl.Value + "') AND pstatus='1' ORDER BY schlnam2";
   da.Fill(tblIncSchools);
  }

  cblSchools.DataSource = tblSchools.DefaultView;
  cblSchools.DataTextField = "schlnam2";
  cblSchools.DataValueField = "schlcode";
  cblSchools.DataBind();

  cblIncluded.DataSource = tblIncSchools.DefaultView;
  cblIncluded.DataTextField = "schlnam2";
  cblIncluded.DataValueField = "schlcode";
  cblIncluded.DataBind();
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
  btnSave.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSave.Page.ClientScript.GetPostBackEventReference(btnSave, string.Empty).ToString() + ";return CheckIsRepeat();}");
  if (!Page.IsPostBack)
  {
   string strUsername = Request.Cookies["Speedo"]["UserName"];

   radRegular.Checked = true;
   dtpDateNeeded.MinDate = DateTime.Now.AddDays(-1);
   DataTable tblSchlGrp = new DataTable();
   DataTable tblSchools = new DataTable();

   txtRequestorName.Text = clsEmployee.GetName(strUsername, EmployeeNameFormat.LastFirst);
   hdnRCCode.Value = clsEmployee.GetRCCode(strUsername);
   txtRCName.Text = clsRC.GetRCName(hdnRCCode.Value);


   using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    cmd.CommandText = "SELECT scgrcode,scgrname FROM CM.SchoolGrouping ORDER BY scgrname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblSchlGrp);

    cmd.CommandText = "SELECT schlcode,schlnam2 FROM CM.Schools WHERE pstatus='1' AND schlcode NOT IN ('" + hdnSelSchl.Value + "') ORDER BY schlnam2";
    da.SelectCommand = cmd;
    da.Fill(tblSchools);
   }
   ddlSchlGroup.DataSource = tblSchlGrp.DefaultView;
   ddlSchlGroup.DataTextField = "scgrname";
   ddlSchlGroup.DataValueField = "scgrcode";
   ddlSchlGroup.DataBind();
   ddlSchlGroup.SelectedValue = "ALL";
   tblSchlGrp.Dispose();

   cblSchools.DataSource = tblSchools.DefaultView;
   cblSchools.DataTextField = "schlnam2";
   cblSchools.DataValueField = "schlcode";
   cblSchools.DataBind();
   tblSchools.Dispose();

   radRegular.Checked = true;
   ShowSpecialDetailsVisible(false);
  }
 }

 protected void radRegular_CheckedChanged(object sender, EventArgs e)
 {
  ShowSpecialDetailsVisible(false);
 }

 protected void radSpecialHQ_CheckedChanged(object sender, EventArgs e)
 {
  ShowSpecialDetailsVisible(true);
  LoadChargeTo(clsTransmittal.DispatchTypes.SpecialHQ);
  LoadApprover(ddlChargeTo.SelectedValue);
  ddlChargeTo.AutoPostBack = true;
 }

 protected void radSpecialSchool_CheckedChanged(object sender, EventArgs e)
 {
  ShowSpecialDetailsVisible(true);
  LoadChargeTo(clsTransmittal.DispatchTypes.SpecialSchool);
  LoadApprover(hdnRCCode.Value);
  ddlChargeTo.AutoPostBack = false;
 }

 protected void ddlChargeTo_SelectedIndexChanged(object sender, EventArgs e)
 {
  if (radSpecialHQ.Checked)
   LoadApprover(ddlChargeTo.SelectedValue);
 }

 protected void ddlSchlGroup_SelectedIndexChanged(object sender, EventArgs e)
 {
  DataTable tblSchools = new DataTable();
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   if (ddlSchlGroup.SelectedValue == "ALL")
       cmd.CommandText = "SELECT schlcode,schlnam2 FROM CM.Schools WHERE pstatus='1' AND schlcode NOT IN ('" + hdnSelSchl.Value + "') ORDER BY schlnam2";
   else
       cmd.CommandText = "SELECT schlcode,schlnam2 FROM CM.Schools WHERE pstatus='1' AND CM.Schools.schlcode NOT IN ('" + hdnSelSchl.Value + "') AND schlcode IN (SELECT schlcode FROM CM.SchoolGroupingDetails WHERE scgrcode='" + ddlSchlGroup.SelectedValue + "') ORDER BY schlnam2";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   cn.Open();
   da.Fill(tblSchools);
  }

  cblSchools.DataSource = tblSchools.DefaultView;
  cblSchools.DataTextField = "schlnam2";
  cblSchools.DataValueField = "schlcode";
  cblSchools.DataBind();
  tblSchools.Dispose();
 }

 protected void btnInclude_Click(object sender, EventArgs e)
 {
  foreach (ListItem itm in cblSchools.Items)
  {
   if (itm.Selected)
    hdnSelSchl.Value = (hdnSelSchl.Value == "" ? itm.Value : (hdnSelSchl.Value + "','" + itm.Value));
  }
  LoadSchools();
 }

 protected void btnRemove_Click(object sender, EventArgs e)
 {
  hdnSelSchl.Value = "";
  foreach (ListItem itm in cblIncluded.Items)
  {
   if (!itm.Selected)
    hdnSelSchl.Value = (hdnSelSchl.Value == "" ? itm.Value : (hdnSelSchl.Value + "','" + itm.Value));
  }
  LoadSchools();
 }

 protected void btnSave_Click(object sender, EventArgs e)
 {
  if (cblIncluded.Items.Count != 0)
  {
   string strTransmittalCode = "";
   string strUserEmail = clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString());
   SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString());
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = new SqlCommand("spTRANInsert", cn);
   cmd.Transaction = tran;
   try
   {
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@datereq", SqlDbType.DateTime);
    cmd.Parameters.Add("@dateneed", SqlDbType.DateTime);
    cmd.Parameters.Add("@itemdesc", SqlDbType.VarChar, 100);
    cmd.Parameters.Add("@unit", SqlDbType.VarChar, 20);
    cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 200);
    cmd.Parameters.Add("@disptype", SqlDbType.Char, 1);
    cmd.Parameters.Add("@chargeto", SqlDbType.VarChar, 10);
    cmd.Parameters.Add("@grphname", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@grphstat", SqlDbType.Char, 1);
    cmd.Parameters.Add("@appname", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@appstat", SqlDbType.Char, 1);
    cmd.Parameters.Add("@status", SqlDbType.Char, 1);
    cmd.Parameters.Add("@trancode", SqlDbType.Char, 9);

    cmd.Parameters["@username"].Value = Request.Cookies["Speedo"]["UserName"];
    cmd.Parameters["@datereq"].Value = DateTime.Now;
    cmd.Parameters["@dateneed"].Value = dtpDateNeeded.Date;
    cmd.Parameters["@itemdesc"].Value = txtItem.Text;
    cmd.Parameters["@unit"].Value = ddlUnit.SelectedValue;
    cmd.Parameters["@remarks"].Value = txtRemarks.Text;
    if (radRegular.Checked)
    {
     cmd.Parameters["@disptype"].Value = "R";
     cmd.Parameters["@chargeto"].Value = "";
     cmd.Parameters["@grphname"].Value = "";
     cmd.Parameters["@grphstat"].Value = "N";
     cmd.Parameters["@appname"].Value = "";
     cmd.Parameters["@appstat"].Value = "N";
     cmd.Parameters["@status"].Value = "A";
    }
    else
    {
     if (radSpecialHQ.Checked)
      cmd.Parameters["@disptype"].Value = "H";
     else if (radSpecialSchool.Checked)
      cmd.Parameters["@disptype"].Value = "S";
     cmd.Parameters["@chargeto"].Value = ddlChargeTo.SelectedValue;
     cmd.Parameters["@grphname"].Value = ddlGrpHead.SelectedValue;
     cmd.Parameters["@appname"].Value = clsTransmittal.GetApprover();
     if (ddlGrpHead.SelectedValue == Request.Cookies["Speedo"]["UserName"].ToString())
     {
      cmd.Parameters["@grphstat"].Value = "A";
      cmd.Parameters["@appstat"].Value = "F";
      cmd.Parameters["@status"].Value = "F";
     }
     else
     {
      clsTransmittal.TransmittalUserType tranuserlvl = clsTransmittal.GetUserLevel(Request.Cookies["Speedo"]["UserName"].ToString());
      if (tranuserlvl == clsTransmittal.TransmittalUserType.SpecialDispatchApprover || tranuserlvl == clsTransmittal.TransmittalUserType.SpecialDispatchApprover2)
      {
       cmd.Parameters["@grphstat"].Value = "A";
       cmd.Parameters["@appstat"].Value = "A";
       cmd.Parameters["@status"].Value = "A";
      }
      else if (tranuserlvl == clsTransmittal.TransmittalUserType.Requestor)
      {
       cmd.Parameters["@grphstat"].Value = "F";
       cmd.Parameters["@appstat"].Value = "F";
       cmd.Parameters["@status"].Value = "F";
      }
     }
    }
    cmd.Parameters["@trancode"].Direction = ParameterDirection.Output;
    cmd.ExecuteNonQuery();
    strTransmittalCode = cmd.Parameters["@trancode"].Value.ToString();
    cmd.Parameters.Clear();

    cmd.CommandType = CommandType.Text;
    foreach (ListItem itm in cblIncluded.Items)
    {
     cmd.CommandText = "INSERT INTO CIS.TransmittalDetails(trancode,schlcode,qty,status) VALUES('" + strTransmittalCode + "','" + itm.Value + "','" + txtQty.Text + "','0')";
     cmd.ExecuteNonQuery();
    }

    if (radSpecialHQ.Checked || radSpecialSchool.Checked)
    {
     if (ddlGrpHead.SelectedValue == Request.Cookies["Speedo"]["UserName"].ToString())
     {
      string strApprover = clsTransmittal.GetApprover();
      string strApprover2 = clsTransmittal.GetApprover2();
      clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.SentToRequestor, txtRequestorName.Text, clsUsers.GetName(strApprover), strUserEmail, strTransmittalCode);
      //clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.SentToApprover, txtRequestorName.Text, clsUsers.GetName(strApprover), clsUsers.GetEmail(strApprover), strTransmittalCode);
      //clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.SentToApprover, txtRequestorName.Text, clsUsers.GetName(strApprover2), clsUsers.GetEmail(strApprover2), strTransmittalCode);
     }
     else if (clsTransmittal.GetUserLevel(Request.Cookies["Speedo"]["UserName"].ToString()) == clsTransmittal.TransmittalUserType.Requestor)
     {
      clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.SentToRequestor, txtRequestorName.Text, ddlGrpHead.SelectedItem.Text, strUserEmail, strTransmittalCode);
      clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.SentToApproverGH, txtRequestorName.Text, ddlGrpHead.SelectedItem.Text, clsUsers.GetEmail(ddlGrpHead.SelectedValue), strTransmittalCode);
     }
    }

    tran.Commit();
   }
   catch
   {
    tran.Rollback();
   }
   finally
   {
    cn.Close();
   }

   Response.Redirect("TranMenu.aspx");
  }
 }

}