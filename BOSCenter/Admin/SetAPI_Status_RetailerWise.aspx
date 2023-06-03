<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Admin/SuperAdmin.Master"   CodeBehind="SetAPI_Status_RetailerWise.aspx.vb" Inherits="BOSCenter.SetAPI_Status_RetailerWise" %>
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


 <div class="container" style="width:90%;">

 
	<div class="row">

    
    	<div class="col-sm-12 ">
        <div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	&nbsp;:: Set Retailer / Customer Wise API Status ::
                    </div>
                </div>
            </div>
        

        

<div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                   <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/excel_32X32.png"/>
                      <asp:ImageButton ID="Imagebtnword" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/word_32X32.png"/>
                      <asp:ImageButton ID="ImagebtnPdf" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/pdf_32X32.png" Visible="False"/>
                    </ContentTemplate> 
                 <Triggers>
                 <asp:PostBackTrigger ControlID="ImagebtnExcel" />
                 <asp:PostBackTrigger ControlID="Imagebtnword" />
                 <asp:PostBackTrigger ControlID="ImagebtnPdf" />
                 </Triggers>

                 </asp:UpdatePanel> 
                 </div>
<%--
                  <div class="col-md-6">
                  <label></label>
                  </div>--%>
                   
              <div class="col-md-6">
              <label class="col-sm-2"></label>
              <div class="col-sm-4 pull-right">
              <asp:DropDownList ID="ddlNoOfRecords" runat="server" 
                      cssclass="form-control inputtext" Visible="false"  AutoPostBack="True" >
                  <asp:ListItem>10 Record(s)</asp:ListItem>
                  <asp:ListItem>25 Record(s)</asp:ListItem>
                  <asp:ListItem>50 Record(s)</asp:ListItem>
                  <asp:ListItem>100 Record(s)</asp:ListItem>
                  <asp:ListItem>200 Record(s)</asp:ListItem>
                  <asp:ListItem>500 Record(s)</asp:ListItem>
                 </asp:DropDownList>
                  </div>
                          
                          <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
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
                            BorderStyle="None" AllowPaging="false" PageSize="1000000"  AutoGenerateColumns="false"  >
                         
                          <Columns>
                 
                                                      

                             <asp:BoundField HeaderText="SrNo" DataField="SrNo" />
                             <asp:BoundField HeaderText="Registration_Date" DataField="Registration_Date" />
                             <asp:BoundField HeaderText="RegistrationId" DataField="RegistrationId" />
                             <asp:BoundField HeaderText="Name" DataField="Name" />
                             <asp:BoundField HeaderText="PanCard" DataField="PanCard" />                               

                             <asp:BoundField HeaderText="AgencyName" DataField="AgencyName" />
                             <asp:BoundField HeaderText="MobileNo" DataField="MobileNo" /> 
                            
                            <asp:TemplateField ShowHeader="true" HeaderText="RechargeAPI">
                                  <ItemTemplate>    
                                  <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>                                                                     
                                        <asp:LinkButton ID="lnk_RechargeAPI" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_RechargeAPI_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("RechargeAPI") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField ShowHeader="true" HeaderText="RechargeAPI2">
                                  <ItemTemplate>    
                                  
                                        <asp:LinkButton ID="lnk_RechargeAPI_2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_RechargeAPI_2_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("RechargeAPI_2") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField ShowHeader="true" HeaderText="MoneyTransferAPI">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_MoneyTransferAPI" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10 " OnClick="lnk_MoneyTransferAPI_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("MoneyTransferAPI") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField ShowHeader="true" HeaderText="MoneyTransferAPI2">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_MoneyTransferAPI_2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10 " OnClick="lnk_MoneyTransferAPI_2_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("MoneyTransferAPI_2") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField ShowHeader="true" HeaderText="PANCardAPI">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_PANCardAPI" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_PANCardAPI_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("PANCardAPI") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                             <asp:TemplateField ShowHeader="true" HeaderText="AEPS_API">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_AEPS_API" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_AEPS_API_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("AEPS_API") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="true" HeaderText="Payin_API">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_Payin_API" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_Payin_API_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("Payin_API") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" HeaderText="Payin_API2">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_Payin_API_2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_Payin_API_2_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("Payin_API2") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" HeaderText="Payout_API">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_Payout_API" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_Payout_API_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("Payout_API") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                      <asp:TemplateField ShowHeader="true" HeaderText="Payout_API2">
                                  <ItemTemplate>                                                                         
                                        <asp:LinkButton ID="lnk_Payout_API_2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top10" OnClick="lnk_Payout_API_2_GridView_Click"
                                          CommandName="Select" Text='<%# Eval("Payout_API2") %>'></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>


                              <asp:TemplateField ShowHeader="true" HeaderText="HoldAmt">
                                  <ItemTemplate>  
                                      <asp:TextBox ID="txtHoldAmt" runat="server" Text='<%# Eval("HoldAmt") %>' Width="80px" CssClass="form-control mar_top10" AutoPostBack="true" MaxLength="6"   OnTextChanged="txt_HoldAmt_GridView_TextChanged"></asp:TextBox>   
                                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" Enabled="True" FilterType="Custom"  ValidChars="0123456789" 
                                TargetControlID="txtHoldAmt">
                            </asp:FilteredTextBoxExtender>                                                               
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField ShowHeader="true" HeaderText="HoldRemarks">
                                  <ItemTemplate>  
                                      <asp:TextBox ID="txtHoldAmtRemarks" runat="server" Text='<%# Eval("HoldRemarks") %>' Width="250px" TextMode="MultiLine" Rows="3" CssClass="form-control mar_top5" AutoPostBack="true"   OnTextChanged="txt_HoldAmtRemarks_GridView_TextChanged"></asp:TextBox>                                                                  
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
    <asp:Panel ID="DeletePopup" runat="server" Width="450px" style="display:none;" >
    
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
    
    <tr style="margin-top:10px">
        <td align="right">
          <asp:Label ID="Label1" runat="server" Text="UserID" ></asp:Label> 
           &nbsp;&nbsp; </td>    
        
          <td align="left">
            <asp:TextBox ID="txtUserId_popup" runat="server" style="margin-top:10px" ReadOnly="true" Width="100px" ></asp:TextBox>
              <asp:Label ID="lblUserId" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>
          </td>
     
    </tr>

        <tr >

        <td align="right" style="width:180px">

          <asp:Label ID="lblfromtime" runat="server" Text="RechargeAPI"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
           
             <asp:DropDownList ID="ddlRechargeAPI_Status" Width="60%" style="margin-top:10px"   runat="server" >
               <asp:ListItem>Active</asp:ListItem>
               <asp:ListItem>Inactive</asp:ListItem>
           </asp:DropDownList>
       &nbsp;</td>
      
    </tr>

     <tr >

        <td align="right" style="width:180px">

          <asp:Label ID="Label6" runat="server" Text="RechargeAPI-2"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
           
             <asp:DropDownList ID="ddlRechargeAPI_2_Status" Width="60%" style="margin-top:10px"   runat="server" >
               <asp:ListItem>Active</asp:ListItem>
               <asp:ListItem>Inactive</asp:ListItem>
           </asp:DropDownList>
       &nbsp;</td>
      
    </tr>
    <tr>
        <td align="right">
          <asp:Label ID="lblTOtime" runat="server"  Text="MoneyTransferAPI"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
          
            <asp:DropDownList ID="ddlMoneyTransferAPI_Status" style="margin-top:4px;width:60%"  runat="server">
               <asp:ListItem>Active</asp:ListItem>
               <asp:ListItem>Inactive</asp:ListItem>
           </asp:DropDownList>
       &nbsp;</td>
     
    </tr>
       <tr>
        <td align="right">
          <asp:Label ID="Label2" runat="server" Text="PANCardAPI"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
          
            <asp:DropDownList ID="ddlPANCardAPI_Status"  Width="60%"  runat="server">
               <asp:ListItem>Active</asp:ListItem>
               <asp:ListItem>Inactive</asp:ListItem>
           </asp:DropDownList>
       &nbsp;</td>
     
    </tr>
    <tr>
        <td align="right">
          <asp:Label ID="Label3" runat="server" Text="AEPSAPI"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
          
            <asp:DropDownList ID="ddlAEPSAPI_Status"  Width="60%"  runat="server">
               <asp:ListItem>Active</asp:ListItem>
               <asp:ListItem>Inactive</asp:ListItem>
           </asp:DropDownList>
       &nbsp;</td>
     
    </tr>
    <tr>
        <td align="right">
          <asp:Label ID="Label4" runat="server" Text="HoldAmt"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
          
           <asp:TextBox ID="txtHoldAmt" runat="server"   
               Width="150px" MaxLength="7" ></asp:TextBox>
           <asp:FilteredTextBoxExtender ID="txtHoldAmt_FilteredTextBoxExtender" 
               runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtHoldAmt">
           </asp:FilteredTextBoxExtender>
       &nbsp;</td>
     
    </tr>
        <tr>
        <td align="right">
          <asp:Label ID="Label5" runat="server" Text="Remarks"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
                     <asp:TextBox ID="txtHoldRemarks" runat="server"   
               Width="170px" MaxLength="0" ></asp:TextBox>
           
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




</asp:Content>
