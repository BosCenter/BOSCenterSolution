<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login_instape.aspx.vb" Inherits="BOSCenter.Login_instape" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Insta-pe</title>
    <link rel="icon" href="./assets_instape/images/logo/favicon.png" />
    <style>
        *{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        .container-fluid{
            background: url("./assets_instape/images/Demo/bgone.jpg");
            background-position: center;
            background-size: cover;
             padding: 55px 0;
            display: flex;
            align-items: center; 
            justify-content: center;
        }
        .row{
            width: 80vw;
            display: flex;
            align-items: center;
            justify-content: space-around;
        }
        #rightsection{
            display: flex; 
            align-items: center; 
            justify-content: center; 
            flex-direction: column; 
            position: relative;
        }
        #rowOne{
            background: linear-gradient(rgba(95,37,158),rgba(122, 60, 189));
            padding: 25px;
            border-radius: 15px;
        }
        #columnone{
            border-radius: 15px; 
            box-shadow: 0 0 5px rgba(0,0,0,0.5); 
            padding: 10px 50px; 
            display: flex; 
            align-items: center;
            justify-content: center; 
            flex-direction: column; 
            gap: 15px; 
            background: white;
        }
    </style>
    <style>
        #txtUserName{
            border: none; 
            outline: none;
            border-bottom: 1px solid #652AA5;
        }
        #txtPassword{
            border: none; 
            outline: none;
            border-bottom: 1px solid #652AA5;
        }
        #clicktochange{
            width: 35px; 
            position: absolute;
            top: 0;
            right: 15px; 
            cursor: pointer;
        }
        #firstOne{
            width: 500px;
        }
    </style>
    <style>
        #btnSubmit{
            padding: 7px 65px;
            border: none;
            box-shadow: 0 0 4px rgba(0,0,0,0.5);
            border-radius: 5px;
            background: #662BA6;
            color: white;
            cursor: pointer;
            font-weight: 550;
            outline: none;
        }
        #btnSubmit:hover{
            background: rgb(84, 3, 173);
            transition: 0.3s ease-in;
        }
        #lnkSignup{
            padding: 7px 58px;
            border: none;
            box-shadow: 0 0 4px rgba(0,0,0,0.5);
            border-radius: 5px;
            background: #662BA6;
            color: white;
            cursor: pointer;
            font-weight: 500;
            outline: none;
            text-decoration: none;
        }
        #lnkSignup:hover{
            background: rgb(84, 3, 173);
            transition: 0.3s ease-in;
        }
    </style>
    <style>
        @media screen and (max-width: 991px){
            #rightsection{
                display: none;
            }
        }
        @media screen and (max-width: 400px){
            #rowOne{
                width: 95vw;
            }
        }
        @media screen and (max-width: 333px){
            #rowOne{
                background: linear-gradient(rgba(95,37,158),rgba(122, 60, 189));
                padding: 15px;
                border-radius: 15px;
                width: 100%;
            }
            #columnone{ 
            padding: 10px 30px; 
        }
        }
    </style>
    <style>
        #buttonadmin{
            padding: 7px 58px;
            border: none;
            box-shadow: 0 0 4px rgba(0,0,0,0.5);
            border-radius: 5px;
            background: #662BA6;
            color: white;
            cursor: pointer;
            font-weight: 500;
            outline: none;
            text-decoration: none;
        }
        #buttonadmin:hover{
            background: rgb(84, 3, 173);
            transition: 0.3s ease-in;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row" id="rowOne">
                    <div class="col-6" id="columnone">
                         <img src="./assets_instape/images/logo/logo-one.png" style="width: 200px;"/>
                        <p style="text-align: center; font-weight: bold; font-size: 19px;">Welcome Back, Please Login to<br /> Your Account</p>
                        <asp:TextBox ID="txtCompanyCode" runat="server" Visible="false" ></asp:TextBox>
                           <asp:DropDownList ID="ddl_Login_For" runat="server" style="border: 1px solid green; padding:  5px 20px; display: none;">
                               <%--   <asp:ListItem>:: Select Login As::</asp:ListItem>
                              <asp:ListItem>Master Distributor</asp:ListItem>
                                <asp:ListItem>Distributor</asp:ListItem>
                                <asp:ListItem>Retailer</asp:ListItem>--%>
                                <asp:ListItem>Customer</asp:ListItem>
                                
                            </asp:DropDownList>
                        <a  href="InstaAdmin_Loginpage.aspx" id="buttonadmin">Admin Login</a>
                        <div class="input-text" id="input-textone">
                            <p style="font-weight: 550; margin: 2px 0; color: rgba(95,37,158);">Username/ Mobile No.</p> 
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-pass">
                            <p style="font-weight: 550; margin: 2px 0; color: rgba(95,37,158);" >Password</p>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"  MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="rememberme" style="display: flex; align-items: center; justify-content: center; gap: 8px;">
                            <input type="checkbox" style="border: none; outline: none;"/>
                            <p style="font-size: 16px; font-weight: 500;">Remember Me</p>
                        </div>
                         <asp:LinkButton ID="btnForgotPassword" runat="server" style="text-decoration: none; 
                        color: rgba(95,37,158); outline: none; border: none;">Forgot Password?</asp:LinkButton>
                                 <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" style="color: red; font-weight: 500;"></asp:Label>
                         <asp:Label ID="Label1" runat="server"  CssClass="errorlabels" Visible="false" style="color: red; font-weight: 500;"></asp:Label>
                          <asp:Label ID="lblpassword" runat="server"  CssClass="errorlabels" Visible="false" style="color: red; font-weight: 500;"></asp:Label>
                        <asp:Button ID="btnSubmit" runat="server" Text="Login"/>
                        <p style="margin: -5px;">- OR -</p>
                         <asp:LinkButton ID="lnkSignup" runat="server">Sign Up</asp:LinkButton>
                    </div>
                    <div class="col-6" id="rightsection"> 
                        <img src="./assets_instape/images/Demo/gifremove/one.gif" id="firstOne"/>
                        <h2 style="text-align: center; color: white;">You Can Login</h2>
                    </div>
                </div>
        </div>


         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel"></ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlForgotPassword" runat="server" style="display:none;"  >
   
<div  id="Div_Alert" tabindex="-1" role="dialog" style="border-radius: 5px;">
   <div class="modal-dialog" role="document">
    <div class="modal-content" style="padding: 2px; background;">
    <div class="modal-header bg_blue" style="padding: 15px; background: skyblue; display: flex; align-items: center; justify-content: center;
            border-top-right-radius: 5px; border-top-left-radius: 5px;">
  <h4 class="text-center pop_head_text" style="font-size:15px;" >Forgot Password / PIN !!!!
  <asp:Button ID="btnCancel" class="btn btn-primary pull-right" runat="server"  Text="X" style="position: absolute; right: 15px; top: 8px; padding: 5px 8px; background: red; 
   color: white; border: none; border-radius: 5px; cursor: pointer;"/>
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModelpopupOTP" PopupControlID="pnlOtpPanel"  BackgroundCssClass="modalBackground"  CancelControlID="btnOTPCancel"></ajaxToolkit:ModalPopupExtender>

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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPanel"  BackgroundCssClass="modalBackground" CancelControlID="btnCancelInfo"></ajaxToolkit:ModalPopupExtender>

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
</body>
</html>
