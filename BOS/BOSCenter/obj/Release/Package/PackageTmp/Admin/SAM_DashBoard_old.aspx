<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="SAM_DashBoard_old.aspx.vb" Inherits="BOSCenter.SAM_DashBoard_old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<style type="text/css">
    .form-section-head-A
 {
     
         border: 1px solid #ccc;
    padding: 9px 5px 0;
    margin-top: 5px;
    background-color: #337ab7;
    text-align: center;
    color: White;
    height: 50px;
    vertical-align: middle;
    font-weight: bold;
     }
     
        .form-section-head-B
 {
     
         border: 1px solid #ccc;
    padding: 9px 5px 0;
    margin-top: 5px;
    background-color: #5cb85c;
    text-align: center;
    color: White;
    height: 50px;
    vertical-align: middle;
    font-weight: bold;
     }
              .form-section-head-C
 {
     
         border: 1px solid #ccc;
    padding: 9px 5px 0;
    margin-top: 5px;
    background-color: #5bc0de;
    text-align: center;
    color: White;
    height: 50px;
    vertical-align: middle;
    font-weight: bold;
     }
     
       .form-section-head-D
 {
     
         border: 1px solid #ccc;
    padding: 9px 5px 0;
    margin-top: 5px;
    background-color:  #f0ad4e;
    text-align: center;
    color: White;
    max-height:200px;
        min-height:50px;
    vertical-align: middle;
    font-weight: bold;
     }
      
      
        .form-section-head-E
 {
     
         border: 1px solid #ccc;
    padding: 9px 5px 0;
    margin-top: 5px;
    background-color: #d9534f;
    text-align: center;
    color: White;
    height: 50px;
    vertical-align: middle;
    font-weight: bold;
     }
  </style>

    <style type="text/css">
        .style1
        {
            width: 138px;
        }
        .style2
        {
            width: 125px;
        }
         .section-step {
  border-radius: 0.25em; 
  box-shadow: 0 0 0 1px #ccc;
        
    </style>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <%--Second Row--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" >
<div class="row" runat="server" id="Div_API" style="margin-top:20px;" >
<div class="col-md-12" >
<%--First Row--%>
<div class="row">
<div class="col-md-3" runat="server" id="div_API_Balance" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;API BALANCE</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-mobile" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12 mar_lft30 ">
    &nbsp;&nbsp;<asp:Label ID="lbl_API_Balance" runat="server" style="font-weight:bold;font-size:large" Visible="True" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12 mar_top10">
    <asp:LinkButton ID="btnAPIBalanceRefresh_N" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_lft30 "  Text=""><i class="fa fa-refresh"> </i> </asp:LinkButton> &nbsp; &nbsp; &nbsp; &nbsp; <asp:LinkButton ID="btnAPIBalanceReport" runat="server" CausesValidation="False" CssClass="btn btn-danger"  Text=""><i class="fa fa-list"> </i> </asp:LinkButton></div>
    </div>

</div>
</div>
</div>
</div>

<div class="col-md-3" runat="server" id="div_API_walletTransfer" >
<div class="form-section sa"  style="color:White;margin-left:4px; background: linear-gradient(to bottom right, green, yellow)">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Wallet Transfer<asp:Button ID="lblIsMobileView"  runat="server" Visible="false"  /></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-exchange" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
   
    &nbsp;&nbsp;<asp:Label ID="Label10" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="btnWalletTransfer" CssClass="btn btn-primary" PostBackUrl="BOS_TransferAmount_Form.aspx" runat="server"  Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
<div class="col-md-3" runat="server" id="div_Recharge" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;Recharge & Bill Payment</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-mobile" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="lblapplication" runat="server" style="font-weight:bold;" Visible="false" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12">
    <asp:Button ID="btnRecharge" CssClass="btn btn-primary" PostBackUrl="BOS_RechargeAPI.aspx" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>
</div>
<div class="col-md-3" runat="server"  id="div_MoneyTransfer" >
<div class="form-section sa"  style="color:White;margin-left:4px;background: linear-gradient(to bottom right, brown,orange 50%);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Money Transfer</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-money" style="color:white;font-size:50"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="lblcalllogs" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="btnMoneyTransfer" style="" CssClass="btn btn-primary" PostBackUrl="BOS_MoneyTransfer.aspx" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
<div class="col-md-3" runat="server" id="div_PanCard" >
<div class="form-section sa"  style="color:White;margin-left:4px; background: linear-gradient(to bottom right, blue,cyan)">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;PAN Card</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-credit-card" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="lblcallrec" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="btnPanCard" style="" PostBackUrl="BOS_PanCard.aspx" CssClass="btn btn-primary" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>








</div>
<%--Second Row--%>
<div class="row">

<div class="col-md-3" runat="server"  id="div_API_HotelBooking" >
<div class="form-section sa"  style="color:White;margin-left:4px;background: linear-gradient(to bottom right, brown,orange 50%);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Hotel Booking</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-hotel" style="color:white;font-size:50"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="Button2" style="" CssClass="btn btn-primary" Visible="false" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
<div class="col-md-3" runat="server" id="div_API_BusBooking" >
<div class="form-section sa"  style="color:White;margin-left:4px; background: linear-gradient(to bottom right, blue,cyan)">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Bus Booking</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-bus" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label4" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="Button3" style="" Visible="false"  CssClass="btn btn-primary" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
<div class="col-md-3" runat="server" id="div_API_Fasttag" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Fast Tag</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-truck" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" style="font-weight:bold;" Visible="false" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12">
    <asp:Button ID="Button4" CssClass="btn btn-primary" Visible="false"  runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>
</div>
<div class="col-md-3" runat="server"  id="div_API_Insurance" >
<div class="form-section sa"  style="color:White;margin-left:4px;background: linear-gradient(to bottom right, brown,orange 50%);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Insurance</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-umbrella" style="color:white;font-size:50"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label5" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="Button5" style="" CssClass="btn btn-primary" Visible="false"  runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>


</div>
<%--Third Row--%>
<div class="row">
<div class="col-md-3" runat="server" id="div_API_AadhaarAtm" >
<div class="form-section sa"  style="color:White;margin-left:4px; background: linear-gradient(to bottom right, blue,cyan)">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Aadhaar ATM</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-id-card" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="Button6" style="" CssClass="btn btn-primary" runat="server" Visible="false"  Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
<div class="col-md-3" runat="server" id="div_API_Loan" >
<div class="form-section sa"  style="color:White;margin-left:4px; background: linear-gradient(to bottom right, brown,orange 50%)">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Loan</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-briefcase" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label7" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="Button7" style=""  Visible="false" CssClass="btn btn-primary" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
<div class="col-md-3" runat="server" id="div_API_Eshopping" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, blue,cyan);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;E-Shopping</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-cart-arrow-down" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label8" runat="server" style="font-weight:bold;" Visible="false" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12">
    <asp:Button ID="Button8" CssClass="btn btn-primary" Visible="false"  runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>
