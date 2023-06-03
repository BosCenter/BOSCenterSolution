<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_Ref_Code_Master.aspx.vb" Inherits="BOSCenter.BOS_Ref_Code_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
/*.modalBackground 
{
    height:100%;
    background-color:#504F4F;
    filter:alpha(opacity=70);
    opacity:0.7;
}*/

.PopupWaiting{
position: absolute;
background-color: #FAFAFA;
z-index: 2900 !important;
opacity: 0.9;
overflow: hidden;
text-align: center; top: 0; left: 0;
height: 220px;
width: 100%;
padding-top:10%;
} 

  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<asp:UpdatePanel runat='server' ID='updatepanel2'>
<ContentTemplate>
    <div class="container">
	<div class="row">
    	<div class ="col-sm-10 col-sm-offset-1">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Reference Code Master
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	 <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Reference Code<asp:Label ID="lblStarTitle" runat="server"  ForeColor="Red" Text=" *"></asp:Label></label>
                        <div class="col-sm-9">
                           <asp:TextBox ID="txtReferenceCode" cssclass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Amount To Debit <asp:Label ID="lblStarType" runat="server"  ForeColor="Red"  Text=" *"></asp:Label></label>
                        <div class="col-sm-9">
                        <asp:TextBox ID="txtAmountToDebit" cssclass="form-control" runat="server" 
                                MaxLength="7"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtAmountToDebit_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtAmountToDebit">
                            </asp:FilteredTextBoxExtender>
                        </div>
                    </div>
                        <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Instamojo Pay Link </label>
                        <div class="col-sm-9">
                        <asp:TextBox ID="txtinstamojo_Pay_Link" cssclass="form-control" runat="server" ></asp:TextBox>
                            
                        </div>
                    </div>                 
                
                     <div class="clearfix" style="margin-bottom:5px;"></div>
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Master Distributor Cashback <asp:Label ID="Label1"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddl_Dis_CommissionType" cssclass="form-control"  runat="server" 
                                AutoPostBack="True">
                            <asp:ListItem>Not Applicable</asp:ListItem>
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Amount</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-sm-5">
                            <asp:TextBox ID="txt_Dis_Commission" cssclass="form-control" runat="server" 
                                 MaxLength="7"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
                                 TargetControlID="txt_Dis_Commission">
    </asp:FilteredTextBoxExtender>
                        </div>
                    </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Distributor Cashback <asp:Label ID="Label2"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddl_Sub_Dis_CommissionType" cssclass="form-control"  runat="server" 
                                AutoPostBack="True">
                            <asp:ListItem>Not Applicable</asp:ListItem>
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Amount</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-sm-5">
                            <asp:TextBox ID="txt_Sub_Dis_Commission" cssclass="form-control" runat="server" 
                                 MaxLength="7"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
                                 TargetControlID="txt_Dis_Commission">
    </asp:FilteredTextBoxExtender>
                        </div>
                    </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Retailer Cashback <asp:Label ID="Label3"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddl_Retailer_CommsissionType" cssclass="form-control"  runat="server" 
                                AutoPostBack="True">
                            <asp:ListItem>Not Applicable</asp:ListItem>
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Amount</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-sm-5">
                            <asp:TextBox ID="txt_Retailer_Commission" cssclass="form-control" runat="server" 
                                 MaxLength="7"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
                                 TargetControlID="txt_Dis_Commission">
    </asp:FilteredTextBoxExtender>
                        </div>
                    </div>
                        <div class="clearfix" style="margin-bottom:5px;"></div>
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Customer Cashback <asp:Label ID="lblStarServiceType"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddl_Customer_CommissionType" cssclass="form-control"  runat="server" 
                                AutoPostBack="True">
                            <asp:ListItem>Not Applicable</asp:ListItem>
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Amount</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-sm-5">
                            <asp:TextBox ID="txt_Customer_Commission" cssclass="form-control" runat="server" 
                                 MaxLength="7"></asp:TextBox>
                             <asp:FilteredTextBoxExtender ID="txtFilteredCommission1" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
                                 TargetControlID="txt_Customer_Commission">
    </asp:FilteredTextBoxExtender>
                        </div>
                    </div>
                         <div class="form-group">
                       
                       <div class="clearfix" style="margin-bottom:5px;"></div>
                   <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                         <asp:Label ID="lblError" runat="server"></asp:Label>    
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>

  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Save" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                            	
                          <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                             cssclass="btn btn-primary mar_top10" Enabled="False" />&nbsp;
                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                         
                        
                          </div> 
                        
                    </div>
                  </div>
                  </form>
                    <div class="clearfix" style="margin-bottom:5px;"></div>
                <div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px"   Visible="false"
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
<%--
                  <div class="col-md-6">
                  <label></label>
                  </div>--%>
                   
              <div class="col-md-6">
              
              <div class="col-sm-6 pull-right">
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
             <div class="col-sm-12 table_head">
                        &nbsp;Reference Code Details ::</div>
            <div class="row">
                 <div class="col-sm-12">
                 <div class="table-responsive">
                    <div class="table_wid">
                   
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                          <Columns>
                              <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btnGrdRowDelete_Click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                            <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="GridView1_SelectedIndexChanged"
                                          CommandName="Select" Text=""><i class="fa fa-pencil"></i></asp:LinkButton>
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
            
        </div>
    </div>
</div>

 <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="350px" style="display:none;"  >
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"  DisplayAfter="0" AssociatedUpdatePanelID="updatepanel2"  >
                     <ProgressTemplate>
                     <div class="PopupWaiting">
                     <%--<img src="../images/ajax-loader.gif" />--%>
                    <%-- <img src="../images/loadmore.gif"  height="150px" />--%>
                     
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
            <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" OnClick="btnDeleteRow_Click" />
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


    </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
