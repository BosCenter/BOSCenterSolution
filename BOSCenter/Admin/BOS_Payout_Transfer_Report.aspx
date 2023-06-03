<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_Payout_Transfer_Report.aspx.vb" Inherits="BOSCenter.BOS_Payout_Transfer_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container" style="width:99%;">
	<div class="row">

    	<div class="col-sm-12">
        <div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                        <asp:Label ID="lblHeading" runat="server" Text="Payout Transation Details"></asp:Label>	
                    </div>
                </div>  
            </div>
        

        	
        	<div class="log_form1">
            	
                

<div class="row">
        <div class="col-sm-10 col-lg-offset-1">
        <form class="form-horizontal">

                    <div class="row">
                      <div class="form-group">
                       
                      <div class="col-sm-4">
                          <asp:CheckBox ID="chkduration" runat="server" 
                              Text="&nbsp;Apply Transfer Date" AutoPostBack="True" />
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                            <asp:Label ID="lblError0" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                            <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblMemberID" runat="server" Visible="false"></asp:Label>
                        </div>
                        
                        
                      </div>
                      </div>

                      <div class="clearfix" style="margin-bottom:5px;"></div>

                       <div class="row">
                      <div class="form-group">
                    
                      <label for="inputEmail3" class="col-sm-1 control-label">From</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtFrom" Enabled="false"  cssclass="form-control inputtext" runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                   <%-- <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>--%>
                            <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                   
                        </div>
                        <label for="inputEmail3" class="col-sm-1 control-label">To</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtTO" Enabled="false"  cssclass="form-control inputtext" runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                   
                             <%-- <asp:CalendarExtender ID="txtTO_CalendarExtender" runat="server" Enabled="True" 
                                TargetControlID="txtTO" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>--%>

                            <asp:CalendarExtender ID="txtTO_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtTO" Format="dd/MM/yyyy" >
                            </asp:CalendarExtender>
                   
                        </div>
                      </div>
                   </div>

                   <div class="clearfix" style="margin-bottom:5px;"></div>

                   

                   <div class="row">

                 
                      <div class="form-group">
                     
                       
                      <label for="inputEmail3" class="col-sm-1 control-label" style="padding-right: 14px;"> Criteria</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlSelectCriteria" cssclass="form-control inputtext"  
                                runat="server" AutoPostBack="True" >
                                <asp:ListItem>All Records</asp:ListItem>
                                <asp:ListItem >TranscationId</asp:ListItem>
                                <asp:ListItem >MobileNo</asp:ListItem>
                                
                            </asp:DropDownList>
                           
                        </div>
                        <label for="inputEmail3" class="col-sm-1 control-label"> Value</label>
                        <div class="col-sm-4">
                           <asp:TextBox ID="txtSearchString" runat="server" 
                                cssclass="form-control inputtext" placeholder="Enter Value"></asp:TextBox>
                   
                        </div>
                      </div>
                   </div>

                   <div class="clearfix" style="margin-bottom:5px;"></div>

        
                  </form>
        </div>

        
        </div>



        <div class="row">
        <div class="form-group">
                        <div class="col-sm-5">
                        
                        
                        </div>
                        <div class="col-sm-7">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                          
                          <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
                            <asp:Label ID="lblError1" runat="server" ></asp:Label>
                            <asp:Label ID="lblExportQry" runat="server"  Visible="false"></asp:Label>
                        </div>
                      </div>
        
        </div>

        <div class="row">
        <div class="form-group">

                  <div class="col-sm-12">
                    <div class="col-sm-5">
                    </div> 
                          <div class="col-sm-7">
                           <asp:Button ID="btnSearch" runat="server" Text="Search" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                            	
                        
                             <asp:Button ID="btnReset" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" CausesValidation="False" 
                                  UseSubmitBehavior="False" />  
                         


                        
                          </div> 
                        
                    </div>
                  
                  </div>

                  <div class="clearfix" style="margin-bottom:5px;"></div>

                  <div class="row">
                 <div class="col-md-4"></div>

              <div class="col-md-2"></div>

                  </div>

</div>

