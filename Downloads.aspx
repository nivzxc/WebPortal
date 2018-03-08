<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="Downloads.aspx.cs" Inherits="Downloads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">CMD</a> » 
     <a href="CheckListContent.aspx" class="SiteMap">Checklist</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Downloads</span></b>
     <br />
     <br />

     <div style='font-weight:bold'>HR Forms</div>
       <div style="padding-left:6px; padding-top:4px">
         <asp:Menu ID="Menu1" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
<%--                 <asp:MenuItem Text="Banclife" Value="Banclife">
                     <asp:MenuItem Text="Banclife Individual Application Form" Value="Banclife Individual Application Form" NavigateUrl="Downloads/HRForm/Banclife_Individual_Application_Form.pdf" Target="_blank"></asp:MenuItem>
                 </asp:MenuItem>--%>

                 <asp:MenuItem Text="BIR" Value="BIR">
                    <asp:MenuItem Text="Application For Registration - BIR Form 1902" Value="Application For Registration - BIR Form 1902" NavigateUrl="Downloads/HRForm/Application_For_Registration_BIR_Form_1902.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Application For Registration Information Update - BIR Form 1905" Value="Application For Registration Information Update - BIR Form 1905" NavigateUrl="Downloads/HRForm/Application_For_Registration_Information_Update_BIR_Form_1905.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Certification Of Update - BIR Form 2305" Value="Certification Of Update - BIR Form 2305" NavigateUrl="Downloads/HRForm/BIR_2305.pdf" Target="_blank"></asp:MenuItem>
                 </asp:MenuItem>

                 <asp:MenuItem Text="Pag-ibig" Value="Pag-ibig">
                    <asp:MenuItem Text="Calamity Loan Form" Value="Calamity Loan Form"  NavigateUrl="Downloads/HRForm/HDMF_Calamity_Loan_Form.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Member's Change of Information - M2-2" Value="Member's Change of Information - M2-2" NavigateUrl="Downloads/HRForm/HDMF_M2-2.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Request for Transfer" Value="Request for Transfer" NavigateUrl="Downloads/HRForm/HDMF_Request_for_Transfer.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Multi-Purpose Loan Application Form" Value="Multi-Purpose Loan Application Form" NavigateUrl="Downloads/HRForm/HDMF_Salary_Loan.pdf" Target="_blank"></asp:MenuItem>
                 </asp:MenuItem>

                 <asp:MenuItem Text="Philcare" Value="Philcare">
                    <asp:MenuItem Text="PhilCare Reimbursement Claim Form" Value="PhilCare Reimbursement Claim Form" NavigateUrl="Downloads/HRForm/PhilCare_Reimbursement_Claim_Form.pdf" Target="_blank"></asp:MenuItem>
<%--                    <asp:MenuItem Text="Philcare Reimbursement Form-back" Value="Philcare Reimbursement Form-back" NavigateUrl="Downloads/HRForm/Philcare_ReimFormback.pdf" Target="_blank"></asp:MenuItem>--%>
                 </asp:MenuItem>

                 <asp:MenuItem Text="Philhealth" Value="Philhealth">
       <%--             <asp:MenuItem Text="Member Data Amendment Form" Value="Member Data Amendment Form" NavigateUrl="Downloads/HRForm/Philhealth_M2_Form.pdf" Target="_blank"></asp:MenuItem>--%>
                    <asp:MenuItem Text="Philhealth Member Registration Form" Value="Philhealth Member Registration Form" NavigateUrl="Downloads/HRForm/Philhealth_PMRF.pdf" Target="_blank"></asp:MenuItem>
                 </asp:MenuItem>

                 <asp:MenuItem Text="SSS" Value="SSS">
                    <asp:MenuItem Text="Personal Record - E-1" Value="Personal Record - E-1" NavigateUrl="Downloads/HRForm/SSS_E-1.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Member Data Change Request – E-4" Value="Member Data Change Request – E-4" NavigateUrl="Downloads/HRForm/SSSForms_Change_Request.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Application For Social Security ID - E-6" Value="Application For Social Security ID - E-6" NavigateUrl="Downloads/HRForm/SSS_E-6.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Maternity Notification" Value="Maternity Notification" NavigateUrl="Downloads/HRForm/Maternity_Notification.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Salary Loan Application" Value="Salary Loan Application" NavigateUrl="Downloads/HRForm/SSSForm_Salary_Loan_New.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Sickness Notification" Value="Sickness Notification" NavigateUrl="Downloads/HRForm/SSS_sicknessnot.pdf" Target="_blank"></asp:MenuItem>
                 </asp:MenuItem>

                  <asp:MenuItem Text="Learning and Development" Value="Learning and Development">
                  <asp:MenuItem Text="External Training Request Form" Value="External Training Request Form" NavigateUrl="Downloads/HRForm/External_Training_Request_Form.docx" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Graduate Studies Application Form" Value="Graduate Studies Application Form" NavigateUrl="Downloads/HRForm/Application_Form_-_Graduate_Studies_revised3.pdf" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="STI Scholarship Application Form" Value="STI Scholarship Application Form" NavigateUrl="Downloads/HRForm/FT-HROD-017-00 STI_Scholarship_Application_Form.docx" Target="_blank"></asp:MenuItem>
