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

public partial class CIS_MRCF_MRCFReImport : System.Web.UI.Page
{
    protected void SpecificationVisibility(bool pShow)
    {
        foreach (DataGridItem itm in dgItems.Items)
        {
            TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
            ptxtItemSpec.Visible = pShow;
        }
    }

    protected void BindItems()
    {
        dgItems.DataSource = clsMRCF.GetMrcfItems(Request.QueryString["mrcfcode"]);
        dgItems.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            if (clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"]) == "023")
            {
                //bool blnReadOnly;
                txtMrcfCode.Text = Request.QueryString["mrcfcode"].ToString();
                txtProcMngrName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

                clsMRCF mrcf = new clsMRCF(txtMrcfCode.Text);
                mrcf.Fill();
                hdnRequestor.Value = mrcf.Username;
                txtReqType.Text = mrcf.RequestTypeDesc;
                txtDateReq.Text = mrcf.DateRequested.ToString("MMMM dd, yyyy");
                txtIntended.Text = mrcf.Intended;
                hdnChargeTo.Value = mrcf.ChargeTo;
                hdnGrpHeadCode.Value = mrcf.GroupHead;
                txtGrpHeadRem.Text = mrcf.GroupHeadRemarks;
                hdnGrpHeadStat.Value = mrcf.GroupHeadStatus;
                hdnDiviHeadCode.Value = mrcf.DivisionHead;
                txtDiviHeadRem.Text = mrcf.DivisionHeadRemarks;
                hdnDiviHeadStat.Value = mrcf.DivisionHeadStatus;
                hdnProcMngrCode.Value = mrcf.ProcurementManager;
                txtProcMngrRem.Text = mrcf.ProcurementManagerRemarks;
                hdnProcMngrStat.Value = mrcf.ProcurementManagerStatus;
                txtStat.Text = mrcf.StatusDescription;
                //blnReadOnly = (hdnProcMngrStat.Value == "F" ? false : true);

                txtRCName.Text = clsEmployee.GetRCName(hdnRequestor.Value);
                txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);

                using (clsUsers users = new clsUsers())
                {
                    users.Username = hdnRequestor.Value;
                    users.Fill();
                    txtRequestorName.Text = users.FullName;
                    hdnRequestorMail.Value = users.Email;

                    users.Username = hdnGrpHeadCode.Value;
                    users.Fill();
                    txtGrpHeadName.Text = users.FullName;
                    hdnGrpHeadMail.Value = users.Email;

                    users.Username = hdnDiviHeadCode.Value;
                    users.Fill();
                    txtDiviHeadName.Text = users.FullName;
                    hdnDiviHeadMail.Value = users.Email;
                }

                BindItems();
                //divButtons.Visible = !blnReadOnly;
                //divButtons2.Visible = !blnReadOnly;
                //divSave.Visible = blnReadOnly;
                // txtProcMngrRem.ReadOnly = blnReadOnly;
                // if (blnReadOnly)
                //    txtProcMngrRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();

            //Retrieve information of MRCF
            DataTable tblMRCF = clsOracleMrcf.GetMRCFDetails(Request.QueryString["mrcfcode"]);

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
                    cmd.Parameters.Add(new SqlParameter("@mrcfcode", Request.QueryString["mrcfcode"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@btchcode", intBatchCode));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //Insert MRCFCode and Batch code of Approved MRCF that is uploaded in interface table
                    cmd.CommandText = "INSERT INTO CIS.MrcfBatch VALUES (@mrcfcode,@btchcode)";
                    cmd.Parameters.Add(new SqlParameter("@mrcfcode", Request.QueryString["mrcfcode"].ToString()));
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

    protected void chkShowSpecification_CheckedChanged(object sender, EventArgs e)
    {
        SpecificationVisibility(chkShowSpecification.Checked);
    }
}