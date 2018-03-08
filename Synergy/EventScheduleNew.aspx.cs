using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Synergy_EventScheduleNew : System.Web.UI.Page
{
	private bool IsCorrectEntries()
	{
		string strErrorMessage = "";

		if (clsValidator.CheckDate(txtStartYear.Text.ToInt(), ddlStartMonth.SelectedValue.ToInt(), ddlStartDay.SelectedValue.ToInt(), ddlStartHour.SelectedValue.ToInt(), ddlStartMinute.SelectedValue.ToInt(), ddlStartTimePeriod.SelectedValue) == clsDateTime.SystemMinDate)
			strErrorMessage += "<br>Invalid date/time start entry.";

		if (clsValidator.CheckDate(txtEndYear.Text.ToInt(), ddlEndMonth.SelectedValue.ToInt(), ddlEndDay.SelectedValue.ToInt(), ddlEndHour.SelectedValue.ToInt(), ddlEndMinute.SelectedValue.ToInt(), ddlEndTimePeriod.SelectedValue) == clsDateTime.SystemMinDate)
			strErrorMessage += "<br>Invalid date/time end entry.";

		lblErrMsg.Text = strErrorMessage;
		divError.Visible = strErrorMessage != "";

		return strErrorMessage == "";
	}

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!Page.IsPostBack)
		{
			hdnEventID.Value = Request.QueryString["eventid"];

			using (PortalDataContext pdc = new PortalDataContext())
			{
				txtEvent.Text = (from ev in pdc.Events
								 where ev.EventID == hdnEventID.Value.ToInt()
								 select ev.Name).SingleOrDefault();
			}

			ddlStartMonth.DataSource = clsDateTime.GetMonths();
			ddlStartMonth.DataValueField = "pvalue";
			ddlStartMonth.DataTextField = "ptext";
			ddlStartMonth.DataBind();
			ddlStartDay.DataSource = clsDateTime.GetDays();
			ddlStartDay.DataValueField = "pvalue";
			ddlStartDay.DataTextField = "ptext";
			ddlStartDay.DataBind();
			ddlStartHour.DataSource = clsDateTime.GetHours();
			ddlStartHour.DataValueField = "pvalue";
			ddlStartHour.DataTextField = "ptext";
			ddlStartHour.DataBind();
			ddlStartMinute.DataSource = clsDateTime.GetMinutes();
			ddlStartMinute.DataValueField = "pvalue";
			ddlStartMinute.DataTextField = "ptext";
			ddlStartMinute.DataBind();
			ddlStartTimePeriod.DataSource = clsDateTime.GetTimePeriod();
			ddlStartTimePeriod.DataValueField = "pvalue";
			ddlStartTimePeriod.DataTextField = "ptext";
			ddlStartTimePeriod.DataBind();

			ddlEndMonth.DataSource = clsDateTime.GetMonths();
			ddlEndMonth.DataValueField = "pvalue";
			ddlEndMonth.DataTextField = "ptext";
			ddlEndMonth.DataBind();
			ddlEndDay.DataSource = clsDateTime.GetDays();
			ddlEndDay.DataValueField = "pvalue";
			ddlEndDay.DataTextField = "ptext";
			ddlEndDay.DataBind();
			ddlEndHour.DataSource = clsDateTime.GetHours();
			ddlEndHour.DataValueField = "pvalue";
			ddlEndHour.DataTextField = "ptext";
			ddlEndHour.DataBind();
			ddlEndMinute.DataSource = clsDateTime.GetMinutes();
			ddlEndMinute.DataValueField = "pvalue";
			ddlEndMinute.DataTextField = "ptext";
			ddlEndMinute.DataBind();
			ddlEndTimePeriod.DataSource = clsDateTime.GetTimePeriod();
			ddlEndTimePeriod.DataValueField = "pvalue";
			ddlEndTimePeriod.DataTextField = "ptext";
			ddlEndTimePeriod.DataBind();

			ddlGamePhase.DataSource = DALPortal.DSLGamePhase();
			ddlGamePhase.DataValueField = "pvalue";
			ddlGamePhase.DataTextField = "ptext";
			ddlGamePhase.DataBind();

			ddlStartMonth.SelectedValue = DateTime.Now.Month.ToString();
			ddlStartDay.SelectedValue = DateTime.Now.Day.ToString();
			txtStartYear.Text = DateTime.Now.Year.ToString();

			ddlEndMonth.SelectedValue = DateTime.Now.Month.ToString();
			ddlEndDay.SelectedValue = DateTime.Now.Day.ToString();
			txtEndYear.Text = DateTime.Now.Year.ToString();

			ddlWinner.DataSource = DALPortal.DSLTeamNA();
			ddlWinner.DataValueField = "TeamID";
			ddlWinner.DataTextField = "TeamName";
			ddlWinner.DataBind();
		}
    }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		if (this.IsCorrectEntries())
		{
			using (PortalDataContext pdc = new PortalDataContext())
			{
				EventGame eg = new EventGame()
				{
					EventID = hdnEventID.Value.ToInt(),
					GamePhase = ddlGamePhase.SelectedValue.ToChar(),
					StartDate = clsValidator.CheckDate(txtStartYear.Text.ToInt(), ddlStartMonth.SelectedValue.ToInt(), ddlStartDay.SelectedValue.ToInt(), ddlStartHour.SelectedValue.ToInt(), ddlStartMinute.SelectedValue.ToInt(), ddlStartTimePeriod.SelectedValue),
					EndDate = clsValidator.CheckDate(txtEndYear.Text.ToInt(), ddlEndMonth.SelectedValue.ToInt(), ddlEndDay.SelectedValue.ToInt(), ddlEndHour.SelectedValue.ToInt(), ddlEndMinute.SelectedValue.ToInt(), ddlEndTimePeriod.SelectedValue),
					Location = txtLocation.Text,
					WinnerTeamID = ddlWinner.SelectedValue.ToInt(),
					IsFinished = chkFinished.Checked,
					IsActive = true,
					CreatedBy = Request.Cookies["Speedo"]["UserName"],
					DateCreated = DateTime.Now
				};

				pdc.EventGames.InsertOnSubmit(eg);
				pdc.SubmitChanges();
			}
			Response.Redirect("EventDetails.aspx?eventid=" + Request.QueryString["eventid"]);
		}
	}

	protected void btnBack_Click(object sender, ImageClickEventArgs e)
	{
		Response.Redirect("EventDetails.aspx?eventid=" + Request.QueryString["eventid"]);
	}
}