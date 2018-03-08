using System;
using System.Net.NetworkInformation;
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

public partial class CIS_MRCF_MRCFNew : System.Web.UI.Page
{
    
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemspec", System.Type.GetType("System.String"));
        tblCart.Columns.Add("GLAccount", System.Type.GetType("System.String"));
        tblCart.Columns.Add("LineType", System.Type.GetType("System.String"));
        tblCart.Columns.Add("TransactionType", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Destination", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Item", System.Type.GetType("System.String"));
        tblCart.Columns.Add("qty", System.Type.GetType("System.Int32"));
        tblCart.Columns.Add("unit", System.Type.GetType("System.String"));
        tblCart.Columns.Add("dateneed", System.Type.GetType("System.DateTime"));
        tblCart.Columns.Add("EmpnameView", System.Type.GetType("System.String"));
        tblCart.Columns.Add("BirthdateView", System.Type.GetType("System.String"));
        tblCart.Columns.Add("TransactionDesc", System.Type.GetType("System.String"));
        tblCart.Columns.Add("LineTypeDesc", System.Type.GetType("System.String"));

        tblCart.Columns.Add("Empname", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Birthdate", System.Type.GetType("System.String"));
        ViewState["Cart"] = tblCart;
    }

    protected void LoadRC(string pRCCode)
    {
        ddlChargeTo.DataSource = clsRC.GetDdlDs();
        ddlChargeTo.DataValueField = "pValue";
        ddlChargeTo.DataTextField = "pText";
        ddlChargeTo.DataBind();

        foreach (ListItem itm in ddlChargeTo.Items)
        {
            if (itm.Value == pRCCode)
            {
                itm.Selected = true;
                LoadApprover(ddlChargeTo.SelectedValue);
                break;
            }
        }
    }

    protected void LoadRC()
    {
        ddlChargeTo.DataSource = clsRC.GetDdlDs();
        ddlChargeTo.DataValueField = "pValue";
        ddlChargeTo.DataTextField = "pText";
        ddlChargeTo.DataBind();
    }

    protected void LoadEmployees()
    {
        ddlEmployees.DataSource = clsMRCFAirlines.GetEmployees();
        ddlEmployees.DataValueField = "pValue";
        ddlEmployees.DataTextField = "pText";
        ddlEmployees.DataBind();
    }

    protected void LoadApprover(string pRcCode)
    {
        //DataTable tblGroupHeadApprover = clsMRCFApprover.DSLGroupHeadApprover(pRcCode);
        DataTable tblGroupHeadApprover = clsModuleApprover.DSLRCApprover(clsModule.MRCFModule, "1", ddlChargeTo.SelectedValue.ToString());
        ddlGrpHead.DataSource = tblGroupHeadApprover;
        ddlGrpHead.DataValueField = "pvalue";
        ddlGrpHead.DataTextField = "ptext";
        ddlGrpHead.DataBind();
        if (ddlGrpHead.Items.Count == 0)
            ddlGrpHead.Items.Add(new ListItem("No Approver Defined", "none"));

        
        DataTable tblDivHeadApprover = clsModuleApprover.DSLRCApprover(clsModule.MRCFModule, "2", ddlChargeTo.SelectedValue.ToString());
        ddlDivision.DataSource = tblDivHeadApprover;
        ddlDivision.DataValueField = "pvalue";
        ddlDivision.DataTextField = "ptext";
        ddlDivision.DataBind();
        if (ddlDivision.Items.Count == 0)
            ddlDivision.Items.Add(new ListItem("No Approver Defined", "none"));

        if (ddlDivision.SelectedValue.ToString() == "none" || ddlGrpHead.SelectedValue.ToString() == "none")
        {
            btnSend.Enabled = false;
        }
        else
        {
            btnSend.Enabled = true;
            //hdnDiviHeadCode.Value = ddlDivision.SelectedValue.ToString();
            using (clsUsers users = new clsUsers())
            {
                users.Username = ddlDivision.SelectedValue.ToString();
                users.Fill();
                hdnDiviHeadMail.Value = users.Email;
            }
        }
        //string strDHApprover = clsMRCFApprover.GetDivisionHeadApprover(pRcCode);
        //if (strDHApprover != "")
        //{
        //    btnSend.Enabled = true;
        //    hdnDiviHeadCode.Value = strDHApprover;
        //    using (clsUsers users = new clsUsers())
        //    {
        //        users.Username = strDHApprover;
        //        users.Fill();
        //        txtDiviHeadName.Text = users.FullName;
        //        hdnDiviHeadMail.Value = users.Email;
        //    }
        //}
        //else
        //{
        //    btnSend.Enabled = false;
        //    hdnDiviHeadCode.Value = "";
        //    txtDiviHeadName.Text = "[No Approver Defined]";
        //    hdnDiviHeadMail.Value = "";
        //}
    }

