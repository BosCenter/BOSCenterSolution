
Public Class BOS_CommissionSettingMaster_Latest
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VEmployeeType, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            ' Clear()
            'FillData()
        Catch ex As Exception
        End Try
    End Sub
    Dim Recharge_APIPer, Flight_APIPer, PAN_APIPer, MoneyTransfer_APIPer, GST_APIPer, BusBooking_APIPer, Rail_APIPer As String
    'Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Try

    '        Dim isErrorFound As Boolean = False
    '        Dim isFocusApplied As Boolean = False
    '        lblError.Text = ""
    '        lblError.CssClass = ""

    '        If Not txtRechargeAPIAllow.Text.Trim = "" Then
    '            If CDec(txtRechargeAPI.Text.Trim) < CDec(txtRechargeAPIAllow.Text) Then
    '                txtRechargeAPIAllow.CssClass = "ValidationError"
    '                isErrorFound = True
    '                If isFocusApplied = False Then
    '                    txtRechargeAPIAllow.Focus()
    '                    isFocusApplied = True
    '                End If
    '            Else
    '                Recharge_APIPer = GV.parseString(txtRechargeAPIAllow.Text.Trim)
    '                txtRechargeAPIAllow.CssClass = "form-control"
    '            End If

    '        Else
    '            txtRechargeAPIAllow.CssClass = "form-control"
    '            Recharge_APIPer = "0"
    '        End If

    '        If Not txtFlight_APIPerAllow.Text.Trim = "" Then
    '            If CDec(txtFlight_APIPer.Text.Trim) < CDec(txtFlight_APIPerAllow.Text) Then
    '                txtFlight_APIPerAllow.CssClass = "ValidationError"
    '                isErrorFound = True
    '                If isFocusApplied = False Then
    '                    txtFlight_APIPerAllow.Focus()
    '                    isFocusApplied = True
    '                End If
    '            Else
    '                Flight_APIPer = GV.parseString(txtFlight_APIPerAllow.Text.Trim)
    '                txtFlight_APIPerAllow.CssClass = "form-control"
    '            End If

    '        Else
    '            txtFlight_APIPerAllow.CssClass = "form-control"
    '            Flight_APIPer = "0"
    '        End If

    '        If Not txtPAN_APIAllow.Text.Trim = "" Then
    '            If CDec(txtPAN_API.Text.Trim) < CDec(txtPAN_APIAllow.Text) Then
    '                txtPAN_APIAllow.CssClass = "ValidationError"
    '                isErrorFound = True
    '                If isFocusApplied = False Then
    '                    txtPAN_APIAllow.Focus()
    '                    isFocusApplied = True
    '                End If
    '            Else
    '                PAN_APIPer = GV.parseString(txtPAN_APIAllow.Text.Trim)
    '                txtPAN_APIAllow.CssClass = "form-control"
    '            End If

    '        Else
    '            txtPAN_APIAllow.CssClass = "form-control"
    '            PAN_APIPer = "0"
    '        End If

    '        If Not txtMoneyTransfer_APIAllow.Text.Trim = "" Then
    '            If CDec(txtMoneyTransfer_API.Text.Trim) < CDec(txtMoneyTransfer_APIAllow.Text) Then
    '                txtMoneyTransfer_APIAllow.CssClass = "ValidationError"
    '                isErrorFound = True
    '                If isFocusApplied = False Then
    '                    txtMoneyTransfer_APIAllow.Focus()
    '                    isFocusApplied = True
    '                End If
    '            Else
    '                MoneyTransfer_APIPer = GV.parseString(txtMoneyTransfer_APIAllow.Text.Trim)
    '                txtMoneyTransfer_APIAllow.CssClass = "form-control"
    '            End If

    '        Else
    '            txtMoneyTransfer_APIAllow.CssClass = "form-control"
    '            MoneyTransfer_APIPer = "0"
    '        End If

    '        If Not txtGST_APIAllow.Text.Trim = "" Then
    '            If CDec(txtGST_API.Text.Trim) < CDec(txtGST_APIAllow.Text) Then
    '                txtGST_APIAllow.CssClass = "ValidationError"
    '                isErrorFound = True
    '                If isFocusApplied = False Then
    '                    txtGST_APIAllow.Focus()
    '                    isFocusApplied = True
    '                End If
    '            Else
    '                GST_APIPer = GV.parseString(txtGST_APIAllow.Text.Trim)
    '                txtGST_APIAllow.CssClass = "form-control"
    '            End If

    '        Else
    '            txtGST_APIAllow.CssClass = "form-control"
    '            GST_APIPer = "0"
    '        End If


    '        If Not txtBusBooking_APIAllow.Text.Trim = "" Then
    '            If CDec(txtBusBooking_API.Text.Trim) < CDec(txtBusBooking_APIAllow.Text) Then
    '                txtBusBooking_APIAllow.CssClass = "ValidationError"
    '                isErrorFound = True
    '                If isFocusApplied = False Then
    '                    txtBusBooking_APIAllow.Focus()
    '                    isFocusApplied = True
    '                End If
    '            Else
    '                BusBooking_APIPer = GV.FL.parseString(txtBusBooking_APIAllow.Text.Trim)
    '                txtBusBooking_APIAllow.CssClass = "form-control"
    '            End If

    '        Else
    '            txtBusBooking_APIAllow.CssClass = "form-control"
    '            BusBooking_APIPer = "0"
    '        End If

    '        If Not txtRail_APIAllow.Text.Trim = "" Then
    '            If CDec(txtRail_API.Text.Trim) < CDec(txtRail_APIAllow.Text) Then
    '                txtRail_APIAllow.CssClass = "ValidationError"
    '                isErrorFound = True
    '                If isFocusApplied = False Then
    '                    txtRail_APIAllow.Focus()
    '                    isFocusApplied = True
    '                End If
    '            Else
    '                Rail_APIPer = GV.FL.parseString(txtRail_APIAllow.Text.Trim)
    '                txtRail_APIAllow.CssClass = "form-control"
    '            End If

    '        Else
    '            txtRail_APIAllow.CssClass = "form-control"
    '            Rail_APIPer = "0"
    '        End If
    '        If isErrorFound = True Then
    '            lblError.Text = "Allow Commission Not Greater Than Getting Commission."
    '            lblError.CssClass = "errorlabels"
    '            Exit Sub
    '        End If

    '        Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
    '        Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
    '        VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
    '        VUpdatedOn = "getdate()"
    '        If lblSessionFlag.Text = 0 Then
    '            If GV.FL.RecCount("BOS_APICommissionSettigs where RegistrationID='" & Registraionid & "'and AgentType='" & AgentType & "'  ") > 0 Then 'Change where condition according to Criteria 
    '                QryStr = " Update BOS_APICommissionSettigs set Recharge_APIPer='" & Recharge_APIPer & "',Flight_APIPer='" & Flight_APIPer & "',PAN_APIPer='" & PAN_APIPer & "',MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "',GST_APIPer='" & GST_APIPer & "',BusBooking_APIPer='" & BusBooking_APIPer & "',Rail_APIPer='" & Rail_APIPer & "',updatedBy='" & VUpdatedBy & "',UpdatedOn=" & VUpdatedOn & " where AgentType='" & AgentType & "' and RegistrationID='" & Registraionid & "' ; "
    '            Else
    '                QryStr = " insert into BOS_APICommissionSettigs (RegistrationID,AgentType,Recharge_APIPer,Flight_APIPer,PAN_APIPer,MoneyTransfer_APIPer,GST_APIPer,BusBooking_APIPer,Rail_APIPer,UpdatedBy,UpdatedOn) values( '" & Registraionid & "','" & AgentType & "','" & Recharge_APIPer & "','" & Flight_APIPer & "','" & PAN_APIPer & "','" & MoneyTransfer_APIPer & "','" & GST_APIPer & "','" & BusBooking_APIPer & "','" & Rail_APIPer & "','" & VUpdatedBy & "'," & VUpdatedOn & " ) ; "
    '            End If
    '            If AgentType.Trim.ToUpper = "Admin".Trim.ToUpper Or AgentType.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set Recharge_APIPer='" & Recharge_APIPer & "' where Recharge_APIPer>" & Recharge_APIPer & " ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set Flight_APIPer='" & Flight_APIPer & "' where Flight_APIPer>" & Flight_APIPer & " ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set PAN_APIPer='" & PAN_APIPer & "' where PAN_APIPer>" & PAN_APIPer & " ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "' where MoneyTransfer_APIPer>" & MoneyTransfer_APIPer & " ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set GST_APIPer='" & GST_APIPer & "' where GST_APIPer>" & GST_APIPer & " ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set BusBooking_APIPer='" & BusBooking_APIPer & "' where BusBooking_APIPer>" & BusBooking_APIPer & " ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set Rail_APIPer='" & Rail_APIPer & "' where Rail_APIPer>" & Rail_APIPer & " ;"
    '            ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set Recharge_APIPer='" & Recharge_APIPer & "' where Recharge_APIPer>" & Recharge_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ; "
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set Flight_APIPer='" & Flight_APIPer & "' where Flight_APIPer>" & Flight_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set PAN_APIPer='" & PAN_APIPer & "' where PAN_APIPer>" & PAN_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set MoneyTransfer_APIPer='" & MoneyTransfer_APIPer & "' where MoneyTransfer_APIPer>" & MoneyTransfer_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set GST_APIPer='" & GST_APIPer & "' where GST_APIPer>" & GST_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set BusBooking_APIPer='" & BusBooking_APIPer & "' where BusBooking_APIPer>" & BusBooking_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"
    '                QryStr = QryStr & " update BOS_APICommissionSettigs set Rail_APIPer='" & Rail_APIPer & "' where Rail_APIPer>" & Rail_APIPer & " and RegistrationId in (Select RegistrationId from [BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') ;"

    '            End If


    '            If GV.FL.DMLQueries(QryStr) = True Then
    '                Clear()
    '                FillData()
    '                lblError.Text = "Commission Updated Successfully."
    '                lblError.CssClass = "Successlabels"

    '            Else
    '                lblError.Text = "Sorry !! Process Can't be Completed."
    '                lblError.CssClass = "errorlabels"
    '            End If
    '        End If

    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub Clear()
    '    Try
    '        VEmployeeType = ""
    '        VUpdatedBy = ""
    '        VUpdatedOn = ""

    '        txtRail_APIAllow.CssClass = "form-control"
    '        txtFlight_APIPerAllow.CssClass = "form-control"
    '        txtPAN_APIAllow.CssClass = "form-control"
    '        txtMoneyTransfer_APIAllow.CssClass = "form-control"
    '        txtGST_APIAllow.CssClass = "form-control"
    '        txtRechargeAPIAllow.CssClass = "form-control"
    '        txtBusBooking_APIAllow.CssClass = "form-control"
    '        txtRechargeAPIAllow.CssClass = "form-control"

    '        lblSessionFlag.Text = 0
    '        btnSave.Text = "Update"
    '        lblError.Text = ""
    '        btnSave.Enabled = True

    '        lblError.CssClass = ""
    '        '  txtEmployeeTypeName.Focus()
    '        lblUpadate.Text = ""
    '    Catch ex As Exception
    '    End Try
    'End Sub


    Public Sub Fill_Contain_Category_No()
        Try
            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Or AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where ProductType='API' and ActiveStatus='Active' and ContainCategory='No' order by Title"
                DS = New DataSet

                DS = GV.FL.OpenDsWithSelectQuery(qryStr)
                ListView1.DataSource = DS
                ListView1.DataBind()
                If DS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To DS.Tables(0).Rows.Count - 1


                        Dim Canchange As String = ""
                        If Not IsDBNull(DS.Tables(0).Rows(i).Item("CanChange")) Then
                            If Not DS.Tables(0).Rows(i).Item("CanChange").ToString() = "" Then
                                Canchange = DS.Tables(0).Rows(i).Item("CanChange").ToString()
                            Else
                                Canchange = ""
                            End If
                        Else
                            Canchange = ""
                        End If

                        Dim lblAPIName As Label = DirectCast(ListView1.Items(i).FindControl("lblAPIName"), Label)
                        Dim lblListAPIName As Label = DirectCast(ListView1.Items(i).FindControl("lblListAPIName"), Label)
                        Dim lblGettingCommType As Label = DirectCast(ListView1.Items(i).FindControl("lblGettingCommType"), Label)
                        Dim txtRechargeAPI As TextBox = DirectCast(ListView1.Items(i).FindControl("txtRechargeAPI"), TextBox)
                        Dim txtServiceCharge As TextBox = DirectCast(ListView1.Items(i).FindControl("txtServiceCharge"), TextBox)
                        Dim txtRechargeAPIAllow As TextBox = DirectCast(ListView1.Items(i).FindControl("txtRechargeAPIAllow"), TextBox)
                        Dim lnkUpdateCommission_List As LinkButton = DirectCast(ListView1.Items(i).FindControl("lnkUpdateCommission_List"), LinkButton)
                        'lnkUpdateCommission_List

                        If Canchange.Trim.ToUpper = "YES".Trim Then ' /// Condition IF Allowed YES

                            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                                txtRechargeAPIAllow.Visible = True
                                txtRechargeAPIAllow.CssClass = "form-control"
                                lblAllowComm.Visible = True

                                lnkUpdateCommission_List.Visible = True
                            Else
                                lblAllowComm.Visible = False


                                txtRechargeAPIAllow.Visible = False
                                txtRechargeAPIAllow.CssClass = "form-control"


                                lnkUpdateCommission_List.Visible = False

                            End If




                            Dim CommissionType As String = ""

                            Dim CommissionType_Col_Name, Commission_Val_Col_Name, ServiceType, ServiceCharge As String

                            ServiceType = "ServiceType"
                            ServiceCharge = "ServiceCharge"

                            If Not IsDBNull(DS.Tables(0).Rows(i).Item(ServiceCharge)) Then
                                If Not DS.Tables(0).Rows(i).Item(ServiceCharge).ToString() = "" Then
                                    txtServiceCharge.Text = DS.Tables(0).Rows(i).Item(ServiceCharge).ToString()

                                    ServiceType = DS.Tables(0).Rows(i).Item(ServiceType).ToString()
                                    If ServiceType.Trim = "Not Applicable".Trim Then
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( N/A )"
                                    ElseIf ServiceType.Trim = "Amount".Trim Then
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( Amt )"
                                    ElseIf ServiceType.Trim = "Percentage".Trim Then
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( % )"
                                    Else
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString()
                                    End If

                                Else
                                    txtServiceCharge.Text = "0"
                                End If
                            Else
                                txtServiceCharge.Text = "0"
                            End If

                            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then

                                CommissionType_Col_Name = "CommissionType"
                                Commission_Val_Col_Name = "Commission"

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item(CommissionType_Col_Name)) Then
                                    If Not DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString() = "" Then
                                        CommissionType = DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString()
                                        lblGettingCommType.Text = CommissionType
                                    Else
                                        CommissionType = ""
                                        lblGettingCommType.Text = ""
                                    End If
                                Else
                                    CommissionType = ""
                                    lblGettingCommType.Text = ""
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                                    If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                                        If CommissionType.Trim = "Not Applicable".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N/A )"
                                        ElseIf CommissionType.Trim = "Amount".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                                        ElseIf CommissionType.Trim = "Percentage".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                                        Else
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                        End If
                                        lblListAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                    Else
                                        lblAPIName.Text = ""

                                    End If
                                Else
                                    lblAPIName.Text = ""
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name)) Then
                                    If Not DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString() = "" Then
                                        txtRechargeAPI.Text = DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString()
                                    Else
                                        txtRechargeAPI.Text = ""
                                    End If
                                Else
                                    txtRechargeAPI.Text = ""
                                End If

                                If GV.parseString(txtRechargeAPI.Text) = "" Then
                                    txtRechargeAPI.Text = "0"
                                End If


                                Dim getCommission As Decimal = CDec(GV.parseString(txtRechargeAPI.Text))

                                If getCommission <= 0 Then
                                    txtRechargeAPIAllow.Visible = False
                                    txtRechargeAPIAllow.CssClass = "form-control"

                                    lnkUpdateCommission_List.Visible = False
                                Else
                                    Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

                                    If comm.Trim = "" Then
                                        comm = "0"
                                    End If
                                    txtRechargeAPIAllow.Text = comm
                                End If







                            ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then

                                CommissionType_Col_Name = "CommissionType" ' Same as distributor
                                Commission_Val_Col_Name = "Commission"



                                If Not IsDBNull(DS.Tables(0).Rows(i).Item(CommissionType_Col_Name)) Then
                                    If Not DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString() = "" Then
                                        CommissionType = DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString()
                                        lblGettingCommType.Text = CommissionType
                                    Else
                                        CommissionType = ""
                                        lblGettingCommType.Text = ""
                                    End If
                                Else
                                    CommissionType = ""
                                    lblGettingCommType.Text = ""
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                                    If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                                        If CommissionType.Trim = "Not Applicable".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N/A )"
                                        ElseIf CommissionType.Trim = "Amount".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                                        ElseIf CommissionType.Trim = "Percentage".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                                        Else
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                        End If
                                        lblListAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                    Else
                                        lblAPIName.Text = ""
                                    End If
                                Else
                                    lblAPIName.Text = ""
                                End If



                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

                                If comm.Trim = "" Then
                                    comm = "0"
                                End If
                                txtRechargeAPI.Text = comm



                                If GV.parseString(txtRechargeAPI.Text) = "" Then
                                    txtRechargeAPI.Text = "0"
                                End If


                                Dim getCommission As Decimal = CDec(GV.parseString(txtRechargeAPI.Text))

                                If getCommission <= 0 Then
                                    txtRechargeAPIAllow.Visible = False
                                    txtRechargeAPIAllow.CssClass = "form-control"

                                    lnkUpdateCommission_List.Visible = False
                                Else
                                    comm = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

                                    If comm.Trim = "" Then
                                        comm = "0"
                                    End If
                                    txtRechargeAPIAllow.Text = comm
                                End If







                            ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then

                                CommissionType_Col_Name = "CommissionType" ' Same as distributor
                                Commission_Val_Col_Name = "Commission"



                                If Not IsDBNull(DS.Tables(0).Rows(i).Item(CommissionType_Col_Name)) Then
                                    If Not DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString() = "" Then
                                        CommissionType = DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString()
                                        lblGettingCommType.Text = CommissionType
                                    Else
                                        CommissionType = ""
                                        lblGettingCommType.Text = ""
                                    End If
                                Else
                                    CommissionType = ""
                                    lblGettingCommType.Text = ""
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                                    If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                                        If CommissionType.Trim = "Not Applicable".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N/A )"
                                        ElseIf CommissionType.Trim = "Amount".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                                        ElseIf CommissionType.Trim = "Percentage".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                                        Else
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                        End If
                                        lblListAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                    Else
                                        lblAPIName.Text = ""
                                    End If
                                Else
                                    lblAPIName.Text = ""
                                End If



                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

                                If comm.Trim = "" Then
                                    comm = "0"
                                End If
                                txtRechargeAPI.Text = comm
                            ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                                CommissionType_Col_Name = "CommissionType" ' Same as distributor
                                Commission_Val_Col_Name = "Commission"



                                If Not IsDBNull(DS.Tables(0).Rows(i).Item(CommissionType_Col_Name)) Then
                                    If Not DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString() = "" Then
                                        CommissionType = DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString()
                                        lblGettingCommType.Text = CommissionType
                                    Else
                                        CommissionType = ""
                                        lblGettingCommType.Text = ""
                                    End If
                                Else
                                    CommissionType = ""
                                    lblGettingCommType.Text = ""
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                                    If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                                        If CommissionType.Trim = "Not Applicable".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N/A )"
                                        ElseIf CommissionType.Trim = "Amount".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                                        ElseIf CommissionType.Trim = "Percentage".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                                        Else
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                        End If
                                        lblListAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                    Else
                                        lblAPIName.Text = ""
                                    End If
                                Else
                                    lblAPIName.Text = ""
                                End If



                                ' Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")
                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

                                If comm.Trim = "" Then
                                    comm = "0"
                                End If
                                txtRechargeAPI.Text = comm

                            Else


                                CommissionType_Col_Name = "CommissionType"
                                Commission_Val_Col_Name = "Commission"

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item(CommissionType_Col_Name)) Then
                                    If Not DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString() = "" Then
                                        CommissionType = DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString()
                                        lblGettingCommType.Text = CommissionType
                                    Else
                                        CommissionType = ""
                                        lblGettingCommType.Text = ""
                                    End If
                                Else
                                    CommissionType = ""
                                    lblGettingCommType.Text = ""
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                                    If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                                        If CommissionType.Trim = "Not Applicable".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N/A )"
                                        ElseIf CommissionType.Trim = "Amount".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                                        ElseIf CommissionType.Trim = "Percentage".Trim Then
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                                        Else
                                            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                        End If
                                        lblListAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                    Else
                                        lblAPIName.Text = ""
                                    End If
                                Else
                                    lblAPIName.Text = ""
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name)) Then
                                    If Not DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString() = "" Then
                                        txtRechargeAPI.Text = DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString()
                                    Else
                                        txtRechargeAPI.Text = ""
                                    End If
                                Else
                                    txtRechargeAPI.Text = ""
                                End If

                            End If





                        Else ' Case No Change Downwards


                            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                                txtRechargeAPIAllow.Visible = False
                                txtRechargeAPIAllow.CssClass = "form-control"
                                lblAllowComm.Visible = False
                                lnkUpdateCommission_List.Visible = False

                            Else
                                txtRechargeAPIAllow.Visible = False
                                txtRechargeAPIAllow.CssClass = "form-control"
                                lblAllowComm.Visible = False
                                lnkUpdateCommission_List.Visible = False
                            End If


                            Dim CommissionType As String = ""

                            Dim CommissionType_Col_Name, Commission_Val_Col_Name As String


                            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then

                                CommissionType_Col_Name = "CommissionType"
                                Commission_Val_Col_Name = "Commission"

                            ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then

                                CommissionType_Col_Name = "Sub_Dis_CommissionType"
                                Commission_Val_Col_Name = "Sub_Dis_Commission"

                            ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then

                                CommissionType_Col_Name = "Retailer_CommissionType"
                                Commission_Val_Col_Name = "Retailer_Commission"
                            ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                                CommissionType_Col_Name = "Customer_CommissionType"
                                Commission_Val_Col_Name = "Customer_Commission"

                            Else
                                CommissionType_Col_Name = "CommissionType"
                                Commission_Val_Col_Name = "Commission"

                            End If

                            Dim ServiceType, ServiceCharge As String

                            ServiceType = "ServiceType"
                            ServiceCharge = "ServiceCharge"

                            If Not IsDBNull(DS.Tables(0).Rows(i).Item(ServiceCharge)) Then
                                If Not DS.Tables(0).Rows(i).Item(ServiceCharge).ToString() = "" Then
                                    txtServiceCharge.Text = DS.Tables(0).Rows(i).Item(ServiceCharge).ToString()

                                    ServiceType = DS.Tables(0).Rows(i).Item(ServiceType).ToString()
                                    If ServiceType.Trim = "Not Applicable".Trim Then
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( N/A )"
                                    ElseIf ServiceType.Trim = "Amount".Trim Then
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( Amt )"
                                    ElseIf ServiceType.Trim = "Percentage".Trim Then
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( % )"
                                    Else
                                        txtServiceCharge.Text = txtServiceCharge.Text.ToString()
                                    End If

                                Else
                                    txtServiceCharge.Text = "0"
                                End If
                            Else
                                txtServiceCharge.Text = "0"
                            End If


                            If Not IsDBNull(DS.Tables(0).Rows(i).Item(CommissionType_Col_Name)) Then
                                If Not DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString() = "" Then
                                    CommissionType = DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString()
                                    lblGettingCommType.Text = CommissionType
                                Else
                                    CommissionType = ""
                                    lblGettingCommType.Text = ""
                                End If
                            Else
                                CommissionType = ""
                                lblGettingCommType.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                                If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                                    If CommissionType.Trim = "Not Applicable".Trim Then
                                        lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N/A )"
                                    ElseIf CommissionType.Trim = "Amount".Trim Then
                                        lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                                    ElseIf CommissionType.Trim = "Percentage".Trim Then
                                        lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                                    Else
                                        lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                    End If
                                    lblListAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                Else
                                    lblAPIName.Text = ""
                                End If
                            Else
                                lblAPIName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name)) Then
                                If Not DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString() = "" Then
                                    txtRechargeAPI.Text = DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString()
                                Else
                                    txtRechargeAPI.Text = ""
                                End If
                            Else
                                txtRechargeAPI.Text = ""
                            End If






                        End If

                    Next
                End If
            Else
                'Case Admin, Other Employee

                Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)

                Dim qryStr As String = "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where ProductType='API' and ActiveStatus='Active' and ContainCategory='No' and AdminID='" & CompanyCode & "' order by Title"
                DS = New DataSet

                DS = GV.FL.OpenDsWithSelectQuery(qryStr)
                ListView1.DataSource = DS
                ListView1.DataBind()
                If DS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To DS.Tables(0).Rows.Count - 1

                        Dim lblAPIName As Label = DirectCast(ListView1.Items(i).FindControl("lblAPIName"), Label)
                        Dim lblListAPIName As Label = DirectCast(ListView1.Items(i).FindControl("lblListAPIName"), Label)
                        Dim lblGettingCommType As Label = DirectCast(ListView1.Items(i).FindControl("lblGettingCommType"), Label)
                        Dim txtRechargeAPI As TextBox = DirectCast(ListView1.Items(i).FindControl("txtRechargeAPI"), TextBox)
                        Dim txtRechargeAPIAllow As TextBox = DirectCast(ListView1.Items(i).FindControl("txtRechargeAPIAllow"), TextBox)
                        Dim lnkUpdateCommission_List As LinkButton = DirectCast(ListView1.Items(i).FindControl("lnkUpdateCommission_List"), LinkButton)

                        Dim txtServiceCharge As TextBox = DirectCast(ListView1.Items(i).FindControl("txtServiceCharge"), TextBox)

                        'lnkUpdateCommission_List


                        txtRechargeAPIAllow.Visible = False
                        txtRechargeAPIAllow.CssClass = "form-control"
                        lblAllowComm.Visible = False
                        lnkUpdateCommission_List.Visible = False


                        Dim CommissionType As String = ""

                        Dim CommissionType_Col_Name, Commission_Val_Col_Name, ServiceType, ServiceCharge As String

                        CommissionType_Col_Name = "Admin_CommissionType"
                        Commission_Val_Col_Name = "Admin_Commission"



                        If Not IsDBNull(DS.Tables(0).Rows(i).Item(CommissionType_Col_Name)) Then
                            If Not DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString() = "" Then
                                CommissionType = DS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString()
                                lblGettingCommType.Text = CommissionType
                            Else
                                CommissionType = ""
                                lblGettingCommType.Text = ""
                            End If
                        Else
                            CommissionType = ""
                            lblGettingCommType.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                            If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                                If CommissionType.Trim = "Not Applicable".Trim Then
                                    lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N/A )"
                                ElseIf CommissionType.Trim = "Amount".Trim Then
                                    lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                                ElseIf CommissionType.Trim = "Percentage".Trim Then
                                    lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                                Else
                                    lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                                End If
                                lblListAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString()
                            Else
                                lblAPIName.Text = ""
                            End If
                        Else
                            lblAPIName.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name)) Then
                            If Not DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString() = "" Then
                                txtRechargeAPI.Text = DS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString()
                            Else
                                txtRechargeAPI.Text = ""
                            End If
                        Else
                            txtRechargeAPI.Text = ""
                        End If


                        ServiceType = "ServiceType"
                        ServiceCharge = "ServiceCharge"

                        If Not IsDBNull(DS.Tables(0).Rows(i).Item(ServiceCharge)) Then
                            If Not DS.Tables(0).Rows(i).Item(ServiceCharge).ToString() = "" Then
                                txtServiceCharge.Text = DS.Tables(0).Rows(i).Item(ServiceCharge).ToString()

                                ServiceType = DS.Tables(0).Rows(i).Item(ServiceType).ToString()
                                If ServiceType.Trim = "Not Applicable".Trim Then
                                    txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( N/A )"
                                ElseIf ServiceType.Trim = "Amount".Trim Then
                                    txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( Amt )"
                                ElseIf ServiceType.Trim = "Percentage".Trim Then
                                    txtServiceCharge.Text = txtServiceCharge.Text.ToString() & " ( % )"
                                Else
                                    txtServiceCharge.Text = txtServiceCharge.Text.ToString()
                                End If

                            Else
                                txtServiceCharge.Text = "0"
                            End If
                        Else
                            txtServiceCharge.Text = "0"
                        End If





                    Next
                End If

            End If



        Catch ex As Exception

        End Try
    End Sub

    Public Sub Fill_Slabwise_Commission()
        Try
            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim comType, comAmt As String
            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Or AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    comType = " Dis_CommissionType "
                    comAmt = " Dis_Commission "

                ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    comType = " Sub_Dis_CommissionType "
                    comAmt = " Sub_Dis_Commission "
                ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    comType = " Retailer_CommissionType "
                    comAmt = " Retailer_Commission "
                ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    comType = " Customer_CommissionType "
                    comAmt = " Customer_Commission "
                Else
                    comType = " Dis_CommissionType "
                    comAmt = " Dis_Commission "
                End If

                Dim grdQry As String = "select RID as 'SrNo', [APIName] as APIName, (convert(varchar,FromAmount)+'-'+convert(varchar,ToAmount)) as 'Slab', " & comType & " as CommissionType," & comAmt & " as Commission from    " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise  where APIName in (select distinct Title from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where  ProductType='API' and ActiveStatus='Active' and  SlabApplicable='With Slab' )   order by FromAmount asc"
                GV.FL.AddInGridViewWithFieldName(grd_SlabwiseCommission, grdQry)
                If grd_SlabwiseCommission.Rows.Count > 0 Then
                    GV.FL.showSerialnoOnGridView(grd_SlabwiseCommission, 0)
                    Div_SlabwiseCommission.Visible = True
                Else
                    Div_SlabwiseCommission.Visible = False
                End If


            Else
                '/// Admin , Other Employees
                Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                comType = " Admin_CommissionType "
                comAmt = " Admin_Commission "

                Dim grdQry As String = "select RID as 'SrNo', [APIName] as APIName, (convert(varchar,FromAmount)+'-'+convert(varchar,ToAmount)) as 'Slab', " & comType & " as CommissionType," & comAmt & " as Commission from    " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA  where AdminID='" & CompanyCode & "' and  APIName in (select distinct Title from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where  ProductType='API' and ActiveStatus='Active' and  SlabApplicable='With Slab' and AdminID='" & CompanyCode & "' )   order by FromAmount asc"
                GV.FL.AddInGridViewWithFieldName(grd_SlabwiseCommission, grdQry)
                If grd_SlabwiseCommission.Rows.Count > 0 Then
                    GV.FL.showSerialnoOnGridView(grd_SlabwiseCommission, 0)
                    Div_SlabwiseCommission.Visible = True
                Else
                    Div_SlabwiseCommission.Visible = False
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub


    Public Sub Fill_Contain_Category_Yes()
        Try


            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Or AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    lblCommssionHeading.Text = "Set Commission For Distributor"
                ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    lblCommssionHeading.Text = "Set Commission For Retailer"
                ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    lblAllowComm.Visible = False
                    btnSave.Visible = False
                    btnClear.Visible = False
                    lblCommssionHeading.Text = "Commission Chart"
                ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    lblAllowComm.Visible = False
                    btnSave.Visible = False
                    btnClear.Visible = False
                    lblCommssionHeading.Text = "Commission Chart"
                Else
                    lblAllowComm.Visible = False
                    btnSave.Visible = False
                    btnClear.Visible = False
                    lblCommssionHeading.Text = "Set Commission For Master Distributor"
                End If



                Dim grdQry As String = "select RID as 'SrNo',isnull((select top 1 CanChange from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission OC where APIName in (select distinct Title from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where ProductType='API' and ActiveStatus='Active' and ContainCategory='Yes')"
                Dim grdDS As DataSet = New DataSet
                grdDS = GV.FL.OpenDsWithSelectQuery(grdQry)
                GridView1.DataSource = grdDS
                GridView1.DataBind()

                GV.FL.showSerialnoOnGridView(GridView1, 0)


                If GridView1.Rows.Count > 0 Then
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        Dim lblCanChange As Label = DirectCast(GridView1.Rows(i).Cells(7).FindControl("lblCanChange"), Label)
                        Dim lblAPIName As Label = DirectCast(GridView1.Rows(i).Cells(7).FindControl("lblAPIName"), Label)
                        Dim Canchange As String = lblCanChange.Text
                        Dim APIName As String = lblAPIName.Text
                        Dim lnkUpdateCommission As LinkButton = DirectCast(GridView1.Rows(i).Cells(8).FindControl("lnkUpdateCommission"), LinkButton)
                        Dim txtAllowCommission As TextBox = DirectCast(GridView1.Rows(i).Cells(7).FindControl("txtAllowCommission"), TextBox)


                        If Canchange.Trim.ToUpper = "YES".Trim Then ' /// Condition IF Allowed YES

                            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                                txtAllowCommission.Visible = True
                                txtAllowCommission.CssClass = "form-control"

                                lnkUpdateCommission.Visible = True
                                lnkUpdateCommission.CssClass = "btn btn-primary"
                            Else
                                txtAllowCommission.Visible = False
                                txtAllowCommission.CssClass = "form-control"

                                lnkUpdateCommission.Visible = False
                                lnkUpdateCommission.CssClass = "btn btn-primary"
                            End If




                            Dim CommissionType As String = ""

                            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then

                                Dim getCommission As Decimal = CDec(GV.parseString(GridView1.Rows(i).Cells(6).Text))

                                If getCommission <= 0 Then
                                    txtAllowCommission.Visible = False
                                    txtAllowCommission.CssClass = "form-control"

                                    lnkUpdateCommission.Visible = False
                                    lnkUpdateCommission.CssClass = "btn btn-primary"
                                Else

                                    Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(3).Text) & "' ")

                                    If comm.Trim = "" Then
                                        comm = "0"
                                    End If
                                    txtAllowCommission.Text = comm
                                End If



                            ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then


                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(3).Text) & "' ")

                                If comm.Trim = "" Then
                                    comm = "0"
                                End If
                                GridView1.Rows(i).Cells(6).Text = comm

                                Dim getCommission As Decimal = CDec(GV.parseString(GridView1.Rows(i).Cells(6).Text))

                                If getCommission <= 0 Then
                                    txtAllowCommission.Visible = False
                                    txtAllowCommission.CssClass = "form-control"

                                    lnkUpdateCommission.Visible = False
                                    lnkUpdateCommission.CssClass = "btn btn-primary"
                                Else
                                    Dim comm1 As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(3).Text) & "' ")

                                    If comm1.Trim = "" Then
                                        comm1 = "0"
                                    End If

                                    txtAllowCommission.Text = comm1
                                End If

                            ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then

                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(3).Text) & "' ")

                                If comm.Trim = "" Then
                                    comm = "0"
                                End If
                                GridView1.Rows(i).Cells(6).Text = comm
                            ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                                'Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(1).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' ")
                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(3).Text) & "' ")


                                If comm.Trim = "" Then
                                    comm = "0"
                                End If
                                GridView1.Rows(i).Cells(6).Text = comm

                            Else

                            End If





                        Else ' Case No Change Downwards


                            txtAllowCommission.Visible = False
                            txtAllowCommission.CssClass = "form-control"

                            lnkUpdateCommission.Visible = False
                            lnkUpdateCommission.CssClass = "btn btn-primary"

                            Dim CommissionType As String = ""

                            Dim CommissionType_Col_Name, Commission_Val_Col_Name As String


                            If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then

                                CommissionType_Col_Name = "Dis_CommissionType"
                                Commission_Val_Col_Name = "Dis_Commission"

                            ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then

                                CommissionType_Col_Name = "Sub_Dis_CommissionType"
                                Commission_Val_Col_Name = "Sub_Dis_Commission"

                            ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then

                                CommissionType_Col_Name = "Retailer_CommissionType"
                                Commission_Val_Col_Name = "Retailer_Commission"

                            ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                                CommissionType_Col_Name = "Customer_CommissionType"
                                Commission_Val_Col_Name = "Customer_Commission"

                            Else
                                CommissionType_Col_Name = "Dis_CommissionType"
                                Commission_Val_Col_Name = "Dis_Commission"
                            End If

                            GridView1.Rows(i).Cells(5).Text = grdDS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString
                            GridView1.Rows(i).Cells(6).Text = grdDS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString



                        End If





                    Next
                End If



            Else
                'case Admin / other employees


                Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)

                lblAllowComm.Visible = False
                btnSave.Visible = False
                btnClear.Visible = False


                ' select RID as Srno,APIName,Category,Code,OperatorName,Admin_CommissionType as Dis_CommissionType,Admin_Commission as Dis_Commission, 'No' as CanChange from BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='CMP1085' and ActiveStatus='Active'
                ' Dim grdQry As String = "select RID as 'SrNo',isnull((select top 1 CanChange from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission OC where APIName in (select distinct Title from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where ProductType='API' and ActiveStatus='Active' and ContainCategory='Yes')"

                Dim grdQry As String = "select RID as Srno,APIName,Category,Code,OperatorName,Admin_CommissionType as Dis_CommissionType,Admin_Commission as Dis_Commission, 'No' as CanChange from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & CompanyCode & "' and ActiveStatus='Active'"

                Dim grdDS As DataSet = New DataSet
                grdDS = GV.FL.OpenDsWithSelectQuery(grdQry)
                GridView1.DataSource = grdDS
                GridView1.DataBind()

                GV.FL.showSerialnoOnGridView(GridView1, 0)


                If GridView1.Rows.Count > 0 Then
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        Dim lblAPIName As Label = DirectCast(GridView1.Rows(i).Cells(6).FindControl("lblAPIName"), Label)
                        Dim APIName As String = lblAPIName.Text
                        Dim lnkUpdateCommission As LinkButton = DirectCast(GridView1.Rows(i).Cells(7).FindControl("lnkUpdateCommission"), LinkButton)
                        Dim txtAllowCommission As TextBox = DirectCast(GridView1.Rows(i).Cells(6).FindControl("txtAllowCommission"), TextBox)


                        txtAllowCommission.Visible = False
                        txtAllowCommission.CssClass = "form-control"

                        lnkUpdateCommission.Visible = False
                        lnkUpdateCommission.CssClass = "btn btn-primary"


                        'Dim CommissionType As String = ""

                        'Dim CommissionType_Col_Name, Commission_Val_Col_Name As String

                        'CommissionType_Col_Name = "Dis_CommissionType"
                        'Commission_Val_Col_Name = "Dis_Commission"

                        'GridView1.Rows(i).Cells(4).Text = grdDS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString
                        'GridView1.Rows(i).Cells(5).Text = grdDS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString

                    Next
                End If


            End If
          




        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                lblAllowComm.Visible = False

                Fill_Contain_Category_No()
                Fill_Contain_Category_Yes()
                Fill_Slabwise_Commission()

                Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    lblCommssionHeading.Text = "Set Commission For Distributor"
                ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    lblCommssionHeading.Text = "Set Commission For Retailer"
                ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    lblAllowComm.Visible = False
                    btnSave.Visible = False
                    btnClear.Visible = False
                    lblCommssionHeading.Text = "Commission Chart"
                ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then
                    lblAllowComm.Visible = False
                    btnSave.Visible = False
                    btnClear.Visible = False
                    lblCommssionHeading.Text = "Commission Chart"
                Else
                    lblAllowComm.Visible = False
                    btnSave.Visible = False
                    btnClear.Visible = False
                    lblCommssionHeading.Text = "Commission Chart"
                End If

                btnSave.Visible = False
                btnClear.Visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdupdateRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)




            Dim txtAllowCommission As TextBox = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(6).FindControl("txtAllowCommission"), TextBox)
            txtAllowCommission.CssClass = "form-control inputtext"
            Dim Commission As String = txtAllowCommission.Text

            Dim CommissionType As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            Dim GettingCommission As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            Dim lblAPIName As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(6).FindControl("lblAPIName"), Label)


            Dim APIName As String = GV.parseString(lblAPIName.Text)
            Dim Category As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
            Dim Code As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            Dim OperatorName As String = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            'OperatorName



            If GV.parseString(Commission) = "" Then
                Commission = "0"
            End If

            If GV.parseString(GettingCommission) = "" Then
                GettingCommission = "0"
            End If

            If CDec(Commission) > CDec(GettingCommission) Then
                txtAllowCommission.CssClass = "ValidationError"
                txtAllowCommission.Focus()
                lblError.Text = "Allow Commission Can't be More than Getting Commission."
                lblError.CssClass = "errorlabels"
            Else
                '[Category],[Code]

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & APIName & "' and Code='" & APIName & "'  ") > 0 Then 'Change where condition according to Criteria 
                    QryStr = " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where [RegistrationID]='" & Registraionid & "' and APIName='" & APIName & "'  and Category='" & APIName & "' and Code='" & APIName & "' ;"

                    If AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                        QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and  [RegistrationID] in (Select RegistrationId from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') and APIName='" & APIName & "'  and Category='" & APIName & "' and Code='" & APIName & "' "
                    ElseIf AgentType.Trim.ToUpper = "Admin".Trim.ToUpper Or AgentType.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        QryStr = QryStr & "  update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and APIName='" & APIName & "'  and Category='" & APIName & "' and Code='" & APIName & "' "
                    End If

                Else
                    QryStr = " INSERT INTO " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents ([Category],[Code],[OperatorName],[AgentType],[RegistrationID],[APIName],[CommissionType],[Commission],[RecordDatetime],[UpdatedBy],[UpdatedOn])  VALUES('" & Category & "','" & Code & "','" & OperatorName & "','" & AgentType & "','" & Registraionid & "','" & APIName & "','" & CommissionType & "'," & Commission & ",getdate(),'" & Registraionid & "',getdate()) ;"
                End If

               

                If GV.FL.DMLQueriesBulk(QryStr) = True Then
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



    Protected Sub grdupdate_List_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
           
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            
            Dim rowId As Integer
            Dim str As String
            Dim strRow() As String
            str = btndetails.UniqueID
            strRow = Split(str, "$")
            rowId = strRow(3).Substring(4)
            
            lblError.Text = ""
            lblError.CssClass = ""

            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim lblListAPIName As Label = DirectCast(ListView1.Items(rowId).FindControl("lblListAPIName"), Label)
            Dim lblGettingCommType As Label = DirectCast(ListView1.Items(rowId).FindControl("lblGettingCommType"), Label)
            Dim txtRechargeAPIAllow As TextBox = CType(ListView1.Items(rowId).FindControl("txtRechargeAPIAllow"), TextBox)
            Dim txtRechargeAPI As TextBox = CType(ListView1.Items(rowId).FindControl("txtRechargeAPI"), TextBox) ' Getting Commission
            txtRechargeAPIAllow.CssClass = "form-control inputtext"


            Dim Commission As String = txtRechargeAPIAllow.Text
            Dim APIName As String = GV.parseString(lblListAPIName.Text)
            Dim CommissionType As String = GV.parseString(lblGettingCommType.Text)
            Dim GettingCommission As String = txtRechargeAPI.Text.Trim

            If GV.parseString(Commission) = "" Then
                Commission = "0"
            End If

            If GV.parseString(GettingCommission) = "" Then
                GettingCommission = "0"
            End If

            If CDec(Commission) > CDec(GettingCommission) Then
                txtRechargeAPIAllow.CssClass = "ValidationError"
                txtRechargeAPIAllow.Focus()
                lblError.Text = "Allow Commission Can't be More than Getting Commission."
                lblError.CssClass = "errorlabels"
            Else

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "'  ") > 0 Then 'Change where condition according to Criteria 
                    QryStr = " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where [RegistrationID]='" & Registraionid & "' and APIName='" & APIName & "'"

                    If AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                        QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and  [RegistrationID] in (Select RegistrationId from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') and APIName='" & APIName & "' "
                    ElseIf AgentType.Trim.ToUpper = "Admin".Trim.ToUpper Or AgentType.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        QryStr = QryStr & "  update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and APIName='" & APIName & "' "
                    End If
                Else
                    QryStr = " INSERT INTO " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents ([AgentType],[RegistrationID],[APIName],[CommissionType],[Commission],[RecordDatetime],[UpdatedBy],[UpdatedOn])  VALUES('" & AgentType & "','" & Registraionid & "','" & APIName & "','" & CommissionType & "'," & Commission & ",getdate(),'" & Registraionid & "',getdate())"
                End If


                If GV.FL.DMLQueriesBulk(QryStr) = True Then
                    lblError.Text = "Commission Updated Successfully."
                    lblError.CssClass = "Successlabels"

                Else
                    lblError.Text = "Sorry !! Process Can't be Completed."
                    lblError.CssClass = "errorlabels"
                End If
            End If



            


            'INSERT INTO BOS_OperatorWiseCommission_Agents ([AgentType],[RegistrationID],[APIName],[Category],[Code],[OperatorName],[CommissionType],[Commission],[RecordDatetime],[UpdatedBy],[UpdatedOn]) VALUES()




            'If Not btndetails Is Nothing Then
            '    If Not btndetails.CommandName.Trim = "" Then
            '        If btndetails.CommandName = "BranchToday" Then
            '            Dim cc As String = btndetails.CommandArgument
            '        End If
            '    End If
            'End If
            'If e.CommandName = "Remove" Then
            '    '////// Deleting Selected Cookie .....
            '    If Request.Cookies.Count > 0 Then
            '        Dim lblItem_ID As Label
            '        lblItem_ID = CType(e.Item.FindControl("Item_Id"), Label)
            '        SelectedItemInfo = New HttpCookie(lblItem_ID.Text.Trim)
            '        SelectedItemInfo.Expires = DateTime.Now.AddDays(-12)
            '        Response.Cookies.Add(SelectedItemInfo)
            '        Response.Redirect("MyCart.aspx")
            '    End If
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub FillData()
        Try
            'txtBusBooking_APIAllow.Visible = True
            'txtRechargeAPIAllow.Visible = True
            'txtFlight_APIPerAllow.Visible = True
            'txtMoneyTransfer_APIAllow.Visible = True
            'txtGST_APIAllow.Visible = True
            'txtPAN_APIAllow.Visible = True
            'txtRail_APIAllow.Visible = True
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
                btnClear.Visible = False
                btnSave.Visible = False
                lblAllowComm.Visible = False
            Else
                lblCommssionHeading.Text = "Set Commission For Master Distributor"
            End If

            
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where RegistrationID='" & Registraionid & "' and AgentType='" & AgentType & "'  ") > 0 Then 'Change where condition according to Criteria 

                Dim CommDS As DataSet = New DataSet

                CommDS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APICommissionSettigs where  RegistrationID='" & Registraionid & "' and AgentType='" & AgentType & "'")

                If Not CommDS Is Nothing Then
                    If CommDS.Tables.Count > 0 Then
                        If CommDS.Tables(0).Rows.Count > 0 Then



                        End If
                    End If
                End If

            End If


        Catch ex As Exception

        End Try
    End Sub



End Class