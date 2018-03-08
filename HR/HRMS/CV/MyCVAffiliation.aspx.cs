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

public partial class HR_HRMS_CV_MyCVAffiliation : System.Web.UI.Page
{

   private void ClearFields()
   {
      txtOrganization.Text = "";
      txtPosition.Text = "";
      txtInclusiveDate.Text = "";
      txtDetails.Text = "";
   }

   private void BindAffiliationList()
   {
      dgAffiliation.DataSource = clsEmployeeAffiliation.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
      dgAffiliation.DataBind();

      if (dgAffiliation.Items.Count == 0)
      {
         divAffiliation.Visible = false;
         lblAffiliationNoRec.Visible = true;
         btnReset.Visible = false;
         btnSave.Visible = false;
      }
      else
      {
         divAffiliation.Visible = true;
         lblAffiliationNoRec.Visible = false;
         btnReset.Visible = true;
         btnSave.Visible = true;
      }
   }

   ///////// Form Events /////////

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         BindAffiliationList();
      }
   }

   protected void dgAffiliation_DeleteCommand(object source, DataGridCommandEventArgs e)
   {
      HiddenField phdnAffiCode = (HiddenField)e.Item.FindControl("hdnAffiCode");
      clsEmployeeAffiliation er = new clsEmployeeAffiliation(phdnAffiCode.Value);
      er.Delete();
      BindAffiliationList();
   }

   protected void btnSave_Click(object sender, EventArgs e)
   {
      foreach (DataGridItem itm in dgAffiliation.Items)
      {
         HiddenField phdnAffiCode = (HiddenField)itm.FindControl("hdnAffiCode");
         TextBox ptxtOrganization = (TextBox)itm.FindControl("txtOrganization");
         TextBox ptxtPosition = (TextBox)itm.FindControl("txtPosition");
         TextBox ptxtDetails = (TextBox)itm.FindControl("txtDetails");
         TextBox ptxtInclusiveDate = (TextBox)itm.FindControl("txtInclusiveDate");

         clsEmployeeAffiliation ea = new clsEmployeeAffiliation();
         ea.AffiliationCode = phdnAffiCode.Value;
         ea.Organization = ptxtOrganization.Text;
         ea.Position = ptxtInclusiveDate.Text;
         ea.InclusiveDates = ptxtInclusiveDate.Text;
         ea.Remarks = (ptxtDetails.Text.Length > 255 ? ptxtDetails.Text.Substring(0, 255) : ptxtDetails.Text);
         ea.Edit();
      }
      BindAffiliationList();
   }

   protected void btnReset_Click(object sender, EventArgs e)
   {
      BindAffiliationList();
   }

   protected void btnAdd_Click(object sender, EventArgs e)
   {
      clsEmployeeAffiliation ea = new clsEmployeeAffiliation();
      ea.Username = Request.Cookies["Speedo"]["UserName"].ToString();
      ea.Organization = txtOrganization.Text;
      ea.Position = txtPosition.Text;
      ea.InclusiveDates = txtInclusiveDate.Text;
      ea.Remarks = (txtDetails.Text.Length > 255 ? txtDetails.Text.Substring(0, 255) : txtDetails.Text);
      ea.Add();

      ClearFields();
      BindAffiliationList();
   }

}