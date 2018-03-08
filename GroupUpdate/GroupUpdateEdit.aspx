<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="GroupUpdateEdit.aspx.cs" Inherits="GroupUpdate_GroupUpdateEdit" %>
<%@Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:ScriptManager runat="server" ID="smThreadNew"></asp:ScriptManager>
	<table width="648px" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;">
					<%--<b><span class="HeaderText">Edit Group Update</span></b>
					<br />
                     <br />
                 <div runat="server" id="div1" class="Quote" visible="true">
                  Title must be maximum of<b> 50 characters.</b><br />
                  Description must be maximum of <b>250 characters.</b><br />
                  Uploaded photo resolution must be exactly<b> 420px wide and 250px tall.</b>
                 </div>--%>
					<br />
                    <div runat="server" id="divError" class="ErrMsg" visible="false">
                  <b>Error during update. Please correct your data entries:</b>
                  <br />
                  <br />
                  <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
                 </div>
                <br />
					<div class="GridBorder">
						<table width="100%" cellpadding="3" cellspacing="1">
							<tr><td colspan="2" class="GridColumns">&nbsp;<b>Edit Group Update Details</b></td></tr>
							<tr>
								<td class="GridRows" style="width: 15%">Title:</td>
								<td class="GridRows" style="width: 85%">
									<asp:TextBox runat="server" ID="txtTitle" CssClass="controls" Width="98%" 
                                        MaxLength="50" BackColor="White" />
									<asp:RequiredFieldValidator runat="server" ID="reqTitle" ControlToValidate="txtTitle" ErrorMessage="<br>[Required]" Display="Dynamic" ForeColor="Red" ValidationGroup="SaveRecord" />

								</td>
							</tr>
                            <tr>
								<td class="GridRows">&nbsp;</td>
								<td class="GridRows" style="vertical-align:top">
                                   <font style="font-style: italic; font-size:x-small"> Title must be maximum of<b> 50 characters.</b></font>
								</td>
							</tr>
							<tr>
								<td class="GridRows" valign="top">Description:</td>
								<td class="GridRows">
									<asp:TextBox runat="server" ID="txtDescription" CssClass="controls" Width="98%" 
                                        MaxLength="250" BackColor="White" Rows="2" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                                        ControlToValidate="txtDescription" ErrorMessage="<br>[Required]" 
                                        Display="Dynamic" ForeColor="Red" ValidationGroup="SaveRecord" />
								</td>
							</tr>
                            <tr>
								<td class="GridRows">&nbsp;</td>
								<td class="GridRows"  style="vertical-align:top">
                                    <font style="font-style: italic; font-size:x-small"> Description must be maximum of <b>250 characters.</b></font>
								</td>
							</tr>
                            <tr>
								<td class="GridRows" style="width: 15%">Contributor:</td>
								<td class="GridRows" style="width: 85%">
									<asp:TextBox runat="server" ID="txtContributor" CssClass="controls" Width="50%" 
                                        MaxLength="20" BackColor="White" />
								</td>
							</tr>

                            <tr>
								<td class="GridRows">&nbsp;</td>
								<td class="GridRows" style="vertical-align:top">
                                   <font style="font-style: italic; font-size:x-small"> Contributor must be maximum of<b> 20 characters.</b></font>
								</td>
							</tr>
                            <tr>
                                        <td class="GridRows" style="width: 15%">
                                            Photo By:
                                        </td>
                                        <td class="GridRows" style="width: 85%">
                                            <asp:TextBox runat="server" ID="txtPhotoSource" CssClass="controls" Width="50%" MaxLength="20"
                                                BackColor="White" />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="GridRows">
                                            &nbsp;
                                        </td>
                                        <td class="GridRows" style="vertical-align: top">
                                            <font style="font-style: italic; font-size: x-small">Photo Contributor must be maximum of<b>
                                                20 characters.</b></font>
                                        </td>
                                    </tr>
                            <tr>
								<td class="GridRows" valign="top">Group Head:</td>
								<td class="GridRows">
									<asp:HiddenField ID="hdnGroupHead" runat="server" />
                                    <asp:Label ID="lblGroupHead" runat="server" CssClass="controls" 
                                        Text="GroupHead" BorderStyle="None"></asp:Label>
                                </td>
							</tr>
                                    <tr>
                              <td class="GridRows">Remarks:</td>
                              <td class="GridRows">
                                  <asp:TextBox ID="txtGroupHeadRemark" runat="server" CssClass="controls" 
                                      Rows="3" TextMode="MultiLine" Width="80%" ReadOnly="True"></asp:TextBox>
                                 </td>
                             </tr> 
                            <%--<tr>
								<td class="GridRows" valign="top">Division Head:</td>
								<td class="GridRows">
									<asp:HiddenField ID="hdnDivisionHead" runat="server" />
                                    <asp:Label ID="lblDivisionHead" runat="server" CssClass="controls" 
                                        Text="DivisionHead" BorderStyle="None"></asp:Label>
                                </td>
							</tr>
                            <tr>
                              <td class="GridRows">Remarks:</td>
                              <td class="GridRows">
                                  <asp:TextBox ID="txtDivisionHeadRemark" runat="server" CssClass="controls" 
                                      Rows="3" TextMode="MultiLine" Width="80%" ReadOnly="True"></asp:TextBox>
                                 </td>
                             </tr> --%>
							<tr>
								<td class="GridRows">Photo:</td>
								<td class="GridRows">
									<asp:FileUpload runat="server" ID="fuAttachment" />
								</td>
							</tr>
                            <tr>
								<td class="GridRows">&nbsp;</td>
								<td class="GridRows"  style="vertical-align:top">
                                   <font style="font-style: italic; font-size:x-small">  Uploaded photo resolution must be exactly<b> 420px wide and 250px tall.</b></font>
								</td>
							</tr>
							<tr>
								<td class="GridRows" style="vertical-align: top;">Contents:</td>
								<td class="GridRows">
									<CKEditor:CKEditorControl ID="ckeContents" runat="server" CssClass="controls" Height="300px" BackColor="White" Width="98%" />
								</td>
							</tr>
						</table>
					</div>
					<br />
                    <asp:HiddenField ID="hndIsPreview" runat="server" />
					<div style="text-align: center;">
                      <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft"  ValidationGroup="SaveRecord"  OnClick="btnSaveAsDraft_Click" />
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"  ValidationGroup="SaveRecord"
                        Visible="False" />&nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Submit"  ValidationGroup="SaveRecord"  OnClick="btnSave_Click"/>
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Void"  OnClick="btnCancel_Click"/>
                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back"  OnClick="btnBack_Click"/>
                        &nbsp;
					</div>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>

