<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master"
	CodeFile="Default.aspx.cs" Inherits="_Default" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
        <script type="text/javascript" src="jquery.min.js"></script>
    <script type="text/javascript" src="jquery.accessible-news-slider.js"></script>
     <script type="text/javascript">
            // when the DOM is ready, convert the feed anchors into feed content
            jQuery(document).ready(function () {

                jQuery('#newsslider').accessNews({""});
            });
    </script>
</asp:Content>
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" runat="Server" Visible="true">
    
    <div class="divNews">
    <div class="container-fluid">
        <div class="panel panel-primary" style="margin-top:20px;">
            <div class="panel-body">
                      NEWS HERE...
                     <ul id="newsslider">         
		                <% LoadNews(); %>	
                    </ul>
            </div>
        </div>
    </div>
  
    </div>

    <br/>

    <div class="container" style="border: 2px; border-color:Black; width:648px; height:100%;">
        <div class="panel panel-danger">
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <% LoadNewEmployees(); %>    
                    
                    </div>
                    <div class="col-lg-6">
                        <% LoadTodayBirthday(); %>
                        <% LoadNextBirthday(); %>
                    </div>
                </div>                
            </div>        
        </div>
      
    </div>

</asp:Content>
