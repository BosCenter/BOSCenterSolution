<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Admin/SuperAdmin.Master"   CodeBehind="BOS_ClosedComplaint_Report_SA.aspx.vb" Inherits="BOSCenter.BOS_ClosedComplaint_Report_SA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src='https://kit.fontawesome.com/a076d05399.js'></script>
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
                        <asp:Label ID="lblHeading" runat="server" Text="Closed Complaint Report"></asp:Label>	
                    </div>
                </div>
            </div>
        

        	
        	<div class="log_form1">
            	
                

<div class="row">
        <div class="col-sm-10 col-lg-offset-1">
        <form class="form-horizontal">


                   <div class="row">

                 
                      <div class="form-group">
                       <div class="col-sm-3">&nbsp;
                       <%-- <asp:CheckBox ID="chkduration" runat="server" 
                              Text="&nbsp;All Pending" AutoPostBack="True" />--%>
                           
                        </div>
                       
                      <label for="inputEmail3" class="col-sm-1 control-label" style="padding-right: 14px;"> Criteria</label>
                        <div class="col-sm-3">
                          <asp:DropDownList ID="ddlSelectCriteria" cssclass="form-control"  runat="server" >
                                <asp:ListItem>All Records</asp:ListItem>
                                <asp:ListItem>Register ID</asp:ListItem>
                                <asp:ListItem>Complaint ID</asp:ListItem>
                                <asp:ListItem>Product Service</asp:ListItem>
                            </asp:DropDownList>
                           
                           
                        </div>
                        <label for="inputEmail3" class="col-sm-1 control-label"> Value</label>
                        <div class="col-sm-3">
                           <asp:TextBox ID="txtSearchingValue" runat="server" 
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
                        <div class="col-sm-5">
                        
                        
                        </div>
                        <div class="col-sm-7">
                            <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
                          
                          <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
                            <asp:Label ID="lblRID" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblExportQry" runat="server"  Visible="false"></asp:Label>
                            <asp:Label ID="lblError1" runat="server" ></asp:Label>
                        </div>
                      </div>
        
        </div>

        <div class="row">
        <div class="form-group">

                  <div class="col-sm-12">
                    <div class="col-sm-5">
                    </div> 
                          <div class="col-sm-7">
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
                 
                  <asp:UpdatePanel ID="UpdatePanel2"  runat="server">
        <ContentTemplate>
       
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/excel_32X32.png"/>
                      <asp:ImageButton ID="ImagebtnWOrd" runat="server" width="32px" Height="32px"  Visible="false"
                          ImageUrl="~/images/word_32X32.png"/>
                      <asp:ImageButton ID="ImagebtnPdf" runat="server" width="32px" Height="32px"  Visible="false"
                          ImageUrl="~/images/pdf_32X32.png"/>
                    </ContentTemplate> 
                 <Triggers>
                 <asp:PostBackTrigger ControlID="ImagebtnExcel" />
                 <asp:PostBackTrigger ControlID="ImagebtnWOrd" />
                 <asp:PostBackTrigger ControlID="ImagebtnPdf" />
                 </Triggers>

                 </asp:UpdatePanel> 
                 
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

         <div class="col-md-12" >
            
                 <div class="table-responsive">
                    <div class="table_wid">
                    <div class="clearfix"></div>
                    <div runat="server" id="ApprovalDiv">

                    <asp:GridView ID="GridView1" runat="server"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True" AutoGenerateColumns="False">
                         
                          <Columns>
                 
                              
                          

                            
                              <asp:BoundField DataField="SrNo" HeaderText="Sr No" />
                              <asp:BoundField DataField="ComplaintDate" HeaderText="Complaint Date" />
                              <asp:BoundField DataField="ComplaintID" HeaderText="Complaint ID" />
                              <asp:BoundField DataField="kCode" HeaderText="kCode" />
                              <asp:BoundField DataField="kCodeType" HeaderText="kCode Type" />
                              <asp:BoundField DataField="Product" HeaderText="Product" />
                               <asp:BoundField DataField="ComplaintProblem" HeaderText="Complaint Problem" />
                            
                               <asp:TemplateField ShowHeader="true" HeaderText="Attachment">
                                  <ItemTemplate>
                                    <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>
                                     <asp:Label ID="lblAttachment" runat="server" Text='<%# Eval("Attachment") %>' Visible="false" ></asp:Label>   
                                     <%-- <asp:Button ID="btnAttachment" runat="server" Text="Download" CssClass="btn btn-primary" OnClick="GridView1_Download"/>--%>
                                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkdwnload" runat="server" CausesValidation="False" CssClass="btn btn-primary"  OnClick="DownloadRow_click" Text=""><i class=" fa fa-download">&nbsp;Download</i></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkdwnload" />
                                            </Triggers>
                                         </asp:UpdatePanel>
                                     </ItemTemplate>
                              </asp:TemplateField>
                            
                            <asp:BoundField DataField="ComplaintStatus" HeaderText="Complaint Status" />
                            <asp:BoundField DataField="AllotedTo" HeaderText="Alloted To" />
                            <asp:BoundField DataField="AllotedDateTime" HeaderText="AllotedDate Time" />
                            <asp:BoundField DataField="ClosedBy" HeaderText="ClosedBy" />
                            <asp:BoundField DataField="ClosedDateTime" HeaderText="ClosedDate Time" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                             <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                     <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="ChatDetails_Click"
                                          CommandName="Select" Text=""><i class=" fas fa-comment">&nbsp;Chat</i></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="CompanyCode" HeaderText="CompanyCode" />
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
    
    





    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="380px" style="display:none;" >

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
        <td colspan="2" align="center">
            &nbsp;</td>
    </tr>
 


    <tr>
        <td align="right">
          <asp:Label ID="lblTOtime" runat="server" Text="Select Employee"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
       
            <asp:DropDownList ID="ddlEmployee" runat="server">
               
           </asp:DropDownList>
       &nbsp;</td>
     
    </tr>
   <tr>
        <td colspan="2" align="center">
            &nbsp;</td>
    </tr>
      <tr>
        <td colspan="2" align="center">
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>  </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="btn btn-primary"/>
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


