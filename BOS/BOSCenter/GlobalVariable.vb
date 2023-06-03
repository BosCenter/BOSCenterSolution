Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.SmtpClient
Imports System.Text
Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.Paragraph
Imports iTextSharp.text.html.simpleparser.CellWrapper
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports RestSharp
Imports System.Data.OleDb
Imports Newtonsoft.Json.Linq




Public Class GlobalVariable

    Public FL As EmvFuncLibForWebVBNET.WebVBDOTNET
    Public FL_WebService As EmvFuncLibForWebVBNET.WebVBDOTNET
    Public FL_AdminLogin As EmvFuncLibForWebVBNET.WebVBDOTNET

    Public key As String = "b14ca5898a4e4133bbce2ea2315a1916"

    Public OrderIDInfo As New System.Web.HttpCookie("OrderIDInfo")



    Public Sub New(ByVal ConnectionFrom As String)
        Try
            '"EMV_Bos", "S", "emvsoftware.in", "5Yl2wv1@", "EMV_USR"
            '"BosCenter_DB", "S", "103.90.242.173", "CapUY77RawrIZa7h", "sa"

            'If ConnectionFrom.Trim.ToUpper = "ADMIN" Then
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim, "S", "emvsoftware.in", "5Yl2wv1@", "EMV_USR")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("EMV_Bos", "S", "emvsoftware.in", "5Yl2wv1@", "EMV_USR")
            'ElseIf ConnectionFrom.Trim.ToUpper = "SUPERADMIN" Then
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET("EMV_Bos", "S", "emvsoftware.in", "5Yl2wv1@", "EMV_USR")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("EMV_Bos", "S", "emvsoftware.in", "5Yl2wv1@", "EMV_USR")
            'ElseIf ConnectionFrom.Trim.ToUpper = "Webservice".Trim.ToUpper Then
            '    FL_WebService = New EmvFuncLibForWebVBNET.WebVBDOTNET("EMV_Bos", "S", "emvsoftware.in", "5Yl2wv1@", "EMV_USR")
            'End If

            '"103.155.85.146", "I9XTw4cw"
            '"103.155.85.146", "I9XTw4cw"
            '"103.155.85.146", "Target@123",
            '"103.216.146.185", "F8ze830m",

            'If ConnectionFrom.Trim.ToUpper = "ADMIN" Then
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim, "S", "103.155.85.146", "Target@123", "sa")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.155.85.146", "Target@123", "sa")
            'ElseIf ConnectionFrom.Trim.ToUpper = "SUPERADMIN" Then
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.155.85.146", "Target@123", "sa")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.155.85.146", "Target@123", "sa")
            'ElseIf ConnectionFrom.Trim.ToUpper = "Webservice".Trim.ToUpper Then
            '    FL_WebService = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.155.85.146", "Target@123", "sa")
            'End If
            'BosCenter_DB
            If ConnectionFrom.Trim.ToUpper = "ADMIN" Then
                FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim, "S", "103.216.146.185", "F8zrde830n1", "sa")
                FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.216.146.185", "F8zrde830n1", "sa")
            ElseIf ConnectionFrom.Trim.ToUpper = "SUPERADMIN" Then
                FL = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.216.146.185", "F8zrde830n1", "sa")
                FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.216.146.185", "F8zrde830n1", "sa")
            ElseIf ConnectionFrom.Trim.ToUpper = "Webservice".Trim.ToUpper Then
                FL_WebService = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.216.146.185", "F8zrde830n1", "sa")
            End If

            'If ConnectionFrom.Trim.ToUpper = "ADMIN" Then 'F8ze830m
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim, "S", "DESKTOP-86QM4Q8", "eklavya", "sa")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "DESKTOP-86QM4Q8", "eklavya", "sa")
            'ElseIf ConnectionFrom.Trim.ToUpper = "SUPERADMIN" Then
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "DESKTOP-86QM4Q8", "eklavya", "sa")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "DESKTOP-86QM4Q8", "eklavya", "sa")
            'ElseIf ConnectionFrom.Trim.ToUpper = "Webservice".Trim.ToUpper Then
            '    FL_WebService = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "DESKTOP-86QM4Q8", "eklavya", "sa")
            'End If

            'If ConnectionFrom.Trim.ToUpper = "ADMIN" Then
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET(get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim, "S", "103.35.121.85,5022", "Boscenter@123", "sa")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.35.121.85,5022", "Boscenter@123", "sa")
            'ElseIf ConnectionFrom.Trim.ToUpper = "SUPERADMIN" Then
            '    FL = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.35.121.85,5022", "Boscenter@123", "sa")
            '    FL_AdminLogin = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.35.121.85,5022", "Boscenter@123", "sa")
            'ElseIf ConnectionFrom.Trim.ToUpper = "Webservice".Trim.ToUpper Then
            '    FL_WebService = New EmvFuncLibForWebVBNET.WebVBDOTNET("BosCenter_DB", "S", "103.35.121.85,5022", "Boscenter@123", "sa")
            'End If

        Catch ex As Exception
        End Try
    End Sub

    ' Public GDS As EmvFuncLibForWebVBNET.WebVBDOTNET

    'Password EncryptString
    'Dev : Naim Khan
    'Date : 06-04-2023
    Public Function EncryptString(ByVal key As String, ByVal plainText As String) As String
        Dim iv As Byte() = New Byte(15) {}
        Dim array As Byte()

        Using aes As Aes = Aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(key)
            aes.IV = iv
            Dim encryptor As ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)

            Using memoryStream As MemoryStream = New MemoryStream()

                Using cryptoStream As CryptoStream = New CryptoStream(CType(memoryStream, Stream), encryptor, CryptoStreamMode.Write)

                    Using streamWriter As StreamWriter = New StreamWriter(CType(cryptoStream, Stream))
                        streamWriter.Write(plainText)
                    End Using

                    array = memoryStream.ToArray()
                End Using
            End Using
        End Using

        Return Convert.ToBase64String(array)
    End Function

    'Password DecryptString
    'Dev : Naim Khan
    'Date : 06-04-2023
    Public Function DecryptString(ByVal key As String, ByVal cipherText As String) As String
        Dim iv As Byte() = New Byte(15) {}
        Dim buffer As Byte() = Convert.FromBase64String(cipherText)

        Using aes As Aes = Aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(key)
            aes.IV = iv
            Dim decryptor As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)

            Using memoryStream As MemoryStream = New MemoryStream(buffer)

                Using cryptoStream As CryptoStream = New CryptoStream(CType(memoryStream, Stream), decryptor, CryptoStreamMode.Read)

                    Using streamReader As StreamReader = New StreamReader(CType(cryptoStream, Stream))
                        Return streamReader.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using
    End Function

    Public Function sh1Encryption() As String
        Try
            Dim sValue As String = "Hello World!"
            Dim strToHash As String
            strToHash = sValue

            Dim sha1Obj As New System.Security.Cryptography.SHA1CryptoServiceProvider()

            Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

            bytesToHash = sha1Obj.ComputeHash(bytesToHash)

            Dim strResult As String = ""

            For Each b As Byte In bytesToHash
                strResult += b.ToString("x2")
            Next

            strResult = strResult

            Dim bValue As Byte() = System.Text.Encoding.ASCII.GetBytes(sValue)
            Dim clsSHA1 As New System.Security.Cryptography.SHA1CryptoServiceProvider()
            Dim bHashValue As Byte() = clsSHA1.ComputeHash(bValue)
            Dim sHashValue As String = System.Text.Encoding.ASCII.GetString(bHashValue)
            sHashValue = sHashValue
        Catch ex As Exception
        End Try
    End Function

    Private enc As System.Text.UTF8Encoding
    Private encryptor As ICryptoTransform
    Private decryptor As ICryptoTransform
    Public Function RdEncrypt(ByVal data As String) As String
        Dim convertedText As String = ""
        Try
            Dim symmetricKey As RijndaelManaged = New RijndaelManaged()
            symmetricKey.Mode = CipherMode.CBC
            Me.enc = New System.Text.UTF8Encoding
            Dim Inputkey As String = "enc_dckey_i#21(.vfi|rst|&indiaUK"
            Dim salt As String = "VfirstEnDcVector"
            Me.encryptor = symmetricKey.CreateEncryptor(Encoding.UTF8.GetBytes(Inputkey), Encoding.UTF8.GetBytes(salt))
            Dim sPlainText As String = data
            If Not String.IsNullOrEmpty(sPlainText) Then
                Dim memoryStream As MemoryStream = New MemoryStream()
                Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, Me.encryptor, CryptoStreamMode.Write)
                cryptoStream.Write(Me.enc.GetBytes(sPlainText), 0, sPlainText.Length)
                cryptoStream.FlushFinalBlock()
                convertedText = Convert.ToBase64String(memoryStream.ToArray())
                memoryStream.Close()
                cryptoStream.Close()
            End If
        Catch ex As Exception

        End Try
        Return convertedText
    End Function
    Public Function IsBase64String(ByVal data As String) As Boolean
        Dim result As Boolean = False
        Dim binaryData() As Byte
        Try
            binaryData = System.Convert.FromBase64String(data)
            result = True
        Catch ex As Exception
            result = False
        End Try
        Return result
    End Function
    Public Function RdDecrypt(ByVal data As String) As String
        Dim convertedText As String = ""
        Try
            Dim symmetricKey As RijndaelManaged = New RijndaelManaged()
            symmetricKey.Mode = CipherMode.CBC
            Me.enc = New System.Text.UTF8Encoding
            Dim Inputkey As String = "enc_dckey_i#21(.vfi|rst|&indiaUK"
            Dim salt As String = "VfirstEnDcVector"
            Me.decryptor = symmetricKey.CreateDecryptor(Encoding.UTF8.GetBytes(Inputkey), Encoding.UTF8.GetBytes(salt))
            Dim cypherTextBytes As Byte() = Convert.FromBase64String(data)
            Dim memoryStream As MemoryStream = New MemoryStream(cypherTextBytes)
            Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, Me.decryptor, CryptoStreamMode.Read)
            Dim plainTextBytes(cypherTextBytes.Length) As Byte
            Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
            memoryStream.Close()
            cryptoStream.Close()
            convertedText = Me.enc.GetString(plainTextBytes, 0, decryptedByteCount)
        Catch ex As Exception

        End Try
        Return convertedText
    End Function

    Public Function send_WhatsApp_Msg_API(ByVal MobileNos As String, txtMessage As String) As Boolean
        Dim Result As Boolean = False
        '// To run this api you need to have framework 4.5 or higher supporting SecurityProtocolType.Tls12
        '// Need to import Newtonsoft.Json.Linq
        Try
            Dim client As New WebClient()
            Dim API_URL As String = ""
            API_URL = "https://api.bulksmshospet.com/api/image.php?api=82dc70c47a1ac749d4f8be8cb162cd97&sender=919354125770&number=91" & MobileNos & "&message=" & txtMessage & "&image=https://emvsoftwares.com/style/images/logo.png"
            ServicePointManager.Expect100Continue = True
            ServicePointManager.DefaultConnectionLimit = 9999
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim data As Stream = client.OpenRead(API_URL)
            Dim reader As New StreamReader(data)
            Dim APIResult As String = reader.ReadToEnd()
            data.Close()
            reader.Close()
            Dim json1 As JObject = JObject.Parse(APIResult)
            If json1.SelectToken("status").ToString.Trim = True Then
                Result = True
            Else
                Result = False
            End If
        Catch ex As Exception
            Return Result
        End Try
        Return Result
    End Function


    '=================================== Start OrderID Cookieesss===========================================
    Public Sub Expire_OrderID_Session(ByVal request As System.Web.HttpRequest, ByVal response As System.Web.HttpResponse)
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "OrderIDInfo" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    OrderIDInfo = New System.Web.HttpCookie("OrderIDInfo")
                    OrderIDInfo("OrderID") = ""
                    OrderIDInfo.Expires = FL.getIndianStandardTime.AddDays(-12)
                    response.Cookies.Add(OrderIDInfo)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function get_OrderID_SessionVariables(ByVal VariableName As String, ByVal request As System.Web.HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "OrderIDInfo" Then
                        found = True
                        Exit For

                    End If
                Next

                If found = True Then
                    If VariableName = "OrderID" Then
                        Return request.Cookies("OrderIDInfo").Values("OrderID")
                    Else
                        Return ""
                    End If

                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub set_OrderID_SessionVariables(ByVal orderID As String, ByVal request As System.Web.HttpRequest, ByVal response As System.Web.HttpResponse)
        Try
            Expire_OrderID_Session(request, response)
            OrderIDInfo = New System.Web.HttpCookie("OrderIDInfo")
            OrderIDInfo("OrderID") = orderID
            OrderIDInfo.Expires = Now.AddHours(1)
            response.Cookies.Add(OrderIDInfo)
        Catch ex As Exception

        End Try
    End Sub

    Public Function PreparePOSTForm(ByVal url As String, ByVal data As System.Collections.Hashtable) As String
        Dim formID As String = "PostForm"
        Dim strForm As StringBuilder = New StringBuilder()
        strForm.Append("<form id='" & formID & "' name='" & formID & "' action='" & url & "' method='POST'>")

        For Each key As DictionaryEntry In data
            strForm.Append(" <input type='hidden' name='" & key.Key & "' value='" & key.Value & "'> ")
        Next
        strForm.Append("</form>")

        Dim strScript As StringBuilder = New StringBuilder()

        strScript.Append("<script language='javascript'> ")
        strScript.Append("var v" + formID + " = document." +
                         formID + "; ")
        strScript.Append(" v" + formID + ".submit(); ")
        strScript.Append("</script>")
        Return strForm.ToString() + strScript.ToString()
    End Function
    '=================================== End OrderID Cookieesss===========================================





    Public LoginInfo As New System.Web.HttpCookie("EMVSoft")
    Public SecureClient_Cookie As New System.Web.HttpCookie("SecureClient_Cookie")
    Public SecureClient_MenuCookie As New System.Web.HttpCookie("SecureClient_MenuCookie")
    Public AdminLoginInfo As New System.Web.HttpCookie("AdminLoginInfo")
    Public Admin_MenuCookie As New System.Web.HttpCookie("Admin_MenuCookie")
    Public ManageAllCookies As New System.Web.HttpCookie("ManageAllCookies")
    Public SuperAdmin_MenuCookie As New System.Web.HttpCookie("SuperAdmin_MenuCookie")

    Public MailSentStatus As Boolean = False
    Public ds As DataSet
    Public smtpclient As New SmtpClient
    Public message As New MailMessage
    Public New_SuperAdmin_Session As Boolean = False
    Public New_EmployeeAdmin_Session As Boolean = False

    Public Function Encode_Url_String(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return str.Replace(" ", "-").Replace("&", "_")
        End If
    End Function
    Public Function Decode_Url_String(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return str.Replace("-", " ").Replace("_", "&")
        End If
    End Function

    Public Function Commision_Calculation_For_SuperAdmin(Amount As Decimal, ByVal OperatorCategory As String, ByVal OperatorCode As String, APIName As String) As String
        Dim Result As String = ""
        Try
            Dim V_CommissionType, VServiceType As String
            V_CommissionType = ""
            VServiceType = ""
            Dim V_Commission, VServiceCharge As Decimal
            V_CommissionType = 0
            VServiceCharge = 0

            Dim VContainCategory, VSlabApplicable As String
            VContainCategory = ""
            VSlabApplicable = ""

            Dim SA_ComAmt, Service_Amt As Decimal
            SA_ComAmt = 0
            Service_Amt = 0

            Dim VFinal_SA_ComAmt As Decimal = 0
            Dim VFinal_Service_Amt As Decimal = 0

            Dim AdminID As String = ""

            Dim qryStr As String = "select * from " & DefaultDatabase.Trim & ".dbo.BOS_ProductServiceMaster_SA where Title='" & APIName.Trim & "' and ActiveStatus='Active'"
            ds = New DataSet
            ds = FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceType")) Then
                            If Not ds.Tables(0).Rows(0).Item("ServiceType").ToString() = "" Then
                                VServiceType = parseString(ds.Tables(0).Rows(0).Item("ServiceType").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceCharge")) Then
                            If Not ds.Tables(0).Rows(0).Item("ServiceCharge").ToString() = "" Then
                                VServiceCharge = parseString(ds.Tables(0).Rows(0).Item("ServiceCharge").ToString())
                            End If
                        End If

                        If VServiceType.Trim.ToUpper = "PERCENTAGE" Then
                            Service_Amt = Math.Round(((Amount * VServiceCharge) / 100), 2)
                        ElseIf VServiceType.Trim.ToUpper = "AMOUNT" Then
                            Service_Amt = (VServiceCharge)
                        End If

                        VFinal_Service_Amt = Service_Amt



                        If VContainCategory.Trim.ToUpper = "YES" And VSlabApplicable.Trim.ToUpper = "Without Slab".ToUpper Then
                            'Case Contain Category
                            ' Table BOS_OperatorWiseCommission_SA

                            qryStr = "select * from " & DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommission_SA where ActiveStatus='Active' and APIName='" & APIName.Trim & "' and Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "'"
                            ds = New DataSet
                            ds = FL.OpenDsWithSelectQuery(qryStr)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SA_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("SA_CommissionType").ToString() = "" Then
                                                V_CommissionType = parseString(ds.Tables(0).Rows(0).Item("SA_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SA_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("SA_Commission").ToString() = "" Then
                                                V_Commission = parseString(ds.Tables(0).Rows(0).Item("SA_Commission").ToString())
                                            End If
                                        End If

                                        If V_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SA_ComAmt = Math.Round(((Amount * V_Commission) / 100), 2)
                                        ElseIf V_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SA_ComAmt = (V_Commission)
                                        End If


                                        VFinal_SA_ComAmt = SA_ComAmt

                                    End If
                                End If
                            End If
                            'End Case Contain Category
                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then
                            'Case Slabwise Commission
                            ' Table BOS_CommissionSlabwise_SA

                            '/// Start With Slab

                            qryStr = " select * from  " & DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwise_SA where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='" & APIName.Trim & "'; "

                            ds = New DataSet
                            ds = FL.OpenDsWithSelectQuery(qryStr)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SA_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("SA_CommissionType").ToString() = "" Then
                                                V_CommissionType = parseString(ds.Tables(0).Rows(0).Item("SA_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SA_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("SA_Commission").ToString() = "" Then
                                                V_Commission = parseString(ds.Tables(0).Rows(0).Item("SA_Commission").ToString())
                                            End If
                                        End If

                                        If V_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SA_ComAmt = Math.Round(((Amount * V_Commission) / 100), 2)
                                        ElseIf V_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SA_ComAmt = (V_Commission)
                                        End If


                                        VFinal_SA_ComAmt = SA_ComAmt


                                    End If
                                End If
                            End If
                            'End Case Slabwise Commission

                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VSlabApplicable.Trim.ToUpper = "Without Slab".ToUpper Then
                            'Case not Slabwise Commission not Contain Category
                            ' Table BOS_ProductServiceMaster_SA

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("SA_CommissionType")) Then
                                If Not ds.Tables(0).Rows(0).Item("SA_CommissionType").ToString() = "" Then
                                    V_CommissionType = parseString(ds.Tables(0).Rows(0).Item("SA_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("SA_Commission")) Then
                                If Not ds.Tables(0).Rows(0).Item("SA_Commission").ToString() = "" Then
                                    V_Commission = parseString(ds.Tables(0).Rows(0).Item("SA_Commission").ToString())
                                End If
                            End If

                            If V_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                SA_ComAmt = Math.Round(((Amount * V_Commission) / 100), 2)
                            ElseIf V_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                SA_ComAmt = (V_Commission)
                            End If


                            VFinal_SA_ComAmt = SA_ComAmt
                            'End Case not Slabwise Commission not Contain Category
                        End If


                    End If
                End If
            End If
            Result = "SUPERADMIN" & ":" & VFinal_SA_ComAmt & "*" & "SERVICECHARGE" & ":" & VFinal_Service_Amt
            '/////////////////////////////////////////////////////////////

        Catch ex As Exception
            Return Result
        End Try
        Return Result

    End Function
    Public Function Commision_Calculation_For_Admin(Amount As Decimal, ByVal OperatorCategory As String, ByVal OperatorCode As String, APIName As String, AdminID As String) As String
        Dim Result As String = ""
        Try
            Dim V_CommissionType, VServiceType As String
            V_CommissionType = ""
            VServiceType = ""
            Dim V_Commission, VServiceCharge As Decimal
            V_CommissionType = 0
            VServiceCharge = 0

            Dim VContainCategory, VSlabApplicable As String
            VContainCategory = ""
            VSlabApplicable = ""

            Dim SA_ComAmt, Service_Amt As Decimal
            SA_ComAmt = 0
            Service_Amt = 0

            Dim VFinal_SA_ComAmt As Decimal = 0
            Dim VFinal_Service_Amt As Decimal = 0

            Dim qryStr As String = "select * from " & DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where AdminID='" & AdminID.Trim & "' and  Title='" & APIName.Trim & "' and ActiveStatus='Active'"
            ds = New DataSet
            ds = FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceType")) Then
                            If Not ds.Tables(0).Rows(0).Item("ServiceType").ToString() = "" Then
                                VServiceType = parseString(ds.Tables(0).Rows(0).Item("ServiceType").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceCharge")) Then
                            If Not ds.Tables(0).Rows(0).Item("ServiceCharge").ToString() = "" Then
                                VServiceCharge = parseString(ds.Tables(0).Rows(0).Item("ServiceCharge").ToString())
                            End If
                        End If

                        If VServiceType.Trim.ToUpper = "PERCENTAGE" Then
                            Service_Amt = Math.Round(((Amount * VServiceCharge) / 100), 2)
                        ElseIf VServiceType.Trim.ToUpper = "AMOUNT" Then
                            Service_Amt = (VServiceCharge)
                        End If

                        VFinal_Service_Amt = Service_Amt



                        If VContainCategory.Trim.ToUpper = "YES" And VSlabApplicable.Trim.ToUpper = "Without Slab".ToUpper Then
                            'Case Contain Category
                            ' Table BOS_OperatorWiseCommission_SA

                            qryStr = "select * from " & DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & AdminID.Trim & "' and   ActiveStatus='Active' and APIName='" & APIName.Trim & "' and Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "'"
                            ds = New DataSet
                            ds = FL.OpenDsWithSelectQuery(qryStr)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString() = "" Then
                                                V_CommissionType = parseString(ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Admin_Commission").ToString() = "" Then
                                                V_Commission = parseString(ds.Tables(0).Rows(0).Item("Admin_Commission").ToString())
                                            End If
                                        End If

                                        If V_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SA_ComAmt = Math.Round(((Amount * V_Commission) / 100), 2)
                                        ElseIf V_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SA_ComAmt = (V_Commission)
                                        End If


                                        VFinal_SA_ComAmt = SA_ComAmt

                                    End If
                                End If
                            End If
                            'End Case Contain Category
                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then
                            'Case Slabwise Commission
                            ' Table BOS_CommissionSlabwise_SA

                            '/// Start With Slab

                            qryStr = " select * from  " & DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA where AdminID='" & AdminID.Trim & "' and   (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='" & APIName.Trim & "'; "

                            ds = New DataSet
                            ds = FL.OpenDsWithSelectQuery(qryStr)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString() = "" Then
                                                V_CommissionType = parseString(ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Admin_Commission").ToString() = "" Then
                                                V_Commission = parseString(ds.Tables(0).Rows(0).Item("Admin_Commission").ToString())
                                            End If
                                        End If

                                        If V_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SA_ComAmt = Math.Round(((Amount * V_Commission) / 100), 2)
                                        ElseIf V_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SA_ComAmt = (V_Commission)
                                        End If


                                        VFinal_SA_ComAmt = SA_ComAmt


                                    End If
                                End If
                            End If
                            'End Case Slabwise Commission

                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VSlabApplicable.Trim.ToUpper = "Without Slab".ToUpper Then
                            'Case not Slabwise Commission not Contain Category
                            ' Table BOS_ProductServiceMaster_SA

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_CommissionType")) Then
                                If Not ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString() = "" Then
                                    V_CommissionType = parseString(ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_Commission")) Then
                                If Not ds.Tables(0).Rows(0).Item("Admin_Commission").ToString() = "" Then
                                    V_Commission = parseString(ds.Tables(0).Rows(0).Item("Admin_Commission").ToString())
                                End If
                            End If

                            If V_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                SA_ComAmt = Math.Round(((Amount * V_Commission) / 100), 2)
                            ElseIf V_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                SA_ComAmt = (V_Commission)
                            End If


                            VFinal_SA_ComAmt = SA_ComAmt
                            'End Case not Slabwise Commission not Contain Category
                        End If


                    End If
                End If
            End If
            Result = "ADMIN" & ":" & VFinal_SA_ComAmt & "*" & "SERVICECHARGE" & ":" & VFinal_Service_Amt
            '/////////////////////////////////////////////////////////////

        Catch ex As Exception
            Return Result
        End Try
        Return Result

    End Function

    Public Function AgentBalance(ByVal AgentID As String, DBName As String) As Decimal
        Dim balanceAmt As Decimal = 0.0
        Try

            Dim str As String = "select ((select isnull(Sum(isnull(TransferAmt,0)),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & AgentID & "')"
            str = str & " - "
            str = str & " (select isnull(Sum(isnull(TransferAmt,0)),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & AgentID & "')) as 'WalletBal' "


            Dim LocalDS As New DataSet
            LocalDS = FL.OpenDsWithSelectQuery(str)

            If Not LocalDS Is Nothing Then
                If LocalDS.Tables.Count > 0 Then
                    If LocalDS.Tables(0).Rows.Count > 0 Then
                        balanceAmt = LocalDS.Tables(0).Rows(0).Item(0)
                    End If
                End If
            End If
        Catch ex As Exception
            Return balanceAmt
        End Try
        Return balanceAmt
    End Function

    Public Sub TransportType(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim AccountType() As String = {":::: Select Transportation Mode  ::::", "BY AIR", "BY ROAD", "BY SHIP", "BY OWN VEHICLE"}
            For i As Integer = 0 To AccountType.Length - 1
                ddl.Items.Add(AccountType(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Function returnAPIBalance(DBName As String) As Long
        Dim Balance As Long = 0
        Try
            Dim Querystring As String = ""
            Querystring = Querystring & " SELECT ("
            Querystring = Querystring & " ("
            Querystring = Querystring & " (select isnull(sum(isnull(TransferAmt,0)),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and TransferTo='Admin'  ) "
            Querystring = Querystring & " +"
            Querystring = Querystring & " (select isnull(sum(isnull(TransferAmt,0)),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Deposit') and TransferFrom='Super Admin' and TransferTo='Admin')"
            Querystring = Querystring & " +"
            Querystring = Querystring & " (select isnull(sum(isnull(TransferAmt,0)),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer-Refund','AEPS','PAN CARD-Refund','Money Transfer-2-Refund','BILLPAY-Refund','RECH-Refund','BOS Shopping-Refund') and TransferFrom='Admin')   "
            Querystring = Querystring & " )"
            Querystring = Querystring & " - "
            Querystring = Querystring & " ("
            Querystring = Querystring & " (select isnull(sum(isnull(TransferAmt,0)),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Withdraw') and TransferFrom='Admin' and TransferTo='Super Admin') "
            Querystring = Querystring & " +"
            Querystring = Querystring & " (select isnull(sum(isnull(TransferAmt,0)),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Money Transfer','PAN CARD','RECH','BILLPAY','Money Transfer-2','BOS Shopping') and TransferTo='Admin')   "
            Querystring = Querystring & " )"
            Querystring = Querystring & " ) as 'APIBalance'"

            Dim ds1 As New DataSet
            ds1 = FL.OpenDsWithSelectQuery(Querystring)

            If Not ds1 Is Nothing Then
                If ds1.Tables.Count > 0 Then
                    If ds1.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds1.Tables(0).Rows(0).Item("APIBalance")) Then
                            If Not ds1.Tables(0).Rows(0).Item("APIBalance").ToString() = "" Then
                                Balance = parseString(ds1.Tables(0).Rows(0).Item("APIBalance").ToString())
                            Else
                                Balance = 0
                            End If
                        Else
                            Balance = 0
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Return Balance
        End Try
        Return Balance
    End Function

    Public Function returnAdminWalletBalance(DBName As String) As Long
        Dim Balance As Long = 0
        Try
            Dim Querystring As String = ""
            Querystring = "  select (select isnull(sum (TransferAmt),0)"
            Querystring = Querystring & " + (select isnull(sum (TransferAmt),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Commission','Service Charge','Withdraw','Commission-Refund','Service Charge-Refund') and TransferTo='Admin') "
            Querystring = Querystring & " + (select isnull(sum (TransferAmt),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Deposit') and TransferFrom='Super Admin' and TransferTo='Admin')"
            Querystring = Querystring & " - (select isnull(sum (TransferAmt),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Service Charge') and TransferFrom='Admin' and 	TransferTo='Super Admin' )"
            Querystring = Querystring & " - (select isnull(sum (TransferAmt),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Withdraw') and TransferFrom='Admin' and 	TransferTo='Super Admin' )"
            Querystring = Querystring & " - ( select isnull(sum (TransferAmt),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where  Amount_Type in ('Service Charge')  and not TransferTo='Super Admin' and TransferFrom='Admin')"
            Querystring = Querystring & " - ( select isnull(sum (TransferAmt),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('Commission','Transaction Charge') and TransferFrom='Admin')"
            Querystring = Querystring & " - ( select isnull(sum (TransferAmt),0) from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('MakePayment','Deposit','Service Charge-Refund','Commission-Refund') and TransferFrom='Admin')"
            Querystring = Querystring & " from " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where Amount_Type in ('MakePayment') and TransferFrom='Super Admin' and 	TransferTo='Admin'"
            Querystring = Querystring & " ) as 'BalAmt'"

            Dim ds1 As New DataSet
            ds1 = FL.OpenDsWithSelectQuery(Querystring)

            If Not ds1 Is Nothing Then
                If ds1.Tables.Count > 0 Then
                    If ds1.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds1.Tables(0).Rows(0).Item("BalAmt")) Then
                            If Not ds1.Tables(0).Rows(0).Item("BalAmt").ToString() = "" Then
                                Balance = parseString(ds1.Tables(0).Rows(0).Item("BalAmt").ToString())
                            Else
                                Balance = 0
                            End If
                        Else
                            Balance = 0
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Return Balance
        End Try
        Return Balance
    End Function
    Public Function GetQueryToUpdateToken(ByVal TaskResult As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try

            If get_Admin_SessionVariables("UserType", request, response).Trim.ToUpper = "Employee".ToUpper Then
                If get_Admin_SessionVariables("ShowUserRightsPanel", request, response).Trim.ToUpper = "Use Task Panel".ToUpper Then
                    Dim EmpCode, TokenID, BranchCode, TokenTaskID, QryStr_1, CompanyCode As String

                    CompanyCode = get_Admin_SessionVariables("CompanyCode", request, response)
                    BranchCode = get_Admin_SessionVariables("BranchCode", request, response)
                    EmpCode = get_Admin_SessionVariables("LoginID", request, response)
                    TokenID = get_Admin_SessionVariables("TokenID", request, response).Trim
                    TokenTaskID = get_Admin_SessionVariables("TokenTaskID", request, response).Trim


                    If FL.RecCount(" CRM_Admin_Token_VS_TaskMaster where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "' and not (TokenTaskID='" & TokenTaskID & "') and TaskStatus='Pending' ") <= 0 Then
                        QryStr_1 = "update CRM_Admin_TokenMaster "
                        QryStr_1 = QryStr_1 & " " & " set PendingTask=0, CompletedTask=TotalTask, TokenStatus='Completed' "
                        QryStr_1 = QryStr_1 & " " & " where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "'; "

                        QryStr_1 = QryStr_1 & " " & " update CRM_Admin_Token_VS_TaskMaster"
                        QryStr_1 = QryStr_1 & " " & " set TaskStatus='Completed', TaskResult='" & TaskResult & "'"
                        QryStr_1 = QryStr_1 & " " & " where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "' and TokenTaskID='" & TokenTaskID & "';"

                    Else
                        QryStr_1 = " update CRM_Admin_TokenMaster "
                        QryStr_1 = QryStr_1 & " " & " set PendingTask=PendingTask-1, CompletedTask=CompletedTask+1, TokenStatus='Pending' "
                        QryStr_1 = QryStr_1 & " " & " where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "'; "

                        QryStr_1 = QryStr_1 & " " & " update CRM_Admin_Token_VS_TaskMaster "
                        QryStr_1 = QryStr_1 & " " & " set TaskStatus='Completed', TaskResult='" & TaskResult & "'"
                        QryStr_1 = QryStr_1 & " " & " where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "' and TokenTaskID='" & TokenTaskID & "';"

                    End If


                    'QryStr_1 = "update CRM_Admin_TokenMaster"
                    'QryStr_1 = QryStr_1 & " " & "set PendingTask=PendingTask-1,CompletedTask=CompletedTask+1,"
                    'QryStr_1 = QryStr_1 & " " & "TokenStatus=("
                    'QryStr_1 = QryStr_1 & " " & "select case when ((select count(*) as count from CRM_Admin_Token_VS_TaskMaster where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "' and not TokenTaskID='" & TokenTaskID & "' and TaskStatus='Pending')<=0"
                    'QryStr_1 = QryStr_1 & " " & ") then 'Completed'  else 'Pending'  end)"
                    'QryStr_1 = QryStr_1 & " " & "where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "'; "

                    'QryStr_1 = QryStr_1 & " " & "update CRM_Admin_Token_VS_TaskMaster"
                    'QryStr_1 = QryStr_1 & " " & "set TaskStatus='Completed', TaskResult='" & TaskResult & "'"
                    'QryStr_1 = QryStr_1 & " " & "where TokenID='" & TokenID & "' and EmpCode='" & EmpCode & "' and BranchCode='" & BranchCode & "' and CompanyCode='" & CompanyCode & "' and TokenTaskID='" & TokenTaskID & "';"

                    Return QryStr_1
                    Exit Function
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
        Return ""
    End Function

    Public Function AvailableShare(ByVal VCompanyCode As String) As String
        Try

            ds = New DataSet
            Dim qry As String = "select (select isnull(sum(No_Of_Shares),0) -(select isnull(sum(NoOFShares),0) from CRM_Admin_Alloted_ShareMaster where Recordstatus='Active' and CompanyCode='" & VCompanyCode & "') ) as 'Available Shares' from CRM_Admin_SharesMaster where CompanyCode='" & VCompanyCode & "'"

            ds = FL.OpenDsWithSelectQuery(qry)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Return ds.Tables(0).Rows(0).Item(0).ToString

                    Else
                        Return 0
                    End If
                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function parseString(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return StrConv(Replace(Replace(Replace(Replace(str, "'", "''"), "&amp;", "&"), "&nbsp;", ""), "&#39;", "''"), VbStrConv.None).Trim().Replace(";", ",")
        End If
    End Function

    Public Function get_AutoNumber(ByVal fieldname As String, DatabaseName As String) As Integer 'done
        Dim valueadd As Integer = 0
        Try
            '" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.

            Dim dsAutoNumber As New DataSet
            Dim qry As String = ""
            qry = " Declare @result nvarchar(max) "
            qry = qry + " if (select count(*) from " & DatabaseName & ".dbo.AutoNumber)>0"
            qry = qry + " begin"
            qry = qry + " update " & DatabaseName & ".dbo.AutoNumber set " + fieldname + "=ISNULL(" + fieldname + ",0)+1 "
            qry = qry + " set @result=(select " + fieldname + " from " & DatabaseName & ".dbo.AutoNumber); "
            qry = qry + " end"
            qry = qry + " else"
            qry = qry + " begin"
            qry = qry + " insert into " & DatabaseName & ".dbo.AutoNumber(" + fieldname + ") values('1')"
            qry = qry + " set @result=(select 1 as " + fieldname + ");"
            qry = qry + " end"
            qry = qry + " select  @result as " & fieldname

            dsAutoNumber = FL.OpenDsWithSelectQuery(qry)

            If Not dsAutoNumber Is Nothing Then
                If dsAutoNumber.Tables.Count > 0 Then
                    If dsAutoNumber.Tables(0).Rows.Count > 0 Then
                        valueadd = dsAutoNumber.Tables(0).Rows(0).Item("" & fieldname & "")
                    Else
                        Return valueadd
                    End If
                Else
                    Return valueadd
                End If
            Else
                Return valueadd
            End If


        Catch ex As Exception
            Return valueadd
        End Try
        Return valueadd
    End Function
    Public con As OleDbConnection

    Public Function RecCount(ByVal Table_Name As String) As Integer  'done
        Dim affectedRows As Integer = 0
        Try

            Dim ds11 As New DataSet
            ds11 = FL.OpenDsWithSelectQuery("select count(*) as 'TotalRow' from " & Table_Name)
            If Not ds11 Is Nothing Then
                If ds11.Tables.Count > 0 Then
                    If ds11.Tables(0).Rows.Count > 0 Then
                        affectedRows = CInt(ds11.Tables(0).Rows(0).Item("TotalRow"))
                    Else
                        affectedRows = 0
                    End If
                Else
                    affectedRows = 0
                End If
            Else
                affectedRows = 0
            End If

            Return affectedRows
            Exit Function
        Catch ex As Exception
            affectedRows = -1
            Return affectedRows
            Exit Function
        End Try
        Return affectedRows
    End Function
    Public Function OpenDsWithSelectQuery(ByVal Query As String) As DataSet  'done
        Try
            conOpen()
            ds = New DataSet
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(Query, con)
            da.SelectCommand.CommandTimeout = 300000
            da.Fill(ds)
            con.Close()
        Catch ex As Exception

        End Try
        Return ds
    End Function
    Public Sub conOpen() 'done
        Try

            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If

        Catch ex As Exception

        End Try
    End Sub


    Public Function AdvisorVerify_LoginTimeOut(ByVal loginId As String, ByVal CompanyCode As String, ByVal whosLogin As String) As Boolean
        Dim result As Boolean = True
        Try
            Dim str As String = ""
            If whosLogin = "Customer" Then
                str = " CRM_Login_Details where User_ID='" & loginId & "' and RecordStatus='Active' and AccountStatus='Active'"
            ElseIf whosLogin = "Advisor" Then
                str = " AutoNumber_Admin where CompanyCode='" & CompanyCode & "'"
            Else
                str = ""
            End If

            If Not str.Trim = "" Then
                LocalDS = New DataSet
                LocalDS = FL.OpenDs(str)
                If LocalDS.Tables(0).Rows.Count > 0 Then
                    Dim FromAMPM, FromTime, ToTime, ToAmPm, CurrentAMPM, CurrentTime As String
                    Dim FromTimeCom() As String = Split(parseString(LocalDS.Tables(0).Rows(0).Item("Advisor_Fromtime")), "-")
                    Dim ToTimeCom() As String = Split(parseString(LocalDS.Tables(0).Rows(0).Item("Advisor_Totime")), "-")

                    If FromTimeCom.Length > 0 And ToTimeCom.Length > 0 Then
                        FromTime = FromTimeCom(0).Replace(":", ".")
                        FromAMPM = FromTimeCom(1)

                        ToTime = ToTimeCom(0).Replace(":", ".")
                        ToAmPm = ToTimeCom(1)

                        If Now.Hour >= 12 Then
                            CurrentAMPM = "PM"
                            CurrentTime = (CInt(Now.Hour) - 12) & "." & Now.Minute
                        Else
                            CurrentAMPM = "AM"
                            CurrentTime = Now.Hour & "." & Now.Minute
                        End If


                        If FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "PM" Then
                            result = False
                        ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "AM" Then
                            If Not ((CDec(CurrentTime) >= CDec(FromTime)) And (CDec(CurrentTime) <= CDec(ToTime))) Then
                                result = False
                            End If
                        ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "AM" Then
                            result = False
                        ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "PM" Then
                            If Not ((CDec(CurrentTime) >= CDec(FromTime)) And (CDec(CurrentTime) <= CDec(ToTime))) Then
                                result = False
                            End If
                        ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "AM" Then
                            If CDec(FromTime) >= CDec(CurrentTime) Then
                                result = False
                            End If
                        ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "PM" Then
                            If CDec(CurrentTime) >= CDec(ToTime) Then
                                result = False
                            End If
                        ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "PM" Then
                            If CDec(FromTime) >= CDec(CurrentTime) Then
                                result = False
                            End If
                        ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "AM" Then
                            If CDec(ToTime) <= CDec(CurrentTime) Then
                                result = False
                            End If
                        End If
                    End If
                End If
            End If

            Return result
        Catch ex As Exception
            Return result
        End Try
    End Function

    Public Function ISTrackingDay(ByVal GPSNotTarckingDay As String, ByVal CurrentDay As String) As Boolean
        Dim IsTrack As Boolean = True
        Try
            Dim IsNotTrackingday() As String = GPSNotTarckingDay.Split(",")

            For i As Integer = 0 To IsNotTrackingday.Length - 1
                If IsNotTrackingday(i).ToUpper.ToString.Trim = CurrentDay.ToUpper.Trim Then
                    IsTrack = False
                    Exit For
                End If
            Next
            Return IsTrack
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function AdvisorVerify_TrackingTime(ByVal loginId As String, ByVal CompanyCode As String, ByVal NewLat As String, ByVal NewLng As String) As Boolean
        Dim result As Boolean = True
        Try
            Dim str As String = ""

            Dim CurrentDay, GPS_IsTrackingApplicable, AccountStatus, GPS_TrackDistance, GPS_TrackDuration As String

            'str = "select GPS_TrackDistance ,GPS_TrackDuration , GPS_IsTrackingApplicable , GPS_DaysWhenNotToTrack , upper((select DATENAME(weekday, SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30')))) , AccountStatus , GPS_TrackFromTime ,  GPS_TrackToTime , (SELECT CONVERT(VARCHAR(5),SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30'), 114) + (CASE WHEN DATEPART(HOUR, SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30')) >= 12 THEN '-PM' ELSE '-AM' END)) as CurrentTime from  CRM_Admin_AdvisorRegistration where AdvisorId='" & loginId & "' and CompanyCode='" & CompanyCode & "'"
            str = "select GPS_TrackDistance ,GPS_TrackDuration , GPS_IsTrackingApplicable , GPS_DaysWhenNotToTrack , upper((select DATENAME(weekday, SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30')))) as CurrentDay , AccountStatus , GPS_TrackFromTime ,  GPS_TrackToTime , (SELECT CONVERT(VARCHAR(5),SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30'), 114) + (CASE WHEN DATEPART(HOUR, SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30')) >= 12 THEN '-PM' ELSE '-AM' END)) as CurrentTime,GPS_NextScheduledTrack from  CRM_Admin_AdvisorRegistration where AdvisorId='" & loginId & "' and CompanyCode='" & CompanyCode & "'"
            str = str & " ; " & "select top 1 * from CRM_Admin_GPS_Cord_Data where AdvisorId='" & loginId & "' and CompanyCode='" & CompanyCode & "' order by Rid Desc "

            If Not str.Trim = "" Then
                LocalDS = New DataSet
                LocalDS = FL_WebService.OpenDsWithSelectQuery(str)
                If LocalDS.Tables(0).Rows.Count > 0 Then
                    Dim FromAMPM, FromTime, ToTime, ToAmPm, CurrentAMPM, CurrentTime As String
                    Dim FromTimeCom() As String = Split(parseString(LocalDS.Tables(0).Rows(0).Item("GPS_TrackFromTime")), "-")
                    Dim ToTimeCom() As String = Split(parseString(LocalDS.Tables(0).Rows(0).Item("GPS_TrackToTime")), "-")
                    Dim GPS_DaysWhenNotToTrack As String = parseString(LocalDS.Tables(0).Rows(0).Item("GPS_DaysWhenNotToTrack"))
                    CurrentDay = parseString(LocalDS.Tables(0).Rows(0).Item("CurrentDay"))
                    GPS_IsTrackingApplicable = parseString(LocalDS.Tables(0).Rows(0).Item("GPS_IsTrackingApplicable"))
                    AccountStatus = parseString(LocalDS.Tables(0).Rows(0).Item("AccountStatus"))
                    GPS_TrackDistance = parseString(LocalDS.Tables(0).Rows(0).Item("GPS_TrackDistance"))
                    GPS_TrackDuration = parseString(LocalDS.Tables(0).Rows(0).Item("GPS_TrackDuration"))

                    '  Dim IsNotTrackingday() As String = GPS_DaysWhenNotToTrack.Split(",")
                    If ISTrackingDay(GPS_DaysWhenNotToTrack, CurrentDay) Then
                        If (GPS_IsTrackingApplicable.Trim.ToUpper = "Yes".Trim.ToUpper And AccountStatus.Trim.ToUpper = "Active".Trim.ToUpper) Then
                            If FromTimeCom.Length > 0 And ToTimeCom.Length > 0 Then
                                FromTime = FromTimeCom(0).Replace(":", ".")
                                FromAMPM = FromTimeCom(1)

                                ToTime = ToTimeCom(0).Replace(":", ".")
                                ToAmPm = ToTimeCom(1)

                                If Now.Hour >= 12 Then
                                    CurrentAMPM = "PM"
                                    CurrentTime = (CInt(Now.Hour) - 12) & "." & Now.Minute
                                Else
                                    CurrentAMPM = "AM"
                                    CurrentTime = Now.Hour & "." & Now.Minute
                                End If


                                If FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "PM" Then
                                    result = False
                                ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "AM" Then
                                    If Not ((CDec(CurrentTime) >= CDec(FromTime)) And (CDec(CurrentTime) <= CDec(ToTime))) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "AM" Then
                                    result = False
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "PM" Then
                                    If Not ((CDec(CurrentTime) >= CDec(FromTime)) And (CDec(CurrentTime) <= CDec(ToTime))) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "AM" Then
                                    If CDec(FromTime) >= CDec(CurrentTime) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "PM" Then
                                    If CDec(CurrentTime) >= CDec(ToTime) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "PM" Then
                                    If CDec(FromTime) >= CDec(CurrentTime) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "AM" Then
                                    If CDec(ToTime) <= CDec(CurrentTime) Then
                                        result = False
                                    End If
                                End If
                            End If
                        Else
                            result = False
                        End If
                    Else
                        result = False
                    End If

                Else
                    result = False

                End If

                If result = True Then


                    Dim OldLat, Oldlong As String
                    If LocalDS.Tables(1).Rows.Count > 0 Then
                        OldLat = parseString(LocalDS.Tables(1).Rows(0).Item("lat"))
                        Oldlong = parseString(LocalDS.Tables(1).Rows(0).Item("Lng"))


                        If parseString(GPS_TrackDistance.Trim) = "" Then
                            GPS_TrackDistance = "1"
                        End If

                        Dim GPS_NextScheduledTrack As DateTime
                        If DistanceBtwTwoCoordinates(OldLat, Oldlong, NewLat, NewLng, "Mtr") >= GPS_TrackDistance Then
                            If IsDBNull(LocalDS.Tables(0).Rows(0).Item("GPS_NextScheduledTrack")) Then
                                GPS_NextScheduledTrack = Now
                            Else
                                GPS_NextScheduledTrack = parseString(LocalDS.Tables(0).Rows(0).Item("GPS_NextScheduledTrack"))
                            End If

                            Dim CurrentDate As Integer = DateDiff(DateInterval.Second, GPS_NextScheduledTrack, Now)
                            If CurrentDate >= 0 Then
                                result = True
                            Else
                                result = False

                            End If


                        Else
                            result = False

                        End If



                    End If
                End If



            End If

            Return result
        Catch ex As Exception
            Return result
        End Try
    End Function

    Public Function UpdateNextShedDate(ByVal AdvisorId As String, ByVal CompanyCode As String, ByVal GPSDuration As Integer)
        Try

            Dim str As String = " select * form CRM_Admin_AdvisorRegistration where AdvisorID='" & AdvisorId & "' and CompanyCode='" & CompanyCode & "'"
            LocalDS = New DataSet
            If Not str = "" Then
                LocalDS = FL_WebService.OpenDsWithSelectQuery(str)

                If LocalDS.Tables(0).Rows.Count > 0 Then
                    Dim GPS_NextScheduledTrack As Date = parseString(LocalDS.Tables(0).Rows(0).Item("GPS_NextScheduledTrack"))
                    If GPS_NextScheduledTrack = "" And GPS_NextScheduledTrack = "NULL" Then
                        GPS_NextScheduledTrack = Now
                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Function

#Region "Distance Functions GPS Co-Ordinates"
    Public Function DistanceBtwTwoCoordinates(ByVal Old_lat As Double, ByVal Old_long As Double, ByVal New_lat As Double, ByVal New_long As Double, ByVal unit As String) As Double
        Dim theta As Double = Old_long - New_long
        Dim dist As Double = Math.Sin(deg2rad(Old_lat)) * Math.Sin(deg2rad(New_lat)) + Math.Cos(deg2rad(Old_lat)) * Math.Cos(deg2rad(New_lat)) * Math.Cos(deg2rad(theta))
        dist = Math.Acos(dist)
        dist = rad2deg(dist)
        dist = dist * 60 * 1.1515
        If unit = "K" Then 'K = kilometer
            dist = dist * 1.60934
        ElseIf unit = "N" Then
            dist = dist * 0.8684
        ElseIf unit = "M" Then  'M = miles
            dist = dist
        ElseIf unit = "Mtr" Then  'Mtr = metre
            dist = dist * 1609.34
        End If
        Return dist
    End Function

    Private Function deg2rad(ByVal deg As Double) As Double
        Return (deg * Math.PI / 180.0)
    End Function

    Private Function rad2deg(ByVal rad As Double) As Double
        Return rad / Math.PI * 180.0
    End Function
#End Region

    Public Function is_SuperAdmin_validSession(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As Boolean
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "EMVSoft" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    If Not request.Cookies("EMVSoft").Values("Session_Id") = "" Then
                        Return True
                    End If
                Else
                    Return False

                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Expire_SuperAdmin_Session(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "EMVSoft" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    LoginInfo = New System.Web.HttpCookie("EMVSoft")
                    LoginInfo("Session_Id") = ""
                    LoginInfo("LoginID") = ""
                    LoginInfo("UserName") = ""
                    LoginInfo("LastLogin") = ""
                    LoginInfo("Group") = ""
                    LoginInfo("BranchCode") = ""
                    LoginInfo("BranchName") = ""
                    LoginInfo("Designation") = ""
                    LoginInfo("DataBaseName") = ""
                    LoginInfo("CompanyCode") = ""
                    LoginInfo.Expires = Now.AddDays(-12)
                    response.Cookies.Add(LoginInfo)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function get_SuperAdmin_SessionVariables(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "EMVSoft" Then
                        found = True
                        Exit For

                    End If
                Next
                If found = True Then
                    If VariableName = "Session_Id" Then
                        Return request.Cookies("EMVSoft").Values("Session_Id")
                    ElseIf VariableName = "LoginID" Then
                        Return request.Cookies("EMVSoft").Values("LoginID")
                    ElseIf VariableName = "UserName" Then
                        Return request.Cookies("EMVSoft").Values("UserName")
                    ElseIf VariableName = "LastLogin" Then
                        Return request.Cookies("EMVSoft").Values("LastLogin")
                    ElseIf VariableName = "Group" Then
                        Return request.Cookies("EMVSoft").Values("Group")
                    ElseIf VariableName = "Designation" Then
                        Return request.Cookies("EMVSoft").Values("Designation")
                    ElseIf VariableName = "ImagePath" Then
                        Return request.Cookies("EMVSoft").Values("ImagePath")
                    ElseIf VariableName = "DataBaseName" Then
                        Return request.Cookies("EMVSoft").Values("DataBaseName")
                    ElseIf VariableName = "CompanyCode" Then
                        Return request.Cookies("EMVSoft").Values("CompanyCode")
                    ElseIf VariableName = "BranchCode" Then
                        Return request.Cookies("EMVSoft").Values("BranchCode")
                    ElseIf VariableName = "BranchName" Then
                        Return request.Cookies("EMVSoft").Values("BranchName")
                    Else
                        Return ""
                    End If

                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function is_Admin_validSession(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As Boolean
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "AdminLoginInfo" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    If Not request.Cookies("AdminLoginInfo").Values("CompanyCode") = "" Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False

                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Expire_Admin_Session(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "AdminLoginInfo" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    LoginInfo = New System.Web.HttpCookie("AdminLoginInfo")
                    AdminLoginInfo("UserRole") = ""
                    AdminLoginInfo("BranchCode") = ""
                    AdminLoginInfo("BranchName") = ""
                    AdminLoginInfo("CompanyCode") = ""
                    AdminLoginInfo("LoginID") = ""
                    AdminLoginInfo("UserName") = ""
                    AdminLoginInfo("UserType") = ""
                    AdminLoginInfo("Designation") = ""
                    AdminLoginInfo("ProfilePic") = ""
                    AdminLoginInfo("LastLogin") = ""
                    AdminLoginInfo("ContactPerson") = ""
                    AdminLoginInfo("SessionId") = ""
                    AdminLoginInfo("ShowUserRightsPanel") = ""
                    AdminLoginInfo("TokenID") = ""
                    AdminLoginInfo("TokenTaskID") = ""
                    'AdminLoginInfo("BranchCode") = ""
                    'AdminLoginInfo("BranchName") = ""
                    'AdminLoginInfo("CompanyCode") = ""
                    'AdminLoginInfo("LoginID") = ""
                    'AdminLoginInfo("CompanyType") = ""
                    'AdminLoginInfo("LastLogin") = ""
                    'AdminLoginInfo("AccountStatus") = ""
                    'AdminLoginInfo("Companylogo") = ""
                    'AdminLoginInfo("Group") = ""
                    AdminLoginInfo.Expires = Now.AddDays(-12)
                    response.Cookies.Add(AdminLoginInfo)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function get_Admin_SessionVariables(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "AdminLoginInfo" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then

                    If VariableName = "CompanyCode" Then
                        Return request.Cookies("AdminLoginInfo").Values("CompanyCode")
                    ElseIf VariableName = "LoginID" Then
                        Return request.Cookies("AdminLoginInfo").Values("LoginID")


                    ElseIf VariableName = "UserType" Then
                        Return request.Cookies("AdminLoginInfo").Values("UserType")
                    ElseIf VariableName = "Designation" Then
                        Return request.Cookies("AdminLoginInfo").Values("Designation")
                    ElseIf VariableName = "ProfilePic" Then
                        Return request.Cookies("AdminLoginInfo").Values("ProfilePic")


                    ElseIf VariableName = "CompanyType" Then
                        Return request.Cookies("AdminLoginInfo").Values("CompanyType")
                    ElseIf VariableName = "LastLogin" Then
                        Return request.Cookies("AdminLoginInfo").Values("LastLogin")


                    ElseIf VariableName = "Group" Then
                        Return request.Cookies("AdminLoginInfo").Values("Group")

                    ElseIf VariableName = "AccountStatus" Then
                        Return request.Cookies("AdminLoginInfo").Values("AccountStatus")

                    ElseIf VariableName = "Companylogo" Then
                        Return request.Cookies("AdminLoginInfo").Values("Companylogo")

                    ElseIf VariableName = "BranchCode" Then
                        Return request.Cookies("AdminLoginInfo").Values("BranchCode")
                    ElseIf VariableName = "BranchName" Then
                        Return request.Cookies("AdminLoginInfo").Values("BranchName")
                    ElseIf VariableName = "LoggedInAs" Then
                        Return request.Cookies("AdminLoginInfo").Values("LoggedInAs")
                    ElseIf VariableName = "LastLogin" Then
                        Return request.Cookies("AdminLoginInfo").Values("LastLogin")
                    ElseIf VariableName = "UserName" Then
                        Return request.Cookies("AdminLoginInfo").Values("UserName")
                    ElseIf VariableName = "ContactPerson" Then
                        Return request.Cookies("AdminLoginInfo").Values("ContactPerson")
                    ElseIf VariableName = "SessionId" Then
                        Return request.Cookies("AdminLoginInfo").Values("SessionId")
                    ElseIf VariableName = "UserRole" Then
                        Return request.Cookies("AdminLoginInfo").Values("UserRole")
                    ElseIf VariableName = "DataBaseName" Then
                        Return request.Cookies("AdminLoginInfo").Values("DataBaseName")
                    ElseIf VariableName = "ShowUserRightsPanel" Then
                        Return request.Cookies("AdminLoginInfo").Values("ShowUserRightsPanel")

                    ElseIf VariableName = "TokenID" Then
                        Return request.Cookies("AdminLoginInfo").Values("TokenID")
                    ElseIf VariableName = "TokenTaskID" Then
                        Return request.Cookies("AdminLoginInfo").Values("TokenTaskID")
                    Else
                        Return ""
                    End If
                    'AdminLoginInfo("Session_Id")
                    'AdminLoginInfo("UserRole") = ""
                Else
                    Return ""

                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub Set_Admin_SessionVariables(ByVal VariableName As String, ByVal Variable_Value As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "AdminLoginInfo" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    AdminLoginInfo = New System.Web.HttpCookie("AdminLoginInfo")

                    AdminLoginInfo("CompanyCode") = get_Admin_SessionVariables("CompanyCode", request, response)
                    AdminLoginInfo("LoginID") = get_Admin_SessionVariables("LoginID", request, response)
                    AdminLoginInfo("UserType") = get_Admin_SessionVariables("UserType", request, response)
                    AdminLoginInfo("Designation") = get_Admin_SessionVariables("Designation", request, response)
                    AdminLoginInfo("ProfilePic") = get_Admin_SessionVariables("ProfilePic", request, response)
                    AdminLoginInfo("CompanyType") = get_Admin_SessionVariables("CompanyType", request, response)
                    AdminLoginInfo("LastLogin") = get_Admin_SessionVariables("LastLogin", request, response)
                    AdminLoginInfo("Group") = get_Admin_SessionVariables("Group", request, response)
                    AdminLoginInfo("AccountStatus") = get_Admin_SessionVariables("AccountStatus", request, response)
                    AdminLoginInfo("Companylogo") = get_Admin_SessionVariables("Companylogo", request, response)
                    AdminLoginInfo("BranchCode") = get_Admin_SessionVariables("BranchCode", request, response)
                    AdminLoginInfo("BranchName") = get_Admin_SessionVariables("BranchName", request, response)
                    AdminLoginInfo("LoggedInAs") = get_Admin_SessionVariables("LoggedInAs", request, response)
                    AdminLoginInfo("LastLogin") = get_Admin_SessionVariables("LastLogin", request, response)
                    AdminLoginInfo("UserName") = get_Admin_SessionVariables("UserName", request, response)
                    AdminLoginInfo("ContactPerson") = get_Admin_SessionVariables("ContactPerson", request, response)
                    AdminLoginInfo("SessionId") = get_Admin_SessionVariables("SessionId", request, response)
                    AdminLoginInfo("UserRole") = get_Admin_SessionVariables("UserRole", request, response)
                    AdminLoginInfo("DataBaseName") = get_Admin_SessionVariables("DataBaseName", request, response)
                    AdminLoginInfo("ShowUserRightsPanel") = get_Admin_SessionVariables("ShowUserRightsPanel", request, response)
                    AdminLoginInfo("TokenID") = get_Admin_SessionVariables("TokenID", request, response)
                    AdminLoginInfo("TokenTaskID") = get_Admin_SessionVariables("TokenTaskID", request, response)

                    If VariableName = "CompanyCode" Then
                        AdminLoginInfo("CompanyCode") = Variable_Value.Trim
                    ElseIf VariableName = "LoginID" Then
                        AdminLoginInfo("LoginID") = Variable_Value.Trim
                    ElseIf VariableName = "UserType" Then
                        AdminLoginInfo("UserType") = Variable_Value.Trim
                    ElseIf VariableName = "Designation" Then
                        AdminLoginInfo("Designation") = Variable_Value.Trim
                    ElseIf VariableName = "ProfilePic" Then
                        AdminLoginInfo("ProfilePic") = Variable_Value.Trim
                    ElseIf VariableName = "CompanyType" Then
                        AdminLoginInfo("CompanyType") = Variable_Value.Trim
                    ElseIf VariableName = "LastLogin" Then
                        AdminLoginInfo("LastLogin") = Variable_Value.Trim
                    ElseIf VariableName = "Group" Then
                        AdminLoginInfo("Group") = Variable_Value.Trim
                    ElseIf VariableName = "AccountStatus" Then
                        AdminLoginInfo("AccountStatus") = Variable_Value.Trim
                    ElseIf VariableName = "Companylogo" Then
                        AdminLoginInfo("Companylogo") = Variable_Value.Trim
                    ElseIf VariableName = "BranchCode" Then
                        AdminLoginInfo("BranchCode") = Variable_Value.Trim
                    ElseIf VariableName = "BranchName" Then
                        AdminLoginInfo("BranchName") = Variable_Value.Trim
                    ElseIf VariableName = "LoggedInAs" Then
                        AdminLoginInfo("LoggedInAs") = Variable_Value.Trim
                    ElseIf VariableName = "LastLogin" Then
                        AdminLoginInfo("LastLogin") = Variable_Value.Trim
                    ElseIf VariableName = "UserName" Then
                        AdminLoginInfo("UserName") = Variable_Value.Trim
                    ElseIf VariableName = "ContactPerson" Then
                        AdminLoginInfo("ContactPerson") = Variable_Value.Trim
                    ElseIf VariableName = "SessionId" Then
                        AdminLoginInfo("SessionId") = Variable_Value.Trim
                    ElseIf VariableName = "UserRole" Then
                        AdminLoginInfo("UserRole") = Variable_Value.Trim
                    ElseIf VariableName = "DataBaseName" Then
                        AdminLoginInfo("DataBaseName") = Variable_Value.Trim
                    ElseIf VariableName = "ShowUserRightsPanel" Then
                        AdminLoginInfo("ShowUserRightsPanel") = Variable_Value.Trim
                    ElseIf VariableName = "TokenID" Then
                        AdminLoginInfo("TokenID") = Variable_Value.Trim
                    ElseIf VariableName = "TokenTaskID" Then
                        AdminLoginInfo("TokenTaskID") = Variable_Value.Trim
                    End If

                    AdminLoginInfo.Expires = Now.AddHours(9)
                    response.Cookies.Add(AdminLoginInfo)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    '////////////  Start Admin Menu Cookie Functions ////////////////////////////////// 
    Public Function get_Admin_MenuCookie(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "Admin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then

                    If VariableName = "Selected_MainMenu" Then
                        Return request.Cookies("Admin_MenuCookie").Values("Selected_MainMenu")
                    ElseIf VariableName = "Selected_SubMenu" Then
                        Return request.Cookies("Admin_MenuCookie").Values("Selected_SubMenu")
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub Set_Admin_MenuCookie(ByVal Selected_MainMenu As String, ByVal Selected_SubMenu As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "Admin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    Admin_MenuCookie = New System.Web.HttpCookie("Admin_MenuCookie")

                    Admin_MenuCookie("Selected_MainMenu") = Selected_MainMenu.Trim
                    Admin_MenuCookie("Selected_SubMenu") = Selected_SubMenu.Trim

                    Admin_MenuCookie.Expires = Now.AddHours(9)
                    response.Cookies.Add(Admin_MenuCookie)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Expire_Admin_MenuCookie(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "Admin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    Admin_MenuCookie = New System.Web.HttpCookie("Admin_MenuCookie")
                    Admin_MenuCookie("Selected_MainMenu") = ""
                    Admin_MenuCookie("Selected_SubMenu") = ""
                    Admin_MenuCookie.Expires = Now.AddDays(-12)
                    response.Cookies.Add(Admin_MenuCookie)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    '////////////  End Admin Menu Cookie Functions /////////////////////////////////

    Public Function get_Customer_MenuCookie(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try
            If Not (request.Cookies("Selected_MainMenu")) Is Nothing Then
                Return request.Cookies("Selected_MainMenu").Value
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub Set_Customer_MenuCookie(ByVal Selected_MainMenu As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            response.Cookies("Selected_MainMenu").Expires = DateTime.Now.AddDays(30)
            response.Cookies("Selected_MainMenu").Value = Selected_MainMenu.ToString
        Catch ex As Exception

        End Try
    End Sub

    '////////////  Start SuperAdmin Menu Cookie Functions ////////////////////////////////// 

    Public Function get_SuperAdmin_MenuCookie(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SuperAdmin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then

                    If VariableName = "Selected_MainMenu" Then
                        Return request.Cookies("SuperAdmin_MenuCookie").Values("Selected_MainMenu")
                    ElseIf VariableName = "Selected_SubMenu" Then
                        Return request.Cookies("SuperAdmin_MenuCookie").Values("Selected_SubMenu")
                    ElseIf VariableName = "NavigationMenu" Then
                        Dim naviagtion As String = parseString(request.Cookies("SuperAdmin_MenuCookie").Values("NavigationMenu"))
                        Return naviagtion
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub Set_SuperAdmin_MenuCookie(ByVal Selected_MainMenu As String, ByVal Selected_SubMenu As String, ByVal NavigationMenu As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SuperAdmin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    SuperAdmin_MenuCookie = New System.Web.HttpCookie("SuperAdmin_MenuCookie")

                    SuperAdmin_MenuCookie("Selected_MainMenu") = Selected_MainMenu.Trim
                    SuperAdmin_MenuCookie("Selected_SubMenu") = Selected_SubMenu.Trim
                    SuperAdmin_MenuCookie("NavigationMenu") = NavigationMenu.Trim

                    SuperAdmin_MenuCookie.Expires = Now.AddHours(9)
                    response.Cookies.Add(SuperAdmin_MenuCookie)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Expire_SuperAdmin_MenuCookie(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SuperAdmin_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    SuperAdmin_MenuCookie = New System.Web.HttpCookie("SuperAdmin_MenuCookie")
                    SuperAdmin_MenuCookie("Selected_MainMenu") = ""
                    SuperAdmin_MenuCookie("Selected_SubMenu") = ""
                    SuperAdmin_MenuCookie.Expires = Now.AddDays(-12)
                    response.Cookies.Add(SuperAdmin_MenuCookie)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    '////////////  End SuperAdmin Menu Cookie Functions /////////////////////////////////

    '===========================================================================================================
    '===========================================================================================================

    'ManageAllCookies


    Public Function is_ManageAllCookies_validSession(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As Boolean
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "ManageAllCookies" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    If Not request.Cookies("ManageAllCookies").Values("Session_Id") = "" Then
                        Return True
                    End If
                Else
                    Return False

                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Expire_ManageAllCookies_Session(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "ManageAllCookies" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    ManageAllCookies = New System.Web.HttpCookie("ManageAllCookies")
                    ManageAllCookies("Session_Id") = ""
                    ManageAllCookies("Path") = ""
                    ManageAllCookies("ID") = ""
                    ManageAllCookies.Expires = Now.AddDays(-12)
                    response.Cookies.Add(ManageAllCookies)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function get_ManageAllCookies_SessionVariables(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "ManageAllCookies" Then
                        found = True
                        Exit For

                    End If
                Next
                If found = True Then
                    If VariableName = "Session_Id" Then
                        Return request.Cookies("ManageAllCookies").Values("Session_Id")
                    ElseIf VariableName = "Path" Then
                        Return request.Cookies("ManageAllCookies").Values("Path")
                    ElseIf VariableName = "ID" Then
                        Return request.Cookies("ManageAllCookies").Values("ID")

                    Else
                        Return ""
                    End If

                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Public Sub sendNewMail(ByVal msg As String, ByVal subject As String, ByVal sendTo As String)
        Try

            Dim mailAddr() As String
            Dim fromAddress As New MailAddress("support@businessonlinesolution.in", "Support BOS")
            smtpclient.Host = "us2.smtp.mailhostbox.com"
            smtpclient.Credentials = New System.Net.NetworkCredential("support@businessonlinesolution.in", "boscenter@123")
            smtpclient.EnableSsl = False
            smtpclient.Port = 25
            message.From = fromAddress

            mailAddr = Split(sendTo, ";")
            message.To.Clear()

            For i As Integer = 0 To mailAddr.Length - 1
                message.To.Add(New MailAddress(mailAddr(i)))
            Next

            message.Subject = subject
            message.IsBodyHtml = True
            message.Body = msg
            smtpclient.Send(message)


        Catch ex As Exception
        End Try
    End Sub

    Public Sub CreateMenuDynamic(ByVal Menu1 As Menu, ByVal Module_Master_TableName As String, ByVal UserRights_TableName As String, ByVal GroupOfUser As String, ByVal topmenu As Menu, ByVal User_id As String)
        Try
            Menu1.Visible = False

            Dim RefModule As String = ""
            Dim strqry As String = ""
            Dim result As [Boolean] = True

            For i As Integer = 0 To Menu1.Items.Count - 1
                Dim val As String = Menu1.Items(i).Text
                RefModule = val
                strqry = " " & Module_Master_TableName & " where MenuText='" & parseString(val) & "' and FormName='" & parseString(Menu1.Items(i).NavigateUrl.Trim()) & "'  and RefModule='" & parseString(RefModule) & "'"
                If Not (FL.RecCount(strqry) > 0) Then
                    result = False
                    Exit For
                End If

                If Menu1.Items(i).ChildItems.Count > 0 Then
                    For j As Integer = 0 To Menu1.Items(i).ChildItems.Count - 1
                        val = Menu1.Items(i).ChildItems(j).NavigateUrl
                        If Not String.IsNullOrEmpty(val.Trim()) Then
                            strqry = " " & Module_Master_TableName & " where MenuText='" & parseString(Menu1.Items(i).ChildItems(j).Text.Trim()) & "' and FormName='" & parseString(val) & "'  and RefModule='" & parseString(RefModule) & "'"
                            If Not (FL.RecCount(strqry) > 0) Then
                                result = False
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
            If result = False Then
                'delete from BDC_Module_Master

                strqry = "delete from " & Module_Master_TableName & " ; delete from " & UserRights_TableName & " where User_ID='Admin'"
                FL.DMLQueries(strqry)

                strqry = ""
                'insert into BDC_Module_Master from main menu

                For i As Integer = 0 To Menu1.Items.Count - 1
                    Dim val As String = Menu1.Items(i).Text
                    RefModule = val

                    If Not String.IsNullOrEmpty(val.Trim()) Then
                        If String.IsNullOrEmpty(strqry.Trim()) Then
                            strqry = "insert into " & Module_Master_TableName & "(MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('" & parseString(val) & "','" & parseString(Menu1.Items(i).NavigateUrl.Trim()) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','false','" & parseString(RefModule) & "')"
                            strqry = strqry & " ; insert into " & UserRights_TableName & "(User_ID,MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('Admin','" & parseString(val) & "','" & parseString(Menu1.Items(i).NavigateUrl.Trim()) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','true','" & parseString(RefModule) & "')"
                        Else
                            strqry = strqry & ";" & "insert into " & Module_Master_TableName & "(MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('" & parseString(val) & "','" & parseString(Menu1.Items(i).NavigateUrl.Trim()) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','false','" & parseString(RefModule) & "')"
                            strqry = strqry & " ; insert into " & UserRights_TableName & "(User_ID,MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('Admin','" & parseString(val) & "','" & parseString(Menu1.Items(i).NavigateUrl.Trim()) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','true','" & parseString(RefModule) & "')"
                        End If
                    End If

                    If Menu1.Items(i).ChildItems.Count > 0 Then
                        For j As Integer = 0 To Menu1.Items(i).ChildItems.Count - 1
                            val = Menu1.Items(i).ChildItems(j).NavigateUrl
                            If Not String.IsNullOrEmpty(val.Trim()) Then
                                If String.IsNullOrEmpty(strqry.Trim()) Then
                                    strqry = "insert into " & Module_Master_TableName & "(MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('" & parseString(Menu1.Items(i).ChildItems(j).Text.Trim()) & "','" & parseString(val) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','false','" & parseString(RefModule) & "')"
                                    strqry = strqry & " ; insert into " & UserRights_TableName & "(User_ID,MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('Admin','" & parseString(Menu1.Items(i).ChildItems(j).Text.Trim()) & "','" & parseString(val) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','true','" & parseString(RefModule) & "')"
                                Else
                                    strqry = strqry & ";" & "insert into " & Module_Master_TableName & "(MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('" & parseString(Menu1.Items(i).ChildItems(j).Text.Trim()) & "','" & parseString(val) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','false','" & parseString(RefModule) & "')"
                                    strqry = strqry & " ; insert into " & UserRights_TableName & "(User_ID,MenuText,FormName,UpdatedOn,UpdatedBy,FrmSelected,RefModule) values('Admin','" & parseString(Menu1.Items(i).ChildItems(j).Text.Trim()) & "','" & parseString(val) & "','" & DateTime.Now.ToString() & "','" & parseString(User_id) & "','true','" & parseString(RefModule) & "')"
                                End If
                            End If
                        Next
                    End If
                Next
                If Not String.IsNullOrEmpty(strqry.Trim()) Then
                    FL.DMLQueries(strqry)
                End If
            End If

            'Creating Menu according to users right
            Dim LocalDS As DataSet = Nothing
            LocalDS = New DataSet()

            LocalDS = FL.OpenDsWithSelectQuery("select * from " & UserRights_TableName & " where user_id='" & parseString(GroupOfUser) & "'")
            Dim menuitemindex As Integer = 0
            If (LocalDS.Tables(0).Rows.Count > 0) Then
                Menu1.Visible = False
            End If
            For i As Integer = 0 To LocalDS.Tables(0).Rows.Count - 1
                If LocalDS.Tables(0).Rows(i)("MenuText").ToString().Equals(LocalDS.Tables(0).Rows(i)("RefModule").ToString()) Then
                    topmenu.Items.Add(New MenuItem(LocalDS.Tables(0).Rows(i)("MenuText").ToString(), LocalDS.Tables(0).Rows(i)("MenuText").ToString(), Nothing, LocalDS.Tables(0).Rows(i)("FormName").ToString()))
                    RefModule = LocalDS.Tables(0).Rows(i)("MenuText").ToString()
                    menuitemindex += 1
                Else
                    If Not (menuitemindex - 1 < 0) Then
                        If (RefModule.ToUpper().Equals(LocalDS.Tables(0).Rows(i)("RefModule").ToString().ToUpper())) Then
                            Dim formName As String = LocalDS.Tables(0).Rows(i)("MenuText").ToString()
                            topmenu.Items(menuitemindex - 1).ChildItems.Add(New MenuItem(formName, formName, Nothing, LocalDS.Tables(0).Rows(i)("FormName").ToString()))
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub addSalutation(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim relation() As String = {"Mr.", "Mrs.", "Miss.", "MS", "Dr.", "OTHER"}
            For i As Integer = 0 To relation.Length - 1
                ddl.Items.Add(relation(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Function DefaultDatabase() As String
        Try
            Return "BosCenter_DB"
        Catch ex As Exception
            Return "BosCenter_DB"
        End Try
    End Function


    Public Sub addConsiderAs(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim type() As String = {"Inventory", "Scrapped", "Sold"}
            For i As Integer = 0 To type.Length - 1
                ddl.Items.Add(type(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub addCareOf(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim relation() As String = {"S/O", "D/O", "C/O", "Spouse"}
            For i As Integer = 0 To relation.Length - 1
                ddl.Items.Add(relation(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub addProof(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim relation() As String = {":::: Select ::::", "Driving License", " Passport No", "Aadhar Card No", "Voter ID", "Others"}
            For i As Integer = 0 To relation.Length - 1
                ddl.Items.Add(relation(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub AddAccountType(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim AccountType() As String = {":::: Select Account Type ::::", "Saving Account", "Current Account"}
            For i As Integer = 0 To AccountType.Length - 1
                ddl.Items.Add(AccountType(i))
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub addBussinessType(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim type() As String = {"Select Option", "General Store", "Insurance Agent", "Professional", "Retail Store", "Student", "Travel Agent", "Money Exchange", "WholeSaler", "Legal Services", "Financial Services", "Pancard Services", "IT Services", "Others"}
            For i As Integer = 0 To type.Length - 1
                ddl.Items.Add(type(i))
            Next
        Catch ex As Exception

        End Try
    End Sub



    Public Function RandomPaswrd() As String
        Dim finalString As String = ""
        Try
            Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            Dim stringChars = New Char(4) {}
            Dim random = New Random()
            For i As Integer = 0 To stringChars.Length - 1
                stringChars(i) = chars(random.[Next](chars.Length))
            Next
            finalString = New String(stringChars)
        Catch ex As Exception
        End Try
        Return finalString
    End Function

    Public Function RandomTransactionPin() As String
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
    Public Sub ExportToPdf(ByVal gridobj As GridView, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String)
        Try
            Dim TargetGridView As GridView = gridobj
            TargetGridView.AllowPaging = False
            TargetGridView.AllowSorting = False
            TargetGridView.AutoGenerateColumns = False

            'TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            While TargetGridView.Columns.Count > 0
                TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            End While

            TargetGridView.DataBind()

            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            TargetGridView.RenderControl(htmlTextWriter)

            Dim stringReader As New StringReader(stringWriter.ToString())
            Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim htmlparser As New HTMLWorker(Doc)
            PdfWriter.GetInstance(Doc, Response.OutputStream)

            Doc.Open()
            htmlparser.Parse(stringReader)
            Doc.Close()
            Response.Write(Doc)
            Response.[End]()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ExportToPdf_DivTag(ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String)
        Try
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            MyDiv.RenderControl(htmlTextWriter)

            Dim stringReader As New StringReader(stringWriter.ToString())
            Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim htmlparser As New HTMLWorker(Doc)
            PdfWriter.GetInstance(Doc, Response.OutputStream)

            Doc.Open()
            htmlparser.Parse(stringReader)
            Doc.Close()
            Response.Write(Doc)
            Response.[End]()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ExportToPdf_DivTag_HavingGridview(ByVal gridobj As GridView, ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String)
        Try
            Dim TargetGridView As GridView = gridobj
            TargetGridView.AllowPaging = False
            TargetGridView.AllowSorting = False
            TargetGridView.AutoGenerateColumns = True

            While TargetGridView.Columns.Count > 0
                TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            End While


            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            ' MyDiv.RenderControl(htmlTextWriter)
            TargetGridView.RenderControl(htmlTextWriter)

            Dim stringReader As New StringReader(stringWriter.ToString())
            Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim htmlparser As New HTMLWorker(Doc)
            PdfWriter.GetInstance(Doc, Response.OutputStream)

            Doc.Open()
            htmlparser.Parse(stringReader)
            Doc.Close()
            Response.Write(Doc)
            Response.[End]()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ExportToExcel(ByVal gridobj As GridView, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String)
        Try

            Dim TargetGridView As GridView = gridobj

            TargetGridView.AllowPaging = False
            TargetGridView.AllowSorting = False
            TargetGridView.AutoGenerateColumns = True

            'While TargetGridView.Columns.Count > 0
            '    TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            'End While
            'qry = "select * from CRM_AdminStateMaster where Country_Name='India' and CompanyCode='cmp10175' order by rid desc "
            'ds = FL.OpenDsWithSelectQuery(qry)
            'TargetGridView.DataSource = ds.Tables(0)
            'TargetGridView.DataBind()



            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            TargetGridView.RenderControl(htmlTextWriter)
            'MyDiv.RenderControl(htmlTextWriter)

            Response.Write(stringWriter.ToString())
            Response.[End]()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ExportToWord(ByVal gridobj As GridView, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String)
        Try

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition",
            "attachment;filename=" & ExportedFileName & ".doc")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-word"

            Dim TargetGridView As GridView = gridobj

            TargetGridView.AllowPaging = False
            TargetGridView.AllowSorting = False
            TargetGridView.AutoGenerateColumns = True



            While TargetGridView.Columns.Count > 0
                TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            End While

            'ds = FL.OpenDsWithSelectQuery(Qry)
            'TargetGridView.DataSource = ds.Tables(0)
            'TargetGridView.DataBind()


            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            TargetGridView.RenderControl(hw)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()


        Catch ex As Exception

        End Try
    End Sub

    Public Function GetAdvisorCategory() As Array
        Try
            Dim str() As String = {"DDS Advisor", "Investment Advisor"}
            Return str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetTotal_WeekOff_and_Holiday(ByVal DaysName As String, ByVal VMonthName As String, ByVal VYear As String) As Integer
        Dim result As Integer = 0
        Try
            If DaysName.Trim = "" Then
                Return result
            Else

                If DaysName.Trim.Contains(",") Then
                    Dim Days() As String = DaysName.Split(",")
                    For i As Integer = 0 To Days.Length - 1
                        Dim CountDays As Integer = Date.DaysInMonth(CInt(VYear), returnMonthNumber(VMonthName))

                        Dim dt As New DateTime
                        dt = CDate(returnMonthNumber(VMonthName) & "\" & "1" & "\" & VYear)
                        For j As Integer = 1 To CountDays
                            dt = dt.AddDays(1)
                            'If dt.DayOfWeek = DayOfWeek.Sunday Then
                            If dt.DayOfWeek.ToString.ToUpper = Days(i).ToUpper Then
                                result += 1
                            End If
                        Next

                    Next
                Else
                    Dim CountDays As Integer = Date.DaysInMonth(CInt(VYear), returnMonthNumber(VMonthName))

                    Dim dt As New DateTime
                    dt = CDate(returnMonthNumber(VMonthName) & "/" & "1" & "/" & VYear)
                    For j As Integer = 1 To CountDays
                        dt = dt.AddDays(1)
                        'If dt.DayOfWeek = DayOfWeek.Sunday Then
                        If dt.DayOfWeek.ToString.ToUpper = DaysName.ToUpper Then
                            result += 1
                        End If
                    Next
                End If


            End If

            Return result
        Catch ex As Exception
            Return result
        End Try
    End Function

    Public Sub GetWeeklyDay(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim WeeklyDay() As String = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}
            For i As Integer = 0 To WeeklyDay.Length - 1
                ddl.Items.Add(WeeklyDay(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub fill_MonthName_InShort_InDropDown(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim MonthNameInShort() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
            For i As Integer = 0 To MonthNameInShort.Length - 1
                ddl.Items.Add(MonthNameInShort(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Fill_MonthName_InLong_InDropDown(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim MonthInLong() As String = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}
            For i As Integer = 0 To MonthInLong.Length - 1
                ddl.Items.Add(MonthInLong(i))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetMonthName(ByVal MonthNumber As Integer) As String
        Dim monthname As String = ""
        Try
            If MonthNumber = 1 Then
                monthname = "January"
            ElseIf MonthNumber = 2 Then
                monthname = "February"
            ElseIf MonthNumber = 3 Then
                monthname = "March"
            ElseIf MonthNumber = 4 Then
                monthname = "April"
            ElseIf MonthNumber = 5 Then
                monthname = "May"
            ElseIf MonthNumber = 6 Then
                monthname = "June"
            ElseIf MonthNumber = 7 Then
                monthname = "july"
            ElseIf MonthNumber = 8 Then
                monthname = "August"
            ElseIf MonthNumber = 9 Then
                monthname = "September"
            ElseIf MonthNumber = 10 Then
                monthname = "October"
            ElseIf MonthNumber = 11 Then
                monthname = "November"
            ElseIf MonthNumber = 12 Then
                monthname = "December"
            End If

            Return monthname

        Catch ex As Exception
            Return monthname
        End Try

    End Function



    Public Function GetMonthReturnAlphabat(ByVal MonthNumber As Integer) As String
        Dim monthname As String = ""
        Try
            If MonthNumber = 1 Then
                monthname = "A"
            ElseIf MonthNumber = 2 Then
                monthname = "B"
            ElseIf MonthNumber = 3 Then
                monthname = "C"
            ElseIf MonthNumber = 4 Then
                monthname = "D"
            ElseIf MonthNumber = 5 Then
                monthname = "E"
            ElseIf MonthNumber = 6 Then
                monthname = "F"
            ElseIf MonthNumber = 7 Then
                monthname = "G"
            ElseIf MonthNumber = 8 Then
                monthname = "H"
            ElseIf MonthNumber = 9 Then
                monthname = "I"
            ElseIf MonthNumber = 10 Then
                monthname = "J"
            ElseIf MonthNumber = 11 Then
                monthname = "K"
            ElseIf MonthNumber = 12 Then
                monthname = "L"
            End If

            Return monthname

        Catch ex As Exception
            Return monthname
        End Try

    End Function


    Public Function returnMonthNumber(ByVal month As String) As Integer
        Try
            month = month.ToUpper

            If month = "JAN" Or month = "JANUARY" Then
                Return 1
            ElseIf month = "FEB" Or month = "FEBRUARY" Then
                Return 2
            ElseIf month = "MAR" Or month = "MARCH" Then
                Return 3
            ElseIf month = "APR" Or month = "APRIL" Then
                Return 4
            ElseIf month = "MAY" Or month = "MAY" Then
                Return 5
            ElseIf month = "JUN" Or month = "JUNE" Then
                Return 6
            ElseIf month = "JUL" Or month = "JULY" Then
                Return 7
            ElseIf month = "AUG" Or month = "AUGUST" Then
                Return 8
            ElseIf month = "SEP" Or month = "SEPTEMBER" Then
                Return 9
            ElseIf month = "OCT" Or month = "OCTOBER" Then
                Return 10
            ElseIf month = "NOV" Or month = "NOVEMBER" Then
                Return 11
            ElseIf month = "DEC" Or month = "DECEMBER" Then
                Return 12
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetIPAddress() As String
        Try
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(sIPAddress) Then
                Return context.Request.ServerVariables("REMOTE_ADDR")
            Else
                Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
                Return ipArray(0)
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''RD','DD-RD','FD','FD-MIS','Daily Deposit Account','Saving Account'
    Public Sub GetAccountType(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()

            Dim AccountType() As String = {"RD", "DD-RD", "FD", "FD-MIS", "Daily Deposit Account", "Saving Account"}
            For i As Integer = 0 To AccountType.Length - 1
                ddl.Items.Add(AccountType(i))
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetDepositFrequency(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim AccountType() As String = {"One Time", "Daily", "Weekly", "Fortnightly", "Monthly", "Bi-Monthly", "Quarterly", "Thrice Yearly", "Half Yearly", "Yearly"}
            For i As Integer = 0 To AccountType.Length - 1
                ddl.Items.Add(AccountType(i))
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetCompundFrequency(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim AccountType() As String = {"Daily", "Weekly", "Fortnightly", "Monthly", "Bi-Monthly", "Quarterly", "Thrice Yearly", "Half Yearly", "Yearly"}
            For i As Integer = 0 To AccountType.Length - 1
                ddl.Items.Add(AccountType(i))
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetOrderPlacingMode(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            Dim AccountType() As String = {"Select Mode", "BY HAND", "COURIER"}
            For i As Integer = 0 To AccountType.Length - 1
                ddl.Items.Add(AccountType(i))
            Next
        Catch ex As Exception
        End Try
    End Sub


    Public Sub ImportExcelsheet_Old(ByVal PrmPathExcelFile As String, ByVal DataGrid1 As GridView, ByVal sheetname As String, ByVal lstbox As ListBox)
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Try ''''''' Fetch Data from Excel
            '174.133.134.26
            Dim DtSet As System.Data.DataSet

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; " & "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 12.0;") ' Select the data from Sheet1 of the workbook.
            'Microsoft.ACE.OLEDB.12.0;
            'Excel 12.0 Xml;HDR=YES;IMEX=1
            MyConnection.Open()
            'MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & sheetname & "$]  ", MyConnection)
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$] ", MyConnection)

            'MyCommand.TableMappings.Add("Table")
            'where F1='MCX' and F2='FUTCOM  GOLD  04APR2009'
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ds = DtSet


            lstbox.Items.Clear()
            For i As Integer = 0 To DtSet.Tables(0).Columns.Count - 1
                lstbox.Items.Add(DtSet.Tables(0).Columns(i).ColumnName)
            Next

            DataGrid1.DataSource = DtSet.Tables(0)
            DataGrid1.DataBind()



            'Dim i As Integer
            'For i = 0 To DtSet.Tables(0).Columns.Count - 1
            '    MsgBox(DtSet.Tables(0).Columns(i).ColumnName)
            'Next


            MyConnection.Close()
        Catch ex As Exception
            MyConnection.Close()
        End Try
    End Sub

    Public Function ImportExcelsheet(ByVal PrmPathExcelFile As String, ByVal DataGrid1 As GridView, ByVal sheetname As String) As String
        Dim Result As String = ""
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Try ''''''' Fetch Data from Excel
            '174.133.134.26
            Dim DtSet As System.Data.DataSet

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; " & "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 12.0;") ' Select the data from Sheet1 of the workbook.
            'Microsoft.ACE.OLEDB.12.0;
            'Excel 12.0 Xml;HDR=YES;IMEX=1
            MyConnection.Open()
            'MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & sheetname & "$]  ", MyConnection)
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$] ", MyConnection)

            'MyCommand.TableMappings.Add("Table")
            'where F1='MCX' and F2='FUTCOM  GOLD  04APR2009'
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ds = DtSet



            'lstbox.Items.Clear()
            Dim IsColumnmatched As String = "NO"


            For j As Integer = 1 To DataGrid1.Columns.Count - 1
                IsColumnmatched = "NO"
                For i As Integer = 0 To DtSet.Tables(0).Columns.Count - 1

                    If DtSet.Tables(0).Columns(i).ColumnName.ToUpper = DataGrid1.Columns(j).ToString.ToUpper Then
                        'lstbox.Items.Add(DtSet.Tables(0).Columns(i).ColumnName)
                        IsColumnmatched = "YES"
                        Exit For
                    End If
                Next
                If IsColumnmatched = "NO" Then
                    Exit For
                End If
            Next


            If IsColumnmatched = "YES" Then
                DataGrid1.DataSource = DtSet.Tables(0)
                DataGrid1.DataBind()
                Result = ""
            Else
                Result = "Error"
            End If

            'Dim i As Integer
            'For i = 0 To DtSet.Tables(0).Columns.Count - 1
            '    MsgBox(DtSet.Tables(0).Columns(i).ColumnName)
            'Next


            MyConnection.Close()
            Return Result
        Catch ex As Exception
            Return Result = "Error"
            MyConnection.Close()
        End Try
    End Function

    Public Sub SendSMS_ForgotPassword(ByVal msg As String, ByVal mobileNo As String)
        Try
            If Not mobileNo = "" And Not msg.Trim = "" Then
                Dim request As HttpWebRequest
                Dim response As HttpWebResponse = Nothing
                Dim url As String

                url = "http://mainadmin.dove-sms.com/sendsms.jsp?user=Web369&password=edc123&mobiles=" & mobileNo & "&sms=" & msg & "&senderid=MEMIND"
                'http://onlinesmslogin.com/quicksms/api.php?username=mustyapp&password=123456&to=9999460472&from=BGTORD&message=hi
                'Solutions.
                request = DirectCast(WebRequest.Create(url), HttpWebRequest)
                response = DirectCast(request.GetResponse(), HttpWebResponse)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Public Sub Call_RefundAPI()
        Try
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse = Nothing
            Dim url As String

            url = "http://www.boscenter.in/api/recharge/?OrderId=123&status=Success&msg=Hi How are you"
            'Method 1 - get
            'request = DirectCast(WebRequest.Create(url), HttpWebRequest)
            'response = DirectCast(request.GetResponse(), HttpWebResponse)

            'Method 2 - get
            Dim client = New RestClient(url)
            Dim request1 = New RestRequest(Method.GET)
            Dim response1 As IRestResponse = client.Execute(request1)
            Dim Str1 As String = response1.Content
            Str1 = Str1.Trim

            MsgBox(Str1.ToString)

        Catch ex As Exception
        End Try
    End Sub


    Public Function CurrentFinancialYear(ByVal Year As String, ByVal Month As String) As String
        Dim Result As String = ""
        Try
            'Dim dt As Date = Now.Date
            'Dim dt2 As Date = New Date(Now.Date.Year, 3, 31)

            'If dt > dt2 Then
            '    Result = Now.Date.Year & "-" & Now.Date.Year + 1
            'Else
            '    Result = Now.Date.Year - 1 & "-" & Now.Date.Year
            'End If

            Dim dt As Date = New Date(CInt(Year), CInt(Month), 1)

            Dim dt2 As Date = New Date(CInt(Year), 3, 31)

            If dt > dt2 Then
                Result = CInt(Year) & "-" & CInt(Year) + 1
            Else
                Result = CInt(Year) - 1 & "-" & CInt(Year)
            End If

            Return Result
        Catch ex As Exception
            Return Result
        End Try
    End Function

    Public LocalDS As DataSet

    Public Function Verify_LoginTimeOut(ByVal loginId As String, ByVal CompanyCode As String, ByVal whosLogin As String) As Boolean
        Dim result As Boolean = True
        Try
            Dim str As String = ""
            If whosLogin = "Customer" Then
                str = "Select * from CRM_Login_Details where User_ID='" & loginId & "' and RecordStatus='Active' and AccountStatus='Active'"
            ElseIf whosLogin = "Employee" Then
                'str = "Select * from CRM_Admin_EmployeeRegistration where EmployeeId='" & loginId & "' and CompanyCode='" & CompanyCode & "'and RecordStatus='Active' and AccountStatus='Active'"
                str = "Select * from CRM_Login_Details where User_ID='" & loginId & "' and RecordStatus='Active' and AccountStatus='Active'"
            Else
                str = ""
            End If

            If Not str.Trim = "" Then
                LocalDS = New DataSet
                LocalDS = FL.OpenDsWithSelectQuery(str)
                If Not LocalDS Is Nothing Then
                    If LocalDS.Tables.Count > 0 Then
                        If LocalDS.Tables(0).Rows.Count > 0 Then
                            Dim FromAMPM, FromTime, ToTime, ToAmPm, CurrentAMPM, CurrentTime As String
                            Dim FromTimeCom() As String = Split(parseString(LocalDS.Tables(0).Rows(0).Item("Fromtime")), "-")
                            Dim ToTimeCom() As String = Split(parseString(LocalDS.Tables(0).Rows(0).Item("Totime")), "-")

                            If FromTimeCom.Length > 0 And ToTimeCom.Length > 0 Then
                                FromTime = FromTimeCom(0).Replace(":", ".")
                                FromAMPM = FromTimeCom(1)

                                ToTime = ToTimeCom(0).Replace(":", ".")
                                ToAmPm = ToTimeCom(1)

                                If Now.Hour >= 12 Then
                                    CurrentAMPM = "PM"
                                    If Now.Minute < 10 Then
                                        CurrentTime = (CInt(Now.Hour) - 12) & ".0" & Now.Minute
                                    Else
                                        CurrentTime = (CInt(Now.Hour) - 12) & "." & Now.Minute
                                    End If

                                Else
                                    CurrentAMPM = "AM"
                                    If Now.Minute < 10 Then
                                        CurrentTime = Now.Hour & ".0" & Now.Minute
                                    Else
                                        CurrentTime = Now.Hour & "." & Now.Minute
                                    End If
                                    'CurrentTime = Now.Hour & "." & Now.Minute
                                End If


                                If FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "PM" Then
                                    result = False
                                ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "AM" Then
                                    If Not ((CDec(CurrentTime) >= CDec(FromTime)) And (CDec(CurrentTime) <= CDec(ToTime))) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "AM" Then
                                    result = False
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "PM" Then
                                    If Not ((CDec(CurrentTime) >= CDec(FromTime)) And (CDec(CurrentTime) <= CDec(ToTime))) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "AM" Then
                                    If CDec(FromTime) >= CDec(CurrentTime) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "AM" And ToAmPm.ToUpper = "PM" And CurrentAMPM = "PM" Then
                                    If CDec(CurrentTime) >= CDec(ToTime) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "PM" Then
                                    If CDec(FromTime) >= CDec(CurrentTime) Then
                                        result = False
                                    End If
                                ElseIf FromAMPM.ToUpper = "PM" And ToAmPm.ToUpper = "AM" And CurrentAMPM = "AM" Then
                                    If CDec(ToTime) <= CDec(CurrentTime) Then
                                        result = False
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Return result
        Catch ex As Exception
            Return result
        End Try
    End Function
    Public Function Verify_AccountStatus(ByVal loginId As String, ByVal dbName As String, ByVal whosLogin As String) As Boolean
        Try
            If whosLogin.Trim.ToUpper = "Distributor".Trim.ToUpper Or whosLogin.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or whosLogin.Trim.ToUpper = "Retailer".Trim.ToUpper Or whosLogin.Trim.ToUpper = "Customer".Trim.ToUpper Then
                If FL.RecCount(" " & dbName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & parseString(loginId) & "' and ActiveStatus='Active'") > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                If FL.RecCount(" " & dbName & ".dbo.CRM_Login_Details where User_ID='" & parseString(loginId) & "' and AccountStatus='Active' ") > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Return True
        End Try
    End Function

    Public Function Verify_IPAddress(ByVal loginId As String, ByVal CompanyCode As String, ByVal whosLogin As String) As Boolean
        Dim result As Boolean = True
        Try

            '''''''' Start IP Login Logic
            Dim DSLogin_IPAddress As New DataSet
            Dim IPAddresses() As String
            Dim CurrentIPAddress As String = GetIPAddress()
            Dim QryLogin_IPAddress As String = ""
            'If whosLogin = "SuperAdmin" Then
            '    Str = " CRM_Login_Details where User_ID='" & loginId & "' and RecordStatus='Active' and AccountStatus='Active'"
            'Else
            If whosLogin = "Employee" Then
                QryLogin_IPAddress = "select Login_IPAddress from CRM_Admin_BranchMaster where Code=(select BranchCode from CRM_Admin_EmployeeRegistration where EmployeeId='" & loginId & "' and CompanyCode='" & CompanyCode & "' ) and CompanyCode='" & CompanyCode & "'"
            ElseIf whosLogin = "Customer" Then
                '  QryLogin_IPAddress = "CRM_Login_Details where User_ID='" & loginId & "' and RecordStatus='Active' and AccountStatus='Active'"
            Else
                QryLogin_IPAddress = ""
            End If


            If Not QryLogin_IPAddress.Trim = "" Then
                DSLogin_IPAddress = FL.OpenDsWithSelectQuery(QryLogin_IPAddress)
                If DSLogin_IPAddress.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(DSLogin_IPAddress.Tables(0).Rows(0).Item("Login_IPAddress")) Then

                        If Not parseString(DSLogin_IPAddress.Tables(0).Rows(0).Item("Login_IPAddress").ToString.Trim) = "" Then
                            IPAddresses = DSLogin_IPAddress.Tables(0).Rows(0).Item("Login_IPAddress").ToString.Split(",")
                            If IPAddresses.Length > 1 Then
                                Dim loopResult As Boolean = False
                                For i As Integer = 0 To IPAddresses.Length - 1
                                    If CurrentIPAddress.Trim.ToString.ToUpper = IPAddresses(i).Trim.ToString.ToUpper Then
                                        loopResult = True
                                        Exit For
                                    End If
                                Next
                                If loopResult = True Then
                                    result = True
                                Else
                                    result = False
                                End If
                            ElseIf IPAddresses.Length = 1 Then

                                If IPAddresses(0).Trim = "" Then
                                    result = True
                                Else
                                    If CurrentIPAddress.Trim.ToString.ToUpper = IPAddresses(0).Trim.ToString.ToUpper Then
                                        result = True
                                    Else
                                        result = False
                                    End If
                                End If

                            Else
                                result = True
                            End If

                        Else
                            result = True
                            Return result
                            Exit Function
                        End If
                    Else
                        result = True
                        Return result
                        Exit Function
                    End If
                End If





                Return result
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function ReturnAge(ByVal DOB As Date) As Integer
        Dim age As Integer = 0
        Try
            Dim currentdate As Date = FL.returnDateMonthWiseWithDateChecking(Now.Date.ToString("dd/MM/yyyy"))
            age = DateDiff(DateInterval.Month, DOB, currentdate)
            If age > 0 Then

                Dim val As String = (age / 12)
                If val.Contains(".") Then
                    Dim valarr() As String = val.Split(".")
                    age = valarr(0).Trim
                Else
                    If currentdate.Day <= DOB.Day Then
                        age = val
                    Else
                        age = CInt(val) - 1
                    End If

                End If
            End If

            Return age
        Catch ex As Exception
            Return age
        End Try
    End Function



    Public Sub SendSMS(ByVal SMSType As String, ByVal MobileNo As String, Optional ByVal Salutation As String = "", Optional ByVal MemberName As String = "", Optional ByVal MemberID As String = "", Optional ByVal BranchName As String = "", Optional ByVal CompanyCode As String = "", Optional ByVal AdvisorID As String = "", Optional ByVal AccountNumber As String = "", Optional ByVal AccountType As String = "", Optional ByVal AccountOpenAmount As String = "", Optional ByVal ReceiptNumber As String = "", Optional ByVal ReceiptDate As String = "", Optional ByVal nextDueDate As String = "", Optional ByVal DepositAmount As String = "", Optional ByVal BalanceAmount As String = "")
        Try

            If parseString(MobileNo.Trim) = "" Or Not parseString(MobileNo.Trim).Length = 10 Or Not IsNumeric(parseString(MobileNo.Trim)) = True Then
                Exit Sub
            End If

            Dim message As String = ""
            If SMSType.Trim.ToUpper = "Register a Member".ToUpper Then
                message = "Congratulation !  " & Salutation & "  " & MemberName & ". Now you are the member of " & BranchName & " Branch. Your member ID is " & MemberID & " ."
            ElseIf SMSType.Trim.ToUpper = "Register an Advisor".ToUpper Then
                message = "Congratulation !  " & Salutation & "  " & MemberName & ". Now you are the Advisor of " & BranchName & " Branch. Your Advisor ID is " & AdvisorID & " ."
            ElseIf SMSType.Trim.ToUpper = "Register an Account".ToUpper Then
                message = "Congratulations and thanks on your investment with Branch " & BranchName & " !! We are really excited that your " & AccountType & " (plan) worth Rs " & AccountOpenAmount & "  is successfully submitted.."
            ElseIf SMSType.Trim.ToUpper = "Deposit Renewal".ToUpper Then
                message = "Thanks to deposited the installment (" & ReceiptNumber & ") of AccountNumber=" & AccountNumber & " on dated " & CDate(ReceiptDate).ToString("dd/MM/yyyy") & ", Installment Amount Received Rs." & DepositAmount & ", Your investment plans next due date is " & CDate(nextDueDate).ToString("dd/MM/yyyy") & "."
            ElseIf SMSType.Trim.ToUpper = "Deposit Amount".ToUpper Then

                message = "" & Salutation & "  " & MemberName & "  your account (" & AccountNumber & ") has been Credited by the Receipt No. " & ReceiptNumber & ". Amount Rs. " & DepositAmount & " only. Current balance is Rs. " & BalanceAmount & ". "

            ElseIf SMSType.Trim.ToUpper = "Withdrawal Amount".ToUpper Then
                message = "" & Salutation & "  " & MemberName & "  your account (" & AccountNumber & ") has been Debited by the Receipt No. " & ReceiptNumber & ". Amount Rs. " & DepositAmount & " only. Current balance is Rs. " & BalanceAmount & ". "
            End If




            If Not message.Trim = "" And Not MobileNo.Trim = "" And Not CompanyCode.Trim = "" Then

                Dim client As New WebClient()
                Dim baseurl As String = ("http://sms.asputility.com/app/smsapi/index.php?key=2586618609DF8B&routeid=370&type=text&contacts=" + MobileNo & "&senderid=OLIVES&msg=" + message & "")
                'http://onlinesmslogin.com/quicksms/api.php?username=suresh&password=123456&to=9999460472&from=OSILIN&message=hi
                Dim data As Stream = client.OpenRead(baseurl)
                Dim reader As New StreamReader(data)
                Dim s As String = reader.ReadToEnd()
                data.Close()
                reader.Close()

                Dim msgQRY As String = ""
                Dim msgCompanyCode As String = CompanyCode.Trim

                If SMSType.Trim.ToUpper = "Register a Member".ToUpper Then
                    msgQRY = "update CRM_Admin_MemberRegisteration set isRegMessageSent='Yes'   where MemberID='" & MemberID.Trim & "' and CompanyCode='" & msgCompanyCode & "';"
                ElseIf SMSType.Trim.ToUpper = "Register an Advisor".ToUpper Then
                    msgQRY = "update CRM_Admin_AdvisorRegistration set isRegMessageSent='Yes'   where AdvisorID='" & AdvisorID.Trim & "' and CompanyCode='" & msgCompanyCode & "';"
                ElseIf SMSType.Trim.ToUpper = "Register an Account".ToUpper Then
                    msgQRY = "update CRM_Admin_Account_Registration set isRegMessageSent='Yes'   where AccountNumber='" & AccountNumber.Trim & "' and CompanyCode='" & msgCompanyCode & "';"
                ElseIf SMSType.Trim.ToUpper = "Deposit Renewal".ToUpper Then
                    msgQRY = "update CRM_Admin_PaymentReceiptMaster set isRegMessageSent='Yes'   where AccountNumber='" & AccountNumber.Trim & "' and ReceiptNumber in (" & ReceiptNumber & ") and CompanyCode='" & msgCompanyCode & "';"
                ElseIf SMSType.Trim.ToUpper = "Deposit Amount".ToUpper Then
                    msgQRY = "update CRM_Admin_SavAccTrans_Master set isRegMessageSent='Yes'   where AccountNumber='" & AccountNumber.Trim & "' and TransactionNumber in (" & ReceiptNumber & ")  and CompanyCode='" & msgCompanyCode & "';"
                ElseIf SMSType.Trim.ToUpper = "Withdrawal Amount".ToUpper Then
                    msgQRY = "update CRM_Admin_SavAccTrans_Master set isRegMessageSent='Yes'   where AccountNumber='" & AccountNumber.Trim & "' and TransactionNumber in (" & ReceiptNumber & ") and CompanyCode='" & msgCompanyCode & "';"
                End If


                If Not msgQRY.Trim = "" Then
                    FL.DMLQueriesBulk(msgQRY)
                End If


            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Function GetFinancialYear() As String
        Dim result As String = ""
        Try
            'result = Now.Date.Year

            ds = New DataSet
            ds = FL.OpenDsWithSelectQuery("SELECT YEAR(getdate()) as year")
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        'ds.Tables(0).Rows(0)

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                            If Not ds.Tables(0).Rows(0).Item("year").ToString = "" Then
                                result = CInt(ds.Tables(0).Rows(0).Item("year").ToString.Trim)
                            End If
                        End If

                    End If
                End If
            End If

            result = CInt(result - 1) & "-" & (result)

        Catch ex As Exception

        End Try
        Return result
    End Function

    Public Function GetPrevious_FinancialYear() As String
        Dim result As String = ""
        Try
            'result = Now.Date.Year

            ds = New DataSet
            ds = FL.OpenDsWithSelectQuery("SELECT YEAR(getdate())-1 as year")
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        'ds.Tables(0).Rows(0)

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                            If Not ds.Tables(0).Rows(0).Item("year").ToString = "" Then
                                result = CInt(ds.Tables(0).Rows(0).Item("year").ToString.Trim)
                            End If
                        End If

                    End If
                End If
            End If

            result = CInt(result - 1) & "-" & (result)

        Catch ex As Exception

        End Try
        Return result
    End Function


    Public Sub ExportToExcel_New(ByVal gridobj As GridView, ByVal Response As System.Web.HttpResponse, ByVal MyDiv As Object, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Dim TargetGridView As GridView = gridobj

            TargetGridView.AllowPaging = False
            TargetGridView.AllowSorting = False
            If Type.Trim.ToUpper = "dyanamic".ToUpper Then
                TargetGridView.AutoGenerateColumns = True
            Else
                TargetGridView.AutoGenerateColumns = False
            End If


            'While TargetGridView.Columns.Count > 0
            '    TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            'End While

            ds = FL.OpenDsWithSelectQuery(Qry)
            TargetGridView.DataSource = ds.Tables(0)
            TargetGridView.DataBind()

            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            TargetGridView.RenderControl(htmlTextWriter)
            'MyDiv.RenderControl(htmlTextWriter)

            Response.Write(stringWriter.ToString())
            Response.[End]()

        Catch ex As Exception

        End Try
    End Sub


    Public Sub ExportToPdf_New(ByVal gridobj As GridView, ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try
            Dim TargetGridView As GridView = gridobj
            TargetGridView.AllowPaging = False
            TargetGridView.AllowSorting = False
            If Type.Trim.ToUpper = "dyanamic".ToUpper Then
                TargetGridView.AutoGenerateColumns = True
            Else
                TargetGridView.AutoGenerateColumns = False
            End If

            'While TargetGridView.Columns.Count > 0
            '    TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            'End While

            ds = FL.OpenDsWithSelectQuery(Qry)
            TargetGridView.DataSource = ds.Tables(0)
            TargetGridView.DataBind()

            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            ' MyDiv.RenderControl(htmlTextWriter)
            TargetGridView.RenderControl(htmlTextWriter)

            Dim stringReader As New StringReader(stringWriter.ToString())
            Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim htmlparser As New HTMLWorker(Doc)
            PdfWriter.GetInstance(Doc, Response.OutputStream)

            Doc.Open()
            htmlparser.Parse(stringReader)
            Doc.Close()
            Response.Write(Doc)
            Response.[End]()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ExportToWord_New(ByVal gridobj As GridView, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition",
            "attachment;filename=" & ExportedFileName & ".doc")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-word "

            Dim TargetGridView As GridView = gridobj

            TargetGridView.AllowPaging = False
            TargetGridView.AllowSorting = False
            If Type.Trim.ToUpper = "dyanamic".ToUpper Then
                TargetGridView.AutoGenerateColumns = True
            Else
                TargetGridView.AutoGenerateColumns = False
            End If



            'While TargetGridView.Columns.Count > 0
            '    TargetGridView.Columns.Remove(TargetGridView.Columns(0))
            'End While

            ds = FL.OpenDsWithSelectQuery(Qry)
            TargetGridView.DataSource = ds.Tables(0)
            TargetGridView.DataBind()


            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            TargetGridView.RenderControl(hw)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()


        Catch ex As Exception

        End Try
    End Sub



    Public Sub ExportToExcel_DivNew(ByVal gridobj As GridView, ByVal Response As System.Web.HttpResponse, ByVal MyDiv As Object, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            'TargetGridView.RenderControl(htmlTextWriter)
            MyDiv.RenderControl(htmlTextWriter)

            Response.Write(stringWriter.ToString())
            Response.[End]()

        Catch ex As Exception

        End Try
    End Sub


    Public Sub ExportToExcel_DivNewWithoutGrid(ByVal Response As System.Web.HttpResponse, ByVal MyDiv As Object, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            'TargetGridView.RenderControl(htmlTextWriter)
            MyDiv.RenderControl(htmlTextWriter)

            Response.Write(stringWriter.ToString())
            Response.[End]()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ExportToPdf_DivNew(ByVal gridobj As GridView, ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            MyDiv.RenderControl(htmlTextWriter)


            Dim stringReader As New StringReader(stringWriter.ToString())
            Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim htmlparser As New HTMLWorker(Doc)
            PdfWriter.GetInstance(Doc, Response.OutputStream)

            Doc.Open()
            htmlparser.Parse(stringReader)

            Doc.Close()
            Response.Write(Doc)
            Response.[End]()


        Catch ex As Exception
        End Try
    End Sub

    '///////////////////////

    Public Sub ExportToPdf_DivNew1(ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            MyDiv.RenderControl(htmlTextWriter)


            Dim stringReader As New StringReader(stringWriter.ToString())
            Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim htmlparser As New HTMLWorker(Doc)
            PdfWriter.GetInstance(Doc, Response.OutputStream)

            Doc.Open()
            htmlparser.Parse(stringReader)

            Doc.Close()
            Response.Write(Doc)
            Response.[End]()


        Catch ex As Exception
        End Try
    End Sub

    '///////////////////////////






    Public Sub ExportToPdf_DivNewTesting(ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & ExportedFileName & ".pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Dim stringWriter As New StringWriter()
            Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
            MyDiv.RenderControl(htmlTextWriter)


            Dim stringReader As New StringReader(stringWriter.ToString())
            Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim htmlparser As New HTMLWorker(Doc)
            PdfWriter.GetInstance(Doc, Response.OutputStream)

            Doc.Open()
            htmlparser.Parse(stringReader)

            Doc.Close()
            Response.Write(Doc)
            Response.[End]()


        Catch ex As Exception
        End Try
    End Sub
    Public Sub ExportToWord_DivNew(ByVal gridobj As GridView, ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition",
            "attachment;filename=" & ExportedFileName & ".doc")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-word "

            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            MyDiv.RenderControl(hw)

            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()


        Catch ex As Exception

        End Try
    End Sub
    Public Sub ExportToWord_DivNewWithouGrid(ByVal MyDiv As Object, ByVal Response As System.Web.HttpResponse, ByVal ExportedFileName As String, ByVal Qry As String, ByVal Type As String)
        Try

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition",
            "attachment;filename=" & ExportedFileName & ".doc")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-word "

            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            MyDiv.RenderControl(hw)

            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()


        Catch ex As Exception

        End Try
    End Sub


    '//////////////////////////////////client Cookies start ///////////////////////////////////////////////////////////////////

    Public Function is_Client_validSession(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As Boolean
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SecureClient_Cookie" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then
                    If Not request.Cookies("SecureClient_Cookie").Values("LoginID") = "" Then
                        Return True
                    End If
                Else
                    Return False

                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Expire_Client_Session(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SecureClient_Cookie" Then
                        found = True
                        Exit For
                    End If
                Next
                If found = True Then



                    SecureClient_Cookie = New System.Web.HttpCookie("SecureClient_Cookie")
                    SecureClient_Cookie("UserName") = ""
                    SecureClient_Cookie("LastLogin") = ""
                    SecureClient_Cookie("Group") = ""
                    SecureClient_Cookie("ImagePath") = ""
                    SecureClient_Cookie("DataBaseName") = ""
                    'AdminLoginInfo("UserName") = ""
                    'AdminLoginInfo("UserType") = ""
                    'AdminLoginInfo("ProfilePic") = ""
                    'AdminLoginInfo("LastLogin") = ""
                    'AdminLoginInfo("ContactPerson") = ""
                    'AdminLoginInfo("SessionId") = ""
                    'AdminLoginInfo("ShowUserRightsPanel") = ""
                    'AdminLoginInfo("TokenID") = ""
                    'AdminLoginInfo("TokenTaskID") = ""
                    'AdminLoginInfo("BranchCode") = ""
                    'AdminLoginInfo("BranchName") = ""
                    'AdminLoginInfo("CompanyCode") = ""
                    'AdminLoginInfo("LoginID") = ""
                    'AdminLoginInfo("CompanyType") = ""
                    'AdminLoginInfo("LastLogin") = ""
                    'AdminLoginInfo("AccountStatus") = ""
                    'AdminLoginInfo("Companylogo") = ""
                    'AdminLoginInfo("Group") = ""
                    SecureClient_Cookie.Expires = Now.AddDays(-12)
                    response.Cookies.Add(SecureClient_Cookie)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function get_Client_SessionVariables(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try
            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SecureClient_Cookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then

                    If VariableName = "UserName" Then
                        Return request.Cookies("SecureClient_Cookie").Values("UserName")
                    ElseIf VariableName = "LoginID" Then
                        Return request.Cookies("SecureClient_Cookie").Values("LoginID")
                    ElseIf VariableName = "DeviceID" Then
                        Return request.Cookies("SecureClient_Cookie").Values("DeviceID")
                    ElseIf VariableName = "LastLogin" Then
                        Return request.Cookies("SecureClient_Cookie").Values("LastLogin")
                    ElseIf VariableName = "Group" Then
                        Return request.Cookies("SecureClient_Cookie").Values("Group")
                    ElseIf VariableName = "ImagePath" Then
                        Return request.Cookies("SecureClient_Cookie").Values("ImagePath")
                    ElseIf VariableName = "DataBaseName" Then
                        Return request.Cookies("SecureClient_Cookie").Values("DataBaseName")
                    ElseIf VariableName = "Device_ID" Then
                        Return request.Cookies("SecureClient_Cookie").Values("Device_ID")
                    ElseIf VariableName = "LoggedInAs" Then
                        Return request.Cookies("SecureClient_Cookie").Values("LoggedInAs")
                    End If

                Else
                    Return Nothing

                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Sub Set_Client_SessionVariables(ByVal VariableName As String, ByVal Variable_Value As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SecureClient_Cookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    AdminLoginInfo = New System.Web.HttpCookie("SecureClient_Cookie")



                    SecureClient_Cookie("UserName") = get_Client_SessionVariables("UserName", request, response)
                    SecureClient_Cookie("LoginID") = get_Client_SessionVariables("LoginID", request, response)
                    SecureClient_Cookie("DeviceID") = get_Client_SessionVariables("DeviceID", request, response)
                    SecureClient_Cookie("LastLogin") = get_Client_SessionVariables("LastLogin", request, response)
                    SecureClient_Cookie("Group") = get_Client_SessionVariables("Group", request, response)
                    SecureClient_Cookie("ImagePath") = get_Client_SessionVariables("ImagePath", request, response)
                    SecureClient_Cookie("DataBaseName") = get_Client_SessionVariables("DataBaseName", request, response)
                    SecureClient_Cookie("LoggedInAs") = get_Client_SessionVariables("LoggedInAs", request, response)

                    If VariableName = "UserName" Then
                        SecureClient_Cookie("UserName") = Variable_Value.Trim


                    ElseIf VariableName = "LoginID" Then
                        SecureClient_Cookie("LoginID") = Variable_Value.Trim
                    ElseIf VariableName = "DeviceID" Then
                        SecureClient_Cookie("DeviceID") = Variable_Value.Trim
                    ElseIf VariableName = "LastLogin" Then
                        SecureClient_Cookie("LastLogin") = Variable_Value.Trim
                    ElseIf VariableName = "Group" Then
                        SecureClient_Cookie("Group") = Variable_Value.Trim
                    ElseIf VariableName = "ImagePath" Then
                        SecureClient_Cookie("ImagePath") = Variable_Value.Trim
                    ElseIf VariableName = "DataBaseName" Then
                        SecureClient_Cookie("DataBaseName") = Variable_Value.Trim
                    ElseIf VariableName = "LoggedInAs" Then
                        SecureClient_Cookie("LoggedInAs") = Variable_Value.Trim

                    End If

                    SecureClient_Cookie.Expires = Now.AddHours(9)
                    response.Cookies.Add(SecureClient_Cookie)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    '////////////  Start Admin Menu Cookie Functions ///////////////////////////////////////////////////////////////////// 
    Public Function get_client_MenuCookie(ByVal VariableName As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse) As String
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then
                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SecureClient_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then

                    If VariableName = "SecureClient_MainMenu" Then
                        Return request.Cookies("SecureClient_MenuCookie").Values("SecureClient_MainMenu")
                    ElseIf VariableName = "SecureClient_SubMenu" Then
                        Return request.Cookies("SecureClient_MenuCookie").Values("SecureClient_SubMenu")
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Sub Set_client_MenuCookie(ByVal Selected_MainMenu As String, ByVal Selected_SubMenu As String, ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SecureClient_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    SecureClient_MenuCookie = New System.Web.HttpCookie("SecureClient_MenuCookie")

                    SecureClient_MenuCookie("SecureClient_MainMenu") = Selected_MainMenu.Trim
                    SecureClient_MenuCookie("SecureClient_SubMenu") = Selected_SubMenu.Trim

                    SecureClient_MenuCookie.Expires = Now.AddHours(9)
                    response.Cookies.Add(SecureClient_MenuCookie)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Expire_client_MenuCookie(ByVal request As HttpRequest, ByVal response As System.Web.HttpResponse)
        Try

            Dim found As Boolean = False
            If request.Cookies.Count > 0 Then

                For i As Integer = 0 To request.Cookies.Count - 1
                    If request.Cookies(i).Name = "SecureClient_MenuCookie" Then
                        found = True
                        Exit For
                    End If
                Next

                If found = True Then
                    SecureClient_MenuCookie = New System.Web.HttpCookie("SecureClient_MenuCookie")
                    SecureClient_MenuCookie("SecureClient_MainMenu") = ""
                    SecureClient_MenuCookie("SecureClient_SubMenu") = ""
                    SecureClient_MenuCookie.Expires = Now.AddDays(-12)
                    response.Cookies.Add(SecureClient_MenuCookie)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    '''''////////////////////////////////////////////  End Client Cookies///////////////////////////////////////////////////////


    Public Function get_SMS_API_Url(ContactNos As String, txtMessage As String) As String
        Try

            Dim senderid As String = ""
            '  senderid = "KalpTC"
            Dim API() As String = FL.AddInVar("SMSAPI+'*'+SMSSenderId", "AutoNumber").Split("*")
            Dim API_Url As String = API(0)
            senderid = API(1)

            Dim output As String = System.String.Format(API_Url, senderid, ContactNos, txtMessage)

            Return output
            'Return "http://bulk.mdsms.in/api/mt/SendSMS?user=kalptree&password=kalptree&senderid=KalpTC&channel=Trans&DCS=0&flashsms=0&number=" + ContactNos + "&text=" + txtMessage + "&route=1049"
            'Return "http://mobicomm.dove-sms.com//submitsms.jsp?user=Justlook&key=4cd19c6053XX&mobile=" + ContactNos + "&message=" + txtMessage + " sms&senderid=infBOS&accusage=1"
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function get_SMS_API_Url1(ContactNos As String, txtMessage As String) As String
        Try
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse = Nothing
            'Dim url As String = "http://bulk.mdsms.in/api/mt/SendSMS?user=kalptree&password=kalptree&senderid=KalpTC&channel=Trans&DCS=0&flashsms=0&number=" + ContactNos + "&text=" + txtMessage + "&route=1049"
            Dim url As String = "http://bulk.mdsms.in:7412/api/mt/SendSMS?user=Kalptree&password=Kalptree&senderid=KalpTC&channel=Trans&DCS=0&flashsms=0&number=" + ContactNos + "&text=" + txtMessage + ""
            request = DirectCast(WebRequest.Create(url), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function sendSMSThroughAPI(ByVal MobileNos As String, txtMessage As String) As String
        Dim val As String = "Success"
        Try
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse = Nothing
            If Not txtMessage = "" Then
                ' txtMessage = txtMessage.Replace("....", "")
                Dim client As New WebClient()
                Dim baseurl As String = (get_SMS_API_Url(MobileNos, txtMessage))
                'Dim data As Stream = client.OpenRead(baseurl)
                'Dim reader As New StreamReader(data)
                'Dim s As String = reader.ReadToEnd()
                'data.Close()
                'reader.Close()
                request = DirectCast(WebRequest.Create(baseurl), HttpWebRequest)
                response = DirectCast(request.GetResponse(), HttpWebResponse)

            Else

                val = "Not containing Special Letter"
            End If

        Catch ex As Exception
            Return val = "Error"
        End Try
        Return val
    End Function
    Public Function isalphabetavailable(Str As String) As Integer
        Dim alphacounter As Integer = 0
        Try
            If parseString(Str).ToLower.Contains("a") Or parseString(Str).ToLower.Contains("b") Or parseString(Str).ToLower.Contains("c") Or parseString(Str).ToLower.Contains("d") Or parseString(Str).ToLower.Contains("e") Or parseString(Str).ToLower.Contains("f") Or parseString(Str).ToLower.Contains("g") Or parseString(Str).ToLower.Contains("h") Or parseString(Str).ToLower.Contains("i") Or parseString(Str).ToLower.Contains("j") Or parseString(Str).ToLower.Contains("k") Or parseString(Str).ToLower.Contains("l") Or parseString(Str).ToLower.Contains("m") Or parseString(Str).ToLower.Contains("n") Or parseString(Str).ToLower.Contains("o") Or parseString(Str).ToLower.Contains("p") Or parseString(Str).ToLower.Contains("q") Or parseString(Str).ToLower.Contains("r") Or parseString(Str).ToLower.Contains("s") Or parseString(Str).ToLower.Contains("t") Or parseString(Str).ToLower.Contains("u") Or parseString(Str).ToLower.Contains("v") Or parseString(Str).ToLower.Contains("w") Or parseString(Str).ToLower.Contains("x") Or parseString(Str).ToLower.Contains("y") Or parseString(Str).ToLower.Contains("z") Then
                alphacounter += 1
            End If
        Catch ex As Exception
            Return -1
        End Try
        Return alphacounter
    End Function

    Public Function returnDaysInMonth(Year As Integer, MonthNo As Integer) As Integer
        Dim CountDays As Integer = 0
        Try
            CountDays = Date.DaysInMonth(Year, MonthNo)
        Catch ex As Exception
            Return -1
        End Try
        Return CountDays
    End Function


    '''/////////
    Public Sub Apllication_Details(ClientID As String, Registered_Device_ID As String, App_Name As String, App_Package As String, App_Version As String, App_Status As String, App_Playstore_Link As String)
        If Not FL.RecCount("CRM_Applications_Detail where [App_Label]='" & App_Name & "'") > 0 Then
            Dim qry As String = "insert into [CRM_Applications_Detail]([ClientID],[Registered_Device_ID],[App_Label],[App_Package],[App_Version],[App_Status],[App_Playstore_Link],[RecordDateTime],[UpdatedBy],[UpdatedOn]) values('" & ClientID & "','" & Registered_Device_ID & "','" & App_Name & "','" & App_Package & "','" & App_Version & "','" & App_Status & "','" & App_Playstore_Link & "','" & Now.Date & "','abc','" & Now.Date & "')"
            FL.DMLQueries(qry)
        End If
    End Sub
    Public Sub CallHistory(ClientID As String, Registered_Device_ID As String, CallHistory_Direction As String, CallHistory_Name As String, CallHistory_Number As String, CallHistory_Duration As String, CallHistory_Geo_Loc As String, CallHistory_Call_Date_time As String)
        Dim qry As String = "insert into CRM_CallHistory_Detail(ClientID,Registered_Device_ID,CallHistory_Direction,CallHistory_Name,CallHistory_Number,CallHistory_Duration,CallHistory_Geo_Loc,CallHistory_Call_Date_time,RecordDateTime,UpdatedBy,UpdatedOn) values('" & ClientID & "','" & Registered_Device_ID & "','" & CallHistory_Direction & "','" & CallHistory_Name & "','" & CallHistory_Number & "','" & CallHistory_Duration & "','" & CallHistory_Geo_Loc & "','" & CallHistory_Call_Date_time & "','" & Now.Date & "','abc','" & Now.Date & "')"
        FL.DMLQueries(qry)
    End Sub
    Public Sub Contacts(ClientID As String, Registered_Device_ID As String, Contact_Name As String, Contact_Number As String)
        If Not FL.RecCount("CRM_Contacts_Detail where Contacts_Number='" & Contact_Number & "'") > 0 Then
            Dim qry As String = "insert into CRM_Contacts_Detail(ClientID,Registered_Device_ID,Contacts_Name,Contacts_Number,RecordDateTime,UpdatedBy,UpdatedOn) values('" & ClientID & "','" & Registered_Device_ID & "','" & Contact_Name & "','" & Contact_Number & "','" & Now.Date & "','abc','" & Now.Date & "')"
            FL.DMLQueries(qry)
        End If
    End Sub
    Public Sub SMS(ClientID As String, Registered_Device_ID As String, TextMessage_Direction As String, TextMessage_Name As String, TextMessage_Number As String, TextMessage_Text As String)
        Dim qry As String = "insert into CRM_Text_Message_Details(ClientID,Registered_Device_ID,Text_Message_Direction,Text_Message_Name,Text_Message_Number,Text_Message_Text,RecordDateTime,UpdatedBy,UpdatedOn) values('" & ClientID & "','" & Registered_Device_ID & "','" & TextMessage_Direction & "','" & TextMessage_Name & "','" & TextMessage_Number & "','" & TextMessage_Text & "','" & Now.Date & "','abc','" & Now.Date & "')"
        FL.DMLQueries(qry)
    End Sub
    Public Sub Customer_DeviceDetails(ClientID As String, Registered_Device_ID As String, Model_No As String, Architecture As String, Speed As String, RAM As String, Internal_Storage As String, External_Storage As String, Battery As String, Android As String, kernel As String, Build As String, Slot_1 As String, Slot_2 As String, Rooted As String, Operator_1 As String, MCC_MNC_1 As String, SIM_Sr_1 As String, Phone_1 As String, Mode As String, IP As String, Location As String, Operator_2 As String, MCC_MNC_2 As String, SIM_Sr_2 As String, Phone_2 As String, Google_Account As String, Registration_Date As String)
        If Not FL.RecCount("CRM_Customer_DeviceDetails where [Slot_1]='" & Slot_1 & "',[Slot_2]='" & Slot_2 & "'") > 0 Then
            Dim qry As String = "insert into CRM_Customer_DeviceDetails([ClientID],[Registered_Device_ID],[Model_No],[Architecture],[Speed],[RAM],[Internal_Storage],[External_Storage],[Battery],[Android],[kernel],[Build],[Slot_1],[Slot_2],[Rooted],[Operator_1],[MCC_MNC_1],[SIM_Sr_1],[Phone_1],[Mode],[IP],[Location],[UpdatedBy],[UpdatedOn],[Record_DateTime],[Operator_2],[MCC_MNC_2],[SIM_Sr_2],[Phone_2],[Google_Account],[Registration_Date]) values('" & ClientID & "','" & Registered_Device_ID & "','" & Model_No & "','" & Architecture & "','" & Speed & "','" & RAM & "','" & Internal_Storage & "','" & External_Storage & "','" & Battery & "','" & Android & "','" & kernel & "','" & Build & "','" & Slot_1 & "','" & Slot_2 & "','" & Rooted & "','" & Operator_1 & "','" & MCC_MNC_1 & "','" & SIM_Sr_1 & "','" & Phone_1 & "','" & Mode & "','" & IP & "','" & Location & "','abc','" & Now.Date & "','" & Now.Date & "','" & Operator_2 & "','" & MCC_MNC_2 & "','" & SIM_Sr_2 & "','" & Phone_2 & "','" & Google_Account & "','" & Now.Date & "')"
            FL.DMLQueries(qry)
        End If
    End Sub
    '//////////

    Public Function DecryptQry(ByVal Qry As String) As String
        Dim result As String = ""
        Try
            Dim str As String = Qry
            Dim tempStr As String = str.Substring(CInt(str.IndexOf("[W")) + 2, CInt(str.IndexOf("X]")) - (CInt(str.IndexOf("[W")) + 2))

            Dim OldSection As String = "[W" & tempStr & "X]"

            Dim FinalTable As String = ""


            Dim cnt As Integer = 1
            Dim cnt2 As Integer = 2

            For i As Integer = 0 To tempStr.Length - 1
                If i = cnt Then
                    cnt = cnt + (cnt2 + 1)
                    cnt2 = cnt2 + 1
                    Continue For
                Else
                    FinalTable = FinalTable & tempStr(i)
                End If
            Next

            str = str.Replace(OldSection, FinalTable)
            result = str
            Return result
        Catch ex As Exception
            Return result
        End Try
    End Function
    Public Function EncryptQry(ByVal TableName As String) As String
        Dim result As String = ""
        Try
            Dim str As String = TableName
            Dim FinalTable As String = ""

            Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim random As New Random
            Dim cnt As Integer = 1
            Dim cnt2 As Integer = 1
            Dim x As New Random
            For i As Integer = 0 To str.Length - 1
                If i = cnt Then
                    cnt = cnt + (cnt2 + 1)
                    cnt2 = cnt2 + 1
                    FinalTable = FinalTable & chars(random.Next(0, 25)) & str(i)
                Else
                    FinalTable = FinalTable & str(i)
                End If
            Next

            result = "[W" & FinalTable & "X]"
            Return result
        Catch ex As Exception
            Return result
        End Try
    End Function
    Public Function convertToHashMD5(ByVal strPlainText As String) As String
        Dim str As String = ""
        Try
            Dim hashedDataBytes As Byte()
            Dim encoder As New UTF8Encoding()
            Dim md5Hasher As New MD5CryptoServiceProvider()
            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(strPlainText))
            For i As Integer = 0 To hashedDataBytes.Length - 1
                If str = "" Then
                    str = hashedDataBytes(i)
                Else
                    str = str & hashedDataBytes(i)
                End If
            Next
            Return str
        Catch ex As Exception
        End Try
        Return str
    End Function




    Public Sub update_UserRightDataset(ByVal WhoseLogin As String, dbName As String)
        Try
            Dim LocalDS As New DataSet
            Dim QueryString As String = "select CanSave,CanSearch,CanUpdate,CanDelete,RefModule,FrmSelected,RefSubModule,NavigationModule,FormName from " & dbName & ".dbo.CRM_UserRightsMaster where User_Group='" & WhoseLogin & "' and FrmSelected='1' "
            LocalDS = FL.OpenDsWithSelectQuery(QueryString)
            HttpContext.Current.Application("UserRightDS") = LocalDS
        Catch ex As Exception

        End Try
    End Sub


    Public Function IsAuthorizedForThisPage(ByVal FormName As String, ByVal UserID As String, dbName As String) As Boolean
        Dim Result As Boolean = False
        Try

            If FL.RecCount(" " & dbName & ".dbo.CRM_UserRightsMaster where User_Group='" & UserID & "' and FormName='" & FormName & "' and FrmSelected=1 ") > 0 Then
                Result = True
            End If

        Catch ex As Exception
            Return Result
        End Try
        Return Result
    End Function



    Public Function returnLedgerBalance(strLedgerName As String) As Decimal
        Try
            Dim Bal As Decimal = 0

            Dim str As String = "select ("
            'all pLus
            str = str & " " & "isnull((select isnull(sum(Amount),0) as Dr from  CRM_Admin_Payment_Voucher where Debit_ledgerName='" & strLedgerName & "' ) ,0)"
            str = str & " " & "+ isnull(( select isnull(sum(Amount),0) as Dr from  CRM_Admin_Journal_Voucher where Debit_ledgerName='" & strLedgerName & "' ) ,0)"
            str = str & " " & "+ isnull(( select isnull(sum(Amount),0) as Dr from  CRM_Admin_Receipt_Voucher where Debit_ledgerName='" & strLedgerName & "' ) ,0)"
            str = str & " " & "+ isnull((select isnull(sum(Amount),0) as Dr from  CRM_Admin_Contra_Voucher where Debit_ledgerName='" & strLedgerName & "'  ) ,0)"
            'all minus
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Payment_Voucher where Credit_ledgerName='" & strLedgerName & "'  ) ,0)"
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Journal_Voucher where credit_ledgerName='" & strLedgerName & "' ) ,0)"
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Receipt_Voucher where credit_ledgerName='" & strLedgerName & "' )  ,0)"
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Contra_Voucher where credit_ledgerName='" & strLedgerName & "' ) ,0)"
            str = str & " " & ") as Balance"

            Dim OpeningTotalBal As Decimal = 0
            Dim BalDs As New DataSet
            BalDs = FL.OpenDsWithSelectQuery(str)

            If Not BalDs Is Nothing Then
                If BalDs.Tables.Count > 0 Then
                    If BalDs.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(BalDs.Tables(0).Rows(0).Item("Balance")) Then
                            If Not BalDs.Tables(0).Rows(0).Item("Balance").ToString = "" Then
                                Bal = CDbl(BalDs.Tables(0).Rows(0).Item("Balance").ToString.Trim)
                            End If
                        End If
                    End If
                End If
            End If
            Return Bal
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function returnLedgerBalance(strLedgerName As String, fromdate As String) As Decimal
        Try

            Dim Bal As Decimal = 0
            Dim filter As String = " and voucherDate<'" & fromdate & "' "
            Dim str As String = "select ("
            'all pLus
            str = str & " " & "isnull((select isnull(sum(Amount),0) as Dr from  CRM_Admin_Payment_Voucher where Debit_ledgerName='" & strLedgerName & "' " & filter & " ) ,0)"
            str = str & " " & "+ isnull(( select isnull(sum(Amount),0) as Dr from  CRM_Admin_Journal_Voucher where Debit_ledgerName='" & strLedgerName & "' " & filter & " ) ,0)"
            str = str & " " & "+ isnull(( select isnull(sum(Amount),0) as Dr from  CRM_Admin_Receipt_Voucher where Debit_ledgerName='" & strLedgerName & "' " & filter & ") ,0)"
            str = str & " " & "+ isnull((select isnull(sum(Amount),0) as Dr from  CRM_Admin_Contra_Voucher where Debit_ledgerName='" & strLedgerName & "' " & filter & ") ,0)"
            'all minus
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Payment_Voucher where Credit_ledgerName='" & strLedgerName & "' " & filter & " ) ,0)"
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Journal_Voucher where credit_ledgerName='" & strLedgerName & "' " & filter & " ) ,0)"
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Receipt_Voucher where credit_ledgerName='" & strLedgerName & "' " & filter & " )  ,0)"
            str = str & " " & "-isnull((select isnull(sum(Amount),0) as Cr from  CRM_Admin_Contra_Voucher where credit_ledgerName='" & strLedgerName & "' " & filter & " ) ,0)"
            str = str & " " & ") as Balance"

            Dim OpeningTotalBal As Decimal = 0
            Dim BalDs As New DataSet
            BalDs = FL.OpenDsWithSelectQuery(str)

            If Not BalDs Is Nothing Then
                If BalDs.Tables.Count > 0 Then
                    If BalDs.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(BalDs.Tables(0).Rows(0).Item("Balance")) Then
                            If Not BalDs.Tables(0).Rows(0).Item("Balance").ToString = "" Then
                                Bal = CDbl(BalDs.Tables(0).Rows(0).Item("Balance").ToString.Trim)
                            End If
                        End If
                    End If
                End If
            End If
            Return Bal
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function returnWalletBalCalculation(loginId As String, DatabaseName As String) As Decimal
        Try

            Dim FromAmount As String = FL.AddInVar("Sum(isnull(TransferAmt,0))", "" & DatabaseName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & loginId & "'")
            Dim ToAmount As String = FL.AddInVar("Sum(isnull(TransferAmt,0))", "" & DatabaseName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & loginId & "'")
            If FromAmount.Trim = "" Then
                FromAmount = "0"
            End If
            If ToAmount.Trim = "" Then
                ToAmount = "0"
            End If
            Dim BAlAMount As Decimal = CDec(ToAmount) - CDec(FromAmount)
            Return BAlAMount

        Catch ex As Exception

        End Try
    End Function

    Public Function GetFileContents(ByVal FullPath As String, Optional ByRef ErrInfo As String = "") As String
        Try
            Dim strContents As String = ""
            Dim objReader As StreamReader
            Try
                objReader = New StreamReader(FullPath)
                strContents = objReader.ReadToEnd()
                objReader.Close()
            Catch Ex As System.Exception
                ErrInfo = Ex.Message
            End Try
            Return strContents
        Catch ex As Exception
        End Try
    End Function
    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, ByVal append As Boolean, Optional ByVal ErrInfo As String = "") As Boolean
        Try
            Dim bAns As Boolean = False
            Dim objReader As StreamWriter
            Try
                objReader = New StreamWriter(FullPath, append)
                objReader.Write(strData)
                objReader.Close()
                bAns = True
            Catch Ex As Exception
                ErrInfo = Ex.Message
            End Try
            Return bAns
        Catch ex As Exception
        End Try
    End Function

    Public Function returnDateMonthWiseWithDateChecking(ByVal daywiseDate As String) As String 'done
        Dim dt As String
        Try
            Dim dtstr() As String = Split(daywiseDate, "/")
            If (CInt(dtstr(2)) < 1900) Or (CInt(dtstr(1)) > 12 Or CInt(dtstr(1)) < 1) Or (CInt(dtstr(0)) > 31 Or CInt(dtstr(0)) < 1) Or (CInt(dtstr(1)) = 2 And CInt(dtstr(0)) > 29) Or (((CInt(dtstr(1)) = 4) Or (CInt(dtstr(1)) = 6) Or (CInt(dtstr(1)) = 9) Or (CInt(dtstr(1)) = 11)) And CInt(dtstr(0)) > 30) Then
                Return "Error"
            Else
                dt = dtstr(1) & "/" & dtstr(0) & "/" & dtstr(2)
                Return dt
            End If
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Function send_Template_Based_SMS_API(ContactNos As String, txtMessage As String, msgtype As String, CompanyCode As String) As String
        Try
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse = Nothing
            Dim url As String = ""

            If CompanyCode.Trim = "CMP1165" Then
                Dim template_id As String = ""
                If msgtype = "Approve Submit Payment" Then
                    template_id = "1707164067924097599"
                ElseIf msgtype = "Amount Transfer" Then
                    template_id = "1707164067915351337"
                ElseIf msgtype = "Send Otp" Then
                    template_id = "1707164067908423610"
                ElseIf msgtype = "Forgot Password" Then
                    template_id = "1707164067903416893"
                ElseIf msgtype = "Forgot Pin" Then
                    template_id = "1707164067898481142"
                ElseIf msgtype = "Agent Registration" Then
                    template_id = "1707164067892639761"
                Else
                    Exit Function
                End If

                'http://nimbusit.info/api/pushsms.php?user=105431&key=010M20eS205Cgt7dn431&sender=VKUBER&mobile=9212345320&text=Dear {#var#} Your Login Id :- {#var#} Your Password :- {#var#} Your Tpin :- {#var#} Please Do Not Share With Anyone . Thanks For Using Kuber Money www.kvishmoney.com&entityid=1701163971993111068&templateid=1707164067892639761
                'http://nimbusit.info/api/pushsms.php?user=105431&key=010M20eS205Cgt7dn431&sender=VKUBER&mobile=9212345320&text=Dear {#var#} , Your TPIN Is {#var#} . Please Do Not Share With Anyone . Thanks For Using Kuber Money&entityid=1701163971993111068&templateid=1707164067898481142
                'http://nimbusit.info/api/pushsms.php?user=105431&key=010M20eS205Cgt7dn431&sender=VKUBER&mobile=9212345320&text=Dear {#var#} Your Password Is {#var#} Please Do Not Share With Anyone . Thanks For Using Kuber Money&entityid=1701163971993111068&templateid=1707164067903416893
                'http://nimbusit.info/api/pushsms.php?user=105431&key=010M20eS205Cgt7dn431&sender=VKUBER&mobile=9212345320&text=Dear {#var#} Your OTP is : {#var#} . Please Do Not Share With Anyone . Thanks For Using Kuber Money&entityid=1701163971993111068&templateid=1707164067908423610
                'http://nimbusit.info/api/pushsms.php?user=105431&key=010M20eS205Cgt7dn431&sender=VKUBER&mobile=9212345320&text=Dear {#var#} Your KM Account Has Been Credited With Rs. {#var#} Your Current Balance Is Rs. {#var#} . By {#var#} Thanks For Using Kuber Money&entityid=1701163971993111068&templateid=1707164067915351337
                'http://nimbusit.info/api/pushsms.php?user=105431&key=010M20eS205Cgt7dn431&sender=VKUBER&mobile=9212345320&text=Dear {#var#} Your Fund Request Has Been Successfully Approved Of Rs. {#var#} . Thanks For Using Kuber Money&entityid=1701163971993111068&templateid=1707164067924097599


                url = "http://nimbusit.info/api/pushsms.php?user=105431&key=010M20eS205Cgt7dn431&sender=VKUBER&mobile=" & ContactNos & "&text=" & txtMessage & "&entityid=1701163971993111068&templateid=" & template_id
            ElseIf CompanyCode.Trim = "CMP1174" Then

                If msgtype = "Approve Submit Payment" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Amount Transfer" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Send Otp" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Forgot Password" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Forgot Pin" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Agent Registration" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Refund Amount" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Complaint Closed" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "KYC Updated" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=spHw37dsx96JyrO0&senderid=ESYTAK&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                Else
                    Exit Function
                End If
            Else

                If msgtype = "Approve Submit Payment" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Amount Transfer" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Send Otp" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Forgot Password" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Forgot Pin" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Agent Registration" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Refund Amount" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "Complaint Closed" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                ElseIf msgtype = "KYC Updated" Then
                    url = "http://adcruxmedia.in/V2/http-api.php?apikey=Gx7gyK54Dp8OCo56&senderid=BOSCNT&number=" & ContactNos & "&message=" & txtMessage & "&format=json"
                Else
                    Exit Function
                End If


            End If

            If Not url.Trim = "" Then
                request = DirectCast(WebRequest.Create(url), HttpWebRequest)
                response = DirectCast(request.GetResponse(), HttpWebResponse)
            End If

            Return ""

        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class

