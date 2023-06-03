<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login_ClickToPay.aspx.vb" Inherits="BOSCenter.Login_ClickToPay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login_Home-Click2Pay</title>
    <link rel="stylesheet" href="./assets_click2pay/css/style.css" />
    <style>
        #btnForgotPassword{
    text-decoration: none;
    color: #284991;
}
#btnForgotPassword:hover {
    text-decoration: underline;
    transition: 0.2s ease-in-out;
}
#ddl_Login_For{
    border: 1px solid grey;
    padding: 5px;
    border-radius: 6px;
    background: #EAF9DD;
    outline: none;
    font-size: 14px;
    font-weight: 500;
}
#txtUserName{
    border: none;
    padding: 8px;
    border-radius: 0;
    background: #EAF9DD;
    outline: none;
    font-size: 14px;
    font-weight: 500;
    border-bottom: 1px solid green;
    border-bottom-left-radius: 0;
    border-bottom-right-radius: 0;
}
#txtPassword{
    border: none;
    padding: 8px;
    border-radius: 0;
    background: #EAF9DD;
    outline: none;
    font-size: 14px;
    font-weight: 500;
    border-bottom: 1px solid green;
    border-bottom-left-radius: 0;
    border-bottom-right-radius: 0;
}
#continue-bar{
    text-decoration: none;
    padding: 5px 20px;
    width: 100%;
    text-align: center;
    border-radius: 6px;
    background: rgb(120,170,76);
    color: white;
    font-size: 18px;
    letter-spacing: 1px;
    position: relative;
    outline: none;
    display: flex;
    border: none; 
    background: none;
}
#btnSubmit{
    text-decoration: none;
    padding: 10px 40px;
    width: 100%;
    text-align: center;
    border-radius: 6px;
    background: rgb(120,170,76);
    color: white;
    font-size: 16px;
    letter-spacing: 1px;
    outline: none;
    display: inline;
    border: none;
}
#btnSubmit:hover{
    background: #284992;
    transition: 0.3s ease-in-out;
}
#lnkSignup{
    text-decoration: none;
    padding: 10px 20px;
    width: 50%;
    text-align: center;
    border-radius: 6px;
    background: #284992;
    color: white;
    font-size: 16px;
    letter-spacing: 1px;
    position: relative;
}
#lnkSignup:hover {
        transition: 0.2s ease-in-out;
        background: rgb(120,170,76);
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-12">
                    <img src="./assets_click2pay/images/icon/icon-bgremove.png" style="width: 120px; position: absolute; top: 0; left: 80px;"/>
                </div>
            </div>
                <div class="row">
                    <div class="col-12 login-panel">
                        <div class="login-section">
                            <h2 id="headinglogin">Login</h2>
                            <p style="font-size: 25px; font-weight: 550; color: #284991;">Welcome At Click2 Pay</p>
                            <asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Your Password?</asp:LinkButton>
                             <asp:TextBox ID="txtCompanyCode" runat="server" Visible="false" ></asp:TextBox>
                            <asp:DropDownList ID="ddl_Login_For" runat="server" style="border: 1px solid green; padding:  5px 20px;">
                                <asp:ListItem>:: Select Login As::</asp:ListItem>
                                <asp:ListItem>Master Distributor</asp:ListItem>
                                <asp:ListItem>Distributor</asp:ListItem>
                                <asp:ListItem>Retailer</asp:ListItem>
                               <%-- <asp:ListItem>Customer</asp:ListItem>--%>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter Your Name"></asp:TextBox>
                            <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Your Password" TextMode="Password"  MaxLength="10"></asp:TextBox>
                            <div id="continue-bar" style="width: 167px; position: relative;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Continue"/>
                                <img src="./assets_click2pay/images/background/next.png" style="width: 21px; position: absolute; right: 38px; top: 13px; "/>
                                </div>
                            <asp:LinkButton ID="lnkSignup" runat="server">Sign Up &nbsp;<img src="./assets_click2pay/images/background/next.png" style="width: 21px; 
                              position: absolute; top: 8px;"/></asp:LinkButton>

                              <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>
                         <asp:Label ID="Label1" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>
                          <asp:Label ID="lblpassword" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>

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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPanel"  BackgroundCssClass="modalBackground" CancelControlID="btnCancelInfo">
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
</body>
</html>