<asp:Button ID="Button1" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupChat" runat="server" TargetControlID="Button1" PopupControlID="pnlChat"  BackgroundCssClass="modalBackground"  CancelControlID="btnSendCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlChat" runat="server" Width="60%" Height="60%" style="display:none;" >

<table  style=" background-color:White;border:1px solid gray; width:100%;height:100%;">

    <tr>
        <td colspan="2" align="center" style=" background-color:#3060ad; color:#FFFFFF;border:1px solid gray;" >
              <div class="row"  >
                        <div class="col-sm-12" align="center">
                        &nbsp;&nbsp;<asp:Label ID="lblHomeInfo1" runat="server" Font-Bold="True" Text="-:: Conversation Window ::-"></asp:Label> 
                         
                        </div>
                     </div>
            
        </td>
       
    </tr>
   <tr>
        <td colspan="2" align="center" >
           
            &nbsp;&nbsp;<asp:Label ID="lblInfoMsg" runat="server" Font-Bold="True" Text=""></asp:Label> 
            
        </td>
       
    </tr>
  
  
 

    <tr>
        <td align="left" colspan="2" >
        
            <asp:ListBox ID="lstChat" style="margin-left:9px;overflow-x:auto;" runat="server" Height="100%" Width="97%"></asp:ListBox>
        
       </td>
       
     
    </tr>
    
    <tr>
        <td colspan="2" align="center">
        <asp:Label ID="lblComplaintMsgID" runat="server" Text="" Visible="false"></asp:Label> 
        <asp:Label ID="lblMesaageTo" runat="server" Text="" Visible="false"></asp:Label> 
            
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSendCancel" runat="server" Text="Close" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
     

</table>

    </asp:Panel>





</asp:Content>
