<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_Food_Licence.aspx.vb" Inherits="BOSCenter.Frm_Food_Licence" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='FOOD LICENCE'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead2' runat='server' Text='Food Licence Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtNameOfApplicant'>  Name Of Applicant</label>
<asp:TextBox ID='txtNameOfApplicant' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDesignation'>  Designation</label>
<asp:DropDownList ID='ddlDesignation' runat='server' class='form-control'>
<asp:ListItem >Individual</asp:ListItem>
<asp:ListItem >Partner</asp:ListItem>
<asp:ListItem >Proprieter</asp:ListItem>
<asp:ListItem >Secretary of dairy co-operative Sociaty</asp:ListItem>
<asp:ListItem >Other</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlKindofBusiness'>  Kind of Business</label>
<asp:DropDownList ID='ddlKindofBusiness' runat='server' class='form-control'>
<asp:ListItem >Hawker</asp:ListItem>
<asp:ListItem >Permanent / Temporary Stall Holder</asp:ListItem>
<asp:ListItem >Home based canteens/dabba wallas</asp:ListItem>
<asp:ListItem >Petty retailer tea/ snacks</asp:ListItem>
<asp:ListItem >Manufacturer/Proccessor</asp:ListItem>
<asp:ListItem >repacker</asp:ListItem>
<asp:ListItem >Food stalls/arrangements in Religious gatherings, fairs etc</asp:ListItem>
<asp:ListItem >Milk producers (who are not member of dairy co opera Sociaty)/ Milk Vendor</asp:ListItem>
<asp:ListItem >Fish/meat/poultry shop/seller</asp:ListItem>
<asp:ListItem >Other(s)</asp:ListItem>
<asp:ListItem >Food Vending Agencies</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtAddressofBusiness'>  Address of Business</label>
<asp:TextBox ID='txtAddressofBusiness' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlStateofBusiness'>  State of Business</label>
<asp:DropDownList ID='ddlStateofBusiness' runat='server' class='form-control'>
<%--<asp:ListItem>Delhi</asp:ListItem>
<asp:ListItem>U.p</asp:ListItem>--%>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtSubDivision_Station_DivisionRailways'>  SubDivision / Station / Division Railways</label>
<asp:TextBox ID='txtSubDivision_Station_DivisionRailways' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlDistrictofBussiness'>  District of Bussiness</label>
<asp:DropDownList ID='ddlDistrictofBussiness' runat='server' class='form-control'>
<%--<asp:ListItem>Delhi</asp:ListItem>
<asp:ListItem>U.p</asp:ListItem>--%>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPINCodeofBusiness'>  PIN Code of Business</label>
<asp:TextBox ID='txtPINCodeofBusiness' runat='server'  class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="txtPINCodeofBusiness_FilteredTextBoxExtender1" 
        runat="server" Enabled="True" TargetControlID="txtPINCodeofBusiness" 
        ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='ddlIsyourCorrespondenceAddresssameasAddressofPremises'>  Is your Correspondence Address same as Address of Premises?</label>
<asp:DropDownList ID='ddlIsyourCorrespondenceAddresssameasAddressofPremises' runat='server' class='form-control'>
<asp:ListItem >Yes</asp:ListItem>
<asp:ListItem >No</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtIFYESEnterDetailHere'>  IF YES, Enter Detail Here</label>
<asp:TextBox ID='txtIFYESEnterDetailHere' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtMobileNumber'>  Mobile Number</label>
<asp:TextBox ID='txtMobileNumber' runat='server'  class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="txtMobilNumber_FilteredTextBoxExtender" 
        runat="server" Enabled="True" TargetControlID="txtMobileNumber" 
        ValidChars="0123456789">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtemailId'>  Email Id</label>
<asp:TextBox ID='txtemailId' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtNatureofBusiness'>  Nature of Business</label>
<asp:TextBox ID='txtNatureofBusiness' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlNoofyearsyouwanttoapplyfor'>  No of years you want to apply for</label>
<asp:DropDownList ID='ddlNoofyearsyouwanttoapplyfor' runat='server' class='form-control'>
<asp:ListItem >1</asp:ListItem>
<asp:ListItem >2</asp:ListItem>
<asp:ListItem >3</asp:ListItem>
<asp:ListItem >4</asp:ListItem>
<asp:ListItem >5</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtNameofthefoodcategory'>  Name of the food category</label>
<asp:TextBox ID='txtNameofthefoodcategory' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtDateofEstablishment'>  Date of Establishment</label>
<asp:TextBox ID='txtDateofEstablishment' runat='server'  class='form-control'></asp:TextBox>
 <asp:CalendarExtender ID="txtDateofEstablishment_CalendarExtender" runat="server" 
        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateofestablishment">
    </asp:CalendarExtender>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtopeningandclosingperiodoftheyear'> In case of Seasonal business, State the opening and closing period of the year</label>
<asp:TextBox ID='txtopeningandclosingperiodoftheyear' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlSourceofWaterSupply'> SELECT Source of Water Supply</label>
<asp:DropDownList ID='ddlSourceofWaterSupply' runat='server' class='form-control'>
<asp:ListItem >Public</asp:ListItem>
<asp:ListItem >Private</asp:ListItem>
<asp:ListItem >Other(s)</asp:ListItem>
<asp:ListItem >N/A</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='ddlelectricpowerisusedinmanufacturingoffooditems'> Whether any electric power is used in manufacturing of food items</label>
<asp:DropDownList ID='ddlelectricpowerisusedinmanufacturingoffooditems' runat='server' class='form-control'>
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
