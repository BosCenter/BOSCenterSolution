<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="CreateAgent.aspx.vb" Inherits="BOSCenter.CreateAgent" %>

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
<asp:Label ID='formheading3' runat='server' Text='Create Agents'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead3' runat='server' Text='Agents Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  AgentType</label>
  <asp:DropDownList ID="ddlAgentType" runat="server" class='form-control' AutoPostBack="true">
  <asp:ListItem>Retailer</asp:ListItem>
    </asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  Company Code</label>
<asp:TextBox ID='txtCompanyCode' runat='server' ReadOnly="true" placeholder='Company Code' class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  Company Name</label>
<asp:TextBox ID='txtCompanyName' runat='server' ReadOnly="true" placeholder='Company Name' class='form-control'></asp:TextBox>
</div>
</div>


</div>


<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  Distributor Id &nbsp;&nbsp; <asp:Label ID="lblSubDisEror" runat="server" Text=""></asp:Label></label>
<asp:TextBox ID='txtSubDistributorID' runat='server' ReadOnly="true"  class='form-control' ></asp:TextBox>
</div>
</div>


<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  Refrence Type</label>
<asp:TextBox ID='txtRefrenceType' runat='server' ReadOnly="true" class='form-control'></asp:TextBox>
</div>
</div>




</div>



<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  RegistrationId</label>
<asp:TextBox ID='txtRegistrationId' runat='server' ReadOnly="true" placeholder='RegistrationId' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentName'>  Agency Name<asp:Label ID="Label4" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtAgencyName' runat='server'  placeholder='Agency Name' class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtPanCardNumber'>  PanCardNumber <asp:Label ID="Label5" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtPanCardNumber' runat='server'  placeholder='PanCardNumber' class='form-control'></asp:TextBox>
</div>
</div>




</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtFirstName'>  FirstName <asp:Label ID="Label6" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtFirstName' runat='server'  placeholder='FirstName' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtLastName'>  LastName</label>
<asp:TextBox ID='txtLastName' runat='server'  placeholder='LastName' class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtDOB'>  DOB (DD/MM/YYYY)<asp:Label ID="Label7" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtDOB' runat='server'  placeholder='DOB' class='form-control'></asp:TextBox>
 <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
        Enabled="True" TargetControlID="txtDOB" TodaysDateFormat="" 
        Format="dd/MM/yyyy">
    </asp:CalendarExtender>
</div>
</div>




</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtEmailID'>  EmailID <asp:Label ID="Label8" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtEmailID' runat='server'  placeholder='EmailID' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtMobileNo'>  MobileNo <asp:Label ID="Label9" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtMobileNo' runat='server'  MaxLength="10" placeholder='MobileNo' class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMobileNo">
    </asp:FilteredTextBoxExtender>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtAlternateMobileNo'>  AlternateMobileNo</label>
<asp:TextBox ID='txtAlternateMobileNo' runat='server' MaxLength="10"  placeholder='AlternateMobileNo' class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtAlternateMobileNo">
    </asp:FilteredTextBoxExtender>
</div>
</div>




</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtPermanentAddress'>  PermanentAddress <asp:Label ID="Label10" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtPermanentAddress' runat='server'  placeholder='PermanentAddress' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtBusinessType'>  BusinessType <asp:Label ID="Label11" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
  <asp:DropDownList ID="ddlBussinessType" runat="server" class='form-control'>
 
    </asp:DropDownList>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtOfficeAddress'>  OfficeAddress<asp:Label ID="Label12" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtOfficeAddress' runat='server'  placeholder='OfficeAddress' class='form-control'></asp:TextBox>
</div>
</div>



</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtState'>  State<asp:Label ID="Label13" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
  <asp:DropDownList ID="ddlState" runat="server" class='form-control' 
        AutoPostBack="True">
 </asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtState'>  District<asp:Label ID="Label15" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
  <asp:DropDownList ID="ddlDistrict" runat="server" class='form-control'>
 </asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtCity'>  City<asp:Label ID="Label14" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtCity' runat='server'  placeholder='City' class='form-control'></asp:TextBox>
</div>
</div>





</div>
<div class="row mar_top10">

<div class="col-md-4">
<div class="form-group">
<label for='txtPincode'>  Pincode<asp:Label ID="Label19" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtPincode' runat='server'  placeholder='Pincode' MaxLength="6" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtPincode">
    </asp:FilteredTextBoxExtender>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtAddharCardNo'>  AddharCardNo <asp:Label ID="Label16" runat="server" ForeColor="red" Text="&nbsp;*"></asp:Label></label>
