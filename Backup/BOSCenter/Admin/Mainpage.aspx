<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Mainpage.aspx.vb" Inherits="BOSCenter.Mainpage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>ERP</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.5 -->
  <link rel="stylesheet" href="~/css/admin.css" />
  <!-- Font Awesome -->
 <link href="~/css/font-awesome.css" rel="stylesheet" />
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
  <!-- Theme style -->
  <link rel="stylesheet" href="~/css/admin_new.css" />
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
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.min.js"></script>
  
  <script language="JavaScript">
<!--
      function autoResize(id) {
          var newheight;
          var newwidth;

          if (document.getElementById) {
              newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
              newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
          }

          document.getElementById(id).height = (newheight) + "px";
          document.getElementById(id).width = (newwidth) + "px";
      }
//-->
</script>

  <style>
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
  </style>
</head>

<body class="hold-transition skin-blue sidebar-mini">
 <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>
      

 <header class="main-header">
    <!-- Logo -->
    <a href="#" class="logo">
      <!-- mini logo for sidebar mini 50x50 pixels -->
      <span class="logo-mini"><b>NIDHI</b></span>
      <!-- logo for regular state and mobile devices -->
      <span class="logo-lg"><b>NIDHI SOFTWARE</b></span>
    </a>
    <!-- Header Navbar: style can be found in header.less -->
    <nav class="navbar navbar-static-top" role="navigation">
      <!-- Sidebar toggle button-->
      <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </a>

      <div class="navbar-custom-menu">
        <ul class="nav navbar-nav">
          <!-- Messages: style can be found in dropdown.less-->
          <li class="dropdown messages-menu">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="fa fa-envelope-o"></i>
              <span class="label label-success">4</span>
            </a>
            <ul class="dropdown-menu">
              <li class="header">You have 4 messages</li>
              <li>
                <!-- inner menu: contains the actual data -->
                <ul class="menu">
                  <li><!-- start message -->
                    <a href="#">
                      <div class="pull-left">
                        <img src="../../dist/img/user2-160x160.jpg" class="img-circle" alt="User Image" />
                      </div>
                      <h4>
                        Support Team
                        <small><i class="fa fa-clock-o"></i> 5 mins</small>
                      </h4>
                      <p>Why not buy a new awesome theme?</p>
                    </a>
                  </li>
                  <!-- end message -->
                </ul>
              </li>
              <li class="footer"><a href="#">See All Messages</a></li>
            </ul>
          </li>
          <!-- Notifications: style can be found in dropdown.less -->
          <li class="dropdown notifications-menu">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="fa fa-bell-o"></i>
              <span class="label label-warning">10</span>
            </a>
            <ul class="dropdown-menu">
              <li class="header">You have 10 notifications</li>
              <li>
                <!-- inner menu: contains the actual data -->
                <ul class="menu">
                  <li>

                    <a href="#">
                      <i class="fa fa-users text-aqua"></i> 5 new members joined today
                    </a>
                  </li>
                </ul>
              </li>
              <li class="footer"><a href="#">View all</a></li>
            </ul>
          </li>
          <!-- Tasks: style can be found in dropdown.less -->
          <li class="dropdown tasks-menu">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="fa fa-flag-o"></i>
              <span class="label label-danger">9</span>
            </a>
            <ul class="dropdown-menu">
              <li class="header">You have 9 tasks</li>
              <li>
                <!-- inner menu: contains the actual data -->
                <ul class="menu">
                  <li><!-- Task item -->
                    <a href="#">
                      <h3>
                        Design some buttons
                        <small class="pull-right">20%</small>
                      </h3>
                      <div class="progress xs">
                        <div class="progress-bar progress-bar-aqua" style="width: 20%" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100">
                          <span class="sr-only">20% Complete</span>
                        </div>
                      </div>
                    </a>
                  </li>
                  <!-- end task item -->
                </ul>
              </li>
              <li class="footer">
                <a href="#">View all tasks</a>
              </li>
            </ul>
          </li>
          <!-- User Account: style can be found in dropdown.less -->
          <li class="dropdown user user-menu">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <img src="../images/man-156584_960_720.png" class="user-image" alt="User Image" />
              <span class="hidden-xs">Munnu</span>
            </a>
            <ul class="dropdown-menu">
              <!-- User image -->
              <li class="user-header">
                <img src="../images/man-156584_960_720.png" class="img-circle" alt="User Image" />

                <p>
                  Munnu Kumar
                </p>
              </li>
             
              <li class="user-footer">
                <div class="pull-left">
                  <a href="#" class="btn btn-default btn-flat">Profile</a>
                </div>
                <div class="pull-right">
                  <a href="#" class="btn btn-default btn-flat">Sign out</a>
                </div>
              </li>
            </ul>
          </li>
          <!-- Control Sidebar Toggle Button -->
          <li>
            <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
          </li>
        </ul>
      </div>
    </nav>
  </header>



 

  <!-- Left side column. contains the logo and sidebar -->
  <aside class="main-sidebar">
    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar">
      <div class="user-panel">
        <div class="pull-left image">
          <img src="../images/man-156584_960_720.png" height="160" width="160" class="img-circle" alt="User Image" />
        </div>
        <div class="pull-left info">
          <p>User Name</p>
          <a href="#"><i class="fa fa-circle text-success"></i> Online</a>
        </div>
      </div>
     <div id="tabs" >

