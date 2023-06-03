<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="publicSevaKendra.aspx.vb" Inherits="BOSCenter.publicSevaKendra" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Home-Public-Seva-Kendra</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.3/font/bootstrap-icons.css" />
    <style>
        #txtUserName{
            width: auto;
            text-align: center; 
            cursor: pointer; 
            background: #E8F0FE; 
            outline: none; 
            color: black;
            font-weight: 450; 
            cursor: pointer; 
            border-radius: 15px; 
            padding: 5px 8px; 
            position: relative;
            border: none;
            transition: 0.2s ease-in;
        }
        #txtUserName:hover{
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
        }
        #txtUserName:focus{
            border: 1px solid rgb(13,170,20);
        }
        #txtPassword{
            width: auto ; 
            text-align: center; 
            cursor: pointer; 
            border: none; 
            background: #E8F0FE; 
            outline: none; 
            color: black;
            font-weight: 450; 
            cursor: pointer; 
            border-radius: 15px;
            padding: 5px 8px; 
            transition: 0.2s ease-in;
        }
        #txtPassword:hover{
            box-shadow: 0 0 5px rgba(0,0,0,0.6);
        }
        #txtPassword:focus{
            border: 1px solid rgb(13,170,20);
        }
        #btnForgotPassword{
            text-decoration: none;
            color: black; 
            font-weight: 500;
            transition: 0.3s ease-in-out;
        }
        #btnForgotPassword:hover{
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
        #btnSubmit{
            background: rgb(13,170,20); 
            font-weight: 500; 
            border: none; 
            border-radius: 5px;
            padding: 6px 15px; 
            width: 100%;
            transition: 0.2s ease-in;
        }   
        #btnSubmit:hover{
            box-shadow: 0 0 5px rgba(0,0,0,0.8); 
            color: white;
            text-decoration: underline;
        }
        #lnkSignup{
            text-decoration: none;
            color: black;
            background: rgb(253,151,4);
            font-weight: 500;
            cursor: pointer;
            transition: 0.2s ease-in;
            border: none;
            padding: 6px 0;
            border-radius: 5px;
        }
        #lnkSignup:hover{
            text-decoration: underline;
            color: white;
            box-shadow: 0 0 5px rgba(0,0,0,0.8);
        }
        #ddl_Login_For{
            border: none;
            outline: none;
            padding: 5px 4px;
            border-radius: 15px;
            background: rgb(232,240,254);
            transition: 0.3s ease-in;
        }
        #ddl_Login_For:hover{
            box-shadow: 0 0 5px rgba(0,0,0,0.8);
        }
 



        @media screen and (max-width: 300px){
            #txtUserName{
                width: 100%;
            }
            #txtPassword{
                width: 100%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>
        <div class="container-fluid" style="text-align: center;">
             <div class="row" id="background-images" style="text-align: center; padding: 20px;">
                <center>
                    <div class="col-12" style="border: none; top: 70%; width: 95%; left: 50%; border-radius: 15px;
                    box-shadow: 0 0 5px rgba(0,0,0,0.8); border: none; background: rgb(253,151,4);">
                <div class="row" style="border-radius: 15px; background: rgb(253,151,4); border: none;">
                    <div class="col-12 col-xl-6 col-lg-6" style="border-top-left-radius: 15px;  
                    border: none; border-bottom-left-radius: 15px; border: none;">
                        <div class="row" style="border: none; padding: 15px; border-top-left-radius: 15px; border-bottom-left-radius: 15px; text-align: center;
                             display: flex; align-items: flex-start; justify-content: center; border: none;">
                            <div class="col-12" style="background: white; z-index: 15; border-radius: 5px;
                         display: flex; align-items: center; justify-content: center; gap: 5px; flex-direction: column; padding: 25px; position: relative; width: 95%;">
                            <div class="header">
                                <img src="./assets_publicSevaKendra/images/icon-1.png" class="img-fluid" style=" width:300px; padding: 15px;" />
                                <img src="./assets_publicSevaKendra/images/icon-2.png" class="img-fluid" style=" width:300px; padding: 15px;" />
                            </div>


                                <div class="hero row">
                                 <div class="col-12">
                                     <h5>Please login to your account</h5>
                                 </div>
                                <div class="col-12" style="margin: 8px 0; border: none;">
                                    <asp:TextBox ID="txtCompanyCode" runat="server" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddl_Login_For" runat="server">
                                            <asp:ListItem>&nbsp;&nbsp;&nbsp;&nbsp;::Select Login As::&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem>Master Distributer</asp:ListItem>
                                            <asp:ListItem>Distributer</asp:ListItem>
                                            <asp:ListItem>Retailer</asp:ListItem>
                                            <%--<asp:ListItem>Customer</asp:ListItem>--%>
                                            <asp:ListItem>Others</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                <div class="col-12" style="margin: 8px 0; border: none;">
                                    <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter Mobile Number"></asp:TextBox>
                                </div><br/>
                                <div class="col-12" style="margin: 8px 0; position: relative;">
                                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Your Password"  TextMode="Password" MaxLength="15"></asp:TextBox>
                                </div>
                                <div class="col-12" style="margin: 8px 0; border: none;">
                            <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>
                                </div>
                                <div class="checkbox" style="margin: 5px 0; display: flex; align-items: center; justify-content: center; gap: 5px;">
                               <%-- <input type="checkbox" /><p style="position: relative; top: 8px; font-weight: 500; color: rgb(255,150,1);">Remember Me</p>--%>
                
                        <label>
                            <asp:CheckBox ID="chkRememberMe" runat="server" /> Remember Me
                        </label>
                  
                            </div>
                            </div>
                                <div class="footer" style=" display: flex; flex-direction: column; gap: 15px; margin-bottom: 16px;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Login" />
                                    <asp:LinkButton ID="lnkSignup" runat="server">Sign Up</asp:LinkButton>
                                     <asp:LinkButton ID="btnForgotPassword" runat="server">Forgot Password / PIN</asp:LinkButton>
                                </div>
