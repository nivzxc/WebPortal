//Programmer: Charlie Bachiller 
//Date finished: March 4, 2011

using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;

public partial class Finance_FinanceCataEditRequest : System.Web.UI.Page
{
    protected void LoadOBDetails()
    {
        if (txtOBNumber.Text != string.Empty)
        {
            clsOB ob = new clsOB(txtOBNumber.Text);
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
        DataTable tblOBDetails = clsOBDetails.GetDataTable(txtOBNumber.Text);
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

    protected void LoadTerminalFee()
    {
        cblTerminalFee.DataSource = clsFinanceTerminalFee.GetDSG();
        cblTerminalFee.DataValueField = "TerminalFeeCode";
        cblTerminalFee.DataTextField = "TerminalFeeName";
        cblTerminalFee.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string _strJobGradeClass = "";
        clsSpeedo.Authenticate();

        clsCATARequest objCataRequest = new clsCATARequest();
        objCataRequest.CataCode = Request.QueryString["catacode"];
        objCataRequest.Fill();

        if (!IsPostBack)
        {
            if (objCataRequest.Status != "0")
            { Response.Redirect("../../AccessDenied.aspx"); }

            DDLSchool();
            DDLRcGroup();
            //DDLObCode();
            chkbLand.Checked = false;
            chkbAir.Checked = false;
            chkbSea.Checked = false;
            trLandBus.Visible = false;
            trLandGasAllowance.Visible = false;
            trLandTollFee.Visible = false;
            trLandVhire.Visible = false;
            trSeaFerry.Visible = false;
            trAirFixTransportation.Visible = false;
            trAirTerminalFee.Visible = false;

            DDLEndorser();

            ddlAcquiremode.Items.Insert(0, new ListItem("For Pickup", "P"));
            ddlAcquiremode.Items.Insert(1, new ListItem("For Deposit", "D"));

            txtCataNumber.Text = objCataRequest.CataCode.Trim();
            txtRequestor.Text = clsEmployee.GetName(objCataRequest.RequestedBy.Trim());
            hdnUsername.Value = objCataRequest.RequestedBy;
            hdnCreateBy.Value = objCataRequest.CreateBy;
            _strJobGradeClass = clsJobGrade.GetJobGradeClass(clsEmployee.GetJobGrade(hdnUsername.Value));

            if (_strJobGradeClass == "1")
            {
                divRepresentation.Visible = true;
                trObNumber.Visible = false;
                pnlModal.Visible = false;
            }
            else
            {
                trObNumber.Visible = true;
                divRepresentation.Visible = false;
                txtOBNumber.Text = objCataRequest.ObCode;
                GetOBDateRange();
                LoadOBDetails();
            }

            txtDestinationFrom.Text = objCataRequest.LocationFrom.Trim();
            txtDestinationTo.Text = objCataRequest.LocationTo.Trim();
            if (objCataRequest.SchoolCode.Trim() != string.Empty)
            { ddlSchool.SelectedValue = objCataRequest.SchoolCode; }
            if (objCataRequest.RcCode.Trim() != string.Empty)
            { ddlRc.SelectedValue = objCataRequest.RcCode; }
            if (objCataRequest.Other.Trim() != string.Empty)
            { txtOthers.Text = objCataRequest.Other; }
            dtDateDeparture.SelectedValue = objCataRequest.Departure;
            dtTimeDeparture.SelectedValue = objCataRequest.Departure;
            dtDateArrival.SelectedValue = objCataRequest.Arrival;
            dtTimeArrival.SelectedValue = objCataRequest.Arrival;
            txtDays.Text = objCataRequest.NumberOfDays.ToString();
            ddlAcquiremode.SelectedValue = objCataRequest.AcquireMode;
            dtDateCheckNeeded.SelectedValue = objCataRequest.DateNeeded;
            txtPurpose.Text = objCataRequest.TripPurpose.Trim();
            txtTotalCATAAmount.Text = objCataRequest.CataAmount.ToString();
            
            DDLAccomodation();
            
            if (double.Parse(txtDays.Text) > 1)
            { divAccomodation.Visible = true; txtHotelName.Text = objCataRequest.HotelName; }
            else { divAccomodation.Visible = false; }

            LoadHiddenValue();

            trNoIncedental.Visible = true;
            trNoRepresentation.Visible = true;
            divTransportation.Visible = true;
            divTravelAllowance.Visible = true;
            divIncedental.Visible = true;
            divApprovers.Visible = true;

            LoadCataDetails();

            MakeCart();

            LoadIncidental();
            DGLoadRepresentation();
            LoadTerminalFee();
          
            //Load Terminal Fee
            foreach (ListItem item in cblTerminalFee.Items)
            {
                item.Selected = clsCATATerminalFee.IsExist(Request.QueryString["catacode"], item.Value.ToInt());
            }

            CATASum();
            LoadApprovers();

        }
    }

    protected bool IsExecutive()
    {
        bool blnReturn = false;
        string _strJobGradeClass = "";
        _strJobGradeClass = clsJobGrade.GetJobGradeClass(clsEmployee.GetJobGrade(hdnUsername.Value));
        //representation if manager or executive
        if (_strJobGradeClass == "1" || _strJobGradeClass == "2")
        {
            trObNumber.Visible = false;
            blnReturn = true;
        }
        else
        {
            trObNumber.Visible = true;
        }
        return blnReturn;

    }

    //protected void DDLObCode()
    //{
    //    ddlObNumber.DataSource = clsOB.GetDSL(Request.Cookies["Speedo"]["UserName"]);
    //    ddlObNumber.DataTextField = "pText";
    //    ddlObNumber.DataValueField = "pValue";
    //    ddlObNumber.DataBind();
    //    //GetOBDateRange();
    //}

    protected void LoadApprovers()
    {
        DataTable tblApprovers = clsCATAApproval.GetDSG(Request.QueryString["catacode"]);
        int intCountEndorser = 0;
        int intCountAuthorizer = 0;
        foreach (DataRow drApprover in tblApprovers.Rows)
        {
            
            if (drApprover["ApproverType"].ToString() == "E")
            {
                if (intCountEndorser != 1)
                {
                    ddlEndorsedBy1.SelectedValue = drApprover["Username"].ToString();
                    intCountEndorser++;
                }
                else
                {
                    ddlEndorsedBy2.SelectedValue = drApprover["Username"].ToString();
                    intCountEndorser++;
                }
            }
            else if (drApprover["ApproverType"].ToString() == "A")
            {
                if (intCountAuthorizer != 1)
                {
                    ddlDivisionHead.SelectedValue = drApprover["Username"].ToString();
                    //hdnAuthorizedby1.Value = drApprover["Username"].ToString();
                    //txtAuthorizeBy1.Text = clsEmployee.GetName(hdnAuthorizedby1.Value);
                    intCountAuthorizer++;
                }
                else
                {
                    ddlAuthorizeby2.SelectedValue = drApprover["Username"].ToString();
                    intCountAuthorizer++;
                }
            }
        }
    }

    protected bool ValidateDateRange()
    {
        bool blnReturn = false;
        if (!IsExecutive())
        {
            DataTable tblOBRange = clsOBDetails.GetStartEndDate(txtOBNumber.Text);
            DateTime dtOBStart = new DateTime();
            DateTime dtOBEnd = new DateTime();
            foreach (DataRow drOBRange in tblOBRange.Rows)
            {
                dtOBStart = DateTime.Parse(drOBRange["DateStart"].ToString()).Date;
                dtOBEnd = DateTime.Parse(drOBRange["DateEnd"].ToString()).Date;
            }

            if (dtDateDeparture.SelectedValue >= dtOBStart && dtDateDeparture.SelectedValue <= dtOBEnd)
            {
                if (dtDateArrival.SelectedValue >= dtOBStart && dtDateArrival.SelectedValue <= dtOBEnd)
                {
                    blnReturn = true;
                }
            }
            else
            {
                divError.Visible = true;
                lblErrMsg.Text = "The departure and arrival date must be between " + dtOBStart.ToShortDateString() + " and " + dtOBEnd.ToShortDateString();
            }


        }
        else
        {
            blnReturn = true;

        }
        return blnReturn;
    }

    protected void GetOBDateRange()
    {
        if (txtOBNumber.Text != string.Empty)
        {
            DataTable tblOBRange = clsOBDetails.GetStartEndDate(txtOBNumber.Text);
            foreach (DataRow drOBRange in tblOBRange.Rows)
            {
                dtDateDeparture.LowerBoundDate = DateTime.Parse(drOBRange["DateStart"].ToString());
                dtDateDeparture.UpperBoundDate = DateTime.Parse(drOBRange["DateEnd"].ToString());

                dtDateArrival.LowerBoundDate = DateTime.Parse(drOBRange["DateStart"].ToString());
                dtDateArrival.UpperBoundDate = DateTime.Parse(drOBRange["DateEnd"].ToString());
            }

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
            if (ddlEndorsedBy1.SelectedValue == ddlEndorsedBy2.SelectedValue.ToString())
            {
                divError.Visible = true;
                lblErrMsg.Text = "Person is already an approver";
                return false;
            }
        }

        if (ddlEndorsedBy1.SelectedValue.ToString() != string.Empty && ddlAuthorizeby2.SelectedValue.ToString() != string.Empty)
        {
            if (ddlEndorsedBy1.SelectedValue == ddlAuthorizeby2.SelectedValue.ToString())
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
            if (ddlAuthorizeby2.SelectedValue.ToString() == ddlDivisionHead.SelectedValue.ToString())
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

    protected void DDLEndorser()
    {
        //ddlEndorsedBy1.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy1.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1");
        ddlEndorsedBy1.DataValueField = "pvalue";
        ddlEndorsedBy1.DataTextField = "ptext";
        ddlEndorsedBy1.DataBind();
        ddlEndorsedBy1.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy1.SelectedIndex = 0;

        //ddlEndorsedBy2.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlEndorsedBy2.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "1");
        ddlEndorsedBy2.DataValueField = "pvalue";
        ddlEndorsedBy2.DataTextField = "ptext";
        ddlEndorsedBy2.DataBind();
        ddlEndorsedBy2.Items.Insert(0, new ListItem("-", String.Empty));
        ddlEndorsedBy2.SelectedIndex = 0;

        ddlDivisionHead.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "2");
        ddlDivisionHead.DataValueField = "pvalue";
        ddlDivisionHead.DataTextField = "ptext";
        ddlDivisionHead.DataBind();

        //ddlAuthorizeby2.DataSource = clsEmployee.DSLEmployeeListManagerVPSupervisor(Request.Cookies["Speedo"]["UserName"]);
        ddlAuthorizeby2.DataSource = clsModuleApprover.GetDSLFinanceApprover(clsModule.RFPModule, "2");
        ddlAuthorizeby2.DataValueField = "pvalue";
        ddlAuthorizeby2.DataTextField = "ptext";
        ddlAuthorizeby2.DataBind();
        ddlAuthorizeby2.Items.Insert(0, new ListItem("-", String.Empty));
        ddlAuthorizeby2.SelectedIndex = 0;
    }

    protected void LoadHiddenValue()
    {
        string _strJobGradeClass = "";
        _strJobGradeClass = clsJobGrade.GetJobGradeClass(clsEmployee.GetJobGrade(hdnUsername.Value));
        if (_strJobGradeClass == "1")
        { hdnRepresentation.Value = "05"; }

        else if (_strJobGradeClass == "2")
        { hdnRepresentation.Value = "04"; }

        hdnTravel.Value = "12";
        hdnLandBus.Value = "14";
        hdnLandGasAllowance.Value = "09";
        hdnLandVHire.Value = "13";
        hdnLandTollFee.Value = "10";
        hdnAirTerminalFee.Value = "07";
        hdnAirFixTranspo.Value = "06";
        hdnSeaFerry.Value = "15";
        hdnOthers.Value = "11";
    }

    protected Boolean ValidateDate()
    {
        DateTime dt1 = dtDateDeparture.SelectedDate;
        DateTime dt2 = dtDateCheckNeeded.SelectedDate;
        DateTime dtNow = DateTime.Now.Date;
        TimeSpan t1 = dt2.Subtract(dt1);

        if (t1.Days > 0)
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

    protected void DGLoadRepresentation()
    {
        DataTable tblRepresentation = clsCATARepresentation.GetDSGMainForm(Request.QueryString["catacode"]);
        foreach (DataRow drw in tblRepresentation.Rows)
        {
            DataTable tblCart2 = ViewState["CartRepresentation"] as DataTable;
            DataRow drowCart2 = tblCart2.NewRow();
            drowCart2["rprsnttn"] = drw["rprsnttn"].ToString();
            tblCart2.Rows.Add(drowCart2);
            ViewState["CartRepresentation"] = tblCart2;
            dgRepresentation.DataSource = tblCart2;
            dgRepresentation.DataBind();
        }

        if (dgRepresentation.Items.Count != 0)
        { RepresentationAllowance(); }
        trRepresentation.Visible = dgRepresentation.Items.Count != 0;
        trNoRepresentation.Visible = dgRepresentation.Items.Count == 0;
        CATASum();
    }

    protected void LoadIncidental()
    {
        //Load Incidental
        DataTable tblIncidental = clsCATAIncedental.GetDSGMainForm(Request.QueryString["catacode"]);
        double dblIncidentalAmount = 0;
        foreach (DataRow drw in tblIncidental.Rows)
        {
            dblIncidentalAmount += double.Parse(drw["amount"].ToString());
            DataTable tblCart1 = ViewState["CartIncedentals"] as DataTable;
            DataRow drowCart1 = tblCart1.NewRow();
            drowCart1["incdental"] = drw["incdental"].ToString();
            drowCart1["amount"] = string.Format("{0:0.00}", double.Parse(drw["amount"].ToString()));
            tblCart1.Rows.Add(drowCart1);
            ViewState["CartIncedentals"] = tblCart1;
            dgIncedentals.DataSource = tblCart1;
            dgIncedentals.DataBind();
            txtIncedentalName.Text = "";
            txtIncedentalAmount.Text = "";
            CATASum();
        }
        trNoIncedental.Visible = dgIncedentals.Items.Count == 0;
        trIncidentalTotal.Visible = dgIncedentals.Items.Count != 0;
        txtIncedentalsTotal.Text = string.Format("{0:0.00}", dblIncidentalAmount);
    }

    protected void LoadCataDetails()
    {
        //Load Cata Details
        DataTable tblCataDetails = clsCATADetails.GetDSGMainForm(Request.QueryString["catacode"]);
        int intCataSubType;
        foreach (DataRow drw in tblCataDetails.Rows)
        {
            intCataSubType = Int32.Parse(drw["stypcode"].ToString());
            switch (intCataSubType)
            {
                case 1:
                    txtAccomodationTotal.Text = drw["amount"].ToString();
                    ddlAccomodation.SelectedValue = "01";
                    break;

                case 2:
                    txtAccomodationTotal.Text = drw["amount"].ToString();
                    ddlAccomodation.SelectedValue = "02";
                    break;

                case 3:
                    txtAccomodationTotal.Text = drw["amount"].ToString();
                    ddlAccomodation.SelectedValue = "03";
                    break;

                case 4:
                    txtRepresentationAmount.Text = drw["amount"].ToString();
                    break;

                case 5:
                    txtRepresentationAmount.Text = drw["amount"].ToString();
                    break;

                case 12:
                    txtTravelTotal.Text = drw["amount"].ToString();
                    txtRatePerDay.Text = string.Format("{0:0.00}", float.Parse(txtTravelTotal.Text) / float.Parse(txtDays.Text));
                    break;

                case 6:
                    txtAirFixedTransportation.Text = drw["amount"].ToString();
                    trAirFixTransportation.Visible = true;
                    trAirTerminalFee.Visible = true;
                    chkbAir.Checked = true;
                    break;

                case 7:
                    txtAirTerminalFee.Text = drw["amount"].ToString();
                    trAirFixTransportation.Visible = true;
                    trAirTerminalFee.Visible = true;
                    chkbAir.Checked = true;
                    break;

                case 9:
                    txtLandGasAllowance.Text = drw["amount"].ToString();
                    trLandBus.Visible = true;
                    trLandGasAllowance.Visible = true;
                    trLandTollFee.Visible = true;
                    trLandVhire.Visible = true;
                    trLandOthers.Visible = true;
                    chkbLand.Checked = true;
                    break;

                case 10:
                    txtLandTollFee.Text = drw["amount"].ToString();
                    trLandBus.Visible = true;
                    trLandGasAllowance.Visible = true;
                    trLandTollFee.Visible = true;
                    trLandVhire.Visible = true;
                    trLandOthers.Visible = true;
                    chkbLand.Checked = true;
                    break;

                case 11:
                    txtLandOther.Text = drw["amount"].ToString();
                    trLandBus.Visible = true;
                    trLandGasAllowance.Visible = true;
                    trLandTollFee.Visible = true;
                    trLandVhire.Visible = true;
                    trLandOthers.Visible = true;
                    chkbLand.Checked = true;
                    break;

                case 13:
                    txtLandVHire.Text = drw["amount"].ToString();
                    trLandBus.Visible = true;
                    trLandGasAllowance.Visible = true;
                    trLandTollFee.Visible = true;
                    trLandVhire.Visible = true;
                    chkbLand.Checked = true;
                    break;

                case 15:
                    txtSeaFerry.Text = drw["amount"].ToString();
                    trSeaFerry.Visible = true;
                    chkbSea.Checked = true;
                    break;

                case 14:
                    txtLandBus.Text = drw["amount"].ToString();
                    trLandBus.Visible = true;
                    trLandGasAllowance.Visible = true;
                    trLandTollFee.Visible = true;
                    trLandVhire.Visible = true;
                    chkbLand.Checked = true;
                    break;
            }
        }
        CATASum();
    }

    protected void CATASum()
    {
        try
        {
            double dblTotalTranspoAllowance = 0.00;

            if ((txtLandBus.Text.Trim() != "") || clsValidator.CheckInteger(txtLandBus.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandBus.Text.Trim()); }

            if ((txtLandGasAllowance.Text.Trim() != "") || clsValidator.CheckInteger(txtLandGasAllowance.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandGasAllowance.Text.Trim()); }

            if ((txtLandVHire.Text.Trim() != "") || clsValidator.CheckInteger(txtLandVHire.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandVHire.Text.Trim()); }

            if ((txtLandTollFee.Text.Trim() != "") || clsValidator.CheckInteger(txtLandTollFee.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandTollFee.Text.Trim()); }

            if ((txtAirTerminalFee.Text.Trim() != "") || clsValidator.CheckInteger(txtAirTerminalFee.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtAirTerminalFee.Text.Trim()); }

            if ((txtAirFixedTransportation.Text.Trim() != "") || clsValidator.CheckInteger(txtAirFixedTransportation.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtAirFixedTransportation.Text.Trim()); }

            if ((txtSeaFerry.Text.Trim() != "") || clsValidator.CheckInteger(txtSeaFerry.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtSeaFerry.Text.Trim()); }

            if ((txtLandOther.Text.Trim() != "") || clsValidator.CheckInteger(txtLandOther.Text.Trim()) == 1)
            { dblTotalTranspoAllowance += Convert.ToDouble(txtLandOther.Text.Trim()); }

            txtTransportationTotal.Text = string.Format("{0:0.00}", dblTotalTranspoAllowance);

            double dblTotalCATAAllowance = 0.00;

            if ((txtRepresentationAmount.Text.Trim() != "") || clsValidator.CheckInteger(txtRepresentationAmount.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtRepresentationAmount.Text.Trim()); }

            if ((txtIncedentalsTotal.Text.Trim() != "") || clsValidator.CheckInteger(txtIncedentalsTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtIncedentalsTotal.Text.Trim()); }

            if ((txtTransportationTotal.Text.Trim() != "") || clsValidator.CheckInteger(txtTransportationTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtTransportationTotal.Text.Trim()); }

            if ((txtTravelTotal.Text.Trim() != "") || clsValidator.CheckInteger(txtTravelTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtTravelTotal.Text.Trim()); }

            if ((txtAccomodationTotal.Text.Trim() != "") || clsValidator.CheckInteger(txtAccomodationTotal.Text.Trim()) == 1)
            { dblTotalCATAAllowance += Convert.ToDouble(txtAccomodationTotal.Text.Trim()); }

            txtTotalCATAAmount.Text = string.Format("{0:0,0.00}", dblTotalCATAAllowance);
        }
        catch { }
    }

    protected void MakeCart()
    {
        //Incidental
        DataTable tblCart1 = new DataTable("CartIncedentals");
        tblCart1.Columns.Add("incdental", System.Type.GetType("System.String"));
        tblCart1.Columns.Add("amount", System.Type.GetType("System.String"));
        ViewState["CartIncedentals"] = tblCart1;
        //Representation
        DataTable tblCart2 = new DataTable("CartRepresentation");
        tblCart2.Columns.Add("rprsnttn", System.Type.GetType("System.String"));
        ViewState["CartRepresentation"] = tblCart2;
    }

    private void DGAddIncidentals()
    {
        double dblIncidentalAmount = 0.00;
        foreach (DataGridItem oItem in dgIncedentals.Items)
        {
            dblIncidentalAmount = dblIncidentalAmount + double.Parse(((TextBox)oItem.FindControl("txtListAmount")).Text);
        }
        txtIncedentalsTotal.Text = string.Format("{0:0.00}", dblIncidentalAmount);
        dblIncidentalAmount = 0.00;
    }

    private void DDLAccomodation()
    {
        ddlAccomodation.DataSource = clsCATASubtype.GetDSL("01");
        ddlAccomodation.DataTextField = "pText";
        ddlAccomodation.DataValueField = "pValue";
        ddlAccomodation.DataBind();
        hdnAccomodation.Value = ddlAccomodation.SelectedValue.ToString();
    }

    private void DDLSchool()
    {
        ddlSchool.DataSource = clsSchool.GetSchoolCMHQOwned();
        ddlSchool.DataTextField = "pText";
        ddlSchool.DataValueField = "pValue";
        ddlSchool.DataBind();
        ddlSchool.Items.Insert(0, new ListItem("-", String.Empty));
        ddlSchool.SelectedIndex = 0;
    }

    private void DDLRcGroup()
    {
        ddlRc.DataSource = clsRC.GetDdlDs();
        ddlRc.DataTextField = "pText";
        ddlRc.DataValueField = "pValue";
        ddlRc.DataBind();
        ddlRc.Items.Insert(0, new ListItem("-", String.Empty));
        ddlRc.SelectedIndex = 0;
    }

    protected void ddlAccomodation_SelectedIndexChanged(object sender, EventArgs e)
    {
        AccomodationAmount();
    }

    protected void dtDateDeparture_DateChanged(object sender, EventArgs e)
    {
        CalculateDate();
    }

    protected void dtDateCheckNeeded_DateChanged(object sender, EventArgs e)
    {
        CalculateDate();
        if (ValidateDate() == false)
        { return; }
    }

    protected void dtDateArrival_DateChanged(object sender, EventArgs e)
    {
        CalculateDate();
        ValidateDate();
    }

    protected void dtTimeArrival_TimeChanged(object sender, EventArgs e)
    { CalculateDate(); }

    protected void CalculateDate()
    {
        DateTime dt1 = dtDateDeparture.SelectedDate;
        DateTime dt2 = dtDateArrival.SelectedDate;

        DataTable tblOBRange = clsOBDetails.GetStartEndDate(txtOBNumber.Text);
        foreach (DataRow drOBRange in tblOBRange.Rows)
        {
            //dtDateArrival.LowerBoundDate = DateTime.Parse(drOBRange["DateStart"].ToString());
            if (dtDateArrival.SelectedDate > DateTime.Parse(drOBRange["DateEnd"].ToString()))
                dt2 = DateTime.Parse(drOBRange["DateEnd"].ToString());
        }

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
            trAirFixTransportation.Visible = true;
            trAirTerminalFee.Visible = true;
            txtAirFixedTransportation.Text = clsCATASettings.GetAmount("03", "06", "NA");
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
            { dblAccomodationAmount = double.Parse(clsCATASettings.GetAmount("01", ddlAccomodation.SelectedValue.ToString(), clsEmployee.GetJobGrade(hdnUsername.Value))); }

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

        dblTravelAllowanceAmount = double.Parse(clsCATASettings.GetAmount("02", clsEmployee.GetJobGrade(hdnUsername.Value)));
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
        string _strJobGradeClass = "";
        _strJobGradeClass = clsJobGrade.GetJobGradeClass(clsEmployee.GetJobGrade(hdnUsername.Value));
        //representation if manager or executive
        if (_strJobGradeClass == "1" || _strJobGradeClass == "2")
        {
            divRepresentation.Visible = true;
            if (dgRepresentation.Items.Count != 0)
            {
                txtRepresentationAmount.Text = clsCATASettings.GetAmount("05", clsEmployee.GetJobGrade(hdnUsername.Value));
                hdnRepresentation.Value = clsCATASettings.GetSubTypeCode("05", clsEmployee.GetJobGrade(hdnUsername.Value));
            }
        }
        else
        { divRepresentation.Visible = false; }

    }

    //Commands Buttons//
    protected void dgIncedentals_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
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
                txtIncedentalsTotal.Text = "";
            }
            CATASum();
        }
        catch
        {
            Response.Redirect("FinanceCataMenu.aspx");
        }
    }

    protected void btndgRepresentationDelete_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            divError.Visible = false;
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
            Session["Representation"] = tblCart2;
            CATASum();
        }

