<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_Shop_Act.aspx.vb" Inherits="BOSCenter.Frm_Shop_Act" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='SHOP ACT'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead4' runat='server' Text='Shop Act Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtNameOfEstablishment'>  Name Of Establishment*</label>
<asp:TextBox ID='txtNameOfEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlPreviousdetailofEstablishment'>  Previous detail of Establishment*</label>
<asp:DropDownList ID='ddlPreviousdetailofEstablishment' runat='server' class='form-control' Width="100%">
<asp:ListItem >New</asp:ListItem>
<asp:ListItem >Renewal</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtIfRenewalEnterShopactNo'>  If Renewal Enter Shop Act No*</label>
<asp:TextBox ID='txtIfRenewalEnterShopactNo' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAddressofEstablishment'>  Address of Establishment*</label>
<asp:TextBox ID='txtAddressofEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtLocalityofEstablishment'>  Locality of Establishment*</label>
<asp:TextBox ID='txtLocalityofEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlStateofEstablishment'>  State of Establishment*</label>
<asp:DropDownList ID='ddlStateofEstablishment' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrictofEstablishment'>  District of Establishment*</label>
<asp:DropDownList ID='ddlDistrictofEstablishment' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtTalukaAndVillageofEstablishment'>  Taluka And Village of Establishment*</label>
<asp:TextBox ID='txtTalukaAndVillageofEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtPINCodeofEstablishment'>  PIN Code of Establishment*</label>
<asp:TextBox ID='txtPINCodeofEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPINCodeofEstablishment_FilteredTextBoxExtender" runat="server" BehaviorID="txtPINCodeofEstablishment_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPINCodeofEstablishment" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtMobileNumber'>  Mobile Number*</label>
<asp:TextBox ID='txtMobileNumber' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtMobileNumber_FilteredTextBoxExtender" runat="server" BehaviorID="txtMobileNumber_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtMobileNumber" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlOwnershipofPremises'>  Ownership of Premises*</label>
<asp:DropDownList ID='ddlOwnershipofPremises' runat='server' class='form-control' Width="100%">
<asp:ListItem >Rent</asp:ListItem>
<asp:ListItem >Own</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtDateofEstablishment'>  Date of Establishment*</label>
<asp:TextBox ID='txtDateofEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:CalendarExtender ID="txtDateofEstablishment_CalendarExtender" runat="server" BehaviorID="txtDateofEstablishment_CalendarExtender" TargetControlID="txtDateofEstablishment" Format="dd/MM/yyyy">
    </asp:CalendarExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtNatureofBusiness'>  Nature of Business*</label>
<asp:TextBox ID='txtNatureofBusiness' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>

<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>
</section>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead3' runat='server' Text='Worker Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtNoOfMenWorker'>  No Of Men Worker*</label>
<asp:TextBox ID='txtNoOfMenWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtNoOfMenWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtNoOfMenWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtNoOfMenWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtNoOfWomenWorker'>  No Of Women Worker*</label>
<asp:TextBox ID='txtNoOfWomenWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtNoOfWomenWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtNoOfWomenWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtNoOfWomenWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtNoOfOtherWorker'>  No Of Other Worker*</label>
<asp:TextBox ID='txtNoOfOtherWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtNoOfOtherWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtNoOfOtherWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtNoOfOtherWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtApprenticesMenWorker'>  Apprentices Men Worker*</label>
<asp:TextBox ID='txtApprenticesMenWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtApprenticesMenWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtApprenticesMenWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtApprenticesMenWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtApprenticesWomenWorker'>  Apprentices Women Worker*</label>
<asp:TextBox ID='txtApprenticesWomenWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtApprenticesWomenWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtApprenticesWomenWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtApprenticesWomenWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtApprenticesOtherWorker'>  Apprentices Other Worker*</label>
<asp:TextBox ID='txtApprenticesOtherWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtApprenticesOtherWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtApprenticesOtherWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtApprenticesOtherWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtPartTimeMenWorker'>  PartTime Men Worker*</label>
<asp:TextBox ID='txtPartTimeMenWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPartTimeMenWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtPartTimeMenWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPartTimeMenWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtPartTimeWomenWorker'>  PartTime Women Worker*</label>
<asp:TextBox ID='txtPartTimeWomenWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPartTimeWomenWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtPartTimeWomenWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPartTimeWomenWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtPartTimeOtherWorker'>  PartTime Other Worker*</label>
<asp:TextBox ID='txtPartTimeOtherWorker' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPartTimeOtherWorker_FilteredTextBoxExtender" runat="server" BehaviorID="txtPartTimeOtherWorker_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPartTimeOtherWorker" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>

<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>
</section>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='Employer Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtFullNameofEmployer'>  Full Name of Employer*</label>
<asp:TextBox ID='txtFullNameofEmployer' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAddressofEmployer'>  Address of Employer*</label>
<asp:TextBox ID='txtAddressofEmployer' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtLocalityofEmployer'>  Locality of Employer*</label>
<asp:TextBox ID='txtLocalityofEmployer' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlStateofEmployer'>  State of Employer*</label>
<asp:DropDownList ID='ddlStateofEmployer' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrictofEmployer'>  District of Employer*</label>
<asp:DropDownList ID='ddlDistrictofEmployer' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtTalukaAndVillageofEmployer'>  Taluka And Village of Employer*</label>
<asp:TextBox ID='txtTalukaAndVillageofEmployer' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPINCodeofEmployer'>  PIN Code of Employer*</label>
<asp:TextBox ID='txtPINCodeofEmployer' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPINCodeofEmployer_FilteredTextBoxExtender" runat="server" BehaviorID="txtPINCodeofEmployer_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPINCodeofEmployer" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtEmployerMobileNumber'>  Employer Mobile Number*</label>
<asp:TextBox ID='txtEmployerMobileNumber' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtEmployerMobileNumber_FilteredTextBoxExtender" runat="server" BehaviorID="txtEmployerMobileNumber_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtEmployerMobileNumber" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtEmployeremailId'>  Employer email Id*</label>
<asp:TextBox ID='txtEmployeremailId' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtResidenceSince'>  Residence Since*</label>
<asp:TextBox ID='txtResidenceSince' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
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
<label for='txtEmployerStatus_Designation'>  Employer Status / Designation*</label>
<asp:TextBox ID='txtEmployerStatus_Designation' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtCategoryofEstablishment'>  Category of Establishment*</label>
<asp:TextBox ID='txtCategoryofEstablishment' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtCategoryDetail'>  Category Detail*</label>
<asp:TextBox ID='txtCategoryDetail' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtEstablishmentType'>  Establishment Type*</label>
<asp:TextBox ID='txtEstablishmentType' runat='server'  class='form-control' Width="100%"></asp:TextBox>
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
