Public Class Admin_IdSettings
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Dim DS As New DataSet
    Dim ChangeSMSAPI As String
    Dim QryStr As String = ""
    Dim RechargeAPI_2_Status As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                'alter table autonumber
                'add RechargeAPI_Status nvarchar(250),
                'PANCardAPI_Status nvarchar(250),
                'MoneyTransferAPI_Status nvarchar(250)

                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber")
                If Not DS Is Nothing Then
                    If DS.Tables.Count > 0 Then
                        If DS.Tables(0).Rows.Count > 0 Then


                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("SubDistributorID")) Then
                                If Not DS.Tables(0).Rows(0).Item("SubDistributorID").ToString() = "" Then
                                    txtSubDistributorID.Text = GV.parseString(DS.Tables(0).Rows(0).Item("SubDistributorID").ToString())
                                Else
                                    txtSubDistributorID.Text = ""
                                End If
                            Else
                                txtSubDistributorID.Text = ""
                            End If


                            'If Not IsDBNull(DS.Tables(0).Rows(0).Item("Flight_APIPer")) Then
                            '    If Not DS.Tables(0).Rows(0).Item("Flight_APIPer").ToString() = "" Then
                            '        txtFlight_APIPer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Flight_APIPer").ToString())
                            '    Else
                            '        txtFlight_APIPer.Text = ""
                            '    End If
                            'Else
                            '    txtFlight_APIPer.Text = ""
                            'End If
                            'If Not IsDBNull(DS.Tables(0).Rows(0).Item("PAN_APIPer")) Then
                            '    If Not DS.Tables(0).Rows(0).Item("PAN_APIPer").ToString() = "" Then
                            '        txtPAN_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PAN_APIPer").ToString())
                            '    Else
                            '        txtPAN_API.Text = ""
                            '    End If
                            'Else
                            '    txtPAN_API.Text = ""
                            'End If

                            'If Not IsDBNull(DS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer")) Then
                            '    If Not DS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer").ToString() = "" Then
                            '        txtMoneyTransfer_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer").ToString())
                            '    Else
                            '        txtMoneyTransfer_API.Text = ""
                            '    End If
                            'Else
                            '    txtMoneyTransfer_API.Text = ""
                            'End If

                            'If Not IsDBNull(DS.Tables(0).Rows(0).Item("GST_APIPer")) Then
                            '    If Not DS.Tables(0).Rows(0).Item("GST_APIPer").ToString() = "" Then
                            '        txtGST_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("GST_APIPer").ToString())
                            '    Else
                            '        txtGST_API.Text = ""
                            '    End If
                            'Else
                            '    txtGST_API.Text = ""
                            'End If

                            'If Not IsDBNull(DS.Tables(0).Rows(0).Item("BusBooking_APIPer")) Then
                            '    If Not DS.Tables(0).Rows(0).Item("BusBooking_APIPer").ToString() = "" Then
                            '        txtBusBooking_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("BusBooking_APIPer").ToString())
                            '    Else
                            '        txtBusBooking_API.Text = ""
                            '    End If
                            'Else
                            '    txtBusBooking_API.Text = ""
                            'End If
                            'If Not IsDBNull(DS.Tables(0).Rows(0).Item("Rail_APIPer")) Then
                            '    If Not DS.Tables(0).Rows(0).Item("Rail_APIPer").ToString() = "" Then
                            '        txtRail_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Rail_APIPer").ToString())
                            '    Else
                            '        txtRail_API.Text = ""
                            '    End If
                            'Else
                            '    txtRail_API.Text = ""
                            'End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ServiceCharge")) Then
                                If Not DS.Tables(0).Rows(0).Item("ServiceCharge").ToString() = "" Then
                                    txtServiceCharge.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ServiceCharge").ToString())
                                Else
                                    txtServiceCharge.Text = ""
                                End If
                            Else
                                txtServiceCharge.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("MoneyTransferAPI_Status")) Then
                                If Not DS.Tables(0).Rows(0).Item("MoneyTransferAPI_Status").ToString() = "" Then
                                    ddlMoneyTransferAPI.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("MoneyTransferAPI_Status").ToString())
                                Else
                                    ddlMoneyTransferAPI.SelectedIndex = 0
                                End If
                            Else
                                ddlMoneyTransferAPI.SelectedIndex = 0
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PANCardAPI_Status")) Then
                                If Not DS.Tables(0).Rows(0).Item("PANCardAPI_Status").ToString() = "" Then
                                    ddlPANCardAPI.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("PANCardAPI_Status").ToString())
                                Else
                                    ddlPANCardAPI.SelectedIndex = 0
                                End If
                            Else
                                ddlPANCardAPI.SelectedIndex = 0
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AEPS_API_Status")) Then
                                If Not DS.Tables(0).Rows(0).Item("AEPS_API_Status").ToString() = "" Then
                                    ddlAEPSAPI.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("AEPS_API_Status").ToString())
                                Else
                                    ddlAEPSAPI.SelectedIndex = 0
                                End If
                            Else
                                ddlAEPSAPI.SelectedIndex = 0
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("RechargeAPI_Status")) Then
                                If Not DS.Tables(0).Rows(0).Item("RechargeAPI_Status").ToString() = "" Then
                                    ddlRechargeAPI.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("RechargeAPI_Status").ToString())
                                Else
                                    ddlRechargeAPI.SelectedIndex = 0
                                End If
                            Else
                                ddlRechargeAPI.SelectedIndex = 0
                            End If
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("RechargeAPI_2_Status")) Then
                                If Not DS.Tables(0).Rows(0).Item("RechargeAPI_2_Status").ToString() = "" Then
                                    ddlRechargeAPI_2.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("RechargeAPI_2_Status").ToString())
                                Else
                                    ddlRechargeAPI_2.SelectedIndex = 0
                                End If
                            Else
                                ddlRechargeAPI_2.SelectedIndex = 0
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("MoneyTransferAPI_2_Status")) Then
                                If Not DS.Tables(0).Rows(0).Item("MoneyTransferAPI_2_Status").ToString() = "" Then
                                    ddlMoneyTransferAPI_2_Status.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("MoneyTransferAPI_2_Status").ToString())
                                Else
                                    ddlMoneyTransferAPI_2_Status.SelectedIndex = 0
                                End If
                            Else
                                ddlMoneyTransferAPI_2_Status.SelectedIndex = 0
                            End If

                        End If
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim Recharge_APIPer, Flight_APIPer, PAN_APIPer, MoneyTransfer_APIPer, GST_APIPer, BusBooking_APIPer, Rail_APIPer, ServiceCharge As String
    Dim strcol As String
    Dim RechargeAPI_Status, MoneyTransferAPI_Status, MoneyTransferAPI_2_Status, PANCardAPI_Status, AEPSAPI_Status, Sub_DistributorID As String
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try

            RechargeAPI_Status = ""
            MoneyTransferAPI_Status = ""
            PANCardAPI_Status = ""
            AEPSAPI_Status = ""
            MoneyTransferAPI_2_Status = ""

            lblSettingInfoName.Text = ""
            lblSettingInfovalue.Text = ""
            Dim Name, Value As String
            Dim lnkbtn As LinkButton = DirectCast(sender, LinkButton)
            If Not lnkbtn Is Nothing Then
                If Not lnkbtn.CommandArgument.Trim = "" Then
                    lbllnkbtn.Text = lnkbtn.CommandArgument.Trim


                    If lbllnkbtn.Text.Trim.ToUpper = "Recharge_API".ToUpper Then
                        'If Not txtRechargeAPI.Text.Trim = "" Then
                        '    Recharge_APIPer = GV.parseString(txtRechargeAPI.Text.Trim)
                        'Else
                        '    Recharge_APIPer = "0"
                        'End If
                        'Name = "Recharge API"
                        'strcol = " Recharge_APIPer='" & Recharge_APIPer & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "Flight_API".ToUpper Then
                        'If Not txtFlight_APIPer.Text.Trim = "" Then
                        '    Flight_APIPer = GV.parseString(txtFlight_APIPer.Text.Trim)
                        'Else
                        '    Flight_APIPer = "0"
                        'End If
                        'Name = "Flight API"
                        'strcol = " Flight_APIPer='" & Flight_APIPer & "' "

                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "PAN_API".ToUpper Then
                        'If Not txtPAN_API.Text.Trim = "" Then
                        '    PAN_APIPer = GV.parseString(txtPAN_API.Text.Trim)
                        'Else
                        '    PAN_APIPer = ""
                        'End If
                        'Name = "PAN API"
                        'strcol = " PAN_APIPer='" & PAN_APIPer & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "MoneyTransfer_API".ToUpper Then
                        'If Not txtMoneyTransfer_API.Text.Trim = "" Then
                        '    MoneyTransfer_APIPer = GV.parseString(txtMoneyTransfer_API.Text.Trim)
                        'Else
                        '    MoneyTransfer_APIPer = ""
                        'End If
                        'Name = "MoneyTransfer API"
                        'strcol = " MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "GST_API".ToUpper Then
                        'If Not txtGST_API.Text.Trim = "" Then
                        '    GST_APIPer = GV.parseString(txtGST_API.Text.Trim)
                        'Else
                        '    GST_APIPer = ""
                        'End If
                        'Name = "GST API"
                        'strcol = " GST_APIPer='" & GST_APIPer & "' "

                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "BusBooking_API".ToUpper Then
                        'If Not txtBusBooking_API.Text.Trim = "" Then
                        '    BusBooking_APIPer = GV.FL.parseString(txtBusBooking_API.Text.Trim)
                        'Else
                        '    BusBooking_APIPer = ""
                        'End If

                        'Name = "BusBooking API"
                        'strcol = " BusBooking_APIPer='" & BusBooking_APIPer & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "Rail_API".ToUpper Then
                        'If Not txtRail_API.Text.Trim = "" Then
                        '    Rail_APIPer = GV.FL.parseString(txtRail_API.Text.Trim)
                        'Else
                        '    Rail_APIPer = ""
                        'End If
                        'Name = "Rail API"
                        'strcol = " Rail_APIPer='" & Rail_APIPer & "' "

                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "SERVICECHARGE".ToUpper Then
                        If Not txtServiceCharge.Text.Trim = "" Then
                            ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                        Else
                            ServiceCharge = "0"
                        End If
                        Name = "Service Charge"
                        strcol = " ServiceCharge='" & ServiceCharge & "' "

                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "RechargeAPI_Status".ToUpper Then
                        RechargeAPI_Status = ddlRechargeAPI.SelectedValue
                        Name = "RechargeAPI_Status"
                        strcol = " RechargeAPI_Status='" & RechargeAPI_Status & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "RechargeAPI_2_Status".ToUpper Then
                        RechargeAPI_2_Status = ddlRechargeAPI_2.SelectedValue
                        Name = "RechargeAPI_2_Status"
                        strcol = " RechargeAPI_2_Status='" & RechargeAPI_2_Status & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "MoneyTransferAPI_Status".ToUpper Then
                        MoneyTransferAPI_Status = ddlMoneyTransferAPI.SelectedValue
                        Name = "MoneyTransferAPI_Status"
                        strcol = " MoneyTransferAPI_Status='" & MoneyTransferAPI_Status & "' "

                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "MoneyTrans_2_Status".ToUpper Then
                        MoneyTransferAPI_2_Status = ddlMoneyTransferAPI_2_Status.SelectedValue
                        Name = "MoneyTransferAPI_2_Status"
                        strcol = " MoneyTransferAPI_2_Status='" & MoneyTransferAPI_2_Status & "' "

                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "PANCardAPI_Status".ToUpper Then
                        PANCardAPI_Status = ddlPANCardAPI.SelectedValue
                        Name = "PANCardAPI_Status"
                        strcol = " PANCardAPI_Status='" & PANCardAPI_Status & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "AEPSAPI_Status".ToUpper Then
                        AEPSAPI_Status = ddlAEPSAPI.SelectedValue
                        Name = "AEPSAPI_Status"
                        strcol = " AEPS_API_Status='" & AEPSAPI_Status & "' "
                    ElseIf lbllnkbtn.Text.Trim.ToUpper = "SubDistributorID".ToUpper Then
                        Sub_DistributorID = GV.parseString(txtSubDistributorID.Text.Trim)
                        Name = "Distributor ID"
                        strcol = " SubDistributorID='" & Sub_DistributorID & "' "
                    End If


                    Dim ABC() As String = strcol.Split("=")

                    If ABC.Length > 0 Then
                        '    Name = ABC(0)
                        Value = ABC(1).Replace("'", "")
                    End If
                    lblSettingNextDate.Text = ""
                    lblError.Text = ""
                    lblError.CssClass = ""
                    lblDialogMsg.Text = "Are You Sure You Want to Update ?"
                    lblSettingInfoName.Text = "Name : <b>" & Name & "</b>"
                    lblSettingInfovalue.Text = "Value : <b>" & Value & "</b>"

                    lblDialogMsg.CssClass = ""
                    btnok.Visible = True
                    btnok.Text = "Yes"
                    btnCancel.Text = "Cancel"
                    btnCancel.Attributes("style") = ""
                    ModalPopupExtender1.Show()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnok_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnok.Click
        Try

            If btnok.Text = "Yes" Then
                lblError.Text = ""
                lblError.CssClass = ""

                Dim strcol As String = ""
                
                If lbllnkbtn.Text.Trim.ToUpper = "Recharge_API".ToUpper Then
                    'If Not txtRechargeAPI.Text.Trim = "" Then
                    '    Recharge_APIPer = GV.parseString(txtRechargeAPI.Text.Trim)
                    'Else
                    '    Recharge_APIPer = "0"
                    'End If

                    'strcol = " Recharge_APIPer='" & Recharge_APIPer & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "Flight_API".ToUpper Then
                    'If Not txtFlight_APIPer.Text.Trim = "" Then
                    '    Flight_APIPer = GV.parseString(txtFlight_APIPer.Text.Trim)
                    'Else
                    '    Flight_APIPer = "0"
                    'End If

                    'strcol = " Flight_APIPer='" & Flight_APIPer & "' "

                ElseIf lbllnkbtn.Text.Trim.ToUpper = "PAN_API".ToUpper Then
                    'If Not txtPAN_API.Text.Trim = "" Then
                    '    PAN_APIPer = GV.parseString(txtPAN_API.Text.Trim)
                    'Else
                    '    PAN_APIPer = ""
                    'End If

                    'strcol = " PAN_APIPer='" & PAN_APIPer & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "MoneyTransfer_API".ToUpper Then
                    'If Not txtMoneyTransfer_API.Text.Trim = "" Then
                    '    MoneyTransfer_APIPer = GV.parseString(txtMoneyTransfer_API.Text.Trim)
                    'Else
                    '    MoneyTransfer_APIPer = ""
                    'End If

                    'strcol = " MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "GST_API".ToUpper Then
                    'If Not txtGST_API.Text.Trim = "" Then
                    '    GST_APIPer = GV.parseString(txtGST_API.Text.Trim)
                    'Else
                    '    GST_APIPer = ""
                    'End If

                    'strcol = " GST_APIPer='" & GST_APIPer & "' "

                ElseIf lbllnkbtn.Text.Trim.ToUpper = "BusBooking_API".ToUpper Then
                    'If Not txtBusBooking_API.Text.Trim = "" Then
                    '    BusBooking_APIPer = GV.FL.parseString(txtBusBooking_API.Text.Trim)
                    'Else
                    '    BusBooking_APIPer = ""
                    'End If


                    'strcol = " BusBooking_APIPer='" & BusBooking_APIPer & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "Rail_API".ToUpper Then
                    'If Not txtRail_API.Text.Trim = "" Then
                    '    Rail_APIPer = GV.FL.parseString(txtRail_API.Text.Trim)
                    'Else
                    '    Rail_APIPer = ""
                    'End If

                    'strcol = " Rail_APIPer='" & Rail_APIPer & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "SERVICECHARGE".ToUpper Then
                    If Not txtServiceCharge.Text.Trim = "" Then
                        ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                    Else
                        ServiceCharge = "0"
                    End If
                    strcol = " ServiceCharge='" & ServiceCharge & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "RechargeAPI_Status".ToUpper Then
                    RechargeAPI_Status = ddlRechargeAPI.SelectedValue
                    strcol = " RechargeAPI_Status='" & RechargeAPI_Status & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "RechargeAPI_2_Status".ToUpper Then
                    RechargeAPI_2_Status = ddlRechargeAPI_2.SelectedValue
                    strcol = " RechargeAPI_2_Status='" & RechargeAPI_2_Status & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "MoneyTransferAPI_Status".ToUpper Then
                    MoneyTransferAPI_Status = ddlMoneyTransferAPI.SelectedValue
                    strcol = " MoneyTransferAPI_Status='" & MoneyTransferAPI_Status & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "MoneyTrans_2_Status".ToUpper Then
                    MoneyTransferAPI_2_Status = ddlMoneyTransferAPI_2_Status.SelectedValue
                    strcol = " MoneyTransferAPI_2_Status='" & MoneyTransferAPI_2_Status & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "PANCardAPI_Status".ToUpper Then
                    PANCardAPI_Status = ddlPANCardAPI.SelectedValue
                    strcol = " PANCardAPI_Status='" & PANCardAPI_Status & "' "
                ElseIf lbllnkbtn.Text.Trim.ToUpper = "AEPSAPI_Status".ToUpper Then
                    AEPSAPI_Status = ddlAEPSAPI.SelectedValue
                    strcol = " AEPS_API_Status='" & AEPSAPI_Status & "' "

                ElseIf lbllnkbtn.Text.Trim.ToUpper = "SubDistributorID".ToUpper Then
                    Sub_DistributorID = GV.parseString(txtSubDistributorID.Text.Trim)
                    'Name = "Sub Distributor ID"
                    strcol = " SubDistributorID='" & Sub_DistributorID & "' "

                    'ElseIf lbllnkbtn.Text.Trim.ToUpper = "ChangeSMSAPI".ToUpper Then
                    '    If Not txtSMSAPI.Text.Trim = "" Then
                    '        ChangeSMSAPI = GV.parseString(txtSMSAPI.Text.Trim)
                    '    Else
                    '        ChangeSMSAPI = "0"
                    '    End If
                    '    Name = "SMS_API_URL"
                    '    strcol = " SMS_API_URL='" & ChangeSMSAPI & "' "
                End If


                QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber set " & strcol & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    lblDialogMsg.Text = "Settings Update Successfully"
                    lblDialogMsg.CssClass = "Successlabels"
                    btnok.Visible = False
                    btnCancel.Text = "Ok"
                    ModalPopupExtender1.Show()
                Else
                    lblDialogMsg.Text = "Settings Updation Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnok.Visible = False
                    btnCancel.Text = "Ok"
                    ModalPopupExtender1.Show()
                End If

            End If


        Catch ex As Exception

        End Try
    End Sub
    Public Function CheckingUpdate(ByVal RecCountQry As String, ByVal CloumnName As String, ByVal value As String) As Boolean
        Try
            If GV.FL.RecCount(RecCountQry) > 0 Then
                'Qry = "if not exists(select * from AutoNumber_Adminwhere convert(numeric(18,0)," & CloumnName & ")>='" & value & "') begin update AutoNumber_Admin set " & CloumnName & "='" & value & "'; Select 'True' as 'Result' end else Select 'False' as 'Result'"
                Dim Qry As String = "if not exists(select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber_Admin where convert(numeric(18,0)," & CloumnName & ")>'" & value & "') begin  Select 'True' as 'Result' end else Select 'False' as 'Result'"
                DS = New DataSet
                DS = GV.FL.OpenDsWithSelectQuery(Qry)
                If Not DS Is Nothing Then
                    If DS.Tables.Count > 0 Then
                        If DS.Tables(0).Rows.Count > 0 Then
                            Return DS.Tables(0).Rows(0).Item(0)
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If

            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


End Class