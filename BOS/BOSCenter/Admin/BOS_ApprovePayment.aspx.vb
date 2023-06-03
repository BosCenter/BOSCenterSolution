
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net




Public Class BOS_ApprovePayment
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
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Register ID".Trim.ToUpper Then
                Filter = " And RegistrationId ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "TransactionID".Trim.ToUpper Then
                Filter = " And TransactionID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "PaymentMode".Trim.ToUpper Then
                Filter = " And PaymentMode like '" & GV.parseString(txtSearchingValue.Text.Trim) & "%'"
            End If
            Querystring = ""

            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                'dis,sd,re,cust
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                'super admin
                Querystring = "select RID as SrNo,RefrenceID,RegistrationId,PaymentMode,(CONVERT(VARCHAR(11),PaymentDate,106)) as PaymentDate,DepositBankName,BranchCode_ChecqueNo as 'BranchCode/ChequeNo',Remarks,TransactionID,isnull(DocumentPath,'') as DocumentPath ,ApporvedStatus,ApporveRemakrs as 'AdminRemarks',Amount,CompanyCode  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_MakePayemnts_Details_SA where ApporvedStatus='Pending'  " & Filter & " order by RID Desc"
            Else
                'admin and other employees 
                Querystring = "select RID as SrNo,RefrenceID,RegistrationId,PaymentMode,(CONVERT(VARCHAR(11),PaymentDate,106)) as PaymentDate,DepositBankName,BranchCode_ChecqueNo as 'BranchCode/ChequeNo',Remarks,TransactionID,isnull(DocumentPath,'') as DocumentPath ,ApporvedStatus,ApporveRemakrs as 'AdminRemarks',Amount,CompanyCode  from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details where ApporvedStatus='Pending'  " & Filter & " order by RID Desc"
            End If



            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                        Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                        If lblAttachment.Text = "" Then
                            lnkdwnload.Visible = False
                        Else
                            lnkdwnload.Visible = True
                        End If
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
            lblTransferAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            lblTrasferTo.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            lblRefrenceID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) 'RefID

            txtDeductAmt.Text = "0"
            txtApprovedAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)

            lblCompanyCode.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(13).Text)

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
                Dim str As String = ""



                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                    'dis,sd,re,cust
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'super admin
                    str = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_MakePayemnts_Details_SA set ApporvedStatus='" & GV.parseString(ddlStatus.SelectedValue.Trim) & "', ApporveRemakrs='" & VRemark & "', ApprovedBy='Super Admin',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "
                    If ddlStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then

                        Dim DBName As String = GV.FL_AdminLogin.AddInVar("DatabaseName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & lblCompanyCode.Text.Trim & "' ")

                        If Not DBName.Trim = "" Then
                            Dim V_Amt_Transfer_TransID As String = GV.FL.getAutoNumber("TransId")
                            VTransferFrom = "Super Admin"
                            VTransferTo = "Admin"

                            VTransferFromMsg = "Your Wallet is Debited by Admin (" & lblCompanyCode.Text.Trim & ")"
                            VTransferToMsg = "Your Wallet is Credited by BOS CENTER PVT LTD"
                            SMSMeassgeTo = "Your Wallet is Credited With Rs. " & VTransferAmt & " By BOS CENTER PVT LTD"
                            str = str & "  " & "insert into " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & lblTransferAmount.Text.Trim & "','" & lblRefrenceID.Text.Trim & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','MakePayment','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                            If CDec(txtDeductAmt.Text.Trim) > 0 Then
                                'Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                Dim VTo As String = "Your Account is credited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                                str = str & " " & "insert into " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(V_Amt_Transfer_TransID) & "','" & lblTransferAmount.Text.Trim & "','" & GV.parseString(lblRefrenceID.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & VTransferTo & "','" & VTransferFrom & "','" & txtDeductAmt.Text.Trim & "',getdate(),'" & VUpdatedBy & "',getdate() ) ;"
                            End If
                        End If



                    End If
                Else
                    'admin and other employees 
                    str = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MakePayemnts_Details set ApporvedStatus='" & GV.parseString(ddlStatus.SelectedValue.Trim) & "', ApporveRemakrs='" & VRemark & "', ApprovedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',ApprovedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ; "
                    If ddlStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then

                        Dim V_Amt_Transfer_TransID As String = GV.FL.getAutoNumber("TransId")
                        VTransferFromMsg = "Your Wallet is Debited by Distributor (" & VTransferFrom & ")"
                        VTransferToMsg = "Your Wallet is Credited by BOS CENTER PVT LTD"
                        SMSMeassgeTo = "Your Wallet is Credited With Rs. " & VTransferAmt & " By BOS CENTER PVT LTD"
                        str = str & "  " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & lblTransferAmount.Text.Trim & "','" & lblRefrenceID.Text.Trim & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','MakePayment','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                        If CDec(txtDeductAmt.Text.Trim) > 0 Then
                            'Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                            Dim VTo As String = "Your Account is credited by ServiceCharge " & txtDeductAmt.Text.Trim & " Rs. Due to MakePayment / AMT " & lblTransferAmount.Text.Trim & "."
                            str = str & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(V_Amt_Transfer_TransID) & "','" & lblTransferAmount.Text.Trim & "','" & GV.parseString(lblRefrenceID.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & VTransferTo & "','" & VTransferFrom & "','" & txtDeductAmt.Text.Trim & "',getdate(),'" & VUpdatedBy & "',getdate() ) ;"
                        End If

                    End If
                End If

                If Not str.Trim = "" Then
                    Dim result As Boolean = GV.FL.DMLQueriesBulk(str)
                    lblDialogMsg.Text = result
                    If result = True Then

                        '// wallet transfer ke case main jo message ja raha hai 

                        '// will not shot in reject mode
                        If ddlStatus.SelectedValue.Trim.ToUpper = "Approved".Trim.ToUpper Then
                            If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then

                                Dim vName As String = GV.FL.AddInVar("isnull(FirstName,'') + ' ' + isnull(LastName,'')", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")
                                Dim vBal As String = GV.AgentBalance(VTransferTo, GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim).ToString
                                Dim ToMobile As String = GV.FL.AddInVar("MobileNo", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")

                                If GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim = "CMP1165" Then
                                    Dim vMessage As String = "Dear " & vName & " Your Fund Request Has Been Successfully Approved Of Rs. " & VTransferAmt & " . Thanks For Using Kuber Money"
                                    GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Approve Submit Payment", "CMP1165")

                                ElseIf GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim = "CMP1174" Then
                                    Dim vMessage As String = "Dear " & vName & " Your Fund Request Has Been Successfully Approved Of Rs. " & VTransferAmt & " . Thanks For Using Easy Talk. Web: https://bit.ly/3p1OkPj App: https://bit.ly/3Szblqb"
                                    GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Approve Submit Payment", "CMP1174")
                                Else
                                    Dim vMessage As String = "Dear " & vName & " Your Fund Request Has Been Successfully Approved Of Rs. " & VTransferAmt & " . Thanks For Using BOS."
                                    GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Approve Submit Payment", "")
                                End If
                            End If
                        End If


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

    Private Sub txtDeductAmt_TextChanged(sender As Object, e As System.EventArgs) Handles txtDeductAmt.TextChanged
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""

            If GV.parseString(txtDeductAmt.Text.Trim) = "" Then
                txtDeductAmt.Text = "0"
            End If

            If GV.parseString(lblTransferAmount.Text.Trim) = "" Then
                lblTransferAmount.Text = "0"
            End If


            If CLng(txtDeductAmt.Text.Trim) >= CLng(lblTransferAmount.Text.Trim) Then
                lblDialogMsg.Text = "Deduction Can't be More Or Equal To Requested Amt."
                lblDialogMsg.CssClass = "errorlabels"
                txtDeductAmt.Focus()
            Else
                txtApprovedAmount.Text = CLng(lblTransferAmount.Text.Trim) - CLng(txtDeductAmt.Text.Trim)
            End If

            ModalPopupExtender1.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""

            If ddlStatus.SelectedValue = "Approved" Then
                txtDeductAmt.Text = "0"
                txtDeductAmt.ReadOnly = False
                txtApprovedAmount.Text = lblTransferAmount.Text.Trim
            Else
                txtDeductAmt.Text = "0"
                txtDeductAmt.ReadOnly = True
                txtApprovedAmount.Text = "0"
            End If

            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub
End Class