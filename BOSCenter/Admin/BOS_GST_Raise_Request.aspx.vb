Public Class BOS_GST_Raise_Request
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                MonthWise()


                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    Div_Criteria.Visible = False
                    lblHeading.Text = "GST Raise Request Form"
                ElseIf group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    Div_Criteria.Visible = False
                    lblHeading.Text = "GST Raise Request Form"
                Else
                    Div_Criteria.Visible = True
                    lblHeading.Text = "GST Raise Request Form"
                End If

                ddlSelectCriteria.Items.Clear()


                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    ddlSelectCriteria.Items.Add("Not Applicable")
                ElseIf group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    ddlSelectCriteria.Items.Add("Not Applicable")
                ElseIf group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    ddlSelectCriteria.Items.Add("Not Applicable")
                    ddlSelectCriteria.Items.Add("Distributor")
                ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    ddlSelectCriteria.Items.Add("Not Applicable")
                    ddlSelectCriteria.Items.Add("Retailer")
                Else
                    ddlSelectCriteria.Items.Add("Not Applicable")
                    ddlSelectCriteria.Items.Add("Master Distributor")
                    ddlSelectCriteria.Items.Add("Distributor")
                    ddlSelectCriteria.Items.Add("Retailer")
                End If
                ddlSelectCriteria_SelectedIndexChanged(sender, e)

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

            lblSearchHeading.Text = " Search Details ::"


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

            Dim FromMonthNo, ToMonthNo As Integer
            FromMonthNo = GV.returnMonthNumber(ddlFromMonth.SelectedValue)
            ToMonthNo = GV.returnMonthNumber(ddlToMonth.SelectedValue)

            'jan 1 aug 8

            If FromMonthNo > ToMonthNo Then
                clear()
                lblError0.Text = "To Month Cannot Be Smaller Then From Month"
                lblError0.CssClass = "errorlabels"
            Else
                lblError0.Text = ""
                lblError0.CssClass = ""
            End If


            If Not (lblError0.Text.Trim = "") Then
                Exit Sub
            End If

            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            MonthWise()
            clear()
            lblExportQry.Text = ""
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
            Dim loginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            Dim AmountType As String = ""
            AmountType = " And Amount_Type='Commission' "

            Dim searchID As String = ""
            If Not ddlvalue.SelectedValue.Trim.ToUpper = "Not Applicable".ToUpper Then
                Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                searchID = aa(0)
                lblSearchHeading.Text = " Search Details of - ( " & GV.parseString(ddlvalue.SelectedValue.Trim).ToUpper & " )"
            Else
                searchID = loginID
                lblSearchHeading.Text = " Search Details of Self"
            End If

            Dim FromMonthNo, ToMonthNo As Integer
            FromMonthNo = GV.returnMonthNumber(ddlFromMonth.SelectedValue)
            ToMonthNo = GV.returnMonthNumber(ddlToMonth.SelectedValue)
            Dim FromDate, ToDate As Date
            Dim TotalRows As Integer = (ToMonthNo - FromMonthNo) '+ 1

            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("SrNo")
            Dim dc2 As DataColumn = New DataColumn("Month")
            Dim dc3 As DataColumn = New DataColumn("FromDate")
            Dim dc4 As DataColumn = New DataColumn("ToDate")
            Dim dc5 As DataColumn = New DataColumn("Remarks")
            Dim dc6 As DataColumn = New DataColumn("CommAmt")
            Dim dc7 As DataColumn = New DataColumn("GSTAmt")
            Dim dc8 As DataColumn = New DataColumn("GrossAmt")
            Dim dc9 As DataColumn = New DataColumn("TDS_Amt")
            Dim dc10 As DataColumn = New DataColumn("CreditBalance")

            'CommAmt	GSTAmt	GrossAmt	TDS_Amt	CreditBalance

            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            dt.Columns.Add(dc5)
            dt.Columns.Add(dc6)
            dt.Columns.Add(dc7)
            dt.Columns.Add(dc8)
            dt.Columns.Add(dc9)
            dt.Columns.Add(dc10)

            For i As Integer = 0 To TotalRows
                Dim dr1 As DataRow = dt.NewRow()
                dr1(0) = i + 1
                dr1(1) = GV.GetMonthName(FromMonthNo + i) & " - " & ddlSelectYears.SelectedValue
                dr1(2) = "1/" & (FromMonthNo + i) & "/" & ddlSelectYears.SelectedValue
                dr1(3) = GV.returnDaysInMonth(ddlSelectYears.SelectedValue, (FromMonthNo + i)) & "/" & FromMonthNo + i & "/" & ddlSelectYears.SelectedValue
                dr1(4) = GV.parseString("Commission")

                FromDate = GV.FL.returnDateMonthWise(dr1(2))
                ToDate = GV.FL.returnDateMonthWise(dr1(3))



                Querystring = " (select (isnull(sum(Actual_Commission_Amt),0) - (select isnull(sum(Actual_Commission_Amt),0) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & searchID & "' and TransactionDate between  '" & FromDate & "' and '" & ToDate & "' and Amount_Type='Commission') ) as 'CommAmt',"
                Querystring = Querystring & " (isnull(sum(GSTAmt),0) - (select isnull(sum(GSTAmt),0) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & searchID & "' and TransactionDate between  '" & FromDate & "' and '" & ToDate & "' and Amount_Type='Commission') ) as 'GSTAmt', "
                Querystring = Querystring & " (isnull(sum(Commission_Without_GST),0)-  (select isnull(sum(Commission_Without_GST),0) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & searchID & "' and TransactionDate between  '" & FromDate & "' and '" & ToDate & "' and Amount_Type='Commission') ) as 'GrossAmt', "
                Querystring = Querystring & " (isnull(sum(TDS_Amt),0) -(select isnull(sum(TDS_Amt),0) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & searchID & "' and TransactionDate between  '" & FromDate & "' and '" & ToDate & "' and Amount_Type='Commission') ) as 'TDS_Amt', "
                Querystring = Querystring & " (isnull(sum(TransferAmt),0) - (select isnull(sum(TransferAmt),0) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & searchID & "' and TransactionDate between  '" & FromDate & "' and '" & ToDate & "' and Amount_Type='Commission') ) as 'CreditBalance'"
                Querystring = Querystring & "  from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents   where  TransferTo='" & searchID & "' and TransactionDate between  '" & FromDate & "' and '" & ToDate & "' and Amount_Type='Commission' ) "

                Dim LocalDS As New DataSet
                LocalDS = GV.FL.OpenDsWithSelectQuery(Querystring)

                If Not LocalDS Is Nothing Then
                    If LocalDS.Tables.Count > 0 Then
                        If LocalDS.Tables(0).Rows.Count > 0 Then
                            'CommAmt	GSTAmt	GrossAmt	TDS_Amt	CreditBalance
                            If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("CommAmt")) Then
                                If Not LocalDS.Tables(0).Rows(0).Item("CommAmt").ToString() = "" Then
                                    dr1(5) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("CommAmt").ToString())
                                Else
                                    dr1(5) = "0"
                                End If
                            Else
                                dr1(5) = "0"
                            End If

                            If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("GSTAmt")) Then
                                If Not LocalDS.Tables(0).Rows(0).Item("GSTAmt").ToString() = "" Then
                                    dr1(6) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("GSTAmt").ToString())
                                Else
                                    dr1(6) = "0"
                                End If
                            Else
                                dr1(6) = "0"
                            End If

                            If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("GrossAmt")) Then
                                If Not LocalDS.Tables(0).Rows(0).Item("GrossAmt").ToString() = "" Then
                                    dr1(7) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("GrossAmt").ToString())
                                Else
                                    dr1(7) = "0"
                                End If
                            Else
                                dr1(7) = "0"
                            End If

                            If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("TDS_Amt")) Then
                                If Not LocalDS.Tables(0).Rows(0).Item("TDS_Amt").ToString() = "" Then
                                    dr1(8) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("TDS_Amt").ToString())
                                Else
                                    dr1(8) = "0"
                                End If
                            Else
                                dr1(8) = "0"
                            End If

                            If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("CreditBalance")) Then
                                If Not LocalDS.Tables(0).Rows(0).Item("CreditBalance").ToString() = "" Then
                                    dr1(9) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("CreditBalance").ToString())
                                Else
                                    dr1(9) = "0"
                                End If
                            Else
                                dr1(9) = "0"
                            End If
                        Else
                            dr1(5) = "0"
                            dr1(6) = "0"
                            dr1(7) = "0"
                            dr1(8) = "0"
                            dr1(9) = "0"
                        End If
                    Else
                        dr1(5) = "0"
                        dr1(6) = "0"
                        dr1(7) = "0"
                        dr1(8) = "0"
                        dr1(9) = "0"
                    End If
                Else
                    dr1(5) = "0"
                    dr1(6) = "0"
                    dr1(7) = "0"
                    dr1(8) = "0"
                    dr1(9) = "0"
                End If
                dt.Rows.Add(dr1)

            Next
            GridView1.DataSource = dt
            GridView1.DataBind()


            Dim CommAmt, GSTAmt, GrossAmt, TDS_Amt, CreditBalance As Decimal

            CommAmt = 0
            GSTAmt = 0
            GrossAmt = 0
            TDS_Amt = 0
            CreditBalance = 0

            For i As Integer = 0 To GridView1.Rows.Count - 1

                If Not ddlvalue.SelectedValue.Trim.ToUpper = "Not Applicable".ToUpper Then
                    '/// All Visible False
                    Dim btn As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("lnkBtnRaiseRequest"), LinkButton)
                    If Not btn Is Nothing Then
                        btn.Visible = False
                    End If
                Else
                    '/// Check if approved state is approve / pending / rejected
                    Dim btn As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("lnkBtnRaiseRequest"), LinkButton)
                    If Not btn Is Nothing Then

                        If CDec(GridView1.Rows(i).Cells(7).Text) > 0 Then
                            Dim vApporvedStatus As String = GV.FL.AddInVar("top 1 ApporvedStatus", "  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_GST_Refund_Details where RegistrationId='" & loginID & "' and GST_Month='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' order by rid desc ")
                            If vApporvedStatus.Trim.ToUpper = "Pending".ToUpper Then
                                btn.Text = "Pending"
                                btn.CssClass = "btn btn-warning mar_top5"
                                btn.Enabled = False
                            ElseIf vApporvedStatus.Trim.ToUpper = "Approved".ToUpper Then
                                btn.Text = "Approved"
                                btn.CssClass = "btn btn-success mar_top5"
                                btn.Enabled = False
                            ElseIf vApporvedStatus.Trim.ToUpper = "Rejected".ToUpper Then
                            Else
                            End If
                        Else
                            btn.Visible = False
                        End If



                    End If



                End If



                CommAmt = CommAmt + CDec(GridView1.Rows(i).Cells(6).Text)
                GSTAmt = GSTAmt + CDec(GridView1.Rows(i).Cells(7).Text)
                GrossAmt = GrossAmt + CDec(GridView1.Rows(i).Cells(8).Text)
                TDS_Amt = TDS_Amt + CDec(GridView1.Rows(i).Cells(9).Text)
                CreditBalance = CreditBalance + CDec(GridView1.Rows(i).Cells(10).Text)
            Next
            GridView1.FooterRow.Cells(5).Text = "Total"
            GridView1.FooterRow.Cells(6).Text = CommAmt
            GridView1.FooterRow.Cells(7).Text = GSTAmt
            GridView1.FooterRow.Cells(8).Text = GrossAmt
            GridView1.FooterRow.Cells(9).Text = TDS_Amt
            GridView1.FooterRow.Cells(10).Text = CreditBalance


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblNoRecords.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnGrdRow_Raise_Request_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex

            btnok.Text = "Yes"
            btnok.Visible = True
            lblDialogMsg.CssClass = ""
            btnCancel.Text = "No"

            lblDialogMsg.Text = "Do You Want To Raise GST Request?"
            MPE_RaiseRequest.Show()


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btn_PopUp_Raise_Request_Yes_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try


            '//// ----------------Variable Declaration  ----------------
            Dim VRefrenceID, VRegistrationId, VGST_Month, VFromDate, VToDate, VRemarks, VCommAmt, VGSTAmt, VGrossAmt, VTDS_Amt, VCreditBalance, VApprovedBy, VApprovedDateTime, VApporvedStatus, VApporveRemakrs, VRecordDateTime, VUpdatedBy, VUpdatedOn As String

            '//// ----------------Variable Assignment  ----------------
            VRefrenceID = GV.FL.getAutoNumber("RefrenceID")
            VRegistrationId = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VGST_Month = GV.parseString(GridView1.Rows(lblRowIndex.Text).Cells(2).Text)
            VFromDate = GV.parseString(GV.FL.returnDateMonthWiseWithDateChecking(GridView1.Rows(lblRowIndex.Text).Cells(3).Text))
            VToDate = GV.parseString(GV.FL.returnDateMonthWiseWithDateChecking(GridView1.Rows(lblRowIndex.Text).Cells(4).Text))
            VRemarks = GV.parseString(GridView1.Rows(lblRowIndex.Text).Cells(5).Text)
            VCommAmt = GV.parseString(GridView1.Rows(lblRowIndex.Text).Cells(6).Text)
            VGSTAmt = GV.parseString(GridView1.Rows(lblRowIndex.Text).Cells(7).Text)
            VGrossAmt = GV.parseString(GridView1.Rows(lblRowIndex.Text).Cells(8).Text)
            VTDS_Amt = GV.parseString(GridView1.Rows(lblRowIndex.Text).Cells(9).Text)
            VCreditBalance = GV.parseString(GridView1.Rows(lblRowIndex.Text).Cells(10).Text)
            VApprovedBy = "NULL"
            VApprovedDateTime = "NULL"
            VApporvedStatus = "Pending"
            VApporveRemakrs = "NULL"
            VRecordDateTime = Now
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = Now



            Dim qry As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_GST_Refund_Details (RefrenceID,RegistrationId,GST_Month,FromDate,ToDate,Remarks,CommAmt,GSTAmt,GrossAmt,TDS_Amt,CreditBalance,ApprovedBy,ApprovedDateTime,ApporvedStatus,ApporveRemakrs,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & VRefrenceID & "','" & VRegistrationId & "','" & VGST_Month & "','" & VFromDate & "','" & VToDate & "','" & VRemarks & "'," & VCommAmt & "," & VGSTAmt & "," & VGrossAmt & "," & VTDS_Amt & "," & VCreditBalance & "," & VApprovedBy & "," & VApprovedDateTime & ",'" & VApporvedStatus & "'," & VApporveRemakrs & ",'" & VRecordDateTime & "','" & VUpdatedBy & "','" & VUpdatedOn & "' )"

            Dim Result As Boolean = GV.FL.DMLQueriesBulk(qry)
            If Result = True Then
                lblDialogMsg.Text = "<div class=""Successlabels"" > Request Raised Successfully.</div><br/><font color='Blue'>Your Request ID Is : " & VRefrenceID & "</font>   "
                Bind()
            Else
                lblDialogMsg.Text = "Process Cann't be Complited."
                lblDialogMsg.CssClass = "errorlabels"
            End If

            btnok.Visible = False
            btnCancel.Text = "Ok"

            MPE_RaiseRequest.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    'btn_PopUp_Raise_Request_Yes_Click
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
                    GV.ExportToExcel_New(GridView1, Response, "", "TransactionDetails", lblExportQry.Text, "dyanamic")
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


    Protected Sub ddlAmountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectYears.SelectedIndexChanged
        Try
            'clear()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub MonthWise()
        Try
            lblError.Text = ""
            lblError0.Text = ""
            lblError1.Text = ""
            lblNoRecords.Text = ""

            lblError.CssClass = ""
            lblError0.CssClass = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""
            lblSearchHeading.Text = "Search Details ::"

            GridView1.DataSource = Nothing
            GridView1.DataBind()


            lblError0.Text = ""
            lblError.Text = ""

            ddlFromMonth.Visible = True
            ddlToMonth.Visible = True
            ddlSelectYears.Visible = True


            ddlSelectYears.Items.Clear()
            For i As Integer = Now.Year To 2018 Step -1
                ddlSelectYears.Items.Add(i)
            Next

            ddlFromMonth.Items.Clear()
            GV.Fill_MonthName_InLong_InDropDown(ddlFromMonth)

            ddlToMonth.Items.Clear()
            GV.Fill_MonthName_InLong_InDropDown(ddlToMonth)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Protected Sub ddlSelectCriteria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectCriteria.SelectedIndexChanged
        Try
            ddlvalue.Items.Clear()
            Dim loginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

            If group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                loginID = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                group = ddlSelectCriteria.SelectedValue
            ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                loginID = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                group = ddlSelectCriteria.SelectedValue
            ElseIf group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                ddlvalue.Items.Add("Not Applicable")
                Exit Sub
            ElseIf group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                ddlvalue.Items.Add("Not Applicable")
                Exit Sub
            Else
                loginID = ""
                group = ddlSelectCriteria.SelectedValue
            End If

            If Not group.Trim.ToUpper = "Not Applicable".ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlvalue, "RegistrationId+':'+FirstName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & group & "' " & loginID & "")
            Else
                ddlvalue.Items.Add("Not Applicable")
            End If
            ddlvalue_SelectedIndexChanged(sender, e)

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlvalue_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlvalue.SelectedIndexChanged
        Try

            lblSearchHeading.Text = "Search Details ::"

            GridView1.DataSource = Nothing
            GridView1.DataBind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class