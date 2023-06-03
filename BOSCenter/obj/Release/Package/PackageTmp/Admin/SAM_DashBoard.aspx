<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="SAM_DashBoard.aspx.vb" Inherits="BOSCenter.SAM_DashBoard" %>

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

     <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        .wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        .loader {
            position: relative;
            width: 35px;
            height: 35px;
        }

            .loader span {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                transform: rotate(calc(18deg * var(--i)));
            }

                .loader span::before {
                    content: " ";
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 4px;
                    height: 4px;
                    background: black;
                    border-radius: 50%;
                    animation: animate 2s linear infinite;
                    animation-delay: calc(0.1s * var(--i));
                }

        #hiperlink:active {
            box-shadow: 0 0 20px rgba(112, 171, 185, 0.8);
            border: none;
            outline: none;
            transition: 0.5s ease;
        }

        @keyframes animate {
            0% {
                transform: scale(0);
            }

            10% {
                transform: scale(1.2);
            }

            80%,100% {
                transform: scale(0);
            }
        }
    </style>

    <style>
        @media screen and (max-width: 576px){
            #containerformargin{
                position:relative;
                bottom:33px;
            }
        }
    </style>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <%--Div Mobile View - Start--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" id="containerformargin">
 <div class="row" runat="server" Id="Div_Slider" visible="false" >      
        <center style="margin-top: 5px;">
            <div class="container" style="width: 100%; display: flex; flex-direction: column; gap: 6px; background-color: white; 
               margin-top: 5px; border-radius: 8px; padding: 20px; margin:0; padding: 0; box-sizing: border-box;">
                <center>
                <div class="row" style="width: 98%; border-radius: 8px; margin-top: 5px;">
               <div class="col-md-12">
                   <img src="#" class="img-fluid image-resized" name="slidermainpage" style="border-radius: 15px; height: 100%; width: 100%;"/>
                   <asp:Image ID="SliderImage_1" runat="server" style="border-radius: 15px; height: 100%; width: 100%;" Visible="false"  />
                   <asp:Image ID="SliderImage_2" runat="server" style="border-radius: 15px; height: 100%; width: 100%;"  Visible="false"  />
                   <asp:Image ID="SliderImage_3" runat="server" style="border-radius: 15px; height: 100%; width: 100%;"  Visible="false"   />
               </div>
        </div>
                    </center>
                </div>
            
            </center>
 
    </div>
<div class="row" runat="server" id="Div_API" style="margin-top:20px;">
<div class="col-md-12">
<div class="row" id="div_AD_MD_DIS" runat="server" >
<div class="col-md-3" runat="server" id="div_API_Balance" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);padding-bottom:22px;">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;Load Balance</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-mobile" style="color:white;font-size:50px;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12 mar_lft30 ">
    &nbsp;&nbsp;<asp:Label ID="lbl_API_Balance" runat="server" style="font-weight:bold;font-size:large" Visible="True" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12" style="margin-top:15px;">
    <asp:LinkButton ID="btnAPIBalanceRefresh_N" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_lft30 "  Text=""><i class="fa fa-refresh"> </i> </asp:LinkButton> &nbsp; &nbsp; &nbsp; &nbsp; <asp:LinkButton ID="btnAPIBalanceReport" runat="server" CausesValidation="False" CssClass="btn btn-danger"  Text=""><i class="fa fa-list"> </i> </asp:LinkButton></div>
    </div>

</div>
</div>
</div>

</div>
<div class="col-md-3" runat="server" id="div_md_ds_Load" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);padding-bottom:23px;">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;Load </h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-mobile" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12 mar_lft30 " style="font-weight:bold;font-size:large">
    D : &nbsp;&nbsp;<asp:Label ID="lbl_md_ds_D_Load" runat="server"  Visible="True" Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12 mar_lft30" style="font-weight:bold;font-size:large;margin-top:15px;">
    M : &nbsp;&nbsp;<asp:Label ID="lbl_md_ds_M_Load" runat="server"  Visible="True" Text="0"></asp:Label>
    </div>
    </div>

</div>
</div>
</div>

</div>

<div class="col-md-3" runat="server" id="div_md_ds_Networth" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow); padding-bottom:23px;">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;Net Worth</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-mobile" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12 mar_lft30 ">
    &nbsp;&nbsp;<asp:Label ID="lbl_Md_DS_NetWortdd" runat="server" 
            style="font-weight:bold;font-size:large" Text="5454"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12" style="margin-top:20px;">
    <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_lft30 " Visible="false"   Text=""><i class="fa fa-refresh"> </i> </asp:LinkButton> &nbsp; &nbsp; &nbsp; &nbsp; <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="False" CssClass="btn btn-danger"  Visible="false"  Text=""><i class="fa fa-list"> </i> </asp:LinkButton>
    </div>
    </div>

</div>
</div>
</div>

</div>

<div class="col-md-3" runat="server" id="div_md_ds_WalletTransfer" >
<div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);padding-bottom:23px;">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;<asp:Label ID="lblWalletTransferName" runat="server" Text="Wallet Transfer"></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class="fa fa-mobile" style="color:white;font-size:50;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12 mar_lft30 ">
    &nbsp;&nbsp;<asp:LinkButton ID="lnkWalletTransfer" runat="server" CausesValidation="False" CssClass="btn btn-danger"  Text="Proceed"> </asp:LinkButton> <asp:Label ID="Label4" runat="server" style="font-weight:bold;font-size:large" Visible="false"  Text="0"></asp:Label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12 mar_top10">
    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_lft30 "  Visible="false"  Text=""><i class="fa fa-refresh"> </i> </asp:LinkButton> &nbsp; &nbsp; &nbsp; &nbsp; </div>
    </div>

</div>
</div>
</div>

</div>

<div class="col-md-3" runat="server" id="div_API_Balance1" visible="false" >
        <div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;Load API Balance</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-users" style="color:white;font-size:30px;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;
 <%--<asp:Label ID="Label6" runat="server" style="font-weight:bold;font-size:large" Visible="True" Text="0"></asp:Label>--%>
        
        <asp:Button ID="btnPayment_Details" runat="server" Text="Payment Details" cssclass="btn btn-primary"/>
    
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12" style="margin-top:15px;">
<%--    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_lft30 "  Text="">
        <i class="fa fa-refresh"> </i> </asp:LinkButton> &nbsp; &nbsp; &nbsp; &nbsp; 
         <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-danger"  Text="">
             <i class="fa fa-list"> </i> </asp:LinkButton>--%>
         <div style="text-align:center;">
             <asp:Button ID="btnReport" runat="server" Text="Report"  cssclass="btn btn-primary"/>
         </div>
         
     </div>
    </div>

</div>
</div>
</div>

    </div>
<div class="col-md-3" runat="server" id="div_API_Log_Report" visible="false" >
        <div class="form-section sa" style="color: White; margin-left: 4px; background: linear-gradient(to bottom right, green, yellow); padding-bottom:22px;">
            <div class="row">
                <div class="col-md-12">
                    <h4>&nbsp;API Log Report</h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    &nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-book" style="color: white; font-size:30px;"></i>
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-12 mar_lft30 ">
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12" style="margin-top: 15px;">
                            <%--    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_lft30 "  Text="">
        <i class="fa fa-refresh"> </i> </asp:LinkButton> &nbsp; &nbsp; &nbsp; &nbsp; 
         <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-danger"  Text="">
             <i class="fa fa-list"> </i> </asp:LinkButton>--%>
                        </div>
                    </div>
                    &nbsp;
                    <br />
                </div>
            </div>
        </div>

    </div>
<div class="col-md-3" runat="server"  id ="div_API_support" visible="false" >
        <div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
<div class="row">
<div class="col-md-12">
<h4 >&nbsp;API Support</h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-credit-card" style="color:white;font-size:30px;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">

  <%--<asp:Label ID="Label10" runat="server" style="font-weight:bold;font-size:large" Visible="True" Text="0">

                </asp:Label>--%>
        <asp:Button ID="btnRaise" runat="server" Text="Raise"  cssclass="btn btn-primary"/>
        <asp:Button ID="btnPending" runat="server" Text="Pending"  cssclass="btn btn-primary"/>
        
    </div>
    
    </div>
   
    <div class="row">
     <div class="col-md-12" style="margin-top:15px;">
    <%--<asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_lft30 "  Text="">
        <i class="fa fa-refresh"> </i> </asp:LinkButton> &nbsp; &nbsp; &nbsp; &nbsp; 
         <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="False" CssClass="btn btn-danger"  Text="">
             <i class="fa fa-list"> </i> </asp:LinkButton></div>--%>
         <div style="text-align:center;">
         <asp:Button ID="btnClose" runat="server" Text="Close"  cssclass="btn btn-danger"/>
             </div>
    </div>

</div>
</div>
</div>

    </div>
</div>

<div class="col-md-12" runat="server"  id ="div_DMT_AEPS_Recha_Report" visible="false"  >
   <div class="form-section sa" style="color:White;margin-left:4px;background: linear-gradient(to bottom right, green, yellow);">
       <div class="row">
        <div class="col-md-12">
             <h4>&nbsp;Report</h4>  
       </div>
       </div>       
<div class="row">
    <div class="col-md-1">
&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-book" style="color:white;font-size:30px;"></i>
    </div>
    <div class="col-md-11">
        <div class="row">
            <div class="col-md-4">
       <div class="form-section">
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa" style="color:white;font-size:30px;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
        <label style="font-size:20px;">Recharge</label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="row">
         Successfull:<asp:Label  runat="server" Text="0"></asp:Label>
     </div>
        <div class="row">
               Fail:<asp:Label  runat="server" Text="0"></asp:Label>
     </div>
        <div class="row">
               Pending:<asp:Label  runat="server" Text="0"></asp:Label>
     </div>
        </div>
</div>
</div>
       </div>

</div>
    <div class="col-md-4">
        <div class="form-section">
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa" style="color:white;font-size:30px;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
        <label style="font-size:20px;">DMT</label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="row">
         Successfull:<asp:Label  runat="server" Text="0"></asp:Label>
     </div>
        <div class="row">
               Fail:<asp:Label runat="server" Text="0"></asp:Label>
     </div>
        <div class="row">
               Pending:<asp:Label runat="server" Text="0"></asp:Label>
     </div>
        </div>
</div>
</div>
        </div>
</div>
    <div class="col-md-4">
        <div class="form-section">
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa" style="color:white;font-size:30px;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
        <label style="font-size:20px;">AEPS</label>
    </div>
    
    </div>
   
    <div class="row">
     <div class="row">
         Successfull:<asp:Label  runat="server" Text="0"></asp:Label>
     </div>
        <div class="row">
               Fail:<asp:Label  runat="server" Text="0"></asp:Label>
     </div>
        <div class="row">
               Pending:<asp:Label  runat="server" Text="0"></asp:Label>
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




    </div>   
</div> 

    
<%--<i class='fa fa-credit-card' style="color:white;font-size:50"></i>--%>

<div id="div_cust_dd" runat="server">
<div class="form-section">
<div class="row">



    <asp:ListView ID="ListView1" runat="server">
    <ItemTemplate>
    <div class="col-md-3" runat="server"   id="div_API_HotelBooking" >
<div class="form-section sa"  style="height:120px;color:White;border-radius:15px;margin-left:4px;
background: linear-gradient(rgba(4,139,225),rgba(131,201,228));">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;<asp:Label ID="lblServiceName" runat="server" Text='<%# Eval("ServiceName") %>' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<i class='<%# Eval("IconName") %>' style="color: white;font-size: 50px; position: absolute; top: 10px;"></i>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12">
    &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" style="font-weight:bold" Text="0" Visible="false"></asp:Label>
    </div>
    </div>
   
    <div class="row">
    <div class="col-md-12" style="display: flex; align-items: center; justify-content: center;">
    <asp:Button ID="btnPostbackUrl" CssClass="btn" runat="server" Text="Proceed" style="box-shadow: 0 0 3px rgba(0,0,0,0.6); 
            text-align: center;  background: deepskyblue;"/></div>
    </div>

</div>
</div>
</div>  
</div>
    </ItemTemplate>

    </asp:ListView>
