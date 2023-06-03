
Public Class SuperAdminSubModuleMaster
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VModuleName, VOrderNo, VRefSubModule, VRefSubModule_Order, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                chklink.Checked = False
                chklink_CheckedChanged(sender, e)
                GV.FL.AddInDropDownListDistinct(ddlRefModule, "ModuleName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule  order by ModuleName asc")
                ddlRefModule.Items.Insert(0, ":::: Select Module ::::")
                ddlRefModule_SelectedIndexChanged(sender, e)
                lblSessionFlag.Text = 0

                Bind()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text
            ModalPopupExtender1.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then


                'Refmodule = GV.FL.AddInVar("ModuleName", "CRM_MainModule  where RID=" & lblRID.Text & "")

                Dim str As String = GV.FL.AddInVar("ModuleName+','+RefSubModule", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule  where RID=" & lblRID.Text & "")

                Dim arr() As String = str.Split(",")
                Dim Refmodule, Refsubmodule As String
                Refmodule = arr(0)
                Refsubmodule = arr(1)


                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where RID=" & lblRID.Text.Trim & " ;"
                QryStr = QryStr & " " & "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & Refmodule & "' and RefSubModule='" & Refsubmodule & "'  ;"
                QryStr = QryStr & " " & "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where RefModule='" & Refmodule & "' and RefSubModule='" & Refsubmodule & "' ;"


                'QryStr = "delete from CRM_MainModule where RID=" & lblRID.Text & ";"
                'QryStr = QryStr & " " & "delete from CRM_Module_Master where RefModule='" & Refmodule & "';"
                'QryStr = QryStr & " " & "delete from CRM_UserRightsMaster where RefModule='" & Refmodule & "';"

                GV.FL.DMLQueries(QryStr)
                Clear()
                lblError.Text = "Record deleted Successfully."
                lblError.CssClass = "Successlabels"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Dim VFormName As String = ""
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            lblError.Text = ""
            lblError.CssClass = ""


            If ddlRefModule.SelectedValue = ":::: Select Module ::::" Then
                lblError.Text = "Select Main Module"
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VModuleName = GV.parseString(ddlRefModule.SelectedValue)
            End If

            If Not txtOrderNo.Text.Trim = "" Then
                VOrderNo = GV.parseString(txtOrderNo.Text.Trim)
            Else
                lblError.Text = "Enter Main Module Order."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            If Not txtRefSubModule.Text.Trim = "" Then
                VRefSubModule = GV.parseString(txtRefSubModule.Text.Trim)
            Else
                lblError.Text = "Enter Sub Module."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            'If txtFormName.Text.Trim = "" Then
            '    lblError.Text = "Enter Url"
            '    lblError.CssClass = "errorlabels"
            '    Exit Sub
            'End If
            If Not txtRefSubModule_Order.Text.Trim = "" Then
                VRefSubModule_Order = GV.parseString(txtRefSubModule_Order.Text.Trim)
            Else
                lblError.Text = "Enter Sub Module Order."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            Dim VUrl, VVsearchKey, VCheck As String
            'VRefSubModule
            VCheck = "NO"
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
                Dim abc As String = "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & VModuleName & "' and RefSubModule='' and not NavigationModule=''"
                If Not GV.FL.RecCount(abc) > 0 Then

                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & VModuleName & "' and RefSubModule='" & VRefSubModule & "'") > 0 Then
                        lblError.Text = "Record Already Exists."
                        lblError.CssClass = "errorlabels"
                    Else
                        QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule (CreateLink,FormName,Searching_Keyword,OrderNo,ModuleName,RefSubModule,RefSubModule_Order,UpdatedBy,UpdatedOn) values('" & VCheck & "','" & VUrl & "','" & VVsearchKey & "','" & VOrderNo & "', '" & VModuleName & "','" & VRefSubModule & "','" & VRefSubModule_Order & "','" & VUpdatedBy & "','" & VUpdatedOn & "' );"
                        If GV.FL.DMLQueries(QryStr) = True Then
                            Clear()
                            ddlRefModule_SelectedIndexChanged(sender, e)
                            Bind()
                            lblError.Text = "Record Saved Successfully."
                            lblError.CssClass = "Successlabels"
                        Else
                            lblError.Text = "Record insertion failed."
                            lblError.CssClass = "errorlabels"
                        End If

                    End If
                Else
                    lblError.Text = "Navigation Module Exists so you can't add Sub Module."
                    lblError.CssClass = "errorlabels"
                End If

            ElseIf lblSessionFlag.Text = 1 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & VModuleName & "' and RefSubModule='" & VRefSubModule & "'  and not RefSubModule='" & GV.parseString(lblOldRefSubModule.Text.Trim) & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    If VCheck = "YES" Then
                        QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule set CreateLink='" & VCheck & "',FormName='" & VUrl & "',Searching_Keyword='" & VVsearchKey & "', OrderNo='" & VOrderNo & "', ModuleName='" & VModuleName & "',RefSubModule='" & VRefSubModule & "',RefSubModule_Order='" & VRefSubModule_Order & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where      ModuleName='" & GV.parseString(lblOldModuleName.Text) & "' and  RefSubModule='" & GV.parseString(lblOldRefSubModule.Text) & "';"
                    Else

                    End If
                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule set CreateLink='" & VCheck & "',FormName='" & VUrl & "',Searching_Keyword='" & VVsearchKey & "', OrderNo='" & VOrderNo & "', ModuleName='" & VModuleName & "',RefSubModule='" & VRefSubModule & "',RefSubModule_Order='" & VRefSubModule_Order & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where      ModuleName='" & GV.parseString(lblOldModuleName.Text) & "' and  RefSubModule='" & GV.parseString(lblOldRefSubModule.Text) & "';"
                    QryStr = QryStr & " " & "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master set OrderNo='" & VOrderNo & "', RefModule='" & VModuleName & "',RefSubModule='" & VRefSubModule & "',RefSubModule_Order='" & VRefSubModule_Order & "' where RefModule='" & GV.parseString(lblOldModuleName.Text) & "' and  RefSubModule='" & GV.parseString(lblOldRefSubModule.Text) & "' ;"
                    QryStr = QryStr & " " & "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster set  OrderNo='" & VOrderNo & "', RefModule='" & VModuleName & "',RefSubModule='" & VRefSubModule & "',RefSubModule_Order='" & VRefSubModule_Order & "' where RefModule='" & GV.parseString(lblOldModuleName.Text) & "' and  RefSubModule='" & GV.parseString(lblOldRefSubModule.Text) & "' ;"

                    If GV.FL.DMLQueries(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Clear()
                        ddlRefModule_SelectedIndexChanged(sender, e)
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Sub Bind()
        Try
            If ddlRefModule.SelectedValue = ":::: Select Module ::::" Then
                QryStr = "select RID as SrNo,ModuleName,OrderNo,RefSubModule,RefSubModule_Order,CreateLink,FormName,Searching_Keyword from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule  order by OrderNo desc  "
            Else
                QryStr = "select RID as SrNo,ModuleName,OrderNo,RefSubModule,RefSubModule_Order,CreateLink,FormName,Searching_Keyword from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & GV.parseString(ddlRefModule.SelectedValue.Trim) & "'  order by OrderNo desc  "
            End If
            GV.FL.AddInGridViewWithFieldName(GridView1, QryStr)
            GV.FL.showSerialnoOnGridView(GridView1, 0)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub Clear()
        Try

            ' ddlRefModule.SelectedValue = ":::: Select Module ::::"

            'txtOrderNo.Text = ""

            txtRefSubModule.Text = ""
            txtRefSubModule_Order.Text = ""
            chklink.Checked = False
            txturl.Text = ""
            txtsearchingKeyword.Text = ""
            txturl.ReadOnly = True
            txtsearchingKeyword.ReadOnly = True

            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblError.CssClass = ""
            If GridView1.Visible = True Then
                Bind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub



    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'lblRID.Text = GridView1.DataKeys(gvrow.RowIndex).Value.ToString()
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text
            ddlRefModule.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            txtOrderNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            txtRefSubModule.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            txtRefSubModule_Order.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            lblOldModuleName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            lblOldOrderNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            lblOldRefSubModule.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            Dim aa As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            If aa = "YES" Then
                chklink.Checked = True
                chklink.Enabled = False
            Else
                chklink.Enabled = True
                chklink.Checked = False
            End If
            txturl.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            txtsearchingKeyword.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            lblSessionFlag.Text = 1
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.CssClass = ""
            lblError.Text = ""
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            ModalPopupExtender1.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception
                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

            End Try
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


    Protected Sub ddlRefModule_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRefModule.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            txtRefSubModule.Text = ""
            txtRefSubModule_Order.Text = ""

            If Not ddlRefModule.SelectedValue = "" Then
                txtOrderNo.Text = GV.FL.AddInVar("OrderNo", "CRM_MainModule  where ModuleName='" & GV.parseString(ddlRefModule.SelectedValue.Trim) & "' ")
            End If


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

End Class