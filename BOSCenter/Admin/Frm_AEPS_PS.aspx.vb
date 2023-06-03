Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json

Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims


Public Class Frm_AEPS_PS
    Inherits System.Web.UI.Page

    Dim AEPS_Obj As New AEPS_Functions

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet



    'var XML = '<?xml version="1.0"?> <PidOptions ver="1.0"> <Opts fCount="1" fType="0" iCount="0" pCount="0" format="0" pidVer="2.0" timeout="10000" posh="UNKNOWN" env="P" /> ' + DString + '<CustOpts><Param name="mantrakey" value="" /></CustOpts> </PidOptions>';




    'AIzaSyB7y_VVy8EomHfwS-QfrR_7onftDFNu3XY
    'Geo Location
    'AIzaSyB7y_VVy8EomHfwS-QfrR_7onftDFNu3XY

    'WHITELISTED IP 	103.205.66.210, 162.144.106.233,103.83.145.84
    'ENVIORMENT 	UAT
    'JWT KEY 	UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh
    'AES ENCRYPTION KEY 	c05085d229a39a7e
    'AES ENCRYPTION IV 	d45195355e9db9a3
    'CALLBACK URL 	https://boscenter.in/api/PaySprintDMTCallbkController
    'SENDER ID 	
    'STATUS 	ACTIVE
    'VERSION 	IP AND AUTHORIZED KEY BASED


    '///// Live

    '    API BASE URL	https://api.paysprint.in/
    'WHITELISTED IP	103.205.66.210,103.77.42.228,122.160.58.113
    'ENVIORMENT	LIVE
    'JWT KEY	UFMwMDgzM2U4Mzc3NmQ5MTlmNGI4ZDRmNjI3NjJiNGUwMDU0MzJi
    'AES ENCRYPTION KEY	1906ac7e46c3b83f
    'AES ENCRYPTION IV	da4e5fa6feb74109
    'CALLBACK URL	https://boscenter.in/api/PaySprintDMTCallbkController
    'SENDER ID	PSPRNT
    'STATUS	ACTIVE

    'https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyB7y_VVy8EomHfwS-QfrR_7onftDFNu3XY


    Dim baseURL As String = "https://api.paysprint.in"
    'Dim baseURL As String = "https://paysprint.in/service-api"


    Dim Bank_List_API_URL As String = baseURL & "/api/v1/service/aeps/banklist/index"
    Dim Enquiry_API_URL As String = baseURL & "/api/v1/service/aeps/balanceenquiry/index"
    Dim Withdraw_API_URL As String = baseURL & "/api/v1/service/aeps/cashwithdraw/index"
    Dim Mini_Statement_API_URL As String = baseURL & "/api/v1/service/aeps/ministatement/index"

    Dim Merchant_Onboard_API_URL As String = baseURL & "/api/v1/service/onboard/onboard/getonboardurl"
    Dim Merchant_Onboard_Status_Chk_API_URL As String = baseURL & "/api/v1/service/onboard/onboard/getonboardstatus"

    Dim Withdraw_Status_API_URL As String = baseURL & "/api/v1/service/aeps/aepsquery/query"
    Dim Withdraw_Three_way_API_URL As String = baseURL & "/api/v1/service/aeps/threeway/threeway"


    Dim aadhar_Pay_API_URL As String = baseURL & "/api/v1/service/aadharpay/aadharpay/index"
    Dim aadhar_Pay_Trans_Status_API_URL As String = baseURL & "/api/v1/service/aadharpay/aadharpayquery/query"


    Public Class aadhar_Pay_API_Parameters
        Dim V_latitude, V_longitude, V_mobilenumber, V_referenceno, V_ipaddress, V_adhaarnumber, V_accessmodetype, V_nationalbankidentification, V_requestremarks, V_data As String
        Dim V_pipe, V_timestamp, V_transactiontype, V_submerchantid, V_is_iris As String
        Dim V_amount As Integer


        Public Property latitude() As String
            Get
                Return V_latitude
            End Get
            Set(ByVal value As String)
                V_latitude = value
            End Set
        End Property
        Public Property longitude() As String
            Get
                Return V_longitude
            End Get
            Set(ByVal value As String)
                V_longitude = value
            End Set
        End Property
        Public Property mobilenumber() As String
            Get
                Return V_mobilenumber
            End Get
            Set(ByVal value As String)
                V_mobilenumber = value
            End Set
        End Property
        Public Property referenceno() As String
            Get
                Return V_referenceno
            End Get
            Set(ByVal value As String)
                V_referenceno = value
            End Set
        End Property
        Public Property ipaddress() As String
            Get
                Return V_ipaddress
            End Get
            Set(ByVal value As String)
                V_ipaddress = value
            End Set
        End Property
        Public Property adhaarnumber() As String
            Get
                Return V_adhaarnumber
            End Get
            Set(ByVal value As String)
                V_adhaarnumber = value
            End Set
        End Property
        Public Property accessmodetype() As String
            Get
                Return V_accessmodetype
            End Get
            Set(ByVal value As String)
                V_accessmodetype = value
            End Set
        End Property
        Public Property nationalbankidentification() As String
            Get
                Return V_nationalbankidentification
            End Get
            Set(ByVal value As String)
                V_nationalbankidentification = value
            End Set
        End Property

        Public Property requestremarks() As String
            Get
                Return V_requestremarks
            End Get
            Set(ByVal value As String)
                V_requestremarks = value
            End Set
        End Property

        Public Property data() As String
            Get
                Return V_data
            End Get
            Set(ByVal value As String)
                V_data = value
            End Set
        End Property

        Public Property pipe() As String
            Get
                Return V_pipe
            End Get
            Set(ByVal value As String)
                V_pipe = value
            End Set
        End Property
        Public Property timestamp() As String
            Get
                Return V_timestamp
            End Get
            Set(ByVal value As String)
                V_timestamp = value
            End Set
        End Property
        Public Property transactionType() As String
            Get
                Return V_transactiontype
            End Get
            Set(ByVal value As String)
                V_transactiontype = value
            End Set
        End Property

        Public Property submerchantid() As String
            Get
                Return V_submerchantid
            End Get
            Set(ByVal value As String)
                V_submerchantid = value
            End Set
        End Property

        Public Property amount() As Integer
            Get
                Return V_amount
            End Get
            Set(ByVal value As Integer)
                V_amount = value
            End Set
        End Property


        Public Property is_iris() As String
            Get
                Return V_is_iris
            End Get
            Set(ByVal value As String)
                V_is_iris = value
            End Set
        End Property


    End Class

    Public Class Withdraw_Three_way_API_Parameters
        Dim V_reference, V_status As String

        Public Property reference() As String
            Get
                Return V_reference
            End Get
            Set(ByVal value As String)
                V_reference = value
            End Set
        End Property

        Public Property status() As String
            Get
                Return V_status
            End Get
            Set(ByVal value As String)
                V_status = value
            End Set
        End Property


    End Class
    Public Class Withdraw_Status_API_Parameters
        Dim V_reference As String

        Public Property reference() As String
            Get
                Return V_reference
            End Get
            Set(ByVal value As String)
                V_reference = value
            End Set
        End Property
    End Class

    Public Class Merchant_Onboard_API_Parameters
        Dim V_merchantcode, V_mobile, V_is_new, V_email, V_firm, V_callback As String

        Public Property merchantcode() As String
            Get
                Return V_merchantcode
            End Get
            Set(ByVal value As String)
                V_merchantcode = value
            End Set
        End Property
        Public Property mobile() As String
            Get
                Return V_mobile
            End Get
            Set(ByVal value As String)
                V_mobile = value
            End Set
        End Property
        Public Property is_new() As String
            Get
                Return V_is_new
            End Get
            Set(ByVal value As String)
                V_is_new = value
            End Set
        End Property
        Public Property email() As String
            Get
                Return V_email
            End Get
            Set(ByVal value As String)
                V_email = value
            End Set
        End Property
        Public Property firm() As String
            Get
                Return V_firm
            End Get
            Set(ByVal value As String)
                V_firm = value
            End Set
        End Property
        Public Property callback() As String
            Get
                Return V_callback
            End Get
            Set(ByVal value As String)
                V_callback = value
            End Set
        End Property


    End Class

    Public Class Merchant_Onboard_Status_Chk_API_Parameters
        Dim V_merchantcode, V_mobile, V_pipe As String

        Public Property merchantcode() As String
            Get
                Return V_merchantcode
            End Get
            Set(ByVal value As String)
                V_merchantcode = value
            End Set
        End Property
        Public Property mobile() As String
            Get
                Return V_mobile
            End Get
            Set(ByVal value As String)
                V_mobile = value
            End Set
        End Property
        Public Property pipe() As String
            Get
                Return V_pipe
            End Get
            Set(ByVal value As String)
                V_pipe = value
            End Set
        End Property

    End Class

    Public Class Enquiry_API_Parameters
        Dim V_latitude, V_longitude, V_mobilenumber, V_referenceno, V_ipaddress, V_adhaarnumber, V_accessmodetype, V_nationalbankidentification, V_requestremarks, V_data As String
        Dim V_pipe, V_timestamp, V_transactiontype, V_submerchantid, V_is_iris As String


        Public Property latitude() As String
            Get
                Return V_latitude
            End Get
            Set(ByVal value As String)
                V_latitude = value
            End Set
        End Property
        Public Property longitude() As String
            Get
                Return V_longitude
            End Get
            Set(ByVal value As String)
                V_longitude = value
            End Set
        End Property
        Public Property mobilenumber() As String
            Get
                Return V_mobilenumber
            End Get
            Set(ByVal value As String)
                V_mobilenumber = value
            End Set
        End Property
        Public Property referenceno() As String
            Get
                Return V_referenceno
            End Get
            Set(ByVal value As String)
                V_referenceno = value
            End Set
        End Property
        Public Property ipaddress() As String
            Get
                Return V_ipaddress
            End Get
            Set(ByVal value As String)
                V_ipaddress = value
            End Set
        End Property
        Public Property adhaarnumber() As String
            Get
                Return V_adhaarnumber
            End Get
            Set(ByVal value As String)
                V_adhaarnumber = value
            End Set
        End Property
        Public Property accessmodetype() As String
            Get
                Return V_accessmodetype
            End Get
            Set(ByVal value As String)
                V_accessmodetype = value
            End Set
        End Property
        Public Property nationalbankidentification() As String
            Get
                Return V_nationalbankidentification
            End Get
            Set(ByVal value As String)
                V_nationalbankidentification = value
            End Set
        End Property

        Public Property requestremarks() As String
            Get
                Return V_requestremarks
            End Get
            Set(ByVal value As String)
                V_requestremarks = value
            End Set
        End Property

        Public Property data() As String
            Get
                Return V_data
            End Get
            Set(ByVal value As String)
                V_data = value
            End Set
        End Property

        Public Property pipe() As String
            Get
                Return V_pipe
            End Get
            Set(ByVal value As String)
                V_pipe = value
            End Set
        End Property
        Public Property timestamp() As String
            Get
                Return V_timestamp
            End Get
            Set(ByVal value As String)
                V_timestamp = value
            End Set
        End Property
        Public Property transactiontype() As String
            Get
                Return V_transactiontype
            End Get
            Set(ByVal value As String)
                V_transactiontype = value
            End Set
        End Property

        Public Property submerchantid() As String
            Get
                Return V_submerchantid
            End Get
            Set(ByVal value As String)
                V_submerchantid = value
            End Set
        End Property

        Public Property is_iris() As String
            Get
                Return V_is_iris
            End Get
            Set(ByVal value As String)
                V_is_iris = value
            End Set
        End Property


    End Class

    Public Class Withdraw_API_Parameters
        Dim V_latitude, V_longitude, V_mobilenumber, V_referenceno, V_ipaddress, V_adhaarnumber, V_accessmodetype, V_nationalbankidentification, V_requestremarks, V_data As String
        Dim V_pipe, V_timestamp, V_transactiontype, V_submerchantid, V_is_iris As String
        Dim V_amount As Integer


        Public Property latitude() As String
            Get
                Return V_latitude
            End Get
            Set(ByVal value As String)
                V_latitude = value
            End Set
        End Property
        Public Property longitude() As String
            Get
                Return V_longitude
            End Get
            Set(ByVal value As String)
                V_longitude = value
            End Set
        End Property
        Public Property mobilenumber() As String
            Get
                Return V_mobilenumber
            End Get
            Set(ByVal value As String)
                V_mobilenumber = value
            End Set
        End Property
        Public Property referenceno() As String
            Get
                Return V_referenceno
            End Get
            Set(ByVal value As String)
                V_referenceno = value
            End Set
        End Property
        Public Property ipaddress() As String
            Get
                Return V_ipaddress
            End Get
            Set(ByVal value As String)
                V_ipaddress = value
            End Set
        End Property
        Public Property adhaarnumber() As String
            Get
                Return V_adhaarnumber
            End Get
            Set(ByVal value As String)
                V_adhaarnumber = value
            End Set
        End Property
        Public Property accessmodetype() As String
            Get
                Return V_accessmodetype
            End Get
            Set(ByVal value As String)
                V_accessmodetype = value
            End Set
        End Property
        Public Property nationalbankidentification() As String
            Get
                Return V_nationalbankidentification
            End Get
            Set(ByVal value As String)
                V_nationalbankidentification = value
            End Set
        End Property

        Public Property requestremarks() As String
            Get
                Return V_requestremarks
            End Get
            Set(ByVal value As String)
                V_requestremarks = value
            End Set
        End Property

        Public Property data() As String
            Get
                Return V_data
            End Get
            Set(ByVal value As String)
                V_data = value
            End Set
        End Property

        Public Property pipe() As String
            Get
                Return V_pipe
            End Get
            Set(ByVal value As String)
                V_pipe = value
            End Set
        End Property
        Public Property timestamp() As String
            Get
                Return V_timestamp
            End Get
            Set(ByVal value As String)
                V_timestamp = value
            End Set
        End Property
        Public Property transactiontype() As String
            Get
                Return V_transactiontype
            End Get
            Set(ByVal value As String)
                V_transactiontype = value
            End Set
        End Property

        Public Property submerchantid() As String
            Get
                Return V_submerchantid
            End Get
            Set(ByVal value As String)
                V_submerchantid = value
            End Set
        End Property

        Public Property amount() As Integer
            Get
                Return V_amount
            End Get
            Set(ByVal value As Integer)
                V_amount = value
            End Set
        End Property


        Public Property is_iris() As String
            Get
                Return V_is_iris
            End Get
            Set(ByVal value As String)
                V_is_iris = value
            End Set
        End Property


    End Class

    Public Class Mini_Statement_API_Parameters
        Dim V_latitude, V_longitude, V_mobilenumber, V_referenceno, V_ipaddress, V_adhaarnumber, V_accessmodetype, V_nationalbankidentification, V_requestremarks, V_data As String
        Dim V_pipe, V_timestamp, V_transactiontype, V_submerchantid, V_is_iris As String


        Public Property latitude() As String
            Get
                Return V_latitude
            End Get
            Set(ByVal value As String)
                V_latitude = value
            End Set
        End Property
        Public Property longitude() As String
            Get
                Return V_longitude
            End Get
            Set(ByVal value As String)
                V_longitude = value
            End Set
        End Property
        Public Property mobilenumber() As String
            Get
                Return V_mobilenumber
            End Get
            Set(ByVal value As String)
                V_mobilenumber = value
            End Set
        End Property
        Public Property referenceno() As String
            Get
                Return V_referenceno
            End Get
            Set(ByVal value As String)
                V_referenceno = value
            End Set
        End Property
        Public Property ipaddress() As String
            Get
                Return V_ipaddress
            End Get
            Set(ByVal value As String)
                V_ipaddress = value
            End Set
        End Property
        Public Property adhaarnumber() As String
            Get
                Return V_adhaarnumber
            End Get
            Set(ByVal value As String)
                V_adhaarnumber = value
            End Set
        End Property
        Public Property accessmodetype() As String
            Get
                Return V_accessmodetype
            End Get
            Set(ByVal value As String)
                V_accessmodetype = value
            End Set
        End Property
        Public Property nationalbankidentification() As String
            Get
                Return V_nationalbankidentification
            End Get
            Set(ByVal value As String)
                V_nationalbankidentification = value
            End Set
        End Property

        Public Property requestremarks() As String
            Get
                Return V_requestremarks
            End Get
            Set(ByVal value As String)
                V_requestremarks = value
            End Set
        End Property

        Public Property data() As String
            Get
                Return V_data
            End Get
            Set(ByVal value As String)
                V_data = value
            End Set
        End Property

        Public Property pipe() As String
            Get
                Return V_pipe
            End Get
            Set(ByVal value As String)
                V_pipe = value
            End Set
        End Property
        Public Property timestamp() As String
            Get
                Return V_timestamp
            End Get
            Set(ByVal value As String)
                V_timestamp = value
            End Set
        End Property
        Public Property transactiontype() As String
            Get
                Return V_transactiontype
            End Get
            Set(ByVal value As String)
                V_transactiontype = value
            End Set
        End Property

        Public Property submerchantid() As String
            Get
                Return V_submerchantid
            End Get
            Set(ByVal value As String)
                V_submerchantid = value
            End Set
        End Property

        Public Property is_iris() As String
            Get
                Return V_is_iris
            End Get
            Set(ByVal value As String)
                V_is_iris = value
            End Set
        End Property


    End Class

    Public Class dataBody
        Dim V_body As String
        Public Property body() As String
            Get
                Return V_body
            End Get
            Set(ByVal value As String)
                V_body = value
            End Set
        End Property
    End Class
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("SearchStudent.aspx") '/Change the name of form
            ElseIf btnClear.Text = "Back" Then
                Response.Redirect("SAM_DashBoard.aspx")
            Else


                lblError.Text = ""
                lblError.CssClass = ""


                txtTransID.Text = ""

                txtTransDate.Text = ""


                txtTransID.Text = GV.FL.getAutoNumber("SessionId")

                'txtPassword.Text = ""

                txtTransDate.Text = Now.Date.ToString("dd/MM/yyyy")

                Session("EditFlag") = 0
                btnSave.Text = "Submit"
                btnClear.Text = "Reset"
                lblError.Text = ""
                btnSave.Enabled = True
                btnDelete.Enabled = False
                btnDelete.Visible = False

                txtfpdata.Text = ""
                imgFinger.ImageUrl = "..\images\uploadimage.png"
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            lblError.Text = ""
            lblError.CssClass = ""

            grdResponseDetails.DataSource = Nothing
            grdResponseDetails.DataBind()

            '///// Start Check API  STATUS Super Admin Level

            Dim AEPS_API_Status As String = ""
            AEPS_API_Status = GV.FL.AddInVar("AEPS_API_Status", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")

            If Not AEPS_API_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! AEPS API Is Inactive At Company Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// Start Check API  STATUS Super Admin Level



            '///// Start Check API  STATUS System Settings 

            AEPS_API_Status = ""
            AEPS_API_Status = GV.FL.AddInVar("AEPS_API_Status", "[AutoNumber]")

            If Not AEPS_API_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! AEPS API Is Inactive At Admin Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            AEPS_API_Status = ""
            AEPS_API_Status = GV.FL.AddInVar("AEPS_API_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not AEPS_API_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 

            Dim V_latitude, V_longitude, V_mobilenumber, V_referenceno, V_ipaddress, V_adhaarnumber, V_accessmodetype, V_nationalbankidentification, V_requestremarks, V_data As String
            Dim V_pipe, V_timestamp, V_transactiontype, V_submerchantid, V_is_iris, V_Amount As String

            Dim StrParameters As String = ""



            If GV.parseString(txtLat.Text) = "" Or GV.parseString(txtLong.Text) = "" Then
                lblError.Text = "Please Allow Location Access To Proceed."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"

                Exit Sub
            Else
                V_latitude = GV.parseString(txtLat.Text)
                V_longitude = GV.parseString(txtLong.Text)
            End If

            V_ipaddress = GV.GetIPAddress()

            If ddlBankList.SelectedIndex = 0 Then
                lblError.Text = "Please Select Bank Name."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                ddlBankList.Focus()
                Exit Sub
            Else
                V_nationalbankidentification = GV.parseString(ddlBankList.SelectedValue)
            End If


            If GV.parseString(txtMobileNumber.Text) = "" Then
                lblError.Text = "Please Enter Customer Phone No."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                txtMobileNumber.Focus()
                Exit Sub
            ElseIf Not IsNumeric(txtMobileNumber.Text) Then
                lblError.Text = "Please Enter Correct Phone No."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                txtMobileNumber.Focus()
                Exit Sub
            ElseIf Not GV.parseString(txtMobileNumber.Text).Length = 10 Then
                lblError.Text = "Please Enter 10 Digit Phone No."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                txtMobileNumber.Focus()
                Exit Sub
            Else
                V_MobileNumber = GV.parseString(txtMobileNumber.Text)
            End If

            If GV.parseString(txtAdhaarNumber.Text) = "" Then
                lblError.Text = "Please Enter Adhaar Number."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                txtAdhaarNumber.Focus()
                Exit Sub
            ElseIf Not IsNumeric(txtAdhaarNumber.Text) Then
                lblError.Text = "Please Enter Correct Adhaar Number."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                txtAdhaarNumber.Focus()
                Exit Sub
            ElseIf Not GV.parseString(txtAdhaarNumber.Text).Length = 12 Then
                lblError.Text = "Please Enter 12 Digit Adhaar Number."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                txtAdhaarNumber.Focus()
                Exit Sub
            Else
                V_AdhaarNumber = GV.parseString(txtAdhaarNumber.Text)
            End If

            If ddlTransactionType.SelectedIndex = 0 Then
                lblError.Text = "Please Select Transaction Type."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"
                ddlTransactionType.Focus()
                Exit Sub
            Else
                V_transactiontype = GV.parseString(ddlTransactionType.SelectedValue)
            End If
            V_Amount = "0"

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If
            Dim walletBal As String = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
            If walletBal.Trim = "" Then
                walletBal = "0"
            End If

            If (V_transactiontype.Trim.ToString.ToUpper = "CW" Or V_transactiontype.Trim.ToString.ToUpper = "M") Then
                If GV.parseString(txtAmount.Text) = "" Then
                    lblError.Text = "Please Enter Amount."
                    lblError.Visible = True
                    lblError.CssClass = "errorLabels"
                    txtAmount.Focus()
                    Exit Sub
                ElseIf Not IsNumeric(txtAmount.Text) Then
                    lblError.Text = "Please Enter Correct Amount."
                    lblError.Visible = True
                    lblError.CssClass = "errorLabels"
                    txtAmount.Focus()
                    Exit Sub
                ElseIf Not (CInt(txtAmount.Text) >= 100 And CInt(txtAmount.Text) <= 10000) Then
                    lblError.Text = "Amount Should Be Between 100 To 10000."
                    lblError.Visible = True
                    lblError.CssClass = "errorLabels"
                    txtAmount.Focus()
                    Exit Sub
                Else
                    V_Amount = GV.parseString(txtAmount.Text)

                End If
            ElseIf V_transactiontype.Trim.ToString.ToUpper = "MS" Then
                If (CDec(walletBal) - CDec(holdAmt)) >= CDec(AEPS_MINI_STATEMENT_CHARGE) Then
                Else
                    lblError.Text = "You Have Insufficient Wallet Amount"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            ElseIf V_transactiontype.Trim.ToString.ToUpper = "BE" Then
                If (CDec(walletBal) - CDec(holdAmt)) >= CDec(AEPS_BALANCE_CHECK_CHARGE) Then
                Else
                    lblError.Text = "You Have Insufficient Wallet Amount"
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
            End If


            ''///// Check For API Balance - Start //////
            'If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
            '    lblError.Text = "Insufficient API Balance."
            '    lblError.CssClass = "errorlabels"
            '    Exit Sub
            'End If
            ''///// Check For API Balance - End //////


            If GV.parseString(txtPidData.Text) = "" Then
                lblError.Text = "Please Scan Your Finger At Device."
                lblError.Visible = True
                lblError.CssClass = "errorLabels"

                Exit Sub
            Else
                V_data = GV.parseString(txtPidData.Text)
            End If


            V_accessmodetype = "SITE"
            V_pipe = "bank1"
            V_requestremarks = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            V_is_iris = "No"
            V_submerchantid = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Replace("-", "")
            V_timestamp = Now  '2020-01-12 13:00:12   y m d   '"2022-07-10 13:26:00" '
            V_referenceno = GV.FL.getAutoNumber("TransId") '   GV.FL.getAutoNumber("SessionId")



            'AES ENCRYPTION KEY 	c05085d229a39a7e
            'AES ENCRYPTION IV 	d45195355e9db9a3



            If V_transactiontype.Trim.ToString.ToUpper = "CW" Then
                Dim setParameter_API_Obj As New Withdraw_API_Parameters()
                setParameter_API_Obj.latitude = V_latitude
                setParameter_API_Obj.longitude = V_longitude
                setParameter_API_Obj.mobilenumber = V_mobilenumber
                setParameter_API_Obj.referenceno = V_referenceno
                setParameter_API_Obj.accessmodetype = V_accessmodetype
                setParameter_API_Obj.adhaarnumber = V_adhaarnumber
                setParameter_API_Obj.data = V_data
                setParameter_API_Obj.ipaddress = V_ipaddress
                setParameter_API_Obj.is_iris = V_is_iris
                setParameter_API_Obj.nationalbankidentification = V_nationalbankidentification
                setParameter_API_Obj.pipe = V_pipe
                setParameter_API_Obj.transactiontype = V_transactiontype
                setParameter_API_Obj.timestamp = V_timestamp
                setParameter_API_Obj.submerchantid = V_submerchantid
                setParameter_API_Obj.amount = V_Amount
                setParameter_API_Obj.requestremarks = V_requestremarks

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "1906ac7e46c3b83f", "da4e5fa6feb74109")
                Dim setParameter_Databody_Obj As New dataBody()
                setParameter_Databody_Obj.body = ssss
                Dim strBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_Databody_Obj)
                APIResult = ReadbyRestClient_NEW(Withdraw_API_URL, strBody)
                'Fail {"response_code":2,"message":"Customer ","errorcode":10004,"clientrefno":"50936","bankrrn":"","ackno":30301971,"status":false,"bankiin":"508534"}
                'Success {"status":true,"message":"Request Completed","ackno":30301974,"amount":100,"balanceamount":"9245.68","bankrrn":"218914425851","bankiin":"508505","response_code":1,"errorcode":"00","clientrefno":"50942"}


                Dim jss As New Script.Serialization.JavaScriptSerializer()
                Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(APIResult)


                If data("status") = True Then
                    'Success {"status":true,"message":"Request Completed","ackno":30301974,"amount":100,"balanceamount":"9245.68","bankrrn":"218914425851","bankiin":"508505","response_code":1,"errorcode":"00","clientrefno":"50942"}

                    Dim V_Trans_ID, V_Trans_Date, V_Trans_Type, V_API_Call_Type, V_Access_Mode, V_Bank_Name, V_Bank_iin, V_Aadhar_Pay_Ref_ID, V_Mobile_No, V_Aadhar_Last_No, V_Remarks, V_Date_added, V_Cash_Withdraw, V_Commission, V_TDS, V_API_status, V_CompanyCode, V_RecordDateTime, V_UpdatedOn, V_UpdatedBy As String
                    V_Trans_ID = V_referenceno
                    V_Trans_Date = Now.Date
                    V_Trans_Type = ddlTransactionType.SelectedItem.Text
                    V_API_Call_Type = ddlAPI_Call.SelectedValue
                    V_Access_Mode = "SITE"
                    V_Bank_Name = ddlBankList.SelectedItem.Text
                    V_latitude = V_latitude
                    V_longitude = V_longitude
                    V_Bank_iin = ""
                    V_Aadhar_Pay_Ref_ID = ""
                    V_Aadhar_Last_No = ""
                    V_Remarks = ""
                    V_Cash_Withdraw = "0"
                    V_Commission = "0"
                    V_TDS = "0"
                    V_API_status = data("status").ToString
                    V_CompanyCode = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                    V_RecordDateTime = V_timestamp
                    V_UpdatedOn = V_timestamp
                    V_UpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    V_Mobile_No = txtMobileNumber.Text
                    V_Date_added = V_timestamp

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Status")
                    Dim dc3 As DataColumn = New DataColumn("ackno")
                    Dim dc4 As DataColumn = New DataColumn("balanceamount")
                    Dim dc5 As DataColumn = New DataColumn("amount")
                    Dim dc6 As DataColumn = New DataColumn("message")
                    Dim dc7 As DataColumn = New DataColumn("bankiin")
                    Dim dc8 As DataColumn = New DataColumn("bankrrn")
                    Dim dc9 As DataColumn = New DataColumn("clientrefno")
                    Dim dc10 As DataColumn = New DataColumn("errorcode")
                    Dim dc11 As DataColumn = New DataColumn("response_code")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)
                    dt.Columns.Add(dc6)
                    dt.Columns.Add(dc7)
                    dt.Columns.Add(dc8)
                    dt.Columns.Add(dc9)
                    dt.Columns.Add(dc10)
                    dt.Columns.Add(dc11)

                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = "1"

                    If data.ContainsKey("status") Then
                        dr1(1) = data("status").ToString
                    Else
                        dr1(1) = ""
                    End If

                    If data.ContainsKey("ackno") Then
                        dr1(2) = data("ackno").ToString
                    Else
                        dr1(2) = ""
                    End If

                    If data.ContainsKey("balanceamount") Then
                        dr1(3) = data("balanceamount").ToString
                    Else
                        dr1(3) = ""
                    End If
                    If data.ContainsKey("amount") Then
                        dr1(4) = data("amount").ToString
                        V_Cash_Withdraw = data("amount").ToString
                    Else
                        dr1(4) = ""
                    End If
                    If data.ContainsKey("message") Then
                        dr1(5) = data("message").ToString
                        V_Remarks = data("message").ToString
                    Else
                        dr1(5) = ""
                    End If


                    If data.ContainsKey("bankiin") Then
                        dr1(6) = data("bankiin").ToString
                        V_Bank_iin = data("bankiin").ToString
                    Else
                        dr1(6) = ""
                    End If
                    If data.ContainsKey("bankrrn") Then
                        dr1(7) = data("bankrrn").ToString
                    Else
                        dr1(7) = ""
                    End If

                    If data.ContainsKey("clientrefno") Then
                        dr1(8) = data("clientrefno").ToString
                    Else
                        dr1(8) = ""
                    End If

                    If data.ContainsKey("errorcode") Then
                        dr1(9) = data("errorcode").ToString
                    Else
                        dr1(9) = ""
                    End If

                    If data.ContainsKey("response_code") Then
                        dr1(10) = data("response_code").ToString
                    Else
                        dr1(10) = ""
                    End If

                    If data.ContainsKey("last_aadhar") Then
                        V_Aadhar_Last_No = data("last_aadhar").ToString
                    End If


                    dt.Rows.Add(dr1)

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()


                    '/// AEPS Commission 

                    Dim insrtQry As String = "INSERT INTO  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_AEPS_API([Trans_ID],[Trans_Date],[Trans_Type],[API_Call_Type],[Access_Mode],[Bank_Name],[Bank_iin],[Aadhar_Pay_Ref_ID],[Mobile_No],[Latitude],[Longitude],[Aadhar_Last_No],[Remarks],[Date_added],[Cash_Withdraw],[Commission],[TDS],[API_status],[CompanyCode],[RecordDateTime],[UpdatedOn],[UpdatedBy])     VALUES('" & V_Trans_ID & "','" & V_Trans_Date & "','" & V_Trans_Type & "','" & V_API_Call_Type & "','" & V_Access_Mode & "','" & V_Bank_Name & "','" & V_Bank_iin & "','" & V_Aadhar_Pay_Ref_ID & "','" & V_Mobile_No & "','" & V_latitude & "','" & V_longitude & "','" & V_Aadhar_Last_No & "','" & V_Remarks & "','" & V_Date_added & "','" & V_Cash_Withdraw & "'," & V_Commission & "," & V_TDS & ",'" & V_API_status & "','" & V_CompanyCode & "','" & V_RecordDateTime & "','" & V_UpdatedOn & "','" & V_UpdatedBy & "');"

                    '//// Credit AEPS Amount Admin To Retailer
                    Dim VTransferFrom, VTransferTo, VRemark, V_Amt_Transfer_TransID, VTransferFromMsg, VTransferToMsg As String
                    V_Amt_Transfer_TransID = GV.FL.getAutoNumber("TransId")
                    VTransferFromMsg = "Your Wallet is Debited by Retailer (" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & ")"
                    VTransferToMsg = "Your Wallet is Credited by Admin Due to AEPS."
                    VRemark = "AEPS Cash Withdraw"
                    VTransferFrom = "Admin"
                    VTransferTo = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    insrtQry = insrtQry & " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & V_Cash_Withdraw & "','" & V_referenceno & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & V_API_Call_Type & "','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & V_Cash_Withdraw & "',getdate(),'" & V_UpdatedBy & "','" & V_UpdatedOn & "' ) ;"
                    '//// End Credit Aadhar Pay Amount Admin To Retailer


                    Dim VAdmin_CommissionType, V_SAdmin_CommissionType As String
                    VAdmin_CommissionType = ""
                    V_SAdmin_CommissionType = ""
                    Dim VAdmin_Commission, V_SAdmin_Commission As Decimal
                    VAdmin_Commission = 0
                    V_SAdmin_Commission = 0
                    Dim DistributorComAmt, SubDIsComAmt, VRetailerComAmt As Decimal
                    DistributorComAmt = 0
                    SubDIsComAmt = 0
                    VRetailerComAmt = 0

                    '//// Commission Calculation Super admin to  Admin
                    Dim qry As String = ""
                    qry = " select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA where (" & V_Amount & ">=FromAmount and  " & V_Amount & "<ToAmount) and APIName='AEPS' and AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'; "
                    DS = New DataSet
                    DS = GV.FL.OpenDsWithSelectQuery(qry)
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If DS.Tables(0).Rows.Count > 0 Then

                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Admin_CommissionType")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Admin_CommissionType").ToString() = "" Then
                                        VAdmin_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Admin_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Admin_Commission")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Admin_Commission").ToString() = "" Then
                                        VAdmin_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Admin_Commission").ToString())
                                    End If
                                End If

                                If VAdmin_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VAdmin_Commission = Math.Round(((V_Amount * VAdmin_Commission) / 100), 2)
                                ElseIf VAdmin_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VAdmin_Commission = (VAdmin_Commission)
                                Else
                                    VAdmin_Commission = 0
                                End If
                                '//// End Admin Commission
                            End If
                        End If
                    End If
                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                    V_Actual_Commission_Amt = 0
                    V_GSTAmt = 0
                    V_Commission_Without_GST = 0
                    V_TDS_Amt = 0
                    V_Net_Commission_Amt = 0

                    If VAdmin_Commission > 0 Then
                        V_Actual_Commission_Amt = VAdmin_Commission
                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                        VAdmin_Commission = V_Net_Commission_Amt


                        Dim Ret_Id, SDtypecommFrom1, SDtypecommTo1 As String
                        VerifyServiceCharge = VAdmin_Commission
                        Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                        Ret_Id = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                        SDtypecommFrom1 = "Your Account is debited by Commission " & VerifyServiceCharge & " Rs. Due to AEPS Cash Withdrawal on RegID " & Ret_Id & "."
                        SDtypecommTo1 = "Your Account is credited by Commission " & VerifyServiceCharge & " Rs. Due to AEPS Cash Withdrawal on RegID " & Ret_Id & " ."
                        insrtQry = insrtQry & " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & V_Actual_Commission_Amt & "', '" & GV.parseString(V_referenceno) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Commission','AEPS','" & Now.Date & "','Super Admin','ADMIN','" & VAdmin_Commission & "',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate() ) ;"
                    End If
                    '//// Commission Calculation Super admin to  Admin
                    Dim VCommissionType, VSub_Dis_CommissionType, VRetailer_CommissionType As String
                    VCommissionType = ""
                    VSub_Dis_CommissionType = ""
                    VRetailer_CommissionType = ""
                    Dim VCommission, VSub_Dis_Commission, VRetailer_Commission As Decimal
                    VCommission = 0
                    VSub_Dis_Commission = 0
                    VRetailer_Commission = 0

                    qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & V_Amount & ">=FromAmount and  " & V_Amount & "<ToAmount) and APIName='AEPS'; "

                    DS = New DataSet
                    DS = GV.FL.OpenDsWithSelectQuery(qry)
                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then
                            If DS.Tables(0).Rows.Count > 0 Then

                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Dis_CommissionType")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Dis_CommissionType").ToString() = "" Then
                                        VCommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Dis_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Dis_Commission")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Dis_Commission").ToString() = "" Then
                                        VCommission = GV.parseString(DS.Tables(0).Rows(0).Item("Dis_Commission").ToString())
                                    End If
                                End If

                                If VCommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    DistributorComAmt = Math.Round(((V_Amount * VCommission) / 100), 2)
                                ElseIf VCommissionType.Trim.ToUpper = "AMOUNT" Then
                                    DistributorComAmt = (VCommission)
                                End If


                                '/////// End Distributor


                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString() = "" Then
                                        VSub_Dis_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString() = "" Then
                                        VSub_Dis_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Sub_Dis_Commission").ToString())
                                    End If
                                End If

                                If VSub_Dis_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    SubDIsComAmt = Math.Round(((V_Amount * VSub_Dis_Commission) / 100), 2)
                                ElseIf VSub_Dis_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    SubDIsComAmt = (VSub_Dis_Commission)
                                End If

                                '/////// End  Sub Distributor


                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_CommissionType")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString() = "" Then
                                        VRetailer_CommissionType = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_CommissionType").ToString())
                                    End If
                                End If

                                If Not IsDBNull(DS.Tables(0).Rows(0).Item("Retailer_Commission")) Then
                                    If Not DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString() = "" Then
                                        VRetailer_Commission = GV.parseString(DS.Tables(0).Rows(0).Item("Retailer_Commission").ToString())
                                    End If
                                End If

                                If VRetailer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                    VRetailerComAmt = Math.Round(((V_Amount * VRetailer_Commission) / 100), 2)
                                ElseIf VRetailer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                    VRetailerComAmt = (VRetailer_Commission)
                                End If

                                '/////// End  Retailer

                                ' DistributorComAmt
                                'SubDIsComAmt
                                ' VRetailerComAmt


                            End If
                        End If
                    End If

                    Dim SubDisID As String = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "'")
                    Dim DisID As String = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "'")
                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DistributorComAmt & " Rs. Due to AEPS cash Withdrawal By RegID " & RetailerID & " / AMT " & V_Amount & "."
                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SubDIsComAmt & " Rs. Due to AEPS cash Withdrawal By RegID " & RetailerID & " / AMT " & V_Amount & "."
                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & VRetailerComAmt & " Rs. Due to AEPS cash Withdrawal By RegID " & RetailerID & " / AMT " & V_Amount & "."

                    Dim DistypecommTo As String = "Your Account is credited by commission " & DistributorComAmt & " Rs. Due to AEPS cash Withdrawal By RegID " & RetailerID & " / AMT " & V_Amount & "."
                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SubDIsComAmt & " Rs. Due to AEPS cash Withdrawal By RegID " & RetailerID & " / AMT " & V_Amount & "."
                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & VRetailerComAmt & " Rs. Due to AEPS cash Withdrawal By RegID " & RetailerID & " / AMT " & V_Amount & "."

                    If DistributorComAmt > 0 Then
                        Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                        V_Actual_Commission_Amt = DistributorComAmt
                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                        DistributorComAmt = V_Net_Commission_Amt
                        insrtQry = insrtQry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & V_Actual_Commission_Amt & "', '" & V_referenceno & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & "AEPS" & "','" & Now.Date & "','" & "ADMIN" & "','" & DisID & "','" & DistributorComAmt & "',getdate(),'admin',getdate()) ;"
                    End If
                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                    '//// Distributor Commission Calculation - End

                    '//// SUB Distributor Commission Calculation - Start
                    V_Actual_Commission_Amt = 0
                    V_GSTAmt = 0
                    V_Commission_Without_GST = 0
                    V_TDS_Amt = 0
                    V_Net_Commission_Amt = 0

                    If SubDIsComAmt > 0 Then
                        Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                        V_Actual_Commission_Amt = SubDIsComAmt
                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                        SubDIsComAmt = V_Net_Commission_Amt
                        insrtQry = insrtQry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & V_Actual_Commission_Amt & "', '" & V_referenceno & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & "AEPS" & "','" & Now.Date & "','" & "ADMIN" & "','" & SubDisID & "','" & SubDIsComAmt & "',getdate(),'admin',getdate() ) ;"
                    End If
                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                    '//// SUB Distributor Commission Calculation - End

                    '//// Retailer Commission Calculation - Start
                    V_Actual_Commission_Amt = 0
                    V_GSTAmt = 0
                    V_Commission_Without_GST = 0
                    V_TDS_Amt = 0
                    V_Net_Commission_Amt = 0

                    If VRetailerComAmt > 0 Then
                        Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                        V_Actual_Commission_Amt = VRetailerComAmt
                        V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                        V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                        V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                        V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                        VRetailerComAmt = V_Net_Commission_Amt
                        insrtQry = insrtQry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "','" & V_Actual_Commission_Amt & "','" & V_referenceno & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & "AEPS" & "','" & Now.Date & "','" & "ADMIN" & "','" & RetailerID & "','" & VRetailerComAmt & "',getdate(),'admin',getdate() ) ;"
                    End If

                    '//// Retailer Commission Calculation - END

                    ' DistributorComAmt
                    'SubDIsComAmt
                    ' VRetailerComAmt


                    Dim Result As Boolean = False
                    If Not insrtQry.Trim = "" Then
                        Result = GV.FL.DMLQueriesBulk(insrtQry)
                    End If
                    Exit Sub
                Else
                    'Status False
                    'Fail {"response_code":2,"message":"Customer ","errorcode":10004,"clientrefno":"50936","bankrrn":"","ackno":30301971,"status":false,"bankiin":"508534"}

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("status")
                    Dim dc3 As DataColumn = New DataColumn("message")
                    Dim dc4 As DataColumn = New DataColumn("errorcode")
                    Dim dc5 As DataColumn = New DataColumn("clientrefno")
                    Dim dc6 As DataColumn = New DataColumn("bankrrn")
                    Dim dc7 As DataColumn = New DataColumn("ackno")
                    Dim dc8 As DataColumn = New DataColumn("bankiin")
                    Dim dc9 As DataColumn = New DataColumn("response_code")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)
                    dt.Columns.Add(dc6)
                    dt.Columns.Add(dc7)
                    dt.Columns.Add(dc8)
                    dt.Columns.Add(dc9)

                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = "1"
                    If data.ContainsKey("status") Then
                        dr1(1) = data("status").ToString
                    Else
                        dr1(1) = ""
                    End If

                    If data.ContainsKey("message") Then
                        dr1(2) = data("message").ToString
                    Else
                        dr1(2) = ""
                    End If
                    If data.ContainsKey("errorcode") Then
                        dr1(3) = data("errorcode").ToString
                    Else
                        dr1(3) = ""
                    End If

                    If data.ContainsKey("clientrefno") Then
                        dr1(4) = data("clientrefno").ToString
                    Else
                        dr1(4) = ""
                    End If

                    If data.ContainsKey("bankrrn") Then
                        dr1(5) = data("bankrrn").ToString
                    Else
                        dr1(5) = ""
                    End If

                    If data.ContainsKey("ackno") Then
                        dr1(6) = data("ackno").ToString
                    Else
                        dr1(6) = ""
                    End If

                    If data.ContainsKey("bankiin") Then
                        dr1(7) = data("bankiin").ToString
                    Else
                        dr1(7) = ""
                    End If

                    If data.ContainsKey("response_code") Then
                        dr1(8) = data("response_code").ToString
                    Else
                        dr1(8) = ""
                    End If


                    dt.Rows.Add(dr1)

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()

                    Exit Sub
                End If



            ElseIf V_transactiontype.Trim.ToString.ToUpper = "BE" Then

                Dim setParameter_API_Obj As New Enquiry_API_Parameters()
                setParameter_API_Obj.latitude = V_latitude
                setParameter_API_Obj.longitude = V_longitude
                setParameter_API_Obj.mobilenumber = V_mobilenumber
                setParameter_API_Obj.referenceno = V_referenceno
                setParameter_API_Obj.accessmodetype = V_accessmodetype
                setParameter_API_Obj.adhaarnumber = V_adhaarnumber
                setParameter_API_Obj.data = V_data
                setParameter_API_Obj.ipaddress = V_ipaddress
                setParameter_API_Obj.is_iris = V_is_iris
                setParameter_API_Obj.nationalbankidentification = V_nationalbankidentification
                setParameter_API_Obj.pipe = V_pipe
                setParameter_API_Obj.transactiontype = V_transactiontype
                setParameter_API_Obj.timestamp = V_timestamp
                setParameter_API_Obj.submerchantid = V_submerchantid
                setParameter_API_Obj.requestremarks = V_requestremarks

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "1906ac7e46c3b83f", "da4e5fa6feb74109")
                Dim setParameter_Databody_Obj As New dataBody()
                setParameter_Databody_Obj.body = ssss
                Dim strBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_Databody_Obj)
                APIResult = ReadbyRestClient_NEW(Enquiry_API_URL, strBody)


                Dim jss As New Script.Serialization.JavaScriptSerializer()
                Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(APIResult)


                If data("status") = True Then

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Status")
                    Dim dc3 As DataColumn = New DataColumn("balanceamount")
                    Dim dc4 As DataColumn = New DataColumn("message")
                    Dim dc5 As DataColumn = New DataColumn("ackno")
                    Dim dc6 As DataColumn = New DataColumn("bankiin")
                    Dim dc7 As DataColumn = New DataColumn("clientrefno")
                    Dim dc8 As DataColumn = New DataColumn("errorcode")
                    Dim dc9 As DataColumn = New DataColumn("response_code")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)
                    dt.Columns.Add(dc6)
                    dt.Columns.Add(dc7)
                    dt.Columns.Add(dc8)
                    dt.Columns.Add(dc9)


                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = "1"

                    If data.ContainsKey("status") Then
                        dr1(1) = data("status").ToString
                    Else
                        dr1(1) = ""
                    End If

                    If data.ContainsKey("balanceamount") Then
                        dr1(2) = data("balanceamount").ToString
                    Else
                        dr1(2) = ""
                    End If

                    If data.ContainsKey("message") Then
                        dr1(3) = data("message").ToString
                    Else
                        dr1(3) = ""
                    End If


                    If data.ContainsKey("ackno") Then
                        dr1(4) = data("ackno").ToString
                    Else
                        dr1(4) = ""
                    End If


                    If data.ContainsKey("bankiin") Then
                        dr1(5) = data("bankiin").ToString
                    Else
                        dr1(5) = ""
                    End If



                    If data.ContainsKey("clientrefno") Then
                        dr1(6) = data("clientrefno").ToString
                    Else
                        dr1(6) = ""
                    End If

                    If data.ContainsKey("errorcode") Then
                        dr1(7) = data("errorcode").ToString
                    Else
                        dr1(7) = ""
                    End If


                    If data.ContainsKey("response_code") Then
                        dr1(8) = data("response_code").ToString
                    Else
                        dr1(8) = ""
                    End If


                    dt.Rows.Add(dr1)

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()


                    Dim insrtQry As String = ""
                    '//// Deduct Service Charge Retailer to Admin
                    Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                    Dim V_DeductAmt As String = AEPS_BALANCE_CHECK_CHARGE
                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & V_DeductAmt & " Rs. Due to AEPS BALANCE CHECK."
                    Dim VTo As String = "Your Account is credited by ServiceCharge " & V_DeductAmt & " Rs. Due to AEPS BALANCE CHECK."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & V_referenceno & "','" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(vTransID) & "','" & V_DeductAmt & "','" & V_referenceno & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & "Admin" & "','" & V_DeductAmt & "',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate() ) ;"
                    '//// End Service Charge Deduction - Retailer T0 Admin

                    '//// Service Charge Deduction - Admin To Superadmin
                    Dim Ret_Id, SDtypecommFrom1, SDtypecommTo1 As String
                    VerifyServiceCharge = AEPS_BALANCE_CHECK_CHARGE
                    vTransID = GV.FL.getAutoNumber("TransId")
                    Ret_Id = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    SDtypecommFrom1 = "Your Account is debited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to AEPS BALANCE CHECK on RegID " & Ret_Id & "."
                    SDtypecommTo1 = "Your Account is credited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to AEPS BALANCE CHECK on RegID " & Ret_Id & " ."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn,Ref_TransID) values('" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & V_referenceno & "','" & VerifyServiceCharge & "','" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Service Charge','Service Charge',getdate(),'ADMIN','Super Admin','" & VerifyServiceCharge & "',getdate(),'" & Ret_Id & "',getdate(),'" & V_referenceno & "') ;"
                    '//// End Service Charge Deduction - Admin To Superadmin

                    Dim Result As Boolean = False
                    Result = GV.FL.DMLQueriesBulk(insrtQry)

                    Exit Sub
                Else
                    'Status False

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Status")
                    Dim dc3 As DataColumn = New DataColumn("message")
                    Dim dc4 As DataColumn = New DataColumn("ackno")
                    Dim dc5 As DataColumn = New DataColumn("bankiin")
                    Dim dc6 As DataColumn = New DataColumn("clientrefno")
                    Dim dc7 As DataColumn = New DataColumn("errorcode")
                    Dim dc8 As DataColumn = New DataColumn("response_code")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)
                    dt.Columns.Add(dc6)
                    dt.Columns.Add(dc7)
                    dt.Columns.Add(dc8)



                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = "1"
                    If data.ContainsKey("status") Then
                        dr1(1) = data("status").ToString
                    Else
                        dr1(1) = ""
                    End If

                    If data.ContainsKey("message") Then
                        dr1(2) = data("message").ToString
                    Else
                        dr1(2) = ""
                    End If


                    If data.ContainsKey("ackno") Then
                        dr1(3) = data("ackno").ToString
                    Else
                        dr1(3) = ""
                    End If


                    If data.ContainsKey("bankiin") Then
                        dr1(4) = data("bankiin").ToString
                    Else
                        dr1(4) = ""
                    End If



                    If data.ContainsKey("clientrefno") Then
                        dr1(5) = data("clientrefno").ToString
                    Else
                        dr1(5) = ""
                    End If



                    If data.ContainsKey("errorcode") Then
                        dr1(6) = data("errorcode").ToString
                    Else
                        dr1(6) = ""
                    End If


                    If data.ContainsKey("response_code") Then
                        dr1(7) = data("response_code").ToString
                    Else
                        dr1(7) = ""
                    End If
                    dt.Rows.Add(dr1)

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()

                    Exit Sub
                End If

            ElseIf V_transactiontype.Trim.ToString.ToUpper = "MS" Then
                Dim setParameter_API_Obj As New Mini_Statement_API_Parameters()

                setParameter_API_Obj.latitude = V_latitude
                setParameter_API_Obj.longitude = V_longitude
                setParameter_API_Obj.mobilenumber = V_mobilenumber
                setParameter_API_Obj.referenceno = V_referenceno
                setParameter_API_Obj.accessmodetype = V_accessmodetype
                setParameter_API_Obj.adhaarnumber = V_adhaarnumber
                setParameter_API_Obj.data = V_data
                setParameter_API_Obj.ipaddress = V_ipaddress
                setParameter_API_Obj.is_iris = V_is_iris
                setParameter_API_Obj.nationalbankidentification = V_nationalbankidentification
                setParameter_API_Obj.pipe = V_pipe
                setParameter_API_Obj.transactiontype = V_transactiontype
                setParameter_API_Obj.timestamp = V_timestamp
                setParameter_API_Obj.submerchantid = V_submerchantid
                setParameter_API_Obj.requestremarks = V_requestremarks

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "1906ac7e46c3b83f", "da4e5fa6feb74109")
                Dim setParameter_Databody_Obj As New dataBody()
                setParameter_Databody_Obj.body = ssss
                Dim strBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_Databody_Obj)
                APIResult = ReadbyRestClient_NEW(Mini_Statement_API_URL, strBody)

                'Dim ss As String = "{""status"":true,""ackno"":13311810,""datetime"":""7\/20\/2022 5:15:33 PM"",""balanceamount"":""14412.64"",""bankrrn"":""220117034388"",""bankiin"":""508505"",""message"":""Mini statement has been generated successfully."",""errorcode"":""0"",""ministatement"":[{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/93755167""},{""date"":""20\/07"",""amount"":100,""txnType"":""D"",""narration"":""FIG\/F\/93597279""},{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/93033302""},{""date"":""20\/07"",""amount"":100,""txnType"":""D"",""narration"":""FIG\/F\/92998016""},{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/92965805""},{""date"":""20\/07"",""amount"":3.84,""txnType"":""D"",""narration"":""FIG\/F\/92957139""}],""ministatementlist"":[],""response_code"":1,""last_aadhar"":""9694"",""name"":""JYOTI"",""clientrefno"":""29469""}"

                '{"status":true,"ackno":13311810,"datetime":"7\/20\/2022 5:15:33 PM","balanceamount":"14412.64","bankrrn":"220117034388","bankiin":"508505","message":"Mini statement has been generated successfully.","errorcode":"0","ministatement":[{"date":"20\/07","amount":3.84,"txnType":"D","narration":"FIG\/F\/93755167"},{"date":"20\/07","amount":100,"txnType":"D","narration":"FIG\/F\/93597279"},{"date":"20\/07","amount":3.84,"txnType":"D","narration":"FIG\/F\/93033302"},{"date":"20\/07","amount":100,"txnType":"D","narration":"FIG\/F\/92998016"},{"date":"20\/07","amount":3.84,"txnType":"D","narration":"FIG\/F\/92965805"},{"date":"20\/07","amount":3.84,"txnType":"D","narration":"FIG\/F\/92957139"}],"ministatementlist":[],"response_code":1,"last_aadhar":"9694","name":"JYOTI","clientrefno":"29469"}

                Dim json1 As JObject = JObject.Parse(APIResult)
                Dim aa As JToken = json1.SelectToken("ministatement")


                If json1.SelectToken("status").ToString.Trim = True Then    'ministatement
                    'Dim fData As String = data("ministatement").ToString
                    'fData = fData.Replace("[", "").Replace("]", "")

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("date")
                    Dim dc3 As DataColumn = New DataColumn("txnType")
                    Dim dc4 As DataColumn = New DataColumn("amount")
                    Dim dc5 As DataColumn = New DataColumn("narration")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)

                    Dim scnt As Integer = 1
                    Dim data12 As List(Of JToken) = aa.Children().ToList
                    For Each msg As JObject In data12
                        Dim dr1 As DataRow = dt.NewRow()
                        dr1(0) = scnt
                        dr1(1) = msg("date")
                        dr1(2) = msg("txnType")
                        dr1(3) = msg("amount")
                        dr1(4) = msg("narration")
                        dt.Rows.Add(dr1)
                        scnt += 1
                        'For Each p In msg
                        '    'jj = jj & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString
                        '    'jj = msg("date").ToString & " " & msg("amount").ToString & " " & msg("txnType").ToString & " " & msg("narration").ToString
                        'Next
                    Next

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()

                    Dim insrtQry As String = ""
                    '//// Deduct Service Charge Retailer to Admin
                    Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                    Dim V_DeductAmt As String = AEPS_MINI_STATEMENT_CHARGE
                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & V_DeductAmt & " Rs. Due to Mini Statement Generate."
                    Dim VTo As String = "Your Account is credited by ServiceCharge " & V_DeductAmt & " Rs. Due to Mini Statement Generate."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & V_referenceno & "','" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(vTransID) & "','" & V_DeductAmt & "','" & V_referenceno & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & "Admin" & "','" & V_DeductAmt & "',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate() ) ;"
                    '//// End Service Charge Deduction - Retailer T0 Admin

                    '//// Service Charge Deduction - Admin To Superadmin
                    Dim Ret_Id, SDtypecommFrom1, SDtypecommTo1 As String
                    VerifyServiceCharge = AEPS_MINI_STATEMENT_CHARGE
                    vTransID = GV.FL.getAutoNumber("TransId")
                    Ret_Id = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    SDtypecommFrom1 = "Your Account is debited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Mini Statement Generate on RegID " & Ret_Id & "."
                    SDtypecommTo1 = "Your Account is credited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Mini Statement Generate on RegID " & Ret_Id & " ."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn,Ref_TransID) values('" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & V_referenceno & "','" & VerifyServiceCharge & "','" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Service Charge','Service Charge',getdate(),'ADMIN','Super Admin','" & VerifyServiceCharge & "',getdate(),'" & Ret_Id & "',getdate(),'" & V_referenceno & "') ;"
                    '//// End Service Charge Deduction - Admin To Superadmin

                    Dim Result As Boolean = False
                    Result = GV.FL.DMLQueriesBulk(insrtQry)

                    Exit Sub


                Else

                    'false
                    Dim jss As New Script.Serialization.JavaScriptSerializer()
                    Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(APIResult)

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Status")
                    Dim dc3 As DataColumn = New DataColumn("message")
                    Dim dc4 As DataColumn = New DataColumn("errorcode")
                    Dim dc5 As DataColumn = New DataColumn("response_code")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)

                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = "1"
                    If data.ContainsKey("status") Then
                        dr1(1) = data("status").ToString
                    Else
                        dr1(1) = ""
                    End If

                    If data.ContainsKey("message") Then
                        dr1(2) = data("message").ToString
                    Else
                        dr1(2) = ""
                    End If
                    If data.ContainsKey("errorcode") Then
                        dr1(3) = data("errorcode").ToString
                    Else
                        dr1(3) = ""
                    End If

                    If data.ContainsKey("response_code") Then
                        dr1(4) = data("response_code").ToString
                    Else
                        dr1(4) = ""
                    End If

                    dt.Rows.Add(dr1)

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()

                    Exit Sub

                End If



            ElseIf V_transactiontype.Trim.ToString.ToUpper = "M" Then
                Dim setParameter_API_Obj As New aadhar_Pay_API_Parameters()
                setParameter_API_Obj.latitude = V_latitude
                setParameter_API_Obj.longitude = V_longitude
                setParameter_API_Obj.mobilenumber = V_mobilenumber
                setParameter_API_Obj.referenceno = V_referenceno
                setParameter_API_Obj.accessmodetype = V_accessmodetype
                setParameter_API_Obj.adhaarnumber = V_adhaarnumber
                setParameter_API_Obj.data = V_data
                setParameter_API_Obj.ipaddress = V_ipaddress
                setParameter_API_Obj.is_iris = V_is_iris
                setParameter_API_Obj.nationalbankidentification = V_nationalbankidentification
                setParameter_API_Obj.pipe = V_pipe
                setParameter_API_Obj.transactionType = V_transactiontype
                setParameter_API_Obj.timestamp = V_timestamp
                setParameter_API_Obj.submerchantid = V_submerchantid
                setParameter_API_Obj.amount = V_Amount
                setParameter_API_Obj.requestremarks = V_requestremarks

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "1906ac7e46c3b83f", "da4e5fa6feb74109")
                Dim setParameter_Databody_Obj As New dataBody()
                setParameter_Databody_Obj.body = ssss
                Dim strBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_Databody_Obj)
                APIResult = ReadbyRestClient_NEW(aadhar_Pay_API_URL, strBody)

                Dim jss As New Script.Serialization.JavaScriptSerializer()
                Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(APIResult)


                If data("status") = True Then

                    Dim V_Trans_ID, V_Trans_Date, V_Trans_Type, V_API_Call_Type, V_Access_Mode, V_Bank_Name, V_Bank_iin, V_Aadhar_Pay_Ref_ID, V_Mobile_No, V_Aadhar_Last_No, V_Remarks, V_Date_added, V_Cash_Withdraw, V_Commission, V_TDS, V_API_status, V_CompanyCode, V_RecordDateTime, V_UpdatedOn, V_UpdatedBy As String
                    V_Trans_ID = V_referenceno
                    V_Trans_Date = Now.Date
                    V_Trans_Type = ddlTransactionType.SelectedItem.Text
                    V_API_Call_Type = ddlAPI_Call.SelectedValue
                    V_Access_Mode = "SITE"
                    V_Bank_Name = ddlBankList.SelectedItem.Text
                    V_latitude = V_latitude
                    V_longitude = V_longitude
                    V_Bank_iin = ""
                    V_Aadhar_Pay_Ref_ID = ""
                    V_Aadhar_Last_No = ""
                    V_Remarks = ""
                    V_Cash_Withdraw = "0"
                    V_Commission = "0"
                    V_TDS = "0"
                    V_API_status = data("status").ToString
                    V_CompanyCode = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                    V_RecordDateTime = V_timestamp
                    V_UpdatedOn = V_timestamp
                    V_UpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    V_Mobile_No = txtMobileNumber.Text
                    V_Date_added = V_timestamp

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("Status")
                    Dim dc3 As DataColumn = New DataColumn("ackno")
                    Dim dc4 As DataColumn = New DataColumn("balanceamount")
                    Dim dc5 As DataColumn = New DataColumn("amount")
                    Dim dc6 As DataColumn = New DataColumn("message")
                    Dim dc7 As DataColumn = New DataColumn("bankiin")
                    Dim dc8 As DataColumn = New DataColumn("bankrrn")
                    Dim dc9 As DataColumn = New DataColumn("clientrefno")
                    Dim dc10 As DataColumn = New DataColumn("response")
                    Dim dc11 As DataColumn = New DataColumn("errorcode")
                    Dim dc12 As DataColumn = New DataColumn("last_aadhar")
                    Dim dc13 As DataColumn = New DataColumn("name")
                    Dim dc14 As DataColumn = New DataColumn("response_code")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)
                    dt.Columns.Add(dc5)
                    dt.Columns.Add(dc6)
                    dt.Columns.Add(dc7)
                    dt.Columns.Add(dc8)
                    dt.Columns.Add(dc9)
                    dt.Columns.Add(dc10)
                    dt.Columns.Add(dc11)
                    dt.Columns.Add(dc12)
                    dt.Columns.Add(dc13)
                    dt.Columns.Add(dc14)


                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = "1"

                    If data.ContainsKey("status") Then
                        dr1(1) = data("status").ToString
                    Else
                        dr1(1) = ""
                    End If

                    If data.ContainsKey("ackno") Then
                        dr1(2) = data("ackno").ToString
                    Else
                        dr1(2) = ""
                    End If

                    If data.ContainsKey("balanceamount") Then
                        dr1(3) = data("balanceamount").ToString
                    Else
                        dr1(3) = ""
                    End If
                    If data.ContainsKey("amount") Then
                        dr1(4) = data("amount").ToString
                        V_Cash_Withdraw = data("amount").ToString
                    Else
                        dr1(4) = ""
                        V_Cash_Withdraw = "0"
                    End If

                    If data.ContainsKey("message") Then
                        dr1(5) = data("message").ToString
                        V_Remarks = data("message").ToString
                    Else
                        dr1(5) = ""
                        V_Remarks = ""
                    End If

                    If data.ContainsKey("bankiin") Then
                        dr1(6) = data("bankiin").ToString
                        V_Bank_iin = data("bankiin").ToString
                    Else
                        dr1(6) = ""
                        V_Bank_iin = ""
                    End If
                    If data.ContainsKey("bankrrn") Then
                        dr1(7) = data("bankrrn").ToString
                    Else
                        dr1(7) = ""
                    End If

                    If data.ContainsKey("clientrefno") Then
                        dr1(8) = data("clientrefno").ToString
                    Else
                        dr1(8) = ""
                    End If

                    If data.ContainsKey("response") Then
                        dr1(9) = data("response").ToString
                    Else
                        dr1(9) = ""
                    End If

                    If data.ContainsKey("errorcode") Then
                        dr1(10) = data("errorcode").ToString
                    Else
                        dr1(10) = ""
                    End If

                    If data.ContainsKey("last_aadhar") Then
                        dr1(11) = data("last_aadhar").ToString
                        V_Aadhar_Last_No = data("last_aadhar").ToString
                    Else
                        dr1(11) = ""
                        V_Aadhar_Last_No = ""
                    End If

                    If data.ContainsKey("name") Then
                        dr1(12) = data("name").ToString
                    Else
                        dr1(12) = ""
                    End If

                    If data.ContainsKey("response_code") Then
                        dr1(13) = data("response_code").ToString
                    Else
                        dr1(13) = ""
                    End If

                    dt.Rows.Add(dr1)

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()


                    If GV.parseString(V_Commission) = "" Then
                        V_Commission = "0"
                    End If

                    If GV.parseString(V_TDS) = "" Then
                        V_TDS = "0"
                    End If




                    Dim insrtQry As String = "INSERT INTO  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_AEPS_API([Trans_ID],[Trans_Date],[Trans_Type],[API_Call_Type],[Access_Mode],[Bank_Name],[Bank_iin],[Aadhar_Pay_Ref_ID],[Mobile_No],[Latitude],[Longitude],[Aadhar_Last_No],[Remarks],[Date_added],[Cash_Withdraw],[Commission],[TDS],[API_status],[CompanyCode],[RecordDateTime],[UpdatedOn],[UpdatedBy])     VALUES('" & V_Trans_ID & "','" & V_Trans_Date & "','" & V_Trans_Type & "','" & V_API_Call_Type & "','" & V_Access_Mode & "','" & V_Bank_Name & "','" & V_Bank_iin & "','" & V_Aadhar_Pay_Ref_ID & "','" & V_Mobile_No & "','" & V_latitude & "','" & V_longitude & "','" & V_Aadhar_Last_No & "','" & V_Remarks & "','" & V_Date_added & "','" & V_Cash_Withdraw & "'," & V_Commission & "," & V_TDS & ",'" & V_API_status & "','" & V_CompanyCode & "','" & V_RecordDateTime & "','" & V_UpdatedOn & "','" & V_UpdatedBy & "');"

                    '//// Credit Aadhar Pay Amount Admin To Retailer
                    Dim VTransferFrom, VTransferTo, VRemark, V_Amt_Transfer_TransID, VTransferFromMsg, VTransferToMsg As String
                    V_Amt_Transfer_TransID = GV.FL.getAutoNumber("TransId")
                    VTransferFromMsg = "Your Wallet is Debited by Retailer (" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & ")"
                    VTransferToMsg = "Your Wallet is Credited by Admin Due to Aadhar Pay."
                    VRemark = "Aadhar Pay Cash Withdraw"
                    VTransferFrom = "Admin"
                    VTransferTo = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    insrtQry = insrtQry & " insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & V_Amt_Transfer_TransID.Trim & "','" & V_Cash_Withdraw & "','" & V_referenceno & "','" & V_Amt_Transfer_TransID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','" & V_API_Call_Type & "','" & VRemark & "','" & Now.Date & "','" & VTransferFrom & "','" & VTransferTo & "','" & V_Cash_Withdraw & "',getdate(),'" & V_UpdatedBy & "','" & V_UpdatedOn & "' ) ;"
                    '//// End Credit Aadhar Pay Amount Admin To Retailer

                    '//// Deduct Service Charge Retailer to Admin
                    Dim NetAmount As Decimal = 0
                    Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " BosCenter_DB.dbo.BOS_ProductServiceVsAdmin_SA where Title='Adhar pay' and AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'").Split(":")
                    If Service.Length > 1 Then
                        If Service(1).Trim = "Percentage" Then
                            NetAmount = (CDec(V_Cash_Withdraw) * CDec(Service(0))) / 100
                        ElseIf Service(1).Trim = "Amount" Then
                            NetAmount = CDec(Service(0))
                        ElseIf Service(1).Trim = "Not Applicable" Then
                            NetAmount = CDec(Service(0))
                        End If
                    End If
                    Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                    Dim V_DeductAmt As String = Math.Round(NetAmount, 2)
                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & V_DeductAmt & " Rs. Due to Aadhar Pay / AMT " & V_Cash_Withdraw & "."
                    Dim VTo As String = "Your Account is credited by ServiceCharge " & V_DeductAmt & " Rs. Due to Aadhar Pay / AMT " & V_Cash_Withdraw & "."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & V_referenceno & "','" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(vTransID) & "','" & V_Cash_Withdraw & "','" & V_referenceno & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & VTransferTo & "','" & VTransferFrom & "','" & V_DeductAmt & "',getdate(),'" & V_UpdatedBy & "',getdate() ) ;"
                    '//// End Service Charge Deduction - Retailer T0 Admin

                    '//// Service Charge Deduction - Admin To Superadmin
                    Dim Ret_Id, SDtypecommFrom1, SDtypecommTo1 As String
                    NetAmount = 0
                    Service = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " BosCenter_DB.dbo.BOS_ProductServiceMaster_SA where Title='Adhar pay'").Split(":")
                    If Service.Length > 1 Then
                        If Service(1).Trim = "Percentage" Then
                            NetAmount = (CDec(V_Cash_Withdraw) * CDec(Service(0))) / 100
                        ElseIf Service(1).Trim = "Amount" Then
                            NetAmount = CDec(Service(0))
                        ElseIf Service(1).Trim = "Not Applicable" Then
                            NetAmount = CDec(Service(0))
                        End If
                    End If
                    VerifyServiceCharge = Math.Round(NetAmount, 2)
                    vTransID = GV.FL.getAutoNumber("TransId")
                    Ret_Id = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    SDtypecommFrom1 = "Your Account is debited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Aadhar Pay on RegID " & Ret_Id & "."
                    SDtypecommTo1 = "Your Account is credited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to Aadhar Pay on RegID " & Ret_Id & " ."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn,Ref_TransID) values('" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & V_referenceno & "','" & VerifyServiceCharge & "','" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Service Charge','Service Charge',getdate(),'ADMIN','Super Admin','" & VerifyServiceCharge & "',getdate(),'" & Ret_Id & "',getdate(),'" & V_referenceno & "') ;"
                    '//// End Service Charge Deduction - Admin To Superadmin


                    Dim Result As Boolean = False
                    Result = GV.FL.DMLQueriesBulk(insrtQry)
                    Exit Sub

                Else

                    'Status False
                    '{"response_code":26,"status":false,"message":"Device is alreay mapped with other merchant,Please contact service provider"}

                    Dim dt As New DataTable
                    Dim dc1 As DataColumn = New DataColumn("SrNo")
                    Dim dc2 As DataColumn = New DataColumn("status")
                    Dim dc3 As DataColumn = New DataColumn("message")
                    Dim dc4 As DataColumn = New DataColumn("response_code")

                    dt.Columns.Add(dc1)
                    dt.Columns.Add(dc2)
                    dt.Columns.Add(dc3)
                    dt.Columns.Add(dc4)


                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = "1"
                    If data.ContainsKey("status") Then
                        dr1(1) = data("status").ToString
                    Else
                        dr1(1) = ""
                    End If

                    If data.ContainsKey("message") Then
                        dr1(2) = data("message").ToString
                    Else
                        dr1(2) = ""
                    End If

                    If data.ContainsKey("response_code") Then
                        dr1(3) = data("response_code").ToString
                    Else
                        dr1(3) = ""
                    End If
                    dt.Rows.Add(dr1)

                    grdResponseDetails.DataSource = dt
                    grdResponseDetails.DataBind()

                    Exit Sub
                End If


            End If


            'If btnSave.Text = "Submit" Then
            '    btnPopupYes.Text = "Yes"
            '    btnPopupYes.Attributes("Style") = ""
            '    btnPopupYes.Visible = True
            '    btnCancel.Text = "No"
            '    lblDialogMsg.Text = "Are You sure you want To Submit?"
            '    lblDialogMsg.CssClass = ""
            '    ModalPopupExtender1.Show()
            'Else
            '    btnPopupYes.Text = "Yes"
            '    btnPopupYes.Visible = True

            '    btnCancel.Attributes("Style") = ""
            '    btnCancel.Text = "No"
            '    lblDialogMsg.Text = "Are You sure you want To Update?"
            '    lblDialogMsg.CssClass = ""
            '    ModalPopupExtender1.Show()
            'End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            lblError.Text = APIResult
            lblError.Visible = True
        End Try
    End Sub
    Protected Sub ddlTransactionType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTransactionType.SelectedIndexChanged
        Try

            txtAmount.Text = "0"
            If ddlTransactionType.SelectedValue.Trim.ToUpper = "CW".ToUpper Or ddlTransactionType.SelectedValue.Trim.ToUpper = "M".ToUpper Then
                div_Amount.Visible = True
            Else
                div_Amount.Visible = False
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub ddlAPI_Call_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlAPI_Call.SelectedIndexChanged
        Try

            ddlTransactionType.Items.Clear()
            If ddlAPI_Call.SelectedValue = "AEPS" Then
                ddlTransactionType.Items.Add(New ListItem(":: Select Type ::", ":: Select Type ::"))
                ddlTransactionType.Items.Add(New ListItem("Balance Enquiry", "BE"))
                ddlTransactionType.Items.Add(New ListItem("Cash Withdraw", "CW"))
                ddlTransactionType.Items.Add(New ListItem("Mini Statement", "MS"))
            Else
                ddlTransactionType.Items.Add(New ListItem(":: Select Type ::", ":: Select Type ::"))
                ddlTransactionType.Items.Add(New ListItem("Pay", "M"))
            End If
            ddlTransactionType.SelectedIndex = 0

            ddlTransactionType_SelectedIndexChanged(sender, e)

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub btnOnboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOnboard.Click
        Try

            Dim V_merchantcode, V_mobile, V_is_new, V_email, V_firm, V_callback As String
            V_merchantcode = ""
            V_email = ""
            V_is_new = ""
            V_mobile = ""
            V_firm = ""
            lblError.Text = ""
            lblError.CssClass = ""


            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & LoginID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If
            Dim walletBal As String = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
            If walletBal.Trim = "" Then
                walletBal = "0"
            End If

            If (CDec(walletBal) - CDec(holdAmt)) >= CDec(AEPS_ONBOARD_CHARGE) Then
            Else
                lblError.Text = "You Have Insufficient Wallet Amount"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If


            DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & LoginID & "'")
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("RegistrationId")) Then
                        If Not DS.Tables(0).Rows(0).Item("RegistrationId").ToString() = "" Then
                            V_merchantcode = GV.parseString(DS.Tables(0).Rows(0).Item("RegistrationId").ToString())
                        Else
                            V_merchantcode = ""
                        End If
                    Else
                        V_merchantcode = ""
                    End If
                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmailID")) Then
                        If Not DS.Tables(0).Rows(0).Item("EmailID").ToString() = "" Then
                            V_email = GV.parseString(DS.Tables(0).Rows(0).Item("EmailID").ToString())
                        Else
                            V_email = ""
                        End If
                    Else
                        V_email = ""
                    End If

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("MobileNo")) Then
                        If Not DS.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                            V_mobile = GV.parseString(DS.Tables(0).Rows(0).Item("MobileNo").ToString())
                        Else
                            V_mobile = ""
                        End If
                    Else
                        V_mobile = ""
                    End If

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("AgencyName")) Then
                        If Not DS.Tables(0).Rows(0).Item("AgencyName").ToString() = "" Then
                            V_firm = GV.parseString(DS.Tables(0).Rows(0).Item("AgencyName").ToString())
                        Else
                            V_firm = ""
                        End If
                    Else
                        V_firm = ""
                    End If

                End If
            End If

            V_is_new = "0"
            V_callback = "https://boscenter.in/api/PaySprintAEPSCallbk"


            Dim setParameter_API_Obj As New Merchant_Onboard_API_Parameters
            setParameter_API_Obj.merchantcode = V_merchantcode.Replace("-", "")
            setParameter_API_Obj.mobile = V_mobile
            setParameter_API_Obj.email = V_email
            setParameter_API_Obj.firm = V_firm
            setParameter_API_Obj.is_new = V_is_new
            setParameter_API_Obj.callback = V_callback

            Dim StrParameters As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)

            Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "1906ac7e46c3b83f", "da4e5fa6feb74109")
            Dim setParameter_Databody_Obj As New dataBody()
            setParameter_Databody_Obj.body = ssss
            Dim strBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_Databody_Obj)

            APIResult = ReadbyRestClient_NEW(Merchant_Onboard_API_URL, StrParameters)

            lblError.Text = GV.parseString(APIResult)
            lblError.Visible = True

            Dim jss As New Script.Serialization.JavaScriptSerializer()
            Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(APIResult)

            If data("redirecturl").Contains("http") Then

                Dim AEPS_Onboard_Status As String = GV.FL.AddInVar("AEPS_Onboard_Status", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration  where  RegistrationId = '" & V_merchantcode & "'")
                If AEPS_Onboard_Status.Trim.ToUpper = "NO" Or AEPS_Onboard_Status.Trim.ToUpper = "" Then

                    Dim insrtQry As String = ""
                    Dim V_referenceno As String = GV.FL.getAutoNumber("TransId")

                    insrtQry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set AEPS_Onboard_Status='Yes', AEPS_Onboard_TransID='" & V_referenceno & "' where  RegistrationId = '" & V_merchantcode & "';"

                    '//// Deduct Service Charge Retailer to Admin
                    Dim vTransID As String = GV.FL.getAutoNumber("TransId")
                    Dim V_DeductAmt As String = AEPS_ONBOARD_CHARGE
                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & V_DeductAmt & " Rs. Due to AEPS ONBOARD CHARGES."
                    Dim VTo As String = "Your Account is credited by ServiceCharge " & V_DeductAmt & " Rs. Due to AEPS ONBOARD CHARGES."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & V_referenceno & "','" & GV.parseString(GV.GetIPAddress) & "','" & GV.parseString(vTransID) & "','" & V_DeductAmt & "','" & V_referenceno & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & "Admin" & "','" & V_DeductAmt & "',getdate(),'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "',getdate() ) ;"
                    '//// End Service Charge Deduction - Retailer T0 Admin

                    '//// Service Charge Deduction - Admin To Superadmin
                    Dim Ret_Id, SDtypecommFrom1, SDtypecommTo1 As String
                    VerifyServiceCharge = AEPS_ONBOARD_CHARGE
                    vTransID = GV.FL.getAutoNumber("TransId")
                    Ret_Id = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    SDtypecommFrom1 = "Your Account is debited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to AEPS ONBOARD CHARGES on RegID " & Ret_Id & "."
                    SDtypecommTo1 = "Your Account is credited by ServiceCharge " & VerifyServiceCharge & " Rs. Due to AEPS ONBOARD CHARGES on RegID " & Ret_Id & " ."
                    insrtQry = insrtQry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,Actual_Transaction_Amount,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn,Ref_TransID) values('" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & V_referenceno & "','" & VerifyServiceCharge & "','" & SDtypecommTo1 & "','" & SDtypecommFrom1 & "','Service Charge','Service Charge',getdate(),'ADMIN','Super Admin','" & VerifyServiceCharge & "',getdate(),'" & Ret_Id & "',getdate(),'" & V_referenceno & "') ;"
                    '//// End Service Charge Deduction - Admin To Superadmin

                    Dim Result As Boolean = False
                    Result = GV.FL.DMLQueriesBulk(insrtQry)

                End If


                Response.Redirect(data("redirecturl"))
            End If



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Function chk_Aeps_Onboard_Status() As Boolean
        Dim result As Boolean = False
        Try
            Dim V_merchantcode, V_mobile As String
            V_merchantcode = ""
            V_mobile = ""

            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & LoginID & "'")
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("RegistrationId")) Then
                        If Not DS.Tables(0).Rows(0).Item("RegistrationId").ToString() = "" Then
                            V_merchantcode = GV.parseString(DS.Tables(0).Rows(0).Item("RegistrationId").ToString())
                        Else
                            V_merchantcode = ""
                        End If
                    Else
                        V_merchantcode = ""
                    End If

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("MobileNo")) Then
                        If Not DS.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                            V_mobile = GV.parseString(DS.Tables(0).Rows(0).Item("MobileNo").ToString())
                        Else
                            V_mobile = ""
                        End If
                    Else
                        V_mobile = ""
                    End If
                End If
            End If


            Dim setParameter_API_Obj As New Merchant_Onboard_Status_Chk_API_Parameters()
            setParameter_API_Obj.merchantcode = V_merchantcode.Replace("-", "")
            setParameter_API_Obj.mobile = V_mobile
            setParameter_API_Obj.pipe = "bank1"

            Dim StrParameters As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)

            Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "1906ac7e46c3b83f", "da4e5fa6feb74109")

            Dim setParameter_Databody_Obj As New dataBody()
            setParameter_Databody_Obj.body = ssss
            Dim strBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_Databody_Obj)

            APIResult = ReadbyRestClient_NEW(Merchant_Onboard_Status_Chk_API_URL, StrParameters)

            Dim jss As New Script.Serialization.JavaScriptSerializer()
            Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(APIResult)

            If data("status") = True Then
                '{"response_code":1,"status":true,"message":"Onboarding completed","is_approved":"Accepted"}
                result = True
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return result
        End Try
        Return result
    End Function

    Private Sub btnChkOnBaord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChkOnBaord.Click
        Try

            Dim V_merchantcode, V_mobile As String
            V_merchantcode = ""
            V_mobile = ""

            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & LoginID & "'")
            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("RegistrationId")) Then
                        If Not DS.Tables(0).Rows(0).Item("RegistrationId").ToString() = "" Then
                            V_merchantcode = GV.parseString(DS.Tables(0).Rows(0).Item("RegistrationId").ToString())
                        Else
                            V_merchantcode = ""
                        End If
                    Else
                        V_merchantcode = ""
                    End If

                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("MobileNo")) Then
                        If Not DS.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                            V_mobile = GV.parseString(DS.Tables(0).Rows(0).Item("MobileNo").ToString())
                        Else
                            V_mobile = ""
                        End If
                    Else
                        V_mobile = ""
                    End If
                End If
            End If


            Dim setParameter_API_Obj As New Merchant_Onboard_Status_Chk_API_Parameters()
            setParameter_API_Obj.merchantcode = V_merchantcode.Replace("-", "")
            setParameter_API_Obj.mobile = V_mobile
            setParameter_API_Obj.pipe = "bank1"

            Dim StrParameters As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)

            ' Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "c05085d229a39a7e", "d45195355e9db9a3")
            Dim ssss As String = AEPS_Functions.CryptAESIn(StrParameters, "1906ac7e46c3b83f", "da4e5fa6feb74109")

            Dim setParameter_Databody_Obj As New dataBody()
            setParameter_Databody_Obj.body = ssss
            Dim strBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_Databody_Obj)

            APIResult = ReadbyRestClient_NEW(Merchant_Onboard_Status_Chk_API_URL, StrParameters)

            Dim jss As New Script.Serialization.JavaScriptSerializer()
            Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(APIResult)


            If data("status") = True Then
                '{"response_code":1,"status":true,"message":"Onboarding completed","is_approved":"Accepted"}

                Dim dt As New DataTable
                Dim dc1 As DataColumn = New DataColumn("SrNo")
                Dim dc2 As DataColumn = New DataColumn("Status")
                Dim dc3 As DataColumn = New DataColumn("message")
                Dim dc4 As DataColumn = New DataColumn("is_approved")
                Dim dc5 As DataColumn = New DataColumn("response_code")

                dt.Columns.Add(dc1)
                dt.Columns.Add(dc2)
                dt.Columns.Add(dc3)
                dt.Columns.Add(dc4)
                dt.Columns.Add(dc5)

                Dim dr1 As DataRow = dt.NewRow()
                dr1(0) = "1"

                If data.ContainsKey("status") Then
                    dr1(1) = data("status").ToString
                Else
                    dr1(1) = ""
                End If
                If data.ContainsKey("message") Then
                    dr1(2) = data("message").ToString
                Else
                    dr1(2) = ""
                End If

                If data.ContainsKey("is_approved") Then
                    dr1(3) = data("is_approved").ToString
                Else
                    dr1(3) = ""
                End If

                If data.ContainsKey("response_code") Then
                    dr1(4) = data("response_code").ToString
                Else
                    dr1(4) = ""
                End If
                dt.Rows.Add(dr1)

                grdResponseDetails.DataSource = dt
                grdResponseDetails.DataBind()
                Exit Sub
            Else
                'Status False
                '{"response_code":2,"status":false,"message":"Merchantcode not found"}

                Dim dt As New DataTable
                Dim dc1 As DataColumn = New DataColumn("SrNo")
                Dim dc2 As DataColumn = New DataColumn("status")
                Dim dc3 As DataColumn = New DataColumn("message")
                Dim dc4 As DataColumn = New DataColumn("response_code")

                dt.Columns.Add(dc1)
                dt.Columns.Add(dc2)
                dt.Columns.Add(dc3)
                dt.Columns.Add(dc4)



                Dim dr1 As DataRow = dt.NewRow()
                dr1(0) = "1"
                If data.ContainsKey("status") Then
                    dr1(1) = data("status").ToString
                Else
                    dr1(1) = ""
                End If

                If data.ContainsKey("message") Then
                    dr1(2) = data("message").ToString
                Else
                    dr1(2) = ""
                End If
                If data.ContainsKey("response_code") Then
                    dr1(3) = data("response_code").ToString
                Else
                    dr1(3) = ""
                End If

                dt.Rows.Add(dr1)

                grdResponseDetails.DataSource = dt
                grdResponseDetails.DataBind()

                Exit Sub
            End If

            lblError.Text = APIResult
            lblError.Visible = True

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub btnPopupYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopupYes.Click
        Try
            If Session("Workfor") = "Delete" Then
                If btnPopupYes.Text = "Ok" Then
                    If Session("EditFlag") = 1 Then
                        Response.Redirect("SearchStudent.aspx") '// Change form Name
                    Else
                        Clear()
                        Exit Sub
                    End If
                End If

                Dim QryStr As String = "delete from CRM_Student_Registration where RID=" & lblRID.Text.Trim & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    lblDialogMsg.Text = "Record deleted Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                Else
                    lblDialogMsg.Text = "Sorry !! Process Can't be Completed.."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnPopupYes.Text = "Ok"
                btnCancel.Attributes("Style") = "Display:None"
                ModalPopupExtender1.Show()
                Exit Sub
            End If
            If btnPopupYes.Text = "Ok" Then
                If Session("EditFlag") = 1 Then
                    Response.Redirect("SearchStudent.aspx") '// Change form Name
                Else
                    Response.Redirect("cKycForm.aspx")
                    'Clear()
                    Exit Sub
                End If
            End If
            lblError.Text = ""
            lblError.CssClass = ""

            Dim VImagePath As String = ""

            VImagePath = ""




            Dim Fpdata As String = txtfpdata.Text
            Dim Arr() As String
            Dim VWebFpdata, VWebFigureData As String

            If Not txtfpdata.Text.Trim = "" Then
                Arr = Fpdata.Split("~")
                VWebFpdata = Arr(0).ToString()
                VWebFigureData = Arr(1).ToString()
            Else
                VWebFigureData = ""
                VWebFpdata = ""
            End If

            Dim VWebFigureData1, VWebFigureData2, VWebFigureData3 As String
            If Not VWebFigureData = "" Then
                If Arr(1).Length >= 87200 Then
                    VWebFigureData1 = VWebFigureData.Substring(0, 43600)
                    VWebFigureData2 = VWebFigureData.Substring(43600, (87200 - 43600))
                    VWebFigureData3 = VWebFigureData.Substring(87200, (VWebFigureData.Length - 87200))
                Else
                    VWebFigureData1 = VWebFigureData.Substring(0, 43600)
                    VWebFigureData2 = VWebFigureData.Substring(43600, (VWebFigureData.Length - 43600))
                    VWebFigureData3 = ""
                End If

            Else
                VWebFigureData1 = ""
                VWebFigureData2 = ""
                VWebFigureData3 = ""
            End If


            If btnSave.Text.Trim.ToUpper = "Submit".Trim.ToUpper Then


                'If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details Where RequestID='" & VRequestID & "' ") > 0 Then 'Change where condition according to Criteria 
                '    lblError.Text = "Record Already Exists."
                '    lblError.CssClass = "errorlabels"
                '    Exit Sub
                'End If



            ElseIf btnSave.Text.Trim.ToUpper = "Update".Trim.ToUpper Then


                'Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details set RequestID='" & VRequestID & "', RequestDate='" & VRequestDate & "', ApprovedBy='" & VApprovedBy & "', ApprovedDateTime='" & VApprovedDateTime & "', ApporvedStatus='" & VApporvedStatus & "', ApporveRemakrs='" & VApporveRemakrs & "', Salutation='" & VSalutation & "', FirstName='" & VFirstName & "', MiddleName='" & VMiddleName & "', LastName='" & VLastName & "', Address='" & VAddress & "', PhoneNo='" & VPhoneNo & "', AadharNumber='" & VAadharNumber & "', CardNumber='" & VCardNumber & "', WebFpData='" & VWebFpdata & "', WebImgFpData1='" & VWebFigureData1 & "', WebImgFpData2='" & VWebFigureData2 & "', WebImgFpData3='" & VWebFigureData3 & "', CompanyCode='" & VCompanyCode & "', RecordDateTime='" & VRecordDateTime & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where RID=" & lblRID.Text.Trim & ""
                Dim QryStr As String  '= "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details set  ApporvedStatus='Pending',  Salutation='" & VSalutation & "', FirstName='" & VFirstName & "', MiddleName='" & VMiddleName & "', LastName='" & VLastName & "', Address='" & VAddress & "', PhoneNo='" & VPhoneNo & "', AadharNumber='" & VAadharNumber & "', CardNumber='" & VCardNumber & "', WebFpData='" & VWebFpdata & "', WebImgFpData1='" & VWebFigureData1 & "', WebImgFpData2='" & VWebFigureData2 & "', WebImgFpData3='" & VWebFigureData3 & "' , UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where RequestID='" & VRequestID & "'"

                'Dim QryStr As String = "update CRM_Student_Registration set  ActiveStatus='" & VActiveStatus & "',ImagePath='" & VImagePath & "', FatherName='" & VFatherName & "', FatherOccupation='" & VFatherOccupation & "', FatherMobileNo='" & VFatherMobileNo & "', MotherName='" & VMotherName & "', MotherOccupation='" & VMotherOccupation & "', MotherMobileNo='" & VMotherMobileNo & "', StudentName='" & VStudentName & "', DOB='" & VDOB & "', SchoolName='" & VSchoolName & "', Class='" & VClass & "', EmailID='" & VEmailID & "', MobileNo='" & VMobileNo & "', ResidenceNo='" & VResidenceNo & "', Address='" & VAddress & "', PreviousInstitute='" & VPreviousInstitute & "', ReasonForLeaving='" & VReasonForLeaving & "', StudentID='" & VStudentID & "', Password='" & VPassword & "', RegistrationDate='" & VRegistrationDate & "' ,WebFpData='" & VWebFpdata & "',WebImgFpData1='" & VWebFigureData1 & "',WebImgFpData2='" & VWebFigureData2 & "',WebImgFpData3='" & VWebFigureData3 & "'  where RID=" & lblRID.Text.Trim & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    'FillPreviousData()

                    lblDialogMsg.Text = "Record Updated Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnPopupYes.Text = "Ok"
                    btnCancel.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                Else
                    lblDialogMsg.Text = "Process Cann't be Complited."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnPopupYes.Text = "Ok"
                    btnCancel.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                End If
            End If



        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Private Sub Clear()
        Try

            lblError.Text = ""
            lblError.CssClass = ""
            txtTransID.Text = ""

            txtTransDate.Text = ""


            Session("EditFlag") = 0
            btnSave.Text = "Submit"
            btnClear.Text = "Reset"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            btnDelete.Visible = False
            txtTransID.Text = GV.FL.AddInVar("Prefix_StudentID", "autonumber") & GV.FL.getAutoNumber("StudentID")
            txtTransDate.Text = Now.Date.ToString("dd/MM/yyyy")


            txtfpdata.Text = ""
            imgFinger.ImageUrl = "..\images\uploadimage.png"

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then



                '"update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set AEPS_Onboard_Status='Yes' where  RegistrationId = '" & VRegistrationId & "';"

                lblError.Text = ""
                lblError.CssClass = ""

                Session("EditFlag") = 0
                div_req.Visible = True

                btnremove.Visible = False
                btnCapture.Visible = False

                Session("Workfor") = "Save"
                txtTransID.Text = GV.FL.getAutoNumber("SessionId")
                txtTransDate.Text = Now.Date.ToString("dd/MM/yyyy")
                btnSave.Text = "Submit"
                btnClear.Text = "Reset"
                btnDelete.Enabled = False
                btnDelete.Visible = False


                ddlBankList.Items.Clear()


                APIResult = GetApiResult_NEW("Bank_List_API_URL")
                Dim dd() As String = APIResult.Split("[")
                Dim pp() As String = dd(1).Split("]")

                Dim json() As String = pp(0).Replace("[", "").Replace("]", "").Split("},{")
                For i As Integer = 0 To json.Length - 1
                    If json(i).Contains("bankName") Then
                        json(i) = json(i).Replace(",{", "{") & "}"
                        Dim jss As New Script.Serialization.JavaScriptSerializer()
                        Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(json(i))
                        Dim xitem As ListItem
                        xitem = New ListItem(data("bankName"), data("iinno"))
                        ddlBankList.Items.Add(xitem)
                    End If
                Next

                If ddlBankList.Items.Count > 0 Then
                    ddlBankList.Items.Insert(0, ":: Select Bank ::")
                    ddlBankList.SelectedIndex = 0
                Else
                    ddlBankList.Items.Add(":: Select Bank ::")
                    ddlBankList.SelectedIndex = 0
                End If

                ddlAPI_Call_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    'Public Sub FillPreviousData()
    '    Try
    '        If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details Where kCode='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "' ") > 0 Then
    '            div_req.Visible = True

    '            DS = GV.FL.OpenDs(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details Where kCode='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "' ")
    '            If Not DS Is Nothing Then
    '                If DS.Tables.Count > 0 Then

    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("RequestID")) Then
    '                        If Not DS.Tables(0).Rows(0).Item("RequestID").ToString() = "" Then
    '                            txtTransID.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RequestID").ToString())
    '                        Else
    '                            txtTransID.Text = ""
    '                        End If
    '                    Else
    '                        txtTransID.Text = ""
    '                    End If

    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("RequestDate")) Then
    '                        If Not DS.Tables(0).Rows(0).Item("RequestDate").ToString() = "" Then
    '                            txtTransDate.Text = CDate(GV.parseString(DS.Tables(0).Rows(0).Item("RequestDate").ToString())).ToString("dd/MM/yyyy")
    '                        Else
    '                            txtTransDate.Text = ""
    '                        End If
    '                    Else
    '                        txtTransDate.Text = ""
    '                    End If




    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("Salutation")) Then
    '                        If Not DS.Tables(0).Rows(0).Item("Salutation").ToString() = "" Then
    '                            ddlSalutation.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Salutation").ToString())
    '                        Else
    '                            ddlSalutation.SelectedIndex = 0
    '                        End If
    '                    Else
    '                        ddlSalutation.SelectedIndex = 0
    '                    End If


    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("FirstName")) Then
    '                        If Not DS.Tables(0).Rows(0).Item("FirstName").ToString() = "" Then
    '                            txtFirstName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("FirstName").ToString())
    '                        Else
    '                            txtFirstName.Text = ""
    '                        End If
    '                    Else
    '                        txtFirstName.Text = ""
    '                    End If

    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("MiddleName")) Then
    '                        If Not DS.Tables(0).Rows(0).Item("MiddleName").ToString() = "" Then
    '                            txtMiddleName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MiddleName").ToString())
    '                        Else
    '                            txtMiddleName.Text = ""
    '                        End If
    '                    Else
    '                        txtMiddleName.Text = ""
    '                    End If

    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("LastName")) Then
    '                        If Not DS.Tables(0).Rows(0).Item("LastName").ToString() = "" Then
    '                            txtLastName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("LastName").ToString())
    '                        Else
    '                            txtLastName.Text = ""
    '                        End If
    '                    Else
    '                        txtLastName.Text = ""
    '                    End If









    '                    Dim webfpdata, WebImgFpData1, WebImgFpData2, WebImgFpData3 As String
    '                    'rahul
    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebFpData")) Then

    '                        If Not DS.Tables(0).Rows(0).Item("WebFpData").ToString() = "" Then

    '                            webfpdata = DS.Tables(0).Rows(0).Item("WebFpData").ToString()
    '                        Else
    '                            webfpdata = ""
    '                        End If
    '                    Else
    '                        webfpdata = ""
    '                    End If

    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData1")) Then

    '                        If Not DS.Tables(0).Rows(0).Item("WebImgFpData1").ToString() = "" Then
    '                            WebImgFpData1 = DS.Tables(0).Rows(0).Item("WebImgFpData1").ToString()
    '                        Else
    '                            WebImgFpData1 = "~/images/uploadimage.png"
    '                        End If
    '                    Else
    '                        WebImgFpData1 = "~/images/uploadimage.png"
    '                    End If

    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData2")) Then

    '                        If Not DS.Tables(0).Rows(0).Item("WebImgFpData2").ToString() = "" Then
    '                            WebImgFpData2 = DS.Tables(0).Rows(0).Item("WebImgFpData2").ToString()
    '                        Else
    '                            WebImgFpData2 = ""
    '                        End If
    '                    Else
    '                        WebImgFpData2 = ""
    '                    End If

    '                    If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData3")) Then

    '                        If Not DS.Tables(0).Rows(0).Item("WebImgFpData3").ToString() = "" Then
    '                            WebImgFpData3 = DS.Tables(0).Rows(0).Item("WebImgFpData3").ToString()
    '                        Else
    '                            WebImgFpData3 = ""
    '                        End If
    '                    Else
    '                        WebImgFpData3 = ""
    '                    End If

    '                    If WebImgFpData1 = "~/images/uploadimage.png" Then
    '                        txtfpdata.Text = ""
    '                    Else
    '                        txtfpdata.Text = webfpdata & "~" & WebImgFpData1 & WebImgFpData2 & WebImgFpData3
    '                    End If

    '                    imgFinger.ImageUrl = WebImgFpData1 & WebImgFpData2 & WebImgFpData3

    '                    btnDelete.Visible = False
    '                    btnSave.Text = "Update"
    '                    btnSave.Enabled = True
    '                    btnSave.Visible = True
    '                    btnClear.Visible = False

    '                    div_req.Visible = True


    '                    txtTransID.ReadOnly = True
    '                    txtTransDate.ReadOnly = True



    '                    ddlSalutation.Enabled = True
    '                    ddlSalutation.CssClass = "form-control"


    '                    txtFirstName.ReadOnly = False
    '                    txtMiddleName.ReadOnly = False
    '                    txtLastName.ReadOnly = False


    '                    btnremove.Visible = True
    '                    btnCapture.Visible = True

    '                    '///////////////======= End set Uploaded Image url ===============================================



    '                End If
    '            End If



    '            Exit Sub
    '        Else
    '            div_req.Visible = False

    '            Session("Workfor") = "Save"
    '            txtTransID.Text = GV.FL.getAutoNumber("SessionId")
    '            txtTransDate.Text = Now.Date.ToString("dd/MM/yyyy")
    '            btnSave.Text = "Submit"
    '            btnClear.Text = "Reset"
    '            btnDelete.Enabled = False
    '            btnDelete.Visible = False
    '        End If
    '            Catch ex As Exception 
    'GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

    '    End Try
    'End Sub

    Private Sub btnDelete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Session("Workfor") = "Delete"
            lblDialogMsg.Text = "Are you sure you want to delete ?"
            lblDialogMsg.CssClass = ""
            btnCancel.Text = "No"
            btnCancel.Attributes("Style") = ""
            btnPopupYes.Text = "Yes"
            ModalPopupExtender1.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Dim APIResult As String = ""
    Dim strBuild As String = ""
    Public Function GetApiResult_NEW(APIMethod As String) As String
        Dim ApiResult As String = ""
        Dim StrParameters As String = ""
        Dim API_URLS As String = ""
        Try
            '222111

            'Dim setParameter_API_Obj As New QueryRemitter_API_Parameters()
            'setParameter_API_Obj.Mobile = GV.parseString(txtEnterMobileNo.Text.Trim)
            'setParameter_API_Obj.bank3_flag = "Yes"

            If APIMethod = "Bank_List_API_URL" Then 'Done
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject("")
                API_URLS = Bank_List_API_URL
                ApiResult = ReadbyRestClient_NEW(API_URLS, StrParameters)
            ElseIf APIMethod = "Enquiry_API_URL" Then 'Done
                Dim setParameter_API_Obj As New Enquiry_API_Parameters()

                setParameter_API_Obj.latitude = GV.parseString(txtLat.Text.Trim)
                setParameter_API_Obj.longitude = GV.parseString(txtLong.Text.Trim)
                setParameter_API_Obj.mobilenumber = GV.parseString(txtMobileNumber.Text.Trim)
                setParameter_API_Obj.referenceno = GV.parseString(txtTransID.Text.Trim)

                setParameter_API_Obj.referenceno = GV.parseString(txtTransID.Text.Trim)

                'setParameter_API_Obj.accno = GV.parseString(txtBankAccountNo.Text.Trim)
                'setParameter_API_Obj.bankid = GV.parseString(ddlSelectBank.SelectedValue.Trim) ' GV.FL.AddInVar("BankID", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.MoneyTransferBankList where Code='" & GV.parseString(ddlSelectBank.SelectedValue.Trim) & "'")
                'setParameter_API_Obj.benename = GV.parseString(txtRecepientMobileNo.Text.Trim)
                'setParameter_API_Obj.referenceid = GV.FL.getAutoNumber("TransId")
                'setParameter_API_Obj.pincode = "110027"
                'setParameter_API_Obj.address = "New Delhi"
                'setParameter_API_Obj.dob = "13-09-1990"
                'setParameter_API_Obj.gst_state = "07"
                'setParameter_API_Obj.bene_id = GV.parseString(lbl_Beneficiary_temp_id.Text.Trim)
                'StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                'API_URLS = PennyDrop_API_URL

            End If



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

            'lblTransId.Text = GV.FL.getAutoNumber("TransId")
            Dim transID As String = GV.FL.getAutoNumber("TransId")

            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(transID) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine

            API_Name = "AEPS API"
            Trans_ID = GV.parseString(transID)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = "10012"
            AgentType = "Retailer"



            Dim tokenHandler As New JwtSecurityTokenHandler()

            Dim SecurityKey As New Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("UFMwMDgzM2U4Mzc3NmQ5MTlmNGI4ZDRmNjI3NjJiNGUwMDU0MzJi")) 'UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh   'UFMwMDgzM2U4Mzc3NmQ5MTlmNGI4ZDRmNjI3NjJiNGUwMDU0MzJi
            Dim Credentials As New Microsoft.IdentityModel.Tokens.SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)

            Dim header As New JwtHeader(Credentials)

            Dim unixEpoch = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            Dim currTimeStamp = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds)

            Dim payload As New JwtPayload()
            payload.AddClaim(New Claim("timestamp", currTimeStamp))
            payload.AddClaim(New Claim("partnerId", "PS00833"))   'PS00343  'PS00833
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

            GV.SaveTextToFile(LogString, Server.MapPath("AEPS_API_LOG.txt"), True)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("AEPS_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


End Class