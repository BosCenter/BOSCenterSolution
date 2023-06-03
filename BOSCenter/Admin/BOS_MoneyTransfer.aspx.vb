Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims



Public Class BOS_MoneyTransfer
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblWalletBal.Text = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)


            If Not IsPostBack Then
                btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"


                div_gateway.Visible = False

                DIV_Clear_2()
                DIV_Clear()
                lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                lblAgentType.Text = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)


                Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                Dim DataBaseName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response)
                Dim Group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)


                Dim Gateway As String = GV.FL.AddInVar("Gateway", GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD  where Title='Money Transfer' and AdminID='" & CompanyCode & "'")

                If Not GV.parseString(Gateway.Trim) = "" Then
                    If GV.parseString(Gateway.Trim).ToUpper = "Money Transfer 1".ToUpper Then
                        ddlGateway.SelectedValue = "MoneyTransferAPI"
                    ElseIf GV.parseString(Gateway.Trim).ToUpper = "Money Transfer 2".ToUpper Then
                        ddlGateway.SelectedValue = "MoneyTransferAPI-2"
                    Else
                        ddlGateway.SelectedValue = "MoneyTransferAPI"
                    End If
                    ddlGateway_SelectedIndexChanged(sender, e)
                Else
                    ddlGateway_SelectedIndexChanged(sender, e)
                End If

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API URL  - Start
    Dim Add_New_Customer_API_URL As String = "https://www.vacsc.com/DMTAPI/AddNewCustomer"
    Dim Verify_Customer_API_URL As String = "https://www.vacsc.com/DMTAPI/VerifyCustomer"
    Dim Bank_Details_API_URL As String = "https://www.vacsc.com/DMTAPI/BankDetails"
    Dim Verify_Bank_Details_API_URL As String = "https://www.vacsc.com/DMTAPI/VerifytBankDetails"
    Dim Add_New_Recipients_API_URL As String = "https://www.vacsc.com/DMTAPI/AddNewRecipients"
    Dim Recipients_Details_API_URL As String = "https://www.vacsc.com/DMTAPI/RecipientsDetails"
    Dim Receipent_List_API_URL As String = "https://www.vacsc.com/DMTAPI/ReceipentList"
    Dim Money_Transfer_API_URL As String = "https://www.vacsc.com/DMTAPI/GetMoneyTransfer"
    Dim VerifyOtp_API_URL As String = "https://www.vacsc.com/DMTAPI/VerifyOtp"
    Dim ResendOtp_API_URL As String = "https://www.vacsc.com/DMTAPI/ResendOtp"
    '///  Money Transfer API URL  - END


    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - Start NEW
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API URL  - Start



    'Dim baseURL As String = "https://paysprint.in/" 
    Dim baseURL As String = "https://api.paysprint.in"

    Dim QueryRemitter_API_URL As String = baseURL & "/api/v1/service/dmt/remitter/queryremitter"
    Dim RegisterRemitter_API_URL As String = baseURL & "/api/v1/service/dmt/remitter/registerremitter"
    Dim RegisterBeneficiary_API_URL As String = baseURL & "/api/v1/service/dmt/beneficiary/registerbeneficiary"
    Dim DeleteBeneficiary_API_URL As String = baseURL & "/api/v1/service/dmt/beneficiary/registerbeneficiary/deletebeneficiary"
    Dim FetchBeneficiary_API_URL As String = baseURL & "/api/v1/service/dmt/beneficiary/registerbeneficiary/fetchbeneficiary"
    Dim PennyDrop_API_URL As String = baseURL & "/api/v1/service/dmt/beneficiary/registerbeneficiary/benenameverify"
    Dim Transaction_API_URL As String = baseURL & "/api/v1/service/dmt/transact/transact"
    Dim TransactionStatus_API_URL As String = baseURL & "/api/v1/service/dmt/transact/transact/querytransact"
    Dim RefundOTP_API_URL As String = baseURL & "/api/v1/service/dmt/refund/refund/resendotp"
    Dim ClaimRefund_API_URL As String = baseURL & "/api/v1/service/dmt/refund/refund/"
    Dim OnboardingWeb_API_URL As String = baseURL & "/api/v1/service/onboard/onboard/getonboardurl"

    '///  Money Transfer API URL  - END NEW

    Public Class QueryRemitter_API_Parameters
        Dim VMobile, Vbank3_flag As String
        Public Property mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property bank3_flag() As String
            Get
                Return Vbank3_flag
            End Get
            Set(ByVal value As String)
                Vbank3_flag = value
            End Set
        End Property

    End Class
    Public Class RegisterRemitter_API_Parameters
        Dim Vmobile, Vfirstname, Vlastname, Vaddress, Votp, Vpincode, Vstateresp, Vbank3_flag, Vdob, Vgst_state As String
        Public Property mobile() As String
            Get
                Return Vmobile
            End Get
            Set(ByVal value As String)
                Vmobile = value
            End Set
        End Property
        Public Property firstname() As String
            Get
                Return Vfirstname
            End Get
            Set(ByVal value As String)
                Vfirstname = value
            End Set
        End Property
        Public Property lastname() As String
            Get
                Return Vlastname
            End Get
            Set(ByVal value As String)
                Vlastname = value
            End Set
        End Property
        Public Property address() As String
            Get
                Return Vaddress
            End Get
            Set(ByVal value As String)
                Vaddress = value
            End Set
        End Property
        Public Property otp() As String
            Get
                Return Votp
            End Get
            Set(ByVal value As String)
                Votp = value
            End Set
        End Property
        Public Property pincode() As String
            Get
                Return Vpincode
            End Get
            Set(ByVal value As String)
                Vpincode = value
            End Set
        End Property
        Public Property stateresp() As String
            Get
                Return Vstateresp
            End Get
            Set(ByVal value As String)
                Vstateresp = value
            End Set
        End Property
        Public Property bank3_flag() As String
            Get
                Return Vbank3_flag
            End Get
            Set(ByVal value As String)
                Vbank3_flag = value
            End Set
        End Property
        Public Property dob() As String
            Get
                Return Vdob
            End Get
            Set(ByVal value As String)
                Vdob = value
            End Set
        End Property
        Public Property gst_state() As String
            Get
                Return Vgst_state
            End Get
            Set(ByVal value As String)
                Vgst_state = value
            End Set
        End Property

    End Class
    Public Class RegisterBeneficiary_API_Parameters
        Dim Vmobile, Vbenename, Vbankid, Vaccno, Vifsccode, Vverified, Vpincode, Vdob, Vgst_state, Vaddress As String
        Public Property mobile() As String
            Get
                Return Vmobile
            End Get
            Set(ByVal value As String)
                Vmobile = value
            End Set
        End Property
        Public Property benename() As String
            Get
                Return Vbenename
            End Get
            Set(ByVal value As String)
                Vbenename = value
            End Set
        End Property
        Public Property bankid() As String
            Get
                Return Vbankid
            End Get
            Set(ByVal value As String)
                Vbankid = value
            End Set
        End Property
        Public Property accno() As String
            Get
                Return Vaccno
            End Get
            Set(ByVal value As String)
                Vaccno = value
            End Set
        End Property
        Public Property ifsccode() As String
            Get
                Return Vifsccode
            End Get
            Set(ByVal value As String)
                Vifsccode = value
            End Set
        End Property
        Public Property verified() As String
            Get
                Return Vverified
            End Get
            Set(ByVal value As String)
                Vverified = value
            End Set
        End Property
        Public Property pincode() As String
            Get
                Return Vpincode
            End Get
            Set(ByVal value As String)
                Vpincode = value
            End Set
        End Property
        Public Property gst_state() As String
            Get
                Return Vgst_state
            End Get
            Set(ByVal value As String)
                Vgst_state = value
            End Set
        End Property
        Public Property dob() As String
            Get
                Return Vdob
            End Get
            Set(ByVal value As String)
                Vdob = value
            End Set
        End Property
        Public Property address() As String
            Get
                Return Vaddress
            End Get
            Set(ByVal value As String)
                Vaddress = value
            End Set
        End Property
    End Class
    Public Class DeleteBeneficiary_API_Parameters
        Dim Vmobile, Vbene_id As String
        Public Property mobile() As String
            Get
                Return Vmobile
            End Get
            Set(ByVal value As String)
                Vmobile = value
            End Set
        End Property
        Public Property bene_id() As String
            Get
                Return Vbene_id
            End Get
            Set(ByVal value As String)
                Vbene_id = value
            End Set
        End Property
    End Class
    Public Class FetchBeneficiary_API_Parameters
        Dim Vmobile As String
        Public Property mobile() As String
            Get
                Return Vmobile
            End Get
            Set(ByVal value As String)
                Vmobile = value
            End Set
        End Property
    End Class
    Public Class PennyDrop_API_Parameters
        Dim Vmobile, Vaccno, Vbankid, Vbenename, Vreferenceid, Vpincode, Vaddress, Vdob, Vgst_state, Vbene_id As String
        Public Property mobile() As String
            Get
                Return Vmobile
            End Get
            Set(ByVal value As String)
                Vmobile = value
            End Set
        End Property
        Public Property accno() As String
            Get
                Return Vaccno
            End Get
            Set(ByVal value As String)
                Vaccno = value
            End Set
        End Property
        Public Property bankid() As String
            Get
                Return Vbankid
            End Get
            Set(ByVal value As String)
                Vbankid = value
            End Set
        End Property
        Public Property benename() As String
            Get
                Return Vbenename
            End Get
            Set(ByVal value As String)
                Vbenename = value
            End Set
        End Property
        Public Property referenceid() As String
            Get
                Return Vreferenceid
            End Get
            Set(ByVal value As String)
                Vreferenceid = value
            End Set
        End Property
        Public Property pincode() As String
            Get
                Return Vpincode
            End Get
            Set(ByVal value As String)
                Vpincode = value
            End Set
        End Property
        Public Property address() As String
            Get
                Return Vaddress
            End Get
            Set(ByVal value As String)
                Vaddress = value
            End Set
        End Property
        Public Property dob() As String
            Get
                Return Vdob
            End Get
            Set(ByVal value As String)
                Vdob = value
            End Set
        End Property
        Public Property gst_state() As String
            Get
                Return Vgst_state
            End Get
            Set(ByVal value As String)
                Vgst_state = value
            End Set
        End Property
        Public Property bene_id() As String
            Get
                Return Vbene_id
            End Get
            Set(ByVal value As String)
                Vbene_id = value
            End Set
        End Property
    End Class
    Public Class Transaction_API_Parameters
        Dim Vmobile, Vreferenceid, Vpipe, Vpincode, Vaddress, Vdob, Vgst_state, Vbene_id, Vtxntype, Vamount As String
        Public Property mobile() As String
            Get
                Return Vmobile
            End Get
            Set(ByVal value As String)
                Vmobile = value
            End Set
        End Property
        Public Property referenceid() As String
            Get
                Return Vreferenceid
            End Get
            Set(ByVal value As String)
                Vreferenceid = value
            End Set
        End Property
        Public Property pipe() As String
            Get
                Return Vpipe
            End Get
            Set(ByVal value As String)
                Vpipe = value
            End Set
        End Property
        Public Property pincode() As String
            Get
                Return Vpincode
            End Get
            Set(ByVal value As String)
                Vpincode = value
            End Set
        End Property
        Public Property address() As String
            Get
                Return Vaddress
            End Get
            Set(ByVal value As String)
                Vaddress = value
            End Set
        End Property
        Public Property dob() As String
            Get
                Return Vdob
            End Get
            Set(ByVal value As String)
                Vdob = value
            End Set
        End Property
        Public Property gst_state() As String
            Get
                Return Vgst_state
            End Get
            Set(ByVal value As String)
                Vgst_state = value
            End Set
        End Property
        Public Property bene_id() As String
            Get
                Return Vbene_id
            End Get
            Set(ByVal value As String)
                Vbene_id = value
            End Set
        End Property
        Public Property txntype() As String
            Get
                Return Vtxntype
            End Get
            Set(ByVal value As String)
                Vtxntype = value
            End Set
        End Property
        Public Property amount() As String
            Get
                Return Vamount
            End Get
            Set(ByVal value As String)
                Vamount = value
            End Set
        End Property

    End Class
    Public Class TransactionStatus_API_Parameters
        Dim Vreferenceid As String
        Public Property referenceid() As String
            Get
                Return Vreferenceid
            End Get
            Set(ByVal value As String)
                Vreferenceid = value
            End Set
        End Property
    End Class
    Public Class RefundOTP_API_Parameters
        Dim Vreferenceid, Vackno As String
        Public Property referenceid() As String
            Get
                Return Vreferenceid
            End Get
            Set(ByVal value As String)
                Vreferenceid = value
            End Set
        End Property
        Public Property ackno() As String
            Get
                Return Vackno
            End Get
            Set(ByVal value As String)
                Vackno = value
            End Set
        End Property
    End Class
    Public Class ClaimRefund_API_Parameters
        Dim Vreferenceid, Vackno, Votp As String
        Public Property referenceid() As String
            Get
                Return Vreferenceid
            End Get
            Set(ByVal value As String)
                Vreferenceid = value
            End Set
        End Property
        Public Property ackno() As String
            Get
                Return Vackno
            End Get
            Set(ByVal value As String)
                Vackno = value
            End Set
        End Property
        Public Property otp() As String
            Get
                Return Votp
            End Get
            Set(ByVal value As String)
                Votp = value
            End Set
        End Property
    End Class
    Public Class OnboardingWeb_API_Parameters
        Dim Vmerchantcode, Vmobile, Vis_new, Vemail, Vfirm, Vcallback '''' As String
        Public Property merchantcode() As String
            Get
                Return Vmerchantcode
            End Get
            Set(ByVal value As String)
                Vmerchantcode = value
            End Set
        End Property
        Public Property mobile() As String
            Get
                Return Vmobile
            End Get
            Set(ByVal value As String)
                Vmobile = value
            End Set
        End Property
        Public Property is_new() As String
            Get
                Return Vis_new
            End Get
            Set(ByVal value As String)
                Vis_new = value
            End Set
        End Property
        Public Property email() As String
            Get
                Return Vemail
            End Get
            Set(ByVal value As String)
                Vemail = value
            End Set
        End Property
        Public Property firm() As String
            Get
                Return Vfirm
            End Get
            Set(ByVal value As String)
                Vfirm = value
            End Set
        End Property
        Public Property callback() As String
            Get
                Return Vcallback
            End Get
            Set(ByVal value As String)
                Vcallback = value
            End Set
        End Property

    End Class


    Public Function GetApiResult_NEW(APIMethod As String) As String
        Dim ApiResult As String = ""
        Dim StrParameters As String = ""
        Dim API_URLS As String = ""
        Try
            '222111
            If APIMethod = "QueryRemitter_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New QueryRemitter_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.bank3_flag = "Yes"
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = QueryRemitter_API_URL
            ElseIf APIMethod = "RegisterRemitter_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New RegisterRemitter_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)

                Dim ss() As String = GV.parseString(txtCustomerName.Text.Trim).Split(" ")
                If ss.Length > 1 Then
                    setParameter_API_Obj.firstname = ss(0).Trim
                    setParameter_API_Obj.lastname = ss(1).Trim
                Else
                    setParameter_API_Obj.firstname = GV.parseString(txtCustomerName.Text.Trim)
                    setParameter_API_Obj.lastname = GV.parseString(txtCustomerName.Text.Trim)
                End If

                setParameter_API_Obj.address = "delhi"
                setParameter_API_Obj.otp = GV.parseString(TextBox1.Text.Trim)
                setParameter_API_Obj.pincode = "110085"
                setParameter_API_Obj.stateresp = GV.parseString(lblstateresp.Text.Trim)
                setParameter_API_Obj.bank3_flag = "Yes"
                setParameter_API_Obj.gst_state = "07"
                setParameter_API_Obj.dob = Now.Date.AddYears(-19)

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = RegisterRemitter_API_URL
            ElseIf APIMethod = "RegisterBeneficiary_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New RegisterBeneficiary_API_Parameters()
                setParameter_API_Obj.mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.benename = GV.parseString(txtRecepientMobileNo.Text.Trim)
                setParameter_API_Obj.bankid = GV.parseString(ddlSelectBank.SelectedValue.Trim)  'GV.FL.AddInVar("BankID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList where Code='" & GV.parseString(ddlSelectBank.SelectedValue.Trim) & "'")
                setParameter_API_Obj.accno = GV.parseString(txtBankAccountNo.Text.Trim)
                setParameter_API_Obj.ifsccode = GV.parseString(txtIFSCCode.Text.Trim)
                setParameter_API_Obj.verified = "1"
                setParameter_API_Obj.gst_state = "07"
                setParameter_API_Obj.dob = "1990-03-02"
                setParameter_API_Obj.address = "New delhi"
                setParameter_API_Obj.pincode = "110343"
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = RegisterBeneficiary_API_URL
            ElseIf APIMethod = "DeleteBeneficiary_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New DeleteBeneficiary_API_Parameters()
                setParameter_API_Obj.mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.bene_id = GV.parseString(lblReceipentId.Text.Trim)
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = DeleteBeneficiary_API_URL
            ElseIf APIMethod = "FetchBeneficiary_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New FetchBeneficiary_API_Parameters()
                setParameter_API_Obj.mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = FetchBeneficiary_API_URL
            ElseIf APIMethod = "PennyDrop_API_Parameters" Then
                Dim setParameter_API_Obj As New PennyDrop_API_Parameters()
                setParameter_API_Obj.mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.accno = GV.parseString(txtBankAccountNo.Text.Trim)
                setParameter_API_Obj.bankid = GV.parseString(ddlSelectBank.SelectedValue.Trim) ' GV.FL.AddInVar("BankID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList where Code='" & GV.parseString(ddlSelectBank.SelectedValue.Trim) & "'")
                setParameter_API_Obj.benename = GV.parseString(txtRecepientMobileNo.Text.Trim)
                setParameter_API_Obj.referenceid = GV.FL.getAutoNumber("TransId")
                setParameter_API_Obj.pincode = "110027"
                setParameter_API_Obj.address = "New Delhi"
                setParameter_API_Obj.dob = "13-09-1990"
                setParameter_API_Obj.gst_state = "07"
                setParameter_API_Obj.bene_id = GV.parseString(lbl_Beneficiary_temp_id.Text.Trim)
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = PennyDrop_API_URL
            ElseIf APIMethod = "Transaction_API_Parameters" Then 'Done

                Dim setParameter_API_Obj As New Transaction_API_Parameters
                setParameter_API_Obj.mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.referenceid = GV.FL_AdminLogin.getAutoNumber("TransId")
                setParameter_API_Obj.pipe = lblBankID.Text
                setParameter_API_Obj.pincode = "110027"
                setParameter_API_Obj.address = "New Delhi"
                setParameter_API_Obj.dob = "13-09-1990"
                setParameter_API_Obj.gst_state = "07"
                setParameter_API_Obj.bene_id = GV.parseString(lblReceipentId.Text.Trim)
                setParameter_API_Obj.txntype = ddlTransferMode.SelectedValue.Trim
                setParameter_API_Obj.amount = lblCaculatedAmt.Text.Trim

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Transaction_API_URL



            ElseIf APIMethod = "TransactionStatus_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New TransactionStatus_API_Parameters

                setParameter_API_Obj.referenceid = "TransId"
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = TransactionStatus_API_URL

            ElseIf APIMethod = "RefundOTP_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New RefundOTP_API_Parameters

                setParameter_API_Obj.referenceid = "TransId"
                setParameter_API_Obj.ackno = "127"

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = RefundOTP_API_URL
            ElseIf APIMethod = "ClaimRefund_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New ClaimRefund_API_Parameters
                setParameter_API_Obj.referenceid = "TransId"
                setParameter_API_Obj.ackno = "127"
                setParameter_API_Obj.otp = txtEnterOTP.Text.Trim
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = ClaimRefund_API_URL
            ElseIf APIMethod = "OnboardingWeb_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New OnboardingWeb_API_Parameters
                setParameter_API_Obj.merchantcode = "1"
                setParameter_API_Obj.mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.is_new = "0"
                setParameter_API_Obj.email = "v@gmail.com"
                setParameter_API_Obj.firm = "PAYMONEY"
                setParameter_API_Obj.callback = "https:partner.com/callbackurl\"
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = OnboardingWeb_API_URL
            End If


            ApiResult = ReadbyRestClient_NEW(API_URLS, StrParameters)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return ApiResult
        End Try
        Return ApiResult
    End Function
    Public Function ReadbyRestClient_NEW(Urls As String, Parameter As String) As String

        Dim API_Name, Trans_ID, Trans_DateTime, Request_String, Response_String, AgentID, AgentType As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        AgentID = ""
        AgentType = ""

        Dim strQry As String = ""

        Dim str As String = ""
        Dim LogString As String = ""
        Try
            lblTransId.Text = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(lblTransId.Text) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine

            API_Name = "Money Transfer API"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text



            Dim tokenHandler As New JwtSecurityTokenHandler()

            Dim SecurityKey As New Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("UFMwMDgzM2U4Mzc3NmQ5MTlmNGI4ZDRmNjI3NjJiNGUwMDU0MzJi")) 'UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh
            Dim Credentials As New Microsoft.IdentityModel.Tokens.SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)

            Dim header As New JwtHeader(Credentials)

            Dim unixEpoch = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            Dim currTimeStamp = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds)

            Dim payload As New JwtPayload()
            payload.AddClaim(New Claim("timestamp", currTimeStamp))
            payload.AddClaim(New Claim("partnerId", "PS00833"))   'PS00343
            payload.AddClaim(New Claim("reqid", currTimeStamp))


            Dim secToken As New JwtSecurityToken(header, payload)

            Dim handler As New JwtSecurityTokenHandler()
            Dim tokenString = handler.WriteToken(secToken)

            Dim token = handler.ReadJwtToken(tokenString)

            Dim dd As String = token.RawData

            ServicePointManager.Expect100Continue = True
            ServicePointManager.DefaultConnectionLimit = 9999
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12



            Dim client As New RestClient(Urls)
            Dim request As New RestSharp.RestRequest(RestSharp.Method.POST)
            request.AddHeader("Accept", "application/json")
            request.AddHeader("Token", dd)
            request.AddHeader("Content-Type", "application/json")
            'request.AddHeader("Authorisedkey", "ZTE3MTYyZDI3YzNjY2Y2ZjE5N2M0NGRkNjg4YzAzYmE=")

            request.AddParameter("application/json", Parameter, RestSharp.ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim

            Response_String = GV.parseString(str)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine

            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function


    Public Class Add_New_Customer_API_Parameters
        Dim VMobile, VName, VDOB, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return VName
            End Get
            Set(ByVal value As String)
                VName = value
            End Set
        End Property
        Public Property DOB() As String
            Get
                Return VDOB
            End Get
            Set(ByVal value As String)
                VDOB = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property
    End Class
    Public Class Verify_Customer_API_Parameters
        Dim VMobile, VKey As String

        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property

    End Class
    Public Class Bank_Details_API_Parameters
        Dim VBankCode, Vifsc, VKey As String

        Public Property BankCode() As String
            Get
                Return VBankCode
            End Get
            Set(ByVal value As String)
                VBankCode = value
            End Set
        End Property

        Public Property ifsc() As String
            Get
                Return Vifsc
            End Get
            Set(ByVal value As String)
                Vifsc = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property

    End Class
    Public Class Verify_Bank_Details_API_Parameters
        Dim VBankAccountCode, VKey, VAccountNo, VMobileNo As String

        Public Property BankAccountCode() As String
            Get
                Return VBankAccountCode
            End Get
            Set(ByVal value As String)
                VBankAccountCode = value
            End Set
        End Property

        Public Property AccountNo() As String
            Get
                Return VAccountNo
            End Get
            Set(ByVal value As String)
                VAccountNo = value
            End Set
        End Property

        Public Property MobileNo() As String
            Get
                Return VMobileNo
            End Get
            Set(ByVal value As String)
                VMobileNo = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property

    End Class
    Public Class Add_New_Recipients_API_Parameters
        Dim VMobile, VBankCode, Vrecipient_name, VIfscCode, VAccountNo, Vcustomermobile, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property
        Public Property BankCode() As String
            Get
                Return VBankCode
            End Get
            Set(ByVal value As String)
                VBankCode = value
            End Set
        End Property

        Public Property recipient_name() As String
            Get
                Return Vrecipient_name
            End Get
            Set(ByVal value As String)
                Vrecipient_name = value
            End Set
        End Property
        Public Property IfscCode() As String
            Get
                Return VIfscCode
            End Get
            Set(ByVal value As String)
                VIfscCode = value
            End Set
        End Property
        Public Property AccountNo() As String
            Get
                Return VAccountNo
            End Get
            Set(ByVal value As String)
                VAccountNo = value
            End Set
        End Property
        Public Property customermobile() As String
            Get
                Return Vcustomermobile
            End Get
            Set(ByVal value As String)
                Vcustomermobile = value
            End Set
        End Property
        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property
    End Class
    Public Class Recipients_Details_API_Parameters
        Dim VMobile, VKey, VRecipient_Id As String

        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Recipient_Id() As String
            Get
                Return VRecipient_Id
            End Get
            Set(ByVal value As String)
                VRecipient_Id = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property
    End Class
    Public Class Receipent_List_API_Parameters
        Dim VMobile, VKey As String

        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property

    End Class
    Public Class Money_Transfer_API_Parameters
        Dim Vcust_id, Vmerchant_document_id, Vchannel, VRecipent, VIFSC, VKey As String
        Dim Vamount, Vrecipient_id, Vmerchant_document_id_type As Integer

        Public Property recipient_id() As Integer
            Get
                Return Vrecipient_id
            End Get
            Set(ByVal value As Integer)
                Vrecipient_id = value
            End Set
        End Property
        Public Property cust_id() As String
            Get
                Return Vcust_id
            End Get
            Set(ByVal value As String)
                Vcust_id = value
            End Set
        End Property
        Public Property amount() As Integer
            Get
                Return Vamount
            End Get
            Set(ByVal value As Integer)
                Vamount = value
            End Set
        End Property
        Public Property merchant_document_id_type() As Integer
            Get
                Return Vmerchant_document_id_type
            End Get
            Set(ByVal value As Integer)
                Vmerchant_document_id_type = value
            End Set
        End Property

        Public Property merchant_document_id() As String
            Get
                Return Vmerchant_document_id
            End Get
            Set(ByVal value As String)
                Vmerchant_document_id = value
            End Set
        End Property
        Public Property channel() As String
            Get
                Return Vchannel
            End Get
            Set(ByVal value As String)
                Vchannel = value
            End Set
        End Property

        Public Property Recipent() As String
            Get
                Return VRecipent
            End Get
            Set(ByVal value As String)
                VRecipent = value
            End Set
        End Property
        Public Property IFSC() As String
            Get
                Return VIFSC
            End Get
            Set(ByVal value As String)
                VIFSC = value
            End Set
        End Property
        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property

    End Class
    Public Class VerifyOtp_API_Parameters
        Dim VOtp, VMobile, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Otp() As String
            Get
                Return VOtp
            End Get
            Set(ByVal value As String)
                VOtp = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property
    End Class

    Public Class ResendOtp_API_Parameters
        Dim VMobile, VKey As String


        Public Property Mobile() As String
            Get
                Return VMobile
            End Get
            Set(ByVal value As String)
                VMobile = value
            End Set
        End Property

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property
    End Class

    Public Function GetApiResult(APIMethod As String) As String
        Dim ApiResult As String = ""
        Dim StrParameters As String = ""
        Dim API_URLS As String = ""

        Try

            If APIMethod = "Verify_Customer_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Verify_Customer_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Verify_Customer_API_URL
            ElseIf APIMethod = "Receipent_List_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Receipent_List_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Receipent_List_API_URL
            ElseIf APIMethod = "Recipients_Details_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Recipients_Details_API_Parameters()
                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                setParameter_API_Obj.Recipient_Id = GV.parseString(lblReceipentId.Text.Trim)
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Recipients_Details_API_URL
            ElseIf APIMethod = "Bank_Details_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Bank_Details_API_Parameters()
                setParameter_API_Obj.BankCode = GV.parseString(ddlSelectBank.SelectedValue.Trim)
                setParameter_API_Obj.ifsc = GV.parseString(txtIFSCCode.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Bank_Details_API_URL
            ElseIf APIMethod = "Verify_Bank_Details_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New Verify_Bank_Details_API_Parameters()
                setParameter_API_Obj.AccountNo = GV.parseString(txtBankAccountNo.Text.Trim)
                setParameter_API_Obj.BankAccountCode = GV.FL.AddInVar("BankID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList where Code='" & GV.parseString(ddlSelectBank.SelectedValue.Trim) & "'")
                setParameter_API_Obj.MobileNo = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Verify_Bank_Details_API_URL
            ElseIf APIMethod = "Money_Transfer_API_Parameters" Then
                Dim setParameter_API_Obj As New Money_Transfer_API_Parameters()

                setParameter_API_Obj.recipient_id = GV.parseString(lblReceipentId.Text.Trim)
                setParameter_API_Obj.cust_id = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.amount = GV.parseString(txtEnterAmt.Text.Trim)
                setParameter_API_Obj.merchant_document_id_type = "1"
                setParameter_API_Obj.merchant_document_id = "AAICB5338D"
                setParameter_API_Obj.channel = GV.parseString(ddlTransferMode.SelectedValue.Trim)
                setParameter_API_Obj.Recipent = GV.parseString(lblReceipentName.Text.Trim)
                setParameter_API_Obj.IFSC = GV.parseString(lblIFSCCode.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Money_Transfer_API_URL
            ElseIf APIMethod = "Add_New_Customer_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New Add_New_Customer_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtMobileNo.Text.Trim)
                setParameter_API_Obj.Name = GV.parseString(txtCustomerName.Text.Trim)
                setParameter_API_Obj.DOB = "10/dec/1999"
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Add_New_Customer_API_URL
            ElseIf APIMethod = "Add_New_Recipients_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New Add_New_Recipients_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtRecepientMobileNo.Text.Trim)
                setParameter_API_Obj.BankCode = GV.FL.AddInVar("BankID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList where Code='" & GV.parseString(ddlSelectBank.SelectedValue.Trim) & "'")
                setParameter_API_Obj.recipient_name = GV.parseString(lblRecepientActualName.Text.Trim)
                setParameter_API_Obj.IfscCode = GV.parseString(txtIFSCCode.Text.Trim)
                setParameter_API_Obj.AccountNo = GV.parseString(txtBankAccountNo.Text.Trim)
                setParameter_API_Obj.customermobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = Add_New_Recipients_API_URL

            ElseIf APIMethod = "VerifyOtp_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New VerifyOtp_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Otp = GV.parseString(txtEnterOTP.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = VerifyOtp_API_URL
            ElseIf APIMethod = "ResendOtp_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New ResendOtp_API_Parameters

                setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = ResendOtp_API_URL
            End If


            ApiResult = ReadbyRestClient(API_URLS, StrParameters)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return ApiResult
        End Try
        Return ApiResult
    End Function
    Public Function ReadbyRestClient(Urls As String, Parameter As String) As String

        Dim API_Name, Trans_ID, Trans_DateTime, Request_String, Response_String, AgentID, AgentType As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        AgentID = ""
        AgentType = ""

        Dim strQry As String = ""

        Dim str As String = ""
        Dim LogString As String = ""
        Try
            lblTransId.Text = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(lblTransId.Text) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine

            API_Name = "Money Transfer API"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text

            Dim client = New RestClient(Urls)
            Dim request = New RestRequest(Method.POST)
            request.AddHeader("postman-token", "7ddff860-92b7-0308-c3fb-97d9a73d4cfc")
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("content-type", "application/x-www-form-urlencoded")
            request.AddParameter("application/x-www-form-urlencoded", Parameter, ParameterType.RequestBody)

            Dim response As IRestResponse = client.Execute(request)
            str = response.Content
            str = str.Trim
            Response_String = GV.parseString(str)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine

            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function

    Public Function ReadbyRestClient_2(Urls As String, Parameter As String) As String

        Dim API_Name, Trans_ID, Trans_DateTime, Request_String, Response_String, AgentID, AgentType As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        AgentID = ""
        AgentType = ""

        Dim strQry As String = ""

        Dim str As String = ""
        Dim LogString As String = ""
        Try
            lblTransId_2.Text = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(lblTransId_2.Text) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine

            API_Name = "Money Transfer API-2"
            Trans_ID = GV.parseString(lblTransId_2.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text

            str = New System.Net.WebClient().DownloadString(Urls)

            str = str.Trim
            Response_String = GV.parseString(str)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine

            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("MONEYTRANSFER_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    Dim APIResult As String = ""
    Dim strBuild As String = ""
    Protected Sub btnSearchCustomerGo_Click(sender As Object, e As EventArgs) Handles btnSearchCustomerGo.Click
        Try
            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""
            If txtEnterMobileNo.Text.Trim = "" Then
                lblSearchCustomerError.Text = "Enter Mobile No."
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If
            Dim MoneyTrasfer As String = ""
            If ddlGateway.SelectedValue.Trim.ToUpper = "MoneyTransferAPI".Trim.ToUpper Then
                MoneyTrasfer = "MoneyTransferAPI_Status"
            Else
                MoneyTrasfer = "MoneyTransferAPI_2_Status"
            End If


            '///// Start Check API  STATUS Super ADmin Level

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Super ADmin Level

            '///// Start Check API  STATUS System Settings 

            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 



            '//// Verify Customer Mobile No
            If ddlGateway.SelectedValue.Trim.ToUpper = "MoneyTransferAPI".Trim.ToUpper Then



                If GV.parseString(lblBank_Limit_1.Text) = "" Then
                    lblBank_Limit_1.Text = "0"
                End If

                If GV.parseString(lblBank_Limit_2.Text) = "" Then
                    lblBank_Limit_2.Text = "0"
                End If

                If GV.parseString(lblBank_Limit_3.Text) = "" Then
                    lblBank_Limit_3.Text = "0"
                End If


                APIResult = GetApiResult_NEW("QueryRemitter_API_Parameters")
                '/// parse and read data in list format through json parse
                Dim json_ As String = APIResult
                Dim ser_ As JObject = JObject.Parse(json_)
                Dim name As String = ""
                Dim MobileNO As String = ""
                Dim VerifyMessage As String = ser_.SelectToken("message").ToString.Trim

                txtEnterMobileNo.ReadOnly = True
                btnSearchCustomerGo.Enabled = False
                btnSearchCustomerGo.CssClass = "btn btn-primary"


                If VerifyMessage = "Remitter not registered OTP sent for new registration." Then

                    lblSearchCustomerError.Text = VerifyMessage
                    lblSearchCustomerError.CssClass = "errorlabels"

                    lblSearchCustomerError.Visible = True
                    btnAddNewCustomer.Visible = True
                    txtEnterMobileNo.ReadOnly = True
                    btnSearchCustomerGo.Enabled = False
                    btnSearchCustomerGo.CssClass = "btn btn-primary"

                    lblstateresp.Text = ser_.SelectToken("stateresp").ToString.Trim

                    'lblSearchCustomerError.Text = VerifyMessage
                    'lblSearchCustomerError.CssClass = "errorlabels"
                    'lblSearchCustomerError.Visible = True
                    'btnAddNewCustomer.Visible = True
                    'txtEnterMobileNo.ReadOnly = True
                    'btnSearchCustomerGo.Enabled = False
                    'btnSearchCustomerGo.CssClass = "btn btn-primary"

                ElseIf VerifyMessage = "Remitter details fetch successfully." Then



                    Dim data_ As List(Of JToken) = ser_.Children().ToList
                    '// Loop through data
                    For Each item As JProperty In data_
                        item.CreateReader()
                        Select Case item.Name

                            Case "response_code"

                                strBuild = strBuild & Environment.NewLine & " response_code : " & ser_.SelectToken("response_code").ToString

                            Case "data"

                                Dim data1 As List(Of JToken) = item.Children().ToList

                                For Each msg As JObject In data1


                                    '/// Fix Attribute Name and get its value
                                    name = msg("fname").ToString & " " & msg("lname").ToString
                                    MobileNO = msg("mobile")

                                    lblBank_Limit_1.Text = msg("bank1_limit").ToString
                                    lblBank_Limit_2.Text = msg("bank2_limit").ToString
                                    lblBank_Limit_3.Text = msg("bank3_limit").ToString

                                    If GV.parseString(lblBank_Limit_1.Text) = "" Then
                                        lblBank_Limit_1.Text = "0"
                                    End If

                                    If GV.parseString(lblBank_Limit_2.Text) = "" Then
                                        lblBank_Limit_2.Text = "0"
                                    End If

                                    If GV.parseString(lblBank_Limit_3.Text) = "" Then
                                        lblBank_Limit_3.Text = "0"
                                    End If

                                    'lblBank_Limit_1.Visible = True
                                    'lblBank_Limit_2.Visible = True
                                    'lblBank_Limit_3.Visible = True

                                Next
                            'Case "response_type_id"
                            '    strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                            Case "message"
                                strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                            Case "status"
                                strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                        End Select
                    Next

                    DIV_Clear()

                    Div_CustomerDetails.Visible = True


                    Dim dt As New DataTable

                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Name")
                    Dim dc3 As DataColumn = New DataColumn("MobileNo")
                    Dim dc4 As DataColumn = New DataColumn("Message")
                    Dim dc5 As DataColumn = New DataColumn("Limit")


                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)


                    Dim dr1 As DataRow = dt.NewRow()

                    dr1(0) = 1
                    dr1(1) = name
                    dr1(2) = MobileNO
                    dr1(3) = VerifyMessage
                    dr1(4) = lblBank_Limit_1.Text
                    dt.Rows.Add(dr1)

                    gdvCustomerDetails.DataSource = dt
                    gdvCustomerDetails.DataBind()
                    'CheckRecepient()
                    Fill_Recepient_List()
                    txtEnterMobileNo.ReadOnly = True
                    btnSearchCustomerGo.Enabled = False
                    btnSearchCustomerGo.CssClass = "btn btn-primary"



                ElseIf VerifyMessage = "Authentication failed" Then
                    'Otp very div visible true

                    Dim data_ As List(Of JToken) = ser_.Children().ToList

                    For Each item As JProperty In data_
                        item.CreateReader()
                        Select Case item.Name
                            Case "response_status_id"

                                strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_status_id").ToString

                            Case "data"
                                Dim data1 As List(Of JToken) = item.Children().ToList
                                For Each msg As JObject In data1

                                    '/// Fix Attribute Name and get its value
                                    name = msg("fname").ToString & " " & msg("lname").ToString
                                    MobileNO = msg("mobile")

                                    lblBank_Limit_1.Text = msg("bank1_limit").ToString
                                    lblBank_Limit_2.Text = msg("bank2_limit").ToString
                                    lblBank_Limit_3.Text = msg("bank3_limit").ToString

                                    If GV.parseString(lblBank_Limit_1.Text) = "" Then
                                        lblBank_Limit_1.Text = "0"
                                    End If

                                    If GV.parseString(lblBank_Limit_2.Text) = "" Then
                                        lblBank_Limit_2.Text = "0"
                                    End If

                                    If GV.parseString(lblBank_Limit_3.Text) = "" Then
                                        lblBank_Limit_3.Text = "0"
                                    End If

                                    'lblBank_Limit_1.Visible = True
                                    'lblBank_Limit_2.Visible = True
                                    'lblBank_Limit_3.Visible = True

                                Next
                            Case "response_type_id"
                                strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                            Case "message"
                                strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                            Case "status"
                                strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                        End Select
                    Next


                    DIV_Clear()
                    'Div_VerifyOTP.Visible = True
                    Div_CustomerDetails.Visible = True


                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Name")
                    Dim dc3 As DataColumn = New DataColumn("MobileNo")
                    Dim dc4 As DataColumn = New DataColumn("Message")
                    Dim dc5 As DataColumn = New DataColumn("Limit")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)


                    Dim dr1 As DataRow = dt.NewRow()

                    dr1(0) = 1
                    dr1(1) = name
                    dr1(2) = MobileNO
                    dr1(3) = VerifyMessage
                    dr1(4) = lblBank_Limit_1.Text
                    dt.Rows.Add(dr1)

                    gdvCustomerDetails.DataSource = dt
                    gdvCustomerDetails.DataBind()




                Else

                    lblSearchCustomerError.Text = VerifyMessage
                    lblSearchCustomerError.CssClass = "errorlabels"
                    lblSearchCustomerError.Visible = True
                    txtEnterMobileNo.ReadOnly = False
                    btnSearchCustomerGo.Enabled = True
                    btnSearchCustomerGo.CssClass = "btn btn-primary"
                End If



            Else

                'Member No:RKITAPI190212
                'Password:	5nrg7nrmz4
                'Api Password:	cg45ob8
                'Encryption Key:77bxjoceldz46lrm...

                Dim partner_id, api_password, mobile_no, operator_code, amount, partner_request_id, circle, recharge_type, user_var1 As String
                'PARTNERID,api_password,operator_code,amount,partner_request_id,circle,user_var1,p1,p2,p3
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                partner_id = "RKITAPI190212"
                api_password = "cg45ob8"
                Dim apistr As String = ""
                'partner_request_id = GV.RandomTransactionPin()
                'circle = GV.FL.AddInVar("CircleCode", "CRM_StateMaster where State_Name = (select State from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "')")
                'recharge_type = "NORMAL"
                'user_var1 = RetailerID


                apistr = "https://rechargkit.biz/get/dmr/validateSender?partner_id=RKITAPI190212&api_password=cg45ob8&name=&mobile_no=" & GV.parseString(txtEnterMobileNo.Text.Trim) & ""

                'APIResult = ReadbyRestClient_GATEWAY2(apistr, apistr, RetailerID, Group)
                APIResult = ReadbyRestClient_2(apistr, apistr)


                Dim json1 As JObject = JObject.Parse(APIResult)

                'Fetch data from data root

                Dim MESSAGE As String = json1.SelectToken("MESSAGE").ToString
                Dim VERIFIED As String = json1.SelectToken("VERIFIED").ToString
                txtEnterMobileNo.ReadOnly = True
                btnSearchCustomerGo.Enabled = False
                btnSearchCustomerGo.CssClass = "btn btn-primary"
                If VERIFIED = "1" Then

                    'Fetch data from data root

                    MESSAGE = json1.SelectToken("MESSAGE").ToString
                    VERIFIED = json1.SelectToken("VERIFIED").ToString
                    Dim name As String = json1.SelectToken("DETAILS").Item("name").ToString
                    Dim mobileNumber As String = json1.SelectToken("DETAILS").Item("mobileNumber").ToString

                    DIV_Clear_2()
                    Div_CustomerDetails_2.Visible = True

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Name")
                    Dim dc3 As DataColumn = New DataColumn("MobileNo")
                    Dim dc4 As DataColumn = New DataColumn("Message")
                    Dim dc5 As DataColumn = New DataColumn("Limit")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)

                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = 1
                    dr1(1) = name
                    dr1(2) = mobileNumber
                    dr1(3) = MESSAGE
                    dr1(4) = lblBank_Limit_1.Text

                    dt.Rows.Add(dr1)
                    gdvCustomerDetails_2.DataSource = dt
                    gdvCustomerDetails_2.DataBind()

                    Fill_Recepient_List_2()
                Else
                    lblSearchCustomerError.Text = MESSAGE
                    lblSearchCustomerError.CssClass = "errorlabels"
                    lblSearchCustomerError.Visible = True
                    btnAddNewCustomer.Visible = True
                    txtEnterMobileNo.ReadOnly = True
                    btnSearchCustomerGo.Enabled = False
                    btnSearchCustomerGo.CssClass = "btn btn-primary"
                End If



                'AddSender
                'https://rechargkit.biz/get/dmr/addSender?partner_id=RKITAPI190212&api_password=cg45ob8&name=sapnarana&mobile_no=9999180633&city=Delhi&state=Delhi&address1=Delhi&pincode=110085
                '{"ERROR":23,"VERIFIED":"","DETAILS":"","MESSAGE":"You are not authorised to use this service."}
                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"name":"sapnarana","mobileNumber":"9999180633","city":"Delhi","state":"Delhi","pincode":"110085","address":"Delhi"},"MESSAGE":"Sender added successfully"}

                'validateSender
                'https://rechargkit.biz/get/dmr/validateSender?partner_id=RKITAPI190212&api_password=cg45ob8&name=sapnarana&mobile_no=9999180633

                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"name":"sapnarana","mobileNumber":"9999180633","city":"Delhi","state":"Delhi","pincode":null,"address":"Delhi"},"MESSAGE":"Sender is valid"}
                '{"ERROR":1,"VERIFIED":0,"DETAILS":"","MESSAGE":"Sender doesn't exits"}

                'SenderLimit
                'https://rechargkit.biz/get/dmr/SenderLimit?partner_id=RKITAPI190212&api_password=cg45ob8&name=eklavya&mobile_no=9212345320
                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"dailyLimit":5000,"remainingMonthlyLimit":25000},"MESSAGE":"Limit successfully fetched"}
                '{"ERROR":1,"VERIFIED":0,"DETAILS":"","MESSAGE":"Customer mobile does not exist"}

                'AddBeneficiary
                'https://rechargkit.biz/get/dmr/AddBeneficiary?partner_id=XXXX&api_password=XXXX&name=XXXX&mobile_no=XXXX&name=XXXX&email=XXXX&bankAccountNumber=XXXX&ifscCode=XXXX&address1=XXXX&city=XXXX&state=XXXX&pincode=XXXX&vpa=XXXX&bankName= XXXX
                'https://rechargkit.biz/get/dmr/AddBeneficiary?partner_id=RKITAPI190212&api_password=cg45ob8&name=eklavyaverma&mobile_no=9212345320&name=eklavyaverma&email=verma.eklavya@gmail.com&bankAccountNumber=73290100008155&ifscCode=barb0dbrohi&address1=delhi&city=delhi&state=delhi&pincode=110085&vpa=XXXX&bankName=Bankofbaroda
                '{"ERROR":0,"DETAILS":{"beneficiaryId":"ABT1612515184482","name":"Sapnarana","email":"sanarana44@gmail.com","benefMobileNumber":"9999180633","bankAccountNumber":"65209313469","ifscCode":"sbin0050722","address1":"delhi","city":"delhi","state":"delhi","pincode":"110085","vpa":"XXXX","bankName":"StateBankofIndia"},"MESSAGE":"Benficiary  added successfully"}
                '{"ERROR":0,"DETAILS":{"beneficiaryId":"ABT1612515412546","name":"eklavyaverma","email":"verma.eklavya@gmail.com","benefMobileNumber":"9212345320","bankAccountNumber":"73290100008155","ifscCode":"barb0dbrohi","address1":"delhi","city":"delhi","state":"delhi","pincode":"110085","vpa":"XXXX","bankName":"Bankofbaroda"},"MESSAGE":"Benficiary  added successfully"}


                'getBeneId

                'https://rechargkit.biz/get/dmr/getBeneId?partner_id=XXXX&api_password=XXXX&bankAccountNumber=XXXX&ifscCode= XXXX
                'https://rechargkit.biz/get/dmr/getBeneId?partner_id=RKITAPI190212&api_password=cg45ob8&bankAccountNumber=73290100008158&ifscCode=barb0dbrohi
                '{"ERROR":0,"VERIFIED":1,"DETAILS":{"beneficiaryId":"ABT1612515412546","name":"eklavyaverma","email":"verma.eklavya@gmail.com","benefMobileNumber":"9212345320","bankAccountNumber":"73290100008155","ifscCode":"barb0dbrohi","address1":"delhi","city":"delhi","state":"delhi","pincode":"110085","vpa":"XXXX","bankName":"Bankofbaroda"},"MESSAGE":"Benficiary details fetched."}
                '{"ERROR":1,"VERIFIED":0,"DETAILS":"","MESSAGE":"Beneficiary not found with given bank account details"}


                'Get Beneficiary List
                'https://rechargkit.biz/get/dmr/beneficiaryList?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=9212345320

                'moneyTransfer
                'https://rechargkit.biz/get/dmr/moneyTransfer?partner_id=XXXX&api_password=XXXX&mobile_no=XXXX&beneId=XXXX&amount=10.00&partner_request_id= XXXX
                'https://rechargkit.biz/get/dmr/moneyTransfer?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=9999180633&beneId=ABT1612515412546&amount=10.00&partner_request_id=52611
                '{"ERROR":0,"STATUS":2,"ORDERID":52305281,"OPTRANSID":"Bad Request","PARTNERREQID":"5261","MESSAGE":"Success","COMMISSION":"0.0000","CHARGE":"8.26"}
                '{"ERROR":0,"STATUS":2,"ORDERID":52309336,"OPTRANSID":"e57b117c679e11ebaef80a0047330000","PARTNERREQID":"526","MESSAGE":"Success","COMMISSION":"0.0000","CHARGE":"8.26"}

                'AddBeneficiaryPennyDrop
                'https://rechargkit.biz/get/dmr/AddBeneficiaryPennyDrop?partner_id=XXXX&api_password=XXXX&mobile_no=XXXX&name=XXXX&email=XXXX&bankAccountNumber=XXXX&ifscCode=XXXX&address1=XXXX&city=XXXX&state=XXXX&pincode=XXXX&vpa=XXXX&benefMobileNumber=XXXX&partner_request_id= XXXX
                'https://rechargkit.biz/get/dmr/AddBeneficiaryPennyDrop?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=9999180633&name=XXXX&email=eklavyaverma&bankAccountNumber=73290100008155&ifscCode=barb0dbrohi&address1=delhi&city=delhi&state=delhi&pincode=110085&vpa=XXXX&benefMobileNumber=9212345320&partner_request_id=526

            End If

            ' txttestBox.Text = strBuild
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnAddNewCustomer_Click(sender As Object, e As EventArgs) Handles btnAddNewCustomer.Click
        Try
            If ddlGateway.SelectedValue.Trim.ToUpper = "MoneyTransferAPI".Trim.ToUpper Then
                If Not txtEnterMobileNo.Text.Trim = "" Then
                    txtMobileNo.Text = GV.parseString(txtEnterMobileNo.Text.Trim)
                End If

                lblAddCustomerError.Text = ""
                lblAddCustomerError.CssClass = ""
                txtCustomerName.Text = ""
                lblSearchCustomerError.Text = ""
                lblSearchCustomerError.CssClass = ""
                lblSearchCustomerError.Visible = False
                btnAddNewCustomer.Visible = False
                'Div_VerifyOTP.Visible = True
                Div_AddCustomer.Visible = True
            Else
                If Not txtEnterMobileNo.Text.Trim = "" Then
                    txtMobileNo_2.Text = GV.parseString(txtEnterMobileNo.Text.Trim)
                End If

                lblAddCustomerError.Text = ""
                lblAddCustomerError.CssClass = ""
                txtCustomerName_2.Text = ""
                lblSearchCustomerError.Text = ""
                lblSearchCustomerError.CssClass = ""
                lblSearchCustomerError.Visible = False
                btnAddNewCustomer.Visible = False
                Div_AddCustomer_2.Visible = True
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnAddCustomerGo_Click(sender As Object, e As EventArgs) Handles btnAddCustomerGo.Click
        Try

            lblAddCustomerError.Text = ""
            lblAddCustomerError.CssClass = ""
            If txtCustomerName.Text.Trim = "" Then
                lblAddCustomerError.Text = "Please Enter Customer Name."
                lblAddCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If


            If TextBox1.Text.Trim = "" Then
                lblAddCustomerError.Text = "Please Enter OTP ."
                lblAddCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If



            'RegisterRemitter_API_Parameters


            '//// Verify Customer Mobile No
            'APIResult = GetApiResult("Add_New_Customer_API_Parameters")

            APIResult = GetApiResult_NEW("RegisterRemitter_API_Parameters")

            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)

            '#EK1
            Dim data_ As List(Of JToken) = ser_.Children().ToList
            Dim msg1 As String = ser_.SelectToken("message").ToString


            '// Loop through data
            For Each item As JProperty In data_
                item.CreateReader()
                Select Case item.Name
                    Case "response_code"

                        strBuild = strBuild & Environment.NewLine & " response_code : " & ser_.SelectToken("response_code").ToString

                    Case "data"
                        Dim data1 As List(Of JToken) = item.Children().ToList
                        For Each msg As JObject In data1
                            '/// Dynamic Name and get its value
                            For Each p In msg
                                strBuild = strBuild & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString
                            Next
                        Next

                    Case "response_type_id"
                        strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                    Case "message"
                        strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                    Case "status"
                        strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                End Select
            Next

            If msg1.Trim.ToUpper = "Remitter Successfully Registered".Trim.ToUpper Then
                DIV_Clear()
                Div_VerifyOTP.Visible = False
                Div_CustomerDetails.Visible = False

                APIResult = GetApiResult_NEW("QueryRemitter_API_Parameters")
                '/// parse and read data in list format through json parse
                Dim json1_ As String = APIResult
                Dim ser1_ As JObject = JObject.Parse(json1_)
                Dim name As String = ""
                Dim MobileNO As String = ""
                Dim VerifyMessage As String = ser1_.SelectToken("message").ToString.Trim

                txtEnterMobileNo.ReadOnly = True
                btnSearchCustomerGo.Enabled = False
                btnSearchCustomerGo.CssClass = "btn btn-primary"

                If VerifyMessage = "Remitter details fetch successfully." Then
                    Dim data1_ As List(Of JToken) = ser1_.Children().ToList
                    '// Loop through data
                    For Each item As JProperty In data1_
                        item.CreateReader()
                        Select Case item.Name
                            Case "response_code"

                                strBuild = strBuild & Environment.NewLine & " response_code : " & ser1_.SelectToken("response_code").ToString

                            Case "data"
                                Dim data1 As List(Of JToken) = item.Children().ToList
                                For Each msg As JObject In data1

                                    '/// Fix Attribute Name and get its value
                                    name = msg("fname").ToString & " " & msg("lname").ToString
                                    MobileNO = msg("mobile")
                                Next
                            'Case "response_type_id"
                            '    strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                            Case "message"
                                strBuild = strBuild & Environment.NewLine & " message : " & ser1_.SelectToken("message").ToString
                            Case "status"
                                strBuild = strBuild & Environment.NewLine & " status : " & ser1_.SelectToken("status").ToString
                        End Select
                    Next
                    DIV_Clear()

                    Div_CustomerDetails.Visible = True

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Name")
                    Dim dc3 As DataColumn = New DataColumn("MobileNo")
                    Dim dc4 As DataColumn = New DataColumn("Message")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)

                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = 1
                    dr1(1) = name
                    dr1(2) = MobileNO
                    dr1(3) = VerifyMessage
                    dt.Rows.Add(dr1)
                    gdvCustomerDetails.DataSource = dt
                    gdvCustomerDetails.DataBind()


                    Fill_Recepient_List()

                    txtEnterMobileNo.ReadOnly = True
                    btnSearchCustomerGo.Enabled = False
                    btnSearchCustomerGo.CssClass = "btn btn-primary"
                End If


            Else
                DIV_Clear()
                Div_VerifyOTP.Visible = False
                Div_CustomerDetails.Visible = False
            End If







        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnAddCustomerGo_2_Click(sender As Object, e As EventArgs) Handles btnAddCustomerGo_2.Click
        Try

            lblAddCustomerError_2.Text = ""
            lblAddCustomerError_2.CssClass = ""
            If txtCustomerName_2.Text.Trim = "" Then
                lblAddCustomerError_2.Text = "Please Enter Customer Name."
                lblAddCustomerError_2.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim partner_id, api_password As String
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            partner_id = "RKITAPI190212"
            api_password = "cg45ob8"
            Dim apistr As String = ""
            'partner_request_id = GV.RandomTransactionPin()
            'circle = GV.FL.AddInVar("CircleCode", "CRM_StateMaster where State_Name = (select State from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "')")
            'recharge_type = "NORMAL"
            'user_var1 = RetailerID


            apistr = "https://rechargkit.biz/get/dmr/addSender?partner_id=RKITAPI190212&api_password=cg45ob8&name=" & txtCustomerName_2.Text.Trim & "&mobile_no=" & txtMobileNo_2.Text.Trim & "&city=Delhi&state=Delhi&address1=Delhi&pincode=110085"
            '{"ERROR":0,"VERIFIED":1,"DETAILS":{"name":"sapnarana","mobileNumber":"9999180633","city":"Delhi","state":"Delhi","pincode":"110085","address":"Delhi"},"MESSAGE":"Sender added successfully"}

            APIResult = ReadbyRestClient_2(apistr, apistr)


            Dim json1 As JObject = JObject.Parse(APIResult)

            'Fetch data from data root

            Dim MESSAGE As String = json1.SelectToken("MESSAGE").ToString
            Dim VERIFIED As String = json1.SelectToken("VERIFIED").ToString
            Dim name As String = json1.SelectToken("DETAILS").Item("name").ToString
            Dim mobileNumber As String = json1.SelectToken("DETAILS").Item("mobileNumber").ToString
            If VERIFIED = "1" Then
                DIV_Clear_2()
                Div_CustomerDetails_2.Visible = True

                Dim dt As New DataTable
                Dim dc1 As DataColumn = New DataColumn("SrNo")
                Dim dc2 As DataColumn = New DataColumn("Name")
                Dim dc3 As DataColumn = New DataColumn("MobileNo")
                Dim dc4 As DataColumn = New DataColumn("Message")

                dt.Columns.Add(dc1)
                dt.Columns.Add(dc2)
                dt.Columns.Add(dc3)
                dt.Columns.Add(dc4)

                Dim dr1 As DataRow = dt.NewRow()
                dr1(0) = 1
                dr1(1) = name
                dr1(2) = mobileNumber
                dr1(3) = MESSAGE

                dt.Rows.Add(dr1)
                gdvCustomerDetails_2.DataSource = dt
                gdvCustomerDetails_2.DataBind()
            Else
                lblAddCustomerError_2.Text = MESSAGE
                lblAddCustomerError_2.CssClass = "errorlabels"
                Exit Sub
            End If

            Fill_Recepient_List_2()

            ' Fill_Recepient_List()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub Fill_Recepient_List_2()
        Try
            grdAddRecepient_2.DataSource = Nothing
            grdAddRecepient_2.DataBind()

            Dim partner_id, api_password As String
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            partner_id = "RKITAPI190212"
            api_password = "cg45ob8"
            Dim apistr As String = ""

            apistr = "https://rechargkit.biz/get/dmr/beneficiaryList?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=" & txtEnterMobileNo.Text.Trim & ""
            '{"ERROR":0,"VERIFIED":1,"DATA":[{"beneficiary_id":"ABT1612515412546","mobile_no":"9212345320","name":"","email":"verma.eklavya@gmail.com","bankAccountNumber":"73290100008155","ifscCode":"barb0dbrohi","bankName":"Bankofbaroda","vpa":"XXXX","address1":"delhi","city":"delhi","state":"delhi","pincode":"110085"},
            APIResult = ReadbyRestClient_2(apistr, apistr)





            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("SrNo")
            Dim dc2 As DataColumn = New DataColumn("BankName")
            Dim dc3 As DataColumn = New DataColumn("IFSC")
            Dim dc4 As DataColumn = New DataColumn("AccountNo")
            Dim dc5 As DataColumn = New DataColumn("Receipent")
            Dim dc6 As DataColumn = New DataColumn("ReceipentId")
            Dim dc7 As DataColumn = New DataColumn("MobileNo")
            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            dt.Columns.Add(dc5)
            dt.Columns.Add(dc6)
            dt.Columns.Add(dc7)
            ' Dim json1 As JObject = JObject.Parse(APIResult)
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim data_ As List(Of JToken) = ser_.Children().ToList


            Dim json As JObject = JObject.Parse(APIResult)


            Dim account_Tk, ifsc_Tk, recipient_name_Tk, recipient_mobile_Tk, bank_Tk, RECID_Tk, RECMobileNO_Tk As JToken
            For Each Row In json("DATA").ToList()


                Dim account, ifsc, recipient_name, recipient_mobile, bank, RECID, RECMobileNO As String
                account_Tk = Row.Item("bankAccountNumber")
                account = DirectCast(account_Tk, JValue).Value

                ifsc_Tk = Row.Item("ifscCode")
                ifsc = DirectCast(ifsc_Tk, JValue).Value

                recipient_name_Tk = Row.Item("name")
                recipient_name = DirectCast(recipient_name_Tk, JValue).Value

                recipient_mobile_Tk = Row.Item("mobile_no")
                recipient_mobile = DirectCast(recipient_mobile_Tk, JValue).Value


                bank_Tk = Row.Item("bankName")
                bank = DirectCast(bank_Tk, JValue).Value


                RECID_Tk = Row.Item("beneficiary_id")
                RECID = DirectCast(RECID_Tk, JValue).Value


                RECMobileNO_Tk = Row.Item("mobile_no")
                RECMobileNO = DirectCast(RECMobileNO_Tk, JValue).Value

                Dim dr1 As DataRow

                dr1 = dt.NewRow()
                dr1(1) = bank
                dr1(2) = ifsc
                dr1(3) = account
                dr1(4) = recipient_name
                dr1(5) = RECID
                dr1(6) = RECMobileNO
                dt.Rows.Add(dr1)

            Next

            grdAddRecepient_2.DataSource = dt
            grdAddRecepient_2.DataBind()
            GV.FL.showSerialnoOnGridView(grdAddRecepient_2, 1)


            If grdAddRecepient_2.Rows.Count > 0 Then
                Div_RecepientDetails_2.Visible = True
                lblError_2.Visible = False
            Else
                grdAddRecepient_2.DataSource = Nothing
                grdAddRecepient_2.DataBind()
                Div_RecepientDetails_2.Visible = True
                lblError_2.Visible = True
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub

    Protected Sub btnAddRecepient_2_Click(sender As Object, e As EventArgs) Handles btnAddRecepient_2.Click
        Try
            txtBankAccountNo_2.Text = ""
            'txtRecepientMobileNo_2.Text = ""
            txtIFSCCode_2.Text = ""
            Div_TransferAmt_2.Visible = False
            Div_AddRecepient_2.Visible = True

            For i As Integer = 0 To grdAddRecepient_2.Rows.Count - 1
                grdAddRecepient_2.Rows(i).BackColor = Color.White
            Next

            ddlSelectBank_2.Items.Clear()
            GV.FL.AddInDropDownListDistinct(ddlSelectBank_2, "name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList order by name ")
            Dim bankstr As String = "Select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList order by name "
            Dim bankds As DataSet = New DataSet
            bankds = GV.FL.OpenDsWithSelectQuery(bankstr)
            For i As Integer = 0 To bankds.Tables(0).Rows.Count - 1
                Dim BankName As String = bankds.Tables(0).Rows(i).Item("name").ToString()
                Dim BankCode As String = bankds.Tables(0).Rows(i).Item("Code").ToString()
                ddlSelectBank_2.Items.Add(BankName)
                ddlSelectBank_2.Items(i).Value = BankCode
                ddlSelectBank_2.Items(i).Text = GV.parseString(BankName)
            Next

            If ddlSelectBank_2.Items.Count > 0 Then
                ddlSelectBank_2.Items.Insert(0, ":::: Select Bank ::::")
            Else
                ddlSelectBank_2.Items.Add(":::: Select Bank ::::")
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnAddRecepientGo_2_Click(sender As Object, e As EventArgs) Handles btnAddRecepientGo_2.Click
        Dim VERIFIED As String = ""
        Dim Message As String = ""
        Try
            lblRecepientError_2.Text = ""
            lblRecepientError_2.CssClass = ""

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If

            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VerifyServiceCharge) Then
            Else
                lblRecepientError_2.Text = "You Have Insufficient Wallet Amount, Service Charge Applicable Rs " & VerifyServiceCharge
                lblRecepientError_2.CssClass = "errorlabels"
                Exit Sub
            End If




            If ddlSelectBank_2.SelectedIndex = 0 Then
                lblRecepientError_2.Text = "Please Select Bank."
                lblRecepientError_2.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtName_2.Text = "" Then
                lblRecepientError_2.Text = "Please Enter Name."
                lblRecepientError_2.CssClass = "errorlabels"
                Exit Sub
            End If
            'If txtRecepientMobileNo_2.Text = "" Then
            '    lblRecepientError_2.Text = "Please Enter Mobile No."
            '    lblRecepientError_2.CssClass = "errorlabels"
            '    Exit Sub
            'End If
            If txtIFSCCode_2.Text = "" Then
                lblRecepientError_2.Text = "Please Enter IFSC."
                lblRecepientError_2.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtBankAccountNo_2.Text = "" Then
                lblRecepientError_2.Text = "Please Enter Bank AccountNo."
                lblRecepientError_2.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim partner_id, api_password, mobile_no, operator_code, amount, partner_request_id, circle, recharge_type, user_var1 As String
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            partner_id = "RKITAPI190212"
            api_password = "cg45ob8"
            Dim apistr As String = ""
            Dim Name As String = GV.get_SuperAdmin_SessionVariables("UserName", Request, Response).Trim
            Dim LoginId As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim

            Dim EmailID As String = GV.FL.AddInVar("EmailID", "BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & LoginId & "'")



            apistr = " https://rechargkit.biz/get/dmr/AddBeneficiary?partner_id=RKITAPI190212&api_password=cg45ob8&name=" & Name & "&mobile_no=" & txtEnterMobileNo.Text.Trim & "&name=" & txtName_2.Text.Trim & "&email=" & EmailID & "&bankAccountNumber=" & txtBankAccountNo_2.Text.Trim & "&ifscCode=" & txtIFSCCode_2.Text.Trim & "&address1=delhi&city=delhi&state=delhi&pincode=110085&vpa=" & LoginId & "&bankName=" & ddlSelectBank_2.SelectedValue.Trim & ""

            APIResult = ReadbyRestClient_2(apistr, apistr)
            Dim json1 As JObject = JObject.Parse(APIResult)


            VERIFIED = json1.SelectToken("ERROR").ToString
            Message = json1.SelectToken("MESSAGE").ToString
            txtEnterMobileNo.ReadOnly = True
            btnSearchCustomerGo.Enabled = False
            btnSearchCustomerGo.CssClass = "btn btn-primary"
            If VERIFIED = "0" Then
                Fill_Recepient_List_2()
                txtBankAccountNo_2.Text = ""
                ' txtRecepientMobileNo_2.Text = ""
                txtIFSCCode_2.Text = ""
                txtName_2.Text = ""
                Div_AddRecepient_2.Visible = False

            Else
                lblRecepientError_2.Text = Message
                lblRecepientError_2.CssClass = "errorlabels"
                Exit Sub
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblRecepientError_2.Text = Message
            lblRecepientError_2.CssClass = "errorlabels"
        End Try
    End Sub

    Protected Sub bntReceipientClose_2_Click(sender As Object, e As EventArgs) Handles bntReceipientClose_2.Click
        Try
            txtName_2.Text = ""
            txtBankAccountNo_2.Text = ""
            'txtRecepientMobileNo_2.Text = ""
            txtIFSCCode_2.Text = ""
            lblRecepientError_2.Text = ""
            lblRecepientError_2.CssClass = ""
            Div_AddRecepient_2.Visible = False
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Protected Sub btnGrdRowTransfer_2_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblRBankName_2.Text = ""
            lblIFSCCode_2.Text = ""
            lblReceipentName_2.Text = ""
            lblReceipentId_2.Text = ""
            lblReceipentMobileNo_2.Text = ""
            lblTranferAmtError_2.Text = ""
            lblTranferAmtError_2.CssClass = ""
            txtServiceCharge_2.Text = ""
            txtNetAmount_2.Text = ""
            txtEnterAmt_2.Text = ""
            lblService_2.Text = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex

            For i As Integer = 0 To grdAddRecepient_2.Rows.Count - 1
                grdAddRecepient_2.Rows(i).BackColor = Color.White
            Next

            grdAddRecepient_2.Rows(lblRowIndex.Text).BackColor = Color.LightGreen
            grdAddRecepient_2.Rows(lblRowIndex.Text).Cells(0).BackColor = Color.White

            lblRBankName_2.Text = GV.parseString(grdAddRecepient_2.Rows(lblRowIndex.Text).Cells(2).Text)
            lblIFSCCode_2.Text = GV.parseString(grdAddRecepient_2.Rows(lblRowIndex.Text).Cells(3).Text)
            lblRAccountNo_2.Text = GV.parseString(grdAddRecepient_2.Rows(lblRowIndex.Text).Cells(4).Text)
            lblReceipentName_2.Text = GV.parseString(grdAddRecepient_2.Rows(lblRowIndex.Text).Cells(5).Text)
            lblReceipentId_2.Text = GV.parseString(grdAddRecepient_2.Rows(lblRowIndex.Text).Cells(6).Text)
            lblReceipentMobileNo_2.Text = GV.parseString(grdAddRecepient_2.Rows(lblRowIndex.Text).Cells(7).Text)
            Div_AddRecepient_2.Visible = False
            Div_TransferAmt_2.Visible = True



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Protected Sub btnVerifyOTPNo_Click(sender As Object, e As EventArgs) Handles btnVerifyOTPNo.Click
        Try

            lblVerifyOTPError.Text = ""
            lblVerifyOTPError.CssClass = ""
            If txtEnterOTP.Text.Trim = "" Then
                lblVerifyOTPError.Text = "Please Enter OTP No."
                lblVerifyOTPError.CssClass = "errorlabels"
                Exit Sub
            End If
            '//// Verify Customer Mobile No
            APIResult = GetApiResult("VerifyOtp_API_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim VerifyMessage As String = ser_.SelectToken("message").ToString.Trim
            Dim status As String = ser_.SelectToken("status").ToString
            If status = "0" Then
                lblVerifyOTPError.CssClass = "successlabels"
            Else
                lblVerifyOTPError.CssClass = "errorlabels"
            End If
            If VerifyMessage = "Wallet opened successfully." Then
                Div_VerifyOTP.Visible = False
                Div_RecepientDetails.Visible = True
                APIResult = GetApiResult("Verify_Customer_API_Parameters")
                '/// parse and read data in list format through json parse
                json_ = APIResult
                ser_ = JObject.Parse(json_)
                Dim name As String = ""
                Dim CustomerID As String = ""
                Dim MobileNO As String = ""
                txtEnterMobileNo.ReadOnly = True
                btnSearchCustomerGo.Enabled = False
                btnSearchCustomerGo.CssClass = "btn btn-primary"

                Dim data_ As List(Of JToken) = ser_.Children().ToList
                '// Loop through data
                For Each item As JProperty In data_
                    item.CreateReader()
                    Select Case item.Name
                        Case "response_status_id"

                            strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_status_id").ToString

                        Case "data"
                            Dim data1 As List(Of JToken) = item.Children().ToList
                            For Each msg As JObject In data1

                                '/// Fix Attribute Name and get its value
                                name = msg("name")
                                CustomerID = msg("customer_id")
                                MobileNO = msg("mobile")
                            Next
                        Case "response_type_id"
                            strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                        Case "message"
                            strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                        Case "status"
                            strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                    End Select
                Next
                DIV_Clear()

                Div_CustomerDetails.Visible = True

                Dim dt As New DataTable
                Dim dc1 As DataColumn = New DataColumn("SrNo")
                Dim dc2 As DataColumn = New DataColumn("CustomerID")
                Dim dc3 As DataColumn = New DataColumn("Name")
                Dim dc4 As DataColumn = New DataColumn("MobileNo")
                Dim dc5 As DataColumn = New DataColumn("Message")

                dt.Columns.Add(dc1)
                dt.Columns.Add(dc2)
                dt.Columns.Add(dc3)
                dt.Columns.Add(dc4)
                dt.Columns.Add(dc5)
                Dim dr1 As DataRow = dt.NewRow()
                dr1(0) = 1
                dr1(1) = CustomerID
                dr1(2) = name
                dr1(3) = MobileNO
                dr1(4) = VerifyMessage
                dt.Rows.Add(dr1)
                gdvCustomerDetails.DataSource = dt
                gdvCustomerDetails.DataBind()
                ' CheckRecepient()
                Fill_Recepient_List()
            Else
                lblVerifyOTPError.Text = VerifyMessage
                Exit Sub
            End If



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnResendOTP_Click(sender As Object, e As EventArgs) Handles btnResendOTP.Click
        Try
            lblVerifyOTPError.Text = ""
            lblVerifyOTPError.CssClass = ""

            '//// Verify Customer Mobile No
            APIResult = GetApiResult("ResendOtp_API_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim VerifyMessage As String = ser_.SelectToken("message").ToString.Trim
            Dim status As String = ser_.SelectToken("status").ToString
            If status = "0" Then
                lblVerifyOTPError.Text = VerifyMessage
                lblVerifyOTPError.CssClass = "successlabels"
            Else
                lblVerifyOTPError.Text = VerifyMessage
                lblVerifyOTPError.CssClass = "errorlabels"
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub


    Public Sub DIV_Clear()
        Try
            btnAddNewCustomer.Visible = False
            Div_AddCustomer.Visible = False
            Div_VerifyOTP.Visible = False
            Div_CustomerDetails.Visible = False
            Div_RecepientDetails.Visible = False
            Div_AddRecepient.Visible = False
            Div_TransferAmt.Visible = False
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub DIV_Clear_2()
        Try
            btnAddNewCustomer.Visible = False
            Div_AddCustomer_2.Visible = False
            Div_CustomerDetails_2.Visible = False
            Div_RecepientDetails_2.Visible = False
            Div_AddRecepient_2.Visible = False
            Div_TransferAmt_2.Visible = False
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub Fill_Recepient_List()
        Try
            grdAddRecepient.DataSource = Nothing
            grdAddRecepient.DataBind()
            APIResult = GetApiResult_NEW("FetchBeneficiary_API_Parameters")



            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim Meassge As String = ser_.SelectToken("message").ToString
            If Meassge = "No recepients found" Then
                grdAddRecepient.DataSource = Nothing
                grdAddRecepient.DataBind()
                Div_RecepientDetails.Visible = True
                lblError.Visible = True
                Exit Sub
            End If

            Dim data_ As List(Of JToken) = ser_.Children().ToList
            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("SrNo")
            Dim dc2 As DataColumn = New DataColumn("BankName")
            Dim dc3 As DataColumn = New DataColumn("IFSC")
            Dim dc4 As DataColumn = New DataColumn("AccountNo")
            Dim dc5 As DataColumn = New DataColumn("Receipent")
            Dim dc6 As DataColumn = New DataColumn("ReceipentId")
            Dim dc7 As DataColumn = New DataColumn("Bankid")
            Dim dc8 As DataColumn = New DataColumn("Verified")
            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            dt.Columns.Add(dc5)
            dt.Columns.Add(dc6)
            dt.Columns.Add(dc7)
            dt.Columns.Add(dc8)


            For Each item As JProperty In data_
                item.CreateReader()
                Select Case item.Name
                    Case "response_code"

                        strBuild = strBuild & Environment.NewLine & " response_code : " & ser_.SelectToken("response_code").ToString

                    Case "data"
                        Dim dr1 As DataRow

                        If grdAddRecepient.Rows.Count > 0 Then
                            For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                                dr1 = dt.NewRow()
                                dr1(0) = GV.parseString(grdAddRecepient.Rows(i).Cells(1).Text)
                                dr1(1) = GV.parseString(grdAddRecepient.Rows(i).Cells(2).Text)
                                dr1(2) = GV.parseString(grdAddRecepient.Rows(i).Cells(3).Text)
                                dr1(3) = GV.parseString(grdAddRecepient.Rows(i).Cells(4).Text)
                                dr1(4) = GV.parseString(grdAddRecepient.Rows(i).Cells(5).Text)
                                dr1(5) = GV.parseString(grdAddRecepient.Rows(i).Cells(6).Text)
                                dr1(6) = GV.parseString(grdAddRecepient.Rows(i).Cells(7).Text)
                                dt.Rows.Add(dr1)
                            Next
                        End If
                        Dim bene_id, bankid, bankname, name, accno, ifsc, verified As String
                        For Each subitem As JObject In item.Values
                            Dim data_1 As List(Of JToken) = subitem.Children().ToList

                            For k As Integer = 0 To data_1.Count - 1

                                Dim pair() As String = Split(data_1(k).ToString.Replace("""", ""), ":")
                                If pair(0).ToString = "bankname" Then
                                    bankname = pair(1).Trim.ToString
                                ElseIf pair(0).ToString = "ifsc" Then
                                    ifsc = pair(1).Trim.ToString
                                ElseIf pair(0).ToString = "accno" Then
                                    accno = pair(1).Trim.ToString
                                ElseIf pair(0).ToString = "name" Then
                                    name = pair(1).Trim.ToString
                                ElseIf pair(0).ToString = "bene_id" Then
                                    bene_id = pair(1).Trim.ToString
                                ElseIf pair(0).ToString = "bankid" Then
                                    bankid = pair(1).Trim.ToString
                                ElseIf pair(0).ToString = "verified" Then
                                    verified = pair(1).Trim.ToString
                                End If

                            Next
                            dr1 = dt.NewRow()
                            dr1(1) = bankname
                            dr1(2) = ifsc
                            dr1(3) = accno
                            dr1(4) = name
                            dr1(5) = bene_id
                            dr1(6) = bankid
                            dr1(7) = verified
                            dt.Rows.Add(dr1)
                        Next


                        grdAddRecepient.DataSource = dt
                        grdAddRecepient.DataBind()



                        If grdAddRecepient.Rows.Count > 0 Then
                            GV.FL.showSerialnoOnGridView(grdAddRecepient, 2)

                            Div_RecepientDetails.Visible = True
                            lblError.Visible = False
                        Else
                            grdAddRecepient.DataSource = Nothing
                            grdAddRecepient.DataBind()
                            Div_RecepientDetails.Visible = True
                            lblError.Visible = True
                        End If

                    Case "response_type_id"
                        strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                    Case "message"
                        strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                    Case "status"
                        strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                End Select
            Next

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try

    End Sub


    Protected Sub btnChangeNo_Click(sender As Object, e As EventArgs) Handles btnChangeNo.Click
        Try
            DIV_Clear()
            DIV_Clear_2()
            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""
            txtEnterMobileNo.Text = ""
            gdvCustomerDetails.DataSource = Nothing
            gdvCustomerDetails.DataBind()
            grdAddRecepient.DataSource = Nothing
            grdAddRecepient.DataBind()
            txtEnterOTP.Text = ""
            txtEnterMobileNo.ReadOnly = False
            btnSearchCustomerGo.Enabled = True
            btnSearchCustomerGo.CssClass = "btn btn-primary"

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnAddRecepient_Click(sender As Object, e As EventArgs) Handles btnAddRecepient.Click
        Try
            txtBankAccountNo.Text = ""
            txtRecepientMobileNo.Text = ""
            txtIFSCCode.Text = ""
            Div_TransferAmt.Visible = False
            Div_AddRecepient.Visible = True

            For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                grdAddRecepient.Rows(i).BackColor = Color.White
            Next
            ddlSelectBank.Items.Clear()
            'GV.FL.AddInDropDownListDistinct(ddlSelectBank, "name", "" & GV.DefaultDatabase & ".dbo.MoneyTransferBankList order by name ")
            Dim bankstr As String = "Select * from " & GV.DefaultDatabase & ".dbo.MoneyTransferBankList_New order by BANKNAME "
            Dim bankds As DataSet = New DataSet
            bankds = GV.FL.OpenDsWithSelectQuery(bankstr)
            For i As Integer = 0 To bankds.Tables(0).Rows.Count - 1
                Dim BankName As String = bankds.Tables(0).Rows(i).Item("BANKNAME").ToString()
                Dim BankCode As String = bankds.Tables(0).Rows(i).Item("BANKID").ToString()
                ddlSelectBank.Items.Add(BankName)
                ddlSelectBank.Items(i).Value = BankCode
                ddlSelectBank.Items(i).Text = GV.parseString(BankName)
            Next

            If ddlSelectBank.Items.Count > 0 Then
                ddlSelectBank.Items.Insert(0, ":::: Select Bank ::::")
            Else
                ddlSelectBank.Items.Add(":::: Select Bank ::::")
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnAddRecepientGo_Click(sender As Object, e As EventArgs) Handles btnAddRecepientGo.Click
        Dim VerifyMessage1 As String = ""
        Try
            lblRecepientError.Text = ""
            lblRecepientError.CssClass = ""


            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If

            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VerifyServiceCharge) Then
            Else
                lblRecepientError.Text = "You Have Insufficient Wallet Amount, Service Charge Applicable Rs " & VerifyServiceCharge
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If

            If ddlSelectBank.SelectedIndex = 0 Then
                lblRecepientError.Text = "Please Select Bank."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtRecepientMobileNo.Text = "" Then
                lblRecepientError.Text = "Please Enter Beneficiary Name."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtIFSCCode.Text = "" Then
                lblRecepientError.Text = "Please Enter IFSC."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If
            If txtBankAccountNo.Text = "" Then
                lblRecepientError.Text = "Please Enter Bank AccountNo."
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If

            APIResult = GetApiResult_NEW("RegisterBeneficiary_API_Parameters")

            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim name As String = ""
            Dim CustomerID As String = ""
            Dim MobileNO As String = ""
            VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
            If VerifyMessage1 = "Receiver account successfully added." Then

                Dim data_ As List(Of JToken) = ser_.Children().ToList
                '// Loop through data
                For Each item As JProperty In data_
                    item.CreateReader()
                    Select Case item.Name
                        Case "response_code"

                            strBuild = strBuild & Environment.NewLine & " response_status_id : " & ser_.SelectToken("response_code").ToString

                        Case "data"
                            Dim data1 As List(Of JToken) = item.Children().ToList
                            For Each msg As JObject In data1
                                '/// Dynamic Name and get its value
                                For Each p In msg
                                    strBuild = strBuild & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString
                                    If p.Key.ToString.Trim.ToUpper = "bene_id".ToUpper Then
                                        lbl_Beneficiary_temp_id.text = p.Value.ToString.Trim
                                    End If
                                Next
                            Next

                        Case "response_type_id"
                            strBuild = strBuild & Environment.NewLine & " response_type_id : " & ser_.SelectToken("response_type_id").ToString
                        Case "message"
                            strBuild = strBuild & Environment.NewLine & " message : " & ser_.SelectToken("message").ToString
                        Case "status"
                            strBuild = strBuild & Environment.NewLine & " status : " & ser_.SelectToken("status").ToString
                    End Select
                Next

                'lblBeneID  bene_id


                APIResult = GetApiResult_NEW("PennyDrop_API_Parameters")
                json_ = APIResult
                ser_ = JObject.Parse(json_)
                VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
                Dim Status As String = ser_.SelectToken("status").ToString.Trim
                If VerifyMessage1.Trim = "Transaction Successful" Then

                    Fill_Recepient_List()
                    txtBankAccountNo.Text = ""
                    txtRecepientMobileNo.Text = ""
                    txtIFSCCode.Text = ""
                    'Div_TransferAmt.Visible = False
                    Div_AddRecepient.Visible = False


                    'Dim recipient_name As String = ser_.SelectToken("data").SelectToken("recipient_name").ToString.Trim
                    'Dim recipient_ifsc As String = ser_.SelectToken("data").SelectToken("ifsc").ToString.Trim
                    'Dim recipient_account As String = ser_.SelectToken("data").SelectToken("account").ToString.Trim
                    'Dim recipient_BankName As String = ser_.SelectToken("data").SelectToken("bank").ToString.Trim


                    'lblRecepientActualName.Text = recipient_name
                    'APIResult = GetApiResult("Add_New_Recipients_API_Parameters")
                    'json_ = APIResult
                    'ser_ = JObject.Parse(json_)

                    'VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
                    'Dim New_RecipientsStatus As String = ser_.SelectToken("status").ToString.Trim
                    'If New_RecipientsStatus.Trim = "0" Then




                    '    Fill_Recepient_List()
                    '    txtBankAccountNo.Text = ""
                    '    txtRecepientMobileNo.Text = ""
                    '    txtIFSCCode.Text = ""
                    '    'Div_TransferAmt.Visible = False
                    '    Div_AddRecepient.Visible = False
                    'Else
                    '    lblRecepientError.Text = VerifyMessage1
                    '    lblRecepientError.CssClass = "errorlabels"
                    '    Exit Sub
                    'End If


                Else

                    Fill_Recepient_List()
                    txtBankAccountNo.Text = ""
                    txtRecepientMobileNo.Text = ""
                    txtIFSCCode.Text = ""
                    'Div_TransferAmt.Visible = False
                    Div_AddRecepient.Visible = False


                    'lblRecepientError.Text = VerifyMessage1
                    ' lblRecepientError.CssClass = "errorlabels"
                    Exit Sub
                End If

                ''//// Service Charge Deduction
                Dim VTransId As String = GV.FL.getAutoNumber("TransId")
                Dim Ret_Id As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                Dim SDtypecommFrom1 As String = "Your Account is debited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Account Verify on RegID " & Ret_Id & "."
                Dim SDtypecommTo1 As String = "Your Account is credited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Account Verify on RegID " & Ret_Id & " ."
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VTransId & "','" & VerifyServiceCharge & "','" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Service Charge','Service Charge',getdate(),'" & Ret_Id & "','ADMIN','" & VerifyServiceCharge & "',getdate(),'" & Ret_Id & "',getdate() ) ;"
                GV.FL.DMLQueriesBulk(QryStr)
                ''//// End Service Charge Deduction


                ''//// Service Charge Deduction - Admin To Superadmin
                VerifyServiceCharge = VerifyServiceCharge
                VTransId = GV.FL.getAutoNumber("TransId")
                Ret_Id = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                SDtypecommFrom1 = "Your Account is debited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Account Verify on RegID " & Ret_Id & "."
                SDtypecommTo1 = "Your Account is credited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Account Verify on RegID " & Ret_Id & " ."
                QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VTransId & "','" & VerifyServiceCharge & "','" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Service Charge','Service Charge',getdate(),'ADMIN','Super Admin','" & VerifyServiceCharge & "',getdate(),'" & Ret_Id & "',getdate() ) ;"
                GV.FL.DMLQueriesBulk(QryStr)
                ''//// End Service Charge Deduction


                '/////////////////////////////
            Else
                Fill_Recepient_List()
                txtBankAccountNo.Text = ""
                txtRecepientMobileNo.Text = ""
                txtIFSCCode.Text = ""
                'Div_TransferAmt.Visible = False
                Div_AddRecepient.Visible = False

                lblRecepientError.Text = VerifyMessage1
                lblRecepientError.CssClass = "errorlabels"
                Exit Sub
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblRecepientError.Text = VerifyMessage1
            lblRecepientError.CssClass = "errorlabels"
        End Try
    End Sub

    Protected Sub btnGrdRowTransfer_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblRBankName.Text = ""
            lblIFSCCode.Text = ""
            lblReceipentName.Text = ""
            lblReceipentId.Text = ""
            lblReceipentMobileNo.Text = ""
            lblTranferAmtError.Text = ""
            lblTranferAmtError.CssClass = ""
            txtServiceCharge.Text = ""
            txtNetAmount.Text = ""
            txtEnterAmt.Text = ""
            lblService.Text = ""
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex

            For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                grdAddRecepient.Rows(i).BackColor = Color.White
            Next

            grdAddRecepient.Rows(lblRowIndex.Text).BackColor = Color.LightGreen
            grdAddRecepient.Rows(lblRowIndex.Text).Cells(0).BackColor = Color.White

            lblRBankName.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(3).Text)
            lblIFSCCode.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(4).Text)
            lblRAccountNo.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(5).Text)
            lblReceipentName.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(6).Text)
            lblReceipentId.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(7).Text)
            lblReceipentMobileNo.Text = GV.parseString(txtEnterMobileNo.Text.Trim)
            Div_AddRecepient.Visible = False
            Div_TransferAmt.Visible = True

            btnTranferAmt.Focus()


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnGrdRowDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex

            lblReceipentId.Text = GV.parseString(grdAddRecepient.Rows(lblRowIndex.Text).Cells(7).Text)
            APIResult = GetApiResult_NEW("DeleteBeneficiary_API_Parameters")



            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            Dim Meassge As String = ser_.SelectToken("message").ToString
            If Meassge = "Beneficiary record deleted successfully." Then
                Fill_Recepient_List()
            End If



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnTranferAmt_Click(sender As Object, e As EventArgs) Handles btnTranferAmt.Click
        Dim VerifyMessage1 As String = ""
        Try
            lblTranferAmtError.Text = ""
            lblTranferAmtError.CssClass = ""

            '///// Start Check API  STATUS Super ADmin Level

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Super ADmin Level

            '///// Start Check API  STATUS System Settings 

            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblTranferAmtError.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblTranferAmtError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 


            If txtEnterAmt.Text.Trim = "" Then
                lblTranferAmtError.Text = "Please Enter Amount."
                lblTranferAmtError.CssClass = "errorlabels"
                'txtEnterAmt.Focus()
                Exit Sub
            End If

            If CInt(txtEnterAmt.Text.Trim) <= 99 Then
                lblTranferAmtError.Text = "Enter Amount Should be Greater Than 99."
                lblTranferAmtError.CssClass = "errorlabels"
                'txtEnterAmt.Focus()
                Exit Sub
            End If

            Dim VNetAmount As Decimal = 0
            If txtNetAmount.Text.Trim = "" Then
                VNetAmount = 0
            Else
                VNetAmount = GV.parseString(txtNetAmount.Text.Trim)
            End If



            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If

            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
            Else
                lblTranferAmtError.Text = "You Have Insufficient Wallet Amount"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// Check For API Balance - Start //////
            If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                lblTranferAmtError.Text = "Insufficient API Balance."
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// Check For API Balance - End //////

            If GV.parseString(lblBank_Limit_1.Text) = "" Then
                lblBank_Limit_1.Text = "0"
            End If
            If GV.parseString(lblBank_Limit_2.Text) = "" Then
                lblBank_Limit_2.Text = "0"
            End If
            If GV.parseString(lblBank_Limit_3.Text) = "" Then
                lblBank_Limit_3.Text = "0"
            End If


            If CInt(txtEnterAmt.Text.Trim) > (CInt(lblBank_Limit_1.Text) + CInt(lblBank_Limit_2.Text) + CInt(lblBank_Limit_3.Text)) Then
                lblTranferAmtError.Text = "Transfer Amount Exceeds Limit."
                lblTranferAmtError.CssClass = "errorlabels"
                'txtEnterAmt.Focus()
                Exit Sub
            End If



            If txtTransactionPin.Text = "" Then
                lblTranferAmtError.Text = "Please Enter Your Transaction Pin."
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim TransPiNo As String = ""
            TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If TransPiNo.Trim = txtTransactionPin.Text.Trim Then
            Else
                lblTranferAmtError.Text = "Invalid Transaction Pin"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If


            btnok_Transfer_1.Text = "Yes"
            btnok.Visible = False
            btnok_Transfer_1.Visible = True

            btnCancel.Text = "No"

            'AccountNo = lblRAccountNo.Text.Trim
            'Receipent = lblReceipentName.Text


            lblDialogMsg.Text = "Name : " & lblReceipentName.Text & " <br> Account No : " & lblRAccountNo.Text.Trim & " <br> Are You sure you want to Proceed ??"
            lblDialogMsg.CssClass = ""
            ModalPopupExtender1.Show()

            Exit Sub

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            If VerifyMessage1.Trim = "" Then
                lblTranferAmtError.Text = ""
                lblTranferAmtError.CssClass = ""
            Else
                lblTranferAmtError.Text = VerifyMessage1
                lblTranferAmtError.CssClass = "errorlabels"
            End If
            lblTranferAmtError.CssClass = ""
            lblTranferAmtError.Text = APIResult
        End Try
    End Sub

    Private Sub btnok_Transfer_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok_Transfer_1.Click
        Try

            Try
                lblTranferAmtError_2.Text = ""
                lblTranferAmtError_2.CssClass = ""

                If GV.parseString(txtEnterAmt.Text) = "" Then
                    txtEnterAmt.Text = "0"
                End If


                Dim TotalAmt As Integer = txtEnterAmt.Text.Trim


                If GV.parseString(lblBank_Limit_1.Text) = "" Then
                    lblBank_Limit_1.Text = "0"
                End If
                If GV.parseString(lblBank_Limit_2.Text) = "" Then
                    lblBank_Limit_2.Text = "0"
                End If
                If GV.parseString(lblBank_Limit_3.Text) = "" Then
                    lblBank_Limit_3.Text = "0"
                End If


                If TotalAmt < 5000 Then
                    '// If amount is less than 5000

                    If TotalAmt <= (CInt(lblBank_Limit_1.Text)) And (CInt(lblBank_Limit_1.Text) >= 100) Then
                        'Only BAnk1
                        lblBankID.Text = "bank1"
                        lblCaculatedAmt.Text = txtEnterAmt.Text.Trim
                        If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                            transferAmt()

                            lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                            lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                            lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                            lblPopTransactionID.Text = Session("TransID") 'TranscationId
                            lblPopTransferAmt.Text = GV.parseString(txtEnterAmt.Text.Trim)
                            lblPopStatus.Text = Session("Msg") 'VerifyMessage1
                            lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                            lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                            lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                            lblpopBankName.Text = lblRBankName.Text.Trim
                            ModalPopupExtender3.Show()

                        End If

                    ElseIf TotalAmt <= (CInt(lblBank_Limit_2.Text)) And (CInt(lblBank_Limit_2.Text) >= 100) Then
                        'Only BAnk2
                        lblBankID.Text = "bank2"

                        lblCaculatedAmt.Text = txtEnterAmt.Text.Trim
                        If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                            transferAmt()
                            lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                            lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                            lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                            lblPopTransactionID.Text = Session("TransID") 'TranscationId
                            lblPopTransferAmt.Text = GV.parseString(txtEnterAmt.Text.Trim)
                            lblPopStatus.Text = Session("Msg") 'VerifyMessage1
                            lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                            lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                            lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                            lblpopBankName.Text = lblRBankName.Text.Trim
                            ModalPopupExtender3.Show()
                        End If



                    ElseIf TotalAmt <= (CInt(lblBank_Limit_3.Text)) And (CInt(lblBank_Limit_3.Text) >= 100) Then
                        'Only BAnk3
                        lblBankID.Text = "bank3"

                        lblCaculatedAmt.Text = txtEnterAmt.Text.Trim

                        If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                            transferAmt()

                            lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                            lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                            lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                            lblPopTransactionID.Text = Session("TransID") 'TranscationId
                            lblPopTransferAmt.Text = GV.parseString(txtEnterAmt.Text.Trim)
                            lblPopStatus.Text = Session("Msg") 'VerifyMessage1
                            lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                            lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                            lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                            lblpopBankName.Text = lblRBankName.Text.Trim
                            ModalPopupExtender3.Show()
                        End If

                    End If


                ElseIf TotalAmt <= CInt(lblBank_Limit_1.Text) Then
                    'Only BAnk1
                    lblBankID.Text = "bank1"
                    If TotalAmt > 5000 Then

                        Dim pro As Decimal = Math.Ceiling(CDec(TotalAmt / 5000))
                        Dim totalNo As Integer = Math.Ceiling(pro)

                        For i As Integer = 1 To totalNo

                            If i = totalNo Then
                                lblCaculatedAmt.Text = ((txtEnterAmt.Text.Trim) - ((i - 1) * 5000))
                            Else
                                lblCaculatedAmt.Text = 5000
                            End If

                            If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                transferAmt()
                            End If
                        Next
                    Else
                        lblCaculatedAmt.Text = txtEnterAmt.Text.Trim
                        If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                            transferAmt()
                        End If

                    End If


                    lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                    lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                    lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                    lblPopTransactionID.Text = Session("TransID") 'TranscationId
                    lblPopTransferAmt.Text = GV.parseString(txtEnterAmt.Text.Trim)
                    lblPopStatus.Text = Session("Msg") 'VerifyMessage1
                    lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                    lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                    lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                    lblpopBankName.Text = lblRBankName.Text.Trim
                    ModalPopupExtender3.Show()


                ElseIf (TotalAmt > CInt(lblBank_Limit_1.Text)) And (TotalAmt <= (CInt(lblBank_Limit_1.Text) + CInt(lblBank_Limit_2.Text))) Then
                    ' Bank1 and Bank2
                    For j As Integer = 1 To 2
                        If j = 1 Then
                            lblBankID.Text = "bank1"

                            If CInt(lblBank_Limit_1.Text) > 5000 Then

                                Dim pro As Decimal = Math.Ceiling(CDec(lblBank_Limit_1.Text.Trim / 5000))
                                Dim totalNo As Integer = Math.Ceiling(pro)

                                For i As Integer = 1 To totalNo

                                    If i = totalNo Then
                                        lblCaculatedAmt.Text = ((lblBank_Limit_1.Text.Trim) - ((i - 1) * 5000))
                                    Else
                                        lblCaculatedAmt.Text = 5000
                                    End If
                                    If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                        transferAmt()
                                    End If
                                Next
                            Else
                                lblCaculatedAmt.Text = lblBank_Limit_1.Text.Trim
                                If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                    transferAmt()
                                End If
                            End If
                        ElseIf j = 2 Then
                            lblBankID.Text = "bank2"

                            Dim RemAmt As Integer = (TotalAmt - CInt(lblBank_Limit_1.Text))
                            If RemAmt > 5000 Then

                                Dim pro As Decimal = Math.Ceiling(CDec(RemAmt / 5000))
                                Dim totalNo As Integer = Math.Ceiling(pro)
                                For i As Integer = 1 To totalNo

                                    If i = totalNo Then
                                        lblCaculatedAmt.Text = ((RemAmt) - ((i - 1) * 5000))
                                    Else
                                        lblCaculatedAmt.Text = 5000
                                    End If
                                    If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                        transferAmt()
                                    End If
                                Next

                            Else
                                lblCaculatedAmt.Text = RemAmt
                                If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                    transferAmt()
                                End If
                            End If

                        End If
                    Next



                    lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                    lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                    lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                    lblPopTransactionID.Text = Session("TransID") 'TranscationId
                    lblPopTransferAmt.Text = GV.parseString(txtEnterAmt.Text.Trim)
                    lblPopStatus.Text = Session("Msg") 'VerifyMessage1
                    lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                    lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                    lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                    lblpopBankName.Text = lblRBankName.Text.Trim
                    ModalPopupExtender3.Show()
                ElseIf (TotalAmt > (CInt(lblBank_Limit_1.Text) + CInt(lblBank_Limit_2.Text))) And (TotalAmt <= (CInt(lblBank_Limit_1.Text) + CInt(lblBank_Limit_2.Text) + CInt(lblBank_Limit_3.Text))) Then
                    ' Bank1 and Bank2 and Bank3
                    For j As Integer = 1 To 3
                        If j = 1 Then
                            lblBankID.Text = "bank1"
                            If CInt(lblBank_Limit_1.Text) > 5000 Then

                                Dim pro As Decimal = Math.Ceiling(CDec(lblBank_Limit_1.Text.Trim / 5000))
                                Dim totalNo As Integer = Math.Ceiling(pro)

                                For i As Integer = 1 To totalNo

                                    If i = totalNo Then
                                        lblCaculatedAmt.Text = ((lblBank_Limit_1.Text.Trim) - ((i - 1) * 5000))
                                    Else
                                        lblCaculatedAmt.Text = 5000
                                    End If
                                    If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                        transferAmt()
                                    End If
                                Next
                            Else
                                lblCaculatedAmt.Text = lblBank_Limit_1.Text.Trim
                                If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                    transferAmt()
                                End If
                            End If
                        ElseIf j = 2 Then
                            lblBankID.Text = "bank2"
                            For i As Integer = 1 To 4
                                lblCaculatedAmt.Text = 5000
                                If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                    transferAmt()
                                End If
                            Next
                        Else
                            lblBankID.Text = "bank3"
                            Dim RemAmt As Integer = (TotalAmt - (CInt(lblBank_Limit_1.Text) + CInt(lblBank_Limit_2.Text)))
                            If RemAmt > 5000 Then

                                Dim pro As Decimal = Math.Ceiling(CDec(RemAmt / 5000))
                                Dim totalNo As Integer = Math.Ceiling(pro)
                                For i As Integer = 1 To totalNo
                                    If i = totalNo Then
                                        lblCaculatedAmt.Text = ((RemAmt) - ((i - 1) * 5000))
                                    Else
                                        lblCaculatedAmt.Text = 5000
                                    End If
                                    If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                        transferAmt()
                                    End If
                                Next
                            Else
                                lblCaculatedAmt.Text = RemAmt
                                If CInt(lblCaculatedAmt.Text.Trim) >= 100 Then
                                    transferAmt()
                                End If
                            End If
                        End If
                    Next

                    'Dim ServiceCharge As Decimal = 0
                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                    '    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                    '    ' ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                    '    'Else
                    '    '    ServiceCharge = 10
                    '    'End If
                    '    If ServiceCharge > 0 Then
                    '        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    '        Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to Money Transfer / AMT " & lblCaculatedAmt.Text.Trim & "."
                    '        Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to Money Transfer / AMT " & lblCaculatedAmt.Text.Trim & "."
                    '        Dim Qry As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & GV.FL.getAutoNumber("TransID") & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge.Text.Trim) & "',getdate(),'Admin',getdate() ) ;"
                    '        GV.FL.DMLQueriesBulk(Qry)
                    '    End If

                    'End If


                    lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                    lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                    lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                    lblPopTransactionID.Text = Session("TransID") 'TranscationId
                    lblPopTransferAmt.Text = GV.parseString(txtEnterAmt.Text.Trim)
                    lblPopStatus.Text = Session("Msg") 'VerifyMessage1
                    lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                    lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                    lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                    lblpopBankName.Text = lblRBankName.Text.Trim
                    ModalPopupExtender3.Show()

                Else
                    'Exceeds Limit
                End If



                'If TotalAmt > 5000 Then
                '    Dim pro As Decimal = Math.Ceiling(CDec(TotalAmt / 5000))
                '    Dim totalNo As Integer = Math.Ceiling(pro)


                '    Dim AmtToTransfer As Integer = 0
                '    For i As Integer = 1 To totalNo

                '        If i = totalNo Then
                '            lblCaculatedAmt.Text = ((txtEnterAmt.Text.Trim) - ((i - 1) * 5000))
                '        Else
                '            lblCaculatedAmt.Text = 5000
                '            lblBankID.Text = "bank1"
                '        End If
                '        transferAmt()

                '    Next
                'Else
                '    lblCaculatedAmt.Text = txtEnterAmt.Text.Trim
                '    transferAmt()
                'End If

            Catch ex As Exception
                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
                lblTranferAmtError_2.CssClass = ""
                lblTranferAmtError_2.Text = APIResult
            End Try

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Public Sub transferAmt()
        Try
            Dim VerifyMessage1 As String = ""

            Dim TranscationId, OrderNO As String
            TranscationId = ""
            OrderNO = ""
            APIResult = GetApiResult_NEW("Transaction_API_Parameters")
            '/// parse and read data in list format through json parse
            Dim json_ As String = APIResult
            Dim ser_ As JObject = JObject.Parse(json_)
            VerifyMessage1 = ser_.SelectToken("message").ToString.Trim
            Session("Msg") = VerifyMessage1
            Dim Status As String = ser_.SelectToken("status").ToString.Trim
            Dim txn_status As String = ser_.SelectToken("txn_status").ToString.Trim
            If txn_status.Trim = "1" Then
                Status = "Success"
            ElseIf txn_status.Trim = "0" Or txn_status.Trim = "5" Then
                Status = "Failed"
            ElseIf txn_status.Trim = "2" Or txn_status.Trim = "3" Or txn_status.Trim = "4" Then
                Status = "Pending"
            Else
                Status = "Failed"
            End If

            OrderNO = ser_.SelectToken("utr").ToString.Trim
            TranscationId = ser_.SelectToken("ackno").ToString.Trim

            Session("TransID") = TranscationId

            Dim data_ As List(Of JToken) = ser_.Children().ToList
            For Each item As JProperty In data_
                item.CreateReader()
                Select Case item.Name
                    Case "data"
                        Dim data1 As List(Of JToken) = item.Children().ToList
                        For Each msg As JObject In data1
                            '/// Fix Attribute Name and get its value
                            TranscationId = msg("tid")
                            OrderNO = msg("client_ref_id")
                        Next
                End Select
            Next

            'ackno
            Dim IFSC, AccountNo, Receipent, ReceipentId As String

            IFSC = lblIFSCCode.Text.Trim
            AccountNo = lblRAccountNo.Text.Trim
            Receipent = lblReceipentName.Text
            ReceipentId = lblReceipentId.Text.Trim


            Dim VUpdatedBy, VUpdatedOn, VRecord_DateTime As String
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"
            VRecord_DateTime = "getdate()"
            Dim Qry As String = "Insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MoneyTransfer_API (txt_status,Refund_Status,TransIpAddress,IFSC,AccountNo,Receipent,ReceipentId,TransId,APIStatus,APIMessage,TransferDate,OrderNo,RefrenceNo,TranscationId,CustomerID,MobileNo,Amount,BankName,Method,Process,RecordDateTime,UpdatedBy,UpdatedOn) values (  '" & txn_status.Trim & "',  'No','" & GV.parseString(GV.GetIPAddress) & "','" & IFSC & "','" & AccountNo & "','" & Receipent & "','" & ReceipentId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & Status & "','" & VerifyMessage1 & " txn_status : " & txn_status.Trim & "','" & Now.Date & "','" & OrderNO & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & TranscationId & "','" & GV.parseString(txtEnterMobileNo.Text.Trim) & "','" & GV.parseString(lblReceipentMobileNo.Text.Trim) & "'," & GV.parseString(lblCaculatedAmt.Text.Trim) & ",'" & GV.parseString(lblRBankName.Text.Trim) & "','" & GV.parseString(ddlTransferMode.SelectedItem.Text.Trim) & "','Money Transfer',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate() )"
            Dim Result As Boolean = False
            Result = GV.FL.DMLQueries(Qry)
            If Result = True Then
                'lblTranferAmtError.Text = VerifyMessage1
                If Status.Trim.ToUpper = "Success".Trim.ToUpper Then
                    If CDec(lblCaculatedAmt.Text.Trim) > 0 Then 'previous 2500
                        Dim TypeName As String = "Money Transfer"
                        Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                        If GRP = "Retailer".ToUpper Then

                            RechargeCommision()
                            If Not lblRID.Text = "" Then
                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim DisID_Com() As String = AAID(1).Split(":")
                                Dim SubDIsID_Com() As String = AAID(2).Split(":")
                                Dim RetailerID_Com() As String = AAID(3).Split(":")

                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)
                                Dim DisID As String = DisID_Com(0)
                                Dim DisCom As String = DisID_Com(1)
                                Dim SUBDisID As String = SubDIsID_Com(0)
                                Dim SUBDisCom As String = SubDIsID_Com(1)
                                Dim RTEID As String = RetailerID_Com(0)
                                Dim RTECom As String = RetailerID_Com(1)

                                Dim arrCanChange() As String = AAID(4).Split(":")
                                Dim vCanChange As String = arrCanChange(1)


                                If vCanChange.Trim.ToUpper = "YES" Then

                                    Dim typeAmtForm As String = "Your Account is debited by " & lblCaculatedAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & lblCaculatedAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."


                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."

                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."


                                    Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & lblCaculatedAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END


                                Else
                                    'vCanChange.Trim.ToUpper = "NO"

                                    Dim typeAmtForm As String = "Your Account is debited by " & lblCaculatedAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & lblCaculatedAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."


                                    Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & lblCaculatedAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If DisCom > 0 Then
                                        V_Actual_Commission_Amt = DisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        DisCom = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Distributor Commission Calculation - End

                                    '//// SUB Distributor Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If SUBDisCom > 0 Then
                                        V_Actual_Commission_Amt = SUBDisCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        SUBDisCom = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// SUB Distributor Commission Calculation - End

                                    '//// Retailer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If RTECom > 0 Then
                                        V_Actual_Commission_Amt = RTECom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        RTECom = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END
                                End If
                            End If


                            Dim admin_Cm_ServcCharge As String = adminCommisionCalMoneyTransfer()
                            If Not admin_Cm_ServcCharge.Trim = "" Then
                                If admin_Cm_ServcCharge.Contains(":") Then
                                    Dim rawSplit() As String = admin_Cm_ServcCharge.Split(":")
                                    Dim Admin_Com_Amt As String = rawSplit(0)
                                    Dim service_Charge_Amt As String = rawSplit(1)

                                    If Not IsNumeric(service_Charge_Amt.Trim) Then
                                        service_Charge_Amt = 0
                                    End If
                                    If service_Charge_Amt > 0 And service_Charge_Amt < 10 Then
                                        service_Charge_Amt = 10
                                    End If

                                    If service_Charge_Amt > 0 Then
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    If Not IsNumeric(Admin_Com_Amt.Trim) Then
                                        Admin_Com_Amt = 0
                                    End If
                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If Admin_Com_Amt > 0 Then
                                        V_Actual_Commission_Amt = Admin_Com_Amt
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        Admin_Com_Amt = V_Net_Commission_Amt

                                        Dim Admintypecommfrom As String = "Your Account is debited by commission " & V_Actual_Commission_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                        Dim AdmintypecommTo As String = "Your Account is credited by commission " & V_Actual_Commission_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','Super Admin','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If


                                End If
                            End If

                            'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                            'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                            'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                            Dim NetAmount As Decimal = 0
                            Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer'").Split(":")

                            If CDec(lblCaculatedAmt.Text.Trim) > 1000 Then

                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        'lblService.Text = Service(0) & " %"
                                        NetAmount = (CDec(lblCaculatedAmt.Text.Trim) * CDec(Service(0))) / 100
                                        'If NetAmount < 25 Then
                                        '    NetAmount = 25
                                        'End If
                                    ElseIf Service(1).Trim = "Amount" Then
                                        'lblService.Text = Service(0)
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        'lblService.Text = Service(0)
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                            Else
                                NetAmount = 10
                            End If

                            If NetAmount > 0 Then
                                Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount.ToString & " Rs. Due to Money Transfer / AMT " & lblCaculatedAmt.Text.Trim & "."
                                Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount.ToString & " Rs. Due to Money Transfer / AMT " & lblCaculatedAmt.Text.Trim & "."
                                Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(NetAmount.ToString) & "',getdate(),'Admin',getdate() ) ;"
                            End If


                            Dim Transaction_Charge_Amt As Decimal = 2.5
                            Dim VFrom_Trans As String = "Your Account is debited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                            Dim VTo_Trans As String = "Your Account is credited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo_Trans & "','" & VFrom_Trans & "','Transaction Charge','Transaction Charge','" & Now.Date & "','Admin','Super Admin','" & Transaction_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                            GV.FL.DMLQueriesBulk(Qry)

                            'Dim Service_Charge_Amt_1 As Decimal = 2.5
                            'Dim VFrom_Service_Charge_1 As String = "Your Account is debited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                            'Dim VTo_Service_Charge_1 As String = "Your Account is credited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                            'Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo_Trans & "','" & VFrom_Trans & "','Transaction Charge','Transaction Charge','" & Now.Date & "','Admin','Super Admin','" & Transaction_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                            'GV.FL.DMLQueriesBulk(Qry)

                            lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                            lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                            lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                            lblPopTransactionID.Text = TranscationId
                            lblPopTransferAmt.Text = GV.parseString(lblCaculatedAmt.Text.Trim)
                            lblPopStatus.Text = VerifyMessage1
                            lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                            lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                            lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                            lblpopBankName.Text = lblRBankName.Text.Trim
                            ModalPopupExtender3.Show()
                            Exit Sub
                        ElseIf GRP = "Customer".ToUpper Then
                            '// #EkYes
                            'In case of Customer 
                            RechargeCommision_Customer()
                            If Not lblRID.Text = "" Then

                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")

                                Dim CustID_Com() As String = AAID(1).Split(":")

                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim CustID As String = CustID_Com(0)
                                Dim CustCom As String = CustID_Com(1)


                                Dim typeAmtForm As String = "Your Account is debited by " & lblCaculatedAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & lblCaculatedAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                Dim Custtypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."

                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                Dim CusttypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & lblCaculatedAmt.Text.Trim & "."


                                Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & lblCaculatedAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                '//// Distributor Commission Calculation - Start


                                '//// customer Commission Calculation - Start
                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If CustCom > 0 Then
                                    V_Actual_Commission_Amt = CustCom
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    CustCom = V_Net_Commission_Amt
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CusttypecommTo & "','" & Custtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                '//// customer Commission Calculation - END
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer'").Split(":")

                                If CDec(lblCaculatedAmt.Text.Trim) > 1000 Then
                                    If Service.Length > 1 Then
                                        If Service(1).Trim = "Percentage" Then
                                            'lblService.Text = Service(0) & " %"
                                            NetAmount = (CDec(lblCaculatedAmt.Text.Trim) * CDec(Service(0))) / 100
                                            'If NetAmount < 25 Then
                                            '    NetAmount = 25
                                            'End If
                                        ElseIf Service(1).Trim = "Amount" Then
                                            'lblService.Text = Service(0)
                                            NetAmount = CDec(Service(0))
                                        ElseIf Service(1).Trim = "Not Applicable" Then
                                            'lblService.Text = Service(0)
                                            NetAmount = CDec(Service(0))
                                        End If
                                    End If
                                Else
                                    NetAmount = 10
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount.ToString & " Rs. Due to Money Transfer / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount.ToString & " Rs. Due to Money Transfer / AMT " & lblCaculatedAmt.Text.Trim & "."
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(NetAmount.ToString) & "',getdate(),'Admin',getdate() ) ;"
                                End If

                                'Dim ServiceCharge As Decimal = 0
                                'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                                '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                '    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                '    ' ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                '    'Else
                                '    '    ServiceCharge = 10
                                '    'End If
                                '    If ServiceCharge > 0 Then
                                '        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                '        Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                '        Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & lblCaculatedAmt.Text.Trim & "."
                                '        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & lblCaculatedAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                '    End If

                                'End If



                            End If

                        End If

                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        '//// Admin & Super Admin Commission Calculation - Start
                        If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                            '//// Admin Commission Calculation - Start
                            Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Comm_Result As String
                            Dim VCus_Amount, V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                            If GV.parseString(lblCaculatedAmt.Text.Trim) = "" Then
                                V_Amount = "0"
                            Else
                                V_Amount = lblCaculatedAmt.Text.Trim
                            End If
                            VCus_Amount = V_Amount

                            V_OperatorCategory = ""
                            V_OperatorCode = ""
                            V_APIName = "Money Transfer"
                            V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                            Comm_Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)

                            Dim Transaction_Charge_Amt As Decimal = 2.5
                            Dim VFrom_Trans As String = "Your Account is debited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                            Dim VTo_Trans As String = "Your Account is credited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo_Trans & "','" & VFrom_Trans & "','Transaction Charge','Transaction Charge','" & Now.Date & "','Admin','Super Admin','" & Transaction_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"



                            If Not GV.parseString(Comm_Result) = "" Then
                                Dim Result_Arry() As String = Comm_Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)

                                If Not IsNumeric(Service_Charge_Amt.Trim) Then
                                    Service_Charge_Amt = 0
                                End If
                                If Service_Charge_Amt > 0 And Service_Charge_Amt < 10 Then
                                    Service_Charge_Amt = 10
                                End If

                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If



                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            '//// Admin Commission Calculation - End



                            '//// Super Admin Commission Calculation - Start
                            Comm_Result = GV.Commision_Calculation_For_SuperAdmin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName)

                            If Not GV.parseString(Comm_Result) = "" Then
                                Dim Result_Arry() As String = Comm_Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Super Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0

                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            '//// Super Admin Commission Calculation - End
                        End If
                        '//// Admin & Super Admin Commission Calculation - End
                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

                        If Not Qry.Trim = "" Then
                            GV.FL.DMLQueriesBulk(Qry)
                        End If

                    Else
                        ''Only Service Charge
                        ''Commented due to condition not require 


                        'Dim TypeName As String = "Money Transfer"

                        'Dim ServiceCharge As Decimal = 0
                        'Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)


                        'Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                        'Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                        'Qry = "insert into BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTE & "','SuperAdmin','" & txtEnterAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"


                        'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                        '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                        '    If ServiceCharge > 0 Then

                        '        Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName
                        '        Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName
                        '        Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                        '    End If
                        'End If
                        'GV.FL.DMLQueriesBulk(Qry)
                    End If

                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub bntReceipientClose_Click(sender As Object, e As EventArgs) Handles bntReceipientClose.Click
        Try
            txtBankAccountNo.Text = ""
            txtRecepientMobileNo.Text = ""
            txtIFSCCode.Text = ""
            lblRecepientError.Text = ""
            lblRecepientError.CssClass = ""
            Div_AddRecepient.Visible = False
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btntransferClose_Click(sender As Object, e As EventArgs) Handles btntransferClose.Click
        Try
            txtEnterAmt.Text = ""
            lblTranferAmtError.Text = ""
            lblTranferAmtError.CssClass = ""
            lblRBankName.Text = ""
            lblIFSCCode.Text = ""
            lblReceipentName.Text = ""
            lblReceipentId.Text = ""
            lblReceipentMobileNo.Text = ""
            Div_TransferAmt.Visible = False
            For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
                grdAddRecepient.Rows(i).BackColor = Color.White
            Next





        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btntransferClose_2_Click(sender As Object, e As EventArgs) Handles btntransferClose_2.Click
        Try
            txtEnterAmt_2.Text = ""
            lblTranferAmtError_2.Text = ""
            lblTranferAmtError_2.CssClass = ""
            lblRBankName_2.Text = ""
            lblIFSCCode_2.Text = ""
            lblReceipentName_2.Text = ""
            lblReceipentId_2.Text = ""
            lblReceipentMobileNo_2.Text = ""
            Div_TransferAmt_2.Visible = False
            For i As Integer = 0 To grdAddRecepient_2.Rows.Count - 1
                grdAddRecepient_2.Rows(i).BackColor = Color.White
            Next

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub RechargeCommision()
        Try

            Dim VCommissionType, VSub_Dis_CommissionType, VRetailer_CommissionType As String
            VCommissionType = ""
            VSub_Dis_CommissionType = ""
            VRetailer_CommissionType = ""
            Dim VCommission, VSub_Dis_Commission, VRetailer_Commission As Decimal
            VCommission = 0
            VSub_Dis_Commission = 0
            VRetailer_Commission = 0


            Dim VContainCategory, VCanChange, VSlabApplicable As String
            VContainCategory = ""
            VCanChange = ""
            VSlabApplicable = ""



            Dim VadminComAmt, DistributorComAmt, SubDIsComAmt, VRetailerComAmt As Decimal
            VadminComAmt = 0
            DistributorComAmt = 0
            SubDIsComAmt = 0
            VRetailerComAmt = 0



            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalDISAmt, VFinalSUBDISAmt, VFinalRETAILERAmt As Decimal
            VFinaladminAmt = 0
            VFinalDISAmt = 0
            VFinalSUBDISAmt = 0
            VFinalRETAILERAmt = 0

            Dim SubDisID As String = ""
            Dim DisID As String = ""
            Dim AdminID As String = ""
            Dim qry As String = ""

            SubDisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "'")
            DisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "'")
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer' and ActiveStatus='Active'"
            ds = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = GV.parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If


                        If VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then

                            '/// Start With Slab

                            Dim Amount1 As String = GV.parseString(lblCaculatedAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Money Transfer'; "

                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dis_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Dis_CommissionType").ToString() = "" Then
                                                VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Dis_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dis_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Dis_Commission").ToString() = "" Then
                                                VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Dis_Commission").ToString())
                                            End If
                                        End If

                                        If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                        ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                            DistributorComAmt = (VCommission)
                                        End If


                                        '/////// End Distributor


                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                                VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                                VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                            End If
                                        End If

                                        If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                        ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SubDIsComAmt = (VSub_Dis_Commission)
                                        End If

                                        '/////// End  Sub Distributor


                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                                VRetailer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                                VRetailer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                            End If
                                        End If

                                        If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                        ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            VRetailerComAmt = (VRetailer_Commission)
                                        End If

                                        '/////// End  Retailer

                                        VFinaladminAmt = VadminComAmt
                                        VFinalDISAmt = DistributorComAmt
                                        VFinalSUBDISAmt = SubDIsComAmt
                                        VFinalRETAILERAmt = VRetailerComAmt


                                    End If
                                End If
                            End If

                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            '/// End With Slab

                        Else
                            '//// Start Without Slab

                            If VContainCategory.Trim.ToUpper = "YES" Then




                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then


                                Dim Amount1 As String = GV.parseString(lblCaculatedAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then

                                    DistributorComAmt = (VCommission)
                                End If

                                '/////// End Distributor



                                qry = " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Money Transfer' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                                qry = qry & " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Money Transfer' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




                                ds = New DataSet
                                ds = GV.FL.OpenDsWithSelectQuery(qry)
                                If Not ds Is Nothing Then
                                    If ds.Tables.Count > 0 Then
                                        If ds.Tables(0).Rows.Count > 0 Then


                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                                If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                                    VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                                If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                                    VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                                End If
                                            End If

                                            If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                            ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                SubDIsComAmt = (VSub_Dis_Commission)
                                            End If

                                            '/////// End  Sub Distributor
                                        End If
                                        '/////// End  Sub Distributor

                                        If ds.Tables.Count > 1 Then
                                            If ds.Tables(1).Rows.Count > 0 Then

                                                If Not IsDBNull(ds.Tables(1).Rows(0).Item("CommissionType")) Then
                                                    If Not ds.Tables(1).Rows(0).Item("CommissionType").ToString() = "" Then
                                                        VRetailer_CommissionType = GV.parseString(ds.Tables(1).Rows(0).Item("CommissionType").ToString())
                                                    End If
                                                End If

                                                If Not IsDBNull(ds.Tables(1).Rows(0).Item("Commission")) Then
                                                    If Not ds.Tables(1).Rows(0).Item("Commission").ToString() = "" Then
                                                        VRetailer_Commission = GV.parseString(ds.Tables(1).Rows(0).Item("Commission").ToString())
                                                    End If
                                                End If

                                                If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                    VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                                ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                    VRetailerComAmt = (VRetailer_Commission)
                                                End If

                                                '/////// End  Retailer

                                            End If
                                        End If

                                        '/////// End  Retailer

                                    End If
                                End If



                                VFinaladminAmt = VadminComAmt
                                VFinalDISAmt = DistributorComAmt
                                VFinalSUBDISAmt = SubDIsComAmt
                                VFinalRETAILERAmt = VRetailerComAmt
                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper



                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                                '/// NEED To CHANGE HERE EK

                                Dim Amount1 As String = GV.parseString(lblCaculatedAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    DistributorComAmt = (VCommission)
                                End If


                                '/////// End Distributor



                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                        VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                        VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                    End If
                                End If

                                If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    SubDIsComAmt = (VSub_Dis_Commission)
                                End If

                                '/////// End  Sub Distributor




                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                        VRetailer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                        VRetailer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                    End If
                                End If

                                If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VRetailerComAmt = (VRetailer_Commission)
                                End If

                                '/////// End  Retailer


                                VFinaladminAmt = VadminComAmt
                                VFinalDISAmt = DistributorComAmt
                                VFinalSUBDISAmt = SubDIsComAmt
                                VFinalRETAILERAmt = VRetailerComAmt
                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            End If

                            '/// End Without Slab
                        End If
                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub RechargeCommision_Customer()
        Try

            Dim VCommissionType, VCustomer_CommissionType As String
            VCommissionType = ""
            VCustomer_CommissionType = ""

            Dim VCommission, VCustomer_Commission As Decimal
            VCommission = 0
            VCustomer_Commission = 0

            Dim VContainCategory, VCanChange, VSlabApplicable As String
            VContainCategory = ""
            VCanChange = ""
            VSlabApplicable = ""



            Dim VadminComAmt, VCustomerComAmt As Decimal
            VadminComAmt = 0
            VCustomerComAmt = 0



            Dim CustomerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalCustomerAmt As Decimal
            VFinaladminAmt = 0
            VFinalCustomerAmt = 0

            Dim AdminID As String = ""

            Dim qry As String = ""

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer' and ActiveStatus='Active'"
            ds = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = GV.parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If


                        If VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then

                            '/// Start With Slab

                            Dim Amount1 As String = GV.parseString(lblCaculatedAmt.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Money Transfer'; "

                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                                VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                                VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                            End If
                                        End If

                                        If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                        ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                            VCustomerComAmt = (VCommission)
                                        End If


                                        '/////// End Distributor




                                        '/////// End  Retailer

                                        VFinaladminAmt = VadminComAmt
                                        VFinalCustomerAmt = VCustomerComAmt




                                    End If
                                End If
                            End If

                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            '/// End With Slab

                        Else
                            '//// Start Without Slab

                            If VContainCategory.Trim.ToUpper = "YES" Then




                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                                Dim Amount1 As String = GV.parseString(lblCaculatedAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VCustomerComAmt = (VCommission)
                                End If

                                '/////// End Distributor



                                VFinaladminAmt = VadminComAmt
                                VFinalCustomerAmt = VCustomerComAmt

                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper


                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                                '/// NEED To CHANGE HERE EK

                                Dim Amount1 As String = GV.parseString(lblCaculatedAmt.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VCustomerComAmt = (VCommission)
                                End If


                                '/////// End Distributor


                                VFinaladminAmt = VadminComAmt
                                VFinalCustomerAmt = VCustomerComAmt

                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper
                            End If

                            '/// End Without Slab
                        End If




                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Function adminCommisionCalMoneyTransfer() As String
        Try

            Dim VAdmin_CommissionType, VAdmin_ServiceChargeType As String

            VAdmin_CommissionType = ""
            VAdmin_ServiceChargeType = ""

            Dim VAdmin_Commission, VAdmin_ServiceCharge As Decimal
            VAdmin_Commission = 0
            VAdmin_ServiceCharge = 0


            Dim VContainCategory, VSlabApplicable As String
            VContainCategory = ""
            VSlabApplicable = ""



            Dim VAdmin_CommissionAmt, VAdmin_ServiceChargeAmt As Decimal
            VAdmin_CommissionAmt = 0
            VAdmin_ServiceChargeAmt = 0



            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim AdminID As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
            Dim VAdmin_CommissionAmt_Final, VAdmin_ServiceChargeAmt_Final As Decimal

            VAdmin_CommissionAmt_Final = 0
            VAdmin_ServiceChargeAmt_Final = 0



            Dim qry As String = ""

            Dim qryStr As String = "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where Title='Money Transfer' and ActiveStatus='Active' and AdminID='" & AdminID & "'"
            ds = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = GV.parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If

                        Dim Amount1 As String = GV.parseString(lblCaculatedAmt.Text.Trim)
                        If Amount1.Trim = "" Then
                            Amount1 = "0"
                        End If
                        Dim Amount As Decimal = Amount1

                        If VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then

                            '/// Start With Slab

                            '//// Start Admin Service Charge
                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceType")) Then
                                If Not ds.Tables(0).Rows(0).Item("ServiceType").ToString() = "" Then
                                    VAdmin_ServiceChargeType = GV.parseString(ds.Tables(0).Rows(0).Item("ServiceType").ToString())
                                End If
                            End If

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceCharge")) Then
                                If Not ds.Tables(0).Rows(0).Item("ServiceCharge").ToString() = "" Then
                                    VAdmin_ServiceCharge = GV.parseString(ds.Tables(0).Rows(0).Item("ServiceCharge").ToString())
                                End If
                            End If

                            If VAdmin_ServiceChargeType.Trim.ToUpper = "PERCENTAGE" Then
                                VAdmin_ServiceChargeAmt = Math.Round(((Amount * VAdmin_ServiceCharge) / 100), 2)
                            ElseIf VAdmin_ServiceChargeType.Trim.ToUpper = "AMOUNT" Then
                                VAdmin_ServiceChargeAmt = (VAdmin_ServiceCharge)
                            End If

                            VAdmin_ServiceChargeAmt_Final = VAdmin_ServiceChargeAmt
                            '//// End Admin Service Charge


                            qry = " select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Money Transfer' and AdminID='" & AdminID & "'; "

                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        '//// Start Admin Commission
                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString() = "" Then
                                                VAdmin_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Admin_Commission").ToString() = "" Then
                                                VAdmin_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Admin_Commission").ToString())
                                            End If
                                        End If

                                        If VAdmin_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            VAdmin_CommissionAmt = Math.Round(((Amount * VAdmin_Commission) / 100), 2)
                                        ElseIf VAdmin_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            VAdmin_CommissionAmt = (VAdmin_Commission)
                                        End If

                                        VAdmin_CommissionAmt_Final = VAdmin_CommissionAmt
                                        '//// End Admin Commission
                                    End If
                                End If
                            End If

                            Return VAdmin_CommissionAmt_Final.ToString & ":" & VAdmin_ServiceChargeAmt_Final.ToString

                            '/// End With Slab

                        Else
                            '//// Start Without Slab


                            '//// Start Admin Commission
                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_CommissionType")) Then
                                If Not ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString() = "" Then
                                    VAdmin_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Admin_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Admin_Commission")) Then
                                If Not ds.Tables(0).Rows(0).Item("Admin_Commission").ToString() = "" Then
                                    VAdmin_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Admin_Commission").ToString())
                                End If
                            End If

                            If VAdmin_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VAdmin_CommissionAmt = Math.Round(((Amount * VAdmin_Commission) / 100), 2)
                            ElseIf VAdmin_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                VAdmin_CommissionAmt = (VAdmin_Commission)
                            End If

                            VAdmin_CommissionAmt_Final = VAdmin_CommissionAmt
                            '//// End Admin Commission



                            '//// Start Admin Service Charge
                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceType")) Then
                                If Not ds.Tables(0).Rows(0).Item("ServiceType").ToString() = "" Then
                                    VAdmin_ServiceChargeType = GV.parseString(ds.Tables(0).Rows(0).Item("ServiceType").ToString())
                                End If
                            End If

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ServiceCharge")) Then
                                If Not ds.Tables(0).Rows(0).Item("ServiceCharge").ToString() = "" Then
                                    VAdmin_ServiceCharge = GV.parseString(ds.Tables(0).Rows(0).Item("ServiceCharge").ToString())
                                End If
                            End If

                            If VAdmin_ServiceChargeType.Trim.ToUpper = "PERCENTAGE" Then
                                VAdmin_ServiceChargeAmt = Math.Round(((Amount * VAdmin_ServiceCharge) / 100), 2)
                            ElseIf VAdmin_ServiceChargeType.Trim.ToUpper = "AMOUNT" Then
                                VAdmin_ServiceChargeAmt = (VAdmin_ServiceCharge)
                            End If

                            VAdmin_ServiceChargeAmt_Final = VAdmin_ServiceChargeAmt
                            '//// End Admin Service Charge



                            Return VAdmin_CommissionAmt_Final.ToString & ":" & VAdmin_ServiceChargeAmt_Final.ToString


                            '/// End Without Slab
                        End If
                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return "0:0"
        End Try
    End Function

    Protected Sub txtEnterAmt_TextChanged(sender As Object, e As EventArgs) Handles txtEnterAmt.TextChanged
        Try
            txtServiceCharge.Text = ""
            txtNetAmount.Text = ""
            lblService.Text = ""
            If txtEnterAmt.Text.Trim = "" Then
                Exit Sub
            End If
            Dim NetAmount As Decimal = 0
            Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer'").Split(":")

            If CDec(txtEnterAmt.Text.Trim) > 1000 Then

                If Service.Length > 1 Then
                    If Service(1).Trim = "Percentage" Then
                        lblService.Text = Service(0) & " %"
                        NetAmount = (CDec(txtEnterAmt.Text.Trim) * CDec(Service(0))) / 100
                        'If NetAmount < 25 Then
                        '    NetAmount = 25
                        'End If
                    ElseIf Service(1).Trim = "Amount" Then
                        lblService.Text = Service(0)
                        NetAmount = CDec(Service(0))
                    ElseIf Service(1).Trim = "Not Applicable" Then
                        lblService.Text = Service(0)
                        NetAmount = CDec(Service(0))
                    End If


                End If
            Else
                NetAmount = 10
            End If

            txtServiceCharge.Text = NetAmount
            txtNetAmount.Text = CDec(GV.parseString(txtEnterAmt.Text.Trim)) + NetAmount

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub txtEnterAmt_2_TextChanged(sender As Object, e As EventArgs) Handles txtEnterAmt_2.TextChanged
        Try
            txtServiceCharge_2.Text = ""
            txtNetAmount_2.Text = ""
            lblService_2.Text = ""
            If txtEnterAmt_2.Text.Trim = "" Then
                Exit Sub
            End If
            Dim NetAmount As Decimal = 0
            Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer-2'").Split(":")

            If CDec(txtEnterAmt_2.Text.Trim) > 1000 Then

                If Service.Length > 1 Then
                    If Service(1).Trim = "Percentage" Then
                        lblService_2.Text = Service(0) & " %"
                        NetAmount = (CDec(txtEnterAmt_2.Text.Trim) * CDec(Service(0))) / 100
                        'If NetAmount < 25 Then
                        '    NetAmount = 25
                        'End If
                    ElseIf Service(1).Trim = "Amount" Then
                        lblService_2.Text = Service(0)
                        NetAmount = CDec(Service(0))
                    ElseIf Service(1).Trim = "Not Applicable" Then
                        lblService_2.Text = Service(0)
                        NetAmount = CDec(Service(0))
                    End If

                End If
            Else
                NetAmount = 10
            End If

            txtServiceCharge_2.Text = NetAmount
            txtNetAmount_2.Text = CDec(GV.parseString(txtEnterAmt_2.Text.Trim)) + NetAmount

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Try
            btnChangeNo.OnClientClick = "printdiv('DIV_PrintReceipt');"
            'Dim btn As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton2"), LinkButton)
            'If Not btn Is Nothing Then
            '    btn.OnClientClick = "window.open('../admin/Print_Installment_Report.aspx?PaymentID=" & GV.parseString(GridView1.Rows(i).Cells(7).Text) & "','_blank');"
            'End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlSelectBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectBank.SelectedIndexChanged
        Try

            If Not ddlSelectBank.SelectedIndex = 0 Then
                txtIFSCCode.Text = GV.FL.AddInVar("IFSCCODE", " " & GV.DefaultDatabase & ".dbo.MoneyTransferBankList_New  where BANKID='" & ddlSelectBank.SelectedValue.Trim & "'")
            Else
                txtIFSCCode.Text = ""
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlGateway_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGateway.SelectedIndexChanged
        Try
            lblError_Gateway.Text = ""
            lblError_Gateway.CssClass = ""
            Dim MoneyTrasfer As String = ""
            DIV_Clear_2()
            DIV_Clear()



            If ddlGateway.SelectedValue.Trim.ToUpper = "MoneyTransferAPI".Trim.ToUpper Then
                MoneyTrasfer = "MoneyTransferAPI_Status"
            Else
                MoneyTrasfer = "MoneyTransferAPI_2_Status"
            End If


            '///// Start Check API  STATUS Super ADmin Level

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Super ADmin Level

            '///// Start Check API  STATUS System Settings 

            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub RechargeCommision_2()
        Try

            Dim VCommissionType, VSub_Dis_CommissionType, VRetailer_CommissionType As String
            VCommissionType = ""
            VSub_Dis_CommissionType = ""
            VRetailer_CommissionType = ""
            Dim VCommission, VSub_Dis_Commission, VRetailer_Commission As Decimal
            VCommission = 0
            VSub_Dis_Commission = 0
            VRetailer_Commission = 0


            Dim VContainCategory, VCanChange, VSlabApplicable As String
            VContainCategory = ""
            VCanChange = ""
            VSlabApplicable = ""



            Dim VadminComAmt, DistributorComAmt, SubDIsComAmt, VRetailerComAmt As Decimal
            VadminComAmt = 0
            DistributorComAmt = 0
            SubDIsComAmt = 0
            VRetailerComAmt = 0



            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalDISAmt, VFinalSUBDISAmt, VFinalRETAILERAmt As Decimal
            VFinaladminAmt = 0
            VFinalDISAmt = 0
            VFinalSUBDISAmt = 0
            VFinalRETAILERAmt = 0

            Dim SubDisID As String = ""
            Dim DisID As String = ""
            Dim AdminID As String = ""
            Dim qry As String = ""

            SubDisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "'")
            DisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "'")
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer-2' and ActiveStatus='Active'"
            ds = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = GV.parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If


                        If VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then

                            '/// Start With Slab

                            Dim Amount1 As String = GV.parseString(txtEnterAmt_2.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Money Transfer-2'; "

                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dis_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Dis_CommissionType").ToString() = "" Then
                                                VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Dis_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dis_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Dis_Commission").ToString() = "" Then
                                                VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Dis_Commission").ToString())
                                            End If
                                        End If

                                        If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                        ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                            DistributorComAmt = (VCommission)
                                        End If


                                        '/////// End Distributor


                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                                VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                                VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                            End If
                                        End If

                                        If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                        ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            SubDIsComAmt = (VSub_Dis_Commission)
                                        End If

                                        '/////// End  Sub Distributor


                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                                VRetailer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                                VRetailer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                            End If
                                        End If

                                        If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                        ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                            VRetailerComAmt = (VRetailer_Commission)
                                        End If

                                        '/////// End  Retailer

                                        VFinaladminAmt = VadminComAmt
                                        VFinalDISAmt = DistributorComAmt
                                        VFinalSUBDISAmt = SubDIsComAmt
                                        VFinalRETAILERAmt = VRetailerComAmt


                                    End If
                                End If
                            End If

                            lblRID_2.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            '/// End With Slab

                        Else
                            '//// Start Without Slab

                            If VContainCategory.Trim.ToUpper = "YES" Then




                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                                Dim Amount1 As String = GV.parseString(txtEnterAmt_2.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then

                                    DistributorComAmt = (VCommission)
                                End If

                                '/////// End Distributor



                                qry = " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Money Transfer-2' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                                qry = qry & " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Money Transfer-2' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




                                ds = New DataSet
                                ds = GV.FL.OpenDsWithSelectQuery(qry)
                                If Not ds Is Nothing Then
                                    If ds.Tables.Count > 0 Then
                                        If ds.Tables(0).Rows.Count > 0 Then


                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                                If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                                    VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                                If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                                    VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                                End If
                                            End If

                                            If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                            ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                SubDIsComAmt = (VSub_Dis_Commission)
                                            End If

                                            '/////// End  Sub Distributor
                                        End If
                                        '/////// End  Sub Distributor

                                        If ds.Tables.Count > 1 Then
                                            If ds.Tables(1).Rows.Count > 0 Then

                                                If Not IsDBNull(ds.Tables(1).Rows(0).Item("CommissionType")) Then
                                                    If Not ds.Tables(1).Rows(0).Item("CommissionType").ToString() = "" Then
                                                        VRetailer_CommissionType = GV.parseString(ds.Tables(1).Rows(0).Item("CommissionType").ToString())
                                                    End If
                                                End If

                                                If Not IsDBNull(ds.Tables(1).Rows(0).Item("Commission")) Then
                                                    If Not ds.Tables(1).Rows(0).Item("Commission").ToString() = "" Then
                                                        VRetailer_Commission = GV.parseString(ds.Tables(1).Rows(0).Item("Commission").ToString())
                                                    End If
                                                End If

                                                If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                    VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                                ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                    VRetailerComAmt = (VRetailer_Commission)
                                                End If

                                                '/////// End  Retailer

                                            End If
                                        End If

                                        '/////// End  Retailer

                                    End If
                                End If



                                VFinaladminAmt = VadminComAmt
                                VFinalDISAmt = DistributorComAmt
                                VFinalSUBDISAmt = SubDIsComAmt
                                VFinalRETAILERAmt = VRetailerComAmt
                                lblRID_2.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper



                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                                '/// NEED To CHANGE HERE EK

                                Dim Amount1 As String = GV.parseString(txtEnterAmt_2.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    DistributorComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    DistributorComAmt = (VCommission)
                                End If


                                '/////// End Distributor



                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                        VSub_Dis_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                        VSub_Dis_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                    End If
                                End If

                                If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    SubDIsComAmt = Math.Round(((Amount * VSub_Dis_Commission) / 100), 2)
                                ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    SubDIsComAmt = (VSub_Dis_Commission)
                                End If

                                '/////// End  Sub Distributor




                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                        VRetailer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                        VRetailer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                    End If
                                End If

                                If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VRetailerComAmt = Math.Round(((Amount * VRetailer_Commission) / 100), 2)
                                ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VRetailerComAmt = (VRetailer_Commission)
                                End If

                                '/////// End  Retailer


                                VFinaladminAmt = VadminComAmt
                                VFinalDISAmt = DistributorComAmt
                                VFinalSUBDISAmt = SubDIsComAmt
                                VFinalRETAILERAmt = VRetailerComAmt
                                lblRID_2.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            End If

                            '/// End Without Slab
                        End If




                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub RechargeCommision_Customer_2()
        Try

            Dim VCommissionType, VCustomer_CommissionType As String
            VCommissionType = ""
            VCustomer_CommissionType = ""

            Dim VCommission, VCustomer_Commission As Decimal
            VCommission = 0
            VCustomer_Commission = 0

            Dim VContainCategory, VCanChange, VSlabApplicable As String
            VContainCategory = ""
            VCanChange = ""
            VSlabApplicable = ""



            Dim VadminComAmt, VCustomerComAmt As Decimal
            VadminComAmt = 0
            VCustomerComAmt = 0



            Dim CustomerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalCustomerAmt As Decimal
            VFinaladminAmt = 0
            VFinalCustomerAmt = 0

            Dim AdminID As String = ""

            Dim qry As String = ""

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Money Transfer-2' and ActiveStatus='Active'"
            ds = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContainCategory")) Then
                            If Not ds.Tables(0).Rows(0).Item("ContainCategory").ToString() = "" Then
                                VContainCategory = GV.parseString(ds.Tables(0).Rows(0).Item("ContainCategory").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                            End If
                        End If

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("SlabApplicable")) Then
                            If Not ds.Tables(0).Rows(0).Item("SlabApplicable").ToString() = "" Then
                                VSlabApplicable = GV.parseString(ds.Tables(0).Rows(0).Item("SlabApplicable").ToString())
                            End If
                        End If


                        If VSlabApplicable.Trim.ToUpper = "With Slab".ToUpper Then

                            '/// Start With Slab

                            Dim Amount1 As String = GV.parseString(txtEnterAmt_2.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Money Transfer-2'; "

                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qry)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                                VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                            End If
                                        End If

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                            If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                                VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                            End If
                                        End If

                                        If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                            VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                        ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                            VCustomerComAmt = (VCommission)
                                        End If


                                        '/////// End Distributor




                                        '/////// End  Retailer

                                        VFinaladminAmt = VadminComAmt
                                        VFinalCustomerAmt = VCustomerComAmt




                                    End If
                                End If
                            End If

                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                            '/// End With Slab

                        Else
                            '//// Start Without Slab

                            If VContainCategory.Trim.ToUpper = "YES" Then




                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                                Dim Amount1 As String = GV.parseString(txtEnterAmt_2.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VCustomerComAmt = (VCommission)
                                End If

                                '/////// End Distributor



                                VFinaladminAmt = VadminComAmt
                                VFinalCustomerAmt = VCustomerComAmt

                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper


                            ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                                '/// NEED To CHANGE HERE EK

                                Dim Amount1 As String = GV.parseString(txtEnterAmt_2.Text.Trim)
                                If Amount1.Trim = "" Then
                                    Amount1 = "0"
                                End If
                                Dim Amount As Decimal = Amount1

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                    If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                        VCommission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VCustomerComAmt = Math.Round(((Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VCustomerComAmt = (VCommission)
                                End If


                                '/////// End Distributor


                                VFinaladminAmt = VadminComAmt
                                VFinalCustomerAmt = VCustomerComAmt

                                lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper
                            End If

                            '/// End Without Slab
                        End If




                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            Dim VerifyMessage1 As String = ""
            Try
                lblTranferAmtError_2.Text = ""
                lblTranferAmtError_2.CssClass = ""
                'Dim MoneyTrasfer As String = ""
                ''///// Start Check API  STATUS Super ADmin Level

                'If ddlGateway.SelectedValue.Trim.ToUpper = "MoneyTransferAPI".Trim.ToUpper Then
                '    MoneyTrasfer = "MoneyTransferAPI_Status"
                'Else
                '    MoneyTrasfer = "MoneyTransferAPI_2_Status"
                'End If


                ''///// Start Check API  STATUS Super ADmin Level

                'Dim MoneyTransferAPI_Status As String = ""
                'MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

                'If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                '    lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                '    lblError_Gateway.CssClass = "errorlabels"
                '    Exit Sub
                'End If

                ''///// End Check API  STATUS Super ADmin Level

                ''///// Start Check API  STATUS System Settings 

                'MoneyTransferAPI_Status = ""
                'MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

                'If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                '    lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                '    lblError_Gateway.CssClass = "errorlabels"
                '    Exit Sub
                'End If

                ''///// End Check API  STATUS Retailer Level Settings 

                'Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

                ''///// Start Check API  STATUS System Settings 
                'MoneyTransferAPI_Status = ""
                'MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

                'If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                '    lblTranferAmtError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                '    lblTranferAmtError.CssClass = "errorlabels"
                '    Exit Sub
                'End If

                ''///// End Check API  STATUS Retailer Level  Settings 



                'If txtEnterAmt_2.Text.Trim = "" Then
                '    lblTranferAmtError_2.Text = "Please Enter Amount."
                '    lblTranferAmtError_2.CssClass = "errorlabels"
                '    Exit Sub
                'End If



                'Dim VNetAmount As Decimal = 0
                'If txtNetAmount_2.Text.Trim = "" Then
                '    VNetAmount = 0
                'Else
                '    VNetAmount = GV.parseString(txtNetAmount_2.Text.Trim)
                'End If

                'If VNetAmount <= 0 Then
                '    lblTranferAmtError_2.Text = "Amount Should be Greater Than 0."
                '    lblTranferAmtError_2.CssClass = "errorlabels"
                '    Exit Sub
                'End If

                'Dim holdAmt As String = ""
                'holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
                'If holdAmt.Trim = "" Then
                '    holdAmt = "0"
                'End If

                'If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
                'Else
                '    lblTranferAmtError_2.Text = "You Have Insufficient Wallet Amount"
                '    lblTranferAmtError_2.CssClass = "errorlabels"
                '    Exit Sub
                'End If

                ''///// Check For API Balance - Start //////
                'If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                '    lblTranferAmtError_2.Text = "Insufficient API Balance."
                '    lblTranferAmtError_2.CssClass = "errorlabels"
                '    Exit Sub
                'End If
                ''///// Check For API Balance - End //////

                'If txtTransactionPin_2.Text = "" Then
                '    lblTranferAmtError_2.Text = "Please Enter Your Transaction Pin."
                '    lblTranferAmtError_2.CssClass = "errorlabels"
                '    Exit Sub
                'End If
                'Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                'Dim TransPiNo As String = ""
                'TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

                'If TransPiNo.Trim = txtTransactionPin_2.Text.Trim Then
                'Else
                '    lblTranferAmtError_2.Text = "Invalid Transaction Pin"
                '    lblTranferAmtError_2.CssClass = "errorlabels"
                '    Exit Sub
                'End If




                Dim TranscationId, OrderNO As String

                OrderNO = ""
                TranscationId = ""

                Dim partner_id, api_password, mobile_no, operator_code, amount, partner_request_id, circle, recharge_type, user_var1 As String
                ' Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                partner_id = "RKITAPI190212"

                api_password = "cg45ob8"
                Dim apistr As String = ""
                partner_request_id = GV.FL.getAutoNumber("TransId")
                apistr = " https://rechargkit.biz/get/dmr/moneyTransfer?partner_id=RKITAPI190212&api_password=cg45ob8&mobile_no=" & lblReceipentMobileNo_2.Text.Trim & "&beneId=" & lblReceipentId_2.Text.Trim & "&amount=" & txtEnterAmt_2.Text.Trim & "&partner_request_id= " & partner_request_id & ""
                ''https://rechargkit.biz/get/dmr/moneyTransfer?partner_id=XXXX&api_password=XXXX&mobile_no=XXXX&beneId=XXXX&amount=10.00&partner_request_id= XXXX

                ''{"ERROR":0,"STATUS":2,"ORDERID":52305281,"OPTRANSID":"Bad Request","PARTNERREQID":"5261","MESSAGE":"Success","COMMISSION":"0.0000","CHARGE":"8.26"}
                ''{"ERROR":0,"STATUS":2,"ORDERID":52309336,"OPTRANSID":"e57b117c679e11ebaef80a0047330000","PARTNERREQID":"526","MESSAGE":"Success","COMMISSION":"0.0000","CHARGE":"8.26"}

                APIResult = ReadbyRestClient_2(apistr, apistr)
                Dim json1 As JObject = JObject.Parse(APIResult)


                Dim Status As String = json1.SelectToken("ERROR").ToString
                VerifyMessage1 = json1.SelectToken("MESSAGE").ToString
                TranscationId = json1.SelectToken("ORDERID").ToString

                Dim IFSC, AccountNo, Receipent, ReceipentId As String

                IFSC = lblIFSCCode_2.Text.Trim
                AccountNo = lblRAccountNo_2.Text.Trim
                Receipent = lblReceipentName_2.Text
                ReceipentId = lblReceipentId_2.Text.Trim


                Dim VUpdatedBy, VUpdatedOn, VRecord_DateTime As String
                VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                VUpdatedOn = "getdate()"
                VRecord_DateTime = "getdate()"
                Dim Qry As String = "Insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_MoneyTransfer_API (Refund_Status,TransIpAddress,IFSC,AccountNo,Receipent,ReceipentId,TransId,APIStatus,APIMessage,TransferDate,OrderNo,RefrenceNo,TranscationId,CustomerID,MobileNo,Amount,BankName,Method,Process,RecordDateTime,UpdatedBy,UpdatedOn) values ('No','" & GV.parseString(GV.GetIPAddress) & "','" & IFSC & "','" & AccountNo & "','" & Receipent & "','" & ReceipentId & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & Status & "','" & VerifyMessage1 & "','" & Now.Date & "','" & OrderNO & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & TranscationId & "','" & GV.parseString(txtEnterMobileNo.Text.Trim) & "','" & GV.parseString(lblReceipentMobileNo_2.Text.Trim) & "'," & GV.parseString(txtEnterAmt_2.Text.Trim) & ",'" & GV.parseString(lblRBankName_2.Text.Trim) & "','IMPS','Money Transfer-2',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate() )"
                Dim Result As Boolean = False
                Result = GV.FL.DMLQueries(Qry)
                If Result = True Then
                    'lblTranferAmtError.Text = VerifyMessage1
                    Qry = ""
                    If Status = "0" Then

                        If CDec(txtEnterAmt_2.Text.Trim) > 0 Then 'previous 2500
                            Dim TypeName As String = "Money Transfer-2"
                            Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                            If GRP = "Retailer".ToUpper Then
                                RechargeCommision_2()
                                If Not lblRID_2.Text = "" Then
                                    Dim AAID() As String = lblRID_2.Text.Split("*")
                                    Dim Adminid_Com() As String = AAID(0).Split(":")
                                    Dim DisID_Com() As String = AAID(1).Split(":")
                                    Dim SubDIsID_Com() As String = AAID(2).Split(":")
                                    Dim RetailerID_Com() As String = AAID(3).Split(":")

                                    Dim adminID As String = Adminid_Com(0)
                                    Dim adminCom As String = Adminid_Com(1)
                                    Dim DisID As String = DisID_Com(0)
                                    Dim DisCom As String = DisID_Com(1)
                                    Dim SUBDisID As String = SubDIsID_Com(0)
                                    Dim SUBDisCom As String = SubDIsID_Com(1)
                                    Dim RTEID As String = RetailerID_Com(0)
                                    Dim RTECom As String = RetailerID_Com(1)


                                    Dim arrCanChange() As String = AAID(4).Split(":")
                                    Dim vCanChange As String = arrCanChange(1)


                                    If vCanChange.Trim.ToUpper = "YES" Then
                                        Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                        Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."

                                        Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."

                                        Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."


                                        Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txtEnterAmt_2.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                        '//// Distributor Commission Calculation - Start
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If DisCom > 0 Then
                                            V_Actual_Commission_Amt = DisCom
                                            V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                            V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                            V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                            V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                            DisCom = V_Net_Commission_Amt
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        '//// Distributor Commission Calculation - End

                                        '//// SUB Distributor Commission Calculation - Start
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If SUBDisCom > 0 Then
                                            V_Actual_Commission_Amt = SUBDisCom
                                            V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                            V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                            V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                            V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                            SUBDisCom = V_Net_Commission_Amt
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        '//// SUB Distributor Commission Calculation - End

                                        '//// Retailer Commission Calculation - Start
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If RTECom > 0 Then
                                            V_Actual_Commission_Amt = RTECom
                                            V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                            V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                            V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                            V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                            RTECom = V_Net_Commission_Amt
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        '//// Retailer Commission Calculation - END

                                    Else
                                        'vCanChange.Trim.ToUpper = "No"

                                        Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                        Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."

                                        Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."

                                        Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                        Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."


                                        Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txtEnterAmt_2.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                        '//// Distributor Commission Calculation - Start
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If DisCom > 0 Then
                                            V_Actual_Commission_Amt = DisCom
                                            V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                            V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                            V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                            V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                            DisCom = V_Net_Commission_Amt
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        '//// Distributor Commission Calculation - End

                                        '//// SUB Distributor Commission Calculation - Start
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If SUBDisCom > 0 Then
                                            V_Actual_Commission_Amt = SUBDisCom
                                            V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                            V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                            V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                            V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                            SUBDisCom = V_Net_Commission_Amt
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        '//// SUB Distributor Commission Calculation - End

                                        '//// Retailer Commission Calculation - Start
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If RTECom > 0 Then
                                            V_Actual_Commission_Amt = RTECom
                                            V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                            V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                            V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                            V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                            RTECom = V_Net_Commission_Amt
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        '//// Retailer Commission Calculation - END

                                    End If

                                    'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    Dim ServiceCharge As Decimal = 0
                                    If CDec(GV.parseString(txtServiceCharge_2.Text.Trim)) > 0 Then
                                        ServiceCharge = GV.parseString(txtServiceCharge_2.Text.Trim)
                                        'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                        ' ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                        'Else
                                        '    ServiceCharge = 10
                                        'End If
                                        If ServiceCharge > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge_2.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If

                                    End If

                                    GV.FL.DMLQueriesBulk(Qry)
                                    Qry = ""
                                Else

                                    Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                    Dim Retid As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & Retid & "','Admin','" & txtEnterAmt_2.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim ServiceCharge As Decimal = 0
                                    If CDec(GV.parseString(txtServiceCharge_2.Text.Trim)) > 0 Then
                                        ServiceCharge = GV.parseString(txtServiceCharge_2.Text.Trim)

                                        If ServiceCharge > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge_2.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If

                                    End If

                                End If
                            ElseIf GRP = "Customer".ToUpper Then
                                'In case of Customer 
                                RechargeCommision_Customer_2()
                                If Not lblRID.Text = "" Then

                                    Dim AAID() As String = lblRID.Text.Split("*")
                                    Dim Adminid_Com() As String = AAID(0).Split(":")

                                    Dim CustID_Com() As String = AAID(1).Split(":")

                                    Dim adminID As String = Adminid_Com(0)
                                    Dim adminCom As String = Adminid_Com(1)

                                    Dim CustID As String = CustID_Com(0)
                                    Dim CustCom As String = CustID_Com(1)



                                    Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                    Dim Custtypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                    Dim CusttypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & txtEnterAmt_2.Text.Trim & "."


                                    Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & txtEnterAmt_2.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                    '//// Distributor Commission Calculation - Start


                                    '//// customer Commission Calculation - Start
                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If CustCom > 0 Then
                                        V_Actual_Commission_Amt = CustCom
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        CustCom = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CusttypecommTo & "','" & Custtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// customer Commission Calculation - END



                                    Dim ServiceCharge As Decimal = 0
                                    If CDec(GV.parseString(txtServiceCharge_2.Text.Trim)) > 0 Then
                                        ServiceCharge = GV.parseString(txtServiceCharge_2.Text.Trim)

                                        If ServiceCharge > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge_2.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If

                                    End If

                                Else

                                    Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt_2.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                                    Dim Cust As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & Cust & "','Admin','" & txtEnterAmt_2.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    Dim ServiceCharge As Decimal = 0
                                    If CDec(GV.parseString(txtServiceCharge_2.Text.Trim)) > 0 Then
                                        ServiceCharge = GV.parseString(txtServiceCharge_2.Text.Trim)

                                        If ServiceCharge > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge_2.Text.Trim & " Rs. Due to " & TypeName & " / AMT " & txtEnterAmt_2.Text.Trim & "."
                                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & txtEnterAmt_2.Text.Trim & "','" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge_2.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If

                                    End If

                                End If

                            End If

                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                            '//// Admin & Super Admin Commission Calculation - Start
                            If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                                '//// Admin Commission Calculation - Start
                                Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Comm_Result As String
                                Dim VCus_Amount, V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                                If GV.parseString(txtEnterAmt_2.Text.Trim) = "" Then
                                    V_Amount = "0"
                                Else
                                    V_Amount = txtEnterAmt_2.Text.Trim
                                End If
                                VCus_Amount = V_Amount

                                V_OperatorCategory = ""
                                V_OperatorCode = ""
                                V_APIName = "Money Transfer-2"
                                V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                                Comm_Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)


                                Dim Transaction_Charge_Amt As Decimal = 2.5
                                Dim VFrom_Trans As String = "Your Account is debited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                Dim VTo_Trans As String = "Your Account is credited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo_Trans & "','" & VFrom_Trans & "','Transaction Charge','Transaction Charge','" & Now.Date & "','Admin','Super Admin','" & Transaction_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"



                                If Not GV.parseString(Comm_Result) = "" Then
                                    Dim Result_Arry() As String = Comm_Result.Split("*")
                                    Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                    Dim Admin_Com_ID As String = "Admin"
                                    Dim Admin_Com_Amt As String = Admin_Com(1)

                                    Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                    Dim Service_Charge_ID As String = ""
                                    Dim Service_Charge_Amt As String = Service_Charge(1)

                                    If Not IsNumeric(Service_Charge_Amt.Trim) Then
                                        Service_Charge_Amt = 0
                                    End If
                                    If Service_Charge_Amt > 0 And Service_Charge_Amt < 10 Then
                                        Service_Charge_Amt = 10
                                    End If

                                    If Service_Charge_Amt > 0 Then
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & " / AMT " & VCus_Amount & "."
                                    Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."

                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If Admin_Com_Amt > 0 Then
                                        V_Actual_Commission_Amt = Admin_Com_Amt
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        Admin_Com_Amt = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                '//// Admin Commission Calculation - End

                                '//// Super Admin Commission Calculation - Start
                                Comm_Result = GV.Commision_Calculation_For_SuperAdmin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName)

                                If Not GV.parseString(Comm_Result) = "" Then
                                    Dim Result_Arry() As String = Comm_Result.Split("*")
                                    Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                    Dim Admin_Com_ID As String = "Super Admin"
                                    Dim Admin_Com_Amt As String = Admin_Com(1)

                                    Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                    Dim Service_Charge_ID As String = ""
                                    Dim Service_Charge_Amt As String = Service_Charge(1)


                                    If Service_Charge_Amt > 0 Then
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txtEnterMobileNo.Text.Trim & " / AMT " & VCus_Amount & "."

                                    V_Actual_Commission_Amt = 0
                                    V_GSTAmt = 0
                                    V_Commission_Without_GST = 0
                                    V_TDS_Amt = 0
                                    V_Net_Commission_Amt = 0

                                    If Admin_Com_Amt > 0 Then
                                        V_Actual_Commission_Amt = Admin_Com_Amt
                                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                        Admin_Com_Amt = V_Net_Commission_Amt
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & TranscationId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId_2.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If
                                '//// Super Admin Commission Calculation - End
                            End If
                            '//// Admin & Super Admin Commission Calculation - End
                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

                            If Not Qry.Trim = "" Then
                                GV.FL.DMLQueriesBulk(Qry)
                            End If


                        Else

                            ''Only Service Charge
                            ''Commented due to condition not require 


                            'Dim TypeName As String = "Money Transfer"

                            'Dim ServiceCharge As Decimal = 0
                            'Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)


                            'Dim typeAmtForm As String = "Your Account is debited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                            'Dim typeAmtTo As String = "Your Account is credited by " & txtEnterAmt.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txtEnterMobileNo.Text.Trim) & "."
                            'Qry = "insert into BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTE & "','SuperAdmin','" & txtEnterAmt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"


                            'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 0 Then
                            '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                            '    If ServiceCharge > 0 Then

                            '        Dim VFrom As String = "Your Account is debited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName
                            '        Dim VTo As String = "Your Account is credited by ServiceCharge " & txtServiceCharge.Text.Trim & " Rs. Due to " & TypeName
                            '        Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & TranscationId & "','" & txtEnterAmt.Text.Trim & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & GV.parseString(txtServiceCharge.Text.Trim) & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                            '    End If
                            'End If
                            'GV.FL.DMLQueriesBulk(Qry)
                        End If

                    End If
                    lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                    lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                    lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                    lblPopTransactionID.Text = TranscationId
                    lblPopTransferAmt.Text = GV.parseString(txtEnterAmt_2.Text.Trim)
                    lblPopStatus.Text = VerifyMessage1
                    lblPopAccountNo.Text = lblRAccountNo_2.Text.Trim
                    lblPopServiceCharge.Text = txtServiceCharge_2.Text.Trim
                    lblpopBankName.Text = lblRBankName_2.Text.Trim
                    lblpopMobileNo.Text = txtEnterMobileNo.Text.Trim
                    ModalPopupExtender3.Show()

                End If

            Catch ex As Exception
                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)


                If VerifyMessage1.Trim = "" Then
                    lblTranferAmtError_2.Text = ""
                    lblTranferAmtError_2.CssClass = ""
                Else
                    lblTranferAmtError_2.Text = VerifyMessage1
                    lblTranferAmtError_2.CssClass = "errorlabels"
                End If
                lblTranferAmtError_2.CssClass = ""
                lblTranferAmtError_2.Text = APIResult
            End Try

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnTranferAmt_2_Click(sender As Object, e As EventArgs) Handles btnTranferAmt_2.Click

        Dim VerifyMessage1 As String = ""
        Try
            lblTranferAmtError_2.Text = ""
            lblTranferAmtError_2.CssClass = ""
            Dim MoneyTrasfer As String = ""
            '///// Start Check API  STATUS Super ADmin Level

            If ddlGateway.SelectedValue.Trim.ToUpper = "MoneyTransferAPI".Trim.ToUpper Then
                MoneyTrasfer = "MoneyTransferAPI_Status"
            Else
                MoneyTrasfer = "MoneyTransferAPI_2_Status"
            End If


            '///// Start Check API  STATUS Super ADmin Level

            Dim MoneyTransferAPI_Status As String = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Company Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Super ADmin Level

            '///// Start Check API  STATUS System Settings 

            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("" & MoneyTrasfer & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblError_Gateway.Text = "Sorry! Money Transfer API Is Inactive At Admin Level, Contact to Administrator"
                lblError_Gateway.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            MoneyTransferAPI_Status = ""
            MoneyTransferAPI_Status = GV.FL.AddInVar("MoneyTransferAPI_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not MoneyTransferAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblTranferAmtError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblTranferAmtError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 



            If txtEnterAmt_2.Text.Trim = "" Then
                lblTranferAmtError_2.Text = "Please Enter Amount."
                lblTranferAmtError_2.CssClass = "errorlabels"
                Exit Sub
            End If



            Dim VNetAmount As Decimal = 0
            If txtNetAmount_2.Text.Trim = "" Then
                VNetAmount = 0
            Else
                VNetAmount = GV.parseString(txtNetAmount_2.Text.Trim)
            End If

            If VNetAmount <= 0 Then
                lblTranferAmtError_2.Text = "Amount Should be Greater Than 0."
                lblTranferAmtError_2.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If

            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
            Else
                lblTranferAmtError_2.Text = "You Have Insufficient Wallet Amount"
                lblTranferAmtError_2.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// Check For API Balance - Start //////
            If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                lblTranferAmtError_2.Text = "Insufficient API Balance."
                lblTranferAmtError_2.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// Check For API Balance - End //////

            If txtTransactionPin_2.Text = "" Then
                lblTranferAmtError_2.Text = "Please Enter Your Transaction Pin."
                lblTranferAmtError_2.CssClass = "errorlabels"
                Exit Sub
            End If
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim TransPiNo As String = ""
            TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If TransPiNo.Trim = txtTransactionPin_2.Text.Trim Then
            Else
                lblTranferAmtError_2.Text = "Invalid Transaction Pin"
                lblTranferAmtError_2.CssClass = "errorlabels"
                Exit Sub
            End If


            btnok.Text = "Yes"
            btnok.Visible = True
            btnok_Transfer_1.visible = False

            btnCancel.Text = "No"
            lblDialogMsg.Text = "Are You sure you want to Proceed ??"
            lblDialogMsg.CssClass = ""
            ModalPopupExtender1.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnRedirectPage_Click(sender As Object, e As EventArgs) Handles btnRedirectPage.Click
        Try

            Response.Redirect("BOS_MoneyTransfer.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class