<asp:TextBox ID='txtAddharCardNo' runat='server'  placeholder='AddharCardNo' class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtGSTNO'>  GSTNO</label>
<asp:TextBox ID='txtGSTNO' runat='server'  placeholder='GSTNO' class='form-control'></asp:TextBox>
</div>
</div>

</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtWebSite'>  WebSite</label>
<asp:TextBox ID='txtWebSite' runat='server'  placeholder='WebSite' class='form-control'></asp:TextBox>
</div>
</div>


</div>
<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>


<div style='margin-bottom:10px;'></div>


<div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
Bank Details
</div>

<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtBankName'>Account Holder Name</label>
<asp:TextBox ID='txtAccountHolderName' runat='server'  placeholder='Account Holder Name' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtBankName'>Bank Name</label>
<asp:TextBox ID='txtBankName' runat='server'  placeholder='Bank Name' class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtBranchName'>Branch Name</label>
<asp:TextBox ID='txtBankBranch' runat='server'  placeholder='Branch Name' 
        class='form-control'></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAccountNo'>  Account Number</label>
<asp:TextBox ID='txtAccountNumber' runat='server'  placeholder='Account Number' 
        class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtAccountType'>  Account Type</label>
  <asp:DropDownList ID="ddlAccountType" runat="server" class='form-control'>
  <asp:ListItem>Saving Account</asp:ListItem>
  <asp:ListItem>Current Account</asp:ListItem>
    </asp:DropDownList>

</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtIFSCcode'>  IFSC Code</label>
<asp:TextBox ID='txtIFSCode' runat='server'  placeholder='IFSC Code' 
        class='form-control'></asp:TextBox>
</div>
</div>

</div>


<div class="row">
<div class="col-md-4">
<div class="form-group">

&nbsp;

</div>
</div>

<div class="col-md-4">
<div class="form-group">
   
<asp:Button ID='btnAddBand' runat='server' Text='Add Details' 
 cssclass='btn btn-primary mar_top30'  />
</div>
</div>
<div class="col-md-4">
<div class="form-group">
 <asp:Label ID="lblErrorGrid" runat="server" Text=""></asp:Label>
</div>

</div>
</div>
</div>
<div class="row" runat="server" id="Div_Grid" visible="false">
<div class="col-sm-12">
<div class="form-group">
<div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="Divgrid" style="overflow:scroll; ">
 <asp:GridView ID="GridView2" runat="server" cssclass="grid-view-themeclass" AutoGenerateColumns="true" ShowFooter="true"
                            BorderStyle="None" PageSize="100">
 <Columns>                           
                             

             
        <asp:TemplateField HeaderText="">
        
                                  <ItemTemplate>
                                    

                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger mar_top5" OnClick="grdDeleteRow_click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                         <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="grdEditRow_click"
                                          CommandName="Select" Text=""><i class="fa fa-pencil-square-o fa-lg"></i></asp:LinkButton>
                                       
                                  </ItemTemplate>
                              </asp:TemplateField>
                           
          </Columns>   
          
          <FooterStyle Font-Size="Large" Font-Bold="true" ForeColor="blue" />
                           
                            </asp:GridView>
                            </div>
                            </div></div> 
</div>
</div> </div>


</div>
</div>
</div>

</div>
</div>




<div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
Upload Documnets 
</div>

<div style="margin-bottom:5px;"></div>


<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtIncomeProof'>  Pan Card</label>
       
    <div style="margin-bottom:5px;"></div>
<asp:FileUpload ID="FileUpload_PanCard" runat="server"  />

    <asp:Button ID="btnUpload_PanCard" runat="server"
        Text="Upload" />
    &nbsp;<asp:Button ID="btnDeleteUpload_PanCard" runat="server" 
         Text="Remove" Enabled="False" />
    <asp:Image ID="Image_PanCard" runat="server" BorderStyle="None" CssClass="" 
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblErrorImage_PanCard" runat="server"></asp:Label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtAgeProof'>  AddharCard Front</label>

<div style="margin-bottom:5px;"></div>
  
<asp:FileUpload ID="FileUpload_AddharCardFront" runat="server"  />

    <asp:Button ID="btnUpload_AddharCardFront" runat="server" 
        Text="Upload" />
    &nbsp;<asp:Button ID="btnDeleteUpload_AddharCardFront" runat="server" 
         Text="Remove" Enabled="False" />
    <asp:Image ID="Image_AddharCardFront" runat="server" BorderStyle="None" CssClass="" 
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblErrorImage_AddharCardFront" runat="server"></asp:Label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtDocumentUpload'>  AddharCard Back</label>

<div style="margin-bottom:5px;"></div>
  
