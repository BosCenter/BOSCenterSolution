<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="frm_udyam_registration.aspx.vb" Inherits="BOSCenter.Frm_udyam_registration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12 '>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='UDYAM REGISTRATION'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='Registration Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtEmployerAdhaarNo'>  Employer Adhaar No*</label>
<asp:TextBox ID='txtEmployerAdhaarNo' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtEmployerAdhaarNo_FilteredTextBoxExtender" runat="server" BehaviorID="txtEmployerAdhaarNo_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtEmployerAdhaarNo" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtFullNameofEmployer'>  Full Name of Employer*</label>
<asp:TextBox ID='txtFullNameofEmployer' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlSocialCategory'>  Social Category*</label>
<asp:DropDownList ID='ddlSocialCategory' runat='server' class='form-control' Width="100%">
    <asp:ListItem >General</asp:ListItem> 
    <asp:ListItem >SC</asp:ListItem> 
    <asp:ListItem >ST</asp:ListItem> 
    <asp:ListItem >OBC</asp:ListItem> 
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlGender'>  Gender*</label>
<asp:DropDownList ID='ddlGender' runat='server' class='form-control' Width="100%">
    <asp:ListItem >Male</asp:ListItem>
    <asp:ListItem >Female</asp:ListItem>
    <asp:ListItem >Other</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlPhysicallyHandicapped'>  Physically Handicapped*</label>
<asp:DropDownList ID='ddlPhysicallyHandicapped' runat='server' class='form-control' Width="100%">
     <asp:ListItem >Yes</asp:ListItem>
    <asp:ListItem >No</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNameOfEnterprise'>  Name Of Enterprise*</label>
<asp:TextBox ID='txtNameOfEnterprise' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtTypeOfOrganisation'>  Type Of Organisation*</label>
<asp:TextBox ID='txtTypeOfOrganisation' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPANNumber'>  PAN Number*</label>
<asp:TextBox ID='txtPANNumber' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtAddressofPlant'>  Address of Plant*</label>
<asp:TextBox ID='txtAddressofPlant' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtLocalityofPlant'>  Locality of Plant*</label>
<asp:TextBox ID='txtLocalityofPlant' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlStateofPlant'>  State of Plant*</label>
<asp:DropDownList ID='ddlStateofPlant' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrictofPlant'>  District of Plant*</label>
<asp:DropDownList ID='ddlDistrictofPlant' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtTalukaAndVillageofPlant'>  Taluka And Village of Plant*</label>
<asp:TextBox ID='txtTalukaAndVillageofPlant' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPINCodeofPlant'>  PIN Code of Plant*</label>
<asp:TextBox ID='txtPINCodeofPlant' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPINCodeofPlant_FilteredTextBoxExtender" runat="server" BehaviorID="txtPINCodeofPlant_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPINCodeofPlant" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAddressofoffice'>  Address of office*</label>
<asp:TextBox ID='txtAddressofoffice' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtLocalityofoffice'>  Locality of office*</label>
<asp:TextBox ID='txtLocalityofoffice' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlStateofoffice'>  State of office*</label>
<asp:DropDownList ID='ddlStateofoffice' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrictofoffice'>  District of office*</label>
<asp:DropDownList ID='ddlDistrictofoffice' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtTalukaAndVillageofoffice'>  Taluka And Village of office*</label>
<asp:TextBox ID='txtTalukaAndVillageofoffice' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPINCodeofoffice'>  PINCode of office*</label>
<asp:TextBox ID='txtPINCodeofoffice' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPINCodeofoffice_FilteredTextBoxExtender" runat="server" BehaviorID="txtPINCodeofoffice_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPINCodeofoffice" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtofficeMobileNumber'>  office Mobile Number*</label>
<asp:TextBox ID='txtofficeMobileNumber' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtofficeMobileNumber_FilteredTextBoxExtender" runat="server" BehaviorID="txtofficeMobileNumber_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtofficeMobileNumber" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtofficeemailId'>  office email Id*</label>
<asp:TextBox ID='txtofficeemailId' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtDateofEstablishment'>  Date of Establishment*</label>
<asp:TextBox ID='txtDateofEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    
    <asp:CalendarExtender ID="txtDateofEstablishment_CalendarExtender" runat="server" BehaviorID="txtDateofEstablishment_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txtDateofEstablishment">
    </asp:CalendarExtender>
    
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber'>  Previous EM1/EM2/SSI/UAM Registration Number, If Any*</label>
<asp:DropDownList ID='ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber' runat='server' class='form-control' Width="100%">
     <asp:ListItem >Yes</asp:ListItem>
    <asp:ListItem >No</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtIfAnyEnterNo'>  If Any Enter No*</label>
