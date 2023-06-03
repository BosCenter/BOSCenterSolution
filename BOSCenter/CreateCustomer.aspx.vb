Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports InstamojoAPI
Imports System.Security.Cryptography

Public Class CreateCustomer
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("SUPERADMIN")
    '//// ----------------Variable Declaration  ----------------
    Dim VRegisterDate, VAgentType, VAgencyName, VFirstName, VEmailID, VDOB, VAlternateMobileNo, VPermanentAddress, VState, VAddharCardNo, VWebSite, VRegistrationId, VPanCardNumber, VMobileNo, VOfficeAddress, VCity, VLastName, VPincode, VBusinessType, VGSTNO As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""
    Dim VError_String As String = ""
    Dim Request_Transaction_Id, Request_name, Request_email, Request_phone, Request_amount, Request_redirect_url, Request_CompanyCode, Request_Purpose, Request_AgentID, Request_TransID As String
    Dim Response_DateTime, Response_Payment_Id, Response_Payment_Status, Response_Id, Response_Transaction_Id, Response_Action_Taken As String

    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then

                formheading3.Text = "Signup Form"
                lblformsectionhead3.Text = "Registration Details"
                ddlAgentType.SelectedValue = "Customer"

                ddlAgentType.CssClass = "form-control"
                txtRefrenceID.Text = ""
                txtRefrenceType.Text = ""


                If Not Request.QueryString("admin") Is Nothing Then
                    If Not Request.QueryString("admin").Trim = "" Then

                        Dim DBName As String = GV.FL_AdminLogin.AddInVar("DatabaseName", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & Request.QueryString("admin").Trim & "' and [Status]='Active' ")
                        If DBName.Trim = "" Then
                            txtCompanyCode.Text = "CMP1045"
                            txtDBName.Text = "BosCenter_DB"
                        Else
                            txtCompanyCode.Text = Request.QueryString("admin").Trim.ToUpper
                            txtDBName.Text = DBName.Trim
                        End If
                    Else
                        txtCompanyCode.Text = "CMP1045"
                        txtDBName.Text = "BosCenter_DB"
                    End If
                Else
                    txtCompanyCode.Text = "CMP1045"
                    txtDBName.Text = "BosCenter_DB"
                End If

                ddlAgentType.CssClass = "form-control"

                lblError.Text = ""
                lblError.CssClass = ""


                ddl_Ref_Code.Items.Clear()
                GV.FL.AddInDropDownListDistinct(ddl_Ref_Code, "Ref_Code", "" & txtDBName.Text & ".dbo.BOS_Ref_Code_Master order by Ref_Code ")
                If ddl_Ref_Code.Items.Count > 0 Then
                    ddl_Ref_Code.Items.Insert(0, ":::: Select Code ::::")
                Else
                    ddl_Ref_Code.Items.Add(":::: Select Code ::::")
                End If
                lblServiceCharge.Text = "0"


                Session("EditFlag") = 0
                Session("Workfor") = "Save"

                If txtDBName.Text.Trim.ToUpper = "BosCenter_DB".Trim.ToUpper Then
                    txtCompanyName.Text = "BOS Money"
                Else
                    Dim CompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " " & txtDBName.Text.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & txtCompanyCode.Text.Trim & "' ")
                    txtCompanyName.Text = GV.parseString(CompanyName.Trim)
                End If


                txtCompanyName.ReadOnly = True

                ddlAgentType_SelectedIndexChanged(sender, e)
                btnSave.Text = "Submit & Proceed"
                btnClear.Text = "Close"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("Default.aspx?admin=" & txtCompanyCode.Text.Trim) '/Change the name of form
            Else
                Clear()
                ddlAgentType_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Dim VCreditBalLimit As String = ""
    Public Shared Sub OpenWindow(ByVal currentPage As Page, ByVal window As String, ByVal htmlPage As String, ByVal width As Int32, ByVal height As Int32)
        Dim sb As New System.Text.StringBuilder()
        sb.Append("popWin=window.open('")
        sb.Append(htmlPage)
        sb.Append("','")
        sb.Append(window)
        sb.Append("','width=")
        sb.Append(width)
        sb.Append(",height=")
        sb.Append(height)
        sb.Append(",toolbar=no,location=no, directories=no,status=no,menubar=no,scrollbars=no,resizable=no")
        sb.Append("');")
        sb.Append("popWin.focus();")

        ScriptManager.RegisterClientScriptBlock(currentPage, GetType(Page), "OpenWindow", sb.ToString(), True)
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim isErrorFound As Boolean = False
            Dim isFocusApplied As Boolean = False
            lblError.Text = ""
            lblError.CssClass = ""



            'If txtRefrenceID.Text.Trim = "" Then
            '    txtRefrenceID.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtRefrenceID.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtRefrenceID.CssClass = "form-control"
            'End If

            If txtCompanyName.Text.Trim = "" Then
                txtCompanyName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtCompanyName.Focus()
                    isFocusApplied = True
                End If
            Else
                txtCompanyName.CssClass = "form-control"
            End If
            If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                If ddl_Ref_Code.SelectedIndex = 0 Then
                    ddl_Ref_Code.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        ddl_Ref_Code.Focus()
                        isFocusApplied = True
                    End If
                Else
                    ddl_Ref_Code.CssClass = "form-control"
                End If
            Else
                If txtAgencyName.Text = "" Then
                    txtAgencyName.CssClass = "ValidationError"
                    isErrorFound = True
                    If isFocusApplied = False Then
                        txtAgencyName.Focus()
                        isFocusApplied = True
                    End If
                Else
                    txtAgencyName.CssClass = "form-control"
                End If
            End If


            'If txtPanCardNumber.Text.Trim = "" Then
            '    txtPanCardNumber.CssClass = "ValidationError"
            '    isErrorFound = True
            '    If isFocusApplied = False Then
            '        txtPanCardNumber.Focus()
            '        isFocusApplied = True
            '    End If
            'Else
            '    txtPanCardNumber.CssClass = "form-control"
            'End If

            If txtFirstName.Text.Trim = "" Then
                txtFirstName.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtFirstName.Focus()
                    isFocusApplied = True
                End If
            Else
                VFirstName = (GV.parseString(txtFirstName.Text.Trim) & " " & GV.parseString(txtLastName.Text.Trim)).ToString.Trim
                txtFirstName.CssClass = "form-control"
            End If

            If txtEmailID.Text.Trim = "" Then
                txtEmailID.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtEmailID.Focus()
                    isFocusApplied = True
                End If

            Else
                txtEmailID.CssClass = "form-control"
            End If

            If txtMobileNo.Text.Trim = "" Then
                txtMobileNo.CssClass = "ValidationError"
                isErrorFound = True
                If isFocusApplied = False Then
                    txtMobileNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not IsNumeric(txtMobileNo.Text) Then
                txtMobileNo.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String = "" Then
                    VError_String = "Please Enter Correct Mobile No."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter Correct Mobile No."
                End If
                If isFocusApplied = False Then
                    txtMobileNo.Focus()
                    isFocusApplied = True
                End If
            ElseIf Not txtMobileNo.Text.Length = 10 Then
                txtMobileNo.CssClass = "ValidationError"
                isErrorFound = True
                If VError_String = "" Then
                    VError_String = "Please Enter 10 Digit Mobile No."
                Else
                    VError_String = VError_String & "<br>" & "Please Enter 10 Digit Mobile No."
                End If
                If isFocusApplied = False Then
                    txtMobileNo.Focus()
                    isFocusApplied = True
                End If
            Else
                txtMobileNo.CssClass = "form-control"
            End If


            'If isErrorFound = True Then
            '    Exit Sub
            'End If

            'VRegistrationId = txtRegistrationId.Text

            'Request_TransID = GV.FL_AdminLogin.getAutoNumber("TransId")
            'Dim New_transId As String = txtCompanyCode.Text & "_" & "CC" & "_" & VRegistrationId & "_" & Request_TransID

            'Request_name = VFirstName.Trim
            'Request_email = VEmailID
            'Request_phone = VMobileNo
            'Request_amount = lblServiceCharge.Text
            'Request_Transaction_Id = New_transId
            'VRef_Code = txtRefrenceID.Text.Trim

            'Try
            '    'Dim txnID As String = Request_Transaction_Id
            '    '/// create cookie start
            '    GV.set_OrderID_SessionVariables(Request_Transaction_Id, Request, Response)
            '    '/// create cookie end


            '    Dim Text As String = MERCHANT_KEY & "|" & Request_Transaction_Id & "|" & Convert.ToDecimal(Request_amount).ToString("g29") & "|" & "Shopping" & "|" & Request_name & "|" & Request_email & "|||||||||||" & SALT_KEY

            '    Dim message As Byte() = Encoding.UTF8.GetBytes(Text)
            '    Dim UE As UnicodeEncoding = New UnicodeEncoding()
            '    Dim hashValue As Byte()
            '    Dim hash As String = ""
            '    Dim hashString As SHA512Managed = New SHA512Managed()
            '    Dim hex As String = ""
            '    hashValue = hashString.ComputeHash(message)
            '    For Each x As Byte In hashValue
            '        hex += String.Format("{0:x2}", x)
            '    Next
            '    hash = hex

            '    Dim data As System.Collections.Hashtable = New System.Collections.Hashtable()

            '    data.Add("hash", hex.ToString)
            '    data.Add("txnid", Request_Transaction_Id.ToString)
            '    data.Add("key", MERCHANT_KEY.ToString)

            '    data.Add("amount", Convert.ToDecimal(Request_amount).ToString("g29"))
            '    data.Add("productinfo", "Shopping")
            '    data.Add("firstname", Request_name)
            '    data.Add("email", Request_email)
            '    data.Add("phone", Request_phone)
            '    Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
            '    Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")
            '    Dim sul As String = strUrl & "Payu_Handler_Success.aspx"
            '    Dim ful As String = strUrl & "Payu_Handler_Fail.aspx"
            '    Dim cul As String = strUrl & "Payu_Handler_Cancel.aspx"
            '    data.Add("surl", sul)
            '    data.Add("furl", ful)
            '    data.Add("lastname", "")
            '    data.Add("curl", cul)
            '    data.Add("address1", "test address 1")
            '    data.Add("address2", "test address 2")
            '    data.Add("city", "Delhi")
            '    data.Add("state", "Delhi")
            '    data.Add("country", "India")
            '    data.Add("zipcode", "110085")
            '    data.Add("udf1", "")
            '    data.Add("udf2", "")
            '    data.Add("udf3", "")
            '    data.Add("udf4", "")
            '    data.Add("udf5", "")
            '    data.Add("pg", "")
            '    data.Add("salt", SALT_KEY)
            '    data.Add("service_provider", "payu_paisa")
            '    'Dim html As String = "<html><head>"
            '    'html += "</head><body onload='document.forms[0].submit()'>"
            '    Dim strForm As String = GV.PreparePOSTForm("https://secure.payu.in/_payment", data)
            '    'html += strForm
            '    'html += "</body></html>"


            '    'OpenWindow(Me.Page, "mywindow",  html, 800, 600)
            '    Dim str As String = " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details (TransIpAddress,Request_DateTime,Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,Request_redirect_url,Request_CompanyCode,Request_Purpose,Request_AgentID,Request_TransID,UpdatedOn,UpdatedBy) values ('" & GV.parseString(GV.GetIPAddress) & "',getdate(),'" & Request_Transaction_Id & "','" & Request_name & "','" & Request_email & "','" & Request_phone & "','" & Request_amount & "','" & Request_redirect_url & "','" & Request_CompanyCode & "','" & Request_Purpose & "','" & Request_AgentID & "','" & Request_TransID & "',getdate(),'Admin')"
            '    GV.FL.DMLQueriesBulk(str)

            '    'Response.Clear()
            '    'Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1")
            '    'Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1")
            '    'Response.Charset = "ISO-8859-1"
            '    'Response.Write(html)
            '    'Response.End()

            '    Response.Write(strForm)

            '    'Page.Controls.Add(New LiteralControl(strForm))



            '    'result = strForm.ToString commented now
            '    'Response.Write(PreparePOSTForm("https://test.payu.in/_payment", data))


            '        Catch ex As Exception 
            ' GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

            'End Try



            'Exit Sub






            VAddharCardNo = ""

            Dim strQry, QryMsg As String

            If VAddharCardNo.Trim = "" Then
                strQry = "" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where ( MobileNo='" & GV.parseString(txtMobileNo.Text) & "') and ( AgentType='" & ddlAgentType.SelectedValue & "' ) "
                QryMsg = "Mobile Already Exists."
            Else
                strQry = "" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where ( MobileNo='" & GV.parseString(txtMobileNo.Text) & "' ' ) and ( AgentType='" & ddlAgentType.SelectedValue & "' ) "
                QryMsg = "Mobile Already Exists."
            End If


            If GV.FL.RecCount(strQry) > 0 Then 'Change where condition according to Criteria 
                lblError.Text = QryMsg
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If


            Dim objPaymentRequest As New PaymentOrder

            objPaymentRequest.name = VFirstName.Trim
            objPaymentRequest.email = VEmailID
            objPaymentRequest.phone = VMobileNo

            If objPaymentRequest.emailInvalid = True Then
                lblError.Text = "Invalid EmailID, Please update EmailID"
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If

            If objPaymentRequest.phoneInvalid = True Then
                lblError.Text = "Invalid Mobile No, Please update Mobile No."
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If

            If objPaymentRequest.nameInvalid = True Then
                lblError.Text = "Invalid Name, Please update Name."
                lblError.CssClass = "errorlabels"
                btnSave.Focus()
                Exit Sub
            End If

            'If Not GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Admin" Then
            '    If CDec(lblServiceCharge.Text.Trim) > 0 Then

            '        Dim holdAmt As String = "0"
            '        If GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Retailer" Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response) = "Customer" Then
            '            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            '            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & txtDBName.Text.Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            '            If holdAmt.Trim = "" Then
            '                holdAmt = "0"
            '            End If
            '        End If

            '        If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(lblServiceCharge.Text.Trim) Then
            '        Else
            '            lblError.Text = "You Have Insufficient Wallet Balance to Create Customer."
            '            lblError.CssClass = "errorlabels"
            '            Exit Sub
            '        End If
            '    End If
            'End If


            If btnSave.Text = "Submit & Proceed" Then
                Div_deInfo.Visible = False
                'btnPopupYes.Text = "Yes"
                'btnPopupYes.Visible = True
                'btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Submit??"
                lblDialogMsg.CssClass = ""

                ModalPopupExtender1.Hide()
                ModalPopupExtender3.Hide()
                ModalPopupExtender4.Hide()

                ModalPopupExtender1.Show()
            Else

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Dim VRefrenceID, VRefrenceType, VActiveStatus, VRef_Code As String
    Private Sub btnPopupYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopupYes.Click
        Try
            If btnPopupYes.Text.Trim.ToUpper = "Ok".Trim.ToUpper Then
                Clear()
                Exit Sub
            End If
            lblError.Text = ""
            lblError.CssClass = ""
            VAgentType = GV.parseString(ddlAgentType.SelectedValue.Trim)

            If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                If Not ddl_Ref_Code.SelectedIndex = 0 Then
                    VRef_Code = GV.parseString(ddl_Ref_Code.SelectedValue.Trim)
                Else
                    VRef_Code = ""
                End If
                If Not txtCompanyName.Text.Trim = "" Then
                    VAgencyName = GV.parseString(txtCompanyName.Text.Trim)
                Else
                    VAgencyName = ""
                End If
            Else
                If Not txtAgencyName.Text.Trim = "" Then
                    VAgencyName = GV.parseString(txtAgencyName.Text.Trim)
                Else
                    VAgencyName = ""
                End If
            End If

            If Not txtRefrenceID.Text.Trim = "" Then
                VRefrenceID = GV.parseString(txtRefrenceID.Text.Trim)
            Else
                VRefrenceID = ""
            End If
            If Not txtRefrenceType.Text.Trim = "" Then
                VRefrenceType = GV.parseString(txtRefrenceType.Text.Trim)
            Else
                VRefrenceType = ""
            End If

            If Not txtFirstName.Text.Trim = "" Then
                VFirstName = GV.parseString(txtFirstName.Text.Trim)
            Else
                VFirstName = ""
            End If

            If Not txtEmailID.Text.Trim = "" Then
                VEmailID = GV.parseString(txtEmailID.Text.Trim)
            Else
                VEmailID = ""
            End If

            VDOB = ""
            VAlternateMobileNo = ""
            VPermanentAddress = ""
            VState = ""
            VAddharCardNo = ""
            VWebSite = ""
            VPanCardNumber = ""
            If Not txtRegistrationId.Text.Trim = "" Then
                VRegistrationId = GV.parseString(txtRegistrationId.Text.Trim)
            Else
                VRegistrationId = ""
            End If

            VOfficeAddress = ""
            If Not txtMobileNo.Text.Trim = "" Then
                VMobileNo = GV.parseString(txtMobileNo.Text.Trim)
            Else
                VMobileNo = ""
            End If
            VCity = ""
            VPincode = ""

            VBusinessType = ""

            If Not txtLastName.Text.Trim = "" Then
                VLastName = GV.parseString(txtLastName.Text.Trim)
            Else
                VLastName = ""
            End If

            VGSTNO = ""

            'VActiveStatus = GV.parseString(ddlStatus.SelectedValue.Trim)
            VActiveStatus = "Inactive"
            VCreditBalLimit = "0"
            VRegisterDate = Now.Date



            Dim VUpdatedBy, VUpdatedOn, VRecord_DateTime, Vpassword As String

            VUpdatedBy = "Admin"

            VUpdatedOn = "getdate()"

            VRecord_DateTime = "getDate()"
            Vpassword = GV.RandomPaswrd()
            Dim trnsactionpinNo As String = GV.RandomTransactionPin
            Dim EMPCode As String = ""

            'If GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Sub Distributor".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Retailer".Trim.ToUpper Or GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper = "Customer".Trim.ToUpper Then
            'Else
            '    VRefrenceID = "ADMIN"
            '    VRefrenceType = "ADMIN"
            '    EMPCode = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            'End If

            Dim VDistrict As String
            VDistrict = ""
            Dim Encrypted_Pass As String = GV.EncryptString(GV.key, Vpassword.Trim)


            If Session("EditFlag") = 0 Then



                If GV.FL.RecCount("" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where RegistrationId='" & VRegistrationId & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If

                Dim APIStatus As String = ""
                APIStatus = "Inactive"
                Dim QryStr As String = "insert into " & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration (AEPS_Onboard_Status,Encrypted_Pass,HoldAmt,Ref_Code,District,AEPS_API_Status,RechargeAPI_Status,MoneyTransferAPI_Status,PANCardAPI_Status,EmpCode,ActiveStatus,TransactionPin,CreditBalnceLimit,RefrenceID,RefrenceType,AgentPassword,UpdatedBy,UpdatedOn,RecordDateTime,RegistrationDate,AgentType,AgencyName,FirstName,EmailID,DOB,AlternateMobileNo,PermanentAddress,State,AddharCardNo,WebSite,RegistrationId,PanCardNumber,MobileNo,OfficeAddress,City,LastName,Pincode,BusinessType,GSTNO) values('No','" & Encrypted_Pass & "' ,0,'" & VRef_Code & "','" & VDistrict & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & APIStatus & "','" & EMPCode & "','" & VActiveStatus & "','" & trnsactionpinNo & "','" & VCreditBalLimit & "','" & VRefrenceID & "','" & VRefrenceType & "','" & Encrypted_Pass & "','" & VUpdatedBy & "'," & VUpdatedOn & "," & VRecord_DateTime & ",'" & VRegisterDate & "', '" & VAgentType & "','" & VAgencyName & "','" & VFirstName & "','" & VEmailID & "','" & VDOB & "','" & VAlternateMobileNo & "','" & VPermanentAddress & "','" & VState & "','" & VAddharCardNo & "','" & VWebSite & "','" & VRegistrationId & "','" & VPanCardNumber & "','" & VMobileNo & "','" & VOfficeAddress & "','" & VCity & "','" & VLastName & "','" & VPincode & "','" & VBusinessType & "','" & VGSTNO & "' ) ;"

                If VAgentType.Trim.ToUpper = "Customer".Trim.ToUpper And txtDBName.Text.Trim = "CMP1174" Then
                    QryStr = QryStr & " insert into BOS_ECom.dbo.E_Com_Cust_Reg_Details (RecordStatus,CustomerID,Cust_Firstname,Cust_LastName,Cust_EmailID,Cust_PhoneNo,Cust_Gender,Cust_Password,Is_PhoneNo_Verified,Is_EmailID_Verified,Cust_ActiveStatus,RecordDatetime,UpdatedOn,UpdatedBy) values ('Active','" & VRegistrationId & "','" & VFirstName & "','" & VLastName & "','" & VEmailID & "','" & VMobileNo & "','" & "Male" & "','" & Encrypted_Pass & "','Verified','Verified','Active'," & VUpdatedOn & "," & VUpdatedOn & ",'" & VUpdatedBy & "');"
                End If



                If Not txtRefrenceType.Text.Trim.ToUpper = "Admin".Trim.ToUpper Then
                    Dim vTransID As String = GV.FL_AdminLogin.getAutoNumber("TransId")

                    Dim VCommissionType_ColName, VCommission_ColName As String
                    Dim CashBackComm As Decimal = 0
                    If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        If txtRefrenceType.Text.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                            VCommissionType_ColName = "Dis_CommissionType"
                            VCommission_ColName = "Dis_Commission"
                        ElseIf txtRefrenceType.Text.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                            VCommissionType_ColName = "Sub_Dis_CommissionType"
                            VCommission_ColName = "Sub_Dis_Commission"
                        ElseIf txtRefrenceType.Text.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                            VCommissionType_ColName = "Retailer_CommissionType"
                            VCommission_ColName = "Retailer_Commission"
                        ElseIf txtRefrenceType.Text.Trim.ToUpper = "Customer".Trim.ToUpper Then
                            VCommissionType_ColName = "Customer_CommissionType"
                            VCommission_ColName = "Customer_Commission"
                        Else
                            VCommissionType_ColName = "Customer_CommissionType"
                            VCommission_ColName = "Customer_Commission"
                        End If
                        DS = New DataSet
                        DS = GV.FL.OpenDsWithSelectQuery("select * from " & txtDBName.Text.Trim & "BOS_Ref_Code_Master where Ref_Code='" & GV.parseString(ddl_Ref_Code.SelectedValue) & "'")
                        If Not DS Is Nothing Then
                            If DS.Tables.Count > 0 Then
                                If DS.Tables(0).Rows.Count > 0 Then
                                    Dim VCommissionType, VCommission As String
                                    VCommissionType = ""
                                    VCommission = "0"

                                    If Not IsDBNull(DS.Tables(0).Rows(0).Item(VCommissionType_ColName)) Then
                                        If Not DS.Tables(0).Rows(0).Item(VCommissionType_ColName).ToString() = "" Then
                                            VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item(VCommissionType_ColName).ToString())
                                        End If
                                    End If

                                    If Not IsDBNull(DS.Tables(0).Rows(0).Item(VCommission_ColName)) Then
                                        If Not DS.Tables(0).Rows(0).Item(VCommission_ColName).ToString() = "" Then
                                            VCommission = GV.parseString(DS.Tables(0).Rows(0).Item(VCommission_ColName).ToString())
                                        End If
                                    End If

                                    If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                        CashBackComm = Math.Round(((lblServiceCharge.Text * VCommission) / 100), 2)
                                    ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                        CashBackComm = (VCommission)
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If

                If GV.FL.RecCount("" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where RegistrationId='" & VRegistrationId & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                If GV.FL.DMLQueriesBulk(QryStr) = True Then

                    Dim message As String = ""       '"Dear " & VAgentType & "- Your registration with BOS has been done successfully."


                    'If GV.FL.AddInVar("AllowSms", GV.DefaultDatabase.Trim & ".dbo.Autonumber").ToString.Trim.ToUpper = "Yes".ToUpper Then
                    '    If txtDBName.Text.Trim = "CMP1165" Then
                    '        Dim vMessage As String = "Dear " & VFirstName.Trim & " " & VLastName.Trim & " Your Login Id :- " & VMobileNo & " Your Password :- " & Vpassword & " Your Tpin :- " & trnsactionpinNo & " Please Do Not Share With Anyone . Thanks For Using Kuber Money www.kvishmoney.com"
                    '        'Dim vMessage As String = "Dear " & VFirstName.Trim & " " & VLastName.Trim & " Your Login Id :- " & VRegistrationId & " Your Password :- " & Vpassword & " Your Tpin :- " & trnsactionpinNo & " Please Do Not Share With Anyone . Thanks For Using Kuber Money www.kvishmoney.com"
                    '        GV.send_Template_Based_SMS_API(VMobileNo, vMessage, "Agent Registration", "CMP1165")

                    '    ElseIf txtDBName.Text.Trim = "CMP1174" Then
                    '        Dim vMessage As String = "Dear " & VFirstName.Trim & " " & VLastName.Trim & " Your Login id: " & VMobileNo & " Password: " & Vpassword & " Tpin: " & trnsactionpinNo & " Please Do Not Share With Anyone.Thanks for using Easy Talk Web: https://bit.ly/3p1OkPj App: https://bit.ly/3Szblqb"
                    '        GV.send_Template_Based_SMS_API(VMobileNo, vMessage, "Agent Registration", "CMP1174")

                    '    Else

                    '        Dim vMessage As String = "Dear " & VFirstName.Trim & " " & VLastName.Trim & " Your Login Id :- " & VMobileNo & " Your Password :- " & Vpassword & " Your Tpin :- " & trnsactionpinNo & " Please Do Not Share With Anyone. Thanks For Using BOS. https://bos.center"
                    '        GV.send_Template_Based_SMS_API(VMobileNo, vMessage, "Agent Registration", "")
                    '    End If
                    'End If

                    'message = " Dear " & VAgentType & "," & "<br /><br /><br />"
                    'message = message & " Your registration with BOS has been done successfully." & "<br /><br />"
                    'message = message & "<b> Your" & " Id: " & VRegistrationId & "</b>" & "<br />"
                    'message = message & "<b> Password: " & Vpassword & "</b>" & "<br />"
                    'message = message & "<b> PinNo: " & trnsactionpinNo & "</b>" & "<br /><br /><br />"
                    ''If Not GV.parseString(ddlAgentType.SelectedValue).Trim.ToUpper = "Retailer".Trim.ToUpper Then
                    ''End If


                    'message = message & " For further assistance feel free to contact us at 8181898901." & "<br /><br />"
                    'message = message & " Regards, " & "<br />"
                    'message = message & " TEAM BOS "


                    'GV.sendNewMail(message, "New Account With BOS", VEmailID)



                    TD_Pin.Visible = True
                    Div_deInfo.Visible = True
                    lblClientID.Text = VRegistrationId
                    lblPassword.Text = Vpassword
                    lblTransactionPin.Text = trnsactionpinNo
                    Request_CompanyCode = txtCompanyCode.Text.Trim
                    Request_AgentID = VRegistrationId
                    If ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        Try

                            Request_TransID = GV.FL_AdminLogin.getAutoNumber("TransId")
                            Dim New_transId As String = txtCompanyCode.Text & "_" & "CC" & "_" & VRegistrationId & "_" & Request_TransID
                            ' CC Means = Create Customer
                            ' WA Means = Wallet Add
                            Request_Purpose = "CC"
                            Request_name = VFirstName.Trim & " " & VLastName.Trim
                            Request_email = VEmailID
                            Request_phone = VMobileNo
                            Request_amount = lblServiceCharge.Text
                            Request_Transaction_Id = New_transId

                            'GV.set_OrderID_SessionVariables(Request_Transaction_Id, Request, Response)

                            Try
                                Dim str As String = " insert into " & txtDBName.Text.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details (TransIpAddress,Reference_Id,Reference_Type,Ref_Plan_Code,Request_DateTime,Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,Request_redirect_url,Request_CompanyCode,Request_Purpose,Request_AgentID,Request_TransID,UpdatedOn,UpdatedBy) values ('" & GV.parseString(GV.GetIPAddress) & "','" & VRefrenceID & "','" & VRefrenceType & "','" & VRef_Code & "',getdate(),'" & Request_Transaction_Id & "','" & Request_name & "','" & Request_email & "','" & Request_phone & "','" & Request_amount & "','" & Request_redirect_url & "','" & Request_CompanyCode & "','" & Request_Purpose & "','" & Request_AgentID & "','" & Request_TransID & "',getdate(),'Admin')"
                                GV.FL.DMLQueriesBulk(str)


                                Dim Gateway As String = GV.FL.AddInVar("Gateway", GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD  where Title='Payment Gateway' and AdminID='" & txtCompanyCode.Text.Trim & "'")
                                Dim FnlGateway As String = ""
                                If Not GV.parseString(Gateway.Trim) = "" Then
                                    If GV.parseString(Gateway.Trim).ToUpper = "PayuMoney".ToUpper Then
                                        FnlGateway = "PayuMoney"
                                    ElseIf GV.parseString(Gateway.Trim).ToUpper = "Easebuzz".ToUpper Then
                                        FnlGateway = "Easebuzz"
                                    Else
                                        FnlGateway = "PayuMoney"
                                    End If
                                Else
                                    FnlGateway = "PayuMoney"
                                End If


                                If FnlGateway.ToUpper = "PayuMoney".ToUpper Then
                                    goPayuGateway(Request_Transaction_Id, Request_email, VRegistrationId, Request_amount, Request_name, Request_phone, VRef_Code, Me)
                                Else
                                    'Easebuzz
                                    goEasebuzzGateway(Request_Transaction_Id, Request_email, VRegistrationId, Request_amount, Request_name, Request_phone, VRef_Code, Me)
                                End If

                            Catch ex As Exception
                                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

                            End Try

                        Catch ex As Exception
                            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

                        End Try

                        Clear()
                        ddl_Ref_Code.SelectedIndex = 0
                        ddl_Ref_Code_SelectedIndexChanged(sender, e)
                        ddlAgentType_SelectedIndexChanged(sender, e)
                        ModalPopupExtender1.Hide()
                        ModalPopupExtender3.Hide()
                        ModalPopupExtender4.Hide()

                    Else
                        Dim serviceCharge As String = GV.FL.AddInVar("ServiceCharge", "" & txtDBName.Text.Trim & ".dbo.AutoNumber")
                        If Not serviceCharge.Trim = "" Then
                            If CInt(serviceCharge.Trim) > 0 Then

                                Try
                                    lblServiceCharge.Text = serviceCharge

                                    Request_TransID = GV.FL_AdminLogin.getAutoNumber("TransId")
                                    Dim New_transId As String = txtCompanyCode.Text & "_" & "CR" & "_" & VRegistrationId & "_" & Request_TransID
                                    ' CC Means = Create Customer
                                    ' CR Means = Create Retailer
                                    ' WA Means = Wallet Add
                                    Request_Purpose = "CR"
                                    Request_name = VFirstName.Trim & " " & VLastName.Trim
                                    Request_email = VEmailID
                                    Request_phone = VMobileNo
                                    Request_amount = lblServiceCharge.Text
                                    Request_Transaction_Id = New_transId

                                    'GV.set_OrderID_SessionVariables(Request_Transaction_Id, Request, Response)

                                    Try
                                        Dim str As String = " insert into " & txtDBName.Text.Trim & ".dbo.BOS_InstaMojo_Gateway_Request_Details (TransIpAddress,Reference_Id,Reference_Type,Ref_Plan_Code,Request_DateTime,Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,Request_redirect_url,Request_CompanyCode,Request_Purpose,Request_AgentID,Request_TransID,UpdatedOn,UpdatedBy) values ('" & GV.parseString(GV.GetIPAddress) & "','" & VRefrenceID & "','" & VRefrenceType & "','" & VRef_Code & "',getdate(),'" & Request_Transaction_Id & "','" & Request_name & "','" & Request_email & "','" & Request_phone & "','" & Request_amount & "','" & Request_redirect_url & "','" & Request_CompanyCode & "','" & Request_Purpose & "','" & Request_AgentID & "','" & Request_TransID & "',getdate(),'Admin')"
                                        GV.FL.DMLQueriesBulk(str)


                                        Dim Gateway As String = GV.FL.AddInVar("Gateway", GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD  where Title='Payment Gateway' and AdminID='" & txtCompanyCode.Text.Trim & "'")
                                        Dim FnlGateway As String = ""
                                        If Not GV.parseString(Gateway.Trim) = "" Then
                                            If GV.parseString(Gateway.Trim).ToUpper = "PayuMoney".ToUpper Then
                                                FnlGateway = "PayuMoney"
                                            ElseIf GV.parseString(Gateway.Trim).ToUpper = "Easebuzz".ToUpper Then
                                                FnlGateway = "Easebuzz"
                                            Else
                                                FnlGateway = "PayuMoney"
                                            End If
                                        Else
                                            FnlGateway = "PayuMoney"
                                        End If


                                        If FnlGateway.ToUpper = "PayuMoney".ToUpper Then
                                            goPayuGateway(Request_Transaction_Id, Request_email, VRegistrationId, Request_amount, Request_name, Request_phone, VRef_Code, Me)
                                        Else
                                            'Easebuzz
                                            goEasebuzzGateway(Request_Transaction_Id, Request_email, VRegistrationId, Request_amount, Request_name, Request_phone, VRef_Code, Me)
                                        End If


                                    Catch ex As Exception
                                        GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

                                    End Try

                                Catch ex As Exception
                                    GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

                                End Try

                                Clear()
                                ddl_Ref_Code.SelectedIndex = 0
                                ddl_Ref_Code_SelectedIndexChanged(sender, e)
                                ddlAgentType_SelectedIndexChanged(sender, e)
                                ModalPopupExtender1.Hide()
                                ModalPopupExtender3.Hide()
                                ModalPopupExtender4.Hide()


                            Else
                                Clear()
                                ddlAgentType_SelectedIndexChanged(sender, e)
                                lblDialogMsg.Text = "Record Saved Successfully."
                                lblDialogMsg.CssClass = "Successlabels"
                                btnCancel.Text = "Ok"
                                btnPopupYes.Visible = False
                                ModalPopupExtender1.Show()
                            End If
                        Else
                            Clear()
                            ddlAgentType_SelectedIndexChanged(sender, e)
                            lblDialogMsg.Text = "Record Saved Successfully."
                            lblDialogMsg.CssClass = "Successlabels"
                            btnCancel.Text = "Ok"
                            btnPopupYes.Visible = False
                            ModalPopupExtender1.Show()
                        End If

                    End If



                Else
                    lblDialogMsg.Text = "Record Insertion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Visible = False
                    ModalPopupExtender1.Show()
                End If


            End If



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Public Sub goEasebuzzGateway(ByVal TransID As String, ByVal VCustomerEmailID As String, ByVal VPayCustomerID As String, ByVal VPayCartAmount As String, ByVal VPayCustomerName As String, ByVal VPayCustomerPhoneNo As String, ByVal VDeliveryRID As String, pg As Page)
        Try
            Dim txnID As String = TransID
            '/// create cookie start
            GV.set_OrderID_SessionVariables(TransID, Request, Response)
            '/// create cookie end
            'Public EaseBuzz_MERCHANT_KEY As String = "MEBKTJC7XL"
            'Public EaseBuzz_SALT_KEY As String = "48Q5G1L07G"
            'Public EaseBuzz_BASE_URL As String = "https://pay.easebuzz.in" '/pay/secure
            'https://pay.easebuzz.in/pay/secure

            Dim Text As String = EaseBuzz_MERCHANT_KEY & "|" & txnID & "|" & VPayCartAmount & "|" & "Shopping" & "|" & VPayCustomerName & "|" & VCustomerEmailID & "|||||||||||" & EaseBuzz_SALT_KEY
            'key|txnid|amount|productinfo|firstname|email|||||||||||salt
            Dim message As Byte() = Encoding.UTF8.GetBytes(Text)
            Dim UE As UnicodeEncoding = New UnicodeEncoding()
            Dim hashValue As Byte()
            Dim hash As String = ""
            Dim hashString As SHA512Managed = New SHA512Managed()
            Dim hex As String = ""
            hashValue = hashString.ComputeHash(message)
            For Each x As Byte In hashValue
                hex += String.Format("{0:x2}", x)
            Next
            hash = hex

            Dim data As System.Collections.Hashtable = New System.Collections.Hashtable()

            'hashVarsSeq = "udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10".Split('|'); // spliting hash sequence from config

            data.Add("hash", hex.ToString)
            data.Add("key", EaseBuzz_MERCHANT_KEY.ToString)
            data.Add("txnid", txnID.ToString)
            data.Add("amount", VPayCartAmount)
            data.Add("productinfo", "Shopping")
            data.Add("firstname", VPayCustomerName)
            data.Add("email", VCustomerEmailID)
            data.Add("phone", VPayCustomerPhoneNo)
            data.Add("salt", EaseBuzz_SALT_KEY)

            Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
            Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")

            Dim sul As String = strUrl & "Easebuzz_Handler_Success.aspx"
            Dim ful As String = strUrl & "Payu_Handler_Fail.aspx"
            Dim cul As String = strUrl & "Payu_Handler_Cancel.aspx"

            data.Add("surl", sul)
            data.Add("furl", ful)
            data.Add("curl", cul)
            data.Add("show_payment_mode", "NB,DC,CC,DAP,MW,UPI,OM,EMI") 'NB,DC,CC,DAP,MW,UPI,OM,EMI


            'show_payment_mode
            'NB,DC,CC,DAP,MW,UPI,OM,EMI     BNPL

            pg.Controls.Add(New LiteralControl(GV.PreparePOSTForm("https://pay.easebuzz.in/pay/secure", data)))
            Response.Write(GV.PreparePOSTForm("https://pay.easebuzz.in/pay/secure", data))

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub goPayuGateway(ByVal TransID As String, ByVal VCustomerEmailID As String, ByVal VPayCustomerID As String, ByVal VPayCartAmount As String, ByVal VPayCustomerName As String, ByVal VPayCustomerPhoneNo As String, ByVal VDeliveryRID As String, pg As Page)
        Try
            Dim txnID As String = TransID
            '/// create cookie start
            GV.set_OrderID_SessionVariables(TransID, Request, Response)
            '/// create cookie end


            Dim Text As String = MERCHANT_KEY & "|" & txnID & "|" & VPayCartAmount & "|" & "Shopping" & "|" & VPayCustomerName & "|" & VCustomerEmailID & "|||||||||||" & SALT_KEY

            Dim message As Byte() = Encoding.UTF8.GetBytes(Text)
            Dim UE As UnicodeEncoding = New UnicodeEncoding()
            Dim hashValue As Byte()
            Dim hash As String = ""
            Dim hashString As SHA512Managed = New SHA512Managed()
            Dim hex As String = ""
            hashValue = hashString.ComputeHash(message)
            For Each x As Byte In hashValue
                hex += String.Format("{0:x2}", x)
            Next
            hash = hex

            Dim data As System.Collections.Hashtable = New System.Collections.Hashtable()

            data.Add("hash", hex.ToString)
            data.Add("key", MERCHANT_KEY.ToString)
            data.Add("txnid", txnID.ToString)
            data.Add("amount", VPayCartAmount)
            data.Add("productinfo", "Shopping")
            data.Add("firstname", VPayCustomerName)
            data.Add("email", VCustomerEmailID)
            data.Add("phone", VPayCustomerPhoneNo)
            data.Add("salt", SALT_KEY)

            Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
            Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")

            Dim sul As String = strUrl & "Payu_Handler_Success.aspx"
            Dim ful As String = strUrl & "Payu_Handler_Fail.aspx"
            Dim cul As String = strUrl & "Payu_Handler_Cancel.aspx"

            data.Add("surl", sul)
            data.Add("furl", ful)
            data.Add("curl", cul)
            data.Add("service_provider", "payu_paisa")
            'Dim strForm As StringBuilder = GV.PreparePOSTForm("https://secure.payu.in/_payment", data)

            pg.Controls.Add(New LiteralControl(GV.PreparePOSTForm("https://secure.payu.in/_payment", data)))

            Response.Write(GV.PreparePOSTForm("https://secure.payu.in/_payment", data))


            'Response.Write(strForm)



            'result = strForm.ToString commented now





            'Response.Write(PreparePOSTForm("https://test.payu.in/_payment", data))
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Private Sub Clear()
        Try
            VAgentType = ""
            VAgencyName = ""
            VFirstName = ""
            VEmailID = ""
            VDOB = ""
            VAlternateMobileNo = ""
            VPermanentAddress = ""
            VState = ""
            VAddharCardNo = ""
            VWebSite = ""
            VRegistrationId = ""
            VPanCardNumber = ""
            VMobileNo = ""
            VOfficeAddress = ""
            VCity = ""
            VLastName = ""
            VPincode = ""
            VBusinessType = ""
            VGSTNO = ""
            txtAgencyName.Text = ""
            txtCompanyName.Text = ""

            txtFirstName.Text = ""

            txtEmailID.Text = ""



            txtMobileNo.Text = ""



            txtLastName.Text = ""



            If txtDBName.Text.Trim.ToUpper = "BosCenter_DB".Trim.ToUpper Then
                txtCompanyName.Text = "BOS Money"
            Else
                Dim CompanyName As String = GV.FL_AdminLogin.AddInVar("CompanyName", " " & txtDBName.Text.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & txtCompanyCode.Text.Trim & "'  ")
                txtCompanyName.Text = GV.parseString(CompanyName.Trim)
            End If

            txtRefrenceID.Text = ""
            lblRefId_Error.Text = ""
            txtRefrenceType.Text = ""




            txtCompanyName.CssClass = "form-control"
            txtFirstName.CssClass = "form-control"
            txtEmailID.CssClass = "form-control"

            txtMobileNo.CssClass = "form-control"

            txtLastName.CssClass = "form-control"


            Session("EditFlag") = 0
            btnSave.Text = "Submit & Proceed"
            btnClear.Text = "Close"

            lblError.Text = ""
            btnSave.Enabled = True
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Protected Sub ddlAgentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgentType.SelectedIndexChanged
        Try

            If ddlAgentType.SelectedValue.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("Distributor_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("DistributorID", txtDBName.Text.Trim)
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("SubDistributor_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("SubDistributorID", txtDBName.Text.Trim)
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                'Response.Redirect("CreateAgent.aspx?admin=" & txtCompanyCode.Text.Trim)
                txtRegistrationId.Text = GV.FL.AddInVar("Retailer_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("RetailerId", txtDBName.Text.Trim)
                lblRef_AgencyName.Text = "Agency Name"
                txtAgencyName.Visible = True
                ddl_Ref_Code.Visible = False
                txtAgencyName.CssClass = "form-control"
                txtRefrenceID.Text = GV.FL.AddInVar("SubDistributorID", "" & txtDBName.Text.Trim & ".dbo.AutoNumber")
                txtRefrenceType.Text = "Distributor"
                txtRefrenceID.Enabled = False
                txtRefrenceID.CssClass = "form-control"
                lblRefId_Error.Text = ""


            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Customer".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("Customer_Prefix", "" & txtDBName.Text.Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("CustomerID", txtDBName.Text.Trim)
                lblRef_AgencyName.Text = "Reference Code"
                txtAgencyName.Visible = False
                ddl_Ref_Code.Visible = True
                ddl_Ref_Code.CssClass = "form-control"

                txtRefrenceType.Text = ""
                txtRefrenceID.Text = ""
                txtRefrenceID.Enabled = True
                txtRefrenceID.CssClass = "form-control"
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub




    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================

            ModalPopupExtender4.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnDelOk_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If Not lblRowIndex.Text.Trim = "" Then


                lblAlertDelPer.Text = ""
                lblDelDialogMsg.Text = "Record deleted Successfully."
                lblDelDialogMsg.CssClass = "Successlabels"
                btnDelCancel.Text = "OK"
                btnDelOk.Visible = False
                DeleteModalPopupExtender.Show()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub





    Protected Sub ddl_Ref_Code_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Ref_Code.SelectedIndexChanged
        Try
            If ddl_Ref_Code.SelectedIndex = 0 Then
                lblServiceCharge.Text = "0"
            Else
                lblServiceCharge.Text = GV.FL.AddInVar("AmtToDebit", "" & txtDBName.Text.Trim & ".dbo.BOS_Ref_Code_Master where Ref_Code='" & GV.parseString(ddl_Ref_Code.SelectedValue) & "'")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub txtRefrenceID_TextChanged(sender As Object, e As EventArgs) Handles txtRefrenceID.TextChanged
        Try

            lblRefId_Error.Text = ""
            lblRefId_Error.CssClass = ""


            DS = GV.FL.OpenDs("" & txtDBName.Text.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where ActiveStatus='Active'  and RegistrationId='" & txtRefrenceID.Text.Trim & "' ")
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("AgentType")) Then
                            If Not DS.Tables(0).Rows(0).Item("AgentType").ToString() = "" Then
                                txtRefrenceType.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AgentType").ToString())
                            Else
                                txtRefrenceType.Text = ""
                            End If
                        Else
                            txtRefrenceType.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("FirstName")) Then
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("LastName")) Then
                                lblRefId_Error.Text = DS.Tables(0).Rows(0).Item("FirstName").ToString.ToUpper & " " & DS.Tables(0).Rows(0).Item("LastName").ToString.ToUpper
                                'lblRefId_Error.CssClass = "successlabels"
                                lblRefId_Error.ForeColor = Color.Blue

                            End If
                        End If




                    Else
                        lblRefId_Error.Text = "Invalid Refer ID".ToString.ToUpper
                        'lblRefId_Error.CssClass = "errorlabels"
                        lblRefId_Error.ForeColor = Color.Red
                        txtRefrenceID.Text = ""
                        txtRefrenceType.Text = ""

                    End If

                Else
                    lblRefId_Error.Text = "Invalid Refer ID".ToString.ToUpper
                    'lblRefId_Error.CssClass = "errorlabels"
                    lblRefId_Error.ForeColor = Color.Red
                    txtRefrenceID.Text = ""
                    txtRefrenceType.Text = ""
                End If
            Else
                lblRefId_Error.Text = "Invalid Refer ID".ToString.ToUpper
                'lblRefId_Error.CssClass = "errorlabels"
                lblRefId_Error.ForeColor = Color.Red

                txtRefrenceID.Text = ""
                txtRefrenceType.Text = ""
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            ModalPopupExtender1.Hide()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub txtEmailID_TextChanged(sender As Object, e As EventArgs) Handles txtEmailID.TextChanged
        Try
            lblEmailError.Text = ""
            If Not txtEmailID.Text = "" Then

                Dim regex As Regex = New Regex("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                Dim IsValid As Boolean = regex.IsMatch(txtEmailID.Text.Trim)
                If Not IsValid Then
                    lblEmailError.Text = "Invalid Email."
                Else
                    lblEmailError.Text = ""
                End If

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class