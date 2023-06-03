Public Class BOS_TransactionReport
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                GroupWiseFilter()
                CheckBox1_CheckedChanged(sender, e)
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                
                If group = "Master Distributor" Then
                    ddlSelect.SelectedIndex = 0
                    ddlSelect.Enabled = False
                    ddlSelect.CssClass = "form-control"
                ElseIf group = "Distributor" Then
                    ddlSelect.SelectedIndex = 0
                    ddlSelect.Enabled = False
                    ddlSelect.CssClass = "form-control"
                Else
                    ddlSelect.SelectedIndex = 0
                    ddlSelect.Enabled = True
                    ddlSelect.CssClass = "form-control"
                End If
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

    Public Sub clear()
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblExportQry.Text = ""
            lblClosingBal.Text = "0"

            lblError.Text = ""
            lblError.CssClass = ""
            lblError0.Text = ""
            lblError0.CssClass = ""
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

        Catch ex As Exception

        End Try
    End Sub
    Public Sub GroupWiseFilter()
        Try
            ddlSelectCriteria.Items.Clear()
            ddlvalue.Items.Clear()
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Loginid As String = ""
            If group = "Master Distributor" Then
                Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                group = "Distributor"
                ddlSelectCriteria.Items.Add("All Transaction")
                ddlSelectCriteria.Items.Add("DistributorID")
                ddlvalue.Items.Add("All Distributor")
            ElseIf group = "Distributor" Then
                Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                group = "Retailer"
                ddlSelectCriteria.Items.Add("All Transaction")
                ddlSelectCriteria.Items.Add("RetailerID")
                ddlvalue.Items.Add("All Retailer")
            Else
                Loginid = ""
                group = "Master Distributor"
                ddlSelectCriteria.Items.Add("All Transaction")
                ddlSelectCriteria.Items.Add("Master DistributorID")
                ddlvalue.Items.Add("All Master Distributor")
            End If
        Catch ex As Exception

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
            lblClosingBal.Text = "0"

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

        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
            lblExportQry.Text = ""
            chkduration.Checked = False
            CheckBox1_CheckedChanged(sender, e)
            ddlSelect.SelectedIndex = 0
            ddlSelect_SelectedIndexChanged(sender, e)
            'ddlSelectCriteria.SelectedIndex = 0
            'ddlSelectCriteria_SelectedIndexChanged(sender, e)
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
            Dim group1 As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            If (group1 = "Master Distributor" Or group1 = "Distributor" Or group1 = "Retailer") Then
                If chkduration.Checked = True Then
                    If Not ddlSelect.SelectedIndex = 0 Then
                        If ddlSelectCriteria.SelectedIndex = 0 Then
                            SearchColumnName = ""
                        Else


                        End If
                    Else
                        If ddlvalue.SelectedIndex = 0 Then
                            Value = ""
                        Else
                            Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                            Value = " And TransferTo='" & aa(0) & "' "
                        End If
                        SearchColumnName = ""
                    End If

                    Querystring = "select RID as SrNo,TransferFrom ,isnull((Select (FirstName+ ' ' +LastName) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferFrom),'BOS CENTER PVT LTD') as TransferFromName,TransferTo ,isnull((Select (FirstName+ ' ' +LastName) from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo),'BOS CENTER PVT LTD')  as TransferToName,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_TransferAmountToAgents] b where TransferFrom in (select RefrenceId from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RefrenceId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ) and TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'  " & Value & "  " & SearchColumnName & "  order by  RID Desc"

                Else
                    If Not ddlSelect.SelectedIndex = 0 Then
                        If ddlSelectCriteria.SelectedIndex = 0 Then
                            SearchColumnName = ""
                        Else
                            If ddlvalue.SelectedIndex = 0 Then
                                Value = ""
                            Else
                                Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                                Value = " And TransferTo='" & aa(0) & "' "
                            End If
                        End If
                    Else
                        If ddlvalue.SelectedIndex = 0 Then
                            Value = ""
                        Else
                            Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                            Value = " And TransferTo='" & aa(0) & "' "
                        End If
                        SearchColumnName = ""
                    End If


                    RefrenceId = " RefrenceID ='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                    Querystring = "select RID as SrNo,TransferFrom ,isnull((Select (FirstName+ ' ' +LastName) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferFrom),'BOS CENTER PVT LTD') as TransferFromName,TransferTo ,isnull((Select (FirstName+ ' ' +LastName) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo),'BOS CENTER PVT LTD')  as TransferToName,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_TransferAmountToAgents] b  where TransferFrom in (select RefrenceId from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where " & RefrenceId & " ) " & Value & " " & SearchColumnName & "   order by  RID Desc"
                End If

            Else


                If chkduration.Checked = True Then
                    If Not ddlSelect.SelectedIndex = 0 Then
                        If ddlSelectCriteria.SelectedIndex = 0 Then
                            lblNoRecords.Text = "Please Select Criteria"
                            lblNoRecords.CssClass = "errorlabels"
                            SearchColumnName = ""
                            Exit Sub
                        Else
                            Dim CT() As String = GV.parseString(ddlSelectCriteria.SelectedValue.Trim).Split(":")
                            If ddlvalue.SelectedIndex = 0 Then
                                Dim final As String = "('" & CT(0) & "')"
                                ' Value = " And TransferTo in " & final & " "
                                Value = " And (TransferFrom in ('" & CT(0) & "'))  "
                            Else
                                Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                                Dim final As String = "('" & CT(0) & "'" & "," & "'" & aa(0) & "')"
                                Value = " And (TransferTo in ('" & aa(0) & "') and TransferFrom in ('" & CT(0) & "')) or (TransferTo in  ('" & CT(0) & "') and TransferFrom in ('" & aa(0) & "'))  "
                            End If
                        End If
                    Else
                        If Not ddlvalue.SelectedIndex = 0 Then
                            Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                            Dim final As String = "('" & aa(0) & "')"
                            ' Value = " AND TransferTo in " & final & " "
                            Value = " And (TransferTo in ('" & aa(0) & "') and TransferFrom in ('" & aa(0) & "')) or (TransferTo in  ('" & aa(0) & "') and TransferFrom in ('" & aa(0) & "'))  "
                        Else
                            Value = ""
                        End If
                        SearchColumnName = ""
                        ' RefrenceId = "  TransferFrom in (select RefrenceId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID ='ADMIN' ) "
                    End If
                    Querystring = "select RID as SrNo,TransferFrom ,isnull((Select (FirstName+ ' ' +LastName) from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferFrom),'BOS CENTER PVT LTD') as TransferFromName,TransferTo ,isnull((Select (FirstName+ ' ' +LastName) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo),'BOS CENTER PVT LTD')  as TransferToName,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_TransferAmountToAgents] b where   TransactionDate between  '" & GV.FL.returnDateMonthWise(txtFrom.Text) & "' and '" & GV.FL.returnDateMonthWise(txtTO.Text) & "'  " & Value & "  " & SearchColumnName & "  order by  RID Desc"

                Else
                    If Not ddlSelect.SelectedIndex = 0 Then
                        If ddlSelectCriteria.SelectedIndex = 0 Then
                            lblNoRecords.Text = "Please Select Criteria"
                            lblNoRecords.CssClass = "errorlabels"
                            SearchColumnName = ""
                            Exit Sub
                        Else
                            Dim CT() As String = GV.parseString(ddlSelectCriteria.SelectedValue.Trim).Split(":")
                            If ddlvalue.SelectedIndex = 0 Then
                                Dim final As String = "('" & CT(0) & "')"
                                'Value = " where TransferTo in " & final & " "
                                Value = " where (TransferFrom in ('" & CT(0) & "')) "
                            Else
                                Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                                Dim final As String = "('" & CT(0) & "'" & "," & "'" & aa(0) & "')"
                                ' Value = " where TransferTo in " & final & " "
                                Value = " where (TransferTo in ('" & aa(0) & "') and TransferFrom in ('" & CT(0) & "')) or (TransferTo in  ('" & CT(0) & "') and TransferFrom in ('" & aa(0) & "'))  "
                            End If
                        End If
                    Else
                        If Not ddlvalue.SelectedIndex = 0 Then
                            Dim aa() As String = GV.parseString(ddlvalue.SelectedValue.Trim).Split(":")
                            Dim final As String = "('" & aa(0) & "')"
                            ' Value = " AND TransferTo in " & final & " "
                            Value = " Where (TransferFrom in ('" & aa(0) & "')) or (TransferTo in  ('" & aa(0) & "'))  "
                        Else
                            Value = ""
                        End If
                        SearchColumnName = ""
                        'RefrenceId = " where TransferFrom in (select RefrenceId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID ='ADMIN' )"
                    End If



                    Querystring = "select RID as SrNo,TransferFrom ,isnull((Select (FirstName+ ' ' +LastName) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferFrom),'BOS CENTER PVT LTD') as TransferFromName,TransferTo ,isnull((Select (FirstName+ ' ' +LastName) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo),'BOS CENTER PVT LTD')  as TransferToName,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_TransferAmountToAgents] b   " & Value & " " & SearchColumnName & "   order by  RID Desc"
                End If

            End If
            

            If Not Querystring = "" Then

                lblExportQry.Text = Querystring.Replace("RID as SrNo", "Row_Number() Over(order by rid desc)  as SrNo")
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then
                    Dim Balnceamt As String = 0
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        If Balnceamt = "" Then
                            Balnceamt = GV.parseString(GridView1.Rows(i).Cells(5).Text)
                        Else
                            Balnceamt = CDec(Balnceamt) + CDec(GridView1.Rows(i).Cells(5).Text)
                        End If
                        GridView1.Rows(i).Cells(6).Text = Balnceamt
                    Next
                    lblClosingBal.Text = Balnceamt
                    GridView1.FooterRow.Cells(5).Text = "Total"
                    GridView1.FooterRow.Cells(6).Text = Balnceamt
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 0)
                Else
                    'clear()
                    lblNoRecords.Text = "Sorry !! No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                End If
            End If

        Catch ex As Exception
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
                    GV.ExportToExcel_New(GridView1, Response, "", "TransactionDetails", lblExportQry.Text, "dyanamic")
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

    Protected Sub ddlSelectCriteria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectCriteria.SelectedIndexChanged
        Try
            Dim Loginid As String = ""
            ddlvalue.Items.Clear()
            Dim Value As String = ""
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            Dim group1 As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            If (group1 = "Master Distributor" Or group1 = "Distributor") Then
                If ddlSelectCriteria.SelectedIndex = 0 Then
                    GroupWiseFilter()
                Else
                    Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                    If group = "Master Distributor" Then
                        Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                        group = "Distributor"
                        ddlvalue.Items.Add("All Distributor")
                    ElseIf group = "Distributor" Then
                        Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                        group = "Retailer"

                        ddlvalue.Items.Add("All Retailer")
                    Else
                        Loginid = ""
                        group = "Master Distributor"
                        ddlvalue.Items.Add("All Master Distributor")
                    End If

                    GV.FL.AddInDropDownListDistinct(ddlvalue, "RegistrationId+':'+FirstName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & group & "' " & Loginid & "")
                    If ddlvalue.Items.Count > 0 Then
                        ddlvalue.Items.Insert(0, "All " & group & " ")
                    Else
                        ddlvalue.Items.Insert(0, "All " & group & " ")
                    End If
                End If
            Else

                'Loginid = " And RefrenceID='" & GV.parseString(ddlSelectCriteria.SelectedValue) & "' "

                If Not ddlSelectCriteria.SelectedIndex = 0 Then
                    Dim aa() As String = GV.parseString(ddlSelectCriteria.SelectedValue.Trim).Split(":")
                    Value = " And RefrenceID='" & aa(0) & "' "
                    If Not ddlSelectCriteria.SelectedIndex = 0 Then
                        If ddlSelect.SelectedValue.Trim.ToUpper = "Not Applicable".Trim.ToUpper Then
                            If ddlSelectCriteria.SelectedIndex = 0 Then
                                GroupWiseFilter()
                            Else
                                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                                If group = "Master Distributor" Then
                                    Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                                    group = "Distributor"
                                    ddlvalue.Items.Add("All Distributor")
                                ElseIf group = "Distributor" Then
                                    Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                                    group = "Retailer"

                                    ddlvalue.Items.Add("All Retailer")
                                Else
                                    Loginid = ""
                                    group = "Master Distributor"
                                    ddlvalue.Items.Add("All Master Distributor")
                                End If

                                GV.FL.AddInDropDownListDistinct(ddlvalue, "RegistrationId+':'+FirstName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & group & "' " & Loginid & "")
                                If ddlvalue.Items.Count > 0 Then
                                    ddlvalue.Items.Insert(0, "All " & group & " ")
                                Else
                                    ddlvalue.Items.Insert(0, "All " & group & " ")
                                End If
                            End If
                        ElseIf ddlSelect.SelectedValue.Trim.ToUpper = "Master Distributor Wise".Trim.ToUpper Then
                            GV.FL.AddInDropDownListDistinct(ddlvalue, "RegistrationId+':'+FirstName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Distributor' " & Value & "")
                            If ddlvalue.Items.Count > 0 Then
                                ddlvalue.Items.Insert(0, "All Transaction")
                            Else
                                ddlvalue.Items.Add("All Transaction")
                            End If
                        ElseIf ddlSelect.SelectedValue.Trim.ToUpper = "Distributor Wise".Trim.ToUpper Then
                            GV.FL.AddInDropDownListDistinct(ddlvalue, "RegistrationId+':'+FirstName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Retailer' " & Value & "")
                            If ddlvalue.Items.Count > 0 Then
                                ddlvalue.Items.Insert(0, "All Transaction")
                            Else
                                ddlvalue.Items.Add("All Transaction")
                            End If

                        End If
                    Else
                        ddlvalue.Items.Add("All Distributor")

                    End If
                Else

                    ddlvalue.Items.Add("All Distributor")
                End If



            End If
            
            

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelect.SelectedIndexChanged
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            ddlvalue.Items.Clear()
            ddlSelectCriteria.Items.Clear()
            If ddlSelect.SelectedValue.Trim.ToUpper = "Not Applicable".Trim.ToUpper Then
                ddlSelectCriteria.Items.Add("All Transaction")
                ddlSelectCriteria.Items.Add("Master DistributorID")
                ddlvalue.Items.Add("All Master Distributor")
            ElseIf ddlSelect.SelectedValue.Trim.ToUpper = "Master Distributor Wise".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlSelectCriteria, "RegistrationId+':'+FirstName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Master Distributor'")
                If ddlSelectCriteria.Items.Count > 0 Then
                    ddlSelectCriteria.Items.Insert(0, "Select Master DistributorID")
                Else
                    ddlSelectCriteria.Items.Add("Select Master DistributorID")
                End If
                ddlvalue.Items.Add("All Transaction")
            ElseIf ddlSelect.SelectedValue.Trim.ToUpper = "Distributor Wise".Trim.ToUpper Then

                GV.FL.AddInDropDownListDistinct(ddlSelectCriteria, "RegistrationId+':'+FirstName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Distributor'")
                If ddlSelectCriteria.Items.Count > 0 Then
                    ddlSelectCriteria.Items.Insert(0, "Select DistributorID")
                Else
                    ddlSelectCriteria.Items.Add("Select DistributorID")
                End If
                ddlvalue.Items.Add("All Transaction")
            End If

           

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlvalue_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlvalue.SelectedIndexChanged
        Try
            GridView1.DataSource = Nothing
            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class