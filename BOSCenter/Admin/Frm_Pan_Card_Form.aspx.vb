Public Class Frm_Pan_Card_Form
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VSelectTitle, VApplicantFirstName, VApplicantMiddleName, VApplicantLastName, VApplicantFatherFirstName, VApplicantFatherMiddleName, VApplicantFatherLastName, VDOB, VGender, VCardName, VAadhaarNumber, VNameonAadhaar, VMobileNumber, VEmail, VPanDeliveryState, VProofOfIdentity, VProofOfAddress, VProofOfDOB, VHouseNo_Building_Landmark, VStatusofapplicant, VRegistrationNumberforcompany_firms_LLPs, VSourceofIncome, VVillage, VPost1, VPost2, VDistrict, VDistrictArea, VAREACODE, VAOType, VRANGECODE, VAONO, VState, VPin As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Dim isErrorFound As Boolean = False
    Dim isFocusApplied As Boolean = False
    Dim VError_Sring As String
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("NameTheForm.aspx") '/Change the name of form
            Else
                ddlSelectTitle.CssClass = "form-control"
                ddlPanDeliveryState.CssClass = "form-control"
                ddlDistrict.CssClass = "form-control"
                ddlDistrictArea.CssClass = "form-control"
                ddlGender.CssClass = "form-control"
                ddlProofOfAddress.CssClass = "form-control"
                ddlProofOfDOB.CssClass = "form-control"
                ddlProofOfIdentity.CssClass = "form-control"
                ddlSourceofIncome.CssClass = "form-control"
                ddlState.CssClass = "form-control"
                ddlStatusofapplicant.CssClass = "form-control"
                txtAadhaarNumber.CssClass = "form-control"
                txtApplicantFirstName.CssClass = "form-control"
                txtApplicantMiddleName.CssClass = "form-control"
                txtApplicantLastName.CssClass = "form-control"
                txtApplicantFatherFirstName.CssClass = "form-control"
                txtApplicantFatherLastName.CssClass = "form-control"
                txtApplicantFatherMiddleName.CssClass = "form-control"
                txtDOB.CssClass = "form-control"
                txtCardName.CssClass = "form-control"
                txtNameonAadhaar.CssClass = "form-control"
                txtMobileNumber.CssClass = "form-control"
                txtEmail.CssClass = "form-control"
                txtHouseNo_Building_Landmark.CssClass = "form-control"
                txtAREACODE.CssClass = "form-control"
                txtRegistrationNumberforcompany_firms_LLPs.CssClass = "form-control"
                txtVillage.CssClass = "form-control"
                txtPost1.CssClass = "form-control"
                txtPost2.CssClass = "form-control"
                txtAOType.CssClass = "form-control"
                txtAONO.CssClass = "form-control"
                txtPin.CssClass = "form-control"
                txtRANGECODE.CssClass = "form-control"

                VSelectTitle = ""
                VApplicantFirstName = ""
                VApplicantMiddleName = ""
                VApplicantLastName = ""
                VApplicantFatherFirstName = ""
                VApplicantFatherMiddleName = ""
                VApplicantFatherLastName = ""
                VDOB = ""
                VGender = ""
                VCardName = ""
                VAadhaarNumber = ""
                VNameonAadhaar = ""
                VMobileNumber = ""
                VEmail = ""
                VPanDeliveryState = ""
                VProofOfIdentity = ""
                VProofOfAddress = ""
                VProofOfDOB = ""
                VHouseNo_Building_Landmark = ""
                VStatusofapplicant = ""
                VRegistrationNumberforcompany_firms_LLPs = ""
                VSourceofIncome = ""
                VVillage = ""
                VPost1 = ""
                VPost2 = ""
                VDistrict = ""
                VDistrictArea = ""
                VAREACODE = ""
                VAOType = ""
                VRANGECODE = ""
                VAONO = ""
                VState = ""
                VPin = ""
                lblError.Text = ""
                lblError.CssClass = ""
                If ddlSelectTitle.Items.Count > 0 Then
                    ddlSelectTitle.SelectedIndex = 0
                End If

                txtApplicantFirstName.Text = ""

                txtApplicantMiddleName.Text = ""

                txtApplicantLastName.Text = ""

                txtApplicantFatherFirstName.Text = ""

                txtApplicantFatherMiddleName.Text = ""

                txtApplicantFatherLastName.Text = ""

                txtDOB.Text = ""

                If ddlGender.Items.Count > 0 Then
                    ddlGender.SelectedIndex = 0
                End If

                txtCardName.Text = ""

                txtAadhaarNumber.Text = ""

                txtNameonAadhaar.Text = ""

                txtMobileNumber.Text = ""

                txtEmail.Text = ""

                If ddlPanDeliveryState.Items.Count > 0 Then
                    ddlPanDeliveryState.SelectedIndex = 0
                End If

                If ddlProofOfIdentity.Items.Count > 0 Then
                    ddlProofOfIdentity.SelectedIndex = 0
                End If

                If ddlProofOfAddress.Items.Count > 0 Then
                    ddlProofOfAddress.SelectedIndex = 0
                End If

                If ddlProofOfDOB.Items.Count > 0 Then
                    ddlProofOfDOB.SelectedIndex = 0
                End If

                txtHouseNo_Building_Landmark.Text = ""

                If ddlStatusofapplicant.Items.Count > 0 Then
                    ddlStatusofapplicant.SelectedIndex = 0
                End If

                txtRegistrationNumberforcompany_firms_LLPs.Text = ""

                If ddlSourceofIncome.Items.Count > 0 Then
                    ddlSourceofIncome.SelectedIndex = 0
                End If

                txtVillage.Text = ""

                txtPost1.Text = ""

                txtPost2.Text = ""

                If ddlDistrict.Items.Count > 0 Then
                    ddlDistrict.SelectedIndex = 0
                End If

                If ddlDistrictArea.Items.Count > 0 Then
                    ddlDistrictArea.SelectedIndex = 0
                End If

                txtAREACODE.Text = ""

                txtAOType.Text = ""

                txtRANGECODE.Text = ""

                txtAONO.Text = ""

                If ddlState.Items.Count > 0 Then
                    ddlState.SelectedIndex = 0
                End If

                txtPin.Text = ""
                ddlState.SelectedValue = "DELHI"
                ddlDistrictArea.SelectedValue = "NORTH DELHI"
                ddlPanDeliveryState.SelectedValue = "DELHI"
                ddlDistrict.SelectedValue = "NORTH DELHI"


                Session("EditFlag") = 0
                btnSave.Text = "Save"
                btnClear.Text = "Reset"
                lblError.Text = ""
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

            If GV.parseString(ddlSelectTitle.SelectedValue) = "" Then
                ddlSelectTitle.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlSelectTitle.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlSelectTitle.CssClass = "form-control"
                VSelectTitle = GV.parseString(ddlSelectTitle.SelectedValue)
            End If

            If GV.parseString(txtApplicantFirstName.Text) = "" Then
                txtApplicantFirstName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantFirstName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantFirstName.CssClass = "form-control"
                VApplicantFirstName = GV.parseString(txtApplicantFirstName.Text)
            End If

            If GV.parseString(txtApplicantMiddleName.Text) = "" Then
                txtApplicantMiddleName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantMiddleName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantMiddleName.CssClass = "form-control"
                VApplicantMiddleName = GV.parseString(txtApplicantMiddleName.Text)
            End If

            If GV.parseString(txtApplicantLastName.Text) = "" Then
                txtApplicantLastName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantLastName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantLastName.CssClass = "form-control"
                VApplicantLastName = GV.parseString(txtApplicantLastName.Text)
            End If

            If GV.parseString(txtApplicantLastName.Text) = "" Then
                txtApplicantLastName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantLastName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantLastName.CssClass = "form-control"
                VApplicantLastName = GV.parseString(txtApplicantLastName.Text)
            End If

            If GV.parseString(txtApplicantFatherFirstName.Text) = "" Then
                txtApplicantFatherFirstName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantFatherFirstName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantFatherFirstName.CssClass = "form-control"
                VApplicantFatherFirstName = GV.parseString(txtApplicantFatherFirstName.Text)
            End If

            If GV.parseString(txtApplicantFatherMiddleName.Text) = "" Then
                txtApplicantFatherMiddleName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantFatherMiddleName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantFatherMiddleName.CssClass = "form-control"
                VApplicantFatherMiddleName = GV.parseString(txtApplicantFatherMiddleName.Text)
            End If

            If GV.parseString(txtApplicantFatherLastName.Text) = "" Then
                txtApplicantFatherLastName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantFatherLastName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantFatherLastName.CssClass = "form-control"
                VApplicantFatherLastName = GV.parseString(txtApplicantFatherLastName.Text)
            End If

            If GV.parseString(txtDOB.Text) = "" Then
                txtDOB.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtDOB.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsDate(GV.FL.returnDateMonthWiseWithDateChecking(txtDOB.Text)) = True Then
                txtDOB.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Correct Date Format."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Correct Date Format."
                End If
                If isFocusApplied = False Then
                    txtDOB.Focus()
                    isFocusApplied = True
                End If
            Else
                txtDOB.CssClass = "form-control"
                VDOB = GV.FL.returnDateMonthWiseWithDateChecking(txtDOB.Text.Trim)
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

            If GV.parseString(txtCardName.Text) = "" Then
                txtCardName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtCardName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtCardName.CssClass = "form-control"
                VCardName = GV.parseString(txtCardName.Text)
            End If

            If GV.parseString(txtAadhaarNumber.Text) = "" Then
                txtAadhaarNumber.CssClass = "ValidationError"
                isErrorFound = True

                If isFocusApplied = False Then
                    txtAadhaarNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtAadhaarNumber.Text) Then
                txtAadhaarNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Numaric No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Numaric No."
                End If
                If isFocusApplied = False Then
                    txtAadhaarNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtAadhaarNumber.text).Length = 12 Then
                txtAadhaarNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter 12 Digit Aadhar No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter 12 Digit Aadhar No."
                End If
                If isFocusApplied = False Then
                    txtAadhaarNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAadhaarNumber.CssClass = "form-control"
                VAadhaarNumber = GV.parseString(txtAadhaarNumber.Text)
            End If

            If GV.parseString(txtNameonAadhaar.Text) = "" Then
                txtNameonAadhaar.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNameonAadhaar.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNameonAadhaar.CssClass = "form-control"
                VNameonAadhaar = GV.parseString(txtNameonAadhaar.Text)
            End If

            If GV.parseString(txtMobileNumber.Text) = "" Then
                txtMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtMobileNumber.text) Then
                txtMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Correct Mobile No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Correct Mobile No."
                End If
                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtMobileNumber.text).Length = 10 Then
                txtMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter 10 Digit Mobile No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter 10 Digit Mobile No."
                End If
                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtMobileNumber.CssClass = "form-control"
                VMobileNumber = GV.parseString(txtMobileNumber.Text)
            End If

            If GV.parseString(txtEmail.Text) = "" Then
                txtEmail.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmail.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEmail.CssClass = "form-control"
                VEmail = GV.parseString(txtEmail.Text)
            End If

            If GV.parseString(ddlPanDeliveryState.SelectedValue) = "" Then
                ddlPanDeliveryState.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlPanDeliveryState.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlPanDeliveryState.CssClass = "form-control"
                VPanDeliveryState = GV.parseString(ddlPanDeliveryState.SelectedValue)
            End If

            If GV.parseString(ddlProofOfIdentity.SelectedValue) = "" Then
                ddlProofOfIdentity.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlProofOfIdentity.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlProofOfIdentity.CssClass = "form-control"
                VProofOfIdentity = GV.parseString(ddlProofOfIdentity.SelectedValue)
            End If

            If GV.parseString(ddlProofOfAddress.SelectedValue) = "" Then
                ddlProofOfAddress.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlProofOfAddress.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlProofOfAddress.CssClass = "form-control"
                VProofOfAddress = GV.parseString(ddlProofOfAddress.SelectedValue)
            End If

            If GV.parseString(ddlProofOfDOB.SelectedValue) = "" Then
                ddlProofOfDOB.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlProofOfDOB.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlProofOfDOB.CssClass = "form-control"
                VProofOfDOB = GV.parseString(ddlProofOfDOB.SelectedValue)
            End If

            If GV.parseString(txtHouseNo_Building_Landmark.Text) = "" Then
                txtHouseNo_Building_Landmark.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtHouseNo_Building_Landmark.Focus()
                    isFocusApplied = True
                End If
            Else
                txtHouseNo_Building_Landmark.CssClass = "form-control"
                VHouseNo_Building_Landmark = GV.parseString(txtHouseNo_Building_Landmark.Text)
            End If

            If GV.parseString(ddlStatusofapplicant.SelectedValue) = "" Then
                ddlStatusofapplicant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStatusofapplicant.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStatusofapplicant.CssClass = "form-control"
                VStatusofapplicant = GV.parseString(ddlStatusofapplicant.SelectedValue)
            End If

            If GV.parseString(txtRegistrationNumberforcompany_firms_LLPs.Text) = "" Then
                txtRegistrationNumberforcompany_firms_LLPs.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtRegistrationNumberforcompany_firms_LLPs.Focus()
                    isFocusApplied = True
                End If
            Else
                txtRegistrationNumberforcompany_firms_LLPs.CssClass = "form-control"
                VRegistrationNumberforcompany_firms_LLPs = GV.parseString(txtRegistrationNumberforcompany_firms_LLPs.Text)
            End If

            If GV.parseString(ddlSourceofIncome.SelectedValue) = "" Then
                ddlSourceofIncome.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlSourceofIncome.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlSourceofIncome.CssClass = "form-control"
                VSourceofIncome = GV.parseString(ddlSourceofIncome.SelectedValue)
            End If

            If GV.parseString(txtVillage.Text) = "" Then
                txtVillage.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtVillage.Focus()
                    isFocusApplied = True
                End If
            Else
                txtVillage.CssClass = "form-control"
                VVillage = GV.parseString(txtVillage.Text)
            End If

            If GV.parseString(txtPost1.Text) = "" Then
                txtPost1.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPost1.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPost1.CssClass = "form-control"
                VPost1 = GV.parseString(txtPost1.Text)
            End If

            If GV.parseString(txtPost2.Text) = "" Then
                txtPost2.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPost2.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPost2.CssClass = "form-control"
                VPost2 = GV.parseString(txtPost2.Text)
            End If

            If GV.parseString(ddlDistrict.SelectedValue) = "" Then
                ddlDistrict.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrict.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrict.CssClass = "form-control"
                VDistrict = GV.parseString(ddlDistrict.SelectedValue)
            End If

            If GV.parseString(ddlDistrictArea.SelectedValue) = "" Then
                ddlDistrictArea.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrictArea.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrictArea.CssClass = "form-control"
                VDistrictArea = GV.parseString(ddlDistrictArea.SelectedValue)
            End If

            If GV.parseString(txtAREACODE.Text) = "" Then
                txtAREACODE.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAREACODE.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAREACODE.CssClass = "form-control"
                VAREACODE = GV.parseString(txtAREACODE.Text)
            End If

            If GV.parseString(txtAOType.Text) = "" Then
                txtAOType.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAOType.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAOType.CssClass = "form-control"
                VAOType = GV.parseString(txtAOType.Text)
            End If

            If GV.parseString(txtRANGECODE.Text) = "" Then
                txtRANGECODE.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtRANGECODE.Focus()
                    isFocusApplied = True
                End If
            Else
                txtRANGECODE.CssClass = "form-control"
                VRANGECODE = GV.parseString(txtRANGECODE.Text)
            End If

            If GV.parseString(txtAONO.Text) = "" Then
                txtAONO.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAONO.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAONO.CssClass = "form-control"
                VAONO = GV.parseString(txtAONO.Text)
            End If

            If GV.parseString(ddlState.SelectedValue) = "" Then
                ddlState.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlState.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlState.CssClass = "form-control"
                VState = GV.parseString(ddlState.SelectedValue)
            End If

            If GV.parseString(txtPin.Text) = "" Then
                txtPin.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPin.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPin.CssClass = "form-control"
                VPin = GV.parseString(txtPin.Text)
            End If

            If isErrorFound = True Then
                If Not VError_Sring.Trim = "" Then
                    lblError.Text = VError_Sring
                    lblError.CssClass = "errorlabels"
                Else
                    lblError.CssClass = False
                End If
                lblError.Visible = True
                Exit Sub
            End If
            '////////// End Bulk Validation
            '///////// PopUp
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
            '///////// PopUp

            'lblError.Text = ""
            'lblError.CssClass = ""
            'If ddlSelectTitle.Items.Count > 0 Then
            '    If Not ddlSelectTitle.SelectedValue.Trim = "" Then
            '        VSelectTitle = GV.parseString(ddlSelectTitle.SelectedValue.Trim)
            '    Else
            '        VSelectTitle = ""
            '    End If
            'End If

            'If Not txtApplicantFirstName.Text.Trim = "" Then
            '    VApplicantFirstName = GV.parseString(txtApplicantFirstName.Text.Trim)
            'Else
            '    VApplicantFirstName = ""
            'End If

            'If Not txtApplicantMiddleName.Text.Trim = "" Then
            '    VApplicantMiddleName = GV.parseString(txtApplicantMiddleName.Text.Trim)
            'Else
            '    VApplicantMiddleName = ""
            'End If

            'If Not txtApplicantLastName.Text.Trim = "" Then
            '    VApplicantLastName = GV.parseString(txtApplicantLastName.Text.Trim)
            'Else
            '    VApplicantLastName = ""
            'End If

            'If Not txtApplicantFatherFirstName.Text.Trim = "" Then
            '    VApplicantFatherFirstName = GV.parseString(txtApplicantFatherFirstName.Text.Trim)
            'Else
            '    VApplicantFatherFirstName = ""
            'End If

            'If Not txtApplicantFatherMiddleName.Text.Trim = "" Then
            '    VApplicantFatherMiddleName = GV.parseString(txtApplicantFatherMiddleName.Text.Trim)
            'Else
            '    VApplicantFatherMiddleName = ""
            'End If

            'If Not txtApplicantFatherLastName.Text.Trim = "" Then
            '    VApplicantFatherLastName = GV.parseString(txtApplicantFatherLastName.Text.Trim)
            'Else
            '    VApplicantFatherLastName = ""
            'End If

            'If Not txtDOB.Text.Trim = "" Then
            '    VDOB = GV.parseString(txtDOB.Text.Trim)
            'Else
            '    VDOB = ""
            'End If

            'If ddlGender.Items.Count > 0 Then
            '    If Not ddlGender.SelectedValue.Trim = "" Then
            '        VGender = GV.parseString(ddlGender.SelectedValue.Trim)
            '    Else
            '        VGender = ""
            '    End If
            'End If

            'If Not txtCardName.Text.Trim = "" Then
            '    VCardName = GV.parseString(txtCardName.Text.Trim)
            'Else
            '    VCardName = ""
            'End If

            'If Not txtAadhaarNumber.Text.Trim = "" Then
            '    VAadhaarNumber = GV.parseString(txtAadhaarNumber.Text.Trim)
            'Else
            '    VAadhaarNumber = ""
            'End If

            'If Not txtNameonAadhaar.Text.Trim = "" Then
            '    VNameonAadhaar = GV.parseString(txtNameonAadhaar.Text.Trim)
            'Else
            '    VNameonAadhaar = ""
            'End If

            'If Not txtMobileNumber.Text.Trim = "" Then
            '    VMobileNumber = GV.parseString(txtMobileNumber.Text.Trim)
            'Else
            '    VMobileNumber = ""
            'End If

            'If Not txtEmail.Text.Trim = "" Then
            '    VEmail = GV.parseString(txtEmail.Text.Trim)
            'Else
            '    VEmail = ""
            'End If

            'If ddlPanDeliveryState.Items.Count > 0 Then
            '    If Not ddlPanDeliveryState.SelectedValue.Trim = "" Then
            '        VPanDeliveryState = GV.parseString(ddlPanDeliveryState.SelectedValue.Trim)
            '    Else
            '        VPanDeliveryState = ""
            '    End If
            'End If

            'If ddlProofOfIdentity.Items.Count > 0 Then
            '    If Not ddlProofOfIdentity.SelectedValue.Trim = "" Then
            '        VProofOfIdentity = GV.parseString(ddlProofOfIdentity.SelectedValue.Trim)
            '    Else
            '        VProofOfIdentity = ""
            '    End If
            'End If

            'If ddlProofOfAddress.Items.Count > 0 Then
            '    If Not ddlProofOfAddress.SelectedValue.Trim = "" Then
            '        VProofOfAddress = GV.parseString(ddlProofOfAddress.SelectedValue.Trim)
            '    Else
            '        VProofOfAddress = ""
            '    End If
            'End If

            'If ddlProofOfDOB.Items.Count > 0 Then
            '    If Not ddlProofOfDOB.SelectedValue.Trim = "" Then
            '        VProofOfDOB = GV.parseString(ddlProofOfDOB.SelectedValue.Trim)
            '    Else
            '        VProofOfDOB = ""
            '    End If
            'End If

            'If Not txtHouseNo_Building_Landmark.Text.Trim = "" Then
            '    VHouseNo_Building_Landmark = GV.parseString(txtHouseNo_Building_Landmark.Text.Trim)
            'Else
            '    VHouseNo_Building_Landmark = ""
            'End If

            'If ddlStatusofapplicant.Items.Count > 0 Then
            '    If Not ddlStatusofapplicant.SelectedValue.Trim = "" Then
            '        VStatusofapplicant = GV.parseString(ddlStatusofapplicant.SelectedValue.Trim)
            '    Else
            '        VStatusofapplicant = ""
            '    End If
            'End If

            'If Not txtRegistrationNumberforcompany_firms_LLPs.Text.Trim = "" Then
            '    VRegistrationNumberforcompany_firms_LLPs = GV.parseString(txtRegistrationNumberforcompany_firms_LLPs.Text.Trim)
            'Else
            '    VRegistrationNumberforcompany_firms_LLPs = ""
            'End If

            'If ddlSourceofIncome.Items.Count > 0 Then
            '    If Not ddlSourceofIncome.SelectedValue.Trim = "" Then
            '        VSourceofIncome = GV.parseString(ddlSourceofIncome.SelectedValue.Trim)
            '    Else
            '        VSourceofIncome = ""
            '    End If
            'End If

            'If Not txtVillage.Text.Trim = "" Then
            '    VVillage = GV.parseString(txtVillage.Text.Trim)
            'Else
            '    VVillage = ""
            'End If

            'If Not txtPost1.Text.Trim = "" Then
            '    VPost1 = GV.parseString(txtPost1.Text.Trim)
            'Else
            '    VPost1 = ""
            'End If

            'If Not txtPost2.Text.Trim = "" Then
            '    VPost2 = GV.parseString(txtPost2.Text.Trim)
            'Else
            '    VPost2 = ""
            'End If

            'If ddlDistrict.Items.Count > 0 Then
            '    If Not ddlDistrict.SelectedValue.Trim = "" Then
            '        VDistrict = GV.parseString(ddlDistrict.SelectedValue.Trim)
            '    Else
            '        VDistrict = ""
            '    End If
            'End If

            'If ddlDistrictArea.Items.Count > 0 Then
            '    If Not ddlDistrictArea.SelectedValue.Trim = "" Then
            '        VDistrictArea = GV.parseString(ddlDistrictArea.SelectedValue.Trim)
            '    Else
            '        VDistrictArea = ""
            '    End If
            'End If

            'If Not txtAREACODE.Text.Trim = "" Then
            '    VAREACODE = GV.parseString(txtAREACODE.Text.Trim)
            'Else
            '    VAREACODE = ""
            'End If

            'If Not txtAOType.Text.Trim = "" Then
            '    VAOType = GV.parseString(txtAOType.Text.Trim)
            'Else
            '    VAOType = ""
            'End If

            'If Not txtRANGECODE.Text.Trim = "" Then
            '    VRANGECODE = GV.parseString(txtRANGECODE.Text.Trim)
            'Else
            '    VRANGECODE = ""
            'End If

            'If Not txtAONO.Text.Trim = "" Then
            '    VAONO = GV.parseString(txtAONO.Text.Trim)
            'Else
            '    VAONO = ""
            'End If

            'If ddlState.Items.Count > 0 Then
            '    If Not ddlState.SelectedValue.Trim = "" Then
            '        VState = GV.parseString(ddlState.SelectedValue.Trim)
            '    Else
            '        VState = ""
            '    End If
            'End If

            'If Not txtPin.Text.Trim = "" Then
            '    VPin = GV.parseString(txtPin.Text.Trim)
            'Else
            '    VPin = ""
            'End If

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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Pan_Card_Form where RID=" & lblRID.Text.Trim & ""
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
            If ddlSelectTitle.Items.Count > 0 Then
                If Not ddlSelectTitle.SelectedValue.Trim = "" Then
                    VSelectTitle = GV.parseString(ddlSelectTitle.SelectedValue.Trim)
                Else
                    VSelectTitle = ""
                End If
            End If

            If Not txtApplicantFirstName.Text.Trim = "" Then
                VApplicantFirstName = GV.parseString(txtApplicantFirstName.Text.Trim)
            Else
                VApplicantFirstName = ""
            End If

            If Not txtApplicantMiddleName.Text.Trim = "" Then
                VApplicantMiddleName = GV.parseString(txtApplicantMiddleName.Text.Trim)
            Else
                VApplicantMiddleName = ""
            End If

            If Not txtApplicantLastName.Text.Trim = "" Then
                VApplicantLastName = GV.parseString(txtApplicantLastName.Text.Trim)
            Else
                VApplicantLastName = ""
            End If

            If Not txtApplicantFatherFirstName.Text.Trim = "" Then
                VApplicantFatherFirstName = GV.parseString(txtApplicantFatherFirstName.Text.Trim)
            Else
                VApplicantFatherFirstName = ""
            End If

            If Not txtApplicantFatherMiddleName.Text.Trim = "" Then
                VApplicantFatherMiddleName = GV.parseString(txtApplicantFatherMiddleName.Text.Trim)
            Else
                VApplicantFatherMiddleName = ""
            End If

            If Not txtApplicantFatherLastName.Text.Trim = "" Then
                VApplicantFatherLastName = GV.parseString(txtApplicantFatherLastName.Text.Trim)
            Else
                VApplicantFatherLastName = ""
            End If

            If Not txtDOB.Text.Trim = "" Then
                VDOB = GV.parseString(txtDOB.Text.Trim)
            Else
                VDOB = ""
            End If

            If ddlGender.Items.Count > 0 Then
                If Not ddlGender.SelectedValue.Trim = "" Then
                    VGender = GV.parseString(ddlGender.SelectedValue.Trim)
                Else
                    VGender = ""
                End If
            End If

            If Not txtCardName.Text.Trim = "" Then
                VCardName = GV.parseString(txtCardName.Text.Trim)
            Else
                VCardName = ""
            End If

            If Not txtAadhaarNumber.Text.Trim = "" Then
                VAadhaarNumber = GV.parseString(txtAadhaarNumber.Text.Trim)
            Else
                VAadhaarNumber = ""
            End If

            If Not txtNameonAadhaar.Text.Trim = "" Then
                VNameonAadhaar = GV.parseString(txtNameonAadhaar.Text.Trim)
            Else
                VNameonAadhaar = ""
            End If

            If Not txtMobileNumber.Text.Trim = "" Then
                VMobileNumber = GV.parseString(txtMobileNumber.Text.Trim)
            Else
                VMobileNumber = ""
            End If

            If Not txtEmail.Text.Trim = "" Then
                VEmail = GV.parseString(txtEmail.Text.Trim)
            Else
                VEmail = ""
            End If

            If ddlPanDeliveryState.Items.Count > 0 Then
                If Not ddlPanDeliveryState.SelectedValue.Trim = "" Then
                    VPanDeliveryState = GV.parseString(ddlPanDeliveryState.SelectedValue.Trim)
                Else
                    VPanDeliveryState = ""
                End If
            End If

            If ddlProofOfIdentity.Items.Count > 0 Then
                If Not ddlProofOfIdentity.SelectedValue.Trim = "" Then
                    VProofOfIdentity = GV.parseString(ddlProofOfIdentity.SelectedValue.Trim)
                Else
                    VProofOfIdentity = ""
                End If
            End If

            If ddlProofOfAddress.Items.Count > 0 Then
                If Not ddlProofOfAddress.SelectedValue.Trim = "" Then
                    VProofOfAddress = GV.parseString(ddlProofOfAddress.SelectedValue.Trim)
                Else
                    VProofOfAddress = ""
                End If
            End If

            If ddlProofOfDOB.Items.Count > 0 Then
                If Not ddlProofOfDOB.SelectedValue.Trim = "" Then
                    VProofOfDOB = GV.parseString(ddlProofOfDOB.SelectedValue.Trim)
                Else
                    VProofOfDOB = ""
                End If
            End If

            If Not txtHouseNo_Building_Landmark.Text.Trim = "" Then
                VHouseNo_Building_Landmark = GV.parseString(txtHouseNo_Building_Landmark.Text.Trim)
            Else
                VHouseNo_Building_Landmark = ""
            End If

            If ddlStatusofapplicant.Items.Count > 0 Then
                If Not ddlStatusofapplicant.SelectedValue.Trim = "" Then
                    VStatusofapplicant = GV.parseString(ddlStatusofapplicant.SelectedValue.Trim)
                Else
                    VStatusofapplicant = ""
                End If
            End If

            If Not txtRegistrationNumberforcompany_firms_LLPs.Text.Trim = "" Then
                VRegistrationNumberforcompany_firms_LLPs = GV.parseString(txtRegistrationNumberforcompany_firms_LLPs.Text.Trim)
            Else
                VRegistrationNumberforcompany_firms_LLPs = ""
            End If

            If ddlSourceofIncome.Items.Count > 0 Then
                If Not ddlSourceofIncome.SelectedValue.Trim = "" Then
                    VSourceofIncome = GV.parseString(ddlSourceofIncome.SelectedValue.Trim)
                Else
                    VSourceofIncome = ""
                End If
            End If

            If Not txtVillage.Text.Trim = "" Then
                VVillage = GV.parseString(txtVillage.Text.Trim)
            Else
                VVillage = ""
            End If

            If Not txtPost1.Text.Trim = "" Then
                VPost1 = GV.parseString(txtPost1.Text.Trim)
            Else
                VPost1 = ""
            End If

            If Not txtPost2.Text.Trim = "" Then
                VPost2 = GV.parseString(txtPost2.Text.Trim)
            Else
                VPost2 = ""
            End If

            If ddlDistrict.Items.Count > 0 Then
                If Not ddlDistrict.SelectedValue.Trim = "" Then
                    VDistrict = GV.parseString(ddlDistrict.SelectedValue.Trim)
                Else
                    VDistrict = ""
                End If
            End If

            If ddlDistrictArea.Items.Count > 0 Then
                If Not ddlDistrictArea.SelectedValue.Trim = "" Then
                    VDistrictArea = GV.parseString(ddlDistrictArea.SelectedValue.Trim)
                Else
                    VDistrictArea = ""
                End If
            End If

            If Not txtAREACODE.Text.Trim = "" Then
                VAREACODE = GV.parseString(txtAREACODE.Text.Trim)
            Else
                VAREACODE = ""
            End If

            If Not txtAOType.Text.Trim = "" Then
                VAOType = GV.parseString(txtAOType.Text.Trim)
            Else
                VAOType = ""
            End If

            If Not txtRANGECODE.Text.Trim = "" Then
                VRANGECODE = GV.parseString(txtRANGECODE.Text.Trim)
            Else
                VRANGECODE = ""
            End If

            If Not txtAONO.Text.Trim = "" Then
                VAONO = GV.parseString(txtAONO.Text.Trim)
            Else
                VAONO = ""
            End If

            If ddlState.Items.Count > 0 Then
                If Not ddlState.SelectedValue.Trim = "" Then
                    VState = GV.parseString(ddlState.SelectedValue.Trim)
                Else
                    VState = ""
                End If
            End If

            If Not txtPin.Text.Trim = "" Then
                VPin = GV.parseString(txtPin.Text.Trim)
            Else
                VPin = ""
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Pan_Card_Form Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Pan_Card_Form (RecordDateTime, EntryBy,SelectTitle,ApplicantFirstName,ApplicantMiddleName,ApplicantLastName,ApplicantFatherFirstName,ApplicantFatherMiddleName,ApplicantFatherLastName,DOB,Gender,CardName,AadhaarNumber,NameonAadhaar,MobileNumber,Email,PanDeliveryState,ProofOfIdentity,ProofOfAddress,ProofOfDOB,HouseNo_Building_Landmark,Statusofapplicant,RegistrationNumberforcompany_firms_LLPs,SourceofIncome,Village,Post1,Post2,District,DistrictArea,AREACODE,AOType,RANGECODE,AONO,State,Pin) values(  '" & VRecordDateTime & "','" & VEntryBy & "','" & VSelectTitle & "','" & VApplicantFirstName & "','" & VApplicantMiddleName & "','" & VApplicantLastName & "','" & VApplicantFatherFirstName & "','" & VApplicantFatherMiddleName & "','" & VApplicantFatherLastName & "','" & VDOB & "','" & VGender & "','" & VCardName & "','" & VAadhaarNumber & "','" & VNameonAadhaar & "','" & VMobileNumber & "','" & VEmail & "','" & VPanDeliveryState & "','" & VProofOfIdentity & "','" & VProofOfAddress & "','" & VProofOfDOB & "','" & VHouseNo_Building_Landmark & "','" & VStatusofapplicant & "','" & VRegistrationNumberforcompany_firms_LLPs & "','" & VSourceofIncome & "','" & VVillage & "','" & VPost1 & "','" & VPost2 & "','" & VDistrict & "','" & VDistrictArea & "','" & VAREACODE & "','" & VAOType & "','" & VRANGECODE & "','" & VAONO & "','" & VState & "','" & VPin & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Pan_Card_Form set UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', SelectTitle='" & VSelectTitle & "', ApplicantFirstName='" & VApplicantFirstName & "', ApplicantMiddleName='" & VApplicantMiddleName & "', ApplicantLastName='" & VApplicantLastName & "', ApplicantFatherFirstName='" & VApplicantFatherFirstName & "', ApplicantFatherMiddleName='" & VApplicantFatherMiddleName & "', ApplicantFatherLastName='" & VApplicantFatherLastName & "', DOB='" & VDOB & "', Gender='" & VGender & "', CardName='" & VCardName & "', AadhaarNumber='" & VAadhaarNumber & "', NameonAadhaar='" & VNameonAadhaar & "', MobileNumber='" & VMobileNumber & "', Email='" & VEmail & "', PanDeliveryState='" & VPanDeliveryState & "', ProofOfIdentity='" & VProofOfIdentity & "', ProofOfAddress='" & VProofOfAddress & "', ProofOfDOB='" & VProofOfDOB & "', HouseNo_Building_Landmark='" & VHouseNo_Building_Landmark & "', Statusofapplicant='" & VStatusofapplicant & "', RegistrationNumberforcompany_firms_LLPs='" & VRegistrationNumberforcompany_firms_LLPs & "', SourceofIncome='" & VSourceofIncome & "', Village='" & VVillage & "', Post1='" & VPost1 & "', Post2='" & VPost2 & "', District='" & VDistrict & "', DistrictArea='" & VDistrictArea & "', AREACODE='" & VAREACODE & "', AOType='" & VAOType & "', RANGECODE='" & VRANGECODE & "', AONO='" & VAONO & "', State='" & VState & "', Pin='" & VPin & "' where RID=" & lblRID.Text.Trim & ""
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
            VSelectTitle = ""
            VApplicantFirstName = ""
            VApplicantMiddleName = ""
            VApplicantLastName = ""
            VApplicantFatherFirstName = ""
            VApplicantFatherMiddleName = ""
            VApplicantFatherLastName = ""
            VDOB = ""
            VGender = ""
            VCardName = ""
            VAadhaarNumber = ""
            VNameonAadhaar = ""
            VMobileNumber = ""
            VEmail = ""
            VPanDeliveryState = ""
            VProofOfIdentity = ""
            VProofOfAddress = ""
            VProofOfDOB = ""
            VHouseNo_Building_Landmark = ""
            VStatusofapplicant = ""
            VRegistrationNumberforcompany_firms_LLPs = ""
            VSourceofIncome = ""
            VVillage = ""
            VPost1 = ""
            VPost2 = ""
            VDistrict = ""
            VDistrictArea = ""
            VAREACODE = ""
            VAOType = ""
            VRANGECODE = ""
            VAONO = ""
            VState = ""
            VPin = ""
            If ddlSelectTitle.Items.Count > 0 Then
                ddlSelectTitle.SelectedIndex = 0
            End If

            txtApplicantFirstName.Text = ""

            txtApplicantMiddleName.Text = ""

            txtApplicantLastName.Text = ""

            txtApplicantFatherFirstName.Text = ""

            txtApplicantFatherMiddleName.Text = ""

            txtApplicantFatherLastName.Text = ""

            txtDOB.Text = ""

            If ddlGender.Items.Count > 0 Then
                ddlGender.SelectedIndex = 0
            End If

            txtCardName.Text = ""

            txtAadhaarNumber.Text = ""

            txtNameonAadhaar.Text = ""

            txtMobileNumber.Text = ""

            txtEmail.Text = ""

            If ddlPanDeliveryState.Items.Count > 0 Then
                ddlPanDeliveryState.SelectedIndex = 0
            End If

            If ddlProofOfIdentity.Items.Count > 0 Then
                ddlProofOfIdentity.SelectedIndex = 0
            End If

            If ddlProofOfAddress.Items.Count > 0 Then
                ddlProofOfAddress.SelectedIndex = 0
            End If

            If ddlProofOfDOB.Items.Count > 0 Then
                ddlProofOfDOB.SelectedIndex = 0
            End If

            txtHouseNo_Building_Landmark.Text = ""

            If ddlStatusofapplicant.Items.Count > 0 Then
                ddlStatusofapplicant.SelectedIndex = 0
            End If

            txtRegistrationNumberforcompany_firms_LLPs.Text = ""

            If ddlSourceofIncome.Items.Count > 0 Then
                ddlSourceofIncome.SelectedIndex = 0
            End If

            txtVillage.Text = ""

            txtPost1.Text = ""

            txtPost2.Text = ""

            If ddlDistrict.Items.Count > 0 Then
                ddlDistrict.SelectedIndex = 0
            End If

            If ddlDistrictArea.Items.Count > 0 Then
                ddlDistrictArea.SelectedIndex = 0
            End If

            txtAREACODE.Text = ""

            txtAOType.Text = ""

            txtRANGECODE.Text = ""

            txtAONO.Text = ""

            If ddlState.Items.Count > 0 Then
                ddlState.SelectedIndex = 0
            End If

            txtPin.Text = ""
            ddlState.SelectedValue = "DELHI"
            ddlDistrictArea.SelectedValue = "NORTH DELHI"
            ddlPanDeliveryState.SelectedValue = "DELHI"
            ddlDistrict.SelectedValue = "NORTH DELHI"

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

                GV.FL.AddInDropDownListDistinct(ddlState, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlPanDeliveryState, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrict, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                GV.FL.AddInDropDownListDistinct(ddlDistrictArea, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlState.SelectedValue = "DELHI"
                ddlDistrictArea.SelectedValue = "NORTH DELHI"
                ddlPanDeliveryState.SelectedValue = "DELHI"
                ddlDistrict.SelectedValue = "NORTH DELHI"

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
                    formheading_1.Text = "Edit PAN CARD FORM "
                    DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Pan_Card_Form where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If ddlSelectTitle.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("SelectTitle")) Then
                                    If Not DS.Tables(0).Rows(0).Item("SelectTitle").ToString() = "" Then
                                        ddlSelectTitle.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("SelectTitle").ToString())
                                    Else
                                        ddlSelectTitle.SelectedIndex = 0
                                    End If
                                Else
                                    ddlSelectTitle.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantFirstName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantFirstName").ToString() = "" Then
                                    txtApplicantFirstName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantFirstName").ToString())
                                Else
                                    txtApplicantFirstName.Text = ""
                                End If
                            Else
                                txtApplicantFirstName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantMiddleName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantMiddleName").ToString() = "" Then
                                    txtApplicantMiddleName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantMiddleName").ToString())
                                Else
                                    txtApplicantMiddleName.Text = ""
                                End If
                            Else
                                txtApplicantMiddleName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantLastName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantLastName").ToString() = "" Then
                                    txtApplicantLastName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantLastName").ToString())
                                Else
                                    txtApplicantLastName.Text = ""
                                End If
                            Else
                                txtApplicantLastName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantFatherFirstName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantFatherFirstName").ToString() = "" Then
                                    txtApplicantFatherFirstName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantFatherFirstName").ToString())
                                Else
                                    txtApplicantFatherFirstName.Text = ""
                                End If
                            Else
                                txtApplicantFatherFirstName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantFatherMiddleName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantFatherMiddleName").ToString() = "" Then
                                    txtApplicantFatherMiddleName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantFatherMiddleName").ToString())
                                Else
                                    txtApplicantFatherMiddleName.Text = ""
                                End If
                            Else
                                txtApplicantFatherMiddleName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantFatherLastName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantFatherLastName").ToString() = "" Then
                                    txtApplicantFatherLastName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantFatherLastName").ToString())
                                Else
                                    txtApplicantFatherLastName.Text = ""
                                End If
                            Else
                                txtApplicantFatherLastName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("DOB")) Then
                                If Not DS.Tables(0).Rows(0).Item("DOB").ToString() = "" Then
                                    txtDOB.Text = GV.parseString(DS.Tables(0).Rows(0).Item("DOB").ToString())
                                Else
                                    txtDOB.Text = ""
                                End If
                            Else
                                txtDOB.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("CardName")) Then
                                If Not DS.Tables(0).Rows(0).Item("CardName").ToString() = "" Then
                                    txtCardName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("CardName").ToString())
                                Else
                                    txtCardName.Text = ""
                                End If
                            Else
                                txtCardName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AadhaarNumber")) Then
                                If Not DS.Tables(0).Rows(0).Item("AadhaarNumber").ToString() = "" Then
                                    txtAadhaarNumber.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AadhaarNumber").ToString())
                                Else
                                    txtAadhaarNumber.Text = ""
                                End If
                            Else
                                txtAadhaarNumber.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NameonAadhaar")) Then
                                If Not DS.Tables(0).Rows(0).Item("NameonAadhaar").ToString() = "" Then
                                    txtNameonAadhaar.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NameonAadhaar").ToString())
                                Else
                                    txtNameonAadhaar.Text = ""
                                End If
                            Else
                                txtNameonAadhaar.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("MobileNumber")) Then
                                If Not DS.Tables(0).Rows(0).Item("MobileNumber").ToString() = "" Then
                                    txtMobileNumber.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MobileNumber").ToString())
                                Else
                                    txtMobileNumber.Text = ""
                                End If
                            Else
                                txtMobileNumber.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Email")) Then
                                If Not DS.Tables(0).Rows(0).Item("Email").ToString() = "" Then
                                    txtEmail.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Email").ToString())
                                Else
                                    txtEmail.Text = ""
                                End If
                            Else
                                txtEmail.Text = ""
                            End If

                            If ddlPanDeliveryState.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("PanDeliveryState")) Then
                                    If Not DS.Tables(0).Rows(0).Item("PanDeliveryState").ToString() = "" Then
                                        ddlPanDeliveryState.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("PanDeliveryState").ToString())
                                    Else
                                        ddlPanDeliveryState.SelectedIndex = 0
                                    End If
                                Else
                                    ddlPanDeliveryState.SelectedIndex = 0
                                End If
                            End If

                            If ddlProofOfIdentity.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("ProofOfIdentity")) Then
                                    If Not DS.Tables(0).Rows(0).Item("ProofOfIdentity").ToString() = "" Then
                                        ddlProofOfIdentity.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ProofOfIdentity").ToString())
                                    Else
                                        ddlProofOfIdentity.SelectedIndex = 0
                                    End If
                                Else
                                    ddlProofOfIdentity.SelectedIndex = 0
                                End If
                            End If

                            If ddlProofOfAddress.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("ProofOfAddress")) Then
                                    If Not DS.Tables(0).Rows(0).Item("ProofOfAddress").ToString() = "" Then
                                        ddlProofOfAddress.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ProofOfAddress").ToString())
                                    Else
                                        ddlProofOfAddress.SelectedIndex = 0
                                    End If
                                Else
                                    ddlProofOfAddress.SelectedIndex = 0
                                End If
                            End If

                            If ddlProofOfDOB.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("ProofOfDOB")) Then
                                    If Not DS.Tables(0).Rows(0).Item("ProofOfDOB").ToString() = "" Then
                                        ddlProofOfDOB.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ProofOfDOB").ToString())
                                    Else
                                        ddlProofOfDOB.SelectedIndex = 0
                                    End If
                                Else
                                    ddlProofOfDOB.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("HouseNo_Building_Landmark")) Then
                                If Not DS.Tables(0).Rows(0).Item("HouseNo_Building_Landmark").ToString() = "" Then
                                    txtHouseNo_Building_Landmark.Text = GV.parseString(DS.Tables(0).Rows(0).Item("HouseNo_Building_Landmark").ToString())
                                Else
                                    txtHouseNo_Building_Landmark.Text = ""
                                End If
                            Else
                                txtHouseNo_Building_Landmark.Text = ""
                            End If

                            If ddlStatusofapplicant.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Statusofapplicant")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Statusofapplicant").ToString() = "" Then
                                        ddlStatusofapplicant.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Statusofapplicant").ToString())
                                    Else
                                        ddlStatusofapplicant.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStatusofapplicant.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("RegistrationNumberforcompany_firms_LLPs")) Then
                                If Not DS.Tables(0).Rows(0).Item("RegistrationNumberforcompany_firms_LLPs").ToString() = "" Then
                                    txtRegistrationNumberforcompany_firms_LLPs.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RegistrationNumberforcompany_firms_LLPs").ToString())
                                Else
                                    txtRegistrationNumberforcompany_firms_LLPs.Text = ""
                                End If
                            Else
                                txtRegistrationNumberforcompany_firms_LLPs.Text = ""
                            End If

                            If ddlSourceofIncome.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("SourceofIncome")) Then
                                    If Not DS.Tables(0).Rows(0).Item("SourceofIncome").ToString() = "" Then
                                        ddlSourceofIncome.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("SourceofIncome").ToString())
                                    Else
                                        ddlSourceofIncome.SelectedIndex = 0
                                    End If
                                Else
                                    ddlSourceofIncome.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Village")) Then
                                If Not DS.Tables(0).Rows(0).Item("Village").ToString() = "" Then
                                    txtVillage.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Village").ToString())
                                Else
                                    txtVillage.Text = ""
                                End If
                            Else
                                txtVillage.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Post1")) Then
                                If Not DS.Tables(0).Rows(0).Item("Post1").ToString() = "" Then
                                    txtPost1.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Post1").ToString())
                                Else
                                    txtPost1.Text = ""
                                End If
                            Else
                                txtPost1.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Post2")) Then
                                If Not DS.Tables(0).Rows(0).Item("Post2").ToString() = "" Then
                                    txtPost2.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Post2").ToString())
                                Else
                                    txtPost2.Text = ""
                                End If
                            Else
                                txtPost2.Text = ""
                            End If

                            If ddlDistrict.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("District")) Then
                                    If Not DS.Tables(0).Rows(0).Item("District").ToString() = "" Then
                                        ddlDistrict.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("District").ToString())
                                    Else
                                        ddlDistrict.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrict.SelectedIndex = 0
                                End If
                            End If

                            If ddlDistrictArea.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("DistrictArea")) Then
                                    If Not DS.Tables(0).Rows(0).Item("DistrictArea").ToString() = "" Then
                                        ddlDistrictArea.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("DistrictArea").ToString())
                                    Else
                                        ddlDistrictArea.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrictArea.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AREACODE")) Then
                                If Not DS.Tables(0).Rows(0).Item("AREACODE").ToString() = "" Then
                                    txtAREACODE.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AREACODE").ToString())
                                Else
                                    txtAREACODE.Text = ""
                                End If
                            Else
                                txtAREACODE.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AOType")) Then
                                If Not DS.Tables(0).Rows(0).Item("AOType").ToString() = "" Then
                                    txtAOType.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AOType").ToString())
                                Else
                                    txtAOType.Text = ""
                                End If
                            Else
                                txtAOType.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("RANGECODE")) Then
                                If Not DS.Tables(0).Rows(0).Item("RANGECODE").ToString() = "" Then
                                    txtRANGECODE.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RANGECODE").ToString())
                                Else
                                    txtRANGECODE.Text = ""
                                End If
                            Else
                                txtRANGECODE.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AONO")) Then
                                If Not DS.Tables(0).Rows(0).Item("AONO").ToString() = "" Then
                                    txtAONO.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AONO").ToString())
                                Else
                                    txtAONO.Text = ""
                                End If
                            Else
                                txtAONO.Text = ""
                            End If

                            If ddlState.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("State")) Then
                                    If Not DS.Tables(0).Rows(0).Item("State").ToString() = "" Then
                                        ddlState.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("State").ToString())
                                    Else
                                        ddlState.SelectedIndex = 0
                                    End If
                                Else
                                    ddlState.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Pin")) Then
                                If Not DS.Tables(0).Rows(0).Item("Pin").ToString() = "" Then
                                    txtPin.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Pin").ToString())
                                Else
                                    txtPin.Text = ""
                                End If
                            Else
                                txtPin.Text = ""
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