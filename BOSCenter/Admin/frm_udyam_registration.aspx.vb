Public Class Frm_udyam_registration
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VEmployerAdhaarNo, VFullNameofEmployer, VSocialCategory, VGender, VPhysicallyHandicapped, VNameOfEnterprise, VTypeOfOrganisation, VPANNumber, VAddressofPlant, VLocalityofPlant, VStateofPlant, VDistrictofPlant, VTalukaAndVillageofPlant, VPINCodeofPlant, VAddressofoffice, VLocalityofoffice, VStateofoffice, VDistrictofoffice, VTalukaAndVillageofoffice, VPINCodeofoffice, VofficeMobileNumber, VofficeemailId, VDateofEstablishment, VPreviousEM1_EM2_SSI_UAMRegistrationNumber, VIfAnyEnterNo, VBankIFSC, VBankAccNo, VMajorActivityofUnit, VNICClassificationCode, VEmployee_Worker, VInvest, VDICCenter, VLastYearITR As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Dim isErrorFound As Boolean = False
    Dim isFocusApplied As Boolean = False
    Dim VError_String As String
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("NameTheForm.aspx") '/Change the name of form
            Else
                txtEmployerAdhaarNo.CssClass = "form-control"
                txtFullNameofEmployer.CssClass = "form-control"
                ddlSocialCategory.CssClass = "form-control"
                ddlGender.CssClass = "form-control"
                ddlPhysicallyHandicapped.CssClass = "form-control"
                txtNameOfEnterprise.CssClass = "form-control"
                txtTypeOfOrganisation.CssClass = "form-control"
                txtPANNumber.CssClass = "form-control"
                txtAddressofPlant.CssClass = "form-control"
                txtLocalityofPlant.CssClass = "form-control"
                ddlStateofPlant.CssClass = "form-control"
                ddlDistrictofPlant.CssClass = "form-control"
                txtTalukaAndVillageofPlant.CssClass = "form-control"
                txtPINCodeofPlant.CssClass = "form-control"
                txtAddressofoffice.CssClass = "form-control"
                txtLocalityofoffice.CssClass = "form-control"
                ddlStateofoffice.CssClass = "form-control"
                ddlDistrictofoffice.CssClass = "form-control"
                txtTalukaAndVillageofoffice.CssClass = "form-control"
                txtPINCodeofoffice.CssClass = "form-control"
                txtofficeMobileNumber.CssClass = "form-control"
                txtofficeemailId.CssClass = "form-control"
                txtDateofEstablishment.CssClass = "form-control"
                ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.CssClass = "form-control"
                txtIfAnyEnterNo.CssClass = "form-control"
                txtBankIFSC.CssClass = "form-control"
                txtBankAccNo.CssClass = "form-control"
                ddlMajorActivityofUnit.CssClass = "form-control"
                txtNICClassificationCode.CssClass = "form-control"
                txtEmployee_Worker.CssClass = "form-control"
                txtInvest.CssClass = "form-control"
                txtDICCenter.CssClass = "form-control"
                ddlLastYearITR.CssClass = "form-control"

                VEmployerAdhaarNo = ""
                VFullNameofEmployer = ""
                VSocialCategory = ""
                VGender = ""
                VPhysicallyHandicapped = ""
                VNameOfEnterprise = ""
                VTypeOfOrganisation = ""
                VPANNumber = ""
                VAddressofPlant = ""
                VLocalityofPlant = ""
                VStateofPlant = ""
                VDistrictofPlant = ""
                VTalukaAndVillageofPlant = ""
                VPINCodeofPlant = ""
                VAddressofoffice = ""
                VLocalityofoffice = ""
                VStateofoffice = ""
                VDistrictofoffice = ""
                VTalukaAndVillageofoffice = ""
                VPINCodeofoffice = ""
                VofficeMobileNumber = ""
                VofficeemailId = ""
                VDateofEstablishment = ""
                VPreviousEM1_EM2_SSI_UAMRegistrationNumber = ""
                VIfAnyEnterNo = ""
                VBankIFSC = ""
                VBankAccNo = ""
                VMajorActivityofUnit = ""
                VNICClassificationCode = ""
                VEmployee_Worker = ""
                VInvest = ""
                VDICCenter = ""
                VLastYearITR = ""
                lblError.Text = ""
                lblError.CssClass = ""
                txtEmployerAdhaarNo.Text = ""

                txtFullNameofEmployer.Text = ""

                If ddlSocialCategory.Items.Count > 0 Then
                    ddlSocialCategory.SelectedIndex = 0
                End If

                If ddlGender.Items.Count > 0 Then
                    ddlGender.SelectedIndex = 0
                End If

                If ddlPhysicallyHandicapped.Items.Count > 0 Then
                    ddlPhysicallyHandicapped.SelectedIndex = 0
                End If

                txtNameOfEnterprise.Text = ""

                txtTypeOfOrganisation.Text = ""

                txtPANNumber.Text = ""

                txtAddressofPlant.Text = ""

                txtLocalityofPlant.Text = ""

                If ddlStateofPlant.Items.Count > 0 Then
                    ddlStateofPlant.SelectedIndex = 0
                End If

                If ddlDistrictofPlant.Items.Count > 0 Then
                    ddlDistrictofPlant.SelectedIndex = 0
                End If

                txtTalukaAndVillageofPlant.Text = ""

                txtPINCodeofPlant.Text = ""

                txtAddressofoffice.Text = ""

                txtLocalityofoffice.Text = ""

                If ddlStateofoffice.Items.Count > 0 Then
                    ddlStateofoffice.SelectedIndex = 0
                End If

                If ddlDistrictofoffice.Items.Count > 0 Then
                    ddlDistrictofoffice.SelectedIndex = 0
                End If

                txtTalukaAndVillageofoffice.Text = ""

                txtPINCodeofoffice.Text = ""

                txtofficeMobileNumber.Text = ""

                txtofficeemailId.Text = ""

                txtDateofEstablishment.Text = ""

                If ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.Items.Count > 0 Then
                    ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedIndex = 0
                End If

                txtIfAnyEnterNo.Text = ""

                txtBankIFSC.Text = ""

                txtBankAccNo.Text = ""

                If ddlMajorActivityofUnit.Items.Count > 0 Then
                    ddlMajorActivityofUnit.SelectedIndex = 0
                End If

                txtNICClassificationCode.Text = ""

                txtEmployee_Worker.Text = ""

                txtInvest.Text = ""

                txtDICCenter.Text = ""

                If ddlLastYearITR.Items.Count > 0 Then
                    ddlLastYearITR.SelectedIndex = 0
                End If

                Session("EditFlag") = 0
                btnSave.Text = "Save"
                btnClear.Text = "Reset"
                lblError.Text = ""
                ddlStateofoffice.SelectedValue = "DELHI"
                ddlDistrictofoffice.SelectedValue = "NORTH DELHI"
                ddlStateofPlant.SelectedValue = "DELHI"
                ddlDistrictofPlant.SelectedValue = "NORTH DELHI"


                btnSave.Enabled = True
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            '////////// Start Bulk Validation

            lblError.Text = ""
            lblError.CssClass = ""

            If GV.parseString(txtEmployerAdhaarNo.Text) = "" Then
                txtEmployerAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmployerAdhaarNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtEmployerAdhaarNo) Then
                txtEmployerAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String.Trim = "" Then
                    VError_String = "Please Enter Correct Aadhar No."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter Correct Aadhar No."
                End If
                If isFocusApplied = False Then
                    txtEmployerAdhaarNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtEmployerAdhaarNo.text).Length = 12 Then
                txtEmployerAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String.Trim = "" Then
                    VError_String = "Please Enter 12 Digit Aadhar No."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter 12 Digit Aadhar No."
                End If
                If isFocusApplied = False Then
                    txtEmployerAdhaarNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEmployerAdhaarNo.CssClass = "form-control"
                VEmployerAdhaarNo = GV.parseString(txtEmployerAdhaarNo.Text)
            End If

            If GV.parseString(txtFullNameofEmployer.Text) = "" Then
                txtFullNameofEmployer.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtFullNameofEmployer.Focus()
                    isFocusApplied = True
                End If
            Else
                txtFullNameofEmployer.CssClass = "form-control"
                VFullNameofEmployer = GV.parseString(txtFullNameofEmployer.Text)
            End If

            If GV.parseString(ddlSocialCategory.SelectedValue) = "" Then
                ddlSocialCategory.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlSocialCategory.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlSocialCategory.CssClass = "form-control"
                VSocialCategory = GV.parseString(ddlSocialCategory.SelectedValue)
            End If

            If GV.parseString(ddlGender.SelectedValue) = "" Then
                ddlGender.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlGender.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlGender.CssClass = "form-control"
                VGender = GV.parseString(ddlGender.SelectedValue)
            End If

            If GV.parseString(ddlPhysicallyHandicapped.SelectedValue) = "" Then
                ddlPhysicallyHandicapped.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlPhysicallyHandicapped.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlPhysicallyHandicapped.CssClass = "form-control"
                VPhysicallyHandicapped = GV.parseString(ddlPhysicallyHandicapped.SelectedValue)
            End If

            If GV.parseString(txtNameOfEnterprise.Text) = "" Then
                txtNameOfEnterprise.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNameOfEnterprise.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNameOfEnterprise.CssClass = "form-control"
                VNameOfEnterprise = GV.parseString(txtNameOfEnterprise.Text)
            End If

            If GV.parseString(txtTypeOfOrganisation.Text) = "" Then
                txtTypeOfOrganisation.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtTypeOfOrganisation.Focus()
                    isFocusApplied = True
                End If
            Else
                txtTypeOfOrganisation.CssClass = "form-control"
                VTypeOfOrganisation = GV.parseString(txtTypeOfOrganisation.Text)
            End If

            If GV.parseString(txtPANNumber.Text) = "" Then
                txtPANNumber.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPANNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPANNumber.CssClass = "form-control"
                VPANNumber = GV.parseString(txtPANNumber.Text)
            End If

            If GV.parseString(txtAddressofPlant.Text) = "" Then
                txtAddressofPlant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAddressofPlant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAddressofPlant.CssClass = "form-control"
                VAddressofPlant = GV.parseString(txtAddressofPlant.Text)
            End If

            If GV.parseString(txtLocalityofPlant.Text) = "" Then
                txtLocalityofPlant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtLocalityofPlant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtLocalityofPlant.CssClass = "form-control"
                VLocalityofPlant = GV.parseString(txtLocalityofPlant.Text)
            End If

            If GV.parseString(ddlStateofPlant.SelectedValue) = "" Then
                ddlStateofPlant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStateofPlant.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStateofPlant.CssClass = "form-control"
                VStateofPlant = GV.parseString(ddlStateofPlant.SelectedValue)
            End If

            If GV.parseString(ddlDistrictofPlant.SelectedValue) = "" Then
                ddlDistrictofPlant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrictofPlant.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrictofPlant.CssClass = "form-control"
                VStateofPlant = GV.parseString(ddlDistrictofPlant.SelectedValue)
            End If

            If GV.parseString(txtTalukaAndVillageofPlant.Text) = "" Then
                txtTalukaAndVillageofPlant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtTalukaAndVillageofPlant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtTalukaAndVillageofPlant.CssClass = "form-control"
                VTalukaAndVillageofPlant = GV.parseString(txtTalukaAndVillageofPlant.Text)
            End If

            If GV.parseString(txtPINCodeofPlant.Text) = "" Then
                txtPINCodeofPlant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPINCodeofPlant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPINCodeofPlant.CssClass = "form-control"
                VPINCodeofPlant = GV.parseString(txtPINCodeofPlant.Text)
            End If

            If GV.parseString(txtAddressofoffice.Text) = "" Then
                txtAddressofoffice.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAddressofoffice.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAddressofoffice.CssClass = "form-control"
                VAddressofoffice = GV.parseString(txtAddressofoffice.Text)
            End If

            If GV.parseString(txtLocalityofoffice.Text) = "" Then
                txtLocalityofoffice.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtLocalityofoffice.Focus()
                    isFocusApplied = True
                End If
            Else
                txtLocalityofoffice.CssClass = "form-control"
                VLocalityofoffice = GV.parseString(txtLocalityofoffice.Text)
            End If

            If GV.parseString(ddlStateofoffice.SelectedValue) = "" Then
                ddlStateofoffice.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStateofoffice.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStateofoffice.CssClass = "form-control"
                VStateofoffice = GV.parseString(ddlStateofoffice.SelectedValue)
            End If

            If GV.parseString(ddlDistrictofoffice.SelectedValue) = "" Then
                ddlDistrictofoffice.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrictofoffice.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrictofoffice.CssClass = "form-control"
                VDistrictofoffice = GV.parseString(ddlDistrictofoffice.SelectedValue)
            End If

            If GV.parseString(txtTalukaAndVillageofoffice.Text) = "" Then
                txtTalukaAndVillageofoffice.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtTalukaAndVillageofoffice.Focus()
                    isFocusApplied = True
                End If
            Else
                txtTalukaAndVillageofoffice.CssClass = "form-control"
                VTalukaAndVillageofoffice = GV.parseString(txtTalukaAndVillageofoffice.Text)
            End If

            If GV.parseString(txtPINCodeofoffice.Text) = "" Then
                txtPINCodeofoffice.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPINCodeofoffice.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPINCodeofoffice.CssClass = "form-control"
                VPINCodeofoffice = GV.parseString(txtPINCodeofoffice.Text)
            End If

            If GV.parseString(txtofficeMobileNumber.Text) = "" Then
                txtofficeMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtofficeMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtofficeMobileNumber.text) Then
                txtofficeMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String.Trim = "" Then
                    VError_String = "Please Enter Correct Mobile No."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter Correct Mobile No."
                End If
                If isFocusApplied = False Then
                    txtofficeMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtofficeMobileNumber.text).Length = 10 Then
                txtofficeMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String.Trim = "" Then
                    VError_String = "Please Enter 10 Digit Mobile No."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter 10 Digit Mobile No."
                End If
                If isFocusApplied = False Then
                    txtofficeMobileNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtofficeMobileNumber.CssClass = "form-control"
                VofficeMobileNumber = GV.parseString(txtofficeMobileNumber.Text)
            End If

            If GV.parseString(txtofficeemailId.Text) = "" Then
                txtofficeemailId.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtofficeemailId.Focus()
                    isFocusApplied = True
                End If
            Else
                txtofficeemailId.CssClass = "form-control"
                VofficeemailId = GV.parseString(txtofficeemailId.Text)
            End If

            If GV.parseString(txtDateofEstablishment.Text) = "" Then
                txtDateofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtDateofEstablishment.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsDate(GV.FL.returnDateMonthWiseWithDateChecking(txtDateofEstablishment.text)) Then
                txtDateofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String.Trim = "" Then
                    VError_String = "Please Enter Correct Date Format."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter Correct Date Format."
                End If
                If isFocusApplied = False Then
                    txtDateofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                txtDateofEstablishment.CssClass = "form-control"
                VDateofEstablishment = GV.parseString(txtDateofEstablishment.Text)
            End If

            If GV.parseString(ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedValue) = "" Then
                ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.CssClass = "form-control"
                VPreviousEM1_EM2_SSI_UAMRegistrationNumber = GV.parseString(ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedValue)
            End If

            If GV.parseString(txtIfAnyEnterNo.Text) = "" Then
                txtIfAnyEnterNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtIfAnyEnterNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtIfAnyEnterNo.CssClass = "form-control"
                VIfAnyEnterNo = GV.parseString(txtIfAnyEnterNo.Text)
            End If

            If GV.parseString(txtBankIFSC.Text) = "" Then
                txtBankIFSC.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtBankIFSC.Focus()
                    isFocusApplied = True
                End If
            Else
                txtBankIFSC.CssClass = "form-control"
                VBankIFSC = GV.parseString(txtBankIFSC.Text)
            End If

            If GV.parseString(txtBankAccNo.Text) = "" Then
                txtBankAccNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtBankAccNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtBankAccNo.Text) Then
                txtBankAccNo.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String.Trim = "" Then
                    VError_String = "Please Enter Correct Bank A/c No."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter Correct Bank A/c No."
                End If
                If isFocusApplied = False Then
                    txtBankAccNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtBankAccNo.CssClass = "form-control"
                VBankAccNo = GV.parseString(txtBankAccNo.Text)
            End If

            If GV.parseString(ddlMajorActivityofUnit.SelectedValue) = "" Then
                ddlMajorActivityofUnit.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlMajorActivityofUnit.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlMajorActivityofUnit.CssClass = "form-control"
                VMajorActivityofUnit = GV.parseString(ddlMajorActivityofUnit.SelectedValue)
            End If

            If GV.parseString(txtNICClassificationCode.Text) = "" Then
                txtNICClassificationCode.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNICClassificationCode.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNICClassificationCode.CssClass = "form-control"
                VNICClassificationCode = GV.parseString(txtNICClassificationCode.Text)
            End If

            If GV.parseString(txtEmployee_Worker.Text) = "" Then
                txtEmployee_Worker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmployee_Worker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEmployee_Worker.CssClass = "form-control"
                VEmployee_Worker = GV.parseString(txtEmployee_Worker.Text)
            End If

            If GV.parseString(txtInvest.Text) = "" Then
                txtInvest.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtInvest.Focus()
                    isFocusApplied = True
                End If
            Else
                txtInvest.CssClass = "form-control"
                VInvest = GV.parseString(txtInvest.Text)
            End If

            If GV.parseString(txtDICCenter.Text) = "" Then
                txtDICCenter.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtDICCenter.Focus()
                    isFocusApplied = True
                End If
            Else
                txtDICCenter.CssClass = "form-control"
                VDICCenter = GV.parseString(txtDICCenter.Text)
            End If

            If GV.parseString(ddlLastYearITR.SelectedValue) = "" Then
                ddlLastYearITR.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlLastYearITR.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlLastYearITR.CssClass = "form-control"
                VLastYearITR = GV.parseString(ddlLastYearITR.SelectedValue)
            End If

            If isErrorFound = True Then
                If Not VError_String.Trim = "" Then
                    lblError.Text = VError_String
                    lblError.CssClass = "errorlabels"
                Else
                    lblError.CssClass = False
                End If
                lblError.Visible = True
                Exit Sub
            End If

            '////////// End Bulk Validation

            'If Not txtEmployerAdhaarNo.Text.Trim = "" Then
            '    VEmployerAdhaarNo = GV.parseString(txtEmployerAdhaarNo.Text.Trim)
            'Else
            '    VEmployerAdhaarNo = ""
            'End If

            'If Not txtFullNameofEmployer.Text.Trim = "" Then
            '    VFullNameofEmployer = GV.parseString(txtFullNameofEmployer.Text.Trim)
            'Else
            '    VFullNameofEmployer = ""
            'End If

            'If ddlSocialCategory.Items.Count > 0 Then
            '    If Not ddlSocialCategory.SelectedValue.Trim = "" Then
            '        VSocialCategory = GV.parseString(ddlSocialCategory.SelectedValue.Trim)
            '    Else
            '        VSocialCategory = ""
            '    End If
            'End If

            'If ddlGender.Items.Count > 0 Then
            '    If Not ddlGender.SelectedValue.Trim = "" Then
            '        VGender = GV.parseString(ddlGender.SelectedValue.Trim)
            '    Else
            '        VGender = ""
            '    End If
            'End If

            'If ddlPhysicallyHandicapped.Items.Count > 0 Then
            '    If Not ddlPhysicallyHandicapped.SelectedValue.Trim = "" Then
            '        VPhysicallyHandicapped = GV.parseString(ddlPhysicallyHandicapped.SelectedValue.Trim)
            '    Else
            '        VPhysicallyHandicapped = ""
            '    End If
            'End If

            'If Not txtNameOfEnterprise.Text.Trim = "" Then
            '    VNameOfEnterprise = GV.parseString(txtNameOfEnterprise.Text.Trim)
            'Else
            '    VNameOfEnterprise = ""
            'End If

            'If Not txtTypeOfOrganisation.Text.Trim = "" Then
            '    VTypeOfOrganisation = GV.parseString(txtTypeOfOrganisation.Text.Trim)
            'Else
            '    VTypeOfOrganisation = ""
            'End If

            'If Not txtPANNumber.Text.Trim = "" Then
            '    VPANNumber = GV.parseString(txtPANNumber.Text.Trim)
            'Else
            '    VPANNumber = ""
            'End If

            'If Not txtAddressofPlant.Text.Trim = "" Then
            '    VAddressofPlant = GV.parseString(txtAddressofPlant.Text.Trim)
            'Else
            '    VAddressofPlant = ""
            'End If

            'If Not txtLocalityofPlant.Text.Trim = "" Then
            '    VLocalityofPlant = GV.parseString(txtLocalityofPlant.Text.Trim)
            'Else
            '    VLocalityofPlant = ""
            'End If

            'If ddlStateofPlant.Items.Count > 0 Then
            '    If Not ddlStateofPlant.SelectedValue.Trim = "" Then
            '        VStateofPlant = GV.parseString(ddlStateofPlant.SelectedValue.Trim)
            '    Else
            '        VStateofPlant = ""
            '    End If
            'End If

            'If ddlDistrictofPlant.Items.Count > 0 Then
            '    If Not ddlDistrictofPlant.SelectedValue.Trim = "" Then
            '        VDistrictofPlant = GV.parseString(ddlDistrictofPlant.SelectedValue.Trim)
            '    Else
            '        VDistrictofPlant = ""
            '    End If
            'End If

            'If Not txtTalukaAndVillageofPlant.Text.Trim = "" Then
            '    VTalukaAndVillageofPlant = GV.parseString(txtTalukaAndVillageofPlant.Text.Trim)
            'Else
            '    VTalukaAndVillageofPlant = ""
            'End If

            'If Not txtPINCodeofPlant.Text.Trim = "" Then
            '    VPINCodeofPlant = GV.parseString(txtPINCodeofPlant.Text.Trim)
            'Else
            '    VPINCodeofPlant = ""
            'End If

            'If Not txtAddressofoffice.Text.Trim = "" Then
            '    VAddressofoffice = GV.parseString(txtAddressofoffice.Text.Trim)
            'Else
            '    VAddressofoffice = ""
            'End If

            'If Not txtLocalityofoffice.Text.Trim = "" Then
            '    VLocalityofoffice = GV.parseString(txtLocalityofoffice.Text.Trim)
            'Else
            '    VLocalityofoffice = ""
            'End If

            'If ddlStateofoffice.Items.Count > 0 Then
            '    If Not ddlStateofoffice.SelectedValue.Trim = "" Then
            '        VStateofoffice = GV.parseString(ddlStateofoffice.SelectedValue.Trim)
            '    Else
            '        VStateofoffice = ""
            '    End If
            'End If

            'If ddlDistrictofoffice.Items.Count > 0 Then
            '    If Not ddlDistrictofoffice.SelectedValue.Trim = "" Then
            '        VDistrictofoffice = GV.parseString(ddlDistrictofoffice.SelectedValue.Trim)
            '    Else
            '        VDistrictofoffice = ""
            '    End If
            'End If

            'If Not txtTalukaAndVillageofoffice.Text.Trim = "" Then
            '    VTalukaAndVillageofoffice = GV.parseString(txtTalukaAndVillageofoffice.Text.Trim)
            'Else
            '    VTalukaAndVillageofoffice = ""
            'End If

            'If Not txtPINCodeofoffice.Text.Trim = "" Then
            '    VPINCodeofoffice = GV.parseString(txtPINCodeofoffice.Text.Trim)
            'Else
            '    VPINCodeofoffice = ""
            'End If

            'If Not txtofficeMobileNumber.Text.Trim = "" Then
            '    VofficeMobileNumber = GV.parseString(txtofficeMobileNumber.Text.Trim)
            'Else
            '    VofficeMobileNumber = ""
            'End If

            'If Not txtofficeemailId.Text.Trim = "" Then
            '    VofficeemailId = GV.parseString(txtofficeemailId.Text.Trim)
            'Else
            '    VofficeemailId = ""
            'End If

            'If Not txtDateofEstablishment.Text.Trim = "" Then
            '    VDateofEstablishment = GV.parseString(txtDateofEstablishment.Text.Trim)
            'Else
            '    VDateofEstablishment = ""
            'End If

            'If ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.Items.Count > 0 Then
            '    If Not ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedValue.Trim = "" Then
            '        VPreviousEM1_EM2_SSI_UAMRegistrationNumber = GV.parseString(ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedValue.Trim)
            '    Else
            '        VPreviousEM1_EM2_SSI_UAMRegistrationNumber = ""
            '    End If
            'End If

            'If Not txtIfAnyEnterNo.Text.Trim = "" Then
            '    VIfAnyEnterNo = GV.parseString(txtIfAnyEnterNo.Text.Trim)
            'Else
            '    VIfAnyEnterNo = ""
            'End If

            'If Not txtBankIFSC.Text.Trim = "" Then
            '    VBankIFSC = GV.parseString(txtBankIFSC.Text.Trim)
            'Else
            '    VBankIFSC = ""
            'End If

            'If Not txtBankAccNo.Text.Trim = "" Then
            '    VBankAccNo = GV.parseString(txtBankAccNo.Text.Trim)
            'Else
            '    VBankAccNo = ""
            'End If

            'If ddlMajorActivityofUnit.Items.Count > 0 Then
            '    If Not ddlMajorActivityofUnit.SelectedValue.Trim = "" Then
            '        VMajorActivityofUnit = GV.parseString(ddlMajorActivityofUnit.SelectedValue.Trim)
            '    Else
            '        VMajorActivityofUnit = ""
            '    End If
            'End If

            'If Not txtNICClassificationCode.Text.Trim = "" Then
            '    VNICClassificationCode = GV.parseString(txtNICClassificationCode.Text.Trim)
            'Else
            '    VNICClassificationCode = ""
            'End If

            'If Not txtEmployee_Worker.Text.Trim = "" Then
            '    VEmployee_Worker = GV.parseString(txtEmployee_Worker.Text.Trim)
            'Else
            '    VEmployee_Worker = ""
            'End If

            'If Not txtInvest.Text.Trim = "" Then
            '    VInvest = GV.parseString(txtInvest.Text.Trim)
            'Else
            '    VInvest = ""
            'End If

            'If Not txtDICCenter.Text.Trim = "" Then
            '    VDICCenter = GV.parseString(txtDICCenter.Text.Trim)
            'Else
            '    VDICCenter = ""
            'End If

            'If ddlLastYearITR.Items.Count > 0 Then
            '    If Not ddlLastYearITR.SelectedValue.Trim = "" Then
            '        VLastYearITR = GV.parseString(ddlLastYearITR.SelectedValue.Trim)
            '    Else
            '        VLastYearITR = ""
            '    End If
            'End If

            If btnSave.Text = "Save" Then
                btnPopupYes.Text = "Yes"
                btnPopupYes.Attributes("Style") = ""
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Save??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            Else
                btnPopupYes.Text = "Yes"
                btnCancel.Attributes("Style") = ""
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Update??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub btnPopupYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopupYes.Click
        Try
            If Session("Workfor") = "Delete" Then
                If btnPopupYes.Text = "Ok" Then
                    If Session("EditFlag") = 1 Then
                        Response.Redirect("WebForm3.aspx") '// Change form Name
                    Else
                        Clear()
                        Exit Sub
                    End If
                End If

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.frm_Udyam_Registration where RID=" & lblRID.Text.Trim & ""
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
            If btnPopupYes.Text = "Ok" Then
                If Session("EditFlag") = 1 Then
                    Response.Redirect("Search_form_Name.aspx") '// Change form Name
                Else
                    Clear()
                    Exit Sub
                End If
            End If
            lblError.Text = ""
            lblError.CssClass = ""
            If Not txtEmployerAdhaarNo.Text.Trim = "" Then
                VEmployerAdhaarNo = GV.parseString(txtEmployerAdhaarNo.Text.Trim)
            Else
                VEmployerAdhaarNo = ""
            End If


            If Not txtFullNameofEmployer.Text.Trim = "" Then
                VFullNameofEmployer = GV.parseString(txtFullNameofEmployer.Text.Trim)
            Else
                VFullNameofEmployer = ""
            End If


            If ddlSocialCategory.Items.Count > 0 Then
                If Not ddlSocialCategory.SelectedValue.Trim = "" Then
                    VSocialCategory = GV.parseString(ddlSocialCategory.SelectedValue.Trim)
                Else
                    VSocialCategory = ""
                End If
            End If


            If ddlGender.Items.Count > 0 Then
                If Not ddlGender.SelectedValue.Trim = "" Then
                    VGender = GV.parseString(ddlGender.SelectedValue.Trim)
                Else
                    VGender = ""
                End If
            End If


            If ddlPhysicallyHandicapped.Items.Count > 0 Then
                If Not ddlPhysicallyHandicapped.SelectedValue.Trim = "" Then
                    VPhysicallyHandicapped = GV.parseString(ddlPhysicallyHandicapped.SelectedValue.Trim)
                Else
                    VPhysicallyHandicapped = ""
                End If
            End If

            If Not txtNameOfEnterprise.Text.Trim = "" Then
                VNameOfEnterprise = GV.parseString(txtNameOfEnterprise.Text.Trim)
            Else
                VNameOfEnterprise = ""
            End If


            If Not txtTypeOfOrganisation.Text.Trim = "" Then
                VTypeOfOrganisation = GV.parseString(txtTypeOfOrganisation.Text.Trim)
            Else
                VTypeOfOrganisation = ""
            End If


            If Not txtPANNumber.Text.Trim = "" Then
                VPANNumber = GV.parseString(txtPANNumber.Text.Trim)
            Else
                VPANNumber = ""
            End If


            If Not txtAddressofPlant.Text.Trim = "" Then
                VAddressofPlant = GV.parseString(txtAddressofPlant.Text.Trim)
            Else
                VAddressofPlant = ""
            End If


            If Not txtLocalityofPlant.Text.Trim = "" Then
                VLocalityofPlant = GV.parseString(txtLocalityofPlant.Text.Trim)
            Else
                VLocalityofPlant = ""
            End If


            If ddlStateofPlant.Items.Count > 0 Then
                If Not ddlStateofPlant.SelectedValue.Trim = "" Then
                    VStateofPlant = GV.parseString(ddlStateofPlant.SelectedValue.Trim)
                Else
                    VStateofPlant = ""
                End If
            End If


            If ddlDistrictofPlant.Items.Count > 0 Then
                If Not ddlDistrictofPlant.SelectedValue.Trim = "" Then
                    VDistrictofPlant = GV.parseString(ddlDistrictofPlant.SelectedValue.Trim)
                Else
                    VDistrictofPlant = ""
                End If
            End If


            If Not txtTalukaAndVillageofPlant.Text.Trim = "" Then
                VTalukaAndVillageofPlant = GV.parseString(txtTalukaAndVillageofPlant.Text.Trim)
            Else
                VTalukaAndVillageofPlant = ""
            End If


            If Not txtPINCodeofPlant.Text.Trim = "" Then
                VPINCodeofPlant = GV.parseString(txtPINCodeofPlant.Text.Trim)
            Else
                VPINCodeofPlant = ""
            End If


            If Not txtAddressofoffice.Text.Trim = "" Then
                VAddressofoffice = GV.parseString(txtAddressofoffice.Text.Trim)
            Else
                VAddressofoffice = ""
            End If


            If Not txtLocalityofoffice.Text.Trim = "" Then
                VLocalityofoffice = GV.parseString(txtLocalityofoffice.Text.Trim)
            Else
                VLocalityofoffice = ""
            End If


            If ddlStateofoffice.Items.Count > 0 Then
                If Not ddlStateofoffice.SelectedValue.Trim = "" Then
                    VStateofoffice = GV.parseString(ddlStateofoffice.SelectedValue.Trim)
                Else
                    VStateofoffice = ""
                End If
            End If


            If ddlDistrictofoffice.Items.Count > 0 Then
                If Not ddlDistrictofoffice.SelectedValue.Trim = "" Then
                    VDistrictofoffice = GV.parseString(ddlDistrictofoffice.SelectedValue.Trim)
                Else
                    VDistrictofoffice = ""
                End If
            End If


            If Not txtTalukaAndVillageofoffice.Text.Trim = "" Then
                VTalukaAndVillageofoffice = GV.parseString(txtTalukaAndVillageofoffice.Text.Trim)
            Else
                VTalukaAndVillageofoffice = ""
            End If


            If Not txtPINCodeofoffice.Text.Trim = "" Then
                VPINCodeofoffice = GV.parseString(txtPINCodeofoffice.Text.Trim)
            Else
                VPINCodeofoffice = ""
            End If


            If Not txtofficeMobileNumber.Text.Trim = "" Then
                VofficeMobileNumber = GV.parseString(txtofficeMobileNumber.Text.Trim)
            Else
                VofficeMobileNumber = ""
            End If


            If Not txtofficeemailId.Text.Trim = "" Then
                VofficeemailId = GV.parseString(txtofficeemailId.Text.Trim)
            Else
                VofficeemailId = ""
            End If


            If Not txtDateofEstablishment.Text.Trim = "" Then
                VDateofEstablishment = GV.parseString(txtDateofEstablishment.Text.Trim)
            Else
                VDateofEstablishment = ""
            End If


            If ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.Items.Count > 0 Then
                If Not ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedValue.Trim = "" Then
                    VPreviousEM1_EM2_SSI_UAMRegistrationNumber = GV.parseString(ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedValue.Trim)
                Else
                    VPreviousEM1_EM2_SSI_UAMRegistrationNumber = ""
                End If
            End If


            If Not txtIfAnyEnterNo.Text.Trim = "" Then
                VIfAnyEnterNo = GV.parseString(txtIfAnyEnterNo.Text.Trim)
            Else
                VIfAnyEnterNo = ""
            End If


            If Not txtBankIFSC.Text.Trim = "" Then
                VBankIFSC = GV.parseString(txtBankIFSC.Text.Trim)
            Else
                VBankIFSC = ""
            End If


            If Not txtBankAccNo.Text.Trim = "" Then
                VBankAccNo = GV.parseString(txtBankAccNo.Text.Trim)
            Else
                VBankAccNo = ""
            End If


            If ddlMajorActivityofUnit.Items.Count > 0 Then
                If Not ddlMajorActivityofUnit.SelectedValue.Trim = "" Then
                    VMajorActivityofUnit = GV.parseString(ddlMajorActivityofUnit.SelectedValue.Trim)
                Else
                    VMajorActivityofUnit = ""
                End If
            End If


            If Not txtNICClassificationCode.Text.Trim = "" Then
                VNICClassificationCode = GV.parseString(txtNICClassificationCode.Text.Trim)
            Else
                VNICClassificationCode = ""
            End If


            If Not txtEmployee_Worker.Text.Trim = "" Then
                VEmployee_Worker = GV.parseString(txtEmployee_Worker.Text.Trim)
            Else
                VEmployee_Worker = ""
            End If


            If Not txtInvest.Text.Trim = "" Then
                VInvest = GV.parseString(txtInvest.Text.Trim)
            Else
                VInvest = ""
            End If


            If Not txtDICCenter.Text.Trim = "" Then
                VDICCenter = GV.parseString(txtDICCenter.Text.Trim)
            Else
                VDICCenter = ""
            End If


            If ddlLastYearITR.Items.Count > 0 Then
                If Not ddlLastYearITR.SelectedValue.Trim = "" Then
                    VLastYearITR = GV.parseString(ddlLastYearITR.SelectedValue.Trim)
                Else
                    VLastYearITR = ""
                End If
            End If


            Dim VRecordDateTime, VEntryBy, VUpdatedOn, VUpdatedBy As String

            VRecordDateTime = Now
            VEntryBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = Now
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            '" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.
            ' '" & VRecordDateTime & "','" & VEntryBy & "',
            'RecordDateTime, EntryBy,
            ' UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "',


            If Session("EditFlag") = 0 Then

                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.frm_Udyam_Registration Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.frm_Udyam_Registration (RecordDateTime, EntryBy,EmployerAdhaarNo,FullNameofEmployer,SocialCategory,Gender,PhysicallyHandicapped,NameOfEnterprise,TypeOfOrganisation,PANNumber,AddressofPlant,LocalityofPlant,StateofPlant,DistrictofPlant,TalukaAndVillageofPlant,PINCodeofPlant,Addressofoffice,Localityofoffice,Stateofoffice,Districtofoffice,TalukaAndVillageofoffice,PINCodeofoffice,officeMobileNumber,officeemailId,DateofEstablishment,PreviousEM1_EM2_SSI_UAMRegistrationNumber,IfAnyEnterNo,BankIFSC,BankAccNo,MajorActivityofUnit,NICClassificationCode,Employee_Worker,Invest,DICCenter,LastYearITR) values( '" & VRecordDateTime & "','" & VEntryBy & "','" & VEmployerAdhaarNo & "','" & VFullNameofEmployer & "','" & VSocialCategory & "','" & VGender & "','" & VPhysicallyHandicapped & "','" & VNameOfEnterprise & "','" & VTypeOfOrganisation & "','" & VPANNumber & "','" & VAddressofPlant & "','" & VLocalityofPlant & "','" & VStateofPlant & "','" & VDistrictofPlant & "','" & VTalukaAndVillageofPlant & "','" & VPINCodeofPlant & "','" & VAddressofoffice & "','" & VLocalityofoffice & "','" & VStateofoffice & "','" & VDistrictofoffice & "','" & VTalukaAndVillageofoffice & "','" & VPINCodeofoffice & "','" & VofficeMobileNumber & "','" & VofficeemailId & "','" & VDateofEstablishment & "','" & VPreviousEM1_EM2_SSI_UAMRegistrationNumber & "','" & VIfAnyEnterNo & "','" & VBankIFSC & "','" & VBankAccNo & "','" & VMajorActivityofUnit & "','" & VNICClassificationCode & "','" & VEmployee_Worker & "','" & VInvest & "','" & VDICCenter & "','" & VLastYearITR & "' )"
                If GV.FL.DMLQueries(QryStr) = True Then

                    Clear()

                    lblDialogMsg.Text = "Record Saved Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                Else
                    lblDialogMsg.Text = "Record Insertion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                End If


            ElseIf Session("EditFlag") = 1 Then


                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.frm_Udyam_Registration set UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', EmployerAdhaarNo='" & VEmployerAdhaarNo & "', FullNameofEmployer='" & VFullNameofEmployer & "', SocialCategory='" & VSocialCategory & "', Gender='" & VGender & "', PhysicallyHandicapped='" & VPhysicallyHandicapped & "', NameOfEnterprise='" & VNameOfEnterprise & "', TypeOfOrganisation='" & VTypeOfOrganisation & "', PANNumber='" & VPANNumber & "', AddressofPlant='" & VAddressofPlant & "', LocalityofPlant='" & VLocalityofPlant & "', StateofPlant='" & VStateofPlant & "', DistrictofPlant='" & VDistrictofPlant & "', TalukaAndVillageofPlant='" & VTalukaAndVillageofPlant & "', PINCodeofPlant='" & VPINCodeofPlant & "', Addressofoffice='" & VAddressofoffice & "', Localityofoffice='" & VLocalityofoffice & "', Stateofoffice='" & VStateofoffice & "', Districtofoffice='" & VDistrictofoffice & "', TalukaAndVillageofoffice='" & VTalukaAndVillageofoffice & "', PINCodeofoffice='" & VPINCodeofoffice & "', officeMobileNumber='" & VofficeMobileNumber & "', officeemailId='" & VofficeemailId & "', DateofEstablishment='" & VDateofEstablishment & "', PreviousEM1_EM2_SSI_UAMRegistrationNumber='" & VPreviousEM1_EM2_SSI_UAMRegistrationNumber & "', IfAnyEnterNo='" & VIfAnyEnterNo & "', BankIFSC='" & VBankIFSC & "', BankAccNo='" & VBankAccNo & "', MajorActivityofUnit='" & VMajorActivityofUnit & "', NICClassificationCode='" & VNICClassificationCode & "', Employee_Worker='" & VEmployee_Worker & "', Invest='" & VInvest & "', DICCenter='" & VDICCenter & "', LastYearITR='" & VLastYearITR & "' where RID=" & lblRID.Text.Trim & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    lblDialogMsg.Text = "Record Updated Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnPopupYes.Text = "Ok"
                    btnCancel.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                Else
                    lblDialogMsg.Text = "Process Cann't be Complited."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnPopupYes.Text = "Ok"
                    btnCancel.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                End If
            End If




        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Private Sub Clear()
        Try
            VEmployerAdhaarNo = ""
            VFullNameofEmployer = ""
            VSocialCategory = ""
            VGender = ""
            VPhysicallyHandicapped = ""
            VNameOfEnterprise = ""
            VTypeOfOrganisation = ""
            VPANNumber = ""
            VAddressofPlant = ""
            VLocalityofPlant = ""
            VStateofPlant = ""
            VDistrictofPlant = ""
            VTalukaAndVillageofPlant = ""
            VPINCodeofPlant = ""
            VAddressofoffice = ""
            VLocalityofoffice = ""
            VStateofoffice = ""
            VDistrictofoffice = ""
            VTalukaAndVillageofoffice = ""
            VPINCodeofoffice = ""
            VofficeMobileNumber = ""
            VofficeemailId = ""
            VDateofEstablishment = ""
            VPreviousEM1_EM2_SSI_UAMRegistrationNumber = ""
            VIfAnyEnterNo = ""
            VBankIFSC = ""
            VBankAccNo = ""
            VMajorActivityofUnit = ""
            VNICClassificationCode = ""
            VEmployee_Worker = ""
            VInvest = ""
            VDICCenter = ""
            VLastYearITR = ""
            txtEmployerAdhaarNo.Text = ""

            txtFullNameofEmployer.Text = ""

            If ddlSocialCategory.Items.Count > 0 Then
                ddlSocialCategory.SelectedIndex = 0
            End If

            If ddlGender.Items.Count > 0 Then
                ddlGender.SelectedIndex = 0
            End If

            If ddlPhysicallyHandicapped.Items.Count > 0 Then
                ddlPhysicallyHandicapped.SelectedIndex = 0
            End If

            txtNameOfEnterprise.Text = ""

            txtTypeOfOrganisation.Text = ""

            txtPANNumber.Text = ""

            txtAddressofPlant.Text = ""

            txtLocalityofPlant.Text = ""

            If ddlStateofPlant.Items.Count > 0 Then
                ddlStateofPlant.SelectedIndex = 0
            End If

            If ddlDistrictofPlant.Items.Count > 0 Then
                ddlDistrictofPlant.SelectedIndex = 0
            End If

            txtTalukaAndVillageofPlant.Text = ""

            txtPINCodeofPlant.Text = ""

            txtAddressofoffice.Text = ""

            txtLocalityofoffice.Text = ""

            If ddlStateofoffice.Items.Count > 0 Then
                ddlStateofoffice.SelectedIndex = 0
            End If

            If ddlDistrictofoffice.Items.Count > 0 Then
                ddlDistrictofoffice.SelectedIndex = 0
            End If

            txtTalukaAndVillageofoffice.Text = ""

            txtPINCodeofoffice.Text = ""

            txtofficeMobileNumber.Text = ""

            txtofficeemailId.Text = ""

            txtDateofEstablishment.Text = ""

            If ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.Items.Count > 0 Then
                ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedIndex = 0
            End If

            txtIfAnyEnterNo.Text = ""

            txtBankIFSC.Text = ""

            txtBankAccNo.Text = ""

            If ddlMajorActivityofUnit.Items.Count > 0 Then
                ddlMajorActivityofUnit.SelectedIndex = 0
            End If

            txtNICClassificationCode.Text = ""

            txtEmployee_Worker.Text = ""

            txtInvest.Text = ""

            txtDICCenter.Text = ""

            If ddlLastYearITR.Items.Count > 0 Then
                ddlLastYearITR.SelectedIndex = 0
            End If

            ddlStateofoffice.SelectedValue = "DELHI"
            ddlDistrictofoffice.SelectedValue = "NORTH DELHI"
            ddlStateofPlant.SelectedValue = "DELHI"
            ddlDistrictofPlant.SelectedValue = "NORTH DELHI"



            Session("EditFlag") = 0
            btnSave.Text = "Save"
            btnClear.Text = "Reset"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Text = ""
                lblError.CssClass = ""


                'GV.FL.AddInDropDownListDistinct(ddlStateofoffice, "", "")
                'ddlStateofoffice.Items.Clear()
                GV.FL.AddInDropDownListDistinct(ddlStateofoffice, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrictofoffice, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlStateofoffice.SelectedValue = "DELHI"
                ddlDistrictofoffice.SelectedValue = "NORTH DELHI"


                GV.FL.AddInDropDownListDistinct(ddlStateofPlant, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrictofPlant, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlStateofPlant.SelectedValue = "DELHI"
                ddlDistrictofPlant.SelectedValue = "NORTH DELHI"





                'If ddlState.Items.Count > 0 Then
                '    ddlState.Items.Insert(0, ":::: Select State ::::")
                'Else
                '    ddlState.Items.Add(":::: Select State ::::")
                'End If

                'ddlDistrict.Items.Clear()
                'ddlDistrict.Items.Add(":::: Select District ::::")


                'ddlSocialCategory.Items.Add("GENERAL")
                'ddlSocialCategory.Items.Add("SC")
                'ddlSocialCategory.Items.Add("ST")
                'ddlSocialCategory.Items.Add("OBC")


                Session("EditFlag") = 0
                Session("Workfor") = "Save"
                If Not Session("RecordID") = "" And Session("RecordEdit") = 1 And Session("RecordEditConfirm") = 9 Then

                    Session("RecordEdit") = 0
                    Session("RecordEditConfirm") = -9
                    Session("EditFlag") = 1
                    Session("Workfor") = "Update"
                    lblRID.Text = Session("RecordID")
                    'UsrRightsDS = FL.OpenDs("OSIL_UserRights_Master where User_ID='" & Session("UserName") & "' and FormName='" & Me.Page.Request.AppRelativeCurrentExecutionFilePath & "'")
                    'applySavingRightOnForm(UsrRightsDS, btnSave)
                    'applyUpdatingRightOnForm(UsrRightsDS, btnSave)
                    btnDelete.Enabled = True
                    btnSave.Text = "Update"
                    btnClear.Text = "Close"
                    btnDelete.Enabled = True
                    formheading_1.Text = "Edit UDYAM REGISTRATION "
                    DS = GV.FL.OpenDs(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.frm_Udyam_Registration where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmployerAdhaarNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("EmployerAdhaarNo").ToString() = "" Then
                                    txtEmployerAdhaarNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EmployerAdhaarNo").ToString())
                                Else
                                    txtEmployerAdhaarNo.Text = ""
                                End If
                            Else
                                txtEmployerAdhaarNo.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("FullNameofEmployer")) Then
                                If Not DS.Tables(0).Rows(0).Item("FullNameofEmployer").ToString() = "" Then
                                    txtFullNameofEmployer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("FullNameofEmployer").ToString())
                                Else
                                    txtFullNameofEmployer.Text = ""
                                End If
                            Else
                                txtFullNameofEmployer.Text = ""
                            End If

                            If ddlSocialCategory.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("SocialCategory")) Then
                                    If Not DS.Tables(0).Rows(0).Item("SocialCategory").ToString() = "" Then
                                        ddlSocialCategory.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("SocialCategory").ToString())
                                    Else
                                        ddlSocialCategory.SelectedIndex = 0
                                    End If
                                Else
                                    ddlSocialCategory.SelectedIndex = 0
                                End If
                            End If

                            If ddlGender.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Gender")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Gender").ToString() = "" Then
                                        ddlGender.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Gender").ToString())
                                    Else
                                        ddlGender.SelectedIndex = 0
                                    End If
                                Else
                                    ddlGender.SelectedIndex = 0
                                End If
                            End If

                            If ddlPhysicallyHandicapped.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("PhysicallyHandicapped")) Then
                                    If Not DS.Tables(0).Rows(0).Item("PhysicallyHandicapped").ToString() = "" Then
                                        ddlPhysicallyHandicapped.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("PhysicallyHandicapped").ToString())
                                    Else
                                        ddlPhysicallyHandicapped.SelectedIndex = 0
                                    End If
                                Else
                                    ddlPhysicallyHandicapped.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NameOfEnterprise")) Then
                                If Not DS.Tables(0).Rows(0).Item("NameOfEnterprise").ToString() = "" Then
                                    txtNameOfEnterprise.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NameOfEnterprise").ToString())
                                Else
                                    txtNameOfEnterprise.Text = ""
                                End If
                            Else
                                txtNameOfEnterprise.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("TypeOfOrganisation")) Then
                                If Not DS.Tables(0).Rows(0).Item("TypeOfOrganisation").ToString() = "" Then
                                    txtTypeOfOrganisation.Text = GV.parseString(DS.Tables(0).Rows(0).Item("TypeOfOrganisation").ToString())
                                Else
                                    txtTypeOfOrganisation.Text = ""
                                End If
                            Else
                                txtTypeOfOrganisation.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PANNumber")) Then
                                If Not DS.Tables(0).Rows(0).Item("PANNumber").ToString() = "" Then
                                    txtPANNumber.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PANNumber").ToString())
                                Else
                                    txtPANNumber.Text = ""
                                End If
                            Else
                                txtPANNumber.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AddressofPlant")) Then
                                If Not DS.Tables(0).Rows(0).Item("AddressofPlant").ToString() = "" Then
                                    txtAddressofPlant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AddressofPlant").ToString())
                                Else
                                    txtAddressofPlant.Text = ""
                                End If
                            Else
                                txtAddressofPlant.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("LocalityofPlant")) Then
                                If Not DS.Tables(0).Rows(0).Item("LocalityofPlant").ToString() = "" Then
                                    txtLocalityofPlant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("LocalityofPlant").ToString())
                                Else
                                    txtLocalityofPlant.Text = ""
                                End If
                            Else
                                txtLocalityofPlant.Text = ""
                            End If

                            If ddlStateofPlant.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("StateofPlant")) Then
                                    If Not DS.Tables(0).Rows(0).Item("StateofPlant").ToString() = "" Then
                                        ddlStateofPlant.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("StateofPlant").ToString())
                                    Else
                                        ddlStateofPlant.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStateofPlant.SelectedIndex = 0
                                End If
                            End If

                            If ddlDistrictofPlant.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("DistrictofPlant")) Then
                                    If Not DS.Tables(0).Rows(0).Item("DistrictofPlant").ToString() = "" Then
                                        ddlDistrictofPlant.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("DistrictofPlant").ToString())
                                    Else
                                        ddlDistrictofPlant.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrictofPlant.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("TalukaAndVillageofPlant")) Then
                                If Not DS.Tables(0).Rows(0).Item("TalukaAndVillageofPlant").ToString() = "" Then
                                    txtTalukaAndVillageofPlant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("TalukaAndVillageofPlant").ToString())
                                Else
                                    txtTalukaAndVillageofPlant.Text = ""
                                End If
                            Else
                                txtTalukaAndVillageofPlant.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PINCodeofPlant")) Then
                                If Not DS.Tables(0).Rows(0).Item("PINCodeofPlant").ToString() = "" Then
                                    txtPINCodeofPlant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PINCodeofPlant").ToString())
                                Else
                                    txtPINCodeofPlant.Text = ""
                                End If
                            Else
                                txtPINCodeofPlant.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Addressofoffice")) Then
                                If Not DS.Tables(0).Rows(0).Item("Addressofoffice").ToString() = "" Then
                                    txtAddressofoffice.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Addressofoffice").ToString())
                                Else
                                    txtAddressofoffice.Text = ""
                                End If
                            Else
                                txtAddressofoffice.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Localityofoffice")) Then
                                If Not DS.Tables(0).Rows(0).Item("Localityofoffice").ToString() = "" Then
                                    txtLocalityofoffice.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Localityofoffice").ToString())
                                Else
                                    txtLocalityofoffice.Text = ""
                                End If
                            Else
                                txtLocalityofoffice.Text = ""
                            End If

                            If ddlStateofoffice.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Stateofoffice")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Stateofoffice").ToString() = "" Then
                                        ddlStateofoffice.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Stateofoffice").ToString())
                                    Else
                                        ddlStateofoffice.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStateofoffice.SelectedIndex = 0
                                End If
                            End If

                            If ddlDistrictofoffice.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Districtofoffice")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Districtofoffice").ToString() = "" Then
                                        ddlDistrictofoffice.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Districtofoffice").ToString())
                                    Else
                                        ddlDistrictofoffice.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrictofoffice.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("TalukaAndVillageofoffice")) Then
                                If Not DS.Tables(0).Rows(0).Item("TalukaAndVillageofoffice").ToString() = "" Then
                                    txtTalukaAndVillageofoffice.Text = GV.parseString(DS.Tables(0).Rows(0).Item("TalukaAndVillageofoffice").ToString())
                                Else
                                    txtTalukaAndVillageofoffice.Text = ""
                                End If
                            Else
                                txtTalukaAndVillageofoffice.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PINCodeofoffice")) Then
                                If Not DS.Tables(0).Rows(0).Item("PINCodeofoffice").ToString() = "" Then
                                    txtPINCodeofoffice.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PINCodeofoffice").ToString())
                                Else
                                    txtPINCodeofoffice.Text = ""
                                End If
                            Else
                                txtPINCodeofoffice.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("officeMobileNumber")) Then
                                If Not DS.Tables(0).Rows(0).Item("officeMobileNumber").ToString() = "" Then
                                    txtofficeMobileNumber.Text = GV.parseString(DS.Tables(0).Rows(0).Item("officeMobileNumber").ToString())
                                Else
                                    txtofficeMobileNumber.Text = ""
                                End If
                            Else
                                txtofficeMobileNumber.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("officeemailId")) Then
                                If Not DS.Tables(0).Rows(0).Item("officeemailId").ToString() = "" Then
                                    txtofficeemailId.Text = GV.parseString(DS.Tables(0).Rows(0).Item("officeemailId").ToString())
                                Else
                                    txtofficeemailId.Text = ""
                                End If
                            Else
                                txtofficeemailId.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("DateofEstablishment")) Then
                                If Not DS.Tables(0).Rows(0).Item("DateofEstablishment").ToString() = "" Then
                                    txtDateofEstablishment.Text = GV.parseString(DS.Tables(0).Rows(0).Item("DateofEstablishment").ToString())
                                Else
                                    txtDateofEstablishment.Text = ""
                                End If
                            Else
                                txtDateofEstablishment.Text = ""
                            End If

                            If ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("PreviousEM1_EM2_SSI_UAMRegistrationNumber")) Then
                                    If Not DS.Tables(0).Rows(0).Item("PreviousEM1_EM2_SSI_UAMRegistrationNumber").ToString() = "" Then
                                        ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("PreviousEM1_EM2_SSI_UAMRegistrationNumber").ToString())
                                    Else
                                        ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedIndex = 0
                                    End If
                                Else
                                    ddlPreviousEM1_EM2_SSI_UAMRegistrationNumber.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("IfAnyEnterNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("IfAnyEnterNo").ToString() = "" Then
                                    txtIfAnyEnterNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("IfAnyEnterNo").ToString())
                                Else
                                    txtIfAnyEnterNo.Text = ""
                                End If
                            Else
                                txtIfAnyEnterNo.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("BankIFSC")) Then
                                If Not DS.Tables(0).Rows(0).Item("BankIFSC").ToString() = "" Then
                                    txtBankIFSC.Text = GV.parseString(DS.Tables(0).Rows(0).Item("BankIFSC").ToString())
                                Else
                                    txtBankIFSC.Text = ""
                                End If
                            Else
                                txtBankIFSC.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("BankAccNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("BankAccNo").ToString() = "" Then
                                    txtBankAccNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("BankAccNo").ToString())
                                Else
                                    txtBankAccNo.Text = ""
                                End If
                            Else
                                txtBankAccNo.Text = ""
                            End If

                            If ddlMajorActivityofUnit.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("MajorActivityofUnit")) Then
                                    If Not DS.Tables(0).Rows(0).Item("MajorActivityofUnit").ToString() = "" Then
                                        ddlMajorActivityofUnit.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("MajorActivityofUnit").ToString())
                                    Else
                                        ddlMajorActivityofUnit.SelectedIndex = 0
                                    End If
                                Else
                                    ddlMajorActivityofUnit.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NICClassificationCode")) Then
                                If Not DS.Tables(0).Rows(0).Item("NICClassificationCode").ToString() = "" Then
                                    txtNICClassificationCode.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NICClassificationCode").ToString())
                                Else
                                    txtNICClassificationCode.Text = ""
                                End If
                            Else
                                txtNICClassificationCode.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Employee_Worker")) Then
                                If Not DS.Tables(0).Rows(0).Item("Employee_Worker").ToString() = "" Then
                                    txtEmployee_Worker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Employee_Worker").ToString())
                                Else
                                    txtEmployee_Worker.Text = ""
                                End If
                            Else
                                txtEmployee_Worker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Invest")) Then
                                If Not DS.Tables(0).Rows(0).Item("Invest").ToString() = "" Then
                                    txtInvest.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Invest").ToString())
                                Else
                                    txtInvest.Text = ""
                                End If
                            Else
                                txtInvest.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("DICCenter")) Then
                                If Not DS.Tables(0).Rows(0).Item("DICCenter").ToString() = "" Then
                                    txtDICCenter.Text = GV.parseString(DS.Tables(0).Rows(0).Item("DICCenter").ToString())
                                Else
                                    txtDICCenter.Text = ""
                                End If
                            Else
                                txtDICCenter.Text = ""
                            End If

                            If ddlLastYearITR.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("LastYearITR")) Then
                                    If Not DS.Tables(0).Rows(0).Item("LastYearITR").ToString() = "" Then
                                        ddlLastYearITR.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("LastYearITR").ToString())
                                    Else
                                        ddlLastYearITR.SelectedIndex = 0
                                    End If
                                Else
                                    ddlLastYearITR.SelectedIndex = 0
                                End If
                            End If

                        End If
                    End If
                Else
                    'UsrRightsDS = New DataSet
                    'UsrRightsDS = FL.OpenDs("OSIL_UserRights_Master  where User_ID='" & Session("UserName") & "' and FormName='" & Me.Page.Request.AppRelativeCurrentExecutionFilePath & "'")
                    'applySavingRightOnForm(UsrRightsDS, btnSave)
                    btnSave.Text = "Save"
                    btnClear.Text = "Reset"
                    btnDelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Private Sub btnDelete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Session("Workfor") = "Delete"
            lblDialogMsg.Text = "Are you sure you want to delete ?"
            lblDialogMsg.CssClass = ""
            btnCancel.Text = "No"
            btnCancel.Attributes("Style") = ""
            btnPopupYes.Text = "Yes"
            ModalPopupExtender1.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

End Class