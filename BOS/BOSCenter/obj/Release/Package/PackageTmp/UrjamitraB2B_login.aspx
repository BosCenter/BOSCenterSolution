<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UrjamitraB2B_login.aspx.vb" Inherits="BOSCenter.UrjamitraB2B_login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html lang="zxx">

<head  runat="server" >
    <!-- Google Tag Manager -->
    <script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
            new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
        j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
        '../../../../../../www.googletagmanager.com/gtm5445.html?id='+i+dl;f.parentNode.insertBefore(j,f);
    })(window,document,'script','dataLayer','GTM-TAGCODE');</script>
    <!-- End Google Tag Manager -->
    <title>URJA MITRA</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta charset="UTF-8">
    <!-- External CSS libraries -->
    <link type="text/css" rel="stylesheet" href="assets_urga/css/bootstrap.min.css">
    <link type="text/css" rel="stylesheet" href="assets_urga/fonts/fo/font-awesome.min.3.dela">
    <link type="text/css" rel="stylesheet" href="assets_urga/fonts/flaticon/flaticon.4.delaye">

    <!-- Favicon icon -->
    <link rel="shortcut icon" href="assets_urga/img/favicon.ico" type="image/x-icon">

    <!-- Google fonts -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800%7CPoppins:400,500,700,800,900%7CRoboto:100,300,400,400i,500,700">
    <link href="https://fonts.googleapis.com/css2?family=Jost:wght@300;400;500;600;700;800;900&amp;display=swap" rel="stylesheet">

    <!-- Custom Stylesheet -->
    <link type="text/css" rel="stylesheet" href="assets_urga/css/style.css">
    <link rel="stylesheet" type="text/css" id="style_sheet" href="assets_urga/css/skins/default.css">

    <style>
        #checkbox-section:hover{
            background: black;
            color: deepskyblue;
            transition: 0.4s ease-in-out;
        }
        #btnForgotPassword:hover{
            text-decoration: underline;
            color: white;
            font-weight:550;
            transition: 0.2s ease;
        }
        #lnkSignup:hover{
            text-decoration: underline;
            color: white;
            font-weight:550;
            transition: 0.2s ease;
        }
    </style>
     
</head>
<body id="top">
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-TAGCODE"
                height="0" width="0" style="display:none;visibility:hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
    <div class="page_loader"></div>

    <!-- Login 2 start -->
    <div class="login-2">
        <div class="container">
            <div class="row login-box">
                <div class="col-lg-6 col-md-12 bg-img">
                    <div class="info">
                        <img src="./assets_urga/img/white mitra.png" alt="logo" height="150px" width="175px" style="margin-left:45px; margin-bottom: 10px;">
                        <div class="info-text">

                            <div class="waviy">

                                <span style="--i:9">U</span>

                                <span style="--i:2">R</span>
                                <span style="--i:3">J</span>
                                <span style="--i:4">A</span>
                                &nbsp;&nbsp;&nbsp;


                                <span style="--i:9">M</span>

                                <span style="--i:11">I</span>
                                <span style="--i:12">T</span>
                                <span style="--i:13">R</span>
                                <span style="--i:14">A</span>
                            </div>


                            <p style="padding-left:30px;">Your Trust is our reward</p>

                        </div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-12 form-info">
                    <div class="form-section">
                        <img src="./assets_urga/img/white mitra.png" height="60px" weight="120px" style="margin-bottom: 10px;">

                        <h2>Sign Into Your Account</h2>
                        <div class="login-inner-form">
                            <form id="UrgaMitraLogin" runat="server" >
                           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


                                <div class="login-inner-form">
                        
                         <div class="form-group form-box">
                                
                        <asp:DropDownList ID="ddl_Login_For" runat="server" class='form-control'>
      <asp:ListItem>:: Select Login As ::</asp:ListItem>
      <asp:ListItem>Master Distributor</asp:ListItem>
      <asp:ListItem>Distributor</asp:ListItem>
      <asp:ListItem>Retailer</asp:ListItem>
      <%--<asp:ListItem>Customer</asp:ListItem>--%>
      <asp:ListItem>Others</asp:ListItem>
    </asp:DropDownList>
                                <i class="flaticon-mail-2"></i>
                            </div>
                            <div class="form-group form-box">
                            <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" 
                            placeholder="Enter Company Code" Visible="false" ></asp:TextBox>
                            <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Enter Your User ID"></asp:TextBox>
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
                                <%--<div class="checkbox" style=" text-align: center; cursor: pointer;">
              <asp:Button ID="" runat="server" Text="Login" class="btn-md btn-theme w-100"  style="background:none; border:1px solid white;"/> 
                                </div>--%>
                                       <div class="form-group mb-0">
              <asp:Button ID="btnSubmit" runat="server" Text="Login" class="btn-md btn-theme w-100"  style="background:none; border:1px solid white;"/> 
                                  
                                                                
                               
                            </div>
                            <div class="form-check checkbox-theme" style="width: 100%; display: none;">

                                      <%--  <input class="form-check-input" type="checkbox" value="" id="rememberMe">
                                       --%>
                                           <div class="checkbox form-group form-box" style="margin-top: 20px; display: flex; align-items: center; justify-content: center;
                                                 width: 100%;">
                                <div class="col-6" ><asp:CheckBox ID="ChkrememberMe" runat="server" Text="Remember me" class="form-check-label"/></div>
                                            <%-- <label class="form-check-label" for="rememberMe">Remember me</label>--%>
                                                           <%--  <a href="#myModal" class="pull-left" data-toggle="modal">--%>
             <div class="col-6">&nbsp &nbsp &nbsp&nbsp &nbsp &nbsp<%--<asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Password</asp:LinkButton>--%></div>
                        
                        
                            
                                    </div>
                                          </div>
                                <div class="row"style="margin-top: 15px;">
                                    <div class="col-6" style="display: flex;">
                                        <input type="checkbox"/>
                                        <p>Remember Me</p>
                                    </div>
                                    <div class="col-6"><asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Password</asp:LinkButton></div>
                                </div>
                           <%-- <div class="form-group mb-0">
              <asp:Button ID="btnSubmit" runat="server" Text="Login" class="btn-md btn-theme w-100"  style="background:none; border:1px solid white;"/> 
                                  
                                                                
                               
                            </div>--%>

                      <div class="checkbox" style="margin-top: 20px; text-align: center; cursor: pointer;">
                                    <p onclick="clickme()" id="checkbox-section" style="font-size: 17px; font-weight: 400; border: none; background: rgb(232,240,254); color: black; border-radius: 25px; padding: 10px; border: 1px solid white; display: none;">B2B Login</p>
                                </div>      

          <p class="text">Don't have an account?<asp:LinkButton ID="lnkSignup"  runat="server">Register here</asp:LinkButton></p> 
                        
                  
                </div>
            </div>
                                  
 <%--       </div>
    </div>
