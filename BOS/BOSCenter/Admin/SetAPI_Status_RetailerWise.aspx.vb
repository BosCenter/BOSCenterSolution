
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO




Public Class SetAPI_Status_RetailerWise
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Querystring = "select RID as SrNo,(CONVERT(VARCHAR(11),RegistrationDate,106)) as Registration_Date,RegistrationId,(FirstName+' '+LastName) as Name,PanCardNumber as 'PanCard',AgencyName,MobileNo,RechargeAPI_Status as 'RechargeAPI',RechargeAPI_2_Status as 'RechargeAPI_2', MoneyTransferAPI_Status as 'MoneyTransferAPI', MoneyTransferAPI_2_Status as 'MoneyTransferAPI_2', PANCardAPI_Status as 'PANCardAPI',AEPS_API_Status as 'AEPS_API',convert(numeric(18,0), isnull(HoldAmt,0)) as 'HoldAmt',HoldAmtRemarks as 'HoldRemarks' from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType in ('Retailer','Customer')   order by  RegistrationDate desc"


            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 0)

                    For i As Integer = 0 To GridView1.Rows.Count - 1

                        Dim lnk_MoneyTransferAPI As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnk_MoneyTransferAPI"), LinkButton)
                        Dim lnk_PANCardAPI As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnk_PANCardAPI"), LinkButton)
                        Dim lnk_AEPS_API As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnk_AEPS_API"), LinkButton)
                        Dim lnk_RechargeAPI As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnk_RechargeAPI"), LinkButton)
                        Dim lnk_RechargeAPI_2 As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnk_RechargeAPI_2"), LinkButton)
                        Dim lnk_MoneyTransferAPI_2 As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnk_MoneyTransferAPI_2"), LinkButton)


                        Dim txt_HoldAmt As TextBox = DirectCast(GridView1.Rows(i).FindControl("txt_HoldAmt"), TextBox)
                        Dim txt_HoldAmtRemarks As TextBox = DirectCast(GridView1.Rows(i).FindControl("txt_HoldAmtRemarks"), TextBox)

                        If lnk_MoneyTransferAPI.Text.Trim.ToUpper = "Active".Trim.ToUpper Then
                            lnk_MoneyTransferAPI.CssClass = "btn btn-success mar_top10"

                        ElseIf lnk_MoneyTransferAPI.Text.Trim.ToUpper = "InActive".Trim.ToUpper Then
                            lnk_MoneyTransferAPI.CssClass = "btn btn-danger mar_top10"
                        End If


                        If lnk_MoneyTransferAPI_2.Text.Trim.ToUpper = "Active".Trim.ToUpper Then
                            lnk_MoneyTransferAPI_2.CssClass = "btn btn-success mar_top10"

                        ElseIf lnk_MoneyTransferAPI_2.Text.Trim.ToUpper = "InActive".Trim.ToUpper Then
                            lnk_MoneyTransferAPI_2.CssClass = "btn btn-danger mar_top10"
                        End If


                        If lnk_PANCardAPI.Text.Trim.ToUpper = "Active".Trim.ToUpper Then
                            lnk_PANCardAPI.CssClass = "btn btn-success mar_top10"

                        ElseIf lnk_PANCardAPI.Text.Trim.ToUpper = "InActive".Trim.ToUpper Then
                            lnk_PANCardAPI.CssClass = "btn btn-danger mar_top10"
                        End If

                        If lnk_AEPS_API.Text.Trim.ToUpper = "Active".Trim.ToUpper Then
                            lnk_AEPS_API.CssClass = "btn btn-success mar_top10"
                        ElseIf lnk_AEPS_API.Text.Trim.ToUpper = "InActive".Trim.ToUpper Then
                            lnk_AEPS_API.CssClass = "btn btn-danger mar_top10"
                        End If

                        If lnk_RechargeAPI.Text.Trim.ToUpper = "Active".Trim.ToUpper Then
                            lnk_RechargeAPI.CssClass = "btn btn-success mar_top10"
                        ElseIf lnk_RechargeAPI.Text.Trim.ToUpper = "InActive".Trim.ToUpper Then
                            lnk_RechargeAPI.CssClass = "btn btn-danger mar_top10"
                        End If
                        If lnk_RechargeAPI_2.Text.Trim.ToUpper = "Active".Trim.ToUpper Then
                            lnk_RechargeAPI_2.CssClass = "btn btn-success mar_top10"
                        ElseIf lnk_RechargeAPI_2.Text.Trim.ToUpper = "InActive".Trim.ToUpper Then
                            lnk_RechargeAPI_2.CssClass = "btn btn-danger mar_top10"
                        End If
                    Next
                Else

                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                    End If
                End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_HoldAmtRemarks_GridView_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""

            Dim QryStr As String = ""

            Dim txt_HoldAmtRemarks As TextBox = TryCast(sender, TextBox)
            Dim gvrow As GridViewRow = DirectCast(txt_HoldAmtRemarks.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)



            Dim HoldAmtRemarks As String = GV.parseString(txt_HoldAmtRemarks.Text)

            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set HoldAmtRemarks='" & HoldAmtRemarks.Trim & "' where RegistrationId='" & CompanyCode & "';"


            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txt_HoldAmt_GridView_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""
            Dim QryStr As String = ""

            Dim txt_HoldAmt As TextBox = TryCast(sender, TextBox)
            Dim gvrow As GridViewRow = DirectCast(txt_HoldAmt.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim HoldAmt As String = GV.parseString(txt_HoldAmt.Text)
            If HoldAmt.Trim = "" Then
                HoldAmt = "0"
            End If

            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set HoldAmt=" & HoldAmt & " where RegistrationId='" & CompanyCode & "';"


            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub lnk_MoneyTransferAPI_GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""
            Dim QryStr As String = ""

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim activeSta As String = GV.parseString(btndetails.Text)

            Dim status As String = "Inactive"
            If activeSta.Trim.ToUpper = "Active".Trim.ToUpper Then
                status = "Inactive"
            ElseIf activeSta.Trim.ToUpper = "InActive".Trim.ToUpper Then
                status = "Active"
            End If


            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set MoneyTransferAPI_Status='" & status & "' where RegistrationId='" & CompanyCode & "';"



            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnk_MoneyTransferAPI_2_GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""
            Dim QryStr As String = ""

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim activeSta As String = GV.parseString(btndetails.Text)

            Dim status As String = "Inactive"
            If activeSta.Trim.ToUpper = "Active".Trim.ToUpper Then
                status = "Inactive"
            ElseIf activeSta.Trim.ToUpper = "InActive".Trim.ToUpper Then
                status = "Active"
            End If


            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set MoneyTransferAPI_2_Status='" & status & "' where RegistrationId='" & CompanyCode & "';"



            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub lnk_PANCardAPI_GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""
            Dim QryStr As String = ""

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim activeSta As String = GV.parseString(btndetails.Text)
            Dim status As String = "Inactive"
            If activeSta.Trim.ToUpper = "Active".Trim.ToUpper Then
                status = "Inactive"
            ElseIf activeSta.Trim.ToUpper = "InActive".Trim.ToUpper Then
                status = "Active"
            End If

            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set PANCardAPI_Status='" & status & "' where RegistrationId='" & CompanyCode & "';"

            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnk_AEPS_API_GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""
            Dim QryStr As String = ""

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim activeSta As String = GV.parseString(btndetails.Text)

            Dim status As String = "Inactive"
            If activeSta.Trim.ToUpper = "Active".Trim.ToUpper Then
                status = "Inactive"
            ElseIf activeSta.Trim.ToUpper = "InActive".Trim.ToUpper Then
                status = "Active"
            End If

            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set AEPS_API_Status='" & status & "' where RegistrationId='" & CompanyCode & "';"


            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnk_RechargeAPI_GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""
            Dim QryStr As String = ""

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim activeSta As String = GV.parseString(btndetails.Text)

            Dim status As String = "Inactive"
            If activeSta.Trim.ToUpper = "Active".Trim.ToUpper Then
                status = "Inactive"
            ElseIf activeSta.Trim.ToUpper = "InActive".Trim.ToUpper Then
                status = "Active"
            End If

            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set RechargeAPI_Status='" & status & "' where RegistrationId='" & CompanyCode & "';"

            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnk_RechargeAPI_2_GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            Dim CompanyCode As String = ""
            Dim QryStr As String = ""

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            CompanyCode = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            Dim activeSta As String = GV.parseString(btndetails.Text)

            Dim status As String = "Inactive"
            If activeSta.Trim.ToUpper = "Active".Trim.ToUpper Then
                status = "Inactive"
            ElseIf activeSta.Trim.ToUpper = "InActive".Trim.ToUpper Then
                status = "Active"
            End If

            QryStr = "Update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set RechargeAPI_2_Status='" & status & "' where RegistrationId='" & CompanyCode & "';"

            If GV.FL.DMLQueries(QryStr) = True Then
                lblNoRecords.Text = "Status Updated Successfully."
                lblNoRecords.CssClass = "Successlabels"
            Else
                lblNoRecords.Text = "Sorry !! Process Can't be Completed."
                lblNoRecords.CssClass = "errorlabels"
            End If
            Bind()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If canPerformOperation(Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", ".."), "Update") = False Then
                lblNoRecords.Text = "Not Autorized To Performe This Action."
                lblNoRecords.CssClass = "errorlabels"
                ImagebtnExcel.Focus()
                Exit Sub
            End If



            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblUserId.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            txtUserId_popup.Text = lblUserId.Text
            btnCancel.Text = "Cancel"
            btnUpdate.Visible = True
            lblDialogMsg.Text = ""
            lblDialogMsg.CssClass = ""


            ddlRechargeAPI_Status.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text).ToString()
            ddlRechargeAPI_2_Status.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text).ToString()
            ddlMoneyTransferAPI_Status.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text).ToString()
            ddlPANCardAPI_Status.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text).ToString()
            ddlAEPSAPI_Status.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text).ToString()
            txtHoldAmt.Text = CInt(GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(13).Text).ToString())
            txtHoldRemarks.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(14).Text).ToString()


            ddlRechargeAPI_Status.CssClass = "form-control"
            ddlMoneyTransferAPI_Status.CssClass = "form-control"
            ddlPANCardAPI_Status.CssClass = "form-control"
            ddlAEPSAPI_Status.CssClass = "form-control"

            txtUserId_popup.CssClass = "form-control"
            txtHoldAmt.CssClass = "form-control"
            txtHoldRemarks.CssClass = "form-control"


            ModalPopupExtender1.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlNoOfRecords_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNoOfRecords.SelectedIndexChanged
        Try
            If ddlNoOfRecords.SelectedValue = "10 Record(s)" Then
                GridView1.PageSize = 10
            ElseIf ddlNoOfRecords.SelectedValue = "25 Record(s)" Then
                GridView1.PageSize = 25
            ElseIf ddlNoOfRecords.SelectedValue = "50 Record(s)" Then
                GridView1.PageSize = 50
            ElseIf ddlNoOfRecords.SelectedValue = "100 Record(s)" Then
                GridView1.PageSize = 100
            ElseIf ddlNoOfRecords.SelectedValue = "200 Record(s)" Then
                GridView1.PageSize = 200
            ElseIf ddlNoOfRecords.SelectedValue = "500 Record(s)" Then
                GridView1.PageSize = 500
            End If
            Bind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
        End Try

    End Sub



    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "RetailerWiseAPIStatus")
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "RetailerWiseAPIStatus")
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnword.Click
        Try
            GV.ExportToWord(GridView1, Response, "RetailerWiseAPIStatus")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            If Not lblUserId.Text = "" Then
                Dim RechargeAPI_Status, MoneyTransferAPI_Status, PANCardAPI_Status, AEPSAPI_Status, HoldAmt, HoldRemarks As String
                RechargeAPI_Status = ddlRechargeAPI_Status.SelectedValue
                MoneyTransferAPI_Status = ddlMoneyTransferAPI_Status.SelectedValue
                PANCardAPI_Status = ddlPANCardAPI_Status.SelectedValue
                AEPSAPI_Status = ddlAEPSAPI_Status.SelectedValue
                HoldAmt = GV.parseString(txtHoldAmt.Text.Trim)
                HoldRemarks = GV.parseString(txtHoldRemarks.Text)
                If HoldAmt.Trim = "" Then
                    HoldAmt = "0"
                End If


                Dim str As String
                str = ("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set  HoldAmtRemarks='" & HoldRemarks & "', HoldAmt='" & HoldAmt & "',AEPS_API_Status='" & AEPSAPI_Status & "', RechargeAPI_Status='" & RechargeAPI_Status & "',MoneyTransferAPI_Status='" & MoneyTransferAPI_Status & "',PANCardAPI_Status='" & PANCardAPI_Status & "' where RegistrationId='" & lblUserId.Text & "'")
                Dim result As Boolean = GV.FL.DMLQueries(str)
                lblDialogMsg.Text = result
                If result = True Then
                    lblDialogMsg.Text = "Record Updated Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    Bind()

                Else
                    lblDialogMsg.Text = "Sorry !! Record Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnUpdate.Visible = False
                ModalPopupExtender1.Show()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class