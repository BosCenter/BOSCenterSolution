Public Class BOS_API_Vs_Category_SA
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("ADMIN")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                GV.FL.AddInDropDownListDistinct(ddlProductService, "Title", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where ContainCategory='Yes' and ProductType='API'")
                If ddlProductService.Items.Count > 0 Then
                    ddlProductService.Items.Insert(0, "Select Service")
                Else
                    ddlProductService.Items.Add("Select Service")
                End If
                lblSessionFlag.Text = "0"
                Bind()
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
    Public Sub Bind()
        Try
            ds = GV.FL.OpenDsWithSelectQuery("select RID as SrNo,* from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA order by RID desc  ")
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            GV.FL.showSerialnoOnGridView(GridView1, 0)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            ddlProductService.SelectedIndex = 0
            txtCategory.Text = ""
            btnSave.Text = "Save"
            lblError.Text = ""
            lblSessionFlag.Text = "0"
            btnSave.Enabled = True
            lblError.CssClass = ""
            btnDelete.Enabled = False
            lblUpadate.Text = ""
        Catch ex As Exception
        End Try
    End Sub
    Dim QryStr As String = ""
    Dim VProduct, VProblem, VUpdatedBy, VUpdatedOn As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ApiResult As String = ""
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            If ddlProductService.SelectedIndex = 0 Then
                lblError.Text = "Please select Product."
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VProduct = GV.parseString(ddlProductService.SelectedItem.Text)
            End If

            If Not txtCategory.Text.Trim = "" Then
                VProblem = GV.parseString(txtCategory.Text.Trim)

            Else
                lblError.Text = "Please Enter Problem."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount("" & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA where ProductService='" & VProduct & "' and Category='" & VProblem & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else

                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA (ProductService,Category,UpdatedBy,UpdatedOn) values('" & VProduct & "','" & VProblem & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        'Clear()
                        txtCategory.Text = ""
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                If GV.FL.RecCount("" & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA where (Category='" & VProblem & "' and not Category ='" & lblUpadate.Text & "') and (ProductService='" & VProduct & "' and not ProductService ='" & lblproduct.Text & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    QryStr = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA set ProductService='" & VProduct & "', Category='" & VProblem & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"

                    'QryStr = QryStr & "  delete from BosCenter_DB.dbo.BOS_OperatorWiseCommission_Agents  where  APIName='" & VProduct & "'   and Category='" & VProblem & "' "


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
            lblError.Text = ApiResult
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
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If


            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            ddlProductService.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            txtCategory.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            lblproduct.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            'Session("Editflag") = 1
            lblSessionFlag.Text = 1
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.Text = ""
            lblError.CssClass = ""

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
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = " Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            btnok.Visible = True
            ModalPopupExtender1.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then
                QryStr = "delete from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA where RID=" & lblRID.Text & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    Bind()
                    Clear()

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

    Protected Sub btnDelete_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
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

    Protected Sub ImagebtnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToExcel_New(GridView1, Response, "", "APIVSCategory", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "APIVSCategory", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "APIVSCategory", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


End Class