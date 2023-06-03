
Public Class Admin_DyanamicServices
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VCountryName, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Rights_Dashboard where ServiceName='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "' and CanshowServices='1';") Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Deleted.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                btnok.Visible = False
                ModalPopupExtender1.Show()
            Else
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = " Are you sure you want to delete ?"
                btnCancel.Text = "Cancel"
                btnok.Visible = True
                ModalPopupExtender1.Show()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then
                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Dashboard where RID=" & lblRID.Text & ";"
                If GV.FL.DMLQueries(QryStr) = True Then
                    Clear()
                    Bind()
                    lblDialogMsg.Text = "Record Deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnok.Visible = False
                ModalPopupExtender1.Show()
            End If


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


    Dim ServiceName, IconName, PostbackUrl, OrderNo As String
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

            If Not txtTitle.Text.Trim = "" Then
                ServiceName = GV.parseString(txtTitle.Text.Trim)
            Else
                ServiceName = ""
            End If

            If Not txtIcon.Text.Trim = "" Then
                IconName = GV.parseString(txtIcon.Text.Trim)
            Else
                IconName = ""
            End If

            If Not txtPostBackUrl.Text.Trim = "" Then
                PostbackUrl = GV.parseString(txtPostBackUrl.Text.Trim)
            Else
                PostbackUrl = ""
            End If

            If Not txtOrderNo.Text.Trim = "" Then
                OrderNo = GV.parseString(txtOrderNo.Text.Trim)
            Else
                OrderNo = ""
            End If

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Dashboard where ServiceName='" & VCountryName & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Dashboard (ServiceName,IconName,PostbackUrl,OrderNo,UpdatedBy,UpdatedOn) values( '" & ServiceName & "','" & IconName & "','" & PostbackUrl & "','" & OrderNo & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        'Clear()
                        Bind()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                        txtTitle.Text = ""
                        txtIcon.Text = ""
                        txtPostBackUrl.Text = ""
                        txtOrderNo.Text = ""
                        txtTitle.Focus()

                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Dashboard where (ServiceName='" & VCountryName & "' and not ServiceName='" & lblUpadate.Text & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    ' Session("EditFlag") = 0

                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Dashboard set IconName='" & IconName & "',PostbackUrl='" & PostbackUrl & "',OrderNo='" & OrderNo & "', ServiceName='" & ServiceName & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Clear()
                        Bind()
                        lblError.Text = "Record Updated Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub Bind()
        Try
            Dim STR As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Dashboard order by OrderNo asc  "
            DS = GV.FL.OpenDsWithSelectQuery(STR)
            GridView1.DataSource = DS.Tables(0)
            GridView1.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            VCountryName = ""
            VUpdatedBy = ""
            VUpdatedOn = ""
            txtIcon.Text = ""
            txtOrderNo.Text = ""
            txtPostBackUrl.Text = ""
            'Session("EditFlag") = 0
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblError.CssClass = ""
            txtTitle.Text = ""
            txtTitle.Focus()
            lblUpadate.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Bind()
                lblSessionFlag.Text = 0
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

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
            'Exit Sub

            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DyanamicServices_Rights_Dashboard where ServiceName='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "' and CanshowServices='1'") Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Updated.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                btnok.Visible = False
                ModalPopupExtender1.Show()
            Else
                txtTitle.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
                lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
                txtIcon.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
                txtPostBackUrl.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
                txtOrderNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
                ' Session("EditFlag") = 1
                lblSessionFlag.Text = 1
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                lblError.CssClass = ""
                lblError.Text = ""
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
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

            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = " Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            btnok.Visible = True
            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception

            End Try
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