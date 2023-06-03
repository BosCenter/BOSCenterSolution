<%@ Page Title="" Language="vb" AutoEventWireup="false"  MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Admin_UserRigths_Dashboard.aspx.vb" Inherits="BOSCenter.Admin_UserRigths_Dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <style type="text/css">
        .ListBoxCssClass
        {
            color:Black;
            
            font-family:Courier New;
            font-size:large;
            font-style:italic;
            }
    </style>
<br />

 <div class="container">
	<div class="row col-sm-12">
    
    
     <div class="col-sm-3">
        <div class="row">
 <div class="col-sm-12">

                	<div class="log_form_head1" style="margin-top:7px;">
                    	Select Modules
                    </div>
                </div>
                 
                	</div>
                   <div class="log_form1">
                   <div class="form-horizontal">

                   <label>Company</label>
                    &nbsp;<asp:DropDownList ID="ddlCompany" CssClass="form-control" runat="server" 
                           AutoPostBack="True">

                           </asp:DropDownList>
                           <label><asp:Label ID="lblGroupError" runat="server" ForeColor="Red" ></asp:Label></label>
                           <asp:Label ID="lblDatabaseName" runat="server" Visible="false" ForeColor="Red" ></asp:Label>
                      
                      <%-- <div class="form-group">
                       <div class="col-sm-12 ">
                       <label>Select Group</label>

                      <asp:DropDownList ID="ddlGroup" CssClass="form-control" runat="server" >
                           <asp:ListItem>:: Select Group ::</asp:ListItem>
                           <asp:ListItem Selected="True">Admin</asp:ListItem>
                           </asp:DropDownList>


                       </div>                       
                       </div>--%>

                      

                    </div>
                     </div> 
  </div>

     <div class="col-sm-9">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Apply Service Rights for Dashboard
                    </div>
                </div>
                	
            </div>
        	<div class="log_form1">
            
                    <div class="form-horizontal">
                    
                      
                      
            <div class="clearfix" style="margin-bottom:5px;"></div>
                    
                      

                      <div class="clearfix" style="margin-bottom:5px;"></div>
                      <div class="form-group">
                       
                        <div class="col-sm-12">


                              <div class="table-responsive">
                    <div class="table_wid">
                    
                    
                    


                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  cssclass="grid-view-themeclass" 
        ShowHeaderWhenEmpty="True" Width="100%" PageSize="1000">
        <Columns>
            <asp:BoundField HeaderText="SrNo" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="Save">
            <HeaderTemplate>
            <asp:Label ID="dd1" runat="server"  Text="ShowServices"></asp:Label><br />
            <asp:Button ID="SaveAll" CommandName="btnSaveAll" CssClass="btn btn-primary mar_top10"  Text="ALL" runat="server" /><br>
             
            </HeaderTemplate>
               
                <ItemTemplate>
                    <asp:CheckBox ID="chkSave" runat="server" CssClass="mar_top10" Enabled="true" />
                </ItemTemplate>
                <ControlStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Search">
            <HeaderTemplate>
            <asp:Label ID="dd190" runat="server"  Text="ShowButton"></asp:Label><br />
            <asp:Button ID="SearchAll" CommandName="btnSearchAll" CssClass="btn btn-primary mar_top10"  Text="ALL" runat="server" /><br>
             
            </HeaderTemplate>
              
                <ItemTemplate>
                    <asp:CheckBox ID="chkSearch" runat="server" CssClass="mar_top10" Enabled="true" />

                        <asp:Label ID="lblIconName" runat="server" Text='<%# Eval("IconName") %>' Visible="false" ></asp:Label>
                        <asp:Label ID="lblPostbackUrl" runat="server" Text='<%# Eval("PostbackUrl") %>' Visible="false" ></asp:Label>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrderNo") %>' Visible="false" ></asp:Label>
              </ItemTemplate>
                 <ControlStyle Width="70px" />
                 <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="ServiceName" HeaderText="ServiceName" />
            
        </Columns>
    </asp:GridView>
                    </div>
                                </div>

                              <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                          <asp:Label ID="lblError" runat="server" ></asp:Label>
                            
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>
        
     <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-4">
                    </div> 
                          <div class="col-sm-8">
                           <asp:Button ID="btnSave" runat="server" Text="Save" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" Enabled="False" />&nbsp;  

                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" Enabled="False" />  
                         
                          </div> 
                    </div>
                  </div>



                        </div>
                   
                      </div>
                      
                    
               </div>
                 
                
            </div>
            
            
        </div>
        
     

   

    </div>

    </div>  
  <asp:Button ID="btnSavingModelpopUp" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnSavingModelpopUp" PopupControlID="pnlSavePopUp"  BackgroundCssClass="modalBackground"  CancelControlID="BtnSaveCancel" >
    </asp:ModalPopupExtender>
  <asp:Panel ID="pnlSavePopUp" runat="server" Width="400px" style="display:none;">
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver">&nbsp;</td>
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
            <asp:Label ID="lblSavingDialogBox" runat="server" Text=""></asp:Label> 
            

        </td>
    </tr>

   
     <tr>
        <td align="center">
        &nbsp;
         </td>
    </tr>
    
    <tr>
        <td align="center">

            
            <asp:Button ID="btnsaveOk" runat="server" Text="Yes" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnSaveCancel" runat="server" Text="No" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>
 </asp:Panel>

</asp:Content>
