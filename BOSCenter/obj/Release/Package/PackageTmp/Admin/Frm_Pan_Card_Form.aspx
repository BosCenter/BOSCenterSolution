<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_Pan_Card_Form.aspx.vb" Inherits="BOSCenter.Frm_Pan_Card_Form" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='PAN CARD FORM'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='Pan Card Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlSelectTitle'>  Select Title*</label>
<asp:DropDownList ID='ddlSelectTitle' runat='server' class='form-control' Width="100%">
 <asp:ListItem >KUMARI/MS</asp:ListItem>
 <asp:ListItem >SHRI</asp:ListItem>
 <asp:ListItem >SMT/MRS</asp:ListItem>
 
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFirstName'>  Applicant First Name*</label>
<asp:TextBox ID='txtApplicantFirstName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantMiddleName'>  Applicant Middle Name*</label>
<asp:TextBox ID='txtApplicantMiddleName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantLastName'>  Applicant Last Name*</label>
<asp:TextBox ID='txtApplicantLastName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFatherFirstName'>  Applicant Father First Name*</label>
<asp:TextBox ID='txtApplicantFatherFirstName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFatherMiddleName'>  Applicant Father Middle Name*</label>
<asp:TextBox ID='txtApplicantFatherMiddleName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFatherLastName'>  Applicant Father Last Name*</label>
<asp:TextBox ID='txtApplicantFatherLastName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtDOB'>  DOB*</label>
<asp:TextBox ID='txtDOB' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" BehaviorID="txtDOB_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txtDOB">
    </asp:CalendarExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlGender'>  Gender*</label>
<asp:DropDownList ID='ddlGender' runat='server' class='form-control' Width="100%">
 <asp:ListItem >Male</asp:ListItem>
 <asp:ListItem >Female</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtCardName'>  Card Name*</label>
<asp:TextBox ID='txtCardName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAadhaarNumber'>  Aadhaar Number*</label>
<asp:TextBox ID='txtAadhaarNumber' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtAadhaarNumber_FilteredTextBoxExtender" runat="server" BehaviorID="txtAadhaarNumber_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtAadhaarNumber" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNameonAadhaar'>  Name on Aadhaar*</label>
<asp:TextBox ID='txtNameonAadhaar' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
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
<label for='txtEmail'>  Email*</label>
<asp:TextBox ID='txtEmail' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlPanDeliveryState'>  Pan Delivery State*</label>
<asp:DropDownList ID='ddlPanDeliveryState' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlProofOfIdentity'>  Proof Of Identity*</label>
<asp:DropDownList ID='ddlProofOfIdentity' runat='server' class='form-control' Width="100%">
 <asp:ListItem >Aadhaar card issued by unique identification authority of india</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlProofOfAddress'>  Proof Of Address*</label>
<asp:DropDownList ID='ddlProofOfAddress' runat='server' class='form-control' Width="100%">
<asp:ListItem >Aadhaar card issued by unique identification authority of india</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlProofOfDOB'>  Proof Of DOB*</label>
<asp:DropDownList ID='ddlProofOfDOB' runat='server' class='form-control' Width="100%">
<asp:ListItem >Aadhaar card issued by unique identification authority of india</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtHouseNo_Building_Landmark'>  HouseNo / Building / Landmark*</label>
<asp:TextBox ID='txtHouseNo_Building_Landmark' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlStatusofapplicant'>  Status of applicant*</label>
<asp:DropDownList ID='ddlStatusofapplicant' runat='server' class='form-control' Width="100%">
<asp:ListItem >Individual</asp:ListItem>
<asp:ListItem >Trusts</asp:ListItem>
<asp:ListItem >Hindu undivided family</asp:ListItem>
<asp:ListItem >Company</asp:ListItem>
<asp:ListItem >Partnership Firm</asp:ListItem>
<asp:ListItem >Association of Persons</asp:ListItem>
<asp:ListItem >Body of Individuals</asp:ListItem>
<asp:ListItem >Local Authority</asp:ListItem>
<asp:ListItem >Artificial Juridical Persons</asp:ListItem>
<asp:ListItem >Limited Liability Partnership</asp:ListItem>

</asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtRegistrationNumberforcompany_firms_LLPs'>  Registration Number for company / firms / LLPs*</label>
<asp:TextBox ID='txtRegistrationNumberforcompany_firms_LLPs' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlSourceofIncome'>  Source of Income*</label>
<asp:DropDownList ID='ddlSourceofIncome' runat='server' class='form-control' Width="100%">
<asp:ListItem >Salary</asp:ListItem>
<asp:ListItem >Capital Gains</asp:ListItem>
<asp:ListItem >Income from Business / Profession</asp:ListItem>
<asp:ListItem >Income from Other sources</asp:ListItem>
<asp:ListItem >Income from House property</asp:ListItem>
<asp:ListItem >No income</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtVillage'>  Village*</label>
<asp:TextBox ID='txtVillage' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPost1'>  Post1*</label>
<asp:TextBox ID='txtPost1' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtPost2'>  Post2*</label>
<asp:TextBox ID='txtPost2' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrict'>  District*</label>
<asp:DropDownList ID='ddlDistrict' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrictArea'>  District Area*</label>
<asp:DropDownList ID='ddlDistrictArea' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAREACODE'>  AREA CODE*</label>
<asp:TextBox ID='txtAREACODE' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtAOType'>  AO Type*</label>
<asp:TextBox ID='txtAOType' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtRANGECODE'>  RANGE CODE*</label>
<asp:TextBox ID='txtRANGECODE' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAONO'>  AO NO*</label>
<asp:TextBox ID='txtAONO' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlState'>  State*</label>
<asp:DropDownList ID='ddlState' runat='server' class='form-control' Width="100%"></asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtPin'>  Pin*</label>
<asp:TextBox ID='txtPin' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPin_FilteredTextBoxExtender" runat="server" BehaviorID="txtPin_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPin" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
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
