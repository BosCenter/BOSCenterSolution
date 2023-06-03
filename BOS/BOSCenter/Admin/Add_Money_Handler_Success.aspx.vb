Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports InstamojoAPI
Imports System.Security.Cryptography

Public Class Add_Money_Handler_Success
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim Response_Transaction_Id, Response_Payment_Id, Response_Payment_Status As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Dim orderid As String = ""
                Dim SessionOrderID As String = GV.get_OrderID_SessionVariables("OrderID", HttpContext.Current.Request, HttpContext.Current.Response)
                If SessionOrderID Is Nothing Then
                    SessionOrderID = ""
                End If
                If SessionOrderID = "" Then
                    Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
                    Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")
                    Response.Redirect(strUrl)
                Else
                    orderid = SessionOrderID
                    GV.Expire_OrderID_Session(HttpContext.Current.Request, HttpContext.Current.Response)
                End If
                Dim dbName As String = ""

                If Not orderid.Trim = "" Then
                    Response_Transaction_Id = orderid.Trim


                    'CMP1045_CC_BOS-1651_157599
                    Dim dbData() As String = orderid.Trim.Split("_")
                    dbName = dbData(0)
                    If dbName.Trim = "CMP1045" Then
                        dbName = GV.DefaultDatabase
                    End If

                    If dbData(1).Trim.ToUpper = "WA" Then
                        Response_Payment_Id = dbData(3).Trim
                        Try
                            Dim VTransferFromMsg As String = ""
                            Dim VTransferToMsg As String = ""
                            Dim SMSMeassgeTo As String = ""
                            Dim SMSMeassgeFrom As String = ""
                            Dim AmouontType As String = ""
                            Dim VTransferTo As String = ""
                            Dim VTransferFrom As String = "Admin"
                            Dim VAmtTransTransID As String = ""
                            Dim VTransferAmt As String = "0"
                            Response_Payment_Status = "Success"

                            ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_Transaction_Id='" & Response_Transaction_Id & "' ")
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Request_TransID")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Request_TransID").ToString() = "" Then
                                                VAmtTransTransID = GV.parseString(ds.Tables(0).Rows(0).Item("Request_TransID").ToString())
                                            Else
                                                VAmtTransTransID = ""
                                            End If
                                        Else
                                            VAmtTransTransID = ""
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Request_AgentID")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Request_AgentID").ToString() = "" Then
                                                VTransferTo = GV.parseString(ds.Tables(0).Rows(0).Item("Request_AgentID").ToString())
                                            Else
                                                VTransferTo = ""
                                            End If
                                        Else
                                            VTransferTo = ""
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Request_amount")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Request_amount").ToString() = "" Then
                                                VTransferAmt = GV.parseString(ds.Tables(0).Rows(0).Item("Request_amount").ToString())
                                            Else
                                                VTransferAmt = "0"
                                            End If
                                        Else
                                            VTransferAmt = "0"
                                        End If


                                    End If
                                End If
                            End If





                            AmouontType = "Deposit"

                            VTransferFromMsg = "Your Wallet is Debited by Distributor (" & VTransferTo & ")"
                            VTransferToMsg = "Your Wallet is Credited by Admin - Gateway"
                            SMSMeassgeTo = "Your Wallet is Credited With Rs. " & VTransferAmt & " By Admin - Gateway"


                            If Not GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents Where Amt_Transfer_TransID='" & VAmtTransTransID & "' ") > 0 Then 'Change where condition according to Criteria 

                                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & VAmtTransTransID & "','" & VAmtTransTransID & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & AmouontType & "','" & "Add By Wallet - Gateway" & "',getdate(),'" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "',getdate(),'Admin',getdate() ) ;"
                                QryStr = QryStr & " update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details set  Response_DateTime=getdate(), Response_Payment_Id='" & Response_Payment_Id & "', Response_Payment_Status='" & Response_Payment_Status & "',  Response_Transaction_Id='" & Response_Transaction_Id & "', Response_Action_Taken='Yes'  where Request_Transaction_Id='" & Response_Transaction_Id & "'; "

                                If GV.FL.DMLQueries(QryStr) = True Then




                                    ''////// Service Charge For Admin To SuperAdmin - Start
                                    Dim NetAmount As Decimal = 0
                                    Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and Title='PayUMoney' ").Split(":")
                                    If Service.Length > 1 Then
                                        If Service(1).Trim = "Percentage" Then
                                            NetAmount = (CDec(VTransferAmt) * CDec(Service(0))) / 100
                                        ElseIf Service(1).Trim = "Amount" Then
                                            NetAmount = CDec(Service(0))
                                        ElseIf Service(1).Trim = "Not Applicable" Then
                                            NetAmount = CDec(Service(0))
                                        End If
                                    End If
                                    If NetAmount > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to Add Money - " & RTE & " ."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to Add Money - " & RTE & " ."
                                        QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & GV.FL_AdminLogin.getAutoNumber("TransId") & "','" & VTransferAmt & "', '" & GV.parseString(VAmtTransTransID) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "',getdate(),'Admin',getdate() ) ;"
                                        GV.FL.DMLQueries(QryStr)
                                    End If
                                    ''///////  Service Charge For Admin To SuperAdmin - End



                                    Dim FromMobile As String = GV.FL.AddInVar("MobileNo", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferFrom & "' ")
                                    Dim ToMobile As String = GV.FL.AddInVar("MobileNo", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")
                                    ''GV.sendSMSThroughAPI(FromMobile, SMSMeassgeFrom)


                                    'GV.sendSMSThroughAPI(ToMobile, SMSMeassgeTo)

                                    formheading3.Text = ":: Payment Status Details :: "

                                    lblError.Text = "Amount Added To Your Wallet Successfully."
                                    lblError.CssClass = "Successlabels"

                                    lblOtherInfo.Text = "Your Payment ID : " & Response_Payment_Id
                                    lblOtherInfo.Text = lblOtherInfo.Text & "<br /><br />Transaction Amount : " & VTransferAmt
                                    lblOtherInfo.Text = lblOtherInfo.Text & "<br /><br />Transaction ID : " & Response_Transaction_Id
                                    lblOtherInfo.ForeColor = Color.Blue
                                    lblOtherInfo.Font.Bold = True



                                    btnRedirect.Visible = True
                                    Wallet_Heading.Visible = False
                                    Wallet_Row.Visible = False

                                Else
                                    formheading3.Text = ":: Payment Status Details :: "
                                    lblError.Text = "Wallet Updation Failed."
                                    lblError.CssClass = "errorlabels"
                                    lblOtherInfo.Text = ""
                                    btnRedirect.Visible = True
                                    Wallet_Heading.Visible = False
                                    Wallet_Row.Visible = False
                                End If
                            Else
                                formheading3.Text = ":: Payment Status Details :: "

                                lblError.Text = "Invalid Transaction ID, Please Try Again."
                                lblOtherInfo.Text = ""

                                lblError.CssClass = "errorlabels"
                                btnRedirect.Visible = True
                                Wallet_Heading.Visible = False
                                Wallet_Row.Visible = False
                            End If
                        Catch ex As Exception
                            formheading3.Text = ":: Payment Status Details :: "

                            lblError.Text = ex.Message
                            lblOtherInfo.Text = ""

                            lblError.CssClass = "errorlabels"
                            btnRedirect.Visible = True
                            Wallet_Heading.Visible = False
                            Wallet_Row.Visible = False
                        End Try
                    End If





                End If



            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnRedirect_Click(sender As Object, e As System.EventArgs) Handles btnRedirect.Click
        Try
            'formheading3.Text = ":: Add Money Form :: "
            formheading3.Text = ""
            lblOtherInfo.Text = ""

            Wallet_Heading.Visible = True
            Wallet_Row.Visible = True
            lblError.Text = ""
            lblError.CssClass = ""
            btnRedirect.Visible = False

            Response.Redirect("BOS_AddMoneyToWallet.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class