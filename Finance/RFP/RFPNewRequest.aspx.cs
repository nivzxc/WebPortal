using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Finance_RFP_RFPNewRequest : System.Web.UI.Page
{
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart.Columns.Add("schlcode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("rccode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("others", System.Type.GetType("System.String"));
        tblCart.Columns.Add("amount", System.Type.GetType("System.String"));
        ViewState["Cart"] = tblCart;

        DataTable tblCartdg = new DataTable("Cartdg");
        tblCartdg.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCartdg.Columns.Add("chargeto", System.Type.GetType("System.String"));
        tblCartdg.Columns.Add("amount", System.Type.GetType("System.String"));
        ViewState["Cartdg"] = tblCartdg;
    }

    protected bool ValidateApprovers()
    {
        bool blnReturn = true;
        if (trEndorseBy2.Visible == true)
        {
            if (ddlAuthorized.SelectedValue == ddlEndorsedBy1.SelectedValue || ddlAuthorized.SelectedValue == ddlEndorsedBy2.SelectedValue)
            {
                blnReturn = false;
            }
            else if ((ddlEndorsedBy1.SelectedValue == ddlAuthorized.SelectedValue || ddlEndorsedBy1.SelectedValue == ddlEndorsedBy2.SelectedValue))
            {
                blnReturn = false;
            }
        }
        else
        {
            if (ddlAuthorized.SelectedValue == ddlEndorsedBy1.SelectedValue)
            {
                blnReturn = false;
            }
        }
        return blnReturn;
    }

    protected void LoadExecutiveDDLs()
    {

        ddlEndorsedBy1.Items.Clear();
        //ddlEndorsedBy1.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy1.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1");
        ddlEndorsedBy1.DataValueField = "pvalue";
        ddlEndorsedBy1.DataTextField = "ptext";
        ddlEndorsedBy1.DataBind();
        ddlEndorsedBy1.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy1.SelectedIndex = 0;

        ddlEndorsedBy2.Items.Clear();
        //ddlEndorsedBy2.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy1.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1");
        ddlEndorsedBy2.DataValueField = "pValue";
        ddlEndorsedBy2.DataTextField = "pText";
        ddlEndorsedBy2.DataBind();
        ddlEndorsedBy2.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy2.SelectedIndex = 0;

        ddlAuthorized.Items.Clear();
        //ddlAuthorized.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlAuthorized.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "2");
        ddlAuthorized.DataValueField = "pValue";
        ddlAuthorized.DataTextField = "pText";
        ddlAuthorized.DataBind();

        using (clsEmployee employee = new clsEmployee())
        {
            employee.Username = ddlExecutive.SelectedValue;
            employee.Fill();
            ddlAuthorized.SelectedValue = clsDivision.GetDivisionHead(employee.DivisionCode);
        }
    }

    protected void LoadDDLs()
    {
        ddlRequestType.Items.Clear();
        ddlRequestType.DataSource = clsRFPRequestType.GetDSL();
        ddlRequestType.DataValueField = "pValue";
        ddlRequestType.DataTextField = "pText";
        ddlRequestType.DataBind();

        ddlSchool.Items.Clear();
        ddlSchool.DataSource = clsSchool.GetSchoolCMHQOwned();
        ddlSchool.DataValueField = "pValue";
        ddlSchool.DataTextField = "pText";
        ddlSchool.DataBind();

        ddlRcCode.Items.Clear();
        ddlRcCode.DataSource = clsRC.GetDdlDs();
        ddlRcCode.DataValueField = "pValue";
        ddlRcCode.DataTextField = "pText";
        ddlRcCode.DataBind();
        dtpDateNeeded.MinDate = DateTime.Now.AddDays(-1);

        ddlEndorsedBy1.Items.Clear();
        ddlEndorsedBy1.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1");
        ddlEndorsedBy1.DataValueField = "pvalue";
        ddlEndorsedBy1.DataTextField = "ptext";
        ddlEndorsedBy1.DataBind();
        ddlEndorsedBy1.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy1.SelectedIndex = 0;

        ddlEndorsedBy2.Items.Clear();
        ddlEndorsedBy2.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1");
        ddlEndorsedBy2.DataValueField = "pValue";
        ddlEndorsedBy2.DataTextField = "pText";
        ddlEndorsedBy2.DataBind();
        ddlEndorsedBy2.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy2.SelectedIndex = 0;

        ddlAuthorized.Items.Clear();
        ddlAuthorized.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "2"); ;
        ddlAuthorized.DataValueField = "pValue";
        ddlAuthorized.DataTextField = "pText";
        ddlAuthorized.DataBind();

        ddlExecutive.Items.Clear();
        ddlExecutive.DataSource = clsEmployee.DSLExecutive();
        ddlExecutive.DataValueField = "pValue";
        ddlExecutive.DataTextField = "pText";
        ddlExecutive.DataBind();

    }

    //Include by Charlie Bachiller
    //2-7-2013
    //If request type is others, hide unhide txtintendedFor
    void ValidateRequestForField()
    {
        if (ddlRequestType.SelectedValue.ToString() == "08")
        {
            trRequestFor.Visible = true;
        }
        else
        {
            trRequestFor.Visible = false;
        }
    }

    protected string GetRequestFor()
    {
        string strReturn = "";
        if (ddlRequestType.SelectedValue.ToString() == "08")
        {
            strReturn = txtIntended.Text;
        }
        else
        {
            strReturn = "";
        }
        return strReturn;
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
       // btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
        btnPrint.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnPrint, null) + ";");
        if (!Page.IsPostBack)
        {
            trNoRequest.Visible = true;
            MakeCart();
            LoadDDLs();
            ValidateRequestForField();
            using (clsEmployee employee = new clsEmployee())
            {
                employee.Username = Request.Cookies["Speedo"]["UserName"];
                employee.Fill();
                if (employee.RcCode == "AUD")
                {
                    ddlAuthorized.SelectedValue = "jun.sagcal";
                }
                else
                { ddlAuthorized.SelectedValue = clsDivision.GetDivisionHead(employee.DivisionCode); }
            }
            txtRequestor.Text = clsEmployee.GetName(Request.Cookies["Speedo"]["UserName"], EmployeeNameFormat.FirstLast);
            ddlEndorsedBy1.SelectedValue = clsModuleApprover.ApproverEmployee(Request.Cookies["Speedo"]["UserName"], clsModule.ATWModule, "1");
            //btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString());
            //btnSend.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSend).ToString());

        }
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
                drowCart["itemdesc"] = txtItemDescription.Text.Trim();
                drowCart["schlcode"] = (ddlChargeTo.SelectedValue == "School") ? ddlSchool.SelectedValue.ToString() : "";
                drowCart["rccode"] = (ddlChargeTo.SelectedValue == "Rc Group") ? ddlRcCode.SelectedValue.ToString() : "";
                drowCart["others"] = (ddlChargeTo.SelectedValue == "Others") ? txtOthers.Text.Trim().ToString() : "";
                drowCart["amount"] = string.Format("{0:0,0.00}", dblAmount);
                tblCart.Rows.Add(drowCart);

                //display in datagrid
                DataTable tblCartdg = ViewState["Cartdg"] as DataTable;
                DataRow drowCartdg = tblCartdg.NewRow();
                drowCartdg["itemdesc"] = txtItemDescription.Text.Trim();
                string strChargedTo = "";

                if (ddlChargeTo.SelectedValue == "School")
                { strChargedTo = clsSchool.GetSchoolName(ddlSchool.SelectedValue.ToString()); }

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

                txtItemDescription.Text = "";
                txtAmount.Text = "";

                trNoRequest.Visible = dgItems.Items.Count == 0;
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
        }
        catch
        {
            Response.Redirect("RFPNewRequest.aspx");
        }


    }

    protected void SaveData(string pSaveType)
    {
        string strPayee = "";
        if (ddlPayee.SelectedValue == "Others")
        {
            if (txtPayeeOthers.Text.Trim() == string.Empty)
            {
                divError.Visible = true;
                lblErrMsg.Text = "Unable to send your request.<br>" +
                                 "<table>" +
                                  "<tr>" +
                                   "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                                   "<td>You need to specify the name of payee.</td>" +
                                  "</tr>" +
                                 "</table>";
                return;
            }
        }

        if (ddlRequestType.SelectedValue == "08")
        {
            if (txtIntended.Text.Trim() == string.Empty)
            {
                divError.Visible = true;
                lblErrMsg.Text = "Unable to send your request.<br>" +
                                 "<table>" +
                                  "<tr>" +
                                   "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                                   "<td>You need to specify the <b>Request For</b> is you choose the request type: <b>Others</b>.</td>" +
                                  "</tr>" +
                                 "</table>";
                return;
            }
        }

        if (!this.ValidateApprovers())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Unable to send your request.<br>" +
                             "<table>" +
                              "<tr>" +
                               "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                               "<td>You need to select your respective approver only <b>Once</b>.</td>" +
                              "</tr>" +
                             "</table>";
            return;
        }


        if (dgItems.Items.Count == 0)
        {
            divError.Visible = true;
            lblErrMsg.Text = "Unable to send your request.<br>" +
                             "<table>" +
                              "<tr>" +
                               "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                               "<td>You need to include at least one item to request. Make sure to click <b>Add New Item</b> button to include your requested item.</td>" +
                              "</tr>" +
                             "</table>";
            return;
        }

        if (txtProjectTitle.Text.Length <= 0)
        {
            divError.Visible = true;
            lblErrMsg.Text = "Unable to send your request.<br>" +
                             "<table>" +
                              "<tr>" +
                               "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                               "<td>You need to fill up the <b>Project Title</b> field to continue.</td>" +
                              "</tr>" +
                             "</table>";
            return;
        }
        else
        {
            DataTable tblItems = ViewState["Cart"] as DataTable;
            clsRFPRequest financerequest = new clsRFPRequest();
            financerequest.RequestCode = ddlRequestType.SelectedValue.ToString().Trim();
            financerequest.RequestFor = GetRequestFor();
            financerequest.ProjectTitle = txtProjectTitle.Text.Trim();
            financerequest.DateNeeded = dtpDateNeeded.Date;
            financerequest.RFANumber = txtRFANumber.Text.Trim();

            if (ddlPayee.SelectedValue == "Self")
            { strPayee = txtRequestor.Text; }
            else if (ddlPayee.SelectedValue == "Executive")
            { strPayee = ddlExecutive.SelectedItem.Text; }
            else if (ddlPayee.SelectedValue == "Others")
            { strPayee = txtPayeeOthers.Text.Trim(); }

            financerequest.PayeeName = strPayee;
            financerequest.SupportingDoument = txtSupportingDocuments.Text.Trim();
            financerequest.Remarks = txtRemarks.Text.Trim();

            if (trEndorseBy2.Visible == true)
            {
                //validating Endorsers
                if (ddlEndorsedBy1.SelectedValue == "" && ddlEndorsedBy2.SelectedValue != "")
                {
                    financerequest.EndorsedBy1 = ddlEndorsedBy2.SelectedValue;
                    financerequest.EndorsedDate1 = DateTime.Parse("1/1/1990");
                    financerequest.EndorsedStatus1 = (ddlEndorsedBy2.SelectedValue != "") ? "2" : "";

                    financerequest.EndorsedBy2 = "";
                    financerequest.EndorsedDate2 = DateTime.Parse("1/1/1990");
                    financerequest.EndorsedStatus2 = "";
                }

                else
                {
                    financerequest.EndorsedBy1 = (ddlEndorsedBy1.SelectedValue != "") ? ddlEndorsedBy1.SelectedValue : "";
                    financerequest.EndorsedDate1 = DateTime.Parse("1/1/1990");
                    financerequest.EndorsedStatus1 = (ddlEndorsedBy1.SelectedValue != "") ? "2" : "";

                    financerequest.EndorsedBy2 = (ddlEndorsedBy2.SelectedValue != "") ? ddlEndorsedBy2.SelectedValue : "";
                    financerequest.EndorsedDate2 = DateTime.Parse("1/1/1990");
                    financerequest.EndorsedStatus2 = (ddlEndorsedBy2.SelectedValue != "") ? "2" : "";
                }
            }
            else
            {
                financerequest.EndorsedBy1 = (ddlEndorsedBy1.SelectedValue != "") ? ddlEndorsedBy1.SelectedValue : "";
                financerequest.EndorsedDate1 = DateTime.Parse("1/1/1990");
                financerequest.EndorsedStatus1 = (ddlEndorsedBy1.SelectedValue != "") ? "2" : "";

                financerequest.EndorsedBy2 = "";
                financerequest.EndorsedDate2 = DateTime.Parse("1/1/1990");
                financerequest.EndorsedStatus2 = "";
            }

            financerequest.AuthorizedBy = ddlAuthorized.SelectedValue;
            financerequest.AuthorizedByDate = DateTime.Parse("1/1/1990");
            financerequest.AuthorizeStatus = "2";
            financerequest.Status = "2";
            financerequest.CreatedBy = Request.Cookies["Speedo"]["UserName"];
            financerequest.CreatedOn = DateTime.Now;
            financerequest.ModifyBy = Request.Cookies["Speedo"]["UserName"];
            financerequest.ModifyOn = DateTime.Parse("1/1/1990");

            if (financerequest.Insert(tblItems, pSaveType) >= 0)
            {
                if (pSaveType == "SAVE")
                { Response.Redirect("RFPMenu.aspx"); }
                else
                {
                    Response.Redirect("RFPPrint.aspx?ControlNumber=" + clsRFPRequest.GetLatestControlNumber(Request.Cookies["Speedo"]["UserName"]) + "");
                }

            }
            else
            {
                divError.Visible = true;
                lblErrMsg.Text = "Unable to send your request.<br>" +
                                 "<table>" +
                                  "<tr>" +
                                   "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                                   "<td>An error occured uing saving. Please make sure you have filled out all the necessary fields.</td>" +
                                  "</tr>" +
                                 "</table>";
            }
        }


    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        SaveData("SAVE");
    }

    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            DataTable tblCartdg = ViewState["Cartdg"] as DataTable;
            tblCart.Rows[e.Item.ItemIndex].Delete();
            tblCartdg.Rows[e.Item.ItemIndex].Delete();
            ViewState["Cartdg"] = tblCartdg;
            ViewState["Cart"] = tblCart;

            dgItems.DataSource = tblCartdg;
            dgItems.DataBind();

            trNoRequest.Visible = dgItems.Items.Count == 0;
        }
        catch
        {
            Response.Redirect("RFPNewRequest.aspx");
        }
    }

    public static Boolean CheckDouble(string pEntry)
    {
        Boolean blnReturn;
        if (Convert.IsDBNull(pEntry) || pEntry == "")
            blnReturn = false;
        else
            blnReturn = true;
        return blnReturn;
    }

    protected void ddlEndorsedBy1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlPayee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayee.SelectedValue == "Self")
        {
            trPayeeExecutive.Visible = false;
            trPayeeOthers.Visible = false;
            trPayeeRequestor.Visible = true;
            LoadDDLs();
            using (clsEmployee employee = new clsEmployee())
            {
                employee.Username = Request.Cookies["Speedo"]["UserName"];
                employee.Fill();
                if (employee.Username != clsDivision.GetDivisionHead(employee.DivisionCode))
                {
                    ddlAuthorized.SelectedValue = clsDivision.GetDivisionHead(employee.DivisionCode);
                }
            }
            txtRequestor.Text = clsEmployee.GetName(Request.Cookies["Speedo"]["UserName"], EmployeeNameFormat.FirstLast);

            ddlEndorsedBy1.SelectedValue = clsModuleApprover.ApproverEmployee(Request.Cookies["Speedo"]["UserName"], clsModule.RFPModule, "1");
        }
        if (ddlPayee.SelectedValue == "Executive")
        {
            trPayeeExecutive.Visible = true;
            trPayeeOthers.Visible = false;
            trPayeeRequestor.Visible = false;
            LoadExecutiveDDLs();
            using (clsEmployee employee = new clsEmployee())
            {
                employee.Username = ddlExecutive.SelectedValue;
                employee.Fill();
                ddlAuthorized.SelectedValue = clsDivision.GetDivisionHead(employee.DivisionCode);
            }
            ddlEndorsedBy1.SelectedValue = clsModuleApprover.ApproverEmployee(ddlExecutive.SelectedValue, clsModule.RFPModule, "1");
        }
        if (ddlPayee.SelectedValue == "Others")
        {
            trPayeeExecutive.Visible = false;
            trPayeeOthers.Visible = true;
            trPayeeRequestor.Visible = false;
            LoadDDLs();
        }
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlRcCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlEndorsedBy2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlExecutive_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadExecutiveDDLs();
        using (clsEmployee employee = new clsEmployee())
        {
            employee.Username = ddlExecutive.SelectedValue;
            employee.Fill();
            ddlAuthorized.SelectedValue = clsDivision.GetDivisionHead(employee.DivisionCode);
        }
    }

    protected void ddlChargeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtItemDescription.Text = string.Empty;
        txtItemDescriptionOther.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtAmountOthers.Text = string.Empty;
        txtOthers.Text = string.Empty;
        ddlRcCode.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;

        if (ddlChargeTo.SelectedValue == "School")
        {
            trSchool.Visible = true;
            trOthers.Visible = false;
            trRc.Visible = false;
            btnSaveAdd.Visible = true;
            btnSaveAddOther.Visible = false;
            txtAmountOthers.Visible = false;
            txtItemDescriptionOther.Visible = false;
            txtAmount.Visible = true;
            txtItemDescription.Visible = true;
        }
        if (ddlChargeTo.SelectedValue == "Rc Group")
        {
            trSchool.Visible = false;
            trOthers.Visible = false;
            trRc.Visible = true;
            btnSaveAdd.Visible = true;
            btnSaveAddOther.Visible = false;
            txtAmountOthers.Visible = false;
            txtItemDescriptionOther.Visible = false;
            txtAmount.Visible = true;
            txtItemDescription.Visible = true;
        }
        if (ddlChargeTo.SelectedValue == "Others")
        {
            trSchool.Visible = false;
            trOthers.Visible = true;
            trRc.Visible = false;
            btnSaveAdd.Visible = false;
            btnSaveAddOther.Visible = true;
            txtAmountOthers.Visible = true;
            txtItemDescriptionOther.Visible = true;
            txtAmount.Visible = false;
            txtItemDescription.Visible = false;
        }
    }

    protected void btnSaveAddOther_Click(object sender, EventArgs e)
    {
        try
        {
            if (dgItems.Items.Count <= 13)
            {
                DataTable tblCart = ViewState["Cart"] as DataTable;
                DataRow drowCart = tblCart.NewRow();
                double dblAmount = 0;
                if (ddlChargeTo.SelectedValue == "Others")
                {
                    dblAmount = Convert.ToDouble(txtAmountOthers.Text);
                }
                else
                {
                    dblAmount = Convert.ToDouble(txtAmount.Text);
                }

                drowCart["itemdesc"] = txtItemDescriptionOther.Text.Trim();
                drowCart["schlcode"] = (ddlChargeTo.SelectedValue == "School") ? ddlSchool.SelectedValue.ToString() : "";
                drowCart["rccode"] = (ddlChargeTo.SelectedValue == "Rc Group") ? ddlRcCode.SelectedValue.ToString() : "";
                drowCart["others"] = (ddlChargeTo.SelectedValue == "Others") ? txtOthers.Text.Trim().ToString() : "";
                drowCart["amount"] = string.Format("{0:0,0.00}", dblAmount);
                tblCart.Rows.Add(drowCart);

                //display in datagrid
                DataTable tblCartdg = ViewState["Cartdg"] as DataTable;
                DataRow drowCartdg = tblCartdg.NewRow();
                drowCartdg["itemdesc"] = txtItemDescriptionOther.Text.Trim();
                string strChargedTo = "";

                if (ddlChargeTo.SelectedValue == "School")
                { strChargedTo = clsSchool.GetSchoolName(ddlSchool.SelectedValue.ToString()); }

                if (ddlChargeTo.SelectedValue == "Rc Group")
                { strChargedTo = clsRC.GetRCName(ddlRcCode.SelectedValue.ToString()); }

                if (ddlChargeTo.SelectedValue == "Others")
                { strChargedTo = txtOthers.Text.Trim(); }

                drowCartdg["chargeto"] = strChargedTo;
                //drowCartdg["amount"] = txtAmountOthers.Text;
                drowCartdg["amount"] = string.Format("{0:0,0.00}", dblAmount);
                tblCartdg.Rows.Add(drowCartdg);
                dgItems.DataSource = tblCartdg;
                dgItems.DataBind();

                //add to temporary memory
                ViewState["Cart"] = tblCart;
                ViewState["Cartdg"] = tblCartdg;

                txtItemDescriptionOther.Text = "";
                txtAmountOthers.Text = "";
                txtOthers.Text = "";

                trNoRequest.Visible = dgItems.Items.Count == 0;
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
        }
        catch
        {
            Response.Redirect("RFPNewRequest.aspx");
        }
    }

    protected void btnAddEndorser2_Click(object sender, ImageClickEventArgs e)
    {
        trEndorseBy2.Visible = true;
        btnAddEndorser2.Visible = false;

    }

    protected void btnAddEndorser3_Click(object sender, ImageClickEventArgs e)
    {
        trEndorseBy2.Visible = false;
        btnAddEndorser2.Visible = true;

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        SaveData("PRINT");
    }


    protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidateRequestForField();
    }
}
