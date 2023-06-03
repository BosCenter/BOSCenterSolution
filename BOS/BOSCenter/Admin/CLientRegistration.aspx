<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="CLientRegistration.aspx.vb" Inherits="BOSCenter.CLientRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    function disableSubmit() {
        document.getElementById('<%= btnsaveOk.ClientID %>').disabled = true;
//        document.getElementById('<%= btnsaveOk.ClientID %>').disabled = true;
        var t = setTimeout("enableSubmit()", 9000);
    }

    function enableSubmit() {

        document.getElementById('<%= btnsaveOk.ClientID %>').disabled = false
//        document.getElementById('<%= btnsaveOk.ClientID %>').disabled = false;
    }
   
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat='server' ID='updatepanel1'>
<ContentTemplate>


<div class="container" >
<div class="col-sm-10 col-sm-offset-1">


<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    <asp:Label ID="lblPageHeading" runat="server" Text="::: Client Registration :::"></asp:Label>
                    	
                    </div>
                </div>
            </div>

<div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
Company Details
</div>

<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtCompanyCode' class='control-label'>Company Code</label>
<asp:TextBox ID='txtCompanyCode' runat='server'  placeholder='CompanyCode' 
        class='form-control' ReadOnly="True"></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtCompanyType' class='control-label'>Create Database<asp:Label 
        ID="Label3" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label> </label> 
    &nbsp;<asp:DropDownList ID="ddlCreateDatabase" runat="server" 
        class='form-control' > 
        <asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:DropDownList>
     <asp:Label ID="lblDatabaseName" runat="server" Visible="False"></asp:Label>
    
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtCompanyName' class='control-label'>Company Name<asp:Label 
        ID="Label4" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtCompanyName' runat='server'  placeholder='CompanyName' class='form-control'></asp:TextBox>
</div>
</div>

</div>
<div class="row">
<div class="col-md-4">
<div class="form-group ">
<label for='txtCompanyHead' class='control-label'>Company Head</label>
<asp:TextBox ID='txtCompanyHead' runat='server'  placeholder='CompanyHead' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtContactPerson' class='control-label'>Contact Person<asp:Label 
        ID="Label6" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtContactPerson' runat='server'  placeholder='ContactPerson' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtTinNo' class='control-label'>TIN No</label>
<asp:TextBox ID='txtTinNo' runat='server'  placeholder='TinNo' class='form-control'></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtCinNo' class='control-label'>CIN No</label>
<asp:TextBox ID='txtCinNo' runat='server'  placeholder='CinNo' class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtBranchCode' class='control-label'>GST No<asp:Label 
        ID="Label15" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtGSTNo' runat='server'  placeholder='GST No' class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtBranchCode' class='control-label'>Website Redirect URL</label>
<asp:TextBox ID='txtWebRedirectUrl' runat='server'  placeholder='Website Url' class='form-control'></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtCinNo' class='control-label'>Credit Bal Limit<asp:Label 
        ID="Label17" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtCreditBalLimit' runat='server'  placeholder='Credit Bal Limit' Text="0" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="txtPinCode_FilteredTextBoxExtender" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtCreditBalLimit">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>

</div>
</div>
</div>

</div>
</div>
</div>

<div class="row" runat="server" id="Div_AccountInfo" visible="false"  >
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
Account Details
</div>



<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">
<div class="row">

<div style="margin-bottom:5px;"></div>
<div class="col-md-4">
<div class="form-group">
<label for='txtCompanyType' class='control-label'>Account Status</label>
    <asp:DropDownList ID="ddlAccountStatus" runat="server" class='form-control'> 
        <asp:ListItem>Inactive</asp:ListItem>
        <asp:ListItem>Active</asp:ListItem>
    </asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtPassword' class='control-label'>Password<asp:Label 
        ID="Label8" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtPassword' runat='server'  placeholder='Password' 
        class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtPassword' class='control-label'>Transaction Pin<asp:Label 
        ID="Label16" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtTransactionPin' runat='server'  placeholder='Transaction Pin' MaxLength="10" 
        class='form-control'></asp:TextBox>
</div>
</div>


</div>

</div>
</div>
</div>

</div>
</div>
</div>


<div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
Company Logo
</div>

<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">
<div class="row">

