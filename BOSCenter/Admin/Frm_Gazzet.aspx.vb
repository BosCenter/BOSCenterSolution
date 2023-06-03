Public Class Frm_Gazzet
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim isErrorFound As Boolean = False
    Dim isFocusApplied As Boolean = False
    Dim v_Error_String As String = ""
    Dim VApplicantCategory, VApplicantTitle, VApplicantFullName, VApplicantDOB, VAge, VGender, VMobileNo, VEmail, VAdhaarNo, VAddress, VDistrict, VTaluka, VVillage, VPin, VApplicationfor, VIfChangeInDateOfBirth, VIfChangeInNameOldFirstName, VOldMidleName, VOldLastName, VNewFirstName, VNewMidleName, VNewLastName, VReason As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("NameTheForm.aspx") '/Change the name of form
            Else
                ddlApplicantCategory.CssClass = "form-control"
                ddlApplicantTitle.CssClass = "form-control"
                txtApplicantFullName.CssClass = "form-control"
                txtApplicantDOB.CssClass = "form-control"
                txtAge.CssClass = "form-control"
                ddlGender.CssClass = "form-control"
                txtMobileNo.CssClass = "form-control"
                txtEmail.CssClass = "form-control"
                txtAdhaarNo.CssClass = "form-control"
                txtAddress.CssClass = "form-control"
                ddlDistrict.CssClass = "form-control"
                txtTaluka.CssClass = "form-control"
                txtVillage.CssClass = "form-control"
                txtPin.CssClass = "form-control"
                ddlApplicationfor.CssClass = "form-control"
                txtIfChangeInDateOfBirth.CssClass = "from-control"
                txtIfChangeInNameOldFirstName.CssClass = "form-control"
                txtOldMidleName.CssClass = "form-control"
                txtOldLastName.CssClass = "form-control"
                txtNewFirstName.CssClass = "form-control"
                txtNewMidleName.CssClass = "form-control"
                txtNewLastName.CssClass = "form-control"
                txtReason.CssClass = "form-control"

                VApplicantCategory = ""
                VApplicantTitle = ""
                VApplicantFullName = ""
                VApplicantDOB = ""
                VAge = ""
                VGender = ""
                VMobileNo = ""
                VEmail = ""
                VAdhaarNo = ""
                VAddress = ""
                VDistrict = ""
                VTaluka = ""
                VVillage = ""
                VPin = ""
                VApplicationfor = ""
                VIfChangeInDateOfBirth = ""
                VIfChangeInNameOldFirstName = ""
                VOldMidleName = ""
                VOldLastName = ""
                VNewFirstName = ""
                VNewMidleName = ""
                VNewLastName = ""
                VReason = ""
                lblError.Text = ""
                lblError.CssClass = ""
                If ddlApplicantCategory.Items.Count > 0 Then
                    ddlApplicantCategory.SelectedIndex = 0
                End If

                If ddlApplicantTitle.Items.Count > 0 Then
                    ddlApplicantTitle.SelectedIndex = 0
                End If

                txtApplicantFullName.Text = ""

                txtApplicantDOB.Text = ""

                txtAge.Text = ""

                If ddlGender.Items.Count > 0 Then
                    ddlGender.SelectedIndex = 0
                End If

                txtMobileNo.Text = ""

                txtEmail.Text = ""

                txtAdhaarNo.Text = ""

                txtAddress.Text = ""

                If ddlDistrict.Items.Count > 0 Then
                    ddlDistrict.SelectedIndex = 0
                End If

                txtTaluka.Text = ""

                txtVillage.Text = ""

                txtPin.Text = ""

                If ddlApplicationfor.Items.Count > 0 Then
                    ddlApplicationfor.SelectedIndex = 0
                End If

                txtIfChangeInDateOfBirth.Text = ""

                txtIfChangeInNameOldFirstName.Text = ""

                txtOldMidleName.Text = ""

                txtOldLastName.Text = ""

                txtNewFirstName.Text = ""

                txtNewMidleName.Text = ""

                txtNewLastName.Text = ""

                txtReason.Text = ""
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
            '///////// start code by neetu - start 8 july 2022 '///////////////

            If GV.parseString(ddlApplicantCategory.SelectedValue) = "" Then
                ddlApplicantCategory.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlApplicantCategory.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlApplicantCategory.CssClass = "form-control"
                VApplicantCategory = GV.parseString(ddlApplicantCategory.SelectedValue)
            End If

            If GV.parseString(ddlApplicantTitle.SelectedValue) = "" Then
                ddlApplicantTitle.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlApplicantTitle.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlApplicantTitle.CssClass = "form-control"
                VApplicantTitle = GV.parseString(ddlApplicantTitle.SelectedValue)
            End If

            If GV.parseString(txtApplicantFullName.Text) = "" Then
                txtApplicantFullName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantFullName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtApplicantFullName.CssClass = "form-control"
                VApplicantFullName = GV.parseString(txtApplicantFullName.Text)
            End If

            If GV.parseString(txtApplicantDOB.Text) = "" Then
                txtApplicantDOB.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtApplicantDOB.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsDate(GV.FL.returnDateMonthWiseWithDateChecking(txtApplicantDOB.Text)) = True Then
                txtApplicantDOB.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Date Format."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Date Format."
                End If

                If isFocusApplied = False Then
                    txtApplicantDOB.Focus()
                    isFocusApplied = True
                End If
            Else

                txtApplicantDOB.CssClass = "form-control"
                VApplicantDOB = GV.FL.returnDateMonthWiseWithDateChecking(txtApplicantDOB.Text.Trim)

            End If


            If GV.parseString(txtAge.Text) = "" Then
                txtAge.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAge.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAge.CssClass = "form-control"
                VAge = GV.parseString(txtAge.Text)
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



            If GV.parseString(txtMobileNo.Text) = "" Then
                txtMobileNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtMobileNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtMobileNo.Text) Then
                txtMobileNo.CssClass = "ValidationError"
                isErrorFound = True
                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Numeric Digit."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Numeric Digit."
                End If
                If isFocusApplied = False Then
                    txtMobileNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtMobileNo.Text).Length = 10 Then
                txtMobileNo.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 10 Digit Phone No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 10 Digit Phone No."
                End If
                If isFocusApplied = False Then
                    txtMobileNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtMobileNo.CssClass = "Form-control"
                VMobileNo = GV.parseString(txtMobileNo.Text)
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

            If GV.parseString(txtAdhaarNo.Text) = "" Then
                txtAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAdhaarNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtAdhaarNo.Text) Then
                txtAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True
                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Adhaar No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Adhaar No."
                End If
                If isFocusApplied = False Then
                    txtAdhaarNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtAdhaarNo.Text).Length = 12 Then
                txtAdhaarNo.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 12 Digit Adhaar No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 12 Digit Adhaar No."
                End If
                If isFocusApplied = False Then
                    txtAdhaarNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAdhaarNo.CssClass = "Form-control"
                VAdhaarNo = GV.parseString(txtAdhaarNo.Text)
            End If

            If GV.parseString(txtAddress.Text) = "" Then
                txtAddress.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAddress.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAddress.CssClass = "form-control"
                VAddress = GV.parseString(txtAddress.Text)
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

            If GV.parseString(txtTaluka.Text) = "" Then
                txtTaluka.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtTaluka.Focus()
                    isFocusApplied = True
                End If
            Else
                txtTaluka.CssClass = "form-control"
                VTaluka = GV.parseString(txtTaluka.Text)
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


            If GV.parseString(txtPin.Text) = "" Then
                txtPin.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPin.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtPin.Text) Then
                txtPin.CssClass = "ValidationError"
                isErrorFound = True
                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Pin No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Pin No."
                End If
                If isFocusApplied = False Then
                    txtPin.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtPin.Text).Length = 6 Then
                txtPin.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 6 Digit Pin No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 6 Digit Pin No."
                End If
                If isFocusApplied = False Then
                    txtPin.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPin.CssClass = "Form-control"
                VPin = GV.parseString(txtPin.Text)
            End If

            If GV.parseString(ddlApplicationfor.SelectedValue) = "" Then
                ddlApplicationfor.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlApplicationfor.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlApplicationfor.CssClass = "form-control"
                VApplicationfor = GV.parseString(ddlApplicationfor.SelectedValue)
            End If

            If GV.parseString(txtIfChangeInDateOfBirth.Text) = "" Then
                txtIfChangeInDateOfBirth.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtIfChangeInDateOfBirth.Focus()
                    isFocusApplied = True
                End If
            Else
                txtIfChangeInDateOfBirth.CssClass = "form-control"
                VIfChangeInDateOfBirth = GV.parseString(txtIfChangeInDateOfBirth.Text)
            End If

            If GV.parseString(txtIfChangeInNameOldFirstName.Text) = "" Then
                txtIfChangeInNameOldFirstName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtIfChangeInNameOldFirstName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtIfChangeInNameOldFirstName.CssClass = "form-control"
                VIfChangeInNameOldFirstName = GV.parseString(txtIfChangeInNameOldFirstName.Text)
            End If

            If GV.parseString(txtOldMidleName.Text) = "" Then
                txtOldMidleName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtOldMidleName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtOldMidleName.CssClass = "form-control"
                VOldMidleName = GV.parseString(txtOldMidleName.Text)
            End If

            If GV.parseString(txtOldLastName.Text) = "" Then
                txtOldLastName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtOldMidleName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtOldLastName.CssClass = "form-control"
                VOldLastName = GV.parseString(txtOldLastName.Text)
            End If

            If GV.parseString(txtNewFirstName.Text) = "" Then
                txtNewFirstName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNewFirstName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNewFirstName.CssClass = "form-control"
                VNewFirstName = GV.parseString(txtNewFirstName.Text)
            End If

            If GV.parseString(txtNewMidleName.Text) = "" Then
                txtNewMidleName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNewMidleName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNewMidleName.CssClass = "form-control"
                VNewMidleName = GV.parseString(txtNewMidleName.Text)
            End If

            If GV.parseString(txtNewLastName.Text) = "" Then
                txtNewLastName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNewLastName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNewLastName.CssClass = "form-control"
                VNewLastName = GV.parseString(txtNewLastName.Text)
            End If

            If GV.parseString(txtReason.Text) = "" Then
                txtReason.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtReason.Focus()
                    isFocusApplied = True
                End If
            Else
                txtReason.CssClass = "form-control"
                VReason = GV.parseString(txtReason.Text)
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

            '///////// end code by neetu - end 8 july 2022   ///////////////////'


            '///////////// POPUP coding /////////////////'

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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gazzet where RID=" & lblRID.Text.Trim & ""
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
            If ddlApplicantCategory.Items.Count > 0 Then
                If Not ddlApplicantCategory.SelectedValue.Trim = "" Then
                    VApplicantCategory = GV.parseString(ddlApplicantCategory.SelectedValue.Trim)
                Else
                    VApplicantCategory = ""
                End If
            End If

            If ddlApplicantTitle.Items.Count > 0 Then
                If Not ddlApplicantTitle.SelectedValue.Trim = "" Then
                    VApplicantTitle = GV.parseString(ddlApplicantTitle.SelectedValue.Trim)
                Else
                    VApplicantTitle = ""
                End If
            End If

            If Not txtApplicantFullName.Text.Trim = "" Then
                VApplicantFullName = GV.parseString(txtApplicantFullName.Text.Trim)
            Else
                VApplicantFullName = ""
            End If

            If Not txtApplicantDOB.Text.Trim = "" Then
                VApplicantDOB = GV.parseString(txtApplicantDOB.Text.Trim)
            Else
                VApplicantDOB = ""
            End If

            If Not txtAge.Text.Trim = "" Then
                VAge = GV.parseString(txtAge.Text.Trim)
            Else
                VAge = ""
            End If

            If ddlGender.Items.Count > 0 Then
                If Not ddlGender.SelectedValue.Trim = "" Then
                    VGender = GV.parseString(ddlGender.SelectedValue.Trim)
                Else
                    VGender = ""
                End If
            End If

            If Not txtMobileNo.Text.Trim = "" Then
                VMobileNo = GV.parseString(txtMobileNo.Text.Trim)
            Else
                VMobileNo = ""
            End If

            If Not txtEmail.Text.Trim = "" Then
                VEmail = GV.parseString(txtEmail.Text.Trim)
            Else
                VEmail = ""
            End If

            If Not txtAdhaarNo.Text.Trim = "" Then
                VAdhaarNo = GV.parseString(txtAdhaarNo.Text.Trim)
            Else
                VAdhaarNo = ""
            End If

            If Not txtAddress.Text.Trim = "" Then
                VAddress = GV.parseString(txtAddress.Text.Trim)
            Else
                VAddress = ""
            End If

            If ddlDistrict.Items.Count > 0 Then
                If Not ddlDistrict.SelectedValue.Trim = "" Then
                    VDistrict = GV.parseString(ddlDistrict.SelectedValue.Trim)
                Else
                    VDistrict = ""
                End If
            End If

            If Not txtTaluka.Text.Trim = "" Then
                VTaluka = GV.parseString(txtTaluka.Text.Trim)
            Else
                VTaluka = ""
            End If

            If Not txtVillage.Text.Trim = "" Then
                VVillage = GV.parseString(txtVillage.Text.Trim)
            Else
                VVillage = ""
            End If

            If Not txtPin.Text.Trim = "" Then
                VPin = GV.parseString(txtPin.Text.Trim)
            Else
                VPin = ""
            End If

            If ddlApplicationfor.Items.Count > 0 Then
                If Not ddlApplicationfor.SelectedValue.Trim = "" Then
                    VApplicationfor = GV.parseString(ddlApplicationfor.SelectedValue.Trim)
                Else
                    VApplicationfor = ""
                End If
            End If

            If Not txtIfChangeInDateOfBirth.Text.Trim = "" Then
                VIfChangeInDateOfBirth = GV.parseString(txtIfChangeInDateOfBirth.Text.Trim)
            Else
                VIfChangeInDateOfBirth = ""
            End If

            If Not txtIfChangeInNameOldFirstName.Text.Trim = "" Then
                VIfChangeInNameOldFirstName = GV.parseString(txtIfChangeInNameOldFirstName.Text.Trim)
            Else
                VIfChangeInNameOldFirstName = ""
            End If

            If Not txtOldMidleName.Text.Trim = "" Then
                VOldMidleName = GV.parseString(txtOldMidleName.Text.Trim)
            Else
                VOldMidleName = ""
            End If

            If Not txtOldLastName.Text.Trim = "" Then
                VOldLastName = GV.parseString(txtOldLastName.Text.Trim)
            Else
                VOldLastName = ""
            End If

            If Not txtNewFirstName.Text.Trim = "" Then
                VNewFirstName = GV.parseString(txtNewFirstName.Text.Trim)
            Else
                VNewFirstName = ""
            End If

            If Not txtNewMidleName.Text.Trim = "" Then
                VNewMidleName = GV.parseString(txtNewMidleName.Text.Trim)
            Else
                VNewMidleName = ""
            End If

            If Not txtNewLastName.Text.Trim = "" Then
                VNewLastName = GV.parseString(txtNewLastName.Text.Trim)
            Else
                VNewLastName = ""
            End If

            If Not txtReason.Text.Trim = "" Then
                VReason = GV.parseString(txtReason.Text.Trim)
            Else
                VReason = ""
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gazzet Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gazzet (RecordDateTime, EntryBy,ApplicantCategory,ApplicantTitle,ApplicantFullName,ApplicantDOB,Age,Gender,MobileNo,Email,AdhaarNo,Address,District,Taluka,Village,Pin,Applicationfor,IfChangeInDateOfBirth,IfChangeInNameOldFirstName,OldMidleName,OldLastName,NewFirstName,NewMidleName,NewLastName,Reason) values('" & VRecordDateTime & "','" & VEntryBy & "', '" & VApplicantCategory & "','" & VApplicantTitle & "','" & VApplicantFullName & "','" & VApplicantDOB & "','" & VAge & "','" & VGender & "','" & VMobileNo & "','" & VEmail & "','" & VAdhaarNo & "','" & VAddress & "','" & VDistrict & "','" & VTaluka & "','" & VVillage & "','" & VPin & "','" & VApplicationfor & "','" & VIfChangeInDateOfBirth & "','" & VIfChangeInNameOldFirstName & "','" & VOldMidleName & "','" & VOldLastName & "','" & VNewFirstName & "','" & VNewMidleName & "','" & VNewLastName & "','" & VReason & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gazzet set  UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', ApplicantCategory='" & VApplicantCategory & "', ApplicantTitle='" & VApplicantTitle & "', ApplicantFullName='" & VApplicantFullName & "', ApplicantDOB='" & VApplicantDOB & "', Age='" & VAge & "', Gender='" & VGender & "', MobileNo='" & VMobileNo & "', Email='" & VEmail & "', AdhaarNo='" & VAdhaarNo & "', Address='" & VAddress & "', District='" & VDistrict & "', Taluka='" & VTaluka & "', Village='" & VVillage & "', Pin='" & VPin & "', Applicationfor='" & VApplicationfor & "', IfChangeInDateOfBirth='" & VIfChangeInDateOfBirth & "', IfChangeInNameOldFirstName='" & VIfChangeInNameOldFirstName & "', OldMidleName='" & VOldMidleName & "', OldLastName='" & VOldLastName & "', NewFirstName='" & VNewFirstName & "', NewMidleName='" & VNewMidleName & "', NewLastName='" & VNewLastName & "', Reason='" & VReason & "' where RID=" & lblRID.Text.Trim & ""
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
            VApplicantCategory = ""
            VApplicantTitle = ""
            VApplicantFullName = ""
            VApplicantDOB = ""
            VAge = ""
            VGender = ""
            VMobileNo = ""
            VEmail = ""
            VAdhaarNo = ""
            VAddress = ""
            VDistrict = ""
            VTaluka = ""
            VVillage = ""
            VPin = ""
            VApplicationfor = ""
            VIfChangeInDateOfBirth = ""
            VIfChangeInNameOldFirstName = ""
            VOldMidleName = ""
            VOldLastName = ""
            VNewFirstName = ""
            VNewMidleName = ""
            VNewLastName = ""
            VReason = ""
            If ddlApplicantCategory.Items.Count > 0 Then
                ddlApplicantCategory.SelectedIndex = 0
            End If

            If ddlApplicantTitle.Items.Count > 0 Then
                ddlApplicantTitle.SelectedIndex = 0
            End If

            txtApplicantFullName.Text = ""

            txtApplicantDOB.Text = ""

            txtAge.Text = ""

            If ddlGender.Items.Count > 0 Then
                ddlGender.SelectedIndex = 0
            End If

            txtMobileNo.Text = ""

            txtEmail.Text = ""

            txtAdhaarNo.Text = ""

            txtAddress.Text = ""

            If ddlDistrict.Items.Count > 0 Then
                ddlDistrict.SelectedIndex = 0
            End If

            txtTaluka.Text = ""

            txtVillage.Text = ""

            txtPin.Text = ""

            If ddlApplicationfor.Items.Count > 0 Then
                ddlApplicationfor.SelectedIndex = 0
            End If

            txtIfChangeInDateOfBirth.Text = ""

            txtIfChangeInNameOldFirstName.Text = ""

            txtOldMidleName.Text = ""

            txtOldLastName.Text = ""

            txtNewFirstName.Text = ""

            txtNewMidleName.Text = ""

            txtNewLastName.Text = ""

            txtReason.Text = ""
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

                Session("EditFlag") = 0
                Session("Workfor") = "Save"


                GV.FL.AddInDropDownListDistinct(ddlDistrict, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlDistrict.SelectedValue = "NORTH DELHI"


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
                    formheading_1.Text = "Edit GAZZET FORM "
                    DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gazzet where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If ddlApplicantCategory.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantCategory")) Then
                                    If Not DS.Tables(0).Rows(0).Item("ApplicantCategory").ToString() = "" Then
                                        ddlApplicantCategory.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantCategory").ToString())
                                    Else
                                        ddlApplicantCategory.SelectedIndex = 0
                                    End If
                                Else
                                    ddlApplicantCategory.SelectedIndex = 0
                                End If
                            End If

                            If ddlApplicantTitle.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantTitle")) Then
                                    If Not DS.Tables(0).Rows(0).Item("ApplicantTitle").ToString() = "" Then
                                        ddlApplicantTitle.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantTitle").ToString())
                                    Else
                                        ddlApplicantTitle.SelectedIndex = 0
                                    End If
                                Else
                                    ddlApplicantTitle.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantFullName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantFullName").ToString() = "" Then
                                    txtApplicantFullName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantFullName").ToString())
                                Else
                                    txtApplicantFullName.Text = ""
                                End If
                            Else
                                txtApplicantFullName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantDOB")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantDOB").ToString() = "" Then
                                    txtApplicantDOB.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantDOB").ToString())
                                Else
                                    txtApplicantDOB.Text = ""
                                End If
                            Else
                                txtApplicantDOB.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Age")) Then
                                If Not DS.Tables(0).Rows(0).Item("Age").ToString() = "" Then
                                    txtAge.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Age").ToString())
                                Else
                                    txtAge.Text = ""
                                End If
                            Else
                                txtAge.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("MobileNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                                    txtMobileNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MobileNo").ToString())
                                Else
                                    txtMobileNo.Text = ""
                                End If
                            Else
                                txtMobileNo.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AdhaarNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("AdhaarNo").ToString() = "" Then
                                    txtAdhaarNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AdhaarNo").ToString())
                                Else
                                    txtAdhaarNo.Text = ""
                                End If
                            Else
                                txtAdhaarNo.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Address")) Then
                                If Not DS.Tables(0).Rows(0).Item("Address").ToString() = "" Then
                                    txtAddress.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Address").ToString())
                                Else
                                    txtAddress.Text = ""
                                End If
                            Else
                                txtAddress.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Taluka")) Then
                                If Not DS.Tables(0).Rows(0).Item("Taluka").ToString() = "" Then
                                    txtTaluka.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Taluka").ToString())
                                Else
                                    txtTaluka.Text = ""
                                End If
                            Else
                                txtTaluka.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Pin")) Then
                                If Not DS.Tables(0).Rows(0).Item("Pin").ToString() = "" Then
                                    txtPin.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Pin").ToString())
                                Else
                                    txtPin.Text = ""
                                End If
                            Else
                                txtPin.Text = ""
                            End If

                            If ddlApplicationfor.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Applicationfor")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Applicationfor").ToString() = "" Then
                                        ddlApplicationfor.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Applicationfor").ToString())
                                    Else
                                        ddlApplicationfor.SelectedIndex = 0
                                    End If
                                Else
                                    ddlApplicationfor.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("IfChangeInDateOfBirth")) Then
                                If Not DS.Tables(0).Rows(0).Item("IfChangeInDateOfBirth").ToString() = "" Then
                                    txtIfChangeInDateOfBirth.Text = GV.parseString(DS.Tables(0).Rows(0).Item("IfChangeInDateOfBirth").ToString())
                                Else
                                    txtIfChangeInDateOfBirth.Text = ""
                                End If
                            Else
                                txtIfChangeInDateOfBirth.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("IfChangeInNameOldFirstName")) Then
                                If Not DS.Tables(0).Rows(0).Item("IfChangeInNameOldFirstName").ToString() = "" Then
                                    txtIfChangeInNameOldFirstName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("IfChangeInNameOldFirstName").ToString())
                                Else
                                    txtIfChangeInNameOldFirstName.Text = ""
                                End If
                            Else
                                txtIfChangeInNameOldFirstName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("OldMidleName")) Then
                                If Not DS.Tables(0).Rows(0).Item("OldMidleName").ToString() = "" Then
                                    txtOldMidleName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("OldMidleName").ToString())
                                Else
                                    txtOldMidleName.Text = ""
                                End If
                            Else
                                txtOldMidleName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("OldLastName")) Then
                                If Not DS.Tables(0).Rows(0).Item("OldLastName").ToString() = "" Then
                                    txtOldLastName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("OldLastName").ToString())
                                Else
                                    txtOldLastName.Text = ""
                                End If
                            Else
                                txtOldLastName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NewFirstName")) Then
                                If Not DS.Tables(0).Rows(0).Item("NewFirstName").ToString() = "" Then
                                    txtNewFirstName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NewFirstName").ToString())
                                Else
                                    txtNewFirstName.Text = ""
                                End If
                            Else
                                txtNewFirstName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NewMidleName")) Then
                                If Not DS.Tables(0).Rows(0).Item("NewMidleName").ToString() = "" Then
                                    txtNewMidleName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NewMidleName").ToString())
                                Else
                                    txtNewMidleName.Text = ""
                                End If
                            Else
                                txtNewMidleName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NewLastName")) Then
                                If Not DS.Tables(0).Rows(0).Item("NewLastName").ToString() = "" Then
                                    txtNewLastName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NewLastName").ToString())
                                Else
                                    txtNewLastName.Text = ""
                                End If
                            Else
                                txtNewLastName.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Reason")) Then
                                If Not DS.Tables(0).Rows(0).Item("Reason").ToString() = "" Then
                                    txtReason.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Reason").ToString())
                                Else
                                    txtReason.Text = ""
                                End If
                            Else
                                txtReason.Text = ""
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