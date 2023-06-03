Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp



Public Class BOS_MoneyTransfer_Old
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblWalletBal.Text = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)


            If Not IsPostBack Then
                btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"

                lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                lblAgentType.Text = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            End If

        Catch ex As Exception
        End Try
    End Sub


    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API URL  - Start
    Dim Add_New_Customer_API_URL As String = "https://www.vacsc.com/DMTAPI/AddNewCustomer"
    Dim Verify_Customer_API_URL As String = "https://www.vacsc.com/DMTAPI/VerifyCustomer"
    Dim Bank_Details_API_URL As String = "https://www.vacsc.com/DMTAPI/BankDetails"
    Dim Verify_Bank_Details_API_URL As String = "https://www.vacsc.com/DMTAPI/VerifytBankDetails"
    Dim Add_New_Recipients_API_URL As String = "https://www.vacsc.com/DMTAPI/AddNewRecipients"
    Dim Recipients_Details_API_URL As String = "https://www.vacsc.com/DMTAPI/RecipientsDetails"
    Dim Receipent_List_API_URL As String = "https://www.vacsc.com/DMTAPI/ReceipentList"
    Dim Money_Transfer_API_URL As String = "https://www.vacsc.com/DMTAPI/GetMoneyTransfer"
    Dim VerifyOtp_API_URL As String = "https://www.vacsc.com/DMTAPI/VerifyOtp"
    Dim ResendOtp_API_URL As String = "https://www.vacsc.com/DMTAPI/ResendOtp"
    '///  Money Transfer API URL  - END

    Public Class Add_New_Customer_API_Parameters
        Dim VMobile, VName, VDOB, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return VName
            End Get
            Set(ByVal value As String)
                VName = value
            End Set
        End Property
        Public Property DOB() As String
            Get
                Return VDOB
            End Get
            Set(ByVal value As String)
                VDOB = value
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
    End Class
    Public Class Verify_Customer_API_Parameters
        Dim VMobile, VKey As String

        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
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

    End Class
    Public Class Bank_Details_API_Parameters
        Dim VBankCode, Vifsc, VKey As String

        Public Property BankCode() As String
            Get
                Return VBankCode
            End Get
            Set(ByVal value As String)
                VBankCode = value
            End Set
        End Property

        Public Property ifsc() As String
            Get
                Return Vifsc
            End Get
            Set(ByVal value As String)
                Vifsc = value
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

    End Class
    Public Class Verify_Bank_Details_API_Parameters
        Dim VBankAccountCode, VKey, VAccountNo, VMobileNo As String

        Public Property BankAccountCode() As String
            Get
                Return VBankAccountCode
            End Get
            Set(ByVal value As String)
                VBankAccountCode = value
            End Set
        End Property

        Public Property AccountNo() As String
            Get
                Return VAccountNo
            End Get
            Set(ByVal value As String)
                VAccountNo = value
            End Set
        End Property

        Public Property MobileNo() As String
            Get
                Return VMobileNo
            End Get
            Set(ByVal value As String)
                VMobileNo = value
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

    End Class
    Public Class Add_New_Recipients_API_Parameters
        Dim VMobile, VBankCode, Vrecipient_name, VIfscCode, VAccountNo, Vcustomermobile, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property
        Public Property BankCode() As String
            Get
                Return VBankCode
            End Get
            Set(ByVal value As String)
                VBankCode = value
            End Set
        End Property

        Public Property recipient_name() As String
            Get
                Return Vrecipient_name
            End Get
            Set(ByVal value As String)
                Vrecipient_name = value
            End Set
        End Property
        Public Property IfscCode() As String
            Get
                Return VIfscCode
            End Get
            Set(ByVal value As String)
                VIfscCode = value
            End Set
        End Property
        Public Property AccountNo() As String
            Get
                Return VAccountNo
            End Get
            Set(ByVal value As String)
                VAccountNo = value
            End Set
        End Property
        Public Property customermobile() As String
            Get
                Return Vcustomermobile
            End Get
            Set(ByVal value As String)
                Vcustomermobile = value
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
    End Class
    Public Class Recipients_Details_API_Parameters
        Dim VMobile, VKey, VRecipient_Id As String

        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Recipient_Id() As String
            Get
                Return VRecipient_Id
            End Get
            Set(ByVal value As String)
                VRecipient_Id = value
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
    End Class
    Public Class Receipent_List_API_Parameters
        Dim VMobile, VKey As String

        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
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

    End Class
    Public Class Money_Transfer_API_Parameters
        Dim Vcust_id, Vmerchant_document_id, Vchannel, VRecipent, VIFSC, VKey As String
        Dim Vamount, Vrecipient_id, Vmerchant_document_id_type As Integer

        Public Property recipient_id() As Integer
            Get
                Return Vrecipient_id
            End Get
            Set(ByVal value As Integer)
                Vrecipient_id = value
            End Set
        End Property
        Public Property cust_id() As String
            Get
                Return Vcust_id
            End Get
            Set(ByVal value As String)
                Vcust_id = value
            End Set
        End Property
        Public Property amount() As Integer
            Get
                Return Vamount
            End Get
            Set(ByVal value As Integer)
                Vamount = value
            End Set
        End Property
        Public Property merchant_document_id_type() As Integer
            Get
                Return Vmerchant_document_id_type
            End Get
            Set(ByVal value As Integer)
                Vmerchant_document_id_type = value
            End Set
        End Property

        Public Property merchant_document_id() As String
            Get
                Return Vmerchant_document_id
            End Get
            Set(ByVal value As String)
                Vmerchant_document_id = value
            End Set
        End Property
        Public Property channel() As String
            Get
                Return Vchannel
            End Get
            Set(ByVal value As String)
                Vchannel = value
            End Set
        End Property

        Public Property Recipent() As String
            Get
                Return VRecipent
            End Get
            Set(ByVal value As String)
                VRecipent = value
            End Set
        End Property
        Public Property IFSC() As String
            Get
                Return VIFSC
            End Get
            Set(ByVal value As String)
                VIFSC = value
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

    End Class
    Public Class VerifyOtp_API_Parameters
        Dim VOtp, VMobile, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Otp() As String
            Get
                Return VOtp
            End Get
            Set(ByVal value As String)
                VOtp = value
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
    End Class
    Public Class ResendOtp_API_Parameters
        Dim VMobile, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
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
    End Class



    Public Function GetApiResult(APIMethod As String) As String
        Dim ApiResult As String = ""
        Dim StrParameters As String = ""
        Dim API_URLS As String = ""

        Try

            If APIMethod = "Verify_Customer_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Verify_Customer_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Verify_Customer_API_URL
            ElseIf APIMethod = "Receipent_List_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Receipent_List_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Receipent_List_API_URL
            ElseIf APIMethod = "Recipients_Details_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Recipients_Details_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                setParameter_API_Obj.Recipient_Id = GV.parseString(lblReceipentId.Text.Trim)
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Recipients_Details_API_URL
            ElseIf APIMethod = "Bank_Details_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Bank_Details_API_Parameters()
                setParameter_API_Obj.BankCode = GV.parseString(ddlSelectBank.SelectedValue.Trim)
                setParameter_API_Obj.ifsc = GV.parseString(txtIFSCCode.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Bank_Details_API_URL
            ElseIf APIMethod = "Verify_Bank_Details_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New Verify_Bank_Details_API_Parameters()
                setParameter_API_Obj.AccountNo = GV.parseString(txtBankAccountNo.Text.Trim)
                setParameter_API_Obj.BankAccountCode = GV.FL.AddInVar("BankID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList where Code='" & GV.parseString(ddlSelectBank.SelectedValue.Trim) & "'")
                setParameter_API_Obj.MobileNo = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Verify_Bank_Details_API_URL
            ElseIf APIMethod = "Money_Transfer_API_Parameters" Then
                Dim setParameter_API_Obj As New Money_Transfer_API_Parameters()

                setParameter_API_Obj.recipient_id = GV.parseString(lblReceipentId.Text.Trim)
                setParameter_API_Obj.cust_id = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.amount = GV.parseString(txtEnterAmt.Text.Trim)
                setParameter_API_Obj.merchant_document_id_type = "1"
                setParameter_API_Obj.merchant_document_id = "AAICB5338D"
                setParameter_API_Obj.channel = GV.parseString(ddlTransferMode.SelectedValue.Trim)
                setParameter_API_Obj.Recipent = GV.parseString(lblReceipentName.Text.Trim)
                setParameter_API_Obj.IFSC = GV.parseString(lblIFSCCode.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Money_Transfer_API_URL
            ElseIf APIMethod = "Add_New_Customer_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Add_New_Customer_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtMobileNo.Text.Trim)
                setParameter_API_Obj.Name = GV.parseString(txtCustomerName.Text.Trim)
                setParameter_API_Obj.DOB = "10/dec/1999"
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Add_New_Customer_API_URL
            ElseIf APIMethod = "Add_New_Recipients_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New Add_New_Recipients_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtRecepientMobileNo.Text.Trim)
                setParameter_API_Obj.BankCode = GV.FL.AddInVar("BankID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList where Code='" & GV.parseString(ddlSelectBank.SelectedValue.Trim) & "'")
                setParameter_API_Obj.recipient_name = GV.parseString(lblRecepientActualName.Text.Trim)
                setParameter_API_Obj.IfscCode = GV.parseString(txtIFSCCode.Text.Trim)
                setParameter_API_Obj.AccountNo = GV.parseString(txtBankAccountNo.Text.Trim)
                setParameter_API_Obj.customermobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Add_New_Recipients_API_URL

            ElseIf APIMethod = "VerifyOtp_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New VerifyOtp_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Otp = GV.parseString(txtEnterOTP.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = VerifyOtp_API_URL
            ElseIf APIMethod = "ResendOtp_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New ResendOtp_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = ResendOtp_API_URL
            End If


            ApiResult = ReadbyRestClient(API_URLS, StrParameters)
        Catch ex As Exception
            Return ApiResult
        End Try
        Return ApiResult
    End Function

    Public Function ReadbyRestClient(Urls As String, Parameter As String) As String

        Dim API_Name, Trans_ID, Trans_DateTime, Request_String, Response_String, AgentID, AgentType As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        AgentID = ""
        AgentType = ""

        Dim strQry As String = ""

        Dim str As String = ""
        Dim LogString As String = ""
        Try
            lblTransId.Text = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(lblTransId.Text) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine

            API_Name = "Money Transfer API"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text

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
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine

            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
        Catch ex As Exception
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX



    Dim APIResult As String = ""
    Dim strBuild As String = ""
    Protected Sub btnSearchCustomerGo_Click(sender As Object, e As EventArgs) Handles btnSearchCustomerGo.Click
        Try
            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""
            If txtEnterMobileNo.Text.Trim = "" Then
                lblSearchCustomerError.Text = "Enter Mobile No."
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If
            Dim MoneyTrasfer As String = ""
            If ddlGateway.SelectedValue.Trim.ToUpper = "Money Transfer".Trim.ToUpper Then
                MoneyTrasfer = "MoneyTransferAPI_Status"
            Else
                MoneyTrasfer = "MoneyTransferAPI_2_Status"
            End If


            '///// Start Check API  STATUS Super ADmin Level

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Super ADmin Level

            '///// Start Check API  STATUS System Settings 

            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 



            '//// Verify Customer Mobile No
            If ddlGateway.SelectedValue.Trim.ToUpper = "Money Transfer".Trim.ToUpper Then

                APIResult = GetApiResult("Verify_Customer_API_Parameters")
                '/// parse and read data in list format through json parse
                Dim json_ As String = APIResult
                Dim ser_ As JObject = JObject.Parse(json_)
                Dim name As String = ""
                Dim CustomerID As String = ""
                Dim MobileNO As String = ""
                Dim VerifyMessage As String = ser_.SelectToken("message").ToString.Trim

                txtEnterMobileNo.ReadOnly = True
                btnSearchCustomerGo.Enabled = False
                btnSearchCustomerGo.CssClass = "btn btn-primary"
                If VerifyMessage = "customer_id does not exist in system" Then
                    lblSearchCustomerError.Text = VerifyMessage
                    lblSearchCustomerError.CssClass = "errorlabels"
                    lblSearchCustomerError.Visible = True
                    btnAddNewCustomer.Visible = True
                    txtEnterMobileNo.ReadOnly = True
                    btnSearchCustomerGo.Enabled = False
                    btnSearchCustomerGo.CssClass = "btn btn-primary"


                ElseIf VerifyMessage = "Non-KYC active" Then
                    Dim data_ As List(Of JToken) = ser_.Children().ToList
                    '// Loop through data
                    For Each item As JProperty In data_
                        item.CreateReader()
                        Select Case item.Name
                            Case "response_status_id"

                                strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_status_id").ToString

                            Case "data"
                                Dim data1 As List(Of JToken) = item.Children().ToList
                                For Each msg As JObject In data1

                                    '/// Fix Attribute Name and get its value
                                    name = msg("name")
                                    CustomerID = msg("customer_id")
                                    MobileNO = msg("mobile")
                                Next
                            Case "response_type_id"
                                strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                            Case "message"
                                strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                            Case "status"
                                strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                        End Select
                    Next
                    DIV_Clear()

                    Div_CustomerDetails.Visible = True

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("CustomerID")
                    Dim dc3 As DataColumn = New DataColumn("Name")
                    Dim dc4 As DataColumn = New DataColumn("MobileNo")
                    Dim dc5 As DataColumn = New DataColumn("Message")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)
                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = 1
                    dr1(1) = CustomerID
                    dr1(2) = name
                    dr1(3) = MobileNO
                    dr1(4) = VerifyMessage
                    dt.Rows.Add(dr1)
                    gdvCustomerDetails.DataSource = dt
                    gdvCustomerDetails.DataBind()
                    ' CheckRecepient()
                    Fill_Recepient_List()

                    txtEnterMobileNo.ReadOnly = True
                    btnSearchCustomerGo.Enabled = False
                    btnSearchCustomerGo.CssClass = "btn btn-primary"
                ElseIf VerifyMessage = "Verification pending" Then
                    'Otp very div visible true

                    Dim data_ As List(Of JToken) = ser_.Children().ToList

                    For Each item As JProperty In data_
                        item.CreateReader()
                        Select Case item.Name
                            Case "response_status_id"

                                strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_status_id").ToString

                            Case "data"
                                Dim data1 As List(Of JToken) = item.Children().ToList
                                For Each msg As JObject In data1

                                    '/// Fix Attribute Name and get its value
                                    name = msg("name")
                                    CustomerID = msg("customer_id")

                                Next
                            Case "response_type_id"
                                strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                            Case "message"
                                strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                            Case "status"
                                strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                        End Select
                    Next



                    DIV_Clear()
                    Div_VerifyOTP.Visible = True
                    Div_CustomerDetails.Visible = True

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("CustomerID")
                    Dim dc3 As DataColumn = New DataColumn("Name")
                    Dim dc4 As DataColumn = New DataColumn("MobileNo")
                    Dim dc5 As DataColumn = New DataColumn("Message")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)
                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = 1
                    dr1(1) = CustomerID
                    dr1(2) = name
                    dr1(3) = "N/A"
                    dr1(4) = VerifyMessage
                    dt.Rows.Add(dr1)
                    gdvCustomerDetails.DataSource = dt
                    gdvCustomerDetails.DataBind()

                Else
                    lblSearchCustomerError.Text = VerifyMessage
                    lblSearchCustomerError.CssClass = "errorlabels"
                    lblSearchCustomerError.Visible = True
                    txtEnterMobileNo.ReadOnly = False
                    btnSearchCustomerGo.Enabled = True
                    btnSearchCustomerGo.CssClass = "btn btn-primary"
                End If



            Else

                'Member No:RKITAPI190212
                'Password:	5nrg7nrmz4
                'Api Password:	cg45ob8
                'Encryption Key:77bxjoceldz46lrm...

                'AddSender
                'https://rechargkit.biz/get/dmr/addSender?partner_id=RKITAPI190212&api_password=cg45ob8&name=sapnarana&mobile_no=9999180633&city=Delhi&state=Delhi&address1=Delhi&pincode=110085
                '{"ERROR":23,"VERIFIED":"","DETAILS":"","MESSAGE":"You are not authorised to use this service."}
                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"name":"sapnarana","mobileNumber":"9999180633","city":"Delhi","state":"Delhi","pincode":"110085","address":"Delhi"},"MESSAGE":"Sender added successfully"}

                'validateSender
                'https://rechargkit.biz/get/dmr/validateSender?partner_id=RKITAPI190212&api_password=cg45ob8&name=sapnarana&mobile_no=9999180633

                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"name":"sapnarana","mobileNumber":"9999180633","city":"Delhi","state":"Delhi","pincode":null,"address":"Delhi"},"MESSAGE":"Sender is valid"}
                '{"ERROR":1,"VERIFIED":0,"DETAILS":"","MESSAGE":"Sender doesn't exits"}

                'SenderLimit
                'https://rechargkit.biz/get/dmr/SenderLimit?partner_id=RKITAPI190212&api_password=cg45ob8&name=eklavya&mobile_no=9212345320
                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"dailyLimit":5000,"remainingMonthlyLimit":25000},"MESSAGE":"Limit successfully fetched"}
                '{"ERROR":1,"VERIFIED":0,"DETAILS":"","MESSAGE":"Customer mobile does not exist"}

                'AddBeneficiary
                'https://rechargkit.biz/get/dmr/AddBeneficiary?partner_id=XXXX&api_password=XXXX&name=XXXX&mobile_no=XXXX&name=XXXX&email=XXXX&bankAccountNumber=XXXX&ifscCode=XXXX&address1=XXXX&city=XXXX&state=XXXX&pincode=XXXX&vpa=XXXX&bankName= XXXX
                'https://rechargkit.biz/get/dmr/AddBeneficiary?partner_id=RKITAPI190212&api_password=cg45ob8&name=eklavyaverma&mobile_no=9212345320&name=eklavyaverma&email=verma.eklavya@gmail.com&bankAccountNumber=73290100008155&ifscCode=barb0dbrohi&address1=delhi&city=delhi&state=delhi&pincode=110085&vpa=XXXX&bankName=Bankofbaroda
                '{"ERROR":0,"DETAILS":{"beneficiaryId":"ABT1612515184482","name":"Sapnarana","email":"sanarana44@gmail.com","benefMobileNumber":"9999180633","bankAccountNumber":"65209313469","ifscCode":"sbin0050722","address1":"delhi","city":"delhi","state":"delhi","pincode":"110085","vpa":"XXXX","bankName":"StateBankofIndia"},"MESSAGE":"Benficiary  added successfully"}
                '{"ERROR":0,"DETAILS":{"beneficiaryId":"ABT1612515412546","name":"eklavyaverma","email":"verma.eklavya@gmail.com","benefMobileNumber":"9212345320","bankAccountNumber":"73290100008155","ifscCode":"barb0dbrohi","address1":"delhi","city":"delhi","state":"delhi","pincode":"110085","vpa":"XXXX","bankName":"Bankofbaroda"},"MESSAGE":"Benficiary  added successfully"}


                'getBeneId

                'https://rechargkit.biz/get/dmr/getBeneId?partner_id=XXXX&api_password=XXXX&bankAccountNumber=XXXX&ifscCode= XXXX
                'https://rechargkit.biz/get/dmr/getBeneId?partner_id=RKITAPI190212&api_password=cg45ob8&bankAccountNumber=73290100008158&ifscCode=barb0dbrohi
                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"beneficiaryId":"ABT1612515412546","name":"eklavyaverma","email":"verma.eklavya@gmail.com","benefMobileNumber":"9212345320","bankAccountNumber":"73290100008155","ifscCode":"barb0dbrohi","address1":"delhi","city":"delhi","state":"delhi","pincode":"110085","vpa":"XXXX","bankName":"Bankofbaroda"},"MESSAGE":"Benficiary details fetched."}
                '{"ERROR":1,"VERIFIED":0,"DETAILS":"","MESSAGE":"Beneficiary not found with given bank account details"}


                'Get Beneficiary List
                'https://rechargkit.biz/get/dmr/beneficiaryList?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=9212345320

                'moneyTransfer
                'https://rechargkit.biz/get/dmr/moneyTransfer?partner_id=XXXX&api_password=XXXX&mobile_no=XXXX&beneId=XXXX&amount=10.00&partner_request_id= XXXX
                'https://rechargkit.biz/get/dmr/moneyTransfer?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=9999180633&beneId=ABT1612515412546&amount=10.00&partner_request_id=52611
                '{"ERROR":0,"STATUS":2,"ORDERID":52305281,"OPTRANSID":"Bad Request","PARTNERREQID":"5261","MESSAGE":"Success","COMMISSION":"0.0000","CHARGE":"8.26"}
                '{"ERROR":0,"STATUS":2,"ORDERID":52309336,"OPTRANSID":"e57b117c679e11ebaef80a0047330000","PARTNERREQID":"526","MESSAGE":"Success","COMMISSION":"0.0000","CHARGE":"8.26"}

                'AddBeneficiaryPennyDrop
                'https://rechargkit.biz/get/dmr/AddBeneficiaryPennyDrop?partner_id=XXXX&api_password=XXXX&mobile_no=XXXX&name=XXXX&email=XXXX&bankAccountNumber=XXXX&ifscCode=XXXX&address1=XXXX&city=XXXX&state=XXXX&pincode=XXXX&vpa=XXXX&benefMobileNumber=XXXX&partner_request_id= XXXX
                'https://rechargkit.biz/get/dmr/AddBeneficiaryPennyDrop?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=9999180633&name=XXXX&email=eklavyaverma&bankAccountNumber=73290100008155&ifscCode=barb0dbrohi&address1=delhi&city=delhi&state=delhi&pincode=110085&vpa=XXXX&benefMobileNumber=9212345320&partner_request_id=526

            End If






            ' txttestBox.Text = strBuild
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddNewCustomer_Click(sender As Object, e As EventArgs) Handles btnAddNewCustomer.Click
        Try
            If Not txtEnterMobileNo.Text.Trim = "" Then
                txtMobileNo.Text = GV.parseString(txtEnterMobileNo.Text.Trim)
            End If
            lblAddCustomerError.Text = ""
            lblAddCustomerError.CssClass = ""
            txtCustomerName.Text = ""
            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""
            lblSearchCustomerError.Visible = False
            btnAddNewCustomer.Visible = False
            Div_AddCustomer.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddCustomerGo_Click(sender As Object, e As EventArgs) Handles btnAddCustomerGo.Click
        Try

            lblAddCustomerError.Text = ""
            lblAddCustomerError.CssClass = ""
            If txtCustomerName.Text.Trim = "" Then
                lblAddCustomerError.Text = "Please Enter Customer Name."
                lblAddCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If


            '//// Verify Customer Mobile No
            APIResult = GetApiResult("Add_New_Customer_API_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)

            '#EK1
            Dim data_ As List(Of JToken) = ser_.Children().ToList
            '// Loop through data
            For Each item As JProperty In data_
                item.CreateReader()
                Select Case item.Name
                    Case "response_status_id"

                        strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_status_id").ToString

                    Case "data"
                        Dim data1 As List(Of JToken) = item.Children().ToList
                        For Each msg As JObject In data1
                            '/// Dynamic Name and get its value
                            For Each p In msg
                                strBuild = strBuild & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString
                            Next
                        Next

                    Case "response_type_id"
                        strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                    Case "message"
                        strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                    Case "status"
                        strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                End Select
            Next







            DIV_Clear()
            Div_VerifyOTP.Visible = True
            Div_CustomerDetails.Visible = False

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnVerifyOTPNo_Click(sender As Object, e As EventArgs) Handles btnVerifyOTPNo.Click
        Try

            lblVerifyOTPError.Text = ""
            lblVerifyOTPError.CssClass = ""
            If txtEnterOTP.Text.Trim = "" Then
                lblVerifyOTPError.Text = "Please Enter OTP No."
                lblVerifyOTPError.CssClass = "errorlabels"
                Exit Sub
            End If
            '//// Verify Customer Mobile No
            APIResult = GetApiResult("VerifyOtp_API_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim VerifyMessage As String = ser_.SelectToken("message").ToString.Trim
            Dim status As String = ser_.SelectToken("status").ToString
            If status = "0" Then
                lblVerifyOTPError.CssClass = "successlabels"
            Else
                lblVerifyOTPError.CssClass = "errorlabels"
            End If
            If VerifyMessage = "Wallet opened successfully." Then
                Div_VerifyOTP.Visible = False
                Div_RecepientDetails.Visible = True
                APIResult = GetApiResult("Verify_Customer_API_Parameters")
                '/// parse and read data in list format through json parse
                json_ = APIResult
                ser_ = JObject.Parse(json_)
                Dim name As String = ""
                Dim CustomerID As String = ""
                Dim MobileNO As String = ""
                txtEnterMobileNo.ReadOnly = True
                btnSearchCustomerGo.Enabled = False
                btnSearchCustomerGo.CssClass = "btn btn-primary"

                Dim data_ As List(Of JToken) = ser_.Children().ToList
                '// Loop through data
                For Each item As JProperty In data_
                    item.CreateReader()
                    Select Case item.Name
                        Case "response_status_id"

                            strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_status_id").ToString

                        Case "data"
                            Dim data1 As List(Of JToken) = item.Children().ToList
                            For Each msg As JObject In data1

                                '/// Fix Attribute Name and get its value
                                name = msg("name")
                                CustomerID = msg("customer_id")
                                MobileNO = msg("mobile")
                            Next
                        Case "response_type_id"
                            strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                        Case "message"
                            strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                        Case "status"
                            strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                    End Select
                Next
                DIV_Clear()

                Div_CustomerDetails.Visible = True

                Dim dt As New DataTable
                Dim dc1 As DataColumn = New DataColumn("SrNo")
                Dim dc2 As DataColumn = New DataColumn("CustomerID")
                Dim dc3 As DataColumn = New DataColumn("Name")
                Dim dc4 As DataColumn = New DataColumn("MobileNo")
                Dim dc5 As DataColumn = New DataColumn("Message")

                dt.Columns.Add(dc1)
                dt.Columns.Add(dc2)
                dt.Columns.Add(dc3)
                dt.Columns.Add(dc4)
                dt.Columns.Add(dc5)
                Dim dr1 As DataRow = dt.NewRow()
                dr1(0) = 1
                dr1(1) = CustomerID
                dr1(2) = name
                dr1(3) = MobileNO
                dr1(4) = VerifyMessage
                dt.Rows.Add(dr1)
                gdvCustomerDetails.DataSource = dt
                gdvCustomerDetails.DataBind()
                ' CheckRecepient()
                Fill_Recepient_List()
            Else
                lblVerifyOTPError.Text = VerifyMessage
                Exit Sub
            End If



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnResendOTP_Click(sender As Object, e As EventArgs) Handles btnResendOTP.Click
        Try
            lblVerifyOTPError.Text = ""
            lblVerifyOTPError.CssClass = ""

            '//// Verify Customer Mobile No
            APIResult = GetApiResult("ResendOtp_API_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim VerifyMessage As String = ser_.SelectToken("message").ToString.Trim
            Dim status As String = ser_.SelectToken("status").ToString
            If status = "0" Then
                lblVerifyOTPError.Text = VerifyMessage
                lblVerifyOTPError.CssClass = "successlabels"
            Else
                lblVerifyOTPError.Text = VerifyMessage
                lblVerifyOTPError.CssClass = "errorlabels"
            End If

        Catch ex As Exception

        End Try
    End Sub


    Public Sub DIV_Clear()
        Try
            btnAddNewCustomer.Visible = False
            Div_AddCustomer.Visible = False
            Div_VerifyOTP.Visible = False
            Div_CustomerDetails.Visible = False
            Div_RecepientDetails.Visible = False
            Div_AddRecepient.Visible = False
            Div_TransferAmt.Visible = False
        Catch ex As Exception

        End Try
    End Sub



    Public Sub Fill_Recepient_List()
        Try
            grdAddRecepient.DataSource = Nothing
            grdAddRecepient.DataBind()
            APIResult = GetApiResult("Receipent_List_API_Parameters")



            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim Meassge As String = ser_.SelectToken("message").ToString
            If Meassge = "No recepients found" Then
                grdAddRecepient.DataSource = Nothing
                grdAddRecepient.DataBind()
                Div_RecepientDetails.Visible = True
                lblError.Visible = True
                Exit Sub
            End If

            Dim data_ As List(Of JToken) = ser_.Children().ToList
            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("SrNo")
            Dim dc2 As DataColumn = New DataColumn("BankName")
            Dim dc3 As DataColumn = New DataColumn("IFSC")
            Dim dc4 As DataColumn = New DataColumn("AccountNo")
            Dim dc5 As DataColumn = New DataColumn("Receipent")
            Dim dc6 As DataColumn = New DataColumn("ReceipentId")
            Dim dc7 As DataColumn = New DataColumn("MobileNo")
            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            dt.Columns.Add(dc5)
            dt.Columns.Add(dc6)
            dt.Columns.Add(dc7)



            For Each item As JProperty In data_
                item.CreateReader()
                Select Case item.Name
                    Case "response_status_id"

                        strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_status_id").ToString

                    Case "data"
                        Dim data1 As List(Of JToken) = item.Children().ToList
                        For Each msg As JObject In data1
                            Dim data_1 As List(Of JToken) = msg.Children().ToList

                            For Each RList As JProperty In data_1
                                RList.CreateReader()
                                Select Case RList.Name
                                    Case "recipient_list"
                                        Dim RecpChild As List(Of JToken) = RList.Children().ToList
                                        For Each FinalChildLoop As JArray In RecpChild

                                            Dim FinalString As String = ""
                                            Dim account, ifsc, recipient_name, recipient_mobile, bank, RECID, RECMobileNO As String
                                            For j As Integer = 0 To FinalChildLoop.Count - 1
                                                FinalString = FinalChildLoop(j).ToString
                                                Dim ser_123 As JObject = JObject.Parse(FinalString)
                                                account = ser_123.SelectToken("account").ToString()
                                                ifsc = ser_123.SelectToken("ifsc").ToString()
                                                recipient_name = ser_123.SelectToken("recipient_name").ToString()
                                                recipient_mobile = ser_123.SelectToken("recipient_mobile").ToString()
                                                bank = ser_123.SelectToken("bank").ToString()
                                                RECID = ser_123.SelectToken("recipient_id").ToString()
                                                RECMobileNO = ser_123.SelectToken("recipient_mobile").ToString()
                                                Dim dr1 As DataRow
                                                If grdAddRecepient.Rows.Count > 0 Then
                                                    For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                                                        dr1 = dt.NewRow()
                                                        dr1(0) = GV.parseString(grdAddRecepient.Rows(i).Cells(1).Text)
                                                        dr1(1) = GV.parseString(grdAddRecepient.Rows(i).Cells(2).Text)
                                                        dr1(2) = GV.parseString(grdAddRecepient.Rows(i).Cells(3).Text)
                                                        dr1(3) = GV.parseString(grdAddRecepient.Rows(i).Cells(4).Text)
                                                        dr1(4) = GV.parseString(grdAddRecepient.Rows(i).Cells(5).Text)
                                                        dr1(5) = GV.parseString(grdAddRecepient.Rows(i).Cells(6).Text)
                                                        dt.Rows.Add(dr1)
                                                    Next
                                                End If
                                                dr1 = dt.NewRow()
                                                dr1(1) = bank
                                                dr1(2) = ifsc
                                                dr1(3) = account
                                                dr1(4) = recipient_name
                                                dr1(5) = RECID
                                                dr1(6) = RECMobileNO
                                                dt.Rows.Add(dr1)

                                            Next
                                            grdAddRecepient.DataSource = dt
                                            grdAddRecepient.DataBind()
                                            GV.FL.showSerialnoOnGridView(grdAddRecepient, 1)


                                            If grdAddRecepient.Rows.Count > 0 Then
                                                Div_RecepientDetails.Visible = True
                                                lblError.Visible = False
                                            Else
                                                grdAddRecepient.DataSource = Nothing
                                                grdAddRecepient.DataBind()
                                                Div_RecepientDetails.Visible = True
                                                lblError.Visible = True
                                            End If

                                        Next




                                End Select



                            Next



                        Next
                    Case "response_type_id"
                        strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                    Case "message"
                        strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                    Case "status"
                        strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                End Select
            Next





        Catch ex As Exception

        End Try

    End Sub


    Protected Sub btnChangeNo_Click(sender As Object, e As EventArgs) Handles btnChangeNo.Click
        Try
            DIV_Clear()
            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""
            txtEnterMobileNo.Text = ""
            gdvCustomerDetails.DataSource = Nothing
            gdvCustomerDetails.DataBind()
            grdAddRecepient.DataSource = Nothing
            grdAddRecepient.DataBind()
            txtEnterOTP.Text = ""
            txtEnterMobileNo.ReadOnly = False
            btnSearchCustomerGo.Enabled = True
            btnSearchCustomerGo.CssClass = "btn btn-primary"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddRecepient_Click(sender As Object, e As EventArgs) Handles btnAddRecepient.Click
        Try
            txtBankAccountNo.Text = ""
            txtRecepientMobileNo.Text = ""
            txtIFSCCode.Text = ""
            Div_TransferAmt.Visible = False
            Div_AddRecepient.Visible = True

            For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                grdAddRecepient.Rows(i).BackColor = Color.White
            Next

            ddlSelectBank.Items.Clear()
            GV.FL.AddInDropDownListDistinct(ddlSelectBank, "name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList order by name ")
            Dim bankstr As String = "Select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList order by name "
            Dim bankds As DataSet = New DataSet
            bankds = GV.FL.OpenDsWithSelectQuery(bankstr)
            For i As Integer = 0 To bankds.Tables(0).Rows.Count - 1
                Dim BankName As String = bankds.Tables(0).Rows(i).Item("name").ToString()
                Dim BankCode As String = bankds.Tables(0).Rows(i).Item("Code").ToString()
                ddlSelectBank.Items.Add(BankName)
                ddlSelectBank.Items(i).Value = BankCode
                ddlSelectBank.Items(i).Text = GV.parseString(BankName)
            Next

            If ddlSelectBank.Items.Count > 0 Then
                ddlSelectBank.Items.Insert(0, ":::: Select Bank ::::")
            Else
                ddlSelectBank.Items.Add(":::: Select Bank ::::")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddRecepientGo_Click(sender As Object, e As EventArgs) Handles btnAddRecepientGo.Click
        Dim VerifyMessage1 As String = ""
        Try
            lblRecepientError.Text = ""
            lblRecepientError.CssClass = ""


            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If

            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VerifyServiceCharge) Then
            Else
                lblRecepientError.Text = "You Have Insufficient Wallet Amount, Service Charge Applicable Rs " & VerifyServiceCharge
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If




            If ddlSelectBank.SelectedIndex = 0 Then
                lblRecepientError.Text = "Please Select Bank."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtRecepientMobileNo.Text = "" Then
                lblRecepientError.Text = "Please Enter Mobile No."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtIFSCCode.Text = "" Then
                lblRecepientError.Text = "Please Enter IFSC."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtBankAccountNo.Text = "" Then
                lblRecepientError.Text = "Please Enter Bank AccountNo."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If

            APIResult = GetApiResult("Bank_Details_API_Parameters")

            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim name As String = ""
            Dim CustomerID As String = ""
            Dim MobileNO As String = ""
            VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
            If VerifyMessage1 = "Success ! Found bank Details for given Ifsc" Then
                APIResult = GetApiResult("Verify_Bank_Details_API_Parameters")
                json_ = APIResult
                ser_ = JObject.Parse(json_)
                VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
                Dim Status As String = ser_.SelectToken("status").ToString.Trim
                If Status.Trim = "0" Then

                    Dim recipient_name As String = ser_.SelectToken("data").SelectToken("recipient_name").ToString.Trim
                    Dim recipient_ifsc As String = ser_.SelectToken("data").SelectToken("ifsc").ToString.Trim
                    Dim recipient_account As String = ser_.SelectToken("data").SelectToken("account").ToString.Trim
                    Dim recipient_BankName As String = ser_.SelectToken("data").SelectToken("bank").ToString.Trim


                    lblRecepientActualName.Text = recipient_name
                    APIResult = GetApiResult("Add_New_Recipients_API_Parameters")
                    json_ = APIResult
                    ser_ = JObject.Parse(json_)

                    VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
                    Dim New_RecipientsStatus As String = ser_.SelectToken("status").ToString.Trim
                    If New_RecipientsStatus.Trim = "0" Then




                        Fill_Recepient_List()
                        txtBankAccountNo.Text = ""
                        txtRecepientMobileNo.Text = ""
                        txtIFSCCode.Text = ""
                        'Div_TransferAmt.Visible = False
                        Div_AddRecepient.Visible = False
                    Else
                        lblRecepientError.Text = VerifyMessage1
                        lblRecepientError.CssClass = "errorlabels"
                        Exit Sub
                    End If


                Else
                    lblRecepientError.Text = VerifyMessage1
                    lblRecepientError.CssClass = "errorlabels"
                    Exit Sub
                End If

                ''//// Service Charge Deduction
                Dim VTransId As String = GV.FL.getAutoNumber("TransId")
                Dim Ret_Id As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                Dim SDtypecommFrom1 As String = "Your Account is debited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Account Verify on RegID " & Ret_Id & "."
                Dim SDtypecommTo1 As String = "Your Account is credited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Account Verify on RegID " & Ret_Id & " ."
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VTransId & "','" & VerifyServiceCharge & "','" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Service Charge','Service Charge',getdate(),'" & Ret_Id & "','ADMIN','" & VerifyServiceCharge & "',getdate(),'" & Ret_Id & "',getdate() ) ;"
                GV.FL.DMLQueriesBulk(QryStr)
                ''//// End Service Charge Deduction


                '/////////////////////////////
            Else
                lblRecepientError.Text = VerifyMessage1
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If

        Catch ex As Exception
            lblRecepientError.Text = VerifyMessage1
            lblRecepientError.CssClass = "errorlabels"
        End Try
    End Sub

    Protected Sub btnGrdRowTransfer_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblRBankName.Text = ""
            lblIFSCCode.Text = ""
            lblReceipentName.Text = ""
            lblReceipentId.Text = ""
            lblReceipentMobileNo.Text = ""
            lblTranferAmtError.Text = ""
            lblTranferAmtError.CssClass = ""
            txtServiceCharge.Text = ""
            txtNetAmount.Text = ""
            txtEnterAmt.Text = ""
            lblService.Text = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex

            For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                grdAddRecepient.Rows(i).BackColor = Color.White
            Next

            grdAddRecepient.Rows(lblRowIndex.Text).BackColor = Color.LightGreen
            grdAddRecepient.Rows(lblRowIndex.Text).Cells(0).BackColor = Color.White

            lblRBankName.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(2).Text)
            lblIFSCCode.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(3).Text)
            lblRAccountNo.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(4).Text)
            lblReceipentName.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(5).Text)
            lblReceipentId.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(6).Text)
            lblReceipentMobileNo.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(7).Text)
            Div_AddRecepient.Visible = False
            Div_TransferAmt.Visible = True



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnTranferAmt_Click(sender As Object, e As EventArgs) Handles btnTranferAmt.Click
        Dim VerifyMessage1 As String = ""
        Try
            lblTranferAmtError.Text = ""
            lblTranferAmtError.CssClass = ""

            '///// Start Check API  STATUS Super ADmin Level

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Super ADmin Level

            '///// Start Check API  STATUS System Settings 

            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblTranferAmtError.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblTranferAmtError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 







            If txtEnterAmt.Text.Trim = "" Then
                lblTranferAmtError.Text = "Please Enter Amount."
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If



            Dim VNetAmount As Decimal = 0
            If txtNetAmount.Text.Trim = "" Then
                VNetAmount = 0
            Else
                VNetAmount = GV.parseString(txtNetAmount.Text.Trim)
            End If

            If VNetAmount <= 0 Then
                lblTranferAmtError.Text = "Amount Should be Greater Than 0."
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If

            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
            Else
                lblTranferAmtError.Text = "You Have Insufficient Wallet Amount"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// Check For API Balance - Start //////
            If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                lblTranferAmtError.Text = "Insufficient API Balance."
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// Check For API Balance - End //////

            If txtTransactionPin.Text = "" Then
                lblTranferAmtError.Text = "Please Enter Your Transaction Pin."
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim TransPiNo As String = ""
            TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If TransPiNo.Trim = txtTransactionPin.Text.Trim Then
            Else
                lblTranferAmtError.Text = "Invalid Transaction Pin"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim TranscationId, OrderNO As String
            TranscationId = ""
            OrderNO = ""
            APIResult = GetApiResult("Money_Transfer_API_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
            Dim Status As String = ser_.SelectToken("status").ToString.Trim
            Dim data_ As List(Of JToken) = ser_.Children().ToList
            For Each item As JProperty In data_
                item.CreateReader()
                Select Case item.Name

                    Case "data"
                        Dim data1 As List(Of JToken) = item.Children().ToList
                        For Each msg As JObject In data1

                            '/// Fix Attribute Name and get its value
                            TranscationId = msg("tid")
                            OrderNO = msg("client_ref_id")
                        Next

                End Select
            Next


            Dim IFSC, AccountNo, Receipent, ReceipentId As String

            IFSC = lblIFSCCode.Text.Trim
            AccountNo = lblRAccountNo.Text.Trim
            Receipent = lblReceipentName.Text
            ReceipentId = lblReceipentId.Text.Trim


            Dim VUpdatedBy, VUpdatedOn, VRecord_DateTime As String
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"
            VRecord_DateTime = "getdate()"
            Dim Qry As String = "Insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MoneyTransfer_API (Refund_Status,TransIpAddress,IFSC,AccountNo,Receipent,ReceipentId,TransId,APIStatus,APIMessage,TransferDate,OrderNo,RefrenceNo,TranscationId,CustomerID,MobileNo,Amount,BankName,Method,Process,RecordDateTime,UpdatedBy,UpdatedOn) values ('No','" & GV.parseString(GV.GetIPAddress) & "','" & IFSC & "','" & AccountNo & "','" & Receipent & "','" & ReceipentId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & Status & "','" & VerifyMessage1 & "','" & Now.Date & "','" & OrderNO & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & TranscationId & "','" & GV.parseString(txtEnterMobileNo.Text.Trim) & "','" & GV.parseString(lblReceipentMobileNo.Text.Trim) & "'," & GV.parseString(txtEnterAmt.Text.Trim) & ",'" & GV.parseString(lblRBankName.Text.Trim) & "','" & GV.parseString(ddlTransferMode.SelectedItem.Text.Trim) & "','Money Transfer',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate() )"
            Dim Result As Boolean = False
            Result = GV.FL.DMLQueries(Qry)
            If Result = True Then
                'lblTranferAmtError.Text = VerifyMessage1

                If Status = "0" Then

                    If CDec(txtEnterAmt.Text.Trim) > 0 Then 'previous 2500
                        Dim TypeName As String = "Money Transfer"
                        Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                        If GRP = "Retailer".ToUpper Then
                            RechargeCommision()
                            If Not lblRID.Text = "" Then

                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim DisID_Com() As String = AAID(1).Split(":")
                                Dim SubDIsID_Com() As String = AAID(2).Split(":")
                                Dim RetailerID_Com() As String = AAID(3).Split(":")

                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)
                                Dim DisID As String = DisID_Com(0)
                                Dim DisCom As String = DisID_Com(1)
                                Dim SUBDisID As String = SubDIsID_Com(0)
                                Dim SUBDisCom As String = SubDIsID_Com(1)
                                Dim RTEID As String = RetailerID_Com(0)
                                Dim RTECom As String = RetailerID_Com(1)


                                Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."

                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."


                                Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txtEnterAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                '//// Retailer Commission Calculation - END



                                'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    ' ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt.Text.Trim & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt.Text.Trim & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                GV.FL.DMLQueriesBulk(Qry)

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



                                Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim Custtypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."

                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."
                                Dim CusttypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt.Text.Trim & "."


                                Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & txtEnterAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                '//// Distributor Commission Calculation - Start


                                '//// customer Commission Calculation - Start
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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CusttypecommTo & "','" & Custtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                '//// customer Commission Calculation - END



                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    ' ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt.Text.Trim & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt.Text.Trim & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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

                            If GV.parseString(txtEnterAmt.Text.Trim) = "" Then
                                V_Amount = "0"
                            Else
                                V_Amount = txtEnterAmt.Text.Trim
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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."

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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."

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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            '//// Super Admin Commission Calculation - End
                        End If
                        '//// Admin & Super Admin Commission Calculation - End
                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

                        If Not Qry.Trim = "" Then
                            GV.FL.DMLQueriesBulk(Qry)
                        End If


                    Else
                        ''Only Service Charge
                        ''Commented due to condition not require 


                        'Dim TypeName As String = "Money Transfer"

                        'Dim ServiceCharge As Decimal = 0
                        'Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)


                        'Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                        'Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                        'Qry = "insert into BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTE & "','SuperAdmin','" & txtEnterAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"


                        'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                        '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                        '    If ServiceCharge > 0 Then

                        '        Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName
                        '        Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName
                        '        Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                        '    End If
                        'End If
                        'GV.FL.DMLQueriesBulk(Qry)
                    End If

                End If


                lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                lblPopTransactionID.Text = TranscationId
                lblPopTransferAmt.Text = GV.parseString(txtEnterAmt.Text.Trim)
                lblPopStatus.Text = VerifyMessage1
                lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                lblPopServiceCharge.Text = txtServiceCharge.Text.Trim



                ModalPopupExtender3.Show()



            End If
            'lblTranferAmtError.Text = VerifyMessage1

            'If Status = "0" Then
            '    lblTranferAmtError.CssClass = "Successlabels"
            'Else
            '    lblTranferAmtError.CssClass = "errorlabels"
            'End If

        Catch ex As Exception


            If VerifyMessage1.Trim = "" Then
                lblTranferAmtError.Text = ""
                lblTranferAmtError.CssClass = ""
            Else
                lblTranferAmtError.Text = VerifyMessage1
                lblTranferAmtError.CssClass = "errorlabels"
            End If
            lblTranferAmtError.CssClass = ""
            lblTranferAmtError.Text = APIResult
        End Try
    End Sub

    Protected Sub bntReceipientClose_Click(sender As Object, e As EventArgs) Handles bntReceipientClose.Click
        Try
            txtBankAccountNo.Text = ""
            txtRecepientMobileNo.Text = ""
            txtIFSCCode.Text = ""
            lblRecepientError.Text = ""
            lblRecepientError.CssClass = ""
            Div_AddRecepient.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btntransferClose_Click(sender As Object, e As EventArgs) Handles btntransferClose.Click
        Try
            txtEnterAmt.Text = ""
            lblTranferAmtError.Text = ""
            lblTranferAmtError.CssClass = ""
            lblRBankName.Text = ""
            lblIFSCCode.Text = ""
            lblReceipentName.Text = ""
            lblReceipentId.Text = ""
            lblReceipentMobileNo.Text = ""
            Div_TransferAmt.Visible = False
            For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                grdAddRecepient.Rows(i).BackColor = Color.White
            Next





        Catch ex As Exception

        End Try
    End Sub


    Public Sub RechargeCommision()
        Try

            Dim VCommissionType, VSub_Dis_CommissionType, VRetailer_CommissionType As String
            VCommissionType = ""
            VSub_Dis_CommissionType = ""
            VRetailer_CommissionType = ""
            Dim VCommission, VSub_Dis_Commission, VRetailer_Commission As Decimal
            VCommission = 0
            VSub_Dis_Commission = 0
            VRetailer_Commission = 0


            Dim VContainCategory, VCanChange, VSlabApplicable As String
            VContainCategory = ""
            VCanChange = ""
            VSlabApplicable = ""



            Dim VadminComAmt, DistributorComAmt, SubDIsComAmt, VRetailerComAmt As Decimal
            VadminComAmt = 0
            DistributorComAmt = 0
            SubDIsComAmt = 0
            VRetailerComAmt = 0



            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalDISAmt, VFinalSUBDISAmt, VFinalRETAILERAmt As Decimal
            VFinaladminAmt = 0
            VFinalDISAmt = 0
            VFinalSUBDISAmt = 0
            VFinalRETAILERAmt = 0

            Dim SubDisID As String = ""
            Dim DisID As String = ""
            Dim AdminID As String = ""
            Dim qry As String = ""

            SubDisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "'")
            DisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "'")
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer' and ActiveStatus='Active'"
            ds = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = GV.parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If


                        If VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then

                            '/// Start With Slab

                            Dim Amount1 As String = GV.parseString(txtEnterAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Money Transfer'; "

                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dis_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Dis_CommissionType").ToString() = "" Then
                                                VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Dis_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dis_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Dis_Commission").ToString() = "" Then
                                                VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Dis_Commission").ToString())
                                            End If
                                        End If

                                        If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                        ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                            DistributorComAmt = (VCommission)
                                        End If


                                        '/////// End Distributor


                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                                VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                                VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                            End If
                                        End If

                                        If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                        ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SubDIsComAmt = (VSub_Dis_Commission)
                                        End If

                                        '/////// End  Sub Distributor


                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                                VRetailer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                                VRetailer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                            End If
                                        End If

                                        If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                        ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            VRetailerComAmt = (VRetailer_Commission)
                                        End If

                                        '/////// End  Retailer

                                        VFinaladminAmt = VadminComAmt
                                        VFinalDISAmt = DistributorComAmt
                                        VFinalSUBDISAmt = SubDIsComAmt
                                        VFinalRETAILERAmt = VRetailerComAmt


                                    End If
                                End If
                            End If

                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            '/// End With Slab

                        Else
                            '//// Start Without Slab

                            If VContainCategory.Trim.ToUpper = "YES" Then




                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                                Dim Amount1 As String = GV.parseString(txtEnterAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then

                                    DistributorComAmt = (VCommission)
                                End If

                                '/////// End Distributor



                                qry = " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Money Transfer' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                                qry = qry & " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Money Transfer' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




                                ds = New DataSet
                                ds = GV.FL.OpenDsWithSelectQuery(qry)
                                If Not ds Is Nothing Then
                                    If ds.Tables.Count > 0 Then
                                        If ds.Tables(0).Rows.Count > 0 Then


                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                                If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                                    VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                                If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                                    VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                                End If
                                            End If

                                            If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                            ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                SubDIsComAmt = (VSub_Dis_Commission)
                                            End If

                                            '/////// End  Sub Distributor
                                        End If
                                        '/////// End  Sub Distributor

                                        If ds.Tables.Count > 1 Then
                                            If ds.Tables(1).Rows.Count > 0 Then

                                                If Not IsDBNull(ds.Tables(1).Rows(0).Item("CommissionType")) Then
                                                    If Not ds.Tables(1).Rows(0).Item("CommissionType").ToString() = "" Then
                                                        VRetailer_CommissionType = GV.parseString(ds.Tables(1).Rows(0).Item("CommissionType").ToString())
                                                    End If
                                                End If

                                                If Not IsDBNull(ds.Tables(1).Rows(0).Item("Commission")) Then
                                                    If Not ds.Tables(1).Rows(0).Item("Commission").ToString() = "" Then
                                                        VRetailer_Commission = GV.parseString(ds.Tables(1).Rows(0).Item("Commission").ToString())
                                                    End If
                                                End If

                                                If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                    VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                                ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                    VRetailerComAmt = (VRetailer_Commission)
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

                                Dim Amount1 As String = GV.parseString(txtEnterAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    DistributorComAmt = (VCommission)
                                End If


                                '/////// End Distributor



                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                        VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                        VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                    End If
                                End If

                                If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    SubDIsComAmt = (VSub_Dis_Commission)
                                End If

                                '/////// End  Sub Distributor




                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                        VRetailer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                        VRetailer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                    End If
                                End If

                                If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VRetailerComAmt = (VRetailer_Commission)
                                End If

                                '/////// End  Retailer


                                VFinaladminAmt = VadminComAmt
                                VFinalDISAmt = DistributorComAmt
                                VFinalSUBDISAmt = SubDIsComAmt
                                VFinalRETAILERAmt = VRetailerComAmt
                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            End If

                            '/// End Without Slab
                        End If




                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////
        Catch ex As Exception

        End Try
    End Sub
    Public Sub RechargeCommision_Customer()
        Try

            Dim VCommissionType, VCustomer_CommissionType As String
            VCommissionType = ""
            VCustomer_CommissionType = ""

            Dim VCommission, VCustomer_Commission As Decimal
            VCommission = 0
            VCustomer_Commission = 0

            Dim VContainCategory, VCanChange, VSlabApplicable As String
            VContainCategory = ""
            VCanChange = ""
            VSlabApplicable = ""



            Dim VadminComAmt, VCustomerComAmt As Decimal
            VadminComAmt = 0
            VCustomerComAmt = 0



            Dim CustomerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalCustomerAmt As Decimal
            VFinaladminAmt = 0
            VFinalCustomerAmt = 0

            Dim AdminID As String = ""

            Dim qry As String = ""

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer' and ActiveStatus='Active'"
            ds = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = GV.parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If


                        If VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then

                            '/// Start With Slab

                            Dim Amount1 As String = GV.parseString(txtEnterAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Money Transfer'; "

                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                                VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                                VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                            End If
                                        End If

                                        If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                        ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                            VCustomerComAmt = (VCommission)
                                        End If


                                        '/////// End Distributor




                                        '/////// End  Retailer

                                        VFinaladminAmt = VadminComAmt
                                        VFinalCustomerAmt = VCustomerComAmt




                                    End If
                                End If
                            End If

                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            '/// End With Slab

                        Else
                            '//// Start Without Slab

                            If VContainCategory.Trim.ToUpper = "YES" Then




                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                                Dim Amount1 As String = GV.parseString(txtEnterAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VCustomerComAmt = (VCommission)
                                End If

                                '/////// End Distributor



                                VFinaladminAmt = VadminComAmt
                                VFinalCustomerAmt = VCustomerComAmt

                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper


                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                                Dim Amount1 As String = GV.parseString(txtEnterAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VCustomerComAmt = (VCommission)
                                End If


                                '/////// End Distributor


                                VFinaladminAmt = VadminComAmt
                                VFinalCustomerAmt = VCustomerComAmt

                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper
                            End If

                            '/// End Without Slab
                        End If




                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtEnterAmt_TextChanged(sender As Object, e As EventArgs) Handles txtEnterAmt.TextChanged
        Try
            txtServiceCharge.Text = ""
            txtNetAmount.Text = ""
            lblService.Text = ""
            If txtEnterAmt.Text.Trim = "" Then
                Exit Sub
            End If
            Dim NetAmount As Decimal = 0
            Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer'").Split(":")

            If CDec(txtEnterAmt.Text.Trim) > 1000 Then

                If Service.Length > 1 Then
                    If Service(1).Trim = "Percentage" Then
                        lblService.Text = Service(0) & " %"
                        NetAmount = (CDec(txtEnterAmt.Text.Trim) * CDec(Service(0))) / 100
                        'If NetAmount < 25 Then
                        '    NetAmount = 25
                        'End If
                    ElseIf Service(1).Trim = "Amount" Then
                        lblService.Text = Service(0)
                        NetAmount = CDec(Service(0))
                    ElseIf Service(1).Trim = "Not Applicable" Then
                        lblService.Text = Service(0)
                        NetAmount = CDec(Service(0))
                    End If


                End If
            Else
                NetAmount = 10
            End If

            txtServiceCharge.Text = NetAmount
            txtNetAmount.Text = CDec(GV.parseString(txtEnterAmt.Text.Trim)) + NetAmount

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Try
            btnChangeNo.OnClientClick = "printdiv('DIV_PrintReceipt');"
            'Dim btn As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton2"), LinkButton)
            'If Not btn Is Nothing Then
            '    btn.OnClientClick = "window.open('../admin/Print_Installment_Report.aspx?PaymentID=" & GV.parseString(GridView1.Rows(i).Cells(7).Text) & "','_blank');"
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlGateway_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGateway.SelectedIndexChanged
        Try
            lblError_Gateway.Text = ""
            lblError_Gateway.CssClass = ""
            Dim MoneyTrasfer As String = ""
            If ddlGateway.SelectedValue.Trim.ToUpper = "Money Transfer".Trim.ToUpper Then
                MoneyTrasfer = "MoneyTransferAPI_Status"
            Else
                MoneyTrasfer = "MoneyTransferAPI_2_Status"
            End If


            '///// Start Check API  STATUS Super ADmin Level

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Super ADmin Level

            '///// Start Check API  STATUS System Settings 

            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If


        Catch ex As Exception

        End Try
    End Sub

End Class