<%--                    <asp:MenuItem Text="Training Request Form" Value="Training Request Form" NavigateUrl="Downloads/HRForm/Training_Request_Form.doc" Target="_blank"></asp:MenuItem>--%>
                 </asp:MenuItem>

                 <asp:MenuItem Text="Recruitment" Value="Recruitment">
                    <%--<asp:MenuItem Text="Hiring Memo" Value="Hiring Memo" NavigateUrl="Downloads/HRForm/Hiring_Memo-4.doc" Target="_blank"></asp:MenuItem>--%>
                    <asp:MenuItem Text="Hiring Memo" Value="Hiring Memo" NavigateUrl="Downloads/HRForm/Hiring_Memo.doc" Target="_blank"></asp:MenuItem>
                    <%--<asp:MenuItem Text="HR Requisition Form (Generic)" Value="HR Requisition Form (Generic)" NavigateUrl="Downloads/HRForm/HR_Requisition_Template_(generic).doc" Target="_blank"></asp:MenuItem>--%>
                    <asp:MenuItem Text="HR Requisition Form" Value="HR Requisition Form" NavigateUrl="Downloads/HRForm/HR_Requisition_Template.doc" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Employee Transfer Application Form" NavigateUrl="Downloads/HRForm/Employee_Transfer_Application_Form.doc" Target="_blank"></asp:MenuItem>
                 </asp:MenuItem>

                 <asp:MenuItem Text="Others" Value="Others">
                    <asp:MenuItem Text="Emergency Loan Form" Value="Emergency Loan Form" NavigateUrl="Downloads/HRForm/Emergency_Loan_Form.xls" Target="_blank"></asp:MenuItem>
                    <asp:MenuItem Text="Fitness Center Membership Form" Value="Fitness Center Membership Form" NavigateUrl="Downloads/HRForm/Fitness_Center_Membership_Form.pdf" Target="_blank"></asp:MenuItem>
<%--                    <asp:MenuItem Text="Graduate Studies Application Form" Value="Graduate Studies Application Form" NavigateUrl="Downloads/HRForm/Application_Form_-_Graduate_Studies_revised3.pdf" Target="_blank"></asp:MenuItem>--%>
<%--                    <asp:MenuItem Text="Scholarship Application Form" Value="Scholarship Application Form" NavigateUrl="Downloads/HRForm/Scholarship_Application_Form.docx" Target="_blank"></asp:MenuItem>--%>
                    <asp:MenuItem Text="ID Replacement Form" Value="ID Replacement Form" NavigateUrl="Downloads/HRForm/ID_Replacement_Form1.pdf" Target="_blank"></asp:MenuItem>
