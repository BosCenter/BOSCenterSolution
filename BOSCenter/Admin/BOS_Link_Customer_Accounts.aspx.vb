Imports System.Data.OleDb
Imports System.Data
Imports System.Net
Imports System.IO
Imports AjaxControlToolkit

Public Class BOS_Link_Customer_Accounts
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Dim aa() As String
    Dim qry As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ddlTransferTo_SelectedIndexChanged(sender, e)
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlTransferTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransferTo.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "ADMIN" Then
                If ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Master Distributor ::::".Trim.ToUpper Then
                    GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Master Distributor' ")
                    If ddlTransferToAgent.Items.Count > 0 Then
                        ddlTransferToAgent.Items.Insert(0, ":::: Select Master Distributor ::::")
                    Else
                        ddlTransferToAgent.Items.Add(":::: Select Master Distributor ::::")
                    End If
                    lblAgentType.Text = "Master Distributor"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Distributor ::::".Trim.ToUpper Then
                    GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Distributor' ")
                    If ddlTransferToAgent.Items.Count > 0 Then
                        ddlTransferToAgent.Items.Insert(0, ":::: Select Distributor ::::")
                    Else
                        ddlTransferToAgent.Items.Add(":::: Select Distributor ::::")
                    End If
                    lblAgentType.Text = "Distributor"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Retailer ::::".Trim.ToUpper Then
                    GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Retailer' ")
                    If ddlTransferToAgent.Items.Count > 0 Then
                        ddlTransferToAgent.Items.Insert(0, ":::: Select Retailer ::::")
                    Else
                        ddlTransferToAgent.Items.Add(":::: Select Retailer ::::")
                    End If
                    lblAgentType.Text = "Retailer"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Customer ::::".Trim.ToUpper Then
                    GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+ (isnull(FirstName,'')+' '+isnull(LastName,''))", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Customer' ")
                    If ddlTransferToAgent.Items.Count > 0 Then
                        ddlTransferToAgent.Items.Insert(0, ":::: Select Customer ::::")
                    Else
                        ddlTransferToAgent.Items.Add(":::: Select Customer ::::")
                    End If
                    lblAgentType.Text = "Customer"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Admin ::::".Trim.ToUpper Then
                    GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, " CompanyCode +':'+ isnull(CompanyName,'') ", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where Status='Active' ")
                    If ddlTransferToAgent.Items.Count > 0 Then
                        ddlTransferToAgent.Items.Insert(0, ":::: Select Admin ::::")
                    Else
                        ddlTransferToAgent.Items.Add(":::: Select Admin ::::")
                    End If
                    lblAgentType.Text = "Admin"
                End If
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        Try
            Dim v_Accountno, v_IFSC, v_UPIID, v_UPIQRCode As String

            v_Accountno = ""
            v_IFSC = ""
            v_UPIID = ""
            v_UPIQRCode = ""

            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False

            If ddlTransferToAgent.Items.Count <= 0 Then
                lblError.Text = "Please Select Agent."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True

                ddlTransferToAgent.Focus()
                Exit Sub
            End If


            If ddlTransferToAgent.SelectedIndex = 0 Then
                lblError.Text = "Please Select Agent."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True

                ddlTransferToAgent.Focus()
                Exit Sub
            End If



            If GV.parseString(txt_account_no.Text) = "" Then
                lblError.Text = "Please Enter Account No."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True

                txt_account_no.Focus()
                Exit Sub
            Else
                v_Accountno = GV.parseString(txt_account_no.Text)
            End If
            If GV.parseString(txt_ifsc.Text) = "" Then
                lblError.Text = "Please Enter IFSC."
                lblError.CssClass = "errorLabels"
                lblError.Visible = True
                txt_ifsc.Focus()
                Exit Sub
            Else
                v_IFSC = GV.parseString(txt_ifsc.Text)
            End If



            If ddlTransferToAgent.SelectedValue.ToString.Contains(":") Then

                aa = ddlTransferToAgent.SelectedValue.ToString.Split(":")

                qry = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & aa(0) & "'"


                Dim insrtQry As String = ""
                If btn_update.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                    insrtQry = "update  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Set easeBuzz_Virtual_Acc_No='" & v_Accountno & "', easeBuzz_IFSC='" & v_IFSC & "'  where RegistrationId='" & aa(0) & "'"
                End If
                Dim rowsAffected As Integer = 0
                Dim result As Boolean
                result = GV.FL.DMLQueriesBulk(insrtQry)
                lbl_result.Visible = True
                If result = True Then
                    Dim destinationpath As String = "~/DistributorDocuments/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & aa(0)


                    If btn_update.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                        lblError.Visible = True
                        lblError.Text = "Record Updated Successfully"
                        lblError.CssClass = "successLabels"
                    Else
                        lblError.Visible = True
                        lblError.Text = "Record Updation Failed"
                        lblError.CssClass = "errorLabels"
                    End If
                End If


            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Public Sub reset()
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            lblError.Visible = False

            If ddlTransferToAgent.Items.Count > 0 Then
                ddlTransferToAgent.SelectedIndex = 0
            End If

            txt_account_no.Text = ""
            txt_ifsc.Text = ""



            txt_account_no.CssClass = "form-control"
            txt_ifsc.CssClass = "form-control"

            ddlTransferToAgent.CssClass = "form-control"

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btn_reset_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        Try
            reset()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class