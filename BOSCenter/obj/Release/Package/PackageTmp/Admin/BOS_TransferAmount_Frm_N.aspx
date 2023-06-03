<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_TransferAmount_Frm_N.aspx.vb" Inherits="BOSCenter.BOS_TransferAmount_Frm_N" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
<br />
<div class="container">
<div class="row" runat="server" id="Div_MyAcountdetails" visible="false">

<div class="col-sm-8 col-sm-offset-2">
<div class='log_form_head1'>
<asp:Label ID='formheading3' runat='server' Text='Transfer Amount Form'></asp:Label>
</div>
<div class="form-section">
<div class="form-section-head">

    <asp:Label ID="Label1" runat="server" Font-Size="medium" Font-Bold="true" Text="My Account Details"></asp:Label>
</div>
<br />
   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-3">
             <asp:Label ID="Label2" runat="server"  Font-Bold="true" Text="My Main Balance"></asp:Label>
   
         </div>

         <div class="col-sm-9">
             <asp:TextBox ID="txtMainBalance" ReadOnly="true" cssclass="form-control" runat="server"></asp:TextBox>
              
         </div>
         


   </div>
   
   </div>
   <br />
   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-3">
             <asp:Label ID="Label3" runat="server"  Font-Bold="true" Text="My Credit Limit"></asp:Label>
   
         </div>

         <div class="col-sm-9">
             <asp:TextBox ID="txtMyCreditLimit" ReadOnly="true" cssclass="form-control" runat="server"></asp:TextBox>
            
         </div>



   </div>
   
   </div>
   <br />
   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-3">
             <asp:Label ID="Label4" runat="server"  Font-Bold="true" Text="Available Credit"></asp:Label>
   
         </div>

         <div class="col-sm-9">
             <asp:TextBox ID="txtAvailableCredit" ReadOnly="true" cssclass="form-control" runat="server"></asp:TextBox>
                 
         </div>



   </div>
   
   </div>
   <br />
   
      <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-3">
             <asp:Label ID="Label13" runat="server"  Font-Bold="true" Text="Hold Amount"></asp:Label>
   
         </div>

         <div class="col-sm-9">
             <asp:TextBox ID="txtHoldAmt" ReadOnly="true" cssclass="form-control" runat="server"></asp:TextBox>
                 
         </div>



   </div>
   
   </div>
   <br />
   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-3">
             <asp:Label ID="Label5" runat="server"  Font-Bold="true" Text="Actual Available Balance To Transfer"></asp:Label>
   
         </div>

         <div class="col-sm-9">
             <asp:TextBox ID="txtActualAvaitrasferAmt" ReadOnly="true" cssclass="form-control" runat="server"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
        runat="server" Enabled="True" TargetControlID="txtActualAvaitrasferAmt" 
                 ValidChars=".0123456789-">
    </asp:FilteredTextBoxExtender>
         </div>



   </div>
   
   </div>

</div>





</div>





</div>







<div class="row">
<div class="col-sm-8 col-sm-offset-2">
<div class="form-section">
<div class="form-section-head">

    <asp:Label ID="Label6" runat="server" Font-Size="medium" Font-Bold="true" Text="Amount Transfer To"></asp:Label>
</div>
<br />
<div class="row">
   <div class="col-sm-12">
         <div class="col-sm-4">
             <asp:Label ID="Label11" runat="server"  Font-Bold="true" Text="Mode"></asp:Label>
   
         </div>
       
         <div class="col-sm-8">
             <asp:DropDownList ID="ddlPaymentMode" cssclass="form-control"   runat="server" autopostback="true">
             <asp:ListItem Selected="True">PLUS</asp:ListItem>
             <asp:ListItem>MINUS</asp:ListItem>
             
       </asp:DropDownList>
         </div>



   </div>
   
   </div>

   <div class="row" runat="server" id="rw_Div_radiobutton">
   <div class="col-sm-12">
         <div class="col-sm-4">
             <asp:Label ID="Label12" runat="server"  Font-Bold="true" Text=""></asp:Label>
   
         </div>
       
         <div class="col-sm-8">
             <asp:RadioButton ID="rb_TransferToType" Text="" runat="server" 
                 AutoPostBack="true" GroupName="x" />&nbsp;&nbsp;
             <asp:RadioButton ID="rb_Customer" Text="Customer" runat="server"  
                 AutoPostBack="true" GroupName="x" />
         </div>



   </div>
   
   </div>
