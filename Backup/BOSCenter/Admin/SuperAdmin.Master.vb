
Imports System.Net
Imports System.IO
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Public Class SuperAdmin
    Inherits System.Web.UI.MasterPage

    Dim GV As New GlobalVariable("ADMIN")

    Dim MainMenu() As String = {"Masters", "Plan Management", "Client Management", "Payment Management", "Operator Management", "Reports", "Mail Management", "Sms Management", "Settings"}
    Dim MainMenuIcon() As String = {"fa fa-book fa-lg", "fa fa-pie-chart fa-lg", "fa fa-user-plus fa-lg", "fa fa-credit-card", "fa fa-users fa-lg", "fa fa-hourglass fa-lg", "fa fa-envelope fa-lg", "fa fa-phone-square fa-lg", "fa fa-wrench fa-lg"}
    Dim SubMenuar() As String = {"Masters:Country Master,State Master,District Master,Group Master,News & Event Master,Portfolio Master,Client Testimonial,Company Type Master", "Plan Management:Plan Master,Billing Cycle Master", "Client Management:Client Registration,Search & Edit,Client Approval,Go To Client A\C", "Payment Management:Receive Payment,Search Payment", "Operator Management:Create Operator,Login Time Frame,Logout User", "Reports:Plan Renewal Report,Account Report,Client Password Report,Operator Login Report,Client Remove A/C Report", "Mail Management:Send Mail,Mail Template", "Sms Management:Send Sms,Add Message Category,Sms Template,Search Send Message", "Settings:Super Admin Module Master,Super Admin Menu Creation,Super Admin User Rights,Change Password,Site Map"}
    Dim SubMenuCSS() As String = {"btn btn-primary btn-sm mb3", "btn btn-success btn-sm mb3", "btn btn-danger btn-sm mb3", "btn btn-warning btn-sm mb3", "btn btn-info btn-sm mb3"}
    Dim strMainMenu As String = ""
    Dim strSubMenu As String = ""

    Public Sub GenerateMenu()
        Try
            Dim main_menu, main_submenu, navigationmenu As String

            If Not GV.get_SuperAdmin_MenuCookie("Selected_MainMenu", Request, Response) = "" Then
                main_menu = GV.get_SuperAdmin_MenuCookie("Selected_MainMenu", Request, Response)
            Else
                Main_Menu = ""
            End If
            'GETTING SELECTED SUB MODULE
            If Not GV.get_SuperAdmin_MenuCookie("Selected_SubMenu", Request, Response) = "" Then
                main_submenu = GV.get_SuperAdmin_MenuCookie("Selected_SubMenu", Request, Response)
            Else
                main_submenu = ""
            End If
            'getting Selected navigation module
            If Not GV.get_SuperAdmin_MenuCookie("NavigationMenu", Request, Response) = "" Then
                navigationmenu = GV.parseString(GV.get_SuperAdmin_MenuCookie("NavigationMenu", Request, Response))
            Else
                navigationmenu = ""
            End If


            'checking condition
            If main_menu = "" Then
                Div_Navigation.InnerHtml = "<b>::&nbsp;</b>Home" & " <b>>></b>" & main_submenu
            Else
                If main_submenu = "" Then
                    If navigationmenu = "" Then
                        Div_Navigation.InnerHtml = "<b>::&nbsp;</b>Home" & " <b>>></b> " & main_menu
                    Else
                        Div_Navigation.InnerHtml = "<b>::&nbsp;</b>Home <b>>></b> " & main_menu & " <b>>>" & navigationmenu
                    End If
                Else
                    If navigationmenu = "" Then
                        Div_Navigation.InnerHtml = "<b>::&nbsp;</b>Home" & " <b>>></b> " & main_menu & " <b>>></b> " & main_submenu
                    Else
                        Div_Navigation.InnerHtml = "<b>::&nbsp;</b>Home <b>>></b> " & main_menu & " <b>>></b> " & main_submenu & " <b>>>" & navigationmenu
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Not IsPostBack Then
            '    Dim CurentFOrm As String = Replace(Me.Page.Request.AppRelativeCurrentExecutionFilePath, "~", "..")
            '    Dim CurrentGrpLogin As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
            '    If Not CurentFOrm = "../Admin/SuperAdminHome.aspx" Then
            '        If GV.IsAuthorizedForThisPage(CurentFOrm, CurrentGrpLogin) = False Then
            '            Response.Redirect("SuperAdminLogout.aspx")
            '        End If
            '    End If
            '    GV.update_UserRightDataset(GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper)
            'End If


            If Not IsPostBack = True Then
                'Me.form1.Target = "_blank"
                lbl_CompanyCode.Text = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                lbl_DBName.Text = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response)
                'id="Div_logo_lg" runat="server"

                Dim CompanyName As String = GV.FL.AddInVar("CompanyName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where DatabaseName='" & lbl_DBName.Text.Trim & "'")

                If CompanyName.Trim = "" Then
                    Div_logo_Text_lg.InnerHtml = "<b>BOS CENTER</b>"
                    Div_logo_Text_sm.InnerHtml = "<b>BOS</b>"
                Else

                    Dim Text_SM, Text_LG As String
                    Text_SM = ""
                    Text_LG = ""
                    For i As Integer = 0 To CompanyName.Trim.Length - 1
                        If i < 3 Then
                            Text_SM = Text_SM & CompanyName(i)
                        End If
                        If i < 10 Then
                            Text_LG = Text_LG & CompanyName(i)
                        End If
                    Next
                    Div_logo_Text_sm.InnerHtml = "<b>" & Text_SM.ToUpper & "</b>"
                    Div_logo_Text_lg.InnerHtml = "<b>" & Text_LG.ToUpper & "</b>"
                End If
            End If

            GV.update_UserRightDataset(GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper, lbl_DBName.Text.Trim)

            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
        
            lblgroupType.Text = ""

            Notification_msg.Attributes("style") = "display:none"
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then

                'Distributor,Retailer,Sub Distributor
                Notification_msg.Attributes("style") = ""

                lblCredit.Visible = True
                lblCreditBalance.Visible = True
                lblCredit.Text = "Aval. Credit :"
                lblWallet.Text = "Wallet Bal :"
                lblWallet.Visible = True
                lblWalletBalance.Visible = True
                Calculation()
                If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    Dim Agencyname As String = GV.FL.AddInVar("AgencyName", "" & lbl_DBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ")
                    lblgroupType.Text = Agencyname & " ( " & GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper & " )"
                    DIv_GroupType.Visible = True
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    Dim Agencyname As String = GV.FL.AddInVar("AgencyName", "" & lbl_DBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ")
                    lblgroupType.Text = Agencyname & " ( " & GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper & " )"
                    DIv_GroupType.Visible = True
                ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    Dim Agencyname As String = GV.FL.AddInVar("AgencyName", "" & lbl_DBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ")
                    lblgroupType.Text = Agencyname & " ( " & GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper & " )"
                    DIv_GroupType.Visible = True
                Else
                    Dim Agencyname As String = GV.FL.AddInVar("AgencyName", "" & lbl_DBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' ")
                    lblgroupType.Text = Agencyname & " ( " & GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper & " )"
                End If
                
            ElseIf GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Admin".Trim.ToUpper Then
                Notification_msg.Attributes("style") = ""
                Dim CompanyName As String = GV.FL.AddInVar("CompanyName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where DatabaseName='" & lbl_DBName.Text.Trim & "'")
                lblgroupType.Text = CompanyName & " ( " & GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper & " )"
                lblCredit.Visible = True
                lblCreditBalance.Visible = True
                lblCredit.Text = "API Bal :"
                lblWallet.Text = "Wallet Bal :"
                lblWallet.Visible = True
                lblWalletBalance.Visible = True
                Calculation()


            Else
                'div1.Visible = True
                'Calculation()

                '/// For Logout
                Dim LocalDS As New DataSet
                LocalDS = GV.FL.OpenDs(lbl_DBName.Text.Trim & ".dbo.CRM_Login_Details where User_ID='" & LoginID & "' and AccountStatus='Active' and RecordStatus='Active' ")
                If Not LocalDS Is Nothing Then
                    If LocalDS.Tables.Count > 0 Then
                        If Not LocalDS.Tables(0).Rows.Count > 0 Then
                            Response.Redirect("SuperAdminLogout.aspx")
                        End If
                    Else
                        Response.Redirect("SuperAdminLogout.aspx")
                    End If
                Else
                    Response.Redirect("SuperAdminLogout.aspx")
                End If

                If GV.Verify_LoginTimeOut(LoginID, "", "Employee") = False Then
                    Response.Redirect("SuperAdminLogout.aspx")
                End If

                DIv_GroupType.Visible = False

                Notification_msg.Attributes("style") = "display:none"

                lblCredit.Visible = False
                lblCreditBalance.Visible = False
                lblWallet.Visible = False
                lblWalletBalance.Visible = False

            End If

            If GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) = "" Then
                Response.Redirect("~/Admin/SuperAdminLogout.aspx")
            End If

            Dim theme As String = GV.FL.AddInVar("isnull(ChangeTheme,'')", lbl_DBName.Text.Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            If theme = "" Then
                lnkred.Attributes("href") = "~/css/admin_Blue.css"
            Else
                lnkred.Attributes("href") = theme
            End If



            Dim Main_Menu As String = ""
            Dim Main_SubMenu As String = ""
            Dim navigationMenu As String = ""
            'SETTING MASTER IF MODULE IS BLANK
            'GETTING SELECTED MODULE
            If Not GV.get_SuperAdmin_MenuCookie("Selected_MainMenu", Request, Response) = "" Then
                Main_Menu = GV.get_SuperAdmin_MenuCookie("Selected_MainMenu", Request, Response)
            Else
                Main_Menu = ""
            End If
            'GETTING SELECTED SUB MODULE
            If Not GV.get_SuperAdmin_MenuCookie("Selected_SubMenu", Request, Response) = "" Then
                Main_SubMenu = "and refSubModule='" & GV.get_SuperAdmin_MenuCookie("Selected_SubMenu", Request, Response) & "' "
            Else
                Main_SubMenu = ""
            End If
            'getting Selected navigation module
            If Not GV.get_SuperAdmin_MenuCookie("Selected_SubMenu", Request, Response) = "" Then
                navigationMenu = "and NavigationMenu='" & GV.get_SuperAdmin_MenuCookie("NavigationMenu", Request, Response) & "' "
            Else
                navigationMenu = ""
            End If
            Dim UserGroup As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            Dim dbName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim

            Dim CompName As String = ""
            If dbName.Trim.ToUpper = "BosCenter_DB".Trim.ToUpper Or dbName.Trim = "" Then
                CompName = "CMP1045"
                dbName = "BosCenter_DB"
            Else
                CompName = dbName
            End If


            If Not IsPostBack = True Then
                lblCurrentIP.Text = GV.GetIPAddress()
                If GV.is_SuperAdmin_validSession(Request, Response) = False Then


              

                    Dim RedirectURL As String = GV.FL.AddInVar("WebRedirectUrl", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where DatabaseName='" & dbName & "'")

                    If RedirectURL.Trim = "" Then
                        Response.Redirect("~/Default.aspx?admin=" & CompName)
                    Else
                        Response.Redirect(RedirectURL)
                    End If

                End If

                If GV.Verify_AccountStatus(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim, GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim) = False Then
                    Response.Redirect("~/Default.aspx?admin=" & CompName)
                End If

                If GV.get_SuperAdmin_SessionVariables("LastLogin", Request, Response) = "" Then
                    lblLastlogin.Text = Now.ToString("dd/MM/yyyy h:mm:ss tt")
                Else
                    lblLastlogin.Text = GV.get_SuperAdmin_SessionVariables("LastLogin", Request, Response).ToString()
                End If

                lblProfileId.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                lblProfileName.Text = GV.get_SuperAdmin_SessionVariables("UserName", Request, Response)
                lblProfileID2.Text = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                'ProfileImgright.Attributes("src") = GV.get_SuperAdmin_SessionVariables("ImagePath", Request, Response)
                If GV.get_SuperAdmin_SessionVariables("ImagePath", Request, Response) = "" Then
                    ProfileImg1.Attributes("src") = "../images/ProfileImage.png"
                    ProfileImage.Attributes("src") = "../images/ProfileImage.png"
                    ProfileImageheader.Attributes("src") = "../images/ProfileImage.png"
                Else
                    ProfileImg1.Attributes("src") = GV.get_SuperAdmin_SessionVariables("ImagePath", Request, Response)
                    ProfileImage.Attributes("src") = GV.get_SuperAdmin_SessionVariables("ImagePath", Request, Response)
                    ProfileImageheader.Attributes("src") = GV.get_SuperAdmin_SessionVariables("ImagePath", Request, Response)
                End If



                Dim iconcounter As Integer = 0
                Dim ds As New DataSet

                '////////////    CHECKING USER GROUP      //////////////



                'ADMIN & EMPLOYEE 3 LEVEL
                'GETTING VALUE
                Dim st1 As String = ""
                Dim BAckPAth As String = GV.get_ManageAllCookies_SessionVariables("Path", Request, Response)
                If BAckPAth = "" Then
                    If UserGroup.Trim.ToUpper = "Admin".Trim.ToUpper Or UserGroup.Trim.ToUpper = "Super Admin".Trim.ToUpper Then

                        If UserGroup.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                            If lblProfileId.Text.Trim.ToUpper = "SR1" Then
                                st1 = "select distinct RefModule,RefSubModule,FormName,OrderNo,RefSubModule_Order from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and FrmSelected=1  and not RefModule in ('Go To Retailer Account','Go To Sub Distributor Account') order by OrderNo,RefSubModule_Order"
                            Else
                                st1 = "select distinct RefModule,RefSubModule,FormName,OrderNo,RefSubModule_Order from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and FrmSelected=1  and not RefModule in ('Go To Retailer Account','Go To Sub Distributor Account','Settings')  order by OrderNo,RefSubModule_Order"
                            End If
                        Else
                            st1 = "select distinct RefModule,RefSubModule,FormName,OrderNo,RefSubModule_Order from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and FrmSelected=1  and not RefModule in ('Go To Retailer Account','Go To Sub Distributor Account') order by OrderNo,RefSubModule_Order"
                        End If

                    Else
                        st1 = "select distinct RefModule,RefSubModule,FormName,OrderNo,RefSubModule_Order from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and FrmSelected=1  and not RefModule in ('Go To Retailer Account','Go To Sub Distributor Account','Go To Distributor Account') order by OrderNo,RefSubModule_Order"
                    End If
                Else
                    st1 = "select distinct RefModule,RefSubModule,FormName,OrderNo,RefSubModule_Order from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and FrmSelected=1 order by OrderNo,RefSubModule_Order"
                End If



                ds = GV.FL.OpenDsWithSelectQuery(st1)
                'CREATING TABLE FORMAT
                Dim dt As New DataTable
                dt.Columns.Add("MainMenu")
                dt.Columns.Add("MainMenuIcon")
                dt.Columns.Add("RefSubModule")
                'INSERTING MODULE AND SUBMODULE
                Dim mainarrlist As New ArrayList
                Dim currowindex As Integer = 0
                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then

                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                                If Not mainarrlist.Contains(ds.Tables(0).Rows(i).Item("RefModule")) Then
                                    dt.Rows.Add()
                                    dt.AcceptChanges()

                                    mainarrlist.Add(ds.Tables(0).Rows(i).Item("RefModule"))
                                    dt.Rows(currowindex)(0) = ds.Tables(0).Rows(i).Item("RefModule")
                                    dt.Rows(currowindex)(2) = ds.Tables(0).Rows(i).Item("RefSubModule")
                                    If iconcounter < MainMenuIcon.Length Then
                                        dt.Rows(currowindex)(1) = MainMenuIcon(iconcounter)
                                        iconcounter += 1
                                    Else
                                        iconcounter = 0
                                        dt.Rows(currowindex)(1) = MainMenuIcon(iconcounter)
                                    End If
                                    currowindex += 1
                                End If

                            Next

                        End If
                    End If
                End If
                'BOUND MODULE AND SUB MODULE
                ListView1.DataSource = dt
                ListView1.DataBind()

              

                'GETTING MENU
                Dim st As String = "select *  from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and FrmSelected=1 and RefModule='" & Main_Menu & "' " & Main_SubMenu & "  order by NavigationModule_Order"
                ds = GV.FL.OpenDsWithSelectQuery(st)
                'CREATE TABLE FORMAT(MENU)
                Dim dtsub As New DataTable
                dtsub.Columns.Add("NavigationModule")
                dtsub.Columns.Add("FormName")
                dtsub.Columns.Add("NavigationModuleCSS")

                'SETTING MENU
                Dim ctrcss As Integer = 0
                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then

                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                dtsub.Rows.Add()
                                dtsub.AcceptChanges()
                                dtsub.Rows(i)(0) = ds.Tables(0).Rows(i).Item("NavigationModule")
                                dtsub.Rows(i)(1) = ds.Tables(0).Rows(i).Item("FormName")
                                dtsub.Rows(i)(2) = SubMenuCSS(ctrcss)
                                If ctrcss < 4 Then
                                    ctrcss += 1
                                Else
                                    ctrcss = 0
                                End If
                            Next

                        End If
                    End If
                End If
                ListView2.DataSource = dtsub
                ListView2.DataBind()
                If GV.FL.RecCount(" " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster  where User_Group='" & UserGroup & "' and RefModule='" & Main_Menu & "' and RefSubModule='' and CreateLink='YES' and FrmSelected=1") > 0 Then
                    ListView2.DataSource = Nothing
                    ListView2.DataBind()
                End If
               

            End If
            If GV.FL.RecCount("" & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster  where User_Group='" & UserGroup & "' and RefModule='" & Main_Menu & "' " & Main_SubMenu & " and CreateLink='YES' and FrmSelected=1") > 0 Then
                ListView2.DataSource = Nothing
                ListView2.DataBind()

            End If
            GenerateMenu()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub MainMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lnk As LinkButton = DirectCast(sender, LinkButton)
            Dim pagename As String = ""
            Dim arr() As String
            If lblcounter.Text = "1" Then
                arr = lblrefmodule.Text.Split(",")
                pagename = arr(0)
                GV.Set_SuperAdmin_MenuCookie(pagename, "", arr(1), Request, Response)
                lblcounter.Text = "0"
            Else
                pagename = lnk.CommandArgument
                'Session("selectedmainmenu") = pagename
                GV.Set_SuperAdmin_MenuCookie(pagename, "", "", Request, Response)
            End If

            Dim ds As New DataSet
            Dim UserGroup As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            ListView2.DataSource = Nothing
            ListView2.DataBind()
            If GV.FL.RecCount("" & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster  where User_Group='" & UserGroup & "' and RefModule='" & pagename & "' and CreateLink='YES'") > 0 Then
                ds = GV.FL.OpenDsWithSelectQuery("select *  from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and RefModule='" & pagename & "' and CreateLink='YES' order by RefModule")
                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then

                            Dim aa As String = ds.Tables(0).Rows(0).Item("FormName")
                            Response.Redirect(aa)
                            Exit Sub
                        End If
                    End If

                End If
            Else

                ds = GV.FL.OpenDsWithSelectQuery("select *  from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and RefModule='" & pagename & "' and CreateLink='' order by RefSubModule_Order")

                Dim dtsub As New DataTable
                dtsub.Columns.Add("NavigationModule")
                dtsub.Columns.Add("FormName")
                dtsub.Columns.Add("NavigationModuleCSS")

                Dim ctrcss As Integer = 0

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then

                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                dtsub.Rows.Add()
                                dtsub.AcceptChanges()
                                dtsub.Rows(i)(0) = ds.Tables(0).Rows(i).Item("NavigationModule")
                                If dtsub.Rows(i)(0) = "" Then
                                    Dim aa As String = ds.Tables(0).Rows(i).Item("FormName")
                                    Response.Redirect(aa)
                                    Exit Sub
                                End If
                                dtsub.Rows(i)(1) = ds.Tables(0).Rows(i).Item("FormName")
                                dtsub.Rows(i)(2) = SubMenuCSS(ctrcss)
                                If ctrcss < 4 Then
                                    ctrcss += 1
                                Else
                                    ctrcss = 0
                                End If
                                If dtsub.Rows(i)(0) = "" Then
                                    Response.Redirect(dtsub.Rows(i)(1))
                                End If
                            Next

                        End If
                    End If
                End If

                ListView2.DataSource = dtsub
                ListView2.DataBind()
                SubMenuPanel.Update()

                'Div_Navigation.InnerHtml = "<b>::&nbsp;</b>" & pagename
                lblNavigations.Text = "<b>::&nbsp;</b>" & pagename
                If Not lblformname.Text = "" Then
                    Response.Redirect(lblformname.Text)
                    lblformname.Text = ""
                Else
                    Response.Redirect("SuperAdminHome.aspx")
                End If


            End If


        Catch ex As Exception
        End Try
    End Sub

    Public Sub SubMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim Selected_SearchingKeyWord As String = ""
            Dim lnk As LinkButton = DirectCast(sender, LinkButton)
            Dim refmodule As String = ""
            Dim refsubmodule As String = ""
            Dim refNavigation As String = ""
            If lblcounter.Text = "1" Then
                Dim arr() As String = lblrefmodule.Text.Split(",")
                refmodule = arr(0)
                refsubmodule = arr(1)
                If arr.Length > 2 Then
                    refNavigation = arr(2)
                End If
                lblcounter.Text = "0"
            Else

                Dim arr() As String = lnk.CommandArgument.Split(",")
                refmodule = arr(0)
                refsubmodule = arr(1)
                If arr.Length > 2 Then
                    refNavigation = arr(2)
                End If


            End If
           
            GV.Set_SuperAdmin_MenuCookie(refmodule, refsubmodule, refNavigation, Request, Response)
           
            Div_Navigation.InnerHtml = "<b>::&nbsp;</b>" & refmodule & " <b>>></b> " & refsubmodule
            Dim ds As New DataSet
            Dim UserGroup As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            Dim st As String = "select *  from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and RefModule='" & refmodule & "' and RefSubModule='" & refsubmodule & "' and FrmSelected=1  order by RefSubModule_Order"
            ds = GV.FL.OpenDsWithSelectQuery(st)
            Dim countQry As String = "" & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster  where User_Group='" & UserGroup & "' and RefModule='" & refmodule & "' and refSubModule='" & refsubmodule & "' and CreateLink='YES' and FrmSelected=1"
            If GV.FL.RecCount(countQry) > 0 Then

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            ListView2.DataSource = Nothing
                            ListView2.DataBind()
                            Dim aa As String = ds.Tables(0).Rows(0).Item("FormName")

                            'Response.Write("<script> window.open( '" + aa + "','_blank' ); </script>")
                            'Response.End()

                            'lnkbtnpinterest.Attributes.Add("href", lblH_Printerest.Text.Trim)
                            'lnkbtnpinterest.Attributes.Add("target", "_blank")

                            'Dim url As String = "http://www.dotnetcurry.com"
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

                            Response.Redirect(aa)
                            Exit Sub
                        End If
                    End If

                End If
            Else
                Dim dtsub As New DataTable
                dtsub.Columns.Add("NavigationModule")
                dtsub.Columns.Add("FormName")
                dtsub.Columns.Add("NavigationModuleCSS")

                Dim ctrcss As Integer = 0

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then

                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                dtsub.Rows.Add()
                                dtsub.AcceptChanges()
                                dtsub.Rows(i)(0) = ds.Tables(0).Rows(i).Item("NavigationModule")
                                dtsub.Rows(i)(1) = ds.Tables(0).Rows(i).Item("FormName")
                                dtsub.Rows(i)(2) = SubMenuCSS(ctrcss)
                                If ctrcss < 4 Then
                                    ctrcss += 1
                                Else
                                    ctrcss = 0
                                End If
                            Next


                        End If
                    End If
                End If

                ListView2.DataSource = dtsub
                ListView2.DataBind()
                '  Div_Navigation.InnerHtml = "<b>::&nbsp;</b>" & refmodule & " <b>>></b> " & refsubmodule
                SubMenuPanel.Update()
                If Not lblformname.Text = "" Then
                    Response.Redirect(lblformname.Text)
                    lblformname.Text = ""
                End If
            End If

            Response.Redirect("SuperAdminHome.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub NavigationMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
        
            Dim lnk As Button = DirectCast(sender, Button)
            Dim Formname As String = lnk.CommandArgument
            GV.Set_SuperAdmin_MenuCookie(GV.get_SuperAdmin_MenuCookie("Selected_MainMenu", Request, Response), GV.get_SuperAdmin_MenuCookie("Selected_SubMenu", Request, Response), lnk.Text, Request, Response)
            Response.Redirect(Formname)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ListView1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListView1.ItemDataBound
        Try

            Dim lnkFull As LinkButton = TryCast(e.Item.FindControl("LinkButton1"), LinkButton)
            'ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(lnkFull)
            ScriptManager.GetCurrent(Me.Page).RegisterAsyncPostBackControl(lnkFull)

            Dim lblRefSubModule As Label = TryCast(e.Item.FindControl("lblRefSubModule"), Label)
            If lblRefSubModule.Text = "" Then
                e.Item.FindControl("iSubMenu").Visible = False
                e.Item.FindControl("UlSubMenu").Visible = False
            Else
                e.Item.FindControl("iSubMenu").Visible = True
                e.Item.FindControl("UlSubMenu").Visible = True
                Dim lstview As ListView = TryCast(e.Item.FindControl("ListView3"), ListView)


                Dim UserGroup As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                Dim iconcounter As Integer = 0
                Dim ds As New DataSet
                Dim st As String = ""
                Dim BAckPAth As String = GV.get_ManageAllCookies_SessionVariables("Path", Request, Response)
                If BAckPAth = "" Then
                    If UserGroup.Trim.ToUpper = "Admin".Trim.ToUpper Or UserGroup.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        st = "select distinct RefModule,refSubModule,RefSubModule_Order  from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and RefModule='" & lnkFull.CommandArgument & "' and FrmSelected=1 and not RefModule in ('Go To Retailer Account','Go To Sub Distributor Account') order by RefSubModule_Order "
                    Else
                        st = "select distinct RefModule,refSubModule,RefSubModule_Order  from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and RefModule='" & lnkFull.CommandArgument & "' and FrmSelected=1 and not RefModule in ('Go To Retailer Account','Go To Sub Distributor Account','Go To Distributor Account') order by RefSubModule_Order "
                    End If

                Else
                    st = "select distinct RefModule,refSubModule,RefSubModule_Order  from  " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where User_Group='" & UserGroup & "' and RefModule='" & lnkFull.CommandArgument & "' and FrmSelected=1 order by RefSubModule_Order "
                End If
                
                ds = GV.FL.OpenDsWithSelectQuery(st)

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then

                            lstview.DataSource = ds
                            lstview.DataBind()

                        End If
                    End If
                End If

            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ListView2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListView2.ItemDataBound
        Try
            Dim lnkFull As Button = TryCast(e.Item.FindControl("Button1"), Button)
            ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(lnkFull)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProfile.Click
        Try
            If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Master Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
                Response.Redirect("BOS_AgenentProfile.aspx")
            Else
                Response.Redirect("ProfileInfo.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub nav()

    End Sub

    Dim theme As String
    'Protected Sub btnChangeTheme1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangeTheme1.Click
    '    Try

    '        lnkred.Attributes("href") = "~/css/admin_new.css"
    '        theme = "~/css/admin_new.css"
    '        GV.FL.DMLQueries("update CRM_Login_Details set ChangeTheme='" & theme & "' where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnChangeTheme2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangeTheme2.Click
    '    Try
    '        lnkred.Attributes("href") = "~/css/admin_Purple.css"
    '        theme = "~/css/admin_Purple.css"
    '        GV.FL.DMLQueries("update CRM_Login_Details set ChangeTheme='" & theme & "' where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnChangeTheme3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangeTheme3.Click
    '    Try
    '        lnkred.Attributes("href") = "~/css/admin_Green.css"
    '        theme = "~/css/admin_Green.css"
    '        GV.FL.DMLQueries("update CRM_Login_Details set ChangeTheme='" & theme & "' where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnChangeTheme4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangeTheme4.Click
    '    Try
    '        lnkred.Attributes("href") = "~/css/admin_Blue.css"
    '        theme = "~/css/admin_Blue.css"
    '        GV.FL.DMLQueries("update CRM_Login_Details set ChangeTheme='" & theme & "' where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnChangeTheme5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangeTheme5.Click
    '    Try
    '        lnkred.Attributes("href") = "~/css/admin_Yellow.css"
    '        theme = "~/css/admin_Yellow.css"
    '        GV.FL.DMLQueries("update CRM_Login_Details set ChangeTheme='" & theme & "' where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Dim formname As String = ""
    Dim VRefModule, VRefSubModule, Vformname, VsearchingKeyword, VrefNavigationModule As String

    Private Sub lnksearchForm_Click(sender As Object, e As System.EventArgs) Handles lnksearchForm.Click
        Try


            formname = txtsearchform.Text
            Dim query As String
            Dim groupType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
            If groupType = "Admin".Trim.ToUpper Then
                query = "select *  from " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where Searching_Keyword = '" & formname & "'  and User_Group = '" & GV.get_SuperAdmin_SessionVariables("Group", Request, Response) & "' "
            Else
                query = "select *  from " & lbl_DBName.Text.Trim & ".dbo.CRM_UserRightsMaster where Searching_Keyword = '" & formname & "'  and User_Group = '" & GV.get_SuperAdmin_SessionVariables("Group", Request, Response) & "' "
            End If

            'Dim query As String = "select  * from NidhiSoftware_Admin_UserRightsMaster where  NavigationModule = '" & formname & "' "
            Dim ds1 As New DataSet
            ds1 = GV.FL.OpenDsWithSelectQuery(query)

            If Not ds1 Is Nothing Then
                If ds1.Tables.Count > 0 Then
                    If ds1.Tables(0).Rows.Count > 0 Then


                        If Not IsDBNull(ds1.Tables(0).Rows(0).Item("RefModule")) Then
                            If Not ds1.Tables(0).Rows(0).Item("RefModule").ToString() = "" Then
                                VRefModule = GV.parseString(ds1.Tables(0).Rows(0).Item("RefModule").ToString())
                            Else
                                VRefModule = ""
                            End If
                        Else
                            VRefModule = ""
                        End If



                        If Not IsDBNull(ds1.Tables(0).Rows(0).Item("RefSubModule")) Then
                            If Not ds1.Tables(0).Rows(0).Item("RefSubModule").ToString() = "" Then
                                VRefSubModule = GV.parseString(ds1.Tables(0).Rows(0).Item("RefSubModule").ToString())
                            Else
                                VRefSubModule = ""
                            End If
                        Else
                            VRefSubModule = ""
                        End If
                        If Not IsDBNull(ds1.Tables(0).Rows(0).Item("NavigationModule")) Then
                            If Not ds1.Tables(0).Rows(0).Item("NavigationModule").ToString() = "" Then
                                VrefNavigationModule = GV.parseString(ds1.Tables(0).Rows(0).Item("NavigationModule").ToString())
                            Else
                                VrefNavigationModule = ""
                            End If
                        Else
                            VrefNavigationModule = ""
                        End If
                        If Not IsDBNull(ds1.Tables(0).Rows(0).Item("formname")) Then
                            If Not ds1.Tables(0).Rows(0).Item("formname").ToString() = "" Then
                                Vformname = GV.parseString(ds1.Tables(0).Rows(0).Item("formname").ToString())
                            Else
                                Vformname = ""
                            End If
                        Else
                            Vformname = ""
                        End If

                        If Not IsDBNull(ds1.Tables(0).Rows(0).Item("Searching_Keyword")) Then
                            If Not ds1.Tables(0).Rows(0).Item("Searching_Keyword").ToString() = "" Then
                                VsearchingKeyword = GV.parseString(ds1.Tables(0).Rows(0).Item("Searching_Keyword").ToString())
                            Else
                                VsearchingKeyword = ""
                            End If
                        Else
                            VsearchingKeyword = ""
                        End If

                        lblformname.Text = Vformname
                        lblrefmodule.Text = ""
                        If Not VRefModule = "" Then
                            lblrefmodule.Text = VRefModule
                        End If
                        If Not VRefSubModule = "" Then
                            lblrefmodule.Text = lblrefmodule.Text & "," & VRefSubModule
                        End If
                        If Not VrefNavigationModule = "" Then
                            lblrefmodule.Text = lblrefmodule.Text & "," & VrefNavigationModule
                        End If

                        lblcounter.Text = "1"
                        If Not VRefSubModule = "" Then
                            SubMenu_Click(sender, e)
                        Else
                            MainMenu_Click(sender, e)
                        End If

                    Else
                        txtsearchform.Text = ""
                    End If
                Else
                    txtsearchform.Text = ""
                End If
            Else
                txtsearchform.Text = ""
            End If



        Catch ex As Exception

        End Try
    End Sub

    Public Sub Calculation()
        Try
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim FromAmount As String = GV.FL.AddInVar("Sum(isnull(TransferAmt,0))", "" & lbl_DBName.Text.Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' and  not (TransferFrom='Super Admin' and TransferTo='API Partner') and not (TransferFrom='API Partner' and TransferTo='Super Admin') ")
            Dim ToAmount As String = GV.FL.AddInVar("Sum(isnull(TransferAmt,0))", "" & lbl_DBName.Text.Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' and   not (TransferFrom='Super Admin' and TransferTo='API Partner') and not (TransferFrom='API Partner' and TransferTo='Super Admin')")

            If FromAmount.Trim = "" Then
                FromAmount = "0"
            End If

            If ToAmount.Trim = "" Then
                ToAmount = "0"
            End If

            Dim BAlAMount As Decimal = CDec(ToAmount) - CDec(FromAmount)
            lblWalletBalance.Text = Math.Round(BAlAMount)
            lblMainBal.Text = Math.Round(BAlAMount)

            If group = "Distributor" Or group = "Master Distributor" Or group = "Retailer" Or group = "Customer" Then

                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & lbl_DBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                If CDec(CreditBAl) <= 0 Then
                    lblCreditBalance.Visible = False
                    lblCredit.Visible = False

                Else
                    lblCreditBal.Text = CreditBAl
                    lblCreditBalance.Visible = True
                    lblCredit.Visible = True
                End If



                Dim HoldAmt As String = GV.FL.AddInVar("HoldAmt", "" & lbl_DBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If HoldAmt = "" Then
                    HoldAmt = "0"
                End If
                lblHold_Amt.Text = CLng(HoldAmt)


            ElseIf group.Trim.ToUpper = "Admin".Trim.ToUpper Then

                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & lbl_DBName.Text.Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                lblCreditBal.Text = CreditBAl
                lblWalletBalance.Text = CDec(lblWalletBalance.Text) + CreditBAl
                lblCreditBalance.Text = GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
                If lblMainBal.Text.Contains("-") Then
                    lblAvailCreditBAl.Text = CDec(lblCreditBal.Text.Trim) + CDec(lblMainBal.Text.Trim)
                Else
                    lblAvailCreditBAl.Text = lblCreditBal.Text.Trim
                End If

                Dim HoldAmt As String = GV.FL.AddInVar("HoldAmt", "" & lbl_DBName.Text.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'")
                If HoldAmt = "" Then
                    HoldAmt = "0"
                End If
                lblHold_Amt.Text = CLng(HoldAmt)

                lblAccualBal.Text = CDec(lblCreditBal.Text.Trim) + CDec(lblMainBal.Text.Trim) - CDec(lblHold_Amt.Text)


                Exit Sub

            Else
                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & lbl_DBName.Text.Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                lblCreditBal.Text = CreditBAl
            End If

            If lblMainBal.Text.Contains("-") Then
                lblAvailCreditBAl.Text = CDec(lblCreditBal.Text.Trim) + CDec(lblMainBal.Text.Trim)
            Else
                lblAvailCreditBAl.Text = lblCreditBal.Text.Trim
            End If

            If lblHold_Amt.Text.Trim = "" Then lblHold_Amt.Text = "0"


            lblAccualBal.Text = CDec(lblCreditBal.Text.Trim) + CDec(lblMainBal.Text.Trim) - CDec(lblHold_Amt.Text)
            lblCreditBalance.Text = lblAvailCreditBAl.Text
        Catch ex As Exception

        End Try
    End Sub




End Class