<asp:UpdatePanel ID="MainUpdatePanel" UpdateMode="Conditional"  runat="server">
<ContentTemplate>

   
<asp:ListView ID="ListView1" runat="server">
<LayoutTemplate>
<div style="width: 100%;">
     <asp:PlaceHolder ID="groupPlaceHolder" runat="server" />
 </div>
</LayoutTemplate>
<grouptemplate>
   <div style="clear: both;">
       <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
    </div>
</grouptemplate>

 <ItemTemplate>
 <ul class="sidebar-menu">  
     <li class="treeview" ><asp:LinkButton ID="LinkButton1"  CommandArgument='<%# Eval("MainMenu") %>' runat="server" ClientIDMode="AutoID"  OnClick="MainMenu_Click"  > <i class='<%# Eval("MainMenuIcon") %>'></i>&nbsp;<span><%# Eval("MainMenu") %></span></asp:LinkButton></li>
  </ul>
 </ItemTemplate>

</asp:ListView>


</ContentTemplate>


</asp:UpdatePanel>

   <%--  <ul class="sidebar-menu  tabs-menu">--%>
    <%-- <ul class="sidebar-menu">

       <li class="treeview"><asp:LinkButton ID="LinkButton1" runat="server" OnClick="MainMenu_Click" CommandArgument="Masters"  ><a href="#"><i class="fa fa-book fa-lg"></i><span>Masters</span></a></asp:LinkButton></li>
        <li class="treeview"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="MainMenu_Click" CommandArgument="Plan Management"><a href="#"><i class="fa fa-pie-chart fa-lg"></i> <span>Plan Management</span></a></asp:LinkButton></li>
        <li class="treeview"><asp:LinkButton ID="LinkButton3" runat="server" OnClick="MainMenu_Click" CommandArgument="Client Management"><a href="#"><i class="fa fa-user-plus fa-lg"></i> <span>Client Management</span></a></asp:LinkButton></li>
        <li class="treeview"><asp:LinkButton ID="LinkButton4" runat="server" OnClick="MainMenu_Click" CommandArgument="Operator Management"><a href="#"><i class="fa fa-users fa-lg"></i> <span>Operator Management</span></a></asp:LinkButton></li>
        <li class="treeview"><asp:LinkButton ID="LinkButton5" runat="server" OnClick="MainMenu_Click" CommandArgument="Reports"><a href="#"><i class="fa fa-hourglass fa-lg"></i> <span>Reports</span></a></asp:LinkButton></li>
        <li class="treeview"><asp:LinkButton ID="LinkButton6" runat="server" OnClick="MainMenu_Click" CommandArgument="Mail Management"><a href="#"><i class="fa fa-envelope fa-lg"></i><span> Mail Management</span></a></asp:LinkButton></li>
        <li class="treeview"><asp:LinkButton ID="LinkButton7" runat="server" OnClick="MainMenu_Click" CommandArgument="Sms Management"><a href="#"><i class="fa fa-phone-square fa-lg"></i> <span>Sms Management</span></a></asp:LinkButton></li>
        <li class="treeview"><asp:LinkButton ID="LinkButton8" runat="server" OnClick="MainMenu_Click" CommandArgument="Settings"><a href="#"><i class="fa fa-wrench fa-lg"></i> <span>Settings</span></a></asp:LinkButton></li>

        <li class="treeview"><a href="#tab-1"><i class="fa fa-book fa-lg"></i> <span>Masters</span></a></li>
        <li class="treeview"><a href="#tab-2"><i class="fa fa-pie-chart fa-lg"></i> <span>Plan Management</span></a></li>
        <li class="treeview"><a href="#tab-3"><i class="fa fa-user-plus fa-lg"></i> <span>Client Management</span></a></li>
        <li class="treeview"><a href="#tab-4"><i class="fa fa-users fa-lg"></i> <span>Operator Management</span></a></li>
        <li class="treeview"><a href="#tab-5"><i class="fa fa-hourglass fa-lg"></i> <span>Reports</span></a></li>
        <li class="treeview"><a href="#tab-6"><i class="fa fa-envelope fa-lg"></i> <span>Mail Management</span></a></li>
        <li class="treeview"><a href="#tab-7"><i class="fa fa-phone-square fa-lg"></i> <span>Sms Management</span></a></li>
        <li class="treeview"><a href="#tab-8"><i class="fa fa-wrench fa-lg"></i> <span>Settings</span></a></li>

      </ul>--%>
     </div>
    </section>
   </aside>




  <div class="content-wrapper">
	<div class="row">
    	<div class="col-sm-12" id="SubMenuDIV" runat="server">

        	<!------------Master Body 1 start-------------->
            <div id="tab-1"  class="tab-content"  >
                <div class="dropdown" >

                    <asp:UpdatePanel ID="SubMenuPanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                         <asp:ListView ID="ListView2" runat="server"  >
                <LayoutTemplate>
