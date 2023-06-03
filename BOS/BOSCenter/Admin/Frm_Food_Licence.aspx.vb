Public Class Frm_Food_Licence
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VNameOfApplicant, VDesignation, VKindofBusiness, VAddressofBusiness, VStateofBusiness, VSubDivision_Station_DivisionRailways, VDistrictofBussiness, VPINCodeofBusiness, VIsyourCorrespondenceAddresssameasAddressofPremises, VIFYESEnterDetailHere, VMobileNumber, VemailId, VNatureofBusiness, VNoofyearsyouwanttoapplyfor, VNameofthefoodcategory, VDateofEstablishment, Vopeningandclosingperiodoftheyear, VSourceofWaterSupply, Velectricpowerisusedinmanufacturingoffooditems As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim v_Error_String As String = ""
    Dim isErrorFound As Boolean = False
    Dim isFocusApplied As Boolean = False

    Dim DS As New DataSet



    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("NameTheForm.aspx") '/Change the name of form
            Else
                txtNameOfApplicant.CssClass = "form-control"
                ddlDesignation.CssClass = "form-control"
                ddlKindofBusiness.CssClass = "form-control"
                txtAddressofBusiness.CssClass = "form-control"
                ddlStateofBusiness.CssClass = "form-control"
                txtSubDivision_Station_DivisionRailways.CssClass = "form-control"
                ddlDistrictofBussiness.CssClass = "form-control"
                txtPINCodeofBusiness.CssClass = "form-control"
                ddlIsyourCorrespondenceAddresssameasAddressofPremises.CssClass = "form-control"
                txtIFYESEnterDetailHere.CssClass = "form-control"
                txtMobileNumber.CssClass = "form-control"
                txtemailId.CssClass = "form-control"
                txtNatureofBusiness.CssClass = "form-control"
                ddlNoofyearsyouwanttoapplyfor.CssClass = "form-control"
                txtNameofthefoodcategory.CssClass = "form-control"
                txtDateofEstablishment.CssClass = "form-control"
                txtopeningandclosingperiodoftheyear.CssClass = "form-control"
                ddlSourceofWaterSupply.CssClass = "form-control"
                ddlelectricpowerisusedinmanufacturingoffooditems.CssClass = "form-control"

                VNameOfApplicant = ""
                VDesignation = ""
                VKindofBusiness = ""
                VAddressofBusiness = ""
                VStateofBusiness = ""
                VSubDivision_Station_DivisionRailways = ""
                VDistrictofBussiness = ""
                VPINCodeofBusiness = ""
                VIsyourCorrespondenceAddresssameasAddressofPremises = ""
                VIFYESEnterDetailHere = ""
                VMobileNumber = ""
                VemailId = ""
                VNatureofBusiness = ""
                VNoofyearsyouwanttoapplyfor = ""
                VNameofthefoodcategory = ""
                VDateofEstablishment = ""
                Vopeningandclosingperiodoftheyear = ""
                VSourceofWaterSupply = ""
                Velectricpowerisusedinmanufacturingoffooditems = ""
                lblError.Text = ""
                lblError.CssClass = ""
                txtNameOfApplicant.Text = ""

                If ddlDesignation.Items.Count > 0 Then
                    ddlDesignation.SelectedIndex = 0
                End If

                If ddlKindofBusiness.Items.Count > 0 Then
                    ddlKindofBusiness.SelectedIndex = 0
                End If

                txtAddressofBusiness.Text = ""

                If ddlStateofBusiness.Items.Count > 0 Then
                    ddlStateofBusiness.SelectedIndex = 0
                End If

                txtSubDivision_Station_DivisionRailways.Text = ""

                If ddlDistrictofBussiness.Items.Count > 0 Then
                    ddlDistrictofBussiness.SelectedIndex = 0
                End If

                txtPINCodeofBusiness.Text = ""

                If ddlIsyourCorrespondenceAddresssameasAddressofPremises.Items.Count > 0 Then
                    ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedIndex = 0
                End If

                txtIFYESEnterDetailHere.Text = ""

                txtMobileNumber.Text = ""

                txtemailId.Text = ""

                txtNatureofBusiness.Text = ""

                If ddlNoofyearsyouwanttoapplyfor.Items.Count > 0 Then
                    ddlNoofyearsyouwanttoapplyfor.SelectedIndex = 0
                End If

                txtNameofthefoodcategory.Text = ""

                txtDateofEstablishment.Text = ""

                txtopeningandclosingperiodoftheyear.Text = ""

                If ddlSourceofWaterSupply.Items.Count > 0 Then
                    ddlSourceofWaterSupply.SelectedIndex = 0
                End If

                If ddlelectricpowerisusedinmanufacturingoffooditems.Items.Count > 0 Then
                    ddlelectricpowerisusedinmanufacturingoffooditems.SelectedIndex = 0
                End If
                ddlStateofBusiness.SelectedValue = "DELHI"
                ddlDistrictofBussiness.SelectedValue = "NORTH DELHI"

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

            lblError.Text = ""
            lblError.CssClass = ""

            '//// code by nidhi start 8 july 2022


            If GV.parseString(txtNameOfApplicant.Text) = "" Then
                txtNameOfApplicant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNameOfApplicant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNameOfApplicant.CssClass = "form-control"
                VNameOfApplicant = GV.parseString(txtNameOfApplicant.Text)
            End If

            If GV.parseString(ddlDesignation.SelectedIndex) = "" Then
                ddlDesignation.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDesignation.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDesignation.CssClass = "form-control"
                VDesignation = GV.parseString(ddlDesignation.SelectedValue)
            End If

            If GV.parseString(ddlKindofBusiness.SelectedIndex) = "" Then
                ddlKindofBusiness.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlKindofBusiness.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlKindofBusiness.CssClass = "form-control"
                VKindofBusiness = GV.parseString(ddlKindofBusiness.SelectedValue)
            End If

            If GV.parseString(txtAddressofBusiness.Text) = "" Then
                txtAddressofBusiness.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAddressofBusiness.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAddressofBusiness.CssClass = "form-control"
                VAddressofBusiness = GV.parseString(txtAddressofBusiness.Text)
            End If

            If GV.parseString(ddlStateofBusiness.SelectedValue) = "" Then
                ddlStateofBusiness.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStateofBusiness.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStateofBusiness.CssClass = "form-control"
                VStateofBusiness = GV.parseString(ddlStateofBusiness.SelectedValue)
            End If

            If GV.parseString(txtSubDivision_Station_DivisionRailways.Text) = "" Then
                txtSubDivision_Station_DivisionRailways.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtSubDivision_Station_DivisionRailways.Focus()
                    isFocusApplied = True
                End If
            Else
                txtSubDivision_Station_DivisionRailways.CssClass = "form-control"
                VSubDivision_Station_DivisionRailways = GV.parseString(txtSubDivision_Station_DivisionRailways.Text)
            End If

            If GV.parseString(ddlDistrictofBussiness.SelectedValue) = "" Then
                ddlDistrictofBussiness.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrictofBussiness.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrictofBussiness.CssClass = "form-control"
                VDistrictofBussiness = GV.parseString(ddlDistrictofBussiness.SelectedValue)
            End If

            If GV.parseString(txtPINCodeofBusiness.Text) = "" Then
                txtPINCodeofBusiness.CssClass = "ValidationError"
                isErrorFound = True

                If isFocusApplied = False Then
                    txtPINCodeofBusiness.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtPINCodeofBusiness.Text) Then
                txtPINCodeofBusiness.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct PIN No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct PIN No."
                End If

                If isFocusApplied = False Then
                    txtPINCodeofBusiness.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtPINCodeofBusiness.Text).Length = 10 Then
                txtPINCodeofBusiness.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 6 Digit PIN No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 6 Digit PIN No."
                End If

                If isFocusApplied = False Then
                    txtPINCodeofBusiness.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPINCodeofBusiness.CssClass = "form-control"
                VPINCodeofBusiness = GV.parseString(txtPINCodeofBusiness.Text)
            End If

            If GV.parseString(ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedValue) = "" Then
                ddlIsyourCorrespondenceAddresssameasAddressofPremises.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlIsyourCorrespondenceAddresssameasAddressofPremises.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlIsyourCorrespondenceAddresssameasAddressofPremises.CssClass = "form-control"
                VIsyourCorrespondenceAddresssameasAddressofPremises = GV.parseString(ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedValue)
            End If

            If GV.parseString(txtIFYESEnterDetailHere.Text) = "" Then
                txtIFYESEnterDetailHere.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtIFYESEnterDetailHere.Focus()
                    isFocusApplied = True
                End If
            Else
                txtIFYESEnterDetailHere.CssClass = "form-control"
                VIFYESEnterDetailHere = GV.parseString(txtIFYESEnterDetailHere.Text)
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

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Mobile No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Mobile No."
                End If


                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtMobileNumber.Text).Length = 10 Then
                txtMobileNumber.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 10 Digit Mobile No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 10 Digit Mobile No."
                End If


                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtMobileNumber.CssClass = "form-control"
                VMobileNumber = GV.parseString(txtMobileNumber.Text)
            End If

            If GV.parseString(txtemailId.Text) = "" Then
                txtemailId.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtemailId.Focus()
                    isFocusApplied = True
                End If
            Else
                txtemailId.CssClass = "form-control"
                VemailId = GV.parseString(txtemailId.Text)
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

            If GV.parseString(ddlNoofyearsyouwanttoapplyfor.SelectedValue) = "" Then
                ddlNoofyearsyouwanttoapplyfor.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlNoofyearsyouwanttoapplyfor.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlNoofyearsyouwanttoapplyfor.CssClass = "form-control"
                VNoofyearsyouwanttoapplyfor = GV.parseString(ddlNoofyearsyouwanttoapplyfor.SelectedValue)
            End If

            If GV.parseString(txtNameofthefoodcategory.Text) = "" Then
                txtNameofthefoodcategory.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNameofthefoodcategory.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNameofthefoodcategory.CssClass = "form-control"
                VNameofthefoodcategory = GV.parseString(txtNameofthefoodcategory.Text)
            End If

            If GV.parseString(txtDateofEstablishment.Text) = "" Then
                txtDateofEstablishment.CssClass = "ValidationError"
                isErrorFound = True

                If isFocusApplied = False Then
                    txtDateofEstablishment.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsDate(GV.FL.returnDateMonthWiseWithDateChecking(txtDateofEstablishment.Text)) = True Then
                txtDateofEstablishment.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Date Format."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Date Format."
                End If

                If isFocusApplied = False Then
                    txtDateofEstablishment.Focus()
                    isFocusApplied = True
                End If
            Else

                txtDateofEstablishment.CssClass = "form-control"
                VDateofEstablishment = GV.FL.returnDateMonthWiseWithDateChecking(txtDateofEstablishment.Text.Trim)

            End If



            If GV.parseString(txtopeningandclosingperiodoftheyear.Text) = "" Then
                txtopeningandclosingperiodoftheyear.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtopeningandclosingperiodoftheyear.Focus()
                    isFocusApplied = True
                End If
            Else
                txtopeningandclosingperiodoftheyear.CssClass = "form-control"
                Vopeningandclosingperiodoftheyear = GV.parseString(txtopeningandclosingperiodoftheyear.Text)
            End If

            If GV.parseString(ddlSourceofWaterSupply.SelectedValue) = "" Then
                ddlSourceofWaterSupply.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlSourceofWaterSupply.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlSourceofWaterSupply.CssClass = "form-control"
                VSourceofWaterSupply = GV.parseString(ddlSourceofWaterSupply.SelectedValue)
            End If

            If GV.parseString(ddlelectricpowerisusedinmanufacturingoffooditems.SelectedValue) = "" Then
                ddlelectricpowerisusedinmanufacturingoffooditems.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlelectricpowerisusedinmanufacturingoffooditems.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlelectricpowerisusedinmanufacturingoffooditems.CssClass = "form-control"
                Velectricpowerisusedinmanufacturingoffooditems = GV.parseString(ddlelectricpowerisusedinmanufacturingoffooditems.SelectedValue)
            End If
           
            If isErrorFound = True Then
                If Not v_Error_String.Trim = "" Then
                    lblError.Text = v_Error_String
                    lblError.CssClass = "errorlabels"
                Else
                    lblError.CssClass = False
                End If
                lblError.Visible = True
                Exit Sub
            End If
            '////End code by nidhi
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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_FOOD_LICENCE where RID=" & lblRID.Text.Trim & ""
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
            If Not txtNameOfApplicant.Text.Trim = "" Then
                VNameOfApplicant = GV.parseString(txtNameOfApplicant.Text.Trim)
            Else
                VNameOfApplicant = ""
            End If

            If ddlDesignation.Items.Count > 0 Then
                If Not ddlDesignation.SelectedValue.Trim = "" Then
                    VDesignation = GV.parseString(ddlDesignation.SelectedValue.Trim)
                Else
                    VDesignation = ""
                End If
            End If

            If ddlKindofBusiness.Items.Count > 0 Then
                If Not ddlKindofBusiness.SelectedValue.Trim = "" Then
                    VKindofBusiness = GV.parseString(ddlKindofBusiness.SelectedValue.Trim)
                Else
                    VKindofBusiness = ""
                End If
            End If

            If Not txtAddressofBusiness.Text.Trim = "" Then
                VAddressofBusiness = GV.parseString(txtAddressofBusiness.Text.Trim)
            Else
                VAddressofBusiness = ""
            End If

            If ddlStateofBusiness.Items.Count > 0 Then
                If Not ddlStateofBusiness.SelectedValue.Trim = "" Then
                    VStateofBusiness = GV.parseString(ddlStateofBusiness.SelectedValue.Trim)
                Else
                    VStateofBusiness = ""
                End If
            End If

            If Not txtSubDivision_Station_DivisionRailways.Text.Trim = "" Then
                VSubDivision_Station_DivisionRailways = GV.parseString(txtSubDivision_Station_DivisionRailways.Text.Trim)
            Else
                VSubDivision_Station_DivisionRailways = ""
            End If

            If ddlDistrictofBussiness.Items.Count > 0 Then
                If Not ddlDistrictofBussiness.SelectedValue.Trim = "" Then
                    VDistrictofBussiness = GV.parseString(ddlDistrictofBussiness.SelectedValue.Trim)
                Else
                    VDistrictofBussiness = ""
                End If
            End If

            If Not txtPINCodeofBusiness.Text.Trim = "" Then
                VPINCodeofBusiness = GV.parseString(txtPINCodeofBusiness.Text.Trim)
            Else
                VPINCodeofBusiness = ""
            End If

            If ddlIsyourCorrespondenceAddresssameasAddressofPremises.Items.Count > 0 Then
                If Not ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedValue.Trim = "" Then
                    VIsyourCorrespondenceAddresssameasAddressofPremises = GV.parseString(ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedValue.Trim)
                Else
                    VIsyourCorrespondenceAddresssameasAddressofPremises = ""
                End If
            End If

            If Not txtIFYESEnterDetailHere.Text.Trim = "" Then
                VIFYESEnterDetailHere = GV.parseString(txtIFYESEnterDetailHere.Text.Trim)
            Else
                VIFYESEnterDetailHere = ""
            End If

            If Not txtMobileNumber.Text.Trim = "" Then
                VMobileNumber = GV.parseString(txtMobileNumber.Text.Trim)
            Else
                VMobileNumber = ""
            End If

            If Not txtemailId.Text.Trim = "" Then
                VemailId = GV.parseString(txtemailId.Text.Trim)
            Else
                VemailId = ""
            End If

            If Not txtNatureofBusiness.Text.Trim = "" Then
                VNatureofBusiness = GV.parseString(txtNatureofBusiness.Text.Trim)
            Else
                VNatureofBusiness = ""
            End If

            If ddlNoofyearsyouwanttoapplyfor.Items.Count > 0 Then
                If Not ddlNoofyearsyouwanttoapplyfor.SelectedValue.Trim = "" Then
                    VNoofyearsyouwanttoapplyfor = GV.parseString(ddlNoofyearsyouwanttoapplyfor.SelectedValue.Trim)
                Else
                    VNoofyearsyouwanttoapplyfor = ""
                End If
            End If

            If Not txtNameofthefoodcategory.Text.Trim = "" Then
                VNameofthefoodcategory = GV.parseString(txtNameofthefoodcategory.Text.Trim)
            Else
                VNameofthefoodcategory = ""
            End If

            If Not txtDateofEstablishment.Text.Trim = "" Then
                VDateofEstablishment = GV.parseString(txtDateofEstablishment.Text.Trim)
            Else
                VDateofEstablishment = ""
            End If

            If Not txtopeningandclosingperiodoftheyear.Text.Trim = "" Then
                Vopeningandclosingperiodoftheyear = GV.parseString(txtopeningandclosingperiodoftheyear.Text.Trim)
            Else
                Vopeningandclosingperiodoftheyear = ""
            End If

            If ddlSourceofWaterSupply.Items.Count > 0 Then
                If Not ddlSourceofWaterSupply.SelectedValue.Trim = "" Then
                    VSourceofWaterSupply = GV.parseString(ddlSourceofWaterSupply.SelectedValue.Trim)
                Else
                    VSourceofWaterSupply = ""
                End If
            End If

            If ddlelectricpowerisusedinmanufacturingoffooditems.Items.Count > 0 Then
                If Not ddlelectricpowerisusedinmanufacturingoffooditems.SelectedValue.Trim = "" Then
                    Velectricpowerisusedinmanufacturingoffooditems = GV.parseString(ddlelectricpowerisusedinmanufacturingoffooditems.SelectedValue.Trim)
                Else
                    Velectricpowerisusedinmanufacturingoffooditems = ""
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_FOOD_LICENCE Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_FOOD_LICENCE (RecordDateTime, EntryBy,NameOfApplicant,Designation,KindofBusiness,AddressofBusiness,StateofBusiness,SubDivision_Station_DivisionRailways,DistrictofBussiness,PINCodeofBusiness,IsyourCorrespondenceAddresssameasAddressofPremises,IFYESEnterDetailHere,MobileNumber,emailId,NatureofBusiness,Noofyearsyouwanttoapplyfor,Nameofthefoodcategory,DateofEstablishment,openingandclosingperiodoftheyear,SourceofWaterSupply,electricpowerisusedinmanufacturingoffooditems) values(  '" & VRecordDateTime & "','" & VEntryBy & "','" & VNameOfApplicant & "','" & VDesignation & "','" & VKindofBusiness & "','" & VAddressofBusiness & "','" & VStateofBusiness & "','" & VSubDivision_Station_DivisionRailways & "','" & VDistrictofBussiness & "','" & VPINCodeofBusiness & "','" & VIsyourCorrespondenceAddresssameasAddressofPremises & "','" & VIFYESEnterDetailHere & "','" & VMobileNumber & "','" & VemailId & "','" & VNatureofBusiness & "','" & VNoofyearsyouwanttoapplyfor & "','" & VNameofthefoodcategory & "','" & VDateofEstablishment & "','" & Vopeningandclosingperiodoftheyear & "','" & VSourceofWaterSupply & "','" & Velectricpowerisusedinmanufacturingoffooditems & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_FOOD_LICENCE set  UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', NameOfApplicant='" & VNameOfApplicant & "', Designation='" & VDesignation & "', KindofBusiness='" & VKindofBusiness & "', AddressofBusiness='" & VAddressofBusiness & "', StateofBusiness='" & VStateofBusiness & "', SubDivision_Station_DivisionRailways='" & VSubDivision_Station_DivisionRailways & "', DistrictofBussiness='" & VDistrictofBussiness & "', PINCodeofBusiness='" & VPINCodeofBusiness & "', IsyourCorrespondenceAddresssameasAddressofPremises='" & VIsyourCorrespondenceAddresssameasAddressofPremises & "', IFYESEnterDetailHere='" & VIFYESEnterDetailHere & "', MobileNumber='" & VMobileNumber & "', emailId='" & VemailId & "', NatureofBusiness='" & VNatureofBusiness & "', Noofyearsyouwanttoapplyfor='" & VNoofyearsyouwanttoapplyfor & "', Nameofthefoodcategory='" & VNameofthefoodcategory & "', DateofEstablishment='" & VDateofEstablishment & "', openingandclosingperiodoftheyear='" & Vopeningandclosingperiodoftheyear & "', SourceofWaterSupply='" & VSourceofWaterSupply & "', electricpowerisusedinmanufacturingoffooditems='" & Velectricpowerisusedinmanufacturingoffooditems & "' where RID=" & lblRID.Text.Trim & ""
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
            VNameOfApplicant = ""
            VDesignation = ""
            VKindofBusiness = ""
            VAddressofBusiness = ""
            VStateofBusiness = ""
            VSubDivision_Station_DivisionRailways = ""
            VDistrictofBussiness = ""
            VPINCodeofBusiness = ""
            VIsyourCorrespondenceAddresssameasAddressofPremises = ""
            VIFYESEnterDetailHere = ""
            VMobileNumber = ""
            VemailId = ""
            VNatureofBusiness = ""
            VNoofyearsyouwanttoapplyfor = ""
            VNameofthefoodcategory = ""
            VDateofEstablishment = ""
            Vopeningandclosingperiodoftheyear = ""
            VSourceofWaterSupply = ""
            Velectricpowerisusedinmanufacturingoffooditems = ""
            txtNameOfApplicant.Text = ""

            If ddlDesignation.Items.Count > 0 Then
                ddlDesignation.SelectedIndex = 0
            End If

            If ddlKindofBusiness.Items.Count > 0 Then
                ddlKindofBusiness.SelectedIndex = 0
            End If

            txtAddressofBusiness.Text = ""

            If ddlStateofBusiness.Items.Count > 0 Then
                ddlStateofBusiness.SelectedIndex = 0
            End If

            txtSubDivision_Station_DivisionRailways.Text = ""

            If ddlDistrictofBussiness.Items.Count > 0 Then
                ddlDistrictofBussiness.SelectedIndex = 0
            End If

            txtPINCodeofBusiness.Text = ""

            If ddlIsyourCorrespondenceAddresssameasAddressofPremises.Items.Count > 0 Then
                ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedIndex = 0
            End If

            txtIFYESEnterDetailHere.Text = ""

            txtMobileNumber.Text = ""

            txtemailId.Text = ""

            txtNatureofBusiness.Text = ""

            If ddlNoofyearsyouwanttoapplyfor.Items.Count > 0 Then
                ddlNoofyearsyouwanttoapplyfor.SelectedIndex = 0
            End If

            txtNameofthefoodcategory.Text = ""

            txtDateofEstablishment.Text = ""

            txtopeningandclosingperiodoftheyear.Text = ""

            If ddlSourceofWaterSupply.Items.Count > 0 Then
                ddlSourceofWaterSupply.SelectedIndex = 0
            End If

            If ddlelectricpowerisusedinmanufacturingoffooditems.Items.Count > 0 Then
                ddlelectricpowerisusedinmanufacturingoffooditems.SelectedIndex = 0
            End If
            ddlStateofBusiness.SelectedValue = "DELHI"
            ddlDistrictofBussiness.SelectedValue = "NORTH DELHI"


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

                GV.FL.AddInDropDownListDistinct(ddlStateofBusiness, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrictofBussiness, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlStateofBusiness.SelectedValue = "DELHI"
                ddlDistrictofBussiness.SelectedValue = "NORTH DELHI"


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
                    formheading_1.Text = "Edit FOOD LICENCE "
                    DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_FOOD_LICENCE where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NameOfApplicant")) Then
                                If Not DS.Tables(0).Rows(0).Item("NameOfApplicant").ToString() = "" Then
                                    txtNameOfApplicant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NameOfApplicant").ToString())
                                Else
                                    txtNameOfApplicant.Text = ""
                                End If
                            Else
                                txtNameOfApplicant.Text = ""
                            End If

                            If ddlDesignation.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Designation")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Designation").ToString() = "" Then
                                        ddlDesignation.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Designation").ToString())
                                    Else
                                        ddlDesignation.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDesignation.SelectedIndex = 0
                                End If
                            End If

                            If ddlKindofBusiness.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("KindofBusiness")) Then
                                    If Not DS.Tables(0).Rows(0).Item("KindofBusiness").ToString() = "" Then
                                        ddlKindofBusiness.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("KindofBusiness").ToString())
                                    Else
                                        ddlKindofBusiness.SelectedIndex = 0
                                    End If
                                Else
                                    ddlKindofBusiness.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AddressofBusiness")) Then
                                If Not DS.Tables(0).Rows(0).Item("AddressofBusiness").ToString() = "" Then
                                    txtAddressofBusiness.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AddressofBusiness").ToString())
                                Else
                                    txtAddressofBusiness.Text = ""
                                End If
                            Else
                                txtAddressofBusiness.Text = ""
                            End If

                            If ddlStateofBusiness.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("StateofBusiness")) Then
                                    If Not DS.Tables(0).Rows(0).Item("StateofBusiness").ToString() = "" Then
                                        ddlStateofBusiness.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("StateofBusiness").ToString())
                                    Else
                                        ddlStateofBusiness.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStateofBusiness.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("SubDivision_Station_DivisionRailways")) Then
                                If Not DS.Tables(0).Rows(0).Item("SubDivision_Station_DivisionRailways").ToString() = "" Then
                                    txtSubDivision_Station_DivisionRailways.Text = GV.parseString(DS.Tables(0).Rows(0).Item("SubDivision_Station_DivisionRailways").ToString())
                                Else
                                    txtSubDivision_Station_DivisionRailways.Text = ""
                                End If
                            Else
                                txtSubDivision_Station_DivisionRailways.Text = ""
                            End If

                            If ddlDistrictofBussiness.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("DistrictofBussiness")) Then
                                    If Not DS.Tables(0).Rows(0).Item("DistrictofBussiness").ToString() = "" Then
                                        ddlDistrictofBussiness.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("DistrictofBussiness").ToString())
                                    Else
                                        ddlDistrictofBussiness.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrictofBussiness.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PINCodeofBusiness")) Then
                                If Not DS.Tables(0).Rows(0).Item("PINCodeofBusiness").ToString() = "" Then
                                    txtPINCodeofBusiness.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PINCodeofBusiness").ToString())
                                Else
                                    txtPINCodeofBusiness.Text = ""
                                End If
                            Else
                                txtPINCodeofBusiness.Text = ""
                            End If

                            If ddlIsyourCorrespondenceAddresssameasAddressofPremises.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("IsyourCorrespondenceAddresssameasAddressofPremises")) Then
                                    If Not DS.Tables(0).Rows(0).Item("IsyourCorrespondenceAddresssameasAddressofPremises").ToString() = "" Then
                                        ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("IsyourCorrespondenceAddresssameasAddressofPremises").ToString())
                                    Else
                                        ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedIndex = 0
                                    End If
                                Else
                                    ddlIsyourCorrespondenceAddresssameasAddressofPremises.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("IFYESEnterDetailHere")) Then
                                If Not DS.Tables(0).Rows(0).Item("IFYESEnterDetailHere").ToString() = "" Then
                                    txtIFYESEnterDetailHere.Text = GV.parseString(DS.Tables(0).Rows(0).Item("IFYESEnterDetailHere").ToString())
                                Else
                                    txtIFYESEnterDetailHere.Text = ""
                                End If
                            Else
                                txtIFYESEnterDetailHere.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("emailId")) Then
                                If Not DS.Tables(0).Rows(0).Item("emailId").ToString() = "" Then
                                    txtemailId.Text = GV.parseString(DS.Tables(0).Rows(0).Item("emailId").ToString())
                                Else
                                    txtemailId.Text = ""
                                End If
                            Else
                                txtemailId.Text = ""
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

                            If ddlNoofyearsyouwanttoapplyfor.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Noofyearsyouwanttoapplyfor")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Noofyearsyouwanttoapplyfor").ToString() = "" Then
                                        ddlNoofyearsyouwanttoapplyfor.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Noofyearsyouwanttoapplyfor").ToString())
                                    Else
                                        ddlNoofyearsyouwanttoapplyfor.SelectedIndex = 0
                                    End If
                                Else
                                    ddlNoofyearsyouwanttoapplyfor.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Nameofthefoodcategory")) Then
                                If Not DS.Tables(0).Rows(0).Item("Nameofthefoodcategory").ToString() = "" Then
                                    txtNameofthefoodcategory.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Nameofthefoodcategory").ToString())
                                Else
                                    txtNameofthefoodcategory.Text = ""
                                End If
                            Else
                                txtNameofthefoodcategory.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("openingandclosingperiodoftheyear")) Then
                                If Not DS.Tables(0).Rows(0).Item("openingandclosingperiodoftheyear").ToString() = "" Then
                                    txtopeningandclosingperiodoftheyear.Text = GV.parseString(DS.Tables(0).Rows(0).Item("openingandclosingperiodoftheyear").ToString())
                                Else
                                    txtopeningandclosingperiodoftheyear.Text = ""
                                End If
                            Else
                                txtopeningandclosingperiodoftheyear.Text = ""
                            End If

                            If ddlSourceofWaterSupply.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("SourceofWaterSupply")) Then
                                    If Not DS.Tables(0).Rows(0).Item("SourceofWaterSupply").ToString() = "" Then
                                        ddlSourceofWaterSupply.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("SourceofWaterSupply").ToString())
                                    Else
                                        ddlSourceofWaterSupply.SelectedIndex = 0
                                    End If
                                Else
                                    ddlSourceofWaterSupply.SelectedIndex = 0
                                End If
                            End If

                            If ddlelectricpowerisusedinmanufacturingoffooditems.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("electricpowerisusedinmanufacturingoffooditems")) Then
                                    If Not DS.Tables(0).Rows(0).Item("electricpowerisusedinmanufacturingoffooditems").ToString() = "" Then
                                        ddlelectricpowerisusedinmanufacturingoffooditems.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("electricpowerisusedinmanufacturingoffooditems").ToString())
                                    Else
                                        ddlelectricpowerisusedinmanufacturingoffooditems.SelectedIndex = 0
                                    End If
                                Else
                                    ddlelectricpowerisusedinmanufacturingoffooditems.SelectedIndex = 0
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