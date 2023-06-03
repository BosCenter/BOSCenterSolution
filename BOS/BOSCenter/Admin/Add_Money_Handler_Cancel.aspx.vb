﻿Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports InstamojoAPI
Imports System.Security.Cryptography

Public Class Add_Money_Handler_Cancel
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------

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
                    'CMP1045_CC_BOS-1651_157599
                    Dim dbData() As String = orderid.Trim.Split("_")
                    dbName = dbData(0)
                    If dbName.Trim = "CMP1045" Then
                        dbName = GV.DefaultDatabase
                    End If

                    If dbData(1).Trim.ToUpper = "WA" Then
                        Dim str As String = "update " & dbName.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details set Response_DateTime=getdate(),Response_Payment_Status='Cancel',Response_Transaction_Id='" & orderid & "',Response_Action_Taken='No' where Request_Transaction_Id='" & orderid & "';"
                        'str = str & " ; " & "update E_Com_Order_Details_Master set Order_Status='success' where Order_ID='" & SessionOrderID & "'"
                        GV.FL.DMLQueriesBulk(str)
                    End If

                End If


                formheading3.Text = ":: Payment Status Details :: "

                lblError.Text = "Your Transaction Is Cancel. Please Try Again."
                lblOtherInfo.Text = "Your Payment ID Is : " & orderid
                lblOtherInfo.ForeColor = Color.Blue
                lblOtherInfo.Font.Bold = True


                lblError.CssClass = "errorlabels"
                btnRedirect.Visible = True
                Wallet_Heading.Visible = False
                Wallet_Row.Visible = False

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