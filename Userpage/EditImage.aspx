<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="EditImage.aspx.cs" Inherits="Userpage_EditImage" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true"> 
 <table width="100%" cellpadding="0" cellspacing="0">
<%--   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="ControlPanel.aspx" class="SiteMap">Control Panel</a> » 
     <a href="EditImage.aspx" class="SiteMap">Edit Image</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>--%>
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Edit Images</span></b>
     <br />
     <br />
     <br />
     <br />
    
     <%
      //using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
      //{
      // cn.Open();
      // SqlCommand cmd = cn.CreateCommand();
      // cmd.CommandText = "SELECT avatar,username FROM users WHERE username='" + Request.Cookies["MySTIHQ"]["username"] + "'";
      // SqlDataReader dr = cmd.ExecuteReader();
      // dr.Read();
      // if (!Convert.IsDBNull(dr["avatar"]))
      // {
      //  Response.ContentType = "image/pjpeg";
      //  Response.BinaryWrite((byte[])dr["avatar"]);
      // }
      // dr.Close();
      //}
     %>
     <asp:Image runat="server" ID="imgAvatar" />
     <br />
     <br />
     <span class="HeaderText">Your Avatar</span>
     <br />
     <span style="color:#4682b4;">
      Upload your avatar.
      <br />
      <b>Note:</b> Your avatar should be 50 x 50 in size and in jpeg format.
     </span>     
     <br />
     <asp:FileUpload runat="server" ID="fldAvatar" CssClass="controls" Width="400px" Font-Size="Small" />
     <br />
     <asp:Label runat="server" ID="lblErrAvatar" Visible="false" Font-Bold="true" Font-Size="Small" ForeColor="red"></asp:Label>
     <br />
     <br />
     <br />
     <asp:Image runat="server" ID="imgRealPic" />
     <br />          
     <span class="HeaderText">Your Real Picture</span>
     <br />
     <span style="color:#4682b4;">Post that smile. Upload your real picture.
     <br>
     <b>Note:</b> Your avatar should be 150 x 200 in size and in jpeg format.
     </span>
     <br />            
     <asp:FileUpload runat="server" ID="fldRealPic" CssClass="controls" Width="400px" Font-Size="Small" Enabled="true" />
     <br />
     <asp:Label runat="server" ID="lblErrRealPic" Visible="false" Font-Bold="true" Font-Size="Small" ForeColor="red"></asp:Label>     
     <br />
     <br />
     <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSave_Click" />
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>