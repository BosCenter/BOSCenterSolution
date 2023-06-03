<%@ Page  Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_AEPS_PS.aspx.vb" Inherits="BOSCenter.Frm_AEPS_PS"   validateRequest="false"   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<%--<script  src="https://code.jquery.com/jquery-3.5.1.min.js" integrity="sha256-9/aliU8dGd2tb6OSsuzixeV4y/faTqgFtohetphbbj0=" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<script type="text/javascript"  type="text/javascript">
    window.onload = getLocation();
    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition, showError);
        } else
        {
            //navigator.geolocation.getCurrentPosition(showPosition, showError);
        }
        
    }

    function showPosition(position) {
        var latlondata = position.coords.latitude + "," + position.coords.longitude;
        var latlon = "Your Latitude Position is:=" + position.coords.latitude + "," + "Your Longitude Position is:=" + position.coords.longitude;
        //alert(latlon)
        document.getElementById("<%=txtLong.ClientID%>").value = position.coords.longitude;
        document.getElementById("<%=txtLat.ClientID%>").value = position.coords.latitude;
    }
    function showError(error) {
        if (error.code == 1) {
            alert("User denied the request for Geolocation.");
        }
        else if (err.code == 2) {
            alert("Location information is unavailable.");
        }
        else if (err.code == 3) {
            alert("The request to get user location timed out.");
        }
        else {
            alert("An unknown error occurred.");
        }
    }

    
</script>

