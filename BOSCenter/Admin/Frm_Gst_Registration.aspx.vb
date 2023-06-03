Public Class Frm_Gst_Registration
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim isErrorFound As Boolean = False
    Dim isFocusApplied As Boolean = False
    Dim v_Error_String As String = ""
    Dim VLastNameofApplicant, VFirstNameofApplicant, VMiddleNameofApplicant, VNameOfEnterprise, VMobileNumber, VEmail, VTypeOfOrganisation, VNATUREOFBUISNESS, VPANNumber, VAddressofENTERPRISES, VLocalityofENTERPRISES, VStateofENTERPRISES, VDistrictofENTERPRISES, VEnterTalukaAndVillageofENTERPRISES, VPin, VBankIFSC, VBankAccNo, VEmployee_Worker As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet



    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("NameTheForm.aspx") '/Change the name of form
            Else
                txtLastNameofApplicant.CssClass = "form-control"
                txtFirstNameofApplicant.CssClass = "form-control"
                txtMiddleNameofApplicant.CssClass = "form-control"
                txtNameOfEnterprise.CssClass = "form-control"
                txtMobileNumber.CssClass = "form-control"
                txtEmail.CssClass = "form-control"
                ddlTypeOfOrganisation.CssClass = "form-control"
                txtNATUREOFBUISNESS.CssClass = "form-control"
                txtPANNumber.CssClass = "form-control"
                txtAddressofENTERPRISES.CssClass = "form-control"
                txtLocalityofENTERPRISES.CssClass = "form-control"
                ddlStateofENTERPRISES.CssClass = "form-control"
                ddlDistrictofENTERPRISES.CssClass = "form-control"
                txtEnterTalukaAndVillageofENTERPRISES.CssClass = "form-control"
                txtPin.CssClass = "form-control"
                txtBankIFSC.CssClass = "form-control"
                txtBankAccNo.CssClass = "form-control"
                txtEmployee_Worker.CssClass = "form-control"

                VLastNameofApplicant = ""
                VFirstNameofApplicant = ""
                VMiddleNameofApplicant = ""
                VNameOfEnterprise = ""
                VMobileNumber = ""
                VEmail = ""
                VTypeOfOrganisation = ""
                VNATUREOFBUISNESS = ""
                VPANNumber = ""
                VAddressofENTERPRISES = ""
                VLocalityofENTERPRISES = ""
                VStateofENTERPRISES = ""
                VDistrictofENTERPRISES = ""
                VEnterTalukaAndVillageofENTERPRISES = ""
                VPin = ""
                VBankIFSC = ""
                VBankAccNo = ""
                VEmployee_Worker = ""
                lblError.Text = ""
                lblError.CssClass = ""
                txtLastNameofApplicant.Text = ""

                txtFirstNameofApplicant.Text = ""

                txtMiddleNameofApplicant.Text = ""

                txtNameOfEnterprise.Text = ""

                txtMobileNumber.Text = ""

                txtEmail.Text = ""

                If ddlTypeOfOrganisation.Items.Count > 0 Then
                    ddlTypeOfOrganisation.SelectedIndex = 0
                End If

                txtNATUREOFBUISNESS.Text = ""

                txtPANNumber.Text = ""

                txtAddressofENTERPRISES.Text = ""

                txtLocalityofENTERPRISES.Text = ""

                If ddlStateofENTERPRISES.Items.Count > 0 Then
                    ddlStateofENTERPRISES.SelectedIndex = 0
                End If

                If ddlDistrictofENTERPRISES.Items.Count > 0 Then
                    ddlDistrictofENTERPRISES.SelectedIndex = 0
                End If

                txtEnterTalukaAndVillageofENTERPRISES.Text = ""

                txtPin.Text = ""

                txtBankIFSC.Text = ""

                txtBankAccNo.Text = ""

                txtEmployee_Worker.Text = ""
                ddlStateofENTERPRISES.SelectedValue = "DELHI"
                ddlDistrictofENTERPRISES.SelectedValue = "NORTH DELHI"
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

            lblError.Text = ""
            lblError.CssClass = ""

            '///////// start code by neetu - start 8 july 2022 '///////////////


            If GV.parseString(txtLastNameofApplicant.Text) = "" Then
                txtLastNameofApplicant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtLastNameofApplicant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtLastNameofApplicant.CssClass = "form-control"
                VLastNameofApplicant = GV.parseString(txtLastNameofApplicant.Text)
            End If

            If GV.parseString(txtFirstNameofApplicant.Text) = "" Then
                txtFirstNameofApplicant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtFirstNameofApplicant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtFirstNameofApplicant.CssClass = "form-control"
                VFirstNameofApplicant = GV.parseString(txtFirstNameofApplicant.Text)
            End If

            If GV.parseString(txtMiddleNameofApplicant.Text) = "" Then
                txtMiddleNameofApplicant.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtMiddleNameofApplicant.Focus()
                    isFocusApplied = True
                End If
            Else
                txtMiddleNameofApplicant.CssClass = "form-control"
                VMiddleNameofApplicant = GV.parseString(txtMiddleNameofApplicant.Text)
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
                    v_Error_String = "Please Enter Correct Phone No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Phone No."
                End If


                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtMobileNumber.Text).Length = 10 Then
                txtMobileNumber.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 10 Digit Phone No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 10 Digit Phone No."
                End If
                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtMobileNumber.CssClass = "Form-control"
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

            If GV.parseString(ddlTypeOfOrganisation.SelectedValue) = "" Then
                ddlTypeOfOrganisation.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlTypeOfOrganisation.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlTypeOfOrganisation.CssClass = "form-control"
                VTypeOfOrganisation = GV.parseString(ddlTypeOfOrganisation.SelectedValue)
            End If

            If GV.parseString(txtNATUREOFBUISNESS.Text) = "" Then
                txtNATUREOFBUISNESS.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNATUREOFBUISNESS.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNATUREOFBUISNESS.CssClass = "form-control"
                VNATUREOFBUISNESS = GV.parseString(txtNATUREOFBUISNESS.Text)
            End If

            If GV.parseString(txtPANNumber.Text) = "" Then
                txtPANNumber.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtPANNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtPANNumber.Text) Then
                txtPANNumber.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Pan No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Pan No."
                End If


                If isFocusApplied = False Then
                    txtPANNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtPANNumber.Text).Length = 6 Then
                txtPANNumber.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 6 Digit Pin No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 10 Digit Pin No."
                End If
                If isFocusApplied = False Then
                    txtPANNumber.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPANNumber.CssClass = "Form-control"
                VPANNumber = GV.parseString(txtPANNumber.Text)
            End If

            If GV.parseString(txtAddressofENTERPRISES.Text) = "" Then
                txtAddressofENTERPRISES.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtAddressofENTERPRISES.Focus()
                    isFocusApplied = True
                End If
            Else
                txtAddressofENTERPRISES.CssClass = "form-control"
                VAddressofENTERPRISES = GV.parseString(txtAddressofENTERPRISES.Text)
            End If


            If GV.parseString(txtLocalityofENTERPRISES.Text) = "" Then
                txtLocalityofENTERPRISES.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtLocalityofENTERPRISES.Focus()
                    isFocusApplied = True
                End If
            Else
                txtLocalityofENTERPRISES.CssClass = "form-control"
                VLocalityofENTERPRISES = GV.parseString(txtLocalityofENTERPRISES.Text)
            End If

            If GV.parseString(ddlStateofENTERPRISES.SelectedValue) = "" Then
                ddlStateofENTERPRISES.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStateofENTERPRISES.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStateofENTERPRISES.CssClass = "form-control"
                VStateofENTERPRISES = GV.parseString(ddlStateofENTERPRISES.SelectedValue)
            End If

            If GV.parseString(ddlDistrictofENTERPRISES.SelectedValue) = "" Then
                ddlDistrictofENTERPRISES.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlDistrictofENTERPRISES.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlDistrictofENTERPRISES.CssClass = "form-control"
                VDistrictofENTERPRISES = GV.parseString(ddlDistrictofENTERPRISES.SelectedValue)
            End If


            If GV.parseString(txtEnterTalukaAndVillageofENTERPRISES.Text) = "" Then
                txtEnterTalukaAndVillageofENTERPRISES.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEnterTalukaAndVillageofENTERPRISES.Focus()
                    isFocusApplied = True
                End If
            Else
                txtEnterTalukaAndVillageofENTERPRISES.CssClass = "form-control"
                VEnterTalukaAndVillageofENTERPRISES = GV.parseString(txtEnterTalukaAndVillageofENTERPRISES.Text)
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

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Bank Acc No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Bank Acc No."
                End If
                If isFocusApplied = False Then
                    txtBankAccNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtBankAccNo.CssClass = "Form-control"
                VBankAccNo = GV.parseString(txtBankAccNo.Text)
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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gst_Registration where RID=" & lblRID.Text.Trim & ""
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
            If Not txtLastNameofApplicant.Text.Trim = "" Then
                VLastNameofApplicant = GV.parseString(txtLastNameofApplicant.Text.Trim)
            Else
                VLastNameofApplicant = ""
            End If

            If Not txtFirstNameofApplicant.Text.Trim = "" Then
                VFirstNameofApplicant = GV.parseString(txtFirstNameofApplicant.Text.Trim)
            Else
                VFirstNameofApplicant = ""
            End If

            If Not txtMiddleNameofApplicant.Text.Trim = "" Then
                VMiddleNameofApplicant = GV.parseString(txtMiddleNameofApplicant.Text.Trim)
            Else
                VMiddleNameofApplicant = ""
            End If

            If Not txtNameOfEnterprise.Text.Trim = "" Then
                VNameOfEnterprise = GV.parseString(txtNameOfEnterprise.Text.Trim)
            Else
                VNameOfEnterprise = ""
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

            If ddlTypeOfOrganisation.Items.Count > 0 Then
                If Not ddlTypeOfOrganisation.SelectedValue.Trim = "" Then
                    VTypeOfOrganisation = GV.parseString(ddlTypeOfOrganisation.SelectedValue.Trim)
                Else
                    VTypeOfOrganisation = ""
                End If
            End If

            If Not txtNATUREOFBUISNESS.Text.Trim = "" Then
                VNATUREOFBUISNESS = GV.parseString(txtNATUREOFBUISNESS.Text.Trim)
            Else
                VNATUREOFBUISNESS = ""
            End If

            If Not txtPANNumber.Text.Trim = "" Then
                VPANNumber = GV.parseString(txtPANNumber.Text.Trim)
            Else
                VPANNumber = ""
            End If

            If Not txtAddressofENTERPRISES.Text.Trim = "" Then
                VAddressofENTERPRISES = GV.parseString(txtAddressofENTERPRISES.Text.Trim)
            Else
                VAddressofENTERPRISES = ""
            End If

            If Not txtLocalityofENTERPRISES.Text.Trim = "" Then
                VLocalityofENTERPRISES = GV.parseString(txtLocalityofENTERPRISES.Text.Trim)
            Else
                VLocalityofENTERPRISES = ""
            End If

            If ddlStateofENTERPRISES.Items.Count > 0 Then
                If Not ddlStateofENTERPRISES.SelectedValue.Trim = "" Then
                    VStateofENTERPRISES = GV.parseString(ddlStateofENTERPRISES.SelectedValue.Trim)
                Else
                    VStateofENTERPRISES = ""
                End If
            End If

            If ddlDistrictofENTERPRISES.Items.Count > 0 Then
                If Not ddlDistrictofENTERPRISES.SelectedValue.Trim = "" Then
                    VDistrictofENTERPRISES = GV.parseString(ddlDistrictofENTERPRISES.SelectedValue.Trim)
                Else
                    VDistrictofENTERPRISES = ""
                End If
            End If

            If Not txtEnterTalukaAndVillageofENTERPRISES.Text.Trim = "" Then
                VEnterTalukaAndVillageofENTERPRISES = GV.parseString(txtEnterTalukaAndVillageofENTERPRISES.Text.Trim)
            Else
                VEnterTalukaAndVillageofENTERPRISES = ""
            End If

            If Not txtPin.Text.Trim = "" Then
                VPin = GV.parseString(txtPin.Text.Trim)
            Else
                VPin = ""
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

            If Not txtEmployee_Worker.Text.Trim = "" Then
                VEmployee_Worker = GV.parseString(txtEmployee_Worker.Text.Trim)
            Else
                VEmployee_Worker = ""
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gst_Registration Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gst_Registration (RecordDateTime, EntryBy,LastNameofApplicant,FirstNameofApplicant,MiddleNameofApplicant,NameOfEnterprise,MobileNumber,Email,TypeOfOrganisation,NATUREOFBUISNESS,PANNumber,AddressofENTERPRISES,LocalityofENTERPRISES,StateofENTERPRISES,DistrictofENTERPRISES,EnterTalukaAndVillageofENTERPRISES,Pin,BankIFSC,BankAccNo,Employee_Worker) values( '" & VRecordDateTime & "','" & VEntryBy & "', '" & VLastNameofApplicant & "','" & VFirstNameofApplicant & "','" & VMiddleNameofApplicant & "','" & VNameOfEnterprise & "','" & VMobileNumber & "','" & VEmail & "','" & VTypeOfOrganisation & "','" & VNATUREOFBUISNESS & "','" & VPANNumber & "','" & VAddressofENTERPRISES & "','" & VLocalityofENTERPRISES & "','" & VStateofENTERPRISES & "','" & VDistrictofENTERPRISES & "','" & VEnterTalukaAndVillageofENTERPRISES & "','" & VPin & "','" & VBankIFSC & "','" & VBankAccNo & "','" & VEmployee_Worker & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gst_Registration set  UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', LastNameofApplicant='" & VLastNameofApplicant & "', FirstNameofApplicant='" & VFirstNameofApplicant & "', MiddleNameofApplicant='" & VMiddleNameofApplicant & "', NameOfEnterprise='" & VNameOfEnterprise & "', MobileNumber='" & VMobileNumber & "', Email='" & VEmail & "', TypeOfOrganisation='" & VTypeOfOrganisation & "', NATUREOFBUISNESS='" & VNATUREOFBUISNESS & "', PANNumber='" & VPANNumber & "', AddressofENTERPRISES='" & VAddressofENTERPRISES & "', LocalityofENTERPRISES='" & VLocalityofENTERPRISES & "', StateofENTERPRISES='" & VStateofENTERPRISES & "', DistrictofENTERPRISES='" & VDistrictofENTERPRISES & "', EnterTalukaAndVillageofENTERPRISES='" & VEnterTalukaAndVillageofENTERPRISES & "', Pin='" & VPin & "', BankIFSC='" & VBankIFSC & "', BankAccNo='" & VBankAccNo & "', Employee_Worker='" & VEmployee_Worker & "' where RID=" & lblRID.Text.Trim & ""
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
            VLastNameofApplicant = ""
            VFirstNameofApplicant = ""
            VMiddleNameofApplicant = ""
            VNameOfEnterprise = ""
            VMobileNumber = ""
            VEmail = ""
            VTypeOfOrganisation = ""
            VNATUREOFBUISNESS = ""
            VPANNumber = ""
            VAddressofENTERPRISES = ""
            VLocalityofENTERPRISES = ""
            VStateofENTERPRISES = ""
            VDistrictofENTERPRISES = ""
            VEnterTalukaAndVillageofENTERPRISES = ""
            VPin = ""
            VBankIFSC = ""
            VBankAccNo = ""
            VEmployee_Worker = ""
            txtLastNameofApplicant.Text = ""

            txtFirstNameofApplicant.Text = ""

            txtMiddleNameofApplicant.Text = ""

            txtNameOfEnterprise.Text = ""

            txtMobileNumber.Text = ""

            txtEmail.Text = ""

            If ddlTypeOfOrganisation.Items.Count > 0 Then
                ddlTypeOfOrganisation.SelectedIndex = 0
            End If

            txtNATUREOFBUISNESS.Text = ""

            txtPANNumber.Text = ""

            txtAddressofENTERPRISES.Text = ""

            txtLocalityofENTERPRISES.Text = ""

            If ddlStateofENTERPRISES.Items.Count > 0 Then
                ddlStateofENTERPRISES.SelectedIndex = 0
            End If

            If ddlDistrictofENTERPRISES.Items.Count > 0 Then
                ddlDistrictofENTERPRISES.SelectedIndex = 0
            End If

            txtEnterTalukaAndVillageofENTERPRISES.Text = ""

            txtPin.Text = ""

            txtBankIFSC.Text = ""

            txtBankAccNo.Text = ""

            txtEmployee_Worker.Text = ""
            ddlStateofENTERPRISES.SelectedValue = "DELHI"
            ddlDistrictofENTERPRISES.SelectedValue = "NORTH DELHI"
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


                GV.FL.AddInDropDownListDistinct(ddlStateofENTERPRISES, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrictofENTERPRISES, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlStateofENTERPRISES.SelectedValue = "DELHI"
                ddlDistrictofENTERPRISES.SelectedValue = "NORTH DELHI"

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
                    formheading_1.Text = "Edit GST REGISTRATION "
                    DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Gst_Registration where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("LastNameofApplicant")) Then
                                If Not DS.Tables(0).Rows(0).Item("LastNameofApplicant").ToString() = "" Then
                                    txtLastNameofApplicant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("LastNameofApplicant").ToString())
                                Else
                                    txtLastNameofApplicant.Text = ""
                                End If
                            Else
                                txtLastNameofApplicant.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("FirstNameofApplicant")) Then
                                If Not DS.Tables(0).Rows(0).Item("FirstNameofApplicant").ToString() = "" Then
                                    txtFirstNameofApplicant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("FirstNameofApplicant").ToString())
                                Else
                                    txtFirstNameofApplicant.Text = ""
                                End If
                            Else
                                txtFirstNameofApplicant.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("MiddleNameofApplicant")) Then
                                If Not DS.Tables(0).Rows(0).Item("MiddleNameofApplicant").ToString() = "" Then
                                    txtMiddleNameofApplicant.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MiddleNameofApplicant").ToString())
                                Else
                                    txtMiddleNameofApplicant.Text = ""
                                End If
                            Else
                                txtMiddleNameofApplicant.Text = ""
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

                            If ddlTypeOfOrganisation.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("TypeOfOrganisation")) Then
                                    If Not DS.Tables(0).Rows(0).Item("TypeOfOrganisation").ToString() = "" Then
                                        ddlTypeOfOrganisation.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("TypeOfOrganisation").ToString())
                                    Else
                                        ddlTypeOfOrganisation.SelectedIndex = 0
                                    End If
                                Else
                                    ddlTypeOfOrganisation.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NATUREOFBUISNESS")) Then
                                If Not DS.Tables(0).Rows(0).Item("NATUREOFBUISNESS").ToString() = "" Then
                                    txtNATUREOFBUISNESS.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NATUREOFBUISNESS").ToString())
                                Else
                                    txtNATUREOFBUISNESS.Text = ""
                                End If
                            Else
                                txtNATUREOFBUISNESS.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AddressofENTERPRISES")) Then
                                If Not DS.Tables(0).Rows(0).Item("AddressofENTERPRISES").ToString() = "" Then
                                    txtAddressofENTERPRISES.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AddressofENTERPRISES").ToString())
                                Else
                                    txtAddressofENTERPRISES.Text = ""
                                End If
                            Else
                                txtAddressofENTERPRISES.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("LocalityofENTERPRISES")) Then
                                If Not DS.Tables(0).Rows(0).Item("LocalityofENTERPRISES").ToString() = "" Then
                                    txtLocalityofENTERPRISES.Text = GV.parseString(DS.Tables(0).Rows(0).Item("LocalityofENTERPRISES").ToString())
                                Else
                                    txtLocalityofENTERPRISES.Text = ""
                                End If
                            Else
                                txtLocalityofENTERPRISES.Text = ""
                            End If

                            If ddlStateofENTERPRISES.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("StateofENTERPRISES")) Then
                                    If Not DS.Tables(0).Rows(0).Item("StateofENTERPRISES").ToString() = "" Then
                                        ddlStateofENTERPRISES.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("StateofENTERPRISES").ToString())
                                    Else
                                        ddlStateofENTERPRISES.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStateofENTERPRISES.SelectedIndex = 0
                                End If
                            End If

                            If ddlDistrictofENTERPRISES.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("DistrictofENTERPRISES")) Then
                                    If Not DS.Tables(0).Rows(0).Item("DistrictofENTERPRISES").ToString() = "" Then
                                        ddlDistrictofENTERPRISES.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("DistrictofENTERPRISES").ToString())
                                    Else
                                        ddlDistrictofENTERPRISES.SelectedIndex = 0
                                    End If
                                Else
                                    ddlDistrictofENTERPRISES.SelectedIndex = 0
                                End If
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("EnterTalukaAndVillageofENTERPRISES")) Then
                                If Not DS.Tables(0).Rows(0).Item("EnterTalukaAndVillageofENTERPRISES").ToString() = "" Then
                                    txtEnterTalukaAndVillageofENTERPRISES.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EnterTalukaAndVillageofENTERPRISES").ToString())
                                Else
                                    txtEnterTalukaAndVillageofENTERPRISES.Text = ""
                                End If
                            Else
                                txtEnterTalukaAndVillageofENTERPRISES.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Employee_Worker")) Then
                                If Not DS.Tables(0).Rows(0).Item("Employee_Worker").ToString() = "" Then
                                    txtEmployee_Worker.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Employee_Worker").ToString())
                                Else
                                    txtEmployee_Worker.Text = ""
                                End If
                            Else
                                txtEmployee_Worker.Text = ""
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