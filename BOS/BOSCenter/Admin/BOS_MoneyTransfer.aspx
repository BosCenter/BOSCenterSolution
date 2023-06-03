<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_MoneyTransfer.aspx.vb" Inherits="BOSCenter.BOS_MoneyTransfer" %>
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
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style='margin-top:15px;'></div>

<br />
<asp:UpdatePanel runat='server' ID='updatepanel21'>
<ContentTemplate>
<div class='container'>
<div class='col-sm-10 col-sm-offset-1'>
<div class='log_form_head1'>
<asp:Label ID='formheading3' runat='server' Text='Money Transfer'></asp:Label>
</div>
<div class='log_form1'>
<div class="row" runat="server" id="Div2">

 
 <div class="col-sm-12">
 
  <div class="form-section" runat="server" id="div_gateway"> 
   <center>  
   
    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Gateway<asp:Label ID="Label20" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlGateway" CssClass="form-control" runat="server" AutoPostBack="true">
                         <asp:ListItem Value="MoneyTransferAPI">MoneyTransferAPI</asp:ListItem>
                            <asp:ListItem Value="MoneyTransferAPI-2">MoneyTransferAPI-2</asp:ListItem>
                            </asp:DropDownList> 
                   
                        </div>
                      </div>
      <div class="clearfix" style="margin-bottom:10px;"></div>
     </center>
    <%-- <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">
                            

                        </div>
                      </div>
                        <div class="clearfix" style="margin-bottom:10px;"></div>--%>
</div>
 </div>
 </div>
<div class='row' runat="server" id="Div_SearchCustomer">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='lblformsectionhead3' runat='server' Text='Search Customer For Mobile No'></asp:Label>
</div>
   
 <div class="row mar_top10">
<div class="col-md-12">
<asp:Label ID="lblError_Gateway" ClientIDMode="Static" runat="server"  ></asp:Label>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter Mobile NO</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtEnterMobileNo' runat='server' placeholder=' Enter Mobile NO' MaxLength="10" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtEnterMobileNo">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
    <asp:Button ID="btnSearchCustomerGo" CssClass="btn btn-primary" runat="server" Text="Go" />&nbsp;<asp:Button ID="btnChangeNo" CssClass="btn btn-warning" runat="server" Text="Change Number" />
    &nbsp;&nbsp;
</div>
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
<div runat="server" id="Div_MoneyTransferAPI_1">
<div class='row' runat="server" id="Div_AddCustomer" visible="false">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label4' runat='server'  Text='Add Customer'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Mobile No</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">

<asp:TextBox ID='txtMobileNo' runat='server' placeholder=' Enter Mobile No' ReadOnly="true" MaxLength="10" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtMobileNo">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Customer Name</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">

<asp:TextBox ID='txtCustomerName'  runat='server' placeholder='Enter Full Name' class='form-control'></asp:TextBox>
</div>
</div>
</div>

<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter OTP</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">

    <asp:TextBox ID="TextBox1" runat="server"  placeholder='Enter OTP' class='form-control'></asp:TextBox>
</div>
</div>
</div>


<div class="row">
<div class="col-md-4">
 <asp:Label ID="lblAddCustomerError" runat="server" Text=""></asp:Label>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  &nbsp;</label>
    <asp:Button ID="btnAddCustomerGo" CssClass="btn btn-primary" runat="server" Text="Go" />
</div>
</div>





</div>
</div>
</div>
</div> 
<div class='row' runat="server" id="Div_CustomerDetails" visible="false">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label6' runat='server' Text='Customer Details'></asp:Label>
</div>

<div class="row mar_top10">

<div class="col-md-12">
<div class="form-group">
<div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="Divgrid">
<asp:GridView ID="gdvCustomerDetails" runat="server"  cssclass="grid-view-themeclass" PageSize="500"
                            BorderStyle="None" AllowPaging="True">
            
                          <Columns>
               
                            
                          </Columns>
                         
                      </asp:GridView></div>
                            </div></div> 
