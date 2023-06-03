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


Public Class EasebuzzInstaCallbkController
    Inherits ApiController


    Dim Gv As New GlobalVariable("ADMIN")
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

    Public Function GetValue(ByVal Data As String) As ResponseData
        Try
            Dim str As String = "insert into boscenter_db.dbo.InstaCollectCallBack(API_Response,RecordDatetime) values('" & Data & "',getdate())"
            executeDMLQuery(str)
            Return getCallbck()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
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
            '"BosCenter_DB", "S", "103.155.85.146", "I9XTw4cw", "sa"
            '"Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=I9XTw4cw".ToString())

            con = New SqlConnection("Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=I9XTw4cw".ToString())
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
            con = New SqlConnection("Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=I9XTw4cw".ToString())
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
            con = New SqlConnection("Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=I9XTw4cw".ToString())
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
    '        con = New SqlConnection("Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=I9XTw4cw".ToString())
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

    Public Function PostData(<FromBody()> ByVal data As JObject) As ResponseData
        Try
            Dim v_API_Response, v_API_event, v_data_id, v_data_created_at, v_data_remitter_full_name, v_data_remitter_account_number, v_data_remitter_account_ifsc, v_data_remitter_phone_number, v_data_unique_transaction_reference, v_data_payment_mode, v_data_amount, v_data_service_charge, v_data_gst_amount, v_data_service_charge_with_gst, v_data_narration, v_data_status, v_data_transaction_date, v_virtual_account_id, v_virtual_label, v_virtual_account_number, v_virtual_ifsc_number, v_API_Authorization As String
            Dim v_AdminID, v_AgentID, v_AgentType, v_Transfer_Trans_ID, v_Ref_TransID As String

            v_API_Response = data.ToString

            'executeDMLQuery("insert into boscenter_db.dbo.InstaCollectCallBack(API_Response,RecordDatetime) values( '" & v_API_Response & "',getdate() ) ;")


            v_AdminID = ""
            v_AgentID = ""
            v_AgentType = ""
            v_Transfer_Trans_ID = get_AutoNumber_Admin("TransId")
            v_Ref_TransID = ""


            v_API_event = ""
            v_data_id = ""
            v_data_created_at = ""
            v_data_remitter_full_name = ""
            v_data_remitter_account_number = ""
            v_data_remitter_account_ifsc = ""
            v_data_remitter_phone_number = ""
            v_data_unique_transaction_reference = ""
            v_data_payment_mode = ""
            v_data_amount = ""
            v_data_service_charge = ""
            v_data_gst_amount = ""
            v_data_service_charge_with_gst = ""
            v_data_narration = ""
            v_data_status = ""
            v_data_transaction_date = ""
            v_virtual_account_id = ""
            v_virtual_label = ""
            v_virtual_account_number = ""
            v_virtual_ifsc_number = ""
            v_API_Authorization = ""


            Dim json1 As JObject = JObject.Parse(v_API_Response)


            Dim aaEvent As JToken = json1.SelectToken("event")
            v_API_event = aaEvent.ToString

            Dim aaRESPONSE As JToken = json1.SelectToken("data")
            Dim data12 As List(Of JToken) = aaRESPONSE.Children().ToList


            For Each item As JProperty In data12
                If item.Name.ToString.Trim.ToUpper = "virtual_account".ToString.Trim.ToUpper Then
                    Dim data121 As List(Of JToken) = item.Children().ToList
                    For Each item23 As JObject In data121
                        For Each p123 In item23
                            If p123.Key.ToString.Trim.ToUpper = "id".ToString.Trim.ToUpper Then
                                v_virtual_account_id = p123.Value.ToString
                            ElseIf p123.Key.ToString.Trim.ToUpper = "Label".ToString.Trim.ToUpper Then
                                v_virtual_label = p123.Value.ToString
                            ElseIf p123.Key.ToString.Trim.ToUpper = "virtual_account_number".ToString.Trim.ToUpper Then
                                v_virtual_account_number = p123.Value.ToString
                                Dim rwStr As String = v_virtual_account_number.Substring(6, v_virtual_account_number.Length - 6)
                                Dim rwData() As String = rwStr.Split("Z")
                                v_AdminID = rwData(0)

                                If rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "M" Then
                                    v_AgentType = "Master Distributor"
                                    v_AgentID = "MD-" & rwData(1).ToString.Substring(2, rwData(1).Length - 2).Trim.ToUpper
                                ElseIf rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "D" Then
                                    v_AgentType = "Distributor"
                                    v_AgentID = "DIS-" & rwData(1).ToString.Substring(3, rwData(1).Length - 3).Trim.ToUpper
                                ElseIf rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "R" Then
                                    v_AgentType = "Retailer"
                                    v_AgentID = "RTE-" & rwData(1).ToString.Substring(3, rwData(1).Length - 3).Trim.ToUpper
                                Else
                                    v_AgentType = "Customer"
                                    v_AgentID = "BOS-" & rwData(1).ToString.Substring(0, rwData(1).Length).Trim.ToUpper
                                End If

                            ElseIf p123.Key.ToString.Trim.ToUpper = "virtual_ifsc_number".ToString.Trim.ToUpper Then
                                v_virtual_ifsc_number = p123.Value.ToString
                            End If
                        Next

                    Next
                ElseIf item.Name.ToString.Trim.ToUpper = "id".ToString.Trim.ToUpper Then
                    v_data_id = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "created_at".ToString.Trim.ToUpper Then
                    v_data_created_at = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "remitter_full_name".ToString.Trim.ToUpper Then
                    v_data_remitter_full_name = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "remitter_account_number".ToString.Trim.ToUpper Then
                    v_data_remitter_account_number = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "remitter_account_ifsc".ToString.Trim.ToUpper Then
                    v_data_remitter_account_ifsc = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "remitter_phone_number".ToString.Trim.ToUpper Then
                    v_data_remitter_phone_number = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "unique_transaction_reference".ToString.Trim.ToUpper Then
                    v_data_unique_transaction_reference = item.Value.ToString
                    v_Ref_TransID = v_data_unique_transaction_reference
                ElseIf item.Name.ToString.Trim.ToUpper = "payment_mode".ToString.Trim.ToUpper Then
                    v_data_payment_mode = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "amount".ToString.Trim.ToUpper Then
                    v_data_amount = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "service_charge".ToString.Trim.ToUpper Then
                    v_data_service_charge = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "gst_amount".ToString.Trim.ToUpper Then
                    v_data_gst_amount = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "service_charge_with_gst".ToString.Trim.ToUpper Then
                    v_data_service_charge_with_gst = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "narration".ToString.Trim.ToUpper Then
                    v_data_narration = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
                    v_data_status = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "transaction_date".ToString.Trim.ToUpper Then
                    v_data_transaction_date = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "Authorization".ToString.Trim.ToUpper Then
                    v_API_Authorization = item.Value.ToString
                End If
            Next
            v_API_event = v_API_event
            v_data_id = v_data_id
            v_data_created_at = v_data_created_at
            v_data_remitter_full_name = v_data_remitter_full_name
            v_data_remitter_account_number = v_data_remitter_account_number
            v_data_remitter_account_ifsc = v_data_remitter_account_ifsc
            v_data_remitter_phone_number = v_data_remitter_phone_number
            v_data_unique_transaction_reference = v_data_unique_transaction_reference
            v_data_payment_mode = v_data_payment_mode
            v_data_amount = v_data_amount
            v_data_service_charge = v_data_service_charge
            v_data_gst_amount = v_data_gst_amount
            v_data_service_charge_with_gst = v_data_service_charge_with_gst
            v_data_narration = v_data_narration
            v_data_status = v_data_status
            v_data_transaction_date = v_data_transaction_date
            v_virtual_account_id = v_virtual_account_id
            v_virtual_label = v_virtual_label
            v_virtual_account_number = v_virtual_account_number
            v_virtual_ifsc_number = v_virtual_ifsc_number
            v_API_Authorization = v_API_Authorization
            v_AdminID = v_AdminID
            v_AgentID = v_AgentID
            v_AgentType = v_AgentType

            Dim str As String = "insert into boscenter_db.dbo.InstaCollectCallBack(AdminID,AgentID,AgentType,Transfer_Trans_ID,API_Response,RecordDatetime,API_event  ,data_id  ,data_created_at  ,data_remitter_full_name  ,data_remitter_account_number  ,data_remitter_account_ifsc  ,data_remitter_phone_number  ,data_unique_transaction_reference  ,data_payment_mode  ,data_amount  ,data_service_charge  ,data_gst_amount  ,data_service_charge_with_gst  ,data_narration  ,data_status  ,data_transaction_date  ,virtual_account_id  ,virtual_label  ,virtual_account_number  ,virtual_ifsc_number  ,API_Authorization) values( '" & v_AdminID & "','" & v_AgentID & "','" & v_AgentType & "','" & v_Transfer_Trans_ID & "','" & v_API_Response & "',getdate() , '" & v_API_event & "','" & v_data_id & "','" & v_data_created_at & "','" & v_data_remitter_full_name & "','" & v_data_remitter_account_number & "','" & v_data_remitter_account_ifsc & "','" & v_data_remitter_phone_number & "','" & v_data_unique_transaction_reference & "','" & v_data_payment_mode & "','" & v_data_amount & "','" & v_data_service_charge & "','" & v_data_gst_amount & "','" & v_data_service_charge_with_gst & "','" & v_data_narration & "','" & v_data_status & "','" & v_data_transaction_date & "','" & v_virtual_account_id & "','" & v_virtual_label & "','" & v_virtual_account_number & "','" & v_virtual_ifsc_number & "','" & v_API_Authorization & "') ;"


            Dim VTransferFromMsg As String = ""
            Dim VTransferToMsg As String = ""
            Dim AmouontType As String = "Deposit"
            Dim VTransferTo As String = v_AgentID
            Dim VTransferFrom As String = "Admin"
            Dim VAmtTransTransID As String = v_Transfer_Trans_ID
            Dim VTransferAmt As String = v_data_amount

            Dim dbName As String = ""

            If v_AdminID.Trim.ToUpper = "CMP1045" Then
                dbName = "boscenter_db"
            Else
                dbName = v_AdminID.Trim.ToUpper
            End If


            Dim v_Ret_ID As String = AddInVar("RegistrationId", " " & dbName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where easeBuzz_Virtual_Acc_No='" & v_virtual_account_number & "'")

            Dim Trns_Id As String = get_AutoNumber_Admin("TransId")
            Dim QryStr As String = " insert into " & dbName.Trim & ".dbo.BOS_TransferAmountToAgents (Ref_TransID,TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & v_Ref_TransID.Trim & "','" & GetIPAddress() & "','" & Trns_Id & "','" & Trns_Id & "','Your Wallet is Credited by BOS CENTER PVT LTD - Account Deposit','Your Wallet is Debited by Admin  (" & v_AdminID & ")  - Account Deposit','Deposit','" & "Add By Wallet - Account Deposit" & "',getdate(),'Super Admin','Admin','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"

            VTransferFromMsg = "Your Wallet is Debited by " & v_AgentType & " (" & VTransferTo & ")  - Account Deposit"
            VTransferToMsg = "Your Wallet is Credited by Admin - Account Deposit"
            QryStr = QryStr & " insert into " & dbName.Trim & ".dbo.BOS_TransferAmountToAgents (Ref_TransID,TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & v_Ref_TransID.Trim & "','" & GetIPAddress() & "','" & VAmtTransTransID & "','" & VAmtTransTransID & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & AmouontType & "','" & "Add By Wallet - Account Deposit" & "',getdate(),'" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"



            If Not v_Ret_ID.Trim = "" Then
                If v_API_event.Trim.ToUpper = "TRANSACTION_CREDIT" Then
                    If VTransferTo.Trim.ToUpper = v_Ret_ID.Trim.ToUpper Then
                        str = str & QryStr
                    Else
                        str = str
                    End If
                Else
                    str = str
                End If
            End If
            If Not RecCount(" boscenter_db.dbo.InstaCollectCallBack where data_unique_transaction_reference='" & v_data_unique_transaction_reference & "'") > 0 Then
                executeDMLQuery(str)
            End If

            Return getCallbck()

            Exit Function

            'Dim ser_ As JObject = JObject.Parse(json_)
            'Dim username As String = ser_.SelectToken("status").ToString.Trim
            'Dim password As String = ser_.SelectToken("password").ToString.Trim
            'Dim amount As String = ser_.SelectToken("amount").ToString.Trim
            ''Return JsonConvert.SerializeObject(returnJson)
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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

    Public Class eventCUST
        Dim Vevent As String
        Public Property [event]() As String
            Get
                Return Vevent
            End Get
            Set(ByVal value As String)
                Vevent = value
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
