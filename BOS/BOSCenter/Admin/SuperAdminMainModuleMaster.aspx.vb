
Public Class SuperAdminMainModuleMaster
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VModuleName, VOrderNo, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)

            lblRID.Text = lbl.Text
            'lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            ModalPopupExtender1.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then

                Dim Refmodule As String = ""
                Refmodule = GV.FL.AddInVar("ModuleName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule  where RID=" & lblRID.Text & "")


                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule where RID=" & lblRID.Text & ";"

                QryStr = QryStr & " " & "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & Refmodule & "';"
                QryStr = QryStr & " " & "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & Refmodule & "';"
                QryStr = QryStr & " " & "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where RefModule='" & Refmodule & "';"

                GV.FL.DMLQueries(QryStr)
                Clear()
                lblError.Text = "Record deleted Successfully."
                lblError.CssClass = "Successlabels"
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()
            chklink.Checked = False
            chklink_CheckedChanged(sender, e)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub chklink_CheckedChanged(sender As Object, e As EventArgs) Handles chklink.CheckedChanged
        Try
            txturl.Text = ""
            txtsearchingKeyword.Text = ""
            If chklink.Checked = False Then
                txturl.ReadOnly = True
                txtsearchingKeyword.ReadOnly = True
            Else
                txturl.ReadOnly = False
                txtsearchingKeyword.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            lblError.Text = ""
            lblError.CssClass = ""

            If Not txtModuleName.Text.Trim = "" Then
                VModuleName = GV.parseString(txtModuleName.Text.Trim)
            Else
                lblError.Text = "Enter Main Module."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If Not txtOrderNo.Text.Trim = "" Then
                VOrderNo = GV.parseString(txtOrderNo.Text.Trim)
            Else
                lblError.Text = "Enter Main Module Order."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            'VRefSubModule
            Dim VUrl, VVsearchKey, VCheck As String
            'VRefSubModule
            VCheck = "NO"
            VUrl = ""
            If chklink.Checked = True Then
                VCheck = "YES"
                If Not txturl.Text.Trim = "" Then
                    VUrl = GV.parseString(txturl.Text.Trim)
                Else
                    lblError.Text = "Enter Main Module Order."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                If Not txtsearchingKeyword.Text.Trim = "" Then
                    VVsearchKey = GV.parseString(txtsearchingKeyword.Text.Trim)
                Else
                    lblError.Text = "Enter Main Module Order."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            End If

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = Now.Date

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule where ModuleName='" & VModuleName & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule (CreateLink,FormName,Searching_Keyword,OrderNo,ModuleName,UpdatedBy,UpdatedOn) values('" & VCheck & "','" & VUrl & "','" & VVsearchKey & "','" & VOrderNo & "', '" & VModuleName & "','" & VUpdatedBy & "','" & VUpdatedOn & "' );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Clear()
                        Bind()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Record insertion failed."
                        lblError.CssClass = "errorlabels"
                    End If

                End If

            ElseIf lblSessionFlag.Text = 1 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule where ModuleName='" & VModuleName & "' and not ModuleName='" & GV.parseString(lblOldModuleName.Text.Trim) & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    If VCheck = "YES" Then
                        QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule set CreateLink='" & VCheck & "',FormName='" & VUrl & "',Searching_Keyword='" & VVsearchKey & "', OrderNo='" & VOrderNo & "', ModuleName='" & VModuleName & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where      ModuleName='" & GV.parseString(lblOldModuleName.Text) & "' ;"
                    Else
                        QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule set CreateLink='" & VCheck & "',FormName='" & VUrl & "',Searching_Keyword='" & VVsearchKey & "', OrderNo='" & VOrderNo & "', ModuleName='" & VModuleName & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where      ModuleName='" & GV.parseString(lblOldModuleName.Text) & "' ;"
                        QryStr = QryStr & " " & "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule set OrderNo='" & VOrderNo & "', ModuleName='" & VModuleName & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where      ModuleName='" & GV.parseString(lblOldModuleName.Text) & "' ;"
                        QryStr = QryStr & " " & "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master set OrderNo='" & VOrderNo & "', RefModule='" & VModuleName & "' where RefModule='" & GV.parseString(lblOldModuleName.Text) & "' ;"
                        QryStr = QryStr & " " & "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster set  OrderNo='" & VOrderNo & "', RefModule='" & VModuleName & "' where RefModule='" & GV.parseString(lblOldModuleName.Text) & "' ;"

                    End If


                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Clear()
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
            Dim Qry As String = "select RID as SrNo,ModuleName,OrderNo,CreateLink,FormName,Searching_Keyword from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule order by OrderNo desc  "
            GV.FL.AddInGridViewWithFieldName(GridView1, Qry)
            GV.FL.showSerialnoOnGridView(GridView1, 0)
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
    Private Sub Clear()
        Try

            txtModuleName.Text = ""
            txtOrderNo.Text = ""

            chklink.Enabled = True
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblError.CssClass = ""
            chklink.Checked = False
            txturl.Text = ""
            txtsearchingKeyword.Text = ""
           txturl.ReadOnly = True
            txtsearchingKeyword.ReadOnly = True
            If GridView1.Visible = True Then
                Bind()
            End If
            txtModuleName.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                chklink.Checked = False
                chklink_CheckedChanged(sender, e)
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
            'lblRID.Text = GridView1.DataKeys(gvrow.RowIndex).Value.ToString()
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)

            lblRID.Text = lbl.Text
            txtModuleName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            txtOrderNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)



            Dim aa As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            If aa = "YES" Then
                chklink.Checked = True
                'chklink.Enabled = False
            Else
                chklink.Enabled = True
                ' chklink.Checked = False
            End If
            chklink_CheckedChanged(sender, e)
            txturl.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            txtsearchingKeyword.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            ' lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            lblOldModuleName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            lblOldOrderNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            lblSessionFlag.Text = 1
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.CssClass = ""
            lblError.Text = ""
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
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