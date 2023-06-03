<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OzzyPeLogin.aspx.vb" Inherits="BOSCenter.OzzyPeLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

    <html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <script src="../js/jQuery-2.1.4.min.js" type="text/javascript" ></script>
  <script src="../js/jquery-1.8.2.js" type="text/javascript"></script>
    <%--    <script src="../js/JSstop.js" type="text/javascript"></script>--%>


<link href="https://netdna.bootstrapcdn.com/font-awesome/3.2.1/css/font-awesome.css" rel="stylesheet"/>
<link href="https://netdna.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.css" rel="stylesheet"/>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDnvSDxvacLn65kW08blk7Cq7a3EEvgKGQ&sensor=false"></script>




<meta charset="utf-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>  </title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.5 -->
  <link id="lnkCSS" runat="server" rel="stylesheet" href="~/css/admin.css" type="text/css" />
    
  <!-- Font Awesome -->
 <link href="~/css/font-awesome.css" rel="stylesheet" />
    <%--  <link id="lnkpuple" runat="server" href="~/css/admin_Purple.css" rel="stylesheet" type="text/css" />--%>
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
  <!-- Theme style -->
  <link id="lnkred" runat="server"  rel="stylesheet" href="~/css/admin_blue.css" />
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  
  <link rel="stylesheet" href="~/css/bootstrap.css" />
  <link href="~/css/style.css" rel="stylesheet" />

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
  <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.min.js"></script>

  <style type ="text/css">
  
  /* enable absolute positioning */
.inner-addon {
  position: relative;
}

/* style glyph */
.inner-addon .glyphicon {
  position: absolute;
  padding: 10px;
  pointer-events: none;
}

/* align glyph */
.left-addon .glyphicon  { left:  0px;}
.right-addon .glyphicon { right: 0px;}

/* add padding  */
.left-addon input  { padding-left:  30px; }
.right-addon input { padding-right: 30px; }



  
  </style>

  <style type="text/css">
.modalBackground 
{
    height:100%;
    background-color:#504F4F;
    filter:alpha(opacity=70);
    opacity:0.7;
}

.divWaiting{
position: absolute;
background-color: #FAFAFA;
z-index: 700 !important;
opacity: 0.9;
overflow: hidden;
text-align: center; top: 0; left: 0;
height: 100%;
width: 100%;
padding-top:10%;
} 

.divpopupWaiting{
position: absolute;
background-color: #FAFAFA;
z-index: 700 !important;
opacity: 0.7;
overflow: hidden;
text-align: center; top: 0; left: 0;
height: auto;
width: 100%;

} 

  </style>

  <style type="text/css">
.grid-view-themeclass tr th
{
	background: #ededed;
     box-shadow: 1px 2px 5px #ccc;
}

/*.grid-view-themeclass tr:nth-of-type(2)
{
	background: #ededed;
     box-shadow: 1px 2px 5px #ccc;
}*/

.grid-view-themeclass tr th
{
   font-size:14px;
}

.grid-view-themeclass tr td
{
font-size:13px;
}

.modalBackground 
{
    height:100%;
    background-color:#504F4F;
    filter:alpha(opacity=70);
    opacity:0.7;
}

.divWaiting{
position: fixed;
background-color: #FAFAFA;
z-index: 2900 !important;
opacity: 0.9;
overflow: hidden;
text-align: center; top: 0; left: 0;
height: 700px;
width: 100%;
padding-top:10%;
} 

  </style>

  <style type="text/css">
