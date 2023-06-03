<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EasyTalk_Login.aspx.vb" Inherits="BOSCenter.EasyTalk_Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>    

<!DOCTYPE html>
<html lang="zxx">

<head  runat="server" >
    <!-- Google Tag Manager -->
    <script>(function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    '../../../../../../www.googletagmanager.com/gtm5445.html?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-TAGCODE');</script>
    <!-- End Google Tag Manager -->
    <title>EasyTalk</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta charset="UTF-8">
    <!-- External CSS libraries -->
    <link type="text/css" rel="stylesheet" href="assets_urga/css/bootstrap.min.css">
    <link type="text/css" rel="stylesheet" href="assets_urga/fonts/fo/font-awesome.min.3.dela">
    <link type="text/css" rel="stylesheet" href="assets_urga/fonts/flaticon/flaticon.4.delaye">

    <!-- Favicon icon -->
    <%--<link rel="shortcut icon" href="assets_urga/img/favicon.ico" type="image/x-icon">--%>

    <!-- Google fonts -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800%7CPoppins:400,500,700,800,900%7CRoboto:100,300,400,400i,500,700">
    <link href="https://fonts.googleapis.com/css2?family=Jost:wght@300;400;500;600;700;800;900&amp;display=swap" rel="stylesheet">

    <!-- Custom Stylesheet -->
    <link type="text/css" rel="stylesheet" href="assets_urga/css/style.css">
    <link rel="stylesheet" type="text/css" id="style_sheet" href="assets_urga/css/skins/default.css">
    <style>
        #txtUserName:hover{
            box-shadow: 0 0 5px rgba(0,0,0,0.8);
        }
        #txtPassword:hover{
            box-shadow: 0 0 5px rgba(0,0,0,0.8);
        }
        #btnSubmit{
            background:none; 
            border:1px solid white;
        }
        #btnSubmit:hover{
            background: rgb(77, 169, 235);
            box-shadow: 0 0 5px rgba(0,0,0,0.8);
            transition: 0.2s ease-in;
            border: none;
            font-weight: 600;
            letter-spacing: 1px;
            color: black;
        }
    </style>
     <style>
        #b2blogin{
            position: absolute; 
            right: 0; 
            top: 0;
            font-weight: 550; 
            color: white; 
            text-decoration: none;
            border: 1px solid white;
            border-radius: 5px;
            padding: 8px;
        }
        #b2blogin:hover{
            text-decoration: underline;
            background: #FFCF49;
            color: white;
            transition: 0.3s ease-in-out;
        }
    </style>
    <style>
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
    </style>
</head>
<body id="top">
<form id="EasyTalkLogin" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-TAGCODE"
                height="0" width="0" style="display:none;visibility:hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
    <div class="page_loader"></div>

    <!-- Login 2 start -->
    <div class="login-2" style="background: linear-gradient(rgb(77, 169, 235),rgb(255,176,2));">
        <div class="container" style="background: linear-gradient(rgb(77, 169, 235),rgb(255,176,2));">
            <div class="row login-box" style="background: linear-gradient(rgb(77, 169, 235),rgb(255,176,2));">
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
                <div class="col-12 form-info">
                    <div class="form-section">
                        <a href="https://www.easytalk.services/(S(z013bjd2qdbnxqpkx1we1x0j))/ETLogin.aspx" 
                            id="b2blogin" onclick="click_Easy()">B2B Login</a>
                        <div class="col-12" style="margin-top: 15px;">
                        <img src="./assets_easyTalk/images/icon/images/officelogo.png" class="img-fluid" style="width: 250px;"/>
                            </div>
                        <h3>Sign Into Your Account</h3>
                        <div class="login-inner-form">
                    <div class="login-inner-form">
                        
                         <div class="form-group form-box">
                                <i class="flaticon-mail-2"></i>
                            </div>
                            <div class="form-group form-box">
                            <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" 
                            placeholder="Enter Company Code" Visible="false" ></asp:TextBox>   
                          <asp:DropDownList ID="ddl_Login_For" runat="server" class='form-control' Visible="false">
      <%--<asp:ListItem>:: Select Login As ::</asp:ListItem>
      <asp:ListItem>Master Distributor</asp:ListItem>
      <asp:ListItem>Distributor</asp:ListItem>
      <asp:ListItem>Retailer</asp:ListItem>--%>
     <asp:ListItem>Customer</asp:ListItem>  
    </asp:DropDownList>

                            <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Enter Your Mobile Number"></asp:TextBox>
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
                         <asp:Label ID="Label1" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>
                          <asp:Label ID="lblpassword" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>
                              </div>
                            <div class="checkbox form-group form-box">

                                    <div class="form-check checkbox-theme" style="width: 100%;">

                                      <%--  <input class="form-check-input" type="checkbox" value="" id="rememberMe">
                                       --%>
                                           <div class="checkbox form-group form-box" style="display: flex; align-items: center; justify-content: center; flex-direction: column; width: 100%;">
                                <asp:CheckBox ID="ChkrememberMe" runat="server" Text="Remember me" class="form-check-label" />
                                            <%-- <label class="form-check-label" for="rememberMe">Remember me</label>--%>
                                                           <%--  <a href="#myModal" class="pull-left" data-toggle="modal">--%>
             &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp       <asp:LinkButton ID="btnForgotPassword"  runat="server">Forgot Password</asp:LinkButton>
                        
                        
                            
                                    </div>
                                          </div>
                            <div class="form-group mb-0">
              <asp:Button ID="btnSubmit" runat="server" Text="Login" class="btn-md btn-theme w-100"/> 
                               
                            </div>
          <p class="text" style="text-align: center; position: relative;">Don't have an account?<asp:LinkButton ID="lnkSignup"  runat="server" style="text-align: center; position: absolute; top: 30px; right: 0; width: 100%;">Register here</asp:LinkButton></p>     
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
   
