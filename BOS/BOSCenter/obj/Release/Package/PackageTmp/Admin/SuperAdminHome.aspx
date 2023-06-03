<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="SuperAdminHome.aspx.vb" Inherits="BOSCenter.SuperAdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--<div class="col-sm-12 mar_top30">
<h3 class="mar_lft10">Member Registration </h3> <hr />

<asp:UpdatePanel runat='server' ID='updatepanel1'>
<ContentTemplate>
<section>
<div class='container'>
<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">

<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtQuantityPurchased' class="col-md-4 control-label" > Enter QuantityPurchased</label>
<div class="col-md-8">
<asp:TextBox ID='txtQuantityPurchased' runat='server'  placeholder='QuantityPurchased' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='ddlPaymentMethod' class="col-md-4 control-label"> Enter PaymentMethod</label>
<div class="col-md-8">
<asp:DropDownList ID='ddlPaymentMethod' runat='server' class='form-control'></asp:DropDownList>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtInvoiceNo' class="col-md-4 control-label"> Enter InvoiceNo</label>
<div class="col-md-8">
<asp:TextBox ID='txtInvoiceNo' runat='server'  placeholder='InvoiceNo' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtInvoiceDate' class="col-md-4 control-label"> Enter InvoiceDate</label>
<div class="col-md-8">
<asp:TextBox ID='txtInvoiceDate' runat='server'  placeholder='InvoiceDate' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtMRP' class="col-md-4 control-label"> Enter MRP</label>
<div class="col-md-8">
<asp:TextBox ID='txtMRP' runat='server'  placeholder='MRP' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtSP' class="col-md-4 control-label"> Enter SP</label>
<div class="col-md-8">
<asp:TextBox ID='txtSP' runat='server'  placeholder='SP' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtTP' class="col-md-4 control-label"> Enter TP</label>
<div class="col-md-8">
<asp:TextBox ID='txtTP' runat='server'  placeholder='TP' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtCommission' class="col-md-4 control-label"> Enter Commission</label>
<div class="col-md-8">
<asp:TextBox ID='txtCommission' runat='server'  placeholder='Commission' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtOurPayout' class="col-md-4 control-label"> Enter OurPayout</label>
<div class="col-md-8">
<asp:TextBox ID='txtOurPayout' runat='server'  placeholder='OurPayout' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtWeight_KG' class="col-md-4 control-label"> Enter Weight_KG</label>
<div class="col-md-8">
<asp:TextBox ID='txtWeight_KG' runat='server'  placeholder='Weight_KG' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtCourier' class="col-md-4 control-label"> Enter Courier</label>
<div class="col-md-8">
<asp:TextBox ID='txtCourier' runat='server'  placeholder='Courier' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtAWB_No' class="col-md-4 control-label"> Enter AWB_No</label>
<div class="col-md-8">
<asp:TextBox ID='txtAWB_No' runat='server'  placeholder='AWB_No' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtBuyerName' class="col-md-4 control-label"> Enter BuyerName</label>
<div class="col-md-8">
<asp:TextBox ID='txtBuyerName' runat='server'  placeholder='BuyerName' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtship_address1' class="col-md-4 control-label"> Enter ship_address1</label>
<div class="col-md-8">
<asp:TextBox ID='txtship_address1' runat='server'  placeholder='ship_address1' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtship_address2' class="col-md-4 control-label"> Enter ship_address2</label>
<div class="col-md-8">
<asp:TextBox ID='txtship_address2' runat='server'  placeholder='ship_address2' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtship_address3' class="col-md-4 control-label"> Enter ship_address3</label>
<div class="col-md-8">
<asp:TextBox ID='txtship_address3' runat='server'  placeholder='ship_address3' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtship_City' class="col-md-4 control-label"> Enter ship_City</label>
<div class="col-md-8">
<asp:TextBox ID='txtship_City' runat='server'  placeholder='ship_City' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtship_State' class="col-md-4 control-label"> Enter ship_State</label>
<div class="col-md-8">
<asp:TextBox ID='txtship_State' runat='server'  placeholder='ship_State' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtship_PostalCode' class="col-md-4 control-label"> Enter ship_PostalCode</label>
<div class="col-md-8">
<asp:TextBox ID='txtship_PostalCode' runat='server'  placeholder='ship_PostalCode' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtship_Country' class="col-md-4 control-label"> Enter ship_Country</label>
<div class="col-md-8">
<asp:TextBox ID='txtship_Country' runat='server'  placeholder='ship_Country' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>
<div class="row">
<div class="col-md-6">
<div class="form-group">
<label for='txtBuyer_Email' class="col-md-4 control-label"> Enter Buyer_Email</label>
<div class="col-md-8">
<asp:TextBox ID='txtBuyer_Email' runat='server'  placeholder='Buyer_Email' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-6">
<div class="form-group">
<label for='txtBuyer_PhoneNo' class="col-md-4 control-label"> Enter Buyer_PhoneNo</label>
<div class="col-md-8">
<asp:TextBox ID='txtBuyer_PhoneNo' runat='server'  placeholder='Buyer_PhoneNo' class='form-control'></asp:TextBox>
</div>
</div>
</div>
</div>

</div>
</div>
</div>
</div>
</div>
</section>
</ContentTemplate>
</asp:UpdatePanel>


</div>--%>

</asp:Content>