</div>--%>
<!-- Login 2 end -->



     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
</asp:ModalPopupExtender>

    <asp:Panel ID="pnlForgotPassword" runat="server" style="display: none;">
   
<div  id="Div_Alert" tabindex="-1" role="dialog" >
   <div class="modal-dialog" role="document">
    <div class="modal-content" style=" position: relative; color: black; width:auto;">
    <div class="modal-header" style="background: rgb(70,141,188); display: flex; justify-content: end;">
  <h4 class="text-center pop_head_text" style="font-size:15px; position: absolute; right: 30%;" >Forgot Password / PIN !!!!
  </h4>
        <asp:Button ID="btnCancel" class="btn btn-primary" runat="server"  Text="X" />
        <%--<button type="button" class="close" data-dismiss="pnlPaymentReminder" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
 </div>
        
 
      <div class="modal-body">

         <div class="clearfix"></div>


        <div class="form-group top_mar15" align="center">
              <div class="col-sm-11 col-sm-offset-1" style="display: flex; align-items: center; justify-content: center; gap: 4px;" >
             <%-- <div class="col-sm-3" >
               <b> Enter Company Code </b>
           </div>--%>
            <div class="col-sm-5">
           <asp:TextBox ID="txtCompanyCode_ForgotPass" runat="server" visible="false" placeholder="Company Code" CssClass="form-control" ></asp:TextBox>
               <asp:RadioButtonList ID="rdbForgotType" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" >
              <asp:ListItem style="font-weight: bold;">Password</asp:ListItem>
              <asp:ListItem style="font-weight: bold;">PIN</asp:ListItem>
              </asp:RadioButtonList>
           </div>
                       <%--  <div class="col-sm-3" >
               <b> Enter Login ID </b>
           </div>--%>

           <div class="col-sm-5">
           <asp:TextBox ID="txtLoginId" runat="server" placeholder="Login Id" style="padding: 2px 16px; outline: none; border: none; border-radius: 5px; cursor: pointer; box-shadow: 0 0 3px rgba(0,0,0,0.4);"></asp:TextBox>
           
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
            <div style="margin:5px;"></div>
              <asp:RadioButtonList ID="rdbType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" >
              <asp:ListItem style="font-weight: bold;">Email ID&amp;nbsp;&amp;nbsp;</asp:ListItem>
              <asp:ListItem style="font-weight: bold;">Mobile No</asp:ListItem>
              </asp:RadioButtonList>

              <label for="exampleInputEmail1"> 
                <asp:Label ID="lbl_Message" runat="server" ForeColor="Red"></asp:Label>
            </label>
                
            
              <div class="col-sm-11 col-sm-offset-1" >
              <div class="col-sm-9" >
              <asp:TextBox ID="txtEmail" runat="server"  placeholder="Email" style="padding: 2px 16px; outline: none; border: none; border-radius: 5px; cursor: pointer; box-shadow: 0 0 3px rgba(0,0,0,0.4);"></asp:TextBox>
               <asp:Label ID="lbl_Forgot_CompanyCode" runat="server" Visible="false" ></asp:Label>
               <asp:Label ID="lbl_Forgot_DBName" runat="server" Visible="false" ></asp:Label>
              </div>
                  <div style="margin:5px;"></div>
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



                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Login 2 end -->
    <!-- External JS libraries -->
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.bundle.min.d.delaye"></script>
    <!-- Custom JS Script -->
    <script>
        let contentBox = document.getElementById("ddl_Login_For");
        let onClickBar = document.getElementById("checkbox-section");
        function clickme() {
            contentBox.style.display = "block";
            onClickBar.style.display = "none";
        }
    </script>
</body>
</html>
