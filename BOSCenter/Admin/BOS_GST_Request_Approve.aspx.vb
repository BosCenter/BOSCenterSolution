
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net




Public Class BOS_GST_Request_Approve
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim Filter As String = ""

            If ddlSelectCriteria.SelectedValue.Trim.ToUpper = "All Records".Trim.ToUpper Then
                Filter = ""
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Request ID".Trim.ToUpper Then
                Filter = " And RefrenceID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Agent ID".Trim.ToUpper Then
                Filter = " And RegistrationId ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            End If

            Querystring = "select RID as SrNo,RefrenceID as 'Request ID',(CONVERT(VARCHAR(11),RecordDateTime,106)) as 'RequestDate',RegistrationId as 'Agent ID',GST_Month,(CONVERT(VARCHAR(11),FromDate,106)) as FromDate,(CONVERT(VARCHAR(11),ToDate,106)) as ToDate,Remarks,CommAmt,GSTAmt,GrossAmt,TDS_Amt,CreditBalance,ApporvedStatus from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_GST_Refund_Details where ApporvedStatus='Pending'  " & Filter & " order by RID Desc"

            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
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
                Bind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub UpdateDetails_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            txtRemarks.Text = ""
            ddlStatus.SelectedIndex = 0
            lblTransferAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
            lblTrasferTo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            lblRefrenceID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text) 'RefID
            lblGSTMonth.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)




            'ddlStatus.SelectedValue = ComplaintStatus
            'If ComplaintStatus = "Closed" Then
            '    txtRemarks.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text)
            'End If

            btnCancel.Visible = True
            btnCancel.Text = "Cancel"
            btnUpdate.Visible = True
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""
            ModalPopupExtender1.Show()

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
            GV.ExportToExcel(GridView1, Response, "ApproveGSTRefund")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "ApproveGSTRefund")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            GV.ExportToWord(GridView1, Response, "ApproveGSTRefund")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
                Dim VTransferAmt, VTransferFromMsg, VTransferToMsg, SMSMeassgeTo, VRemark, VTransferFrom, VTransferTo As String
                Dim VUpdatedBy, VUpdatedOn As String

                VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

                VUpdatedOn = "getdate()"
                VTransferAmt = ""
                VTransferFrom = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                VTransferTo = GV.parseString(lblTrasferTo.Text.Trim)
                If Not txtRemarks.Text = "" Then
                    VRemark = GV.parseString(txtRemarks.Text.Trim)
                Else
                    VRemark = ""
                End If

                If Not lblTransferAmount.Text = "" Then
                    VTransferAmt = GV.parseString(lblTransferAmount.Text.Trim)
                End If
                Dim str As String
                str = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_GST_Refund_Details set ApporvedStatus='" & GV.parseString(ddlStatus.SelectedValue.Trim) & "', ApporveRemakrs='" & VRemark & "', ApprovedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "
                If ddlStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then

                    Dim V_Amt_Transfer_TransID As String = GV.FL.getAutoNumber("TransId")

                    VTransferFromMsg = "Your Wallet is Debited For GST Refund -(" & lblGSTMonth.Text & ") By Agent ID (" & lblTrasferTo.Text & ")"
                    VTransferToMsg = "Your Wallet is Credited For GST Refund -(" & lblGSTMonth.Text & ") by BOS CENTER PVT LTD"
                    SMSMeassgeTo = "Your Wallet is Credited With Rs. " & VTransferAmt & " By BOS CENTER PVT LTD"
                    str = str & "  " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferAmt & "','" & lblRefrenceID.Text.Trim & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','GSTRefund','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                End If

                Dim result As Boolean = GV.FL.DMLQueriesBulk(str)
                lblDialogMsg.Text = result
                If result = True Then
                    lblDialogMsg.Text = "Record Updated Successfully."
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
            Bind()
            ddlNoOfRecords.SelectedIndex = 0
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class