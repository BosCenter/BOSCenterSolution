<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_RechargeAPI.aspx.vb" Inherits="BOSCenter.BOS_RechargeAPI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    </script>
<script type="text/javascript">
    $(document).ready(function () {

        $(".inputtext").change(function (event) {
            var obj = $(this);
            //alert(obj.val());
            
        });

});

    
</script>
<br />
<br />

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
 <div class="container">
 <div class="row">
            	<div class="col-sm-10 col-sm-offset-1">
                	<div class="log_form_head1">
                       
                        <asp:Label ID="lblbillheading" runat="server" Text=""></asp:Label>  
                    </div>
                </div>
            </div>

             <div class="row" runat="server" id="Div1">

 <div class="col-sm-1"> 
 &nbsp;
 </div>
 <div class="col-sm-10">
 
  <div class="form-section" runat="server" id="div_gateway"> 
   <center>  
   
    <div class="form-group" >
                        <label for="inputEmail3" class="col-sm-3 control-label">Gateway<asp:Label ID="Label4" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlGateway" CssClass="form-control" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="Recharge">RechargeAPI</asp:ListItem>
                            <asp:ListItem Value="Recharge-2">RechargeAPI-2</asp:ListItem>
                            </asp:DropDownList> 
                   
                        </div>
                      </div>
      <div class="clearfix" style="margin-bottom:10px;"></div>
         

     </center>
     
                <%--  <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            
                        </div>
                      </div>
                        <div class="clearfix" style="margin-bottom:10px;"></div>--%>
</div>
 </div>
 </div>




 <div class="row" runat="server" id="Div_Navigation_buttons">

 <div class="col-sm-1"> 
 &nbsp;
 </div>
 <div class="col-sm-10">
 
  <div class="form-section"> 
   <center>  
   <div class="col-sm-2">
   <asp:Button ID="btnmobile" runat="server" Text="Mobile" Width="100%" cssclass="btn btn-danger mar_top10" />
   </div>
   <div class="col-sm-2">
  <asp:Button ID="btndth" runat="server" Text="DTH" Width="100%" cssclass="btn btn-danger mar_top10" /> 
   </div>
   <div class="col-sm-2">
   <asp:Button ID="btnpostpaid" runat="server" Width="100%" Text="PostPaid" cssclass="btn btn-danger mar_top10" />
   </div>
   <div class="col-sm-2">
   <asp:Button ID="btnelectricity" runat="server" Width="100%" Text="Electricity" cssclass="btn btn-danger mar_top10" />
   </div>
    <div class="col-sm-2">
  <asp:Button ID="btnbroadband" runat="server" Width="100%" Text="Broadband" cssclass="btn btn-danger mar_top10" />
   </div>
    <div class="col-sm-2">
   <asp:Button ID="btngas" runat="server" Width="100%" Text="gas" cssclass="btn btn-danger mar_top10" />
   </div>
   <div class="col-sm-2">
  <asp:Button ID="btnlandline" runat="server" Width="100%" Text="landline" cssclass="btn btn-danger mar_top10" />
   </div>
    <div class="col-sm-2">
         <asp:Button ID="btnwaterbill" runat="server" Width="100%" Text="Waterbill" cssclass="btn btn-danger mar_top10" />
    </div>
      <div class="clearfix" style="margin-bottom:10px;"></div>
     </center>
