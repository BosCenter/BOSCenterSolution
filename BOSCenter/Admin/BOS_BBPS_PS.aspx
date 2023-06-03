<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_BBPS_PS.aspx.vb" Inherits="BOSCenter.BOS_BBPS_PS"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type ="text/javascript" language ="javascript">
    function printdiv(printpage) {
        var headstr = "<html><head><title></title></head><body>";
        var footstr = "</body>";
        var newstr = document.all.item(printpage).innerHTML;
        var oldstr = document.body.innerHTML;
        document.body.innerHTML = headstr + newstr + footstr;
        w = window.open("", "_blank", "k");
        w.document.write(headstr + newstr + footstr);
        w.print();
        document.body.innerHTML = oldstr;
        return false;
    }
    //window.onload = getLocation();
    //function getLocation() {
    //    if (navigator.geolocation) {
    //        navigator.geolocation.getCurrentPosition(showPosition, showError);
    //    } else {
    //        //navigator.geolocation.getCurrentPosition(showPosition, showError);
    //    }

    //}
    //function showPosition(position) {
    //    var latlondata = position.coords.latitude + "," + position.coords.longitude;
    //    var latlon = "Your Latitude Position is:=" + position.coords.latitude + "," + "Your Longitude Position is:=" + position.coords.longitude;
    //    //alert(latlon)
    //    document.getElementById("<%=txtLong.ClientID%>").value = position.coords.longitude;
    //    document.getElementById("<%=txtLat.ClientID%>").value = position.coords.latitude;
    //}
    //function showError(error) {
    //    if (error.code == 1) {
    //        alert("User denied the request for Geolocation.");
    //    }
    //    else if (err.code == 2) {
    //        alert("Location information is unavailable.");
    //    }
    //    else if (err.code == 3) {
    //        alert("The request to get user location timed out.");
    //    }
    //    else {
    //        alert("An unknown error occurred.");
    //    }
    //}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style='margin-top:15px;'></div>

<br />
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>
<div class='container'>
<div class='col-sm-10 col-sm-offset-1'>



<div class='log_form1'>




<div class="row" runat="server" id="Div_Navigation_buttons">

 <div class="col-sm-12">
 
  <div class="form-section"> 
   <center>  
   <div class="col-sm-2">
   <asp:Button ID="btnmobile" runat="server" Text="Mobile" Width="100%" cssclass="btn btn-danger mar_top10" />
   </div>
   <div class="col-sm-2">
  <asp:Button ID="btndth" runat="server" Text="DTH" Width="100%" cssclass="btn btn-danger mar_top10" /> 
   </div>
<div class="col-sm-2">
   <asp:Button ID="btnelectricity" runat="server" Width="100%" Text="Electricity" cssclass="btn btn-danger mar_top10"   />
   </div>
   <div class="col-sm-2">
   <asp:Button ID="btnpostpaid" runat="server" Width="100%" Text="PostPaid" cssclass="btn btn-danger mar_top10"  />
   </div>
   
    <div class="col-sm-2">
  <asp:Button ID="btnbroadband" runat="server" Width="100%" Text="Broadband" cssclass="btn btn-danger mar_top10"   />
   </div>
    <div class="col-sm-2">
   <asp:Button ID="btngas" runat="server" Width="100%" Text="gas" cssclass="btn btn-danger mar_top10" visible="false"  />
   </div>
   <div class="col-sm-2">
  <asp:Button ID="btnlandline" runat="server" Width="100%" Text="landline" cssclass="btn btn-danger mar_top10"   />
   </div>
    <div class="col-sm-2">
         <asp:Button ID="btnwaterbill" runat="server" Width="100%" Text="Water Bill" cssclass="btn btn-danger mar_top10"  />
    </div>
 <div class="col-sm-2">
         <asp:Button ID="btnEMI" runat="server" Width="100%" Text="EMI" cssclass="btn btn-danger mar_top10"  />
    </div>
<div class="col-sm-2">
         <asp:Button ID="btnMunicipality" runat="server" Width="100%" Text="Municipality" cssclass="btn btn-danger mar_top10"  />
    </div>
<div class="col-sm-2">
         <asp:Button ID="btnLPG" runat="server" Width="100%" Text="LPG" cssclass="btn btn-danger mar_top10"  />
    </div>
<div class="col-sm-2">
         <asp:Button ID="btnCable" runat="server" Width="100%" Text="Cable" cssclass="btn btn-danger mar_top10"  />
    </div>
<div class="col-sm-2">
         <asp:Button ID="btnInsurance" runat="server" Width="100%" Text="Insurance" cssclass="btn btn-danger mar_top10"  />
    </div>

       <div class="col-sm-2">
         <asp:Button ID="btnFastTage" runat="server" Width="100%" Text="FasTag" cssclass="btn btn-danger mar_top10"  />
    </div>

      <div class="clearfix" style="margin-bottom:10px;"></div>
     </center>
