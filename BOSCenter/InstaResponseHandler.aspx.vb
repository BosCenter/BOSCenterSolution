Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports InstamojoAPI


Public Class InstaResponseHandler
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim CashbackQry As String = ""


    Dim DS As New DataSet
    'BosTest10
    Dim Request_Transaction_Id, Request_name, Request_email, Request_phone, Request_amount, Request_redirect_url, Request_CompanyCode, Request_Purpose, Request_AgentID, Request_TransID As String
    Dim Response_DateTime, Response_Payment_Id, Response_Payment_Status, Response_Id, Response_Transaction_Id, Response_Action_Taken As String

    '//// ----------------Variable Declaration  ----------------

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                If Not Request.QueryString("transaction_id") Is Nothing Then
                    If Not Request.QueryString("transaction_id").Trim = "" Then

                        If Not Request.QueryString("payment_status") Is Nothing Then
                            If Not Request.QueryString("payment_status").Trim = "" Then
                                Response_Payment_Status = Request.QueryString("payment_status").ToString
                            End If
                        End If

                        Response_Transaction_Id = Request.QueryString("transaction_id").ToString

                        'CMP1045_WA_RTE-101_21063
                        Dim pp As String() = Response_Transaction_Id.Split("_")
                        lblCompanyCode.Text = pp(0)
                        lblPurpose.Text = pp(1)
                        lblRegistrationID.Text = pp(2)
                        lblTransID.Text = pp(3)


                        Dim DBName As String = GV.FL_AdminLogin.AddInVar("DatabaseName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & lblCompanyCode.Text & "' and [Status]='Active' ")
                        If DBName.Trim = "" Then
                            'Invalid Company Code
                            Exit Sub
                        Else
                            lblDBName.Text = DBName.Trim
                        End If

                        If Response_Payment_Status.Trim.ToUpper = "CREDIT" Then
                            'Case If Success
                            Response_Transaction_Id = Request.QueryString("transaction_id").ToString
                            If GV.FL.RecCount("" & lblDBName.Text.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_Transaction_Id='" & Response_Transaction_Id & "' and Request_Purpose='CC' and (Response_Action_Taken='' or Response_Action_Taken is NULL)") > 0 Then

                                If Not Request.QueryString("payment_id") Is Nothing Then
                                    If Not Request.QueryString("payment_id").Trim = "" Then
                                        Response_Payment_Id = Request.QueryString("payment_id").ToString
                                    End If
                                End If

                                If Not Request.QueryString("id") Is Nothing Then
                                    If Not Request.QueryString("id").Trim = "" Then
                                        Response_Id = Request.QueryString("id").ToString
                                    End If
                                End If


                                Try
                                    Dim VTransferFromMsg As String = ""
                                    Dim VTransferToMsg As String = ""
                                    Dim SMSMeassgeTo As String = ""
                                    Dim SMSMeassgeFrom As String = ""
                                    Dim AmouontType As String = ""
                                    Dim VTransferTo As String = ""
                                    Dim VTransferFrom As String = "Admin"
                                    Dim VAmtTransTransID As String = ""
                                    Dim VTransferAmt As String = "0"
                                    Dim VReference_Id As String = ""
                                    Dim VReference_Type As String = ""
                                    Dim VRef_Plan_Code As String = ""

                                    DS = GV.FL.OpenDs("" & lblDBName.Text.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_Transaction_Id='" & Response_Transaction_Id & "' ")
                                    If Not DS Is Nothing Then
                                        If DS.Tables.Count > 0 Then
                                            If DS.Tables(0).Rows.Count > 0 Then

                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Request_TransID")) Then
                                                    If Not DS.Tables(0).Rows(0).Item("Request_TransID").ToString() = "" Then
                                                        VAmtTransTransID = GV.parseString(DS.Tables(0).Rows(0).Item("Request_TransID").ToString())
                                                    Else
                                                        VAmtTransTransID = ""
                                                    End If
                                                Else
                                                    VAmtTransTransID = ""
                                                End If

                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Request_AgentID")) Then
                                                    If Not DS.Tables(0).Rows(0).Item("Request_AgentID").ToString() = "" Then
                                                        VTransferTo = GV.parseString(DS.Tables(0).Rows(0).Item("Request_AgentID").ToString())
                                                    Else
                                                        VTransferTo = ""
                                                    End If
                                                Else
                                                    VTransferTo = ""
                                                End If

                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Reference_Id")) Then
                                                    If Not DS.Tables(0).Rows(0).Item("Reference_Id").ToString() = "" Then
                                                        VReference_Id = GV.parseString(DS.Tables(0).Rows(0).Item("Reference_Id").ToString())
                                                    Else
                                                        VReference_Id = ""
                                                    End If
                                                Else
                                                    VReference_Id = ""
                                                End If

                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Reference_Type")) Then
                                                    If Not DS.Tables(0).Rows(0).Item("Reference_Type").ToString() = "" Then
                                                        VReference_Type = GV.parseString(DS.Tables(0).Rows(0).Item("Reference_Type").ToString())
                                                    Else
                                                        VReference_Type = ""
                                                    End If
                                                Else
                                                    VReference_Type = ""
                                                End If


                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Ref_Plan_Code")) Then
                                                    If Not DS.Tables(0).Rows(0).Item("Ref_Plan_Code").ToString() = "" Then
                                                        VRef_Plan_Code = GV.parseString(DS.Tables(0).Rows(0).Item("Ref_Plan_Code").ToString())
                                                    Else
                                                        VRef_Plan_Code = ""
                                                    End If
                                                Else
                                                    VRef_Plan_Code = ""
                                                End If

                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Request_amount")) Then
                                                    If Not DS.Tables(0).Rows(0).Item("Request_amount").ToString() = "" Then
                                                        VTransferAmt = GV.parseString(DS.Tables(0).Rows(0).Item("Request_amount").ToString())
                                                    Else
                                                        VTransferAmt = "0"
                                                    End If
                                                Else
                                                    VTransferAmt = "0"
                                                End If


                                            End If
                                        End If
                                    End If




                                    AmouontType = "Deposit"

                                    VTransferFromMsg = "Your Wallet is Debited by Distributor (" & VTransferTo & ")"
                                    VTransferToMsg = "Your Wallet is Credited by BOS CENTER PVT LTD"
                                    SMSMeassgeTo = "Your Wallet is Credited With Rs. " & VTransferAmt & " By BOS CENTER PVT LTD"



                                    '/////Cashback Calculations - start
                                    Dim VCommissionType_ColName, VCommission_ColName As String
                                    Dim CashBackComm As Decimal = 0


                                    If VReference_Type.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                                        VCommissionType_ColName = "Dis_CommissionType"
                                        VCommission_ColName = "Dis_Commission"
                                    ElseIf VReference_Type.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                                        VCommissionType_ColName = "Sub_Dis_CommissionType"
                                        VCommission_ColName = "Sub_Dis_Commission"
                                    ElseIf VReference_Type.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                                        VCommissionType_ColName = "Retailer_CommissionType"
                                        VCommission_ColName = "Retailer_Commission"
                                    ElseIf VReference_Type.Trim.ToUpper = "Customer".Trim.ToUpper Then
                                        VCommissionType_ColName = "Customer_CommissionType"
                                        VCommission_ColName = "Customer_Commission"
                                    Else
                                        VCommissionType_ColName = "Customer_CommissionType"
                                        VCommission_ColName = "Customer_Commission"
                                    End If

                                    DS = New DataSet
                                    DS = GV.FL.OpenDsWithSelectQuery("select * from " & lblDBName.Text.Trim & ".dbo.BOS_Ref_Code_Master where Ref_Code='" & GV.parseString(VRef_Plan_Code) & "'")
                                    If Not DS Is Nothing Then
                                        If DS.Tables.Count > 0 Then
                                            If DS.Tables(0).Rows.Count > 0 Then
                                                Dim VCommissionType, VCommission As String
                                                VCommissionType = ""
                                                VCommission = "0"

                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item(VCommissionType_ColName)) Then
                                                    If Not DS.Tables(0).Rows(0).Item(VCommissionType_ColName).ToString() = "" Then
                                                        VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item(VCommissionType_ColName).ToString())
                                                    End If
                                                End If

                                                If Not IsDBNull(DS.Tables(0).Rows(0).Item(VCommission_ColName)) Then
                                                    If Not DS.Tables(0).Rows(0).Item(VCommission_ColName).ToString() = "" Then
                                                        VCommission = GV.parseString(DS.Tables(0).Rows(0).Item(VCommission_ColName).ToString())
                                                    End If
                                                End If

                                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                    CashBackComm = Math.Round(((VTransferAmt * VCommission) / 100), 2)
                                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                                    CashBackComm = (VCommission)
                                                End If

                                            End If
                                        End If
                                    End If


                                    If CashBackComm > 0 Then

                                        Dim DistypecommTo As String = "Your Account is credited by Cashback " & CashBackComm & " Rs. Due to CreateCustomer on RegID " & lblRegistrationID.Text.Trim & " / AMT " & VTransferAmt
                                        Dim Distypecommfrom As String = "Your Account is debited by Cashback " & CashBackComm & " Rs. Due to CreateCustomer on RegID " & lblRegistrationID.Text.Trim & " / AMT " & VTransferAmt & " / By ID " & VReference_Id & "."


                                        Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt, CashBComm As Decimal
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0
                                        CashBComm = 0


                                        V_Actual_Commission_Amt = CashBackComm
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        CashBComm = V_Net_Commission_Amt

                                        CashbackQry = "insert into " & lblDBName.Text.Trim & ".dbo.BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values(  '" & lblTransID.Text.Trim & "', '" & VTransferAmt & "','" & GV.parseString(lblTransID.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','Cashback','" & Now.Date & "','Admin','" & VReference_Id & "','" & CashBComm & "',getdate(),'Admin',getdate() ) ;"

                                    End If

                                    '/////Cashback Calculations - End






                                    If Not GV.FL.RecCount(" " & lblDBName.Text.Trim & ".dbo.BOS_TransferAmountToAgents Where Amt_Transfer_TransID='" & VAmtTransTransID & "' ") > 0 Then 'Change where condition according to Criteria 

                                        Dim QryStr As String = "insert into " & lblDBName.Text.Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & VAmtTransTransID & "','" & VAmtTransTransID & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & AmouontType & "','" & "Add By Wallet - Gateway" & "',getdate(),'" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"
                                        QryStr = QryStr & " update " & lblDBName.Text.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details set  Response_DateTime=getdate(), Response_Payment_Id='" & Response_Payment_Id & "', Response_Payment_Status='" & Response_Payment_Status & "', Response_Id='" & Response_Id & "', Response_Transaction_Id='" & Response_Transaction_Id & "', Response_Action_Taken='Yes'  where Request_Transaction_Id='" & Response_Transaction_Id & "'; "
                                        QryStr = QryStr & " " & CashbackQry

                                        If GV.FL.DMLQueries(QryStr) = True Then
                                            'Dim FromMobile As String = GV.FL.AddInVar("MobileNo", " " & lblDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferFrom & "' ")
                                            'Dim ToMobile As String = GV.FL.AddInVar("MobileNo", " " & lblDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")
                                            'GV.sendSMSThroughAPI(FromMobile, SMSMeassgeFrom)
                                            'GV.sendSMSThroughAPI(ToMobile, SMSMeassgeTo)

                                            formheading3.Text = ":: Payment Status Details :: "

                                            lblError.Text = "Amount Added To Your Wallet Successfully."
                                            lblError.CssClass = "Successlabels"

                                            lblOtherInfo.Text = "Your Payment ID : " & Response_Payment_Id
                                            lblOtherInfo.Text = lblOtherInfo.Text & "<br /><br />Transaction Amount : " & VTransferAmt
                                            lblOtherInfo.Text = lblOtherInfo.Text & "<br /><br />Bos Transaction ID : " & Response_Transaction_Id
                                            lblOtherInfo.ForeColor = Color.Blue
                                            lblOtherInfo.Font.Bold = True



                                            btnRedirect.Visible = True

                                        Else
                                            formheading3.Text = ":: Payment Status Details :: "

                                            lblError.Text = "Wallet Updation Failed."
                                            lblError.CssClass = "errorlabels"
                                            lblOtherInfo.Text = ""
                                            btnRedirect.Visible = True

                                        End If
                                    Else
                                        formheading3.Text = ":: Payment Status Details :: "

                                        lblError.Text = "Invalid Transaction ID, Please Try Again."
                                        lblOtherInfo.Text = ""

                                        lblError.CssClass = "errorlabels"
                                        btnRedirect.Visible = True
                                    End If
                                Catch ex As Exception
                                    GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
                                    formheading3.Text = ":: Payment Status Details :: "

                                    lblError.Text = ex.Message
                                    lblOtherInfo.Text = ""

                                    lblError.CssClass = "errorlabels"
                                    btnRedirect.Visible = True
                                End Try


                            Else
                                formheading3.Text = ":: Payment Status Details :: "

                                lblError.Text = "Invalid Transaction ID."
                                lblOtherInfo.Text = ""

                                lblError.CssClass = "errorlabels"
                                btnRedirect.Visible = True

                            End If
                        Else
                            'Case If Failed

                            If Not Request.QueryString("payment_id") Is Nothing Then
                                If Not Request.QueryString("payment_id").Trim = "" Then
                                    Response_Payment_Id = Request.QueryString("payment_id").ToString
                                End If
                            End If

                            formheading3.Text = ":: Payment Status Details :: "

                            lblError.Text = "Your Transaction Is Failed. Please Try Again."
                            lblOtherInfo.Text = "Your Payment ID Is : " & Response_Payment_Id
                            lblOtherInfo.ForeColor = Color.Blue
                            lblOtherInfo.Font.Bold = True


                            lblError.CssClass = "errorlabels"
                            btnRedirect.Visible = True

                        End If


                    End If
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Private Sub btnRedirect_Click(sender As Object, e As System.EventArgs) Handles btnRedirect.Click
        Try
            lblOtherInfo.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""

            Response.Redirect("Default.aspx?admin=" & lblCompanyCode.Text) '/Change the name of form)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class