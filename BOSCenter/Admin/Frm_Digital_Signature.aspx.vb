Public Class Frm_Digital_Signature
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VApplicantLastName, VApplicantFirstName, VApplicantMiddleName, VAdhaarNo, VPanNo, VMobileNumber, VEmail, VDOB, VTypeofsignature, VStates, VDistrict, VTaluka, VVillage, VLocation, VStreet, VBuildingName, VHouseNo_Building_Landmark As String
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
                txtApplicantLastName.CssClass = "form-control"
                txtApplicantFirstName.CssClass = "form-control"
                txtApplicantMiddleName.CssClass = "form-control"
                txtAdhaarNo.CssClass = "form-control"
                txtPanNo.CssClass = "form-control"
                txtMobileNumber.CssClass = "form-control"
                txtEmail.CssClass = "form-control"
                txtDOB.CssClass = "form-control"
                ddlTypeofsignature.CssClass = "form-control"
                ddlStates.CssClass = "form-control"
                ddlDistrict.CssClass = "form-control"
                txtTaluka.CssClass = "form-control"
                txtVillage.CssClass = "form-control"
                txtLocation.CssClass = "form-control"
                txtStreet.CssClass = "form-control"
                txtBuildingName.CssClass = "form-control"
                txtHouseNo_Building_Landmark.CssClass = "form-control"

                VApplicantLastName = ""
                VApplicantFirstName = ""
                VApplicantMiddleName = ""
                VAdhaarNo = ""
                VPanNo = ""
                VMobileNumber = ""
                VEmail = ""
                VDOB = ""
                VTypeofsignature = ""
                VStates = ""
                VDistrict = ""
                VTaluka = ""
                VVillage = ""
                VLocation = ""
                VStreet = ""
                VBuildingName = ""
                VHouseNo_Building_Landmark = ""
                lblError.Text = ""
                lblError.CssClass = ""
                txtApplicantLastName.Text = ""

                txtApplicantFirstName.Text = ""

                txtApplicantMiddleName.Text = ""

                txtAdhaarNo.Text = ""

                txtPanNo.Text = ""

                txtMobileNumber.Text = ""

                txtEmail.Text = ""

                txtDOB.Text = ""

                If ddlTypeofsignature.Items.Count > 0 Then
                    ddlTypeofsignature.SelectedIndex = 0
                End If

                If ddlStates.Items.Count > 0 Then
                    ddlStates.SelectedIndex = 0
                End If

                If ddlDistrict.Items.Count > 0 Then
                    ddlDistrict.SelectedIndex = 0
                End If

                txtTaluka.Text = ""

                txtVillage.Text = ""

                txtLocation.Text = ""

                txtStreet.Text = ""

                txtBuildingName.Text = ""

                txtHouseNo_Building_Landmark.Text = ""

                Session("EditFlag") = 0
                btnSave.Text = "Save"
                btnClear.Text = "Reset"
                lblError.Text = ""

                ddlStates.SelectedValue = "DELHI"
                ddlDistrict.SelectedValue = "NORTH DELHI"

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
            lblError.Visible = False
            '//// code by Nidhi -  start 8 july 2022

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
                txtAdhaarNo.CssClass = "form-control"
                VAdhaarNo = GV.parseString(txtAdhaarNo.Text)
            End If

            If GV.parseString(txtPanNo.Text) = "" Then
                txtPanNo.CssClass = "ValidationError"
                isErrorFound = True

                If isFocusApplied = False Then
                    txtPanNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtPanNo.Text).Length = 10 Then
                txtPanNo.CssClass = "ValidationError"
                isErrorFound = True

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter 10 Digit Pan No."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter 10 Digit Pan No."
                End If

                If isFocusApplied = False Then
                    txtPanNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtPanNo.CssClass = "form-control"
                VPanNo = GV.parseString(txtPanNo.Text)
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

                If v_Error_String.Trim = "" Then
                    v_Error_String = "Please Enter Correct Date Format."
                Else
                    v_Error_String = v_Error_String & "<br>" & "Please Enter Correct Date Format."
                End If

                If isFocusApplied = False Then
                    txtDOB.Focus()
                    isFocusApplied = True
                End If
            Else

                txtDOB.CssClass = "form-control"
                VDOB = GV.FL.returnDateMonthWiseWithDateChecking(txtDOB.Text.Trim)

            End If

            If GV.parseString(ddlTypeofsignature.SelectedValue) = "" Then
                ddlTypeofsignature.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlTypeofsignature.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlTypeofsignature.CssClass = "form-control"
                VTypeofsignature = GV.parseString(ddlTypeofsignature.SelectedValue)
            End If

            If GV.parseString(ddlStates.SelectedValue) = "" Then
                ddlStates.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlStates.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlStates.CssClass = "form-control"
                VStates = GV.parseString(ddlStates.SelectedValue)
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

            If GV.parseString(txtLocation.Text) = "" Then
                txtLocation.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtLocation.Focus()
                    isFocusApplied = True
                End If
            Else
                txtLocation.CssClass = "form-control"
                VLocation = GV.parseString(txtLocation.Text)
            End If

            If GV.parseString(txtStreet.Text) = "" Then
                txtStreet.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtStreet.Focus()
                    isFocusApplied = True
                End If
            Else
                txtStreet.CssClass = "form-control"
                VStreet = GV.parseString(txtStreet.Text)
            End If

            If GV.parseString(txtBuildingName.Text) = "" Then
                txtBuildingName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtBuildingName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtBuildingName.CssClass = "form-control"
                VBuildingName = GV.parseString(txtBuildingName.Text)
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

            '///// End - Code by Nidhi


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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Digital_signature where RID=" & lblRID.Text.Trim & ""
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
            If Not txtApplicantLastName.Text.Trim = "" Then
                VApplicantLastName = GV.parseString(txtApplicantLastName.Text.Trim)
            Else
                VApplicantLastName = ""
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

            If Not txtAdhaarNo.Text.Trim = "" Then
                VAdhaarNo = GV.parseString(txtAdhaarNo.Text.Trim)
            Else
                VAdhaarNo = ""
            End If

            If Not txtPanNo.Text.Trim = "" Then
                VPanNo = GV.parseString(txtPanNo.Text.Trim)
            Else
                VPanNo = ""
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

            If Not txtDOB.Text.Trim = "" Then
                VDOB = GV.parseString(txtDOB.Text.Trim)
            Else
                VDOB = ""
            End If

            If ddlTypeofsignature.Items.Count > 0 Then
                If Not ddlTypeofsignature.SelectedValue.Trim = "" Then
                    VTypeofsignature = GV.parseString(ddlTypeofsignature.SelectedValue.Trim)
                Else
                    VTypeofsignature = ""
                End If
            End If

            If ddlStates.Items.Count > 0 Then
                If Not ddlStates.SelectedValue.Trim = "" Then
                    VStates = GV.parseString(ddlStates.SelectedValue.Trim)
                Else
                    VStates = ""
                End If
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

            If Not txtLocation.Text.Trim = "" Then
                VLocation = GV.parseString(txtLocation.Text.Trim)
            Else
                VLocation = ""
            End If

            If Not txtStreet.Text.Trim = "" Then
                VStreet = GV.parseString(txtStreet.Text.Trim)
            Else
                VStreet = ""
            End If

            If Not txtBuildingName.Text.Trim = "" Then
                VBuildingName = GV.parseString(txtBuildingName.Text.Trim)
            Else
                VBuildingName = ""
            End If

            If Not txtHouseNo_Building_Landmark.Text.Trim = "" Then
                VHouseNo_Building_Landmark = GV.parseString(txtHouseNo_Building_Landmark.Text.Trim)
            Else
                VHouseNo_Building_Landmark = ""
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

                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Digital_signature Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Digital_signature (RecordDateTime, EntryBy,ApplicantLastName,ApplicantFirstName,ApplicantMiddleName,AdhaarNo,PanNo,MobileNumber,Email,DOB,Typeofsignature,States,District,Taluka,Village,Location,Street,BuildingName,HouseNo_Building_Landmark) values('" & VRecordDateTime & "','" & VEntryBy & "', '" & VApplicantLastName & "','" & VApplicantFirstName & "','" & VApplicantMiddleName & "','" & VAdhaarNo & "','" & VPanNo & "','" & VMobileNumber & "','" & VEmail & "','" & VDOB & "','" & VTypeofsignature & "','" & VStates & "','" & VDistrict & "','" & VTaluka & "','" & VVillage & "','" & VLocation & "','" & VStreet & "','" & VBuildingName & "','" & VHouseNo_Building_Landmark & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Digital_signature set UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "',ApplicantLastName='" & VApplicantLastName & "', ApplicantFirstName='" & VApplicantFirstName & "', ApplicantMiddleName='" & VApplicantMiddleName & "', AdhaarNo='" & VAdhaarNo & "', PanNo='" & VPanNo & "', MobileNumber='" & VMobileNumber & "', Email='" & VEmail & "', DOB='" & VDOB & "', Typeofsignature='" & VTypeofsignature & "', States='" & VStates & "', District='" & VDistrict & "', Taluka='" & VTaluka & "', Village='" & VVillage & "', Location='" & VLocation & "', Street='" & VStreet & "', BuildingName='" & VBuildingName & "', HouseNo_Building_Landmark='" & VHouseNo_Building_Landmark & "' where RID=" & lblRID.Text.Trim & ""
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
            VApplicantLastName = ""
            VApplicantFirstName = ""
            VApplicantMiddleName = ""
            VAdhaarNo = ""
            VPanNo = ""
            VMobileNumber = ""
            VEmail = ""
            VDOB = ""
            VTypeofsignature = ""
            VStates = ""
            VDistrict = ""
            VTaluka = ""
            VVillage = ""
            VLocation = ""
            VStreet = ""
            VBuildingName = ""
            VHouseNo_Building_Landmark = ""
            txtApplicantLastName.Text = ""

            txtApplicantFirstName.Text = ""

            txtApplicantMiddleName.Text = ""

            txtAdhaarNo.Text = ""

            txtPanNo.Text = ""

            txtMobileNumber.Text = ""

            txtEmail.Text = ""

            txtDOB.Text = ""

            If ddlTypeofsignature.Items.Count > 0 Then
                ddlTypeofsignature.SelectedIndex = 0
            End If

            If ddlStates.Items.Count > 0 Then
                ddlStates.SelectedIndex = 0
            End If

            If ddlDistrict.Items.Count > 0 Then
                ddlDistrict.SelectedIndex = 0
            End If

            txtTaluka.Text = ""

            txtVillage.Text = ""

            txtLocation.Text = ""

            txtStreet.Text = ""

            txtBuildingName.Text = ""

            txtHouseNo_Building_Landmark.Text = ""

            Session("EditFlag") = 0
            btnSave.Text = "Save"
            btnClear.Text = "Reset"
            lblError.Text = ""

            ddlStates.SelectedValue = "DELHI"
            ddlDistrict.SelectedValue = "NORTH DELHI"


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

                GV.FL.AddInDropDownListDistinct(ddlStates, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrict, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlStates.SelectedValue = "DELHI"
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
                    formheading_1.Text = "Edit DIGITAL SIGNATURE "
                    DS = GV.FL.OpenDs(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Digital_signature where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApplicantLastName")) Then
                                If Not DS.Tables(0).Rows(0).Item("ApplicantLastName").ToString() = "" Then
                                    txtApplicantLastName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApplicantLastName").ToString())
                                Else
                                    txtApplicantLastName.Text = ""
                                End If
                            Else
                                txtApplicantLastName.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("AdhaarNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("AdhaarNo").ToString() = "" Then
                                    txtAdhaarNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AdhaarNo").ToString())
                                Else
                                    txtAdhaarNo.Text = ""
                                End If
                            Else
                                txtAdhaarNo.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("PanNo")) Then
                                If Not DS.Tables(0).Rows(0).Item("PanNo").ToString() = "" Then
                                    txtPanNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PanNo").ToString())
                                Else
                                    txtPanNo.Text = ""
                                End If
                            Else
                                txtPanNo.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("DOB")) Then
                                If Not DS.Tables(0).Rows(0).Item("DOB").ToString() = "" Then
                                    txtDOB.Text = GV.parseString(DS.Tables(0).Rows(0).Item("DOB").ToString())
                                Else
                                    txtDOB.Text = ""
                                End If
                            Else
                                txtDOB.Text = ""
                            End If

                            If ddlTypeofsignature.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Typeofsignature")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Typeofsignature").ToString() = "" Then
                                        ddlTypeofsignature.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Typeofsignature").ToString())
                                    Else
                                        ddlTypeofsignature.SelectedIndex = 0
                                    End If
                                Else
                                    ddlTypeofsignature.SelectedIndex = 0
                                End If
                            End If

                            If ddlStates.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("States")) Then
                                    If Not DS.Tables(0).Rows(0).Item("States").ToString() = "" Then
                                        ddlStates.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("States").ToString())
                                    Else
                                        ddlStates.SelectedIndex = 0
                                    End If
                                Else
                                    ddlStates.SelectedIndex = 0
                                End If
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Location")) Then
                                If Not DS.Tables(0).Rows(0).Item("Location").ToString() = "" Then
                                    txtLocation.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Location").ToString())
                                Else
                                    txtLocation.Text = ""
                                End If
                            Else
                                txtLocation.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Street")) Then
                                If Not DS.Tables(0).Rows(0).Item("Street").ToString() = "" Then
                                    txtStreet.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Street").ToString())
                                Else
                                    txtStreet.Text = ""
                                End If
                            Else
                                txtStreet.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("BuildingName")) Then
                                If Not DS.Tables(0).Rows(0).Item("BuildingName").ToString() = "" Then
                                    txtBuildingName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("BuildingName").ToString())
                                Else
                                    txtBuildingName.Text = ""
                                End If
                            Else
                                txtBuildingName.Text = ""
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