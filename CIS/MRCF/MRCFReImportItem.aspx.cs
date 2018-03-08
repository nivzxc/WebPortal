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

public partial class CIS_MRCF_MRCFReImportItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"]) != "023")
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
    }

    protected DataTable GetMRCFDetail(string pMRCFCode, string pMRCFLine)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT CIS.MRCF.mrcfcode as MRCFCode, CIS.MRCF.datereq as DateReq,CIS.MRCF.username as Uname, CIS.MRCFDETAILS.qty As Quantity,CIS.MRCFDETAILS.asstcode As AssetCode, CIS.MRCFDETAILS.ltypcode AS LineTypeCode," +
                                  "CIS.MRCFDETAILS.itemcode as ItemCode, CIS.MRCFDETAILS.ItemDesc as ItemDesc,CIS.MRCFDETAILS.unit as Unit,MRCFDETAILS.GLAccount as GLAccount,MRCFDETAILS.Destination as Destination,CIS.MRCFDETAILS.dateneed as DateEnd,CIS.MRCF.proccode as proccode  " +
                                  "FROM cis.mrcf inner join cis.mrcfdetails on CIS.MRCF.mrcfcode = cis.mrcfdetails.mrcfcode WHERE CIS.MRCF.mrcfcode ='" + pMRCFCode + "' AND CIS.MRCFDetails.mitmcode='" + pMRCFLine + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);

            }
        }
        return tblReturn;
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

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();

            //Retrieve information of MRCF
            DataTable tblMRCF = GetMRCFDetail(txtMRCF.Text, txtLineNumber.Text);

            using (clsOracleMrcf ReqMrcf = new clsOracleMrcf())
            {
                //Generate batch entry number
                int intBatchCode = 0;
                cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='btchcode'";
                intBatchCode = cmd.ExecuteScalar().ToString().ToInt() + 1;
                cmd.Parameters.Clear();
                ReqMrcf.BatchId = intBatchCode;

                if (ReqMrcf.Insert(tblMRCF) == 1)
                {
                    //Delete MRCF and Batch if existing
                    cmd.CommandText = "DELETE FROM CIS.MrcfBatch WHERE mrcfcode= @mrcfcode";
                    cmd.Parameters.Add(new SqlParameter("@mrcfcode", txtMRCF.Text));
                    cmd.Parameters.Add(new SqlParameter("@btchcode", intBatchCode));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //Insert MRCFCode and Batch code of Approved MRCF that is uploaded in interface table
                    cmd.CommandText = "INSERT INTO CIS.MrcfBatch VALUES (@mrcfcode,@btchcode)";
                    cmd.Parameters.Add(new SqlParameter("@mrcfcode", txtMRCF.Text));
                    cmd.Parameters.Add(new SqlParameter("@btchcode", intBatchCode));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //Increment Batchcode in SPeedo.Keys
                    cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=(pvalue+1) WHERE pkey='btchcode'";
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    Response.Redirect("MRCFIssues.aspx");
                }
                else
                {
                    Response.Redirect("MRCFIssues.aspx");
                }

            }
        }

    }
}