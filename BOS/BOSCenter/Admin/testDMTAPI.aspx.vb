Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports RestSharp

Public Class testDMTAPI
    Inherits System.Web.UI.Page


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

    Protected Sub btnCallDMTAPI_Click(sender As Object, e As EventArgs) Handles btnCallDMTAPI.Click
        Try
            lblResult.Text = ""
            lblResult.Text = GetApiResult("Verify_Customer_API_Parameters")
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetApiResult(APIMethod As String) As String
        Dim ApiResult As String = ""
        Dim StrParameters As String = ""
        Dim API_URLS As String = ""

        Try

            If APIMethod = "Verify_Customer_API_Parameters" Then
                Dim setParameter_API_Obj As New Verify_Customer_API_Parameters()
                setParameter_API_Obj.Mobile = txtPhoneNumber.Text.Trim
                setParameter_API_Obj.Key = txtApiKey.Text
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Verify_Customer_API_URL
            ElseIf APIMethod = "Receipent_List_API_Parameters" Then
                Dim setParameter_API_Obj As New Receipent_List_API_Parameters()
                setParameter_API_Obj.Mobile = txtPhoneNumber.Text.Trim
                setParameter_API_Obj.Key = txtApiKey.Text
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Receipent_List_API_URL
            ElseIf APIMethod = "Recipients_Details_API_Parameters" Then
                Dim setParameter_API_Obj As New Recipients_Details_API_Parameters()
                setParameter_API_Obj.Mobile = txtPhoneNumber.Text.Trim
                setParameter_API_Obj.Key = txtApiKey.Text
                setParameter_API_Obj.Recipient_Id = "81721898"
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Recipients_Details_API_URL
            ElseIf APIMethod = "Bank_Details_API_Parameters" Then
                Dim setParameter_API_Obj As New Bank_Details_API_Parameters()
                setParameter_API_Obj.BankCode = ""
                setParameter_API_Obj.ifsc = ""
                setParameter_API_Obj.Key = txtApiKey.Text
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Bank_Details_API_URL
            ElseIf APIMethod = "Verify_Bank_Details_API_Parameters" Then
                Dim setParameter_API_Obj As New Verify_Bank_Details_API_Parameters()
                setParameter_API_Obj.AccountNo = ""
                setParameter_API_Obj.BankAccountCode = ""
                setParameter_API_Obj.MobileNo = ""
                setParameter_API_Obj.Key = txtApiKey.Text
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Verify_Bank_Details_API_URL
            ElseIf APIMethod = "Money_Transfer_API_Parameters" Then
                Dim setParameter_API_Obj As New Money_Transfer_API_Parameters()

                setParameter_API_Obj.recipient_id = ""
                setParameter_API_Obj.cust_id = ""
                setParameter_API_Obj.amount = ""
                setParameter_API_Obj.merchant_document_id_type = ""
                setParameter_API_Obj.merchant_document_id = ""
                setParameter_API_Obj.channel = ""
                setParameter_API_Obj.Recipent = ""
                setParameter_API_Obj.IFSC = ""
                setParameter_API_Obj.Key = txtApiKey.Text

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Money_Transfer_API_URL
            ElseIf APIMethod = "Add_New_Customer_API_Parameters" Then
                Dim setParameter_API_Obj As New Add_New_Customer_API_Parameters

                setParameter_API_Obj.Mobile = "9211747877"
                setParameter_API_Obj.Name = "Eklavya verma"
                setParameter_API_Obj.DOB = "10/dec/1999"
                setParameter_API_Obj.Key = txtApiKey.Text

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Add_New_Customer_API_URL
            ElseIf APIMethod = "Add_New_Recipients_API_Parameters" Then
                Dim setParameter_API_Obj As New Add_New_Recipients_API_Parameters

                setParameter_API_Obj.Mobile = ""
                setParameter_API_Obj.BankCode = ""
                setParameter_API_Obj.recipient_name = ""
                setParameter_API_Obj.IfscCode = ""
                setParameter_API_Obj.AccountNo = ""
                setParameter_API_Obj.customermobile = ""
                setParameter_API_Obj.Key = txtApiKey.Text

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Add_New_Recipients_API_URL

            ElseIf APIMethod = "VerifyOtp_API_Parameters" Then
                Dim setParameter_API_Obj As New VerifyOtp_API_Parameters

                setParameter_API_Obj.Mobile = ""
                setParameter_API_Obj.Otp = ""
                setParameter_API_Obj.Key = txtApiKey.Text

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = VerifyOtp_API_URL
            ElseIf APIMethod = "ResendOtp_API_Parameters" Then
                Dim setParameter_API_Obj As New ResendOtp_API_Parameters

                setParameter_API_Obj.Mobile = ""
                setParameter_API_Obj.Key = txtApiKey.Text

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = ResendOtp_API_URL
            End If


            ApiResult = ReadbyRestClient(API_URLS, StrParameters)
        Catch ex As Exception
            Return ApiResult
        End Try
        Return ApiResult
    End Function

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX









    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  ALL Recharge API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    '///  ALL Recharge API URL  - Start
    Dim Recharge_API_URL As String = "https://www.vacsc.com/API/AllRecharge"
    '///  ALL Recharge API URL  - End

    Public Function ReadbyRestClient(Urls As String, Parameter As String) As String
        Dim str As String = ""
        Try
            'https://dotnetco.de/setup-post-rest-webservice-asp-net-vb-net/
            'https://github.com/restsharp/RestSharp/downloads


            Dim client = New RestClient(Urls)
            Dim request = New RestRequest(Method.POST)
            request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("content-type", "application/x-www-form-urlencoded")
            request.AddParameter("application/x-www-form-urlencoded", Parameter, ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim
        Catch ex As Exception
            Return str
        End Try
        Return str

    End Function
    Public Class PartnerRechargeRequest

        Dim VOperatorCode, VOrderID, VNumber, VTypeName, VAPIKey As String
        Dim VAmount As Double

        Public Property OperatorCode() As String
            Get
                Return VOperatorCode
            End Get
            Set(ByVal value As String)
                VOperatorCode = value
            End Set
        End Property

        Public Property OrderID() As String
            Get
                Return VOrderID
            End Get
            Set(ByVal value As String)
                VOrderID = value
            End Set
        End Property

        Public Property Number() As String
            Get
                Return VNumber
            End Get
            Set(ByVal value As String)
                VNumber = value
            End Set
        End Property

        Public Property Amount() As Double
            Get
                Return VAmount
            End Get
            Set(ByVal value As Double)
                VAmount = value
            End Set
        End Property

        Public Property TypeName() As String
            Get
                Return VTypeName
            End Get
            Set(ByVal value As String)
                VTypeName = value
            End Set
        End Property

        Public Property APIKey() As String
            Get
                Return VAPIKey
            End Get
            Set(ByVal value As String)
                VAPIKey = value
            End Set
        End Property
    End Class
    Protected Sub btnCallAPI_Click(sender As Object, e As EventArgs) Handles btnCallAPI.Click
        Try

            lblResult.Text = ""
            Dim r As New PartnerRechargeRequest()
            r.Amount = txtAmount.Text
            r.APIKey = txtApiKey.Text
            r.Number = txtPhoneNumber.Text
            r.OperatorCode = txtOperatorName.Text
            r.OrderID = txtOrderID.Text
            r.TypeName = txtTypeName.Text
            Dim s As String = Newtonsoft.Json.JsonConvert.SerializeObject(r)




            Dim ApiResult As String = ReadbyRestClient(Recharge_API_URL, s)



            lblResult.Text = ApiResult
            Dim json1 As JObject = JObject.Parse(ApiResult)
            Dim Vstatus As String = json1.SelectToken("status").ToString
            Dim VDescription As String = json1.SelectToken("Description").ToString
            Dim VOrderID As String = json1.SelectToken("OrderID").ToString
            Dim VUserID As String = json1.SelectToken("UserID").ToString



            VUserID = VUserID
        Catch ex As Exception

        End Try


    End Sub

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  ALL Recharge API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX





    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  PAN CARD API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    '///  PAN CARD API URL  - Start
    Dim PAN_CARD_APPLY_API_URL As String = "https://www.vacsc.com/PANAPI/APPLY"
    '///  PAN CARD API URL  - End

    Public Function ReadbyRestClient_PANCARD(Urls As String, Parameter As String) As String
        Dim str As String = ""
        Try

            Dim client = New RestClient(Urls)
            Dim request = New RestRequest(Method.POST)
            request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("content-type", "application/x-www-form-urlencoded")
            request.AddParameter("application/x-www-form-urlencoded", Parameter, ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim
        Catch ex As Exception
            Return str
        End Try
        Return str

    End Function
    Public Class PAN_CARD_APPLY_API_Parameters

        Dim VKey As String
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
    End Class
    Protected Sub btnCall_PAN_API_Click(sender As Object, e As EventArgs) Handles btnCall_PAN_API.Click
        Try
            Dim StrParameters As String = ""
            lblResult.Text = ""
            Dim setParameter_API_Obj As New PAN_CARD_APPLY_API_Parameters()
            setParameter_API_Obj.VCCType = 0
            setParameter_API_Obj.Qty = 0
            setParameter_API_Obj.Key = txtApiKey.Text
            StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)

            Dim ApiResult As String = ReadbyRestClient_PANCARD(PAN_CARD_APPLY_API_URL, StrParameters)



            'lblResult.Text = ApiResult
            ''Dim json1 As JObject = JObject.Parse(ApiResult)
            ''Dim Vstatus As String = json1.SelectToken("status")
            ''Dim VDescription As String = json1.SelectToken("Description")
            ''Dim VOrderID As String = json1.SelectToken("OrderID")
            ''Dim VUserID As String = json1.SelectToken("UserID")



            'VUserID = VUserID
        Catch ex As Exception

        End Try


    End Sub

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  PAN CARD API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX








    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                'https://www.vacsc.com/API/AllRecharge?APIKey=UTwHXNFqMTAUPrW5wktuluSARpx7SQ2k3lFh14sZ&OpertareName=AR&OrderID=1111123&Number=9212345320&Amount=10&TypeName=AIRTEL
                'https://www.vacsc.com/API/AllRecharge?APIKey=UTwHXNFqMTAUPrW5wktuluSARpx7SQ2k3lFh14sZ&OpertareName=AR&OrderID=123&Number=9212345320&Amount=55&TypeName=RECH
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Public Sub fdfsdf()
    '    Dim origResponse As HttpWebResponse
    '    Dim objResponse As HttpWebResponse
    '    Dim origReader As StreamReader
    '    Dim objReader As StreamReader

    '    Dim origRequest As HttpWebRequest = DirectCast(HttpWebRequest.Create("https://api.orls.voxeo.net/files/exports/2017/10/31?"), HttpWebRequest)
    '    origRequest.Headers.Add("Authorization", "Basic c3NhX2RzdDpBc3AzY3RfMTIzNA==")
    '    origRequest.AllowAutoRedirect = False
    '    origRequest.Method = "GET"
    '    Try
    '        origResponse = DirectCast(origRequest.GetResponse(), HttpWebResponse)
    '        Dim Stream As Stream = origResponse.GetResponseStream()
    '        Dim sr As New StreamReader(Stream, Encoding.GetEncoding("utf-8"))
    '        Dim str As String = sr.ReadToEnd()
    '        MessageBox.Show(str)
    '    Catch ex As WebException
    '        MsgBox(ex.Status)
    '        Dim myRequest As HttpWebRequest = DirectCast(HttpWebRequest.Create("https://api.orls.voxeo.net/files/exports/2017/10/31/Export-production-202715_20171030.zip.gpg"), HttpWebRequest)
    '        myRequest.Headers.Add("Authorization", "Basic c3NhX2RzdDpBc3AzY3RfMTIzNA==")
    '        Try
    '            objResponse = DirectCast(myRequest.GetResponse(), HttpWebResponse)

    '        Catch ex2 As WebException
    '            objReader = New StreamReader(objResponse.GetResponseStream())
    '            MsgBox(objReader.ReadToEnd())
    '            MsgBox(ex2.Status)
    '        End Try
    '    End Try
    'End Sub


    'Verify Customer API URL
    'Add New Customer API URL


    'Bank Details API URL
    'Verify Bank Details API URL

    'Add New Recipients API URL
    'Recipients Details API URL
    'Receipent List API URL


    'Money Transfer API URL









End Class