<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/SuperAdmin.Master" CodeBehind="BOS_AEPS_Report.aspx.vb" Inherits="BOSCenter.BOS_AEPS_Report" %>

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

    <div class="container" style="width: 99%;">
        <div class="row">

            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="log_form_head1">
                            <asp:Label ID="lblHeading" runat="server" Text=" AEPS Report"></asp:Label>
                        </div>
                    </div>
                </div>



                <div class="log_form1">



                    <div class="row">
                        <div class="col-sm-10 col-lg-offset-1">
                            <form class="form-horizontal">

                                <div class="row">
                                    <div class="form-group">

                                        <div class="col-sm-4">
                                            <asp:CheckBox ID="chkduration" runat="server"
                                                Text="&nbsp;Apply Date" AutoPostBack="True" />
                                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>
                                            <asp:Label ID="lblError0" runat="server" Font-Bold="True" Font-Size="11px"></asp:Label>

                                        </div>


                                    </div>
                                </div>

                                <div class="clearfix" style="margin-bottom: 5px;"></div>

                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-3">

                                            <asp:DropDownList ID="ddlAmountType" CssClass="form-control inputtext" runat="server" AutoPostBack="true">
                                                <asp:ListItem>All Type</asp:ListItem>
                                                <asp:ListItem>AEPS</asp:ListItem>
                                                <asp:ListItem>AADHAR PAY</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <label for="inputEmail3" class="col-sm-1 control-label">From</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtFrom" Enabled="false" CssClass="form-control inputtext" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server"
                                                Enabled="True" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>

                                        </div>
                                        <label for="inputEmail3" class="col-sm-1 control-label">To</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtTO" Enabled="false" CssClass="form-control inputtext" runat="server" ClientIDMode="Static"></asp:TextBox>

                                            <asp:CalendarExtender ID="txtTO_CalendarExtender" runat="server" Enabled="True"
                                                TargetControlID="txtTO" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>

                                        </div>
                                    </div>
                                </div>


                                <div class="clearfix" style="margin-bottom: 5px;"></div>


                            </form>
                        </div>


                    </div>



                    <div class="row">
                        <div class="form-group">
                            <div class="col-sm-5">
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblRID" runat="server" Visible="False"></asp:Label>

                                <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
                                <asp:Label ID="lblError1" runat="server"></asp:Label>
                                <asp:Label ID="lblExportQry" runat="server" Visible="false"></asp:Label>
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
                                        CssClass="btn btn-primary mar_top10" ValidationGroup="a" />&nbsp;  
                            	
                        
                             <asp:Button ID="btnReset" runat="server" Text="Reset"
                                 CssClass="btn btn-primary mar_top10" CausesValidation="False"
                                 UseSubmitBehavior="False" />




                                </div>

                            </div>

                        </div>

                        <div class="clearfix" style="margin-bottom: 5px;"></div>

                        <div class="row">
                            <div class="col-md-4"></div>

                            <div class="col-md-2"></div>

                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-12 ExportPanel">

                            <div class="col-md-6">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <asp:ImageButton ID="ImagebtnExcel" runat="server" Visible="false" Width="32px" Height="32px"
                                            ImageUrl="~/images/excel_32X32.png" />
                                        <asp:ImageButton ID="ImagebtnWOrd" runat="server" Width="32px" Height="32px" Visible="false"
                                            ImageUrl="~/images/word_32X32.png" />
                                        <asp:ImageButton ID="Imagepdf" runat="server" Width="32px" Height="32px" Visible="false"
                                            ImageUrl="~/images/pdf_32X32.png" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="ImagebtnExcel" />
                                        <asp:PostBackTrigger ControlID="ImagebtnWOrd" />
                                        <asp:PostBackTrigger ControlID="Imagepdf" />
                                    </Triggers>

                                </asp:UpdatePanel>

                            </div>


                            <div class="col-md-6">
                                <label class="col-sm-2"></label>
                                <div class="col-sm-4 pull-right">
                                    <asp:DropDownList ID="ddlNoOfRecords" runat="server"
                                        CssClass="form-control inputtext" AutoPostBack="True" Visible="False">
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
            <div class="row">
                <div class="col-lg-4">
                    &nbsp;&nbsp;   Search Details ::-
                </div>


            </div>


        </div>
        <div class="clearfix"></div>
        <div class="table-responsive">
            <div class="table_wid">
                <div runat="server" id="ApprovalDiv">
                    <asp:GridView ID="GridView1" runat="server" CssClass="grid-view-themeclass" ShowFooter="True" PageSize="1000000"
                        BorderStyle="None" AllowPaging="True" AlternatingRowStyle-Wrap="false" AutoGenerateColumns="true">
                        <Columns>

                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="btnGrdPrint_Click"
                                        CommandName="Select" Text=""><i class="fa fa-print"></i>&nbsp;Print</asp:LinkButton>
                                    <asp:Label ID="lblgrdRID" runat="server" Text='<%# Eval("SrNo") %>' Visible="false"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerSettings Position="TopAndBottom" />
                        <PagerStyle CssClass="grid-pagin" />
                        <FooterStyle Font-Size="Medium" Font-Bold="True" ForeColor="#CC0000" />
                    </asp:GridView>

                </div>
            </div>
        </div>



    </div>
    <asp:Button ID="btnReasonPopup" runat="server" Text="Button" Style="display: none;" />
    <asp:ModalPopupExtender ID="ModalPopup_Reason" runat="server" TargetControlID="btnReasonPopup" PopupControlID="pnlReasonPopup" BackgroundCssClass="modalBackground" CancelControlID="btnReasonCancel">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlReasonPopup" runat="server" Width="350px" Style="display: none;">

        <table style="width: 100%; background-color: White; border: 1px solid gray;">
            <tr>
                <td align="center" bgcolor="Silver">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" bgcolor="Silver">
                    <strong>Reason Dialog</strong>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="Silver">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter&nbsp; Reason:<label style="color: Red;">*</label>
                    <asp:RequiredFieldValidator ID="Required_ReasonValidator" runat="server"
                        ControlToValidate="txtReason" ErrorMessage="Please Enter Reason."
                        Font-Bold="True" ForeColor="Red" ValidationGroup="x"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"
                        TextMode="MultiLine" ValidationGroup="x" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnReasonSubmit" runat="server" Text="Submit" Width="80px"
                        CssClass="btn btn-primary" ValidationGroup="x" />
                    &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReasonCancel" runat="server" Text="Cancel" Width="80px"
                CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
        </table>



    </asp:Panel>

    <asp:Button ID="modalPopupButton" runat="server" Text="Button" Style="display: none;" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="modalPopupButton" PopupControlID="DeletePopup" BackgroundCssClass="modalBackground" CancelControlID="btnCancel">
    </asp:ModalPopupExtender>
    <asp:Panel ID="DeletePopup" runat="server" Width="350px" Style="display: none;">

        <table style="width: 100%; background-color: White; border: 1px solid gray;">
            <tr>
                <td align="center" bgcolor="Silver">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" bgcolor="Silver">
                    <strong>Confirmation Dialog</strong>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="Silver">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnok" runat="server" Text="OK" Width="80px" CssClass="btn btn-primary" />
                    &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
        </table>

    </asp:Panel>


    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button1" PopupControlID="InformationPopup" BackgroundCssClass="modalBackground" CancelControlID="btnCancelInfo">
    </asp:ModalPopupExtender>

    <asp:Panel ID="InformationPopup" runat="server" Width="350px" Style="display: none;">
        <table style="width: 100%; background-color: White; border: 1px solid gray;">
            <tr>
                <td align="center" bgcolor="Silver">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" bgcolor="Silver">
                    <strong>
                        <asp:Label ID="lblDialogMsgInfo" runat="server" Text=""></asp:Label></strong>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="Silver">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
            <tr>
                <td align="left"></td>

            </tr>

            <tr>
                <td align="center">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnPrintReceipt1" runat="server" Text="Print Receipt" Width="120px" CssClass="btn btn-danger" />
                    &nbsp;
                    <asp:Button ID="btnCancelInfo" runat="server" Text="Cancel" Width="80px" CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
        </table>

    </asp:Panel>


    <%-- <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="replymodal" BackgroundCssClass="modalBackground" CancelControlID="btnmodalcancel">
    </asp:ModalPopupExtender>

   <asp:Panel runat="server" ID="replymodal">

        <div class="modal-dialog modal-lg" style="border: 5px solid #02B0E8;">
            <div class="modal-content">

                <div class="modal-body">
                    <div id="DIV_PrintReceipt">

                        <center>
                            <asp:Image id="ImagepopUp" runat="server" style="width: 290px; height: 80px; margin-top: -8px; margin-bottom: -18px;"></asp:Image>
                           
                            <hr>
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table style="text-align: left; width: 100%;">
                                                <tbody>
                                                    <tr>

                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <asp:Label ID="lblInformation" runat="server" Text="AgencyName :"></asp:Label></b> :
                                                                <label id="lblrtshopname" class="control-label" style="font-weight: bold">
                                                                    <asp:Label ID="lblPopAgencyName" runat="server"></asp:Label></label>
                                                        </td>

                                                    </tr>
                                                   
                                                </tbody>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="text-align: right; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <asp:Label ID="Label17" runat="server" Text="TransactionId : "></asp:Label></b>:
                                                                <label id="lblReceipt" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                    <asp:Label ID="lblPopTransactionID" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>Date: </b>
                                                            <label id="lblDate" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                <asp:Label ID="lblPopDateTime" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="text-align: left; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <label id="lblrchmade" style="color: #000000; font-weight: bold">Pancard Type : </label>
                                                        </b>:<label id="lblBankOP" style="text-transform: uppercase; color: #000; font-weight: bold"><asp:Label ID="lblPopPancartType" runat="server"></asp:Label></label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 14px;" id="Nlblbeneficiary"><b>Status</b> :
                                                                <label id="lblBeneficiary" style="color: #000000; font-weight: bold">
                                                                    <asp:Label ID="lblPopStatus" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></label>
                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>

                                    </tr>

                                </tbody>
                            </table>
                            <table class="txn-details">
                                <tbody>
                                    <tr style="text-align: center">

                                        <td style="color: #000; font-weight: bold; border: 1px solid #000;"><strong>UTR No</strong>
                                        </td>
                                        <td style="color: #000; font-weight: bold; border: 1px solid #000;"><strong>Status</strong>
                                        </td>
                                        <td style="color: #000; font-weight: bold; border: 1px solid #000;"><strong>Amount Paid</strong>
                                        </td>
                                    </tr>

                                    <tr style="text-align: center">
                                        <td style="border: 1px solid #000;">
                                            <b>
                                                <asp:Label ID="lblPopTransactionID1" runat="server"></asp:Label></b></td>
                                        <td style="border: 1px solid #000;">
                                            <b>
                                                <asp:Label ID="lblPopStatus1" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </b></td>

                                        <td style="border: 1px solid #000; text-align: right">
                                            <b>₹ <span id="rptData_lblAmount_0">
                                                <asp:Label ID="lblPopTransationAmt" runat="server"></asp:Label></span></b></td>
                                    </tr>


                                    <tr style="text-align: center">
                                        <td style="border: 1px solid #000;"></td>
                                        <td style="border: 1px solid #000; font-weight: bold;">Total</td>
                                        <td style="border: 1px solid #000; text-align: right"><b>₹
                                                    <label id="lblFinalAmount" style="text-transform: uppercase; color: #000; font-weight: 800; font-size: 20px !important">
                                                        <asp:Label ID="lblPopTransationAmt1" runat="server"></asp:Label></label>
                                        </b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <label id="lblrlrshopadd" style="display: none"></label>
                            <p id="lbltotalvaluewrd" style="font-weight: bold; font-size: 15px; text-align: left; padding-left: 10px;">In words:
                                <asp:Label ID="lblInword" runat="server"></asp:Label></p>

                        </center>
                        <div>
                            
                            <asp:Button ID="btnmodalcancel" runat="server" Text="Cancel" CssClass="btn btn-primary"/>&nbsp;
                            <asp:Button ID="btnPrintReceipt" runat="server" Text="Print" CssClass="btn btn-primary" />
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>--%>

</asp:Content>
