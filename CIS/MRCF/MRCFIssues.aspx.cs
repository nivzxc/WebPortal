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
using STIeForms;
using Oracles;

public partial class CIS_MRCF_MRCFIssues : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsOracleMrcf.IsOracleUp())
        {
            Response.Redirect("OracleDatabaseProblem.aspx");
        }
        if (clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"]) != "023")
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
    }

    protected void LoadMRCFError()
    {
        string strWrite = "";
        DataTable tblMRCFImportError = clsOracleMrcf.GetMRCFError();
        foreach (DataRow drNew in tblMRCFImportError.Rows)
        {
            if (clsOracleMrcf.IsExist(drNew["BATCH_ID"].ToString()))
            {
                strWrite = strWrite + "<tr>" +
                                      "<td class='GridRows'>" + drNew["BATCH_ID"].ToString() + "</td>" +
                                       "<td class='GridRows'><a href='MRCFReImport.aspx?mrcfcode=" + clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()) + "' style='font-size:small;'>" + clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()) + "</a></td>" +
                                      "</tr>";
            }
        }
        Response.Write(strWrite);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strMRCFCode = txtMRCF.Text;

        if (strMRCFCode != "")
        {
            Response.Redirect("MRCFReImport.aspx?mrcfcode=" + strMRCFCode);
        }
    }
}