<script language="javascript" type="text/javascript">


    var quality = 60; //(1 to 100) (recommanded minimum 55)
    var timeout = 10; // seconds (minimum=10(recommanded), maximum=60, unlimited=0 )



    function Capture() {
        try {


            document.getElementById("<%=txtfpdata.ClientID%>").value = "";


            var res = CaptureFinger(quality, timeout);
            if (res.httpStaus) {

                ////               document.getElementById("<%=lblError_FigurePrint.ClientID%>").value = "ErrorCode: " + res.data.ErrorCode + " ErrorDescription: " + res.data.ErrorDescription;
                debugger;
                if (res.data.ErrorCode == "0") {
                    var fpdaa = "data:image/bmp;base64," + res.data.BitmapData;
                    document.getElementById("<%=imgFinger.ClientID%>").src = fpdaa.toString();

                    var str = res.data.IsoTemplate;
                    document.getElementById("<%=txtfpdata.ClientID%>").value = str.toString() + "~" + fpdaa.toString();


                }
            }
            else {
                alert(res.err);
            }



        }
        catch (e) {
            alert(e);
        }
        return false;
    };


    // PaySprint Functions
    function discoverAvdm() {
        var GetCustomDomName = "127.0.0.1";
        var SuccessFlag = 0;
        var primaryUrl = "https://" + GetCustomDomName + ":";

        try {
            var protocol = window.location.href;
            if (protocol.indexOf("https") >= 0) {
                primaryUrl = "https://" + GetCustomDomName + ":";
            }
        } catch (e) { }

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
                    document.getElementById("<%=txtDeviceInfo.ClientID%>").value = data.toString();
                    //$("#txtDeviceInfo").val(data);
                    finalUrl = primaryUrl + i.toString();
                    var $doc = $.parseXML(data);//$data
                    debugger;
                    var CmbData1 = $($doc).find('RDService').attr('status');
                    var CmbData2 = $($doc).find('RDService').attr('info');

                    if (RegExp('\\b' + 'Mantra' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'Morpho_RD_Service' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'SecuGen India Registered device Level 0' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'Precision - Biometric Device is ready for capture' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'RD service for Startek FM220 provided by Access Computech' + '\\b').test(CmbData2) == true || RegExp('\\b' + 'NEXT' + '\\b').test(CmbData2) == true) {

                        debugger;
                        console.log($($doc).find('Interface').eq(0).attr('path'));

                        if (RegExp('\\b' + 'Mantra' + '\\b').test(CmbData2) == true) {

                            if ($($doc).find('Interface').eq(0).attr('path') == "/rd/capture") {
                                MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                            }
                            if ($($doc).find('Interface').eq(1).attr('path') == "/rd/capture") {
                                MethodCapture = $($doc).find('Interface').eq(1).attr('path');
                            }
                            if ($($doc).find('Interface').eq(0).attr('path') == "/rd/info") {
                                MethodInfo = $($doc).find('Interface').eq(0).attr('path');
                            }
                            if ($($doc).find('Interface').eq(1).attr('path') == "/rd/info") {
                                MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                            }
                        } else if (RegExp('\\b' + 'Morpho_RD_Service' + '\\b').test(CmbData2) == true) {
                            MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                            MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        } else if (RegExp('\\b' + 'SecuGen India Registered device Level 0' + '\\b').test(CmbData2) == true) {
                            MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                            MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        } else if (RegExp('\\b' + 'Precision - Biometric Device is ready for capture' + '\\b').test(CmbData2) == true) {
                            MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                            MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        } else if (RegExp('\\b' + 'RD service for Startek FM220 provided by Access Computech' + '\\b').test(CmbData2) == true) {
                            MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                            MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        } else if (RegExp('\\b' + 'NEXT' + '\\b').test(CmbData2) == true) {
                            MethodCapture = $($doc).find('Interface').eq(0).attr('path');
                            MethodInfo = $($doc).find('Interface').eq(1).attr('path');
                        }

                        if (CmbData1 == 'READY') {

                            $('#method').val(finalUrl + MethodCapture);
                            $('#info').val(finalUrl + MethodInfo);

                            SuccessFlag = 1;

                            alert("Device detected successfully");


                            return false;
                        }
                        else if (CmbData1 == 'USED') {
                            $('#method').val(finalUrl + MethodCapture);
                            $('#info').val(finalUrl + MethodInfo);

                            SuccessFlag = 1;

                            alert("Device detected successfully");


                            return false;
                        }


                        else if (CmbData1 == 'NOTREADY') {
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




    function deviceInfoAvdm() {
        var GetCustomDomName = "127.0.0.1";
        var SuccessFlag = 0;
        var primaryUrl1 = "https://" + GetCustomDomName + ":";

        try {
            var protocol = window.location.href;
            if (protocol.indexOf("https") >= 0) {
                primaryUrl1 = "https://" + GetCustomDomName + ":";
            }
        } catch (e) { }

        url = "";
        SuccessFlag = 0;


        var finUrl = $('#info').val();
        url = "";

        var err = "";

        var res;
        $.support.cors = true;
        var httpStaus = false;
        var jsonstr = "";
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
                //$('#txtDeviceInfo').val(data);
                document.getElementById("<%=txtDeviceInfo.ClientID%>").value = data;
            },
            error: function (jqXHR, ajaxOptions, thrownError) {
                alert(thrownError);
                res = { httpStaus: httpStaus, err: getHttpError(jqXHR) };
            },
        });

        return res;

    }




    function CaptureAvdm() {
        DString = '';
        device = "mantra";


        var strWadh = "";
        var strOtp = "";


        var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="2" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';

        

        var finUrl = $('#method').val();


        var verb = "CAPTURE";


        var err = "";

        var res;
        $.support.cors = true;
        var httpStaus = false;
        var jsonstr = "";

        $.ajax({

            type: "CAPTURE",
            async: false,
            crossDomain: true,
            url: finUrl,
            data: XML,
            contentType: "text/xml; charset=utf-8",
            processData: false,
            success: function (data) {

                if (device == "morpho") {
                    var xmlString = (new XMLSerializer()).serializeToString(data);  //morpho
                } else if (device == "mantra") {
                    var xmlString = data;  //mantra
                } else if (device == "secugen") {
                    var xmlString = (new XMLSerializer()).serializeToString(data);  //secugen
                } else if (device == "precision") {
                    var xmlString = (new XMLSerializer()).serializeToString(data);  //precision
                } else if (device == "startek") {
                    var xmlString = (new XMLSerializer()).serializeToString(data);  //startek
                } else if (device == "nextrd") {
                    var xmlString = (new XMLSerializer()).serializeToString(data);  //next rd
                }
                httpStaus = true;
                res = { httpStaus: httpStaus, data: xmlString };


                document.getElementById("<%=txtPidData.ClientID%>").value = xmlString.toString();
                //$('#txtPidData').val(xmlString);
                var $doc = data;
                var Message = $($doc).find('Resp').attr('errInfo');
                var errorcode = $($doc).find('Resp').attr('errCode');
                if (errorcode == 0) {

                    var $doc = $.parseXML(data);
                    var Message = $($doc).find('Resp').attr('errInfo');

                    alert(Message);

                } else {
                    $('#loaderbala').css("display", "none");
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



<div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-10 col-sm-offset-1'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='AEPS Form'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>

<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead4' runat='server' Text='Request Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>
<div runat="server" id="div_req">
<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtStudentID'>  Reference No.</label>
<asp:TextBox ID='txtTransID' runat='server'  class='form-control' ReadOnly="True"></asp:TextBox>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for='txtRegistrationDate'>  Date</label>
<asp:TextBox ID='txtTransDate' runat='server'  readonly="true" class='form-control'></asp:TextBox>
    <asp:CalendarExtender ID="txtTransDate_CalendarExtender" runat="server" 
        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTransDate">
    </asp:CalendarExtender>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for='txtStudentID'> API CALL</label>
<asp:DropDownList ID='ddlAPI_Call' runat='server' class='form-control' autopostback="true">
<asp:ListItem >AEPS</asp:ListItem>
<asp:ListItem >AADHAR PAY </asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for='txtStudentID'> Bank</label>
<asp:DropDownList ID='ddlBankList' runat='server' class='form-control'>
<asp:ListItem value=":: Select Bank ::" text=":: Select Value ::" ></asp:ListItem>
<asp:ListItem >Axis Bank</asp:ListItem>
<asp:ListItem >Bank Of Baroda</asp:ListItem>
<asp:ListItem >Punjab National Bank</asp:ListItem>
</asp:DropDownList>
</div>
</div>




</div>

<div style='margin-bottom:10px;'>
</div>
</div>

<div class="row mar_top10">

<div class="col-md-3">
<div class="form-group">
<label for='txtPassword'>  Mobile Number</label>
<asp:TextBox ID='txtMobileNumber' runat='server'  class='form-control' MaxLength="10"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtMobileNumber_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMobileNumber">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtRegistrationDate'>  Adhaar Number</label>
<asp:TextBox ID='txtAdhaarNumber' runat='server'  class='form-control' MaxLength="12"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtAdhaarNumber_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtAdhaarNumber">
    </asp:FilteredTextBoxExtender>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for='txtStudentID'>  Transaction Type </label>
<asp:DropDownList ID='ddlTransactionType' runat='server' autopostback="true" class='form-control'>
<asp:ListItem value=":: Select Type ::" text=":: Select Value ::" ></asp:ListItem>
<asp:ListItem value="BE" text="Balance Enquiry" ></asp:ListItem>
<asp:ListItem  value="CW" text="Cash Withdraw"></asp:ListItem>
<asp:ListItem  value="MS" text="Mini Statement"></asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<div runat="server" id="div_Amount" > 
<label for='ddlActive'>Amount</label>
<asp:TextBox ID='txtAmount' runat='server'  class='form-control' MaxLength="5"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtAmount_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtAmount">
    </asp:FilteredTextBoxExtender>

</div>
</div>
</div>
</div>
<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>
</section>




<section>
 <%-- Start - Upload Documents Section --%>
 <div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
     Finger print  </div>

<div style="margin-bottom:5px;"></div>


<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label for='txtSignatureUpload'>  Finger Print Scan<asp:Label 
        ID="Label36" runat="server" Text=" *" ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label></label>
<%--<asp:FileUpload ID="FileUpload1" runat="server"  />--%>
 <div style="margin-bottom:5px;"></div>
    <asp:TextBox ID="txtfpdata" runat="server"  Enabled ="false" 
        placeholder='Figure Print Data' visible="false" cssclass="form-control"></asp:TextBox>
    <div style="margin-bottom:5px;"></div>

     
     <asp:Button ID="btnCapture" runat="server"  visible="false"  Text="Capture"   ValidationGroup="a"  OnClientClick="return Capture();"  />&nbsp;  
    &nbsp;<asp:Button ID="btnremove" runat="server"  visible="false"    Text="Remove" />
    <asp:Image ID="imgFinger" runat="server"  visible="false"  BorderStyle="None" CssClass="" 
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblError_FigurePrint" runat="server"  visible="false" ></asp:Label>
 <asp:TextBox ID="txtLong" runat="server"  Enabled ="false"  style="display:none;" 
         cssclass="form-control"></asp:TextBox>
 <asp:TextBox ID="txtLat" runat="server"  Enabled ="false"  style="display:none;" 
         cssclass="form-control"></asp:TextBox>
&nbsp;
<input id="method" type="hidden" value="">
<input id="info" type="hidden" value="">
<input type="hidden" name="txtWadh" id="txtWadh">
    <button type="button" class="btn btn-danger" onclick="discoverAvdm();">CHECK DEVICE</button>

	
    <button type="button" class="btn btn-warning"   onclick="CaptureAvdm();">SCAN</button>

	
    <button type="button" class="btn btn-warning" style="display:none;"   onclick="deviceInfoAvdm();">info</button>
		
 <asp:TextBox ID="txtDeviceInfo" runat="server"   TextMode="MultiLine" Rows="30" Columns="50"  Enabled ="false" style="display:none;"    
         cssclass="form-control"></asp:TextBox> 
	

 <asp:TextBox ID="txtPidData" runat="server"    TextMode="MultiLine" Rows="30" Columns="50" Enabled ="false" style="display:none;"   
         cssclass="form-control"></asp:TextBox> 

</div>
</div>
</div>
</div>
</div>
</div>

</div>
</div>
</div>
<%-- End - Upload Documents Section --%>
</section>

<div class='form-section'>
<div class='row'>
<div class='col-md-12'>
 <asp:Label ID='lblRID' runat='server' Text='' Visible='false'></asp:Label> 
 
<asp:Label ID='lblError' runat='server' Text=''></asp:Label> 

</div>
</div>
<div class='row'>
<div class='col-md-12'>
<center> <asp:Button ID='btnSave' runat='server' Text='Save' 
 cssclass='btn btn-primary mar_top10' ValidationGroup='a' />&nbsp;
 <asp:Button ID='btnDelete' runat='server' Text='Delete' 
 cssclass='btn btn-primary mar_top10' Enabled='False' />  &nbsp;
<asp:Button ID='btnClear' runat='server' Text='Reset' 
cssclass='btn btn-primary mar_top10' />
  &nbsp;
<asp:Button ID='btnOnboard' runat='server' Text='OnBoard' 
cssclass='btn btn-danger mar_top10' />
 &nbsp;   &nbsp;
<asp:Button ID='btnChkOnBaord' runat='server' Text=' Chk OnBoard' 
cssclass='btn btn-danger mar_top10' />
 &nbsp;
    <br />
</center>
</div>
</div>
</div>
<div style='margin-top:5px;'>

<section>
 <%-- Start - Upload Documents Section --%>
 <div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
     Details:-  </div>

<div style="margin-bottom:5px;"></div>


<div class="row">
<div class="col-md-12">

<div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="Divgrid" style="overflow:scroll; ">
 <asp:GridView ID="grdResponseDetails" runat="server" cssclass="grid-view-themeclass" AutoGenerateColumns="true" ShowFooter="true"
                            BorderStyle="None" PageSize="100">
 <Columns>                           
                     
        <asp:TemplateField HeaderText="">
        
                                  <ItemTemplate>
                                    

                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" visible="false" CssClass="btn btn-danger mar_top5" 
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                         <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"  visible="false"  CssClass="btn btn-default" 
                                          CommandName="Select" Text=""><i class="fa fa-pencil-square-o fa-lg"></i></asp:LinkButton>
                                       
                                  </ItemTemplate>
                              </asp:TemplateField>
                           
          </Columns>   
          
          <FooterStyle Font-Size="Large" Font-Bold="true" ForeColor="blue" />
                           
                            </asp:GridView>
                            </div>
                            </div></div> 

</div>
</div>
</div>

</div>
</div>
</div>
<%-- End - Upload Documents Section --%>
</section>


</div>
</ContentTemplate>

    <Triggers>
    <%--<asp:PostBackTrigger ControlID="btnUpload_Photo" />--%>
    </Triggers>

</asp:UpdatePanel></div>
</div>
</div>
 <asp:Button ID='modalPopupButton' runat='server' Text='Button' style='display:none;'/>
<asp:ModalPopupExtender ID='ModalPopupExtender1' runat='server' TargetControlID='modalPopupButton' PopupControlID='DeletePopup'  BackgroundCssClass='modalBackground'  CancelControlID='btnCancel' >
</asp:ModalPopupExtender>
<asp:Panel ID='DeletePopup' runat='server' Width='350px' style='display:none;'  >
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
           <asp:Label ID="lblDeleteDocumentInfo"  Visible="false"  runat="server" Text=""></asp:Label>
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

 

</asp:Content>
