


Imports Org.BouncyCastle.Crypto.Agreement

Public Class SAM_DashBoard
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Dim aa() As String
    Dim qry As String = ""
    ' Dim todaydate As Date = CDate(Now.Date.ToString("MM/dd/yyyy"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim group1 As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            If Not IsPostBack Then
                divmobile.Attributes("style") = "display:none"
                divmobile1.Attributes("style") = "display:none"
                divmobile2.Attributes("style") = "display:none"
                divmobile_paykare.Attributes("style") = "display:none"
                divmobile_EasyTalk.Attributes("style") = "display:none"


                Div_API.Visible = True
                div_AD_MD_DIS.Visible = False
                div_RET.Visible = False
                Dim DBName11 As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim
                If DBName11 = "CMP1165" Then
                    lblWalletTransferName.Text = "KM To KM"
                ElseIf DBName11 = "CMP1205" Then
                    lblWalletTransferName.Text = "KM To KM"
                ElseIf DBName11 = "CMP1179" Then
                    lblWalletTransferName.Text = "KM To KM"
                ElseIf DBName11 = "CMP1218" Then
                    lblWalletTransferName.Text = "KM To KM"
                ElseIf DBName11 = "CMP1174" Then
                    lblWalletTransferName.Text = "KM To KM"
                ElseIf DBName11 = "CMP1235" Then
                    lblWalletTransferName.Text = "KM To KM"
                Else
                    lblWalletTransferName.Text = "Wallet Transfer"
                End If
                If group1 = "Retailer" Or group1.Trim.ToUpper = "Customer".ToUpper Then

                    div_md_ds_Load.Visible = False
                    div_md_ds_Networth.Visible = False
                    div_md_ds_WalletTransfer.Visible = False

                    If fBrowserIsMobile() = False Then

                        'Case if mobile view is off
                        If group1 = "Retailer" Then
                            Div_API.Visible = True
                            div_AD_MD_DIS.Visible = False
                            div_RET.Visible = True
                            Div_Month.Visible = True
                            If DBName11 = "CMP1235" Then
                                Div_Slider.Visible = True
                            End If


                            'chkAPIStatus()
                            Dim companycode As String = GV.FL.AddInVar("CompanyCode", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration where Databasename='" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "'")
                            Dim Str As String = " Select * from CRM_DyanamicServices_Rights_Dashboard where CompanyCode='" & companycode & "' and CanshowServices='1' ;"
                            ds = GV.FL.OpenDsWithSelectQuery(Str)
                            ListView1.DataSource = ds
                            ListView1.DataBind()

                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                            Dim btnPostbackUrl As Button = DirectCast(ListView1.Items(i).FindControl("btnPostbackUrl"), Button)
                                            Dim CanshowButton As String = ""
                                            CanshowButton = ds.Tables(0).Rows(i).Item("CanshowButton")
                                            If CanshowButton = "True" Then
                                                btnPostbackUrl.Visible = True
                                            Else
                                                btnPostbackUrl.Visible = False
                                            End If

                                            If Not IsDBNull(ds.Tables(0).Rows(i).Item("PostbackUrl")) Then
                                                If Not ds.Tables(0).Rows(i).Item("PostbackUrl").ToString() = "" Then
                                                    btnPostbackUrl.PostBackUrl = GV.parseString(ds.Tables(0).Rows(i).Item("PostbackUrl").ToString())
                                                Else
                                                    btnPostbackUrl.PostBackUrl = "#"
                                                End If
                                            Else
                                                btnPostbackUrl.PostBackUrl = "#"
                                            End If

                                        Next
                                    End If
                                End If
                            End If

                            'Dim strut As String = "select (isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type='Commission')) as 'TotalEarned'  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type='Commission' "

                            '=======================================================================
                            '///// Today Wise - Start
                            Dim StartDate, EndDate As Date
                            StartDate = Now.Date
                            EndDate = Now.Date
                            Dim LoginId As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                            Dim DBName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim



                            lbl_Today_Earned.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type='Commission')) as 'TotalEarned' ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type='Commission'  ")
                            If IsNumeric(lbl_Today_Earned.Text) Then
                                lbl_Today_Earned.Text = Math.Round(CDec(lbl_Today_Earned.Text))
                            Else
                                lbl_Today_Earned.Text = "0"
                            End If

                            lbl_Today_Load.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund') )) as 'TotalLoad' ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund')  ")
                            If IsNumeric(lbl_Today_Load.Text) Then
                                lbl_Today_Load.Text = Math.Round(CDec(lbl_Today_Load.Text))
                            Else
                                lbl_Today_Load.Text = "0"
                            End If

                            lbl_Today_Used.Text = GV.FL.AddInVar("isnull(Sum(TransferAmt),0) ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('POSTPAID','RECH','BILLPAY','Money Transfer-2','Service Charge','PAN CARD','BOS Shopping','Money Transfer','IRCTC Booking')  ")
                            If IsNumeric(lbl_Today_Used.Text) Then
                                lbl_Today_Used.Text = Math.Round(CDec(lbl_Today_Used.Text))
                            Else
                                lbl_Today_Used.Text = "0"
                            End If

                            lbl_Today_Pending.Text = GV.FL.AddInVar("isnull(Sum(Amount),0)", DBName & ".dbo.BOS_MakePayemnts_Details where  ApporvedStatus='Pending' and RegistrationId in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999'  ")
                            If IsNumeric(lbl_Today_Pending.Text) Then
                                lbl_Today_Pending.Text = Math.Round(CDec(lbl_Today_Pending.Text))
                            Else
                                lbl_Today_Pending.Text = "0"
                            End If


                            '///// Today Wise - End
                            '=======================================================================


                            '=======================================================================
                            '///// Month Wise - Start
                            StartDate = CDate(Now.Month & "/01" & "/" & Now.Year)
                            EndDate = CDate((Now.Month) & "/" & Date.DaysInMonth(Now.Year, Now.Month) & "/" & Now.Year)

                            lbl_Monthly_Earned.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type='Commission')) as 'TotalEarned' ", " BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type='Commission'  ")
                            If IsNumeric(lbl_Monthly_Earned.Text) Then
                                lbl_Monthly_Earned.Text = Math.Round(CDec(lbl_Monthly_Earned.Text))
                            Else
                                lbl_Monthly_Earned.Text = "0"
                            End If

                            lbl_Monthly_Load.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund') )) as 'TotalLoad' ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund')  ")
                            If IsNumeric(lbl_Monthly_Load.Text) Then
                                lbl_Monthly_Load.Text = Math.Round(CDec(lbl_Monthly_Load.Text))
                            Else
                                lbl_Monthly_Load.Text = "0"
                            End If

                            lbl_Monthly_Used.Text = GV.FL.AddInVar("isnull(Sum(TransferAmt),0) ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('POSTPAID','RECH','BILLPAY','Money Transfer-2','Service Charge','PAN CARD','BOS Shopping','Money Transfer','IRCTC Booking')  ")
                            If IsNumeric(lbl_Monthly_Used.Text) Then
                                lbl_Monthly_Used.Text = Math.Round(CDec(lbl_Monthly_Used.Text))
                            Else
                                lbl_Monthly_Used.Text = "0"
                            End If

                            lbl_Monthly_Pending.Text = GV.FL.AddInVar("isnull(Sum(Amount),0)", DBName & ".dbo.BOS_MakePayemnts_Details where  ApporvedStatus='Pending' and RegistrationId in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999'  ")
                            If IsNumeric(lbl_Monthly_Pending.Text) Then
                                lbl_Monthly_Pending.Text = Math.Round(CDec(lbl_Monthly_Pending.Text))
                            Else
                                lbl_Monthly_Pending.Text = "0"
                            End If


                            '///// Month Wise - End
                            '=======================================================================





                            '    Dim empDetails As DataSet = New DataSet
                            '    Dim STR2 As String = "SELECT COUNT(User_Name) as EmplyoeeType FROM CRM_Login_Details WHERE NOT User_Type='Super Admin' ;"
                            '    STR2 = STR2 & "SELECT COUNT(User_Name) as TotalTechnician FROM CRM_Login_Details WHERE User_Type='Technician'"

                            '    empDetails = GV.FL.OpenDsWithSelectQuery(STR2)

                            '    If Not empDetails Is Nothing Then
                            '        If empDetails.Tables.Count > 0 Then
                            '            If empDetails.Tables(0).Rows.Count > 0 Then
                            '                listviewEmpDetails.DataSource = empDetails
                            '                listviewEmpDetails.DataBind()

                            '                For i As Integer = 0 To listviewEmpDetails.Items.Count - 1
                            '                    Dim linkbtntotalemp As LinkButton = DirectCast(listviewEmpDetails.Items(i).FindControl("lnkbtnTotalEmployess"), LinkButton)
                            '                    Dim linkbtnTechnician As LinkButton = DirectCast(listviewEmpDetails.Items(i).FindControl("lnkbtnTotalTechnician"), LinkButton)

                            '                    If Not IsDBNull(empDetails.Tables(0).Rows(i).Item("EmplyoeeType")) Then
                            '                        If Not empDetails.Tables(0).Rows(i).Item("EmplyoeeType").ToString() = "" Then
                            '                            linkbtntotalemp.Text = GV.parseString(empDetails.Tables(0).Rows(i).Item("EmplyoeeType").ToString())
                            '                        Else
                            '                            linkbtntotalemp.Text = "0"
                            '                        End If
                            '                    Else
                            '                        linkbtntotalemp.Text = "0"
                            '                    End If


                            '                    If Not IsDBNull(empDetails.Tables(1).Rows(i).Item("TotalTechnician")) Then
                            '                        If Not empDetails.Tables(1).Rows(i).Item("TotalTechnician").ToString() = "" Then
                            '                            linkbtnTechnician.Text = GV.parseString(empDetails.Tables(1).Rows(i).Item("TotalTechnician").ToString())
                            '                        Else
                            '                            linkbtnTechnician.Text = "0"
                            '                        End If
                            '                    Else
                            '                        linkbtnTechnician.Text = "0"
                            '                    End If
                            '                Next
                            '            End If
                            '        End If
                            '    End If


                        ElseIf group1.Trim.ToUpper = "Customer".ToUpper Then

                            'Div_API.Visible = False
                            'div_AD_MD_DIS.Visible = False
                            'div_RET.Visible = True
                            'chkAPIStatus()

                            div_cust_dd.Visible = True



                                Dim companycode As String = GV.FL.AddInVar("CompanyCode", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration where Databasename='" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "'")
                                Dim Str As String = " Select * from CRM_DyanamicServices_Rights_Dashboard where CompanyCode='" & companycode & "' and CanshowServices_Cust='1' ;"
                                ds = GV.FL.OpenDsWithSelectQuery(Str)
                                ListView1.DataSource = ds
                                ListView1.DataBind()

                                If Not ds Is Nothing Then

                                    If ds.Tables.Count > 0 Then
                                        If ds.Tables(0).Rows.Count > 0 Then

                                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                                Dim btnPostbackUrl As Button = DirectCast(ListView1.Items(i).FindControl("btnPostbackUrl"), Button)
                                                Dim CanshowButton As String = ""
                                                CanshowButton = ds.Tables(0).Rows(i).Item("CanshowButton_Cust")
                                                If CanshowButton = "True" Then
                                                    btnPostbackUrl.Visible = True
                                                Else
                                                    btnPostbackUrl.Visible = False
                                                End If

                                                If Not IsDBNull(ds.Tables(0).Rows(i).Item("PostbackUrl")) Then
                                                    If Not ds.Tables(0).Rows(i).Item("PostbackUrl").ToString() = "" Then
                                                        btnPostbackUrl.PostBackUrl = GV.parseString(ds.Tables(0).Rows(i).Item("PostbackUrl").ToString())
                                                    Else
                                                        btnPostbackUrl.PostBackUrl = "#"
                                                    End If
                                                Else
                                                    btnPostbackUrl.PostBackUrl = "#"
                                                End If

                                            Next
                                        End If
                                    End If
                                End If

                            End If

                            Else
                        'Case if mobile view is on

                        If DBName11 = "CMP1205" Then
                            divmobile1.Visible = True
                            divmobile1.Attributes("style") = ""
                        ElseIf DBName11 = "CMP1179" Then
                            divmobile2.Visible = True
                            divmobile2.Attributes("style") = ""
                        ElseIf DBName11 = "CMP1218" Then
                            If group1 = "Retailer" Then

                                tr_Retailer_1.Visible = True
                                tr_Retailer_2.Visible = False
                                tr_Retailer_3.Visible = False
                                'Retailer_4.Visible = True


                                tr_Customer_1.Visible = False
                                tr_Customer_2.Visible = False
                                tr_Customer_3.Visible = False
                                tr_Customer_4.Visible = False
                                tr_Customer_5.Visible = False
                                tr_Customer_6.Visible = False
                                tr_Customer_7.Visible = False
                                tr_Customer_Mall.Visible = False


                                divmobile.Visible = True
                                divmobile.Attributes("style") = ""
                            ElseIf group1 = "Customer" Then
                                divmobile_paykare.Visible = True
                                divmobile_paykare.Attributes("style") = ""

                            End If
                        ElseIf DBName11 = "CMP1174" Then
                            If group1 = "Retailer" Then

                                tr_Retailer_1.Visible = True
                                tr_Retailer_2.Visible = False
                                tr_Retailer_3.Visible = False
                                'Retailer_4.Visible = True


                                tr_Customer_1.Visible = False
                                tr_Customer_2.Visible = False
                                tr_Customer_3.Visible = False
                                tr_Customer_4.Visible = False
                                tr_Customer_5.Visible = False
                                tr_Customer_6.Visible = False
                                tr_Customer_7.Visible = False
                                tr_Customer_Mall.Visible = False


                                divmobile.Visible = True
                                divmobile.Attributes("style") = ""
                            ElseIf group1 = "Customer" Then
                                divmobile_EasyTalk.Visible = True
                                divmobile_EasyTalk.Attributes("style") = ""
                            End If
                        Else

                            If group1 = "Retailer" Then
                                tr_Retailer_1.Visible = True
                                tr_Retailer_2.Visible = False
                                tr_Retailer_3.Visible = False
                                'Retailer_4.Visible = True

                                tr_Customer_1.Visible = False
                                tr_Customer_2.Visible = False
                                tr_Customer_3.Visible = False
                                tr_Customer_4.Visible = False
                                tr_Customer_5.Visible = False
                                tr_Customer_6.Visible = False
                                tr_Customer_7.Visible = False
                                tr_Customer_Mall.Visible = False


                                divmobile.Visible = True
                                divmobile.Attributes("style") = ""


                            ElseIf group1 = "Customer" Then
                                tr_Retailer_1.Visible = False
                                tr_Retailer_2.Visible = False
                                tr_Retailer_3.Visible = False


                                tr_Customer_1.Visible = True
                                tr_Customer_2.Visible = True
                                tr_Customer_3.Visible = True
                                tr_Customer_4.Visible = True
                                tr_Customer_5.Visible = True
                                tr_Customer_6.Visible = True
                                tr_Customer_7.Visible = True
                                tr_Customer_Mall.Visible = False


                                divmobile.Visible = True

                                divmobile.Attributes("style") = ""

                            End If

                        End If


                        '    chkAPIStatus()

                    End If



                ElseIf group1 = "Distributor" Or group1 = "Master Distributor" Then

                    Div_API.Visible = True
                    div_AD_MD_DIS.Visible = True

                    'lbl_API_Balance.Text = Math.Round(AgentBalance(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)))

                    show_MD_DIS_Bal()

                    div_md_ds_Load.Visible = True
                    div_md_ds_Networth.Visible = True
                    div_md_ds_WalletTransfer.Visible = True

                    Dim StartDate, EndDate As Date
                    StartDate = "01/01/2020" 'Now.Date
                    EndDate = Now.Date
                    Dim LoginId As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    Dim DBName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim
                    Dim reFilter As String = ""

                    If group1 = "Master Distributor" Then
                        reFilter = "  TransferFrom in ( select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID in (select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID='" & LoginId & "' and AgentType='Distributor') and AgentType='Retailer' ) "
                        lbl_Md_DS_NetWortdd.Text = GV.FL.AddInVar("isnull(Sum(TransferAmt),0) ", DBName & ".dbo.BOS_TransferAmountToAgents where " & reFilter & "    and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('POSTPAID','RECH','BILLPAY','Money Transfer-2','Service Charge','PAN CARD','Money Transfer','IRCTC Booking')  ")

                        StartDate = Now.Date
                        EndDate = Now.Date

                        lbl_md_ds_D_Load.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund') )) as 'TotalLoad' ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund')  ")


                        StartDate = CDate(Now.Month & "/01" & "/" & Now.Year)
                        EndDate = CDate((Now.Month) & "/" & Date.DaysInMonth(Now.Year, Now.Month) & "/" & Now.Year)

                        lbl_md_ds_M_Load.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund') )) as 'TotalLoad' ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund')  ")

                    ElseIf group1 = "Distributor" Then

                        reFilter = "  TransferFrom in ( select distinct RegistrationId from " & DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RefrenceID in ('" & LoginId & "') and AgentType='Retailer' ) "
                        Dim ss As String = DBName & ".dbo.BOS_TransferAmountToAgents where " & reFilter & "  and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('POSTPAID','RECH','BILLPAY','Money Transfer-2','Service Charge','PAN CARD','Money Transfer','IRCTC Booking')  "
                        lbl_Md_DS_NetWortdd.Text = GV.FL.AddInVar("isnull(Sum(TransferAmt),0) ", ss)

                        StartDate = Now.Date
                        EndDate = Now.Date

                        lbl_md_ds_D_Load.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund') )) as 'TotalLoad' ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund')  ")


                        StartDate = CDate(Now.Month & "/01" & "/" & Now.Year)
                        EndDate = CDate((Now.Month) & "/" & Date.DaysInMonth(Now.Year, Now.Month) & "/" & Now.Year)

                        lbl_md_ds_M_Load.Text = GV.FL.AddInVar("(isnull(Sum(TransferAmt),0) -(select isnull(Sum(TransferAmt),0)  from " & DBName & ".dbo.BOS_TransferAmountToAgents where  TransferFrom in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund') )) as 'TotalLoad' ", DBName & ".dbo.BOS_TransferAmountToAgents where TransferTo in ('" & LoginId & "')   and RecordDateTime between '" & StartDate & " 00:00:00.000' and '" & EndDate & " 23:59:59.999' and Amount_Type in ('Deposit','MakePayment','GSTRefund')  ")
                    End If


                    If IsNumeric(lbl_Md_DS_NetWortdd.Text) Then
                        lbl_Md_DS_NetWortdd.Text = Math.Round(CDec(lbl_Md_DS_NetWortdd.Text))
                    Else
                        lbl_Md_DS_NetWortdd.Text = "0"
                    End If

                    If IsNumeric(lbl_md_ds_D_Load.Text) Then
                        lbl_md_ds_D_Load.Text = Math.Round(CDec(lbl_md_ds_D_Load.Text))
                    Else
                        lbl_md_ds_D_Load.Text = "0"
                    End If

                    If IsNumeric(lbl_md_ds_M_Load.Text) Then
                        lbl_md_ds_M_Load.Text = Math.Round(CDec(lbl_md_ds_M_Load.Text))
                    Else
                        lbl_md_ds_M_Load.Text = "0"
                    End If


                ElseIf group1 = "Admin" Then
                    Div_API.Visible = True
                    div_AD_MD_DIS.Visible = True
                    div_API_Balance1.Visible = True
                    div_API_Log_Report.Visible = True
                    div_API_support.Visible = True

                    div_md_ds_Load.Visible = False
                    div_md_ds_Networth.Visible = False
                    div_md_ds_WalletTransfer.Visible = False

                    lbl_API_Balance.Text = API_Balance()
                ElseIf group1 = "Super Admin" Then

                    Div_API.Visible = True
                    div_AD_MD_DIS.Visible = True
                    div_API_Balance1.Visible = True
                    div_API_Log_Report.Visible = True
                    div_API_support.Visible = True

                    div_md_ds_Load.Visible = False
                    div_md_ds_Networth.Visible = False
                    div_md_ds_WalletTransfer.Visible = False

                    showSuperAdminBal()
                Else
                    'Div_API.Visible = False

                    Div_API.Visible = True
                    div_AD_MD_DIS.Visible = True

                    div_md_ds_Load.Visible = False
                    div_md_ds_Networth.Visible = False
                    div_md_ds_WalletTransfer.Visible = False

                    lbl_API_Balance.Text = API_Balance()

                End If
            End If

            If Not IsPostBack = True Then

                If Not Session("NotificationPic") Is Nothing Then
                    If Session("NotificationPic") = "Start" Then
                        Session("NotificationPic") = "End"
                        Dim NotificationPic As String = ""
                        If group1.Trim.ToUpper = "Retailer".ToUpper Or group1.Trim.ToUpper = "Customer".ToUpper Or group1.Trim.ToUpper = "Distributor".Trim.ToUpper Or group1.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                            NotificationPic = GV.FL.AddInVar("NotificationPic", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Bos_Notification_Master where AgentType='" & GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim & "' and  ActiveStatus='Active'")
                        ElseIf group1.Trim.ToUpper = "Admin".Trim.ToUpper Then
                            NotificationPic = GV.FL.AddInVar("NotificationPic", " " & GV.DefaultDatabase.Trim & ".dbo.Bos_Notification_Master_SA where AgentType='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and  ActiveStatus='Active'")
                        End If
                        If Not GV.parseString(NotificationPic.Trim) = "" Then
                            imgNotificationPic.ImageUrl = NotificationPic
                            ModalPopupExtender1.Show()
                        End If
                    End If

                End If



            End If

            '<-----------Ozzype UPIID & Address Start------------------>
            Dim x As String = ""
            x = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            txtCity.CssClass = "form-control"
            If Not IsPostBack Then
                ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & x & "'")
                Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                If grp = "Retailer".ToUpper Or grp = "Customer".ToUpper Then
                    Fill_Details()
                End If

                'If btn_update.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                '    lblError.Visible = True
                '    lblError.Text = "Record Updated Successfully"
                '    lblError.CssClass = "successLabels"
                'Else
                '    lblError.Visible = True
                '    lblError.Text = "Record Updation Failed"
                '    lblError.CssClass = "errorLabels"
                'End If
                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("City")) Then
                            If Not ds.Tables(0).Rows(0).Item("City").ToString() = "" Then
                                txtCity.Text = GV.parseString(ds.Tables(0).Rows(0).Item("City").ToString())
                            Else
                                txtCity.Text = ""
                            End If
                        Else
                            txtCity.Text = ""
                        End If
                    End If
                End If

            End If

            '<-----------Ozzype UPIID & Address  End------------------>
            'Div_API.Visible = True
        Catch ex As Exception

        End Try

    End Sub
    Public Sub show_MD_DIS_Bal()
        Try
            '"select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & "  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where  RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'     " & agentTypeFilter & "  order by  AgentType,RID desc"

            Dim Querystring As String = ""
            Querystring = "select RegistrationId from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where  RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
            If Not Querystring = "" Then
                ds = New DataSet
                ds = GV.FL.OpenDsWithSelectQuery(Querystring)
                Dim TotalAmt As Decimal = 0
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            TotalAmt = TotalAmt + CDec(AgentBalance(GV.parseString(ds.Tables(0).Rows(i).Item("RegistrationId"))))
                        Next
                    End If
                End If
                lbl_API_Balance.Text = TotalAmt
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub showSuperAdminBal()
        Try
            '"select RID as SrNo,AgentType,(FirstName+' '+LastName) as Name,RegistrationId,PanCardNumber as 'PanCard',EmailID,(CONVERT(VARCHAR(11),RegistrationDate,106)) as RegistrationDate,AgencyName,MobileNo,'0' as 'WalletBal'  " & MoreColumns & "  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where  RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'     " & agentTypeFilter & "  order by  AgentType,RID desc"

            Dim Querystring As String = ""
            Querystring = "select DatabaseName  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration order by rid desc"
            If Not Querystring = "" Then
                ds = New DataSet
                ds = GV.FL.OpenDsWithSelectQuery(Querystring)
                Dim TotalAmt As Decimal = 0
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            TotalAmt = TotalAmt + CDec(GV.returnAPIBalance(GV.parseString(ds.Tables(0).Rows(i).Item("DatabaseName"))))
                        Next
                    End If
                End If
                lbl_API_Balance.Text = TotalAmt
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub chkAPIStatus()
    '    Try
    '        '///// Start Check API  STATUS System Settings 

    '        Dim PANCardAPI_Status As String = ""
    '        PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

    '        If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
    '            btnPanCard.Visible = False
    '        End If

    '        '///// End Check API  STATUS Retailer Level Settings 

    '        Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

    '        '///// Start Check API  STATUS System Settings 
    '        PANCardAPI_Status = ""
    '        PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

    '        If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
    '            btnPanCard.Visible = False
    '        End If

    '        '///// End Check API  STATUS Retailer Level  Settings 



    '        '///// Start Check API  STATUS System Settings 

    '        Dim MoneyTransferAPI_Status As String = ""
    '        MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

    '        If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
    '            btnMoneyTransfer.Visible = False

    '        End If

    '        '///// End Check API  STATUS Retailer Level Settings 

    '        '///// Start Check API  STATUS System Settings 
    '        MoneyTransferAPI_Status = ""
    '        MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

    '        If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
    '            btnMoneyTransfer.Visible = False
    '        End If

    '        '///// End Check API  STATUS Retailer Level  Settings 

    '        '///// Start Check API  STATUS System Settings 

    '        Dim RechargeAPI_Status As String = ""
    '        RechargeAPI_Status = GV.FL.AddInVar("RechargeAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

    '        If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
    '            btnRecharge.Visible = False
    '        End If

    '        '///// End Check API  STATUS Retailer Level Settings 

    '        '///// Start Check API  STATUS System Settings 
    '        RechargeAPI_Status = ""
    '        RechargeAPI_Status = GV.FL.AddInVar("RechargeAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

    '        If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
    '            btnRecharge.Visible = False
    '        End If

    '        '///// End Check API  STATUS Retailer Level  Settings 
    '    Catch ex As Exception

    '    End Try
    'End Sub



    Dim API_balanceAmt As Decimal
    Public Function API_Balance() As Decimal
        API_balanceAmt = 0.0
        Try

            Dim str As String = "select distinct RegistrationId from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration"

            Dim LocalDS1 As New DataSet
            LocalDS1 = GV.FL.OpenDsWithSelectQuery(str)

            If Not LocalDS1 Is Nothing Then
                If LocalDS1.Tables.Count > 0 Then
                    If LocalDS1.Tables(0).Rows.Count > 0 Then

                        For i As Integer = 0 To LocalDS1.Tables(0).Rows.Count - 1
                            API_balanceAmt = API_balanceAmt + AgentBalance(LocalDS1.Tables(0).Rows(i).Item(0))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Return API_balanceAmt
        End Try
        Return API_balanceAmt
    End Function
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


    Dim MobileCheck As Regex = New Regex("(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od|ad)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase Or RegexOptions.Multiline Or RegexOptions.Compiled)
    Dim MobileVersionCheck As Regex = New Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase Or RegexOptions.Multiline Or RegexOptions.Compiled)

    Public Function fBrowserIsMobile() As Boolean
        Dim result As Boolean = False
        Try
            Dim userAgent As String = Request.ServerVariables("HTTP_USER_AGENT")
            Debug.Assert(Not HttpContext.Current Is Nothing)

            If Not HttpContext.Current.Request Is Nothing And Not HttpContext.Current.Request.ServerVariables("HTTP_USER_AGENT") Is Nothing Then
                Dim u As String = HttpContext.Current.Request.ServerVariables("HTTP_USER_AGENT").ToString()
                If u.Length < 4 Then
                    result = False
                ElseIf MobileCheck.IsMatch(u) Or MobileVersionCheck.IsMatch(u.Substring(0, 4)) Then
                    result = True
                End If

            End If


            'Dim device_info As String = String.Empty
            'If OS.IsMatch(userAgent) Then
            '    device_info = OS.Match(userAgent).Groups(0).Value
            'End If
            'If device.IsMatch(userAgent.Substring(0, 4)) Then
            '    device_info += device.Match(userAgent).Groups(0).Value
            'End If
            'If Not String.IsNullOrEmpty(device_info) Then
            '    Response.Redirect(Convert.ToString("Mobile.aspx?device_info=") & device_info)
            'End If


        Catch ex As Exception

        End Try
        Return result
    End Function

    Protected Sub imgbtn_Recharge_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Recharge.Click
        Try
            Session("Navigate") = "mobile"
            Response.Redirect("BOS_bbps_ps.aspx?type=mobile")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_PostPaid_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_PostPaid.Click
        Try
            Session("Navigate") = "Postpaid"
            Response.Redirect("BOS_BBPS_PS.aspx?type=Postpaid")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_DTH_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_DTH.Click
        Try
            Session("Navigate") = "dth"
            Response.Redirect("BOS_bbps_ps.aspx?type=dth")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Broadband_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Broadband.Click
        Try
            Session("Navigate") = "Broadband"
            Response.Redirect("BOS_BBPS_PS.aspx?type=Broadband")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Electricity_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Electricity.Click
        Try
            Session("Navigate") = "Electricity"
            Response.Redirect("BOS_BBPS_PS.aspx?type=Electricity")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Gas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Gas.Click
        Try
            Session("Navigate") = "LPG"
            Response.Redirect("BOS_BBPS_PS.aspx?type=LPG")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_WaterBill_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_WaterBill.Click
        Try
            Session("Navigate") = "Waterbill"
            Response.Redirect("BOS_BBPS_PS.aspx?type=Waterbill")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_LandLine_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_LandLine.Click
        Try
            Session("Navigate") = "Landline"
            Response.Redirect("BOS_BBPS_PS.aspx?type=Landline")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_WalletPay_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_WalletPay.Click
        Try
            Response.Redirect("BOS_TransferAmount_Form.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_AddWallet_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AddWallet.Click
        Try
            Response.Redirect("BOS_AddMoneyToWallet.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_MoneyTransfer_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MoneyTransfer.Click
        Try
            Response.Redirect("BOS_MoneyTransfer.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_PanCoupans_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_PanCoupans.Click
        Try
            Response.Redirect("BOS_PanCard.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_AEPS_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AEPS.Click
        Try
            Response.Redirect("Frm_AEPS_PS.aspx")
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub imgbtn_MyAccount_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MyAccount.Click
    '    Try
    '        Response.Redirect("BOS_WalletTransationReport.aspx")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub imgbtn_MyQRCode_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MyQRCode.Click
        Try
            Response.Redirect("My_Acc_Details.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Store_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Store.Click
        Try

            Response.Redirect("https://shopping.boscenter.in/")
            'lblInformation.Text = "Service is Inactive, Due to COVID-19"
            'ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_GSTHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_GSTHistory.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_AEPSHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AEPSHistory.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub imgbtn_MoveToBankHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MoveToBankHistory.Click
    '    Try
    '        lblInformation.Text = "Service is Inactive, Due to COVID-19"
    '        ModalPopupExtender3.Show()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub imgbtn_MoveToBank_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MoveToBank.Click
    '    Try
    '        lblInformation.Text = "Service is Inactive, Due to COVID-19"
    '        ModalPopupExtender3.Show()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub imgbtn_WalletTransfer_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_WalletTransfer.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Loan_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Loan.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub imgbtn_Insurance_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Insurance.Click
    '    Try
    '        lblInformation.Text = "Service is Inactive, Due to COVID-19"
    '        ModalPopupExtender3.Show()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub imgbtn_Event_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Event.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Holiday_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Holiday.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub imgbtn_Fastag_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Fastag.Click
    '    Try
    '        lblInformation.Text = "Service is Inactive, Due to COVID-19"
    '        ModalPopupExtender3.Show()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub imgbtn_Hotels_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Hotels.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Trains_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Trains.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Bus_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Bus.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Flights_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Flights.Click
        Try
            'lblInformation.Text = "Service is Inactive, Due to COVID-19"
            'ModalPopupExtender3.Show()
            Response.Redirect("https://www.bostravel.in/")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_GSTRegistration_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_GSTRegistration.Click
        Try
            Response.Redirect("BOS_MoneyTransfer.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_MicroATM_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MicroATM.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnAPIBalanceRefresh_N_Click(sender As Object, e As EventArgs) Handles btnAPIBalanceRefresh_N.Click
        Try
            Dim group1 As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            If group1 = "Distributor" Or group1 = "Master Distributor" Then

                show_MD_DIS_Bal()
                'lbl_API_Balance.Text = Math.Round(AgentBalance(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)))
            ElseIf group1 = "Admin" Then
                lbl_API_Balance.Text = API_Balance()
            ElseIf group1 = "Super Admin" Then
                showSuperAdminBal()
            Else
                Div_API.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAPIBalanceReport_Click(sender As Object, e As EventArgs) Handles btnAPIBalanceReport.Click
        Try

            Response.Redirect("API_Balance_Report.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_LoadWallet1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_LoadWallet1.Click
        Try

            Response.Redirect("BOS_MakePayment.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkWalletTransfer_Click(sender As Object, e As EventArgs) Handles lnkWalletTransfer.Click
        Try
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Master Distributor" Then
                If GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim = "CMP1165" Then
                    Response.Redirect("BOS_TransferAmount_Frm_N.aspx")
                Else
                    Response.Redirect("BOS_TransferAmount_Form.aspx")
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    '<-----------Ozzype UPIID Start------------------>
    Public Sub Fill_Details()
        Try
            qry = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "'"
            Dim ds As DataSet = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qry)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID")) Then
                            If Not ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString = "" Then
                                txt_upi_id.Text = ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString
                            Else
                                txt_upi_id.Text = ""
                            End If
                        Else
                            txt_upi_id.Text = ""
                        End If
                        txt_upi_id.ReadOnly = True
                        txt_upi_id.ForeColor = Drawing.Color.Blue
                        txt_upi_id.Font.Bold = True

                    End If

                End If

            End If



        Catch ex As Exception

        End Try
    End Sub

    '<-----------Ozzype UPIID End------------------>
    Protected Sub btnPayment_Details_Click(sender As Object, e As EventArgs) Handles btnPayment_Details.Click
        Try
            Response.Redirect("BOS_MakePayment.aspx")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            Response.Redirect("BOS_MakePayment_Report_SA.aspx")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Response.Redirect("BOS_API_Log_Report.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRaise_Click(sender As Object, e As EventArgs) Handles btnRaise.Click
        Try
            Response.Redirect("BOS_Raise_Request_Complaint.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPending_Click(sender As Object, e As EventArgs) Handles btnPending.Click
        Try
            Response.Redirect("BOS_PendingForm_Report_SA.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Response.Redirect("BOS_ClosedComplaint_Report_SA.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_EMI_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtn_EMI.Click
        Try
            Session("Navigate") = "EMI"
            Response.Redirect("BOS_BBPS_PS.aspx?type=EMI")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Cable_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtn_Cable.Click
        Try
            Session("Navigate") = "Cable"
            Response.Redirect("BOS_BBPS_PS.aspx?type=Cable")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Insurance_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtn_Insurance.Click
        Try
            Session("Navigate") = "Insurance"
            Response.Redirect("BOS_BBPS_PS.aspx?type=Insurance")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn_Fastag_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtn_Fastag.Click
        Try
            'Session("Navigate") = ""
            'Response.Redirect("BOS_BBPS_PS.aspx")
        Catch ex As Exception

        End Try
    End Sub

    'Dim VParmanentAddress As String = ""
    'Protected Sub txtPermanentAddress_TextChanged(sender As Object, e As EventArgs) Handles txtPermanentAddress.TextChanged
    '    Try
    '        If GV.parseString(txtPermanentAddress.Text) = "" Then

    '        Else
    '            VParmanentAddress = GV.parseString("txtPermanentAddress.Text")
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class