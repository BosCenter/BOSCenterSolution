Public Class API_Balance_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlAgentType.Items.Clear()
                ddlSelectCriteria.Items.Clear()

                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                If group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    ddlAgentType.Items.Add("All Admin")
                    ddlSelectCriteria.Items.Add("All Records")
                ElseIf group.Trim.ToUpper = "Admin".Trim.ToUpper Then
                    ddlAgentType.Items.Add("All AgentType")
                    ddlAgentType.Items.Add("Customer")
                    ddlAgentType.Items.Add("Master Distributor")
                    ddlAgentType.Items.Add("Retailer")
                    ddlAgentType.Items.Add("Distributor")

                    ddlSelectCriteria.Items.Add("All Records")
                    ddlSelectCriteria.Items.Add("Agent ID")
                    ddlSelectCriteria.Items.Add("Reference ID")
                    ddlSelectCriteria.Items.Add("Name")
                ElseIf group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    ddlAgentType.Items.Add("All AgentType")
                    ddlAgentType.Items.Add("Distributor")
                    ddlAgentType.Items.Add("Customer")

                    ddlSelectCriteria.Items.Add("All Records")
                    ddlSelectCriteria.Items.Add("Agent ID")
                    ddlSelectCriteria.Items.Add("Name")
                ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    ddlAgentType.Items.Add("All AgentType")
                    ddlAgentType.Items.Add("Customer")
                    ddlAgentType.Items.Add("Retailer")

                    ddlSelectCriteria.Items.Add("All Records")
                    ddlSelectCriteria.Items.Add("Agent ID")
                    ddlSelectCriteria.Items.Add("Name")
                Else

                End If

                btnSearch_Click(sender, e)

                'Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                'If group = "Distributor" Then
                '    lblHeading.Text = "Go To Sub Distributor Account"

                'ElseIf group = "Sub Distributor" Then
                '    lblHeading.Text = "Go To Retailer Account"
                'Else
                '    lblHeading.Text = "Go To Distributor Account"
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub




    Public Sub clear()
        Try
            ds = New DataSet
            txtSearchString.Text = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

            'ddlSelectCriteria.SelectedIndex = 0

            'ddlAgentType.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

            If Not ddlSelectCriteria.SelectedIndex = 0 Then
                If txtSearchString.Text.Trim = "" Then
                    lblError1.Text = "Please Enter the Value"
                    lblError1.CssClass = "errorlabels"
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                    Exit Sub
                End If
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub

    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim Loginid As String = ""
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim MoreColumns As String = ""

            If group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then

                Querystring = "select RID as SrNo,(CONVERT(VARCHAR(11),RegisterationDate,106)) as RegistrationDate,CompanyCode as 'AdminID',CompanyName,ContactPerson,Mobile_No,Email_ID,ClientPassword,ClientPin,DatabaseName as 'DBName','0' as 'APIBalance' from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration order by rid desc"
                
                lblExportQry.Text = Querystring
                If Not Querystring = "" Then
                    GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)
                    GridView1.DataBind()
                    If GridView1.Rows.Count > 0 Then
                        lblNoRecords.Text = ""
                        lblNoRecords.CssClass = ""
                        Dim TotalAmt As Decimal = 0


                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            GridView1.Rows(i).Cells(0).Text = i + 1
                            GridView1.Rows(i).Cells(10).Text = GV.returnAPIBalance(GV.parseString(GridView1.Rows(i).Cells(9).Text.Trim))
                            TotalAmt = TotalAmt + CDec(GridView1.Rows(i).Cells(10).Text)
                        Next

                        GridView1.FooterRow.Cells(9).ForeColor = Drawing.Color.Blue
                        GridView1.FooterRow.Cells(10).ForeColor = Drawing.Color.Blue

                        GridView1.FooterRow.Cells(9).Font.Bold = True
                        GridView1.FooterRow.Cells(10).Font.Bold = True

                        GridView1.FooterRow.Cells(9).Text = "Total"
                        GridView1.FooterRow.Cells(10).Text = TotalAmt
                    Else
                        clear()
                        lblNoRecords.Text = "No Records Found"
                        lblNoRecords.CssClass = "errorlabels"
                    End If
                End If
            ElseIf group.Trim.ToUpper = "Admin".Trim.ToUpper Then
                If group.Trim.ToUpper = "Admin".ToUpper Then
                    MoreColumns = " ,AgentPassword as 'Password',TransactionPin as 'Pin' "
                End If

                Dim agentTypeFilter As String = ""

                If Not ddlAgentType.SelectedIndex = 0 Then
                    agentTypeFilter = " AgentType= '" & ddlAgentType.SelectedValue & "'"
                End If

                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    If Not agentTypeFilter.Trim = "" Then
                        agentTypeFilter = " Where " & agentTypeFilter
                    End If
                Else
                    If Not agentTypeFilter.Trim = "" Then
                        agentTypeFilter = " and " & agentTypeFilter
                    End If
                End If



                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & "  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration]  " & agentTypeFilter & "  order by  AgentType,RID desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Name" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & " from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where   FirstName  like '" & GV.parseString(txtSearchString.Text.Trim) & "%'  " & agentTypeFilter & "  order by  AgentType,RID desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Agent ID" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & " from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where   RegistrationId='" & GV.parseString(txtSearchString.Text.Trim) & "'  " & agentTypeFilter & "   order by  AgentType,RID desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Reference ID" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & " from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where  RefrenceID='" & GV.parseString(txtSearchString.Text.Trim) & "'  " & agentTypeFilter & "   order by  AgentType,RID desc"
                End If

                lblExportQry.Text = Querystring
                If Not Querystring = "" Then
                    GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)
                    GridView1.DataBind()
                    If GridView1.Rows.Count > 0 Then
                        lblNoRecords.Text = ""
                        lblNoRecords.CssClass = ""
                        Dim TotalAmt As Decimal = 0


                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            GridView1.Rows(i).Cells(0).Text = i + 1
                            GridView1.Rows(i).Cells(9).Text = AgentBalance(GV.parseString(GridView1.Rows(i).Cells(3).Text.Trim))
                            TotalAmt = TotalAmt + CDec(GridView1.Rows(i).Cells(9).Text)
                        Next

                        GridView1.FooterRow.Cells(8).ForeColor = Drawing.Color.Blue
                        GridView1.FooterRow.Cells(9).ForeColor = Drawing.Color.Blue

                        GridView1.FooterRow.Cells(8).Font.Bold = True
                        GridView1.FooterRow.Cells(9).Font.Bold = True

                        GridView1.FooterRow.Cells(8).Text = "Total"
                        GridView1.FooterRow.Cells(9).Text = TotalAmt

                    Else
                        clear()
                        lblNoRecords.Text = "No Records Found"
                        lblNoRecords.CssClass = "errorlabels"
                    End If
                End If
            ElseIf group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or group.Trim.ToUpper = "Distributor".Trim.ToUpper Then


                If group.Trim.ToUpper = "Admin".ToUpper Then
                    MoreColumns = "  "
                End If

                Dim agentTypeFilter As String = ""

                If Not ddlAgentType.SelectedIndex = 0 Then
                    agentTypeFilter = " and  AgentType= '" & ddlAgentType.SelectedValue & "'"
                End If


                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & "  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where  RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'     " & agentTypeFilter & "  order by  AgentType,RID desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Name" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & " from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where  RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'  and    FirstName  like '" & GV.parseString(txtSearchString.Text.Trim) & "%'  " & agentTypeFilter & "  order by  AgentType,RID desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Agent ID" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & " from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'  and   RegistrationId='" & GV.parseString(txtSearchString.Text.Trim) & "'  " & agentTypeFilter & "   order by  AgentType,RID desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Reference ID" Then
                    Querystring = "select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & " from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where  RefrenceID='" & GV.parseString(txtSearchString.Text.Trim) & "'  " & agentTypeFilter & "   order by  AgentType,RID desc"
                End If

                lblExportQry.Text = Querystring
                If Not Querystring = "" Then
                    GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)
                    GridView1.DataBind()
                    If GridView1.Rows.Count > 0 Then
                        lblNoRecords.Text = ""
                        lblNoRecords.CssClass = ""
                        Dim TotalAmt As Decimal = 0


                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            GridView1.Rows(i).Cells(0).Text = i + 1
                            GridView1.Rows(i).Cells(9).Text = AgentBalance(GV.parseString(GridView1.Rows(i).Cells(3).Text.Trim))
                            TotalAmt = TotalAmt + CDec(GridView1.Rows(i).Cells(9).Text)
                        Next

                        GridView1.FooterRow.Cells(8).ForeColor = Drawing.Color.Blue
                        GridView1.FooterRow.Cells(9).ForeColor = Drawing.Color.Blue

                        GridView1.FooterRow.Cells(8).Font.Bold = True
                        GridView1.FooterRow.Cells(9).Font.Bold = True

                        GridView1.FooterRow.Cells(8).Text = "Total"
                        GridView1.FooterRow.Cells(9).Text = TotalAmt

                    Else
                        clear()
                        lblNoRecords.Text = "No Records Found"
                        lblNoRecords.CssClass = "errorlabels"
                    End If
                End If
            Else

            End If

            

        Catch ex As Exception
            lblNoRecords.Text = ex.Message
        End Try
    End Sub
    Dim balanceAmt As Decimal
    Public Function AgentBalance(ByVal AgentID As String) As Decimal
        balanceAmt = 0.0
        Try

            Dim str As String = "select ((select isnull(Sum(isnull(TransferAmt,0)),0) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & AgentID & "')"
            str = str & " - "
            str = str & " (select isnull(Sum(isnull(TransferAmt,0)),0) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & AgentID & "')) as 'WalletBal' "


            Dim LocalDS As New DataSet
            LocalDS = GV.FL.OpenDsWithSelectQuery(str)

            If Not LocalDS Is Nothing Then
                If LocalDS.Tables.Count > 0 Then
                    If LocalDS.Tables(0).Rows.Count > 0 Then
                        balanceAmt = LocalDS.Tables(0).Rows(0).Item(0)
                    End If
                End If
            End If
        Catch ex As Exception
            Return balanceAmt
        End Try
        Return balanceAmt
    End Function
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
        Catch ex As Exception

        End Try
    End Sub




    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

   

End Class