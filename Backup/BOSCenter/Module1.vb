Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.SmtpClient
Imports System.Text
Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net

Module Module1

    Public ds As DataSet
    Public Customer_MenuCookies As New HttpCookie("Customer_MenuCookies")
    Public APIKey As String = "UTwHXNFqMTAUPrW5wktuluSARpx7SQ2k3lFh14sZ"
    Public VerifyServiceCharge As Decimal = 4
    Public TDS_Per As Decimal = 0.05 '5%
    Public GST_Per As Decimal = 0.847457627118644 ' 18%

    Public auth_endpoint As String = "https://www.instamojo.com/oauth2/token/"
    Public endpoint As String = "https://api.instamojo.com/v2/"
    Public client_id As String = "dKbCrLvyPMRNTKKhzknozPjK5br6e0G2Z9Uu9QxS"
    Public client_secret As String = "hBriZIe7dE7MfkgqALkzdfxTvJkpC2x8g4xk5rsXPtejlDrF6FVP27Crj5DCwXKwvGcDcJXTmRnhNJSal8KwSQzbrEMpEbVnlw42GjOjYLzW9eopagwrtXCOPA5LrPJE"

    Public Function canPerformOperation(CurrentForm As String, CheckOption As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim SearchOperation As String = ""
            If CheckOption.Trim.ToUpper = "SAVE" Then
                SearchOperation = "CanSave"
            ElseIf CheckOption.Trim.ToUpper = "Search".ToUpper Then
                SearchOperation = "CanSearch"
            ElseIf CheckOption.Trim.ToUpper = "Update".ToUpper Then
                SearchOperation = "CanUpdate"
            ElseIf CheckOption.Trim.ToUpper = "Delete".ToUpper Then
                SearchOperation = "CanDelete"
            End If
            If Not SearchOperation.Trim = "" Then
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurrentForm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If DataRows(D).Item(SearchOperation) = True Then
                                result = True
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
        Return result
    End Function
End Module
