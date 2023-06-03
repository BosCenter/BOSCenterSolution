
Public Class BOS_ProductServiceMaster_SA
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ddlType.SelectedIndex = 0
                ddlType_SelectedIndexChanged(sender, e)
                lblSessionFlag.Text = 0


                ddl_SA_CommsissionType.SelectedIndex = 0
                ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)


                Bind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim str As String

    Public Sub Bind()
        Try
            str = "select RID as SrNo,* from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA  order by rid desc "
            ds = GV.FL.OpenDsWithSelectQuery(str)
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                GV.FL.showSerialnoOnGridView(GridView1, 1)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Dim statecode As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim V_SA_CommissionType, V_SA_Commission, VSlabApplicable As String
            V_SA_CommissionType = ""
            V_SA_Commission = "0"

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
            lblError.Text = ""
            lblError.CssClass = ""
            Dim Vtitle, VStatus, VContainCategory, Vtype, VServiceType, VServiceCharge As String
            If GV.parseString(txtTitle.Text) = "" Then
                lblError.Text = "Please Enter Title."
                lblError.CssClass = "errorlabels"
                txtTitle.Focus()
                Exit Sub
            Else
                Vtitle = GV.parseString(txtTitle.Text.Trim)
            End If
            If ddlType.SelectedIndex = 0 Then
                lblError.Text = "Please Select Type."
                lblError.CssClass = "errorlabels"
                ddlType.Focus()
                Exit Sub
            Else
                Vtype = GV.parseString(ddlType.SelectedValue.Trim)
            End If
            If Vtype.Trim.ToUpper = "API" Then
                If ddlContainCategory.SelectedIndex = 0 Then
                    lblError.Text = "Please Select Contain Category."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    VContainCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
                End If
                If ddlServiceCharge.SelectedIndex = 0 Then
                    VServiceType = GV.parseString(ddlServiceCharge.SelectedValue.Trim)
                    VServiceCharge = "0"
                Else
                    VServiceType = GV.parseString(ddlServiceCharge.SelectedValue.Trim)
                    If txtServiceCharge.Text = "" Then
                        lblError.Text = "Please Enter Service Charge."
                        lblError.CssClass = "errorlabels"
                        txtServiceCharge.Focus()
                        Exit Sub
                    Else
                        VServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                    End If
                End If


                If VContainCategory.Trim.ToUpper = "NO" Then



                    If ddl_SA_CommsissionType.SelectedIndex = 0 Then
                        V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
                        V_SA_Commission = "0"
                    Else
                        V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
                        If txt_SA_Commission.Text = "" Then
                            lblError.Text = "Please Enter Customer Commission."
                            lblError.CssClass = "errorlabels"
                            txt_SA_Commission.Focus()
                            Exit Sub
                        Else
                            V_SA_Commission = GV.parseString(txt_SA_Commission.Text.Trim)
                        End If
                    End If

                Else
                    V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
                    V_SA_Commission = "0"
                End If
            Else

                VContainCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
                VServiceType = GV.parseString(ddlServiceCharge.SelectedValue.Trim)
                V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
                V_SA_Commission = "0"
                VServiceCharge = "0"

            End If



            VStatus = GV.parseString(ddlStatus.SelectedValue.Trim)
            VSlabApplicable = ddlSlabApplicable.SelectedValue
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where Title='" & Vtitle & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " Title Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA (SA_CommissionType,SA_Commission,SlabApplicable,ServiceType,ServiceCharge,ContainCategory,ProductType,Title,ActiveStatus,UpdatedBy,UpdatedOn) values('" & V_SA_CommissionType & "'," & V_SA_Commission & ",'" & VSlabApplicable & "','" & VServiceType & "'," & VServiceCharge & ",'" & VContainCategory & "','" & Vtype & "','" & Vtitle & "','" & VStatus & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        Clear()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                        txtTitle.Focus()
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where   (Title='" & Vtitle & "' and not Title='" & GV.parseString(lblUpadate.Text) & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    ' Session("EditFlag") = 0
                    QryStr = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA set SlabApplicable='" & VSlabApplicable & "', ServiceType='" & VServiceType & "',ServiceCharge=" & VServiceCharge & ",SA_CommissionType='" & V_SA_CommissionType & "',SA_Commission=" & V_SA_Commission & ",ContainCategory='" & VContainCategory & "',ProductType='" & Vtype & "', Title='" & Vtitle & "',ActiveStatus='" & VStatus & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"

                    'QryStr = QryStr & "  delete from BosCenter_DB.dbo.BOS_OperatorWiseCommission_Agents  where  APIName='" & Vtitle.Trim & "' "



                    If GV.FL.DMLQueriesBulk(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Bind()
                        Clear()
                        ddlType_SelectedIndexChanged(sender, e)

                        ddlServiceCharge_SelectedIndexChanged(sender, e)
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

    Private Sub Clear()
        Try

            txtTitle.Text = ""

            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""
            ddlType.SelectedIndex = 0
            ddlServiceCharge.SelectedIndex = 0



            ddl_SA_CommsissionType.SelectedIndex = 0
            ddl_SA_CommsissionType.Enabled = False
            ddl_SA_CommsissionType.CssClass = "form-control"
            txt_SA_Commission.Text = "0"
            txt_SA_Commission.Enabled = False
            txt_SA_Commission.CssClass = "form-control"

            ddlContainCategory.SelectedIndex = 0
            ddlStatus.SelectedIndex = 0

            txtServiceCharge.Text = "0"

            ddlContainCategory.Enabled = False
            ddlContainCategory.CssClass = "form-control"

            ddlServiceCharge.Enabled = False
            ddlServiceCharge.CssClass = "form-control"
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"
            ' lblStarCommissionType.Visible = False
            lblStarServiceType.Visible = False
            lblStarContain.Visible = False
            ddlSlabApplicable.SelectedIndex = 0
            ddlSlabApplicable.Enabled = False


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)

            ddlServiceCharge_SelectedIndexChanged(sender, e)

            Bind()

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
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If


            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            txtTitle.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) = "&nbsp;" Then
                ddlType.SelectedIndex = 0
            Else
                ddlType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            End If

            ddlType_SelectedIndexChanged(sender, e)
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) = "&nbsp;" Then
                ddlContainCategory.SelectedIndex = 0
            Else
                ddlContainCategory.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            End If
            ddlContainCategory_SelectedIndexChanged(sender, e)

            ddlSlabApplicable.SelectedValue = GridView1.Rows(gvrow.RowIndex).Cells(5).Text

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text) = "&nbsp;" Then
                ddl_SA_CommsissionType.SelectedIndex = 0
            Else
                ddl_SA_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            End If

            ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)
            txt_SA_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)


            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "&nbsp;" Then
                ddlServiceCharge.SelectedIndex = 0
            Else
                ddlServiceCharge.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
            End If

            ddlServiceCharge_SelectedIndexChanged(sender, e)
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text) = "&nbsp;" Then
                txtServiceCharge.Text = 0
            Else
                txtServiceCharge.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            End If

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) = "&nbsp;" Then
                ddlStatus.SelectedIndex = 0
            Else
                ddlStatus.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
            End If




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
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
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
                QryStr = "delete from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where RID=" & lblRID.Text & ""
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
                GV.ExportToExcel_New(GridView1, Response, "", "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub ddlContainCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContainCategory.SelectedIndexChanged
        Try

            ddl_SA_CommsissionType.Enabled = False
            ddl_SA_CommsissionType.CssClass = "form-control"
            txt_SA_Commission.Enabled = False
            txt_SA_Commission.CssClass = "form-control"

            ddlServiceCharge.Enabled = False
            ddlServiceCharge.CssClass = "form-control"
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"
            ' lblStarCommissionType.Visible = False
            lblStarServiceType.Visible = False

            If Not ddlContainCategory.SelectedIndex = 0 Then
                If ddlContainCategory.SelectedValue.Trim.ToUpper = "Yes".Trim.ToUpper Then
                    ddlServiceCharge_SelectedIndexChanged(sender, e)



                    ddl_SA_CommsissionType.Enabled = False
                    ddl_SA_CommsissionType.CssClass = "form-control"
                    txt_SA_Commission.Enabled = False
                    txt_SA_Commission.CssClass = "form-control"


                    'Customer


                    lblStarServiceType.Visible = True

                    ddlServiceCharge.Enabled = True
                    ddlServiceCharge.CssClass = "form-control"
                    'txtServiceCharge.Enabled = True
                    'txtServiceCharge.CssClass = "form-control"
                ElseIf ddlContainCategory.SelectedValue.Trim.ToUpper = "No".Trim.ToUpper Then

                    ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)
                    ddl_SA_CommsissionType.Enabled = True
                    ddl_SA_CommsissionType.CssClass = "form-control"


                    ddlServiceCharge_SelectedIndexChanged(sender, e)
                    '  lblStarCommissionType.Visible = True
                    lblStarContain.Visible = True
                    lblStarServiceType.Visible = True

                    'txtCommssion.Enabled = True
                    'txtCommssion.CssClass = "form-control"
                    ddlServiceCharge.Enabled = True
                    ddlServiceCharge.CssClass = "form-control"
                    'txtServiceCharge.Enabled = True
                    'txtServiceCharge.CssClass = "form-control"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try
            ddlServiceCharge.SelectedIndex = 0
            ddlServiceCharge_SelectedIndexChanged(sender, e)

            ddlContainCategory.SelectedIndex = 0




            ddl_SA_CommsissionType.SelectedIndex = 0
            ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_SA_CommsissionType.Enabled = False
            ddl_SA_CommsissionType.CssClass = "form-control"
            txt_SA_Commission.Enabled = False
            txt_SA_Commission.CssClass = "form-control"


            ddlContainCategory.Enabled = False
            ddlContainCategory.CssClass = "form-control"

            ddlServiceCharge.Enabled = False
            ddlServiceCharge.CssClass = "form-control"
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"
            'lblStarCommissionType.Visible = False
            lblStarServiceType.Visible = False
            lblStarContain.Visible = False

            ddlSlabApplicable.Enabled = False



            If Not ddlType.SelectedIndex = 0 Then
                If ddlType.SelectedValue.Trim.ToUpper = "Service".Trim.ToUpper Then
                    'lblStarCommissionType.Visible = False
                    lblStarServiceType.Visible = False
                    lblStarContain.Visible = False

                    ddl_SA_CommsissionType.Enabled = False
                    ddl_SA_CommsissionType.CssClass = "form-control"
                    txt_SA_Commission.Enabled = False
                    txt_SA_Commission.CssClass = "form-control"


                    ddlServiceCharge.Enabled = False
                    ddlServiceCharge.CssClass = "form-control"
                    txtServiceCharge.Enabled = False
                    txtServiceCharge.CssClass = "form-control"
                    ddlContainCategory.Enabled = False
                    ddlContainCategory.CssClass = "form-control"
                ElseIf ddlType.SelectedValue.Trim.ToUpper = "API".Trim.ToUpper Then

                    ddlServiceCharge_SelectedIndexChanged(sender, e)
                    ' lblStarCommissionType.Visible = False
                    lblStarServiceType.Visible = False
                    lblStarContain.Visible = True


                    ddl_SA_CommsissionType.Enabled = False
                    ddl_SA_CommsissionType.CssClass = "form-control"

                    ddlSlabApplicable.Enabled = True

                    ddlContainCategory.Enabled = True
                    ddlContainCategory.CssClass = "form-control"
                    'txtCommssion.Enabled = False
                    'txtCommssion.CssClass = "form-control"
                    ddlServiceCharge.Enabled = False
                    ddlServiceCharge.CssClass = "form-control"
                    'txtServiceCharge.Enabled = False
                    'txtServiceCharge.CssClass = "form-control"
                End If
            Else

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlServiceCharge_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServiceCharge.SelectedIndexChanged
        Try
            txtServiceCharge.Enabled = True
            txtServiceCharge.CssClass = "form-control"
            If ddlServiceCharge.SelectedIndex = 0 Then
                txtServiceCharge.Text = "0"
                txtServiceCharge.Enabled = False
                txtServiceCharge.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub ddlCommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommsissionType.SelectedIndexChanged
    '    Try
    '        txtCommssion.Enabled = True
    '        txtCommssion.CssClass = "form-control"
    '        If ddlCommsissionType.SelectedIndex = 0 Then
    '            txtCommssion.Text = "0"
    '            txtCommssion.Enabled = False
    '            txtCommssion.CssClass = "form-control"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub


    Private Sub ddl_SA_CommsissionType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_SA_CommsissionType.SelectedIndexChanged
        Try
            txt_SA_Commission.Enabled = True
            txt_SA_Commission.CssClass = "form-control"
            If ddl_SA_CommsissionType.SelectedIndex = 0 Then
                txt_SA_Commission.Text = "0"
                txt_SA_Commission.Enabled = False
                txt_SA_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class