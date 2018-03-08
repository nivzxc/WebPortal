using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using System.Data;
using Microsoft.VisualBasic;

public partial class Finance_PCASH_PettyCashRequestNew : System.Web.UI.Page
{



    public void LoadInitialized()
    {

        ddlChargeType.DataSource = clsPCASChargeType.GetDSL();
        ddlChargeType.DataValueField = "pvalue";
        ddlChargeType.DataTextField = "ptext";
        ddlChargeType.DataBind();

        ddlPayeeName.DataSource = clsEmployee.DSLEmployeeList();
        ddlPayeeName.DataValueField = "pvalue";
        ddlPayeeName.DataTextField = "ptext";
        ddlPayeeName.DataBind();

        ddlHeadApprover.DataSource = clsModuleApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], "PETTY", "1");
        ddlHeadApprover.DataValueField = "pvalue";
        ddlHeadApprover.DataTextField = "ptext";
        ddlHeadApprover.DataBind();

        //ddlDivisionHead.DataSource = clsDivision.GetDdlDsDHead(clsEmployee.GetDivisionCode(Request.Cookies["Speedo"]["UserName"]));
        ddlDivisionHead.DataSource = clsModuleApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], "PETTY", "2");
        ddlDivisionHead.DataValueField = "pvalue";
        ddlDivisionHead.DataTextField = "ptext";
        ddlDivisionHead.DataBind();

        ddlFiledOB.DataSource = clsOB.GetDSLApproveOBPCAS(Request.Cookies["Speedo"]["UserName"]);
        ddlFiledOB.DataValueField = "pvalue";
        ddlFiledOB.DataTextField = "ptext";
        ddlFiledOB.DataBind();
        if (ddlFiledOB.Items.Count == 0)
        {
            ddlFiledOB.Items.Add("No Approved OB");

        }

        lblRequestorName.Text = clsEmployee.GetName(Request.Cookies["Speedo"]["UserName"]);

        if (chkbExecutive.Checked == true)
        {
            //trObNumber.Visible = true;
            ddlFiledOB.DataSource = clsOB.GetDSL(Request.Cookies["Speedo"]["UserName"]);
            ddlFiledOB.DataTextField = "pText";
            ddlFiledOB.DataValueField = "pValue";
            ddlFiledOB.DataBind();
            if (ddlFiledOB.Items.Count == 0)
            {
                ddlFiledOB.Items.Add("No Approved OB");
            }
            LoadOBDetails();
            //GetStartEndDate();
        }
    }

    protected void LoadOBDetails()
    {
        if (ddlFiledOB.SelectedValue.ToString() != string.Empty)
        {
            clsOB ob = new clsOB(ddlFiledOB.SelectedValue.ToString());
            ob.Fill();
            txtOBCode.Text = ob.OBCode;
            txtOBType.Text = clsOB.GetOBTypeDesc(ob.OBType);
            txtRenderedTo.Text = clsDepartment.GetName(ob.DepartmentCode);
            txtDateFiled.Text = ob.DateFile.ToString("MMM dd, yyyy hh:mm tt");
            TextBox2.Text = clsUsers.GetName(ob.Username);
            txtReason.Text = ob.Reason;
            txtStatus.Text = clsOB.ToOBStatus(ob.Status);

        }

        string strWrite = "";
        DataTable tblOBDetails = clsOBDetails.GetDataTable(ddlFiledOB.SelectedValue.ToString());
        foreach (DataRow drw in tblOBDetails.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                   "<td class=''>" + clsValidator.CheckDate(drw["keyin"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                                   "<td class=''>" + clsValidator.CheckDate(drw["keyout"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                                   "<td class=''>" + drw["updateby"].ToString() + "</td>" +
                                  "</tr>";
        }
        lblSchedule.Text = strWrite;

    }

    protected void MakeCart()
    {
        DataTable tblCart1 = new DataTable("CartItems");
        tblCart1.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart1.Columns.Add("amount", System.Type.GetType("System.String"));
        ViewState["CartItems"] = tblCart1;

        //DataTable tblCart2 = new DataTable("CartRepresentation");
        //tblCart2.Columns.Add("rprsnttn", System.Type.GetType("System.String"));
        //ViewState["CartRepresentation"] = tblCart2;
    }

    protected bool ValidateOB()
    {
        bool blnReturn = false;
        if (ddlFiledOB.SelectedValue.ToString() != "No Approved OB")
        {
            blnReturn = true;
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = "Approved filed Official Business is required.";
        }
        return blnReturn;
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

        if (dblCartSumValidator<=1000)
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

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
        if (!Page.IsPostBack)
        {
            this.LoadInitialized();
            MakeCart();
            trOB.Visible = false;
            pnlHeader.Visible = false;
            txtReason.Text = clsOB.GetOBReason(ddlFiledOB.SelectedValue.ToSafeString());
        }

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string strPurpose = "0";
        if (chkTransportation.Checked == true && chkOthers.Checked == true)
        {
            strPurpose = "3";
        }
        else if (chkTransportation.Checked==true && chkOthers.Checked==false)
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

            if (ValidateOB() == false && chkTransportation.Checked == true && chkOthers.Checked == false)
            {

            }
            else
            {
                if (ValidateAmount() == false)
                {

                }
                else
                {


                    using (clsPCASRequest objPCASRequest = new clsPCASRequest())
                    {
                        if (chkOthers.Checked == true && ddlPayeeType.SelectedValue.ToString() == "others")
                        {
                            objPCASRequest.RequestedBy = ddlPayeeName.SelectedValue.ToString();
                        }
                        else
                        {
                            objPCASRequest.RequestedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                        }
                        objPCASRequest.IsExecutive = "0";
                        objPCASRequest.Reason = txtReason.Text;
                        objPCASRequest.DateNeeded = dtpDateFromNeeded.SelectedDate;
                        objPCASRequest.Classification = strPurpose;
                        if (objPCASRequest.Classification == "1" || objPCASRequest.Classification == "3")
                        {
                            objPCASRequest.OBCode = ddlFiledOB.SelectedValue.ToString();
                        }
                        else
                        {
                            objPCASRequest.OBCode = "";
                        }
                        objPCASRequest.ChargeTypeCode = ddlChargeType.SelectedValue.ToString();
                        if (ddlChargeType.SelectedValue.ToString() == "1")
                        {
                            objPCASRequest.Others = "";
                            objPCASRequest.SchoolCode = "";
                            //objPCASRequest.RCCode = clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"].ToString());
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
        Response.Redirect("PettyCashRequestNewExec.aspx");
    }
    protected void ddlPCashClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlPCashClass.SelectedValue == "2")
        //{
        //    trOB.Visible = false;
        //    pnlHeader.Visible = false;
        //    trPayeeType.Visible = true;

        //    try
        //    {
        //        if (ddlPayeeType.SelectedValue == "others")
        //        {
        //            trPayeeName.Visible = true;
        //        }
        //        else if (ddlPayeeType.SelectedValue == "self")
        //        {
        //            trPayeeName.Visible = false;
        //        }
        //    }
        //    catch { }
        //}
        //else if (ddlPCashClass.SelectedValue == "1")
        //{
        //    trOB.Visible = true;
        //    pnlHeader.Visible = true;
        //    trPayeeType.Visible = false;
        //    trPayeeName.Visible = false;
        //}
    }

    protected void ddlRequestEndorser_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlMainChargeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlRequestEndorser.DataSource = clsDepartmentRC.GetDSLRCApprovers(ddlMainChargeTo.SelectedValue);
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
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void btnViewOB1_Click(object sender, ImageClickEventArgs e)
    {
        LoadOBDetails();
        pnlModal_ModalPopupExtender.Show();
    }

    protected void btnViewOB_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlFiledOB.SelectedValue.ToString() != string.Empty)
        {
            pnlModal_ModalPopupExtender.Show();
        }
    }
    protected void ddlFiledOB_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOBDetails();
        txtReason.Text = clsOB.GetOBReason(ddlFiledOB.SelectedValue.ToSafeString());
    }
    protected void ddlPayeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayeeType.SelectedValue == "others")
        {
            trPayeeName.Visible = true;
        }
        else if (ddlPayeeType.SelectedValue == "self")
        {
            trPayeeName.Visible = false;
        }
    }
    protected void ddlPayeeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void chkTransportation_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTransportation.Checked == false)
        {
            trOB.Visible = false;
            pnlHeader.Visible = false;
            //trPayeeType.Visible = true;

            //try
            //{
            //    if (ddlPayeeType.SelectedValue == "others")
            //    {
            //        trPayeeName.Visible = true;
            //    }
            //    else if (ddlPayeeType.SelectedValue == "self")
            //    {
            //        trPayeeName.Visible = false;
            //    }
            //}
            //catch { }
        }
        else if (chkTransportation.Checked == true)
        {
            trOB.Visible = true;
            pnlHeader.Visible = true;
            //trPayeeType.Visible = false;
            //trPayeeName.Visible = false;
        }
    }

    protected void chkOthers_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOthers.Checked == true)
        {
            trPayeeType.Visible = true;

            try
            {
                if (ddlPayeeType.SelectedValue == "others")
                {
                    trPayeeName.Visible = true;
                }
                else if (ddlPayeeType.SelectedValue == "self")
                {
                    trPayeeName.Visible = false;
                }
            }
            catch { }
        }
        else if (chkOthers.Checked == false)
        {
            trPayeeType.Visible = false;
            trPayeeName.Visible = false;
        }
    }
}