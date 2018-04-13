using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using HqWeb.Forums;
using HqWeb;
using STIeForms;
using HqWeb.Reward;
using HqWeb.GroupUpdate;

public partial class App_Master_Default : System.Web.UI.MasterPage
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    ///////////////////////////////////////////////////////
    ///// REMOVE DUE TO HEAVY LOAD ON WEBPAGE        ////// 
    ///// UNUSED METHODS                             //////
    ///// BY: calvin cavite Feb 20, 2018             //////
    ///////////////////////////////////////////////////////

    //private void LoadSynergyScore()
    //{
    //   int synergyID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    //   using (PortalDataContext pdc = new PortalDataContext())
    //   {
    //      var q = (from t in pdc.Teams
    //               let xScore = (from ets in pdc.EventTeamScores where ets.TeamID == t.TeamID select ets.Score).Sum()
    //               where t.ActivityID == synergyID
    //               orderby t.ColorID descending
    //               select new
    //               {
    //                  TeamName = t.Name,
    //                  TeamScore = (xScore == null ? 0 : xScore)
    //               }).ToList();

    //      chartTeams.DataSource = q.ToList();
    //      chartTeams.DataBind();

    //      chartTeams.Series[0].Points[0].Color = System.Drawing.Color.Gray;
    //      chartTeams.Series[0].Points[0].MarkerImage = "~/support/Synergy_Gray100.png";
    //      chartTeams.Series[0].Points[1].Color = System.Drawing.Color.ForestGreen;
    //      chartTeams.Series[0].Points[1].MarkerImage = "~/support/Synergy_Green100.png";
    //      chartTeams.Series[0].Points[2].Color = System.Drawing.Color.DodgerBlue;
    //      chartTeams.Series[0].Points[2].MarkerImage = "~/support/Synergy_Blue100.png";
    //      chartTeams.Series[0].Points[3].Color = System.Drawing.Color.Crimson;
    //      chartTeams.Series[0].Points[3].MarkerImage = "~/support/Synergy_Red100.png";
    //   }
    //}

    //private void LoadSynergyScore()
    //{
    //    int synergyID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    //    using (PortalDataContext pdc = new PortalDataContext())
    //    {
    //        var q = (from t in pdc.Teams
    //                 let xScore = (from ets in pdc.EventTeamScores where ets.TeamID == t.TeamID select ets.Score).Sum()
    //                 where t.ActivityID == synergyID && t.Name != "STI College Global City"
    //                 orderby t.ColorID descending
    //                 select new
    //                 {
    //                     TeamName = t.Name,
    //                     TeamScore = (xScore == null ? 0 : xScore)
    //                 }).ToList();

    //        chartTeams.DataSource = q.ToList();
    //        chartTeams.DataBind();

    //        decimal maxPoint = (from t in pdc.Teams
    //                            let xScore = (from ets in pdc.EventTeamScores where ets.TeamID == t.TeamID select ets.Score).Sum()
    //                            where t.ActivityID == synergyID
    //                            orderby xScore descending
    //                            select xScore).Take(1).SingleOrDefault().Value;
    //        int ceiling = (int)(Math.Floor(Math.Round(maxPoint / 10)) * 10) + 40;

    //        chartTeams.Series[0].BorderColor = System.Drawing.Color.Gray;
    //        chartTeams.Series[0].Points[0].Color = System.Drawing.Color.Gray;
    //        chartTeams.Series[0].Points[0].MarkerImage = "~/support/Synergy_Gray150.png";
    //        chartTeams.Series[0].Points[1].Color = System.Drawing.Color.ForestGreen;
    //        chartTeams.Series[0].Points[1].MarkerImage = "~/support/Synergy_Green150.png";
    //        chartTeams.Series[0].Points[2].Color = System.Drawing.Color.DodgerBlue;
    //        chartTeams.Series[0].Points[2].MarkerImage = "~/support/Synergy_Blue150.png";
    //        chartTeams.Series[0].Points[3].Color = System.Drawing.Color.Crimson;
    //        chartTeams.Series[0].Points[3].MarkerImage = "~/support/Synergy_Red150.png";
    //        chartTeams.ChartAreas[0].BorderDashStyle = System.Web.UI.DataVisualization.Charting.ChartDashStyle.Solid;
    //        chartTeams.ChartAreas[0].ShadowColor = System.Drawing.Color.Gray;
    //        chartTeams.ChartAreas[0].AxisY.Interval = 50;
    //        chartTeams.ChartAreas[0].AxisY.Maximum = ceiling;
    //        chartTeams.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
    //        chartTeams.Series[0].Font = new System.Drawing.Font("Verdana", 10, System.Drawing.FontStyle.Bold);
    //        chartTeams.Series[0].Points[0].LabelForeColor = System.Drawing.Color.DarkSlateGray;
    //        chartTeams.Series[0].Points[1].LabelForeColor = chartTeams.Series[0].Points[1].Color;
    //        chartTeams.Series[0].Points[2].LabelForeColor = chartTeams.Series[0].Points[2].Color;
    //        chartTeams.Series[0].Points[3].LabelForeColor = chartTeams.Series[0].Points[3].Color;

    //    }
    //}

    //private string GetActiveEventName()
    //{
    //    string strReturn = "";
    //    try
    //    {
    //        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    //        {
    //            using (SqlCommand cmd = cn.CreateCommand())
    //            {
    //                cmd.CommandText = "SELECT (SELECT Name FROM Portal.Events WHERE EventID=PEA.EventID) AS EventName FROM Portal.EventsActive as PEA WHERE IsActive='1'";
    //                cn.Open();
    //                strReturn = cmd.ExecuteScalar().ToString() + " Team Standing";
    //            }
    //        }

    //        if (strReturn != string.Empty)
    //        {
    //            //trSportsFestLogo.Visible = true;
    //            //trSportsFestLogoSpace.Visible = true;
    //            //trSportsSchedule.Visible = true;
    //            //trSportsScheduleSpace.Visible = true;
    //            //trSportsFestActiveTeam.Visible = true;
    //            //trSportsFestActiveTeamSpace.Visible = true;
    //            //divEventLogo.Visible = true;
    //            //litEventLogo.Text = "<asp:Image ImageUrl='~/Support/SportFestImage/" + GetActiveEventLogo() + "' />";
    //        }
    //        else
    //        {
    //            //trSportsFestLogo.Visible = false;
    //            //trSportsFestLogoSpace.Visible = false;
    //            //trSportsSchedule.Visible = false;
    //            //trSportsScheduleSpace.Visible = false;
    //            //trSportsFestActiveTeam.Visible = false;
    //            //trSportsFestActiveTeamSpace.Visible = false;
    //            //divEventLogo.Visible = false;
    //        }
    //    }
    //    catch
    //    {
    //        //trSportsFestLogo.Visible = false;
    //        //trSportsFestLogoSpace.Visible = false;
    //        //trSportsSchedule.Visible = false;
    //        //trSportsScheduleSpace.Visible = false;
    //        //trSportsFestActiveTeam.Visible = false;
    //        //trSportsFestActiveTeamSpace.Visible = false;
    //        //divEventLogo.Visible = false;
    //    }
    //    return strReturn;
    //}

    //private string GetActiveEvent()
    //{
    //    string strReturn = "";
    //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    //    {
    //        using (SqlCommand cmd = cn.CreateCommand())
    //        {
    //            try
    //            {
    //                cmd.CommandText = "SELECT EventID FROM Portal.EventsActive WHERE IsActive='1'";
    //                cn.Open();
    //                strReturn = cmd.ExecuteScalar().ToString();
    //            }
    //            catch
    //            { 

    //            }


    //        }
    //    }
    //    return strReturn;
    //}

    //protected string GetActiveEventLogo()
    //{
    //    string strReturn;
    //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    //    {
    //        using (SqlCommand cmd = cn.CreateCommand())
    //        {
    //            cmd.CommandText = "SELECT EventLogo FROM Portal.EventsActive WHERE IsActive='1'";
    //            cn.Open();
    //            strReturn = cmd.ExecuteScalar().ToString();
    //        }
    //    }
    //    return strReturn;
    //}

    //private string GetActiveEventLogoMain()
    //{
    //    string strReturn = "";
    //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    //    {
    //        using (SqlCommand cmd = cn.CreateCommand())
    //        {
    //            cmd.CommandText = "SELECT EventLogoMain FROM Portal.EventsActive WHERE IsActive='1'";
    //            cn.Open();
    //            strReturn = cmd.ExecuteScalar().ToString();
    //        }
    //    }
    //    return strReturn;
    //}

    //protected void LoadActiveTeamScores()
    //{
    //    string strWrite = "";
    //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    //    {
    //        using (SqlCommand cmd = cn.CreateCommand())
    //        {
    //            cmd.CommandText = "SELECT TeamID,(SELECT Name FROM Portal.Team WHERE ActivityID=@ActivityID AND TeamID=pet.TeamID) AS Name, Rank, Score FROM Portal.EventTeamScore AS pet WHERE EventID=@EventID ORDER BY Rank";
    //            cmd.Parameters.Add(new SqlParameter("@EventID", GetActiveEvent()));
    //            cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
    //            cn.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            while (dr.Read())
    //            {
    //                //strWrite = strWrite + "<tr> " +
    //                //                          "<td><a href='http://hq.sti.edu/Synergy/TeamDetails.aspx?teamid=" + dr["TeamID"].ToString() + "'>" + dr["Name"].ToString() + "</a></td>" +
    //                //                          "<td style='text-align:left'>" + dr["Score"].ToString() + "</td>" +
    //                //                          "</tr>";

    //                strWrite = strWrite + "<div class='masterpanelcontent' style='line-height:10px'>" +
    //                                          "<div class='left'><a href='" + clsSystemConfigurations.PortalRootURL + "/Synergy/TeamDetails.aspx?teamid=" + dr["TeamID"].ToString() + "'>" + dr["Name"].ToString() + "</a></div>" +
    //                                          "<div class='right'>" + dr["Score"].ToString() + "</div>" +
    //                                          "<div class='clearer'>&nbsp;</div>" +
    //                                      "</div>";
    //            }
    //        }
    //    }


    //    if (strWrite.Length > 0)
    //    {
    //        //strWrite = "<div class='masterpanel'>" + GetActiveEventName() + "</div>" + strWrite + "<div class='masterpanelspace'></div>";
    //        strWrite = "<div class='' style='font-weight:bold'>Team Standing</div>" + strWrite + "<div class='masterpanelspace'></div>";

    //        Response.Write(strWrite);
    //    }
    //}

    private string GetFileNameLogo(int teamID)
    {
        string strReturn = "";
        switch (teamID)
        {
            case 1:
                strReturn = "BBBx50.jpg";
                break;
            case 2:
                strReturn = "RPx50.jpg";
                break;
            case 3:
                strReturn = "SVx50.jpg";
                break;
            case 4:
                strReturn = "OCx50.jpg";
                break;
            case 5:
                strReturn = "Orange50.jpg";
                break;
            case 6:
                strReturn = "Green50.jpg";
                break;
            case 7:
                strReturn = "Blue50.jpg";
                break;
            case 8:
                strReturn = "Red50.jpg";
                break;
            case 10:
                strReturn = "Synergy_Red50.png";
                break;
            case 11:
                strReturn = "Synergy_Blue50.png";
                break;
            case 12:
                strReturn = "Synergy_Green50.png";
                break;
            case 13:
                strReturn = "Synergy_Gray50.png";
                break;
            case 14:
                strReturn = "STIGlobal_50.png";
                break;
        }
        return strReturn;
    }


    ///////////////////////////////////////////////////////
    ///// REMOVE DUE TO HEAVY LOAD ON WEBPAGE        ////// 
    ///// UNUSED METHODS                             //////
    ///// BY: calvin cavite Feb 20, 2018             //////
    ///////////////////////////////////////////////////////

    //private string LoadLatestScheduleTeams(int gameID, int eventID)
    //{
    //    string strReturn = "";

    //    List<EventGameTeam> eventGameTeamList = new List<EventGameTeam>();
    //    using (PortalDataContext pdc = new PortalDataContext())
    //    {
    //        eventGameTeamList = (from egt in pdc.EventGameTeams
    //                             where egt.GameID == gameID
    //                             orderby egt.Rank.Value
    //                             select egt).ToList();
    //    }

    //    foreach (EventGameTeam egt in eventGameTeamList)
    //    {
    //        strReturn += (strReturn == "" ? "" : "<tr><td  style='color:Red;border-color: #FFFFFF;'>&nbsp;<b>Versus&nbsp;</b></td></tr>") +
    //                   "<tr><td style='border-color: #FFFFFF;font-size:15px; line-height:15px'><b><a href='../Synergy/TeamDetails.aspx?teamid=" + egt.TeamID + "'>" + DALPortal.GetTeamName(egt.TeamID) + "</a></b></td></tr>";
    //    }
    //    return "<tr><td style='border-color: #FFFFFF;'><table align='center' style='border-color: #FFFFFF;'>" + strReturn + "</table></td></tr>";
    //}

    //protected void LoadLatestSchedule()
    //{
    //    string strWrite = "";
    //    int intCountEvents = 0;
    //    int intCount = 0;
    //    DateTime latestDate = DALPortal.GetLatestGameDate();

    //    if (latestDate != DateTime.MinValue)
    //    {
    //        DateTime latestDateStart = clsDateTime.ChangeTimeToStart(latestDate);
    //        DateTime latestDateEnd = clsDateTime.ChangeTimeToEnd(latestDate);

    //        List<EventGame> eventGamesList = new List<EventGame>();

    //        using (PortalDataContext pdc = new PortalDataContext())
    //        {
    //            eventGamesList = (from eg in pdc.EventGames
    //                              where eg.IsActive == true && eg.StartDate >= latestDateStart && eg.IsFinished == false
    //                              orderby eg.StartDate
    //                              select eg).ToList();
    //        }
    //        intCount = eventGamesList.Count;
    //        foreach (EventGame eg in eventGamesList)
    //        {
    //            if (intCountEvents > 4)
    //            { break; }
    //            strWrite += "<div class='GridBorder' style='text-align:center;border-color: #FFFFFF; font-size: 11px; line-height:5px'>" +
    //                                "<table style='width:100%;border-color: #FFFFFF;'>" +
    //                                     "<tr><td style='border-color: #FFFFFF;'><b><a href='" + clsSystemConfigurations.PortalRootURL + "/Synergy/EventDetails.aspx?eventid=" + eg.EventID.ToString() + "'>" + DALPortal.GetEventName(eg.EventID) + "</a></b></td></tr>" +
    //                                     "<tr><td style='border-color: #FFFFFF;'>" + DALPortal.GetGamePhaseName(eg.GamePhase.ToString()) + "</td></tr>" +
    //                                     this.LoadLatestScheduleTeams(eg.GameID, eg.EventID) +
    //                                     "<tr><td style='color:Black;border-color: #FFFFFF;'>" + eg.StartDate.ToString("hh:mm tt ddd, MMM dd") + "</td></tr>" +
    //                                     "<tr><td style='color:Black;border-color: #FFFFFF;'>@ " + eg.Location + "</td></tr>" +
    //                                "</table><hr/>" +
    //                           "</div>";
    //            intCountEvents++;
    //        }
    //    }

    //    if (intCount > 0)
    //    {
    //        masterlitLatestSchedule.Text = "<div class='' ><div class='' style='font-weight:bold'>Game Schedule</div><div class='masterpanelspace'></div>" + strWrite + "</div>";
    //    }
    //}

    protected void LoadNotification()
    {
        string strWrite = "";
        int intpMrcfUnread = 0;
        int intpRequisitionUnread = 0;
        int intpCrs = 0;
        int intpTransmittal = 0;
        string strpUserName = Request.Cookies["Speedo"]["UserName"].ToString();
        //imgpnlavatar.ImageUrl = "~/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg";
        int intpNotificationUnread = clsMessage.CountUnRead(strpUserName);

        ///Petty Cash - Rollie
        int intpPCAS = clsPCASApproval.CountForPCASApproval(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpPCAS > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/Finance/PCASH/PettyCashRequestMenu.aspx'> Petty Cash (" + intpPCAS + ")</a></div>";
        }

        int intpPCASCustodian = clsPCASRequestCustodian.CountForApproval(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpPCASCustodian > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/Finance/PCASH/PettyCashRequestCashierMenu.aspx'> Petty Cash (" + intpPCASCustodian + ")</a></div>";
        }
        
        int intpPCASFPC1 = clsPCASApproval.CountForPCASApproval(clsPCASFPCApprover.GetUsername(1));
        if (intpPCASFPC1 > 0 && clsPCASFPCApprover.IsExisting(Request.Cookies["Speedo"]["UserName"].ToString())==true)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/Finance/PCASH/PettyCashRequestFinanceMenu.aspx'> Petty Cash - FPC (" + intpPCASFPC1 + ")</a></div>";
        }
        //////

        //Transmittal
        intpTransmittal = Convert.ToInt16(clsTransmittal.CountForApproval(Request.Cookies["Speedo"]["UserName"].ToString()));
        if (intpTransmittal > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/CIS/Transmittal/TranMenu.aspx'> Transmittal (" + intpTransmittal + ")</a></div>";
            //lnkpnllftATW.Text = "<b>Authority to Work (" + intpATW + ")</b>";
            //lnkpnllftATW.Font.Bold = true;
        }

        int intpATW = clsATW.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpATW > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/ATW/ATWMenu.aspx'> Authority To Work (" + intpATW + ")</a></div>";
            //lnkpnllftATW.Text = "<b>Authority to Work (" + intpATW + ")</b>";
            //lnkpnllftATW.Font.Bold = true;
        }

        int intGroupUpdate = GroupUpdateApproval.GetCountForModification(Request.Cookies["Speedo"]["UserName"].ToString()) + GroupUpdateApproval.GetCountForApprovalLevel1(Request.Cookies["Speedo"]["UserName"].ToString()) + GroupUpdateApproval.GetCountForApprovalLevel2(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intGroupUpdate > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/GroupUpdate/GroupUpdateMain.aspx'> Group Update (" + intGroupUpdate + ")</a></div>";
            //lnkpnllftATW.Text = "<b>Authority to Work (" + intpATW + ")</b>";
            //lnkpnllftATW.Font.Bold = true;
        }

        int intpIAR = clsIAR.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpIAR > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/IAR/IARMenu.aspx'> Internet Request (" + intpIAR + ")</a></div>";
            //lnkpnllftIAR.Text = "<b>Internet Request (" + intpIAR + ")</b>";
            //lnkpnllftIAR.Font.Bold = true;
        }

        using (SqlConnection cnp = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmdp = cnp.CreateCommand();
            cmdp.CommandText = "SELECT COUNT(mrcfcode) FROM CIS.Mrcf WHERE (username='" + strpUserName + "' AND status='M') OR (sprvcode='" + strpUserName + "' AND sprvstat='F') OR (headcode='" + strpUserName + "' AND headstat='F' AND sprvstat IN ('A','X','N')) OR (proccode='" + strpUserName + "' AND procstat='F' AND ((sprvstat='A' AND headstat='N') OR (headstat='A')))";
            cnp.Open();
            intpMrcfUnread = (int)cmdp.ExecuteScalar();
            //Updated by charlie bachiller
            //Updated due to voided transaction on requisition still counted
            //cmdp.CommandText = "SELECT COUNT(requcode) FROM CIS.Requisition WHERE (username='" + strpUserName + "' AND status='M') OR (sprvcode='" + strpUserName + "' AND sprvstat='F') OR (headcode='" + strpUserName + "' AND headstat='F' AND sprvstat IN ('A','X','N')) OR (suppcode='" + strpUserName + "' AND (suppstat='F' OR suppstat='P'))";
            cmdp.CommandText = "SELECT COUNT(requcode) FROM CIS.Requisition WHERE ((username='" + strpUserName + "' AND status='M') OR (sprvcode='" + strpUserName + "' AND sprvstat='F'  AND status='F') OR (headcode='" + strpUserName + "' AND headstat='F' AND sprvstat IN ('A','X','N')   AND status='F') OR (suppcode='" + strpUserName + "' AND (suppstat='F' OR suppstat='P')  AND status IN ('A','P')))";
            intpRequisitionUnread = (int)cmdp.ExecuteScalar();

            //cmdp.CommandText = "SELECT COUNT(username) FROM Users.Users WHERE DATEPART(mm,brthdate)='" + DateTime.Now.Month + "' AND DATEPART(dd,brthdate)='" + DateTime.Now.Day + "' AND pstatus='1'";
            //intpCelebrants = (int)cmdp.ExecuteScalar();

            cmdp.CommandText = "SELECT COUNT(crscode) FROM CM.Crs WHERE (cmhname='" + strpUserName + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='F')) OR (ccname='" + strpUserName + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='E' OR pstatus='P'))";
            intpCrs = (int)cmdp.ExecuteScalar();
        }

        if (intpMrcfUnread > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/CIS/MRCF/MRCFMenu.aspx'> MRCF (" + intpMrcfUnread + ")</a></div>";
            //lnkpnllftMRCF.Text = "<b>MRCF (" + intpMrcfUnread + ")</b>";
        }

        if (intpRequisitionUnread > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/CIS/Requisition/RequMenu.aspx'> Requisition (" + intpRequisitionUnread + ")</a></div>";
            //lnkpnllftRequisition.Text = "<b>Requisition (" + intpRequisitionUnread + ")</b>";
            //lnkpnllftRequisition.Font.Bold = true;
        }


        int intpCATA = 0;
        int intCtrCATA = 0;
        
        //Added by charlie for cata
        //add by charlie
        DataTable tblEndorserApproval = clsCATAApproval.GetDSGForApprovalEndorser(Request.Cookies["Speedo"]["UserName"]);
        DataTable tblAuthorizeApproval = clsCATAApproval.GetDSGForApprovalAuthorize(Request.Cookies["Speedo"]["UserName"]);
        intCtrCATA = tblEndorserApproval.Rows.Count + tblAuthorizeApproval.Rows.Count;


        if (intCtrCATA > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/Finance/CATA/FinanceCataMenu.aspx'> Request For CATA (" + intCtrCATA + ")</a></div>";
        }

        ////cata for approvals

        //Added by charlie for RFP
        int intpRFP = 0;
        int intCtrRFP = 0;
        DataTable tblRFP = clsRFPRequest.GetDSGMainFormApprover(Request.Cookies["Speedo"]["UserName"]);
        foreach (DataRow drw in tblRFP.Rows)
        {
            if (clsFinanceApprover.IsAuthoritary(Request.Cookies["Speedo"]["UserName"], "ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
            {
                if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "")
                { continue; }

                else if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "2")
                { continue; }

                else if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "1")
                { continue; }

                else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "2")
                { continue; }

                else
                { intpRFP += 1; }
            }

            else if (clsFinanceApprover.IsEndorder1(Request.Cookies["Speedo"]["UserName"], "ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
            {
                if (drw["Endorsed1Status"].ToString().Trim() != "2")
                { continue; }

                else
                { intpRFP += 1; }
            }

            else if (clsFinanceApprover.IsEndorder2(Request.Cookies["Speedo"]["UserName"], "ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
            {
                if (drw["Endorsed2Status"].ToString().Trim() != "2")
                { continue; }
                else
                { intpRFP += 1; }
            }

            else
            { intpRFP += 1; }

            intCtrRFP++;
        }
        if (intCtrRFP > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/Finance/RFP/RFPMenu.aspx'> Request For Payment (" + intpRFP + ")</a></div>";
        }

        //////////////////////////
        ////// HRMS Summary //////
        //////////////////////////


        int intpLeave = clsLeave.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpLeave > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/Leave/LeaveMenu.aspx'> Leave (" + intpLeave + ")</a></div>";
        }

        int intpOvertime = clsOvertime.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpOvertime > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/Overtime/OvertimeMenu.aspx'> Overtime (" + intpOvertime + ")</a></div>";
        }

        int intpOB = clsOB.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"]);
        if (intpOB > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/OB/OBMenu.aspx'> Official Business (" + intpOB + ")</a></div>";
        }

        int intpUndertime = clsUndertime.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpUndertime > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/Undertime/UndertimeMenu.aspx'> Undertime (" + intpUndertime + ")</a></div>";
        }


        /////////////////////////////////////
        //  UNUSED                         //
        //  By Calvin Cavite Feb 20, 2018  //
        /////////////////////////////////////
        ////Reward Points Notification
        //string strUsername = Request.Cookies["Speedo"]["UserName"].ToString();
        //if (clsRewardApproval.IsEncoder(strUsername) || clsRewardApproval.IsApproverHR(strUsername))
        //{
        //    int intReward = 0;
        //    if (clsRewardApproval.IsEncoder(strUsername))
        //    {
        //        intReward = clsRewardApproval.ForModification();
        //    }
        //    if (clsRewardApproval.IsApproverHR(strUsername))
        //    {
        //        intReward = clsRewardApproval.GetDSGForApprovalLevel1(strUsername).Rows.Count + clsRewardApproval.GetDSGForApprovalLevel2(strUsername).Rows.Count;
        //    }


        //    if (intReward > 0)
        //    {
        //        strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/RewardPoint/TransactionMain.aspx'> Rewards Point (" + intReward + ")</a></div>";
        //    }
        //}


 	//////////////////////////
        ////// MRCF Assign ///////
        //////////////////////////


        int intAssignMRCF = clsMRCFAssign.GetTotalAssigned(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intAssignMRCF > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/CIS/MRCF/MRCFMenu.aspx'> Assigned MRCF (" + intAssignMRCF + ")</a></div>";
        }

        //////////////////////////
        ////// Journal Approval ///////
        //////////////////////////

        int intJournalCount = EmployeeJournalApproval.GetApprovalCount(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intJournalCount > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/EmployeeJournal/EmployeeJournalListFApproval.aspx'> Journal (" + intJournalCount + ")</a></div>";
        }


        if (strWrite.Length > 0)
        {
            divNotification.Visible = true;
            ltNotification.Text = strWrite;
        }
        else
        {
            divNotification.Visible = false;            
        }
    }

    protected void LoadPicture()
    {
        try
        {
            string strWrite = "";
            string strpUserName = Request.Cookies["Speedo"]["UserName"].ToString();
            //imgpnlavatar.ImageUrl = "~/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg";

            strWrite = "<div id='headerUserImage'><a href='" + clsSystemConfigurations.PortalRootURL + "/Userpage/Userpage.aspx?username=" + strpUserName + "'><img class='circle-image' id='imgpnlavatar' src='" + clsSystemConfigurations.PortalRootURL + "/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg'/></a></div>";
            Response.Write(strWrite);
        }
        catch
        {
            Response.Redirect("MemberLogin.aspx");
        }
    }

    protected void LoadAnnouncement()
    {
        string strWrite = "";
        /*
        List<Thread> threadList = new List<Thread>();
        using (ThreadDataContext tdc = new ThreadDataContext())
        {
            threadList = (from t in tdc.Threads
                          where t.IsPosted == true && t.IsActive == true
                          orderby t.PostedDate descending
                          select t).Take(7).ToList();
        }

        foreach (Thread t in threadList)
        {
            strWrite = strWrite + "<li>" +
                                    "<div class='left'><a href='Threads/Thread.aspx?threadid=" + t.ThreadID.ToString() + "&page=1' title='" + t.Title + "'>" + clsString.CutString(t.Title, 30) + "</a>" + "</div>" +
                                    "<div class='right'>" + t.PostedDate.Value.ToString("ddd MMM, dd, yyyy") + "</div>" +
                                    "<div class='clearer'>&nbsp;</div>" +
                                  "</li>";
        }
        Response.Write(strWrite);
        */
    }

    public void Authen()
    {
        try
        {
            bool blnAuthenticated = false;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username,email FROM Users.Users WHERE username=@username AND pstatus='1'";
                cmd.Parameters.Add(new SqlParameter("@email", Response.Cookies["Speedo"]["UserName"].ToString()));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                blnAuthenticated = dr.Read();
                if (blnAuthenticated)
                {
                    Response.Cookies["Speedo"].Expires = DateTime.Now.AddYears(1);
                }
                else
                {
                    Response.Cookies["Speedo"].Expires = DateTime.Now.AddDays(-1);
                    Response.Redirect("MemberLogin.aspx");

                }
                dr.Close();
            }
        }
        catch {
            if (Request.Cookies["Speedo"] != null)
            {
                Response.Cookies["Speedo"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect("MemberLogin.aspx");
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // this.LoadSynergyScore();

            if (!Page.IsPostBack)
            {
                //RefreshShoutBox();
                //this.Authen();
                clsSpeedo.Authenticate(Request.Cookies["Speedo"]["UserName"].ToString());
                //this.LoadLatestSchedule();

            }
            Usernm.Text=clsUsers.GetName(Request.Cookies["Speedo"]["UserName"].ToString());
            //if (GetActiveEventName() != string.Empty)
            //{
            //    LoadActiveTeamScores();
            //}

            LoadNotification();           
            lblDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MMMM dd, yyyy");
            lblDay.Text = Convert.ToDateTime(DateTime.Now).ToString("dddd");
            string strURLurl = HttpContext.Current.Request.Url.AbsoluteUri;

            //lnkPnlRewardPoint.Text = "My Rewards: <b>" + string.Format("{0:n2}", clsRewardDetail.GetPoints(Request.Cookies["Speedo"]["UserName"])) + "</b>";

            if (strURLurl.Contains("SynergyHome.aspx") || strURLurl.Contains("SynergyHome2.aspx"))
            {
                //trSportsFestLogo.Visible = false;
                //trSportsFestLogoSpace.Visible = false;
                //trSportsSchedule.Visible = false;
                //trSportsScheduleSpace.Visible = false;
                //trSportsFestActiveTeam.Visible = false;
                //trSportsFestActiveTeamSpace.Visible = false;
            }

            //lnkPnlUsername.Text = strpUserName;
            //lnkPnlUsername.NavigateUrl = "~/Userpage/UserPage.aspx?username=" + strpUserName;


        }
        catch
        {
            //Response.Redirect("~/UnderConstruction.aspx");
        }
    }

}
