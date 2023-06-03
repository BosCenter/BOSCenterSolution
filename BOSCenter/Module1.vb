Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.SmtpClient
Imports System.Text
Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net

Module Module1

    Public ds As DataSet
    Public Customer_MenuCookies As New HttpCookie("Customer_MenuCookies")
    Public APIKey As String = "UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh"
    Public APIKey_New As String = "UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh"
    Public VerifyServiceCharge As Decimal = 3
    Public TDS_Per As Decimal = 0.05 '5%
    Public GST_Per As Decimal = 0.18 ' 18%

    'Admin se Superadmin pr bhi VerifyServiceCharge krna hai

    Public siteName As String = "https://boscenter.in"

    Public auth_endpoint As String = "https://www.instamojo.com/oauth2/token/"
    Public endpoint As String = "https://api.instamojo.com/v2/"
    Public client_id As String = "dKbCrLvyPMRNTKKhzknozPjK5br6e0G2Z9Uu9QxS"
    Public client_secret As String = "hBriZIe7dE7MfkgqALkzdfxTvJkpC2x8g4xk5rsXPtejlDrF6FVP27Crj5DCwXKwvGcDcJXTmRnhNJSal8KwSQzbrEMpEbVnlw42GjOjYLzW9eopagwrtXCOPA5LrPJE"

    'Public MERCHANT_KEY As String = "idpYvzfd"
    'Public SALT_KEY As String = "MDZsxiMB5h"
    'Public PAYU_BASE_URL As String = "https://secure.payu.in"


    'XXXXXXXXX Update Marchent_Key and SALT_KEY OLD on 04/04/2023XXXXXXXXXXXXXXXXX
    ' Public MERCHANT_KEY As String = "P9KLtP"
    'Public SALT_KEY As String = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC8Gz0QcronWCsfFlN1xXrDiqGl46AL82XC9/B7YshoUDTQpfq3ebBy+o/XB50cDk6889dM9IWEUhppksMfz0rGdhChTAwsf9fRLcguPaOWhHRkztXYdeftczo0lqw/pLjlX6PmrZIYzvmrYyZFntjwHZjkBxlJjAh1Z65yegaHYtyBVHc7cJQ7F9h+ZKTV7iCXjM2RmnBSbcSEp0Q2Rhvnc3vJZu1jG6WZQmWypCj1FpYVpg7qp1+rDvbHGRjOHXVrTxu/Jb3jUK6BRM/M6udwlVbnI9Ap4gU55owB6njXAeSGnfjBBL2mlVyR0PEfwuxFejhdOkLMlx5jcAul5vQ7AgMBAAECggEAP+56CJcVkb2zmjM6VnKx0LfTTNBaWvV0hplCEI14U4u/B5MB7U3cnJdwP8MEsL5kIHA3op4QqvJEq9EHpE4NufnymP+Bu7YPobaPrLeiW8vUy1ZI8/LOyrM4+xqinFbdyIeC6yne17Oww4FFRrBARwuoJQgMniSL02tTrPCwO17XLFALTw33KHaWwg3NVXCAAiYDob8dxuEV2IUmeE0T2dfD/so2shU/4Jg9AIhQ2NKZY+4s+YrgiQn7dWBMxP+qnGxij5gsFXBhibObFV8zhgI0YEOHd321Q6gU6zNIe8sTKnpEdZzr0Htvl5TV/pnfjQGQcfek6X5ku1lfGy/dMQKBgQDblGOMaGx1ZrrSFVktD82+6NgUoFdVHivgN6/9Ef6Ux+KBtyqFFk8PF+kwTmvJtBRmeaVA7bLrDTuygZk6KB5GT1PIKciIMJpBSNPGwQO13cfTgC5xjIHjFLlKW6LvC8XlGedvtd/d3nesw7iKsSAlnv2rWv4Am9wSO9LFUQmo1QKBgQDbTnVkeph49FcmQxxY0eW/35kMnabfdmElIH+rBiwgUT6SgsxU2Z1fzKCzdpgeJKww9aHcOE6+0VT5NIPp5ZhkpMkc4D7ZLqCOKLqJdePrVgG766MmcnglQXUmw34uP99luC8chqJi/sNrCnnMbfKhDrG5ifJBTcVAONR7oXGwzwKBgCqALUiHCb8rlCuHoEwdi/hQv0o2wtCh12I9xR2Ztwn7KndrCZra95B3U1ZbR3eyGGTBVOAdYg0m8ZAaj5r3Gu0G+7N5iuv5ZFIQk5Ub1OkFtWDVpaeqx5U5dKfU1tOoFrTCb85qiJs8LTPOalDF+e/uNzFMmm9pu4338FLq3ZDFAoGAXC1cFkGZOK2qu8BVd9exc7Ztw+m3rBE7v7krMB1GCsdbP4WOkNNu3EdL/GKKiZDsdx+nSsK4BmhCCQTTtUn1hcflaCexAuQgQ+BGl2Rfyhi6XdwrFQvkB/S9Yu+kZ5gdM8n4s0q7klR2mtUqYIyOALgGZ2/dzSj9EYNRfC1dde0CgYEAjZTmz5L4BZia9SC4CQUgw4wqjj4zEZ1TUkn1Esc8XCxxDOi0AvUSZGdSOlIxS1ty8jvJVFvsCci/0WJ+tMGL0kobrbB1+B1bXXtGhHdYgR4M8H2MF7A23mhA/2PkoKSBvujZjXkzq175kiKuKEUWS9LhpwTUt39C2Cz4QI9I5mo="



    'XXXXXXXXX Update Marchent_Key and SALT_KEY on 04/04/2023XXXXXXXXXXXXXXXXX
    Public MERCHANT_KEY As String = "idpYvzfd"
    Public SALT_KEY As String = "MDZsxiMB5h"
    Public PAYU_BASE_URL As String = "https://secure.payu.in"
    'XXXXXXXXX Update Marchent_Key and SALT_KEY on 04/04/2023XXXXXXXXXXXXXXXXX




    Public EaseBuzz_MERCHANT_KEY As String = "MEBKTJC7XL"
    Public EaseBuzz_SALT_KEY As String = "48Q5G1L07G"
    Public EaseBuzz_BASE_URL As String = "https://pay.easebuzz.in"

    'Public hashSequence As String = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10"



    ' Paysprint AEPS Variables
    Public AEPS_ONBOARD_CHARGE As Decimal = 20
    Public AEPS_MINI_STATEMENT_CHARGE As Decimal = 0
    Public AEPS_BALANCE_CHECK_CHARGE As Decimal = 0



    Public Function canPerformOperation(CurrentForm As String, CheckOption As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim SearchOperation As String = ""
            If CheckOption.Trim.ToUpper = "SAVE" Then
                SearchOperation = "CanSave"
            ElseIf CheckOption.Trim.ToUpper = "Search".ToUpper Then
                SearchOperation = "CanSearch"
            ElseIf CheckOption.Trim.ToUpper = "Update".ToUpper Then
                SearchOperation = "CanUpdate"
            ElseIf CheckOption.Trim.ToUpper = "Delete".ToUpper Then
                SearchOperation = "CanDelete"
            End If
            If Not SearchOperation.Trim = "" Then
                Dim LocalDS As New DataSet
                LocalDS = CType(HttpContext.Current.Application("UserRightDS"), DataSet)
                Dim DataRows() As DataRow
                DataRows = LocalDS.Tables(0).Select(" FormName='" & CurrentForm & "' ")
                If Not DataRows Is Nothing Then
                    If DataRows.Count > 0 Then
                        For D As Integer = 0 To DataRows.Count - 1
                            If DataRows(D).Item(SearchOperation) = True Then
                                result = True
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
        Return result
    End Function
End Module
