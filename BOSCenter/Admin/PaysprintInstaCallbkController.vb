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
Imports Octokit
Imports System.Threading.Tasks

Public Class PaysprintInstaCallbkController
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
            'Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123

            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
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
            'Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123
            'Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123

            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
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
            'Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123
            'Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123

            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
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
            con.Close()
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



    'Private Function TestUrl(ByVal str As String) As String
    '    If str = "Payment" Then
    '        Dim s As HttpWebRequest
    '        Dim enc As UTF8Encoding
    '        Dim postdata As String
    '        Dim postdatabytes As Byte()
    '        s = HttpWebRequest.Create("https://babajiclubs.com/api/verify_bos.php/")
    '        enc = New System.Text.UTF8Encoding()
    '        postdata = v_API_Response
    '        postdatabytes = enc.GetBytes(postdata)
    '        s.Method = "POST"
    '        s.ContentType = "application/x-www-form-urlencoded"
    '        s.ContentLength = postdatabytes.Length

    '        Using stream = s.GetRequestStream()
    '            stream.Write(postdatabytes, 0, postdatabytes.Length)
    '        End Using
    '        Dim result = s.GetResponse()
    '        str = v_API_Response
    '        Return str
    '    End If
    'End Function

    Dim v_API_Response
    Public Function PostData(<FromBody()> ByVal data As JObject) As ResponseData
        Try

            Dim v_event, txnstatus, Message, ackno, amount,
            refid, ttype, api_type, upi_txn_id, txn_completion_date, customer_virtual_address,
            customer_mobile_number, customer_account_name, vAccountId, param_enc As String
            Dim status As Boolean
            Dim response_code As Integer

            Dim v_AdminID, v_AgentID, v_AgentType, v_Transfer_Trans_ID, v_Ref_TransID As String
            Dim LogString As String = ""
            v_API_Response = data.ToString
            Dim Str As String
            v_AdminID = ""
            v_AgentID = ""
            v_AgentType = ""
            v_Transfer_Trans_ID = get_AutoNumber_Admin("TransId")
            v_Ref_TransID = ""
            Dim json1 As JObject = JObject.Parse(v_API_Response)

            Dim aaEvent As JToken = json1.SelectToken("event")
            v_event = aaEvent.ToString
            status = json1.SelectToken("param")("status")
            response_code = json1.SelectToken("param")("response_code")
            txnstatus = json1.SelectToken("param")("txnstatus")
            Message = json1.SelectToken("param")("message")
            ackno = json1.SelectToken("param")("ackno")
            amount = json1.SelectToken("param")("amount")
            refid = json1.SelectToken("param")("refid")
            ttype = json1.SelectToken("param")("ttype")
            api_type = json1.SelectToken("param")("api_type")
            upi_txn_id = json1.SelectToken("param")("upi_txn_id")
            txn_completion_date = json1.SelectToken("param")("txn_completion_date")
            customer_virtual_address = json1.SelectToken("param")("customer_virtual_address")
            customer_mobile_number = json1.SelectToken("param")("customer_mobile_number")
            customer_account_name = json1.SelectToken("param")("customer_account_name")
            param_enc = json1.SelectToken("param_enc")

            v_Ref_TransID = json1.SelectToken("param")("ackno")

            Dim v_MarchantCode As String

            If refid IsNot Nothing Then
                v_MarchantCode = AddInVar("MerchantCode", "BosCenter_DB.dbo.DynamicQRCode where refid='" & refid & "'")
                If v_MarchantCode = String.Empty Then
                    v_MarchantCode = AddInVar("MerchantCode", "BosCenter_DB.dbo.StaticQRCode where refid='" & refid & "'")
                End If
                Dim VPAID As String = AddInVar("VPA", "BosCenter_DB.dbo.createvpa where MarchentCode='" & v_MarchantCode & "'")
                VPAID = VPAID.Remove(VPAID.LastIndexOf("@"))
                Dim rwData() As String = VPAID.Trim.ToUpper.Split("Z")
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
            End If
            Dim dbName As String = ""
            If v_AdminID.Trim.ToUpper = "CMP1045" Then
                dbName = "boscenter_db"
            Else
                dbName = v_AdminID.Trim.ToUpper
            End If
            Str = "insert into " & dbName & ".dbo.InstaCollectCallBack(AdminID,AgentType,Transfer_Trans_ID,API_Response,
            RecordDatetime,API_event ,data_id  ,data_created_at  ,data_remitter_full_name  ,data_remitter_account_number  ,data_remitter_account_ifsc  ,
            data_remitter_phone_number  ,data_unique_transaction_reference  ,data_payment_mode  ,dataAmount,data_service_charge  ,data_gst_amount  ,
            data_service_charge_with_gst  ,data_narration  ,data_status  ,data_transaction_date  ,virtual_account_id  ,virtual_label  ,
            virtual_account_number  ,virtual_ifsc_number  ,API_Authorization) 
            values( '" & v_AdminID & "','" & v_AgentType & "','" & v_Transfer_Trans_ID & "','" & v_API_Response & "',getdate() , '" & v_event & "','" & upi_txn_id & "','" & txn_completion_date & "','" & customer_account_name & "','" & customer_virtual_address & "','','" & customer_mobile_number & "','" & refid & "','" & ttype & "','" & amount & "','0','0','0','" & Message & "','','" & txn_completion_date & "','" & customer_virtual_address & "','" & v_AgentType & "','" & customer_virtual_address & "','','" & param_enc & "') ;"
            executeDMLQuery(Str)

            If aaEvent = "UPIQR_SUCCESS" Then

                Dim VTransferFromMsg As String = ""
                Dim VTransferToMsg As String = ""
                Dim AmouontType As String = "Deposit"
                Dim VTransferTo As String = v_AgentID
                Dim VTransferFrom As String = "Admin"
                Dim VAmtTransTransID As String = v_Transfer_Trans_ID
                Dim VTransferAmt As String = amount


                Dim v_Ret_ID As String = AddInVar("RegistrationId", dbName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where easeBuzz_Virtual_Acc_No='" & vAccountId & "'")

                    Dim Trns_Id As String = get_AutoNumber_Admin("TransId")
                    Dim QryStr As String = " insert into " & dbName & ".dbo.BOS_TransferAmountToAgents (Ref_TransID,TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & v_Ref_TransID.Trim & "','" & GetIPAddress() & "','" & Trns_Id & "','" & Trns_Id & "','Your Wallet is Credited by BOS CENTER PVT LTD - Account Deposit','Your Wallet is Debited by Admin  (" & v_AdminID & ")  - Account Deposit','Deposit','" & "Add By Wallet - Account Deposit" & "',getdate(),'Super Admin','Admin','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"

                    VTransferFromMsg = "Your Wallet is Debited by " & v_AgentType & " (" & VTransferTo & ")  - Account Deposit"
                    VTransferToMsg = "Your Wallet is Credited by Admin - Account Deposit"
                QryStr = QryStr & " insert into " & dbName & ".dbo.BOS_TransferAmountToAgents(Ref_TransID, TransIpAddress, API_TransId, Amt_Transfer_TransID, TransferToMsg, TransferFromMsg, Amount_Type, Remark, TransactionDate, TransferFrom, TransferTo, TransferAmt, RecordDateTime, UpdatedBy, UpdatedOn) values( '" & v_Ref_TransID.Trim & "','" & GetIPAddress() & "','" & VAmtTransTransID & "','" & refid & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & AmouontType & "','" & "Add By Wallet - Account Deposit" & "',getdate(),'" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"
                executeDMLQuery(QryStr)

                    ''////// Service Charge For Retailer To Admin - Start
                    Dim NetAmount1 As Decimal = 0
                Dim Service1() As String = AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", dbName & ".dbo.BOS_ProductServiceMaster where Title='PayUMoney' ").Split(":")
                'After Discussion with Sir
                If VTransferAmt > 500 Then
                    'If Service1.Length > 1 Then
                    '    If Service1(1).Trim = "Percentage" Then
                    NetAmount1 = (CDec(VTransferAmt) * CDec(1)) / 100
                    'ElseIf Service1(1).Trim = "Amount" Then
                    '    NetAmount1 = CDec(Service1(0))
                    'ElseIf Service1(1).Trim = "Not Applicable" Then
                    '    NetAmount1 = CDec(Service1(0))
                    'End If
                    'End If
                Else
                    'After Discussion with Sir netamount1=5
                    'Again changes after discussion with Sir betamount1=2.5
                    NetAmount1 = 2.5
                End If

                If NetAmount1 > 0 Then
                        Dim RTE As String = v_AgentID
                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount1 & " Rs. Due to Add Money - " & RTE & " ."
                        Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount1 & " Rs. Due to Add Money - " & RTE & " ."
                    QryStr = "insert into " & dbName & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & GV.FL_AdminLogin.getAutoNumber("TransId") & "','" & VTransferAmt & "', '" & GV.parseString(refid) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.ToString("MM/dd/yyyy") & "','" & RTE & "','ADMIN','" & NetAmount1 & "',getdate(),'Admin',getdate() ) ;"
                    executeDMLQuery(QryStr)
                    End If
                ''///////  Service Charge For Retailer To Admin - End



                ''////// Service Charge For Admin To SuperAdmin - Start
                Dim NetAmount As Decimal = 0
                'After Discussion with Sir
                If VTransferAmt > 500 Then
                    Dim Service() As String = AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & dbName & ".dbo.BOS_ProductServiceVsAdmin_SA where AdminID='" & v_AdminID & "' and Title='PayUMoney' ").Split(":")
                    'If Service.Length > 1 Then
                    '    If Service(1).Trim = "Percentage" Then
                    NetAmount = (CDec(VTransferAmt) * CDec(1)) / 100
                    'ElseIf Service(1).Trim = "Amount" Then
                    '            NetAmount = CDec(Service(0))
                    '        ElseIf Service(1).Trim = "Not Applicable" Then
                    '            NetAmount = CDec(Service(0))
                    '        End If
                    '    End If
                Else
                    'After Discussion with Sir
                    NetAmount = 2.5
                End If
                If NetAmount > 0 Then
                        Dim RTE As String = v_AgentID
                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to Add Money - " & RTE & " ."
                        Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to Add Money - " & RTE & " ."
                    QryStr = "insert into " & dbName & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & GV.FL_AdminLogin.getAutoNumber("TransId") & "','" & VTransferAmt & "', '" & GV.parseString(refid) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.ToString("MM/dd/yyyy") & "','ADMIN','SUPER ADMIN','" & NetAmount & "',getdate(),'Admin',getdate() ) ;"
                    executeDMLQuery(QryStr)
                    End If
                    ''///////  Service Charge For Admin To SuperAdmin - End

                    Dim rowcount As Integer = 0
                    rowcount = RecCount(dbName & "dbo.InstaCollectCallBack where data_unique_transaction_reference='" & refid & "'")
                    If Not rowcount > 0 Then
                        'executeDMLQuery(str)
                    End If
                End If

            '    Dim PstResponse As String
            'PstResponse = TestUrl("Payment")
            Return getCallbck()
            Exit Function
        Catch ex As Exception
        End Try
        'TestUrl("Payment")
        Return getCallbck()
    End Function

    Public Function getCallbck() As ResponseData
        Dim response = New ResponseData With {
        .status = "200",
        .message = "Transaction completed successfully"
    }
        Return response
    End Function
    Public Class ResponseData
        'Public Property JsonRespone As String
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
