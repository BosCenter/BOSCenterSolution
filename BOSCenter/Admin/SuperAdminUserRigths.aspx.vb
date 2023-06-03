
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Web.Mail
Imports System.Net


Public Class SuperAdminUserRigths
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Dim flag As Integer = 0
    Dim x As Integer = 140
    Dim oldval As String = ""
    Dim colorarr() As System.Drawing.Color = {Drawing.Color.LightCyan, Drawing.Color.LightYellow, Drawing.Color.LightGreen, Drawing.Color.LightPink, Drawing.Color.LightGray}
    Dim colrCount As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim Filter As String = ""
                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Super Admin" Then
                    GV.FL.AddInDropDownListDistinct(ddlGroup, "Group_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master " & Filter & "")
                    ddlGroup.Items.Insert(0, "Select Group")
                    ddlGroup.Items.Insert(1, "Master Distributor")
                    ddlGroup.Items.Insert(2, "Distributor")
                    ddlGroup.Items.Insert(3, "Retailer")
                    ddlGroup.Items.Insert(4, "Customer")
                Else
                    Filter = " where not Group_Name='Super Admin' "
                    GV.FL.AddInDropDownListDistinct(ddlGroup, "Group_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Group_Master " & Filter & "")
                    ddlGroup.Items.Insert(0, "Select Group")
                End If
                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Super Admin" Then
                    GV.FL.AddInListAll(lstModules, "ModuleName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule order By OrderNo")
                Else
                    Dim ds As New DataSet
                    Dim qry As String = "select distinct RefModule,OrderNo from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster  where  User_Group='" & GV.get_SuperAdmin_SessionVariables("Group", Request, Response) & "' and  FrmSelected=1  order By OrderNo"
                    ds = GV.FL.OpenDsWithSelectQuery(qry)
                    If Not ds Is Nothing Then
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                lstModules.DataSource = ds.Tables(0)
                                lstModules.DataValueField = "RefModule"
                                lstModules.DataBind()
                            End If
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblError.Text = ex.Message
        End Try
    End Sub

    Public Sub fillGrid(ByVal qry As String)
        Try
            If Session("Mod") = "Yes" Then
                ds = GV.FL.OpenDsWithSelectQuery(qry)
                GridView1.DataSource = ds
                GridView1.DataBind()
            Else
                GV.FL.AddInGridview(GridView1, qry)
            End If
            'GV.FL.AddInGridview(GridView1, qry)
            If GridView1.Rows.Count > 0 Then

                btnSave.Enabled = True
                btnClear.Enabled = True
                GV.FL.showSerialnoOnGridView(GridView1, 0)

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

                            If colrCount < colorarr.Length - 1 Then
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

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' GV.FL.DMLQueries("CRM_MailTemplateMaster where RID='" & txtMailID.Text & "'")
            'Bind()
            'BtnRefresh_Click(sender, e)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub lstModules_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstModules.SelectedIndexChanged
        Try
            lblGroupError.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""
            Dim Filter As String = ""
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Super Admin" Then
            Else
                Filter = "and not RefModule='Settings' "
            End If
            If Not ddlGroup.SelectedValue = "Select Group" Then
                If Not GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Super Admin" Then
                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule  where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES'") > 0 Then
                        Dim AA As String = GV.FL.AddInVar("CreateLink", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule where ModuleName='" & lstModules.SelectedValue & "'")
                        If AA.ToUpper = "Yes".ToUpper Then
                            Session("Mod") = "Yes"
                            'fillGrid("select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,'' as RefSubModule,ModuleName as NavigationModule,Searching_Keyword as 'SearchKeyword',CreateLink from CRM_MainModule where ModuleName='" & lstModules.SelectedValue & "'  order by OrderNo asc")
                            fillGrid("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='Admin' and  RefModule='" & lstModules.SelectedValue & "'  and FrmSelected=1  order by OrderNo,RefSubModule  asc")
                            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "'") > 0 Then

                                SelectNone(3, "chkSave")
                                SelectNone(4, "chkSearch")
                                SelectNone(5, "chkUpdate")
                                SelectNone(6, "chkDelete")
                                SelectNone(1, "chkSelect")

                                Dim LocalDS As New DataSet
                                LocalDS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "' and RefModule='" & lstModules.SelectedValue & "' and FrmSelected=1  order by rid asc")
                                Dim chk As CheckBox
                                For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                                    For j As Integer = 0 To GridView1.Rows.Count - 1

                                        If GV.parseString(LocalDS.Tables(0).Rows(i).Item("RefModule").ToString.Trim) = GV.parseString(GridView1.Rows(j).Cells(2).Text.Trim) And (GV.parseString(LocalDS.Tables(0).Rows(i).Item("RefSubModule").ToString.Trim).ToUpper() = GV.parseString(GridView1.Rows(j).Cells(8).Text.Trim).ToUpper()) Then
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
                                            Exit For
                                        End If

                                    Next
                                Next
                            Else

                                If Not lstModules.SelectedItem Is Nothing Then
                                    Session("Mod") = "NO"
                                    '' fillGrid("CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "'  order by OrderNo,RefSubModule  asc")
                                    'fillGrid(" (select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,RefSubModule,'' as NavigationModule,Searching_Keyword as 'SearchKeyword',CreateLink from CRM_SubModule where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES') union All (select RID,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete ,RefModule ,RefSubModule, NavigationModule,Searching_Keyword as SearchKeyword,CreateLink from CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "') ")
                                    fillGrid("select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='Admin' and  RefModule='" & lstModules.SelectedValue & "'  and FrmSelected=1  order by OrderNo,RefSubModule  asc")
                                End If
                            End If

                        End If
                    Else

                        'Dim qry As String = " (select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,RefSubModule,'' as NavigationModule,Searching_Keyword as 'SearchKeyword',CreateLink from CRM_SubModule where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES') union All (select RID,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete ,RefModule ,RefSubModule, NavigationModule,Searching_Keyword as SearchKeyword,CreateLink from CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "')"
                        Session("Mod") = "Yes"
                        'fillGrid(qry)
                        fillGrid(" select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='Admin' and  RefModule='" & lstModules.SelectedValue & "'  and FrmSelected=1  order by OrderNo,RefSubModule  asc")
                        If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "'  ") > 0 Then
                            SelectNone(3, "chkSave")
                            SelectNone(4, "chkSearch")
                            SelectNone(5, "chkUpdate")
                            SelectNone(6, "chkDelete")
                            SelectNone(1, "chkSelect")

                            Dim LocalDS As New DataSet
                            LocalDS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "' and RefModule='" & lstModules.SelectedValue & "' and FrmSelected=1  order by rid asc")
                            Dim chk As CheckBox
                            For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                                For j As Integer = 0 To GridView1.Rows.Count - 1

                                    If GV.parseString(LocalDS.Tables(0).Rows(i).Item("NavigationModule").ToString.Trim) = GV.parseString(GridView1.Rows(j).Cells(2).Text.Trim) And (GV.parseString(LocalDS.Tables(0).Rows(i).Item("RefSubModule").ToString.Trim).ToUpper() = GV.parseString(GridView1.Rows(j).Cells(8).Text.Trim).ToUpper()) Then
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
                                        Exit For
                                    End If
                                Next
                            Next
                        Else

                            If Not lstModules.SelectedItem Is Nothing Then
                                Session("Mod") = "NO"
                                fillGrid(" select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='Admin' and  RefModule='" & lstModules.SelectedValue & "'  and FrmSelected=1  order by OrderNo,RefSubModule  asc")
                                'fillGrid("(select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,RefSubModule,'' as NavigationModule,Searching_Keyword as 'SearchKeyword',CreateLink from CRM_SubModule where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES') union All (select RID,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete ,RefModule ,RefSubModule, NavigationModule,Searching_Keyword as SearchKeyword,CreateLink from CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "')")
                            End If

                        End If

                    End If
                Else



                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule  where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES'") > 0 Then
                        Dim AA As String = GV.FL.AddInVar("CreateLink", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule where ModuleName='" & lstModules.SelectedValue & "'")
                        If AA.ToUpper = "Yes".ToUpper Then
                            Session("Mod") = "Yes"
                            fillGrid("select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,'' as RefSubModule,ModuleName as NavigationModule,Searching_Keyword,CreateLink from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_MainModule where ModuleName='" & lstModules.SelectedValue & "'  order by OrderNo asc")

                            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "'") > 0 Then

                                SelectNone(3, "chkSave")
                                SelectNone(4, "chkSearch")
                                SelectNone(5, "chkUpdate")
                                SelectNone(6, "chkDelete")
                                SelectNone(1, "chkSelect")

                                Dim LocalDS As New DataSet
                                LocalDS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "' and RefModule='" & lstModules.SelectedValue & "' and FrmSelected=1  order by rid asc")
                                Dim chk As CheckBox
                                For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                                    For j As Integer = 0 To GridView1.Rows.Count - 1

                                        If GV.parseString(LocalDS.Tables(0).Rows(i).Item("RefModule").ToString.Trim) = GV.parseString(GridView1.Rows(j).Cells(2).Text.Trim) And (GV.parseString(LocalDS.Tables(0).Rows(i).Item("RefSubModule").ToString.Trim).ToUpper() = GV.parseString(GridView1.Rows(j).Cells(8).Text.Trim).ToUpper()) Then

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


                                            Exit For

                                        End If

                                    Next
                                Next
                            Else

                                If Not lstModules.SelectedItem Is Nothing Then
                                    Session("Mod") = "NO"
                                    '' fillGrid("CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "'  order by OrderNo,RefSubModule  asc")
                                    fillGrid(" (select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,RefSubModule,'' as NavigationModule,Searching_Keyword,CreateLink from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES') union All (select RID,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete ,RefModule ,RefSubModule, NavigationModule,Searching_Keyword as SearchKeyword,CreateLink from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "') ")

                                End If
                            End If

                        End If
                    Else

                        Dim qry As String = " (select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,RefSubModule,'' as NavigationModule,Searching_Keyword ,CreateLink from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES') union All (select RID,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete ,RefModule ,RefSubModule, NavigationModule,Searching_Keyword as SearchKeyword,CreateLink from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "')"
                        Session("Mod") = "Yes"
                        fillGrid(qry)

                        If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "'  ") > 0 Then
                            SelectNone(3, "chkSave")
                            SelectNone(4, "chkSearch")
                            SelectNone(5, "chkUpdate")
                            SelectNone(6, "chkDelete")
                            SelectNone(1, "chkSelect")

                            Dim LocalDS As New DataSet
                            LocalDS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "' and RefModule='" & lstModules.SelectedValue & "' and FrmSelected=1  order by rid asc")
                            Dim chk As CheckBox
                            For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                                For j As Integer = 0 To GridView1.Rows.Count - 1

                                    If GV.parseString(LocalDS.Tables(0).Rows(i).Item("NavigationModule").ToString.Trim) = GV.parseString(GridView1.Rows(j).Cells(2).Text.Trim) And (GV.parseString(LocalDS.Tables(0).Rows(i).Item("RefSubModule").ToString.Trim).ToUpper() = GV.parseString(GridView1.Rows(j).Cells(8).Text.Trim).ToUpper()) Then
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
                                        Exit For
                                    End If
                                Next
                            Next
                        Else

                            If Not lstModules.SelectedItem Is Nothing Then
                                Session("Mod") = "NO"
                                fillGrid("(select RID,'0' as FrmSelected,FormName,'0' as CanSave,'0' as CanSearch,'0' as CanUpdate,'0' as CanDelete ,ModuleName as RefModule ,RefSubModule,'' as NavigationModule,Searching_Keyword ,CreateLink from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & lstModules.SelectedValue & "' and CreateLink='YES') union All (select RID,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete ,RefModule ,RefSubModule, NavigationModule,Searching_Keyword as SearchKeyword,CreateLink from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "')")
                            End If

                        End If

                    End If



                End If

            Else
                btnSave.Enabled = False
                btnClear.Enabled = False
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                lblGroupError.Text = "Please Select Group"
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

            If ddlGroup.Items.Count > 0 Then
                ddlGroup.SelectedIndex = 0
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

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        Try
            lblGroupError.Text = ""
            lblError.CssClass = ""

            lstModules.SelectedIndex = -1
            GridView1.DataSource = Nothing
            GridView1.DataBind()

            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "'") > 0 Then

                SelectNone(3, "chkSave")
                SelectNone(4, "chkSearch")
                SelectNone(5, "chkUpdate")
                SelectNone(6, "chkDelete")
                SelectNone(1, "chkSelect")

                Dim LocalDS As New DataSet
                LocalDS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & GV.parseString(ddlGroup.SelectedValue) & "' and FrmSelected=1  order by rid asc")
                Dim chk As CheckBox
                For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                    For j As Integer = 0 To GridView1.Rows.Count - 1

                        If GV.parseString(LocalDS.Tables(0).Rows(i).Item("NavigationModule").ToString.Trim) = GV.parseString(GridView1.Rows(j).Cells(9).Text.Trim) Then
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
                            Exit For
                        End If

                    Next
                Next
            Else
                If Not lstModules.SelectedItem Is Nothing Then
                    fillGrid("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & lstModules.SelectedValue & "'  order by OrderNo,RefSubModule  asc")
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnsaveOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsaveOk.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            Dim canSave, canSearch, canUpdate, CanDelete, Canselect As Boolean
            Dim UserGroup, navigateurl, RefModule, RefSubModule, RefNavigationModule, MenuText, Searching_Keyword As String
            Dim updatedby As String = ""
            'Dim userid() As String = ddlGroup.SelectedItem.Value.ToString().Split("-")

            Dim Qry As String = ""
            Dim chk As CheckBox
            UserGroup = ddlGroup.SelectedItem.Value
            If UserGroup = "Select Group" Then
                lblGroupError.Text = "Please Select Group"
                Exit Sub
            Else
                lblGroupError.Text = ""
            End If


            If Not lstModules.SelectedItem Is Nothing Then

                GV.FL.DMLQueries("Delete from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and RefModule='" & lstModules.SelectedValue & "'")

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
                    Searching_Keyword = GV.parseString(GridView1.Rows(i).Cells(10).Text.Trim)

                    Dim lblCreateLink As Label = DirectCast(GridView1.Rows(i).FindControl("lblCreateLink"), Label)
                    Dim VRefSubModule_Order, VRefNavigationModule_Order As String

                    VRefSubModule_Order = " ( select distinct RefSubModule_Order from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_SubModule where ModuleName='" & RefModule & "' and  RefSubModule='" & RefSubModule & "')"
                    VRefNavigationModule_Order = " ( select distinct NavigationModule_Order from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Module_Master where RefModule='" & RefModule & "' and  RefSubModule='" & RefSubModule & "' and NavigationModule='" & RefNavigationModule & "' )"


                    updatedby = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

                    'If Canselect = True Then
                    If Qry.Trim = "" Then
                        Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster(Searching_Keyword,CreateLink,OrderNO,RefSubModule_Order,NavigationModule_Order,User_Group,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy) values('" & Searching_Keyword & "','" & lblCreateLink.Text.Trim & "',(select orderno from CRM_MainModule where ModuleName='" & RefModule & "')," & VRefSubModule_Order & "," & VRefNavigationModule_Order & ",'" & UserGroup & "','" & Canselect & "','" & navigateurl & "','" & canSave & "','" & canSearch & "','" & canUpdate & "','" & CanDelete & "','" & RefModule & "','" & RefSubModule & "','" & RefNavigationModule & "',getDate(),'" & updatedby & "') ;"
                    Else
                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_UserRightsMaster(Searching_Keyword,CreateLink,OrderNO,RefSubModule_Order,NavigationModule_Order,User_Group,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy) values('" & Searching_Keyword & "','" & lblCreateLink.Text.Trim & "',(select orderno from CRM_MainModule where ModuleName='" & RefModule & "')," & VRefSubModule_Order & "," & VRefNavigationModule_Order & ",'" & UserGroup & "','" & Canselect & "','" & navigateurl & "','" & canSave & "','" & canSearch & "','" & canUpdate & "','" & CanDelete & "','" & RefModule & "','" & RefSubModule & "','" & RefNavigationModule & "',getDate(),'" & updatedby & "') ;"
                    End If
                    'End If
                Next

                If Not Qry.Trim = "" Then
                    If GV.FL.DMLQueries(Qry) = True Then
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




End Class