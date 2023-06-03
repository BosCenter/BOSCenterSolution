<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="SuperAdminMenuCreation.aspx.vb" Inherits="BOSCenter.SuperAdminMenuCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
	<div class="row">
    	<div class="col-sm-8 col-sm-offset-2">
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Super Admin Menu Form
                    </div>
                </div>
            </div>
        	<div class="log_form1">
            	 <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Select Main Module</label>
                        
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlRefModule" cssclass="form-control" runat="server" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="clearfix" style="margin-bottom:5px;"></div>
                    
                    <div class="form-group" id="Div_RefSubModule" runat="server" >
                        <label for="inputEmail3" class="col-sm-4 control-label">Select Sub Module</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlRefSubModule" cssclass="form-control" runat="server" 
                                AutoPostBack="True">
                            </asp:DropDownList>

                                <div class="clearfix" style="margin-bottom:2px;"></div>
                        </div>     
                    </div>





                    <div class="clearfix" style="margin-bottom:5px;"></div>

                    <form class="form-horizontal">
                    
                       <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Enter Navigation Module</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtNavigationModule" cssclass="form-control" runat="server"></asp:TextBox>
                        

    
                        </div>
                      </div>
                    <div class="clearfix" style="margin-bottom:5px;"></div>


                    



                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Enter&nbsp; Navigation Order</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtNavigationModule_Order" cssclass="form-control" 
                                runat="server" Text=""  ></asp:TextBox>
                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
        runat="server" Enabled="True" FilterType="Numbers" 
        TargetControlID="txtNavigationModule_Order"></asp:FilteredTextBoxExtender>
                        </div>
                        
                      </div>
                    <div class="clearfix" style="margin-bottom:5px;"></div>


                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Enter&nbsp; Url</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFormName" cssclass="form-control" runat="server"></asp:TextBox>
                      
                        </div>
                        
                      </div>
                                     <div class="clearfix" style="margin-bottom:5px;"></div>
     
            <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Searching Keyword</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtsearchingKeyword" cssclass="form-control inputtext" 
                                runat="server"  ClientIDMode="Static" ></asp:TextBox>
                        </div>
                      </div>
                         <div class="clearfix" style="margin-bottom:5px;"></div>
                   
                   <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label"></label>
                        <div class="col-sm-8">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                         <asp:Label ID="lblError" runat="server"></asp:Label>
                          <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="ddlRefModule" ErrorMessage="Select Module" CssClass="errorlabels"  
                                 ValidationGroup="a" Display="Dynamic" InitialValue="Select" ></asp:RequiredFieldValidator>

                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtRefSubModule" ErrorMessage="Enter Sub Module" CssClass="errorlabels"  
                                 ValidationGroup="a" Display="Dynamic" ></asp:RequiredFieldValidator>
                         
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtFormName" ErrorMessage="Enter url" CssClass="errorlabels"  
                                 ValidationGroup="a" Display="Dynamic" ></asp:RequiredFieldValidator>

                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtNavigationModule" ErrorMessage="Enter Navigation Module" CssClass="errorlabels"  
                                 ValidationGroup="a" Display="Dynamic" ></asp:RequiredFieldValidator>
--%>

                                       
                         
                             
                         
                            <asp:Label ID="lblIsSubMenu_Available" runat="server" Visible="False"  ></asp:Label>
                            <asp:Label ID="lblOldRefModule" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblOldRefSubModule" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lbloldSearchKeyword" runat="server" Visible="False"></asp:Label>
&nbsp;<asp:Label ID="lblOldNavigationModule" runat="server" Visible="False"></asp:Label>
&nbsp;<asp:Label ID="lblOldUrl" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>



                  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-4">
                    </div> 
                          <div class="col-sm-8">
                           <asp:Button ID="btnSave" runat="server" Text="Save" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                            	
                          <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                             cssclass="btn btn-primary mar_top10" Enabled="False" />&nbsp;
                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                         
                        
                          </div> 
                        
                    </div>
                  </div>

                  <div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                 
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/excel_32X32.png" Visible="false"/>
                      <asp:ImageButton ID="Imagebtnword" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/word_32X32.png" Visible="false"/>
                      <asp:ImageButton ID="Imagebtnpdf" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/pdf_32X32.png" Visible="false"/>
                 
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


                  </form>
                  <div class="clearfix"></div>
                
            </div>
            
        </div>

    </div>
      <div class="col-sm-12 table_head">
                        Menu Details</div>  
    <div class="row">
                 <div class="col-sm-11">
                 <div class="table-responsive">
                    <div class="table_wid">
                  
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                          <Columns>
                            
                            <asp:BoundField HeaderText="SrNo" DataField="SrNo" />
                              <asp:BoundField HeaderText="Module" DataField="RefModule" />
                              <asp:BoundField HeaderText="SubModule" DataField="RefSubModule" />
                              <asp:BoundField HeaderText="NavigationModule" DataField="NavigationModule" />
                              <asp:BoundField HeaderText="NavigationModule_Order" DataField="NavigationModule_Order" />
                              <asp:BoundField HeaderText="FormName" DataField="FormName" />
                                <asp:BoundField DataField="Searching_Keyword" HeaderText="Searching_Keyword" />
                             
                              
                            
                              <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btnGrdRowDelete_Click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                            <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="GridView1_SelectedIndexChanged"
                                          CommandName="Select" Text=""><i class="fa fa-pencil-square-o fa-lg"></i></asp:LinkButton>
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
            Are you sure you want to delete ?</td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="Button2" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" OnClick="btnDeleteRow_Click" />
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
