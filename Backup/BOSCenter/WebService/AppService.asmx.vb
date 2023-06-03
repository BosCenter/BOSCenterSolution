Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Net
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
'<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://earnalot.securelations.com/WebService/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class AppService
    Inherits System.Web.Services.WebService

    Dim GV As New GlobalVariable("Webservice")

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function ReturnVerifyAdvisor(ByVal CompanyCode As String, ByVal AdvisorId As String, ByVal Password As String) As String
        Dim Result As String = ""
        Dim SqlQry As String = ""
        Try

            Dim DatabaseName As String = "CMP10175"
            SqlQry = " CRM_ClientRegistration where  ClientID='" & AdvisorId & "'"
            If GV.FL_WebService.RecCount(SqlQry) > 0 Then
                SqlQry = " CRM_ClientRegistration where  ClientID='" & AdvisorId & "' and Client_Password='" & Password & "'"
                If GV.FL_WebService.RecCount(SqlQry) > 0 Then
                    SqlQry = " CRM_ClientRegistration where  ClientID='" & AdvisorId & "' and Client_Password='" & Password & "' and AccountStatus='Active'"
                    If GV.FL_WebService.RecCount(SqlQry) > 0 Then
                        SqlQry = " CRM_ClientRegistration where  ClientID='" & AdvisorId & "' and Client_Password='" & Password & "' and AccountStatus='Active' and ApprovedStatus='Approved'"
                        If GV.FL_WebService.RecCount(SqlQry) > 0 Then
                            'SqlQry = " NidhiSoftware_Admin_AdvisorVsDDA_LoginRights where  ClientID='" & AdvisorId & "'"
                            'If GV.FL_WebService.RecCount(SqlQry) > 0 Then
                            '    If GV.AdvisorVerify_LoginTimeOut(AdvisorId, CompanyCode, "Advisor") = True Then
                            '        Result = "Success"
                            '    Else
                            '        Result = "Sorry!! Incorrect Login Time"
                            '    End If

                            'Else
                            '    Result = "Sorry!! You Are Not Allowed To Login"
                            'End If
                            Result = "Success"
                        Else
                            Result = "Sorry!! Your Account is Not Approved"
                        End If

                    Else
                        Result = "Sorry!! Your Account is InActive"
                    End If

                Else
                    Result = "Incorrect Password"
                End If

            Else
                Result = "Incorrect Advisor ID"
            End If
            Return Result
        Catch ex As Exception
            Return ex.StackTrace
        End Try
        Return Result
    End Function

    <WebMethod()> _
    Public Function SaveCoordinates(ByVal AdvisorId As String, ByVal CompanyCode As String, ByVal Lang As String, ByVal Lat As String) As Boolean
        Dim result As Boolean = False
        Try
            'Dim GV As New GlobalVariable("Webservice")

            If GV.AdvisorVerify_TrackingTime(AdvisorId, CompanyCode, Lat, Lang) = True Then
                Dim message As String = "New Coordinates Lat : " & Lat & ", Lang : " & Lang
                Dim Duration As Integer = GV.FL_WebService.AddInVar("GPS_TrackDuration", "NidhiSoftware_Admin_AdvisorRegistration where AdvisorID='" & AdvisorId & "' and CompanyCode='" & CompanyCode & "'")

                Dim qry As String = "insert into NidhiSoftware_Admin_GPS_Cord_Data(MemberId,AdvisorId,DataDate,Lat,Lng,InsertionTime,Description,CompanyCode,UpdatedOn,UpdatedBy) values((select MemberId from NidhiSoftware_Admin_AdvisorRegistration  where CompanyCode='" & CompanyCode & "' and AdvisorID='" & AdvisorId & "'),'" & AdvisorId & "',getdate(),'" & Lat & "','" & Lang & "',getdate(),'" & message & "','" & CompanyCode & "',getdate(),'" & CompanyCode & "')"
                qry = qry & " ; " & " update NidhiSoftware_Admin_AdvisorRegistration set  GPS_NextScheduledTrack='" & DateAdd(DateInterval.Second, Duration, Now) & "' where AdvisorID='" & AdvisorId & "' and CompanyCode='" & CompanyCode & "' "

                result = GV.FL_WebService.DMLQueries(qry)


            End If

        Catch ex As Exception
            Return ex.StackTrace
        End Try
        Return result
    End Function

    <WebMethod()> _
    Public Function DMLQueries(ByVal SqlQry As String) As String
        Dim result As String = "0"
        Try
            result = GV.FL_WebService.DMLQueries(SqlQry)
        Catch ex As Exception
            Return ex.StackTrace
        End Try
        Return result
    End Function

    <WebMethod()> _
    Public Sub DMLQueriesNonReturn(ByVal SqlQry As String)
        Try
            GV.FL_WebService.DMLQueriesBulk(SqlQry)
        Catch ex As Exception
        End Try
    End Sub

    <WebMethod()> _
    Public Sub DMLQueriesNonReturn_FT(ByVal SqlQry As String, allowFormat As Boolean)
        Try
            If allowFormat = True Then
                SqlQry = GV.DecryptQry(SqlQry)
            End If
            GV.FL_WebService.DMLQueriesBulk(SqlQry)
        Catch ex As Exception

        End Try
    End Sub

    <WebMethod()> _
    Public Sub DMLQueriesNonReturn_DT(ByVal SqlQry As String, DataType As String)
        Try
            If DataType.Trim = "Device Details" Then
                If SqlQry.Contains("Insert into") Then

                ElseIf SqlQry.Contains("Update") Then

                Else
                    Exit Sub
                End If
            End If
            GV.FL_WebService.DMLQueriesBulk(SqlQry)
        Catch ex As Exception

        End Try
    End Sub

    <WebMethod()> _
    Public Function AddInVar(ByVal RetriveField As String, ByVal TableName As String) As String
        Dim val As String = ""
        Try
            val = GV.FL_WebService.AddInVar(RetriveField, TableName)
        Catch ex As Exception
            Return val = "-1"
        End Try
        Return val
    End Function

    <WebMethod()> _
    Public Function RecCount(ByVal TableName As String) As Integer
        Dim val As Integer = 0
        Try
            val = GV.FL_WebService.RecCount(TableName)
        Catch ex As Exception
            Return val = -1
        End Try
        Return val
    End Function

    <WebMethod()> _
    Public Function getAutoNumber(ByVal FieldName As String) As String
        Dim val As String = ""
        Try
            val = GV.FL_WebService.getAutoNumber(FieldName).ToString
        Catch ex As Exception
            Return val = "-1"
        End Try
        Return val
    End Function

    <WebMethod()> _
    Public Function sendSMSThroughAPI(ByVal MobileNos As String, txtMessage As String) As String
        Dim val As String = "Success"
        Try
            val = GV.sendSMSThroughAPI(MobileNos, txtMessage)
        Catch ex As Exception
            Return val = "Error"
        End Try
        Return val
    End Function

End Class