<div  id="Div_Alert" tabindex="-1" role="dialog" style="border-radius: 5px;">
   <div class="modal-dialog" role="document">
    <div class="modal-content" style="padding: 2px; background: none;">
    <div class="modal-header bg_blue" style="padding: 15px; background: skyblue; display: flex; align-items: center; justify-content: center;">
  <h4 class="text-center pop_head_text" style="font-size:15px;" >Forgot Password / PIN !!!!
  <asp:Button ID="btnCancel" class="btn btn-primary pull-right" runat="server"  Text="X" style="position: absolute; right: 15px; top: 6px;"/>
  </h4>
        <%--<button type="button" class="close" data-dismiss="pnlPaymentReminder" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
 </div>
        
 
      <div class="modal-body" style="padding: 12px; background: grey; color: white; font-weight: 450;">

         <div class="clearfix"></div>


        <div class="form-group top_mar15" align="center">
              <div class="col-sm-11 col-sm-offset-1" >
             <%-- <div class="col-sm-3" >
               <b> Enter Company Code </b>
           </div>--%>
            <div class="col-sm-5" >
           <asp:TextBox ID="txtCompanyCode_ForgotPass" runat="server" visible="false" placeholder="Company Code" CssClass="form-control" ></asp:TextBox>
               <asp:RadioButtonList ID="rdbForgotType" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
              <asp:ListItem>Password</asp:ListItem>
              <asp:ListItem>PIN</asp:ListItem>
              </asp:RadioButtonList>
           </div>
                       <%--  <div class="col-sm-3" >
               <b> Enter Login ID </b>
           </div>--%>

           <div class="col-sm-5" >
           <asp:TextBox ID="txtLoginId" runat="server" placeholder="Login Id" CssClass="form-control" style="width: 80%;"></asp:TextBox>
           
           </div>

              </div>
       
       
           
       </div>

         <div class="clearfix "></div>
          <div class="col-sm-12 col-sm-offset-1">
              <asp:Label ID="lblinvalid" runat="server"></asp:Label>
             </div>
             <div class="clearfix "></div>
        <div class="form-group top_mar15" align="center">

              <label for="exampleInputEmail1"></label>
              <asp:Label ID="lblInfo" runat="server" style="padding: 10px;"></asp:Label>

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
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPanel"  BackgroundCssClass="modalBackground" CancelControlID="btnCancelInfo"  >
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

       <center> 
           <tr runat="server" id="lblInformationTR" >
    <td align="center" valign="middle" Height="60px" > 
        <asp:Label ID="lblInformation" runat="server"  Text="Are you Sure You Want to Proceed?"></asp:Label>  </td>
        
    </tr>

        </center>
         <div class="clearfix" style="margin-bottom:15px;"></div>

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
                    <asp:Button ID="btnCancelInfo" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />               
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



                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Login 2 end -->
 </form>
    <!-- External JS libraries -->
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.bundle.min.d.delaye"></script>
    <!-- Custom JS Script -->


    <script>
        function click_Easy() {
            let loadingBar = document.getElementById("loading_EasyTalk");
            loadingBar.style.display = "flex";
            loadingBar.style.background = "linear-gradient( rgba(0,0,0,0.1),rgba(0,0,0,0.1))";
        }
    </script>
</body>
</html>
