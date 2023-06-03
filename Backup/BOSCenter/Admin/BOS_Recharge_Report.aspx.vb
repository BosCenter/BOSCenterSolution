Public Class BOS_Recharge_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                CheckBox1_CheckedChanged(sender, e)
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    lblHeading.Text = "Recharge & Bill Payment Report"
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


            Bind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
            lblExportQry.Text = ""
            chkduration.Checked = False
            CheckBox1_CheckedChanged(sender, e)

        Catch ex As Exception

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

                Dim duration As String = ""
                If chkduration.Checked = True Then
                    duration = " and Recharge_Date between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' "
                End If

                If GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Mobile".Trim.ToUpper Then
                    Querystring = "Select RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as  'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "DTH".Trim.ToUpper Then
                    Querystring = "Select  RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "PostPaid".Trim.ToUpper Then
                    Querystring = "Select  RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Electricity".Trim.ToUpper Then
                    Querystring = "Select  RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Broadband".Trim.ToUpper Then
                    Querystring = "Select  RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Gas".Trim.ToUpper Then
                    Querystring = "Select  RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Landline".Trim.ToUpper Then
                    Querystring = "Select  RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'TelephoneNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Waterbill".Trim.ToUpper Then
                    Querystring = "Select  RID as SrNo,(CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount',API_TransId as 'TRANS_NO', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "' " & duration & "  order by rid desc "
                End If


                ' and Recharge_Date between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' 



                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim TotalAmt As Decimal = 0


                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(0).Text = i + 1
                        If GV.parseString(GridView1.Rows(i).Cells(5).Text) = "" Then GridView1.Rows(i).Cells(5).Text = "0"
                        TotalAmt = TotalAmt + GV.parseString(GridView1.Rows(i).Cells(5).Text)
                    Next


                    GridView1.FooterRow.Cells(4).Text = "Total"
                    GridView1.FooterRow.Cells(5).Text = TotalAmt
                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
                Exit Sub

            Else


            End If

        Catch ex As Exception
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
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToExcel_New(GridView1, Response, "", "APIWiseTransactionDetails", lblExportQry.Text, "dyanamic")
                End If
            End If

        Catch ex As Exception

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

        End Try
    End Sub


    Protected Sub ddlAmountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAmountType.SelectedIndexChanged
        Try
            clear()
        Catch ex As Exception

        End Try
    End Sub
End Class