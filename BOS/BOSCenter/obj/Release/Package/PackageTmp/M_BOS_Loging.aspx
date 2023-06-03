<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="M_BOS_Loging.aspx.vb" Inherits="BOSCenter.M_BOS_Loging" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Bos-Login</title>
     <link rel="icon" href="../images/Bos-Login-form/icon.png"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.3/font/bootstrap-icons.css" />
    <style>
        #background-images{
            background: url("./images/Bos-Login-form/bos-login-bg-1.png");
            background-position: center;
            background-size: cover;
            height: 100vh;
        }
        #log-in::before{
            content: " ";
            display: inline-block;
            position: absolute;
            top: 37px;
            width: 70px;
            height: 4px;
            border-radius: 25px;
            background:  linear-gradient(to right, rgb(30,153,233),rgb(255,164,61));
        }
        #sign-up{
            cursor: pointer;
            position: relative; 
            font-weight: bold;
            color: white;
            text-decoration: none;
            transition: 0.3s ease-in-out;
        }
        #sign-up:hover{
            text-decoration: underline;
        }
        #ddl_Login_For{
            width: auto;
            margin: 10px 0;
            text-align: center; 
            cursor: pointer; 
            background: #E8F0FE; 
            outline: none; 
            color: black;
            font-weight: 500; 
            cursor: pointer; 
            border-radius: 15px; 
            padding: 5px 8px; 
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
            position: relative;
            border: none;
        }
        #txtUserName{
            width: auto;
            text-align: center; 
            cursor: pointer; 
            background: #E8F0FE; 
            outline: none; 
            color: black;
            font-weight: 500; 
            cursor: pointer; 
            border-radius: 15px; 
            padding: 5px 8px; 
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
            position: relative;
            border: none;
        }
        #txtPassword{
            width: auto ; 
            text-align: center; 
            cursor: pointer; 
            border: none; 
            background: #E8F0FE; 
            outline: none; 
            color: black;
            font-weight: 500; 
            cursor: pointer; 
            border-radius: 15px;
            padding: 5px 8px; 
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
        }
        #forgot-password{
            text-decoration: none;
            color: white; 
            font-weight: 500;
            transition: 0.3s ease-in-out;
        }
        #forgot-password:hover{
            text-decoration: underline;
        }
        #aboutUs{
            color: rgb(30,153,233);
            font-size: 20px; 
            font-weight: bold; 
            margin-top: 10px; 
            font-family: 'Times New Roman', Times, serif; 
            cursor: pointer;
            transition: 0.3s ease-in-out;
        }
        #aboutUs:hover{
            color: blue;
        }
        #productssection{
            color: rgb(30,153,233);
            font-size: 20px; 
            font-weight: bold; 
            margin-top: 10px; 
            font-family: 'Times New Roman', Times, serif; 
            cursor: pointer;
            transition: 0.3s ease-in-out;
        }
        #productssection:hover{
            color: blue;
        }
        #ourservices{
            color: rgb(255,164,61); 
            font-size: 20px; 
            font-weight: bold; 
            font-family: 'Times New Roman', Times, serif;
            cursor: pointer;
            transition: 0.3s ease-in-out;
        }
        #ourservices:hover{
            color: blue;
        }
        #bubbles span{
            width: 30px; 
            height: 30px;
            background: rgb(30,153,233); 
            margin: 0 4px; 
            border-radius: 50%; 
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
            animation: animate 15s linear infinite;
            animation-duration: calc(185s / var(--i));
        }
        #bubbles span:nth-child(even){
            background: rgb(255,164,61);
            box-shadow: 0 0 10px white,
            0 0 50px white,
            0 0 100px  white;
        }
        @keyframes animate{
            0%{
                transform: translateY(100vh) scale(0);
            }
            100%{
                transform: translateY(-10vh) scale(1);
            }
        }

        @media screen and (max-width: 991px){
            #hero-two{
                display: none;
            }
        }
        @media screen and (max-width: 303px){
            #input-email{
                width: 120%;
                border: none;
                outline: none;
                position: relative;
                right: 16px;
            }
            #input-password{
                width: 120%;
                border: none;
                outline: none;
                position: relative;
                right: 16px;
            }
            #select-section{
                width: 130%;
                border: none;
                outline: none;
                position: relative;
                right: 21px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
             <div class="row" id="background-images" style="position: relative;">
                <div class="col-12" style="border: none;  position: absolute; top: 50%; width: 80%; left: 50%; transform: translate(-50%,-50%); border-radius: 15px; box-shadow: 0 0 5px rgba(0,0,0,0.8); 
                background: white">
                <div class="row" style="border-radius: 15px; background: linear-gradient(to right, rgba(253, 251, 251, 0.8),rgba(0,0,0,0.8));">
                    <div class="col-12 col-xl-6 col-lg-6" style="border-top-left-radius: 15px;  
                    border: none; border-bottom-left-radius: 15px;">
                        <div class="row" style="border: none; padding: 15px; border-top-left-radius: 15px; border-bottom-left-radius: 15px; height: 100%; text-align: center; display: flex; align-items: flex-start; justify-content: center; height: 90vh;">
                            <!-- <div class="col-12" style="border: none;">
                            <img src="./images/Bos-Login-form/icon-1.png" class="img-fluid" width="200px" style="padding: 15px;">
                            </div> -->
                            <div class="col-12" style="border-top: 3px solid rgb(30,153,233); border-bottom: 3px solid rgb(255,164,61); border-radius: 15px; background: linear-gradient(rgba(56, 56, 56, 0.8),rgba(0,0,0,0.8)); position: relative; z-index: 15;
                         display: flex; align-items: center; justify-content: center; gap: 15px; flex-direction: column; padding: 50px; position: relative; height: 85vh;">
                            <div class="bubbles" id="bubbles" style="overflow: hidden; display: flex; border: none; position: absolute; z-index: -5; width: 100%; height: 100%;">
                            <span style="--i:1;"></span>
                            <span style="--i:75;"></span>
                            <span style="--i:84;"></span>
                            <span style="--i:13;"></span>
                            <span style="--i:17;"></span>
                            <span style="--i:37;"></span>
                            <span style="--i:44;"></span>
                            <span style="--i:68;"></span>
                            <span style="--i:63;"></span>
                            <span style="--i:10;"></span>
                            <span style="--i:53;"></span>
                            <span style="--i:12;"></span>
                            <span style="--i:99;"></span>
                            <span style="--i:28;"></span>
                            <span style="--i:19;"></span>
                            <span style="--i:42;"></span>
                            <span style="--i:18;"></span>
                            <span style="--i:6;"></span>
                            <span style="--i:78;"></span>
                            <span style="--i:55;"></span>
                            <span style="--i:96;"></span>
                            <span style="--i:55;"></span>
                            <span style="--i:25;"></span>
                            <span style="--i:98;"></span>
                            <span style="--i:55;"></span>
                            <span style="--i:36;"></span>
                            <span style="--i:49;"></span>
                            <span style="--i:85;"></span>
                            <span style="--i:22;"></span>
                            <span style="--i:38;"></span>
                            </div>
                            <div class="header">
                                <img src="./images/Bos-Login-form/icon-1.png" class="img-fluid" width="250px" style="padding: 15px;" />
                            </div>
                                <div class="container-one row" style="display: flex; gap: 20px;">
                                        <div class="col-12">
                                    <h3 id="log-in" style="cursor: pointer; position: relative; border: none; font-weight: bold; color: white;">Login</h3>
                                </div>
                                </div>

                                <div class="hero row">
                                    <div class="col-12">
                                         <asp:Label ID="lblCompanyName" runat="server" Text="Login Panel" Visible="false"></asp:Label>
                                    </div>
                                <div  class="col-12">
                                 <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" Visible="false" ></asp:TextBox>
                                    <asp:DropDownList ID="ddl_Login_For" runat="server" >
                                       <asp:ListItem>::select Login As::</asp:ListItem>
                                        <asp:ListItem>Master Distributor</asp:ListItem>
                                        <asp:ListItem>Distributor</asp:ListItem>
                                        <asp:ListItem>Retailer</asp:ListItem>
                                        <asp:ListItem>Customer</asp:ListItem>
                                       <asp:ListItem>Others</asp:ListItem>
                                    </asp:DropDownList>
                                
                                </div>
                                <div class="col-12" style="margin: 8px 0; border: none;">
                                    <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter Mobile Number" ></asp:TextBox>
                                <%--<input id="input-email" type="text" placeholder="Enter Mobile Number" />
                                <i id="inputemail" class="bi bi-person-circle"  style="display: none; position: absolute; top: 5px; left: 0;"></i>--%>
                                </div><br/>
                                <div class="col-12" style="margin: 8px 0; position: relative;">
                                 <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                                 <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" 
                                   CssClass="errorlabels" Visible="false" ></asp:Label>
                                    </div>
                                <div class="col-12" style="margin: 8px 0; position: relative;">
                                    <asp:TextBox ID="txtPassword" runat="server"  placeholder="Enter Your Password" TextMode="Password" MaxLength="15"></asp:TextBox>
                               <%-- <i id="inputpassword" class="bi bi-eye" style="display: none; position: absolute; top: 5px; color: black; left: 0;"></i>
                                <input id="input-password" type="password" placeholder="Enter Your Password" />--%>

                                </div>
                                <div class="checkbox-one" style="margin: 5px 0; display: flex; align-items: center; justify-content: center; gap: 5px;">
                                <%--<input type="checkbox" /><p style="position: relative; top: 8px; font-weight: 500; color: white;">Remember Me</p>--%>
                                     <asp:CheckBox ID="ChkrememberMe" runat="server" Text="Remember me" class="form-check-label" Style="color:white;" />
                            </div>
                            </div>
                                <div class="footer" style=" display: flex; flex-direction: column; gap: 15px; margin-bottom: 16px;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Login" class="bg-danger text-white"
                                        style="font-weight: 500; border: none; box-shadow: 0 0 5px rgba(0,0,0,0.8);
                                         border-radius: 5px; padding: 6px 15px; width: 100%;"/>
                                    <asp:LinkButton ID="lnkSignup"  runat="server">Sign Up</asp:LinkButton> 
                                    <asp:LinkButton ID="btnForgotPassword"  runat="server">Forgot Password</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-xl-6 col-lg-6" style="border: none; border-top-right-radius: 15px; border-bottom-right-radius: 15px; padding: 15px;" id="hero-two"> 
                        <div class="row" style="background: black; color: white; height: 85vh; overflow: auto; border: 3px solid rgb(30,153,233); border-radius: 15px;">
                            <div class="col=12" style="text-align: end; position: relative; border: none;">
                                <a id="contactUs" href="#" style="text-decoration: none; color: white; padding: 3px 8px; border: 2px solid white; font-weight: 500; position: absolute; right: 20px; top: 30px; z-index: 10; border-radius: 4px; background: skyblue;">Contact Us</a>
                            </div> 
                            <div class="col=12" style="text-align: center; padding: 15px; border: none; position: relative;">
                                <img id="slider-img" src="./images/Bos-Login-form/logo-one.png" class="d-block w-100" alt="..." style="border-radius: 15px; margin-top: 20px; display: flex;" />
                                <div class="row" style="display: flex; ">
                                    <div class="col-4">
                                    <h3 onclick="aboutussection()" id="aboutUs"><a href="https://bos.center/about-us/index.html">About Us</a></h3>
                                    </div>
                                    <div class="col-4">
                                    <h3 id="ourservices" onclick="ourservicesclick()" style="position: relative; top: 10px;">Our Services</h3>
                                    </div>
                                    <div class="col-4">
                                    <h3 id="productssection"><a href="https://bos.center/products/index.html">Products</a></h3>
                                    </div>
                                </div>
                                    <p id="parafirst" style="border: none; text-align: justify; margin-top: 15px; font-size: 14px; font-weight: 500; color: white; letter-spacing: 1px;">
                                    <strong style="font-size: 18px;">Business Online Solution</strong> is one of the leading, most reputed <u>flight, hotels, Bus, Mobile Recharges, Bill Payment, Pan card, API provider</u> that empowers Travel and Banking business <span style="color: rgb(30,153,233);"> <u>by providing travels technology solutions to tour operators, travel agents, online travel agencies, banking, and travel management companies all around the world.</u></span> We have always delivered client satisfaction. Today we can vouch on our excellent services and quality deliverance and the transparency in every part of the business with the clients<a onclick="readMe()" id="readMe" href="#" style=" text-decoration: none;"><span style="font-size: 18px;">R</span>ead More...</a>
                                        <p id="paraone" style="letter-spacing: 1px; font-weight: 500; display: none; color: white    ; font-size: 14px; text-align: justify; border: none;">We offer an API based end to end private label / white label travel portal turnkey solution that is tailored to suit the complete needs of an online travel agency. On white label solution allows you to build your online travel agency completely under your Brand without any redirection. Confirmation voucher will be issued with your logo on your Website itself. This solution allows travel agents to use our Fare and technology to create their own travel Brand. You can earn commissions and more profits by using our Innovative solutions for Flight, Hotels, Bus, Pan card, Mobile Recharges, Bill Payment, AEPS and many more.</p>
                                        <p id="paratwo" style="letter-spacing: 1px; font-weight: 500; display: none; border: none;  color: white; font-size: 14px; text-align: justify;">We deliver all API Integration Solution-One of the most reliable and most trusted Global Distribution System (GDS), that helps the travel portals to get the best service providers by their side to broadcast their effective services at the portal and which in turn will bring huge website traffic as well. We integrate API which will enable you to reach the maximum number of travelers and business partners.<br><br><br>                                            

                                            We are the most trusted and reliable providers of Multi-Recharge Systems that allow the consumers to do all kinds of booking like hotel, bus, railways; complete all types of recharges and bill payments like Electricity Bill, Mobile Recharge, DTH Recharge, Data Card Recharge etc. for all major operators (Airtel, Idea, Vodafone, Aircel, Reliance, TATA, Videocon, BSNL, MTNL, MTS, Electricity bill payment of all states) in India, along with availing 24x7 Online Transaction Services and Banking Facilities.<br><br><br>
                                            
                                            It is also a place, where we encourage establishment of B2B along with B2C platforms and offer dependable and trusted channels for Retailers and Distributors for all major operators.</p>
                                    </p>
                                    <div  id="ourservicessec" class="row" style="display: flex; align-items: center; justify-content: center; display: none;">
                                        <h2 style="margin-top: 10px;">We provide You</h2>
                                        <div class="col-4 col-lg-6" style="margin-top: 10px;">
                                        <img src="./images/Bos-Login-form/our-products/m-recharge.png" class="img-fluid" width="150px" style="border-radius: 8px;" />
                                        <p>Recharge API</p>
                                        </div>
                                        <div class="col-4 col-lg-6" style="margin-top: 10px;">
                                        <img src="./images/Bos-Login-form/our-products/billpay.png" class="img-fluid" width="150px" style="border-radius: 8px;" />
                                        <p>Bill Payment (BBPS)</p>
                                        </div>
                                        <div class="col-4 col-lg-6" style="margin-top: 10px;">
                                        <img src="./images/Bos-Login-form/our-products/gst.jpg" class="img-fluid" width="150px" style="border-radius: 8px;" />
                                        <p>GST Registration</p>
                                        </div>
                                        <div class="col-4 col-lg-6" style="margin-top: 10px;">
                                        <img src="./images/Bos-Login-form/our-products/mobile-app.jpg" class="img-fluid" width="150px" style="border-radius: 8px;" />
                                        <p>Mobile Application</p>
                                        </div>
                                        <div class="col-4 col-lg-6" style="margin-top: 10px;">
                                        <img src="./images/Bos-Login-form/our-products/tour.jpg" class="img-fluid" width="150px" style="border-radius: 8px;" />
                                        <p>Holiday Tour Packages</p>
                                        </div>
                                        <div class="col-12">
                                            <a href="https://bos.center/services/index.html">and much more.....</a>
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>  
                    </div>
                </div>
             </div>
        </div>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </ajaxToolkit:ModalPopupExtender>

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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModelpopupOTP" PopupControlID="pnlOtpPanel"  BackgroundCssClass="modalBackground"  CancelControlID="btnOTPCancel" >
    </ajaxToolkit:ModalPopupExtender>

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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPanel"  BackgroundCssClass="modalBackground" CancelControlID="btnCancelInfo" >
    </ajaxToolkit:ModalPopupExtender>
  
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" 
    integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
    <script>
        function readMe() {
            let paragraphOne = document.getElementById("paraone");
            let paragraphTwo = document.getElementById("paratwo");
            let readMe = document.getElementById("readMe");
            let contactUs = document.getElementById("contactUs");
            contactUs.style.display = "none";
            paragraphOne.style.display = "block";
            paragraphTwo.style.display = "block";
            readMe.style.display = "none";
        }
        function aboutussection() {
            let contactUs = document.getElementById("contactUs");
            let ourservices = document.getElementById("ourservicessec");
            let sliderImg = document.getElementById("slider-img");
            let paraFirst = document.getElementById("parafirst");
            paraFirst.style.display = "block";
            sliderImg.style.visibility = "visible";
            contactUs.style.display = "flex";
            ourservices.style.display = "none";
        }
        function ourservicesclick() {
            let paragraphOne = document.getElementById("paraone");
            let paragraphTwo = document.getElementById("paratwo");
            let ourservices = document.getElementById("ourservicessec");
            let contactUs = document.getElementById("contactUs");
            let paraFirst = document.getElementById("parafirst");
            let sliderImg = document.getElementById("slider-img");
            paraFirst.style.display = "none";
            ourservices.style.display = "flex";
            paragraphOne.style.display = "none";
            paragraphTwo.style.display = "none";
            contactUs.style.display = "none";
            sliderImg.style.display = "none";
        }
    </script>
</body>
</html>
