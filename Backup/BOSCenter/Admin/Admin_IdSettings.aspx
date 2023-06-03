<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master"  CodeBehind="Admin_IdSettings.aspx.vb" Inherits="BOSCenter.Admin_IdSettings" %>
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
                    	Admin ID Settings</div>
                </div>
            </div>

      
<asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender9" runat="Server"
    TargetControlID="MonthlyPanel2"
    CollapsedSize="0"
    ExpandedSize="0"
    Collapsed="True"
    ExpandControlID="Image10"
    CollapseControlID="Image10"
    AutoCollapse="False"
    AutoExpand="False"
    ScrollContents="false"
    TextLabelID="Label1"
    CollapsedText="Show Details..."
    ExpandedText="Hide Details" 
    ImageControlID="Image10"
        ExpandedImage="~/images/UP.png"
    CollapsedImage="~/images/Down.png"
    ExpandDirection="Vertical" />
<div class="row">
<div class="col-sm-12">

<div class="form-section">
<asp:Panel ID="Panel8" runat="server">
<div class="form-section-head">
% Getting Commission of Settings<asp:Image ID="Image10" runat="server"  height="20" width="20" class="pull-right"/>
</div>
</asp:Panel> 
<asp:Panel ID="MonthlyPanel2" runat="server" style="width:97%;">
<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">


<%--<div class="row">
<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">Recharge API</label>
<asp:LinkButton ID="LinkButton67" runat="server" CommandArgument="Recharge_API" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>

<asp:TextBox ID="txtRechargeAPI" runat="server" class="form-control"  
        placeholder="Recharge API" MaxLength="5"></asp:TextBox>
     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
        TargetControlID="txtRechargeAPI">
    </asp:FilteredTextBoxExtender>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for="txtCompanyCode" class="control-label">Flight API</label>
<asp:LinkButton ID="LinkButton68" runat="server" CommandArgument="Flight_API" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>

<asp:TextBox ID="txtFlight_APIPer" runat="server" class="form-control" MaxLength="5" 
        placeholder="Flight API"></asp:TextBox>
        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
        TargetControlID="txtFlight_APIPer">
    </asp:FilteredTextBoxExtender>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for="txtCompanyCode" class="control-label">BusBooking API</label>
<asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="BusBooking_API" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>

<asp:TextBox ID="txtBusBooking_API" runat="server" class="form-control" MaxLength="5" 
        placeholder="BusBooking API"></asp:TextBox>
        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
        TargetControlID="txtBusBooking_API">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for="txtCompanyCode" class="control-label">Rail API</label>
<asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="Rail_API" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>

<asp:TextBox ID="txtRail_API" runat="server" class="form-control" MaxLength="5" 
        placeholder="Rail API"></asp:TextBox>
        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
        TargetControlID="txtRail_API">
    </asp:FilteredTextBoxExtender>
</div>
</div>
</div>--%>


 <div class="row">

 <%--<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">PAN API</label>
<asp:LinkButton ID="LinkButton86" runat="server" CommandArgument="PAN_API" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:TextBox ID="txtPAN_API" runat="server" class="form-control"  
        placeholder="PAN API" MaxLength="5"></asp:TextBox>
     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
        runat="server" Enabled="True" 
        TargetControlID="txtPAN_API" FilterType="Custom" ValidChars="1234567890.">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">MoneyTransfer API</label>
<asp:LinkButton ID="LinkButton87" runat="server" CommandArgument="MoneyTransfer_API" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:TextBox ID="txtMoneyTransfer_API" runat="server" class="form-control"  
        placeholder="MoneyTransfer API" MaxLength="5"></asp:TextBox>
     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" 
        runat="server" Enabled="True" 
        TargetControlID="txtMoneyTransfer_API" FilterType="Custom"  ValidChars="1234567890.">
    </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">GST API</label>
<asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="GST_API" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:TextBox ID="txtGST_API" runat="server" class="form-control"  
        placeholder="GST API" MaxLength="5"></asp:TextBox>
     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
        runat="server" Enabled="True" 
        TargetControlID="txtGST_API" FilterType="Custom"  ValidChars="1234567890.">
    </asp:FilteredTextBoxExtender>
</div>
</div>--%>



<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">SERVICE CHARGE</label>
<asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="SERVICECHARGE" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:TextBox ID="txtServiceCharge" runat="server" class="form-control"  
        placeholder="SERVICE CHARGE" MaxLength="7"></asp:TextBox>
     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
        runat="server" Enabled="True" 
        TargetControlID="txtServiceCharge" FilterType="Custom"  ValidChars="1234567890.">
    </asp:FilteredTextBoxExtender>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">Distributor ID</label>
<asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="SubDistributorID" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:TextBox ID="txtSubDistributorID" runat="server" class="form-control"  
        placeholder="Distributor ID" MaxLength="7"></asp:TextBox>
    
</div>
</div>


 </div>




