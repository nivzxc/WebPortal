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
using System.Text;
using GrayMatterSoft;

public partial class CMD_CRS_CRSDispatchDetailsCC : System.Web.UI.Page
{

   public void BindItems()
   {
      DataTable tblDispatchDetails = new DataTable();
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT datentry,dispdeta,disptype,CAST(DATEPART(mm,datedisp) AS VARCHAR) + '/' + CAST(DATEPART(dd,datedisp) AS VARCHAR) + '/' + CAST(DATEPART(yy,datedisp) AS VARCHAR) AS datedisp,CAST(DATEPART(mm,recdate) AS VARCHAR) + '/' + CAST(DATEPART(dd,recdate) AS VARCHAR) + '/' + CAST(DATEPART(yy,recdate) AS VARCHAR) AS recdate,recby FROM CM.CrsDetailsDispatch WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "' ORDER BY datentry DESC";
         SqlDataAdapter da = new SqlDataAdapter(cmd);
         cn.Open();
         da.Fill(tblDispatchDetails);
      }
      dgItems.DataSource = tblDispatchDetails;
      dgItems.DataBind();
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         trReceived.Visible = false;
         trDispatch.Visible = false;
         bool blnReadOnly = false;
         txtCrseCode.Text = Request.QueryString["crsecode"].ToString();
         using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT crsettle,yearterm,currcode,datecomp,ordernum,pstatus FROM CM.CrsDetails WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            txtCrseTtle.Text = dr["crsettle"].ToString();
            txtYearTerm.Text = dr["yearterm"].ToString();
            txtAvailability.Text = clsCRS.ToCAStatusDesc(clsCRS.GetCAStatus(txtCrseCode.Text));
            txtNoReq.Text = dr["ordernum"].ToString();
            hdnPStatus.Value = dr["pstatus"].ToString();
            txtStatus.Text = clsCRS.ToCrsDetailsStatusDesc(dr["pstatus"].ToString());
            blnReadOnly = (dr["pstatus"].ToString() == "C" || dr["pstatus"].ToString() == "F" ? true : false);
            dr.Close();
         }
         divAddDispatch.Visible = !blnReadOnly;

         BindItems();
      }
   }

   protected void btnAdd_Click(object sender, ImageClickEventArgs e)
   {
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         if (chkReceived.Checked)
         {
            cmd.CommandText = "INSERT INTO CM.CrsDetailsDispatch(crscode,crsecode,datentry,dispdeta,disptype,datedisp,recby,recdate) VALUES('" + Request.QueryString["crscode"] + "','" + Request.QueryString["crsecode"] + "','" + DateTime.Now + "',@dispdeta,@disptype,@datedisp,@recby,@recdate)";
            cmd.Parameters.Add("@dispdeta", SqlDbType.VarChar, 255);
            cmd.Parameters.Add("@disptype", SqlDbType.Char, 1);
            cmd.Parameters.Add("@datedisp", SqlDbType.DateTime);
            cmd.Parameters.Add("@recby", SqlDbType.VarChar, 30);
            cmd.Parameters.Add("@recdate", SqlDbType.DateTime);
            cmd.Parameters["@dispdeta"].Value = txtDispDet.Text;
            cmd.Parameters["@disptype"].Value = ddlDispType.SelectedValue;
            cmd.Parameters["@datedisp"].Value = dteDateDisp.Date;
            cmd.Parameters["@recby"].Value = txtRecBy.Text;
            cmd.Parameters["@recdate"].Value = dteDateRec.Date;
         }
         else if (chkDispatched.Checked)
         {
            cmd.CommandText = "INSERT INTO CM.CrsDetailsDispatch(crscode,crsecode,datentry,dispdeta,disptype,datedisp) VALUES('" + Request.QueryString["crscode"] + "','" + Request.QueryString["crsecode"] + "','" + DateTime.Now + "',@dispdeta,@disptype,@datedisp)";
            cmd.Parameters.Add("@dispdeta", SqlDbType.VarChar, 255);
            cmd.Parameters.Add("@disptype", SqlDbType.Char, 1);
            cmd.Parameters.Add("@datedisp", SqlDbType.DateTime);
            cmd.Parameters["@dispdeta"].Value = txtDispDet.Text;
            cmd.Parameters["@disptype"].Value = ddlDispType.SelectedValue;
            cmd.Parameters["@datedisp"].Value = dteDateDisp.Date;
         }
         else
         {
            cmd.CommandText = "INSERT INTO CM.CrsDetailsDispatch(crscode,crsecode,datentry,dispdeta,disptype) VALUES('" + Request.QueryString["crscode"] + "','" + Request.QueryString["crsecode"] + "','" + DateTime.Now + "',@dispdeta,@disptype)";
            cmd.Parameters.Add("@dispdeta", SqlDbType.VarChar, 255);
            cmd.Parameters.Add("@disptype", SqlDbType.Char, 1);
            cmd.Parameters["@dispdeta"].Value = txtDispDet.Text;
            cmd.Parameters["@disptype"].Value = ddlDispType.SelectedValue;
         }

         cn.Open();
         cmd.ExecuteNonQuery();
         cmd.Parameters.Clear();

         if (ddlDispType.SelectedValue == "C")
         {
            cmd.CommandText = "UPDATE CM.CrsDetails SET pstatus='C',datecomp='" + DateTime.Now + "' WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "'";
            cmd.ExecuteNonQuery();
         }
         else if (ddlDispType.SelectedValue == "P" && hdnPStatus.Value == "E")
         {
            cmd.CommandText = "UPDATE CM.CrsDetails SET pstatus='P' WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "'";
            cmd.ExecuteNonQuery();
         }
      }
      txtDispDet.Text = "";
      txtRecBy.Text = "";
      Response.Redirect("CRSDispatchDetailsCC.aspx?crscode=" + Request.QueryString["crscode"] + "&crsecode=" + Request.QueryString["crsecode"]);
   }

   protected void btnClose_Click(object sender, ImageClickEventArgs e)
   {
      if (divAddDispatch.Visible)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("window.opener.document.forms[0].submit();");
         sb.Append("window.close();");
         ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindowScript", sb.ToString(), true);
      }
      else
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("window.close();");
         ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindowScript", sb.ToString(), true);
      }
   }

   protected void dgItems_EditCommand(object source, DataGridCommandEventArgs e)
   {
      dgItems.EditItemIndex = e.Item.ItemIndex;
      BindItems();
   }

   protected void dgItems_CancelCommand(object source, DataGridCommandEventArgs e)
   {
      dgItems.EditItemIndex = -1;
      BindItems();
   }

   protected void dgItems_UpdateCommand(object source, DataGridCommandEventArgs e)
   {
      HiddenField phdnDateEntry = (HiddenField)e.Item.FindControl("hdnDateEntry");
      TextBox ptxtRecBy = (TextBox)e.Item.FindControl("txtRecBy");
      GMDatePicker pdteRecDate = (GMDatePicker)e.Item.FindControl("dteRecDate");

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "UPDATE CM.CrsDetailsDispatch SET recby=@recby,recdate=@recdate WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "' AND datentry='" + phdnDateEntry.Value + "'";
         cmd.Parameters.Add("@recby", SqlDbType.VarChar, 30);
         cmd.Parameters.Add("@recdate", SqlDbType.DateTime);
         cmd.Parameters["@recby"].Value = ptxtRecBy.Text;
         cmd.Parameters["@recdate"].Value = pdteRecDate.Date;
         cn.Open();
         cmd.ExecuteNonQuery();
      }
      dgItems.EditItemIndex = -1;
      BindItems();
   }

   protected void chkDispatched_CheckedChanged(object sender, EventArgs e)
   {
      trDispatch.Visible = chkDispatched.Checked;
      if (chkDispatched.Checked == false && chkReceived.Checked)
      {
         chkReceived.Checked = false;
         trReceived.Visible = false;
      }
   }

   protected void chkReceived_CheckedChanged(object sender, EventArgs e)
   {
      trReceived.Visible = chkReceived.Checked;
      if (chkReceived.Checked)
      {
         chkDispatched.Checked = true;
         trDispatch.Visible = true;
      }
   }

}