<div style="width: 100%;">
     <asp:PlaceHolder ID="groupPlaceHolder" runat="server" />
 </div>
</LayoutTemplate>
<grouptemplate>
   <%--<div style="clear: both;">--%>
       <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
   <%-- </div>--%>
</grouptemplate>
<ItemTemplate>
    <asp:Button ID="Button1"  CommandArgument='<%# Eval("SubMenu") %>' class='<%# Eval("SubMenuCSS") %>' runat="server" Text='<%# Eval("SubMenu") %>'   ClientIDMode="AutoID"  OnClick="SubMenu_Click" />
 </ItemTemplate>
                </asp:ListView>
                    </ContentTemplate>
                    </asp:UpdatePanel>

               <%-- <a href="CountryMaster.aspx" class="btn btn-success btn-sm mb3"   >Country</a>

                    <button class="btn btn-success btn-sm mb3 load" type="button" value="Country"  >Country Master</button>
                    <button class="btn btn-success btn-sm mb3" type="button" value="Country" onclick="LoadPage('../SuperAdmin/StateMaster.aspx');" >State Master</button>
                    <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >District Master</button>
                    <button class="btn btn-warning btn-sm mb3" type="button" value="Country" >Group Master</button>
                    <button class="btn btn-info btn-sm mb3" type="button" value="Country" >News & Event Master</button>
                    <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Portfolio Master</button>
                    <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Client Testimonial</button>
                    <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >Company</button>--%>
                </div>   
            </div>            
            
            <!------------Master Body 1 End-----------------> 

           <%--
            <!--------------Plan Management Body 2 start------------->
             <div id="tab-2" class="tab-content">

             <div class="dropdown">
              
             <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Plane Master</button>
             <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Module Master</button>
             <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >Duration Master</button>
             <button class="btn btn-warning btn-sm mb3" type="button" value="Country" >Plan vs Module Master</button>
             <button class="btn btn-info btn-sm mb3" type="button" value="Country" >Plan Type Master</button>
             
            </div>
		
            </div>
            <!---------------Plan Management  Body 2 End--------------->



            <!----------------Client Management body 3 start------------->
            <div id="tab-3" class="tab-content">

             <div class="dropdown">
             <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Client Registration</button>
             <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Search & Edit</button>
             <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >Client Verification</button>
             <button class="btn btn-warning btn-sm mb3" type="button" value="Country" >Go To Client A\C</button>
             <button class="btn btn-info btn-sm mb3" type="button" value="Country" >Change Plan</button>
            </div>
			
            </div>
            <!--------------Client Management 3 End------------------>
            
            <!--------------Operator Management Body 4 Start------------->
            
            <div id="tab-4" class="tab-content">

             <div class="dropdown">
             <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Create Operator</button>
             <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Login Time Frame</button>
             <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >Logout User</button>
             
            </div>
		
            </div>
            
            <!--------------Operator Management Body 4 End----------------->

            <!-------------Reports Body 5 Start---------------->
            <div id="tab-5" class="tab-content">

             <div class="dropdown">

             <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Plan Wise Client Report</button>
             <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Plan Renewal Report</button>
             
             <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >Account Report</button>

             <button class="btn btn-warning btn-sm mb3" type="button" value="Country" >Client Password Report</button>
             <button class="btn btn-info btn-sm mb3" type="button" value="Country" >Operator Login Report</button>
             <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Client Remove A/C Report</button>
             <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Client Updation Report</button>
            
            </div>
	
            </div>
            <!-------------Reports Body 5 End------------------>

            <!-------------Mail Management Body 6 Start---------------->
             <div id="tab-6" class="tab-content">

             <div class="dropdown">
             <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Send Mail</button>
              <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Add Template</button>
              
            </div>
            </div>
		
           
            <!-------------Mail Management Body 6 End------------------>

            <!-------------SMS Management Body 7 Start---------------->
            <div id="tab-7" class="tab-content">

             <div class="dropdown">
              <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >Send Sms</button>
              <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Add Message Category</button>
              <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >Add Template</button>
              <button class="btn btn-warning btn-sm mb3" type="button" value="Country" >Search Send Message</button>
            </div>
		
            </div>
            <!-------------SMS Management Body 7 End------------------>



             <!-------------Settings Body 8 Start---------------->
             <div id="tab-8" class="tab-content">

             <div class="dropdown">
             <button class="btn btn-primary btn-sm mb3" type="button" value="Country" >User Rights</button>
              <button class="btn btn-success btn-sm mb3" type="button" value="Country" >Client DB Allocation</button>
              <button class="btn btn-danger btn-sm mb3" type="button" value="Country" >Change Password</button>
              <button class="btn btn-warning btn-sm mb3" type="button" value="Country" >Site Map</button>
            </div>
			
            </div>
            <!-------------Settings Body 8 End------------------>

