using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class HR_HRMS_ATW_ATWNew : System.Web.UI.Page
{

    protected void CreateDataTable()
    {
        DataTable tblATWSchedule = new DataTable("ATWSchedule");
        tblATWSchedule.Columns.Add("datestrt", System.Type.GetType("System.DateTime"));
        tblATWSchedule.Columns.Add("dateend", System.Type.GetType("System.DateTime"));
        tblATWSchedule.Columns.Add("reason", System.Type.GetType("System.String"));
        ViewState["ATWSchedule"] = tblATWSchedule;
    }

    ///////////////////////////////
    ///////// Page Events /////////
    ///////////////////////////////

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        //btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
        if (!Page.IsPostBack)
        {
            
            //string strProcessScript = "this.value='" + clsString.Submit + "';this.disabled=true;";
           // btnSend.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSend, "").ToString());

            CreateDataTable();
            ddlDivision.DataSource = clsModuleApprover.GetDSLDivisionHeadApprover(Request.Cookies["Speedo"]["UserName"], clsModule.ATWModule);
            ddlDivision.DataValueField = "pvalue";
            ddlDivision.DataTextField = "ptext";
            ddlDivision.DataBind();

            using (clsEmployee employee = new clsEmployee())
            {
                employee.Username = Request.Cookies["Speedo"]["UserName"];
                employee.Fill();
                lblRequestorName.Text = employee.FirstName + " " + employee.MiddleInitial + ". " + employee.LastName;
               
            }

            ddlApprover.DataSource = clsModuleApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], clsModule.ATWModule, "1");
            ddlApprover.DataValueField = "pvalue";
            ddlApprover.DataTextField = "ptext";
            ddlApprover.DataBind();

            divScheduleList.Visible = false;

            dtpDateStart.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0);
            dtpDateEnd.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);
        }
    }

    protected void btnAddNewItem_Click(object sender, EventArgs e)
    {
        string strErrorMessage = "";

        if (dtpDateStart.Date > dtpDateEnd.Date)
            strErrorMessage = "Invalid date start and date end.";

        if (DateTime.Now >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0) && (clsDateTime.GetDateOnly(dtpDateStart.Date) == clsDateTime.GetDateOnly(DateTime.Now) || clsDateTime.GetDateOnly(dtpDateEnd.Date) == clsDateTime.GetDateOnly(DateTime.Now)))
            strErrorMessage = "Invalid specified date. Deadline of submission has been reached.";

        if (strErrorMessage.Length == 0)
        {
            try
            {
                DataTable tblATWSchedule = ViewState["ATWSchedule"] as DataTable;
                DataRow drwATWS = tblATWSchedule.NewRow();
                drwATWS["datestrt"] = dtpDateStart.Date;
                drwATWS["dateend"] = dtpDateEnd.Date;
                drwATWS["reason"] = txtScheduleReason.Text;
                tblATWSchedule.Rows.Add(drwATWS);

                dgSchedule.DataSource = tblATWSchedule;
                dgSchedule.DataBind();
                divScheduleList.Visible = dgSchedule.Items.Count > 0;
                lblNoATWSchedule.Visible = !divScheduleList.Visible;

                txtScheduleReason.Text = "";
            }
            catch { Response.Redirect("ATWNew.aspx"); }
            divError.Visible = false;
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = strErrorMessage;
        }
    }

    protected void dgSchedule_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblATWSchedule = ViewState["ATWSchedule"] as DataTable;
            tblATWSchedule.Rows[e.Item.ItemIndex].Delete();
            ViewState["ATWSchedule"] = tblATWSchedule;

            dgSchedule.DataSource = tblATWSchedule;
            dgSchedule.DataBind();
        }
        catch
        {
            Response.Redirect("ATWNew.aspx");
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string strErrorMessage = "";
        string strUsername = Request.Cookies["Speedo"]["UserName"];

        if (dgSchedule.Items.Count == 0)
            strErrorMessage += "<br>You should file atleast 1 schedule.";

        if (ddlDivision.SelectedValue.ToString().Length == 0 || ddlApprover.SelectedValue.ToString().Length == 0)
            strErrorMessage += "<br>Department/Division approver was not defined.";

        if (strErrorMessage.Length == 0)
        {
            DataTable tblATWSchedule = ViewState["ATWSchedule"] as DataTable;
            using (clsATW atw = new clsATW())
            {
                atw.Username = strUsername;
                atw.DateFile = DateTime.Now;
                atw.Reason = txtReason.Text;
                atw.ApproverHeadName = ddlApprover.SelectedValue.ToString();
                atw.ApproverHeadStatus = (strUsername == ddlApprover.SelectedValue ? "A" : "F");
                //atw.ApproverDivisionName = hdnApproverDivision.Value;
                atw.ApproverDivisionName = ddlDivision.SelectedValue.ToString();
                atw.ApproverDivisionStatus = "F";
                atw.Status = "F";
                atw.CreateBy = strUsername;
                atw.CreateOn = DateTime.Now;
                atw.Insert(tblATWSchedule);

                if (atw.ApproverHeadStatus == "A")
                {
                    atw.SendNotification(ATWMailType.FiledAcknowledgementDRequestor);
                    atw.SendNotification(ATWMailType.FiledNotificationDApprover);
                }
                else
                {
                    atw.SendNotification(ATWMailType.FiledAcknowledgementHRequestor);
                    atw.SendNotification(ATWMailType.FiledNotificationHApprover);
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert("+ atw.ATWCode + ")", true);
            }
            //Response.Redirect("ATWMenu.aspx");
            
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = strErrorMessage;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ATWMenu.aspx");
    }



}