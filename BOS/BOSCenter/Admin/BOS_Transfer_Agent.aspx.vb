
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO

Public Class BOS_Transfer_Agent
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VCompanyCode As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                btnWelcomeLetter.Enabled = False
                ddlAccount_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub clear()
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            ddlAccount.SelectedIndex = 0
            ddlSelectCriteria.SelectedIndex = 0
            txtSearchString.Text = ""
            btnWelcomeLetter.Enabled = False
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try

            lblError1.Text = ""
            lblNoRecords.Text = ""
            lblBranchName.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()
            
            If (Not ddlSelectCriteria.SelectedValue = "All Records") And GV.parseString(txtSearchString.Text.Trim) = "" Then
                lblError1.Text = "Enter Search value"
                lblError1.CssClass = "errorlabels"
                Exit Sub
            End If
              Bind()

        Catch ex As Exception

        End Try
    End Sub

    Dim Querystring As String = ""
    Public Sub Bind()
        Try

            Dim SearchColumnName As String = ""
            Dim colName As String = ""
            Dim Branchfilter As String = ""
            Dim AccountFilter As String = ""
            If ddlAccount.SelectedValue = "Shift Distributor" Then
                AccountFilter = " Where AgentType='Distributor' "
            ElseIf ddlAccount.SelectedValue = "Shift Retailer" Then
                AccountFilter = " Where AgentType='Retailer' "
            End If
            If ddlSelectCriteria.SelectedValue = "All Records" Then
                SearchColumnName = ""
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Register ID".Trim.ToUpper Then
                SearchColumnName = " and RegistrationId = '" & txtSearchString.Text.Trim & "' "
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Name".Trim.ToUpper Then
                SearchColumnName = " and FirstName like '" & txtSearchString.Text.Trim & "%' "
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Mobile No".Trim.ToUpper Then
                SearchColumnName = " and MobileNo = '" & txtSearchString.Text.Trim & "' "
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Refrence Id".Trim.ToUpper Then
                SearchColumnName = " and RefrenceID = '" & txtSearchString.Text.Trim & "' "

            Else
                SearchColumnName = ""
            End If

            Querystring = "select RID as SrNo,RegistrationId,AgentType,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,PanCardNumber,(FirstName+' '+LastName)	as 'Name',EmailID,MobileNo,RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration b  " & AccountFilter & "   " & SearchColumnName & "  order by  RID Desc"
            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    btnWelcomeLetter.Enabled = True
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                Else
                    'clear()
                    btnWelcomeLetter.Enabled = False
                    lblBranchName.Text = ""
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                End If
            End If

        Catch ex As Exception
            lblNoRecords.Text = ex.Message
        End Try
    End Sub



    Protected Sub DeleteRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text


            lblDialogMsg.Text = "Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            ' Button2.Visible = True
            ModalPopupExtender1.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()

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

            If GridView1.Rows.Count > 0 Then
                Bind()
            End If

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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub
    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "TranferAgent")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnword.Click
        Try
            GV.ExportToWord(GridView1, Response, "TranferAgent")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagebtnpdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnpdf.Click
        Try

            GV.ExportToPdf_DivTag_HavingGridview(GridView1, ApprovalDiv, Response, "TranferAgent")
        Catch ex As Exception

        End Try
    End Sub



    Public Sub SelectAll(ByVal cellIndex As Integer, ByVal controlID As String)
        Try

            If cellIndex = 0 Then
                Dim chk As CheckBox
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                    chk.Checked = True
                Next

            Else
                Dim chk As CheckBox
                Dim str() As String
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    str = Split(GridView1.Rows(i).Cells(1).Text, ".")
                    If Not str.Length > 1 Then
                        chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                        chk.Checked = False
                    Else
                        chk = GridView1.Rows(i).Cells(cellIndex).FindControl(controlID)
                        chk.Checked = True
                    End If
                Next
            End If
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

            Dim btn As Button
            btn = GridView1.HeaderRow.Cells(0).FindControl("frmselectAll")

            If btn.Text = "ALL" Then
                btn.Text = "NONE"
                SelectAll(0, "chkSelect")
            ElseIf btn.Text = "NONE" Then
                btn.Text = "ALL"
                SelectNone(0, "chkSelect")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnWelcomeLetter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWelcomeLetter.Click
        Try

            lblError1.Text = ""
            lblError1.CssClass = ""

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim newRID As String = ""

            If Not GridView1.Rows.Count > 0 Then
                lblNoRecords.Text = "Sorry there is no record."
                lblNoRecords.CssClass = "errorlabels"
            Else
                Dim chk As CheckBox

                For i As Integer = 0 To GridView1.Rows.Count - 1
                    chk = GridView1.Rows(i).Cells(0).FindControl("chkSelect")
                    If chk.Checked = True Then
                        Dim lbl As Label = DirectCast(GridView1.Rows(i).Cells(0).FindControl("lblgrdRID"), Label)

                        If newRID = "" Then
                            newRID = "" & GV.parseString(lbl.Text.Trim)
                        Else
                            newRID = newRID & "," & GV.parseString(lbl.Text.Trim)
                        End If

                    Else
                    End If
                Next

                If Not newRID.Trim = "" Then
                    lblAccountRID.Text = newRID
                    ddlChangeAgent.SelectedIndex = 0
                    btnUpdate.Visible = True
                    btnCancel.Text = "Cancel"
                    lblDialogMsg.Text = ""
                    lblDialogMsg.CssClass = ""
                    ModalPopupExtender1.Show()
                Else
                    lblNoRecords.Text = "Please select Agent."
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

 

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try

            If Not GV.parseString(lblAccountRID.Text) = "" Then
                Dim ChnageBranchName As String = ""
                Dim ChnageBranchCode As String = ""
                lblAccountRID.Text = "(" & GV.parseString(lblAccountRID.Text) & ")"
                Dim EmpInfo() As String = GV.parseString(ddlChangeAgent.SelectedValue.Trim).Split(":")
                ChnageBranchCode = EmpInfo(0)
                ChnageBranchName = EmpInfo(1)
                If ddlChangeAgent.SelectedIndex = 0 Then
                    lblDialogMsg.Text = "Select Agent."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                If Not ChnageBranchCode = "" Or ChnageBranchName = "" Then
                    Dim str As String
                    str = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set RefrenceID='" & ChnageBranchCode & "'  where RID in  " & GV.parseString(lblAccountRID.Text) & "  ;"

                    Dim result As Boolean = GV.FL.DMLQueries(str)
                    lblDialogMsg.Text = result
                    If result = True Then
                        lblAccountRID.Text = ""
                        'ddlChangeBranch.Enabled = False
                        'ddlChangeBranch.CssClass = "form-control"
                        lblDialogMsg.Text = "Agent Transfered Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        Bind()

                    Else
                        lblDialogMsg.Text = "Sorry !! Record Failed."
                        lblDialogMsg.CssClass = "errorlabels"
                    End If
                    btnCancel.Text = "OK"
                    btnUpdate.Visible = False
                    ModalPopupExtender1.Show()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlAccount_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlAccount.SelectedIndexChanged
        Try
            btnWelcomeLetter.Enabled = False
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblAccountRID.Text = ""
            lblBranchName.Text = ""

            If ddlAccount.SelectedValue.Trim.ToUpper = "Shift Distributor".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlChangeAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where AgentType='Master Distributor'")
                If ddlChangeAgent.Items.Count > 0 Then
                    ddlChangeAgent.Items.Insert(0, "Select Master Distributor")
                Else
                    ddlChangeAgent.Items.Add("Select Master Distributor")
                End If
            ElseIf ddlAccount.SelectedValue.Trim.ToUpper = "Shift Retailer".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlChangeAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where AgentType='Distributor'")
                If ddlChangeAgent.Items.Count > 0 Then
                    ddlChangeAgent.Items.Insert(0, "Select Distributor")
                Else
                    ddlChangeAgent.Items.Add("Select Distributor")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class