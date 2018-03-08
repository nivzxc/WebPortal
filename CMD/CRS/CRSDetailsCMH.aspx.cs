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
public partial class CMD_CRS_CRSDetailsCMH : System.Web.UI.Page
{

	protected void BindCoursewareMaterials()
	{
		DataTable tblCourseware = new DataTable();
		DataTable tblSubjects = new DataTable();
		tblCourseware.Columns.Add("currcode", System.Type.GetType("System.String"));
		tblCourseware.Columns.Add("crsecode", System.Type.GetType("System.String"));
		tblCourseware.Columns.Add("crsettle", System.Type.GetType("System.String"));
		tblCourseware.Columns.Add("yearterm", System.Type.GetType("System.String"));
		tblCourseware.Columns.Add("avail", System.Type.GetType("System.String"));
		tblCourseware.Columns.Add("ordernum", System.Type.GetType("System.Int32"));
		tblCourseware.Columns.Add("pstatus", System.Type.GetType("System.String"));
		tblCourseware.Columns.Add("pstatusd", System.Type.GetType("System.String"));
		tblCourseware.Columns.Add("price", System.Type.GetType("System.Double"));

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			SqlDataReader dr;
			cmd.CommandText = "SELECT currcode,crsecode,crsettle,yearterm,ordernum,price,pstatus FROM CM.CrsDetails WHERE crscode='" + Request.QueryString["crscode"] + "' ORDER BY currcode,yearterm,crsettle";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			cn.Open();
			da.Fill(tblSubjects);

			foreach (DataRow drow in tblSubjects.Rows)
			{
				DataRow rwNewCourseware = tblCourseware.NewRow();
				rwNewCourseware["currcode"] = drow["currcode"].ToString();
				rwNewCourseware["crsecode"] = drow["crsecode"].ToString();
				rwNewCourseware["crsettle"] = drow["crsettle"].ToString();
				rwNewCourseware["yearterm"] = drow["yearterm"].ToString();
				rwNewCourseware["pstatus"] = drow["pstatus"].ToString();
				rwNewCourseware["ordernum"] = Convert.ToInt16(drow["ordernum"].ToString());
				rwNewCourseware["pstatusd"] = clsCRS.ToCrsDetailsStatusDesc(drow["pstatus"].ToString());
				rwNewCourseware["price"] = Convert.ToDouble(drow["price"].ToString());

				cmd.CommandText = "SELECT cwdstat FROM Academics.CoursewareInventory WHERE crsecode='" + drow["crsecode"] + "'";
				dr = cmd.ExecuteReader();
				if (dr.Read())
					rwNewCourseware["avail"] = clsCRS.ToCAStatusDesc(dr["cwdstat"].ToString());
				else
					rwNewCourseware["avail"] = "No status";
				dr.Close();

				tblCourseware.Rows.Add(rwNewCourseware);
			}
		}
		dgRCW.DataSource = tblCourseware;
		dgRCW.Columns[0].FooterText = "&nbsp;<b>Total ordered items [" + tblCourseware.Rows.Count + "]</b>";
		dgRCW.Columns[4].FooterText = "<b>" + Convert.ToDouble(tblCourseware.Compute("SUM(price)", String.Empty).ToString()).ToString("###,##0.00") + "</b>&nbsp;";
		dgRCW.DataBind();

		foreach (DataGridItem ditm in dgRCW.Items)
		{
			HiddenField phdnPStatus = (HiddenField)ditm.FindControl("hdnPStatus");
			Label plblPStatus = (Label)ditm.FindControl("lblPStatus");
			DropDownList pddlEndorse = (DropDownList)ditm.FindControl("ddlEndorse");
			TextBox ptxtDispDeta = (TextBox)ditm.FindControl("txtDispDeta");

			if (phdnPStatus.Value == "F")
			{
				plblPStatus.Visible = false;
				pddlEndorse.Visible = true;
				ditm.BackColor = System.Drawing.Color.AliceBlue;
			}
			else if (phdnPStatus.Value == "E")
			{
				plblPStatus.Visible = true;
				pddlEndorse.Visible = false;
				ditm.BackColor = System.Drawing.Color.Honeydew;
			}
			else if (phdnPStatus.Value == "D")
			{
				plblPStatus.Visible = true;
				pddlEndorse.Visible = false;
				ditm.BackColor = System.Drawing.Color.MistyRose;
			}
			else if (phdnPStatus.Value == "P")
			{
				plblPStatus.Visible = true;
				pddlEndorse.Visible = false;
				ditm.BackColor = System.Drawing.Color.LightYellow;
			}
			else if (phdnPStatus.Value == "C")
			{
				plblPStatus.Visible = true;
				pddlEndorse.Visible = false;
				ditm.BackColor = System.Drawing.Color.Thistle;
			}
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			clsCRS.AuthenticateUser(clsCRS.CRSUserType.ChannelManagerHead, Request.Cookies["Speedo"]["UserName"], Request.QueryString["crscode"]);

			bool blnReadOnly = true;
			txtCrsCode.Text = Request.QueryString["crscode"].ToString();			
			clsCRS crs = new clsCRS(txtCrsCode.Text);
			crs.Load();
			hdnCMName.Value = crs.ChannelManager;
			hdnSchlCode.Value = crs.SchoolCode;
			txtRemarks.Text = crs.Remarks;
			txtCMHRem.Text = crs.ChannelManagerHeadRemarks;
			hdnCCCode.Value = crs.CoursewareCoordinator;
			txtCCRem.Text = crs.CoursewareCoordinatorRemarks;
			txtDateReq.Text = crs.DateRequested.ToString();

   txtCMName.Text = clsUsers.GetName(hdnCMName.Value);
   txtCMHName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);
   txtCCName.Text = clsUsers.GetName(hdnCCCode.Value);
			txtSchlName.Text = clsSchool.GetSchoolName(hdnSchlCode.Value);

