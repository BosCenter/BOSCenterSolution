Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp


Public Class BOS_RechargeAPI
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim VEmployeeType, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Public Sub reset_button_css()
        Try
            btnmobile.CssClass = "btn btn-danger mar_top10"
            btndth.CssClass = "btn btn-danger mar_top10"
            btnpostpaid.CssClass = "btn btn-danger mar_top10"
            btnelectricity.CssClass = "btn btn-danger mar_top10"
            btnbroadband.CssClass = "btn btn-danger mar_top10"
            btngas.CssClass = "btn btn-danger mar_top10"
            btnlandline.CssClass = "btn btn-danger mar_top10"
            btnwaterbill.CssClass = "btn btn-danger mar_top10"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            Dim LocalDS As New DataSet
            LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            Dim DataRows() As DataRow
            DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            If Not DataRows Is Nothing Then
                If DataRows.Count > 0 Then
                    For D As Integer = 0 To DataRows.Count - 1
                        If Not DataRows(D).Item("CanDelete") = True Then
                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where EmpType='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Deleted.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                btnok.Visible = False
                ModalPopupExtender1.Show()
            Else
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = " Are you sure you want to delete ?"
                btnCancel.Text = "Cancel"
                btnok.Visible = True
                ModalPopupExtender1.Show()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Clear()
            Bind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ApiResult As String = ""

        Try
            
            lblError.Text = ""
            lblError.CssClass = ""
            Dim RechargeAPI_Status As String = ""
            Dim RechargeAPI As String = ""
            If ddlGateway.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                RechargeAPI = "RechargeAPI_Status"
            ElseIf ddlGateway.SelectedValue.Trim.ToUpper = "Recharge-2".Trim.ToUpper Then
                RechargeAPI = "RechargeAPI_2_Status"
            End If
            '///// Start Check API  STATUS Super Admin Level 

            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")
            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! Recharge API Is Inactive At Company Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// End Check API  STATUS Super Admin Level  

            '///// Start Check API  STATUS System Settings 


            RechargeAPI_Status = ""
            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! Recharge API Is Inactive At Admin Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            RechargeAPI_Status = ""
            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 



            Dim VServiceType, Voperator, Cus_MobileNo, VCus_Amount, VCus_Payable, VOperatorCode As String
            VOperatorCode = ""
            VServiceType = lblSelectedService.Text.Trim
            If Not ddlOperators.SelectedIndex = 0 Then
                Voperator = ddlOperators.SelectedItem.Text
            Else
                lblError.Text = "Select Operators"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If Not ddlOperators.SelectedIndex = 0 Then
                VOperatorCode = ddlOperators.SelectedValue
            End If
            If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "PostPaid".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblError.Text = "Enter Mobile No"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            ElseIf lblSelectedService.Text.Trim.ToUpper = "landline".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblError.Text = "Enter Landline No"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

            ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "DTH".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "gas".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblError.Text = "Enter Consumer No"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            End If
            Cus_MobileNo = GV.parseString(txt_Mobile_CA_No.Text.Trim)
            If txtAmt.Text = "" Or txtAmt.Text = "0" Then
                lblError.Text = "Enter Amount"
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VCus_Amount = GV.parseString(txtAmt.Text.Trim)
            End If
            VCus_Payable = GV.parseString(txtPayableAmt.Text.Trim)
            If VCus_Amount = "" Then
                VCus_Amount = "0"
            End If
            Dim VNetAmount As Decimal = 0
            If txtNetAmount.Text = "" Then
                VNetAmount = 0
            Else
                VNetAmount = GV.parseString(txtNetAmount.Text.Trim)
            End If

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If


            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
            Else
                lblError.Text = "You Have Insufficient Balance."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If txtTransactionPin.Text = "" Then
                lblError.Text = "Please Enter Your Transaction Pin."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If



            '///// Check For API Balance - Start //////
            If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                lblError.Text = "Insufficient API Balance."
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// Check For API Balance - End //////



            'Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim TransPiNo As String = ""
            TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If TransPiNo.Trim = txtTransactionPin.Text.Trim Then
            Else
                lblError.Text = "Invalid transaction Pin"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"



            If btnSave.Text.Trim.ToUpper = "Proceed".Trim.ToUpper Then
                btnok.Text = "Yes"
                btnok.Visible = True
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Proceed ??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            Else

                btnok.Text = "Yes"
                btnok.Visible = True
                btnCancel.Attributes("Style") = ""
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Update??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            End If

            

        Catch ex As Exception
            lblError.Text = ApiResult
        End Try
    End Sub


    Private Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
      
        Dim ApiResult As String = ""

        Try
            If btnok.Text.Trim.ToUpper = "Ok".Trim.ToUpper Then
                Response.Redirect("BOS_RechargeAPI.aspx")
            End If




            lblError.Text = ""
            lblError.CssClass = ""
            
            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)


            Dim VServiceType, Voperator, Cus_MobileNo, VCus_Amount, VCus_Payable, VOperatorCode As String
            VOperatorCode = ""
            VServiceType = lblSelectedService.Text.Trim
            If Not ddlOperators.SelectedIndex = 0 Then
                Voperator = ddlOperators.SelectedItem.Text
            Else
                lblError.Text = "Select Operators"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If Not ddlOperators.SelectedIndex = 0 Then
                VOperatorCode = ddlOperators.SelectedValue
            End If
            If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "PostPaid".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblError.Text = "Enter Mobile No"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            ElseIf lblSelectedService.Text.Trim.ToUpper = "landline".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblError.Text = "Enter Landline No"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

            ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "DTH".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "gas".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblError.Text = "Enter Consumer No"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            End If
            Cus_MobileNo = GV.parseString(txt_Mobile_CA_No.Text.Trim)
            If txtAmt.Text = "" Or txtAmt.Text = "0" Then
                lblError.Text = "Enter Amount"
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VCus_Amount = GV.parseString(txtAmt.Text.Trim)
            End If
            VCus_Payable = GV.parseString(txtPayableAmt.Text.Trim)
            If VCus_Amount = "" Then
                VCus_Amount = "0"
            End If
            Dim VNetAmount As Decimal = 0
            If txtNetAmount.Text = "" Then
                VNetAmount = 0
            Else
                VNetAmount = GV.parseString(txtNetAmount.Text.Trim)
            End If

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If



            'Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
          


            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"
            System.Threading.Thread.Sleep(5000)

            If ddlGateway.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                'Gateway 1

                ' Doing Way 1 Start
                Dim TypeName As String = ""

                If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Postpaid".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "dth".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Then
                    TypeName = "RECH"
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "GAS".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                    TypeName = "BILLPAY"
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Landline".Trim.ToUpper Then
                    TypeName = "Landline"
                End If

                Dim objRechargeParameters As New PartnerRechargeRequest()
                objRechargeParameters.Amount = VCus_Amount
                objRechargeParameters.APIKey = APIKey
                objRechargeParameters.Number = Cus_MobileNo
                objRechargeParameters.OperatorCode = VOperatorCode
                objRechargeParameters.OpertareName = Voperator
                objRechargeParameters.OrderID = RetailerID
                objRechargeParameters.TypeName = TypeName
                objRechargeParameters.Gateway = "2"

                Dim ParameterList As String = Newtonsoft.Json.JsonConvert.SerializeObject(objRechargeParameters)
                ApiResult = ReadbyRestClient(Recharge_API_URL, ParameterList, RetailerID, group)


                '{"OperatorCode":"XX","OrderID":"XXXXXXXX","Number":"XXXXXXXXXX","Amount":0.0,
                '"TypeName":"XXXX","APIKey":"XXXXXXXX147852369021564789632032589712XXXXX","Gateway":"0"}
                '           Response
                '{"status":"0","Description":"Success","OrderID":"2227299","UserID":"10006"} 


                'Reading the json in string format

                Dim json1 As JObject = JObject.Parse(ApiResult)

                'Fetch data from data root 

                Dim Vstatus As String = ""
                Dim VTransId As String = json1.SelectToken("OrderID").ToString
                Dim Vurid As String = RetailerID
                Dim Vmobile As String = Cus_MobileNo
                Dim Vamount As String = VCus_Amount
                Dim VoperatorId As String = VOperatorCode
                Dim Verror_code As String = GV.parseString(json1.SelectToken("status").ToString)
                Dim Vservice As String = TypeName
                Dim Vbal As String = "0"
                Dim VcommissionBal As String = "0"
                Dim VresText As String = GV.parseString(json1.SelectToken("Description").ToString)
                Dim VbillAmount As String = "0"
                Dim VbillName As String = ""
                Dim VRecord_DateTime As String = "GetDate()"
                Dim VorderId As String = json1.SelectToken("UserID").ToString
                If json1.SelectToken("status").ToString.Trim = "1" Then
                    Vstatus = "Success"
                ElseIf json1.SelectToken("status").ToString.Trim = "2" Then
                    Vstatus = "Failed"
                ElseIf json1.SelectToken("status").ToString.Trim = "3" Then
                    Vstatus = "Processing"
                Else
                    Vstatus = json1.SelectToken("status").ToString.Trim
                End If



                ' Doing Way 1 End

                If lblSessionFlag.Text = 0 Then
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API (TransIpAddress,Gateway,Refund_Status,TransId,RetailerID,Recharge_ServiceType,Recharge_Operator,Recharge_MobileNo_CaNo,Recharge_Amount,Recharge_PayableAmount,Recharge_Date,API_orderId,API_status,API_TransId,API_urid,API_mobile,API_amount,API_operatorId,API_error_code,API_service,API_bal,API_commissionBal,API_resText,API_billAmount,API_billName,UpdatedBy,UpdatedOn) values ('" & GV.parseString(GV.GetIPAddress) & "','1','No','" & GV.parseString(lblTransId.Text.Trim) & "','" & RetailerID & "','" & VServiceType & "','" & Voperator & "','" & Cus_MobileNo & "','" & VCus_Amount & "','" & VCus_Amount & "'," & VUpdatedOn & ",'" & VorderId & "','" & Vstatus & "','" & VTransId & "','" & Vurid & "','" & Vmobile & "','" & Vamount & "','" & VoperatorId & "','" & Verror_code & "','" & Vservice & "','" & Vbal & "','" & VcommissionBal & "','" & VresText & "','" & VbillAmount & "','" & VbillName & "','" & VUpdatedBy & "'," & VUpdatedOn & ") ; "
                    QryStr = QryStr & " " & " insert into " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info (RecordDatetime,API_TransId,Recharge_TransId,API_status,API_resText,CompanyCode,DBName) values(getdate(),'" & VTransId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(Vstatus) & "','" & GV.parseString(VresText) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "','" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "') ; "


                    If Not Vstatus.Trim.ToUpper = "Failed".ToUpper Then
                        Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                        If GRP = "Retailer".ToUpper Then
                            'IF Type is of Retailer

                            RechargeCommision(lblSelectedService.Text.Trim, VOperatorCode, "Recharge")
                            If Not lblRID.Text = "" Then
                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim DisID_Com() As String = AAID(1).Split(":")
                                Dim SubDIsID_Com() As String = AAID(2).Split(":")
                                Dim RetailerID_Com() As String = AAID(3).Split(":")

                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim DisID As String = DisID_Com(0)
                                Dim DisCom As String = DisID_Com(1)
                                Dim SUBDisID As String = SubDIsID_Com(0)
                                Dim SUBDisCom As String = SubDIsID_Com(1)
                                Dim RTEID As String = RetailerID_Com(0)
                                Dim RTECom As String = RetailerID_Com(1)

                                Dim arrCanChange() As String = AAID(4).Split(":")
                                Dim vCanChange As String = arrCanChange(1)


                                If vCanChange.Trim.ToUpper = "YES" Then
                                    Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END


                                Else
                                    'Condition Where canChange is NO

                                    Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END



                                End If

                                

                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                End If

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End



                            Else

                                Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RetailerID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If


                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                            End If

                        ElseIf GRP = "Customer".ToUpper Then
                            'In case of Customer 

                            '//// Customer Commission Calculation - Start
                            RechargeCommision_Customer(lblSelectedService.Text.Trim, VOperatorCode, "Recharge")
                            If Not lblRID.Text = "" Then

                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim CustID_Com() As String = AAID(1).Split(":")
                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim CustID As String = CustID_Com(0)
                                Dim CustCom As String = CustID_Com(1)



                                Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                Dim CustTypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim CustTypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."


                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                '//// Customer Commission Calculation - Start
                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If CustCom > 0 Then
                                    V_Actual_Commission_Amt = CustCom
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    CustCom = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CustTypecommTo & "','" & CustTypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If


                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                                '//// Customer Commission Calculation - END


                            Else

                                Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                            End If

                        End If

                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        '//// Admin & Super Admin Commission Calculation - Start
                        If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                            '//// Admin Commission Calculation - Start
                            Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Result As String
                            Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                            If GV.parseString(txtAmt.Text.Trim) = "" Then
                                V_Amount = "0"
                            Else
                                V_Amount = txtAmt.Text.Trim
                            End If
                            V_OperatorCategory = lblSelectedService.Text.Trim
                            V_OperatorCode = VOperatorCode
                            V_APIName = "Recharge"
                            V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                            Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)

                            If Not GV.parseString(Result) = "" Then
                                Dim Result_Arry() As String = Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If

                            '//// Admin Commission Calculation - End

                            '//// Super Admin Commission Calculation - Start
                            Result = GV.Commision_Calculation_For_SuperAdmin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName)

                            If Not GV.parseString(Result) = "" Then
                                Dim Result_Arry() As String = Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Super Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            '//// Super Admin Commission Calculation - End
                        End If
                        '//// Admin & Super Admin Commission Calculation - End
                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


                    End If
                    If GV.FL.DMLQueriesBulk(QryStr) = True Then

                        lblDialogMsg.Text = "Record Saved Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnok.Text = "Ok"
                        btnok.Visible = True
                        btnCancel.Attributes("style") = "display:none"
                        Clear()
                        Bind()
                        ModalPopupExtender1.Show()

                    Else
                        lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                        lblDialogMsg.CssClass = "errorlabels"
                        btnCancel.Text = "Ok"
                        btnok.Visible = False
                        ModalPopupExtender1.Show()
                    End If

                End If




            ElseIf ddlGateway.SelectedValue.Trim.ToUpper = "Recharge-2".Trim.ToUpper Then
                'Gateway 2

                Dim partner_id, api_password, mobile_no, operator_code, amount, partner_request_id, circle, recharge_type, user_var1 As String
                'PARTNERID,api_password,operator_code,amount,partner_request_id,circle,user_var1,p1,p2,p3

                partner_id = "RKITAPI190212"
                api_password = "cg45ob8"
                mobile_no = Cus_MobileNo
                operator_code = VOperatorCode
                amount = VCus_Amount
                partner_request_id = GV.RandomTransactionPin()
                circle = GV.FL.AddInVar("CircleCode", "CRM_StateMaster where State_Name = (select State from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "')")
                recharge_type = "NORMAL"
                user_var1 = RetailerID
                'Member No:RKITAPI190212
                'Password:	5nrg7nrmz4
                'Api Password:	cg45ob8
                'Encryption Key:77bxjoceldz46lrm...





                Dim TypeName As String = ""
                Dim apistr As String = ""
                If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Then
                    TypeName = "RECH"
                    apistr = "https://rechargkit.biz/get/prepaid/mobile?partner_id=" & partner_id & "&api_password=" & api_password & "&mobile_no=" & mobile_no & "&operator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&circle=" & circle & "&recharge_type=NORMAL&user_var1=" & user_var1 & ""
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Postpaid".Trim.ToUpper Then
                    TypeName = "POSTPAID"
                    apistr = "https://rechargkit.biz/get/postpaid/mobile?partner_id=" & partner_id & "&api_password=" & api_password & "&mobile_no=" & mobile_no & "&operator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&circle=" & circle & "&recharge_type=NORMAL&user_var1=" & user_var1 & ""
                ElseIf lblSelectedService.Text.Trim.ToUpper = "dth".Trim.ToUpper Then
                    TypeName = "DTH"
                    apistr = "https://rechargkit.biz/get/dth?partner_id=" & partner_id & "&api_password=" & api_password & "&customer_id=" & mobile_no & "&opeator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&user_var1=" & user_var1 & ""
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Then
                    TypeName = "BROADBAND"
                    apistr = "https://rechargkit.biz/get/billpayment?partner_id=" & partner_id & "&api_password=" & api_password & "&operator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&user_var1=" & user_var1 & "&p1=" & mobile_no & ""
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Then
                    TypeName = "ELECTRICITY"
                    apistr = "https://rechargkit.biz/get/billpayment?partner_id=" & partner_id & "&api_password=" & api_password & "&operator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&user_var1=" & user_var1 & "&p1=" & mobile_no & ""
                ElseIf lblSelectedService.Text.Trim.ToUpper = "GAS".Trim.ToUpper Then
                    TypeName = "GAS"
                    apistr = "https://rechargkit.biz/get/billpayment?partner_id=" & partner_id & "&api_password=" & api_password & "&operator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&user_var1=" & user_var1 & "&p1=" & mobile_no & ""
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                    TypeName = "WATERBILL"
                    apistr = "https://rechargkit.biz/get/billpayment?partner_id=" & partner_id & "&api_password=" & api_password & "&operator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&user_var1=" & user_var1 & "&p1=" & mobile_no & ""
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Landline".Trim.ToUpper Then
                    TypeName = "LANDLINE"
                    apistr = "https://rechargkit.biz/get/billpayment?partner_id=" & partner_id & "&api_password=" & api_password & "&operator_code=" & operator_code & "&amount=" & amount & "&partner_request_id=" & partner_request_id & "&user_var1=" & user_var1 & "&p1=" & mobile_no & ""
                End If


                'Dim IPAddress As String = ""

                ApiResult = ReadbyRestClient_GATEWAY2(apistr, apistr, RetailerID, group)

                Dim json1 As JObject = JObject.Parse(ApiResult)

                'Fetch data from data root
                Dim ERROR1 As String = json1.SelectToken("ERROR").ToString
                Dim STATUS As String = json1.SelectToken("STATUS").ToString
                Dim ORDERID As String = json1.SelectToken("ORDERID").ToString
                Dim OPTRANSID As String = json1.SelectToken("OPTRANSID").ToString
                Dim PARTNERREQID As String = json1.SelectToken("PARTNERREQID").ToString
                Dim MESSAGE As String = json1.SelectToken("MESSAGE").ToString
                Dim USERVAR1 As String = json1.SelectToken("USERVAR1").ToString
                Dim USERVAR2 As String = json1.SelectToken("USERVAR2").ToString
                Dim USERVAR3 As String = json1.SelectToken("USERVAR3").ToString
                Dim COMMISSION As String = json1.SelectToken("COMMISSION").ToString





                Dim Vstatus As String = ""
                Dim VTransId As String = PARTNERREQID
                Dim Vurid As String = RetailerID
                Dim Vmobile As String = Cus_MobileNo
                Dim Vamount As String = VCus_Amount
                Dim VoperatorId As String = VOperatorCode
                Dim Verror_code As String = STATUS
                Dim Vservice As String = TypeName
                Dim Vbal As String = "0"
                Dim VcommissionBal As String = "0"
                Dim VresText As String = MESSAGE
                Dim VbillAmount As String = "0"
                Dim VbillName As String = ""
                Dim VRecord_DateTime As String = "GetDate()"
                Dim VorderId As String = ORDERID
                'If json1.SelectToken("status").ToString.Trim = "1" Then
                '    Vstatus = "Success"
                'ElseIf json1.SelectToken("status").ToString.Trim = "2" Then
                '    Vstatus = "Failed"
                'ElseIf json1.SelectToken("status").ToString.Trim = "3" Then
                '    Vstatus = "Processing"
                'Else
                '    Vstatus = json1.SelectToken("status").ToString.Trim
                'End If
                If MESSAGE.Trim.ToUpper = "Success".Trim.ToUpper Then
                    Vstatus = "Success"
                ElseIf MESSAGE.Trim.ToUpper = "pending".Trim.ToUpper Then
                    Vstatus = "Processing"
                Else
                    Vstatus = "Failed"
                End If


                ' Doing Way 1 End

                If lblSessionFlag.Text = 0 Then
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API (TransIpAddress,Gateway,Refund_Status,TransId,RetailerID,Recharge_ServiceType,Recharge_Operator,Recharge_MobileNo_CaNo,Recharge_Amount,Recharge_PayableAmount,Recharge_Date,API_orderId,API_status,API_TransId,API_urid,API_mobile,API_amount,API_operatorId,API_error_code,API_service,API_bal,API_commissionBal,API_resText,API_billAmount,API_billName,UpdatedBy,UpdatedOn) values ('" & GV.parseString(GV.GetIPAddress) & "','1','No','" & GV.parseString(lblTransId.Text.Trim) & "','" & RetailerID & "','" & VServiceType & "','" & Voperator & "','" & Cus_MobileNo & "','" & VCus_Amount & "','" & VCus_Amount & "'," & VUpdatedOn & ",'" & VorderId & "','" & Vstatus & "','" & VTransId & "','" & Vurid & "','" & Vmobile & "','" & Vamount & "','" & VoperatorId & "','" & Verror_code & "','" & Vservice & "','" & Vbal & "','" & VcommissionBal & "','" & VresText & "','" & VbillAmount & "','" & VbillName & "','" & VUpdatedBy & "'," & VUpdatedOn & ") ; "
                    QryStr = QryStr & " " & " insert into " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info (RecordDatetime,API_TransId,Recharge_TransId,API_status,API_resText,CompanyCode,DBName) values(getdate(),'" & VTransId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(Vstatus) & "','" & GV.parseString(VresText) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "','" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "') ; "


                    If Not Vstatus.Trim.ToUpper = "Failed".ToUpper Then
                        Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                        If GRP = "Retailer".ToUpper Then
                            'IF Type is of Retailer

                            RechargeCommision(lblSelectedService.Text.Trim, VOperatorCode, "Recharge-2")
                            If Not lblRID.Text = "" Then
                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim DisID_Com() As String = AAID(1).Split(":")
                                Dim SubDIsID_Com() As String = AAID(2).Split(":")
                                Dim RetailerID_Com() As String = AAID(3).Split(":")
                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim DisID As String = DisID_Com(0)
                                Dim DisCom As String = DisID_Com(1)
                                Dim SUBDisID As String = SubDIsID_Com(0)
                                Dim SUBDisCom As String = SubDIsID_Com(1)
                                Dim RTEID As String = RetailerID_Com(0)
                                Dim RTECom As String = RetailerID_Com(1)

                                Dim arrCanChange() As String = AAID(4).Split(":")
                                Dim vCanChange As String = arrCanChange(1)


                                If vCanChange.Trim.ToUpper = "YES" Then
                                    Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END


                                Else
                                    'vCanChange.Trim.ToUpper = "No"

                                    Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END


                                End If

                                

                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End



                            Else

                                ' Retailer
                                Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RetailerID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If


                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                            End If

                        ElseIf GRP = "Customer".ToUpper Then
                            'In case of Customer 

                            '//// Customer Commission Calculation - Start
                            RechargeCommision_Customer(lblSelectedService.Text.Trim, VOperatorCode, "Recharge-2")
                            If Not lblRID.Text = "" Then

                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim CustID_Com() As String = AAID(1).Split(":")
                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim CustID As String = CustID_Com(0)
                                Dim CustCom As String = CustID_Com(1)



                                Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                Dim CustTypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim CustTypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."


                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                '//// Customer Commission Calculation - Start
                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If CustCom > 0 Then
                                    V_Actual_Commission_Amt = CustCom
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    CustCom = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CustTypecommTo & "','" & CustTypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If


                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                                '//// Customer Commission Calculation - END


                            Else
                                Dim typeAmtForm As String = "Your Account is debited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txtAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','Admin','" & txtAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                            End If

                        End If

                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        '//// Admin & Super Admin Commission Calculation - Start
                        If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                            '//// Admin Commission Calculation - Start
                            Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Result As String
                            Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                            If GV.parseString(txtAmt.Text.Trim) = "" Then
                                V_Amount = "0"
                            Else
                                V_Amount = txtAmt.Text.Trim
                            End If
                            V_OperatorCategory = lblSelectedService.Text.Trim
                            V_OperatorCode = VOperatorCode
                            V_APIName = "Recharge-2"
                            V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                            Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)

                            If Not GV.parseString(Result) = "" Then
                                Dim Result_Arry() As String = Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If

                            '//// Admin Commission Calculation - End

                            '//// Super Admin Commission Calculation - Start
                            Result = GV.Commision_Calculation_For_SuperAdmin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName)

                            If Not GV.parseString(Result) = "" Then
                                Dim Result_Arry() As String = Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Super Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            '//// Super Admin Commission Calculation - End
                        End If
                        '//// Admin & Super Admin Commission Calculation - End
                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


                    End If
                    If GV.FL.DMLQueriesBulk(QryStr) = True Then

                        lblDialogMsg.Text = "Record Saved Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnok.Text = "Ok"
                        btnok.Visible = True
                        btnCancel.Attributes("style") = "display:none"
                        Clear()
                        Bind()
                        ModalPopupExtender1.Show()
                    Else
                        lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                        lblDialogMsg.CssClass = "errorlabels"
                        btnCancel.Text = "Ok"
                        btnok.Visible = False
                        ModalPopupExtender1.Show()
                    End If

                End If






                '{" & vbCrLf & "  "ERROR": 16," & vbCrLf & "  "STATUS": 3," & vbCrLf & "  "ORDERID": 51791389," & vbCrLf & "  "OPTRANSID": "RKIT51791389"," & vbCrLf & "  "PARTNERREQID": "113"," & vbCrLf & "  "MESSAGE": "pending"," & vbCrLf & "  "USERVAR1": "test"," & vbCrLf & "  "USERVAR2": ""," & vbCrLf & "  "USERVAR3": ""," & vbCrLf & "  "COMMISSION": "0.00"" & vbCrLf & "

                '{"ERROR":0,"STATUS":1,"ORDERID":51645247,"OPTRANSID":"ONR2101231816190530","PARTNERREQID":"634","MESSAGE":"Success","USERVAR1":"rte-554","USERVAR2":"","USERVAR3":"","COMMISSION":"0.3500"}
                '{"ERROR":16,"STATUS":3,"ORDERID":51645411,"OPTRANSID":"ONR2101231818190131","PARTNERREQID":"635","MESSAGE":"pending","USERVAR1":"rte-554","USERVAR2":"","USERVAR3":"","COMMISSION":"0.00"}
                '{"ERROR":16,"STATUS":3,"ORDERID":51645625,"OPTRANSID":"RKIT51645625","PARTNERREQID":"636","MESSAGE":"pending","USERVAR1":"rte-554","USERVAR2":"","USERVAR3":"","COMMISSION":"0.00"}
                '{"ERROR":0,"STATUS":1,"ORDERID":51645786,"OPTRANSID":"1236225710","PARTNERREQID":"640","MESSAGE":"Success","USERVAR1":"rte-554","USERVAR2":"","USERVAR3":"","COMMISSION":"0.3300"}

                'RECHARGE PREPAID
                'https://rechargkit.biz/get/prepaid/mobile?partner_id=xxxxx&api_password=xxxxx&mobile_no=xxxxxxxxxx&operator_code=x&amount=10&partner_request_id=xxxx&circle=xx&recharge_type=NORMAL&user_var1=xxx
                'POSTPAID

                '{"ERROR":0,"STATUS":1,"ORDERID":755058,"OPTRANSID":"497055187","PARTNERREQID":"rkittyest3","MESSAGE":"Success","USERVAR1":"101","USERVAR2":"","USERVAR3":"","COMMISSION":"0.00","CHARGE":"0.00"}

                'https://rechargkit.biz/get/postpaid/mobile?partner_id=xxxxx&api_password=xxxxx&mobile_no=xxxxxxxxxx&operator_code=xx&amount=10&partner_request_id=xxxx&circle=xx&recharge_type=NORMAL&user_var1=xxx

                'DTH Recharge
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20"}
                'https://rechargkit.biz/get/dth?partner_id=xxxx&api_password=xxxx&customer_id=1234567890&opeator_code=23&amount=10&partner_request_id=15&customer_email=user@xyz.com&user_var1=122


                'Electricity
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20","CHARGE":"5.00"}
                'https://rechargkit.biz/get/billpayment?partner_id=xxxx&api_password=xxxx&operator_code=xx&amount=10&partner_request_id=15&user_var1=123&p1=123456&p2=124&p3=1234

                'Gas
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20","CHARGE":"5.00"}
                'https://rechargkit.biz/get/billpayment?partner_id=xxxx&api_password=xxxx&operator_code=xx&amount=10&partner_request_id=15&user_var1=123&p1=123456&p2=124&p3=1234

                'Landline
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20","CHARGE":"5.00"}
                'https://rechargkit.biz/get/billpayment?partner_id=xxxx&api_password=xxxx&operator_code=xx&amount=10&partner_request_id=15&user_var1=123&p1=123456&p2=124&p3=1234

                'Water
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20","CHARGE":"5.00"}
                'https://rechargkit.biz/get/billpayment?partner_id=xxxx&api_password=xxxx&operator_code=xx&amount=10&partner_request_id=15&user_var1=123&p1=123456&p2=124&p3=1234

                'Broadband
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20","CHARGE":"5.00"}
                'https://rechargkit.biz/get/billpayment?partner_id=xxxx&api_password=xxxx&operator_code=xx&amount=10&partner_request_id=15&user_var1=123&p1=123456&p2=124&p3=1234



                'Life Insurance
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20","CHARGE":"5.00"}
                'https://rechargkit.biz/get/billpayment?partner_id=xxxx&api_password=xxxx&operator_code=xx&amount=10&partner_request_id=15&user_var1=123&p1=123456&p2=124&p3=1234

                'Datacard Recharge
                '{"ERROR":0,"STATUS":1,"ORDERID":753093,"OPTRANSID":"2107938600","PARTNERREQID":"rkittyeLst101","MESSAGE":"Success","USERVAR1":"1213","USERVAR2":"","USERVAR3":"","COMMISSION":"0.20"}
                'https://rechargkit.biz/get/prepaid/mobile?partner_id=xxxxx&api_password=xxxxx&mobile_no=xxxxxxxxxx&operator_code=x&amount=799&partner_request_id=xxxx&circle=xx&recharge_type=NORMAL&user_var1=xxx


                'Balance Check
                '{"ERROR":0,"MESSAGE":"Success","USERVAR1":"123","USERVAR2":"","USERVAR3":"","WALLET_BALANCE":"61.70","DMR_BALANCE":"0.00"}
                'https://rechargkit.biz/get/user/balance?partner_id=xxxx&api_password=xxx&user_var1=123




                Exit Sub


            End If







        Catch ex As Exception
            lblError.Text = ApiResult
        End Try


    End Sub



    Public Sub Bind()
        Try
            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim qry As String = ""

            'API_TransId as TransNo,

            If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Then
                qry = "Select (CONVERT(VARCHAR(11),Recharge_Date,106)) as  'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "dth".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Landline".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'TelephoneNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Postpaid".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "GAS".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            End If
            
            DS = GV.FL.OpenDsWithSelectQuery(qry)
            GridView1.DataSource = DS.Tables(0)
            GridView1.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            VEmployeeType = ""
            VUpdatedBy = ""
            VUpdatedOn = ""
            lblService.Text = ""
            txtNetAmount.Text = ""
            txtServiceCharge.Text = ""
            ddlOperators.SelectedIndex = 0
            txt_Mobile_CA_No.Text = ""
            txtAmt.Text = ""
            txtPayableAmt.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""
            lblSessionFlag.Text = 0
            btnSave.Text = "Proceed"
            lblError.Text = ""
            btnSave.Enabled = True

            lblError.CssClass = ""

            lblUpadate.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblSessionFlag.Text = 0

                If Not Session("Navigate") Is Nothing Then

                    Div_Navigation_buttons.Visible = False

                    'btnmobile.Visible = False
                    'btnpostpaid.Visible = False
                    'btndth.Visible = False
                    'btnbroadband.Visible = False
                    'btnelectricity.Visible = False
                    'btngas.Visible = False
                    'btnwaterbill.Visible = False
                    'btnlandline.Visible = False

                    If Session("Navigate") = "mobile" Then
                        add_Recharge_Operators(ddlOperators, "mobile")
                     
                    ElseIf Session("Navigate") = "Postpaid" Then
                        add_Recharge_Operators(ddlOperators, "Postpaid")


                    ElseIf Session("Navigate") = "dth" Then
                        add_Recharge_Operators(ddlOperators, "dth")


                    ElseIf Session("Navigate") = "Broadband" Then
                        add_Recharge_Operators(ddlOperators, "Broadband")


                    ElseIf Session("Navigate") = "Electricity" Then
                        add_Recharge_Operators(ddlOperators, "Electricity")


                    ElseIf Session("Navigate") = "gas" Then
                        add_Recharge_Operators(ddlOperators, "gas")


                    ElseIf Session("Navigate") = "waterbill" Then
                        add_Recharge_Operators(ddlOperators, "waterbill")


                    ElseIf Session("Navigate") = "Landline" Then
                        add_Recharge_Operators(ddlOperators, "Landline")


                    Else
                        add_Recharge_Operators(ddlOperators, "mobile")


                    End If

                Else
                    Div_Navigation_buttons.Visible = True

                    btnmobile_Click(sender, e)
                End If
                ddlGateway_SelectedIndexChanged(sender, e)

            End If
            lblWalletBal.Text = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            Dim LocalDS As New DataSet
            LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            Dim DataRows() As DataRow
            DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            If Not DataRows Is Nothing Then
                If DataRows.Count > 0 Then
                    For D As Integer = 0 To DataRows.Count - 1
                        If Not DataRows(D).Item("CanUpdate") = True Then

                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'lblRID.Text = GridView1.DataKeys(gvrow.RowIndex).Value.ToString()

            lblRID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where EmpType='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "';") > 0 Then
                lblDialogMsg.CssClass = ""
                lblDialogMsg.Text = "Can't be Updated.<b>" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text) & "</b>  is in Use."
                btnCancel.Text = "Ok"
                btnok.Visible = False
                ModalPopupExtender1.Show()
            Else
                lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(1).Text)
                ' Session("EditFlag") = 1
                lblSessionFlag.Text = 1
                btnSave.Text = "Update"

                lblError.CssClass = ""
                lblError.Text = ""
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


    Private Sub btnmobile_Click(sender As Object, e As System.EventArgs) Handles btnmobile.Click
        Try

            add_Recharge_Operators(ddlOperators, "mobile")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btndth_Click(sender As Object, e As System.EventArgs) Handles btndth.Click
        Try
            add_Recharge_Operators(ddlOperators, "dth")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnelectricity_Click(sender As Object, e As System.EventArgs) Handles btnelectricity.Click
        Try
            add_Recharge_Operators(ddlOperators, "Electricity")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnlandline_Click(sender As Object, e As System.EventArgs) Handles btnlandline.Click
        Try
            add_Recharge_Operators(ddlOperators, "Landline")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnpostpaid_Click(sender As Object, e As System.EventArgs) Handles btnpostpaid.Click
        Try
            add_Recharge_Operators(ddlOperators, "Postpaid")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnwaterbill_Click(sender As Object, e As System.EventArgs) Handles btnwaterbill.Click
        Try
            add_Recharge_Operators(ddlOperators, "waterbill")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btngas_Click(sender As Object, e As System.EventArgs) Handles btngas.Click
        Try
            add_Recharge_Operators(ddlOperators, "gas")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnbroadband_Click(sender As Object, e As System.EventArgs) Handles btnbroadband.Click
        Try
            add_Recharge_Operators(ddlOperators, "Broadband")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub add_Recharge_Operators(ByVal ddl As DropDownList, ByVal ServiceName As String)
        Try
            Clear()
            ddl.Items.Clear()
            'Dim OperatorList() As String
            Dim OperatorCount As Integer = 0
            reset_button_css()
            lblSelectedService.Text = ServiceName.Trim.ToUpper
            If ServiceName.Trim.ToUpper = "Mobile".ToUpper Then
                btnmobile.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " ::  Mobile Recharge ::"
                lbl_Service_History_Heading.Text = " Mobile Recharge History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter Mobile No"
                'OperatorList = {"AR:AIRTEL", "BS:BSNL", "ID:IDEA ", "VF:VODAFONE ", "RJ:RELIANCE JIO", "TI:TATA INDICOM", "TD:TATA DOCOMO", "AI:AIRCEL", "TE:TELENOR", "VG:VIRGIN GSM", "VC:VIRGIN CDMA", "MTS:MTS", "DL:TATA DOCOMO CDMA LANDLINE", "BR:BSNL VALIDITY/SPECIAL", "TB:DOCOMO GSM SPECIAL", "UN:UNINOR", "NS:UNINOR SPECIAL", "BSK:BSNL TOPUP (J&amp;K)", "BSJ:BSNL SPECIAL ( J&amp;K )", "JKI:J&amp;K ( IDEA EXPRESS )", "JKJ:JIO-JK"}

            ElseIf ServiceName.Trim.ToUpper = "dth".ToUpper Then
                btndth.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " ::  DTH Recharge ::"
                lbl_Service_History_Heading.Text = " DTH Recharge History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter CA No"

                'OperatorList = {"AD:AIRTEL DTH", "BT:BIG TV DTH", "DT:DISH TV DTH", "TS:TATA SKY DTH", "VD:VIDEOCON DTH", "ST:SUN TV DTH"}

            ElseIf ServiceName.Trim.ToUpper = "Landline".ToUpper Then
                btnlandline.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " ::  Landline Payment ::"
                lbl_Service_History_Heading.Text = " Landline Payment History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter Telephone No"

                'OperatorList = {"AL:AIRTEL LANDLINE", "BL:BSNL - CORPORATE", "ML:MTNL - MUMBAI", "BIL:BSNL - INDIVIDUAL"}


            ElseIf ServiceName.Trim.ToUpper = "Postpaid".ToUpper Then
                btnpostpaid.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " ::  Postpaid Payment ::"
                lbl_Service_History_Heading.Text = " Postpaid Payment History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter Mobile No"

                'OperatorList = {"AP:AIRTEL BILL", "BP:BSNL BILL", "IP:IDEA BILL ", "VP:VODAFONE BILL ", "RP:RELIANCE BILL", "TP:TATA BILL", "AIP:AIRCLE BILL"}


            ElseIf ServiceName.Trim.ToUpper = "Electricity".ToUpper Then
                btnelectricity.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " ::  Electricity Payment ::"
                lbl_Service_History_Heading.Text = " Electricity Payment History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter CA No"

                'OperatorList = {"PGE:PASCHIM GUJARAT VIJ COMPANY LIMITED PGVCL", "MGE:MADHYA GUJARAT VIJ COMPANY LIMITED (MGVCL)", "UGE:UTTAR GUJARAT VIJ COMPANY LIMITED (UGVCL)", "DGE:DAKSHIN GUJARAT VIJ COMPANY LIMITED (DGVCL)", "TAE:TORRENT POWER - AGRA", "MSE: MSEDC LIMITED", "REE:ADANI ELECTRICITY MUMBAI LTD", "BRE:BSES RAJDHANI POWER LIMITED", "BYE:BBSES YAMUNA POWER LIMITED", "BYE:BBSES YAMUNA POWER LIMITED", "NDE:TATA POWER-DELHI", "BME:BEST UNDERTAKING - MUMBAI", "NNE:NOIDA POWER COMPANY LIMITED", "TTE:TRIPURA STATE ELECTRICITY CORPORATION LTD", "MPE:MP PASCHIM KSHETRA VIDYUT VITARAN - INDORE", "JUE:JAMSHEDPUR UTILITIES AND SERVICES COMPANY LIMITED", "IBE:INDIA POWER CORPORATION LIMITED - BIHAR", "CCE:CHHATTISGARH STATE ELECTRICITY BOARD", "CWE: CALCUTTA ELECTRICITY SUPPLY LTD (CESC)", "BBE:BANGALORE ELECTRICITY SUPPLY COMPANY", "AAE:ASSAM POWER DISTRIBUTION COMPANY LTD RAPDR", "AVE:AJMER VIDYUT VITRAN NIGAM LIMITED (AVVNL)", "BEE:BHARATPUR ELECTRICITY SERVICES LTD. (BESL)", "BKE:BIKANER ELECTRICITY SUPPLY LIMITED (BKESL)", "DDE:DAMAN AND DIU ELECTRICITY", "DNE:DNH POWER DISTRIBUTION COMPANY LIMITED", "APE:APEPDCL-EASTERN POWER DISTRIBUTION CO AP LTD", "GEE:GULBARGA ELECTRICITY SUPPLY COMPANY LIMITED GESCOM", "IWE:INDIA POWER CORPORATION - WEST BENGAL", "JDE:JODHPUR VIDYUT VITRAN NIGAM LIMITED (JDVVNL)", "JIE:JAIPUR VIDYUT VITRAN NIGAM (JVVNL)", "KTE:KOTA ELECTRICITY DISTRIBUTION LIMITED (KEDL)", "MHE:MEGHALAYA POWER DIST CORP LTD", "MZE:MUZAFFARPUR VIDYUT VITRAN LIMITED", "NBE:NORTH BIHAR POWER DISTRIBUTION COMPANY LTD.", "NSE:NNESCO, ODISHA", "SBE:SOUTH BIHAR POWER DISTRIBUTION COMPANY LTD.", "SDE: SNDL NAGPUR", "STE:SOUTHCO, ODISHA", "ASE:APSPDCL-SOUTHERN POWER DISTRIBUTION CO AP LTD", "TME:TATA POWER - MUMBAI", "TNE:TAMIL NADU ELECTRICITY BOARD (TNEB)", "AJE:TP AJMER DISTRIBUTION LTD (TPADL)", "UKE:UTTARAKHAND POWER CORPORATION LIMITED", "UBE:UTTAR PRADESH POWER CORP LTD (UPPCL) - URBAN", "URE: UTTAR PRADESH POWER CORP LTD (UPPCL) - RURAL", "WSE:WESCO UTILITY", "DHE:DAKSHIN HARYANA BIJLI VITRAN NIGAM (DHBVN)", "PSE:PUNJAB STATE POWER CORPORATION LTD (PSPCL)", "HNE:HUBLI ELECTRICITY SUPPLY COMPANY LTD (HESCOM)", "UHE:UTTAR HARYANA BIJLI VITRAN NIGAM (UHBVN)", "CRE:CHAMUNDESHWARI ELECTRICITY SUPPLY CORP LTD (CESCOM", "HPE:HIMACHAL PRADESH STATE ELECTRICITY BOARD (HPSEB)", "JBL:JHARKHAND BIJLI VITRAN NIGAM LTD (JBVNL)", "WBE:WEST BENGAL STATE ELECTRICITY DISTRIBUTION CO. LTD", "THE:TORRENT POWER - AHMEDABAD", "TBE:TORRENT POWER - BHIWANDI", "TSE:TORRENT POWER - SURAT", "MRE:MP POORV KSHETRA VIDYUT VITARAN - RURAL", "MME:MP MADHYA KSHETRA VIDYUT VITARAN CO. LTD.-RURAL", "MUE:MP MADHYA KSHETRA VIDYUT VITARAN CO. LTD.-URBAN", "TLE:TELANGANA SOUTHERN POWER DISTRIBUTION CO LTD", "SPE:SIKKIM POWER - RURAL (SKMPWR)", "KNE:KANPUR ELECTRICITY SUPPLY COMPANY", "NME:NDMC ELECTRICITY", "GOE:GOA ELECTRICITY DEPARTMENT", "ANE:ASSAM POWER DISTRIBUTION COMPANY LTD (NON-RAPDR)", "MKE:M.P. POORV KSHETRA VIDYUT VITARAN - URBAN", "NNE:DEPARTMENT OF POWER - NAGALAND", "MSD:MESCOM - MANGALORE", "SUE:SIKKIM POWER (URBAN)", "COE:CESU - ODISHA", "KSE:KSEBL - KERALA", "PME:POWER &amp; ELECTRICITY DEPARTMENT - MIZORAM"}


            ElseIf ServiceName.Trim.ToUpper = "Broadband".ToUpper Then

                btnbroadband.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " :: Broadband Payment ::"
                lbl_Service_History_Heading.Text = " Broadband Payment History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter CA No"

                'OperatorList = {"AFB:ACT FIBERNET", "ABB:AIRTEL BROADBAND", "CBB:CONNECT BROADBAND", "HBB:HATHWAY BROADBAND", "NBB:NEXTRA BROADBAND", "SBB:SPECTRANET BROADBAND", "TBB:TIKONA BROADBAND", "TTB:TTN BROADBAND", "DBB:D VOIS COMMUNICATIONS", "ANB:ASIANET BROADBAND", "FBB:FUSIONNET WEB SERVICES", "CWB:COMWAY BROADBAND"}


            ElseIf ServiceName.Trim.ToUpper = "GAS".ToUpper Then
                btngas.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " ::  Gas Payment ::"
                lbl_Service_History_Heading.Text = " Gas Payment History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter CA No"


                'OperatorList = {"AG:TADANI GAS", "GGCL:GUJARAT GAS COMPANY LTD", "AVG:AAVANTIKA GAS", "CUG:CENTRAL UP GAS LIMITED", "CGG:CHAROTAR GAS SAHAKARI MANDALI", "HCG:HARYANA CITY GAS", "IAG:INDIANOIL - ADANI GAS", "IPG:INDRAPRASTHA GAS", "MMG:MAHANAGAR GAS", "MNG:MAHARASHTRA NATURAL GAS", "SGG:SABARMATI GAS", "SUG:SITI ENERGY", "TNG:TRIPURA NATURAL GAS", "UCG:UNIQUE CENTRAL PIPED GASES", "VGG:VADODARA GAS", "IRG:IRM ENERGY"}


            ElseIf ServiceName.Trim.ToUpper = "Waterbill".ToUpper Or ServiceName.Trim.ToUpper = "Water bill".ToUpper Then
                btnwaterbill.CssClass = "btn btn-success mar_top10"
                lbl_Service_Heading.Text = " ::  Waterbill Payment ::"
                lbl_Service_History_Heading.Text = " Waterbill Payment History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter CA No"
                If ddlGateway.SelectedValue.Trim = "Recharge-2" Then
                    ServiceName = "Water bill"
                End If
                'OperatorList = {"BKW:BANGALORE WATER SUPPLY AND SEWERAGE BOARD", "BMW:BHOPAL MUNICIPAL CORPORATION", "DDW:DELHI JAL BOARD", "GWW:GREATER WARANGAL MUNICIPAL CORPORATION", "GMW:GWALIOR MUNICIPAL CORPORATION", "HTW:HYDERABAD METROPOLITAN WATER SUPPLY AND SEWERAGE B", "IMW:INDORE MUNICIPAL CORPORATION", "JMW:JABALPUR MUNICIPAL CORPORATION", "JPW:MUNICIPAL CORPORATION JALANDHAR", "JWW:MUNICIPAL CORPORATION LUDHIANA - WATER", "HGW:MUNICIPAL CORPORATION OF GURUGRAM", "NDW:NEW DELHI MUNICIPAL COUNCIL (NDMC)", "PMW:PUNE MUNICIPAL CORPORATION", "SGW:SURAT MUNICIPAL CORPORATION", "UMW:UJJAIN NAGAR NIGAM - PHED", "RBW:URBAN IMPROVEMENT TRUST (UIT) - BHIWADI", "UUW:UTTARAKHAND JAL SANSTHAN", "SMW:SILVASSA MUNICIPAL COUNCIL"}
            End If

            'If Not OperatorList Is Nothing Then
            '    OperatorCount = OperatorList.Length - 1
            'Else
            '    OperatorCount = OperatorCount - 1
            'End If




            Dim recqry As String = "Select Code,OperatorName  from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission where APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and Category='" & GV.parseString(ServiceName.Trim) & "' order by OperatorName asc "
            Dim RecDS As DataSet = New DataSet

            RecDS = GV.FL.OpenDsWithSelectQuery(recqry)
            If RecDS.Tables(0).Rows.Count > 0 Then
                For R As Integer = 0 To RecDS.Tables(0).Rows.Count - 1
                    If Not IsDBNull(RecDS.Tables(0).Rows(R).Item("Code")) Then
                        If Not RecDS.Tables(0).Rows(R).Item("Code").ToString() = "" Then
                            Dim Code As String = RecDS.Tables(0).Rows(R).Item("Code").ToString().Trim
                            Dim OperatorName As String = RecDS.Tables(0).Rows(R).Item("OperatorName").ToString().Trim
                            ddl.Items.Add("")
                            ddl.Items(R).Value = Code
                            ddl.Items(R).Text = GV.parseString(OperatorName)
                        End If

                    End If
                Next
            End If




            'For i As Integer = 0 To OperatorCount
            '    Dim str() As String = OperatorList(i).Split(":")
            '    ddl.Items.Add("")
            '    ddl.Items(i).Value = str(0)
            '    ddl.Items(i).Text = GV.parseString(str(1))
            'Next

            If ddl.Items.Count > 0 Then
                ddl.Items.Insert(0, ":: Select Operator ::")
            Else
                ddl.Items.Add(":: Select Operator ::")
            End If

            ddl.SelectedIndex = 0


            Bind()


        Catch ex As Exception

        End Try
    End Sub

    Public Sub RechargeAPI()
        Try
            ' Doing Way 1 Start
            Dim IPAddress As String = ""
            'Reading the json in string format
            ' IPAddress = New System.Net.WebClient().DownloadString("http://vacsc.in/api/recharge.php?token=#token&mobile=#mobile&amount=#amount&opid=#opid&urid=#urid&opvalue1=#opvalue1&opvalue2=#opvalue2&opvalue3=#opvalue3")

            IPAddress = New System.Net.WebClient().DownloadString("https://rechargkit.biz/get/prepaid/mobile?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=#mobile&operator_code=x&amount=#amount&partner_request_id=xxxx&circle=xx&recharge_type=NORMAL&user_var1=xxx")

            Dim json1 As JObject = JObject.Parse(IPAddress)

            'Fetch data from data root 
            Dim orderId As String = json1.SelectToken("data").SelectToken("orderId").ToString
            Dim status As String = json1.SelectToken("data").SelectToken("status").ToString
            Dim TransId As String = json1.SelectToken("data").SelectToken("TransId").ToString
            Dim urid As String = json1.SelectToken("data").SelectToken("urid").ToString
            Dim mobile As String = json1.SelectToken("data").SelectToken("mobile").ToString
            Dim amount As String = json1.SelectToken("data").SelectToken("amount").ToString
            Dim operatorId As String = json1.SelectToken("data").SelectToken("operatorId").ToString
            Dim error_code As String = json1.SelectToken("data").SelectToken("error_code").ToString
            Dim service As String = json1.SelectToken("data").SelectToken("service").ToString
            Dim bal As String = json1.SelectToken("data").SelectToken("bal").ToString
            Dim commissionBal As String = json1.SelectToken("data").SelectToken("commissionBal").ToString
            Dim resText As String = json1.SelectToken("data").SelectToken("resText").ToString
            Dim billAmount As String = json1.SelectToken("data").SelectToken("billAmount").ToString
            Dim billName As String = json1.SelectToken("data").SelectToken("billName").ToString


            ' Doing Way 1 End
        Catch ex As Exception

        End Try
    End Sub
    Public Sub RechargeCommision(ByVal OperatorCategory As String, ByVal OperatorCode As String, ByVal gateway As String)
        Try

            Dim VCommissionType, VSub_Dis_CommissionType, VRetailer_CommissionType As String
            VCommissionType = ""
            VSub_Dis_CommissionType = ""
            VRetailer_CommissionType = ""
            Dim VCommission, VSub_Dis_Commission, VRetailer_Commission As Decimal
            VCommission = 0
            VSub_Dis_Commission = 0
            VRetailer_Commission = 0


            Dim VContainCategory, VCanChange As String
            VContainCategory = ""
            VCanChange = ""


            Dim VadminComAmt, DistributorComAmt, SubDIsComAmt, VRetailerComAmt As Decimal
            VadminComAmt = 0
            DistributorComAmt = 0
            SubDIsComAmt = 0
            VRetailerComAmt = 0



            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalDISAmt, VFinalSUBDISAmt, VFinalRETAILERAmt As Decimal
            Dim SubDisID As String = ""
            Dim DisID As String = ""
            Dim AdminID As String = ""
            Dim qry As String = ""
            SubDisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "'")
            DisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "'")
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='" & gateway & "' and ActiveStatus='Active'"
            DS = New DataSet
            DS = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not DS.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(DS.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not DS.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(DS.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If



                        If VContainCategory.Trim.ToUpper = "YES" Then

                            VCanChange = ""


                            '#EK
                            ' Select  isnull((select top 1 CanChange from BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from BOS_OperatorWiseCommission OC  where  APIName='Recharge' and 	Category='Mobile' and 	Code='AR'
                            qryStr = "Select  isnull((select top 1 CanChange from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission OC  where  APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "'"
                            DS = New DataSet
                            DS = GV.FL.OpenDsWithSelectQuery(qryStr)
                            If Not DS Is Nothing Then
                                If DS.Tables.Count > 0 Then
                                    If DS.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CanChange")) Then
                                            If Not DS.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                                VCanChange = GV.parseString(DS.Tables(0).Rows(0).Item("CanChange").ToString())
                                            End If
                                        End If

                                        If VCanChange.Trim.ToUpper = "YES" Then

                                            Dim Amount1 As String = GV.parseString(txtAmt.Text.Trim)
                                            If Amount1.Trim = "" Then
                                                Amount1 = "0"
                                            End If
                                            Dim Amount As Decimal = Amount1

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Dis_CommissionType")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Dis_CommissionType").ToString() = "" Then
                                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Dis_CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Dis_Commission")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Dis_Commission").ToString() = "" Then
                                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Dis_Commission").ToString())
                                                End If
                                            End If

                                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then

                                                DistributorComAmt = (VCommission)
                                            End If

                                            '/////// End Distributor

                                            qry = " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "' and   RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                                            qry = qry & " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "' and  RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




                                            DS = New DataSet
                                            DS = GV.FL.OpenDsWithSelectQuery(qry)
                                            If Not DS Is Nothing Then
                                                If DS.Tables.Count > 0 Then
                                                    If DS.Tables(0).Rows.Count > 0 Then


                                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CommissionType")) Then
                                                            If Not DS.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                                                VSub_Dis_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("CommissionType").ToString())
                                                            End If
                                                        End If

                                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Commission")) Then
                                                            If Not DS.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                                                VSub_Dis_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Commission").ToString())
                                                            End If
                                                        End If

                                                        If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                            SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                                        ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                            SubDIsComAmt = (VSub_Dis_Commission)
                                                        End If

                                                        '/////// End  Sub Distributor
                                                    End If
                                                    '/////// End  Sub Distributor

                                                    If DS.Tables.Count > 1 Then
                                                        If DS.Tables(1).Rows.Count > 0 Then

                                                            If Not IsDBNull(DS.Tables(1).Rows(0).Item("CommissionType")) Then
                                                                If Not DS.Tables(1).Rows(0).Item("CommissionType").ToString() = "" Then
                                                                    VRetailer_CommissionType = GV.parseString(DS.Tables(1).Rows(0).Item("CommissionType").ToString())
                                                                End If
                                                            End If

                                                            If Not IsDBNull(DS.Tables(1).Rows(0).Item("Commission")) Then
                                                                If Not DS.Tables(1).Rows(0).Item("Commission").ToString() = "" Then
                                                                    VRetailer_Commission = GV.parseString(DS.Tables(1).Rows(0).Item("Commission").ToString())
                                                                End If
                                                            End If

                                                            If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                                VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                                            ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                                VRetailerComAmt = (VRetailer_Commission)
                                                            End If

                                                            '/////// End  Retailer

                                                        End If
                                                    End If

                                                    '/////// End  Retailer

                                                End If
                                            End If



                                            VFinaladminAmt = VadminComAmt
                                            VFinalDISAmt = DistributorComAmt
                                            VFinalSUBDISAmt = SubDIsComAmt
                                            VFinalRETAILERAmt = VRetailerComAmt
                                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper







                                        ElseIf VCanChange.Trim.ToUpper = "NO" Then

                                            '/// NEED To CHANGE HERE EK

                                            Dim Amount1 As String = GV.parseString(txtAmt.Text.Trim)
                                            If Amount1.Trim = "" Then
                                                Amount1 = "0"
                                            End If
                                            Dim Amount As Decimal = Amount1

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Dis_CommissionType")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Dis_CommissionType").ToString() = "" Then
                                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Dis_CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Dis_Commission")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Dis_Commission").ToString() = "" Then
                                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Dis_Commission").ToString())
                                                End If
                                            End If

                                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                                DistributorComAmt = (VCommission)
                                            End If


                                            '/////// End Distributor



                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                                    VSub_Dis_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                                    VSub_Dis_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                                End If
                                            End If

                                            If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                            ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                SubDIsComAmt = (VSub_Dis_Commission)
                                            End If

                                            '/////// End  Sub Distributor




                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                                    VRetailer_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                                    VRetailer_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                                End If
                                            End If

                                            If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                            ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                VRetailerComAmt = (VRetailer_Commission)
                                            End If

                                            '/////// End  Retailer


                                            VFinaladminAmt = VadminComAmt
                                            VFinalDISAmt = DistributorComAmt
                                            VFinalSUBDISAmt = SubDIsComAmt
                                            VFinalRETAILERAmt = VRetailerComAmt
                                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper




                                        End If

                                    End If
                                End If
                            End If





                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                            Dim Amount1 As String = GV.parseString(txtAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Commission").ToString())
                                End If
                            End If

                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then

                                DistributorComAmt = (VCommission)
                            End If

                            '/////// End Distributor



                            qry = " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and  RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                            qry = qry & " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and  RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




                            DS = New DataSet
                            DS = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not DS Is Nothing Then
                                If DS.Tables.Count > 0 Then
                                    If DS.Tables(0).Rows.Count > 0 Then


                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CommissionType")) Then
                                            If Not DS.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                                VSub_Dis_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Commission")) Then
                                            If Not DS.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                                VSub_Dis_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Commission").ToString())
                                            End If
                                        End If

                                        If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                        ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SubDIsComAmt = (VSub_Dis_Commission)
                                        End If

                                        '/////// End  Sub Distributor
                                    End If
                                    '/////// End  Sub Distributor

                                    If DS.Tables.Count > 1 Then
                                        If DS.Tables(1).Rows.Count > 0 Then

                                            If Not IsDBNull(DS.Tables(1).Rows(0).Item("CommissionType")) Then
                                                If Not DS.Tables(1).Rows(0).Item("CommissionType").ToString() = "" Then
                                                    VRetailer_CommissionType = GV.parseString(DS.Tables(1).Rows(0).Item("CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(DS.Tables(1).Rows(0).Item("Commission")) Then
                                                If Not DS.Tables(1).Rows(0).Item("Commission").ToString() = "" Then
                                                    VRetailer_Commission = GV.parseString(DS.Tables(1).Rows(0).Item("Commission").ToString())
                                                End If
                                            End If

                                            If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                            ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                VRetailerComAmt = (VRetailer_Commission)
                                            End If

                                            '/////// End  Retailer

                                        End If
                                    End If

                                    '/////// End  Retailer

                                End If
                            End If



                            VFinaladminAmt = VadminComAmt
                            VFinalDISAmt = DistributorComAmt
                            VFinalSUBDISAmt = SubDIsComAmt
                            VFinalRETAILERAmt = VRetailerComAmt
                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper



                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                            '/// NEED To CHANGE HERE EK

                            Dim Amount1 As String = GV.parseString(txtAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                    VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                    VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Commission").ToString())
                                End If
                            End If

                            If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                            ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                DistributorComAmt = (VCommission)
                            End If


                            '/////// End Distributor



                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                    VSub_Dis_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                    VSub_Dis_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                End If
                            End If

                            If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                            ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                SubDIsComAmt = (VSub_Dis_Commission)
                            End If

                            '/////// End  Sub Distributor




                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                    VRetailer_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                    VRetailer_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                End If
                            End If

                            If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                            ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                VRetailerComAmt = (VRetailer_Commission)
                            End If

                            '/////// End  Retailer


                            VFinaladminAmt = VadminComAmt
                            VFinalDISAmt = DistributorComAmt
                            VFinalSUBDISAmt = SubDIsComAmt
                            VFinalRETAILERAmt = VRetailerComAmt
                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                        End If


                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////

        Catch ex As Exception

        End Try
    End Sub
    Public Sub RechargeCommision_Customer(ByVal OperatorCategory As String, ByVal OperatorCode As String, ByVal gateway As String)
        Try

            Dim VCommissionType, VCustomer_CommissionType As String
            VCommissionType = ""
            VCustomer_CommissionType = ""

            Dim VCommission, VCustomer_Commission As Decimal
            VCommission = 0


            VCustomer_Commission = 0


            Dim VContainCategory, VCanChange As String
            VContainCategory = ""
            VCanChange = ""


            Dim VadminComAmt, VCustomerComAmt As Decimal
            VadminComAmt = 0
            VCustomerComAmt = 0



            Dim CustomerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalCustomerAmt As Decimal
            VFinaladminAmt = 0
            VFinalCustomerAmt = 0

            Dim AdminID As String = ""
            Dim qry As String = ""
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='" & gateway & "' and ActiveStatus='Active'"
            DS = New DataSet
            DS = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not DS.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(DS.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not DS.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(DS.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If



                        If VContainCategory.Trim.ToUpper = "YES" Then

                            VCanChange = ""


                            '#EK
                            ' Select  isnull((select top 1 CanChange from BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from BOS_OperatorWiseCommission OC  where  APIName='Recharge' and 	Category='Mobile' and 	Code='AR'
                            qryStr = "Select  isnull((select top 1 CanChange from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission OC  where  APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "'"
                            DS = New DataSet
                            DS = GV.FL.OpenDsWithSelectQuery(qryStr)
                            If Not DS Is Nothing Then
                                If DS.Tables.Count > 0 Then
                                    If DS.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CanChange")) Then
                                            If Not DS.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                                VCanChange = GV.parseString(DS.Tables(0).Rows(0).Item("CanChange").ToString())
                                            End If
                                        End If

                                        If VCanChange.Trim.ToUpper = "NO" Then

                                            '/// NEED To CHANGE HERE EK

                                            Dim Amount1 As String = GV.parseString(txtAmt.Text.Trim)
                                            If Amount1.Trim = "" Then
                                                Amount1 = "0"
                                            End If
                                            Dim Amount As Decimal = Amount1

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                                    VCustomer_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                                If Not DS.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                                    VCustomer_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                                End If
                                            End If

                                            If VCustomer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                VCustomerComAmt = Math.Round(((Amount * VCustomer_Commission) / 100), 2)
                                            ElseIf VCustomer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                VCustomerComAmt = (VCustomer_Commission)
                                            End If

                                            '/////// End  Retailer


                                            VFinalCustomerAmt = VCustomerComAmt

                                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper




                                        End If

                                    End If
                                End If
                            End If





                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                            Dim Amount1 As String = GV.parseString(txtAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                    VCustomer_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                    VCustomer_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                End If
                            End If


                            If VCustomer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VCustomerComAmt = Math.Round(((Amount * VCustomer_Commission) / 100), 2)
                            ElseIf VCustomer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                VCustomerComAmt = (VCustomer_Commission)
                            End If

                            '/////// End Distributor

                            VFinalCustomerAmt = VCustomerComAmt


                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper



                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                            '/// NEED To CHANGE HERE EK

                            Dim Amount1 As String = GV.parseString(txtAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                    VCustomer_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                If Not DS.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                    VCustomer_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                End If
                            End If

                            If VCustomer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VCustomerComAmt = Math.Round(((Amount * VCustomer_Commission) / 100), 2)
                            ElseIf VCustomer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                VCustomerComAmt = (VCustomer_Commission)
                            End If

                            VFinalCustomerAmt = VCustomerComAmt
                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                        End If


                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////

        Catch ex As Exception

        End Try
    End Sub


  

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  ALL Recharge API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    '///  ALL Recharge API URL  - Start
    Dim Recharge_API_URL As String = "https://www.vacsc.com/API/AllRecharge"
    '///  ALL Recharge API URL  - End

    Public Function ReadbyRestClient(Urls As String, Parameter As String, VAgentID As String, VAgentType As String) As String

        Dim API_Name, Trans_ID, Trans_DateTime, Request_String, Response_String, AgentID, AgentType As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        AgentID = ""
        AgentType = ""

        Dim strQry As String = ""



        Dim str As String = ""
        Dim LogString As String = ""
        Try
            'Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", request, response)
            lblTransId.Text = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(lblTransId.Text) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine
            API_Name = "Recharge API"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = VAgentID
            AgentType = VAgentType


            Dim client = New RestClient(Urls)
            Dim request = New RestRequest(Method.POST)
            request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("content-type", "application/x-www-form-urlencoded")
            request.AddParameter("application/x-www-form-urlencoded", Parameter, ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim
            Response_String = GV.parseString(str)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("RECHARGE_API_LOG.txt"), True)

        Catch ex As Exception
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("RECHARGE_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function


    Public Function ReadbyRestClient_GATEWAY2(Urls As String, Parameter As String, VAgentID As String, VAgentType As String) As String

        Dim API_Name, Trans_ID, Trans_DateTime, Request_String, Response_String, AgentID, AgentType As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        AgentID = ""
        AgentType = ""

        Dim strQry As String = ""



        Dim str As String = ""
        Dim LogString As String = ""
        Try
            'Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", request, response)
            lblTransId.Text = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(lblTransId.Text) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine
            API_Name = "Recharge API-2"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = VAgentID
            AgentType = VAgentType

            str = New System.Net.WebClient().DownloadString(Urls)
           
            str = str.Trim
            Response_String = GV.parseString(str)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("RECHARGE_API_LOG.txt"), True)

        Catch ex As Exception
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("RECHARGE_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function



    Public Class PartnerRechargeRequest

        Dim VOperatorCode, VOpertareName, VOrderID, VNumber, VTypeName, VAPIKey, VGateway As String
        Dim VAmount As Double

        Public Property OperatorCode() As String
            Get
                Return VOperatorCode
            End Get
            Set(ByVal value As String)
                VOperatorCode = value
            End Set
        End Property
        Public Property OpertareName() As String
            Get
                Return VOpertareName
            End Get
            Set(ByVal value As String)
                VOpertareName = value
            End Set
        End Property
        Public Property OrderID() As String
            Get
                Return VOrderID
            End Get
            Set(ByVal value As String)
                VOrderID = value
            End Set
        End Property

        Public Property Number() As String
            Get
                Return VNumber
            End Get
            Set(ByVal value As String)
                VNumber = value
            End Set
        End Property

        Public Property Amount() As Double
            Get
                Return VAmount
            End Get
            Set(ByVal value As Double)
                VAmount = value
            End Set
        End Property

        Public Property TypeName() As String
            Get
                Return VTypeName
            End Get
            Set(ByVal value As String)
                VTypeName = value
            End Set
        End Property

        Public Property APIKey() As String
            Get
                Return VAPIKey
            End Get
            Set(ByVal value As String)
                VAPIKey = value
            End Set
        End Property
        Public Property Gateway() As String
            Get
                Return VGateway
            End Get
            Set(ByVal value As String)
                VGateway = value
            End Set
        End Property
    End Class
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  ALL Recharge API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    Protected Sub txtAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAmt.TextChanged
        Try
            txtPayableAmt.Text = ""
            lblService.Text = ""
            txtNetAmount.Text = ""
            lblService.Text = ""
            If Not txtAmt.Text.Trim = "" Then
                txtPayableAmt.Text = GV.parseString(txtAmt.Text.Trim)
            End If
            Dim NetAmount As Decimal = 0
            Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "'").Split(":")
            If Service.Length > 1 Then
                If Service(1).Trim = "Percentage" Then
                    lblService.Text = Service(0) & " %"
                    NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                ElseIf Service(1).Trim = "Amount" Then
                    lblService.Text = Service(0)
                    NetAmount = CDec(Service(0))
                ElseIf Service(1).Trim = "Not Applicable" Then
                    'ddlOperators
                    Service = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission where APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedValue.Trim) & "'").Split(":")
                    If Service.Length > 1 Then
                        If Service(1).Trim = "Percentage" Then
                            lblService.Text = Service(0) & " %"
                            NetAmount = (CDec(txtPayableAmt.Text.Trim) * CDec(Service(0))) / 100
                        ElseIf Service(1).Trim = "Amount" Then
                            lblService.Text = Service(0)
                            NetAmount = CDec(Service(0))
                        ElseIf Service(1).Trim = "Not Applicable" Then
                            If Service(0) = "" Then
                                Service(0) = "0"
                            End If

                            lblService.Text = Service(0)
                            NetAmount = CDec(Service(0))
                        End If
                    Else
                        If Service(0) = "" Then
                            Service(0) = "0"
                        End If
                        lblService.Text = Service(0)
                        NetAmount = CDec(Service(0))
                    End If
                End If
            End If

            txtServiceCharge.Text = NetAmount
            txtNetAmount.Text = CDec(GV.parseString(txtAmt.Text.Trim)) + NetAmount

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlGateway_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGateway.SelectedIndexChanged
        Try

            lblError_Gateway.Text = ""
            lblError_Gateway.CssClass = ""

            Dim RechargeAPI_Status As String = ""
            Dim RechargeAPI As String = ""
            If ddlGateway.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                RechargeAPI = "RechargeAPI_Status"
            ElseIf ddlGateway.SelectedValue.Trim.ToUpper = "Recharge-2".Trim.ToUpper Then
                RechargeAPI = "RechargeAPI_2_Status"
            End If
            '///// Start Check API  STATUS Super Admin Level 

            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")
            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Recharge API Is Inactive At Company Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// End Check API  STATUS Super Admin Level  

            '///// Start Check API  STATUS System Settings 


            RechargeAPI_Status = ""
            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Recharge API Is Inactive At Admin Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim ServiceName As String = lblSelectedService.Text

            If ServiceName.Trim.ToUpper = "Mobile".ToUpper Then
                btnmobile_Click(sender, e)
            ElseIf ServiceName.Trim.ToUpper = "dth".ToUpper Then
                btndth_Click(sender, e)
            ElseIf ServiceName.Trim.ToUpper = "Landline".ToUpper Then
                btnlandline_Click(sender, e)
            ElseIf ServiceName.Trim.ToUpper = "Postpaid".ToUpper Then
                btnpostpaid_Click(sender, e)
            ElseIf ServiceName.Trim.ToUpper = "Electricity".ToUpper Then
                btnelectricity_Click(sender, e)
            ElseIf ServiceName.Trim.ToUpper = "Broadband".ToUpper Then
                btnbroadband_Click(sender, e)
            ElseIf ServiceName.Trim.ToUpper = "GAS".ToUpper Then
                btngas_Click(sender, e)
            ElseIf ServiceName.Trim.ToUpper = "Waterbill".ToUpper Then
                btnwaterbill_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class