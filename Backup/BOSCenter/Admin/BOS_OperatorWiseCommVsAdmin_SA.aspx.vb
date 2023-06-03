
Public Class BOS_OperatorWiseCommVsAdmin_SA
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Dim vcountry, VState, VUpdatedBy, VUpdatedOn As String
    Dim QryStr As String = ""
    Dim VServiceType, VServiceCharge As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'InsertOperator()
                'Exit Sub

                GV.FL.AddInDropDownListAll(ddl_Admin, "CompanyCode +':'+ CompanyName", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration order by RID Desc")
                If ddl_Admin.Items.Count > 0 Then
                    ddl_Admin.Items.Insert(0, ":: Select Admin ::")
                Else
                    ddl_Admin.Items.Add(":: Select Admin ::")
                End If
                ddl_Admin_SelectedIndexChanged(sender, e)



                GV.FL.AddInDropDownListDistinct(ddlProductService, "Title", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where ContainCategory='Yes' and ProductType='API' ")
                If ddlProductService.Items.Count > 0 Then
                    ddlProductService.Items.Insert(0, ":: Select Service ::")
                Else
                    ddlProductService.Items.Add(":: Select Service ::")
                End If
                ddlProductService_SelectedIndexChanged(sender, e)

                lblSessionFlag.Text = 0

                ddl_AD_CommsissionType.SelectedIndex = 0
                ddl_SA_CommsissionType_SelectedIndexChanged(sender, e)

                ddlServiceCharge.SelectedIndex = 0
                ddlServiceCharge_SelectedIndexChanged(sender, e)


                Bind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddl_Admin_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Admin.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            Bind()
            ddlProductService.SelectedIndex = 0
            ddlProductService_SelectedIndexChanged(sender, e)


        Catch ex As Exception

        End Try
    End Sub


    Dim str As String

    Public Sub Bind()
        Try
            Dim API As String = ""
            Dim Category As String = ""


            If Not ddl_Admin.SelectedIndex = 0 Then

                Dim AdminID() As String = ddl_Admin.SelectedValue.Split(":")

                Dim WhereClause As String = ""

                If ddlProductService.SelectedIndex = 0 And ddlContainCategory.SelectedIndex = 0 Then
                    WhereClause = ""
                ElseIf Not ddlProductService.SelectedIndex = 0 And Not ddlContainCategory.SelectedIndex = 0 Then
                    WhereClause = " where OWSA.APIName='" & GV.parseString(ddlProductService.SelectedValue.Trim) & "' and OWSA.Category='" & GV.parseString(ddlContainCategory.SelectedValue.Trim) & "' "
                ElseIf Not ddlProductService.SelectedIndex = 0 And ddlContainCategory.SelectedIndex = 0 Then
                    WhereClause = " where OWSA.APIName='" & GV.parseString(ddlProductService.SelectedValue.Trim) & "'  "
                End If

                str = "select '0' as 'SrNo',APIName,Category,Code,OperatorName,SA_CommissionType as 'SA_Comm_Type' ,SA_Commission as 'SA_Comm',"
                str = str & "isnull((select top 1 Admin_CommissionType from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA OWAD where AdminID='" & AdminID(0) & "' and OWAD.Code=OWSA.Code and OWAD.OperatorName=OWSA.OperatorName and OWAD.APIName=OWSA.APIName and OWAD.Category=OWSA.Category),'NA') as 'AD_Comm_Type',"
                str = str & "isnull((select top 1 Admin_Commission from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA OWAD where AdminID='" & AdminID(0) & "' and OWAD.Code=OWSA.Code and OWAD.OperatorName=OWSA.OperatorName and OWAD.APIName=OWSA.APIName and OWAD.Category=OWSA.Category),0) as 'AD_Comm',"

                str = str & "isnull((select top 1 ServiceType from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA OWAD where AdminID='" & AdminID(0) & "' and OWAD.Code=OWSA.Code and OWAD.OperatorName=OWSA.OperatorName and OWAD.APIName=OWSA.APIName and OWAD.Category=OWSA.Category),'NA') as 'AD_Service_Type',"
                str = str & "isnull((select top 1 ServiceCharge from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA OWAD where AdminID='" & AdminID(0) & "' and OWAD.Code=OWSA.Code and OWAD.OperatorName=OWSA.OperatorName and OWAD.APIName=OWSA.APIName and OWAD.Category=OWSA.Category),0) as 'Service_Charge',"

                str = str & "isnull((select top 1 ActiveStatus from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA OWAD where AdminID='" & AdminID(0) & "' and OWAD.Code=OWSA.Code and OWAD.OperatorName=OWSA.OperatorName and OWAD.APIName=OWSA.APIName and OWAD.Category=OWSA.Category),'NA') as 'Status'"
                str = str & " from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA OWSA  " & WhereClause & " order by rid desc "
                ds = GV.FL.OpenDsWithSelectQuery(str)
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()
                If GridView1.Rows.Count > 0 Then
                    GV.FL.showSerialnoOnGridView(GridView1, 0)
                End If
            Else
                GridView1.DataSource = Nothing
                GridView1.DataBind()
            End If



        Catch ex As Exception
        End Try
    End Sub
    Dim statecode As String
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            Dim VAPIName, VCategory, VStatus, V_SA_CommissionType, V_SA_Commission, V_AD_CommissionType, V_AD_Commission, VAdminID As String

            V_SA_Commission = "0"
            V_SA_CommissionType = ""

            V_AD_Commission = "0"
            V_AD_CommissionType = ""


            If ddl_Admin.SelectedIndex = 0 Then
                lblError.Text = "Please Select Admin."
                lblError.CssClass = "errorlabels"
                ddl_Admin.Focus()
                Exit Sub
            Else
                Dim yy() As String = ddl_Admin.SelectedValue.Trim.Split(":")
                VAdminID = yy(0).Trim
            End If


            If ddlProductService.SelectedIndex = 0 Then
                lblError.Text = "Please Select API Name."
                lblError.CssClass = "errorlabels"
                ddlProductService.Focus()
                Exit Sub
            Else
                VAPIName = GV.parseString(ddlProductService.SelectedValue.Trim)
            End If

            If ddlContainCategory.SelectedIndex = 0 Then
                lblError.Text = "Please Select Category."
                lblError.CssClass = "errorlabels"
                ddlContainCategory.Focus()
                Exit Sub
            Else
                VCategory = GV.parseString(ddlContainCategory.SelectedValue.Trim)
            End If


            If ddl_AD_CommsissionType.SelectedIndex = 0 Then
                V_SA_CommissionType = GV.parseString(ddl_AD_CommsissionType.SelectedValue.Trim)
            Else
                If txt_AD_Commission.Text = "" Then
                    lblError.Text = "Please Enter Admin Commission."
                    lblError.CssClass = "errorlabels"
                    txt_AD_Commission.Focus()
                    Exit Sub
                Else
                    V_SA_Commission = GV.parseString(txt_AD_Commission.Text.Trim)
                End If
                V_SA_CommissionType = GV.parseString(ddl_AD_CommsissionType.SelectedValue.Trim)
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
            Dim comm As String = ""


            QryStr = "delete from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & VAdminID.Trim & "' and APIName='" & VAPIName.Trim & "' and Category='" & VCategory.Trim & "'; "
            If V_SA_CommissionType.Trim.ToUpper = "Percentage".ToUpper Then
                V_SA_CommissionType = "Percentage"
                comm = "convert(decimal(18,2),((SA_Commission*" & CDec(V_SA_Commission) & ")/100))"
            ElseIf V_SA_CommissionType.Trim.ToUpper = "Amount".ToUpper Then
                V_SA_CommissionType = "Amount"
                comm = "convert(decimal(18,2)," & CDec(V_SA_Commission) & ")"
            Else
                V_SA_CommissionType = "Not Applicable"
                comm = "convert(decimal(18,2)," & CDec(V_SA_Commission) & ")"
            End If

            QryStr = QryStr & "insert into " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA(ServiceType,ServiceCharge,AdminID,APIName,Category,Code,OperatorName,Admin_CommissionType,Admin_Commission,ActiveStatus,UpdatedBy,UpdatedOn) "
            QryStr = QryStr & " select '" & VServiceType & "'," & VServiceCharge & ",'" & VAdminID.Trim & "',APIName,Category,Code,OperatorName,'" & V_SA_CommissionType & "'," & comm & ",'" & VStatus.Trim & "','" & VUpdatedBy.Trim & "',getdate() "
            QryStr = QryStr & " from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA OWSA where OWSA.APIName='" & VAPIName.Trim & "' and OWSA.Category='" & VCategory.Trim & "';"


            If GV.FL.DMLQueries(QryStr) = True Then
                Bind()
                lblError.Text = "Commission Applied Successfully."
                lblError.CssClass = "Successlabels"

            Else
                lblError.Text = "Sorry !! Process Can't be Completed."
                lblError.CssClass = "errorlabels"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Clear()
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            ddlProductService.Enabled = True
            ddlProductService.CssClass = "form-control"
            ddlContainCategory.Enabled = True
            ddlContainCategory.CssClass = "form-control"
            '  ddlContainCategory.SelectedIndex = 0
            txt_AD_Commission.Text = "0"
            lblSessionFlag.Text = 0
            btnSave.Text = "Save"
            lblError.Text = ""
            lblError.CssClass = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            lblUpadate.Text = ""

            'txtOperatorName.Text = ""
            'txtCode.Text = ""
            ddlStatus.SelectedIndex = 0

            ddl_AD_CommsissionType.SelectedIndex = 0
            If ddl_AD_CommsissionType.SelectedIndex = 0 Then
                txt_AD_Commission.Text = "0"
                txt_AD_Commission.Enabled = False
                txt_AD_Commission.CssClass = "form-control"
            End If
            txtServiceCharge.Enabled = False
            txtServiceCharge.CssClass = "form-control"
            txtServiceCharge.Text = "0"

            ddlServiceCharge.Enabled = True
            ddlServiceCharge.CssClass = "form-control"
            ddlServiceCharge.SelectedIndex = 0
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
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Clear()
            ddlProductService.SelectedIndex = 0
            ddlProductService_SelectedIndexChanged(sender, e)

            ddl_AD_CommsissionType.SelectedIndex = 0
            ddl_Admin_SelectedIndexChanged(sender, e)

           



            Bind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImagebtnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToExcel_New(GridView1, Response, "", "OperatorWiseCommission", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ImagebtnWOrd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try

            If GridView1.Rows.Count > 0 Then
                GV.ExportToWord_New(GridView1, Response, "OperatorWiseCommission", "select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA  order by rid desc ", "dyanamic")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imagepdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try
            If GridView1.Rows.Count > 0 Then
                GV.ExportToPdf_New(GridView1, "", Response, "OperatorWiseCommission", "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA  order by rid desc ", "dyanamic")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub ddlProductService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProductService.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            ddlContainCategory.Items.Clear()
            GV.FL.AddInDropDownListDistinct(ddlContainCategory, "upper(Category)", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_APIVSCategory_Master_SA where ProductService='" & GV.parseString(ddlProductService.SelectedValue.Trim) & "'  ")
            If ddlContainCategory.Items.Count > 0 Then
                ddlContainCategory.Items.Insert(0, ":: Select Category ::")
            Else
                ddlContainCategory.Items.Add(":: Select Category ::")
            End If
            If Not lblSessionFlag.Text = 1 Then
                Bind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlContainCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContainCategory.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            If Not lblSessionFlag.Text = 1 Then
                Bind()
            End If
        Catch ex As Exception

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

        End Try
    End Sub


    Public Sub InsertOperator()
        Try
            Dim category() As String = {"Mobile", "dth", "Landline", "Postpaid", "Electricity", "GAS", "Waterbill", "Broadband"}

            For i As Integer = 0 To category.Length - 1
                add_Recharge_Operators(category(i))
            Next
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddl_SA_CommsissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_AD_CommsissionType.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            txt_AD_Commission.Enabled = True
            txt_AD_Commission.CssClass = "form-control"
            If ddl_AD_CommsissionType.SelectedIndex = 0 Then
                txt_AD_Commission.Text = "0"
                txt_AD_Commission.Enabled = False
                txt_AD_Commission.CssClass = "form-control"
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class