<asp:FileUpload ID="FileUpload_AddharCardBack" runat="server"  />

    <asp:Button ID="btnUpload_AddharCardBack" runat="server" 
        Text="Upload" />
    &nbsp;<asp:Button ID="btnDeleteUpload_AddharCardBack" runat="server" 
         Text="Remove" Enabled="False" />
    <asp:Image ID="Image_AddharCardBack" runat="server" BorderStyle="None" CssClass="" 
        Height="100px" ImageUrl="~/images/uploadimage.png" Width="100px" />
    <asp:Label ID="lblErrorImage_AddharCardBack" runat="server"></asp:Label>
</div>
</div>
</div>
<div class="row">

<div class="col-md-4">
<div class="form-group">
<label for='txtSignatureUpload'> Other Documents </label>
<asp:FileUpload ID="FileUpload_OtherDocuments" runat="server"  />

    <asp:Button ID="btnUpload_OtherDocuments" runat="server" 
        Text="Upload" />
    &nbsp;<asp:Button ID="btnDeleteUpload_OtherDocuments" runat="server" 
         Text="Remove" Enabled="False" />
    <asp:Image ID="Image_OtherDocuments" runat="server" BorderStyle="None" CssClass="" 
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblErrorImage_OtherDocuments" runat="server"></asp:Label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtPhotoIdentification'> Photo </label>
    
<asp:FileUpload ID="FileUpload_Photo" runat="server"  />

    <asp:Button ID="btnUpload_Photo" runat="server" 
        Text="Upload" />
    &nbsp;<asp:Button ID="btnDeleteUpload_Photo" runat="server" 
         Text="Remove" Enabled="False" EnableTheming="True" />
    <asp:Image ID="Image_Photo" runat="server" BorderStyle="None" CssClass="" 
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblErrorImage_Photo" runat="server"></asp:Label>
</div>
</div>

</div>
</div>
</div>
</div>

</div>
</div>
</div>

<div class='form-section'>
<div class='row'>
<div class='col-md-12'>
 <asp:Label ID='lblRID' runat='server' Text='' Visible='false'></asp:Label>
 <asp:Label ID='lblWalletBal' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblServiceCharge' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblDBName' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblError' runat='server' Text=''></asp:Label> 
</div>
</div>
<div class='row'>
<div class='col-md-12'>
<center> <asp:Button ID='btnSave' runat='server' Text='Submit & Proceed' 
 cssclass='btn btn-primary mar_top10' ValidationGroup='a' />&nbsp;
 <asp:Button ID='btnDelete' runat='server' Text='Close' 
 cssclass='btn btn-primary mar_top10'/>  &nbsp;
 </center>
</div>
</div>
</div>
<div style='margin-top:5px;'></div>

 <asp:Button ID='modalPopupButton' runat='server' Text='Button' style='display:none;'/>
<asp:ModalPopupExtender ID='ModalPopupExtender1' runat='server' TargetControlID='modalPopupButton' PopupControlID='DeletePopup'  BackgroundCssClass='modalBackground'  CancelControlID='btnCancel' >
</asp:ModalPopupExtender>
<asp:Panel ID='DeletePopup' runat='server' Width='350px' style='display:none;'  >
<asp:UpdateProgress ID="UpdateProgress1" runat="server"  DisplayAfter="0" AssociatedUpdatePanelID="updatepanel21"  >
                     <ProgressTemplate>
                     <div class="PopupWaiting">
                         
                         <img alt="Loading ..." src="../images/ajax-loader.gif" />
                                     
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
    <asp:Label ID="Label1" runat="server" Text="MemID" Font-Bold="True"></asp:Label> 
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
<asp:Button ID='btnPopupYes' runat='server' Text='OK' Width='80px' CssClass='btn btn-primary'/>
&nbsp;&nbsp;&nbsp
<asp:Button ID='btnCancel' runat='server' Text='Cancel' Width='80px' CssClass='btn btn-primary' />
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

<asp:PostBackTrigger ControlID="btnUpload_PanCard" />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_PanCard" EventName="Click"  />

<asp:PostBackTrigger ControlID="btnUpload_AddharCardBack"  />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_AddharCardBack"  EventName="Click"   />

<asp:PostBackTrigger ControlID="btnUpload_AddharCardFront"  />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_AddharCardFront"  EventName="Click"   />

<asp:PostBackTrigger ControlID="btnUpload_Photo"  />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_Photo"  EventName="Click"  />

<asp:PostBackTrigger ControlID="btnUpload_OtherDocuments"  />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_OtherDocuments"   EventName="Click" />

</Triggers>


</asp:UpdatePanel>
</div</div></div>
  </form>
<script src="https://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="./js/bootstrap.min.js" type="text/javascript"></script>

</body>
</html>
