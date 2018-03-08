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
using Oracles;

public partial class CIS_MRCF_MRCFViewOracle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!clsOracleMrcf.IsOracleUp())
        {
            Response.Redirect("OracleDatabaseProblem.aspx");
        }
    }

    protected void LoadMRCFForImport()
    {
        string strWrite = "";
        DataTable tblMRCFForImport = clsOracleMrcf.GetMRCFForImport();
        foreach (DataRow drNew in tblMRCFForImport.Rows)
        {
            if (!clsOracleMrcf.IsBatchVoid(drNew["BATCH_ID"].ToString()))
            {
                if (clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()) != string.Empty)
                {
                    clsMRCF mrcf = new clsMRCF(clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()));
                    mrcf.Fill();
                    strWrite = strWrite + "<tr>" +
                                          "<td class='GridRows'>" + drNew["BATCH_ID"].ToString() + "</td>" +
                                          "<td class='GridRows'><a href='MRCFDetailsPM.aspx?mrcfcode=" + clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()) + "' style='font-size:small;'>" + clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()) + "</a></td>" +
                                          "<td class='GridRows'>" + mrcf.Intended + "</td>" +
                                          "<td class='GridRows'>" + mrcf.Username + "</td>" +
                                          "<td class='GridRows'>" + mrcf.DateRequested.ToString("MMMM dd, yyyy") + "</td>" +
                                          "</tr>";
                }
            }
        }
        Response.Write(strWrite);
    }

    //protected void LoadMRCFError()
    //{
    //    string strWrite = "";
    //    DataTable tblMRCFImportError = clsOracleMrcf.GetMRCFError();
    //    foreach (DataRow drNew in tblMRCFImportError.Rows)
    //    {
    //        if (clsOracleMrcf.IsExist(drNew["BATCH_ID"].ToString()))
    //        {
    //            strWrite = strWrite + "<tr>" +
    //                                  "<td class='GridRows'>" + drNew["BATCH_ID"].ToString() + "</td>" +
    //                                   "<td class='GridRows'><a href='MRCFReImport.aspx?mrcfcode=" + clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()) + "' style='font-size:small;'>" + clsOracleMrcf.GetMRCF(drNew["BATCH_ID"].ToString()) + "</a></td>" +
    //                                  "</tr>";
    //        }
    //    }
    //    Response.Write(strWrite);
    //}
}