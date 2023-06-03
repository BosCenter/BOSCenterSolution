<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PayKre_Login.aspx.vb" Inherits="BOSCenter.PayKre_Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


<!-- Google Tag Manager -->
    <script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
            new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
        j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
        '../../../../../../www.googletagmanager.com/gtm5445.html?id='+i+dl;f.parentNode.insertBefore(j,f);
    })(window,document,'script','dataLayer','GTM-TAGCODE');</script>
    <!-- End Google Tag Manager -->
    <title>PayKre </title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta charset="UTF-8"/>
    <!-- External CSS libraries -->
    <link type="text/css" rel="stylesheet" href="assets/css/bootstrap.min.css"/>
    <link type="text/css" rel="stylesheet" href="assets/fonts/fo/font-awesome.min.3.dela"/>
    <link type="text/css" rel="stylesheet" href="assets/fonts/flaticon/flaticon.4.delaye"//>

    <!-- Favicon icon -->
    <link rel="shortcut icon" href="assets/img/favicon.ico" type="image/x-icon" />

    <!-- Google fonts -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800%7CPoppins:400,500,700,800,900%7CRoboto:100,300,400,400i,500,700"/>
    <link href="https://fonts.googleapis.com/css2?family=Jost:wght@300;400;500;600;700;800;900&amp;display=swap" rel="stylesheet"/>

    <!-- Custom Stylesheet -->
    <link type="text/css" rel="stylesheet" href="assets/css/style.css"/>
    <link rel="stylesheet" type="text/css" id="style_sheet" href="assets/css/skins/default.css"/>


     
        
               
        
      	<%--<script src="https://easytalkstechnologies.com/js/jquery-min.js"></script>--%>


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
      #btnForgotPassword:hover{
          text-decoration: underline;
          font-weight: 550;
         transition: 0.3s ease-in-out;
      }
     #lnkSignup:hover{
         text-decoration: underline;
         font-weight: 550;
         transition: 0.3s ease-in-out;
     }
</style>

		
</head>
<body>
          <form id="form2" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>
              
<!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-TAGCODE"
                  height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->
<div class="page_loader"></div>

<!-- Login 2 start -->
<div class="login-2">
    <div class="container">
        <div class="row login-box">

            <div class="col-lg-6 col-md-12 bg-img">

                <div class="info" style="text-align: center;">
<img src="assets_PayKre/Logo/one.jpg" alt="logo" width="300" style="text-align: center;"><br/>
                    <div class="info-text">
                        <div class="waviy">
                            <span style="--i:1">w</span>
                            <span style="--i:2">e</span>
                            <span style="--i:3">l</span>
                            <span style="--i:4">c</span>
                            <span style="--i:5">o</span>
                            <span style="--i:6">m</span>
                            <span style="--i:7">e</span>
                            <span class="color-yellow" style="--i:8">t</span>
                            <span class="color-yellow" style="--i:9">o</span><br />
                            <span style="--i:10">P</span>
                            <span style="--i:11">A</span>
                            <span style="--i:12">Y</span>
                            <span style="--i:13">K</span>
                            <span style="--i:14">R</span>
                            <span style="--i:14">E</span>
                        </div>
                        <p>
                            PayKre is an Online Money Solution do provide International Money Transfer, Domestic Money transfer, Cash withdrawal and many more banking services...
                        </p>
                        <div class="social-buttons">
                            <a href="#" class="social-button social-button-facebook">
                                <i class="fa fa-facebook"></i>
                            </a>
                            <a href="https://twitter.com/YaseenTechs" class="social-button social-button-twitter">
                                <i class="fa fa-twitter"></i>
                            </a>
                            <a href="https://www.instagram.com/wahabdba786/" class="social-button social-button-instagram">
                                <i class="fa fa-instagram"></i>
                            </a>
                            <a href="https://www.linkedin.com/in/yaseen-tech-services-yts-4517ba220/" class="social-button social-button-linkedin">
                                <i class="fa fa-linkedin"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-12 form-info">
                <div class="form-section">
                    <div class="logo clearfix">
                        <a href="login-2.html"  >
                            <img src="assets_PayKre/Logo/one.jpg" alt="logo">
                        </a>
                    </div>
                    <h3>Sign Into Your Account</h3>
                    <div class="login-inner-form">
                        
                            <div class="form-group form-box">
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
                                <i class="flaticon-mail-2"></i>
                            </div>
                            <div class="form-group form-box">
                            <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Enter Your User Name"></asp:TextBox>
                                <i class="flaticon-password"></i>
                            </div>
                            <div class="form-group form-box">
                                  <asp:TextBox method="post" ID="txtPassword" runat="server" class="form-control" 
                            placeholder="Enter Your Password" TextMode="Password" MaxLength="10"></asp:TextBox>
                                <i class="flaticon-password"></i>
                            </div>
                             <div class="form-group form-box">
                                 <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>
                              </div>
                            <div class="checkbox form-group form-box">
                               <%-- <div class="form-check checkbox-theme">
                                    <input class="form-check-input" type="checkbox" value="" id="rememberMe">
                                    <label class="form-check-label" for="rememberMe">Remember me</label>
                                </div>--%>
                                <a href="#myModal" class="pull-left" data-toggle="modal">
                        <asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Password / PIN</asp:LinkButton>
                        
                        </a>
                            </div>
                            <div class="form-group mb-0">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn-md btn-theme w-100" />
                                
                            </div>
                            <p class="text">Don't have an account? <asp:LinkButton ID="lnkSignup"  runat="server">Register here</asp:LinkButton></p>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Login 2 end -->

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

    </form>

		
	
<!-- External JS libraries -->
<script src="assets/js/jquery.min.js"></script>
<script src="assets/js/popper.min.js"></script>
<script src="assets/js/bootstrap.bundle.min.d.delaye"></script>
<!-- Custom JS Script -->

</body>
</html>

