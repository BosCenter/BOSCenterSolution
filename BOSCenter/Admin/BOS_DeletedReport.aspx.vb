Imports System.Drawing

Public Class BOS_DeletedReport
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Then
                '    lblHeading.Text = "Search & Edit Sub Distributor"
                '    ddlAgentType.SelectedValue = "Sub Distributor"
                '    ddlAgentType.Enabled = False
                '    ddlAgentType.CssClass = "form-control"
                'ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Sub Distributor" Then
                '    lblHeading.Text = "Search & Edit Retailer"
                '    ddlAgentType.SelectedValue = "Retailer"
                '    ddlAgentType.Enabled = False
                '    ddlAgentType.CssClass = "form-control"
                'Else
                '    lblHeading.Text = "Search & Edit Distributor"
                '    ddlAgentType.SelectedValue = "Distributor"
                '    ddlAgentType.Enabled = False
                '    ddlAgentType.CssClass = "form-control"
                'End If


                CheckBox1_CheckedChanged(sender, e)

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkduration.CheckedChanged
        Try



            lblError.Text = ""
            lblError0.Text = ""
            lblError1.Text = ""
            lblNoRecords.Text = ""

            lblError.CssClass = ""
            lblError0.CssClass = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()


            If chkduration.Checked = True Then
                txtFrom.Text = ""
                txtTO.Text = ""
                txtFrom.Enabled = True
                txtTO.Enabled = True
                lblError.Text = ""
                lblError0.Text = ""
            Else
                lblError0.Text = ""
                lblError.Text = ""
                txtFrom.Text = ""
                txtTO.Text = ""
                txtFrom.Enabled = False
                txtTO.Enabled = False
                txtFrom.CssClass = "form-control"
                txtTO.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub clear()
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblExportQry.Text = ""

            txtSearchString.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""
            lblError0.Text = ""
            lblError0.CssClass = ""
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            'Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            'Dim LocalDS As New DataSet
            'LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            'Dim DataRows() As DataRow
            'DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            'If Not DataRows Is Nothing Then
            '    If DataRows.Count > 0 Then
            '        For D As Integer = 0 To DataRows.Count - 1
            '            If Not DataRows(D).Item("CanUpdate") = True Then
            '            Else
            '                lblDialogMsg.CssClass = ""
            '                lblDialogMsg.Text = "Not Autorized To Performe This Action."
            '                btnCancel.Text = "Ok"
            '                btnok.Visible = False
            '                ModalPopupExtender1.Show()
            '                Exit Sub
            '            End If
            '        Next

            '    End If
            'End If
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)

            lblRID.Text = lbl.Text
            Session("RecordID") = lbl.Text
            Session("RecordEdit") = 1
            Session("RecordEditConfirm") = 9
            Session("TransactionType") = "Edit"
            Session("WorkType") = "Edit"
            Response.Redirect("BOS_CreateAgent.aspx")

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            lblExportQry.Text = ""
            lblError.Text = ""
            lblError0.Text = ""
            lblError1.Text = ""
            lblNoRecords.Text = ""

            lblError.CssClass = ""
            lblError0.CssClass = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            If chkduration.Checked = True Then
                Dim isErrorFound As Boolean = False
                Dim isFocusApplied As Boolean = False

                If txtFrom.Text.Trim = "" Then
                    txtFrom.CssClass = "ValidationError"
                    isErrorFound = True
                Else
                    txtFrom.CssClass = "form-control"
                End If

                If txtTO.Text.Trim = "" Then
                    txtTO.CssClass = "ValidationError"
                    isErrorFound = True
                Else
                    txtTO.CssClass = "form-control"
                End If
                If isErrorFound = True Then
                    Exit Sub
                End If
                If Not (lblError.Text.Trim = "") Or Not (lblError0.Text.Trim = "") Then
                    Exit Sub
                End If
                Dim Vfromdate As String = GV.returnDateMonthWiseWithDateChecking(txtFrom.Text)
                Dim VTodate As String = GV.returnDateMonthWiseWithDateChecking(txtTO.Text)
                If Vfromdate = "Error" Then
                    'clear()
                    lblError.Text = "Invalid From Date"
                    lblError.CssClass = "errorlabels"
                Else
                    lblError.Text = ""
                    lblError.CssClass = ""
                End If

                If VTodate = "Error" Then
                    'clear()
                    lblError0.Text = "Invalid To Date"
                    lblError0.CssClass = "errorlabels"
                Else
                    lblError0.Text = ""
                    lblError0.CssClass = ""
                End If

                If Not (lblError.Text.Trim = "") Or Not (lblError0.Text.Trim = "") Then
                    Exit Sub
                End If

                If DateDiff(DateInterval.Day, CDate(Vfromdate), CDate(VTodate)) < 0 Then
                    clear()
                    lblError0.Text = "To Date Cannot Be Smaller Then From Date"
                    lblError0.CssClass = "errorlabels"
                Else
                    lblError0.Text = ""
                    lblError0.CssClass = ""
                End If


                If Not (lblError0.Text.Trim = "") Then
                    Exit Sub
                End If



            End If



            If Not ddlSelectCriteria.SelectedValue = "All Records" And GV.parseString(txtSearchString.Text.Trim) = "" Then
                lblError1.Text = "Enter Search value"
                lblError1.CssClass = "errorlabels"
                Exit Sub
            End If



            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
            lblExportQry.Text = ""
            chkduration.Checked = False
            CheckBox1_CheckedChanged(sender, e)
            ddlSelectCriteria.SelectedIndex = 0
            txtSearchString.Text = ""
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim SearchColumnName As String = ""
            Dim colName As String = ""
            'GV.get_Distributor_SessionVariables("LoginID", Request, Response)
            Dim RefrenceId As String = ""
            Dim branchFilter As String = ""



            If chkduration.Checked = True Then

                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    SearchColumnName = ""
                ElseIf ddlSelectCriteria.SelectedValue = "Agent ID" Then
                    SearchColumnName = " and RegistrationId = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "Name" Then
                    SearchColumnName = " and FirstName like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Mobile No" Then
                    SearchColumnName = " and MobileNo = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "State" Then
                    SearchColumnName = " and State like '" & txtSearchString.Text.Trim & "%' "
                Else
                    SearchColumnName = ""
                End If



                If Not ddlAgentType.SelectedIndex = 0 Then
                    colName = " and AgentType='" & GV.parseString(ddlAgentType.SelectedValue) & "' "
                End If



                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Master Distributor" Then
                    RefrenceId = " and RefrenceID ='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Then
                    RefrenceId = " and RefrenceID ='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                Else
                    RefrenceId = ""
                End If

                Querystring = "select RID as SrNo,AgentType,(CONVERT(VARCHAR(11),DeletedOn,106)) as DeletedDate,DeletedBy,RegistrationId,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName	,(FirstName+ ' ' +LastName) as Name,MobileNo	,BusinessType	,PanCardNumber,	OfficeAddress,State,ActiveStatus,UploadPanCard=Replace(UploadPanCard,'~','https://www.boscenter.in'),UploadAddharCard_Front=Replace(UploadAddharCard_Front,'~','https://www.boscenter.in'),UploadAddharCard_Back=Replace(UploadAddharCard_Back,'~','https://www.boscenter.in'),UploadOtherProof=Replace(UploadOtherProof,'~','https://www.boscenter.in'),UploadPhoto=Replace(UploadPhoto,'~','https://www.boscenter.in') from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration_Deleted b where DeletedOn between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' " & colName & "  " & SearchColumnName & "  " & RefrenceId & "  order by  RID Desc"

            Else

                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    SearchColumnName = ""
                ElseIf ddlSelectCriteria.SelectedValue = "Agent ID" Then
                    SearchColumnName = " where RegistrationId = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "Name" Then
                    SearchColumnName = " where FirstName like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Mobile No" Then
                    SearchColumnName = " where MobileNo = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "State" Then
                    SearchColumnName = " where State like '" & txtSearchString.Text.Trim & "%' "
                Else
                    SearchColumnName = ""
                End If

                If Not ddlAgentType.SelectedIndex = 0 Then
                    If SearchColumnName = "" Then
                        colName = " where AgentType='" & GV.parseString(ddlAgentType.SelectedValue) & "' "
                    Else
                        colName = " and AgentType='" & GV.parseString(ddlAgentType.SelectedValue) & "' "
                    End If
                End If


                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Master Distributor" Then
                    RefrenceId = " and RefrenceID ='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Then
                    RefrenceId = " and RefrenceID ='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                Else
                    RefrenceId = ""
                End If

                Querystring = "select RID as SrNo,AgentType,(CONVERT(VARCHAR(11),DeletedOn,106)) as DeletedDate,DeletedBy,RegistrationId,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName	,(FirstName+ ' ' +LastName) as Name,MobileNo	,BusinessType	,PanCardNumber,	OfficeAddress,State,ActiveStatus,UploadPanCard=Replace(UploadPanCard,'~','https://www.boscenter.in'),UploadAddharCard_Front=Replace(UploadAddharCard_Front,'~','https://www.boscenter.in'),UploadAddharCard_Back=Replace(UploadAddharCard_Back,'~','https://www.boscenter.in'),UploadOtherProof=Replace(UploadOtherProof,'~','https://www.boscenter.in'),UploadPhoto=Replace(UploadPhoto,'~','https://www.boscenter.in') from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration_Deleted b  " & SearchColumnName & "  " & colName & " " & RefrenceId & " " & branchFilter & "  order by  RID Desc"
            End If


            If Not Querystring = "" Then

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)

                Else
                    'clear()
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblNoRecords.Text = ex.Message
        End Try
    End Sub

    Dim QryStr As String

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

            If GridView1.Rows.Count > 0 Then
                Bind()
            End If

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
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub
    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToExcel_New(GridView1, Response, "", "DeletedReport", lblExportQry.Text, "dyanamic")
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub ImagebtnWOrd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToWord_New(GridView1, Response, "DeletedReport", lblExportQry.Text, "dyanamic")
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub ExportToPdf_DivTag_HavingGridview(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToPdf_New(GridView1, "", Response, "DeletedReport  ", lblExportQry.Text, "dyanamic")
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Protected Sub ddlSelectCriteria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectCriteria.SelectedIndexChanged
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class