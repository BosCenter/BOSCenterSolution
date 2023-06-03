<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_Trans_Com_Report.aspx.vb" Inherits="BOSCenter.BOS_Trans_Com_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<script type="text/javascript">
    $(document).ready(function () {
        $(".inputtext").change(function (event) {
            var obj = $(this);
            //alert(obj.val());
        });
    });
</script>
<br />

 <div class="container" style="width:99%;">
	<div class="row">

    	<div class="col-sm-12">
        <div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                        <asp:Label ID="lblHeading" runat="server" Text="All Transaction Report"></asp:Label>	
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
                              Text="&nbsp;Apply Transaction Date" AutoPostBack="True" />
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                            <asp:Label ID="lblError0" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                         
                        </div>
                        
                        
                      </div>
                      </div>

                      <div class="clearfix" style="margin-bottom:5px;"></div>

                       <div class="row">
                      <div class="form-group">
                      <div class="col-sm-3">
                            
                          <asp:DropDownList ID="ddlAmountType" cssclass="form-control inputtext" runat="server" AutoPostBack="true">
                          <asp:ListItem>All Transactions</asp:ListItem>
                          <asp:ListItem>Balance Transfer</asp:ListItem>
                          <asp:ListItem>Commission</asp:ListItem>
                          <asp:ListItem>Money Transfer</asp:ListItem>
                          <asp:ListItem>PAN CARD</asp:ListItem>
                          <asp:ListItem>Recharge</asp:ListItem>
                          </asp:DropDownList>
                   
                      </div>
                      <label for="inputEmail3" class="col-sm-1 control-label">From</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtFrom" Enabled="false"  cssclass="form-control inputtext" runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                    <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                   
                        </div>
                        <label for="inputEmail3" class="col-sm-1 control-label">To</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtTO" Enabled="false"  cssclass="form-control inputtext" runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                   
                              <asp:CalendarExtender ID="txtTO_CalendarExtender" runat="server" Enabled="True" 
                                TargetControlID="txtTO" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                   
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
                      <asp:ImageButton ID="ImagebtnWOrd" runat="server" width="32px" Height="32px" Visible="false"
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
                    <div class="row">
                     <div class="col-lg-4"> 
                       &nbsp;&nbsp;   Search Details ::-
                     </div>
             
           
                    </div>
                    

                        </div>
                    <div class="clearfix"></div>
                       <div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="ApprovalDiv">
                    <asp:GridView ID="GridView1" runat="server" cssclass="grid-view-themeclass" ShowFooter="True" PageSize="10000"
                            BorderStyle="None" AllowPaging="True" AlternatingRowStyle-Wrap ="false" AutoGenerateColumns="false">
                          
                          <Columns>
                  
                         <asp:BoundField DataField="SrNo" HeaderText="SrNo" />
                         <asp:BoundField DataField="DATE" HeaderText="DATE" />
                         <asp:BoundField DataField="TIME" HeaderText="TIME" />
                         <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                         <asp:BoundField DataField="Type" HeaderText="Type" />
                         <asp:BoundField DataField="Account" HeaderText="Account" />
                         <asp:BoundField DataField="Cr" HeaderText="Cr" />
                         <asp:BoundField DataField="Dr" HeaderText="Dr" />   
                         <asp:BoundField DataField="Balance" HeaderText="Balance" />   
                          </Columns>
                              <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
                <FooterStyle Font-Size="Medium" Font-Bold="True" ForeColor="#CC0000" />
                      </asp:GridView>

                        </div> 
                      </div>
                    </div>
                      
          
    
</div>
 <asp:Button ID="btnReasonPopup" runat="server" Text="Button" style="display:none;"/>
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





</asp:Content>
