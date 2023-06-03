
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net




Public Class BOS_GST_Raised_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim Filter As String = ""
            Dim Status As String = ""
            Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            'GridView1.Columns(2).Visible = True
            Dim GROUPFilter As String = ""

            If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Or grp.Trim.ToUpper = "Customer".Trim.ToUpper Then
                GROUPFilter = " RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'"
            Else
                GROUPFilter = ""

            End If

            If ddlApproveStatus.SelectedValue.Trim.ToUpper = "All Status".Trim.ToUpper Then
                Status = ""
            ElseIf ddlApproveStatus.SelectedValue.Trim.ToUpper = "Pending".Trim.ToUpper Then
                Status = " where ApporvedStatus ='" & GV.parseString(ddlApproveStatus.SelectedValue.Trim) & "'"
            ElseIf ddlApproveStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then
                Status = " where ApporvedStatus ='" & GV.parseString(ddlApproveStatus.SelectedValue.Trim) & "'"
            ElseIf ddlApproveStatus.SelectedValue.Trim.ToUpper = "Rejected".Trim.ToUpper Then
                Status = " where ApporvedStatus = '" & GV.parseString(ddlApproveStatus.SelectedValue.Trim) & "'"
            End If

            If ddlSelectCriteria.SelectedValue.Trim.ToUpper = "All Records".Trim.ToUpper Then
                Filter = ""
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Request ID".Trim.ToUpper Then
                Filter = "  RefrenceID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Agent ID".Trim.ToUpper Then
                Filter = "  RegistrationId ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            End If
            Dim condition As String = ""
            If Status = "" Then
                If Filter = "" Then
                    condition = ""
                Else
                    condition = " Where "
                End If
            Else
                If Not Filter = "" Then
                    condition = " And "
                Else
                    condition = ""
                End If

            End If
            If Not GROUPFilter = "" Then
                If condition = "" Then
                    GROUPFilter = " Where " & GROUPFilter
                Else
                    GROUPFilter = " And " & GROUPFilter
                End If
            End If


            Querystring = "select RID as SrNo,RefrenceID as 'Request ID',(CONVERT(VARCHAR(11),RecordDateTime,106)) as 'RequestDate',RegistrationId as 'Agent ID',GST_Month,(CONVERT(VARCHAR(11),FromDate,106)) as FromDate,(CONVERT(VARCHAR(11),ToDate,106)) as ToDate,Remarks,CommAmt,GSTAmt,GrossAmt,TDS_Amt,CreditBalance,ApporvedStatus,ApporveRemakrs from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_GST_Refund_Details " & Status & " " & condition & "  " & Filter & " " & GROUPFilter & " "
            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                Dim CommAmt, GSTAmt, GrossAmt, TDS_Amt, CreditBalance As Decimal

                CommAmt = 0
                GSTAmt = 0
                GrossAmt = 0
                TDS_Amt = 0
                CreditBalance = 0


                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(0).Text = i + 1
                        CommAmt = CommAmt + CDec(GridView1.Rows(i).Cells(8).Text)
                        GSTAmt = GSTAmt + CDec(GridView1.Rows(i).Cells(9).Text)
                        GrossAmt = GrossAmt + CDec(GridView1.Rows(i).Cells(10).Text)
                        TDS_Amt = TDS_Amt + CDec(GridView1.Rows(i).Cells(11).Text)
                        CreditBalance = CreditBalance + CDec(GridView1.Rows(i).Cells(12).Text)
                    Next

                    GridView1.FooterRow.Cells(7).Text = "Total"
                    GridView1.FooterRow.Cells(8).Text = CommAmt
                    GridView1.FooterRow.Cells(9).Text = GSTAmt
                    GridView1.FooterRow.Cells(10).Text = GrossAmt
                    GridView1.FooterRow.Cells(11).Text = TDS_Amt
                    GridView1.FooterRow.Cells(12).Text = CreditBalance


                Else

                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Or grp.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    ddlSelectCriteria.Items.Remove("Agent ID")
                End If
                Bind()
            End If

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



    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            GV.ExportToWord(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            lblExportQry.Text = ""

            lblError1.Text = ""
            lblNoRecords.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            If Not ddlSelectCriteria.SelectedValue = "All Records" And GV.parseString(txtSearchingValue.Text.Trim) = "" Then
                lblError1.Text = "Enter Search value"
                lblError1.CssClass = "errorlabels"
                Exit Sub
            End If
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub clear()
        Try
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            ddlSelectCriteria.SelectedIndex = 0
            txtSearchingValue.Text = ""
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            ddlNoOfRecords.SelectedIndex = 0
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class