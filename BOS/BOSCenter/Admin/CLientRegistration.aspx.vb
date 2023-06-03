
Imports System.IO
Imports System.Net


Public Class CLientRegistration
    Inherits System.Web.UI.Page


    Dim GV As New GlobalVariable("SUPERADMIN")

    Dim VPlanType, VUpdatedBy, VUpdatedOn As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                ddlCreateDatabase.Enabled = False
                ddlCreateDatabase.CssClass = "form-control"


                lblSessionFlag_App_delete.Text = "Fresh"

                GV.FL.AddInDropDownListDistinct(ddlState, "State_Name", "CRM_StateMaster")
                If ddlState.Items.Count > 0 Then
                    ddlState.Items.Insert(0, ":::: Select State ::::")
                Else
                    ddlState.Items.Add(":::: Select State ::::")
                End If

                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add(":::: Select District ::::")

                GV.FL.AddInDropDownListDistinct(ddlCountry, "Country_Name", "CRM_CountryMaster")
                If ddlCountry.Items.Count > 0 Then
                    ddlCountry.SelectedValue = "INDIA"
                    ddlCountry.Items.Insert(0, ":::: Select Country ::::")
                Else
                    ddlCountry.Items.Add(":::: Select Country ::::")
                End If



                lblSessionFlag.Text = 0


                btnUpload.Text = "Upload"
                btnDeleteUpload.Enabled = False
                lblPageHeading.Text = "::: Client Registration :::"
                lblSessionFlag.Text = 0
                If Not Session("RID") = "" And Not Session("ClientRegistrationEdit") = 0 And Session("ClientRegistrationEditConfirm") = 9 Then
                    lblSessionRID.Text = Session("RID").ToString()
                    Session("RID") = ""
                    Session("ClientRegistrationEdit") = 0
                    Div_AccountInfo.Visible = True
                    lblPageHeading.Text = ":::: Edit ClientRegistration Details ::::"
                    lblSessionFlag_App_delete.Text = "Edit"



                    DS = GV.FL.OpenDs("BOS_ClientRegistration where RID= " & lblSessionRID.Text.Trim & "")

                    If DS.Tables(0).Rows.Count > 0 Then
                        btnUpload.Enabled = True
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CompanyCode")) Then
                            If Not DS.Tables(0).Rows(0).Item("CompanyCode") = "" Then
                                txtCompanyCode.Text = DS.Tables(0).Rows(0).Item("CompanyCode").ToString()
                            Else
                                txtCompanyCode.Text = ""
                            End If
                        Else
                            txtCompanyCode.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CompanyName")) Then
                            If Not DS.Tables(0).Rows(0).Item("CompanyName") = "" Then
                                txtCompanyName.Text = DS.Tables(0).Rows(0).Item("CompanyName").ToString()
                            Else
                                txtCompanyName.Text = ""
                            End If
                        Else
                            txtCompanyName.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ClientPassword")) Then
                            If Not DS.Tables(0).Rows(0).Item("ClientPassword") = "" Then
                                txtPassword.Text = DS.Tables(0).Rows(0).Item("ClientPassword").ToString()
                            Else
                                txtPassword.Text = ""
                            End If
                        Else
                            txtPassword.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Status")) Then
                            If Not DS.Tables(0).Rows(0).Item("Status") = "" Then
                                ddlAccountStatus.SelectedValue = DS.Tables(0).Rows(0).Item("Status").ToString()
                            Else
                                ddlAccountStatus.SelectedValue = ""
                            End If
                        Else
                            ddlAccountStatus.SelectedValue = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CompanyHead")) Then
                            If Not DS.Tables(0).Rows(0).Item("CompanyHead") = "" Then
                                txtCompanyHead.Text = DS.Tables(0).Rows(0).Item("CompanyHead").ToString
                            Else
                                txtCompanyHead.Text = ""
                            End If
                        Else
                            txtCompanyHead.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ContactPerson")) Then
                            If Not DS.Tables(0).Rows(0).Item("ContactPerson") = "" Then
                                txtContactPerson.Text = DS.Tables(0).Rows(0).Item("ContactPerson").ToString()
                            Else
                                txtContactPerson.Text = ""
                            End If
                        Else
                            txtCompanyHead.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Address_1")) Then
                            txtAddress_1.Text = DS.Tables(0).Rows(0).Item("Address_1").ToString()
                        Else
                            txtAddress_1.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Address_2")) Then
                            txtAddress_2.Text = DS.Tables(0).Rows(0).Item("Address_2").ToString()
                        Else
                            txtAddress_2.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Address_3")) Then
                            txtAddress_3.Text = DS.Tables(0).Rows(0).Item("Address_3").ToString()
                        Else
                            txtAddress_3.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("PinCode")) Then
                            txtPinCode.Text = DS.Tables(0).Rows(0).Item("PinCode").ToString()
                        Else
                            txtPinCode.Text = ""
                        End If
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("State")) Then
                            ddlState.SelectedValue = DS.Tables(0).Rows(0).Item("State").ToString()
                        Else
                            ddlState.SelectedIndex = 0
                        End If
                        ddlState_SelectedIndexChanged(sender, e)
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("District")) Then
                            If Not DS.Tables(0).Rows(0).Item("District") = "" Then
                                ddlDistrict.SelectedValue = DS.Tables(0).Rows(0).Item("District").ToString()
                            Else
                                ddlDistrict.SelectedIndex = 0
                            End If
                        Else
                            ddlDistrict.SelectedIndex = 0
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Country")) Then
                            ddlCountry.SelectedValue = DS.Tables(0).Rows(0).Item("Country").ToString()
                        Else
                            ddlCountry.SelectedIndex = 0
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CreditBalnceLimit")) Then
                            If Not DS.Tables(0).Rows(0).Item("CreditBalnceLimit").ToString() = "" Then
                                txtCreditBalLimit.Text = GV.parseString(DS.Tables(0).Rows(0).Item("CreditBalnceLimit").ToString())
                            Else
                                txtCreditBalLimit.Text = "0"
                            End If
                        Else
                            txtCreditBalLimit.Text = "0"
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("City")) Then
                            txtCity.Text = DS.Tables(0).Rows(0).Item("City").ToString()
                        Else
                            txtCity.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("PhoneNo_1")) Then
                            txtPhoneNo_1.Text = DS.Tables(0).Rows(0).Item("PhoneNo_1").ToString()
                        Else
                            txtPhoneNo_1.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("PhoneNo_2")) Then
                            txtPhoneNo_2.Text = DS.Tables(0).Rows(0).Item("PhoneNo_2").ToString()
                        Else
                            txtPhoneNo_2.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Mobile_No")) Then
                            txtMobile_No.Text = DS.Tables(0).Rows(0).Item("Mobile_No").ToString()
                        Else
                            txtMobile_No.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Email_ID")) Then
                            txtEmail_ID.Text = DS.Tables(0).Rows(0).Item("Email_ID").ToString()
                        Else
                            txtEmail_ID.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("TinNo")) Then
                            txtTinNo.Text = DS.Tables(0).Rows(0).Item("TinNo").ToString()
                        Else
                            txtTinNo.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CinNo")) Then
                            txtCinNo.Text = DS.Tables(0).Rows(0).Item("CinNo").ToString()
                        Else
                            txtCinNo.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("GSTNO")) Then
                            txtGSTNo.Text = DS.Tables(0).Rows(0).Item("GSTNO").ToString()
                        Else
                            txtGSTNo.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebRedirectUrl")) Then
                            txtWebRedirectUrl.Text = DS.Tables(0).Rows(0).Item("WebRedirectUrl").ToString()
                        Else
                            txtWebRedirectUrl.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("IsNewDatabase")) Then
                            ddlCreateDatabase.SelectedValue = DS.Tables(0).Rows(0).Item("IsNewDatabase").ToString()
                        Else
                            ddlCreateDatabase.SelectedValue = "No"
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("DatabaseName")) Then
                            lblDatabaseName.Text = DS.Tables(0).Rows(0).Item("DatabaseName").ToString()
                        Else
                            lblDatabaseName.Text = "BosCenter_DB"
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ClientPin")) Then
                            txtTransactionPin.Text = DS.Tables(0).Rows(0).Item("ClientPin").ToString()
                        Else
                            txtTransactionPin.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Companylogo")) Then
                            If Not DS.Tables(0).Rows(0).Item("Companylogo") = "" Then
                                'If File.Exists(DS.Tables(0).Rows(0).Item("Companylogo").ToString()) Then
                                VCompanylogo = DS.Tables(0).Rows(0).Item("Companylogo").ToString()
                                Image1.ImageUrl = DS.Tables(0).Rows(0).Item("Companylogo").ToString()
                                btnUpload.ToolTip = DS.Tables(0).Rows(0).Item("Companylogo").ToString()
                                btnUpload.Text = "Download"
                                btnDeleteUpload.Enabled = True
                                'Else
                                '    Image1.ImageUrl = "~/images/MissingIcon3.png"
                                '    btnUpload.ToolTip = DS.Tables(0).Rows(0).Item("Companylogo").ToString()
                                '    Image1.Visible = True
                                '    btnUpload.Text = "Download"
                                '    btnDeleteUpload.Enabled = True
                                'End If
                            Else
                                VCompanylogo = ""

                                btnUpload.Text = "Upload"
                                btnDeleteUpload.Enabled = False
                            End If

                        Else
                            VCompanylogo = ""
                            btnUpload.Text = "Upload"
                            btnDeleteUpload.Enabled = False
                        End If
                        If Not Session("Deleted") Is Nothing Then
                            If Session("Deleted") = True Then
                                lblSessionFlag_App_delete.Text = "Deleted"
                                lblPageHeading.Text = ":::: Deleted Client Details ::::"
                                Session("Deleted") = False
                                ddlCreateDatabase.Enabled = False
                                ddlCreateDatabase.CssClass = "form-control"
                                ddlDistrict.CssClass = "form-control"
                                ddlState.CssClass = "form-control"
                                ddlCountry.CssClass = "form-control"
                                ddlCreateDatabase.CssClass = "form-control"
                                ddlAccountStatus.CssClass = "form-control"
                                ddlCreateDatabase.Enabled = False
                                txtCompanyName.ReadOnly = True
                                txtCompanyHead.ReadOnly = True
                                txtContactPerson.ReadOnly = True
                                txtTinNo.ReadOnly = True
                                txtCinNo.ReadOnly = True
                                txtPassword.ReadOnly = True
                                ddlAccountStatus.Enabled = False
                                btnDeleteUpload.Visible = False
                                txtAddress_1.ReadOnly = True
                                txtAddress_2.ReadOnly = True
                                txtAddress_3.ReadOnly = True
                                ddlCountry.Enabled = False
                                ddlState.Enabled = False
                                ddlDistrict.Enabled = False
                                txtCity.ReadOnly = True
                                txtPinCode.ReadOnly = True
                                txtPhoneNo_1.ReadOnly = True
                                txtPhoneNo_2.ReadOnly = True
                                txtMobile_No.ReadOnly = True
                                txtEmail_ID.ReadOnly = True
                                btnClear.Text = "Close"
                                btnSave.Visible = False
                                btnCancel.Visible = False

                                FileUpload1.Enabled = False
                                btnDeleteUpload.Visible = False
                                If btnUpload.Text = "Upload" Then
                                    btnUpload.Enabled = False
                                Else
                                    btnUpload.Enabled = True
                                End If
                            End If
                        End If
                        If Not Session("Approved") Is Nothing Then
                            If Session("Approved") = True Then
                                lblPageHeading.Text = ":::: Approve Client Details ::::"
                                lblSessionFlag_App_delete.Text = "Approved"

                                Session("Approved") = False
                                ddlCreateDatabase.Enabled = False
                                ddlCreateDatabase.CssClass = "form-control"
                                txtCompanyName.ReadOnly = True
                                txtCompanyHead.ReadOnly = True
                                txtContactPerson.ReadOnly = True
                                txtTinNo.ReadOnly = True
                                txtPassword.ReadOnly = True
                                ddlAccountStatus.Enabled = False
                                ddlAccountStatus.CssClass = "form-control"
                                txtAddress_1.ReadOnly = True
                                txtAddress_2.ReadOnly = True
                                txtAddress_3.ReadOnly = True
                                ddlCountry.Enabled = False
                                ddlCountry.CssClass = "form-control"
                                ddlState.Enabled = False
                                ddlState.CssClass = "form-control"
                                ddlDistrict.Enabled = False
                                ddlDistrict.CssClass = "form-control"
                                txtCity.ReadOnly = True
                                txtPinCode.ReadOnly = True
                                txtPhoneNo_1.ReadOnly = True
                                txtPhoneNo_2.ReadOnly = True
                                txtMobile_No.ReadOnly = True
                                txtEmail_ID.ReadOnly = True
                                txtCinNo.ReadOnly = True
                                btnApprovalCancel.Enabled = True
                                btnApprovalCancel.Visible = True
                                btnSave.Text = "Approved"
                                btnSave.CssClass = "btn btn-success btn-sm mb3"
                                btnClear.Text = "Not Approved"
                                btnClear.CssClass = "btn btn-danger btn-sm mb3"
                                btnApprovalCancel.Text = "Cancel"
                                btnApprovalCancel.CssClass = "btn btn-primary btn-sm mb3"

                                FileUpload1.Enabled = False
                                btnDeleteUpload.Visible = False
                                If btnUpload.Text = "Upload" Then
                                    btnUpload.Enabled = False
                                Else
                                    btnUpload.Enabled = True
                                End If


                            End If
                        End If




                    End If
                    If btnSave.Text = "Approved" Then

                    Else
                        lblSessionFlag.Text = 1
                        ddlCreateDatabase.Enabled = False
                        ddlCreateDatabase.CssClass = "form-control"
                        btnSave.Text = "Update"
                        btnClear.Text = "Close"
                    End If
                Else
                    txtCompanyCode.Text = GV.FL.AddInVar("CompanyCode_Prefix", "AutoNumber") & GV.FL.getAutoNumber("CompanyCode")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Dim VBranchCode, VBranchName, VCinNo, VStatus, VGracePeriodInDays, VCompanyType, VCompanyCode, VCompanyName, VCompanyHead, VContactPerson, VCompanylogo, VDistrict, VAddress_1, VAddress_2, VAddress_3, VPinCode, VState, VCity, VCountry, VPhoneNo_1, VPhoneNo_2, VMobile_No, VEmail_ID, VFaxNo, VTinNo, VPlanCode, VPlanName, VBillingCycle, VBilingAmount, VPlanStartDate, VNextBillingDate, VNextBillingDateWithGracePeriod As String

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            Dim isErrorFound As Boolean = False
            Dim isFocusApplied As Boolean = False


            If btnSave.Text = "Approved" Then

                lblDialogMsg.Text = "Are You Sure You Want to Approve ?"
                lblDialogMsg.CssClass = ""
                btnOk.Text = "Yes"
                btnCancel.Attributes("style") = ""
                lblApproveOrUpapprove.Text = "Approved"
                ModalPopupExtender1.Show()

            ElseIf btnSave.Text = "Save" Or btnSave.Text = "Update" Then

                If GV.parseString(txtCompanyName.Text) = "" Then
                    txtCompanyName.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtCompanyName.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtCompanyName.CssClass = "form-control"
                End If



                If GV.parseString(txtContactPerson.Text) = "" Then
                    txtContactPerson.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtContactPerson.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtContactPerson.CssClass = "form-control"
                End If

                If GV.parseString(txtAddress_1.Text) = "" Then
                    txtAddress_1.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtAddress_1.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtAddress_1.CssClass = "form-control"
                End If

                If GV.parseString(txtAddress_2.Text) = "" Then
                    txtAddress_2.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtAddress_2.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtAddress_2.CssClass = "form-control"
                End If

                If GV.parseString(ddlCountry.SelectedValue) = ":::: Select Country ::::" Then
                    ddlCountry.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        ddlCountry.Focus()
                        isFocusApplied = True
                    End If
                Else
                    ddlCountry.CssClass = "form-control"
                End If

                If GV.parseString(ddlState.SelectedValue) = ":::: Select State ::::" Then
                    ddlState.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        ddlState.Focus()
                        isFocusApplied = True
                    End If
                Else
                    ddlState.CssClass = "form-control"
                End If

                If GV.parseString(ddlDistrict.SelectedValue) = ":::: Select District ::::" Then
                    ddlDistrict.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        ddlDistrict.Focus()
                        isFocusApplied = True
                    End If
                Else
                    ddlDistrict.CssClass = "form-control"
                End If

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

                If GV.parseString(txtPinCode.Text) = "" Then
                    txtPinCode.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtPinCode.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtPinCode.CssClass = "form-control"
                End If

                If GV.parseString(txtMobile_No.Text) = "" Then
                    txtMobile_No.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtMobile_No.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtMobile_No.CssClass = "form-control"
                End If

                If GV.parseString(txtEmail_ID.Text) = "" Then
                    txtEmail_ID.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtEmail_ID.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtEmail_ID.CssClass = "form-control"
                End If

                If lblSessionFlag.Text = 1 Then

                    If GV.parseString(txtPassword.Text) = "" Then
                        txtPassword.CssClass = "ValidationError"
                        isErrorFound = True
                        If isFocusApplied = False Then
                            txtPassword.Focus()
                            isFocusApplied = True
                        End If
                    Else
                        txtPassword.CssClass = "form-control"
                    End If

                    If GV.parseString(txtTransactionPin.Text) = "" Then
                        txtTransactionPin.CssClass = "ValidationError"
                        isErrorFound = True
                        If isFocusApplied = False Then
                            txtTransactionPin.Focus()
                            isFocusApplied = True
                        End If
                    Else
                        txtTransactionPin.CssClass = "form-control"
                    End If


                End If

                If isErrorFound = True Then
                    Exit Sub
                End If





                If lblSessionFlag.Text = 0 Then
                    lblSavingDialogBox.Text = "Are You Sure You Want to Save ?"
                    lblSavingDialogBox.CssClass = ""
                    lblSavingDialogBox.Visible = True
                    Div_deInfo.Visible = False
                    lblPassword.Text = " "
                    lblClientID.Text = " "
                    lblConfrmMsg.Text = ""
                    lblConfrmMsg.CssClass = ""
                    btnsaveOk.Text = "Yes"
                    btnsaveOk.Visible = True
                    BtnSaveCancel.Text = "No"
                    lblpendingaproval.Text = ""
                    lblpendingaproval.CssClass = ""
                    'btnCancel.Visible = False
                    BtnSaveCancel.Attributes("style") = ""

                    ModalPopupExtender2.Show()

                ElseIf lblSessionFlag.Text = 1 Then
                    'lblDialogMsg.Text = "Are You Sure You Want to Update?"
                    'ModalPopupExtender1.Show()

                    lblSavingDialogBox.Text = "Are You Sure You Want to Update ?"
                    lblSavingDialogBox.CssClass = ""
                    lblSavingDialogBox.Visible = True
                    Div_deInfo.Visible = False
                    lblPassword.Text = " "
                    lblClientID.Text = " "
                    lblConfrmMsg.Text = ""
                    lblConfrmMsg.CssClass = ""
                    btnsaveOk.Text = "Yes"
                    btnsaveOk.Visible = True
                    BtnSaveCancel.Text = "No"
                    lblpendingaproval.Text = ""
                    lblpendingaproval.CssClass = ""
                    'btnCancel.Visible = False
                    BtnSaveCancel.Attributes("style") = ""
                    ModalPopupExtender2.Show()
                End If
            End If




        Catch ex As Exception
        End Try
    End Sub
    Dim VRegisterationDate, VGSTNo, VClientPassword, VLastLogin, VClientRole, VIsNewDatabase, VDatabaseName, VChangeTheme, VRecord_DateTime, VEmpCode As String

    Private Sub Clear()
        Try
            ddlCreateDatabase.SelectedValue = "No"
            Div_deInfo.Visible = True
            btnApprovalCancel.Visible = False
            btnUpload.Text = "Upload"
            Image1.ImageUrl = "..\images\uploadimage.png"
            ddlCreateDatabase.SelectedIndex = 0
            txtCompanyCode.Text = ""
            txtCompanyName.Text = ""
            txtCompanyHead.Text = ""
            txtContactPerson.Text = ""
            txtAddress_1.Text = ""
            txtAddress_2.Text = ""
            txtAddress_3.Text = ""
            txtWebRedirectUrl.Text = ""
            txtPassword.Text = ""
            txtTransactionPin.Text = ""
            lblDatabaseName.Text = ""

            txtPinCode.Text = ""
            ddlState.SelectedIndex = 0
            If ddlState.SelectedValue = ":::: Select State ::::" Then
                ddlDistrict.Items.Insert(0, ":::: Select District ::::")
                ddlDistrict.SelectedIndex = 0
            End If
            txtCompanyCode.Text = GV.FL.AddInVar("CompanyCode_Prefix", "AutoNumber") & GV.FL.getAutoNumber("CompanyCode")
            txtCity.Text = ""
            ddlCountry.SelectedValue = "INDIA"
            txtPhoneNo_1.Text = ""
            txtPhoneNo_2.Text = ""
            txtMobile_No.Text = ""
            txtEmail_ID.Text = ""
            txtTinNo.Text = ""
            lblError.CssClass = ""
            txtCinNo.Text = ""
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            txtCreditBalLimit.Text = "0"
            btnSave.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Reset" Then
                Clear()
            ElseIf btnClear.Text = "Not Approved" Then


                lblDialogMsg.Text = "Are You Sure You Want to Not Approved ?"
                lblDialogMsg.CssClass = ""
                btnOk.Text = "Yes"
                btnCancel.Attributes("style") = ""
                lblApproveOrUpapprove.Text = "Not Approved"
                ModalPopupExtender1.Show()
            ElseIf btnClear.Text = "Close" Then
                If lblSessionFlag_App_delete.Text = "Deleted" Then
                    Response.Redirect("SearchDeletedClient.aspx")
                ElseIf lblSessionFlag_App_delete.Text = "Edit" Then
                    Response.Redirect("SearchClientDetails.aspx")
                End If
            Else
                Response.Redirect("SearchApproveClient.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click

        Try
            lblErrorImageError.Text = ""

            If (btnUpload.Text = "Download") Then
                DownloadDoc(btnUpload.ToolTip)

            Else
                If FileUpload1.HasFile = True Then
                    SaveImage(FileUpload1, btnUpload, btnDeleteUpload)
                    Image1.ImageUrl = btnUpload.ToolTip.ToString()

                Else
                    lblErrorImageError.Text = "No file Selected for photo"
                    btnSave.Focus()
                End If
            End If

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

    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                    Dim completeFilePath As String

                    If Not Directory.Exists(Server.MapPath("..\CompanyLogos")) Then
                        Directory.CreateDirectory(Server.MapPath("..\CompanyLogos"))
                    End If

                    completeFilePath = Server.MapPath("..\CompanyLogos\" & txtCompanyCode.Text & Session("ext"))
                    'imgPath = Server.MapPath("~\Docs\" & txtPatientID.Text & "\" & txtPrescriptionNo.Text & "\Doppler" & Session("ext"))
                    imgPath = "..\CompanyLogos\" & txtCompanyCode.Text & Session("ext")

                    imagUpload.PostedFile.SaveAs(completeFilePath)

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

    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload.Click
        Try
            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the Logo Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            ModalPopupExtender4.Show()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                VCompanyCode = GV.parseString(txtCompanyCode.Text.Trim)
                'VCompanyCode = get_Admin_SessionVariables("CompanyCode", Request, Response)

                ' If lblDeleteDocumentInfo.Text = "IncomeProof" Then

                Dim imgpath As String = btnUpload.ToolTip
                File.Delete(Server.MapPath(imgpath))
                qry = "update BOS_ClientRegistration set Companylogo=''  where CompanyCode='" & VCompanyCode & "' "
                btnUpload.ToolTip = ""
                btnUpload.Text = "Upload"
                btnDeleteUpload.Enabled = False
                Image1.ImageUrl = "..\images\uploadimage.png"
                btnDeleteUpload.Focus()

                lblDeleteInfo.Text = "Document Removed Successfully."
                lblDeleteInfo.CssClass = "successlabels"
                'End If


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

    Protected Sub ddlState_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlState.SelectedIndexChanged
        Try

            If ddlState.SelectedValue.Trim.ToUpper = ":::: Select State ::::".ToUpper Then
                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add(":::: Select District ::::")
                ddlDistrict.SelectedIndex = 0
            Else
                GV.FL.AddInDropDownListDistinct(ddlDistrict, "District_Name", "CRM_DistrictMaster where State_Name='" & ddlState.SelectedValue & "' and Country_Name='" & ddlCountry.SelectedValue & "'")
                If ddlDistrict.Items.Count > 0 Then
                    ddlDistrict.Items.Insert(0, ":::: Select District ::::")
                Else
                    ddlDistrict.Items.Add(":::: Select District ::::")
                End If
                ddlDistrict.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Function RandomPaswrd() As String
        Dim finalString As String = ""
        Try
            Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            Dim stringChars = New Char(8) {}
            Dim random = New Random()
            For i As Integer = 0 To stringChars.Length - 1
                stringChars(i) = chars(random.[Next](chars.Length))
            Next
            finalString = New String(stringChars)
        Catch ex As Exception
        End Try
        Return finalString
    End Function

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
        Try
            Dim Qry As String
            If btnOk.Text = "Yes" Then
                If lblApproveOrUpapprove.Text = "Approved" Then
                    Dim ApproBy_str As String = "ApprovedBy='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',Approval_DateTime=getDate(),"
                    Qry = "update BOS_ClientRegistration  set " & ApproBy_str & " ApprovedStatus='Approved' where RID=" & lblSessionRID.Text & ";"
                    If GV.parseString(ddlCreateDatabase.SelectedValue) = "Yes" Then
                        Qry = Qry & "update " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_ClientRegistration  set " & ApproBy_str & " ApprovedStatus='Approved' where CompanyCode='" & GV.parseString(txtCompanyCode.Text.Trim) & "'"
                    End If

                    Dim result As Boolean = GV.FL.DMLQueries(Qry)
                    lblDialogMsg.Text = result
                    If result = True Then
                        lblDialogMsg.Text = "Record Approved Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnOk.Text = "Ok"
                        btnCancel.Attributes("style") = "display:none"
                        ModalPopupExtender1.Show()
                    Else
                        lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                        lblDialogMsg.CssClass = "errorlabels"
                        ModalPopupExtender1.Show()
                        btnOk.Visible = False
                    End If
                ElseIf lblApproveOrUpapprove.Text = "Not Approved" Then
                    Dim result As Boolean
                    Qry = "update BOS_ClientRegistration set ApprovedStatus='Not Approved' where RID=" & lblSessionRID.Text & ";"
                    If GV.parseString(ddlCreateDatabase.SelectedValue) = "Yes" Then
                        Qry = Qry & "update " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_ClientRegistration  set ApprovedStatus='Not Approved' where CompanyCode='" & GV.parseString(txtCompanyCode.Text.Trim) & "'"
                    End If
                    result = GV.FL.DMLQueries(Qry)
                    lblDialogMsg.Text = result
                    If result = True Then
                        lblDialogMsg.Text = "Record Not Approved Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnOk.Text = "Ok"
                        btnCancel.Attributes("style") = "display:none"
                        ModalPopupExtender1.Show()
                    Else
                        lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                        lblDialogMsg.CssClass = "errorlabels"
                        ModalPopupExtender1.Show()
                        btnOk.Visible = False
                    End If
                End If
            ElseIf btnOk.Text = "Ok" Then
                Response.Redirect("SearchApproveClient.aspx")
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnApprovalCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprovalCancel.Click
        Try
            Response.Redirect("SearchApproveClient.aspx")
        Catch ex As Exception

        End Try
    End Sub
    Dim qry1 As String

    Dim VWebRedirectUrl, VCreditBalLimit As String

    Protected Sub btnsaveOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsaveOk.Click
        Try

            If btnsaveOk.Text = "Ok" Then
                If lblSessionFlag.Text = 1 Then
                    Response.Redirect("SearchClientDetails.aspx")
                Else
                    Clear()
                    Exit Sub
                End If
            End If

            VRegisterationDate = Now.Date()


            If Not txtCinNo.Text.Trim = "" Then
                VCinNo = GV.parseString(txtCinNo.Text.Trim)
            Else
                VCinNo = ""
            End If

            If Not ddlCreateDatabase.SelectedValue.Trim = "" Then
                VIsNewDatabase = GV.parseString(ddlCreateDatabase.SelectedValue.Trim)
            Else
                VIsNewDatabase = ""
            End If

            If Not txtCompanyCode.Text.Trim = "" Then
                VCompanyCode = GV.parseString(txtCompanyCode.Text.Trim)
            Else
                VCompanyCode = ""
            End If
            If Not txtCity.Text.Trim = "" Then
                VCity = GV.parseString(txtCity.Text.Trim)
            Else
                VCity = ""
            End If
            If Not txtCompanyName.Text.Trim = "" Then
                VCompanyName = GV.parseString(txtCompanyName.Text.Trim)
            Else
                VCompanyName = ""
            End If

            If Not txtCompanyHead.Text.Trim = "" Then
                VCompanyHead = GV.parseString(txtCompanyHead.Text.Trim)
            Else
                VCompanyHead = ""
            End If

            If Not txtContactPerson.Text.Trim = "" Then
                VContactPerson = GV.parseString(txtContactPerson.Text.Trim)
            Else
                VContactPerson = ""
            End If

            If Not txtAddress_1.Text.Trim = "" Then
                VAddress_1 = GV.parseString(txtAddress_1.Text.Trim)
            Else
                VAddress_1 = ""
            End If

            If Not txtAddress_2.Text.Trim = "" Then
                VAddress_2 = GV.parseString(txtAddress_2.Text.Trim)
            Else
                VAddress_2 = ""
            End If

            If Not txtAddress_3.Text.Trim = "" Then
                VAddress_3 = GV.parseString(txtAddress_3.Text.Trim)
            Else
                VAddress_3 = ""
            End If

            If Not txtPinCode.Text.Trim = "" Then
                VPinCode = GV.parseString(txtPinCode.Text.Trim)
            Else
                VPinCode = ""
            End If

            If Not ddlState.SelectedValue = "" Then
                VState = GV.parseString(ddlState.SelectedValue)
            Else
                VState = ""
            End If

            If Not ddlDistrict.SelectedValue.Trim = "" Then
                VDistrict = GV.parseString(ddlDistrict.SelectedValue.Trim)
            Else
                VDistrict = ""
            End If

            If Not ddlCountry.SelectedValue = "" Then
                VCountry = GV.parseString(ddlCountry.SelectedValue)
            Else
                VCountry = ""
            End If

            If Not txtPhoneNo_1.Text.Trim = "" Then
                VPhoneNo_1 = GV.parseString(txtPhoneNo_1.Text.Trim)
            Else
                VPhoneNo_1 = ""
            End If

            If Not txtPhoneNo_2.Text.Trim = "" Then
                VPhoneNo_2 = GV.parseString(txtPhoneNo_2.Text.Trim)
            Else
                VPhoneNo_2 = ""
            End If

            If Not txtMobile_No.Text.Trim = "" Then
                VMobile_No = GV.parseString(txtMobile_No.Text.Trim)
            Else
                VMobile_No = ""
            End If

            If Not txtEmail_ID.Text.Trim = "" Then
                VEmail_ID = GV.parseString(txtEmail_ID.Text.Trim)
            Else
                VEmail_ID = ""
            End If

            If Not txtTinNo.Text.Trim = "" Then
                VTinNo = GV.parseString(txtTinNo.Text.Trim)
            Else
                VTinNo = ""
            End If

            If Not txtWebRedirectUrl.Text.Trim = "" Then
                VWebRedirectUrl = GV.parseString(txtWebRedirectUrl.Text.Trim)
            Else
                VWebRedirectUrl = ""
            End If


            If Not txtGSTNo.Text.Trim = "" Then
                VGSTNo = GV.parseString(txtGSTNo.Text.Trim)
            Else
                VGSTNo = ""
            End If

            If Not txtCreditBalLimit.Text.Trim = "" Then
                VCreditBalLimit = GV.parseString(txtCreditBalLimit.Text.Trim)
            Else
                VCreditBalLimit = "0"
            End If
         
            If UCase(btnUpload.Text = "Download") Then
                VCompanylogo = (btnUpload.ToolTip)

            Else
                VCompanylogo = ""
            End If

           



            VClientRole = "Admin"

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = Now.Date

            VRecord_DateTime = "getDate()"
            VEmpCode = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            'getdate()
            Dim vClientPassword As String = ""
            If lblSessionFlag.Text = 0 Then
                vClientPassword = RandomPaswrd()
                VStatus = "Active"
            Else
                vClientPassword = txtPassword.Text
                VStatus = ddlAccountStatus.SelectedValue
            End If
            vClientPassword = GV.EncryptString(GV.key, vClientPassword.Trim)


            If ddlCreateDatabase.SelectedValue = "Yes" Then
                VDatabaseName = VCompanyCode
                CreateNewData()
                'CreateNewDataAlter()
            Else
                VDatabaseName = GV.DefaultDatabase()
            End If

            If btnsaveOk.Text = "Yes" Then

                ' UpdateProgress1.Visible = True
                If lblSessionFlag.Text = 0 Then
                    If GV.FL.RecCount("BOS_ClientRegistration where CompanyCode='" & VCompanyCode & "'") > 0 Then 'Change where condition according to Criteria 

                        lblSavingDialogBox.Text = "Record Already Exists."
                        lblSavingDialogBox.CssClass = "errorlabels"
                        Div_deInfo.Visible = False
                        btnsaveOk.Visible = False
                        BtnSaveCancel.Text = "Ok"
                        Clear()
                        ModalPopupExtender2.Show()

                    Else
                        Dim APIStatus As String = "Inactive"

                        Dim qry As String = ""
                        If ddlCreateDatabase.SelectedValue.Trim.ToUpper = "No".ToUpper Then

                            '////////////////////////////////////   Start Qry For Current DB  ///////////////////////////////////////////////////////////////////////////////////////////////////////
                            'Sapna
                            qry = qry & "  " & " insert into BOS_ClientRegistration (Encrypted_Pass,HoldAmt,AEPS_API_Status,RechargeAPI_Status,MoneyTransferAPI_Status,PANCardAPI_Status,CreditBalnceLimit,WebRedirectUrl,RegisterationDate,CompanyCode,CompanyName,CompanyHead,ContactPerson,Companylogo,Address_1,Address_2,Address_3,PinCode,State,District,City,Country,PhoneNo_1,PhoneNo_2,Mobile_No,Email_ID,TinNo,CinNo,GSTNo,Status,ClientPassword,LastLogin,ClientRole,IsNewDatabase,DatabaseName,ChangeTheme,Record_DateTime,EmpCode,UpdatedBy,UpdatedOn) values('Null',',0,'" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "', '" & VCreditBalLimit & "', '" & VWebRedirectUrl & "', '" & VRegisterationDate & "','" & VCompanyCode & "','" & VCompanyName & "','" & VCompanyHead & "','" & VContactPerson & "','" & VCompanylogo & "','" & VAddress_1 & "','" & VAddress_2 & "','" & VAddress_3 & "','" & VPinCode & "','" & VState & "','" & VDistrict & "','" & VCity & "','" & VCountry & "','" & VPhoneNo_1 & "','" & VPhoneNo_2 & "','" & VMobile_No & "','" & VEmail_ID & "','" & VTinNo & "','" & VCinNo & "','" & VGSTNo & "','" & VStatus & "','" & vClientPassword & "','" & VLastLogin & "','" & VClientRole & "','" & VIsNewDatabase & "','" & VDatabaseName & "','" & VChangeTheme & "',getdate(),'" & VEmpCode & "','" & VUpdatedBy & "',getdate() );"


                            'Companylogo    BOS_ClientRegistration  CompanyCode


                            'Dim qry_Rights As String = "select * from NidhiSoftware_Admin_Module_Master order by RID asc"
                            'DS = New DataSet
                            'DS = GV.FL.OpenDsWithSelectQuery(qry_Rights)
                            'If DS.Tables(0).Rows.Count > 0 Then
                            '    For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                            '        If Not IsDBNull(DS.Tables(0).Rows(i).Item("FormName")) Then
                            '            If Not DS.Tables(0).Rows(i).Item("FormName").ToString() = "" Then
                            '                lblFormName.Text = GV.parseString(DS.Tables(0).Rows(i).Item("FormName").ToString())

                            '            Else
                            '                lblFormName.Text = ""
                            '            End If
                            '        Else
                            '            lblFormName.Text = ""
                            '        End If
                            '        If Not IsDBNull(DS.Tables(0).Rows(i).Item("RefModule")) Then
                            '            If Not DS.Tables(0).Rows(i).Item("RefModule").ToString() = "" Then
                            '                lblRefModule.Text = GV.parseString(DS.Tables(0).Rows(i).Item("RefModule").ToString())

                            '            Else
                            '                lblRefModule.Text = ""
                            '            End If
                            '        Else
                            '            lblRefModule.Text = ""
                            '        End If

                            '        If Not IsDBNull(DS.Tables(0).Rows(i).Item("RefSubModule")) Then
                            '            If Not DS.Tables(0).Rows(i).Item("RefSubModule").ToString() = "" Then
                            '                lblRefSubModule.Text = GV.parseString(DS.Tables(0).Rows(i).Item("RefSubModule").ToString())

                            '            Else
                            '                lblRefSubModule.Text = ""
                            '            End If
                            '        Else
                            '            lblRefSubModule.Text = ""
                            '        End If

                            '        If Not IsDBNull(DS.Tables(0).Rows(i).Item("NavigationModule")) Then
                            '            If Not DS.Tables(0).Rows(i).Item("NavigationModule").ToString() = "" Then
                            '                lblNavigationModule.Text = GV.parseString(DS.Tables(0).Rows(i).Item("NavigationModule").ToString())

                            '            Else
                            '                lblNavigationModule.Text = ""
                            '            End If
                            '        Else
                            '            lblNavigationModule.Text = ""
                            '        End If
                            '        If Not IsDBNull(DS.Tables(0).Rows(i).Item("OrderNo")) Then
                            '            If Not DS.Tables(0).Rows(i).Item("OrderNo").ToString() = "" Then
                            '                lblOrderNo.Text = GV.parseString(DS.Tables(0).Rows(i).Item("OrderNo").ToString())

                            '            Else
                            '                lblOrderNo.Text = ""
                            '            End If
                            '        Else
                            '            lblOrderNo.Text = ""
                            '        End If
                            '        'If Not IsDBNull(DS.Tables(0).Rows(i).Item("RefSubModule_Order")) Then
                            '        '    If Not DS.Tables(0).Rows(i).Item("RefSubModule_Order").ToString() = "" Then
                            '        '        lblRefSubModule_Order.Text = GV.parseString(DS.Tables(0).Rows(i).Item("RefSubModule_Order").ToString())

                            '        '    Else
                            '        '        lblRefSubModule_Order.Text = ""
                            '        '    End If
                            '        'Else
                            '        '    lblRefSubModule_Order.Text = ""
                            '        'End If
                            '        'If Not IsDBNull(DS.Tables(0).Rows(i).Item("NavigationModule_Order")) Then
                            '        '    If Not DS.Tables(0).Rows(i).Item("NavigationModule_Order").ToString() = "" Then
                            '        '        lblNavigationModule_Order.Text = GV.parseString(DS.Tables(0).Rows(i).Item("NavigationModule_Order").ToString())
                            '        '    Else
                            '        '        lblNavigationModule_Order.Text = ""
                            '        '    End If
                            '        'Else
                            '        '    lblNavigationModule_Order.Text = ""
                            '        'End If
                            '        Dim VRefSubModule_Order, VRefNavigationModule_Order As String
                            '        VRefSubModule_Order = " ( select distinct RefSubModule_Order from NidhiSoftware_Admin_SubModule where ModuleName='" & GV.parseString(lblRefModule.Text.Trim) & "' and  RefSubModule='" & GV.parseString(lblRefSubModule.Text.Trim) & "')"
                            '        VRefNavigationModule_Order = " ( select distinct NavigationModule_Order from NidhiSoftware_Admin_Module_Master where RefModule='" & GV.parseString(lblRefModule.Text.Trim) & "' and  RefSubModule='" & GV.parseString(lblRefSubModule.Text.Trim) & "' and NavigationModule='" & GV.parseString(lblNavigationModule.Text.Trim) & "' )"

                            '        If qry1 = "" Then
                            '            qry1 = "insert into NidhiSoftware_Admin_UserRightsMaster(OrderNO,RefSubModule_Order,NavigationModule_Order,CompanyCode,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy) values((select orderno from NidhiSoftware_Admin_MainModule where ModuleName='" & GV.parseString(lblRefModule.Text.Trim) & "')," & VRefSubModule_Order & "," & VRefNavigationModule_Order & ",'" & VCompanyCode & "','1','" & GV.parseString(lblFormName.Text.Trim) & "','True','True','True','True','" & GV.parseString(lblRefModule.Text.Trim) & "','" & GV.parseString(lblRefSubModule.Text.Trim) & "','" & GV.parseString(lblNavigationModule.Text.Trim) & "',getDate(),'" & get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "') ;"
                            '        Else
                            '            qry1 = qry1 & "insert into NidhiSoftware_Admin_UserRightsMaster(OrderNO,RefSubModule_Order,NavigationModule_Order,CompanyCode,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy) values((select orderno from NidhiSoftware_Admin_MainModule where ModuleName='" & GV.parseString(lblRefModule.Text.Trim) & "')," & VRefSubModule_Order & "," & VRefNavigationModule_Order & ",'" & VCompanyCode & "','1','" & GV.parseString(lblFormName.Text.Trim) & "','True','True','True','True','" & GV.parseString(lblRefModule.Text.Trim) & "','" & GV.parseString(lblRefSubModule.Text.Trim) & "','" & GV.parseString(lblNavigationModule.Text.Trim) & "',getDate(),'" & get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "') ;"
                            '        End If



                            '        ' GV.FL.DMLQueriesBulk(qry1)
                            '    Next
                            'End If


                            'qry1 = " insert into NidhiSoftware_Admin_UserRightsMaster(FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,OrderNo,UpdatedOn,UpdatedBy,RefSubModule_Order,NavigationModule_Order,User_Group,CompanyCode) select '1',FormName,'1','1','1','1',RefModule,RefSubModule,NavigationModule,OrderNo,getdate(),'" & VUpdatedBy & "',RefSubModule_Order,NavigationModule_Order,'Admin','" & VCompanyCode & "' from NidhiSoftware_Admin_Module_Master;"
                            qry = qry & qry1
                            'qry = qry & Qry_CountryMaster & Qry_StateMaster & Qry_DistrictMaster & qry1
                            '////////////////////////////////////   End Qry For Current DB  ///////////////////////////////////////////////////////////////////////////////////////////////////////
                        ElseIf ddlCreateDatabase.SelectedValue.Trim.ToUpper = "Yes".ToUpper Then

                            'sana1
                            ', CreditBalnceLimit='" & VCreditBalLimit & "'


                            Dim ClientPin As String = "1234"

                            qry = " insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration (Encrypted_Pass,HoldAmt,AEPS_API_Status,RechargeAPI_Status,MoneyTransferAPI_Status,PANCardAPI_Status,CreditBalnceLimit,ClientPin,WebRedirectUrl,RegisterationDate,CompanyCode,CompanyName,CompanyHead,ContactPerson,Companylogo,Address_1,Address_2,Address_3,PinCode,State,District,City,Country,PhoneNo_1,PhoneNo_2,Mobile_No,Email_ID,TinNo,CinNo,GSTNo,Status,ClientPassword,LastLogin,ClientRole,IsNewDatabase,DatabaseName,ChangeTheme,Record_DateTime,EmpCode,UpdatedBy,UpdatedOn) values('Null',0,'" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "', '" & VCreditBalLimit & "', '" & ClientPin & "', '" & VWebRedirectUrl & "', '" & VRegisterationDate & "','" & VCompanyCode & "','" & VCompanyName & "','" & VCompanyHead & "','" & VContactPerson & "','" & VCompanylogo & "','" & VAddress_1 & "','" & VAddress_2 & "','" & VAddress_3 & "','" & VPinCode & "','" & VState & "','" & VDistrict & "','" & VCity & "','" & VCountry & "','" & VPhoneNo_1 & "','" & VPhoneNo_2 & "','" & VMobile_No & "','" & VEmail_ID & "','" & VTinNo & "','" & VCinNo & "','" & VGSTNo & "','" & VStatus & "','" & vClientPassword & "','" & VLastLogin & "','" & VClientRole & "','" & VIsNewDatabase & "','" & VDatabaseName & "','" & VChangeTheme & "',getdate(),'" & VEmpCode & "','" & VUpdatedBy & "',getdate() );"
                            qry = qry & "  " & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_ClientRegistration (Encrypted_Pass,HoldAmt,AEPS_API_Status,RechargeAPI_Status,MoneyTransferAPI_Status,PANCardAPI_Status,CreditBalnceLimit,ClientPin,WebRedirectUrl,RegisterationDate,CompanyCode,CompanyName,CompanyHead,ContactPerson,Companylogo,Address_1,Address_2,Address_3,PinCode,State,District,City,Country,PhoneNo_1,PhoneNo_2,Mobile_No,Email_ID,TinNo,CinNo,GSTNo,Status,ClientPassword,LastLogin,ClientRole,IsNewDatabase,DatabaseName,ChangeTheme,Record_DateTime,EmpCode,UpdatedBy,UpdatedOn) values('Null',0,'" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "', '" & VCreditBalLimit & "','" & ClientPin & "','" & VWebRedirectUrl & "', '" & VRegisterationDate & "','" & VCompanyCode & "','" & VCompanyName & "','" & VCompanyHead & "','" & VContactPerson & "','" & VCompanylogo & "','" & VAddress_1 & "','" & VAddress_2 & "','" & VAddress_3 & "','" & VPinCode & "','" & VState & "','" & VDistrict & "','" & VCity & "','" & VCountry & "','" & VPhoneNo_1 & "','" & VPhoneNo_2 & "','" & VMobile_No & "','" & VEmail_ID & "','" & VTinNo & "','" & VCinNo & "','" & VGSTNo & "','" & VStatus & "','" & vClientPassword & "','" & VLastLogin & "','" & VClientRole & "','" & VIsNewDatabase & "','" & VDatabaseName & "','" & VChangeTheme & "',getdate(),'" & VEmpCode & "','" & VUpdatedBy & "',getdate() );"

                            qry = qry & "  insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_Login_Details (Encrypted_Pass,User_Name,User_ID,User_Password,User_Type,AccountStatus,User_CreationDate,LoggedinStatus,RecordStatus,UpdatedOn,UpdatedBy,Fromtime,Totime,ImagePath,TotalTime,LastLoginTime,EmailId,MobileNo,ChangeTheme,Profile,Designation,TargetAmuont,MDPassword,EmpSkill,EmpType,Canlogin,CreditBalnceLimit,TransactionPin,CompanyCode) values('Null', '" & VCompanyName & "','Admin','" & vClientPassword & "','Admin','Active',getdate(),'No','Active',getdate(),'Super Admin','00:01-AM','11:59-PM','" & VCompanylogo & "',NULL,NULL,'" & VEmail_ID & "','" & VMobile_No & "','" & VChangeTheme & "',NULL,NULL,NULL,NULL,NULL,'Back Office Employee','Yes','" & VCreditBalLimit & "','" & ClientPin & "','" & VCompanyCode & "' );"
                            qry1 = "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_Module_Master (FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,OrderNo,UpdatedOn,UpdatedBy,RefSubModule_Order,NavigationModule_Order) select FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,OrderNo,UpdatedOn,UpdatedBy,RefSubModule_Order,NavigationModule_Order from " & GV.DefaultDatabase.Trim & ".dbo.CRM_Module_Master  ;"

                            qry = qry & qry1
                            'insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_Login_Details (User_Name,User_ID,User_Password,User_Type,AccountStatus,User_CreationDate,LoggedinStatus,RecordStatus,UpdatedOn,UpdatedBy,Fromtime,Totime,ImagePath,TotalTime,LastLoginTime,EmailId,MobileNo,ChangeTheme,Profile,Designation,TargetAmuont,MDPassword,EmpSkill,EmpType,Canlogin,CreditBalnceLimit,TransactionPin,CompanyCode) values( '" & VUser_Name & "','" & VUser_ID & "','" & VUser_Password & "','" & VUser_Type & "','" & VAccountStatus & "','" & VUser_CreationDate & "','" & VLoggedinStatus & "','" & VRecordStatus & "','" & VUpdatedOn & "','" & VUpdatedBy & "','" & VFromtime & "','" & VTotime & "','" & VImagePath & "'," & VTotalTime & ",'" & VLastLoginTime & "','" & VEmailId & "','" & VMobileNo & "','" & VChangeTheme & "','" & VProfile & "','" & VDesignation & "'," & VTargetAmuont & ",'" & VMDPassword & "','" & VEmpSkill & "','" & VEmpType & "','" & VCanlogin & "'," & VCreditBalnceLimit & ",'" & VTransactionPin & "','" & VCompanyCode & "' )

                            Dim Qry_NewDB As String = ""
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_CountryMaster (Country_Name,UpdatedBy,UpdatedOn) select Country_Name,UpdatedBy,UpdatedOn  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_CountryMaster  ;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_DistrictMaster (Country_Name,State_Name,District_Name,UpdatedBy,UpdatedOn) select Country_Name,State_Name,District_Name,UpdatedBy,UpdatedOn  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_DistrictMaster ; "
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_StateMaster (Country_Name,State_Name,UpdatedBy,UpdatedOn) select Country_Name,State_Name,UpdatedBy,UpdatedOn  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_StateMaster  ;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_Admin_BankAccount_Master (AccountHolder_Name,AccountNo,IFSC_Code,Bank_Name,BranchName,AccountType,PanNo,UpdatedBy,UpdatedOn,CompanyCode) select AccountHolder_Name,AccountNo,IFSC_Code,Bank_Name,BranchName,AccountType,PanNo,UpdatedBy,UpdatedOn,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Admin_BankAccount_Master;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_CommissionSlabwise (APIName,FromAmount,ToAmount,Dis_CommissionType,Dis_Commission,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,UpdatedBy,UpdatedOn,CompanyCode) select APIName,FromAmount,ToAmount,Dis_CommissionType,Dis_Commission,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,UpdatedBy,UpdatedOn,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_ComplaintVSProblem_Master (Product,ProductProblem,UpdatedBy,UpdatedOn,CompanyCode) select Product,ProductProblem,UpdatedBy,UpdatedOn,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ComplaintVSProblem_Master;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_OperatorWiseCommission (APIName,Category,Code,OperatorName,Dis_CommissionType,Dis_Commission,UpdatedBy,UpdatedOn,ActiveStatus,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,CompanyCode) select APIName,Category,Code,OperatorName,Dis_CommissionType,Dis_Commission,UpdatedBy,UpdatedOn,ActiveStatus,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_ProductServiceMaster (Title,Commission,ActiveStatus,UpdatedBy,UpdatedOn,ProductType,ContainCategory,CanChange,CommissionType,ServiceType,ServiceCharge,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,SlabApplicable,CompanyCode) select Title,Commission,ActiveStatus,UpdatedBy,UpdatedOn,ProductType,ContainCategory,CanChange,CommissionType,ServiceType,ServiceCharge,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,SlabApplicable,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_EmployeeTypeMaster (Employee_Type,UpdatedBy,UpdatedOn,CompanyCode) select Employee_Type,UpdatedBy,UpdatedOn,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_EmployeeTypeMaster;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_Group_Master (Group_Name,UpdatedBy,UpdatedOn,CompanyCode) select Group_Name,UpdatedBy,UpdatedOn,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_Group_Master;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_MainModule (ModuleName,OrderNO,UpdatedOn,UpdatedBy,Choice,url,CreateLink,FormName,Searching_Keyword,CompanyCode) select ModuleName,OrderNO,UpdatedOn,UpdatedBy,Choice,url,CreateLink,FormName,Searching_Keyword,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_MainModule;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_SubModule (ModuleName,UpdatedOn,UpdatedBy,OrderNO,RefSubModule,RefSubModule_Order,url,Choice,CreateLink,FormName,Searching_Keyword,CompanyCode) select ModuleName,UpdatedOn,UpdatedBy,OrderNO,RefSubModule,RefSubModule_Order,url,Choice,CreateLink,FormName,Searching_Keyword,'" & VCompanyCode & "' from " & GV.DefaultDatabase.Trim & ".dbo.CRM_SubModule;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.CRM_UserRightsMaster (User_Group,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy,OrderNo,NavigationModule_Order,RefSubModule_Order,Choice,url,CreateLinkModule,CreateLinkSubModule,CreateLink,Searching_Keyword,CompanyCode) select User_Group,FrmSelected,FormName,CanSave,CanSearch,CanUpdate,CanDelete,RefModule,RefSubModule,NavigationModule,UpdatedOn,UpdatedBy,OrderNo,NavigationModule_Order,RefSubModule_Order,Choice,url,CreateLinkModule,CreateLinkSubModule,CreateLink,Searching_Keyword,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.CRM_UserRightsMaster ;" ' where User_Group in ('Admin','Distributor','Retailer','Sub Distributor') and FrmSelected='1'
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.BOS_APIVSCategory_Master (ProductService,Category,UpdatedBy,UpdatedOn,CanChange,CompanyCode) select ProductService,Category,UpdatedBy,UpdatedOn,CanChange,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.AutoNumber (Customer_Prefix,CustomerID,ClientID,Client_Prefix,SessionId,ReceiptId,VoucherNo,ProjectID,Project_Prefix,EmployeeId,Employee_Prefix,WeekOff,MsgSendDate,ItemType_ID,CallRefrenceNo,PlanCode,DistributorID_Prefix,Distributor_ID,ReturnID,ListID,ProductId,DistributorID,Distributor_Prefix,SubDistributorID,SubDistributor_Prefix,RetailerId,Retailer_Prefix,SMSAPI,SMSSenderId,ComplaintPrefix,ComplaintId,Recharge_APIPer,Flight_APIPer,PAN_APIPer,MoneyTransfer_APIPer,GST_APIPer,BusBooking_APIPer,Rail_APIPer,TransId,RefrenceID,ServiceCharge,Amt_Transfer_TransID,RechargeAPI_Status,PANCardAPI_Status,MoneyTransferAPI_Status,AEPS_API_Status,CompanyCode_Prefix,CompanyCode) select Customer_Prefix,CustomerID,ClientID,Client_Prefix,SessionId,ReceiptId,VoucherNo,ProjectID,Project_Prefix,EmployeeId,Employee_Prefix,WeekOff,MsgSendDate,ItemType_ID,CallRefrenceNo,PlanCode,DistributorID_Prefix,Distributor_ID,ReturnID,ListID,ProductId,DistributorID,Distributor_Prefix,SubDistributorID,SubDistributor_Prefix,RetailerId,Retailer_Prefix,SMSAPI,SMSSenderId,ComplaintPrefix,ComplaintId,Recharge_APIPer,Flight_APIPer,PAN_APIPer,MoneyTransfer_APIPer,GST_APIPer,BusBooking_APIPer,Rail_APIPer,TransId,RefrenceID,ServiceCharge,Amt_Transfer_TransID,RechargeAPI_Status,PANCardAPI_Status,MoneyTransferAPI_Status,AEPS_API_Status,CompanyCode_Prefix,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.AutoNumber;"
                            Qry_NewDB = Qry_NewDB & "insert into " & GV.parseString(txtCompanyCode.Text) & ".dbo.MoneyTransferBankList (Name,Code,BankID,NEFT,IMPS,Verification,CompanyCode) select Name,Code,BankID,NEFT,IMPS,Verification,'" & VCompanyCode & "'  from " & GV.DefaultDatabase.Trim & ".dbo.MoneyTransferBankList;"

                            qry = qry & Qry_NewDB
                        End If



                        Dim result As Boolean = GV.FL.DMLQuery_withCommand(qry)
                        If result = True Then
                            lblSavingDialogBox.Visible = False
                            Div_deInfo.Visible = True
                            lblPassword.Text = " " & GV.DecryptString(GV.key, vClientPassword)
                            lblClientID.Text = " " & VCompanyCode
                            lblConfrmMsg.Text = "Client Registered Successfully."
                            lblConfrmMsg.CssClass = "Successlabels"
                            lblpendingaproval.Text = " "
                            lblpendingaproval.CssClass = ""
                            btnsaveOk.Text = "Ok"
                            'btnCancel.Visible = False
                            BtnSaveCancel.Attributes("style") = "display:none"
                            'Clear()
                            ModalPopupExtender2.Show()

                        Else
                            lblClientID.Visible = False
                            lblConfrmMsg.Visible = False
                            lblpendingaproval.Visible = False
                            lblSavingDialogBox.Text = "Sorry !! Client Registration Failed."
                            lblSavingDialogBox.CssClass = "errorlabels"
                            btnsaveOk.Visible = False
                            ModalPopupExtender2.Show()

                        End If

                        'UpdateProgress1.Visible = False
                    End If
                ElseIf lblSessionFlag.Text = 1 Then

                    '  Dim result As Boolean = GV.FL.DMLQueries("delete from BOS_ClientRegistration where RID=" & lblRID.Text & "")


                    Dim qry As String = "" ' "update BOS_ClientRegistration set HO_BranchCode='" & VBranchCode & "',HO_BranchName='" & VBranchName & "', CinNo='" & VCinNo & "',ApprovedStatus='Pending', Status='" & GV.parseString(ddlAccountStatus.SelectedValue) & "' ,ClientPassword='" & GV.parseString(txtPassword.Text.Trim) & "',  GracePeriodInDays='" & VGracePeriodInDays & "' , CompanyType='" & VCompanyType & "', CompanyCode='" & VCompanyCode & "', CompanyName='" & VCompanyName & "', CompanyHead='" & VCompanyHead & "', ContactPerson='" & VContactPerson & "', Companylogo='" & VCompanylogo & "', Address_1='" & VAddress_1 & "', Address_2='" & VAddress_2 & "', Address_3='" & VAddress_3 & "', PinCode='" & VPinCode & "', State='" & VState & "', City='" & VCity & "', District='" & VDistrict & "' , Country='" & VCountry & "', PhoneNo_1='" & VPhoneNo_1 & "', PhoneNo_2='" & VPhoneNo_2 & "', Mobile_No='" & VMobile_No & "', Email_ID='" & VEmail_ID & "', FaxNo='" & VFaxNo & "', TinNo='" & VTinNo & "', PlanCode='" & VPlanCode & "', PlanName='" & VPlanName & "', BillingCycle='" & VBillingCycle & "', BilingAmount=" & VBilingAmount & ", PlanStartDate='" & VPlanStartDate & "', NextBillingDate='" & VNextBillingDate & "', NextBillingDateWithGracePeriod='" & VNextBillingDateWithGracePeriod & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where RID=" & lblSessionRID.Text & ";"
                    If GV.parseString(ddlCreateDatabase.SelectedValue) = "Yes" Then
                        qry = " update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration set Encrypted_Pass='Null',  CreditBalnceLimit='" & VCreditBalLimit & "',  WebRedirectUrl='" & VWebRedirectUrl & "', CompanyName='" & VCompanyName & "', CompanyHead='" & VCompanyHead & "', ContactPerson='" & VContactPerson & "', Companylogo='" & VCompanylogo & "', Address_1='" & VAddress_1 & "', Address_2='" & VAddress_2 & "', Address_3='" & VAddress_3 & "', PinCode='" & VPinCode & "', State='" & VState & "', District='" & VDistrict & "', City='" & VCity & "', Country='" & VCountry & "', PhoneNo_1='" & VPhoneNo_1 & "', PhoneNo_2='" & VPhoneNo_2 & "', Mobile_No='" & VMobile_No & "', Email_ID='" & VEmail_ID & "', TinNo='" & VTinNo & "', CinNo='" & VCinNo & "', GSTNo='" & VGSTNo & "', Status='" & VStatus & "', ClientPassword='" & vClientPassword & "', ClientPin='" & GV.parseString(txtTransactionPin.Text) & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=getdate() where CompanyCode='" & GV.parseString(txtCompanyCode.Text) & "';"
                        qry = qry & " update " & GV.parseString(lblDatabaseName.Text) & ".dbo.BOS_ClientRegistration set Encrypted_Pass='Null',  CreditBalnceLimit='" & VCreditBalLimit & "',  WebRedirectUrl='" & VWebRedirectUrl & "', CompanyName='" & VCompanyName & "', CompanyHead='" & VCompanyHead & "', ContactPerson='" & VContactPerson & "', Companylogo='" & VCompanylogo & "', Address_1='" & VAddress_1 & "', Address_2='" & VAddress_2 & "', Address_3='" & VAddress_3 & "', PinCode='" & VPinCode & "', State='" & VState & "', District='" & VDistrict & "', City='" & VCity & "', Country='" & VCountry & "', PhoneNo_1='" & VPhoneNo_1 & "', PhoneNo_2='" & VPhoneNo_2 & "', Mobile_No='" & VMobile_No & "', Email_ID='" & VEmail_ID & "', TinNo='" & VTinNo & "', CinNo='" & VCinNo & "', GSTNo='" & VGSTNo & "', Status='" & VStatus & "', ClientPin='" & GV.parseString(txtTransactionPin.Text) & "',ClientPassword='" & vClientPassword & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=getdate() where CompanyCode='" & GV.parseString(txtCompanyCode.Text) & "';"
                        qry = qry & " update " & GV.parseString(lblDatabaseName.Text) & ".dbo.CRM_Login_Details set Encrypted_Pass='Null',  CreditBalnceLimit='" & VCreditBalLimit & "',  AccountStatus='" & VStatus & "', User_Name='" & VCompanyName & "', User_Password='" & vClientPassword & "',TransactionPin='" & GV.parseString(txtTransactionPin.Text) & "',  ImagePath='" & VCompanylogo & "', MobileNo='" & VMobile_No & "', EmailId='" & VEmail_ID & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=getdate() where User_ID='Admin';"
                    Else
                        qry = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration set Encrypted_Pass='Null',  CreditBalnceLimit='" & VCreditBalLimit & "',  WebRedirectUrl='" & VWebRedirectUrl & "', CompanyName='" & VCompanyName & "', CompanyHead='" & VCompanyHead & "', ContactPerson='" & VContactPerson & "', Companylogo='" & VCompanylogo & "', Address_1='" & VAddress_1 & "', Address_2='" & VAddress_2 & "', Address_3='" & VAddress_3 & "', PinCode='" & VPinCode & "', State='" & VState & "', District='" & VDistrict & "', City='" & VCity & "', Country='" & VCountry & "', PhoneNo_1='" & VPhoneNo_1 & "', PhoneNo_2='" & VPhoneNo_2 & "', Mobile_No='" & VMobile_No & "', Email_ID='" & VEmail_ID & "', TinNo='" & VTinNo & "', CinNo='" & VCinNo & "', GSTNo='" & VGSTNo & "', Status='" & VStatus & "', ClientPassword='" & vClientPassword & "',ClientPin='" & GV.parseString(txtTransactionPin.Text) & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=getdate() where CompanyCode='" & GV.parseString(txtCompanyCode.Text) & "';"
                        If GV.parseString(lblDatabaseName.Text).ToUpper = "BosCenter_DB".ToUpper Then
                            qry = qry & " update " & GV.parseString(lblDatabaseName.Text) & ".dbo.BOS_ClientRegistration set Encrypted_Pass='Null',  CreditBalnceLimit='" & VCreditBalLimit & "',  WebRedirectUrl='" & VWebRedirectUrl & "', CompanyName='" & VCompanyName & "', CompanyHead='" & VCompanyHead & "', ContactPerson='" & VContactPerson & "', Companylogo='" & VCompanylogo & "', Address_1='" & VAddress_1 & "', Address_2='" & VAddress_2 & "', Address_3='" & VAddress_3 & "', PinCode='" & VPinCode & "', State='" & VState & "', District='" & VDistrict & "', City='" & VCity & "', Country='" & VCountry & "', PhoneNo_1='" & VPhoneNo_1 & "', PhoneNo_2='" & VPhoneNo_2 & "', Mobile_No='" & VMobile_No & "', Email_ID='" & VEmail_ID & "', TinNo='" & VTinNo & "', CinNo='" & VCinNo & "', GSTNo='" & VGSTNo & "', Status='" & VStatus & "', ClientPin='" & GV.parseString(txtTransactionPin.Text) & "',ClientPassword='" & vClientPassword & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=getdate() where CompanyCode='" & GV.parseString(txtCompanyCode.Text) & "';"
                            qry = qry & " update " & GV.parseString(lblDatabaseName.Text) & ".dbo.CRM_Login_Details set Encrypted_Pass='Null',  CreditBalnceLimit='" & VCreditBalLimit & "',  AccountStatus='" & VStatus & "', User_Name='" & VCompanyName & "', User_Password='" & vClientPassword & "',TransactionPin='" & GV.parseString(txtTransactionPin.Text) & "',  ImagePath='" & VCompanylogo & "', MobileNo='" & VMobile_No & "', EmailId='" & VEmail_ID & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=getdate() where User_ID='Admin';"
                        End If
                    End If
                    Dim result As Boolean
                    result = GV.FL.DMLQuery_withCommand(qry)

                    lblDialogMsg.Text = result
                    If result = True Then

                        lblSavingDialogBox.Visible = False
                        Div_deInfo.Visible = False
                        lblConfrmMsg.Text = "Client Updated Successfully."
                        lblConfrmMsg.CssClass = "Successlabels"
                        lblpendingaproval.Text = ""
                        lblpendingaproval.CssClass = ""


                        btnsaveOk.Text = "Ok"
                        BtnSaveCancel.Attributes("style") = "display:none"
                        ModalPopupExtender2.Show()

                    Else
                        'lblDialogMsg.Text = "Sorry !! Record Updation Failed."
                        'lblDialogMsg.CssClass = "errorlabels"
                        'ModalPopupExtender1.Show()
                        'btnOk.Visible = False

                        lblClientID.Visible = False
                        lblClientID.Visible = False
                        lblConfrmMsg.Visible = False
                        lblpendingaproval.Visible = False
                        lblSavingDialogBox.Text = "Sorry !! Client Details Update Failed."
                        lblSavingDialogBox.CssClass = "errorlabels"
                        btnsaveOk.Visible = False
                        BtnSaveCancel.Text = "Ok"
                        ModalPopupExtender2.Show()


                    End If

                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCountry.SelectedIndexChanged
        Try
            If ddlCountry.SelectedValue = ":::: Select Country ::::" Then
                ddlState.Items.Clear()
                ddlState.Items.Add(":::: Select State ::::")
                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add(":::: Select District ::::")
            Else
                GV.FL.AddInDropDownListDistinct(ddlState, "State_Name", "CRM_StateMaster where Country_Name='" & ddlCountry.SelectedValue & "'")
                If ddlState.Items.Count > 0 Then
                    ddlState.Items.Insert(0, ":::: Select State ::::")
                Else
                    ddlState.Items.Add(":::: Select State ::::")
                End If
                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add(":::: Select District ::::")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CreateNewData()
        Try
            Dim qry As String = "CREATE DATABASE " & GV.parseString(txtCompanyCode.Text) & ""
            GV.FL.OpenDsWithSelectQuery(qry)

            Dim FullQuery As String = GV.FL.GetFileContents(Server.MapPath("Admin_DatabaseScript.txt"))

            qry = ""
            qry = "use " & GV.parseString(txtCompanyCode.Text) & ";"
            qry = qry & FullQuery
            qry = qry.Replace(""" & vbCrLf & """, "")
            'txtAddress_1.Text = qry

            GV.FL.DMLQueriesBulk(qry)

        Catch ex As Exception
        End Try
    End Sub
    Public Sub CreateNewDataAlter()
        Try
            Dim qry As String
            'Dim qry As String = "CREATE DATABASE " & GV.parseString(txtCompanyCode.Text) & ""
            'Dim DatabaseDS As DataSet = New DataSet
            'DatabaseDS = GV.FL.OpenDsWithSelectQuery(qry)

            Dim FullQuery As String = GV.FL.GetFileContents(Server.MapPath("Admin_DatabaseScriptAlterTable.txt"))

            qry = ""
            qry = "use " & GV.parseString(txtCompanyCode.Text) & ";"
            qry = qry & FullQuery
            'qry = qry.Replace(""" & vbCrLf & """, "")
            'txtAddress_1.Text = qry
            GV.FL.DMLQueriesBulk(qry)

        Catch ex As Exception
        End Try
    End Sub


End Class