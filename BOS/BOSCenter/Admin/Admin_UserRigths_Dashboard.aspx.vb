
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Web.Mail
Imports System.Net


Public Class Admin_UserRigths_Dashboard
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("SUPERADMIN")

    Dim VCompanyCode As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'GV.FL.AddInDropDownListDistinct(ddlGroup, "Group_Name", "NidhiSoftware_Group_Master")
                'ddlGroup.Items.Insert(0, "Select Group")
                'GV.FL.AddInListAll(lstModules, "ModuleName", "NidhiSoftware_SuperAdmin_MainModule order By OrderNo")

                GV.FL.AddInDropDownListAll(ddlCompany, "CompanyCode +':'+ CompanyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration order by RID Desc")
                If ddlCompany.Items.Count > 0 Then
                    ddlCompany.Items.Insert(0, ":: Select Company ::")
                Else
                    ddlCompany.Items.Add(":: Select Company ::")
                End If


            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Dim flag As Integer = 0



    Public Sub Bind()
        Try


        Catch ex As Exception
        End Try

    End Sub


    Dim oldval As String = ""
    Dim colorarr() As System.Drawing.Color = {Drawing.Color.LightCyan, Drawing.Color.LightYellow, Drawing.Color.LightGreen, Drawing.Color.LightPink, Drawing.Color.LightGray}
    Dim colrCount As Integer = 0
    Public Sub fillGrid(ByVal qry As String)
        Try
            If Session("Dyanamic") = "No" Then
                Dim StrDS As New DataSet
                StrDS = GV.FL.OpenDsWithSelectQuery(qry)
                GridView1.DataSource = StrDS
                GridView1.DataBind()
            Else
                GV.FL.AddInGridview(GridView1, qry)

            End If
            

            If GridView1.Rows.Count > 0 Then

                btnSave.Enabled = True
                btnClear.Enabled = True
                GV.FL.showSerialnoOnGridView(GridView1)

                Dim count As Integer = 0
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    If Not GV.parseString(GridView1.Rows(i).Cells(5).Text.Trim) = "" Then

                        If oldval = "" Then
                            oldval = GV.parseString(GridView1.Rows(i).Cells(5).Text.Trim)
                        End If

                        If GridView1.Rows(i).Cells(5).Text.Trim = oldval Then
                            GridView1.Rows(i).BackColor = colorarr(colrCount)
                        Else
                            oldval = GV.parseString(GridView1.Rows(i).Cells(5).Text.Trim)

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

        End Try
    End Sub


    Dim x As Integer = 140

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
           
        Catch ex As Exception

        End Try
    End Sub

    

    

    Public Sub SelectAll(ByVal cellIndex As Integer, ByVal controlID As String)
        Try
            Dim chk As CheckBox
            For i As Integer = 0 To GridView1.Rows.Count - 1
                chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                chk.Checked = True
            Next

            'If cellIndex = 1 Then

            'Else
            '    Dim chk As CheckBox
            '    For i As Integer = 0 To GridView1.Rows.Count - 1
            '        chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
            '        chk.Checked = True
            '        'If GridView1.Rows(i).Cells(2).Text.Trim = GridView1.Rows(i).Cells(3).Text.Trim Then
            '        '    chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
            '        '    chk.Checked = False
            '        'Else

            '        'End If
            '    Next
            'End If
        Catch ex As Exception

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
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If e.CommandName = "btnSaveAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(1).FindControl("SaveAll")

                If btn.Text = "ALL" Then
                    btn.Text = "NONE"
                    SelectAll(1, "chkSave")
                ElseIf btn.Text = "NONE" Then
                    btn.Text = "ALL"
                    SelectNone(1, "chkSave")
                End If
            ElseIf e.CommandName = "btnSearchAll" Then
                Dim btn As Button
                btn = GridView1.HeaderRow.Cells(2).FindControl("SearchAll")

                If btn.Text = "ALL" Then
                    btn.Text = "NONE"
                    SelectAll(2, "chkSearch")
                ElseIf btn.Text = "NONE" Then
                    btn.Text = "ALL"
                    SelectNone(2, "chkSearch")
                End If
           
            End If
            'frmselect


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            clear()
        Catch ex As Exception

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



            GridView1.DataSource = Nothing
            GridView1.DataBind()
        Catch ex As Exception

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

        End Try
    End Sub



    Protected Sub ddlCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCompany.SelectedIndexChanged
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

            'If ddlGroup.SelectedIndex = 0 Then
            '    btnSave.Enabled = False
            '    btnClear.Enabled = False
            '    GridView1.DataSource = Nothing
            '    GridView1.DataBind()
            '    lblGroupError.Text = "Please Select Group"
            '    Exit Sub
            'End If

            Dim arr() As String = ddlCompany.SelectedValue.Split(":")
            VCompanyCode = arr(0)

            lblDatabaseName.Text = GV.FL.AddInVar("DatabaseName", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & VCompanyCode & "'")
            If Not lblDatabaseName.Text.Trim = "" Then
                Session("Dyanamic") = "No"
                fillGrid("Select *,'0' as CanshowServices, '0' as CanshowButton,'0' as CanshowServices_Cust, '0' as CanshowButton_Cust from " & GV.DefaultDatabase.Trim & ".dbo.CRM_DyanamicServices_Dashboard   order by OrderNo  asc")

                'Session("Dyanamic") = "Yes"
                'fillGrid(" " & lblDatabaseName.Text & ".dbo.CRM_DyanamicServices_Rights_Dashboard where CompanyCode='" & VCompanyCode & "'  order by OrderNo  asc")
            End If

            If GV.FL.RecCount(" " & lblDatabaseName.Text.Trim & ".dbo.CRM_DyanamicServices_Rights_Dashboard where CompanyCode='" & VCompanyCode & "'") > 0 Then
                Session("Dyanamic") = "Yes"

                SelectNone(1, "chkSave")
                SelectNone(2, "chkSearch")
                SelectNone(3, "chkSave_Cust")
                SelectNone(4, "chkSearch_Cust")

                Dim LocalDS As New DataSet
                LocalDS = GV.FL.OpenDs(" " & lblDatabaseName.Text.Trim & ".dbo.CRM_DyanamicServices_Rights_Dashboard where CompanyCode='" & VCompanyCode & "' and  (CanshowServices='1' or  CanshowServices_Cust='1' or CanshowButton='1' or CanshowButton_Cust='1')  order by OrderNo asc")
                Dim chk As CheckBox
                For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                    For j As Integer = 0 To GridView1.Rows.Count - 1

                        If (GV.parseString(LocalDS.Tables(0).Rows(i).Item("ServiceName").ToString.Trim).ToUpper() = GV.parseString(GridView1.Rows(j).Cells(5).Text.Trim).ToUpper()) Then
                            chk = GridView1.Rows(j).Cells(1).FindControl("chkSave")
                            If Not IsDBNull(LocalDS.Tables(0).Rows(i).Item("CanshowServices")) Then
                                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanshowServices")
                            End If


                            chk = GridView1.Rows(j).Cells(2).FindControl("chkSearch")
                            If Not IsDBNull(LocalDS.Tables(0).Rows(i).Item("CanshowButton")) Then
                                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanshowButton")
                            End If


                            chk = GridView1.Rows(j).Cells(3).FindControl("chkSave_Cust")
                            If Not IsDBNull(LocalDS.Tables(0).Rows(i).Item("CanshowServices_Cust")) Then
                                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanshowServices_Cust")
                            End If


                            chk = GridView1.Rows(j).Cells(4).FindControl("chkSearch_Cust")
                            If Not IsDBNull(LocalDS.Tables(0).Rows(i).Item("CanshowButton_Cust")) Then
                                chk.Checked = LocalDS.Tables(0).Rows(i).Item("CanshowButton_Cust")
                            End If




                            'Exit For
                        End If

                    Next
                Next
            Else
                Session("Dyanamic") = "No"
                fillGrid("Select *,'0' as CanshowServices, '0' as CanshowButton,'0' as CanshowServices_Cust, '0' as CanshowButton_Cust from " & GV.DefaultDatabase.Trim & ".dbo.CRM_DyanamicServices_Dashboard   order by OrderNo  asc")

            End If




        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnsaveOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsaveOk.Click
        Try

            lblError.Text = ""
            lblError.CssClass = "" '_Cust
            Dim canSave, canSearch, canSave_Cust, canSearch_Cust, canUpdate, CanDelete, Canselect As Boolean
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

            'If ddlGroup.SelectedIndex = 0 Then
            '    btnSave.Enabled = False
            '    btnClear.Enabled = False
            '    GridView1.DataSource = Nothing
            '    GridView1.DataBind()
            '    lblGroupError.Text = "Please Select Group"
            '    Exit Sub
            'End If


            Dim arr() As String = ddlCompany.SelectedValue.Split(":")
            VCompanyCode = arr(0)


            If Not lblDatabaseName.Text.Trim = "" Then
                Qry = "Delete from  " & lblDatabaseName.Text.Trim & ".dbo.CRM_DyanamicServices_Rights_Dashboard where CompanyCode='" & VCompanyCode & "' ;"
                GV.FL.DMLQueries(Qry)
            End If


            Qry = ""

            lblError.Text = ""

            For i As Integer = 0 To GridView1.Rows.Count - 1

                chk = GridView1.Rows(i).Cells(1).FindControl("chkSave")
                If (chk.Checked = True) Then
                    canSave = True
                Else
                    canSave = False
                End If


                chk = GridView1.Rows(i).Cells(2).FindControl("chkSearch")
                If (chk.Checked = True) Then
                    canSearch = True
                Else
                    canSearch = False
                End If


                chk = GridView1.Rows(i).Cells(3).FindControl("chkSave_Cust")
                If (chk.Checked = True) Then
                    canSave_Cust = True
                Else
                    canSave_Cust = False
                End If


                chk = GridView1.Rows(i).Cells(4).FindControl("chkSearch_Cust")
                If (chk.Checked = True) Then
                    canSearch_Cust = True
                Else
                    canSearch_Cust = False
                End If

                Dim lblIconName As Label = DirectCast(GridView1.Rows(i).FindControl("lblIconName"), Label)
                Dim lblPostbackUrl As Label = DirectCast(GridView1.Rows(i).FindControl("lblPostbackUrl"), Label)
                Dim lblOrderNo As Label = DirectCast(GridView1.Rows(i).FindControl("lblOrderNo"), Label)
                Dim servicename As String = GV.parseString(GridView1.Rows(i).Cells(5).Text.Trim)


                updatedby = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

                If Qry.Trim = "" Then
                    Qry = "insert into " & lblDatabaseName.Text.Trim & ".dbo.CRM_DyanamicServices_Rights_Dashboard(CanshowServices_Cust,CanshowButton_Cust,User_Group,CompanyCode,CanshowServices,CanshowButton,ServiceName,IconName,PostbackUrl,OrderNo,UpdatedOn,UpdatedBy) values('" & canSave_Cust & "','" & canSearch_Cust & "', 'Admin','" & VCompanyCode & "','" & canSave & "','" & canSearch & "','" & servicename & "','" & GV.parseString(lblIconName.Text) & "','" & GV.parseString(lblPostbackUrl.Text) & "','" & GV.parseString(lblOrderNo.Text) & "',getDate(),'" & updatedby & "') ;"
                Else
                    Qry = Qry & " " & "insert into " & lblDatabaseName.Text.Trim & ".dbo.CRM_DyanamicServices_Rights_Dashboard(CanshowServices_Cust,CanshowButton_Cust,User_Group,CompanyCode,CanshowServices,CanshowButton,ServiceName,IconName,PostbackUrl,OrderNo,UpdatedOn,UpdatedBy) values('" & canSave_Cust & "','" & canSearch_Cust & "', 'Admin','" & VCompanyCode & "','" & canSave & "','" & canSearch & "','" & servicename & "','" & GV.parseString(lblIconName.Text) & "','" & GV.parseString(lblPostbackUrl.Text) & "','" & GV.parseString(lblOrderNo.Text) & "',getDate(),'" & updatedby & "') ;"
                End If

            Next

            'select * from CRM_DyanamicServices_Rights_Dashboard  


            If Not Qry.Trim = "" Then
                If GV.FL.DMLQueriesBulk(Qry) = True Then

                    btnsaveOk.Visible = False
                    'BtnSaveCancel.Attributes("style") = "display:none"
                    BtnSaveCancel.Text = "Ok"
                    lblSavingDialogBox.Text = "!!!! Rights Applied Successfully !!!!"
                    lblSavingDialogBox.CssClass = "Successlabels"
                Else

                    btnsaveOk.Visible = False
                    BtnSaveCancel.Text = "Ok"
                    lblSavingDialogBox.Text = "!!!! Rights Applying Failed !!!!"
                    lblSavingDialogBox.CssClass = "Errorlabels"
                End If
            End If
            ModalPopupExtender2.Show()

        Catch ex As Exception

        End Try
    End Sub

    
End Class