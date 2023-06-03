Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Web.Mail
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.SmtpClient
Imports System.Net.WebClient
Imports Newtonsoft.Json.Linq
Imports System
Imports System.Security.Cryptography
Imports Newtonsoft.Json


Public Class PaytsLogin
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("SUPERADMIN")
    Dim Group As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If ((Not (Request.Cookies("Super_UserName")) Is Nothing) _
                            AndAlso (Not (Request.Cookies("Super_Password")) Is Nothing)) Then

                    If Request.Cookies("Super_CompanyCode") Is Nothing Then
                        txtCompanyCode.Text = ""
                    Else
                        txtCompanyCode.Text = Request.Cookies("Super_CompanyCode").Value
                    End If

                    If Request.Cookies("Super_LoginAs") Is Nothing Then
                        ddl_Login_For.SelectedIndex = 0
                    Else
                        ddl_Login_For.SelectedValue = Request.Cookies("Super_LoginAs").Value
                    End If

                    If Request.Cookies("Super_UserName") Is Nothing Then
                        txtUserName.Text = ""
                    Else
                        txtUserName.Text = Request.Cookies("Super_UserName").Value
                    End If

                    If Request.Cookies("Super_Password") Is Nothing Then
                        txtPassword.Attributes("value") = ""
                    Else
                        txtPassword.Attributes("value") = Request.Cookies("Super_Password").Value
                    End If
                End If

                If Not IsPostBack Then


                    Dim host As String = Request.Url.Host.ToLower()
                    If host = "boscenter.in" Then
                        'Response.Redirect("kvishmoney/index.html")
                        Response.Redirect("bos_index.html")
                    ElseIf host = "agent.kvishmoney.com" Then
                        txtCompanyCode.Text = "CMP1185"
                        txtCompanyCode.Enabled = True
                        txtCompanyCode.CssClass = "form-control"
                    ElseIf host = "easytalk.services" Then
                        txtCompanyCode.Text = "CMP1185"
                        txtCompanyCode.Enabled = False
                        txtCompanyCode.CssClass = "form-control"
                    ElseIf host = "payts.app" Then
                        txtCompanyCode.Text = "CMP1185"
                        txtCompanyCode.Enabled = False
                        txtCompanyCode.CssClass = "form-control"
                    Else
                        If Not Request.QueryString("admin") Is Nothing Then
                            If Not Request.QueryString("admin").Trim = "" Then
                                txtCompanyCode.Text = Request.QueryString("admin").Trim
                                txtCompanyCode.Enabled = False
                            Else
                                txtCompanyCode.Text = "CMP1185"
                                txtCompanyCode.Enabled = True
                            End If
                        Else
                            txtCompanyCode.Text = "CMP1185"
                            txtCompanyCode.Enabled = True
                        End If
                        txtCompanyCode.CssClass = "form-control"
                    End If



                End If
            End If

            lblError.Text = ""
            lblError.CssClass = ""



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


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
            lblinvalid.Text = ""
            lblinvalid.CssClass = ""

            Dim CompanyCode As String = GV.parseString(txtCompanyCode.Text.Trim)


            If CompanyCode.Trim = "" Then
                CompanyCode = "CMP1185"
            End If
            txtCompanyCode_ForgotPass.Text = CompanyCode


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
            Dim CompanyCode As String = ""
            Dim DBName As String = ""

            lbl_Forgot_CompanyCode.Text = ""
            lbl_Forgot_DBName.Text = ""


            If txtCompanyCode_ForgotPass.Text.Trim = "" Then
                lblinvalid.Text = "Please Enter Company Code"
                lblinvalid.CssClass = "errorlabels"
                ModalPopupExtender1.Show()
                Exit Sub
            Else
                CompanyCode = txtCompanyCode_ForgotPass.Text.Trim
                DBName = GV.FL_AdminLogin.AddInVar("DatabaseName", " BOS_ClientRegistration where CompanyCode='" & CompanyCode & "' and [Status]='Active' ")

                If DBName.Trim = "" Then
                    lblinvalid.Text = "CompanyCode is Incorrect"
                    lblinvalid.Visible = True
                    lblinvalid.CssClass = "errorlabels"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

            End If
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

                        If GV.FL.RecCount(DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & GV.parseString(MemberId) & "'") Then
                            If GV.FL.RecCount(DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where  RegistrationId='" & GV.parseString(MemberId) & "' and EmailID='" & GV.parseString(Email) & "'") > 0 Then


                                GV.sendNewMail(OTP, " BOS Forgot Password - OTP ", GV.parseString(Email))
                                txtOtpNo.Text = ""
                                lblinvalid.Text = ""
                                lblinvalid.CssClass = ""

                                lbl_Forgot_CompanyCode.Text = CompanyCode
                                lbl_Forgot_DBName.Text = DBName


                                ModalPopupExtender2.Show()
                            Else
                                lblinvalid.Text = "Invalid EmailID."
                                lblinvalid.CssClass = "errorlabels"
                                ModalPopupExtender1.Show()
                            End If
                        Else
                            lblinvalid.Text = "Invalid Login Id."
                            lblinvalid.CssClass = "errorlabels"
                            ModalPopupExtender1.Show()
                        End If

                    ElseIf rdbType.SelectedValue = "Mobile No" Then

                        If GV.FL.RecCount(DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & GV.parseString(MemberId) & "' ") Then
                            If GV.FL.RecCount(DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & GV.parseString(MemberId) & "' and  MobileNo='" & GV.parseString(Email) & "' ") > 0 Then

                                If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then
                                    Dim vName As String = GV.FL.AddInVar("isnull(FirstName,'') + ' ' + isnull(LastName,'')", " " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(MemberId) & "' ")
                                    If DBName.Trim = "CMP1165" Then
                                        Dim vMessage As String = "Dear " & vName & " Your OTP is : " & Session("OTP") & " . Please Do Not Share With Anyone . Thanks For Using Kuber Money"
                                        GV.send_Template_Based_SMS_API(GV.parseString(Email), vMessage, "Send Otp", "CMP1165")
                                    Else

                                        Dim vMessage As String = "Dear " & vName & " Your OTP is : " & Session("OTP") & " . Please Do Not Share With Anyone . Thanks For Using BOS."
                                        GV.send_Template_Based_SMS_API(GV.parseString(Email), vMessage, "Send Otp", "")
                                    End If
                                End If

                                'GV.sendSMSThroughAPI(GV.parseString(Email), OTP)

                                lbl_Message.Text = "OTP has been sent to your Mobile Number."


                                lbl_Forgot_CompanyCode.Text = CompanyCode
                                lbl_Forgot_DBName.Text = DBName

                                txtOtpNo.Text = ""
                                lblinvalid.Text = ""
                                lblinvalid.CssClass = ""

                                ModalPopupExtender2.Show()

                            Else
                                lblinvalid.Text = "Invalid Mobile Number."
                                lblinvalid.CssClass = "errorlabels"
                                ModalPopupExtender1.Show()
                            End If
                        Else
                            lblinvalid.Text = "Invalid Login ID."
                            lblinvalid.CssClass = "errorlabels"
                            ModalPopupExtender1.Show()
                        End If
                    End If

                Else
                    'Case Employee / Admin
                    If rdbType.SelectedValue = "Email ID&nbsp;&nbsp;" Then

                        If GV.FL.RecCount(DBName & ".dbo.CRM_Login_Details where  User_ID='" & GV.parseString(MemberId) & "'") Then
                            If GV.FL.RecCount(DBName & ".dbo.CRM_Login_Details where   User_ID='" & GV.parseString(MemberId) & "' and EmailID='" & GV.parseString(Email) & "'") > 0 Then
                                GV.sendNewMail(OTP, " BOS Forget Password - OTP ", GV.parseString(Email))
                                lbl_Forgot_CompanyCode.Text = CompanyCode
                                lbl_Forgot_DBName.Text = DBName


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
                            lblinvalid.Text = "Invalid Login Id."
                            lblinvalid.CssClass = "errorlabels"
                            ModalPopupExtender1.Show()
                        End If

                    ElseIf rdbType.SelectedValue = "Mobile No" Then

                        If GV.FL.RecCount(DBName & ".dbo.CRM_Login_Details where   User_ID='" & GV.parseString(MemberId) & "' ") Then
                            If GV.FL.RecCount(DBName & ".dbo.CRM_Login_Details where  User_ID='" & GV.parseString(MemberId) & "' and  MobileNo='" & GV.parseString(Email) & "' ") > 0 Then
                                'GV.sendSMSThroughAPI(GV.parseString(Email), OTP)

                                If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then
                                    Dim vName As String = GV.FL.AddInVar("isnull(FirstName,'') + ' ' + isnull(LastName,'')", " " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(MemberId) & "' ")
                                    If DBName.Trim = "CMP1165" Then
                                        Dim vMessage As String = "Dear " & vName & " Your OTP is : " & Session("OTP") & " . Please Do Not Share With Anyone . Thanks For Using Kuber Money"
                                        GV.send_Template_Based_SMS_API(GV.parseString(Email), vMessage, "Send Otp", "CMP1165")
                                    Else
                                        Dim vMessage As String = "Dear " & vName & " Your OTP is : " & Session("OTP") & " . Please Do Not Share With Anyone . Thanks For Using BOS."
                                        GV.send_Template_Based_SMS_API(GV.parseString(Email), vMessage, "Send Otp", "")
                                    End If
                                End If

                                lbl_Forgot_CompanyCode.Text = CompanyCode
                                lbl_Forgot_DBName.Text = DBName

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
                            lblinvalid.Text = "Invalid Login ID."
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

            Dim CompanyCode As String = ""
            Dim DBName As String = ""

            Dim str As String = ""
            Dim msg As String = ""
            lblresult.Text = ""
            If btnproceed.Text = "Ok" Then
                btnproceed.Text = "Proceed"
                Exit Sub
            End If
            CompanyCode = lbl_Forgot_CompanyCode.Text.Trim
            DBName = lbl_Forgot_DBName.Text.Trim


            If Not txtLoginId.Text.Trim = "" Then
                If txtLoginId.Text.Trim.Contains("-") Then
                    str = GV.FL_AdminLogin.AddInVar("RegistrationId + ',' + AgentPassword+ ',' + isnull(TransactionPin,'')", DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where    RegistrationId='" & GV.parseString(txtLoginId.Text.Trim) & "'")
                Else
                    str = GV.FL_AdminLogin.AddInVar("User_ID + ',' + User_password+ ',' + isnull(TransactionPin,'')", DBName & ".dbo.CRM_Login_Details where   User_ID='" & GV.parseString(txtLoginId.Text.Trim) & "'")
                End If

                Dim strarr() As String = str.Split(",")


                If Session("Src") = "EmailID" Then
                    msg = "Dear User, <br> <br> :: Your BOS Center Login Details :: <br> <br>"
                    msg = msg & "Company Code : " & CompanyCode.Trim & "<br>"
                    msg = msg & "User ID : " & strarr(0).ToString.Trim & "<br>"
                    msg = msg & "Password : " & strarr(1).ToString.Trim & "<br>"
                    msg = msg & "Login Link :  https://www.boscenter.in" & "<br>"

                    msg = msg & "<br> <br> <br> Thank you , <br> Team BOS Center"

                    GV.sendNewMail(msg, "BOS Center Login Details ", GV.parseString(GV.parseString(txtEmail.Text.Trim)))
                    lblresult.Text = "Your Login Details Successfully Sent to Your Email Id"
                    lblresult.CssClass = "successlabels"
                ElseIf Session("Src") = "Mobile" Then
                    msg = ":: Your BOS Center Login Details :: " & Environment.NewLine & Environment.NewLine
                    msg = msg & "Company Code : " & CompanyCode.Trim & Environment.NewLine
                    msg = msg & "User ID : " & strarr(0).ToString.Trim & Environment.NewLine
                    msg = msg & "Password : " & strarr(1).ToString.Trim & Environment.NewLine & Environment.NewLine
                    msg = msg & "Login Link :  https://www.boscenter.in"


                    If rdbForgotType.SelectedValue = "Password" Then
                        If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then
                            Dim vName As String = GV.FL.AddInVar("isnull(FirstName,'') + ' ' + isnull(LastName,'')", " " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(txtLoginId.Text.Trim) & "' ")
                            If DBName.Trim = "CMP1165" Then

                                Dim vMessage As String = "Dear " & vName & " Your Password Is " & strarr(1).ToString.Trim & " Please Do Not Share With Anyone . Thanks For Using Kuber Money"
                                GV.send_Template_Based_SMS_API(GV.parseString(txtEmail.Text.Trim), vMessage, "Forgot Password", "CMP1165")
                            Else
                                Dim vMessage As String = "Dear " & vName & " Your Password Is " & strarr(1).ToString.Trim & " Please Do Not Share With Anyone. Thanks For Using BOS."
                                GV.send_Template_Based_SMS_API(GV.parseString(txtEmail.Text.Trim), vMessage, "Forgot Password", "")
                            End If
                        End If
                    ElseIf rdbForgotType.SelectedValue = "PIN" Then
                        If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then
                            Dim vName As String = GV.FL.AddInVar("isnull(FirstName,'') + ' ' + isnull(LastName,'')", " " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.parseString(txtLoginId.Text.Trim) & "' ")
                            If DBName.Trim = "CMP1165" Then

                                Dim vMessage As String = "Dear " & vName & " , Your TPIN Is " & strarr(2).ToString.Trim & " . Please Do Not Share With Anyone . Thanks For Using Kuber Money"
                                GV.send_Template_Based_SMS_API(GV.parseString(txtEmail.Text.Trim), vMessage, "Forgot Pin", "CMP1165")

                            Else
                                Dim vMessage As String = "Dear " & vName & ", Your TPIN is " & strarr(2).ToString.Trim & " . Please Do Not Share With Anyone. Thanks For Using BOS."
                                GV.send_Template_Based_SMS_API(GV.parseString(txtEmail.Text.Trim), vMessage, "Forgot Pin", "")
                            End If
                        End If
                    End If

                    'GV.sendSMSThroughAPI(GV.parseString(txtEmail.Text.Trim), msg)

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

    Protected Sub lnkSignup_Click(sender As Object, e As EventArgs) Handles lnkSignup.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""

            Dim CompanyCode As String = GV.parseString(txtCompanyCode.Text.Trim)

            If CompanyCode.Trim = "" Then
                CompanyCode = "CMP1185"
            Else
                Dim DBName As String = GV.FL_AdminLogin.AddInVar("DatabaseName", " BOS_ClientRegistration where CompanyCode='" & CompanyCode & "' and [Status]='Active' ")
                'BosCenter_DB.dbo.
                If DBName.Trim = "" Then
                    lblError.Text = "CompanyCode is Incorrect"
                    lblError.Visible = True
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            End If
            Response.Redirect("CreateCustomer.aspx?admin=" & CompanyCode)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            lblErrormsg.Text = ""
            lblErrormsg.CssClass = ""

            Dim CompanyCode As String = GV.parseString(txtCompanyCode.Text.Trim)

            Dim DBName As String = GV.FL_AdminLogin.AddInVar("DatabaseName", " BOS_ClientRegistration where CompanyCode='" & CompanyCode & "' and [Status]='Active' ")

            If DBName.Trim = "" Then
                lblError.Text = "CompanyCode is Incorrect"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            'this.MasterPageFile = "~/MasterPage2.master"

            If ddl_Login_For.SelectedIndex = 0 Then
                lblError.Text = "Select Login As ..."
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                lblError.Visible = False
            End If

            If txtUserName.Text = "" Or txtPassword.Text = "" Then
                lblError.Text = "Enter User Name And Password"
                lblError.Visible = True
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                lblError.Visible = False
            End If


            Dim Encrypted_Pass As String = ""
            If Not GV.parseString(txtPassword.Text.Trim) = "" Then
                Encrypted_Pass = GV.EncryptString(GV.key, txtPassword.Text.Trim)
            End If

            GV.Expire_ManageAllCookies_Session(Request, Response)

            Dim Result As Boolean = False


            If Len(txtUserName.Text.Trim) = 10 And IsNumeric(txtUserName.Text.Trim) And Not ddl_Login_For.SelectedValue = "Others" Then

                If GV.FL_AdminLogin.RecCount(DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where MobileNo='" & GV.parseString(txtUserName.Text.Trim) & "' and AgentType='" & ddl_Login_For.SelectedValue & "' ") Then
                    Result = True
                Else
                    Result = False
                    lblError.Text = "UserID Or Password Is Incorrect"
                    lblError.CssClass = "errorlabels"
                    lblError.Visible = True
                    'Show Error 
                    Exit Sub
                End If

                ds = GV.FL_AdminLogin.OpenDs(DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where  MobileNo='" & GV.parseString(txtUserName.Text.Trim) & "'  and AgentType='" & ddl_Login_For.SelectedValue & "'  and AgentPassword='" & Encrypted_Pass & "' ")
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        Group = ddl_Login_For.SelectedValue
                        Dim AccountStatus As String = ds.Tables(0).Rows(0).Item("ActiveStatus").ToString()

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
                                GV.LoginInfo("CompanyCode") = ""
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
                            GV.LoginInfo("CompanyCode") = CompanyCode
                            GV.LoginInfo("DataBaseName") = DBName
                            GV.LoginInfo.Expires = Now.AddHours(9)
                            Response.Cookies.Add(GV.LoginInfo)
                        End If


                        lblError.Visible = False

                        'If chkRememberMe.Checked Then
                        '    Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(30)
                        '    Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                        '    Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                        '    Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(30)
                        'Else
                        '    Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                        '    Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                        '    Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                        '    Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)
                        'End If

                        Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)



                        Response.Cookies("Super_CompanyCode").Value = txtCompanyCode.Text.Trim
                        Response.Cookies("Super_UserName").Value = txtUserName.Text.Trim
                        Response.Cookies("Super_Password").Value = txtPassword.Text.Trim
                        Response.Cookies("Super_LoginAs").Value = ddl_Login_For.SelectedValue
                        Session("NotificationPic") = "Start"
                        Response.Redirect("~/Admin/SAM_DashBoard.aspx")
                    Else
                        lblError.Visible = True
                        lblError.Text = "UserID Or Password Is Incorrect"
                        lblError.CssClass = "errorlabels"

                    End If

                Else
                    lblError.Visible = True
                    lblError.Text = "UserID Or Password Is Incorrect"
                    lblError.CssClass = "errorlabels"

                End If


                Exit Sub

            ElseIf txtUserName.Text.Contains("-") Then
                ds = GV.FL_AdminLogin.OpenDs(DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & GV.parseString(txtUserName.Text.Trim) & "' and AgentPassword='" & Encrypted_Pass & "' ")
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Group = ds.Tables(0).Rows(0).Item("AgentType").ToString()
                        Dim AccountStatus As String = ds.Tables(0).Rows(0).Item("ActiveStatus").ToString()
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
                                GV.LoginInfo("CompanyCode") = ""
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
                            GV.LoginInfo("CompanyCode") = CompanyCode
                            GV.LoginInfo("DataBaseName") = DBName
                            GV.LoginInfo.Expires = Now.AddHours(9)
                            Response.Cookies.Add(GV.LoginInfo)
                        End If


                        lblError.Visible = False

                        'If chkRememberMe.Checked Then
                        '    Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(30)
                        '    Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                        '    Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                        '    Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(30)
                        'Else
                        '    Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                        '    Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                        '    Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                        '    Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)
                        'End If

                        Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)


                        Response.Cookies("Super_CompanyCode").Value = txtCompanyCode.Text.Trim
                        Response.Cookies("Super_UserName").Value = txtUserName.Text.Trim
                        Response.Cookies("Super_Password").Value = txtPassword.Text.Trim
                        Response.Cookies("Super_LoginAs").Value = ddl_Login_For.SelectedValue
                        Session("NotificationPic") = "Start"
                        'Response.Redirect("~/Admin/SuperAdminHome.aspx")
                        Response.Redirect("~/Admin/SAM_DashBoard.aspx")
                    Else
                        lblError.Visible = True
                        lblError.Text = "UserID Or Password Is Incorrect"
                        lblError.CssClass = "errorlabels"

                    End If

                Else
                    lblError.Visible = True
                    lblError.Text = "UserID Or Password Is Incorrect"
                    lblError.CssClass = "errorlabels"

                End If



            Else

                If Not ddl_Login_For.SelectedValue = "Others" Then
                    lblError.Visible = True
                    lblError.Text = "Select Valid UserType"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                Dim zz As String = DBName.Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.parseString(txtUserName.Text.Trim) & "' and User_Password='" & Encrypted_Pass & "' and  Recordstatus='Active' and AccountStatus='Active'"
                ds = GV.FL_AdminLogin.OpenDs(zz)
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
                            GV.LoginInfo("CompanyCode") = ""
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
                        'connectDatabaseName = DefaultDatabase()
                        CanLogin = ds.Tables(0).Rows(0).Item("Canlogin").ToString()
                        GV.LoginInfo("BranchCode") = ""
                        GV.LoginInfo("BranchName") = ""
                        GV.LoginInfo("UserName") = ds.Tables(0).Rows(0).Item("User_Name").ToString()
                        GV.LoginInfo("LastLogin") = Now.ToString("dd/MM/yyyy h:mm:ss tt")
                        GV.LoginInfo("Group") = ds.Tables(0).Rows(0).Item("User_Type").ToString()
                        GV.LoginInfo("ImagePath") = ds.Tables(0).Rows(0).Item("ImagePath").ToString()
                        GV.LoginInfo("DataBaseName") = DBName
                        GV.LoginInfo("CompanyCode") = CompanyCode
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
                    Dim str As String = ""
                    str = "Update " & DBName.Trim & ".dbo.CRM_Login_Details set LastLoginTime=getdate() where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "';"
                    str = str & " " & "insert into " & DBName.Trim & ".dbo.CRM_OpertaorLoginReport (User_Type,OperatorName,LoginId,SessionId,LoginTime) values ('" & GV.get_SuperAdmin_SessionVariables("Group", Request, Response) & "','" & GV.get_SuperAdmin_SessionVariables("UserName", Request, Response) & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & sessionID & "',getdate());"
                    GV.FL_AdminLogin.DMLQueries(str)

                    'If chkRememberMe.Checked Then
                    '    Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(30)
                    '    Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                    '    Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                    '    Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(30)
                    'Else
                    '    Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                    '    Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                    '    Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                    '    Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)
                    'End If

                    Response.Cookies("Super_CompanyCode").Value = txtCompanyCode.Text.Trim
                    Response.Cookies("Super_UserName").Value = txtUserName.Text.Trim
                    Response.Cookies("Super_Password").Value = txtPassword.Text.Trim
                    Response.Cookies("Super_LoginAs").Value = ddl_Login_For.SelectedValue
                    Session("NotificationPic") = "Start"
                    'Response.Redirect("~/Admin/SuperAdminHome.aspx")
                    Response.Redirect("~/Admin/SAM_DashBoard.aspx")
                Else

                    lblError.Text = "UserID Or Password Is Incorrect"
                    lblError.Visible = True
                    lblError.CssClass = "errorlabels"

                End If

            End If




        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

End Class