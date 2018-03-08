using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Synergy_TeamDetails : System.Web.UI.Page
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

	private void BindTeamComposition()
	{
		dgTeamComposition.DataSource = clsTeamMembers.DSGTeamMember(hdnTeamID.Value.ToInt());
		dgTeamComposition.DataBind();
		foreach (DataGridItem ditm in dgTeamComposition.Items)
		{
			HiddenField phdnUsername = (HiddenField)ditm.FindControl("hdnUsername");
			HiddenField phdnDepartment = (HiddenField)ditm.FindControl("hdnDepartment");
			HiddenField phdnDivision = (HiddenField)ditm.FindControl("hdnDivision");
			Label plblDepartment = (Label)ditm.FindControl("lblDepartment");

			string strPreviousEvents = DALPortal.GetJoinedEvents(phdnUsername.Value, 2);
            string strCurrentEvents = DALPortal.GetJoinedEvents(phdnUsername.Value, SynergyCurrentID);

			plblDepartment.Text = phdnDepartment.Value + "&nbsp;&nbsp;(" + phdnDivision.Value + ")";
			Image pimgPicture = (Image)ditm.FindControl("imgPicture");
			pimgPicture.ImageUrl = "http://hq.sti.edu/pictures/realpicture/" + phdnUsername.Value + ".jpg";
		}
	}

	private void LoadTeamMembers()
	{
		cblTeamMembers.DataSource = clsTeamMembers.DSLIncluded(hdnTeamID.Value.ToInt());
		cblTeamMembers.DataValueField = "pvalue";
		cblTeamMembers.DataTextField = "ptext";
		cblTeamMembers.DataBind();

		cblEmployeeList.DataSource = clsTeamMembers.DSLExcluded();
		cblEmployeeList.DataValueField = "pvalue";
		cblEmployeeList.DataTextField = "ptext";
		cblEmployeeList.DataBind();
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			hdnTeamID.Value = Request.QueryString["teamid"];
            trTeamCompositionEditor.Visible = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);

			Team thread = new Team();
			using (PortalDataContext pdc = new PortalDataContext())
			{
				thread = (from t in pdc.Teams
						  where t.TeamID == hdnTeamID.Value.ToInt()
						  select t).SingleOrDefault();
			}
			txtTeamName.Text = thread.Name;
			txtCaptain.Text = thread.Captain;
			txtViceCaptain.Text = thread.ViceCaptain;
			

			this.BindTeamComposition();
			this.LoadTeamMembers();

            imgpnlavatar.ImageUrl = DALPortal.GetTeamLogo(hdnTeamID.Value.ToInt());
            btnBack.Attributes.Add("onclick", "history.back(); return false");
		}
	}

	protected void btnBack_Click(object sender, ImageClickEventArgs e)
	{

	}

	protected void btnRemove_Click(object sender, ImageClickEventArgs e)
	{
		foreach (ListItem itm in cblTeamMembers.Items)
		{
			if (itm.Selected)
				clsTeamMembers.DeleteMember(hdnTeamID.Value.ToInt(), itm.Value);
		}
		this.LoadTeamMembers();
		this.BindTeamComposition();
	}

	protected void btnInclude_Click(object sender, ImageClickEventArgs e)
	{
		foreach (ListItem itm in cblEmployeeList.Items)
		{
            if (itm.Selected)
            {
                if (!clsTeamMembers.MemberExist(hdnTeamID.Value.ToInt(), itm.Value))
                {
                    clsTeamMembers.InsertMember(hdnTeamID.Value.ToInt(), itm.Value);
                }
            }
		}
		this.LoadTeamMembers();
		this.BindTeamComposition();
	}
}