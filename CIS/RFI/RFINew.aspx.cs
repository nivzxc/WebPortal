using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HRMS;
using STIeForms;

public partial class CIS_RFI_RFINew : System.Web.UI.Page
{

 protected void MakeCart()
 {
  DataTable tblItems = new DataTable("RequestedItems");
  tblItems.Columns.Add("ItemDesc", System.Type.GetType("System.String"));
  tblItems.Columns.Add("ItemDetails", System.Type.GetType("System.String"));
  tblItems.Columns.Add("DateNeeded", System.Type.GetType("System.DateTime"));
  ViewState["RequestedItems"] = tblItems;
 }

 /////////////////////////////// 
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!Page.IsPostBack)
  {
   trNoRequest.Visible = true;
   this.MakeCart();

   txtRequestorName.Text = clsEmployee.GetName(Request.Cookies["Speedo"]["UserName"], EmployeeNameFormat.LastFirst);
   hdnDepartmentCode.Value = clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"]);

   using (clsEmployee objEmployee = new clsEmployee())
   {
    objEmployee.Username = RFI.ProcurementHead;
    objEmployee.Fill();
    txtProcurementName.Text = objEmployee.LastName + ", " + objEmployee.FirstName;
   }

   DataTable tblDepartmentApprover = clsDepartmentApprover.DSLApproverDepartment(hdnDepartmentCode.Value, EFormType.RFI);
   ddlApprover.DataSource = tblDepartmentApprover;
   ddlApprover.DataValueField = "pvalue";
   ddlApprover.DataTextField = "ptext";
   ddlApprover.DataBind();
   if (ddlApprover.Items.Count == 0)
   {
    ddlApprover.Items.Add(new ListItem("No Approver Defined", "none"));
   }
   hdnProcurementCode.Value = RFI.ProcurementHead;
   dteDateNeeded.MinDate = clsDateTime.AddDaysWorking(5);
   dteDateNeeded.Date = clsDateTime.AddDaysWorking(5);
  }
 }

 protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
 {
  try
  {
   DataTable tblItems = ViewState["RequestedItems"] as DataTable;
   tblItems.Rows[e.Item.ItemIndex].Delete();
   ViewState["RequestedItems"] = tblItems;

   dgItems.DataSource = tblItems;
   dgItems.DataBind();

   trNoRequest.Visible = dgItems.Items.Count == 0;
  }
  catch
  {
   Response.Redirect("RFINew.aspx");
  }
 }

 protected void btnSend_Click(object sender, ImageClickEventArgs e)
 {
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
  else
  {
   DataTable tblItems = ViewState["RequestedItems"] as DataTable;
   RFI objRFI = new RFI();
   objRFI.Username = Request.Cookies["Speedo"]["UserName"];
   objRFI.Intended = txtIntended.Text;
   objRFI.Approver = ddlApprover.SelectedValue;
   objRFI.ApproverStatus = "F";
   objRFI.ProcurementManager = hdnProcurementCode.Value;
   objRFI.ProcurementManagerStatus = "F";
   objRFI.Insert(tblItems);

   string strApproverMail = clsUsers.GetEmail(ddlApprover.SelectedValue);
   string strApproverName = clsEmployee.GetName(ddlApprover.SelectedValue);
   RFI.SendNotification(RFI.RFIMailType.SentToApprover, txtRequestorName.Text, strApproverName, strApproverMail, objRFI.RFICode);
   RFI.SendNotification(RFI.RFIMailType.SentToRequestor, txtRequestorName.Text, strApproverName, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), objRFI.RFICode);

   Response.Redirect("RFIMain.aspx");
  }
 }

 protected void btnSaveAdd_Click(object sender, ImageClickEventArgs e)
 {
  try
  {
   DataTable tblItems = ViewState["RequestedItems"] as DataTable;
   DataRow drw = tblItems.NewRow();
   drw["ItemDesc"] = txtItem.Text;
   drw["ItemDetails"] = txtItemDetails.Text;
   drw["DateNeeded"] = dteDateNeeded.Date.ToShortDateString();
   tblItems.Rows.Add(drw);
   ViewState["RequestedItems"] = tblItems;
   dgItems.DataSource = tblItems;
   dgItems.DataBind();

   txtItem.Text = "";
   txtItemDetails.Text = "";
   dteDateNeeded.MinDate = clsDateTime.AddDaysWorking(5);
   dteDateNeeded.Date = clsDateTime.AddDaysWorking(5);

   trNoRequest.Visible = dgItems.Items.Count == 0;
  }
  catch
  {
   Response.Redirect("RFINew.aspx");
  }
 }

}