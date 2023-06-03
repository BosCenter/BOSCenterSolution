Public Class PrintReceipt
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("ADMIN")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Request.QueryString("Type") Is Nothing Then
                If Not Request.QueryString("Type").Trim = "" Then

                    Div_Recharge.Visible = False
                    div_pancard.Visible = False
                    div_moneytransfer.Visible = False
                    div_aeps.Visible = False




                    If (Request.QueryString("Type").Trim).ToUpper = "E-Card".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "P-Card".Trim.ToUpper Then
                        Div_Pancard.Visible = True
                        Dim abc As String = "  select  RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,AgentID as 'AGENT_ID',API_TransId as 'TRANS_NO',CoupanType as 'SERVICE_TYPE',Amount as 'AMOUNT',TotalCoupan as 'COUPONS',TotalAmount as 'TOTAL_AMT',API_Message as 'STATUS',Remarks as 'REMARKS' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Pan_Card_API where API_Message='Success' and TransId='" & Request.QueryString("ID").Trim & "' and CoupanType='" & GV.Decode_Url_String(Request.QueryString("Type").Trim) & "' order by rid desc  "
                        ds = New DataSet
                        ds = GV.FL.OpenDsWithSelectQuery(abc)
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DATE")) Then
                                    If Not ds.Tables(0).Rows(0).Item("DATE").ToString() = "" Then
                                        lblPopDateTime.Text = GV.parseString(ds.Tables(0).Rows(0).Item("DATE").ToString()) & " " & GV.parseString(ds.Tables(0).Rows(0).Item("TIME").ToString())

                                    Else
                                        lblPopDateTime.Text = ""
                                    End If
                                Else
                                    lblPopDateTime.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TRANS_NO")) Then
                                    If Not ds.Tables(0).Rows(0).Item("TRANS_NO").ToString() = "" Then
                                        lblPopTransactionID.Text = GV.parseString(ds.Tables(0).Rows(0).Item("TRANS_NO").ToString())
                                        lblPopTransactionID1.Text = GV.parseString(ds.Tables(0).Rows(0).Item("TRANS_NO").ToString())
                                    Else
                                        lblPopTransactionID.Text = ""
                                        lblPopTransactionID1.Text = ""
                                    End If
                                Else
                                    lblPopTransactionID1.Text = ""
                                    lblPopTransactionID.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AMOUNT")) Then
                                    If Not ds.Tables(0).Rows(0).Item("AMOUNT").ToString() = "" Then
                                        lblPopTransationAmt.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AMOUNT").ToString())
                                        lblPopTransationAmt1.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AMOUNT").ToString())
                                    Else
                                        lblPopTransationAmt1.Text = ""
                                        lblPopTransationAmt.Text = ""
                                    End If
                                Else
                                    lblPopTransationAmt1.Text = ""
                                    lblPopTransationAmt.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("STATUS")) Then
                                    If Not ds.Tables(0).Rows(0).Item("STATUS").ToString() = "" Then
                                        lblPopStatus1.Text = GV.parseString(ds.Tables(0).Rows(0).Item("STATUS").ToString())
                                        lblPopStatus.Text = GV.parseString(ds.Tables(0).Rows(0).Item("STATUS").ToString())
                                    Else
                                        lblPopStatus.Text = ""
                                        lblPopStatus1.Text = ""
                                    End If
                                Else
                                    lblPopStatus.Text = ""
                                    lblPopStatus1.Text = ""
                                End If
                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SERVICE_TYPE")) Then
                                    If Not ds.Tables(0).Rows(0).Item("SERVICE_TYPE").ToString() = "" Then
                                        lblPopPancartType.Text = GV.parseString(ds.Tables(0).Rows(0).Item("SERVICE_TYPE").ToString())
                                    Else
                                        lblPopPancartType.Text = ""
                                    End If
                                Else
                                    lblPopPancartType.Text = ""
                                End If

                                lblInword.Text = GV.FL.ConvertAmountInWordFormat(lblPopTransationAmt1.Text)
                            End If
                        End If
                        ImagepopUp.ImageUrl = GV.FL.AddInVar("Companylogo", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ").Trim.Replace("..", siteName)
                        lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                    ElseIf (Request.QueryString("Type").Trim).ToUpper = "POSTPAID".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "BROADBAND".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "ELECTRICITY".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "WATERBILL".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "MOBILE".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "DTH".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "Gas".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "Landline".Trim.ToUpper Then

                        Div_Recharge.Visible = True
                        Dim abc As String = "  Select  RID as 'SrNo',(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks',Recharge_ServiceType from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where  API_TransId='" & Request.QueryString("ID").Trim & "' and Recharge_ServiceType='" & GV.Decode_Url_String(Request.QueryString("Type").Trim) & "' order by rid desc  "
                        ds = New DataSet
                        ds = GV.FL.OpenDsWithSelectQuery(abc)
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DATE")) Then
                                    If Not ds.Tables(0).Rows(0).Item("DATE").ToString() = "" Then
                                        lblPopDateTime_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("DATE").ToString()) & " " & GV.parseString(ds.Tables(0).Rows(0).Item("TIME").ToString())

                                    Else
                                        lblPopDateTime_Recharge.Text = ""
                                    End If
                                Else
                                    lblPopDateTime_Recharge.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TRANS_NO")) Then
                                    If Not ds.Tables(0).Rows(0).Item("TRANS_NO").ToString() = "" Then
                                        lblTransactionNo_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("TRANS_NO").ToString())
                                        lblTransation1_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("TRANS_NO").ToString())
                                    Else
                                        lblTransation1_Recharge.Text = ""
                                        lblTransactionNo_Recharge.Text = ""
                                    End If
                                Else
                                    lblTransation1_Recharge.Text = ""
                                    lblTransactionNo_Recharge.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AMOUNT")) Then
                                    If Not ds.Tables(0).Rows(0).Item("AMOUNT").ToString() = "" Then
                                        lblAmt_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AMOUNT").ToString())
                                        lblFinalAmount_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AMOUNT").ToString())
                                    Else
                                        lblFinalAmount_Recharge.Text = ""
                                        lblAmt_Recharge.Text = ""
                                    End If
                                Else
                                    lblFinalAmount_Recharge.Text = ""
                                    lblAmt_Recharge.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("STATUS")) Then
                                    If Not ds.Tables(0).Rows(0).Item("STATUS").ToString() = "" Then
                                        lblStatus_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("STATUS").ToString())
                                        lblTStatus_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("STATUS").ToString())
                                    Else
                                        lblTStatus_Recharge.Text = ""
                                        lblStatus_Recharge.Text = ""
                                    End If
                                Else
                                    lblTStatus_Recharge.Text = ""
                                    lblStatus_Recharge.Text = ""
                                End If


                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Operator")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Operator").ToString() = "" Then
                                        lblType_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Operator").ToString())
                                    Else
                                        lblType_Recharge.Text = ""
                                    End If
                                Else
                                    lblType_Recharge.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Recharge_ServiceType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Recharge_ServiceType").ToString() = "" Then
                                        lblMode_Recharge.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Recharge_ServiceType").ToString())
                                    Else
                                        lblMode_Recharge.Text = ""
                                    End If
                                Else
                                    lblMode_Recharge.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CANo")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CANo").ToString() = "" Then
                                        lblCustomer_Mobileno.Text = GV.parseString(ds.Tables(0).Rows(0).Item("CANo").ToString())

                                    Else
                                        lblCustomer_Mobileno.Text = ""
                                    End If
                                Else
                                    lblCustomer_Mobileno.Text = ""
                                End If

                                lbltotalvaluewrd_Recharge.Text = GV.FL.ConvertAmountInWordFormat(lblFinalAmount_Recharge.Text)
                            End If
                        End If
                        Img_Recharge.ImageUrl = GV.FL.AddInVar("Companylogo", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ").Trim.Replace("..", siteName)
                        lblshopname_Recharge.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")


                    ElseIf (Request.QueryString("Type").Trim).ToUpper = "IMPS".Trim.ToUpper Or (Request.QueryString("Type").Trim).ToUpper = "NEFT".Trim.ToUpper Then

                        Div_MoneyTransfer.Visible = True
                        Dim abc As String = "  Select top 1 RID as 'SrNo',(CONVERT(VARCHAR(11),TransferDate,106)) as 'Date',Receipent,CONVERT(varchar(15),CAST(RecordDateTime AS TIME),100) as Time,OrderNo as 'OrderNo',RefrenceNo as 'RefrenceNo',TranscationId as 'TransId',CustomerID as 'CustomerID',MobileNo,Amount,BankName as 'BankName',Method,Process,APIMessage as 'Message',AccountNo from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MoneyTransfer_API where TranscationId='" & Request.QueryString("ID").Trim & "' and Method='" & GV.Decode_Url_String(Request.QueryString("Type").Trim) & "' order by rid desc  "
                        ds = New DataSet
                        ds = GV.FL.OpenDsWithSelectQuery(abc)
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DATE")) Then
                                    If Not ds.Tables(0).Rows(0).Item("DATE").ToString() = "" Then
                                        lblDate_Money.Text = GV.parseString(ds.Tables(0).Rows(0).Item("DATE").ToString()) & " " & GV.parseString(ds.Tables(0).Rows(0).Item("TIME").ToString())
                                    Else
                                        lblDate_Money.Text = ""
                                    End If
                                Else
                                    lblDate_Money.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TransId")) Then
                                    If Not ds.Tables(0).Rows(0).Item("TransId").ToString() = "" Then
                                        lblTransactionId_Money.Text = GV.parseString(ds.Tables(0).Rows(0).Item("TransId").ToString())
                                    Else
                                        lblTransactionId_Money.Text = ""
                                    End If
                                Else
                                    lblTransactionId_Money.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CustomerID")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CustomerID").ToString() = "" Then
                                        lblCustomerMobileNo_Money.Text = GV.parseString(ds.Tables(0).Rows(0).Item("CustomerID").ToString())
                                    Else
                                        lblCustomerMobileNo_Money.Text = "N/A"
                                    End If
                                Else
                                    lblCustomerMobileNo_Money.Text = "N/A"
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MobileNo")) Then
                                    If Not ds.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                                        lblMobileNo_Money.Text = GV.parseString(ds.Tables(0).Rows(0).Item("MobileNo").ToString())
                                    Else
                                        lblMobileNo_Money.Text = "N/A"
                                    End If
                                Else
                                    lblMobileNo_Money.Text = "N/A"
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AccountNo")) Then
                                    If Not ds.Tables(0).Rows(0).Item("AccountNo").ToString() = "" Then
                                        lblAccountNo_Money.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AccountNo").ToString())
                                    Else
                                        lblAccountNo_Money.Text = ""
                                    End If
                                Else
                                    lblAccountNo_Money.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Method")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Method").ToString() = "" Then
                                        lblMode_Money.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Method").ToString())

                                    Else
                                        lblMode_Money.Text = ""
                                    End If
                                Else
                                    lblMode_Money.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("BankName")) Then
                                    If Not ds.Tables(0).Rows(0).Item("BankName").ToString() = "" Then
                                        lblBankName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("BankName").ToString())
                                    Else
                                        lblBankName.Text = ""
                                    End If
                                Else
                                    lblBankName.Text = ""
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Receipent")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Receipent").ToString() = "" Then
                                        lblBeneficiaryName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Receipent").ToString())
                                    Else
                                        lblBeneficiaryName.Text = ""
                                    End If
                                Else
                                    lblBeneficiaryName.Text = ""
                                End If


                                Dim abc1 As String = "  Select RID as 'SrNo',MobileNo,Amount,BankName as 'BankName',OrderNo,Method,Process,APIMessage as 'Message',TranscationId as 'TransId',AccountNo from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MoneyTransfer_API where  TranscationId='" & Request.QueryString("ID").Trim & "' and Method='" & GV.Decode_Url_String(Request.QueryString("Type").Trim) & "' order by rid desc  "
                                Dim DS1 As New DataSet
                                DS1 = New DataSet
                                DS1 = GV.FL.OpenDsWithSelectQuery(abc1)
                                ListView1.DataSource = DS1
                                ListView1.DataBind()
                                Dim FinalAmt As Decimal = 0
                                If DS1.Tables.Count > 0 Then
                                    If DS1.Tables(0).Rows.Count > 0 Then

                                        For i As Integer = 0 To DS1.Tables(0).Rows.Count - 1
                                            Dim lblUTR As Label = DirectCast(ListView1.Items(i).FindControl("lblUTR"), Label)
                                            Dim lblStatus As Label = DirectCast(ListView1.Items(i).FindControl("lblStatus"), Label)
                                            Dim lblAmount As Label = DirectCast(ListView1.Items(i).FindControl("lblAmount"), Label)

                                            If Not IsDBNull(DS1.Tables(0).Rows(i).Item("OrderNo")) Then
                                                If Not DS1.Tables(0).Rows(i).Item("OrderNo").ToString() = "" Then
                                                    lblUTR.Text = GV.parseString(DS1.Tables(0).Rows(i).Item("OrderNo").ToString())
                                                Else
                                                    lblUTR.Text = ""
                                                End If
                                            Else
                                                lblUTR.Text = ""
                                            End If

                                            If Not IsDBNull(DS1.Tables(0).Rows(i).Item("Message")) Then
                                                If Not DS1.Tables(0).Rows(i).Item("Message").ToString() = "" Then
                                                    lblStatus.Text = GV.parseString(DS1.Tables(0).Rows(i).Item("Message").ToString())
                                                Else
                                                    lblStatus.Text = ""
                                                End If
                                            Else
                                                lblStatus.Text = ""
                                            End If

                                            If Not IsDBNull(DS1.Tables(0).Rows(i).Item("Amount")) Then
                                                If Not DS1.Tables(0).Rows(i).Item("Amount").ToString() = "" Then
                                                    lblAmount.Text = GV.parseString(DS1.Tables(0).Rows(i).Item("Amount").ToString())
                                                Else
                                                    lblAmount.Text = ""
                                                End If
                                            Else
                                                lblAmount.Text = ""
                                            End If
                                            FinalAmt += CDec(lblAmount.Text.Trim)
                                        Next
                                    End If
                                End If
                                lblFinalAmount_Money.Text = FinalAmt
                                lbltotalvaluewrd_Money.Text = GV.FL.ConvertAmountInWordFormat(FinalAmt)
                            End If
                        End If

                        Image1.ImageUrl = GV.FL.AddInVar("Companylogo", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ").Replace("..", siteName)


                        Dim info() As String = GV.FL.AddInVar("isnull(AgencyName,'')+':'+ isnull(MobileNo,'') +':'+ isnull(OfficeAddress,'')", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'").ToString.Split(":")


                        lblshopname_Money.Text = info(0)
                        lblMobileNo_Money.Text = info(1)
                        lblshopname_Address.Text = info(2)
                        ' lblshopname_Money.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                    ElseIf (Request.QueryString("Type").Trim).ToUpper = "AEPS".Trim.ToUpper Then
                        '///
                        div_aeps.Visible = True

                        Dim abc As String = " Select  *,(CONVERT(VARCHAR(11),RecordDateTime,106)) as 'Date',CONVERT(varchar(15),CAST(RecordDateTime AS TIME),100) as Time from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_AEPS_API where RID=" & Request.QueryString("ID").Trim & ""
                        ds = New DataSet
                        ds = GV.FL.OpenDsWithSelectQuery(abc)
                        If Not ds Is Nothing Then
                            If ds.Tables.Count > 0 Then
                                If ds.Tables(0).Rows.Count > 0 Then

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("API_Call_Type")) Then
                                        If Not ds.Tables(0).Rows(0).Item("API_Call_Type").ToString() = "" Then
                                            lbl_AEPS_API_TYPE.Text = GV.parseString(ds.Tables(0).Rows(0).Item("API_Call_Type").ToString())

                                        Else
                                            lbl_AEPS_API_TYPE.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_API_TYPE.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DATE")) Then
                                        If Not ds.Tables(0).Rows(0).Item("DATE").ToString() = "" Then
                                            lbl_AEPS_TransDate.Text = GV.parseString(ds.Tables(0).Rows(0).Item("DATE").ToString()) & " " & GV.parseString(ds.Tables(0).Rows(0).Item("TIME").ToString())
                                        Else
                                            lbl_AEPS_TransDate.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_TransDate.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Latitude")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Latitude").ToString() = "" Then
                                            lbl_AEPS_LAT_LONG.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Latitude").ToString()) & " / " & GV.parseString(ds.Tables(0).Rows(0).Item("Longitude").ToString())
                                        Else
                                            lbl_AEPS_LAT_LONG.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_LAT_LONG.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Aadhar_Last_No")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Aadhar_Last_No").ToString() = "" Then
                                            lbl_AEPS_AADHAR_LASTNO.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Aadhar_Last_No").ToString())
                                        Else
                                            lbl_AEPS_AADHAR_LASTNO.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_AADHAR_LASTNO.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Remarks")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Remarks").ToString() = "" Then
                                            lbl_AEPS_Remarks.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Remarks").ToString())
                                        Else
                                            lbl_AEPS_Remarks.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_Remarks.Text = ""
                                    End If
                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Bank_Name")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Bank_Name").ToString() = "" Then
                                            lbl_AEPS_BANK_IIN.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Bank_Name").ToString()).ToUpper & " / " & GV.parseString(ds.Tables(0).Rows(0).Item("Bank_iin").ToString())
                                        Else
                                            lbl_AEPS_BANK_IIN.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_BANK_IIN.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Mobile_No")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Mobile_No").ToString() = "" Then
                                            lbl_AEPS_MobileNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Mobile_No").ToString())
                                        Else
                                            lbl_AEPS_MobileNo.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_MobileNo.Text = ""
                                    End If



                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Cash_Withdraw")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Cash_Withdraw").ToString() = "" Then
                                            lbl_AEPS_CashWithdraw.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Cash_Withdraw").ToString())
                                        Else
                                            lbl_AEPS_CashWithdraw.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_CashWithdraw.Text = ""
                                    End If

                                    lbl_AEPS_CashWithdraw_Total.Text = lbl_AEPS_CashWithdraw.Text


                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Access_Mode")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Access_Mode").ToString() = "" Then
                                            lbl_AEPS_ACCESS_MODE.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Access_Mode").ToString())
                                        Else
                                            lbl_AEPS_ACCESS_MODE.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_ACCESS_MODE.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Trans_ID")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Trans_ID").ToString() = "" Then
                                            lbl_AEPS_TransID.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Trans_ID").ToString())
                                        Else
                                            lbl_AEPS_TransID.Text = ""
                                        End If
                                    Else
                                        lbl_AEPS_TransID.Text = ""
                                    End If


                                    lbl_AEPS_Amt_InWords.Text = GV.FL.ConvertAmountInWordFormat(lbl_AEPS_CashWithdraw.Text.Trim)
                                End If
                            End If
                        End If

                        imgAEPS.ImageUrl = GV.FL.AddInVar("Companylogo", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ").Trim.Replace("..", siteName)
                        lbl_AEPS_AgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")


                    End If

                Else

                End If
            Else

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


End Class