Public Class Frm_IT_Return
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VITRETURNFor, VApplicantLastName, VApplicantFirstName, VApplicantMiddleName, VMobileNumber, VEmail, VNAMEOFENTERPRISES As String

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
                ddlITRETURNFor.CssClass = "form-control"
                txtApplicantLastName.CssClass = "form-control"
                txtApplicantFirstName.CssClass = "form-control"
                txtApplicantMiddleName.CssClass = "form-control"
                txtMobileNumber.CssClass = "form-control"
                txtEmail.CssClass = "form-control"
                txtNAMEOFENTERPRISES.CssClass = "form-control"

                VITRETURNFor = ""
                VApplicantLastName = ""
                VApplicantFirstName = ""
                VApplicantMiddleName = ""
                VMobileNumber = ""
                VEmail = ""
                VNAMEOFENTERPRISES = ""
                lblError.Text = ""
                lblError.CssClass = ""
                If ddlITRETURNFor.Items.Count > 0 Then
                    ddlITRETURNFor.SelectedIndex = 0
                End If

                txtApplicantLastName.Text = ""

                txtApplicantFirstName.Text = ""

                txtApplicantMiddleName.Text = ""

                txtMobileNumber.Text = ""

                txtEmail.Text = ""

                txtNAMEOFENTERPRISES.Text = ""

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

            '//// code by nidhi start 8 july 22

            If GV.parseString(ddlITRETURNFor.SelectedValue) = "" Then
                ddlITRETURNFor.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    ddlITRETURNFor.Focus()
                    isFocusApplied = True
                End If
            Else
                ddlITRETURNFor.CssClass = "form-control"
                VITRETURNFor = GV.parseString(ddlITRETURNFor.SelectedValue)
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

                If isFocusApplied = False Then
                    txtMobileNumber.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not GV.parseString(txtMobileNumber.Text).Length = 10 Then
                txtMobileNumber.CssClass = "ValidationError"
                isErrorFound = True

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

            If GV.parseString(txtNAMEOFENTERPRISES.Text) = "" Then
                txtNAMEOFENTERPRISES.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtNAMEOFENTERPRISES.Focus()
                    isFocusApplied = True
                End If
            Else
                txtNAMEOFENTERPRISES.CssClass = "form-control"
                VNAMEOFENTERPRISES = GV.parseString(txtNAMEOFENTERPRISES.Text)
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

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_IT_Return where RID=" & lblRID.Text.Trim & ""
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
            If ddlITRETURNFor.Items.Count > 0 Then
                If Not ddlITRETURNFor.SelectedValue.Trim = "" Then
                    VITRETURNFor = GV.parseString(ddlITRETURNFor.SelectedValue.Trim)
                Else
                    VITRETURNFor = ""
                End If
            End If

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

            If Not txtNAMEOFENTERPRISES.Text.Trim = "" Then
                VNAMEOFENTERPRISES = GV.parseString(txtNAMEOFENTERPRISES.Text.Trim)
            Else
                VNAMEOFENTERPRISES = ""
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

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_IT_Return Where RID=" & lblRID.Text.Trim & " ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_IT_Return (RecordDateTime, EntryBy,ITRETURNFor,ApplicantLastName,ApplicantFirstName,ApplicantMiddleName,MobileNumber,Email,NAMEOFENTERPRISES) values('" & VRecordDateTime & "','" & VEntryBy & "', '" & VITRETURNFor & "','" & VApplicantLastName & "','" & VApplicantFirstName & "','" & VApplicantMiddleName & "','" & VMobileNumber & "','" & VEmail & "','" & VNAMEOFENTERPRISES & "' )"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_IT_Return set UpdatedOn='" & VUpdatedOn & "', UpdatedBy='" & VUpdatedBy & "', ITRETURNFor='" & VITRETURNFor & "', ApplicantLastName='" & VApplicantLastName & "', ApplicantFirstName='" & VApplicantFirstName & "', ApplicantMiddleName='" & VApplicantMiddleName & "', MobileNumber='" & VMobileNumber & "', Email='" & VEmail & "', NAMEOFENTERPRISES='" & VNAMEOFENTERPRISES & "' where RID=" & lblRID.Text.Trim & ""
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
            VITRETURNFor = ""
            VApplicantLastName = ""
            VApplicantFirstName = ""
            VApplicantMiddleName = ""
            VMobileNumber = ""
            VEmail = ""
            VNAMEOFENTERPRISES = ""
            If ddlITRETURNFor.Items.Count > 0 Then
                ddlITRETURNFor.SelectedIndex = 0
            End If

            txtApplicantLastName.Text = ""

            txtApplicantFirstName.Text = ""

            txtApplicantMiddleName.Text = ""

            txtMobileNumber.Text = ""

            txtEmail.Text = ""

            txtNAMEOFENTERPRISES.Text = ""

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
                    formheading_1.Text = "Edit IT RETURN "
                    DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_IT_Return where RID='" & Session("RecordID") & "'")
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If ddlITRETURNFor.Items.Count > 0 Then
                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("ITRETURNFor")) Then
                                    If Not DS.Tables(0).Rows(0).Item("ITRETURNFor").ToString() = "" Then
                                        ddlITRETURNFor.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ITRETURNFor").ToString())
                                    Else
                                        ddlITRETURNFor.SelectedIndex = 0
                                    End If
                                Else
                                    ddlITRETURNFor.SelectedIndex = 0
                                End If
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

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("NAMEOFENTERPRISES")) Then
                                If Not DS.Tables(0).Rows(0).Item("NAMEOFENTERPRISES").ToString() = "" Then
                                    txtNAMEOFENTERPRISES.Text = GV.parseString(DS.Tables(0).Rows(0).Item("NAMEOFENTERPRISES").ToString())
                                Else
                                    txtNAMEOFENTERPRISES.Text = ""
                                End If
                            Else
                                txtNAMEOFENTERPRISES.Text = ""
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