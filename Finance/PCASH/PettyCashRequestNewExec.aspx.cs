using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using System.Data;

public partial class Finance_PCASH_PettyCashRequestNewExec : System.Web.UI.Page
{
    public void LoadInitialized()
    {
        ddlExecutive.DataSource = clsDivision.GetDSLDHead();
        ddlExecutive.DataValueField = "pvalue";
        ddlExecutive.DataTextField = "ptext";
        ddlExecutive.DataBind();

        ddlChargeType.DataSource = clsPCASChargeType.GetDSL();
        ddlChargeType.DataValueField = "pvalue";
        ddlChargeType.DataTextField = "ptext";
        ddlChargeType.DataBind();

        ddlHeadApprover.DataSource = clsModuleApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], "PETTY", "1");
        ddlHeadApprover.DataValueField = "pvalue";
        ddlHeadApprover.DataTextField = "ptext";
        ddlHeadApprover.DataBind();

        //ddlDivisionHead.DataSource = clsDivision.GetDdlDsDHead(clsEmployee.GetDivisionCode(Request.Cookies["Speedo"]["UserName"]));
        ddlDivisionHead.DataSource = clsModuleApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], "PETTY", "2");
        ddlDivisionHead.DataValueField = "pvalue";
        ddlDivisionHead.DataTextField = "ptext";
        ddlDivisionHead.DataBind();
    }

    protected void MakeCart()
    {
        DataTable tblCart1 = new DataTable("CartItems");
        tblCart1.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart1.Columns.Add("amount", System.Type.GetType("System.Double"));
        ViewState["CartItems"] = tblCart1;

        //DataTable tblCart2 = new DataTable("CartRepresentation");
        //tblCart2.Columns.Add("rprsnttn", System.Type.GetType("System.String"));
        //ViewState["CartRepresentation"] = tblCart2;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
        if (!Page.IsPostBack)
        {
            this.LoadInitialized();
            MakeCart();
        }

    }

    protected bool ValidateAmount()
    {
        bool blnReturn = false;

        double dblCartSumValidator = 0;
        DataTable tblCartSUmValidator = ViewState["CartItems"] as DataTable;
        foreach (DataRow drw in tblCartSUmValidator.Rows)
        {
            using (clsPCASRequestDetails objRequestDetails = new clsPCASRequestDetails())
            {
                dblCartSumValidator = dblCartSumValidator + Convert.ToDouble(drw["amount"]);
            }
        }

        if (dblCartSumValidator <= 1000)
        {
            if (dgItems.Items.Count != 0)
            {
                blnReturn = true;
            }
            else
            {
                divError.Visible = true;
                lblErrMsg.Text = "No items found.";
            }
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = "The PCAS Amount should not be greater than 1,000.00 Pesos.";
        }
        return blnReturn;
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string strPurpose = "0";
        if (chkTransportation.Checked == true && chkOthers.Checked == true)
        {
            strPurpose = "3";
        }
        else if (chkTransportation.Checked == true && chkOthers.Checked == false)
        {
            strPurpose = "1";
        }
        else if (chkTransportation.Checked == false && chkOthers.Checked == true)
        {
            strPurpose = "2";
        }
        else
        {
            strPurpose = "0";
        }

        if (strPurpose != "0")
        {
            if (ValidateAmount() == false)
            {

            }
            else
            {
                using (clsPCASRequest objPCASRequest = new clsPCASRequest())
                {
                    objPCASRequest.RequestedBy = ddlExecutive.SelectedValue.ToString();
                    objPCASRequest.IsExecutive = "1";
                    objPCASRequest.Reason = txtReason.Text;
                    objPCASRequest.DateNeeded = dtpDateFromNeeded.SelectedDate;
                    objPCASRequest.Classification = strPurpose;
                    objPCASRequest.ChargeTypeCode = ddlChargeType.SelectedValue.ToString();
                    if (ddlChargeType.SelectedValue.ToString() == "1")
                    {
                        objPCASRequest.Others = "";
                        objPCASRequest.SchoolCode = "";
                        objPCASRequest.RCCode = clsDepartmentRC.GetRCcode(clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"].ToString()));
                    }
                    else if (ddlChargeType.SelectedValue.ToString() == "2")
                    {
                        objPCASRequest.Others = "";
                        objPCASRequest.SchoolCode = "";
                        objPCASRequest.RCCode = ddlMainChargeTo.SelectedValue.ToString();
                    }
                    else if (ddlChargeType.SelectedValue.ToString() == "3")
                    {
                        objPCASRequest.Others = "";
                        objPCASRequest.RCCode = "";
                        objPCASRequest.SchoolCode = ddlMainChargeTo.SelectedValue.ToString();
                    }
                    else if (ddlChargeType.SelectedValue.ToString() == "4")
                    {
                        objPCASRequest.Others = txtAppOthers.Text;
                        objPCASRequest.SchoolCode = "";
                        objPCASRequest.RCCode = "";
                    }
                    objPCASRequest.PCASStat = "P";
                    objPCASRequest.OBCode = "";
                    objPCASRequest.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                    objPCASRequest.ModifyBy = Request.Cookies["Speedo"]["UserName"].ToString();
                    objPCASRequest.Remarks = txtRemarks.Text;
                    if (objPCASRequest.Insert() > 0)
                    {

                        DataTable tblCart1 = ViewState["CartItems"] as DataTable;

                        foreach (DataRow drw in tblCart1.Rows)
                        {
                            using (clsPCASRequestDetails objRequestDetails = new clsPCASRequestDetails())
                            {
                                objRequestDetails.PCascode = clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString());
                                objRequestDetails.ItemName = drw["itemdesc"].ToString();
                                objRequestDetails.Amount = Convert.ToDouble(drw["amount"]);
                                objRequestDetails.Insert();
                            }
                        }

                        DataTable tblApprovers = new DataTable();
                        tblApprovers.Columns.Add("PCASCode");
                        tblApprovers.Columns.Add("Username");
                        tblApprovers.Columns.Add("ApproverOrder");
                        tblApprovers.Columns.Add("ApproverType");
                        tblApprovers.Columns.Add("StatusCode");
                        int intCount = 0;


                        if (ddlChargeType.SelectedValue.ToString() == "2")
                        {
                            intCount++;
                            DataRow drNewRow = tblApprovers.NewRow();
                            drNewRow["PCASCode"] = clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString());
                            drNewRow["Username"] = ddlRequestEndorser.SelectedValue.ToString();
                            drNewRow["ApproverOrder"] = intCount;
                            drNewRow["ApproverType"] = "E";
                            drNewRow["StatusCode"] = "0";
                            tblApprovers.Rows.Add(drNewRow);
                        }
                        intCount++;
                        DataRow drNewRowAH = tblApprovers.NewRow();
                        drNewRowAH["PCASCode"] = clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString());
                        drNewRowAH["Username"] = ddlHeadApprover.SelectedValue.ToString();
                        drNewRowAH["ApproverOrder"] = intCount;
                        drNewRowAH["ApproverType"] = "H";
                        drNewRowAH["StatusCode"] = "0";
                        tblApprovers.Rows.Add(drNewRowAH);

                        intCount++;
                        DataRow drNewRowDH = tblApprovers.NewRow();
                        drNewRowDH["PCASCode"] = clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString());
                        drNewRowDH["Username"] = ddlDivisionHead.SelectedValue.ToString();
                        drNewRowDH["ApproverOrder"] = intCount;
                        drNewRowDH["ApproverType"] = "D";
                        drNewRowDH["StatusCode"] = "0";
                        tblApprovers.Rows.Add(drNewRowDH);

                        int intFCount = 0;
                        foreach (DataRow drwFPCApprover in clsPCASFPCApprover.GetDSGMainForm().Rows)
                        {
                            intCount++;
                            intFCount++;
                            DataRow drNewRowFPC = tblApprovers.NewRow();
                            drNewRowFPC["PCASCode"] = clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString());
                            drNewRowFPC["Username"] = drwFPCApprover["fpcaname"].ToString();
                            drNewRowFPC["ApproverOrder"] = intCount;
                            drNewRowFPC["ApproverType"] = "F" + intFCount.ToString();
                            drNewRowFPC["StatusCode"] = "0";
                            tblApprovers.Rows.Add(drNewRowFPC);
                        }

                        using (clsPCASApproval objApproval = new clsPCASApproval())
                        {
                            objApproval.Insert(tblApprovers);
                        }


                        clsPCASRequest.SendEmailNotification("Requestor", clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString()), Request.Cookies["Speedo"]["UserName"].ToString(), ddlHeadApprover.SelectedValue.ToString());
                        if (ddlChargeType.SelectedValue.ToString() == "2")
                        {
                            clsPCASRequest.SendEmailNotification("Approver", clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString()), Request.Cookies["Speedo"]["UserName"].ToString(), ddlRequestEndorser.SelectedValue.ToString());
                        }
                        else
                        {
                            clsPCASRequest.SendEmailNotification("Approver", clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString()), Request.Cookies["Speedo"]["UserName"].ToString(), ddlHeadApprover.SelectedValue.ToString());
                        }

                        Response.Redirect("PettyCashRequestMenu.aspx");
                    }
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PettyCashRequestMenu.aspx");
    }
    protected void ddlChargeTo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlRcCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            lblIncidentalError.Visible = false;
            divError.Visible = false;
            DataTable tblCart1 = ViewState["CartItems"] as DataTable;
            tblCart1.Rows[e.Item.ItemIndex].Delete();
            ViewState["CartItems"] = tblCart1;

            dgItems.DataSource = tblCart1;
            dgItems.DataBind();

        }
        catch
        {
            Response.Redirect("PettyCashRequestNew.aspx");
        }
    }
    protected void chkbExecutive_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("PettyCashRequestNew.aspx");
    }
    protected void ddlPCashClass_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlRequestEndorser_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlMainChargeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlRequestEndorser.DataSource = clsDepartmentRC.GetDSLRCApprovers(ddlMainChargeTo.SelectedValue);
        //ddlRequestEndorser.DataValueField = "pvalue";
        //ddlRequestEndorser.DataTextField = "ptext";
        //ddlRequestEndorser.DataBind();

        ddlRequestEndorser.DataSource = clsModuleApprover.DSLApproverRC(ddlMainChargeTo.SelectedValue, "PETTY", "1");
        ddlRequestEndorser.DataValueField = "pvalue";
        ddlRequestEndorser.DataTextField = "ptext";
        ddlRequestEndorser.DataBind();

    }

    protected void ddlChargeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlChargeType.SelectedValue == "1")
        {
            trAppDepartment.Visible = false;
            trAppEndorser.Visible = false;
            trAppOthers.Visible = false;
        }
        else if (ddlChargeType.SelectedValue == "2")
        {
            trAppDepartment.Visible = true;
            trAppEndorser.Visible = true;
            trAppOthers.Visible = false;

            ddlMainChargeTo.DataSource = clsRC.GetDdlDs();
            ddlMainChargeTo.DataValueField = "pvalue";
            ddlMainChargeTo.DataTextField = "ptext";
            ddlMainChargeTo.DataBind();

            ddlRequestEndorser.DataSource = clsModuleApprover.DSLApproverRC(ddlMainChargeTo.SelectedValue, "PETTY", "1");
            ddlRequestEndorser.DataValueField = "pvalue";
            ddlRequestEndorser.DataTextField = "ptext";
            ddlRequestEndorser.DataBind();
        }
        else if (ddlChargeType.SelectedValue == "3")
        {
            trAppDepartment.Visible = true;
            trAppEndorser.Visible = false;
            trAppOthers.Visible = false;

            ddlMainChargeTo.DataSource = clsSchool.GetDSLSchool();
            ddlMainChargeTo.DataValueField = "pvalue";
            ddlMainChargeTo.DataTextField = "ptext";
            ddlMainChargeTo.DataBind();
        }
        else if (ddlChargeType.SelectedValue == "4")
        {
            trAppDepartment.Visible = false;
            trAppEndorser.Visible = false;
            trAppOthers.Visible = true;
        }
    }
    protected void btnIncedentalAdd_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        lblIncidentalError.Visible = false;
        if (dgItems.Items.Count < 8)
        {
            DataTable tblCart1 = ViewState["CartItems"] as DataTable;
            DataRow drowCart1 = tblCart1.NewRow();
            drowCart1["itemdesc"] = txtIncedentalName.Text.Trim();
            drowCart1["amount"] = string.Format("{0:0.00}", double.Parse(txtIncedentalAmount.Text));
            tblCart1.Rows.Add(drowCart1);
            ViewState["CartItems"] = tblCart1;
            dgItems.DataSource = tblCart1;
            dgItems.DataBind();
            txtIncedentalName.Text = "";
            txtIncedentalAmount.Text = "";
            //DGAddIncidentals();

            if (dgItems.Items.Count != 0)
            {

            }
            else
            {

            }
        }
        else
        {
            lblIncidentalError.Text = "Maximum number of items reached."; lblIncidentalError.Visible = true; return;
        }
    }
    protected void chkTransportation_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void chkOthers_CheckedChanged(object sender, EventArgs e)
    {

    }
}