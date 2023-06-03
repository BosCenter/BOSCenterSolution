<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_CommissionSettingMaster_Latest.aspx.vb" Inherits="BOSCenter.BOS_CommissionSettingMaster_Latest" %>
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
    	<div class="col-sm-10 col-sm-offset-1">
          <div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	<asp:Label ID="Label3" runat="server" Text="Recharge API Commission"></asp:Label>
                    </div>
                </div>
            </div>
            	<div class="row">
            	<div class="col-sm-12">
               
               <div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="ApprovalDiv">
                        <asp:Panel ID="Panel1" runat="server" style="overflow:scroll;" Height="400px">
                        <asp:GridView ID="GridView1" runat="server" cssclass="grid-view-themeclass" AutoGenerateColumns="false"  PageSize="100000" style="height:400px;overflow:scroll;"
                            BorderStyle="None" AllowPaging="True" AlternatingRowStyle-Wrap ="false">
                         
                          <Columns>
                       <asp:BoundField  HeaderText="SrNo" DataField="SrNo" />
                       <asp:BoundField  HeaderText="APIName" DataField="APIName" />
                       <asp:BoundField  HeaderText="Category" DataField="Category" />
                       <asp:BoundField  HeaderText="Code" DataField="Code" />
                       <asp:BoundField  HeaderText="OperatorName" DataField="OperatorName" />
                       <asp:BoundField  HeaderText="CommissionType" DataField="Dis_CommissionType" />
                       <asp:BoundField  HeaderText="Commission" DataField="Dis_Commission" />
                             <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                  <div style="margin-top:3px;"></div>
                                      <asp:TextBox ID="txtAllowCommission" MaxLength="5" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
        runat="server" Enabled="True" 
        TargetControlID="txtAllowCommission" FilterType="Custom" ValidChars="1234567890.">
    </asp:FilteredTextBoxExtender>
                                      <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>
                                      <asp:Label ID="lblCanChange" runat="server" Text='<%# Eval("CanChange") %>' Visible="false" ></asp:Label>
                                      <asp:Label ID="lblAPIName" runat="server" Text='<%# Eval("APIName") %>' Visible="false" ></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle Width="100px"></ItemStyle>
                              </asp:TemplateField>
                               <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                   <div style="margin-top:3px;"></div>
                                     <asp:LinkButton ID="lnkUpdateCommission" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="grdupdateRow_click"
                                          CommandName="Select" Text="">Update</asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>

                            
                          </Columns>
                              <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
               
                      </asp:GridView>
                        </asp:Panel>

                    
                        </div> 
                      </div>
                    </div>
            
               </div> 
                </div>

<div runat="server" id="Div_SlabwiseCommission" >
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	<asp:Label ID="Label4" runat="server" Text="Slab Wise Commission"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
            	<div class="col-sm-12">
                              <div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="Div1">
                        <asp:Panel ID="Panel2" runat="server" style="overflow:scroll;" Height="400px">
                        <asp:GridView ID="grd_SlabwiseCommission" runat="server" cssclass="grid-view-themeclass" AutoGenerateColumns="false"  PageSize="100000" style="height:400px;overflow:scroll;"
                            BorderStyle="None" AllowPaging="True" AlternatingRowStyle-Wrap ="false">
                         
                          <Columns>
                       <asp:BoundField  HeaderText="SrNo" DataField="SrNo" />
                       <asp:BoundField  HeaderText="APIName" DataField="APIName" />
                       <asp:BoundField  HeaderText="Slab" DataField="Slab" />
                      <asp:BoundField  HeaderText="CommissionType" DataField="CommissionType" />
                       <asp:BoundField  HeaderText="Commission" DataField="Commission" />                           
                          </Columns>
                              <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
               
                      </asp:GridView>
                        </asp:Panel>

                    
                        </div> 
                      </div>
                    </div>
            
               </div> 
                </div>

                </div>








            
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
                         <label for="inputEmail3" class="col-sm-3 control-label">
                             <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Getting Commission"></asp:Label></label>
                              <label for="inputEmail3" class="col-sm-3 control-label">
                             <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="Service Charges"></asp:Label></label>
                          <label for="inputEmail3" class="col-sm-3 control-label">
                              <asp:Label ID="lblAllowComm" ForeColor="Blue" runat="server" Text="Allow Commission"></asp:Label></label>
                      </div>
                   
           <asp:ListView ID="ListView1" runat="server">

       <ItemTemplate>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
            
             <div class="form-group">
                         <label for="inputEmail3" class="col-sm-3 control-label">   <asp:Label ID="lblAPIName" runat="server" ForeColor="Blue" Text=""></asp:Label> </label>
                           <div class="col-sm-3">
                            <asp:TextBox ID="txtRechargeAPI" cssclass="form-control inputtext" ReadOnly="true" runat="server"  ClientIDMode="Static" ></asp:TextBox> <%--<asp:Label ID="lblgetting" runat="server" ForeColor="Blue" Text=""></asp:Label></label>--%>
                   <asp:Label ID="lblGettingCommType" runat="server"  Visible="false" Text=""></asp:Label>
                   <asp:Label ID="lblListAPIName" runat="server"  Visible="false" Text=""></asp:Label>
                        </div>
                         <div class="col-sm-2">
                            <asp:TextBox ID="txtServiceCharge" MaxLength="5" cssclass="form-control inputtext" runat="server" Enabled=false   ClientIDMode="Static" ></asp:TextBox><asp:Label ID="Label6" runat="server" ForeColor="Blue" Text=""></asp:Label></label>
                        </div>
                           <div class="col-sm-2">
                            <asp:TextBox ID="txtRechargeAPIAllow" MaxLength="5" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox><asp:Label ID="lblAllow" runat="server" ForeColor="Blue" Text=""></asp:Label></label>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
        runat="server" Enabled="True" 
        TargetControlID="txtRechargeAPIAllow" FilterType="Custom" ValidChars="1234567890.">
    </asp:FilteredTextBoxExtender>
                        </div>
                           <div class="col-sm-2">
                            <asp:LinkButton ID="lnkUpdateCommission_List" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="grdupdate_List_click"
                                           Text="">Update</asp:LinkButton>
                   
                        </div>
                      </div>
                      </ItemTemplate> 
                   </asp:ListView> 
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
