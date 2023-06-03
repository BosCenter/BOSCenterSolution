Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.WebRequestMethods
Imports System.Web.Http
Imports System.Web.Mvc


Public Class AddFundController
    Inherits ApiController

    Dim GV As New GlobalVariable("ADMIN")
    Dim Request_TransID As String
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

    ' GET api/<controller>
    Public Function GetValues() As IEnumerable(Of String)
        Return New String() {"value1", "value2"}
    End Function

    Dim FinalResult As AddFund = New AddFund

    'Dim Request_Transaction_Id, Request_name, Request_email, Request_phone, Request_amount, Request_redirect_url, Request_CompanyCode, Request_Purpose, Request_AgentID, Request_TransID As String
    Public Function GetValue(ByVal Companycode As String, ByVal LoginID As String, ByVal UserPassword As String) As AddFund
        'Dim Chkval As New Dictionary(Of String, String) From {
        '    {"Status", ""},
        '    {"CompanyCode", ""},
        '    {"CompanyName", ""},
        '    {"LoginID", ""}
        '    }

        Dim FundResult As String = ""

        If Companycode.Trim = "" Then
            FundResult = "Please Enter Companycode"
            FinalResult.Status = FundResult
            Return FinalResult
            Exit Function
        End If

        If LoginID.Trim = "" Then
            FundResult = "Please Enter Login ID"
            FinalResult.Status = FundResult
            Return FinalResult
            Exit Function
        End If
        If UserPassword.Trim = "" Then
            FundResult = "Please Enter Password"
            FinalResult.Status = FundResult
            Return FinalResult
            Exit Function
        End If

        Dim DBName As String = AddInVar("DataBaseName", "BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & Companycode.Trim & "' and [Status]='Active'")
        If DBName.Trim = "" Then
            FundResult = "Please Enter Correct CompanyCode"
            FinalResult.Status = FundResult
            Return FinalResult
            Exit Function
        End If

        Request_TransID = ""


        Request_TransID = AddInVar("TransId", "[BosCenter_DB].[dbo].[AutoNumber]") & get_AutoNumber_Admin("Pay_Trans_ID")
        'Request_TransID = GV.FL_AdminLogin.getAutoNumber("TransId")
        Dim localDs As New DataSet

        If Len(LoginID.Trim) = 10 And IsNumeric(LoginID.Trim) Then
            localDs = OpenDsWithSelectQuery("Select * from " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where MobileNo='" & LoginID & "'and AgentType='Customer' and AgentPassword='" & UserPassword & "' and ActiveStatus='Active' ")
        ElseIf LoginID.Trim.Contains("-") Then
            localDs = OpenDsWithSelectQuery("select * from " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & LoginID & "' and AgentPassword='" & UserPassword & "'  and AgentType in ('Customer','Retailer')  and ActiveStatus='Active' ")
        Else
            FundResult = "Invalid LoginID"
            FinalResult.Status = FundResult
            Return FinalResult
            Exit Function
        End If



        If localDs.Tables.Count > 0 Then
            If localDs.Tables(0).Rows.Count > 0 Then
                FinalResult.Status = "True"
                FinalResult.LoginID = LoginID
                FinalResult.CompanyCode = Companycode
                FinalResult.AgentName = localDs.Tables(0).Rows(0).Item("FirstName") & " " & localDs.Tables(0).Rows(0).Item("LastName")
                FinalResult.AgentType = localDs.Tables(0).Rows(0).Item("AgentType")
                FinalResult.TranID = Companycode + "_" + localDs.Tables(0).Rows(0).Item("Registrationid") + "_" + Request_TransID

                FinalResult.Redirect = "https://vdapayments.com/Add_Money.aspx?TranID=" + FinalResult.TranID
            Else
                FundResult = "Invalid LoginID Or Password"
                FinalResult.Status = FundResult
                Return FinalResult
                Exit Function
            End If

            Dim GV As New GlobalVariable(DBName)

            Dim InsrtQry = executeDMLQuery("insert into ApiResponse(Status,CompanyCode,LoginID,AgentName,AgentType,Redirect,CreatedDate) values('" & FinalResult.Status & "','" & FinalResult.CompanyCode & "','" & FinalResult.LoginID & "','" & FinalResult.AgentName & "','" & FinalResult.AgentType & "','" & FinalResult.Redirect & "','" & System.DateTime.Now & "')")
        Else
            FundResult = "Invalid LoginID Or Password"
            FinalResult.Status = FundResult
            Return FinalResult
            Exit Function
        End If



        Return FinalResult
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
            conOpen()
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
            conOpen()
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
            conOpen()
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
    Public Sub conOpen() 'DESKTOP-86QM4Q8
        Try
            'con = New SqlConnection("Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123".ToString())
            con = New SqlConnection("Server=103.216.146.185;DataBase=BosCenter_DB;user id=sa;password=F8zrde830n1".ToString())
            'con = New SqlConnection("Server=DESKTOP-86QM4Q8;DataBase=BosCenter_DB;user id=sa;password=eklavya".ToString())
            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If
        Catch ex As Exception

        End Try
    End Sub



    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As Integer) As String
        Return "value"
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
