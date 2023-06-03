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
Imports System.Device.Location


Public Class M_BOS_Loging
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

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



            'txtCompanyCode.Text = "CMP1196"
            'https://midigitalnewbanks.financial/

            Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
            Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")

            If strUrl.Trim.ToUpper = "https://midigitalnewbanks.financial/".Trim.ToUpper Then
                txtCompanyCode.Text = "CMP1196"
            ElseIf strUrl.Contains("midigitalnewbanks") Then
                txtCompanyCode.Text = "CMP1196"
            End If



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
                'lblError.Text = "Select Login As ..."
                'lblError.Visible = True
                'lblError.CssClass = "errorlabels"
                'Exit Sub
                lblError.Visible = False
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

                        If ChkrememberMe.Checked Then
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

                        If ChkrememberMe.Checked Then
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
                    If ChkrememberMe.Checked Then
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
                End If

                If Not IsPostBack Then

                    txtCompanyCode.Text = "CMP1045"
                    txtCompanyCode.Enabled = False
                    txtCompanyCode.CssClass = "form-control"





                End If
            End If

            lblError.Text = ""
            lblError.CssClass = ""



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
        'Try
        '    If Not IsPostBack Then
        '        'Dim v_API_event, v_data_id, v_data_created_at, v_data_remitter_full_name, v_data_remitter_account_number, v_data_remitter_account_ifsc, v_data_remitter_phone_number, v_data_unique_transaction_reference, v_data_payment_mode, v_data_amount, v_data_service_charge, v_data_gst_amount, v_data_service_charge_with_gst, v_data_narration, v_data_status, v_data_transaction_date, v_virtual_account_id, v_virtual_label, v_virtual_account_number, v_virtual_ifsc_number, v_API_Authorization As String
        '        'Dim v_AdminID, v_AgentID, v_AgentType, v_Transfer_Trans_ID As String

        '        'v_AdminID = ""
        '        'v_AgentID = ""
        '        'v_AgentType = ""
        '        'v_Transfer_Trans_ID = ""



        '        'v_API_event = ""
        '        'v_data_id = ""
        '        'v_data_created_at = ""
        '        'v_data_remitter_full_name = ""
        '        'v_data_remitter_account_number = ""
        '        'v_data_remitter_account_ifsc = ""
        '        'v_data_remitter_phone_number = ""
        '        'v_data_unique_transaction_reference = ""
        '        'v_data_payment_mode = ""
        '        'v_data_amount = ""
        '        'v_data_service_charge = ""
        '        'v_data_gst_amount = ""
        '        'v_data_service_charge_with_gst = ""
        '        'v_data_narration = ""
        '        'v_data_status = ""
        '        'v_data_transaction_date = ""
        '        'v_virtual_account_id = ""
        '        'v_virtual_label = ""
        '        'v_virtual_account_number = ""
        '        'v_virtual_ifsc_number = ""
        '        'v_API_Authorization = ""


        '        'Dim APIResult As String = "{  ""event"": ""TRANSACTION_CREDIT"",  ""data"": {    ""id"": ""cvatd4a828c446129bf99fdacc8419da"",    ""created_at"": ""2022-11-30T19:03:39.999227+05:30"",    ""remitter_full_name"": ""MANISHA VERMA"",    ""remitter_account_number"": ""XXXXXX8158"",    ""remitter_account_ifsc"": ""BARB0DBROHI"",    ""remitter_phone_number"": ""9212345320"",    ""unique_transaction_reference"": ""233464047245"",    ""payment_mode"": ""UPI"",    ""amount"": 5.0,    ""service_charge"": 2.0,    ""gst_amount"": 0.36,    ""service_charge_with_gst"": 2.36,    ""narration"": ""NA"",    ""status"": ""unsettled"",    ""transaction_date"": ""2022-11-30T19:03:38+05:30"",    ""virtual_account"": {      ""id"": ""cva25105fe07479a8781259d9ff570a9"",      ""label"": ""Retailer"",      ""virtual_account_number"": ""111222CMP1045ZRTE554"",      ""virtual_ifsc_number"": ""YESB0CMSNOC""    },    ""Authorization"": ""575a01e05a670fcea9d11c66201cbf7796d8792427df555a78e9c1dcd644d44663e50deb08cf3aaaffbbafd2b81668cd93695cd0cf29ed63fcc729f56264a8ea""  }}"
        '        'Dim json1 As JObject = JObject.Parse(APIResult)


        '        'Dim aaEvent As JToken = json1.SelectToken("event")
        '        'v_API_event = aaEvent.ToString

        '        'Dim aaRESPONSE As JToken = json1.SelectToken("data")
        '        'Dim jj As String = ""
        '        'Dim data12 As List(Of JToken) = aaRESPONSE.Children().ToList


        '        'For Each item As JProperty In data12
        '        '    If item.Name.ToString.Trim.ToUpper = "virtual_account".ToString.Trim.ToUpper Then
        '        '        Dim data121 As List(Of JToken) = item.Children().ToList
        '        '        For Each item23 As JObject In data121
        '        '            For Each p123 In item23
        '        '                If p123.Key.ToString.Trim.ToUpper = "id".ToString.Trim.ToUpper Then
        '        '                    v_virtual_account_id = p123.Value.ToString
        '        '                ElseIf p123.Key.ToString.Trim.ToUpper = "Label".ToString.Trim.ToUpper Then
        '        '                    v_virtual_label = p123.Value.ToString
        '        '                ElseIf p123.Key.ToString.Trim.ToUpper = "virtual_account_number".ToString.Trim.ToUpper Then
        '        '                    v_virtual_account_number = p123.Value.ToString
        '        '                    Dim rwStr As String = v_virtual_account_number.Substring(6, v_virtual_account_number.Length - 6)
        '        '                    Dim rwData() As String = rwStr.Split("Z")
        '        '                    v_AdminID = rwData(0)

        '        '                    If rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "M" Then
        '        '                        v_AgentType = "Master Distributor"
        '        '                        v_AgentID = "MD-" & rwData(1).ToString.Substring(2, rwData(1).Length - 2).Trim.ToUpper
        '        '                    ElseIf rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "D" Then
        '        '                        v_AgentType = "Distributor"
        '        '                        v_AgentID = "DIS-" & rwData(1).ToString.Substring(3, rwData(1).Length - 3).Trim.ToUpper
        '        '                    ElseIf rwData(1).ToString.Substring(0, 1).Trim.ToUpper = "R" Then
        '        '                        v_AgentType = "Retailer"
        '        '                        v_AgentID = "RTE-" & rwData(1).ToString.Substring(3, rwData(1).Length - 3).Trim.ToUpper
        '        '                    End If




        '        '                ElseIf p123.Key.ToString.Trim.ToUpper = "virtual_ifsc_number".ToString.Trim.ToUpper Then
        '        '                    v_virtual_ifsc_number = p123.Value.ToString
        '        '                End If
        '        '            Next

        '        '        Next
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "id".ToString.Trim.ToUpper Then
        '        '        v_data_id = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "created_at".ToString.Trim.ToUpper Then
        '        '        v_data_created_at = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "remitter_full_name".ToString.Trim.ToUpper Then
        '        '        v_data_remitter_full_name = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "remitter_account_number".ToString.Trim.ToUpper Then
        '        '        v_data_remitter_account_number = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "remitter_account_ifsc".ToString.Trim.ToUpper Then
        '        '        v_data_remitter_account_ifsc = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "remitter_phone_number".ToString.Trim.ToUpper Then
        '        '        v_data_remitter_phone_number = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "unique_transaction_reference".ToString.Trim.ToUpper Then
        '        '        v_data_unique_transaction_reference = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "payment_mode".ToString.Trim.ToUpper Then
        '        '        v_data_payment_mode = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "amount".ToString.Trim.ToUpper Then
        '        '        v_data_amount = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "service_charge".ToString.Trim.ToUpper Then
        '        '        v_data_service_charge = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "gst_amount".ToString.Trim.ToUpper Then
        '        '        v_data_gst_amount = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "service_charge_with_gst".ToString.Trim.ToUpper Then
        '        '        v_data_service_charge_with_gst = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "narration".ToString.Trim.ToUpper Then
        '        '        v_data_narration = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
        '        '        v_data_status = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "transaction_date".ToString.Trim.ToUpper Then
        '        '        v_data_transaction_date = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "Authorization".ToString.Trim.ToUpper Then
        '        '        v_API_Authorization = item.Value.ToString
        '        '    End If
        '        'Next
        '        'v_API_event = v_API_event
        '        'v_data_id = v_data_id
        '        'v_data_created_at = v_data_created_at
        '        'v_data_remitter_full_name = v_data_remitter_full_name
        '        'v_data_remitter_account_number = v_data_remitter_account_number
        '        'v_data_remitter_account_ifsc = v_data_remitter_account_ifsc
        '        'v_data_remitter_phone_number = v_data_remitter_phone_number
        '        'v_data_unique_transaction_reference = v_data_unique_transaction_reference
        '        'v_data_payment_mode = v_data_payment_mode
        '        'v_data_amount = v_data_amount
        '        'v_data_service_charge = v_data_service_charge
        '        'v_data_gst_amount = v_data_gst_amount
        '        'v_data_service_charge_with_gst = v_data_service_charge_with_gst
        '        'v_data_narration = v_data_narration
        '        'v_data_status = v_data_status
        '        'v_data_transaction_date = v_data_transaction_date
        '        'v_virtual_account_id = v_virtual_account_id
        '        'v_virtual_label = v_virtual_label
        '        'v_virtual_account_number = v_virtual_account_number
        '        'v_virtual_ifsc_number = v_virtual_ifsc_number
        '        'v_API_Authorization = v_API_Authorization
        '        'v_AdminID = v_AdminID
        '        'v_AgentID = v_AgentID
        '        'v_AgentType = v_AgentType
        '        'v_API_Authorization = v_API_Authorization
        '        'Exit Sub



        '        'Dim client As New WebClient()
        '        'Dim API_URL As String = ""
        '        'API_URL = "https://mapi.indiamart.com/wservce/crm/crmListing/v2/?glusr_crm_key=mR21Fb1t5X7BTvet4naN7liGoFfMlDFi&start_time=10-nov-2022&end_time=16-nov-2022"
        '        'ServicePointManager.Expect100Continue = True
        '        'ServicePointManager.DefaultConnectionLimit = 9999
        '        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        '        'Dim data As Stream = client.OpenRead(API_URL)
        '        'Dim reader As New StreamReader(data)
        '        'Dim APIResult As String = reader.ReadToEnd()
        '        'data.Close()
        '        'reader.Close()


        '        ''Dim jsonString As String = "" ' "{""CODE"": 200,""STATUS"":""SUCCESS"",""MESSAGE"":"""",""TOTAL_RECORDS"": 2,""RESPONSE"":[{""UNIQUE_QUERY_ID"":""551297957"",""QUERY_TYPE"":""B"",""QUERY_TIME"":""2022-11-11 13:04:14"",""SENDER_NAME"":""Tushar Rajput"",""SENDER_MOBILE"":""+91-8445395934"",""SENDER_EMAIL"":"""",""SUBJECT"":""Requirement for MLM Website Development Services"",""SENDER_COMPANY"":"""",""SENDER_ADDRESS"":""Agra, Uttar Pradesh"",""SENDER_CITY"":""Agra"",""SENDER_STATE"":""Uttar Pradesh"",""SENDER_COUNTRY_ISO"":""IN"",""SENDER_MOBILE_ALT"":"""",""SENDER_PHONE"":"""",""SENDER_PHONE_ALT"":"""",""SENDER_EMAIL_ALT"":"""",""QUERY_PRODUCT_NAME"":""MLM Website Development Services"",""QUERY_MESSAGE"":""I am looking for service provider for MLM Website Development Services.Kindly send me other details.Service Location : AgraProbable Requirement Type Business UsePreferred Location : Service Providers from Local Area will be Preferred<br>"",""CALL_DURATION"":"""",""RECEIVER_MOBILE"":""""},{""UNIQUE_QUERY_ID"":""551297957"",""QUERY_TYPE"":""B"",""QUERY_TIME"":""2022-11-11 13:04:14"",""SENDER_NAME"":""Tushar Rajput"",""SENDER_MOBILE"":""+91-8445395934"",""SENDER_EMAIL"":"""",""SUBJECT"":""Requirement for MLM Website Development Services"",""SENDER_COMPANY"":"""",""SENDER_ADDRESS"":""Agra, Uttar Pradesh"",""SENDER_CITY"":""Agra"",""SENDER_STATE"":""Uttar Pradesh"",""SENDER_COUNTRY_ISO"":""IN"",""SENDER_MOBILE_ALT"":"""",""SENDER_PHONE"":"""",""SENDER_PHONE_ALT"":"""",""SENDER_EMAIL_ALT"":"""",""QUERY_PRODUCT_NAME"":""MLM Website Development Services"",""QUERY_MESSAGE"":""I am looking for service provider for MLM Website Development Services.Kindly send me other details.Service Location : AgraProbable Requirement Type  Business UsePreferred Location : Service Providers from Local Area will be Preferred<br>"",""CALL_DURATION"":"""",""RECEIVER_MOBILE"":""""} ]  }"
        '        ''jsonString = New System.Net.WebClient().DownloadString("https://mapi.indiamart.com/wservce/crm/crmListing/v2/?glusr_crm_key=mR21Fb1t5X7BTvet4naN7liGoFfMlDFi&start_time=10-nov-2022&end_time=16-nov-2022")


        '        'Dim json1 As JObject = JObject.Parse(APIResult)

        '        'Dim aaCODE As JToken = json1.SelectToken("CODE")
        '        'Dim aaSTATUS As JToken = json1.SelectToken("STATUS")
        '        'Dim aaMESSAGE As JToken = json1.SelectToken("MESSAGE")
        '        'Dim aaTOTAL_RECORDS As JToken = json1.SelectToken("TOTAL_RECORDS")

        '        'Dim aaRESPONSE As JToken = json1.SelectToken("RESPONSE")

        '        'Dim jj As String = ""
        '        'Dim data12 As List(Of JToken) = aaRESPONSE.Children().ToList
        '        'For Each msg As JObject In data12
        '        '    For Each p In msg
        '        '        jj = jj & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString

        '        '        If p.Key.ToString.Trim.ToUpper = "UNIQUE_QUERY_ID".ToString.Trim.ToUpper Then
        '        '            jj = jj & " >>  " & p.Value.ToString
        '        '        ElseIf p.Key.ToString.Trim.ToUpper = "QUERY_TYPE".ToString.Trim.ToUpper Then
        '        '            jj = jj & " >>  " & p.Value.ToString
        '        '        ElseIf p.Key.ToString.Trim.ToUpper = "SENDER_NAME".ToString.Trim.ToUpper Then
        '        '            jj = jj & " >>  " & p.Value.ToString
        '        '        ElseIf p.Key.ToString.Trim.ToUpper = "SENDER_MOBILE".ToString.Trim.ToUpper Then
        '        '            jj = jj & " >>  " & p.Value.ToString
        '        '        End If

        '        '        'jj = msg("date").ToString & " " & msg("amount").ToString & " " & msg("txnType").ToString & " " & msg("narration").ToString
        '        '        'Dim dr1 As DataRow = dt.NewRow()
        '        '        'dr1(0) = i + 1
        '        '        'dr1(1) = msg("date")
        '        '        'dr1(2) = msg("txnType")
        '        '        'dr1(3) = msg("amount")
        '        '        'dr1(4) = msg("narration")
        '        '        'dt.Rows.Add(dr1)

        '        '    Next
        '        'Next



        '        ''For Each item As JProperty In data12
        '        ''    If item.Name.ToString.Trim.ToUpper = "UNIQUE_QUERY_ID".ToString.Trim.ToUpper Then
        '        ''        jj = jj & " >>  " & item.Value.ToString
        '        ''    ElseIf item.Name.ToString.Trim.ToUpper = "QUERY_TYPE".ToString.Trim.ToUpper Then
        '        ''        jj = jj & " >>  " & item.Value.ToString
        '        ''    ElseIf item.Name.ToString.Trim.ToUpper = "SENDER_NAME".ToString.Trim.ToUpper Then
        '        ''        jj = jj & " >>  " & item.Value.ToString
        '        ''    ElseIf item.Name.ToString.Trim.ToUpper = "SENDER_MOBILE".ToString.Trim.ToUpper Then
        '        ''        jj = jj & " >>  " & item.Value.ToString
        '        ''    End If
        '        ''Next


        '        'jj = jj



        '        'Exit Sub



        '        '28.7192,77.1505
        '        '28.6035,77.0894

        '        'Dim pin1 As New GeoCoordinate(28.7192, 77.1505)
        '        'Dim pin2 As New GeoCoordinate(28.6035, 77.0894)
        '        'Dim distanceBetween As Double = pin1.GetDistanceTo(pin2)  ' result gives in meters
        '        'distanceBetween = Math.Round((distanceBetween / 1000), 2)  ' divide by 1000 to get result in kilometer
        '        'distanceBetween = distanceBetween
        '        'Exit Sub


        '        '"{""status"":""success"",""country"":""India"",""countryCode"":""IN"",""region"":""DL"",""regionName"":""National Capital Territory of Delhi"",""city"":""Delhi"",""zip"":""110003"",""lat"":28.6542,""lon"":77.2373,""timezone"":""Asia/Kolkata"",""isp"":""Anjani Broadband Solutions Pvt Ltd"",""org"":""Bright Net Technologies Pvt Ltd"",""as"":""AS58965 ANJANI BROADBAND SOLUTIONS PVT.LTD."",""query"":""103.77.42.184""}"
        '        'Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
        '        'Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")


        '        'lblError.Text = strUrl.Trim
        '        'lblError.Visible = True
        '        'lblError.CssClass = "successlabels"

        '        'Exit Sub


        '        'Dim pin1 As New GeoCoordinate(28.6998107, 77.098854)
        '        'Dim pin2 As New GeoCoordinate(29.043958, 77.6351452)
        '        ''28.7130226,77.1170072
        '        ''28.7041019, 77.127736
        '        ''28.6985308,77.133315
        '        ''28.694917,77.1373062
        '        ''28.6950299,77.1518115
        '        ''28.6924136,77.1540861
        '        ''28.6800838,77.1608238
        '        ''28.7017554,77.2000367 summercool



        '        ''https://maps.googleapis.com/maps/api/distancematrix/xml?units=imperial&origins=28.6998107,77.098854&destinations=29.043958,77.6351452&key=AIzaSyCqNE77AmnSEywlJ63QTJsboi7E7wFPmMQ
        '        ''https://www.codeproject.com/Articles/1169032/Calculating-distance-using-Google-Maps-in-asp-net
        '        'Dim distanceBetween As Double = pin1.GetDistanceTo(pin2)  ' result gives in meters
        '        'distanceBetween = Math.Round((distanceBetween / 1000), 2)  ' divide by 1000 to get result in kilometer
        '        'distanceBetween = distanceBetween

        '        ''https://api.bulksmshospet.com/api/text.php?api=82dc70c47a1ac749d4f8be8cb162cd97&sender=919212345320&number=919212345320&message=this%20is%20testing%20message%20Again%20123

        '        'Dim client2 As New RestSharp.RestClient("https://india-pincode-with-latitude-and-longitude.p.rapidapi.com/api/v1/district?page=1&state=Haryana&limit=50")
        '        'Dim Request1 As New RestSharp.RestRequest(RestSharp.Method.GET)
        '        'ServicePointManager.Expect100Continue = True
        '        'ServicePointManager.DefaultConnectionLimit = 9999
        '        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        '        'Request1.AddHeader("Accept", "application/json")
        '        ''Request1.AddHeader("Content-Type", "application/json")
        '        'Request1.AddHeader("X-RapidAPI-Key", "63628ae171msh5c0abeca2467523p169a76jsn50878346a4f2")
        '        'Request1.AddHeader("X-RapidAPI-Host", "india-pincode-with-latitude-and-longitude.p.rapidapi.com")
        '        'Dim Response1 As RestSharp.IRestResponse = client2.Execute(Request1)
        '        'Dim asd As String = Response1.Content
        '        'asd = asd


        '        'Dim client As New RestClient(Urls)
        '        'Dim request As New RestSharp.RestRequest(RestSharp.Method.POST)
        '        'request.AddHeader("Accept", "application/json")
        '        'request.AddHeader("Token", dd)
        '        'request.AddHeader("Content-Type", "application/json")
        '        ''request.AddHeader("Authorisedkey", "ZTE3MTYyZDI3YzNjY2Y2ZjE5N2M0NGRkNjg4YzAzYmE=")

        '        'request.AddParameter("application/json", Parameter, RestSharp.ParameterType.RequestBody)
        '        'Dim response As IRestResponse = client.Execute(request)
        '        'Str = response.Content
        '        'Str = Str.Trim


        '        ''////////
        '        'Dim strBuild As String = ""
        '        'Dim str As String = "{ ""status "":true, ""response_code "":1, ""info "":{ ""operator "": ""Airtel "", ""circle "": ""Delhi NCR ""}, ""message "": ""Successful ""}"
        '        'Dim json1 As JObject = JObject.Parse(str)

        '        'Dim data_ As List(Of JToken) = json1.Children().ToList
        '        'For Each item As JProperty In data_

        '        '    item.CreateReader()
        '        '    If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "info".ToString.Trim.ToUpper Then
        '        '        Dim data1 As List(Of JToken) = item.Children().ToList
        '        '        For Each msg As JObject In data1
        '        '            '/// Dynamic Name and get its value
        '        '            For Each p In msg
        '        '                strBuild = strBuild & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString
        '        '                If p.Key.ToString.Trim.ToUpper = "operator".ToUpper Then
        '        '                    strBuild = p.Value.ToString.Trim
        '        '                ElseIf p.Key.ToString.Trim.ToUpper = "circle".ToUpper Then
        '        '                    strBuild = p.Value.ToString.Trim
        '        '                End If
        '        '            Next
        '        '        Next
        '        '    End If
        '        'Next
        '        ''///////////////

        '        'Dim strBuild As String = ""
        '        'Dim dt As New DataTable
        '        'Dim dc1 As DataColumn = New DataColumn("Type")
        '        'Dim dc2 As DataColumn = New DataColumn("Rs")
        '        'Dim dc3 As DataColumn = New DataColumn("Desc")
        '        'Dim dc4 As DataColumn = New DataColumn("validity")
        '        'Dim dc5 As DataColumn = New DataColumn("last_update")

        '        'dt.Columns.Add(dc1)
        '        'dt.Columns.Add(dc2)
        '        'dt.Columns.Add(dc3)
        '        'dt.Columns.Add(dc4)
        '        'dt.Columns.Add(dc5)

        '        'Dim planStr As String = "{ ""status "":true, ""response_code "":1, ""info "":{ ""TOPUP "":[{ ""rs "": ""10 "", ""desc "": ""Talktime of Rs. 7.47 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""20 "", ""desc "": ""Talktime of Rs. 14.95 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""100 "", ""desc "": ""Talktime of Rs. 81.75 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""500 "", ""desc "": ""Talktime of Rs 423.73 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1000 "", ""desc "": ""Talktime of Rs 847.46 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5000 "", ""desc "": ""Talktime of Rs 4237.29 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""}], ""3G\/4G "":[{ ""rs "": ""19 "", ""desc "": ""Enjoy 1GB data, valid for 1 day "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""58 "", ""desc "": ""3 GB | Validity same as your existing bundle \/ smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""98 "", ""desc "": ""Wynk Premium Data Pack5 GB Data | 30 days of Wynk Premium subscription "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""108 "", ""desc "": ""PVME Data Pack6 GB Data | 28 days of Prime Video Mobile Edition subscription "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""118 "", ""desc "": ""12 GB | Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""148 "", ""desc "": ""Xstream Mobile Data Pack 15GB Data |28 days access to any 1 channel (SonyLiv, LionsgatePlay, ErosNow, HoiChoi, ManoramaMAX) on Airtel Xstream App at no extra cost, Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""301 "", ""desc "": ""50 GB , 1year of Wynk Music Premium subscription, Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""}], ""RATE CUTTER "":[{ ""rs "": ""99 "", ""desc "": ""Rs 99 Talktime 200 MB Data Local\/STD\/LL @ 2.5\/sec Tariff Calls 28 Days Validity "", ""validity "": ""28 days "", ""last_update "": ""01-01-1970 ""}], ""Romaing "":[{ ""rs "": ""18 "", ""desc "": ""Enjoy ISD calling at discounted rates for 28 days. For country wise tariff visit www.airtel.in "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""496 "", ""desc "": ""Unlimited incoming, 500 MB, 100 min local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""648 "", ""desc "": ""100 mins incoming, 500 MB, 100 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""649 "", ""desc "": ""Unlimited incoming, 500 MB, 100 min local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""755 "", ""desc "": ""1 GB data, valid for 5 days.- Covered Countries: UAE, Saudi Arabia, Malaysia, USA, Oman, Qatar, UK, Kuwait, Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""5 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1499 "", ""desc "": ""Unlimited incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""2499 "", ""desc "": ""Unlimited incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3598 "", ""desc "": ""250mins incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3599 "", ""desc "": ""Unlimited incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3997 "", ""desc "": ""Unlimited incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3998 "", ""desc "": ""500 mins incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""4999 "", ""desc "": ""Unlimited incoming, 1 GB\/day, 500 mins of local\/India calls,100 sms - Covered Countries :UAE, Saudi Arabia, Malaysia, USA, Oman, Qatar, UK, Kuwait, Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5997 "", ""desc "": ""Details: Unlimited incoming, 1800 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5998 "", ""desc "": ""Details: Unlimited incoming, 1800 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, Friance, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5999 "", ""desc "": ""Details: 300 mins Incoming, 300 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""}], ""COMBO "":[{ ""rs "": ""128 "", ""desc "": ""Enjoy Local & STD calls 2.5p\/sec National Video Calls 5p\/sec DATA 50p\/MB; SMS Rs.1 Local Rs.1.5 STD Rs.5 ISD "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""155 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data, 300 National SMS for 24 days.  "", ""validity "": ""24 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""179 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 2 GB data, 300 National SMS for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""209 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, daily 1GB data and 100 SMS  "", ""validity "": ""21 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""239 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data per day, 100 National SMS\/day for 24 days.  "", ""validity "": ""24 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""265 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data per day, 100 National SMS\/day for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""296 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 25GB Data and 100 SMS\/day  "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""299 "", ""desc "": ""Enjoy 1.5GB\/day data, unlimited Local, STD & Roaming calls on any network and 100 SMS\/day.  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""319 "", ""desc "": ""Enjoy unlimited Local STD & Roaming calls on any network 2GB\/day Data and 100 SMS\/day for 1 Month  "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""359 "", ""desc "": ""Enjoy Unlimited Local, STD & Roaming calls Local STD & Roaming calls on any network daily 2GB data and 100 SMS. Pack valid for 28 days. Also get Amazon Prime Video Mobile Edition & 1 Airtel Xstream channel for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""399 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 2.5GB\/day data and 100 SMS.  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""455 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 6GB data, 900 National SMS for 84 days  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""479 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1.5 GB data per day, 100 National SMS\/day for 56 days  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""549 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, daily 2GB data and 100 SMS  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""599 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 3 GB data per day, 100 National SMS\/day for 28 days and Disney+ Hostar Mobile for 1 year  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""666 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1.5 GB data per day, 100 National SMS\/day for 77 days  "", ""validity "": ""77 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""699 "", ""desc "": ""Enjoy Unlimited Local, STD & Roaming calls Local STD & Roaming calls on any network daily 3GB data and 100 SMS  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""719 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 1.5GB\/day data and 100 SMS  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""799 "", ""desc "": ""100mins of Incoming\/Outgoing (India+Local) calls. Covered Countries : UAE Saudi Arabia Malaysia USA Oman Qatar UK Kuwait Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""839 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 2GB\/day data and 100 SMS  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""997 "", ""desc "": ""100 mins incoming, 500 MB, 100 mins local\/India calls, 100 sms - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1199 "", ""desc "": ""Details: 1 GB Data, 100mins of Incoming\/Outgoing (India+Local) calls. Covered Countries : UAE Saudi Arabia Malaysia USA Oman Qatar UK Kuwait Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1799 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 24GB data, 3600 National SMS for 365 days  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""2999 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network within India, daily 2GB data & 100 SMS  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3359 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, daily 2.5GB & 100 SMS. Pack Valid for 365 days  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3995 "", ""desc "": ""250 mins incoming, 3 GB, 250 mins local\/India calls, 100 SMS - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""6999 "", ""desc "": ""500 mins incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8997 "", ""desc "": ""Details: Unlimited incoming, 3600 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8998 "", ""desc "": ""Details: Unlimited incoming, 3600 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, France, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8999 "", ""desc "": ""Details: 600 mins Incoming, 600 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14997 "", ""desc "": ""Details: Unlimited incoming, 7200 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14998 "", ""desc "": ""Details: Unlimited incoming, 7200 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, France, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14999 "", ""desc "": ""Details: 1200 mins Incoming, 1200 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""}]}, ""message "": ""Fetch Successful ""}"
        '        'Dim json2 As JObject = JObject.Parse(planStr)
        '        'Dim data1_ As List(Of JToken) = json2.Children().ToList
        '        'For Each item As JProperty In data1_
        '        '    item.CreateReader()
        '        '    If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "info".ToString.Trim.ToUpper Then
        '        '        Dim data1 As List(Of JToken) = item.Children().ToList
        '        '        Dim data2_ As List(Of JToken) = data1.Children().ToList
        '        '        For Each msg1 As JProperty In data2_
        '        '            '/// Dynamic Name and get its value
        '        '            Dim vType As String = ""
        '        '            Dim vRs As String = ""
        '        '            Dim vDes As String = ""
        '        '            Dim vvalidity As String = ""
        '        '            Dim vlast_update As String = ""

        '        '            vType = msg1.Name
        '        '            Dim data3_ As List(Of JToken) = msg1.Children().ToList
        '        '            For Each pnk As JArray In data3_
        '        '                For k As Integer = 0 To pnk.Count - 1
        '        '                    strBuild = pnk(k).ToString
        '        '                    Dim json4 As JObject = JObject.Parse(strBuild)
        '        '                    Dim datap_ As List(Of JToken) = json4.Children().ToList
        '        '                    For Each item2 As JProperty In datap_
        '        '                        If item2.Name.ToString.Trim.ToUpper = "Rs".ToString.Trim.ToUpper Then
        '        '                            vRs = item2.Value
        '        '                        ElseIf item2.Name.ToString.Trim.ToUpper = "Desc".ToString.Trim.ToUpper Then
        '        '                            vDes = item2.Value
        '        '                        ElseIf item2.Name.ToString.Trim.ToUpper = "validity".ToString.Trim.ToUpper Then
        '        '                            vvalidity = item2.Value
        '        '                        ElseIf item2.Name.ToString.Trim.ToUpper = "last_update".ToString.Trim.ToUpper Then
        '        '                            vlast_update = item2.Value
        '        '                        End If
        '        '                    Next

        '        '                    If Not vRs.Trim = "" Then
        '        '                        Dim dr1 As DataRow = dt.NewRow()
        '        '                        dr1(0) = vType
        '        '                        dr1(1) = vRs
        '        '                        dr1(2) = vDes
        '        '                        dr1(3) = vvalidity
        '        '                        dr1(4) = vlast_update
        '        '                        dt.Rows.Add(dr1)
        '        '                    End If


        '        '                Next

        '        '            Next

        '        '        Next
        '        '    End If
        '        'Next

        '        'grdAddRecepient.DataSource = dt.DefaultView
        '        'grdAddRecepient.DataBind()






        '        'Dim strBuild As String = ""
        '        'Dim planStr As String = "{ ""status "":true, ""response_code "":1, ""info "":{ ""TOPUP "":[{ ""rs "": ""10 "", ""desc "": ""Talktime of Rs. 7.47 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""20 "", ""desc "": ""Talktime of Rs. 14.95 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""100 "", ""desc "": ""Talktime of Rs. 81.75 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""500 "", ""desc "": ""Talktime of Rs 423.73 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1000 "", ""desc "": ""Talktime of Rs 847.46 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5000 "", ""desc "": ""Talktime of Rs 4237.29 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""}], ""3G\/4G "":[{ ""rs "": ""19 "", ""desc "": ""Enjoy 1GB data, valid for 1 day "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""58 "", ""desc "": ""3 GB | Validity same as your existing bundle \/ smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""98 "", ""desc "": ""Wynk Premium Data Pack5 GB Data | 30 days of Wynk Premium subscription "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""108 "", ""desc "": ""PVME Data Pack6 GB Data | 28 days of Prime Video Mobile Edition subscription "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""118 "", ""desc "": ""12 GB | Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""148 "", ""desc "": ""Xstream Mobile Data Pack 15GB Data |28 days access to any 1 channel (SonyLiv, LionsgatePlay, ErosNow, HoiChoi, ManoramaMAX) on Airtel Xstream App at no extra cost, Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""301 "", ""desc "": ""50 GB , 1year of Wynk Music Premium subscription, Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""}], ""RATE CUTTER "":[{ ""rs "": ""99 "", ""desc "": ""Rs 99 Talktime 200 MB Data Local\/STD\/LL @ 2.5\/sec Tariff Calls 28 Days Validity "", ""validity "": ""28 days "", ""last_update "": ""01-01-1970 ""}], ""Romaing "":[{ ""rs "": ""18 "", ""desc "": ""Enjoy ISD calling at discounted rates for 28 days. For country wise tariff visit www.airtel.in "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""496 "", ""desc "": ""Unlimited incoming, 500 MB, 100 min local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""648 "", ""desc "": ""100 mins incoming, 500 MB, 100 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""649 "", ""desc "": ""Unlimited incoming, 500 MB, 100 min local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""755 "", ""desc "": ""1 GB data, valid for 5 days.- Covered Countries: UAE, Saudi Arabia, Malaysia, USA, Oman, Qatar, UK, Kuwait, Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""5 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1499 "", ""desc "": ""Unlimited incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""2499 "", ""desc "": ""Unlimited incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3598 "", ""desc "": ""250mins incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3599 "", ""desc "": ""Unlimited incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3997 "", ""desc "": ""Unlimited incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3998 "", ""desc "": ""500 mins incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""4999 "", ""desc "": ""Unlimited incoming, 1 GB\/day, 500 mins of local\/India calls,100 sms - Covered Countries :UAE, Saudi Arabia, Malaysia, USA, Oman, Qatar, UK, Kuwait, Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5997 "", ""desc "": ""Details: Unlimited incoming, 1800 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5998 "", ""desc "": ""Details: Unlimited incoming, 1800 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, Friance, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5999 "", ""desc "": ""Details: 300 mins Incoming, 300 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""}], ""COMBO "":[{ ""rs "": ""128 "", ""desc "": ""Enjoy Local & STD calls 2.5p\/sec National Video Calls 5p\/sec DATA 50p\/MB; SMS Rs.1 Local Rs.1.5 STD Rs.5 ISD "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""155 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data, 300 National SMS for 24 days.  "", ""validity "": ""24 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""179 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 2 GB data, 300 National SMS for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""209 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, daily 1GB data and 100 SMS  "", ""validity "": ""21 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""239 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data per day, 100 National SMS\/day for 24 days.  "", ""validity "": ""24 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""265 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data per day, 100 National SMS\/day for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""296 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 25GB Data and 100 SMS\/day  "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""299 "", ""desc "": ""Enjoy 1.5GB\/day data, unlimited Local, STD & Roaming calls on any network and 100 SMS\/day.  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""319 "", ""desc "": ""Enjoy unlimited Local STD & Roaming calls on any network 2GB\/day Data and 100 SMS\/day for 1 Month  "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""359 "", ""desc "": ""Enjoy Unlimited Local, STD & Roaming calls Local STD & Roaming calls on any network daily 2GB data and 100 SMS. Pack valid for 28 days. Also get Amazon Prime Video Mobile Edition & 1 Airtel Xstream channel for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""399 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 2.5GB\/day data and 100 SMS.  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""455 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 6GB data, 900 National SMS for 84 days  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""479 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1.5 GB data per day, 100 National SMS\/day for 56 days  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""549 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, daily 2GB data and 100 SMS  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""599 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 3 GB data per day, 100 National SMS\/day for 28 days and Disney+ Hostar Mobile for 1 year  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""666 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1.5 GB data per day, 100 National SMS\/day for 77 days  "", ""validity "": ""77 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""699 "", ""desc "": ""Enjoy Unlimited Local, STD & Roaming calls Local STD & Roaming calls on any network daily 3GB data and 100 SMS  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""719 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 1.5GB\/day data and 100 SMS  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""799 "", ""desc "": ""100mins of Incoming\/Outgoing (India+Local) calls. Covered Countries : UAE Saudi Arabia Malaysia USA Oman Qatar UK Kuwait Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""839 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 2GB\/day data and 100 SMS  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""997 "", ""desc "": ""100 mins incoming, 500 MB, 100 mins local\/India calls, 100 sms - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1199 "", ""desc "": ""Details: 1 GB Data, 100mins of Incoming\/Outgoing (India+Local) calls. Covered Countries : UAE Saudi Arabia Malaysia USA Oman Qatar UK Kuwait Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1799 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 24GB data, 3600 National SMS for 365 days  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""2999 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network within India, daily 2GB data & 100 SMS  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3359 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, daily 2.5GB & 100 SMS. Pack Valid for 365 days  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3995 "", ""desc "": ""250 mins incoming, 3 GB, 250 mins local\/India calls, 100 SMS - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""6999 "", ""desc "": ""500 mins incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8997 "", ""desc "": ""Details: Unlimited incoming, 3600 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8998 "", ""desc "": ""Details: Unlimited incoming, 3600 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, France, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8999 "", ""desc "": ""Details: 600 mins Incoming, 600 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14997 "", ""desc "": ""Details: Unlimited incoming, 7200 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14998 "", ""desc "": ""Details: Unlimited incoming, 7200 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, France, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14999 "", ""desc "": ""Details: 1200 mins Incoming, 1200 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""}]}, ""message "": ""Fetch Successful ""}"
        '        'Dim json2 As JObject = JObject.Parse(planStr)
        '        'Dim data1_ As List(Of JToken) = json2.Children().ToList
        '        'For Each item As JProperty In data1_
        '        '    item.CreateReader()
        '        '    If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
        '        '        strBuild = item.Value.ToString
        '        '    ElseIf item.Name.ToString.Trim.ToUpper = "info".ToString.Trim.ToUpper Then
        '        '        Dim data1 As List(Of JToken) = item.Children().ToList
        '        '        Dim data2_ As List(Of JToken) = data1.Children().ToList
        '        '        For Each msg1 As JProperty In data2_
        '        '            '/// Dynamic Name and get its value
        '        '            Dim vType As String = ""
        '        '            Dim vRs As String = ""
        '        '            Dim vDes As String = ""
        '        '            Dim vvalidity As String = ""
        '        '            Dim vlast_update As String = ""

        '        '            vType = msg1.Name
        '        '            Dim data3_ As List(Of JToken) = msg1.Children().ToList
        '        '            For Each pnk As JArray In data3_
        '        '                For k As Integer = 0 To pnk.Count - 1
        '        '                    strBuild = pnk(k).ToString
        '        '                    Dim json4 As JObject = JObject.Parse(strBuild)
        '        '                    Dim datap_ As List(Of JToken) = json4.Children().ToList
        '        '                    For Each item2 As JProperty In datap_
        '        '                        If item2.Name.ToString.Trim.ToUpper = "Rs".ToString.Trim.ToUpper Then
        '        '                            vRs = item2.Value
        '        '                        ElseIf item2.Name.ToString.Trim.ToUpper = "Desc".ToString.Trim.ToUpper Then
        '        '                            vDes = item2.Value
        '        '                        ElseIf item2.Name.ToString.Trim.ToUpper = "validity".ToString.Trim.ToUpper Then
        '        '                            vvalidity = item2.Value
        '        '                        ElseIf item2.Name.ToString.Trim.ToUpper = "last_update".ToString.Trim.ToUpper Then
        '        '                            vlast_update = item2.Value
        '        '                        End If
        '        '                    Next

        '        '                Next
        '        '                'strBuild = strBuild & pnk(0).ToString & " = " & pnk(1).ToString
        '        '            Next
        '        '            'If msg.Key.ToString.Trim.ToUpper = "operator".ToUpper Then
        '        '            '    strBuild = p.Value.ToString.Trim
        '        '            'ElseIf p.Key.ToString.Trim.ToUpper = "circle".ToUpper Then
        '        '            '    strBuild = p.Value.ToString.Trim
        '        '            'End If
        '        '            'For Each p In msg

        '        '            'Next
        '        '        Next
        '        '    End If
        '        'Next


        '        'Dim client As New WebClient()
        '        'Dim API_URL As String = ""
        '        'API_URL = "https://api.postalpincode.in/pincode/110001"
        '        ''ServicePointManager.Expect100Continue = True
        '        ''ServicePointManager.DefaultConnectionLimit = 9999
        '        ''ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        '        'Dim data As Stream = client.OpenRead(API_URL)
        '        'Dim reader As New StreamReader(data)
        '        'Dim APIResult As String = reader.ReadToEnd()
        '        'data.Close()
        '        'reader.Close()
        '        'APIResult = APIResult.Remove(0, 1)
        '        'APIResult = APIResult.Remove(APIResult.Length - 1, 1)
        '        'Dim jj As String = ""
        '        'Dim json1 As JObject = JObject.Parse(APIResult)
        '        'Dim aa As JToken = json1.SelectToken("PostOffice")
        '        'Dim data12 As List(Of JToken) = aa.Children().ToList
        '        'For Each msg As JObject In data12
        '        '    jj = jj & " >>  " & msg("Pincode").ToString & " , " & msg("Name").ToString & " , " & msg("District").ToString & " , " & msg("State").ToString & " , " & msg("Country").ToString
        '        'Next




        '        'Dim result As Boolean = GV.send_WhatsApp_Msg_API("9212345320", "Hello this is testting message")
        '        'result = result
        '        'result = GV.send_WhatsApp_Msg_API("8181898902", "Hello this is testting message")
        '        'result = result

        '        'Dim jj As String = Chr(34) + "Chr(34)SatishChr(34)" + Chr(34)
        '        'Dim ss As String = "{""status" & Chr(34) & ":true,""ackno"":13311810,""datetime"":""7\/20\/2022 5:15:33 PM"",""balanceamount"":""14412.64"",""bankrrn"":""220117034388"",""bankiin"":""508505"",""message"":""Mini statement has been generated successfully."",""errorcode"":""0"",""ministatement"":[{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/93755167""},{""date"":""20\/07"",""amount"":100,""txnType"":""D"",""narration"":""FIG\/F\/93597279""},{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/93033302""},{""date"":""20\/07"",""amount"":100,""txnType"":""D"",""narration"":""FIG\/F\/92998016""},{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/92965805""},{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/92957139""}],""ministatementlist"":[],""response_code"":1,""last_aadhar"":""9694"",""name"":""JYOTI"",""clientrefno"":""29469""}"
        '        ''ss = "{'status':true,'ackno':13311810,'datetime':'7\/20\/2022 5:15:33 PM','balanceamount':'14412.64','bankrrn':'220117034388','bankiin':'508505','message':'Mini statement has been generated successfully.','errorcode':'0','ministatement':[{'date':'20\/07','amount':3.84,'txnType':'D','narration':'FIG\/F\/93755167'},{'date':'20\/07','amount':100,'txnType':'D','narration':'FIG\/F\/93597279'},{'date':'20\/07','amount':3.84,'txnType':'D','narration':'FIG\/F\/93033302'},{'date':'20\/07','amount':100,'txnType':'D','narration':'FIG\/F\/92998016'},{'date':'20\/07','amount':3.84,'txnType':'D','narration':'FIG\/F\/92965805'},{'date':'20\/07','amount':3.84,'txnType':'D','narration':'FIG\/F\/92957139'}],'ministatementlist':[],'response_code':1,'last_aadhar':'9694','name':'JYOTI','clientrefno':'29469'}"
        '        'ss = ss.Replace("""", Chr(34))

        '        'Dim dt As New DataTable
        '        'Dim dc1 As DataColumn = New DataColumn("SrNo")
        '        'Dim dc2 As DataColumn = New DataColumn("date")
        '        'Dim dc3 As DataColumn = New DataColumn("txnType")
        '        'Dim dc4 As DataColumn = New DataColumn("amount")
        '        'Dim dc5 As DataColumn = New DataColumn("narration")

        '        'dt.Columns.Add(dc1)
        '        'dt.Columns.Add(dc2)
        '        'dt.Columns.Add(dc3)
        '        'dt.Columns.Add(dc4)
        '        'dt.Columns.Add(dc5)


        '        'Dim json1 As JObject = JObject.Parse(ss)
        '        'Dim aa As JToken = json1.SelectToken("ministatement")
        '        'Dim data12 As List(Of JToken) = aa.Children().ToList
        '        'For Each msg As JObject In data12
        '        '    For Each p In msg
        '        '        'jj = jj & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString
        '        '        'jj = msg("date").ToString & " " & msg("amount").ToString & " " & msg("txnType").ToString & " " & msg("narration").ToString
        '        '        Dim dr1 As DataRow = dt.NewRow()
        '        '        dr1(0) = i + 1
        '        '        dr1(1) = msg("date")
        '        '        dr1(2) = msg("txnType")
        '        '        dr1(3) = msg("amount")
        '        '        dr1(4) = msg("narration")
        '        '        dt.Rows.Add(dr1)
        '        '    Next
        '        'Next
        '        ''

        '        If ((Not (Request.Cookies("Super_UserName")) Is Nothing) _
        '                    AndAlso (Not (Request.Cookies("Super_Password")) Is Nothing)) Then

        '            If Request.Cookies("Super_CompanyCode") Is Nothing Then
        '                txtCompanyCode.Text = ""
        '            Else
        '                txtCompanyCode.Text = Request.Cookies("Super_CompanyCode").Value
        '            End If

        '            If Request.Cookies("Super_LoginAs") Is Nothing Then
        '                ddl_Login_For.SelectedIndex = 0
        '            Else
        '                ddl_Login_For.SelectedValue = Request.Cookies("Super_LoginAs").Value
        '            End If

        '            If Request.Cookies("Super_UserName") Is Nothing Then
        '                txtUserName.Text = ""
        '            Else
        '                txtUserName.Text = Request.Cookies("Super_UserName").Value
        '            End If

        '            If Request.Cookies("Super_Password") Is Nothing Then
        '                txtPassword.Attributes("value") = ""
        '            Else
        '                txtPassword.Attributes("value") = Request.Cookies("Super_Password").Value
        '            End If

        '            ChkrememberMe.Checked = True
        '        End If

        '        If Not IsPostBack Then

        '            Dim host As String = Request.Url.Host.ToLower()

        '            If host = "boscenter.in" Then
        '                If fBrowserIsMobile() = False Then
        '                    Response.Redirect("bos_index.html")
        '                Else
        '                    Response.Redirect("Webform6.aspx")
        '                    txtCompanyCode.Text = "CMP1085"
        '                    txtCompanyCode.Enabled = False
        '                    txtCompanyCode.CssClass = "form-control"
        '                End If
        '                'host = Request.Url.AbsoluteUri

        '                'Response.Redirect("kvishmoney/index.html")

        '            ElseIf host = "agent.kvishmoney.com" Or host = "www.agent.kvishmoney.com" Then
        '                Response.Redirect("kmLogin.aspx")

        '                'txtCompanyCode.Text = "CMP1165"
        '                'txtCompanyCode.Enabled = True
        '                'txtCompanyCode.CssClass = "form-control"
        '            ElseIf host = "easytalk.services" Or host = "www.easytalk.services" Then
        '                If fBrowserIsMobile() = False Then
        '                    Response.Redirect("ETLogin.aspx")
        '                    'txtCompanyCode.Text = "CMP1174"
        '                    'txtCompanyCode.Enabled = False
        '                    'txtCompanyCode.CssClass = "form-control"
        '                Else
        '                    Response.Redirect("EasyTalk_Login.aspx")
        '                    'txtCompanyCode.Text = "CMP1174"
        '                    'txtCompanyCode.Enabled = False
        '                    'txtCompanyCode.CssClass = "form-control"
        '                End If
        '                'Response.Redirect("ETLogin.aspx")
        '                'txtCompanyCode.Text = "CMP1174"
        '                'txtCompanyCode.Enabled = False
        '                'txtCompanyCode.CssClass = "form-control"

        '                'CMP1177
        '            ElseIf host = "payts.app" Or host = "www.payts.app" Then
        '                Response.Redirect("PaytsLogin.aspx")

        '            ElseIf host = "vdapayments.com" Or host = "www.vdapayments.com" Or host = "https://vdapayments.com" Then
        '                txtCompanyCode.Text = "CMP1207"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"

        '            ElseIf host = "www.login.urjamitra.org.in" Or host = "www.login.urjamitra.org.in" Or host = "https://login.urjamitra.org.in" Then
        '                Response.Redirect("UrgamitraLogin.aspx")
        '                txtCompanyCode.Text = "CMP1205"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"

        '            ElseIf host = "icicimikyc.shop" Or host = "www.icicimikyc.shop" Then
        '                txtCompanyCode.Text = "CMP1187"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"
        '            ElseIf host = "midigitalnewbanks.financial" Or host = "www.midigitalnewbanks.financial" Or host = "https://midigitalnewbanks.financial" Then
        '                txtCompanyCode.Text = "CMP1196"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"

        '                'midigitalnewbanks.financial
        '            ElseIf host = "aepsmerchant.in" Or host = "www.aepsmerchant.in" Or host = "https://aepsmerchant.in" Then
        '                txtCompanyCode.Text = "CMP1202"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"
        '            ElseIf host = "easysoft.org.in" Or host = "www.easysoft.org.in" Then
        '                txtCompanyCode.Text = "CMP1177"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"
        '            ElseIf host = "www.login.ozzype.com" Or host = "https://login.ozzype.com" Then
        '                If fBrowserIsMobile() = False Then
        '                    Response.Redirect("OzzypeB2B_Login.aspx")
        '                    txtCompanyCode.Text = "CMP1179"
        '                    txtCompanyCode.Enabled = False
        '                    txtCompanyCode.CssClass = "form-control"
        '                Else
        '                    Response.Redirect("OzzyPeLogin.aspx")
        '                    txtCompanyCode.Text = "CMP1179"
        '                    txtCompanyCode.Enabled = False
        '                    txtCompanyCode.CssClass = "form-control"
        '                End If
        '            ElseIf host = "www.paykre.com" Or host = "https://paykre.com" Then
        '                txtCompanyCode.Text = "CMP1218"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"
        '            ElseIf host = "www.publicsevakendra.co.in" Or host = "https://publicsevakendra.co.in" Then
        '                Response.Redirect("publicSevaKendra.aspx")
        '                txtCompanyCode.Text = "CMP1223"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"
        '            ElseIf host = "admin.yaseentech.com" Then
        '                txtCompanyCode.Text = "CMP1185"
        '                txtCompanyCode.Enabled = False
        '                txtCompanyCode.CssClass = "form-control"

        '            Else
        '                If Not Request.QueryString("admin") Is Nothing Then
        '                    If Not Request.QueryString("admin").Trim = "" Then
        '                        txtCompanyCode.Text = Request.QueryString("admin").Trim
        '                        txtCompanyCode.Enabled = False
        '                    Else
        '                        txtCompanyCode.Text = "CMP1085"
        '                        txtCompanyCode.Enabled = True
        '                    End If
        '                Else
        '                    txtCompanyCode.Text = "CMP1085"
        '                    txtCompanyCode.Enabled = True
        '                End If
        '                txtCompanyCode.CssClass = "form-control"
        '            End If
        '        End If
        '    End If

        '    'lblError.Text = Request.Url.Host.ToLower()
        '    'lblError.CssClass = "successlabels"
        '    'lblError.Visible = True
        '    Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
        '    Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")

        '    If strUrl.Trim.ToUpper = "https://midigitalnewbanks.financial/".Trim.ToUpper Then
        '        txtCompanyCode.Text = "CMP1196"
        '    ElseIf strUrl.Contains("midigitalnewbanks") Then
        '        txtCompanyCode.Text = "CMP1196"
        '    Else
        '        'txtCompanyCode.Text = "CMP1196"
        '    End If


        '    Dim VCompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " BOS_ClientRegistration where CompanyCode='" & GV.parseString(txtCompanyCode.Text.Trim) & "' and [Status]='Active' ")

        '    If VCompanyName.Trim = "" Then
        '        lblCompanyName.Text = "Login Panel"
        '    Else
        '        lblCompanyName.Text = VCompanyName
        '    End If

        '    'lblCompanyName.Text = Request.Url.Host.ToLower()

        '    'If DBName.Trim = "" Then
        '    '    lblError.Text = "CompanyCode is Incorrect"
        '    '    lblError.Visible = True
        '    '    lblError.CssClass = "errorlabels"
        '    '    Exit Sub
        '    'End If

        '    '  GV.sendNewMail(" Hello This is a Test again a test MAil ", " Hellow This is again New test mail ", "fasf43242@gmdail.com;sanarana44@gmail.com;verma.eklavya@gmail.com;support@businessonlinesolution.in;sdfafs@gdfs.com")


        '    'Dim host As String = Request.Url.Host.ToLower()
        '    'If host = "boscenter.in" Then
        '    '    'Response.Redirect("kvishmoney/index.html")
        '    '    Response.Redirect("bos_index.html")
        '    'ElseIf host = "agent.kvishmoney.com" Then
        '    'Else
        '    'End If


        '        Catch ex As Exception 
        ' GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        '    lblError.Text = ex.Message.ToString
        'End Try
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
    ''GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
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
            lblinvalid.Text = ""
            lblinvalid.CssClass = ""

            Dim CompanyCode As String = GV.parseString(txtCompanyCode.Text.Trim)


            If CompanyCode.Trim = "" Then
                CompanyCode = "CMP1085"
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

                                    ElseIf DBName.Trim = "CMP1174" Then
                                        Dim vMessage As String = "Dear " & vName & " Your OTP is : " & Session("OTP") & " . Please Do Not Share With Anyone .Thanks For Using Easy Talk Website:- https://bit.ly/3p1OkPj App:- https://bit.ly/3Szblqb"
                                        GV.send_Template_Based_SMS_API(GV.parseString(Email), vMessage, "Send Otp", "CMP1174")

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
                                    ElseIf DBName.Trim = "CMP1174" Then
                                        Dim vMessage As String = "Dear " & vName & " Your OTP is : " & Session("OTP") & " . Please Do Not Share With Anyone .Thanks For Using Easy Talk Website:- https://bit.ly/3p1OkPj App:- https://bit.ly/3Szblqb"
                                        GV.send_Template_Based_SMS_API(GV.parseString(Email), vMessage, "Send Otp", "CMP1174")
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
                            ElseIf DBName.Trim = "CMP1174" Then
                                Dim vMessage As String = "Dear " & vName & " Your Password Is " & strarr(1).ToString.Trim & " Please Do Not Share With Anyone. Thanks For Using Easy Talk. Web: https://bit.ly/3p1OkPj App: https://bit.ly/3Szblqb"
                                GV.send_Template_Based_SMS_API(GV.parseString(txtEmail.Text.Trim), vMessage, "Forgot Password", "CMP1174")
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
                            ElseIf DBName.Trim = "CMP1174" Then
                                Dim vMessage As String = "Dear " & vName & ", Your TPIN is " & strarr(2).ToString.Trim & " . Please Do Not Share With Anyone. Thanks For Using Easy Talk. Web: https://bit.ly/3p1OkPj App: https://bit.ly/3Szblqb"
                                GV.send_Template_Based_SMS_API(GV.parseString(txtEmail.Text.Trim), vMessage, "Forgot Pin", "CMP1174")
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    'Protected Sub Page_Load(senderAs Object, eAs EventArgs) Handles Me.Load
    '    Dim originAs String = GetLocation(18.6414, 72.8722)
    '    Dim destinationAs String = GetLocation(18.9647, 72.8258)
    '    lblDistance.Text = GetDrivingDistanceInMiles(origin, destination).ToString()
    'End Sub

    '''' <summary>
    '''' Get Driving Distance In Miles based on Source and Destination.
    '''' </summary>
    '''' <param name="origin"></param>
    '''' <param name="destination"></param>
    '''' <returns></returns>
    'Public Function GetDrivingDistanceInMiles(originAs String, destinationAs String) As Double
    '    Dim urlAs String = (Convert.ToString((Convert.ToString("https://maps.googleapis.com/maps/api/distancematrix/xml?origins=") & origin) +"&destinations=") & destination) +"&mode=driving&sensor=false&language=en-EN&units=imperial"
    '    Dim requestAs HttpWebRequest =DirectCast(WebRequest.Create(url), HttpWebRequest)
    '    Dim responseAs WebResponse = request.GetResponse()
    '    Dim dataStreamAs Stream = response.GetResponseStream()
    '    Dim sreaderAs New StreamReader(dataStream)
    '    Dim responsereaderAs String = sreader.ReadToEnd()
    '    Response.Close()
    '    Dim xmldocAs New XmlDocument()
    '    xmldoc.LoadXml(responsereader)
    '    If xmldoc.GetElementsByTagName("status")(0).ChildNodes(0).InnerText = "OK" Then
    '        Dim distanceAs XmlNodeList = xmldoc.GetElementsByTagName("distance")
    '        Return Convert.ToDouble(distance(0).ChildNodes(1).InnerText.Replace(" mi", ""))
    '    End If

    '    Return 0
    'End Function

    '''' <summary>
    '''' Get Location based on Latitude and Longitude.
    '''' </summary>
    '''' <param name="latitude"></param>
    '''' <param name="longitude"></param>
    '''' <returns></returns>
    'Public Function GetLocation(latitudeAs Double, longitudeAs Double) As String
    '    Dim urlAs String ="https://maps.googleapis.com/maps/api/geocode/xml?latlng=" & latitude &"," & longitude &"&sensor=false"
    '    Dim requestAs HttpWebRequest =DirectCast(WebRequest.Create(url), HttpWebRequest)
    '    Dim responseAs WebResponse = request.GetResponse()
    '    Dim dataStreamAs Stream = response.GetResponseStream()
    '    Dim sreaderAs New StreamReader(dataStream)
    '    Dim responsereaderAs String = sreader.ReadToEnd()
    '    Response.Close()
    '    Dim xmldocAs New XmlDocument()
    '    xmldoc.LoadXml(responsereader)
    '    If xmldoc.GetElementsByTagName("status")(0).ChildNodes(0).InnerText = "OK" Then
    '        Dim locationAs XmlNodeList = xmldoc.GetElementsByTagName("distance")
    '        Return xmldoc.GetElementsByTagName("formatted_address")(0).ChildNodes(0).InnerText
    '    End If

    '    Return ""
    'End Function


    'String url = String.Format("https://maps.googleapis.com/maps/api/distancematrix/xml?units=imperial&origins={0},{1}&destinations={2},{3}&key=AIzaSyCqNE77AmnSEywlJ63QTJsboi7E7wFPmMQ", lat1, lon1, lat2, lon2);

    Dim MobileCheck As Regex = New Regex("(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od|ad)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase Or RegexOptions.Multiline Or RegexOptions.Compiled)
    Dim MobileVersionCheck As Regex = New Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase Or RegexOptions.Multiline Or RegexOptions.Compiled)

    Public Function fBrowserIsMobile() As Boolean
        Dim result As Boolean = False
        Try
            Dim userAgent As String = Request.ServerVariables("HTTP_USER_AGENT")
            Debug.Assert(Not HttpContext.Current Is Nothing)

            If Not HttpContext.Current.Request Is Nothing And Not HttpContext.Current.Request.ServerVariables("HTTP_USER_AGENT") Is Nothing Then
                Dim u As String = HttpContext.Current.Request.ServerVariables("HTTP_USER_AGENT").ToString()
                If u.Length < 4 Then
                    result = False
                ElseIf MobileCheck.IsMatch(u) Or MobileVersionCheck.IsMatch(u.Substring(0, 4)) Then
                    result = True
                End If

            End If


            'Dim device_info As String = String.Empty
            'If OS.IsMatch(userAgent) Then
            '    device_info = OS.Match(userAgent).Groups(0).Value
            'End If
            'If device.IsMatch(userAgent.Substring(0, 4)) Then
            '    device_info += device.Match(userAgent).Groups(0).Value
            'End If
            'If Not String.IsNullOrEmpty(device_info) Then
            '    Response.Redirect(Convert.ToString("Mobile.aspx?device_info=") & device_info)
            'End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
        Return result
    End Function

End Class