</div>
<div class="col-md-3" runat="server"  id="div_API_bosoutlet" >
<div class="form-section sa"  style="color:White;margin-left:4px;background: linear-gradient(to bottom right, brown,orange 50%);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Outlet</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-building" style="color:white;font-size:50"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label9" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="Button9" style="" CssClass="btn btn-primary" Visible="false"  runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
</div>
<%--Fourth Row--%>
<div class="row">


<div class="col-md-3" runat="server" id="div_API_FlightBooking" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Flight Booking</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-plane" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" style="font-weight:bold;" Visible="false" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12">
    <asp:Button ID="Button1" CssClass="btn btn-primary" Visible="false" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>
</div>





<div class="col-md-3" runat="server" id="div_API_MiniATM" >
<div class="form-section sa"  style="color:White;margin-left:4px; background: linear-gradient(to bottom right, blue,cyan)">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Mini ATM</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-money" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label11" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label></div>
    </div>
   
    <div class="row">
    <div class="col-md-12">
    <asp:Button ID="Button11" style=""  Visible="false" CssClass="btn btn-primary" runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>  
</div>
<div class="col-md-3" runat="server" id="div_API_TrainBooking" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;Train booking</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-train" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label12" runat="server" style="font-weight:bold;" Visible="false" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12">
    <asp:Button ID="Button12" CssClass="btn btn-primary" Visible="false"  runat="server" Text="Proceed" /></div>
    </div>

