<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_irctc_api.aspx.vb" Inherits="BOSCenter.BOS_irctc_api" %>
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
                    	IRCTC API Test 
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	
                    <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">&nbsp;Type<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlCoupanType" runat="server" 
                                cssclass="form-control inputtext">
                                 <asp:ListItem Value="Order Booking">Login Auth</asp:ListItem>
                            <asp:ListItem Value="Login Auth">Order Booking</asp:ListItem>
                           
                            </asp:DropDownList> 
                   
                        </div>

                      </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>

                                         <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Partner No<asp:Label ID="Label3" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtPartnerNo" cssclass="form-control inputtext" 
                                runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                 
                        </div>
                      </div>
 <div class="clearfix" style="margin-bottom:5px;"></div>

            <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Password<asp:Label ID="Label5" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtPassword" cssclass="form-control inputtext" runat="server" TextMode="SingleLine"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                      </div>
                       
                <div class="clearfix" style="margin-bottom:5px;"></div>

            <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Request Decrypt<asp:Label ID="Label2" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtRequestDecrypt" cssclass="form-control inputtext" runat="server" TextMode="MultiLine" Rows="2"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                      </div>       
                                  <div class="clearfix" style="margin-bottom:5px;"></div>

            <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Response Decrypt<asp:Label ID="Label4" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtResponseDecrypt" cssclass="form-control inputtext" runat="server" TextMode="MultiLine" Rows="2"  ClientIDMode="Static" ></asp:TextBox>
                   
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
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="false" >
                                                                 

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
