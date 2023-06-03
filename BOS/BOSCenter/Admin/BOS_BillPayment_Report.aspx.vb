Public Class BOS_BillPayment_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    lblHeading.Text = "Bill Payment Report"
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


    Public Sub clear()
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblExportQry.Text = ""

            'lblError.Text = ""
            'lblError.CssClass = ""
            'lblError0.Text = ""
            'lblError0.CssClass = ""
            'lblError1.Text = ""
            'lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            lblExportQry.Text = ""
            'lblError.Text = ""
            'lblError0.Text = ""
            'lblError1.Text = ""
            lblNoRecords.Text = ""

            'lblError.CssClass = ""
            'lblError0.CssClass = ""
            'lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()


            Bind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
            lblExportQry.Text = ""
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
                Dim Filter_Mode As String = ""
                Dim Filter_rechType As String = ""
                If Not ddlAmountType.SelectedIndex = 0 Then
                    Filter_rechType = " and Recharge_ServiceType='" & ddlAmountType.SelectedValue.Trim & "'  and not API_service='RECH' "
                Else
                    Filter_rechType = " and not API_service='RECH'  "
                End If
                If Not ddlMode.SelectedIndex = 0 Then
                    Filter_Mode = " and Mode='" & ddlMode.SelectedValue.Trim & "' "
                End If



                If GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Mobile".Trim.ToUpper Then
                    Querystring = "Select RID as 'Sr No',Recharge_MobileNo_CaNo as 'Mobile No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')   " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "DTH".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "PostPaid".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'Mobile No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Electricity".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Broadband".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'Mobile No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "LPG".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Landline".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'Telephone No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Water".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "EMI".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Municipality".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Cable".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Insurance".Trim.ToUpper Then
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                Else
                    Querystring = "Select  RID as 'Sr No',Recharge_MobileNo_CaNo as 'CA No',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where API_status in ('Success','true')  " & Filter_rechType & " " & Filter_Mode & " " & duration & "  order by rid desc "
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    GV.FL.showSerialnoOnGridView(GridView1, 1)

                    'Dim TotalAmt As Decimal = 0


                    'For i As Integer = 0 To GridView1.Rows.Count - 1
                    '    GridView1.Rows(i).Cells(1).Text = i + 1
                    '    If GV.parseString(GridView1.Rows(i).Cells(6).Text) = "" Then GridView1.Rows(i).Cells(6).Text = "0"
                    '    TotalAmt = TotalAmt + GV.parseString(GridView1.Rows(i).Cells(6).Text)
                    'Next


                    'GridView1.FooterRow.Cells(5).Text = "Total"
                    'GridView1.FooterRow.Cells(6).Text = TotalAmt
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

    Protected Sub btnGrdPrint_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try


            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            'lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
            'lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            'lblPopDateTime.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text) & " " & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            'lblPopTransactionID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            'lblPopTransactionAmt.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            'lblPopStatus.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
            'lblpopOperator.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            'lblpopMobileNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            'ModalPopupExtender3.Show()

            Dim btn As LinkButton = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("LinkButton3"), LinkButton)
            Response.Redirect("../Admin/PrintReceipt.aspx?Type=" & ddlAmountType.SelectedValue.Trim & "&ID=" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text))



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Try
            btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"
            'Dim btn As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton2"), LinkButton)
            'If Not btn Is Nothing Then
            '    btn.OnClientClick = "window.open('../admin/Print_Installment_Report.aspx?PaymentID=" & GV.parseString(GridView1.Rows(i).Cells(7).Text) & "','_blank');"
            'End If
        Catch ex As Exception

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

    Protected Sub ddlMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMode.SelectedIndexChanged
        Try
            clear()
        Catch ex As Exception

        End Try
    End Sub


End Class