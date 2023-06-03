Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports RestSharp

Public Class testAPI
    Inherits System.Web.UI.Page



    'Add New Customer API URL
    'Verify Customer API URL

    'Bank Details API URL
    'Verify Bank Details API URL

    'Add New Recipients API URL
    'Recipients Details API URL
    'Receipent List API URL

    'Money Transfer API URL




    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  ALL Recharge API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    '///  ALL Recharge API URL  - Start
    Dim Recharge_API_URL As String = "https://www.vacsc.com/API/AllRecharge"
    '///  ALL Recharge API URL  - End

    Public Function ReadbyRestClient(Urls As String, Parameter As String) As String
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
        Catch ex As Exception

        End Try


    End Sub

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  ALL Recharge API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


End Class