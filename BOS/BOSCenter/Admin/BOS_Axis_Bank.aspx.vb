Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.Web.Script.Serialization
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims


Public Class BOS_Axis_Bank
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    'Dim baseURL As String = "https://api.paysprint.in/service-api"
    'Dim Generate_UCC_URL As String = baseURL & "/api/v1/service/axisbank-utm/axisutm/generateurl"
    'service/

    Dim Generate_UCC_URL As String = "https://api.paysprint.in/api/v1/service/axisbank-utm/axisutm/generateurl"

    'Need to Develop 2 recall Methods
    '///  AEPS API URL  - END

    Public Class Generate_UCC_Parameters
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

    Public Function GetApiResult(APIMethod As String) As String
        Dim ApiResult As String = ""
        Dim StrParameters As String = ""
        Dim API_URLS As String = ""
        Try
            If APIMethod = "Generate_UCC_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Generate_UCC_Parameters()
                setParameter_API_Obj.type = lblAccountType.Text
                setParameter_API_Obj.merchantcode = lblAgentID.Text
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Generate_UCC_URL
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

            API_Name = "Axis-Bank-Account"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text



            Dim tokenHandler As New JwtSecurityTokenHandler()

            Dim SecurityKey As New Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("UFMwMDgzM2U4Mzc3NmQ5MTlmNGI4ZDRmNjI3NjJiNGUwMDU0MzJi")) 'UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh
            Dim Credentials As New Microsoft.IdentityModel.Tokens.SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)

            Dim header As New JwtHeader(Credentials)

            Dim unixEpoch = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            Dim currTimeStamp = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds)

            Dim payload As New JwtPayload()
            payload.AddClaim(New Claim("timestamp", currTimeStamp))
            payload.AddClaim(New Claim("partnerId", "PS00833"))   'PS00343 - PS00833
            payload.AddClaim(New Claim("reqid", currTimeStamp))


            Dim secToken As New JwtSecurityToken(header, payload)

            Dim handler As New JwtSecurityTokenHandler()
            Dim tokenString = handler.WriteToken(secToken)

            Dim token = handler.ReadJwtToken(tokenString)

            Dim dd As String = token.RawData

            ServicePointManager.Expect100Continue = True
            ServicePointManager.DefaultConnectionLimit = 9999
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12



            Dim client As New RestClient(Urls)
            Dim request As New RestSharp.RestRequest(RestSharp.Method.POST)
            request.AddHeader("Accept", "application/json")
            request.AddHeader("Token", dd)
            request.AddHeader("Content-Type", "application/json")

            'request.AddHeader("Authorisedkey", "ZTE3MTYyZDI3YzNjY2Y2ZjE5N2M0NGRkNjg4YzAzYmE=")

            request.AddParameter("application/json", Parameter, RestSharp.ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim

            Response_String = GV.parseString(str)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine

            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            'GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
        Catch ex As Exception
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            'GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
            Return str
        End Try
        Return str
    End Function

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    Dim APIResult As String = ""

    Private Sub lnkbtn_StartAEPS_Click(sender As Object, e As System.EventArgs) Handles lnkbtn_StartAEPS.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""

            lblAccountType.Text = "1"
            lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            APIResult = GetApiResult("Generate_UCC_Parameters")

            '/// parse and read data in list format through json parse

            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim status As String = ser_.SelectToken("status").ToString.Trim
            Dim response_code As String = ser_.SelectToken("response_code").ToString.Trim
            Dim message As String = ser_.SelectToken("message").ToString.Trim
            Dim type As String = ""
            Dim data As String = ""

            If response_code = "1" Or response_code = "3" Then
                type = ser_.SelectToken("type").ToString.Trim
                data = ser_.SelectToken("data").ToString.Trim
                Response.Redirect(data)
            Else
                lblError.Text = message
                lblError.CssClass = "errorlabels"
            End If

        Catch ex As Exception
            lblError.Text = "Error At API Level"
            lblError.CssClass = "errorlabels"
        End Try
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Text = ""
                lblError.CssClass = ""

                lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                lblAgentType.Text = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkbtn_CurrentAccount_Click(sender As Object, e As EventArgs) Handles lnkbtn_CurrentAccount.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            lblAccountType.Text = "2"
            lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            APIResult = GetApiResult("Generate_UCC_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim status As String = ser_.SelectToken("status").ToString.Trim
            Dim response_code As String = ser_.SelectToken("response_code").ToString.Trim
            Dim message As String = ser_.SelectToken("message").ToString.Trim
            Dim type As String = ""
            Dim data As String = ""

            If response_code = "1" Or response_code = "3" Then
                type = ser_.SelectToken("type").ToString.Trim
                data = ser_.SelectToken("data").ToString.Trim
                Response.Redirect(data)
            Else
                lblError.Text = message
                lblError.CssClass = "errorlabels"
            End If

        Catch ex As Exception
            lblError.Text = "Error At API Level"
            lblError.CssClass = "errorlabels"
        End Try
    End Sub


End Class