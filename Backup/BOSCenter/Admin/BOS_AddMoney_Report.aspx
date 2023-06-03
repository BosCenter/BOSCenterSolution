<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Admin/SuperAdmin.Master"   CodeBehind="BOS_AddMoney_Report.aspx.vb" Inherits="BOSCenter.BOS_AddMoney_Report" %>
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


 <div class="container" style="width:95%;">

 
	<div class="row">

    
    	<div class="col-sm-12">
        <div class="row">
            	<div class="col-sm-12">
                	<div class="log_form_head1">
                    	&nbsp;Add Money Details
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

                   
              <div class="col-md-6">
              <label class="col-sm-2"></label>
              <div class="col-sm-4 pull-right">
              <asp:DropDownList ID="ddlNoOfRecords" runat="server" 
                      cssclass="form-control inputtext" AutoPostBack="True" Visible="False" >
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
                            BorderStyle="None" AutoGenerateColumns="true" PageSize="1000000">
                         
                          
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