--%>
          
             </div>
        </div>
   
    <div class="row">
      <div class="col-sm-12 mar_top30">
    <!-------------Content Page Goes Here------------------>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server"   DisplayAfter="1" >
            <ProgressTemplate>
           <div class="divWaiting">            
<%--	<asp:Label ID="lblWait" runat="server" 
	Text=" Please wait... " />--%>
	<asp:Image ID="imgWait" runat="server" 
	ImageAlign="Top" Width="210px" Height="150px" ImageUrl="../images/loadmore.gif" />
  </div>
                
            </ProgressTemplate>
            </asp:UpdateProgress>

        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional"  >
        <ContentTemplate>
            
        
        <div class="embed-responsive embed-responsive-16by9">
        <iframe runat="server" id="contentPage" style="width:100%; border:0px;z-index:1000;" scrolling="yes" class="embed-responsive-item"  >
        
        </iframe>
       </div>

        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
     </div>
    </div>
     

 



      </form>

   <div id="aa" >
   </div>

<!-- jQuery 2.1.4 -->
<script src="../js/jQuery-2.1.4.min.js" type="text/javascript" ></script>
<!-- Bootstrap 3.3.5 -->
<script src="../js/bootstrap.min.js" type="text/javascript"></script>
<!-- ChartJS 1.0.1 -->
<script src="../js/Chart.min.js" type="text/javascript"></script>
<!-- FastClick -->
<script src="../js/fastclick.js" type="text/javascript"></script>
<!-- AdminLTE App -->
<script src="../js/app.min.js"></script>
<!-- AdminLTE for demo purposes -->
<script src="../js/demo.js"></script>

<script>
    $(document).ready(function () {
        $(".tabs-menu a").click(function (event) {
            event.preventDefault();
            $(this).parent().addClass("current");
            $(this).parent().siblings().removeClass("current");
            var tab = $(this).attr("href");
            $(".tab-content").not(tab).css("display", "none");
            $(tab).fadeIn();
        });


        $('.load').click(function () {
            //alert('hi');
            $("#aa").append("hi");
            $("#aa").stop().load("index-2.html");
        });

    });

    $('.treeview a').click(function () {
        $(this).addClass('active').siblings().removeClass('active');
    });

</script>