.errorlabels
{  
  background-image: url("../images/cancel.png") ; 
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #BD1711;
/*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
  display:inline-block;
  background-color:#FDE2DA;
  padding: 5px   5px 5px 25px ;
  border-radius: 3px;
  position : relative;
  border: 1px solid #D03F3F;
   margin-bottom:4px;
}
.errorlabels-sm
{  
  background-image: url("../images/cancel.png")  ;
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #BD1711;
/*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
  display:inline-block;
  background-color:#FDE2DA;
  padding:  5px   5px 5px 25px ;
  border-radius: 3px;
  position : relative;
  border: 1px solid #D03F3F;
   
}
.Successlabels_sm
{  
  background-image: url("../images/success.png") ;
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #5A0000;
 
  display:inline-block;
  background-color:rgb(197, 247, 177);
  padding:  5px   5px 5px 25px ;
    border-radius: 3px; 
   
  /*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
    border: 1px solid #09CE09;
  }
.Successlabels
{  
  background-image: url("../images/success.png") ;
  background-repeat: no-repeat;
  background-position: left center ;
  background-size: 20px 20px;
  color: #5A0000;

  display:inline-block;
  background-color:rgb(197, 247, 177);
  padding: 5px   5px 5px 25px ;
    border-radius: 3px; 
    margin-bottom:4px;
  /*box-shadow:1px 3px 10px rgba(0,0,0,.65);*/
    border: 1px solid #09CE09;
  }
  
  .ValidationError
  {display: block;
  width: 100%;
  height: 34px;
  padding: 6px 12px;
  font-size: 14px;
  line-height: 1.42857143;
  color: #555;
  background-color: #fff;
  background-image: none;
  border: 1px solid red;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px red(0, 0, 0, .075);
          box-shadow: inset 0 1px 1px red(0, 0, 0, .075);
  -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
       -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
          transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
      }
      
      .ValidationError:focus {
  border-color: red;
  outline: 0;
  -webkit-box-shadow: inset 0 1px 1px red(0,0,0,.075), 0 0 8px red(102, 175, 233, .6);
          box-shadow: inset 0 1px 1px red(0,0,0,.075), 0 0 8px red(102, 175, 233, .6);
}
  
</style>

<script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />

<style type="text/css">
.PopupWaiting{
position: absolute;
background-color: #FAFAFA;
z-index: 2900 !important;
opacity: 0.9;
overflow: hidden;
text-align: center; top: 0; left: 0;
height: 220px;
width: 100%;
padding-top:10%;
} 
</style>
</head>
    <body>
    
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

      <div>

           <div class="row" style="padding-bottom: 65px;">

        <div class="footer" id="footer-one" style="position: fixed; top: calc(90vh); height: 18vh; width: 100%; right: 1px; z-index: 5 ; background: #DD4B39; 
                display: flex; align-items: center; justify-content: space-evenly; border: none;">
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/home.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;"/>
              <p>Home</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/shopping-bag--v1.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;"/>
              <p>Store</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/shield.jpg"  width="28px" style="background: white; border-radius: 50%; border: 1px solid white;"/>
              <p><a href="#insurance" style="text-decoration: none; color: white;">Insurance</a></p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
               <img src="../images/Footer images/rupee-1.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;"/>
              <p>Report</p>
          </div>
          <div class="col-2" style="position: relative; bottom: 19px; color: white; text-align: center;">
              <img src="../images/Footer images/history.png"  width="30px" style="background: white; border-radius: 50%; border: 3px solid white;"/>
              <p>History</p>
          </div>
        </div>

          <center>
        <div class="row" style="width: 98%; display: flex; align-items: flex-start; justify-content: center; border-radius: 8px; margin-top: 5px; background: #f5f5f5;">
            <div class="col-md-6" style="width: 100%; display: flex; align-items: center; background: #f5f5f5; padding: 8px;">
                 <img src="../Images/Ozzype/logo-1.jpeg" width="40px">
                                <p style="position: relative; top: 5px; left: 5px; font-weight: bold; color: black; font-size: 14px;">Add Address</p>

            </div>
            <div class="col-md-6" style="width: 100%; display: flex; align-items: center; justify-content: space-evenly; background-color: #f5f5f5; position: relative;">
