using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using HRMS;
using STIeForms;

public partial class CIS_MRCF_MRCFExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=MRCF_" + DateTime.Now.ToString("MM-dd-yy") + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";    
    }

    protected void LoadRecords()
    {
        string strWrite = "";
        int intCtr = 1;
        DataTable tblLeaveForApproval = clsMRCF.DDLExportToExcel(Request.QueryString["scode"], Request.QueryString["ucode"]);
        foreach (DataRow drw in tblLeaveForApproval.Rows)
        {
            try
            {

                strWrite += "<tr>" +
                             "<td class='GridRows' style='text-align:center;'>" + drw["batchcode"].ToString() + "</td>" +
                             "<td class='GridRows' style='text-align:center;'>" + drw["MRCFCode"].ToString() + "</td>" +
                             "<td class='GridRows' >" + drw["IntendedFor"].ToString() + "</td>" +
                             "<td class='GridRows' >" + drw["Requestor"].ToString() + "</td>" +
                             "<td class='GridRows' style='text-align:center;'>" + Convert.ToDateTime(drw["Datereq"].ToString()).ToString("dd-MMM-yy") + "</td>" +
                             "<td class='GridRows' >" + drw["assignto"].ToString() + "</td>" +
                             "<td class='GridRows' style='text-align:center;'>" + drw["StatusDescription"].ToString() + "</td>" +
                             "<td class='GridRows' style='text-align:center;'>" + Convert.ToDateTime(drw["Dateassign"].ToString()).ToString("dd-MMM-yy") + "</td>" +
                             "</tr>";
                intCtr += 1;
            }
            catch { }
        }

        Response.Write(strWrite);
    }
}