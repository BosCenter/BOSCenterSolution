Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports InstamojoAPI
Imports System.Security.Cryptography

Public Class BOS_AddMoneyToWallet
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    Dim Request_Transaction_Id, Request_name, Request_email, Request_phone, Request_amount, Request_redirect_url, Request_CompanyCode, Request_Purpose, Request_AgentID, Request_TransID As String
    Dim Response_DateTime, Response_Payment_Id, Response_Payment_Status, Response_Id, Response_Transaction_Id, Response_Action_Taken As String

    '//// ----------------Variable Declaration  ----------------

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then


            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Public Sub goEasebuzzGateway(ByVal TransID As String, ByVal VCustomerEmailID As String, ByVal VPayCustomerID As String, ByVal VPayCartAmount As String, ByVal VPayCustomerName As String, ByVal VPayCustomerPhoneNo As String, ByVal VDeliveryRID As String, pg As Page)
        Try
            Dim txnID As String = TransID
            '/// create cookie start
            GV.set_OrderID_SessionVariables(TransID, Request, Response)
            '/// create cookie end
            'Public EaseBuzz_MERCHANT_KEY As String = "MEBKTJC7XL"
            'Public EaseBuzz_SALT_KEY As String = "48Q5G1L07G"
            'Public EaseBuzz_BASE_URL As String = "https://pay.easebuzz.in" '/pay/secure
            'https://pay.easebuzz.in/pay/secure

            Dim Text As String = EaseBuzz_MERCHANT_KEY & "|" & txnID & "|" & VPayCartAmount & "|" & "Shopping" & "|" & VPayCustomerName & "|" & VCustomerEmailID & "|||||||||||" & EaseBuzz_SALT_KEY
            'key|txnid|amount|productinfo|firstname|email|||||||||||salt
            Dim message As Byte() = Encoding.UTF8.GetBytes(Text)
            Dim UE As UnicodeEncoding = New UnicodeEncoding()
            Dim hashValue As Byte()
            Dim hash As String = ""
            Dim hashString As SHA512Managed = New SHA512Managed()
            Dim hex As String = ""
            hashValue = hashString.ComputeHash(message)
            For Each x As Byte In hashValue
                hex += String.Format("{0:x2}", x)
            Next
            hash = hex

            Dim data As System.Collections.Hashtable = New System.Collections.Hashtable()

            'hashVarsSeq = "udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10".Split('|'); // spliting hash sequence from config

            data.Add("hash", hex.ToString)
            data.Add("key", EaseBuzz_MERCHANT_KEY.ToString)
            data.Add("txnid", txnID.ToString)
            data.Add("amount", VPayCartAmount)
            data.Add("productinfo", "Shopping")
            data.Add("firstname", VPayCustomerName)
            data.Add("email", VCustomerEmailID)
            data.Add("phone", VPayCustomerPhoneNo)
            data.Add("salt", EaseBuzz_SALT_KEY)

            Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
            Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")

            Dim sul As String = strUrl & "Admin/Add_Money_EaseBuzz_Handler_Success.aspx"
            Dim ful As String = strUrl & "Admin/Add_Money_Handler_Fail.aspx"
            Dim cul As String = strUrl & "Admin/Add_Money_Handler_Cancel.aspx"

            data.Add("surl", sul)
            data.Add("furl", ful)
            data.Add("curl", cul)
            data.Add("show_payment_mode", "NB,DC,CC,DAP,MW,UPI,OM,EMI") 'NB,DC,CC,DAP,MW,UPI,OM,EMI


            'show_payment_mode
            'NB,DC,CC,DAP,MW,UPI,OM,EMI     BNPL

            pg.Controls.Add(New LiteralControl(GV.PreparePOSTForm("https://pay.easebuzz.in/pay/secure", data)))
            Response.Write(GV.PreparePOSTForm("https://pay.easebuzz.in/pay/secure", data))

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub goPayuGateway(ByVal TransID As String, ByVal VCustomerEmailID As String, ByVal VPayCustomerID As String, ByVal VPayCartAmount As String, ByVal VPayCustomerName As String, ByVal VPayCustomerPhoneNo As String, ByVal VDeliveryRID As String, pg As Page)
        Try
            Dim txnID As String = TransID
            '/// create cookie start
            GV.set_OrderID_SessionVariables(TransID, Request, Response)
            '/// create cookie end


            Dim Text As String = MERCHANT_KEY & "|" & txnID & "|" & VPayCartAmount & "|" & "Shopping" & "|" & VPayCustomerName & "|" & VCustomerEmailID & "|||||||||||" & SALT_KEY

            Dim message As Byte() = Encoding.UTF8.GetBytes(Text)
            Dim UE As UnicodeEncoding = New UnicodeEncoding()
            Dim hashValue As Byte()
            Dim hash As String = ""
            Dim hashString As SHA512Managed = New SHA512Managed()
            Dim hex As String = ""
            hashValue = hashString.ComputeHash(message)
            For Each x As Byte In hashValue
                hex += String.Format("{0:x2}", x)
            Next
            hash = hex

            Dim data As System.Collections.Hashtable = New System.Collections.Hashtable()

            data.Add("hash", hex.ToString)
            data.Add("key", MERCHANT_KEY.ToString)
            data.Add("txnid", txnID.ToString)
            data.Add("amount", VPayCartAmount)
            data.Add("productinfo", "Shopping")
            data.Add("firstname", VPayCustomerName)
            data.Add("email", VCustomerEmailID)
            data.Add("phone", VPayCustomerPhoneNo)
            data.Add("salt", SALT_KEY)

            Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
            Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")

            Dim sul As String = strUrl & "Admin/Add_Money_Handler_Success.aspx"
            Dim ful As String = strUrl & "Admin/Add_Money_Handler_Fail.aspx"
            Dim cul As String = strUrl & "Admin/Add_Money_Handler_Cancel.aspx"

            data.Add("surl", sul)
            data.Add("furl", ful)
            data.Add("curl", cul)
            data.Add("service_provider", "payu_paisa")

            pg.Controls.Add(New LiteralControl(GV.PreparePOSTForm("https://secure.payu.in/_payment", data)))

            Response.Write(GV.PreparePOSTForm("https://secure.payu.in/_payment", data))

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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

            If Not (CInt(txtAmount.Text.Trim) >= 100 And CInt(txtAmount.Text.Trim) <= 100000) Then
                lblError.Text = "Please Enter Amount Between 100 To 100000"
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


            Request_name = VFirstName.Trim & " " & VLastName.Trim
            Request_email = VEmailID
            Request_phone = VMobileNo
            Request_amount = VAmount
            Request_Transaction_Id = New_transId
            Request_Purpose = "WA"

            Try
                Dim str1 As String = " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details (TransIpAddress,Reference_Id,Reference_Type,Ref_Plan_Code,Request_DateTime,Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,Request_redirect_url,Request_CompanyCode,Request_Purpose,Request_AgentID,Request_TransID,UpdatedOn,UpdatedBy) values ('" & GV.parseString(GV.GetIPAddress) & "','" & VLoginID & "','" & VLoginGroup & "','" & VLoginID & "',getdate(),'" & Request_Transaction_Id & "','" & Request_name & "','" & Request_email & "','" & Request_phone & "','" & Request_amount & "','" & Request_redirect_url & "','" & Request_CompanyCode & "','" & Request_Purpose & "','" & Request_AgentID & "','" & Request_TransID & "',getdate(),'Admin')"
                GV.FL.DMLQueriesBulk(str1)


                'ddl_Default_Service.Items.Add("PayuMoney")
                'ddl_Default_Service.Items.Add("Easebuzz")

                Dim Gateway As String = GV.FL.AddInVar("Gateway", GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD  where Title='Payment Gateway' and AdminID='" & VCompanyCode & "'")
                Dim FnlGateway As String = ""
                If Not GV.parseString(Gateway.Trim) = "" Then
                    If GV.parseString(Gateway.Trim).ToUpper = "PayuMoney".ToUpper Then
                        FnlGateway = "PayuMoney"
                    ElseIf GV.parseString(Gateway.Trim).ToUpper = "Easebuzz".ToUpper Then
                        FnlGateway = "Easebuzz"
                    Else
                        FnlGateway = "PayuMoney"
                    End If
                Else
                    FnlGateway = "PayuMoney"
                End If


                If FnlGateway.ToUpper = "PayuMoney".ToUpper Then
                    goPayuGateway(Request_Transaction_Id, Request_email, GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim, Request_amount, Request_name, Request_phone, GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim, Me)
                Else
                    'Easebuzz
                    goEasebuzzGateway(Request_Transaction_Id, Request_email, GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim, Request_amount, Request_name, Request_phone, GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim, Me)
                End If

            Catch ex As Exception
                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

            End Try

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblError.Text = ex.Message & " : " & ex.StackTrace
            lblError.CssClass = "errorlabels"
        End Try



    End Sub

    Private Sub btnPopupYes_Click(sender As Object, e As System.EventArgs) Handles btnPopupYes.Click
        Try
            Response.Redirect("BOS_AddMoneyToWallet.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class