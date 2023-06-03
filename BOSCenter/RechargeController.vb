Imports System.Net
Imports System.Web.Http
Imports System.Data
Imports System.Data.SqlClient


Public Class RechargeController
    Inherits ApiController

    ' GET api/<controller>
    Public Function GetValues() As IEnumerable(Of String)

        'Dim request As HttpWebRequest
        'Dim response As HttpWebResponse = Nothing
        'Dim url As String = "http://bulk.mdsms.in:7412/api/mt/SendSMS?user=Oliveshine&password=Oliveshine&senderid=HERBAL&channel=Trans&DCS=0&flashsms=0&number=" + ContactNos & "&text=" + txtMessage & "&route=1048"
        'request = DirectCast(WebRequest.Create(url), HttpWebRequest)
        'response = DirectCast(request.GetResponse(), HttpWebResponse)

        Return New String() {"value1", "value2"}
    End Function


    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As Integer) As String
        Return "value"
    End Function

    'https://www.boscenter.in/api/recharge/?OrderId=1000&status=fail&msg=sdfsfs

    Dim GV As New GlobalVariable("ADMIN")

    Public Function GetValue(ByVal OrderId As String, ByVal status As String, ByVal msg As String) As String
        Dim v_OrderId, v_status, v_msg As String
        v_OrderId = parseString(OrderId)
        v_status = parseString(status)
        v_msg = parseString(msg)
        Dim result As Integer = 0
        Try
            Dim cn As SqlConnection
            Dim cmd As SqlCommand
            Dim qry As String = ""
            Dim orderIDFound As Boolean = True
            'con = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())

            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())

            Dim ds11 As DataSet = OpenDsWithSelectQuery("select * from Recharge_API_DB_Info where API_TransId='" & v_OrderId & "'")
            Dim DBName As String = ""
            Dim CompanyCode As String = ""

            If Not ds11 Is Nothing Then
                If ds11.Tables.Count > 0 Then
                    If ds11.Tables(0).Rows.Count > 0 Then
                        DBName = ds11.Tables(0).Rows(0).Item("DBName")
                        CompanyCode = ds11.Tables(0).Rows(0).Item("CompanyCode")
                        'API_status='Processing'

                        Dim API_status As String = ""
                        Dim RefundedStatus As Boolean = False

                        If Not IsDBNull(ds11.Tables(0).Rows(0).Item("API_status")) Then
                            If ds11.Tables(0).Rows(0).Item("API_status").ToString.Trim.ToUpper = "Processing".ToUpper Then

                                If v_status.Trim.ToUpper = "2".ToUpper Then
                                    'QryStr = QryStr & " " & " insert into BosCenter_DB.dbo.Recharge_API_DB_Info (RecordDatetime,API_TransId,Recharge_TransId,API_status,API_resText,CompanyCode,DBName) values(getdate(),'" & VTransId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(Vstatus) & "','" & GV.parseString(VresText) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "','" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "') ; "
                                    If RecCount("  " & DBName & ".dbo.BOS_Recharge_API where API_TransId='" & v_OrderId & "' ") > 0 Then
                                        Dim Refund_TransID As String = get_AutoNumber("TransId")
                                        qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_Status,Refund_TransID,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "','Yes','" & Refund_TransID & "','" & v_OrderId & "','" & v_status & "','" & v_msg & "',getdate());"
                                        qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
                                        qry = qry & " update " & DBName & ".dbo.BOS_Recharge_API set API_status='" & v_status & "',API_resText='" & v_msg & "',Refund_Status='Yes',Refund_TransID='" & Refund_TransID & "' where API_TransId='" & v_OrderId & "' ;"
                                        qry = qry & " insert into " & DBName & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) select Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount, '" & Refund_TransID & "' as Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg=Replace(Replace(Replace(TransferFromMsg,'RECH','RECH-Refund'),'BILLPAY','BILLPAY-Refund'),'Landline','Landline-Refund'),TransferFromMsg=Replace(Replace(Replace(TransferToMsg,'RECH','RECH-Refund'),'BILLPAY','BILLPAY-Refund'),'Landline','Landline-Refund'),Amount_Type=Amount_Type+'-Refund',Remark=Remark+'-Refund',TransactionDate,TransferFrom=TransferTo,TransferTo=TransferFrom,TransferAmt,getdate() as 'RecordDateTime','Admin' as 'UpdatedBy',getdate()  as 'UpdatedOn' from " & DBName & ".dbo.BOS_TransferAmountToAgents where API_TransId='" & v_OrderId & "' ; "
                                        RefundedStatus = True

                                    Else
                                        orderIDFound = False
                                        qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & " - Initial - 2 -',getdate());"
                                    End If
                                ElseIf v_status.Trim.ToUpper = "1".ToUpper Then
                                    qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & " - Initial - 1 -',getdate());"
                                    'con = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())

                                    con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())

                                    If RecCount("  " & DBName & ".dbo.BOS_Recharge_API where API_TransId='" & v_OrderId & "' ") > 0 Then
                                        qry = qry & " update " & DBName & ".dbo.BOS_Recharge_API set API_status='" & v_status & "',API_resText='" & v_msg & "',Refund_Status='No',Refund_TransID=NULL where API_TransId='" & v_OrderId & "' ;"
                                        qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
                                    Else
                                        orderIDFound = False
                                    End If
                                Else
                                    qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & " - Initial - else -',getdate());"
                                    qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
                                End If

                                'cn = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())
                                con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
                                cmd = New SqlCommand(qry, cn)
                                cmd.CommandType = CommandType.Text
                                cn.Open()
                                result = cmd.ExecuteNonQuery
                                cn.Close()

                                If orderIDFound = False Then
                                    Return "Order ID Not Found"
                                Else
                                    If result > 0 Then
                                        If RefundedStatus = True Then
                                            Return "Refunded Successfully"
                                        Else
                                            Return "Status Updated Successfully"
                                        End If

                                    Else
                                        Return "An Error occurred"
                                    End If
                                End If

                            Else
                                ' Status Already updated
                                Return "Order Status Already Updated."
                            End If
                        Else
                            Return "API Status Is Null."
                            'Found null
                        End If








                    Else
                        Return "Order ID Not Found"
                    End If
                Else
                    Return "Order ID Not Found"
                End If
            Else
                Return "Order ID Not Found"
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            v_msg = v_msg & "- Exception - " & ex.Message
            Dim qry As String = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('CMP1045',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & "',getdate()); "
            qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
            Dim cn As SqlConnection = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
            'con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
            Dim cmd As New SqlCommand(qry, cn)
            cmd.CommandType = CommandType.Text
            cn.Open()
            result = cmd.ExecuteNonQuery
            cn.Close()
            Return "An exception occurred"
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
    Public con As SqlConnection

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
            conOpen()
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
    Public Sub conOpen() 'done
        Try

            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Function parseString(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return StrConv(Replace(Replace(Replace(Replace(str, "'", "''"), "&amp;", "&"), "&nbsp;", ""), "&#39;", "''"), VbStrConv.None).Trim().Replace(";", ",")
        End If
    End Function

    ' POST api/<controller>
    Public Sub PostValue(<FromBody()> ByVal value As String)

    End Sub


    ' PUT api/<controller>/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub


    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub

End Class