        catch
        {
            Response.Redirect("FinanceCataMenu.aspx");
        }
    }

    protected void btnIncedentalAdd_Click(object sender, EventArgs e)
    {
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
            DGAddIncidentals();
        }
        else
        { lblIncidentalError.Text = "Maximum number of items reached"; lblIncidentalError.Visible = true; return; }

        AccomodationAmount();
        CATASum();
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


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        using (clsCATARequest objRequest = new clsCATARequest())
        {
            objRequest.CataCode = Request.QueryString["catacode"];
            objRequest.Fill();
            if (objRequest.Status == "0")
            {
                if (clsCATARequest.Cancel(objRequest.CataCode) > 0)
                {
                    Response.Redirect("FinanceCataMenu.aspx");
                }
            }
        }
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlRc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtRatePerDay_TextChanged(object sender, EventArgs e)
    {
        TravelAllowanceEditted();
    }


    protected void btnSaveSubmit_Click(object sender, EventArgs e)
    {
        Save("Finalize");
    }

    protected bool ValidateOB()
    {
        bool blnReturn = false;
        string _strJobGradeClass = clsJobGrade.GetJobGradeClass(clsEmployee.GetJobGrade(hdnUsername.Value));


        if (_strJobGradeClass == "1" || _strJobGradeClass == "2")
        {
            blnReturn = true;
        }
        else
        {
            if (txtOBNumber.Text != string.Empty)
            {
                blnReturn = true;
            }
            else
            {
                divError.Visible = true;
                lblErrMsg.Text = "Approved filed Official Business is required.";
            }
        }

        
        return blnReturn;
    }

    protected void Save(string strType)
    {
        divError.Visible = false;
        if (ValidateDate() == false)
        { return; }

        if (ValidateApprovers() == false)
        { return; }

        if (ValidateOB() == false)
        { return; }

        if (ValidateDateRange() == false)
        { return; }

        CATASum();
        clsCATARequest CataRequest = new clsCATARequest();
        CataRequest.CataCode = txtCataNumber.Text.Trim();
        CataRequest.SchoolCode = (ddlSchool.SelectedValue.ToString() != "") ? ddlSchool.SelectedValue.ToString() : "";
        CataRequest.RcCode = (ddlRc.SelectedValue.ToString() != "") ? ddlRc.SelectedValue.ToString() : "";
        CataRequest.Other = (txtOthers.Text.Trim() != "") ? txtOthers.Text.Trim() : "";
        CataRequest.LocationFrom = txtDestinationFrom.Text.Trim();
        CataRequest.LocationTo = txtDestinationTo.Text.Trim();
        CataRequest.NumberOfDays = double.Parse(txtDays.Text.Trim());
        CataRequest.AcquireMode = ddlAcquiremode.SelectedValue.ToString();
        CataRequest.Departure = clsDateTime.CombineDateTime(dtDateDeparture.SelectedDate, dtTimeDeparture.SelectedTime);
        CataRequest.Arrival = clsDateTime.CombineDateTime(dtDateArrival.SelectedDate, dtTimeArrival.SelectedTime);
        CataRequest.HotelName = txtHotelName.Text.Trim();
        CataRequest.TripPurpose = txtPurpose.Text.Trim();
        CataRequest.DateNeeded = dtDateCheckNeeded.SelectedDate;
        CataRequest.CataAmount = double.Parse(string.Format("{0:0,0.00}", double.Parse(txtTotalCATAAmount.Text.Trim())));
        CataRequest.RequestedBy = txtRequestor.Text.Trim();
        CataRequest.Status = strType == "Edit" ? "0" : "1";

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
            drNewRow["StatusCode"] = hdnCreateBy.Value == ddlEndorsedBy1.SelectedValue ? "1" : "0";
            tblApprovers.Rows.Add(drNewRow);
        }

        if (ddlEndorsedBy2.SelectedValue != string.Empty)
        {
            intCount++;
            DataRow drNewRow = tblApprovers.NewRow();
            drNewRow["Username"] = ddlEndorsedBy2.SelectedValue;
            drNewRow["ApproverOrder"] = intCount;
            drNewRow["ApproverType"] = "E";
            drNewRow["StatusCode"] = hdnCreateBy.Value == ddlEndorsedBy2.SelectedValue ? "1" : "0";
            tblApprovers.Rows.Add(drNewRow);
        }
        intCount++;
        DataRow drNewRow1 = tblApprovers.NewRow();
        drNewRow1["Username"] = ddlDivisionHead.SelectedValue.ToString();
        drNewRow1["ApproverOrder"] = intCount;
        drNewRow1["ApproverType"] = "A";
        drNewRow1["StatusCode"] = hdnCreateBy.Value == ddlDivisionHead.SelectedValue.ToString() ? "1" : "0";
        tblApprovers.Rows.Add(drNewRow1);

        if (ddlAuthorizeby2.SelectedValue != string.Empty)
        {
            intCount++;
            DataRow drNewRow = tblApprovers.NewRow();
            drNewRow["Username"] = ddlAuthorizeby2.SelectedValue;
            drNewRow["ApproverOrder"] = intCount;
            drNewRow["ApproverType"] = "A";
            drNewRow["StatusCode"] = hdnCreateBy.Value == ddlAuthorizeby2.SelectedValue ? "1" : "0";
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

        //CataRequest.CreateBy = hdnUsername.Value;
        //CataRequest.CreateOn = DateTime.Now;
        CataRequest.ModifyBy = Request.Cookies["Speedo"]["UserName"];
        CataRequest.ModifyOn = DateTime.Now;
        CataRequest.ObCode = txtOBNumber.Text;
        ///Incidental
        DataTable tblRepresentation = ViewState["CartRepresentation"] as DataTable;
        ///Representation
        DataTable tblIncidental = ViewState["CartIncedentals"] as DataTable;

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

        if (CataRequest.Update(GetRequest(), tblApprovers, tblIncidental, tblRepresentation,tblTerminalFee) > 0)
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
        else
        { divError.Visible = true; lblErrMsg.Text = "Syntax Error on Saving Request"; }
    
    }
    protected void btnViewOB_Click(object sender, ImageClickEventArgs e)
    {
        
    }

    public static void Redirect(string url, string target, string windowFeatures)
    {
        HttpContext context = HttpContext.Current;

        if ((String.IsNullOrEmpty(target) ||
            target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
            String.IsNullOrEmpty(windowFeatures))
        {

            context.Response.Redirect(url);
        }
        else
        {
            Page page = (Page)context.Handler;
            if (page == null)
            {
                throw new InvalidOperationException(
                    "Cannot redirect to new window outside Page context.");
            }
            url = page.ResolveClientUrl(url);

            string script;
            if (!String.IsNullOrEmpty(windowFeatures))
            {
                script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
            }
            else
            {
                script = @"window.open(""{0}"", ""{1}"");";
            }

            script = String.Format(script, url, target, windowFeatures);
            ScriptManager.RegisterStartupScript(page,
                typeof(Page),
                "Redirect",
                script,
                true);
        }
    }
    protected void ddlObNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOBDetails();
    }
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        pnlModal_ModalPopupExtender.Hide();
    }
}
