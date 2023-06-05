
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net
Imports AjaxControlToolkit
Imports Newtonsoft.Json.Linq
Imports BOSCenter.BOS_FidyPayOut_API
Imports System.Web.Http

Public Class BOS_ApprovePayment
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim flag, baneid, accountname As String
    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim Filter As String = ""

            If ddlSelectCriteria.SelectedValue.Trim.ToUpper = "All Records".Trim.ToUpper Then
                Filter = ""
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Register ID".Trim.ToUpper Then
                Filter = " And RegistrationId ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "TransactionID".Trim.ToUpper Then
                Filter = " And TransactionID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "PaymentMode".Trim.ToUpper Then
                Filter = " And PaymentMode like '" & GV.parseString(txtSearchingValue.Text.Trim) & "%'"
            End If
            Querystring = ""

            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
                Querystring = "select RID as SrNo,RefrenceID,RegistrationId,PaymentMode,(CONVERT(VARCHAR(11),PaymentDate,106)) as PaymentDate,DepositBankName,BranchCode_ChecqueNo as 'BranchCode/ChequeNo',Remarks,TransactionID,isnull(DocumentPath,'') as DocumentPath ,ApporvedStatus,ApporveRemakrs as 'AdminRemarks',Amount,CompanyCode,AccountHolder  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_MakePayemnts_Details_SA where ApporvedStatus='Pending'  " & Filter & " order by RID Desc"
            Else
                'admin and other employees 
                Querystring = "select RID as SrNo,RefrenceID,RegistrationId,PaymentMode,(CONVERT(VARCHAR(11),PaymentDate,106)) as PaymentDate,DepositBankName,BranchCode_ChecqueNo as 'BranchCode/ChequeNo',Remarks,TransactionID,isnull(DocumentPath,'') as DocumentPath ,ApporvedStatus,ApporveRemakrs as 'AdminRemarks',Amount,CompanyCode,AccountHolder  from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details where ApporvedStatus='Pending'  " & Filter & " order by RID Desc"
            End If



            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                        Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                        If lblAttachment.Text = "" Then
                            lnkdwnload.Visible = False
                        Else
                            lnkdwnload.Visible = True
                        End If
                        GridView1.Rows(i).Cells(1).Text = i + 1
                    Next
                Else

                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub UpdateDetails_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            txtRemarks.Text = ""
            ddlStatus.SelectedIndex = 0
            lblTransferAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            lblTrasferTo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            lblRefrenceID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) 'RefID

            txtDeductAmt.Text = "0"
            txtApprovedAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)

            lblCompanyCode.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(13).Text)
            lblAccountNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(14).Text)
            'ddlStatus.SelectedValue = ComplaintStatus
            'If ComplaintStatus = "Closed" Then
            '    txtRemarks.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text)
            'End If

            btnCancel.Visible = True
            btnCancel.Text = "Cancel"
            btnUpdate.Visible = True
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""
            ModalPopupExtender1.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlNoOfRecords_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNoOfRecords.SelectedIndexChanged
        Try
            If ddlNoOfRecords.SelectedValue = "10 Record(s)" Then
                GridView1.PageSize = 10
            ElseIf ddlNoOfRecords.SelectedValue = "25 Record(s)" Then
                GridView1.PageSize = 25
            ElseIf ddlNoOfRecords.SelectedValue = "50 Record(s)" Then
                GridView1.PageSize = 50
            ElseIf ddlNoOfRecords.SelectedValue = "100 Record(s)" Then
                GridView1.PageSize = 100
            ElseIf ddlNoOfRecords.SelectedValue = "200 Record(s)" Then
                GridView1.PageSize = 200
            ElseIf ddlNoOfRecords.SelectedValue = "500 Record(s)" Then
                GridView1.PageSize = 500
            End If
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception
                GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

            End Try
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)
        End Try

    End Sub



    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            GV.ExportToWord(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""


            If Not lblRID.Text = "" Then
                If txtRemarks.Text = "" Then
                    lblDialogMsg.Text = "Pls Enter Remarks."
                    lblDialogMsg.CssClass = "errorlabels"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If
                Dim VTransferAmt, VTransferFromMsg, VTransferToMsg, SMSMeassgeTo, VRemark, VTransferFrom, VTransferTo, AccountNo As String
                Dim VUpdatedBy, VUpdatedOn As String

                VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

                VUpdatedOn = "getdate()"
                VTransferAmt = ""
                VTransferFrom = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                VTransferTo = GV.parseString(lblTrasferTo.Text.Trim)
                AccountNo = GV.parseString(lblAccountNo.Text)
                If Not txtRemarks.Text = "" Then
                    VRemark = GV.parseString(txtRemarks.Text.Trim)
                Else
                    VRemark = ""
                End If

                If Not lblTransferAmount.Text = "" Then
                    VTransferAmt = GV.parseString(lblTransferAmount.Text.Trim)
                End If
                Dim str As String = ""



                Dim VRetailerID As String = GV.FL.AddInVar("RegistrationId", "BOS_MakePayemnts_Details where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & " ")
                flag = GV.FL.AddInVar("flag", GV.get_Admin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details where RegistrationId in ('" & VTransferTo & "')  and ApporvedStatus='Pending' ")
                'baneid = GV.FL.AddInVar("BaneID", GV.get_Admin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details where RegistrationId in ('" & VTransferTo & "')  and ApporvedStatus='Pending'  ")
                'accountname = GV.FL.AddInVar("AccountHolder", GV.get_Admin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details where RegistrationId in ('" & VTransferTo & "')   and ApporvedStatus='Pending' ")




                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                    str = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_MakePayemnts_Details_SA set ApporvedStatus='" & GV.parseString(ddlStatus.SelectedValue.Trim) & "', ApporveRemakrs='" & VRemark & "', ApprovedBy='Super Admin',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "
                    If ddlStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then

                        Dim DBName As String = GV.FL_AdminLogin.AddInVar("DatabaseName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & lblCompanyCode.Text.Trim & "' ")

                        If Not DBName.Trim = "" Then
                            Dim V_Amt_Transfer_TransID As String = GV.FL.getAutoNumber("TransId")
                            VTransferFrom = "Super Admin"
                            VTransferTo = "Admin"

                            VTransferFromMsg = "Your Wallet is Debited by Admin (" & lblCompanyCode.Text.Trim & ")"
                            VTransferToMsg = "Your Wallet is Credited by BOS CENTER PVT LTD"
                            SMSMeassgeTo = "Your Wallet is Credited With Rs. " & VTransferAmt & " By BOS CENTER PVT LTD"
                            str = str & "  " & "insert into " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & lblTransferAmount.Text.Trim & "','" & lblRefrenceID.Text.Trim & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','MakePayment','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                            If CDec(txtDeductAmt.Text.Trim) > 0 Then
                                'Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                Dim VTo As String = "Your Account is credited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                str = str & " " & "insert into " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(V_Amt_Transfer_TransID) & "','" & lblTransferAmount.Text.Trim & "','" & GV.parseString(lblRefrenceID.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & VTransferTo & "','" & VTransferFrom & "','" & txtDeductAmt.Text.Trim & "',getdate(),'" & VUpdatedBy & "',getdate() ) ;"
                            End If
                        End If

                    End If
                Else
                    'admin and other employees 
                    If ddlStatus.Text = "Approved" Then

                        Dim FinalApi_URL As String = ""
                        Dim Final_Parameters As String = ""
                        Dim FinalAPI_Result As String = ""
                        Dim ApiMethods As String = ""
                        Dim GenerateToken As String = ""
                        Dim v_Transfer_Trans_ID As String
                        Dim Descriptions As String
                        'Dim ApproveAmount As String = GV.FL.AddInVar("ApproveAmount", "BOS_MakePayemnts_Details where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "' and RegistrationId='" & VRetailerID & "' ")


                        Dim Obj As New BOS_FidyPayOut_API
                        Dim Set_APIParameters_obj As New BOS_FidyPayOut_API.GenerateTokken_API_Parameters
                        Set_APIParameters_obj.amount = lblTransferAmount.Text
                        Set_APIParameters_obj.RegistrationId = GV.parseString(lblTrasferTo.Text.Trim)
                        Set_APIParameters_obj.CompanyCode = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                        Dim Set_APIParameters_obj_Domestic As New DomesticPayment_API_Parameters
                        Final_Parameters = Newtonsoft.Json.JsonConvert.SerializeObject(Set_APIParameters_obj)
                        FinalAPI_Result = "https://api.boscenter.in/api/BOS/GenerateToken"
                        ApiMethods = "POST"
                        FinalAPI_Result = Obj.ReadByRestClient(FinalAPI_Result, Final_Parameters)
                        Dim json_ As String = FinalAPI_Result
                        Dim ser_ As JObject = JObject.Parse(json_)
                        Dim code As String = ser_.SelectToken("code").ToString.Trim
                        Dim description As String = ser_.SelectToken("description").ToString.Trim
                        Dim merchantTrxnRefId As String = ser_.SelectToken("merchantTrxnRefId").ToString.Trim
                        Dim token As String = ser_.SelectToken("token").ToString.Trim
                        Dim status As String = ser_.SelectToken("status").ToString.Trim
                        Dim bankaccountkey As String = "473PHAIC7IYSV23OXB"
                        Dim DomesticPayment_Api_Url As String = "https://api.boscenter.in/api/BOS/DomesticPayment"
                        If status = "Success" Then
                            Set_APIParameters_obj_Domestic.address = GV.FL.AddInVar("AccountAddress", "BOS_beneficiaryDetails where AccountNumber='" & lblAccountNo.Text & "' ")
                            Set_APIParameters_obj_Domestic.amount = lblTransferAmount.Text
                            Set_APIParameters_obj_Domestic.bankaccountkey = bankaccountkey
                            Set_APIParameters_obj_Domestic.beneficiaryAccNo = GV.FL.AddInVar("AccountNumber", "BOS_beneficiaryDetails where AccountNumber='" & lblAccountNo.Text & "' ")
                            Set_APIParameters_obj_Domestic.beneficiaryIfscCode = GV.FL.AddInVar("IfscCode", "BOS_beneficiaryDetails where AccountNumber='" & lblAccountNo.Text & "' ")
                            Set_APIParameters_obj_Domestic.beneficiaryName = GV.FL.AddInVar("AccountName", "BOS_beneficiaryDetails where AccountNumber='" & lblAccountNo.Text & "' ")
                            Set_APIParameters_obj_Domestic.emailAddress = GV.FL.AddInVar("EmailID", "BOS_beneficiaryDetails where AccountNumber='" & lblAccountNo.Text & "' ")
                            Set_APIParameters_obj_Domestic.merchantTrxnRefId = merchantTrxnRefId
                            Set_APIParameters_obj_Domestic.mobileNumber = GV.FL.AddInVar("MobileNo", "BOS_beneficiaryDetails where AccountNumber='" & lblAccountNo.Text & "' ")
                            Set_APIParameters_obj_Domestic.otp = token
                            Set_APIParameters_obj_Domestic.transferType = "NEFT"
                            Set_APIParameters_obj_Domestic.trxnNote = "PayOut"
                            Final_Parameters = Newtonsoft.Json.JsonConvert.SerializeObject(Set_APIParameters_obj_Domestic)
                            ApiMethods = "POST"
                            FinalApi_URL = DomesticPayment_Api_Url
                            If ApiMethods = "POST" Then
                                FinalAPI_Result = Obj.ReadByRestClient(FinalApi_URL, Final_Parameters)
                                Dim TrxnId As String

                                json_ = FinalAPI_Result
                                Dim ser1_ As JObject = JObject.Parse(json_)
                                Dim Status1 As String = ser1_.SelectToken("status").ToString.Trim
                                TrxnId = ser1_.SelectToken("merchantTrxnRefId").ToString.Trim
                                Descriptions = ser1_.SelectToken("description").ToString.Trim

                                If status = "Pending" Then
                                    Dim PaymentStatus_API_Parameters_obj As New PaymentStatus_API_Parameters
                                    PaymentStatus_API_Parameters_obj.trxn_id = TrxnId
                                    Final_Parameters = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentStatus_API_Parameters_obj)
                                    ApiMethods = "POST"
                                    FinalApi_URL = "https://api.boscenter.in/api/BOS/DomesticPaymentStatus"
                                    If ApiMethods = "POST" Then
                                        FinalAPI_Result = Obj.ReadByRestClient(FinalApi_URL, Final_Parameters)

                                        Dim jsons_ As String = FinalAPI_Result
                                        Dim sers_ As JObject = JObject.Parse(jsons_)
                                        status = sers_.SelectToken("status").ToString.Trim
                                        TrxnId = sers_.SelectToken("trxn_id").ToString.Trim

                                        If status = "Success" Then
                                            ModalPopupExtender3.Show()
                                            lblPopDateTime.Text = sers_.SelectToken("creationDateTime").ToString.Trim
                                            lblPopTransactionId.Text = sers_.SelectToken("trxn_id").ToString.Trim
                                            lblPopbankAccount.Text = sers_.SelectToken("beneficiaryAccNo").ToString.Trim
                                            lblpopAmount.Text = sers_.SelectToken("amount").ToString.Trim
                                            lblpopName.Text = sers_.SelectToken("beneficiaryName").ToString.Trim
                                            lblPopUTR.Text = sers_.SelectToken("utr").ToString.Trim
                                            lblPopStatus.Text = sers_.SelectToken("status").ToString.Trim
                                            v_Transfer_Trans_ID = GV.get_AutoNumber("TransId", "BosCenter_DB")
                                            Dim InsrtQry As String = "insert into " & GV.get_Admin_SessionVariables("DBName", Request, Response) & ".dbo.FidyPayoutResponse(amount ,code,baneAddress,beneDescription,beneficiaryIfscCode,merchantTrxnRefId,trxn_id,debitAccNo,utr,beneficiaryAccNo,beneficiaryName,instructionIdentification,bankaccountKey,transactionIdentification,banestatus,creationDateTime,ApiResponse,LoginID,CompanyCode) Values('" & sers_.SelectToken("amount").ToString.Trim & "','" & sers_.SelectToken("code").ToString.Trim & "','" & sers_.SelectToken("address").ToString.Trim & "','" & sers_.SelectToken("description").ToString.Trim & "',
                            '" & sers_.SelectToken("beneficiaryIfscCode").ToString.Trim & "','" & sers_.SelectToken("merchantTrxnRefId").ToString.Trim & "','" & sers_.SelectToken("trxn_id").ToString.Trim & "','" & sers_.SelectToken("debitAccNo").ToString.Trim & "','" & sers_.SelectToken("utr").ToString.Trim & "','" & sers_.SelectToken("beneficiaryAccNo").ToString.Trim & "','" & sers_.SelectToken("beneficiaryName").ToString.Trim & "','" & sers_.SelectToken("instructionIdentification").ToString.Trim & "','" & sers_.SelectToken("bankaccountKey").ToString.Trim & "','" & sers_.SelectToken("transactionIdentification").ToString.Trim & "','" & sers_.SelectToken("status").ToString.Trim & "','" & sers_.SelectToken("creationDateTime").ToString.Trim & "','" & sers_.ToString & "'
                            ,'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "')"

                                            Dim result As Boolean = GV.FL.DMLQueriesBulk(InsrtQry)

                                            str = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details set ApporvedStatus='" & GV.parseString(ddlStatus.SelectedValue.Trim) & "', ApporveRemakrs='" & VRemark & "', ApprovedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "
                                            If ddlStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then
                                                Dim V_Amt_Transfer_TransID As String = GV.FL.getAutoNumber("TransId")
                                                VTransferFromMsg = "Your Wallet is Debited by Distributor (" & VTransferFrom & ")"
                                                VTransferToMsg = "Your Wallet is Credited by BOS CENTER PVT LTD"
                                                SMSMeassgeTo = "Your Wallet is Credited With Rs. " & VTransferAmt & " By BOS CENTER PVT LTD"
                                                str = str & "  " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & lblTransferAmount.Text.Trim & "','" & lblRefrenceID.Text.Trim & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','MakePayment','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                                If CDec(txtDeductAmt.Text.Trim) > 0 Then
                                                    'Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                                    str = str & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(V_Amt_Transfer_TransID) & "','" & lblTransferAmount.Text.Trim & "','" & GV.parseString(lblRefrenceID.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & VTransferTo & "','" & VTransferFrom & "','" & txtDeductAmt.Text.Trim & "',getdate(),'" & VUpdatedBy & "',getdate() ) ;"
                                                End If

                                                If CDec(txtDeductAmt.Text.Trim) > 0 Then
                                                    'Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                                    str = str & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(V_Amt_Transfer_TransID) & "','" & lblTransferAmount.Text.Trim & "','" & GV.parseString(lblRefrenceID.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & VTransferTo & "','" & VTransferFrom & "','" & txtDeductAmt.Text.Trim & "',getdate(),'" & VUpdatedBy & "',getdate() ) ;"
                                                End If
                                            End If

                                            If Not str.Trim = "" Then
                                                result = GV.FL.DMLQueriesBulk(str)
                                                lblDialogMsg.Text = result
                                                If result = True Then

                                                    '// wallet transfer ke case main jo message ja raha hai 

                                                    '// will not shot in reject mode
                                                    If ddlStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then
                                                        If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then

                                                            Dim vName As String = GV.FL.AddInVar("isnull(FirstName,'') + ' ' + isnull(LastName,'')", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")
                                                            Dim vBal As String = GV.AgentBalance(VTransferTo, GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim).ToString
                                                            Dim ToMobile As String = GV.FL.AddInVar("MobileNo", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")

                                                            If GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim = "CMP1165" Then
                                                                Dim vMessage As String = "Dear " & vName & " Your Fund Request Has Been Successfully Approved Of Rs. " & VTransferAmt & " . Thanks For Using Kuber Money"
                                                                GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Approve Submit Payment", "CMP1165")

                                                            ElseIf GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim = "CMP1174" Then
                                                                Dim vMessage As String = "Dear " & vName & " Your Fund Request Has Been Successfully Approved Of Rs. " & VTransferAmt & " . Thanks For Using Easy Talk. Web: https://bit.ly/3p1OkPj App: https://bit.ly/3Szblqb"
                                                                GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Approve Submit Payment", "CMP1174")
                                                            Else
                                                                Dim vMessage As String = "Dear " & vName & " Your Fund Request Has Been Successfully Approved Of Rs. " & VTransferAmt & " . Thanks For Using BOS."
                                                                GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Approve Submit Payment", "")
                                                            End If
                                                        End If
                                                    End If


                                                    lblDialogMsg.Text = "Record Updated Successfully."
                                                    lblDialogMsg.CssClass = "Successlabels"
                                                    Bind()
                                                    btnCancel.Text = "OK"
                                                    btnUpdate.Visible = False
                                                End If

                                                ModalPopupExtender1.Show()

                                            ElseIf status = "Failed" Then
                                                ModalPopupExtender3.Show()
                                                lblPopDateTime.Text = sers_.SelectToken("creationDateTime").ToString.Trim
                                                lblPopTransactionId.Text = sers_.SelectToken("trxn_id").ToString.Trim
                                                lblPopbankAccount.Text = sers_.SelectToken("beneficiaryAccNo").ToString.Trim
                                                lblpopAmount.Text = sers_.SelectToken("amount").ToString.Trim
                                                lblpopName.Text = sers_.SelectToken("beneficiaryName").ToString.Trim
                                                lblPopUTR.Text = sers_.SelectToken("utr").ToString.Trim
                                                lblPopStatus.Text = sers_.SelectToken("status").ToString.Trim
                                                v_Transfer_Trans_ID = GV.get_AutoNumber("TransId", "BosCenter_DB")


                                            End If
                                        End If

                                    Else

                                        lblDialogMsg.Text = Descriptions.ToString()
                                        lblDialogMsg.CssClass = "errorlabels"
                                        ModalPopupExtender1.Show()

                                        'lblDialogMsg.Text = Descriptions.ToString()
                                        'lblDialogMsg.CssClass = "errorlabels"
                                    End If
                                End If

                            End If
                        End If
                    Else
                        str = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details set ApporvedStatus='" & GV.parseString(ddlStatus.SelectedValue.Trim) & "', ApporveRemakrs='" & VRemark & "', ApprovedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "
                        If ddlStatus.SelectedValue.Trim.ToUpper = "Rejected".Trim.ToUpper Then
                            Dim V_Amt_Transfer_TransID As String = GV.FL.getAutoNumber("TransId")
                            VTransferFromMsg = "Your Wallet is Credited by Distributor (" & VTransferFrom & ")"
                            VTransferToMsg = "Your Wallet is Debited by BOS CENTER PVT LTD"
                            SMSMeassgeTo = "Your Wallet is Debited With Rs. " & VTransferAmt & " By BOS CENTER PVT LTD"
                            str = str & "  " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferAmt & "','" & lblRefrenceID.Text.Trim & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferFromMsg & "','" & VTransferToMsg & "','MakePayment','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                            If CDec(lblTransferAmount.Text.Trim) > 0 Then
                                'Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                                Dim VFrom As String = "Your Account is credited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                Dim VTo As String = "Your Account is debited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                str = str & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(V_Amt_Transfer_TransID) & "','" & lblTransferAmount.Text.Trim & "','" & GV.parseString(lblRefrenceID.Text.Trim) & "','" & VFrom & "','" & VTo & "','Service Charge','Service Charge',getdate(),'" & VTransferTo & "','" & VTransferFrom & "','" & txtDeductAmt.Text.Trim & "',getdate(),'" & VUpdatedBy & "',getdate() ) ;"
                            End If
                            If Not str.Trim = "" Then
                                GV.FL.DMLQueriesBulk(str)
                            End If
                            lblDialogMsg.Text = "Sorry !! Record Failed."
                            lblDialogMsg.CssClass = "errorlabels"
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            lblExportQry.Text = ""

            lblError1.Text = ""
            lblNoRecords.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            If Not ddlSelectCriteria.SelectedValue = "All Records" And GV.parseString(txtSearchingValue.Text.Trim) = "" Then
                lblError1.Text = "Enter Search value"
                lblError1.CssClass = "errorlabels"
                Exit Sub
            End If
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub DownloadRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lblAttachment As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("lblAttachment"), Label)
            DownloadDoc(lblAttachment.Text)
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Sub DownloadDoc(ByVal DocPath As String)
        Try

            Dim fi As New FileInfo(DocPath)
            Dim strURL As String = DocPath
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" & fi.Name & """")
            Dim data As Byte() = req.DownloadData(Server.MapPath(strURL))
            response.BinaryWrite(data)


            response.[End]()
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub clear()
        Try
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            ddlSelectCriteria.SelectedIndex = 0
            txtSearchingValue.Text = ""
            Bind()
            ddlNoOfRecords.SelectedIndex = 0
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub txtDeductAmt_TextChanged(sender As Object, e As System.EventArgs) Handles txtDeductAmt.TextChanged
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""

            If GV.parseString(txtDeductAmt.Text.Trim) = "" Then
                txtDeductAmt.Text = "0"
            End If

            If GV.parseString(lblTransferAmount.Text.Trim) = "" Then
                lblTransferAmount.Text = "0"
            End If


            If CLng(txtDeductAmt.Text.Trim) >= CLng(lblTransferAmount.Text.Trim) Then
                lblDialogMsg.Text = "Deduction Can't be More Or Equal To Requested Amt."
                lblDialogMsg.CssClass = "errorlabels"
                txtDeductAmt.Focus()
            Else
                txtApprovedAmount.Text = CLng(lblTransferAmount.Text.Trim) - CLng(txtDeductAmt.Text.Trim)
            End If

            ModalPopupExtender1.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""

            If ddlStatus.SelectedValue = "Approved" Then
                txtDeductAmt.Text = "0"
                txtDeductAmt.ReadOnly = False
                txtApprovedAmount.Text = lblTransferAmount.Text.Trim
            Else
                txtDeductAmt.Text = "0"
                txtDeductAmt.ReadOnly = True
                txtApprovedAmount.Text = lblTransferAmount.Text.Trim
            End If

            ModalPopupExtender1.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
    End Sub




End Class