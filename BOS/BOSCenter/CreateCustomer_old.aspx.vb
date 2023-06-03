Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports InstamojoAPI



Public Class CreateCustomer_old
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

                formheading3.Text = "BOS - Signup Form"
                lblformsectionhead3.Text = "Customer Details"
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
                GV.addBussinessType(ddlBussinessType)

                ddl_Ref_Code.Items.Clear()
                GV.FL.AddInDropDownListDistinct(ddl_Ref_Code, "Ref_Code", "" & txtDBName.Text & ".dbo.BOS_Ref_Code_Master order by Ref_Code ")
                If ddl_Ref_Code.Items.Count > 0 Then
                    ddl_Ref_Code.Items.Insert(0, ":::: Select Code ::::")
                Else
                    ddl_Ref_Code.Items.Add(":::: Select Code ::::")
                End If
                lblServiceCharge.Text = "0"

                ddlState.Items.Clear()
                GV.FL.AddInDropDownListDistinct(ddlState, "State_Name", "" & txtDBName.Text.Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                If ddlState.Items.Count > 0 Then
                    ddlState.Items.Insert(0, ":::: Select State ::::")
                Else
                    ddlState.Items.Add(":::: Select State ::::")
                End If

                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add(":::: Select District ::::")


                Session("EditFlag") = 0
                Session("Workfor") = "Save"

                If txtDBName.Text.Trim.ToUpper = "BosCenter_DB".Trim.ToUpper Then
                    txtAgencyName.Text = "BOS Money"
                Else
                    Dim CompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " " & txtDBName.Text.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & txtCompanyCode.Text.Trim & "' ")
                    txtAgencyName.Text = GV.parseString(CompanyName.Trim)
                End If


                txtAgencyName.ReadOnly = True

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

            If txtAgencyName.Text.Trim = "" Then
                txtAgencyName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAgencyName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAgencyName.CssClass = "form-control"
            End If

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
            If txtDOB.Text.Trim = "" Then
                txtDOB.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtDOB.Focus()
                    isFocusApplied = True
                End If
            Else
                txtDOB.CssClass = "form-control"
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

            If txtCreditBalLimit.Text.Trim = "" Then
                txtCreditBalLimit.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtCreditBalLimit.Focus()
                    isFocusApplied = True
                End If
            Else
                txtCreditBalLimit.CssClass = "form-control"
            End If

            If isErrorFound = True Then
                Exit Sub
            End If

            If Not txtAddharCardNo.Text.Trim = "" Then
                VAddharCardNo = GV.parseString(txtAddharCardNo.Text.Trim)
            Else
                VAddharCardNo = ""
            End If


            Dim strQry, QryMsg As String

            If VAddharCardNo.Trim = "" Then
                strQry = "" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where ( MobileNo='" & GV.parseString(txtMobileNo.Text) & "') and ( AgentType='" & ddlAgentType.SelectedValue & "' ) "
                QryMsg = "Record with Mobile Already Exists."
            Else
                strQry = "" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where ( MobileNo='" & GV.parseString(txtMobileNo.Text) & "'  or AddharCardNo='" & VAddharCardNo & "' ) and ( AgentType='" & ddlAgentType.SelectedValue & "' ) "
                QryMsg = "Record with Mobile or AddharCardNo Already Exists."
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
            If Not txtAgencyName.Text.Trim = "" Then
                VAgencyName = GV.parseString(txtAgencyName.Text.Trim)
            Else
                VAgencyName = ""
            End If

            If Not ddl_Ref_Code.SelectedIndex = 0 Then
                VRef_Code = GV.parseString(ddl_Ref_Code.SelectedValue.Trim)
            Else
                VRef_Code = ""
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

            If Not txtDOB.Text.Trim = "" Then
                VDOB = GV.FL.returnDateMonthWise(txtDOB.Text.Trim)
            Else
                VDOB = ""
            End If

            If Not txtAlternateMobileNo.Text.Trim = "" Then
                VAlternateMobileNo = GV.parseString(txtAlternateMobileNo.Text.Trim)
            Else
                VAlternateMobileNo = ""
            End If

            If Not txtPermanentAddress.Text.Trim = "" Then
                VPermanentAddress = GV.parseString(txtPermanentAddress.Text.Trim)
            Else
                VPermanentAddress = ""
            End If

            If Not ddlState.SelectedIndex = 0 Then
                VState = GV.parseString(ddlState.SelectedValue.Trim)
            Else
                VState = ""
            End If

            If Not txtAddharCardNo.Text.Trim = "" Then
                VAddharCardNo = GV.parseString(txtAddharCardNo.Text.Trim)
            Else
                VAddharCardNo = ""
            End If

            If Not txtWebSite.Text.Trim = "" Then
                VWebSite = GV.parseString(txtWebSite.Text.Trim)
            Else
                VWebSite = ""
            End If

            If Not txtRegistrationId.Text.Trim = "" Then
                VRegistrationId = GV.parseString(txtRegistrationId.Text.Trim)
            Else
                VRegistrationId = ""
            End If

            If Not txtPanCardNumber.Text.Trim = "" Then
                VPanCardNumber = GV.parseString(txtPanCardNumber.Text.Trim)
            Else
                VPanCardNumber = ""
            End If

            If Not txtMobileNo.Text.Trim = "" Then
                VMobileNo = GV.parseString(txtMobileNo.Text.Trim)
            Else
                VMobileNo = ""
            End If

            If Not txtOfficeAddress.Text.Trim = "" Then
                VOfficeAddress = GV.parseString(txtOfficeAddress.Text.Trim)
            Else
                VOfficeAddress = ""
            End If

            If Not txtCity.Text.Trim = "" Then
                VCity = GV.parseString(txtCity.Text.Trim)
            Else
                VCity = ""
            End If

            If Not txtLastName.Text.Trim = "" Then
                VLastName = GV.parseString(txtLastName.Text.Trim)
            Else
                VLastName = ""
            End If

            If Not txtPincode.Text.Trim = "" Then
                VPincode = GV.parseString(txtPincode.Text.Trim)
            Else
                VPincode = ""
            End If

            If Not ddlBussinessType.SelectedIndex = 0 Then
                VBusinessType = GV.parseString(ddlBussinessType.SelectedValue.Trim)
            Else
                VBusinessType = ""
            End If

            VActiveStatus = GV.parseString(ddlStatus.SelectedValue.Trim)
            'VActiveStatus = "Inactive"

            If Not txtGSTNO.Text.Trim = "" Then
                VGSTNO = GV.parseString(txtGSTNO.Text.Trim)
            Else
                VGSTNO = ""
            End If
            If Not txtCreditBalLimit.Text.Trim = "" Then
                VCreditBalLimit = GV.parseString(txtCreditBalLimit.Text.Trim)
            Else
                VCreditBalLimit = "0"
            End If
            VRegisterDate = Now.Date

            '//////// ======Start get All Uploaded Image Path ==================================
            Dim VPanCard_Path, VAddharCardBack_Path, VAddharCardFront_PAth, VPhoto_Path, VOtherDocuments_Path As String
            If Not btnUpload_PanCard.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_PanCard.ToolTip.Trim)
                VPanCard_Path = "~/DistributorDocuments/" & txtDBName.Text.Trim & "/" & VRegistrationId & "/" & fi.Name
            Else
                VPanCard_Path = ""
            End If

            '& "/" & txtDBName.Text.Trim
            If Not btnUpload_AddharCardBack.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_AddharCardBack.ToolTip.Trim)
                VAddharCardBack_Path = "~/DistributorDocuments/" & txtDBName.Text.Trim & "/" & VRegistrationId & "/" & fi.Name
            Else
                VAddharCardBack_Path = ""
            End If

            If Not btnUpload_AddharCardFront.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_AddharCardFront.ToolTip.Trim)
                VAddharCardFront_PAth = "~/DistributorDocuments/" & txtDBName.Text.Trim & "/" & VRegistrationId & "/" & fi.Name
            Else
                VAddharCardFront_PAth = ""
            End If

            If Not btnUpload_Photo.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_Photo.ToolTip.Trim)
                VPhoto_Path = "~/DistributorDocuments/" & txtDBName.Text.Trim & "/" & VRegistrationId & "/" & fi.Name
            Else
                VPhoto_Path = ""
            End If

            If Not btnUpload_OtherDocuments.ToolTip.Trim = "" Then
                Dim fi As New FileInfo(btnUpload_OtherDocuments.ToolTip.Trim)
                VOtherDocuments_Path = "~/DistributorDocuments/" & txtDBName.Text.Trim & "/" & VRegistrationId & "/" & fi.Name
            Else
                VOtherDocuments_Path = ""
            End If
            '//////// ======End get All Uploaded Image Path ==================================

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
            VDistrict = ddlDistrict.SelectedValue
            Dim Encrypted_Pass As String = GV.convertToHashMD5(Vpassword.Trim)


            If Session("EditFlag") = 0 Then

                If GV.FL.RecCount("" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where RegistrationId='" & VRegistrationId & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If



                Dim APIStatus As String = ""

                APIStatus = "Inactive"

                Dim QryStr As String = "insert into " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration (Encrypted_Pass,HoldAmt,Ref_Code,District,AEPS_API_Status,RechargeAPI_Status,MoneyTransferAPI_Status,PANCardAPI_Status,EmpCode,ActiveStatus,TransactionPin,CreditBalnceLimit,RefrenceID,RefrenceType,AgentPassword,UploadPanCard,UploadAddharCard_Front,UploadAddharCard_Back,UploadOtherProof,UploadPhoto,UpdatedBy,UpdatedOn,RecordDateTime,RegistrationDate,AgentType,AgencyName,FirstName,EmailID,DOB,AlternateMobileNo,PermanentAddress,State,AddharCardNo,WebSite,RegistrationId,PanCardNumber,MobileNo,OfficeAddress,City,LastName,Pincode,BusinessType,GSTNO) values('" & Encrypted_Pass & "' ,0,'" & VRef_Code & "','" & VDistrict & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & EMPCode & "','" & VActiveStatus & "','" & trnsactionpinNo & "','" & VCreditBalLimit & "','" & VRefrenceID & "','" & VRefrenceType & "','" & Vpassword & "','" & VPanCard_Path & "','" & VAddharCardFront_PAth & "','" & VAddharCardBack_Path & "','" & VOtherDocuments_Path & "','" & VPhoto_Path & "','" & VUpdatedBy & "'," & VUpdatedOn & "," & VRecord_DateTime & ",'" & VRegisterDate & "', '" & VAgentType & "','" & VAgencyName & "','" & VFirstName & "','" & VEmailID & "','" & VDOB & "','" & VAlternateMobileNo & "','" & VPermanentAddress & "','" & VState & "','" & VAddharCardNo & "','" & VWebSite & "','" & VRegistrationId & "','" & VPanCardNumber & "','" & VMobileNo & "','" & VOfficeAddress & "','" & VCity & "','" & VLastName & "','" & VPincode & "','" & VBusinessType & "','" & VGSTNO & "' ) ;"
                QryStr = QryStr & " " & " delete from " & txtDBName.Text.Trim & ".dbo.BOS_BankDetails where RegistrationId = '" & VRegistrationId & "';"

                If GridView2.Rows.Count > 0 Then
                    For i As Integer = 0 To GridView2.Rows.Count - 1

                        Dim AccountHolderName, BankName, BrnachName, AccountNo, AccountType, IFSCCode As String
                        AccountHolderName = GV.parseString(GridView2.Rows(i).Cells(2).Text)
                        BankName = GV.parseString(GridView2.Rows(i).Cells(3).Text)
                        BrnachName = GV.parseString(GridView2.Rows(i).Cells(4).Text)
                        AccountNo = GV.parseString(GridView2.Rows(i).Cells(5).Text)
                        AccountType = GV.parseString(GridView2.Rows(i).Cells(6).Text)
                        IFSCCode = GV.parseString(GridView2.Rows(i).Cells(7).Text)
                        If QryStr = "" Then
                            QryStr = "insert into " & txtDBName.Text.Trim & ".dbo.BOS_BankDetails (AccountHolderName,AgentType,RegistrationId,RegistrationDate,BankName,BranchName,AccountType,IFSCCode,AccountNo,UpdatedBy,UpdatedOn,RecordDateTime ) values ('" & AccountHolderName & "','" & VAgentType & "','" & VRegistrationId & "','" & VRegisterDate & "' ,'" & BankName & "','" & BrnachName & "','" & AccountType & "','" & IFSCCode & "','" & AccountNo & "','" & VUpdatedBy & "' ," & VUpdatedOn & "," & VUpdatedOn & ");"
                        Else
                            QryStr = QryStr & " " & "insert into " & txtDBName.Text.Trim & ".dbo.BOS_BankDetails (AccountHolderName,AgentType,RegistrationId,RegistrationDate,BankName,BranchName,AccountType,IFSCCode,AccountNo,UpdatedBy,UpdatedOn,RecordDateTime ) values ('" & AccountHolderName & "','" & VAgentType & "','" & VRegistrationId & "','" & VRegisterDate & "' ,'" & BankName & "','" & BrnachName & "','" & AccountType & "','" & IFSCCode & "','" & AccountNo & "','" & VUpdatedBy & "' ," & VUpdatedOn & "," & VUpdatedOn & ");"
                        End If

                    Next
                End If
                If Not txtRefrenceType.Text.Trim.ToUpper = "Admin".Trim.ToUpper Then
                    Dim vTransID As String = GV.FL_AdminLogin.getAutoNumber("TransId")

                    'If CDec(lblServiceCharge.Text.Trim) > 0 Then
                    '    Dim SDId As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    '    Dim SDtypecommFrom As String = "Your Account is debited by Amount Transfer " & lblServiceCharge.Text & " Rs. Due to CreateCustomer on RegID " & VRegistrationId & "."
                    '    Dim SDtypecommTo As String = "Your Account is credited by Amount Transfer " & lblServiceCharge.Text & " Rs. Due to CreateCustomer on RegID " & VRegistrationId & " By ID " & SDId & "."

                    '    QryStr = QryStr & " " & "insert into " & txtDBName.Text.Trim & ".dbo.BOS_TransferAmountToAgents (API_TransId,Ref_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & vTransID & "','" & vTransID & "','" & lblServiceCharge.Text.Trim & "','" & SDtypecommTo & "','" & SDtypecommFrom & "','Deposit','Deposit','" & Now.Date & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & VRegistrationId & "','" & lblServiceCharge.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                    'End If


                    '/////Cashback Calculations - start
                    Dim VCommissionType_ColName, VCommission_ColName As String
                    Dim CashBackComm As Decimal = 0


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


                    'If CashBackComm > 0 Then

                    '    Dim DistypecommTo As String = "Your Account is credited by Cashback " & CashBackComm & " Rs. Due to CreateCustomer on RegID " & VRegistrationId & " / AMT " & lblServiceCharge.Text
                    '    Dim Distypecommfrom As String = "Your Account is debited by Cashback " & CashBackComm & " Rs. Due to CreateCustomer on RegID " & VRegistrationId & " / AMT " & lblServiceCharge.Text & " / By ID " & VRefrenceID & "."


                    '    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt, CashBComm As Decimal
                    '    V_Actual_Commission_Amt = 0
                    '    V_GSTAmt = 0
                    '    V_Commission_Without_GST = 0
                    '    V_TDS_Amt = 0
                    '    V_Net_Commission_Amt = 0
                    '    CashBComm = 0


                    '    V_Actual_Commission_Amt = CashBackComm
                    '    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                    '    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                    '    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                    '    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                    '    CashBComm = V_Net_Commission_Amt

                    '    QryStr = QryStr & " " & "insert into " & txtDBName.Text.Trim & ".dbo.BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values(  '" & vTransID & "', '" & lblServiceCharge.Text & "','" & GV.parseString(vTransID) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','Cashback','" & Now.Date & "','Admin','" & VRefrenceID & "','" & CashBComm & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                    'End If

                    '/////Cashback Calculations - End

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

                    Dim destinationpath As String = "~/DistributorDocuments/" & txtDBName.Text.Trim & "/" & VRegistrationId

                    If Not btnUpload_PanCard.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_PanCard.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_PanCard.ToolTip.Trim), Server.MapPath(VPanCard_Path))
                        End If
                    End If
                    If Not btnUpload_OtherDocuments.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_OtherDocuments.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_OtherDocuments.ToolTip.Trim), Server.MapPath(VOtherDocuments_Path))
                        End If
                    End If


                    If Not btnUpload_AddharCardBack.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_AddharCardBack.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_AddharCardBack.ToolTip.Trim), Server.MapPath(VAddharCardBack_Path))
                        End If
                    End If


                    If Not btnUpload_AddharCardFront.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_AddharCardFront.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_AddharCardFront.ToolTip.Trim), Server.MapPath(VAddharCardFront_PAth))
                        End If
                    End If



                    If Not btnUpload_Photo.ToolTip.Trim = "" Then

                        If Not Directory.Exists(Server.MapPath(destinationpath)) Then
                            Directory.CreateDirectory(Server.MapPath(destinationpath))
                        End If
                        If File.Exists(Server.MapPath(btnUpload_Photo.ToolTip.Trim)) Then
                            File.Move(Server.MapPath(btnUpload_Photo.ToolTip.Trim), Server.MapPath(VPhoto_Path))
                        End If
                    End If

                    TD_Pin.Visible = True
                    Div_deInfo.Visible = True
                    lblClientID.Text = VRegistrationId
                    lblPassword.Text = Vpassword
                    lblTransactionPin.Text = trnsactionpinNo

                    Request_CompanyCode = txtCompanyCode.Text.Trim
                    Request_AgentID = VRegistrationId

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

                    Clear()
                    ddlAgentType_SelectedIndexChanged(sender, e)
                    ddl_Ref_Code.SelectedIndex = 0
                    ddl_Ref_Code_SelectedIndexChanged(sender, e)


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


                    ModalPopupExtender1.Hide()
                    ModalPopupExtender3.Hide()
                    ModalPopupExtender4.Hide()


                    'ddlAgentType_SelectedIndexChanged(sender, e)
                    'lblDialogMsg.Text = "Record Saved Successfully."
                    'lblDialogMsg.CssClass = "Successlabels"
                    'btnCancel.Text = "Ok"
                    'btnPopupYes.Visible = False
                    'ModalPopupExtender1.Show()
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
            ddlAccountType.SelectedIndex = 0

            txtAgencyName.Text = ""

            txtFirstName.Text = ""

            txtEmailID.Text = ""

            txtDOB.Text = ""

            txtAlternateMobileNo.Text = ""

            txtPermanentAddress.Text = ""

            ddlState.SelectedIndex = 0

            txtAddharCardNo.Text = ""

            txtWebSite.Text = ""


            txtPanCardNumber.Text = ""

            txtMobileNo.Text = ""

            txtOfficeAddress.Text = ""

            txtCity.Text = ""

            txtLastName.Text = ""

            txtPincode.Text = ""
            ddlStatus.SelectedIndex = 0

            If txtDBName.Text.Trim.ToUpper = "BosCenter_DB".Trim.ToUpper Then
                txtAgencyName.Text = "BOS Money"
            Else
                Dim CompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " " & txtDBName.Text.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & txtCompanyCode.Text.Trim & "'  ")
                txtAgencyName.Text = GV.parseString(CompanyName.Trim)
            End If

            txtRefrenceID.Text = ""
            lblRefId_Error.Text = ""
            txtRefrenceType.Text = ""



            txtAccountHolderName.CssClass = "form-control"
            txtBankBranch.CssClass = "form-control"
            txtBankName.CssClass = "form-control"
            txtAccountNumber.CssClass = "form-control"
            txtIFSCode.CssClass = "form-control"
            ddlAccountType.CssClass = "form-control"

            lblErrorGrid.Text = ""
            lblErrorGrid.CssClass = ""

            ddlAccountType.CssClass = "form-control"
            txtAgencyName.CssClass = "form-control"
            txtFirstName.CssClass = "form-control"
            txtEmailID.CssClass = "form-control"
            txtDOB.CssClass = "form-control"
            txtAlternateMobileNo.CssClass = "form-control"
            txtPermanentAddress.CssClass = "form-control"
            ddlState.CssClass = "form-control"
            txtAddharCardNo.CssClass = "form-control"
            txtWebSite.CssClass = "form-control"
            txtPanCardNumber.CssClass = "form-control"
            txtMobileNo.CssClass = "form-control"
            txtOfficeAddress.CssClass = "form-control"
            txtCity.CssClass = "form-control"
            txtLastName.CssClass = "form-control"
            txtPincode.CssClass = "form-control"
            ddlBussinessType.CssClass = "form-control"
            txtGSTNO.CssClass = "form-control"

            GridView2.DataSource = Nothing
            GridView2.DataBind()
            txtAccountHolderName.Text = ""
            txtAccountNumber.Text = ""
            txtBankBranch.Text = ""
            txtBankName.Text = ""
            txtIFSCode.Text = ""


            Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"
            Image_AddharCardFront.ImageUrl = "~/images/uploadimage.png"
            Image_OtherDocuments.ImageUrl = "~/images/uploadimage.png"
            Image_Photo.ImageUrl = "~/images/uploadimage.png"
            Image_PanCard.ImageUrl = "~/images/uploadimage.png"

            btnUpload_AddharCardBack.Text = "Upload"
            btnUpload_PanCard.Text = "Upload"
            btnUpload_AddharCardFront.Text = "Upload"
            btnUpload_Photo.Text = "Upload"
            btnUpload_OtherDocuments.Text = "Upload"

            btnDeleteUpload_AddharCardBack.Enabled = False
            btnDeleteUpload_PanCard.Enabled = False
            btnDeleteUpload_AddharCardFront.Enabled = False
            btnDeleteUpload_Photo.Enabled = False
            btnDeleteUpload_OtherDocuments.Enabled = False

            ddlBussinessType.SelectedIndex = 0


            txtGSTNO.Text = ""
            GridView2.DataSource = Nothing
            GridView2.DataBind()
            lblErrorGrid.Text = ""
            lblErrorGrid.CssClass = ""
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
                Response.Redirect("CreateAgent.aspx?admin=" & txtCompanyCode.Text.Trim)
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("Customer_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("CustomerID", txtDBName.Text.Trim)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_PanCard.Click
        Try
            Try
                lblErrorImage_PanCard.Text = ""
                lblErrorImage_PanCard.CssClass = ""
                If (btnUpload_PanCard.Text = "Download") Then
                    DownloadDoc(btnUpload_PanCard.ToolTip)
                Else

                    If FileUpload_PanCard.HasFile = True Then

                        filePath = FileUpload_PanCard.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                            SaveImage(FileUpload_PanCard, btnUpload_PanCard, btnDeleteUpload_PanCard, "PanCard")
                            Dim fi As New FileInfo(btnUpload_PanCard.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_PanCard.ImageUrl = btnUpload_PanCard.ToolTip.ToString()
                                btnUpload_PanCard.Focus()

                            End If
                        Else
                            lblInformation.Text = "Invalid Image Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_PanCard.Focus()
                        End If

                    Else
                        lblErrorImage_PanCard.Text = "Please Select Image To Upload."
                        lblErrorImage_PanCard.CssClass = "errorlabels"
                        btnUpload_PanCard.Focus()
                    End If

                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_AgeProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_AddharCardBack.Click
        Try
            Try
                lblErrorImage_AddharCardBack.Text = ""
                lblErrorImage_AddharCardBack.CssClass = ""
                If (btnUpload_AddharCardBack.Text = "Download") Then
                    DownloadDoc(btnUpload_AddharCardBack.ToolTip)
                Else
                    If FileUpload_AddharCardBack.HasFile = True Then


                        filePath = FileUpload_AddharCardBack.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                            SaveImage(FileUpload_AddharCardBack, btnUpload_AddharCardBack, btnDeleteUpload_AddharCardBack, "AddharCardBack")
                            Dim fi As New FileInfo(btnUpload_AddharCardBack.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_AddharCardBack.ImageUrl = btnUpload_AddharCardBack.ToolTip.ToString()
                                btnUpload_AddharCardBack.Focus()
                            End If
                        Else
                            lblInformation.Text = "Invalid Document Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_AddharCardBack.Focus()
                        End If

                    Else
                        lblErrorImage_AddharCardBack.Text = "Please Select Document To Upload."
                        lblErrorImage_AddharCardBack.CssClass = "errorlabels"
                        btnUpload_AddharCardBack.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_AddressProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_AddharCardFront.Click
        Try
            Try
                lblErrorImage_AddharCardFront.Text = ""
                lblErrorImage_AddharCardFront.CssClass = ""
                If (btnUpload_AddharCardFront.Text = "Download") Then
                    DownloadDoc(btnUpload_AddharCardFront.ToolTip)
                Else
                    If FileUpload_AddharCardFront.HasFile = True Then

                        filePath = FileUpload_AddharCardFront.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                            SaveImage(FileUpload_AddharCardFront, btnUpload_AddharCardFront, btnDeleteUpload_AddharCardFront, "AddharCardFront")
                            Dim fi As New FileInfo(btnUpload_AddharCardFront.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_AddharCardFront.ImageUrl = btnUpload_AddharCardFront.ToolTip.ToString()
                                btnUpload_AddharCardFront.Focus()

                            End If
                        Else
                            lblInformation.Text = "Invalid Image Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_AddharCardFront.Focus()
                        End If


                    Else
                        lblErrorImage_AddharCardFront.Text = "Please Select Image To Upload."
                        lblErrorImage_AddharCardFront.CssClass = "errorlabels"
                        btnUpload_AddharCardFront.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_Photo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_Photo.Click
        Try
            Try
                lblErrorImage_Photo.Text = ""
                lblErrorImage_Photo.CssClass = ""
                If (btnUpload_Photo.Text = "Download") Then
                    DownloadDoc(btnUpload_Photo.ToolTip)
                Else
                    If FileUpload_Photo.HasFile = True Then

                        filePath = FileUpload_Photo.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                            SaveImage(FileUpload_Photo, btnUpload_Photo, btnDeleteUpload_Photo, "Photo")
                            Dim fi As New FileInfo(btnUpload_Photo.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_Photo.ImageUrl = btnUpload_Photo.ToolTip.ToString()
                                btnUpload_Photo.Focus()
                            Else
                                Image_Photo.ImageUrl = "~/images/documents.png"
                                btnUpload_Photo.Focus()
                            End If
                        Else
                            lblInformation.Text = "Invalid Document Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_Photo.Focus()
                        End If

                    Else
                        lblErrorImage_Photo.Text = "No file Selected for photo"
                        lblErrorImage_Photo.CssClass = "errorlabels"
                        btnUpload_Photo.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_Signature_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_OtherDocuments.Click
        Try
            Try

                lblErrorImage_OtherDocuments.Text = ""
                lblErrorImage_OtherDocuments.CssClass = ""

                If (btnUpload_OtherDocuments.Text = "Download") Then
                    DownloadDoc(btnUpload_OtherDocuments.ToolTip)
                Else
                    If FileUpload_OtherDocuments.HasFile = True Then

                        filePath = FileUpload_OtherDocuments.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                            SaveImage(FileUpload_OtherDocuments, btnUpload_OtherDocuments, btnDeleteUpload_OtherDocuments, "OtherDocuments")
                            Dim fi As New FileInfo(btnUpload_OtherDocuments.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_OtherDocuments.ImageUrl = btnUpload_OtherDocuments.ToolTip.ToString()
                                btnUpload_OtherDocuments.Focus()
                            Else
                                Image_OtherDocuments.ImageUrl = "~/images/documents.png"
                                btnUpload_OtherDocuments.Focus()
                            End If
                        Else
                            lblInformation.Text = "Invalid Document Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_OtherDocuments.Focus()
                        End If


                    Else
                        lblErrorImage_OtherDocuments.Text = "No file Selected for Other Doc."
                        lblErrorImage_OtherDocuments.CssClass = "errorlabels"
                        btnUpload_OtherDocuments.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Sub DownloadDoc(ByVal DocPath As String)
        Try

            Dim fi As New FileInfo(DocPath)
            Dim strURL As String = DocPath
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" & fi.Name & """")
            Dim data As Byte() = req.DownloadData(Server.MapPath(strURL))
            response.BinaryWrite(data)


            response.[End]()
        Catch ex As Exception
        End Try
    End Sub

    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""

    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button, ByVal imgstr As String)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                    Dim completeFilePath As String = ""


                    If Not Directory.Exists(Server.MapPath("~/Temp")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp"))
                    End If

                    If Not Directory.Exists(Server.MapPath("~/Temp/" & txtDBName.Text.Trim)) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & txtDBName.Text.Trim))
                    End If



                    'txtDBName.Text.Trim

                    completeFilePath = Server.MapPath("~/Temp" & "/" & txtDBName.Text.Trim & "/" & txtRegistrationId.Text.Trim & "_" & imgstr & Session("ext"))
                    imgPath = "~/Temp" & "/" & txtDBName.Text.Trim & "/" & txtRegistrationId.Text.Trim & "_" & imgstr & Session("ext")

                    imagUpload.PostedFile.SaveAs(completeFilePath)

                    Dim bytesInStream As Byte() = New Byte(imagUpload.PostedFile.InputStream.Length - 1) {}
                    Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(imagUpload.PostedFile.InputStream)

                    Dim bmp As New Bitmap(image)
                    bmp = ImageOptimization(bmp, completeFilePath, filename, "THUMBNAIL")
                    bmp.Save(completeFilePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                    'bmp.Save(completeFilePath + "/Thumbnail_" + filename, System.Drawing.Imaging.ImageFormat.Jpeg)



                    UploadButtonName.ToolTip = imgPath
                    UploadButtonName.Text = "Download"
                    RemoveButtonName.Enabled = True



                Else
                End If
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub



    Private Function ImageOptimization(ByVal image As Image, ByVal targetPath As String, ByVal fileName As String, ByVal imagetype As String) As Bitmap

        fileName = Path.GetFileName(fileName)
        fileName.Replace("\", "")
        Dim namefileName = fileName.Substring(0, fileName.LastIndexOf("."))
        Dim ext1 As String = Path.GetExtension(fileName)
        Dim imgPath As String = ""


        Dim reqWidth As Integer = 0
        Dim reqHeight As Integer = 0
        reqWidth = image.Width
        reqHeight = image.Height

        Dim thumbnailImg = Resize_Image(DirectCast(image, Bitmap), reqWidth, reqHeight)


        imgPath = Convert.ToString((targetPath & Convert.ToString("/"c)) + namefileName) & ext1

        If System.IO.File.Exists(imgPath) Then
            System.IO.File.Delete(imgPath)
        End If
        'thumbnailImg.Save(imgPath, image.RawFormat);

        Return thumbnailImg
    End Function

    Private Function Resize_Image(ByVal streamImage As Bitmap, ByVal maxWidth As Integer, ByVal maxHeight As Integer) As Bitmap
        Dim originalImage As New Bitmap(streamImage)
        Dim newWidth As Integer = originalImage.Width
        Dim newHeight As Integer = originalImage.Height
        Dim aspectRatio As Double = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height)

        If aspectRatio <= 1 AndAlso originalImage.Width > maxWidth Then
            newWidth = maxWidth
            newHeight = Convert.ToInt32(Math.Round(newWidth / aspectRatio))
        ElseIf aspectRatio > 1 AndAlso originalImage.Height > maxHeight Then
            newHeight = maxHeight
            newWidth = Convert.ToInt32(Math.Round(newHeight * aspectRatio))
        End If
        Return New Bitmap(originalImage, newWidth, newHeight)
    End Function


    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_PanCard.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "PanCard"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_AgeProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_AddharCardFront.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "AddharCardFront"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_AddressProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_AddharCardBack.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "AddharCardBack"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_Photo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_Photo.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the Photo Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "Photo"
            ModalPopupExtender4.Show()


        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_Signature_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_OtherDocuments.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the Signature Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "OtherDocuments"
            ModalPopupExtender4.Show()


        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                Dim VMemberID As String = GV.parseString(txtRegistrationId.Text.Trim)


                If lblDeleteDocumentInfo.Text = "PanCard" Then

                    Dim imgpath As String = btnUpload_PanCard.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadPanCard=''  where RegistrationId='" & VMemberID & "' "
                    btnUpload_PanCard.ToolTip = ""
                    btnUpload_PanCard.Text = "Upload"
                    btnDeleteUpload_PanCard.Enabled = False
                    Image_PanCard.ImageUrl = "~/images/uploadimage.png"
                    btnDeleteUpload_PanCard.Focus()

                    lblDeleteInfo.Text = "Document Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "AddharCardFront" Then

                    Dim imgpath As String = btnUpload_AddharCardFront.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadAddharCard_Front=''  where RegistrationId='" & VMemberID & "' "

                    btnUpload_AddharCardFront.ToolTip = ""
                    btnUpload_AddharCardFront.Text = "Upload"
                    btnDeleteUpload_AddharCardFront.Enabled = False
                    btnDeleteUpload_AddharCardFront.Focus()
                    Image_AddharCardFront.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "Document Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "AddharCardBack" Then

                    Dim imgpath As String = btnUpload_AddharCardBack.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadAddharCard_Back=''  where RegistrationId='" & VMemberID & "'"

                    btnUpload_AddharCardBack.ToolTip = ""
                    btnUpload_AddharCardBack.Text = "Upload"
                    btnDeleteUpload_AddharCardBack.Enabled = False
                    btnDeleteUpload_AddharCardBack.Focus()
                    Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "Document Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "Photo" Then

                    Dim imgpath As String = btnUpload_Photo.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadPhoto=''  where RegistrationId='" & VMemberID & "' "

                    btnUpload_Photo.ToolTip = ""
                    btnUpload_Photo.Text = "Upload"
                    btnDeleteUpload_Photo.Enabled = False
                    btnDeleteUpload_Photo.Focus()
                    Image_Photo.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "Photo Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "OtherDocuments" Then

                    Dim imgpath As String = btnUpload_OtherDocuments.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadOtherProof=''  where RegistrationId='" & VMemberID & "' "

                    btnUpload_OtherDocuments.ToolTip = ""
                    btnUpload_OtherDocuments.Text = "Upload"
                    btnDeleteUpload_OtherDocuments.Enabled = False
                    btnDeleteUpload_OtherDocuments.Focus()
                    Image_OtherDocuments.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "OtherDocuments Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"





                End If


                If Not qry = "" Then
                    GV.FL.DMLQueries(qry)
                End If

                btnDelete_Document.Visible = False
                btnDelete_Document.Text = "OK"
                btnCancelDeleteDocument.Text = "OK"

                'btnCancelDeleteDocument.Attributes("style") = "display:none"

            Else

            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdDeleteRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try


            lblDelType.Text = ""
            lblRowIndex.Text = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex
            Session("DeleteType") = "GRD"
            lblDel.Text = GV.parseString(GridView2.Rows(gvrow.RowIndex).Cells(2).Text)
            lblDelType.Text = GV.parseString(GridView2.Rows(gvrow.RowIndex).Cells(1).Text)
            lblDelDialogMsg.Text = "Are you sure you want to delete ?"
            lblAlertDelPer.Text = "It Will Delete Permanently."
            lblDelDialogMsg.CssClass = ""
            btnDelCancel.Text = "Cancel"
            btnDelOk.Visible = True
            DeleteModalPopupExtender.Show()


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelOk_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If Not lblRowIndex.Text.Trim = "" Then

                RemoveAndSwap_SelectedRow(GV.parseString(lblRowIndex.Text))
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
    Public Sub RemoveAndSwap_SelectedRow(ByVal rowindex As Integer)
        Try
            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("SrNo")
            Dim dc2 As DataColumn = New DataColumn("AccountHolderName")
            Dim dc3 As DataColumn = New DataColumn("BankName")
            Dim dc4 As DataColumn = New DataColumn("BranchName")
            Dim dc5 As DataColumn = New DataColumn("AccountNo")
            Dim dc6 As DataColumn = New DataColumn("AccountType")
            Dim dc7 As DataColumn = New DataColumn("IFSCCode")

            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            dt.Columns.Add(dc5)
            dt.Columns.Add(dc6)
            dt.Columns.Add(dc7)


            For i As Integer = 0 To GridView2.Rows.Count - 1
                If Not (lblRowIndex.Text = i) Then
                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = GV.parseString(GridView2.Rows(i).Cells(1).Text)
                    dr1(1) = GV.parseString(GridView2.Rows(i).Cells(2).Text)
                    dr1(2) = GV.parseString(GridView2.Rows(i).Cells(3).Text)
                    dr1(3) = GV.parseString(GridView2.Rows(i).Cells(4).Text)
                    dr1(4) = GV.parseString(GridView2.Rows(i).Cells(5).Text)
                    dr1(5) = GV.parseString(GridView2.Rows(i).Cells(6).Text)
                    dr1(6) = GV.parseString(GridView2.Rows(i).Cells(7).Text)
                    dt.Rows.Add(dr1)
                End If
            Next

            GridView2.DataSource = dt
            GridView2.DataBind()
            GV.FL.showSerialnoOnGridView(GridView2, 1)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdEditRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex
            btnAddBand.Text = "Update"
            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("SrNo")
            Dim dc2 As DataColumn = New DataColumn("AccountHolderName")
            Dim dc3 As DataColumn = New DataColumn("BankName")
            Dim dc4 As DataColumn = New DataColumn("BranchName")
            Dim dc5 As DataColumn = New DataColumn("AccountNo")
            Dim dc6 As DataColumn = New DataColumn("AccountType")
            Dim dc7 As DataColumn = New DataColumn("IFSCCode")

            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            dt.Columns.Add(dc5)
            dt.Columns.Add(dc6)
            dt.Columns.Add(dc7)

            For i As Integer = 0 To GridView2.Rows.Count - 1
                If Not (lblRowIndex.Text = i) Then
                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = GV.parseString(GridView2.Rows(i).Cells(1).Text)
                    dr1(1) = GV.parseString(GridView2.Rows(i).Cells(2).Text)
                    dr1(2) = GV.parseString(GridView2.Rows(i).Cells(3).Text)
                    dr1(3) = GV.parseString(GridView2.Rows(i).Cells(4).Text)
                    dr1(4) = GV.parseString(GridView2.Rows(i).Cells(5).Text)
                    dr1(5) = GV.parseString(GridView2.Rows(i).Cells(6).Text)
                    dr1(6) = GV.parseString(GridView2.Rows(i).Cells(7).Text)
                    dt.Rows.Add(dr1)
                Else
                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = GV.parseString(GridView2.Rows(i).Cells(1).Text)
                    dr1(1) = GV.parseString(GridView2.Rows(i).Cells(2).Text)
                    dr1(2) = GV.parseString(GridView2.Rows(i).Cells(3).Text)
                    dr1(3) = GV.parseString(GridView2.Rows(i).Cells(4).Text)
                    dr1(4) = GV.parseString(GridView2.Rows(i).Cells(5).Text)
                    dr1(5) = GV.parseString(GridView2.Rows(i).Cells(6).Text)
                    dr1(6) = GV.parseString(GridView2.Rows(i).Cells(7).Text)
                    dt.Rows.Add(dr1)
                    Session("AA") = "Edit"
                    txtAccountHolderName.Text = GV.parseString(GridView2.Rows(i).Cells(2).Text)
                    txtBankName.Text = GV.parseString(GridView2.Rows(i).Cells(3).Text)
                    txtBankBranch.Text = GV.parseString(GridView2.Rows(i).Cells(4).Text)
                    txtAccountNumber.Text = GV.parseString(GridView2.Rows(i).Cells(5).Text)
                    ddlAccountType.SelectedValue = GV.parseString(GridView2.Rows(i).Cells(6).Text)
                    txtIFSCode.Text = GV.parseString(GridView2.Rows(i).Cells(7).Text)
                End If
            Next



            GridView2.DataSource = dt
            GridView2.DataBind()
            GV.FL.showSerialnoOnGridView(GridView2, 1)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddBand_Click(sender As Object, e As EventArgs) Handles btnAddBand.Click
        Try
            Div_Grid.Visible = True

            lblErrorGrid.Text = ""
            lblErrorGrid.CssClass = ""


            Dim isErrorFound As Boolean
            Dim isFocusApplied As Boolean = False


            If isErrorFound = True Then
                Exit Sub
            End If
            If txtBankName.Text = "" Then
                lblErrorGrid.Text = "Please Enter Bank Name."
                lblErrorGrid.CssClass = "errorlabels"
                txtBankName.CssClass = "ValidationError"
                txtBankName.Focus()
                Exit Sub
            Else
                txtBankName.CssClass = "form-control"
            End If
            If txtBankBranch.Text = "" Then
                lblErrorGrid.Text = "Please Enter Branch Name."
                lblErrorGrid.CssClass = "errorlabels"
                txtBankBranch.CssClass = "ValidationError"
                txtBankBranch.Focus()
                Exit Sub
            Else
                txtBankBranch.CssClass = "form-control"
            End If
            If txtAccountNumber.Text = "" Then
                lblErrorGrid.Text = "Please Enter AccountNo."
                lblErrorGrid.CssClass = "errorlabels"
                txtAccountNumber.CssClass = "ValidationError"
                txtAccountNumber.Focus()
                Exit Sub
            Else
                txtAccountNumber.CssClass = "form-control"
            End If

            If txtIFSCode.Text = "" Then
                lblErrorGrid.Text = "Please Enter IFSC Code."
                lblErrorGrid.CssClass = "errorlabels"
                txtIFSCode.CssClass = "ValidationError"
                txtIFSCode.Focus()
                Exit Sub
            Else
                txtIFSCode.CssClass = "form-control"
            End If




            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("SrNo")
            Dim dc2 As DataColumn = New DataColumn("AccountHolderName")
            Dim dc3 As DataColumn = New DataColumn("BankName")
            Dim dc4 As DataColumn = New DataColumn("BranchName")
            Dim dc5 As DataColumn = New DataColumn("AccountNo")
            Dim dc6 As DataColumn = New DataColumn("AccountType")
            Dim dc7 As DataColumn = New DataColumn("IFSCCode")

            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            dt.Columns.Add(dc5)
            dt.Columns.Add(dc6)
            dt.Columns.Add(dc7)



            For i As Integer = 0 To GridView2.Rows.Count - 1
                If lblRowIndex.Text.Trim = "" Then
                    If (GV.parseString(GridView2.Rows(i).Cells(4).Text) = GV.parseString(txtAccountNumber.Text)) Then
                        lblErrorGrid.Text = "Record Allready Exists !!!"
                        lblErrorGrid.CssClass = "errorlabels"
                        Exit Sub
                    End If
                Else
                    If (GV.parseString(GridView2.Rows(i).Cells(4).Text) = GV.parseString(txtAccountNumber.Text)) And Not i = CInt(lblRowIndex.Text) Then
                        lblErrorGrid.Text = "Record Allready Exists !!!"
                        lblErrorGrid.CssClass = "errorlabels"
                        Exit Sub
                    End If

                End If
            Next


            '//////////////////////////////////
            For i As Integer = 0 To GridView2.Rows.Count - 1
                Dim dr1 As DataRow = dt.NewRow()

                If btnAddBand.Text = "Update" Then
                    If CInt(lblRowIndex.Text) = i Then
                        dr1(1) = GV.parseString(txtAccountHolderName.Text)
                        dr1(2) = GV.parseString(txtBankName.Text)
                        dr1(3) = GV.parseString(txtBankBranch.Text)
                        dr1(4) = GV.parseString(txtAccountNumber.Text)
                        dr1(5) = GV.parseString(ddlAccountType.SelectedValue)
                        dr1(6) = GV.parseString(txtIFSCode.Text)

                    Else
                        dr1(0) = GV.parseString(GridView2.Rows(i).Cells(1).Text)
                        dr1(1) = GV.parseString(GridView2.Rows(i).Cells(2).Text)
                        dr1(2) = GV.parseString(GridView2.Rows(i).Cells(3).Text)
                        dr1(3) = GV.parseString(GridView2.Rows(i).Cells(4).Text)
                        dr1(4) = GV.parseString(GridView2.Rows(i).Cells(5).Text)
                        dr1(5) = GV.parseString(GridView2.Rows(i).Cells(6).Text)
                        dr1(6) = GV.parseString(GridView2.Rows(i).Cells(7).Text)
                    End If

                Else
                    dr1(0) = GV.parseString(GridView2.Rows(i).Cells(1).Text)
                    dr1(1) = GV.parseString(GridView2.Rows(i).Cells(2).Text)
                    dr1(2) = GV.parseString(GridView2.Rows(i).Cells(3).Text)
                    dr1(3) = GV.parseString(GridView2.Rows(i).Cells(4).Text)
                    dr1(4) = GV.parseString(GridView2.Rows(i).Cells(5).Text)
                    dr1(5) = GV.parseString(GridView2.Rows(i).Cells(6).Text)
                    dr1(6) = GV.parseString(GridView2.Rows(i).Cells(7).Text)
                End If

                dt.Rows.Add(dr1)




            Next
            If Not btnAddBand.Text = "Update" Then
                Dim dr1 As DataRow = dt.NewRow()
                dr1(1) = GV.parseString(txtAccountHolderName.Text)
                dr1(2) = GV.parseString(txtBankName.Text)
                dr1(3) = GV.parseString(txtBankBranch.Text)
                dr1(4) = GV.parseString(txtAccountNumber.Text)
                dr1(5) = GV.parseString(ddlAccountType.SelectedValue)
                dr1(6) = GV.parseString(txtIFSCode.Text)
                dt.Rows.Add(dr1)
                lblErrorGrid.Text = "Record Insert Successfully."
                lblErrorGrid.CssClass = "Successlabels"

            Else
                lblErrorGrid.Text = "Record udpated Successfully."
                lblErrorGrid.CssClass = "Successlabels"

            End If
            '///////////////////////////////////
            txtAccountNumber.Text = ""
            txtBankName.Text = ""
            txtBankBranch.Text = ""
            txtIFSCode.Text = ""
            ddlAccountType.SelectedIndex = 0
            lblRowIndex.Text = ""
            btnAddBand.Text = "Add Details"

            GridView2.DataSource = dt
            GridView2.DataBind()
            GV.FL.showSerialnoOnGridView(GridView2, 1)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlState.SelectedIndexChanged
        Try
            If ddlState.SelectedIndex = 0 Then
                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add(":::: Select District ::::")
            Else
                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add(":::: Select District ::::")

                GV.FL.AddInDropDownListDistinct(ddlDistrict, " District_Name ", "" & txtDBName.Text.Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' and State_Name='" & ddlState.SelectedValue & "'")
                If ddlDistrict.Items.Count > 0 Then
                    ddlDistrict.Items.Insert(0, ":::: Select District ::::")
                Else
                    ddlDistrict.Items.Add(":::: Select District ::::")
                End If
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