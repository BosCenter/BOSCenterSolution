Imports RestSharp
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Security.Cryptography
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class BOS_irctc_api
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim QryStr As String = ""

    Dim DS As New DataSet



    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()

            ddlCoupanType.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ApiResult As String = ""
        Try

            lblError.Text = ""
            lblError.CssClass = ""

            '///// Start Check API  STATUS Super ADmin Level 


            '/////// API

            If ddlCoupanType.SelectedItem.Text.Trim.ToUpper = "P Card".Trim.ToUpper Then

            ElseIf ddlCoupanType.SelectedItem.Text.Trim.ToUpper = "E Card".Trim.ToUpper Then

            End If

            Dim StrParameters As String = ""

            Dim sendJson As New Dictionary(Of String, String) From {
            {"username", ""},
            {"password", ""},
            {"amount", ""}
        }
            sendJson("username") = txtPartnerNo.Text.Trim
            sendJson("password") = txtPassword.Text.Trim
            sendJson("amount") = "100"

            '{"username":"9003056060,"password":"sivsiv123", "amount":"2"}


            'Dim setParameter_API_Obj As New data()
            'setParameter_API_Obj.data = JsonConvert.SerializeObject(sendJson, Formatting.Indented)

            'StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(sendJson)

            StrParameters = Encrypt_New(JsonConvert.SerializeObject(sendJson, Formatting.Indented))
            'StrParameters = Encrypt_New(setParameter_API_Obj.data)
            txtRequestDecrypt.Text = Decrypt_New(StrParameters)
            ApiResult = ReadbyRestClient_PANCARD(LoginAuth_API_URL, StrParameters)
            Dim json1 As JObject = JObject.Parse(ApiResult)
            txtResponseDecrypt.Text = Decrypt_New(json1.SelectToken("Message").ToString)

            'Dim Vstatus As String = json1.SelectToken("data").ToString
            'Dim Vmessage As String = json1.SelectToken("message").ToString
            'Dim VRecord_DateTime As String = "getdate()"
        Catch ex As Exception
            lblError.Text = ApiResult
        End Try




    End Sub
    Private Function Encrypt_New(ByVal text As String) As String
        Dim EncryptionKey As String = "59d15ufbt4dj5uot"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(text)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using

                text = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using

        Return text
    End Function

    Private Function Decrypt_New(ByVal cipher As String) As String
        Dim EncryptionKey As String = "59d15ufbt4dj5uot" '"MAKV2SPBNI99212"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipher)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using

                cipher = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

        Return cipher
    End Function



    Private Sub Clear()
        Try

            ddlCoupanType.SelectedIndex = 0

            lblSessionFlag.Text = 0
            btnSave.Text = "Proceed"
            lblError.Text = ""
            btnSave.Enabled = True
            lblError.CssClass = ""
            lblUpadate.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Public Class CustomersRest
        Public FirstName As String
        Public LastName As String
        Public CustomerID As Guid
    End Class
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtPartnerNo.Text = "9911477307"
                txtPassword.Text = "2RJ3D"


                Dim c As New CustomersRest
                c.FirstName = ""
                c.LastName = "Gates"
                c.CustomerID = Guid.Empty
                Dim RestURL As String = "https://example.com/api/customers/"
                Dim client As New HttpClient
                Dim JsonData As String = JsonConvert.SerializeObject(c)
                Dim RestContent As New StringContent(JsonData, Encoding.UTF8, "application/json")
                'Dim RestResponse As HttpResponseMessage = WebClient.PostAsync(RestURL, RestContent)
                'ResultMessage.Text = RestResponse.StatusCode.ToString

                '"{"FirstName":"Bill","LastName":"Gates","CustomerID":"00000000-0000-0000-0000-000000000000"}"
            End If

        Catch ex As Exception

        End Try
    End Sub


    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  PAN CARD API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    '///  PAN CARD API URL  - Start
    'Dim LoginAuth_API_URL As String = "http://localhost:56380/api/IrctcLoginAuth" '"https://boscenter.in/api/IrctcLoginAuth"
    Dim LoginAuth_API_URL As String = "https://boscenter.in/api/IrctcLoginAuth"
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
            API_Name = "IRCTC API"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text

            'Card_Type'
            Dim client = New RestClient(Urls)
            Dim request = New RestRequest(Method.POST)
            'request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("content-type", "application/x-www-form-urlencoded")
            'request.AddParameter("application/x-www-form-urlencoded", Parameter, ParameterType.RequestBody)
            request.RequestFormat = DataFormat.Json
            request.Method = Method.POST
            request.AddParameter("data", Parameter)
            'request.AddHeader("content-type", "application/json; charset=utf-8")
            'request.AddParameter("application/json; charset=utf-8", Parameter, ParameterType.RequestBody)


            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim
            Response_String = GV.parseString(str)
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            'GV.SaveTextToFile(LogString, Server.MapPath("PANCARDAPI_LOG.txt"), True)
        Catch ex As Exception
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            'GV.SaveTextToFile(LogString, Server.MapPath("PANCARDAPI_LOG.txt"), True)
            Return str
        End Try

        Return str

    End Function

    Public Class data
        Dim Vdata As String
        Public Property data() As String
            Get
                Return Vdata
            End Get
            Set(ByVal value As String)
                Vdata = value
            End Set
        End Property
    End Class


    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  PAN CARD API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

End Class