</div>
 </div>
 </div>

<div class='row' runat="server" id="Div_bbps_section" visible="false">
<div class='col-md-12'>
<div class='form-section'>
<div class="row mar_top10">

<div class="col-md-2">
<div class="form-group">
<label for='txtBankName'>Mode</label>
<asp:DropDownList ID="ddl_BBPS_Mode" CssClass="form-control" runat="server">
                        
                            </asp:DropDownList> 
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label for='txtBankName'><asp:Label ID="lbl_BBPS_Operator_Heading" runat="server" Text="Operator"></asp:Label></label>
<asp:DropDownList ID="ddl_BBPS_Operators" CssClass="form-control" runat="server">
                            </asp:DropDownList> 
<asp:Label ID="lbl_BBPS_Fetch_Response" style="margin-top:5px;" runat="server" Text=""  ></asp:Label>
</div>
</div>

<div class="col-md-2">
<div class="form-group">
<label for='txtBankName'><asp:Label ID="lbl_BBPS_Title_Heading" runat="server" Text="Enter Ca No"></asp:Label></label>
<asp:TextBox ID='txt_BBPS_CA_No' runat='server' placeholder='' MaxLength="15" class='form-control'></asp:TextBox>

 <br/>
 <asp:TextBox ID="txtLong" runat="server"  Enabled ="false"  style="display:none;" 
         cssclass="form-control"></asp:TextBox>
 <asp:TextBox ID="txtLat" runat="server"  Enabled ="false"  style="display:none;" 
         cssclass="form-control"></asp:TextBox>
</div>
</div>
<div runat="server" id="div_BBPS_Amt" visible="false" >
<div class="col-md-2">
<div class="form-group">
<label for='txtBankName'>Amount</label>
<asp:TextBox ID='txt_BBPS_Amt' runat='server' placeholder='' MaxLength="13" enabled="false" class='form-control'></asp:TextBox>
 
</div>
</div>
</div>


<div class="col-md-2">
<div class="form-group">
    <label for='txtBankName'>&nbsp;</label><br/>
<asp:Button ID="btn_BBPS_Fetch" CssClass="btn btn-primary" runat="server" Text="FETCH" />

</div>
</div>



</div>
</div>
</div>
</div>

<div class='row' runat="server" id="Div_SearchCustomer">
<div class='col-md-12'>
<div class='form-section'>
<%--<div class='form-section-head'>
<asp:Label ID='lblformsectionhead3' runat='server' Text='Mobile No'></asp:Label>
</div>--%>
   
 <div class="row mar_top10">
<div class="col-md-12">
<asp:Label ID="lblError_Gateway" ClientIDMode="Static" runat="server"  ></asp:Label>
<asp:Label ID="lblWalletBal" ClientIDMode="Static" visible="false" runat="server"  ></asp:Label>
<asp:Label ID="lblBillFetch" ClientIDMode="Static" visible="false" runat="server"  ></asp:Label>
<asp:Label ID="lblAgentID" ClientIDMode="Static" visible="false" runat="server"  ></asp:Label>
<asp:Label ID="lblAgentType" ClientIDMode="Static" visible="false" runat="server"  ></asp:Label>
<asp:Label ID="lblTransId" ClientIDMode="Static" visible="false" runat="server"  ></asp:Label>
<asp:Label ID="lblSessionFlag" ClientIDMode="Static" visible="false" runat="server"  ></asp:Label>
<asp:Label ID="lblRID" ClientIDMode="Static" visible="false" runat="server"  ></asp:Label> 

</div>
</div>

<div class="row mar_top10">

<div class="col-md-2">
<div class="form-group">
<label for='txtBankName'><asp:Label ID="lbl_Mobile_CA_No_Heading" runat="server" Text="Enter Mobile No"></asp:Label></label>
<asp:TextBox ID='txt_Mobile_CA_No' runat='server' placeholder='' MaxLength="15" autopostback="true" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" Enabled="True" FilterType="Custom,Numbers,UppercaseLetters" 
                                TargetControlID="txt_Mobile_CA_No">
                            </asp:FilteredTextBoxExtender><br/>
<asp:Label ID="lblFetchOperator" runat="server" Text=""  ></asp:Label>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label for='txtBankName'>Operator</label>
<asp:DropDownList ID="ddlOperators" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOperators_SelectedIndexChanged">
                            </asp:DropDownList> 
</div>
</div>


