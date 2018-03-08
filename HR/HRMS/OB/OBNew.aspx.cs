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
using eWorld.UI;
using HRMS;

public partial class HR_HRMS_OB_OBNew : System.Web.UI.Page
{

   protected void MakeCart()
   {
      DataTable tblCart = new DataTable("Cart");
      tblCart.Columns.Add("focsdate", System.Type.GetType("System.DateTime"));
      tblCart.Columns.Add("keyin", System.Type.GetType("System.DateTime"));
      tblCart.Columns.Add("keyout", System.Type.GetType("System.DateTime"));
      ViewState["Cart"] = tblCart;
   }

   protected void LoadDepartmentApprover(string pDepartmentCode)
   {
      DataTable tblDepartmentApprover = clsDepartmentApprover.DSLApproverDepartment(pDepartmentCode, EFormType.OfficialBussiness);
      ddlRequestApprover.DataSource = tblDepartmentApprover;
      ddlRequestApprover.DataValueField = "pvalue";
      ddlRequestApprover.DataTextField = "ptext";
      ddlRequestApprover.DataBind();
   }

   protected void BindDefaultSchedule()
   {
      using (clsShift shift = new clsShift())
      {
         shift.ShiftCode = clsShift.GetDayShiftCode(Request.Cookies["Speedo"]["UserName"], dtpOBDate.SelectedDate);
         shift.Fill();
         dtpInTime.SelectedTime = shift.TimeStart;
         dtpOutTime.SelectedTime = shift.TimeEnd;
      }
   }

