
Public Class BOS_ProductServiceMaster_AD
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                GV.FL.AddInDropDownListDistinct(ddlTitle, "Title", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA  where ProductType='API'")
                If ddlTitle.Items.Count > 0 Then
                    ddlTitle.Items.Insert(0, ":: Select Title ::")
                Else
                    ddlTitle.Items.Add(":: Select Title ::")
                End If
                ddlTitle.SelectedIndex = 0
                ddlTitle_SelectedIndexChanged(sender, e)




                ddlType.SelectedIndex = 0
                ddlType_SelectedIndexChanged(sender, e)
                lblSessionFlag.Text = 0
                ddl_Distributor_CommsissionType.SelectedIndex = 0
                ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0
                ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Retailer_CommsissionType.SelectedIndex = 0
                ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)

                ddl_Customer_CommsissionType.SelectedIndex = 0
                ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)


                Bind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim str As String

    Public Sub Bind()
        Try
            str = "select RID as SrNo,* from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster  order by rid desc "
            ds = GV.FL.OpenDsWithSelectQuery(str)
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                GV.FL.showSerialnoOnGridView(GridView1, 1)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Dim statecode As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim V_Sub_Dis_CommissionType, V_Sub_Dis_Commission, V_Retailer_CommissionType, V_Retailer_Commission, V_Customer_CommissionType, V_Customer_Commission, VSlabApplicable As String
            V_Sub_Dis_CommissionType = ""
            V_Sub_Dis_Commission = "0"
            V_Retailer_CommissionType = ""
            V_Retailer_Commission = "0"
            V_Customer_CommissionType = ""
            V_Customer_Commission = "0"

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
            Dim Vtitle, VCommison, VCommisonType, VStatus, VContainCategory, Vtype, VServiceType, VServiceCharge, VCanChange As String
            If ddlTitle.SelectedIndex = 0 Then
                lblError.Text = "Please Select Title."
                lblError.CssClass = "errorlabels"
                ddlTitle.Focus()
                Exit Sub
            Else
                Vtitle = GV.parseString(ddlTitle.SelectedValue.Trim)
            End If

            If ddlType.SelectedIndex = 0 Then
                lblError.Text = "Please Select Type."
                lblError.CssClass = "errorlabels"
                ddlType.Focus()
                Exit Sub
            Else
                Vtype = GV.parseString(ddlType.SelectedValue.Trim)
            End If
            If Vtype.Trim.ToUpper = "API" Then
                If ddlContainCategory.SelectedIndex = 0 Then
                    lblError.Text = "Please Select Contain Category."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    VContainCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
                End If

                If ddlServiceCharge.SelectedIndex = 0 Then
                    VServiceType = GV.parseString(ddlServiceCharge.SelectedValue.Trim)
                    VServiceCharge = "0"
                Else
                    VServiceType = GV.parseString(ddlServiceCharge.SelectedValue.Trim)
                    If txtServiceCharge.Text = "" Then
                        lblError.Text = "Please Enter Service Charge."
                        lblError.CssClass = "errorlabels"
                        txtServiceCharge.Focus()
                        Exit Sub
                    Else
                        VServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                    End If
                End If


                If VContainCategory.Trim.ToUpper = "NO" Then

                    If ddl_Distributor_CommsissionType.SelectedIndex = 0 Then
                        VCommisonType = GV.parseString(ddl_Distributor_CommsissionType.SelectedValue.Trim)
                        VCommison = "0"
                    Else
                        VCommisonType = GV.parseString(ddl_Distributor_CommsissionType.SelectedValue.Trim)
                        If txt_Dis_Commission.Text = "" Then
                            lblError.Text = "Please Enter Master Dis Commission."
                            lblError.CssClass = "errorlabels"
                            txt_Dis_Commission.Focus()
                            Exit Sub
                        Else
                            VCommison = GV.parseString(txt_Dis_Commission.Text.Trim)
                        End If
                    End If

                    If ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0 Then
                        V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Distributor_CommsissionType.SelectedValue.Trim)
                        V_Sub_Dis_Commission = "0"
                    Else
                        V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Distributor_CommsissionType.SelectedValue.Trim)
                        If txt_Sub_Dis_Commission.Text = "" Then
                            lblError.Text = "Please Enter Dis Commission."
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

                    If ddl_Customer_CommsissionType.SelectedIndex = 0 Then
                        V_Customer_CommissionType = GV.parseString(ddl_Customer_CommsissionType.SelectedValue.Trim)
                        V_Customer_Commission = "0"
                    Else
                        V_Customer_CommissionType = GV.parseString(ddl_Customer_CommsissionType.SelectedValue.Trim)
                        If txt_Customer_Commission.Text = "" Then
                            lblError.Text = "Please Enter Customer Commission."
                            lblError.CssClass = "errorlabels"
                            txt_Customer_Commission.Focus()
                            Exit Sub
                        Else
                            V_Customer_Commission = GV.parseString(txt_Customer_Commission.Text.Trim)
                        End If
                    End If



                    If ddlCanChange.SelectedIndex = 0 Then
                        lblError.Text = "Please Select Can Change."
                        lblError.CssClass = "errorlabels"
                        ddlCanChange.Focus()
                        Exit Sub
                    Else
                        VCanChange = GV.parseString(ddlCanChange.SelectedValue.Trim)
                    End If

                Else
                    VCanChange = GV.parseString(ddlCanChange.SelectedValue.Trim)
                    VCommisonType = GV.parseString(ddl_Distributor_CommsissionType.SelectedValue.Trim)
                    VCommison = "0"
                    V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Distributor_CommsissionType.SelectedValue.Trim)
                    V_Sub_Dis_Commission = "0"

                    V_Retailer_CommissionType = GV.parseString(ddl_Retailer_CommsissionType.SelectedValue.Trim)
                    V_Retailer_Commission = "0"

                    V_Customer_CommissionType = GV.parseString(ddl_Customer_CommsissionType.SelectedValue.Trim)
                    V_Customer_Commission = "0"
                End If
            Else
                VCanChange = GV.parseString(ddlCanChange.SelectedValue.Trim)
                VContainCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
                VServiceType = GV.parseString(ddlServiceCharge.SelectedValue.Trim)
                VCommisonType = GV.parseString(ddl_Distributor_CommsissionType.SelectedValue.Trim)
                VCommison = "0"
                V_Sub_Dis_CommissionType = GV.parseString(ddl_Sub_Distributor_CommsissionType.SelectedValue.Trim)
                V_Sub_Dis_Commission = "0"

                V_Retailer_CommissionType = GV.parseString(ddl_Retailer_CommsissionType.SelectedValue.Trim)
                V_Retailer_Commission = "0"

                V_Customer_CommissionType = GV.parseString(ddl_Customer_CommsissionType.SelectedValue.Trim)
                V_Customer_Commission = "0"




                VServiceCharge = "0"

            End If



            VStatus = GV.parseString(ddlStatus.SelectedValue.Trim)
            VSlabApplicable = ddlSlabApplicable.SelectedValue
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then

                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='" & Vtitle & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " Title Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster (Customer_CommissionType,Customer_Commission,SlabApplicable,CommissionType,Commission,Sub_Dis_CommissionType,Sub_Dis_Commission,Retailer_CommissionType,Retailer_Commission,ServiceType,ServiceCharge,CanChange,ContainCategory,ProductType,Title,ActiveStatus,UpdatedBy,UpdatedOn) values('" & V_Customer_CommissionType & "'," & V_Customer_Commission & ",'" & VSlabApplicable & "','" & VCommisonType & "','" & VCommison & "','" & V_Sub_Dis_CommissionType & "'," & V_Sub_Dis_Commission & ",'" & V_Retailer_CommissionType & "'," & V_Retailer_Commission & ",'" & VServiceType & "'," & VServiceCharge & ",'" & VCanChange & "','" & VContainCategory & "','" & Vtype & "','" & Vtitle & "','" & VStatus & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        Clear()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"
                        ddlTitle.Focus()
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then
                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where   (Title='" & Vtitle & "' and not Title='" & GV.parseString(lblUpadate.Text) & "')") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                Else
                    ' Session("EditFlag") = 0
                    QryStr = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster set SlabApplicable='" & VSlabApplicable & "', ServiceType='" & VServiceType & "',ServiceCharge=" & VServiceCharge & ",Sub_Dis_CommissionType='" & V_Sub_Dis_CommissionType & "',Sub_Dis_Commission=" & V_Sub_Dis_Commission & ",Retailer_CommissionType='" & V_Retailer_CommissionType & "',Retailer_Commission=" & V_Retailer_Commission & ",Customer_CommissionType='" & V_Customer_CommissionType & "',Customer_Commission=" & V_Customer_Commission & ",CommissionType='" & VCommisonType & "',Commission=" & VCommison & ",CanChange='" & VCanChange & "',ContainCategory='" & VContainCategory & "',ProductType='" & Vtype & "', Title='" & Vtitle & "',ActiveStatus='" & VStatus & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"

                    QryStr = QryStr & "  delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_Agents  where  APIName='" & Vtitle.Trim & "' "



                    If GV.FL.DMLQueriesBulk(QryStr) = True Then
                        lblSessionFlag.Text = 0
                        Bind()
                        Clear()
                        ddlType_SelectedIndexChanged(sender, e)
                        ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)



                        ddlServiceCharge_SelectedIndexChanged(sender, e)
                        lblError.Text = "Record Updated Successfully."
                        lblError.CssClass = "Successlabels"
                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try

            ddlTitle.SelectedIndex = 0
            ddlTitle.Enabled = True

            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""
            ddlType.SelectedIndex = 0
            ddlServiceCharge.SelectedIndex = 0

            ddl_Distributor_CommsissionType.SelectedIndex = 0
            ddl_Distributor_CommsissionType.Enabled = False
            ddl_Distributor_CommsissionType.CssClass = "form-control"
            txt_Dis_Commission.Text = "0"
            txt_Dis_Commission.Enabled = False
            txt_Dis_Commission.CssClass = "form-control"

            ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0
            ddl_Sub_Distributor_CommsissionType.Enabled = False
            ddl_Sub_Distributor_CommsissionType.CssClass = "form-control"
            txt_Sub_Dis_Commission.Text = "0"
            txt_Sub_Dis_Commission.Enabled = False
            txt_Sub_Dis_Commission.CssClass = "form-control"

            ddl_Retailer_CommsissionType.SelectedIndex = 0
            ddl_Retailer_CommsissionType.Enabled = False
            ddl_Retailer_CommsissionType.CssClass = "form-control"
            txt_Retailer_Commission.Text = "0"
            txt_Retailer_Commission.Enabled = False
            txt_Retailer_Commission.CssClass = "form-control"

            ddl_Customer_CommsissionType.SelectedIndex = 0
            ddl_Customer_CommsissionType.Enabled = False
            ddl_Customer_CommsissionType.CssClass = "form-control"
            txt_Customer_Commission.Text = "0"
            txt_Customer_Commission.Enabled = False
            txt_Customer_Commission.CssClass = "form-control"

            ddlContainCategory.SelectedIndex = 0
            ddlCanChange.SelectedIndex = 0
            ddlStatus.SelectedIndex = 0

            txtServiceCharge.Text = "0"
            ddlCanChange.Enabled = False
            ddlCanChange.CssClass = "form-control"

            ddlContainCategory.Enabled = False
            ddlContainCategory.CssClass = "form-control"

            ddlServiceCharge.Enabled = False
            ddlServiceCharge.CssClass = "form-control"
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"
            ' lblStarCommissionType.Visible = False
            lblStarServiceType.Visible = False
            lblStarCanChange.Visible = False
            lblStarContain.Visible = False
            ddlSlabApplicable.SelectedIndex = 0
            ddlSlabApplicable.Enabled = False


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)

            ddlServiceCharge_SelectedIndexChanged(sender, e)

            Bind()

        Catch ex As Exception

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
            ddlTitle.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) = "&nbsp;" Then
                ddlType.SelectedIndex = 0
            Else
                ddlType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            End If

            ddlType_SelectedIndexChanged(sender, e)
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text) = "&nbsp;" Then
                ddlContainCategory.SelectedIndex = 0
            Else
                ddlContainCategory.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
            End If
            ddlContainCategory_SelectedIndexChanged(sender, e)
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text) = "&nbsp;" Then
                ddlServiceCharge.SelectedIndex = 0
            Else
                ddlServiceCharge.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)
            End If

            ddlServiceCharge_SelectedIndexChanged(sender, e)
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text) = "&nbsp;" Then
                txtServiceCharge.Text = 0
            Else
                txtServiceCharge.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            End If

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text) = "&nbsp;" Then
                ddl_Distributor_CommsissionType.SelectedIndex = 0
            Else
                ddl_Distributor_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)
            End If

            ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Dis_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)


            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text) = "&nbsp;" Then
                ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0
            Else
                ddl_Sub_Distributor_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            End If
            ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Sub_Dis_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)

            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text) = "&nbsp;" Then
                ddl_Retailer_CommsissionType.SelectedIndex = 0
            Else
                ddl_Retailer_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(11).Text)
            End If

            ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Retailer_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(12).Text)


            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(13).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(13).Text) = "&nbsp;" Then
                ddl_Customer_CommsissionType.SelectedIndex = 0
            Else
                ddl_Customer_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(13).Text)
            End If

            ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
            txt_Customer_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(14).Text)




            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(15).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(15).Text) = "&nbsp;" Then
                ddlCanChange.SelectedIndex = 0
            Else
                ddlCanChange.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(15).Text)
            End If
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(16).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(16).Text) = "&nbsp;" Then
                ddlStatus.SelectedIndex = 0
            Else
                ddlStatus.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(16).Text)
            End If

            ddlSlabApplicable.SelectedValue = GridView1.Rows(gvrow.RowIndex).Cells(17).Text

            'Session("Editflag") = 1

            ddlTitle.Enabled = False

            lblSessionFlag.Text = 1
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.Text = ""
            lblError.CssClass = ""

        Catch ex As Exception
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

        End Try
    End Sub

    Protected Sub btnDeleteRow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not lblRID.Text = "" Then
                QryStr = "delete from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where RID=" & lblRID.Text & ""
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

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
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

    Protected Sub ImagebtnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToExcel_New(GridView1, Response, "", "ServiceAPIMaster", "select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "ServiceAPIMaster", "select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "ServiceAPIMaster", "select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub ddlContainCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContainCategory.SelectedIndexChanged
        Try
            ddl_Distributor_CommsissionType.Enabled = False
            ddl_Distributor_CommsissionType.CssClass = "form-control"
            txt_Dis_Commission.Enabled = False
            txt_Dis_Commission.CssClass = "form-control"

            ddl_Sub_Distributor_CommsissionType.Enabled = False
            ddl_Sub_Distributor_CommsissionType.CssClass = "form-control"
            txt_Sub_Dis_Commission.Enabled = False
            txt_Sub_Dis_Commission.CssClass = "form-control"

            ddl_Retailer_CommsissionType.Enabled = False
            ddl_Retailer_CommsissionType.CssClass = "form-control"
            txt_Retailer_Commission.Enabled = False
            txt_Retailer_Commission.CssClass = "form-control"

            ddl_Customer_CommsissionType.Enabled = False
            ddl_Customer_CommsissionType.CssClass = "form-control"
            txt_Customer_Commission.Enabled = False
            txt_Customer_Commission.CssClass = "form-control"

            ddlServiceCharge.Enabled = False
            ddlServiceCharge.CssClass = "form-control"
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"
            ' lblStarCommissionType.Visible = False
            lblStarServiceType.Visible = False
            ddlCanChange.Enabled = False
            lblStarCanChange.Visible = False
            ddlCanChange.CssClass = "form-control"

            If Not ddlContainCategory.SelectedIndex = 0 Then
                If ddlContainCategory.SelectedValue.Trim.ToUpper = "Yes".Trim.ToUpper Then
                    ddlServiceCharge_SelectedIndexChanged(sender, e)
                    ddlCanChange.Enabled = True
                    ddlCanChange.CssClass = "form-control"

                    ' lblStarCommissionType.Visible = False
                    ddl_Distributor_CommsissionType.Enabled = False
                    ddl_Distributor_CommsissionType.CssClass = "form-control"
                    txt_Dis_Commission.Enabled = False
                    txt_Dis_Commission.CssClass = "form-control"


                    ddl_Sub_Distributor_CommsissionType.Enabled = False
                    ddl_Sub_Distributor_CommsissionType.CssClass = "form-control"
                    txt_Sub_Dis_Commission.Enabled = False
                    txt_Sub_Dis_Commission.CssClass = "form-control"

                    ddl_Retailer_CommsissionType.Enabled = False
                    ddl_Retailer_CommsissionType.CssClass = "form-control"
                    txt_Retailer_Commission.Enabled = False
                    txt_Retailer_Commission.CssClass = "form-control"


                    ddl_Customer_CommsissionType.Enabled = False
                    ddl_Customer_CommsissionType.CssClass = "form-control"
                    txt_Customer_Commission.Enabled = False
                    txt_Customer_Commission.CssClass = "form-control"


                    'Customer


                    lblStarCanChange.Visible = True
                    lblStarServiceType.Visible = True

                    ddlServiceCharge.Enabled = True
                    ddlServiceCharge.CssClass = "form-control"
                    'txtServiceCharge.Enabled = True
                    'txtServiceCharge.CssClass = "form-control"
                ElseIf ddlContainCategory.SelectedValue.Trim.ToUpper = "No".Trim.ToUpper Then
                    ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
                    ddl_Distributor_CommsissionType.Enabled = True
                    ddl_Distributor_CommsissionType.CssClass = "form-control"

                    ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
                    ddl_Sub_Distributor_CommsissionType.Enabled = True
                    ddl_Sub_Distributor_CommsissionType.CssClass = "form-control"

                    ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)
                    ddl_Retailer_CommsissionType.Enabled = True
                    ddl_Retailer_CommsissionType.CssClass = "form-control"


                    ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
                    ddl_Customer_CommsissionType.Enabled = True
                    ddl_Customer_CommsissionType.CssClass = "form-control"


                    ddlServiceCharge_SelectedIndexChanged(sender, e)
                    ddlCanChange.Enabled = True
                    lblStarCanChange.Visible = True
                    ddlCanChange.CssClass = "form-control"
                    '  lblStarCommissionType.Visible = True
                    lblStarContain.Visible = True
                    lblStarServiceType.Visible = True

                    'txtCommssion.Enabled = True
                    'txtCommssion.CssClass = "form-control"
                    ddlServiceCharge.Enabled = True
                    ddlServiceCharge.CssClass = "form-control"
                    'txtServiceCharge.Enabled = True
                    'txtServiceCharge.CssClass = "form-control"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try
            ddlCanChange.SelectedIndex = 0
            ddlServiceCharge.SelectedIndex = 0
            ddlServiceCharge_SelectedIndexChanged(sender, e)

            ddlContainCategory.SelectedIndex = 0
            ddlCanChange.Enabled = False
            ddlCanChange.CssClass = "form-control"

            ddl_Distributor_CommsissionType.SelectedIndex = 0
            ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Distributor_CommsissionType.Enabled = False
            ddl_Distributor_CommsissionType.CssClass = "form-control"
            txt_Dis_Commission.Enabled = False
            txt_Dis_Commission.CssClass = "form-control"

            ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0
            ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Sub_Distributor_CommsissionType.Enabled = False
            ddl_Sub_Distributor_CommsissionType.CssClass = "form-control"
            txt_Sub_Dis_Commission.Enabled = False
            txt_Sub_Dis_Commission.CssClass = "form-control"

            ddl_Retailer_CommsissionType.SelectedIndex = 0
            ddl_Retailer_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Retailer_CommsissionType.Enabled = False
            ddl_Retailer_CommsissionType.CssClass = "form-control"
            txt_Retailer_Commission.Enabled = False
            txt_Retailer_Commission.CssClass = "form-control"


            ddl_Customer_CommsissionType.SelectedIndex = 0
            ddl_Customer_CommsissionType_SelectedIndexChanged(sender, e)
            ddl_Customer_CommsissionType.Enabled = False
            ddl_Customer_CommsissionType.CssClass = "form-control"
            txt_Customer_Commission.Enabled = False
            txt_Customer_Commission.CssClass = "form-control"


            ddlContainCategory.Enabled = False
            ddlContainCategory.CssClass = "form-control"

            ddlServiceCharge.Enabled = False
            ddlServiceCharge.CssClass = "form-control"
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"
            'lblStarCommissionType.Visible = False
            lblStarServiceType.Visible = False
            lblStarCanChange.Visible = False
            lblStarContain.Visible = False

            ddlSlabApplicable.Enabled = False



            If Not ddlType.SelectedIndex = 0 Then
                If ddlType.SelectedValue.Trim.ToUpper = "Service".Trim.ToUpper Then
                    'lblStarCommissionType.Visible = False
                    lblStarServiceType.Visible = False
                    lblStarContain.Visible = False
                    ddl_Distributor_CommsissionType.Enabled = False
                    ddl_Distributor_CommsissionType.CssClass = "form-control"
                    txt_Dis_Commission.Enabled = False
                    txt_Dis_Commission.CssClass = "form-control"

                    ddl_Sub_Distributor_CommsissionType.Enabled = False
                    ddl_Sub_Distributor_CommsissionType.CssClass = "form-control"
                    txt_Sub_Dis_Commission.Enabled = False
                    txt_Sub_Dis_Commission.CssClass = "form-control"

                    ddl_Retailer_CommsissionType.Enabled = False
                    ddl_Retailer_CommsissionType.CssClass = "form-control"
                    txt_Retailer_Commission.Enabled = False
                    txt_Retailer_Commission.CssClass = "form-control"

                    ddl_Customer_CommsissionType.Enabled = False
                    ddl_Customer_CommsissionType.CssClass = "form-control"
                    txt_Customer_Commission.Enabled = False
                    txt_Customer_Commission.CssClass = "form-control"


                    ddlServiceCharge.Enabled = False
                    ddlServiceCharge.CssClass = "form-control"
                    txtServiceCharge.Enabled = False
                    txtServiceCharge.CssClass = "form-control"
                    ddlContainCategory.Enabled = False
                    ddlContainCategory.CssClass = "form-control"
                ElseIf ddlType.SelectedValue.Trim.ToUpper = "API".Trim.ToUpper Then
                    ddl_Distributor_CommsissionType_SelectedIndexChanged(sender, e)
                    ddlServiceCharge_SelectedIndexChanged(sender, e)
                    ' lblStarCommissionType.Visible = False
                    lblStarServiceType.Visible = False
                    lblStarContain.Visible = True
                    ddl_Distributor_CommsissionType.Enabled = False
                    ddl_Distributor_CommsissionType.CssClass = "form-control"

                    ddl_Sub_Distributor_CommsissionType.Enabled = False
                    ddl_Sub_Distributor_CommsissionType.CssClass = "form-control"

                    ddl_Retailer_CommsissionType.Enabled = False
                    ddl_Retailer_CommsissionType.CssClass = "form-control"


                    ddl_Customer_CommsissionType.Enabled = False
                    ddl_Customer_CommsissionType.CssClass = "form-control"

                    ddlContainCategory.CssClass = "form-control"
                    'txtCommssion.Enabled = False
                    'txtCommssion.CssClass = "form-control"
                    ddlServiceCharge.Enabled = False
                    ddlServiceCharge.CssClass = "form-control"
                    'txtServiceCharge.Enabled = False
                    'txtServiceCharge.CssClass = "form-control"
                End If
            Else

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlServiceCharge_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServiceCharge.SelectedIndexChanged
        Try
            txtServiceCharge.Enabled = True
            txtServiceCharge.CssClass = "form-control"
            If ddlServiceCharge.SelectedIndex = 0 Then
                txtServiceCharge.Text = "0"
                txtServiceCharge.Enabled = False
                txtServiceCharge.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub ddlCommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommsissionType.SelectedIndexChanged
    '    Try
    '        txtCommssion.Enabled = True
    '        txtCommssion.CssClass = "form-control"
    '        If ddlCommsissionType.SelectedIndex = 0 Then
    '            txtCommssion.Text = "0"
    '            txtCommssion.Enabled = False
    '            txtCommssion.CssClass = "form-control"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub



    Protected Sub ddl_Distributor_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Distributor_CommsissionType.SelectedIndexChanged
        Try
            txt_Dis_Commission.Enabled = True
            txt_Dis_Commission.CssClass = "form-control"
            If ddl_Distributor_CommsissionType.SelectedIndex = 0 Then
                txt_Dis_Commission.Text = "0"
                txt_Dis_Commission.Enabled = False
                txt_Dis_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Sub_Distributor_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sub_Distributor_CommsissionType.SelectedIndexChanged
        Try
            txt_Sub_Dis_Commission.Enabled = True
            txt_Sub_Dis_Commission.CssClass = "form-control"
            If ddl_Sub_Distributor_CommsissionType.SelectedIndex = 0 Then
                txt_Sub_Dis_Commission.Text = "0"
                txt_Sub_Dis_Commission.Enabled = False
                txt_Sub_Dis_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception

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

        End Try
    End Sub



    Private Sub ddl_Customer_CommsissionType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Customer_CommsissionType.SelectedIndexChanged
        Try
            txt_Customer_Commission.Enabled = True
            txt_Customer_Commission.CssClass = "form-control"
            If ddl_Customer_CommsissionType.SelectedIndex = 0 Then
                txt_Customer_Commission.Text = "0"
                txt_Customer_Commission.Enabled = False
                txt_Customer_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlTitle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTitle.SelectedIndexChanged
        Try
            Dim Title, ProductType, ContainCategory, SlabApplicable As String

            If Not ddlTitle.SelectedIndex = 0 Then

                Dim dd As String = GV.FL.AddInVar("Title+'*'+ProductType+'*'+ContainCategory+'*'+SlabApplicable", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where Title='" & ddlTitle.SelectedValue & "'")


                If Not dd.Trim = "" Then
                    Dim dd1() As String = dd.Split("*")
                    Title = dd1(0)
                    ProductType = dd1(1)
                    ContainCategory = dd1(2)
                    SlabApplicable = dd1(3)

                    ddlType.SelectedValue = ProductType
                    ddlType_SelectedIndexChanged(sender, e)

                    ddlContainCategory.SelectedValue = ContainCategory
                    ddlContainCategory_SelectedIndexChanged(sender, e)

                    ddlSlabApplicable.SelectedValue = SlabApplicable

                    ddlType.Enabled = False
                    ddlContainCategory.Enabled = False
                    ddlSlabApplicable.Enabled = False

                End If

            Else

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class