</div>
</div>
</div>


</div>
</div>
</div>
<div class='row' runat="server" id="Div_VerifyOTP" visible="false">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label5' runat='server' Text='Verify OTP'></asp:Label>
</div>

<div class="row mar_top10">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter OTP</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">

<asp:TextBox ID='txtEnterOTP' runat='server' class='form-control'></asp:TextBox>
</div>
</div>
</div>
<div class="row mar_top10">
<div class="col-md-12">
 <asp:Label ID="lblVerifyOTPError" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblstateresp" runat="server" Text="" Visible="false" ></asp:Label>
    
</div>






</div>
<div class="row mar_top10">
<div class="col-md-4">
&nbsp;
</div>
<div class="col-md-4">
<div class="form-group">

    <asp:Button ID="btnVerifyOTPNo" runat="server"  Text="Verify" CssClass="btn btn-primary"  />&nbsp;
      <asp:Button ID="btnResendOTP" runat="server"  Text="Resend OTP" CssClass="btn btn-primary"  />
</div>
</div>





</div>
</div>
</div>
</div>
<div class='row' runat="server" id="Div_RecepientDetails" visible="false">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label7' runat='server' Text='Recepient Details'></asp:Label>
</div>
<div class="row mar_top10">

<div class="col-md-12">
<div class="form-group">
    <asp:Label ID="lblError" runat="server" CssClass="mar_lft30" ForeColor="Red" Font-Bold="true" Text="Oops! Recepient Not Found,Please Add Recepient For Transfer."></asp:Label>
</div>
</div>
</div>



<div class="row">

<div class="col-md-12">
<div class="form-group">
<div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="Div1">
<asp:GridView ID="grdAddRecepient" runat="server"  cssclass="grid-view-themeclass" PageSize="5000"
                            BorderStyle="None" AllowPaging="True">
            
                          <Columns>
            
                             <asp:TemplateField ShowHeader="False" HeaderText="Transfer">
                                  <ItemTemplate>
                                    
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top5"   OnClick="btnGrdRowTransfer_Click"
                                          CommandName="Select" Text=""><i class="fa fa-arrow-right"></i></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                                  <ItemTemplate>
                                    
                                          <asp:LinkButton ID="lnkbtnGridDelete" runat="server" CausesValidation="False" CssClass="btn btn-danger mar_top5"   OnClick="btnGrdRowDelete_Click"
                                          CommandName="Select" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                         
                      </asp:GridView></div> </div> </div> 
</div>
</div>
</div>
<div class="row">

<div class="col-md-4">
<div class="form-group">
  &nbsp;<asp:Label ID="lblIFSCCode" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblReceipentName" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblRBankName" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblRAccountNo" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblReceipentId" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblReceipentMobileNo" runat="server" Visible="false" Text=""></asp:Label>
&nbsp;<asp:Label ID="lblBank_Limit_1" runat="server" Visible="false" Text=""></asp:Label>
&nbsp;<asp:Label ID="lblBank_Limit_2" runat="server" Visible="false" Text=""></asp:Label>
&nbsp;<asp:Label ID="lblBank_Limit_3" runat="server" Visible="false" Text=""></asp:Label>      

</div>
</div>
<div class="col-md-4">
<div class="form-group">
  &nbsp;
</div>
</div>
<div class="col-md-4">
<div class="form-group">
  <asp:Button ID="btnAddRecepient" runat="server" CssClass="btn btn-primary pull-right" Text="Add Recepient" />
</div>
</div>
</div>

</div>
</div>
</div>
<div class='row' runat="server" id="Div_AddRecepient" visible="false">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label9' runat='server'  Text='Add Recepient'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Select Bank<asp:Label ID="Label13" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
    <asp:DropDownList ID="ddlSelectBank" runat="server"  class='form-control' AutoPostBack="True"> 
    </asp:DropDownList>

