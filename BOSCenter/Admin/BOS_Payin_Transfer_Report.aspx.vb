Public Class BOS_Payin_Transfer_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"
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

                'If DateDiff(DateInterval.Day, CDate(Vfromdate), CDate(VTodate)) < 0 Then
                '    clear()
                '    lblError0.Text = "To Date Cannot Be Smaller Then From Date"
                '    lblError0.CssClass = "errorlabels"
                'Else
                '    lblError0.Text = ""
                '    lblError0.CssClass = ""
                'End If


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
                ElseIf ddlSelectCriteria.SelectedValue = "CustomerID" Then
                    SearchColumnName = " and CustomerID = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "RefrenceNo" Then
                    SearchColumnName = " and RefrenceNo = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "TranscationId" Then
                    SearchColumnName = " and TranscationId = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "MobileNo" Then
                    SearchColumnName = " and MobileNo = '" & txtSearchString.Text.Trim & "' "
                Else
                    SearchColumnName = ""
                End If

                'Querystring = " Select RID as 'Sr No',(CONVERT(VARCHAR(11),TransferDate,106)) as 'Transfer Date',CONVERT(varchar(15),CAST(RecordDateTime AS TIME),100) as Time,OrderNo as 'UTR No',(select top 1 AgencyName from BOS_Dis_SubDis_Retailer_Registration where RegistrationId=fr.RefrenceNo)  as 'Agency Name',TranscationId as 'Transcation ID',Receipent as 'Beneficiary Name',AccountNo,MobileNo as 'Sender Mobile No',Amount,BankName as 'Bank Name',Method,Process,APIMessage as 'Message' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MoneyTransfer_API fr  b where (APIMessage in ('Success','Transaction successful')  or APIMessage like '%Transaction Successful%' ) and TransferDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'  and RefrenceNo='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' " & SearchColumnName & "    order by  RID Desc"
                Querystring = " Select RID as 'Sr No',(CONVERT(VARCHAR(11),TransactionDate,106)) as 'Transfer Date',CONVERT(varchar(15),CAST(RecordDateTime AS TIME),100) as Time ,Amt_Transfer_TransID as 'Transcation ID',data_remitter_full_name as 'Name',customer_virtual_address as 'UPIID',TransferFrom as 'Transfer From', TransferTo as'Transfer To',TransferAmt as 'Transfer Amount', Remark,TransferFromMsg,TransferToMsg from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type='Payin' and TransferTo= '" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'and TransactionDate between  '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & "' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & "'" & SearchColumnName & " order by  RID Desc"

            Else

                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    SearchColumnName = ""
                ElseIf ddlSelectCriteria.SelectedValue = "CustomerID" Then
                    SearchColumnName = " and CustomerID = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "RefrenceNo" Then
                    SearchColumnName = " and RefrenceNo = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "TranscationId" Then
                    SearchColumnName = " and TranscationId = '" & txtSearchString.Text.Trim & "' "
                ElseIf ddlSelectCriteria.SelectedValue = "MobileNo" Then
                    SearchColumnName = " and MobileNo = '" & txtSearchString.Text.Trim & "' "
                Else
                    SearchColumnName = ""
                End If
                Querystring = " Select RID as 'Sr No',(CONVERT(VARCHAR(11),TransactionDate,106)) as 'Transfer Date',CONVERT(varchar(15),CAST(RecordDateTime AS TIME),100) as Time ,Amt_Transfer_TransID as 'Transcation ID',data_remitter_full_name as 'Name',customer_virtual_address as 'UPIID',TransferFrom as 'Transfer From', TransferTo as'Transfer To',TransferAmt as 'Transfer Amount', Remark,TransferFromMsg,TransferToMsg from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type='Payin' and TransferTo= '" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'  " & SearchColumnName & "  order by  RID Desc "



            End If
            'replace referenceId to agencyNane
            'customerid replace with  beneficiery Name
            '
            'PrintReceipt
            'in printing utr main order no


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

    Protected Sub btnGrdPrint_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim btn As LinkButton = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("LinkButton3"), LinkButton)
            Response.Redirect("../Admin/PrintReceipt.aspx?Type=" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text) & "&ID=" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text))


            'lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
            'lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

            'lblPopDateTime.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text) & " " & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            'lblPopTransactionID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            'lblPopTransferAmt.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            'lblPopStatus.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(13).Text)
            'lblpopMobileNo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
            'lblpopBankName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
            'ModalPopupExtender3.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Try
            btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

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
                    GV.ExportToExcel_New(GridView1, Response, "", "MoneyTransferDetails", lblExportQry.Text, "dyanamic")
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
                    GV.ExportToWord_New(GridView1, Response, "MoneyTransferDetails", lblExportQry.Text, "dyanamic")
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
                    GV.ExportToPdf_New(GridView1, "", Response, "MoneyTransferDetails  ", lblExportQry.Text, "dyanamic")
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