using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Synergy_SurveyResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strWrite = "";
        DataTable tblEvents = SynergySurvey.GetEvents();
        foreach (DataRow drEvents in tblEvents.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + drEvents["itemcode"].ToString() + "</td>" +
                                        "<td class='GridRows'>" + drEvents["itemdesc"].ToString() + "</td>" +
                                        "<td class='GridRows'>" + SynergySurvey.Yes("M", drEvents["itemcode"].ToString().ToInt()) + "</td>" +
                                        "<td class='GridRows'>" + SynergySurvey.Yes("F", drEvents["itemcode"].ToString().ToInt()) + "</td>" +
                                        "<td class='GridRows'>" + SynergySurvey.YesTotal(drEvents["itemcode"].ToString().ToInt()) + "</td>" +
                                        "<td class='GridRows'>" + SynergySurvey.Preferred("M", drEvents["itemcode"].ToString().ToInt()) + "</td>" +
                                        "<td class='GridRows'>" + SynergySurvey.Preferred("F", drEvents["itemcode"].ToString().ToInt()) + "</td>" +
                                        "<td class='GridRows'>" + SynergySurvey.PreferredTotal(drEvents["itemcode"].ToString().ToInt()) + "</td>" +
                                  "</tr>";
        }
        lblResult.Text = strWrite;

        strWrite = "";

        lblOthers.Text = "<tr><td>" + SynergySurvey.UserOthers() + "</td></tr>";

        int intRespondentMale = SynergySurvey.CountRespondend("M");
        int intRespondentFemale = SynergySurvey.CountRespondend("F");
        int intRespondentTotal = intRespondentFemale + intRespondentMale;
            
        strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + intRespondentMale + "</td>" +
                                        "<td class='GridRows'>" + intRespondentFemale + "</td>" +
                                        "<td class='GridRows'>" + intRespondentTotal + "</td>" +
                              "</tr>";

        lblRespondents.Text = strWrite;
        strWrite = "";

        string strStatus = "";
        int intParticipate = 0;
        int intNotParticipate = 0;

        DataTable tblEmployees = SynergySurvey.GetEmployee();
        foreach (DataRow drEmployees in tblEmployees.Rows)
        {
            if (drEmployees["status"].ToString() == "Yes")
            {
                strStatus = "<td class='GridRowsGreen'>&nbsp;</td>";
                intParticipate += 1;
            }
            else
            {
                strStatus = "<td class='GridRowsRed'>&nbsp;</td>";
                intNotParticipate += 1;
            }
            strWrite = strWrite + "<tr>" +
                                            "<td class='GridRows'>" + drEmployees["username"] + "</td>" +
                                            strStatus +
                                  "</tr>";
        }

        strWrite = strWrite + "<tr>" +
                                            "<td class='GridRows'>Total Employees:</td>" +
                                           "<td class='GridRows' style='text-align:center'>" + tblEmployees.Rows.Count + "</td>" +
                              "</tr>";

        strWrite = strWrite + "<tr>" +
                                            "<td class='GridRows'>Participated:</td>" +
                                           "<td class='GridRows' style='text-align:center'>" + intParticipate + "</td>" +
                              "</tr>";

        strWrite = strWrite + "<tr>" +
                                            "<td class='GridRows'>Not Participated:</td>" +
                                           "<td class='GridRows' style='text-align:center'>" + intNotParticipate + "</td>" +
                              "</tr>";
        lblStatus.Text = strWrite;


        strWrite = "";
        DataTable tblPrefered = SynergySurvey.GetMostPreferred();
        foreach (DataRow drPrefered in tblPrefered.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                                "<td class='GridRows'>" + drPrefered["rank"].ToString() + "</td>" +
                                                "<td class='GridRows' style='text-align:center'>" + drPrefered["itemdesc"].ToString() + "</td>" +
                                                "<td class='GridRows' style='text-align:center'>" + drPrefered["count"].ToString() + "</td>" +
                                  "</tr>";
        }

        lblPreferred.Text = strWrite;
       
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SurveyResultExcel.aspx");
    }
}