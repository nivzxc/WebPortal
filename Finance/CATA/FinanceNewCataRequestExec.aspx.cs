using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Finance_CATA_FinanceNewCataRequestExec : System.Web.UI.Page
{
    protected Boolean ValidateDate()
    {
        DateTime dt1 = dtDateDeparture.SelectedDate;
        DateTime dt2 = dtDateCheckNeeded.SelectedDate;
        DateTime dtNow = DateTime.Now.Date;
        TimeSpan t1 = dt2.Subtract(dt1);

        if (dt1 < dtNow)
        {
            divError.Visible = true;
            lblErrMsg.Text = "Invalid date of departure";
            lblErrMsg.Focus();
            return false;
        }

        else if (t1.Days > 0)
        {
            divError.Visible = true;
            lblErrMsg.Text = "Invalid date check needed from date of departure";
            lblErrMsg.Focus();
            return false;
        }
        else
        {
            divError.Visible = false;
            return true;
        }
    }

    protected Boolean ValidateApprovers()
    {

        if (ddlEndorsedBy1.SelectedValue.ToString() != string.Empty && ddlDivisionHead.SelectedValue.ToString() != string.Empty)
        {
            if (ddlEndorsedBy1.SelectedValue.ToString() == ddlDivisionHead.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlEndorsedBy1.SelectedValue.ToString() != string.Empty && ddlEndorsedBy2.SelectedValue.ToString() != string.Empty)
        {
            if (ddlEndorsedBy1.SelectedValue.ToString() == ddlEndorsedBy2.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlEndorsedBy1.SelectedValue.ToString() != string.Empty && ddlAuthorizeby2.SelectedValue.ToString() != string.Empty)
        {
            if (ddlEndorsedBy1.SelectedValue.ToString() == ddlAuthorizeby2.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlEndorsedBy2.SelectedValue.ToString() != string.Empty && ddlDivisionHead.SelectedValue.ToString() != string.Empty)
        {
            if (ddlEndorsedBy2.SelectedValue.ToString() == ddlDivisionHead.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlEndorsedBy2.SelectedValue.ToString() != string.Empty && ddlEndorsedBy1.SelectedValue.ToString() != string.Empty)
        {
            if (ddlEndorsedBy2.SelectedValue.ToString() == ddlEndorsedBy1.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlEndorsedBy2.SelectedValue.ToString() != string.Empty && ddlAuthorizeby2.SelectedValue.ToString() != string.Empty)
        {
            if (ddlEndorsedBy2.SelectedValue.ToString() == ddlAuthorizeby2.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlAuthorizeby2.SelectedValue.ToString() != string.Empty && ddlDivisionHead.SelectedValue.ToString() != string.Empty)
        {
            if (ddlAuthorizeby2.SelectedValue == ddlDivisionHead.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlAuthorizeby2.SelectedValue.ToString() != string.Empty && ddlEndorsedBy1.SelectedValue.ToString() != string.Empty)
        {
            if (ddlAuthorizeby2.SelectedValue.ToString() == ddlEndorsedBy1.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlAuthorizeby2.SelectedValue.ToString() != string.Empty && ddlEndorsedBy2.SelectedValue.ToString() != string.Empty)
        {
            if (ddlAuthorizeby2.SelectedValue.ToString() == ddlEndorsedBy2.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        return true;
    }

    protected void CATASum()
    {
        try
        {
            double dblTotalTranspoAllowance = 0.00;

            if ((txtLandBus.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtLandBus.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandBus.Text.Trim()); }

            if ((txtLandGasAllowance.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtLandGasAllowance.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandGasAllowance.Text.Trim()); }

            if ((txtLandVHire.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtLandVHire.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandVHire.Text.Trim()); }

            if ((txtLandTollFee.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtLandTollFee.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandTollFee.Text.Trim()); }

            if ((txtAirTerminalFee.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtAirTerminalFee.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtAirTerminalFee.Text.Trim()); }

            //if ((txtAirFixedTransportation.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtAirFixedTransportation.Text.Trim()) == 1)
            //{ dblTotalTranspoAllowance += Convert.ToDouble(txtAirFixedTransportation.Text.Trim()); }

            if ((txtSeaFerry.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtSeaFerry.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtSeaFerry.Text.Trim()); }

            if ((txtLandOther.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtLandOther.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandOther.Text.Trim()); }

            txtTransportationTotal.Text = string.Format("{0:0.00}", dblTotalTranspoAllowance);

            double dblTotalCATAAllowance = 0.00;

            if ((txtRepresentationAmount.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtRepresentationAmount.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtRepresentationAmount.Text.Trim()); }

            if ((txtIncedentalsTotal.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtIncedentalsTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtIncedentalsTotal.Text.Trim()); }

            if ((txtTransportationTotal.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtTransportationTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtTransportationTotal.Text.Trim()); }

            if ((txtTravelTotal.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtTravelTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtTravelTotal.Text.Trim()); }

            if ((txtAccomodationTotal.Text.Trim() != string.Empty) || clsValidator.CheckInteger(txtAccomodationTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtAccomodationTotal.Text.Trim()); }
            txtTotalCATAAmount.Text = string.Format("{0:0,0.00}", dblTotalCATAAllowance);
        }
        catch { }
    }

    protected void MakeCart()
    {
        DataTable tblCart1 = new DataTable("CartIncedentals");
        tblCart1.Columns.Add("incdental", System.Type.GetType("System.String"));
        tblCart1.Columns.Add("amount", System.Type.GetType("System.String"));
        ViewState["CartIncedentals"] = tblCart1;

        DataTable tblCart2 = new DataTable("CartRepresentation");
        tblCart2.Columns.Add("rprsnttn", System.Type.GetType("System.String"));
        ViewState["CartRepresentation"] = tblCart2;
    }

    private void DGAddIncidentals()
    {
        double dblIncidentalAmount = double.Parse(hdnIncidentalTotal.Value);
        foreach (DataGridItem oItem in dgIncedentals.Items)
        {
            dblIncidentalAmount = dblIncidentalAmount + double.Parse(((TextBox)oItem.FindControl("txtListAmount")).Text);
        }
        txtIncedentalsTotal.Text = string.Format("{0:0.00}", dblIncidentalAmount);
        dblIncidentalAmount = 0.00;
    }

    protected void dgIncedentals_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            lblIncidentalError.Visible = false;
            divError.Visible = false;
            DataTable tblCart1 = ViewState["CartIncedentals"] as DataTable;
            tblCart1.Rows[e.Item.ItemIndex].Delete();
            ViewState["CartIncedentals"] = tblCart1;

            dgIncedentals.DataSource = tblCart1;
            dgIncedentals.DataBind();

            DGAddIncidentals();
            if (dgIncedentals.Items.Count != 0)
            {
                trIncidentalTotal.Visible = true;
                trNoIncedental.Visible = false;
                dgIncedentals.Visible = true;
            }
            else
            {
                trIncidentalTotal.Visible = false;
                trNoIncedental.Visible = true;
                dgIncedentals.Visible = false;
            }
            CATASum();
        }
        catch
        {
            Response.Redirect("FinanceNewCataRequest.aspx");
        }
    }

    protected void DDLLoad()
    {
        ddlSchool.DataSource = clsSchool.GetSchoolCMHQOwned();
        ddlSchool.DataTextField = "pText";
        ddlSchool.DataValueField = "pValue";
        ddlSchool.DataBind();
        ddlSchool.Items.Insert(0, new ListItem("-", String.Empty));
        ddlSchool.SelectedIndex = 0;
        ddlRc.DataSource = clsRC.GetDdlDs();
        ddlRc.DataTextField = "pText";
        ddlRc.DataValueField = "pValue";
        ddlRc.DataBind();
        ddlRc.Items.Insert(0, new ListItem("-", String.Empty));
        ddlRc.SelectedIndex = 0;

        //if (!IsExecutive())
        //{
        //    trObNumber.Visible = true;
        //    ddlObNumber.DataSource = clsOB.GetDSL(Request.Cookies["Speedo"]["UserName"]);
        //    ddlObNumber.DataTextField = "pText";
        //    ddlObNumber.DataValueField = "pValue";
        //    ddlObNumber.DataBind();
        //    GetOBDateRange();
        //}

        //ddlEndorsedBy1.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy1.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.CATAModule, "1");
        ddlEndorsedBy1.DataValueField = "pvalue";
        ddlEndorsedBy1.DataTextField = "ptext";
        ddlEndorsedBy1.DataBind();
        ddlEndorsedBy1.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy1.SelectedIndex = 0;


        ddlEndorsedBy1.SelectedValue = clsModuleApprover.ApproverEmployee(Request.Cookies["Speedo"]["UserName"], clsModule.ATWModule, "1");

        //ddlEndorsedBy2.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy2.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.CATAModule, "1");
        ddlEndorsedBy2.DataValueField = "pvalue";
        ddlEndorsedBy2.DataTextField = "ptext";
        ddlEndorsedBy2.DataBind();
        ddlEndorsedBy2.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy2.SelectedIndex = 0;

        //ddlAuthorizeby2.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlAuthorizeby2.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.CATAModule, "2");
        ddlAuthorizeby2.DataValueField = "pvalue";
        ddlAuthorizeby2.DataTextField = "ptext";
        ddlAuthorizeby2.DataBind();
        ddlAuthorizeby2.Items.Insert(0, new ListItem("-", String.Empty));
        ddlAuthorizeby2.SelectedIndex = 0;

        ddlExecutive.DataSource = clsEmployee.DSLExecutive();
        ddlExecutive.DataValueField = "pValue";
        ddlExecutive.DataTextField = "pText";
        ddlExecutive.DataBind();

        ddlAcquiremode.Items.Insert(0, new ListItem("For Pickup", "P"));
        ddlAcquiremode.Items.Insert(1, new ListItem("For Deposit", "D"));


        if (ddlDivisionHead.SelectedValue.ToString() != ddlExecutive.SelectedValue.ToString())
        {
            ddlAuthorizeby2.SelectedValue = ddlExecutive.SelectedValue.ToString();
        }
        else
        {
            ddlAuthorizeby2.SelectedIndex = 0;
        }
    }

    private void LoadAccomodation()
    {
        ddlAccomodation.DataSource = clsCATASubtype.GetDSL("01");
        ddlAccomodation.DataTextField = "pText";
        ddlAccomodation.DataValueField = "pValue";
        ddlAccomodation.DataBind();
    }

    protected void ddlAccomodation_SelectedIndexChanged(object sender, EventArgs e)
    {
        AccomodationAmount();
    }

    protected void dtDateDeparture_DateChanged(object sender, EventArgs e)
    {
        CalculateDate();
        ValidateDate();
    }

    protected void dtDateArrival_DateChanged(object sender, EventArgs e)
    {
        CalculateDate();
    }

    protected void dtDateCheckNeeded_DateChanged(object sender, EventArgs e)
    {
        CalculateDate();
        if (ValidateDate() == false)
        { return; }
    }

    protected void dtTimeArrival_TimeChanged(object sender, EventArgs e)
    { CalculateDate(); }

    protected void CalculateDate()
    {
        DateTime dt1 = dtDateDeparture.SelectedDate;
        DateTime dt2 = dtDateArrival.SelectedDate;

        //if (!IsExecutive())
        //{
        //    DataTable tblOBRange = clsOBDetails.GetStartEndDate(ddlObNumber.SelectedValue.ToString());
        //    foreach (DataRow drOBRange in tblOBRange.Rows)
        //    {
        //        if (dtDateArrival.SelectedDate > DateTime.Parse(drOBRange["DateEnd"].ToString()))
        //            dt2 = DateTime.Parse(drOBRange["DateEnd"].ToString());
        //    }
        //}

        TimeSpan t1 = dt2.Subtract(dt1);

        if (t1.Days > 0)
        {
            //Check if Time arrival is below or above 2pm
            TimeSpan now = (dtTimeArrival.SelectedTime).TimeOfDay;
            TimeSpan twoPM = TimeSpan.FromHours(14);
            if (TimeSpan.Compare(now, twoPM) < 0)
            { txtDays.Text = Convert.ToString(double.Parse(t1.Days.ToString()) + 0.5); }
            else
            { txtDays.Text = Convert.ToString(double.Parse(t1.Days.ToString()) + 1); }
            //Check if Time arrival is below or above 2pm
        }
        else
        {
            dtDateArrival.SelectedValue = dtDateDeparture.SelectedDate;
            dt1 = dtDateDeparture.SelectedDate;
            dt2 = dtDateArrival.SelectedDate;
            t1 = dt2.Subtract(dt1);
            txtDays.Text = Convert.ToString(t1.Days + 1);


        }
        AccomodationAmount();
        TravelAllowanceEditted();

        CATASum();
    }

    protected void chkbLand_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbLand.Checked == true)
        {
            trLandBus.Visible = true;
            trLandGasAllowance.Visible = true;
            trLandTollFee.Visible = true;
            trLandVhire.Visible = true;
            trLandOthers.Visible = true;
        }
        else
        {
            trLandBus.Visible = false;
            trLandGasAllowance.Visible = false;
            trLandTollFee.Visible = false;
            trLandVhire.Visible = false;
            trLandOthers.Visible = false;
            txtLandOther.Text = "";
            txtLandBus.Text = "";
            txtLandGasAllowance.Text = "";
            txtLandTollFee.Text = "";
            txtLandVHire.Text = "";
        }
        CATASum();
    }

    protected void chkbAir_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbAir.Checked == true)
        {
            //trAirFixTransportation.Visible = true;
            trAirTerminalFee.Visible = true;
            //txtAirFixedTransportation.Text = clsCATASettings.GetAmount("03", "06", "NA");

            //Automatic select Manila as terminal Fee
            foreach (ListItem item in cblTerminalFee.Items)
            {
                item.Selected = item.Value == "1" ? true : false;
            }

            double dlbTerminalFee = 0;
            foreach (ListItem itm in cblTerminalFee.Items)
            {
                if (itm.Selected)
                {
                    dlbTerminalFee += clsFinanceTerminalFee.GetAmount(itm.Value.ToInt());
                }
            }

            txtAirTerminalFee.Text = dlbTerminalFee > 0 ? string.Format("{0:0.00}", dlbTerminalFee).ToString() : "0";
            //txtAirTerminalFee.Text = clsCATASettings.GetAmount("03", "07", "NA");
        }
        else
        {
            trAirFixTransportation.Visible = false;
            trAirTerminalFee.Visible = false;
            txtAirTerminalFee.Text = "";
            txtAirFixedTransportation.Text = "";

            //Refresh check box terminal fee
            foreach (ListItem item in cblTerminalFee.Items)
            {
                item.Selected = false;
            }
        }
        CATASum();
    }

    protected void chkbSea_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbSea.Checked == true)
        {
            trSeaFerry.Visible = true;
        }
        else
        {
            trSeaFerry.Visible = false;
            txtSeaFerry.Text = "";
        }
        CATASum();
    }

    private void AccomodationAmount()
    {
        try
        {
            DateTime dt1 = dtDateDeparture.SelectedDate;
            DateTime dt2 = dtDateArrival.SelectedDate;
            TimeSpan t1 = dt2.Subtract(dt1);

            int intTotalAccomodationAllowance = 0;
            double dblAccomodationAmount;
            double dblDay = double.Parse(t1.Days.ToString());
            dblDay = dblDay + 1.0;
            
                if (ddlAccomodation.SelectedValue == "02")
                { dblAccomodationAmount = double.Parse(clsCATASettings.GetAmount("01", ddlAccomodation.SelectedValue.ToString(), "NA")); }
                else if (ddlAccomodation.SelectedValue == "03")
                { dblAccomodationAmount = double.Parse(clsCATASettings.GetAmount("01", ddlAccomodation.SelectedValue.ToString(), "NA")); }
                else
                { dblAccomodationAmount = double.Parse(clsCATASettings.GetAmount("01", ddlAccomodation.SelectedValue.ToString(), clsEmployee.GetJobGrade(ddlExecutive.SelectedValue.ToString()))); }
            

            if (dblDay == 0 || dblDay == 1.0)
            {
                txtAccomodationTotal.Text = "";
                hdnAccomodation.Value = "";
                divAccomodation.Visible = false;
            }

            else
            {
                if (dblDay >= 2.0)
                {
                    intTotalAccomodationAllowance = Convert.ToInt32(dblDay - 1.0) * Convert.ToInt32(dblAccomodationAmount);
                }
                txtAccomodationTotal.Text = string.Format("{0:0.00}", intTotalAccomodationAllowance);
                hdnAccomodation.Value = ddlAccomodation.SelectedValue.ToString();
                divAccomodation.Visible = true;

            }
            CATASum();
        }

        catch
        {
            txtAccomodationTotal.Text = "";
            CATASum();
        }
    }

    private void TravelAllowance()
    {
        double dblDay = double.Parse(txtDays.Text);
        double dblTotalTravelAllowance;
        double dblTravelAllowanceAmount;
        //if (chkbExecutive.Checked != true)
        //{ dblTravelAllowanceAmount = double.Parse(clsCATASettings.GetAmount("02", clsEmployee.GetJobGrade(hdnUsername.Value))); }
        //else
        dblTravelAllowanceAmount = double.Parse(clsCATASettings.GetAmount("02", clsEmployee.GetJobGrade(ddlExecutive.SelectedValue.ToString())));
        hdnTravel.Value = "12";

        if (dblDay == 0)
        {
            txtTravelTotal.Text = Convert.ToString(dblTravelAllowanceAmount);
        }
        else
        {
            txtRatePerDay.Text = string.Format("{0:0.00}", dblTravelAllowanceAmount);
            dblTotalTravelAllowance = dblDay * dblTravelAllowanceAmount;
            txtTravelTotal.Text = string.Format("{0:0.00}", dblTotalTravelAllowance);
        }
    }

    private void TravelAllowanceEditted()
    {
        float fltRatePerDay = 0;
        float fltDays = 0;

        try
        {
            if ((txtRatePerDay.Text.Trim() != string.Empty) || clsValidator.CheckFloat(txtRatePerDay.Text.Trim()) == 1)
            {
                fltRatePerDay = float.Parse(txtRatePerDay.Text.Trim());
                fltDays = float.Parse(txtDays.Text);
                txtTravelTotal.Text = string.Format("{0:0.00}", fltRatePerDay * fltDays);
            }
            else
            {
                fltDays = float.Parse(txtDays.Text);
                txtTravelTotal.Text = string.Format("{0:0.00}", fltRatePerDay * fltDays);
            }
        }
        catch
        {
            fltDays = float.Parse(txtDays.Text);
            txtTravelTotal.Text = string.Format("{0:0.00}", fltRatePerDay * fltDays);
        }
        CATASum();
    }

    private void RepresentationAllowance()
    {
        string _strJobGradeClass = clsJobGrade.GetJobGradeClass(clsEmployee.GetJobGrade(hdnUsername.Value));
     
            divRepresentation.Visible = true;
            if (dgRepresentation.Items.Count != 0)
            {
                txtRepresentationAmount.Text = clsCATASettings.GetAmount("05", clsEmployee.GetJobGrade(ddlExecutive.SelectedValue.ToString()));
                hdnRepresentation.Value = clsCATASettings.GetSubTypeCode("05", clsEmployee.GetJobGrade(ddlExecutive.SelectedValue.ToString()));
            }
            else
            {
                Session["Representation"] = null;
                dgRepresentation.DataSource = null;
                dgRepresentation.DataBind();
                trRepresentation.Visible = dgRepresentation.Items.Count != 0;
                trNoRepresentation.Visible = dgRepresentation.Items.Count == 0;
                dgRepresentation.Visible = dgRepresentation.Items.Count == 0;
                txtRepresentationAmount.Text = "";
            }
        
        if (dgRepresentation.Items.Count == 0)
        { txtRepresentationAmount.Text = ""; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSaveandApprove.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSaveandApprove.Page.ClientScript.GetPostBackEventReference(btnSaveandApprove, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        btnSaveandApprove.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSaveandApprove, null) + ";");
        if (!Page.IsPostBack)
        {
         chkbExecutive.Checked = true;
            using (clsEmployee employee = new clsEmployee())
            {
                hdnUsername.Value = Request.Cookies["Speedo"]["UserName"];
                employee.Username = Request.Cookies["Speedo"]["UserName"];
                employee.Fill();
                //txtRequestor.Text = employee.FirstName + " " + employee.MiddleInitial + ". " + employee.LastName;
                txtRequestorName.Text = employee.FirstName + " " + employee.MiddleInitial + ". " + employee.LastName;

                ddlDivisionHead.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.CATAModule, "2");
                ddlDivisionHead.DataValueField = "pvalue";
                ddlDivisionHead.DataTextField = "ptext";
                ddlDivisionHead.DataBind();

                if (employee.RcCode == "AUD")
                {
                    //hdnAuthorizedby1.Value = "jun.sagcal";
                    //txtAuthorizeBy1.Text = "Engracio Sagcal Jr.";
                    ddlDivisionHead.SelectedValue = "jun.sagcal";
                }
                else
                {
                    ddlDivisionHead.SelectedValue = clsDivision.GetDivisionHead(employee.DivisionCode);
                    //hdnAuthorizedby1.Value = clsDivision.GetDivisionHead(employee.DivisionCode);
                    //txtAuthorizeBy1.Text = clsEmployee.GetName(hdnAuthorizedby1.Value, EmployeeNameFormat.FirstLast);
                }
            }

            string _strJobGradeClass = clsJobGrade.GetJobGradeClass(clsEmployee.GetJobGrade(hdnUsername.Value));
            if (_strJobGradeClass == "1")
            {
                ddlExecutive.SelectedValue = hdnUsername.Value;
            }

            dtDateDeparture.SelectedValue = DateTime.Now;
            dtDateArrival.SelectedValue = DateTime.Now;

            DDLLoad();
            LoadAccomodation();

            MakeCart();

            trNoIncedental.Visible = true;
            trNoRepresentation.Visible = true;

            CalculateDate();

            //Travel Allowance
            TravelAllowance();

            LoadHiddenValue();

            //accommodation if 2 days and up
            if (int.Parse(txtDays.Text) > 1)
            { divAccomodation.Visible = true; }
            else { divAccomodation.Visible = false; }

            RepresentationAllowance();
            LoadTerminalFee();
            divTransportation.Visible = true;
            divTravelAllowance.Visible = true;
            divIncedental.Visible = true;
            divApprovers.Visible = true;
            CATASum();
            ddlExecutive.Visible = true;
        }
    }

    protected void LoadTerminalFee()
    {
        cblTerminalFee.DataSource = clsFinanceTerminalFee.GetDSG();
        cblTerminalFee.DataValueField = "TerminalFeeCode";
        cblTerminalFee.DataTextField = "TerminalFeeName";
        cblTerminalFee.DataBind();
    }

    protected void LoadHiddenValue()
    {
        hdnLandBus.Value = "14";
        hdnLandGasAllowance.Value = "09";
        hdnLandVHire.Value = "13";
        hdnLandTollFee.Value = "10";
        hdnAirTerminalFee.Value = "07";
        hdnAirFixTranspo.Value = "06";
        hdnSeaFerry.Value = "15";
        hdnOthers.Value = "11";

        hdnIncidentalTotal.Value = "0.00";
    }

    protected void btndgRepresentationDelete_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            divError.Visible = false;
            lblRepresentationError.Visible = false;
            DataTable tblCart2 = ViewState["CartRepresentation"] as DataTable;
            tblCart2.Rows[e.Item.ItemIndex].Delete();
            ViewState["CartRepresentation"] = tblCart2;

            dgRepresentation.DataSource = tblCart2;
            dgRepresentation.DataBind();

            if (dgRepresentation.Items.Count == 0)
            {
                trRepresentation.Visible = false;
                trNoRepresentation.Visible = true;
                dgRepresentation.Visible = false;
                txtRepresentationAmount.Text = "";
            }
            else
            {
                trRepresentation.Visible = true;
                trNoRepresentation.Visible = false;
                dgRepresentation.Visible = true;
            }
            CATASum();
        }

        catch
        {
            Response.Redirect("FinanceCataMenu.aspx");
        }
    }

    protected void btnIncedentalAdd_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        lblIncidentalError.Visible = false;
        if (dgIncedentals.Items.Count < 8)
        {
            DataTable tblCart1 = ViewState["CartIncedentals"] as DataTable;
            DataRow drowCart1 = tblCart1.NewRow();
            drowCart1["incdental"] = txtIncedentalName.Text.Trim();
            drowCart1["amount"] = string.Format("{0:0.00}", double.Parse(txtIncedentalAmount.Text));
            tblCart1.Rows.Add(drowCart1);
            ViewState["CartIncedentals"] = tblCart1;
            dgIncedentals.DataSource = tblCart1;
            dgIncedentals.DataBind();
            txtIncedentalName.Text = "";
            txtIncedentalAmount.Text = "";
            DGAddIncidentals();

            if (dgIncedentals.Items.Count != 0)
            {
                trIncidentalTotal.Visible = true;
                trNoIncedental.Visible = false;
                dgIncedentals.Visible = true;
            }
            else
            {
                trIncidentalTotal.Visible = false;
                trNoIncedental.Visible = true;
                dgIncedentals.Visible = false;
            }
            AccomodationAmount();
            CATASum();
        }
        else
        {
            lblIncidentalError.Text = "Maximum number of items reached."; lblIncidentalError.Visible = true; return;
        }

    }

    protected void btnAddPerson_Click(object sender, EventArgs e)
    {
        lblRepresentationError.Visible = false;
        if (dgRepresentation.Items.Count <= 5)
        {
            DataTable tblCart2 = ViewState["CartRepresentation"] as DataTable;
            DataRow drowCart2 = tblCart2.NewRow();
            drowCart2["rprsnttn"] = txtRepresentationPerson.Text.Trim();
            tblCart2.Rows.Add(drowCart2);
            ViewState["CartRepresentation"] = tblCart2;
            dgRepresentation.DataSource = tblCart2;
            dgRepresentation.DataBind();
            txtRepresentationPerson.Text = "";
            RepresentationAllowance();
            CATASum();
            if (dgRepresentation.Items.Count == 0)
            {
                trRepresentation.Visible = false;
                trNoRepresentation.Visible = true;
                dgRepresentation.Visible = false;
                txtRepresentationAmount.Text = "";
            }
            else
            {
                trRepresentation.Visible = true;
                trNoRepresentation.Visible = false;
                dgRepresentation.Visible = true;
            }
        }
        else
        { lblRepresentationError.Text = "Maximum number of person reached."; lblRepresentationError.Visible = true; return; }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save("Edit");
    }

    public DataTable GetRequest()
    {
        DataTable tblReturn = new DataTable();
        tblReturn.Columns.Add("setcode");
        tblReturn.Columns.Add("amount");

        if (txtLandBus.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnLandBus.Value, Convert.ToDouble(txtLandBus.Text.Trim())); }

        if (txtLandGasAllowance.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnLandGasAllowance.Value, Convert.ToDouble(txtLandGasAllowance.Text.Trim())); }

        if (txtLandVHire.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnLandVHire.Value, Convert.ToDouble(txtLandVHire.Text.Trim())); }

        if (txtLandTollFee.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnLandTollFee.Value, Convert.ToDouble(txtLandTollFee.Text.Trim())); }

        if (txtAirTerminalFee.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnAirTerminalFee.Value, Convert.ToDouble(txtAirTerminalFee.Text.Trim())); }

        if (txtAirFixedTransportation.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnAirFixTranspo.Value, Convert.ToDouble(txtAirFixedTransportation.Text.Trim())); }

        if (txtSeaFerry.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnSeaFerry.Value, Convert.ToDouble(txtSeaFerry.Text.Trim())); }

        if (txtLandOther.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnOthers.Value, Convert.ToDouble(txtLandOther.Text.Trim())); }

        if (txtAccomodationTotal.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnAccomodation.Value, Convert.ToDouble(txtAccomodationTotal.Text.Trim())); }

        if (txtTravelTotal.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnTravel.Value, Convert.ToDouble(txtTravelTotal.Text.Trim())); }

        if (txtRepresentationAmount.Text.Trim() != string.Empty)
        { tblReturn.Rows.Add(hdnRepresentation.Value, Convert.ToDouble(txtRepresentationAmount.Text.Trim())); }

        return tblReturn;
    }

    protected void btnAddTravelAllowance_Click(object sender, EventArgs e)
    {
        CATASum();
    }

    protected void btnAddTranspoAllowance_Click(object sender, EventArgs e)
    {
        CATASum();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FinanceCataMenu.aspx");
    }

    private void EmptyTextBoxValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if ((c.Controls.Count > 0))
            {
                EmptyTextBoxValues(c);
            }
            else
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
            }
        }
    }

    protected void ddlFranchisee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void chkbExecutive_CheckedChanged(object sender, EventArgs e)
    {
     Response.Redirect("FinanceNewCataRequest.aspx");
            ddlExecutive.Visible = true;
            TravelAllowance();
            AccomodationAmount();
            RepresentationAllowance();
            CalculateDate();
            //GetOBDateRange();

        CATASum();
    }

    protected void txtLandVHire_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }

    protected void txtLandBus_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }
    protected void txtLandGasAllowance_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }
    protected void txtLandTollFee_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }
    protected void txtLandOther_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }
    protected void txtAirFixedTransportation_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }
    protected void txtAirTerminalFee_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }
    protected void txtSeaFerry_TextChanged(object sender, EventArgs e)
    {
        CATASum();
    }

    protected void ddlEndorsedBy1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEndorsedBy1.SelectedValue.ToString() == ddlDivisionHead.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
        if (ddlEndorsedBy1.SelectedValue.ToString() == ddlEndorsedBy2.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
        if (ddlEndorsedBy1.SelectedValue.ToString() == ddlAuthorizeby2.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
    }
    protected void ddlEndorsedBy2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEndorsedBy2.SelectedValue.ToString() == ddlDivisionHead.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
        if (ddlEndorsedBy2.SelectedValue.ToString() == ddlEndorsedBy1.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
        if (ddlEndorsedBy2.SelectedValue.ToString() == ddlAuthorizeby2.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
    }
    protected void ddlAuthorizeby2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAuthorizeby2.SelectedValue.ToString() == ddlDivisionHead.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
        if (ddlAuthorizeby2.SelectedValue.ToString() == ddlEndorsedBy1.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
        if (ddlAuthorizeby2.SelectedValue.ToString() == ddlEndorsedBy2.SelectedValue.ToString())
        {
            divError.Visible = true;
            lblErrMsg.Text = "Person is already an approver";
        }
    }
    protected void txtRatePerDay_TextChanged(object sender, EventArgs e)
    {
        TravelAllowanceEditted();
    }
    protected void btnSaveandApprove_Click(object sender, EventArgs e)
    {
        Save("Approve");
    }

    protected void Save(string strType)
    {
        divError.Visible = false;
        if (ValidateDate() == false)
        { return; }

        if (ValidateApprovers() == false)
        { return; }

        DataTable tblRepresentation = ViewState["CartRepresentation"] as DataTable;
        DataTable tblIncidental = ViewState["CartIncedentals"] as DataTable;

        CATASum();
        clsCATARequest CataRequest = new clsCATARequest();
        CataRequest.SchoolCode = (ddlSchool.SelectedValue.ToString() != "") ? ddlSchool.SelectedValue.ToString() : string.Empty;
        CataRequest.RcCode = (ddlRc.SelectedValue.ToString() != "") ? ddlRc.SelectedValue.ToString() : string.Empty;
        CataRequest.Other = (txtOthers.Text.Trim() != "") ? txtOthers.Text.Trim() : string.Empty;
        CataRequest.LocationFrom = txtDestinationFrom.Text.Trim();
        CataRequest.LocationTo = txtDestinationTo.Text.Trim();
        CataRequest.NumberOfDays = double.Parse(txtDays.Text.Trim());
        CataRequest.Departure = clsDateTime.CombineDateTime(dtDateDeparture.SelectedDate, dtTimeDeparture.SelectedTime);
        CataRequest.Arrival = clsDateTime.CombineDateTime(dtDateArrival.SelectedDate, dtTimeArrival.SelectedTime);
        CataRequest.HotelName = txtHotelName.Text.Trim();
        CataRequest.TripPurpose = txtPurpose.Text.Trim();
        CataRequest.DateNeeded = dtDateCheckNeeded.SelectedDate;
        CataRequest.AcquireMode = ddlAcquiremode.SelectedValue.ToString();
        CataRequest.CataAmount = double.Parse(string.Format("{0:0.00}", double.Parse(txtTotalCATAAmount.Text.Trim())));
        CataRequest.RequestedBy = ddlExecutive.SelectedValue.ToString();
        CataRequest.ObCode = "";


        DataTable tblApprovers = new DataTable();
        tblApprovers.Columns.Add("Username");
        tblApprovers.Columns.Add("ApproverOrder");
        tblApprovers.Columns.Add("ApproverType");
        tblApprovers.Columns.Add("StatusCode");
        int intCount = 0;

        if (ddlEndorsedBy1.SelectedValue != string.Empty)
        {
            intCount++;
            DataRow drNewRow = tblApprovers.NewRow();
            drNewRow["Username"] = ddlEndorsedBy1.SelectedValue;
            drNewRow["ApproverOrder"] = intCount;
            drNewRow["ApproverType"] = "E";
            drNewRow["StatusCode"] = hdnUsername.Value == ddlEndorsedBy1.SelectedValue ? "1" : "0";
            tblApprovers.Rows.Add(drNewRow);
        }

        if (ddlEndorsedBy2.SelectedValue != string.Empty)
        {
            intCount++;
            DataRow drNewRow = tblApprovers.NewRow();
            drNewRow["Username"] = ddlEndorsedBy2.SelectedValue;
            drNewRow["ApproverOrder"] = intCount;
            drNewRow["ApproverType"] = "E";
            drNewRow["StatusCode"] = hdnUsername.Value == ddlEndorsedBy2.SelectedValue ? "1" : "0";
            tblApprovers.Rows.Add(drNewRow);
        }
        intCount++;
        DataRow drNewRow1 = tblApprovers.NewRow();
        drNewRow1["Username"] = ddlDivisionHead.SelectedValue.ToString();
        drNewRow1["ApproverOrder"] = intCount;
        drNewRow1["ApproverType"] = "A";
        drNewRow1["StatusCode"] = hdnUsername.Value == ddlDivisionHead.SelectedValue.ToString() ? "1" : "0";
        tblApprovers.Rows.Add(drNewRow1);

        if (ddlAuthorizeby2.SelectedValue != string.Empty)
        {
            intCount++;
            DataRow drNewRow = tblApprovers.NewRow();
            drNewRow["Username"] = ddlAuthorizeby2.SelectedValue;
            drNewRow["ApproverOrder"] = intCount;
            drNewRow["ApproverType"] = "A";
            drNewRow["StatusCode"] = hdnUsername.Value == ddlAuthorizeby2.SelectedValue ? "1" : "0";
            tblApprovers.Rows.Add(drNewRow);
        }

        foreach (DataRow drFinanceApprover in clsCATAFinanceApprovers.GetDSG().Rows)
        {
            intCount++;
            DataRow drNewRow = tblApprovers.NewRow();
            drNewRow["Username"] = drFinanceApprover["aprvname"].ToString();
            drNewRow["ApproverOrder"] = intCount;
            drNewRow["ApproverType"] = "F";
            drNewRow["StatusCode"] = "0";
            tblApprovers.Rows.Add(drNewRow);
        }


        CataRequest.Status = strType == "Edit" ? "0" : "1";
        CataRequest.CreateBy = Request.Cookies["Speedo"]["UserName"];
        CataRequest.CreateOn = DateTime.Now;
        CataRequest.ModifyBy = Request.Cookies["Speedo"]["UserName"];
        CataRequest.ModifyOn = DateTime.Now;

        DataTable tblTerminalFee = new DataTable();
        tblTerminalFee.Columns.Add("TerminalFeeCode");
        tblTerminalFee.Columns.Add("TerminalRate");
        foreach (ListItem itm in cblTerminalFee.Items)
        {
            if (itm.Selected)
            {
                DataRow drTerminalFee = tblTerminalFee.NewRow();
                drTerminalFee["TerminalFeeCode"] = itm.Value.ToInt();
                drTerminalFee["TerminalRate"] = clsFinanceTerminalFee.GetAmount(itm.Value.ToInt());
                tblTerminalFee.Rows.Add(drTerminalFee);
            }
        }


        if (CataRequest.Insert(GetRequest(), tblApprovers, tblIncidental, tblRepresentation, tblTerminalFee) > 0)
        {
            EmptyTextBoxValues(this);
            chkbAir.Checked = false;
            chkbLand.Checked = false;
            chkbSea.Checked = false;
            dgIncedentals.DataSource = null;
            dgIncedentals.DataBind();
            dgRepresentation.DataSource = null;
            dgRepresentation.DataBind();
            Response.Redirect("FinanceCataMenu.aspx");
        }
    }
    //protected void ddlObNumber_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetOBDateRange();
    //}
    protected void btnCloseTerminal_Click(object sender, EventArgs e)
    {
        pnlViewTerminal_ModalPopupExtender.Hide();
        double dlbTerminalFee = 0;
        foreach (ListItem itm in cblTerminalFee.Items)
        {
            if (itm.Selected)
            {
                dlbTerminalFee += clsFinanceTerminalFee.GetAmount(itm.Value.ToInt());
            }
        }

        txtAirTerminalFee.Text = dlbTerminalFee > 0 ? string.Format("{0:0.00}", dlbTerminalFee).ToString() : "0";
        CATASum();
    }
    protected void ddlExecutive_SelectedIndexChanged(object sender, EventArgs e)
    {

        //ddlEndorsedBy1.SelectedValue = ddlExecutive.SelectedValue.ToString();
        if (ddlDivisionHead.SelectedValue.ToString()!= ddlExecutive.SelectedValue.ToString())
        {
            ddlAuthorizeby2.SelectedValue = ddlExecutive.SelectedValue.ToString();
        }
        else
        {
            ddlAuthorizeby2.SelectedIndex = 0;
        }

    }
}