
Public Class AddMessageTemplates
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
                lblSessionFlag.Text = 0
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Dim flag As Integer = 0

    Dim VUpdatedBy, VUpdatedOn As String
    Public Sub Bind()
        Try
            ds = GV.FL.OpenDsWithSelectQuery("select max(RID) as RID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SMSTemplateMaster")
            If IsDBNull(ds.Tables(0).Rows(0).Item("RID")) Then
                txtMsgID.Text = "1"
            Else
                txtMsgID.Text = CDbl(ds.Tables(0).Rows(0).Item("RID")) + 1
            End If

            GV.FL.AddInDropDownListDistinct(ddlCategory, "Category", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SMSCategoryMaster")
            ddlCategory.Items.Insert(0, ":::: Select Category ::::")
            GV.FL.AddInListDistinct(lstTemplates, "Title", "CRM_SMSTemplateMaster")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            'Session("Editflag") = 0
            lblSessionFlag.Text = 0
            txtMsgTitle.Text = ""
            txtMessage.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""
            txtMsgTitle.Enabled = True
            ddlCategory.Enabled = True
            btnSave.Text = "Save"
            btndelete.Enabled = False

            Bind()
        Catch ex As Exception

        End Try

    End Sub
    Dim QryStr As String = ""
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If btnSave.Text.Trim.ToUpper = "Save".Trim.ToUpper Then
                Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If Not DataRows(D).Item("CanSave") = True Then
                                lblDialogMsg.CssClass = ""
                                lblDialogMsg.Text = "Not Autorized To Performe This Action."
                                btnCancel.Text = "Ok"
                                btnok.Visible = False
                                ModalPopupExtender1.Show()
                                Exit Sub
                            End If
                        Next

                    End If
                End If
            ElseIf btnSave.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If Not DataRows(D).Item("CanUpdate") = True Then
                                lblDialogMsg.CssClass = ""
                                lblDialogMsg.Text = "Not Autorized To Performe This Action."
                                btnCancel.Text = "Ok"
                                btnok.Visible = False
                                ModalPopupExtender1.Show()
                                Exit Sub
                            End If
                        Next

                    End If
                End If
            End If
            If ddlCategory.Text.Trim = "" Then
                lblError.Text = "Please Enter Category"
                lblError.CssClass = "errorlabels"
                ddlCategory.Focus()
                Exit Sub
            ElseIf txtMsgTitle.Text.Trim = "" Then
                lblError.Text = "Please Enter Message Title"
                lblError.CssClass = "errorlabels"
                txtMsgTitle.Focus()
                Exit Sub
            ElseIf txtMessage.Text.Trim = "" Then
                txtMessage.Focus()
                lblError.Text = "Please Enter Message "
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else

                VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                VUpdatedOn = "getdate()"
                If lblSessionFlag.Text = 1 Then
                    'Session("Editflag") = 0

                    QryStr = ("update  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SMSTemplateMaster set UpdatedBy='" & VUpdatedBy & "',UpdatedOn=" & VUpdatedOn & ", Category='" & GV.parseString(ddlCategory.SelectedValue) & "', Title='" & GV.parseString(txtMsgTitle.Text.Trim) & "', [Message]='" & GV.parseString(txtMessage.Text.Trim) & "' where RID=" & txtMsgID.Text & " ")
                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        BtnRefresh_Click(sender, e)
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        '  lblSessionFlag.Text = 1
                        lblError.Text = "Record Updation failed."
                        lblError.CssClass = "errorlabels"
                    End If
                Else
                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SMSTemplateMaster where Title='" & GV.parseString(txtMsgTitle.Text.Trim) & "'") > 0 Then
                        lblError.Text = "Message Title Exists, Sorry It Cann't Be Saved!!!!"
                        lblError.CssClass = "errorlabels"
                        Exit Sub
                    Else
                        QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SMSTemplateMaster(Category,Title,Message,UpdatedBy,UpdatedOn) values('" & GV.parseString(ddlCategory.Text.Trim) & "','" & GV.parseString(txtMsgTitle.Text.Trim) & "','" & GV.parseString(txtMessage.Text.Trim) & "','" & VUpdatedBy & "'," & VUpdatedOn & ")"
                        If GV.FL.DMLQueries(QryStr) = True Then
                            BtnRefresh_Click(sender, e)
                            lblError.Text = "Record Saved Successfully."
                            lblError.CssClass = "Successlabels"
                        Else
                            lblError.Text = "Record Insertion failed."
                            lblError.CssClass = "errorlabels"
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Dim x As Integer = 140


    Private Sub lstTemplates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTemplates.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ds = New DataSet
            ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SMSTemplateMaster where  Title='" & lstTemplates.SelectedItem.Value & "' ")
            If ds.Tables(0).Rows.Count > 0 Then
                txtMsgID.Text = ds.Tables(0).Rows(0).Item("RID").ToString
                ddlCategory.SelectedValue = ds.Tables(0).Rows(0).Item("Category").ToString
                txtMsgTitle.Text = ds.Tables(0).Rows(0).Item("Title").ToString
                txtMessage.Text = ds.Tables(0).Rows(0).Item("Message").ToString
                ddlCategory.Enabled = False
                txtMsgTitle.Enabled = False
                'Session("Editflag") = 1
                lblSessionFlag.Text = 1
                btnSave.Text = "Update"
                btndelete.Enabled = True
            Else
                'Session("Editflag") = 0
                lblSessionFlag.Text = 0
                btnSave.Text = "Save"
                ddlCategory.Enabled = True
                btndelete.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btndelete.Click
        Try
            Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            Dim LocalDS As New DataSet
            LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            Dim DataRows() As DataRow
            DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            If Not DataRows Is Nothing Then
                If DataRows.Count > 0 Then
                    For D As Integer = 0 To DataRows.Count - 1
                        If Not DataRows(D).Item("CanDelete") = True Then
                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If
            If lblSessionFlag.Text = 1 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Are you sure you want to delete ?"
                btnCancel.Text = "Cancel"
                btnok.Visible = True

                lblRID.Text = txtMsgID.Text

                Me.ModalPopupExtender1.Show()

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim result As Boolean = GV.FL.DMLQueries("delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SMSTemplateMaster where RID='" & lblRID.Text & "'")
            If result = True Then
                lblDialogMsg.Text = "Record deleted Successfully."
                lblDialogMsg.CssClass = "Successlabels"
                Bind()
                BtnRefresh_Click(sender, e)
            Else
                lblDialogMsg.Text = "Sorry !! Record deletion Failed."
                lblDialogMsg.CssClass = "errorlabels"
            End If
            btnCancel.Text = "OK"
            btnok.Visible = False
            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub
End Class