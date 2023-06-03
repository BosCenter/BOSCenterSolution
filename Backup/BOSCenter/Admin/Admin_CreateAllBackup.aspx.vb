Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.IO
Imports Ionic
Imports Ionic.Zip

Public Class Admin_CreateAllBackup
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                GV.FL.AddInDropDownListDistinct(ddlSelectDatabase, "db_name(s_mf.database_id) ", "sys.master_files s_mf where s_mf.state = 0 and not file_guid is null and  not db_name(s_mf.database_id)='msdb'")
                If ddlSelectDatabase.Items.Count > 0 Then
                    ddlSelectDatabase.Items.Insert(0, ":::: All Database ::::")
                    ddlSelectDatabase.Items.Insert(0, ":::: Select Database ::::")
                End If


                GetBackupList()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Dim ds As DataSet

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        Try

            lblError1.Text = ""
            lblError1.CssClass = ""

            If ddlSelectDatabase.Items.Count <= 0 Then
                Exit Sub
            End If
            If ddlSelectDatabase.SelectedValue.ToUpper = ":::: Select Database ::::".ToUpper Then
                Exit Sub
            End If

            RemovePerviousBackup()

            Dim backupDIR As String = "E:\NIDHI-Backup\CMP10175"
            If Not System.IO.Directory.Exists(backupDIR) Then
                System.IO.Directory.CreateDirectory(backupDIR)
            End If

            Dim FullPAth As String = ""
            Dim Qry As String = ""


            If ddlSelectDatabase.SelectedValue.Trim.ToUpper = ":::: All Database ::::".ToUpper Then
                For i As Integer = 0 To ddlSelectDatabase.Items.Count - 1
                    If Not ddlSelectDatabase.Items(i).Value.Trim.ToUpper = ":::: All Database ::::".ToUpper Then
                        FullPAth = backupDIR & "\" & ddlSelectDatabase.Items(i).Value.ToUpper.Trim & ".bak"
                        If Qry.Trim = "" Then
                            Qry = " backup database " & GV.FL.parseString(ddlSelectDatabase.Items(i).Value.Trim) & " to disk='" & FullPAth & "' ; "
                        Else
                            Qry = Qry & " backup database " & GV.FL.parseString(ddlSelectDatabase.Items(i).Value.Trim) & " to disk='" & FullPAth & "' ; "
                        End If
                    End If

                Next
            Else
                FullPAth = backupDIR & "\" & ddlSelectDatabase.SelectedValue.ToUpper.Trim & ".bak"
                Qry = "backup database " & GV.FL.parseString(ddlSelectDatabase.SelectedValue.Trim) & " to disk='" & FullPAth & "'"
            End If


            If Qry.Trim = "" Then
                Exit Sub
            End If

            GV.FL.DMLQuery_withCommand(Qry)

            'ds = New DataSet
            'ds = GV.FL.OpenDsWithSelectQuery(Qry)

            If System.IO.File.Exists(FullPAth) Then
                lblError1.Text = "Backup database successfully"
                lblError1.CssClass = "Successlabels"
            End If
            GetBackupList()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DownloadFiles(ByVal sender As Object, ByVal e As EventArgs)
        Using zip As New ZipFile()
            zip.AlternateEncodingUsage = ZipOption.AsNecessary
            zip.AddDirectoryByName("Files")
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim filePath As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            zip.AddFile(filePath, "Files")
            Response.Clear()
            Response.BufferOutput = False
            Dim zipName As String = [String].Format("Backup_{0}.zip", Path.GetFileNameWithoutExtension(filePath) & "_" & DateTime.Now.ToString("dd-MMM-yyyy"))
            Response.ContentType = "application/zip"
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
            zip.Save(Response.OutputStream)
            Response.[End]()
        End Using
    End Sub

    Public Sub GetBackupList()
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            Dim path As String = "E:\NIDHI-Backup\CMP10175\" '"C:\WIPLBACKUP\MSSQLBACKUP"  '"E:\NIDHI-Backup\CMP10175\"
            Dim diFiles As New DirectoryInfo(path)
            GridView1.DataSource = diFiles.GetFiles("*.bak")
            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub RemovePerviousBackup()
        Try

            Dim path As String = "E:\NIDHI-Backup\CMP10175\"
            If ddlSelectDatabase.SelectedValue.Trim.ToUpper = ":::: All Database ::::".ToUpper Then
                If System.IO.Directory.Exists(path) Then
                    Dim diFiles As New DirectoryInfo(path)
                    diFiles.Delete(True)
                End If
            Else
                Dim FullPAth As String = path & "\" & ddlSelectDatabase.SelectedValue.ToUpper.Trim & ".bak"
                If System.IO.File.Exists(FullPAth) Then
                    System.IO.File.Delete(FullPAth)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If e.CommandName = "btnfrmselectAll" Then
                'Using zip As New ZipFile()
                '    zip.AlternateEncodingUsage = ZipOption.AsNecessary
                '    zip.AddDirectoryByName("Files")
                '    For i As Integer = 0 To GridView1.Rows.Count - 1
                '        Dim filePath As String = GV.parseString(GridView1.Rows(i).Cells(6).Text)
                '        zip.AddFile(filePath, "Files")
                '    Next

                '    Response.Clear()
                '    Response.BufferOutput = False
                '    Dim zipName As String = [String].Format("Backup_{0}.zip", "AllDatabase" & "_" & DateTime.Now.ToString("dd-MMM-yyyy"))
                '    Response.ContentType = "application/zip"
                '    Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
                '    zip.Save(Response.OutputStream)
                '    Response.[End]()
                'End Using
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetLastFiveBackupList()
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            Dim path As String = "E:\ALLBackups\MSSQLBACKUP\"
            Dim diFiles As New DirectoryInfo(path)
            GridView1.DataSource = diFiles.GetFiles("*.rar")
            GridView1.DataBind()
        
            If GridView1.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    Dim btnDel As LinkButton = DirectCast(GridView1.Rows(i).FindControl("LinkButton1"), LinkButton)
                    If Not btnDel Is Nothing Then
                        btnDel.Visible = False
                    End If
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnLastFiveBackups_Click(sender As Object, e As EventArgs) Handles btnLastFiveBackups.Click
        Try
            GetLastFiveBackupList()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            GetBackupList()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub DeleteRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            ' Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)

            Dim filePath As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            lblRID.Text = filePath
            lblDialogMsg.Text = "Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            Button2.Visible = True
            ModalPopupExtender1.Show()

           


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            If Not lblRID.Text = "" Then
              
                If System.IO.File.Exists(lblRID.Text) Then
                    System.IO.File.Delete(lblRID.Text)
                    lblDialogMsg.Text = "Backup deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    GetBackupList()
                End If

                btnCancel.Text = "OK"
                Button2.Visible = False
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class