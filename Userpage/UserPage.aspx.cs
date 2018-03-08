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
using System.Text.RegularExpressions;
using HRMS;
using HqWeb.Forums;

public partial class Userpage_Userpage : System.Web.UI.Page
{
    protected void Load_GB()
    {
        int intCtr = 0;
        string strWrite = "";
        int intPage = (Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"]));
        int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
        int intStart = ((intPage - 1) * intPageSize) + 1;
        int intEnd = intPage * intPageSize;

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM (SELECT postby,gbcode,contents,datesubm,emptitle,ROW_NUMBER() OVER(ORDER BY datesubm DESC) AS RowNum FROM Speedo.Guestbooks INNER JOIN Users.Users ON Speedo.Guestbooks.postby = Users.Users.username WHERE Speedo.Guestbooks.username='" + Request.QueryString["username"] + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                intCtr++;
                strWrite = "<tr>" +
                               "<td>" +
                                                                 "<div class='GridBorder'>" +
                                                                     "<table width='100%' cellpadding='4' class='grid' style='border-color: #FAFAFA'>" +
                                                                         "<tr>" +
                                                                             "<td style='text-align:left;border-color: #FAFAFA' class='GridRows'>" +
                                                                                 "<table cellpaddin='0' cellspacing='0' >" +
                                                                                     "<tr>" +
                                                                                         "<td style='width:10%'>" +
                                                                                            "<img src='../Pictures/avatar/" + clsSpeedo.GetAvatar(dr["postby"].ToString()) + ".jpg' width='50' height='50' />&nbsp;" +
                                                                                            "</td>" +
                                                                                        "<td  style='width:90%'>" +
                                                                                    "<a href='UserPage.aspx?username=" + dr["postby"] + "' style='font-size:small;'>" + dr["postby"] + "</a> - <i>" + dr["emptitle"] + "</i><br>" +
                                                                                       "<span style='font-size:x-small;'>Posted last " + Convert.ToDateTime(dr["datesubm"]).ToString("MMMM dd, yyyy hh:mm:ss tt") + "</span>" +
                                                                                            "</td>" +
                                                                                        "</tr>" +
                                                                                    "</table>" +
                                                                                "</td>" +
                                                                            "</tr>" +
                                                                            "<tr>" +
                    "<td class='GridRows' style='font-size:small;border-color: #FAFAFA'>" + clsBB.FormatContents(dr["contents"].ToString()) + "</td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</div>" +
                                                                "</td>" +
                                                            "</tr>" +
                                                            "<tr><td height='3px'></td></tr>";
                Response.Write(strWrite);
            }
            if (intCtr == 0)
            {
                strWrite = "<tr>" +
                               "<td>" +
                                                                 "<div class='GridBorder'>" +
                                                                     "<table width='100%' cellpadding='5' class='grid'>" +
                                                                         "<tr>" +
                                                                             "<td style='text-align:left;' class='GridColumns'>&nbsp;</td>" +
                                                                            "</tr>" +
                                                                            "<tr>" +
                                                                             "<td class='GridRows' style='background-color: #ffffff;'>No Guestbook entries :(<br></td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</div>" +
                                                                "</td>" +
                                                            "</tr>" +
                                                        "<tr><td height='3px'></td></tr>";
                Response.Write(strWrite);
            }
            dr.Close();
        }
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
            cmd.CommandText = "SELECT COUNT(gbcode) AS tcount FROM Speedo.Guestbooks WHERE username='" + Request.QueryString["username"] + "'";
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
                Response.Write("&nbsp;&nbsp;<a href='UserPage.aspx?username=" + Request.QueryString["username"] + "&page=" + intPage + "'>" + intPage + "</a>");
            intPage++;
            intTRowsTemp -= intPageSize;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            clsGuestbooksLog.InsertRecord(Request.QueryString["username"], Request.Cookies["Speedo"]["UserName"]);

            //if (File.Exists(Server.MapPath("~/pictures/avatar/") + Request.QueryString["username"] + ".jpg"))
            //    imgAvatar.ImageUrl = "~/pictures/avatar/" + Request.QueryString["username"] + ".jpg";
            //else
            //    imgAvatar.ImageUrl = "~/pictures/avatar/default.jpg";

            if (File.Exists(Server.MapPath("~/pictures/realpicture/") + Request.QueryString["username"] + ".jpg"))
                imgRealPic.ImageUrl = "~/pictures/realpicture/" + Request.QueryString["username"] + ".jpg";
            else
                imgRealPic.ImageUrl = "~/pictures/realpicture/default.jpg";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT firname + ' ' + lastname AS name,email FROM Users.Users WHERE username='" + Request.QueryString["username"] + "'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblName.Text = dr["name"].ToString();
                    lblEmail.Text = "<a href='mailto://" + dr["email"].ToString() + "'>" + dr["email"].ToString() + "</a>";
                }
                dr.Close();
            }

            using (clsEmployee employee = new clsEmployee())
            {
                employee.Username = Request.QueryString["username"];
                employee.Fill();
                lblTitle.Text = employee.Position;
                lblLocal.Text = clsUsers.GetLocalNumber(employee.Username);
                lblBirthDate.Text = employee.BirthDate.ToString("MMMM dd, ") + "19??";
                lblDivision.Text = clsDivision.GetDivisionName(employee.DivisionCode);
                //lblGroup.Text = clsGroup.GetGroupName(employee.GroupCode);
                lblDepartment.Text = clsDepartment.GetName(employee.DepartmentCode);
                lblHobbies.Text = employee.Hobbies;
            }


        }
    }

    protected void btnPostGB_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "INSERT INTO Speedo.Guestbooks(username,postby,datesubm,contents) VALUES('" + Request.QueryString["username"] + "','" + Request.Cookies["Speedo"]["UserName"] + "','" + DateTime.Now + "',@contents)";
            cmd.Parameters.Add("@contents", SqlDbType.Text);
            cmd.Parameters["@contents"].Value = txtPost.Text;
            cn.Open();
            cmd.ExecuteNonQuery();
        }
        Response.Redirect("~/Userpage/UserPage.aspx?username=" + Request.QueryString["username"]);
    }

}