    protected void LoadUnit()
    {
        //string strPrimaryUOM = clsOracleMrcf.GetPrimaryUOM(ddlItem.SelectedValue.ToString());
        //string strUOMClass = clsOracleMrcf.GetUOMClass(strPrimaryUOM);
        ddlUnit.DataSource = clsOracleMrcf.GetDSLUOM(ddlItem.SelectedValue.ToString());
        ddlUnit.DataTextField = "pText";
        ddlUnit.DataValueField = "pValue";
        ddlUnit.DataBind();
    }

    private void LoadUnitAll()
    {
        ddlUnit.DataSource = clsOracleMrcf.GetMrcfUnitAll();
        ddlUnit.DataTextField = "pText";
        ddlUnit.DataValueField = "pValue";
        ddlUnit.DataBind();
    }

    public static bool IsConnectedToServer()
    {
        //Uri url = new Uri("http://martini.sti.edu");
        //string pingurl = string.Format("{0}", url.Host);
        //string host = pingurl;
        //bool result = false;
        //Ping p = new Ping();
        //try
        //{
        //    PingReply reply = p.Send(host, 3000);
        //    if (reply.Status == IPStatus.Success)
        //        return true;
        //}
        //catch { }
        //return result;
        string strStatus = "";
        string strPing = "";
        string strHost = "carlos";
        bool blnReturn = false;
        try
        {

            strStatus = null;
            Ping ping = new Ping();
            PingReply pingreply = ping.Send(strHost);
            strPing += "Address: " + pingreply.Address + "\r";
            strPing += "Roundtrip Time: " + pingreply.RoundtripTime + "\r";
            strPing += "TTL (Time To Live): " + pingreply.Options.Ttl + "\r";
            strPing += "Buffer Size: " + pingreply.Buffer.Length.ToString() + "\r";

        }
        catch (Exception err)
        {
            //failed to ping
            strStatus = err.Message.ToString();
        }
        return blnReturn;
    }

