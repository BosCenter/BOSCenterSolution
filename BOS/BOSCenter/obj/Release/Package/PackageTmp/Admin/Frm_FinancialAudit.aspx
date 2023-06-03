<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Frm_FinancialAudit.aspx.vb" Inherits="BOSCenter.Frm_FinancialAudit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container">
     <div class="col-md-10 col-md-offset-1">
        <div class="form-section-head">
            Fiancial Audit
        </div>
         <div class="form-section">
             <div class="row">
               
                   <div class="col-md-4" cssclass="form-group">
                       <div class="form-group">
                       <lable>Name</lable>
                       <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                 </div>
                 <div class="col-md-4" cssclass="form-group">
                     <div class="form-group">
                     <lable>Mobile No.</lable>
                     <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                         </div>
                 </div>
                  <div class="col-md-4">
                      <div class="from-group">
                     <lable>Annual Turnover</lable>
                       <asp:TextBox ID="txtAnnual_Turnover" runat="server" CssClass="form-control" ></asp:TextBox>
                          </div>
                 </div>
                
             </div>
             <div class="row">
                 <div class="col-md-4">
                     <div class="form-group">
                      <lable>Address</lable>
                       <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" ></asp:TextBox>
                     </div>
                 </div>
                 <div class="col-md-4">
                     <div class="form-group">
                        <lable>District</lable>
                         <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>                       
                     </div>
                     </div>
                 <div class="col-md-4">
                     <div class="form-group">
                           <lable>State</lable>
                         <asp:DropDownList ID="ddlState_UnionTerritory" runat="server" CssClass="form-control"></asp:DropDownList>
                     </div>
                 </div>
                 </div>
             <div class="row">
                 <div class="col-md-4">
                     <div class="form-group">
                      <lable>Pin</lable>
                       <asp:TextBox ID="txtPin" runat="server" CssClass="form-control" ></asp:TextBox>
                     </div>
                 </div>
                 <div class="col-md-4">
                     <div class="form-group">
                                      
                     </div>
                     </div>
                 <div class="col-md-4">
                     <div class="form-group">
                           
                     </div>
                 </div>
                 </div>  
             </div>
         <div class="form-section">
             <div class="row">
                 <div class="col-md-12">
                     <center>
                     <asp:Label id="lblError" runat="server" Text=""></asp:Label>
                     <asp:Label id="lblRID" runat="server" Text="" Visible="false" ></asp:Label>
                         </center>
                 </div>
             </div>
             <div class="row">
                 <center>
                     <asp:Button id="btnSave" runat="server" Text="Save" cssclass="btn btn-primary"/>
                     <asp:Button id="btnDelete" runat="server" Text="Delete" cssclass="btn btn-danger" />
                     <asp:Button id="btnReset" runat="server" Text="Reset" cssclass="btn btn-primary"/>
                     
                 </center>
             </div>
         </div>
         </div>
     </div>
    <asp:Button ID='modalPopupButton' runat='server' Text='Button' style='display:none;'/>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID='modalPopupButton' PopupControlID='DeletePopup'  BackgroundCssClass='modalBackground'  CancelControlID='btnCancel' ></ajaxToolkit:ModalPopupExtender>
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
