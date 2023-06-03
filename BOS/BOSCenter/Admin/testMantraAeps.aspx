<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testMantraAeps.aspx.vb" Inherits="BOSCenter.testMantraAeps" validateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script  src="https://code.jquery.com/jquery-3.5.1.min.js" integrity="sha256-9/aliU8dGd2tb6OSsuzixeV4y/faTqgFtohetphbbj0=" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="method" type="hidden" value="">
<input id="info" type="hidden" value="">
<input type="hidden" name="txtWadh" id="txtWadh">
    <button type="button" class="btn btn-danger" onclick="discoverAvdm();">CHECK DEVICE</button>

	
    <button type="button" class="btn btn-warning"   onclick="CaptureAvdm();">SCAN</button>

	
    <button type="button" class="btn btn-warning"   onclick="deviceInfoAvdm();">info</button>
	<h1>DEVICE INFO</h1>
	<textarea id="txtDeviceInfo"  name="devIndo"  type="text"  rows="10" cols="50"  height="1000"></textarea>
	
	<h1>CAPTURE DATA</h1>
	<textarea id="txtPidData"  name="devPid"  type="text" rows="20"  cols="100" height="1000"></textarea>
	
	

    <script type="text/javascript">
      function discoverAvdm() {
        var GetCustomDomName = "127.0.0.1";
        var SuccessFlag = 0;
        var primaryUrl = "http://" + GetCustomDomName + ":";

        try {
          var protocol = window.location.href;
          if (protocol.indexOf("https") >= 0) {
            primaryUrl = "https://" + GetCustomDomName + ":";
          }
        } catch (e) {}

        url = "";
     
        SuccessFlag = 0;
        for (var i = 11100; i <= 11112; i++) {
          console.log("Discovering RD service on port : " + i.toString());
          var verb = "RDSERVICE";
          var err = "";

          var res;
          $.support.cors = true;
          var httpStaus = false;
          var jsonstr = "";
          var data = new Object();
          var obj = new Object();

          $.ajax({
            type: "RDSERVICE",
            async: false,
            crossDomain: true,
            url: primaryUrl + i.toString(),
            contentType: "text/xml; charset=utf-8",
            processData: false,
            cache: false,
            crossDomain: true,

            success: function (data) {
              httpStaus = true;
              res = {
                httpStaus: httpStaus,
                data: data
              };
              //alert(data);

				$("#txtDeviceInfo").val(data);
                 // document.getElementById"<%=txtDeviceInfo1.ClientID%>").value = data.toString();

              finalUrl = primaryUrl + i.toString();
              var $doc = $.parseXML(data);//$data
			  debugger;
              var CmbData1 = $($doc).find('RDService').attr('status');
              var CmbData2 = $($doc).find('RDService').attr('info');

               if(RegExp('\\b'+ 'Mantra' +'\\b').test(CmbData2)==true  ||  RegExp('\\b'+ 'Morpho_RD_Service' +'\\b').test(CmbData2)==true  ||  RegExp('\\b'+ 'SecuGen India Registered device Level 0' +'\\b').test(CmbData2)==true ||  RegExp('\\b'+ 'Precision - Biometric Device is ready for capture' +'\\b').test(CmbData2)==true ||  RegExp('\\b'+ 'RD service for Startek FM220 provided by Access Computech' +'\\b').test(CmbData2)==true ||  RegExp('\\b'+ 'NEXT' +'\\b').test(CmbData2)==true  ){
			   
			   debugger;
							console.log($($doc).find('Interface').eq(0).attr('path'));
						
							if(RegExp('\\b'+ 'Mantra' +'\\b').test(CmbData2)==true){
							
        							if($($doc).find('Interface').eq(0).attr('path')=="/rd/capture")
        							{
        							  MethodCapture=$($doc).find('Interface').eq(0).attr('path');
        							}
        							if($($doc).find('Interface').eq(1).attr('path')=="/rd/capture")
        							{
        							  MethodCapture=$($doc).find('Interface').eq(1).attr('path');
        							}
        							if($($doc).find('Interface').eq(0).attr('path')=="/rd/info")
        							{
        							  MethodInfo=$($doc).find('Interface').eq(0).attr('path');
        							}
        							if($($doc).find('Interface').eq(1).attr('path')=="/rd/info")
        							{
        							  MethodInfo=$($doc).find('Interface').eq(1).attr('path');
        							}
							}else if(RegExp('\\b'+ 'Morpho_RD_Service' +'\\b').test(CmbData2)==true){
							        MethodCapture=$($doc).find('Interface').eq(0).attr('path');
							        MethodInfo=$($doc).find('Interface').eq(1).attr('path');
							}else if(RegExp('\\b'+ 'SecuGen India Registered device Level 0' +'\\b').test(CmbData2)==true){
							        MethodCapture=$($doc).find('Interface').eq(0).attr('path');
							        MethodInfo=$($doc).find('Interface').eq(1).attr('path');
							}else if(RegExp('\\b'+ 'Precision - Biometric Device is ready for capture' +'\\b').test(CmbData2)==true){
							        MethodCapture=$($doc).find('Interface').eq(0).attr('path');
							        MethodInfo=$($doc).find('Interface').eq(1).attr('path');
							}else if(RegExp('\\b'+ 'RD service for Startek FM220 provided by Access Computech' +'\\b').test(CmbData2)==true){
							        MethodCapture=$($doc).find('Interface').eq(0).attr('path');
							        MethodInfo=$($doc).find('Interface').eq(1).attr('path');
							}else if(RegExp('\\b'+ 'NEXT' +'\\b').test(CmbData2)==true){
							        MethodCapture=$($doc).find('Interface').eq(0).attr('path');
							        MethodInfo=$($doc).find('Interface').eq(1).attr('path');
							}

							if(CmbData1=='READY')
							{	
							 
							    $('#method').val( finalUrl+MethodCapture);
							    $('#info').val( finalUrl+MethodInfo);
							 
								SuccessFlag=1;
								
								    alert("Device detected successfully");
								     
        					
								return false;
							}
							else if(CmbData1=='USED')
							{	
							   $('#method').val( finalUrl+MethodCapture);
							   $('#info').val( finalUrl+MethodInfo);
							 
								SuccessFlag=1;
								
								 alert("Device detected successfully");
								     
        					
								return false;
							}
							
							
							else if(CmbData1=='NOTREADY')
							{
								alert("Device Not Discover");
								return false;								
							}	
						}

            },
            error: function (jqXHR, ajaxOptions, thrownError) {
              if (i == "8005" && OldPort == true) {
                OldPort = false;
                i = "11099";
              }
            },

          });

          if (SuccessFlag == 1) {
            break;
          }
        }

        if (SuccessFlag == 0) {
          alert("Connection failed Please try again.");
        } else {
          //alert("RDSERVICE Discover Successfully");
        }
        $("select#ddlAVDM").prop('selectedIndex', 0);
        return res;
      };
	  
	  
	  
	  
	  	function deviceInfoAvdm()
		{
		var GetCustomDomName = "127.0.0.1";
		var SuccessFlag = 0;
        var primaryUrl1 = "http://" + GetCustomDomName + ":";

        try {
          var protocol = window.location.href;
          if (protocol.indexOf("https") >= 0) {
            primaryUrl1 = "https://" + GetCustomDomName + ":";
          }
        } catch (e) {}

        url = "";
        SuccessFlag = 0;


		var finUrl=  $('#info').val();
        url = "";
		
			var err = "";

			var res;
			$.support.cors = true;
			var httpStaus = false;
			var jsonstr="";
			;
				$.ajax({

				type: "DEVICEINFO",
				async: false,
				crossDomain: true,
				url: finUrl,
				contentType: "text/xml; charset=utf-8",
				processData: false,
				success: function (data) {
					httpStaus = true;
					res = { httpStaus: httpStaus, data: data };
                    //document.getElementById"<%=txtDeviceInfo1.ClientID%>").value = data.toString();
					$('#txtDeviceInfo').val(data); ing.toString();
				},
				error: function (jqXHR, ajaxOptions, thrownError) {
				alert(thrownError);
					res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
				},
			});

			return res;

		}

	  
	  
	  
	  function CaptureAvdm()
		{
		DString = '';
       device="mantra";


			var strWadh="";
		    var strOtp="";
	     
	   
	   var XML='<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="0" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> '+DString+'<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';
 	  

            var finUrl=  $('#method').val();
			

					 var verb = "CAPTURE";


                        var err = "";

						var res;
						$.support.cors = true;
						var httpStaus = false;
						var jsonstr="";
						
							$.ajax({

							type: "CAPTURE",
							async: false,
							crossDomain: true,
							url: finUrl,
							data:XML,
							contentType: "text/xml; charset=utf-8",
							processData: false,
							success: function (data) {
							
							 if(device == "morpho"){
							   var xmlString = (new XMLSerializer()).serializeToString(data);  //morpho
							}else if(device == "mantra"){
								var xmlString = data;  //mantra
							}else if(device == "secugen"){
								var xmlString = (new XMLSerializer()).serializeToString(data);  //secugen
							}else if(device == "precision"){
								var xmlString = (new XMLSerializer()).serializeToString(data);  //precision
							}else if(device == "startek"){
								var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
							}else if(device == "nextrd"){
								  var xmlString = (new XMLSerializer()).serializeToString(data);  //next rd
							}
							httpStaus = true;
							res = { httpStaus: httpStaus, data: xmlString};
							
						
                                //document.getElementById"<%=txtPidData1.ClientID%>").value = xmlString.toString();
								$('#txtPidData').val(xmlString);
								var $doc = data;
								var Message =  $($doc).find('Resp').attr('errInfo');
								var errorcode =	 $($doc).find('Resp').attr('errCode');
									if(errorcode==0)
									{

										var $doc = $.parseXML(data);
										var Message =  $($doc).find('Resp').attr('errInfo');
										
										alert(Message);
										
									}else{
										$('#loaderbala').css("display","none");
										alert('Capture Failed');
										window.location.reload();
									}	

							},
							error: function (jqXHR, ajaxOptions, thrownError) {
							//$('#txtPidOptions').val(XML);
							alert(thrownError);
								res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
							},
						});

						return res;
		}
		
		function getHttpError(jqXHR) {
		    var err = "Unhandled Exception";
		    if (jqXHR.status === 0) {
		        err = 'Service Unavailable';
		    } else if (jqXHR.status == 404) {
		        err = 'Requested page not found';
		    } else if (jqXHR.status == 500) {
		        err = 'Internal Server Error';
		    } else if (thrownError === 'parsererror') {
		        err = 'Requested JSON parse failed';
		    } else if (thrownError === 'timeout') {
		        err = 'Time out error';
		    } else if (thrownError === 'abort') {
		        err = 'Ajax request aborted';
		    } else {
		        err = 'Unhandled Error';
		    }
		    return err;
		}


    </script>
					
 <asp:TextBox ID="txtDeviceInfo1" runat="server"   TextMode="MultiLine" Rows="30" Columns="50"       
         cssclass="form-control"></asp:TextBox> 
	

 <asp:TextBox ID="txtPidData1" runat="server"    TextMode="MultiLine" Rows="30" Columns="50"     
         cssclass="form-control"></asp:TextBox> 
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
