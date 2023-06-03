<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Ezlypay_Login.aspx.vb" Inherits="BOSCenter.Ezlypay_Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login-Egyly-Pay</title>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" 
            integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous" />
    <link rel="icon" href="./assets_egzlypay/images/favicon/fav.png" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" 
        integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <%--Custom CSS--%>
    <style>
        *{
            margin: 0px;
            padding: 0px;
            box-sizing: border-box;
        }
        .container-fluid{
            background: url("./assets_egzlypay/images/background/background.png");
            height: 100vh;
            background-size: cover;
            background-position: center;
            display: flex;
            align-items: center;
            justify-content: center;
        }
       .logo-bar{
            width: 70px;
            background: white;
            border-radius: 50%;
            padding: 15px 15px;
        }
        .login-box{
            width: 80%;
            padding: 15px;
            border-radius: 5px;
            background: linear-gradient(rgba(0,0,0,0.4),rgba(0,0,0,0.8));
        }
        .left-side-bar{
            border: 1px solid whitesmoke;
            border-radius: 5px;
            box-shadow: 0 0 5px rgba(0,0,0,0.2);
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            gap: 15px;
            padding: 15px;
        }
        .left-side-bar h2{
            font-size: 23px;
            font-weight: 650;
            color: rgb(30,148,213);
        }
        .left-side-bar .select-bar{
            padding: 2px 26px;
            border-radius: 3px; 
            font-weight: 500;
            outline: none;
            border: none;
            font-size: 14px;
            text-align: center;
        }
        .textbar,.passwordbar{
            border: none;
            outline: none;
            padding: 2px 8px;
            border-radius: 3px;
            font-weight: 500;
            text-align: center;
            font-size: 14px;
        }
        .remembermebar{
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .rememberme{
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 4px;
        }
        .rememberme p{
            font-size: 13px;
            color: white;
            position: relative;
            top: 8px;
            font-weight: 500;
        }
        .forgotpassword .forgotpasswordBar{
            width: 100%;
            font-size: 13px;
            text-decoration: none;
            font-weight: 500;
        }
        .forgotpassword .forgotpasswordBar:hover{
            width: 100%;
            color: rgb(30,148,213);
            text-decoration: underline;
            transition: all 0.3s ease;
        }
        #mobilelogin,#lnkSignup,#btnSubmit{
            border: 1px solid white;
            padding: 4px 80px;
            margin-top: 15px;
            border-radius: 4px;
            text-decoration: none;
            color: rgb(30,148,213);
            font-weight: 500;
        }
        #btnSubmit{
            border: 1px solid white;
            padding: 4px 80px;
            margin-top: 15px;
            border-radius: 4px;
            text-decoration: none;
            color: rgb(30,148,213);
            font-weight: 500;
            background: none;
        }
        #mobilelogin:hover{
             background: rgb(30,148,213);
             color: white;
        }
        #lnkSignup:hover{
             background: rgb(30,148,213);
             color: white;
        }
        #btnSubmit:hover{
            background: rgb(30,148,213);
            color: white;
        }
        .right-side-bar{
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .rightsidebarimg{
            width: 800px;
        }
        #mobilelogin{
            display: none;
        }
        #faxmark{
            display: none;
        }
        #mobilereslogo{
            display: none;
        }
        #paraline{
            display: none;
        }
    </style>
    <%-- Media Query --%>
    <style>
        @media screen and (max-width: 1199px){
            .container-fluid{
                flex-direction: column;
            }
            #mobilereslogo{
                width: 80px;
                display: block;
                position: absolute;
                top: 15px;
                border: 1px solid white;
                border-radius: 50%;
                padding: 12px 15px;
                background: white;
            }
            .login-box{
                height: 90vh;
                background: url("./assets_egzlypay/images/background/right-img.png");
                background-repeat: no-repeat;   
                background-position: center; 
                width: 100%;
            }
            .left-side-bar{
                display: none;
            }
            .rightsidebarimg{
                display: none;
            }
            #mobilelogin,#lnkSignup,#btnSubmit{
                display: flex;
                border: 2px solid red;
                background: white;
                padding: 6px 25px;
                border-radius: 4px;
                font-size: 19px;
                font-weight: 500;
                text-decoration: none;
                background: red;
                color: white;
                cursor: pointer;
                box-shadow: 0 0 4px rgba(0,0,0,0.5);
            }
            #mobilelogin:hover{
                border: 2px solid white;
                background: white;
                color: red;
            }
            #lnkSignup:hover{
                border: 2px solid white;
                background: white;
                color: red;
            }
            #btnSubmit{
                border: 2px solid white;
                background: white;
                color: red;
            }
            #leftsidebarsection{
                background: white;
            }
            .logo-bar{
                border: 1px solid grey;
            }
            .login-box select,.textbar,.passwordbar{
                box-shadow: 0 0 4px rgba(0,0,0,0.5);
            }
            .rememberme p{
                color: black;
            }
            #mobilelogin{
                border: 1px solid grey;
            }
            #lnkSignup{
                border: 1px solid grey;
            }
            #btnSubmit{
                border: 1px solid grey;
            }
            #leftsidebarsection{
                position: relative;
            }
            #faxmark{
                display: block;
                position: absolute;
                 top: 20px;
                 right: 25px;
                 border: 1px solid grey;
                 padding: 6px 8px;
                 border-radius: 50%;
                 color: red;
            }
            #faxmark:hover{
                background: red;
                 color: white;
                 border: 1px solid red;
            }
            #paraline{
                display: block;
                position: absolute;
                bottom: 35px;
                text-align: center;
                left: 0px;
                color: white;
                font-weight: 500;
                font-size: 16px;
            }
        }
    </style>
    <style>
        @media screen and (max-width: 300px){
            .rememberme p{
                font-size: 10px;
            }
            .forgotpassword .forgotpasswordBar{
                font-size: 10px;
                position: relative;
                bottom: 4px; 
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <img src="./assets_egzlypay/images/logo/logo.png" id="mobilereslogo"/>
            <div class="row login-box">
                <div class="col-lg-6 left-side-bar" id="leftsidebarsection">
                    <i class="fa-solid fa-xmark" id="faxmark"></i>
                    <img src="./assets_egzlypay/images/logo/logo.png" class="logo-bar"/>
                    <h2>USER LOGIN</h2>
              <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" placeholder="Enter Company Code" Visible="false" ></asp:TextBox>
                    <asp:DropDownList ID="ddl_Login_For" runat="server" class="select-bar">
                        <asp:ListItem>:: Select Login As ::</asp:ListItem>
                        <asp:ListItem>Master Distributor</asp:ListItem>
                          <asp:ListItem>Distributor</asp:ListItem>
                          <asp:ListItem>Retailer</asp:ListItem>
                        <asp:ListItem>Others</asp:ListItem>
                    </asp:DropDownList>
                                  <asp:TextBox ID="txtUserName" class="textbar" runat="server" placeholder="Enter Your User Id"></asp:TextBox>
                            <asp:TextBox method="post" ID="txtPassword" class="passwordbar" runat="server"  placeholder="Enter Password" TextMode="Password" MaxLength="10"></asp:TextBox>
                                             <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>

                    <div class="row remembermebar">
                        <div class="col-6 rememberme">
                            <%-- <input type="checkbox" />--%>
                             <%--<p>Remember&nbsp;&nbsp;Me</p>--%>

                            <asp:CheckBox ID="chkRememberMe" runat="server" /><p>Remember&nbsp;&nbsp;Me</p>
                     
                        </div>
                    <div class="col-6 forgotpassword">
                       
                         <asp:LinkButton ID="btnForgotPassword" runat="server" class="forgotpasswordBar">Forgot&nbsp;&nbsp;Password?&nbsp;&nbsp;/PIN</asp:LinkButton>
                        </div>
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" />
                        <asp:LinkButton ID="lnkSignup" runat="server">Sign Up</asp:LinkButton>
                </div>
                <div class="col-lg-6 col-12 right-side-bar" id="rightsidebarone">
                    <img src="./assets_egzlypay/images/background/right-img.png" class="rightsidebarimg"/>
                    <a href="#" id="mobilelogin">Login</a>
                </div>
                    <p id="paraline">Pay your Bills, Mobile Recharge and Much more with us in Easy Steps.</p>
            </div>
        </div>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
</asp:ModalPopupExtender>

  <asp:Panel ID="pnlForgotPassword" runat="server" style="display:none; background: white; border-radius: 4px;">
   
<div  id="Div_Alert" tabindex="-1" role="dialog">
   <div class="modal-dialog" role="document">
    <div class="modal-content">
    <div class="modal-header bg_blue">
  <h4 class="text-center pop_head_text" style="font-size:15px; width: 100%; position: relative; padding: 12px; font-weight: bold;">Forgot Password / PIN !!!!
  <asp:Button ID="btnCancel" class="btn btn-primary pull-right" runat="server"  Text="X" style="position: absolute; right: 5px; 
              top: 4px; padding: 2px 8px;"/>
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

    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" runat="server" TargetControlID="Button1" PopupControlID="InformationPanel"  BackgroundCssClass="modalBackground" >
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
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
    <script>
        let mobileLogin = document.getElementById("mobilelogin");
        mobileLogin.addEventListener("click", function () {
            let rightSideBarOne = document.getElementById("rightsidebarone");
            let mobileLogin = document.getElementById("mobilelogin");
            let leftSideBarSection = document.getElementById("leftsidebarsection");
            leftSideBarSection.style.display = "flex";
            leftSideBarSection.style.position = "relative";
            leftSideBarSection.style.zIndex = "100";
            rightSideBarOne.style.background = "none";
            mobileLogin.style.display = "none";
        });
        let faxMark = document.getElementById("faxmark");
        faxMark.addEventListener("click", function () {
            let mobileLogin = document.getElementById("mobilelogin");
            let leftSideBarSection = document.getElementById("leftsidebarsection");
            leftSideBarSection.style.display = "none";
            mobileLogin.style.display = "block";
            
        })
    </script>
</body>
</html>
