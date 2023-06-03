Imports System.Net
Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Data
Imports Newtonsoft.Json
Imports System.Security.Cryptography
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Net.Http
Imports System.Web.Script.Serialization


Public Class PaySprintAEPSCallbkController
    Inherits ApiController
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
    '    Catch ex As Exception

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

    Public Function PostData(<FromBody()> ByVal data As dataAEPS) As ResponseData

        Dim returnJson As New Dictionary(Of String, String) From {
                  {"status", ""},
                  {"message", ""}
              }
        Try
            returnJson("status") = "200"
            returnJson("message") = "Transaction completed successfully"
            'JavaScriptSerializer().Serialize()
            Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonString As String = javaScriptSerializer.Serialize(returnJson)

            'Return JavaScriptSerializer().Serialize()
            Return getCallbck()

            'Return JsonConvert.SerializeObject(returnJson)
            Exit Function

            'Dim Rec_json_string As String = data.merchant_id
            'Dim json_ As String = Rec_json_string
            'Dim ser_ As JObject = JObject.Parse(json_)

            'Dim username As String = ser_.SelectToken("username").ToString.Trim
            'Dim password As String = ser_.SelectToken("password").ToString.Trim
            'Dim amount As String = ser_.SelectToken("amount").ToString.Trim


            'If username.Trim = "" Then
            '    returnJson("ERROR") = "1"
            '    returnJson("MESSAGE") = "Please Enter LoginId."
            '    'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '    Return JsonConvert.SerializeObject(returnJson, Formatting.Indented)
            '    Exit Function
            'End If

            'If Not (Len(username.Trim) = 10 And IsNumeric(username.Trim)) Then
            '    returnJson("ERROR") = "1"
            '    returnJson("MESSAGE") = "Please Enter Valid LoginId."
            '    'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '    Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '    Exit Function
            'End If

            'If password.Trim = "" Then
            '    returnJson("ERROR") = "2"
            '    returnJson("MESSAGE") = "Please Enter Password."
            '    'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '    Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '    Exit Function
            'End If

            'If amount.Trim = "" Then
            '    returnJson("ERROR") = "3"
            '    returnJson("MESSAGE") = "Please Enter Bill Amount."
            '    'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '    Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '    Exit Function
            'End If

            'If Not IsNumeric(amount.Trim) Then
            '    returnJson("ERROR") = "4"
            '    returnJson("MESSAGE") = "Bill Amount Must be Numeric."
            '    'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '    Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '    Exit Function
            'End If

            'Dim localDS As New DataSet

            'Dim DBName As String = "CMP1140"
            'localDS = OpenDsWithSelectQuery("select * from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   MobileNo='" & username & "' and AgentPassword='" & password & "'  and AgentType in ('Retailer')  and ActiveStatus='Active' ") ''Customer',

            'If localDS.Tables.Count > 0 Then
            '    If localDS.Tables(0).Rows.Count > 0 Then
            '        If Not returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName) >= CDec(amount) Then

            '            returnJson("ERROR") = "6"
            '            returnJson("MESSAGE") = "Insufficient Wallet Balance"
            '            'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '            Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '            Exit Function
            '        Else
            '            '//// Success

            '            returnJson("ERROR") = "0"
            '            returnJson("MESSAGE") = "Success"
            '            returnJson("USERVAR1") = localDS.Tables(0).Rows(0).Item("RegistrationId")
            '            returnJson("BALANCE") = returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName)
            '            'Return Request.CreateResponse(HttpStatusCode.Created, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '            Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '        End If
            '    Else
            '        returnJson("ERROR") = "7"
            '        returnJson("MESSAGE") = "Invalid LoginID Or Password"
            '        'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '        Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '        Exit Function
            '    End If
            'Else
            '    returnJson("ERROR") = "7"
            '    returnJson("MESSAGE") = "Invalid LoginID Or Password"
            '    'Return Request.CreateErrorResponse(HttpStatusCode.OK, Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented)))
            '    Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            '    Exit Function
            'End If

        Catch ex As Exception
            returnJson("status") = "400"
            returnJson("message") = ex.Message
            'Return (JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            Return getCallbck()
            Exit Function
        End Try


    End Function

    Public Function getCallbck() As ResponseData
        Dim response = New ResponseData With {
        .status = "200",
        .message = "Transaction completed successfully"
    }
        Return response
    End Function
    Public Class ResponseData
        Public Property status As String
        Public Property message As String
    End Class

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


    Public Class dataAEPS
        Dim V_merchant_id, V_partner_id, V_request_id, V_amount As String
        Public Property merchant_id() As String
            Get
                Return V_merchant_id
            End Get
            Set(ByVal value As String)
                V_merchant_id = value
            End Set
        End Property

        Public Property partner_id() As String
            Get
                Return V_partner_id
            End Get
            Set(ByVal value As String)
                V_partner_id = value
            End Set
        End Property

        Public Property request_id() As String
            Get
                Return V_request_id
            End Get
            Set(ByVal value As String)
                V_request_id = value
            End Set
        End Property

        Public Property amount() As String
            Get
                Return V_amount
            End Get
            Set(ByVal value As String)
                V_amount = value
            End Set
        End Property


    End Class

    Public Class CustomersRest
        Public FirstName As String
        Public LastName As String
        Public CustomerID As Guid
    End Class

End Class
