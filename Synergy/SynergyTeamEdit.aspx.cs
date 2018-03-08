using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;
using System.Configuration;

public partial class Synergy_SynergyTeamEdit : System.Web.UI.Page
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]))
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
            LoadActiveEvents();
            ddlCaptain.DataSource = clsEmployee.DSLUsername();
            ddlCaptain.DataValueField = "pvalue";
            ddlCaptain.DataTextField = "ptext";
            ddlCaptain.DataBind();

            ddlViceCaptain.DataSource = clsEmployee.DSLUsername();
            ddlViceCaptain.DataValueField = "pvalue";
            ddlViceCaptain.DataTextField = "ptext";
            ddlViceCaptain.DataBind();
            LoadDetails();

            btnBack.Attributes.Add("onclick", "history.back(); return false");

        }
    }

    protected string LoadEvent(string pTeamID)
    {
        string strReturn = "";
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT EventID From Portal.EventTeamScore WHERE TeamID=@TeamID";
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@TeamID", pTeamID));
                strReturn = cmd.ExecuteScalar().ToString();
            }
        }
        catch { }
        return strReturn;
    }

    protected void LoadActiveEvents()
    {
        DataTable tblEvents = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT EventID AS pValue, Name AS pText From Portal.Events WHERE ActivityID=@ActivityID ORDER BY pText";
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblEvents);
            }
        }

        DataTable tblEventsFinal = new DataTable();
        tblEventsFinal.Columns.Add("pvalue");
        tblEventsFinal.Columns.Add("ptext");

        DataRow dr = tblEventsFinal.NewRow();
        dr["pvalue"] = string.Empty;
        dr["ptext"] = "-";
        tblEventsFinal.Rows.Add(dr);

        foreach (DataRow drEvents in tblEvents.Rows)
        {
            dr = tblEventsFinal.NewRow();
            dr["pvalue"] = drEvents["pvalue"];
            dr["ptext"] = drEvents["ptext"];
            tblEventsFinal.Rows.Add(dr);
        }


        ddlEvent.DataSource = tblEventsFinal;
        ddlEvent.DataValueField = "pvalue";
        ddlEvent.DataTextField = "ptext";
        ddlEvent.DataBind();
    }

    protected void LoadDetails()
    {

        string strTeamId = Request.QueryString["teamid"].ToString();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Portal.Team WHERE TeamID=@TeamID";
                cmd.Parameters.Add(new SqlParameter("@TeamID", Request.QueryString["teamid"].ToString()));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtTeamName.Text = dr["Name"].ToString();
                    ddlCaptain.SelectedValue = dr["Captain"].ToString();
                    ddlViceCaptain.SelectedValue = dr["ViceCaptain"].ToString();
                    chkActive.Checked = dr["IsActive"].ToString() == "True" ? true : false;
                }
            }
        }

        ddlEvent.SelectedValue = LoadEvent(strTeamId);
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (SaveEdit() > 0)
        {
            Response.Redirect("Synergy.aspx");
        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        
    }

    protected int SaveEdit()
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            cmd.CommandText = "UPDATE Portal.Team SET Name=@Name,Captain=@Captain,ViceCaptain=@ViceCaptain,IsActive=@IsActive WHERE TeamID=@TeamID";
            cmd.Parameters.Add(new SqlParameter("@Name", txtTeamName.Text));
            cmd.Parameters.Add(new SqlParameter("@Captain", ddlCaptain.SelectedValue.ToString()));
            cmd.Parameters.Add(new SqlParameter("@ViceCaptain", ddlViceCaptain.SelectedValue.ToString()));
            cmd.Parameters.Add(new SqlParameter("@IsActive", chkActive.Checked));
            cmd.Parameters.Add(new SqlParameter("@TeamID", Request.QueryString["teamid"].ToString()));
            intReturn = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();

            if (ddlEvent.SelectedValue != string.Empty)
            {
                cmd.CommandText = "SELECT EventTeamScoreID FROM Portal.EventTeamScore WHERE EventID=@EventID AND TeamID=@TeamID";
                cmd.Parameters.Add(new SqlParameter("@TeamID", Request.QueryString["teamid"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@EventID", ddlEvent.SelectedValue.ToString()));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE Portal.EventTeamScore SET EventID=@EventID WHERE TeamID=@TeamID";
                    cmd.Parameters.Add(new SqlParameter("@TeamID", Request.QueryString["teamid"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@EventID", ddlEvent.SelectedValue.ToString()));
                    intReturn = cmd.ExecuteNonQuery();

                }
                else
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO Portal.EventTeamScore (EventID,TeamID,Rank,Score) VALUES(@EventID, @TeamID,'0', '0')";
                    cmd.Parameters.Add(new SqlParameter("@TeamID", Request.QueryString["teamid"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@EventID", ddlEvent.SelectedValue.ToString()));
                    intReturn = cmd.ExecuteNonQuery();

                }
            }
            else
            {
                cmd.Parameters.Clear();
                string strTeamId = Request.QueryString["teamid"].ToString();
                cmd.CommandText = "DELETE FROM  Portal.EventTeamScore WHERE TeamID=@TeamID AND EventID=@EventID";
                cmd.Parameters.Add(new SqlParameter("@TeamID", Request.QueryString["teamid"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@EventID", LoadEvent(strTeamId)));
                intReturn = cmd.ExecuteNonQuery();
            }

        }
        return intReturn;
    }
}