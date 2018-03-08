using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Synergy_EventMenu : System.Web.UI.Page
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    private void LoadEventList()
    {
        using (PortalDataContext pdc = new PortalDataContext())
        {
            var q = (from e in pdc.Events
                     where e.IsActive == true && e.ActivityID == SynergyCurrentID
                     let DivisionName = (from ed in pdc.EventDivisions
                                              where ed.EventDivisionID == e.EventDivisionID
                                              select ed.Name).SingleOrDefault()
                     let CategoryName = (from ec in pdc.EventCategories
                                              where ec.EventCategoryID == e.EventCategoryID
                                              select ec.Name).SingleOrDefault()
                     let TeamLogo = (from t in pdc.Teams
                                               where t.TeamID == e.WinnerTeamID
                                               select t.TeamLogo).SingleOrDefault()
                     orderby e.Name
                     select new { 
                         EventID = e.EventID,
                         Name = e.Name,
                         EventDivisionName = DivisionName,
                         EventCategoryName = CategoryName,
                         MaxPoint = e.MaxPoint,
                         WinnerTeamID = e.WinnerTeamID,
                         WinnerTeamLogo = TeamLogo
                     }).ToList();

            dgEventMenu.DataSource = q;
            dgEventMenu.DataBind();
        }

        foreach (DataGridItem ditm in dgEventMenu.Items)
        {
            HiddenField phdnWinner = (HiddenField)ditm.FindControl("hdnWinner");
            Image pimgWinner = (Image)ditm.FindControl("imgWinner");
            pimgWinner.Visible = (phdnWinner.Value.Trim().Length != 0 && phdnWinner.Value != "0");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            btnNewRecord.Visible = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);
            this.LoadEventList();
        }
    }

    protected void btnNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("EventNew.aspx");
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Synergy.aspx");
    }


}