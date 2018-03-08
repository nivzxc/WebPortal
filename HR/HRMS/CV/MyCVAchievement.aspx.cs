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
using GrayMatterSoft;
using HRMS;

public partial class HR_HRMS_CV_MyCVAchievement : System.Web.UI.Page
{


   private void ClearFields()
   {
      txtAchievement.Text = "";
      txtDetails.Text = "";
      txtDateAchieved.Text = "";
   }

   private void BindAchievementList()
   {
      dgAchievement.DataSource = clsEmployeeAchievement.GetDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
      dgAchievement.DataBind();

      if (dgAchievement.Items.Count == 0)
      {
         divAchievement.Visible = false;
         lblAchievementNoRec.Visible = true;
         btnReset.Visible = false;
         btnSave.Visible = false;
      }
      else
      {
         divAchievement.Visible = true;
         lblAchievementNoRec.Visible = false;
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
         BindAchievementList();
      }
   }

   protected void btnSave_Click(object sender, EventArgs e)
   {
      foreach (DataGridItem itm in dgAchievement.Items)
      {
         HiddenField phdnAchvCode = (HiddenField)itm.FindControl("hdnAchvCode");
         TextBox ptxtAchievement = (TextBox)itm.FindControl("txtAchievement");
         TextBox ptxtDetails = (TextBox)itm.FindControl("txtRemarks");
         TextBox ptxtAchiveDate = (TextBox)itm.FindControl("txtAchiveDate");

         clsEmployeeAchievement ea = new clsEmployeeAchievement();
         ea.AchievementCode = phdnAchvCode.Value;
         ea.Achievement = ptxtAchievement.Text;
         ea.AchievementDate = ptxtAchiveDate.Text;
         ea.Details = (ptxtDetails.Text.Length > 255 ? ptxtDetails.Text.Substring(0, 255) : ptxtDetails.Text);
         ea.Edit();
      }
      BindAchievementList();
   }

   protected void btnReset_Click(object sender, EventArgs e)
   {
      BindAchievementList();
   }

   protected void dgEducation_DeleteCommand(object source, DataGridCommandEventArgs e)
   {
      HiddenField phdnAchvCode = (HiddenField)e.Item.FindControl("hdnAchvCode");
      clsEmployeeAchievement ea = new clsEmployeeAchievement(phdnAchvCode.Value);
      ea.Delete();
      BindAchievementList();
   }

   protected void btnAdd_Click(object sender, EventArgs e)
   {
      clsEmployeeAchievement ea = new clsEmployeeAchievement();
      ea.Username = Request.Cookies["Speedo"]["UserName"].ToString();
      ea.Achievement = txtAchievement.Text;
      ea.AchievementDate = txtDateAchieved.Text;
      ea.Details = (txtDetails.Text.Length > 255 ? txtDetails.Text.Substring(0, 255) : txtDetails.Text);
      ea.Add();

      ClearFields();
      BindAchievementList();
   }

}