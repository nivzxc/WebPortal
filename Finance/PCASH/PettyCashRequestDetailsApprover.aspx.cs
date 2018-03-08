using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STIeForms;
using HRMS;
using System.Data;

public partial class Finance_PCASH_PettyCashRequestDetailsApprover : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (clsPCASApproval.GetApproverType(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["pcascode"].ToString()) == "F1" || clsPCASApproval.GetApproverType(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["pcascode"].ToString()) == "F2" || clsPCASApproval.GetApproverType(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["pcascode"].ToString()) == "F3")
            {
                trFPC1.Visible = true;
                trFPC2.Visible = true;
                trFPC3.Visible = true;
                trAssignedCashier.Visible = true;
                btnApprove.Visible = false;
                btnApprove2.Visible = false;
                btnSaveAndApprove.Visible = true;
                btnSaveAndApprove2.Visible = true;
                trFPCD1.Visible = true;
                trFPCD2.Visible = true;
                trFPCFinalApprover.Visible = true;
                if (clsPCASApproval.GetApproverType(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["pcascode"].ToString()) == "F3")
                {
                    ddlFPCFinalApprover.Enabled = false;
                }
            }
            else 
            {
                trFPCD1.Visible = false;
                trFPCD2.Visible = false;
            }

            MakeCart();
            using (clsPCASRequest objPCASRequest = new clsPCASRequest())
            {
                objPCASRequest.PCASCode = Request.QueryString["pcascode"].ToString();
                objPCASRequest.Fill();
                lblPCASCode.Text = Request.QueryString["pcascode"].ToString();
                lblReason.Text = objPCASRequest.Reason;
                lblRequestor.Text = clsEmployee.GetName(objPCASRequest.CreatedBy);
                lblPayeeName.Text = clsEmployee.GetName(objPCASRequest.RequestedBy);
                lblDataFundsNeeded.Text = objPCASRequest.DateNeeded.ToString("MM/dd/yyyy");
                lblClassification.Text = clsPCASClassfication.GetName(objPCASRequest.Classification);
                lblFiledOB.Text = objPCASRequest.OBCode.ToString().Trim().Length == 0 ? "Not Applicable" : objPCASRequest.OBCode;
                lblChargeType.Text = clsPCASChargeType.GetName(objPCASRequest.ChargeTypeCode);
                if (objPCASRequest.ChargeTypeCode == "1")
                {
                    lblChargeTo.Text = clsRC.GetRCName(objPCASRequest.RCCode);
                }
                else if (objPCASRequest.ChargeTypeCode == "2")
                {
                    lblChargeTo.Text = clsRC.GetRCName(objPCASRequest.RCCode);
                }
                else if (objPCASRequest.ChargeTypeCode == "3")
                {
                    //lblChargeTo.Text = clsDepartment.GetName(objPCASRequest.SchoolCode);
                    lblChargeTo.Text = clsSchool.GetSchoolName(objPCASRequest.SchoolCode);
                }
                else if (objPCASRequest.ChargeTypeCode == "4")
                {
                    lblChargeTo.Text = objPCASRequest.Others;
                }
                lblRemarks.Text = objPCASRequest.Remarks;
                txtApprovedRFA.Text = objPCASRequest.ApprovedRFA.ToString();
                txtAmountAllocated.Text = objPCASRequest.AmountAllocated.ToString();
                txtNet.Text = objPCASRequest.NetAmount.ToString();
                txtRequestAmount.Text = objPCASRequest.RequestAmount.ToString();
                txtRemainingBudget.Text = objPCASRequest.RemainingBudget.ToString();

                ddlCustodian.DataSource = clsPCASCustodianFPC.GetDSL();
                ddlCustodian.DataValueField = "pvalue";
                ddlCustodian.DataTextField = "ptext";
                ddlCustodian.DataBind();

                ddlFPCFinalApprover.DataSource = clsModuleApprover.GetDSLFinanceApprover("PETTYFPC3", "3");
                ddlFPCFinalApprover.DataValueField = "pvalue";
                ddlFPCFinalApprover.DataTextField = "ptext";
                ddlFPCFinalApprover.DataBind();

                try
                {
                    ddlCustodian.SelectedValue = clsPCASRequestCustodian.GetUsername(Request.QueryString["pcascode"].ToString());
                }
                catch
                { }
                
                try
                {
                    ddlFPCFinalApprover.SelectedValue = clsPCASApproval.GetFinalFPCApproverUserName(Request.QueryString["pcascode"].ToString());
                }
                catch
                { }

                ddlSchool.DataSource = clsFinanceAccountDueFrom.GetDSLAccountDueFrom();
                ddlSchool.DataValueField = "pvalue";
                ddlSchool.DataTextField = "ptext";
                ddlSchool.DataBind();

                ddlRcCode.DataSource = clsRC.GetDdlDs();
                ddlRcCode.DataValueField = "pvalue";
                ddlRcCode.DataTextField = "ptext";
                ddlRcCode.DataBind();

                ddlAccountExpenses.DataSource = clsFinanceAccountExpenses.GetDSLAccountExpenses();
                ddlAccountExpenses.DataValueField = "pvalue";
                ddlAccountExpenses.DataTextField = "ptext";
                ddlAccountExpenses.DataBind();

                this.LoadDetails();

            }
        }
    }

    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("aexpcode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("schlcode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("rccode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("others", System.Type.GetType("System.String"));
        tblCart.Columns.Add("amount", System.Type.GetType("System.String"));
        ViewState["Cart"] = tblCart;

        DataTable tblCartdg = new DataTable("Cartdg");
        tblCartdg.Columns.Add("aexpname", System.Type.GetType("System.String"));
        tblCartdg.Columns.Add("chargeto", System.Type.GetType("System.String"));
        tblCartdg.Columns.Add("amount", System.Type.GetType("System.String"));
        ViewState["Cartdg"] = tblCartdg;
    }



    protected void LoadDetails()
    {
        DataTable tblDetails = clsPCASRequestAllocation.GetDSGMainForm(Request.QueryString["pcascode"]);
        ViewState["Cart"] = tblDetails;

        DataTable tblCartdg = ViewState["Cartdg"] as DataTable;

        foreach (DataRow drw in tblDetails.Rows)
        {
            DataRow drowCartdg = tblCartdg.NewRow();
            drowCartdg["aexpname"] = drw["aexpname"];
            string strChargedTo = "";
            if (drw["schlcode"].ToString().Trim() != "")
            { strChargedTo = clsSchool.GetSchoolName(drw["schlcode"].ToString().Trim()); }
            if (drw["rccode"].ToString().Trim() != "")
            { strChargedTo = clsRC.GetRCName(drw["rccode"].ToString().Trim()); }
            if (drw["others"].ToString().Trim() != "")
            { strChargedTo = drw["others"].ToString().Trim(); }

            double dblAmount = double.Parse(drw["amount"].ToString());
            drowCartdg["chargeto"] = strChargedTo;
            drowCartdg["amount"] = string.Format("{0:0,0.00}", dblAmount);
            tblCartdg.Rows.Add(drowCartdg);
        }

        ViewState["Cartdg"] = tblCartdg;
        dgItems.DataSource = tblCartdg;
        dgItems.DataBind();

        //added 2013-09-02
        double dblCartSum = 0;
        DataTable tblCartSUm = ViewState["Cart"] as DataTable;
        foreach (DataRow drw in tblCartSUm.Rows)
        {
            using (clsPCASRequestDetails objRequestDetails = new clsPCASRequestDetails())
            {
                dblCartSum = dblCartSum + Convert.ToDouble(drw["amount"]);
            }
        }
        //txtRequestAmount.Text = string.Format("{0:0,0.00}", dblCartSum);
        txtRequestAmount.Text = string.Format("{0:0.00}", dblCartSum);
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string strApproverType = clsPCASApproval.GetApproverType(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["pcascode"].ToString());
        if (clsPCASApproval.TagApprovedOrNot(Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), "1") > 0)
        {
            try
            {
                clsPCASRequest.SendEmailNotification("FPCApproved", Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), "");
                if ((strApproverType == "E") || (strApproverType == "H") || (strApproverType == "D") || (strApproverType == "F3"))
                {
                    if ((strApproverType == "F3"))
                    {
                        clsPCASRequest.Approve(Request.QueryString["pcascode"].ToString());
                        clsPCASRequest.UpdateAdjustment(Request.QueryString["pcascode"].ToString(), clsPCASRequestAllocation.GetAmount(Request.QueryString["pcascode"].ToString()) - clsPCASRequestDetails.GetAmount(Request.QueryString["pcascode"].ToString()));
                    }
                    clsPCASRequest.SendEmailNotification("Requestor", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), clsPCASApproval.GetNextApproverUserName(Request.QueryString["pcascode"].ToString()));
                }
            }
            catch { }
            clsPCASRequest.SendEmailNotification("Approver", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), clsPCASApproval.GetNextApproverUserName(Request.QueryString["pcascode"].ToString()));
            //clsPCASRequest.SendEmailNotification("Requestor", clsPCASRequest.GetLastCreatedRequest(Request.Cookies["Speedo"]["UserName"].ToString()), Request.Cookies["Speedo"]["UserName"].ToString(), ddlHeadApprover.SelectedValue.ToString());
            Response.Redirect("PettyCashRequestMenu.aspx");
        }


    }
    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        if (clsPCASApproval.TagApprovedOrNot(Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), "2") > 0)
        {
            clsPCASRequest.DisApprove(Request.QueryString["pcascode"].ToString());
            clsPCASRequest.SendEmailNotification("Requestor", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), clsPCASApproval.GetNextApproverUserName(Request.QueryString["pcascode"].ToString()));
            Response.Redirect("PettyCashRequestMenu.aspx");
        }
    }

    public void LoadPCASDetails()
    {
        string strWrite = "";
        double dbltotal = 0.0;
        string strChargeTo = "";
        DataTable tblDetails = clsPCASRequestDetails.GetDSGMainForm(Request.QueryString["pcascode"].ToString());
        //DataTable tblDetails = clsRFPRequestDetails.GetDSG(Request.QueryString["ControlNumber"]);
        foreach (DataRow drw in tblDetails.Rows)
        {
            //if (drw["schlcode"].ToString().Trim() != "")
            //{ strChargeTo = clsSchool.GetSchoolName(drw["schlcode"].ToString().Trim()); }
            //if (drw["rccode"].ToString().Trim() != "")
            //{ strChargeTo = clsRC.GetRCName(drw["rccode"].ToString().Trim()); }
            //if (drw["others"].ToString().Trim() != "")
            //{ strChargeTo = drw["others"].ToString().Trim(); }

            double dblAmount = Double.Parse(drw["amount"].ToString());
            strWrite += "<tr>" +
                          "<td colspan='2' class='GridRows'>&nbsp;" + drw["itemname"].ToString() + "</td>" +
                //"<td class='GridRows'>&nbsp;" + strChargeTo + "</td>" +
                          "<td align='right' class='GridRows'>" + string.Format("{0:0,0.00}", dblAmount) + "</td>" +
                        "</tr>";
            dbltotal += double.Parse(drw["amount"].ToString());
        }

        strWrite += "<tr>" +
                         "<td colspan='3'align='right' class='GridRows'><b>Total Amount:    P " + string.Format("{0:0,0.00}", dbltotal) + "</b></td>" +
                    "</tr>";

        Response.Write(strWrite);
    }

    public void LoadPCASAdjustmentDetails()
    {
        string strWrite = "";
        double dbltotal = 0.0;
        string strChargeTo = "";
        DataTable tblDetails = clsPCASRequestDetails.GetDSGMainForm(Request.QueryString["pcascode"].ToString());
        //DataTable tblDetails = clsRFPRequestDetails.GetDSG(Request.QueryString["ControlNumber"]);
        foreach (DataRow drw in tblDetails.Rows)
        {
            //if (drw["schlcode"].ToString().Trim() != "")
            //{ strChargeTo = clsSchool.GetSchoolName(drw["schlcode"].ToString().Trim()); }
            //if (drw["rccode"].ToString().Trim() != "")
            //{ strChargeTo = clsRC.GetRCName(drw["rccode"].ToString().Trim()); }
            //if (drw["others"].ToString().Trim() != "")
            //{ strChargeTo = drw["others"].ToString().Trim(); }

            double dblAmount = Double.Parse(drw["amount"].ToString());
            strWrite += "<tr>" +
                          "<td colspan='2' class='GridRows'>&nbsp;" + drw["itemname"].ToString() + "</td>" +
                //"<td class='GridRows'>&nbsp;" + strChargeTo + "</td>" +
                          "<td align='right' class='GridRows'>" + string.Format("{0:0,0.00}", dblAmount) + "</td>" +
                        "</tr>";
            dbltotal += double.Parse(drw["amount"].ToString());
        }

        strWrite += "<tr>" +
                         "<td colspan='3'align='right' class='GridRows'><b>Total Amount:    P " + string.Format("{0:0,0.00}", dbltotal) + "</b></td>" +
                    "</tr>";

       Response.Write(strWrite);
    }
    protected void ddlChargeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlChargeTo.SelectedValue.ToString() == "School")
        {
            trRc.Visible = false;
            trDueFrom.Visible = true;
            trOthers.Visible = false;

            ddlAccountExpenses.DataSource = clsFinanceAccountExpenses.GetDSLAccountExpenses();
            ddlAccountExpenses.DataValueField = "pvalue";
            ddlAccountExpenses.DataTextField = "ptext";
            ddlAccountExpenses.DataBind();
        }
        else if (ddlChargeTo.SelectedValue.ToString() == "Rc Group")
        {
            trRc.Visible = true;
            trDueFrom.Visible = false;
            trOthers.Visible = false;

            ddlAccountExpenses.DataSource = clsFinanceAccountExpenses.GetDSLAccountExpenses(ddlRcCode.SelectedValue.ToString());
            ddlAccountExpenses.DataValueField = "pvalue";
            ddlAccountExpenses.DataTextField = "ptext";
            ddlAccountExpenses.DataBind();
        }
        else if (ddlChargeTo.SelectedValue.ToString() == "Others")
        {
            trRc.Visible = false;
            trDueFrom.Visible = false;
            trOthers.Visible = true;

            ddlAccountExpenses.DataSource = clsFinanceAccountExpenses.GetDSLAccountExpenses();
            ddlAccountExpenses.DataValueField = "pvalue";
            ddlAccountExpenses.DataTextField = "ptext";
            ddlAccountExpenses.DataBind();
        }
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {

        try
        {
            if (dgItems.Items.Count <= 13)
            {
                DataTable tblCart = ViewState["Cart"] as DataTable;
                DataRow drowCart = tblCart.NewRow();
                double dblAmount = Convert.ToDouble(txtAmount.Text);
                drowCart["aexpcode"] = ddlAccountExpenses.SelectedValue.ToString();
                drowCart["schlcode"] = (ddlChargeTo.SelectedValue == "School") ? ddlSchool.SelectedValue.ToString() : "";
                drowCart["rccode"] = (ddlChargeTo.SelectedValue == "Rc Group") ? ddlRcCode.SelectedValue.ToString() : "";
                drowCart["others"] = (ddlChargeTo.SelectedValue == "Others") ? txtOthers.Text.Trim().ToString() : "";
                drowCart["amount"] = string.Format("{0:0,0.00}", dblAmount);
                tblCart.Rows.Add(drowCart);

                //display in datagrid
                DataTable tblCartdg = ViewState["Cartdg"] as DataTable;
                DataRow drowCartdg = tblCartdg.NewRow();
                drowCartdg["aexpname"] = clsFinanceAccountExpenses.GetAccountExpenseName(ddlAccountExpenses.SelectedValue.ToString());
                string strChargedTo = "";

                if (ddlChargeTo.SelectedValue == "School")
                //{ strChargedTo = clsFinanceAccountDueFrom.GetDueFromName(ddlSchool.SelectedValue.ToString()); }
                { strChargedTo = clsFinanceAccountDueFrom.GetDueFromName(ddlSchool.SelectedValue.ToString()); }

                if (ddlChargeTo.SelectedValue == "Rc Group")
                { strChargedTo = clsRC.GetRCName(ddlRcCode.SelectedValue.ToString()); }

                if (ddlChargeTo.SelectedValue == "Others")
                { strChargedTo = txtOthers.Text.Trim(); }

                drowCartdg["chargeto"] = strChargedTo;
                drowCartdg["amount"] = string.Format("{0:0,0.00}", dblAmount);
                tblCartdg.Rows.Add(drowCartdg);
                dgItems.DataSource = tblCartdg;
                dgItems.DataBind();

                //add to temporary memory
                ViewState["Cart"] = tblCart;
                ViewState["Cartdg"] = tblCartdg;

                //txtItemDescription.Text = "";
                txtAmount.Text = "";


                //added 2013-09-02
                double dblCartSum = 0;
                DataTable tblCartSUm = ViewState["Cart"] as DataTable;
                foreach (DataRow drw in tblCartSUm.Rows)
                {
                    using (clsPCASRequestDetails objRequestDetails = new clsPCASRequestDetails())
                    {
                        dblCartSum = dblCartSum + Convert.ToDouble(drw["amount"]);
                    }
                }
                //txtRequestAmount.Text = string.Format("{0:0,0.00}", dblCartSum);
                txtRequestAmount.Text = string.Format("{0:0.00}", dblCartSum);
            }
            else
            {
                divError.Visible = true;
                lblErrMsg.Text = "Unable to add item to request list.<br>" +
                                 "<table>" +
                                  "<tr>" +
                                   "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                                   "<td>A maximum of <b>thirteen(13)</b> items can be added in request list.</td>" +
                                  "</tr>" +
                                 "</table>";
                return;
            }

            try
            {
                txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
            }
            catch { }
            try
            {
                //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
            }
            catch { }
        }
        catch
        {
            Response.Redirect("PettyCashRequestDetailsApprover.aspx");
        }


    }
    protected void btnSaveAddOther_Click(object sender, EventArgs e)
    {

    }
    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            DataTable tblCartdg = ViewState["Cartdg"] as DataTable;
            tblCart.Rows[e.Item.ItemIndex].Delete();
            tblCartdg.Rows[e.Item.ItemIndex].Delete();
            tblCart.AcceptChanges();
            tblCartdg.AcceptChanges();
            ViewState["Cartdg"] = tblCartdg;
            ViewState["Cart"] = tblCart;

            dgItems.DataSource = tblCartdg;
            dgItems.DataBind();


            //added 2013-09-02
            double dblCartSum = 0;
            DataTable tblCartSUm = ViewState["Cart"] as DataTable;
            foreach (DataRow drw in tblCartSUm.Rows)
            {
                using (clsPCASRequestDetails objRequestDetails = new clsPCASRequestDetails())
                {
                    dblCartSum = dblCartSum + Convert.ToDouble(drw["amount"]);
                }
            }
            //txtRequestAmount.Text = string.Format("{0:0,0.00}", dblCartSum);
            txtRequestAmount.Text = string.Format("{0:0.00}", dblCartSum);


            //trNoRequest.Visible = dgItems.Items.Count == 0;

            try
            {
                txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
            }
            catch { }
            try
            {
                //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
            }
            catch { }
        }
        catch
        {
            Response.Redirect("PettyCashRequestCashierMenu.aspx");
        }
    }

    protected void ddlRcCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        ddlAccountExpenses.DataSource = clsFinanceAccountExpenses.GetDSLAccountExpenses(ddlRcCode.SelectedValue.ToString());
        ddlAccountExpenses.DataValueField = "pvalue";
        ddlAccountExpenses.DataTextField = "ptext";
        ddlAccountExpenses.DataBind();
        //}

    }
    protected void ddlAccountExpenses_SelectedIndexChanged(object sender, EventArgs e)
    {
        //hdnAccountExpense.Value = ddlRcCode.SelectedValue.ToString();
    }

    protected bool ValidateAmount()
    {
        bool blnReturn = false;

        double dblCartSumValidator = 0;
        DataTable tblCartSUmValidator = ViewState["Cart"] as DataTable;
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

    protected void btnSaveAndApprove_Click(object sender, EventArgs e)
    {
        if (ValidateAmount() == false)
        {

        }
        else
        {

            string strApproverType = clsPCASApproval.GetApproverType(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["pcascode"].ToString());


            using (clsPCASRequestAllocation objDELRequestAllocation = new clsPCASRequestAllocation())
            {
                objDELRequestAllocation.PCascode = Request.QueryString["pcascode"].ToString();
                objDELRequestAllocation.Delete();
            }

            DataTable tblCart1 = ViewState["Cart"] as DataTable;

            foreach (DataRow drw in tblCart1.Rows)
            {
                using (clsPCASRequestAllocation objRequestAllocation = new clsPCASRequestAllocation())
                {
                    objRequestAllocation.PCascode = Request.QueryString["pcascode"].ToString();
                    objRequestAllocation.AccountExpenseCode = drw["aexpcode"].ToString();
                    objRequestAllocation.RCCode = drw["rccode"].ToString();
                    objRequestAllocation.Schoolcode = drw["schlcode"].ToString();
                    objRequestAllocation.Others = drw["others"].ToString();
                    objRequestAllocation.Amount = Convert.ToDouble(drw["amount"]);
                    objRequestAllocation.Insert();
                }
            }

            if (clsPCASApproval.TagApprovedOrNot(Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), "1") > 0)
            {
                try
                {
                    clsPCASRequest.SendEmailNotification("FPCApproved", Request.QueryString["pcascode"].ToString(), Request.Cookies["Speedo"]["UserName"].ToString(), "");
                    if ((strApproverType == "F1") || (strApproverType == "F2") || (strApproverType == "F3"))
                    {
                        clsPCASRequest.UpdateFPCData(Request.QueryString["pcascode"].ToString(), Convert.ToDouble(txtApprovedRFA.Text), Convert.ToDouble(txtAmountAllocated.Text), Convert.ToDouble(txtNet.Text), Convert.ToDouble(txtRequestAmount.Text), Convert.ToDouble(txtRemainingBudget.Text));
                    }

                    if ((strApproverType == "E") || (strApproverType == "H") || (strApproverType == "D") || (strApproverType == "F3"))
                    {
                        if ((strApproverType == "F3"))
                        {
                            clsPCASRequest.Approve(Request.QueryString["pcascode"].ToString());
                            clsPCASRequest.UpdateAdjustment(Request.QueryString["pcascode"].ToString(), clsPCASRequestAllocation.GetAmount(Request.QueryString["pcascode"].ToString()) - clsPCASRequestDetails.GetAmount(Request.QueryString["pcascode"].ToString()));
                            clsPCASRequest.SendEmailNotification("RequestorFinalFPC", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), "");
                            clsPCASRequest.SendEmailNotification("CashierApprover", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), ddlCustodian.SelectedValue.ToString());

                            foreach (DataRow drw in clsPCASRequest.GetDSGMainFormApproverPerRC(Request.QueryString["pcascode"].ToString()).Rows)
                            {
                                string pTable;
                                pTable = "<TABLE border='1'>" +
                                        "<TR><TD class='GirdRows'>Account Expenses</TD><TD class='GirdRows'>Amount</TD></TR>";
                                foreach (DataRow drw1 in clsFinanceAccountExpenses.GetDSGMainFormExpensesAmount(Request.QueryString["pcascode"].ToString(), drw["rccode"].ToString()).Rows)
                                {
                                    pTable = pTable +
                                        "<TR><TD class='GirdRows'>" + drw1["aexpname"] + "</TD><TD class='GirdRows'>" + drw1["amount"] + "</TD></TR>";
                                }
                                pTable = pTable + "</TABLE>";
                                clsPCASRequest.SendEmailNotificationAdjustment("Adjustment4Heads", clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), drw["username"].ToString(), pTable);
                            }

                        }
                        else
                        {
                            // clsPCASRequest.UpdateAdjustment(Request.QueryString["pcascode"].ToString(), clsPCASRequestAllocation.GetAmount(Request.QueryString["pcascode"].ToString()) - clsPCASRequestDetails.GetAmount(Request.QueryString["pcascode"].ToString()));
                            clsPCASRequest.SendEmailNotification("Requestor", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), clsPCASApproval.GetNextApproverUserName(Request.QueryString["pcascode"].ToString()));

                        }
                    }
                    else {
                        clsPCASApproval.UpdateFPC3Approver(Request.QueryString["pcascode"].ToString(), ddlFPCFinalApprover.SelectedValue.ToString());
                    }
                    using (clsPCASRequestCustodian objRequestCustodian = new clsPCASRequestCustodian())
                    {
                        objRequestCustodian.PCascode = Request.QueryString["pcascode"].ToString();
                        objRequestCustodian.Delete();
                        objRequestCustodian.PCascode = Request.QueryString["pcascode"].ToString();
                        objRequestCustodian.Username = ddlCustodian.SelectedValue.ToString();
                        objRequestCustodian.Insert();
                    }
                }
                catch { }

                try
                {
                    txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
                }
                catch { }
                try
                {
                    //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
                }
                catch { }

                clsPCASRequest.SendEmailNotification("Approver", Request.QueryString["pcascode"].ToString(), clsPCASRequest.GetCreatedBy(Request.QueryString["pcascode"].ToString()), clsPCASApproval.GetNextApproverUserName(Request.QueryString["pcascode"].ToString()));
                Response.Redirect("PettyCashRequestMenu.aspx");

            }
        }
    }
    protected void txtAmountAllocated_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
        }
        catch { }
        try
        {
            //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
        }
        catch { }
    }
    protected void txtAmountAllocated_Unload(object sender, EventArgs e)
    {
        try
        {
            txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
        }
        catch { }
        try
        {
            //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
        }
        catch { }
    }
    protected void txtApprovedRFA_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
        }
        catch { }
        try
        {
            //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
        }
        catch { }
    }
    protected void txtApprovedRFA_Unload(object sender, EventArgs e)
    {
        try
        {
            txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
        }
        catch { }
        try
        {
            //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
        }
        catch { }
    }
    protected void txtRequestAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
        }
        catch { }
        try
        {
            //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
        }
        catch { }
    }
    protected void txtRequestAmount_Unload(object sender, EventArgs e)
    {
        try
        {
            txtNet.Text = (Convert.ToDouble(txtApprovedRFA.Text) - Convert.ToDouble(txtAmountAllocated.Text)).ToString();
        }
        catch { }
        try
        {
            //txtRemainingBudget.Text = (Convert.ToDouble(txtNet.Text) - Convert.ToDouble(txtRequestAmount.Text)).ToString();
        }
        catch { }
    }
}