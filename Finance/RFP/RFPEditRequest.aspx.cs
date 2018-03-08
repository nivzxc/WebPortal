using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Finance_RFP_RFPEditRequest : System.Web.UI.Page
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
        //ddlEndorsedBy1.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy1.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1");
        ddlEndorsedBy1.DataValueField = "pvalue";
        ddlEndorsedBy1.DataTextField = "ptext";
        ddlEndorsedBy1.DataBind();
        ddlEndorsedBy1.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy1.SelectedIndex = 0;

        ddlEndorsedBy2.Items.Clear();
        //ddlEndorsedBy2.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy2.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1"); ;
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

    }

    protected void LoadDetails()
    {
        DataTable tblDetails = clsRFPRequestDetails.GetDSG(Request.QueryString["ControlNumber"]);
        ViewState["Cart"] = tblDetails;

        DataTable tblCartdg = ViewState["Cartdg"] as DataTable;

        foreach (DataRow drw in tblDetails.Rows)
        {
            DataRow drowCartdg = tblCartdg.NewRow();
            drowCartdg["itemdesc"] = drw["itemdesc"];
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
    }

    protected void LoadRFPInfo()
    {
        using (clsRFPRequest objRFPRequest = new clsRFPRequest())
        {
            objRFPRequest.ControlNumber = Request.QueryString["ControlNumber"];
            objRFPRequest.Fill();
            txtControlNumber.Text = objRFPRequest.ControlNumber;
            txtRequestor.Text = objRFPRequest.PayeeName;
            ddlRequestType.SelectedValue = objRFPRequest.RequestCode;
            txtIntended.Text = objRFPRequest.RequestFor;
            txtProjectTitle.Text = objRFPRequest.ProjectTitle;
            txtRFANumber.Text = objRFPRequest.RFANumber.Trim();
            dtpDateNeeded.Date = objRFPRequest.DateNeeded;
            txtSupportingDocuments.Text = objRFPRequest.SupportingDoument;
            txtRemarks.Text = objRFPRequest.Remarks;

            ddlEndorsedBy1.SelectedValue = (objRFPRequest.EndorsedBy1 != "") ? objRFPRequest.EndorsedBy1 : "";
            if (objRFPRequest.EndorsedBy2.Trim() != "")
            {
                trEndorseBy2.Visible = true;
                btbAddEndorser2.Visible = false;
                ddlEndorsedBy2.SelectedValue = objRFPRequest.EndorsedBy2;
            }
            ddlAuthorized.SelectedValue = objRFPRequest.AuthorizedBy;
            LoadDetails();
        }
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
        if (!Page.IsPostBack)
        {
            MakeCart();
            LoadDDLs();
            
            using (clsRFPRequest objRFPRequest = new clsRFPRequest())
            {
                objRFPRequest.ControlNumber = Request.QueryString["ControlNumber"];
                objRFPRequest.Fill();

                if (objRFPRequest.Status != "2")
                { Response.Redirect("../../AccessDenied.aspx"); }

                if (objRFPRequest.EndorsedBy1.Trim() != string.Empty && objRFPRequest.EndorsedBy2.Trim() == string.Empty)
                {
                    if (objRFPRequest.EndorsedStatus1.Trim() == "2")
                    { LoadRFPInfo(); }
                    else
                    { Response.Redirect("../../AccessDenied.aspx"); }
                }

                if (objRFPRequest.EndorsedBy1.Trim() != string.Empty && objRFPRequest.EndorsedBy2.Trim() != string.Empty)
                {
                    if (objRFPRequest.EndorsedStatus1.Trim() == "2" && objRFPRequest.EndorsedStatus2.Trim() == "2")
                    { LoadRFPInfo(); }
                    else
                    { Response.Redirect("../../AccessDenied.aspx"); }
                }
                if (objRFPRequest.EndorsedBy1.Trim() == string.Empty && objRFPRequest.EndorsedBy2.Trim() == string.Empty)
                {
                    if (objRFPRequest.AuthorizeStatus.Trim() == "2")
                    { LoadRFPInfo(); }
                    else
                    { Response.Redirect("../../AccessDenied.aspx"); }
                }
            }
            ValidateRequestForField();
        }
    }

    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCarts = ViewState["Cart"] as DataTable;
            DataTable tblCartdg = ViewState["Cartdg"] as DataTable;
            tblCarts.Rows[e.Item.ItemIndex].Delete();
            tblCartdg.Rows[e.Item.ItemIndex].Delete();
            tblCarts.AcceptChanges();
            tblCartdg.AcceptChanges();
            ViewState["Cartdg"] = tblCartdg;
            ViewState["Cart"] = tblCarts;

            dgItems.DataSource = tblCartdg;
            dgItems.DataBind();

            trNoRequest.Visible = dgItems.Items.Count == 0;
        }
        catch
        {
            Response.Redirect("RFPNewRequest.aspx");
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
                double dblAmount = 0;
                string strItemDescription = "";
                //dblAmount = Convert.ToDouble(txtAmount.Text);
                if (ddlChargeTo.SelectedValue == "Others")
                {
                    dblAmount = Convert.ToDouble(txtAmountOthers.Text);
                    strItemDescription = txtItemDescriptionOther.Text.Trim();
                }
                else
                {
                    dblAmount = Convert.ToDouble(txtAmount.Text);
                    strItemDescription = txtItemDescription.Text.Trim();
                }
                drowCart["itemdesc"] = strItemDescription;
                drowCart["schlcode"] = (ddlChargeTo.SelectedValue == "School") ? ddlSchool.SelectedValue.ToString() : "";
                drowCart["rccode"] = (ddlChargeTo.SelectedValue == "Rc Group") ? ddlRcCode.SelectedValue.ToString() : "";
                drowCart["others"] = (ddlChargeTo.SelectedValue == "Others") ? txtOthers.Text.Trim().ToString() : "";
                drowCart["amount"] = string.Format("{0:0,0.00}", dblAmount);
                tblCart.Rows.Add(drowCart);

                //display in datagrid
                DataTable tblCartdg = ViewState["Cartdg"] as DataTable;
                DataRow drowCartdg = tblCartdg.NewRow();
                drowCartdg["itemdesc"] = strItemDescription;
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
            financerequest.ControlNumber = Request.QueryString["ControlNumber"];
            financerequest.RequestCode = ddlRequestType.SelectedValue.ToString().Trim();
            financerequest.RequestFor = GetRequestFor();
            financerequest.ProjectTitle = txtProjectTitle.Text.Trim();
            financerequest.DateNeeded = dtpDateNeeded.Date;
            financerequest.RFANumber = txtRFANumber.Text.Trim();

            financerequest.PayeeName = txtRequestor.Text;

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

            if (financerequest.Update(tblItems, pSaveType) >= 0)
            {

                if (pSaveType == "SAVE")
                { Response.Redirect("RFPMenu.aspx"); }
                else
                {
                    Response.Redirect("RFPPrint.aspx?ControlNumber=" + txtControlNumber.Text + "");
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

    protected void ddlEndorsedBy1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlEndorsedBy2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btbAddEndorser2_Click(object sender, EventArgs e)
    {
        trEndorseBy2.Visible = true;
        btbAddEndorser2.Visible = false;
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
                string strItemDescription = "";
                //dblAmount = Convert.ToDouble(txtAmount.Text);
                if (ddlChargeTo.SelectedValue == "Others")
                {
                    dblAmount = Convert.ToDouble(txtAmountOthers.Text);
                    strItemDescription = txtItemDescriptionOther.Text.Trim();
                }
                else
                {
                    dblAmount = Convert.ToDouble(txtAmount.Text);
                    strItemDescription = txtItemDescription.Text.Trim();
                }
                drowCart["itemdesc"] = strItemDescription;
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
        btbAddEndorser2.Visible = false;
    }
    protected void btnAddEndorser3_Click(object sender, ImageClickEventArgs e)
    {
        trEndorseBy2.Visible = false;
        btbAddEndorser2.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFPMenu.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        using (clsRFPRequest objRequest = new clsRFPRequest())
        {
            objRequest.ControlNumber = Request.QueryString["ControlNumber"];
            objRequest.Fill();
            if (objRequest.Status == "2")
            {
                if (clsRFPRequest.Cancel(objRequest.ControlNumber) > 0)
                {
                    Response.Redirect("RFPMenu.aspx");
                }
            }
        }
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
