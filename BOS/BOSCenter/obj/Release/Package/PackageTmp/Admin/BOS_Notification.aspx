<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_Notification.aspx.vb" Inherits="BOSCenter.BOS_Notification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function disableSubmit() {
            //document.getElementById('<%= lblError.ClientID %>').style.display = "";

            var t = setTimeout("enableSubmit()", 3000);
        }

        function enableSubmit() {
            document.getElementsByName("lblError").value = "";

            document.getElementById('<%= lblError.ClientID %>').style.display = "none";
        }
   
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel runat='server' ID='updatepanel2'>
<ContentTemplate>
<div style='margin-top:15px;'></div>
<div class='container'>
<div class='col-sm-12'>
<div class='log_form_head1'>
<asp:Label ID='formheading3' runat='server' Text='Add Notification Form'></asp:Label>
</div>
<div class='log_form1'>
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>


<section>
<div class='row'>
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead3' runat='server' Text='Notification Details'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>
<div class='form-group'>
<div class='row'>
<div class='col-md-3' >
<label for='txtProduct_Date'>  Notification ID <label for='txtProduct_Date' style="color:Red;">  *</label></label>
</div>
<div class='col-md-9'>
<asp:TextBox ID='txtNotificationID' runat='server'  placeholder='Notification ID' 
        ReadOnly="true" class='form-control'></asp:TextBox>
</div>
</div>
</div>
<%--<div class='form-group'>
<div class='row'>
<div class='col-md-3' >
<label for='txtProduct_Date'>  Notification Date <label for='txtProduct_Date' style="color:Red;">  *</label></label>
</div>
<div class='col-md-9'>
<asp:TextBox ID='txtNotification_Date' runat='server'  
        placeholder='Notification_Date' class='form-control'></asp:TextBox>
 <asp:CalendarExtender ID="txtDOJ_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtNotification_Date" TodaysDateFormat="" 
        Format="dd/MM/yyyy">
    </asp:CalendarExtender>
</div>
</div>
</div>--%>
<div class='form-group'>
<div class='row'>
<div class='col-md-3' >
<label for='ddlProduct_BrandCode'>  Select Agent <label for='txtProduct_Date' style="color:Red;">  *</label></label>
</div>
<div class='col-md-9'>
<asp:DropDownList ID='ddl_AgentType' runat='server' class='form-control'>
<asp:ListItem>:: Select Agent ::</asp:ListItem>
<asp:ListItem>Master Distributor</asp:ListItem>
<asp:ListItem>Distributor</asp:ListItem>
<%--<asp:ListItem>Sub Distributor</asp:ListItem>--%>
<asp:ListItem>Retailer</asp:ListItem>
<asp:ListItem>Customer</asp:ListItem>
</asp:DropDownList>

</div>
</div>
</div>
<div style='margin-top:5px;'></div>
<div class='form-group'>
<div class='row'>
<div class='col-md-3' >
<label for='txtProduct_OrderCategory'>  Active Status <label for='txtProduct_Date' style="color:Red;">  *</label></label>
</div>
<div class='col-md-9'>
<asp:DropDownList ID='ddl_ActiveStatus' runat='server' class='form-control'>
<asp:ListItem>:: Select Status ::</asp:ListItem>
<asp:ListItem>Active</asp:ListItem>
<asp:ListItem>Inactive</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>
<div style='margin-top:5px;'></div>

<div class='form-group'>
<div class='row'>
<div class='col-md-3' >
<label for='txtProduct_Descp'>  Notification Banner <label for='txtProduct_Date' style="color:Red;">  *</label></label>
</div>
<div class='col-md-5'>
<div class="clearfix" style="margin-bottom:15px;"></div>
<asp:FileUpload ID="FileUpload_OroginalPic" runat="server"  />
<div class="clearfix" style="margin-bottom:10px;"></div>
    <asp:Button ID="btnUpload_OriginalPic" runat="server" 
        Text="Upload" />
    &nbsp;<asp:Button ID="btnRemove_OroginalPic" runat="server" 
         Text="Remove" Enabled="False" EnableTheming="True" />
</div>
    <div class="col-sm-4">
        
    <asp:Image ID="Image_OriginalPic" runat="server" BorderStyle="None" CssClass="" 
        Height="180px" ImageUrl="~/images/uploadimage.png" Width="180px" />
    <asp:Label ID="lblErrorImage_OriginalPic" runat="server"></asp:Label>
        </div>