<%--                    <asp:MenuItem Text="Training Request Form" Value="Training Request Form" NavigateUrl="Downloads/HRForm/Training_Request_Form.doc" Target="_blank"></asp:MenuItem>--%>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
         </asp:Menu>
     </div>
     <%--<div class="masterpanelcontent">&nbsp;Banclife</div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink6" NavigateUrl="~/Downloads/HRForm/Banclife_Individual_Application_Form.pdf"  Text="Banclife Individual Application Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     
     <div class="masterpanelcontent">&nbsp;BIR</div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink11" NavigateUrl="~/Downloads/HRForm/Application_For_Registration_BIR_Form_1902.pdf" Text="Application For Registration - BIR Form 1902" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink12" NavigateUrl="~/Downloads/HRForm/Application_For_Registration_Information_Update_BIR_Form_1905.pdf"  Text="Application For Registration Information Update - BIR Form 1905" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink13" NavigateUrl="~/Downloads/HRForm/Certification_Of_Update_BIR_Form_2305.pdf"  Text="Certification Of Update - BIR Form 2305" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>

     <div class="masterpanelcontent">&nbsp;Pag-ibig</div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink14" NavigateUrl="~/Downloads/HRForm/HDMF_Calamity_Loan_Form.pdf" Text="Calamity Loan Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink15" NavigateUrl="~/Downloads/HRForm/HDMF_M2-2.pdf"  Text="Member's Change of Information - M2-2" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink16" NavigateUrl="~/Downloads/HRForm/HDMF_Request_for_Transfer.pdf"  Text="Request for Transfer" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink17" NavigateUrl="~/Downloads/HRForm/HDMF_Salary_Loan.pdf"  Text="Multi-Purpose Loan Application Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>

     <div class="masterpanelcontent">&nbsp;Philcare</div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink18" NavigateUrl="~/Downloads/HRForm/Philcare_ReimForm1-front.pdf"  Text="Philcare Reimbursement Form-front" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink19" NavigateUrl="~/Downloads/HRForm/Philcare_ReimFormback.pdf"  Text="Philcare Reimbursement Form-back" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     
     <div class="masterpanelcontent">&nbsp;Philhealth</div>    
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink20" NavigateUrl="~/Downloads/HRForm/Philhealth_M2_Form.pdf"  Text="Member Data Amendment Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink21" NavigateUrl="~/Downloads/HRForm/Philhealth_PMRF.pdf"  Text="Philhealth Member Registration Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>

     <div class="masterpanelcontent">&nbsp;SSS</div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink22" NavigateUrl="~/Downloads/HRForm/SSS_E-1.pdf"  Text="Personal Record - E-1" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink23" NavigateUrl="~/Downloads/HRForm/SSS_E-4.pdf"  Text="Member's Data Amendment Form - E-4" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink24" NavigateUrl="~/Downloads/HRForm/SSS_E-6.pdf"  Text="Application For Social Security ID - E-6" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink25" NavigateUrl="~/Downloads/HRForm/SSS_MAT-1.pdf"  Text="Maternity Notification - MAT-1" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/Downloads/HRForm/SSS_MemberLoan_Application.pdf"  Text="Member Loan Application" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink26" NavigateUrl="~/Downloads/HRForm/SSS_sicknessnot.pdf"  Text="Sickness Notification" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     
     <div class="masterpanelcontent">&nbsp;Recruitment</div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/Downloads/HRForm/HR_Requisition_Form_Template.pdf"  Text="HR Requisition Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/Downloads/HRForm/Hiring_Memo-3.pdf"  Text="Hiring Memo" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>

     <div class="masterpanelcontent">&nbsp;Others</div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink27" NavigateUrl="~/Downloads/HRForm/Emergency_Loan_Form.pdf"  Text="Emergency Loan Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink5" NavigateUrl="~/Downloads/HRForm/Fitness_Gym_Membership_Form.jpg"  Text="Fitness Gym Membership Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink7" NavigateUrl="~/Downloads/HRForm/GOK_APPLICATION_FORM.doc"  Text="GOK Application Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink28" NavigateUrl="~/Downloads/HRForm/ID_Replacement_Form1.pdf"  Text="ID Replacement Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent">&nbsp;&nbsp;<asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/Downloads/HRForm/Training_Request_Form.doc"  Text="Training Request Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelspace"></div>--%>

