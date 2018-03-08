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

public partial class CIS_MRCF_MRCFDetailsPM : System.Web.UI.Page
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

    public void BindEmployee()
    {
        DataTable tblEmployeesAssign = clsMRCF.DDLEmployee("PROC", "1");
        ddlAssign.DataSource = tblEmployeesAssign;
        ddlAssign.DataValueField = "pvalue";
        ddlAssign.DataTextField = "ptext";
        ddlAssign.DataBind();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        btnPrint.Visible = false;


    


        //btnApprove.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnApprove).ToString());
        //btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString());
        //btnModify.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnModify).ToString());

        string strProcessScript = "this.value='Processing...';this.disabled=true;";
        btnApprove.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnApprove, "").ToString());
        btnDisApprove.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnDisApprove, "").ToString());
        btnModify.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnModify, "").ToString());
        btnApprove2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnApprove2, "").ToString());
        btnDisapprove2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnDisapprove2, "").ToString());
        btnModification2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnModification2, "").ToString());
        
            
        
        if (!Page.IsPostBack)
        {
            BindEmployee();
            clsMRCF.AuthenticateUser(clsMRCF.MRCFUserType.ProcurementManager, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["mrcfcode"].ToString());

            bool blnReadOnly;
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
            blnReadOnly = (hdnProcMngrStat.Value == "F" ? false : true);

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
            divButtons.Visible = !blnReadOnly;
            divButtons2.Visible = !blnReadOnly;
            divSave.Visible = blnReadOnly;
            txtProcMngrRem.ReadOnly = blnReadOnly;
            if (blnReadOnly)
                txtProcMngrRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE CIS.MrcfDetails SET itemspec=@itemspec WHERE mitmcode=@mitmcode";
            cmd.Parameters.Add("@itemspec", SqlDbType.VarChar, 5000);
            cmd.Parameters.Add("@mitmcode", SqlDbType.BigInt);
            cn.Open();
            foreach (DataGridItem itm in dgItems.Items)
            {
                HiddenField phdnMitmCode = (HiddenField)itm.FindControl("hdnMitmCode");
                TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
                cmd.Parameters["@itemspec"].Value = ptxtItemSpec.Text;
                cmd.Parameters["@mitmcode"].Value = phdnMitmCode.Value;
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (!clsOracleMrcf.IsOracleUp())
        {
            Response.Redirect("OracleDatabaseProblem.aspx");
        }

        clsMRCFAssign objAssign = new clsMRCFAssign();
        objAssign.MRCFCode = Request.QueryString["mrcfcode"].ToString();
        objAssign.AssignBy = objAssign.GetProcurementManager("PROCMNGR");
        objAssign.CreateBy = objAssign.GetProcurementManager("PROCMNGR");
        objAssign.IsActive = "1"; //1 = Active , 0 = Inactive
        objAssign.AssignTo = ddlAssign.SelectedValue.ToString();
        objAssign.Remarks = txtAssignRemarks.Text;
        objAssign.StatusCode = objAssign.GetInitialStatusCode();
        objAssign.AssignEmployee();
        objAssign.AssignEmployeeDetails("None");


        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();


            //Retrieve information of MRCF
            DataTable tblMRCF = clsOracleMrcf.GetMRCFDetails(Request.QueryString["mrcfcode"]);

            using (clsOracleMrcf ReqMrcf = new clsOracleMrcf())
            {
                //Update MRCF status
                cmd.CommandText = "UPDATE CIS.Mrcf SET status='A',procstat='A',sprvstat=@sprvstat,headstat=@headstat,procdate='" + DateTime.Now + "',procrem=@procrem WHERE mrcfcode='" + Request.QueryString["mrcfcode"] + "'";
                cmd.Parameters.Add("@procrem", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
                cmd.Parameters.Add("@headstat", SqlDbType.Char, 1);
                cmd.Parameters["@procrem"].Value = txtProcMngrRem.Text;
                cmd.Parameters["@sprvstat"].Value = (hdnGrpHeadStat.Value == "F" ? "N" : hdnGrpHeadStat.Value);
                cmd.Parameters["@headstat"].Value = (hdnDiviHeadStat.Value == "F" ? "N" : hdnDiviHeadStat.Value);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                //Update MRCF Details
                cmd.CommandText = "UPDATE CIS.MrcfDetails SET itemspec=@itemspec WHERE mitmcode=@mitmcode";
                cmd.Parameters.Add("@itemspec", SqlDbType.VarChar, 5000);
                cmd.Parameters.Add("@mitmcode", SqlDbType.BigInt);
                foreach (DataGridItem itm in dgItems.Items)
                {
                    HiddenField phdnMitmCode = (HiddenField)itm.FindControl("hdnMitmCode");
                    TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
                    cmd.Parameters["@itemspec"].Value = ptxtItemSpec.Text;
                    cmd.Parameters["@mitmcode"].Value = phdnMitmCode.Value;
                    cmd.ExecuteNonQuery();
                }
                cmd.Parameters.Clear();

                clsMRCF.SendNotification(clsMRCF.MRCFMailType.ApproveToRequestor, txtRequestorName.Text, txtProcMngrName.Text, hdnRequestorMail.Value, txtMrcfCode.Text);
                clsMRCF.SendNotification(clsMRCF.MRCFMailType.ApproveToApproverPM, txtRequestorName.Text, txtProcMngrName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtMrcfCode.Text);
                

                //Validate if the Request is not canvass only. If Not it will enter the ORACLE for Interfacing.
                if (clsMRCF.GetRequestType(Request.QueryString["mrcfcode"]) != "C")
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
                    }

                    Response.Redirect("MRCFMenu.aspx");

                    
                }
                else
                {
                    Response.Redirect("MRCFMenu.aspx");
                }

            }
        }
    }

    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        clsMRCF mrcf = new clsMRCF(txtMrcfCode.Text);
        mrcf.DisapprovePM(txtProcMngrRem.Text, hdnGrpHeadStat.Value, hdnDiviHeadStat.Value);

        clsMRCF.SendNotification(clsMRCF.MRCFMailType.DisapproveToRequestor, txtRequestorName.Text, txtProcMngrName.Text, hdnRequestorMail.Value, txtMrcfCode.Text);
        clsMRCF.SendNotification(clsMRCF.MRCFMailType.DisapproveToApproverPM, txtRequestorName.Text, txtProcMngrName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtMrcfCode.Text);

        Response.Redirect("MRCFMenu.aspx");
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        clsMRCF mrcf = new clsMRCF(txtMrcfCode.Text);
        mrcf.ModificationPM(txtProcMngrRem.Text, hdnGrpHeadStat.Value, hdnDiviHeadStat.Value);

        clsMRCF.SendNotification(clsMRCF.MRCFMailType.ModificationToRequestor, txtRequestorName.Text, txtProcMngrName.Text, hdnRequestorMail.Value, txtMrcfCode.Text);
        clsMRCF.SendNotification(clsMRCF.MRCFMailType.ModificationToApproverPM, txtRequestorName.Text, txtProcMngrName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtMrcfCode.Text);

        Response.Redirect("MRCFMenu.aspx");
    }

    protected void chkShowSpecification_CheckedChanged(object sender, EventArgs e)
    {
        SpecificationVisibility(chkShowSpecification.Checked);
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("rptMRCFDetails.aspx?mrcfcode=" + Request.QueryString["mrcfcode"]);
    }


    protected void LoadListAssigned()
    {
        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
        string strStatus = "";
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Top 10 (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,statcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE assignto = '" + ddlAssign.SelectedValue.ToString() + "' AND isactive = '1' ORDER BY hdlrcode ASC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                           {
                               strStatus = dr["statcode"].ToString() == "000" ? "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='100%' border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='red' style='border-radius:1px; ' width= '100%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>" : "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'  border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='#1874cd' style='border-radius:1px; ' width= '" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>";
                strWrite = strWrite + "<tr>" +
                   
                                                                                                            "<td class='GridRows'>" +
                                                                                                            "<a href='MRCFDetailsDH.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["IntendedFor"] + "</a><br>" +
                                                                                                             "Assigned by: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignby"] + "'>" + dr["assignby"] + "</a><br>" +
                                                                                                               "Date Assigned: " + Convert.ToDateTime(dr["DateAssign"]).ToString("MMMM dd, yyyy") + "<br>" +
                                                                                                          "Remarks: " + dr["Remarks"] +
                                                                                                             "</td>" +
                                                                                                            "<td class='GridRows'>" + dr["StatusDescription"].ToString() +
                                                                                                       strStatus +
                                                                                                        "</td>" +
                                                                                                        "</tr>" ;
                intCtr++;
            }
            dr.Close();
        }
                
        Response.Write(strWrite);
        if (intCtr == 0)
            Response.Write("<tr><td colspan='2' class='GridRows' style='text-align:center;'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='2' class='GridRows' style='text-align:center;'>[ " + intCtr + " records found ]</td></tr>");
    }




    protected void lbtnLoadEmployee_Click(object sender, EventArgs e)
    {
        Bewise.Web.UI.WebControls.FlashControl p = Master.FindControl("FlashControl1") as Bewise.Web.UI.WebControls.FlashControl;
        if (p != null)
        {
            p.Visible = false;
        }
        ModalPopupExtender1.Show();
    }
    protected void lbtnClose_Click(object sender, EventArgs e)
    {
        Bewise.Web.UI.WebControls.FlashControl p = Master.FindControl("FlashControl1") as Bewise.Web.UI.WebControls.FlashControl;
        if (p != null)
        {
            p.Visible = true;
        }

        ModalPopupExtender1.Hide();
    }
}
