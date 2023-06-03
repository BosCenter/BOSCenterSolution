
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Web.Mail
Imports System.Net


Public Class Admin_UserRigths
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("SUPERADMIN")

    Dim VCompanyCode As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'GV.FL.AddInDropDownListDistinct(ddlGroup, "Group_Name", "NidhiSoftware_Group_Master")
                'ddlGroup.Items.Insert(0, "Select Group")
                'GV.FL.AddInListAll(lstModules, "ModuleName", "NidhiSoftware_SuperAdmin_MainModule order By OrderNo")

                GV.FL.AddInDropDownListAll(ddlCompany, "CompanyCode +':'+ CompanyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration where not CompanyCode='cmp1045' order by RID Desc")
                If ddlCompany.Items.Count > 0 Then
                    ddlCompany.Items.Insert(0, ":: Select Company ::")
                Else
                    ddlCompany.Items.Add(":: Select Company ::")
                End If


            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblError.Text = ex.Message
        End Try
    End Sub
    Dim flag As Integer = 0



    Public Sub Bind()
        Try


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub


    Dim oldval As String = ""
    Dim colorarr() As System.Drawing.Color = {Drawing.Color.LightCyan, Drawing.Color.LightYellow, Drawing.Color.LightGreen, Drawing.Color.LightPink, Drawing.Color.LightGray}
    Dim colrCount As Integer = 0
    Public Sub fillGrid(ByVal qry As String)
        Try

            GV.FL.AddInGridview(GridView1, qry)
            If GridView1.Rows.Count > 0 Then

                btnSave.Enabled = True
                btnClear.Enabled = True
                GV.FL.showSerialnoOnGridView(GridView1)

                Dim count As Integer = 0
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    If Not GV.parseString(GridView1.Rows(i).Cells(8).Text.Trim) = "" Then

                        If oldval = "" Then
                            oldval = GV.parseString(GridView1.Rows(i).Cells(8).Text.Trim)
                        End If

                        If GridView1.Rows(i).Cells(8).Text.Trim = oldval Then
                            GridView1.Rows(i).BackColor = colorarr(colrCount)
                        Else
                            oldval = GV.parseString(GridView1.Rows(i).Cells(8).Text.Trim)

                            If colrCount < colorarr.Length Then
                                colrCount += 1
                            Else
                                colrCount = 0
                            End If
                            GridView1.Rows(i).BackColor = colorarr(colrCount)

                        End If

                    Else
                        GridView1.Rows(i).BackColor = Drawing.Color.LightYellow
                    End If
                Next
            Else
                btnSave.Enabled = False
                btnClear.Enabled = False
                GridView1.DataSource = Nothing
                GridView1.DataBind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Dim x As Integer = 140

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' GV.FL.DMLQueries("NidhiSoftware_SuperAdmin_MailTemplateMaster where RID='" & txtMailID.Text & "'")
            'Bind()
            'BtnRefresh_Click(sender, e)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    'Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl.SelectedIndexChanged
    '    Try
    '        lstContacts.Items.Clear()
    '        ds = New DataSet
    '        ds = GV.FL.OpenDsWithSelectQuery("select CompanyName+' '+Mobile_No as fname from BOS_ClientRegistration where Recordstatus='Active' and ApprovedStatus='Approved'")
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Dim i As Integer
    '            For i = 0 To ds.Tables(0).Rows.Count - 1
    '                lstContacts.Items.Add(ds.Tables(0).Rows(i).Item("fname").ToString())
    '            Next
    '        End If
    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

    '    End Try
    'End Sub

    Protected Sub lstModules_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstModules.SelectedIndexChanged
        Try
            lblGroupError.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""
            lblDatabaseName.Text = ""

            If ddlCompany.SelectedIndex = 0 Then
                btnSave.Enabled = False
                btnClear.Enabled = False
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                lblGroupError.Text = "Please Select Company"
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                btnSave.Enabled = False
                btnClear.Enabled = False
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                lblGroupError.Text = "Please Select Group"
                Exit Sub
            End If

            Dim arr() As String = ddlCompany.SelectedValue.Split(":")
            VCompanyCode = arr(0)

            lblDatabaseName.Text = GV.FL.AddInVar("DatabaseName", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & VCompanyCode & "'")
            If Not lblDatabaseName.Text.Trim = "" Then
                fillGrid("" & GV.DefaultDatabase.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & ddlGroup.SelectedValue & "' and  RefModule='" & lstModules.SelectedValue & "' and FrmSelected='1'  order by OrderNo,RefModule  asc")
            End If


            If GV.FL.RecCount(" " & lblDatabaseName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & ddlGroup.SelectedValue & "'") > 0 Then

                SelectNone(3, "chkSave")
                SelectNone(4, "chkSearch")
                SelectNone(5, "chkUpdate")
                SelectNone(6, "chkDelete")
                SelectNone(1, "chkSelect")

                Dim LocalDS As New DataSet
                LocalDS = GV.FL.OpenDs(" " & lblDatabaseName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & ddlGroup.SelectedValue & "' and RefModule='" & lstModules.SelectedValue & "' and FrmSelected=1  order by rid asc")
                Dim chk As CheckBox
                For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                    For j As Integer = 0 To GridView1.Rows.Count - 1

                        If (GV.parseString(LocalDS.Tables(0).Rows(i).Item("NavigationModule").ToString.Trim).ToUpper() = GV.parseString(GridView1.Rows(j).Cells(2).Text.Trim).ToUpper()) And (GV.parseString(LocalDS.Tables(0).Rows(i).Item("RefSubModule").ToString.Trim).ToUpper() = GV.parseString(GridView1.Rows(j).Cells(8).Text.Trim).ToUpper()) Then
                            chk = GridView1.Rows(j).Cells(1).FindControl("chkSelect")
                            chk.Checked = True

                            chk = GridView1.Rows(j).Cells(3).FindControl("chkSave")
                            chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanSave")

                            chk = GridView1.Rows(j).Cells(4).FindControl("chkSearch")
                            chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanSearch")

                            chk = GridView1.Rows(j).Cells(5).FindControl("chkUpdate")
                            chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanUpdate")

                            chk = GridView1.Rows(j).Cells(6).FindControl("chkDelete")
                            chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanDelete")
                            'Exit For
                        End If

                    Next
                Next
            Else
                If Not lstModules.SelectedItem Is Nothing Then
                    fillGrid("" & GV.DefaultDatabase.Trim & ".dbo.CRM_UserRightsMaster where ModuleName='" & lstModules.SelectedValue & "'  order by OrderNo,ModuleName  asc")
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub SelectAll(ByVal cellIndex As Integer, ByVal controlID As String)
        Try

            If cellIndex = 1 Then
                Dim chk As CheckBox
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                    chk.Checked = True
                Next
            Else
                Dim chk As CheckBox
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    If GridView1.Rows(i).Cells(2).Text.Trim = GridView1.Rows(i).Cells(8).Text.Trim Then
                        chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                        chk.Checked = False
                    Else
                        chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                        chk.Checked = True
                    End If
                Next
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub SelectNone(ByVal cellIndex As Integer, ByVal controlID As String)
        Try
            Dim chk As CheckBox

            For i As Integer = 0 To GridView1.Rows.Count - 1
                chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                chk.Checked = False
            Next
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If e.CommandName = "btnSaveAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(3).FindControl("SaveAll")

                If btn.Text = "ALL" Then
                    btn.Text = "NONE"
                    SelectAll(3, "chkSave")
                ElseIf btn.Text = "NONE" Then
                    btn.Text = "ALL"
                    SelectNone(3, "chkSave")
                End If
            ElseIf e.CommandName = "btnSearchAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(4).FindControl("SearchAll")

                If btn.Text = "ALL" Then
                    btn.Text = "NONE"
                    SelectAll(4, "chkSearch")
                ElseIf btn.Text = "NONE" Then
                    btn.Text = "ALL"
                    SelectNone(4, "chkSearch")
                End If
            ElseIf e.CommandName = "btnUpdateAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(5).FindControl("UpdateAll")

                If btn.Text = "ALL" Then
                    btn.Text = "NONE"
                    SelectAll(5, "chkUpdate")
                ElseIf btn.Text = "NONE" Then
                    btn.Text = "ALL"
                    SelectNone(5, "chkUpdate")
                End If
            ElseIf e.CommandName = "btnDeleteAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(6).FindControl("DeleteAll")

                If btn.Text = "ALL" Then
                    btn.Text = "NONE"
                    SelectAll(6, "chkDelete")
                ElseIf btn.Text = "NONE" Then
                    btn.Text = "ALL"
                    SelectNone(6, "chkDelete")
                End If
            ElseIf e.CommandName = "btnfrmselectAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(1).FindControl("frmselectAll")

                If btn.Text = "ALL" Then
                    btn.Text = "NONE"
                    SelectAll(1, "chkSelect")
                ElseIf btn.Text = "NONE" Then
                    btn.Text = "ALL"
                    SelectNone(1, "chkSelect")
                End If
            End If
            'frmselect


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub clear()
        Try
            lblGroupError.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""

            btnSave.Enabled = False
            btnClear.Enabled = False

            If ddlCompany.Items.Count > 0 Then
                ddlCompany.SelectedIndex = 0
            End If

            lstModules.SelectedIndex = -1

            GridView1.DataSource = Nothing
            GridView1.DataBind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            lblSavingDialogBox.CssClass = ""
            lblSavingDialogBox.Text = "Are You Sure You Want to Save ?"
            btnsaveOk.Text = "Yes"
            btnsaveOk.Visible = True
            BtnSaveCancel.Attributes("style") = ""
            BtnSaveCancel.Text = "No"
            ModalPopupExtender2.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        Try
            lblGroupError.Text = ""
            lblError.CssClass = ""
            ddlGroup.SelectedIndex = 0
            GridView1.DataSource = Nothing
            GridView1.DataBind()

            lstModules.Items.Clear()



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnsaveOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsaveOk.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            Dim canSave, canSearch, canUpdate, CanDelete, Canselect As Boolean
            Dim UserGroup, navigateurl, RefModule, RefSubModule, RefNavigationModule, MenuText, VSearching_Keyword As String
            Dim updatedby As String = ""
            'Dim userid() As String = ddlGroup.SelectedItem.Value.ToString().Split("-")
            Dim chk As CheckBox
            Dim Qry As String = ""


            If ddlCompany.SelectedIndex = 0 Then
                btnSave.Enabled = False
                btnClear.Enabled = False
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                lblGroupError.Text = "Please Select Company"
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                btnSave.Enabled = False
                btnClear.Enabled = False
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                lblGroupError.Text = "Please Select Group"
                Exit Sub
            End If


            If Not lstModules.SelectedItem Is Nothing Then


                Dim arr() As String = ddlCompany.SelectedValue.Split(":")
                VCompanyCode = arr(0)


                If Not lblDatabaseName.Text.Trim = "" Then
                    Qry = "Delete from  " & lblDatabaseName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & ddlGroup.SelectedValue & "' and RefModule='" & lstModules.SelectedValue & "';"
                    GV.FL.DMLQueries(Qry)
                End If


                Qry = ""

                lblError.Text = ""

                For i As Integer = 0 To GridView1.Rows.Count - 1

                    chk = GridView1.Rows(i).Cells(1).FindControl("chkSelect")
                    If (chk.Checked = True) Then
                        Canselect = True
                    Else
                        Canselect = False
                    End If

                    chk = GridView1.Rows(i).Cells(3).FindControl("chkSave")
                    If (chk.Checked = True) Then
                        canSave = True
                    Else
                        canSave = False
                    End If


                    chk = GridView1.Rows(i).Cells(4).FindControl("chkSearch")
                    If (chk.Checked = True) Then
                        canSearch = True
                    Else
                        canSearch = False
                    End If

                    chk = GridView1.Rows(i).Cells(5).FindControl("chkUpdate")
                    If (chk.Checked = True) Then
                        canUpdate = True
                    Else
                        canUpdate = False
                    End If


                    chk = GridView1.Rows(i).Cells(6).FindControl("chkDelete")
                    If (chk.Checked = True) Then
                        CanDelete = True
                    Else
                        CanDelete = False
                    End If


                    RefModule = GV.parseString(GridView1.Rows(i).Cells(7).Text.Trim)
                    RefSubModule = GV.parseString(GridView1.Rows(i).Cells(8).Text.Trim)
                    RefNavigationModule = GV.parseString(GridView1.Rows(i).Cells(2).Text.Trim)
                    navigateurl = GV.parseString(GridView1.Rows(i).Cells(9).Text.Trim)
                    VSearching_Keyword = GV.parseString(GridView1.Rows(i).Cells(10).Text.Trim)
                    updatedby = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    Dim VRefSubModule_Order, VRefNavigationModule_Order, VCreateLink As String


                    VRefSubModule_Order = " ( select distinct RefSubModule_Order from " & GV.DefaultDatabase.Trim & ".dbo.CRM_SubModule where ModuleName='" & RefModule & "' and  RefSubModule='" & RefSubModule & "')"
                    VCreateLink = " ( select distinct CreateLink  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_SubModule where ModuleName='" & RefModule & "' and  RefSubModule='" & RefSubModule & "')"
                    VRefNavigationModule_Order = " ( select distinct NavigationModule_Order from " & GV.DefaultDatabase.Trim & ".dbo.CRM_Module_Master where RefModule='" & RefModule & "' and  RefSubModule='" & RefSubModule & "' and NavigationModule='" & RefNavigationModule & "' )"

                    If Qry.Trim = "" Then
                        Qry = "insert into " & lblDatabaseName.Text.Trim & ".dbo.CRM_UserRightsMaster(CreateLink,User_Group,OrderNO,RefSubModule_Order,NavigationModule_Order,CompanyCode,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy,Searching_Keyword) values( " & VCreateLink & ",'" & ddlGroup.SelectedValue & "',(select orderno from " & GV.DefaultDatabase.Trim & ".dbo.CRM_MainModule where ModuleName='" & RefModule & "')," & VRefSubModule_Order & "," & VRefNavigationModule_Order & ",'" & VCompanyCode & "','" & Canselect & "','" & navigateurl & "','" & canSave & "','" & canSearch & "','" & canUpdate & "','" & CanDelete & "','" & RefModule & "','" & RefSubModule & "','" & RefNavigationModule & "',getDate(),'" & updatedby & "','" & VSearching_Keyword & "') ;"
                    Else
                        Qry = Qry & " " & "insert into " & lblDatabaseName.Text.Trim & ".dbo.CRM_UserRightsMaster(CreateLink,User_Group,OrderNO,RefSubModule_Order,NavigationModule_Order,CompanyCode,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy,Searching_Keyword) values(" & VCreateLink & ",'" & ddlGroup.SelectedValue & "',(select orderno from " & GV.DefaultDatabase.Trim & ".dbo.CRM_MainModule where ModuleName='" & RefModule & "')," & VRefSubModule_Order & "," & VRefNavigationModule_Order & ",'" & VCompanyCode & "','" & Canselect & "','" & navigateurl & "','" & canSave & "','" & canSearch & "','" & canUpdate & "','" & CanDelete & "','" & RefModule & "','" & RefSubModule & "','" & RefNavigationModule & "',getDate(),'" & updatedby & "','" & VSearching_Keyword & "') ;"
                    End If

                Next

                If Not Qry.Trim = "" Then
                    If GV.FL.DMLQueriesBulk(Qry) = True Then
                        btnsaveOk.Text = "Ok"
                        btnsaveOk.Visible = False
                        'BtnSaveCancel.Attributes("style") = "display:none"
                        BtnSaveCancel.Text = "Ok"
                        lblSavingDialogBox.Text = "!!!! Rights Applied Successfully !!!!"
                        lblSavingDialogBox.CssClass = "Successlabels"
                    Else
                        lblSavingDialogBox.Text = "!!!! Rights Applying Failed !!!!"
                        lblSavingDialogBox.CssClass = "Errorlabels"
                    End If
                End If
                ModalPopupExtender2.Show()

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        Try
            lblGroupError.Text = ""
            lblError.CssClass = ""

            lstModules.SelectedIndex = -1
            GridView1.DataSource = Nothing
            GridView1.DataBind()

            lstModules.Items.Clear()

            If Not ddlGroup.SelectedIndex = 0 Then
                Dim arr() As String = ddlCompany.SelectedValue.Split(":")
                VCompanyCode = arr(0)

                Dim LocalDS As New DataSet
                'LocalDS = GV.FL.OpenDsWithSelectQuery("select  distinct RefModule,OrderNo from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='Admin' order by OrderNo asc")

                Dim str As String = "select  distinct RefModule,OrderNo from " & GV.DefaultDatabase.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & ddlGroup.SelectedValue & "' and FrmSelected='1' order by OrderNo asc"
                LocalDS = GV.FL.OpenDsWithSelectQuery("select  distinct RefModule,OrderNo from " & GV.DefaultDatabase.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & ddlGroup.SelectedValue & "' and FrmSelected='1' order by OrderNo asc")


                For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                    lstModules.Items.Add(LocalDS.Tables(0).Rows(i).Item("RefModule").ToString.Trim)
                Next

                ''select  distinct RefModule,OrderNo from CRM_UserRightsMaster where User_Group='Admin' order by OrderNo asc





                'If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where CompanyCode='" & VCompanyCode & "'") > 0 Then

                '    SelectNone(3, "chkSave")
                '    SelectNone(4, "chkSearch")
                '    SelectNone(5, "chkUpdate")
                '    SelectNone(6, "chkDelete")
                '    SelectNone(1, "chkSelect")

                '    LocalDS = New DataSet
                '    LocalDS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where CompanyCode='" & VCompanyCode & "' and FrmSelected=1  order by rid asc")
                '    Dim chk As CheckBox
                '    For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                '        For j As Integer = 0 To GridView1.Rows.Count - 1

                '            If GV.parseString(LocalDS.Tables(0).Rows(i).Item("NavigationModule").ToString.Trim) = GV.parseString(GridView1.Rows(j).Cells(9).Text.Trim) Then
                '                chk = GridView1.Rows(j).Cells(1).FindControl("chkSelect")
                '                chk.Checked = True

                '                chk = GridView1.Rows(j).Cells(3).FindControl("chkSave")
                '                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanSave")

                '                chk = GridView1.Rows(j).Cells(4).FindControl("chkSearch")
                '                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanSearch")

                '                chk = GridView1.Rows(j).Cells(5).FindControl("chkUpdate")
                '                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanUpdate")

                '                chk = GridView1.Rows(j).Cells(6).FindControl("chkDelete")
                '                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanDelete")
                '                Exit For
                '            End If

                '        Next
                '    Next
                'Else

                '    If Not lstModules.SelectedItem Is Nothing Then
                '        fillGrid("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where RefModule='" & lstModules.SelectedValue & "'  order by OrderNo,RefSubModule  asc")
                '    End If
                'End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class