<div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                 
                  <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
       
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/excel_32X32.png"/>
                      <asp:ImageButton ID="ImagebtnWOrd" runat="server" width="32px" Height="32px"  Visible="false"
                          ImageUrl="~/images/word_32X32.png"/>
                      <asp:ImageButton ID="Imagepdf" runat="server" width="32px" Height="32px" Visible="false"
                          ImageUrl="~/images/pdf_32X32.png"/>
                    </ContentTemplate> 
                 <Triggers>
                 <asp:PostBackTrigger ControlID="ImagebtnExcel" />
                 <asp:PostBackTrigger ControlID="ImagebtnWOrd" />
                 <asp:PostBackTrigger ControlID="Imagepdf" />
                 </Triggers>

                 </asp:UpdatePanel> 
                 
                 </div>

                   
              <div class="col-md-6">
              <label class="col-sm-2"></label>
              <div class="col-sm-4 pull-right">
              <asp:DropDownList ID="ddlNoOfRecords" runat="server" 
                      cssclass="form-control inputtext" AutoPostBack="True" >
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
 </div>
       </div>

        
    </div>
    
            
              
                    <div class=" table_head">
                   &nbsp;&nbsp;   Search Details ::-
                        
                        
                        </div>
                    <div class="clearfix"></div>
                       <div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="ApprovalDiv">
                    <asp:GridView ID="GridView1" runat="server" cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True" AlternatingRowStyle-Wrap ="false">
                      
                          <Columns>
           <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>   
                                     <%-- OnClick="btnGrdPrint_Click--%>
                                    <%-- <asp:LinkButton ID="LinkButton3" runat="server"  CausesValidation="False" CssClass="btn btn-primary" 
                                          CommandName="Select" Text=""><i class="fa fa-print"></i>&nbsp;Print</asp:LinkButton>--%>
                                     <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("Sr No") %>' Visible="false" ></asp:Label>
                                      
                                  </ItemTemplate>
                              </asp:TemplateField>
                            
                   
                          </Columns>
                              <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
               
                      </asp:GridView>

                        </div> 
                      </div>
                    </div>
                      
          
    
</div>
 <asp:Button ID="btnReasonPopup" runat="server" Text="Button" style="display:none;"/>
  <%--  <asp:ModalPopupExtender ID="ModalPopup_Reason" runat="server" TargetControlID="btnReasonPopup" PopupControlID="pnlReasonPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnReasonCancel" >
    </asp:ModalPopupExtender>--%>
    <asp:ModalPopupExtender ID="ModalPopup_Reason" runat="server" TargetControlID="btnReasonPopup" PopupControlID="pnlReasonPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnReasonCancel" >
    </asp:ModalPopupExtender>

    <asp:Panel ID="pnlReasonPopup" runat="server" Width="350px" style="display:none;" >

<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>Reason Dialog</strong>
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


            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter&nbsp; Reason:<label style="color:Red;" >*</label>
            <asp:RequiredFieldValidator ID="Required_ReasonValidator" runat="server" 
                ControlToValidate="txtReason" ErrorMessage="Please Enter Reason." 
                Font-Bold="True" ForeColor="Red" ValidationGroup="x"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"  
                TextMode="MultiLine" ValidationGroup="x" Width="250px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnReasonSubmit" runat="server" Text="Submit" Width="80px" 
                CssClass="btn btn-primary" ValidationGroup="x"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReasonCancel" runat="server" Text="Cancel" Width="80px" 
                CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>



    </asp:Panel>



 
    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
<%--    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>--%>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>

    <asp:Panel ID="DeletePopup" runat="server" Width="350px" style="display:none;"  >

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
             <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"  />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>


        <asp:Button ID="Button1" runat="server" Text="Button" style="display:none;"/>
<%--<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelInfo" >
</asp:ModalPopupExtender>--%>
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelInfo">
    </asp:ModalPopupExtender>

<asp:Panel ID="InformationPopup" runat="server" Width="350px" style="display:none;"    >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong><asp:Label ID="lblDialogMsgInfo" runat="server" Text="" ></asp:Label></strong>
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
            <asp:Label ID="lblMobileNo1" runat="server" CssClass="mar_lft30" Text="MobileNo : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblpopMobileNo" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
             <asp:Label ID="l" runat="server" CssClass="mar_lft30" Text="Bank Name : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblpopBankName" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label19" runat="server" CssClass="mar_lft30" Text="TransferAmt : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopTransferAmt" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            
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
          <asp:Button ID="btnPrintReceipt" runat="server" Text="Print Receipt" Width="120px" CssClass="btn btn-danger" /> &nbsp; <asp:Button ID="btnCancelInfo" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary"/> 
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>

</asp:Content>
