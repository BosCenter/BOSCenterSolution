<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="SearchClientDetails.aspx.vb" Inherits="BOSCenter.SearchClientDetails" %>
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
                    	Search & Edit Client Details
                    </div>
                </div>
            </div>
        

        	
        	<div class="log_form1">
            	
                

<div class="row">
        <div class="col-sm-8">
        <form class="form-horizontal">

                    <div class="row">
                      <div class="form-group">

                      <div class="col-sm-4">
                          <asp:CheckBox ID="chkduration" runat="server" 
                              Text="&nbsp;Apply Registration Date" AutoPostBack="True" />
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                            <asp:Label ID="lblError0" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                        </div>
                          <%--           <asp:TemplateField HeaderText="SrNo">
            <ItemTemplate>
                <%# Container.DataItemIndex  + 1%>
            </ItemTemplate>
        </asp:TemplateField>
                              <asp:BoundField HeaderText="RID" DataField="RID" />--%>
                        
                      </div>
                      </div>

                      <div class="clearfix" style="margin-bottom:5px;"></div>

                       <div class="row">
                      <div class="form-group">
                      <label for="inputEmail3" class="col-sm-2 control-label">From</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtFrom" Enabled="false"  cssclass="form-control inputtext" runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                   
                            <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="txtFrom">
                            </asp:CalendarExtender>
                   
                        </div>
                        <label for="inputEmail3" class="col-sm-2 control-label">To</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtTO" Enabled="false"  cssclass="form-control inputtext" runat="server"  ClientIDMode="Static"  ></asp:TextBox>
                   
                            <asp:CalendarExtender ID="txtTO_CalendarExtender" runat="server" Enabled="True" 
                                TargetControlID="txtTO">
                            </asp:CalendarExtender>
                   
                        </div>
                      </div>
                   </div>

                   <div class="clearfix" style="margin-bottom:5px;"></div>

                   

                   <div class="row">
                      <div class="form-group">
                      <label for="inputEmail3" class="col-sm-2 control-label" style="padding-right: 14px;"> Crieteria</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlSelectCriteria" cssclass="form-control inputtext"  runat="server" >
                                <asp:ListItem>All Records</asp:ListItem>
                                <asp:ListItem>Company Code</asp:ListItem>
                                <asp:ListItem>Company Name</asp:ListItem>
                                <asp:ListItem>Contact person</asp:ListItem>
                                <asp:ListItem>Email ID</asp:ListItem>
                                <asp:ListItem>Mobile No</asp:ListItem>
                                <asp:ListItem>Active Status</asp:ListItem>
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
                          
                          <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
                            <asp:Label ID="lblError1" runat="server" ></asp:Label>
                        </div>
                      </div>

                  
                  </form>
        </div>

        <div class="col-sm-4" style="border:1px solid #ccc; padding:5px;">
        
        <div class="row">
           <div class="col-md-12">
           
            <div style="width:40%; float:left;" >
                         <asp:ListBox ID="lstAllColumn" runat="server" style="width:100%;height:150px" 
                             Font-Bold="True" Font-Size="Small" Font-Names="Verdana" 
                             SelectionMode="Multiple" >
                             
                             <asp:ListItem>CompanyHead</asp:ListItem>
                             <asp:ListItem>Address_1</asp:ListItem>
                             <asp:ListItem>Address_2</asp:ListItem>
                             <asp:ListItem>Address_3</asp:ListItem>
                             <asp:ListItem>PinCode</asp:ListItem>
                             <asp:ListItem>Country</asp:ListItem>
                             <asp:ListItem>State</asp:ListItem>
                             <asp:ListItem>District</asp:ListItem>
                             <asp:ListItem>City</asp:ListItem>
                             <asp:ListItem>PhoneNo_1</asp:ListItem>
                             <asp:ListItem>PhoneNo_2</asp:ListItem>
                             <asp:ListItem>Email_ID</asp:ListItem>
                             <asp:ListItem>WebRedirectUrl</asp:ListItem>
                              <asp:ListItem>GSTNo</asp:ListItem>
                             <asp:ListItem>TinNo</asp:ListItem>
                             
                         </asp:ListBox>
                        </div>
                
                        <div style="width:20%; float:left; text-align: center; margin-top: 50px;">
                            <asp:Button ID="btnColumnAdd" cssclass="btn-primary"  runat="server" 
                                Width="28px" Height="40px" Text=">>" />&nbsp;
                             <asp:Button ID="btnColumnRemove" cssclass="btn-primary" runat="server" 
                                Width="28px" Height="40px" Text="<<" />
                        </div>
                         
                        <div style="width:40%; float:left;">
                            <asp:ListBox ID="lstShowColumn" runat="server" style="width:100%;height:150px" 
                             Font-Bold="True" Font-Size="Small" Font-Names="Verdana" 
                                SelectionMode="Multiple" >
                                <asp:ListItem>CompanyCode</asp:ListItem>
                              <asp:ListItem>CompanyName</asp:ListItem>
                              <asp:ListItem>ContactPerson</asp:ListItem>
                              <asp:ListItem>Mobile_No</asp:ListItem>
                              <asp:ListItem>ClientPassword</asp:ListItem>
                              <asp:ListItem>ClientPin</asp:ListItem>
                              <asp:ListItem>CreditBalnceLimit</asp:ListItem>
                              <asp:ListItem>Status</asp:ListItem>
                              
                         </asp:ListBox>
                         
                        </div>


           
           </div>       
                    
                     
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
                 
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/excel_32X32.png"/>
                      <asp:ImageButton ID="Imagebtnword" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/word_32X32.png"/>
                      <asp:ImageButton ID="Imagebtnpdf" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/pdf_32X32.png"/>
                 
                 </div>

                   
              <div class="col-md-6">
              <label class="col-sm-2"></label>
              <div class="col-sm-4 pull-right">
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

        
    </div>
      <div class=" table_head">
                   &nbsp;&nbsp;   Search Details ::
                        
                        
                        </div>
            
                 <div class="table-responsive">
                    <div class="table_wid">
                  
                    <div class="clearfix"></div>
                    <div runat="server" id="ApprovalDiv">
                    <asp:GridView ID="GridView1" runat="server"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                          
                          <Columns>
              
                             <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="DeleteRow_click"
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
