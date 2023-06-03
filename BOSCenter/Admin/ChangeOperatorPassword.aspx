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
                    	Change Password
                    </div>
                </div>
            </div>
        	<div class="log_form1"> 
            
                <formview>
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
                                TextMode="Password" ></asp:TextBox>


                           
                         </div>

                          <%-- <asp:Label ID="lblnewPass" runat="server" Text="Enter New Password" CssClass="errorlabels-sm" Visible="false" ></asp:Label>--%>
 
                           

                                                       <asp:RegularExpressionValidator runat="server" ErrorMessage="Enter Valid Password [Use @ and $ Numbers alphabet Lower and Upper Case]" ControlToValidate="txtNewPassword"
      ValidationExpression='^[A-Za-z][A-Za-z0-9@$]*$' CssClass="errorlabels-sm"></asp:RegularExpressionValidator>

                        
                          
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

                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Enter Valid Password" ControlToValidate="txtConfirmPassword"
      ValidationExpression='^[A-Za-z][A-Za-z0-9@$]*$' CssClass="errorlabels-sm"></asp:RegularExpressionValidator>


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
                    </formview>

                <%--<formview>
                <form action="/myaction.php" name="myForm" onsubmit="return validateForm()" method="post" style="display: flex; align-items: center;
                   justify-content: center; flex-direction: column; gap: 18px; padding: 25px;">
        <div class="formdesign" id="phone" style="display: flex; align-items: center; justify-content: flex-start; gap: 40px; width: 100%;">
            <h5 style="font-weight: bold; font-size: 15px;">Old Password</h5> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="number" name="fphone" 
                style="padding: 6px 35px; border: 1px solid grey; outline: none; border-radius: 3px; background: white;"/><span class="formerror"></span> 
        </div>
        <div class="formdesign" id="pass" style="display: flex; align-items: center; justify-content: flex-start; gap: 40px; width: 100%;">
            <h5 style="font-weight: bold; font-size: 15px;">New Password</h5> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="password" name="fpass" 
                 style="padding: 6px 35px; border: 1px solid grey; outline: none; border-radius: 3px; background: white;"/><span class="formerror"></span> 
        </div>
        <div class="formdesign" id="cpass" style="display: flex; align-items: center; justify-content: flex-start; gap: 40px; width: 100%;">
            <h5 style="font-weight: bold; font-size: 15px;">Confirm Password</h5> &nbsp;<input type="password" name="fcpass" 
                style="padding: 6px 35px; border: 1px solid grey; outline: none; border-radius: 3px; background: white;"/><span class="formerror"></span> 
        </div>
                    <div class="button-section" style="width: 100%; display: flex; align-items: center; justify-content: center; gap: 15px; margin-top: 30px;">
        <input class="but" type="submit" value="Change Password" style="background: #3C8DBC; color: white; border: none; outline: none; border-radius: 3px; padding: 2px 18px;"/>
        <input class="reset" type="submit" value="Reset" style="background: #3C8DBC; color: white; border: none; outline: none; border-radius: 3px; padding: 2px 18px;"/>
                        </div>
    </form>
                    </formview>--%>
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

    <script>
        function seterror(id, error) {
            element = document.getElementById(id);
            element.getElementsByClassName("formerror")[0].innerHTML = error;
        }
        function validateForm() {
            var returnval = true;
            var name = document.forms["myForm"]["fname"].value;
            if (name.length < 5) {
                seterror("name", "Length of name is too short");
                returnval = false;
            }
            var Password = document.forms["myForm"]["fpass"].value;
            if (Password.length < 5) {
                seterror("pass", "Length of Password is too short");
                returnval = false;
            }

            return returnval;
        }
    </script>


</asp:Content>