</div>
</div>
</div>
<div id="div_RET" runat="server">
<div class="form-section">
<h3>Today</h3>
<div class="row" id="div2" runat="server"  >

<div class="col-md-3" runat="server"  >
<div class="form-section sa"  style="height:120px; color:White; border-radius: 10px; margin-left:4px;
background: linear-gradient(to bottom right, brown,orange 50%); padding: 8px;">
<div class="row">
<div class="col-md-12">
<h4  >&nbsp;<asp:Label ID="lblServiceName" runat="server"  Text='LOAD' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--Div Mobile View - Start--%>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Today_Load" runat="server" Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>
<div  class="col-md-3" runat="server"  >
<div class="form-section sa"  style="height:120px; color:White; border-radius: 10px; margin-left:4px;
  background: linear-gradient(to bottom right, brown,orange 50%); padding: 8px;"">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;<asp:Label ID="Label2" runat="server" Text='USED' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--<td style="margin-left:100px;" class="style1">
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
--%>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Today_Used" runat="server"  Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>
<div  class="col-md-3" runat="server"  >
<div class="form-section sa" style="height:120px; color:White; border-radius: 10px; margin-left:4px;
 background: linear-gradient(to bottom right, brown,orange 50%);padding: 8px;">
<div class="row">
<div class="col-md-12" >
<h4>&nbsp;<asp:Label ID="Label5" runat="server" Text='PENDING' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--<tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>--%>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Today_Pending" runat="server"  Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>
<div  class="col-md-3" runat="server"  >
<div class="form-section sa"  style="height:120px; color:White; border-radius: 10px; margin-left:4px;
 background: linear-gradient(to bottom right, brown,orange 50%); padding: 8px;">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;<asp:Label ID="Label7" runat="server" Text='EARNED' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--Div Mobile View - End--%>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Today_Earned" runat="server"  Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>


</div>



</div>

<div class="form-section" runat="server" id="Div_Month" visible="false" >
<h3>Month</h3>
<div class="row" id="div1" runat="server"  >


<div id="Div3" class="col-md-3" runat="server"  >
<div class="form-section sa"  style="height:120px; color:White; border-radius: 10px; margin-left:4px;
  background: linear-gradient(to bottom right, brown,orange 50%); padding: 8px;">
