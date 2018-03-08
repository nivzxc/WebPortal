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
using System.Text.RegularExpressions;
using HRMS;
public partial class CMD_SIS_SchoolsDirectory : System.Web.UI.Page
{

     //protected void Load_Records()
     //{
     // int intCtr = 0;
     // string strWrite = "";
     // string strWhere = "";
     // using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
     // {
     //  if (ddlNatlGrp.SelectedValue != "all")
     //   strWhere += " AND natlcode='" + ddlNatlGrp.SelectedValue + "'";

     //  if (ddlChannelManager.SelectedValue != "all")
     //   strWhere += " AND cmname='" + ddlChannelManager.SelectedValue + "'";

     //  if (ddlOwnType.SelectedValue != "all")
     //   strWhere += " AND hqowned='" + ddlOwnType.SelectedValue + "'";

     //  SqlCommand cmd = cn.CreateCommand();
     //  cmd.CommandText = "SELECT schlcode,schlname,schlnam2,telnmbr,faxnmbr,ceoname,cooname,cmname,schladdr,hqowned FROM CM.Schools WHERE schlname LIKE '%" + Regex.Replace(txtSchlName.Text, "'", "") + "%' AND pstatus='1'" + strWhere + " ORDER BY schlnam2";
     //  cn.Open();
     //  SqlDataReader dr = cmd.ExecuteReader();
     //  while (dr.Read())
     //  {
     //   strWrite += "<tr>" +
     //    "<td class='GridRows'>" +
     //     "<table>" +
     //      "<tr><td>" +
     //       "<table>" +
     //        "<tr>" +
     //         "<td style='width:10%'><img src='../../Support/" + (dr["hqowned"].ToString() == "1" ? "bookmark16" : "star16") + ".png'></td>" +
     //         "<td style='width:90%'>&nbsp;<b><a href='SchoolsDirectoryDetails.aspx?schlcode=" + dr["schlcode"] + "&schlname=" + dr["schlname"] + "' style='font-size:small;'>" + dr["schlnam2"] + "</a></b><td>" +
     //        "</tr>" +
     //       "</table>" +
     //      "</td></tr>";
     //   if (!chkBasic.Checked)
     //   {
     //    strWrite += "<tr><td>" + dr["schladdr"] + "</td></tr>" +
     //     "<tr><td>CEO: " + dr["ceoname"] + "</td></tr>" +
     //     "<tr><td>COO: " + dr["cooname"] + "</td></tr>" +
     //     "<tr><td>CM: " + clsUsers.GetName(dr["cmname"].ToString()) + "</td></tr>";
     //   }
     //   strWrite += "</table>" +
     //                "</td>" +
     //                 "<td class='GridRows'>" +
     //                                                       "<table>" +
     //                                                           "<tr><td>Tel # " + dr["telnmbr"] + "</td></tr>" +
     //                                                           "<tr><td>Fax # " + dr["faxnmbr"] + "</td></tr>" +
     //                                                       "</table>" +
     //                                                   "</td>" +
     //                                               "</tr>";
     //   intCtr++;
     //  }
     //  dr.Close();
     // }
     // Response.Write(strWrite);
     //}

