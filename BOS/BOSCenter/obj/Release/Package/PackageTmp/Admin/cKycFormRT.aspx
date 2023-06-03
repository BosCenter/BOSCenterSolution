<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="cKycFormRT.aspx.vb" Inherits="BOSCenter.cKycFormRT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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



    
    </script>



<div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-10 col-sm-offset-1'>
<div class='log_form_head1'>
<asp:Label ID='formheading_1' runat='server' Text='Card KYC Form'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>

<asp:Label ID='lblError' runat='server' Text=''></asp:Label> 

<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead4' runat='server' Text='Card Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>
<div runat="server" id="div_req" visible="false" >
<div class="row mar_top10">
<div class="col-md-2">
<div class="form-group">
<label for='txtStudentID'>  Request ID</label>
<asp:TextBox ID='txtRequestID' runat='server'  class='form-control' ReadOnly="True"></asp:TextBox>
</div>
</div>

<div class="col-md-2">
<div class="form-group">
<label for='txtRegistrationDate'>  Request Date</label>
<asp:TextBox ID='txtRequestDate' runat='server'  class='form-control'></asp:TextBox>
    <asp:CalendarExtender ID="txtRequestDate_CalendarExtender" runat="server" 
        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtRequestDate">
    </asp:CalendarExtender>
</div>
</div>

<div class="col-md-2">
<div class="form-group">
<label for='ddlActive'>Approved Status</label>
<asp:DropDownList ID='ddlApprovedStatus' runat='server' class='form-control'>
<asp:ListItem >Pending</asp:ListItem>
<asp:ListItem >Approved</asp:ListItem>
<asp:ListItem >Rejected</asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-md-6">
<div class="form-group">
<label for='txtRegistrationDate'>  Remarks</label>
<asp:TextBox ID='txtAdminRemarks' runat='server'  class='form-control' ReadOnly="True" ></asp:TextBox>
</div>
</div>

</div>

<div style='margin-bottom:10px;'>
</div>
</div>

<div class="row mar_top10">
<div class="col-md-3">
<div class="form-group">
<label for='txtStudentID'>  Salutation </label>
<asp:DropDownList ID='ddlSalutation' runat='server' class='form-control'>
<asp:ListItem >Mr.</asp:ListItem>
<asp:ListItem >Ms.</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtPassword'>  First Name</label>
<asp:TextBox ID='txtFirstName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for='txtRegistrationDate'>  Middle Name</label>
<asp:TextBox ID='txtMiddleName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for='ddlActive'>Last Name</label>
<asp:TextBox ID='txtLastName' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>
</section>

<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead3' runat='server' Text='Other Details'></asp:Label>
</div>

<div class='row'>
<div class='col-md-6'>
<div class='form-group'>
<label for='lblEmailID'>  Address</label>
<asp:TextBox ID='txtAddress' runat='server'  class='form-control'></asp:TextBox>
</div>
</div>
<div class='col-md-6'>
<div class='form-group'>
<label for='lblMobileNo'>  Phone No</label>
<asp:TextBox ID='txtPhoneNo' runat='server'  class='form-control' MaxLength="10"></asp:TextBox>
    <asp:FilteredTextBoxExtender ID="txtMobileNo_FilteredTextBoxExtender" 
        runat="server" FilterType="Numbers" TargetControlID="txtPhoneNo">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class='row'>
<div class='col-md-6'>
<div class='form-group'>
<label for='txtResidenceNo'>  Aadhar Number</label>
<asp:TextBox ID='txtaadharNo' runat='server'  class='form-control' 
        MaxLength="15"></asp:TextBox>
   
</div>
</div>
<div class='col-md-6'>
<div class='form-group'>
<label for='txtAddress'>  Card Number</label>
<asp:TextBox ID='txtCardNo' runat='server'  class='form-control'  MaxLength="16"></asp:TextBox>
</div>
</div>

</div>

<div class='row'>
<div class='col-md-6'>
<div class='form-group'>
<label for='txtResidenceNo'>  Reference Number</label>
<asp:TextBox ID='txtReferenceNo' runat='server'  class='form-control' 
        MaxLength="15"></asp:TextBox>
   
</div>
</div>
</div>

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
<label for='txtSignatureUpload'>  Finger Print Upload<asp:Label 
        ID="Label36" runat="server" Text=" *" ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label></label>
<%--<asp:FileUpload ID="FileUpload1" runat="server"  />--%>
 <div style="margin-bottom:5px;"></div>
    <asp:TextBox ID="txtfpdata" runat="server"  Enabled ="false" 
        placeholder='Figure Print Data' cssclass="form-control"></asp:TextBox>
    <div style="margin-bottom:5px;"></div>

     
     <asp:Button ID="btnCapture" runat="server" Text="Capture"   ValidationGroup="a"  OnClientClick="return Capture();"  />&nbsp;  
    &nbsp;<asp:Button ID="btnremove" runat="server"   Text="Remove" />
    <asp:Image ID="imgFinger" runat="server" BorderStyle="None" CssClass="" 
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblError_FigurePrint" runat="server"></asp:Label>
     
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
        Height="61px" ImageUrl="~/images/uploadimage.png" Width="71px" />
    <asp:Label ID="lblErrorImage_AddharCardBack" runat="server"></asp:Label>
</div>
</div>

</div>

<div class="row">

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
<%-- End - Upload Documents Section --%>
</section>

<div class='form-section'>
<div class='row'>
<div class='col-md-12'>
 <asp:Label ID='lblRID' runat='server' Text='' Visible='false'></asp:Label> 
 
</div>
</div>
<div class='row'>
<div class='col-md-12'>
<center> <asp:Button ID='btnSave' runat='server' Text='Submit' 
 cssclass='btn btn-primary mar_top10' ValidationGroup='a' />&nbsp;
 <asp:Button ID='btnDelete' runat='server' Text='Delete' 
 cssclass='btn btn-primary mar_top10' Enabled='False' />  &nbsp;
<asp:Button ID='btnClear' runat='server' Text='Reset' 
cssclass='btn btn-primary mar_top10' /> </center>
</div>
</div>
</div>
<div style='margin-top:5px;'>
</div>
</ContentTemplate>

    <Triggers>


<asp:PostBackTrigger ControlID="btnUpload_AddharCardBack"  />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_AddharCardBack"  EventName="Click"   />

<asp:PostBackTrigger ControlID="btnUpload_AddharCardFront"  />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_AddharCardFront"  EventName="Click"   />

<asp:PostBackTrigger ControlID="btnUpload_Photo"  />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_Photo"  EventName="Click"  />
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
