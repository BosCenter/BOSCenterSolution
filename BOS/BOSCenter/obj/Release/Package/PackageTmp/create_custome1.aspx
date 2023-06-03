<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="create_custome1.aspx.vb" Inherits="BOSCenter.create_custome1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <script src="../js/jQuery-2.1.4.min.js" type="text/javascript" ></script>
  <script src="../js/jquery-1.8.2.js" type="text/javascript"></script>
    <%--    <script src="../js/JSstop.js" type="text/javascript"></script>--%>


<link href="https://netdna.bootstrapcdn.com/font-awesome/3.2.1/css/font-awesome.css" rel="stylesheet"/>
<link href="https://netdna.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.css" rel="stylesheet"/>
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

<body class="hold-transition skin-blue sidebar-mini" style="background: linear-gradient( rgb(40,149,123),rgb(16,166,87));">
 <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div style='margin-top:15px;'></div>
<div class='container' style="border-radius: 40px; padding: 15px;">
 
    <asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>



<div class='col-sm-10 col-sm-offset-1'>
<div class='log_form_head1' style="background: darkgrey; font-size: 30px; border-top-left-radius: 15px; border-top-right-radius: 15px;">
<asp:Label ID='formheading3' runat='server' Text='Sign Up'></asp:Label>
</div>
<div class='log_form1' style="background: rgb(39,39,42); border: none;">


<div class='row' >
<div class='col-md-12' >
<div class='form-section' style="background: rgb(85,93,80); border-radius: 15px; color: white;">

<div style='margin-bottom:40px;'>
</div>

<div class="row mar_top10" style="display:none;">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'></label>
    <asp:TextBox ID="txtAgentType" runat="server" CssClass="form-control"></asp:TextBox>
 <%-- <asp:DropDownList ID="ddlAgentType" runat="server" class='form-control' AutoPostBack="true">
      <asp:ListItem>Customer</asp:ListItem>
      <asp:ListItem>Retailer</asp:ListItem>
    </asp:DropDownList>--%>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'> </label>
<asp:TextBox ID='txtCompanyCode' runat='server'  ReadOnly="true" placeholder='Company Code' class='form-control' ></asp:TextBox>
<asp:TextBox ID='txtDBName' runat='server' Visible="false" ReadOnly="true" class='form-control'></asp:TextBox>
<asp:TextBox ID='txtRegistrationId' runat='server' Visible="false" ReadOnly="true" placeholder='RegistrationId' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentName'><asp:Label ID="Label4" runat="server" ForeColor="red" Text="&nbsp;" ></asp:Label></label>
<asp:TextBox ID='txtCompanyName' runat='server'  placeholder='Agency Name'
        class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtFirstName'>Customer Name <asp:Label ID="Label6" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtFirstName' runat='server'  placeholder='Full Name' class='form-control'></asp:TextBox>
</div>
</div>
<%--<div class="col-md-4">
<div class="form-group">
<label for='txtLastName'>  LastName</label>
<asp:TextBox ID='txtLastName' runat='server'  placeholder='LastName' class='form-control'></asp:TextBox>
</div>
</div>--%>
<div class="col-md-4">
<div class="form-group">
<label for='txtEmailID'>  Email ID <asp:Label ID="Label8" runat="server" ForeColor="red" Text="&nbsp;*" ></asp:Label></label>
<asp:TextBox ID='txtEmailID' runat='server'  placeholder='EmailID' class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtMobileNo'>  Mobile No <asp:Label ID="Label9" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtMobileNo' runat='server'  MaxLength="10" placeholder='MobileNo' class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMobileNo">
    </asp:FilteredTextBoxExtender>
</div>
</div>



</div>
<div class="row mar_top10">


<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  Refrence ID<asp:Label ID="Label5" runat="server" ForeColor="red" Text="&nbsp;*"  Visible="false"></asp:Label> &nbsp;<asp:Label ID="lblRefId_Error" runat="server" ForeColor="red" Text=""></asp:Label></label>
<asp:TextBox ID='txtRefrenceID' runat='server'  placeholder=' Refrence ID' 
        class='form-control' AutoPostBack="True"></asp:TextBox>

</div>
</div>


<div class="col-md-4">
<div class="form-group">
<label for='txtAgentName'>  Reference Type</label>
<asp:TextBox ID='txtRefrenceType' runat='server' ReadOnly="true" placeholder='Reference Type' class='form-control'></asp:TextBox>
</div>
</div>



<div class="col-md-4">
<div class="form-group">
<label for='txtAgentName'> <asp:Label ID="lblRef_AgencyName" runat="server" Text="Reference Code"></asp:Label> <asp:Label ID="Label20" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:DropDownList ID="ddl_Ref_Code" runat="server" class='form-control' AutoPostBack="true">
   
    </asp:DropDownList>
     <asp:TextBox ID='txtAgencyName' runat='server' placeholder='Agency Name' class='form-control'></asp:TextBox> 

</div>
</div>





</div>
<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>


<div style='margin-bottom:10px;'></div>


<div class='row'>
<div class='row'>
<div class='col-md-12'>
 <asp:Label ID='lblRID' runat='server' Text='' Visible='false'></asp:Label>
 <asp:Label ID='lblWalletBal' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblServiceCharge' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblError' runat='server' Text=''></asp:Label> 
</div>
</div>
<div class='row'>
    <center>
<div class='col-md-12'>
<center> <asp:Button ID='btnSave' runat='server' Text='Proceed' 
 cssclass='btn btn-primary mar_top10' ValidationGroup='a' style="color: black; background: green; border: none;" />&nbsp;&nbsp;