    protected void LoadPrimaryItemCategory()
    {
        DataTable tblPrimaryItemCat = new DataTable();
        tblPrimaryItemCat.Columns.Add("pText");
        tblPrimaryItemCat.Columns.Add("pValue");

        DataRow drPrimaryItemCategory = tblPrimaryItemCat.NewRow();
        drPrimaryItemCategory["pText"] = "ALL";
        drPrimaryItemCategory["pValue"] = "ALL";
        tblPrimaryItemCat.Rows.Add(drPrimaryItemCategory);

        foreach (DataRow dr in clsRequisitonItemCategory.GetDSLReq().Rows)
        {
            drPrimaryItemCategory = tblPrimaryItemCat.NewRow();
            drPrimaryItemCategory["pText"] = dr["pText"].ToString();
            drPrimaryItemCategory["pValue"] = dr["pValue"].ToString();
            tblPrimaryItemCat.Rows.Add(drPrimaryItemCategory);
        }

        ddlItemCategory.DataSource = tblPrimaryItemCat;
        ddlItemCategory.DataTextField = "pText";
        ddlItemCategory.DataValueField = "pValue";
        ddlItemCategory.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {   
        clsSpeedo.Authenticate();
       // btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
       // bool blnReturn = IsConnectedToServer();
        btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");

        // REMOVE DUE TO ORACLE DB is not available
        // By Calvin V.C
        // feb 13, 2018

        //if (!clsOracleMrcf.IsOracleUp())
        //{
        //    Response.Redirect("OracleDatabaseProblem.aspx");
        //}


        if (!Page.IsPostBack)
        {
            LoadMonths();
            LoadDays();
            LoadAirlines();
            LoadEmployees();
            //trAirfare.Visible = false;    
            trNoRequest.Visible = true;
            string strRCCode = "";
            MakeCart();
            txtRequestorName.Text = clsEmployee.GetName(Request.Cookies["Speedo"]["UserName"], EmployeeNameFormat.LastFirst);
            strRCCode = clsEmployee.GetRCCode(Request.Cookies["Speedo"]["UserName"]);
            string strProcessScript = "this.value='Processing...';this.disabled=true;";
            btnSend.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSend, "").ToString());
            
            clsUsers users = new clsUsers(clsMRCFApprover.GetProcurementManager());
            users.Fill();
            txtProcMngrName.Text = users.FullName;
            hdnProcMngrMail.Value = users.Email;


            ddlLineType.DataSource = clsMrcfLineType.GetDataSourceListLineType(clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"])).DefaultView;
            ddlLineType.DataBind();
            ddlLineType.Items[0].Selected = true;

            if (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true)
            {
                trItems.Visible = true;
                if (ddlLineType.SelectedValue.ToString() == "1")
                {
                    trItemsCategory.Visible = false;
                }
                else
                {
                    trItemsCategory.Visible = true;
                }
            }
            else
            {
                trItems.Visible = false;
            }

            LoadPrimaryItemCategory();

            ddlTransactionType.DataSource = clsMRCFTransactionType.GetDataSourceList(ddlLineType.SelectedValue.ToString()).DefaultView;
            ddlTransactionType.DataBind();
            ddlTransactionType.Items[0].Selected = true;


            ddlItem.DataSource = clsOracleMrcf.GetDataSourceListItems(ddlLineType.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString());
            ddlItem.DataBind();

            LoadUnitAll();

            ddlType.DataSource = clsMRCF.GetDDLSourceMrcfRequestType().DefaultView;
            ddlType.DataValueField = "pValue";
            ddlType.DataTextField = "pText";
            ddlType.DataBind();



            if (strRCCode != "")
                LoadRC(strRCCode);
            else
                LoadRC();

            DateTime dteDate = clsMRCF.GetMinimumDateNeeded(ddlType.SelectedValue);
            dteDateNeeded.MinDate = dteDate;
            dteDateNeeded.Date = dteDate;

            if (!ValidateEmployee())
            {
                return;
            }
        }
    }

    protected bool ValidateEmployee()
    {
        bool blnReturn = false;
        if (!clsOracleMrcf.IsHasEmployeeDataOnOracle(Request.Cookies["Speedo"]["UserName"]))
        {
            divError.Visible = true;
            lblErrMsg.Text = "Unable to create an MRCF Request.<br>" +
                             "<table>" +
                              "<tr>" +
                               "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                               "<td>Error has been encountered please contact your System Administrator.</td>" +
                              "</tr>" +
                             "</table>";
            blnReturn = false;
        }
        else
        {
            blnReturn = true;
        }
        return blnReturn;
    }

    protected bool ValidateApprover()
    {
        bool blnReturn = false;
        if (ddlDivision.SelectedValue.ToString().Length == 0 || ddlGrpHead.SelectedValue.ToString().Length == 0)
        {
            divError.Visible = true;
            lblErrMsg.Text = "Unable to create an MRCF Request.<br>" +
                             "<table>" +
                              "<tr>" +
                               "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                               "<td>Department/Division approver was not defined.</td>" +
                              "</tr>" +
                             "</table>";
            blnReturn = false;
        }
        else
        {
            blnReturn = true;
        }
        return blnReturn;
    }

    protected bool ValidateItem()
    {
        bool blnReturn = false;
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
            blnReturn = false;
        }
        else
        {
            blnReturn = true;
        }
        return blnReturn;
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (!ValidateEmployee())
        {
            return;
        }
        if (!ValidateItem())
        {
            return;
        }

