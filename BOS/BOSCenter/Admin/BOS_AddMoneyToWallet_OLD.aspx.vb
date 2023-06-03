Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports InstamojoAPI





Public Class BOS_AddMoneyToWallet_OLD
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
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

                        If Response_Payment_Status.Trim.ToUpper = "CREDIT" Then
                            'Case If Success
                            Response_Transaction_Id = Request.QueryString("transaction_id").ToString
                            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_Transaction_Id='" & Response_Transaction_Id & "' and Request_Purpose='WA' and (Response_Action_Taken='' or Response_Action_Taken is NULL)") > 0 Then

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


                                    ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_Transaction_Id='" & Response_Transaction_Id & "' ")
                                    If Not ds Is Nothing Then
                                        If ds.Tables.Count > 0 Then
                                            If ds.Tables(0).Rows.Count > 0 Then

                                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Request_TransID")) Then
                                                    If Not ds.Tables(0).Rows(0).Item("Request_TransID").ToString() = "" Then
                                                        VAmtTransTransID = GV.parseString(ds.Tables(0).Rows(0).Item("Request_TransID").ToString())
                                                    Else
                                                        VAmtTransTransID = ""
                                                    End If
                                                Else
                                                    VAmtTransTransID = ""
                                                End If

                                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Request_AgentID")) Then
                                                    If Not ds.Tables(0).Rows(0).Item("Request_AgentID").ToString() = "" Then
                                                        VTransferTo = GV.parseString(ds.Tables(0).Rows(0).Item("Request_AgentID").ToString())
                                                    Else
                                                        VTransferTo = ""
                                                    End If
                                                Else
                                                    VTransferTo = ""
                                                End If

                                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Request_amount")) Then
                                                    If Not ds.Tables(0).Rows(0).Item("Request_amount").ToString() = "" Then
                                                        VTransferAmt = GV.parseString(ds.Tables(0).Rows(0).Item("Request_amount").ToString())
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


                                    If Not GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents Where Amt_Transfer_TransID='" & VAmtTransTransID & "' ") > 0 Then 'Change where condition according to Criteria 

                                        Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & VAmtTransTransID & "','" & VAmtTransTransID & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & AmouontType & "','" & "Add By Wallet - Gateway" & "',getdate(),'" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"
                                        QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details set  Response_DateTime=getdate(), Response_Payment_Id='" & Response_Payment_Id & "', Response_Payment_Status='" & Response_Payment_Status & "', Response_Id='" & Response_Id & "', Response_Transaction_Id='" & Response_Transaction_Id & "', Response_Action_Taken='Yes'  where Request_Transaction_Id='" & Response_Transaction_Id & "'; "

                                        If GV.FL.DMLQueries(QryStr) = True Then
                                            Dim FromMobile As String = GV.FL.AddInVar("MobileNo", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferFrom & "' ")
                                            Dim ToMobile As String = GV.FL.AddInVar("MobileNo", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")
                                            'GV.sendSMSThroughAPI(FromMobile, SMSMeassgeFrom)
                                            GV.sendSMSThroughAPI(ToMobile, SMSMeassgeTo)

                                            formheading3.Text = ":: Payment Status Details :: "

                                            lblError.Text = "Amount Added To Your Wallet Successfully."
                                            lblError.CssClass = "Successlabels"

                                            lblOtherInfo.Text = "Your Payment ID : " & Response_Payment_Id
                                            lblOtherInfo.Text = lblOtherInfo.Text & "<br /><br />Transaction Amount : " & VTransferAmt
                                            lblOtherInfo.Text = lblOtherInfo.Text & "<br /><br />Bos Transaction ID : " & Response_Transaction_Id
                                            lblOtherInfo.ForeColor = Color.Blue
                                            lblOtherInfo.Font.Bold = True



                                            btnRedirect.Visible = True
                                            Wallet_Heading.Visible = False
                                            Wallet_Row.Visible = False

                                        Else
                                            formheading3.Text = ":: Payment Status Details :: "
                                            lblError.Text = "Wallet Updation Failed."
                                            lblError.CssClass = "errorlabels"
                                            lblOtherInfo.Text = ""
                                            btnRedirect.Visible = True
                                            Wallet_Heading.Visible = False
                                            Wallet_Row.Visible = False
                                        End If
                                    Else
                                        formheading3.Text = ":: Payment Status Details :: "

                                        lblError.Text = "Invalid Transaction ID, Please Try Again."
                                        lblOtherInfo.Text = ""

                                        lblError.CssClass = "errorlabels"
                                        btnRedirect.Visible = True
                                        Wallet_Heading.Visible = False
                                        Wallet_Row.Visible = False
                                    End If
                                Catch ex As Exception
                                    formheading3.Text = ":: Payment Status Details :: "

                                    lblError.Text = ex.Message
                                    lblOtherInfo.Text = ""

                                    lblError.CssClass = "errorlabels"
                                    btnRedirect.Visible = True
                                    Wallet_Heading.Visible = False
                                    Wallet_Row.Visible = False
                                End Try


                            Else
                                formheading3.Text = ":: Payment Status Details :: "

                                lblError.Text = "Invalid Transaction ID."
                                lblOtherInfo.Text = ""

                                lblError.CssClass = "errorlabels"
                                btnRedirect.Visible = True
                                Wallet_Heading.Visible = False
                                Wallet_Row.Visible = False

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
                            Wallet_Heading.Visible = False
                            Wallet_Row.Visible = False
                        End If


                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            txtAmount.Text = "0"
            lblError.Text = ""
            lblError.CssClass = ""

            btnRedirect.Visible = False
            lblOtherInfo.Text = ""


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnProceedToAddMoney_Click(sender As Object, e As EventArgs) Handles btnProceedToAddMoney.Click
        Try

            Dim VCompanyCode, VLoginID, VLoginGroup, VDatabaseName, VFirstName, VLastName, VEmailID, VMobileNo, VAmount, NewURL As String



            Request_name = ""
            Request_email = ""
            Request_phone = ""
            Request_amount = ""
            Request_redirect_url = ""
            Request_Purpose = ""
            Request_CompanyCode = ""
            Request_AgentID = ""
            Request_TransID = ""


            VMobileNo = ""
            VEmailID = ""
            VFirstName = ""
            VLastName = ""
            NewURL = ""
            VAmount = "0"

            lblError.Text = ""
            lblError.CssClass = ""

            If GV.parseString(txtAmount.Text) = "" Or GV.parseString(txtAmount.Text) = "0" Then
                lblError.Text = "Please Enter Amount"
                lblError.CssClass = "errorlabels"
                txtAmount.Focus()
                Exit Sub
            End If


            VLoginGroup = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            VLoginID = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VCompanyCode = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
            VDatabaseName = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response)

            Request_CompanyCode = VCompanyCode
            Request_AgentID = VLoginID



            ds = GV.FL.OpenDs("" & VDatabaseName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VLoginID.Trim & "' ")
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("FirstName")) Then
                            If Not ds.Tables(0).Rows(0).Item("FirstName").ToString() = "" Then
                                VFirstName = GV.parseString(ds.Tables(0).Rows(0).Item("FirstName").ToString())
                            Else
                                VFirstName = ""
                            End If
                        Else
                            VFirstName = ""
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("LastName")) Then
                            If Not ds.Tables(0).Rows(0).Item("LastName").ToString() = "" Then
                                VLastName = GV.parseString(ds.Tables(0).Rows(0).Item("LastName").ToString())
                            Else
                                VLastName = ""
                            End If
                        Else
                            VLastName = ""
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("MobileNo")) Then
                            If Not ds.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                                VMobileNo = GV.parseString(ds.Tables(0).Rows(0).Item("MobileNo").ToString())
                            Else
                                VMobileNo = ""
                            End If
                        Else
                            VMobileNo = ""
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("EmailID")) Then
                            If Not ds.Tables(0).Rows(0).Item("EmailID").ToString() = "" Then
                                VEmailID = GV.parseString(ds.Tables(0).Rows(0).Item("EmailID").ToString())
                            Else
                                VEmailID = ""
                            End If
                        Else
                            VEmailID = ""
                        End If


                    End If
                End If
            End If

            Dim objClass As Instamojo = InstamojoImplementation.getApi(client_id, client_secret, endpoint, auth_endpoint)



            Request_TransID = GV.FL_AdminLogin.getAutoNumber("TransId")


            Dim New_transId As String = VCompanyCode & "_" & "WA" & "_" & VLoginID & "_" & Request_TransID

            VAmount = CInt(GV.parseString(txtAmount.Text))


            Dim objPaymentRequest As New PaymentOrder


            Request_name = VFirstName.Trim & " " & VLastName.Trim
            Request_email = VEmailID
            Request_phone = VMobileNo
            Request_amount = VAmount
            Request_Transaction_Id = New_transId
            Request_Purpose = "WA"
            Request_redirect_url = "https://www.boscenter.in/Admin/BOS_AddMoneyToWallet.aspx"

            objPaymentRequest.name = Request_name
            objPaymentRequest.email = Request_email
            objPaymentRequest.phone = Request_phone
            objPaymentRequest.amount = Request_amount
            objPaymentRequest.transaction_id = Request_Transaction_Id
            objPaymentRequest.redirect_url = Request_redirect_url
            objPaymentRequest.description = "Add To Wallet"

            If objPaymentRequest.amountInvalid = True Then
                lblError.Text = "Amount Is Incorrect. Enter Valid Amount."
                lblError.CssClass = "errorlabels"
                txtAmount.Focus()
                Exit Sub
            End If

            If objPaymentRequest.emailInvalid = True Then
                lblError.Text = "Invalid EmailID, Please update EmailID"
                lblError.CssClass = "errorlabels"
                txtAmount.Focus()
                Exit Sub
            End If

            If objPaymentRequest.phoneInvalid = True Then
                lblError.Text = "Invalid Mobile No, Please update Mobile No."
                lblError.CssClass = "errorlabels"
                txtAmount.Focus()
                Exit Sub
            End If

            If objPaymentRequest.nameInvalid = True Then
                lblError.Text = "Invalid Name, Please update Name."
                lblError.CssClass = "errorlabels"
                txtAmount.Focus()
                Exit Sub
            End If

            If objPaymentRequest.transactionIdInvalid = True Then
                lblError.Text = "Invalid TransactionID, Please Retry Again."
                lblError.CssClass = "errorlabels"
                txtAmount.Focus()
                Exit Sub
            End If

            '//webhook is optional.
            'objPaymentRequest.webhook_url = "https://www.boscenter.in/InstaResponseHandler.aspx"

            Try

                Dim objPaymentResponse As CreatePaymentOrderResponse = objClass.createNewPaymentRequest(objPaymentRequest)
                NewURL = objPaymentResponse.payment_options.payment_url

            Catch ex As Exception
                lblError.Text = ex.Message & " : " & ex.StackTrace
                lblError.CssClass = "errorlabels"
            End Try

            If Not NewURL.Trim = "" Then
                Dim str As String = " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details (TransIpAddress,Request_DateTime,Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,Request_redirect_url,Request_CompanyCode,Request_Purpose,Request_AgentID,Request_TransID,UpdatedOn,UpdatedBy) values ('" & GV.parseString(GV.GetIPAddress) & "',getdate(),'" & Request_Transaction_Id & "','" & Request_name & "','" & Request_email & "','" & Request_phone & "','" & Request_amount & "','" & Request_redirect_url & "','" & Request_CompanyCode & "','" & Request_Purpose & "','" & Request_AgentID & "','" & Request_TransID & "',getdate(),'" & VLoginID & "')"
                GV.FL.DMLQueriesBulk(str)

                Response.Redirect(NewURL)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message & " : " & ex.StackTrace
            lblError.CssClass = "errorlabels"
        End Try



    End Sub

    Private Sub btnPopupYes_Click(sender As Object, e As System.EventArgs) Handles btnPopupYes.Click
        Try
            Response.Redirect("BOS_AddMoneyToWallet.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRedirect_Click(sender As Object, e As System.EventArgs) Handles btnRedirect.Click
        Try
            formheading3.Text = ":: Add Money Form :: "
            lblOtherInfo.Text = ""

            Wallet_Heading.Visible = True
            Wallet_Row.Visible = True
            lblError.Text = ""
            lblError.CssClass = ""
            btnRedirect.Visible = False

            Response.Redirect("BOS_AddMoneyToWallet.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class