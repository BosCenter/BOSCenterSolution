<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_FidyPayOut_API.aspx.vb" Inherits="BOSCenter.BOS_FidyPayOut_API" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="conatiner">
        <div class="row">
            <div class="col-sm-10 col-sm-offset-1">
                <div class="form-section-head">Payout</div>
                <div class="form-section">
                    <div style="margin: 10px;"></div>
                    <div class="row">
                        <div class="col-sm-3">
                            <label>Mobile Number<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_MobileNo" runat="server" Placeholder="Enter Mobile No." CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btn_Search_Click" />
                        </div>
                    </div>
                    <div style="margin: 10px;"></div>
                    <div class="row">
                        <div class="col-sm-12">
                          <center>
                              <asp:Label id="lblError" runat="server" Text="" ></asp:Label>
                        </center>
                        </div>
                    </div>
                    <div style="margin: 10px;"></div>
                    <div class="row" runat="server" Id="DIVGrid" visible="false"  >
                        <div class="col-sm-12">
                          <div Class="table_head"  >
                        <center>
                             Customer Details
                        </center>                     
                    </div>
                   <div class="table-responsive">
                       <div class="table_wid">
                           <asp:GridView ID="gvShowDetails" runat="server" CssClass="grid-view-themeclass" BorderStyle="None" AllowPaging="true" AutoGenerateColumns="false" OnSelectedIndexChanged="gvShowDetails_SelectedIndexChanged">
                               <Columns>

                                            <asp:BoundField DataField="BID"  HeaderText="SrNo"/>
                                            <asp:BoundField DataField="AccountNumber"  HeaderText="Account Number"/>
                                            <asp:BoundField DataField="IfscCode"  HeaderText="Ifsc Code"/>
                                            <asp:BoundField DataField="AccountName"  HeaderText="Account Name"/>
                                            <asp:BoundField DataField="EmailID"  HeaderText="EmailID"/>
                                            <asp:BoundField DataField="MobileNo"  HeaderText="MobileNo"/>
                                             <asp:BoundField DataField="AccountAddress"  HeaderText="Address"/>
                                            <asp:BoundField  DataField="AccountStatus"  HeaderText="Account Status"/>
                                  
                                   <asp:TemplateField>
                                       <ItemTemplate>

                                             <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary"  OnClick="lnkbtnSelect_click"> Select</asp:LinkButton>
                                           
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
                    <div style="margin: 20px;"></div>
                </div>
                <div class="form-section">
                    <div style="margin: 5px;"></div>
                    <div class="row">
                        <div class="col-sm-6">
                            <label>Beneficiary Name<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_bene_Name" runat="server" Placeholder="Enter Beneficiary Name" CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label>Beneficiary Mobile<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_bene_Mobile" runat="server" Placeholder="Enter Beneficiary Mobile Number" CssClass="form-control "></asp:TextBox>
                        </div>
                    </div>
                    <div style="margin: 5px;"></div>
                    <div class="row">
                        <div class="col-sm-6">
                            <label>Beneficiary Account Number<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_bene_AccountNumber" runat="server" Placeholder="Enter Beneficiary Account Number" CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label>Beneficiary IFSC Code<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_bene_IFSC" runat="server" Placeholder="Enter Beneficiary IFSC Code" CssClass="form-control "></asp:TextBox>
                        </div>
                    </div>
                    <div style="margin: 5px;"></div>
                    <div class="row">
                        <div class="col-sm-6">
                            <label>Beneficiary Email<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_ben_Email" runat="server" Placeholder="Enter Beneficiary Email" CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label>Beneficiary Address<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_bene_Address" runat="server" Placeholder="Enter Beneficiary Address" CssClass="form-control "></asp:TextBox>
                        </div>
                    </div>
                     <div style="margin: 5px;"></div>
                    <div class="row" runat="server" id="DivTransacaton" visible="false" >
                        <div class="col-sm-12">
                    <div class="row" >
                        <div class="col-sm-6">
                            <label>Amount<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_Transc_Amount" runat="server" Placeholder="Enter Amount" CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label>Transaction Type<span style="color:red;">*</span></label>
                            <asp:DropDownList ID="ddl_Transc_Type" runat="server" CssClass="form-control">
                                <asp:ListItem>NEFT</asp:ListItem>
                                <asp:ListItem>IMPS</asp:ListItem>
                                <asp:ListItem>RTGS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="margin: 5px;"></div>
                    <div class="row" >
                        <div class="col-sm-6">
                            <label>Remarks<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_Transc_Remarks" runat="server" Placeholder="Enter Amount" CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                                   <label>TPin<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txt_Transc_Pin" runat="server" Placeholder="Enter Transation Pin" CssClass="form-control " TextMode="Password" ></asp:TextBox>
                        </div>
                    </div>
                            </div>
                        </div>
                    <div style="margin: 20px;"></div>
                    <div class="form-section">
                        <div class="row">
                            <cenetr>
                                <asp:Label ID="lbl_Error" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_Error1" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblRID" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblWalletBal" runat="server" Text="" Visibal="false"></asp:Label>
                            </cenetr>
                            <center>
                                <asp:Button ID="btn_bene_Save" runat="server" Text="Save" cssclass="btn btn-primary " OnClick="btn_bene_Save_Click" />
                                &nbsp &nbsp &nbsp
                              <asp:Button ID="btn_bene_clear" runat="server" Text="Clear" cssclass="btn btn-primary " OnClick="btn_bene_clear_Click" />
                            </center>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <asp:Button ID='modalPopupButton' runat='server' Text='Button' Style='display: none;' />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID='modalPopupButton' PopupControlID='DeletePopup' BackgroundCssClass='modalBackground' CancelControlID='btnCancel' >
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID='DeletePopup' runat='server' Width='350px' Style='display: none;'>
        <table style='width: 100%; background-color: White; border: 1px solid gray;'>
            <tr>
                <td align='center' bgcolor='Silver'>&nbsp;</td>
            </tr>
            <tr>
                <td align='center' bgcolor='Silver'>
                    <strong>Confirmation Dialog</strong>
                    <br />
                </td>
            </tr>
            <tr>
                <td align='center' bgcolor='Silver'>&nbsp;
                </td>
            </tr>
            <tr>
                <td align='center'>&nbsp;
                </td>
            </tr>
            <tr>
                <td align='center'>
                    <asp:Label ID='lblDialogMsg' runat='server' Text=''></asp:Label>
                </td>
            </tr>
            <tr>
                <td align='center'>&nbsp;
                </td>
            </tr>
            <tr>
                <td align='center'>
                    <asp:Button ID='btnPopupYes' runat='server' Text='OK' Width='80px' CssClass='btn btn-primary' />
                    &nbsp;&nbsp;&nbsp
                    <asp:Button ID='btnCancel' runat='server' Text='Cancel' Width='80px' CssClass='btn btn-primary' />
                </td>
            </tr>
            <tr>
                <td align='center'>&nbsp; 
                </td>
            </tr>
        </table>
    </asp:Panel>

             <asp:Button ID="Button1" runat="server" Text="Button" style="display:none;"/>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPopup"  BackgroundCssClass="modalBackground"  CancelControlID="btnCancelInfo" >
    </ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="InformationPopup" runat="server" Width="350px" style="display:none;"    >
