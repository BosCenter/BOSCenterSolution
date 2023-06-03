<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Admin_AreaMaster.aspx.vb" Inherits="BOSCenter.Admin_AreaMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</br>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
     <div class="container">
	<div class="row">
    	<div class="col-sm-8 col-sm-offset-2">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	::: Area Master :::
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	
                    <form class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Select Country</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlCountry" cssclass="form-control" runat="server" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                       <div class="clearfix" style="margin-bottom:5px;"></div>




                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Select State</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlStateName" cssclass="form-control" runat="server" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>

                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Select District</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddldistrict" cssclass="form-control" runat="server" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    


                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Enter Area</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtarea" cssclass="form-control" runat="server"></asp:TextBox>
                       
                        </div>
                      </div>

                       <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Enter PinCode</label>
                        <div class="col-sm-9">
                           <asp:TextBox ID="txtPincode" cssclass="form-control inputtext" runat="server"  
                                ClientIDMode="Static" MaxLength="6" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtPincode_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtPincode">
                            </asp:FilteredTextBoxExtender>
                        </div>
                      </div>


                      <div class="clearfix" style="margin-bottom:5px;"></div>
                   

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblError" runat="server" ></asp:Label>
                          
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblExportQry" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>&nbsp;
                        <div class="clearfix" style="margin-bottom:5px;"></div>
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
                
                <div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                 
            <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
       
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/excel_32X32.png"/>
                      <asp:ImageButton ID="ImagebtnWOrd" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/word_32X32.png"/>
                      <asp:ImageButton ID="Imagepdf" runat="server" width="32px" Height="32px" 
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
            <div class="row">
                 <div class="col-sm-12">
                 <div class="table-responsive">
                    <div class="table_wid">
                    <div class="col-sm-12 table_head">
                        &nbsp;Area Details</div>
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  AllowPaging="True" cssclass="grid-view-themeclass"
                            BorderStyle="None">
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                          <Columns>
                                <asp:TemplateField HeaderText="SrNo">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
                              <asp:BoundField HeaderText="Country" DataField="Country_Name" />
                              <asp:BoundField HeaderText="State" DataField="State_Name" />
                              <asp:BoundField HeaderText="District" DataField="District_Name" />
                              <asp:BoundField HeaderText="Area" DataField="Area_Name" />
                              <asp:BoundField HeaderText="Pin Code" DataField="PinCode" />
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
    </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
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
    </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