<!-- page script -->
<script>
    $(function () {
        /* ChartJS
        * -------
        * Here we will create a few charts using ChartJS
        */

        //--------------
        //- AREA CHART -
        //--------------

        // Get context with jQuery - using jQuery's .get() method.
        var areaChartCanvas = $("#areaChart").get(0).getContext("2d");
        // This will get the first returned node in the jQuery collection.
        var areaChart = new Chart(areaChartCanvas);

        var areaChartData = {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
        {
            label: "Electronics",
            fillColor: "rgba(210, 214, 222, 1)",
            strokeColor: "rgba(210, 214, 222, 1)",
            pointColor: "rgba(210, 214, 222, 1)",
            pointStrokeColor: "#c1c7d1",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(220,220,220,1)",
            data: [65, 59, 80, 81, 56, 55, 40]
        },
        {
            label: "Digital Goods",
            fillColor: "rgba(60,141,188,0.9)",
            strokeColor: "rgba(60,141,188,0.8)",
            pointColor: "#3b8bba",
            pointStrokeColor: "rgba(60,141,188,1)",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(60,141,188,1)",
            data: [28, 48, 40, 19, 86, 27, 90]
        }
      ]
        };

        var areaChartOptions = {
            //Boolean - If we should show the scale at all
            showScale: true,
            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: false,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - Whether the line is curved between points
            bezierCurve: true,
            //Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            //Boolean - Whether to show a dot for each point
            pointDot: false,
            //Number - Radius of each point dot in pixels
            pointDotRadius: 4,
            //Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            //Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            //Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            //String - A legend template
          
            //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true
        };

        //Create the line chart
        areaChart.Line(areaChartData, areaChartOptions);

        //-------------
        //- LINE CHART -
        //--------------
        var lineChartCanvas = $("#lineChart").get(0).getContext("2d");
        var lineChart = new Chart(lineChartCanvas);
        var lineChartOptions = areaChartOptions;
        lineChartOptions.datasetFill = false;
        lineChart.Line(areaChartData, lineChartOptions);

        //-------------
        //- PIE CHART -
        //-------------
        // Get context with jQuery - using jQuery's .get() method.
        var pieChartCanvas = $("#pieChart").get(0).getContext("2d");
        var pieChart = new Chart(pieChartCanvas);
        var PieData = [
      {
          value: 700,
          color: "#f56954",
          highlight: "#f56954",
          label: "Chrome"
      },
      {
          value: 500,
          color: "#00a65a",
          highlight: "#00a65a",
          label: "IE"
      },
      {
          value: 400,
          color: "#f39c12",
          highlight: "#f39c12",
          label: "FireFox"
      },
      {
          value: 600,
          color: "#00c0ef",
          highlight: "#00c0ef",
          label: "Safari"
      },
      {
          value: 300,
          color: "#3c8dbc",
          highlight: "#3c8dbc",
          label: "Opera"
      },
      {
          value: 100,
          color: "#d2d6de",
          highlight: "#d2d6de",
          label: "Navigator"
      }
    ];
        var pieOptions = {
            //Boolean - Whether we should show a stroke on each segment
            segmentShowStroke: true,
            //String - The colour of each segment stroke
            segmentStrokeColor: "#fff",
            //Number - The width of each segment stroke
            segmentStrokeWidth: 2,
            //Number - The percentage of the chart that we cut out of the middle
            percentageInnerCutout: 50, // This is 0 for Pie charts
            //Number - Amount of animation steps
            animationSteps: 100,
            //String - Animation easing effect
            animationEasing: "easeOutBounce",
            //Boolean - Whether we animate the rotation of the Doughnut
            animateRotate: true,
            //Boolean - Whether we animate scaling the Doughnut from the centre
            animateScale: false,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,
            // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //String - A legend template
            
        };
        //Create pie or douhnut chart
        // You can switch between pie and douhnut using the method below.
        pieChart.Doughnut(PieData, pieOptions);

        //-------------
        //- BAR CHART -
        //-------------
        var barChartCanvas = $("#barChart").get(0).getContext("2d");
        var barChart = new Chart(barChartCanvas);
        var barChartData = areaChartData;
        barChartData.datasets[1].fillColor = "#00a65a";
        barChartData.datasets[1].strokeColor = "#00a65a";
        barChartData.datasets[1].pointColor = "#00a65a";
        var barChartOptions = {
            //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
            scaleBeginAtZero: true,
            //Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            //String - Colour of the grid lines
            scaleGridLineColor: "rgba(0,0,0,.05)",
            //Number - Width of the grid lines
            scaleGridLineWidth: 1,
            //Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            //Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            //Boolean - If there is a stroke on each bar
            barShowStroke: true,
            //Number - Pixel width of the bar stroke
            barStrokeWidth: 2,
            //Number - Spacing between each of the X value sets
            barValueSpacing: 5,
            //Number - Spacing between data sets within X values
            barDatasetSpacing: 1,
            //String - A legend template
            
            //Boolean - whether to make the chart responsive
            responsive: true,
            maintainAspectRatio: true
        };

        barChartOptions.datasetFill = false;
        barChart.Bar(barChartData, barChartOptions);
    });
</script>



</body>