</div>
</div>
</div>
</div>

</div>




<%--Div Mobile View - Start--%>


<div class="row" id="divmobile" runat="server"  style="display:none" >

                <center>
       <div class='form-section section-step'>

     
 <asp:Label ID="Label25" runat="server" Font-Size="Large" Text="Wallet Bal" ForeColor="Blue" style="text-align:left;" Visible="false"
       ></asp:Label>
      
      <table style="margin-left:10px;">
      <tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td style="margin-left:15px ;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_WalletPay" runat="server" ImageUrl="../Iconss/wallet1.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label26" runat="server"
    Text="Wallet Pay"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
<asp:ImageButton ID="imgbtn_MoneyTransfer" runat="server" ImageUrl="../Iconss/money-transfer.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label27" runat="server"
    Text="Money Transfer"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_AddWallet" runat="server" ImageUrl="../Iconss/wallet4.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label28" runat="server"
    Text="Add Amount"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_MyAccount" runat="server" ImageUrl="../Iconss/user_1.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label29" runat="server"
    Text="My Account"></asp:Label>
</center>

</td>

</tr>


<tr><td colspan="4">&nbsp;</td></tr>
</table>
       
       
       </div>
       </center>






         <center>
       <div class='form-section section-step'>

     
 <asp:Label ID="Label14" runat="server" Font-Size="Large" Text="Recharge & Bill Payment" ForeColor="Blue" style="text-align:left;"
       ></asp:Label>
       
       <table style="margin-left:10px;">
       <tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td style="margin-left:15px ;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Recharge" runat="server" ImageUrl="../Iconss/mobile-recharge.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label15" runat="server"
    Text="Recharge"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
<asp:ImageButton ID="imgbtn_PostPaid" runat="server" ImageUrl="../Iconss/payment-method.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label16" runat="server"
    Text="PostPaid"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_DTH" runat="server" ImageUrl="../Iconss/DTH.jpg" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label17" runat="server"
    Text="DTH"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Broadband" runat="server" ImageUrl="../Iconss/wifi.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label18" runat="server"
    Text="Broadband"></asp:Label>
</center>

</td>

</tr>

<tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_Electricity" runat="server" ImageUrl="../Iconss/153-512.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label19" runat="server"
    Text="Electricity"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Gas" runat="server" ImageUrl="../Iconss/gas.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label20" runat="server"
    Text="Gas"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_WaterBill" runat="server" ImageUrl="../Iconss/2933961.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label21" runat="server"
    Text="Water Bill"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:100px;" class="style1"><center>
 <asp:ImageButton ID="imgbtn_LandLine" runat="server" 
        ImageUrl="../Iconss/landline.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label22" runat="server"
    Text="LandLine"></asp:Label>
</center>

</td>

</tr>
<tr><td colspan="4">&nbsp;</td></tr>
</table>
       
       
       </div>
       </center>









   <center>

   <div class='form-section section-step'>

<table style="margin-left:10px;">
<asp:Label ID="Label23" runat="server" Font-Size="Large" 
        Text="More Services"  ForeColor="Blue"
       ></asp:Label>
       <tr><td colspan="4">&nbsp;</td></tr>
       <tr id="tr_Retailer_1" runat="server"  >
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_AEPS" runat="server" ImageUrl="../Iconss/click.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label24" runat="server"
    Text="AEPS"></asp:Label>
