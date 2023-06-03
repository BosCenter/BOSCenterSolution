Public Class BOS_GST_TDS_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                rdbDatewise.Checked = True
                rdbDatewise_CheckedChanged(sender, e)

                'rdbMonthwise.Visible = False



                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    Div_Criteria.Visible = False
                    lblHeading.Text = "GST & TDS Report"
                ElseIf group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    Div_Criteria.Visible = False
                    lblHeading.Text = "GST & TDS Report"
                Else
                    Div_Criteria.Visible = True
                    lblHeading.Text = "GST & TDS Report"
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


            If rdbDatewise.Checked = True Then
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
            Else
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

            End If


            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            rdbDatewise.Checked = True
            rdbDatewise_CheckedChanged(sender, e)
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

            Dim AgentID, AgencyName, Agent_PanCard As String

            Dim Agentcounter As Integer = 0
            AgentID = ""
            Agent_PanCard = ""
            AgencyName = ""

            Dim searchID As String = ""
            If Not ddlvalue.SelectedValue.Trim.ToUpper = "Not Applicable".ToUpper Then

                If ddlvalue.SelectedValue.Trim.ToUpper = "ALL AGENTS" Then
                    lblSearchHeading.Text = " Search Details For All Agents"
                Else
                    Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                    searchID = aa(0)
                    lblSearchHeading.Text = " Search Details of - ( " & GV.parseString(ddlvalue.SelectedValue.Trim).ToUpper & " )"

                    AgentID = searchID
                    Agent_PanCard = GV.FL.AddInVar(" PanCardNumber ", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & AgentID & "' ")
                    AgencyName = GV.FL.AddInVar(" AgencyName ", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & AgentID & "' ")
                End If
            Else
                searchID = loginID
                lblSearchHeading.Text = " Search Details of Self"
                AgentID = searchID
                Agent_PanCard = GV.FL.AddInVar(" PanCardNumber ", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & AgentID & "' ")
                AgencyName = GV.FL.AddInVar(" AgencyName ", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & AgentID & "' ")
            End If



            If rdbDatewise.Checked = True Then
                Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferFromMsg as 'Type',TransferTo as 'Account',Actual_Commission_Amt as 'CommAmt',	GSTAmt,	Commission_Without_GST as 'GrossAmt',	TDS_Amt,0 as 'Cr',TransferAmt as 'Dr','0' as Balance  from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom='" & searchID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' and Amount_Type='Commission' ) "
                Querystring = Querystring & " union "
                Querystring = Querystring & " (select RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,Amount_Type as 'Remarks',TransferToMsg as 'Type',TransferFrom as 'Account',Actual_Commission_Amt as 'CommAmt',	GSTAmt,	Commission_Without_GST as 'GrossAmt',	TDS_Amt, TransferAmt as 'Cr', 0 as 'Dr','0' as Balance from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & searchID & "' and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "' and Amount_Type='Commission')"
                Querystring = Querystring & "  order by rid desc "
            Else

                Dim FromMonthNo, ToMonthNo As Integer
                FromMonthNo = GV.returnMonthNumber(ddlFromMonth.SelectedValue)
                ToMonthNo = GV.returnMonthNumber(ddlToMonth.SelectedValue)
                Dim FromDate, ToDate As Date
                Dim TotalRows As Integer = (ToMonthNo - FromMonthNo) '+ 1

                Dim dt As New DataTable
                Dim dc1 As DataColumn = New DataColumn("SrNo")

                Dim dc2 As DataColumn = New DataColumn("AgentID")
                Dim dc3 As DataColumn = New DataColumn("AgencyName")

                Dim dc4 As DataColumn = New DataColumn("PanCard")

                Dim dc5 As DataColumn = New DataColumn("Month")
                Dim dc6 As DataColumn = New DataColumn("FromDate")
                Dim dc7 As DataColumn = New DataColumn("ToDate")
                Dim dc8 As DataColumn = New DataColumn("Remarks")

                Dim dc9 As DataColumn = New DataColumn("CommAmt")
                Dim dc10 As DataColumn = New DataColumn("GSTAmt")
                Dim dc11 As DataColumn = New DataColumn("GrossAmt")
                Dim dc12 As DataColumn = New DataColumn("TDS_Amt")
                Dim dc13 As DataColumn = New DataColumn("CreditBalance")

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
                dt.Columns.Add(dc11)
                dt.Columns.Add(dc12)
                dt.Columns.Add(dc13)


                Dim LocalDS_Agents As New DataSet
                If ddlvalue.SelectedValue.Trim.ToUpper = "ALL AGENTS" Then

                    Dim QryStr As String = "select distinct RegistrationId,AgencyName,PanCardNumber from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where AgentType='" & ddlSelectCriteria.SelectedValue.Trim.ToString & "'"
                    LocalDS_Agents = GV.FL.OpenDsWithSelectQuery(QryStr)

                    If Not LocalDS_Agents Is Nothing Then
                        If LocalDS_Agents.Tables.Count > 0 Then
                            If LocalDS_Agents.Tables(0).Rows.Count > 0 Then
                                Agentcounter = LocalDS_Agents.Tables(0).Rows.Count - 1

                            End If
                        End If
                    End If

                End If



                Dim rowCounter As Integer = 0


                For j As Integer = 0 To Agentcounter

                    If ddlvalue.SelectedValue.Trim.ToUpper = "ALL AGENTS" Then
                        AgentID = LocalDS_Agents.Tables(0).Rows(j).Item(0).ToString
                        searchID = AgentID
                        AgencyName = GV.parseString(LocalDS_Agents.Tables(0).Rows(j).Item(1).ToString)
                        Agent_PanCard = GV.parseString(LocalDS_Agents.Tables(0).Rows(j).Item(2).ToString)
                    End If



                    For i As Integer = 0 To TotalRows


                        Dim dr1 As DataRow = dt.NewRow()

                        rowCounter += 1
                        dr1(0) = rowCounter

                        dr1(1) = AgentID
                        dr1(2) = AgencyName
                        dr1(3) = Agent_PanCard


                        dr1(4) = GV.GetMonthName(FromMonthNo + i) & " - " & ddlSelectYears.SelectedValue
                        dr1(5) = "1/" & (FromMonthNo + i) & "/" & ddlSelectYears.SelectedValue
                        dr1(6) = GV.returnDaysInMonth(ddlSelectYears.SelectedValue, (FromMonthNo + i)) & "/" & FromMonthNo + i & "/" & ddlSelectYears.SelectedValue
                        dr1(7) = GV.parseString("Commission")

                        FromDate = GV.FL.returnDateMonthWise(dr1(5))
                        ToDate = GV.FL.returnDateMonthWise(dr1(6))



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
                                            dr1(8) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("CommAmt").ToString())
                                        Else
                                            dr1(8) = "0"
                                        End If
                                    Else
                                        dr1(8) = "0"
                                    End If

                                    If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("GSTAmt")) Then
                                        If Not LocalDS.Tables(0).Rows(0).Item("GSTAmt").ToString() = "" Then
                                            dr1(9) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("GSTAmt").ToString())
                                        Else
                                            dr1(9) = "0"
                                        End If
                                    Else
                                        dr1(9) = "0"
                                    End If

                                    If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("GrossAmt")) Then
                                        If Not LocalDS.Tables(0).Rows(0).Item("GrossAmt").ToString() = "" Then
                                            dr1(10) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("GrossAmt").ToString())
                                        Else
                                            dr1(10) = "0"
                                        End If
                                    Else
                                        dr1(10) = "0"
                                    End If

                                    If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("TDS_Amt")) Then
                                        If Not LocalDS.Tables(0).Rows(0).Item("TDS_Amt").ToString() = "" Then
                                            dr1(11) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("TDS_Amt").ToString())
                                        Else
                                            dr1(11) = "0"
                                        End If
                                    Else
                                        dr1(11) = "0"
                                    End If

                                    If Not IsDBNull(LocalDS.Tables(0).Rows(0).Item("CreditBalance")) Then
                                        If Not LocalDS.Tables(0).Rows(0).Item("CreditBalance").ToString() = "" Then
                                            dr1(12) = GV.parseString(LocalDS.Tables(0).Rows(0).Item("CreditBalance").ToString())
                                        Else
                                            dr1(12) = "0"
                                        End If
                                    Else
                                        dr1(12) = "0"
                                    End If
                                Else
                                    dr1(8) = "0"
                                    dr1(9) = "0"
                                    dr1(10) = "0"
                                    dr1(11) = "0"
                                    dr1(12) = "0"
                                End If
                            Else
                                dr1(8) = "0"
                                dr1(9) = "0"
                                dr1(10) = "0"
                                dr1(11) = "0"
                                dr1(12) = "0"
                            End If
                        Else
                            dr1(8) = "0"
                            dr1(9) = "0"
                            dr1(10) = "0"
                            dr1(11) = "0"
                            dr1(12) = "0"
                        End If
                        dt.Rows.Add(dr1)

                    Next

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
                    CommAmt = CommAmt + CDec(GridView1.Rows(i).Cells(8).Text)
                    GSTAmt = GSTAmt + CDec(GridView1.Rows(i).Cells(9).Text)
                    GrossAmt = GrossAmt + CDec(GridView1.Rows(i).Cells(10).Text)
                    TDS_Amt = TDS_Amt + CDec(GridView1.Rows(i).Cells(11).Text)
                    CreditBalance = CreditBalance + CDec(GridView1.Rows(i).Cells(12).Text)
                Next
                GridView1.FooterRow.Cells(7).Text = "Total"
                GridView1.FooterRow.Cells(8).Text = CommAmt
                GridView1.FooterRow.Cells(9).Text = GSTAmt
                GridView1.FooterRow.Cells(10).Text = GrossAmt
                GridView1.FooterRow.Cells(11).Text = TDS_Amt
                GridView1.FooterRow.Cells(12).Text = CreditBalance

                Exit Sub


            End If



            If Not Querystring = "" Then
                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 0)

                    '10,11,12
                    'GridView1.Columns(5).Visible = False
                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(10).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(11).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(11).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(11).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(11).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(11).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(12).Text = Balnceamt

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


    Protected Sub ddlAmountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectYears.SelectedIndexChanged
        Try
            'clear()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub rdbDatewise_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDatewise.CheckedChanged
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


            If rdbDatewise.Checked = True Then
                txtFrom.Text = ""
                txtTO.Text = ""
                txtFrom.Enabled = True
                txtTO.Enabled = True
                lblError.Text = ""
                lblError0.Text = ""

                txtFrom.Visible = True
                txtTO.Visible = True

                ddlFromMonth.Visible = False
                ddlToMonth.Visible = False
                ddlSelectYears.Visible = False



            Else
                lblError0.Text = ""
                lblError.Text = ""
                txtFrom.Text = ""
                txtTO.Text = ""
                txtFrom.Enabled = False
                txtTO.Enabled = False

                txtFrom.Visible = False
                txtTO.Visible = False

                txtFrom.CssClass = "form-control"
                txtTO.CssClass = "form-control"

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


            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub rdbMonthwise_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMonthwise.CheckedChanged
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


            If rdbDatewise.Checked = True Then
                txtFrom.Text = ""
                txtTO.Text = ""
                txtFrom.Enabled = True
                txtTO.Enabled = True
                lblError.Text = ""
                lblError0.Text = ""
                txtFrom.Visible = True
                txtTO.Visible = True



                ddlFromMonth.Visible = False
                ddlToMonth.Visible = False
                ddlSelectYears.Visible = False



            Else
                lblError0.Text = ""
                lblError.Text = ""
                txtFrom.Text = ""
                txtTO.Text = ""
                txtFrom.Enabled = False
                txtTO.Enabled = False

                txtFrom.Visible = False
                txtTO.Visible = False

                txtFrom.CssClass = "form-control"
                txtTO.CssClass = "form-control"

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


            End If
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
                If loginID.Trim = "" Then
                    ddlvalue.Items.Insert(0, "ALL AGENTS")
                End If
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