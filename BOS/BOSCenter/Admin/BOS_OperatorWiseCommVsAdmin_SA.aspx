<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_OperatorWiseCommVsAdmin_SA.aspx.vb" Inherits="BOSCenter.BOS_OperatorWiseCommVsAdmin_SA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">

       function disableSubmit() {
           //document.getElementById('<%= lblError.ClientID %>').style.display = "";

           var t = setTimeout("enableSubmit()", 3000);
       }

       function enableSubmit() {
           document.getElementsByName("lblError").value = "";

           document.getElementById('<%= lblError.ClientID %>').style.display = "none";
       }
   
    
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
    <div class="container">
	<div class="row">
    	<div class ="col-sm-11 col-sm-offset-1">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Operator Wise Commission Vs Admin Form
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	
                 <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Admin <asp:Label ID="Label2" runat="server"  ForeColor="Red"  Text=" *"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddl_Admin" cssclass="form-control"  runat="server" 
                                AutoPostBack="true">
                         
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix" style="margin-bottom:5px;"></div>
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">API Name <asp:Label ID="lblStarType" runat="server"  ForeColor="Red"  Text=" *"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlProductService" cssclass="form-control"  runat="server" AutoPostBack="true">
                         
                            </asp:DropDownList>
                        </div>
                    </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>

                       
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Category <asp:Label ID="lblStarContain"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlContainCategory" cssclass="form-control"  runat="server" AutoPostBack="true">
                           
                            </asp:DropDownList>
                        </div>
                    </div>
                     
                                   
                    <div class="clearfix" style="margin-bottom:5px;"></div>
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Admin Commission <asp:Label ID="lblStarServiceType"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddl_AD_CommsissionType" cssclass="form-control"  runat="server" 
                                AutoPostBack="True">
                            <asp:ListItem>Not Applicable</asp:ListItem>
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Amount</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-sm-5">
                            <asp:TextBox ID="txt_AD_Commission" cssclass="form-control" runat="server"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtFilteredCommission1" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" 
                                 TargetControlID="txt_AD_Commission">
    </asp:FilteredTextBoxExtender>
                        </div>
                    </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>
                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Service Charge <asp:Label ID="Label1"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlServiceCharge" cssclass="form-control"  runat="server" 
                                AutoPostBack="True">
                            <asp:ListItem>Not Applicable</asp:ListItem>
                            <asp:ListItem>Percentage</asp:ListItem>
                            <asp:ListItem>Amount</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-sm-5">
                            <asp:TextBox ID="txtServiceCharge" cssclass="form-control" runat="server"></asp:TextBox>
                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
        runat="server" Enabled="True" FilterType="Custom" ValidChars=".0123456789" TargetControlID="txtServiceCharge">
    </asp:FilteredTextBoxExtender>
                        </div>
                    </div>

                       <div class="clearfix" style="margin-bottom:5px;"></div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Status <asp:Label ID="lblStarStatus"  ForeColor="Red" runat="server" Text=" *"></asp:Label></label>
                        <div class="col-sm-9">
                           
                            <asp:DropDownList ID="ddlStatus" runat="server" class='form-control'>
 <asp:ListItem>Active</asp:ListItem>
 <asp:ListItem>Inactive</asp:ListItem>
    </asp:DropDownList>

                        </div>
                      </div>
                    
                       <div class="clearfix" style="margin-bottom:5px;"></div>
                   <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                         <asp:Label ID="lblError" runat="server"></asp:Label>    
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCategory" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>

  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                           <asp:Button ID="btnSave" runat="server" Text="Save" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a"  OnClientClick="disableSubmit()" />&nbsp;  
                            	
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
<%--
                  <div class="col-md-6">
                  <label></label>
                  </div>--%>
                   
              <div class="col-md-6">
              
              <div class="col-sm-6 pull-right">
                  </div>
</div>
</div>

</div>
</div>
            
             <div class="col-sm-12 table_head">
                        &nbsp;Search Details ::</div>
            <div class="row">
                 <div class="col-sm-12">
                 <div class="table-responsive">
                    <div class="table_wid">
                   
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"   cssclass="grid-view-themeclass"
                            BorderStyle="None" PageSize="100000">
                                                                                   

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
