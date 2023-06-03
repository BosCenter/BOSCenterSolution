Public Class BOS_AEPS_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ' btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"
                CheckBox1_CheckedChanged(sender, e)
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                ' ModalPopupExtender2.Show()
                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    lblHeading.Text = "AEPS Report"
                    'ElseIf (group.Trim.ToUpper = "Sub Distributor".Trim.ToUpper) Or (group.Trim.ToUpper = "Distributor".Trim.ToUpper) Then
                    '    lblHeading.Text = "Commission / Balance Transfer Report"
                    '    ddlAmountType.Items.Remove("Money Transfer")
                    '    ddlAmountType.Items.Remove("PAN CARD")
                    '    ddlAmountType.Items.Remove("Recharge & Bill Payment")
                    '    ddlAmountType.Items.Remove("Service Charges") 'All Transactions
                    'Else
                    '    lblHeading.Text = " Balance Transfer Report"
                    '    ddlAmountType.Items.Remove("Money Transfer")
                    '    ddlAmountType.Items.Remove("PAN CARD")
                    '    ddlAmountType.Items.Remove("Recharge & Bill Payment")
                    '    ddlAmountType.Items.Remove("Service Charges") 'All Transactions
                    '    ddlAmountType.Items.Remove("All Transactions")
                    '    ddlAmountType.Items.Remove("Commission")

                End If

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
                    ' clear()
                    lblError.Text = "Invalid From Date"
                    lblError.CssClass = "errorlabels"
                Else
                    lblError.Text = ""
                    lblError.CssClass = ""
                End If

                If VTodate = "Error" Then
                    ' clear()
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

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim SearchColumnName As String = ""
            Dim RefrenceId As String = ""
            Dim Value As String = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

            Dim AmountType As String = ""

            Dim loginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
            If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then

                Dim SERVICE_TYPE As String = ""
                If GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "AEPS".Trim.ToUpper Then
                    SERVICE_TYPE = " and API_Call_Type='AEPS' "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "AADHAR PAY".Trim.ToUpper Then
                    SERVICE_TYPE = " and API_Call_Type='AADHAR PAY' "
                End If

                If chkduration.Checked = True Then
                    Querystring = " select  RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,UpdatedBy as 'AGENT_ID',API_Call_Type as 'API',Trans_Type as 'Service_Type',Trans_ID as 'Trans_No',Cash_Withdraw as 'Amount',Access_Mode,Bank_Name,Bank_IIN,Mobile_No,Latitude,Longitude,Aadhar_Last_No,Remarks from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_AEPS_API where UpdatedBy='" & loginID.Trim & "' and RecordDateTime between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' " & SERVICE_TYPE & " order by rid desc"
                Else
                    Querystring = " select  RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,UpdatedBy as 'AGENT_ID',API_Call_Type as 'API',Trans_Type as 'Service_Type', Trans_ID as 'Trans_No',Cash_Withdraw as 'Amount',Access_Mode,Bank_Name,Bank_IIN,Mobile_No,Latitude,Longitude,Aadhar_Last_No,Remarks from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_AEPS_API where UpdatedBy='" & loginID.Trim & "' " & SERVICE_TYPE & " order by rid desc "
                End If

                ',Aadhar_Pay_Ref_ID

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim TotalAmt As Decimal = 0


                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1
                        If GV.parseString(GridView1.Rows(i).Cells(8).Text) = "" Then GridView1.Rows(i).Cells(8).Text = "0"
                        TotalAmt = TotalAmt + GV.parseString(GridView1.Rows(i).Cells(8).Text)
                        Dim btn As LinkButton = DirectCast(GridView1.Rows(i).FindControl("LinkButton3"), LinkButton)
                        'btn.Visible = False

                        'If Not btn Is Nothing Then
                        '    btn.OnClientClick = "window.open('../Admin/PrintReceipt.aspx?Type=" & GV.Encode_Url_String(GV.parseString(GridView1.Rows(i).Cells(6).Text)) & "&ID=" & GV.parseString(GridView1.Rows(i).Cells(5).Text) & ",'_blank');"
                        'End If
                    Next

                    GridView1.FooterRow.Cells(7).Text = "Total"
                    GridView1.FooterRow.Cells(8).Text = TotalAmt
                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
                Exit Sub

            Else


            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblNoRecords.Text = ex.Message
        End Try
    End Sub

    Dim QryStr As String

    Protected Sub btnGrdPrint_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            Dim btn As LinkButton = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("LinkButton3"), LinkButton)

            Dim lblgrdRID As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("lblgrdRID"), Label)


            'Response.Redirect("../Admin/PrintReceipt.aspx?Type=" & GV.Encode_Url_String(GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)) & "&ID=" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text))
            Response.Redirect("../Admin/PrintReceipt.aspx?Type=" & "AEPS" & "&ID=" & GV.parseString(lblgrdRID.Text))

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    'Protected Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
    '    Try
    '        ' btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"

    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

    '    End Try
    'End Sub
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
                    GV.ExportToExcel_New(GridView1, Response, "", "APIWiseTransactionDetails", lblExportQry.Text, "dyanamic")
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
                    GV.ExportToWord_New(GridView1, Response, "TransactionDetails", lblExportQry.Text, "dyanamic")
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
                    GV.ExportToPdf_New(GridView1, "", Response, "TransactionDetails  ", lblExportQry.Text, "dyanamic")
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Protected Sub ddlAmountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAmountType.SelectedIndexChanged
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class