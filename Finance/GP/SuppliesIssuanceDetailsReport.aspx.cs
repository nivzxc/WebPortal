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
using WebChart.Design;
using WebChart;
using System.Drawing;
using HRMS;

public partial class Finance_GP_SuppliesIssuanceDetailsReport : System.Web.UI.Page
{

	protected void LoadRecords()
	{
		double dblRCTotal = 0;
		double dblTotal = 0;
		double dblTemp = 0;

		int intCtr = 0;
		double[] dblMonthTotal = new double[12];
		string[] strMonth = new string[] { "April", "May", "June", "July", "August", "September", "October", "November", "December", "January", "February", "March" };
		DateTime[] dteFrom = new DateTime[] { Convert.ToDateTime("4/1/2007"), Convert.ToDateTime("5/1/2007"), Convert.ToDateTime("6/1/2007"), Convert.ToDateTime("7/1/2007"), Convert.ToDateTime("8/1/2007"), Convert.ToDateTime("9/1/2007"), Convert.ToDateTime("10/1/2007"), Convert.ToDateTime("11/1/2007"), Convert.ToDateTime("12/1/2007"), Convert.ToDateTime("1/1/2008"), Convert.ToDateTime("2/1/2008"), Convert.ToDateTime("3/1/2008") };
		DateTime[] dteTo = new DateTime[] { Convert.ToDateTime("4/30/2007"), Convert.ToDateTime("5/31/2007"), Convert.ToDateTime("6/30/2007"), Convert.ToDateTime("7/31/2007"), Convert.ToDateTime("8/31/2007"), Convert.ToDateTime("9/30/2007"), Convert.ToDateTime("10/31/2007"), Convert.ToDateTime("11/30/2007"), Convert.ToDateTime("12/31/2007"), Convert.ToDateTime("1/31/2008"), Convert.ToDateTime("2/29/2008"), Convert.ToDateTime("3/31/2008") };

		string strWrite = "";
		string strAltRowClass = "GridRows2";

		DataTable tblRC = new DataTable();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["GreatPlains"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cn.Open();
			SqlDataReader dr;

			cmd.CommandText = "SELECT custnmbr,custname FROM rm00101 WHERE rm00101.custclas='HQ DEPARTMENT' AND rm00101.custnmbr IN ('" + clsRC.GetSQLInClauseGP(Request.QueryString["divicode"]) + "') ORDER BY custname";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(tblRC);

			foreach (DataRow drwRC in tblRC.Rows)
			{
				dblTemp = 0;
				dblRCTotal = 0;

				if (strAltRowClass == "GridRows2")
					strAltRowClass = "GridRows";
				else if (strAltRowClass == "GridRows")
					strAltRowClass = "GridRows2";

				strWrite = strWrite + "<tr>" +
																											"<td class='" + strAltRowClass + "'>" + drwRC["custname"] + "</td>";
				for (intCtr = 0; intCtr <= 11; intCtr++)
				{
					cmd.CommandText = "SELECT SUM(sop30200.docamnt) AS pTotal FROM sop30200 INNER JOIN rm00101 ON rm00101.custnmbr = sop30200.custnmbr WHERE rm00101.custclas='HQ DEPARTMENT' AND (left(sop30200.sopnumbe,3)='OSD') AND rm00101.custnmbr='" + drwRC["custnmbr"] + "' AND sop30200.docdate BETWEEN '" + dteFrom[intCtr] + "' AND '" + dteTo[intCtr] + "'";
					dr = cmd.ExecuteReader();
					if (dr.Read())
						dblTemp = (Convert.IsDBNull(dr["pTotal"].ToString()) || dr["pTotal"].ToString() == "" ? 0 : Convert.ToDouble(dr["pTotal"].ToString()));
					dr.Close();
					dblRCTotal = dblRCTotal + dblTemp;
					dblMonthTotal[intCtr] = dblMonthTotal[intCtr] + dblTemp;
					strWrite = strWrite + "<td class='" + strAltRowClass + "' style='text-align:right;'>" + (dblTemp == 0 ? "-" : dblTemp.ToString("#,###,##0.00")) + "</td>";
				}
				strWrite = strWrite + "<td class='" + strAltRowClass + "' style='text-align:right;'>" + (dblRCTotal == 0 ? "-" : dblRCTotal.ToString("#,###,##0.00")) + "</td></tr>";
				dblTotal = dblTotal + dblRCTotal;
			}
		}
		strWrite = strWrite + "<tr><td class='GridRows2'><b>Total</b></td>";
		for (intCtr = 0; intCtr <= 11; intCtr++)
			strWrite = strWrite + "<td class='GridRows2' style='text-align:right;'><b>" + dblMonthTotal[intCtr].ToString("#,###,##0.00") + "</b></td>";
		strWrite = strWrite + "<td class='GridRows2' style='text-align:right;'><b>" + dblTotal.ToString("#,###,##0.00") + "</b></td></tr>";

		Response.Write(strWrite);
	}