<table style="width:100%;background-color:White;border:1px solid gray;">
<tr>
<td align="center" bgcolor="Silver"   >&nbsp;</td>
</tr>
    <tr>
        <td align="center" bgcolor="Silver">
            <strong><asp:Label ID="lblDialogMsgInfo" runat="server" Text="Transaction Status" ></asp:Label></strong>
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
            <asp:Label ID="lblInformation" runat="server" CssClass="mar_lft30" Text="TransactionId : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopTransactionId" runat="server" ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="Label17" runat="server" CssClass="mar_lft30" Text="Bank Account Number : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopbankAccount" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="lblMobileNo1" runat="server" CssClass="mar_lft30" Text="Amount: " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblpopAmount" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
            <asp:Label ID="lblbeneid" runat="server" CssClass="mar_lft30" Text="Name : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblpopName" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />
          <%--    <asp:Label ID="Label9" runat="server" CssClass="mar_lft30" Text="UTR : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopUTR" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label>--%>
            <asp:Label ID="Label19" runat="server" CssClass="mar_lft30" Text="Status : " ></asp:Label> &nbsp;&nbsp;<asp:Label ID="lblPopStatus" runat="server"  ForeColor="Blue" Font-Bold="true" ></asp:Label><br />

            </div>
        </td>
       
    </tr>
  
    <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnCancelInfo" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" style="display:none;"/> &nbsp; <asp:Button ID="btnRedirectPage" runat="server" Text="OK" Width="120px" CssClass="btn btn-danger" />&nbsp; <%--<asp:Button ID="btnPrintReceipt" runat="server" Text="Print Receipt" Width="120px" CssClass="btn btn-danger" />--%>
        </td>
    </tr>
        <tr>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>

    </asp:Panel>

</asp:Content>