<div runat="server" id="div_Circle" visible="false" >
<div class="col-md-3">
<div class="form-group">
<label for='txtBankName'>Circle &nbsp; <asp:linkButton ID="btnBrowsePlan"  runat="server" Text="View Plan" /></label>
<asp:DropDownList ID="ddlCircle" CssClass="form-control" runat="server">

                            </asp:DropDownList> 
<asp:DropDownList ID="ddlGateway" CssClass="form-control" runat="server" visible="false"></asp:DropDownList> 

</div>
</div>

</div>

<div class="col-md-2">
<div class="form-group">
<label for='txtBankName'>Amount</label>
<asp:TextBox ID='txt_Recharge_Amt' runat='server' placeholder='' MaxLength="4" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender89" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_Recharge_Amt">
                            </asp:FilteredTextBoxExtender>
<asp:Label ID="lblSelectedService" runat="server" Text="" Visible="false" ></asp:Label>
</div>
</div>

    `
<div class="col-md-2">
<div class="form-group">
    <label for='txtBankName'>&nbsp;</label><br/>
<asp:Button ID="btn_Proceed_Recharge" CssClass="btn btn-primary" runat="server" Text="Proceed" />

</div>
</div>

</div>
    <div class="col-sm-12">
        <div class="row" runat="server" id="DIV_ShowCustomerDetail" visible="false" Style=" font-size:medium; font-weight:bold ;">
        <div class="form-group ">
            <div class="col-sm-12">
                <asp:TextBox ID="txt_ShowCustomerDetail" Text="" TextMode="MultiLine" Style=" font-size:medium; font-weight:bold ;" ></asp:TextBox>
            </div>
        </div>
    </div>
    </div>
    






<div class="row mar_top10">
<div class="col-md-2">
 
</div>
</div>

<div class="row" runat="server" visible="false">
<div class="col-md-4">
<div class="form-group">
&nbsp;
</div>
</div>
<div class="col-md-12">
<div class="form-group">
<label for='txtRegistrationId'>  &nbsp;</label>
    <asp:TextBox ID='txttestBox' runat='server' TextMode="MultiLine" Height="100px" class='form-control'></asp:TextBox>
</div>
</div>

</div>



<div class="row">
<div class="col-md-2">
<div class="form-group">
&nbsp;
</div>
</div>
<div class="col-md-6">
<div class="form-group">
    <asp:Label ID="lblSearchCustomerError" runat="server" Text=""  Font-Bold="true"></asp:Label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
    <asp:Button ID="btnAddNewCustomer" Visible="false" CssClass="btn btn-primary pull-right" runat="server" Text="Add Customer" />
</div>
</div>
</div>



</div>
</div>
</div>

<%-- Start - Browse Plan Section --%>
<div runat="server" id="Divgrid_plan"  visible="false">
<section>
 <div class="row">
<div class="col-sm-12">

<div class="form-section">
<div class="form-section-head">
     -:: Search Result ::-  </div>

<div style="margin-bottom:5px;"></div>


<div class="row">
<div class="col-md-12">

<div class="table-responsive">
                    <div class="table_wid">
                    <div style="overflow:scroll; ">
 <asp:GridView ID="grdResponseDetails" runat="server" cssclass="grid-view-themeclass" AutoGenerateColumns="true" ShowFooter="true"
                            BorderStyle="None" PageSize="10000">
 <Columns>                           
                     
        <asp:TemplateField HeaderText="">
        
                                  <ItemTemplate>
                                    
&nbsp;&nbsp;
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"  CssClass="btn btn-primary mar_top5"  OnClick="btnGrdRowGo_Click" 
                                          CommandName="Select" Text="Go"  ></asp:LinkButton> &nbsp;&nbsp;
                                         <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"  visible="false"  CssClass="btn btn-default" 
                                          CommandName="Select" Text=""><i class="fa fa-pencil-square-o fa-lg"></i></asp:LinkButton>
                                       
                                  </ItemTemplate>
                              </asp:TemplateField>
                           
          </Columns>   
          
          <FooterStyle Font-Size="Large" Font-Bold="true" ForeColor="blue" />
                           
                            </asp:GridView>

<asp:Label ID="lblSearchPlan" runat="server" Text=""  Font-Bold="true"></asp:Label>

                            </div>
                            </div></div> 

</div>
</div>
</div>

</div>
</div>
</section>
</div>
<%-- End - Browse Plan Section --%>
<div class="clearfix" style="margin-bottom:10px;"></div>
            <div class="col-sm-12 table_head">
         <center>
                <asp:Label ID="lbl_Service_History_Heading" runat="server" Text="Transaction History"></asp:Label> 
                        </center>
                        </div>
            <div class="row">
                 <div class="col-sm-12">
                 <div class="table-responsive">
                    <div class="table_wid">
                    
                    <div class="clearfix"></div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"  cssclass="grid-view-themeclass"
                            BorderStyle="None" AllowPaging="True">
                          <%--  <AlternatingRowStyle BackColor="#f5f5f5" />--%>
                          <Columns>
<asp:TemplateField HeaderText="">
        
                                  <ItemTemplate>
                                    
                                        <asp:LinkButton ID="lnkBtnRepeat" runat="server" CausesValidation="False"  CssClass="btn btn-primary mar_top5" OnClick="lnkBtnRepeat_Click"    
                                          CommandName="Select" Text=""><i class="fa fa-chevron-circle-right fa-lg"></i></asp:LinkButton>
                                       
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="SrNo">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1%>
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

<div style='margin-top:5px;'></div>
</div</div></div>


 <asp:Button ID="Button2" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button2" PopupControlID="DeleteDocumentPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelDeleteDocument" >
</asp:ModalPopupExtender>

<asp:Panel ID="DeleteDocumentPopup" runat="server" Width="400px" style="display:none;" >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong>::: Alert Dialog :::</strong>
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
           <asp:Label ID="lblDeleteInfo" runat="server" Text="This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"></asp:Label></td>
           <asp:Label ID="lblDeleteDocumentInfo" runat="server" Text=""></asp:Label>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnDelete_Document" runat="server" Text="Yes" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelDeleteDocument" runat="server" Text="No" Width="80px" CssClass="btn btn-primary" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>
    </asp:Panel>

           
         <asp:Button ID="Button1" runat="server" Text="Button" style="display:none;"/>
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelInfo" >
</asp:ModalPopupExtender>

<asp:Panel ID="InformationPopup" runat="server" Width="350px" style="display:none;"    >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong><asp:Label ID="lblDialogMsgInfo" runat="server" Text="" ></asp:Label></strong>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" bgcolor="Silver">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" >
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left">
        <div  id="DIV_PrintReceipt">        
            <asp:Label ID="Label16" runat="server" CssClass="mar_lft30" Text="DateTime : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopDateTime" runat="server" ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="lblInformation" runat="server" CssClass="mar_lft30" Text="AgencyName : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopAgencyName" runat="server" ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label17" runat="server" CssClass="mar_lft30" Text="TransactionId : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopTransactionID" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="lblMobileNo1" runat="server" CssClass="mar_lft30" Text="MobileNo : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblpopMobileNo" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
             <asp:Label ID="l" runat="server" CssClass="mar_lft30" Text="Operator : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblpopOperator" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label19" runat="server" CssClass="mar_lft30" Text="TransactionAmt : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopTransactionAmt" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label21" runat="server" CssClass="mar_lft30" Text="Status : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopStatus" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label>
            </div>
        </td>
       
    </tr>
  
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnCancelInfo" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" style="display:none;"/> &nbsp; <asp:Button ID="btnRedirectPage" runat="server" Text="OK" Width="120px" CssClass="btn btn-danger" />&nbsp; <asp:Button ID="btnPrintReceipt" runat="server" Text="Print Receipt" Width="120px" CssClass="btn btn-danger" />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>

       <asp:Button ID="deleteButton4" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="DeleteModalPopupExtender" runat="server" TargetControlID="deleteButton4" PopupControlID="DeletePopup1"  BackgroundCssClass="modalBackground"  CancelControlID="btnDelCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup1" runat="server" Width="350px" style="display:none;"  >

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
            <asp:Label ID="lblDelDialogMsg" runat="server" Text=""></asp:Label>  <br />
            <asp:Label ID="lblAlertDelPer" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblDel" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblDelType" runat="server" Text="" Visible="false"></asp:Label> 
            <asp:Label ID="lblRowIndex" runat="server" Text="" Visible="false"></asp:Label>  
            </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnDelOk" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"/>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary"  />
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>



    </asp:Panel>


    
    <asp:Button ID="modalPopupButton" runat="server" Text="Button" style="display:none;"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="350px" style="display:none;"  >
      <asp:UpdateProgress ID="UpdWaitImage" runat="server"  DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
                 <ProgressTemplate>
                      <div class="PopupWaiting">
                                 <asp:Image ID="imgProgress" ImageUrl="../images/ajax-loader.gif" runat="server" />
                                          </div>    
                                </ProgressTemplate>
                                </asp:UpdateProgress>   
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
             <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>

    
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"  />
            <asp:Button ID="btnok_Transfer_1" runat="server" visible="false" Text="OK" Width="80px" CssClass="btn btn-primary"  /> 
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" /> </ContentTemplate>
        </asp:UpdatePanel>
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>






</ContentTemplate>




</asp:UpdatePanel>
</asp:Content>
