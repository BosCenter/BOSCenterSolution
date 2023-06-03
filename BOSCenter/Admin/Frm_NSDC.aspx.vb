Public Class Frm_NSDC
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VRegistrationDate, VTrainingCentreName, VAddress, VNearbyLandmark, VPincode, VState_UnionTerritory, VDistrict, VTehsil_Mandal_Block, VCity_Village_Town, VParliamentaryConstituency, VGeoLocation, VAddressProof As String
    Dim EditFlag As Integer = 0



    Dim QryStr As String = ""

    Dim DS As New DataSet

    Dim isErrorFound As Boolean = False
    Dim IsFocusApplied As Boolean = False
    Dim VError_Sring As String
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("NameTheForm.aspx") '/Change the name of form
            Else
                txtRegistrationDate.CssClass = "form-control"
                txtTrainingCentreName.CssClass = "form-control"
                txtAddress.CssClass = "form-control"
                txtNearbyLandmark.CssClass = "form-control"
                txtPincode.CssClass = "form-control"
                ddlState_UnionTerritory.CssClass = "form-control"
                ddlDistrict.CssClass = "form-control"
                txtTehsil_Mandal_Block.CssClass = "form-control"
                txtCity_Village_Town.CssClass = "form-control"
                txtParliamentaryConstituency.CssClass = "form-control"
                txtGeoLocation.CssClass = "form-control"
                ddlAddressProof.CssClass = "form-control"

                VRegistrationDate = ""
                VTrainingCentreName = ""
                VAddress = ""
                VNearbyLandmark = ""
                VPincode = ""
                VState_UnionTerritory = ""
                VDistrict = ""
                VTehsil_Mandal_Block = ""
                VCity_Village_Town = ""
                VParliamentaryConstituency = ""
                VGeoLocation = ""
                VAddressProof = ""
                lblError.Text = ""
                lblError.CssClass = ""
                txtRegistrationDate.Text = ""

                txtTrainingCentreName.Text = ""

                txtAddress.Text = ""

                txtNearbyLandmark.Text = ""

                txtPincode.Text = ""

                If ddlState_UnionTerritory.Items.Count > 0 Then
                    ddlState_UnionTerritory.SelectedIndex = 0
                End If

                If ddlDistrict.Items.Count > 0 Then
                    ddlDistrict.SelectedIndex = 0
                End If

                txtTehsil_Mandal_Block.Text = ""

                txtCity_Village_Town.Text = ""

                txtParliamentaryConstituency.Text = ""

                txtGeoLocation.Text = ""

                If ddlAddressProof.Items.Count > 0 Then
                    ddlAddressProof.SelectedIndex = 0
                End If
                ddlState_UnionTerritory.SelectedValue = "DELHI"
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
            '//////////// Start Bulk Validation
            lblError.Text = ""
            lblError.CssClass = ""

            If GV.parseString(txtRegistrationDate.Text) = "" Then
                txtRegistrationDate.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtRegistrationDate.Focus()
                    IsFocusApplied = True
                End If
            ElseIf Not IsDate(GV.FL.returnDateMonthWiseWithDateChecking(txtRegistrationDate.text)) = True Then
                txtRegistrationDate.CssClass = "ValidationError"
                isErrorFound = True
                If VError_Sring.Trim = "" Then
                    VError_Sring = "Please Enter Correct Date Format."
                Else
                    VError_Sring = VError_Sring & "<br>" & "Please Enter Correct Date Format."
                End If
                If IsFocusApplied = False Then
                    txtRegistrationDate.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtRegistrationDate.CssClass = "form-control"
                VRegistrationDate = GV.FL.returnDateMonthWiseWithDateChecking(txtRegistrationDate.Text)
            End If

            If GV.parseString(txtTrainingCentreName.Text) = "" Then
                txtTrainingCentreName.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtTrainingCentreName.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtTrainingCentreName.CssClass = "form-control"
                VTrainingCentreName = GV.parseString(txtTrainingCentreName.Text)
            End If

            If GV.parseString(txtAddress.Text) = "" Then
                txtAddress.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtAddress.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtAddress.CssClass = "form-control"
                VAddress = GV.parseString(txtAddress.Text)
            End If

            If GV.parseString(txtNearbyLandmark.Text) = "" Then
                txtNearbyLandmark.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtNearbyLandmark.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtNearbyLandmark.CssClass = "form-control"
                VNearbyLandmark = GV.parseString(txtNearbyLandmark.Text)
            End If

            If GV.parseString(txtPincode.Text) = "" Then
                txtPincode.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtPincode.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtPincode.CssClass = "form-control"
                VPincode = GV.parseString(txtPincode.Text)
            End If

            If GV.parseString(ddlState_UnionTerritory.SelectedValue) = "" Then
                ddlState_UnionTerritory.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    ddlState_UnionTerritory.Focus()
                    IsFocusApplied = True
                End If
            Else
                ddlState_UnionTerritory.CssClass = "form-control"
                VState_UnionTerritory = GV.parseString(ddlState_UnionTerritory.SelectedValue)
            End If

            If GV.parseString(ddlDistrict.SelectedValue) = "" Then
                ddlDistrict.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    ddlDistrict.Focus()
                    IsFocusApplied = True
                End If
            Else
                ddlDistrict.CssClass = "form-control"
                VDistrict = GV.parseString(ddlDistrict.SelectedValue)
            End If

            If GV.parseString(txtTehsil_Mandal_Block.Text) = "" Then
                txtTehsil_Mandal_Block.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtTehsil_Mandal_Block.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtTehsil_Mandal_Block.CssClass = "form-control"
                VTehsil_Mandal_Block = GV.parseString(txtTehsil_Mandal_Block.Text)
            End If

            If GV.parseString(txtCity_Village_Town.Text) = "" Then
                txtCity_Village_Town.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtCity_Village_Town.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtCity_Village_Town.CssClass = "form-control"
                VCity_Village_Town = GV.parseString(txtCity_Village_Town.Text)
            End If

            If GV.parseString(txtParliamentaryConstituency.Text) = "" Then
                txtParliamentaryConstituency.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtParliamentaryConstituency.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtParliamentaryConstituency.CssClass = "form-control"
                VParliamentaryConstituency = GV.parseString(txtParliamentaryConstituency.Text)
            End If

            If GV.parseString(txtGeoLocation.Text) = "" Then
                txtGeoLocation.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    txtGeoLocation.Focus()
                    IsFocusApplied = True
                End If
            Else
                txtGeoLocation.CssClass = "form-control"
                VGeoLocation = GV.parseString(txtGeoLocation.Text)
            End If

            If GV.parseString(ddlAddressProof.SelectedValue) = "" Then
                ddlAddressProof.CssClass = "ValidationError"
                isErrorFound = True
                If IsFocusApplied = False Then
                    ddlAddressProof.Focus()
                    IsFocusApplied = True
                End If
            Else
                ddlAddressProof.CssClass = "form-control"
                VAddressProof = GV.parseString(ddlAddressProof.SelectedValue)
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
            '///////////  End Bulk Validation



            'lblError.Text = ""
            'lblError.CssClass = ""
            'If Not txtRegistrationDate.Text.Trim = "" Then
            '    VRegistrationDate = GV.parseString(txtRegistrationDate.Text.Trim)
            'Else
            '    VRegistrationDate = ""
            'End If

            'If Not txtTrainingCentreName.Text.Trim = "" Then
            '    VTrainingCentreName = GV.parseString(txtTrainingCentreName.Text.Trim)
            'Else
            '    VTrainingCentreName = ""
            'End If

            'If Not txtAddress.Text.Trim = "" Then
            '    VAddress = GV.parseString(txtAddress.Text.Trim)
            'Else
            '    VAddress = ""
            'End If

            'If Not txtNearbyLandmark.Text.Trim = "" Then
            '    VNearbyLandmark = GV.parseString(txtNearbyLandmark.Text.Trim)
            'Else
            '    VNearbyLandmark = ""
            'End If

            'If Not txtPincode.Text.Trim = "" Then
            '    VPincode = GV.parseString(txtPincode.Text.Trim)
            'Else
            '    VPincode = ""
            'End If

            'If ddlState_UnionTerritory.Items.Count > 0 Then
            '    If Not ddlState_UnionTerritory.SelectedValue.Trim = "" Then
            '        VState_UnionTerritory = GV.parseString(ddlState_UnionTerritory.SelectedValue.Trim)
            '    Else
            '        VState_UnionTerritory = ""
            '    End If
            'End If

            'If ddlDistrict.Items.Count > 0 Then
            '    If Not ddlDistrict.SelectedValue.Trim = "" Then
            '        VDistrict = GV.parseString(ddlDistrict.SelectedValue.Trim)
            '    Else
            '        VDistrict = ""
            '    End If
            'End If

            'If Not txtTehsil_Mandal_Block.Text.Trim = "" Then
            '    VTehsil_Mandal_Block = GV.parseString(txtTehsil_Mandal_Block.Text.Trim)
            'Else
            '    VTehsil_Mandal_Block = ""
            'End If

            'If Not txtCity_Village_Town.Text.Trim = "" Then
            '    VCity_Village_Town = GV.parseString(txtCity_Village_Town.Text.Trim)
            'Else
            '    VCity_Village_Town = ""
            'End If

            'If Not txtParliamentaryConstituency.Text.Trim = "" Then
            '    VParliamentaryConstituency = GV.parseString(txtParliamentaryConstituency.Text.Trim)
            'Else
            '    VParliamentaryConstituency = ""
            'End If

            'If Not txtGeoLocation.Text.Trim = "" Then
            '    VGeoLocation = GV.parseString(txtGeoLocation.Text.Trim)
            'Else
            '    VGeoLocation = ""
            'End If

            'If ddlAddressProof.Items.Count > 0 Then
            '    If Not ddlAddressProof.SelectedValue.Trim = "" Then
            '        VAddressProof = GV.parseString(ddlAddressProof.SelectedValue.Trim)
            '    Else
            '        VAddressProof = ""
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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_NSDC_Details where RID=" & lblRID.Text.Trim & ""
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
            If Not txtRegistrationDate.Text.Trim = "" Then
                VRegistrationDate = GV.parseString(txtRegistrationDate.Text.Trim)
            Else
                VRegistrationDate = ""
            End If

            If Not txtTrainingCentreName.Text.Trim = "" Then
                VTrainingCentreName = GV.parseString(txtTrainingCentreName.Text.Trim)
            Else
                VTrainingCentreName = ""
            End If

            If Not txtAddress.Text.Trim = "" Then
                VAddress = GV.parseString(txtAddress.Text.Trim)
            Else
                VAddress = ""
            End If

            If Not txtNearbyLandmark.Text.Trim = "" Then
                VNearbyLandmark = GV.parseString(txtNearbyLandmark.Text.Trim)
            Else
                VNearbyLandmark = ""
            End If

            If Not txtPincode.Text.Trim = "" Then
                VPincode = GV.parseString(txtPincode.Text.Trim)
            Else
                VPincode = ""
            End If

            If ddlState_UnionTerritory.Items.Count > 0 Then
                If Not ddlState_UnionTerritory.SelectedValue.Trim = "" Then
                    VState_UnionTerritory = GV.parseString(ddlState_UnionTerritory.SelectedValue.Trim)
                Else
                    VState_UnionTerritory = ""
                End If
            End If

            If ddlDistrict.Items.Count > 0 Then
                If Not ddlDistrict.SelectedValue.Trim = "" Then
                    VDistrict = GV.parseString(ddlDistrict.SelectedValue.Trim)
                Else
                    VDistrict = ""
                End If
            End If

            If Not txtTehsil_Mandal_Block.Text.Trim = "" Then
                VTehsil_Mandal_Block = GV.parseString(txtTehsil_Mandal_Block.Text.Trim)
            Else
                VTehsil_Mandal_Block = ""
            End If

            If Not txtCity_Village_Town.Text.Trim = "" Then
                VCity_Village_Town = GV.parseString(txtCity_Village_Town.Text.Trim)
            Else
                VCity_Village_Town = ""
            End If

            If Not txtParliamentaryConstituency.Text.Trim = "" Then
                VParliamentaryConstituency = GV.parseString(txtParliamentaryConstituency.Text.Trim)
            Else
                VParliamentaryConstituency = ""
            End If

            If Not txtGeoLocation.Text.Trim = "" Then
                VGeoLocation = GV.parseString(txtGeoLocation.Text.Trim)
            Else
                VGeoLocation = ""
            End If

            If ddlAddressProof.Items.Count > 0 Then
                If Not ddlAddressProof.SelectedValue.Trim = "" Then
                    VAddressProof = GV.parseString(ddlAddressProof.SelectedValue.Trim)
                Else
                    VAddressProof = ""
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_NSDC_Details Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_NSDC_Details (RecordDateTime, EntryBy,RegistrationDate,TrainingCentreName,Address,NearbyLandmark,Pincode,State_UnionTerritory,District,Tehsil_Mandal_Block,City_Village_Town,ParliamentaryConstituency,GeoLocation,AddressProof) values( '" & VRecordDateTime & "','" & VEntryBy & "','" & VRegistrationDate & "','" & VTrainingCentreName & "','" & VAddress & "','" & VNearbyLandmark & "','" & VPincode & "','" & VState_UnionTerritory & "','" & VDistrict & "','" & VTehsil_Mandal_Block & "','" & VCity_Village_Town & "','" & VParliamentaryConstituency & "','" & VGeoLocation & "','" & VAddressProof & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_NSDC_Details set UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', RegistrationDate='" & VRegistrationDate & "', TrainingCentreName='" & VTrainingCentreName & "', Address='" & VAddress & "', NearbyLandmark='" & VNearbyLandmark & "', Pincode='" & VPincode & "', State_UnionTerritory='" & VState_UnionTerritory & "', District='" & VDistrict & "', Tehsil_Mandal_Block='" & VTehsil_Mandal_Block & "', City_Village_Town='" & VCity_Village_Town & "', ParliamentaryConstituency='" & VParliamentaryConstituency & "', GeoLocation='" & VGeoLocation & "', AddressProof='" & VAddressProof & "' where RID=" & lblRID.Text.Trim & ""
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
            VRegistrationDate = ""
            VTrainingCentreName = ""
            VAddress = ""
            VNearbyLandmark = ""
            VPincode = ""
            VState_UnionTerritory = ""
            VDistrict = ""
            VTehsil_Mandal_Block = ""
            VCity_Village_Town = ""
            VParliamentaryConstituency = ""
            VGeoLocation = ""
            VAddressProof = ""
            txtRegistrationDate.Text = ""

            txtTrainingCentreName.Text = ""

            txtAddress.Text = ""

            txtNearbyLandmark.Text = ""

            txtPincode.Text = ""

            If ddlState_UnionTerritory.Items.Count > 0 Then
                ddlState_UnionTerritory.SelectedIndex = 0
            End If

            If ddlDistrict.Items.Count > 0 Then
                ddlDistrict.SelectedIndex = 0
            End If

            txtTehsil_Mandal_Block.Text = ""

            txtCity_Village_Town.Text = ""

            txtParliamentaryConstituency.Text = ""

            txtGeoLocation.Text = ""

            If ddlAddressProof.Items.Count > 0 Then
                ddlAddressProof.SelectedIndex = 0
            End If
            ddlState_UnionTerritory.SelectedValue = "DELHI"
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

                GV.FL.AddInDropDownListDistinct(ddlState_UnionTerritory, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrict, " District_Name ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlState_UnionTerritory.SelectedValue = "DELHI"
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
                    formheading_1.Text = "Edit NSDC Form "
                    DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_NSDC_Details where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("RegistrationDate")) Then
                                If Not DS.Tables(0).Rows(0).Item("RegistrationDate").ToString() = "" Then
                                    txtRegistrationDate.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RegistrationDate").ToString())
                                Else
                                    txtRegistrationDate.Text = ""
                                End If
                            Else
                                txtRegistrationDate.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("TrainingCentreName")) Then
                                If Not DS.Tables(0).Rows(0).Item("TrainingCentreName").ToString() = "" Then
                                    txtTrainingCentreName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("TrainingCentreName").ToString())
                                Else
                                    txtTrainingCentreName.Text = ""
                                End If
                            Else
                                txtTrainingCentreName.Text = ""
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NearbyLandmark")) Then
                                If Not DS.Tables(0).Rows(0).Item("NearbyLandmark").ToString() = "" Then
                                    txtNearbyLandmark.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NearbyLandmark").ToString())
                                Else
                                    txtNearbyLandmark.Text = ""
                                End If
                            Else
                                txtNearbyLandmark.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Pincode")) Then
                                If Not DS.Tables(0).Rows(0).Item("Pincode").ToString() = "" Then
                                    txtPincode.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Pincode").ToString())
                                Else
                                    txtPincode.Text = ""
                                End If
                            Else
                                txtPincode.Text = ""
                            End If

                            If ddlState_UnionTerritory.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("State_UnionTerritory")) Then
                                    If Not DS.Tables(0).Rows(0).Item("State_UnionTerritory").ToString() = "" Then
                                        ddlState_UnionTerritory.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("State_UnionTerritory").ToString())
                                    Else
                                        ddlState_UnionTerritory.SelectedIndex = 0
                                    End If
                                Else
                                    ddlState_UnionTerritory.SelectedIndex = 0
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Tehsil_Mandal_Block")) Then
                                If Not DS.Tables(0).Rows(0).Item("Tehsil_Mandal_Block").ToString() = "" Then
                                    txtTehsil_Mandal_Block.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Tehsil_Mandal_Block").ToString())
                                Else
                                    txtTehsil_Mandal_Block.Text = ""
                                End If
                            Else
                                txtTehsil_Mandal_Block.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("City_Village_Town")) Then
                                If Not DS.Tables(0).Rows(0).Item("City_Village_Town").ToString() = "" Then
                                    txtCity_Village_Town.Text = GV.parseString(DS.Tables(0).Rows(0).Item("City_Village_Town").ToString())
                                Else
                                    txtCity_Village_Town.Text = ""
                                End If
                            Else
                                txtCity_Village_Town.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ParliamentaryConstituency")) Then
                                If Not DS.Tables(0).Rows(0).Item("ParliamentaryConstituency").ToString() = "" Then
                                    txtParliamentaryConstituency.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ParliamentaryConstituency").ToString())
                                Else
                                    txtParliamentaryConstituency.Text = ""
                                End If
                            Else
                                txtParliamentaryConstituency.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("GeoLocation")) Then
                                If Not DS.Tables(0).Rows(0).Item("GeoLocation").ToString() = "" Then
                                    txtGeoLocation.Text = GV.parseString(DS.Tables(0).Rows(0).Item("GeoLocation").ToString())
                                Else
                                    txtGeoLocation.Text = ""
                                End If
                            Else
                                txtGeoLocation.Text = ""
                            End If

                            If ddlAddressProof.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("AddressProof")) Then
                                    If Not DS.Tables(0).Rows(0).Item("AddressProof").ToString() = "" Then
                                        ddlAddressProof.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("AddressProof").ToString())
                                    Else
                                        ddlAddressProof.SelectedIndex = 0
                                    End If
                                Else
                                    ddlAddressProof.SelectedIndex = 0
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