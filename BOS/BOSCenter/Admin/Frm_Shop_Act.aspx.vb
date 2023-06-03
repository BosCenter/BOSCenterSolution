Public Class Frm_Shop_Act
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VFullNameofEmployer, VAddressofEmployer, VLocalityofEmployer, VStateofEmployer, VDistrictofEmployer, VTalukaAndVillageofEmployer, VPINCodeofEmployer, VEmployerMobileNumber, VEmployeremailId, VResidenceSince, VEmployerAdhaarNo, VEmployerStatus_Designation, VCategoryofEstablishment, VCategoryDetail, VEstablishmentType, VNoOfMenWorker, VNoOfWomenWorker, VNoOfOtherWorker, VApprenticesMenWorker, VApprenticesWomenWorker, VApprenticesOtherWorker, VPartTimeMenWorker, VPartTimeWomenWorker, VPartTimeOtherWorker, VNameOfEstablishment, VPreviousdetailofEstablishment, VIfRenewalEnterShopactNo, VAddressofEstablishment, VLocalityofEstablishment, VStateofEstablishment, VDistrictofEstablishment, VTalukaAndVillageofEstablishment, VPINCodeofEstablishment, VMobileNumber, VOwnershipofPremises, VDateofEstablishment, VNatureofBusiness As String
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
                txtNoOfMenWorker.CssClass = "form-control"
                txtNoOfWomenWorker.CssClass = "form-control"
                txtNoOfOtherWorker.CssClass = "form-control"
                txtApprenticesMenWorker.CssClass = "form-control"
                txtApprenticesOtherWorker.CssClass = "form-control"
                txtApprenticesWomenWorker.CssClass = "form-control"
                txtPartTimeMenWorker.CssClass = "form-control"
                txtPartTimeOtherWorker.CssClass = "form-control"
                txtPartTimeWomenWorker.CssClass = "form-control"
                txtNameOfEstablishment.CssClass = "form-control"
                ddlPreviousdetailofEstablishment.CssClass = "form-control"
                txtIfRenewalEnterShopactNo.CssClass = "form-control"
                txtAddressofEstablishment.CssClass = "form-control"
                txtLocalityofEstablishment.CssClass = "form-control"
                ddlStateofEstablishment.CssClass = "form-control"
                ddlDistrictofEstablishment.CssClass = "form-control"
                txtTalukaAndVillageofEstablishment.CssClass = "form-control"
                txtPINCodeofEstablishment.CssClass = "form-control"
                txtMobileNumber.CssClass = "form-control"
                ddlOwnershipofPremises.CssClass = "form-control"
                txtDateofEstablishment.CssClass = "form-control"
                txtNatureofBusiness.CssClass = "form-control"
                txtFullNameofEmployer.CssClass = "form-control"
                txtAddressofEmployer.CssClass = "form-control"
                txtLocalityofEmployer.CssClass = "form-control"
                ddlStateofEmployer.CssClass = "form-control"
                ddlDistrictofEmployer.CssClass = "form-control"
                txtTalukaAndVillageofEmployer.CssClass = "form-control"
                txtPINCodeofEmployer.CssClass = "form-control"
                txtEmployerMobileNumber.CssClass = "form-control"
                txtEmployeremailId.CssClass = "form-control"
                txtResidenceSince.CssClass = "form-control"
                txtEmployerAdhaarNo.CssClass = "form-control"
                txtEmployerStatus_Designation.CssClass = "form-control"
                txtCategoryofEstablishment.CssClass = "form-control"
                txtCategoryDetail.CssClass = "form-control"
                txtEstablishmentType.CssClass = "form-control"


                VFullNameofEmployer = ""
                VAddressofEmployer = ""
                VLocalityofEmployer = ""
                VStateofEmployer = ""
                VDistrictofEmployer = ""
                VTalukaAndVillageofEmployer = ""
                VPINCodeofEmployer = ""
                VEmployerMobileNumber = ""
                VEmployeremailId = ""
                VResidenceSince = ""
                VEmployerAdhaarNo = ""
                VEmployerStatus_Designation = ""
                VCategoryofEstablishment = ""
                VCategoryDetail = ""
                VEstablishmentType = ""
                VNoOfMenWorker = ""
                VNoOfWomenWorker = ""
                VNoOfOtherWorker = ""
                VApprenticesMenWorker = ""
                VApprenticesWomenWorker = ""
                VApprenticesOtherWorker = ""
                VPartTimeMenWorker = ""
                VPartTimeWomenWorker = ""
                VPartTimeOtherWorker = ""
                VNameOfEstablishment = ""
                VPreviousdetailofEstablishment = ""
                VIfRenewalEnterShopactNo = ""
                VAddressofEstablishment = ""
                VLocalityofEstablishment = ""
                VStateofEstablishment = ""
                VDistrictofEstablishment = ""
                VTalukaAndVillageofEstablishment = ""
                VPINCodeofEstablishment = ""
                VMobileNumber = ""
                VOwnershipofPremises = ""
                VDateofEstablishment = ""
                VNatureofBusiness = ""
                lblError.Text = ""
                lblError.CssClass = ""
                txtFullNameofEmployer.Text = ""

                txtAddressofEmployer.Text = ""

                txtLocalityofEmployer.Text = ""

                If ddlStateofEmployer.Items.Count > 0 Then
                    ddlStateofEmployer.SelectedIndex = 0
                End If

                If ddlDistrictofEmployer.Items.Count > 0 Then
                    ddlDistrictofEmployer.SelectedIndex = 0
                End If

                txtTalukaAndVillageofEmployer.Text = ""

                txtPINCodeofEmployer.Text = ""

                txtEmployerMobileNumber.Text = ""

                txtEmployeremailId.Text = ""

                txtResidenceSince.Text = ""

                txtEmployerAdhaarNo.Text = ""

                txtEmployerStatus_Designation.Text = ""

                txtCategoryofEstablishment.Text = ""

                txtCategoryDetail.Text = ""

                txtEstablishmentType.Text = ""

                txtNoOfMenWorker.Text = ""

                txtNoOfWomenWorker.Text = ""

                txtNoOfOtherWorker.Text = ""

                txtApprenticesMenWorker.Text = ""

                txtApprenticesWomenWorker.Text = ""

                txtApprenticesOtherWorker.Text = ""

                txtPartTimeMenWorker.Text = ""

                txtPartTimeWomenWorker.Text = ""

                txtPartTimeOtherWorker.Text = ""

                txtNameOfEstablishment.Text = ""

                If ddlPreviousdetailofEstablishment.Items.Count > 0 Then
                    ddlPreviousdetailofEstablishment.SelectedIndex = 0
                End If

                txtIfRenewalEnterShopactNo.Text = ""

                txtAddressofEstablishment.Text = ""

                txtLocalityofEstablishment.Text = ""

                If ddlStateofEstablishment.Items.Count > 0 Then
                    ddlStateofEstablishment.SelectedIndex = 0
                End If

                If ddlDistrictofEstablishment.Items.Count > 0 Then
                    ddlDistrictofEstablishment.SelectedIndex = 0
                End If

                txtTalukaAndVillageofEstablishment.Text = ""

                txtPINCodeofEstablishment.Text = ""

                txtMobileNumber.Text = ""

                If ddlOwnershipofPremises.Items.Count > 0 Then
                    ddlOwnershipofPremises.SelectedIndex = 0
                End If

                txtDateofEstablishment.Text = ""

                txtNatureofBusiness.Text = ""
                ddlStateofEstablishment.SelectedValue = "DELHI"
                ddlDistrictofEstablishment.SelectedValue = "NORTH DELHI"

                ddlStateofEmployer.SelectedValue = "DELHI"
                ddlDistrictofEmployer.SelectedValue = "NORTH DELHI"


                Session("EditFlag") = 0
                btnSave.Text = "Save"
                btnClear.Text = "Reset"
                lblError.Text = ""
                btnSave.Enabled = True
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            '/////////// Start Bulk Validation

            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False

            If GV.parseString(txtNameOfEstablishment.Text) = "" Then
                txtNameOfEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNameOfEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNameOfEstablishment.CssClass = "form-control"
                VNameOfEstablishment = GV.parseString(txtNameOfEstablishment.Text)
            End If

            If GV.parseString(ddlPreviousdetailofEstablishment.SelectedValue) = "" Then
                ddlPreviousdetailofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlPreviousdetailofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlPreviousdetailofEstablishment.CssClass = "form-control"
                VPreviousdetailofEstablishment = GV.parseString(ddlPreviousdetailofEstablishment.SelectedValue)
            End If

            If GV.parseString(txtIfRenewalEnterShopactNo.Text) = "" Then
                txtIfRenewalEnterShopactNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtIfRenewalEnterShopactNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtIfRenewalEnterShopactNo.CssClass = "form-control"
                VIfRenewalEnterShopactNo = GV.parseString(txtIfRenewalEnterShopactNo.Text)
            End If

            If GV.parseString(txtAddressofEstablishment.Text) = "" Then
                txtAddressofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAddressofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAddressofEstablishment.CssClass = "form-control"
                VAddressofEstablishment = GV.parseString(txtAddressofEstablishment.Text)
            End If

            If GV.parseString(txtLocalityofEstablishment.Text) = "" Then
                txtLocalityofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtLocalityofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                txtLocalityofEstablishment.CssClass = "form-control"
                VLocalityofEstablishment = GV.parseString(txtLocalityofEstablishment.Text)
            End If

            If GV.parseString(ddlStateofEstablishment.SelectedValue) = "" Then
                ddlStateofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStateofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStateofEstablishment.CssClass = "form-control"
                VStateofEstablishment = GV.parseString(ddlStateofEstablishment.SelectedValue)
            End If

            If GV.parseString(ddlDistrictofEstablishment.SelectedValue) = "" Then
                ddlDistrictofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrictofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrictofEstablishment.CssClass = "form-control"
                VDistrictofEstablishment = GV.parseString(ddlDistrictofEstablishment.SelectedValue)
            End If

            If GV.parseString(txtTalukaAndVillageofEstablishment.Text) = "" Then
                txtTalukaAndVillageofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtTalukaAndVillageofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                txtTalukaAndVillageofEstablishment.CssClass = "form-control"
                VTalukaAndVillageofEstablishment = GV.parseString(txtTalukaAndVillageofEstablishment.Text)
            End If

            If GV.parseString(txtPINCodeofEstablishment.Text) = "" Then
                txtPINCodeofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPINCodeofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPINCodeofEstablishment.CssClass = "form-control"
                VPINCodeofEstablishment = GV.parseString(txtPINCodeofEstablishment.Text)
            End If

            If GV.parseString(txtMobileNumber.Text) = "" Then
                txtMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtMobileNumber.Text) Then
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
            ElseIf Not GV.parseString(txtMobileNumber.Text).Length = 10 Then
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

            If GV.parseString(ddlOwnershipofPremises.SelectedValue) = "" Then
                ddlOwnershipofPremises.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlOwnershipofPremises.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlOwnershipofPremises.CssClass = "form-control"
                VOwnershipofPremises = GV.parseString(ddlOwnershipofPremises.SelectedValue)
            End If

            If GV.parseString(txtDateofEstablishment.Text) = "" Then
                txtDateofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtDateofEstablishment.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsDate(GV.FL.returnDateMonthWiseWithDateChecking(txtDateofEstablishment.text)) = True Then
                txtDateofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Correct Date Format"
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Correct Date Format"
                End If
                If isFocusApplied = False Then
                    txtNatureofBusiness.Focus()
                    isFocusApplied = True
                End If
            Else
                txtDateofEstablishment.CssClass = "form-control"
                VDistrictofEstablishment = GV.parseString(txtDateofEstablishment.Text)
            End If

            If GV.parseString(txtNatureofBusiness.Text) = "" Then
                txtNatureofBusiness.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNatureofBusiness.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNatureofBusiness.CssClass = "form-control"
                VNatureofBusiness = GV.parseString(txtNatureofBusiness.Text)
            End If

            If GV.parseString(txtNoOfMenWorker.Text) = "" Then
                txtNoOfMenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNoOfMenWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtNoOfMenWorker.Text) Then
                txtNoOfMenWorker.CssClass = "ValidationError"
                    isErrorFound = True
                    If VError_Sring.Trim = "" Then
                        VError_Sring = "Please Enter Number."
                    Else
                        VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                    End If

                If isFocusApplied = False Then
                    txtNoOfMenWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                    txtNoOfMenWorker.CssClass = "form-control"
                VNoOfMenWorker = GV.parseString(txtNoOfMenWorker.Text)
            End If

            If GV.parseString(txtNoOfWomenWorker.Text) = "" Then
                txtNoOfWomenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNoOfWomenWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtNoOfWomenWorker.Text) Then
                txtNoOfWomenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                        VError_Sring = "Please Enter Number."
                    Else
                        VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                    End If

                If isFocusApplied = False Then
                txtNoOfWomenWorker.Focus()
                isFocusApplied = True
                End If
            Else
                txtNoOfWomenWorker.CssClass = "form-control"
                VNoOfWomenWorker = GV.parseString(txtNoOfWomenWorker.Text)
            End If

            If GV.parseString(txtNoOfOtherWorker.Text) = "" Then
                txtNoOfOtherWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNoOfOtherWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtNoOfOtherWorker.Text) Then
                txtNoOfOtherWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Number."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                End If

                If isFocusApplied = False Then
                    txtNoOfOtherWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNoOfOtherWorker.CssClass = "form-control"
                VNoOfOtherWorker = GV.parseString(txtNoOfOtherWorker.Text)
            End If

            If GV.parseString(txtApprenticesMenWorker.Text) = "" Then
                txtApprenticesMenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApprenticesMenWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtApprenticesMenWorker.Text) Then
                txtApprenticesMenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Number."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                End If

                If isFocusApplied = False Then
                    txtApprenticesMenWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApprenticesMenWorker.CssClass = "form-control"
                VApprenticesMenWorker = GV.parseString(txtApprenticesMenWorker.Text)
            End If

            If GV.parseString(txtApprenticesWomenWorker.Text) = "" Then
                txtApprenticesWomenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApprenticesWomenWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtApprenticesWomenWorker.Text) Then
                txtApprenticesWomenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Number."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                End If

                If isFocusApplied = False Then
                    txtApprenticesWomenWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApprenticesWomenWorker.CssClass = "form-control"
                VApprenticesWomenWorker = GV.parseString(txtApprenticesWomenWorker.Text)
            End If

            If GV.parseString(txtApprenticesOtherWorker.Text) = "" Then
                txtApprenticesOtherWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApprenticesOtherWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtApprenticesOtherWorker.Text) Then
                txtApprenticesOtherWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Number."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                End If

                If isFocusApplied = False Then
                    txtApprenticesOtherWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApprenticesOtherWorker.CssClass = "form-control"
                VApprenticesOtherWorker = GV.parseString(txtApprenticesOtherWorker.Text)
            End If

            If GV.parseString(txtPartTimeMenWorker.Text) = "" Then
                txtPartTimeMenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPartTimeMenWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtPartTimeMenWorker.Text) Then
                txtPartTimeMenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Number."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                End If

                If isFocusApplied = False Then
                    txtPartTimeMenWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPartTimeMenWorker.CssClass = "form-control"
                VPartTimeMenWorker = GV.parseString(txtPartTimeMenWorker.Text)
            End If

            If GV.parseString(txtPartTimeWomenWorker.Text) = "" Then
                txtPartTimeWomenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPartTimeWomenWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtPartTimeWomenWorker.Text) Then
                txtPartTimeWomenWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Number."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                End If

                If isFocusApplied = False Then
                    txtPartTimeWomenWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPartTimeWomenWorker.CssClass = "form-control"
                VPartTimeWomenWorker = GV.parseString(txtPartTimeWomenWorker.Text)
            End If

            If GV.parseString(txtPartTimeOtherWorker.Text) = "" Then
                txtPartTimeOtherWorker.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPartTimeOtherWorker.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtPartTimeOtherWorker.Text) Then
                txtPartTimeOtherWorker.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Number."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Number."
                End If

                If isFocusApplied = False Then
                    txtPartTimeOtherWorker.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPartTimeOtherWorker.CssClass = "form-control"
                VPartTimeOtherWorker = GV.parseString(txtPartTimeOtherWorker.Text)
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

            If GV.parseString(txtAddressofEmployer.Text) = "" Then
                txtAddressofEmployer.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAddressofEmployer.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAddressofEmployer.CssClass = "form-control"
                VAddressofEmployer = GV.parseString(txtAddressofEmployer.Text)
            End If

            If GV.parseString(txtLocalityofEmployer.Text) = "" Then
                txtLocalityofEmployer.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtLocalityofEmployer.Focus()
                    isFocusApplied = True
                End If
            Else
                txtLocalityofEmployer.CssClass = "form-control"
                VLocalityofEmployer = GV.parseString(txtLocalityofEmployer.Text)
            End If

            If GV.parseString(ddlStateofEmployer.SelectedValue) = "" Then
                ddlStateofEmployer.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStateofEmployer.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStateofEmployer.CssClass = "form-control"
                VStateofEmployer = GV.parseString(ddlStateofEmployer.SelectedValue)
            End If

            If GV.parseString(ddlDistrictofEmployer.SelectedValue) = "" Then
                ddlDistrictofEmployer.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrictofEmployer.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrictofEmployer.CssClass = "form-control"
                VDistrictofEmployer = GV.parseString(ddlDistrictofEmployer.SelectedValue)
            End If

            If GV.parseString(txtTalukaAndVillageofEmployer.Text) = "" Then
                txtTalukaAndVillageofEmployer.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtTalukaAndVillageofEmployer.Focus()
                    isFocusApplied = True
                End If
            Else
                txtTalukaAndVillageofEmployer.CssClass = "form-control"
                VTalukaAndVillageofEmployer = GV.parseString(txtTalukaAndVillageofEmployer.Text)
            End If

            If GV.parseString(txtPINCodeofEmployer.Text) = "" Then
                txtPINCodeofEmployer.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPINCodeofEmployer.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPINCodeofEmployer.CssClass = "form-control"
                VPINCodeofEmployer = GV.parseString(txtPINCodeofEmployer.Text)
            End If

            If GV.parseString(txtEmployerMobileNumber.Text) = "" Then
                txtEmployerMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmployerMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtEmployerMobileNumber.Text) Then
                txtEmployerMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Correct Mobile No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Correct Mobile No."
                End If
                If isFocusApplied = False Then
                    txtEmployerMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtEmployerMobileNumber.Text).Length = 10 Then
                txtEmployerMobileNumber.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter 10 Digit Mobile No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter 10 Digit Mobile No."
                End If
                If isFocusApplied = False Then
                    txtEmployerMobileNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEmployerMobileNumber.CssClass = "form-control"
                VEmployerMobileNumber = GV.parseString(txtEmployerMobileNumber.Text)
            End If

            If GV.parseString(txtEmployeremailId.Text) = "" Then
                txtEmployeremailId.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmployeremailId.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEmployeremailId.CssClass = "form-control"
                VEmployeremailId = GV.parseString(txtEmployeremailId.Text)
            End If

            If GV.parseString(txtResidenceSince.Text) = "" Then
                txtResidenceSince.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtResidenceSince.Focus()
                    isFocusApplied = True
                End If
            Else
                txtResidenceSince.CssClass = "form-control"
                VResidenceSince = GV.parseString(txtResidenceSince.Text)
            End If

            If GV.parseString(txtEmployerAdhaarNo.Text) = "" Then
                txtEmployerAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmployerAdhaarNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtEmployerAdhaarNo.Text) Then
                txtEmployerAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring = "" Then
                    VError_Sring = "Please Enter Correct Aadhar No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Correct Aadhar No."
                End If
            ElseIf Not GV.parseString(txtEmployerAdhaarNo.Text).Length = 12 Then
                txtEmployerAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring = "" Then
                    VError_Sring = "Please Enter 12 Digit Aadhar No."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter 12 Digit Aadhar No."
                End If

            Else
                txtEmployerAdhaarNo.CssClass = "form-control"
                VEmployerAdhaarNo = GV.parseString(txtEmployerAdhaarNo.Text)
            End If

            If GV.parseString(txtEmployerStatus_Designation.Text) = "" Then
                txtEmployerStatus_Designation.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmployerStatus_Designation.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEmployerStatus_Designation.CssClass = "form-control"
                VEmployerStatus_Designation = GV.parseString(txtEmployerStatus_Designation.Text)
            End If

            If GV.parseString(txtCategoryofEstablishment.Text) = "" Then
                txtCategoryofEstablishment.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtCategoryofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else
                txtCategoryofEstablishment.CssClass = "form-control"
                VCategoryofEstablishment = GV.parseString(txtCategoryofEstablishment.Text)
            End If

            If GV.parseString(txtCategoryDetail.Text) = "" Then
                txtCategoryDetail.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtCategoryDetail.Focus()
                    isFocusApplied = True
                End If
            Else
                txtCategoryDetail.CssClass = "form-control"
                VCategoryDetail = GV.parseString(txtCategoryDetail.Text)
            End If

            If GV.parseString(txtEstablishmentType.Text) = "" Then
                txtEstablishmentType.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEstablishmentType.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEstablishmentType.CssClass = "form-control"
                VEstablishmentType = GV.parseString(txtEstablishmentType.Text)
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






            '/////////// End Bulk Validation



            'If Not txtFullNameofEmployer.Text.Trim = "" Then
            '    VFullNameofEmployer = GV.parseString(txtFullNameofEmployer.Text.Trim)
            'Else
            '    VFullNameofEmployer = ""
            'End If

            'If Not txtAddressofEmployer.Text.Trim = "" Then
            '    VAddressofEmployer = GV.parseString(txtAddressofEmployer.Text.Trim)
            'Else
            '    VAddressofEmployer = ""
            'End If

            'If Not txtLocalityofEmployer.Text.Trim = "" Then
            '    VLocalityofEmployer = GV.parseString(txtLocalityofEmployer.Text.Trim)
            'Else
            '    VLocalityofEmployer = ""
            'End If

            'If ddlStateofEmployer.Items.Count > 0 Then
            '    If Not ddlStateofEmployer.SelectedValue.Trim = "" Then
            '        VStateofEmployer = GV.parseString(ddlStateofEmployer.SelectedValue.Trim)
            '    Else
            '        VStateofEmployer = ""
            '    End If
            'End If

            'If ddlDistrictofEmployer.Items.Count > 0 Then
            '    If Not ddlDistrictofEmployer.SelectedValue.Trim = "" Then
            '        VDistrictofEmployer = GV.parseString(ddlDistrictofEmployer.SelectedValue.Trim)
            '    Else
            '        VDistrictofEmployer = ""
            '    End If
            'End If

            'If Not txtTalukaAndVillageofEmployer.Text.Trim = "" Then
            '    VTalukaAndVillageofEmployer = GV.parseString(txtTalukaAndVillageofEmployer.Text.Trim)
            'Else
            '    VTalukaAndVillageofEmployer = ""
            'End If

            'If Not txtPINCodeofEmployer.Text.Trim = "" Then
            '    VPINCodeofEmployer = GV.parseString(txtPINCodeofEmployer.Text.Trim)
            'Else
            '    VPINCodeofEmployer = ""
            'End If

            'If Not txtEmployerMobileNumber.Text.Trim = "" Then
            '    VEmployerMobileNumber = GV.parseString(txtEmployerMobileNumber.Text.Trim)
            'Else
            '    VEmployerMobileNumber = ""
            'End If

            'If Not txtEmployeremailId.Text.Trim = "" Then
            '    VEmployeremailId = GV.parseString(txtEmployeremailId.Text.Trim)
            'Else
            '    VEmployeremailId = ""
            'End If

            'If Not txtResidenceSince.Text.Trim = "" Then
            '    VResidenceSince = GV.parseString(txtResidenceSince.Text.Trim)
            'Else
            '    VResidenceSince = ""
            'End If

            'If Not txtEmployerAdhaarNo.Text.Trim = "" Then
            '    VEmployerAdhaarNo = GV.parseString(txtEmployerAdhaarNo.Text.Trim)
            'Else
            '    VEmployerAdhaarNo = ""
            'End If

            'If Not txtEmployerStatus_Designation.Text.Trim = "" Then
            '    VEmployerStatus_Designation = GV.parseString(txtEmployerStatus_Designation.Text.Trim)
            'Else
            '    VEmployerStatus_Designation = ""
            'End If

            'If Not txtCategoryofEstablishment.Text.Trim = "" Then
            '    VCategoryofEstablishment = GV.parseString(txtCategoryofEstablishment.Text.Trim)
            'Else
            '    VCategoryofEstablishment = ""
            'End If

            'If Not txtCategoryDetail.Text.Trim = "" Then
            '    VCategoryDetail = GV.parseString(txtCategoryDetail.Text.Trim)
            'Else
            '    VCategoryDetail = ""
            'End If

            'If Not txtEstablishmentType.Text.Trim = "" Then
            '    VEstablishmentType = GV.parseString(txtEstablishmentType.Text.Trim)
            'Else
            '    VEstablishmentType = ""
            'End If

            'If Not txtNoOfMenWorker.Text.Trim = "" Then
            '    VNoOfMenWorker = GV.parseString(txtNoOfMenWorker.Text.Trim)
            'Else
            '    VNoOfMenWorker = ""
            'End If

            'If Not txtNoOfWomenWorker.Text.Trim = "" Then
            '    VNoOfWomenWorker = GV.parseString(txtNoOfWomenWorker.Text.Trim)
            'Else
            '    VNoOfWomenWorker = ""
            'End If

            'If Not txtNoOfOtherWorker.Text.Trim = "" Then
            '    VNoOfOtherWorker = GV.parseString(txtNoOfOtherWorker.Text.Trim)
            'Else
            '    VNoOfOtherWorker = ""
            'End If

            'If Not txtApprenticesMenWorker.Text.Trim = "" Then
            '    VApprenticesMenWorker = GV.parseString(txtApprenticesMenWorker.Text.Trim)
            'Else
            '    VApprenticesMenWorker = ""
            'End If

            'If Not txtApprenticesWomenWorker.Text.Trim = "" Then
            '    VApprenticesWomenWorker = GV.parseString(txtApprenticesWomenWorker.Text.Trim)
            'Else
            '    VApprenticesWomenWorker = ""
            'End If

            'If Not txtApprenticesOtherWorker.Text.Trim = "" Then
            '    VApprenticesOtherWorker = GV.parseString(txtApprenticesOtherWorker.Text.Trim)
            'Else
            '    VApprenticesOtherWorker = ""
            'End If

            'If Not txtPartTimeMenWorker.Text.Trim = "" Then
            '    VPartTimeMenWorker = GV.parseString(txtPartTimeMenWorker.Text.Trim)
            'Else
            '    VPartTimeMenWorker = ""
            'End If

            'If Not txtPartTimeWomenWorker.Text.Trim = "" Then
            '    VPartTimeWomenWorker = GV.parseString(txtPartTimeWomenWorker.Text.Trim)
            'Else
            '    VPartTimeWomenWorker = ""
            'End If

            'If Not txtPartTimeOtherWorker.Text.Trim = "" Then
            '    VPartTimeOtherWorker = GV.parseString(txtPartTimeOtherWorker.Text.Trim)
            'Else
            '    VPartTimeOtherWorker = ""
            'End If

            'If Not txtNameOfEstablishment.Text.Trim = "" Then
            '    VNameOfEstablishment = GV.parseString(txtNameOfEstablishment.Text.Trim)
            'Else
            '    VNameOfEstablishment = ""
            'End If

            'If ddlPreviousdetailofEstablishment.Items.Count > 0 Then
            '    If Not ddlPreviousdetailofEstablishment.SelectedValue.Trim = "" Then
            '        VPreviousdetailofEstablishment = GV.parseString(ddlPreviousdetailofEstablishment.SelectedValue.Trim)
            '    Else
            '        VPreviousdetailofEstablishment = ""
            '    End If
            'End If

            'If Not txtIfRenewalEnterShopactNo.Text.Trim = "" Then
            '    VIfRenewalEnterShopactNo = GV.parseString(txtIfRenewalEnterShopactNo.Text.Trim)
            'Else
            '    VIfRenewalEnterShopactNo = ""
            'End If

            'If Not txtAddressofEstablishment.Text.Trim = "" Then
            '    VAddressofEstablishment = GV.parseString(txtAddressofEstablishment.Text.Trim)
            'Else
            '    VAddressofEstablishment = ""
            'End If

            'If Not txtLocalityofEstablishment.Text.Trim = "" Then
            '    VLocalityofEstablishment = GV.parseString(txtLocalityofEstablishment.Text.Trim)
            'Else
            '    VLocalityofEstablishment = ""
            'End If

            'If ddlStateofEstablishment.Items.Count > 0 Then
            '    If Not ddlStateofEstablishment.SelectedValue.Trim = "" Then
            '        VStateofEstablishment = GV.parseString(ddlStateofEstablishment.SelectedValue.Trim)
            '    Else
            '        VStateofEstablishment = ""
            '    End If
            'End If

            'If ddlDistrictofEstablishment.Items.Count > 0 Then
            '    If Not ddlDistrictofEstablishment.SelectedValue.Trim = "" Then
            '        VDistrictofEstablishment = GV.parseString(ddlDistrictofEstablishment.SelectedValue.Trim)
            '    Else
            '        VDistrictofEstablishment = ""
            '    End If
            'End If

            'If Not txtTalukaAndVillageofEstablishment.Text.Trim = "" Then
            '    VTalukaAndVillageofEstablishment = GV.parseString(txtTalukaAndVillageofEstablishment.Text.Trim)
            'Else
            '    VTalukaAndVillageofEstablishment = ""
            'End If

            'If Not txtPINCodeofEstablishment.Text.Trim = "" Then
            '    VPINCodeofEstablishment = GV.parseString(txtPINCodeofEstablishment.Text.Trim)
            'Else
            '    VPINCodeofEstablishment = ""
            'End If

            'If Not txtMobileNumber.Text.Trim = "" Then
            '    VMobileNumber = GV.parseString(txtMobileNumber.Text.Trim)
            'Else
            '    VMobileNumber = ""
            'End If

            'If ddlOwnershipofPremises.Items.Count > 0 Then
            '    If Not ddlOwnershipofPremises.SelectedValue.Trim = "" Then
            '        VOwnershipofPremises = GV.parseString(ddlOwnershipofPremises.SelectedValue.Trim)
            '    Else
            '        VOwnershipofPremises = ""
            '    End If
            'End If

            'If Not txtDateofEstablishment.Text.Trim = "" Then
            '    VDateofEstablishment = GV.parseString(txtDateofEstablishment.Text.Trim)
            'Else
            '    VDateofEstablishment = ""
            'End If

            'If Not txtNatureofBusiness.Text.Trim = "" Then
            '    VNatureofBusiness = GV.parseString(txtNatureofBusiness.Text.Trim)
            'Else
            '    VNatureofBusiness = ""
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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Shop_Act where RID=" & lblRID.Text.Trim & ""
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
            If Not txtFullNameofEmployer.Text.Trim = "" Then
                VFullNameofEmployer = GV.parseString(txtFullNameofEmployer.Text.Trim)
            Else
                VFullNameofEmployer = ""
            End If

            If Not txtAddressofEmployer.Text.Trim = "" Then
                VAddressofEmployer = GV.parseString(txtAddressofEmployer.Text.Trim)
            Else
                VAddressofEmployer = ""
            End If

            If Not txtLocalityofEmployer.Text.Trim = "" Then
                VLocalityofEmployer = GV.parseString(txtLocalityofEmployer.Text.Trim)
            Else
                VLocalityofEmployer = ""
            End If

            If ddlStateofEmployer.Items.Count > 0 Then
                If Not ddlStateofEmployer.SelectedValue.Trim = "" Then
                    VStateofEmployer = GV.parseString(ddlStateofEmployer.SelectedValue.Trim)
                Else
                    VStateofEmployer = ""
                End If
            End If

            If ddlDistrictofEmployer.Items.Count > 0 Then
                If Not ddlDistrictofEmployer.SelectedValue.Trim = "" Then
                    VDistrictofEmployer = GV.parseString(ddlDistrictofEmployer.SelectedValue.Trim)
                Else
                    VDistrictofEmployer = ""
                End If
            End If

            If Not txtTalukaAndVillageofEmployer.Text.Trim = "" Then
                VTalukaAndVillageofEmployer = GV.parseString(txtTalukaAndVillageofEmployer.Text.Trim)
            Else
                VTalukaAndVillageofEmployer = ""
            End If

            If Not txtPINCodeofEmployer.Text.Trim = "" Then
                VPINCodeofEmployer = GV.parseString(txtPINCodeofEmployer.Text.Trim)
            Else
                VPINCodeofEmployer = ""
            End If

            If Not txtEmployerMobileNumber.Text.Trim = "" Then
                VEmployerMobileNumber = GV.parseString(txtEmployerMobileNumber.Text.Trim)
            Else
                VEmployerMobileNumber = ""
            End If

            If Not txtEmployeremailId.Text.Trim = "" Then
                VEmployeremailId = GV.parseString(txtEmployeremailId.Text.Trim)
            Else
                VEmployeremailId = ""
            End If

            If Not txtResidenceSince.Text.Trim = "" Then
                VResidenceSince = GV.parseString(txtResidenceSince.Text.Trim)
            Else
                VResidenceSince = ""
            End If

            If Not txtEmployerAdhaarNo.Text.Trim = "" Then
                VEmployerAdhaarNo = GV.parseString(txtEmployerAdhaarNo.Text.Trim)
            Else
                VEmployerAdhaarNo = ""
            End If

            If Not txtEmployerStatus_Designation.Text.Trim = "" Then
                VEmployerStatus_Designation = GV.parseString(txtEmployerStatus_Designation.Text.Trim)
            Else
                VEmployerStatus_Designation = ""
            End If

            If Not txtCategoryofEstablishment.Text.Trim = "" Then
                VCategoryofEstablishment = GV.parseString(txtCategoryofEstablishment.Text.Trim)
            Else
                VCategoryofEstablishment = ""
            End If

            If Not txtCategoryDetail.Text.Trim = "" Then
                VCategoryDetail = GV.parseString(txtCategoryDetail.Text.Trim)
            Else
                VCategoryDetail = ""
            End If

            If Not txtEstablishmentType.Text.Trim = "" Then
                VEstablishmentType = GV.parseString(txtEstablishmentType.Text.Trim)
            Else
                VEstablishmentType = ""
            End If

            If Not txtNoOfMenWorker.Text.Trim = "" Then
                VNoOfMenWorker = GV.parseString(txtNoOfMenWorker.Text.Trim)
            Else
                VNoOfMenWorker = ""
            End If

            If Not txtNoOfWomenWorker.Text.Trim = "" Then
                VNoOfWomenWorker = GV.parseString(txtNoOfWomenWorker.Text.Trim)
            Else
                VNoOfWomenWorker = ""
            End If

            If Not txtNoOfOtherWorker.Text.Trim = "" Then
                VNoOfOtherWorker = GV.parseString(txtNoOfOtherWorker.Text.Trim)
            Else
                VNoOfOtherWorker = ""
            End If

            If Not txtApprenticesMenWorker.Text.Trim = "" Then
                VApprenticesMenWorker = GV.parseString(txtApprenticesMenWorker.Text.Trim)
            Else
                VApprenticesMenWorker = ""
            End If

            If Not txtApprenticesWomenWorker.Text.Trim = "" Then
                VApprenticesWomenWorker = GV.parseString(txtApprenticesWomenWorker.Text.Trim)
            Else
                VApprenticesWomenWorker = ""
            End If

            If Not txtApprenticesOtherWorker.Text.Trim = "" Then
                VApprenticesOtherWorker = GV.parseString(txtApprenticesOtherWorker.Text.Trim)
            Else
                VApprenticesOtherWorker = ""
            End If

            If Not txtPartTimeMenWorker.Text.Trim = "" Then
                VPartTimeMenWorker = GV.parseString(txtPartTimeMenWorker.Text.Trim)
            Else
                VPartTimeMenWorker = ""
            End If

            If Not txtPartTimeWomenWorker.Text.Trim = "" Then
                VPartTimeWomenWorker = GV.parseString(txtPartTimeWomenWorker.Text.Trim)
            Else
                VPartTimeWomenWorker = ""
            End If

            If Not txtPartTimeOtherWorker.Text.Trim = "" Then
                VPartTimeOtherWorker = GV.parseString(txtPartTimeOtherWorker.Text.Trim)
            Else
                VPartTimeOtherWorker = ""
            End If

            If Not txtNameOfEstablishment.Text.Trim = "" Then
                VNameOfEstablishment = GV.parseString(txtNameOfEstablishment.Text.Trim)
            Else
                VNameOfEstablishment = ""
            End If

            If ddlPreviousdetailofEstablishment.Items.Count > 0 Then
                If Not ddlPreviousdetailofEstablishment.SelectedValue.Trim = "" Then
                    VPreviousdetailofEstablishment = GV.parseString(ddlPreviousdetailofEstablishment.SelectedValue.Trim)
                Else
                    VPreviousdetailofEstablishment = ""
                End If
            End If

            If Not txtIfRenewalEnterShopactNo.Text.Trim = "" Then
                VIfRenewalEnterShopactNo = GV.parseString(txtIfRenewalEnterShopactNo.Text.Trim)
            Else
                VIfRenewalEnterShopactNo = ""
            End If

            If Not txtAddressofEstablishment.Text.Trim = "" Then
                VAddressofEstablishment = GV.parseString(txtAddressofEstablishment.Text.Trim)
            Else
                VAddressofEstablishment = ""
            End If

            If Not txtLocalityofEstablishment.Text.Trim = "" Then
                VLocalityofEstablishment = GV.parseString(txtLocalityofEstablishment.Text.Trim)
            Else
                VLocalityofEstablishment = ""
            End If

            If ddlStateofEstablishment.Items.Count > 0 Then
                If Not ddlStateofEstablishment.SelectedValue.Trim = "" Then
                    VStateofEstablishment = GV.parseString(ddlStateofEstablishment.SelectedValue.Trim)
                Else
                    VStateofEstablishment = ""
                End If
            End If

            If ddlDistrictofEstablishment.Items.Count > 0 Then
                If Not ddlDistrictofEstablishment.SelectedValue.Trim = "" Then
                    VDistrictofEstablishment = GV.parseString(ddlDistrictofEstablishment.SelectedValue.Trim)
                Else
                    VDistrictofEstablishment = ""
                End If
            End If

            If Not txtTalukaAndVillageofEstablishment.Text.Trim = "" Then
                VTalukaAndVillageofEstablishment = GV.parseString(txtTalukaAndVillageofEstablishment.Text.Trim)
            Else
                VTalukaAndVillageofEstablishment = ""
            End If

            If Not txtPINCodeofEstablishment.Text.Trim = "" Then
                VPINCodeofEstablishment = GV.parseString(txtPINCodeofEstablishment.Text.Trim)
            Else
                VPINCodeofEstablishment = ""
            End If

            If Not txtMobileNumber.Text.Trim = "" Then
                VMobileNumber = GV.parseString(txtMobileNumber.Text.Trim)
            Else
                VMobileNumber = ""
            End If

            If ddlOwnershipofPremises.Items.Count > 0 Then
                If Not ddlOwnershipofPremises.SelectedValue.Trim = "" Then
                    VOwnershipofPremises = GV.parseString(ddlOwnershipofPremises.SelectedValue.Trim)
                Else
                    VOwnershipofPremises = ""
                End If
            End If

            If Not txtDateofEstablishment.Text.Trim = "" Then
                VDateofEstablishment = GV.parseString(txtDateofEstablishment.Text.Trim)
            Else
                VDateofEstablishment = ""
            End If

            If Not txtNatureofBusiness.Text.Trim = "" Then
                VNatureofBusiness = GV.parseString(txtNatureofBusiness.Text.Trim)
            Else
                VNatureofBusiness = ""
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Shop_Act Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Shop_Act (RecordDateTime, EntryBy,FullNameofEmployer,AddressofEmployer,LocalityofEmployer,StateofEmployer,DistrictofEmployer,TalukaAndVillageofEmployer,PINCodeofEmployer,EmployerMobileNumber,EmployeremailId,ResidenceSince,EmployerAdhaarNo,EmployerStatus_Designation,CategoryofEstablishment,CategoryDetail,EstablishmentType,NoOfMenWorker,NoOfWomenWorker,NoOfOtherWorker,ApprenticesMenWorker,ApprenticesWomenWorker,ApprenticesOtherWorker,PartTimeMenWorker,PartTimeWomenWorker,PartTimeOtherWorker,NameOfEstablishment,PreviousdetailofEstablishment,IfRenewalEnterShopactNo,AddressofEstablishment,LocalityofEstablishment,StateofEstablishment,DistrictofEstablishment,TalukaAndVillageofEstablishment,PINCodeofEstablishment,MobileNumber,OwnershipofPremises,DateofEstablishment,NatureofBusiness) values( '" & VRecordDateTime & "','" & VEntryBy & "', '" & VFullNameofEmployer & "','" & VAddressofEmployer & "','" & VLocalityofEmployer & "','" & VStateofEmployer & "','" & VDistrictofEmployer & "','" & VTalukaAndVillageofEmployer & "','" & VPINCodeofEmployer & "','" & VEmployerMobileNumber & "','" & VEmployeremailId & "','" & VResidenceSince & "','" & VEmployerAdhaarNo & "','" & VEmployerStatus_Designation & "','" & VCategoryofEstablishment & "','" & VCategoryDetail & "','" & VEstablishmentType & "','" & VNoOfMenWorker & "','" & VNoOfWomenWorker & "','" & VNoOfOtherWorker & "','" & VApprenticesMenWorker & "','" & VApprenticesWomenWorker & "','" & VApprenticesOtherWorker & "','" & VPartTimeMenWorker & "','" & VPartTimeWomenWorker & "','" & VPartTimeOtherWorker & "','" & VNameOfEstablishment & "','" & VPreviousdetailofEstablishment & "','" & VIfRenewalEnterShopactNo & "','" & VAddressofEstablishment & "','" & VLocalityofEstablishment & "','" & VStateofEstablishment & "','" & VDistrictofEstablishment & "','" & VTalukaAndVillageofEstablishment & "','" & VPINCodeofEstablishment & "','" & VMobileNumber & "','" & VOwnershipofPremises & "','" & VDateofEstablishment & "','" & VNatureofBusiness & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Shop_Act set  UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', FullNameofEmployer='" & VFullNameofEmployer & "', AddressofEmployer='" & VAddressofEmployer & "', LocalityofEmployer='" & VLocalityofEmployer & "', StateofEmployer='" & VStateofEmployer & "', DistrictofEmployer='" & VDistrictofEmployer & "', TalukaAndVillageofEmployer='" & VTalukaAndVillageofEmployer & "', PINCodeofEmployer='" & VPINCodeofEmployer & "', EmployerMobileNumber='" & VEmployerMobileNumber & "', EmployeremailId='" & VEmployeremailId & "', ResidenceSince='" & VResidenceSince & "', EmployerAdhaarNo='" & VEmployerAdhaarNo & "', EmployerStatus_Designation='" & VEmployerStatus_Designation & "', CategoryofEstablishment='" & VCategoryofEstablishment & "', CategoryDetail='" & VCategoryDetail & "', EstablishmentType='" & VEstablishmentType & "', NoOfMenWorker='" & VNoOfMenWorker & "', NoOfWomenWorker='" & VNoOfWomenWorker & "', NoOfOtherWorker='" & VNoOfOtherWorker & "', ApprenticesMenWorker='" & VApprenticesMenWorker & "', ApprenticesWomenWorker='" & VApprenticesWomenWorker & "', ApprenticesOtherWorker='" & VApprenticesOtherWorker & "', PartTimeMenWorker='" & VPartTimeMenWorker & "', PartTimeWomenWorker='" & VPartTimeWomenWorker & "', PartTimeOtherWorker='" & VPartTimeOtherWorker & "', NameOfEstablishment='" & VNameOfEstablishment & "', PreviousdetailofEstablishment='" & VPreviousdetailofEstablishment & "', IfRenewalEnterShopactNo='" & VIfRenewalEnterShopactNo & "', AddressofEstablishment='" & VAddressofEstablishment & "', LocalityofEstablishment='" & VLocalityofEstablishment & "', StateofEstablishment='" & VStateofEstablishment & "', DistrictofEstablishment='" & VDistrictofEstablishment & "', TalukaAndVillageofEstablishment='" & VTalukaAndVillageofEstablishment & "', PINCodeofEstablishment='" & VPINCodeofEstablishment & "', MobileNumber='" & VMobileNumber & "', OwnershipofPremises='" & VOwnershipofPremises & "', DateofEstablishment='" & VDateofEstablishment & "', NatureofBusiness='" & VNatureofBusiness & "' where RID=" & lblRID.Text.Trim & ""
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
        End Try
    End Sub
    Private Sub Clear()
        Try
            VFullNameofEmployer = ""
            VAddressofEmployer = ""
            VLocalityofEmployer = ""
            VStateofEmployer = ""
            VDistrictofEmployer = ""
            VTalukaAndVillageofEmployer = ""
            VPINCodeofEmployer = ""
            VEmployerMobileNumber = ""
            VEmployeremailId = ""
            VResidenceSince = ""
            VEmployerAdhaarNo = ""
            VEmployerStatus_Designation = ""
            VCategoryofEstablishment = ""
            VCategoryDetail = ""
            VEstablishmentType = ""
            VNoOfMenWorker = ""
            VNoOfWomenWorker = ""
            VNoOfOtherWorker = ""
            VApprenticesMenWorker = ""
            VApprenticesWomenWorker = ""
            VApprenticesOtherWorker = ""
            VPartTimeMenWorker = ""
            VPartTimeWomenWorker = ""
            VPartTimeOtherWorker = ""
            VNameOfEstablishment = ""
            VPreviousdetailofEstablishment = ""
            VIfRenewalEnterShopactNo = ""
            VAddressofEstablishment = ""
            VLocalityofEstablishment = ""
            VStateofEstablishment = ""
            VDistrictofEstablishment = ""
            VTalukaAndVillageofEstablishment = ""
            VPINCodeofEstablishment = ""
            VMobileNumber = ""
            VOwnershipofPremises = ""
            VDateofEstablishment = ""
            VNatureofBusiness = ""
            txtFullNameofEmployer.Text = ""

            txtAddressofEmployer.Text = ""

            txtLocalityofEmployer.Text = ""

            If ddlStateofEmployer.Items.Count > 0 Then
                ddlStateofEmployer.SelectedIndex = 0
            End If

            If ddlDistrictofEmployer.Items.Count > 0 Then
                ddlDistrictofEmployer.SelectedIndex = 0
            End If

            txtTalukaAndVillageofEmployer.Text = ""

            txtPINCodeofEmployer.Text = ""

            txtEmployerMobileNumber.Text = ""

            txtEmployeremailId.Text = ""

            txtResidenceSince.Text = ""

            txtEmployerAdhaarNo.Text = ""

            txtEmployerStatus_Designation.Text = ""

            txtCategoryofEstablishment.Text = ""

            txtCategoryDetail.Text = ""

            txtEstablishmentType.Text = ""

            txtNoOfMenWorker.Text = ""

            txtNoOfWomenWorker.Text = ""

            txtNoOfOtherWorker.Text = ""

            txtApprenticesMenWorker.Text = ""

            txtApprenticesWomenWorker.Text = ""

            txtApprenticesOtherWorker.Text = ""

            txtPartTimeMenWorker.Text = ""

            txtPartTimeWomenWorker.Text = ""

            txtPartTimeOtherWorker.Text = ""

            txtNameOfEstablishment.Text = ""

            If ddlPreviousdetailofEstablishment.Items.Count > 0 Then
                ddlPreviousdetailofEstablishment.SelectedIndex = 0
            End If

            txtIfRenewalEnterShopactNo.Text = ""

            txtAddressofEstablishment.Text = ""

            txtLocalityofEstablishment.Text = ""

            If ddlStateofEstablishment.Items.Count > 0 Then
                ddlStateofEstablishment.SelectedIndex = 0
            End If

            If ddlDistrictofEstablishment.Items.Count > 0 Then
                ddlDistrictofEstablishment.SelectedIndex = 0
            End If

            txtTalukaAndVillageofEstablishment.Text = ""

            txtPINCodeofEstablishment.Text = ""

            txtMobileNumber.Text = ""

            If ddlOwnershipofPremises.Items.Count > 0 Then
                ddlOwnershipofPremises.SelectedIndex = 0
            End If

            txtDateofEstablishment.Text = ""

            txtNatureofBusiness.Text = ""
            ddlStateofEstablishment.SelectedValue = "DELHI"
            ddlDistrictofEstablishment.SelectedValue = "NORTH DELHI"

            ddlStateofEmployer.SelectedValue = "DELHI"
            ddlDistrictofEmployer.SelectedValue = "NORTH DELHI"

            Session("EditFlag") = 0
            btnSave.Text = "Save"
            btnClear.Text = "Reset"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Text = ""
                lblError.CssClass = ""

                GV.FL.AddInDropDownListDistinct(ddlStateofEmployer, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrictofEmployer, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")


                GV.FL.AddInDropDownListDistinct(ddlStateofEstablishment, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrictofEstablishment, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlStateofEstablishment.SelectedValue = "DELHI"
                ddlDistrictofEstablishment.SelectedValue = "NORTH DELHI"

                ddlStateofEmployer.SelectedValue = "DELHI"
                ddlDistrictofEmployer.SelectedValue = "NORTH DELHI"

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
                    formheading_1.Text = "Edit SHOP ACT "
                    DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Shop_Act where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("FullNameofEmployer")) Then
                                If Not DS.Tables(0).Rows(0).Item("FullNameofEmployer").ToString() = "" Then
                                    txtFullNameofEmployer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("FullNameofEmployer").ToString())
                                Else
                                    txtFullNameofEmployer.Text = ""
                                End If
                            Else
                                txtFullNameofEmployer.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AddressofEmployer")) Then
                                If Not DS.Tables(0).Rows(0).Item("AddressofEmployer").ToString() = "" Then
                                    txtAddressofEmployer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AddressofEmployer").ToString())
                                Else
                                    txtAddressofEmployer.Text = ""
                                End If
                            Else
                                txtAddressofEmployer.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("LocalityofEmployer")) Then
                                If Not DS.Tables(0).Rows(0).Item("LocalityofEmployer").ToString() = "" Then
                                    txtLocalityofEmployer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("LocalityofEmployer").ToString())
                                Else
                                    txtLocalityofEmployer.Text = ""
                                End If
                            Else
                                txtLocalityofEmployer.Text = ""
                            End If

                            If ddlStateofEmployer.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("StateofEmployer")) Then
                                    If Not DS.Tables(0).Rows(0).Item("StateofEmployer").ToString() = "" Then
                                        ddlStateofEmployer.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("StateofEmployer").ToString())
                                    Else
                                        ddlStateofEmployer.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStateofEmployer.SelectedIndex = 0
                                End If
                            End If

                            If ddlDistrictofEmployer.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("DistrictofEmployer")) Then
                                    If Not DS.Tables(0).Rows(0).Item("DistrictofEmployer").ToString() = "" Then
                                        ddlDistrictofEmployer.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("DistrictofEmployer").ToString())
                                    Else
                                        ddlDistrictofEmployer.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrictofEmployer.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("TalukaAndVillageofEmployer")) Then
                                If Not DS.Tables(0).Rows(0).Item("TalukaAndVillageofEmployer").ToString() = "" Then
                                    txtTalukaAndVillageofEmployer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("TalukaAndVillageofEmployer").ToString())
                                Else
                                    txtTalukaAndVillageofEmployer.Text = ""
                                End If
                            Else
                                txtTalukaAndVillageofEmployer.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PINCodeofEmployer")) Then
                                If Not DS.Tables(0).Rows(0).Item("PINCodeofEmployer").ToString() = "" Then
                                    txtPINCodeofEmployer.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PINCodeofEmployer").ToString())
                                Else
                                    txtPINCodeofEmployer.Text = ""
                                End If
                            Else
                                txtPINCodeofEmployer.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmployerMobileNumber")) Then
                                If Not DS.Tables(0).Rows(0).Item("EmployerMobileNumber").ToString() = "" Then
                                    txtEmployerMobileNumber.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EmployerMobileNumber").ToString())
                                Else
                                    txtEmployerMobileNumber.Text = ""
                                End If
                            Else
                                txtEmployerMobileNumber.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmployeremailId")) Then
                                If Not DS.Tables(0).Rows(0).Item("EmployeremailId").ToString() = "" Then
                                    txtEmployeremailId.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EmployeremailId").ToString())
                                Else
                                    txtEmployeremailId.Text = ""
                                End If
                            Else
                                txtEmployeremailId.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ResidenceSince")) Then
                                If Not DS.Tables(0).Rows(0).Item("ResidenceSince").ToString() = "" Then
                                    txtResidenceSince.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ResidenceSince").ToString())
                                Else
                                    txtResidenceSince.Text = ""
                                End If
                            Else
                                txtResidenceSince.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmployerAdhaarNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("EmployerAdhaarNo").ToString() = "" Then
                                    txtEmployerAdhaarNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EmployerAdhaarNo").ToString())
                                Else
                                    txtEmployerAdhaarNo.Text = ""
                                End If
                            Else
                                txtEmployerAdhaarNo.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmployerStatus_Designation")) Then
                                If Not DS.Tables(0).Rows(0).Item("EmployerStatus_Designation").ToString() = "" Then
                                    txtEmployerStatus_Designation.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EmployerStatus_Designation").ToString())
                                Else
                                    txtEmployerStatus_Designation.Text = ""
                                End If
                            Else
                                txtEmployerStatus_Designation.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("CategoryofEstablishment")) Then
                                If Not DS.Tables(0).Rows(0).Item("CategoryofEstablishment").ToString() = "" Then
                                    txtCategoryofEstablishment.Text = GV.parseString(DS.Tables(0).Rows(0).Item("CategoryofEstablishment").ToString())
                                Else
                                    txtCategoryofEstablishment.Text = ""
                                End If
                            Else
                                txtCategoryofEstablishment.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("CategoryDetail")) Then
                                If Not DS.Tables(0).Rows(0).Item("CategoryDetail").ToString() = "" Then
                                    txtCategoryDetail.Text = GV.parseString(DS.Tables(0).Rows(0).Item("CategoryDetail").ToString())
                                Else
                                    txtCategoryDetail.Text = ""
                                End If
                            Else
                                txtCategoryDetail.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("EstablishmentType")) Then
                                If Not DS.Tables(0).Rows(0).Item("EstablishmentType").ToString() = "" Then
                                    txtEstablishmentType.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EstablishmentType").ToString())
                                Else
                                    txtEstablishmentType.Text = ""
                                End If
                            Else
                                txtEstablishmentType.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NoOfMenWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("NoOfMenWorker").ToString() = "" Then
                                    txtNoOfMenWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NoOfMenWorker").ToString())
                                Else
                                    txtNoOfMenWorker.Text = ""
                                End If
                            Else
                                txtNoOfMenWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NoOfWomenWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("NoOfWomenWorker").ToString() = "" Then
                                    txtNoOfWomenWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NoOfWomenWorker").ToString())
                                Else
                                    txtNoOfWomenWorker.Text = ""
                                End If
                            Else
                                txtNoOfWomenWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NoOfOtherWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("NoOfOtherWorker").ToString() = "" Then
                                    txtNoOfOtherWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NoOfOtherWorker").ToString())
                                Else
                                    txtNoOfOtherWorker.Text = ""
                                End If
                            Else
                                txtNoOfOtherWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApprenticesMenWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApprenticesMenWorker").ToString() = "" Then
                                    txtApprenticesMenWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApprenticesMenWorker").ToString())
                                Else
                                    txtApprenticesMenWorker.Text = ""
                                End If
                            Else
                                txtApprenticesMenWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApprenticesWomenWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApprenticesWomenWorker").ToString() = "" Then
                                    txtApprenticesWomenWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApprenticesWomenWorker").ToString())
                                Else
                                    txtApprenticesWomenWorker.Text = ""
                                End If
                            Else
                                txtApprenticesWomenWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApprenticesOtherWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApprenticesOtherWorker").ToString() = "" Then
                                    txtApprenticesOtherWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApprenticesOtherWorker").ToString())
                                Else
                                    txtApprenticesOtherWorker.Text = ""
                                End If
                            Else
                                txtApprenticesOtherWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PartTimeMenWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("PartTimeMenWorker").ToString() = "" Then
                                    txtPartTimeMenWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PartTimeMenWorker").ToString())
                                Else
                                    txtPartTimeMenWorker.Text = ""
                                End If
                            Else
                                txtPartTimeMenWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PartTimeWomenWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("PartTimeWomenWorker").ToString() = "" Then
                                    txtPartTimeWomenWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PartTimeWomenWorker").ToString())
                                Else
                                    txtPartTimeWomenWorker.Text = ""
                                End If
                            Else
                                txtPartTimeWomenWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PartTimeOtherWorker")) Then
                                If Not DS.Tables(0).Rows(0).Item("PartTimeOtherWorker").ToString() = "" Then
                                    txtPartTimeOtherWorker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PartTimeOtherWorker").ToString())
                                Else
                                    txtPartTimeOtherWorker.Text = ""
                                End If
                            Else
                                txtPartTimeOtherWorker.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NameOfEstablishment")) Then
                                If Not DS.Tables(0).Rows(0).Item("NameOfEstablishment").ToString() = "" Then
                                    txtNameOfEstablishment.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NameOfEstablishment").ToString())
                                Else
                                    txtNameOfEstablishment.Text = ""
                                End If
                            Else
                                txtNameOfEstablishment.Text = ""
                            End If

                            If ddlPreviousdetailofEstablishment.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("PreviousdetailofEstablishment")) Then
                                    If Not DS.Tables(0).Rows(0).Item("PreviousdetailofEstablishment").ToString() = "" Then
                                        ddlPreviousdetailofEstablishment.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("PreviousdetailofEstablishment").ToString())
                                    Else
                                        ddlPreviousdetailofEstablishment.SelectedIndex = 0
                                    End If
                                Else
                                    ddlPreviousdetailofEstablishment.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("IfRenewalEnterShopactNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("IfRenewalEnterShopactNo").ToString() = "" Then
                                    txtIfRenewalEnterShopactNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("IfRenewalEnterShopactNo").ToString())
                                Else
                                    txtIfRenewalEnterShopactNo.Text = ""
                                End If
                            Else
                                txtIfRenewalEnterShopactNo.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AddressofEstablishment")) Then
                                If Not DS.Tables(0).Rows(0).Item("AddressofEstablishment").ToString() = "" Then
                                    txtAddressofEstablishment.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AddressofEstablishment").ToString())
                                Else
                                    txtAddressofEstablishment.Text = ""
                                End If
                            Else
                                txtAddressofEstablishment.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("LocalityofEstablishment")) Then
                                If Not DS.Tables(0).Rows(0).Item("LocalityofEstablishment").ToString() = "" Then
                                    txtLocalityofEstablishment.Text = GV.parseString(DS.Tables(0).Rows(0).Item("LocalityofEstablishment").ToString())
                                Else
                                    txtLocalityofEstablishment.Text = ""
                                End If
                            Else
                                txtLocalityofEstablishment.Text = ""
                            End If

                            If ddlStateofEstablishment.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("StateofEstablishment")) Then
                                    If Not DS.Tables(0).Rows(0).Item("StateofEstablishment").ToString() = "" Then
                                        ddlStateofEstablishment.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("StateofEstablishment").ToString())
                                    Else
                                        ddlStateofEstablishment.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStateofEstablishment.SelectedIndex = 0
                                End If
                            End If

                            If ddlDistrictofEstablishment.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("DistrictofEstablishment")) Then
                                    If Not DS.Tables(0).Rows(0).Item("DistrictofEstablishment").ToString() = "" Then
                                        ddlDistrictofEstablishment.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("DistrictofEstablishment").ToString())
                                    Else
                                        ddlDistrictofEstablishment.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrictofEstablishment.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("TalukaAndVillageofEstablishment")) Then
                                If Not DS.Tables(0).Rows(0).Item("TalukaAndVillageofEstablishment").ToString() = "" Then
                                    txtTalukaAndVillageofEstablishment.Text = GV.parseString(DS.Tables(0).Rows(0).Item("TalukaAndVillageofEstablishment").ToString())
                                Else
                                    txtTalukaAndVillageofEstablishment.Text = ""
                                End If
                            Else
                                txtTalukaAndVillageofEstablishment.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PINCodeofEstablishment")) Then
                                If Not DS.Tables(0).Rows(0).Item("PINCodeofEstablishment").ToString() = "" Then
                                    txtPINCodeofEstablishment.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PINCodeofEstablishment").ToString())
                                Else
                                    txtPINCodeofEstablishment.Text = ""
                                End If
                            Else
                                txtPINCodeofEstablishment.Text = ""
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

                            If ddlOwnershipofPremises.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("OwnershipofPremises")) Then
                                    If Not DS.Tables(0).Rows(0).Item("OwnershipofPremises").ToString() = "" Then
                                        ddlOwnershipofPremises.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("OwnershipofPremises").ToString())
                                    Else
                                        ddlOwnershipofPremises.SelectedIndex = 0
                                    End If
                                Else
                                    ddlOwnershipofPremises.SelectedIndex = 0
                                End If
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NatureofBusiness")) Then
                                If Not DS.Tables(0).Rows(0).Item("NatureofBusiness").ToString() = "" Then
                                    txtNatureofBusiness.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NatureofBusiness").ToString())
                                Else
                                    txtNatureofBusiness.Text = ""
                                End If
                            Else
                                txtNatureofBusiness.Text = ""
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
        End Try
    End Sub

End Class