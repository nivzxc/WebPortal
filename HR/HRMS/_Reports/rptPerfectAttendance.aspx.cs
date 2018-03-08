using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;

public partial class HR_HRMS_Reports_rptPerfectAttendance : System.Web.UI.Page
{

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!Page.IsPostBack)
    //    {
    //        LoadDdl();
    //    }
    //}


    ////protected void LoadDdl()
    ////{
    ////    ddlLevel.DataSource = clsPerfectAttendance.GetPerfectAttendanceLevel();
    ////    ddlLevel.DataValueField = "pvalue";
    ////    ddlLevel.DataTextField = "ptext";
    ////    ddlLevel.DataBind();
    ////}

    //private void LoadReport()
    //{
    //    string checkFlag = "<img src='../../../support/checkHR.png' alt='' />";
    //    string uncheckFlag = "-";
    //    string reportBuffer = "";
    //    string reportBufferDivision = "";
    //    string reportString = "";
    //    DataTable monthsTable = clsPerfectAttendance.GetMonths();
    //    int monthsTableCount = monthsTable.Rows.Count;
    //    DataTable divisionTable = clsPerfectAttendance.GetDivision();
    //    int columnCount = 4 + monthsTable.Rows.Count;
    //    int overallAnnual = 0;
    //    int[] overallMonth = new int[monthsTableCount];

    //    reportString += "<div class='GridBorder'>" +
    //                    "<table width='100%' cellpadding='5'>" +
    //                        "<tr><td colspan='" + columnCount.ToString() + "' class='GridText' style='text-align:center;'><b>" + ddlLevel.SelectedItem.ToString() +  "</b></td></tr>" +
    //                        "<tr>" +
    //                                    "<td class='GridColumns'>&nbsp;</td>" +
    //                            "<td class='GridColumns'><b>Division</b></td>" +
    //                            "<td class='GridColumns'><b>Employee</b></td>";

    //    foreach (DataRow row in monthsTable.Rows)
    //    {
    //        string targetMonth = clsValidator.CheckDate(row["tspfrom"].ToString()).ToString("MMMM");
    //        reportString += "<td class='GridColumns'><b>" + targetMonth + "</b></td>";
    //    }

    //    reportString += "<td class='GridColumns'><b>Running for Annual</b></td></tr>";

    //    foreach (DataRow rowD in divisionTable.Rows)
    //    {
    //        int counter = 1;
    //        int divisionAnnual = 0;
    //        int[] divisionTotalMonth = new int[monthsTableCount];
    //        int divisionTotal = 0;
    //        string strFiscalYear = clsPerfectAttendance.GetActiveFiscalYear();
    //        string strLevelCode = ddlLevel.SelectedValue.ToString();
    //        DataTable divisionEmployee = clsPerfectAttendance.GetEmployees(rowD["divicode"].ToString(), strFiscalYear, strLevelCode);
    //        reportBufferDivision = "";
    //        reportBufferDivision += "<tr><td colspan='3' class='GridRows'><b>" + rowD["division"].ToString() + "</b></td>";
    //        for (int i = 0; i < monthsTableCount; ++i)
    //        {
    //            string rowColor = (i == 0 ? "GridRowsRed" : "GridRows");
    //            reportBufferDivision += "<td class='" + rowColor + "'>&nbsp;</td>";
    //        }
    //        reportBufferDivision += "<td class='GridRows'>&nbsp;</td>" +
    //                                      "</tr>";

    //        foreach (DataRow rowE in divisionEmployee.Rows)
    //        {
    //            int employeeTotal = 0;
    //            bool isRunningForAnnual = true;
    //            reportBuffer = "";
    //            reportBuffer += "<td colspan='2' class='GridRows'>&nbsp;&nbsp;&nbsp;" + rowE["EmployeeName"].ToSafeString() + "</td>";

    //            int ctr = 0;
    //            foreach (DataRow rowM in monthsTable.Rows)
    //            {
    //                string rowColor = (ctr == 0 ? "GridRowsRed" : "GridRows");
    //                bool isPerfectAttendance = clsPerfectAttendance.IsPerfectAttendance(rowE["username"].ToString(), rowM["fsclyear"].ToString(), rowM["fsmncode"].ToString(), strLevelCode);

    //                reportBuffer += "<td class='" + rowColor + "' style='text-align:center;'>" + (isPerfectAttendance ? checkFlag : uncheckFlag) + "</td>";

    //                if (isPerfectAttendance)
    //                {
    //                    employeeTotal++;
    //                    divisionTotalMonth[ctr]++;
    //                    overallMonth[ctr]++;
    //                    divisionTotal++;
    //                }
    //                else
    //                {
    //                    isRunningForAnnual = false;
    //                }
    //                ctr++;
    //            }

    //            if (isRunningForAnnual)
    //            {
    //                divisionAnnual++;
    //                overallAnnual++;
    //            }

    //            reportBuffer += "<td class='GridRows' style='text-align:center;'>" + (isRunningForAnnual ? checkFlag : uncheckFlag) + "</td></tr>";

    //            if (employeeTotal > 0)
    //            {
    //                reportBufferDivision += "<tr><td class='GridRows' style='text-align:center;'>" + counter.ToString("00") + "</td>" + reportBuffer;
    //                counter++;
    //            }
    //        }

    //        reportBufferDivision += "<tr>" +
    //                                      "<td colspan='3' class='GridRowsGreen'><b>" + rowD["division"].ToSafeString() + " Total</b></td>";
    //        for (int i = 0; i < monthsTableCount; ++i)
    //        {
    //            string rowColor = (i == 0 ? "GridRowsRed" : "GridRowsGreen");
    //            reportBufferDivision += "<td class='" + rowColor + "' style='text-align:center;'><b>" + (divisionTotalMonth[i] == 0 ? "-" : divisionTotalMonth[i].ToString()) + "</b></td>";
    //        }
    //        reportBufferDivision += "<td class='GridRowsGreen' style='text-align:center;'><b>" + (divisionAnnual == 0 ? "-" : divisionAnnual.ToString()) + "</b></td></tr>";

    //        if (divisionTotal > 0)
    //        {
    //            reportString += reportBufferDivision;
    //        }
    //    }

    //    reportString += "<tr>" +
    //                          "<td colspan='3' class='GridRowsGreen'><b>Overall Total</b></td>";
    //    for (int i = 0; i < monthsTableCount; ++i)
    //    {
    //        string rowColor = (i == 0 ? "GridRowsRed" : "GridRowsGreen");
    //        reportString += "<td class='" + rowColor + "' style='text-align:center;'><b>" + (overallMonth[i] == 0 ? "-" : overallMonth[i].ToString()) + "</b></td>";
    //    }
    //    reportString += "<td class='GridRowsGreen' style='text-align:center;'><b>" + (overallAnnual == 0 ? "-" : overallAnnual.ToString()) + "</b></td></tr>";

    //    reportString += "</table></div>";
    //    litReport.Text = reportString;
    //}

    //protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    //{
    //    LoadReport();
    //}

    private DataTable GetMonths()
    {
        DataTable months = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            string fiscalYear = "";
            DateTime lastProcess = DateTime.Now;
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT fsclyear FROM Speedo.FiscalYear WHERE penabled='1'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                fiscalYear = dr["fsclyear"].ToString();
            }
            dr.Close();

            cmd.CommandText = "SELECT TOP 1 tspto FROM HR.TimeSheetPeriod WHERE tspmode='M' ORDER BY tspcode DESC";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lastProcess = clsValidator.CheckDate(dr["tspto"].ToString());
            }
            dr.Close();

            cmd.CommandText = "SELECT tspfrom = datefrom, tspto = dateto FROM Speedo.FiscalYearMonth WHERE dateto <= @TargetYear AND fsclyear=@fsclyear ORDER BY fsmncode DESC";
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
            cmd.CommandText = "SELECT username, lastname + ', ' + firname AS EmployeeName, datestrt FROM HR.Employees WHERE divicode=@divicode AND username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND pstatus='1' ORDER BY lastname,firname";
            cmd.Parameters.Add(new SqlParameter("@divicode", divisionCode));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(employees);
        }
        return employees;
    }

    private bool IsPerfectAttendance(string username, DateTime dateStart, DateTime dateFrom, DateTime dateTo)
    {
        bool isPerfect = false;

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            //bool hasViolation = true;
            float tardiness = 1;
            float undertime = 1;
            float absence = 1;
            float leaveWithPay = 1;
            float leaveWithOutPay = 1;
            float workingHours = 1;

            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            //comment by Charlie Bachiller May 18, 2012
            //cmd.CommandText = "SELECT username FROM HR.Employees WHERE username = @username AND username IN (SELECT username FROM HR.LeaveCDL WHERE enabled='1') AND (username IN (SELECT username FROM HR.Offense WHERE enabled='1') OR username IN (SELECT username FROM HR.Leave3Days WHERE enabled='1'))";
            //cmd.Parameters.Add(new SqlParameter("@username", username));
            //cmd.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
            //cmd.Parameters.Add(new SqlParameter("@DateTo", dateTo));
            //cn.Open();
            //SqlDataReader dr = cmd.ExecuteReader();
            //hasViolation = dr.Read();
            //dr.Close();

            //if (!hasViolation)
            //{

            cmd.CommandText = "SELECT TotalLate = SUM(lateunit), TotalUndertime = SUM(undrunit), TotalAbsent = SUM(absunit), TotalHours = SUM(workunit) " +
                              "FROM HR.Timesheet WHERE username=@username AND (focsdate BETWEEN @DateFrom AND @DateTo)";
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cmd.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
            cmd.Parameters.Add(new SqlParameter("@DateTo", dateTo));
            SqlDataReader dr = cmd.ExecuteReader();

            //dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                tardiness = clsValidator.CheckFloat(dr["TotalLate"].ToString());
                undertime = clsValidator.CheckFloat(dr["TotalUndertime"].ToString());
                absence = clsValidator.CheckFloat(dr["TotalAbsent"].ToString());
                workingHours = clsValidator.CheckFloat(dr["TotalHours"].ToString());
            }
            dr.Close();

            cmd.CommandText = "SELECT TotalLWP = SUM(lwithpay), TotalLWOP = SUM(lwoutpay) " +
              "FROM HR.Timesheet WHERE username=@username AND (focsdate BETWEEN @DateFrom AND @DateTo) AND focsdate NOT IN (SELECT dateapp FROM HR.CDL)";

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                leaveWithPay = clsValidator.CheckFloat(dr["TotalLWP"].ToString());
                leaveWithOutPay = clsValidator.CheckFloat(dr["TotalLWOP"].ToString());
            }
            dr.Close();

            isPerfect = dateStart <= dateFrom && tardiness == 0 && undertime == 0 && absence == 0 && leaveWithPay == 0 && leaveWithOutPay == 0 && workingHours != 0;

        }

        return isPerfect;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string checkFlag = "<img src='../../../support/checkHR.png' alt='' />";
        string uncheckFlag = "-";
        string reportBuffer = "";
        string reportBufferDivision = "";
        string reportString = "";
        DataTable monthsTable = this.GetMonths();
        int monthsTableCount = monthsTable.Rows.Count;
        DataTable divisionTable = this.GetDivision();
        int columnCount = 4 + monthsTable.Rows.Count;
        int overallAnnual = 0;
        int[] overallMonth = new int[monthsTableCount];

        reportString += "<div class='GridBorder'>" +
                        "<table width='100%' cellpadding='5'>" +
                            "<tr><td colspan='" + columnCount.ToString() + "' class='GridText' style='text-align:center;'><b>Perfect Attendance Report</b></td></tr>" +
                            "<tr>" +
                                "<td class='GridColumns'>&nbsp;</td>" +
                                "<td class='GridColumns'><b>Division</b></td>" +
                                "<td class='GridColumns'><b>Employee</b></td>";

        foreach (DataRow row in monthsTable.Rows)
        {
            string targetMonth = clsValidator.CheckDate(row["tspfrom"].ToString()).ToString("MMMM");
            reportString += "<td class='GridColumns'><b>" + targetMonth + "</b></td>";
        }

        reportString += "<td class='GridColumns'><b>Running for Annual</b></td></tr>";

        foreach (DataRow rowD in divisionTable.Rows)
        {
            int counter = 1;
            int divisionAnnual = 0;
            int[] divisionTotalMonth = new int[monthsTableCount];
            int divisionTotal = 0;

            DataTable divisionEmployee = this.GetEmployees(rowD["divicode"].ToString());
            reportBufferDivision = "";
            reportBufferDivision += "<tr><td colspan='3' class='GridRows'><b>" + rowD["division"].ToString() + "</b></td>";
            for (int i = 0; i < monthsTableCount; ++i)
            {
                string rowColor = (i == 0 ? "GridRowsRed" : "GridRows");
                reportBufferDivision += "<td class='" + rowColor + "'>&nbsp;</td>";
            }
            reportBufferDivision += "<td class='GridRows'>&nbsp;</td>" +
                                    "</tr>";

            foreach (DataRow rowE in divisionEmployee.Rows)
            {
                int employeeTotal = 0;
                bool isRunningForAnnual = true;
                reportBuffer = "";
                reportBuffer += "<td colspan='2' class='GridRows'>&nbsp;&nbsp;&nbsp;" + rowE["EmployeeName"].ToSafeString() + "</td>";

                int ctr = 0;
                foreach (DataRow rowM in monthsTable.Rows)
                {
                    string rowColor = (ctr == 0 ? "GridRowsRed" : "GridRows");
                    DateTime dateStart = clsValidator.CheckDate(rowM["tspfrom"].ToString());
                    DateTime dateEnd = clsValidator.CheckDate(rowM["tspto"].ToString()).Date.AddDays(1).AddSeconds(-1);
                    bool isPerfectAttendance = this.IsPerfectAttendance(rowE["username"].ToString(), clsValidator.CheckDate(rowE["datestrt"].ToString()), dateStart, dateEnd);

                    reportBuffer += "<td class='" + rowColor + "' style='text-align:center;'>" + (isPerfectAttendance ? checkFlag : uncheckFlag) + "</td>";

                    if (isPerfectAttendance)
                    {
                        employeeTotal++;
                        divisionTotalMonth[ctr]++;
                        overallMonth[ctr]++;
                        divisionTotal++;
                    }
                    else
                    {
                        employeeTotal++;
                        isRunningForAnnual = false;
                    }
                    ctr++;
                }

                if (isRunningForAnnual)
                {
                    divisionAnnual++;
                    overallAnnual++;
                }

                reportBuffer += "<td class='GridRows' style='text-align:center;'>" + (isRunningForAnnual ? checkFlag : uncheckFlag) + "</td></tr>";

                if (employeeTotal > 0)
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
                reportBufferDivision += "<td class='" + rowColor + "' style='text-align:center;'><b>" + (divisionTotalMonth[i] == 0 ? "-" : divisionTotalMonth[i].ToString()) + "</b></td>";
            }
            reportBufferDivision += "<td class='GridRowsGreen' style='text-align:center;'><b>" + (divisionAnnual == 0 ? "-" : divisionAnnual.ToString()) + "</b></td></tr>";

            if (divisionTotal > 0)
            {
                reportString += reportBufferDivision;
            }
        }

        reportString += "<tr>" +
                        "<td colspan='3' class='GridRowsGreen'><b>Overall Total</b></td>";
        for (int i = 0; i < monthsTableCount; ++i)
        {
            string rowColor = (i == 0 ? "GridRowsRed" : "GridRowsGreen");
            reportString += "<td class='" + rowColor + "' style='text-align:center;'><b>" + (overallMonth[i] == 0 ? "-" : overallMonth[i].ToString()) + "</b></td>";
        }
        reportString += "<td class='GridRowsGreen' style='text-align:center;'><b>" + (overallAnnual == 0 ? "-" : overallAnnual.ToString()) + "</b></td></tr>";

        reportString += "</table></div>";
        litReport.Text = reportString;
    }
}