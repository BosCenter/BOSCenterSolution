
Public Class BOS_Ref_Code_Master
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                txtReferenceCode.Text = ""
                txtAmountToDebit.Text = "0"

                lblSessionFlag.Text = 0
                ddl_Dis_CommissionType.SelectedIndex = 0
                ddl_Dis_CommissionType_SelectedIndexChanged(sender, e)

                ddl_Sub_Dis_CommissionType.SelectedIndex = 0
                ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Retailer_CommsissionType.SelectedIndex = 0
                ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Customer_CommissionType.SelectedIndex = 0
                ddl_Customer_CommissionType_SelectedIndexChanged(sender, e)


                Bind()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Dim str As String

    Public Sub Bind()
        Try
            str = "select RID as SrNo,Ref_Code,AmtToDebit,Dis_CommissionType as 'Dis Comm Type',Dis_Commission as 'Dis Comm',Sub_Dis_CommissionType as 'SD Comm Type',Sub_Dis_Commission as 'SD Comm',Retailer_CommissionType as 'RET Comm Type',Retailer_Commission as 'RET Comm',Customer_CommissionType as 'Cust Comm Type',Customer_Commission as 'Cust Comm' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master  order by rid desc "
            ds = GV.FL.OpenDsWithSelectQuery(str)
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                GV.FL.showSerialnoOnGridView(GridView1, 1)
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Dim statecode As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim V_ReferenceCode, V_AmountToDebit, V_Dis_CommissionType, V_Dis_Commission, V_Customer_CommissionType, V_Customer_Commission, V_Sub_Dis_CommissionType, V_Sub_Dis_Commission, V_Retailer_CommissionType, V_Retailer_Commission, V_instamojo_Pay_Link As String

            V_ReferenceCode = ""
            V_AmountToDebit = "0"
            V_Dis_CommissionType = ""
            V_Dis_Commission = "0"
            V_Customer_CommissionType = ""
            V_Customer_Commission = "0"
            V_Sub_Dis_CommissionType = ""
            V_Sub_Dis_Commission = "0"
            V_Retailer_CommissionType = ""
            V_Retailer_Commission = "0"
            V_instamojo_Pay_Link = ""


            If btnSave.Text.Trim.ToUpper = "Save".Trim.ToUpper Then
                Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1

                            If Not DataRows(D).Item("CanSave") = True Then
                                lblDialogMsg.CssClass = ""
                                lblDialogMsg.Text = "Not Autorized To Performe This Action."
                                btnCancel.Text = "Ok"
                                btnok.Visible = False
                                ModalPopupExtender1.Show()
                                Exit Sub
                            End If
                        Next

                    End If
                End If
            ElseIf btnSave.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If Not DataRows(D).Item("CanUpdate") = True Then
                                lblDialogMsg.CssClass = ""
                                lblDialogMsg.Text = "Not Autorized To Performe This Action."
                                btnCancel.Text = "Ok"
                                btnok.Visible = False
                                ModalPopupExtender1.Show()
                                Exit Sub
                            End If
                        Next

                    End If
                End If
            End If
            lblError.Text = ""
            lblError.CssClass = ""

            If GV.parseString(txtReferenceCode.Text) = "" Then
                lblError.Text = "Please Enter Reference Code."
                lblError.CssClass = "errorlabels"
                txtReferenceCode.Focus()
                Exit Sub
            Else
                V_ReferenceCode = GV.parseString(txtReferenceCode.Text.Trim)
            End If

            V_AmountToDebit = GV.parseString(txtAmountToDebit.Text.Trim)
            V_instamojo_Pay_Link = GV.parseString(txtinstamojo_Pay_Link.Text.Trim)

            If ddl_Customer_CommissionType.SelectedIndex = 0 Then
                V_Customer_CommissionType = GV.parseString(ddl_Customer_CommissionType.SelectedValue.Trim)
                V_Customer_Commission = "0"
            Else
                V_Customer_CommissionType = GV.parseString(ddl_Customer_CommissionType.SelectedValue.Trim)
                If txt_Customer_Commission.Text = "" Then
                    lblError.Text = "Please Enter Customer Charge."
                    lblError.CssClass = "errorlabels"
                    txt_Customer_Commission.Focus()
                    Exit Sub
                Else
                    V_Customer_Commission = GV.parseString(txt_Customer_Commission.Text.Trim)
                End If
            End If


            If ddl_Dis_CommissionType.SelectedIndex = 0 Then
                V_Dis_CommissionType = GV.parseString(ddl_Dis_CommissionType.SelectedValue.Trim)
                V_Dis_Commission = "0"
            Else
                V_Dis_CommissionType = GV.parseString(ddl_Dis_CommissionType.SelectedValue.Trim)
                If txt_Dis_Commission.Text = "" Then
                    lblError.Text = "Please Enter Master Distributor Comm."
                    lblError.CssClass = "errorlabels"
                    txt_Dis_Commission.Focus()
                    Exit Sub
                Else
                    V_Dis_Commission = GV.parseString(txt_Dis_Commission.Text.Trim)
                End If
            End If

            If ddl_Sub_Dis_CommissionType.SelectedIndex = 0 Then
                V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Dis_CommissionType.SelectedValue.Trim)
                V_Sub_Dis_Commission = "0"
            Else
                V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Dis_CommissionType.SelectedValue.Trim)
                If txt_Sub_Dis_Commission.Text = "" Then
                    lblError.Text = "Please Enter Distributor Comm."
                    lblError.CssClass = "errorlabels"
                    txt_Sub_Dis_Commission.Focus()
                    Exit Sub
                Else
                    V_Sub_Dis_Commission = GV.parseString(txt_Sub_Dis_Commission.Text.Trim)
                End If
            End If


            If ddl_Retailer_CommsissionType.SelectedIndex = 0 Then
                V_Retailer_CommissionType = GV.parseString(ddl_Retailer_CommsissionType.SelectedValue.Trim)
                V_Retailer_Commission = "0"
            Else
                V_Retailer_CommissionType = GV.parseString(ddl_Retailer_CommsissionType.SelectedValue.Trim)
                If txt_Retailer_Commission.Text = "" Then
                    lblError.Text = "Please Enter Retailer Commission."
                    lblError.CssClass = "errorlabels"
                    txt_Retailer_Commission.Focus()
                    Exit Sub
                Else
                    V_Retailer_Commission = GV.parseString(txt_Retailer_Commission.Text.Trim)
                End If
            End If



            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master where Ref_Code='" & V_ReferenceCode & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " Reference Code Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master (instamojo_Pay_Link,Ref_Code,AmtToDebit,Dis_CommissionType,Dis_Commission,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,Customer_CommissionType,Customer_Commission,UpdatedBy,UpdatedOn) values( '" & V_instamojo_Pay_Link & "','" & V_ReferenceCode & "','" & V_AmountToDebit & "','" & V_Dis_CommissionType & "'," & V_Dis_Commission & ",'" & V_Sub_Dis_CommissionType & "'," & V_Sub_Dis_Commission & ",'" & V_Retailer_CommissionType & "'," & V_Retailer_Commission & ",'" & V_Customer_CommissionType & "'," & V_Customer_Commission & ",'" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        Clear()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                        txtReferenceCode.Focus()
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master where   (Ref_Code='" & V_ReferenceCode & "' and not Ref_Code='" & GV.parseString(lblUpadate.Text) & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    ' Session("EditFlag") = 0
                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master set  instamojo_Pay_Link='" & V_instamojo_Pay_Link & "', Ref_Code='" & V_ReferenceCode & "',AmtToDebit=" & V_AmountToDebit & ",Dis_CommissionType='" & V_Dis_CommissionType & "',Dis_Commission=" & V_Dis_Commission & ",Sub_Dis_CommissionType='" & V_Sub_Dis_CommissionType & "',Sub_Dis_Commission=" & V_Sub_Dis_Commission & ",Retailer_CommissionType='" & V_Retailer_CommissionType & "',Retailer_Commission=" & V_Retailer_Commission & ",Customer_CommissionType='" & V_Customer_CommissionType & "',Customer_Commission=" & V_Customer_Commission & ", UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"


                    If GV.FL.DMLQueriesBulk(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Bind()
                        Clear()
                        ddl_Dis_CommissionType_SelectedIndexChanged(sender, e)

                        ddl_Customer_CommissionType_SelectedIndexChanged(sender, e)
                        lblError.Text = "Record Updated Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub Clear()
        Try

            txtReferenceCode.Text = ""

            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""
            txtAmountToDebit.Text = "0"
            ddl_Customer_CommissionType.SelectedIndex = 0

            ddl_Dis_CommissionType.SelectedIndex = 0
            ddl_Dis_CommissionType.CssClass = "form-control"
            txt_Dis_Commission.Text = "0"
            txt_Dis_Commission.Enabled = False
            txt_Dis_Commission.CssClass = "form-control"

            ddl_Sub_Dis_CommissionType.SelectedIndex = 0
            ddl_Sub_Dis_CommissionType.CssClass = "form-control"
            txt_Sub_Dis_Commission.Text = "0"
            txt_Sub_Dis_Commission.Enabled = False
            txt_Sub_Dis_Commission.CssClass = "form-control"

            ddl_Retailer_CommsissionType.SelectedIndex = 0
            ddl_Retailer_CommsissionType.CssClass = "form-control"
            txt_Retailer_Commission.Text = "0"
            txt_Retailer_Commission.Enabled = False
            txt_Retailer_Commission.CssClass = "form-control"

            txtinstamojo_Pay_Link.Text = ""

            txt_Customer_Commission.Text = "0"


            ddl_Customer_CommissionType.CssClass = "form-control"
            txt_Customer_Commission.Enabled = False
            txt_Customer_Commission.CssClass = "form-control"

            lblStarServiceType.Visible = False

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddl_Dis_CommissionType_SelectedIndexChanged(sender, e)
            ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Customer_CommissionType_SelectedIndexChanged(sender, e)
            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            Dim LocalDS As New DataSet
            LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            Dim DataRows() As DataRow
            DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            If Not DataRows Is Nothing Then
                If DataRows.Count > 0 Then
                    For D As Integer = 0 To DataRows.Count - 1
                        If Not DataRows(D).Item("CanUpdate") = True Then
                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If


            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            txtReferenceCode.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            txtAmountToDebit.Text = CInt(GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text))


            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) = "&nbsp;" Then
                ddl_Dis_CommissionType.SelectedIndex = 0
            Else
                ddl_Dis_CommissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            End If

            ddl_Dis_CommissionType_SelectedIndexChanged(sender, e)
            txt_Dis_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text) = "&nbsp;" Then
                ddl_Sub_Dis_CommissionType.SelectedIndex = 0
            Else
                ddl_Sub_Dis_CommissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            End If
            ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Sub_Dis_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "&nbsp;" Then
                ddl_Retailer_CommsissionType.SelectedIndex = 0
            Else
                ddl_Retailer_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
            End If

            ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Retailer_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)




            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text) = "&nbsp;" Then
                ddl_Customer_CommissionType.SelectedIndex = 0
            Else
                ddl_Customer_CommissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)
            End If
            ddl_Customer_CommissionType_SelectedIndexChanged(sender, e)
            txt_Customer_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text)

            txtinstamojo_Pay_Link.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text)


            'Session("Editflag") = 1
            lblSessionFlag.Text = 1
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.Text = ""
            lblError.CssClass = ""

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            Dim LocalDS As New DataSet
            LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
            Dim DataRows() As DataRow
            DataRows = LocalDS.Tables(0).Select(" FormName='" & CurentFOrm & "' ")
            If Not DataRows Is Nothing Then
                If DataRows.Count > 0 Then
                    For D As Integer = 0 To DataRows.Count - 1
                        If Not DataRows(D).Item("CanDelete") = True Then
                            lblDialogMsg.CssClass = ""
                            lblDialogMsg.Text = "Not Autorized To Performe This Action."
                            btnCancel.Text = "Ok"
                            btnok.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                    Next

                End If
            End If
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = " Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            btnok.Visible = True
            ModalPopupExtender1.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then
                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master where RID=" & lblRID.Text & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    Bind()
                    Clear()

                    lblDialogMsg.Text = "Record Deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnok.Visible = False
                ModalPopupExtender1.Show()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnDelete_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            lblDialogMsg.CssClass = ""
            lblDialogMsg.Text = " Are you sure you want to delete ?"
            btnCancel.Text = "Cancel"
            btnok.Visible = True
            ModalPopupExtender1.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ImagebtnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToExcel_New(GridView1, Response, "", "ServiceAPIMaster", "select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "ServiceAPIMaster", "select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "ServiceAPIMaster", "select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Ref_Code_Master  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub ddl_Customer_CommissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Customer_CommissionType.SelectedIndexChanged
        Try
            txt_Customer_Commission.Enabled = True
            txt_Customer_Commission.CssClass = "form-control"
            If ddl_Customer_CommissionType.SelectedIndex = 0 Then
                txt_Customer_Commission.Text = "0"
                txt_Customer_Commission.Enabled = False
                txt_Customer_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddl_Dis_CommissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Dis_CommissionType.SelectedIndexChanged
        Try
            txt_Dis_Commission.Enabled = True
            txt_Dis_Commission.CssClass = "form-control"
            If ddl_Dis_CommissionType.SelectedIndex = 0 Then
                txt_Dis_Commission.Text = "0"
                txt_Dis_Commission.Enabled = False
                txt_Dis_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sub_Dis_CommissionType.SelectedIndexChanged
        Try
            txt_Sub_Dis_Commission.Enabled = True
            txt_Sub_Dis_Commission.CssClass = "form-control"
            If ddl_Sub_Dis_CommissionType.SelectedIndex = 0 Then
                txt_Sub_Dis_Commission.Text = "0"
                txt_Sub_Dis_Commission.Enabled = False
                txt_Sub_Dis_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddl_Retailer_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Retailer_CommsissionType.SelectedIndexChanged
        Try
            txt_Retailer_Commission.Enabled = True
            txt_Retailer_Commission.CssClass = "form-control"
            If ddl_Retailer_CommsissionType.SelectedIndex = 0 Then
                txt_Retailer_Commission.Text = "0"
                txt_Retailer_Commission.Enabled = False
                txt_Retailer_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class