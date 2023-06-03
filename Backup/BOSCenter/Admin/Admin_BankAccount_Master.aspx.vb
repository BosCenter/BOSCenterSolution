
Public Class Admin_BankAccount_Master
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Dim vcountryName, VCompanyCode, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""
    Dim DS As New DataSet
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            If txtAccountHolderName.Text.Trim = "" Then
                lblError.Text = "Acount Holder Name Required."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtAccountNo.Text.Trim = "" Then
                lblError.Text = "Acount No Required."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtIFSCCode.Text.Trim = "" Then
                lblError.Text = "IFSC Code Required."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtBankName.Text.Trim = "" Then
                lblError.Text = "Bank Name Required."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtBranchName.Text = "" Then
                lblError.Text = "Branch Name Required."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


          
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"
            If Session("Editflag") = 1 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Admin_BankAccount_Master where ( AccountNo='" & GV.parseString(txtAccountNo.Text.Trim) & "' and not AccountNo='" & GV.parseString(lblUpadate.Text) & "' )") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    Session("Editflag") = 0
                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Admin_BankAccount_Master set AccountHolder_Name='" & GV.parseString(txtAccountHolderName.Text.Trim) & "', AccountNo='" & GV.parseString(txtAccountNo.Text.Trim) & "',IFSC_Code='" & GV.parseString(txtIFSCCode.Text.Trim) & "',Bank_Name='" & GV.parseString(txtBankName.Text.Trim) & "', BranchName='" & GV.parseString(txtBranchName.Text.Trim) & "', AccountType='" & GV.parseString(ddlAccountType.SelectedItem.Value.Trim) & "',PanNo='" & GV.parseString(txtPanNo.Text.Trim) & "',UpdatedBy='" & VUpdatedBy & "',UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text & " "
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Admin_BankAccount_Master where  AccountNo='" & GV.parseString(txtAccountNo.Text.Trim) & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Admin_BankAccount_Master (AccountHolder_Name,AccountNo,IFSC_Code,Bank_Name,BranchName,AccountType,PanNo,UpdatedBy,UpdatedOn) values('" & GV.parseString(txtAccountHolderName.Text.Trim) & "','" & GV.parseString(txtAccountNo.Text.Trim) & "','" & GV.parseString(txtIFSCCode.Text.Trim) & "', '" & GV.parseString(txtBankName.Text.Trim) & "','" & GV.parseString(txtBranchName.Text.Trim) & "','" & GV.parseString(ddlAccountType.SelectedValue.Trim) & "','" & GV.parseString(txtPanNo.Text.Trim) & "','" & VUpdatedBy & "'," & VUpdatedOn & "); "
                    If GV.FL.DMLQueries(QryStr) = True Then
                        ClearAll()
                        Bind()
                        lblError.Text = "Record Saved Successfully."
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
            Dim str As String = ""
            lblExportQry.Text = ""
            str = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Admin_BankAccount_Master order by RID desc "
            DS = GV.FL.OpenDsWithSelectQuery(str)
            lblExportQry.Text = str
            GridView1.DataSource = DS.Tables(0)
            GridView1.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)

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
                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Admin_BankAccount_Master where RID=" & lblRID.Text & " "
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
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'lblRID.Text = GridView1.DataKeys(gvrow.RowIndex).Value.ToString()

            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)

            txtAccountHolderName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            txtAccountNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            txtIFSCCode.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            txtBankName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            txtBranchName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            ddlAccountType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            txtPanNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)


            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            lblError.Text = ""
            Session("Editflag") = 1
            btnSave.Text = "Update"
            lblError.CssClass = ""
            btnDelete.Enabled = True



        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            ClearAll()
            lblExportQry.Text = ""

            Session("Editflag") = 0
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
            lblExportQry.Text = ""
            txtBranchName.Text = ""
            txtAccountHolderName.Text = ""
            txtAccountNo.Text = ""
            txtBankName.Text = ""
            txtIFSCCode.Text = ""
            If ddlAccountType.Items.Count > 0 Then
                ddlAccountType.SelectedIndex = 0
            End If
            txtPanNo.Text = ""

            lblError.Text = ""
            lblError.CssClass = ""
            lblRID.Text = ""
            Session("Editflag") = 0
            btnSave.Text = "Save"
            btnDelete.Enabled = False
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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub
    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToExcel_New(GridView1, Response, "", "BankAccountMaster", lblExportQry.Text, "Static")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImagebtnWOrd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToWord_New(GridView1, Response, "BankAccountMaster", lblExportQry.Text, "Static")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ExportToPdf_DivTag_HavingGridview(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToPdf_New(GridView1, "", Response, "BankAccountMaster", lblExportQry.Text, "Static")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class