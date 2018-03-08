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
using HRMS;
public partial class CMD_SIS_Schools : System.Web.UI.Page
{

	protected void LoadBranches()
	{
		string strWrite = "";
		int intCtr = 0;
        ////////////////////// REMOVE BY CALVIN CAVITE FEB 22, 2018
        ////////////////////// no use due to STI Portal change to PFIC Portal
        //using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
        //{
        //	SqlCommand cmd = cn.CreateCommand();
        //	cmd.CommandText = "SELECT schlcode,schlnam2,schlname,hqowned FROM CM.Schools ORDER BY schlnam2";
        //	cn.Open();
        //	SqlDataReader dr = cmd.ExecuteReader();
        //	while (dr.Read())
        //	{
        //		strWrite = "<tr>" +
        //														"<td class='GridRows'>" +
        //														"<img src='../../Support/" +(dr["hqowned"].ToString() == "1" ? "bookmark16.png" : "star16.png") + "' alt='' /></a>" +
        //														"</td>" +
        //														"<td class='GridRows'>" +
        //														 "<a href='SchoolDetails.aspx?schlcode=" + dr["schlcode"] + "&schlname=" + dr["schlname"] + "' style='font-size:small;'>" + dr["schlnam2"].ToString() + " (" + dr["schlcode"].ToString() + ")</a>" +
        //														"</td>" +
        //													"</tr>";
        //		Response.Write(strWrite);
        //		intCtr++;
        //	}
        //	dr.Close();
        //}
        //if (intCtr == 0)
        //	Response.Write("<tr><td colspan='2' class='GridRows'>No record found</td></tr>");
        //else
        //	Response.Write("<tr><td colspan='2' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");


        using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM dbo.Branches ORDER BY branchcode DESC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = "<tr>" +
                             "<td class='GridRows'>" +
                                "<a href='SchoolDetails.aspx?schlcode=" + dr["branchcode"] + "&schlname=" + dr["branchname"] + "'>" + dr["branchname"].ToString() + " (" + dr["branchcode"].ToString() + ")</a>" +
                             "</td>" +
                             "<td class='GridRows'>" +
                                 dr["branchaddress"].ToString()  +
                             "</td>" +
                             "<td class='GridRows'>" +
                                 dr["branchcontact"].ToString() +
                             "</td>" +
                             "<td class='GridRows'>" +
                                 dr["branchmnger"].ToString() +
                             "</td>" +
                             "<td class='GridRows'><a type='button' style='margin-left:5px;' href='branchdelete.aspx?branchcode=" + dr["branchcode"].ToString()+"' OnClick='btnDelete'><img src=/Support/Disapproved.png width='15px' height='15px' /></a></td>" +
                           "</tr>";
                
                Response.Write(strWrite);
                intCtr++;
            }
            dr.Close();
            cn.Close();
        }
        if (intCtr == 0)
            Response.Write("No record found");
        else
            Response.Write("[ " + intCtr + " records found ]");

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
            clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString());
        }
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SchoolDirectoryExcel.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (clsSchool branches = new clsSchool()) {

            branches.branchName = tbBranchnm.Text;
            branches.branchAddress = tbBranchaddress.Text;
            branches.branchEmail = tbBranchEmail.Text;
            branches.branchContact = tbBranchcontact.Text;
            branches.branchManager = tbBranchmnger.Text;

            if (branches.insert() == 1) {

                tbBranchnm.Text = "";
                tbBranchaddress.Text = "";
                tbBranchEmail.Text = "";
                tbBranchcontact.Text = "";
                tbBranchmnger.Text = "";
                //ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "alert('You have successfully added new branch'); window.location='" + Request.ApplicationPath + "CMD/SIS/Branches.aspx';", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModalSuccessEntry').modal('show');</script>", false);
            }
            
        }
    }   
}
