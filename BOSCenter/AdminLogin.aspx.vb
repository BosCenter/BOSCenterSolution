Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Web.Mail
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.SmtpClient
Imports System.Net.WebClient

Public Class AdminLogin
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Dim Group As String = ""
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try




            'this.MasterPageFile = "~/MasterPage2.master"
            lblErrormsg.Text = ""
            lblErrormsg.CssClass = ""
            If txtUserName.Text = "" And txtPassword.Text = "" Then
                lblError.Visible = True
            Else
                lblError.Visible = False
            End If
            GV.Expire_ManageAllCookies_Session(Request, Response)
            If txtUserName.Text.Contains("-") Then

                ds = GV.FL_AdminLogin.OpenDs("BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(txtUserName.Text.Trim) & "' and AgentPassword='" & GV.parseString(txtPassword.Text.Trim) & "' ")

                If ds.Tables(0).Rows.Count > 0 Then

                    Group = GV.FL.AddInVar("AgentType", "BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(txtUserName.Text.Trim) & "' ")

                    Dim AccountStatus As String = GV.FL.AddInVar("ActiveStatus", "BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(txtUserName.Text.Trim) & "' ")
                    If Not AccountStatus.Trim.ToUpper = "Active".Trim.ToUpper Then
                        lblErrormsg.Text = "Your Account Is InActive."
                        lblErrormsg.CssClass = "errorlabels"
                        Exit Sub
                    End If



                    createMenuCookie()
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
                            'LoggedInAs
                            GV.LoginInfo = New HttpCookie("LoginInfo")
                            GV.LoginInfo("Session_Id") = ""
                            GV.LoginInfo("LoginID") = ""
                            GV.LoginInfo("UserName") = ""
                            GV.LoginInfo("Designation") = ""
                            GV.LoginInfo("LastLogin") = ""
                            GV.LoginInfo("Group") = ""
                            GV.LoginInfo("BranchCode") = ""
                            GV.LoginInfo("BranchName") = ""
                            GV.LoginInfo("ImagePath") = ""
                            GV.LoginInfo("DataBaseName") = ""
                            GV.LoginInfo("LoggedInAs") = ""
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
                        GV.LoginInfo("LoginID") = ds.Tables(0).Rows(0).Item("RegistrationId").ToString()
                        GV.LoginInfo("UserName") = ds.Tables(0).Rows(0).Item("FirstName").ToString()
                        'GV.LoginInfo("LastLogin") = Now.ToString("dd/MM/yyyy h:mm:ss tt")
                        GV.LoginInfo("Group") = ds.Tables(0).Rows(0).Item("AgentType").ToString()
                        GV.LoginInfo("LoggedInAs") = ds.Tables(0).Rows(0).Item("AgentType").ToString()
                        GV.LoginInfo("ImagePath") = ds.Tables(0).Rows(0).Item("UploadPhoto").ToString()
                        'GV.LoginInfo("DataBaseName") = GV.DefaultDatabase
                        GV.LoginInfo.Expires = Now.AddHours(9)
                        Response.Cookies.Add(GV.LoginInfo)
                    End If


                    lblError.Visible = False

                    If chkRememberMe.Checked Then
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                    Else
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                    End If
                    Response.Cookies("Super_UserName").Value = txtUserName.Text.Trim
                    Response.Cookies("Super_Password").Value = txtPassword.Text.Trim

                    Response.Redirect("~/Admin/SuperAdminHome.aspx")
                Else
                    lblError.Visible = True

                End If

            Else

                ds = GV.FL_AdminLogin.OpenDs("CRM_Login_Details where User_ID='" & GV.parseString(txtUserName.Text.Trim) & "' and User_Password='" & GV.parseString(txtPassword.Text.Trim) & "' and  Recordstatus='Active' and AccountStatus='Active'")

                If ds.Tables(0).Rows.Count > 0 Then

                    createMenuCookie()


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
                        'connectDatabaseName = DefaultDatabase()
                        DBNAME = GV.DefaultDatabase()

                        CanLogin = ds.Tables(0).Rows(0).Item("Canlogin").ToString()
                        GV.LoginInfo("BranchCode") = ds.Tables(0).Rows(0).Item("BranchCode").ToString()
                        GV.LoginInfo("BranchName") = ds.Tables(0).Rows(0).Item("BranchName").ToString()
                        GV.LoginInfo("UserName") = ds.Tables(0).Rows(0).Item("User_Name").ToString()
                        GV.LoginInfo("LastLogin") = Now.ToString("dd/MM/yyyy h:mm:ss tt")
                        GV.LoginInfo("Group") = ds.Tables(0).Rows(0).Item("User_Type").ToString()
                        GV.LoginInfo("ImagePath") = ds.Tables(0).Rows(0).Item("ImagePath").ToString()
                        GV.LoginInfo("DataBaseName") = GV.DefaultDatabase
                        GV.LoginInfo.Expires = Now.AddHours(9)
                        Response.Cookies.Add(GV.LoginInfo)
                    End If

                    If GV.Verify_LoginTimeOut(ds.Tables(0).Rows(0).Item("User_ID").ToString(), "", "Employee") = False Then
                        lblErrormsg.Text = "Incorrect Login Time."
                        lblErrormsg.CssClass = "errorlabels"
                        Exit Sub
                    End If

                    If CanLogin.Trim.ToUpper = "NO".Trim.ToUpper Then
                        lblErrormsg.Text = "You Are Not Authorized To Login."
                        lblErrormsg.CssClass = "errorlabels"
                        Exit Sub
                    End If

                    lblError.Visible = False

                    GV.New_SuperAdmin_Session = True
                    Dim str As String
                    str = "Update CRM_Login_Details set LastLoginTime=getdate() where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "';"
                    str = str & " " & "insert into CRM_OpertaorLoginReport (User_Type,OperatorName,LoginId,SessionId,LoginTime) values ('" & GV.get_SuperAdmin_SessionVariables("Group", Request, Response) & "','" & GV.get_SuperAdmin_SessionVariables("UserName", Request, Response) & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & sessionID & "',getdate());"

                    GV.FL_AdminLogin.DMLQueries(str)

                    If chkRememberMe.Checked Then
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                    Else
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                    End If
                    Response.Cookies("Super_UserName").Value = txtUserName.Text.Trim
                    Response.Cookies("Super_Password").Value = txtPassword.Text.Trim

                    Response.Redirect("~/Admin/SuperAdminHome.aspx")
                Else
                    lblError.Visible = True

                End If
            End If




        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If ((Not (Request.Cookies("Super_UserName")) Is Nothing) _
                            AndAlso (Not (Request.Cookies("Super_Password")) Is Nothing)) Then
                    txtUserName.Text = Request.Cookies("Super_UserName").Value
                    txtPassword.Attributes("value") = Request.Cookies("Super_Password").Value
                    chkRememberMe.Checked = True
                End If
            End If



            '  GV.sendNewMail(" Hello This is a Test again a test MAil ", " Hellow This is again New test mail ", "fasf43242@gmdail.com;sanarana44@gmail.com;verma.eklavya@gmail.com;support@businessonlinesolution.in;sdfafs@gdfs.com")



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    'Protected Sub rdbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdbType.SelectedIndexChanged
    '    Try
    '        txtEmail.Visible = True
    '        txtEmail.Text = ""
    '        btnForgot_SubmitDetails.Visible = True

    '        If rdbType.SelectedValue = "Email ID&nbsp;&nbsp;" Then
    '            txtEmail().Attributes("PlaceHolder") = "Email ID"
    '        ElseIf rdbType.SelectedValue = "Mobile No" Then
    '            txtEmail().Attributes("PlaceHolder") = "Mobile No"
    '        End If
    '        ModalPopupExtender1.Show()

    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
    '    End Try
    'End Sub

    'Protected Sub btnForgotPassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnForgotPassword.Click
    '    Try
    '        rdbType.Visible = True
    '        txtEmail.Visible = False
    '        txtEmail.Text = ""
    '        lbl_Message.Text = ""
    '        lblInfo.Text = "Enter Registered emaiID or MobileNo to Recieve your login Details."
    '        btnForgot_SubmitDetails.Visible = False
    '        rdbType.ClearSelection()
    '        ModalPopupExtender1.Show()
    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
    '    End Try
    'End Sub

    'Protected Sub btnForgot_SubmitDetails_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnForgot_SubmitDetails.Click
    '    Try
    '        rdbType.Visible = False
    '        txtEmail.Visible = False
    '        lblInfo.Text = ""
    '        lbl_Message.Text = ""


    '        Dim str As String = ""
    '        Dim message As String = ""


    '        If rdbType.SelectedValue = "Email ID&nbsp;&nbsp;" Then
    '            If GV.FL_AdminLogin.RecCount("CRM_Login_Details where EmailID='" & GV.parseString(txtEmail.Text.Trim) & "'") Then

    '                str = GV.FL_AdminLogin.AddInVar("User_ID+','+User_Password", "CRM_Login_Details where EmailID='" & GV.parseString(txtEmail.Text.Trim) & "'")
    '                Dim strarr() As String = str.Split(",")
    '                message = "!!! Your Login Details !!!"
    '                message = message & "<br>" & "User Id : " & strarr(0)
    '                message = message & "<br>" & "User_Password : " & strarr(1)
    '                GV.sendNewMail(message, "password recovery!!!!", GV.parseString(txtEmail.Text.Trim))
    '                lbl_Message.Text = "login Details has been sent to your Email ID."
    '            Else
    '                lbl_Message.Text = "Invalid EmailID."
    '            End If
    '        ElseIf rdbType.SelectedValue = "Mobile No" Then
    '            If GV.FL_AdminLogin.RecCount("CRM_Login_Details where MobileNo='" & GV.parseString(txtEmail.Text.Trim) & "'") Then
    '                str = GV.FL_AdminLogin.AddInVar("User_ID+','+User_Password", "CRM_Login_Details where MobileNo='" & GV.parseString(txtEmail.Text.Trim) & "'")
    '                Dim strarr() As String = str.Split(",")
    '                message = "!!! Your Login Details !!!"
    '                message = message & "<br>" & "User Id : " & strarr(0)
    '                message = message & "<br>" & "User_Password : " & strarr(1)


    '                GV.SendSMS_ForgotPassword(message, txtEmail.Text.Trim)
    '                lbl_Message.Text = "login Details has been sent to your Mobile No."
    '            Else
    '                lbl_Message.Text = "Invalid mobile no."
    '            End If
    '        End If


    '        btnForgot_SubmitDetails.Visible = False
    '        ModalPopupExtender1.Show()
    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
    '    End Try
    'End Sub
    Public Sub createMenuCookie()
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub btnForgotPassword_Click(sender As Object, e As System.EventArgs) Handles btnForgotPassword.Click
        Try
            rdbType.Visible = True
            txtEmail.Visible = False
            txtEmail.Text = ""
            lbl_Message.Text = ""
            txtLoginId.Text = ""
            lblInfo.Text = "Enter Registered EmaiID or Mobile Number to Recieve your Login Details."
            btnForgot_SubmitDetails.Visible = False
            rdbType.ClearSelection()
            ModalPopupExtender1.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub rdbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdbType.SelectedIndexChanged
        Try
            txtEmail.Visible = True
            txtEmail.Text = ""
            btnForgot_SubmitDetails.Visible = True

            If rdbType.SelectedValue = "Email ID&nbsp;&nbsp;" Then
                txtEmail().Attributes("PlaceHolder") = "Email ID"
                Session("Src") = "EmailID"
            ElseIf rdbType.SelectedValue = "Mobile No" Then
                txtEmail().Attributes("PlaceHolder") = "Mobile No"
                Session("Src") = "Mobile"
            End If
            ModalPopupExtender1.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnForgot_SubmitDetails_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnForgot_SubmitDetails.Click
        Try

            lblInfo.Text = ""
            lbl_Message.Text = ""
            lblinvalid.Text = ""
            lblinvalid.CssClass = ""
            Dim Email As String = ""
            Dim MemberId As String = ""
            If Not txtLoginId.Text.Trim = "" Then
                MemberId = txtLoginId.Text.Trim
                If Not txtEmail.Text.Trim = "" Then
                    Email = txtEmail.Text.Trim
                Else
                    If Session("Src") = "EmailID" Then
                        lblinvalid.Text = "Please Enter Email ID"
                        lblinvalid.CssClass = "errorlabels"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    ElseIf Session("Src") = "Mobile" Then
                        lblinvalid.Text = "Please Enter Mobile No"
                        lblinvalid.CssClass = "errorlabels"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    Else
                        lblinvalid.Text = ""
                        lblinvalid.CssClass = ""
                    End If
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If
            Else
                lblinvalid.Text = "Please Enter Login ID"
                lblinvalid.CssClass = "errorlabels"
                ModalPopupExtender1.Show()
                Exit Sub
            End If
            Dim OTP As String = RandomOTP()
            Session("OTP") = OTP
            OTP = "Your One Time Password is : " & OTP
            If Not MemberId = "" Then
                If MemberId.Contains("-") Then
                    'Case Distributor / Sub Dis / Retailer 

                    If rdbType.SelectedValue = "Email ID&nbsp;&nbsp;" Then

                        If GV.FL.RecCount("BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(MemberId) & "'") Then
                            If GV.FL.RecCount("BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(MemberId) & "' and EmailID='" & GV.parseString(Email) & "'") > 0 Then
                                GV.sendNewMail(OTP, " BOS Forget Password - OTP ", GV.parseString(Email))
                                txtOtpNo.Text = ""
                                lblinvalid.Text = ""
                                lblinvalid.CssClass = ""
                                ModalPopupExtender2.Show()
                            Else
                                lblinvalid.Text = "Invalid EmailID."
                                lblinvalid.CssClass = "errorlabels"
                                ModalPopupExtender1.Show()
                            End If
                        Else
                            lblinvalid.Text = "Invalid Client Id."
                            lblinvalid.CssClass = "errorlabels"
                            ModalPopupExtender1.Show()
                        End If

                    ElseIf rdbType.SelectedValue = "Mobile No" Then

                        If GV.FL.RecCount("BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(MemberId) & "' ") Then
                            If GV.FL.RecCount("BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(MemberId) & "' and  MobileNo='" & GV.parseString(Email) & "' ") > 0 Then
                                GV.sendSMSThroughAPI(GV.parseString(Email), OTP)

                                lbl_Message.Text = "OTP has been sent to your Mobile Number."
                                txtOtpNo.Text = ""
                                ModalPopupExtender2.Show()
                                lblinvalid.Text = ""
                                lblinvalid.CssClass = ""
                            Else
                                lblinvalid.Text = "Invalid Mobile Number."
                                lblinvalid.CssClass = "errorlabels"
                                ModalPopupExtender1.Show()
                            End If
                        Else
                            lblinvalid.Text = "Invalid Client ID."
                            lblinvalid.CssClass = "errorlabels"
                            ModalPopupExtender1.Show()
                        End If
                    End If

                Else
                    'Case Employee / Admin
                    If rdbType.SelectedValue = "Email ID&nbsp;&nbsp;" Then

                        If GV.FL.RecCount("CRM_Login_Details where User_ID='" & GV.parseString(MemberId) & "'") Then
                            If GV.FL.RecCount("CRM_Login_Details where User_ID='" & GV.parseString(MemberId) & "' and EmailID='" & GV.parseString(Email) & "'") > 0 Then
                                GV.sendNewMail(OTP, " BOS Forget Password - OTP ", GV.parseString(Email))
                                txtOtpNo.Text = ""
                                lblinvalid.Text = ""
                                lblinvalid.CssClass = ""
                                ModalPopupExtender2.Show()
                            Else
                                lblinvalid.Text = "Invalid EmailID."
                                lblinvalid.CssClass = "errorlabels"
                                ModalPopupExtender1.Show()
                            End If
                        Else
                            lblinvalid.Text = "Invalid Client Id."
                            lblinvalid.CssClass = "errorlabels"
                            ModalPopupExtender1.Show()
                        End If

                    ElseIf rdbType.SelectedValue = "Mobile No" Then

                        If GV.FL.RecCount("CRM_Login_Details where User_ID='" & GV.parseString(MemberId) & "' ") Then
                            If GV.FL.RecCount("CRM_Login_Details where User_ID='" & GV.parseString(MemberId) & "' and  MobileNo='" & GV.parseString(Email) & "' ") > 0 Then
                                GV.sendSMSThroughAPI(GV.parseString(Email), OTP)

                                lbl_Message.Text = "OTP has been sent to your Mobile Number."
                                txtOtpNo.Text = ""
                                ModalPopupExtender2.Show()
                                lblinvalid.Text = ""
                                lblinvalid.CssClass = ""
                            Else
                                lblinvalid.Text = "Invalid Mobile Number."
                                lblinvalid.CssClass = "errorlabels"
                                ModalPopupExtender1.Show()
                            End If
                        Else
                            lblinvalid.Text = "Invalid Client ID."
                            lblinvalid.CssClass = "errorlabels"
                            ModalPopupExtender1.Show()
                        End If
                    End If
                End If



            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnOtpSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOtpSubmit.Click
        Try

            lblOTPError.Text = ""
            lblOTPError.CssClass = ""
            lblOTPError.Text = ""
            lblOTPError.CssClass = ""
            If txtOtpNo.Text = "" Then
                lblOTPError.Text = "Enter OTP No."
                lblOTPError.CssClass = "errorlabels"
                ModalPopupExtender2.Show()
                Exit Sub
            End If
            Dim Otp As String = ""
            If Not Session("OTP") = "" Then
                Otp = Session("OTP")
            End If

            If txtOtpNo.Text.Trim = GV.parseString(Otp) Then
                Session("OTP") = ""
                lblOTPError.Text = ""
                lblOTPError.CssClass = ""
                btnProceed_Click(sender, e)
            Else
                lblOTPError.Text = "Invalid OTP No."
                lblOTPError.CssClass = "errorlabels"
                ModalPopupExtender2.Show()
                Exit Sub
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub btnProceed_Click(sender As Object, e As System.EventArgs) Handles btnproceed.Click
        Try

            Dim str As String = ""
            Dim msg As String = ""
            lblresult.Text = ""
            If btnproceed.Text = "Ok" Then
                btnproceed.Text = "Proceed"
                Exit Sub
            End If
            If Not txtLoginId.Text.Trim = "" Then
                If txtLoginId.Text.Trim.Contains("-") Then
                    str = GV.FL_AdminLogin.AddInVar("RegistrationId + ',' + AgentPassword", "BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(txtLoginId.Text.Trim) & "'")
                Else
                    str = GV.FL_AdminLogin.AddInVar("User_ID + ',' + User_password", "CRM_Login_Details where User_ID='" & GV.parseString(txtLoginId.Text.Trim) & "'")
                End If

                Dim strarr() As String = str.Split(",")


                If Session("Src") = "EmailID" Then
                    msg = "Dear User, <br> <br> :: Your BOS Center Login Details :: <br> <br>"
                    msg = msg & "User ID : " & strarr(0).ToString.Trim & "<br>"
                    msg = msg & "Password : " & strarr(1).ToString.Trim & "<br>"
                    msg = msg & "Login Link :  https://www.boscenter.in" & "<br>"

                    msg = msg & "<br> <br> <br> Thank you , <br> Team BOS Center"

                    GV.sendNewMail(msg, "BOS Center Login Details ", GV.parseString(GV.parseString(txtEmail.Text.Trim)))
                    lblresult.Text = "Your Login Details Successfully Sent to Your Email Id"
                    lblresult.CssClass = "successlabels"
                ElseIf Session("Src") = "Mobile" Then
                    msg = ":: Your BOS Center Login Details :: " & Environment.NewLine & Environment.NewLine
                    msg = msg & "User ID : " & strarr(0).ToString.Trim & Environment.NewLine
                    msg = msg & "Password : " & strarr(1).ToString.Trim & Environment.NewLine & Environment.NewLine
                    msg = msg & "Login Link :  https://www.boscenter.in"

                    GV.sendSMSThroughAPI(GV.parseString(txtEmail.Text.Trim), msg)
                    lblresult.Text = "Your Login Details Successfully Sent to Your Mobile Number"
                    lblresult.CssClass = "successlabels"
                End If
                btnproceed.Text = "Ok"
                ModalPopupExtender3.Show()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Function RandomOTP() As String
        Dim finalString As String = ""
        Try
            Dim chars = "0123456789"
            Dim stringChars = New Char(3) {}
            Dim random = New Random()
            For i As Integer = 0 To stringChars.Length - 1
                stringChars(i) = chars(random.[Next](chars.Length))
            Next
            finalString = New String(stringChars)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
        Return finalString
    End Function
End Class

