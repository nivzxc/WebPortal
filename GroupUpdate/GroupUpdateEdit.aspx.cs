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

public partial class GroupUpdate_GroupUpdateEdit : System.Web.UI.Page
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

        //ckeContents.config.toolbar = new object[]
        //    {
        //        new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "SpellChecker", "Scayt" },
        //        new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
        //        "/",
        //        new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
        //        new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
        //        new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
        //        "/",
        //        new object[] { "Styles", "Format", "Font", "FontSize" },
        //        new object[] { "TextColor", "BGColor" },
        //    };

        ckeContents.config.toolbar = new object[]
			{
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "SpellChecker", "Scayt" },
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
                new object[] { "Image", "Table", "HorizontalRule", "Smiley", "SpecialChar" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				"/",
				new object[] { "Styles", "Format", "Font", "FontSize" },
				new object[] { "TextColor", "BGColor" },
			};

        if (!Page.IsPostBack)
        {
            

            string strUsername = Request.Cookies["Speedo"]["UserName"];
            int intGroupUpdateCode = Request.QueryString["GroupUpdateCode"].ToString().ToInt();
            using (clsGroupUpdate objView = new clsGroupUpdate())
            {
                objView.GroupUpdateCode = intGroupUpdateCode;
                objView.Fill();

                txtTitle.Text = objView.Title;
                txtDescription.Text = objView.Description;
                lblGroupHead.Text = clsEmployee.GetName(GroupUpdateApproval.GetApproverName(intGroupUpdateCode.ToString(), "1"));
                txtGroupHeadRemark.Text = GroupUpdateApproval.GetApproverRemarks(intGroupUpdateCode.ToString(), "1");
                //lblDivisionHead.Text = clsEmployee.GetName(GroupUpdateApproval.GetApproverName(intGroupUpdateCode.ToString(), "2"));
                //txtDivisionHeadRemark.Text = GroupUpdateApproval.GetApproverRemarks(intGroupUpdateCode.ToString(), "2");
                txtPhotoSource.Text = objView.PhotoSource;
                hdnGroupHead.Value = GroupUpdateApproval.GetApprover(GroupUpdateApproval.GroupUpdateUserType.GroupHead, clsEmployee.GetDepartmentCode(strUsername));
                ckeContents.Text = objView.Content;
                txtContributor.Text = objView.Contributor;
            }

        }
    }
    protected bool Validate()
    {
        bool blnReturn = false;
        divError.Visible = false;
        string strErrorMessage = "";

        if (fuAttachment.HasFile)
        {
            if (fuAttachment.FileName.Contains(".jpg") || fuAttachment.FileName.Contains(".jpeg") || fuAttachment.FileName.Contains(".png") || fuAttachment.FileName.Contains(".JPG") || fuAttachment.FileName.Contains(".JPEG") || fuAttachment.FileName.Contains(".PNG") || fuAttachment.FileName.Contains(".GIF") || fuAttachment.FileName.Contains(".gif"))
            {
                System.Drawing.Image uploadedImage = System.Drawing.Image.FromStream(fuAttachment.PostedFile.InputStream);
                Single height = uploadedImage.PhysicalDimension.Height;
                Single width = uploadedImage.PhysicalDimension.Width;

                if (height != 250 || width != 420)
                {
                    strErrorMessage += " Image dimension must be exactly 420 pixels for width and 250 pixels for height.<br>";
                }
            }
            else
            {
                strErrorMessage += "Only the following format will be accepted <b>.JPG, .JPEG and .PNG</b>.<br/>";
            }
        }

        if (strErrorMessage.Length > 0)
        {
            divError.Visible = true;
            lblErrMsg.Text = strErrorMessage;
            blnReturn = false;
        }
        else
        {
            blnReturn = true;
        }


        return blnReturn;
    }

    protected void SaveData(bool pFinalize)
    {
        string username = Request.Cookies["Speedo"]["UserName"];
        string fileName = "";
        bool blnIsApprover = false;

        if (Validate())
        {
            using (clsGroupUpdate objEdit = new clsGroupUpdate())
            {
                fileName = System.Guid.NewGuid().ToString() + Path.GetExtension(fuAttachment.FileName);
                int intGroupUpdateCode = Request.QueryString["GroupUpdateCode"].ToString().ToInt();
                objEdit.GroupUpdateCode = intGroupUpdateCode;
                objEdit.Title = txtTitle.Text;
                objEdit.Description = txtDescription.Text;
                objEdit.Content = ckeContents.Text;
                objEdit.DepartmentCode = clsEmployee.GetDepartmentCode(username);
                objEdit.DivisionCode = clsEmployee.GetDivisionCode(username);
                objEdit.CreateBy = username;
                objEdit.ModifyBy = username;
                objEdit.Status = hdnGroupHead.Value == username ? "1" : "0";
                objEdit.Enabled = "1";
                objEdit.Contributor = txtContributor.Text;
                objEdit.PhotoSource = txtPhotoSource.Text;

                if (fuAttachment.HasFile)
                {
                    objEdit.ImageFilename = fileName;
                }
                else
                {
                    objEdit.ImageFilename = "";
                }

                if (pFinalize == true)
                {
                    if (objEdit.Status == "1")
                    {
                        blnIsApprover = true;
                    }
                }
                else
                {
                    //Save As draft
                    objEdit.Status = "5";
                }


                if (objEdit.Update(blnIsApprover) > 0)
                {
                    if (fuAttachment.HasFile)
                    {
                        fuAttachment.SaveAs(Server.MapPath(@"~\UploadedFiles\GroupUpdates\" + fileName));
                    }

                    if (hndIsPreview.Value == string.Empty)
                    {
                        Response.Redirect("GroupUpdateMain.aspx");
                    }
                }
            }
        }
    }

       protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        hndIsPreview.Value = "TRUE";
        SaveData(false);
        btnSaveAsDraft.Visible = false;
        btnPreview.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        hndIsPreview.Value = "";
        SaveData(true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupUpdateMain.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (clsGroupUpdate.VoidGroupUpdate(Request.QueryString["GroupUpdateCode"].ToString()) > 0)
        {
            Response.Redirect("GroupUpdateMain.aspx");
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        hndIsPreview.Value = "TRUE";
        SaveData(false);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('GroupUpdatePreview.aspx?GroupUpdateCode=" + Request.QueryString["GroupUpdateCode"].ToString() + "');", true);
    
    }
}