<div class="row">
<div class="col-md-12">
<h4  >&nbsp;<asp:Label ID="Label9" runat="server"  Text='LOAD' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--<script type="text/javascript">
    $(document).ready(function () {

        var isMobile = false;

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
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Monthly_Load" runat="server" Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>
<div id="Div4"  class="col-md-3" runat="server"  >
<div class="form-section sa" style="height:120px; color:White; border-radius: 10px; margin-left:4px;
 background: linear-gradient(to bottom right, brown,orange 50%); padding: 8px;">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;<asp:Label ID="Label11" runat="server" Text='USED' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--<i class='fa fa-credit-card' style="color:white;font-size:50"></i>--%>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Monthly_Used" runat="server"  Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>
<div id="Div5"  class="col-md-3" runat="server"  >
<div class="form-section sa" style="height:120px; color:White; border-radius: 10px; margin-left:4px;
  background: linear-gradient(to bottom right, brown,orange 50%); padding: 8px;">
<div class="row">
<div class="col-md-12" >
<h4>&nbsp;<asp:Label ID="Label38" runat="server" Text='PENDING' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--<i class='fa fa-credit-card' style="color:white;font-size:50"></i>--%>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Monthly_Pending" runat="server"  Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>
<div id="Div6"  class="col-md-3" runat="server"  >
<div class="form-section sa" style="height:120px; color:White; border-radius: 10px; margin-left:4px;
  background: linear-gradient(to bottom right, brown,orange 50%); padding: 8px;">
<div class="row">
<div class="col-md-12">
<h4>&nbsp;<asp:Label ID="Label43" runat="server" Text='EARNED' ></asp:Label></h4>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;&nbsp;<%--<i class='fa fa-credit-card' style="color:white;font-size:50"></i>--%>
</div>
<div class="col-md-8">
    <div class="row">
    <div class="col-md-12" style="font-weight:bold;font-size:x-large;">
    &nbsp; &#8377; &nbsp;<asp:Label ID="lbl_Monthly_Earned" runat="server"  Text="0" ></asp:Label>
    </div>
    </div>

</div>
</div>
</div>  
</div>



</div>



</div>

</div>



<%--Div Mobile View - Start--%>


<div class="row" id="divmobile" runat="server" visible="false" >

<center>
       <div class='form-section section-step' style="background-color:#EADFC1">

     
 <asp:Label ID="Label25" runat="server" Font-Size="Large" Text="Wallet Bal" ForeColor="Blue" style="text-align:left;" Visible="false"
       ></asp:Label>
      
      <table style="margin-left:10px">
      <tr><td colspan="4">&nbsp;</td></tr>
<tr style="margin-bottom:150px;">
<td style="margin-left:15px ;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_WalletPay" runat="server" ImageUrl="../Iconss/wallet1.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label26" runat="server"
    Text="Wallet Pay&nbsp;&nbsp;"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
<asp:ImageButton ID="imgbtn_LoadWallet1" runat="server" 
        ImageUrl="../Iconss/wallet3.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label1" runat="server"
    Text="Load Wallet"></asp:Label>
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
    Text="My Account&nbsp;&nbsp;"></asp:Label>
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
 <asp:ImageButton ID="imgbtn_Recharge" runat="server" ImageUrl="../Iconss/newOne/mob-rec.jpg" style="border-radius: 5px;"  Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label15" runat="server"
    Text="Recharge"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
<asp:ImageButton ID="imgbtn_PostPaid" runat="server" ImageUrl="../Iconss/newOne/postpaid.jpg" style="border-radius: 5px; position: relative; right: 4px;" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label16" runat="server"
    Text="PostPaid"></asp:Label>
</center>
</td>
<td style="margin-left:15px" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_DTH" runat="server" ImageUrl="../Iconss/newOne/dth.png" style="border-radius: 5px; position: relative; right: 6px;" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label17" runat="server"
    Text="DTH"></asp:Label>
</center>
</td>
<td style="margin-left:15px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Broadband" runat="server" ImageUrl="../Iconss/newOne/broadband.jpg" style="border-radius: 5px;" Height="50px" Width="50px" /><br/><br />
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
<asp:ImageButton ID="imgbtn_Electricity" runat="server" ImageUrl="../Iconss/newOne/ele.jpg" style="border-radius: 5px;" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label19" runat="server"
    Text="Electricity"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Gas" runat="server" ImageUrl="../Iconss/newOne/gas.png" style="border-radius: 5px;" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label20" runat="server"
    Text="Gas"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_WaterBill" runat="server" ImageUrl="../Iconss/newOne/water-bill.jpg" style="border-radius: 5px;" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label21" runat="server"
    Text="Water Bill"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:100px;" class="style1">
    <center>
 <asp:ImageButton ID="imgbtn_LandLine" runat="server" 
        ImageUrl="../Iconss/newOne/landline.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label22" runat="server"
    Text="LandLine"></asp:Label>
</center>

</td>

</tr>

           <tr style="border-bottom: 1px dashed gray;"><td colspan="4">&nbsp;</td></tr>
<tr><td colspan="4">&nbsp;</td></tr>

<tr style="margin-bottom:150px;">
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_EMI" runat="server" ImageUrl="../Iconss/newOne/emi.png" style="border-radius: 5px;" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label6" runat="server"
    Text="EMI"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
 <asp:ImageButton ID="imgbtn_Cable" runat="server" ImageUrl="../Iconss/newOne/cable.jpg" style="border-radius: 5px;" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label8" runat="server"
    Text="Cable"></asp:Label>
</center>
</td>
<td style="margin-left:100px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_Insurance" runat="server" ImageUrl="../Iconss/newOne/insurance.png" style="border-radius: 5px;" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label10" runat="server"
    Text="Insurance"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:100px;" class="style1">
    <center>
 <asp:ImageButton ID="imgbtn_Fastag" runat="server" 
        ImageUrl="../Iconss/newOne/fastag.png" Height="50px" style="border-radius: 5px;" Width="50px" /><br /><br />
<asp:Label ID="Label12" runat="server"
    Text="Fastag"></asp:Label>
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
 <asp:ImageButton ID="imgbtn_GSTRegistration" runat="server" ImageUrl="../Iconss/money-transfer.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label273" runat="server"
    Text="Money Transfer "></asp:Label>
</center>
</td>
</tr>



<tr  id="tr_Retailer_2" runat="server"   style="border-bottom: 1px dashed gray;">
    
    <%--<td colspan="4">&nbsp;</td>--%></tr>


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
<%--<center>
<asp:ImageButton ID="imgbtn_Fastag" runat="server" 
        ImageUrl="../Iconss/fasttag3.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label32" runat="server"
    Text="Fastag"></asp:Label>
</center>--%>
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
<%--<center>
 <asp:ImageButton ID="imgbtn_Insurance" runat="server" 
        ImageUrl="../Iconss/insurance.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label35" runat="server"
    Text="Insurance"></asp:Label>
</center>--%>
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
<asp:ImageButton ID="imgbtn_MoneyTransfer" runat="server" ImageUrl="../Iconss/money-transfer.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label27" runat="server"
    Text="Money Transfer"></asp:Label>
</center>
</td>
<td style="margin-left:100px;margin-bottom:20px;" class="style1">
<center>
<asp:ImageButton ID="imgbtn_Store" runat="server" ImageUrl="../Iconss/bank.png" Height="50px" Width="50px" /><br /><br />
<asp:Label ID="Label42" runat="server" 
    Text="Online Shopping"></asp:Label>
</center>
</td>

<td style="margin-left:100px;margin-bottom:20px;" class="style1">
    <center>
<asp:ImageButton ID="imgbtn_WalletTransfer" Visible="false"  runat="server" ImageUrl="../Iconss/wallet_1.png" Height="50px" Width="50px" /><br /><br />

<asp:Label ID="Label37" runat="server"  Visible="false" 
    Text="Wallet Transfer"></asp:Label>
</center>
</td>




<%--<td style="margin-left:100px;" class="style1">
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
--%>

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

</td>

</tr>


<tr><td colspan="4">&nbsp;</td></tr>
</table>
       
       
       </div>
       </center>
       
</div>


<%--Div Mobile View-Urjamitra - Start--%>
<div class="row" id="divmobile1" runat="server">

        <div class="row" style=" width: 80vw; display: flex; flex-direction: column; background-color: white; padding: 18px;" id="Retailer_1" runat="server" ></div>
          <center>
           <div class="menu-bar" style=" display: flex; align-items: flex-start; justify-content: space-between; margin-top: 50px; padding: 8px;">
                    <img src="../Images/Urjamitra/urja mitraaa.png" width="40px">
                    <img src="../Images/Urjamitra/menu.png" width="40px">
                </div>
          <div class="row" style=" display: flex; flex-direction: column; gap: 15px;">
                <div class="col-md-12">
                    <hr>
                    <h5 style="  margin: -14px;  text-align: center;  color: #0C1250;  font-size: 16px;  font-weight: 610;">Utility Services</h5>
                    <hr>
                </div>
            </div>

          <div class="row" style=" display: flex; align-items: center; justify-content: space-evenly;">
              <center>
                  <div class="col-md-3 show-more">
                
                     
                       <a href="BOS_BBPS_PS.aspx?type=Mobile"><img src="../Images/Urjamitra/recharge.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a><br /> 
                      <p style="  font-size: 13px; font-weight: 600;">Recharge</p>
              </div>
              </center>
              <center>
                     <div class="col-md-3 show-more">
                     
                         <a href="BOS_BBPS_PS.aspx?type=Electricity"><img src="../Images/Urjamitra/bill pay.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a><br /> 
                       <p style="  font-size: 13px; font-weight: 600;">Electricity Bill</p>
              </div>
              </center>
              <center>
                  <div class="col-md-3 show-more" >
                 
                      <a href="BOS_BBPS_PS.aspx?type=Postpaid"><img src="../Images/Urjamitra/MobileBill.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a><br /> 
                       <p style="  font-size: 13px; font-weight: 600;">Mobile Bill</p>
              </div>
              </center>
              <center>
                  <div class="col-md-3 show-more">
                       <a href="#"><img src="../Images/Urjamitra/Other.png" width="40px" style="position: relative;" class="loadMore"></a><br /> 
                      <p style="font-size: 13px; font-weight: 600;">Other</p>
                   
              </div>
              </center>    
          </div>
          
           <div class="row" style=" display: flex; align-items: center; justify-content: space-evenly;">
                     <center>
                <div class="col-md-3 show-more" style="display:none">
               <a href="../Admin/BOS_BBPS_PS.aspx?type=Broadband"><img src="../Images/Urjamitra/broadband.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a><br /> 
                <p style="font-size: 13px; font-weight: 600;">Broadband</p>
                </div>
                    </center>
                     <center>
                <div class="col-md-3 show-more" style="display:none">
                  <a href="../Admin/BOS_BBPS_PS.aspx?type=LPG"><img src="../Images/Urjamitra/gas.png" width="45px" style="position: relative;" onclick="clickloadingurja()"></a><br />   
                <p style="font-size: 13px; font-weight: 600;">LPG</p>
                </div>
                         </center>
                     <center>
                <div class="col-md-3 show-more" style="display:none">
                    <a href="../Admin/BOS_BBPS_PS.aspx?type=Landline"><img src="../Images/Urjamitra/landline.png" width="36px" style="position: relative;" onclick="clickloadingurja()"></a><br />  
                <p style="font-size: 13px; font-weight: 600;">Landline</p>
                </div>
                         </center>
                     <center>
                <div class="col-md-3 show-more" style="display:none">
                  <a href="../Admin/BOS_BBPS_PS.aspx?type=WaterBill">
                      <img src="../Images/Urjamitra/water-bill.png" width="45px" style="position: relative;" onclick="clickloadingurja()"></a>      
                <p style="font-size: 13px; font-weight: 600;">Water Bill</p>
                </div>
                     </center>
            </div>
              
           <div class="row" style=" display: flex; align-items: center; justify-content: space-evenly;" >
                   <center>
                  <div class="col-md-3 show-more" style="display:none">
                    <a href="../Admin/BOS_BBPS_PS.aspx?type=EMI"> <img src="../Images/Urjamitra/emi.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a> <br />
                   <p style="font-size: 13px; font-weight: 600;">EMI</p>
                   </div>
                       </center>
                   <center>
                  <div class="col-md-3 show-more" style="display:none">
                  <a href="../Admin/BOS_BBPS_PS.aspx?type=Municipality">  <img src="../Images/Urjamitra/municipality.png" width="35px" style="position: relative;" onclick="clickloadingurja()"></a><br />  
                <p style="font-size: 13px; font-weight: 600;">Municipality</p>
                   </div>
                       </center>
                   <center>
                  <div class="col-md-3 show-more" style="display:none">
                     <a href="../Admin/BOS_BBPS_PS.aspx?type=Cable"> <img src="../Images/Urjamitra/cable.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a><br /> 
                <p style="font-size: 13px; font-weight: 600;">Cable</p>
                   </div>
                     </center>
                   <center>
                  <div class="col-md-3 show-more" style="display:none">
                    <a href="../Admin/BOS_BBPS_PS.aspx?type=Insurance"><img src="../Images/Urjamitra/insurance.png" width="30px" style="position: relative;"></a><br />  
                <p style="font-size: 13px; font-weight: 600;">Insurance</p>
                   </div>
                   </center>
              </div>



          <div class="row" style=" display: flex; align-items: center; justify-content: space-evenly;">
                     <center>
                <div class="col-md-3" style="display:none">
               <a href="../Admin/BOS_BBPS_PS.aspx?type=Broadband"><img src="../Images/Urjamitra/broadband.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a> 
                <p style="font-size: 13px; position: relative; right: 5px;">Broadband</p>
                </div>
                    </center>
                     <center>
                <div class="col-md-3" style="display:none">
                  <a href="../Admin/BOS_BBPS_PS.aspx?type=LPG"><img src="../Images/Urjamitra/gas.png" width="50px" style="position: relative;" onclick="clickloadingurja()"></a>   
                <p style="font-size: 13px; position: relative;">LPG</p>
                </div>
                         </center>
                     <center>
                <div class="col-md-3" style="display:none">
                    <a href="../Admin/BOS_BBPS_PS.aspx?type=Landline"><img src="../Images/Urjamitra/landline.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a>  
                <p style="font-size: 13px; position: relative; top: 5px;">Landline</p>
                </div>
                         </center>
                     <center>
                <div class="col-md-3" style="display:none">
                  <a href="../Admin/BOS_BBPS_PS.aspx?type=WaterBill"><img src="../Images/Urjamitra/water-bill.png" width="60px" style="position: relative; top: 12px;" onclick="clickloadingurja()"></a>       
                <p style="font-size: 13px; position: relative; top: 5px;">Water<br>Bill</p>
                </div>
                     </center>
            </div>
              
           <div class="row" style=" display: flex; align-items: center; justify-content: space-evenly;" >
                   <center>
                  <div class="col-md-3" style="display:none">
                    <a href="../Admin/BOS_BBPS_PS.aspx?type=EMI"> <img src="../Images/Urjamitra/emi.png" width="40px" style="position: relative;" onclick="clickloadingurja()"></a> 
                   <p style="font-size: 13px; position: relative; top: 6px;">EMI</p>
                   </div>
                       </center>
                   <center>
                  <div class="col-md-3" style="display:none">
                  <a href="../Admin/BOS_BBPS_PS.aspx?type=Municipality">  <img src="../Images/Urjamitra/municipality.png" width="35px" style="position: relative; top: 10px;" onclick="clickloadingurja()"></a>  
                <p style="font-size: 13px; position: relative; top: 12px;">Municipality</p>
                   </div>
                       </center>
                   <center>
                  <div class="col-md-3" style="display:none">
                     <a href="../Admin/BOS_BBPS_PS.aspx?type=Cable"> <img src="../Images/Urjamitra/cable.png" width="40px" style="position: relative; top: 10px;" onclick="clickloadingurja()"></a> 
                <p style="font-size: 13px; position: relative; top: 10px;">Cable</p>
                   </div>
                     </center>
                   <center>
                  <div class="col-md-3" style="display:none">
                    <a href="../Admin/BOS_BBPS_PS.aspx?type=Insurance"><img src="../Images/Urjamitra/insurance.png" width="40px" style="position: relative; top: 10px;" onclick="clickloadingurja()"></a>  
                <p style="font-size: 13px; position: relative; top: 10px;">Insurance</p>
                   </div>
                   </center>
              </div>
              
             <div class="row" style=" display: flex; flex-direction: column; gap: 15px;">
                <div class="col-md-12">
                    <hr>
                    <h5 style="  margin: -14px;  text-align: center;  color: #0C1250;  font-size: 16px;  font-weight: 610;">Financial Services</h5>
                    <hr>
                </div>
            </div>
             <div class="row" style=" display: flex;  align-items: flex-start;  justify-content: space-evenly;">
                 <center>
                <div class="col-md-4">
                    
                     
                   <a href="../Admin/Urja_Dashboard2.aspx"> <img src="../Images/Urjamitra/bank.png" width="30px" onclick="clickloadingurja()"></a> <br />
                    <p style="  font-size: 13px; font-weight: 600;">Credit Card to<br> Bank Transfer</p> 
                </div>
                 </center>
                 <center>
                <div class="col-md-4">
                  
                 <a href="#"> <img src="../Images/Urjamitra/credit.png" width="30px" onclick="clickloadingurja()"> </a><br />  
                    <p style=" font-size: 13px; font-weight: 600;">Card to Card<br> Payment</p>
                </div>
                     </center>
                 <center>
                <div class="col-md-4">
                    
                  <img src="../Images/Urjamitra/bill pay.png" width="30px" onclick="clickOpenurja()">  <br />
                    <p style=" font-size: 13px; font-weight: 600;">Help For Cr.<br>
                        Card Bill Payment</p>
                </div>

                         <%---------------PopUp Start-------------------%>
                <div id="popup" style="width: 80%; height: 40vh; border-radius: 15px; z-index: 200; position:absolute; bottom: 20%; left: 10%; background: white; box-shadow: 0 0 5px rgba(0,0,0,0.6); visibility: hidden;">
                    <div class="col-md-12" style="text-align: end;">
                    <img onclick="clickOff()" src="../images/Ozzype/call on/cross.png" width="30px" style="box-shadow: 0 0 4px rgba(0,0,0,0.4); position: relative; top: 8px; right: 6px; border-radius: 5px; background-color: red;">
                        </div>
                    <br />
                    <h3 style="color: rgb   (12,18,80);"><b>Call On</b></h3>

                    <p style="color: rgb(12,18,80);">1800 309 2030</p>

                    <h3 style="color: rgb   (12,18,80);"><b>Mobile No</b></h3>

                     <p style="color: rgb(12,18,80);">+91 9312123000</p>
                </div>
                     <%---------------PopUp End-------------------%>

                     </center>
                </div>

            <div class="row" style="  display: flex;  flex-direction: column;  gap: 15px;">
                <div class="col-md-12">
                    <hr>
                    <h5 style="  margin: -14px;  text-align: center;  color: #0C1250;  font-size: 16px;  font-weight: 610;">Accounting Services</h5>
                    <hr>
                </div>
                </div>
            <div class="row" style=" display: flex; /*align-items: center;*/ justify-content: space-evenly;"> 
                <center>
                <div class="col-md-4">
                     <%--<asp:ImageButton ID="lnkbtn_Income" runat="server"  ImageUrl="../Images/Urjamitra/tax.png" width="30px" /><br />--%>
                          <a href="../Admin/Frm_IT_Return.aspx">
                          <img src="../Images/Urjamitra/tax.png" width="30px" onclick="clickloadingurja()"> </a> 
                           <p style=" font-size: 13px; font-weight: 600;">Income Tax<br /> Return</p>
                         
                      </div>
                </center>
                <center>
                <div class="col-md-4">
                    <%-- <asp:ImageButton ID="lnkbtn_GST" runat="server"  ImageUrl="../Images/Urjamitra/gst.png" width="30px" /><br />--%>
                   <a href="../Admin/Frm_Gst_Registration.aspx">
                       <img src="../Images/Urjamitra/gst.png" width="30px" onclick="clickloadingurja()"></a> 
                    <p style=" font-size: 13px; font-weight: 600;">GST Reg.&<br /> Returnt</p>
                </div>
                </center>
                <center>
                <div class="col-md-4">
                    <%-- <asp:ImageButton ID="lnkbtn_Finicial" runat="server"  ImageUrl="../Images/Urjamitra/finicial.png" width="30px" /><br />--%>
                    <a href="../Admin/Frm_FinancialAudit.aspx"><img src="../Images/Urjamitra/finicial.png" width="30px" onclick="clickloadingurja   ()"></a> 
                    <p style=" font-size: 13px; font-weight: 600;">Fiancial<br /> Audit</p>
                </div>
                 </center>
                </div>
            
     <div style="margin:15px;"></div>
      <div class="row" id="foot" style=" font-size:small; padding: 8px; margin-top:25px; background-color:rgb(12,18,80);">
               <center>
             <class="col-md-12">
                   <b style="color:white;">Toll Free: 1800 309 2030</b><br />
                <b style="color:white;">An initiative by : Power Payment Services Pvt. Ltd.</b>

             </class>
                   </center>
          </div>
         
          <div class="wrapper" id="loading" style="position: fixed; top: 0; left: 0; width: 100%; display: none; z-index: 20;">
    <div class="loader">
      <span style="--i:1;"></span>
      <span style="--i:2;"></span>
      <span style="--i:3;"></span>
      <span style="--i:4;"></span>
      <span style="--i:5;"></span>
      <span style="--i:6;"></span>
      <span style="--i:7;"></span>
      <span style="--i:8;"></span>
      <span style="--i:9;"></span>
      <span style="--i:10;"></span>
      <span style="--i:11;"></span>
      <span style="--i:12;"></span>
      <span style="--i:13;"></span>
      <span style="--i:14;"></span>
      <span style="--i:15;"></span>
      <span style="--i:16;"></span>
      <span style="--i:17;"></span>
      <span style="--i:18;"></span>
      <span style="--i:19;"></span>
      <span style="--i:20;"></span>
    </div>
  </div> 
              <script>
                  function clickloadingurja() {
                      let loadingBar = document.getElementById("loading");
                      loadingBar.style.display = "flex";
                      loadingBar.style.background = "linear-gradient( rgba(0,0,0,0.1),rgba(0,0,0,0.1))";
                  }
              </script>
        </div>
<%--Div Mobile View-Urjamitra - End--%>

<%--Div Mobile View-Ozzype - Start--%>
  <div class="row" runat="server"  id="divmobile2" visible="false" style="position:relative;">

         <div class="wrapper" id="loading-two" style="position: fixed; top: 0; left: 0; width: 100%; display: none; z-index: 20;">
    <div class="loader">
      <span style="--i:1;"></span>
      <span style="--i:2;"></span>
      <span style="--i:3;"></span>
      <span style="--i:4;"></span>
      <span style="--i:5;"></span>
      <span style="--i:6;"></span>
      <span style="--i:7;"></span>
      <span style="--i:8;"></span>
      <span style="--i:9;"></span>
      <span style="--i:10;"></span>
      <span style="--i:11;"></span>
      <span style="--i:12;"></span>
      <span style="--i:13;"></span>
      <span style="--i:14;"></span>
      <span style="--i:15;"></span>
      <span style="--i:16;"></span>
      <span style="--i:17;"></span>
      <span style="--i:18;"></span>
      <span style="--i:19;"></span>
      <span style="--i:20;"></span>
    </div>
  </div>

          <center> 
              <a name="Home"> </a>
        <div class="row" style="width: 98%; display: flex; align-items: flex-start; justify-content: center; border-radius: 8px; margin-top: 5px; background: #f5f5f5; padding: 17px;">
            <div class="col-md-6" style="width: 100%; display: flex; align-items: center; background: #f5f5f5; padding: 8px;">
               <a onclick="clickloadingozzy()" href="../Admin/cKycForm.aspx"><img src="../Images/Ozzype/logo-1.jpeg" width="40px" ></a>  
                                <p style="position: relative; top: 5px; left: 5px; font-weight: bold; color: black; font-size: 14px; display: block;">Add Address
                                    <%--<asp:TextBox ID='txtPermanentAddress' runat='server'  placeholder='PermanentAddress' Style="position:absolute; top: 25px; border: 2px solid black; height: 15px; width: 62px; z-index: 5; left: 499px;"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtCity" runat="server" placeholder="City" Style="position:absolute; height: 19px; left: -10px; top: 22px; right: auto; width: 133%; font-weight: 400; font-size: 15px; border: none; background: #f5f5f5; outline: none;" ReadOnly="true"  ></asp:TextBox>
                                </p><br />
            </div>
            <div class="col-md-6" style="width: 100%; display: flex; align-items: center; justify-content: space-evenly; gap: 15px; background-color: #f5f5f5; padding: 9px; position: relative; top: 5px;">
                 <img src="../images/Nav-Bar/QR-code.png" width="30px" />
                <%--<img src="../images/Nav-Bar/bell.png" width="30px"/>--%>
              <a href="../Admin/BOS_Raise_Request_Complaint.aspx"><img src="../images/Nav-Bar/question.png" width="30px"/></a>  

                <%--<p style="font-size: 18px;"><a href="#" style="text-decoration: none;">Sign Up/<a href="#" style="text-decoration: none; position: relative;" onclick="clicklog()">Login</a></p>--%>
                    

                <%-----Popup start---%>
               <%-- <div id="pop-bar" class="popup-bar" style="visibility:hidden; width: 180%; padding: 10px; position:absolute; top: 10px; right: 10%; transform: translate()-50% -50%; z-index: 100; background-color: rgb(204, 206, 224); border-radius: 15px; box-shadow: 0 0 6px rgba(0,0,0,0.9);">
                    <div class="col-md-12" style="text-align: end;">
                    <img src="../images/Ozzype/call on/cross.png" onclick="clicklogoff()" width="40px" style="position: relative; top: 5px; border-radius: 12px;"/>
                        </div> 
                    <div class="col-md-12">
                        <h1 style="font-size: 45px; font-weight: 600;">Log In</h1>
                        <p style="font-size: 17px; font-weight: 500;">With Social network</p>
                        <br />
                        <p style="font-size: 17px; font-weight: 500;">or with Username</p>
                        <p style="font-size: 17px; font-weight: 500;">You can log in using</p>
                        <br />
                        <input type="text" placeholder="Enter Your Mobile Number" style="border-radius: 5px; padding: 12px; border: none;"/>
                        <input type="password" placeholder="Enter Your Password" style="border-radius: 5px; padding: 12px; border: none; margin-top: 4px;"/><br /><br />    
                        <a href="#" style="color: rgb(25, 48, 179); font-size: 18px; text-decoration: none; font-weight: 500;">Forgot Password</a>
                        <br /><br /><br />
                        <button class="btn" style="width: 60%; color: white; background-color: palevioletred;">Log In</button><br /><br />
                        <a href="#" style="color: rgb(25, 48, 179); font-size: 16px; text-decoration: none;">Don't have an account?</a><br />
                        <a href="#" style="color: rgb(25, 48, 179); font-size: 16px; text-decoration: none;">Register</a><br />
                    </div>
                </div>--%>
                 <%-----Popup End---%>

            </div>
        </div>  
            </center>

          <center style="margin-top: 5px;">
            <div class="container" style="width: 100%; display: flex; flex-direction: column; gap: 6px; background-color: #f5f5f5; margin-top: 5px; border-radius: 8px; padding: 20px; margin:0; padding: 0; box-sizing: border-box;">

                <center>
                <div class="row" style="width: 98%; border-radius: 8px; margin-top: 5px;">
               <div class="col-md-12">
                   <img name="slide" width="100%" height="100%" style="border-radius: 15px;"/>
               </div>
        </div>
                    </center>
                </div>
            
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Transfer Money</h5></div>
            <div class="row" style="width:100%;  display: flex; align-items: center; justify-content: space-between;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <a onclick="clickloadingozzy()" href="../Admin/BOS_MoneyTransfer.aspx" ><img src="../Images/Nav-Bar/Tra mon/mobile.jpg" width="42px" style="position: relative; bottom: 2px;border-radius: 15px;"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Bank<br>Transfer</p>
              </div>
                 <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="../Admin/My_Acc_Details.aspx"><img src="../Images/Nav-Bar/Tra mon/to-bank.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">To Bank/<br>UPI ID</p>
              </div>
               <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../Images/Nav-Bar/Tra mon/to-self.jpg" width="42px" style="position: relative; bottom: 3px; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">To Self<br>Account</p>
              </div>
               <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <a href="../Admin/BOS_AddMoneyToWallet.aspx"><img src="../Images/Nav-Bar/Tra mon/check-bank.jpg" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Add<br>Amount</p>
              </div>
            </div>
             <div class="row" style="padding:8px; width:100%; display: flex; align-items: center; justify-content: space-between; background-color: #E7F0FF; border-bottom-left-radius: 15px; border-bottom-right-radius: 15px; position: relative; top: 5px;">
              <div class="col-3">
                 
                <p style="font-size: 15px; font-weight: bold; position: relative; top:5px;">My UPI ID:<%--<p style="font-size: 12px; font-weight: 500;">8825689753@ybl</p> --%> </p> 
                <%--<img src="../Images//Ozzype/some-pics/next.jpeg" style="position: relative; bottom: 7px; color: grey; border-radius: 8px;" widh: 30px;>--%>
                  
              </div>
                 <div class="col-9">
                      <asp:TextBox ID="txt_upi_id" runat="server" cssclass="form-control"  Style="" ReadOnly="true"  ></asp:TextBox>
                 </div>
            </div>

        </div>
            </center>

          <center>
                <div class="row" style="width: 100%; display: flex; justify-content: space-between; padding: 5px; align-items: flex-start; margin-top: 3px;">
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                     <a href="../Admin/BOS_TransferAmount_Form.aspx"><img src="../Images/Ozzype/some-pics/phone-pay-wallet.jpeg" style="font-size: 20px; position: relative; top: 3px;" onclick="clickloadingozzy()"> </a> 
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">OZZY Pay Wallet</p>
                    </div>
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                      <img src="../Images/Ozzype/some-pics/reward.jpeg" style="font-size: 20px; position: relative; top: 3px;" onclick="clickloadingozzy()">
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; left:7px; top: 3px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>M.Cash:0</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                    </div>
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                       <img src="../Images/Ozzype/some-pics/refer.jpeg" style="font-size: 20px; position: relative; top: 3px;" onclick="clickloadingozzy()">
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">Refer & Get</p>
                    </div>
                </div>
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Recharge & Pay Bills</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a href="BOS_BBPS_PS.aspx?type=Mobile"><img src="../Images/Nav-Bar/TM/mobile.jpg" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Mobile</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a href="BOS_BBPS_PS.aspx?type=DTH"><img src="../Images/Nav-Bar/TM/dth.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">DTH</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="BOS_BBPS_PS.aspx?type=Electricity"> <img src="../Images/Nav-Bar/TM/electricity.jpg" width="42px" style="position: relative; top: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Electricity</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="BOS_BBPS_PS.aspx?type=Postpaid"><img src="../Images/Nav-Bar/TM/pospaid.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Postpaid</p>
              </div>
            </div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <center>
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a href="BOS_BBPS_PS.aspx?type=Broadband"><img src="../Images/Nav-Bar/TM/broadband.png" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Broadband</p>
              </div>
                    </center>
                <center>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="BOS_BBPS_PS.aspx?type=Waterbill"><img src="../Images/Nav-Bar/TM/water.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Water Bill</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a href="BOS_BBPS_PS.aspx?type=EMI"><img src="../Images/Nav-Bar/TM/emi.png" width="42px" style="position: relative; top: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">EMI</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <a href="BOS_BBPS_PS.aspx?type=Municipality"><img src="../Images/Nav-Bar/TM/muncipality.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Municipality</p>
              </div>
                    </center>
            </div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <center>
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="BOS_BBPS_PS.aspx?type=LPG"><img src="../Images/Nav-Bar/TM/lpg.png" width="42px" style="position: relative; bottom: 2px; border-radius:15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">LPG</p>
              </div>
                    </center>
                <center>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a href="BOS_BBPS_PS.aspx?type=Cable"><img src="../Images/Nav-Bar/TM/cable.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Cable</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a href="BOS_BBPS_PS.aspx?type=Insurance"><img src="../Images/Nav-Bar/TM/insurance.png" width="42px" style="position: relative; top: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Insurance</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="BOS_BBPS_PS.aspx?type=Fastag"><img src="../Images/Nav-Bar/TM/fastag.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Fastag</p>
              </div>
                    </center>
            </div>
        </div>
            </center>

          <center>
          <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
       <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;"><a name="Insurance"></a>   Insurance</h5></div>     
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <a href="#"><img src="../images/Nav-Bar/Insurance/bike.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Bike</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/car.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Car</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/health.png" width="42px" style="position: relative; top: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Health++</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/PA.png" width="42px" style="position: relative; top: 10px; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; position: relative; top: 10px;">Personal<br>Accident</p>
              </div>
            </div>
            <div class="row" style=" width:100%;  display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/tl.png" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Term<br>Life</p>
              </div>
               <div class="col- md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: space-around; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"> <img src="../Images/Nav-Bar/Insurance/IF.png" width="42px" style="position: relative; bottom: 5px; border-radius: 15px;" onclick="clickloadingozzy()"></a>  
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">International<br>Travel</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/ir.png" width="42px" style="position: relative; bottom: 3px; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;position: relative; top: 1px;">Insurance<br>Renewal</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <%--<img src="./Images//Ozzype/check-bank-balance.jpeg" width="45px">--%>
               <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
              </div>
            </div>
        </div>
            </center>
       
          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Travel Booking</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="https://www.makemytrip.com/"><img src="../images/Nav-Bar/Travel Booking/flight.png" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Flight</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
            <a href="https://www.irctc.co.in/"><img src="../images/Nav-Bar/Travel Booking/train.jpg" width="42px" style="position: relative; bottom: 3px; border-radius: 15px;" onclick="clickloadingozzy()"></a>  
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">IRCTC</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="https://www.redbus.in/"><img src="../images/Nav-Bar/Travel Booking/bus.jpg" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Bus</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="https://www.booking.com/"><img src="../images/Nav-Bar/Travel Booking/hotel.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Hotels</p>
              </div>
            </div>
            
        </div>
            </center>
       
          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Switch</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="https://www.swiggy.com/"><img src="../images/Nav-Bar/Switch/swiggy.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">swiggy</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="https://www.delhimetrorail.com/"> <img src="../images/Nav-Bar/Switch/dmrc.png" width="42px" style="border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px; position: relative;">Delhi Metro</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a href="https://www.myntra.com/"><img src="../images/Nav-Bar/Switch/myntra.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloadingozzy()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Myntra</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="https://www.hotstar.com/" style="background-color: #f5f5f5;"><img src="../images/Nav-Bar/Switch/hotstar.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Hotstar</p>
              </div>
            </div>
            
        </div>
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Subcription</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
            <a href="https://www.hotstar.com/"><img src="../images/Nav-Bar/Subcription/disney-hotstar.png" width="42px" style="position: relative;border-radius: 15px; top:10px;" onclick="clickloadingozzy()"></a>  
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px; top:10px;">Disney<br />Hotstar</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
            <a href="https://tinder.com/"><img src="../images/Nav-Bar/Subcription/tinder.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloadingozzy()"></a>  
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px; position: relative; top: 5px;">Tinder</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="https://www.zee5.com/"><img src="../images/Nav-Bar/Subcription/zee5.jpeg" width="42px" style="position: relative;border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 5px; position: relative; top: 5px;">Zee5</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="https://www.flipkart.com/"><img src="../images/Nav-Bar/Subcription/flipkart.png" width="42px" style="position: relative;border-radius: 15px;" onclick="clickloadingozzy()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; position: relative; top: 5px;">Flipkart</p>
              </div>
            </div>
            
        </div>
            </center>

    <div style="margin:65px;">
    </div>
      <div class="footer" style="position: fixed; top: calc(90vh); height: 18vh; width: 100%; right: 1px; z-index: 5 ; background: #DD4B39; 
                display: flex; align-items: center; justify-content: space-evenly; border: none;">
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
            <a href="#Home"><img src="../images/Footer images/home.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;" onclick="clickloadingozzy()"/ ></a>  
              <p>Home</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
            <a href="#"><img src="../images/Footer images/shopping-bag--v1.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;" onclick="clickloadingozzy()"/></a>  
              <p>Store</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
            <a href="#Insurance"><img src="../images/Footer images/shield.jpg"  width="28px" style="background: white; border-radius: 50%; border: 1px solid white;" onclick="clickloadingozzy()"/></a>  
              <p>Insurance</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
               <a href="../Admin/BOS_API_Wise_Report.aspx"><img src="../images/Footer images/rupee-1.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;" onclick="clickloadingozzy()"/></a>
              <p>Report</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
            <a href="../Admin/BOS_AddMoney_Report.aspx"><img src="../images/Footer images/history.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;" onclick="clickloadingozzy()"/></a>  
              <p>History</p>
          </div>
        </div>

       <script>
           var i = 0;
           var images = [];
           var time = 4000;

           images[0] = "../images/Ozzype/Banner/one.png";
           images[1] = "../images/Ozzype/Banner/two.png";
           images[2] = "../images/Ozzype/Banner/three.png";



           function changeImg() {
               document.slide.src = images[i];
               if (i < images.length - 1) {
                   i++;
               } else {
                   i = 0;
               }
               setTimeout("changeImg()", time);
           }
           window.onload = changeImg;
           function clickloadingozzy() {
               let loadingBar = document.getElementById("loading-two");
               loadingBar.style.display = "flex";
               loadingBar.style.background = "linear-gradient( rgba(0,0,0,0.1),rgba(0,0,0,0.1))";
           }
       </script>
 </div>
 
  
<%--Div Mobile View-Ozzype  - End--%>



<%--Div Mobile View-PayKre - Start--%>

    <div class="row" runat="server" style="padding-bottom: 70px; position: relative;" id="divmobile_paykare">

        <div class="wrapper" id="loading-one" style="position: fixed; top: 0; left: 0; width: 100%; display: none; z-index: 20;">
    <div class="loader">
      <span style="--i:1;"></span>
      <span style="--i:2;"></span>
      <span style="--i:3;"></span>
      <span style="--i:4;"></span>
      <span style="--i:5;"></span>
      <span style="--i:6;"></span>
      <span style="--i:7;"></span>
      <span style="--i:8;"></span>
      <span style="--i:9;"></span>
      <span style="--i:10;"></span>
      <span style="--i:11;"></span>
      <span style="--i:12;"></span>
      <span style="--i:13;"></span>
      <span style="--i:14;"></span>
      <span style="--i:15;"></span>
      <span style="--i:16;"></span>
      <span style="--i:17;"></span>
      <span style="--i:18;"></span>
      <span style="--i:19;"></span>
      <span style="--i:20;"></span>
    </div>
  </div>


          <center>
        <div class="row" style="width: 98%; display: flex; align-items: flex-start; justify-content: center; border-radius: 8px; margin-top: 5px; background: rgb(195,195,195);">
            <div class="col-md-6" style="width: 100%; display: flex; align-items: center; background: rgb(195,195,195); padding: 6px;" onclick="clickloading()">
                 <img src="../assets_PayKre/images/all-images/nav-bar/person.png" style="border-radius: 15px;" width="42px">
                                <p style="position: relative; top: 5px; left: 5px; font-weight: bold; color: black; font-size: 14px;">Add Address</p>
                                <%--<a href="#" style="border: none; position: absolute; top: 4px; right: 0; background-color: lightskyblue; color: white; text-decoration: none; outline: none; border-radius: 8px;
                 margin-top: 2px; font-weight: bold; letter-spacing: 1px; box-shadow: 0 0 4px rgba(0,0,0,0.6);
                 padding: 8px; font-size: 17px;">B2B Login <img src="../images/Nav-Bar/right-arrow.png"  width="15px" style="position: relative; bottom: 1px;"/></a>--%>
            </div>
            <div class="col-md-6" style="width: 100%; display: flex; align-items: center; justify-content: space-evenly; background-color: #f5f5f5; position: relative;">
               <%--<img src="../images/Nav-Bar/QR-code.png" width="30px" />--%>
<%--                <img src="../images/Nav-Bar/bell.png" width="30px"/>
                <img src="../images/Nav-Bar/question.png" width="30px"/>--%>
<%--                <a onclick="clickLogin()" href="#" style="border: none; position: absolute; top: 4px; right: 35px; background-color: lightskyblue; color: white; text-decoration: none; outline: none; border-radius: 8px;
                 margin-top: 2px; font-weight: bold; letter-spacing: 1px; box-shadow: 0 0 4px rgba(0,0,0,0.6);
                 padding: 8px; font-size: 17px;">Login <img src="../images/Nav-Bar/right-arrow.png"  width="15px" style="position: relative; bottom: 1px;"/></a>--%>


                   <%------------------PopupLogIn Start------------------%>
                              <table class="container" id="table-box" style=" border-radius: 15px;
                                        position: absolute; top: 50%; right: 50%; z-index: 5; background: black;
                                        box-shadow: 0 0 5px rgba(0,0,0,0.8); display: flex; align-items: center; right: 14%;
                                        justify-content: center; width: 90vw; height: 85vh; visibility: hidden;">
                                                  
              <tr class="row" style="display: flex; align-items: center; justify-content:center; border: none; border-radius: 15px; 
                            box-shadow: 0 0 6px rgba(255,255,255,0.9); width: 89vw; height: 85vh;">
            <td class="col" style="background: black; position: relative; border-radius: 15px; display: flex; align-content: center; justify-content: center; 
                  flex-direction: column; padding: 5px 80px; width: 88vw; height: 85vh;">
            <div class="xmark" style="position:absolute; left: 88%; top: 3%;"> 
            <a onclick="clickOneTwo()" href="#" style="z-index: 10; position: relative; padding: 0 8px; border: none; border-radius: 5px; font-size: 21px; background-color: white; color: red;
                                   font-weight: 550;">x</a>
          </div>


          <div class="log-in-bar" style="text-align: center; color: white; width: 87vw; height: 85vh;  position: absolute; left: 0;   ">
            <div class="log-in-bar-one">
              <!-- <img src="../P5/Images/imgj.png" class="img-one">
              <p class="number">1</p> -->
              <div class="log-in-content">
                <h1 style=" padding: 15px;">Log In</h1>
          </div>
        </div>
            <div class="log-in-bar-two">
          <!-- <button class="btn facebook-bar">
            <i class="fa-brands fa-facebook-f"></i>
            <p>Facebook</p>
          </button>
          <button class="btn google-bar">
            <i class="fa-brands fa-google"></i>
            <p>Google</p>
          </button> -->
        </div>
      </div>

      <div class="log-in-bar second"style=" text-align: center; color: white;">
        <div class="log-in-bar-one">
          <!-- <img src="../P5/Images/imgj.png" class="img-one">
          <p class="number">2</p> -->
          <div class="log-in-bar-one-section">
            <p><b>You can log in using</b></p>
          </div>
        </div>
        <div class="log-in-bar-two-2" style="display: flex; flex-direction: column; align-items: center; justify-content: center; gap: 5px;"> 
             <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" Visible="false" ></asp:TextBox>   
                            <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter Your Mobile Number" style="border-radius: 5px; color: black; padding: 12px; width: 80%; border: 5px solid red;" ></asp:TextBox>

            <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Your Password" TextMode="Password" MaxLength="10"
                                style="border-radius: 5px; padding: 12px; border: none; margin-top:4px; width: 80%; color: black;"></asp:TextBox><br />
             <asp:LinkButton ID="btnForgotPassword" runat="server" style="color: white;">Forgot Password</asp:LinkButton>
            <asp:CheckBox ID="ChkrememberMe" runat="server" Text="Remember me" class="form-check-label" style="margin-top: 8px;"/>
             
         <%-- <input type="text" placeholder="Enter Your Mobile Number">
          <input type="password" placeholder="Enter Your Password">
          <p><a href="#">Forgot password</a></p>--%>

            <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>
                             <asp:Label ID="Label3" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>
                              <asp:Label ID="lblpassword" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>
        </div>
      </div>
               

      <div class="log-in-bar third"style="text-align: center; color: white;">
        <div class="log-in-bar-one" style="text-align: center; margin-top: 15px;">
          <!-- <img src="../P5/Images/imgj.png" class="img-one">
          <p class="number">3</p> -->
        </div><br />    
        <div class="log-in-bar-two-2-2">
             <asp:Button ID="Button1" runat="server" Text="Log In" style="width:80%; color: white; background-color: palevioletred; border-radius:5px; padding:8px 20px; border:none;"/><br />
          <%--<button class="btn" style="color: white;" >Log In</button>--%>
        </div>
        <div class="log-in-bar-three-3" style="margin-top: 20px;">


                            <a href="#" style="color: rgb(25, 48, 179); font-size: 16px; text-decoration: none; font-weight: 550; margin-top: 15px;">Don't have an account?</a><br />


                            <a href="../create_custome1.aspx" style="color: rgb(25, 48, 179); font-size: 16px; font-weight: 550; margin-top: 15px; text-decoration: none; color: white; text-decoration: underline;">Sign-up</a><br />
          <%--<p><a href="#">Don't have an account ? <p>Register</p></a></p>--%>
          <!-- <img src="../P5/Images/imgj.png" class="img-one">
          <p class="para">4</p> -->
        </div>
      </div>



        </td>
              </tr>
                                                    
                                               </table>
                   <%------------------PopupLogIn End------------------%>


            </div>
        </div>  
            </center>

          <center style="margin-top: 5px;">
            <div class="container" style="width: 100%; display: flex; flex-direction: column; gap: 6px; background-color: #f5f5f5; margin-top: 5px; border-radius: 8px; padding: 20px; margin:0; padding: 0; box-sizing: border-box;">

                <center>
                <div class="row" style="width: 100%; border-radius: 8px; margin-top: 5px;">
                    <div class="col-12">
                   <img name="slide1" width="100%" height="100%" style="border-radius: 15px;"/>
                        </div>
        </div>
                    </center>
                </div>
            
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: rgb(195,195,195); flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Transfer Money</h5></div>
            <div class="row" style="width:100%;  display: flex; align-items: center; justify-content: space-between;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <a href="../Admin/BOS_MoneyTransfer.aspx" ><img src="../assets_PayKre/images/all-images/TM/mobile.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;" onclick="clickloading()"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">To Mobile<br>Number</p>
              </div>
                 <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../assets_PayKre/images/all-images/TM/bank.jpg" width="42px" style="border-radius: 15px; bottom: 4px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">To Bank/<br>UPI ID</p>
              </div>
               <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../assets_PayKre/images/all-images/TM/to-self.jpg" width="42px" style="position: relative; bottom: 3px; border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">To Self<br>Account</p>
              </div>
               <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <a href="#"></a><img src="../assets_PayKre/images/all-images/TM/check-bank.jpg" width="42px" style="border-radius: 15px;" onclick="clickloading()">
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Check<br>Bank Balance</p>
              </div>
            </div>
             <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-between; background-color: #E7F0FF; border-bottom-left-radius: 15px; border-bottom-right-radius: 15px; position: relative; top: 10px;">
              <div class="col-12" style="width: 100%; display: flex; align-items: center; justify-content: space-between; margin-top: 15px; padding: 0 20px;">
                <p style="font-size: 15px; font-weight: bold;">My UPI ID:<p style="font-size: 12px; font-weight: 500;">8825689753@ybl</p></p>
                <img src="../Images//Ozzype/some-pics/next.jpeg" style="position: relative; bottom: 7px; color: grey; border-radius: 8px;" widh: 30px;>
              </div>
            </div>

        </div>
            </center>

          <center>
                <div class="row" style="width: 100%; display: flex; justify-content: space-between; padding: 5px; align-items: flex-start; margin-top: 4px;">
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                      <img src="../assets_PayKre/images/all-images/refer-sec/wallet.jpg" width="30px" style="font-size: 20px; position: relative; top: 3px; border-radius: 15px;" onclick="clickloading()"> 
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">Paykare wallet</p>
                    </div>
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                      <img src="../assets_PayKre/images/all-images/refer-sec/gift.jpg" width="30px" style="font-size: 20px; position: relative; top: 3px; border-radius: 15px;" onclick="clickloading()">
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rewards&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                    </div>
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                       <img src="../assets_PayKre/images/all-images/refer-sec/refer.jpg" width="30px" style="font-size: 20px; position: relative; top: 3px; border-radius: 15px;" onclick="clickloading()">
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">Refer & Get ₹100</p>
                    </div>
                </div>
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: rgb(195,195,195); flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Recharge & Pay Bills</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> --> 
              <a href="BOS_BBPS_PS.aspx?type=Mobile"><img src="../assets_PayKre/images/all-images/recharge/mobile.png" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;" onclick="clickloading()"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Mobile</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a href="BOS_BBPS_PS.aspx?type=DTH"><img src="../assets_PayKre/images/all-images/recharge/dth.png" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">DTH</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="BOS_BBPS_PS.aspx?type=Electricity"> <img src="../assets_PayKre/images/all-images/recharge/electricity.png" width="42px" style="border-radius: 15px; position: relative; top: 2px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Electricity</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="BOS_BBPS_PS.aspx?type=Postpaid"><img src="../assets_PayKre/images/all-images/recharge/postpaid.png" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Postpaid</p>
              </div>
            </div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <center>
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a href="BOS_BBPS_PS.aspx?type=Broadband"><img src="../assets_PayKre/images/all-images/recharge/broadband.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Broadband</p>
              </div>
                    </center>
                <center>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="BOS_BBPS_PS.aspx?type=Waterbill"><img src="../assets_PayKre/images/all-images/recharge/water.jpg" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Water Bill</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a href="BOS_BBPS_PS.aspx?type=EMI"><img src="../assets_PayKre/images/all-images/recharge/emi.png" width="42px" style="border-radius: 15px; position: relative; top: 2px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">EMI</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <a href="BOS_BBPS_PS.aspx?type=Municipality"><img src="../assets_PayKre/images/all-images/recharge/muncipality.png" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Municipality</p>
              </div>
                    </center>
            </div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <center>
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="BOS_BBPS_PS.aspx?type=LPG"><img src="../assets_PayKre/images/all-images/recharge/lpg.jpg" style="width: 42px; position: relative; bottom: 2px; border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">LPG</p>
              </div>
                    </center>
                <center>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a href="BOS_BBPS_PS.aspx?type=Cable"><img src="../assets_PayKre/images/all-images/recharge/cable.jpg" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Cable</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a href="BOS_BBPS_PS.aspx?type=Insurance"><img src="../assets_PayKre/images/all-images/recharge/insurance.png" width="42px" style="border-radius: 15px; position: relative; top: 2px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Insurance</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="BOS_BBPS_PS.aspx?type=Fastag"><img src="../assets_PayKre/images/all-images/recharge/fastag.png" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Fastag</p>
              </div>
                    </center>
            </div>
        </div>
            </center>

          <center>
              <a name="insurance"></a>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: rgb(195,195,195); flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Insurance</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <a href="#"><img src="../assets_PayKre/images/all-images/insurance/bike.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;" onclick="clickloading()"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Bike</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../assets_PayKre/images/all-images/insurance/car.png" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Car</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../assets_PayKre/images/all-images/insurance/health.jpg" width="42px" style="border-radius: 15px; position: relative; top: 2px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Health++</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../assets_PayKre/images/all-images/insurance/PA.jpg" width="42px" style="border-radius: 15px; position: relative; top: 10px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; position: relative; top: 10px;">Personal<br>Accident</p>
              </div>
            </div>
            <div class="row" style=" width:100%;  display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../assets_PayKre/images/all-images/insurance/TL.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Term<br>Life</p>
              </div>
               <div class="col- md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: space-around; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"> <img src="../assets_PayKre/images/all-images/insurance/IT.png" width="42px" style="border-radius: 15px; position: relative; bottom: 5px;" onclick="clickloading()"></a>  
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">International<br>Travel</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../assets_PayKre/images/all-images/insurance/IR.jpg" width="42px" style="border-radius: 15px; position: relative; bottom: 3px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 5px; position: relative; bottom: 1px;">Insurance<br>Renewal</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <%--<img src="./Images//Ozzype/check-bank-balance.jpeg" width="45px">--%>
               <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
              </div>
            </div>
        </div>
            </center>
       
          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: rgb(195,195,195); flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Travel Booking</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../images/Nav-Bar/Travel Booking/flight.png" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Flight</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
            <a href="#"><img src="../images/Nav-Bar/Travel Booking/train.jpg" width="42px" style="position: relative; bottom: 3px; border-radius: 15px;" onclick="clickloading()"></a>  
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">IRCTC</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../images/Nav-Bar/Travel Booking/bus.jpg" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Red Bus</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../images/Nav-Bar/Travel Booking/hotel.png" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Hotels</p>
              </div>
            </div>
            
        </div>
            </center>
       
          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: rgb(195,195,195); flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Switch</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../images/Nav-Bar/Switch/swiggy.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">swiggy</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"> <img src="../images/Nav-Bar/Switch/dmrc.png" width="42px" style="border-radius: 15px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px; position: relative;">Delhi Metro</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a href="#"><img src="../images/Nav-Bar/Switch/myntra.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloading()"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Myntra</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../images/Nav-Bar/Switch/hotstar.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloading()"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Hotstar</p>
              </div>
            </div>
            
        </div>
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: rgb(195,195,195); flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Subcription</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <img src="../images/Nav-Bar/Subcription/disney-hotstar.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloading()">
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; position: relative; top: 5px;">Disney Hotstar</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <img src="../images/Nav-Bar/Subcription/tinder.png" width="42px" style="position: relative; border-radius: 15px; " onclick="clickloading()">
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; position: relative; top: 5px;">Tinder</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <img src="../images/Nav-Bar/Subcription/zee5.jpeg" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloading()">
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; position: relative; top: 5px;">Zee5</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <img src="../images/Nav-Bar/Subcription/flipkart.png" width="42px" style="position: relative; border-radius: 15px;" onclick="clickloading()">
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; position: relative; top: 5px;">Flipkart</p>
              </div>
            </div>
            
        </div>
            </center>
            <div style="margin:75px;">
    </div>

         <div class="footer" id="footer-one" style="position: fixed; top: calc(90vh); height: 18vh; width: 100%; right: 1px; z-index: 5 ; background: linear-gradient(rgb(255,20,60),rgb(121,55,255)); 
                display: flex; align-items: center; justify-content: space-evenly; border: none;">
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/home.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;" onclick="clickloading()"/>
              <p>Home</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/shopping-bag--v1.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;" onclick="clickloading()"/>
              <p>Offline Services</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/shield.jpg"  width="28px" style="background: white; border-radius: 50%; border: 1px solid white;" onclick="clickloading()"/>
              <p><a href="#insurance" style="text-decoration: none; color: white;">Insurance</a></p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
               <img src="../images/Footer images/rupee-1.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;" onclick="clickloading()"/>
              <p>Wealth</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/history.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;"/>
              <p>History</p>
          </div>
        </div>

        <script>
            var i = 0;
            var images = [];
            var time = 4000;

            images[0] = "../assets_PayKre/images/slider/slider-one.png";
            images[1] = "../assets_PayKre/images/slider/slider-two.png";
            images[2] = "../assets_PayKre/images/slider/slider-three.png";



            function changeImg() {
                document.slide1.src = images[i];
                if (i < images.length - 1) {
                    i++;
                } else {
                    i = 0;
                }
                setTimeout("changeImg()", time);
            }
            window.onload = changeImg;

            let tableBox = document.getElementById("table-box");
            let footerOne = document.getElementById("footer-one");
            function clickLogin() {
                tableBox.style.visibility = "visible";
                footerOne.style.visibility = "hidden";
            }
            function clickOneTwo() {
                tableBox.style.visibility = "hidden";
                footerOne.style.visibility = "visible";
            }
            function clickloading() {
                let loadingBar = document.getElementById("loading-one");
                loadingBar.style.display = "flex";
                loadingBar.style.background = "linear-gradient( rgba(0,0,0,0.1),rgba(0,0,0,0.1))";
            }
        </script>
        
    </div>

<%--Div Mobile View-PayKre - End--%>

<%--Div Mobile View-EasyTalk - Start--%>
       <div class="row" runat="server" id="divmobile_EasyTalk" style=" background-color: rgb(145, 200, 240); border: 10px solid rgb(145, 200, 240); margin-top: 10px;">
         
         <div class="wrapper" id="loading_EasyTalk" style="position: fixed; top: 0; left: 0; width: 100%; display: none; z-index: 20;">
    <div class="loader">
      <span style="--i:1;"></span>
      <span style="--i:2;"></span>
      <span style="--i:3;"></span>
      <span style="--i:4;"></span>
      <span style="--i:5;"></span>
      <span style="--i:6;"></span>
      <span style="--i:7;"></span>
      <span style="--i:8;"></span>
      <span style="--i:9;"></span>
      <span style="--i:10;"></span>
      <span style="--i:11;"></span>
      <span style="--i:12;"></span>
      <span style="--i:13;"></span>
      <span style="--i:14;"></span>
      <span style="--i:15;"></span>
      <span style="--i:16;"></span>
      <span style="--i:17;"></span>
      <span style="--i:18;"></span>
      <span style="--i:19;"></span>
      <span style="--i:20;"></span>
        <img src="../Easy Talk - Copy/Images/Icon/icon-1.png" width="40"/>
    </div>
  </div>


         <center style="width: 100%; align-items: center; margin-top: 20px; justify-content: center; display: flex; flex-direction: column; gap: 6px;
           background-color: rgb(145, 200, 240); padding-bottom: 80px; border: 2px solid rgb(145, 200, 240);">


        <center style="width: 95%; display: flex; align-items: center; justify-content: center; background-color: rgb(145, 200, 240);">
      <div class="row" style="background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); margin-top: 8px; width: 100%; 
           box-shadow: 0 0 5px rgba(0,0,0,0.8 ); border-radius: 5px;">   
            <div class="col-sm-12" style="display: flex; align-items: center; justify-content: space-between; padding: 8px; position: relative;
                    border: 2px solid rgb(145, 200, 240);">
             <a Onclick="click_Easy()" href="../Admin/cKycForm.aspx"><div class="box-one" style="display: flex;">
              <img src="../Easy Talk - Copy/Images/Header/user-info.png" style="border-radius: 8px;" width="40px"/>
                <p style="position: relative; top: 5px; left: 5px; font-weight: bold; color: black; font-size: 14px;">Add Address
                <img src="../Easy Talk - Copy/Images/Header/drop-down.png" width="20px"></p>
              </div></a> 
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/QR-code.png"  width="40px"/>
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/bell.png"  width="40px"/>
            </div>
        </div>
       </center>

         <div class="row" style="width: 100%; border-radius: 8px; margin-top: 5px;">
               <div class="col-md-12">
                   <img name="slide_EasyTalk" width="100%" height="100%" style="border-radius: 15px;"/>
               </div>
        </div>
                  
        <div class="row" style="width: 95%; display: flex; flex-direction: column; align-items: center; gap: 10px; justify-content: center;border-radius: 15px;
        background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); box-shadow: 0 0 5px rgba(0,0,0,0.8 );">
            <div class="col-sm-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Transfer Money</h5></div>
            <div class="row" style="display: flex; align-items: center; justify-content: space-between;">
               <a Onclick="click_Easy()" href="../Admin/BOS_MoneyTransfer.aspx" ><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Tra mon/mobile.jpg"  width="40px" style="border-radius: 5px;"/>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Fund<br/>Transfer</p>
              </div> </a> 
                
              <a Onclick="click_Easy()" href="../Admin/My_Acc_Details.aspx" >  <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Tra mon/to-bank.png"  width="40px" style="border-radius: 5px;"/>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">To Bank/<br>UPI ID</p>
              </div></a> 
               <a Onclick="click_Easy()" href="#" >                            <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Tra mon/to-self.jpg"  width="40px" style="border-radius: 5px;"/>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">To Self<br>Account</p>
              </div></a> 
              <a Onclick="click_Easy()" href="../Admin/BOS_AddMoneyToWallet.aspx" > <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Tra mon/check-bank.jpg"  width="40px" style="border-radius: 5px;"/>
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Add<br/>Amount</p>
              </div></a>  
            </div>
            <div class="row" style="width: 100%; display: flex; align-items: center; justify-content: center; background-color: #E7F0FF; 
         border-bottom-left-radius: 15px; border-bottom-right-radius: 15px; position: relative; top: 10px;">
              <div class="col-sm-12" style="display: flex; align-items: center; justify-content: space-between; margin-top: 15px;">
                <p style="font-size: 15px; font-weight: bold;">My UPI ID:<p style="font-size: 12px; font-weight: 500;">8825689753@ybl</p></p>
              </div>
            </div>
        </div>

        <div class="row" style="width: 95%; display: flex; justify-content: space-between; padding: 5px; align-items: flex-start; margin-top: 3px;">
          <a Onclick="click_Easy()" href="../Admin/BOS_TransferAmount_Form.aspx" > <div class="col-sm-4 col-sm-offset-1  " style="background-color: rgb(248, 199, 63); color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
            <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/reward/wallet.png" width="40px"/>
            <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px; color: black; font-weight: 500;">Easy talk Wallet</p>
          </div></a>  
          <a Onclick="click_Easy()" href="#" ><div class="col-sm-4 col-sm-offset-1 " style="background-color: rgb(248, 199, 63); color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px;">
            <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/reward/reward.png" width="40px"/>
            <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px; color: black; font-weight: 500;">&nbsp&nbsp&nbsp&nbsp&nbsp Rewards &nbsp&nbsp&nbsp&nbsp&nbsp <br></p>
          </div> </a>  
          <a Onclick="click_Easy()" href="#" ><div class="col-sm-4 col-sm-offset-1" style="background-color: rgb(248, 199, 63); color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; left: 4px;">
            <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/reward/refer.png" width="40px"/>
            <P style="font-size: 10px; width: 180%; margin-top: 3px; position: relative; top: 3px; color: black; font-weight: 500;">Refer & Get ₹100</P>
          </div> </a>  
        </div>

        <div class="row" style="display: flex; align-items: center; gap: 10px; justify-content: center; width: 95%; border-radius: 15px; 
    background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); margin-top: 5px; box-shadow: 0 0 5px rgba(0,0,0,0.8 );
           flex-direction:column;">
          <div class="col-sm-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Recharge & Pay Bills</h5></div>
            <div class="row" style="display: flex;">
             <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=Mobile" style="width: 70px;"><div class="col-sm-3 show-more-easy-talk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/mobile.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Mobile<br>Recharge</p>
            </div> </a>
            <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=DTH" style="width: 70px;">  <div class="col-sm-3 show-more-easy-talk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;  position: relative; bottom: 8px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/dth.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">DTH</p>
            </div></a> 
             <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=Electricity" style="width: 70px;">  <div class="col-sm-3 show-more-easy-talk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;
            position: relative; bottom: 8px;">       
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/electricity.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Electricity</p>
            </div></a>
            <a Onclick="click_Easy()" href="#" style="width: 70px;"> <div class="col-sm-3 show-more-easy-talk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/card-card.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Credit Card<br>Bill Pament</p>
            </div></a>  
        
              </div>
            <div class="row" style="display: flex;">
            <a Onclick="click_Easy()" href="#" style="width: 70px;"> <div class="col-sm-3 drop-easy-talk show-more-easy-talk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/rent.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Rent Payment</p>
            </div> </a> 
             <a  Onclick="click_Easy()" href="#" style="width: 70px; position: relative; bottom: 6px;"><div class="col-sm-3 drop-easy-talk show-more-easy-talk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;
                 position: relative; top: 6px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/loan-repayment.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Loan<br />Repayment</p>
            </div> </a> 
            <a Onclick="click_Easy()" href="#" style="width: 70px; position: relative; bottom: 6px;"> <div class="col-sm-3 drop-easy-talk show-more-easy-talk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;
                  position: relative; top: 6px;">       
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/education-loan.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Education<br />Fees</p>
            </div></a>  
             <a  ><div class="col-sm-3 show-more-easy-talk show-more-easyTalk" id="tenEasyTalk" style="width: 70px; text-align: center; display: flex;
            flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/TM/next.png" width="40px" style="border-radius: 5px;" class="see-all-easy-talk"/>
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">See All</p>
            </div></a>
                <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=Postpaid" > <div class="col-sm-3 show-more-easy-talk" id="nineEasyTalk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; display: none;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/postpaid.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px; text-align: start;">Postpaid</p>
            </div> </a> 
                </div>
            <div class="row" style="display: flex;">
             <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=Broadband" ><div class="col-sm-3 show-more-easy-talk" id="eightEasyTalk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;
                 position: relative; top: 6px; display: none;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/broadband.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px; text-align: start;">Broadband/<br>Landline</p>
            </div> </a> 
            <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=Waterbill" > <div class="col-sm-3 show-more-easy-talk" id="sevenEasyTalk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;
                  position: relative; top: 6px; display: none;">       
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/water-bill.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;text-align: start;">Water Bill</p>
            </div></a>  
            <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=EMI"  style="position: relative; top: 5px;"><div class="col-sm-3 show-more-easy-talk" id="sixEasyTalk" style="display: none; text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; display: none;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/emi.png" width="40px" style="border-radius: 5px;" class="see-all-easy-talk"/>
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; text-align: start;">EMI</p>
            </div> </a>
                <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=LPG" style="position: relative; top: 5px;"> <div class="col-sm-3 show-more-easy-talk"  id="fiveEasyTalk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; display: none;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/lpg.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px; text-align: start;">Book A<br/>Cylinder</p>
            </div> </a> 
                </div>
            <div class="row" style="display: flex;"> 
             <a Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=Municipality" ><div class="col-sm-3 show-more-easy-talk" id="fourEasyTalk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;
                 position: relative; top: 6px;display: none;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/municipality.jpg" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px; text-align: start;">Municipality</p>
            </div> </a> 
            <a  Onclick="click_Easy()" href="../Admin/BOS_BBPS_PS.aspx?type=Cable" > <div class="col-sm-3 show-more-easy-talk" id="threeEasyTalk" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;
                  position: relative; top: 6px; display: none;">       
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/cable.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px; text-align: start;">Cable TV</p>
            </div></a>  
            <a Onclick="click_Easy()" href="#" style="position: relative; top: 5px;"> <div class="col-sm-3 show-more-easy-talk" id="twoEasyTalk" style="display: none; text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; display: none;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/fastag.png" width="40px" style="border-radius: 5px;" class="see-all-easy-talk"/>
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; text-align: start;">FASTag</p>
            </div> </a> 
                <div class="col-sm-3 show-more-easy-talk" id="oneEasyTalk" onclick="seeLessEasyTalk()" style="display: none;
        text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; display: none;
               position: relative; top: 5px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/show-more/seeLess.png" width="40px" style="border-radius: 5px;" class="see-all-easy-talk"/>
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; text-align: start;">See Less</p>
            </div>
                </div>
             </div> 

        <div class="row" style="display: flex; align-items: center; gap: 10px; justify-content: center; width: 95%; border-radius: 15px; 
       background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); box-shadow: 0 0 5px rgba(0,0,0,0.8 ); margin-top: 5px; padding: 5px 0; flex-direction:column;">
            <div class="col-sm-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Insurance</h5></div>
          <div class="row" style="display: flex; align-items: center; justify-content: space-between; padding: 5px; margin: -10px;">
            <a Onclick="click_Easy()" href="#" style="width: 70px;"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/bike.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 5px;">Bike</p>
            </div> </a> 
             <a Onclick="click_Easy()" href="#" style="width: 70px;"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/car.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 5px;">Car</p>
            </div> </a>
             <a Onclick="click_Easy()" href="#" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; margin-bottom: 5px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/health.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 5px;">Health++</p>
            </div> </a> 
            <a Onclick="click_Easy()" href="#" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; position: relative; top: 5px ;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/PA.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 5px;">Personal<br>Accident</p>
            </div> </a>  
          </div>
          <div class="row" style="display: flex; align-items: center; justify-content: space-between; margin-top: 5px;">
            <a Onclick="click_Easy()" href="#" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/TL.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 5px;">Term<br>Life</p>
            </div></a>
            <a Onclick="click_Easy()" href="#" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/IF.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">International<br>Travel</p>
            </div></a>
            <a Onclick="click_Easy()" href="#" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/IR.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Insurance<br>Renewal</p>
            </div></a>
            <a Onclick="click_Easy()" href="#" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; position: relative; bottom: 8px;">
