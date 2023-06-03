<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="500.aspx.vb" Inherits="BOSCenter._500" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>500 - Internal Server Error</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="Content-Language" content="en" />
<link href="../css/bootstrap.min.css" rel="stylesheet" />
<link href="../css/bootstrap.css" rel="stylesheet" />
<link href="../css/style.css" rel="stylesheet" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
 <script src="http://code.jquery.com/jquery-1.10.2.min.js"></script>


 <style type="text/css">
.modalBackground 
{
    height:100%;
    background-color:#504F4F;
    filter:alpha(opacity=70);
    opacity:0.7;
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
</head>

<body class="bg1">
<form id="Form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>

<div class="container">
	<div class="row">
    	<div class="col-sm-6 col-sm-offset-3">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head">
                    	<asp:Label ID="Label1" runat="server"  Text="500 - Internal Server Error"></asp:Label>
                    </div>
                </div>
            </div>
        	<div class="log_form">
            	
                    <div class="clearfix "></div>
                   <div class="form-group">
                    
                    <div class="col-sm-12 top_mar12">
                         <asp:Label ID="lblErrormsg" runat="server" Text="The server encountered something unexpected that didn't allow it to complete the request. We apologize. You can go back to main page."></asp:Label>
                        </div>
                  </div>


                  <div class="clearfix"></div>
                  <div class="form-group">
                  	<div class="col-sm-12">
                          <a href="../Admin/SuperAdminHome.aspx" class="btn btn-primary pull-right mar_top10" >Back</a> 
                    	
                    </div>
                  </div>
       
            </div>
        </div>
    </div>
</div>


   </form>
<script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="../js/bootstrap.min.js" type="text/javascript"></script>

</body>
</html>