</div>
</div>
</div>
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Beneficiary Name<asp:Label ID="Label12" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtRecepientMobileNo'  runat='server' MaxLength="30" class='form-control'></asp:TextBox>
 


</div>
</div>

</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  IFSC Code<asp:Label ID="Label11" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtIFSCCode'  runat='server' class='form-control'></asp:TextBox>
</div>
</div>

</div>
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Bank Account No<asp:Label ID="Label8" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtBankAccountNo'  runat='server' class='form-control'></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-md-12">
<asp:Label ID="lblRecepientError" runat="server" Text=""></asp:Label><asp:Label ID="lblRecepientActualName" runat="server" Text="" Visible="false"></asp:Label> 
    <asp:Label ID="lbl_Beneficiary_temp_id" runat="server" Visible="False"></asp:Label>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  &nbsp;</label>
    <asp:Button ID="btnAddRecepientGo" CssClass="btn btn-primary" runat="server" Text="Go" />&nbsp;
    <asp:Button ID="bntReceipientClose" CssClass="btn btn-primary" runat="server" Text="Close" />
</div>
</div>





</div>
</div>
</div>
</div>
<div class='row' runat="server" id="Div_TransferAmt" visible="false">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label10' runat='server'  Text='Transfer Amount'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter Amount  <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>

</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtEnterAmt'  MaxLength="7" runat='server' class='form-control' AutoPostBack="True"></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtEnterAmt">
                            </asp:FilteredTextBoxExtender>
<asp:Label ID="lblCaculatedAmt" runat="server"  visible="false" ></asp:Label>
<asp:Label ID="lblBankID" runat="server"  visible="false" ></asp:Label>

</div>
</div>

</div>



<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Service Charge  </label>

</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtServiceCharge' ReadOnly="true"  runat='server' class='form-control'></asp:TextBox>


</div>
</div>
  <div class="col-sm-2">
                          <div class="clearfix" style="margin-bottom:7px;"></div>
                             <asp:Label ID="lblService" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                     </div>
</div>


<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>   Net Amount  </label>

</div>
</div>
<div class="col-md-4">
<div class="form-group">
  <asp:TextBox ID="txtNetAmount" ReadOnly="true" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>&nbsp;

</div>
</div>

</div>




<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Transfer Mode  <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label> </label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
    <asp:DropDownList ID="ddlTransferMode" runat="server" class='form-control'>
    <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
    <asp:ListItem Value="IMPS">IMPS</asp:ListItem>
    </asp:DropDownList>

</div>
</div>

</div>


<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter Transaction PIN <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label> </label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
        <asp:TextBox ID="txtTransactionPin" cssclass="form-control" MaxLength="4" TextMode ="Password"  runat="server"></asp:TextBox>

</div>
</div>

</div>


<div class="row">
<div class="col-md-12">
<asp:Label ID="lblTranferAmtError" runat="server" Text=""></asp:Label>
<asp:Label ID="lblWalletBal" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblRID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblTransId" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblAgentID" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblAgentType" runat="server" Visible="False"></asp:Label>
</div>
</div>








<div class="row">
<div class="col-md-4">
&nbsp;
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  &nbsp;</label>
    <asp:Button ID="btnTranferAmt" CssClass="btn btn-primary" runat="server" Text="Go" />&nbsp;
    <asp:Button ID="btntransferClose" CssClass="btn btn-primary" runat="server" Text="Close" />
</div>
</div>





</div>
</div>
</div>
</div>
</div>

<div runat="server" id="Div_MoneyTransferAPI_2">
<div class='row' runat="server" id="Div_AddCustomer_2" visible="true">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label23' runat='server'  Text='Add Customer'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Mobile No</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">

<asp:TextBox ID='txtMobileNo_2' runat='server' placeholder=' Enter Mobile No' ReadOnly="true" MaxLength="10" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtMobileNo_2">
                            </asp:FilteredTextBoxExtender>
</div>
</div>
</div>
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Customer Name</label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">

