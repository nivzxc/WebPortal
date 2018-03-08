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
using System.IO;
using HRMS;
public partial class CMD_CRS_CRSNew : System.Web.UI.Page
{

   protected string GetRequestedCourseware()
   {
      string strReturn = "";
      foreach (DataGridItem ditm in dgRCW.Items)
      {
         Label plblCourseCode = (Label)ditm.FindControl("lblCourseCode");
         if (strReturn == "")
            strReturn = plblCourseCode.Text;
         else
            strReturn = strReturn + "','" + plblCourseCode.Text;
      }
      return strReturn;
   }

   protected void BindCoursewareList(string pSortEx)
   {
      DataTable tblCourseware = new DataTable("cwr");
      tblCourseware.Columns.Add("crsecode", System.Type.GetType("System.String"));
      tblCourseware.Columns.Add("crsettle", System.Type.GetType("System.String"));
      tblCourseware.Columns.Add("yearterm", System.Type.GetType("System.String"));
      tblCourseware.Columns.Add("avail", System.Type.GetType("System.String"));
      tblCourseware.Columns.Add("ordernum", System.Type.GetType("System.Int32"));

      DataTable tblSubjects = new DataTable();
      using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
      {
         SqlCommand cmdOmega = cnOmega.CreateCommand();
         if (ddlYearTerm.SelectedValue == "all")
            cmdOmega.CommandText = "SELECT DISTINCT dbo_subject.subject_code,subject_name,dbo_mcurriculum_aux.year_level,dbo_mcurriculum_aux.term FROM dbo_mcurriculum_aux INNER JOIN dbo_subject ON dbo_mcurriculum_aux.subject_code = dbo_subject.subject_code WHERE cur_ref_no='" + ddlCurriculum.SelectedValue + "' AND dbo_subject.subject_code NOT IN ('" + GetRequestedCourseware() + "') ORDER BY dbo_mcurriculum_aux.year_level,dbo_mcurriculum_aux.term,subject_name";
         else
            cmdOmega.CommandText = "SELECT DISTINCT dbo_subject.subject_code,subject_name,dbo_mcurriculum_aux.year_level,dbo_mcurriculum_aux.term FROM dbo_mcurriculum_aux INNER JOIN dbo_subject ON dbo_mcurriculum_aux.subject_code = dbo_subject.subject_code WHERE cur_ref_no='" + ddlCurriculum.SelectedValue + "' AND dbo_subject.subject_code NOT IN ('" + GetRequestedCourseware() + "') AND dbo_mcurriculum_aux.year_level='" + ddlYearTerm.SelectedValue.Substring(0, 1) + "' AND dbo_mcurriculum_aux.term='" + ddlYearTerm.SelectedValue.Substring(2, 1) + "' ORDER BY dbo_mcurriculum_aux.year_level,dbo_mcurriculum_aux.term,subject_name";
         SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
         daOmega.Fill(tblSubjects);

         foreach (DataRow drow in tblSubjects.Rows)
         {
            DataRow rwNewCourseware;
            rwNewCourseware = tblCourseware.NewRow();
            rwNewCourseware["crsecode"] = drow["subject_code"].ToString();
            rwNewCourseware["crsettle"] = drow["subject_name"].ToString();
            rwNewCourseware["yearterm"] = drow["year_level"].ToString() + "-" + drow["term"].ToString();
            rwNewCourseware["avail"] = clsCRS.ToCAStatusDesc(clsCRS.GetCAStatus(drow["subject_code"].ToString()));
            rwNewCourseware["ordernum"] = clsCRS.OrderedCoursewareCount(Request.QueryString["schlcode"], drow["subject_code"].ToString());
            tblCourseware.Rows.Add(rwNewCourseware);
         }
      }

      tblCourseware.DefaultView.Sort = pSortEx;
      dgCW.DataSource = tblCourseware.DefaultView;
      dgCW.DataBind();

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         SqlDataReader dr;
         cn.Open();
         foreach (DataGridItem ditm in dgCW.Items)
         {
            CheckBox pchkInclude = (CheckBox)ditm.FindControl("chkInclude");
            Label plblCourseCode = (Label)ditm.FindControl("lblCourseCode");
            cmd.CommandText = "SELECT pstatus FROM Cm.CrsDetails WHERE crsecode='" + plblCourseCode.Text + "' AND pstatus IN ('F','E') AND crscode IN (SELECT crscode FROM CM.Crs WHERE schlcode='" + Request.QueryString["schlcode"] + "')";
            dr = cmd.ExecuteReader();
            pchkInclude.Enabled = !dr.Read();
            dr.Close();
         }
      }

