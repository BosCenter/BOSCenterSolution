Public Class Admin_AreaMaster
    Inherits System.Web.UI.Page


    Dim GV As New GlobalVariable("ADMIN")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                GV.FL.AddInDropDownListDistinct(ddlCountry, "Country_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_CountryMaster")
                If ddlCountry.Items.Count > 0 Then
                    ddlCountry.Items.Insert(0, ":::: Select Country ::::")
                Else
                    ddlCountry.Items.Add(":::: Select Country ::::")
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Dim vcountryName, VUpdatedBy, VUpdatedOn, VpinCode As String
    Dim QryStr As String = ""

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
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


            If ddlCountry.SelectedIndex = 0 Then
                lblError.Text = "Please Select Country."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            If ddlStateName.SelectedIndex = 0 Then
                lblError.Text = "Please Select State."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If ddldistrict.SelectedIndex = 0 Then
                lblError.Text = "Please Select State."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtarea.Text.Trim = "" Then
                lblError.Text = "Area Name Required."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If txtPincode.Text.Trim = "" Then
                lblError.Text = "Enter PinCode."
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VpinCode = txtPincode.Text.Trim
            End If


            VUpdatedBy = GV.get_Admin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If Session("Editflag") = 1 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_AreaMaster where Country_Name='" & GV.parseString(ddlCountry.SelectedValue) & "' and State_Name='" & ddlStateName.SelectedValue & "' and District_Name ='" & ddldistrict.SelectedValue & "' and ( Area_Name='" & GV.parseString(txtarea.Text.Trim) & "' and not Area_Name='" & GV.parseString(lblUpadate.Text) & "' ) and PinCode='" & GV.parseString(txtPincode.Text.Trim) & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    Session("Editflag") = 0
                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_AreaMaster set PinCode='" & VpinCode & "', Country_Name='" & GV.parseString(ddlCountry.SelectedValue) & "', State_Name='" & GV.parseString(ddlStateName.SelectedItem.Value.Trim) & "',District_Name ='" & ddldistrict.SelectedValue & "',Area_Name='" & GV.parseString(txtarea.Text.Trim) & "',UpdatedBy='" & VUpdatedBy & "',UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text & " "
                    If GV.FL.DMLQueries(QryStr) = True Then
                        ClearAll()
                        Bind()
                        lblError.Text = "Record Updated Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If
            Else

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_AreaMaster where Country_Name='" & GV.parseString(ddlCountry.SelectedValue) & "' and State_Name='" & ddlStateName.SelectedValue & "' and District_Name ='" & ddldistrict.SelectedValue & "' and Area_Name='" & GV.parseString(txtarea.Text.Trim) & "' and PinCode='" & GV.parseString(txtPincode.Text.Trim) & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_AreaMaster(PinCode,Country_Name,State_Name,District_Name,Area_Name,UpdatedBy,UpdatedOn) values('" & VpinCode & "','" & GV.parseString(ddlCountry.SelectedValue) & "','" & GV.parseString(ddlStateName.SelectedItem.Value.Trim) & "','" & ddldistrict.SelectedValue & "','" & GV.parseString(txtarea.Text.Trim) & "','" & VUpdatedBy & "'," & VUpdatedOn & "); "
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                        txtarea.Text = ""
                        txtPincode.Text = ""
                        txtarea.Focus()

                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If
            End If

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

            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Call_Registration where Area='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Deleted.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                btnok.Visible = False
                ModalPopupExtender1.Show()

            ElseIf GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DealerMaster where Area='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Deleted.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "</b>  is in Use."
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
                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_AreaMaster where RID=" & lblRID.Text & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    ClearAll()
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
            'lblRID.Text = GridView1.DataKeys(gvrow.RowIndex).Value.ToString()

            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            If GV.FL.RecCount("CRM_Call_Registration where Area='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Updated.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                btnok.Visible = False
                ModalPopupExtender1.Show()

            ElseIf GV.FL.RecCount("CRM_DealerMaster where Area='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Updated.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                btnok.Visible = False
                ModalPopupExtender1.Show()

            Else
                ddlCountry.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
                ddlStateName.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
                ddldistrict.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
                lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
                txtarea.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
                txtPincode.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
                lblError.Text = ""
                Session("Editflag") = 1
                btnSave.Text = "Update"
                ddlCountry.Enabled = False
                ddlCountry.CssClass = "form-control"

                ddldistrict.Enabled = False
                ddldistrict.CssClass = "form-control"
                ddlStateName.Enabled = False
                ddlStateName.CssClass = "form-control"
                lblError.CssClass = ""
                btnDelete.Enabled = True
                txtarea.Focus()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = " Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            btnok.Visible = True
            ModalPopupExtender1.Show()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearAll()
        Try
            txtPincode.Text = ""
            lblExportQry.Text = ""
            txtarea.Text = ""
            lblError.Text = ""
            lblRID.Text = ""
            lblError.CssClass = ""
            Session("Editflag") = 0
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            Bind()
            ddlCountry.Enabled = True
            ddlCountry.CssClass = "form-control"
            ddlStateName.Enabled = True
            ddlStateName.CssClass = "form-control"
            ddldistrict.Enabled = True
            ddldistrict.CssClass = "form-control"

            ddlCountry.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            txtarea.Text = ""
            lblError.Text = ""
            lblRID.Text = ""
            txtPincode.Text = ""
            ddlCountry.SelectedIndex = 0
            ddlStateName.SelectedIndex = 0
            ddldistrict.SelectedIndex = 0
            lblError.CssClass = ""
            Session("Editflag") = 0
            lblExportQry.Text = ""
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            GridView1.PageIndex = 0
            Bind()
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            ddlCountry.Focus()
            ddlCountry.Enabled = True
            ddlCountry.CssClass = "form-control"
            ddlStateName.Enabled = True
            ddlStateName.CssClass = "form-control"
            ddldistrict.Enabled = True
            ddldistrict.CssClass = "form-control"
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCountry.SelectedIndexChanged
        Try
            If ddlCountry.SelectedValue = ":::: Select Country ::::" Then
                txtarea.Text = ""
                Session("Editflag") = 0
                ddlStateName.Items.Clear()
                ddlStateName.Items.Add(":::: Select State ::::")
                ddlStateName_SelectedIndexChanged(sender, e)
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                lblError.Text = ""
                lblError.CssClass = ""
                GridView1.DataSource = Nothing
                GridView1.DataBind()

            Else
                GV.FL.AddInDropDownListDistinct(ddlStateName, "State_Name", "CRM_StateMaster where Country_Name='" & ddlCountry.SelectedValue & "' ")
                If ddlStateName.Items.Count > 0 Then
                    ddlStateName.Items.Insert(0, ":::: Select State ::::")
                Else
                    ddlStateName.Items.Add(":::: Select State ::::")
                End If
                txtarea.Text = ""
                lblError.Text = ""
                lblError.CssClass = ""
                GridView1.DataSource = Nothing
                GridView1.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlStateName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlStateName.SelectedIndexChanged
        Try

            If ddlStateName.SelectedValue = ":::: Select State ::::" Then
                txtarea.Text = ""
                Session("Editflag") = 0
                ddldistrict.Items.Clear()
                ddldistrict.Items.Add(":::: Select District ::::")
                ddldistrict_SelectedIndexChanged(sender, e)
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                lblError.Text = ""
                lblError.CssClass = ""
                GridView1.DataSource = Nothing
                GridView1.DataBind()

            Else
                GV.FL.AddInDropDownListDistinct(ddldistrict, "District_Name", "CRM_DistrictMaster where Country_Name='" & ddlCountry.SelectedValue & "' and State_Name ='" & ddlStateName.SelectedValue & "' ")
                If ddldistrict.Items.Count > 0 Then
                    ddldistrict.Items.Insert(0, ":::: Select District ::::")
                Else
                    ddldistrict.Items.Add(":::: Select District ::::")
                End If
                txtarea.Text = ""
                lblError.Text = ""
                lblError.CssClass = ""
                GridView1.DataSource = Nothing
                GridView1.DataBind()
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddldistrict_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddldistrict.SelectedIndexChanged
        Try
            Bind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Bind()
        Try
            lblExportQry.Text = ""
            Dim str As String = ""
            If Not ddldistrict.SelectedIndex = 0 Then
                str = "select * from CRM_AreaMaster where Country_Name='" & ddlCountry.SelectedValue & "' and State_Name='" & ddlStateName.SelectedValue & "' and District_Name ='" & ddldistrict.SelectedValue & "' order by RID asc "
                ds = GV.FL.OpenDsWithSelectQuery(str)
                lblExportQry.Text = str
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()
            End If

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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub
    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToExcel_New(GridView1, Response, "", "AreaMaster", lblExportQry.Text, "Static")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ImagebtnWOrd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToWord_New(GridView1, Response, "AreaMaster", lblExportQry.Text, "Static")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ExportToPdf_DivTag_HavingGridview(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToPdf_New(GridView1, "", Response, "AreaMaster", lblExportQry.Text, "Static")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class