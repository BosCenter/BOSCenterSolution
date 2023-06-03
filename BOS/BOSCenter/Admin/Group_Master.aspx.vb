
Public Class Group_Master
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
                lblSessionFlag.Text = 0
            End If
        Catch ex As Exception
        End Try
    End Sub


    Dim VGroupName, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet


    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
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
                                Button2.Visible = False
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
                                Button2.Visible = False
                                ModalPopupExtender1.Show()
                                Exit Sub
                            End If
                        Next

                    End If
                End If
            End If
            If Not txtGroupName.Text.Trim = "" Then
                VGroupName = GV.parseString(txtGroupName.Text.Trim)
            Else
                VGroupName = ""
            End If

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = Now.Date

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master where Group_Name='" & VGroupName & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master (Group_Name,UpdatedBy,UpdatedOn) values( '" & VGroupName & "','" & VUpdatedBy & "','" & VUpdatedOn & "' )"
                    If GV.FL.DMLQueriesBulk(QryStr) = True Then
                        ClearAll()
                        Bind()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Record insertion failed. " & QryStr
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master where Group_Name='" & VGroupName & "' and not Group_Name='" & GV.parseString(lblUpadate.Text) & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else

                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master set Group_Name='" & VGroupName & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where RID=" & lblRID.Text.Trim & ";"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        ClearAll()
                        Bind()
                        lblError.Text = "Record Updated Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Record Updation failed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub Bind()
        Try
            Dim Filter As String = ""
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Super Admin" Then
            Else
                Filter = " where not Group_Name='Super Admin' "
            End If
            DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master " & Filter & " order by RID desc  ")
            GridView1.DataSource = DS.Tables(0)
            GridView1.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearAll()
        Try
            txtGroupName.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""
            lblRID.Text = ""
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            GridView1.PageIndex = 0
            If GridView1.Visible = True Then
                Bind()
            End If

            txtGroupName.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
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
                            Button2.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_type='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Deleted.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                Button2.Visible = False
                ModalPopupExtender1.Show()
            Else
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = " Are you sure you want to delete ?"
                btnCancel.Text = "Cancel"
                Button2.Visible = True
                ModalPopupExtender1.Show()
            End If




            'lblDialogMsg.CssClass = ""
            'lblDialogMsg.Text = " Are you sure you want to delete ?"
            'Button2.Visible = True
            'btnCancel.Text = "Cancel"
            'ModalPopupExtender1.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then

                QryStr = ("delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master where RID=" & lblRID.Text & "")
                If GV.FL.DMLQueries(QryStr) = True Then
                    ClearAll()
                    Bind()
                    lblDialogMsg.Text = "Record Deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "Sorry !! Record deletion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                Button2.Visible = False
                ModalPopupExtender1.Show()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = " Are you sure you want to delete ?"
            Button2.Visible = True
            btnCancel.Text = "Cancel"
            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

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
                            Button2.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_type='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Updated.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                Button2.Visible = False
                ModalPopupExtender1.Show()
            Else
                txtGroupName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
                lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
                lblError.Text = ""
                lblError.CssClass = ""
                lblSessionFlag.Text = 1
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtGroupName.Focus()
            End If



            'lblError.Text = ""
            'lblError.CssClass = ""
            'lblSessionFlag.Text = 1
            'btnSave.Text = "Update"
            'btnDelete.Enabled = True
            'txtGroupName.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            ClearAll()
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




End Class