<asp:TextBox ID='txtIfAnyEnterNo' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtIfAnyEnterNo_FilteredTextBoxExtender" runat="server" BehaviorID="txtIfAnyEnterNo_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtIfAnyEnterNo" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtBankIFSC'>  Bank IFSC*</label>
<asp:TextBox ID='txtBankIFSC' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtBankAccNo'>  Bank Acc No</label>
<asp:TextBox ID='txtBankAccNo' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtBankAccNo_FilteredTextBoxExtender" runat="server" BehaviorID="txtBankAccNo_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtBankAccNo" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlMajorActivityofUnit'>  Major Activity of Unit*</label>
<asp:DropDownList ID='ddlMajorActivityofUnit' runat='server' class='form-control' Width="100%">
     <asp:ListItem >Manufacturing</asp:ListItem>
    <asp:ListItem >Services</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtNICClassificationCode'>  NIC Classification Code*</label>
<asp:TextBox ID='txtNICClassificationCode' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtEmployee_Worker'>  Employee Worker*</label>
<asp:TextBox ID='txtEmployee_Worker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtInvest'>  Invest*</label>
<asp:TextBox ID='txtInvest' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtDICCenter'>  DICCenter*</label>
<asp:TextBox ID='txtDICCenter' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlLastYearITR'>  Last Year ITR*</label>
<asp:DropDownList ID='ddlLastYearITR' runat='server' class='form-control' Width="100%">
     <asp:ListItem >Yes</asp:ListItem>
    <asp:ListItem >No</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>

<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>
</section>


<div class='form-section'>
<div class='row'>
<div class='col-md-12'>
 <asp:Label ID='lblRID' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblError' runat='server' Text=''></asp:Label> 
</div>
</div>
<div class='row'>
<div class='col-md-12'>
<center> <asp:Button ID='btnSave' runat='server' Text='Save' 
 cssclass='btn btn-primary mar_top10' ValidationGroup='a' />&nbsp;
 <asp:Button ID='btnDelete' runat='server' Text='Delete' 
 cssclass='btn btn-primary mar_top10' Enabled='False' />  &nbsp;
<asp:Button ID='btnClear' runat='server' Text='Reset' 
cssclass='btn btn-primary mar_top10' /> </center>
</div>
</div>
</div>
<div style='margin-top:5px;'>
</div>
</ContentTemplate>
</asp:UpdatePanel></div>
</div>
</div>
 <asp:Button ID='modalPopupButton' runat='server' Text='Button' style='display:none;'/>
<asp:ModalPopupExtender ID='ModalPopupExtender1' runat='server' TargetControlID='modalPopupButton' PopupControlID='DeletePopup'  BackgroundCssClass='modalBackground'  CancelControlID='btnCancel' >
</asp:ModalPopupExtender>
<asp:Panel ID='DeletePopup' runat='server' Width='350px' style='display:none;'  >
<table style='width:100%;background-color:White;border:1px solid gray;'>
<tr>
<td align='center' bgcolor='Silver'>&nbsp;</td>
</tr>
<tr>
<td align='center' bgcolor='Silver'>
<strong>Confirmation Dialog</strong>
<br />
</td>
</tr>
<tr>
<td align='center' bgcolor='Silver'>&nbsp;
</td>
</tr>
<tr>
<td align='center'>&nbsp;
</td>
</tr>
<tr>
<td align='center'>
<asp:Label ID='lblDialogMsg' runat='server' Text=''></asp:Label>  </td>
</tr>
<tr>
<td align='center'>&nbsp;
</td>
</tr>
<tr>
<td align='center'> 
<asp:Button ID='btnPopupYes' runat='server' Text='OK' Width='80px' CssClass='btn btn-primary'/>
&nbsp;&nbsp;&nbsp
<asp:Button ID='btnCancel' runat='server' Text='Cancel' Width='80px' CssClass='btn btn-primary' />
</td>
</tr>
<tr>
<td align='center'>&nbsp; 
</td>
</tr>
</table>
</asp:Panel>

</asp:Content>
