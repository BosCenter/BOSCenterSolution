
Public Class BOS_CommissionChart_AD
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

    Public Sub Fill_Contain_Category_No()
        Try
            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)


            Dim qryStr As String = "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster where ProductType='API' and ActiveStatus='Active' and ContainCategory='No' order by Title"
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

                        Dim CommissionType_Col_Name, Commission_Val_Col_Name As String


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
                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

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



                            Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

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
                                comm = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

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



                            Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

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



                            ' Dim comm As String = GV.FL.AddInVar("Commission", " BosCenter_DB.dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from BosCenter_DB.dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")
                            Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & DS.Tables(0).Rows(i).Item("Title").ToString() & "' ")

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







                    ''Dim lblAllow As Label = DirectCast(ListView1.Items(i).FindControl("lblAllow"), Label)
                    ''Dim lblgetting As Label = DirectCast(ListView1.Items(i).FindControl("lblgetting"), Label)


                    'Dim ContainCategory As String = ""

                    'If Not IsDBNull(DS.Tables(0).Rows(i).Item("ContainCategory")) Then
                    '    If Not DS.Tables(0).Rows(i).Item("ContainCategory").ToString() = "" Then
                    '        ContainCategory = DS.Tables(0).Rows(i).Item("ContainCategory").ToString()
                    '    Else
                    '        ContainCategory = ""
                    '    End If
                    'Else
                    '    ContainCategory = ""
                    'End If

                    'If AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    '    lblAllowComm.Visible = False
                    '    txtRechargeAPIAllow.Visible = False
                    'Else
                    '    lblAllowComm.Visible = True
                    '    txtRechargeAPIAllow.Visible = True
                    'End If


                    'If Canchange.Trim.ToUpper = "Yes".Trim.ToUpper And ContainCategory.Trim.ToUpper = "Yes".Trim.ToUpper Then
                    '    txtRechargeAPIAllow.CssClass = "form-control"
                    '    txtRechargeAPIAllow.Enabled = True
                    'ElseIf Canchange.Trim.ToUpper = "NO".Trim.ToUpper And ContainCategory.Trim.ToUpper = "NO".Trim.ToUpper Then
                    '    txtRechargeAPIAllow.CssClass = "form-control"
                    '    txtRechargeAPIAllow.Enabled = False
                    'ElseIf Canchange.Trim.ToUpper = "Yes".Trim.ToUpper And ContainCategory.Trim.ToUpper = "NO".Trim.ToUpper Then
                    '    txtRechargeAPIAllow.CssClass = "form-control"
                    '    txtRechargeAPIAllow.Enabled = True
                    'ElseIf Canchange.Trim.ToUpper = "NO".Trim.ToUpper And ContainCategory.Trim.ToUpper = "Yes".Trim.ToUpper Then
                    '    txtRechargeAPIAllow.CssClass = "form-control"
                    '    txtRechargeAPIAllow.Enabled = False
                    'End If









                    'If Not IsDBNull(DS.Tables(0).Rows(i).Item("Title")) Then
                    '    If Not DS.Tables(0).Rows(i).Item("Title").ToString() = "" Then
                    '        If CommissionType.Trim = "Not Applicable".Trim Then
                    '            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( N?A )"
                    '        ElseIf CommissionType.Trim = "Amount".Trim Then
                    '            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( Amt )"
                    '        ElseIf CommissionType.Trim = "Percentage".Trim Then
                    '            lblAPIName.Text = DS.Tables(0).Rows(i).Item("Title").ToString() & " ( % )"
                    '        End If

                    '    Else
                    '        lblAPIName.Text = ""
                    '    End If
                    'Else
                    '    lblAPIName.Text = ""
                    'End If




                    ''If CommissionType = "Amount" Then
                    ''    lblAllow.Text = "Amt"
                    ''    lblgetting.Text = "Amt"
                    ''ElseIf CommissionType = "Percentage" Then
                    ''    lblAllow.Text = "%"
                    ''    lblgetting.Text = "%"
                    ''Else
                    ''    lblAllow.Text = "N/A"
                    ''    lblgetting.Text = "N/A"
                    ''End If




                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Fill_Slabwise_Commission()
        Try
            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            Dim comType, comAmt As String

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



            Dim grdQry As String = "select RID as 'SrNo', [APIName] as APIName, (convert(varchar,FromAmount)+'-'+convert(varchar,ToAmount)) as 'Slab', " & comType & " as CommissionType," & comAmt & " as Commission from    " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise  where APIName in (select distinct Title from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster where  ProductType='API' and ActiveStatus='Active' and  SlabApplicable='With Slab' )   order by FromAmount asc"
            GV.FL.AddInGridViewWithFieldName(grd_SlabwiseCommission, grdQry)
            If grd_SlabwiseCommission.Rows.Count > 0 Then
                GV.FL.showSerialnoOnGridView(grd_SlabwiseCommission, 0)
                Div_SlabwiseCommission.Visible = True
            Else
                Div_SlabwiseCommission.Visible = False
            End If




        Catch ex As Exception

        End Try
    End Sub


    Public Sub Fill_Contain_Category_Yes()
        Try

            Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim Registraionid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

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



            Dim grdQry As String = "select RID as 'SrNo',isnull((select top 1 CanChange from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission OC where APIName in (select distinct Title from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster where ProductType='API' and ActiveStatus='Active' and ContainCategory='Yes')"
            Dim grdDS As DataSet = New DataSet
            grdDS = GV.FL.OpenDsWithSelectQuery(grdQry)
            GridView1.DataSource = grdDS
            GridView1.DataBind()

            GV.FL.showSerialnoOnGridView(GridView1, 0)


            If GridView1.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    Dim lblCanChange As Label = DirectCast(GridView1.Rows(i).Cells(6).FindControl("lblCanChange"), Label)
                    Dim lblAPIName As Label = DirectCast(GridView1.Rows(i).Cells(6).FindControl("lblAPIName"), Label)
                    Dim Canchange As String = lblCanChange.Text
                    Dim APIName As String = lblAPIName.Text
                    Dim lnkUpdateCommission As LinkButton = DirectCast(GridView1.Rows(i).Cells(7).FindControl("lnkUpdateCommission"), LinkButton)
                    Dim txtAllowCommission As TextBox = DirectCast(GridView1.Rows(i).Cells(6).FindControl("txtAllowCommission"), TextBox)


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

                            Dim getCommission As Decimal = CDec(GV.parseString(GridView1.Rows(i).Cells(5).Text))

                            If getCommission <= 0 Then
                                txtAllowCommission.Visible = False
                                txtAllowCommission.CssClass = "form-control"

                                lnkUpdateCommission.Visible = False
                                lnkUpdateCommission.CssClass = "btn btn-primary"
                            Else

                                Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(1).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' ")

                                If comm.Trim = "" Then
                                    comm = "0"
                                End If
                                txtAllowCommission.Text = comm
                            End If



                        ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then


                            Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(1).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' ")

                            If comm.Trim = "" Then
                                comm = "0"
                            End If
                            GridView1.Rows(i).Cells(5).Text = comm

                            Dim getCommission As Decimal = CDec(GV.parseString(GridView1.Rows(i).Cells(5).Text))

                            If getCommission <= 0 Then
                                txtAllowCommission.Visible = False
                                txtAllowCommission.CssClass = "form-control"

                                lnkUpdateCommission.Visible = False
                                lnkUpdateCommission.CssClass = "btn btn-primary"
                            Else
                                Dim comm1 As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(1).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' ")

                                If comm1.Trim = "" Then
                                    comm1 = "0"
                                End If

                                txtAllowCommission.Text = comm1
                            End If

                        ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then

                            Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(1).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' ")

                            If comm.Trim = "" Then
                                comm = "0"
                            End If
                            GridView1.Rows(i).Cells(5).Text = comm
                        ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then

                            'Dim comm As String = GV.FL.AddInVar("Commission", " BosCenter_DB.dbo.BOS_OperatorWiseCommission_Agents where RegistrationID=(select top 1 RefrenceID from BosCenter_DB.dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & Registraionid & "'  ) and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(1).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' ")
                            Dim comm As String = GV.FL.AddInVar("Commission", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & GV.parseString(GridView1.Rows(i).Cells(1).Text) & "' and Code='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "' ")


                            If comm.Trim = "" Then
                                comm = "0"
                            End If
                            GridView1.Rows(i).Cells(5).Text = comm

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

                        GridView1.Rows(i).Cells(4).Text = grdDS.Tables(0).Rows(i).Item(CommissionType_Col_Name).ToString
                        GridView1.Rows(i).Cells(5).Text = grdDS.Tables(0).Rows(i).Item(Commission_Val_Col_Name).ToString



                    End If





                Next
            End If




            'Dumb_CommissionType


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'BOS_ProductServiceVsAdmin_SA


                lblAllowComm.Visible = False

                Fill_Contain_Category_No()
                Fill_Contain_Category_Yes()
                Fill_Slabwise_Commission()

                Dim AgentType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                ElseIf AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                ElseIf AgentType.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                ElseIf AgentType.Trim.ToUpper = "Customer".Trim.ToUpper Then
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

                If GV.FL.RecCount("" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "' and Category='" & APIName & "' and Code='" & APIName & "'  ") > 0 Then 'Change where condition according to Criteria 
                    QryStr = " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where [RegistrationID]='" & Registraionid & "' and APIName='" & APIName & "'  and Category='" & APIName & "' and Code='" & APIName & "' ;"

                    If AgentType.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                        QryStr = QryStr & " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and  [RegistrationID] in (Select RegistrationId from " & GV.DefaultDatabase.Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') and APIName='" & APIName & "'  and Category='" & APIName & "' and Code='" & APIName & "' "
                    ElseIf AgentType.Trim.ToUpper = "Admin".Trim.ToUpper Or AgentType.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        QryStr = QryStr & "  update " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and APIName='" & APIName & "'  and Category='" & APIName & "' and Code='" & APIName & "' "
                    End If

                Else
                    QryStr = " INSERT INTO " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents ([Category],[Code],[OperatorName],[AgentType],[RegistrationID],[APIName],[CommissionType],[Commission],[RecordDatetime],[UpdatedBy],[UpdatedOn])  VALUES('" & Category & "','" & Code & "','" & OperatorName & "','" & AgentType & "','" & Registraionid & "','" & APIName & "','" & CommissionType & "'," & Commission & ",getdate(),'" & Registraionid & "',getdate()) ;"
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

                If GV.FL.RecCount("" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents where RegistrationID='" & Registraionid & "' and APIName='" & APIName & "'  ") > 0 Then 'Change where condition according to Criteria 
                    QryStr = " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where [RegistrationID]='" & Registraionid & "' and APIName='" & APIName & "'"

                    If AgentType.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                        QryStr = QryStr & " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and  [RegistrationID] in (Select RegistrationId from " & GV.DefaultDatabase.Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RefrenceID='" & Registraionid & "') and APIName='" & APIName & "' "
                    ElseIf AgentType.Trim.ToUpper = "Admin".Trim.ToUpper Or AgentType.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        QryStr = QryStr & "  update " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents set CommissionType='" & CommissionType & "',[Commission]=" & Commission & ",[UpdatedBy]='" & Registraionid & "',UpdatedOn=getdate()  where Commission>" & Commission & " and APIName='" & APIName & "' "
                    End If
                Else
                    QryStr = " INSERT INTO " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_Agents ([AgentType],[RegistrationID],[APIName],[CommissionType],[Commission],[RecordDatetime],[UpdatedBy],[UpdatedOn])  VALUES('" & AgentType & "','" & Registraionid & "','" & APIName & "','" & CommissionType & "'," & Commission & ",getdate(),'" & Registraionid & "',getdate())"
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
                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APICommissionSettigs where RegistrationId in (Select RefrenceID from " & GV.DefaultDatabase.Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & Registraionid & "') ")
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Distributor" Then
                lblCommssionHeading.Text = "Set Commission For Retailer"
                DS = New DataSet
                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APICommissionSettigs where RegistrationId in (Select RefrenceID from " & GV.DefaultDatabase.Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & Registraionid & "') ")
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Retailer" Then
                lblCommssionHeading.Text = "Commission Chart"
                DS = New DataSet
                DS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APICommissionSettigs where RegistrationId in (Select RefrenceID from " & GV.DefaultDatabase.Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & Registraionid & "') ")
                btnClear.Visible = False
                btnSave.Visible = False
                lblAllowComm.Visible = False
            Else
                lblCommssionHeading.Text = "Set Commission For Master Distributor"
            End If


            If GV.FL.RecCount("" & GV.DefaultDatabase.Trim & ".dbo.BOS_APICommissionSettigs where RegistrationID='" & Registraionid & "' and AgentType='" & AgentType & "'  ") > 0 Then 'Change where condition according to Criteria 

                Dim CommDS As DataSet = New DataSet

                CommDS = GV.FL.OpenDsWithSelectQuery("select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APICommissionSettigs where  RegistrationID='" & Registraionid & "' and AgentType='" & AgentType & "'")

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