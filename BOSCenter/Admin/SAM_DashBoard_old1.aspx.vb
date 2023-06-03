Public Class SAM_DashBoard_old1
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    ' Dim todaydate As Date = CDate(Now.Date.ToString("MM/dd/yyyy"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim group1 As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            If Not IsPostBack Then
                divmobile.Attributes("style") = "display:none"

                If group1 = "Retailer" Or group1.Trim.ToUpper = "Customer".ToUpper Then

                    If fBrowserIsMobile() = False Then
                        'Case if mobile view is off

                        Div_API.Visible = True

                        div_MoneyTransfer.Visible = True
                        div_PanCard.Visible = True
                        div_Recharge.Visible = True

                        div_API_FlightBooking.Visible = True
                        div_API_HotelBooking.Visible = True
                        div_API_BusBooking.Visible = True
                        div_API_Fasttag.Visible = True
                        div_API_Insurance.Visible = True
                        div_API_AadhaarAtm.Visible = True
                        div_API_Loan.Visible = True
                        div_API_Eshopping.Visible = True
                        div_API_bosoutlet.Visible = True
                        div_API_walletTransfer.Visible = True
                        div_API_MiniATM.Visible = True
                        div_API_TrainBooking.Visible = True




                        div_API_Balance.Visible = False

                        chkAPIStatus()

                    Else
                        'Case if mobile view is on


                        div_MoneyTransfer.Visible = False
                        div_PanCard.Visible = False
                        div_Recharge.Visible = False

                        div_API_FlightBooking.Visible = False
                        div_API_HotelBooking.Visible = False
                        div_API_BusBooking.Visible = False
                        div_API_Fasttag.Visible = False
                        div_API_Insurance.Visible = False
                        div_API_AadhaarAtm.Visible = False
                        div_API_Loan.Visible = False
                        div_API_Eshopping.Visible = False
                        div_API_bosoutlet.Visible = False
                        div_API_walletTransfer.Visible = False
                        div_API_MiniATM.Visible = False
                        div_API_TrainBooking.Visible = False

                        div_API_Balance.Visible = False

                        If group1 = "Retailer" Then

                            tr_Retailer_1.Visible = True
                            tr_Retailer_2.Visible = False
                            tr_Retailer_3.Visible = False


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
                            tr_Customer_Mall.Visible = True

                            divmobile.Visible = True
                            'divmobile.Attributes.Remove("display")
                            ' divmobile.Attributes.Add("style", "display:")
                            divmobile.Attributes("style") = ""

                        End If

                        chkAPIStatus()

                    End If



                ElseIf group1 = "Distributor" Or group1 = "Master Distributor" Then

                    Div_API.Visible = False

                    div_MoneyTransfer.Visible = False
                    div_PanCard.Visible = False
                    div_Recharge.Visible = False
                    div_API_Balance.Visible = False


                    div_API_FlightBooking.Visible = False
                    div_API_HotelBooking.Visible = False
                    div_API_BusBooking.Visible = False
                    div_API_Fasttag.Visible = False
                    div_API_Insurance.Visible = False
                    div_API_AadhaarAtm.Visible = False
                    div_API_Loan.Visible = False
                    div_API_Eshopping.Visible = False
                    div_API_bosoutlet.Visible = False
                    div_API_walletTransfer.Visible = False
                    div_API_MiniATM.Visible = False
                    div_API_TrainBooking.Visible = False

                ElseIf group1 = "Admin" Or group1 = "Super Admin" Then
                    Div_API.Visible = True
                    div_MoneyTransfer.Visible = False
                    div_PanCard.Visible = False
                    div_Recharge.Visible = False

                    div_API_FlightBooking.Visible = False
                    div_API_HotelBooking.Visible = False
                    div_API_BusBooking.Visible = False
                    div_API_Fasttag.Visible = False
                    div_API_Insurance.Visible = False
                    div_API_AadhaarAtm.Visible = False
                    div_API_Loan.Visible = False
                    div_API_Eshopping.Visible = False
                    div_API_bosoutlet.Visible = False
                    div_API_walletTransfer.Visible = False
                    div_API_MiniATM.Visible = False
                    div_API_TrainBooking.Visible = False



                    If group1 = "Super Admin" Then
                        lbl_API_Balance.Visible = False
                        btnAPIBalanceRefresh_N.Visible = False

                        div_API_Balance.Visible = True
                    Else
                        lbl_API_Balance.Text = API_Balance()
                        div_API_Balance.Visible = True
                    End If

                Else

                    Div_API.Visible = True

                    div_MoneyTransfer.Visible = False
                    div_PanCard.Visible = False
                    div_Recharge.Visible = False

                    div_API_FlightBooking.Visible = False
                    div_API_HotelBooking.Visible = False
                    div_API_BusBooking.Visible = False
                    div_API_Fasttag.Visible = False
                    div_API_Insurance.Visible = False
                    div_API_AadhaarAtm.Visible = False
                    div_API_Loan.Visible = False
                    div_API_Eshopping.Visible = False
                    div_API_bosoutlet.Visible = False
                    div_API_walletTransfer.Visible = False
                    div_API_MiniATM.Visible = False
                    div_API_TrainBooking.Visible = False

                    lbl_API_Balance.Visible = False
                    btnAPIBalanceRefresh_N.Visible = False

                    div_API_Balance.Visible = True

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


            'Div_API.Visible = True
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub



    Public Sub chkAPIStatus()
        Try
            '///// Start Check API  STATUS System Settings 

            Dim PANCardAPI_Status As String = ""
            PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                btnPanCard.Visible = False
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            PANCardAPI_Status = ""
            PANCardAPI_Status = GV.FL.AddInVar("PANCardAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not PANCardAPI_Status.Trim.ToUpper = "ACTIVE" Then
                btnPanCard.Visible = False
            End If

            '///// End Check API  STATUS Retailer Level  Settings 



            '///// Start Check API  STATUS System Settings 

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                btnMoneyTransfer.Visible = False

            End If

            '///// End Check API  STATUS Retailer Level Settings 

            '///// Start Check API  STATUS System Settings 
            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                btnMoneyTransfer.Visible = False
            End If

            '///// End Check API  STATUS Retailer Level  Settings 

            '///// Start Check API  STATUS System Settings 

            Dim RechargeAPI_Status As String = ""
            RechargeAPI_Status = GV.FL.AddInVar("RechargeAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                btnRecharge.Visible = False
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            '///// Start Check API  STATUS System Settings 
            RechargeAPI_Status = ""
            RechargeAPI_Status = GV.FL.AddInVar("RechargeAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                btnRecharge.Visible = False
            End If

            '///// End Check API  STATUS Retailer Level  Settings 
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    'Protected Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
    '    Try
    '        'lbl_API_Balance.Text = GV.API_Balance()
    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

    '    End Try
    'End Sub

    Protected Sub btnAPIBalanceRefresh_N_Click(sender As Object, e As EventArgs) Handles btnAPIBalanceRefresh_N.Click
        Try
            lbl_API_Balance.Text = API_Balance()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnAPIBalanceReport_Click(sender As Object, e As EventArgs) Handles btnAPIBalanceReport.Click
        Try
            Response.Redirect("API_Balance_Report.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return balanceAmt
        End Try
        Return balanceAmt
    End Function
    Dim MobileCheck As Regex = New Regex("(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od|ad)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase Or RegexOptions.Multiline Or RegexOptions.Compiled)
    Dim MobileVersionCheck As Regex = New Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase Or RegexOptions.Multiline Or RegexOptions.Compiled)


    'Dim OS As New Regex("(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
    'Dim device As New Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase Or RegexOptions.Multiline)

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
        Return result
    End Function

    Protected Sub imgbtn_Recharge_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Recharge.Click
        Try
            Session("Navigate") = "mobile"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_PostPaid_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_PostPaid.Click
        Try
            Session("Navigate") = "Postpaid"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_DTH_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_DTH.Click
        Try
            Session("Navigate") = "dth"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Broadband_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Broadband.Click
        Try
            Session("Navigate") = "Broadband"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Electricity_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Electricity.Click
        Try
            Session("Navigate") = "Electricity"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Gas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Gas.Click
        Try
            Session("Navigate") = "gas"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_WaterBill_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_WaterBill.Click
        Try
            Session("Navigate") = "waterbill"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_LandLine_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_LandLine.Click
        Try
            Session("Navigate") = "Landline"
            Response.Redirect("BOS_RechargeAPI.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_WalletPay_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_WalletPay.Click
        Try
            Response.Redirect("BOS_TransferAmount_Form.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_AddWallet_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AddWallet.Click
        Try
            Response.Redirect("BOS_AddMoneyToWallet.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_MoneyTransfer_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MoneyTransfer.Click
        Try
            Response.Redirect("BOS_MoneyTransfer.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_PanCoupans_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_PanCoupans.Click
        Try
            Response.Redirect("BOS_PanCard.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_AEPS_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AEPS.Click
        Try
            Response.Redirect("BOS_AEPS.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_MyAccount_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MyAccount.Click
        Try
            Response.Redirect("BOS_WalletTransationReport.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Store_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Store.Click
        Try

            Response.Redirect("https://shopping.boscenter.in/")
            'lblInformation.Text = "Service is Inactive, Due to COVID-19"
            'ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_GSTHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_GSTHistory.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_AEPSHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AEPSHistory.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_MoveToBankHistory_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MoveToBankHistory.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_MoveToBank_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MoveToBank.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_WalletTransfer_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_WalletTransfer.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Loan_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Loan.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Insurance_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Insurance.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Event_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Event.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Holiday_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Holiday.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Fastag_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Fastag.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Hotels_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Hotels.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Trains_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Trains.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Bus_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Bus.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_Flights_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Flights.Click
        Try
            'lblInformation.Text = "Service is Inactive, Due to COVID-19"
            'ModalPopupExtender3.Show()
            Response.Redirect("https://www.bostravel.in/")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_GSTRegistration_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_GSTRegistration.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub imgbtn_MicroATM_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_MicroATM.Click
        Try
            lblInformation.Text = "Service is Inactive, Due to COVID-19"
            ModalPopupExtender3.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class