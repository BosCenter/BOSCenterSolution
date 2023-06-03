<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Admin_CreateAllBackup.aspx.vb" Inherits="BOSCenter.Admin_CreateAllBackup" %>
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
                    	Create & Download Database Backup
                    </div>
                </div>
            </div>
        

        	
        	<div class="log_form1">
            	
                

<div class="row">
        <div class="col-sm-10">
        <form class="form-horizontal">

                  
                      <div class="clearfix" style="margin-bottom:5px;"></div>

                  
                   <div class="row">
                      <div class="form-group">
                          <div class="col-sm-3">
                            <asp:DropDownList ID="ddlSelectDatabase" cssclass="form-control inputtext mar_top10 "  
                                runat="server"  >
                                                         
                            </asp:DropDownList>&nbsp;
                           
                        </div>
                        <div class="col-sm-3">
                          <asp:Button ID="btnCreate" runat="server" Text="Create Backup Now" 
                             cssclass="btn btn-primary mar_top10" />  &nbsp;
                            	

                        </div>
                        <div class="col-sm-3">
                          <asp:Button ID="btnLastFiveBackups" runat="server" Text="Last Seven Backups" 
                             cssclass="btn btn-primary mar_top10" />  
                            	

                        </div>
                        <div class="col-sm-3">
                          <asp:Button ID="btnReset" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                            	

                        </div>
                       
                      </div>
                   </div>

                   <div class="clearfix" style="margin-bottom:5px;"></div>

                   
        
                  </form>
        </div>

        
        
        </div>
        </div>


        <div class="row">
           <div class="form-group">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-8">
                <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblAccountNo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblAccountType" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
                <asp:Label ID="lblError1" runat="server" ></asp:Label>
                <asp:Label ID="lblExportQry" runat="server" style="margin-top:5px;" Visible="false"></asp:Label>
                </div>
            </div>
         </div>


          <div class=" table_head">&nbsp;&nbsp;   Search Details ::-</div>

                          <div class="table-responsive">
                    <div class="table_wid">

                    <div class="clearfix"></div>
                           
                    <asp:GridView ID="GridView1" runat="server"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                          
                          <Columns>
                          <asp:TemplateField ShowHeader="False">
                              <HeaderTemplate>
                    <%--<asp:Button ID="frmselectAll" runat="server" CommandName="btnfrmselectAll"  cssclass="btn btn-warning mar_top10"
                        Text="All Download" />--%>
                    
                </HeaderTemplate>
                             
                                  <ItemTemplate>
                                      
                                        <asp:UpdatePanel runat='server' ID='updatepanel1'>
<ContentTemplate>
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="DownloadFiles"
                                          CommandName="Select" Text=""><i class="fa fa-arrow-down">Download</i></asp:LinkButton>
                                          
                                          </ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="LinkButton2" />


</Triggers>
</asp:UpdatePanel>
                                  </ItemTemplate>
                                  <HeaderStyle Width="100px" />
                                  <ItemStyle Width="100px" />
                                  
                              </asp:TemplateField>
                              
                                <asp:TemplateField ShowHeader="False">
                             
                             
                                  <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="DeleteRow_click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                       
                                  </ItemTemplate>
                                  <HeaderStyle Width="100px" />
                                  <ItemStyle Width="100px" />
                                  
                              </asp:TemplateField>



                            
                          </Columns>
                              <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
               
                      </asp:GridView>

                        
                      </div>
                    </div>
        

        </div> 
        
        </div> </div> 
            

               


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
        <td align="center" bgcolor="Silver">&nbsp;
            </td>
    </tr>
    <tr>
        <td align="center">&nbsp;
            </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>  </td>
    </tr>
    <tr>
        <td align="center">&nbsp;
            </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="Button2" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">&nbsp;
            </td>
    </tr>
</table>



    </asp:Panel>

    



</asp:Content>
