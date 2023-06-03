
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net




Public Class BOS_MakePayment_Report_SA
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
            GridView1.Columns(2).Visible = True
            Dim GROUPFilter As String = ""

            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
                Exit Sub
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
                GROUPFilter = ""
            Else
                'admin and other employees 
                GROUPFilter = " CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "' "
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
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "RegistrationId".Trim.ToUpper Then
                Filter = "  RegistrationId ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "TransactionID".Trim.ToUpper Then
                Filter = "  TransactionID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "RefrenceID".Trim.ToUpper Then
                Filter = "  RefrenceID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "PaymentMode".Trim.ToUpper Then
                Filter = "  PaymentMode like '" & GV.parseString(txtSearchingValue.Text.Trim) & "%'"
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

            Dim durationDate As String = ""
            If chkduration.Checked = True Then
                If Status.Trim = "" And condition.Trim = "" And Filter.Trim = "" And GROUPFilter.Trim = "" Then
                    durationDate = " where  PaymentDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'"
                Else
                    durationDate = " and  PaymentDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'"
                End If
            End If

            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                Querystring = "select RID as SrNo,RefrenceID,RegistrationId,PaymentMode,(CONVERT(VARCHAR(11),PaymentDate,106)) as PaymentDate,DepositBankName,BranchCode_ChecqueNo as 'BranchCode/ChequeNo',Remarks,TransactionID,isnull(DocumentPath,'') as DocumentPath ,ApporvedStatus as 'Status',Amount,CompanyCode  from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_MakePayemnts_Details_SA " & Status & " " & condition & "  " & Filter & " " & GROUPFilter & " " & durationDate & " order by rid desc"
                'super admin
            Else
                'admin and other employees 
                Querystring = "select RID as SrNo,RefrenceID,RegistrationId,PaymentMode,(CONVERT(VARCHAR(11),PaymentDate,106)) as PaymentDate,DepositBankName,BranchCode_ChecqueNo as 'BranchCode/ChequeNo',Remarks,TransactionID,isnull(DocumentPath,'') as DocumentPath ,ApporvedStatus as 'Status',Amount,CompanyCode  from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_MakePayemnts_Details_SA " & Status & " " & condition & "  " & Filter & " " & GROUPFilter & " " & durationDate & " order by rid desc"
            End If


            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 0)
                    Dim TotalSum As Decimal = 0

                    If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                        'dis,sd,re,cust
                        Exit Sub
                    ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        'super admin
                        If Not Status.Trim = "" Then
                            For i As Integer = 0 To GridView1.Rows.Count - 1
                                TotalSum = TotalSum + CDec(GV.parseString(GridView1.Rows(i).Cells(5).Text))
                            Next
                            GridView1.FooterRow.Cells(4).ForeColor = Drawing.Color.Blue
                            GridView1.FooterRow.Cells(4).Font.Bold = True

                            GridView1.FooterRow.Cells(5).ForeColor = Drawing.Color.Blue
                            GridView1.FooterRow.Cells(5).Font.Bold = True


                            GridView1.FooterRow.Cells(4).Text = "Total Sum"
                            GridView1.FooterRow.Cells(5).Text = TotalSum
                        End If

                    Else
                        'admin and other employees 
                        GridView1.Columns(2).Visible = False
                    End If


                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                        Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                        If lblAttachment.Text = "" Then
                            lnkdwnload.Visible = False
                        Else
                            lnkdwnload.Visible = True
                        End If
                    Next



                Else

                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Or grp.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    Exit Sub
                ElseIf grp.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                Else
                    ddlApproveStatus.Items.Remove("RefrenceID")
                End If

                Bind()
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

    'Protected Sub UpdateDetails_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim btndetails As LinkButton = TryCast(sender, LinkButton)
    '        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
    '        Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
    '        lblRID.Text = lbl.Text.Trim
    '        txtRemarks.Text = ""
    '        ddlStatus.SelectedIndex = 0
    '        'Dim ComplaintStatus As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
    '        'ddlStatus.SelectedValue = ComplaintStatus
    '        'If ComplaintStatus = "Closed" Then
    '        '    txtRemarks.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text)
    '        'End If

    '        btnCancel.Visible = True
    '        btnCancel.Text = "Cancel"
    '        btnUpdate.Visible = True
    '        lblDialogMsg.Text = ""
    '        lblDialogMsg.CssClass = ""
    '        ModalPopupExtender1.Show()

    '    Catch ex As Exception

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

    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            GV.ExportToWord(GridView1, Response, "ApprovePayment")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""


            If Not lblRID.Text = "" Then
                If txtRemarks.Text = "" Then
                    lblDialogMsg.Text = "Pls Enter Remarks."
                    lblDialogMsg.CssClass = "errorlabels"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If
                Dim str As String = ""




                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                    str = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_MakePayemnts_Details_SA set ApporvedStatus='" & GV.parseString(ddlStatus.SelectedValue.Trim) & "', Remarks='" & GV.parseString(txtRemarks.Text.Trim) & "', ApprovedBy='Super Admin',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " "
                Else
                    'admin and other employees 
                End If


                If Not str.Trim = "" Then
                    Dim result As Boolean = GV.FL.DMLQueries(str)
                    lblDialogMsg.Text = result
                    If result = True Then
                        lblDialogMsg.Text = "Record Updated Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        Bind()

                    Else
                        lblDialogMsg.Text = "Sorry !! Record Updation Failed."
                        lblDialogMsg.CssClass = "errorlabels"
                    End If
                    btnCancel.Text = "OK"
                    btnUpdate.Visible = False
                End If

                ModalPopupExtender1.Show()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
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


            If Not ddlSelectCriteria.SelectedValue = "All Records" And GV.parseString(txtSearchingValue.Text.Trim) = "" Then
                lblError1.Text = "Enter Search value"
                lblError1.CssClass = "errorlabels"
                Exit Sub
            End If
            Bind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DownloadRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lblAttachment As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("lblAttachment"), Label)
            DownloadDoc(lblAttachment.Text)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub DownloadDoc(ByVal DocPath As String)
        Try

            Dim fi As New FileInfo(DocPath)
            Dim strURL As String = DocPath
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" & fi.Name & """")
            Dim data As Byte() = req.DownloadData(Server.MapPath(strURL))
            response.BinaryWrite(data)


            response.[End]()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            clear()
        Catch ex As Exception

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

        End Try
    End Sub

End Class