	protected void Page_Load(object sender, EventArgs e)
 {
		if (!Page.IsPostBack)
		{
			SmoothLineChart lcTBudget;
			DataRow drownew;
			DataTable tblOSIReport = new DataTable();
			tblOSIReport.Columns.Add("month");
			tblOSIReport.Columns.Add("tamount");
			chaBudCon.ChartTitle.Text = Request.QueryString["division"] + " Office Supplies Budget Consumption FY: 2007-2008 [As of " + DateTime.Now.ToString("MMMM dd, yyyy") + "]";

			double dblHighest = 0;
			int intCtr = 0;
			int intDiviCtr = 0;
			double[] dblTotal = new double[12];
			double[] dblMonthTotal = new double[12];
			string[] strMonth = new string[] { "April", "May", "June", "July", "August", "September", "October", "November", "December", "January", "February", "March" };
			DateTime[] dteFrom = new DateTime[] { Convert.ToDateTime("4/1/2007"), Convert.ToDateTime("5/1/2007"), Convert.ToDateTime("6/1/2007"), Convert.ToDateTime("7/1/2007"), Convert.ToDateTime("8/1/2007"), Convert.ToDateTime("9/1/2007"), Convert.ToDateTime("10/1/2007"), Convert.ToDateTime("11/1/2007"), Convert.ToDateTime("12/1/2007"), Convert.ToDateTime("1/1/2008"), Convert.ToDateTime("2/1/2008"), Convert.ToDateTime("3/1/2008") };
			DateTime[] dteTo = new DateTime[] { Convert.ToDateTime("4/30/2007"), Convert.ToDateTime("5/31/2007"), Convert.ToDateTime("6/30/2007"), Convert.ToDateTime("7/31/2007"), Convert.ToDateTime("8/31/2007"), Convert.ToDateTime("9/30/2007"), Convert.ToDateTime("10/31/2007"), Convert.ToDateTime("11/30/2007"), Convert.ToDateTime("12/31/2007"), Convert.ToDateTime("1/31/2008"), Convert.ToDateTime("2/29/2008"), Convert.ToDateTime("3/31/2008") };
			Color[] clrDivision = new Color[] { Color.Blue, Color.Yellow, Color.Purple, Color.DarkGreen, Color.Orange, Color.DarkBlue, Color.Brown, Color.Pink };

			DataTable tblRC = new DataTable();
			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["GreatPlains"].ToString()))
			{
				SqlCommand cmd = cn.CreateCommand();
				cn.Open();
				SqlDataReader dr;

				cmd.CommandText = "SELECT custnmbr,custname FROM rm00101 WHERE rm00101.custclas='HQ DEPARTMENT' AND rm00101.custnmbr IN ('" + clsRC.GetSQLInClauseGP(Request.QueryString["divicode"]) + "') ORDER BY custname";
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(tblRC);

				foreach (DataRow drow in tblRC.Rows)
				{
					for (intCtr = 0; intCtr <= 11; intCtr++)
						dblMonthTotal[intCtr] = 0;

					for (intCtr = 0; intCtr <= 11; intCtr++)
					{
						cmd.CommandText = "SELECT SUM(sop30200.docamnt) AS pTotal FROM sop30200 INNER JOIN rm00101 ON rm00101.custnmbr = sop30200.custnmbr WHERE rm00101.custclas='HQ DEPARTMENT' AND (left(sop30200.sopnumbe,3)='OSD') AND rm00101.custnmbr='" + drow["custnmbr"] + "' AND sop30200.docdate BETWEEN '" + dteFrom[intCtr] + "' AND '" + dteTo[intCtr] + "'";
						dr = cmd.ExecuteReader();
						if (dr.Read())
						{
							dblMonthTotal[intCtr] = dblMonthTotal[intCtr] + (Convert.IsDBNull(dr["pTotal"].ToString()) || dr["pTotal"].ToString() == "" ? 0 : Convert.ToDouble(dr["pTotal"].ToString()));
							dblTotal[intCtr] = dblTotal[intCtr] + dblMonthTotal[intCtr];
						}
						dr.Close();
					}

					for (intCtr = 0; intCtr <= 11; intCtr++)
					{
						drownew = tblOSIReport.NewRow();
						drownew["month"] = strMonth[intCtr];
						drownew["tamount"] = dblMonthTotal[intCtr];
						tblOSIReport.Rows.Add(drownew);
					}

					lcTBudget = new SmoothLineChart();
					lcTBudget.Line.Width = 2;
					lcTBudget.Line.Color = clrDivision[intDiviCtr];
					lcTBudget.Legend = drow["custname"].ToString();
					lcTBudget.DataSource = tblOSIReport.DefaultView;
					lcTBudget.DataXValueField = "month";
					lcTBudget.DataYValueField = "tamount";
					lcTBudget.DataBind();
					chaBudCon.Charts.Add(lcTBudget);

					tblOSIReport.Rows.Clear();
					intDiviCtr++;
				}

				for (intCtr = 0; intCtr <= 11; intCtr++)
				{
					drownew = tblOSIReport.NewRow();
					drownew["month"] = strMonth[intCtr];
					drownew["tamount"] = dblTotal[intCtr];
					tblOSIReport.Rows.Add(drownew);
					if (dblHighest == 0)
						dblHighest = dblTotal[intCtr];
					else
						if (dblTotal[intCtr] > dblHighest)
							dblHighest = dblTotal[intCtr];
				}

				lcTBudget = new SmoothLineChart();
				lcTBudget.Line.Width = 2;
				lcTBudget.Line.Color = Color.Red;
				lcTBudget.DataLabels.Visible = true;
				lcTBudget.DataLabels.Background.Color = System.Drawing.Color.White;
				lcTBudget.Legend = Request.QueryString["division"];
				lcTBudget.DataSource = tblOSIReport.DefaultView;
				lcTBudget.DataXValueField = "month";
				lcTBudget.DataYValueField = "tamount";
				lcTBudget.DataBind();
				chaBudCon.Charts.Add(lcTBudget);
			}

			chaBudCon.YCustomEnd = Convert.ToInt32(dblHighest) + 1000;
			chaBudCon.YValuesInterval = Convert.ToInt32((dblHighest + 1000) / 10);
			chaBudCon.RedrawChart();
		}
 }

}