    protected void Load_Records()
    {
        ////////////////////////////////////////////////
        ////////// -STI DIRECTORY LOAD SCHOOL- /////////
        /////////// REMOVE DUE TO CHANGE ///////////////
        /////////// DIRECTORY TO PFIC BRANCHES /////////
        ////////////////////////////////////////////////
        /////////// BY CALVIN CAVITE 2/26/2018 /////////
        ////////////////////////////////////////////////

        //int intCtr = 0;
        //string strWrite = "";
        //string strWhere = "";
        ////Added by Charlie Bachiller 10/04/2011
        //string strCEO = "";
        //string strCOO = "";
        //string strCM = "";

        //using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        //{
        //    if (ddlNatlGrp.SelectedValue != "all")
        //        strWhere += " AND natlcode='" + ddlNatlGrp.SelectedValue + "'";

        //    if (ddlChannelManager.SelectedValue != "all")
        //        strWhere += " AND cmname='" + ddlChannelManager.SelectedValue + "'";

        //    if (ddlOwnType.SelectedValue != "all")
        //        strWhere += " AND hqowned='" + ddlOwnType.SelectedValue + "'";

        //    SqlCommand cmd = cn.CreateCommand();
        //    cmd.CommandText = "SELECT schlcode,schlname,schlnam2,telnmbr,faxnmbr,ceoname,cooname,cmname,schladdr,hqowned FROM CM.Schools WHERE schlname LIKE '%" + Regex.Replace(txtSchlName.Text, "'", "") + "%' AND pstatus='1'" + strWhere + " ORDER BY schlnam2";
        //    cn.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        strWrite += "<tr>" +
        //         "<td class='GridRows'>" +
        //          "<table>" +
        //           "<tr><td>" +
        //            "<table>" +
        //             "<tr>" +
        //              "<td style='width:10%'><img src='../../Support/" + (dr["hqowned"].ToString() == "1" ? "bookmark16" : "star16") + ".png'></td>" +
        //              "<td style='width:90%'>&nbsp;<b><a href='SchoolsDirectoryDetails.aspx?schlcode=" + dr["schlcode"] + "&schlname=" + dr["schlname"] + "' style='font-size:small;'>" + dr["schlname"] + "</a></b><td>" +
        //             "</tr>" +
        //            "</table>" +
        //           "</td></tr>";
        //        if (!chkBasic.Checked)
        //        {
        //            //Added by Charlie Bachiller 10/04/2011
        //            if (dr["hqowned"].ToString() == "0")
        //            {
        //                strCEO = "President";
        //                strCOO = "School Administrator";
        //                strCM = "SOM";
        //            }
        //            else
        //            {
        //                strCEO = "School Administrator";
        //                strCOO = "Deputy School Administrator";
        //                strCM = "SSA";
        //            }
        //            strWrite += "<tr><td>" + dr["schladdr"] + "</td></tr>" +
        //             "<tr><td>" + strCEO + ": " + dr["ceoname"] + "</td></tr>" +
        //             "<tr><td>" + strCOO + ": " + dr["cooname"] + "</td></tr>" +
        //             "<tr><td>" + strCM + ": " + clsUsers.GetName(dr["cmname"].ToString()) + "</td></tr>";
        //        }
        //        strWrite += "</table>" +
        //                     "</td>" +
        //                      "<td class='GridRows'>" +
        //                                                            "<table>" +
        //                                                                "<tr><td>Tel # " + dr["telnmbr"] + "</td></tr>" +
        //                                                                "<tr><td>Fax # " + dr["faxnmbr"] + "</td></tr>" +
        //                                                            "</table>" +
        //                                                        "</td>" +
        //                                                    "</tr>";
        //        intCtr++;
        //    }
        //    dr.Close();
        //}
        //Response.Write(strWrite);

        string searchResult = "";

        using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString)) {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "select * from dbo.Branches where branchname like '%"+ txtSchlName.Text + "%' or branchaddress like '%"+ txtSchlName.Text + "%'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {

                searchResult += "<tr>" +
                    "               <td class='BranchGridRows'>" +
                                        //"<table>" +
                                        //    "<tr>" +
                                        //        "<td> " +
                                                    "<div class='row'>" +
                                                        "<div class='col-xs-4'>" +
                                                            "<font style='font-weight:bold;'>Code: </font>"  +
                                                        "</div>" +
                                                        "<div class='col-xs-7'>" +
                                                            "<a href='#'> " + dr["branchcode"].ToString() + " </a><br> " +
                                                        "</div>" +                                               
                                                    "</div>"+
                                                       "<div class='row'>" +
                                                        "<div class='col-xs-4'>" +
                                                            "<font style='font-weight:bold;'>Branch Name: </font>" + 
                                                        "</div>" +
                                                        "<div class='col-xs-7'>" +
                                                            dr["branchname"].ToString() + "<br>" +
                                                        "</div>" +
                                                    "</div>" +
                                                       "<div class='row'>" +
                                                        "<div class='col-xs-4'>" +
                                                            "<font style='font-weight:bold;'>Branch Address: </font>" + 
                                                        "</div>" +
                                                        "<div class='col-xs-7'>" +
                                                            dr["branchaddress"].ToString() + "<br>" +
                                                        "</div>" +
                                                    "</div>" +
                                                       "<div class='row'>" +
                                                        "<div class='col-xs-4'>" +
                                                            "<font style='font-weight:bold;'>Branch Manager: </font>" + 
                                                        "</div>" +
                                                        "<div class='col-xs-7'>" +
                                                            dr["branchmnger"].ToString() + "<br>" +
                                                        "</div>" +
                                                    "</div>" +                                           
                                        //        "</td>" +
                                        //    "</tr>" +
                                        //"</table>" +
                                    "</td>" +
                                    "<td class='BranchGridRows'>" + dr["branchcontact"].ToString() +"</td>"+                                
                                 "<tr>";
                                 
            }
            dr.Close();
            cn.Close();
        }
        Response.Write(searchResult);

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {    

        ////////////////////////////////////////////////
        ////////// -STI DIRECTORY LOAD SCHOOL- /////////
        /////////// REMOVE DUE TO CHANGE ///////////////
        /////////// DIRECTORY TO PFIC BRANCHES /////////
        ////////////////////////////////////////////////
        /////////// BY CALVIN CAVITE 2/26/2018 /////////
        ////////////////////////////////////////////////

        //clsSpeedo.Authenticate();

        //if (!Page.IsPostBack)
        //{
        // DataTable tblNational = new DataTable();
        // DataTable tblCM = new DataTable();
        // using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
        // {
        //  SqlCommand cmd = cn.CreateCommand();
        //  cmd.CommandText = "SELECT natlcode,natlname FROM CM.SchoolNational ORDER BY natlcode";
        //  SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  cn.Open();
        //  da.Fill(tblNational);

        //  cmd.CommandText = "SELECT firname + ' ' + lastname AS pname,username FROM Users.Users WHERE username IN (SELECT DISTINCT cmname FROM CM.Schools) AND pstatus='1' ORDER BY firname";
        //  da.SelectCommand = cmd;
        //  da.Fill(tblCM);
        // }

        // ddlNatlGrp.DataSource = tblNational;
        // ddlNatlGrp.DataValueField = "natlcode";
        // ddlNatlGrp.DataTextField = "natlname";
        // ddlNatlGrp.DataBind();

        // ddlChannelManager.DataSource = tblCM;
        // ddlChannelManager.DataValueField = "username";
        // ddlChannelManager.DataTextField = "pname";
        // ddlChannelManager.DataBind();

        // ListItem itm;
        // itm = new ListItem();
        // itm.Value = "all";
        // itm.Text = "All";
        // itm.Selected = true;
        // ddlNatlGrp.Items.Add(itm);

        // itm = new ListItem();
        // itm.Value = "all";
        // itm.Text = "All";
        // itm.Selected = true;
        // ddlChannelManager.Items.Add(itm);



    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Redirect("SchoolDirectoryExcel.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
    }
}


