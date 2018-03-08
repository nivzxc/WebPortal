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

public partial class GroupUpdate_GroupUpdateAdd : System.Web.UI.Page
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

        //<CKEditor:CKEditorControl ID="ckeContents" runat="server" BackColor="White" 
        //CssClass="controls" Height="300px" ToolbarFull="Cut|Copy|Paste|PasteText|PasteFromWord|-|SpellChecker|Scayt
        //        Undo|Redo|-|Find|Replace|-|Bold|Italic|Underline|Strike
        //        Image|Table|HorizontalRule|Smiley|SpecialChar
        //        /   
        //        NumberedList|BulletedList|-|Outdent|Indent|Blockquote|CreateDiv
        //        JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock
        //        Link|Unlink|Anchor
        //        TextColor|BGColor
        //        Subscript|Superscript
        //        /
        //        Styles|Format|Font|FontSize" Width="98%" />

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


        //if (hndGroupUpdateCode.Value != string.Empty)
        //{
        //    //Response.Redirect("GroupUpdatePreview.aspx?GroupUpdateCode=" + hdnGroupHead.Value);
        //    btnPreview.OnClientClick = "javascript:window.open('GroupUpdatePreview.aspx?GroupUpdateCode=" + hndGroupUpdateCode.Value + "');";
        //}



        if (!Page.IsPostBack)
        {
            string username = Request.Cookies["Speedo"]["UserName"];
            
                hdnGroupHead.Value = GroupUpdateApproval.GetApprover(GroupUpdateApproval.GroupUpdateUserType.GroupHead, clsEmployee.GetDepartmentCode(username));
                //hdnDivisionHead.Value = GroupUpdateApproval.GetApprover(GroupUpdateApproval.GroupUpdateUserType.DivisionHead, clsEmployee.GetDepartmentCode(username));

        }
    }

    protected bool Validate()
    {
        bool blnReturn = false;
        divError.Visible = false;
        string strErrorMessage = "";

        if (hndGroupUpdateCode.Value == string.Empty && hndIsPreview.Value == string.Empty)
        {
            if (hdnGroupHead.Value.Length == 0)
            {
                strErrorMessage += "No group head has been assigned to your department, please contact your system administrator.<br/>";
            }

            if (!fuAttachment.HasFile)
            {
                strErrorMessage += "Image attachement is required.<br/>";
            }

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
        else
        {
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
        DataTable tblApprover = new DataTable();
        tblApprover.Columns.Add("ApprovalLevel");
        tblApprover.Columns.Add("Username");

        //Add Grouphead in datatable
        DataRow drnew = tblApprover.NewRow();
        drnew["ApprovalLevel"] = "1";
        drnew["Username"] = hdnGroupHead.Value;
        tblApprover.Rows.Add(drnew);

        //Add DivisionHead in datatable
        //drnew = tblApprover.NewRow();
        //drnew["ApprovalLevel"] = "2";
        //drnew["Username"] = hdnDivisionHead.Value;
        //tblApprover.Rows.Add(drnew);

        if (Validate())
        {
            using (clsGroupUpdate objAdd = new clsGroupUpdate())
            {
                fileName = System.Guid.NewGuid().ToString() + Path.GetExtension(fuAttachment.FileName);
                objAdd.Title = txtTitle.Text;
                objAdd.Description = txtDescription.Text;
                objAdd.Content = ckeContents.Text;
                objAdd.DepartmentCode = clsEmployee.GetDepartmentCode(username);
                objAdd.DivisionCode = clsEmployee.GetDivisionCode(username);
                objAdd.Contributor = txtContributor.Text;
                objAdd.PhotoSource = txtPhotoSource.Text;
                objAdd.CreateBy = username;
                objAdd.ModifyBy = username;
                objAdd.Status = hdnGroupHead.Value == username ? "1" : "0";
                objAdd.Enabled = "1";
                objAdd.ImageFilename = fileName;

                if (pFinalize == true)
                {
                    if (objAdd.Status == "1")
                    {
                        blnIsApprover = true;
                    }
                }
                else
                {
                    //Save As draft
                    objAdd.Status = "5";
                }

                if (hndGroupUpdateCode.Value == string.Empty)
                {
                    int intGroupUpdateCode = objAdd.Insert(tblApprover, blnIsApprover);
                    if (intGroupUpdateCode > 0)
                    {
                        fuAttachment.SaveAs(Server.MapPath(@"~\UploadedFiles\GroupUpdates\" + fileName));

                        if (pFinalize == true)
                        {
                            Response.Redirect("GroupUpdateMain.aspx");
                        }
                        else
                        {
                            hndGroupUpdateCode.Value = intGroupUpdateCode.ToString();
                        }
                    }
                }
                else
                {
                    if (fuAttachment.HasFile)
                    {
                        objAdd.ImageFilename = fileName;
                    }
                    else
                    {
                        objAdd.ImageFilename = "";
                    }

                    objAdd.GroupUpdateCode = hndGroupUpdateCode.Value.ToString().ToInt();
                    if (objAdd.Update(blnIsApprover) > 0)
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
    }

   
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        hndIsPreview.Value = "";
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
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        hndIsPreview.Value = "TRUE";
        SaveData(false);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('GroupUpdatePreview.aspx?GroupUpdateCode=" + hndGroupUpdateCode.Value + "');", true);
    }
}