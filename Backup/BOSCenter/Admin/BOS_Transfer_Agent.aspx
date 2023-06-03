<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Admin/SuperAdmin.Master"  CodeBehind="BOS_Transfer_Agent.aspx.vb" Inherits="BOSCenter.BOS_Transfer_Agent" %>
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
                    	Shift Distributor / Retailer</div>
                </div>
            </div>
        
        	<div class="log_form1">
            	
<div class="row">
        <div class="col-sm-12">
        <form class="form-horizontal">

                  <div class="row">
                      <div class="form-group">
                        <div class="col-sm-1">
                           &nbsp;
                           
                        </div>
                        
                        <div class="col-sm-3">
                    <asp:DropDownList ID="ddlAccount" cssclass="form-control inputtext"  runat="server" 
                                AutoPostBack="True" >
                              <asp:ListItem>Shift Distributor</asp:ListItem>
                              <asp:ListItem>Shift Retailer</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                           <label for="inputEmail3" class="col-sm-1 control-label" > Crieteria</label>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlSelectCriteria" cssclass="form-control inputtext"  runat="server" >
                                <asp:ListItem>All Records</asp:ListItem>
                                <asp:ListItem>Register ID</asp:ListItem>
                                <asp:ListItem>Name</asp:ListItem>
                                <asp:ListItem>Mobile No</asp:ListItem>
                              <asp:ListItem>Refrence Id</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                            <label for="inputEmail3" class="col-sm-1 control-label"> Value</label>
                        <div class="col-sm-2">
                           <asp:TextBox ID="txtSearchString" runat="server" 
                                cssclass="form-control inputtext" placeholder="Enter Value"></asp:TextBox>
                   
                        </div>
                        
                      </div>
                   </div>
                   
                   <div class="clearfix" style="margin-bottom:5px;"></div>

        
                  </form>
        </div>

      
        </div>



        <div class="row">
        
        <div class="form-group">
                        <div class="col-sm-4"></div>
                        <div class="col-sm-8">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                          
                          <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
                            <asp:Label ID="lblError1" runat="server" ></asp:Label>
                            <asp:Label ID="lblBranchName" Visible="False" runat="server" ></asp:Label>
                        </div>
                      </div>
        </div>

        <div class="row">
        <div class="form-group">

                  <div class="col-sm-12">
                    <div class="col-sm-4">
                    </div> 
                          <div class="col-sm-8">
                           <asp:Button ID="btnSearch" runat="server" Text="Search" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                            	
                        
                             <asp:Button ID="btnReset" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" CausesValidation="False" 
                                  UseSubmitBehavior="False" />  
                         


                        
                          &nbsp;  
                            	
                        
                             <asp:Button ID="btnWelcomeLetter" runat="server" Text="Shift Agent" 
                             cssclass="btn btn-primary mar_top10" CausesValidation="False" 
                                  UseSubmitBehavior="False" />  
                         


                        
                          </div> 
                        
                    </div>
                  	
                  </div>

                  <div class="clearfix" style="margin-bottom:5px;"></div>

                  <div class="row">
                 <div class="col-md-4"></div>


              <div class="col-md-2"></div>

                  </div>

</div>

<div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                 
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" Visible="false"
                          ImageUrl="~/images/excel_32X32.png"/>
                      <asp:ImageButton ID="Imagebtnword" runat="server" width="32px" Height="32px" Visible="false"
                          ImageUrl="~/images/word_32X32.png"/>
                      <asp:ImageButton ID="Imagebtnpdf" runat="server" width="32px" Height="32px" Visible="false"
                          ImageUrl="~/images/pdf_32X32.png"/>
                 
                 </div>
<%--
                  <div class="col-md-6">
                  <label></label>
                  </div>--%>
                   
              <div class="col-md-6">
              <label class="col-sm-2"></label>
              <div class="col-sm-4 pull-right">
              <asp:DropDownList ID="ddlNoOfRecords" runat="server" 
                      cssclass="form-control inputtext" AutoPostBack="True" Visible="false" >
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

        
    </div>
    
            
                 
                    <div class=" table_head">
                   &nbsp;&nbsp;   Search Details ::-
                        
                        
                        </div>
                    <div class="clearfix"></div>
                    <div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="ApprovalDiv">
                    <asp:GridView ID="GridView1" runat="server" cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True" PageSize="100000">
                          
                          <Columns>
                           
                             <asp:TemplateField ShowHeader="False">
                                
                             <HeaderTemplate>
                    <asp:Button ID="frmselectAll" runat="server" CommandName="btnfrmselectAll"  cssclass="btn btn-primary mar_top10"
                        Text="ALL" />
                    
                </HeaderTemplate>
                                  <ItemTemplate>
                                    
                                      <asp:CheckBox ID="chkSelect" runat="server" />
                                 
                                      <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>
                                    
                                  </ItemTemplate>
                              </asp:TemplateField>
                              

                            
                          </Columns>
               
                      </asp:GridView>

                        </div> 
                      </div>
                    </div>
                      
          
    
</div>


   
    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="400px"  style="display:none;">

<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td colspan="2" align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td colspan="2" align="center" bgcolor="Silver">
            <strong>Confirmation Dialog</strong>
            <br />
        </td>
       
    </tr>
    <tr>
        <td colspan="2" align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
   
     

    <tr>
        <td align="left">
          &nbsp;&nbsp;<asp:Label ID="lblChangeBranch" runat="server" Text="Shift To "></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
         
            <asp:DropDownList ID="ddlChangeAgent" runat="server" CssClass="form-control mar_top10" Width="200px">
              
           </asp:DropDownList>  &nbsp;&nbsp;
       </td>
     
    </tr>
   <tr>
        <td colspan="2" align="center">
            &nbsp;</td>
    </tr>
      <tr>
        <td colspan="2" align="center">
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblAccountRID" runat="server" Text="" Visible ="false"></asp:Label>  </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnUpdate" runat="server" Text="Proceed" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td colspan="2" align="center">
            &nbsp;</td>
    </tr>

</table>

    </asp:Panel>




</asp:Content>
