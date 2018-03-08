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

public partial class CIS_Requisition_RequMenu : System.Web.UI.Page
{

    protected void LoadSuppliesCustodian()
    {
        string strWrite;
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 10 totcost,requcode,userrem,datereq,username,status FROM CIS.Requisition WHERE suppcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND (status='A' OR status='P') ORDER BY datereq DESC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = "<tr>" +
                                                                "<td class='GridRows'>" +
                                                                    "<a href='RequDetailsSC.aspx?requcode=" + dr["requcode"] + "'><img src='../../Support/approval.png' alt='' /></a>" +
                                                                "</td>" +
                                                                "<td class='GridRows'>" +
                                                                    "<a href='RequDetailsSC.aspx?requcode=" + dr["requcode"] + "' style='font-size:small'>" + dr["userrem"] + "</a> " +
                                                                    "by <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
                                                                    "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
                                                                    "Cost: P " + Convert.ToDouble(dr["totcost"]).ToString("###,##0.00") +
                                                                "</td>" +
                                                                "<td class='GridRows'>For processing</td>" +
                                                            "</tr>";
                Response.Write(strWrite);
                intCtr++;
            }
            dr.Close();
        }
        if (intCtr == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
    }

    protected void LoadDivisionHeadMenu()
    {
        string strWrite;
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 10 requcode,totcost,userrem,datereq,status,username FROM CIS.Requisition WHERE headcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND (sprvstat='A' OR sprvstat='X') AND headstat='F' ORDER BY datereq DESC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = "<tr>" +
                                                                "<td class='GridRows'>" +
                                                                    "<a href='RequDetailsDH.aspx?requcode=" + dr["requcode"] + "'><img src='../../Support/approval.png' alt='' /></a>" +
                                                                "</td>" +
                                                                "<td class='GridRows'>" +
                                                                    "<a href='RequDetailsDH.aspx?requcode=" + dr["requcode"] + "' style='font-size:small;'>" + dr["userrem"] + "</a> by <a href='../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
                                                                    "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
                                                                    "Cost: P " + Convert.ToDouble(dr["totcost"]).ToString("###,##0.00") +
                                                                "</td>" +
                                                                "<td class='GridRows'>Awaiting for your approval</td>" +
                                                            "</tr>";
                Response.Write(strWrite);
                intCtr++;
            }
            dr.Close();
        }
        if (intCtr == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
    }

    protected void LoadGroupHeadMenu()
    {
        string strWrite;
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 10 requcode,totcost,userrem,datereq,status,username FROM CIS.Requisition WHERE sprvcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND sprvstat='F' ORDER BY datereq DESC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = "<tr>" +
                                                                "<td class='GridRows'>" +
                                                                    "<a href='RequDetailsGH.aspx?requcode=" + dr["requcode"] + "'><img src='../../Support/approval.png' alt='' /></a>" +
                                                                "</td>" +
                                                                "<td class='GridRows'>" +
                                                                    "<a href='RequDetailsGH.aspx?requcode=" + dr["requcode"] + "' style='font-size:small;'>" + dr["userrem"] + "</a><br>" +
                                                                    "Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
                                                                    "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
                                                                    "Cost: P " + Convert.ToDouble(dr["totcost"]).ToString("###,##0.00") +
                                                                "</td>" +
                                                                "<td class='GridRows'>Awaiting for your approval</td>" +
                                                            "</tr>";
                Response.Write(strWrite);
                intCtr++;
            }
            dr.Close();
        }
        if (intCtr == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
    }

    protected void LoadRequisition()
    {
        string strWrite = "";
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 9 requcode,datereq,userrem,sprvcode,sprvstat,headcode,headstat,suppcode,suppstat,status,username,totcost FROM CIS.Requisition WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' ORDER BY datereq DESC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = strWrite + "<tr>" +
                                          "<td class='GridRows'>" +
                                                                                                             "<a href='RequDetails.aspx?requcode=" + dr["requcode"] + "'><img src='../../Support/" + clsRequisition.GetRequestStatusIcon(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["suppcode"].ToString(), dr["suppstat"].ToString()) + "' alt='' /></a>" +
                                                                                                            "</td>" +
                                                                                                            "<td class='GridRows'>" +
                                                                                                             "<a href='RequDetails.aspx?requcode=" + dr["requcode"] + "' style='font-size:small;'>" + dr["userrem"] + "</a><br>" +
                                                                                                                "Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
                                                                                                                "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd,yyyy") + "<br>" +
                                                                                                                "Total Cost: P " + Convert.ToDouble(dr["totcost"]).ToString("###,##0.00") +
                                                                                                            "</td>" +
                                                                                                            "<td class='GridRows'>" + clsRequisition.GetRequestStatusRemarks(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["suppcode"].ToString(), dr["suppstat"].ToString()) + "</td>" +
                                                                                                        "</tr>";
                intCtr++;
            }
            dr.Close();
        }
        Response.Write(strWrite);
        if (intCtr == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='3' class='GridRows'>[" + intCtr + " records found]</td></tr>");
    }

    protected void LoadBudgetDetails()
    {
        try
        {

            string strWrite;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                string strQuarter = clsSpeedo.GetCurrentFiscalQuarter();
                if (strQuarter == "1")
                    cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.rbudget1 AS rbudget FROM Hr.Rc INNER JOIN Finance.QuarterBudget ON Hr.Rc.rccode = Finance.QuarterBudget.rccode WHERE Finance.QuarterBudget.fiscyear='" + clsSpeedo.GetCurrentFiscalYear() + "' AND Finance.QuarterBudget.rccode IN (SELECT rccode FROM HR.Employees WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "') ORDER BY Hr.Rc.rcname";
                else if (strQuarter == "2")
                    cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.rbudget2 AS rbudget FROM Hr.Rc INNER JOIN Finance.QuarterBudget ON Hr.Rc.rccode = Finance.QuarterBudget.rccode WHERE Finance.QuarterBudget.fiscyear='" + clsSpeedo.GetCurrentFiscalYear() + "' AND Finance.QuarterBudget.rccode IN (SELECT rccode FROM HR.Employees WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "') ORDER BY Hr.Rc.rcname";
                else if (strQuarter == "3")
                    cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.rbudget3 AS rbudget FROM Hr.Rc INNER JOIN Finance.QuarterBudget ON Hr.Rc.rccode = Finance.QuarterBudget.rccode WHERE Finance.QuarterBudget.fiscyear='" + clsSpeedo.GetCurrentFiscalYear() + "' AND Finance.QuarterBudget.rccode IN (SELECT rccode FROM HR.Employees WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "') ORDER BY Hr.Rc.rcname";
                else if (strQuarter == "4")
                    cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.rbudget4 AS rbudget FROM Hr.Rc INNER JOIN Finance.QuarterBudget ON Hr.Rc.rccode = Finance.QuarterBudget.rccode WHERE Finance.QuarterBudget.fiscyear='" + clsSpeedo.GetCurrentFiscalYear() + "' AND Finance.QuarterBudget.rccode IN (SELECT rccode FROM HR.Employees WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "') ORDER BY Hr.Rc.rcname";

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strWrite = "<tr>" +
                                                                    "<td class='GridRows'>" +
                                                                        "<a href='#' style='font-size:small;'>" + dr["rcname"] + "</a><br>" +
                                                                    "</td>" +
                                                                    "<td class='GridRows' style='text-align:center;'>P " + Convert.ToDouble(dr["rbudget"].ToString()).ToString("###,##0.00") + "</td>" +
                                                                "</tr>";
                    Response.Write(strWrite);
                }
                dr.Close();
            }
        }

        catch
        {
            Response.Redirect("RequisitionBudgetProblem.aspx");
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            if (clsRequisition.GetUserType(Request.Cookies["Speedo"]["UserName"].ToString()) == clsRequisition.RequisitionUserType.DivisionHead)
            {
                DataTable tblTBudget = new DataTable();
                DataTable tblUBudget = new DataTable();

                string strQuarter = clsSpeedo.GetCurrentFiscalQuarter();
                string strFiscalYear = clsSpeedo.GetCurrentFiscalYear();

                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    if (strQuarter == "1")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget1 AS tbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    else if (strQuarter == "2")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget2 AS tbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    else if (strQuarter == "3")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget3 AS tbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    else if (strQuarter == "4")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget4 AS tbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cn.Open();
                    da.Fill(tblTBudget);

                    if (strQuarter == "1")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget1 - Finance.QuarterBudget.rbudget1 AS rbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    else if (strQuarter == "2")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget2 - Finance.QuarterBudget.rbudget2 AS rbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    else if (strQuarter == "3")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget3 - Finance.QuarterBudget.rbudget3 AS rbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    else if (strQuarter == "4")
                        cmd.CommandText = "SELECT Hr.Rc.rcname,Finance.QuarterBudget.tbudget4 - Finance.QuarterBudget.rbudget4 AS rbudget FROM Finance.QuarterBudget INNER JOIN Hr.Rc ON Finance.QuarterBudget.rccode = Hr.Rc.rccode WHERE accbcode='OSB' AND fiscyear='" + strFiscalYear + "' AND Finance.QuarterBudget.rccode IN (SELECT DISTINCT rccode FROM CIS.RequisitionApprover WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head') ORDER BY rcname";
                    da.SelectCommand = cmd;
                    da.Fill(tblUBudget);
                }

                chaBudCon.ChartTitle.Text = "Office Supplies Budget Consumption FY: " + strFiscalYear + " Quarter: " + strQuarter;

                WebChart.ColumnChart ccTBudget = new WebChart.ColumnChart();
                WebChart.ColumnChart ccUBudget = new WebChart.ColumnChart();

                ccTBudget.Shadow.Visible = true;
                ccTBudget.MaxColumnWidth = 15;
                ccTBudget.Fill.Color = System.Drawing.Color.FromArgb(90, System.Drawing.Color.Blue);
                ccTBudget.DataLabels.Visible = true;
                ccTBudget.DataLabels.Background.Color = System.Drawing.Color.White;
                ccTBudget.Legend = "Total Budget Allotted";
                ccTBudget.DataSource = tblTBudget.DefaultView;
                ccTBudget.DataXValueField = "rcname";
                ccTBudget.DataYValueField = "tbudget";
                ccTBudget.DataBind();

                ccUBudget.Shadow.Visible = true;
                ccUBudget.MaxColumnWidth = 15;
                ccUBudget.Fill.Color = System.Drawing.Color.FromArgb(90, System.Drawing.Color.Red);
                ccUBudget.DataLabels.Visible = true;
                ccUBudget.DataLabels.Background.Color = System.Drawing.Color.White;
                ccUBudget.Legend = "Used Budget";
                ccUBudget.DataSource = tblUBudget.DefaultView;
                ccUBudget.DataXValueField = "rcname";
                ccUBudget.DataYValueField = "rbudget";
                ccUBudget.DataBind();

                chaBudCon.Charts.Add(ccTBudget);
                chaBudCon.Charts.Add(ccUBudget);
                chaBudCon.RedrawChart();
            }
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequNew.aspx");
    }

}
