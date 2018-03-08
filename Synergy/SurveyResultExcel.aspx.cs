using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Synergy_SurveyResultExcel : System.Web.UI.Page
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

        lblOthers.Text = "<tr><td>" + SynergySurvey.UserOthers() + "</td></tr>";

        strWrite = "";
        int intRespondentMale = SynergySurvey.CountRespondend("M");
        int intRespondentFemale = SynergySurvey.CountRespondend("F");
        int intRespondentTotal = intRespondentFemale + intRespondentMale;

        strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + intRespondentMale + "</td>" +
                                        "<td class='GridRows'>" + intRespondentFemale + "</td>" +
                                        "<td class='GridRows'>" + intRespondentTotal + "</td>" +
                              "</tr>";

        lblRespondents.Text = strWrite;

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + "Synergy_Survey_Result" + DateTime.Now.ToString("MM-dd-yy") + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
    }
}