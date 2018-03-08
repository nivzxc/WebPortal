﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_HRMS_Reports_rptExcessiveTardinessReport : System.Web.UI.Page
{

    //private DataTable GetMonths()
    //{
    //    DataTable months = new DataTable();

    //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    //    {
    //        SqlCommand cmd = cn.CreateCommand();
    //        cmd.CommandText = "SELECT tspcode, tspfrom, tspto FROM HR.TimeSheetPeriod WHERE YEAR(tspfrom) = @TargetYear AND tspmode='M' ORDER BY tspcode DESC";
    //        cmd.Parameters.Add(new SqlParameter("@TargetYear", DateTime.Now.Year.ToString()));
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(months);
    //    }

    //    return months;
    //}

    private DataTable GetMonths()
    {
        DataTable months = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            string fiscalYear = "";
            DateTime lastProcess = DateTime.Now;
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT fsclyear FROM Speedo.FiscalYear WHERE fsclyear=@fsclyear";
            cmd.Parameters.Add(new SqlParameter("@fsclyear", ddlFiscalYear.SelectedValue.ToString()));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                fiscalYear = dr["fsclyear"].ToString();
            }
            dr.Close();
            cmd.Parameters.Clear();

            cmd.CommandText = "SELECT TOP 1 tspto FROM HR.TimeSheetPeriod WHERE tspmode='M' ORDER BY tspcode DESC";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lastProcess = clsValidator.CheckDate(dr["tspto"].ToString());
            }
            dr.Close();

            //Update by CHarlie Bachiller
            cmd.CommandText = "SELECT tspfrom = datefrom, tspto = dateto FROM Speedo.FiscalYearMonth WHERE dateto <= @TargetYear AND fsclyear=@fsclyear ORDER BY fsmncode DESC";
            //cmd.CommandText = "SELECT tspfrom = datefrom, tspto = dateto FROM Speedo.FiscalYearMonth WHERE fsclyear=@fsclyear ORDER BY fsmncode DESC";

            cmd.Parameters.Add(new SqlParameter("@TargetYear", lastProcess));
            cmd.Parameters.Add(new SqlParameter("@fsclyear", fiscalYear));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(months);
        }

        return months;
    }

    private DataTable GetDivision()
    {
        DataTable division = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT divicode, division FROM HR.Division ORDER BY division";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(division);
        }

        return division;
    }

    private DataTable GetEmployees(string divisionCode)
    {
        DataTable employees = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username, lastname + ', ' + firname AS EmployeeName FROM HR.Employees WHERE pstatus='1' AND divicode=@divicode AND username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') ORDER BY lastname,firname";
            cmd.Parameters.Add(new SqlParameter("@divicode", divisionCode));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(employees);
        }

        return employees;
    }

    private int GetEmployeeCount(string username, DateTime dateFrom, DateTime dateTo)
    {
        int count = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE lateunit > 0 AND username=@username AND (focsdate BETWEEN @DateFrom AND @DateTo)";
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cmd.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
            cmd.Parameters.Add(new SqlParameter("@DateTo", dateTo));
            cn.Open();
            int.TryParse(cmd.ExecuteScalar().ToSafeString(), out count);
        }
        return count;
    }

    private float GetEmployeeSum(string username, DateTime dateFrom, DateTime dateTo)
    {
        float sum = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lateunit) FROM HR.Timesheet WHERE lateunit > 0 AND username=@username AND (focsdate BETWEEN @DateFrom AND @DateTo)";
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cmd.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
            cmd.Parameters.Add(new SqlParameter("@DateTo", dateTo));
            //cmd.Parameters.Add(new SqlParameter("@DateTo", dateTo.AddMonths(-1)));
            cn.Open();
            float.TryParse(cmd.ExecuteScalar().ToSafeString(), out sum);
        }
        return sum * 60;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int totalEmployee = 0;
        string reportString = "";
        string reportBuffer = "";
        string reportBufferDivision = "";
        DataTable monthsTable = this.GetMonths();
        int monthsTableCount = monthsTable.Rows.Count;
        DataTable divisionTable = this.GetDivision();
        int columnCount = 5 + (monthsTable.Rows.Count * 2);
        int overallCount = 0;
        int[] overallMonthCount = new int[monthsTableCount];
        float overallSum = 0;
        float[] overallMonthSum = new float[monthsTableCount];

        reportString += "<div class='GridBorder'>" +
                        "<table width='100%' cellpadding='5'>" +
                            "<tr><td colspan='" + columnCount.ToString() + "' class='GridText' style='text-align:center;'><b>Excessive Tardiness Summary</b></td></tr>" +
                            "<tr>" +
                                "<td rowspan='2' class='GridColumns'>&nbsp;</td>" +
                                "<td rowspan='2' class='GridColumns'><b>Division</b></td>" +
                                "<td rowspan='2' class='GridColumns'><b>Employee</b></td>";

        foreach (DataRow row in monthsTable.Rows)
        {
            string targetMonth = clsValidator.CheckDate(row["tspfrom"].ToString()).ToString("MMMM");
            reportString += "<td colspan='2' class='GridColumns'><b>" + targetMonth + "</b></td>";
        }

        reportString += "<td colspan='2' class='GridColumns'><b>Total</b></td></tr><tr>";

        foreach (DataRow row in monthsTable.Rows)
        {
            reportString += "<td class='GridColumns'><b>Count</b></td>" +
                            "<td class='GridColumns'><b>Min</b></td>";
        }

        reportString += "<td class='GridColumns'><b>Count</b></td>" +
                        "<td class='GridColumns'><b>Min</b></td>" +
                        "</tr>";

        foreach (DataRow rowD in divisionTable.Rows)
        {
            int counter = 1;
            int divisionTotalCount = 0;
            int[] divisionTotalMonthCount = new int[monthsTableCount];
            float divisionTotalSum = 0;
            float[] divisionTotalMonthSum = new float[monthsTableCount];

            DataTable divisionEmployee = this.GetEmployees(rowD["divicode"].ToString());

            reportBufferDivision = "";
            reportBufferDivision += "<tr>" +
                                    "<td colspan='3' class='GridRows'><b>" + rowD["division"].ToString() + "</b></td>";
            for (int i = 0; i < monthsTableCount; ++i)
            {
                string rowColor = (i == 0 ? "GridRowsRed" : "GridRows");
                reportBufferDivision += "<td class='" + rowColor + "'>&nbsp;</td>" +
                                        "<td class='" + rowColor + "'>&nbsp;</td>";
            }
            reportBufferDivision += "<td class='GridRows'>&nbsp;</td>" +
                                    "<td class='GridRows'>&nbsp;</td>" +
                                    "</tr>";


            foreach (DataRow rowE in divisionEmployee.Rows)
            {
                bool isExcessive = false;
                int employeeTotalCount = 0;
                float employeeTotalSum = 0;
                reportBuffer = "";
                reportBuffer += "<td colspan='2' class='GridRows'>&nbsp;&nbsp;&nbsp;" + rowE["EmployeeName"].ToSafeString() + "</td>";

                int ctr = 0;
                foreach (DataRow rowM in monthsTable.Rows)
                {
                    string rowColor = (ctr == 0 ? "GridRowsRed" : "GridRows");
                    DateTime dateStart = clsValidator.CheckDate(rowM["tspfrom"].ToString());
                    DateTime dateEnd = clsValidator.CheckDate(rowM["tspto"].ToString());
                    dateEnd = dateEnd.Date.AddDays(1).AddSeconds(-1);
                    int employeeCountTemp = this.GetEmployeeCount(rowE["username"].ToString(), dateStart, dateEnd);
                    float employeeSumTemp = this.GetEmployeeSum(rowE["username"].ToString(), dateStart, dateEnd);
                    int employeeCount = 0;
                    float employeeSum = 0;

                    if (employeeCountTemp > 4 || employeeSumTemp > 60)
                    {
                        employeeCount = employeeCountTemp;
                        employeeSum = employeeSumTemp;
                    }

                    reportBuffer += "<td class='" + rowColor + "' style='text-align:center;'>" + (employeeCount == 0 ? "-" : employeeCount.ToString()) + "</td>" +
                                    "<td class='" + rowColor + "' style='text-align:center;'>" + (employeeSum == 0 ? "-" : employeeSum.ToString("##0.00")) + "</td>";

                    if (!isExcessive)
                    {
                        isExcessive = employeeCount > 4 || employeeSum > 60;
                    }

                    employeeTotalCount += employeeCount;
                    employeeTotalSum += employeeSum;
                    divisionTotalMonthCount[ctr] = divisionTotalMonthCount[ctr] + employeeCount;
                    divisionTotalMonthSum[ctr] = divisionTotalMonthSum[ctr] + employeeSum;
                    divisionTotalCount = divisionTotalCount + employeeCount;
                    divisionTotalSum = divisionTotalSum + employeeSum;
                    overallMonthCount[ctr] = overallMonthCount[ctr] + employeeCount;
                    overallMonthSum[ctr] = overallMonthSum[ctr] + employeeSum;
                    overallCount = overallCount + employeeCount;
                    overallSum = overallSum + employeeSum;

                    if (ctr == 0 && employeeCount > 0)
                    {
                        totalEmployee++;
                    }

                    ctr++;
                }

                reportBuffer += "<td class='GridRows' style='text-align:center;'>" + (employeeTotalCount == 0 ? "-" : employeeTotalCount.ToString()) + "</td>" +
                                "<td class='GridRows' style='text-align:center;'>" + (employeeTotalSum == 0 ? "-" : employeeTotalSum.ToString("##0.00")) + "</td>" +
                                "</tr>";

                if (isExcessive)
                {
                    reportBufferDivision += "<tr><td class='GridRows' style='text-align:center;'>" + counter.ToString("00") + "</td>" + reportBuffer;
                    counter++;
                }
            }

            reportBufferDivision += "<tr>" +
                                    "<td colspan='3' class='GridRowsGreen'><b>" + rowD["division"].ToSafeString() + " Total</b></td>";
            for (int i = 0; i < monthsTableCount; ++i)
            {
                string rowColor = (i == 0 ? "GridRowsRed" : "GridRowsGreen");
                reportBufferDivision += "<td class='" + rowColor + "' style='text-align:center;'><b>" + (divisionTotalMonthCount[i] == 0 ? "-" : divisionTotalMonthCount[i].ToString()) + "</b></td>" +
                                        "<td class='" + rowColor + "' style='text-align:center;'><b>" + (divisionTotalMonthSum[i] == 0 ? "-" : divisionTotalMonthSum[i].ToString("##0.00")) + "</b></td>";
            }
            reportBufferDivision += "<td class='GridRowsGreen' style='text-align:center;'><b>" + (divisionTotalCount == 0 ? "-" : divisionTotalCount.ToString()) + "</b></td>" +
                                    "<td class='GridRowsGreen' style='text-align:center;'><b>" + (divisionTotalSum == 0 ? "-" : divisionTotalSum.ToString("##0.00")) + "</b></td>" +
                                    "</tr>";

            if (divisionTotalCount > 0)
            {
                reportString += reportBufferDivision;
            }
        }

        reportString += "<tr>" +
                        "<td colspan='3' class='GridRowsGreen'><b>Overall Total (" + totalEmployee.ToString() + ")</b></td>";
        for (int i = 0; i < monthsTableCount; ++i)
        {
            string rowColor = (i == 0 ? "GridRowsRed" : "GridRowsGreen");
            reportString += "<td class='" + rowColor + "' style='text-align:center;'><b>" + (overallMonthCount[i] == 0 ? "-" : overallMonthCount[i].ToString()) + "</b></td>" +
                            "<td class='" + rowColor + "' style='text-align:center;'><b>" + (overallMonthSum[i] == 0 ? "-" : overallMonthSum[i].ToString("##0.00")) + "</b></td>";
        }
        reportString += "<td class='GridRowsGreen' style='text-align:center;'><b>" + (overallCount == 0 ? "-" : overallCount.ToString()) + "</b></td>" +
                        "<td class='GridRowsGreen' style='text-align:center;'><b>" + (overallSum == 0 ? "-" : overallSum.ToString("##0.00")) + "</b></td>" +
                        "</tr>";

        reportString += "</table></div>";
        litReport.Text = reportString;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HR/HRMS/HRMS.aspx");
    }
}