</div>
 </div>
 </div>

  <div class="row">
            	<div class="col-sm-10 col-sm-offset-1">
                	<div class="log_form_head1">
                       
                        <asp:Label ID="lbl_Service_Heading" runat="server" Text=""></asp:Label>  
                    </div>
                </div>
            </div>

	<div class="row">
    	<div class="col-sm-10 col-sm-offset-1">
        
        	
            
                      <div class="clearfix" style="margin-bottom:10px;"></div>
        	<div class="log_form1">
            	
                    <form class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Operator<asp:Label ID="Label3" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlOperators" CssClass="form-control" runat="server">
                            </asp:DropDownList> 
                   
                        </div>
                      </div>
                   
          
                      <div class="clearfix" style="margin-bottom:10px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">
                           <asp:Label ID="lbl_Mobile_CA_No_Heading" runat="server" Text="Mobile Recharge">
                              </asp:Label>   <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label>
                    
                        </label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_Mobile_CA_No" cssclass="form-control inputtext"  runat="server" MaxLength="15" ClientIDMode="Static" ></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
        runat="server" Enabled="True" FilterType="Numbers" 
        TargetControlID="txt_Mobile_CA_No">
    </asp:FilteredTextBoxExtender>
                        </div>
  <label for="inputEmail3" class="col-sm-2 control-label">Amount<asp:Label ID="Label2" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtAmt" cssclass="form-control inputtext" 
                                onfocusout="FillAmt()" MaxLength="15" runat="server"  ClientIDMode="Static" 
                                AutoCompleteType="None" AutoPostBack="True"  ></asp:TextBox>
                      <asp:FilteredTextBoxExtender ID="txtPhoneNo_1_FilteredTextBoxExtender" 
        runat="server" Enabled="True" FilterType="Numbers" 
        TargetControlID="txtAmt">
    </asp:FilteredTextBoxExtender>
                        </div>

                      </div>

                                             <div class="clearfix" style="margin-bottom:10px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Payable Amount</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtPayableAmt" cssclass="form-control inputtext" ReadOnly="true" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                   
                        </div>
                          <label for="inputEmail3" class="col-sm-2 control-label"> Service Charge</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtServiceCharge" ReadOnly="true" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>
                        </div>
                         <div class="col-sm-2">
                          <div class="clearfix" style="margin-bottom:7px;"></div>
                             <asp:Label ID="lblService" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                     </div>
                      </div>

                                           
                       <div class="clearfix" style="margin-bottom:10px;"></div>
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"> Net Amount</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtNetAmount" ReadOnly="true" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>&nbsp;
                            
                   
                        </div>
 <label for="inputEmail3" class="col-sm-2 control-label"> Enter Trans PIN<asp:Label ID="Label6" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-3">
                          <asp:TextBox ID="txtTransactionPin" cssclass="form-control" MaxLength="4" TextMode ="Password"  runat="server"></asp:TextBox>
                   
                        </div>

                      </div>

            
            <div class="clearfix" style="margin-bottom:10px;"></div>

                  <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                         <asp:Label ID="lblSelectedService" runat="server" Text="" Visible="false" ></asp:Label>
                            <asp:Label ID="lblRID" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_SA_Commission" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_AD_Commission" runat="server" Visible="false"></asp:Label>
                             <asp:Label ID="lblError" ClientIDMode="Static" runat="server"  ></asp:Label>
<asp:Label ID="lblTransId" runat="server" Visible="False"></asp:Label>
 <asp:Label ID="lblError_Gateway" ClientIDMode="Static" runat="server"  ></asp:Label>

                            <asp:Label ID="lblWalletBal" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblUpadate" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSessionFlag" runat="server" Visible="False"></asp:Label>
                        </div>
                      </div>




                  <div class="form-group">
                  	<div class="col-sm-12">
                    <div class="col-sm-3">
                    </div> 
                          <div class="col-sm-9">
                        
         &emsp; &emsp;  &emsp;                   <asp:Button ID="btnSave" runat="server" Text="Proceed" OnClientClick="disableSubmit()"
                             cssclass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;&nbsp;&nbsp;  
                            	
                          
                             <asp:Button ID="btnClear" runat="server" Text="Reset" 
                             cssclass="btn btn-primary mar_top10" />  
                        


                        
                          </div> 
                        
                    </div>
                  </div>

        
                  </form>
                  <div class="clearfix"></div>

                  
                
            </div>

            <div class="clearfix" style="margin-bottom:10px;"></div>
            <div class="col-sm-12 table_head">
         <center>
                <asp:Label ID="lbl_Service_History_Heading" runat="server" Text="Mobile Recharge History"></asp:Label> 
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
                              <asp:TemplateField HeaderText="SrNo">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
                             <%-- <asp:BoundField HeaderText="Date"  />
                              <asp:BoundField HeaderText="Time"  />
                              <asp:BoundField HeaderText="M.No"  />
                                <asp:BoundField HeaderText="Operator"  />
                                  <asp:BoundField HeaderText="Amount"  />
                                  <asp:BoundField HeaderText="Status"  />
                              --%>
                            
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
</div> 




        
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
           
            <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary"  />
             
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

    </ContentTemplate> </asp:UpdatePanel> 
</asp:Content>
