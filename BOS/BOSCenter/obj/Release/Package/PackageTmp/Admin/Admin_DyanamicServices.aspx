﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Admin_DyanamicServices.aspx.vb" Inherits="BOSCenter.Admin_DyanamicServices" %>
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
                    	Dynamic Services
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	
                    <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Title</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtTitle" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                      </div>
                   
          
                      <div class="clearfix" style="margin-bottom:5px;"></div>
            
            <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Icon</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtIcon" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                      </div>
                   
          
                      <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Postback Url</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtPostBackUrl" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                      </div>
                   
            <div class="clearfix" style="margin-bottom:5px;"></div>

                        <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Order No</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtOrderNo" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtOrderNo_FilteredTextBoxExtender" 
        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOrderNo">
    </asp:FilteredTextBoxExtender>
                        </div>
                      </div>
                      <div class="clearfix" style="margin-bottom:5px;"></div>



                  <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                             <asp:Label ID="lblError" ClientIDMode="Static" runat="server"  ></asp:Label>
                   
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtTitle" ErrorMessage="Enter Title" CssClass="errorlabels"  
                                 ValidationGroup="a"></asp:RequiredFieldValidator>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtIcon" ErrorMessage="Enter Icon" CssClass="errorlabels"  
                                 ValidationGroup="a"></asp:RequiredFieldValidator>
                               
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtOrderNo" ErrorMessage="Enter OrderNo" CssClass="errorlabels"  
                                 ValidationGroup="a"></asp:RequiredFieldValidator>

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
                  <div class="clearfix"></div>
                
            </div>
            <div class="col-sm-12 table_head">
                        Search Details ::
                        </div>
            <div class="row">
                 <div class="col-sm-12">
                 <div class="table-responsive">
                    <div class="table_wid">
                    
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                          <Columns>
                              <asp:TemplateField HeaderText="SrNo">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
                              <asp:BoundField HeaderText="ServiceName" DataField="ServiceName" />
                              <asp:BoundField HeaderText="IconName" DataField="IconName" />
                              <asp:BoundField HeaderText="PostbackUrl" DataField="PostbackUrl" />
                              <asp:BoundField HeaderText="OrderNo" DataField="OrderNo" />
                              <asp:BoundField HeaderText="RID" DataField="RID" />
                              
                            
                             <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btnGrdRowDelete_Click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>

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




</asp:Content>
