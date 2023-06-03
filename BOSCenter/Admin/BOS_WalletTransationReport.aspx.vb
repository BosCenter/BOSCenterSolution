Public Class BOS_WalletTransationReport
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("ADMIN")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                Dim Loginid As String = ""
                If group = "Master Distributor" Then
                    Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                    group = "Distributor"
                    Div_myWallet.Visible = True
                ElseIf group = "Distributor" Then
                    Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                    group = "Retailer"
                    Div_myWallet.Visible = True
                ElseIf group.ToUpper.Trim = "Admin".ToUpper.Trim Then
                    Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                    group = "Retailer"
                    Div_myWallet.Visible = True
                Else

                    Div_myWallet.Visible = True
                    Loginid = ""
                    group = "Master Distributor"
                End If


                Calculation()
                Bind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Public Sub Clear()
        Try
            Bind()
            Calculation()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Public Sub Calculation()
        Try
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim FromAmount As String = GV.FL.AddInVar("Sum(isnull(TransferAmt,0))", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            Dim ToAmount As String = GV.FL.AddInVar("Sum(isnull(TransferAmt,0))", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            If FromAmount.Trim = "" Then
                FromAmount = "0"
            End If
            If ToAmount.Trim = "" Then
                ToAmount = "0"
            End If
            Dim BAlAMount As Decimal = CDec(ToAmount) - CDec(FromAmount)
            txtMainBalance.Text = Math.Round(BAlAMount)
            If group = "Distributor" Or group = "Master Distributor" Or group = "Retailer" Then
                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                txtMyCreditLimit.Text = CreditBAl
            Else
                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                txtMyCreditLimit.Text = CreditBAl
            End If
            If txtMainBalance.Text.Contains("-") Then
                txtAvailableCredit.Text = CDec(txtMyCreditLimit.Text.Trim) + CDec(txtMainBalance.Text.Trim)
            Else
                txtAvailableCredit.Text = txtMyCreditLimit.Text.Trim
            End If

            Dim HoldAmt As String
            If group.ToUpper.Trim = "Admin".ToUpper.Trim Then
                HoldAmt = GV.FL.AddInVar("HoldAmt", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'")
            Else
                HoldAmt = GV.FL.AddInVar("HoldAmt", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            End If
            If HoldAmt = "" Then
                HoldAmt = "0"
            End If
            txtHoldAmt.Text = CDec(HoldAmt)


            txtActualAvaitrasferAmt.Text = CDec(txtMyCreditLimit.Text.Trim) + CDec(txtMainBalance.Text.Trim) - CDec(txtHoldAmt.Text.Trim)

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim SearchColumnName As String = ""
            Dim RefrenceId As String = ""
            Dim Value As String = ""


            RefrenceId = " RefrenceID ='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
            Querystring = "select Top 10 RID as SrNo,TransferTo ,(Select (FirstName+ ' ' +LastName) from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] a where a.RegistrationId=b.TransferTo) as Name,TransferAmt,'0' as Balance,Remark,(CONVERT(VARCHAR(11),TransactionDate,106)) as TransactionDate from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_TransferAmountToAgents] b  where TransferFrom in (select RefrenceId from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where " & RefrenceId & " ) " & SearchColumnName & "   order by  RID Desc"


            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then
                    Dim Balnceamt As String = 0
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        If Balnceamt = "" Then
                            Balnceamt = GV.parseString(GridView1.Rows(i).Cells(3).Text)
                        Else
                            Balnceamt = CLng(Balnceamt) + CLng(GridView1.Rows(i).Cells(3).Text)
                        End If
                        GridView1.Rows(i).Cells(4).Text = Balnceamt
                        GridView1.Rows(i).Cells(0).Text = i + 1
                    Next
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class
