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
public partial class CMD_CRS_CRSDetailsCC : System.Web.UI.Page
{

	protected void LoadRecords()
	{
		string strWrite = "";

		DataTable tblTemp = new DataTable();
		DataTable tblCurriculum = new DataTable();
		tblCurriculum.Columns.Add("currcode", System.Type.GetType("System.String"));
		tblCurriculum.Columns.Add("progcode", System.Type.GetType("System.String"));
		tblCurriculum.Columns.Add("progname", System.Type.GetType("System.String"));

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			SqlDataReader dr;

			cmd.CommandText = "SELECT DISTINCT currcode FROM CM.CrsDetails WHERE crscode='" + Request.QueryString["crscode"] + "' ORDER BY currcode";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			cn.Open();
			da.Fill(tblTemp);
			
			using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
			{
				SqlCommand cmdOmega = cnOmega.CreateCommand();
				SqlDataReader drOmega;
				cnOmega.Open();
				foreach (DataRow drow in tblTemp.Rows)
				{
					DataRow rwNew = tblCurriculum.NewRow();
					rwNew["currcode"] = drow["currcode"].ToString();
					cmdOmega.CommandText = "SELECT dbo_course.course_code,dbo_course.course_name FROM dbo_mcurriculum INNER JOIN dbo_course ON dbo_mcurriculum.course_code = dbo_course.course_code WHERE cur_ref_no='" + drow["currcode"] + "'";
					drOmega = cmdOmega.ExecuteReader();
					if (drOmega.Read())
					{
						rwNew["progcode"] = drOmega["course_code"].ToString();
						rwNew["progname"] = drOmega["course_name"].ToString();
					}
					drOmega.Close();
					tblCurriculum.Rows.Add(rwNew);
				}
				cnOmega.Close();
			}

			foreach (DataRow drow in tblCurriculum.Rows)
			{
				strWrite = strWrite + "<div class='GridBorder'>" +
																											"<table width='100%' cellpadding='5' class='grid'>" +
																												"<tr>" +
																													"<td class='GridText' colspan='6' style='font-size:x-small;'>" +
																														"<table>" +
																															"<tr>" +
																																"<td><img src='../../Support/attach16.png'></td>" +
																																"<td>" + drow["progname"] + " <b>[" + drow["currcode"] + "]</b></td>" +
																															"</tr>" +
																														"</table>" +
																													"</td>" +
																												"</tr>" +
																												"<tr>" +
																													"<td class='GridColumns' style='width:5%;'>&nbsp;</td>" +
																													"<td class='GridColumns' style='width:50%;'><b>Courseware Requested</b></td>" +																													
																													"<td class='GridColumns' style='width:15%;'><b>Curriculum</b></td>" +
																													"<td class='GridColumns' style='width:15%;'><b>Availability</b></td>" +
																													"<td class='GridColumns' style='width:5%;'><b>#</b></td>" +
																													"<td class='GridColumns' style='width:10%;'><b>Charge</b></td>" +
																												"</tr>";
				cmd.CommandText = "SELECT currcode,crsecode,crsettle,yearterm,ordernum,price,pstatus FROM CM.CrsDetails WHERE crscode='" + Request.QueryString["crscode"] + "' AND currcode='" + drow["currcode"] + "' AND (pstatus='E' OR pstatus='P' OR pstatus='C') ORDER BY yearterm,crsettle";
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					strWrite = strWrite + "<tr>" +
																												"<td class='GridRows' style='text-align:center;'><img src='../../Support/" + clsCRS.GetCrsDetailsStatusIcon(dr["pstatus"].ToString()) + "' alt=''></td>" +
																												"<td class='GridRows'>" +
																													"<a href='#' onclick=window.open('CRSDispatchDetailsCC.aspx?crscode=" + Request.QueryString["crscode"] + "&crsecode=" + dr["crsecode"] + "',null,'height=600,width=700,status=yes,toolbar=no,menubar=no,location=no,scrollbars=1')>" + dr["crsettle"] + " (" + dr["crsecode"] + ")</a> [sy " + dr["yearterm"] + "]" +
																												"</td>" +																												
																												"<td class='GridRows' style='text-align:center;'>" + dr["currcode"] + "</td>" +
																												"<td class='GridRows' style='text-align:center;'>" + clsCRS.ToCAStatusDesc(clsCRS.GetCAStatus(dr["crsecode"].ToString())) + "</td>" +
																												"<td class='GridRows' style='text-align:center;'>" + dr["ordernum"] + "</td>" +
																												"<td class='GridRows' style='text-align:right;'>" + Convert.ToDouble(dr["price"].ToString()).ToString("##,##0.00") + "</td>" +
																											"</tr>";
				}
				strWrite = strWrite + "</table></div><br>";
				dr.Close();
			}
		}
		Response.Write(strWrite);
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			clsCRS.AuthenticateUser(clsCRS.CRSUserType.CoursewareCoordinator, Request.Cookies["Speedo"]["UserName"], Request.QueryString["crscode"]);

			bool blnReadOnly = true;
			txtCrsCode.Text = Request.QueryString["crscode"].ToString();
   txtCCName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);
			clsCRS crs = new clsCRS(txtCrsCode.Text);
			crs.Load();
			txtDateReq.Text = crs.DateRequested.ToString();
			txtRemarks.Text = crs.Remarks;
			txtCMHRem.Text = crs.ChannelManagerHeadRemarks;
			txtCCRem.Text = crs.CoursewareCoordinatorRemarks;

   txtCMName.Text = clsUsers.GetName(crs.ChannelManager);
   txtCMHName.Text = clsUsers.GetName(crs.ChannelManagerHead);
			txtSchlName.Text = clsSchool.GetSchoolName(crs.SchoolCode);

			DataTable tblAttachment = new DataTable();
			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandText = "SELECT crsecode FROM CM.CrsDetails WHERE crscode='" + Request.QueryString["crscode"] + "' AND (pstatus='E' OR pstatus='P')";
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
			txtCCRem.ReadOnly = blnReadOnly;
			if (blnReadOnly)
				txtCCRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");
		}
 }

	protected void btnSaveChanges_Click(object sender, ImageClickEventArgs e)
	{
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "UPDATE CM.Crs SET ccrem=@ccrem WHERE crscode='" + Request.QueryString["crscode"] + "'";
			cmd.Parameters.Add("@ccrem", SqlDbType.VarChar, 255);
			cmd.Parameters["@ccrem"].Value = txtCCRem.Text;
			cn.Open();
			cmd.ExecuteNonQuery();
		}
	}

}