<%--                <img src="../images/Nav-Bar/QR-code.png" width="30px" />
                <img src="../images/Nav-Bar/bell.png" width="30px"/>
                <img src="../images/Nav-Bar/question.png" width="30px"/>--%>
                <a onclick="clickLogin()" href="#" style="border: none; position: absolute; top: 4px; right: 35px; background-color: lightskyblue; color: white; text-decoration: none; outline: none; border-radius: 8px;
                 margin-top: 2px; font-weight: bold; letter-spacing: 1px; box-shadow: 0 0 4px rgba(0,0,0,0.6);
                 padding: 8px; font-size: 17px;">Login <img src="../images/Nav-Bar/right-arrow.png"  width="15px" style="position: relative; bottom: 1px;"/></a>


                   <%------------------PopupLogIn Start------------------%>
                              <table class="container" id="table-box" style=" border-radius: 15px;
                                        position: absolute; top: 50%; right: 50%; z-index: 5; background: black;
                                        box-shadow: 0 0 5px rgba(0,0,0,0.8); display: flex; align-items: center; right: 14%;
                                        justify-content: center; width: 90vw; height: 85vh; visibility: hidden;">
                                                  
              <tr class="row" style="display: flex; align-items: center; justify-content:center; border: none; border-radius: 15px; 
                            box-shadow: 0 0 6px rgba(255,255,255,0.9); width: 89vw; height: 85vh;">
            <td class="col" style="background: black; position: relative; border-radius: 15px; display: flex; align-content: center; justify-content: center; 
                  flex-direction: column; padding: 5px 80px; width: 88vw; height: 85vh;">
            <div class="xmark" style="position:absolute; left: 88%; top: 3%;"> 
            <a onclick="clickOneTwo()" href="#" style="z-index: 10; position: relative; padding: 0 8px; border: none; border-radius: 5px; font-size: 21px; background-color: white; color: red;
                                   font-weight: 550;">x</a>
          </div>


          <div class="log-in-bar" style="text-align: center; color: white; width: 87vw; height: 85vh;  position: absolute; left: 0;   ">
            <div class="log-in-bar-one">
              <!-- <img src="../P5/Images/imgj.png" class="img-one">
              <p class="number">1</p> -->
              <div class="log-in-content">
                <h1 style=" padding: 15px;">Log In</h1>
          </div>
        </div>
        <div class="log-in-bar-two">
          <!-- <button class="btn facebook-bar">
            <i class="fa-brands fa-facebook-f"></i>
            <p>Facebook</p>
          </button>
          <button class="btn google-bar">
            <i class="fa-brands fa-google"></i>
            <p>Google</p>
          </button> -->
        </div>
      </div>

      <div class="log-in-bar second"style=" text-align: center; color: white;"> </div>
        <div class="log-in-bar-one">
          <!-- <img src="../P5/Images/imgj.png" class="img-one">
          <p class="number">2</p> -->
          <div class="log-in-bar-one-section">
            <p><b>You can log in using</b></p>
          </div>
        </div>
        <div class="log-in-bar-two-2" style="display: flex; flex-direction: column; align-items: center; justify-content: center; gap: 5px;"> 
             <asp:TextBox ID="txtCompanyCode" runat="server" class="form-control" Visible="false" ></asp:TextBox>   
             <asp:TextBox ID="txtUserName" runat="server" placeholder="Mobile Number" style="border-radius: 5px; padding: 12px; border: none; color:black; position: relative; z-index: 10;"></asp:TextBox>

            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password" MaxLength="10"
                                style="border-radius: 5px; padding: 12px; border: none; margin-top:4px; position: relative; z-index: 10; color:black;"></asp:TextBox><br />
             <asp:LinkButton ID="btnForgotPassword" runat="server" style="color: white; position: relative; z-index: 10;">Forgot Password</asp:LinkButton>
            <asp:CheckBox ID="ChkrememberMe" runat="server" Text="Remember me" class="form-check-label" style="position: relative; z-index: 10; margin-top: 8px;"/>     
             
         <%-- <input type="text" placeholder="Enter Your Mobile Number">
          <input type="password" placeholder="Enter Your Password">
          <p><a href="#">Forgot password</a></p>--%>

            <asp:Label ID="lblErrormsg" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblError" runat="server" Text="Invalid User and Password!!" CssClass="errorlabels" Visible="false" ></asp:Label>
                             <asp:Label ID="Label3" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>
                              <asp:Label ID="lblpassword" runat="server"  CssClass="errorlabels" Visible="false" ></asp:Label>
        </div>
     
               

      <div class="log-in-bar third"style="text-align: center; color: white;">
        <div class="log-in-bar-one" style="text-align: center; margin-top: 15px;">
          <!-- <img src="../P5/Images/imgj.png" class="img-one">
          <p class="number">3</p> -->
        </div><br />    
        <div class="log-in-bar-two-2-2">
             <asp:Button ID="btnSubmit" runat="server" Text="Log In" style="width: 100%; position: relative; z-index: 10; color: white; background-color: palevioletred; border-radius:5px; padding:8px 20px; border:none;"/><br />
          <%--<button class="btn" style="color: white;" >Log In</button>--%>
        </div>
        <div class="log-in-bar-three-3" style="margin-top: 20px;">


                            <a href="#" style="color: rgb(25, 48, 179); font-size: 16px; text-decoration: none; font-weight: 550; margin-top: 15px; position: relative; z-index: 10;">Don't have an account?</a><br />


                            <a href="../create_custome1.aspx" style="color: rgb(25, 48, 179); font-size: 16px; font-weight: 550; margin-top: 15px; text-decoration: none; color: white; text-decoration: underline; position: relative; z-index: 10; top: 8px;">Sign-up</a><br />
          <%--<p><a href="#">Don't have an account ? <p>Register</p></a></p>--%>
          <!-- <img src="../P5/Images/imgj.png" class="img-one">
          <p class="para">4</p> -->
        </div>
      </div>



        </td>
              </tr>
                                                    
                                               </table>
      
             
            </div>
        </div>  
            </center>

          <center style="margin-top: 5px;">
            <div class="container" style="width: 100%; display: flex; flex-direction: column; gap: 6px; background-color: #f5f5f5; margin-top: 5px; border-radius: 8px; padding: 20px; margin:0; padding: 0; box-sizing: border-box;">

                <center>
                <div class="row" style="width: 98%; border-radius: 8px; margin-top: 5px;">
               <div class="col-md-12">
                   <img name="slide" width="100%" height="100%" style="border-radius: 15px;"/>
               </div>
        </div>
                    </center>
                </div>
            
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Transfer Money</h5></div>
            <div class="row" style="width:100%;  display: flex; align-items: center; justify-content: space-between;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <a onclick="clickLogin()" href="#" ><img src="../Images/Nav-Bar/Tra mon/mobile.jpg" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450;">Bank<br>Transfer</p>
              </div>
                 <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a onclick="clickLogin()" href="#"><img src="../Images/Nav-Bar/Tra mon/to-bank.png" width="42px" style="border-radius: 15px;"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">To Bank/<br>UPI ID</p>
              </div>
               <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a onclick="clickLogin()" href="#"><img src="../Images/Nav-Bar/Tra mon/to-self.jpg" width="42px" style="position: relative; bottom: 3px; border-radius: 15px;"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">To Self<br>Account</p>
              </div>
               <div class="col-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <a onclick="clickLogin()" href="#"></a><img src="../Images/Nav-Bar/Tra mon/check-bank.jpg" width="42px" style="border-radius: 15px;">
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Add<br>Amount</p>
              </div>
            </div>
             <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-between; background-color: #E7F0FF; border-bottom-left-radius: 15px; border-bottom-right-radius: 15px; position: relative; top: 5px;">
              <div class="col-12" style="width: 100%; display: flex; align-items: center; justify-content: space-between; margin-top: 15px; padding: 0 20px;">
                <p style="font-size: 15px; font-weight: bold;">My UPI ID:<p style="font-size: 12px; font-weight: 500;">8825689753@ybl</p></p>
                <img src="../Images/Ozzype/some-pics/next.jpeg" style="position: relative; bottom: 7px; color: grey; border-radius: 8px;" widh: 30px;>
              </div>
            </div>

        </div>
            </center>

          <center>
                <div class="row" style="width: 100%; display: flex; justify-content: space-between; padding: 5px; align-items: flex-start; margin-top: 3px;">
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                      <img src="../Images/Ozzype/some-pics/phone-pay-wallet.jpeg" style="font-size: 20px; position: relative; top: 3px;"> 
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">OZZY Pay Wallet</p>
                    </div>
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                      <img src="../Images/Ozzype/some-pics/reward.jpeg" style="font-size: 20px; position: relative; top: 3px;">
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>M.Cash:0</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                    </div>
                    <div class="col-md-4" style="background-color: #4E84DA; color: white; display: flex; align-items: center; justify-content: center; flex-direction: column; padding: 3px 10px; border-radius: 8px; position: relative; right: 4px;">
                       <img src="../Images/Ozzype/some-pics/refer.jpeg" style="font-size: 20px; position: relative; top: 3px;">
                      <p style="font-size: 10px; width: 150%; margin-top: 3px; position: relative; top: 3px;">Refer & Get</p>
                    </div>
                </div>
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Recharge & Pay Bills</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a onclick="clickLogin()" href="#"><img src="../Images/Nav-Bar/TM/mobile.jpg" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Mobile</p>
              </div>
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a onclick="clickLogin()" href="#"><img src="../Images/Nav-Bar/TM/dth.png" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">DTH</p>
              </div>
