Imports System.Net
Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Data
Imports Newtonsoft.Json
Imports System.Security.Cryptography
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Net.Http



Public Class PaySprintDMTCallbkController
    Inherits ApiController

    Dim GV As New GlobalVariable("ADMIN")
    Public Function GetValues() As IEnumerable(Of String)
        Return New String() {"value1", "value2"}
    End Function

    Public Function GetIPAddress() As String
        Try
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(sIPAddress) Then
                Return context.Request.ServerVariables("REMOTE_ADDR")
            Else
                Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
                Return ipArray(0)
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return Nothing
        End Try
    End Function

    Public Function GetValue(ByVal Data As String) As String

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
    'Public Function conOpen() As String
    '    Try
    '        con = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())
    '        If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
    '            con.Open()
    '        End If
    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

    '    End Try
    '    Return ""
    'End Function
    Public Function parseString(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return StrConv(Replace(Replace(Replace(Replace(str, "'", "''"), "&amp;", "&"), "&nbsp;", ""), "&#39;", "''"), VbStrConv.None).Trim().Replace(";", ",")
        End If
    End Function

    Public Function PostData(<FromBody()> ByVal data As dataCUST) As String

        Dim returnJson As New Dictionary(Of String, String) From {
                  {"ERROR", ""},
                  {"MESSAGE", ""},
                  {"BALANCE", ""},
                  {"USERVAR1", ""},
                  {"USERVAR2", ""},
                  {"USERVAR3", ""}
              }
        Try

            Dim Rec_json_string As String = data.data
            Dim json_ As String = Rec_json_string
            Dim ser_ As JObject = JObject.Parse(json_)

            Dim username As String = ser_.SelectToken("username").ToString.Trim
            Dim password As String = ser_.SelectToken("password").ToString.Trim()
            Dim amount As String = ser_.SelectToken("amount").ToString.Trim


            If username.Trim = "" Then
                returnJson("ERROR") = "1"
                returnJson("MESSAGE") = "Please Enter LoginId."
                'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
                Exit Function
            End If

            If Not (Len(username.Trim) = 10 And IsNumeric(username.Trim)) Then
                returnJson("ERROR") = "1"
                returnJson("MESSAGE") = "Please Enter Valid LoginId."
                'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                Exit Function
            End If

            If password.Trim = "" Then
                returnJson("ERROR") = "2"
                returnJson("MESSAGE") = "Please Enter Password."
                'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                Exit Function
            End If

            If amount.Trim = "" Then
                returnJson("ERROR") = "3"
                returnJson("MESSAGE") = "Please Enter Bill Amount."
                'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                Exit Function
            End If

            If Not IsNumeric(amount.Trim) Then
                returnJson("ERROR") = "4"
                returnJson("MESSAGE") = "Bill Amount Must be Numeric."
                'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                Exit Function
            End If

            Dim localDS As New DataSet

            Dim DBName As String = "CMP1140"
            localDS = OpenDsWithSelectQuery("select * from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   MobileNo='" & username & "' and AgentPassword='" & password & "'  and AgentType in ('Retailer')  and ActiveStatus='Active' ") ''Customer',

            If localDS.Tables.Count > 0 Then
                If localDS.Tables(0).Rows.Count > 0 Then
                    If Not returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName) >= CDec(amount) Then

                        returnJson("ERROR") = "6"
                        returnJson("MESSAGE") = "Insufficient Wallet Balance"
                        'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                        Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                        Exit Function
                    Else
                        '//// Success

                        returnJson("ERROR") = "0"
                        returnJson("MESSAGE") = "Success"
                        returnJson("USERVAR1") = localDS.Tables(0).Rows(0).Item("RegistrationId")
                        returnJson("BALANCE") = returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName)
                        'Return Request.CreateResponse(HttpStatusCode.Created, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                        Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                    End If

                Else
                    returnJson("ERROR") = "7"
                    returnJson("MESSAGE") = "Invalid LoginID Or Password"
                    'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                    Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                    Exit Function
                End If
            Else
                returnJson("ERROR") = "7"
                returnJson("MESSAGE") = "Invalid LoginID Or Password"
                'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
                Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
                Exit Function
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            returnJson("ERROR") = "1"
            returnJson("MESSAGE") = ex.Message
            'Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            Exit Function
        End Try


    End Function

    'Public Function PostData(<FromBody()> ByVal data As dataCUST) As HttpResponseMessage

    '    Dim returnJson As New Dictionary(Of String, String) From {
    '              {"ERROR", ""},
    '              {"MESSAGE", ""},
    '              {"BALANCE", ""},
    '              {"USERVAR1", ""},
    '              {"USERVAR2", ""},
    '              {"USERVAR3", ""}
    '          }
    '    Try

    '        '<FromBody()>
    '        'Request.CreateResponse(HttpStatusCode.OK, "message")
    '        'Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Message")
    '        'Dim message = Request.CreateResponse(HttpStatusCode.Created, "message")
    '        'message.Headers.Location = New Uri(Request.RequestUri.ToString)
    '        'Return message

    '        'gyyh7YPOOYj4VeLUybV+Txs0Rv0ZDji7vJUPd6BjfmepS8VDgPBkQ6OYYjbjjqMW
    '        Dim Rec_json_string As String = Decrypt_New(data.data)
    '        Dim json_ As String = Rec_json_string
    '        Dim ser_ As JObject = JObject.Parse(json_)

    '        Dim username As String = ser_.SelectToken("username").ToString.Trim
    '        Dim password As String = ser_.SelectToken("password").ToString.Trim
    '        Dim amount As String = ser_.SelectToken("amount").ToString.Trim


    '        If username.Trim = "" Then
    '            returnJson("ERROR") = "1"
    '            returnJson("MESSAGE") = "Please Enter LoginId."
    '            Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If Not (Len(username.Trim) = 10 And IsNumeric(username.Trim)) Then
    '            returnJson("ERROR") = "1"
    '            returnJson("MESSAGE") = "Please Enter Valid LoginId."
    '            Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If password.Trim = "" Then
    '            returnJson("ERROR") = "2"
    '            returnJson("MESSAGE") = "Please Enter Password."
    '            Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If amount.Trim = "" Then
    '            returnJson("ERROR") = "3"
    '            returnJson("MESSAGE") = "Please Enter Bill Amount."
    '            Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If Not IsNumeric(amount.Trim) Then
    '            returnJson("ERROR") = "4"
    '            returnJson("MESSAGE") = "Bill Amount Must be Numeric."
    '            Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        Dim localDS As New DataSet

    '        Dim DBName As String = "CMP1140"
    '        localDS = OpenDsWithSelectQuery("select * from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   MobileNo='" & username & "' and AgentPassword='" & password & "'  and AgentType in ('Retailer')  and ActiveStatus='Active' ") ''Customer',

    '        If localDS.Tables.Count > 0 Then
    '            If localDS.Tables(0).Rows.Count > 0 Then
    '                If Not returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName) >= CDec(amount) Then

    '                    returnJson("ERROR") = "6"
    '                    returnJson("MESSAGE") = "Insufficient Wallet Balance"
    '                    Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                    'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                    Exit Function
    '                Else
    '                    '//// Success

    '                    returnJson("ERROR") = "0"
    '                    returnJson("MESSAGE") = "Success"
    '                    returnJson("USERVAR1") = localDS.Tables(0).Rows(0).Item("RegistrationId")
    '                    returnJson("BALANCE") = returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName)
    '                    Return Request.CreateResponse(HttpStatusCode.Created, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                    'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                End If

    '            Else
    '                returnJson("ERROR") = "7"
    '                returnJson("MESSAGE") = "Invalid LoginID Or Password"
    '                Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                Exit Function
    '            End If
    '        Else
    '            returnJson("ERROR") = "7"
    '            returnJson("MESSAGE") = "Invalid LoginID Or Password"
    '            Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
    '        returnJson("ERROR") = "1"
    '        returnJson("MESSAGE") = ex.Message
    '        Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))

    '        'returnJson("ERROR") = "1"
    '        'returnJson("MESSAGE") = ex.Message
    '        'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '        Exit Function
    '    End Try



    '    'Dim strQry As String = ""
    '    'Dim obj As String = ""
    '    'Try
    '    '    obj = data.data
    '    '    Return Request.CreateResponse(HttpStatusCode.OK, obj.ToString)

    '    '        Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
    '    '    Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message)

    '    'End Try


    '    'Dim c As New CustomersRest
    '    'c.FirstName = "Bill"
    '    'c.LastName = "Gates"
    '    'c.CustomerID = Guid.Empty
    '    'Dim RestURL As String = "https://example.com/api/customers/"
    '    'Dim client As New Http.HttpClient
    '    'Dim JsonData As String = JsonConvert.SerializeObject(c)
    '    'Dim RestContent As New Http.StringContent(JsonData, Encoding.UTF8, "application/json")

    '    'Dim RestResponse As Http.HttpResponseMessage = HttpClient.PostAsync(RestURL, RestContent)
    '    'ResultMessage.Text = RestResponse.StatusCode.ToString


    'End Function

    'Public Function PostValue(<FromBody()> ByVal Data As String) As HttpResponseMessage
    '    Dim returnJson As New Dictionary(Of String, String) From {
    '              {"ERROR", ""},
    '              {"MESSAGE", ""},
    '              {"BALANCE", ""},
    '              {"USERVAR1", ""},
    '              {"USERVAR2", ""},
    '              {"USERVAR3", ""}
    '          }
    '    Try
    '        Dim strQry As String = ""
    '        Dim obj As String = ""
    '        Try
    '            obj = Newtonsoft.Json.JsonConvert.DeserializeObject(Data)
    '            strQry = "insert into BosCenter_DB.dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('IRCTC' ,'1' ,getdate() ,'" & Data.ToString & "' ,'" & obj.ToString & "' ,'Retailer' ,'Retailer')"
    '            executeDMLQuery(strQry)
    '                Catch ex As Exception 
    '.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
    '            strQry = "insert into BosCenter_DB.dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('IRCTC' ,'1' ,getdate() ,'" & Data.ToString & "' ,'" & Data.ToString & "' ,'Retailer' ,'Retailer')"
    '            executeDMLQuery(strQry)
    '        End Try


    '        '<FromBody()>

    '        'Request.CreateResponse(HttpStatusCode.OK, "message")
    '        'Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Message")
    '        'Dim message = Request.CreateResponse(HttpStatusCode.Created, "message")
    '        'message.Headers.Location = New Uri(Request.RequestUri.ToString)
    '        'Return message


    '        'gyyh7YPOOYj4VeLUybV+Txs0Rv0ZDji7vJUPd6BjfmepS8VDgPBkQ6OYYjbjjqMW
    '        Dim Rec_json_string As String = Decrypt_New(obj)
    '        Dim json_ As String = Rec_json_string
    '        Dim ser_ As JObject = JObject.Parse(json_)

    '        Dim username As String = ser_.SelectToken("username").ToString.Trim
    '        Dim password As String = ser_.SelectToken("password").ToString.Trim
    '        Dim amount As String = ser_.SelectToken("amount").ToString.Trim


    '        If username.Trim = "" Then
    '            returnJson("ERROR") = "1"
    '            returnJson("MESSAGE") = "Please Enter LoginId."
    '            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If Not (Len(username.Trim) = 10 And IsNumeric(username.Trim)) Then
    '            returnJson("ERROR") = "1"
    '            returnJson("MESSAGE") = "Please Enter Valid LoginId."
    '            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If password.Trim = "" Then
    '            returnJson("ERROR") = "2"
    '            returnJson("MESSAGE") = "Please Enter Password."
    '            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If amount.Trim = "" Then
    '            returnJson("ERROR") = "3"
    '            returnJson("MESSAGE") = "Please Enter Bill Amount."
    '            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        If Not IsNumeric(amount.Trim) Then
    '            returnJson("ERROR") = "4"
    '            returnJson("MESSAGE") = "Bill Amount Must be Numeric."
    '            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '        Dim localDS As New DataSet

    '        Dim DBName As String = "CMP1140"


    '        localDS = OpenDsWithSelectQuery("select * from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   MobileNo='" & username & "' and AgentPassword='" & password & "'  and AgentType in ('Retailer')  and ActiveStatus='Active' ") ''Customer',

    '        If localDS.Tables.Count > 0 Then
    '            If localDS.Tables(0).Rows.Count > 0 Then
    '                If Not returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName) >= CDec(amount) Then

    '                    returnJson("ERROR") = "6"
    '                    returnJson("MESSAGE") = "Insufficient Wallet Balance"
    '                    Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                    'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                    Exit Function
    '                Else
    '                    '//// Success

    '                    returnJson("ERROR") = "0"
    '                    returnJson("MESSAGE") = "Success"
    '                    returnJson("USERVAR1") = localDS.Tables(0).Rows(0).Item("RegistrationId")
    '                    returnJson("BALANCE") = returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName)
    '                    Return Request.CreateResponse(HttpStatusCode.Created, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                    'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                End If

    '            Else
    '                returnJson("ERROR") = "7"
    '                returnJson("MESSAGE") = "Invalid LoginID Or Password"
    '                Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                Exit Function
    '            End If
    '        Else
    '            returnJson("ERROR") = "7"
    '            returnJson("MESSAGE") = "Invalid LoginID Or Password"
    '            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            Exit Function
    '        End If

    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
    '        returnJson("ERROR") = "1"
    '        returnJson("MESSAGE") = ex.Message
    '        Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))

    '        'returnJson("ERROR") = "1"
    '        'returnJson("MESSAGE") = ex.Message
    '        'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '        Exit Function
    '    End Try
    'End Function

    ' POST api/<controller>
    'Public Function PostValue(<FromBody()> ByVal Data As String) As HttpResponseMessage
    '    Dim returnJson As New Dictionary(Of String, String) From {
    '              {"ERROR", ""},
    '              {"MESSAGE", ""},
    '              {"BALANCE", ""},
    '              {"USERVAR1", ""},
    '              {"USERVAR2", ""},
    '              {"USERVAR3", ""}
    '          }
    '    Dim dd As New HttpResponseMessage(HttpStatusCode.OK)


    '    'dd.Content = New StringContent(Data)
    '    Try

    '        'gyyh7YPOOYj4VeLUybV+Txs0Rv0ZDji7vJUPd6BjfmepS8VDgPBkQ6OYYjbjjqMW


    '        Dim Rec_json_string As String = Decrypt_New(Data)
    '        Dim json_ As String = Rec_json_string
    '        Dim ser_ As JObject = JObject.Parse(json_)

    '        Dim username As String = ser_.SelectToken("username").ToString.Trim
    '        Dim password As String = ser_.SelectToken("password").ToString.Trim
    '        Dim amount As String = ser_.SelectToken("amount").ToString.Trim


    '        If username.Trim = "" Then
    '            returnJson("ERROR") = "1"
    '            returnJson("MESSAGE") = "Please Enter LoginId."
    '            dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            Return dd
    '            Exit Function
    '        End If

    '        If Not (Len(username.Trim) = 10 And IsNumeric(username.Trim)) Then
    '            returnJson("ERROR") = "1"
    '            returnJson("MESSAGE") = "Please Enter Valid LoginId."
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            Return dd

    '            Exit Function
    '        End If

    '        If password.Trim = "" Then
    '            returnJson("ERROR") = "2"
    '            returnJson("MESSAGE") = "Please Enter Password."
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            Return dd
    '            Exit Function
    '        End If

    '        If amount.Trim = "" Then
    '            returnJson("ERROR") = "3"
    '            returnJson("MESSAGE") = "Please Enter Bill Amount."
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            Return dd

    '            Exit Function
    '        End If

    '        If Not IsNumeric(amount.Trim) Then
    '            returnJson("ERROR") = "4"
    '            returnJson("MESSAGE") = "Bill Amount Must be Numeric."
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            Return dd

    '            Exit Function
    '        End If

    '        Dim localDS As New DataSet

    '        Dim DBName As String = "CMP1140"


    '        localDS = OpenDsWithSelectQuery("select * from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   MobileNo='" & username & "' and AgentPassword='" & password & "'  and AgentType in ('Retailer')  and ActiveStatus='Active' ") ''Customer',

    '        If localDS.Tables.Count > 0 Then
    '            If localDS.Tables(0).Rows.Count > 0 Then
    '                If Not returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName) >= CDec(amount) Then

    '                    returnJson("ERROR") = "6"
    '                    returnJson("MESSAGE") = "Insufficient Wallet Balance"
    '                    dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                    Return dd

    '                    'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                    Exit Function
    '                Else
    '                    '//// Success

    '                    returnJson("ERROR") = "0"
    '                    returnJson("MESSAGE") = "Success"
    '                    returnJson("USERVAR1") = localDS.Tables(0).Rows(0).Item("RegistrationId")
    '                    returnJson("BALANCE") = returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName)
    '                    'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                    dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                    Return dd
    '                End If

    '            Else
    '                returnJson("ERROR") = "7"
    '                returnJson("MESSAGE") = "Invalid LoginID Or Password"
    '                'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '                dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '                Return dd
    '                Exit Function
    '            End If
    '        Else
    '            returnJson("ERROR") = "7"
    '            returnJson("MESSAGE") = "Invalid LoginID Or Password"
    '            'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '            dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '            Return dd

    '            Exit Function
    '        End If

    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

    '        returnJson("ERROR") = "1"
    '        returnJson("MESSAGE") = ex.Message
    '        'Return Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
    '        dd.Content = New StringContent(Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
    '        Return dd
    '        Exit Function
    '    End Try
    'End Function

    ' PUT api/<controller>/5
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

    Public Class CustomersRest
        Public FirstName As String
        Public LastName As String
        Public CustomerID As Guid
    End Class

End Class
