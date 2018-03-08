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

public partial class App_Master_NoRightPanel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadNotification();
        lblDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MMMM dd, yyyy");
        lblDay.Text = Convert.ToDateTime(DateTime.Now).ToString("dddd");
    }

    protected void LoadPicture()
    {
        string strWrite = "";
        string strpUserName = Request.Cookies["Speedo"]["UserName"].ToString();
        //imgpnlavatar.ImageUrl = "~/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg";

        strWrite = "<div id='headerUserImage'><a href='" + clsSystemConfigurations.PortalRootURL + "/Userpage/Userpage.aspx?username=" + strpUserName + "'><img id='imgpnlavatar' src='" + clsSystemConfigurations.PortalRootURL + "/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg' style='height:90px;width:90px;' /></a></div>";
        Response.Write(strWrite);
    }

    protected void LoadNotification()
    {
        string strWrite = "";
        int intpMrcfUnread = 0;
        int intpRequisitionUnread = 0;
        int intpCrs = 0;
        string strpUserName = Request.Cookies["Speedo"]["UserName"].ToString();
        //imgpnlavatar.ImageUrl = "~/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg";
        int intpNotificationUnread = clsMessage.CountUnRead(strpUserName);


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

        //Added by charlie for cata
        //add by charlie
        int intCtrCATA = 0;
        DataTable tblEndorserApproval = clsCATAApproval.GetDSGForApprovalEndorser(Request.Cookies["Speedo"]["UserName"]);
        DataTable tblAuthorizeApproval = clsCATAApproval.GetDSGForApprovalAuthorize(Request.Cookies["Speedo"]["UserName"]);
        intCtrCATA = tblEndorserApproval.Rows.Count + tblAuthorizeApproval.Rows.Count;

        if (intCtrCATA > 0)
        {
            lnkpnllftFinanceCataRequestMenu.Text = "<b>Request for CATA (" + intCtrCATA + ")</b>";
            lnkpnllftFinanceCataRequestMenu.Font.Bold = true;
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
            //lnkpnllftFinanceRequest.Text = "<b>Request for Payment (" + intpRFP + ")</b>";
            //lnkpnllftFinanceRequest.Font.Bold = true;
        }

        //if (intpCelebrants > 0)
        //    lnkpnllftBirthday.Text = "<b>Birthday Celebrators (" + intpCelebrants + ")</b>";

        //if (intpCrs > 0)
        //   lnkpnlrgtCMDCRS.Text = "<b>Courseware Request (" + intpCrs + ")</b>";

        //////////////////////////
        ////// HRMS Summary //////
        //////////////////////////



        int intpLeave = clsLeave.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpLeave > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/Leave/LeaveMenu.aspx'> Leave (" + intpLeave + ")</a></div>";
            //lnkpnllftLeave.Text = "<b>Leave (" + intpLeave + ")</b>";
            //lnkpnllftLeave.Font.Bold = true;
        }

        int intpOvertime = clsOvertime.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpOvertime > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/Overtime/OvertimeMenu.aspx'> Overtime (" + intpOvertime + ")</a></div>";
            //lnkpnllftOvertime.Text = "<b>Overtime (" + intpOvertime + ")</b>";
            //lnkpnllftOvertime.Font.Bold = true;
        }

        int intpOB = clsOB.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"]);
        if (intpOB > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/OB/OBMenu.aspx'> Official Business (" + intpOB + ")</a></div>";
            //lnkpnllftOB.Text = "<b>OB (" + intpOB + ")</b>";
            //lnkpnllftOB.Font.Bold = true;
        }

        int intpUndertime = clsUndertime.GetTotalForAttention(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intpUndertime > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRMS/Undertime/UndertimeMenu.aspx'> Undertime (" + intpUndertime + ")</a></div>";
            //lnkpnllftUndertime.Text = "<b>Undertime (" + intpUndertime + ")</b>";
            //lnkpnllftUndertime.Font.Bold = true;
        }


        //Reward Points Notification
        if (Request.Cookies["Speedo"]["UserName"].ToString() == "liezel.diego" || Request.Cookies["Speedo"]["UserName"].ToString() == "jay.jamandre" || Request.Cookies["Speedo"]["UserName"].ToString() == "giselle.batalla")
        {
            int intReward = 0;
            if (Request.Cookies["Speedo"]["UserName"].ToString() == "liezel.diego")
            {
                intReward = clsRewardApproval.GetDSGForApprovalLevel1("liezel.diego").Rows.Count;
            }
            else if (Request.Cookies["Speedo"]["UserName"].ToString() == "jay.jamandre")
            {
                intReward = clsRewardApproval.GetDSGForApprovalLevel2("jay.jamandre").Rows.Count;
            }
            else
            {
                intReward = clsRewardApproval.ForModification();
            }

            if (intReward > 0)
            {
                strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/RewardPoint/TransactionMain.aspx'> Rewards Point (" + intReward + ")</a></div>";
            }
        }


	//////////////////////////
        ////// MRCF Assign ///////
        //////////////////////////


        int intAssignMRCF = clsMRCFAssign.GetTotalAssigned(Request.Cookies["Speedo"]["UserName"].ToString());
        if (intAssignMRCF > 0)
        {
            strWrite += "<div class='masterpanelcontent'><a href='" + clsSystemConfigurations.PortalRootURL + "/CIS/MRCF/MRCFMenu.aspx'> Assigned MRCF (" + intAssignMRCF + ")</a></div>";
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
}
