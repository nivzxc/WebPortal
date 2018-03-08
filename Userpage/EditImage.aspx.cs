using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Userpage_EditImage : System.Web.UI.Page
{
 
	protected void Page_Load(object sender, EventArgs e)
    {
		if (File.Exists(Server.MapPath("~/pictures/avatar/") + Request.Cookies["Speedo"]["username"] + ".jpg"))
			imgAvatar.ImageUrl = "~/pictures/avatar/" + Request.Cookies["Speedo"]["username"] + ".jpg";
		else
			imgAvatar.ImageUrl = "~/pictures/avatar/default.jpg";

		if (File.Exists(Server.MapPath("~/pictures/realpicture/") + Request.Cookies["Speedo"]["username"] + ".jpg"))
			imgRealPic.ImageUrl = "~/pictures/realpicture/" + Request.Cookies["Speedo"]["username"] + ".jpg";
		else
			imgRealPic.ImageUrl = "~/pictures/realpicture/default.jpg";
		
    }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		//if (fldAvatar.PostedFile.FileName.Trim().Length > 0 && fldAvatar.PostedFile.ContentLength > 0)
		//{
		// using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		// {
		//  using (SqlCommand cmd = cn.CreateCommand())
		//  {
		//   cn.Open();

		//   int intImgLen = fldAvatar.PostedFile.ContentLength;
		//   byte[] picbyte = new byte[intImgLen];
		//   Stream ImageStream = fldAvatar.PostedFile.InputStream;
		//   ImageStream.Read(picbyte, 0, intImgLen);

		//   cmd.CommandText = "UPDATE users SET avatar=@avatar WHERE username='" + Request.Cookies["MySTIHQ"]["username"] + "'";
		//   cmd.Parameters.Add("@avatar", SqlDbType.Image);
		//   cmd.Parameters["@avatar"].Value = picbyte;
		//   cmd.ExecuteNonQuery();
		//  }
		// }
		//}

		if (fldAvatar.HasFile)
		{
            string strImageType = fldAvatar.PostedFile.ContentType.ToString().ToLower();
            System.Drawing.Image imgAvatar = System.Drawing.Image.FromStream(fldAvatar.PostedFile.InputStream);

            float imgWidth = imgAvatar.PhysicalDimension.Width;
			float imgeHeight = imgAvatar.PhysicalDimension.Height;

			if (imgWidth <= 50 && imgeHeight <= 50)
			{				
				lblErrAvatar.Visible = false;
				if (strImageType == "image/pjpeg" || strImageType == "image/jpeg")
					fldAvatar.SaveAs(Server.MapPath("~/pictures/avatar/" + Request.Cookies["Speedo"]["username"] + ".jpg"));
				else
				{
					lblErrAvatar.Visible = true;
					lblErrAvatar.Text = "Error: Invalid picture type." + strImageType;
                    
				}
			}
			else
			{
				lblErrAvatar.Visible = true;
				lblErrAvatar.Text = "Error: Invalid picture size.";
			}
		}

		if (fldRealPic.HasFile)
		{
			string strRealImageType = fldRealPic.PostedFile.ContentType.ToString().ToLower();
			System.Drawing.Image imgReal = System.Drawing.Image.FromStream(fldRealPic.PostedFile.InputStream);

			float imgRealWidth = imgReal.PhysicalDimension.Width;
			float imgRealHeight = imgReal.PhysicalDimension.Height;

			if (imgRealWidth <= 150 && imgRealHeight <= 200)
			{
				lblErrAvatar.Visible = false;
				if (strRealImageType == "image/pjpeg" || strRealImageType == "image/jpeg")
					fldRealPic.SaveAs(Server.MapPath("~/pictures/realpicture/" + Request.Cookies["Speedo"]["username"] + ".jpg"));
				else
				{
					lblErrRealPic.Visible = true;
					lblErrRealPic.Text = "Error: Invalid picture type.";
				}
			}
			else
			{
				lblErrRealPic.Visible = true;
				lblErrRealPic.Text = "Error: Invalid picture size.";
			}
		}	

	}

}