<asp:TextBox ID='txtCustomerName_2'  runat='server' placeholder=' Enter Customer Name' class='form-control'></asp:TextBox>
</div>
</div>




</div>
<div class="row">
<div class="col-md-4">
 <asp:Label ID="lblAddCustomerError_2" runat="server" Text=""></asp:Label>
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  &nbsp;</label>
    <asp:Button ID="btnAddCustomerGo_2" CssClass="btn btn-primary" runat="server" Text="Go" />
</div>
</div>





</div>
</div>
</div>
</div> 
<div class='row' runat="server" id="Div_CustomerDetails_2" visible="true">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label26' runat='server' Text='Customer Details'></asp:Label>
</div>

<div class="row mar_top10">

<div class="col-md-12">
<div class="form-group">
<div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="Div6">
<asp:GridView ID="gdvCustomerDetails_2" runat="server"  cssclass="grid-view-themeclass" PageSize="500"
                            BorderStyle="None" AllowPaging="True">
            
                          <Columns>
            
                            
                          </Columns>
                         
                      </asp:GridView></div>
                            </div></div> 
</div>
</div>
</div>


</div>
</div>
</div>

<div class='row' runat="server" id="Div_RecepientDetails_2" visible="true">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label29' runat='server' Text='Recepient Details'></asp:Label>
</div>
<div class="row mar_top10">

<div class="col-md-12">
<div class="form-group">
    <asp:Label ID="lblError_2" runat="server" CssClass="mar_lft30" ForeColor="Red" Font-Bold="true" Text="Oops! Recepient Not Found,Please Add Recepient For Transfer."></asp:Label>
</div>
</div>
</div>



<div class="row">

<div class="col-md-12">
<div class="form-group">
<div class="table-responsive">
                    <div class="table_wid">
                    <div runat="server" id="Div9">
<asp:GridView ID="grdAddRecepient_2" runat="server"  cssclass="grid-view-themeclass" PageSize="5000"
                            BorderStyle="None" AllowPaging="True">
            
                          <Columns>
            
                             <asp:TemplateField ShowHeader="False" HeaderText="Transfer">
                                  <ItemTemplate>
                                    
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-primary mar_top5" OnClick="btnGrdRowTransfer_2_Click"
                                          CommandName="Select" Text=""><i class="fa fa-arrow-right"></i></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                         
                      </asp:GridView></div> </div> </div> 
</div>
</div>
</div>
<div class="row">

<div class="col-md-4">
<div class="form-group">
  &nbsp;<asp:Label ID="lblIFSCCode_2" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblReceipentName_2" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblRBankName_2" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblRAccountNo_2" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblReceipentId_2" runat="server" Visible="false" Text=""></asp:Label>
  &nbsp;<asp:Label ID="lblReceipentMobileNo_2" runat="server" Visible="false" Text=""></asp:Label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
  &nbsp;
</div>
</div>
<div class="col-md-4">
<div class="form-group">
  <asp:Button ID="btnAddRecepient_2" runat="server" CssClass="btn btn-primary pull-right" Text="Add Recepient" />
</div>
</div>
</div>

</div>
</div>
</div>

<div class='row' runat="server" id="Div_AddRecepient_2" visible="true">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label37' runat='server'  Text='Add Recepient'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Select Bank<asp:Label ID="Label38" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
    <asp:DropDownList ID="ddlSelectBank_2" runat="server"  class='form-control'> 
    </asp:DropDownList>

</div>
</div>
</div>
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter Name<asp:Label ID="Label25" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtName_2'  runat='server' class='form-control'></asp:TextBox>
</div>
</div>

</div>
<%--<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Mobile No<asp:Label ID="Label39" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtRecepientMobileNo_2'  runat='server' MaxLength="10" class='form-control'></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtRecepientMobileNo_2">
                            </asp:FilteredTextBoxExtender>


</div>
</div>

