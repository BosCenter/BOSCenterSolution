
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net




Public Class BOS_ApproveRefund
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim Filter As String = ""

            Dim TypeFilter As String = ""

            If ddlTransType.SelectedValue = "All Type" Then
                TypeFilter = ""
            Else
                TypeFilter = " TransType='" & ddlTransType.SelectedValue & "' and "
            End If

            If ddlSelectCriteria.SelectedValue.Trim.ToUpper = "All Records".Trim.ToUpper Then
                Filter = ""
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "RequestID".Trim.ToUpper Then
                Filter = " And RequestID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Agent ID".Trim.ToUpper Then
                Filter = " And kCode ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "TransactionID".Trim.ToUpper Then
                Filter = " And TransID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            End If
            Querystring = ""

            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
                'Querystring = "select RID as SrNo,RefrenceID,RegistrationId,PaymentMode,(CONVERT(VARCHAR(11),PaymentDate,106)) as PaymentDate,DepositBankName,BranchCode_ChecqueNo as 'BranchCode/ChequeNo',Remarks,TransactionID,isnull(DocumentPath,'') as DocumentPath ,ApporvedStatus,ApporveRemakrs as 'AdminRemarks',Amount,CompanyCode  from BosCenter_DB.dbo.BOS_MakePayemnts_Details_SA where ApporvedStatus='Pending'  " & Filter & " order by RID Desc"
            Else
                'admin and other employees 
                Querystring = "select RID as 'SrNo',RequestID,(CONVERT(VARCHAR(11),RequestDate,106)) as 'RequestDate',	kCode as 'AgentID',kCodeType as 'AgentType',(select top 1 AgencyName from BOS_Dis_SubDis_Retailer_Registration RR where RR.RegistrationId=RM.kCode)  as 'AgencyName',TransType,TransID,Amount,Remarks,ApporvedStatus from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Refund_Request_Master RM where " & TypeFilter & " ApporvedStatus='Pending' " & Filter & "  order by rid desc"
            End If



            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        'Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                        'Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                        'If lblAttachment.Text = "" Then
                        '    lnkdwnload.Visible = False
                        'Else
                        '    lnkdwnload.Visible = True
                        'End If
                        GridView1.Rows(i).Cells(1).Text = i + 1
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
                Bind()
            End If

        Catch ex As Exception

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
            lblTransType.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            lblTransID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
            txtApprovedAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            lblTransferAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            lblAgentID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)

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



    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "ApproveRefundDetails")
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "ApproveRefundDetails")
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            GV.ExportToWord(GridView1, Response, "ApproveRefundDetails")
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

                Dim VRemark As String
                Dim VUpdatedBy, VUpdatedOn, LoginID, VTransID, VApproveStatus As String
                LoginID = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                Dim DBName As String = GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim

                VUpdatedBy = LoginID
                VUpdatedOn = "getdate()"
                VTransID = lblTransID.Text.Trim
                VApproveStatus = ddlStatus.SelectedValue.Trim


                If Not txtRemarks.Text = "" Then
                    VRemark = GV.parseString(txtRemarks.Text.Trim)
                Else
                    VRemark = ""
                End If

                Dim str As String = ""



                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin

                Else
                    'admin and other employees 

                    If VApproveStatus.Trim.ToUpper = "Approved".Trim.ToUpper Then
                        Dim Refund_TransID As String = GV.FL_AdminLogin.getAutoNumber("TransId")
                        str = "update " & DBName.Trim & ".dbo.BOS_Refund_Request_Master set ApporvedStatus='" & VApproveStatus & "',Refund_TransID='" & Refund_TransID & "', ApporveRemarks='" & VRemark & "', ApprovedBy='" & LoginID & "',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "

                        If GV.parseString(lblTransType.Text.Trim).ToUpper = "Recharge & Bill Payment".Trim.ToUpper Then
                            str = str & " insert into " & DBName & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) select Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount, '" & Refund_TransID & "' as Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg=Replace(Replace(Replace(TransferFromMsg,'RECH','RECH-Refund'),'BILLPAY','BILLPAY-Refund'),'Landline','Landline-Refund'),TransferFromMsg=Replace(Replace(Replace(TransferToMsg,'RECH','RECH-Refund'),'BILLPAY','BILLPAY-Refund'),'Landline','Landline-Refund'),Amount_Type=Amount_Type+'-Refund',Remark=Remark+'-Refund',TransactionDate,TransferFrom=TransferTo,TransferTo=TransferFrom,TransferAmt,getdate() as 'RecordDateTime','Admin' as 'UpdatedBy',getdate()  as 'UpdatedOn' from " & DBName & ".dbo.BOS_TransferAmountToAgents where Ref_TransID='" & VTransID & "' ; "

                            str = str & " update " & DBName & ".dbo.BOS_Recharge_API set Refund_Status='Yes',Refund_Req_Status='" & VApproveStatus & "',Refund_TransID='" & Refund_TransID & "' where TransId='" & VTransID & "'  and RetailerID='" & lblAgentID.Text.Trim & "' ;"
                        ElseIf GV.parseString(lblTransType.Text.Trim).ToUpper = "Money Transfer".Trim.ToUpper Then
                            str = str & " insert into " & DBName & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) select Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount, '" & Refund_TransID & "' as Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg=Replace(TransferFromMsg,'Money Transfer','Money Transfer-Refund'),TransferFromMsg=Replace(TransferToMsg,'Money Transfer','Money Transfer-Refund'),Amount_Type=Amount_Type+'-Refund',Remark=Remark+'-Refund',TransactionDate,TransferFrom=TransferTo,TransferTo=TransferFrom,TransferAmt,getdate() as 'RecordDateTime','Admin' as 'UpdatedBy',getdate()  as 'UpdatedOn' from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amt_Transfer_TransID='" & VTransID & "' or Ref_TransID='" & VTransID & "' ; "

                            str = str & " update " & DBName & ".dbo.BOS_MoneyTransfer_API set Refund_Status='Yes',Refund_Req_Status='" & VApproveStatus & "',Refund_TransID='" & Refund_TransID & "'  where TransID='" & VTransID & "' and RefrenceNo='" & lblAgentID.Text.Trim & "';"

                        ElseIf GV.parseString(lblTransType.Text.Trim).ToUpper = "Pan Card".Trim.ToUpper Then
                            str = str & " insert into " & DBName & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) select Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount, '" & Refund_TransID & "' as Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg=Replace(TransferFromMsg,'PAN CARD','PAN CARD-Refund'),TransferFromMsg=Replace(TransferToMsg,'PAN CARD','PAN CARD-Refund'),Amount_Type=Amount_Type+'-Refund',Remark=Remark+'-Refund',TransactionDate,TransferFrom=TransferTo,TransferTo=TransferFrom,TransferAmt,getdate() as 'RecordDateTime','Admin' as 'UpdatedBy',getdate()  as 'UpdatedOn' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  API_TransId='" & VTransID & "' ; "

                            str = str & " update " & DBName & ".dbo.BOS_Pan_Card_API set Refund_Status='Yes',Refund_Req_Status='" & VApproveStatus & "',Refund_TransID='" & Refund_TransID & "'  where TransID='" & VTransID & "' and AgentID='" & lblAgentID.Text.Trim & "';"

                        End If
                    Else
                        str = "update " & DBName.Trim & ".dbo.BOS_Refund_Request_Master set ApporvedStatus='" & VApproveStatus & "', ApporveRemarks='" & VRemark & "', ApprovedBy='" & LoginID & "',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "

                        If GV.parseString(lblTransType.Text.Trim).ToUpper = "Recharge & Bill Payment".Trim.ToUpper Then
                            str = str & " update " & DBName & ".dbo.BOS_Recharge_API set Refund_Req_Status='" & VApproveStatus & "' where TransId='" & VTransID & "'  and RetailerID='" & lblAgentID.Text.Trim & "' ;"
                        ElseIf GV.parseString(lblTransType.Text.Trim).ToUpper = "Money Transfer".Trim.ToUpper Then
                            str = str & " update " & DBName & ".dbo.BOS_MoneyTransfer_API set Refund_Req_Status='" & VApproveStatus & "'  where TransID='" & VTransID & "' and RefrenceNo='" & lblAgentID.Text.Trim & "';"
                        ElseIf GV.parseString(lblTransType.Text.Trim).ToUpper = "Pan Card".Trim.ToUpper Then
                            str = str & " update " & DBName & ".dbo.BOS_Pan_Card_API set Refund_Req_Status='" & VApproveStatus & "'  where TransID='" & VTransID & "' and AgentID='" & lblAgentID.Text.Trim & "';"
                        End If

                    End If

                End If

                If Not str.Trim = "" Then
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
                End If

                ModalPopupExtender1.Show()
            End If

        Catch ex As Exception

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
            Bind()
            ddlNoOfRecords.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""

            If ddlStatus.SelectedValue = "Approved" Then
                txtApprovedAmount.Text = lblTransferAmount.Text.Trim
            Else
                txtApprovedAmount.Text = "0"
            End If

            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlTransType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransType.SelectedIndexChanged
        Try
            Bind()
        Catch ex As Exception

        End Try
    End Sub
End Class