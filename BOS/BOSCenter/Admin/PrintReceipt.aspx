<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrintReceipt.aspx.vb" Inherits="BOSCenter.PrintReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
         window.print();
     </script>
    <style type="text/css">
        .control-label {
            color: #000 !important;
        }

        .control-header {
            color: #000;
        }

        b {
            color: #000 !important;
            font-weight: bold;
        }

        .txn-details {
            width: 100%;
            /*border: 3px solid #05569A;*/
            padding: 30px;
            /* margin-top: 10px;*/
        }

        .txn-details {
            width: 100%;
            /*border: 3px solid #05569A;*/
            padding: 30px 10px;
            /*margin-top: 10px;*/
        }

            .txn-details td {
                padding: 0px 5px;
                border: 3px solid #05569A;
            }

                .txn-details td:last-child {
                    border-right: 0px;
                }

            .txn-details b {
                display: block;
                font-size: 18px;
                /*  margin-top: 5px;
                margin-bottom: 5px;*/
            }

        .payment-details {
            width: 100%;
            border: 3px solid #05569A;
            /* margin-top: 20px;*/
        }

            .payment-details thead {
                background-color: #05569A;
                /* padding: 40px;*/
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div ID="div_pancard" runat="server" >

        <div class="modal-dialog modal-lg" style="border: 5px solid #02B0E8;">
            <div class="modal-content">

                <div class="modal-body">
                    <div id="DIV_PrintReceipt">

                        <center>
                            <asp:Image id="ImagepopUp" runat="server" style="width:290px; height:80px; margin-top:20px; margin-bottom:20px;padding:20px;"></asp:Image>
                            <hr/>
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table style="text-align: left; width:100%;">
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
                      
                    </div>
                </div>
            </div>
        </div>
    </div>

            <div ID="div_recharge" runat="server">

        <div class="modal-dialog modal-lg" style="border: 5px solid #02B0E8;">
            <div class="modal-content">

                <div class="modal-body">
                    <div>

                        <center>
                            <asp:Image id="Img_Recharge" runat="server" style="width:290px; height:80px; margin-top:20px; margin-bottom:20px;padding:20px;"></asp:Image>
                            <hr/>
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table style="text-align: left; width:100%;">
                                                <tbody>
                                                    <tr>

                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <asp:Label ID="Label1" runat="server" Text="AgencyName :"></asp:Label></b> :
                                                                <label id="lblrtshopname_Recharge" class="control-label" style="font-weight: bold">
                                                                    <asp:Label ID="lblshopname_Recharge" runat="server"></asp:Label></label>
                                                        </td>

                                                    </tr>
                                                     <%-- <tr>

                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <asp:Label ID="Label4" runat="server" Text="Mobile No :"></asp:Label></b> :
                                                                <label id="lblMobileNo" class="control-label" style="font-weight: bold">
                                                                    <asp:Label ID="lblMobileNo_Recharge" runat="server"></asp:Label></label>
                                                        </td>

                                                    </tr>--%>
                                                   
                                                </tbody>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="text-align: right; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <asp:Label ID="Label3" runat="server" Text="TransactionId : "></asp:Label></b>:
                                                                <label id="lblReceipt1" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                    <asp:Label ID="lblTransation1_Recharge" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>Date: </b>
                                                            <label id="lblDate_Recharge" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                <asp:Label ID="lblPopDateTime_Recharge" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="text-align:left; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <label id="lblrchmade_Recharge1" style="color: #000000; font-weight: bold">Operator Type : </label>
                                                        </b>:<label id="lblBankOP_Recharge1" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                            <asp:Label ID="lblType_Recharge" runat="server"></asp:Label></label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 14px;" id="Nlblbeneficiary_Recharge"><b>
                                                              <asp:Label ID="lblCustomer_Mobileno_Type" runat="server" text="Mobile No/CA No"></asp:Label>
                                                            </b> :
                                                                <label id="lblBeneficiary_Recharge" style="color: #000000; font-weight: bold">
                                                                    <asp:Label ID="lblCustomer_Mobileno" runat="server"></asp:Label></label>
                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>

                                         <td>
                                            <table style="text-align:right; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <label id="lblrchmade_Recharge" style="color: #000000; font-weight: bold">Mode </label>
                                                        </b>:<label id="lblBankOP_Recharge" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                            <asp:Label ID="lblMode_Recharge" runat="server"></asp:Label></label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 14px;" id="lblRe"><b>Status</b> :
                                                                <label id="lblRech" style="color: #000000; font-weight: bold">
                                                                    <asp:Label ID="lblStatus_Recharge" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></label>
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
                                                <asp:Label ID="lblTransactionNo_Recharge" runat="server"></asp:Label></b></td>
                                        <td style="border: 1px solid #000;">
                                            <b>
                                                <asp:Label ID="lblTStatus_Recharge" runat="server" Font-Bold="true"></asp:Label>
                                            </b></td>

                                        <td style="border: 1px solid #000; text-align: right">
                                            <b>₹ <span id="rptData_lblAmount__Recharge">
                                                <asp:Label ID="lblAmt_Recharge" runat="server"></asp:Label></span></b></td>
                                    </tr>


                                    <tr style="text-align: center">
                                        <td style="border: 1px solid #000;"></td>
                                        <td style="border: 1px solid #000; font-weight: bold;">Total</td>
                                        <td style="border: 1px solid #000; text-align: right"><b>₹
                                                    <label id="lblFinalAmount_Recharge1" style="text-transform: uppercase; color: #000; font-weight: 800; font-size: 20px !important">
                                                        <asp:Label ID="lblFinalAmount_Recharge" runat="server"></asp:Label></label>
                                        </b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <label id="lblrlrshopadd_Recharge" style="display: none"></label>
                            <p id="lbltotalvaluewrd_Recharge1" style="font-weight: bold; font-size: 15px; text-align: left; padding-left: 10px;">In words:
                                <asp:Label ID="lbltotalvaluewrd_Recharge" runat="server"></asp:Label></p>
                              
                        </center>
                      
                    </div>
                </div>
            </div>
        </div>
    </div>


            <div ID="div_moneytransfer" runat="server">

        <div class="modal-dialog modal-lg" style="border: 5px solid #02B0E8;">
            <div class="modal-content">

                <div class="modal-body">
                    <div>

                        <center>
                            <asp:Image id="Image1" runat="server" style="width:290px; height:80px; margin-top:20px; margin-bottom:20px;padding:20px;"></asp:Image>
                            <hr/>
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table style="text-align: left; width:100%;">
                                                <tbody>
                                                    <tr>

                                                        <td style="display: block; font-size: 14px;">
                                                            <b> <asp:Label ID="Label2" runat="server" Text="AgencyName :"></asp:Label></b> :
                                                                <label id="lblrtshopname_Recharge1" class="control-label" style="font-weight: bold">
                                                                    <asp:Label ID="lblshopname_Money" runat="server"></asp:Label></label>
                                                            <br />
                                                            <b> <asp:Label ID="Label6" runat="server" Text="Address :"></asp:Label></b> :
                                                            <label id="lblrtshopname_Address" class="control-label" style="font-weight: bold">
                                                                    <asp:Label ID="lblshopname_Address" runat="server"></asp:Label></label>
                                                        </td>

                                                    </tr>
                                                      <tr>

                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <asp:Label ID="Label4" runat="server" Text="Mobile No :"></asp:Label></b> :
                                                                <label id="lblMobileNo" class="control-label" style="font-weight: bold">
                                                                    <asp:Label ID="lblMobileNo_Money" runat="server"></asp:Label></label>
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
                                                            <asp:Label ID="Label5" runat="server" Text="TransactionId : "></asp:Label></b>:
                                                                <label id="lblReceipt11" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                    <asp:Label ID="lblTransactionId_Money" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>Date: </b>
                                                            <label id="lblDate_Recharge1" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                <asp:Label ID="lblDate_Money" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="text-align:left; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <label id="lblrchmade_Recharge11" style="color: #000000; font-weight: bold">Bank Name : </label>
                                                        </b>:<label id="lblBankOP_Recharge11" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                            <asp:Label ID="lblBankName" runat="server"></asp:Label></label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 14px;" id="Nlblbeneficiary_Recharge1"><b>
                                                              <asp:Label ID="Label9" runat="server" text="Beneficiary"></asp:Label>
                                                            </b> :
                                                                <label id="lblBeneficiary_Recharge1" style="color: #000000; font-weight: bold">
                                                                    <asp:Label ID="lblBeneficiaryName" runat="server"></asp:Label></label>
                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>

                                         <td>
                                            <table style="text-align:right; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <label id="lblrchmade_Recharge2" style="color: #000000; font-weight: bold">Mode </label>
                                                        </b>:<label id="lblBankOP_Recharge2" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                            <asp:Label ID="lblMode_Money" runat="server"></asp:Label></label></td>
                                                    </tr>
                                                  
                                                </tbody>
                                            </table>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td>
                                            <table style="text-align:left; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <label id="lblrchmade_Recharge111" style="color: #000000; font-weight: bold">Account No: </label>
                                                        </b>:<label id="lblBankOP_Recharge111" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                            <asp:Label ID="lblAccountNo_Money" runat="server"></asp:Label></label></td>
                                                    </tr>
                                                   
                                                </tbody>
                                            </table>
                                        </td>

                                         <td>
                                            <table style="text-align:right; width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <label id="lblrchmade_Recharge112" style="color: #000000; font-weight: bold">Customer Mobile No: </label>
                                                        </b>:<label id="lblBankOP_Recharge112" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                            <asp:Label ID="lblCustomerMobileNo_Money" runat="server"></asp:Label></label></td>
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
                                    <asp:ListView ID="ListView1" runat="server">
                                        <ItemTemplate>
                                            <tr style="text-align: center">
                                        <td style="border: 1px solid #000;">
                                            <b>
                                                <asp:Label ID="lblUTR" runat="server"></asp:Label></b></td>
                                        <td style="border: 1px solid #000;">
                                            <b>
                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                            </b></td>

                                        <td style="border: 1px solid #000; text-align: right">
                                            <b>₹ <span id="rptData_lblAmount__Recharge2">
                                                <asp:Label ID="lblAmount" runat="server"></asp:Label></span></b></td>
                                    </tr>

                                        </ItemTemplate>

                                    </asp:ListView>
                                  
                                    


                                    <tr style="text-align: center">
                                        <td style="border: 1px solid #000;"></td>
                                        <td style="border: 1px solid #000; font-weight: bold;">Total</td>
                                        <td style="border: 1px solid #000; text-align: right"><b>₹
                                                    <label id="lblFinalAmount_Recharge2" style="text-transform: uppercase; color: #000; font-weight: 800; font-size: 20px !important">
                                                        <asp:Label ID="lblFinalAmount_Money" runat="server"></asp:Label></label>
                                        </b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <label id="lblrlrshopadd_Recharge2" style="display: none"></label>
                            <p id="lbltotalvaluewrd_Recharge2" style="font-weight: bold; font-size: 15px; text-align: left; padding-left: 10px;">In words:
                                <asp:Label ID="lbltotalvaluewrd_Money" runat="server"></asp:Label></p>
                              
                        </center>
                      
                    </div>
                </div>
            </div>
        </div>
    </div>


            <div ID="div_aeps" runat="server" >

        <div class="modal-dialog modal-lg" style="border: 5px solid #02B0E8;">
            <div class="modal-content">

                <div class="modal-body">
                    <div id="DIV_PrintReceipt1">

                        <center>
                            <asp:Image id="imgAEPS" runat="server" style="width:290px; height:80px; margin-top:20px; margin-bottom:20px;padding:20px;"></asp:Image>
                            <hr/>
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table style="text-align: left; width:100%;">
                                                <tbody>
                                                    <tr>

                                                        <td style="display: block; font-size: 14px;"><b>
                                                            <asp:Label ID="Label7" runat="server" Text="AgencyName :"></asp:Label></b> :
                                                                <label id="lblrtshopname" class="control-label" style="font-weight: bold">
                                                                    <asp:Label ID="lbl_AEPS_AgencyName" runat="server"></asp:Label></label>
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
                                                            <asp:Label ID="Label10" runat="server" Text="TransactionId : "></asp:Label></b>:
                                                                <label id="lblReceip22t" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                    <asp:Label ID="lbl_AEPS_TransID" runat="server"></asp:Label></label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><b>Date: </b>
                                                            <label id="lblDate222" style="text-transform: uppercase; color: #000; font-weight: bold">
                                                                <asp:Label ID="lbl_AEPS_TransDate" runat="server"></asp:Label></label>
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
                                                            <label id="lblrchmade33" style="color: #000000; font-weight: bold">TYPE : </label>
                                                        </b>:<label id="lblBankOP333" style="text-transform: uppercase; color: #000; font-weight: bold"><asp:Label ID="lbl_AEPS_API_TYPE" ForeColor="Blue" Font-Bold="true" runat="server"></asp:Label></label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="display: block; font-size: 14px;"><strong>BANK NAME / BANK IIN : </strong> <asp:Label ID="lbl_AEPS_BANK_IIN" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 14px;" id="Nlblbeneficiary333"><strong>LATITUDE / LONGITUDE : </strong> <asp:Label ID="lbl_AEPS_LAT_LONG" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 14px;" id="Nlblbeneficiary33322"><strong>ACCESS MODE : </strong> <asp:Label ID="lbl_AEPS_ACCESS_MODE" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 14px;" id="Nlblbeneficiary33332"><strong>AADHAR LAST NO : </strong> <asp:Label ID="lbl_AEPS_AADHAR_LASTNO" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
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

                                        <td style="color: #000; font-weight: bold; border: 1px solid #000;"><strong>Mobile No</strong>
                                        </td>
                                        <td style="color: #000; font-weight: bold; border: 1px solid #000;"><strong>Status</strong>
                                        </td>
                                        <td style="color: #000; font-weight: bold; border: 1px solid #000;"><strong>Amount Paid</strong>
                                        </td>
                                    </tr>

                                    <tr style="text-align: center">
                                        <td style="border: 1px solid #000;">
                                            <b>
                                                <asp:Label ID="lbl_AEPS_MobileNo" runat="server"></asp:Label></b></td>
                                        <td style="border: 1px solid #000;">
                                            <b>
                                                <asp:Label ID="lbl_AEPS_Remarks" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </b></td>

                                        <td style="border: 1px solid #000; text-align: right">
                                            <b>₹ <span id="rptData_lblAmount_012">
                                                <asp:Label ID="lbl_AEPS_CashWithdraw" runat="server"></asp:Label></span></b></td>
                                    </tr>


                                    <tr style="text-align: center">
                                        <td style="border: 1px solid #000;"></td>
                                        <td style="border: 1px solid #000; font-weight: bold;">Total</td>
                                        <td style="border: 1px solid #000; text-align: right"><b>₹
                                                    <label id="lblFinalAmountddd" style="text-transform: uppercase; color: #000; font-weight: 800; font-size: 20px !important">
                                                        <asp:Label ID="lbl_AEPS_CashWithdraw_Total" runat="server"></asp:Label></label>
                                        </b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <label id="lblrlrshopadd333" style="display: none"></label>
                            <p id="lbltotalvaluewrd333" style="font-weight: bold; font-size: 15px; text-align: left; padding-left: 10px;">In words:
                                <asp:Label ID="lbl_AEPS_Amt_InWords" runat="server"></asp:Label></p>
                              
                        </center>
                      
                    </div>
                </div>
            </div>
        </div>
    </div>

        </div>
    </form>
</body>
</html>
