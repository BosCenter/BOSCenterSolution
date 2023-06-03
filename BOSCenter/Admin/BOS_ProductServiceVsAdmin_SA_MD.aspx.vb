
Public Class BOS_ProductServiceVsAdmin_SA_MD
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'CMP1045:Business Online Solution

                Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                Dim DataBaseName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response)
                Dim Group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                Dim CompanyName As String = GV.FL.AddInVar("CompanyName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where DatabaseName='" & DataBaseName.Trim & "'")


                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    GV.FL.AddInDropDownListAll(ddl_Admin, "CompanyCode +':'+ CompanyName", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration order by RID Desc")
                    If ddl_Admin.Items.Count > 0 Then
                        ddl_Admin.Items.Insert(0, ":: Select Admin ::")
                    Else
                        ddl_Admin.Items.Add(":: Select Admin ::")
                    End If
                    ddl_Admin.SelectedValue = CompanyCode.Trim.ToUpper & ":" & CompanyName
                    ddl_Admin_SelectedIndexChanged(sender, e)

                    lblSessionFlag.Text = 0
                    Bind()
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Admin".Trim.ToUpper Then
                    GV.FL.AddInDropDownListAll(ddl_Admin, "CompanyCode +':'+ CompanyName", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration order by RID Desc")
                    If ddl_Admin.Items.Count > 0 Then
                        ddl_Admin.Items.Insert(0, ":: Select Admin ::")
                    Else
                        ddl_Admin.Items.Add(":: Select Admin ::")
                    End If
                    ddl_Admin.SelectedValue = CompanyCode.Trim.ToUpper & ":" & CompanyName
                    ddl_Admin_SelectedIndexChanged(sender, e)
                    ddl_Admin.Enabled = False

                    lblSessionFlag.Text = 0
                    Bind()
                Else


                End If



            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Dim str As String

    Public Sub Bind()
        Try
            If Not ddl_Admin.SelectedIndex = 0 Then
                Dim dd() As String = ddl_Admin.SelectedValue.Split(":")
                str = "select RID as SrNo,(select CompanyCode+':'+CompanyName from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration CL where CL.CompanyCode=PSA.AdminID) as [AdminIDX],* from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD PSA where AdminID='" & dd(0).Trim & "'  order by rid desc "
                ds = GV.FL.OpenDsWithSelectQuery(str)
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()
                If GridView1.Rows.Count > 0 Then
                    GV.FL.showSerialnoOnGridView(GridView1, 1)
                End If
            Else

                GridView1.DataSource = Nothing
                GridView1.DataBind()

                'str = "select RID as SrNo,(select CompanyCode+':'+CompanyName from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration CL where CL.CompanyCode=PSA.AdminID) as [AdminIDX],* from BosCenter_DB.dbo.BOS_ProductServiceVsAdmin_SA PSA  order by rid desc "
                'ds = GV.FL.OpenDsWithSelectQuery(str)
                'GridView1.DataSource = ds.Tables(0)
                'GridView1.DataBind()
                'If GridView1.Rows.Count > 0 Then
                '    GV.FL.showSerialnoOnGridView(GridView1, 1)
                'End If
            End If



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Dim statecode As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim V_Admin_CommissionType, V_Admin_Commission, VSlabApplicable As String
            V_Admin_CommissionType = ""
            V_Admin_Commission = "0"

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
            Dim VAdminID, Vtitle, VStatus, VContainCategory, VGateway, VServiceType, VServiceCharge As String

            If ddl_Admin.SelectedIndex = 0 Then
                lblError.Text = "Please Select Admin."
                lblError.CssClass = "errorlabels"
                ddl_Admin.Focus()
                Exit Sub
            Else
                Dim dd() As String = GV.parseString(ddl_Admin.SelectedValue.Trim).Split(":")
                VAdminID = dd(0).Trim
            End If

            If ddl_Service_Category.SelectedIndex = 0 Then
                lblError.Text = "Please Select Service."
                lblError.CssClass = "errorlabels"
                ddl_Service_Category.Focus()
                Exit Sub
            Else
                Vtitle = GV.parseString(ddl_Service_Category.SelectedValue.Trim)
            End If

            If ddl_Default_Service.SelectedIndex = 0 Then
                lblError.Text = "Please Select Gateway."
                lblError.CssClass = "errorlabels"
                ddl_Default_Service.Focus()
                Exit Sub
            Else
                VGateway = GV.parseString(ddl_Default_Service.SelectedValue.Trim)
            End If

            V_Admin_Commission = "0"
            VServiceCharge = "0"


            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"


            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD where Title='" & Vtitle & "' and AdminID='" & VAdminID & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " Service Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD (AdminID,Title,Gateway,UpdatedBy,UpdatedOn) values('" & VAdminID & "','" & Vtitle & "','" & VGateway & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        Clear()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                        ddl_Service_Category.Focus()
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD where  AdminID='" & VAdminID & "' and   (Title='" & Vtitle & "' and not Title='" & GV.parseString(lblUpadate.Text) & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    ' Session("EditFlag") = 0
                    QryStr = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD set Gateway='" & VGateway & "', Title='" & Vtitle & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"

                    'QryStr = QryStr & "  delete from BosCenter_DB.dbo.BOS_OperatorWiseCommission_Agents  where  APIName='" & Vtitle.Trim & "' "



                    If GV.FL.DMLQueriesBulk(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Bind()
                        Clear()

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
            ddl_Service_Category.Enabled = True
            ddl_Default_Service.Enabled = True

            ddl_Service_Category.SelectedIndex = 0
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""
            ddl_Default_Service.SelectedIndex = 0

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddl_Admin_SelectedIndexChanged(sender, e)
            Bind()

            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                ddl_Admin.Enabled = True
            Else
                ddl_Admin.Enabled = False
            End If
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
            ddl_Service_Category.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            ddl_Service_Category_SelectedIndexChanged(sender, e)

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) = "&nbsp;" Then
                ddl_Default_Service.SelectedIndex = 0
            Else
                ddl_Default_Service.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            End If


            ddl_Admin.Enabled = False
            ddl_Service_Category.Enabled = False


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
                QryStr = "delete from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD where RID=" & lblRID.Text & ""
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
                GV.ExportToExcel_New(GridView1, Response, "", "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "ServiceAPIMaster", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub





    Protected Sub ddl_Service_Category_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Service_Category.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ddl_Default_Service.Items.Clear()


            If Not ddl_Service_Category.SelectedIndex = 0 Then

                '<asp:ListItem >: Select Case Service :</asp:ListItem>
                '<asp:ListItem>Money Transfer</asp:ListItem>
                '<asp:ListItem > Recharge & Bill Payment</asp:ListItem>
                '<asp:ListItem>PAN CARD</asp:ListItem>
                '<asp:ListItem > AEPS</asp:ListItem>


                ddl_Default_Service.Items.Add(":: Select Gateway ::")

                If ddl_Service_Category.SelectedValue = "Money Transfer" Then
                    ddl_Default_Service.Items.Add("Money Transfer 1")
                    ddl_Default_Service.Items.Add("Money Transfer 2")
                ElseIf ddl_Service_Category.SelectedValue = "Recharge & Bill Payment" Then
                    ddl_Default_Service.Items.Add("Recharge 1")
                    ddl_Default_Service.Items.Add("Recharge 2")
                ElseIf ddl_Service_Category.SelectedValue = "Payment Gateway" Then
                    ddl_Default_Service.Items.Add("PayuMoney")
                    ddl_Default_Service.Items.Add("Easebuzz")
                ElseIf ddl_Service_Category.SelectedValue = "PAN CARD" Then
                    ddl_Default_Service.Items.Add("PAN CARD")
                ElseIf ddl_Service_Category.SelectedValue = "AEPS" Then
                    ddl_Default_Service.Items.Add("AEPS")
                ElseIf ddl_Service_Category.SelectedValue = "Payin" Then
                    ddl_Default_Service.Items.Add("CF_Payin1")
                    ddl_Default_Service.Items.Add("IC_Payin2")
                    ddl_Default_Service.Items.Add("FP_Payin3")
                ElseIf ddl_Service_Category.SelectedValue = "Payout" Then
                    ddl_Default_Service.Items.Add("FP_Payout1")
                    ddl_Default_Service.Items.Add("CF_Payout2")
                End If

                ddl_Default_Service.SelectedIndex = 0


            Else
                ddl_Default_Service.Items.Add(":: Select Gateway ::")
                ddl_Default_Service.SelectedIndex = 0

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddl_Admin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Admin.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class