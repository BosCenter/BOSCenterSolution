Imports RestSharp
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq



Public Class BOS_PanCard
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VAmount, VcoupanType, VTotalCoupan, VRemarks, VTotalAmount, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

  

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddlCoupanType.SelectedIndex = 0
            ddlCoupanType_SelectedIndexChanged(sender, e)
            Bind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ApiResult As String = ""
        Try
            
            lblError.Text = ""
            lblError.CssClass = ""

            '///// Start Check API  STATUS Super ADmin Level 

            Dim PANCardAPI_Status As String = ""
            PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! PAN CARD API Is Inactive At Company Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Super Admin LEvel 

            '///// Start Check API  STATUS System Settings 

            PANCardAPI_Status = ""
            PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! PAN CARD API Is Inactive At Admin Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            PANCardAPI_Status = ""
            PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 






            If ddlCoupanType.SelectedIndex = 0 Then
                lblError.Text = "Please select Coupan Type."
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VcoupanType = GV.parseString(ddlCoupanType.SelectedItem.Text)
            End If

            If Not txtAmount.Text.Trim = "" Then
                VAmount = GV.parseString(txtAmount.Text.Trim)
                If CInt(VAmount) > 0 Then
                Else
                    lblError.Text = "Amount Should be greater than 0."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            Else
                '  VAmount = "0"
                lblError.Text = "Please Enter Amount."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If Not txtTotalCoupan.Text.Trim = "" Then
                VTotalCoupan = GV.parseString(txtTotalCoupan.Text.Trim)
                If CInt(VTotalCoupan) > 0 Then
                Else
                    lblError.Text = "Coupan Should be greater than 0."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            Else
                'VTotalCoupan = "0"
                lblError.Text = "Please Enter Coupan."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If Not txtTotalAmt.Text.Trim = "" Then
                VTotalAmount = GV.parseString(txtTotalAmt.Text.Trim)
            Else
                lblError.Text = "Please Enter TotalAmount."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If Not txtRemarks.Text.Trim = "" Then
                VRemarks = GV.parseString(txtRemarks.Text.Trim)
            Else
                lblError.Text = "Please Enter Remarks."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            Dim VNetAmount As Decimal = 0
            If txtNetAmount.Text = "" Then
                VNetAmount = 0
            Else
                VNetAmount = GV.parseString(txtNetAmount.Text.Trim)
            End If

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If

            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
            Else
                lblError.Text = "You Have Insufficient Wallet Amount"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// Check For API Balance - Start //////
            If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                lblError.Text = "Insufficient API Balance."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// Check For API Balance - End //////



            If txtTransactionPin.Text = "" Then
                lblError.Text = "Please Enter Your Transaction Pin."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim TransPiNo As String = ""
            TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If TransPiNo.Trim = txtTransactionPin.Text.Trim Then
            Else
                lblError.Text = "Invalid transaction Pin"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"


            If btnSave.Text.Trim.ToUpper = "Proceed".Trim.ToUpper Then


                btnok.Text = "Yes"
                btnok.Visible = True
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Proceed ??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            Else

                btnok.Text = "Yes"
                btnok.Visible = True
                btnCancel.Attributes("Style") = ""
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Update??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            End If


        Catch ex As Exception
            lblError.Text = ApiResult
        End Try
    End Sub

    Private Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try

            If btnok.Text.Trim.ToUpper = "Ok".Trim.ToUpper Then
                Response.Redirect("BOS_panCard.aspx")
            End If

            Dim ApiResult As String = ""
            Try

                lblError.Text = ""
                lblError.CssClass = ""

                '///// Start Check API  STATUS Super ADmin Level 

                Dim PANCardAPI_Status As String = ""
                PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

                If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                    lblError.Text = "Sorry! PAN CARD API Is Inactive At Company Level, Contact to Administrator"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                '///// End Check API  STATUS Retailer Super Admin LEvel 

                '///// Start Check API  STATUS System Settings 

                PANCardAPI_Status = ""
                PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

                If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                    lblError.Text = "Sorry! PAN CARD API Is Inactive At Admin Level, Contact to Administrator"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                '///// End Check API  STATUS Retailer Level Settings 

                Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

                '///// Start Check API  STATUS System Settings 
                PANCardAPI_Status = ""
                PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

                If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                    lblError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                '///// End Check API  STATUS Retailer Level  Settings 



                If ddlCoupanType.SelectedIndex = 0 Then
                    lblError.Text = "Please select Coupan Type."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    VcoupanType = GV.parseString(ddlCoupanType.SelectedItem.Text)
                End If

                If Not txtAmount.Text.Trim = "" Then
                    VAmount = GV.parseString(txtAmount.Text.Trim)
                    If CInt(VAmount) > 0 Then
                    Else
                        lblError.Text = "Amount Should be greater than 0."
                        lblError.CssClass = "errorlabels"
                        Exit Sub
                    End If
                Else
                    '  VAmount = "0"
                    lblError.Text = "Please Enter Amount."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                If Not txtTotalCoupan.Text.Trim = "" Then
                    VTotalCoupan = GV.parseString(txtTotalCoupan.Text.Trim)
                    If CInt(VTotalCoupan) > 0 Then
                    Else
                        lblError.Text = "Coupan Should be greater than 0."
                        lblError.CssClass = "errorlabels"
                        Exit Sub
                    End If
                Else
                    'VTotalCoupan = "0"
                    lblError.Text = "Please Enter Coupan."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                If Not txtTotalAmt.Text.Trim = "" Then
                    VTotalAmount = GV.parseString(txtTotalAmt.Text.Trim)
                Else
                    lblError.Text = "Please Enter TotalAmount."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                If Not txtRemarks.Text.Trim = "" Then
                    VRemarks = GV.parseString(txtRemarks.Text.Trim)
                Else
                    lblError.Text = "Please Enter Remarks."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim VNetAmount As Decimal = 0
                If txtNetAmount.Text = "" Then
                    VNetAmount = 0
                Else
                    VNetAmount = GV.parseString(txtNetAmount.Text.Trim)
                End If

                'Dim holdAmt As String = ""
                'holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
                'If holdAmt.Trim = "" Then
                '    holdAmt = "0"
                'End If

                'If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
                'Else
                '    lblError.Text = "You Have Insufficient Wallet Amount"
                '    lblError.CssClass = "errorlabels"
                '    Exit Sub
                'End If

                ''///// Check For API Balance - Start //////
                'If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                '    lblError.Text = "Insufficient API Balance."
                '    lblError.CssClass = "errorlabels"
                '    Exit Sub
                'End If
                '///// Check For API Balance - End //////



                'If txtTransactionPin.Text = "" Then
                '    lblError.Text = "Please Enter Your Transaction Pin."
                '    lblError.CssClass = "errorlabels"
                '    Exit Sub
                'End If

                'Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                'Dim TransPiNo As String = ""
                'TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

                'If TransPiNo.Trim = txtTransactionPin.Text.Trim Then
                'Else
                '    lblError.Text = "Invalid transaction Pin"
                '    lblError.CssClass = "errorlabels"
                '    Exit Sub
                'End If


                VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                VUpdatedOn = "getdate()"
                System.Threading.Thread.Sleep(5000)
                If lblSessionFlag.Text = 0 Then

                   '/////// API
                    Dim CtypeID As Integer = 0
                    If ddlCoupanType.SelectedItem.Text.Trim.ToUpper = "P Card".Trim.ToUpper Then
                        CtypeID = 2
                    ElseIf ddlCoupanType.SelectedItem.Text.Trim.ToUpper = "E Card".Trim.ToUpper Then
                        CtypeID = 1
                    End If

                    Dim APIRetailerID, APIRetailerMobile As String
                    APIRetailerID = ""
                    APIRetailerID = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    APIRetailerMobile = GV.FL.AddInVar("MobileNo", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & APIRetailerID & "'")
                    Dim RetailerDetailes As String = " RetailerID - " & APIRetailerID & " And RetailerMobileNo - " & APIRetailerMobile
                    Dim StrParameters As String = ""
                    Dim setParameter_API_Obj As New PAN_CARD_APPLY_API_Parameters()
                    setParameter_API_Obj.VCCType = CtypeID
                    setParameter_API_Obj.Qty = VTotalCoupan
                    setParameter_API_Obj.Key = APIKey
                    setParameter_API_Obj.Remarks = RetailerDetailes

                    StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)

                    StrParameters = StrParameters.Replace("VCCType", "Card_Type")
                    ApiResult = ReadbyRestClient_PANCARD(PAN_CARD_APPLY_API_URL, StrParameters)
                    Dim json1 As JObject = JObject.Parse(ApiResult)
                    Dim Vstatus As String = json1.SelectToken("status").ToString
                    Dim Vmessage As String = json1.SelectToken("message").ToString
                    Dim VRecord_DateTime As String = "getdate()"
                    '/////// END API

                    If Not Vstatus = "0" Then
                        lblError.Text = "Error At the end of API, Please try again later"
                        lblError.CssClass = "errorlabels"
                        Exit Sub
                    End If

                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Pan_Card_API (Refund_Status,TransIpAddress,API_TransId,AgentID,TransId,API_Status,API_Message,TotalAmount,CoupanType,Amount,TotalCoupan,Remarks,RecordDateTime,UpdatedBy,UpdatedOn) values('No','" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & RetailerID & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & Vstatus & "','" & Vmessage & "', " & VTotalAmount & ",'" & VcoupanType & "'," & VAmount & "," & VTotalCoupan & ",'" & VRemarks & "'," & VUpdatedOn & ",'" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        'Clear()
                        Dim TypeName As String = "PAN CARD"
                        Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                        If GRP = "Retailer".ToUpper Then
                            RechargeCommision()
                            If Not lblRID.Text = "" Then

                                Dim AAID() As String = lblRID.Text.Split("*")

                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim DisID_Com() As String = AAID(1).Split(":")
                                Dim SubDIsID_Com() As String = AAID(2).Split(":")
                                Dim RetailerID_Com() As String = AAID(3).Split(":")
                                Dim arrCanChange() As String = AAID(4).Split(":")

                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim DisID As String = DisID_Com(0)
                                Dim DisCom As String = DisID_Com(1)
                                Dim SUBDisID As String = SubDIsID_Com(0)
                                Dim SUBDisCom As String = SubDIsID_Com(1)
                                Dim RTEID As String = RetailerID_Com(0)
                                Dim RTECom As String = RetailerID_Com(1)

                                Dim vCanChange As String = arrCanChange(1)


                                If vCanChange.Trim.ToUpper = "YES" Then
                                    Dim typeAmtForm As String = "Your Account is debited by " & txtTotalAmt.Text.Trim & " Rs. Due to " & TypeName
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtTotalAmt.Text.Trim & " Rs. Due to " & TypeName

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."



                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "', '" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "', '" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// Retailer Commission Calculation - END
                                Else
                                    'VCanChange.Trim.ToUpper = "NO"

                                    Dim typeAmtForm As String = "Your Account is debited by " & txtTotalAmt.Text.Trim & " Rs. Due to " & TypeName
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtTotalAmt.Text.Trim & " Rs. Due to " & TypeName

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."



                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "', '" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "', '" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// Retailer Commission Calculation - END






                                End If


                                


                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                            End If
                        ElseIf GRP = "Customer".ToUpper Then
                            'In case of Customer 

                            RechargeCommision_Customer()
                            If Not lblRID.Text = "" Then

                                Dim AAID() As String = lblRID.Text.Split("*")

                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim CustID_Com() As String = AAID(1).Split(":")

                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim CustID As String = CustID_Com(0)
                                Dim CustCom As String = CustID_Com(1)

                                Dim typeAmtForm As String = "Your Account is debited by " & txtTotalAmt.Text.Trim & " Rs. Due to " & TypeName
                                Dim typeAmtTo As String = "Your Account is credited by " & txtTotalAmt.Text.Trim & " Rs. Due to " & TypeName

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                Dim CustTypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."

                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                Dim CustTypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."

                                QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & txtTotalAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0


                                '//// Customer Commission Calculation - Start
                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If CustCom > 0 Then
                                    V_Actual_Commission_Amt = CustCom
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    CustCom = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "', '" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CustTypecommTo & "','" & CustTypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                '//// Customer Commission Calculation - END


                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VTotalAmount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTotalAmount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                End If
                            End If
                        End If


                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        '//// Admin & Super Admin Commission Calculation - Start
                        If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                            '//// Admin Commission Calculation - Start
                            Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Comm_Result As String
                            Dim VCus_Amount, V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                            If GV.parseString(txtTotalAmt.Text.Trim) = "" Then
                                V_Amount = "0"
                            Else
                                V_Amount = txtTotalAmt.Text.Trim
                            End If
                            VCus_Amount = V_Amount

                            V_OperatorCategory = ""
                            V_OperatorCode = ""
                            V_APIName = "Recharge"
                            V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                            Comm_Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)

                            If Not GV.parseString(Comm_Result) = "" Then
                                Dim Result_Arry() As String = Comm_Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & "  / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If

                            '//// Admin Commission Calculation - End

                            '//// Super Admin Commission Calculation - Start
                            Comm_Result = GV.Commision_Calculation_For_SuperAdmin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName)

                            If Not GV.parseString(Comm_Result) = "" Then
                                Dim Result_Arry() As String = Comm_Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Super Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & "  / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & "  / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            '//// Super Admin Commission Calculation - End
                        End If
                        '//// Admin & Super Admin Commission Calculation - End
                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

                        If Not QryStr.Trim = "" Then
                            GV.FL.DMLQueriesBulk(QryStr)
                        End If

                        Bind()
                        Clear()

                        ddlCoupanType_SelectedIndexChanged(sender, e)
                        lblDialogMsg.Text = "Record Saved Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnok.Text = "Ok"
                        btnok.Visible = True
                        btnCancel.Attributes("style") = "display:none"
                        ModalPopupExtender1.Show()

                    Else
                        lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                        lblDialogMsg.CssClass = "errorlabels"
                        btnCancel.Text = "Ok"
                        btnok.Visible = False
                        ModalPopupExtender1.Show()
                    End If
                    'End If

                ElseIf lblSessionFlag.Text = 1 Then
                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Pan_Card_API set TotalAmount=" & VTotalAmount & ", TotalCoupan=" & VTotalCoupan & ",Remarks='" & VRemarks & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0

                        Clear()
                        Bind()
                        lblDialogMsg.Text = "Record Updated Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnCancel.Text = "Ok"
                        btnok.Visible = False
                        ModalPopupExtender1.Show()
                    Else
                        lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                        lblDialogMsg.CssClass = "errorlabels"
                        btnCancel.Text = "Ok"
                        btnok.Visible = False
                        ModalPopupExtender1.Show()
                    End If
                End If
                '   End If




            Catch ex As Exception
                lblError.Text = ApiResult
            End Try



        Catch ex As Exception
        End Try
    End Sub



    Public Sub Bind()
        Try
            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            DS = GV.FL.OpenDsWithSelectQuery("select RID as SrNo,(CONVERT(VARCHAR(11),RecordDateTime,106)) as 'PanDate',CONVERT(varchar(15),CAST(RecordDateTime AS TIME),100) as 'PanTime',* from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Pan_Card_API where AgentID='" & RetailerID & "' order by RID desc  ")
            GridView1.DataSource = DS.Tables(0)
            GridView1.DataBind()
            GV.FL.showSerialnoOnGridView(GridView1, 0)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            lblService.Text = ""
            txtNetAmount.Text = ""
            txtServiceCharge.Text = ""
            ddlCoupanType.SelectedIndex = 0
            txtAmount.Text = "0"
            txtTotalAmt.Text = "0"
            VUpdatedBy = ""
            VUpdatedOn = ""
            txtRemarks.Text = ""
            txtTotalCoupan.Text = "0"
            lblSessionFlag.Text = 0
            Calculation()
            btnSave.Text = "Proceed"
            lblError.Text = ""
            btnSave.Enabled = True
            lblError.CssClass = ""
            lblUpadate.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblWalletBal.Text = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
                lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                lblAgentType.Text = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                Bind()
                lblSessionFlag.Text = 0
                ddlCoupanType_SelectedIndexChanged(sender, e)
               

            End If

        Catch ex As Exception

        End Try
    End Sub

    

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
        End Try

    End Sub


    Protected Sub ddlCoupanType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCoupanType.SelectedIndexChanged
        Try
            txtAmount.Text = ddlCoupanType.SelectedValue.Trim
            Calculation()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtTotalCoupan_TextChanged(sender As Object, e As EventArgs) Handles txtTotalCoupan.TextChanged
        Try
            Calculation()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Calculation()
        Try
            Dim TotalAmt, CoupanNo, Amount As Long
            TotalAmt = 0
            CoupanNo = 0
            Amount = 0
            If txtAmount.Text = "" Then
                Amount = 1
            Else
                Amount = GV.parseString(txtAmount.Text.Trim)
            End If
            If txtTotalCoupan.Text.Trim = "" Then
                CoupanNo = 0
            Else
                CoupanNo = GV.parseString(txtTotalCoupan.Text.Trim)
            End If
            TotalAmt = CoupanNo * Amount
            txtTotalAmt.Text = TotalAmt
            Dim NetAmount As Decimal = 0
            Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='PanCard'").Split(":")
            If Service.Length > 1 Then
                If Service(1).Trim = "Percentage" Then
                    lblService.Text = Service(0) & " %"
                    NetAmount = (CDec(txtTotalAmt.Text.Trim) * CDec(Service(0))) / 100
                ElseIf Service(1).Trim = "Amount" Then
                    lblService.Text = Service(0)
                    NetAmount = CDec(Service(0))
                ElseIf Service(1).Trim = "Not Applicable" Then
                    lblService.Text = Service(0)
                    NetAmount = CDec(Service(0))
                End If
            End If
            txtServiceCharge.Text = NetAmount
            txtNetAmount.Text = CDec(GV.parseString(txtTotalAmt.Text.Trim)) + NetAmount
        Catch ex As Exception

        End Try
    End Sub


    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  PAN CARD API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    '///  PAN CARD API URL  - Start
    Dim PAN_CARD_APPLY_API_URL As String = "https://www.vacsc.com/PANAPI/APPLY"
    '///  PAN CARD API URL  - End

    Public Function ReadbyRestClient_PANCARD(Urls As String, Parameter As String) As String
        Dim str As String = ""
        Dim LogString As String = ""
        Dim API_Name, Trans_ID, Trans_DateTime, Request_String, Response_String, AgentID, AgentType As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        AgentID = ""
        AgentType = ""

        Dim strQry As String = ""


        Try
            lblTransId.Text = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(lblTransId.Text) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine
            API_Name = "PAN Card API"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text

            'Card_Type'
            Dim client = New RestClient(Urls)
            Dim request = New RestRequest(Method.POST)
            request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("content-type", "application/x-www-form-urlencoded")
            request.AddParameter("application/x-www-form-urlencoded", Parameter, ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim
            Response_String = GV.parseString(str)
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            GV.SaveTextToFile(LogString, Server.MapPath("PANCARDAPI_LOG.txt"), True)
        Catch ex As Exception
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            GV.SaveTextToFile(LogString, Server.MapPath("PANCARDAPI_LOG.txt"), True)
            Return str
        End Try

        Return str

    End Function

    Public Class PAN_CARD_APPLY_API_Parameters

        Dim VKey, VRemarks As String
        Dim VCType, VQty As Integer

        Public Property VCCType() As Integer
            Get
                Return VCType
            End Get
            Set(ByVal value As Integer)
                VCType = value
            End Set
        End Property

        Public Property Qty() As Integer
            Get
                Return VQty
            End Get
            Set(ByVal value As Integer)
                VQty = value
            End Set
        End Property


        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property


        Public Property Remarks() As String
            Get
                Return VRemarks
            End Get
            Set(ByVal value As String)
                VRemarks = value
            End Set
        End Property
    End Class
    

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  PAN CARD API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX






    Public Sub RechargeCommision()
        Try
            'Dim VadminPer, DistributorPer, SubDIsPer, VRetailer As Decimal

            Dim VCommissionType, VSub_Dis_CommissionType, VRetailer_CommissionType As String
            VCommissionType = ""
            VSub_Dis_CommissionType = ""
            VRetailer_CommissionType = ""
            Dim VCommission, VSub_Dis_Commission, VRetailer_Commission As Decimal
            VCommission = 0
            VSub_Dis_Commission = 0
            VRetailer_Commission = 0


            Dim VContainCategory, VCanChange As String
            VContainCategory = ""
            VCanChange = ""


            Dim VadminComAmt, DistributorComAmt, SubDIsComAmt, VRetailerComAmt As Decimal
            VadminComAmt = 0
            DistributorComAmt = 0
            SubDIsComAmt = 0
            VRetailerComAmt = 0



            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalDISAmt, VFinalSUBDISAmt, VFinalRETAILERAmt As Decimal
            Dim SubDisID As String = ""
            Dim DisID As String = ""
            Dim AdminID As String = ""
            Dim qry As String = ""
            SubDisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "'")
            DisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "'")
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='PanCard' and ActiveStatus='Active'"
            DS = New DataSet
            DS = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not DS.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(DS.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not DS.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(DS.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If



                        If VContainCategory.Trim.ToUpper = "YES" Then




                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                            Dim Amount1 As String = GV.parseString(txtTotalAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Commission").ToString())
                                End If
                            End If

                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                DistributorComAmt = (Amount * VCommission) / 100
                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                Dim CoupanQty As Long = 0
                                If txtTotalCoupan.Text.Trim = "" Then
                                    CoupanQty = 0
                                Else
                                    CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                End If
                                DistributorComAmt = (CoupanQty * VCommission)
                            End If

                            '/////// End Distributor



                            qry = " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='PanCard' and  RegistrationID in (select RefrenceID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                            qry = qry & " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='PanCard' and  RegistrationID in (select RefrenceID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




                            DS = New DataSet
                            DS = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not DS Is Nothing Then
                                If DS.Tables.Count > 0 Then
                                    If DS.Tables(0).Rows.Count > 0 Then


                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CommissionType")) Then
                                            If Not DS.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                                VSub_Dis_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Commission")) Then
                                            If Not DS.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                                VSub_Dis_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Commission").ToString())
                                            End If
                                        End If

                                        If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SubDIsComAmt = (Amount * VSub_Dis_Commission) / 100
                                        ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            Dim CoupanQty As Long = 0
                                            If txtTotalCoupan.Text.Trim = "" Then
                                                CoupanQty = 0
                                            Else
                                                CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                            End If
                                            SubDIsComAmt = (CoupanQty * VSub_Dis_Commission)
                                        End If

                                        '/////// End  Sub Distributor
                                    End If
                                    '/////// End  Sub Distributor

                                    If DS.Tables.Count > 1 Then
                                        If DS.Tables(1).Rows.Count > 0 Then

                                            If Not IsDBNull(DS.Tables(1).Rows(0).Item("CommissionType")) Then
                                                If Not DS.Tables(1).Rows(0).Item("CommissionType").ToString() = "" Then
                                                    VRetailer_CommissionType = GV.parseString(DS.Tables(1).Rows(0).Item("CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(DS.Tables(1).Rows(0).Item("Commission")) Then
                                                If Not DS.Tables(1).Rows(0).Item("Commission").ToString() = "" Then
                                                    VRetailer_Commission = GV.parseString(DS.Tables(1).Rows(0).Item("Commission").ToString())
                                                End If
                                            End If

                                            If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                VRetailerComAmt = (Amount * VRetailer_Commission) / 100
                                            ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                Dim CoupanQty As Long = 0
                                                If txtTotalCoupan.Text.Trim = "" Then
                                                    CoupanQty = 0
                                                Else
                                                    CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                                End If
                                                VRetailerComAmt = (CoupanQty * VRetailer_Commission)
                                            End If

                                            '/////// End  Retailer

                                        End If
                                    End If
                                    
                                    '/////// End  Retailer

                                End If
                            End If



                            VFinaladminAmt = VadminComAmt
                            VFinalDISAmt = DistributorComAmt
                            VFinalSUBDISAmt = SubDIsComAmt
                            VFinalRETAILERAmt = VRetailerComAmt
                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper



                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                            '/// NEED To CHANGE HERE EK

                            Dim Amount1 As String = GV.parseString(txtTotalAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Commission").ToString())
                                End If
                            End If

                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                DistributorComAmt = (Amount * VCommission) / 100
                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                Dim CoupanQty As Long = 0
                                If txtTotalCoupan.Text.Trim = "" Then
                                    CoupanQty = 0
                                Else
                                    CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                End If
                                DistributorComAmt = (CoupanQty * VCommission)
                            End If


                            '/////// End Distributor



                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                    VSub_Dis_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                    VSub_Dis_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                End If
                            End If

                            If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                SubDIsComAmt = (Amount * VSub_Dis_Commission) / 100
                            ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                Dim CoupanQty As Long = 0
                                If txtTotalCoupan.Text.Trim = "" Then
                                    CoupanQty = 0
                                Else
                                    CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                End If
                                SubDIsComAmt = (CoupanQty * VSub_Dis_Commission)
                            End If

                            '/////// End  Sub Distributor




                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                    VRetailer_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                    VRetailer_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                End If
                            End If

                            If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VRetailerComAmt = (Amount * VRetailer_Commission) / 100
                            ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                Dim CoupanQty As Long = 0
                                If txtTotalCoupan.Text.Trim = "" Then
                                    CoupanQty = 0
                                Else
                                    CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                End If
                                VRetailerComAmt = (CoupanQty * VRetailer_Commission)
                            End If

                            '/////// End  Retailer


                            VFinaladminAmt = VadminComAmt
                            VFinalDISAmt = DistributorComAmt
                            VFinalSUBDISAmt = SubDIsComAmt
                            VFinalRETAILERAmt = VRetailerComAmt
                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                        End If


                    End If
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub

    Public Sub RechargeCommision_Customer()
        Try
            'Dim VadminPer, DistributorPer, SubDIsPer, VRetailer As Decimal

            Dim VCommissionType, VCustomer_CommissionType As String
            VCommissionType = ""
            VCustomer_CommissionType = ""

            Dim VCommission, VCustomer_Commission As Decimal
            VCommission = 0
            VCustomer_Commission = 0


            Dim VContainCategory, VCanChange As String
            VContainCategory = ""
            VCanChange = ""


            Dim VadminComAmt, VCustomerComAmt As Decimal
            VadminComAmt = 0
            VCustomerComAmt = 0



            Dim CustomerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalCustomerAmt As Decimal
            Dim CustID As String = ""
            Dim AdminID As String = ""
            Dim qry As String = ""
          
            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='PanCard' and ActiveStatus='Active'"
            DS = New DataSet
            DS = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not DS.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(DS.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not DS.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(DS.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If



                        If VContainCategory.Trim.ToUpper = "YES" Then




                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                            Dim Amount1 As String = GV.parseString(txtTotalAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                End If
                            End If

                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VCustomerComAmt = (Amount * VCommission) / 100
                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                Dim CoupanQty As Long = 0
                                If txtTotalCoupan.Text.Trim = "" Then
                                    CoupanQty = 0
                                Else
                                    CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                End If
                                VCustomerComAmt = (CoupanQty * VCommission)
                            End If

                            '/////// End customer

                            VFinaladminAmt = VadminComAmt
                            VFinalCustomerAmt = VCustomerComAmt

                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                            '/// NEED To CHANGE HERE EK
                            Dim Amount1 As String = GV.parseString(txtTotalAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                End If
                            End If

                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VCustomerComAmt = (Amount * VCommission) / 100
                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                Dim CoupanQty As Long = 0
                                If txtTotalCoupan.Text.Trim = "" Then
                                    CoupanQty = 0
                                Else
                                    CoupanQty = GV.parseString(txtTotalCoupan.Text.Trim)
                                End If
                                VCustomerComAmt = (CoupanQty * VCommission)
                            End If


                            '/////// End Customer

                          VFinaladminAmt = VadminComAmt
                            VFinalCustomerAmt = VCustomerComAmt
                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper
                        End If


                    End If
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub
End Class