<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master"    CodeBehind="LoginLogOffMaster.aspx.vb" Inherits="BOSCenter.LoginLogOffMaster" %>
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
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

 <div class="container" style="width:99%;">

 
	<div class="row">

    
    	<div class="col-sm-12">
        <div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	&nbsp;Set Active/LogOff Employees 
                    </div>
                </div>
            </div>
        

        

<div class="row">
<div class="col-sm-12 ExportPanel">
                
                 <div class="col-md-6">
                   <asp:UpdatePanel ID="UpdatePanel2"  runat="server">
        <ContentTemplate>
                  <asp:ImageButton ID="ImagebtnExcel" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/excel_32X32.png"/>
                      <asp:ImageButton ID="Imagebtnword" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/word_32X32.png"/>
                      <asp:ImageButton ID="ImagebtnPdf" runat="server" width="32px" Height="32px" 
                          ImageUrl="~/images/pdf_32X32.png"/>
                  </ContentTemplate> 
                 <Triggers>
                 <asp:PostBackTrigger ControlID="ImagebtnExcel" />
                 <asp:PostBackTrigger ControlID="Imagebtnword" />
                 <asp:PostBackTrigger ControlID="ImagebtnPdf" />
                 </Triggers>

                 </asp:UpdatePanel> 

                  <asp:Label ID="lblError" ClientIDMode="Static" runat="server"  ></asp:Label>
                 </div>
<%--
                  <div class="col-md-6">
                  <label></label>
                  </div>--%>
                   
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
                          
                          <asp:Label ID="lblNoRecords" runat="server" ></asp:Label>
                          <asp:Label ID="lblExportQry" visible="false" runat="server" ></asp:Label>
                          
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
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                          <Columns>
                   <%--           <asp:TemplateField HeaderText="SrNo">
            <ItemTemplate>
                <%# Container.DataItemIndex  + 1%>
            </ItemTemplate>
        </asp:TemplateField>
                              <asp:BoundField HeaderText="RID" DataField="RID" />--%>
                              
                            
                              
                            
                             <asp:TemplateField ShowHeader="true" >
                              <HeaderTemplate>
             <asp:Button ID="frmLogoffAll"  CommandName="btnfrmLogoffAll"  Text="Log off ALL" runat="server" Font-Bold="true"   CssClass="btn btn-primary btn-md mb3" style="margin-top:8px;" /><br>
            </HeaderTemplate>
                                  <ItemTemplate>
                                 
                                      <%--<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="DeleteRow_click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>--%>
                                      <%--<asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false" ></asp:Label>--%>
                                        
                                      
                                        <asp:LinkButton ID="btnLogOff" runat="server" CausesValidation="False" CssClass="btn btn-primary" style="margin-top:5px;" OnClick="GridView1_SelectedIndexChanged"
                                          CommandName="Select" Text="Log Off"></asp:LinkButton>
                                  </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                              

                            
                              <asp:BoundField DataField="RID" HeaderText="SrNo" />
                              

                            
                              <asp:BoundField DataField="User_ID" HeaderText="Login Id" />
                              <asp:BoundField DataField="User_Name" HeaderText="Employee Name" />
                              <asp:BoundField DataField="AccountStatus" HeaderText="Account Status" />
                              

                            
                          </Columns>
                              <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="grid-pagin" />
               
                      </asp:GridView>

                        </div>
                      </div>
                    </div>
                      
          </div> 
    

     </ContentTemplate>
        </asp:UpdatePanel>



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
          <asp:Label ID="Label1" runat="server" Text="UserID" ></asp:Label> 
           &nbsp;&nbsp; </td>    
        
          <td align="left">
            <asp:TextBox ID="txtUserId_popup" runat="server" ReadOnly="true" Width="100px" ></asp:TextBox>
              <asp:Label ID="lblUserId" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>
          </td>
     
    </tr>

    <tr>
        <td colspan="2" align="center">
            &nbsp;</td>
    </tr>

    <tr>
        <td align="right">
          <asp:Label ID="lblfromtime" runat="server" Text="From Time"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
           <asp:TextBox ID="txtFromTime" runat="server" Width="60px" ></asp:TextBox>
           <asp:MaskedEditExtender ID="txtFromTime_MaskedEditExtender" 
                runat="server" ClearTextOnInvalid="True" CultureAMPMPlaceholder="" 
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" 
                TargetControlID="txtFromTime" UserTimeFormat="TwentyFourHour">
            </asp:MaskedEditExtender>
             <asp:DropDownList ID="ddlfromAm_PM" runat="server" >
               <asp:ListItem>AM</asp:ListItem>
               <asp:ListItem>PM</asp:ListItem>
           </asp:DropDownList>
       &nbsp;</td>
      
    </tr>

      <tr>
        <td colspan="2" align="center">
            &nbsp;</td>
    </tr>


    <tr>
        <td align="right">
          <asp:Label ID="lblTOtime" runat="server" Text="To Time"></asp:Label> 
       &nbsp;&nbsp;</td>
       <td align="left">
           <asp:TextBox ID="txtTotime" runat="server" Width="60px" ></asp:TextBox>
          <asp:MaskedEditExtender ID="txtToTime_MaskedEditExtender" runat="server" 
                ClearTextOnInvalid="True" CultureAMPMPlaceholder="" 
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" 
                TargetControlID="txtToTime" UserTimeFormat="TwentyFourHour">
            </asp:MaskedEditExtender>
            <asp:DropDownList ID="ddlToAm_Pm" runat="server">
               <asp:ListItem>AM</asp:ListItem>
               <asp:ListItem>PM</asp:ListItem>
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

   


</asp:Content>
