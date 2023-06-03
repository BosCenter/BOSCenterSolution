<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="kmLogin.aspx.vb" Inherits="BOSCenter.kmLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="UTF-8" />
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, shrink-to-fit=no">
      
      <title>Kuber Money - Recharge & Bill Payment, Booking</title>
      <meta name="description" content="Kuber Money - Recharge & Bill Payment, Booking">
      <meta name="author" content="https://www.boscenter.in/">

         <link href="https://boscenter.in/css/bootstrap.min.css" rel="stylesheet" />
<link href="https://boscenter.in/css/bootstrap.css" rel="stylesheet" />
<link href="https://boscenter.in/css/style.css" rel="stylesheet" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />

<style type="text/css">
.modalBackground 
{
    height:100%;
    background-color:#504F4F;
    filter:alpha(opacity=70);
    opacity:0.7;
}
 </style>

 <style type="text/css">
.errorlabels
{  
  background-image: url("./images/cancel.png") ;
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #BD1711;
/*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
  display:inline-block;
  background-color:#FDE2DA;
  padding: 5px   5px 5px 25px ;
  border-radius: 3px;
  position : relative;
  border: 1px solid #D03F3F;
   margin-bottom:4px;
}
.errorlabels-sm
{  
  background-image: url("./images/cancel.png")  ;
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #BD1711;
/*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
  display:inline-block;
  background-color:#FDE2DA;
  padding:  5px   5px 5px 25px ;
  border-radius: 3px;
  position : relative;
  border: 1px solid #D03F3F;
   
}
.Successlabels_sm
{  
  background-image: url("./images/success.png") ;
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #5A0000;
 
  display:inline-block;
  background-color:rgb(197, 247, 177);
  padding:  5px   5px 5px 25px ;
    border-radius: 3px; 
   
  /*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
    border: 1px solid #09CE09;
  }
.Successlabels
{  
  background-image: url("./images/success.png") ;
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #5A0000;

  display:inline-block;
  background-color:rgb(197, 247, 177);
  padding: 5px   5px 5px 25px ;
    border-radius: 3px; 
    margin-bottom:4px;
  /*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
    border: 1px solid #09CE09;
  }
  
  .ValidationError
  {display: block;
  width: 100%;
  height: 34px;
  padding: 6px 12px;
  font-size: 14px;
  line-height: 1.42857143;
  color: #555;
  background-color: #fff;
  background-image: none;
  border: 1px solid red;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px red(0, 0, 0, .075);
          box-shadow: inset 0 1px 1px red(0, 0, 0, .075);
  -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
       -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
          transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
      }
      
      .ValidationError:focus {
  border-color: red;
  outline: 0;
  -webkit-box-shadow: inset 0 1px 1px red(0,0,0,.075), 0 0 8px red(102, 175, 233, .6);
          box-shadow: inset 0 1px 1px red(0,0,0,.075), 0 0 8px red(102, 175, 233, .6);
}
  
</style>

</head>
       <body class="container">
           <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>
        <div>
        </div>

    
   	<div class="row">
    <div class="col-sm-7 mar_top30 ">
    <center>
    <div id="myCarousel" class="carousel slide" data-ride="carousel"  >
  <!-- Indicators -->
  <ol class="carousel-indicators"  >
    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
    <li data-target="#myCarousel" data-slide-to="1"></li>
    <li data-target="#myCarousel" data-slide-to="2"></li>
    <li data-target="#myCarousel" data-slide-to="3"></li>
    <li data-target="#myCarousel" data-slide-to="4"></li>
    <li data-target="#myCarousel" data-slide-to="5"></li>
  </ol>

  <!-- Wrapper for slides -->
  <div class="carousel-inner">
      
      <div class="item active" style="width:98%">
      <img src="https://boscenter.in/images/1.jpg" alt="xxxxx">
      </div>
         <div class="item"  style="width:98%">
      <img src="https://boscenter.in/images/2.jpg" alt="xxxxx">
    </div>
    <div class="item"  style="width:98%">
      <img src="https://boscenter.in/images/3.jpg" alt="xxxxx">
    </div>
       <div class="item"  style="width:98%">
      <img src="https://boscenter.in/images/4.jpg" alt="xxxxx">
    </div>
     <div class="item"  style="width:98%">
      <img src="https://boscenter.in/images/5.jpg" alt="xxxxx">
    </div>
    <div class="item"  style="width:98%">
      <img src="https://boscenter.in/images/6.jpg" alt="xxxxx">
    </div>
  </div>

 <!-- Left and right controls -->
</div>
    </center>

    </div>
    	<div class="col-sm-5" >
           
            <div class="form-section mar_top30">

                <div>
                    <img src="https://boscenter.in/images/KUBERLOGO.jpg" alt="xxxxx" width="100%">
                </div>
                
              <div style="color:#29166F;margin-top:30px;">
                    <b>>>&nbsp; Customer Care No : &nbsp;  +91-9540730530 </b>
                </div>
                <div class="row">
                   <div class="col-sm-6" style="color:#29166F;margin-top:20px;">
                        <a href="https://agent.kvishmoney.com/CreateCustomer.aspx?admin=CMP1165"><img src="https://boscenter.in/images/kvMoney1.jpg" width="200px" /></a>
                    </div>
                    <div class="col-sm-5" style="color:#29166F;margin-top:20px; ">
                    <%--    <div class="form-section" style="height: 40px;">
                            <b> <a href="https://agent.kvishmoney.com/Default.aspx?admin=CMP1165" class="text-white"><i class="fa fa-sign-in"></i> Sign In</a></b>
                        </div>--%>
                    </div>
              
                    <div class="col-sm-12" >

<div class="form-section mar_top30 " style=" background-color:gainsboro;">
            	
                	<div class="form-group" >
                        <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" 
                            placeholder="Enter Company Code" Visible="false" ></asp:TextBox>
                        <asp:DropDownList ID="ddl_Login_For" runat="server" class='form-control'>
      <asp:ListItem>:: Select Login As ::</asp:ListItem>
      <asp:ListItem>Master Distributor</asp:ListItem>
      <asp:ListItem>Distributor</asp:ListItem>
      <asp:ListItem>Retailer</asp:ListItem>
      <asp:ListItem>Customer</asp:ListItem>
      <asp:ListItem>Others</asp:ListItem>
    </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Enter Your User Name"></asp:TextBox>
                        
                    </div>
                    <div class="form-group">
                    <asp:TextBox method="post" ID="txtPassword" runat="server" class="form-control" 
                            placeholder="Enter Your Password" TextMode="Password" MaxLength="10"></asp:TextBox>
                    
                    </div>
             
                   <div class="form-group">
                    

                    <div class="col-sm-12 top_mar12">
                         <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>
                    </div>
                  </div>

                    <div class="clearfix"></div>
                      <div class="form-group">
                   <%-- <div class="col-sm-4">
                      <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="chkRememberMe" runat="server" /> Remember Me
                        </label>
                      </div>
                    </div>--%>
                  
                      <div class="col-sm-12 top_mar12 pull-left">
                              <a href="#myModal" class="pull-left" data-toggle="modal">
                        <asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Password / PIN</asp:LinkButton>
                        <asp:LinkButton ID="lnkSignup" visible="false" runat="server">Sign Up</asp:LinkButton>
                        </a>
              	

                        
                  <div class="col-sm-6 pull-right">
 <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" /></div>
</div>
                           
                  </div>

            <div class="clearfix"></div>
     

                 
       
            </div>
                    </div>
                    

                </div>

              

                <div style="color:#0093DD ;margin-top:30px;">
                    <b> <u> Services Related Customer Care No </u></b>
                </div>

                <div style="color: #29166F; margin-top: 10px; margin-bottom: 50px">
                    <b>>>&nbsp;Recharge & Pan Card :  &nbsp;  +91-9540730531 </b><br />
                    <b>>> &nbsp;Money Transfer  : &nbsp;  +91-9540730540 </b><br />
                    <b>>> &nbsp;Bill Payment & Aeps  :  &nbsp; +91-9540730541 </b><br />
                    <b>>>&nbsp;Credit Request & KYC  :  &nbsp; +91-9540730571 </b><br />
                </div>

                <div style="color:#0093DD ;margin-top:10px;">
                    <a href="#"><img src="https://boscenter.in/images/google_play.png" height="30px" width="100px" /></a>
                </div>
                <div style="color: #0093DD; margin-top: 10px; margin-bottom: 10px">
                    <b>   <a href="#">Terms & Conditions</a>  &nbsp; | &nbsp;<a href="#">FAQ</a> &nbsp; | &nbsp;<a href="#">KYC Policy</a> &nbsp;</b>
                </div>

            </div>
            </div>
        
    </div>
   

           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
</asp:ModalPopupExtender>

  <asp:Panel ID="pnlForgotPassword" runat="server" style="display:none;"  >
   
<div  id="Div_Alert" tabindex="-1" role="dialog" >
   <div class="modal-dialog" role="document">
    <div class="modal-content">
    <div class="modal-header bg_blue">
  <h4 class="text-center pop_head_text" style="font-size:15px;" >Forgot Password / PIN !!!!
  <asp:Button ID="btnCancel" class="btn btn-primary pull-right" runat="server"  Text="X" />
  </h4>
        <%--<button type="button" class="close" data-dismiss="pnlPaymentReminder" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
 </div>
        
 
      <div class="modal-body">

         <div class="clearfix"></div>


        <div class="form-group top_mar15" align="center">
              <div class="col-sm-11 col-sm-offset-1" >
             <%-- <div class="col-sm-3" >
               <b> Enter Company Code </b>
           </div>--%>
            <div class="col-sm-5" >
           <asp:TextBox ID="txtCompanyCode_ForgotPass" runat="server" visible="false" placeholder="Company Code" CssClass="form-control" ></asp:TextBox>
               <asp:RadioButtonList ID="rdbForgotType" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" >
              <asp:ListItem>Password</asp:ListItem>
              <asp:ListItem>PIN</asp:ListItem>
              </asp:RadioButtonList>
           </div>
                       <%--  <div class="col-sm-3" >
               <b> Enter Login ID </b>
           </div>--%>

           <div class="col-sm-5" >
           <asp:TextBox ID="txtLoginId" runat="server" placeholder="Login Id" CssClass="form-control" ></asp:TextBox>
           
           </div>

              </div>
       
       
           
       </div>

         <div class="clearfix "></div>
          <div class="col-sm-12 col-sm-offset-1" >
              <asp:Label ID="lblinvalid" runat="server"></asp:Label>
             </div>
             <div class="clearfix "></div>
        <div class="form-group top_mar15" align="center">

              <label for="exampleInputEmail1"></label>
              <asp:Label ID="lblInfo" runat="server"></asp:Label>

              <asp:RadioButtonList ID="rdbType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" >
              <asp:ListItem>Email ID&amp;nbsp;&amp;nbsp;</asp:ListItem>
              <asp:ListItem>Mobile No</asp:ListItem>
              </asp:RadioButtonList>

              <label for="exampleInputEmail1"> 
                <asp:Label ID="lbl_Message" runat="server" ForeColor="Red"></asp:Label>
            </label>
                
              <div class="col-sm-11 col-sm-offset-1" >
              <div class="col-sm-9" >
              <asp:TextBox ID="txtEmail" runat="server" class="form-control top_mar15"  placeholder="Email"></asp:TextBox>
               <asp:Label ID="lbl_Forgot_CompanyCode" runat="server" Visible="false" ></asp:Label>
               <asp:Label ID="lbl_Forgot_DBName" runat="server" Visible="false" ></asp:Label>
              </div>
              <div class="col-sm-2" >
              <asp:Button ID="btnForgot_SubmitDetails" runat="server" Text="Submit" 
                  class="btn btn-primary pull-right top_mar15" />
                    <div class="clearfix "></div>
              </div>
              
              </div>
            
          </div>

      </div>
       <div class="clearfix "></div>
             <div class="modal-footer">
          
       </div>
                </div>
  </div>
    <div>
  </div>
</div>

</asp:Panel> 

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="btnModelpopupOTP" runat="server" Text="Button1" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModelpopupOTP" PopupControlID="pnlOtpPanel"  BackgroundCssClass="modalBackground"  CancelControlID="btnOTPCancel" >
</asp:ModalPopupExtender>

<asp:Panel ID="pnlOtpPanel" runat="server" style="display:none;">
    <div  id="Div1" tabindex="-1" role="dialog" >
   <div class="modal-dialog" role="document">
    <div class="modal-content">
    <div class="modal-header bg_blue">
  <h4 class="text-center pop_head_text" style="font-size:15px;" >Validate OTP dialog 
      !!!</h4> 
 </div>
        
 
      <div class="modal-body">

  

       <div class="clearfix"></div>

       

        
        <div class="form-group top_mar15">
              <div class="col-sm-11 col-sm-offset-1" >
                         <div class="col-sm-3" >
               <b> Enter OTP </b>
           </div>

           <div class="col-sm-6" >
           <asp:TextBox ID="txtOtpNo" runat="server" CssClass="form-control" MaxLength="4" ></asp:TextBox>
           
           </div>

              </div>
       
       
           
       </div>

         <div class="clearfix "></div>
         <div class="form-group top_mar15">
         <div class="col-sm-12 col-sm-offset-2" >
              <asp:Label ID="lblOTPError" runat="server"></asp:Label>
             </div> 
                </div> 
                <div class="clearfix "></div>
        <div class="form-group top_mar15" align="center">
              <div class="col-sm-9 col-sm-offset-3" >
              <div class="col-sm-8" >
              <asp:Button ID="btnOtpSubmit" runat="server" Text="Submit" 
                  class="btn btn-primary" />
                  &nbsp;<asp:Button ID="btnOTPCancel" runat="server" Text="Cancel" 
                  class="btn btn-primary" />
                    
             </div>
             
              
              </div>
            <div class="clearfix "></div>
          </div>

      </div>
       <div class="clearfix "></div>
             <div class="modal-footer">
          
       </div>
                </div>
  </div>
    <div>
  </div>
</div>
</asp:Panel>

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="Button1" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPanel"  BackgroundCssClass="modalBackground"   >
</asp:ModalPopupExtender>

<asp:Panel ID="InformationPanel" runat="server" Width="350px" style="display:none;"   >
<div  id="Div2" tabindex="-1" role="dialog" >
   <div class="modal-dialog" role="document">
    <div class="modal-content">
    <div class="modal-header bg_blue">
  <h4 class="text-center pop_head_text" style="font-size:15px;" >Information Dialog 
      !!!</h4> 
 </div>
        
 
      <div class="modal-body">

  

       <div class="clearfix"></div>

       

        
        <div class="form-group top_mar15">
              <div class="col-sm-10 col-sm-offset-1" >
                  <div class="col-sm-12" ><b><asp:Label ID="lblresult" ForeColor="Blue" runat="server"></asp:Label></b></div>
              </div>  
       </div>

       <div class="clearfix "></div>

       <center><div class="form-group top_mar15" align="center">
              <div class="col-sm-12" >
              <div class="col-sm-12" >
              <asp:Button ID="btnproceed" runat="server" Text="Proceed" 
                  class="btn btn-primary" />                   
             </div>
             </div>
          
       </div></center>

       <div class="clearfix "></div>

      </div>
       <div class="clearfix "></div>
             <div class="modal-footer">
          
       </div>
                </div>
  </div>
    <div>
  </div>
</div>
</asp:Panel>

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

</ContentTemplate>
</asp:UpdatePanel>
<!-----------------------------Forgot Popup End----------------------------------------------------------------------------------------------------------------------------------------->



   </form>
<script src="https://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="./js/bootstrap.min.js" type="text/javascript"></script>


   
           
   </body>



</html>
