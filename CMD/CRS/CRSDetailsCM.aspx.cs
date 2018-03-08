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
public partial class CMD_CRS_CRSDetailsCM : System.Web.UI.Page
{

   protected void LoadRecords()
   {
      string strWrite = "";
      double dblPrice = 0.0;
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
         }

         foreach (DataRow drow in tblCurriculum.Rows)
         {
            dblPrice = 0;
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
                                                                                                                                         "<td class='GridColumns' style='width:55%;'><b>Courseware Requested</b></td>" +
                                                                                                                                         "<td class='GridColumns' style='width:5%;'><b>Year</b></td>" +
                                                                                                                                         "<td class='GridColumns' style='width:15%;'><b>Availability</b></td>" +
                                                                                                                                         "<td class='GridColumns' style='width:5%;'><b>#</b></td>" +
                                                                                                                                         "<td class='GridColumns' style='width:15%;'><b>Charge</b></td>" +
                                                                                                                                    "</tr>";
            cmd.CommandText = "SELECT crsecode,crsettle,yearterm,price,ordernum,pstatus FROM CM.CrsDetails WHERE crscode='" + Request.QueryString["crscode"] + "' AND currcode='" + drow["currcode"] + "' ORDER BY yearterm,crsettle";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               strWrite += "<tr>" +
                              "<td class='GridRows' style='text-align:center;'><img src='../../Support/" + clsCRS.GetCrsDetailsStatusIcon(dr["pstatus"].ToString()) + "'></td>" +
                                                                                "<td class='GridRows'><a href='#' onclick=window.open('CRSDispatchDetailsCM.aspx?crscode=" + Request.QueryString["crscode"] + "&crsecode=" + dr["crsecode"] + "',null,'height=600,width=600,status=yes,toolbar=no,menubar=no,location=no,scrollbars=1')>" + dr["crsettle"] + " (" + dr["crsecode"] + ")</a></td>" +
                                                                                "<td class='GridRows' style='text-align:center;'>" + dr["yearterm"] + "</td>" +
                                                                                "<td class='GridRows' style='text-align:center;'>" + clsCRS.ToCAStatusDesc(clsCRS.GetCAStatus(dr["crsecode"].ToString())) + "</td>" +
                                                                                "<td class='GridRows' style='text-align:center;'>" + dr["ordernum"] + "</td>" +
                                                                                "<td class='GridRows' style='text-align:right;'>" + Convert.ToDouble(dr["price"].ToString()).ToString("#,##0.00") + "</td>" +
                                                                           "</tr>";
               dblPrice += Convert.ToDouble(dr["price"].ToString());
            }
            dr.Close();
            strWrite += "<tr>" +
             "<td class='GridColumns'>&nbsp;</td>" +
                                                                             "<td class='GridColumns'>&nbsp;</td>" +
                                                                             "<td class='GridColumns'>&nbsp;</td>" +
                                                                             "<td class='GridColumns'>&nbsp;</td>" +
                                                                             "<td class='GridColumns'>&nbsp;</td>" +
                                                                             "<td class='GridColumns' style='text-align:right;'><b>" + dblPrice.ToString("###,##0.00") + "</b></td>" +
                                                                        "</tr></table></div><br>";
         }
      }
      Response.Write(strWrite);
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         clsCRS.AuthenticateUser(clsCRS.CRSUserType.ChannelManager, Request.Cookies["Speedo"]["UserName"], Request.QueryString["crscode"]);

         clsCRS crs = new clsCRS(Request.QueryString["crscode"].ToString());
         crs.Load();
         txtCrsCode.Text = crs.CRSCode;
         txtDateReq.Text = crs.DateRequested.ToString();
         hdnSchlCode.Value = crs.SchoolCode;
         txtRemarks.Text = crs.Remarks;
         txtCMHRemarks.Text = crs.ChannelManagerHeadRemarks;
         txtCCRemarks.Text = crs.CoursewareCoordinatorRemarks;

         txtCMName.Text = clsUsers.GetName(crs.ChannelManager);
         txtCMHName.Text = clsUsers.GetName(crs.ChannelManagerHead);
         txtCCName.Text = clsUsers.GetName(crs.CoursewareCoordinator);
         txtSchlName.Text = clsSchool.GetSchoolName(hdnSchlCode.Value);

         DataTable tblAttachment = new DataTable();
         using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT filename,details,filecont FROM CM.CrsAttachment WHERE crscode='" + Request.QueryString["crscode"] + "'";
            cn.Open();
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
      }
   }

}
