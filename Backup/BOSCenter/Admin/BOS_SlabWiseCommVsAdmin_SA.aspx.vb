
Public Class BOS_SlabWiseCommVsAdmin_SA
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'InsertOperator()
                'Exit Sub
                GV.FL.AddInDropDownListAll(ddl_Admin, "CompanyCode +':'+ CompanyName", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration order by RID Desc")
                If ddl_Admin.Items.Count > 0 Then
                    ddl_Admin.Items.Insert(0, ":: Select Admin ::")
                Else
                    ddl_Admin.Items.Add(":: Select Admin ::")
                End If
                ddl_Admin_SelectedIndexChanged(sender, e)

                GV.FL.AddInDropDownListDistinct(ddlAPIName, "Title", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where SlabApplicable='With Slab' and ProductType='API' ")
                If ddlAPIName.Items.Count > 0 Then
                    ddlAPIName.Items.Insert(0, "Select API")
                Else
                    ddlAPIName.Items.Add("Select API")
                End If
                ddlAPIName_SelectedIndexChanged(sender, e)

                lblSessionFlag.Text = 0

                ddl_AD_CommsissionType.SelectedIndex = 0
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

            If Not ddl_Admin.SelectedIndex = 0 Then

                If Not ddlAPIName.SelectedIndex = 0 Then
                    API = " and APIName ='" & GV.parseString(ddlAPIName.SelectedValue.Trim) & "' "
                End If
                Dim dd() As String = ddl_Admin.SelectedValue.Split(":")

                str = "select RID as SrNo,(select CompanyCode+':'+CompanyName from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration CL where CL.CompanyCode=PSA.AdminID) as [AdminIDX],* from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA PSA where AdminID='" & dd(0).Trim & "' " & API & "  order by rid desc "
                ds = GV.FL.OpenDsWithSelectQuery(str)
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()
                If GridView1.Rows.Count > 0 Then
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                End If
            Else
                GridView1.DataSource = Nothing
                GridView1.DataBind()
            End If
           

        Catch ex As Exception
        End Try
    End Sub

    Dim statecode As String = ""
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim VAdminID, VAPIName, VFromAmount, VToAmount As String
            Dim V_AD_CommissionType, V_AD_Commission As String

            V_AD_CommissionType = ""
            V_AD_Commission = "0"
            VFromAmount = "0"
            VToAmount = "0"

            If ddl_Admin.SelectedIndex = 0 Then
                lblError.Text = "Please Select Admin."
                lblError.CssClass = "errorlabels"
                ddl_Admin.Focus()
                Exit Sub
            Else
                Dim yy() As String = ddl_Admin.SelectedValue.Trim.Split(":")
                VAdminID = yy(0).Trim
            End If

            If ddlAPIName.SelectedIndex = 0 Then
                lblError.Text = "Please Select API Name."
                lblError.CssClass = "errorlabels"
                ddlAPIName.Focus()
                Exit Sub
            Else
                VAPIName = GV.parseString(ddlAPIName.SelectedValue.Trim)
            End If

            If ddl_Slab.SelectedIndex = 0 Then
                lblError.Text = "Please Select Slab Value."
                lblError.CssClass = "errorlabels"
                ddl_Slab.Focus()
                Exit Sub
            Else
                Dim dd() As String = ddl_Slab.SelectedValue.Split("-")
                VFromAmount = dd(0)
                VToAmount = dd(1)
            End If


            If ddl_AD_CommsissionType.SelectedIndex = 0 Then
                V_AD_CommissionType = GV.parseString(ddl_AD_CommsissionType.SelectedValue.Trim)
            Else
                If txt_AD_Commission.Text = "" Then
                    lblError.Text = "Please Enter Admin Commission."
                    lblError.CssClass = "errorlabels"
                    txt_AD_Commission.Focus()
                    Exit Sub
                Else
                    V_AD_Commission = GV.parseString(txt_AD_Commission.Text.Trim)
                End If
                V_AD_CommissionType = GV.parseString(ddl_AD_CommsissionType.SelectedValue.Trim)
            End If
            If GV.parseString(V_AD_Commission.Trim) = "" Then
                V_AD_Commission = "0"
            End If


            '/////////////////////////

            '////////////////////////////

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then
                'VCode
                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA where AdminID='" & VAdminID & "' and APIName='" & VAPIName & "' and FromAmount='" & VFromAmount & "' and ToAmount='" & VToAmount & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " API With Same From And To Amount Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA (AdminID,Admin_CommissionType,Admin_Commission,APIName,FromAmount,ToAmount,UpdatedBy,UpdatedOn) values('" & VAdminID & "','" & V_AD_CommissionType & "'," & V_AD_Commission & ",'" & VAPIName & "','" & VFromAmount & "','" & VToAmount & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()

                        ddl_Slab.SelectedIndex = 0


                        txt_AD_Commission.Text = "0"


                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"

                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                QryStr = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA set Admin_CommissionType='" & V_AD_CommissionType & "',Admin_Commission=" & V_AD_Commission & ", UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"

                'FromAmount='" & VFromAmount & "',ToAmount='" & VToAmount & "',



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

            ddl_Slab.SelectedIndex = 0
            ddl_Admin.Enabled = True
            ddl_Slab.Enabled = True

            txt_AD_Commission.Text = "0"
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""


            ddl_AD_CommsissionType.SelectedIndex = 0
            If ddl_AD_CommsissionType.SelectedIndex = 0 Then
                txt_AD_Commission.Text = "0"
                txt_AD_Commission.Enabled = False
                txt_AD_Commission.CssClass = "form-control"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddlAPIName.SelectedIndex = 0
            ddlAPIName_SelectedIndexChanged(sender, e)
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

            ddl_Admin.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            ddl_Admin_SelectedIndexChanged(sender, e)


            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            ddlAPIName.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            ddlAPIName_SelectedIndexChanged(sender, e)

            ddl_Slab.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) & "-" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)

            ddl_AD_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)
            txt_AD_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)




            ddl_Admin.Enabled = False
            ddl_Slab.Enabled = False

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
                QryStr = "delete from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA where RID=" & lblRID.Text & ""
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
                GV.ExportToExcel_New(GridView1, Response, "", "SlabWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "SlabWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "SlabWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub ddlAPIName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAPIName.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            If Not lblSessionFlag.Text = 1 Then
                Bind()
            End If
            ddl_Slab.Items.Clear()
            If Not ddlAPIName.SelectedIndex = 0 Then
                GV.FL.AddInDropDownListAll(ddl_Slab, "convert(varchar,FromAmount)+'-'+convert(varchar,ToAmount)", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA where APIName='" & ddlAPIName.SelectedValue.Trim & "'  order by FromAmount asc")
                If ddl_Slab.Items.Count > 0 Then
                    ddl_Slab.Items.Insert(0, ":: Select Slab Value ::")
                Else
                    ddl_Slab.Items.Add(":: Select Slab Value ::")
                End If
            Else
                ddl_Slab.Items.Add(":: Select Slab Value ::")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_SA_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_AD_CommsissionType.SelectedIndexChanged
        Try
            txt_AD_Commission.Enabled = True
            txt_AD_Commission.CssClass = "form-control"
            If ddl_AD_CommsissionType.SelectedIndex = 0 Then
                txt_AD_Commission.Text = "0"
                txt_AD_Commission.Enabled = False
                txt_AD_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddl_Admin_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Admin.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            Bind()
            ddlAPIName.SelectedIndex = 0
            ddlAPIName_SelectedIndexChanged(sender, e)


        Catch ex As Exception

        End Try
    End Sub

End Class