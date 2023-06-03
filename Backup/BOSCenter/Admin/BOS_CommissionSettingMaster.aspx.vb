
Public Class BOS_CommissionSettingMaster
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VEmployeeType, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()
            FillData()
        Catch ex As Exception
        End Try
    End Sub
    Dim Recharge_APIPer, Flight_APIPer, PAN_APIPer, MoneyTransfer_APIPer, GST_APIPer, BusBooking_APIPer, Rail_APIPer As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            
            Dim isErrorFound As Boolean = False
            Dim isFocusApplied As Boolean = False
            lblError.Text = ""
            lblError.CssClass = ""

            If Not txtRechargeAPIAllow.Text.Trim = "" Then
                If CDec(txtRechargeAPI.Text.Trim) < CDec(txtRechargeAPIAllow.Text) Then
                    txtRechargeAPIAllow.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtRechargeAPIAllow.Focus()
                        isFocusApplied = True
                    End If
                Else
                    Recharge_APIPer = GV.parseString(txtRechargeAPIAllow.Text.Trim)
                    txtRechargeAPIAllow.CssClass = "form-control"
                End If

            Else
                txtRechargeAPIAllow.CssClass = "form-control"
                Recharge_APIPer = "0"
            End If
          
            If Not txtFlight_APIPerAllow.Text.Trim = "" Then
                If CDec(txtFlight_APIPer.Text.Trim) < CDec(txtFlight_APIPerAllow.Text) Then
                    txtFlight_APIPerAllow.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtFlight_APIPerAllow.Focus()
                        isFocusApplied = True
                    End If
                Else
                    Flight_APIPer = GV.parseString(txtFlight_APIPerAllow.Text.Trim)
                    txtFlight_APIPerAllow.CssClass = "form-control"
                End If

            Else
                txtFlight_APIPerAllow.CssClass = "form-control"
                Flight_APIPer = "0"
            End If
         
            If Not txtPAN_APIAllow.Text.Trim = "" Then
                If CDec(txtPAN_API.Text.Trim) < CDec(txtPAN_APIAllow.Text) Then
                    txtPAN_APIAllow.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtPAN_APIAllow.Focus()
                        isFocusApplied = True
                    End If
                Else
                    PAN_APIPer = GV.parseString(txtPAN_APIAllow.Text.Trim)
                    txtPAN_APIAllow.CssClass = "form-control"
                End If

            Else
                txtPAN_APIAllow.CssClass = "form-control"
                PAN_APIPer = "0"
            End If
          
            If Not txtMoneyTransfer_APIAllow.Text.Trim = "" Then
                If CDec(txtMoneyTransfer_API.Text.Trim) < CDec(txtMoneyTransfer_APIAllow.Text) Then
                    txtMoneyTransfer_APIAllow.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtMoneyTransfer_APIAllow.Focus()
                        isFocusApplied = True
                    End If
                Else
                    MoneyTransfer_APIPer = GV.parseString(txtMoneyTransfer_APIAllow.Text.Trim)
                    txtMoneyTransfer_APIAllow.CssClass = "form-control"
                End If

            Else
                txtMoneyTransfer_APIAllow.CssClass = "form-control"
                MoneyTransfer_APIPer = "0"
            End If
        
            If Not txtGST_APIAllow.Text.Trim = "" Then
                If CDec(txtGST_API.Text.Trim) < CDec(txtGST_APIAllow.Text) Then
                    txtGST_APIAllow.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtGST_APIAllow.Focus()
                        isFocusApplied = True
                    End If
                Else
                    GST_APIPer = GV.parseString(txtGST_APIAllow.Text.Trim)
                    txtGST_APIAllow.CssClass = "form-control"
                End If

            Else
                txtGST_APIAllow.CssClass = "form-control"
                GST_APIPer = "0"
            End If
         

            If Not txtBusBooking_APIAllow.Text.Trim = "" Then
                If CDec(txtBusBooking_API.Text.Trim) < CDec(txtBusBooking_APIAllow.Text) Then
                    txtBusBooking_APIAllow.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtBusBooking_APIAllow.Focus()
                        isFocusApplied = True
                    End If
                Else
                    BusBooking_APIPer = GV.FL.parseString(txtBusBooking_APIAllow.Text.Trim)
                    txtBusBooking_APIAllow.CssClass = "form-control"
                End If

            Else
                txtBusBooking_APIAllow.CssClass = "form-control"
                BusBooking_APIPer = "0"
            End If

            If Not txtRail_APIAllow.Text.Trim = "" Then
                If CDec(txtRail_API.Text.Trim) < CDec(txtRail_APIAllow.Text) Then
                    txtRail_APIAllow.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtRail_APIAllow.Focus()
                        isFocusApplied = True
                    End If
                Else
                    Rail_APIPer = GV.FL.parseString(txtRail_APIAllow.Text.Trim)
                    txtRail_APIAllow.CssClass = "form-control"
                End If

            Else
                txtRail_APIAllow.CssClass = "form-control"
                Rail_APIPer = "0"
            End If
            If isErrorFound = True Then
                lblError.Text = "Allow Commission Not Greater Than Getting Commission."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"
            If lblSessionFlag.Text = 0 Then
                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where RegistrationID='" & Registraionid & "'and AgentType='" & AgentType & "'  ") > 0 Then 'Change where condition according to Criteria 
                    QryStr = " Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set Recharge_APIPer='" & Recharge_APIPer & "',Flight_APIPer='" & Flight_APIPer & "',PAN_APIPer='" & PAN_APIPer & "',MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "',GST_APIPer='" & GST_APIPer & "',BusBooking_APIPer='" & BusBooking_APIPer & "',Rail_APIPer='" & Rail_APIPer & "',updatedBy='" & VUpdatedBy & "',UpdatedOn=" & VUpdatedOn & " where AgentType='" & AgentType & "' and RegistrationID='" & Registraionid & "' ; "
                Else
                    QryStr = " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs (RegistrationID,AgentType,Recharge_APIPer,Flight_APIPer,PAN_APIPer,MoneyTransfer_APIPer,GST_APIPer,BusBooking_APIPer,Rail_APIPer,UpdatedBy,UpdatedOn) values( '" & Registraionid & "','" & AgentType & "','" & Recharge_APIPer & "','" & Flight_APIPer & "','" & PAN_APIPer & "','" & MoneyTransfer_APIPer & "','" & GST_APIPer & "','" & BusBooking_APIPer & "','" & Rail_APIPer & "','" & VUpdatedBy & "'," & VUpdatedOn & " ) ; "
                End If
                If AgentType.Trim.ToUpper = "Admin".Trim.ToUpper Or AgentType.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set Recharge_APIPer='" & Recharge_APIPer & "' where Recharge_APIPer>" & Recharge_APIPer & " ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set Flight_APIPer='" & Flight_APIPer & "' where Flight_APIPer>" & Flight_APIPer & " ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set PAN_APIPer='" & PAN_APIPer & "' where PAN_APIPer>" & PAN_APIPer & " ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "' where MoneyTransfer_APIPer>" & MoneyTransfer_APIPer & " ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set GST_APIPer='" & GST_APIPer & "' where GST_APIPer>" & GST_APIPer & " ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set BusBooking_APIPer='" & BusBooking_APIPer & "' where BusBooking_APIPer>" & BusBooking_APIPer & " ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set Rail_APIPer='" & Rail_APIPer & "' where Rail_APIPer>" & Rail_APIPer & " ;"
                ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set Recharge_APIPer='" & Recharge_APIPer & "' where Recharge_APIPer>" & Recharge_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ; "
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set Flight_APIPer='" & Flight_APIPer & "' where Flight_APIPer>" & Flight_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set PAN_APIPer='" & PAN_APIPer & "' where PAN_APIPer>" & PAN_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "' where MoneyTransfer_APIPer>" & MoneyTransfer_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set GST_APIPer='" & GST_APIPer & "' where GST_APIPer>" & GST_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set BusBooking_APIPer='" & BusBooking_APIPer & "' where BusBooking_APIPer>" & BusBooking_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
                    QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs set Rail_APIPer='" & Rail_APIPer & "' where Rail_APIPer>" & Rail_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"

                End If


                If GV.FL.DMLQueries(QryStr) = True Then
                    Clear()
                    FillData()
                    lblError.Text = "Commission Updated Successfully."
                    lblError.CssClass = "Successlabels"

                Else
                    lblError.Text = "Sorry !! Process Can't be Completed."
                    lblError.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            VEmployeeType = ""
            VUpdatedBy = ""
            VUpdatedOn = ""

            txtRail_APIAllow.CssClass = "form-control"
            txtFlight_APIPerAllow.CssClass = "form-control"
            txtPAN_APIAllow.CssClass = "form-control"
            txtMoneyTransfer_APIAllow.CssClass = "form-control"
            txtGST_APIAllow.CssClass = "form-control"
            txtRechargeAPIAllow.CssClass = "form-control"
            txtBusBooking_APIAllow.CssClass = "form-control"
            txtRechargeAPIAllow.CssClass = "form-control"

            lblSessionFlag.Text = 0
            btnSave.Text = "Update"
            lblError.Text = ""
            btnSave.Enabled = True

            lblError.CssClass = ""
            '  txtEmployeeTypeName.Focus()
            lblUpadate.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                FillData()
                lblSessionFlag.Text = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    
    Public Sub FillData()
        Try
            txtBusBooking_APIAllow.Visible = True
            txtRechargeAPIAllow.Visible = True
            txtFlight_APIPerAllow.Visible = True
            txtMoneyTransfer_APIAllow.Visible = True
            txtGST_APIAllow.Visible = True
            txtPAN_APIAllow.Visible = True
            txtRail_APIAllow.Visible = True
            btnClear.Visible = True
            btnSave.Visible = True
            lblAllowComm.Visible = True
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Master Distributor" Then
                lblCommssionHeading.Text = "Set Commission For Distributor"
                DS = New DataSet
                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where RegistrationId in (Select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & Registraionid & "') ")
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Then
                lblCommssionHeading.Text = "Set Commission For Retailer"
                DS = New DataSet
                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where RegistrationId in (Select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & Registraionid & "') ")
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Retailer" Then
                lblCommssionHeading.Text = "Commission Chart"
                DS = New DataSet
                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where RegistrationId in (Select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & Registraionid & "') ")
                txtBusBooking_APIAllow.Visible = False
                txtRechargeAPIAllow.Visible = False
                txtFlight_APIPerAllow.Visible = False
                txtMoneyTransfer_APIAllow.Visible = False
                txtGST_APIAllow.Visible = False
                txtPAN_APIAllow.Visible = False
                txtRail_APIAllow.Visible = False
                btnClear.Visible = False
                btnSave.Visible = False
                lblAllowComm.Visible = False
            Else
                lblCommssionHeading.Text = "Set Commission For Master Distributor"
                DS = New DataSet
                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber")
            End If

            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Recharge_APIPer")) Then
                            If Not DS.Tables(0).Rows(0).Item("Recharge_APIPer").ToString() = "" Then
                                txtRechargeAPI.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Recharge_APIPer").ToString())
                            Else
                                txtRechargeAPI.Text = "0"
                            End If
                        Else
                            txtRechargeAPI.Text = "0"
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Flight_APIPer")) Then
                            If Not DS.Tables(0).Rows(0).Item("Flight_APIPer").ToString() = "" Then
                                txtFlight_APIPer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Flight_APIPer").ToString())
                            Else
                                txtFlight_APIPer.Text = "0"
                            End If
                        Else
                            txtFlight_APIPer.Text = "0"
                        End If
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("PAN_APIPer")) Then
                            If Not DS.Tables(0).Rows(0).Item("PAN_APIPer").ToString() = "" Then
                                txtPAN_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PAN_APIPer").ToString())
                            Else
                                txtPAN_API.Text = "0"
                            End If
                        Else
                            txtPAN_API.Text = "0"
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer")) Then
                            If Not DS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer").ToString() = "" Then
                                txtMoneyTransfer_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer").ToString())
                            Else
                                txtMoneyTransfer_API.Text = "0"
                            End If
                        Else
                            txtMoneyTransfer_API.Text = "0"
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("GST_APIPer")) Then
                            If Not DS.Tables(0).Rows(0).Item("GST_APIPer").ToString() = "" Then
                                txtGST_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("GST_APIPer").ToString())
                            Else
                                txtGST_API.Text = "0"
                            End If
                        Else
                            txtGST_API.Text = "0"
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("BusBooking_APIPer")) Then
                            If Not DS.Tables(0).Rows(0).Item("BusBooking_APIPer").ToString() = "" Then
                                txtBusBooking_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("BusBooking_APIPer").ToString())
                            Else
                                txtBusBooking_API.Text = "0"
                            End If
                        Else
                            txtBusBooking_API.Text = "0"
                        End If
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Rail_APIPer")) Then
                            If Not DS.Tables(0).Rows(0).Item("Rail_APIPer").ToString() = "" Then
                                txtRail_API.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Rail_APIPer").ToString())
                            Else
                                txtRail_API.Text = "0"
                            End If
                        Else
                            txtRail_API.Text = "0"
                        End If

                    End If
                End If
            End If
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where RegistrationID='" & Registraionid & "' and AgentType='" & AgentType & "'  ") > 0 Then 'Change where condition according to Criteria 

                Dim CommDS As DataSet = New DataSet

                CommDS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where  RegistrationID='" & Registraionid & "' and AgentType='" & AgentType & "'")

                If Not CommDS Is Nothing Then
                    If CommDS.Tables.Count > 0 Then
                        If CommDS.Tables(0).Rows.Count > 0 Then

                            If Not IsDBNull(CommDS.Tables(0).Rows(0).Item("Recharge_APIPer")) Then
                                If Not CommDS.Tables(0).Rows(0).Item("Recharge_APIPer").ToString() = "" Then
                                    txtRechargeAPIAllow.Text = GV.parseString(CommDS.Tables(0).Rows(0).Item("Recharge_APIPer").ToString())
                                Else
                                    txtRechargeAPIAllow.Text = ""
                                End If
                            Else
                                txtRechargeAPIAllow.Text = ""
                            End If


                            If Not IsDBNull(CommDS.Tables(0).Rows(0).Item("Flight_APIPer")) Then
                                If Not CommDS.Tables(0).Rows(0).Item("Flight_APIPer").ToString() = "" Then
                                    txtFlight_APIPerAllow.Text = GV.parseString(CommDS.Tables(0).Rows(0).Item("Flight_APIPer").ToString())
                                Else
                                    txtFlight_APIPerAllow.Text = ""
                                End If
                            Else
                                txtFlight_APIPerAllow.Text = ""
                            End If
                            If Not IsDBNull(CommDS.Tables(0).Rows(0).Item("PAN_APIPer")) Then
                                If Not CommDS.Tables(0).Rows(0).Item("PAN_APIPer").ToString() = "" Then
                                    txtPAN_APIAllow.Text = GV.parseString(CommDS.Tables(0).Rows(0).Item("PAN_APIPer").ToString())
                                Else
                                    txtPAN_APIAllow.Text = ""
                                End If
                            Else
                                txtPAN_APIAllow.Text = ""
                            End If

                            If Not IsDBNull(CommDS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer")) Then
                                If Not CommDS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer").ToString() = "" Then
                                    txtMoneyTransfer_APIAllow.Text = GV.parseString(CommDS.Tables(0).Rows(0).Item("MoneyTransfer_APIPer").ToString())
                                Else
                                    txtMoneyTransfer_APIAllow.Text = ""
                                End If
                            Else
                                txtMoneyTransfer_APIAllow.Text = ""
                            End If

                            If Not IsDBNull(CommDS.Tables(0).Rows(0).Item("GST_APIPer")) Then
                                If Not CommDS.Tables(0).Rows(0).Item("GST_APIPer").ToString() = "" Then
                                    txtGST_APIAllow.Text = GV.parseString(CommDS.Tables(0).Rows(0).Item("GST_APIPer").ToString())
                                Else
                                    txtGST_APIAllow.Text = ""
                                End If
                            Else
                                txtGST_APIAllow.Text = ""
                            End If

                            If Not IsDBNull(CommDS.Tables(0).Rows(0).Item("BusBooking_APIPer")) Then
                                If Not CommDS.Tables(0).Rows(0).Item("BusBooking_APIPer").ToString() = "" Then
                                    txtBusBooking_APIAllow.Text = GV.parseString(CommDS.Tables(0).Rows(0).Item("BusBooking_APIPer").ToString())
                                Else
                                    txtBusBooking_APIAllow.Text = ""
                                End If
                            Else
                                txtBusBooking_APIAllow.Text = ""
                            End If
                            If Not IsDBNull(CommDS.Tables(0).Rows(0).Item("Rail_APIPer")) Then
                                If Not CommDS.Tables(0).Rows(0).Item("Rail_APIPer").ToString() = "" Then
                                    txtRail_APIAllow.Text = GV.parseString(CommDS.Tables(0).Rows(0).Item("Rail_APIPer").ToString())
                                Else
                                    txtRail_APIAllow.Text = ""
                                End If
                            Else
                                txtRail_APIAllow.Text = ""
                            End If

                        End If
                    End If
                End If

            End If


        Catch ex As Exception

        End Try
    End Sub
    

 
End Class