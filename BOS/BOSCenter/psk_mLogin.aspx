<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="psk_mLogin.aspx.vb" Inherits="BOSCenter.psk_mLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>HOME_PSA_LOGIN</title>
    <style>
        *{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        #txtUserName{
            cursor: pointer; 
            width: 90%;  
            text-align: center; 
            border: none; 
            font-size: 16px; 
            font-weight: 500; 
            box-shadow: 0 0 3px rgba(0,0,0,0.6);
            padding: 8px; 
            border-radius: 8px;
        }
        #txtUserName:hover{
            box-shadow: 0 0 3px rgba(0,0,0,0.6);
        }
        #txtPassword{
            cursor: pointer;
            width: 90%; 
            text-align: center; 
            border: none;
            font-size: 16px;
            font-weight: 500;
            box-shadow: 0 0 3px rgba(0,0,0,0.6);
            padding: 8px;
            border-radius: 8px;
        }
        #inputtwo:hover{
            box-shadow: 0 0 3px rgba(0,0,0,0.6);
        }
        #btnSubmit{
            width: 90%; 
            text-align: center;
            text-decoration: none; 
            padding: 8px;
            font-size: 16px; 
            font-weight: 500; 
            border: none; 
            outline: none; 
            border-radius: 8px;
            box-shadow: 0 0 2px rgba(0,0,0,0.6); 
            cursor: pointer;
        }
        #btnSubmit:active{
            background: orange;
            color: white;
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
        }
        #btnForgotPassword{
            width: 100%;
            text-align: center; 
            text-decoration: none;
            color: orange; 
            font-size: 18px; 
            cursor: pointer;
            border: none; 
            outline: none;
        }
        #btnForgotPassword:hover{
            text-decoration: underline;
            color: blueviolet;
        }
        #lnkSignup{
            width: 90%; 
            text-align: end;
            text-decoration: none; 
            color: orange; 
            cursor: pointer; 
            border: none; 
            outline: none;
        }
        #lnkSignup:hover{
            text-decoration: underline;
            color: blueviolet;
        }


        @media screen and (min-width: 598px){
        .container-fluid{
            display: none;
        }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="container-fluid" style="position: relative;">
            
            <div class="row" style="margin-top: 15px; width: 100%; display: flex; flex-direction: column; gap: 6px; background-color: white; margin-top: 5px;
              padding: 20px; margin:0; padding: 0; box-sizing: border-box;">

                <center>
                <div class="row" style="width: 98%; margin-top: 5px;">
               <div class="col-md-12">
                   <img name="PSA_slider" width="100%" height="auto" style="border-radius: 5px;"/>
               </div>
        </div>
                    </center>
                </div>
            
            <div class="row">
                <div class="col-12" style="text-align: center;">
                    <img src="./assets_publicSevaKendra/Images/bgremovericonpsa.png"  width="90px"/>
                </div>
            </div>
             
            <div class="row" style="display: flex; flex-direction: column; align-items: center; justify-content: center; gap: 15px;">
                
             <%--   <select style=" width: 90%; text-align: center; cursor: pointer; border: none; font-size: 16px; font-weight: 500; box-shadow: 0 0 3px rgba(0,0,0,0.6);
                    padding: 8px; border-radius: 8px; color: grey;">
                    <option>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;::Select Login As ::</option>
                                 <option>Master Distributor</option>
                                 <option>Distributor</option>
                                 <option>Retailer</option>
                                 <option>Others</option>
                </select>--%>
                <asp:TextBox ID="txtCompanyCode" runat="server" Visible="false"></asp:TextBox>
                <asp:DropDownList ID="ddl_Login_For" runat="server" style=" width: 90%; text-align: center; cursor: pointer; border: none; font-size: 16px; font-weight: 500; box-shadow: 0 0 3px rgba(0,0,0,0.6);
                    padding: 8px; border-radius: 8px; color: grey;" >
                    <asp:ListItem>::Select Login As::</asp:ListItem>
                    <asp:ListItem>Master Distributor</asp:ListItem>
                     <asp:ListItem>Distributor</asp:ListItem>
                     <asp:ListItem>Retailer</asp:ListItem>
                     <asp:ListItem>Others</asp:ListItem>
                </asp:DropDownList>

                <%--<div class="userid" style=" width: 100%; text-align: center;">
                    <img src="./assets_publicSevaKendra/Images/input/person.png" width="30px" style="position: absolute;"/>
                    <input type="text" id="inputone" placeholder="USERID / MOBILE"/ >
                </div>--%>
                <asp:TextBox ID="txtUserName" runat="server" placeholder="USERID / MOBILE" style=" width: 90%; text-align: center;" CssClass=""></asp:TextBox>

               <%-- <div class="password" style="width: 100%; text-align: center;" >
                    <img src="./assets_publicSevaKendra/Images/input/lock.png" width="30px" style="position: absolute;"/>
                    <img src="./assets_publicSevaKendra/Images/input/eye.png" width="30px" style="position: absolute; right: 40px;"/>
                <input type="text" id="inputtwo" placeholder="PASSWORD"/>
                    </div>--%>
                <asp:TextBox ID="txtPassword" runat="server" placeholder="PASSWORD" style="width: 90%; text-align: center;" CssClass="" TextMode="Password" >
                     
                </asp:TextBox>

                <asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Password?</asp:LinkButton>
                      <div class="col-12" style="margin: 8px 0; border: none;">
                            <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>
                                </div>
                <%--<button id="signin-bar">SIGN IN</button>--%>
                <asp:Button ID="btnSubmit" runat="server" Text="SIGN IN" />
                 <asp:LinkButton ID="lnkSignup" runat="server">REGISTER NOW</asp:LinkButton>
            </div>

            <div class="footer" style="text-align: center; position: absolute; width: 100%; top: 90vh;">
             <%--   <p style="font-weight: 550;">Helpline: 8010405278</p>--%>
            </div>
        </div>


<!----------------------------Forgot Popup start---------------------------------------------------------------------------------------------------------------------------------------->
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel">
    </asp:ModalPopupExtender>
  <asp:Panel ID="pnlForgotPassword" runat="server" style="background: none;">
   
<div  id="Div_Alert" tabindex="-1" role="dialog" style=" background: black; border-radius: 15px; color: white;">
   <div class="modal-dialog" role="document">
    <div class="modal-content">
    <div class="modal-header bg_blue" style="display: flex; align-items: center; justify-content: center; padding: 20px; position: relative; background: skyblue;
        border-top-right-radius: 15px;  border-top-left-radius: 15px;">
  <h4 class="text-center pop_head_text" style="font-size:15px;" >Forgot Password / PIN !!!!
  <asp:Button ID="btnCancel" class="btn btn-primary pull-right" runat="server"  Text="X" style="position: absolute; right: 10px; top: 3px;"/>
  </h4>
        <%--<button type="button" class="close" data-dismiss="pnlPaymentReminder" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
 </div>
        
 
      <div class="modal-body" style="border: 2px solid black; padding: 20px;">

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
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel">
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
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel">
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
    <script>
        var i = 0;
        var images = [];
        var time = 3000;

        images[0] = "./assets_publicSevaKendra/Images/new-one/one.png";
        images[1] = "./assets_publicSevaKendra/Images/new-one/two.png";
        images[2] = "./assets_publicSevaKendra/Images/new-one/three.jpeg";



        function changeImg_ozzy() {
            document.PSA_slider.src = images[i];
            if (i < images.length - 1) {
                i++;
            } else {
                i = 0;
            }
            setTimeout("changeImg_ozzy()", time);
        }
        window.onload = changeImg_ozzy;
    </script>
</body>
</html>
