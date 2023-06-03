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


Public Class _Default
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("SUPERADMIN")
    Dim Group As String = ""

    Private Function Encrypt_New(ByVal text As String) As String
        Dim EncryptionKey As String = "59d15ufbt4dj5uot"
        Dim clearBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(text)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using

                text = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using

        Return text
    End Function

    Private Function Decrypt_New(ByVal cipher As String) As String
        Dim EncryptionKey As String = "59d15ufbt4dj5uot" '"MAKV2SPBNI99212"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipher)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using

                cipher = System.Text.Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

        Return cipher
    End Function

    Public Shared Function Encrypt(ByVal plainText As String, ByVal passPhrase As String, ByVal saltValue As String, ByVal hashAlgorithm As String, ByVal passwordIterations As Integer, ByVal initVector As String, ByVal keySize As Integer) As String
        Dim initVectorBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(saltValue)
        Dim plainTextBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(plainText)
        Dim password As PasswordDeriveBytes = New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)
        Dim symmetricKey As RijndaelManaged = New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
        Dim memoryStream As MemoryStream = New MemoryStream()
        Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
        Return cipherText
    End Function


    Public Shared Function Decrypt(ByVal cipherText As String, ByVal passPhrase As String, ByVal saltValue As String, ByVal hashAlgorithm As String, ByVal passwordIterations As Integer, ByVal initVector As String, ByVal keySize As Integer) As String
        Dim initVectorBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(saltValue)
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)
        Dim password As PasswordDeriveBytes = New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)
        Dim symmetricKey As RijndaelManaged = New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
        Dim memoryStream As MemoryStream = New MemoryStream(cipherTextBytes)
        Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
        memoryStream.Close()
        cryptoStream.Close()
        Dim plainText As String = System.Text.Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
        Return plainText
    End Function

    Public Function testLogic(ByVal EncData As String) As String
        Try
            Dim returnJson As New Dictionary(Of String, String) From {
                      {"ERROR", ""},
                      {"MESSAGE", ""},
                      {"BALANCE", ""},
                      {"USERVAR1", ""},
                      {"USERVAR2", ""},
                      {"USERVAR3", ""}
                  }

            Dim Rec_json_string As String = Decrypt_New(EncData)
            Dim json_ As String = Rec_json_string
            Dim ser_ As JObject = JObject.Parse(json_)

            Dim username As String = ser_.SelectToken("username").ToString.Trim
            Dim password As String = ser_.SelectToken("password").ToString.Trim
            Dim amount As String = ser_.SelectToken("amount").ToString.Trim

            Dim internal_encData As String = Encrypt_New(JsonConvert.SerializeObject(returnJson, Formatting.Indented))
            Return internal_encData
        Catch ex As Exception

        End Try
    End Function

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            'Dim strss As String = Encrypt_New("{""username"":""9003056060"",""password"":""sivsiv123"", ""amount"":""2""}")
            'Dim strss As String = Decrypt_New("bQBX3+t5OFlYB1YcrTIYL4xMF7tgNF2p4e7ploVSgwvr67iuImOo9MSvRLHiaL6ZX1n07QL+dIpRYM0MLqgPhw==")
            'Dim dd As String = testLogic("kZd/K1WrHAtDlBIKP3gyHdJZwUZJ/0Gc0XNomVw82KeYZoCgSmFyAnZmDS0NphPWgxGgy61f60HBmQolFuIgAw9VJ27NNVF4bxp0mQOcflSAH4dydcFFFHCtnqloWHhJrVe5LQo5YNn2yhKIJtoiU4PVNvh6oQNCxtiu+/GyE3s=")
            'dd = dd

            'strss = Decrypt_New(dd)

            'strss = strss

            'bQBX3+t5OFlYB1YcrTIYL4xMF7tgNF2p4e7ploVSgwvr67iuImOo9MSvRLHiaL6ZX1n07QL+dIpRYM0MLqgPhw==
            'GV.sh1Encryption()

            ''Member No:RKITAPI190212
            ''Password:	5nrg7nrmz4
            ''Api Password:	cg45ob8
            ''Encryption Key:77bxjoceldz46lrm...
            'Dim partner_request_id As String = GV.RandomTransactionPin()
            ''{"ERROR":0,"STATUS":1,"ORDERID":51645247,"OPTRANSID":"ONR2101231816190530","PARTNERREQID":"634","MESSAGE":"Success","USERVAR1":"rte-554","USERVAR2":"","USERVAR3":"","COMMISSION":"0.3500"}
            'Dim IPAddress As String = ""
            'IPAddress = New System.Net.WebClient().DownloadString("https://rechargkit.biz/get/prepaid/mobile?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=7303334574&operator_code=1&amount=10&partner_request_id=" & partner_request_id & "&circle=5&recharge_type=NORMAL&user_var1=test")
            'Dim json1 As JObject = JObject.Parse(IPAddress)

            ''Fetch data from data root
            'Dim ERROR1 As String = json1.SelectToken("ERROR").ToString
            'Dim STATUS As String = json1.SelectToken("STATUS").ToString
            'Dim ORDERID As String = json1.SelectToken("ORDERID").ToString
            'Dim OPTRANSID As String = json1.SelectToken("OPTRANSID").ToString
            'Dim PARTNERREQID As String = json1.SelectToken("PARTNERREQID").ToString
            'Dim MESSAGE As String = json1.SelectToken("MESSAGE").ToString
            'Dim USERVAR1 As String = json1.SelectToken("USERVAR1").ToString
            'Dim USERVAR2 As String = json1.SelectToken("USERVAR2").ToString
            'Dim USERVAR3 As String = json1.SelectToken("USERVAR3").ToString
            'Dim COMMISSION As String = json1.SelectToken("COMMISSION").ToString

            'Exit Sub
            lblError.Text = ""
            lblError.CssClass = ""
            lblErrormsg.Text = ""
            lblErrormsg.CssClass = ""


            Dim CompanyCode As String = GV.parseString(txtCompanyCode.Text.Trim)

            'CompanyName

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
                Encrypted_Pass = GV.convertToHashMD5(txtPassword.Text.Trim)
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

                ds = GV.FL_AdminLogin.OpenDs(DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where  MobileNo='" & GV.parseString(txtUserName.Text.Trim) & "'  and AgentType='" & ddl_Login_For.SelectedValue & "'  and Encrypted_Pass='" & Encrypted_Pass & "' ")
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

                        If chkRememberMe.Checked Then
                            Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(30)
                        Else
                            Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)
                        End If
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
                ds = GV.FL_AdminLogin.OpenDs(DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & GV.parseString(txtUserName.Text.Trim) & "' and Encrypted_Pass='" & Encrypted_Pass & "' ")
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

                        If chkRememberMe.Checked Then
                            Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(30)
                        Else
                            Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)
                        End If
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
                Dim zz As String = DBName.Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.parseString(txtUserName.Text.Trim) & "' and Encrypted_Pass='" & Encrypted_Pass & "' and  Recordstatus='Active' and AccountStatus='Active'"
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
                    If chkRememberMe.Checked Then
                        Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(30)
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(30)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(30)
                        Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(30)
                    Else
                        Response.Cookies("Super_CompanyCode").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_UserName").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_Password").Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies("Super_LoginAs").Expires = DateTime.Now.AddDays(-1)
                    End If
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
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

                    chkRememberMe.Checked = True
                End If
                If Not IsPostBack Then

                    If Not Request.QueryString("admin") Is Nothing Then
                        If Not Request.QueryString("admin").Trim = "" Then
                            txtCompanyCode.Text = Request.QueryString("admin").Trim
                            txtCompanyCode.Enabled = False
                        Else
                            txtCompanyCode.Text = "CMP1085"
                            txtCompanyCode.Enabled = True
                        End If
                    Else
                        txtCompanyCode.Text = "CMP1085"
                        txtCompanyCode.Enabled = True
                    End If
                    txtCompanyCode.CssClass = "form-control"
                End If
            End If

            lblError.Text = ""
            lblError.CssClass = ""



            Dim VCompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " BOS_ClientRegistration where CompanyCode='" & GV.parseString(txtCompanyCode.Text.Trim) & "' and [Status]='Active' ")

            If VCompanyName.Trim = "" Then
                lblCompanyName.Text = "Login Panel"
            Else
                lblCompanyName.Text = VCompanyName
            End If



            'If DBName.Trim = "" Then
            '    lblError.Text = "CompanyCode is Incorrect"
            '    lblError.Visible = True
            '    lblError.CssClass = "errorlabels"
            '    Exit Sub
            'End If

            '  GV.sendNewMail(" Hello This is a Test again a test MAil ", " Hellow This is again New test mail ", "fasf43242@gmdail.com;sanarana44@gmail.com;verma.eklavya@gmail.com;support@businessonlinesolution.in;sdfafs@gdfs.com")



        Catch ex As Exception

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

    '    Catch ex As Exception
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
    '    Catch ex As Exception
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
    '    Catch ex As Exception
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
            txtCompanyCode_ForgotPass.Text = ""


            lblInfo.Text = "Enter Registered EmaiID or Mobile Number to Recieve your Login Details."
            btnForgot_SubmitDetails.Visible = False
            rdbType.ClearSelection()
            ModalPopupExtender1.Show()
        Catch ex As Exception
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
                                GV.sendNewMail(OTP, " BOS Forget Password - OTP ", GV.parseString(Email))
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
                                GV.sendSMSThroughAPI(GV.parseString(Email), OTP)

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
                                GV.sendSMSThroughAPI(GV.parseString(Email), OTP)
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
                    str = GV.FL_AdminLogin.AddInVar("RegistrationId + ',' + AgentPassword", DBName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where    RegistrationId='" & GV.parseString(txtLoginId.Text.Trim) & "'")
                Else
                    str = GV.FL_AdminLogin.AddInVar("User_ID + ',' + User_password", DBName & ".dbo.CRM_Login_Details where   User_ID='" & GV.parseString(txtLoginId.Text.Trim) & "'")
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

                    GV.sendSMSThroughAPI(GV.parseString(txtEmail.Text.Trim), msg)
                    lblresult.Text = "Your Login Details Successfully Sent to Your Mobile Number"
                    lblresult.CssClass = "successlabels"
                End If
                btnproceed.Text = "Ok"
                ModalPopupExtender3.Show()
            End If
        Catch ex As Exception

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
        End Try
        Return finalString
    End Function

    Protected Sub lnkSignup_Click(sender As Object, e As EventArgs) Handles lnkSignup.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""



            Dim CompanyCode As String = GV.parseString(txtCompanyCode.Text.Trim)


            If CompanyCode.Trim = "" Then
                CompanyCode = "CMP1085"
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

        End Try
    End Sub






End Class

