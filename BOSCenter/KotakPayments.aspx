<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="KotakPayments.aspx.vb" Inherits="BOSCenter.KotakPayments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login-Kotak-Payments</title>
    <style>
        *{
            margin: 0;
            padding: 0;
            box-sizing: border-box; 
        }
        .container-fluid{
            background: linear-gradient(rgba(0,0,0,0.01),rgba(0,0,0,0.02)), url("./assets_kotak-payments/images/bg.jpg");
            height: 100vh;
            background-repeat: no-repeat;
            background-position: center;
            background-size: cover;
            display: flex; 
            align-items: center; 
            justify-content: center;
            position: relative;
        }
        #imagebigscreen{
            width: 110px;
            position: absolute; 
            top: 25px; 
            right: 35px;
            cursor: pointer;
        }
        .container-fluid .row{
            width: 90%;
            display: flex; 
            align-items: center; 
            justify-content: center;
        }
        .container-fluid .row .col-12{ 
            display: flex; 
            align-items: center;
            justify-content: center; 
            flex-direction: column;
            gap: 18px;
            padding: 50px;
            border-radius: 13px;
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
            border: none; 
            outline: none;
            background: #CCCCCC;
            background: linear-gradient(rgba(0,0,0,0.05),rgba(0,0,0,0.06)),url("./assets_kotak-payments/images/bg\ -one.jpg");
            background-position: center;
            background-size: cover;
        }
        .container-fluid .row .col-12 h2{
            text-align: center; 
            color: rgb(114, 114, 114);
        }
        .container-fluid .row .col-12 input{
            border: none; 
            border-radius: 8px;
            padding: 10px; 
            font-size: 16px; 
            font-weight: 500; 
            outline: none; 
            background: white;
            cursor: pointer;
        }
        .container-fluid .row .col-12 input:hover{
            background: white;
            color: black;
        }
        #btnForgotPassword{
            text-decoration: none;  
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
            color: rgb(172, 14, 14);
        } 
        #btnForgotPassword:hover{
            text-decoration: underline;
            color: rgb(83, 83, 170);
            transition: 0.1s ease-in-out;
        }
        #lnkSignup{
            text-decoration: none;  
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
            color: rgb(172, 14, 14);
        }
        #lnkSignup:hover{
            text-decoration: underline;
            color: rgb(83, 83, 170);
            transition: 0.1s ease-in-out;
        }
        #btnSubmit{
            width: 80%;
            padding: 12px;
            border-radius: 8px;
            border: none;
            outline: none;
            font-size: 16px;
            font-weight: 500;
            cursor: pointer;
        }
        #btnSubmit:hover{
            background: crimson;
            color: white;
            transition: 0.2s ease;
        }
        #imgsmallscreen{
            width: 90px;
        }
        #ddl_Login_For{
            border: none; 
            border-radius: 8px;
            padding: 8px 25px; 
            font-size: 16px; 
            font-weight: 500; 
            outline: none; 
            background: white;
            cursor: pointer;
        }
        @media screen and (max-width: 991px){
            #imagebigscreen{
                display: none;
            }
        }
        @media screen and (min-width: 990px){
            #imgsmallscreen{
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
                <img id="imagebigscreen" src="./assets_kotak-payments/Logo/logo-two.png"/>
            <div class="row">
                 <div class="col-12">
                    <img id="imgsmallscreen" src="./assets_kotak-payments/Logo/logo-two.png"/>
                     <h2>WELCOME TO<br /><span style="font-size: 35px; font-weight: bold;">Kotak Payments</span></h2>
                     <asp:TextBox ID="txtCompanyCode" runat="server" Visible="false"></asp:TextBox>
                     <asp:DropDownList ID="ddl_Login_For" runat="server">
                         <asp:ListItem>:: Select Login As ::</asp:ListItem>
                         <asp:ListItem>Master Distributor</asp:ListItem>
                         <asp:ListItem>Distributor</asp:ListItem>
                         <asp:ListItem>Retailer</asp:ListItem>
                         <%--<asp:ListItem>Customer</asp:ListItem>--%>
                         <asp:ListItem>Others</asp:ListItem>
                     </asp:DropDownList>
                     <asp:TextBox runat="server" ID="txtUserName" class="form-control" placeholder="Enter Username"></asp:TextBox>
                     <asp:TextBox runat="server" placeholder="Enter Your Password" ID="txtPassword" class="form-control" TextMode="Password"></asp:TextBox>
                     <%--<input type="text" placeholder="Enter Username"/>--%>
                     <%--<input type="password" placeholder="Enter Your Password" />--%>
                     <asp:LinkButton ID="btnForgotPassword"  runat="server">Forgot Password</asp:LinkButton> 
                     <%--<a id="forgot-password" href="#">Forgot Password</a>--%>
                     <asp:Button ID="btnSubmit" runat="server" Text="Login" class="btn-md btn-theme w-100"/> 
                       <asp:Label ID="lblErrormsg" runat="server" Text="" style="color:red" ></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" style="color:red" ></asp:Label>     
                     <%--<a id="registerhere" href="#">Register Here</a>--%>
                     <asp:LinkButton ID="lnkSignup"  runat="server">Register Here</asp:LinkButton> 
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

