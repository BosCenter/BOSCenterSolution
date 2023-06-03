Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports InstamojoAPI



Public Class CreateCustomer
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("SUPERADMIN")
    '//// ----------------Variable Declaration  ----------------
    Dim VRegisterDate, VAgentType, VAgencyName, VFirstName, VEmailID, VDOB, VAlternateMobileNo, VPermanentAddress, VState, VAddharCardNo, VWebSite, VRegistrationId, VPanCardNumber, VMobileNo, VOfficeAddress, VCity, VLastName, VPincode, VBusinessType, VGSTNO As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim Request_Transaction_Id, Request_name, Request_email, Request_phone, Request_amount, Request_redirect_url, Request_CompanyCode, Request_Purpose, Request_AgentID, Request_TransID As String
    Dim Response_DateTime, Response_Payment_Id, Response_Payment_Status, Response_Id, Response_Transaction_Id, Response_Action_Taken As String

    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then

                formheading3.Text = "Signup Form"
                lblformsectionhead3.Text = "Registration Details"
                ddlAgentType.SelectedValue = "Customer"

                ddlAgentType.CssClass = "form-control"
                txtRefrenceID.Text = ""
                txtRefrenceType.Text = ""


                If Not Request.QueryString("admin") Is Nothing Then
                    If Not Request.QueryString("admin").Trim = "" Then

                        Dim DBName As String = GV.FL_AdminLogin.AddInVar("DatabaseName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & Request.QueryString("admin").Trim & "' and [Status]='Active' ")
                        If DBName.Trim = "" Then
                            txtCompanyCode.Text = "CMP1045"
                            txtDBName.Text = "BosCenter_DB"
                        Else
                            txtCompanyCode.Text = Request.QueryString("admin").Trim.ToUpper
                            txtDBName.Text = DBName.Trim
                        End If
                    Else
                        txtCompanyCode.Text = "CMP1045"
                        txtDBName.Text = "BosCenter_DB"
                    End If
                Else
                    txtCompanyCode.Text = "CMP1045"
                    txtDBName.Text = "BosCenter_DB"
                End If

                ddlAgentType.CssClass = "form-control"

                lblError.Text = ""
                lblError.CssClass = ""


                ddl_Ref_Code.Items.Clear()
                GV.FL.AddInDropDownListDistinct(ddl_Ref_Code, "Ref_Code", "" & txtDBName.Text & ".dbo.BOS_Ref_Code_Master order by Ref_Code ")
                If ddl_Ref_Code.Items.Count > 0 Then
                    ddl_Ref_Code.Items.Insert(0, ":::: Select Code ::::")
                Else
                    ddl_Ref_Code.Items.Add(":::: Select Code ::::")
                End If
                lblServiceCharge.Text = "0"

              
                Session("EditFlag") = 0
                Session("Workfor") = "Save"

                If txtDBName.Text.Trim.ToUpper = "BosCenter_DB".Trim.ToUpper Then
                    txtCompanyName.Text = "BOS Money"
                Else
                    Dim CompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " " & txtDBName.Text.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & txtCompanyCode.Text.Trim & "' ")
                    txtCompanyName.Text = GV.parseString(CompanyName.Trim)
                End If


                txtCompanyName.ReadOnly = True

                ddlAgentType_SelectedIndexChanged(sender, e)
                btnSave.Text = "Submit & Proceed"
                btnClear.Text = "Close"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("Default.aspx?admin=" & txtCompanyCode.Text.Trim) '/Change the name of form
            Else
                Clear()
                ddlAgentType_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Dim VCreditBalLimit As String = ""

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim isErrorFound As Boolean = False
            Dim isFocusApplied As Boolean = False
            lblError.Text = ""
            lblError.CssClass = ""

            'If txtRefrenceID.Text.Trim = "" Then
            '    txtRefrenceID.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtRefrenceID.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtRefrenceID.CssClass = "form-control"
            'End If

            If txtCompanyName.Text.Trim = "" Then
                txtCompanyName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtCompanyName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtCompanyName.CssClass = "form-control"
            End If
            If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                If ddl_Ref_Code.SelectedIndex = 0 Then
                    ddl_Ref_Code.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        ddl_Ref_Code.Focus()
                        isFocusApplied = True
                    End If
                Else
                    ddl_Ref_Code.CssClass = "form-control"
                End If
            Else
                If txtAgencyName.Text = "" Then
                    txtAgencyName.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtAgencyName.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtAgencyName.CssClass = "form-control"
                End If
            End If
          

            'If txtPanCardNumber.Text.Trim = "" Then
            '    txtPanCardNumber.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtPanCardNumber.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtPanCardNumber.CssClass = "form-control"
            'End If

            If txtFirstName.Text.Trim = "" Then
                txtFirstName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtFirstName.Focus()
                    isFocusApplied = True
                End If
            Else
                VFirstName = (GV.parseString(txtFirstName.Text.Trim) & " " & GV.parseString(txtLastName.Text.Trim)).ToString.Trim
                txtFirstName.CssClass = "form-control"
            End If

            If txtEmailID.Text.Trim = "" Then
                txtEmailID.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmailID.Focus()
                    isFocusApplied = True
                End If
            Else
                VEmailID = GV.parseString(txtEmailID.Text.Trim)
                txtEmailID.CssClass = "form-control"
            End If

            If txtMobileNo.Text.Trim = "" Then
                txtMobileNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtMobileNo.Focus()
                    isFocusApplied = True
                End If
            Else
                VMobileNo = GV.parseString(txtMobileNo.Text)
                txtMobileNo.CssClass = "form-control"
            End If

            'If txtPermanentAddress.Text.Trim = "" Then
            '    txtPermanentAddress.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtPermanentAddress.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtPermanentAddress.CssClass = "form-control"
            'End If

            'If ddlBussinessType.SelectedIndex = 0 Then
            '    ddlBussinessType.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        ddlBussinessType.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    ddlBussinessType.CssClass = "form-control"
            'End If

            'If txtOfficeAddress.Text.Trim = "" Then
            '    txtOfficeAddress.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtOfficeAddress.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtOfficeAddress.CssClass = "form-control"
            'End If

            'If GV.parseString(ddlState.SelectedIndex) = 0 Then
            '    ddlState.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        ddlState.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    ddlState.CssClass = "form-control"
            'End If

            'If GV.parseString(ddlDistrict.SelectedIndex) = 0 Then
            '    ddlDistrict.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        ddlDistrict.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    ddlDistrict.CssClass = "form-control"
            'End If


            'If txtCity.Text.Trim = "" Then
            '    txtCity.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtCity.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtCity.CssClass = "form-control"
            'End If


            'If txtPincode.Text.Trim = "" Then
            '    txtPincode.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtPincode.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtPincode.CssClass = "form-control"
            'End If
            'If txtAddharCardNo.Text.Trim = "" Then
            '    txtAddharCardNo.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtAddharCardNo.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtAddharCardNo.CssClass = "form-control"
            'End If


            If isErrorFound = True Then
                Exit Sub
            End If

            VAddharCardNo = ""

            Dim strQry, QryMsg As String

            If VAddharCardNo.Trim = "" Then
                strQry = "" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where ( MobileNo='" & GV.parseString(txtMobileNo.Text) & "') and ( AgentType='" & ddlAgentType.SelectedValue & "' ) "
                QryMsg = "Mobile Already Exists."
            Else
                strQry = "" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where ( MobileNo='" & GV.parseString(txtMobileNo.Text) & "' ' ) and ( AgentType='" & ddlAgentType.SelectedValue & "' ) "
                QryMsg = "Mobile Already Exists."
            End If


            If GV.FL.RecCount(strQry) > 0 Then 'Change where condition according to Criteria 
                lblError.Text = QryMsg
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If


            Dim objPaymentRequest As New PaymentOrder

            objPaymentRequest.name = VFirstName.Trim
            objPaymentRequest.email = VEmailID
            objPaymentRequest.phone = VMobileNo

            If objPaymentRequest.emailInvalid = True Then
                lblError.Text = "Invalid EmailID, Please update EmailID"
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If

            If objPaymentRequest.phoneInvalid = True Then
                lblError.Text = "Invalid Mobile No, Please update Mobile No."
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If

            If objPaymentRequest.nameInvalid = True Then
                lblError.Text = "Invalid Name, Please update Name."
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If

            'If objPaymentRequest.transactionIdInvalid = True Then
            '    lblError.Text = "Invalid TransactionID, Please Retry Again."
            '    lblError.CssClass = "errorlabels"
            '    btnSave.Focus()
            '    Exit Sub
            'End If

            'If Not GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Admin" Then
            '    If CDec(lblServiceCharge.Text.Trim) > 0 Then

            '        Dim holdAmt As String = "0"
            '        If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Retailer" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Customer" Then
            '            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            '            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & txtDBName.Text.Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            '            If holdAmt.Trim = "" Then
            '                holdAmt = "0"
            '            End If
            '        End If

            '        If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(lblServiceCharge.Text.Trim) Then
            '        Else
            '            lblError.Text = "You Have Insufficient Wallet Balance to Create Customer."
            '            lblError.CssClass = "errorlabels"
            '            Exit Sub
            '        End If
            '    End If
            'End If


            If btnSave.Text = "Submit & Proceed" Then
                Div_deInfo.Visible = False
                'btnPopupYes.Text = "Yes"
                'btnPopupYes.Visible = True
                'btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Submit??"
                lblDialogMsg.CssClass = ""

                ModalPopupExtender1.Hide()
                ModalPopupExtender3.Hide()
                ModalPopupExtender4.Hide()

                ModalPopupExtender1.Show()
            Else

            End If
        Catch ex As Exception
        End Try
    End Sub

    Dim VRefrenceID, VRefrenceType, VActiveStatus, VRef_Code As String
    Private Sub btnPopupYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopupYes.Click
        Try
            If Session("Workfor") = "Delete" Then
                If btnPopupYes.Text = "Ok" Then
                    If Session("EditFlag") = 1 Then
                        Response.Redirect("BOS_SearchCustomer.aspx") '// Change form Name
                    Else
                        Clear()
                        Exit Sub
                    End If
                End If
                Dim QryStr As String = ""
                QryStr = " delete from " & txtDBName.Text.Trim & ".dbo.BOS_BankDetails where RegistrationId = '" & txtRegistrationId.Text.Trim & "';"
                QryStr = QryStr & " " & "delete from " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RID=" & lblRID.Text.Trim & ";"

                If GV.FL.DMLQueries(QryStr) = True Then
                    lblDialogMsg.Text = "Record deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "Sorry !! Process Can't be Completed.."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnPopupYes.Text = "Ok"
                btnCancel.Attributes("Style") = "Display:None"
                ModalPopupExtender1.Show()
                Exit Sub
            End If
            If btnPopupYes.Text.Trim.ToUpper = "Ok".Trim.ToUpper Then
                If Session("EditFlag") = 1 Then
                    Response.Redirect("BOS_SearchCustomer.aspx") '// Change form Name
                Else
                    Clear()
                    Exit Sub
                End If
            End If
            lblError.Text = ""
            lblError.CssClass = ""
            VAgentType = GV.parseString(ddlAgentType.SelectedValue.Trim)
           



            If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                If Not ddl_Ref_Code.SelectedIndex = 0 Then
                    VRef_Code = GV.parseString(ddl_Ref_Code.SelectedValue.Trim)
                Else
                    VRef_Code = ""
                End If
                If Not txtCompanyName.Text.Trim = "" Then
                    VAgencyName = GV.parseString(txtCompanyName.Text.Trim)
                Else
                    VAgencyName = ""
                End If
            Else
                If Not txtAgencyName.Text.Trim = "" Then
                    VAgencyName = GV.parseString(txtAgencyName.Text.Trim)
                Else
                    VAgencyName = ""
                End If
            End If
          


            If Not txtRefrenceID.Text.Trim = "" Then
                VRefrenceID = GV.parseString(txtRefrenceID.Text.Trim)
            Else
                VRefrenceID = ""
            End If
            If Not txtRefrenceType.Text.Trim = "" Then
                VRefrenceType = GV.parseString(txtRefrenceType.Text.Trim)
            Else
                VRefrenceType = ""
            End If

            If Not txtFirstName.Text.Trim = "" Then
                VFirstName = GV.parseString(txtFirstName.Text.Trim)
            Else
                VFirstName = ""
            End If

            If Not txtEmailID.Text.Trim = "" Then
                VEmailID = GV.parseString(txtEmailID.Text.Trim)
            Else
                VEmailID = ""
            End If



            VDOB = ""
            VAlternateMobileNo = ""
            VPermanentAddress = ""
            VState = ""
            VAddharCardNo = ""
            VWebSite = ""
            VPanCardNumber = ""
            If Not txtRegistrationId.Text.Trim = "" Then
                VRegistrationId = GV.parseString(txtRegistrationId.Text.Trim)
            Else
                VRegistrationId = ""
            End If

            VOfficeAddress = ""
            If Not txtMobileNo.Text.Trim = "" Then
                VMobileNo = GV.parseString(txtMobileNo.Text.Trim)
            Else
                VMobileNo = ""
            End If
            VCity = ""
            VPincode = ""

            VBusinessType = ""

            If Not txtLastName.Text.Trim = "" Then
                VLastName = GV.parseString(txtLastName.Text.Trim)
            Else
                VLastName = ""
            End If

            VGSTNO = ""

            'VActiveStatus = GV.parseString(ddlStatus.SelectedValue.Trim)
            VActiveStatus = "Active"

            VCreditBalLimit = "0"

            VRegisterDate = Now.Date



            Dim VUpdatedBy, VUpdatedOn, VRecord_DateTime, Vpassword As String

            VUpdatedBy = "Admin"

            VUpdatedOn = "getdate()"

            VRecord_DateTime = "getDate()"
            Vpassword = GV.RandomPaswrd()
            Dim trnsactionpinNo As String = GV.RandomTransactionPin
            Dim EMPCode As String = ""

            'If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Sub Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
            'Else
            '    VRefrenceID = "ADMIN"
            '    VRefrenceType = "ADMIN"
            '    EMPCode = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            'End If

            Dim VDistrict As String
            VDistrict = ""
            Dim Encrypted_Pass As String = GV.convertToHashMD5(Vpassword.Trim)


            If Session("EditFlag") = 0 Then

                If GV.FL.RecCount("" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where RegistrationId='" & VRegistrationId & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If



                Dim APIStatus As String = ""

                APIStatus = "Inactive"

                Dim QryStr As String = "insert into " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration (Encrypted_Pass,HoldAmt,Ref_Code,District,AEPS_API_Status,RechargeAPI_Status,MoneyTransferAPI_Status,PANCardAPI_Status,EmpCode,ActiveStatus,TransactionPin,CreditBalnceLimit,RefrenceID,RefrenceType,AgentPassword,UpdatedBy,UpdatedOn,RecordDateTime,RegistrationDate,AgentType,AgencyName,FirstName,EmailID,DOB,AlternateMobileNo,PermanentAddress,State,AddharCardNo,WebSite,RegistrationId,PanCardNumber,MobileNo,OfficeAddress,City,LastName,Pincode,BusinessType,GSTNO) values('" & Encrypted_Pass & "' ,0,'" & VRef_Code & "','" & VDistrict & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & EMPCode & "','" & VActiveStatus & "','" & trnsactionpinNo & "','" & VCreditBalLimit & "','" & VRefrenceID & "','" & VRefrenceType & "','" & Vpassword & "','" & VUpdatedBy & "'," & VUpdatedOn & "," & VRecord_DateTime & ",'" & VRegisterDate & "', '" & VAgentType & "','" & VAgencyName & "','" & VFirstName & "','" & VEmailID & "','" & VDOB & "','" & VAlternateMobileNo & "','" & VPermanentAddress & "','" & VState & "','" & VAddharCardNo & "','" & VWebSite & "','" & VRegistrationId & "','" & VPanCardNumber & "','" & VMobileNo & "','" & VOfficeAddress & "','" & VCity & "','" & VLastName & "','" & VPincode & "','" & VBusinessType & "','" & VGSTNO & "' ) ;"
                
                If Not txtRefrenceType.Text.Trim.ToUpper = "Admin".Trim.ToUpper Then
                    Dim vTransID As String = GV.FL_AdminLogin.getAutoNumber("TransId")


                    Dim VCommissionType_ColName, VCommission_ColName As String
                    Dim CashBackComm As Decimal = 0
                    If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        If txtRefrenceType.Text.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                            VCommissionType_ColName = "Dis_CommissionType"
                            VCommission_ColName = "Dis_Commission"
                        ElseIf txtRefrenceType.Text.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                            VCommissionType_ColName = "Sub_Dis_CommissionType"
                            VCommission_ColName = "Sub_Dis_Commission"
                        ElseIf txtRefrenceType.Text.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                            VCommissionType_ColName = "Retailer_CommissionType"
                            VCommission_ColName = "Retailer_Commission"
                        ElseIf txtRefrenceType.Text.Trim.ToUpper = "Customer".Trim.ToUpper Then
                            VCommissionType_ColName = "Customer_CommissionType"
                            VCommission_ColName = "Customer_Commission"
                        Else
                            VCommissionType_ColName = "Customer_CommissionType"
                            VCommission_ColName = "Customer_Commission"
                        End If
                        DS = New DataSet
                        DS = GV.FL.OpenDsWithSelectQuery("select * from " & txtDBName.Text.Trim & "BOS_Ref_Code_Master where Ref_Code='" & GV.parseString(ddl_Ref_Code.SelectedValue) & "'")
                        If Not DS Is Nothing Then
                            If DS.Tables.Count > 0 Then
                                If DS.Tables(0).Rows.Count > 0 Then
                                    Dim VCommissionType, VCommission As String
                                    VCommissionType = ""
                                    VCommission = "0"

                                    If Not IsDBNull(DS.Tables(0).Rows(0).Item(VCommissionType_ColName)) Then
                                        If Not DS.Tables(0).Rows(0).Item(VCommissionType_ColName).ToString() = "" Then
                                            VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item(VCommissionType_ColName).ToString())
                                        End If
                                    End If

                                    If Not IsDBNull(DS.Tables(0).Rows(0).Item(VCommission_ColName)) Then
                                        If Not DS.Tables(0).Rows(0).Item(VCommission_ColName).ToString() = "" Then
                                            VCommission = GV.parseString(DS.Tables(0).Rows(0).Item(VCommission_ColName).ToString())
                                        End If
                                    End If

                                    If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                        CashBackComm = Math.Round(((lblServiceCharge.Text * VCommission) / 100), 2)
                                    ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                        CashBackComm = (VCommission)
                                    End If

                                End If
                            End If
                        End If
                    End If

                    
                End If

                If GV.FL.RecCount("" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where RegistrationId='" & VRegistrationId & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                If GV.FL.DMLQueriesBulk(QryStr) = True Then

                    Dim message As String = "Dear " & VAgentType & "- Your registration with BOS has been done successfully."
                    message = message & " Your" & " Id: " & VRegistrationId
                    message = message & " Password: " & Vpassword
                    message = message & " PinNo: " & trnsactionpinNo


                    message = message & " Plz check your mail. For further assistance feel free to contact us at 8181898901."
                    GV.sendSMSThroughAPI(VMobileNo, message)

                    message = " Dear " & VAgentType & "," & "<br /><br /><br />"
                    message = message & " Your registration with BOS has been done successfully." & "<br /><br />"
                    message = message & "<b> Your" & " Id: " & VRegistrationId & "</b>" & "<br />"
                    message = message & "<b> Password: " & Vpassword & "</b>" & "<br />"
                    message = message & "<b> PinNo: " & trnsactionpinNo & "</b>" & "<br /><br /><br />"
                    'If Not GV.parseString(ddlAgentType.SelectedValue).Trim.ToUpper = "Retailer".Trim.ToUpper Then

                    'End If

                    message = message & " For further assistance feel free to contact us at 8181898901." & "<br /><br />"
                    message = message & " Regards, " & "<br />"
                    message = message & " TEAM BOS "

                    GV.sendNewMail(message, "New Account With BOS", VEmailID)
                    TD_Pin.Visible = True
                    Div_deInfo.Visible = True
                    lblClientID.Text = VRegistrationId
                    lblPassword.Text = Vpassword
                    lblTransactionPin.Text = trnsactionpinNo
                    Request_CompanyCode = txtCompanyCode.Text.Trim
                    Request_AgentID = VRegistrationId
                    If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then

                        Dim redirectUrl As String = GV.FL_AdminLogin.AddInVar("instamojo_Pay_Link", " " & txtDBName.Text.Trim & ".dbo.BOS_Ref_Code_Master where Ref_Code='" & ddl_Ref_Code.SelectedValue & "'  ")
                        Dim NewURL As String = ""


                        Try
                            Dim client_id, client_secret, endpoint, auth_endpoint As String

                            auth_endpoint = "https://www.instamojo.com/oauth2/token/" '"https://test.instamojo.com/oauth2/token/" 
                            endpoint = "https://api.instamojo.com/v2/" '"https://test.instamojo.com/v2/" '
                            client_id = "dKbCrLvyPMRNTKKhzknozPjK5br6e0G2Z9Uu9QxS"
                            client_secret = "hBriZIe7dE7MfkgqALkzdfxTvJkpC2x8g4xk5rsXPtejlDrF6FVP27Crj5DCwXKwvGcDcJXTmRnhNJSal8KwSQzbrEMpEbVnlw42GjOjYLzW9eopagwrtXCOPA5LrPJE"

                            Dim objClass As Instamojo = InstamojoImplementation.getApi(client_id, client_secret, endpoint, auth_endpoint)
                            Dim objPaymentRequest As New PaymentOrder



                            'PaymentOrderListResponse objPaymentRequestStatusResponse =  objClass.getPaymentOrderList(objPaymentOrderListRequest);

                            Request_TransID = GV.FL_AdminLogin.getAutoNumber("TransId")
                            Dim New_transId As String = txtCompanyCode.Text & "_" & "CC" & "_" & VRegistrationId & "_" & Request_TransID

                            Request_name = VFirstName.Trim & " " & VLastName.Trim
                            Request_email = VEmailID
                            Request_phone = VMobileNo
                            Request_amount = lblServiceCharge.Text
                            Request_Transaction_Id = New_transId
                            Request_Purpose = "CC"
                            Request_redirect_url = "https://www.boscenter.in/InstaResponseHandler.aspx"

                            objPaymentRequest.name = Request_name
                            objPaymentRequest.email = Request_email
                            objPaymentRequest.phone = Request_phone
                            objPaymentRequest.amount = Request_amount
                            objPaymentRequest.transaction_id = Request_Transaction_Id
                            objPaymentRequest.redirect_url = Request_redirect_url
                            objPaymentRequest.description = VRef_Code



                            '//webhook is optional.
                            'objPaymentRequest.webhook_url = "https://www.boscenter.in/InstaResponseHandler.aspx"

                            Try

                                Dim objPaymentResponse As CreatePaymentOrderResponse = objClass.createNewPaymentRequest(objPaymentRequest)
                                NewURL = objPaymentResponse.payment_options.payment_url

                            Catch ex As Exception

                            End Try

                        Catch ex As Exception

                        End Try
                        If Not NewURL.Trim = "" Then
                            Dim str As String = " insert into " & txtDBName.Text.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details (TransIpAddress,Reference_Id,Reference_Type,Ref_Plan_Code,Request_DateTime,Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,Request_redirect_url,Request_CompanyCode,Request_Purpose,Request_AgentID,Request_TransID,UpdatedOn,UpdatedBy) values ('" & GV.parseString(GV.GetIPAddress) & "','" & VRefrenceID & "','" & VRefrenceType & "','" & VRef_Code & "',getdate(),'" & Request_Transaction_Id & "','" & Request_name & "','" & Request_email & "','" & Request_phone & "','" & Request_amount & "','" & Request_redirect_url & "','" & Request_CompanyCode & "','" & Request_Purpose & "','" & Request_AgentID & "','" & Request_TransID & "',getdate(),'Admin')"
                            GV.FL.DMLQueriesBulk(str)

                            Response.Redirect(NewURL)
                        Else
                            If Not GV.parseString(redirectUrl.Trim) = "" Then
                                If GV.parseString(redirectUrl.Trim).Contains("https") Then

                                    Dim str As String = " insert into " & txtDBName.Text.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details (TransIpAddress,Reference_Id,Reference_Type,Ref_Plan_Code,Request_DateTime,Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,Request_redirect_url,Request_CompanyCode,Request_Purpose,Request_AgentID,Request_TransID,UpdatedOn,UpdatedBy) values ('" & GV.parseString(GV.GetIPAddress) & "','" & VRefrenceID & "','" & VRefrenceType & "','" & VRef_Code & "',getdate(),'" & Request_Transaction_Id & "','" & Request_name & "','" & Request_email & "','" & Request_phone & "','" & Request_amount & "','" & Request_redirect_url & "','" & Request_CompanyCode & "','" & Request_Purpose & "','" & Request_AgentID & "','" & Request_TransID & "',getdate(),'Admin')"
                                    GV.FL.DMLQueriesBulk(str)

                                    Response.Redirect(redirectUrl)
                                End If
                            End If
                        End If
                        Clear()
                        ddl_Ref_Code.SelectedIndex = 0
                        ddl_Ref_Code_SelectedIndexChanged(sender, e)
                        ddlAgentType_SelectedIndexChanged(sender, e)
                        ModalPopupExtender1.Hide()
                        ModalPopupExtender3.Hide()
                        ModalPopupExtender4.Hide()

                    Else
                        
                        Clear()
                        ddlAgentType_SelectedIndexChanged(sender, e)
                        lblDialogMsg.Text = "Record Saved Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnCancel.Text = "Ok"
                        btnPopupYes.Visible = False
                        ModalPopupExtender1.Show()
                    End If
                   


                Else
                    lblDialogMsg.Text = "Record Insertion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Visible = False
                    ModalPopupExtender1.Show()
                End If


            End If



        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            VAgentType = ""
            VAgencyName = ""
            VFirstName = ""
            VEmailID = ""
            VDOB = ""
            VAlternateMobileNo = ""
            VPermanentAddress = ""
            VState = ""
            VAddharCardNo = ""
            VWebSite = ""
            VRegistrationId = ""
            VPanCardNumber = ""
            VMobileNo = ""
            VOfficeAddress = ""
            VCity = ""
            VLastName = ""
            VPincode = ""
            VBusinessType = ""
            VGSTNO = ""
            txtAgencyName.Text = ""
            txtCompanyName.Text = ""

            txtFirstName.Text = ""

            txtEmailID.Text = ""



            txtMobileNo.Text = ""



            txtLastName.Text = ""



            If txtDBName.Text.Trim.ToUpper = "BosCenter_DB".Trim.ToUpper Then
                txtCompanyName.Text = "BOS Money"
            Else
                Dim CompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " " & txtDBName.Text.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & txtCompanyCode.Text.Trim & "'  ")
                txtCompanyName.Text = GV.parseString(CompanyName.Trim)
            End If

            txtRefrenceID.Text = ""
            lblRefId_Error.Text = ""
            txtRefrenceType.Text = ""




            txtCompanyName.CssClass = "form-control"
            txtFirstName.CssClass = "form-control"
            txtEmailID.CssClass = "form-control"

            txtMobileNo.CssClass = "form-control"

            txtLastName.CssClass = "form-control"


            Session("EditFlag") = 0
            btnSave.Text = "Submit & Proceed"
            btnClear.Text = "Close"

            lblError.Text = ""
            btnSave.Enabled = True
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub ddlAgentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgentType.SelectedIndexChanged
        Try

            If ddlAgentType.SelectedValue.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("Distributor_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("DistributorID", txtDBName.Text.Trim)
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("SubDistributor_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("SubDistributorID", txtDBName.Text.Trim)
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                'Response.Redirect("CreateAgent.aspx?admin=" & txtCompanyCode.Text.Trim)
                txtRegistrationId.Text = GV.FL.AddInVar("Retailer_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("RetailerId", txtDBName.Text.Trim)
                lblRef_AgencyName.Text = "Agency Name"
                txtAgencyName.Visible = True
                ddl_Ref_Code.Visible = False
                txtAgencyName.CssClass = "form-control"
                txtRefrenceID.Text = GV.FL.AddInVar("Sub_DistributorID", "" & txtDBName.Text.Trim & ".dbo.AutoNumber")
                txtRefrenceType.Text = "Distributor"
                txtRefrenceID.Enabled = False
                txtRefrenceID.CssClass = "form-control"
                lblRefId_Error.Text = ""
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("Customer_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("CustomerID", txtDBName.Text.Trim)
                lblRef_AgencyName.Text = "Reference Code"
                txtAgencyName.Visible = False
                ddl_Ref_Code.Visible = True
                ddl_Ref_Code.CssClass = "form-control"

                txtRefrenceType.Text = ""
                txtRefrenceID.Text = ""
                txtRefrenceID.Enabled = True
                txtRefrenceID.CssClass = "form-control"
            End If

        Catch ex As Exception

        End Try
    End Sub

 


    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            
            ModalPopupExtender4.Show()
        Catch ex As Exception

        End Try
    End Sub
   
    Protected Sub btnDelOk_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If Not lblRowIndex.Text.Trim = "" Then


                lblAlertDelPer.Text = ""
                lblDelDialogMsg.Text = "Record deleted Successfully."
                lblDelDialogMsg.CssClass = "Successlabels"
                btnDelCancel.Text = "OK"
                btnDelOk.Visible = False
                DeleteModalPopupExtender.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub
    

    

    
    Protected Sub ddl_Ref_Code_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Ref_Code.SelectedIndexChanged
        Try
            If ddl_Ref_Code.SelectedIndex = 0 Then
                lblServiceCharge.Text = "0"
            Else
                lblServiceCharge.Text = GV.FL.AddInVar("AmtToDebit", "" & txtDBName.Text.Trim & ".dbo.BOS_Ref_Code_Master where Ref_Code='" & GV.parseString(ddl_Ref_Code.SelectedValue) & "'")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtRefrenceID_TextChanged(sender As Object, e As EventArgs) Handles txtRefrenceID.TextChanged
        Try

            lblRefId_Error.Text = ""
            lblRefId_Error.CssClass = ""


            DS = GV.FL.OpenDs("" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where ActiveStatus='Active'  and RegistrationId='" & txtRefrenceID.Text.Trim & "' ")
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("AgentType")) Then
                            If Not DS.Tables(0).Rows(0).Item("AgentType").ToString() = "" Then
                                txtRefrenceType.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AgentType").ToString())
                            Else
                                txtRefrenceType.Text = ""
                            End If
                        Else
                            txtRefrenceType.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("FirstName")) Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("LastName")) Then
                                lblRefId_Error.Text = DS.Tables(0).Rows(0).Item("FirstName").ToString.ToUpper & " " & DS.Tables(0).Rows(0).Item("LastName").ToString.ToUpper
                                'lblRefId_Error.CssClass = "successlabels"
                                lblRefId_Error.ForeColor = Color.Blue

                            End If
                        End If


                       

                    Else
                        lblRefId_Error.Text = "Invalid Refer ID".ToString.ToUpper
                        'lblRefId_Error.CssClass = "errorlabels"
                        lblRefId_Error.ForeColor = Color.Red
                        txtRefrenceID.Text = ""
                        txtRefrenceType.Text = ""

                    End If

                Else
                    lblRefId_Error.Text = "Invalid Refer ID".ToString.ToUpper
                    'lblRefId_Error.CssClass = "errorlabels"
                    lblRefId_Error.ForeColor = Color.Red
                    txtRefrenceID.Text = ""
                    txtRefrenceType.Text = ""
                End If
            Else
                lblRefId_Error.Text = "Invalid Refer ID".ToString.ToUpper
                'lblRefId_Error.CssClass = "errorlabels"
                lblRefId_Error.ForeColor = Color.Red

                txtRefrenceID.Text = ""
                txtRefrenceType.Text = ""
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            ModalPopupExtender1.Hide()
        Catch ex As Exception

        End Try
    End Sub
End Class