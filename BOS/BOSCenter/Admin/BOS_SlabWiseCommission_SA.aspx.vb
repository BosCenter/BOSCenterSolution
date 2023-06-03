
Public Class BOS_SlabWiseCommission_SA
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'InsertOperator()
                'Exit Sub
                GV.FL.AddInDropDownListDistinct(ddlAPIName, "Title", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where SlabApplicable='With Slab' and ProductType='API' ")
                If ddlAPIName.Items.Count > 0 Then
                    ddlAPIName.Items.Insert(0, "Select API")
                Else
                    ddlAPIName.Items.Add("Select API")
                End If
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
            Dim API As String = ""
            Dim Category As String = ""
            If Not ddlAPIName.SelectedIndex = 0 Then
                API = " where APIName ='" & GV.parseString(ddlAPIName.SelectedValue.Trim) & "' "
            End If

            str = "select RID as SrNo,* from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA " & API & " " & Category & "  order by rid desc "
            ds = GV.FL.OpenDsWithSelectQuery(str)
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                GV.FL.showSerialnoOnGridView(GridView1, 1)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Dim statecode As String = ""
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim VAPIName, VFromAmount, VToAmount As String
            Dim V_SA_CommissionType, V_SA_Commission As String

            V_SA_CommissionType = ""
            V_SA_Commission = "0"


            If ddlAPIName.SelectedIndex = 0 Then
                lblError.Text = "Please Select API Name."
                lblError.CssClass = "errorlabels"
                ddlAPIName.Focus()
                Exit Sub
            Else
                VAPIName = GV.parseString(ddlAPIName.SelectedValue.Trim)
            End If


            If txtFromAmount.Text = "" Then
                lblError.Text = "Please Enter From Amount."
                lblError.CssClass = "errorlabels"
                txtFromAmount.Focus()
                Exit Sub
            Else
                VFromAmount = GV.parseString(txtFromAmount.Text.Trim)
            End If

            If txtToAmount.Text = "" Then
                lblError.Text = "Please Enter From Amount."
                lblError.CssClass = "errorlabels"
                txtToAmount.Focus()
                Exit Sub
            Else
                VToAmount = GV.parseString(txtToAmount.Text.Trim)
            End If

            If ddl_SA_CommsissionType.SelectedIndex = 0 Then
                V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
            Else
                If txt_SA_Commission.Text = "" Then
                    lblError.Text = "Please Enter SA Commission."
                    lblError.CssClass = "errorlabels"
                    txt_SA_Commission.Focus()
                    Exit Sub
                Else
                    V_SA_Commission = GV.parseString(txt_SA_Commission.Text.Trim)
                End If
                V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
            End If
            If GV.parseString(V_SA_Commission.Trim) = "" Then
                V_SA_Commission = "0"
            End If


            '/////////////////////////

            '////////////////////////////

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then
                'VCode
                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA where APIName='" & VAPIName & "' and FromAmount='" & VFromAmount & "' and ToAmount='" & VToAmount & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " API With Same From And To Amount Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA (SA_CommissionType,SA_Commission,APIName,FromAmount,ToAmount,UpdatedBy,UpdatedOn) values('" & V_SA_CommissionType & "'," & V_SA_Commission & ",'" & VAPIName & "','" & VFromAmount & "','" & VToAmount & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        txtFromAmount.Text = ""
                        txtToAmount.Text = ""

                        txt_SA_Commission.Text = "0"


                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"

                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                QryStr = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA set FromAmount='" & VFromAmount & "',ToAmount='" & VToAmount & "',SA_CommissionType='" & V_SA_CommissionType & "',SA_Commission=" & V_SA_Commission & ", UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"


                If GV.FL.DMLQueriesBulk(QryStr) = True Then
                    lblSessionFlag.Text = 0
                    Bind()
                    Clear()

                    lblError.Text = "Record Updated Successfully."
                    lblError.CssClass = "Successlabels"
                Else
                    lblError.Text = "Sorry !! Process Can't be Completed."
                    lblError.CssClass = "errorlabels"
                End If
                ' End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            ddlAPIName.Enabled = True
            ddlAPIName.CssClass = "form-control"
            txtFromAmount.Text = ""
            txtToAmount.Text = ""
            txt_SA_Commission.Text = "0"
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""


            ddl_SA_CommsissionType.SelectedIndex = 0
            If ddl_SA_CommsissionType.SelectedIndex = 0 Then
                txt_SA_Commission.Text = "0"
                txt_SA_Commission.Enabled = False
                txt_SA_Commission.CssClass = "form-control"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddlAPIName.SelectedIndex = 0
            ddlProductService_SelectedIndexChanged(sender, e)
            Bind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try


            lblSessionFlag.Text = 1
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            ddlAPIName.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            ddlProductService_SelectedIndexChanged(sender, e)

            txtFromAmount.Text = CInt(GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text))
            txtToAmount.Text = CInt(GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text))



            ddl_SA_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)
            txt_SA_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)


            'Session("Editflag") = 1
            ddlAPIName.Enabled = False
            ddlAPIName.CssClass = "form-control"

            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.Text = ""
            lblError.CssClass = ""

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

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
                QryStr = "delete from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA where RID=" & lblRID.Text & ""
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
                GV.ExportToExcel_New(GridView1, Response, "", "SlabWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "SlabWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "SlabWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub ddlProductService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAPIName.SelectedIndexChanged
        Try

            If Not lblSessionFlag.Text = 1 Then
                Bind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_SA_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_SA_CommsissionType.SelectedIndexChanged
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