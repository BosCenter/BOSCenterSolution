<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="vpaylogin.aspx.vb" Inherits="BOSCenter.vpaylogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login-V-Pay</title>
    <link rel="icon" href="./assets_v-pay/logo/favicon.png" />
    <style>
        *{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        .row{
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .left-section{ 
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            background: url("./assets_v-pay/moreimages/bgeight-one.png");
            background-repeat: no-repeat;
            background-position: center;
            background-size: cover;
        }
        .inner-left-section{
            gap: 25px;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            padding: 12px;
        }
        .inputext{
            border: none;
            outline: none;
            padding: 8px;
            border-radius: 8px;
            background: #0B0161;
            color: White;
        }
        .inputext::placeholder{
            color: white;
            font-weight: 500;
        }
        .inputpassword{
            border: none;
            outline: none;
            padding: 8px;
            border-radius: 8px;
            background: #0B0161;
            color: White;
        }
        .inputpassword::placeholder{
            color: white;
            font-weight: 500;
        }
        #loginbar{
            text-decoration: none;
            color: white;
            width: 50%;
            font-weight: 550;
            background: #0B0161;
            padding: 8px;
            border-radius: 8px;
            text-align: center;
            cursor: pointer;
            transition: 0.2s ease-in;
        }
        #loginbar:hover{
            background:#92ed5f;
            color: #0B0161;
        }
        #btnSubmit{
            text-decoration: none;
            color: white;
            width: 50%;
            font-weight: 550;
            background: #0B0161;
            padding: 12px;
            border-radius: 8px;
            text-align: center;
            cursor: pointer;
            transition: 0.2s ease-in;
            border: none;
            outline: none;
        }
        #btnSubmit:hover{
            background:#92ed5f;
            color: #0B0161;
            border: none;
            outline: none;
        }
        #forgotpass{
            text-decoration: none;
            width: 50%;
            font-weight: 500;
            color: #0B0161;
            padding: 8px;
            border-radius: 8px;
            text-align: center;
            cursor: pointer;
        }
        #forgotpass:hover{
            color: white;
            text-decoration: underline;
        }
        #orbar{
            position: relative;
        }
        #orbar:after{
            content: " ";
            display: inline;
            border: 1px solid black;
            width: 80px;
            height: 1px;
            position: absolute;
            top: 7px;
            right: 35px;
            background: black;
            border-radius: 30px;
        }
        #orbar:before{
            content: " ";
            display: inline;
            border: 1px solid black;
            width: 80px;
            height: 1px;
            position: absolute;
            top: 7px;
            left: 35px;
            background: black;
            border-radius: 30px;
        }
        .right-section{
            width: 100%;
            background: #193380;
            height: 162vh;
        }
        .thirdline{
            display: flex; 
            align-items: center; 
            justify-content: flex-start;
        }
        .customersupport{
            width: 90%;
            display: flex; 
            align-items: flex-start; 
            justify-content: flex-start;
            flex-direction: column;
            gap: 8px; 
            padding: 6px;
            box-shadow: 0 0 5px rgba(0,0,0,0.6); 
            border-radius: 8px;
            border: none;
        }
        @media screen and (max-width: 991px){
            .row{
                display: flex;
                align-items: center;
                justify-content: center;
                flex-direction: column;
            }
        }
        @media screen and (max-width: 335px){
            .thirdline{
                position: relative;
                right: 15px;
            }
            .customersupport{
                box-shadow: none;
            }
        }
    </style>
    <style>
        #lnkSignup{
            width: 160px;
            text-align: center;
            padding: 8px;
            border-radius: 6px;
            background: rgb(12,1,99);
            color: white;
        }
            #lnkSignup:hover {
                background: rgb(146,237,95);
                transition: 0.2s ease-in-out;
            }
            #btnForgotPassword{
                text-decoration: none;
                color: white;
            }
            #btnForgotPassword:hover{
                color: rgb(146,237,95);
                text-decoration: underline;
                transition: 0.3s ease-in-out;
            }
    </style>
    <style>
        *{
            padding: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12  left-section">
                    <div class="inner-left-section">
                         <img src="./assets_v-pay/logo/logo-one.png" style="width: 200px;"/>   
                        <h2 style="color: #0B0161; font-size: 28px; font-weight: 550; text-align: center;">WelCome to V-Pay Business</h2>
                        <p style="color: #0B0161; font-size: 18px; text-align: center; font-weight: 550;">Log in with username or mobile number</p>
                         <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" 
                            placeholder="Enter Company Code" Visible="false" ></asp:TextBox>
                        <asp:DropDownList ID="ddl_Login_For" runat="server"  class="inputext" style="font-size: 18px;">
                             <asp:ListItem>:: Select Login As ::</asp:ListItem>
                               <asp:ListItem>Master Distributor</asp:ListItem>
                               <asp:ListItem>Distributor</asp:ListItem>
                               <asp:ListItem>Retailer</asp:ListItem>
                               <asp:ListItem>Others</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="Username / Mobile Number"  class="inputext" 
                             style="font-size: 16px;"></asp:TextBox>
                       <%-- <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" class="inputpassword" TextMode="Password" 
                             MaxLength="10" style="font-size: 16px;"></asp:TextBox>--%>
                         <asp:TextBox ID="txtPassword" runat="server"  class="inputpassword" 
                            placeholder="Enter Your Password" TextMode="Password" MaxLength="10"></asp:TextBox>
                        <%--<input type="text" placeholder="Username / Mobile Number" class="inputext"/>
                        <input type="password" placeholder="Password" class="inputpassword"/>--%>
                        <%-- <a href="#" id="loginbar">Login 
                             &nbsp;&nbsp;<img src="./assets_v-pay/moreimages/rightarrow.png" style="width:20px; position:absolute;"/>
                         </a>--%>
                          <asp:Label ID="lblErrormsg" runat="server" Text="" Style="color:red;"  ></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" Style="color:red;"  ></asp:Label>
                         <asp:Label ID="Label1" runat="server"  CssClass="errorlabels" Visible="false" Style="color:red;"  ></asp:Label>
                          <asp:Label ID="lblpassword" runat="server"  CssClass="errorlabels" Visible="false" Style="color:red;"  ></asp:Label>
                        <div class="logincontent" style="width: 150px; position: relative;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Login" style="width: 150px;"/>
                            <img src="./assets_v-pay/moreimages/rightarrow.png" style="width:20px; position:absolute; right: 20px; top: 10px;"/>
                            </div>
                            <div class="col-12" style="width: 100%; display: flex; align-items: center; justify-content: center;">
                                <div class="rm" style="width: 100%; display: flex; align-items: center; justify-content: center;">
                                    <input type="checkbox" />
                                    <p style="font-size: 15px; ">&nbsp;&nbsp;Remember Me</p>
                                    <%--<asp:LinkButton ID="LinkButton1" runat="server" style="">Remember Me</asp:LinkButton>--%>
                                </div>
                            </div>
                        <p id="orbar">OR</p>
                        <%--<a href="#" id="forgotpass">Forgot Password?</a>--%>
                        <asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Password?</asp:LinkButton>
                            <asp:LinkButton ID="lnkSignup" runat="server" style="text-decoration: none;">Sign Up
                                &nbsp;&nbsp;<img src="./assets_v-pay/moreimages/rightarrow.png" style="width:20px; position:absolute;"/>
                            </asp:LinkButton>
                        <div class="downloadapp" style="display: flex; align-items: center; justify-content: center; width: 80%; border: 1px solid black; 
                       padding: 10px; border-radius: 8px; background: black;">
                            <p style="font-size: 14px; font-weight: bold; color: white;">Download The App Now &nbsp;&nbsp;<img src="./assets_v-pay/moreimages/playstore.png"
                                style="width: 20px; position:absolute; cursor: pointer;"/></p>
                         </div>
                        <div class="customersupport">
                            <div class="firstline" style="display: flex; align-items: center; justify-content: flex-start;">
                            <img src="./assets_v-pay/moreimages/wa.png" width="30px"/>
                            &nbsp;&nbsp;&nbsp;<p>7088756627</p>
                            </div>
                            <div class="secondline" style="display: flex; align-items: center; justify-content: flex-start;">
                            <img src="./assets_v-pay/moreimages/ci.png" width="30px"/>
                            &nbsp;&nbsp;&nbsp;<p>7088756626/25</p>
                             </div>
                            <div class="thirdline">
                            <img src="./assets_v-pay/moreimages/ei.png" width="30px"/>
                            &nbsp;&nbsp;&nbsp;<p >customercare@vpay.in</p>
                                </div>
                        </div>
                </div>
               </div>
                <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12 right-section">
                <div class="inner-right-section">
                        <div class="col-12" style="text-align: start; padding: 15px 25px; ">
                            <h2 style="color: white;">Making life Simple</h2>
                        </div>
                    <div class="col-12" style="text-align: center;">
                        <center style="margin-top: 5px;">
            <div class="container" style="width: 100%; display: flex; flex-direction: column; gap: 6px; background-color: #f5f5f5; margin-top: 5px; border-radius: 8px; padding: 20px; margin:0; padding: 0; box-sizing: border-box;">

                <center style="background: #193380;">
                <div class="row" style="width: 95%; border-radius: 8px; margin-top: 5px; background: #193380;">
                    <div class="col-12">
                   <img name="slidevpay" width="95%" height="95%" style="border-radius: 15px;"/>
                        </div>
        </div>
                    </center>
                </div>
            
            </center>
                    </div>
                    <div class="col-12" style="text-align: start; padding: 15px 25px; ">
                            <h2 style="color: white;">Current At V-Pay</h2>
                        </div>
                    <div class="col-12" style="text-align: center;">
                        <img src="./assets_v-pay/moreimages/rightimgone.png" style="width: 420px;"/>
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlForgotPassword" runat="server" style="display: none; background: white; color: black;">
   
<div  id="Div_Alert" tabindex="-1" role="dialog">
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModelpopupOTP" PopupControlID="pnlOtpPanel"  BackgroundCssClass="modalBackground"  CancelControlID="btnOTPCancel">
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
    <script>
        var i = 0;
        var images = [];
        var time = 4000;

        images[0] = "./assets_v-pay/moreimages/aeps/aepsone.png";
        images[1] = "./assets_v-pay/moreimages/aeps/panone.png";
        images[2] = "./assets_v-pay/moreimages/aeps/QR.png";



        function changeImg() {
            document.slidevpay.src = images[i];
            if (i < images.length - 1) {
                i++;
            } else {
                i = 0;
            }
            setTimeout("changeImg()", time);
        }
        window.onload = changeImg;
    </script>
</body>
</html>
