using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HRMS;

public partial class HR_HRMS_Overtime_OvertimeNew : System.Web.UI.Page
{

    protected void LoadDepartmentApprover(string pDepartmentCode)
    {
        DataTable tblDepartmentApprover = clsModuleApprover.DSLApproverDepartment(pDepartmentCode, clsModule.OvertimeModule, "1");
        ddlRequestApprover.DataSource = tblDepartmentApprover;
        ddlRequestApprover.DataValueField = "pvalue";
        ddlRequestApprover.DataTextField = "ptext";
        ddlRequestApprover.DataBind();
    }

    private void CheckLateFiling()
    {
        DateTime dteOTStart = clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime);
        trApproverCOO.Visible = (DateTime.Now > dteOTStart);
    }

    private Boolean IsLateFiling()
    {
        Boolean blnReturn = false;
        DateTime dteOTStart = clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime);
        blnReturn = (DateTime.Now > dteOTStart);
        return blnReturn;
    }

    ///////////////////////////////
    ///////// Form Events /////////
    ///////////////////////////////

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
        //btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
        if (!Page.IsPostBack)
        {
            //string strProcessScript = "this.value='Submitting...';this.disabled=true;";
            //btnSend.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSend, "").ToString());
           
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            lblRequestorName.Text = clsUsers.GetName(strUsername);
            dtpFromTime.SelectedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 30, 0);
            dtpToTime.SelectedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 30, 0);
            txtHours.Text = "1";

            ddlRequestDepartment.DataSource = clsDepartment.GetDdlDs();
            ddlRequestDepartment.DataValueField = "pValue";
            ddlRequestDepartment.DataTextField = "pText";
            ddlRequestDepartment.DataBind();

            ddlHeadApprover.DataSource = clsModuleApprover.DSLApproverEmployee(strUsername, clsModule.OvertimeModule, "1");
            ddlHeadApprover.DataValueField = "pvalue";
            ddlHeadApprover.DataTextField = "ptext";
            ddlHeadApprover.DataBind();

            DataTable tblDivisionApprover = clsModuleApprover.DSLApproverEmployee(strUsername, clsModule.OvertimeModule, "2");
            if (tblDivisionApprover.Rows.Count > 0)
            {
                ddlDivisionHead.DataSource = tblDivisionApprover;
                ddlDivisionHead.DataValueField = "pvalue";
                ddlDivisionHead.DataTextField = "ptext";
                ddlDivisionHead.DataBind();
                //hdnApproverDivision.Value = tblDivisionApprover.Rows[0]["pvalue"].ToString();
                //txtApproverDivision.Text = tblDivisionApprover.Rows[0]["ptext"].ToString();
            }

            DataTable tblCOOApprover = clsModuleApprover.DSLApprover(clsModule.OvertimeModule, "3");
            if (tblCOOApprover.Rows.Count > 0)
            {
                hdnApproverCOO.Value = tblCOOApprover.Rows[0]["pvalue"].ToString();
                txtApproverCOO.Text = tblCOOApprover.Rows[0]["ptext"].ToString();
            }
        }
    }

    protected void ddlOTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        trRDepartment.Visible = ddlOTType.SelectedValue == "1";
        trRApprover.Visible = ddlOTType.SelectedValue == "1";
        LoadDepartmentApprover(ddlRequestDepartment.SelectedValue);
    }

    protected void ddlRequestDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDepartmentApprover(ddlRequestDepartment.SelectedValue);
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string strErrorMessage = "";
        string strUsername = Request.Cookies["Speedo"]["UserName"];
        string strEmployeeDepartmentCode = clsEmployee.GetDepartmentCode(strUsername);
        bool blnDHRequired = clsOvertime.IsDivisionHeadApproverRequired(Request.Cookies["Speedo"]["UserName"], clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime), clsDateTime.CombineDateTime(dtpToDate.SelectedDate, dtpToTime.SelectedTime));

        if (clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime) >= clsDateTime.CombineDateTime(dtpToDate.SelectedDate, dtpToTime.SelectedTime))
            strErrorMessage += "<br>You entered an invalid date bracket.";

        if (strErrorMessage.Length == 0)
        {
            using (clsOvertime ot = new clsOvertime())
            {
                ot.Username = strUsername;
                ot.DateFile = DateTime.Now;
                ot.DateStart = clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime);
                ot.DateEnd = clsDateTime.CombineDateTime(dtpToDate.SelectedDate, dtpToTime.SelectedTime);
                ot.Units = clsValidator.CheckFloat(txtHours.Text);
                ot.Reason = txtReason.Text;
                ot.ChargeType = ddlOTType.SelectedValue;
                if (ddlOTType.SelectedValue == "0")
                {
                    ot.DepartmentCode = strEmployeeDepartmentCode;
                    ot.ApproverRequestorName = "";
                    ot.ApproverRequestorStatus = "0";
                }
                else
                {
                    ot.DepartmentCode = ddlRequestDepartment.SelectedValue;
                    ot.ApproverRequestorName = ddlRequestApprover.SelectedValue;
                    ot.ApproverRequestorStatus = "F";
                }

                ot.ApproverHeadName = ddlHeadApprover.SelectedValue.ToString();
                ot.ApproverHeadStatus = (strUsername == ddlHeadApprover.SelectedValue ? "A" : "F");
                //ot.ApproverDivisionName = hdnApproverDivision.Value;
                ot.ApproverDivisionName = ddlDivisionHead.SelectedValue.ToString();
                if (IsLateFiling())
                {
                    ot.ApproverDivisionStatus = "F";
                    ot.ApproverCOOName = "";
                    ot.ApproverCOOStatus = "X";
                }
                else
                {
                    ot.ApproverDivisionStatus = (blnDHRequired ? "F" : (clsModuleApprover.IsApprovalRequiredDepartment(ddlDivisionHead.SelectedValue.ToString(), clsModule.OvertimeModule, "2", strEmployeeDepartmentCode) ? "F" : "X"));
                    ot.ApproverCOOName = "";
                    ot.ApproverCOOStatus = "X";
                }

                //ot.ApproverCOOName = hdnApproverCOO.Value;
                //ot.ApproverCOOStatus = (DateTime.Now > clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime) ? "F" : "X");

                ot.Insert();
            }
            //ADDED by CALVIN CAVITE FEB 15, 2018
            ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalSuccess();", true);

 
        
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = strErrorMessage;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OvertimeMenu.aspx");
    }

    protected void dtpFromDate_DateChanged(object sender, EventArgs e)
    {
        txtHours.Text = clsDateTime.DateDiff(pDateFormat.Hour, clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime), clsDateTime.CombineDateTime(dtpToDate.SelectedDate, dtpToTime.SelectedTime)).ToString();
        // CheckLateFiling();
    }

    protected void dtpToDate_DateChanged(object sender, EventArgs e)
    {
        txtHours.Text = clsDateTime.DateDiff(pDateFormat.Hour, clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime), clsDateTime.CombineDateTime(dtpToDate.SelectedDate, dtpToTime.SelectedTime)).ToString();
        // CheckLateFiling();
    }

    protected void dtpFromTime_TimeChanged(object sender, EventArgs e)
    {
        txtHours.Text = clsDateTime.DateDiff(pDateFormat.Hour, clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime), clsDateTime.CombineDateTime(dtpToDate.SelectedDate, dtpToTime.SelectedTime)).ToString();
        // CheckLateFiling();  
    }

    protected void dtpToTime_TimeChanged(object sender, EventArgs e)
    {
        txtHours.Text = clsDateTime.DateDiff(pDateFormat.Hour, clsDateTime.CombineDateTime(dtpFromDate.SelectedDate, dtpFromTime.SelectedTime), clsDateTime.CombineDateTime(dtpToDate.SelectedDate, dtpToTime.SelectedTime)).ToString();
        //CheckLateFiling();
    }

}