Public Class GoToAccount
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                If group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    lblHeading.Text = "Go To Distributor Account"
                ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    lblHeading.Text = "Go To Retailer Account"
                ElseIf group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    lblHeading.Text = "Go To Admin Account"
                Else
                    lblHeading.Text = "Go To Master Distributor Account"
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Public Sub clear()
        Try
            ds = New DataSet
            txtSearchString.Text = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try

            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""

            If Not ddlSelectCriteria.SelectedIndex = 0 Then
                If txtSearchString.Text.Trim = "" Then
                    lblError1.Text = "Please Enter the Value"
                    lblError1.CssClass = "errorlabels"
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                    Exit Sub
                End If
            End If
            Bind()


        Catch ex As Exception

        End Try
    End Sub


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim Loginid As String = ""
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            If group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                group = "Distributor"
            ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                group = "Retailer"
            ElseIf group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then

            Else
                Loginid = ""
                group = "Master Distributor"
            End If

            If group.Trim.ToUpper = "Super Admin".ToUpper Then
                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    ddlSelectCriteria.Text = ""
                    Querystring = "select '0' as 'SrNo',CompanyName as 'Company Name',CompanyCode as 'Company Code',User_Type as 'Role',CinNo as 'Cin No',GSTNo as 'GST No',Status as 'Account Status',DatabaseName,RecordStatus as 'Status','0' as 'API Balance' from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration  order by  rid Desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Name" Then
                    Querystring = "select '0' as 'SrNo',CompanyName as 'Company Name',CompanyCode as 'Company Code',User_Type as 'Role',CinNo as 'Cin No',GSTNo as 'GST No',Status as 'Account Status',DatabaseName,RecordStatus as 'Status','0' as 'API Balance' from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration Where CompanyName  like '" & GV.parseString(txtSearchString.Text.Trim) & "%' order by  rid Desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Login ID" Then
                    Querystring = "select '0' as 'SrNo',CompanyName as 'Company Name',CompanyCode as 'Company Code',User_Type as 'Role',CinNo as 'Cin No',GSTNo as 'GST No',Status as 'Account Status',DatabaseName,RecordStatus as 'Status','0' as 'API Balance' from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration Where  CompanyCode='" & GV.parseString(txtSearchString.Text.Trim) & "'  order by  rid Desc"
                End If
            Else
                If ddlSelectCriteria.SelectedValue = "All Records" Then
                    ddlSelectCriteria.Text = ""
                    Querystring = "select '0' as 'SrNo',(FirstName+' '+LastName) as Name,RegistrationId as 'Registration ID',AgentType as 'Agent Role',ActiveStatus as 'Status',(CONVERT(VARCHAR(11),RegistrationDate,106)) as 'Registration Date',AgencyName as 'Agency Name',MobileNo as 'Mobile No','0' as 'Wallet Bal' from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & group & "' " & Loginid & "  order by  rid Desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Name" Then
                    Querystring = "select '0' as 'SrNo',(FirstName+' '+LastName) as Name,RegistrationId as 'Registration ID',AgentType as 'Agent Role',ActiveStatus as 'Status',(CONVERT(VARCHAR(11),RegistrationDate,106)) as 'Registration Date',AgencyName as 'Agency Name',MobileNo as 'Mobile No','0' as 'Wallet Bal' from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where AgentType='" & group & "' " & Loginid & "  and FirstName  like '" & GV.parseString(txtSearchString.Text.Trim) & "%' order by  rid Desc"
                ElseIf ddlSelectCriteria.SelectedValue = "Login ID" Then
                    Querystring = "select '0' as 'SrNo',(FirstName+' '+LastName) as Name,RegistrationId as 'Registration ID',AgentType as 'Agent Role',ActiveStatus as 'Status',(CONVERT(VARCHAR(11),RegistrationDate,106)) as 'Registration Date',AgencyName as 'Agency Name',MobileNo as 'Mobile No','0' as 'Wallet Bal' from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] Where AgentType='" & group & "' " & Loginid & "  and RegistrationId='" & GV.parseString(txtSearchString.Text.Trim) & "'  order by  rid Desc"
                End If
            End If
           
            lblExportQry.Text = Querystring
            If Not Querystring = "" Then
                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()
                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 1)


                    If Not group.Trim.ToUpper = "Super Admin".ToUpper Then

                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            GridView1.Rows(i).Cells(9).Text = GV.AgentBalance(GV.parseString(GridView1.Rows(i).Cells(3).Text.Trim), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
                            'GridView1.Rows(i).Cells(4).Text = GV.parseString(GV.DecryptString(GV.key, GridView1.Rows(i).Cells(4).Text.Trim))

                        Next
                    Else
                        For i As Integer = 0 To GridView1.Rows.Count - 1
                            GridView1.Rows(i).Cells(10).Text = GV.returnAPIBalance(GV.parseString(GridView1.Rows(i).Cells(8).Text.Trim))
                            ' GridView1.Rows(i).Cells(3).Text = GV.parseString(GV.DecryptString(GV.key, GridView1.Rows(i).Cells(3).Text.Trim))

                        Next
                    End If

                Else
                    clear()
                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception
            lblNoRecords.Text = ex.Message
        End Try
    End Sub

    
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim loginID, Password As String
            Dim groupType As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim x As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim Group As String = ""
            If groupType.Trim.ToUpper = "Super Admin".ToUpper Then

                loginID = "Admin"
                Password = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
                Group = "Admin"
                createMenuCookie(Group)
                ds = GV.FL.OpenDs(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) & "' ")
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
                            GV.LoginInfo("LoggedInAs") = ""
                            GV.LoginInfo("CompanyCode") = ""
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
                        GV.LoginInfo("LoginID") = loginID
                        GV.LoginInfo("UserName") = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
                        GV.LoginInfo("ImagePath") = GV.FL_AdminLogin.AddInVar("Companylogo", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text) & "' ") 'Companylogo    BOS_ClientRegistration  CompanyCode
                        GV.LoginInfo("Group") = Group
                        GV.LoginInfo("DataBaseName") = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(8).Text)
                        GV.LoginInfo("CompanyCode") = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)

                        GV.LoginInfo("LoggedInAs") = "Admin"
                        GV.LoginInfo.Expires = Now.AddHours(9)
                        Response.Cookies.Add(GV.LoginInfo)
                        GV.ManageAllCookies = New HttpCookie("ManageAllCookies")
                        GV.ManageAllCookies("Session_Id") = GV.FL_AdminLogin.getAutoNumber("SessionId")
                        GV.ManageAllCookies("Path") = groupType
                        GV.ManageAllCookies("ID") = x
                        Response.Cookies.Add(GV.ManageAllCookies)

                    End If
                    Response.Redirect("~/Admin/SuperAdminHome.aspx")

                End If

            Else

                loginID = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(3).Text)
                Password = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
                Group = GV.FL.AddInVar("AgentType", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(loginID.Trim) & "' ")

                ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & loginID & "' and AgentPassword='" &  Password & "'  and AgentType='" & Group & "'")
                createMenuCookie(Group)
                If Group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
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
                                GV.LoginInfo("LoggedInAs") = ""
                                GV.LoginInfo("CompanyCode") = ""
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

                            GV.LoginInfo("LoggedInAs") = "Admin"
                            GV.LoginInfo.Expires = Now.AddHours(9)
                            Response.Cookies.Add(GV.LoginInfo)
                            GV.ManageAllCookies = New HttpCookie("ManageAllCookies")
                            GV.ManageAllCookies("Session_Id") = GV.FL_AdminLogin.getAutoNumber("SessionId")
                            'GV.ManageAllCookies("Path") = groupType
                            'GV.ManageAllCookies("ID") = x

                            If GV.get_ManageAllCookies_SessionVariables("Path", Request, Response) = "" Then
                                GV.ManageAllCookies("Path") = groupType
                            Else
                                GV.ManageAllCookies("Path") = GV.get_ManageAllCookies_SessionVariables("Path", Request, Response) & ":" & groupType
                            End If
                            If GV.get_ManageAllCookies_SessionVariables("ID", Request, Response) = "" Then
                                GV.ManageAllCookies("ID") = x
                            Else
                                GV.ManageAllCookies("ID") = GV.get_ManageAllCookies_SessionVariables("ID", Request, Response) & ":" & x
                            End If

                            Response.Cookies.Add(GV.ManageAllCookies)

                        End If
                        Response.Redirect("~/Admin/SuperAdminHome.aspx")

                    End If
                ElseIf Group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
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
                                GV.LoginInfo("LoggedInAs") = ""
                                GV.LoginInfo("CompanyCode") = ""

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

                            GV.LoginInfo("LoggedInAs") = "Master Distributor"
                            GV.LoginInfo.Expires = Now.AddHours(9)
                            Response.Cookies.Add(GV.LoginInfo)
                            GV.ManageAllCookies = New HttpCookie("ManageAllCookies")
                            If GV.get_ManageAllCookies_SessionVariables("Path", Request, Response) = "" Then
                                GV.ManageAllCookies("Path") = groupType
                            Else
                                GV.ManageAllCookies("Path") = GV.get_ManageAllCookies_SessionVariables("Path", Request, Response) & ":" & groupType
                            End If
                            If GV.get_ManageAllCookies_SessionVariables("ID", Request, Response) = "" Then
                                GV.ManageAllCookies("ID") = x
                            Else
                                GV.ManageAllCookies("ID") = GV.get_ManageAllCookies_SessionVariables("ID", Request, Response) & ":" & x
                            End If

                            Response.Cookies.Add(GV.ManageAllCookies)
                        End If
                        Response.Redirect("~/Admin/SuperAdminHome.aspx")

                    End If
                ElseIf Group = "Retailer" Then
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
                                GV.LoginInfo("LoggedInAs") = ""
                                GV.LoginInfo("CompanyCode") = ""
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
                            '" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.
                            GV.LoginInfo("LoggedInAs") = "Distributor"

                            GV.LoginInfo.Expires = Now.AddHours(9)
                            Response.Cookies.Add(GV.LoginInfo)
                            If GV.get_ManageAllCookies_SessionVariables("Path", Request, Response) = "" Then
                                GV.ManageAllCookies("Path") = groupType
                            Else
                                GV.ManageAllCookies("Path") = GV.get_ManageAllCookies_SessionVariables("Path", Request, Response) & ":" & groupType
                            End If
                            If GV.get_ManageAllCookies_SessionVariables("ID", Request, Response) = "" Then
                                GV.ManageAllCookies("ID") = x
                            Else
                                GV.ManageAllCookies("ID") = GV.get_ManageAllCookies_SessionVariables("ID", Request, Response) & ":" & x
                            End If

                            Response.Cookies.Add(GV.ManageAllCookies)
                        End If
                        Response.Redirect("~/Admin/SuperAdminHome.aspx")

                    End If
                Else

                End If


                GV.ManageAllCookies.Expires = Now.AddHours(9)
            End If

           
          
            

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Try
            clear()
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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToExcel_New(GridView1, Response, "", "DistributorDetails", lblExportQry.Text, "dyanamic")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImagebtnWOrd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnword.Click
        Try
            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToWord_New(GridView1, Response, "DistributorDetails", lblExportQry.Text, "dyanamic")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ExportToPdf_DivTag_HavingGridview(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagepdf.Click
        Try

            If Not lblExportQry.Text = "" Then
                If GridView1.Rows.Count > 0 Then
                    GV.ExportToPdf_New(GridView1, "", Response, "DistributorDetails", lblExportQry.Text, "dyanamic")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub createMenuCookie(Group As String)
        Try
            Dim found As Boolean = False
            Dim dataAdd As Integer = 0
            If Group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
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
            ElseIf Group.Trim.ToUpper = "Distributor".Trim.ToUpper Then

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
            ElseIf Group = "Retailer" Then
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
            ElseIf Group.Trim.ToUpper = "Admin".Trim.ToUpper Then
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
            End If
            
        Catch ex As Exception

        End Try
    End Sub


End Class