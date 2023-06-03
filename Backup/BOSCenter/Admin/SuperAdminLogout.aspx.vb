
Public Class SuperAdminLogout
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim VPAthDistributor As String = ""
            Dim VGroupid As String = ""
            Dim BAckPAth As String = GV.get_ManageAllCookies_SessionVariables("Path", Request, Response)
            Dim BAckIdPAth As String = GV.get_ManageAllCookies_SessionVariables("ID", Request, Response)
            If Not BAckIdPAth.Trim = "" Then
                Dim PAth() As String = BAckPAth.Split(":")
                Dim PathLengthSize As Integer = PAth.Length
                Dim ID() As String = BAckIdPAth.Split(":")
                Dim IDLengthSize As Integer = ID.Length
                If PathLengthSize = 4 Then
                    VPAthDistributor = PAth(3)
                    VGroupid = ID(3)
                    GV.ManageAllCookies = New HttpCookie("ManageAllCookies")
                    GV.ManageAllCookies("Path") = PAth(0) & ":" & PAth(1) & ":" & PAth(2)
                    GV.ManageAllCookies("ID") = ID(0) & ":" & ID(1) & ":" & ID(2)
                    Response.Cookies.Add(GV.ManageAllCookies)
                ElseIf PathLengthSize = 3 Then
                    VPAthDistributor = PAth(2)
                    VGroupid = ID(2)
                    GV.ManageAllCookies = New HttpCookie("ManageAllCookies")
                    GV.ManageAllCookies("Path") = PAth(0) & ":" & PAth(1)
                    GV.ManageAllCookies("ID") = ID(0) & ":" & ID(1)
                    Response.Cookies.Add(GV.ManageAllCookies)

                ElseIf PathLengthSize = 2 Then
                    VPAthDistributor = PAth(1)
                    VGroupid = ID(1)
                    GV.ManageAllCookies = New HttpCookie("ManageAllCookies")
                    GV.ManageAllCookies("Path") = PAth(0)
                    GV.ManageAllCookies("ID") = ID(0)
                    Response.Cookies.Add(GV.ManageAllCookies)
                ElseIf PathLengthSize = 1 Then
                    VPAthDistributor = PAth(0)
                    VGroupid = ID(0)
                    GV.Expire_ManageAllCookies_Session(Request, Response)
                End If

                If VPAthDistributor = "Distributor" Or VPAthDistributor = "Master Distributor" Or VPAthDistributor = "Retailer" Then
                    ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VGroupid & "'   and AgentType='" & VPAthDistributor & "'")
                Else
                    If VPAthDistributor.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                        ds = GV.FL.OpenDs(GV.DefaultDatabase.Trim & ".dbo.CRM_Login_Details where User_ID='" & VGroupid & "' and User_Type='" & VPAthDistributor & "'")
                    Else
                        ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & VGroupid & "'   and User_Type='" & VPAthDistributor & "'")
                    End If

                End If

                createMenuCookie(VGroupid)
                If VPAthDistributor = "Distributor" Or VPAthDistributor = "Master Distributor" Or VPAthDistributor = "Retailer" Then
                    If ds.Tables(0).Rows.Count > 0 Then



                        Dim found As Boolean = False
                        Dim dataAdd As Integer = 0

                        If Request.Cookies.Count > 0 Then

                            For i As Integer = 0 To Request.Cookies.Count - 1
                                If Request.Cookies(i).Name = "EMVSoft" Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found = True Then
                                GV.LoginInfo = New HttpCookie("EMVSoft")

                                GV.LoginInfo("Session_Id") = ""
                                GV.LoginInfo("LoginID") = ""
                                GV.LoginInfo("UserName") = ""
                                GV.LoginInfo("LastLogin") = ""
                                GV.LoginInfo("Group") = ""
                                GV.LoginInfo("ImagePath") = ""
                                GV.LoginInfo("DataBaseName") = ""
                                GV.LoginInfo("CompanyCode") = ""
                                GV.LoginInfo("LoggedInAs") = ""

                                GV.LoginInfo.Expires = Now.AddDays(-12)
                                Response.Cookies.Add(GV.LoginInfo)
                                dataAdd = 0
                            Else
                                dataAdd = 0
                            End If
                        Else
                            dataAdd = 0
                        End If
                        Dim sessionID As String
                        If dataAdd = 0 Then

                            GV.LoginInfo = New HttpCookie("EMVSoft")

                            GV.LoginInfo("Session_Id") = GV.FL_AdminLogin.getAutoNumber("SessionId")
                            sessionID = GV.LoginInfo("Session_Id")
                            GV.LoginInfo("LoginID") = ds.Tables(0).Rows(0).Item("RegistrationId").ToString()
                            GV.LoginInfo("UserName") = ds.Tables(0).Rows(0).Item("FirstName").ToString()
                            GV.LoginInfo("ImagePath") = ds.Tables(0).Rows(0).Item("UploadPhoto").ToString()
                            GV.LoginInfo("Group") = ds.Tables(0).Rows(0).Item("AgentType").ToString()
                            GV.LoginInfo("DataBaseName") = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim
                            GV.LoginInfo("CompanyCode") = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim
                            GV.LoginInfo.Expires = Now.AddHours(9)
                            Response.Cookies.Add(GV.LoginInfo)
                        End If
                        Response.Redirect("~/Admin/SuperAdminHome.aspx")

                    End If
                Else
                    Dim found As Boolean = False
                    Dim dataAdd As Integer = 0
                    If Request.Cookies.Count > 0 Then

                        For i As Integer = 0 To Request.Cookies.Count - 1
                            If Request.Cookies(i).Name = "EMVSoft" Then
                                found = True
                                Exit For
                            End If
                        Next
                        If found = True Then
                            GV.LoginInfo = New HttpCookie("EMVSoft")
                            GV.LoginInfo("Session_Id") = ""
                            GV.LoginInfo("LoginID") = ""
                            GV.LoginInfo("UserName") = ""
                            GV.LoginInfo("Designation") = ""
                            GV.LoginInfo("LastLogin") = ""
                            GV.LoginInfo("Group") = ""
                            GV.LoginInfo("BranchCode") = ""
                            GV.LoginInfo("BranchName") = ""
                            GV.LoginInfo("ImagePath") = ""
                            GV.LoginInfo("CompanyCode") = ""
                            GV.LoginInfo("DataBaseName") = ""
                            GV.Expire_SuperAdmin_Session(Request, Response)
                            GV.LoginInfo.Expires = Now.AddDays(-12)
                            Response.Cookies.Add(GV.LoginInfo)

                            dataAdd = 0
                        Else
                            dataAdd = 0
                        End If
                    Else
                        dataAdd = 0
                    End If
                    Dim sessionID As String = ""
                    Dim CanLogin As String = ""
                    If dataAdd = 0 Then
                        GV.LoginInfo = New HttpCookie("EMVSoft")
                        GV.LoginInfo("Session_Id") = GV.FL_AdminLogin.getAutoNumber("SessionId")
                        sessionID = GV.LoginInfo("Session_Id")
                        GV.LoginInfo("LoginID") = ds.Tables(0).Rows(0).Item("User_ID").ToString()
                        Dim DBNAME As String = ""
                        CanLogin = ds.Tables(0).Rows(0).Item("Canlogin").ToString()
                        GV.LoginInfo("BranchCode") = ""
                        GV.LoginInfo("BranchName") = ""
                        GV.LoginInfo("UserName") = ds.Tables(0).Rows(0).Item("User_Name").ToString()
                        GV.LoginInfo("Group") = ds.Tables(0).Rows(0).Item("User_Type").ToString()
                        GV.LoginInfo("ImagePath") = ds.Tables(0).Rows(0).Item("ImagePath").ToString()
                        GV.LoginInfo("DataBaseName") = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim
                        GV.LoginInfo("CompanyCode") = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                        GV.LoginInfo.Expires = Now.AddHours(9)
                        Response.Cookies.Add(GV.LoginInfo)
                    End If
                    Response.Redirect("~/Admin/SuperAdminHome.aspx")
                End If
                
            Else
                If Not GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) = "" Then
                    Dim dbName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim
                    Dim CompName As String = ""

                    If dbName.Trim.ToUpper = GV.DefaultDatabase.Trim.ToUpper Or dbName.Trim = "" Then
                        CompName = "CMP1085"
                        dbName = GV.DefaultDatabase.Trim
                    Else
                        CompName = dbName
                    End If

                    Dim RedirectURL As String = GV.FL.AddInVar("WebRedirectUrl", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where DatabaseName='" & dbName & "'")

                    GV.Expire_SuperAdmin_Session(Request, Response)

                    If RedirectURL.Trim = "" Then
                        Response.Redirect("~/Default.aspx?admin=" & CompName)
                    Else
                        Response.Redirect(RedirectURL)
                    End If

                Else
                    '' lblMessage.Text = "!!!!!!!   Session Timed Out Please login   !!!!"
                    Dim dbName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim
                    Dim CompName As String = ""

                    If dbName.Trim.ToUpper = GV.DefaultDatabase.Trim.ToUpper Or dbName.Trim = "" Then
                        CompName = "CMP1085"
                        dbName = GV.DefaultDatabase.Trim
                    Else
                        CompName = dbName
                    End If

                    Dim RedirectURL As String = GV.FL.AddInVar("WebRedirectUrl", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where DatabaseName='" & dbName & "'")


                    GV.Expire_SuperAdmin_Session(Request, Response)

                    If RedirectURL.Trim = "" Then
                        Response.Redirect("~/Default.aspx?admin=" & CompName)
                    Else
                        Response.Redirect(RedirectURL)
                    End If


                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub createMenuCookie(Group As String)
        Try
            Dim found As Boolean = False
            Dim dataAdd As Integer = 0
            If Request.Cookies.Count > 0 Then

                For i As Integer = 0 To Request.Cookies.Count - 1
                    If Request.Cookies(i).Name = "SuperAdmin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    GV.SuperAdmin_MenuCookie = New HttpCookie("SuperAdmin_MenuCookie")

                    GV.SuperAdmin_MenuCookie("Selected_MainMenu") = ""
                    GV.SuperAdmin_MenuCookie("Selected_SubMenu") = ""
                    GV.SuperAdmin_MenuCookie.Expires = Now.AddDays(1)
                    Response.Cookies.Add(GV.SuperAdmin_MenuCookie)
                    dataAdd = 0

                Else
                    dataAdd = 0
                End If
            Else
                dataAdd = 0
            End If

            If dataAdd = 0 Then
                GV.SuperAdmin_MenuCookie = New HttpCookie("SuperAdmin_MenuCookie")
                GV.SuperAdmin_MenuCookie("Selected_MainMenu") = ""
                GV.SuperAdmin_MenuCookie("Selected_SubMenu") = ""

                GV.SuperAdmin_MenuCookie.Expires = Now.AddHours(9)
                Response.Cookies.Add(GV.SuperAdmin_MenuCookie)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class