Public Class BOS_ProductService_VsAdmin1
    Inherits System.Web.UI.Page


    Dim GV As New GlobalVariable("ADMIN")
    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                GV.FL.AddInDropDownListAll(ddl_Admin, "CompanyCode +':'+ CompanyName", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration order by RID Desc")
                If ddl_Admin.Items.Count > 0 Then
                    ddl_Admin.Items.Insert(0, ":: Select Admin ::")
                Else
                    ddl_Admin.Items.Add(":: Select Admin ::")
                End If

                'ddlTitle.SelectedIndex = 0
                'ddlTitle_SelectedIndexChanged(sender, e)
                'lblSessionFlag.Text = 0

                'ddl_Admin_CommsissionType.SelectedIndex = 0
                'ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)

                'ddlServiceCharge.SelectedIndex = 0
                'ddlServiceCharge_SelectedIndexChanged(sender, e)

                'Bind()

                GV.FL.AddInDropDownListDistinct(ddlTitle, "Title", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where ProductType='Service'")
                If ddlTitle.Items.Count > 0 Then
                    ddlTitle.Items.Insert(0, ":: Select Title ::")
                Else
                    ddlTitle.Items.Add(":: Select Title ::")
                End If
                ddlTitle.SelectedIndex = 0
                ddlTitle_SelectedIndexChanged(sender, e)

                ddlType.SelectedIndex = 0
                ddlType_SelectedIndexChanged(sender, e)
                lblSessionFlag.Text = 0

                ddl_Admin_CommsissionType.SelectedIndex = 0
                ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)

                ddlServiceCharge.SelectedIndex = 0
                ddlServiceCharge_SelectedIndexChanged(sender, e)

                Bind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim str As String = ""
    Public Sub Bind()
        Try
            If Not ddl_Admin.SelectedIndex = 0 Then
                Dim dd() As String = ddl_Admin.SelectedValue.Split(":")
                str = "select RID as SrNo,(select CompanyCode+':'+CompanyName from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration CL where CL.CompanyCode=PSA.AdminID) as [AdminIDX],* from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA PSA where AdminID='" & dd(0).Trim & "' order by rid desc"
                ds = GV.FL.OpenDsWithSelectQuery(str)
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()
                If GridView1.Rows.Count > 0 Then
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                End If
            Else

                GridView1.DataSource = Nothing
                GridView1.DataBind()

                'str = "select RID as SrNo,(select CompanyCode+':'+CompanyName from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration CL where CL.CompanyCode=PSA.AdminID) as [AdminIDX],* from BosCenter_DB.dbo.BOS_ProductServiceVsAdmin_SA PSA  order by rid desc "
                'ds = GV.FL.OpenDsWithSelectQuery(str)
                'GridView1.DataSource = ds.Tables(0)
                'GridView1.DataBind()
                'If GridView1.Rows.Count > 0 Then
                '    GV.FL.showSerialnoOnGridView(GridView1, 1)
                'End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Dim statecode As String = ""
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim V_Admin_CommissionType, V_Admin_Commission, VSlabApplicable As String
            V_Admin_CommissionType = ""
            V_Admin_Commission = "0"

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
            Dim VAdminID, Vtitle, VStatus, VContainCategory, Vtype, VServiceType, VServiceCharge As String
            VServiceCharge = "0"

            If ddl_Admin.SelectedIndex = 0 Then
                lblError.Text = "Please Select Admin."
                lblError.CssClass = "errorlabels"
                ddl_Admin.Focus()
                Exit Sub
            Else
                Dim dd() As String = GV.parseString(ddl_Admin.SelectedValue.Trim).Split(":")
                VAdminID = dd(0).Trim
            End If

            If ddlTitle.SelectedIndex = 0 Then
                lblError.Text = "Please Select Title."
                lblError.CssClass = "errorlabels"
                ddlTitle.Focus()
                Exit Sub
            Else
                Vtitle = GV.parseString(ddlTitle.SelectedValue.Trim)
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



                If VContainCategory.Trim.ToUpper = "NO" Then


                    If ddl_Admin_CommsissionType.SelectedIndex = 0 Then
                        V_Admin_CommissionType = GV.parseString(ddl_Admin_CommsissionType.SelectedValue.Trim)
                        V_Admin_Commission = "0"
                    Else
                        V_Admin_CommissionType = GV.parseString(ddl_Admin_CommsissionType.SelectedValue.Trim)
                        If txt_Admin_Commission.Text = "" Then
                            lblError.Text = "Please Enter Customer Commission."
                            lblError.CssClass = "errorlabels"
                            txt_Admin_Commission.Focus()
                            Exit Sub
                        Else
                            V_Admin_Commission = GV.parseString(txt_Admin_Commission.Text.Trim)
                        End If
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



                Else

                    V_Admin_CommissionType = GV.parseString(ddl_Admin_CommsissionType.SelectedValue.Trim)
                    V_Admin_Commission = "0"
                End If
            Else
                VContainCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
                VServiceType = GV.parseString(ddlServiceCharge.SelectedValue.Trim)
                V_Admin_CommissionType = GV.parseString(ddl_Admin_CommsissionType.SelectedValue.Trim)
                V_Admin_Commission = "0"
                VServiceCharge = "0"

            End If



            VStatus = GV.parseString(ddlStatus.SelectedValue.Trim)
            VSlabApplicable = ddlSlabApplicable.SelectedValue
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where Title='" & Vtitle & "' and AdminID='" & VAdminID & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " Title Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA (AdminID,Admin_CommissionType,Admin_Commission,SlabApplicable,ServiceType,ServiceCharge,ContainCategory,ProductType,Title,ActiveStatus,UpdatedBy,UpdatedOn) values('" & VAdminID & "','" & V_Admin_CommissionType & "'," & V_Admin_Commission & ",'" & VSlabApplicable & "','" & VServiceType & "'," & VServiceCharge & ",'" & VContainCategory & "','" & Vtype & "','" & Vtitle & "','" & VStatus & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        Clear()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                        ddlTitle.Focus()
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where  AdminID='" & VAdminID & "' and   (Title='" & Vtitle & "' and not Title='" & GV.parseString(lblUpadate.Text) & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    ' Session("EditFlag") = 0
                    QryStr = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA set SlabApplicable='" & VSlabApplicable & "', ServiceType='" & VServiceType & "',ServiceCharge=" & VServiceCharge & ",Admin_CommissionType='" & V_Admin_CommissionType & "',Admin_Commission=" & V_Admin_Commission & ",ContainCategory='" & VContainCategory & "',ProductType='" & Vtype & "', Title='" & Vtitle & "',ActiveStatus='" & VStatus & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"

                    'QryStr = QryStr & "  delete from BosCenter_DB.dbo.BOS_OperatorWiseCommission_Agents  where  APIName='" & Vtitle.Trim & "' "



                    If GV.FL.DMLQueriesBulk(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Bind()
                        Clear()
                        ddlType_SelectedIndexChanged(sender, e)

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
            ddl_Admin.Enabled = True
            ddlTitle.Enabled = True

            ddlTitle.SelectedIndex = 0

            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""
            ddlType.SelectedIndex = 0



            ddl_Admin_CommsissionType.SelectedIndex = 0
            ddl_Admin_CommsissionType.Enabled = False
            ddl_Admin_CommsissionType.CssClass = "form-control"
            txt_Admin_Commission.Text = "0"
            txt_Admin_Commission.Enabled = False
            txt_Admin_Commission.CssClass = "form-control"

            ddlServiceCharge.SelectedIndex = 0
            ddlServiceCharge.CssClass = "form-control"

            txtServiceCharge.Text = "0"
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"


            ddlContainCategory.SelectedIndex = 0
            ddlStatus.SelectedIndex = 0



            ddlContainCategory.Enabled = False
            ddlContainCategory.CssClass = "form-control"

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
            ddl_Admin_SelectedIndexChanged(sender, e)
            ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
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
            ddlTitle.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

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
                ddl_Admin_CommsissionType.SelectedIndex = 0
            Else
                ddl_Admin_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            End If

            ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Admin_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)


            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "&nbsp;" Then
                ddlServiceCharge.SelectedIndex = 0
            Else
                ddlServiceCharge.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
            End If

            ddlServiceCharge_SelectedIndexChanged(sender, e)
            txtServiceCharge.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)


            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) = "&nbsp;" Then
                ddlStatus.SelectedIndex = 0
            Else
                ddlStatus.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
            End If

            ddl_Admin.Enabled = False
            ddlTitle.Enabled = False


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
                QryStr = "delete from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where RID=" & lblRID.Text & ""
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
                GV.ExportToExcel_New(GridView1, Response, "", "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub ddlContainCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContainCategory.SelectedIndexChanged
        Try
            ddl_Admin_CommsissionType.Enabled = False
            ddl_Admin_CommsissionType.CssClass = "form-control"
            txt_Admin_Commission.Enabled = False
            txt_Admin_Commission.CssClass = "form-control"


            lblStarServiceType.Visible = False
            If Not ddlContainCategory.SelectedIndex = 0 Then
                If ddlContainCategory.SelectedValue.Trim.ToUpper = "Yes".Trim.ToUpper Then

                    ddl_Admin_CommsissionType.Enabled = False
                    ddl_Admin_CommsissionType.CssClass = "form-control"
                    txt_Admin_Commission.Enabled = False
                    txt_Admin_Commission.CssClass = "form-control"


                    'Customer


                    lblStarServiceType.Visible = True

                ElseIf ddlContainCategory.SelectedValue.Trim.ToUpper = "No".Trim.ToUpper Then

                    ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
                    ddl_Admin_CommsissionType.Enabled = True
                    ddl_Admin_CommsissionType.CssClass = "form-control"



                    lblStarContain.Visible = True
                    lblStarServiceType.Visible = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try

            ddlContainCategory.SelectedIndex = 0


            ddl_Admin_CommsissionType.SelectedIndex = 0
            ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Admin_CommsissionType.Enabled = False
            ddl_Admin_CommsissionType.CssClass = "form-control"
            txt_Admin_Commission.Enabled = False
            txt_Admin_Commission.CssClass = "form-control"


            ddlContainCategory.Enabled = False
            ddlContainCategory.CssClass = "form-control"

            'lblStarCommissionType.Visible = False
            lblStarServiceType.Visible = False
            lblStarContain.Visible = False

            ddlSlabApplicable.Enabled = False



            If Not ddlType.SelectedIndex = 0 Then
                If ddlType.SelectedValue.Trim.ToUpper = "Service".Trim.ToUpper Then
                    ''lblStarCommissionType.Visible = False
                    'lblStarServiceType.Visible = False
                    'lblStarContain.Visible = False

                    'ddl_Admin_CommsissionType.Enabled = False
                    'ddl_Admin_CommsissionType.CssClass = "form-control"
                    'txt_Admin_Commission.Enabled = False
                    'txt_Admin_Commission.CssClass = "form-control"


                    'ddlContainCategory.Enabled = False
                    'ddlContainCategory.CssClass = "form-control"
                    lblStarServiceType.Visible = False
                    lblStarContain.Visible = True


                    ddl_Admin_CommsissionType.Enabled = False
                    ddl_Admin_CommsissionType.CssClass = "form-control"

                    ddlContainCategory.CssClass = "form-control"

                ElseIf ddlType.SelectedValue.Trim.ToUpper = "API".Trim.ToUpper Then
                    ' lblStarCommissionType.Visible = False
                    lblStarServiceType.Visible = False
                    lblStarContain.Visible = True


                    ddl_Admin_CommsissionType.Enabled = False
                    ddl_Admin_CommsissionType.CssClass = "form-control"

                    ddlContainCategory.CssClass = "form-control"
                    'txtCommssion.Enabled = False
                    'txtCommssion.CssClass = "form-control"

                    'txtServiceCharge.Enabled = False
                    'txtServiceCharge.CssClass = "form-control"
                End If
            Else

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddl_Customer_CommsissionType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Admin_CommsissionType.SelectedIndexChanged
        Try
            txt_Admin_Commission.Enabled = True
            txt_Admin_Commission.CssClass = "form-control"
            If ddl_Admin_CommsissionType.SelectedIndex = 0 Then
                txt_Admin_Commission.Text = "0"
                txt_Admin_Commission.Enabled = False
                txt_Admin_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlServiceCharge_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlServiceCharge.SelectedIndexChanged
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

    Protected Sub ddlTitle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTitle.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            Dim Title, ProductType, ContainCategory, SlabApplicable As String

            If Not ddlTitle.SelectedIndex = 0 Then

                Dim dd As String = GV.FL.AddInVar("Title+'*'+ProductType+'*'+ContainCategory+'*'+SlabApplicable", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where Title='" & ddlTitle.SelectedValue & "'")


                If Not dd.Trim = "" Then
                    Dim dd1() As String = dd.Split("*")
                    Title = dd1(0)
                    ProductType = dd1(1)
                    ContainCategory = dd1(2)
                    SlabApplicable = dd1(3)

                    ddlType.SelectedValue = ProductType
                    ddlType_SelectedIndexChanged(sender, e)

                    ddlContainCategory.SelectedValue = ContainCategory
                    ddlContainCategory_SelectedIndexChanged(sender, e)

                    ddlSlabApplicable.SelectedValue = SlabApplicable

                    ddlType.Enabled = False
                    ddlContainCategory.Enabled = False
                    ddlSlabApplicable.Enabled = False

                End If

            Else
                ddlType.SelectedIndex = 0
                ddlType_SelectedIndexChanged(sender, e)

                ddlContainCategory.SelectedIndex = 0
                ddlContainCategory_SelectedIndexChanged(sender, e)

                ddlType.Enabled = False
                ddlContainCategory.Enabled = False
                ddlSlabApplicable.Enabled = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Admin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Admin.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            Bind()
        Catch ex As Exception

        End Try
    End Sub

End Class