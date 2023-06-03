<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_PanCard.aspx.vb" Inherits="BOSCenter.BOS_PanCard" %>
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
<br /><br />
 <div class="container">
	<div class="row">
    	<div class="col-sm-8 col-sm-offset-2">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	PAN CARD COUPON
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	
                    <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Coupan Type<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlCoupanType" runat="server" 
                                cssclass="form-control inputtext" AutoPostBack="True">
                            <asp:ListItem Value="0">Select Type</asp:ListItem>
                            <asp:ListItem Value="72">E Card</asp:ListItem>
                            <asp:ListItem Value="107">P Card</asp:ListItem>
                            </asp:DropDownList> 
                   
                        </div>
<label for="inputEmail3" class="col-sm-3 control-label"> Amount<asp:Label ID="Label2" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtAmount" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static" ReadOnly="True" ></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtAmount">
    </asp:FilteredTextBoxExtender>
                        </div>

                      </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>

                                         <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Total Coupan<asp:Label ID="Label3" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtTotalCoupan" cssclass="form-control inputtext" 
                                runat="server"  ClientIDMode="Static" AutoPostBack="True" ></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTotalCoupan">
    </asp:FilteredTextBoxExtender>
                        </div>

 <label for="inputEmail3" class="col-sm-3 control-label">Total Amount<asp:Label ID="Label4" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtTotalAmt" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static" ReadOnly="True" ></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTotalAmt">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
 <div class="clearfix" style="margin-bottom:5px;"></div>

            <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Remarks<asp:Label ID="Label5" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtRemarks" cssclass="form-control inputtext" runat="server" TextMode="MultiLine"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                      </div>
                       
                       
                       
                        <div class="clearfix" style="margin-bottom:10px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Service Charge</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtServiceCharge" ReadOnly="true" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                        </div>
                         <div class="col-sm-3">
                          <div class="clearfix" style="margin-bottom:7px;"></div>
                             <asp:Label ID="lblService" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                     </div>
                      </div>
                      
                       <div class="clearfix" style="margin-bottom:10px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Net Amount</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtNetAmount" ReadOnly="true" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>&nbsp;
                        </div>
 <label for="inputEmail3" class="col-sm-3 control-label"> Enter Trans PIN<asp:Label ID="Label6" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                          <asp:TextBox ID="txtTransactionPin" cssclass="form-control" MaxLength="4" TextMode ="Password"  runat="server"></asp:TextBox>
                        </div>
                      </div>
                       
                                           <div class="clearfix" style="margin-bottom:5px;"></div>
            
                  <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                             <asp:Label ID="lblError" ClientIDMode="Static" runat="server"  ></asp:Label>
                     <asp:Label ID="lblWalletBal" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblTransId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblAgentID" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblAgentType" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>




                  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Proceed" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                        
                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                         


                        
                          </div> 
                        
                    </div>
                  </div>

        
                  </form>
                  <div class="clearfix"></div>
                
            </div>
            <div class="col-sm-12 table_head">
                        PAN CARD Details ::
                        </div>
            <div class="row">
                 <div class="col-sm-12">
                 <div class="table-responsive">
                    <div class="table_wid">
                    
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                        
                          <Columns>
                              <asp:BoundField HeaderText="SrNo" DataField="SrNo" />
                              <asp:BoundField HeaderText="TransID" DataField="API_TransId" />
                              <asp:BoundField HeaderText="Date" DataField="PanDate" />
                              <asp:BoundField HeaderText="Time" DataField="PanTime" />
                              <asp:BoundField HeaderText="CoupanType" DataField="CoupanType" />
                              <asp:BoundField HeaderText="Amount" DataField="Amount" />
                              <asp:BoundField HeaderText="TotalCoupan" DataField="TotalCoupan" />
                              <asp:BoundField HeaderText="TotalAmount" DataField="TotalAmount" />
                              <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                              <asp:BoundField HeaderText="Message" DataField="API_Message" />
                              <%--<asp:BoundField HeaderText="Status" DataField="API_Status" />--%>
                              
                            
                            <%-- <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btnGrdRowDelete_Click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                           <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="GridView1_SelectedIndexChanged"
                                          CommandName="Select" Text=""><i class="fa fa-pencil"></i></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>--%>
                              

                            
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
</div>


    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="350px" style="display:none;"  >
      <asp:UpdateProgress ID="UpdWaitImage" runat="server"  DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
                 <ProgressTemplate>
                      <div class="PopupWaiting">
                                 <asp:Image ID="imgProgress" ImageUrl="../images/ajax-loader.gif" runat="server" />
                                          </div>    
                                </ProgressTemplate>
                                </asp:UpdateProgress>   
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
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"  />
             
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" /> </ContentTemplate>
        </asp:UpdatePanel>
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>




</asp:Content>
