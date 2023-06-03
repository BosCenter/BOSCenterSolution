<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_CommissionSettingMaster.aspx.vb" Inherits="BOSCenter.BOS_CommissionSettingMaster" %>
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
 <div class="container">
	<div class="row">
    	<div class="col-sm-8 col-sm-offset-2">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	<asp:Label ID="lblCommssionHeading" runat="server" Text="Set Commission For Distributor"></asp:Label>
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	
                    <form class="form-horizontal">
                      <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label"><asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Services"></asp:Label></label>
                         <label for="inputEmail3" class="col-sm-4 control-label">
                             <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Getting Commission %"></asp:Label></label>
                          <label for="inputEmail3" class="col-sm-4 control-label">
                              <asp:Label ID="lblAllowComm" ForeColor="Blue" runat="server" Text="Allow Commission %"></asp:Label></label>
                      </div>
                   
          
                      <div class="clearfix" style="margin-bottom:5px;"></div>
            
             <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">Recharge </label>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtRechargeAPI" cssclass="form-control inputtext" ReadOnly="true" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtRechargeAPIAllow" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
        runat="server" Enabled="True" 
        TargetControlID="txtRechargeAPIAllow" FilterType="Custom" ValidChars="1234567890.-">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
                   
           <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">Flight Booking </label>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtFlight_APIPer" cssclass="form-control inputtext" runat="server" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtFlight_APIPerAllow" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
        runat="server" Enabled="True" 
        TargetControlID="txtFlight_APIPerAllow" FilterType="Custom" ValidChars="1234567890.-">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
                    <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">Bus Booking </label>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtBusBooking_API" cssclass="form-control inputtext" runat="server"  ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtBusBooking_APIAllow" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
        runat="server" Enabled="True" 
        TargetControlID="txtBusBooking_APIAllow" FilterType="Custom" ValidChars="1234567890.-">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
                            <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">Train Booking</label>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtRail_API" cssclass="form-control inputtext" runat="server" ReadOnly="true"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtRail_APIAllow" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
        runat="server" Enabled="True" 
        TargetControlID="txtRail_APIAllow" FilterType="Custom" ValidChars="1234567890.-">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
                         <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">Pan Card</label>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtPAN_API" cssclass="form-control inputtext" runat="server" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtPAN_APIAllow" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
        runat="server" Enabled="True" 
        TargetControlID="txtPAN_APIAllow" FilterType="Custom" ValidChars="1234567890.-">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
                   
                      <div class="clearfix" style="margin-bottom:5px;"></div>


                        <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">Money Transfer </label>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtMoneyTransfer_API" cssclass="form-control inputtext" runat="server" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtMoneyTransfer_APIAllow" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
        runat="server" Enabled="True" 
        TargetControlID="txtMoneyTransfer_APIAllow" FilterType="Custom" ValidChars="1234567890.-">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
          
                      <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">GST Services</label>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtGST_API" cssclass="form-control inputtext" runat="server" ReadOnly="true" ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                           <div class="col-sm-4">
                            <asp:TextBox ID="txtGST_APIAllow" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
        runat="server" Enabled="True" 
        TargetControlID="txtGST_APIAllow" FilterType="Custom" ValidChars="1234567890.-">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
                   
          
                 

                      <div class="clearfix" style="margin-bottom:5px;"></div>



                  <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                             <asp:Label ID="lblError" ClientIDMode="Static" runat="server"  ></asp:Label>
                   
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>




                  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Update" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                         


                        
                          </div> 
                        
                    </div>
                  </div>

        
                  </form>
                  <div class="clearfix"></div>
                
            </div>
          
        </div>
    </div>
</div>




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
