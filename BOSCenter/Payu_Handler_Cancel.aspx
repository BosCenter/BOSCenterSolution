<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="Payu_Handler_Cancel.aspx.vb" Inherits="BOSCenter.Payu_Handler_Cancel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <script src="../js/jQuery-2.1.4.min.js" type="text/javascript" ></script>
  <script src="../js/jquery-1.8.2.js" type="text/javascript"></script>
<%--    <script src="../js/JSstop.js" type="text/javascript"></script>--%>


<link href="https://netdna.bootstrapcdn.com/font-awesome/3.2.1/css/font-awesome.css" rel="stylesheet">
<link href="https://netdna.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.css" rel="stylesheet">
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDnvSDxvacLn65kW08blk7Cq7a3EEvgKGQ&sensor=false"></script>




<meta charset="utf-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>BOS Center</title>
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

<body class="hold-transition skin-blue sidebar-mini">
 <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>
      
<div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-10 col-sm-offset-1'>
<div class='log_form_head1'>
<asp:Label ID='formheading3' runat='server' Text='BOS - Payment Status Details'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head' runat="server" visible="false" >
<asp:Label ID='lblformsectionhead3' runat='server' Text='Response Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>



<div class="row mar_top10">
<div class="col-md-12">
<div class="form-group">
<center>
<asp:Label ID="lblError" runat="server"></asp:Label><br /><br />
<asp:Label ID="lblOtherInfo" runat="server"></asp:Label>
    <asp:Label ID="lblCompanyCode" Visible="false"  runat="server"></asp:Label>
    <asp:Label ID="lblDBName" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblTransID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblRegistrationID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPurpose" runat="server" Visible="false"></asp:Label>

<br /><br />

<asp:Button ID="btnRedirect" Visible="false"  CssClass="btn btn-primary" runat="server" 
        Text="OK" />

        </center>
</div>
</div>


</div>
<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>


</ContentTemplate>


<Triggers>


</Triggers>


</asp:UpdatePanel>
</div>
</div>
</div>



   </form>
<script src="https://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="./js/bootstrap.min.js" type="text/javascript"></script>

</body>
</html>