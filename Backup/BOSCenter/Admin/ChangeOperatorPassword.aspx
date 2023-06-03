<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="ChangeOperatorPassword.aspx.vb" Inherits="BOSCenter.ChangeOperatorPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
 <div class="container">
	<div class="row">
    	<div class="col-sm-8 col-sm-offset-2">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Change Pasword
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            
                   <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Old Password</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtOldPassword" cssclass="form-control" runat="server" 
                                TextMode="Password"></asp:TextBox>
                         
                          </div> 

                        <%-- <asp:Label ID="lbloldPass" runat="server" Text="Enter Old Password" CssClass="errorlabels-sm" Visible="false" ></asp:Label>--%>
                         
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="btnchpwd" ControlToValidate="txtOldPassword" CssClass="errorlabels-sm" runat="server" ErrorMessage="Enter Old Password"></asp:RequiredFieldValidator>
 
                      </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">New Password</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtNewPassword" cssclass="form-control" runat="server" 
                                TextMode="Password"  ></asp:TextBox>
                           
                         </div>

                          <%-- <asp:Label ID="lblnewPass" runat="server" Text="Enter New Password" CssClass="errorlabels-sm" Visible="false" ></asp:Label>--%>
 
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="btnchpwd"
                                    ControlToValidate="txtNewPassword" ErrorMessage="Enter New Password" 
                                     CssClass="errorlabels-sm"></asp:RequiredFieldValidator> 
                          
                      </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Confirm Password</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtConfirmPassword" cssclass="form-control" runat="server" 
                                TextMode="Password"></asp:TextBox>
                            
                            </div>
                       <%--   <asp:Label ID="lblConfirmPass" runat="server" Text="Enter Confirm Password" CssClass="errorlabels-sm" Visible="false" ></asp:Label>
--%>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="btnchpwd"
                                    ControlToValidate="txtConfirmPassword" ErrorMessage="Enter Confirm Password" 
                                      CssClass="errorlabels-sm"></asp:RequiredFieldValidator>  
                      </div>

                  <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-7">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                          <asp:Label ID="lblError" runat="server" ></asp:Label>
                           
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="btnchpwd"
                                    ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" 
                                    ErrorMessage="Password-Mismatch" CssClass="errorlabels-sm"
                                     ></asp:CompareValidator>
                        </div>
                      </div>



                  <div class="clearfix" style="margin-bottom:5px;"></div>
                   
                  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnChangePassword" runat="server" ValidationGroup="btnchpwd" Text="Change Password" CausesValidation="true" 
                             cssclass="btn btn-primary mar_top10"  />&nbsp;  
                         <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" 
                             cssclass="btn btn-primary mar_top10"  />
                          </div> 
                        
                    </div>
                  </div>

  </form>
                 <div class="clearfix" style="margin-bottom:5px;"></div>
                
            </div>
            
            
        </div>
    </div>
</div>


<%--
    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="350px" style="display:none;"  >

<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>Confirmation Dialog</strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            Are you sure you want to delete ?</td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="Button2" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" OnClick="btnDeleteRow_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>--%>

    </asp:Panel>




</asp:Content>
