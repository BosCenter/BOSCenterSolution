<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_WalletTransationReport.aspx.vb" Inherits="BOSCenter.BOS_WalletTransationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<div class="container">
<div class="row">

<div class="col-sm-8 col-sm-offset-2">
<div runat="server" id="Div_myWallet" visible="false">
<div class='log_form_head1'>
<asp:Label ID='formheading3' runat='server' Text='My Wallet'></asp:Label>
</div>
<div class="form-section">
<div class="form-section-head">

    <asp:Label ID="Label1" runat="server" Font-Size="medium" Font-Bold="true" Text="My Wallet Details"></asp:Label>
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
             <asp:Label ID="Label6" runat="server"  Font-Bold="true" Text="Hold Amount"></asp:Label>
   
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
        runat="server" Enabled="True" FilterType="Custom"  ValidChars="0123456789." TargetControlID="txtActualAvaitrasferAmt">
    </asp:FilteredTextBoxExtender>
         </div>



   </div>
   
   </div>

</div>
</div>

                    <div class=" table_head">
                   &nbsp;&nbsp;   Last 10 Transaction ::-
                        
                        
                        </div>
                    <div class="clearfix"></div>
                       <div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="ApprovalDiv">
                    <asp:GridView ID="GridView1" runat="server" cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True" AlternatingRowStyle-Wrap ="false">
                          
                          <Columns>
                  
                         
                            
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
