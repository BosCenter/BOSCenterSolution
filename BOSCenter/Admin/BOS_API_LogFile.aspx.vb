Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Net.Mail

Public Class BOS_API_LogFile
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtLogDetails.Text = ""

            If Not IsPostBack Then

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


   

    Protected Sub btnClearLogs_Click(sender As Object, e As EventArgs) Handles btnClearLogs.Click
        Try

            Dim FileName As String = ""

            If ddlAPILogs.SelectedIndex = 0 Then
                FileName = ""
            ElseIf ddlAPILogs.SelectedValue = "Recharge API" Then
                FileName = "RECHARGE_API_LOG.txt"
            ElseIf ddlAPILogs.SelectedValue = "Money Transfer API" Then
                FileName = "MONEYTRANSFER_API_LOG.txt"
            ElseIf ddlAPILogs.SelectedValue = "PAN Card API" Then
                FileName = "PANCARDAPI_LOG.txt"
            ElseIf ddlAPILogs.SelectedValue = "AEPS API" Then
                FileName = "AEPS_API_LOG.txt"
            End If


            If Not FileName.Trim = "" Then
                GV.SaveTextToFile("", Server.MapPath(FileName), False)
                If File.Exists(Server.MapPath(FileName)) Then
                    File.Delete(Server.MapPath(FileName))
                End If
            End If

            txtLogDetails.Text = ""

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try

            Response.Redirect("BOS_API_LogFile.aspx")

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            Dim FileName As String = ""
            txtLogDetails.Text = ""
            If ddlAPILogs.SelectedIndex = 0 Then
                FileName = ""
            ElseIf ddlAPILogs.SelectedValue = "Recharge API" Then
                FileName = "RECHARGE_API_LOG.txt"
            ElseIf ddlAPILogs.SelectedValue = "Money Transfer API" Then
                FileName = "MONEYTRANSFER_API_LOG.txt"
            ElseIf ddlAPILogs.SelectedValue = "PAN Card API" Then
                FileName = "PANCARDAPI_LOG.txt"
            End If


            If Not FileName.Trim = "" Then
                txtLogDetails.Text = GV.GetFileContents(Server.MapPath(FileName))
            End If




        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class