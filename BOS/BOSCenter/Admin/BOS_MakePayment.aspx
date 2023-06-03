<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_MakePayment.aspx.vb" Inherits="BOSCenter.BOS_MakePayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"><br />
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>
<div class ="container">
<div class ="row">
<div class ="col-sm-9 col-sm-offset-1">
<div class ="log_form_head1">
Make Payment
</div>
<div class ="form-section">
<div class ="row">
<div class ="col-sm-1">

</div>
<div class ="col-sm-2">
    <asp:Label ID="lblpaymentmode" CssClass="text-bold" runat="server" Text="Payment Mode:"></asp:Label>
</div>
<div class = "col-sm-9">
    <asp:RadioButton ID="rdbCash" runat="server" GroupName="a" Text="Cash Deposit" AutoPostBack ="true"/>
    <asp:RadioButton ID="rdbCheque" runat="server" GroupName="a" Text="UPI/IMPS" AutoPostBack ="true" />
    <asp:RadioButton ID="rdbNetBanking" runat="server" GroupName="a" Text="Net Banking" AutoPostBack ="true"/>
    <asp:RadioButton ID="rdbATMTransfer" runat="server" GroupName="a" Text="CDM Deposit" AutoPostBack ="true"/>
    <asp:RadioButton ID="rdbPosMachine" runat="server" GroupName="a" Text="RTGS/NEFT" AutoPostBack ="true"/>
    <asp:RadioButton ID="rdbOthers" runat="server" GroupName="a" Text="Others" AutoPostBack ="true"/>
<%--<asp:RadioButtonList ID="rdbPaymentMode" runat="server" 
        RepeatDirection="Horizontal">
        <asp:ListItem>Cash&nbsp;</asp:ListItem>
        <asp:ListItem>Cheque&nbsp;</asp:ListItem>
        <asp:ListItem>Net Banking&nbsp;</asp:ListItem>
        <asp:ListItem>ATM Transfer&nbsp;</asp:ListItem>
     
    </asp:RadioButtonList>--%>
</div>
</div>
<div class ="row mar_top5">
<div class ="col-sm-1">

</div>
<div class ="col-sm-3">
    <asp:Label ID="lblamount" CssClass="text-bold" runat="server" Text="Amount:"></asp:Label>
</div>
<div class ="col-sm-4">
<asp:TextBox ID="txtamount" CssClass="form-control" runat="server"></asp:TextBox>
  <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
        runat="server" Enabled="True" 
        TargetControlID="txtamount" FilterType="Custom"  ValidChars="1234567890.">
    </asp:FilteredTextBoxExtender>
</div>
<div class ="col-sm-3 text-red">
<asp:Label ID="lblMinimumAmt" CssClass="text-bold" runat="server" Text=""></asp:Label>
</div>
<div class ="col-sm-1">

</div>
</div>
<div class ="row mar_top5">
<div class ="col-sm-1">

</div>
<div class ="col-sm-3">
    <asp:Label ID="lbldb" CssClass="text-bold" runat="server" Text="Depositing Bank:"></asp:Label>
</div>
<div class ="col-sm-4">
<asp:DropDownList ID="ddnbanklist" CssClass="form-control" runat="server">
        <asp:ListItem>--Select Bank--</asp:ListItem>
    </asp:DropDownList>
</div>
<div class ="col-sm-4">

</div>

</div>
<div class ="row mar_top5">
<div class ="col-sm-1">

</div>
<div class ="col-sm-3">
    <asp:Label ID="lblbranchcode" CssClass="text-bold" runat="server" Text="Branch Code / Name:"></asp:Label>
</div>
<div class ="col-sm-4">
<asp:TextBox ID="txtbranchcode" CssClass="form-control" runat="server"></asp:TextBox>
</div>
<div class ="col-sm-4">

</div>

</div>
<div class ="row mar_top5">
<div class ="col-sm-1">

</div>
<div class ="col-sm-3 mar_top10">
    <asp:Label ID="lblremarks" CssClass="text-bold" runat="server" Text="Remarks:"></asp:Label>

</div>
<div class ="col-sm-4">
<asp:TextBox ID="txtremarks" CssClass="form-control" runat="server" 
        TextMode="MultiLine"></asp:TextBox>
</div>
<div class ="col-sm-4">
</div>

</div>
<div class ="row mar_top5">
<div class ="col-sm-1">

</div>
<div class ="col-sm-3">
    <asp:Label ID="lbltid" CssClass="text-bold" runat="server" Text="Transaction ID:"></asp:Label>
</div>
<div class ="col-sm-4">
<asp:TextBox ID="txttransactionid" CssClass="form-control" runat="server"></asp:TextBox>
</div>
<div class ="col-sm-4">
</div>

</div>
<div class ="row mar_top5" runat="server" id="Div_Upload">
<div class ="col-sm-1">

</div>
<div class ="col-sm-3">
    <asp:Label ID="lbluploadslip" CssClass="text-bold" runat="server" Text="Upload Slip:"></asp:Label>
</div>
<div class ="col-sm-4">
<div class="form-group">

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
<div class ="col-sm-4">
</div>

</div>
<div class ="row mar_top10">
<div class ="col-sm-2">
&nbsp;
</div>
<div class ="col-sm-8">
    <asp:Label ID="lblRID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblRefrenceID" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID='lblError' runat='server' Text=''></asp:Label> 
</div>
    
<div class ="col-sm-2">
</div>

</div>
<div class ="row mar_top10">
<div class ="col-sm-4">

</div>
<div class ="col-sm-4">
    <asp:Button ID="btnsubmit" CssClass="btn btn-primary" runat="server" Text="Submit" />&nbsp;
    <asp:Button ID="btnReset" CssClass="btn btn-primary" runat="server" Text="Reset" />
</div>
<div class ="col-sm-4">
</div>

</div>
</div>
</div>
</div>
</div>
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

    </ContentTemplate> 


    <Triggers>

<asp:PostBackTrigger ControlID="btnUpload_PanCard" />
<asp:AsyncPostBackTrigger ControlID="btnDeleteUpload_PanCard" EventName="Click"  />

</Triggers> 
</asp:UpdatePanel> 
</asp:Content>