</center>
</td>

<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_MicroATM" runat="server" ImageUrl="../Iconss/atm.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label251" runat="server"
    Text="Micro ATM"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_PanCoupans" runat="server" ImageUrl="../Iconss/membership.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label262" runat="server"
    Text="Pan Coupans "></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_GSTRegistration" runat="server" ImageUrl="../Iconss/taxes_3.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label273" runat="server"
    Text="GST Registration "></asp:Label>
</center>
</td>
</tr>



<tr  id="tr_Retailer_2" runat="server"   style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>


<tr id="tr_Retailer_3" runat="server" ><td colspan="4">&nbsp;</td></tr>
<tr id="tr_Customer_1" runat="server" >
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_Flights" runat="server" 
        ImageUrl="../Iconss/travel3.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label289" runat="server"
    Text="Flight"></asp:Label>
</center>
</td>

<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Bus" runat="server" ImageUrl="../Iconss/bus.png" 
        Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label2912" runat="server"
    Text="Bus"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Trains" runat="server" ImageUrl="../Iconss/train2.png" 
        Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label30" runat="server"
    Text="Train"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Hotels" runat="server" ImageUrl="../Iconss/hotel1.png" 
        Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label31" runat="server"
    Text="Hotel"></asp:Label>
</center>
</td>
</tr>

<tr  id="tr_Customer_2" runat="server" style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr  id="tr_Customer_3" runat="server" ><td colspan="4">&nbsp;</td></tr>
<tr  id="tr_Customer_4" runat="server">
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_Fastag" runat="server" 
        ImageUrl="../Iconss/fasttag3.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label32" runat="server"
    Text="Fastag"></asp:Label>
</center>
</td>

<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Holiday" runat="server" ImageUrl="../Iconss/beach.png" 
        Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label33" runat="server"
    Text="Holiday"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Event" runat="server" 
        ImageUrl="../Iconss/entertainment.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label34" runat="server"
    Text="Event"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Insurance" runat="server" 
        ImageUrl="../Iconss/insurance.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label35" runat="server"
    Text="Insurance"></asp:Label>
</center>
</td>
</tr>

<tr  id="tr_Customer_5" runat="server" style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr  id="tr_Customer_6" runat="server"><td colspan="4">&nbsp;</td></tr>


<tr  id="tr_Customer_7" runat="server" style="margin-bottom:150px;">
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_Loan" runat="server" ImageUrl="../Iconss/loan.png" 
        Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label36" runat="server"
    Text="Loan"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
    <center>
<asp:ImageButton ID="imgbtn_WalletTransfer" Visible="false"  runat="server" ImageUrl="../Iconss/wallet_1.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label37" runat="server"  Visible="false" 
    Text="Wallet Transfer"></asp:Label>
</center>
</td>

<td style="margin-left:100px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_MoveToBank"  Visible="false"  runat="server" ImageUrl="../Iconss/banks.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label38" runat="server"  Visible="false" 
    Text=" Move To Bank"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_MoveToBankHistory"  Visible="false"  runat="server" ImageUrl="../Iconss/bill.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label39" runat="server"  Visible="false" 
    Text="Move To Bank History"></asp:Label>

</center>
</td>


</tr>
<%--<tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>--%>
<tr><td colspan="4">&nbsp;</td></tr>

<tr style="margin-bottom:150px; display:none;">
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
 <center>
 <asp:ImageButton ID="imgbtn_AEPSHistory" runat="server" ImageUrl="../Iconss/online-payment_1.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label40" runat="server"
    Text="AEPS History"></asp:Label>
 </center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_GSTHistory" runat="server" ImageUrl="../Iconss/transfer_1.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label41" runat="server"
    Text="GST History"></asp:Label>
</center>

</td>

 
</tr>
<tr><td colspan="4">&nbsp;</td></tr>

</table>
</div> 

</center>