<%-- <div style='font-weight:bold'>Finance Forms</div>
     <div class="masterpanelcontent"><asp:HyperLink runat="server" ID="HyperLink7" NavigateUrl="#"  Text="Liquidation Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelcontent"><asp:HyperLink runat="server" ID="HyperLink8" NavigateUrl="#"  Text="Reimbursement Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelspace"></div>--%>
     <br />
             <div style='font-weight:bold'>Legal Form</div>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu5" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="Contract Review" Value="Contract Review">
                     <asp:MenuItem Text="Contract Review Form" Value="Contract Review Form" NavigateUrl="Downloads/Contract_Review_Form.docx"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>
        
        <br />
     <div style='font-weight:bold'>MIS Forms</div>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu2" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="Project Request" Value="Project Request">
                     <asp:MenuItem Text="Project Request Form" Value="Project Request Form" NavigateUrl="Downloads/MIS/FT-MIS-001-01_Project_Request_Form.docx"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Change Request" Value="Project Request">
                     <asp:MenuItem Text="Change Request Form" Value="Change Request Form" NavigateUrl="Downloads/MIS/03Change_Request_Form.docx"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>
             <br />

          <div style='font-weight:bold'>Training Form</div>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu9" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="Training Request" Value="Project Request">
                     <asp:MenuItem Text="Training Request Form" Value="Training Request Form" NavigateUrl="Downloads/Training/Training_Request_Form_2015.doc"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>
             <br />
             <br />
     <div style='font-weight:bold'>Assessment Form</div>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu8" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="Project Request" Value="Project Request Form">
                     <asp:MenuItem Text="Project Request Form" Value="Project Request Form" NavigateUrl="Downloads/ARAForms/FT-ARA-006-01_Project_Request_Form.docx"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>

          <br />
      <div style='font-weight:bold'>Finance Forms</div>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu4" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="Finance Liquidation Form" Value="Finance Liquidation Form">
                     <asp:MenuItem Text="Finance Liquidation Form" Value="Finance Liquidation Form" NavigateUrl="Downloads/FinanceForms/Finance_Liquidation_Form_2012.xlsx"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Inventory Charging Slip" Value="Inventory Charging Slip">
                     <asp:MenuItem Text="Inventory Charging Slip" Value="Inventory Charging Slip" NavigateUrl="Downloads/FinanceForms/ICS_2012.xlsx"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Petty Cash Liquidation Report" Value="Petty Cash Liquidation Report">
                     <asp:MenuItem Text="Petty Cash Liquidation Report" Value="Petty Cash Liquidation Report" NavigateUrl="Downloads/FinanceForms/Petty_Cash_Liquidation_Report.xlsx"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Request for Billing/Adjustment" Value="Request for Billing/Adjustment">
                     <asp:MenuItem Text="Request for Billing/Adjustment" Value="Request for Billing/Adjustment" NavigateUrl="Downloads/FinanceForms/RBA.xlsx"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Request for Budget Realignment" Value="Request for Budget Realignment">
                     <asp:MenuItem Text="Request for Budget Realignment" Value="Request for Budget Realignment" NavigateUrl="Downloads/FinanceForms/RBR.xlsx"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>
        <br />
