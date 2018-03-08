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
using STIeForms;

public partial class CIS_MRCF_MRCFAirfare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         clsSpeedo.Authenticate();
         if (!Page.IsPostBack)
         { 
         }
    }

    protected void LoadAirlines()
    {

        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT airlcode,Airldesc,URL,status FROM cis.mrcfairlines ORDER BY airldesc ASC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string strStatus = "<td runat='server' class='GridRows' style='vertical-align:middle;'><center> <a href='#' runat='server' id='" + dr["airlcode"].ToString() + "' onclick='UpdateStatus(this.id)'><img src='../../Support/unchecked.png' alt='' /></a> </center>";
                if  (dr["status"].ToString() == "1")
                {
                    strStatus = "<td runat='server' class='GridRows' style='vertical-align:middle;'><center> <a href='#' runat='server' id='" + dr["airlcode"].ToString() + "' onclick='UpdateStatus(this.id)'><img src='../../Support/checked.png' alt='' /></a> </center>";
                }
                //Response.Cookies["MRCF"]["MRCFCODE"] = dr["mrcfcode"].ToString();'" + + "'
                strWrite = strWrite + "<tr>" +
       "<td runat='server' class='GridRows' style='vertical-align:middle; text-align:center;'>" +
       "<a href='#' runat='server' id='" + dr["airlcode"].ToString() + "' onclick='ModalPop(this.id)'><img src='../../Support/edit16.png' alt='' /></a>" +
       "<td class='GridRows'style='vertical-align:middle;'><a href=javascript:winpop('" + dr["URL"].ToString() + "',800,600,0,0,1); runat='server'>"+ dr["airldesc"].ToString() + "</a>" +
       strStatus +
       "</tr>";
                intCtr++;
            }
            dr.Close();
        }

        Response.Write(strWrite);
        if (intCtr == 0)
            Response.Write("<tr><td colspan='3' class='GridRows' style='text-align:center'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='3' class='GridRows' style='text-align:center'>[ " + intCtr + " records found ]</td></tr>");
    }
    protected void txtURL_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {
        txtURL.Text = "";
        txtAirlineName.Text = "";
        mdlpopupAdd.Show();
    }

    protected void lnkbtnHideAdd_Click(object sender, EventArgs e)
    {
        mdlpopupAdd.Hide();
    }

    protected void btnMPopup_Click(object sender, EventArgs e)
    {
        clsMRCFAirlines objAirlines = new clsMRCFAirlines();
        objAirlines.Fill(inpHide.Value);
        txtEditAirlName.Text = objAirlines.AirlineDescription;
        txtEditURL.Text = objAirlines.URL;
        Boolean blnStatus = false;
        if (objAirlines.Status == "1"){blnStatus = true;}
        ckbAirlines.Checked = blnStatus;
        mdlpopupEdit.Show();

    }
    protected void lnkbtnHideEdit_Click(object sender, EventArgs e)
    {
        mdlpopupEdit.Hide();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        clsMRCFAirlines objAirlines = new clsMRCFAirlines();
        objAirlines.AirlineDescription = txtAirlineName.Text;
        objAirlines.URL = txtURL.Text;
        objAirlines.CreateBy = Request.Cookies["Speedo"]["UserName"].ToString();
        objAirlines.InsertAirlines();
        Response.Redirect("MRCFAirlines.aspx");
    }
    protected void btnEditSave_Click(object sender, EventArgs e)
    {
        clsMRCFAirlines objAirlines = new clsMRCFAirlines();
        objAirlines.CreateBy = Request.Cookies["Speedo"]["UserName"].ToString();
        objAirlines.AirlineCode = inpHide.Value;
        objAirlines.AirlineDescription = txtEditAirlName.Text;
        objAirlines.URL = txtEditURL.Text;
        if (ckbAirlines.Checked == true){objAirlines.Status = "1";}
        else { objAirlines.Status = "0";  }
        objAirlines.UpdateAirlines();
        Response.Redirect("MRCFAirlines.aspx");
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        clsMRCFAirlines objAirlines = new clsMRCFAirlines();
        if (clsMRCFAirlines.CheckStatus(inpHide.Value)) { objAirlines.Status = "0"; }
        else { objAirlines.Status = "1"; }
        objAirlines.AirlineCode = inpHide.Value;
        objAirlines.CreateBy = Request.Cookies["Speedo"]["UserName"].ToString();
        objAirlines.UpdateStatus();

    }

    }
