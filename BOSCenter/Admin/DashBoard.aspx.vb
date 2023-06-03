Public Class DashBoard
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    Dim TodayDate As Date = CDate(Now.Date.ToString("MM/dd/yyyy"))
  
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    Dim RegDetais As DataSet = New DataSet

        '    Dim query As String = "select count(Reg_Type) as TodaysAMCReg from CRM_Warrenty_AMC_Reg where Reg_Type='AMC Registration' and Reg_Date='" & TodayDate & "' "
        '    query = query & "select count(Reg_Type) as TotalAMCReg from CRM_Warrenty_AMC_Reg where Reg_Type='AMC Registration' and Reg_Date<='" & TodayDate & "' "

        '    query = query & "select count(Reg_Type) as TodaysWarrantyReg from CRM_Warrenty_AMC_Reg where Reg_Type='Warranty Registration' and Reg_Date='" & TodayDate & "'"
        '    query = query & "select count(Reg_Type) as TotalsWarrantyReg from CRM_Warrenty_AMC_Reg where Reg_Type='Warranty Registration' and Reg_Date<='" & TodayDate & "'"

        '    query = query & "select count(CallDate) as TotalServiceCall from CRM_Call_Registration where CallDate<= '" & TodayDate & "'"
        '    query = query & "select count(CallDate) as TodayServiceCall from CRM_Call_Registration where CallDate = '" & TodayDate & "'"


        '    RegDetais = GV.FL.OpenDsWithSelectQuery(query)
        '    If Not RegDetais Is Nothing Then
        '        If RegDetais.Tables.Count > 0 Then
        '            If RegDetais.Tables(0).Rows.Count > 0 Then
        '                listViewRegDetails.DataSource = RegDetais
        '                listViewRegDetails.DataBind()

        '                For i As Integer = 0 To listViewRegDetails.Items.Count - 1
        '                    Dim totalAMC As LinkButton = DirectCast(listViewRegDetails.Items(i).FindControl("lnkbtnTotalAMC"), LinkButton)
        '                    Dim todayAMC As LinkButton = DirectCast(listViewRegDetails.Items(i).FindControl("lnkbtnTodayAMC"), LinkButton)

        '                    Dim totalWarranty As LinkButton = DirectCast(listViewRegDetails.Items(i).FindControl("lnkbtnTotalWarranty"), LinkButton)
        '                    Dim todayWarranty As LinkButton = DirectCast(listViewRegDetails.Items(i).FindControl("lnkbtnTodayWarranty"), LinkButton)

        '                    Dim totalServiseCall As LinkButton = DirectCast(listViewRegDetails.Items(i).FindControl("lnkbtnTotalserviceCall"), LinkButton)
        '                    Dim todayServiceCall As LinkButton = DirectCast(listViewRegDetails.Items(i).FindControl("lnkbtnTodayserviceCall"), LinkButton)


        '                    If Not IsDBNull(RegDetais.Tables(1).Rows(i).Item("TotalAMCReg")) Then
        '                        If Not RegDetais.Tables(1).Rows(i).Item("TotalAMCReg").ToString() = "" Then
        '                            totalAMC.Text = RegDetais.Tables(1).Rows(i).Item("TotalAMCReg")

        '                        Else
        '                            totalAMC.Text = ""
        '                        End If
        '                    Else
        '                        totalAMC.Text = ""
        '                    End If

        '                    If Not IsDBNull(RegDetais.Tables(0).Rows(i).Item("TodaysAMCReg")) Then
        '                        If Not RegDetais.Tables(0).Rows(i).Item("TodaysAMCReg").ToString() = "" Then
        '                            todayAMC.Text = RegDetais.Tables(0).Rows(i).Item("TodaysAMCReg")

        '                        Else
        '                            todayAMC.Text = ""
        '                        End If
        '                    Else
        '                        todayAMC.Text = ""
        '                    End If

        '                    If Not IsDBNull(RegDetais.Tables(3).Rows(i).Item("TotalsWarrantyReg")) Then
        '                        If Not RegDetais.Tables(3).Rows(i).Item("TotalsWarrantyReg").ToString() = "" Then
        '                            totalWarranty.Text = RegDetais.Tables(3).Rows(i).Item("TotalsWarrantyReg")

        '                        Else
        '                            totalWarranty.Text = ""
        '                        End If
        '                    Else
        '                        totalWarranty.Text = ""
        '                    End If

        '                    If Not IsDBNull(RegDetais.Tables(2).Rows(i).Item("TodaysWarrantyReg")) Then
        '                        If Not RegDetais.Tables(2).Rows(i).Item("TodaysWarrantyReg").ToString() = "" Then
        '                            todayWarranty.Text = RegDetais.Tables(2).Rows(i).Item("TodaysWarrantyReg")

        '                        Else
        '                            todayWarranty.Text = ""
        '                        End If
        '                    Else
        '                        todayWarranty.Text = ""
        '                    End If

        '                    If Not IsDBNull(RegDetais.Tables(4).Rows(i).Item("TotalServiceCall")) Then
        '                        If Not RegDetais.Tables(4).Rows(i).Item("TotalServiceCall").ToString() = "" Then
        '                            totalServiseCall.Text = RegDetais.Tables(4).Rows(i).Item("TotalServiceCall")

        '                        Else
        '                            totalServiseCall.Text = ""
        '                        End If
        '                    Else
        '                        totalServiseCall.Text = ""
        '                    End If

        '                    If Not IsDBNull(RegDetais.Tables(5).Rows(i).Item("TodayServiceCall")) Then
        '                        If Not RegDetais.Tables(5).Rows(i).Item("TodayServiceCall").ToString() = "" Then
        '                            todayServiceCall.Text = RegDetais.Tables(5).Rows(i).Item("TodayServiceCall")

        '                        Else
        '                            todayServiceCall.Text = ""
        '                        End If
        '                    Else
        '                        todayServiceCall.Text = ""
        '                    End If
        '                Next
        '            End If
        '        End If
        '    End If


        '    '==============================&&&&&&&&&&&&&&&&===================================================

        '    Dim ExpiryDetails As DataSet = New DataSet

        '    Dim qry As String = "select count(WarrantyExpiredate) as todayExpireWarranty from CRM_Warrenty_AMC_Reg where Reg_Type='Warranty Registration' and  WarrantyExpiredate= '" & TodayDate & "'"
        '    qry = qry & "select count(WarrantyExpiredate) as LastdayExpireWarranty from CRM_Warrenty_AMC_Reg where Reg_Type='Warranty Registration' and WarrantyExpiredate= '" & TodayDate.AddDays(-1) & "'"
        '    qry = qry & "select count(WarrantyExpiredate) as NextdayExpireWarranty from CRM_Warrenty_AMC_Reg where Reg_Type='Warranty Registration' and WarrantyExpiredate= '" & TodayDate.AddDays(+1) & "'"
        '    qry = qry & "select count(WarrantyExpiredate) as NextTendayExpireWarranty from CRM_Warrenty_AMC_Reg where Reg_Type='Warranty Registration' and WarrantyExpiredate between '" & TodayDate.AddDays(+1) & "' and '" & TodayDate.AddDays(+10) & "'"

        '    qry = qry & "SELECT COUNT(Billing_Date_GracePeriod) AS todayExpireAMC FROM CRM_Warrenty_AMC_Reg WHERE Reg_Type='AMC Registration' AND Billing_Date_GracePeriod='" & TodayDate & "'"
        '    qry = qry & "SELECT COUNT(Billing_Date_GracePeriod) AS LastdayExpireAMC FROM CRM_Warrenty_AMC_Reg WHERE Reg_Type='AMC Registration' AND Billing_Date_GracePeriod='" & TodayDate.AddDays(-1) & "'"
        '    qry = qry & "SELECT COUNT(Billing_Date_GracePeriod) AS LastdayExpireAMC FROM CRM_Warrenty_AMC_Reg WHERE Reg_Type='AMC Registration' AND Billing_Date_GracePeriod='" & TodayDate.AddDays(+1) & "'"
        '    qry = qry & "SELECT COUNT(Billing_Date_GracePeriod) AS LastdayExpireAMC FROM CRM_Warrenty_AMC_Reg WHERE Reg_Type='AMC Registration' AND Billing_Date_GracePeriod between '" & TodayDate.AddDays(+1) & "' and '" & TodayDate.AddDays(+10) & "'"

        '    ExpiryDetails = GV.FL.OpenDsWithSelectQuery(qry)
        '    listviewExpiryDetails.DataSource = ExpiryDetails
        '    listviewExpiryDetails.DataBind()

        '    If Not ExpiryDetails Is Nothing Then
        '        If ExpiryDetails.Tables.Count > 0 Then
        '            If ExpiryDetails.Tables(0).Rows.Count > 0 Then

        '                For i As Integer = 0 To listviewExpiryDetails.Items.Count - 1

        '                    Dim todayExpireAMC As LinkButton = DirectCast(listviewExpiryDetails.Items(i).FindControl("lnkbtnTodayExpiryAMC"), LinkButton)
        '                    Dim LstDayExpireAMC As LinkButton = DirectCast(listviewExpiryDetails.Items(i).FindControl("lnkbtnLastDayExpiryAMC"), LinkButton)
        '                    Dim NextdayExpireAMC As LinkButton = DirectCast(listviewExpiryDetails.Items(i).FindControl("lnkbtnNextDayExpiryAMC"), LinkButton)
        '                    Dim NextTenExpireAMC As LinkButton = DirectCast(listviewExpiryDetails.Items(i).FindControl("lnkbtnNextTenDayExpiryAMC"), LinkButton)


        '                Next

        '            End If
        '        End If
        '    End If
        '        Catch ex As Exception 
        'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        'End Try
    End Sub

End Class