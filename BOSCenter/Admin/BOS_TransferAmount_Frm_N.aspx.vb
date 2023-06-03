Public Class BOS_TransferAmount_Frm_N
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("ADMIN")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                lblAmt_Transfer_TransID.Text = GV.FL.getAutoNumber("TransId")

                ddlTransferToAgent.Items.Clear()
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                Dim Loginid As String = ""
                Div_MyAcountdetails.Visible = True
                If group.Trim.ToUpper = "Admin".Trim.ToUpper Then

                    ddlTransferTo.Items.Clear()
                    ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                    ddlTransferTo.Items.Add(":::: Select Customer ::::")

                    rw_Div_radiobutton.Visible = False
                    ddlTransferTo.Visible = True
                    Label7.Visible = False
                    ddlPaymentMode.Visible = True
                    Label11.Visible = True
                    lblAgentType.Visible = False
                    Div_MyAcountdetails.Visible = True

                    ddlTransferTo_SelectedIndexChanged(sender, e)
                ElseIf group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then

                    ddlTransferTo.Items.Clear()
                    ddlTransferTo.Items.Add(":::: Select Admin ::::")

                    rw_Div_radiobutton.Visible = False

                    ddlTransferTo.Visible = True
                    Label7.Visible = False
                    ddlPaymentMode.Visible = True
                    Label11.Visible = True
                    lblAgentType.Visible = False
                    Div_MyAcountdetails.Visible = False
                    ddlTransferTo_SelectedIndexChanged(sender, e)
                Else
                    rw_Div_radiobutton.Visible = True

                    lblAgentType.Visible = True
                    ddlTransferTo.Visible = False
                    Label7.Visible = True
                    ddlPaymentMode.Visible = False
                    Label11.Visible = False
                    If group = "Master Distributor" Then
                        Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                        group = "Distributor"
                        Div_MyAcountdetails.Visible = True

                        ddlTransferTo.Items.Clear()
                        ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                        ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                        ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                        ddlTransferTo.Items.Add(":::: Select Customer ::::")
                        ddlTransferToAgent.Visible = False

                        rw_Div_radiobutton.Visible = False
                        ddlTransferTo.Visible = True
                        Label7.Visible = False
                        ddlPaymentMode.Visible = True
                        Label11.Visible = True
                        lblAgentType.Visible = False

                        txtCustomerID.Text = ""
                        txtCustomerName.Text = ""

                        txtCustomerID.Visible = True
                        txtCustomerName.Visible = True

                        txtCustomerName.ReadOnly = True
                        txtCustomerID.ReadOnly = False
                    ElseIf group = "Distributor" Then
                        Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                        group = "Retailer"
                        Div_MyAcountdetails.Visible = True

                        ddlTransferTo.Items.Clear()
                        ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                        ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                        ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                        ddlTransferTo.Items.Add(":::: Select Customer ::::")
                        ddlTransferToAgent.Visible = False

                        rw_Div_radiobutton.Visible = False
                        ddlTransferTo.Visible = True
                        Label7.Visible = False
                        ddlPaymentMode.Visible = True
                        Label11.Visible = True
                        lblAgentType.Visible = False

                        txtCustomerID.Text = ""
                        txtCustomerName.Text = ""

                        txtCustomerID.Visible = True
                        txtCustomerName.Visible = True

                        txtCustomerName.ReadOnly = True
                        txtCustomerID.ReadOnly = False


                    ElseIf group = "Customer" Then

                        Div_MyAcountdetails.Visible = True
                        lblAgentType.Text = "Customer"
                        rb_TransferToType.Visible = False
                        rb_Customer.Checked = True
                        rb_Customer_CheckedChanged(sender, e)
                        Calculation()
                        Session("EditFlag") = 0
                        Exit Sub
                    ElseIf group = "Retailer" Then
                        ddlPaymentMode.Items.Remove("MINUS")

                        Div_MyAcountdetails.Visible = True
                        lblAgentType.Text = "Retailer"
                        Calculation()
                        Session("EditFlag") = 0

                        ddlTransferTo.Items.Clear()
                        ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                        ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                        ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                        ddlTransferTo.Items.Add(":::: Select Customer ::::")
                        ddlTransferToAgent.Visible = False

                        rw_Div_radiobutton.Visible = False
                        ddlTransferTo.Visible = True
                        Label7.Visible = False
                        ddlPaymentMode.Visible = True
                        Label11.Visible = True
                        lblAgentType.Visible = False

                        txtCustomerID.Text = ""
                        txtCustomerName.Text = ""

                        txtCustomerID.Visible = True
                        txtCustomerName.Visible = True

                        txtCustomerName.ReadOnly = True
                        txtCustomerID.ReadOnly = False

                        Exit Sub
                    Else
                        Div_MyAcountdetails.Visible = False
                        Loginid = ""
                        group = "Master Distributor"
                    End If
                    'formheading3.Text = "Transfer Amount " & group & " Form"
                    lblAgentType.Text = group
                    rb_TransferToType.Text = group
                    rb_TransferToType.Checked = True

                    GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & group & "' " & Loginid & "")
                    If ddlTransferToAgent.Items.Count > 0 Then
                        ddlTransferToAgent.Items.Insert(0, ":::: Select " & group & " ::::")
                    Else
                        ddlTransferToAgent.Items.Add(":::: Select " & group & " ::::")
                    End If
                End If
                Calculation()
                Session("EditFlag") = 0
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlPaymentMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentMode.SelectedIndexChanged
        Try
            ddlTransferTo.Items.Clear()
            txtCustomerID.Text = ""
            txtCustomerName.Text = ""

            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            If ddlPaymentMode.SelectedValue = "MINUS" Then
                If group.Trim.ToUpper = "Admin".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                    ddlTransferTo.Items.Add(":::: Select Customer ::::")
                    ddlTransferTo_SelectedIndexChanged(sender, e)

                ElseIf group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Admin ::::")
                ElseIf group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                ElseIf group.Trim.ToUpper = "Retailer".Trim.ToUpper Then

                ElseIf group.Trim.ToUpper = "Customer".Trim.ToUpper Then

                Else
                End If
            Else
                If group.Trim.ToUpper = "Admin".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                    ddlTransferTo.Items.Add(":::: Select Customer ::::")
                    ddlTransferTo_SelectedIndexChanged(sender, e)
                ElseIf group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Admin ::::")
                ElseIf group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                    ddlTransferTo.Items.Add(":::: Select Customer ::::")
                ElseIf group.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                    ddlTransferTo.Items.Add(":::: Select Customer ::::")
                ElseIf group.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    ddlTransferTo.Items.Add(":::: Select Master Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Distributor ::::")
                    ddlTransferTo.Items.Add(":::: Select Retailer ::::")
                    ddlTransferTo.Items.Add(":::: Select Customer ::::")
                ElseIf group.Trim.ToUpper = "Customer".Trim.ToUpper Then

                Else
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            If group = "Master Distributor" Or group = "Distributor" Or group = "Retailer" Then
                If GV.parseString(txtCustomerID.Text.Trim) = "" Then
                    lblError.Text = "Please Enter ID."
                    lblError.CssClass = "errorlabels"
                    txtCustomerID.Focus()
                    Exit Sub
                End If
            ElseIf group = "Customer" Then
                If rb_Customer.Checked = True Then
                    If GV.parseString(txtCustomerID.Text.Trim) = "" Then
                        lblError.Text = "Please Enter Customer ID."
                        lblError.CssClass = "errorlabels"
                        txtCustomerID.Focus()
                        Exit Sub
                    End If
                Else
                    If ddlTransferToAgent.SelectedIndex = 0 Then
                        lblError.Text = "Select Transfer To " & lblAgentType.Text.Trim
                        lblError.CssClass = "errorlabels"
                        Exit Sub
                    End If
                End If
            Else

                If ddlTransferToAgent.SelectedIndex = 0 Then
                    lblError.Text = "Select Transfer To " & lblAgentType.Text.Trim
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

            End If
            If txtTransferAmt.Text = "" Then
                lblError.Text = "Enter Transfer Amount"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            If txtTransactionPin.Text = "" Then
                lblError.Text = "Please Enter Your transaction Pin"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If




            Dim TransPiNo As String = ""
            If group = "Master Distributor" Or group = "Distributor" Or group = "Retailer" Or group = "Customer" Then
                TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            Else
                TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            End If

            If TransPiNo.Trim = txtTransactionPin.Text.Trim Then
            Else
                lblError.Text = "Invalid transaction Pin"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            If group = "Master Distributor" Or group = "Distributor" Or group = "Retailer" Or group = "Customer" Then

                If ddlPaymentMode.SelectedValue = "PLUS" Then
                    Dim holdAmt As String = ""
                    holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                    If holdAmt.Trim = "" Then
                        holdAmt = "0"
                    End If
                    If CLng(txtTransferAmt.Text.Trim) > CLng(txtActualAvaitrasferAmt.Text.Trim) Then
                        lblError.Text = "Transfer Amt Not Greater Than Actual Available Amt."
                        lblError.CssClass = "errorlabels"
                        btnTransfer.Focus()
                        Exit Sub
                    End If

                    If CLng(txtTransferAmt.Text.Trim) > (CLng(txtActualAvaitrasferAmt.Text.Trim) - CDec(holdAmt)) Then
                        lblError.Text = "You Have Insufficient Wallet Amount"
                        lblError.CssClass = "errorlabels"
                        btnTransfer.Focus()
                        Exit Sub
                    End If
                Else
                    '/// case wallet minus
                    '
                    Dim bal As Decimal = GV.returnWalletBalCalculation(GV.parseString(txtCustomerID.Text.Trim), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)

                    If CDec(txtTransferAmt.Text.Trim) > bal Then
                        lblError.Text = "Insufficient Wallet Bal In " & GV.parseString(txtCustomerID.Text.Trim) & " [ " & bal & " ]"
                        lblError.CssClass = "errorlabels"
                        btnTransfer.Focus()
                        Exit Sub
                    End If


                End If


            ElseIf group.ToString.Trim.ToUpper = "Admin".Trim.ToUpper Then

                If ddlPaymentMode.SelectedValue = "PLUS" Then
                    Dim holdAmt As String = ""
                    holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_ClientRegistration] where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'")
                    If holdAmt.Trim = "" Then
                        holdAmt = "0"
                    End If
                    If CLng(txtTransferAmt.Text.Trim) > CLng(txtActualAvaitrasferAmt.Text.Trim) Then
                        lblError.Text = "Transfer Amt Not Greater Than Actual Available Amt."
                        lblError.CssClass = "errorlabels"
                        btnTransfer.Focus()
                        Exit Sub
                    End If

                    If CLng(txtTransferAmt.Text.Trim) > (CLng(txtActualAvaitrasferAmt.Text.Trim) - CDec(holdAmt)) Then
                        lblError.Text = "You Have Insufficient Wallet Amount"
                        lblError.CssClass = "errorlabels"
                        btnTransfer.Focus()
                        Exit Sub
                    End If
                End If


            ElseIf group.ToString.Trim.ToUpper = "Super Admin".Trim.ToUpper Then



            End If


            If btnTransfer.Text = "Transfer Amt" Then
                Session("EditFlag") = 0
                btnPopupYes.Text = "Yes"
                btnPopupYes.Visible = True
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to TransferAmt ??"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
                'Else
                '    btnPopupYes.Text = "Yes"
                '    btnPopupYes.Visible = True
                '    btnCancel.Attributes("Style") = ""
                '    btnCancel.Text = "No"
                '    lblDialogMsg.Text = "Are You sure you want to Update??"
                '    lblDialogMsg.CssClass = ""
                '    ModalPopupExtender1.Show()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub Clear()
        Try

            lblAmt_Transfer_TransID.Text = GV.FL.getAutoNumber("TransId")
            If ddlTransferToAgent.Items.Count > 0 Then
                ddlTransferToAgent.SelectedIndex = 0
            End If
            txtCustomerID.Text = ""
            txtCustomerName.Text = ""

            txtTransactionPin.Text = ""
            txtRemark.Text = ""
            txtTransferAmt.Text = ""
            lblError.Text = ""
            lblError.CssClass = ""
            lblRID.Text = ""
            Session("Editflag") = 0
            btnTransfer.Text = "Transfer Amt"
            Calculation()



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Private Sub btnPopupYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopupYes.Click
        Try

            If btnPopupYes.Text.Trim.ToUpper = "Ok".Trim.ToUpper Then
                Clear()
            End If
            lblError.Text = ""
            lblError.CssClass = ""

            Dim Group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim

            Dim VTransferTo, VTransferFrom, VTransferAmt, VRemark, TransactionDate, DatabaseName As String
            '//////// ======End get All Uploaded Image Path ==================================

            DatabaseName = GV.get_SuperAdmin_SessionVariables("DatabaseName", Request, Response).Trim
            VTransferFrom = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            If Group = "Master Distributor" Or Group = "Distributor" Or Group = "Retailer" Or Group = "Customer" Then
                VTransferTo = GV.parseString(txtCustomerID.Text.Trim)
                'If rb_Customer.Checked = True Then
                '    VTransferTo = GV.parseString(txtCustomerID.Text.Trim)
                'Else
                '    If ddlTransferToAgent.SelectedIndex = 0 Then
                '        lblError.Text = "Select Transfer To " & lblAgentType.Text.Trim
                '        lblError.CssClass = "errorlabels"
                '        Exit Sub
                '    End If

                '    Dim aa() As String = GV.parseString(ddlTransferToAgent.SelectedValue.Trim).Split(":")
                '    VTransferTo = aa(0)
                'End If
            ElseIf Group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then

                If ddlTransferToAgent.SelectedIndex = 0 Then
                    lblError.Text = "Select Transfer To " & lblAgentType.Text.Trim
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim aa() As String = GV.parseString(ddlTransferToAgent.SelectedValue.Trim).Split(":")
                'VTransferTo = aa(0)
                VTransferFrom = "Super Admin"
                VTransferTo = "Admin"
                DatabaseName = GV.FL_AdminLogin.AddInVar("DatabaseName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & aa(0).Trim & "' ").Trim

                If DatabaseName.Trim = "" Then
                    DatabaseName = "BosCenter_DB"
                End If

            Else

                If ddlTransferToAgent.SelectedIndex = 0 Then
                    lblError.Text = "Select Transfer To " & lblAgentType.Text.Trim
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim aa() As String = GV.parseString(ddlTransferToAgent.SelectedValue.Trim).Split(":")
                VTransferTo = aa(0)


            End If

            If txtTransferAmt.Text = "" Then
                lblError.Text = "Enter Transfer Amount"
                lblError.CssClass = "errorlabels"
                Exit Sub
            Else
                VTransferAmt = GV.parseString(txtTransferAmt.Text.Trim)
            End If

            If Not txtRemark.Text = "" Then
                VRemark = GV.parseString(txtRemark.Text.Trim)
            Else
                VRemark = ""
            End If

            Dim VUpdatedBy, VUpdatedOn, VRecord_DateTime As String

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            VUpdatedOn = "getdate()"
            TransactionDate = Now.Date
            VRecord_DateTime = "getDate()"
            'Vpassword = GV.RandomPaswrd()
            'Dim trnsactionpinNo As String = GV.RandomTransactionPin


            Dim VGroup As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

            Dim VTransferFromMsg As String = ""
            Dim VTransferToMsg As String = ""
            Dim SMSMeassgeTo As String = ""
            Dim SMSMeassgeFrom As String = ""
            Dim AmouontType As String = ""
            AmouontType = "Deposit"

            If VGroup = "SUPER ADMIN" Then
                Dim aa() As String = GV.parseString(ddlTransferToAgent.SelectedValue.Trim).Split(":")

                If ddlPaymentMode.SelectedValue = "PLUS" Then
                    VTransferFromMsg = "Your Wallet is Debited by Admin (" & aa(0).Trim & ")"
                    VTransferToMsg = "Your Wallet is Credited by ADMIN."
                    SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By ADMIN."
                Else
                    AmouontType = "Withdraw"
                    VTransferToMsg = "Your Wallet is Credited by Admin (" & aa(0).Trim & ")"
                    VTransferFromMsg = "Your Wallet is Debited by ADMIN."
                    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By ADMIN."
                    Dim Swapping As String = ""
                    Swapping = VTransferFrom
                    VTransferFrom = VTransferTo
                    VTransferTo = Swapping
                End If

            ElseIf VGroup = "ADMIN" Then

                If ddlPaymentMode.SelectedValue = "PLUS" Then

                    VTransferFromMsg = "Your Wallet is Debited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferToMsg = "Your Wallet is Credited by ADMIN."
                    SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By ADMIN."
                Else
                    AmouontType = "Withdraw"
                    VTransferToMsg = "Your Wallet is Credited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferFromMsg = "Your Wallet is Debited by ADMIN."
                    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By ADMIN."
                    Dim Swapping As String = ""
                    Swapping = VTransferFrom
                    VTransferFrom = VTransferTo
                    VTransferTo = Swapping
                End If

            ElseIf VGroup = "MASTER DISTRIBUTOR" Then

                If ddlPaymentMode.SelectedValue = "PLUS" Then

                    VTransferFromMsg = "Your Wallet is Debited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferToMsg = "Your Wallet is Credited by Master Distributor (" & VTransferFrom & ")"
                    SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By Master Distributor (" & VTransferFrom & ")"
                Else
                    AmouontType = "Withdraw"
                    VTransferToMsg = "Your Wallet is Credited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferFromMsg = "Your Wallet is Debited by Master Distributor (" & VTransferFrom & ")"
                    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Master Distributor (" & VTransferFrom & ")"
                    Dim Swapping As String = ""
                    Swapping = VTransferFrom
                    VTransferFrom = VTransferTo
                    VTransferTo = Swapping
                End If




                'If rb_Customer.Checked = True Then
                '    VTransferFromMsg = "Your Wallet is Debited by Customer (" & VTransferFrom & ")"
                '    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Customer (" & VTransferTo & ")"
                'Else
                '    VTransferFromMsg = "Your Wallet is Debited by Distributor (" & VTransferFrom & ")"
                '    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Distributor (" & VTransferTo & ")"
                'End If

                'VTransferToMsg = "Your Wallet is Credited by Master Distributor (" & VTransferTo & ")"
                'SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By Master Distributor (" & VTransferFrom & ")"

            ElseIf VGroup = "DISTRIBUTOR" Then

                If ddlPaymentMode.SelectedValue = "PLUS" Then

                    VTransferFromMsg = "Your Wallet is Debited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferToMsg = "Your Wallet is Credited by Distributor (" & VTransferFrom & ")"
                    SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By Distributor (" & VTransferFrom & ")"
                Else
                    AmouontType = "Withdraw"
                    VTransferToMsg = "Your Wallet is Credited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferFromMsg = "Your Wallet is Debited by Distributor (" & VTransferFrom & ")"
                    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Distributor (" & VTransferFrom & ")"
                    Dim Swapping As String = ""
                    Swapping = VTransferFrom
                    VTransferFrom = VTransferTo
                    VTransferTo = Swapping
                End If



                'If rb_Customer.Checked = True Then
                '    VTransferFromMsg = "Your Wallet is Debited by Customer (" & VTransferTo & ")"
                '    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Customer (" & VTransferTo & ")"
                'Else
                '    VTransferFromMsg = "Your Wallet is Debited by Retailer (" & VTransferTo & ")"
                '    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Retailer (" & VTransferTo & ")"
                'End If


                'VTransferToMsg = "Your Wallet is Credited by Distributor (" & VTransferFrom & ")"
                'SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By Distributor (" & VTransferFrom & ")"


            ElseIf VGroup = "Retailer".ToUpper Then
                If ddlPaymentMode.SelectedValue = "PLUS" Then

                    VTransferFromMsg = "Your Wallet is Debited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferToMsg = "Your Wallet is Credited by Retailer (" & VTransferFrom & ")"
                    SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By Retailer (" & VTransferFrom & ")"
                Else
                    AmouontType = "Withdraw"
                    VTransferToMsg = "Your Wallet is Credited by " & lblAgentType.Text.Trim & " (" & VTransferTo & ")"
                    VTransferFromMsg = "Your Wallet is Debited by Retailer (" & VTransferFrom & ")"
                    SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Retailer (" & VTransferFrom & ")"
                    Dim Swapping As String = ""
                    Swapping = VTransferFrom
                    VTransferFrom = VTransferTo
                    VTransferTo = Swapping
                End If

                'VTransferFromMsg = "Your Wallet is Debited by Customer (" & VTransferTo & ")"
                'SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Customer (" & VTransferTo & ")"
                'VTransferToMsg = "Your Wallet is Credited by Retailer (" & VTransferFrom & ")"
                'SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By Retailer (" & VTransferFrom & ")"


            ElseIf VGroup = "Customer".ToUpper Then
                VTransferFromMsg = "Your Wallet is Debited by Customer (" & VTransferTo & ")"
                SMSMeassgeFrom = "Your Wallet is Debited With Rs. " & txtTransferAmt.Text.Trim & " By Customer (" & VTransferTo & ")"
                VTransferToMsg = "Your Wallet is Credited by Customer (" & VTransferFrom & ")"
                SMSMeassgeTo = "Your Wallet is Credited With Rs. " & txtTransferAmt.Text.Trim & " By Customer (" & VTransferFrom & ")"
            End If



            If Session("EditFlag") = 0 Then

                If Not GV.FL.RecCount(" " & DatabaseName & ".dbo.BOS_TransferAmountToAgents Where Amt_Transfer_TransID='" & lblAmt_Transfer_TransID.Text.Trim & "' ") > 0 Then 'Change where condition according to Criteria 

                    Dim QryStr As String = "insert into " & DatabaseName & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & lblAmt_Transfer_TransID.Text.Trim & "','" & lblAmt_Transfer_TransID.Text.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & AmouontType & "','" & VRemark & "','" & TransactionDate & "','" & VTransferFrom & "','" & VTransferTo & "','" & VTransferAmt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                    If GV.FL.DMLQueries(QryStr) = True Then
                        Session("EditFlag") = 1
                        Dim FromMobile As String = ""
                        Dim ToMobile As String = ""
                        'BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode=
                        If Group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                            FromMobile = GV.FL.AddInVar("MobileNo", " " & GV.DefaultDatabase.Trim & ".dbo.CRM_Login_Details where [User_ID]='SR' ")
                            ToMobile = GV.FL.AddInVar("MobileNo", " " & DatabaseName & ".dbo.CRM_Login_Details where [User_ID]='Admin' ")
                        Else
                            FromMobile = GV.FL.AddInVar("MobileNo", " " & DatabaseName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferFrom & "' ")
                            ToMobile = GV.FL.AddInVar("MobileNo", " " & DatabaseName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")

                            If ddlPaymentMode.SelectedValue = "PLUS" Then
                                If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then
                                    Dim vName As String = GV.FL.AddInVar("isnull(FirstName,'') + ' ' + isnull(LastName,'')", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferTo & "' ")
                                    Dim vBal As String = GV.AgentBalance(VTransferTo, DatabaseName).ToString
                                    Dim vAgencyName As String = GV.FL.AddInVar("AgencyName", " " & DatabaseName & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & VTransferFrom & "' ")


                                    If GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim = "CMP1165" Then
                                        Dim vMessage As String = "Dear " & vName & " Your KM Account Has Been Credited With Rs. " & txtTransferAmt.Text.Trim & " Your Current Balance Is Rs. " & vBal & " . By " & vAgencyName & " Thanks For Using Kuber Money"
                                        GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Amount Transfer", "CMP1165")

                                    ElseIf GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim = "CMP1174" Then
                                        Dim vMessage As String = "Dear " & vName & " Your Easy Talk Account Has Been Credited With Rs. " & txtTransferAmt.Text.Trim & " Your Current Bal. is Rs. " & vBal & " .Thanks Easy Talk. Web: https://bit.ly/3p1OkPj App: https://bit.ly/3Szblqb"
                                        GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Amount Transfer", "CMP1174")
                                    Else

                                        Dim vMessage As String = "Dear " & vName & " Your BOS Account Has Been Credited With Rs. " & txtTransferAmt.Text.Trim & " Your Current Balance Is Rs. " & vBal & " . Thanks For Using BOS."
                                        GV.send_Template_Based_SMS_API(ToMobile, vMessage, "Amount Transfer", "")
                                    End If
                                End If
                            End If

                        End If

                        'If Not FromMobile.Trim = "" Then
                        '    GV.sendSMSThroughAPI(FromMobile, SMSMeassgeFrom)
                        'End If

                        'If Not ToMobile.Trim = "" Then
                        '    GV.sendSMSThroughAPI(ToMobile, SMSMeassgeTo)
                        'End If




                        Clear()
                        lblDialogMsg.Text = "Amount Transfer Successfully."
                        lblDialogMsg.CssClass = "Successlabels"
                        btnCancel.Text = "Ok"
                        btnPopupYes.Visible = False
                        ModalPopupExtender1.Show()
                    Else
                        lblDialogMsg.Text = "Transfer Failed."
                        lblDialogMsg.CssClass = "errorlabels"
                        btnCancel.Text = "Ok"
                        btnPopupYes.Visible = False
                        ModalPopupExtender1.Show()
                    End If
                Else
                    lblDialogMsg.Text = "Amount Transfer Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Visible = False
                    ModalPopupExtender1.Show()
                End If



            End If



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
            If group = "Master Distributor" Or group = "Distributor" Or group = "Retailer" Or group = "Customer" Then
                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                txtMyCreditLimit.Text = CreditBAl
            ElseIf group.Trim.ToUpper = "Admin".Trim.ToUpper Then
                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                txtMyCreditLimit.Text = CreditBAl
                txtMainBalance.Text = GV.returnAdminWalletBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
            Else
                Dim CreditBAl As String = GV.FL.AddInVar("CreditBalnceLimit", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_Login_Details where User_ID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                If CreditBAl = "" Then
                    CreditBAl = "0"
                End If
                txtMyCreditLimit.Text = CreditBAl
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
            If txtMainBalance.Text.Contains("-") Then
                txtAvailableCredit.Text = CDec(txtMyCreditLimit.Text.Trim) + CDec(txtMainBalance.Text.Trim)
            Else
                txtAvailableCredit.Text = txtMyCreditLimit.Text.Trim
            End If

            txtActualAvaitrasferAmt.Text = CDec(txtMyCreditLimit.Text.Trim) + CDec(txtMainBalance.Text.Trim) - CDec(txtHoldAmt.Text.Trim)

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlTransferTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransferTo.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            txtCustomerID.Text = ""
            txtCustomerName.Text = ""

            If ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Master Distributor ::::".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Master Distributor' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select Master Distributor ::::")
                Else
                    ddlTransferToAgent.Items.Add(":::: Select Master Distributor ::::")
                End If
                lblAgentType.Text = "Master Distributor"
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Distributor ::::".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Distributor' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select Distributor ::::")
                Else
                    ddlTransferToAgent.Items.Add(":::: Select Distributor ::::")
                End If
                lblAgentType.Text = "Distributor"
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Retailer ::::".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Retailer' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select Retailer ::::")
                Else
                    ddlTransferToAgent.Items.Add(":::: Select Retailer ::::")
                End If
                lblAgentType.Text = "Retailer"
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Customer ::::".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+ (isnull(FirstName,'')+' '+isnull(LastName,''))", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='Customer' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select Customer ::::")
                Else
                    ddlTransferToAgent.Items.Add(":::: Select Customer ::::")
                End If
                lblAgentType.Text = "Customer"
            ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Admin ::::".Trim.ToUpper Then
                GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, " CompanyCode +':'+ isnull(CompanyName,'') ", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where Status='Active' ")
                If ddlTransferToAgent.Items.Count > 0 Then
                    ddlTransferToAgent.Items.Insert(0, ":::: Select Admin ::::")
                Else
                    ddlTransferToAgent.Items.Add(":::: Select Admin ::::")
                End If
                lblAgentType.Text = "Admin"
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub rb_Customer_CheckedChanged(sender As Object, e As EventArgs) Handles rb_Customer.CheckedChanged
        Try

            If rb_Customer.Checked = True Then
                lblAgentType.Text = "Customer"
                ddlTransferToAgent.Visible = False

                txtCustomerID.Text = ""
                txtCustomerName.Text = ""

                txtCustomerID.Visible = True
                txtCustomerName.Visible = True

                txtCustomerName.ReadOnly = True
                txtCustomerID.ReadOnly = False

            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub rb_TransferToType_CheckedChanged(sender As Object, e As EventArgs) Handles rb_TransferToType.CheckedChanged
        Try
            If rb_TransferToType.Checked = True Then

                txtCustomerID.Visible = False
                txtCustomerName.Visible = False
                ddlTransferToAgent.Visible = True
                txtCustomerID.Text = ""
                txtCustomerName.Text = ""



                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                Dim Loginid As String = ""
                Div_MyAcountdetails.Visible = True
                If group.Trim.ToUpper = "Admin".Trim.ToUpper Or group.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                    'ddlTransferTo.Visible = True
                    'Label7.Visible = False
                    'ddlPaymentMode.Visible = True
                    'Label11.Visible = True
                    'lblAgentType.Visible = False
                    'Div_MyAcountdetails.Visible = False
                    'ddlTransferTo_SelectedIndexChanged(sender, e)
                Else
                    lblAgentType.Visible = True
                    ddlTransferTo.Visible = False
                    Label7.Visible = True
                    ddlPaymentMode.Visible = False
                    Label11.Visible = False
                    If group = "Master Distributor" Then
                        Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                        group = "Distributor"
                        Div_MyAcountdetails.Visible = True
                    ElseIf group = "Distributor" Then
                        Loginid = " And RefrenceID='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "' "
                        group = "Retailer"
                        Div_MyAcountdetails.Visible = True
                    ElseIf group = "Retailer" Or group = "Customer" Then

                        'Div_MyAcountdetails.Visible = True
                        'lblAgentType.Text = "Customer"
                        'rb_TransferToType.Visible = False
                        'rb_Customer.Checked = True
                        'rb_Customer_CheckedChanged(sender, e)
                        'Calculation()
                        'Session("EditFlag") = 0
                        'Exit Sub
                    Else
                        Div_MyAcountdetails.Visible = False
                        Loginid = ""
                        group = "Distributor"
                    End If
                    lblAgentType.Text = group
                    rb_TransferToType.Text = group

                    GV.FL.AddInDropDownListDistinct(ddlTransferToAgent, "RegistrationId+':'+AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & group & "' " & Loginid & "")
                    If ddlTransferToAgent.Items.Count > 0 Then
                        ddlTransferToAgent.Items.Insert(0, ":::: Select " & group & " ::::")
                    Else
                        ddlTransferToAgent.Items.Add(":::: Select " & group & " ::::")
                    End If
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged
        Try

            If GV.parseString(txtCustomerID.Text.Trim) = "" Then
                txtCustomerName.Text = ""
                'txtCustomerName.CssClass = "form-control"
                txtCustomerID.Focus()
            ElseIf GV.parseString(txtCustomerID.Text.Trim).ToUpper = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).ToUpper Then

                txtCustomerName.ForeColor = Drawing.Color.Red
                txtCustomerName.Text = "Same ID Is Not Allowed"

                txtCustomerID.Text = ""
                txtCustomerID.Focus()

            Else
                Dim CustID As String = GV.parseString(txtCustomerID.Text.Trim)

                Dim AgentType As String = ""

                If ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Master Distributor ::::".Trim.ToUpper Then
                    AgentType = "Master Distributor"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Distributor ::::".Trim.ToUpper Then
                    AgentType = "Distributor"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Retailer ::::".Trim.ToUpper Then
                    AgentType = "Retailer"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Customer ::::".Trim.ToUpper Then
                    AgentType = "Customer"
                ElseIf ddlTransferTo.SelectedValue.Trim.ToUpper = ":::: Select Admin ::::".Trim.ToUpper Then
                    AgentType = "Admin"
                End If

                If ddlPaymentMode.SelectedValue = "PLUS" Then
                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & AgentType.Trim & "'  and ActiveStatus='Active' and RegistrationId='" & CustID & "' ") > 0 Then
                        txtCustomerName.ForeColor = Drawing.Color.Blue
                        txtCustomerName.Text = GV.FL.AddInVar(" (isnull(FirstName,'')+' '+isnull(LastName,'')) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & AgentType.Trim & "'  and ActiveStatus='Active' and RegistrationId='" & CustID & "' ")
                    Else
                        txtCustomerName.ForeColor = Drawing.Color.Red
                        txtCustomerName.Text = "Invalid ID"

                        txtCustomerID.Text = ""
                        txtCustomerID.Focus()
                    End If
                Else
                    ' /// Case MINUS
                    Dim loginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).ToUpper

                    If GV.FL.RecCount("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & AgentType.Trim & "'  and ActiveStatus='Active' and RegistrationId='" & CustID & "' and RefrenceID='" & loginID & "' ") > 0 Then
                        txtCustomerName.ForeColor = Drawing.Color.Blue
                        txtCustomerName.Text = GV.FL.AddInVar(" (isnull(FirstName,'')+' '+isnull(LastName,'')) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where AgentType='" & AgentType.Trim & "'  and ActiveStatus='Active' and RegistrationId='" & CustID & "'  and RefrenceID='" & loginID & "' ")
                    Else
                        txtCustomerName.ForeColor = Drawing.Color.Red
                        txtCustomerName.Text = "Invalid ID"

                        txtCustomerID.Text = ""
                        txtCustomerID.Focus()
                    End If
                End If


            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class