</div>
</div>
</div>
</asp:Panel> 
</div>
</div>
</div>

<asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
    TargetControlID="pnlAPIWiseStatus"
    CollapsedSize="0"
    ExpandedSize="0"
    Collapsed="True"
    ExpandControlID="Image1"
    CollapseControlID="Image1"
    AutoCollapse="False"
    AutoExpand="False"
    ScrollContents="false"
    TextLabelID="Label1"
    CollapsedText="Show Details..."
    ExpandedText="Hide Details" 
    ImageControlID="Image1"
        ExpandedImage="~/images/UP.png"
    CollapsedImage="~/images/Down.png"
    ExpandDirection="Vertical" />
<div class="row">
<div class="col-sm-12">

<div class="form-section">
<asp:Panel ID="Panel1" runat="server">
<div class="form-section-head">
Set API Wise Active / Inactive  <asp:Image ID="Image1" runat="server"  height="20" width="20" class="pull-right"/>
</div>
</asp:Panel> 
<asp:Panel ID="pnlAPIWiseStatus" runat="server" style="width:97%;">
<div style="margin-bottom:5px;"></div>

<div class="row">
<div class="col-md-12">
<div class="bder top-mrg20">

 <%-- Start Activate / Deactivate API--%>

  <div class="row">
 <div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">PAN API</label>
<asp:LinkButton ID="lnkbtn_PANCardAPI_Status" runat="server" CommandArgument="PANCardAPI_Status" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
 <asp:DropDownList ID="ddlPANCardAPI" cssclass="form-control inputtext"  
                                runat="server"  >
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>


</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">MoneyTrans API</label>
<asp:LinkButton ID="lnkbtn_MoneyTransferAPI_Status" runat="server" CommandArgument="MoneyTransferAPI_Status" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:DropDownList ID="ddlMoneyTransferAPI" cssclass="form-control inputtext"  
                                runat="server"  >
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
</div>
</div>
<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">Recharge API</label>
<asp:LinkButton ID="lnkbtn_RechargeAPI_Status" runat="server" CommandArgument="RechargeAPI_Status" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:DropDownList ID="ddlRechargeAPI" cssclass="form-control inputtext"  
                                runat="server"  >
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">AEPS API</label>
<asp:LinkButton ID="lnkbtn_AEPSAPI_Status" runat="server" CommandArgument="AEPSAPI_Status" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:DropDownList ID="ddlAEPSAPI" cssclass="form-control inputtext"  
                                runat="server"  >
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
</div>
</div>
 </div>
 <div class="row">
 <div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">Recharge API-2</label>
<asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="RechargeAPI_2_Status" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:DropDownList ID="ddlRechargeAPI_2" cssclass="form-control inputtext"  
                                runat="server"  >
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for="txtSharePrice" class="control-label">MoneyTrans API-2</label>
<asp:LinkButton ID="lnkbtn_MoneyTransferAPI_2_Status" runat="server" CommandArgument="MoneyTrans_2_Status" onclick ="btnUpdate_click" CssClass="btn btn-primary btn-sm pull-right" >
    <i class="icon-save icon-3"></i>
    </asp:LinkButton>
<asp:DropDownList ID="ddlMoneyTransferAPI_2_Status" cssclass="form-control inputtext"  
                                runat="server"  >
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
</div>
</div>

 </div>

 <%-- End Activate / Deactivate API--%>


</div>
</div>
</div>
</asp:Panel> 
</div>
</div>
</div>



<div class="clearfix" style="margin-bottom:5px;"></div>  
<div id="Div1" runat="server" visible="false">
        	<div class="row">
             <div class="form-group">
                  <div class="row">
                  <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                             <asp:Label ID="Label8" ClientIDMode="Static" runat="server"  ></asp:Label>
                   
                            <asp:Label ID="lbllnkbtn" runat="server" Visible="False"></asp:Label>
                        </div>
                  </div>
                  <div class="row">
                  <div class="col-sm-12">
                    
                      <div class="col-sm-6 col-sm-offset-5">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"  Visible="false"
                             cssclass="btn btn-primary" ValidationGroup="a" />
                    </div>
                  </div>
                
                  </div>
                        
                      </div>

                  

        
                  
                  <div class="clearfix"></div>
                
                


            
            </div>

            </div>

</div>


           
        </div>
    </div>
    



    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="UpdatePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="UpdatePopup" runat="server" Width="450px"  style="display:none;" >

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
        <td align="left">
            <asp:Label ID="lblSettingInfoName" runat="server" Text="" style="margin-left:70px;"></asp:Label><br />
            <asp:Label ID="lblSettingInfovalue" runat="server" Text="" style="margin-left:70px;"></asp:Label><br />
            <asp:Label ID="lblSettingNextDate" runat="server" Text="" style="margin-left:70px;" Font-Bold="true"></asp:Label>
            
            </td>

    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" />
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
