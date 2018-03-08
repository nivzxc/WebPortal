using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using STIeForms;
using System.Drawing.Imaging;
using HRMS;
using HqWeb.GroupUpdate;

public partial class GroupUpdate_GroupUpdateGH : System.Web.UI.Page
{
    protected void ValidateUser()
    {
        string username = Request.Cookies["Speedo"]["UserName"];
        if (!(clsGroupUpdate.HasAccess(username) || clsGroupUpdate.IsGroupApprover(username) || clsGroupUpdate.IsDivisionApprover(username)))
        { Response.Redirect("../AccessDenied.aspx"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        ValidateUser();
        string strUsername = Request.Cookies["Speedo"]["UserName"];
        int intGroupUpdateCode = Request.QueryString["GroupUpdateCode"].ToString().ToInt();
        string strStatus = "";
        using (clsGroupUpdate objView = new clsGroupUpdate())
        {
            objView.GroupUpdateCode = intGroupUpdateCode;
            objView.Fill();
            if (objView.Status == "0")
            {
                strStatus = "For Approval";
            }

            else if (objView.Status == "1")
            {
                strStatus = "Approved";
            }

            else if (objView.Status == "2")
            {
                strStatus = "Disapproved";
            }
            else if (objView.Status == "3")
            {
                strStatus = "Voided";
            }

            lblTitle.Text = objView.Title;
            lblCOntributor.Text = objView.Contributor;
            lblCreateBy.Text = clsEmployee.GetName(objView.CreateBy);
            lblCreateOn.Text = Convert.ToDateTime(objView.CreateOn).ToString("MMM dd, yyyy");
            //lblLink.Text = "<a href='../UploadedFiles/GroupUpdates/" + objView.ImageFilename + "' target='_blank'>View Image</a>";
            lblImage.Text = "<img src='../UploadedFiles/GroupUpdates/" + objView.ImageFilename + "' width='200' height='119' alt='' /></a>";
            lblDescription.Text = objView.Description;
            lblPhotoBy.Text = objView.PhotoSource;
            lblContent.Text = objView.Content;
            lblStatus.Text = strStatus;
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (GroupUpdateApproval.ApprovedLevel1(Request.QueryString["GroupUpdateCode"].ToString(), Request.Cookies["Speedo"]["UserName"],txtRemark.Text) > 0)
        {
            Response.Redirect("GroupUpdateMain.aspx");
        }
    }
    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        if (GroupUpdateApproval.DisApprovedLevel1(Request.QueryString["GroupUpdateCode"].ToString(), Request.Cookies["Speedo"]["UserName"], txtRemark.Text) > 0)
        {
            Response.Redirect("GroupUpdateMain.aspx");
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (GroupUpdateApproval.Modification(Request.QueryString["GroupUpdateCode"].ToString(), Request.Cookies["Speedo"]["UserName"], txtRemark.Text) > 0)
        {
            Response.Redirect("GroupUpdateMain.aspx");
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('GroupUpdatePreview.aspx?GroupUpdateCode=" + Request.QueryString["GroupUpdateCode"].ToString() + "');", true);
    }
}