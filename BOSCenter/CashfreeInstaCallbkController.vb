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

Public Class CashfreeInstaCallbkController
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

    Public Function GetValue(ByVal Data As String) As ResponseData
        Try
            Dim str As String = "insert into boscenter_db.dbo.InstaCollectCallBack(API_Response,RecordDatetime) values('" & Data & "',getdate())"
            executeDMLQuery(str)
            Return getCallbck()
        Catch ex As Exception

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
            Return valueadd
        End Try
        Return valueadd
    End Function

    Dim GV As New GlobalVariable("Admin")
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
            '"BosCenter_DB", "S", "103.155.85.146", "I9XTw4cw", "sa"
            '"Server=103.216.146.185;DataBase=BosCenter_DB;user id=sa;password=F8zrde830n1".ToString())
            '"Server=DESKTOP-CIBE9DM;DataBase=BosCenter_DB;user id=sa;password=eklavya".ToString())
            '"Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123"

            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString())
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
            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString())
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
            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString())
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


    Public Function parseString(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return StrConv(Replace(Replace(Replace(Replace(str, "'", "''"), "&amp;", "&"), "&nbsp;", ""), "&#39;", "''"), VbStrConv.None).Trim().Replace(";", ",")
        End If
    End Function

    Public Function PostData(<FromBody()> ByVal data As JObject) As ResponseData
        Try
            Dim v_API_Response, v_event, amount, vAccountId, vAccountNumber, email, phone, remitterAccount,
            remitterIfsc, remarks, remitterName, paymentTime, signature As String
            Dim referenceId As String
            Dim utr, creditRefNo As String
            Dim v_AdminID, v_AgentID, v_AgentType, v_Transfer_Trans_ID, v_Ref_TransID, root As String
            Dim LogString As String = ""

            v_API_Response = data.ToString

            root = HttpContext.Current.Server.MapPath("~/CashFreelog.txt")
            System.IO.File.WriteAllText(root, v_API_Response)


            v_AdminID = ""
            v_AgentID = ""
            v_AgentType = ""
            v_Transfer_Trans_ID = get_AutoNumber_Admin("TransId")
            v_Ref_TransID = ""
            Dim json1 As JObject = JObject.Parse(v_API_Response)

            Dim aaEvent As JToken = json1.SelectToken("event")
            v_event = aaEvent.ToString
            amount = json1.SelectToken("amount")
            vAccountId = json1.SelectToken("vAccountId")
            Dim rwStr As String = vAccountId
            Dim rwData() As String = rwStr.ToUpper.Split("Z")
            v_AdminID = rwData(0)
            v_AgentID = rwData(1)
            If rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "M" Then
                v_AgentType = "Master Distributor"
                v_AgentID = "MD-" & rwData(1).ToString.Substring(2, rwData(1).Length - 2).Trim.ToUpper
            ElseIf rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "D" Then
                v_AgentType = "Distributor"
                v_AgentID = "DIS-" & rwData(1).ToString.Substring(3, rwData(1).Length - 3).Trim.ToUpper
            ElseIf rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "R" Then
                v_AgentType = "Retailer"
                v_AgentID = "RTE-" & rwData(1).ToString.Substring(3, rwData(1).Length - 3).Trim.ToUpper
            ElseIf rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "B" Then
                v_AgentType = "Customer"
                v_AgentID = "BOS-" & rwData(1).ToString.Substring(3, rwData(1).Length - 3).Trim.ToUpper
            End If

            vAccountNumber = json1.SelectToken("vAccountNumber")
            email = json1.SelectToken("email")
            phone = json1.SelectToken("phone")
            referenceId = CInt(json1.SelectToken("referenceId"))
            utr = json1.SelectToken("utr")
            creditRefNo = json1.SelectToken("creditRefNo")
            remitterAccount = json1.SelectToken("remitterAccount")
            remitterIfsc = json1.SelectToken("remitterIfsc")
            remarks = json1.SelectToken("remarks")
            remitterName = json1.SelectToken("remitterName")
            paymentTime = json1.SelectToken("paymentTime")
            signature = json1.SelectToken("signature")


            Dim str As String = "insert into boscenter_db.dbo.InstaCollectCallBack(AdminID,AgentID,AgentType,Transfer_Trans_ID,API_Response,RecordDatetime,API_event ,data_id  ,data_created_at  ,data_remitter_full_name  ,data_remitter_account_number  ,data_remitter_account_ifsc  ,data_remitter_phone_number  ,data_unique_transaction_reference  ,data_payment_mode  ,data_amount  ,data_service_charge  ,data_gst_amount  ,data_service_charge_with_gst  ,data_narration  ,data_status  ,data_transaction_date  ,virtual_account_id  ,virtual_label  ,virtual_account_number  ,virtual_ifsc_number  ,API_Authorization) values( '" & v_AdminID & "','" & v_AgentID & "','" & v_AgentType & "','" & referenceId & "','" & v_API_Response & "',getdate() , '" & v_event & "','" & vAccountId & "','" & paymentTime & "','" & remitterName & "','" & remitterAccount & "','" & remitterIfsc & "','" & phone & "','" & referenceId & "','online','" & amount & "','0','0','0','" & remarks & "','','" & paymentTime & "','" & vAccountId & "','','" & vAccountNumber & "','" & remitterIfsc & "','" & signature & "') ;"
            executeDMLQuery(str)

            Dim VTransferFromMsg As String = ""
            Dim VTransferToMsg As String = ""
            Dim AmouontType As String = "Deposit"
            Dim VTransferTo As String = v_AgentID
            Dim VTransferFrom As String = "Admin"
            Dim VAmtTransTransID As String = v_Transfer_Trans_ID
            Dim VTransferAmt As String = amount

            Dim dbName As String = ""

            If v_AdminID.Trim.ToUpper = "CMP1045" Then
                dbName = "boscenter_db"
            Else
                dbName = v_AdminID.Trim.ToUpper
            End If

            Dim v_Ret_ID As String = AddInVar("RegistrationId", " " & dbName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where easeBuzz_Virtual_Acc_No='" & vAccountId & "'")

            Dim Trns_Id As String = get_AutoNumber_Admin("TransId")
            Dim QryStr As String = " insert into " & dbName.Trim & ".dbo.BOS_TransferAmountToAgents (Ref_TransID,TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & v_Ref_TransID.Trim & "','" & GetIPAddress() & "','" & Trns_Id & "','" & Trns_Id & "','Your Wallet is Credited by BOS CENTER PVT LTD - Account Deposit','Your Wallet is Debited by Admin  (" & v_AdminID & ")  - Account Deposit','Deposit','" & "Add By Wallet - Account Deposit" & "',getdate(),'Super Admin','Admin','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"

            VTransferFromMsg = "Your Wallet is Debited by " & v_AgentType & " (" & VTransferTo & ")  - Account Deposit"
            VTransferToMsg = "Your Wallet is Credited by Admin - Account Deposit"
            QryStr = QryStr & " insert into " & dbName.Trim & ".dbo.BOS_TransferAmountToAgents (Ref_TransID,TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & v_Ref_TransID.Trim & "','" & GetIPAddress() & "','" & VAmtTransTransID & "','" & VAmtTransTransID & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & AmouontType & "','" & "Add By Wallet - Account Deposit" & "',getdate(),'" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"
            executeDMLQuery(QryStr)
            'If Not v_Ret_ID.Trim = "" Then
            If v_event.Trim.ToUpper = "AMOUNT_COLLECTED" Then
                If VTransferTo.Trim.ToUpper = v_Ret_ID.Trim.ToUpper Then
                    str = str & QryStr
                Else
                    str = str
                End If
            Else
                str = str
                End If
            'End If
            Dim rowcount As Integer = 0
            rowcount = RecCount(" boscenter_db.dbo.InstaCollectCallBack where data_unique_transaction_reference='" & referenceId & "'")
            If Not rowcount > 0 Then
                executeDMLQuery(str)
            End If

            Return getCallbck()

            Exit Function
        Catch ex As Exception
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