<div style='font-weight:bold'>Quality Management System (QMS) Forms and Templates</div>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu3" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="Control of Documents" Value="Control of Documents">

                     <asp:MenuItem Text="Document Retrieval Request Form" Value="Document Retrieval Request Form" NavigateUrl="Downloads/FT-QMS-002-01_Document_Retrieval_Form.docx"></asp:MenuItem>
                     <%--<asp:MenuItem Text="Document Management Form" Value="Document Management Form" NavigateUrl="Downloads/FT-QMS-001-00_Document_Management_Form.pdf"></asp:MenuItem>--%>
                     <asp:MenuItem Text="Document Management Form - ARCHIVAL" Value="Document Management Form - ARCHIVAL" NavigateUrl="Downloads/FT-QMS-018-00_Document_Management_Form_-_ARCHIVAL.docx"></asp:MenuItem>
                     <asp:MenuItem Text="Document Management Form - CREATION or REVISION" Value="Document Management Form - CREATION or REVISION" NavigateUrl="Downloads/FT-QMS-019-00_Document_Management_Form_-_CREATION_or_REVISION.docx"></asp:MenuItem>
                     <asp:MenuItem Text="Document Management Form - DISPOSAL" Value="Document Management Form - DISPOSAL" NavigateUrl="Downloads/FT-QMS-020-00_Document_Management_Form_-_DISPOSAL.docx"></asp:MenuItem>
                     <asp:MenuItem Text="Document Appraisal Form" Value="Document Appraisal Form" NavigateUrl="Downloads/FT-QMS-021-00_Document_Appraisal_Form.docx"></asp:MenuItem>

                     <asp:MenuItem Text="Quality Policy Template" Value="Quality Policy Template" NavigateUrl="Downloads/QMS/FT-QMS-011-01_Quality_Policy_Template.docx"></asp:MenuItem>
                     <asp:MenuItem Text="Quality Procedure Template" Value="Quality Procedure Template" NavigateUrl="Downloads/QMS/FT-QMS-012-01_Quality_Procedure_Template.docx"></asp:MenuItem>
                     <asp:MenuItem Text="Quality Guideline Template" Value="Quality Guideline Template" NavigateUrl="Downloads/QMS/FT-QMS-013-01_Quality_Guideline_Template.docx"></asp:MenuItem>
                     <asp:MenuItem Text="Quality Work Instruction Template" Value="Quality Work Instruction Template" NavigateUrl="Downloads/QMS/FT-QMS-014-01_Quality_Work_Instruction_Template.docx"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Control of Records" Value="Control of Records">
                     <asp:MenuItem Text="Record Management Form" Value="Record Management Form" NavigateUrl="Downloads/FT-QMS-003-00_Record_Management_Form.pdf"></asp:MenuItem>
                     <asp:MenuItem Text="Record Access Request Form" Value="Record Access Request Form" NavigateUrl="Downloads/FT-QMS-004-00_Record_Access_Request_Form.pdf"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Control of Nonconformity/Corrective Action" Value="Control of Nonconformity/Corrective Action">
                     <asp:MenuItem Text="Corrective Action Notice" Value="Preventive Action Notice" NavigateUrl="Downloads/FT-QMS-006-01_Corrective_Action_Notice.pdf"></asp:MenuItem>
                     <asp:MenuItem Text="Corrective Action Notice Log Template" Value="Preventive Action Notice Log Template" NavigateUrl="Downloads/FT-QMS-008-00_Corrective_Action_Notice_Log_-_(name_of_unit_or_department)_(template).xlsx"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem Text="Preventive Action" Value="Preventive Action">
                     <asp:MenuItem Text="Preventive Action Notice" Value="Corrective Action Notice" NavigateUrl="Downloads/FT-QMS-007-01_Preventive_Action_Notice.pdf"></asp:MenuItem>
                     <asp:MenuItem Text="Preventive Action Notice Log Template" Value="Corrective Action Notice Log Template" NavigateUrl="Downloads/FT-QMS-009-00_Preventive_Action_Notice_Log_-_(name_of_unit_or_department)_(template).xlsx"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>


     <%--<div class="masterpanelcontent"><asp:HyperLink runat="server" ID="HyperLink9" NavigateUrl="~/Downloads/MIS_Project_Request.docx"  Text="Project Request Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelspace"></div>--%>
     <br />
     <div style='font-weight:bold'>Communications Forms</div>
<%--     <div class="masterpanelcontent"><asp:HyperLink runat="server" ID="HyperLink10" NavigateUrl="~/Downloads/PRF_CSG-03-2010.xlsx"  Text="Project Request Form" CssClass="HyperLink" Target="_blank"></asp:HyperLink></div>
     <div class="masterpanelspace"></div>--%>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu6" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="Project Request" Value="Project Request">
                     <asp:MenuItem Text="Project Request Form" Value="Project Request Form" NavigateUrl="Downloads/PRF_CSG-03-2010.xlsx"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>

      <br />
               <div style='font-weight:bold'>School Operations</div>
      <div style="padding-left:6px; padding-top:4px">
     <asp:Menu ID="Menu7" runat="server" BackColor="#E3EAEB"  CssClass="HyperLink"
               DynamicHorizontalOffset="2" Font-Names="Helvetica" Font-Size="14px" 
               ForeColor="#666666" StaticSubMenuIndent="10px">
             <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
             <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <DynamicMenuStyle BackColor="#E3EAEB" />
             <DynamicSelectedStyle BackColor="#666666" />
             <Items>
                 <asp:MenuItem Text="RIC Form" Value="Control of Documents">
                     <asp:MenuItem Text="RIC Form" Value="Document Management Form" NavigateUrl="Downloads/FT-GRG-001-03_Requests_Inquiries_Concerns_Form.docx"></asp:MenuItem>
                 </asp:MenuItem>
             </Items>
             <StaticHoverStyle BackColor="#666666" ForeColor="White" />
             <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
             <StaticSelectedStyle BackColor="#1C5E55" />
     </asp:Menu>
     </div>

     <br /><br />
   <div style="text-align: center;">
						&nbsp;<asp:Button ID="btnBack" runat="server" Text="Back to Home Page" onclick="btnBack_Click" 
                           />
					</div>
    </div>     
   </td>
  </tr>
     
 </table>  
</asp:Content>

