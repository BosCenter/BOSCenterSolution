<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_NSDC.aspx.vb" Inherits="BOSCenter.Frm_NSDC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='NSDC Form'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='NSDC Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtRegistrationDate'>  Registration Date*</label>
<asp:TextBox ID='txtRegistrationDate' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:CalendarExtender ID="txtRegistrationDate_CalendarExtender" runat="server" BehaviorID="txtRegistrationDate_CalendarExtender" TargetControlID="txtRegistrationDate" Format="dd/MM/yyyy">
    </asp:CalendarExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtTrainingCentreName'>  Training Centre Name*</label>
<asp:TextBox ID='txtTrainingCentreName' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAddress'>  Address*</label>
<asp:TextBox ID='txtAddress' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNearbyLandmark'>  Near by Landmark*</label>
<asp:TextBox ID='txtNearbyLandmark' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtPincode'>  Pin code*</label>
<asp:TextBox ID='txtPincode' runat='server'  class='form-control' Width="100%"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtPincode_FilteredTextBoxExtender" runat="server" BehaviorID="txtPincode_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtPincode" ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlState_UnionTerritory'>  State / Union Territory*</label>
<asp:DropDownList ID='ddlState_UnionTerritory' runat='server' class='form-control' Width="100%"></asp:DropDownList>
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
<label for='txtTehsil_Mandal_Block'>  Tehsil / Mandal / Block*</label>
<asp:TextBox ID='txtTehsil_Mandal_Block' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtCity_Village_Town'>  City / Village / Town*</label>
<asp:TextBox ID='txtCity_Village_Town' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtParliamentaryConstituency'>  Parliamentary Constituency*</label>
<asp:TextBox ID='txtParliamentaryConstituency' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtGeoLocation'>  Geo Location*</label>
<asp:TextBox ID='txtGeoLocation' runat='server'  class='form-control' Width="100%"></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlAddressProof'>  Address Proof*</label>
<asp:DropDownList ID='ddlAddressProof' runat='server' class='form-control' Width="100%">
 <asp:ListItem >Electricity Bill (Not Older Than 2 Months)</asp:ListItem>
<asp:ListItem >Rent Agreement</asp:ListItem>
<asp:ListItem >Telephone Bill (BSNL/MTNL Only)</asp:ListItem>
<asp:ListItem >GST Registration</asp:ListItem>
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