			DataTable tblAttachment = new DataTable();
			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandText = "SELECT crsecode FROM CM.CrsDetails WHERE pstatus='F'";
				cn.Open();				
				SqlDataReader dr = cmd.ExecuteReader();
				blnReadOnly = !dr.Read();
				dr.Close();

				cmd.CommandText = "SELECT filename,details,filecont FROM CM.CrsAttachment WHERE crscode='" + Request.QueryString["crscode"] + "'";
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(tblAttachment);
			}

			string strWrite = "";
			string strPicture = "";
			foreach (DataRow drow in tblAttachment.Rows)
			{
				if (!File.Exists(Server.MapPath("~/UploadedFiles/CRS/") + drow["filename"]))
				{
					Byte[] bytAttachment;
					bytAttachment = drow["filecont"] as Byte[];

					FileStream fsAttachment = new FileStream(Server.MapPath("~/UploadedFiles/CRS/") + drow["filename"], FileMode.OpenOrCreate, FileAccess.Write);
					fsAttachment.Write(bytAttachment, 0, bytAttachment.Length);
					fsAttachment.Close();
				}

				switch (Path.GetExtension(drow["filename"].ToString()).ToLower())
				{
					case ".jpg":
					case ".jpeg":
						strPicture = "fsview22.png";
						break;
					case ".doc":
						strPicture = "word22.png";
						break;
					case ".xls":
						strPicture = "excel22.png";
						break;
					case ".pdf":
						strPicture = "pdf22.png";
						break;
					default:
						strPicture = "star22.png";
						break;
				}

				strWrite += "<tr>" +
																	"<td><img src='../../Support/" + strPicture + "' /></td>" +
																	"<td>&nbsp;<a href='../../UploadedFiles/CRS/" + drow["filename"] + "' target='_blank'>" + drow["details"] + " [" + drow["filename"] + "]</a></td>" +
																"</tr>";
			}
			if (strWrite != "")
				lblAttachments.Text = "<table cellpadding='2'>" + strWrite + "</table>"; 

			divButton.Visible = !blnReadOnly;
			divSelection.Visible = !blnReadOnly;
			txtCMHRem.ReadOnly = blnReadOnly;

			if (blnReadOnly)
				txtCMHRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");

			BindCoursewareMaterials();
		}
 }

	protected void btnEndorseAll_Click(object sender, EventArgs e)
	{
		foreach (DataGridItem ditm in dgRCW.Items)
		{
			HiddenField phdnPStatus = (HiddenField)ditm.FindControl("hdnPStatus");
			DropDownList pddlEndorse = (DropDownList)ditm.FindControl("ddlEndorse");
			if (phdnPStatus.Value == "F")
				pddlEndorse.SelectedValue = "E";
		}
	}

	protected void btnPendingAll_Click(object sender, EventArgs e)
	{
		foreach (DataGridItem ditm in dgRCW.Items)
		{
			HiddenField phdnPStatus = (HiddenField)ditm.FindControl("hdnPStatus");
			DropDownList pddlEndorse = (DropDownList)ditm.FindControl("ddlEndorse");
			if (phdnPStatus.Value == "F")
				pddlEndorse.SelectedValue = "F";
		}
	}

	protected void btnDisapprove_Click(object sender, EventArgs e)
	{
		foreach (DataGridItem ditm in dgRCW.Items)
		{
			HiddenField phdnPStatus = (HiddenField)ditm.FindControl("hdnPStatus");
			DropDownList pddlEndorse = (DropDownList)ditm.FindControl("ddlEndorse");
			if (phdnPStatus.Value == "F")
				pddlEndorse.SelectedValue = "D";
		}
	}

	protected void btnEndorse_Click(object sender, ImageClickEventArgs e)
	{
		SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString());
		cn.Open();
		SqlTransaction tran = cn.BeginTransaction();
		SqlCommand cmd = cn.CreateCommand();
		cmd.Transaction = tran;
		try
		{
			foreach (DataGridItem ditm in dgRCW.Items)
			{
				HiddenField phdnPStatus = (HiddenField)ditm.FindControl("hdnPStatus");
				Label plblCourseCode = (Label)ditm.FindControl("lblCourseCode");
				DropDownList pddlEndorse = (DropDownList)ditm.FindControl("ddlEndorse");
				if ((phdnPStatus.Value == "F") && (pddlEndorse.SelectedValue == "D" || pddlEndorse.SelectedValue == "E"))
				{
					cmd.CommandText = "UPDATE CM.CrsDetails SET pstatus='" + pddlEndorse.SelectedValue + "',cmhdate='" + DateTime.Now + "' WHERE crsecode='" + plblCourseCode.Text + "' AND crscode='" + Request.QueryString["crscode"] + "'";
					cmd.ExecuteNonQuery();
				}
			}
			cmd.CommandText = "UPDATE CM.Crs SET cmhrem=@cmhrem WHERE crscode='" + Request.QueryString["crscode"] + "'";
			cmd.Parameters.Add("@cmhrem", SqlDbType.VarChar, 255);
			cmd.Parameters["@cmhrem"].Value = txtCMHRem.Text;
			cmd.ExecuteNonQuery();
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
