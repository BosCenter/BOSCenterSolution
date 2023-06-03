
Public Class SuperAdminMenuCreation
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VFrmSelected, VFormName, VCanSave, VCanSearch, VCanUpdate, VCanDelete, VRefModule, VRefSubModule, VNavigationModule, VUpdatedOn, VUpdatedBy As String
    Dim VRefSubModule_Order, VNavigationModule_Order As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Div_RefSubModule.Visible = False

            GV.FL.AddInDropDownListDistinct(ddlRefModule, "ModuleName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule order by ModuleName asc")
            ddlRefModule.Items.Insert(0, ":::: Select Module ::::")
            ddlRefModule_SelectedIndexChanged(sender, e)
            lblSessionFlag.Text = 0
            Bind()
        End If

    End Sub


    Public Sub Bind()
        Try

            If ddlRefModule.SelectedValue = ":::: Select Module ::::" Then
                If ddlRefSubModule.SelectedValue = ":::: Select Sub Module ::::" Or ddlRefSubModule.SelectedValue = "Not Applicable" Or ddlRefSubModule.SelectedValue = "Add Sub Module" Then
                    QryStr = "select RID as SrNo,RefModule,RefSubModule,NavigationModule,NavigationModule_Order,FormName,Searching_Keyword from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master order by RID desc"
                Else
                    QryStr = "select RID as SrNo,RefModule,RefSubModule,NavigationModule,NavigationModule_Order,FormName,Searching_Keyword from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefSubModule='" & ddlRefSubModule.SelectedValue & "' order by RID desc"
                End If
            Else
                If ddlRefSubModule.SelectedValue = ":::: Select Sub Module ::::" Or ddlRefSubModule.SelectedValue = "Not Applicable" Or ddlRefSubModule.SelectedValue = "Add Sub Module" Then
                    QryStr = "select RID as SrNo,RefModule,RefSubModule,NavigationModule,NavigationModule_Order,FormName,Searching_Keyword from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & ddlRefModule.SelectedValue & "' order by RID desc"
                Else
                    QryStr = "select RID as SrNo,RefModule,RefSubModule,NavigationModule,NavigationModule_Order,FormName,Searching_Keyword from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & ddlRefModule.SelectedValue & "'  and ( RefSubModule='" & ddlRefSubModule.SelectedValue & "' or RefSubModule is null) order by RID desc"
                End If

            End If
        GV.FL.AddInGridViewWithFieldName(GridView1, QryStr)
            GV.FL.showSerialnoOnGridView(GridView1, 0)

        Catch ex As Exception
        End Try
    End Sub
    Dim VsearchingKeyword As String = ""
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            lblError.CssClass = ""
            lblError.Text = ""
            If ddlRefModule.SelectedValue = ":::: Select Module ::::" Then
                lblError.Text = "Select Main Module"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If lblIsSubMenu_Available.Text.ToUpper = "YES" Then
                If ddlRefSubModule.SelectedValue = ":::: Select Sub Module ::::" Then
                    lblError.Text = "Select Sub Module"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            End If


            If txtNavigationModule.Text.Trim = "" Then
                lblError.Text = "Enter Navigation Module"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If txtFormName.Text.Trim = "" Then
                lblError.Text = "Enter Url"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If



            If txtNavigationModule_Order.Text.Trim = "" Then
                lblError.Text = "Enter Navigation Module Order"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtsearchingKeyword.Text.Trim = "" Then
                lblError.Text = "Enter Search Keyword"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            VFrmSelected = "false"

            VCanSave = "false"
            VCanSearch = "false"
            VCanUpdate = "false"
            VCanDelete = "false"

            If ddlRefModule.Items.Count > 0 Then
                If Not ddlRefModule.SelectedValue.Trim = "" Then
                    VRefModule = GV.parseString(ddlRefModule.SelectedValue.Trim)
                Else
                    VRefModule = ""
                End If
            End If


            If lblIsSubMenu_Available.Text.ToUpper = "YES" Then
                If ddlRefSubModule.Items.Count > 0 Then
                    If Not ddlRefSubModule.SelectedValue.Trim = "" Then
                        VRefSubModule = GV.parseString(ddlRefSubModule.SelectedValue.Trim)
                    Else
                        VRefSubModule = ""
                    End If
                End If
            Else
                VRefSubModule = ""
            End If

            If Not txtsearchingKeyword.Text.Trim = "" Then
                VsearchingKeyword = GV.parseString(txtsearchingKeyword.Text.Trim)
            Else
                VsearchingKeyword = ""
            End If
            If Not txtNavigationModule.Text.Trim = "" Then
                VNavigationModule = GV.parseString(txtNavigationModule.Text.Trim)
            Else
                VNavigationModule = ""
            End If

            If Not txtNavigationModule_Order.Text.Trim = "" Then
                VNavigationModule_Order = GV.parseString(txtNavigationModule_Order.Text.Trim)
            Else
                VNavigationModule_Order = ""
            End If

            If Not txtFormName.Text.Trim = "" Then
                VFormName = GV.parseString(txtFormName.Text.Trim)
            Else
                VFormName = ""
            End If

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = Now.Date

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master  where RefModule='" & VRefModule & "' and RefSubModule='" & VRefSubModule & "' and NavigationModule='" & VNavigationModule & "'") > 0 Then
                    lblError.Text = "Navigation Module Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.SarbHerb_Admin_Module_Master  where Searching_Keyword='" & VsearchingKeyword & "' ") > 0 Then
                        lblError.Text = "Searching KeyWords Already Exists."
                        lblError.CssClass = "errorlabels"
                        txtsearchingKeyword.Focus()
                        Exit Sub
                    End If
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master (Searching_Keyword,NavigationModule_Order,RefSubModule_Order,OrderNo,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy) values('" & VsearchingKeyword & "','" & VNavigationModule_Order & "',(select top 1 RefSubModule_Order from CRM_SubModule where ModuleName='" & VRefModule & "' and RefSubModule='" & VRefSubModule & "' ),(select  top 1  OrderNo from CRM_MainModule where ModuleName='" & VRefModule & "'), '" & VFrmSelected & "','" & VFormName & "','" & VCanSave & "','" & VCanSearch & "','" & VCanUpdate & "','" & VCanDelete & "','" & VRefModule & "','" & VRefSubModule & "','" & VNavigationModule & "','" & VUpdatedOn & "','" & VUpdatedBy & "' );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Clear()
                        ddlRefModule_SelectedIndexChanged(sender, e)
                        Bind()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Record Insertion Failed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then

                Dim st1 As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master  where RefModule='" & VRefModule & "'  and RefSubModule='" & VRefSubModule & "' and NavigationModule='" & VNavigationModule & "'   and not NavigationModule='" & GV.parseString(lblOldNavigationModule.Text.Trim) & "' "

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master  where RefModule='" & VRefModule & "'  and RefSubModule='" & VRefSubModule & "' and NavigationModule='" & VNavigationModule & "'    and not NavigationModule='" & GV.parseString(lblOldNavigationModule.Text.Trim) & "' ") > 0 Then
                    lblError.Text = "Navigation Module Already Exists."
                    lblError.CssClass = "errorlabels"
                Else
                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master  where Searching_Keyword='" & VsearchingKeyword & "'    and not Searching_Keyword='" & GV.parseString(lbloldSearchKeyword.Text.Trim) & "' ") > 0 Then
                        lblError.Text = "Searching KeyWords Already Exists."
                        lblError.CssClass = "errorlabels"
                        txtsearchingKeyword.Focus()
                        Exit Sub
                    End If
                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master set Searching_Keyword ='" & VsearchingKeyword & "', FrmSelected='" & VFrmSelected & "', FormName='" & VFormName & "', CanSave='" & VCanSave & "', CanSearch='" & VCanSearch & "', CanUpdate='" & VCanUpdate & "', CanDelete='" & VCanDelete & "', RefModule='" & VRefModule & "', RefSubModule='" & VRefSubModule & "', NavigationModule='" & VNavigationModule & "',NavigationModule_Order=" & VNavigationModule_Order & ", UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "' where RID=" & lblRID.Text.Trim & ";"
                    QryStr = QryStr & " " & "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster set  FormName='" & VFormName & "',NavigationModule='" & VNavigationModule & "',NavigationModule_Order='" & VNavigationModule_Order & "' where RefModule='" & VRefModule & "' and RefSubModule='" & VRefSubModule & "' and NavigationModule='" & GV.parseString(lblOldNavigationModule.Text.Trim) & "' ;"

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
        End Try
    End Sub

    Private Sub Clear()
        Try

            txtFormName.Text = ""

           

            'txtRefSubModule.Text = ""
            txtNavigationModule.Text = ""
            txtNavigationModule_Order.Text = ""
            txtsearchingKeyword.Text = ""
            lbloldSearchKeyword.Text = ""
            ddlRefSubModule.SelectedIndex = 0
            lblOldNavigationModule.Text = ""
            lblOldRefModule.Text = ""
            lblOldRefSubModule.Text = ""
            lblOldUrl.Text = ""
            lblRID.Text = ""

            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            Div_RefSubModule.Visible = False

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
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            lblOldRefSubModule.Text = ""
            ' lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text
            If Not IsDBNull(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) Then
                If Not GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) = "" Then

                    ddlRefModule.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
                    ddlRefModule_SelectedIndexChanged(sender, e)
                End If
            End If


            If Not IsDBNull(GridView1.Rows(gvrow.RowIndex).Cells(2).Text) Then
                If Not GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text) = "" Then
                    ddlRefSubModule.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
                    lblOldRefSubModule.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
                End If
            End If


            txtNavigationModule.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text.Trim)
            txtNavigationModule_Order.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text.Trim)
            txtFormName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text.Trim)
            txtsearchingKeyword.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text.Trim)
            lbloldSearchKeyword.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text.Trim)



            lblOldNavigationModule.Text = txtNavigationModule.Text
            lblOldUrl.Text = txtFormName.Text


            lblError.Text = ""
            lblSessionFlag.Text = 1
            btnSave.Text = "Update"

            btnDelete.Enabled = True

            lblError.CssClass = ""
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text
            ModalPopupExtender1.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then

                Dim str As String = GV.FL.AddInVar("RefModule+','+RefSubModule+','+NavigationModule", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master  where RID=" & lblRID.Text & "")

                Dim arr() As String = str.Split(",")
                Dim Refmodule, Refsubmodule, NavigationModule As String
                Refmodule = arr(0)
                Refsubmodule = arr(1)
                NavigationModule = arr(2)


                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & Refmodule & "' and RefSubModule='" & Refsubmodule & "' and NavigationModule='" & NavigationModule & "' ;"
                QryStr = QryStr & " " & "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where RefModule='" & Refmodule & "' and RefSubModule='" & Refsubmodule & "' and NavigationModule='" & NavigationModule & "' ;"

                GV.FL.DMLQueries(QryStr)
                Bind()
                Clear()
                lblError.Text = "Record deleted Successfully."
                lblError.CssClass = "Successlabels"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
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

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            If ddlRefModule.Items.Count > 0 Then
                ddlRefModule.SelectedIndex = 0
            End If
            ddlRefSubModule.Items.Clear()
            Clear()
            Bind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlRefModule_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRefModule.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ddlRefSubModule.Items.Clear()
            GV.FL.AddInDropDownListDistinct(ddlRefSubModule, "RefSubModule", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & ddlRefModule.SelectedValue & "' and not RefSubModule=''  order by RefSubModule")

            If ddlRefSubModule.Items.Count > 0 Then
                ddlRefSubModule.Items.Insert(0, ":::: Select Sub Module ::::")
                Div_RefSubModule.Visible = True
                lblIsSubMenu_Available.Text = "Yes"
            Else
                Div_RefSubModule.Visible = False
                lblIsSubMenu_Available.Text = "False"
            End If

            'ddlRefSubModule.Items.Insert(1, "Not Applicable")
            'ddlRefSubModule.Items.Insert(2, "Add Sub Module")

            'txtRefSubModule.Text = ""
            txtFormName.Text = ""
            txtNavigationModule.Text = ""
            'txtRefSubModule.Visible = False
            'ddlRefSubModule.SelectedValue = "Not Applicable"
            Bind()

        Catch ex As Exception
        End Try
    End Sub

    'Protected Sub ddlRefSubModule_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRefSubModule.SelectedIndexChanged
    '    Try


    '        'lblIsSubMenu_Available
    '        If Not ddlRefModule.SelectedValue = ":::: Select Module ::::" Then
    '            If GV.FL.RecCount("CRM_SubModule where ModuleName='" & GV.parseString(ddlRefModule.SelectedValue.Trim) & "' and not RefSubModule is null or RefSubModule=''") > 0 Then
    '                GV.FL.AddInDropDownListDistinct(ddlRefSubModule, "RefSubModule", "CRM_SubModule where ModuleName='" & GV.parseString(ddlRefModule.SelectedValue.Trim) & "'")
    '                ddlRefSubModule.Items.Insert(0, ":::: Select Sub Module ::::")
    '                lblIsSubMenu_Available.Text = "Yes"
    '            Else
    '                lblIsSubMenu_Available.Text = "No"
    '            End If
    '        Else
    '            lblIsSubMenu_Available.Text = "No"
    '        End If

    '        Bind()
    '    Catch ex As Exception

    '    End Try
    'End Sub

End Class