<br />

   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-4">
             <asp:Label ID="Label7" runat="server"  Font-Bold="true" Text="Transfer To" Visible="false"></asp:Label> <asp:Label ID="lblAgentType" runat="server" Font-Size="Small" Font-Bold="true" Text="Amount Transfer To" Visible="false" ></asp:Label>
    <asp:DropDownList ID="ddlTransferTo" cssclass="form-control" runat="server" 
                 Visible="false" AutoPostBack="True">
    <asp:ListItem>:::: Select Master Distributor ::::</asp:ListItem>
    <asp:ListItem>:::: Select Distributor ::::</asp:ListItem>
    <asp:ListItem>:::: Select Retailer ::::</asp:ListItem>
    <asp:ListItem>:::: Select Customer ::::</asp:ListItem>
       </asp:DropDownList>
         </div>
       
         <div class="col-sm-8">
         <asp:DropDownList ID="ddlTransferToAgent" cssclass="form-control" runat="server">
             </asp:DropDownList>
             <asp:ListSearchExtender ID="ddlTransferToAgent_ListSearchExtender" 
                 runat="server" Enabled="True" IsSorted="True" QueryPattern="Contains" 
                 TargetControlID="ddlTransferToAgent">
             </asp:ListSearchExtender>
<asp:TextBox ID="txtCustomerID" cssclass="form-control pull-left " AutoPostBack="true"  placeholder='Enter ID' MaxLength="15" Width="25%" Visible="false"  runat="server"></asp:TextBox>
            <asp:TextBox ID="txtCustomerName" cssclass="form-control"  ForeColor="Blue" Font-Bold="true"  placeholder='Name' Width="75%"    Visible="false"  runat="server"></asp:TextBox>
                         
         </div>



   </div>
   
   </div>
   <br />
   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-4">
             <asp:Label ID="Label8" runat="server"  Font-Bold="true" Text="Transfer Amount"></asp:Label>
   
         </div>

         <div class="col-sm-8">
             <asp:TextBox ID="txtTransferAmt" cssclass="form-control" MaxLength="7" runat="server"></asp:TextBox>
               <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTransferAmt">
    </asp:FilteredTextBoxExtender>
         </div>



   </div>
   
   </div>
   <br />
   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-4">
             <asp:Label ID="Label9" runat="server"  Font-Bold="true" Text="Remarks"></asp:Label>
   
         </div>

         <div class="col-sm-8">
             <asp:TextBox ID="txtRemark" TextMode="multiline" height="100px" cssclass="form-control" runat="server"></asp:TextBox>
         </div>



   </div>
   
   </div>
   <br />
   <div class="row">
   <div class="col-sm-12">
         <div class="col-sm-4">
             <asp:Label ID="Label10" runat="server"  Font-Bold="true" Text="Enter Transaction PIN"></asp:Label>
   
         </div>

         <div class="col-sm-8">
             <asp:TextBox ID="txtTransactionPin" cssclass="form-control" MaxLength="4" TextMode ="Password"  runat="server"></asp:TextBox>
         </div>



   </div>
   
   </div>
   <br />

 <div class='form-section'>
<div class='row'>
<div class='col-md-12'>
 <asp:Label ID='lblRID' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblAmt_Transfer_TransID' runat='server' Text='' Visible='false'></asp:Label> 
 <asp:Label ID='lblError' runat='server' Text=''></asp:Label> 
</div>
</div>
<div class='row'>
<div class='col-md-12'>
<center> <asp:Button ID='btnTransfer' runat='server' Text='Transfer Amt' 
 cssclass='btn btn-primary mar_top10' ValidationGroup='a' />&nbsp;

<asp:Button ID='btnReset' runat='server' Text='Reset' 
cssclass='btn btn-primary mar_top10' /> </center>
</div>
</div>
</div>

</div>
</div>
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



</asp:Content>
