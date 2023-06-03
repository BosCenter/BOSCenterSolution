<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="MobileD.aspx.vb" Inherits="BOSCenter.MobileD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {

        var isMobile = false; //initiate as false

        //debugger;
        if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {

            $("div#divweb").hide();
            $("div#divmobile").show();
        }
        else {
            $("div#divmobile").hide();
            $("div#divweb").show();
        }

    });

</script>


<div class="container" style="width:90%;"  >
<div class="row" id="divmobile" style="display:none;">

         <div  style="margin-bottom:50px;"></div>


         <center>
       <div class='form-section section-step'>

     
 <asp:Label ID="Label25" runat="server" Font-Size="Large" Text="Wallet Bal" ForeColor="Blue" style="text-align:left;" Visible="false"
       ></asp:Label>
      
      <table style="margin-left:10px;">
      <tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td style="margin-left:15px ;" class="style1">
<center>
 <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Iconss/wallet1.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label26" runat="server"
    Text="Wallet Pay"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../Iconss/money-transfer.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label27" runat="server"
    Text="Money Transfer"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="../Iconss/wallet4.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label28" runat="server"
    Text="Add Wallet"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../Iconss/user_1.png" Height="50px" Width="50px" /><br /><br />
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

     
 <asp:Label ID="Label3" runat="server" Font-Size="Large" Text="Recharge & Bill Payment" ForeColor="Blue" style="text-align:left;"
       ></asp:Label>
       
       <table style="margin-left:10px;">
       <tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td style="margin-left:15px ;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Recharge" runat="server" ImageUrl="../Iconss/mobile-recharge.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label10" runat="server"
    Text="Recharge"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
<asp:ImageButton ID="imgbtn_PostPaid" runat="server" ImageUrl="../Iconss/payment-method.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label9" runat="server"
    Text="PostPaid"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_DTH" runat="server" ImageUrl="../Iconss/DTH.jpg" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label8" runat="server"
    Text="DTH"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Broadband" runat="server" ImageUrl="../Iconss/wifi.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label7" runat="server"
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

<asp:Label ID="Label6" runat="server"
    Text="Electricity"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Gas" runat="server" ImageUrl="../Iconss/gas.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label5" runat="server"
    Text="Gas"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_WaterBill" runat="server" ImageUrl="../Iconss/2933961.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label11" runat="server"
    Text="Water Bill"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:100px;" class="style1"><center>
 <asp:ImageButton ID="imgbtnLandLine" runat="server" ImageUrl="../Iconss/landline.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label4" runat="server"
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
<asp:Label ID="Label12" runat="server" Font-Size="Large" 
        Text="More Services"  ForeColor="Blue"
       ></asp:Label>
       <tr><td colspan="4">&nbsp;</td></tr>
       <tr>
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_AEPS" runat="server" ImageUrl="../Iconss/click.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label13" runat="server"
    Text="AEPS"></asp:Label>
</center>
</td>

<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_MicroATM" runat="server" ImageUrl="../Iconss/atm.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label15" runat="server"
    Text="Micro ATM"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_PanCoupans" runat="server" ImageUrl="../Iconss/membership.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label17" runat="server"
    Text="Pan Coupans "></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_GSTRegistration" runat="server" ImageUrl="../Iconss/taxes_3.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label19" runat="server"
    Text="GST Registration "></asp:Label>
</center>
</td>
</tr>



<tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr><td colspan="4">&nbsp;</td></tr>
<tr>
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="../Iconss/travel3.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label14" runat="server"
    Text="Flight"></asp:Label>
</center>
</td>

<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
 <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="../Iconss/bus.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label16" runat="server"
    Text="Bus"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="../Iconss/train2.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label18" runat="server"
    Text="Train"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="../Iconss/hotel1.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label20" runat="server"
    Text="Hotel"></asp:Label>
</center>
</td>
</tr>

<tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr><td colspan="4">&nbsp;</td></tr>
<tr>
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="../Iconss/fasttag3.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label21" runat="server"
    Text="Fastag"></asp:Label>
</center>
</td>

<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
 <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="../Iconss/beach.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label22" runat="server"
    Text="Holiday"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="../Iconss/entertainment.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label23" runat="server"
    Text="Event"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
 <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../Iconss/insurance.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label24" runat="server"
    Text="Insurance"></asp:Label>
</center>
</td>
</tr>

<tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr><td colspan="4">&nbsp;</td></tr>


<tr style="margin-bottom:150px;">
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
<center>
<asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="../Iconss/loan.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label30" runat="server"
    Text="Loan"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
    <center>
<asp:ImageButton ID="imgbtn_WalletTransfer" runat="server" ImageUrl="../Iconss/wallet_1.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label35" runat="server"
    Text="Wallet Transfer"></asp:Label>
</center>
</td>

<td style="margin-left:100px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_MoveToBank" runat="server" ImageUrl="../Iconss/banks.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label31" runat="server"
    Text=" Move To Bank"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_MoveToBankHistory" runat="server" ImageUrl="../Iconss/bill.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label32" runat="server"
    Text="Move To Bank History"></asp:Label>

</center>
</td>


</tr>
<tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr><td colspan="4">&nbsp;</td></tr>

<tr style="margin-bottom:150px;">
<td style="margin-left:15px;margin-bottom:50px;" class="style1">
 <center>
 <asp:ImageButton ID="imgbtn_AEPSHistory" runat="server" ImageUrl="../Iconss/online-payment_1.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label34" runat="server"
    Text="AEPS History"></asp:Label>
 </center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_GSTHistory" runat="server" ImageUrl="../Iconss/transfer_1.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label2" runat="server"
    Text="GST History"></asp:Label>
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
      <tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_Store" runat="server" ImageUrl="../Iconss/bank.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label33" runat="server"
    Text="BOS MALL"></asp:Label>
</center>
</td>

</tr>


<tr><td colspan="4">&nbsp;</td></tr>
</table>
       
       
       </div>
       </center>



</div>




<div class="row" id="divweb" style="display:none;"  >
<div style="margin-bottom:100px" ></div>
<div class="col-sm-8 col-sm-offset-2" ><center>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="&nbsp;" Visible="false"
        ForeColor="Blue"></asp:Label></center>
</div>
</div>

</div> 


</asp:Content>

