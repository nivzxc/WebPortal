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

public partial class GroupUpdate_GroupUpdateDetailsMain : System.Web.UI.Page
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
            lblContributor.Text = objView.Contributor;
            lblTitle.Text = objView.Title;
            lblCreateBy.Text = clsEmployee.GetName(objView.CreateBy);
            lblCreateOn.Text = Convert.ToDateTime(objView.CreateOn).ToString("MMM dd, yyyy");
            //lblLink.Text = "<a href='../UploadedFiles/GroupUpdates/" + objView.ImageFilename + "' target='_blank'>View Image</a>";
            lblImage.Text = "<img src='../UploadedFiles/GroupUpdates/" + objView.ImageFilename + "' width='200' height='119' alt='' /></a>";
            lblDescription.Text = objView.Description;
            lblContent.Text = objView.Content;
            lblStatus.Text = strStatus;
            lblPhotoSource.Text = objView.PhotoSource;
            lblGroupHead.Text = clsEmployee.GetName(GroupUpdateApproval.GetApproverName(intGroupUpdateCode.ToString(), "1"));
            txtGroupHeadRemark.Text = GroupUpdateApproval.GetApproverRemarks(intGroupUpdateCode.ToString(), "1");
            //lblDivisionHead.Text = clsEmployee.GetName(GroupUpdateApproval.GetApproverName(intGroupUpdateCode.ToString(), "2"));
            //txtDivisionHeadRemark.Text = GroupUpdateApproval.GetApproverRemarks(intGroupUpdateCode.ToString(), "2");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupUpdateMain.aspx");
    }
}