        if (!ValidateApprover())
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(txtIntended.Text))
        {
            btnSend.Enabled = true;
            btnSend.Text = "Submit";
            return;
        }

        clsMRCF objNewMRCF = new clsMRCF();
        objNewMRCF.Username = Request.Cookies["Speedo"]["UserName"];
        objNewMRCF.RequestType = ddlType.SelectedValue.ToString();
        objNewMRCF.Intended = txtIntended.Text;
        objNewMRCF.ChargeTo = ddlChargeTo.SelectedValue.ToString();
        objNewMRCF.DivisionHead = ddlDivision.SelectedValue.ToString();
        objNewMRCF.GroupHead = ddlGrpHead.SelectedValue.ToString();
        objNewMRCF.ProcurementManager = ConfigurationManager.AppSettings["ProcurementManager"];
        if (objNewMRCF.Insert(ViewState["Cart"] as DataTable) > 0)
        {
            Response.Redirect("MRCFMenu.aspx");
        }
        
    }

    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {
            LoadAirlines();  

            DataTable tblCart = ViewState["Cart"] as DataTable;
            DataRow drowCart = tblCart.NewRow();

            string strItemUnit = "";

            string strItemDescription = "";
            //Item Description
            if (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true)
            {
                strItemDescription = ddlItem.SelectedItem.Text;
            }
            else
            {
                strItemDescription = txtItem.Text;
            }

            //Unit of measurement
            //Line type Code for Service
            if (clsMrcfLineType.IsHasUnitOfMeasurement(ddlLineType.SelectedValue.ToString()) == false)
            {
                strItemUnit = "LOT";
            }

            else
            {
                strItemUnit = ddlUnit.SelectedValue.ToString();
            }

            drowCart["itemdesc"] = strItemDescription;
            drowCart["itemspec"] = txtSpec.Text;
            drowCart["GLAccount"] = (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true ? clsMRCFItem.GetGLAccountItems(clsOracleMrcf.GetCategoryId(ddlItem.SelectedValue.ToString())) : clsMRCFGLAccount.GetGLAccountCode(ddlTransactionType.SelectedValue.ToString(), clsEmployee.GetDivisionCode(Request.Cookies["Speedo"]["UserName"]), clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"])));
            drowCart["LineType"] = ddlLineType.SelectedValue.ToString();
            drowCart["TransactionType"] = (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true ? clsOracleMrcf.GetCategoryId(ddlItem.SelectedValue.ToString()) : ddlTransactionType.SelectedValue.ToString());
            drowCart["Destination"] = clsMrcfLineType.GetDestinationTypeCode(ddlLineType.SelectedValue.ToString());
            drowCart["Item"] = (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true ? ddlItem.SelectedValue.ToString() : "");
            drowCart["qty"] = txtQty.Text;
            drowCart["unit"] = strItemUnit;
            drowCart["dateneed"] = dteDateNeeded.Date.ToShortDateString();
            //new
            if (ddlTransactionType.Visible != false)
            {
                drowCart["TransactionDesc"] = ddlTransactionType.SelectedItem.Text + " - [" + drowCart["TransactionType"] + "]";
            }
            else { string strTansType;
            strTansType = drowCart["TransactionType"].ToString();
            drowCart["TransactionDesc"] = clsMRCFItem.GetTransactionTypeName(strTansType) + " - [" + drowCart["TransactionType"] + "]";
            }
               
            //(clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true ? clsOracleMrcf.GetCategoryId(ddlItem.SelectedValue.ToString()) : ddlTransactionType.SelectedValue.ToString()) + "]";
            drowCart["LineTypeDesc"] = ddlLineType.SelectedItem.Text + " - [" + ddlLineType.SelectedValue.ToString() + "]";
            if (ddlTransactionType.SelectedValue.ToString() == "4135")
            {
                string EmpName;
                string Birthdate;
                string BirthdateView;
                string empBdate =  ddlMonth.Text + " " + ddlDays.Text + ", " + ddlYear.Text;
                if (ckbOthers.Checked == true) { EmpName = txtNameOthers.Text; BirthdateView = empBdate; Birthdate = empBdate; }
                else
                {
                    string conBirthdate;
                    conBirthdate = Convert.ToDateTime(ddlEmployees.SelectedValue).ToString("yyyy");
                    EmpName = ddlEmployees.SelectedItem.Text;
                    BirthdateView = Convert.ToDateTime(ddlEmployees.SelectedValue).ToString("MMMM dd, ") + conBirthdate.Substring(0, 2) + "??";
                    Birthdate = Convert.ToDateTime(ddlEmployees.SelectedValue).ToString("MMMM dd, yyyy");
                }
                drowCart["EmpnameView"] = "<tr><td>Name : " + EmpName + "<br> <br>";
                drowCart["BirthDateView"] = "Birthdate : " + BirthdateView + "</td></tr>";

                drowCart["Empname"] = EmpName;
                drowCart["BirthDate"] = Birthdate;
            }
            else
            {
                drowCart["Empname"] = "";
                drowCart["BirthDate"] = "";
             
            }

            tblCart.Rows.Add(drowCart);
            ViewState["Cart"] = tblCart;
            dgItems.DataSource = tblCart;
            dgItems.DataBind();
            
            txtItem.Text = "";
            txtQty.Text = "";
            DateTime dteDate = clsMRCF.GetMinimumDateNeeded(ddlType.SelectedValue);
            dteDateNeeded.MinDate = dteDate;
            dteDateNeeded.Date = dteDate;
            txtSpec.Text = "";

            trNoRequest.Visible = dgItems.Items.Count == 0;
        }
        catch
        {
            Response.Redirect("MRCFNew.aspx");
        }
    }

    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            tblCart.Rows[e.Item.ItemIndex].Delete();
            ViewState["Cart"] = tblCart;

            dgItems.DataSource = tblCart;
            dgItems.DataBind();

            trNoRequest.Visible = dgItems.Items.Count == 0;
        }
        catch
        {
            Response.Redirect("MRCFNew.aspx");
        }
    }

    protected void ddlChargeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadApprover(ddlChargeTo.SelectedValue);
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dteDate = clsMRCF.GetMinimumDateNeeded(ddlType.SelectedValue);
        dteDateNeeded.MinDate = dteDate;
        dteDateNeeded.Date = dteDate;

        DataTable tblCart = ViewState["Cart"] as DataTable;
        foreach (DataRow drow in tblCart.Rows)
        {
            drow["dateneed"] = dteDate;
        }
        ViewState["Cart"] = tblCart;
        dgItems.DataSource = tblCart;
        dgItems.DataBind();
    }


    protected void ddlLineType_SelectedIndexChanged(object sender, EventArgs e)
    {
        trItemsCategory.Visible = false;
        ddlTransactionType.DataSource = clsMRCFTransactionType.GetDataSourceList(ddlLineType.SelectedValue.ToString());
        ddlTransactionType.DataBind();

        if (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true)
        {
            if (ddlLineType.SelectedValue.ToString() == "1")
            {
                trItemsCategory.Visible = false;
            }
            else
            {
                trItemsCategory.Visible = true;
            }

            trItems.Visible = true;
            trCategory.Visible = false;
        }
        else
        {
            trItems.Visible = false;
            trCategory.Visible = true;

        }

        ddlItem.DataSource = clsOracleMrcf.GetDataSourceListItems(ddlLineType.SelectedValue.ToString(), ddlItemCategory.SelectedItem.Value.ToString());
        ddlItem.DataBind();


        //Unit of measurement
        //Line type Code for Service
        if (clsMrcfLineType.IsHasUnitOfMeasurement(ddlLineType.SelectedValue.ToString()) == true)
        {
            trItemUnit.Visible = true;

            if (clsMrcfLineType.GetDestinationTypeCode(ddlLineType.SelectedValue.ToString()).ToUpper() == "INVENTORY")
            {
                LoadUnit();

            }

            else
            {
                LoadUnitAll();
            }
        }

        else
        {
            trItemUnit.Visible = false;
        }

        //Item Description
        if (clsMrcfLineType.IsHasItemDesc(ddlLineType.SelectedValue.ToString()) == true)
        {
            trItemDescription.Visible = true;
        }
        else
        {
            trItemDescription.Visible = false;
            txtItem.Text = "";
        }

        //Travel Conditions
        if (ddlTransactionType.SelectedValue.ToString() == "4135")
        {
            // divAirlines.Attributes.Add("style", "block");
            LoadAirlines();
            trAirfare.Visible = true;
            trEmployee.Visible = true;
            ckbOthers.Checked = false;
            txtNameOthers.Visible = false;
        }
        else
        {
            trAirfare.Visible = false;
            trEmployee.Visible = false;
            // divAirlines.Attributes.Add("style", "none");
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnit();
    }

    protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {
            if (ddlTransactionType.SelectedValue.ToString() == "4135")
            {
               // divAirlines.Attributes.Add("style", "block");
                LoadAirlines();
                trAirfare.Visible = true;
                trEmployee.Visible = true;
                ckbOthers.Checked = false;
                txtNameOthers.Visible = false;
            }
            else
            {
                trAirfare.Visible = false;
                trEmployee.Visible = false;
               // divAirlines.Attributes.Add("style", "none");
            }
        }
     
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlItem.DataSource = clsOracleMrcf.GetDataSourceListItems(ddlLineType.SelectedValue.ToString(), ddlItemCategory.SelectedItem.Value.ToString());
        ddlItem.DataBind();

        LoadUnit();
    }

    protected void ddlGrpHead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void LoadAirlines()
    {
        HtmlTableCell CellMain = tAirlines;

        HtmlTable tblAirlines = new HtmlTable();
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell Cell = new HtmlTableCell();

        CellMain.Controls.Clear();

        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            DataTable tblReturn = new DataTable();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Airldesc,url FROM cis.mrcfairlines WHERE status = '1' ORDER BY airldesc ASC";
            cn.Open();
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
            foreach(DataRow dr in tblReturn.Rows)
            {
                HtmlAnchor A = new HtmlAnchor();

                HyperLink links = new HyperLink();
                links.Text = "<li>" + dr["airldesc"].ToString() +"</li>";
                links.Target = "_blank";
                links.NavigateUrl = dr["url"].ToString();
                Cell.Controls.Add(links);
            }
            cn.Close();
        if (tblReturn.Rows.Count == 0 )
        {
            Label lbl = new Label();
            lbl.Text = "No available Airlines reference";
            Cell.Controls.Add(lbl);
        }
        }
        
        tblAirlines.Attributes.Add("cellspacing", "0");
        tblAirlines.Attributes.Add("cellpadding", "0");
        row.Cells.Add(Cell);
        tblAirlines.Rows.Add(row);
        tAirlines.Controls.Add(tblAirlines);
               
    }

    protected void ckbOthers_CheckedChanged(object sender, EventArgs e)
    {
        LoadAirlines();
        if (ckbOthers.Checked == true)
        {
            txtNameOthers.Visible = true;
            ddlEmployees.Visible = false;
            trBirthdate.Visible = true;
        }
        else
        { 
            txtNameOthers.Visible = false; 
            ddlEmployees.Visible = true; 
            trBirthdate.Visible = false; 
        }
    }


    protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAirlines();
    }

    private void LoadMonths()
    {
        LoadAirlines();
        string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

        for (int x = 0; x < 12; x++)
        {
            ddlMonth.Items.Add(monthNames[x].ToString());
        }

        int yr = DateTime.Now.Year;
        for (int y = 1940; y < yr + 1; y++)
        {
            ddlYear.Items.Add(y.ToString());
        }
  
    }

    private void LoadDays()
    {     
        ddlDays.Items.Clear();
        int dys = DateTime.DaysInMonth(int.Parse(ddlYear.Text), ddlMonth.SelectedIndex + 1);
        for (int x = 1; x < dys + 1; x++)
        {
            ddlDays.Items.Add(x.ToString());
        }
        LoadAirlines();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDays();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDays();
    }

    protected void ddlDays_SelectedIndexChanged(object sender, EventArgs e)
    {
       // LoadDays();
    }
}
