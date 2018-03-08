<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="BranchesSettings.aspx.cs" Inherits="CMD_SIS_Schools" %>

<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
<asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="SISMenu.aspx" class="SiteMap">SIS</a> » 
     <a href="Schools.aspx" class="SiteMap">Schools</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>--%>
    
  <tr>
   <td>
    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Philippine First Branch Settings</span></b>
     <br />
     <br />
         <div class="panel panel-info">
         <div class="panel-heading"><font style="font-weight:bold;">New Branch</font></div>
         <div class="panel-body">             
                 <asp:UpdatePanel ID="brnch" runat="server">
                     <ContentTemplate>
                         <div class="form-group-sm row">
                             <div class="col-xs-12">
                                <asp:Label ID="lbBranchnm" text="Branch Name" runat="server"></asp:Label>
                                <asp:TextBox ID="tbBranchnm" require="required" class="form-control" runat="server" required></asp:TextBox>
                             </div>
                             
                         </div>
                         <div class="form-group-sm row">                             
                             <div class="col-xs-12">
                                <asp:Label ID="lbBranchaddress" text="Branch Address" runat="server"></asp:Label>
                                <asp:TextBox ID="tbBranchaddress" class="form-control" runat="server" required></asp:TextBox>
                             </div>
                         </div>
                         <div class="form-group-sm row">
                             <div class="col-xs-6">
                                <asp:Label ID="lbBranchEmail" text="Email Address" runat="server"></asp:Label>
                                <asp:TextBox ID="tbBranchEmail" class="form-control" runat="server" ></asp:TextBox>
                             </div>
                             <div class="col-xs-6">
                                <asp:Label ID="lbBranchcontact" text="Contact No." runat="server"></asp:Label>
                                <asp:TextBox ID="tbBranchcontact" class="form-control" runat="server" required></asp:TextBox>
                             </div>
                         </div>
                         <div class="form-group-sm row">
                             <div class="col-xs-6">
                                <asp:Label ID="lbBranchmnger" text="Branch Assigned Manager" runat="server"></asp:Label>
                                <asp:TextBox ID="tbBranchmnger" class="form-control" runat="server" required></asp:TextBox>
                             </div>
                             <div class="col-xs-6">
                                 <br>
                                 <asp:Button  class="pull-right btn btn-success" ID="btnSend" runat="server" Text="Submit" ValidationGroup="save" onclick="btnSave_Click"/>
                             </div>
                         </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>               
         </div>
    </div>
  
     <div class="GridBorder">
          <div class="panel panel-info">
            <div class="panel-heading">                
                <div class="row">
                    <div class="col-xs-6">
                        <font style="font-weight:bold;">Branch Details</font>
                    </div>
                    <div class="col-xs-6">
                        <asp:ImageButton class="pull-right" ID="btnExport" runat="server" 
                        ImageUrl="~/Support/btnExportToExcel.jpg" onclick="btnExport_Click" />
                    </div>                  
                </div>
            </div>
                <div class="panel-body">  
                    
                      <table style="width:100%;"class="grid">    
                       <tr>        
                        <td class="GridColumns" style="width:100px;"><b>Branch Name</b></td>
                        <td class="GridColumns" style="width:90px;"><b>Branch Address</b></td>
                        <td class="GridColumns" style="width:30px;"><b>Landline</b></td>
                        <td class="GridColumns" style="width:100px;"><b>Assigned Manager</b></td>  
                        <td class="GridColumns" style="width:10px;"><b></b></td>  
                       </tr>
                       <% LoadBranches(); %>
                      </table>
                </div>
          </div>    
     </div>
    </div>    
    
    </td>
   </tr>
     
 </table>

    
<!-- Modal Success Entry -->
<div id="myModalSuccessEntry" class="modal fade" role="dialog" style="position:fixed; width:100%; margin-top:150px;" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog modal-sm">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="background-color:lightgreen;">
      
        <h4 class="modal-title" style="text-align:center; color:white;">Success!</h4>
      </div>
      <div class="modal-body">
        <p>You have added new branch successfully!</p>
           <a style="margin-left:30%;" type="button" class="btn btn-default" href="BranchesSettings.aspx" >Ok, Thanks</a>
      </div>
    </div>

  </div>
</div>

</asp:Content>