<div class="col-md-10">
<div class="form-group">

<asp:FileUpload ID="FileUpload1" runat="server" />

    <asp:Button ID="btnUpload" runat="server"  
        Text="Upload" CausesValidation="False" />
    &nbsp;<asp:Button ID="btnDeleteUpload" runat="server" 
         Text="Remove" />
    <asp:Image ID="Image1" runat="server" BorderStyle="None" CssClass="" 
        Height="81px" ImageUrl="~/images/uploadimage.png" Width="90px" />
    <asp:Label ID="lblErrorImageError" runat="server" Text=""></asp:Label>
</div>
</div>


</div>

</div>
</div>
</div>

</div>
</div>
</div>


<div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
Company Address Details
</div>

<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAddress_1' class='control-label'>Address_1<asp:Label 
        ID="Label9" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtAddress_1' runat='server'  placeholder='Address_1' class='form-control' TextMode="SingleLine"></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtAddress_2' class='control-label'>Address_2<asp:Label 
        ID="Label10" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtAddress_2' runat='server'  placeholder='Address_2' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtAddress_3' class='control-label'>Address_3</label>
<asp:TextBox ID='txtAddress_3' runat='server'  placeholder='Address_3' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row">

<div class="col-md-4">
<div class="form-group">
<label for='txtCountry' class='control-label'>Country<asp:Label 
        ID="Label11" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:DropDownList ID='ddlCountry' runat='server' class='form-control' 
        AutoPostBack="True"></asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtState' class='control-label'>State<asp:Label 
        ID="Label12" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:DropDownList ID='ddlState' runat='server' class='form-control' 
        AutoPostBack="True"></asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtDistrict' class='control-label'>District<asp:Label 
        ID="Label13" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:DropDownList ID='ddlDistrict' runat='server' class='form-control' 
        ></asp:DropDownList>
</div>
</div>
</div>
<div class="row">

<div class="col-md-4">
<div class="form-group">
<label for='txtCity' class='control-label'>City</label>
<asp:TextBox ID='txtCity' runat='server'  placeholder='City' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtPinCode' class='control-label'>PinCode<asp:Label 
        ID="Label14" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtPinCode' runat='server'  placeholder='PinCode' 
        class='form-control' MaxLength="6"></asp:TextBox>

<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtPinCode">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
</div>
</div>
</div>

</div>
</div>
</div>

<div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
Company Contact Details
</div>

<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtPhoneNo_1' class='control-label'>PhoneNo_1</label>
<asp:TextBox ID='txtPhoneNo_1' runat='server'  placeholder='PhoneNo_1' 
        class='form-control' MaxLength="12"></asp:TextBox>

  <asp:FilteredTextBoxExtender ID="txtPhoneNo_1_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtPhoneNo_1">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtPhoneNo_2' class='control-label'>PhoneNo_2</label>
<asp:TextBox ID='txtPhoneNo_2' runat='server'  placeholder='PhoneNo_2' 
        class='form-control' MaxLength="12"></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtPhoneNo_2">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtMobile_No' class='control-label'>Mobile No<asp:Label 
        ID="Label5" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtMobile_No' runat='server'  placeholder='Mobile_No' 
        class='form-control' MaxLength="10"></asp:TextBox>

 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtMobile_No">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtEmail_ID' class='control-label'>Email ID<asp:Label 
        ID="Label7" runat="server" Text=" *" ForeColor="Red" Font-Bold="True"></asp:Label></label>
<asp:TextBox ID='txtEmail_ID' runat='server'  placeholder='Email_ID' class='form-control'></asp:TextBox>
</div>
</div>

</div>

</div>
</div>
</div>

</div>
</div>
</div>


</div> 


<div class="row">
<div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                             <asp:Label ID="lblError" ClientIDMode="Static" runat="server"  ></asp:Label>
                               <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblSessionRID" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblSessionWorkType" runat="server" Visible="False"></asp:Label>
                   
                            <asp:Label ID="lblSessionFlag_App_delete" runat="server" Visible="False"></asp:Label>
                   
                               <asp:Label ID="lblFormName" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblRefModule" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblRefSubModule" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblNavigationModule" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblOrderNo" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblRefSubModule_Order" runat="server" Visible="False"></asp:Label>
                               <asp:Label ID="lblNavigationModule_Order" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>
