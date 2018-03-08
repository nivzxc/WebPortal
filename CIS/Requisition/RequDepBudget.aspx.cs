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

public partial class CIS_Requisition_RequDepBudget : System.Web.UI.Page
{

    protected void BindItems()
    {
        DataTable tblRCBudget = new DataTable();
        string strFiscalYear = clsSpeedo.GetCurrentFiscalYear();
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT HR.RC.rccode,rcname,tbudget1,rbudget1,tbudget2,rbudget2,tbudget3,rbudget3,tbudget4,rbudget4 FROM Finance.QuarterBudget INNER JOIN HR.RC ON Finance.QuarterBudget.rccode = HR.RC.rccode WHERE accbcode='OSB' AND HR.Rc.divicode = '" + ddlDivision.SelectedValue + "' AND  fiscyear='" + strFiscalYear + "' ORDER BY rcname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblRCBudget);
        }
        dgRC.DataSource = tblRCBudget;
        dgRC.DataBind();
    }

    protected void Load_Departments()
    {
        string strWrite = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT rccode,rcname FROM HR.Rc WHERE divicode='" + ddlDivision.SelectedValue + "' ORDER BY rccode,rcname";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = "<tr>" +
                                                                "<td class='GridRows'>" +
                                                                    "<br><a href='' style='font-size:small;'>" + dr["rccode"] + " - " + dr["rcname"] + "</a><br>" +
                                                                "</td>" +
                                                                "<td class='GridRows' style='text-align:center;'>" +
                                                                "</td>" +
                                                            "</tr>";
                Response.Write(strWrite);
            }
            dr.Close();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            DataTable tblDivision = new DataTable();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT divicode,division FROM HR.Division ORDER BY division";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblDivision);
            }
            ddlDivision.DataSource = tblDivision;
            ddlDivision.DataValueField = "divicode";
            ddlDivision.DataTextField = "division";
            ddlDivision.DataBind();

            BindItems();
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItems();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strFiscalYear = clsSpeedo.GetCurrentFiscalYear();
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            cmd.CommandText = "UPDATE Finance.QuarterBudget SET tbudget1=@tbudget1,rbudget1=@rbudget1,tbudget2=@tbudget2,rbudget2=@rbudget2,tbudget3=@tbudget3,rbudget3=@rbudget3,tbudget4=@tbudget4,rbudget4=@rbudget4 WHERE accbcode='OSB' AND rccode=@rccode AND fiscyear=@fiscyear";
            cmd.Parameters.Add("@tbudget1", SqlDbType.Float);
            cmd.Parameters.Add("@rbudget1", SqlDbType.Float);
            cmd.Parameters.Add("@tbudget2", SqlDbType.Float);
            cmd.Parameters.Add("@rbudget2", SqlDbType.Float);
            cmd.Parameters.Add("@tbudget3", SqlDbType.Float);
            cmd.Parameters.Add("@rbudget3", SqlDbType.Float);
            cmd.Parameters.Add("@tbudget4", SqlDbType.Float);
            cmd.Parameters.Add("@rbudget4", SqlDbType.Float);
            cmd.Parameters.Add("@rccode", SqlDbType.Char, 3);
            cmd.Parameters.Add("@fiscyear", SqlDbType.Char, 9);

            foreach (DataGridItem ditm in dgRC.Items)
            {
                HiddenField pmhdnRcCode = (HiddenField)ditm.FindControl("phdnRcCode");
                TextBox pmtxttbudget1 = (TextBox)ditm.FindControl("ptxttbudget1");
                TextBox pmtxtrbudget1 = (TextBox)ditm.FindControl("ptxtrbudget1");
                TextBox pmtxttbudget2 = (TextBox)ditm.FindControl("ptxttbudget2");
                TextBox pmtxtrbudget2 = (TextBox)ditm.FindControl("ptxtrbudget2");
                TextBox pmtxttbudget3 = (TextBox)ditm.FindControl("ptxttbudget3");
                TextBox pmtxtrbudget3 = (TextBox)ditm.FindControl("ptxtrbudget3");
                TextBox pmtxttbudget4 = (TextBox)ditm.FindControl("ptxttbudget4");
                TextBox pmtxtrbudget4 = (TextBox)ditm.FindControl("ptxtrbudget4");
                cmd.Parameters["@tbudget1"].Value = pmtxttbudget1.Text;
                cmd.Parameters["@rbudget1"].Value = pmtxtrbudget1.Text;
                cmd.Parameters["@tbudget2"].Value = pmtxttbudget2.Text;
                cmd.Parameters["@rbudget2"].Value = pmtxtrbudget2.Text;
                cmd.Parameters["@tbudget3"].Value = pmtxttbudget3.Text;
                cmd.Parameters["@rbudget3"].Value = pmtxtrbudget3.Text;
                cmd.Parameters["@tbudget4"].Value = pmtxttbudget4.Text;
                cmd.Parameters["@rbudget4"].Value = pmtxtrbudget4.Text;
                cmd.Parameters["@rccode"].Value = pmhdnRcCode.Value;
                cmd.Parameters["@fiscyear"].Value = strFiscalYear;
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequDepBudget.aspx");
    }

}
