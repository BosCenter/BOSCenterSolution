Public Class BOS_Trans_Com_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                CheckBox1_CheckedChanged(sender, e)
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    lblHeading.Text = "All Transaction Report"
                Else
                    lblHeading.Text = "Commission Report"
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
            Dim loginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            Dim AmountType As String = ""
            If ddlAmountType.SelectedValue.Trim.ToUpper = "All Transactions".Trim.ToUpper Then
                AmountType = ""
            ElseIf ddlAmountType.SelectedValue.Trim.ToUpper = "Balance Transfer".Trim.ToUpper Then
                AmountType = " And Amount_Type in ('Deposit','Withdraw') "
            ElseIf ddlAmountType.SelectedValue.Trim.ToUpper = "Commission".Trim.ToUpper Then
                AmountType = " And Amount_Type='Commission' "
            ElseIf ddlAmountType.SelectedValue.Trim.ToUpper = "Money Transfer".Trim.ToUpper Then
                AmountType = " And Amount_Type='Money Transfer' "
            ElseIf ddlAmountType.SelectedValue.Trim.ToUpper = "PAN CARD".Trim.ToUpper Then
                AmountType = " And Amount_Type='PAN CARD' "
            ElseIf ddlAmountType.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                AmountType = " And Amount_Type='RECH' "
            End If
            If ddlAmountType.SelectedValue.Trim.ToUpper = "All Transactions".Trim.ToUpper Then

                If chkduration.Checked = True Then

                    ' Querystring = "select RID as SrNo,TransferTo ,(Select (FirstName+ ' ' +LastName) From [BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo) as Name,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from [BOS_TransferAmountToAgents] b where TransferFrom in (select RefrenceId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ) and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'  " & Value & "  " & SearchColumnName & "  order by  RID Desc"
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Withdraw' as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw') and TransferFrom='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " ) "
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Deposit' as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw') and TransferTo='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " "
                    Querystring = Querystring & " ) union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw') and TransferFrom='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " ) "
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw') and TransferTo='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " "
                    Querystring = Querystring & " ) order by rid desc "

                Else
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Withdraw' as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw') and  TransferFrom='" & loginID & "' " & AmountType & " )"
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Deposit' as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw') and  TransferTo='" & loginID & "' " & AmountType & " "
                    Querystring = Querystring & " ) union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw') and  TransferFrom='" & loginID & "' " & AmountType & " )"
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw') and  TransferTo='" & loginID & "' " & AmountType & " "
                    Querystring = Querystring & " ) order by rid desc "


                End If
            ElseIf ddlAmountType.SelectedValue.Trim.ToUpper = "Balance Transfer".Trim.ToUpper Then

                If chkduration.Checked = True Then

                    ' Querystring = "select RID as SrNo,TransferTo ,(Select (FirstName+ ' ' +LastName) From [BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo) as Name,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from [BOS_TransferAmountToAgents] b where TransferFrom in (select RefrenceId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ) and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'  " & Value & "  " & SearchColumnName & "  order by  RID Desc"
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Withdraw' as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " ) "
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Deposit' as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " "
                    Querystring = Querystring & " ) order by rid desc "

                Else
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Withdraw' as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & loginID & "' " & AmountType & " )"
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,'Deposit' as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & loginID & "' " & AmountType & " "
                    Querystring = Querystring & " ) order by rid desc "


                End If


            Else
                If chkduration.Checked = True Then

                    ' Querystring = "select RID as SrNo,TransferTo ,(Select (FirstName+ ' ' +LastName) From [BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo) as Name,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from [BOS_TransferAmountToAgents] b where TransferFrom in (select RefrenceId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ) and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'  " & Value & "  " & SearchColumnName & "  order by  RID Desc"
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " ) "
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & loginID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' " & AmountType & " "
                    Querystring = Querystring & " ) order by rid desc "

                Else
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & loginID & "' " & AmountType & " )"
                    Querystring = Querystring & " union "
                    Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account', TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & loginID & "' " & AmountType & " "
                    Querystring = Querystring & " ) order by rid desc "


                End If
            End If




            If Not Querystring = "" Then

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 0)
                    GridView1.Columns(5).Visible = False
                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(6).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim = "" Then
                                    Balnceamt = 0
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim = "" Then
                                    Balnceamt = 0
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(6).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(7).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(7).Text).Trim = "" Then
                                    Balnceamt = 0
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(7).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(7).Text).Trim = "" Then
                                    Balnceamt = 0
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(7).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(8).Text = Balnceamt

                    Next
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


    Protected Sub ddlAmountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAmountType.SelectedIndexChanged
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class