<%--              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Insurance/next.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 5px;">See All</p>--%>
            </div></a>      
          </div>
        </div>

        <div class="row" style="display: flex; align-items: center; gap: 10px; justify-content: center; width: 95%; border-radius: 15px;  background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); margin-top: 5px; box-shadow: 0 0 5px rgba(0,0,0,0.8 ); flex-direction:column;">
          <div class="col-sm-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Travel Bookings</h5></div>
          <div class="row" style="display: flex; align-items: center; justify-content: space-between; padding: 5px; margin: -10px;">
            <a Onclick="click_Easy()" href="https://www.makemytrip.com/" ><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Travel Booking/flight.png" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Flights</p>
            </div> </a>  
            <a Onclick="click_Easy()" href="https://www.irctc.co.in/" ><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Travel Booking/train.jpg" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">IRCTC</p>
            </div> </a>  
            <a Onclick="click_Easy()" href="https://m.redbus.in/" ><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Travel Booking/bus.jpg" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Bus</p>
            </div> </a>  
            <a Onclick="click_Easy()" href="https://www.booking.com/" ><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Travel Booking/hotel.png" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Hotels</p>
            </div> </a>  
          </div>
        </div>

        <div class="row" style="display: flex; align-items: center; gap: 10px; justify-content: center; width: 95%; border-radius: 15px;  background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); margin-top: 5px; box-shadow: 0 0 5px rgba(0,0,0,0.8 );flex-direction:column;">
          <div class="col-sm-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Switch</h5></div>
          <div class="row" style="display: flex; align-items: center; justify-content: space-between; padding: 5px; margin: -10px;">
         <a Onclick="click_Easy()" href="https://www.swiggy.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: whitesmoke; background-color: #622EA2; font-size: 25px;"></i> -->
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/swiggy.png" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Swiggy</p>
            </div></a>   
          <a Onclick="click_Easy()" href="https://www.zomato.com/"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/zomato.png" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Zomato</p>
            </div></a>    
           <a Onclick="click_Easy()" href="https://www.myntra.com/"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/myntra.png" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Myntra</p>
            </div></a>    
           <a Onclick="click_Easy()" href="https://pizzaonline.dominos.co.in/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; ">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/Dominos.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Domino's</p>
            </div></a>
          </div>
           <div class="row" style="display: flex; align-items: center; justify-content: space-between; padding: 5px; margin: -10px;">
         <a Onclick="click_Easy()" href="https://www.amazon.in/?&ext_vrnc=hi&tag=googhydrabk1-21&ref=pd_sl_5szpgfto9i_e&adgrpid=58075519359&hvpone=&hvptwo=&hvadid=486462454211&hvpos=&hvnetw=g&hvrand=11555263037452230483&hvqmt=e&hvdev=c&hvdvcmdl=&hvlocint=&hvlocphy=9061699&hvtargid=kwd-64107830&hydadcr=14452_2154371&gclid=EAIaIQobChMI5fHk5_ay_AIVUTUrCh2cpQlJEAAYASAAEgKJRPD_BwE"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: whitesmoke; background-color: #622EA2; font-size: 25px;"></i> -->
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/amazon.jpg" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Amazon</p>
            </div></a>   
          <a Onclick="click_Easy()" href="https://online.kfc.co.in/"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/kfc.png" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">KFC</p>
            </div></a>    
           <a Onclick="click_Easy()" href="https://www.pizzahut.co.in/?utm_source=google&utm_medium=cpc&utm_campaign=iP_Devyani_Search_PR_Brand_Exact&utm_content=Brand&gclid=EAIaIQobChMI_9KalPey_AIViBsrCh2yfQQxEAAYAiAAEgJL5fD_BwE"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/pizzaHut.png" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Pizza Hut</p>
            </div></a>    
           <a Onclick="click_Easy()" href="https://www.oyorooms.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; ">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/oyo.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Oyo </p>
            </div></a>
          </div>
          <div class="row" style="display: flex; align-items: center; justify-content: space-between; padding: 5px; margin: -10px;">
         <a Onclick="click_Easy()" href="https://www.olacabs.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: whitesmoke; background-color: #622EA2; font-size: 25px;"></i> -->
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/ola.png" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">OLA</p>
            </div></a>   
          <a Onclick="click_Easy()" href="https://auth.uber.com/v2/?breeze_local_zone=phx6&next_url=https%3A%2F%2Fm.uber.com%2F&state=TCpe1F-JTEzRYPdroLxajWe1ELhUI3DUcKzAVO7fDzU%3D"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/uber.jpg" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Uber</p>
            </div></a>    
           <a Onclick="click_Easy()" href="https://in.bookmyshow.com/" style="position: relative; top: 6px;"> <div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/bms.png" style="border-radius: 5px;" width="40px">
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Book My<br />Show</p>
            </div></a>    
           <a Onclick="click_Easy()" href="https://www.delhimetrorail.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; ">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Switch/dmrc.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">DMRC</p>
            </div></a>
          </div>
        </div>

        <div class="row" style="display: flex; align-items: center; gap: 10px; justify-content: center; width: 95%; border-radius: 15px;  background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); margin-top: 3px; box-shadow: 0 0 5px rgba(0,0,0,0.8 ); flex-direction:column;">
          <div class="col-sm-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Investment</h5></div>
          <div class="row" style="display: flex; align-items: center; justify-content: space-between; padding: 5px; margin: -10px; ">
          <a Onclick="click_Easy()" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
 
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/investment/fd.jpg" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 5px;">Fixed<br />Deposit</p>
            </div></a>  
          <a Onclick="click_Easy()" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: whitesmoke; background-color: #622EA2; font-size: 25px;"></i> -->
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/investment/rd.jpg" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 5px;">Recurring<br />Deposit</p>
            </div></a>    
          <a Onclick="click_Easy()" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: whitesmoke; background-color: #622EA2; font-size: 25px;"></i> -->
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/investment/gl.png" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 5px;">Gold<br />Loan</p>
            </div></a>    
          <a Onclick="click_Easy()" style="width: 70px;"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
