<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="branchdelete.aspx.cs" Inherits="CMD_SIS_branchdelete" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
    <script>
        $(window).on('load', function () {
            $('#myModalDelete').modal('show');
        });
    </script>
<div class="panel panel-danger" style="height:500px;">
   <div class="panel-heading">WARNING!</div>
   <div class="panel-body"> 
   
      
   </div>
</div>
           <!-- Modal Delete Branches Pop up -->
<div id="myModalDelete" class="modal fade" style="position:fixed; width:100%; margin-top:250px;" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog modal-sm">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="background-color:indianred;">      
        <h4 class="modal-title" style="text-align:center; color:white;">Branch Deleted</h4>
      </div>
      <div class="modal-body">
        <p>You have deleted a branch successfully!</p>
        <a style="margin-left:30%;" type="button" class="btn btn-default" href="BranchesSettings.aspx" >Ok, Thanks</a>
      </div>
    </div>
  </div>
</div>



</asp:Content>

