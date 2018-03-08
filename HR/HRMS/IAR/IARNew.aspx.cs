using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_IAR_IARNew : System.Web.UI.Page
{

    ///////// Page Events /////////

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        //btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
        if (!Page.IsPostBack)
        {
            //string strProcessScript = "this.value='" + clsString.Submit + "';this.disabled=true;";
            //btnSend.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSend, "").ToString());

            ddlDivision.DataSource = clsModuleApprover.GetDSLDivisionHeadApprover(Request.Cookies["Speedo"]["UserName"], clsModule.IARModule);
            ddlDivision.DataValueField = "pvalue";
            ddlDivision.DataTextField = "ptext";
            ddlDivision.DataBind();

            using (clsEmployee employee = new clsEmployee())
            {
                employee.Username = Request.Cookies["Speedo"]["UserName"];
                employee.Fill();
                txtRequestorName.Text = employee.FirstName + " " + employee.MiddleInitial + ". " + employee.LastName;

                //Update By Charlie Bachiller 10-19-2012
                //hdnDivisionHead.Value = clsDivision.GetDivisionHead(employee.DivisionCode);
                ////hdnDivisionHead.Value = clsModuleApprover.GetApproverDivisionHead(Request.Cookies["Speedo"]["UserName"], clsModule.IARModule);
                //txtApproverDivision.Text = clsEmployee.GetName(hdnDivisionHead.Value, EmployeeNameFormat.FirstLast);
            }

            ddlApprover.DataSource = clsModuleApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], clsModule.IARModule, "1");
            ddlApprover.DataValueField = "pvalue";
            ddlApprover.DataTextField = "ptext";
            ddlApprover.DataBind();

            dtpDateStart.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);
            dtpDateEnd.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string strErrorMessage = "";
        string strUsername = Request.Cookies["Speedo"]["UserName"];

        if (dtpDateStart.Date > dtpDateEnd.Date)
            strErrorMessage = clsString.InvalidDateStartEnd;

        if (DateTime.Now >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0) && (clsDateTime.GetDateOnly(dtpDateStart.Date) == clsDateTime.GetDateOnly(DateTime.Now) || clsDateTime.GetDateOnly(dtpDateEnd.Date) == clsDateTime.GetDateOnly(DateTime.Now)))
            strErrorMessage = clsString.InvalidDateDeadlineReach;

        if (ddlDivision.SelectedValue.ToString().Length == 0 || ddlApprover.SelectedValue.ToString().Length == 0)
            strErrorMessage += clsString.NotDefinedApprovers;

        if (strErrorMessage.Length == 0)
        {
            using (clsIAR iar = new clsIAR())
            {
                iar.Username = strUsername;
                iar.DateFile = DateTime.Now;
                iar.DateStart = dtpDateStart.Date;
                iar.DateEnd = dtpDateEnd.Date;
                iar.Reason = txtReason.Text;
                iar.ApproverHeadName = ddlApprover.SelectedValue.ToString();
                iar.ApproverHeadStatus = (strUsername == ddlApprover.SelectedValue ? "A" : "F");
                //iar.ApproverDivisionName = hdnDivisionHead.Value;
                iar.ApproverDivisionName = ddlDivision.SelectedValue.ToString();
                iar.ApproverDivisionStatus = "F";
                iar.Status = "F";
                iar.CreateBy = strUsername;
                iar.CreateOn = DateTime.Now;
                iar.Insert();

                if (iar.ApproverHeadStatus == "A")
                {
                    iar.SendNotification(IARMailType.FiledAcknowledgementDRequestor);
                    iar.SendNotification(IARMailType.FiledNotificationDApprover);
                }
                else
                {
                    iar.SendNotification(IARMailType.FiledAcknowledgementHRequestor);
                    iar.SendNotification(IARMailType.FiledNotificationHApprover);
                }
            }
            Response.Redirect("IARMenu.aspx");
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = strErrorMessage;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("IARMenu.aspx");
    }

}