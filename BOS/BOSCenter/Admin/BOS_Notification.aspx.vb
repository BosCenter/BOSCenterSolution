Imports System.IO
Imports System.Net

Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class BOS_Notification
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim NotificationID, NotificationDate, AgentType, ActiveStatus, NotificationPic, RecordDatetime, UpdatedOn, UpdatedBy As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    'Page Load
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            If Not IsPostBack Then
                txtNotificationID.Text = GV.FL.getAutoNumber("TransId")
                'txtNotification_Date.Text = Now.Date.ToString("dd/MM/yyyy")
                Session("Workfor") = "Save"
                Session("EditFlag") = 0
                Bind()
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Save
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            Dim iserrorfound As Boolean = False

            If txtNotificationID.Text = "" Then

                If iserrorfound = False Then
                    iserrorfound = True
                    txtNotificationID.CssClass = "ValidationError"
                    lblError.Text = "Please Enter Notification ID"
                    txtNotificationID.Focus()
                End If
            Else
                txtNotificationID.CssClass = "form-control"
            End If

            'If txtNotification_Date.Text = "" Then
            '    If iserrorfound = False Then
            '        iserrorfound = True
            '        txtNotification_Date.CssClass = "ValidationError"
            '        lblError.Text = "Please Enter Notification Date"
            '        txtNotification_Date.Focus()
            '    End If
            'Else
            '    txtNotification_Date.CssClass = "form-control"
            'End If

            If ddl_AgentType.SelectedIndex = 0 Then

                If iserrorfound = False Then
                    iserrorfound = True
                    ddl_AgentType.CssClass = "ValidationError"
                    lblError.Text = "Please Select Agent Type."
                    ddl_AgentType.Focus()
                End If
            Else
                AgentType = ddl_AgentType.SelectedValue
                ddl_AgentType.CssClass = "form-control"
            End If

            If ddl_ActiveStatus.SelectedIndex = 0 Then

                If iserrorfound = False Then
                    iserrorfound = True
                    ddl_ActiveStatus.CssClass = "ValidationError"
                    lblError.Text = "Please Select Active Status."
                    ddl_ActiveStatus.Focus()
                End If
            Else
                ddl_ActiveStatus.CssClass = "form-control"
            End If

            If btnUpload_OriginalPic.ToolTip = "" Then
                iserrorfound = True
                lblError.Text = "Please Upload Notification Pic."
                FileUpload_OroginalPic.Focus()
            End If

            If iserrorfound = True Then
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            


            'If Not txtProduct_Pic.Text.Trim = "" Then
            '    VProduct_Pic = GV.parseString(txtProduct_Pic.Text.Trim)
            'Else
            '    VProduct_Pic = ""
            'End If

            'NotificationID, NotificationDate, AgentType, ActiveStatus, NotificationPic, RecordDatetime, UpdatedOn, UpdatedBy

            If btnSave.Text = "Save" Then
                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master Where AgentType='" & AgentType & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                btnPopupYes.Text = "Yes"
                btnPopupYes.Attributes("Style") = ""
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Save??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            Else
                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master Where (AgentType='" & AgentType & "' and not AgentType='" & lblOld_value.Text.Trim & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                btnPopupYes.Text = "Yes"
                btnPopupYes.Attributes("Style") = ""
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Update??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnPopupYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopupYes.Click
        Try
            If Session("Workfor") = "Delete" Then

                Dim QryStr As String = "delete from  " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master where RID=" & lblRID.Text.Trim & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    Dim imgpath As String = btnUpload_OriginalPic.ToolTip
                    If File.Exists(Server.MapPath(imgpath)) Then
                        File.Delete(Server.MapPath(imgpath))
                    End If
                    lblDialogMsg.Text = "Record deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "Sorry !! Process Can't be Completed.."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnPopupYes.Attributes("Style") = "Display:None"
                ModalPopupExtender1.Show()
                Bind()
                Clear()
                btnDelete.Focus()
                Exit Sub
            End If
            If Session("Workfor") = "GridRowDelete" Then

                Dim QryStr As String = "delete from  " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master where RID=" & lblRID.Text.Trim & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    Dim imgpath As String = btnUpload_OriginalPic.ToolTip
                    If File.Exists(Server.MapPath(imgpath)) Then
                        File.Delete(Server.MapPath(imgpath))
                    End If
                    lblDialogMsg.Text = "Record deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "Sorry !! Process Can't be Completed.."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnPopupYes.Attributes("Style") = "Display:None"
                ModalPopupExtender1.Show()
                Bind()
                Clear()
                Exit Sub
            End If
            lblError.Text = ""
            lblError.CssClass = ""
            'NotificationID, NotificationDate, AgentType, ActiveStatus, NotificationPic, RecordDatetime, UpdatedOn, UpdatedBy


            RecordDatetime = Now
            UpdatedOn = Now
            UpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim

            Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

            NotificationID = GV.parseString(txtNotificationID.Text.Trim)
            'If Not txtNotification_Date.Text.Trim = "" Then
            '    NotificationDate = GV.FL.returnDateMonthWise(txtNotification_Date.Text.Trim)
            'Else
            '    NotificationDate = ""
            'End If
            NotificationDate = Now.Date

            If ddl_ActiveStatus.Items.Count > 0 Then
                If Not ddl_ActiveStatus.SelectedIndex = 0 Then
                    ActiveStatus = GV.parseString(ddl_ActiveStatus.SelectedValue.Trim)
                Else
                    ActiveStatus = ""
                End If
            End If

            If ddl_AgentType.Items.Count > 0 Then
                If Not ddl_AgentType.SelectedValue.Trim = "" Then
                    AgentType = GV.parseString(ddl_AgentType.SelectedValue.Trim)
                Else
                    AgentType = ""
                End If
            End If

            'If Not txtProduct_Pic.Text.Trim = "" Then
            '    VProduct_Pic = GV.parseString(txtProduct_Pic.Text.Trim)
            'Else
            '    VProduct_Pic = ""
            'End If
            '//////// ======Start get All Uploaded Image Path ==================================

            If Not btnUpload_OriginalPic.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_OriginalPic.ToolTip.Trim)
                'VOriginalPic = "~/MemberDocument/CompanyCode/" & VCompanyCode & "/" & VMemberID & "/" & fi.Name
                NotificationPic = "~/MakePaymentDocs/" & CompanyCode.Trim & "_" & ddl_AgentType.SelectedValue.Trim & "_" & fi.Name.Trim
            Else
                NotificationPic = ""
            End If


            If Session("EditFlag") = 0 Then


                Dim QryStr As String = "insert into  " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master (NotificationID, NotificationDate, AgentType, ActiveStatus, NotificationPic, RecordDatetime, UpdatedOn, UpdatedBy) values( '" & NotificationID & " ','" & NotificationDate & " ','" & AgentType & " ','" & ActiveStatus & " ','" & NotificationPic & " ','" & RecordDatetime & "','" & UpdatedOn & "','" & UpdatedBy & "' )"
                If GV.FL.DMLQueries(QryStr) = True Then
                    Dim destinationpath As String = "~/MakePaymentDocs/"
                    If Not btnUpload_OriginalPic.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_OriginalPic.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_OriginalPic.ToolTip.Trim), Server.MapPath(NotificationPic))
                        End If
                    End If
                    Clear()
                    Bind()

                    lblError.Text = "Record Saved Successfully."
                    lblError.CssClass = "Successlabels"

                    'btnCancel.Text = "Ok"
                    'btnPopupYes.Attributes("style") = "display:none"
                    'ModalPopupExtender1.Show()
                Else
                    lblError.Text = "Record Insertion Failed."
                    lblError.CssClass = "errorlabels"
                    'btnCancel.Text = "Ok"
                    'btnPopupYes.Attributes("style") = "display:none"
                    'ModalPopupExtender1.Show()
                End If

            ElseIf Session("EditFlag") = 1 Then

                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master Where (AgentType='" & AgentType & "' and not AgentType='" & lblOld_value.Text.Trim & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                'NotificationID, NotificationDate, AgentType, ActiveStatus, NotificationPic, RecordDatetime, UpdatedOn, UpdatedBy



                Dim QryStr As String = "update  " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master set  AgentType='" & AgentType & "', ActiveStatus='" & ActiveStatus & "', NotificationPic='" & NotificationPic & "', UpdatedBy='" & UpdatedBy & "', UpdatedOn='" & UpdatedOn & "'  where RID=" & lblRID.Text.Trim & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    Dim destinationpath As String = "~/MakePaymentDocs/"
                    If Not btnUpload_OriginalPic.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_OriginalPic.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_OriginalPic.ToolTip.Trim), Server.MapPath(NotificationPic))
                        End If
                    End If
                    lblDialogMsg.Text = "Record Updated Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                    Clear()
                    Bind()
                Else
                    lblDialogMsg.Text = "Process Cann't be Complited."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                End If
            End If



        Catch ex As Exception
        End Try
    End Sub

    'Clear
    Private Sub Clear()
        Try
            ddl_ActiveStatus.Enabled = True
            ddl_ActiveStatus.CssClass = "form-control"

            ddl_AgentType.Enabled = True
            ddl_AgentType.CssClass = "form-control"

            'txtNotification_Date.CssClass = "form-control"
            'txtNotification_Date.Enabled = True
            'txtNotification_Date.CssClass = "form-control"
            txtNotificationID.Text = GV.FL.getAutoNumber("TransId")

            'If ddlProductCategory.Items.Count > 0 Then
            '    ddlProductCategory.SelectedIndex = 0
            'End If
            Image_OriginalPic.ImageUrl = "~/images/uploadimage.png"
            btnUpload_OriginalPic.Text = "Upload"
            btnRemove_OroginalPic.Enabled = False

            'If ddlProduct_BrandCode.Items.Count > 0 Then
            '    ddlProduct_BrandCode.SelectedIndex = 0
            'End If

            formheading3.Text = "Add New Notification"
            ' txtProduct_Pic.Text = ""
            'txtNotification_Date.Text = Now.Date.ToString("dd/MM/yyyy")
            Session("EditFlag") = 0
            btnSave.Text = "Save"
            btnClear.Text = "Reset"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            Session("Workfor") = "Save"
            txtNotificationID.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()
            Bind()
        Catch ex As Exception
        End Try
    End Sub

    'Search
    Public Sub Bind()
        Try
            'Dim VBradCode, VorderCategory As String
            'If Not ddl_AgentType.SelectedIndex = 0 Then
            '    VBradCode = "where Product_BrandCode='" & ddl_AgentType.SelectedValue.Trim & "'  "
            'Else
            '    VBradCode = ""
            'End If
            'If VBradCode = "" Then
            '    If Not ddlProductCategory.SelectedIndex = 0 Then
            '        VorderCategory = "where Product_OrderCategory='" & ddlProductCategory.SelectedValue.Trim & "'  "
            '    Else
            '        VorderCategory = ""
            '    End If
            'Else
            '    If Not ddlProductCategory.SelectedIndex = 0 Then
            '        VorderCategory = "and Product_OrderCategory='" & ddlProductCategory.SelectedValue.Trim & "'  "
            '    Else
            '        VorderCategory = ""
            '    End If
            'End If


            Dim qry As String = "select RID as SrNo,NotificationID,(CONVERT(VARCHAR(11),NotificationDate,106)) as NotificationDate,[AgentType],[NotificationPic],[ActiveStatus] from  " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master order by RID desc  "
            DS = GV.FL.OpenDsWithSelectQuery(qry)
            GridView1.DataSource = DS.Tables(0)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                lblExportQry.Text = qry.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.showSerialnoOnGridView(GridView1, 1)
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Delete
    Protected Sub btnGrdRowdelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim lblgrdRID As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("lblgrdRID"), Label)
            lblRID.Text = lblgrdRID.Text
            Session("Workfor") = "GridRowDelete"
            lblDialogMsg.Text = "Are you sure you want to delete?"
            lblDialogMsg.CssClass = ""
            btnCancel.Text = "No"
            btnPopupYes.Text = "Yes"
            btnPopupYes.Attributes("Style") = ""
            ModalPopupExtender1.Show()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnDelete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Session("Workfor") = "Delete"
            lblDialogMsg.Text = "Are you sure you want to delete ?"
            lblDialogMsg.CssClass = ""
            btnCancel.Text = "No"
            btnPopupYes.Attributes("Style") = ""
            btnPopupYes.Text = "Yes"
            ModalPopupExtender1.Show()
        Catch ex As Exception
        End Try
    End Sub

    'Edit
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lblgrdRID As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("lblgrdRID"), Label)
            lblRID.Text = lblgrdRID.Text
            lblOld_value.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            txtNotificationID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            'Dim Pdate As Date = CDate(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            'txtNotification_Date.Text = Pdate.ToString("dd/MM/yyyy")
            ddl_ActiveStatus.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            ddl_AgentType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            'txtNotification_Date.Enabled = False
            'txtNotification_Date.CssClass = "form-control"
            ddl_AgentType.CssClass = "form-control"
            ddl_ActiveStatus.CssClass = "form-control"
            txtNotificationID.CssClass = "form-control"


            'Image_OriginalPic.ImageUrl = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)


            DS = GV.FL.OpenDs(" " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master where RID=" & lblRID.Text & "")
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("NotificationPic")) Then

                        If Not DS.Tables(0).Rows(0).Item("NotificationPic").ToString() = "" Then
                            filePath = DS.Tables(0).Rows(0).Item("NotificationPic").ToString()
                            filename = Path.GetFileName(filePath)
                            ext = Path.GetExtension(filename)
                            If (ext.Trim.ToUpper = ".jpg".Trim.ToUpper) Or (ext.Trim.ToUpper = ".jpeg".Trim.ToUpper) Or (ext.Trim.ToUpper = ".png".Trim.ToUpper) Or (ext.Trim.ToUpper = ".gif".Trim.ToUpper) Then

                                btnUpload_OriginalPic.ToolTip = GV.parseString(DS.Tables(0).Rows(0).Item("NotificationPic").ToString())
                                Image_OriginalPic.ImageUrl = btnUpload_OriginalPic.ToolTip
                            End If
                            btnUpload_OriginalPic.Text = "Download"
                            btnRemove_OroginalPic.Enabled = True
                        Else
                            btnUpload_OriginalPic.ToolTip = ""
                            Image_OriginalPic.ImageUrl = "~/images/uploadimage.png"
                        End If
                    Else
                        btnUpload_OriginalPic.ToolTip = ""
                        Image_OriginalPic.ImageUrl = "~/images/uploadimage.png"
                    End If

                End If
            End If


            Session("EditFlag") = 1
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            formheading3.Text = "Edit Notification Details"
            For i As Integer = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).BackColor = Color.White
                GridView1.Rows(i).Cells(0).ForeColor = Color.Black
            Next
            GridView1.Rows(gvrow.RowIndex).BackColor = Color.LightBlue
            GridView1.Rows(gvrow.RowIndex).Cells(0).ForeColor = Color.White
            txtNotificationID.Focus()

        Catch ex As Exception
        End Try
    End Sub

    'Exporting
    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImagebtnExcel.Click
        Try
            ' Dim Excelqry As String = "select RID as SrNo, Product_Date,Product_OrderCategory,Product_Code,Product_Descp,Product_BrandCode,Product_Num,Product_Pic from Bos_Notification_Master "
            If GridView1.Rows.Count > 0 Then
                GV.ExportToExcel_New(GridView1, Response, "", "NotificationDetails", lblExportQry.Text, "dyanamic")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub ImagebtnWOrd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImagebtnWOrd.Click
        Try
            'Dim Wordqry As String = "select RID as SrNo, Product_Date,Product_OrderCategory,Product_Code,Product_Descp,Product_BrandCode,Product_Num,Product_Pic from Bos_Notification_Master "
            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "NotificationDetails", lblExportQry.Text, "dyanamic")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Imagepdf_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Imagepdf.Click
        Try
            ' Dim PDFqry As String = "select RID as SrNo, Product_Date,Product_OrderCategory,Product_Code,Product_Descp,Product_BrandCode,Product_Num,Product_Pic from Bos_Notification_Master "
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "NotificationDetails", lblExportQry.Text, "dyanamic")
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Gridview Indexes
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub
    Protected Sub ddlNoOfRecords_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNoOfRecords.SelectedIndexChanged
        Try
            If ddlNoOfRecords.SelectedValue = "10 Record(s)" Then
                GridView1.PageSize = 10
            ElseIf ddlNoOfRecords.SelectedValue = "25 Record(s)" Then
                GridView1.PageSize = 25
            ElseIf ddlNoOfRecords.SelectedValue = "50 Record(s)" Then
                GridView1.PageSize = 50
            ElseIf ddlNoOfRecords.SelectedValue = "100 Record(s)" Then
                GridView1.PageSize = 100
            ElseIf ddlNoOfRecords.SelectedValue = "200 Record(s)" Then
                GridView1.PageSize = 200
            ElseIf ddlNoOfRecords.SelectedValue = "500 Record(s)" Then
                GridView1.PageSize = 500
            End If
            Bind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
        End Try

    End Sub

    'Events
    'Image
    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""
    Protected Sub btnUpload_OriginalPic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_OriginalPic.Click
        Try
            lblErrorImage_OriginalPic.Text = ""
            lblErrorImage_OriginalPic.CssClass = ""
            If (btnUpload_OriginalPic.Text = "Download") Then
                DownloadDoc(btnUpload_OriginalPic.ToolTip)
            Else

                If FileUpload_OroginalPic.HasFile = True Then

                    filePath = FileUpload_OroginalPic.PostedFile.FileName
                    filename = Path.GetFileName(filePath)
                    ext = Path.GetExtension(filename)

                    If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                        SaveImage(FileUpload_OroginalPic, btnUpload_OriginalPic, btnRemove_OroginalPic, "Product")
                        Dim fi As New FileInfo(btnUpload_OriginalPic.ToolTip.ToString())
                        Dim ext As String = fi.Extension.ToUpper
                        If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                            Image_OriginalPic.ImageUrl = btnUpload_OriginalPic.ToolTip.ToString()
                            btnUpload_OriginalPic.Focus()
                        End If
                    Else
                        lblInformation.Text = "Invalid Image Type."
                        lblInformation.CssClass = "errorlabels"
                        ModalPopupExtender3.Show()
                        btnUpload_OriginalPic.Focus()
                    End If

                Else
                    lblErrorImage_OriginalPic.Text = "Please Select Image To Upload."
                    lblErrorImage_OriginalPic.CssClass = "errorlabels"
                    btnUpload_OriginalPic.Focus()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button, ByVal imgstr As String)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                    Dim completeFilePath As String = ""


                    Dim aa As String = "~/Temp/"
                    If Not Directory.Exists(Server.MapPath(aa)) Then
                        Directory.CreateDirectory(Server.MapPath(aa))
                    End If
                    Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                    completeFilePath = Server.MapPath("~/Temp" & "/" & CompanyCode.Trim & "_" & txtNotificationID.Text.Trim & "_" & Session("ext"))
                    imgPath = "~/Temp" & "/" & CompanyCode.Trim & "_" & txtNotificationID.Text.Trim & "_" & Session("ext")

                    imagUpload.PostedFile.SaveAs(completeFilePath)

                    Dim bytesInStream As Byte() = New Byte(imagUpload.PostedFile.InputStream.Length - 1) {}
                    Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(imagUpload.PostedFile.InputStream)

                    Dim bmp As New Bitmap(image)
                    bmp = ImageOptimization(bmp, completeFilePath, filename, "THUMBNAIL")
                    bmp.Save(completeFilePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                    'bmp.Save(completeFilePath + "/Thumbnail_" + filename, System.Drawing.Imaging.ImageFormat.Jpeg)



                    UploadButtonName.ToolTip = imgPath
                    UploadButtonName.Text = "Download"
                    RemoveButtonName.Enabled = True



                Else
                End If
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function ImageOptimization(ByVal image As Image, ByVal targetPath As String, ByVal fileName As String, ByVal imagetype As String) As Bitmap

        fileName = Path.GetFileName(fileName)
        fileName.Replace("\", "")
        Dim namefileName = fileName.Substring(0, fileName.LastIndexOf("."))
        Dim ext1 As String = Path.GetExtension(fileName)
        Dim imgPath As String = ""


        Dim reqWidth As Integer = 0
        Dim reqHeight As Integer = 0
        reqWidth = image.Width
        reqHeight = image.Height

        Dim thumbnailImg = Resize_Image(DirectCast(image, Bitmap), reqWidth, reqHeight)


        imgPath = Convert.ToString((targetPath & Convert.ToString("/"c)) + namefileName) & ext1

        If System.IO.File.Exists(imgPath) Then
            System.IO.File.Delete(imgPath)
        End If

        Return thumbnailImg
    End Function
    Private Function Resize_Image(ByVal streamImage As Bitmap, ByVal maxWidth As Integer, ByVal maxHeight As Integer) As Bitmap
        Dim originalImage As New Bitmap(streamImage)
        Dim newWidth As Integer = originalImage.Width
        Dim newHeight As Integer = originalImage.Height
        Dim aspectRatio As Double = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height)

        If aspectRatio <= 1 AndAlso originalImage.Width > maxWidth Then
            newWidth = maxWidth
            newHeight = Convert.ToInt32(Math.Round(newWidth / aspectRatio))
        ElseIf aspectRatio > 1 AndAlso originalImage.Height > maxHeight Then
            newHeight = maxHeight
            newWidth = Convert.ToInt32(Math.Round(newHeight * aspectRatio))
        End If
        Return New Bitmap(originalImage, newWidth, newHeight)
    End Function
    Public Sub DownloadDoc(ByVal DocPath As String)
        Try

            Dim fi As New FileInfo(DocPath)
            Dim strURL As String = DocPath
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" & fi.Name & """")
            Dim data As Byte() = req.DownloadData(Server.MapPath(strURL))
            response.BinaryWrite(data)


            response.[End]()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove_OroginalPic.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "ProductImage"
            lblDeleteDocumentInfo.Visible = False

            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                '  VListId = GV.parseString(txtListId.Text.Trim)
                'VCompanyCode = VCompanyCode

                If lblDeleteDocumentInfo.Text = "ProductImage" Then

                    Dim imgpath As String = btnUpload_OriginalPic.ToolTip
                    If File.Exists(Server.MapPath(imgpath)) Then
                        File.Delete(Server.MapPath(imgpath))
                    End If

                    qry = "update  " & GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master set NotificationPic=''  where NotificationID='" & txtNotificationID.Text.Trim & "';"
                    GV.FL.DMLQueriesBulk(qry)

                    btnUpload_OriginalPic.ToolTip = ""
                    btnUpload_OriginalPic.Text = "Upload"
                    btnRemove_OroginalPic.Enabled = False
                    Image_OriginalPic.ImageUrl = "~/images/uploadimage.png"
                    btnUpload_OriginalPic.Focus()

                    lblDeleteInfo.Text = "Image Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"


                End If


                If Not qry = "" Then
                    GV.FL.DMLQueries(qry)
                End If

                btnDelete_Document.Visible = False
                btnDelete_Document.Text = "OK"
                btnCancelDeleteDocument.Text = "OK"

            Else

            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception

        End Try
    End Sub



End Class