
Public Class AddMailTemplates
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                lblError.Text = ""
                Bind()
                lblSessionFlag.Text = 0
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblError.Text = ex.Message
        End Try
    End Sub
    Dim flag As Integer = 0
    Dim QryStr As String
    Dim VUpdatedBy, VUpdatedOn As String
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""
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
            If txtMsgTitle.Text = "" Then
                lblError.Text = "Please Enter Message Title"
                lblError.CssClass = "errorlabels"

                txtMsgTitle.Focus()
                Exit Sub
            ElseIf txtMessage1.Content = "" Then
                txtMessage1.Focus()
                lblError.Text = "Please Enter Message "
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                VUpdatedOn = "getdate()"
                If lblSessionFlag.Text = 1 Then

                    QryStr = (" update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MailTemplateMaster set UpdatedBy='" & VUpdatedBy & "',UpdatedOn=" & VUpdatedOn & ", Title='" & GV.parseString(txtMsgTitle.Text.Trim) & "',Message='" & (txtMessage1.Content) & "' where RID=" & GV.parseString(txtMailID.Text.Trim) & " ")

                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        BtnRefresh_Click(sender, e)
                        lblError.Text = "Record Updated Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        'lblSessionFlag.Text = 1
                        lblError.Text = "Record Updation failed."
                        lblError.CssClass = "errorlabels"
                    End If


                Else
                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MailTemplateMaster where Title='" & GV.parseString(txtMsgTitle.Text.Trim) & "'") > 0 Then
                        lblError.Text = "Message Title Exists, Sorry It Cann't Be Saved!!!!"
                        lblError.CssClass = "errorlabels"
                        Exit Sub
                    Else
                        QryStr = " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MailTemplateMaster(Title,Message,UpdatedBy,UpdatedOn) values('" & GV.parseString(txtMsgTitle.Text.Trim) & "','" & (txtMessage1.Content) & "','" & VUpdatedBy & "'," & VUpdatedOn & ") ;"
                        Dim result As Boolean = GV.FL.DMLQueries(QryStr)
                        If result = True Then
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            'Session("Editflag") = 0
            lblSessionFlag.Text = 0
            txtMsgTitle.Text = ""
            txtMessage1.Content = ""
            lblError.Text = ""

            txtMsgTitle.Enabled = True
            lblError.CssClass = ""
            btnSave.Text = "Save"
            btnClear.Enabled = True
            btndelete.Enabled = False

            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Public Sub Bind()
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ds = GV.FL.OpenDsWithSelectQuery("select max(RID) as RID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MailTemplateMaster")
            If IsDBNull(ds.Tables(0).Rows(0).Item("RID")) Then
                txtMailID.Text = "1"
            Else
                txtMailID.Text = CDbl(ds.Tables(0).Rows(0).Item("RID")) + 1
            End If


            GV.FL.AddInListDistinct(lstTemplates, "Title", "CRM_MailTemplateMaster")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Dim x As Integer = 140

    Private Sub lstTemplates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTemplates.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ds = New DataSet
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
            ds = GV.FL.OpenDs("CRM_MailTemplateMaster where  Title='" & lstTemplates.SelectedItem.Value & "' ")
            If ds.Tables(0).Rows.Count > 0 Then
                txtMailID.Text = ds.Tables(0).Rows(0).Item("RID").ToString

                txtMsgTitle.Text = ds.Tables(0).Rows(0).Item("Title").ToString
                txtMessage1.Content = ds.Tables(0).Rows(0).Item("Message").ToString

                txtMsgTitle.Enabled = False
                'Session("Editflag") = 1
                lblSessionFlag.Text = 1
                btnSave.Text = "Update"
                btnClear.Enabled = True
            Else
                Session("Editflag") = 0
                btnSave.Text = "Save"

                btnClear.Enabled = False
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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
            lblError.Text = ""
            lblError.CssClass = ""
            If lblSessionFlag.Text = 1 Then
                lblDialogMsg.Text = "Are you sure you want to delete ?"
                btnCancel.Text = "Cancel"
                btnok.Visible = True
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim result As Boolean = GV.FL.DMLQueries("delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MailTemplateMaster where RID='" & txtMailID.Text & "'")
            'lblDialogMsg.Text = result
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class