</div>--%>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  IFSC Code<asp:Label ID="Label40" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtIFSCCode_2'  runat='server' class='form-control'></asp:TextBox>
</div>
</div>

</div>
<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Bank Account No<asp:Label ID="Label41" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtBankAccountNo_2'  runat='server' class='form-control'></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-md-12">
<asp:Label ID="lblRecepientError_2" runat="server" Text=""></asp:Label><asp:Label ID="lblRecepientActualName_2" runat="server" Text="" Visible="false"></asp:Label>
</div>
</div>
<div class="row">
<div class="col-md-4">
&nbsp;
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  &nbsp;</label>
    <asp:Button ID="btnAddRecepientGo_2" CssClass="btn btn-primary" runat="server" Text="Go" />&nbsp;
    <asp:Button ID="bntReceipientClose_2" CssClass="btn btn-primary" runat="server" Text="Close" />
</div>
</div>





</div>
</div>
</div>
</div>

<div class='row' runat="server" id="Div_TransferAmt_2" visible="true">
<div class='col-md-12'>
<div class='form-section'>
<div class='form-section-head'>
<asp:Label ID='Label44' runat='server'  Text='Transfer Amount'></asp:Label>
</div>
<div style='margin-bottom:10px;'>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter Amount  <asp:Label ID="Label45" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label></label>

</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtEnterAmt_2'  MaxLength="7" runat='server' class='form-control' AutoPostBack="True"></asp:TextBox>
 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtEnterAmt_2">
                            </asp:FilteredTextBoxExtender>

</div>
</div>

</div>



<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Service Charge  </label>

</div>
</div>
<div class="col-md-4">
<div class="form-group">
<asp:TextBox ID='txtServiceCharge_2' ReadOnly="true"  runat='server' class='form-control'></asp:TextBox>


</div>
</div>
  <div class="col-sm-2">
                          <div class="clearfix" style="margin-bottom:7px;"></div>
                             <asp:Label ID="lblService_2" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                     </div>
</div>


<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>   Net Amount  </label>

</div>
</div>
<div class="col-md-4">
<div class="form-group">
  <asp:TextBox ID="txtNetAmount_2" ReadOnly="true" cssclass="form-control inputtext" runat="server"  ClientIDMode="Static" ></asp:TextBox>&nbsp;

</div>
</div>

</div>



<div class="row">
<div class="col-md-4">
<div class="form-group">
<label for='txtAgentType'>  Enter Transaction PIN <asp:Label ID="Label182" runat="server" ForeColor="Red" Text="&nbsp;*"></asp:Label> </label>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
        <asp:TextBox ID="txtTransactionPin_2" cssclass="form-control" MaxLength="4" TextMode ="Password"  runat="server"></asp:TextBox>

</div>
</div>

</div>


<div class="row">
<div class="col-md-12">
<asp:Label ID="lblTranferAmtError_2" runat="server" Text=""></asp:Label>
<asp:Label ID="lblWalletBal_2" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblRID_2" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblTransId_2" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblAgentID_2" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblAgentType_2" runat="server" Visible="False"></asp:Label>
</div>
</div>








<div class="row">
<div class="col-md-4">
&nbsp;
</div>
<div class="col-md-4">
<div class="form-group">
<label for='txtRegistrationId'>  &nbsp;</label>
    <asp:Button ID="btnTranferAmt_2" CssClass="btn btn-primary" runat="server" Text="Go" />&nbsp;
    <asp:Button ID="btntransferClose_2" CssClass="btn btn-primary" runat="server" Text="Close" />
</div>
</div>





</div>
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
             <asp:Label ID="l" runat="server" CssClass="mar_lft30" Text="Bank Name : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblpopBankName" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label22" runat="server" CssClass="mar_lft30" Text="AccountNo : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopAccountNo" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label19" runat="server" CssClass="mar_lft30" Text="TransferAmt : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopTransferAmt" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label24" runat="server" CssClass="mar_lft30" Text="ServiceCharge : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopServiceCharge" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
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
