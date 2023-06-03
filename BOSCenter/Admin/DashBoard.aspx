<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="DashBoard.aspx.vb" Inherits="BOSCenter.DashBoard" %>
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
	 
	 .form-section
	 {
		 border-radius:5px;
	 }
	 
	 td
	 {
		 font-size:14px;
		
font-family: 'Pavanam', sans-serif;
	 }

tr:hover
{
	background:#f1f1f1;
}

tr:first-child
{
	    background: #ededed;
    box-shadow: 1px 2px 1px #dedede;
}





@media screen 
  and (min-device-width: 1200px) 
  and (max-device-width: 1600px) 
  and (-webkit-min-device-pixel-ratio: 1) { 
  
  .padd
  {
	  padding:0 140px 0 30px;
  }
  
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">

<div class="col-sm-offset-1 col-md-10">

<div class="col-sm-4">

<div class="form-section" runat="server" id="Section1" >
<div class="form-section-head-A">
::   Registration Details ::
</div>

<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">
<div class="row">


         <div class="col-md-12">              
           <div style="width:100%;overflow:auto;">
         

<table style="width:100%;">
     <tr>
     <td style="padding:5px;" ><b></b></td>
     <td style="padding:5px;" ><b>Total</b></td>
     <td style="padding:5px;" ><b>Today</b></td>
     </tr>
    <asp:ListView ID="listViewRegDetails" runat="server">
    <ItemTemplate>
    
    <tr>
     <td style="padding:5px;" >Service Call</td>
       <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTotalserviceCall" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTodayserviceCall" runat="server" Text="0"></asp:LinkButton></td>
   </tr>
     
     <tr>
     <td style="padding:5px;" >AMC</td>
      <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTotalAMC" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTodayAMC" runat="server" Text="0"></asp:LinkButton></td>
     </tr>
     
     <tr>
     <td style="padding:5px;" >Warranty</td>
      <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTotalWarranty" runat="server" Text="0"></asp:LinkButton></td>
           
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTodayWarranty" runat="server" Text="0"></asp:LinkButton></td>
     </tr>

       </ItemTemplate>
    </asp:ListView>
     </table>
          
     
     </div> 
     </div> 
     
         

</div>

</div>
</div>
</div>

</div>
</div>

<div class="col-sm-7">

<div class="form-section" runat="server" id="Div1" >
<div class="form-section-head-B">
::AMC/Warranty Expiry Details ::
</div>

<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">
<div class="row">


         <div class="col-md-12">              
           <div style="width:100%;overflow:auto;">
         

<table style="width:100%;">
     <tr>
     <td style="padding:5px;" ><b></b></td>
     <td style="padding:5px;" ><b>Today</b></td>
     <td style="padding:5px;" ><b>LastDay</b></td>
     <td style="padding:5px;" ><b>Next Day</b></td>
     <td style="padding:5px;" ><b>Next 10 Days</b></td>
     </tr>
    <asp:ListView ID="listviewExpiryDetails" runat="server">
   <ItemTemplate>
    <tr>
     <td style="padding:5px;" >AMC</td>
      <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTodayExpiryAMC" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnLastDayExpiryAMC" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnNextDayExpiryAMC" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnNextTenDayExpiryAMC" runat="server" Text="0"></asp:LinkButton></td>
     </tr>
     
   <tr>
     <td style="padding:5px;" >Warranty</td>
      <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnTodayExpiryWarranty" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnLastDayExpiryWarranty" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnNextDayExpiryWarranty" runat="server" Text="0"></asp:LinkButton></td>
     <td style="padding:5px;" ><asp:LinkButton ID="lnkbtnNextTenDayExpiryWarranty" runat="server" Text="0"></asp:LinkButton></td>
     </tr>
     s
   </ItemTemplate>
 </asp:ListView>
     
 
    
     </table>
          
     
     </div> 
     </div> 
     
         

</div>

</div>
</div>
</div>

</div>


</div>
</div>






</div>


</asp:Content>
