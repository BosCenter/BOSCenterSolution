<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_Gst_Registration.aspx.vb" Inherits="BOSCenter.Frm_Gst_Registration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='GST REGISTRATION'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='GST Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtLastNameofApplicant'>  Last Name of Applicant</label>
<asp:TextBox ID='txtLastNameofApplicant' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtFirstNameofApplicant'>  First Name of Applicant</label>
<asp:TextBox ID='txtFirstNameofApplicant' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtMiddleNameofApplicant'>  Middle Name of Applicant</label>
<asp:TextBox ID='txtMiddleNameofApplicant' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNameOfEnterprise'>  Name Of Enterprise</label>
<asp:TextBox ID='txtNameOfEnterprise' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
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
<div class="col-md-3">
<div class="form-group">
<label for='ddlTypeOfOrganisation'>  Type Of Organisation</label>
<asp:DropDownList ID='ddlTypeOfOrganisation' runat='server' class='form-control'>
 <asp:ListItem >SELF OWNER</asp:ListItem>
 <asp:ListItem >PARTNERSHIP</asp:ListItem>
 <asp:ListItem >PVT.LTD</asp:ListItem>
 <asp:ListItem >TRUST</asp:ListItem>
 
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNATUREOFBUISNESS'>  NATURE OF BUISNESS</label>
<asp:TextBox ID='txtNATUREOFBUISNESS' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtPANNumber'>  PAN Number</label>
<asp:TextBox ID='txtPANNumber' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAddressofENTERPRISES'>  Address of ENTERPRISES</label>
<asp:TextBox ID='txtAddressofENTERPRISES' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtLocalityofENTERPRISES'>  Locality of ENTERPRISES</label>
<asp:TextBox ID='txtLocalityofENTERPRISES' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlStateofENTERPRISES'>  State of ENTERPRISES</label>
<asp:DropDownList ID='ddlStateofENTERPRISES' runat='server' class='form-control'>
<%--<asp:ListItem >Delhi</asp:ListItem>
<asp:ListItem >UP</asp:ListItem>--%>
</asp:DropDownList>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrictofENTERPRISES'>  District of ENTERPRISES</label>
<asp:DropDownList ID='ddlDistrictofENTERPRISES' runat='server' class='form-control'>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtEnterTalukaAndVillageofENTERPRISES'>  Enter Taluka And Village of ENTERPRISES</label>
<asp:TextBox ID='txtEnterTalukaAndVillageofENTERPRISES' runat='server'  class='form-control'></asp:TextBox>
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
<div class="col-md-3">
<div class="form-group">
<label for='txtBankIFSC'>  Bank IFSC</label>
<asp:TextBox ID='txtBankIFSC' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtBankAccNo'>  Bank Acc No</label>
<asp:TextBox ID='txtBankAccNo' runat='server'  class='form-control'></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtBankAccNo_FilteredTextBoxExtender" 
        runat="server" Enabled="True" FilterType="Numbers" 
        TargetControlID="txtBankAccNo" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtEmployee_Worker'>  Employee / Worker</label>
<asp:TextBox ID='txtEmployee_Worker' runat='server'  class='form-control'></asp:TextBox>
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