      dgCW.Columns[2].Visible = (ddlYearTerm.SelectedValue == "all");
   }

   protected void BindCurriculum()
   {
      DataTable tblCurriculum = new DataTable();
      using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
      {
         SqlCommand cmdOmega = cnOmega.CreateCommand();
         cmdOmega.CommandText = "SELECT DISTINCT cur_ref_no FROM dbo_pmatrix_cur WHERE branch_code='" + Request.QueryString["schlcode"] + "' AND ind_active='A' AND course_code='" + ddlProgram.SelectedValue + "'";
         SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
         daOmega.Fill(tblCurriculum);
      }
      ddlCurriculum.DataSource = tblCurriculum;
      ddlCurriculum.DataTextField = "cur_ref_no";
      ddlCurriculum.DataValueField = "cur_ref_no";
      ddlCurriculum.DataBind();
   }

   protected void BindYearTerm()
   {
      DataTable tblYearTerm = new DataTable();
      using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
      {
         SqlCommand cmdOmega = cnOmega.CreateCommand();
         cmdOmega.CommandText = "SELECT DISTINCT CAST(year_level AS VARCHAR) + '-' + CAST(term AS VARCHAR) AS yearterm FROM dbo_mcurriculum_aux WHERE cur_ref_no='" + ddlCurriculum.SelectedValue + "' ORDER BY yearterm";
         SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
         daOmega.Fill(tblYearTerm);
      }

      ddlYearTerm.Items.Clear();
      ListItem litm = new ListItem("all", "all");
      ddlYearTerm.Items.Add(litm);
      foreach (DataRow drow in tblYearTerm.Rows)
      {
         ListItem litmnew = new ListItem(drow["yearterm"].ToString(), drow["yearterm"].ToString());
         ddlYearTerm.Items.Add(litmnew);
      }
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         DataTable tblCWR = new DataTable("CWR");
         tblCWR.Columns.Add("currcode", System.Type.GetType("System.String"));
         tblCWR.Columns.Add("crsecode", System.Type.GetType("System.String"));
         tblCWR.Columns.Add("crsettle", System.Type.GetType("System.String"));
         tblCWR.Columns.Add("yearterm", System.Type.GetType("System.String"));
         tblCWR.Columns.Add("avail", System.Type.GetType("System.String"));
         tblCWR.Columns.Add("ordernum", System.Type.GetType("System.Int32"));
         tblCWR.Columns.Add("price", System.Type.GetType("System.Int32"));
         ViewState["CWR"] = tblCWR;

         DataTable tblAttachments = new DataTable("Attachments");
         tblAttachments.Columns.Add("filename", System.Type.GetType("System.String"));
         tblAttachments.Columns.Add("filepath", System.Type.GetType("System.String"));
         tblAttachments.Columns.Add("details", System.Type.GetType("System.String"));
         ViewState["Attachments"] = tblAttachments;

         txtUsername.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);
         txtSchlName.Text = clsSchool.GetSchoolName(Request.QueryString["schlcode"]);

         DataTable tblPrograms = new DataTable();
         using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
         {
            SqlCommand cmdOmega = cnOmega.CreateCommand();
            cmdOmega.CommandText = "SELECT DISTINCT dbo_course.course_code, course_name FROM dbo_pmatrix_cur INNER JOIN dbo_course ON dbo_pmatrix_cur.course_code = dbo_course.course_code WHERE branch_code='" + Request.QueryString["schlcode"] + "' AND ind_active = 'A' ORDER BY course_name";
            SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
            cnOmega.Open();
            daOmega.Fill(tblPrograms);
         }
         ddlProgram.DataSource = tblPrograms;
         ddlProgram.DataTextField = "course_name";
         ddlProgram.DataValueField = "course_code";
         ddlProgram.DataBind();
         BindCurriculum();
         BindYearTerm();
         BindCoursewareList("yearterm");
      }
   }

   protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
   {
      BindCurriculum();
      BindYearTerm();
      BindCoursewareList("yearterm");
   }

   protected void ddlCurriculum_SelectedIndexChanged(object sender, EventArgs e)
   {
      BindYearTerm();
      BindCoursewareList("yearterm");
   }

   protected void ddlYearTerm_SelectedIndexChanged(object sender, EventArgs e)
   {
      BindCoursewareList("yearterm");
   }

   protected void btnExclude_Click(object sender, ImageClickEventArgs e)
   {
      DataTable tblCWR = ViewState["CWR"] as DataTable;
      foreach (DataGridItem ditm in dgRCW.Items)
      {
         CheckBox pchkDelete = (CheckBox)ditm.FindControl("chkDelete");
         Label plblCourseCode = (Label)ditm.FindControl("lblCourseCode");
         foreach (DataRow drow in tblCWR.Rows)
         {
            if (pchkDelete.Checked && drow["crsecode"].ToString() == plblCourseCode.Text)
            {
               drow.Delete();
               break;
            }
         }
      }
      ViewState["CWR"] = tblCWR;
      dgRCW.DataSource = tblCWR;
      dgRCW.Columns[4].FooterText = Convert.ToDouble(tblCWR.Compute("SUM(price)", String.Empty).ToString()).ToString("###,##0.00") + "&nbsp;";
      dgRCW.DataBind();
      BindCoursewareList("yearterm");
   }

   protected void btnSend_Click(object sender, ImageClickEventArgs e)
   {
      if (dgRCW.Items.Count != 0)
      {
         string strCWRCode = "";
         string strCMHName = clsCRS.GetCmh();
         SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString());
         cn.Open();
         SqlTransaction tran = cn.BeginTransaction();
         SqlCommand cmd = cn.CreateCommand();
         cmd.Transaction = tran;
         try
         {
            int intTForApproval = 0;

            foreach (DataGridItem ditm in dgRCW.Items)
            {
               Label plblTRequest = (Label)ditm.FindControl("lblTRequest");
               if (Convert.ToInt16(plblTRequest.Text) > 0)
                  intTForApproval++;
            }

            cmd.CommandText = "spCWRInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@schlcode", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("@cmname", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@cmhname", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@ccname", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@datereq", SqlDbType.DateTime);
            cmd.Parameters.Add("@tcost", SqlDbType.Float);
            cmd.Parameters.Add("@treorder", SqlDbType.Int);
            cmd.Parameters.Add("@crscode", SqlDbType.Char, 11);

            cmd.Parameters["@schlcode"].Value = Request.QueryString["schlcode"];
            cmd.Parameters["@cmname"].Value = Request.Cookies["Speedo"]["UserName"].ToString();
            cmd.Parameters["@remarks"].Value = txtRemarks.Text;
            cmd.Parameters["@cmhname"].Value = clsCRS.GetCmh();
            cmd.Parameters["@ccname"].Value = clsCRS.GetCc();
            cmd.Parameters["@datereq"].Value = DateTime.Now;
            cmd.Parameters["@tcost"].Value = intTForApproval * clsCRS.GetCoursewarePrice();
            cmd.Parameters["@treorder"].Value = intTForApproval;
            cmd.Parameters["@crscode"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            strCWRCode = cmd.Parameters["@crscode"].Value.ToString();

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO CM.CrsDetails(crscode,crsecode,crsettle,yearterm,currcode,ordernum,price,pstatus) VALUES(@crscode,@crsecode,@crsettle,@yearterm,@currcode,@ordernum,@price,@pstatus)";
            cmd.Parameters.Add("@crscode", SqlDbType.Char, 11);
            cmd.Parameters.Add("@crsecode", SqlDbType.VarChar, 10);
            cmd.Parameters.Add("@crsettle", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@yearterm", SqlDbType.VarChar, 10);
            cmd.Parameters.Add("@currcode", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@ordernum", SqlDbType.Int);
            cmd.Parameters.Add("@price", SqlDbType.Float);
            cmd.Parameters.Add("@pstatus", SqlDbType.Char, 1);
            cmd.Parameters["@crscode"].Value = strCWRCode;

            foreach (DataGridItem ditm in dgRCW.Items)
            {
               Label plblCourseCode = (Label)ditm.FindControl("lblCourseCode");
               Label plblCourseDesc = (Label)ditm.FindControl("lblCourseDesc");
               Label plblYearTerm = (Label)ditm.FindControl("lblYearTerm");
               Label plblCurrCode = (Label)ditm.FindControl("lblCurrCode");
               Label plblTRequest = (Label)ditm.FindControl("lblTRequest");
               int intTRequest = Convert.ToInt32(plblTRequest.Text) + 1;


               cmd.Parameters["@crsecode"].Value = plblCourseCode.Text;
               cmd.Parameters["@crsettle"].Value = plblCourseDesc.Text;
               cmd.Parameters["@yearterm"].Value = plblYearTerm.Text;
               cmd.Parameters["@currcode"].Value = plblCurrCode.Text;
               cmd.Parameters["@ordernum"].Value = intTRequest;

               if (intTRequest > 1 && strCMHName != Request.Cookies["Speedo"]["UserName"])
               {
                  cmd.Parameters["@price"].Value = clsCRS.GetCoursewarePrice();
                  cmd.Parameters["@pstatus"].Value = "F";
               }
               else
               {
                  cmd.Parameters["@price"].Value = "0";
                  cmd.Parameters["@pstatus"].Value = "E";
               }
               cmd.ExecuteNonQuery();
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "INSERT INTO CM.CrsAttachment VALUES('" + strCWRCode + "',@filename,@details,@filecont)";
            cmd.Parameters.Add("@filename", SqlDbType.VarChar, 20);
            cmd.Parameters.Add("@details", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@filecont", SqlDbType.Image);
            int intCtr = 0;
            foreach (DataGridItem ditm in dgAttachments.Items)
            {
               Label plblDetails = (Label)ditm.FindControl("lblDetails");
               Label plblFilePath = (Label)ditm.FindControl("lblFilePath");
               HiddenField phdnFileName = (HiddenField)ditm.FindControl("hdnFileName");

               FileStream fsUpload = new FileStream(plblFilePath.Text, FileMode.OpenOrCreate, FileAccess.Read);
               Byte[] bytUpload = new Byte[fsUpload.Length];

               fsUpload.Read(bytUpload, 0, Convert.ToInt32(fsUpload.Length));
               fsUpload.Close();

               cmd.Parameters["@filename"].Value = strCWRCode + intCtr + Path.GetExtension(phdnFileName.Value);
               cmd.Parameters["@details"].Value = plblDetails.Text;
               cmd.Parameters["@filecont"].Value = bytUpload;
               cmd.ExecuteNonQuery();
               intCtr++;
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
         Response.Redirect("CRSMenu.aspx");
      }
   }

   protected void btnInclude_Click(object sender, ImageClickEventArgs e)
   {
      try
      {
         DataTable tblCWR = ViewState["CWR"] as DataTable;
         foreach (DataGridItem ditm in dgCW.Items)
         {
            CheckBox pchkInclude = (CheckBox)ditm.FindControl("chkInclude");
            Label plblCourseCode = (Label)ditm.FindControl("lblCourseCode");
            Label plblCourseDesc = (Label)ditm.FindControl("lblCourseDesc");
            Label plblYearTerm = (Label)ditm.FindControl("lblYearTerm");
            Label plblAvailability = (Label)ditm.FindControl("lblAvailability");
            Label plblTRequest = (Label)ditm.FindControl("lblTRequest");
            if (pchkInclude.Checked)
            {
               DataRow drowCWR = tblCWR.NewRow();
               drowCWR["currcode"] = ddlCurriculum.SelectedValue;
               drowCWR["crsecode"] = plblCourseCode.Text;
               drowCWR["crsettle"] = plblCourseDesc.Text;
               drowCWR["yearterm"] = plblYearTerm.Text;
               drowCWR["avail"] = plblAvailability.Text;
               drowCWR["ordernum"] = plblTRequest.Text;
               drowCWR["price"] = (Convert.ToInt32(plblTRequest.Text) > 0 ? clsCRS.GetCoursewarePrice() : 0);
               tblCWR.Rows.Add(drowCWR);
            }
         }
         tblCWR.DefaultView.Sort = "currcode,yearterm,crsettle";
         ViewState["CWR"] = tblCWR;
         dgRCW.DataSource = tblCWR;
         dgRCW.Columns[5].FooterText = Convert.ToDouble(tblCWR.Compute("SUM(price)", String.Empty).ToString()).ToString("###,##0.00") + "&nbsp;";
         dgRCW.DataBind();
         BindCoursewareList("yearterm");
      }
      catch
      {
         Response.Redirect("CRSNew.aspx?schlcode=" + Request.QueryString["schlcode"]);
      }
   }

   protected void dgCW_SortCommand(object source, DataGridSortCommandEventArgs e)
   {
      BindCoursewareList(e.SortExpression.ToString());
   }

   protected void dgAttachments_DeleteCommand(object source, DataGridCommandEventArgs e)
   {
      try
      {
         DataTable tblAttachments = ViewState["Attachments"] as DataTable;
         tblAttachments.Rows[e.Item.ItemIndex].Delete();
         ViewState["Attachments"] = tblAttachments;

         dgAttachments.DataSource = tblAttachments;
         dgAttachments.DataBind();
      }
      catch
      {
         Response.Redirect("MRCFNew.aspx");
      }
   }

   protected void btnAttach_Click(object sender, ImageClickEventArgs e)
   {
      try
      {
         DataTable tblAttachments = ViewState["Attachments"] as DataTable;
         DataRow drow = tblAttachments.NewRow();
         drow["filename"] = fldAttach.FileName;
         drow["filepath"] = fldAttach.PostedFile.FileName;
         drow["details"] = txtAttachDetails.Text;
         tblAttachments.Rows.Add(drow);

         tblAttachments.DefaultView.Sort = "details";
         ViewState["Attachments"] = tblAttachments;
         dgAttachments.DataSource = tblAttachments;
         dgAttachments.DataBind();
         BindCoursewareList("yearterm");

         txtAttachDetails.Text = "";
      }
      catch
      {
         Response.Redirect("CRSNew.aspx?schlcode=" + Request.QueryString["schlcode"]);
      }
   }
}
