
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO

Public Class SearchClientDetails
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("SUPERADMIN")


    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkduration.CheckedChanged
        Try
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

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CheckBox1_CheckedChanged(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnColumnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnColumnAdd.Click
        Try
            Dim selectedIndexes() As Integer = lstAllColumn.GetSelectedIndices
            Dim selectedItems(selectedIndexes.Length) As String

            For i As Integer = 0 To selectedIndexes.Length - 1
                lstShowColumn.Items.Add(lstAllColumn.Items((selectedIndexes(i))))
                selectedItems(i) = lstAllColumn.Items((selectedIndexes(i))).ToString
            Next

            For i As Integer = 0 To selectedItems.Length - 1
                lstAllColumn.Items.Remove(selectedItems(i))
            Next

            lstAllColumn.ClearSelection()
            lstShowColumn.ClearSelection()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnColumnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnColumnRemove.Click
        Try
            Dim selectedIndexes() As Integer = lstShowColumn.GetSelectedIndices
            Dim selectedItems(selectedIndexes.Length) As String

            For i As Integer = 0 To selectedIndexes.Length - 1
                lstAllColumn.Items.Add(lstShowColumn.Items((selectedIndexes(i))))
                selectedItems(i) = lstShowColumn.Items((selectedIndexes(i))).ToString
            Next

            For i As Integer = 0 To selectedItems.Length - 1
                lstShowColumn.Items.Remove(selectedItems(i))
            Next

            lstAllColumn.ClearSelection()
            lstShowColumn.ClearSelection()

        Catch ex As Exception

        End Try
    End Sub


    Public Sub clear()
        Try
            ddlSelectCriteria.SelectedIndex = 0
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

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
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
                Dim Vfromdate As String = GV.FL.returnDateMonthWiseWithDateChecking(txtFrom.Text)
                Dim VTodate As String = GV.FL.returnDateMonthWiseWithDateChecking(txtTO.Text)
                If Vfromdate = "Error" Then
                    clear()
                    lblError.Text = "Invalid From Date"
                    lblError.CssClass = "errorlabels"
                Else
                    lblError.Text = ""
                    lblError.CssClass = ""
                End If

                If VTodate = "Error" Then
                    clear()
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

        End Try
    End Sub

    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim SearchColumnName As String = ""
            Dim colName As String = ""
            For i As Integer = 0 To lstShowColumn.Items.Count - 1
                If colName = "" Then
                    colName = lstShowColumn.Items(i).Value
                Else
                    colName = colName & "," & lstShowColumn.Items(i).Value
                End If
            Next

            If chkduration.Checked = True Then
                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    SearchColumnName = ""
                ElseIf ddlSelectCriteria.SelectedValue = "Company Code" Then
                    SearchColumnName = " and CompanyCode like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Company Name" Then
                    SearchColumnName = " and CompanyName like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Mobile No" Then
                    SearchColumnName = " and Mobile_No like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Email ID" Then
                    SearchColumnName = " and Email_ID like '" & txtSearchString.Text.Trim & " %' "
                ElseIf ddlSelectCriteria.SelectedValue = "Active Status" Then
                    SearchColumnName = " and Status like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Contact Person" Then
                    SearchColumnName = " and ContactPerson like '" & txtSearchString.Text.Trim & "%' "
                Else
                    SearchColumnName = ""
                End If
                Querystring = "select RID as SrNo," & colName & " from BOS_ClientRegistration where RegisterationDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & SearchColumnName & "  order by  RID Desc"
            Else

                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    SearchColumnName = ""
                ElseIf ddlSelectCriteria.SelectedValue = "Company Code" Then
                    SearchColumnName = " where CompanyCode like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Company Name" Then
                    SearchColumnName = " where CompanyName like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Mobile No" Then
                    SearchColumnName = " where Mobile_No like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Email ID" Then
                    SearchColumnName = " where Email_ID like '" & txtSearchString.Text.Trim & " %' "
                ElseIf ddlSelectCriteria.SelectedValue = "Active Status" Then
                    SearchColumnName = " where Status like '" & txtSearchString.Text.Trim & "%' "
                ElseIf ddlSelectCriteria.SelectedValue = "Contact Person" Then
                    SearchColumnName = " where ContactPerson like '" & txtSearchString.Text.Trim & "%' "
                Else
                    SearchColumnName = ""
                End If

                Querystring = "select RID as SrNo," & colName & " from BOS_ClientRegistration " & SearchColumnName & "  order by  RID Desc"

            End If

            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)

                    For i As Integer = 0 To GridView1.Rows.Count - 1

                        If GV.parseString(GridView1.Rows(i).Cells(2).Text) = "CMP1045" Then
                            Dim btnDel As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton1"), LinkButton)
                            btnDel.Visible = False
                            Exit For

                        End If
                       
                    Next

                Else
                    'clear()
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

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text
            Session("RID") = lbl.Text
            Session("ClientRegistrationEdit") = 1
            Session("ClientRegistrationEditConfirm") = 9
            ' Session("WorkType") = "Edit"
            Response.Redirect("CLientRegistration.aspx")

        Catch ex As Exception

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
            Button2.Visible = True
            ModalPopupExtender1.Show()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
            chkduration.Checked = False
            CheckBox1_CheckedChanged(sender, e)
            GridView1.DataSource = Nothing
            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            If Not lblRID.Text = "" Then
                Dim result As Boolean = GV.FL.DMLQueries("delete from BOS_ClientRegistration where RID=" & lblRID.Text & "")
                'Dim result As Boolean = GV.FL.DMLQueries("update BOS_ClientRegistration set Recordstatus='Deleted',DeletedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',DeletedDate=Getdate() where RID=" & lblRID.Text & "")
                lblDialogMsg.Text = result
                If result = True Then
                    lblDialogMsg.Text = "Record deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    Bind()
                    clear()
                Else
                    lblDialogMsg.Text = "Sorry !! Record deletion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                Button2.Visible = False
                ModalPopupExtender1.Show()
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
            GV.ExportToExcel(GridView1, Response, "ClientDetails")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnword.Click
        Try
            GV.ExportToWord(GridView1, Response, "ClientDetails")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagebtnpdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnpdf.Click
        Try
            ' GV.ExportToPdf(GridView1, Response, "ClientDetails")

            GV.ExportToPdf_DivTag_HavingGridview(GridView1, ApprovalDiv, Response, "ClientDetails")

            'Response.ContentType = "application/pdf"
            'Response.AddHeader("content-disposition", "attachment;filename=div.pdf")
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)

            'Dim stringWriter As New IO.StringWriter()
            'Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            'ApprovalDiv.RenderControl(htmlTextWriter)

            'Dim stringReader As New IO.StringReader(stringWriter.ToString())
            'Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            'Dim htmlparser As New HTMLWorker(Doc)
            'PdfWriter.GetInstance(Doc, Response.OutputStream)

            'Doc.Open()
            'htmlparser.Parse(stringReader)
            'Doc.Close()
            'Response.Write(Doc)
            'Response.[End]()
        Catch ex As Exception

        End Try
    End Sub

End Class