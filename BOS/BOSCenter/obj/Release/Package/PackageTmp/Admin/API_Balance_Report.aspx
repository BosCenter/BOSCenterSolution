<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="API_Balance_Report.aspx.vb" Inherits="BOSCenter.API_Balance_Report" %>
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
                    	<asp:Label ID="lblHeading" runat="server" Text="API BALANCE REPORT"></asp:Label>
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
                      <asp:DropDownList ID="ddlAgentType" cssclass="form-control inputtext"  runat="server" >
                                <asp:ListItem>All AgentType</asp:ListItem>
                                <asp:ListItem>Customer</asp:ListItem>
                                 <asp:ListItem>Master Distributor</asp:ListItem>
                                <asp:ListItem>Retailer</asp:ListItem>
                                <asp:ListItem>Distributor</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlSelectCriteria" cssclass="form-control inputtext"  runat="server" >
                                <asp:ListItem>All Records</asp:ListItem>
                                <asp:ListItem>Agent ID</asp:ListItem>
                                <asp:ListItem>Reference ID</asp:ListItem>
                                <asp:ListItem>Name</asp:ListItem>
                            </asp:DropDownList>
                           
                        </div>
                        <label for="inputEmail3" class="col-sm-2 control-label"> Value</label>
                        <div class="col-sm-4">
                           <asp:TextBox ID="txtSearchString" runat="server" 
                                cssclass="form-control inputtext" placeholder="Enter Value"></asp:TextBox>
                   
                        </div>
                      </div>
                   </div>

                   <div class="clearfix" style="margin-bottom:5px;"></div>

                   
          
                   
                                      
     
                  <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>
                              <asp:Label ID="lblExportQry" runat="server"  Visible="false"></asp:Label>
                          <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
                            <asp:Label ID="lblError1" runat="server" ></asp:Label>
                        </div>
                      </div>

                  


        
                  </form>
        </div>

        </div>


        <div class="row">
        <div class="form-group">

                  <div class="col-sm-12">
                    <div class="col-sm-4">
                    </div> 
                          <div class="col-sm-8">
                           <asp:Button ID="btnSearch" runat="server" Text="Search" 
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />
                             
                             &nbsp;  
                                                     
                             <asp:Button ID="btnReset" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" CausesValidation="False" 
                                  UseSubmitBehavior="False" />  
                         


                        
                          </div> 
                        
                    </div>
                
                  </div>

                  <div class="clearfix" style="margin-bottom:5px;"></div>

                  

</div>

<div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                 
                   <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
       
                
                    </ContentTemplate> 
                 <Triggers>
                
                                </Triggers>

                 </asp:UpdatePanel> 	
                 
                 </div>

                   
              <div class="col-md-6">
              <label class="col-sm-2"></label>
              <div class="col-sm-4 pull-right">
             
                  </div>
</div>
</div>

</div>
 </div>
       </div>

        
    </div>
    <div class=" table_head">
                   &nbsp;&nbsp;   Search Details ::
                        
                        
                        </div>
            
                 <div class="table-responsive">
                    <div class="table_wid">
                    
                    <div class="clearfix"></div>
                    <div runat="server" id="ApprovalDiv">
                    <asp:GridView ID="GridView1" runat="server"  cssclass="grid-view-themeclass"
                            BorderStyle="None" PageSize="10000000" ShowFooter="True">
                        
                          
                              <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
               
                      </asp:GridView>

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
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>  </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="Button2" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"/>
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
