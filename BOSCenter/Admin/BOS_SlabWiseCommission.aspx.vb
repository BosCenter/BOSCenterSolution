
Public Class BOS_SlabWiseCommission
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'InsertOperator()
                'Exit Sub

                lblSessionFlag.Text = 0


                GV.FL.AddInDropDownListDistinct(ddlAPIName, "Title", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where SlabApplicable='With Slab' and ProductType='API' ")
                If ddlAPIName.Items.Count > 0 Then
                    ddlAPIName.Items.Insert(0, "Select API")
                Else
                    ddlAPIName.Items.Add("Select API")
                End If
                ddlProductService_SelectedIndexChanged(sender, e)




                ddl_Distributor_CommsissionType.SelectedIndex = 0
                ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0
                ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Retailer_CommsissionType.SelectedIndex = 0
                ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Customer_CommsissionType.SelectedIndex = 0
                ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)

                Bind()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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

            str = "select RID as SrNo,* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise " & API & " " & Category & "  order by rid desc "
            ds = GV.FL.OpenDsWithSelectQuery(str)
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                GV.FL.showSerialnoOnGridView(GridView1, 1)
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Dim statecode As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            Dim VAPIName, VFromAmount, VToAmount As String
            Dim V_Dis_CommissionType, V_Dis_Commission, V_Sub_Dis_CommissionType, V_Sub_Dis_Commission, V_Retailer_CommissionType, V_Retailer_Commission, V_Customer_CommissionType, V_Customer_Commission As String

            V_Sub_Dis_CommissionType = ""
            V_Sub_Dis_Commission = "0"

            V_Dis_CommissionType = ""
            V_Dis_Commission = "0"

            V_Retailer_CommissionType = ""
            V_Retailer_Commission = "0"

            V_Customer_CommissionType = ""
            V_Customer_Commission = "0"


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


            If ddl_Distributor_CommsissionType.SelectedIndex = 0 Then
                V_Dis_CommissionType = GV.parseString(ddl_Distributor_CommsissionType.SelectedValue.Trim)
            Else
                If txt_Dis_Commission.Text = "" Then
                    lblError.Text = "Please Enter Master Dis Commission."
                    lblError.CssClass = "errorlabels"
                    txt_Dis_Commission.Focus()
                    Exit Sub
                Else
                    V_Dis_Commission = GV.parseString(txt_Dis_Commission.Text.Trim)
                End If
                V_Dis_CommissionType = GV.parseString(ddl_Distributor_CommsissionType.SelectedValue.Trim)
            End If
            If GV.parseString(V_Dis_Commission.Trim) = "" Then
                V_Dis_Commission = "0"
            End If


            '/////////////////////////


            If ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0 Then
                V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Distributor_CommsissionType.SelectedValue.Trim)
            Else
                If txt_Sub_Dis_Commission.Text = "" Then
                    lblError.Text = "Please Enter Dis Commission."
                    lblError.CssClass = "errorlabels"
                    txt_Sub_Dis_Commission.Focus()
                    Exit Sub
                Else
                    V_Sub_Dis_Commission = GV.parseString(txt_Sub_Dis_Commission.Text.Trim)
                End If
                V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Distributor_CommsissionType.SelectedValue.Trim)
            End If
            If GV.parseString(V_Sub_Dis_Commission.Trim) = "" Then
                V_Sub_Dis_Commission = "0"
            End If



            If ddl_Retailer_CommsissionType.SelectedIndex = 0 Then
                V_Retailer_CommissionType = GV.parseString(ddl_Retailer_CommsissionType.SelectedValue.Trim)
            Else
                If txt_Retailer_Commission.Text = "" Then
                    lblError.Text = "Please Enter Retailer Commission."
                    lblError.CssClass = "errorlabels"
                    txt_Retailer_Commission.Focus()
                    Exit Sub
                Else
                    V_Retailer_Commission = GV.parseString(txt_Retailer_Commission.Text.Trim)
                End If
                V_Retailer_CommissionType = GV.parseString(ddl_Retailer_CommsissionType.SelectedValue.Trim)
            End If
            If GV.parseString(V_Retailer_Commission.Trim) = "" Then
                V_Retailer_Commission = "0"
            End If


            If ddl_Customer_CommsissionType.SelectedIndex = 0 Then
                V_Customer_CommissionType = GV.parseString(ddl_Customer_CommsissionType.SelectedValue.Trim)
            Else
                If txt_Customer_Commission.Text = "" Then
                    lblError.Text = "Please Enter Customer Commission."
                    lblError.CssClass = "errorlabels"
                    txt_Customer_Commission.Focus()
                    Exit Sub
                Else
                    V_Customer_Commission = GV.parseString(txt_Customer_Commission.Text.Trim)
                End If
                V_Customer_CommissionType = GV.parseString(ddl_Customer_CommsissionType.SelectedValue.Trim)
            End If
            If GV.parseString(V_Customer_Commission.Trim) = "" Then
                V_Customer_Commission = "0"
            End If

            '////////////////////////////

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then
                'VCode
                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where APIName='" & VAPIName & "' and FromAmount='" & VFromAmount & "' and ToAmount='" & VToAmount & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " API With Same From And To Amount Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise (Customer_CommissionType,Customer_Commission,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,APIName,FromAmount,ToAmount,Dis_CommissionType,Dis_Commission,UpdatedBy,UpdatedOn) values('" & V_Customer_CommissionType & "'," & V_Customer_Commission & ",'" & V_Sub_Dis_CommissionType & "'," & V_Sub_Dis_Commission & ",'" & V_Retailer_CommissionType & "'," & V_Retailer_Commission & ",'" & VAPIName & "','" & VFromAmount & "','" & VToAmount & "','" & V_Dis_CommissionType & "'," & V_Dis_Commission & ",'" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        txt_Dis_Commission.Text = "0"
                        txt_Retailer_Commission.Text = "0"
                        txt_Sub_Dis_Commission.Text = "0"


                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"

                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise set FromAmount='" & VFromAmount & "',ToAmount='" & VToAmount & "',Sub_Dis_CommissionType='" & V_Sub_Dis_CommissionType & "',Sub_Dis_Commission=" & V_Sub_Dis_Commission & ",Retailer_CommissionType='" & V_Retailer_CommissionType & "',Retailer_Commission=" & V_Retailer_Commission & ",Customer_CommissionType='" & V_Customer_CommissionType & "',Customer_Commission=" & V_Customer_Commission & ",Dis_CommissionType='" & V_Dis_CommissionType & "',Dis_Commission=" & V_Dis_Commission & ", UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"


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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub Clear()
        Try
            ddlAPIName.Enabled = True
            ddlAPIName.CssClass = "form-control"
            ddl_Slab.SelectedIndex = 0
            ddl_Slab.Enabled = True
            txt_Dis_Commission.Text = "0"
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""


            ddl_Distributor_CommsissionType.SelectedIndex = 0
            If ddl_Distributor_CommsissionType.SelectedIndex = 0 Then
                txt_Dis_Commission.Text = "0"
                txt_Dis_Commission.Enabled = False
                txt_Dis_Commission.CssClass = "form-control"
            End If

            ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0
            If ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0 Then
                txt_Sub_Dis_Commission.Text = "0"
                txt_Sub_Dis_Commission.Enabled = False
                txt_Sub_Dis_Commission.CssClass = "form-control"
            End If


            ddl_Retailer_CommsissionType.SelectedIndex = 0
            If ddl_Retailer_CommsissionType.SelectedIndex = 0 Then
                txt_Retailer_Commission.Text = "0"
                txt_Retailer_Commission.Enabled = False
                txt_Retailer_Commission.CssClass = "form-control"
            End If

            ddl_Customer_CommsissionType.SelectedIndex = 0
            If ddl_Customer_CommsissionType.SelectedIndex = 0 Then
                txt_Customer_Commission.Text = "0"
                txt_Customer_Commission.Enabled = False
                txt_Customer_Commission.CssClass = "form-control"
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddlAPIName.SelectedIndex = 0
            ddlProductService_SelectedIndexChanged(sender, e)
            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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

            ddl_Slab.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) & "-" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)

            'txtFromAmount.Text = CInt(GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text))
            'txtToAmount.Text = CInt(GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text))



            ddl_Distributor_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Dis_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)


            ddl_Sub_Distributor_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Sub_Dis_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)

            ddl_Retailer_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Retailer_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text) = "" Then
                ddl_Customer_CommsissionType.SelectedIndex = 0
            Else
                ddl_Customer_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text)
            End If

            ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Customer_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text)


            'Session("Editflag") = 1
            ddl_Slab.Enabled = False
            ddlAPIName.Enabled = False
            ddlAPIName.CssClass = "form-control"

            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.Text = ""
            lblError.CssClass = ""

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then
                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where RID=" & lblRID.Text & ""
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ImagebtnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToExcel_New(GridView1, Response, "", "SlabWiseCommission", "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "SlabWiseCommission", "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "SlabWiseCommission", "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub ddlProductService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAPIName.SelectedIndexChanged
        Try

            lblError.Text = ""
            lblError.CssClass = ""


            If Not lblSessionFlag.Text = "1" Then
                Bind()
            End If

            ddl_Slab.Items.Clear()
            If Not ddlAPIName.SelectedIndex = 0 Then
                GV.FL.AddInDropDownListAll(ddl_Slab, "convert(varchar,FromAmount)+'-'+convert(varchar,ToAmount)", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA where APIName='" & ddlAPIName.SelectedValue.Trim & "'  order by FromAmount asc")
                If ddl_Slab.Items.Count > 0 Then
                    ddl_Slab.Items.Insert(0, ":: Select Slab Value ::")
                Else
                    ddl_Slab.Items.Add(":: Select Slab Value ::")
                End If
            Else
                ddl_Slab.Items.Add(":: Select Slab Value ::")
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddl_Distributor_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Distributor_CommsissionType.SelectedIndexChanged
        Try
            txt_Dis_Commission.Enabled = True
            txt_Dis_Commission.CssClass = "form-control"
            If ddl_Distributor_CommsissionType.SelectedIndex = 0 Then
                txt_Dis_Commission.Text = "0"
                txt_Dis_Commission.Enabled = False
                txt_Dis_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sub_Distributor_CommsissionType.SelectedIndexChanged
        Try
            txt_Sub_Dis_Commission.Enabled = True
            txt_Sub_Dis_Commission.CssClass = "form-control"
            If ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0 Then
                txt_Sub_Dis_Commission.Text = "0"
                txt_Sub_Dis_Commission.Enabled = False
                txt_Sub_Dis_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddl_Retailer_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Retailer_CommsissionType.SelectedIndexChanged
        Try
            txt_Retailer_Commission.Enabled = True
            txt_Retailer_Commission.CssClass = "form-control"
            If ddl_Retailer_CommsissionType.SelectedIndex = 0 Then
                txt_Retailer_Commission.Text = "0"
                txt_Retailer_Commission.Enabled = False
                txt_Retailer_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub ddl_Customer_CommsissionType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Customer_CommsissionType.SelectedIndexChanged
        Try
            txt_Customer_Commission.Enabled = True
            txt_Customer_Commission.CssClass = "form-control"
            If ddl_Customer_CommsissionType.SelectedIndex = 0 Then
                txt_Customer_Commission.Text = "0"
                txt_Customer_Commission.Enabled = False
                txt_Customer_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class