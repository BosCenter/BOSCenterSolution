
Public Class BOS_OperatorWiseCommission_SA
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'InsertOperator()
                'Exit Sub
                GV.FL.AddInDropDownListDistinct(ddlProductService, "Title", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where ContainCategory='Yes' and ProductType='API' ")
                If ddlProductService.Items.Count > 0 Then
                    ddlProductService.Items.Insert(0, "Select Service")
                Else
                    ddlProductService.Items.Add("Select Service")
                End If
                ddlContainCategory.Items.Add("Not Applicable")
                lblSessionFlag.Text = 0

                ddl_SA_CommsissionType.SelectedIndex = 0
                ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)

                ddlServiceCharge.SelectedIndex = 0
                ddlServiceCharge_SelectedIndexChanged(sender, e)

                Bind()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Dim str As String
    Dim VServiceType, VServiceCharge As String

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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub Bind()
        Try
            Dim API As String = ""
            Dim Category As String = ""
            If Not ddlProductService.SelectedIndex = 0 Then
                API = " where APIName ='" & GV.parseString(ddlProductService.SelectedValue.Trim) & "' "
            End If
            If Not ddlContainCategory.SelectedIndex = 0 Then
                Category = " And Category ='" & GV.parseString(ddlContainCategory.SelectedValue.Trim) & "' "
            End If
            str = "select RID as SrNo,* from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA " & API & " " & Category & "  order by rid desc "
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

            lblError.Text = ""
            lblError.CssClass = ""
            Dim VAPIName, VCategory, VCode, VStatus, VOperatorName, V_SA_CommissionType, V_SA_Commission As String

            V_SA_Commission = ""
            V_SA_CommissionType = ""

            If ddlProductService.SelectedIndex = 0 Then
                lblError.Text = "Please Select API Name."
                lblError.CssClass = "errorlabels"
                ddlProductService.Focus()
                Exit Sub
            Else
                VAPIName = GV.parseString(ddlProductService.SelectedValue.Trim)
            End If

            If ddlContainCategory.SelectedValue.Trim.ToUpper = "Not Applicable".Trim.ToUpper Then
                VCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
            Else
                If ddlContainCategory.SelectedIndex = 0 Then
                    lblError.Text = "Please Select Category."
                    lblError.CssClass = "errorlabels"
                    ddlContainCategory.Focus()
                    Exit Sub
                Else
                    VCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
                End If
            End If

            If txtCode.Text = "" Then
                lblError.Text = "Please Enter Code."
                lblError.CssClass = "errorlabels"
                txtCode.Focus()
                Exit Sub
            Else
                VCode = GV.parseString(txtCode.Text.Trim)
            End If
            If txtOperatorName.Text = "" Then
                lblError.Text = "Please Enter Operator Name."
                lblError.CssClass = "errorlabels"
                txtOperatorName.Focus()
                Exit Sub
            Else
                VOperatorName = GV.parseString(txtOperatorName.Text.Trim)
            End If

            If ddl_SA_CommsissionType.SelectedIndex = 0 Then
                V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
            Else
                If txt_SA_Commission.Text = "" Then
                    lblError.Text = "Please Enter Dis Commission."
                    lblError.CssClass = "errorlabels"
                    txt_SA_Commission.Focus()
                    Exit Sub
                Else
                    V_SA_Commission = GV.parseString(txt_SA_Commission.Text.Trim)
                End If
                V_SA_CommissionType = GV.parseString(ddl_SA_CommsissionType.SelectedValue.Trim)
            End If
            If GV.parseString(V_SA_Commission.Trim) = "" Then
                V_SA_Commission = "0"
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



            '/////////////////////////



            '////////////////////////////




            VStatus = GV.parseString(ddlStatus.SelectedValue.Trim)

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"

            If lblSessionFlag.Text = 0 Then
                'VCode
                If GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA where APIName='" & VAPIName & "' and Category='" & VCategory & "' and Code='" & VCode & "'") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = " Operator Code Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub

                Else
                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA (ServiceType,ServiceCharge,APIName,Category,Code,OperatorName,SA_CommissionType,SA_Commission,ActiveStatus,UpdatedBy,UpdatedOn) values('" & VServiceType & "'," & VServiceCharge & ",'" & VAPIName & "','" & VCategory & "','" & VCode & "','" & VOperatorName & "','" & V_SA_CommissionType & "'," & V_SA_Commission & ",'" & VStatus & "','" & VUpdatedBy & "'," & VUpdatedOn & " );"
                    If GV.FL.DMLQueries(QryStr) = True Then
                        Bind()
                        txtCode.Text = ""
                        txtOperatorName.Text = ""
                        ddl_SA_CommsissionType.SelectedIndex = 0
                        ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)
                        ddlServiceCharge.SelectedIndex = 0
                        ddlServiceCharge_SelectedIndexChanged(sender, e)

                        ddlStatus.SelectedIndex = 0
                        ' Clear()
                        lblError.Text = "Record Saved Successfully."
                        lblError.CssClass = "Successlabels"

                    Else
                        lblError.Text = "Sorry !! Process Can't be Completed."
                        lblError.CssClass = "errorlabels"
                    End If
                End If

            ElseIf lblSessionFlag.Text = 1 Then

                QryStr = "update " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA set  ServiceType='" & VServiceType & "', ServiceCharge=" & VServiceCharge & " , Code='" & VCode & "',OperatorName='" & VOperatorName & "',SA_CommissionType='" & V_SA_CommissionType & "',SA_Commission=" & V_SA_Commission & ",ActiveStatus='" & VStatus & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn=" & VUpdatedOn & " where RID=" & lblRID.Text.Trim & " ;"

                'QryStr = QryStr & "  delete from BosCenter_DB.dbo.BOS_OperatorWiseCommission_SA_Agents  where  APIName='" & VAPIName & "'   and Category='" & VCategory & "' and Code='" & VCode & "' "




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
                ' End If

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub Clear()
        Try
            ddlProductService.Enabled = True
            ddlProductService.CssClass = "form-control"
            ddlContainCategory.Enabled = True
            ddlContainCategory.CssClass = "form-control"
            '  ddlContainCategory.SelectedIndex = 0
            txt_SA_Commission.Text = "0"
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""

            txtOperatorName.Text = ""
            txtCode.Text = ""
            ddlStatus.SelectedIndex = 0

            ddl_SA_CommsissionType.SelectedIndex = 0
            If ddl_SA_CommsissionType.SelectedIndex = 0 Then
                txt_SA_Commission.Text = "0"
                txt_SA_Commission.Enabled = False
                txt_SA_Commission.CssClass = "form-control"
            End If

            ddlServiceCharge.SelectedIndex = 0
            If ddlServiceCharge.SelectedIndex = 0 Then
                txtServiceCharge.Text = "0"
                txtServiceCharge.Enabled = False
                txtServiceCharge.CssClass = "form-control"
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddlProductService.SelectedIndex = 0
            ddlProductService_SelectedIndexChanged(sender, e)
            Bind()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblSessionFlag.Text = 1
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim
            lblUpadate.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            ddlProductService.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)

            lblCategory.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
            ddlProductService_SelectedIndexChanged(sender, e)

            ddlContainCategory.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text).Trim.ToUpper
            txtCode.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)

            txtOperatorName.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text)

            ddl_SA_CommsissionType.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
            ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)
            txt_SA_Commission.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(7).Text)


            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text) = "&nbsp;" Then
                ddlServiceCharge.SelectedIndex = 0
            Else
                ddlServiceCharge.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
            End If

            ddlServiceCharge_SelectedIndexChanged(sender, e)
            If GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text) = "" Or GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text) = "&nbsp;" Then
                txtServiceCharge.Text = 0
            Else
                txtServiceCharge.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(9).Text)
            End If



            ddlStatus.SelectedValue = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(10).Text)

            'Session("Editflag") = 1
            ddlProductService.Enabled = False
            ddlProductService.CssClass = "form-control"
            ddlContainCategory.Enabled = False
            ddlContainCategory.CssClass = "form-control"
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            lblError.Text = ""
            lblError.CssClass = ""

            txtCode.Focus()


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

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
                QryStr = "delete from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA where RID=" & lblRID.Text & ""
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
                GV.ExportToExcel_New(GridView1, Response, "", "OperatorWiseCommission", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "OperatorWiseCommission", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "OperatorWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub ddlProductService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProductService.SelectedIndexChanged
        Try
            ddlContainCategory.Items.Clear()
            GV.FL.AddInDropDownListDistinct(ddlContainCategory, "upper(Category)", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA where ProductService='" & GV.parseString(ddlProductService.SelectedValue.Trim) & "'  ")
            If ddlContainCategory.Items.Count > 0 Then
                ddlContainCategory.Items.Insert(0, "Select Category")
            Else
                ddlContainCategory.Items.Add("Not Applicable")
            End If
            If Not lblSessionFlag.Text = 1 Then
                Bind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Protected Sub ddlContainCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContainCategory.SelectedIndexChanged
        Try

            If Not lblSessionFlag.Text = 1 Then
                Bind()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub add_Recharge_Operators(ByVal ServiceName As String)
        Try


            Dim OperatorList() As String
            Dim OperatorCount As Integer = 0

            If ServiceName.Trim.ToUpper = "Mobile".ToUpper Then

                OperatorList = {"AR:AIRTEL", "BS:BSNL", "ID:IDEA ", "VF:VODAFONE ", "RJ:RELIANCE JIO", "TI:TATA INDICOM", "TD:TATA DOCOMO", "AI:AIRCEL", "TE:TELENOR", "VG:VIRGIN GSM", "VC:VIRGIN CDMA", "MTS:MTS", "DL:TATA DOCOMO CDMA LANDLINE", "BR:BSNL VALIDITY/SPECIAL", "TB:DOCOMO GSM SPECIAL", "UN:UNINOR", "NS:UNINOR SPECIAL", "BSK:BSNL TOPUP (J&amp;K)", "BSJ:BSNL SPECIAL ( J&amp;K )", "JKI:J&amp;K ( IDEA EXPRESS )", "JKJ:JIO-JK"}

            ElseIf ServiceName.Trim.ToUpper = "dth".ToUpper Then

                OperatorList = {"AD:AIRTEL DTH", "BT:BIG TV DTH", "DT:DISH TV DTH", "TS:TATA SKY DTH", "VD:VIDEOCON DTH", "ST:SUN TV DTH"}

            ElseIf ServiceName.Trim.ToUpper = "Landline".ToUpper Then


                OperatorList = {"AL:AIRTEL LANDLINE", "BL:BSNL - CORPORATE", "ML:MTNL - MUMBAI", "BIL:BSNL - INDIVIDUAL"}


            ElseIf ServiceName.Trim.ToUpper = "Postpaid".ToUpper Then


                OperatorList = {"AP:AIRTEL BILL", "BP:BSNL BILL", "IP:IDEA BILL ", "VP:VODAFONE BILL ", "RP:RELIANCE BILL", "TP:TATA BILL", "AIP:AIRCLE BILL"}


            ElseIf ServiceName.Trim.ToUpper = "Electricity".ToUpper Then

                OperatorList = {"PGE:PASCHIM GUJARAT VIJ COMPANY LIMITED PGVCL", "MGE:MADHYA GUJARAT VIJ COMPANY LIMITED (MGVCL)", "UGE:UTTAR GUJARAT VIJ COMPANY LIMITED (UGVCL)", "DGE:DAKSHIN GUJARAT VIJ COMPANY LIMITED (DGVCL)", "TAE:TORRENT POWER - AGRA", "MSE: MSEDC LIMITED", "REE:ADANI ELECTRICITY MUMBAI LTD", "BRE:BSES RAJDHANI POWER LIMITED", "BYE:BBSES YAMUNA POWER LIMITED", "BYE:BBSES YAMUNA POWER LIMITED", "NDE:TATA POWER-DELHI", "BME:BEST UNDERTAKING - MUMBAI", "NNE:NOIDA POWER COMPANY LIMITED", "TTE:TRIPURA STATE ELECTRICITY CORPORATION LTD", "MPE:MP PASCHIM KSHETRA VIDYUT VITARAN - INDORE", "JUE:JAMSHEDPUR UTILITIES AND SERVICES COMPANY LIMITED", "IBE:INDIA POWER CORPORATION LIMITED - BIHAR", "CCE:CHHATTISGARH STATE ELECTRICITY BOARD", "CWE: CALCUTTA ELECTRICITY SUPPLY LTD (CESC)", "BBE:BANGALORE ELECTRICITY SUPPLY COMPANY", "AAE:ASSAM POWER DISTRIBUTION COMPANY LTD RAPDR", "AVE:AJMER VIDYUT VITRAN NIGAM LIMITED (AVVNL)", "BEE:BHARATPUR ELECTRICITY SERVICES LTD. (BESL)", "BKE:BIKANER ELECTRICITY SUPPLY LIMITED (BKESL)", "DDE:DAMAN AND DIU ELECTRICITY", "DNE:DNH POWER DISTRIBUTION COMPANY LIMITED", "APE:APEPDCL-EASTERN POWER DISTRIBUTION CO AP LTD", "GEE:GULBARGA ELECTRICITY SUPPLY COMPANY LIMITED GESCOM", "IWE:INDIA POWER CORPORATION - WEST BENGAL", "JDE:JODHPUR VIDYUT VITRAN NIGAM LIMITED (JDVVNL)", "JIE:JAIPUR VIDYUT VITRAN NIGAM (JVVNL)", "KTE:KOTA ELECTRICITY DISTRIBUTION LIMITED (KEDL)", "MHE:MEGHALAYA POWER DIST CORP LTD", "MZE:MUZAFFARPUR VIDYUT VITRAN LIMITED", "NBE:NORTH BIHAR POWER DISTRIBUTION COMPANY LTD.", "NSE:NNESCO, ODISHA", "SBE:SOUTH BIHAR POWER DISTRIBUTION COMPANY LTD.", "SDE: SNDL NAGPUR", "STE:SOUTHCO, ODISHA", "ASE:APSPDCL-SOUTHERN POWER DISTRIBUTION CO AP LTD", "TME:TATA POWER - MUMBAI", "TNE:TAMIL NADU ELECTRICITY BOARD (TNEB)", "AJE:TP AJMER DISTRIBUTION LTD (TPADL)", "UKE:UTTARAKHAND POWER CORPORATION LIMITED", "UBE:UTTAR PRADESH POWER CORP LTD (UPPCL) - URBAN", "URE: UTTAR PRADESH POWER CORP LTD (UPPCL) - RURAL", "WSE:WESCO UTILITY", "DHE:DAKSHIN HARYANA BIJLI VITRAN NIGAM (DHBVN)", "PSE:PUNJAB STATE POWER CORPORATION LTD (PSPCL)", "HNE:HUBLI ELECTRICITY SUPPLY COMPANY LTD (HESCOM)", "UHE:UTTAR HARYANA BIJLI VITRAN NIGAM (UHBVN)", "CRE:CHAMUNDESHWARI ELECTRICITY SUPPLY CORP LTD (CESCOM", "HPE:HIMACHAL PRADESH STATE ELECTRICITY BOARD (HPSEB)", "JBL:JHARKHAND BIJLI VITRAN NIGAM LTD (JBVNL)", "WBE:WEST BENGAL STATE ELECTRICITY DISTRIBUTION CO. LTD", "THE:TORRENT POWER - AHMEDABAD", "TBE:TORRENT POWER - BHIWANDI", "TSE:TORRENT POWER - SURAT", "MRE:MP POORV KSHETRA VIDYUT VITARAN - RURAL", "MME:MP MADHYA KSHETRA VIDYUT VITARAN CO. LTD.-RURAL", "MUE:MP MADHYA KSHETRA VIDYUT VITARAN CO. LTD.-URBAN", "TLE:TELANGANA SOUTHERN POWER DISTRIBUTION CO LTD", "SPE:SIKKIM POWER - RURAL (SKMPWR)", "KNE:KANPUR ELECTRICITY SUPPLY COMPANY", "NME:NDMC ELECTRICITY", "GOE:GOA ELECTRICITY DEPARTMENT", "ANE:ASSAM POWER DISTRIBUTION COMPANY LTD (NON-RAPDR)", "MKE:M.P. POORV KSHETRA VIDYUT VITARAN - URBAN", "NNE:DEPARTMENT OF POWER - NAGALAND", "MSD:MESCOM - MANGALORE", "SUE:SIKKIM POWER (URBAN)", "COE:CESU - ODISHA", "KSE:KSEBL - KERALA", "PME:POWER &amp; ELECTRICITY DEPARTMENT - MIZORAM"}


            ElseIf ServiceName.Trim.ToUpper = "Broadband".ToUpper Then

                OperatorList = {"AFB:ACT FIBERNET", "ABB:AIRTEL BROADBAND", "CBB:CONNECT BROADBAND", "HBB:HATHWAY BROADBAND", "NBB:NEXTRA BROADBAND", "SBB:SPECTRANET BROADBAND", "TBB:TIKONA BROADBAND", "TTB:TTN BROADBAND", "DBB:D VOIS COMMUNICATIONS", "ANB:ASIANET BROADBAND", "FBB:FUSIONNET WEB SERVICES", "CWB:COMWAY BROADBAND"}


            ElseIf ServiceName.Trim.ToUpper = "GAS".ToUpper Then

                OperatorList = {"AG:TADANI GAS", "GGCL:GUJARAT GAS COMPANY LTD", "AVG:AAVANTIKA GAS", "CUG:CENTRAL UP GAS LIMITED", "CGG:CHAROTAR GAS SAHAKARI MANDALI", "HCG:HARYANA CITY GAS", "IAG:INDIANOIL - ADANI GAS", "IPG:INDRAPRASTHA GAS", "MMG:MAHANAGAR GAS", "MNG:MAHARASHTRA NATURAL GAS", "SGG:SABARMATI GAS", "SUG:SITI ENERGY", "TNG:TRIPURA NATURAL GAS", "UCG:UNIQUE CENTRAL PIPED GASES", "VGG:VADODARA GAS", "IRG:IRM ENERGY"}


            ElseIf ServiceName.Trim.ToUpper = "Waterbill".ToUpper Then

                OperatorList = {"BKW:BANGALORE WATER SUPPLY AND SEWERAGE BOARD", "BMW:BHOPAL MUNICIPAL CORPORATION", "DDW:DELHI JAL BOARD", "GWW:GREATER WARANGAL MUNICIPAL CORPORATION", "GMW:GWALIOR MUNICIPAL CORPORATION", "HTW:HYDERABAD METROPOLITAN WATER SUPPLY AND SEWERAGE B", "IMW:INDORE MUNICIPAL CORPORATION", "JMW:JABALPUR MUNICIPAL CORPORATION", "JPW:MUNICIPAL CORPORATION JALANDHAR", "JWW:MUNICIPAL CORPORATION LUDHIANA - WATER", "HGW:MUNICIPAL CORPORATION OF GURUGRAM", "NDW:NEW DELHI MUNICIPAL COUNCIL (NDMC)", "PMW:PUNE MUNICIPAL CORPORATION", "SGW:SURAT MUNICIPAL CORPORATION", "UMW:UJJAIN NAGAR NIGAM - PHED", "RBW:URBAN IMPROVEMENT TRUST (UIT) - BHIWADI", "UUW:UTTARAKHAND JAL SANSTHAN", "SMW:SILVASSA MUNICIPAL COUNCIL"}
            End If

            If Not OperatorList Is Nothing Then
                OperatorCount = OperatorList.Length - 1
            Else
                OperatorCount = OperatorCount - 1
            End If





            QryStr = ""
            For i As Integer = 0 To OperatorCount
                Dim str() As String = OperatorList(i).Split(":")
                'ddl.Items.Add("")
                'ddl.Items(i).Value = str(0)
                'ddl.Items(i).Text = GV.parseString(str(1))


                If QryStr.Trim = "" Then
                    QryStr = "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA (APIName,Category,Code,OperatorName,Dis_CommissionType,Dis_Commission,ActiveStatus,UpdatedBy,UpdatedOn) values('Recharge','" & ServiceName & "','" & str(0) & "','" & GV.parseString(str(1)) & "','Percentage',0,'Active','abc',getdate() ) ; "
                Else
                    QryStr = QryStr & " " & "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA (APIName,Category,Code,OperatorName,Dis_CommissionType,Dis_Commission,ActiveStatus,UpdatedBy,UpdatedOn) values('Recharge','" & ServiceName & "','" & str(0) & "','" & GV.parseString(str(1)) & "','Percentage',0,'Active','abc',getdate() ) ; "
                End If

            Next
            GV.FL.DMLQueriesBulk(QryStr)

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Public Sub InsertOperator()
        Try
            Dim category() As String = {"Mobile", "dth", "Landline", "Postpaid", "Electricity", "GAS", "Waterbill", "Broadband"}

            For i As Integer = 0 To category.Length - 1
                add_Recharge_Operators(category(i))
            Next
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Protected Sub ddl_SA_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_SA_CommsissionType.SelectedIndexChanged
        Try
            txt_SA_Commission.Enabled = True
            txt_SA_Commission.CssClass = "form-control"
            If ddl_SA_CommsissionType.SelectedIndex = 0 Then
                txt_SA_Commission.Text = "0"
                txt_SA_Commission.Enabled = False
                txt_SA_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class