<%--              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/investment/" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 5px;">Flipkart </p>--%>
            </div></a>    
          </div>
        </div>

        <div class="row" style="margin-bottom: 5px; display: flex; align-items: center; gap: 10px; justify-content: center; width: 95%; border-radius: 15px;  background: linear-gradient(to left, rgb(145, 200, 240),rgb(248, 225, 160)); box-shadow: 0 0 5px rgba(0,0,0,0.8 ); flex-direction:column;">
          <div class="col-sm-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Subscriptions</h5></div>
          <div class="row" style="display: flex; align-items: center; justify-content: space-between; padding: 5px; margin: -10px; ">
          <a Onclick="click_Easy()" href="https://www.hotstar.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
 
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Subcription/disney-hotstar.png" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 5px;">Hotstar</p>
            </div></a>  
          <a Onclick="click_Easy()" href="https://tinder.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
              <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: whitesmoke; background-color: #622EA2; font-size: 25px;"></i> -->
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Subcription/tinder.png" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 5px;">Tinder</p>
            </div></a>    
          <a Onclick="click_Easy()" href="https://www.zee5.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: whitesmoke; background-color: #622EA2; font-size: 25px;"></i> -->
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Subcription/zee5.jpeg" style="width: 40px; border-radius: 5px;">
              <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 5px;">zee5</p>
            </div></a>    
          <a Onclick="click_Easy()" href="https://www.flipkart.com/"><div class="col-sm-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/Subcription/flipkart.png" width="40px" style="border-radius: 5px;" />
              <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 5px;">Flipkart </p>
            </div></a>    
          </div>
        </div>
             <div style="margin:-23px"></div>
        <div class="footer row" style="background-color: rgb(3, 100, 168); display: flex; align-items: center; justify-content: space-around;
         width: 100%; padding: 12px; position: fixed; top: calc(100vh - 11vh); height: 14vh;" >
     <a Onclick="click_Easy()" href="../Admin/BOS_API_Wise_Report.aspx"> <div class="col-sm-4" style="position: relative; bottom: 11px; cursor: pointer; display: flex;
      flex-direction: column; text-align: center; align-items: center; justify-content: center;">
      <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/footer/history.jpg" width="35px"/>
      <a href="#" style="text-decoration: none; color: white; font-size: 19px; font-weight: 500; position: relative; bottom: 8px;">History</a>
      </div></a>
     <a Onclick="click_Easy()" href="https://shopping.easytalk.services/"> <div class="col-sm-4" style="position: relative; bottom: 11px; display: flex; flex-direction: column; text-align: center;
          align-items: center; justify-content: center; cursor: pointer;">
      <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/footer/home.png" width="35px"/>
      <a href="#" style="text-decoration: none; color: white; font-size: 19px; font-weight: 500; position: relative; bottom: 8px;">Store</a>
      </div></a>
     <a Onclick="click_Easy()" href="../Admin/BOS_Raise_Request_Complaint.aspx"><div class="col-sm-4" style="position: relative; bottom: 11px; cursor: pointer; display: flex; flex-direction: column; text-align: center;
        align-items: center; justify-content: center;">
      <img src="../Easy Talk - Copy/Images/All-img/Nav-Bar/footer/support.png" width="35px"/>
      <a href="#" style="text-decoration: none; color: white; font-size: 19px; font-weight: 500; position: relative; bottom: 8px;">Support</a>
      </div></a> 
    </div>

    </center>
    
        </div>
       <script>
           $(".show-more-easy-talk").slice(0, 3).show();
           $(".show-more-easyTalk").on("click", function () {
               $(".show-more-easy-talk:hidden").slice(0, 12).show();
               $(".show-more-easyTalk").hide()
           });
           function seeLessEasyTalk() {
               let oneEasy = document.getElementById("oneEasyTalk");
               let twoEasy = document.getElementById("twoEasyTalk");
               let threeEasy = document.getElementById("threeEasyTalk");
               let fourEasy = document.getElementById("fourEasyTalk");
               let fiveEasy = document.getElementById("fiveEasyTalk");
               let sixEasy = document.getElementById("sixEasyTalk");
               let sevenEasy = document.getElementById("sevenEasyTalk");
               let eightEasy = document.getElementById("eightEasyTalk");
               let nineEasy = document.getElementById("nineEasyTalk");
               let tenEasy = document.getElementById("tenEasyTalk");
               oneEasy.style.display = "none";
               twoEasy.style.display = "none";
               threeEasy.style.display = "none";
               fourEasy.style.display = "none";
               fiveEasy.style.display = "none";
               sixEasy.style.display = "none";
               sevenEasy.style.display = "none";
               eightEasy.style.display = "none";
               nineEasy.style.display = "none";
               tenEasy.style.display = "flex";
           }
       </script>
        <script>
            var i = 0;
            var images = [];
            var time = 4000;

            images[0] = "../assets_easyTalk/Images/sliders/one.jpg";
            images[1] = "../assets_easyTalk/Images/sliders/two.jpg";
            images[2] = "../assets_easyTalk/Images/sliders/three.jpg";
            images[3] = "../assets_easyTalk/Images/sliders/four.jpg";
            images[4] = "../assets_easyTalk/Images/sliders/five.jpg";
            images[5] = "../assets_easyTalk/Images/sliders/six.jpg";
            images[6] = "../assets_easyTalk/Images/sliders/seven.jpg";
            images[7] = "../assets_easyTalk/Images/sliders/eight.jpg";
            images[8] = "../assets_easyTalk/Images/sliders/nine.jpg";
            images[9] = "../assets_easyTalk/Images/sliders/ten.jpg";



            function changeImg() {
                document.slide_EasyTalk.src = images[i];
                if (i < images.length - 1) {
                    i++;
                } else {
                    i = 0;
                }
                setTimeout("changeImg()", time);
            }
            window.onload = changeImg;
            $(".see-all-easy-talk").slice(0, 3).show();
            $(".drop-easy-talk").on("click", function () {
                $(".see-all-easy-talk:hidden").slice(0, 9).show();
                /* $(".loadMore").slice(0, 9).hide();*/
            })

            function click_Easy() {
                let loadingBar = document.getElementById("loading_EasyTalk");
                loadingBar.style.display = "flex";
                loadingBar.style.background = "linear-gradient( rgba(0,0,0,0.1),rgba(0,0,0,0.1))";
            }
        </script>
      
<%--Div Mobile View-EasyTalk - End--%>

     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
        <script>
            $(".show-more").slice(0, 3).show();
            $(".loadMore").on("click", function () {
                $(".show-more:hidden").slice(0, 12).show();
                /* $(".loadMore").slice(0, 9).hide();*/
            })

            let popUp = document.getElementById("popup");
            function clickOpen() {
                popUp.style.visibility = "visible";
            }
            function clickOff() {
                popUp.style.visibility = "hidden";
            }
        </script>

</div>
</div>
</div>
<script>
    var i = 0;
    var images = [];
    var time = 4000;

    images[0] = "../sliderimages/slidermainpage/one.png";
    images[1] = "../sliderimages/slidermainpage/two.png";
    images[2] = "../sliderimages/slidermainpage/three.png";



    function changeImg() {
        document.slidermainpage.src = images[i];
        if (i < images.length - 1) {
            i++;
        } else {
            i = 0;
        }
        setTimeout("changeImg()", time);
    }
    window.onload = changeImg;
</script>


<%--<script type="text/javascript">
    $(document).ready(function () {

        var isMobile = false;


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
