Imports System.Data.SqlClient

Public Class BOS_API_Wise_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                CheckBox1_CheckedChanged(sender, e)

                ddlTransactionBetween.Visible = False

                Dim loginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                loginID = "'" & loginID & "'"
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                If group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    lblHeading.Text = " Service Wise Transaction Report"


                    ddlTransferTo.SelectedValue = ":::: Retailer ::::"
                    ddlTransferTo.Enabled = False


                    ddlTransferTo_SelectedIndexChanged(sender, e)
                    ddlTransferToAgent.SelectedValue = GV.FL.AddInVar("RegistrationId+':'+AgencyName", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Retailer' and RegistrationId in (" & loginID & ") ")
                    ddlTransferToAgent.Enabled = False
                ElseIf group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    lblHeading.Text = " Service Wise Transaction Report"


                    ddlTransferTo.SelectedValue = ":::: Customer ::::"
                    ddlTransferTo.Enabled = False


                    ddlTransferTo_SelectedIndexChanged(sender, e)
                    ddlTransferToAgent.SelectedValue = GV.FL.AddInVar("RegistrationId+':'+(isnull(FirstName,'')+' '+isnull(LastName,''))", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Customer' and RegistrationId in (" & loginID & ") ")
                    ddlTransferToAgent.Enabled = False

                ElseIf (group.Trim.ToUpper = "Master Distributor".Trim.ToUpper) Then
                    lblHeading.Text = "Transactions Report"



                    ddlTransferTo.SelectedValue = ":::: Master Distributor ::::"
                    ddlTransferTo.Enabled = False

                    ddlTransferTo_SelectedIndexChanged(sender, e)
                    ddlTransferToAgent.SelectedValue = GV.FL.AddInVar("RegistrationId+':'+AgencyName", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Master Distributor' and RegistrationId in (" & loginID & ") ")
                    ddlTransferToAgent.Enabled = False


                ElseIf (group.Trim.ToUpper = "Distributor".Trim.ToUpper) Then
                    lblHeading.Text = "Transactions Report"

                    ddlTransferTo.SelectedValue = ":::: Distributor ::::"
                    ddlTransferTo.Enabled = False

                    ddlTransferTo_SelectedIndexChanged(sender, e)
                    ddlTransferToAgent.SelectedValue = GV.FL.AddInVar("RegistrationId+':'+AgencyName", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Distributor' and RegistrationId in (" & loginID & ") ")
                    ddlTransferToAgent.Enabled = False

                ElseIf (group.Trim.ToUpper = "Super Admin".Trim.ToUpper) Then

                    ddlTransactionBetween.Visible = True


                    ddlTransferTo.SelectedValue = ":::: Super Admin ::::"
                    ddlTransferTo.Enabled = False
                    lblHeading.Text = "Transactions Report"

                    ddlTransferTo_SelectedIndexChanged(sender, e)
                    ddlTransferToAgent.Enabled = True

                Else
                    'Admin

                    ddlTransferTo.Items.Remove(":::: Super Admin ::::")

                    ddlTransferTo.SelectedValue = ":::: Admin ::::"
                    ddlTransferTo.Enabled = True

                    lblHeading.Text = " Service Wise Transaction Report"


                    ddlTransferTo_SelectedIndexChanged(sender, e)
                    ddlTransferToAgent.Enabled = True
                End If

                ddlAmountType_SelectedIndexChanged(sender, e)




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

            If ddlTransferToAgent.SelectedValue = ":::: Select ::::" Then
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                If group.Trim = "Super Admin".ToUpper Then
                    lblNoRecords.Text = "Please Select Admin."
                Else
                    lblNoRecords.Text = "Please Select Agent."
                End If

                lblNoRecords.CssClass = "errorlabels"
                ddlTransferToAgent.Focus()
                Exit Sub
            End If

            ':::: Select ::::

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

    Protected Sub ddlTransferTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransferTo.SelectedIndexChanged
        Try
            ddlAmountType.Items.Clear()
            ddlAmountType.Items.Add("All Transactions")
            ddlAmountType.Items.Add("Balance Transfer")
            ddlAmountType.Items.Add("Commission")
            ddlAmountType.Items.Add("Service Charges")
            ddlAmountType.Items.Add("Money Transfer")
            ddlAmountType.Items.Add("PAN CARD")
            ddlAmountType.Items.Add("AEPS")
            ddlAmountType.Items.Add("Recharge & Bill Payment")
            ddlAmountType.Items.Add("API Balance")
            ddlAmountType.Items.Add("Net Worth")
            ddlAmountType.Items.Add("Load Balance")
            ddlAmountType.Items.Add("Wallet Report")
            ddlAmountType.Items.Add("Payin")
            ddlAmountType.Items.Add("Payout")

            ddlTransferToAgent.Items.Clear()

            If ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Master Distributor ::::".Trim.ToUpper Then

                ddlAmountType.Items.Remove("Money Transfer")
                ddlAmountType.Items.Remove("PAN CARD")
                ddlAmountType.Items.Remove("Recharge & Bill Payment")
                ddlAmountType.Items.Remove("Service Charges")
                ddlAmountType.Items.Remove("API Balance")
                ddlAmountType.Items.Remove("Wallet Report")
                ddlAmountType.Items.Remove("AEPS")

                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Master Distributor' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select ::::")
                    ddlTransferToAgent.Items.Insert(1, "ALL AGENTS")
                Else
                    ddlTransferToAgent.Items.Add("N/A")
                End If
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Distributor ::::".Trim.ToUpper Then
                ddlAmountType.Items.Remove("Money Transfer")
                ddlAmountType.Items.Remove("PAN CARD")
                ddlAmountType.Items.Remove("Recharge & Bill Payment")
                ddlAmountType.Items.Remove("API Balance")
                ddlAmountType.Items.Remove("Wallet Report")
                ddlAmountType.Items.Remove("AEPS")
                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Distributor' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select ::::")
                    ddlTransferToAgent.Items.Insert(1, "ALL AGENTS")
                Else
                    ddlTransferToAgent.Items.Add("N/A")
                End If
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Retailer ::::".Trim.ToUpper Then
                ddlAmountType.Items.Remove("API Balance")
                ddlAmountType.Items.Remove("Wallet Report")

                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Retailer' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select ::::")
                    ddlTransferToAgent.Items.Insert(1, "ALL AGENTS")
                Else
                    ddlTransferToAgent.Items.Add("N/A")
                End If
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Customer ::::".Trim.ToUpper Then
                ddlAmountType.Items.Remove("API Balance")
                ddlAmountType.Items.Remove("Net Worth")
                ddlAmountType.Items.Remove("Load Balance")
                ddlAmountType.Items.Remove("Wallet Report")


                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+ (isnull(FirstName,'')+' '+isnull(LastName,''))", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Customer' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select ::::")
                    ddlTransferToAgent.Items.Insert(1, "ALL AGENTS")
                Else
                    ddlTransferToAgent.Items.Add("N/A")
                End If
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Admin ::::".Trim.ToUpper Then
                ddlAmountType.Items.Remove("Money Transfer")
                ddlAmountType.Items.Remove("PAN CARD")
                ddlAmountType.Items.Remove("Recharge & Bill Payment")
                ddlAmountType.Items.Remove("Net Worth")
                ddlAmountType.Items.Remove("Load Balance")
                ddlAmountType.Items.Remove("AEPS")

                ddlTransferToAgent.Items.Add("N/A")
            Else
                'Super admin

                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "CompanyCode+':'+CompanyName", "" & GV.DefaultDatabase.Trim & ".dbo.[BOS_ClientRegistration] ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select ::::")
                    ddlTransferToAgent.Items.Insert(1, "ALL ADMIN")
                    'ddlTransferToAgent.Items.Insert(0, "N/A")
                Else
                    ddlTransferToAgent.Items.Add(":::: Select ::::")
                End If

                ddlAmountType.Items.Remove("Money Transfer")
                ddlAmountType.Items.Remove("PAN CARD")
                ddlAmountType.Items.Remove("Recharge & Bill Payment")
                ddlAmountType.Items.Remove("Net Worth")
                ddlAmountType.Items.Remove("Load Balance")
                ddlAmountType.Items.Remove("Wallet Report")
            End If

            ddlAmountType.SelectedIndex = 0
            ddlAmountType_SelectedIndexChanged(sender, e)

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Dim Querystring As String = ""

    Public Sub AddInGridViewWithFieldName(ByVal datagrid As GridView, ByVal Query As String)
        Try
            '"Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()

            Dim sConnection As String = "Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()
            Using Con As New SqlConnection(sConnection)
                Con.Open()
                ds = New DataSet()
                Using Com As New SqlDataAdapter(Query, Con)
                    Com.SelectCommand.CommandTimeout = 300000
                    Com.Fill(ds)
                    Con.Close()
                End Using
            End Using
            datagrid.DataSource = ds.Tables(0)
            datagrid.DataBind()
        Catch ex As Exception
            'ProjectData.SetProjectError(ex)
            Dim ex2 As Exception = ex
            'ProjectData.ClearProjectError()
        End Try
    End Sub
    Public Sub Bind()
        Try
            Dim SearchColumnName As String = ""
            Dim RefrenceId As String = ""
            Dim Value As String = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim loginID As String = ""

            Dim AmountType As String = ""
            Dim aa() As String

            Dim IPAddress As String = ""

            Dim DBName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim.ToUpper
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

            If group.Trim = "ADMIN" Then
                IPAddress = " ,TransIpAddress "
            End If

            Dim Filter_apiPartnerTrans As String = ""

            If ddlTransferTo.SelectedValue = ":::: Super Admin ::::" Then
                loginID = "'Super Admin'"
                If ddlTransferToAgent.SelectedValue.Contains(":") Then
                    Dim dd() As String = GV.parseString(ddlTransferToAgent.SelectedValue.Trim).Split(":")
                    DBName = GV.FL_AdminLogin.AddInVar("DatabaseName", " BOS_ClientRegistration where CompanyCode='" & dd(0) & "' ")
                    If DBName.Trim = "" Then
                        DBName = "BosCenter_DB"
                    End If
                Else
                    DBName = "BosCenter_DB"
                End If

                If ddlTransactionBetween.SelectedValue = "All Transactions" Then
                    Filter_apiPartnerTrans = ""
                ElseIf ddlTransactionBetween.SelectedValue = "Between SA And API Partner Only" Then
                    Filter_apiPartnerTrans = " and  (TransferFrom='API Partner' or TransferTo='API Partner') "
                ElseIf ddlTransactionBetween.SelectedValue = "Between SA And Admin Only" Then
                    Filter_apiPartnerTrans = " and not  (TransferFrom='API Partner' or TransferTo='API Partner') "
                End If

            Else
                If GV.parseString(ddlTransferToAgent.SelectedValue.Trim).ToUpper = "N/A" Then
                    loginID = "'ADMIN'"
                Else
                    If ddlTransferToAgent.SelectedValue.Contains(":") Then
                        aa = GV.parseString(ddlTransferToAgent.SelectedValue.Trim).Split(":")
                        loginID = aa(0)
                        loginID = "'" & loginID & "'"
                    ElseIf ddlTransferToAgent.SelectedValue = "ALL AGENTS" Then
                        If ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Master Distributor ::::".Trim.ToUpper Then
                            loginID = " (select distinct RegistrationId from " & DBName & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Master Distributor') "
                        ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Distributor ::::".Trim.ToUpper Then
                            loginID = " (select distinct RegistrationId from " & DBName & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Distributor') "
                        ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Retailer ::::".Trim.ToUpper Then
                            loginID = " (select distinct RegistrationId from " & DBName & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Retailer') "
                        ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Customer ::::".Trim.ToUpper Then
                            loginID = " (select distinct RegistrationId from " & DBName & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Customer') "
                        Else
                            Exit Sub
                        End If
                    Else

                        Exit Sub
                    End If

                End If
            End If



            '//////////


            If GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "All Transactions".Trim.ToUpper Then
                AmountType = ""

                If loginID = "'ADMIN'" Then
                    If chkduration.Checked = True Then
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                        Querystring = Querystring & " ) union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                        Querystring = Querystring & " ) order by rid desc "
                    Else
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "    from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                        Querystring = Querystring & " ) union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "    from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                        Querystring = Querystring & " ) order by rid desc "
                    End If
                Else
                    If ddlTransferToAgent.SelectedItem.Text = "ALL ADMIN" Then

                        If chkduration.Checked = True Then
                            'DB Name : CMP1045 or BosCenter_DB
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from BosCenter_DB.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from BosCenter_DB.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1085

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1085.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1085.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1085.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1085.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1174

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1174.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1174.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1174.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1174.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1179

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1179.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1179.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1179.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1179.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1207

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1207.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1207.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1207.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1207.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    " & AmountType & " ) "
                            Querystring = Querystring & " union "

                            'DB Name : CMP1218

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1218.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1218.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1218.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1218.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1223

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1223.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1223.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1223.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1223.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1278

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1278.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1278.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1278.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1278.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                            Querystring = Querystring & " ) order by rid desc "

                        Else
                            'DB Name : CMP1045 or BosCenter_DB
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from BosCenter_DB.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from BosCenter_DB.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1085

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1085.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1085.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1085.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1085.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1174

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1174.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1174.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1174.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1174.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1179

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1179.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1179.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1179.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1179.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1207

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1207.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1207.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1207.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1207.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    " & AmountType & " ) "
                            Querystring = Querystring & " union "

                            'DB Name : CMP1218

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1218.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1218.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1218.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1218.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1223

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1223.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1223.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1223.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1223.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            'DB Name : CMP1278

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1278.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1278.dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & "  union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "   from CMP1278.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS' " & IPAddress & "  from CMP1278.dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "  " & AmountType & " "
                            Querystring = Querystring & " ) order by rid desc "
                        End If

                    Else

                        If chkduration.Checked = True Then

                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "    from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                            Querystring = Querystring & " ) union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                            Querystring = Querystring & " ) order by rid desc "
                        Else
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "    from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                            Querystring = Querystring & " ) union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "    from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                            Querystring = Querystring & " union "
                            Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                            Querystring = Querystring & " ) order by rid desc "
                        End If
                    End If
                End If







                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")

                Try
                    Dim sConnection As String = "Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()
                    Using Con As New SqlConnection(sConnection)
                        Con.Open()
                        ds = New DataSet()
                        Using Com As New SqlDataAdapter(lblExportQry.Text, Con)
                            Com.SelectCommand.CommandTimeout = 300000
                            Com.Fill(ds)
                            Con.Close()
                        End Using
                    End Using
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()
                Catch ex As Exception
                    'ProjectData.SetProjectError(ex)
                    Dim ex2 As Exception = ex
            'ProjectData.ClearProjectError()
        End Try
                'AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    GridView1.Columns(0).Visible = False

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(10).Text) = "" Then GridView1.Rows(i).Cells(10).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(10).Text)
                    Next


                    GridView1.FooterRow.Cells(8).Text = "Total"
                    GridView1.FooterRow.Cells(9).Text = Total_CR
                    GridView1.FooterRow.Cells(10).Text = Total_DR
                    GridView1.FooterRow.Cells(11).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(8).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(10).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(11).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
                Exit Sub
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Wallet Report".Trim.ToUpper Then
                AmountType = ""
                If loginID = "'ADMIN'" Then

                    'If chkduration.Checked = True Then
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                    '    Querystring = Querystring & " union "
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                    '    Querystring = Querystring & " ) union "
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                    '    Querystring = Querystring & " union "
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                    '    Querystring = Querystring & " ) order by rid desc "
                    'Else
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                    '    Querystring = Querystring & " union "
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                    '    Querystring = Querystring & " ) union "
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                    '    Querystring = Querystring & " union "
                    '    Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                    '    Querystring = Querystring & " ) order by rid desc "
                    'End If

                    Dim durationStr As String = ""
                    If chkduration.Checked = True Then
                        durationStr = "  and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' "
                    End If


                    Querystring = "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment','Deposit') and TransferFrom='Super Admin' and TransferTo='Admin' " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Commission','Service Charge','Withdraw','Commission-Refund','Service Charge-Refund') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Service Charge','Withdraw') and TransferFrom='Admin' and TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Service Charge')  and not TransferTo='Super Admin' and TransferFrom='Admin' " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Commission','Transaction Charge')   and TransferFrom='Admin' " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment','Deposit','Service Charge-Refund','Commission-Refund') and TransferFrom='Admin' " & durationStr & " ) "
                    Querystring = Querystring & " ) order by rid desc"



                Else

                    If chkduration.Checked = True Then
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "    from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & " and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                        Querystring = Querystring & " ) union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " ) "
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and   RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & AmountType & " "
                        Querystring = Querystring & " ) order by rid desc "
                    Else
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Withdraw' as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO','Deposit' as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                        Querystring = Querystring & " ) union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " )"
                        Querystring = Querystring & " union "
                        Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where not Amount_Type in ('Deposit','Withdraw','MakePayment','GSTRefund') and  TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   " & AmountType & " "
                        Querystring = Querystring & " ) order by rid desc "
                    End If
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    GridView1.Columns(0).Visible = False

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(10).Text) = "" Then GridView1.Rows(i).Cells(10).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(10).Text)
                    Next


                    GridView1.FooterRow.Cells(8).Text = "Total"
                    GridView1.FooterRow.Cells(9).Text = Total_CR
                    GridView1.FooterRow.Cells(10).Text = Total_DR
                    GridView1.FooterRow.Cells(11).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(8).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(10).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(11).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
                Exit Sub
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Net Worth".Trim.ToUpper Then
                AmountType = ""

                Dim reFilter As String = ""
                If group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    reFilter = " and TransferFrom in ( select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID in (select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID=" & loginID & " and AgentType='Distributor') and AgentType='Retailer' ) "
                ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    reFilter = " and TransferFrom in ( select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID in (" & loginID & ") and AgentType='Retailer' ) "
                ElseIf group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    reFilter = " and TransferFrom in ( " & loginID & " ) "
                End If


                If chkduration.Checked = True Then
                    Querystring = Querystring & " select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('PAN CARD','RECH','BILLPAY','Money Transfer-2','Money Transfer','POSTPAID')  and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & reFilter & "  order by rid desc"
                Else
                    Querystring = Querystring & " select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('PAN CARD','RECH','BILLPAY','Money Transfer-2','Money Transfer','POSTPAID')  " & reFilter & "   order by rid desc"
                End If





                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    GridView1.Columns(0).Visible = False

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(10).Text) = "" Then GridView1.Rows(i).Cells(10).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(10).Text)
                    Next


                    GridView1.FooterRow.Cells(8).Text = "Total"
                    GridView1.FooterRow.Cells(9).Text = Total_CR
                    GridView1.FooterRow.Cells(10).Text = Total_DR
                    GridView1.FooterRow.Cells(11).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(8).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(10).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(11).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
                Exit Sub

            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Balance Transfer".Trim.ToUpper Then

                Dim Neg_Amt_Typ, Pos_Amt_Typ As String

                If loginID = "'ADMIN'" Then
                    'Neg_Amt_Typ = "'MakePayment','Deposit','GSTRefund'"
                    'Pos_Amt_Typ = "'Withdraw'"

                    Neg_Amt_Typ = "'Withdraw'"
                    Pos_Amt_Typ = "'Deposit','MakePayment','GSTRefund'"

                    If chkduration.Checked = True Then
                        Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                        Querystring = Querystring & "  union "
                        Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Pos_Amt_Typ & ")) "
                        Querystring = Querystring & "  order by rid desc "
                    Else
                        Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & ")  ) ) "
                        Querystring = Querystring & "  union "
                        Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ")  ) ) "
                        Querystring = Querystring & "  order by rid desc "
                    End If


                Else
                    If group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                        Neg_Amt_Typ = "'Deposit'"

                        Dim fltrClause As String = ""
                        If ddlServiceType.SelectedValue.Trim.ToUpper = "ALL".ToUpper Then
                            fltrClause = " TransferFrom in (" & loginID & ") "
                        ElseIf ddlServiceType.SelectedValue.Trim.ToUpper = "ALL Distributor".ToUpper Then
                            fltrClause = " TransferFrom in (" & loginID & ") and TransferTo in ( select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID=" & loginID & " and AgentType='Distributor') "
                        Else
                            fltrClause = " TransferFrom in (" & loginID & ") and TransferTo in ('" & ddlServiceType.SelectedValue.Trim.Split(":")(0) & "' ) "
                        End If
                        If chkduration.Checked = True Then
                            Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  " & fltrClause & " " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                            Querystring = Querystring & "  order by rid desc "
                        Else
                            Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  " & fltrClause & "  " & Filter_apiPartnerTrans & "    and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                            Querystring = Querystring & "  order by rid desc "
                        End If

                    ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                        Neg_Amt_Typ = "'Deposit'"
                        Dim fltrClause As String = ""
                        If ddlServiceType.SelectedValue.Trim.ToUpper = "ALL".ToUpper Then
                            fltrClause = " TransferFrom in (" & loginID & ") "
                        ElseIf ddlServiceType.SelectedValue.Trim.ToUpper = "ALL Retailer".ToUpper Then
                            fltrClause = " TransferFrom in (" & loginID & ") and TransferTo in ( select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID=" & loginID & " and AgentType='Retailer') "
                        Else
                            fltrClause = " TransferFrom in (" & loginID & ") and TransferTo in ('" & ddlServiceType.SelectedValue.Trim.Split(":")(0) & "' ) "
                        End If
                        If chkduration.Checked = True Then
                            Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  " & fltrClause & " " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                            Querystring = Querystring & "  order by rid desc "
                        Else
                            Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  " & fltrClause & "  " & Filter_apiPartnerTrans & "    and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                            Querystring = Querystring & "  order by rid desc "
                        End If

                    Else
                        Neg_Amt_Typ = "'Withdraw'"
                        Pos_Amt_Typ = "'Deposit','MakePayment','GSTRefund'"

                        If ddlTransferToAgent.SelectedItem.Text = "ALL ADMIN" Then
                            If chkduration.Checked = True Then
                                Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from BosCenter_DB.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                                Querystring = Querystring & "  union "
                                Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Pos_Amt_Typ & ")) "
                                Querystring = Querystring & "  order by rid desc "
                            Else

                                Querystring = "(select RID as 'Sr No',  API_TransId  COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID  COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom   COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo  COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark  COLLATE DATABASE_DEFAULT as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg COLLATE DATABASE_DEFAULT as 'TYPE'     from BosCenter_DB.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',  API_TransId  COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID  COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom  COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo  COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark  COLLATE DATABASE_DEFAULT as 'REMARKS',  TransferAmt as 'CR',  0 as 'DR',  '0' as 'BALANCE',  TransferToMsg  COLLATE DATABASE_DEFAULT as 'TYPE'    from BosCenter_DB.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw')))  UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark COLLATE DATABASE_DEFAULT as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg COLLATE DATABASE_DEFAULT as 'TYPE'     from CMP1085.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark COLLATE DATABASE_DEFAULT as 'REMARKS',  TransferAmt as 'CR',  0 as 'DR',  '0' as 'BALANCE',  TransferToMsg COLLATE DATABASE_DEFAULT as 'TYPE'    from CMP1085.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw')))   UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark COLLATE DATABASE_DEFAULT as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg COLLATE DATABASE_DEFAULT as 'TYPE'     from CMP1174.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark  COLLATE DATABASE_DEFAULT as 'REMARKS',  TransferAmt as 'CR',  0 as 'DR',  '0' as 'BALANCE',  TransferToMsg COLLATE DATABASE_DEFAULT as 'TYPE'    from CMP1174.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw')))    UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark COLLATE DATABASE_DEFAULT as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg COLLATE DATABASE_DEFAULT as 'TYPE'     from CMP1179.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark COLLATE DATABASE_DEFAULT as 'REMARKS',  TransferAmt as 'CR',  0 as 'DR',  '0' as 'BALANCE',  TransferToMsg COLLATE DATABASE_DEFAULT as 'TYPE'    from CMP1179.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw')))  UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark COLLATE DATABASE_DEFAULT as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg COLLATE DATABASE_DEFAULT as 'TYPE'     from CMP1207.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',  API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',  Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',  TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',  Remark COLLATE DATABASE_DEFAULT as 'REMARKS',  TransferAmt as 'CR',  0 as 'DR',  '0' as 'BALANCE',  TransferToMsg COLLATE DATABASE_DEFAULT as 'TYPE'    from CMP1207.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw')))  UNION ALL  (select RID as 'Sr No',  API_TransId as 'TRANSACTION NO',  Ref_TransID as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom as 'TRANSFER FROM',  TransferTo as 'TRANSFER TO',  Remark as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg as 'TYPE'     from CMP1218.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom as 'TRANSFER FROM',  TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE'    from CMP1218.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw')))   UNION ALL  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom as 'TRANSFER FROM',  TransferTo as 'TRANSFER TO',  Remark as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg as 'TYPE'     from CMP1223.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom as 'TRANSFER FROM',  TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE'    from CMP1223.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw')))   UNION ALL  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',  LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom as 'TRANSFER FROM',  TransferTo as 'TRANSFER TO',  Remark as 'REMARKS',  0 as 'CR',  TransferAmt as 'DR',  '0' as 'BALANCE',  TransferFromMsg as 'TYPE'     from CMP1278.dbo.BOS_TransferAmountToAgents  where  TransferFrom in ('Super Admin') and (Amount_Type in ('Withdraw') or Amount_Type in ('Deposit','MakePayment','GSTRefund'))      UNION ALL  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',  convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',  TransferFrom as 'TRANSFER FROM',  TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE'    from CMP1278.dbo.BOS_TransferAmountToAgents  where TransferTo in ('Super Admin') and (Amount_Type in ('Deposit','MakePayment','GSTRefund') or Amount_Type in ('Withdraw'))))))))))) "

                                ''BosCenter_DB
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from BosCenter_DB.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & " ) "
                                'Querystring = Querystring & "   union all  "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & " ) "
                                ''CMP1085
                                'Querystring = Querystring & "   union all "
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from CMP1085.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & " ) "
                                'Querystring = Querystring & "   union all "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from CMP1085.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & " ) "
                                ''CMP1174
                                'Querystring = Querystring & "   union all "
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from CMP1174.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & " )"
                                'Querystring = Querystring & "   union all "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from CMP1174.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ") "
                                ''CMP1179
                                'Querystring = Querystring & "   union all "
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from CMP1179.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & ") "
                                'Querystring = Querystring & "   union all "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from CMP1179.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ") "
                                ''CMP1207
                                'Querystring = Querystring & "   union all "
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from CMP1207.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & ")  "
                                'Querystring = Querystring & "   union all "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from CMP1207.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ") "
                                ''CMP1218
                                'Querystring = Querystring & "   union all "
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from CMP1218.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & ") "
                                'Querystring = Querystring & "   union all "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from CMP1218.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ") "
                                ''CMP1223
                                'Querystring = Querystring & "   union all "
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from CMP1223.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & ") "
                                'Querystring = Querystring & "   union all "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from CMP1223.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ") "
                                ''CMP1278
                                'Querystring = Querystring & "   union all "
                                'Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from CMP1278.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & ")  "
                                'Querystring = Querystring & "   union all "
                                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from CMP1278.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ")  ) ) "
                                'Querystring = Querystring & "  order by rid desc "
                            End If

                        Else

                            If chkduration.Checked = True Then
                                Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                                Querystring = Querystring & "  union "
                                Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Pos_Amt_Typ & ")) "
                                Querystring = Querystring & "  order by rid desc "
                            Else
                                Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Neg_Amt_Typ & ") or Amount_Type in (" & Pos_Amt_Typ & ")  ) ) "
                                Querystring = Querystring & "  union "
                                Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and (Amount_Type in (" & Pos_Amt_Typ & ") or Amount_Type in (" & Neg_Amt_Typ & ")  ) ) "
                                Querystring = Querystring & "  order by rid desc "
                            End If
                        End If
                    End If


                End If


                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                'GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                Try
                    '"Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()

                    Dim sConnection As String = "Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()
                    Using Con As New SqlConnection(sConnection)
                        Con.Open()
                        ds = New DataSet()
                        Using Com As New SqlDataAdapter(lblExportQry.Text, Con)
                            Com.SelectCommand.CommandTimeout = 300000
                            Com.Fill(ds)
                            Con.Close()
                        End Using
                    End Using
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()
                Catch ex As Exception
                    'ProjectData.SetProjectError(ex)
                    Dim ex2 As Exception = ex
                    'ProjectData.ClearProjectError()
                End Try

                If GridView1.Rows.Count > 0 Then

                    GridView1.Columns(0).Visible = False

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(10).Text) = "" Then GridView1.Rows(i).Cells(10).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(10).Text)
                    Next


                    GridView1.FooterRow.Cells(8).Text = "Total"
                    GridView1.FooterRow.Cells(9).Text = Total_CR
                    GridView1.FooterRow.Cells(10).Text = Total_DR
                    GridView1.FooterRow.Cells(11).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(10).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(11).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Load Balance".Trim.ToUpper Then

                Dim Neg_Amt_Typ, Pos_Amt_Typ As String

                If loginID = "'ADMIN'" Then
                    Neg_Amt_Typ = "'MakePayment','Deposit','GSTRefund'"
                    Pos_Amt_Typ = "'Withdraw'"
                Else
                    Neg_Amt_Typ = "'Withdraw'"
                    Pos_Amt_Typ = "'Deposit','MakePayment','GSTRefund'"
                End If

                If chkduration.Checked = True Then
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type in (" & Pos_Amt_Typ & ")) "
                    Querystring = Querystring & "  order by rid desc "
                Else
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'TYPE' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type in (" & Neg_Amt_Typ & ") ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'REMARKS', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'TYPE' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type in (" & Pos_Amt_Typ & ")) "
                    Querystring = Querystring & "  order by rid desc "
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    GridView1.Columns(0).Visible = False

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(8).Text) = "" Then GridView1.Rows(i).Cells(8).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(8).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                    Next


                    GridView1.FooterRow.Cells(7).Text = "Total"
                    GridView1.FooterRow.Cells(8).Text = Total_CR
                    GridView1.FooterRow.Cells(9).Text = Total_DR
                    GridView1.FooterRow.Cells(10).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(8).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(8).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(10).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub
                '//// AEPS - Start
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "AEPS".Trim.ToUpper Then

                Dim Service_Type As String = ""
                If Not ((ddlServiceType.SelectedValue.Trim.ToUpper = "N/A".ToUpper) Or (ddlServiceType.SelectedValue.Trim.ToUpper = "All Type".ToUpper)) Then
                    Service_Type = " and [API_Call_Type]='" & ddlServiceType.SelectedValue & "' "
                End If

                If chkduration.Checked = True Then
                    Querystring = " select  RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,UpdatedBy as 'AGENT_ID',API_Call_Type as 'API',Trans_Type as 'Service_Type',Trans_ID as 'Trans_No',Cash_Withdraw as 'Amount',Access_Mode,Bank_Name,Bank_IIN,Mobile_No,Latitude,Longitude,Aadhar_Last_No,Remarks from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_AEPS_API where UpdatedBy='" & loginID.Trim & "' and RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & Service_Type & " order by rid desc"
                Else
                    Querystring = " select  RID as SrNo,convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,UpdatedBy as 'AGENT_ID',API_Call_Type as 'API',Trans_Type as 'Service_Type', Trans_ID as 'Trans_No',Cash_Withdraw as 'Amount',Access_Mode,Bank_Name,Bank_IIN,Mobile_No,Latitude,Longitude,Aadhar_Last_No,Remarks from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_AEPS_API where UpdatedBy='" & loginID.Trim & "' " & Service_Type & " order by rid desc "
                End If


                'If chkduration.Checked = True Then
                '    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and RecordDateTime between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' and Amount_Type='Commission' " & Service_Type & " ) "
                '    Querystring = Querystring & "  union "
                '    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and RecordDateTime between '" & GV.FL.returnDateMonthWise(txtFrom.Text) & " 00:00:00.000' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & " 23:59:59.999' and Amount_Type='Commission' " & Service_Type & " )  "
                '    Querystring = Querystring & "  order by rid desc "
                'Else
                '    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                '    Querystring = Querystring & "  union "
                '    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "
                '    Querystring = Querystring & "  order by rid desc "
                'End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then
                    GridView1.Columns(0).Visible = False
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub
                '//// AEPS - End


            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Commission".Trim.ToUpper Then

                Dim Service_Type As String = ""
                If Not ((ddlServiceType.SelectedValue.Trim.ToUpper = "N/A".ToUpper) Or (ddlServiceType.SelectedValue.Trim.ToUpper = "All Type".ToUpper)) Then
                    Service_Type = " and [Remark]='" & ddlServiceType.SelectedValue & "' "
                End If

                If ddlTransferToAgent.SelectedItem.Text = "ALL ADMIN" Then

                    'BosCenter_DB
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from BosCenter_DB.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring + "  union "
                    Querystring = Querystring + "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "

                    'CMP1085
                    Querystring = Querystring + "  union "
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from CMP1085.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring + "  union "
                    Querystring = Querystring + "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from CMP1085.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "

                    'CMP1174
                    Querystring = Querystring & "  union "
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from CMP1174.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from CMP1174.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "

                    'CMP1179
                    Querystring = Querystring & "  union "
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from CMP1179.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from CMP1179.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "

                    'CMP1207
                    Querystring = Querystring & "  union "
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from CMP1207.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from CMP1207.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "

                    'CMP1218
                    Querystring = Querystring & "  union "
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from CMP1218.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from CMP1218.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "

                    'CMP1223
                    Querystring = Querystring & "  union "
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from CMP1223.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from CMP1223.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "

                    'CMP1278
                    Querystring = Querystring & "  union "
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from CMP1278.dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from CMP1278.dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "



                    Querystring = Querystring & "  order by rid desc "

                Else

                    If chkduration.Checked = True Then
                        Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type='Commission' " & Service_Type & " ) "
                        Querystring = Querystring & "  union "
                        Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type='Commission' " & Service_Type & " )  "
                        Querystring = Querystring & "  order by rid desc "
                    Else
                        Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Commission'  " & Service_Type & "  ) "
                        Querystring = Querystring & "  union "
                        Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'COMMISSION TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Commission'  " & Service_Type & " )  "
                        Querystring = Querystring & "  order by rid desc "
                    End If
                End If
                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    GridView1.Columns(0).Visible = False

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""


                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(8).Text) = "" Then GridView1.Rows(i).Cells(8).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(8).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                    Next


                    GridView1.FooterRow.Cells(7).Text = "Total"
                    GridView1.FooterRow.Cells(8).Text = Total_CR
                    GridView1.FooterRow.Cells(9).Text = Total_DR
                    GridView1.FooterRow.Cells(10).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(8).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(8).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(10).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Service Charges".Trim.ToUpper Then
                If chkduration.Checked = True Then
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'CHARGE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "   and RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type='Service Charge' ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'CHARGE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' and Amount_Type='Service Charge')  "
                    Querystring = Querystring & "  order by rid desc "
                Else
                    Querystring = " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'CHARGE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS' " & IPAddress & "   from " & DBName & ".dbo.BOS_TransferAmountToAgents  where  TransferFrom in (" & loginID & ") " & Filter_apiPartnerTrans & "    and Amount_Type='Service Charge' ) "
                    Querystring = Querystring & "  union "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as 'TIME',TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Remark as 'CHARGE TYPE', TransferAmt as 'CR', 0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS' " & IPAddress & "  from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in (" & loginID & ") " & Filter_apiPartnerTrans & "   and Amount_Type='Service Charge')  "
                    Querystring = Querystring & "  order by rid desc "
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then
                    GridView1.Columns(0).Visible = False


                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""


                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(5).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(8).Text) = "" Then GridView1.Rows(i).Cells(8).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(8).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                    Next


                    GridView1.FooterRow.Cells(7).Text = "Total"
                    GridView1.FooterRow.Cells(8).Text = Total_CR
                    GridView1.FooterRow.Cells(9).Text = Total_DR
                    GridView1.FooterRow.Cells(10).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(8).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(8).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(8).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(10).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub

            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Money Transfer".Trim.ToUpper Then



                Dim Service_Type As String = ""
                If Not ((ddlServiceType.SelectedValue.Trim.ToUpper = "N/A".ToUpper) Or (ddlServiceType.SelectedValue.Trim.ToUpper = "All Type".ToUpper)) Then
                    Service_Type = " and [Method]='" & ddlServiceType.SelectedValue & "' "
                End If

                ' Dim req_Status As String = ",(select top 1 isnull(ApporvedStatus,'NA')  from " & DBName & ".dbo.BOS_Refund_Request_Master RM where RM.kCode=RA.RefrenceNo and RM.TransID=RA.TransId) as 'Req_Status'"
                Dim req_Status As String = " ,Refund_Req_Status as 'Req Status',Process as 'Gate way' "

                If chkduration.Checked = True Then
                    Querystring = " select RID as 'Sr No',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,RefrenceNo as 'AGENT ID',TranscationId as 'TRANSACTION NO',Method as 'SERVICE TYPE',Amount as 'TRANSFER AMT',MobileNo AS 'MOBILE NO',BankName as 'BANK NAME',APIMessage AS 'STATUS',TransId as 'Trans ID',Refund_Status as 'Refund Status',Refund_TransID as 'Refund TransID' " & req_Status & "  " & IPAddress & "  from " & DBName & ".dbo.BOS_MoneyTransfer_API RA where RefrenceNo in (" & loginID & ")  and RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & Service_Type & "  order by rid desc"
                Else
                    Querystring = " select RID as 'Sr No',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,RefrenceNo as 'AGENT ID',TranscationId as 'TRANSACTION NO',Method as 'SERVICE TYPE',Amount as 'TRANSFER AMT',MobileNo AS 'MOBILE NO',BankName as 'BANK NAME',APIMessage AS 'STATUS',TransId as 'Trans ID',Refund_Status as 'Refund Status',Refund_TransID as 'Refund TransID' " & req_Status & "  " & IPAddress & "  from " & DBName & ".dbo.BOS_MoneyTransfer_API RA  WHERE RefrenceNo in (" & loginID & ")  " & Service_Type & "  order by rid desc"
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then


                    If group.Trim.ToUpper = "Retailer".Trim.ToUpper Or group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        GridView1.Columns(0).Visible = True
                    Else
                        GridView1.Columns(0).Visible = False
                    End If



                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""


                    Dim TotalAmt As Decimal = 0


                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1
                        If GV.parseString(GridView1.Rows(i).Cells(7).Text) = "" Then GridView1.Rows(i).Cells(7).Text = "0"
                        TotalAmt = TotalAmt + GV.parseString(GridView1.Rows(i).Cells(7).Text)


                        Dim btnRefund As LinkButton = DirectCast(GridView1.Rows(i).FindControl("btnRefund"), LinkButton)
                        If GV.parseString(GridView1.Rows(i).Cells(12).Text).ToUpper = "YES" Or GV.parseString(GridView1.Rows(i).Cells(11).Text) = "" Or GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "APPROVED" Or GV.parseString(GridView1.Rows(i).Cells(10).Text).Contains("Transaction Successful") = True Then
                            btnRefund.Visible = False
                        ElseIf GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "PENDING" Then
                            btnRefund.CssClass = "btn btn-warning"
                            btnRefund.Text = GV.parseString(GridView1.Rows(i).Cells(14).Text)
                            btnRefund.Enabled = False
                        ElseIf GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "REJECTED" Then
                            btnRefund.CssClass = "btn btn-danger"
                            btnRefund.Text = GV.parseString(GridView1.Rows(i).Cells(14).Text)
                            btnRefund.Enabled = False
                        End If

                    Next


                    GridView1.FooterRow.Cells(6).Text = "Total"
                    GridView1.FooterRow.Cells(7).Text = TotalAmt
                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "PAN CARD".Trim.ToUpper Then

                Dim Service_Type As String = ""
                If Not ((ddlServiceType.SelectedValue.Trim.ToUpper = "N/A".ToUpper) Or (ddlServiceType.SelectedValue.Trim.ToUpper = "All Type".ToUpper)) Then
                    Service_Type = " and [CoupanType]='" & ddlServiceType.SelectedValue & "' "
                End If

                'Dim req_Status As String = ",(select top 1 isnull(ApporvedStatus,'NA')  from " & DBName & ".dbo.BOS_Refund_Request_Master RM where RM.kCode=RA.AgentID and RM.TransID=RA.TransId) as 'Req_Status'"
                Dim req_Status As String = " ,Refund_Req_Status as 'Req Status' "

                If chkduration.Checked = True Then
                    Querystring = " select  RID as 'Sr No',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,AgentID as 'AGENT ID',API_TransId as 'TRANSACTION NO',CoupanType as 'SERVICE TYPE',TotalAmount as 'TRANSACTION AMT',TotalCoupan as 'TOTAL COUPONS',API_Message as 'STATUS',Remarks as 'REMARKS',TransId as 'Trans ID',Refund_Status as 'Refund Status',Refund_TransID as 'Refund TransID' " & req_Status & "  " & IPAddress & "  from " & DBName & ".dbo.BOS_Pan_Card_API RA where agentID in (" & loginID & ")  and RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & Service_Type & "  order by rid desc"
                Else
                    Querystring = " select  RID as 'Sr No',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,AgentID as 'AGENT ID',API_TransId as 'TRANSACTION NO',CoupanType as 'SERVICE TYPE',TotalAmount as 'TRANSACTION AMT',TotalCoupan as 'TOTAL COUPONS',API_Message as 'STATUS',Remarks as 'REMARKS',TransId as 'Trans ID',Refund_Status as 'Refund Status',Refund_TransID as 'Refund TransID' " & req_Status & "  " & IPAddress & "  from " & DBName & ".dbo.BOS_Pan_Card_API RA  where agentID in (" & loginID & ")  " & Service_Type & "  order by rid desc "
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    If group.Trim.ToUpper = "Retailer".Trim.ToUpper Or group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        GridView1.Columns(0).Visible = True
                    Else
                        GridView1.Columns(0).Visible = False
                    End If

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim TotalAmt As Decimal = 0
                    Dim TotalCoupans As Decimal = 0

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1
                        If GV.parseString(GridView1.Rows(i).Cells(7).Text) = "" Then GridView1.Rows(i).Cells(7).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(8).Text) = "" Then GridView1.Rows(i).Cells(8).Text = "0"

                        TotalAmt = TotalAmt + GV.parseString(GridView1.Rows(i).Cells(7).Text)
                        TotalCoupans = TotalCoupans + GV.parseString(GridView1.Rows(i).Cells(8).Text)

                        Dim btnRefund As LinkButton = DirectCast(GridView1.Rows(i).FindControl("btnRefund"), LinkButton)

                        If GV.parseString(GridView1.Rows(i).Cells(12).Text).ToUpper = "YES" Or GV.parseString(GridView1.Rows(i).Cells(11).Text) = "" Or GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "APPROVED" Then
                            btnRefund.Visible = False
                        ElseIf GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "PENDING" Then
                            btnRefund.CssClass = "btn btn-warning"
                            btnRefund.Text = GV.parseString(GridView1.Rows(i).Cells(14).Text)
                            btnRefund.Enabled = False
                        ElseIf GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "REJECTED" Then
                            btnRefund.CssClass = "btn btn-danger"
                            btnRefund.Text = GV.parseString(GridView1.Rows(i).Cells(14).Text)
                            btnRefund.Enabled = False
                        End If


                    Next


                    GridView1.FooterRow.Cells(6).Text = "Total"
                    GridView1.FooterRow.Cells(7).Text = TotalAmt
                    GridView1.FooterRow.Cells(8).Text = TotalCoupans

                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub


            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Recharge & Bill Payment".Trim.ToUpper Then

                Dim Service_Type As String = ""
                If Not ((ddlServiceType.SelectedValue.Trim.ToUpper = "N/A".ToUpper) Or (ddlServiceType.SelectedValue.Trim.ToUpper = "All Type".ToUpper)) Then
                    Service_Type = " and [API_service]='" & ddlServiceType.SelectedValue & "' "
                End If

                'Dim req_Status As String = ",(select top 1 isnull(ApporvedStatus,'NA')  from " & DBName & ".dbo.BOS_Refund_Request_Master RM where RM.kCode=RA.RetailerID and RM.TransID=RA.TransId) as 'Req_Status'"
                Dim req_Status As String = " ,Refund_Req_Status as 'Req Status' "
                'Refund_Req_Status

                If chkduration.Checked = True Then
                    Querystring = " select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, Recharge_Date, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), Recharge_Date, 100), 7)) as TIME,RetailerID as 'AGENT ID',API_service as 'SERVICE TYPE',Recharge_PayableAmount as 'TRANSACTION AMT',Recharge_MobileNo_CaNo AS 'MOBILE NO',API_status AS 'STATUS',API_resText AS 'REMARKS',TransId as 'Trans ID',Refund_Status as 'Refund Status' ,Refund_TransID as 'Refund TransID' " & req_Status & " " & IPAddress & "  from " & DBName & ".dbo.BOS_Recharge_API RA where RetailerID in (" & loginID & ")  and Recharge_Date between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' " & Service_Type & "  order by rid desc"
                Else
                    Querystring = " select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, Recharge_Date, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), Recharge_Date, 100), 7)) as TIME,RetailerID as 'AGENT ID',API_service as 'SERVICE TYPE',Recharge_PayableAmount as 'TRANSACTION AMT',Recharge_MobileNo_CaNo AS 'MOBILE NO',API_status AS 'STATUS',API_resText AS 'REMARKS',TransId as 'Trans ID',Refund_Status as 'Refund Status',Refund_TransID as 'Refund TransID' " & req_Status & " " & IPAddress & " from " & DBName & ".dbo.BOS_Recharge_API RA where RetailerID in (" & loginID & ")  " & Service_Type & "  order by rid desc "
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    If group.Trim.ToUpper = "Retailer".Trim.ToUpper Or group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        GridView1.Columns(0).Visible = True
                    Else
                        GridView1.Columns(0).Visible = False
                    End If

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim TotalAmt As Decimal = 0


                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1
                        If GV.parseString(GridView1.Rows(i).Cells(7).Text) = "" Then GridView1.Rows(i).Cells(7).Text = "0"
                        TotalAmt = TotalAmt + GV.parseString(GridView1.Rows(i).Cells(7).Text)

                        '12 status 'Yes'
                        Dim btnRefund As LinkButton = DirectCast(GridView1.Rows(i).FindControl("btnRefund"), LinkButton)
                        If GV.parseString(GridView1.Rows(i).Cells(12).Text).ToUpper = "YES" Or GV.parseString(GridView1.Rows(i).Cells(11).Text) = "" Or GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "APPROVED" Or GV.parseString(GridView1.Rows(i).Cells(9).Text).ToUpper = "TRUE" Then
                            btnRefund.Visible = False
                        ElseIf GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "PENDING" Then
                            btnRefund.CssClass = "btn btn-warning"
                            btnRefund.Text = GV.parseString(GridView1.Rows(i).Cells(14).Text)
                            btnRefund.Enabled = False
                        ElseIf GV.parseString(GridView1.Rows(i).Cells(14).Text).ToUpper = "REJECTED" Then
                            btnRefund.CssClass = "btn btn-danger"
                            btnRefund.Text = GV.parseString(GridView1.Rows(i).Cells(14).Text)
                            btnRefund.Enabled = False
                        End If
                    Next
                    GridView1.FooterRow.Cells(6).Text = "Total"
                    GridView1.FooterRow.Cells(7).Text = TotalAmt
                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
                Exit Sub

            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "API Balance".Trim.ToUpper Then


                Querystring = ""
                Dim durationStr As String = ""
                If ddlTransferToAgent.SelectedItem.Text = "ALL ADMIN" Then

                    If chkduration.Checked = True Then
                        durationStr = "  and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' "
                    End If
                    'DB Name : CMP1045 or BosCenter_DB
                    Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId  COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "
                    'DB Name : CMP1085
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID  COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "
                    'DB Name : CMP1174
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "

                    'DB Name : CMP1179
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID  COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "


                    'DB Name : CMP1207
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "

                    'DB Name : CMP1218
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "

                    'DB Name : CMP1223
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT  as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom  COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "


                    'DB Name : CMP1278
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type  COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "


                    Querystring = Querystring & " ) order by rid desc"

                Else
                    If chkduration.Checked = True Then
                        durationStr = "  and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' "
                    End If

                    Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit','AEPS') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " ) order by rid desc"

                End If

                'Querystring = Querystring & "  ("
                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom='Super Admin' and TransferTo='Admin' and Amount_Type in ('Deposit','MakePayment')  " & durationStr & " ) "
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom='Admin' and Amount_Type='BILLPAY-Refund' " & durationStr & " )"
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom='Admin' and Amount_Type='Money Transfer-Refund' " & durationStr & " )"
                ''Querystring = Querystring & " union"
                ''Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom='Super Admin' and TransferTo='Admin' and Amount_Type='Commission' " & durationStr & " )   "
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom='Admin' and Amount_Type='RECH-Refund' " & durationStr & " )   "
                ''Querystring = Querystring & " union"
                ''Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom='Super Admin' and TransferTo='Admin' and  Amount_Type='Service Charge-Refund' " & durationStr & " )   "
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select  RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom='Admin' and TransferTo='Super Admin' and Amount_Type='Withdraw' " & durationStr & " ) "
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferTo='Admin' and Amount_Type='BILLPAY' " & durationStr & " )   "
                ''Querystring = Querystring & " union"
                ''Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferFrom='Admin' and TransferTo='Super Admin' and  Amount_Type='Commission-Refund' " & durationStr & " )   "
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferTo='Admin' and Amount_Type='Money Transfer' " & durationStr & " )   "
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferTo='Admin' and Amount_Type='PAN CARD' " & durationStr & " )   "
                'Querystring = Querystring & " union"
                'Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferTo='Admin' and Amount_Type='RECH' " & durationStr & " )   "
                ''Querystring = Querystring & " union"
                ''Querystring = Querystring & " (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR','0' as 'BALANCE',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where TransferFrom='Admin' and TransferTo='Super Admin' and  Amount_Type='Service Charge' " & durationStr & " )   "
                'Querystring = Querystring & " ) order by rid desc"

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                'GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                Try
                    '"Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()

                    Dim sConnection As String = "Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()
                    Using Con As New SqlConnection(sConnection)
                        Con.Open()
                        ds = New DataSet()
                        Using Com As New SqlDataAdapter(lblExportQry.Text, Con)
                            Com.SelectCommand.CommandTimeout = 300000
                            Com.Fill(ds)
                            Con.Close()
                        End Using
                    End Using
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()
                Catch ex As Exception
                    'ProjectData.SetProjectError(ex)
                    Dim ex2 As Exception = ex
                    'ProjectData.ClearProjectError()
                End Try

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0
                    GridView1.Columns(0).Visible = False

                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(7).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(7).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(7).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(7).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(10).Text) = "" Then GridView1.Rows(i).Cells(10).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(10).Text)
                    Next


                    GridView1.FooterRow.Cells(7).Text = "Total"
                    GridView1.FooterRow.Cells(9).Text = Total_CR
                    GridView1.FooterRow.Cells(10).Text = Total_DR
                    GridView1.FooterRow.Cells(11).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(10).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If

                        GridView1.Rows(i).Cells(11).Text = Balnceamt

                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub

            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "PayOut".Trim.ToUpper Then

                Querystring = ""
                Dim durationStr As String = ""

                If ddlTransferToAgent.SelectedItem.Text = "ALL ADMIN" Then

                    If chkduration.Checked = True Then
                        durationStr = "  and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' "
                    End If
                    'DB Name : CMP1045 or BosCenter_DB
                    Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address' from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address' from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address' from BosCenter_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    'DB Name : CMP1085
                    'Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address' from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address' from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress as 'Trans Ip Address' from CMP1085.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    'DB Name : CMP1174
                    'Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1174.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    'DB Name : CMP1179
                    'Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1179.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    'DB Name : CMP1207
                    'Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1207.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    'DB Name : CMP1218
                    'Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1218.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    'DB Name : CMP1223
                    'Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type  COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1223.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    'DB Name : CMP1278
                    'Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address'  from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId COLLATE DATABASE_DEFAULT as 'TRANSACTION NO',Ref_TransID COLLATE DATABASE_DEFAULT as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom COLLATE DATABASE_DEFAULT as 'TRANSFER FROM',TransferTo COLLATE DATABASE_DEFAULT as 'TRANSFER TO',Amount_Type COLLATE DATABASE_DEFAULT as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg COLLATE DATABASE_DEFAULT as 'REMARKS',TransIpAddress COLLATE DATABASE_DEFAULT as 'Trans Ip Address' from CMP1278.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " ) order by rid desc"

                Else

                    If chkduration.Checked = True Then
                        durationStr = "  and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' "
                    End If

                    Querystring = Querystring & "  ("
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " union all "
                    Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                    Querystring = Querystring & " ) order by rid desc"
                End If

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                Try
                    '"Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()

                    Dim sConnection As String = "Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123;Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=200;Pooling=true".ToString()
                    Using Con As New SqlConnection(sConnection)
                        Con.Open()
                        ds = New DataSet()
                        Using Com As New SqlDataAdapter(lblExportQry.Text, Con)
                            Com.SelectCommand.CommandTimeout = 300000
                            Com.Fill(ds)
                            Con.Close()
                        End Using
                    End Using
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()
                Catch ex As Exception
                    'ProjectData.SetProjectError(ex)
                    Dim ex2 As Exception = ex
                    'ProjectData.ClearProjectError()
                End Try

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0
                    GridView1.Columns(0).Visible = False
                    'GridView1.Columns(11).Visible = False


                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(7).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(7).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(10).Text) = "" Then GridView1.Rows(i).Cells(10).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(10).Text)
                    Next


                    GridView1.FooterRow.Cells(8).Text = "Total"
                    GridView1.FooterRow.Cells(9).Text = Total_CR
                    GridView1.FooterRow.Cells(10).Text = Total_DR
                    'GridView1.FooterRow.Cells(11).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If

                        'GridView1.Rows(i).Cells(11).Text = Balnceamt
                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub

            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Payin".Trim.ToUpper Then

                Querystring = ""
                Dim durationStr As String = ""
                If chkduration.Checked = True Then
                    durationStr = "  and  RecordDateTime between '" & GV.returnDateMonthWiseWithDateChecking(txtFrom.Text) & " 00:00:00.000' and '" & GV.returnDateMonthWiseWithDateChecking(txtTO.Text) & " 23:59:59.999' "
                End If

                'Querystring = Querystring & "  ("
                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                'Querystring = Querystring & " union all "
                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Super Admin' and 	TransferTo='Admin'  " & durationStr & " ) "
                'Querystring = Querystring & " union all "
                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin'  " & durationStr & " ) "
                'Querystring = Querystring & " union all "
                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferFrom='Admin' and 	TransferTo='Super Admin'  " & durationStr & " ) "
                'Querystring = Querystring & " union all "
                'Querystring = Querystring & "  (select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Ref_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',0 as 'CR',TransferAmt as 'DR',TransferFromMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from " & DBName & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Pay Out') and TransferTo='Admin'  " & durationStr & " ) "
                'Querystring = Querystring & " ) order by rid desc"

                Querystring = "select RID as 'Sr No',API_TransId as 'TRANSACTION NO',Amt_Transfer_TransID as 'UPIRefID',convert(varchar, RecordDateTime, 106) as 'DATE',LTRIM(RIGHT(CONVERT(VARCHAR(20), RecordDateTime, 100), 7)) as TIME,TransferFrom as 'TRANSFER FROM',TransferTo as 'TRANSFER TO',Amount_Type as 'SERVICE TYPE',TransferAmt as 'CR',0 as 'DR',TransferToMsg as 'REMARKS',TransIpAddress as 'Trans Ip Address' from BOSCENTER_DB.dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Payin') and TransferTo='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim.ToUpper & "'"
                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then

                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""

                    Dim Total_CR As Decimal = 0
                    Dim Total_DR As Decimal = 0
                    GridView1.Columns(0).Visible = False
                    'GridView1.Columns(11).Visible = False


                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(1).Text = i + 1

                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "ADMIN"
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(5).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(6).Text = "BOSCENTER"
                        End If

                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(7).Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                        End If
                        If (GV.parseString(GridView1.Rows(i).Cells(6).Text).Trim.ToUpper = "SUPER ADMIN".Trim.ToUpper) Then
                            GridView1.Rows(i).Cells(7).Text = "BOSCENTER"
                        End If

                        If GV.parseString(GridView1.Rows(i).Cells(9).Text) = "" Then GridView1.Rows(i).Cells(9).Text = "0"
                        If GV.parseString(GridView1.Rows(i).Cells(10).Text) = "" Then GridView1.Rows(i).Cells(10).Text = "0"

                        Total_CR = Total_CR + GV.parseString(GridView1.Rows(i).Cells(9).Text)
                        Total_DR = Total_DR + GV.parseString(GridView1.Rows(i).Cells(10).Text)
                    Next


                    GridView1.FooterRow.Cells(8).Text = "Total"
                    GridView1.FooterRow.Cells(9).Text = Total_CR
                    GridView1.FooterRow.Cells(10).Text = Total_DR
                    'GridView1.FooterRow.Cells(11).Text = Total_CR - Total_DR

                    Dim Balnceamt As Decimal = 0

                    For i As Integer = GridView1.Rows.Count - 1 To 0 Step -1
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(9).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(9).Text.Trim)
                                End If

                            End If
                        End If
                        If Not GridView1.Rows(i).Cells(9).Text.Trim = "" Then
                            If Balnceamt = 0 Then
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = -CDec(GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim)
                                End If

                            Else
                                If GV.parseString(GridView1.Rows(i).Cells(10).Text).Trim = "" Then
                                    Balnceamt = Balnceamt
                                Else
                                    Balnceamt = CDec(Balnceamt) - CDec(GridView1.Rows(i).Cells(10).Text.Trim)
                                End If

                            End If
                        End If

                        'GridView1.Rows(i).Cells(11).Text = Balnceamt
                    Next


                Else
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If

                Exit Sub

            End If




            '//////////











        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            'lblNoRecords.Text = ex.Message
        End Try
    End Sub

    Dim QryStr As String = ""
    Protected Sub btnRefundClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            lblMsgDialog.Text = ""
            lblMsgDialog.CssClass = ""
            btnUpdate.Visible = True
            btnCancel.Text = "Cancel"


            If GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Recharge & Bill Payment".Trim.ToUpper Then
                lblTransType.Text = "Recharge & Bill Payment"

                txtTransID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text)
                txtAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
                txtRemarks.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)

                ModalPopupExtender1.Show()


            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Money Transfer".Trim.ToUpper Then
                lblTransType.Text = "Money Transfer"

                txtTransID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text)
                txtAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
                txtRemarks.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
                ModalPopupExtender1.Show()


            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Pan Card".Trim.ToUpper Then
                lblTransType.Text = "Pan Card"
                txtTransID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text)
                txtAmount.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
                txtRemarks.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
                ModalPopupExtender1.Show()


            End If





            'If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID='" & lblRID.Text.Trim & "' ") > 0 Then
            '    lblDialogMsg.CssClass = ""
            '    lblDialogMsg.Text = "You Can't Delete "
            '    btnCancel.Text = "Ok"
            '    btnok.Visible = False
            '    ModalPopupExtender1.Show()
            'Else

            '    lblDialogMsg.Text = "Are you sure you want to delete ?"
            '    lblDialogMsg.CssClass = ""

            'End If
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
                    'GV.ExportToExcel_New(GridView1, Response, "", "APIWiseTransactionDetails", lblExportQry.Text, "dyanamic")
                    GV.ExportToExcel_DivNew(GridView1, Response, ApprovalDiv, "ServiceWiseTransReport", "", "")
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
            ddlServiceType.Items.Clear()

            If GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Commission".Trim.ToUpper Then
                ddlServiceType.Items.Add("All TYPE")
                ddlServiceType.Items.Add("RECH")
                ddlServiceType.Items.Add("BILLPAY")
                ddlServiceType.Items.Add("PAN CARD")
                ddlServiceType.Items.Add("MONEY TRANSFER")
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Money Transfer".Trim.ToUpper Then
                ddlServiceType.Items.Add("All TYPE")
                ddlServiceType.Items.Add("IMPS")
                ddlServiceType.Items.Add("NEFT")
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "AEPS".Trim.ToUpper Then
                ddlServiceType.Items.Add("All TYPE")
                ddlServiceType.Items.Add("AEPS")
                ddlServiceType.Items.Add("AADHAR PAY")
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "PAN CARD".Trim.ToUpper Then
                ddlServiceType.Items.Add("All TYPE")
                ddlServiceType.Items.Add("E CARD")
                ddlServiceType.Items.Add("P CARD")
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Recharge & Bill Payment".Trim.ToUpper Then
                ddlServiceType.Items.Add("All TYPE")
                ddlServiceType.Items.Add("RECH")
                ddlServiceType.Items.Add("BILLPAY")
                ddlServiceType.Items.Add("LANDLINE")
            ElseIf GV.parseString(ddlAmountType.SelectedValue.Trim).ToUpper = "Balance Transfer".Trim.ToUpper Then


                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim.ToUpper
                Dim DBName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim

                If (group.Trim.ToUpper = "Master Distributor".Trim.ToUpper) Then
                    'select distinct RegistrationId from  BOS_Dis_SubDis_Retailer_Registration where RefrenceID='MD-195' and AgentType='Distributor'

                    GV.FL.AddInDropDownListDistinct(ddlServiceType, "RegistrationId+':'+(isnull(FirstName,'')+' '+ LastName)", DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID='" & LoginID & "' and AgentType='Distributor'")
                    If ddlServiceType.Items.Count > 0 Then
                        ddlServiceType.Items.Insert(0, "ALL")
                        ddlServiceType.Items.Insert(1, "ALL Distributor")
                    Else
                        ddlServiceType.Items.Add("ALL")
                        ddlServiceType.Items.Add("ALL Distributor")
                    End If

                ElseIf (group.Trim.ToUpper = "Distributor".Trim.ToUpper) Then
                    GV.FL.AddInDropDownListDistinct(ddlServiceType, "RegistrationId+':'+(isnull(FirstName,'')+' '+ LastName) ", DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID='" & LoginID & "' and AgentType='Retailer'")
                    If ddlServiceType.Items.Count > 0 Then
                        ddlServiceType.Items.Insert(0, "ALL")
                        ddlServiceType.Items.Insert(1, "ALL Retailer")
                    Else
                        ddlServiceType.Items.Add("ALL")
                        ddlServiceType.Items.Add("ALL Retailer")
                    End If
                Else
                    ddlServiceType.Items.Add("N/A")


                End If


            Else
                ddlServiceType.Items.Add("N/A")
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub ddlTransactionBetween_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransactionBetween.SelectedIndexChanged
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

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        Try

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim loginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
            Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim.ToUpper
            Dim DBName As String = GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim


            Dim VRequestDate, VRequestID, VkCode, VkCodeType, VTransType, VTransID, VAmount, VRemarks, VApprovedBy, VApprovedDateTime, VApporvedStatus, VApporveRemarks, VRefund_TransID, VCompanyCode, VRecordDateTime, VUpdatedBy, VUpdatedOn As String
            VRequestDate = Now.Date
            VRequestID = GV.FL_AdminLogin.getAutoNumber("TransId")
            VkCode = loginID
            VkCodeType = group
            VTransType = GV.parseString(lblTransType.Text.Trim)
            VTransID = GV.parseString(txtTransID.Text.Trim)
            VAmount = GV.parseString(txtAmount.Text.Trim)
            VRemarks = GV.parseString(txtRemarks.Text.Trim)
            VApprovedBy = "NULL"
            VApprovedDateTime = "NULL"
            VApporvedStatus = "Pending"
            VApporveRemarks = "NULL"
            VRefund_TransID = "NULL"
            VCompanyCode = CompanyCode
            VRecordDateTime = Now
            VUpdatedBy = loginID
            VUpdatedOn = Now


            If VAmount.Trim = "" Then
                VAmount = "0"
            End If

            QryStr = "insert into " & DBName & ".dbo.BOS_Refund_Request_Master (RequestDate,RequestID,kCode,kCodeType,TransType,TransID,Amount,Remarks,ApprovedBy,ApprovedDateTime,ApporvedStatus,ApporveRemarks,Refund_TransID,CompanyCode,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & VRequestDate & "','" & VRequestID & "','" & VkCode & "','" & VkCodeType & "','" & VTransType & "','" & VTransID & "'," & VAmount & ",'" & VRemarks & "'," & VApprovedBy & "," & VApprovedDateTime & ",'" & VApporvedStatus & "'," & VApporveRemarks & "," & VRefund_TransID & ",'" & VCompanyCode & "','" & VRecordDateTime & "','" & VUpdatedBy & "','" & VUpdatedOn & "' );"


            If GV.parseString(lblTransType.Text.Trim).ToUpper = "Recharge & Bill Payment".Trim.ToUpper Then
                QryStr = QryStr & " update " & DBName & ".dbo.BOS_Recharge_API set Refund_Req_Status='Pending' where TransID='" & VTransID & "' and RetailerID='" & VkCode & "';"

            ElseIf GV.parseString(lblTransType.Text.Trim).ToUpper = "Money Transfer".Trim.ToUpper Then
                QryStr = QryStr & " update " & DBName & ".dbo.BOS_MoneyTransfer_API set Refund_Req_Status='Pending' where TransID='" & VTransID & "' and RefrenceNo='" & VkCode & "';"

            ElseIf GV.parseString(lblTransType.Text.Trim).ToUpper = "Pan Card".Trim.ToUpper Then
                QryStr = QryStr & " update " & DBName & ".dbo.BOS_Pan_Card_API set Refund_Req_Status='Pending' where TransID='" & VTransID & "' and AgentID='" & VkCode & "';"

            End If


            If GV.FL.DMLQueries(QryStr) = True Then
                lblMsgDialog.Text = "Request Submitted Successfully.<br> Request ID : " & VRequestID
                lblMsgDialog.CssClass = "Successlabels"
                btnUpdate.Visible = False
                btnCancel.Text = "Ok"
                Bind()
            Else
                lblMsgDialog.Text = "Request Failed, Try Again Later."
                lblMsgDialog.CssClass = "errorlabels"
            End If

            ModalPopupExtender1.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class