   ///////////////////////////////
   ///////// Form Events /////////
   ///////////////////////////////

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      //btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
      btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
       if (!Page.IsPostBack)
      {
         // string strProcessScript = "this.value='Submitting...';this.disabled=true;";
          //btnSend.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSend, "").ToString());
         
         MakeCart();

         txtRequestorName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

         ddlDepartment.DataSource = clsDepartment.GetDdlDs();
         ddlDepartment.DataValueField = "pValue";
         ddlDepartment.DataTextField = "pText";
         ddlDepartment.DataBind();

         ddlHeadApprover.DataSource = clsDepartmentApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], EFormType.OfficialBussiness);
         ddlHeadApprover.DataValueField = "pvalue";
         ddlHeadApprover.DataTextField = "ptext";
         ddlHeadApprover.DataBind();

         dtpOBDate.SelectedDate = DateTime.Now;
         BindDefaultSchedule();
         divScheduleList.Visible = false;
      }
   }

   protected void ddlOBType_SelectedIndexChanged(object sender, EventArgs e)
   {
      trRDepartment.Visible = ddlOBType.SelectedValue == "1";
      trRApprover.Visible = ddlOBType.SelectedValue == "1";
      LoadDepartmentApprover(ddlDepartment.SelectedValue);
   }

   protected void btnSend_Click(object sender, EventArgs e)
   {
      string strErrorMessage = "";

      if (dgSchedule.Items.Count == 0)
         strErrorMessage += "<br>You should file atleast 1 schedule.";

      if (strErrorMessage.Length == 0)
      {
         using (clsOB ob = new clsOB())
         {
            ob.Username = Request.Cookies["Speedo"]["UserName"];
            ob.DateFile = DateTime.Now;
            ob.Reason = txtReason.Text;
            ob.OBType = ddlOBType.SelectedValue;
            if (ddlOBType.SelectedValue == "1")
            {
               ob.DepartmentCode = ddlDepartment.SelectedValue;
               ob.ApproverRequestorName = ddlRequestApprover.SelectedValue;
            }
            else
            {
               ob.DepartmentCode = clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"]);
               ob.ApproverRequestorName = "";
            }
            ob.ApproverHeadName = ddlHeadApprover.SelectedValue;
            ob.Insert();

            DataTable tblCart = ViewState["Cart"] as DataTable;

            foreach (DataGridItem itm in dgSchedule.Items)
            {
               HiddenField phdnFocusDate = (HiddenField)itm.FindControl("hdnFocusDate");
               CalendarPopup pdtpKeyOBDate = (CalendarPopup)itm.FindControl("dtpKeyOBDate");
               TimePicker pdtpKeyInTime = (TimePicker)itm.FindControl("dtpKeyInTime");
               TimePicker pdtpKeyOutTime = (TimePicker)itm.FindControl("dtpKeyOutTime");

               clsOBDetails obdetails = new clsOBDetails();
               obdetails.OBCode = ob.OBCode;
               obdetails.FocusDate = clsValidator.CheckDate(phdnFocusDate.Value);
               obdetails.KeyIn = clsDateTime.CombineDateTime(pdtpKeyOBDate.SelectedDate, pdtpKeyInTime.SelectedTime);
               obdetails.KeyOut = clsDateTime.CombineDateTime(pdtpKeyOBDate.SelectedDate, pdtpKeyOutTime.SelectedTime);
               obdetails.Status = "1";
               obdetails.UpdateBy = Request.Cookies["Speedo"]["UserName"];
               obdetails.UpdateOn = DateTime.Now;
               obdetails.Add();
            }

            if (ob.OBType == "0")
            {
               ob.SendNotification(OBMailType.FiledAcknowledgementHRequestor);
               ob.SendNotification(OBMailType.FiledNotificationHApprover);
            }
            else
            {
               ob.SendNotification(OBMailType.FiledAcknowledgementRRequestor);
               ob.SendNotification(OBMailType.FiledNotificationRApprover);
            }

         }
         //ADDED by CALVIN CAVITE FEB 15, 2018
         ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalSuccess();", true);
         
         //Response.Redirect("OBMenu.aspx");
            
      }
      else
      {
         divError.Visible = true;
         lblErrMsg.Text = strErrorMessage;
      }
   }

   protected void btnBack_Click(object sender, EventArgs e)
   {
      Response.Redirect("OBMenu.aspx");
   }

   protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
   {
      LoadDepartmentApprover(ddlDepartment.SelectedValue);
   }

    //Raffy 09-21-12
    //Validate OB Date Function
   private Boolean validateOBDate(DateTime pDt,DataTable pTbl)
   {
       Boolean RetVal = true;
       for (int i = 0; i < pTbl.Rows.Count; i++)
       {
           DateTime dt=Convert.ToDateTime(pTbl.Rows[i].ItemArray[0].ToString());
           if(pDt.ToShortDateString()==dt.ToShortDateString())
           {
               RetVal = false;
               break;
           }
       }
           return RetVal;
   }
    //end raffy 09-21-12

   protected void btnAddNewItem_Click(object sender, EventArgs e)
   {
      if (clsDateTime.CombineDateTime(dtpOBDate.SelectedDate, dtpInTime.SelectedTime) >= clsDateTime.CombineDateTime(dtpOBDate.SelectedDate, dtpOutTime.SelectedTime))
      {
         divError.Visible = true;
         lblErrMsg.Text = "Invalid Date Entries.";
      }
      else
      {
         try
         {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            int intDays = clsValidator.CheckInteger(ddlDays.SelectedValue);
            for (int intCtr = 0; intCtr < intDays; intCtr++)
            {
                if (validateOBDate(dtpOBDate.SelectedDate.AddDays(intCtr), tblCart))
                {
                    DataRow drowCart = tblCart.NewRow();
                    drowCart["focsdate"] = dtpOBDate.SelectedDate.AddDays(intCtr);
                    drowCart["keyin"] = clsDateTime.CombineDateTime(dtpOBDate.SelectedDate, dtpInTime.SelectedTime).AddDays(intCtr);
                    drowCart["keyout"] = clsDateTime.CombineDateTime(dtpOBDate.SelectedDate, dtpOutTime.SelectedTime).AddDays(intCtr);
                    tblCart.Rows.Add(drowCart);
                }
            }
            dtpOBDate.SelectedDate = DateTime.Now;

            dgSchedule.DataSource = tblCart;
            dgSchedule.DataBind();
            divScheduleList.Visible = dgSchedule.Items.Count > 0;
            lblNoOBSchedule.Visible = !divScheduleList.Visible;
         }
         catch { Response.Redirect("OBNew.aspx"); }
         divError.Visible = false;
      }
   }

   protected void dgSchedule_DeleteCommand(object source, DataGridCommandEventArgs e)
   {
      try
      {
         DataTable tblCart = ViewState["Cart"] as DataTable;
         tblCart.Rows[e.Item.ItemIndex].Delete();
         ViewState["Cart"] = tblCart;

         dgSchedule.DataSource = tblCart;
         dgSchedule.DataBind();
      }
      catch
      {
         Response.Redirect("OBNew.aspx");
      }
   }

   protected void dtpInDate_DateChanged(object sender, EventArgs e)
   {
      BindDefaultSchedule();
   }

}