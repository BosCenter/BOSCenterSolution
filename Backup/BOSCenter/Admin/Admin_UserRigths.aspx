<%@ Page Title="" Language="vb" AutoEventWireup="false"  MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="Admin_UserRigths.aspx.vb" Inherits="BOSCenter.Admin_UserRigths" %>
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
                      
                       <div class="form-group">
                       <div class="col-sm-12 ">
                       <label>Select Group</label>

                      <asp:DropDownList ID="ddlGroup" CssClass="form-control" runat="server" 
                           AutoPostBack="True">
                           <asp:ListItem>:: Select Group ::</asp:ListItem>
                           <asp:ListItem>Admin</asp:ListItem>
                           <asp:ListItem>Master Distributor</asp:ListItem>
                           <asp:ListItem>Distributor</asp:ListItem>
                           <asp:ListItem>Retailer</asp:ListItem>
                           <asp:ListItem>Customer</asp:ListItem>
                           </asp:DropDownList>


                       </div>                       
                       </div>

                      <div class="form-group">
                       <div class="col-sm-12 ">
                       <label>Select Module</label>

                        <asp:ListBox ID="lstModules" runat="server"  CssClass="form-control"
                              Height="400px" AutoPostBack="True"  Font-Bold="True" 
                               Font-Size="12pt" >
                           </asp:ListBox>


                       </div>                       
</div>

                    </div>
                     </div> 
  </div>

     <div class="col-sm-9">
        
        	<div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	Apply User Rights
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
            <asp:TemplateField>
             <HeaderTemplate>
             <asp:Label ID="dddf" runat="server"  Text="View"></asp:Label>
             <asp:Button ID="frmselectAll"  CommandName="btnfrmselectAll"  CssClass="btn btn-primary mar_top10" Text="ALL" runat="server" /><br>
             
            </HeaderTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" 
                        Checked='<%# Bind("FrmSelected") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server"  CssClass="mar_top10"
                        Checked='<%# Bind("FrmSelected") %>' Enabled="true" />
                </ItemTemplate>
                <ControlStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="NavigationModule" HeaderText="Navigation_Module" />
            
            
            
            
            <asp:TemplateField HeaderText="Save">
            <HeaderTemplate>
            <asp:Label ID="dd1" runat="server"  Text="Save"></asp:Label>
            <asp:Button ID="SaveAll" CommandName="btnSaveAll" CssClass="btn btn-primary mar_top10"  Text="ALL" runat="server" /><br>
             
            </HeaderTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkSave" runat="server" Checked='<%# Bind("CanSave") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSave" runat="server" CssClass="mar_top10" Checked='<%# Bind("CanSave") %>' 
                        Enabled="true" />
                </ItemTemplate>
                <ControlStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Search">
            <HeaderTemplate>
            <asp:Label ID="dd190" runat="server"  Text="Search"></asp:Label>
            <asp:Button ID="SearchAll" CommandName="btnSearchAll" CssClass="btn btn-primary mar_top10"  Text="ALL" runat="server" /><br>
             
            </HeaderTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkSearch" runat="server" Checked='<%# Bind("CanSearch") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSearch" runat="server" CssClass="mar_top10" Checked='<%# Bind("CanSearch") %>' 
                        Enabled="true" />
                </ItemTemplate>
                 <ControlStyle Width="70px" />
                 <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Update">
             <HeaderTemplate>
             <asp:Label ID="dd98" runat="server"  Text="Update"></asp:Label>
             <asp:Button ID="UpdateAll"  CommandName="btnUpdateAll" CssClass="btn btn-primary mar_top10" Text="ALL" runat="server" /><br>
             
            </HeaderTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkUpdate" runat="server" Checked='<%# Bind("CanUpdate") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkUpdate" runat="server" CssClass="mar_top10" Checked='<%# Bind("CanUpdate") %>' 
                        Enabled="true" />
                </ItemTemplate>
                <ControlStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
             <HeaderTemplate>
             <asp:Label ID="dde" runat="server"  Text="Delete"></asp:Label>
             <asp:Button ID="DeleteAll"  CommandName="btnDeleteAll" CssClass="btn btn-primary mar_top10"  Text="ALL" runat="server" /><br>
             
            </HeaderTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkDelete" runat="server" 
                        Checked='<%# Bind("CanDelete") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkDelete" runat="server" CssClass="mar_top10" Checked='<%# Bind("CanDelete") %>' 
                        Enabled="true" />
                </ItemTemplate>
                <ControlStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:BoundField DataField="RefModule" HeaderText="Ref_Module" />
            <asp:BoundField DataField="RefSubModule" HeaderText="Ref_Sub_Module" />
            <asp:BoundField DataField="FormName" HeaderText="Forms" />
             <asp:BoundField DataField="Searching_Keyword" HeaderText="Keyword" />
            
            
            
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
    <asp:Panel ID="pnlSavePopUp" runat="server" Width="400px" >

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