</div>
</div>
<div class='form-group'>
<div class='row'>
<div class='col-md-6'>
<asp:UpdatePanel ID='UpdatePanel1'  runat='server'>
<ContentTemplate>
<asp:ImageButton ID='ImagebtnExcel' runat='server' Visible="false"  width='32px' Height='32px' 
ImageUrl='~/images/excel_32X32.png'/>
<asp:ImageButton ID='ImagebtnWOrd' runat='server'  Visible="false"  width='32px' Height='32px' 
ImageUrl='~/images/word_32X32.png'/>
<asp:ImageButton ID='Imagepdf' runat='server'  Visible="false"  width='32px' Height='32px' 
ImageUrl='~/images/pdf_32X32.png'/>
</ContentTemplate> 
<Triggers>
<asp:PostBackTrigger ControlID='ImagebtnExcel' />
<asp:PostBackTrigger ControlID='ImagebtnWOrd' />
<asp:PostBackTrigger ControlID='Imagepdf' />
 </Triggers>
</asp:UpdatePanel> 
 </div>
<div class='col-md-6'>
<label class='col-sm-2'></label>
<div class='col-sm-4 pull-right'>
<asp:DropDownList ID='ddlNoOfRecords' runat='server'  
cssclass='form-control inputtext' AutoPostBack='True' >
<asp:ListItem>10 Record(s)</asp:ListItem>
 <asp:ListItem>25 Record(s)</asp:ListItem>
 <asp:ListItem>50 Record(s)</asp:ListItem>
 <asp:ListItem>100 Record(s)</asp:ListItem>
 <asp:ListItem>200 Record(s)</asp:ListItem>
<asp:ListItem>500 Record(s)</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>
</div>
<div style='margin-bottom:10px;'></div>
</div>
</div>
</div>
</section>


<div class='form-section'>
<div class='row'>
<div class='col-md-12'>
<asp:Label ID='lblOld_value' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblRID' runat='server' Text='' Visible='false'></asp:Label>
 <asp:Label ID='lblExportQry' runat='server' Text='' Visible='false'></asp:Label> 
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
cssclass='btn btn-primary mar_top10' /> </center>
</div>
</div>
</div>
<div style='margin-top:5px;'>
</div>
</ContentTemplate>
</asp:UpdatePanel></div>
</div>
</div>

<div class='container'>
<div class='col-md-12'>
<div class='row'>
<div class='col-md-12'>
<div class='log_form_head1' Style='Text-align:left'>
<asp:Label ID='gridheading2' runat='server' Text='Search Details'></asp:Label>
</div>
</div>
</div>
<div class='row'>
<div class='col-md-12'>
<div class='table-responsive'>
<div class='table_wid'>
<asp:GridView ID='GridView1' runat='server' ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" EmptyDataText=":: No Records Found ::" EmptyDataRowStyle-Font-Bold="true" BorderStyle="None" AllowPaging="True" cssclass="grid-view-themeclass">
<Columns> 
<asp:TemplateField>
<ItemTemplate>
<asp:Label ID='lblgrdRID' runat='server' Text='<%# Eval("SrNo") %>' Visible='false' ></asp:Label>
<asp:LinkButton ID='LinkButton1' OnClick='btnGrdRowdelete_Click' CssClass='btn btn-danger' runat='server'><i class='fa fa-trash'></i></asp:LinkButton>
<asp:LinkButton ID='LinkButton2' OnClick='GridView1_SelectedIndexChanged' CssClass='btn btn-primary' runat='server'><i class='fa fa-pencil'></i></asp:LinkButton>

</ItemTemplate>
 <ItemStyle Width="100px" />
</asp:TemplateField>
 <asp:BoundField  HeaderText="SrNo" DataField="SrNo" />
    <asp:BoundField  HeaderText="NotificationID" DataField="NotificationID" />
      <asp:ImageField NullDisplayText="No Image" ControlStyle-Height="120px" ControlStyle-Width="120px"  DataImageUrlField="NotificationPic" HeaderText="NotificationPic" >
    </asp:ImageField>
    <asp:BoundField  HeaderText="NotificationDate" DataField="NotificationDate" />
    <asp:BoundField  HeaderText="AgentType" DataField="AgentType" />
    <asp:BoundField  HeaderText="ActiveStatus" DataField="ActiveStatus" />
  


</Columns>
  <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
</asp:GridView>
</div>
</div>
</div>
</div>
</div>
</div>
</ContentTemplate>



      <Triggers>

<asp:PostBackTrigger ControlID="btnUpload_OriginalPic" />
<asp:AsyncPostBackTrigger ControlID="btnRemove_OroginalPic" EventName="Click"  />


</Triggers>
</asp:UpdatePanel>

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
<asp:Button ID='btnPopupYes' runat='server' Text='OK' Width='80px' CssClass='btn btn-primary' OnClientClick="disableSubmit()"/>
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

<asp:Button ID="bre" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="bre" PopupControlID="InformationPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelInfo" >
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

</asp:Content>
