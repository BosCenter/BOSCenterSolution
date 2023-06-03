Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports RestSharp
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims


Public Class testAPI
    Inherits System.Web.UI.Page



    '///  ALL Recharge API URL  - Start
    Dim Recharge_API_URL As String = "https://www.boscenter.in/api/GenerateUCC"

    'Dim Recharge_API_URL As String = "http://localhost:56380/api/GenerateUCC"


    '///  ALL Recharge API URL  - End
    'http://localhost:56380/
    Public Function ReadbyRestClient(Urls As String, Parameter As String) As String
        Dim str As String = ""
        Try
            'Dim ddd As 

            'Dim symmetricKey = Convert.FromBase64String("UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh")
            'Dim tokenHandler As New JwtSecurityTokenHandler()

            'Dim SecurityKey As New Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh"))
            'Dim Credentials As New Microsoft.IdentityModel.Tokens.SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)

            'Dim header As New JwtHeader(Credentials)
            'Dim unixEpoch = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            'Dim Now = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds)

            'Dim payload As New JwtPayload()
            'payload.AddClaim(New Claim("timestamp", Now))
            'payload.AddClaim(New Claim("partnerId", "PS00343"))
            'payload.AddClaim(New Claim("reqid", Now))

            'Dim secToken As New JwtSecurityToken(header, payload)

            'Dim handler As New JwtSecurityTokenHandler()
            'Dim tokenString = handler.WriteToken(secToken)

            'Dim token = handler.ReadJwtToken(tokenString)

            'Dim dd As String = token.RawData

            'ServicePointManager.Expect100Continue = True
            'ServicePointManager.DefaultConnectionLimit = 9999
            'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12



            Dim client As New RestClient(Urls)
            Dim request As New RestSharp.RestRequest(RestSharp.Method.POST)
            request.AddHeader("Accept", "application/json")
            request.AddHeader("Content-Type", "application/json")
            request.AddParameter("application/json", Parameter, RestSharp.ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim

        Catch ex As Exception
            Return ex.Message
        End Try
        Return str

    End Function

    'request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
    'request.AddHeader("cache-control", "no-cache")
    'Dim request = New RestRequest(Method.POST)
    'request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
    'request.AddHeader("cache-control", "no-cache")
    'request.AddHeader("content-type", "application/x-www-form-urlencoded")
    'request.AddParameter("application/x-www-form-urlencoded", Parameter, ParameterType.RequestBody)


    Public Class QueryRemitter_API_Parameters
        Dim VMobile, Vbank3_flag As String
        Public Property mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property bank3_flag() As String
            Get
                Return Vbank3_flag
            End Get
            Set(ByVal value As String)
                Vbank3_flag = value
            End Set
        End Property

    End Class
    Protected Sub btnCallAPI_Click(sender As Object, e As EventArgs) Handles btnCallAPI.Click
        Try

            lblResult.Text = ""
            Dim r As New dataCUST()
            r.merchantcode = "TZ1233235YD874"
            r.type = 1


            Dim s As String = Newtonsoft.Json.JsonConvert.SerializeObject(r)


            Dim ApiResult As String = ReadbyRestClient(Recharge_API_URL, s)
            lblResult.Text = ApiResult
        Catch ex As Exception

        End Try


    End Sub

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  ALL Recharge API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    Public Class dataCUST
        Dim Vmerchantcode As String
        Dim Vtype As Integer
        Public Property merchantcode() As String
            Get
                Return Vmerchantcode
            End Get
            Set(ByVal value As String)
                Vmerchantcode = value
            End Set
        End Property

        Public Property type() As Integer
            Get
                Return Vtype
            End Get
            Set(ByVal value As Integer)
                Vtype = value
            End Set
        End Property
    End Class

End Class