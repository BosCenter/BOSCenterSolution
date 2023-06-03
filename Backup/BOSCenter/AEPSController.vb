Imports System.Net
Imports System.Web.Http
Imports System.Data
Imports System.Data.SqlClient

Public Class AEPSController
    Inherits ApiController

    ' GET api/<controller>
    Public Function GetValues() As IEnumerable(Of String)
        Return New String() {"value1", "value2"}
    End Function

    ' GET api/<controller>/5
    Public Function GetValue(ByVal Txntype As String, ByVal Timestamp As String, ByVal BcId As String, ByVal TerminalId As String, ByVal TransactionId As String, ByVal Amount As String, ByVal TxnStatus As String, ByVal BankIIN As String, ByVal TxnMedium As String, ByVal EndCustMobile As String) As String
        '//// ----------------Variable Declaration  ----------------
        Dim VTxntype, VAPITimestamp, VBcId, VTerminalId, VTransactionId, VAmount, VTxnStatus, VBankIIN, VTxnMedium, VEndCustMobile As String
        'Txntype,Timestamp,BcId,TerminalId, TransactionId,Amount,TxnStatus, BankIIN,TxnMedium,EndCustMobile
        Dim result As Integer = 0

        '//// ----------------Variable Assignment  ----------------
        VTxntype = Txntype
        VAPITimestamp = Timestamp
        VBcId = BcId
        VTerminalId = TerminalId
        VTransactionId = TransactionId
        VAmount = Amount
        VTxnStatus = TxnStatus
        VBankIIN = BankIIN
        VTxnMedium = TxnMedium
        VEndCustMobile = EndCustMobile
        Try
            Dim cn As SqlConnection
            Dim cmd As SqlCommand
            Dim qry As String = "insert into BOS_AEPS_CheckStatus (RecordDateTime,Txntype,APITimestamp,BcId,TerminalId,TransactionId,Amount,TxnStatus,BankIIN,TxnMedium,EndCustMobile) values( getdate() ,'" & VTxntype & "','" & VAPITimestamp & "','" & VBcId & "','" & VTerminalId & "','" & VTransactionId & "','" & VAmount & "','" & VTxnStatus & "','" & VBankIIN & "','" & VTxnMedium & "','" & VEndCustMobile & "' )"
            cn = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())
            cmd = New SqlCommand(qry, cn)
            cmd.CommandType = CommandType.Text
            cn.Open()
            result = cmd.ExecuteNonQuery
            cn.Close()
            If result > 0 Then
                Return "Submitted to Log Section - CheckStatus "
            Else
                Return "Problem in Insertion - CheckStatus "
            End If
        Catch ex As Exception
            Return "Exception Message - " & ex.Message
        End Try

    End Function

    Public Function GetValue(ByVal TransactionId As String, ByVal VenderId As String, ByVal BcCode As String, ByVal Status As String, ByVal rrn As String) As String
        Dim VTransactionId, VVenderId, VBcCode, VAPIStatus, Vrrn As String
        'TransactionId ,VenderId ,BcCode ,APIStatus ,rrn
        Dim result As Integer = 0
        VVenderId = VenderId
        Vrrn = rrn
        VBcCode = BcCode
        VAPIStatus = Status
        VTransactionId = TransactionId
        Try
            Dim cn As SqlConnection
            Dim cmd As SqlCommand
            Dim qry As String = "insert into BOS_AEPS_UpdateStatus (RecordDateTime,TransactionId ,VenderId ,BcCode ,APIStatus ,rrn) values( getdate() ,'" & VTransactionId & "','" & VVenderId & "','" & VBcCode & "','" & VAPIStatus & "','" & Vrrn & "' )"
            cn = New SqlConnection("Server=103.205.66.210,2499;DataBase=BosCenter_DB;user id=sa;password=CapUY77RawrIZa7h".ToString())
            cmd = New SqlCommand(qry, cn)
            cmd.CommandType = CommandType.Text
            cn.Open()
            result = cmd.ExecuteNonQuery
            cn.Close()
            If result > 0 Then
                Return "Submitted to Log Section - UpdateStatus "
            Else
                Return "Problem in Insertion - UpdateStatus "
            End If

        Catch ex As Exception
            Return "Exception Message - " & ex.Message
        End Try
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