<asp:Button ID='btnClear' runat='server' Text='Back' 
cssclass='btn btn-danger mar_top10' /> </center>
</div>
    <center>

</div>
</div>
<div style='margin-top:5px;'></div>
</div



</div>

 <asp:Button ID='modalPopupButton' runat='server' Text='Button' style='display:none;'/>
<asp:ModalPopupExtender ID='ModalPopupExtender1' runat='server' TargetControlID='modalPopupButton' PopupControlID='DeletePopup'  BackgroundCssClass='modalBackground'   >
</asp:ModalPopupExtender>
<asp:Panel ID="DeletePopup" runat="server" Width="400px" style="display:none;" >
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"  DisplayAfter="0" AssociatedUpdatePanelID="updatepanel21"  >
                     <ProgressTemplate>
                     <div class="PopupWaiting">
                          <%--<asp:Image ID="imgProgress" ImageUrl="images/ajax-loader.gif" runat="server" />
                                          </div>   --%>     
                     </div>
                     </ProgressTemplate>
                 </asp:UpdateProgress>
<table style='width:100%;background-color:White;border:1px solid gray;'>
<tr>
<td align='center' bgcolor='Silver'>&nbsp;</td>
</tr>
<tr>
<td align='center' bgcolor='Silver'>
<strong>Confirmation Dialog</strong>
<br />
</td>
</tr>
<tr>
<td align='center' bgcolor='Silver'>&nbsp;
</td>
</tr>
<tr>
<td align='center'>&nbsp;
</td>
</tr>

 <tr>
        <td align="center" >
           <div class="row" runat="server" id="Div_deInfo" visible="false">
   <table style="width:50%;">
    <tr>
    <td>
    <asp:Label ID="Label1" runat="server" Text="AgentID" Font-Bold="True"></asp:Label> 
    </td>
    
    <td align="center">
    <asp:Label ID="lblClientID" runat="server" Text="" ForeColor="#cc0000"></asp:Label> 
    </td>
    
    </tr>
   
    <tr>
     <td>
    <asp:Label ID="Label2" runat="server" Text="Password " Font-Bold="True"></asp:Label> 
    </td>
     <td align="center" >
           <asp:Label ID="lblPassword" runat="server" Text="" ForeColor="#cc0000" ></asp:Label>  
           </td> 
    </tr>
    <tr runat="server" id="TD_Pin" visible="false">
     <td>
    <asp:Label ID="Label3" runat="server" Text="Transaction Pin " Font-Bold="True"></asp:Label> 
    </td>
     <td align="center" >
           <asp:Label ID="lblTransactionPin" runat="server" Text="" ForeColor="#cc0000" ></asp:Label>  
           </td> 
    </tr>
   </table>
   </div>
   </td>
    </tr>




<tr>
<td align='center'>
<asp:Label ID='lblDialogMsg' runat='server' Text=''></asp:Label>  </td>
</tr>
<tr>
<td align='center'>&nbsp;
</td>
</tr>

<tr>
<td align='center'> 
<asp:Button ID='btnPopupYes' runat='server' Text='Yes' Width='80px' CssClass='btn btn-primary'/>
&nbsp;&nbsp;&nbsp
<asp:Button ID='btnCancel' runat='server' Text='No' Width='80px' CssClass='btn btn-primary' />
</td>
</tr>
<tr>
<td align='center'>&nbsp; 
</td>
</tr>
</table>
</asp:Panel>


 <asp:Button ID="Button2" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button2" PopupControlID="DeleteDocumentPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelDeleteDocument" >
</asp:ModalPopupExtender>

<asp:Panel ID="DeleteDocumentPopup" runat="server" Width="400px" style="display:none;" >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>::: Alert Dialog :::</strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
           <asp:Label ID="lblDeleteInfo" runat="server" Text="This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"></asp:Label></td>
           <asp:Label ID="lblDeleteDocumentInfo" runat="server" Text=""></asp:Label>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnDelete_Document" runat="server" Text="Yes" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelDeleteDocument" runat="server" Text="No" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>
    </asp:Panel>

         <asp:Button ID="Button1" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelInfo" >
</asp:ModalPopupExtender>

<asp:Panel ID="InformationPopup" runat="server" Width="350px"  style="display:none;"  >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>Information Dialog</strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" >
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblInformation" runat="server" ></asp:Label>
        </td>
    </tr>
  
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnCancelInfo" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>
       <asp:Button ID="deleteButton4" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="DeleteModalPopupExtender" runat="server" TargetControlID="deleteButton4" PopupControlID="DeletePopup1"  BackgroundCssClass="modalBackground"  CancelControlID="btnDelCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup1" runat="server" Width="350px" style="display:none;"  >

<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>Confirmation Dialog</strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblDelDialogMsg" runat="server" Text=""></asp:Label>  <br />
            <asp:Label ID="lblAlertDelPer" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblDel" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblDelType" runat="server" Text="" Visible="false"></asp:Label> 
            <asp:Label ID="lblRowIndex" runat="server" Text="" Visible="false"></asp:Label>  
            </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnDelOk" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" OnClick="btnDelOk_Click"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary"  />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>



    </asp:Panel>



</ContentTemplate>

   <Triggers>
                 <asp:PostBackTrigger ControlID="btnPopupYes" />
              
                 </Triggers>

</asp:UpdatePanel>



</div>



   </form>
<script src="https://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="./js/bootstrap.min.js" type="text/javascript"></script>

</body>
</html>
