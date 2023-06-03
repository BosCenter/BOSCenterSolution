<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_Voter_ID.aspx.vb" Inherits="BOSCenter.Frm_Voter_ID" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='VOTER ID'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='Voter ID Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantLastName'>  Applicant Last Name</label>
<asp:TextBox ID='txtApplicantLastName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFirstName'>  Applicant First Name</label>
<asp:TextBox ID='txtApplicantFirstName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantMiddleName'>  Applicant Middle Name</label>
<asp:TextBox ID='txtApplicantMiddleName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFathersLastName'>  Applicant Father's LastName</label>
<asp:TextBox ID='txtApplicantFathersLastName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFathersFirstName'>  Applicant Father's First Name</label>
<asp:TextBox ID='txtApplicantFathersFirstName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFathersMiddleName'>  Applicant Father's Middle Name</label>
<asp:TextBox ID='txtApplicantFathersMiddleName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtMobileNumber'>  Mobile Number</label>
<asp:TextBox ID='txtMobileNumber' runat='server'  class='form-control'></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtMobileNumber_FilteredTextBoxExtender" 
        runat="server" Enabled="True" FilterType="Numbers" 
        TargetControlID="txtMobileNumber" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtEmail'>  Email</label>
<asp:TextBox ID='txtEmail' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtMothersName'>  Mother's Name</label>
<asp:TextBox ID='txtMothersName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtDOB'>  DOB (DD/MM/YYYY)</label>
<asp:TextBox ID="txtDOB" runat="server" CssClass="form-control"></asp:TextBox>
      <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" 
        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDOB">
    </asp:CalendarExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlGender'>  Gender</label>
<asp:DropDownList ID='ddlGender' runat='server' class='form-control'>
 <asp:ListItem >Male</asp:ListItem>
<asp:ListItem >Female</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNAMEOFPARLIMANTRY_ASSEMBLY'>  NAME OF PARLIMANTRY ASSEMBLY</label>
<asp:TextBox ID='txtNAMEOFPARLIMANTRY_ASSEMBLY' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlMaritalStatus'>  Marital Status</label>
<asp:DropDownList ID='ddlMaritalStatus' runat='server' class='form-control'>
 <asp:ListItem >Single</asp:ListItem>
<asp:ListItem >Married</asp:ListItem>
<asp:ListItem >Widowed</asp:ListItem>
<asp:ListItem >Separated</asp:ListItem>
<asp:ListItem >Divorced</asp:ListItem>


</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlStates'>  States</label>
<asp:DropDownList ID='ddlStates' runat='server' class='form-control'>
<%--<asp:ListItem >Delhi</asp:ListItem>
<asp:ListItem >UP</asp:ListItem>--%>

</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrict'>  District</label>
<asp:DropDownList ID='ddlDistrict' runat='server' class='form-control'>
<%--<asp:ListItem >East</asp:ListItem>
<asp:ListItem >West</asp:ListItem>--%>

</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtTaluka'>  Taluka</label>
<asp:TextBox ID='txtTaluka' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtVillage'>  Village</label>
<asp:TextBox ID='txtVillage' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtLocation'>  Location</label>
<asp:TextBox ID='txtLocation' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtStreet'>  Street</label>
<asp:TextBox ID='txtStreet' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtBuildingName'>  Building Name</label>
<asp:TextBox ID='txtBuildingName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtHouseNo_Building_Landmark'>  HouseNo / Building / Landmark</label>
<asp:TextBox ID='txtHouseNo_Building_Landmark' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPin'>  Pin</label>
<asp:TextBox ID='txtPin' runat='server'  class='form-control'></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPin_FilteredTextBoxExtender" runat="server" 
        Enabled="True" FilterType="Numbers" TargetControlID="txtPin" 
        ValidChars="0123456789">
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