<center>
       <div class='form-section section-step'  id="tr_Customer_Mall" runat="server" style="background:linear-gradient(to bottom right, white,orange 80%);">

 
      
      <table style="margin-left:10px;">
      <tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td colspan="4" style="margin-left:100px;">
<center>
<asp:ImageButton ID="imgbtn_Store" runat="server" ImageUrl="../Iconss/bank.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label42" runat="server"  Font-Bold="true"
    Text="BOS SHOPPING CENTER"></asp:Label>
</center>
</td>

</tr>


<tr><td colspan="4">&nbsp;</td></tr>
</table>
       
       
       </div>
       </center>



</div>

<%--Div Mobile View - End--%>





</div>
</div>
<center>
<div class="row" runat="server" id="Div_CustAPI" style="margin-top:20px;width:97%;">

<iframe  width="100%" height="2100px" scrolling="no" seamless="yes"  src="../inside.html">
</iframe>
</div>
  </center>
  <%--<frameset border="0" rows="100%," cols="100%"
			frameborder="no"><frame name="TopFrame"
			scrolling="yes" noresize src="https://www.google.com/"><frame name="BottomFrame" scrolling="no"
			noresize><noframes></noframes></frameset>--%>
</div>

<%--<script type="text/javascript">
    $(document).ready(function () {

        var isMobile = false;

//        debugger;
//        if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
//        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
//            document.getElementById('<%= lblIsMobileView.ClientID %>').value = "Yes";
//            // document.getElementById('<%= lblIsMobileView.ClientID %>').text = "Yes";
//       
//            }
//        else {
//             document.getElementById('<%= lblIsMobileView.ClientID %>').value = "No";
//            // document.getElementById('<%= lblIsMobileView.ClientID %>').text = "No";

//        }

    });

</script>--%>

<asp:Button ID="Button10" runat="server" Text="Button" style="display:none;" />
        <asp:Panel ID="PnlHomeDetails" runat="server"  style=" width:80%;display:none;" >
        <div class="table_head" style=" background-color:#3060ad; color:#FFFFFF;">
                     <div class="row"  >
                        <div class="col-sm-12" align="center">
                        &nbsp;&nbsp;<asp:Label ID="lblHomeInfo1" runat="server" Font-Bold="True" Text="Notification Window"></asp:Label> 
                         <asp:Button ID="btnCancel" runat="server" 
                BorderStyle="None" CausesValidation="False" Font-Bold="True" 
                ForeColor="White" Text="X" width="30px" BackColor="Red" 
                Font-Size="20px"  CssClass="pull-right"/>
                        </div>
                     </div>
                     <div class="row" >
                   <div class="col-sm-12" >
                    <asp:Label ID="Label13" runat="server" Font-Bold="True" 
                    ForeColor="#CC0000"></asp:Label>
                    </div>
                   </div>
                      
                        
                        </div>


                                   
        <div class="table-responsive"   style="background-color:White;max-height:400px; overflow:auto;">
                    <div class="table_wid">
                       <div class="row" style="width:95%">
                        <asp:Label ID="lblHomeInfo" runat="server" Font-Bold="True"></asp:Label> 
                 <div class="col-sm-12" runat="server"  id="div_grd" >
                <center>
                <asp:Image ID="imgNotificationPic" runat="server"  Width="90%" ImageAlign="Middle" AlternateText="Notification Image" />
                 </center> 


                    </div>
                    </div>
                    </div>
                    </div>


    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"  TargetControlID="Button10"
    PopupControlID="PnlHomeDetails"
    CancelControlID="btncancel"
    BackgroundCssClass="modalBackground" >
    </asp:ModalPopupExtender> 

    <asp:Button ID="bre" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="bre" PopupControlID="InformationPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelInfo" >
</asp:ModalPopupExtender>
<asp:Panel ID="InformationPopup" runat="server" Width="350px"  style="display:none;"  >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>Information Dialog</strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" >
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblInformation" runat="server" ></asp:Label>
        </td>
    </tr>
  
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnCancelInfo" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>

</asp:Content>
