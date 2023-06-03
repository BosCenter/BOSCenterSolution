<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_Gazzet.aspx.vb" Inherits="BOSCenter.Frm_Gazzet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='GAZZET FORM'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='GAZZET Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlApplicantCategory'>  Applicant Category</label>
<asp:DropDownList ID='ddlApplicantCategory' runat='server' class='form-control'>
 <asp:ListItem >Open Category</asp:ListItem>
 <asp:ListItem >Backward</asp:ListItem>
 <asp:ListItem >OBC Category</asp:ListItem>
<asp:ListItem >Minor</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlApplicantTitle'>  Applicant Title</label>
<asp:DropDownList ID='ddlApplicantTitle' runat='server' class='form-control'>
 <asp:ListItem >SHRI</asp:ListItem>
 <asp:ListItem >KUMARI</asp:ListItem>
 <asp:ListItem >MS</asp:ListItem>
<asp:ListItem >SHRI</asp:ListItem>
<asp:ListItem >SMT</asp:ListItem>
<asp:ListItem >MRS</asp:ListItem>
<asp:ListItem >Vakil</asp:ListItem>

</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantFullName'>  Applicant Full Name</label>
<asp:TextBox ID='txtApplicantFullName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtApplicantDOB'>  Applicant DOB (DD/MM/YYYY)</label>
 <asp:TextBox ID="txtApplicantDOB" runat="server" CssClass="form-control"></asp:TextBox>
      <asp:CalendarExtender ID="txtApplicantDOB_CalendarExtender" runat="server" 
        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtApplicantDOB">
    </asp:CalendarExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtAge'>  Age</label>
<asp:TextBox ID='txtAge' runat='server'  class='form-control'></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtAge_FilteredTextBoxExtender" runat="server" 
        Enabled="True" FilterType="Numbers" TargetControlID="txtAge" 
        ValidChars="0123456789.">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlGender'>  Gender</label>
<asp:DropDownList ID='ddlGender' runat='server' class='form-control'>
<asp:ListItem >Female</asp:ListItem>
<asp:ListItem >Male</asp:ListItem>
<asp:ListItem >Other</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtMobileNo'>  MobileNo</label>
<asp:TextBox ID='txtMobileNo' runat='server'  class='form-control'></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtMobileNo_FilteredTextBoxExtender" 
        runat="server" Enabled="True" FilterType="Numbers" 
        TargetControlID="txtMobileNo" ValidChars="0123456789">
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
<label for='txtAdhaarNo'>  Adhaar No</label>
<asp:TextBox ID='txtAdhaarNo' runat='server'  class='form-control'></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtAdhaarNo_FilteredTextBoxExtender" 
        runat="server" Enabled="True" FilterType="Numbers" 
        TargetControlID="txtAdhaarNo" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAddress'>  Address</label>
<asp:TextBox ID='txtAddress' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrict'>  District</label>
<asp:DropDownList ID='ddlDistrict' runat='server' class='form-control'>
<%--<asp:ListItem >Delhi</asp:ListItem>
<asp:ListItem >UP</asp:ListItem>--%>
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
<label for='txtPin'>  Pin</label>
<asp:TextBox ID='txtPin' runat='server'  class='form-control'></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPin_FilteredTextBoxExtender" runat="server" 
        Enabled="True" FilterType="Numbers" TargetControlID="txtPin" 
        ValidChars="123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlApplicationfor'>  Application for</label>
<asp:DropDownList ID='ddlApplicationfor' runat='server' class='form-control'>
<asp:ListItem >Change in Name</asp:ListItem>
<asp:ListItem >Change in DOB</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtIfChangeInDateOfBirth'>  If Change In Date Of Birth</label>
<asp:TextBox ID='txtIfChangeInDateOfBirth' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtIfChangeInNameOldFirstName'>  If Change In Name Old First Name</label>
<asp:TextBox ID='txtIfChangeInNameOldFirstName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtOldMidleName'>  Old Midle Name</label>
<asp:TextBox ID='txtOldMidleName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtOldLastName'>  Old Last Name</label>
<asp:TextBox ID='txtOldLastName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNewFirstName'>  New First Name</label>
<asp:TextBox ID='txtNewFirstName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtNewMidleName'>  New Middle Name</label>
<asp:TextBox ID='txtNewMidleName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNewLastName'>  New Last Name</label>
<asp:TextBox ID='txtNewLastName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtReason'>  Reason</label>
<asp:TextBox ID='txtReason' runat='server'  class='form-control'></asp:TextBox>
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
