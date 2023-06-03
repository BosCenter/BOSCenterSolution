Imports System.Net
Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Data
Imports Newtonsoft.Json
Imports System.Security.Cryptography
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Net.Http

Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims


Public Class GenerateUCCController
    Inherits ApiController

    'API URL  https://www.boscenter.in/api/GenerateUCC
    'Mehod - POST
    'Merchant Code : TZ1233235YD874

    Dim GV As New GlobalVariable("ADMIN")
    Dim Generate_UCC_URL As String = "https://api.paysprint.in/api/v1/service/axisbank-utm/axisutm/generateurl"
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


    Public Function ReadbyRestClient(Urls As String, Parameter As String) As String
        Dim str As String = ""
        Try
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



            Dim client As New RestSharp.RestClient(Urls)
            Dim request As New RestSharp.RestRequest(RestSharp.Method.POST)
            request.AddHeader("Accept", "application/json")
            request.AddHeader("Token", dd)
            request.AddHeader("Content-Type", "application/json")

            request.AddParameter("application/json", Parameter, RestSharp.ParameterType.RequestBody)
            Dim response As RestSharp.IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

            Return str
        End Try
        Return str
    End Function
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

    Public Function GetValue() As String
        Return "value"
    End Function
    Public Function get_AutoNumber(ByVal fieldname As String) As Integer 'done
        Dim valueadd As Integer = 0
        Try
            Dim dsAutoNumber As New DataSet
            Dim qry As String = ""
            qry = " Declare @result nvarchar(max) "
            qry = qry + " if (select count(*) from AutoNumber)>0"
            qry = qry + " begin"
            qry = qry + " update AutoNumber set " + fieldname + "=ISNULL(" + fieldname + ",0)+1 "
            qry = qry + " set @result=(select " + fieldname + " from AutoNumber); "
            qry = qry + " end"
            qry = qry + " else"
            qry = qry + " begin"
            qry = qry + " insert into AutoNumber(" + fieldname + ") values('1')"
            qry = qry + " set @result=(select 1 as " + fieldname + ");"
            qry = qry + " end"
            qry = qry + " select  @result as " & fieldname

            dsAutoNumber = OpenDsWithSelectQuery(qry)

            If Not dsAutoNumber Is Nothing Then
                If dsAutoNumber.Tables.Count > 0 Then
                    If dsAutoNumber.Tables(0).Rows.Count > 0 Then
                        valueadd = dsAutoNumber.Tables(0).Rows(0).Item("" & fieldname & "")
                    Else
                        Return valueadd
                    End If
                Else
                    Return valueadd
                End If
            Else
                Return valueadd
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return valueadd
        End Try
        Return valueadd
    End Function

    Public Function get_AutoNumber_Admin(ByVal fieldname As String) As Integer 'done
        Dim valueadd As Integer = 0
        Try
            Dim dsAutoNumber As New DataSet
            Dim qry As String = ""
            qry = " Declare @result nvarchar(max) "
            qry = qry + " if (select count(*) from [BosCenter_DB].[dbo].AutoNumber)>0"
            qry = qry + " begin"
            qry = qry + " update [BosCenter_DB].[dbo].AutoNumber set " + fieldname + "=ISNULL(" + fieldname + ",0)+1 "
            qry = qry + " set @result=(select " + fieldname + " from [BosCenter_DB].[dbo].AutoNumber); "
            qry = qry + " end"
            qry = qry + " else"
            qry = qry + " begin"
            qry = qry + " insert into [BosCenter_DB].[dbo].AutoNumber(" + fieldname + ") values('1')"
            qry = qry + " set @result=(select 1 as " + fieldname + ");"
            qry = qry + " end"
            qry = qry + " select  @result as " & fieldname

            dsAutoNumber = OpenDsWithSelectQuery(qry)

            If Not dsAutoNumber Is Nothing Then
                If dsAutoNumber.Tables.Count > 0 Then
                    If dsAutoNumber.Tables(0).Rows.Count > 0 Then
                        valueadd = dsAutoNumber.Tables(0).Rows(0).Item("" & fieldname & "")
                    Else
                        Return valueadd
                    End If
                Else
                    Return valueadd
                End If
            Else
                Return valueadd
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return valueadd
        End Try
        Return valueadd
    End Function

    Public con As SqlConnection
    Public Function executeDMLQuery(ByVal Query As String) As Integer   'done
        Dim RowAffected As Integer = 0

        Try
            con = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())
            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If
            Dim cmd As SqlCommand
            cmd = New SqlCommand(Query, con)
            cmd.CommandType = CommandType.Text
            RowAffected = cmd.ExecuteNonQuery
            con.Close()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
        Return RowAffected
    End Function
    Public Function RecCount(ByVal Table_Name As String) As Integer  'done
        Dim affectedRows As Integer = 0
        Try

            Dim ds11 As New DataSet
            ds11 = OpenDsWithSelectQuery("select count(*) as 'TotalRow' from " & Table_Name)
            If Not ds11 Is Nothing Then
                If ds11.Tables.Count > 0 Then
                    If ds11.Tables(0).Rows.Count > 0 Then
                        affectedRows = CInt(ds11.Tables(0).Rows(0).Item("TotalRow"))
                    Else
                        affectedRows = 0
                    End If
                Else
                    affectedRows = 0
                End If
            Else
                affectedRows = 0
            End If

            Return affectedRows
            Exit Function
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            affectedRows = -1
            Return affectedRows
            Exit Function
        End Try
        Return affectedRows
    End Function
    Public Function OpenDsWithSelectQuery(ByVal Query As String) As DataSet  'done
        Try
            con = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())
            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If
            ds = New DataSet
            Dim da As SqlDataAdapter = New SqlDataAdapter(Query, con)
            da.SelectCommand.CommandTimeout = 300000
            da.Fill(ds)
            con.Close()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
        Return ds
    End Function

    Public Function AddInVar(ByVal retrivefield As String, ByVal tablename As String) As String 'done
        Try
            Dim str, variablename As String
            con = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())
            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If
            Dim dsVar As New DataSet
            str = "select " & retrivefield & " from " & tablename
            dsVar = OpenDsWithSelectQuery(str)
            If dsVar.Tables(0).Rows.Count = 0 Then
                variablename = ""
                con.Close()
                Return variablename
                Exit Function
            Else
                If IsDBNull(dsVar.Tables(0).Rows(0).Item(0)) = True Then
                    variablename = ""
                Else
                    variablename = dsVar.Tables(0).Rows(0).Item(0)
                End If
                con.Close()
                Return variablename
                Exit Function
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
        Dim s As String = ""
        Return s
    End Function


    Public Function parseString(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return StrConv(Replace(Replace(Replace(Replace(str, "'", "''"), "&amp;", "&"), "&nbsp;", ""), "&#39;", "''"), VbStrConv.None).Trim().Replace(";", ",")
        End If
    End Function

    Public Function PostData(<FromBody()> ByVal data As dataCUST) As String

        Dim returnJson As New Dictionary(Of String, String) From {
                  {"status", ""},
                  {"response_code", ""},
                  {"type", ""},
                  {"data", ""},
                  {"message", ""}
              }
        Try
            Dim merchantcode As String = data.merchantcode.ToString.Trim
            Dim type As String = data.type.ToString.Trim

            If merchantcode.Trim = "" Then
                returnJson("response_code") = "6"
                returnJson("message") = "Validation error"
                Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
                Exit Function
            End If
            If type.Trim = "" Then
                returnJson("response_code") = "6"
                returnJson("message") = "Validation error"
                Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
                Exit Function
            End If

            If Not IsNumeric(type.Trim) Then
                returnJson("response_code") = "6"
                returnJson("message") = "Validation error"
                Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
                Exit Function
            End If

            If Not (type.Trim = "1" Or type.Trim = "2") Then
                returnJson("response_code") = "6"
                returnJson("message") = "Validation error"
                Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
                Exit Function
            End If

            If Not merchantcode = "TZ1233235YD874" Then
                returnJson("response_code") = "7"
                returnJson("message") = "Authentication failed"
                Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
                Exit Function
            End If


            Dim ApiResult As String = ""
            Dim StrParameters As String = ""
            Dim API_URLS As String = ""
            Dim setParameter_API_Obj As New Generate_UCC_Parameters()
            setParameter_API_Obj.type = type
            setParameter_API_Obj.merchantcode = merchantcode
            StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
            API_URLS = Generate_UCC_URL
            ApiResult = ReadbyRestClient(API_URLS, StrParameters)

            Dim json_ As String = ApiResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim status_res As String = ser_.SelectToken("status").ToString.Trim
            Dim response_code_res As String = ser_.SelectToken("response_code").ToString.Trim
            Dim message As String = ser_.SelectToken("message").ToString.Trim
            Dim type_res As String = ""
            Dim data_res As String = ""

            If response_code_res = "1" Or response_code_res = "3" Then
                returnJson("status") = ser_.SelectToken("status").ToString.Trim
                returnJson("response_code") = response_code_res
                returnJson("type") = ser_.SelectToken("type").ToString.Trim
                returnJson("data") = ser_.SelectToken("data").ToString.Trim
                returnJson("message") = ser_.SelectToken("message").ToString.Trim
            Else
                returnJson("status") = ser_.SelectToken("status").ToString.Trim
                returnJson("response_code") = response_code_res
                returnJson("type") = ""
                returnJson("data") = ""
                returnJson("message") = ser_.SelectToken("message").ToString.Trim
            End If

            Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)

            Exit Function


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            returnJson("status") = "False"
            returnJson("response_code") = "6"
            returnJson("type") = ""
            returnJson("data") = ""
            returnJson("message") = ex.Message.ToString
            Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
            Exit Function
        End Try


    End Function



    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub

    Public Function returnWalletBalCalculation(loginId As String, DatabaseName As String) As Decimal
        Dim BAlAMount As Decimal = 0
        Try

            Dim FromAmount As String = AddInVar("Sum(isnull(TransferAmt,0))", "" & DatabaseName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & loginId & "'")
            Dim ToAmount As String = AddInVar("Sum(isnull(TransferAmt,0))", "" & DatabaseName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & loginId & "'")
            Dim HoldAmt As String = AddInVar("HoldAmt", "" & DatabaseName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & loginId & "'")

            If FromAmount.Trim = "" Then
                FromAmount = "0"
            End If
            If ToAmount.Trim = "" Then
                ToAmount = "0"
            End If
            If HoldAmt = "" Then
                HoldAmt = "0"
            End If

            BAlAMount = CDec(ToAmount) - CDec(FromAmount) - CDec(HoldAmt)


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
        Return BAlAMount
    End Function

    Public Class data1
        Public FirstName As String
        Public LastName As String
        Public CustomerID As Guid
    End Class

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

    Public Class CustomersRest
        Public FirstName As String
        Public LastName As String
        Public CustomerID As Guid
    End Class

End Class