<div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-5">
                    </div> 
                          <div class="col-sm-7">
                           <asp:Button ID="btnSave" runat="server" Text="Save" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                            	
                          <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  &nbsp;

                          <asp:Button ID="btnApprovalCancel" runat="server" Text="close" 
                             cssclass="btn btn-primary mar_top10" Enabled="False" Visible="False" />
                         
                          </div> 
                        
                    </div>
                  </div>

                  <div class="clearfix" style="margin-bottom:5px;"></div>
 </div>

</div>




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
            <asp:Label ID="lblApproveOrUpapprove" runat="server" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>  </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnOk" runat="server" Text="Yes" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="No" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>
    </asp:Panel>




    <asp:Button ID="btnSavingModelpopUp" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnSavingModelpopUp" PopupControlID="pnlSavePopUp"  BackgroundCssClass="modalBackground"  CancelControlID="BtnSaveCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSavePopUp" runat="server" Width="350px" style="display:none;">
   <%-- 
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
    <ProgressTemplate>
    <div class="divpopupWaiting">
    <asp:Image ID="imgWait" runat="server" 
	ImageAlign="Middle"  ImageUrl="../images/gear.gif" />
    
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
    
    
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver">&nbsp;</td>
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
            <asp:Label ID="lblSavingDialogBox" runat="server" Text=""></asp:Label> 
            

        </td>
    </tr>

   <tr>
        <td align="center" >
           <div class="row" runat="server" id="Div_deInfo" visible="false">
   <table style="width:50%;">
    <tr>
    <td>
    <asp:Label ID="Label1" runat="server" Text="UserID" Font-Bold="True"></asp:Label> 
    </td>
    
    <td align="center">
    <asp:Label ID="lblClientID" runat="server" Text="" ForeColor="#cc0000"></asp:Label> 
    </td>
    
    </tr>
   
    <tr>
     <td>
    <asp:Label ID="Label2" runat="server" Text="Password " Font-Bold="True"></asp:Label> 
    </td>
     <td align="center" >
           <asp:Label ID="lblPassword" runat="server" Text="" ForeColor="#cc0000" ></asp:Label>  
           </td> 
    </tr>
   </table>
   </div>
   </td>
    </tr>
     <tr>
        <td align="center">
        &nbsp;
         </td>
    </tr>
    <tr>
        <td align="center">
         <asp:Label ID="lblConfrmMsg" runat="server" Text=""></asp:Label>
           &nbsp;</td>
           
    </tr>
    
      
     <tr>
        <td align="center">
            &nbsp; </td>
         <tr>
             <td align="center">
                 <asp:Label ID="lblpendingaproval" runat="server" Text=""></asp:Label>
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                     <ProgressTemplate>
                         <img src="../images/ajax-loader.gif" />
                     </ProgressTemplate>
                 </asp:UpdateProgress>
                 &nbsp;</td>
         </tr>
         <tr>
             <td align="center">
                 <asp:Button ID="btnsaveOk" runat="server" CssClass="btn btn-primary" Text="Yes" UseSubmitBehavior="false" OnClientClick="disableSubmit()"
                     Width="80px" />
                 &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="BtnSaveCancel" runat="server" CssClass="btn btn-primary" 
                     Text="No" Width="80px" />
             </td>
         </tr>
         <tr>
             <td align="center">
                 &nbsp;</td>
         </tr>
    </tr>

</table>
 </asp:Panel>


  <asp:Button ID="Button2" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button2" PopupControlID="DeleteDocumentPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelDeleteDocument" >
</asp:ModalPopupExtender>

<asp:Panel ID="DeleteDocumentPopup" runat="server" Width="400px" style="display:none;" >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>::: Alert Dialog :::</strong>
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
           <asp:Label ID="lblDeleteInfo" runat="server" Text="This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"></asp:Label></td>
           <asp:Label ID="lblDeleteDocumentInfo" runat="server" Text=""></asp:Label>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnDelete_Document" runat="server" Text="Yes" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelDeleteDocument" runat="server" Text="No" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>
    </asp:Panel>




</ContentTemplate>
 <Triggers>
    <asp:PostBackTrigger ControlID="btnUpload" />
    </Triggers>
</asp:UpdatePanel>









</asp:Content>
