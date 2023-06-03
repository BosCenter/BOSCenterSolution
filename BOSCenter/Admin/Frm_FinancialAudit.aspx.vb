Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class Frm_FinancialAudit
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")

    Dim Name, MobileNo, Annualturnover, Address, District, State, Pin As String

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                lblError.Text = ""
                lblError.CssClass = ""

                GV.FL.AddInDropDownListDistinct(ddlState_UnionTerritory, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response) & ".dbo.CRM_StateMaster Where Country_Name='INDIA'")
                GV.FL.AddInDropDownListDistinct(ddlDistrict, "District_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response) & ".dbo.CRM_DistrictMaster where Country_Name='INDIA' ")
                ddlState_UnionTerritory.SelectedValue = "DELHI"
                ddlDistrict.SelectedValue = "NORTH DELHI"

                Session("workfor") = 0
                Session("EditFlag") = "Save"

            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            If GV.parseString(txtName.Text) = "" Then
                lblError.Text = "Please Enter the Name"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                txtName.Focus()
                Exit Sub
            Else
                Name = GV.parseString(txtName.Text)
            End If
            If GV.parseString(txtMobileNo.Text) = "" Then
                lblError.Text = "Please Enter Mobile No."
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                txtMobileNo.Focus()
                Exit Sub
            Else
                MobileNo = GV.parseString(txtMobileNo.Text)
            End If
            If GV.parseString(txtAnnual_Turnover.Text) = "" Then
                lblError.Text = "Please Enter Annual Turnover"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                txtAnnual_Turnover.Focus()
                Exit Sub
            Else
                Annualturnover = GV.parseString(txtAnnual_Turnover.Text)
            End If
            If GV.parseString(txtAddress.Text) = "" Then
                lblError.Text = "Please Enter the Address"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                txtAddress.Focus()
                Exit Sub
            Else
                Address = GV.parseString(txtAddress.Text)
            End If
            If GV.parseString(ddlDistrict.SelectedValue) = "" Then
                lblError.Text = "Please Select District"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                ddlDistrict.Focus()
                Exit Sub
            Else
                District = GV.parseString(ddlDistrict.SelectedValue)
            End If
            If GV.parseString(ddlState_UnionTerritory.SelectedValue) = "" Then
                lblError.Text = "Please Select Sate"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                ddlState_UnionTerritory.Focus()
                Exit Sub
            Else
                State = GV.parseString(ddlState_UnionTerritory.SelectedValue)
            End If
            If GV.parseString(txtPin.Text) = "" Then
                lblError.Text = "Please Enter Pin"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                txtPin.Focus()
                Exit Sub
            Else
                Pin = GV.parseString(txtPin.Text)
            End If


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

    Protected Sub btnPopupYes_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try

            If Session("Workfor") = "Delete" Then
                If btnPopupYes.Text = "Ok" Then
                    If Session("EditFlag") = 1 Then
                        Response.Redirect("#") '// Change form Name
                    Else
                        Clear()
                        Exit Sub
                    End If
                End If

                Dim QryStr As String = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Financial_Audit where RID=" & lblRID.Text.Trim & ""
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
                    Response.Redirect("#") '// Change form Name
                Else
                    Clear()
                    Exit Sub
                End If
            End If


            lblError.Text = ""
            lblError.CssClass = ""

            If Not txtName.Text.Trim = "" Then
                Name = GV.parseString(txtName.Text.Trim)
            Else
                Name = ""
            End If
            If Not txtMobileNo.Text.Trim = "" Then
                MobileNo = GV.parseString(txtMobileNo.Text.Trim)
            Else
                MobileNo = ""
            End If
            If Not txtAnnual_Turnover.Text.Trim = "" Then
                Annualturnover = GV.parseString(txtAnnual_Turnover.Text.Trim)
            Else
                Annualturnover = ""
            End If

            If Not txtAddress.Text.Trim = "" Then
                Address = GV.parseString(txtAddress.Text.Trim)
            Else
                Address = ""
            End If

            If Not txtPin.Text.Trim = "" Then
                Pin = GV.parseString(txtPin.Text.Trim)
            Else
                Pin = ""
            End If
            If ddlState_UnionTerritory.Items.Count > 0 Then
                If Not ddlState_UnionTerritory.SelectedValue.Trim = "" Then
                    State = GV.parseString(ddlState_UnionTerritory.SelectedValue.Trim)
                Else
                    State = ""
                End If
            End If

            If ddlState_UnionTerritory.Items.Count > 0 Then
                If Not ddlDistrict.SelectedValue.Trim = "" Then
                    State = GV.parseString(ddlDistrict.SelectedValue.Trim)
                Else
                    State = ""
                End If
            End If


            Dim recorddateTime, EntryBy, UpdatedOn, UpdatedBy As String
            recorddateTime = Now
            EntryBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            UpdatedOn = Now
            UpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)


            If Session("EditFlag") = 0 Then

                If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Financial_Audit where RID= " & lblRID.Text.Trim & " ") > 0 Then
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                Dim QryStr As String = "Insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & " .dbo.Frm_Financial_Audit(Name, MobileNo, Annualturnover, Address, District, State, Pin) Values('" & Name & "','" & MobileNo & "','" & Annualturnover & "','" & Address & "','" & District & "','" & State & "','" & Pin & "','" & recorddateTime & "','" & EntryBy & "')"
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

                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.Frm_Financial_Audit set Name='" & Name & "',MobileNo='" & MobileNo & "',Annualturnover='" & Annualturnover & "',  Address='" & Address & "', District='" & District & "', State_UnionTerritory='" & State & "',Pin='" & Pin & "',UpdatedOn='" & UpdatedOn & "', UpdatedBy='" & UpdatedBy & "' where RID=" & lblRID.Text.Trim & ""
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

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Private Sub Clear()
        Try
            txtName.Text = ""
            txtMobileNo.Text = ""
            txtAddress.Text = ""
            txtAnnual_Turnover.Text = ""
            txtPin.Text = ""
            ddlDistrict.SelectedIndex = ""
            ddlState_UnionTerritory.SelectedIndex = ""
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



End Class