<%--                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a onclick="clickLogin()" href="#"><img src="../Images/Nav-Bar/TM/mobile.jpg" width="42px" style="border-radius: 15px;"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">DTH Testing</p>
              </div>--%>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a onclick="clickLogin()"> <img src="../Images/Nav-Bar/TM/electricity.jpg" width="42px" style="border-radius: 15px; position: relative; top: 2px;"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Electricity</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/pospaid.png" width="42px" style="border-radius: 15px;"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Postpaid</p>
              </div>
            </div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <center>
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/broadband.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Broadband</p>
              </div>
                    </center>
                <center>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/water.png" width="42px" style="border-radius: 15px;"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Water Bill</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a honclick="clickLogin()"><img src="../Images/Nav-Bar/TM/emi.png" width="42px" style="border-radius: 15px; position: relative; top: 2px;"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">EMI</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/muncipality.png" width="42px" style="border-radius: 15px;"></a>
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Municipality</p>
              </div>
                    </center>
            </div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <center>
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/lpg.png" style="width: 42px; position: relative; bottom: 2px;"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">LPG</p>
              </div>
                    </center>
                <center>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/cable.png" width="42px" style="border-radius: 15px;"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Cable</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/insurance.png" width="42px" style="border-radius: 15px; position: relative; top: 2px;"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Insurance</p>
              </div>
                    </center>
                <center>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a onclick="clickLogin()"><img src="../Images/Nav-Bar/TM/fastag.png" width="42px" style="border-radius: 15px;"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Fastag</p>
              </div>
                    </center>
            </div>
        </div>
            </center>

          <center>
              <a name="insurance"></a>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Insurance</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <a href="#"><img src="../images/Nav-Bar/Insurance/bike.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;"></a>
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Bike</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/car.png" width="42px" style="border-radius: 15px;"></a> 
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">Car</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/health.png" width="42px" style="border-radius: 15px; position: relative; top: 2px;"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px;">Health++</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/PA.png" width="42px" style="border-radius: 15px; position: relative; top: 10px;"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; position: relative; top: 10px;">Personal<br>Accident</p>
              </div>
            </div>
            <div class="row" style=" width:100%;  display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/tl.png" width="42px" style="border-radius: 15px; position: relative; bottom: 2px;"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Term<br>Life</p>
              </div>
               <div class="col- md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: space-around; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"> <img src="../Images/Nav-Bar/Insurance/IF.png" width="42px" style="border-radius: 15px; position: relative; bottom: 5px;"></a>  
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">International<br>Travel</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../Images/Nav-Bar/Insurance/ir.png" width="42px" style="border-radius: 15px; position: relative; bottom: 3px;"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 5px; position: relative; bottom: 1px;">Insurance<br>Renewal</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <%--<img src="./Images//Ozzype/check-bank-balance.jpeg" width="45px">--%>
               <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
              </div>
            </div>
        </div>
            </center>
       
          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Travel Booking</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../images/Nav-Bar/Travel Booking/flight.png" width="42px" style="position: relative; bottom: 2px; border-radius: 15px;"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">Flight</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
            <a href="#"><img src="../images/Nav-Bar/Travel Booking/train.jpg" width="42px" style="position: relative; bottom: 3px; border-radius: 15px;"></a>  
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px;">IRCTC</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../images/Nav-Bar/Travel Booking/bus.jpg" width="42px" style="position: relative; border-radius: 15px;"></a> 
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Bus</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../images/Nav-Bar/Travel Booking/hotel.png" width="42px" style="border-radius: 15px;"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px;">Hotels</p>
              </div>
            </div>
            
        </div>
            </center>
       
          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Switch</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"><img src="../images/Nav-Bar/Switch/swiggy.png" width="42px" style="position: relative; border-radius: 15px;"></a> 
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; margin-top: 3px;">swiggy</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
             <a href="#"> <img src="../images/Nav-Bar/Switch/dmrc.png" width="42px" style="border-radius: 15px;"></a>
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px; position: relative;">Delhi Metro</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <a href="#"><img src="../images/Nav-Bar/Switch/myntra.png" width="42px" style="position: relative; border-radius: 15px;"></a>
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Myntra</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
             <a href="#"><img src="../images/Nav-Bar/Switch/hotstar.png" width="42px" style="position: relative; border-radius: 15px;"></a> 
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; margin-top: 3px; position: relative;">Hotstar</p>
              </div>
            </div>
            
        </div>
            </center>

          <center>
           <div class="row" style="width:98%; display: flex; align-items: center; gap: 10px; justify-content: center; border-radius: 15px; background-color: #f5f5f5; flex-direction: column; margin-top: 10px;">
               <div class="col-md-12" style="width: 100%;"><h5 style="font-size: 16px; font-weight: bold; text-align: start; margin-top: 10px;">Subcription</h5></div>
            <div class="row" style="width:100%; display: flex; align-items: center; justify-content: space-around;">
                <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
              <!-- <i class="fa-regular fa-user" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <img src="../images/Nav-Bar/Subcription/disney-hotstar.png" width="42px" style="position: relative; border-radius: 15px; top:10px;">
                <p style="font-size: 11px;  color: black; width: 190%; font-weight: 450; position: relative; top: 10px;">Disney<br />Hotstar</p>
              </div>
                 <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center; font-size: 22px;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #e15838; font-size: 25px;"></i> -->
              <img src="../images/Nav-Bar/Subcription/tinder.png" width="42px" style="position: relative; border-radius: 15px;">
                <p style="font-size: 11px; color: black; width: 190%; font-weight: 450; margin-top: 3px; position: relative; top: 5px;">Tinder</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns" style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/to-self-copy-1.jpeg"  style="padding: 10px; border-radius: 8px; color: white; font-size: 25px; width: 60px;">  -->
              <img src="../images/Nav-Bar/Subcription/zee5.jpeg" width="42px" style="position: relative; border-radius: 15px;">
                <p style="font-size: 11px; width: 190%; font-weight: 450; color: black; position: relative; top: 5px;">Zee5</p>
              </div>
               <div class="col-md-3" style="text-align: center; display: flex; flex-direction: column; align-items: center; justify-content: center;">
                <!-- <i class="fa-sharp fa-solid fa-building-columns"  style="padding: 8px; border-radius: 8px; color: white; background-color: #622EA2; font-size: 25px;"></i> -->
                <!-- <img src="../Easy Talk - Copy/Images/Transfer-money/check-bank-copy.jpeg"  style="padding: 10px; border-radius: 8px; font-size: 25px; width: 60px;">  -->
              <img src="../images/Nav-Bar/Subcription/flipkart.png" width="42px" style="position: relative;border-radius: 15px;">
                <p style="font-size: 11px; width: 200%; font-weight: 450; color: black; position: relative; top: 5px;">Flipkart</p>
              </div>
            </div>
            
        </div>
            </center>

    </div>


        <script>
            var i = 0;
            var images = [];
            var time = 4000;

            images[0] = "../images/Ozzype/Banner/one.png";
            images[1] = "../images/Ozzype/Banner/two.png";
            images[2] = "../images/Ozzype/Banner/three.png";



            function changeImg() {
                document.slide.src = images[i];
                if (i < images.length - 1) {
                    i++;
                } else {
                    i = 0;
                }
                setTimeout("changeImg()", time);
            }
            window.onload = changeImg;

            let tableBox = document.getElementById("table-box");
            let footerOne = document.getElementById("footer-one");
            function clickLogin() {
                tableBox.style.visibility = "visible";
                footerOne.style.visibility = "hidden";
            }
            function clickOneTwo() {
                tableBox.style.visibility = "hidden";
                footerOne.style.visibility = "visible";
            }
        </script>


      </div>
       
        

             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
    <!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->

    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="pnlForgotPassword"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
        </ajaxToolkit:ModalPopupExtender>

      <asp:Panel ID="pnlForgotPassword" runat="server" style="display:none; background-color:white;"  >
   
    <div  id="Div_Alert" tabindex="-1" role="dialog" >
       <div class="modal-dialog" role="document">
        <div class="modal-content" style=" position: relative; width: auto;">
        <div class="modal-header bg_blue" style="background: rgb(70,141,188); display: flex; justify-content: end; position: relative; padding: 12px;">
      <h4 class="text-center pop_head_text" style="font-size:15px; position: absolute; left: 30%; color: white;">Forgot Password / PIN !!!!
      </h4>
            <asp:Button ID="btnCancel" class="btn btn-primary pull-right" runat="server"  Text="X"  style="margin-right: 15px;"/>
            <%--<button type="button" class="close" data-dismiss="pnlPaymentReminder" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
     </div>
        
 
          <div class="modal-body" style="padding: 10px;">

             <div class="clearfix"></div>


            <div class="form-group top_mar15" align="center">
                  <div class="col-sm-11 col-sm-offset-1"  style="display: flex; align-items: center; justify-content: center; gap: 8px; margin-top: 10px;">
                 <%-- <div class="col-sm-3" >
                   <b> Enter Company Code </b>
               </div>--%>
                <div class="col-sm-5">
               <asp:TextBox ID="txtCompanyCode_ForgotPass" runat="server" visible="false" placeholder="Company Code" CssClass="form-control" ></asp:TextBox>
                   <asp:RadioButtonList ID="rdbForgotType" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" >
                  <asp:ListItem style="font-weight: bold;"> &nbsp;Password&nbsp;</asp:ListItem>
                  <asp:ListItem style="font-weight: bold;">&nbsp;PIN</asp:ListItem>
                  </asp:RadioButtonList>
               </div>
                           <%--  <div class="col-sm-3" >
                   <b> Enter Login ID </b>
               </div>--%>

               <div class="col-sm-5">
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

                  <asp:RadioButtonList ID="rdbType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" style="margin: 15px 0;" >
                  <asp:ListItem style="font-weight: bold;">&nbsp;Email ID&nbsp;&amp;nbsp;&amp;nbsp;</asp:ListItem>
                  <asp:ListItem style="font-weight: bold;">&nbsp;Mobile No</asp:ListItem>
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
         <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
        <script>
            let popUpBar = document.getElementById("pop-bar");
            let tableBox = document.getElementById("table-box");
            function clicklog() {
                tableBox.style.display = "flex";
            }
            function clickme() {
                tableBox.style.display = "none";
            }
            let 
        </script>
    </body>
    </html>
