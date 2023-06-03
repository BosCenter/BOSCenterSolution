<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_AddMoneyToWallet.aspx.vb" Inherits="BOSCenter.BOS_AddMoneyToWallet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style='margin-top:15px;'>f</div>


<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>
<div class='container'>
<div class='col-sm-10 col-sm-offset-1'>
<div class='log_form_head1'>
<asp:Label ID='formheading3' runat='server' Text=':: Add Money Form ::'></asp:Label>
</div>
<div class='log_form1'>

<div class='row' runat="server" id="Div_SearchCustomer">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head' id="Wallet_Heading" runat="server" >
<asp:Label ID='lblformsectionhead3' runat='server' Text='Add Money To BOS Wallet'></asp:Label>
</div>
    

<div class="row mar_top10" id="Wallet_Row" runat="server">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter Amount</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtAmount' runat='server' placeholder=' Enter Amount' MaxLength="10" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtAmount">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
    <asp:Button ID="btnProceedToAddMoney" CssClass="btn btn-primary" runat="server" 
        Text="Proceed To Add Money" />&nbsp;<asp:Button ID="btnClear" 
        CssClass="btn btn-warning" runat="server" Text="Clear" />
    &nbsp;&nbsp;
</div>
</div>
</div>

<div class="row">
<div class="col-md-12">
<center>
<asp:Label ID="lblError" runat="server"></asp:Label><br /><br />
<asp:Label ID="lblOtherInfo" runat="server"></asp:Label>
<br /><br />

<asp:Button ID="btnRedirect" Visible="false"  CssClass="btn btn-primary" runat="server" 
        Text="OK" />

        </center>
<asp:Label ID="Label5" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="Label6" runat="server" Text="" Visible="false"></asp:Label>


<asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
</div>
</div>


</div>
</div>
</div>


<div style='margin-top:5px;'></div>
</div</div></div>
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
    <tr>
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

<asp:Panel ID="InformationPopup" runat="server" Width="350px" style="display:none;"    >
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
        <td align="left">
        <div  id="DIV_PrintReceipt">        
            <asp:Label ID="Label16" runat="server" CssClass="mar_lft30" Text="DateTime : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopDateTime" runat="server" ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="lblInformation" runat="server" CssClass="mar_lft30" Text="AgencyName : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopAgencyName" runat="server" ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label17" runat="server" CssClass="mar_lft30" Text="TransactionId : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopTransactionID" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label22" runat="server" CssClass="mar_lft30" Text="AccountNo : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopAccountNo" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label19" runat="server" CssClass="mar_lft30" Text="TransferAmt : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopTransferAmt" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label24" runat="server" CssClass="mar_lft30" Text="ServiceCharge : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopServiceCharge" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label21" runat="server" CssClass="mar_lft30" Text="Status : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopStatus" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label>
            </div>
        </td>
       
    </tr>
  
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnCancelInfo" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" /> &nbsp; <asp:Button ID="btnPrintReceipt" runat="server" Text="Print Receipt" Width="120px" CssClass="btn btn-danger" />
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
            <asp:Button ID="btnDelOk" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"/>
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




</asp:UpdatePanel>
</asp:Content>