<%--                                <div class="row" style="text-align: center;">
                                    <div class="col-12" style="text-align: center;">
                                        <p style="font-size: 14px;"><i class="bi bi-geo-alt-fill"></i>
                                            Building No.87 Talawadi Road Front By Jio Office Dhariyawad District Pratapgarh Rajasthan 313605
                                        </p>
                                       <p style="font-size: 14px;"><i class="bi bi-telephone-fill"></i>
                                           +91 +918290347573</p>
                                        <p style="font-size: 14px;"><i class="bi bi-envelope"></i>racypaytechnology@gmail.com / support@racypay.in</p>
                                        </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>


                    <div class="col-xl-6 col-lg-6" style="padding: 15px;" id="hero-two"> 
                        <div class="row" style="background: none; border-radius: 5px; color: white; border: none; border-radius: 15px;">

                                                        <!-- Carousel -->
                          <div id="demo" class="carousel slide" data-bs-ride="carousel">

  <!-- Indicators/dots -->
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#demo" data-bs-slide-to="0" class="active"></button>
    <button type="button" data-bs-target="#demo" data-bs-slide-to="1"></button>
    <button type="button" data-bs-target="#demo" data-bs-slide-to="2"></button>
  </div>

  <!-- The slideshow/carousel -->
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img src="./assets_publicSevaKendra/images/slider/one.jpeg" alt="Los Angeles" class="d-block w-100" style="border-radius: 5px;"/>
    </div>
    <div class="carousel-item">
      <img src="./assets_publicSevaKendra/images/slider/six.jpeg" alt="Chicago" class="d-block w-100" style="border-radius: 5px;" />
    </div>
    <div class="carousel-item">
      <img src="./assets_publicSevaKendra/images/slider/seven.png" alt="New York" class="d-block w-100" style="border-radius: 5px;"/>
    </div>
  </div>

  <!-- Left and right controls/icons -->
  <button class="carousel-control-prev" type="button" data-bs-target="#demo" data-bs-slide="prev">
    <span class="carousel-control-prev-icon"></span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#demo" data-bs-slide="next">
    <span class="carousel-control-next-icon"></span>
  </button>
</div>
                         

                        </div>
                    </div>  
                    </div>
                </div>
                </center>
             </div>
        </div>

        <!----------------------------Forgot Popup start---------------------------------------------------------------------------------------------------------------------------------------->
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  

<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
<!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

<asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"  TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
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
 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"  TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel">
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
 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server"  TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel">
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" 
    integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
</body>
</html>


