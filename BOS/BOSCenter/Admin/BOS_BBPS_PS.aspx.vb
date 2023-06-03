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



Public Class BOS_BBPS_PS
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------

    'WHITELISTED IP 	103.205.66.210, 162.144.106.233,103.83.145.84
    'ENVIORMENT 	UAT
    'JWT KEY 	UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh
    'AES ENCRYPTION KEY 	c05085d229a39a7e
    'AES ENCRYPTION IV 	d45195355e9db9a3
    'CALLBACK URL 	https://boscenter.in/api/PaySprintDMTCallbkController
    'SENDER ID 	
    'STATUS 	ACTIVE
    'VERSION 	IP AND AUTHORIZED KEY BASED

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblWalletBal.Text = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)


            If Not IsPostBack Then



                lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                lblAgentType.Text = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                Dim DataBaseName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response)
                Dim Group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                ddlGateway.Items.Clear()
                ddlGateway.Items.Add("Recharge")
                ddlGateway.Items.Add("Recharge-2")



                Dim Gateway As String = GV.FL.AddInVar("Gateway", GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA_MD  where Title='Recharge & Bill Payment' and AdminID='" & CompanyCode & "'")
                If Not GV.parseString(Gateway.Trim) = "" Then
                    If GV.parseString(Gateway.Trim).ToUpper = "Recharge 1".ToUpper Then
                        ddlGateway.SelectedValue = "Recharge"
                    ElseIf GV.parseString(Gateway.Trim).ToUpper = "Recharge 2".ToUpper Then
                        ddlGateway.SelectedValue = "Recharge-2"
                    Else
                        ddlGateway.SelectedValue = "Recharge"
                    End If
                Else
                End If

                lblSessionFlag.Text = 0
                btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"

                If Not Request.QueryString("type") Is Nothing Then
                    If Not Request.QueryString("type").Trim = "" Then
                        If Request.QueryString("type").Trim.ToUpper = "mobile".ToUpper Then
                            btnmobile_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "dth".ToUpper Then
                            btndth_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Landline".ToUpper Then
                            btnlandline_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Postpaid".ToUpper Then
                            btnpostpaid_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Electricity".ToUpper Then
                            btnelectricity_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Broadband".ToUpper Then
                            btnbroadband_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "LPG".ToUpper Then
                            btnLPG_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Waterbill".ToUpper Then
                            btnwaterbill_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "LPG".ToUpper Then
                            btnLPG_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "EMI".ToUpper Then
                            btnEMI_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Municipality".ToUpper Then
                            btnMunicipality_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Cable".ToUpper Then
                            btnCable_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        ElseIf Request.QueryString("type").Trim.ToUpper = "Insurance".ToUpper Then
                            btnInsurance_Click(sender, e)
                            Div_Navigation_buttons.Visible = False
                        Else
                            btnmobile_Click(sender, e)
                        End If
                    Else
                        btnmobile_Click(sender, e)
                    End If
                Else
                    btnmobile_Click(sender, e)
                End If




            End If

        Catch ex As Exception
        End Try
    End Sub


    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
        End Try

    End Sub
    Public Sub allButtonVisibleFalse()
        Try
            btndth.Visible = False
            btnmobile.Visible = False
        Catch ex As Exception

        End Try
    End Sub



    '///  Recharge API URL  - Start



    'Dim baseURL As String = "https://paysprint.in"
    Dim baseURL As String = "https://api.paysprint.in"

    'Dim HLR_CHK_API_URL As String = baseURL & "/service-api/api/v1/service/recharge/hlrapi/hlrcheck"
    'Dim DTH_INFO_API_URL As String = baseURL & "/service-api/api/v1/service/recharge/hlrapi/dthinfo"
    'Dim BROWSE_PLAN_API_URL As String = baseURL & "/service-api/api/v1/service/recharge/hlrapi/browseplan"
    'Dim OPERATOR_LIST_API_URL As String = baseURL & "/service-api/api/v1/service/recharge/recharge/getoperator"
    'Dim DO_RECHARGE_API_URL As String = baseURL & "/service-api/api/v1/service/recharge/recharge/dorecharge"
    'Dim STATUS_ENQUIRY_API_URL As String = baseURL & "/service-api/api/v1/service/recharge/recharge/status"

    Dim HLR_CHK_API_URL As String = baseURL & "/api/v1/service/recharge/hlrapi/hlrcheck"
    Dim DTH_INFO_API_URL As String = baseURL & "/api/v1/service/recharge/hlrapi/dthinfo"
    Dim BROWSE_PLAN_API_URL As String = baseURL & "/api/v1/service/recharge/hlrapi/browseplan"
    Dim OPERATOR_LIST_API_URL As String = baseURL & "/api/v1/service/recharge/recharge/getoperator"
    Dim DO_RECHARGE_API_URL As String = baseURL & "/api/v1/service/recharge/recharge/dorecharge"
    Dim STATUS_ENQUIRY_API_URL As String = baseURL & "/api/v1/service/recharge/recharge/status"



    'Dim BILL_PAYMENT_OPERATOR_LIST_API_URL As String = "https://paysprint.in/service-api/api/v1/service/bill-payment/bill/getoperator"
    'Dim BILL_PAYMENT_FETCH_API_URL As String = "https://paysprint.in/service-api/api/v1/service/bill-payment/bill/fetchbill"
    'Dim BILL_PAYMENT_STATUS_ENQUIRY_API_URL As String = "https://paysprint.in/service-api/api/v1/service/bill-payment/bill/status"
    'Dim BILL_PAYMENT_PAY_BILL_API_URL As String = "https://paysprint.in/service-api/api/v1/service/bill-payment/bill/paybill"


    Dim BILL_PAYMENT_OPERATOR_LIST_API_URL As String = baseURL & "/api/v1/service/bill-payment/bill/getoperator"
    Dim BILL_PAYMENT_FETCH_API_URL As String = baseURL & "/api/v1/service/bill-payment/bill/fetchbill"
    Dim BILL_PAYMENT_STATUS_ENQUIRY_API_URL As String = baseURL & "/api/v1/service/bill-payment/bill/status"
    Dim BILL_PAYMENT_PAY_BILL_API_URL As String = baseURL & "/api/v1/service/bill-payment/bill/paybill"


    '{"operator":"11","canumber":"102277100","amount":"100","referenceid":"20018575947","latitude":"27.2232","longitude":"78.26535","mode":"online",
    '"bill_fetch":{"billAmount":"820.0","billnetamount":"820.0","billdate":"01Jan1990","dueDate":"2021-01-06","acceptPayment":true,"acceptPartPay":false,"cellNumber":"102277100","userName":"SALMAN"}}
    'https://www.aspsnippets.com/questions/167649/Solved-ASPNet-Error-Unable-to-cast-object-of-type-NewtonsoftJsonLinqJObject-to-type-NewtonsoftJsonLinqJArray/


    '///  Recharge API URL  - END NEW
    Public Class BILL_PAYMENT_STATUS_ENQUIRY_API_Parameters
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

    Public Class BILL_PAYMENT_PAY_BILL_API_Parameters
        Dim Voperator, Vcanumber, Vamount, Vreferenceid As String
        Dim Vlatitude, Vlongitude, Vmode, Vbill_fetch As String
        'Public Property bill_fetch As List(Of bill_fetch)
        Public Property [operator]() As String
            Get
                Return Voperator
            End Get
            Set(ByVal value As String)
                Voperator = value
            End Set
        End Property
        Public Property canumber() As String
            Get
                Return Vcanumber
            End Get
            Set(ByVal value As String)
                Vcanumber = value
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
        Public Property referenceid() As String
            Get
                Return Vreferenceid
            End Get
            Set(ByVal value As String)
                Vreferenceid = value
            End Set
        End Property
        Public Property latitude() As String
            Get
                Return Vlatitude
            End Get
            Set(ByVal value As String)
                Vlatitude = value
            End Set
        End Property
        Public Property longitude() As String
            Get
                Return Vlongitude
            End Get
            Set(ByVal value As String)
                Vlongitude = value
            End Set
        End Property
        Public Property mode() As String
            Get
                Return Vmode
            End Get
            Set(ByVal value As String)
                Vmode = value
            End Set
        End Property
        Public Property bill_fetch() As String
            Get
                Return Vbill_fetch
            End Get
            Set(ByVal value As String)
                Vbill_fetch = value
            End Set
        End Property

    End Class


    Public Class bill_fetch
        Dim VbillAmount, Vbillnetamount, Vbilldate, VdueDate As String
        Dim VacceptPayment, VacceptPartPay, VcellNumber, VuserName As String

        Public Property billAmount() As String
            Get
                Return VbillAmount
            End Get
            Set(ByVal value As String)
                VbillAmount = value
            End Set
        End Property
        Public Property billnetamount() As String
            Get
                Return Vbillnetamount
            End Get
            Set(ByVal value As String)
                Vbillnetamount = value
            End Set
        End Property
        Public Property billdate() As String
            Get
                Return Vbilldate
            End Get
            Set(ByVal value As String)
                Vbilldate = value
            End Set
        End Property
        Public Property dueDate() As String
            Get
                Return VdueDate
            End Get
            Set(ByVal value As String)
                VdueDate = value
            End Set
        End Property
        Public Property acceptPayment() As String
            Get
                Return VacceptPayment
            End Get
            Set(ByVal value As String)
                VacceptPayment = value
            End Set
        End Property
        Public Property acceptPartPay() As String
            Get
                Return VacceptPartPay
            End Get
            Set(ByVal value As String)
                VacceptPartPay = value
            End Set
        End Property
        Public Property cellNumber() As String
            Get
                Return VcellNumber
            End Get
            Set(ByVal value As String)
                VcellNumber = value
            End Set
        End Property
        Public Property userName() As String
            Get
                Return VuserName
            End Get
            Set(ByVal value As String)
                VuserName = value
            End Set
        End Property

    End Class


    Public Class BILL_PAYMENT_FETCH_API_Parameters
        Dim Vcanumber As String
        Dim Voperator As Long
        Dim Vmode As String

        Public Property canumber() As String
            Get
                Return Vcanumber
            End Get
            Set(ByVal value As String)
                Vcanumber = value
            End Set
        End Property
        Public Property [operator]() As Long
            Get
                Return Voperator
            End Get
            Set(ByVal value As Long)
                Voperator = value
            End Set
        End Property

        Public Property mode() As String
            Get
                Return Vmode
            End Get
            Set(ByVal value As String)
                Vmode = value
            End Set
        End Property
    End Class

    Public Class HLR_CHK_API_Parameters
        Dim Vtype As String
        Dim Vnumber As Long

        Public Property number() As Long
            Get
                Return Vnumber
            End Get
            Set(ByVal value As Long)
                Vnumber = value
            End Set
        End Property

        Public Property type() As String
            Get
                Return Vtype
            End Get
            Set(ByVal value As String)
                Vtype = value
            End Set
        End Property
    End Class
    Public Class DTH_INFO_API_Parameters
        Dim Vop As String
        Dim Vcanumber As Long
        Public Property canumber() As Long
            Get
                Return Vcanumber
            End Get
            Set(ByVal value As Long)
                Vcanumber = value
            End Set
        End Property

        Public Property op() As String
            Get
                Return Vop
            End Get
            Set(ByVal value As String)
                Vop = value
            End Set
        End Property
    End Class

    Public Class BROWSE_PLAN_API_Parameters
        Dim Vcircle, Vop As String
        Public Property circle() As String
            Get
                Return Vcircle
            End Get
            Set(ByVal value As String)
                Vcircle = value
            End Set
        End Property

        Public Property op() As String
            Get
                Return Vop
            End Get
            Set(ByVal value As String)
                Vop = value
            End Set
        End Property
    End Class

    Public Class DO_RECHARGE_API_Parameters
        Dim Voperator, Vcanumber, Vamount, Vreferenceid As String
        Public Property [operator]() As String
            Get
                Return Voperator
            End Get
            Set(ByVal value As String)
                Voperator = value
            End Set
        End Property

        Public Property canumber() As String
            Get
                Return Vcanumber
            End Get
            Set(ByVal value As String)
                Vcanumber = value
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


        Public Property referenceid() As String
            Get
                Return Vreferenceid
            End Get
            Set(ByVal value As String)
                Vreferenceid = value
            End Set
        End Property
    End Class

    Public Class STATUS_ENQUIRY_API_Parameters
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

    Public Function GetApiResult_NEW(APIMethod As String) As String
        Dim ApiResult As String = ""
        Dim StrParameters As String = ""
        Dim API_URLS As String = ""
        Try
            '222111
            'OPERATOR_LIST_API_URL

            If APIMethod = "OPERATOR_LIST_API_URL" Then 'Done
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject("")
                API_URLS = OPERATOR_LIST_API_URL

            ElseIf APIMethod = "BILL_PAYMENT_OPERATOR_LIST_API_URL" Then 'Done
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject("")
                API_URLS = BILL_PAYMENT_OPERATOR_LIST_API_URL
            ElseIf APIMethod = "HLR_CHK_API_Parameters" Then 'Error

                Dim setParameter_API_Obj As New HLR_CHK_API_Parameters
                setParameter_API_Obj.number = GV.parseString(txt_Mobile_CA_No.Text.Trim)
                setParameter_API_Obj.type = "mobile"
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = HLR_CHK_API_URL
            ElseIf APIMethod = "DTH_INFO_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New DTH_INFO_API_Parameters
                setParameter_API_Obj.canumber = GV.parseString(txt_Mobile_CA_No.Text.Trim)
                setParameter_API_Obj.op = ddlOperators.SelectedItem.Text
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = DTH_INFO_API_URL
            ElseIf APIMethod = "BROWSE_PLAN_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New BROWSE_PLAN_API_Parameters
                setParameter_API_Obj.circle = GV.parseString("Delhi NCR")
                setParameter_API_Obj.op = GV.parseString("Airtel")
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = BROWSE_PLAN_API_URL
            ElseIf APIMethod = "DO_RECHARGE_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New DO_RECHARGE_API_Parameters
                setParameter_API_Obj.canumber = GV.parseString(txt_Mobile_CA_No.Text)
                setParameter_API_Obj.operator = GV.parseString(ddlOperators.SelectedValue)
                setParameter_API_Obj.referenceid = GV.parseString(GV.FL.getAutoNumber("TransId"))
                setParameter_API_Obj.amount = GV.parseString(txt_Recharge_Amt.Text.Trim)

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = DO_RECHARGE_API_URL
            ElseIf APIMethod = "BILL_PAYMENT_FETCH_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New BILL_PAYMENT_FETCH_API_Parameters
                setParameter_API_Obj.canumber = GV.parseString(txt_BBPS_CA_No.Text)
                setParameter_API_Obj.operator = GV.parseString(ddl_BBPS_Operators.SelectedValue)
                setParameter_API_Obj.mode = GV.parseString(ddl_BBPS_Mode.SelectedValue)
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = BILL_PAYMENT_FETCH_API_URL
            End If


            ApiResult = ReadbyRestClient_NEW(API_URLS, StrParameters)
        Catch ex As Exception
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

            API_Name = "Recharge API"
            Trans_ID = GV.parseString(lblTransId.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameter)
            AgentID = lblAgentID.Text
            AgentType = lblAgentType.Text



            Dim tokenHandler As New JwtSecurityTokenHandler()

            Dim SecurityKey As New Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("UFMwMDgzM2U4Mzc3NmQ5MTlmNGI4ZDRmNjI3NjJiNGUwMDU0MzJi")) 'UFMwMDgzM2U4Mzc3NmQ5MTlmNGI4ZDRmNjI3NjJiNGUwMDU0MzJi << Live  'UFMwMDM0M2ViYzQ1ODBmM2VhYTdlYTI2YmFiMWU5Yjg4OTMxZWZh <<UAT
            Dim Credentials As New Microsoft.IdentityModel.Tokens.SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)

            Dim header As New JwtHeader(Credentials)

            Dim unixEpoch = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            Dim currTimeStamp = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds)

            Dim payload As New JwtPayload()
            payload.AddClaim(New Claim("timestamp", currTimeStamp))
            payload.AddClaim(New Claim("partnerId", "PS00833")) 'PS00833   <<LIVE 'PS00343  <<UAT
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

            GV.SaveTextToFile(LogString, Server.MapPath("RECHARGE_API_LOG.txt"), True)
        Catch ex As Exception
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            GV.SaveTextToFile(LogString, Server.MapPath("RECHARGE_API_LOG.txt"), True)
            Return str
        End Try
        Return str

    End Function

    'btn_Proceed_Recharge


    Protected Sub btn_Proceed_Recharge_Click(sender As Object, e As EventArgs) Handles btn_Proceed_Recharge.Click
        Dim lineNo As Decimal = 0.0
        lineNo = 1
        Try
            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""

            ddlOperators.CssClass = "form-control"
            ddlCircle.CssClass = "form-control"

            'lblFetchOperator.Text = ""
            'lblFetchOperator.CssClass = ""

            Dim RechargeAPI_Status As String = ""
            Dim RechargeAPI As String = ""
            If ddlGateway.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                RechargeAPI = "RechargeAPI_Status"
            ElseIf ddlGateway.SelectedValue.Trim.ToUpper = "Recharge-2".Trim.ToUpper Then
                RechargeAPI = "RechargeAPI_2_Status"
            End If
            '///// Start Check API  STATUS Super Admin Level 
            '///// Start Check API  STATUS Super Admin Level 

            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")
            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Recharge API Is Inactive At Company Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// End Check API  STATUS Super Admin Level  

            '///// Start Check API  STATUS System Settings 


            RechargeAPI_Status = ""
            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Sorry! Recharge API Is Inactive At Admin Level, Contact to Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            RechargeAPI_Status = ""
            RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                lblSearchCustomerError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 



            Dim holdAmt As String = ""
            holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
            If holdAmt.Trim = "" Then
                holdAmt = "0"
            End If



            Dim VServiceType, Voperator, Vcircle, Cus_MobileNo, VCus_Amount, VCus_Payable, VOperatorCode As String
            VOperatorCode = ""
            Vcircle = ""
            Voperator = ""

            'VOperatorCode = "11"
            'Vcircle = "Delhi NCR"
            'Voperator = "Airtel"


            VServiceType = lblSelectedService.Text.Trim
            lineNo = 2
            If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "PostPaid".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblSearchCustomerError.Text = "Enter Mobile No"
                    lblSearchCustomerError.CssClass = "errorlabels"
                    txt_Mobile_CA_No.Focus()
                    Exit Sub
                End If
                If Not ddlOperators.SelectedIndex = 0 Then
                    Voperator = ddlOperators.SelectedItem.Text
                Else
                    lblSearchCustomerError.Text = "Select Operators"
                    lblSearchCustomerError.CssClass = "errorlabels"
                    ddlOperators.CssClass = "ValidationError"

                    ddlOperators.Focus()

                    Exit Sub
                End If
                If Not ddlOperators.SelectedIndex = 0 Then
                    VOperatorCode = ddlOperators.SelectedValue
                End If

                If Not ddlCircle.SelectedIndex = 0 Then
                    Vcircle = ddlCircle.SelectedItem.Text
                Else
                    lblSearchCustomerError.Text = "Select Circle"
                    lblSearchCustomerError.CssClass = "errorlabels"
                    ddlCircle.CssClass = "ValidationError"
                    ddlCircle.Focus()
                    Exit Sub
                End If

            ElseIf lblSelectedService.Text.Trim.ToUpper = "landline".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblSearchCustomerError.Text = "Enter Landline No"
                    lblSearchCustomerError.CssClass = "errorlabels"
                    txt_Mobile_CA_No.Focus()
                    Exit Sub
                End If

            ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "DTH".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "gas".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                If txt_Mobile_CA_No.Text = "" Then
                    lblSearchCustomerError.Text = "Enter Consumer No"
                    lblSearchCustomerError.CssClass = "errorlabels"
                    txt_Mobile_CA_No.Focus()
                    Exit Sub
                End If
                If Not ddlOperators.SelectedIndex = 0 Then
                    Voperator = ddlOperators.SelectedItem.Text
                Else
                    lblSearchCustomerError.Text = "Select Operators"
                    lblSearchCustomerError.CssClass = "errorlabels"
                    ddlOperators.Focus()

                    Exit Sub
                End If
                If Not ddlOperators.SelectedIndex = 0 Then
                    VOperatorCode = ddlOperators.SelectedValue
                End If

            End If

            Cus_MobileNo = GV.parseString(txt_Mobile_CA_No.Text.Trim)
            If GV.parseString(txt_Recharge_Amt.Text) = "" Or txt_Recharge_Amt.Text = "0" Then
                lblSearchCustomerError.Text = "Enter Amount"
                lblSearchCustomerError.CssClass = "errorlabels"
                txt_Recharge_Amt.Focus()
                Exit Sub
            Else
                VCus_Amount = GV.parseString(txt_Recharge_Amt.Text.Trim)
            End If
            VCus_Payable = VCus_Amount
            If VCus_Amount = "" Then
                VCus_Amount = "0"
            End If
            Dim VNetAmount As Decimal = 0
            VNetAmount = VCus_Amount


            If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
            Else
                lblSearchCustomerError.Text = "You Have Insufficient Balance."
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// Check For API Balance - Start //////
            If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                lblSearchCustomerError.Text = "Insufficient API Balance."
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// Check For API Balance - End //////


            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            Dim VUpdatedBy, VUpdatedOn As String

            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VUpdatedOn = "getdate()"
            'System.Threading.Thread.Sleep(2000)
            lineNo = 3
            If ddlGateway.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                Dim TypeName As String = ""
                If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Postpaid".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "dth".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Then
                    TypeName = "RECH"
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "GAS".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                    TypeName = "BILLPAY"
                ElseIf lblSelectedService.Text.Trim.ToUpper = "Landline".Trim.ToUpper Then
                    TypeName = "Landline"
                End If

                Dim QryStr As String = ""
                lineNo = 4

                If lblSessionFlag.Text = 0 Then

                    lblSessionFlag.Text = 9


                    Dim VTransId As String = GV.parseString(GV.FL.getAutoNumber("TransId"))

                    Dim setParameter_API_Obj As New DO_RECHARGE_API_Parameters
                    setParameter_API_Obj.canumber = GV.parseString(txt_Mobile_CA_No.Text)
                    setParameter_API_Obj.operator = GV.parseString(ddlOperators.SelectedValue)
                    setParameter_API_Obj.referenceid = VTransId
                    setParameter_API_Obj.amount = GV.parseString(txt_Recharge_Amt.Text.Trim)
                    APIResult = ReadbyRestClient_NEW(DO_RECHARGE_API_URL, Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj))



                    'VTransId = "157063"

                    Dim strBuild As String = ""
                    Dim v_response_code, v_message, v_operatorid, v_ackno, v_refid As String
                    Dim v_status As Boolean = False
                    v_message = ""
                    v_operatorid = ""
                    v_ackno = ""
                    v_refid = ""
                    v_response_code = ""
                    'APIResult = "{ ""status "":true, ""response_code "":1, ""operatorid "": ""0 "", ""ackno "":29506346, ""refid "": ""157063 "", ""message "": ""Recharge for Airtel of Amount 10 is successful. ""}"
                    '{"status":true,"response_code":1,"operatorid":"0","ackno":29545627,"refid":"157281","message":"Recharge for Airtel of Amount 10 is successful."}
                    'APIResult = "{ ""status "":true, ""response_code "":1, ""operatorid "": ""0 "", ""ackno "":29546989, ""refid "": ""157286 "", ""message "": ""Recharge for Airtel of Amount 10 is successful. ""}"
                    Dim json2 As JObject = JObject.Parse(APIResult)
                    Dim data1_ As List(Of JToken) = json2.Children().ToList
                    For Each item As JProperty In data1_
                        If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
                            v_response_code = item.Value.ToString
                        ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
                            v_status = item.Value
                        ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
                            v_message = item.Value.ToString
                        ElseIf item.Name.ToString.Trim.ToUpper = "operatorid".ToString.Trim.ToUpper Then
                            v_operatorid = item.Value.ToString
                        ElseIf item.Name.ToString.Trim.ToUpper = "ackno".ToString.Trim.ToUpper Then
                            v_ackno = item.Value.ToString
                        ElseIf item.Name.ToString.Trim.ToUpper = "refid".ToString.Trim.ToUpper Then
                            v_refid = item.Value.ToString
                        End If
                    Next
                    lineNo = 5

                    'Gateway 2
                    VTransId = VTransId
                    Dim Vurid As String = RetailerID
                    Dim Vmobile As String = Cus_MobileNo
                    Dim Vamount As String = VCus_Amount
                    Dim VoperatorId As String = VOperatorCode
                    Dim Verror_code As String = v_status
                    Dim Vservice As String = TypeName
                    Dim Vbal As String = "0"
                    Dim VcommissionBal As String = "0"
                    Dim VresText As String = v_message
                    Dim VbillAmount As String = "0"
                    Dim VbillName As String = ""
                    Dim VRecord_DateTime As String = "GetDate()"
                    Dim VorderId As String = v_refid


                    QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API (TransIpAddress,Gateway,Refund_Status,TransId,RetailerID,Recharge_ServiceType,Recharge_Operator,Recharge_MobileNo_CaNo,Recharge_Amount,Recharge_PayableAmount,Recharge_Date,API_orderId,API_status,API_TransId,API_urid,API_mobile,API_amount,API_operatorId,API_error_code,API_service,API_bal,API_commissionBal,API_resText,API_billAmount,API_billName,UpdatedBy,UpdatedOn) values ('" & GV.parseString(GV.GetIPAddress) & "','1','No','" & GV.parseString(lblTransId.Text.Trim) & "','" & RetailerID & "','" & VServiceType & "','" & Voperator & "','" & Cus_MobileNo & "','" & VCus_Amount & "','" & VCus_Amount & "'," & VUpdatedOn & ",'" & VorderId & "','" & v_status & "','" & VTransId & "','" & Vurid & "','" & Vmobile & "','" & Vamount & "','" & VoperatorId & "','" & Verror_code & "','" & Vservice & "','" & Vbal & "','" & VcommissionBal & "','" & VresText & "','" & VbillAmount & "','" & VbillName & "','" & VUpdatedBy & "'," & VUpdatedOn & ") ; "
                    QryStr = QryStr & " " & " insert into " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info (RecordDatetime,API_TransId,Recharge_TransId,API_status,API_resText,CompanyCode,DBName) values(getdate(),'" & VTransId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(v_status) & "','" & GV.parseString(VresText) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "','" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "') ; "
                    lineNo = 6

                    If v_status = True Then
                        Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                        lineNo = 6.1
                        If GRP = "Retailer".ToUpper Then
                            'IF Type is of Retailer
                            lineNo = 6.2
                            RechargeCommision(lblSelectedService.Text.Trim, VOperatorCode, "Recharge", txt_Recharge_Amt.Text.Trim)
                            lineNo = 6.3
                            If Not lblRID.Text = "" Then
                                lineNo = 6.4
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

                                lineNo = 7
                                If vCanChange.Trim.ToUpper = "YES" Then
                                    Dim typeAmtForm As String = "Your Account is debited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txt_Recharge_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

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
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If


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
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    lineNo = 8
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
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// Retailer Commission Calculation - END


                                Else
                                    'vCanChange.Trim.ToUpper = "No"
                                    lineNo = 9
                                    Dim typeAmtForm As String = "Your Account is debited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txt_Recharge_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

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
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If


                                    '//// Distributor Commission Calculation - End
                                    lineNo = 10
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
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

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
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    '//// Retailer Commission Calculation - END


                                End If

                                lineNo = 11
                                Dim ServiceCharge As Decimal = 0
                                If CDec(GV.parseString(ServiceCharge)) > 0 Then
                                    ServiceCharge = ServiceCharge
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedItem.Text) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End
                                lineNo = 12


                            Else

                                ' Retailer
                                Dim typeAmtForm As String = "Your Account is debited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RetailerID & "','Admin','" & txt_Recharge_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                Dim ServiceCharge As Decimal = 0
                                If ServiceCharge > 0 Then
                                    ServiceCharge = ServiceCharge
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If
                                lineNo = 13

                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedItem.Text.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                            End If
                            lineNo = 14
                        ElseIf GRP = "Customer".ToUpper Then
                            'In case of Customer 

                            '//// Customer Commission Calculation - Start
                            RechargeCommision_Customer(lblSelectedService.Text.Trim, VOperatorCode, "Recharge", txt_Recharge_Amt.Text.Trim)
                            If Not lblRID.Text = "" Then

                                Dim AAID() As String = lblRID.Text.Split("*")
                                Dim Adminid_Com() As String = AAID(0).Split(":")
                                Dim CustID_Com() As String = AAID(1).Split(":")
                                Dim adminID As String = Adminid_Com(0)
                                Dim adminCom As String = Adminid_Com(1)

                                Dim CustID As String = CustID_Com(0)
                                Dim CustCom As String = CustID_Com(1)



                                Dim typeAmtForm As String = "Your Account is debited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                Dim CustTypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim CustTypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                lineNo = 15

                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & txt_Recharge_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                '//// Customer Commission Calculation - Start
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
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CustTypecommTo & "','" & CustTypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                lineNo = 16

                                Dim ServiceCharge As Decimal = 0
                                If ServiceCharge > 0 Then
                                    ServiceCharge = ServiceCharge
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If
                                lineNo = 17
                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedItem.Text.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End

                                '//// Customer Commission Calculation - END

                                lineNo = 18
                            Else
                                Dim typeAmtForm As String = "Your Account is debited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','Admin','" & txt_Recharge_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                lineNo = 19
                                Dim ServiceCharge As Decimal = 0
                                If CDec(ServiceCharge) > 0 Then
                                    ServiceCharge = ServiceCharge
                                    'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                    '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                    'Else
                                    '    ServiceCharge = 10
                                    'End If
                                    If ServiceCharge > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If
                                lineNo = 20
                                ''////// Service Charge For Admin To SuperAdmin - Start
                                Dim NetAmount As Decimal = 0
                                Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddlOperators.SelectedItem.Text.Trim) & "'").Split(":")
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                    ElseIf Service(1).Trim = "Amount" Then
                                        NetAmount = CDec(Service(0))
                                    ElseIf Service(1).Trim = "Not Applicable" Then
                                        NetAmount = CDec(Service(0))
                                    End If
                                End If
                                If NetAmount > 0 Then
                                    Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                ''///////  Service Charge For Admin To SuperAdmin - End
                                lineNo = 21
                            End If

                        End If

                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        '//// Admin & Super Admin Commission Calculation - Start
                        If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                            '//// Admin Commission Calculation - Start
                            Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Result As String
                            Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                            If GV.parseString(txt_Recharge_Amt.Text.Trim) = "" Then
                                V_Amount = "0"
                            Else
                                V_Amount = txt_Recharge_Amt.Text.Trim
                            End If
                            lineNo = 22
                            V_OperatorCategory = lblSelectedService.Text.Trim
                            V_OperatorCode = VOperatorCode
                            V_APIName = "Recharge"
                            V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                            Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)
                            lineNo = 23
                            If Not GV.parseString(Result) = "" Then
                                Dim Result_Arry() As String = Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                lineNo = 24
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
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            lineNo = 25
                            '//// Admin Commission Calculation - End

                            '//// Super Admin Commission Calculation - Start
                            Result = GV.Commision_Calculation_For_SuperAdmin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName)

                            If Not GV.parseString(Result) = "" Then
                                Dim Result_Arry() As String = Result.Split("*")
                                Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                Dim Admin_Com_ID As String = "Super Admin"
                                Dim Admin_Com_Amt As String = Admin_Com(1)

                                Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                Dim Service_Charge_ID As String = ""
                                Dim Service_Charge_Amt As String = Service_Charge(1)


                                If Service_Charge_Amt > 0 Then
                                    Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                                Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                V_Actual_Commission_Amt = 0
                                V_GSTAmt = 0
                                V_Commission_Without_GST = 0
                                V_TDS_Amt = 0
                                V_Net_Commission_Amt = 0
                                lineNo = 26
                                If Admin_Com_Amt > 0 Then
                                    V_Actual_Commission_Amt = Admin_Com_Amt
                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                    Admin_Com_Amt = V_Net_Commission_Amt
                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If

                            End If
                            '//// Super Admin Commission Calculation - End
                        End If
                        '//// Admin & Super Admin Commission Calculation - End
                        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        lineNo = 27
                    End If




                    If GV.FL.DMLQueriesBulk(QryStr) = True Then
                        lineNo = 28
                        lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                        lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                        lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                        lblPopTransactionID.Text = VTransId
                        lblPopTransactionAmt.Text = GV.parseString(txt_Recharge_Amt.Text.Trim)
                        lblPopStatus.Text = v_status
                        lblpopOperator.Text = TypeName.Trim
                        lblpopMobileNo.Text = txt_Mobile_CA_No.Text.Trim

                        ModalPopupExtender3.Show()

                    Else
                        lineNo = 29

                        lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                        lblDialogMsg.CssClass = "errorlabels"
                        btnCancel.Text = "Ok"
                        btnok.Visible = False
                        ModalPopupExtender1.Show()
                    End If




                End If





            End If

            'lblSearchCustomerError.Text = APIResult
        Catch ex As Exception
            lblSearchCustomerError.Text = lineNo & " : " & ex.Message & " : " & APIResult
        End Try
    End Sub


    Protected Sub btnRedirectPage_Click(sender As Object, e As EventArgs) Handles btnRedirectPage.Click
        Try
            Response.Redirect("BOS_BBPS_PS.aspx")
        Catch ex As Exception

        End Try
    End Sub
    Public Sub RechargeCommision_Customer(ByVal OperatorCategory As String, ByVal OperatorCode As String, ByVal gateway As String, ByVal Recharge_Amount As String)
        Try

            Dim VCommissionType, VCustomer_CommissionType As String
            VCommissionType = ""
            VCustomer_CommissionType = ""

            Dim VCommission, VCustomer_Commission As Decimal
            VCommission = 0


            VCustomer_Commission = 0


            Dim VContainCategory, VCanChange As String
            VContainCategory = ""
            VCanChange = ""


            Dim VadminComAmt, VCustomerComAmt As Decimal
            VadminComAmt = 0
            VCustomerComAmt = 0



            Dim CustomerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalCustomerAmt As Decimal
            VFinaladminAmt = 0
            VFinalCustomerAmt = 0

            Dim AdminID As String = ""
            Dim qry As String = ""
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='" & gateway & "' and ActiveStatus='Active'"
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



                        If VContainCategory.Trim.ToUpper = "YES" Then

                            VCanChange = ""


                            '#EK
                            ' Select  isnull((select top 1 CanChange from BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from BOS_OperatorWiseCommission OC  where  APIName='Recharge' and 	Category='Mobile' and 	Code='AR'
                            qryStr = "Select  isnull((select top 1 CanChange from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission OC  where  APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "'"
                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                                            End If
                                        End If

                                        If VCanChange.Trim.ToUpper = "NO" Then

                                            '/// NEED To CHANGE HERE EK

                                            Dim Amount1 As String = GV.parseString(Recharge_Amount)
                                            If Amount1.Trim = "" Then
                                                Amount1 = "0"
                                            End If
                                            Dim Amount As Decimal = Amount1

                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                                If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                                    VCustomer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                                End If
                                            End If

                                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                                If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                                    VCustomer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                                End If
                                            End If

                                            If VCustomer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                                VCustomerComAmt = Math.Round(((Amount * VCustomer_Commission) / 100), 2)
                                            ElseIf VCustomer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                                VCustomerComAmt = (VCustomer_Commission)
                                            End If

                                            '/////// End  Retailer


                                            VFinalCustomerAmt = VCustomerComAmt

                                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper




                                        End If

                                    End If
                                End If
                            End If





                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                            Dim Amount1 As String = GV.parseString(Recharge_Amount)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                    VCustomer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                    VCustomer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                End If
                            End If


                            If VCustomer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VCustomerComAmt = Math.Round(((Amount * VCustomer_Commission) / 100), 2)
                            ElseIf VCustomer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                VCustomerComAmt = (VCustomer_Commission)
                            End If

                            '/////// End Distributor

                            VFinalCustomerAmt = VCustomerComAmt


                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper



                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "NO" Then

                            '/// NEED To CHANGE HERE EK

                            Dim Amount1 As String = GV.parseString(Recharge_Amount)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_CommissionType")) Then
                                If Not ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString() = "" Then
                                    VCustomer_CommissionType = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_CommissionType").ToString())
                                End If
                            End If

                            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Customer_Commission")) Then
                                If Not ds.Tables(0).Rows(0).Item("Customer_Commission").ToString() = "" Then
                                    VCustomer_Commission = GV.parseString(ds.Tables(0).Rows(0).Item("Customer_Commission").ToString())
                                End If
                            End If

                            If VCustomer_CommissionType.Trim.ToUpper = "PERCENTAGE" Then
                                VCustomerComAmt = Math.Round(((Amount * VCustomer_Commission) / 100), 2)
                            ElseIf VCustomer_CommissionType.Trim.ToUpper = "AMOUNT" Then
                                VCustomerComAmt = (VCustomer_Commission)
                            End If

                            VFinalCustomerAmt = VCustomerComAmt
                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & CustomerID & ":" & VFinalCustomerAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper

                        End If


                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////

        Catch ex As Exception

        End Try
    End Sub
    Public Sub RechargeCommision(ByVal OperatorCategory As String, ByVal OperatorCode As String, ByVal gateway As String, ByVal Recharge_Amount As String)
        Try

            Dim VCommissionType, VSub_Dis_CommissionType, VRetailer_CommissionType As String
            VCommissionType = ""
            VSub_Dis_CommissionType = ""
            VRetailer_CommissionType = ""
            Dim VCommission, VSub_Dis_Commission, VRetailer_Commission As Decimal
            VCommission = 0
            VSub_Dis_Commission = 0
            VRetailer_Commission = 0


            Dim VContainCategory, VCanChange As String
            VContainCategory = ""
            VCanChange = ""


            Dim VadminComAmt, DistributorComAmt, SubDIsComAmt, VRetailerComAmt As Decimal
            VadminComAmt = 0
            DistributorComAmt = 0
            SubDIsComAmt = 0
            VRetailerComAmt = 0



            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim VFinaladminAmt, VFinalDISAmt, VFinalSUBDISAmt, VFinalRETAILERAmt As Decimal
            Dim SubDisID As String = ""
            Dim DisID As String = ""
            Dim AdminID As String = ""
            Dim qry As String = ""
            SubDisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "'")
            DisID = GV.FL.AddInVar("RefrenceID", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "'")
            'AdminID = GV.FL.AddInVar("RegistrationId", "BOS_APICommissionSettigs where RetailerID in select RegistrationID from BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & DisID & "'")

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='" & gateway & "' and ActiveStatus='Active'"
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



                        If VContainCategory.Trim.ToUpper = "YES" Then

                            VCanChange = ""


                            '#EK
                            ' Select  isnull((select top 1 CanChange from BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from BOS_OperatorWiseCommission OC  where  APIName='Recharge' and 	Category='Mobile' and 	Code='AR'
                            qryStr = "Select  isnull((select top 1 CanChange from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_APIVSCategory_Master where ProductService=OC.APIName and Category=OC.Category),'No') as 'CanChange',* from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission OC  where  APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "'"
                            ds = New DataSet
                            ds = GV.FL.OpenDsWithSelectQuery(qryStr)
                            If Not ds Is Nothing Then
                                If ds.Tables.Count > 0 Then
                                    If ds.Tables(0).Rows.Count > 0 Then

                                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("CanChange")) Then
                                            If Not ds.Tables(0).Rows(0).Item("CanChange").ToString() = "" Then
                                                VCanChange = GV.parseString(ds.Tables(0).Rows(0).Item("CanChange").ToString())
                                            End If
                                        End If

                                        If VCanChange.Trim.ToUpper = "YES" Then

                                            Dim Amount1 As String = GV.parseString(Recharge_Amount)
                                            If Amount1.Trim = "" Then
                                                Amount1 = "0"
                                            End If
                                            Dim Amount As Decimal = Amount1

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

                                            qry = " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "' and   RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                                            qry = qry & " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and 	Category='" & OperatorCategory & "' and 	Code='" & OperatorCode & "' and  RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




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







                                        ElseIf VCanChange.Trim.ToUpper = "NO" Then

                                            '/// NEED To CHANGE HERE EK

                                            Dim Amount1 As String = GV.parseString(Recharge_Amount)
                                            If Amount1.Trim = "" Then
                                                Amount1 = "0"
                                            End If
                                            Dim Amount As Decimal = Amount1

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
                                            lblRID.Text = "ADMIN" & ":" & VFinaladminAmt & "*" & DisID & ":" & VFinalDISAmt & "*" & SubDisID & ":" & VFinalSUBDISAmt & "*" & RetailerID & ":" & VFinalRETAILERAmt & "*" & "CanChange" & ":" & VCanChange.Trim.ToUpper




                                        End If

                                    End If
                                End If
                            End If





                        ElseIf VContainCategory.Trim.ToUpper = "NO" And VCanChange.Trim.ToUpper = "YES" Then

                            Dim Amount1 As String = GV.parseString(Recharge_Amount)
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



                            qry = " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and  RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                            qry = qry & " Select  * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='" & gateway & "' and  RegistrationID in (select RefrenceID from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




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

                            Dim Amount1 As String = GV.parseString(Recharge_Amount)
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


                    End If
                End If
            End If

            '/////////////////////////////////////////////////////////////

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_BBPS_Fetch_Click(sender As Object, e As EventArgs) Handles btn_BBPS_Fetch.Click
        Try
            Dim V_latitude, V_longitude As String
            V_latitude = "28.61492"
            V_longitude = "77.02272"

            lbl_BBPS_Fetch_Response.Text = ""
            lbl_BBPS_Fetch_Response.CssClass = ""
            ddl_BBPS_Operators.CssClass = "form-control"
            txt_BBPS_CA_No.CssClass = "form-control"

            'http://ip-api.com/json/103.77.42.184
            'If GV.parseString(txtLat.Text) = "" Or GV.parseString(txtLong.Text) = "" Then
            '    lbl_BBPS_Fetch_Response.Text = "Please Allow Location Access To Proceed."
            '    lbl_BBPS_Fetch_Response.Visible = True
            '    lbl_BBPS_Fetch_Response.CssClass = "errorLabels"
            '    Exit Sub
            'Else
            '    V_latitude = GV.parseString(txtLat.Text)
            '    V_longitude = GV.parseString(txtLong.Text)
            'End If



            If Not ddl_BBPS_Operators.Items.Count > 0 Then

            End If

            If ddl_BBPS_Operators.SelectedIndex = 0 Then
                ddl_BBPS_Operators.CssClass = "ValidationError"
                ddl_BBPS_Operators.Focus()
                Exit Sub
            End If

            If GV.parseString(txt_BBPS_CA_No.Text) = "" Then
                txt_BBPS_CA_No.CssClass = "ValidationError"
                txt_BBPS_CA_No.Focus()
                Exit Sub
            End If



            If btn_BBPS_Fetch.Text.Trim.ToUpper = "Pay Bill".ToUpper Then

                'Exit Sub

                Try
                    Dim client As New WebClient()
                    Dim baseurl As String = ("http://ip-api.com/json/" & GV.GetIPAddress())
                    Dim data As Stream = client.OpenRead(baseurl)
                    Dim reader As New StreamReader(data)
                    Dim s As String = reader.ReadToEnd()
                    data.Close()
                    reader.Close()
                    Dim json1 As JObject = JObject.Parse(s)
                    V_latitude = json1.SelectToken("lat").ToString
                    V_longitude = json1.SelectToken("lon").ToString
                Catch ex As Exception

                End Try



                'Dim vTransID As String = "20018575947" 'GV.FL.getAutoNumber("TransId")
                'Dim setParameter_API_Obj As New BILL_PAYMENT_PAY_BILL_API_Parameters
                'setParameter_API_Obj.canumber = GV.parseString(txt_BBPS_CA_No.Text)
                'setParameter_API_Obj.operator = GV.parseString(ddl_BBPS_Operators.SelectedValue)
                'setParameter_API_Obj.mode = GV.parseString(ddl_BBPS_Mode.SelectedValue)

                'setParameter_API_Obj.amount = GV.parseString(txt_BBPS_Amt.Text)
                'setParameter_API_Obj.latitude = "27.2232"
                'setParameter_API_Obj.longitude = "78.26535"
                'setParameter_API_Obj.referenceid = vTransID
                'setParameter_API_Obj.bill_fetch = """billAmount"":""820.0"",""billnetamount"":""820.0"",""billdate"":""01Jan1990"",""dueDate"":""2021-01-06"",""acceptPayment"":true,""acceptPartPay"":false,""cellNumber"":""102277100"",""userName"":""SALMAN"""
                ''{"operator":"11","canumber":"102277100","amount":"100","referenceid":"20018575947","latitude":"27.2232","longitude":"78.26535","mode":"online",
                ''"bill_fetch":{"billAmount":"820.0","billnetamount":"820.0","billdate":"01Jan1990","dueDate":"2021-01-06","acceptPayment":true,"acceptPartPay":false,"cellNumber":"102277100","userName":"SALMAN"}}



                'Dim StrParameters As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                'APIResult = ReadbyRestClient_NEW(BILL_PAYMENT_PAY_BILL_API_URL, StrParameters)

                'Dim setParameter_API_Obj1 As New BILL_PAYMENT_STATUS_ENQUIRY_API_Parameters
                'setParameter_API_Obj1.referenceid = vTransID


                'Dim StrParameters1 As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj1)
                'APIResult = ReadbyRestClient_NEW(BILL_PAYMENT_STATUS_ENQUIRY_API_URL , StrParameters1)

                Dim lineNo As Decimal = 0.0
                lineNo = 1
                Try


                    Dim RechargeAPI_Status As String = ""
                    Dim RechargeAPI As String = ""
                    If ddlGateway.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                        RechargeAPI = "RechargeAPI_Status"
                    ElseIf ddlGateway.SelectedValue.Trim.ToUpper = "Recharge-2".Trim.ToUpper Then
                        RechargeAPI = "RechargeAPI_2_Status"
                    End If
                    '///// Start Check API  STATUS Super Admin Level 
                    '///// Start Check API  STATUS Super Admin Level 

                    RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")
                    If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                        lbl_BBPS_Fetch_Response.Text = "Sorry! Recharge API Is Inactive At Company Level, Contact to Administrator"
                        lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                        Exit Sub
                    End If
                    '///// End Check API  STATUS Super Admin Level  

                    '///// Start Check API  STATUS System Settings 


                    RechargeAPI_Status = ""
                    RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

                    If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                        lbl_BBPS_Fetch_Response.Text = "Sorry! Recharge API Is Inactive At Admin Level, Contact to Administrator"
                        lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                        Exit Sub
                    End If

                    '///// End Check API  STATUS Retailer Level Settings 

                    Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

                    '///// Start Check API  STATUS System Settings 
                    RechargeAPI_Status = ""
                    RechargeAPI_Status = GV.FL.AddInVar("" & RechargeAPI & "", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

                    If Not RechargeAPI_Status.Trim.ToUpper = "ACTIVE" Then
                        lbl_BBPS_Fetch_Response.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                        lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                        Exit Sub
                    End If

                    '///// End Check API  STATUS Retailer Level  Settings 



                    Dim holdAmt As String = ""
                    holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
                    If holdAmt.Trim = "" Then
                        holdAmt = "0"
                    End If



                    Dim VServiceType, Voperator, Vcircle, Cus_MobileNo, VCus_Amount, VCus_Payable, VOperatorCode As String
                    VOperatorCode = ""
                    Vcircle = ""
                    Voperator = ""



                    VServiceType = lblSelectedService.Text.Trim
                    Dim ServiceName As String = VServiceType.Trim

                    lineNo = 2
                    If ServiceName.Trim.ToUpper = "Landline".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Operator"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter No. + STD Code"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If


                    ElseIf ServiceName.Trim.ToUpper = "Postpaid".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Operator"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter Mobile Number"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If
                    ElseIf ServiceName.Trim.ToUpper = "Electricity".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Operator"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter CA Number"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If

                    ElseIf ServiceName.Trim.ToUpper = "Broadband".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Operator"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter No. + STD Code"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If

                    ElseIf ServiceName.Trim.ToUpper = "LPG".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Operator"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter CA Number"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If

                    ElseIf ServiceName.Trim.ToUpper = "Waterbill".ToUpper Or ServiceName.Trim.ToUpper = "Water bill".ToUpper Or ServiceName.Trim.ToUpper = "Water".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Water Board"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter CA / RR No."
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If
                    ElseIf ServiceName.Trim.ToUpper = "LPG".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Operator"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter CA Number"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If

                    ElseIf ServiceName.Trim.ToUpper = "EMI".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Lender"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter Loan Account No."
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If

                    ElseIf ServiceName.Trim.ToUpper = "Municipality".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Corporation"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter Customer ID"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If


                    ElseIf ServiceName.Trim.ToUpper = "Cable".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Operator"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter Mobile / Acc No."
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If

                    ElseIf ServiceName.Trim.ToUpper = "Insurance".ToUpper Then

                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            Voperator = ddl_BBPS_Operators.SelectedItem.Text
                        Else
                            lbl_BBPS_Fetch_Response.Text = "Select Insurer"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            ddl_BBPS_Operators.CssClass = "ValidationError"
                            ddl_BBPS_Operators.Focus()
                            Exit Sub
                        End If
                        If Not ddl_BBPS_Operators.SelectedIndex = 0 Then
                            VOperatorCode = ddl_BBPS_Operators.SelectedValue
                        End If

                        If txt_BBPS_CA_No.Text = "" Then
                            lbl_BBPS_Fetch_Response.Text = "Enter Policy Number"
                            lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                            txt_BBPS_CA_No.Focus()
                            Exit Sub
                        End If

                    End If

                    Cus_MobileNo = GV.parseString(txt_BBPS_CA_No.Text.Trim)

                    If GV.parseString(txt_BBPS_Amt.Text) = "" Or txt_BBPS_Amt.Text = "0" Then
                        lbl_BBPS_Fetch_Response.Text = "Enter Amount"
                        lbl_BBPS_Fetch_Response.CssClass = "errorlabels"
                        txt_BBPS_Amt.Focus()
                        Exit Sub
                    Else
                        VCus_Amount = GV.parseString(txt_BBPS_Amt.Text.Trim)
                    End If

                    VCus_Payable = VCus_Amount
                    If VCus_Amount = "" Then
                        VCus_Amount = "0"
                    End If
                    Dim VNetAmount As Decimal = 0
                    VNetAmount = VCus_Amount


                    If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(VNetAmount) Then
                    Else
                        lblSearchCustomerError.Text = "You Have Insufficient Balance."
                        lblSearchCustomerError.CssClass = "errorlabels"
                        Exit Sub
                    End If


                    '///// Check For API Balance - Start //////
                    If CDec(VNetAmount) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                        lblSearchCustomerError.Text = "Insufficient API Balance."
                        lblSearchCustomerError.CssClass = "errorlabels"
                        Exit Sub
                    End If
                    '///// Check For API Balance - End //////


                    Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                    Dim VUpdatedBy, VUpdatedOn As String

                    VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                    VUpdatedOn = "getdate()"

                    'System.Threading.Thread.Sleep(2000)
                    lineNo = 3
                    If ddlGateway.SelectedValue.Trim.ToUpper = "Recharge".Trim.ToUpper Then
                        Dim TypeName As String = ""
                        If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Postpaid".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "dth".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Then
                            TypeName = "RECH"
                        ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "GAS".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Then
                            TypeName = "BILLPAY"
                        ElseIf lblSelectedService.Text.Trim.ToUpper = "Landline".Trim.ToUpper Then
                            TypeName = "Landline"
                        Else
                            TypeName = "BILLPAY"
                        End If

                        Dim QryStr As String = ""
                        lineNo = 4

                        If lblSessionFlag.Text = 0 Then

                            lblSessionFlag.Text = 9


                            'Dim VTransId As String = GV.parseString(GV.FL.getAutoNumber("TransId"))

                            'Dim setParameter_API_Obj As New DO_RECHARGE_API_Parameters
                            'setParameter_API_Obj.canumber = GV.parseString(txt_Mobile_CA_No.Text)
                            'setParameter_API_Obj.operator = GV.parseString(ddlOperators.SelectedValue)
                            'setParameter_API_Obj.referenceid = VTransId
                            'setParameter_API_Obj.amount = GV.parseString(txt_Recharge_Amt.Text.Trim)
                            'APIResult = ReadbyRestClient_NEW(DO_RECHARGE_API_URL, Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj))

                            Dim vTransID As String = GV.FL.getAutoNumber("TransId") '"20018575947" '
                            Dim setParameter_API_Obj As New BILL_PAYMENT_PAY_BILL_API_Parameters
                            Dim setbill_fetchParameter As New bill_fetch
                            '"bill_fetch":{"amount":"5700","name":"KRISHANGAHLOT","dueDate":"14-Sep-2022","billDate":"14-Sep-2022","billNumber":null},"message":"Bill Fetched Success."}
                            setbill_fetchParameter.acceptPartPay = ""
                            setbill_fetchParameter.acceptPayment = ""
                            setbill_fetchParameter.billAmount = ""
                            setbill_fetchParameter.billdate = ""
                            setbill_fetchParameter.billnetamount = ""
                            setbill_fetchParameter.cellNumber = ""
                            setbill_fetchParameter.dueDate = ""
                            setbill_fetchParameter.userName = ""

                            setParameter_API_Obj.canumber = GV.parseString(txt_BBPS_CA_No.Text)
                            setParameter_API_Obj.operator = GV.parseString(ddl_BBPS_Operators.SelectedValue)
                            setParameter_API_Obj.mode = GV.parseString(ddl_BBPS_Mode.SelectedValue)
                            setParameter_API_Obj.amount = GV.parseString(txt_BBPS_Amt.Text)
                            setParameter_API_Obj.latitude = V_latitude
                            setParameter_API_Obj.longitude = V_longitude
                            setParameter_API_Obj.referenceid = vTransID
                            setParameter_API_Obj.bill_fetch = lblBillFetch.Text.Trim  'Newtonsoft.Json.JsonConvert.SerializeObject(setbill_fetchParameter)
                            '{"operator":"11","canumber":"102277100","amount":"100","referenceid":"20018575947","latitude":"27.2232","longitude":"78.26535","mode":"online",
                            '"bill_fetch":{"billAmount":"820.0","billnetamount":"820.0","billdate":"01Jan1990","dueDate":"2021-01-06","acceptPayment":true,"acceptPartPay":false,"cellNumber":"102277100","userName":"SALMAN"}}


                            Dim StrParameters As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                            APIResult = ReadbyRestClient_NEW(BILL_PAYMENT_PAY_BILL_API_URL, StrParameters)




                            'VTransId = "157063"

                            Dim strBuild As String = ""
                            Dim v_response_code, v_message, v_operatorid, v_ackno, v_refid As String
                            Dim v_status As Boolean = False
                            v_message = ""
                            v_operatorid = ""
                            v_ackno = ""
                            v_refid = ""
                            v_response_code = ""
                            Dim json2 As JObject = JObject.Parse(APIResult)
                            Dim data1_ As List(Of JToken) = json2.Children().ToList
                            For Each item As JProperty In data1_
                                If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
                                    v_response_code = item.Value.ToString
                                ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
                                    v_status = item.Value
                                ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
                                    v_message = item.Value.ToString
                                ElseIf item.Name.ToString.Trim.ToUpper = "operatorid".ToString.Trim.ToUpper Then
                                    v_operatorid = item.Value.ToString
                                ElseIf item.Name.ToString.Trim.ToUpper = "ackno".ToString.Trim.ToUpper Then
                                    v_ackno = item.Value.ToString
                                ElseIf item.Name.ToString.Trim.ToUpper = "refid".ToString.Trim.ToUpper Then
                                    v_refid = item.Value.ToString
                                End If
                            Next
                            lineNo = 5

                            'Gateway 2
                            VTransId = VTransId
                            Dim Vurid As String = RetailerID
                            Dim Vmobile As String = Cus_MobileNo
                            Dim Vamount As String = VCus_Amount
                            Dim VoperatorId As String = VOperatorCode
                            Dim Verror_code As String = v_status
                            Dim Vservice As String = TypeName
                            Dim Vbal As String = "0"
                            Dim VcommissionBal As String = "0"
                            Dim VresText As String = v_message
                            Dim VbillAmount As String = "0"
                            Dim VbillName As String = ""
                            Dim VRecord_DateTime As String = "GetDate()"
                            Dim VorderId As String = v_refid
                            Dim VMode As String = ddl_BBPS_Mode.SelectedValue



                            QryStr = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API (Mode,TransIpAddress,Gateway,Refund_Status,TransId,RetailerID,Recharge_ServiceType,Recharge_Operator,Recharge_MobileNo_CaNo,Recharge_Amount,Recharge_PayableAmount,Recharge_Date,API_orderId,API_status,API_TransId,API_urid,API_mobile,API_amount,API_operatorId,API_error_code,API_service,API_bal,API_commissionBal,API_resText,API_billAmount,API_billName,UpdatedBy,UpdatedOn) values ('" & VMode & "','" & GV.parseString(GV.GetIPAddress) & "','1','No','" & GV.parseString(lblTransId.Text.Trim) & "','" & RetailerID & "','" & VServiceType & "','" & Voperator & "','" & Cus_MobileNo & "','" & VCus_Amount & "','" & VCus_Amount & "'," & VUpdatedOn & ",'" & VorderId & "','" & v_status & "','" & vTransID & "','" & Vurid & "','" & Vmobile & "','" & Vamount & "','" & VoperatorId & "','" & Verror_code & "','" & Vservice & "','" & Vbal & "','" & VcommissionBal & "','" & VresText & "','" & VbillAmount & "','" & VbillName & "','" & VUpdatedBy & "'," & VUpdatedOn & ") ; "
                            QryStr = QryStr & " " & " insert into " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info (RecordDatetime,API_TransId,Recharge_TransId,API_status,API_resText,CompanyCode,DBName) values(getdate(),'" & VTransId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(v_status) & "','" & GV.parseString(VresText) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "','" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "') ; "
                            lineNo = 6

                            If v_status = True Then
                                Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                                lineNo = 6.1
                                If GRP = "Retailer".ToUpper Then
                                    'IF Type is of Retailer
                                    lineNo = 6.2
                                    RechargeCommision(lblSelectedService.Text.Trim, VOperatorCode, "Recharge", txt_BBPS_Amt.Text.Trim)
                                    lineNo = 6.3
                                    If Not lblRID.Text = "" Then
                                        lineNo = 6.4
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

                                        lineNo = 7
                                        If vCanChange.Trim.ToUpper = "YES" Then
                                            Dim typeAmtForm As String = "Your Account is debited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim typeAmtTo As String = "Your Account is credited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                            Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                            Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txt_BBPS_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                            Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                            '//// Distributor Commission Calculation - Start
                                            V_Actual_Commission_Amt = 0
                                            V_GSTAmt = 0
                                            V_Commission_Without_GST = 0
                                            V_TDS_Amt = 0
                                            V_Net_Commission_Amt = 0
                                            If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                                If DisCom > 0 Then
                                                    V_Actual_Commission_Amt = DisCom
                                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                    DisCom = V_Net_Commission_Amt
                                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                                End If
                                            End If



                                            '//// Distributor Commission Calculation - End

                                            '//// SUB Distributor Commission Calculation - Start
                                            V_Actual_Commission_Amt = 0
                                            V_GSTAmt = 0
                                            V_Commission_Without_GST = 0
                                            V_TDS_Amt = 0
                                            V_Net_Commission_Amt = 0
                                            If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                                If SUBDisCom > 0 Then
                                                    V_Actual_Commission_Amt = SUBDisCom
                                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                    SUBDisCom = V_Net_Commission_Amt
                                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                                End If
                                            End If

                                            lineNo = 8
                                            '//// SUB Distributor Commission Calculation - End

                                            '//// Retailer Commission Calculation - Start
                                            V_Actual_Commission_Amt = 0
                                            V_GSTAmt = 0
                                            V_Commission_Without_GST = 0
                                            V_TDS_Amt = 0
                                            V_Net_Commission_Amt = 0

                                            If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                                If RTECom > 0 Then
                                                    V_Actual_Commission_Amt = RTECom
                                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                    RTECom = V_Net_Commission_Amt
                                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                                End If

                                            End If


                                            '//// Retailer Commission Calculation - END


                                        Else
                                            'vCanChange.Trim.ToUpper = "No"
                                            lineNo = 9
                                            Dim typeAmtForm As String = "Your Account is debited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim typeAmtTo As String = "Your Account is credited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                            Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                            Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                            Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RTEID & "','Admin','" & txt_BBPS_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                            Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                            '//// Distributor Commission Calculation - Start
                                            V_Actual_Commission_Amt = 0
                                            V_GSTAmt = 0
                                            V_Commission_Without_GST = 0
                                            V_TDS_Amt = 0
                                            V_Net_Commission_Amt = 0

                                            If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                                If DisCom > 0 Then
                                                    V_Actual_Commission_Amt = DisCom
                                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                    DisCom = V_Net_Commission_Amt
                                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                                End If

                                            End If


                                            '//// Distributor Commission Calculation - End
                                            lineNo = 10
                                            '//// SUB Distributor Commission Calculation - Start
                                            V_Actual_Commission_Amt = 0
                                            V_GSTAmt = 0
                                            V_Commission_Without_GST = 0
                                            V_TDS_Amt = 0
                                            V_Net_Commission_Amt = 0

                                            If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then

                                                If SUBDisCom > 0 Then
                                                    V_Actual_Commission_Amt = SUBDisCom
                                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                    SUBDisCom = V_Net_Commission_Amt
                                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "', '" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                                End If

                                            End If

                                            '//// SUB Distributor Commission Calculation - End

                                            '//// Retailer Commission Calculation - Start
                                            V_Actual_Commission_Amt = 0
                                            V_GSTAmt = 0
                                            V_Commission_Without_GST = 0
                                            V_TDS_Amt = 0
                                            V_Net_Commission_Amt = 0

                                            If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                                If RTECom > 0 Then
                                                    V_Actual_Commission_Amt = RTECom
                                                    V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                    V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                    V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                    V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                    RTECom = V_Net_Commission_Amt
                                                    QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                                End If

                                            End If


                                            '//// Retailer Commission Calculation - END


                                        End If

                                        lineNo = 11
                                        Dim ServiceCharge As Decimal = 0
                                        If CDec(GV.parseString(ServiceCharge)) > 0 Then
                                            ServiceCharge = ServiceCharge
                                            'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                            '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                            'Else
                                            '    ServiceCharge = 10
                                            'End If
                                            If ServiceCharge > 0 Then
                                                Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                            End If

                                        End If

                                        ''////// Service Charge For Admin To SuperAdmin - Start
                                        Dim NetAmount As Decimal = 0
                                        Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddl_BBPS_Operators.SelectedItem.Text) & "'").Split(":")
                                        If Service.Length > 1 Then
                                            If Service(1).Trim = "Percentage" Then
                                                NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                            ElseIf Service(1).Trim = "Amount" Then
                                                NetAmount = CDec(Service(0))
                                            ElseIf Service(1).Trim = "Not Applicable" Then
                                                NetAmount = CDec(Service(0))
                                            End If
                                        End If
                                        If NetAmount > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        ''///////  Service Charge For Admin To SuperAdmin - End
                                        lineNo = 12


                                    Else

                                        ' Retailer
                                        Dim typeAmtForm As String = "Your Account is debited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        Dim typeAmtTo As String = "Your Account is credited by " & txt_Recharge_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_Mobile_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & RetailerID & "','Admin','" & txt_BBPS_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        Dim ServiceCharge As Decimal = 0
                                        If ServiceCharge > 0 Then
                                            ServiceCharge = ServiceCharge
                                            If ServiceCharge > 0 Then
                                                Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                            End If

                                        End If
                                        lineNo = 13

                                        ''////// Service Charge For Admin To SuperAdmin - Start
                                        Dim NetAmount As Decimal = 0
                                        Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddl_BBPS_Operators.SelectedItem.Text.Trim) & "'").Split(":")
                                        If Service.Length > 1 Then
                                            If Service(1).Trim = "Percentage" Then
                                                NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                            ElseIf Service(1).Trim = "Amount" Then
                                                NetAmount = CDec(Service(0))
                                            ElseIf Service(1).Trim = "Not Applicable" Then
                                                NetAmount = CDec(Service(0))
                                            End If
                                        End If
                                        If NetAmount > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        ''///////  Service Charge For Admin To SuperAdmin - End

                                    End If
                                    lineNo = 14
                                ElseIf GRP = "Customer".ToUpper Then
                                    'In case of Customer 

                                    '//// Customer Commission Calculation - Start
                                    RechargeCommision_Customer(lblSelectedService.Text.Trim, VOperatorCode, "Recharge", txt_BBPS_Amt.Text.Trim)
                                    If Not lblRID.Text = "" Then

                                        Dim AAID() As String = lblRID.Text.Split("*")
                                        Dim Adminid_Com() As String = AAID(0).Split(":")
                                        Dim CustID_Com() As String = AAID(1).Split(":")
                                        Dim adminID As String = Adminid_Com(0)
                                        Dim adminCom As String = Adminid_Com(1)

                                        Dim CustID As String = CustID_Com(0)
                                        Dim CustCom As String = CustID_Com(1)



                                        Dim typeAmtForm As String = "Your Account is debited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        Dim typeAmtTo As String = "Your Account is credited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                        Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                        Dim CustTypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        Dim CustTypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        lineNo = 15

                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & CustID & "','Admin','" & txt_BBPS_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                        Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal
                                        '//// Customer Commission Calculation - Start
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                            If CustCom > 0 Then
                                                V_Actual_Commission_Amt = CustCom
                                                V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                CustCom = V_Net_Commission_Amt
                                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CustTypecommTo & "','" & CustTypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                            End If
                                        End If


                                        lineNo = 16

                                        Dim ServiceCharge As Decimal = 0
                                        If ServiceCharge > 0 Then
                                            ServiceCharge = ServiceCharge
                                            'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                            '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                            'Else
                                            '    ServiceCharge = 10
                                            'End If
                                            If ServiceCharge > 0 Then
                                                Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                            End If

                                        End If
                                        lineNo = 17
                                        ''////// Service Charge For Admin To SuperAdmin - Start
                                        Dim NetAmount As Decimal = 0
                                        Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddl_BBPS_Operators.SelectedItem.Text.Trim) & "'").Split(":")
                                        If Service.Length > 1 Then
                                            If Service(1).Trim = "Percentage" Then
                                                NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                            ElseIf Service(1).Trim = "Amount" Then
                                                NetAmount = CDec(Service(0))
                                            ElseIf Service(1).Trim = "Not Applicable" Then
                                                NetAmount = CDec(Service(0))
                                            End If
                                        End If
                                        If NetAmount > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        ''///////  Service Charge For Admin To SuperAdmin - End

                                        '//// Customer Commission Calculation - END

                                        lineNo = 18
                                    Else
                                        Dim typeAmtForm As String = "Your Account is debited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        Dim typeAmtTo As String = "Your Account is credited by " & txt_BBPS_Amt.Text.Trim & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                        QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & vTransID & "','" & VCus_Amount & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date & "','" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','Admin','" & txt_Recharge_Amt.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        lineNo = 19
                                        Dim ServiceCharge As Decimal = 0
                                        If CDec(ServiceCharge) > 0 Then
                                            ServiceCharge = ServiceCharge
                                            'If CDec(GV.parseString(txtServiceCharge.Text.Trim)) > 10 Then
                                            '    ServiceCharge = GV.parseString(txtServiceCharge.Text.Trim)
                                            'Else
                                            '    ServiceCharge = 10
                                            'End If
                                            If ServiceCharge > 0 Then
                                                Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                Dim VTo As String = "Your Account is credited by ServiceCharge " & ServiceCharge & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','" & RTE & "','ADMIN','" & ServiceCharge & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                            End If

                                        End If
                                        lineNo = 20
                                        ''////// Service Charge For Admin To SuperAdmin - Start
                                        Dim NetAmount As Decimal = 0
                                        Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", "" & GV.DefaultDatabase.Trim & ".dbo.BOS_OperatorWiseCommissionVsAdmin_SA where AdminID='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' and   APIName='" & GV.parseString(ddlGateway.SelectedValue.Trim) & "' and OperatorName='" & GV.parseString(ddl_BBPS_Operators.SelectedItem.Text.Trim) & "'").Split(":")
                                        If Service.Length > 1 Then
                                            If Service(1).Trim = "Percentage" Then
                                                NetAmount = (CDec(txt_Recharge_Amt.Text.Trim) * CDec(Service(0))) / 100
                                            ElseIf Service(1).Trim = "Amount" Then
                                                NetAmount = CDec(Service(0))
                                            ElseIf Service(1).Trim = "Not Applicable" Then
                                                NetAmount = CDec(Service(0))
                                            End If
                                        End If
                                        If NetAmount > 0 Then
                                            Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & " - " & RTE & " ."
                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','ADMIN','SUPER ADMIN','" & NetAmount & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If
                                        ''///////  Service Charge For Admin To SuperAdmin - End
                                        lineNo = 21
                                    End If

                                End If

                                'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                                '//// Admin & Super Admin Commission Calculation - Start
                                If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                                    '//// Admin Commission Calculation - Start
                                    Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Result As String
                                    Dim V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                                    If GV.parseString(txt_BBPS_Amt.Text.Trim) = "" Then
                                        V_Amount = "0"
                                    Else
                                        V_Amount = txt_BBPS_Amt.Text.Trim
                                    End If
                                    lineNo = 22
                                    V_OperatorCategory = lblSelectedService.Text.Trim
                                    V_OperatorCode = VOperatorCode
                                    V_APIName = "Recharge"
                                    V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                                    Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)
                                    lineNo = 23
                                    If Not GV.parseString(Result) = "" Then
                                        Dim Result_Arry() As String = Result.Split("*")
                                        Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                        Dim Admin_Com_ID As String = "Admin"
                                        Dim Admin_Com_Amt As String = Admin_Com(1)

                                        Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                        Dim Service_Charge_ID As String = ""
                                        Dim Service_Charge_Amt As String = Service_Charge(1)


                                        If Service_Charge_Amt > 0 Then
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If

                                        Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        lineNo = 24
                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0

                                        If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                            If Admin_Com_Amt > 0 Then
                                                V_Actual_Commission_Amt = Admin_Com_Amt
                                                V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                Admin_Com_Amt = V_Net_Commission_Amt
                                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                            End If

                                        End If


                                    End If
                                    lineNo = 25
                                    '//// Admin Commission Calculation - End

                                    '//// Super Admin Commission Calculation - Start
                                    Result = GV.Commision_Calculation_For_SuperAdmin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName)

                                    If Not GV.parseString(Result) = "" Then
                                        Dim Result_Arry() As String = Result.Split("*")
                                        Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                        Dim Admin_Com_ID As String = "Super Admin"
                                        Dim Admin_Com_Amt As String = Admin_Com(1)

                                        Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                        Dim Service_Charge_ID As String = ""
                                        Dim Service_Charge_Amt As String = Service_Charge(1)


                                        If Service_Charge_Amt > 0 Then
                                            Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                            Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                            QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & VTransId & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                        End If

                                        Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."
                                        Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_BBPS_CA_No.Text.Trim & " / AMT " & VCus_Amount & "."

                                        V_Actual_Commission_Amt = 0
                                        V_GSTAmt = 0
                                        V_Commission_Without_GST = 0
                                        V_TDS_Amt = 0
                                        V_Net_Commission_Amt = 0
                                        lineNo = 26
                                        If GV.parseString(ddl_BBPS_Mode.SelectedValue).Trim.ToUpper = "OFFLINE" Then
                                            If Admin_Com_Amt > 0 Then
                                                V_Actual_Commission_Amt = Admin_Com_Amt
                                                V_GSTAmt = Math.Round((V_Actual_Commission_Amt - (GST_Per * V_Actual_Commission_Amt)), 2)
                                                V_Commission_Without_GST = Math.Round((GST_Per * V_Actual_Commission_Amt), 2)
                                                V_TDS_Amt = Math.Round((GST_Per * V_Actual_Commission_Amt * TDS_Per), 2)
                                                V_Net_Commission_Amt = Math.Round(((GST_Per * V_Actual_Commission_Amt) - (GST_Per * V_Actual_Commission_Amt * TDS_Per)), 2)
                                                Admin_Com_Amt = V_Net_Commission_Amt
                                                QryStr = QryStr & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & vTransID & "','" & VCus_Amount & "', '" & GV.parseString(lblTransId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                            End If
                                        End If



                                    End If
                                    '//// Super Admin Commission Calculation - End
                                End If
                                '//// Admin & Super Admin Commission Calculation - End
                                'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                                lineNo = 27
                            Else
                                '// False


                            End If


                            Try
                                GV.SaveTextToFile(QryStr, Server.MapPath("RECHARGE_API_LOG.txt"), True)
                            Catch ex As Exception

                            End Try


                            If GV.FL.DMLQueriesBulk(QryStr) = True Then
                                lineNo = 28
                                lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                                lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")
                                lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                                lblPopTransactionID.Text = vTransID
                                lblPopTransactionAmt.Text = GV.parseString(txt_BBPS_Amt.Text.Trim)
                                lblPopStatus.Text = v_status
                                lblpopOperator.Text = TypeName.Trim
                                lblpopMobileNo.Text = txt_BBPS_CA_No.Text.Trim

                                ModalPopupExtender3.Show()

                            Else
                                lineNo = 29

                                lblDialogMsg.Text = "Sorry !! Process Can't be Completed."
                                lblDialogMsg.CssClass = "errorlabels"
                                btnCancel.Text = "Ok"
                                btnok.Visible = False
                                ModalPopupExtender1.Show()
                            End If




                        End If





                    End If

                    'lblSearchCustomerError.Text = APIResult
                Catch ex As Exception
                    lblSearchCustomerError.Text = lineNo & " : " & ex.Message & " : " & APIResult
                End Try

            ElseIf btn_BBPS_Fetch.Text.Trim.ToUpper = "Get Bill".ToUpper Then
                lblBillFetch.Text = ""

                Dim APIStatus_BBPS_Fetch As Boolean = False
                Dim setParameter_API_Obj As New BILL_PAYMENT_FETCH_API_Parameters
                setParameter_API_Obj.canumber = GV.parseString(txt_BBPS_CA_No.Text)
                setParameter_API_Obj.operator = GV.parseString(ddl_BBPS_Operators.SelectedValue)
                setParameter_API_Obj.mode = GV.parseString(ddl_BBPS_Mode.SelectedValue)
                Dim StrParameters As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                APIResult = ReadbyRestClient_NEW(BILL_PAYMENT_FETCH_API_URL, StrParameters)
                Dim dt As New DataTable
                Dim dc1 As DataColumn = New DataColumn("CA_No")
                Dim dc2 As DataColumn = New DataColumn("Name")
                Dim dc3 As DataColumn = New DataColumn("Amount")
                Dim dc4 As DataColumn = New DataColumn("BillDate")
                Dim dc5 As DataColumn = New DataColumn("Duedate")
                Dim dc6 As DataColumn = New DataColumn("Message")
                dt.Columns.Add(dc1)
                dt.Columns.Add(dc2)
                dt.Columns.Add(dc3)
                dt.Columns.Add(dc4)
                dt.Columns.Add(dc5)
                dt.Columns.Add(dc6)

                Dim vCA_No As String = GV.parseString(txt_BBPS_CA_No.Text)
                Dim vName As String = ""
                Dim vAmount As String = ""
                Dim vDuedate As String = ""
                Dim vMessage As String = ""
                Dim vBilldate As String = ""
                Dim vResponse_Code As String = ""





                ''{"status":true,"response_code":1,"amount":"5700","name":"KRISHANGAHLOT","billdate":"14-Sep-2022","duedate":"14-Sep-2022",
                '"bill_fetch":{"amount":"5700","name":"KRISHANGAHLOT","dueDate":"14-Sep-2022","billDate":"14-Sep-2022","billNumber":null},"message":"Bill Fetched Success."}

                Dim json2 As JObject = JObject.Parse(APIResult)
                Dim data1_ As List(Of JToken) = json2.Children().ToList
                For Each item As JProperty In data1_
                    If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
                        strBuild = item.Value.ToString
                        vResponse_Code = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
                        APIStatus_BBPS_Fetch = item.Value
                        strBuild = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
                        strBuild = item.Value.ToString
                        vMessage = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "amount".ToString.Trim.ToUpper Then
                        vAmount = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "duedate".ToString.Trim.ToUpper Then
                        vDuedate = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "billDate".ToString.Trim.ToUpper Then
                        vBilldate = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "name".ToString.Trim.ToUpper Then
                        vName = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "bill_fetch".ToString.Trim.ToUpper Then
                        lblBillFetch.Text = item.Value.ToString

                    ElseIf item.Name.ToString.Trim.ToUpper = "info".ToString.Trim.ToUpper Then
                        'Dim data1 As List(Of JToken) = item.Children().ToList
                        'Dim data2_ As List(Of JToken) = data1.Children().ToList
                        'For Each msg1 As JProperty In data2_
                        '    '/// Dynamic Name and get its value


                        '    vType = msg1.Name
                        '    Dim data3_ As List(Of JToken) = msg1.Children().ToList
                        '    For Each pnk As JArray In data3_
                        '        For k As Integer = 0 To pnk.Count - 1
                        '            strBuild = pnk(k).ToString
                        '            Dim json4 As JObject = JObject.Parse(strBuild)
                        '            Dim datap_ As List(Of JToken) = json4.Children().ToList
                        '            For Each item2 As JProperty In datap_
                        '                If item2.Name.ToString.Trim.ToUpper = "Rs".ToString.Trim.ToUpper Then
                        '                    vRs = item2.Value
                        '                ElseIf item2.Name.ToString.Trim.ToUpper = "Desc".ToString.Trim.ToUpper Then
                        '                    vDes = item2.Value
                        '                ElseIf item2.Name.ToString.Trim.ToUpper = "validity".ToString.Trim.ToUpper Then
                        '                    vvalidity = item2.Value
                        '                ElseIf item2.Name.ToString.Trim.ToUpper = "last_update".ToString.Trim.ToUpper Then
                        '                    vlast_update = item2.Value
                        '                End If
                        '            Next

                        '            If Not vRs.Trim = "" Then
                        '                Dim dr1 As DataRow = dt.NewRow()
                        '                dr1(0) = vType
                        '                dr1(1) = vRs
                        '                dr1(2) = vDes
                        '                dr1(3) = vvalidity
                        '                'dr1(4) = vlast_update
                        '                dt.Rows.Add(dr1)
                        '            End If


                        '        Next

                        '    Next

                        'Next
                    End If
                Next

                If APIStatus_BBPS_Fetch = True Then

                    Dim dr1 As DataRow = dt.NewRow()
                    dr1(0) = vCA_No
                    dr1(1) = vName
                    dr1(2) = vAmount
                    dr1(3) = vBilldate
                    dr1(4) = vDuedate
                    dr1(5) = vMessage
                    dt.Rows.Add(dr1)

                    Divgrid_plan.Visible = True
                    grdResponseDetails.DataSource = dt.DefaultView
                    grdResponseDetails.DataBind()
                    grdResponseDetails.Visible = True


                Else
                    Dim dt1 As New DataTable
                    Dim dc11 As DataColumn = New DataColumn("Response_Code")
                    Dim dc21 As DataColumn = New DataColumn("Status")
                    Dim dc31 As DataColumn = New DataColumn("Message")
                    dt1.Columns.Add(dc11)
                    dt1.Columns.Add(dc21)
                    dt1.Columns.Add(dc31)


                    Dim dr11 As DataRow = dt1.NewRow()
                    dr11(0) = vResponse_Code
                    dr11(1) = APIStatus_BBPS_Fetch
                    dr11(2) = vMessage
                    dt1.Rows.Add(dr11)

                    Divgrid_plan.Visible = True
                    grdResponseDetails.DataSource = dt1.DefaultView
                    grdResponseDetails.DataBind()
                    grdResponseDetails.Visible = True

                    For i As Integer = 0 To grdResponseDetails.Rows.Count - 1
                        Dim btnRefund As LinkButton = DirectCast(grdResponseDetails.Rows(i).FindControl("LinkButton1"), LinkButton)
                        btnRefund.Visible = False
                    Next

                End If

            End If




        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBrowsePlan_Click(sender As Object, e As EventArgs) Handles btnBrowsePlan.Click
        Try


            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""
            ddlOperators.CssClass = "form-control"
            ddlCircle.CssClass = "form-control"



            If ddlOperators.SelectedIndex = 0 Then
                lblSearchCustomerError.Text = "Select Operators"
                lblSearchCustomerError.CssClass = "errorlabels"
                ddlOperators.CssClass = "ValidationError"
                ddlOperators.Focus()
                Exit Sub
            End If
            If ddlCircle.SelectedIndex = 0 Then
                lblSearchCustomerError.Text = "Select Circle"
                lblSearchCustomerError.CssClass = "errorlabels"
                ddlCircle.CssClass = "ValidationError"
                ddlCircle.Focus()
                Exit Sub
            End If

            Dim APIStatus_Browse_Plan As Boolean = False

            Dim setParameter_API_Obj As New BROWSE_PLAN_API_Parameters
            setParameter_API_Obj.circle = GV.parseString(ddlCircle.SelectedValue)
            setParameter_API_Obj.op = GV.parseString(ddlOperators.SelectedItem.Text)
            Dim StrParameters As String = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
            APIResult = ReadbyRestClient_NEW(BROWSE_PLAN_API_URL, StrParameters)

            'Type , Rs , Des,validity ,last_update
            Dim dt As New DataTable
            Dim dc1 As DataColumn = New DataColumn("Type")
            Dim dc2 As DataColumn = New DataColumn("Rs")
            Dim dc3 As DataColumn = New DataColumn("Description")
            Dim dc4 As DataColumn = New DataColumn("Validity")
            'Dim dc5 As DataColumn = New DataColumn("last_update")

            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)
            dt.Columns.Add(dc3)
            dt.Columns.Add(dc4)
            'dt.Columns.Add(dc5)

            'Dim planStr As String = "{ ""status "":true, ""response_code "":1, ""info "":{ ""TOPUP "":[{ ""rs "": ""10 "", ""desc "": ""Talktime of Rs. 7.47 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""20 "", ""desc "": ""Talktime of Rs. 14.95 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""100 "", ""desc "": ""Talktime of Rs. 81.75 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""500 "", ""desc "": ""Talktime of Rs 423.73 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1000 "", ""desc "": ""Talktime of Rs 847.46 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5000 "", ""desc "": ""Talktime of Rs 4237.29 "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""}], ""3G\/4G "":[{ ""rs "": ""19 "", ""desc "": ""Enjoy 1GB data, valid for 1 day "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""58 "", ""desc "": ""3 GB | Validity same as your existing bundle \/ smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""98 "", ""desc "": ""Wynk Premium Data Pack5 GB Data | 30 days of Wynk Premium subscription "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""108 "", ""desc "": ""PVME Data Pack6 GB Data | 28 days of Prime Video Mobile Edition subscription "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""118 "", ""desc "": ""12 GB | Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""148 "", ""desc "": ""Xstream Mobile Data Pack 15GB Data |28 days access to any 1 channel (SonyLiv, LionsgatePlay, ErosNow, HoiChoi, ManoramaMAX) on Airtel Xstream App at no extra cost, Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""301 "", ""desc "": ""50 GB , 1year of Wynk Music Premium subscription, Validity same as your existing bundle \/smart pack "", ""validity "": ""N\/A "", ""last_update "": ""30-06-2022 ""}], ""RATE CUTTER "":[{ ""rs "": ""99 "", ""desc "": ""Rs 99 Talktime 200 MB Data Local\/STD\/LL @ 2.5\/sec Tariff Calls 28 Days Validity "", ""validity "": ""28 days "", ""last_update "": ""01-01-1970 ""}], ""Romaing "":[{ ""rs "": ""18 "", ""desc "": ""Enjoy ISD calling at discounted rates for 28 days. For country wise tariff visit www.airtel.in "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""496 "", ""desc "": ""Unlimited incoming, 500 MB, 100 min local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""648 "", ""desc "": ""100 mins incoming, 500 MB, 100 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""649 "", ""desc "": ""Unlimited incoming, 500 MB, 100 min local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""755 "", ""desc "": ""1 GB data, valid for 5 days.- Covered Countries: UAE, Saudi Arabia, Malaysia, USA, Oman, Qatar, UK, Kuwait, Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""5 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1499 "", ""desc "": ""Unlimited incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""2499 "", ""desc "": ""Unlimited incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3598 "", ""desc "": ""250mins incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3599 "", ""desc "": ""Unlimited incoming, 3 GB, 250 mins local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3997 "", ""desc "": ""Unlimited incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : USA, UK, Canada, Singapore, Thailand, Malasiya, Australia, France, Germany, Netherlands, China, Indonesia, Hong Kong & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3998 "", ""desc "": ""500 mins incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : UAE, Saudi Arabia, Oman, Kuwait, Bahrain, Qatar, Iran, Iraq, Russia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""4999 "", ""desc "": ""Unlimited incoming, 1 GB\/day, 500 mins of local\/India calls,100 sms - Covered Countries :UAE, Saudi Arabia, Malaysia, USA, Oman, Qatar, UK, Kuwait, Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5997 "", ""desc "": ""Details: Unlimited incoming, 1800 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5998 "", ""desc "": ""Details: Unlimited incoming, 1800 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, Friance, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""5999 "", ""desc "": ""Details: 300 mins Incoming, 300 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""90 days "", ""last_update "": ""30-06-2022 ""}], ""COMBO "":[{ ""rs "": ""128 "", ""desc "": ""Enjoy Local & STD calls 2.5p\/sec National Video Calls 5p\/sec DATA 50p\/MB; SMS Rs.1 Local Rs.1.5 STD Rs.5 ISD "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""155 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data, 300 National SMS for 24 days.  "", ""validity "": ""24 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""179 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 2 GB data, 300 National SMS for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""209 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, daily 1GB data and 100 SMS  "", ""validity "": ""21 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""239 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data per day, 100 National SMS\/day for 24 days.  "", ""validity "": ""24 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""265 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1 GB data per day, 100 National SMS\/day for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""296 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 25GB Data and 100 SMS\/day  "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""299 "", ""desc "": ""Enjoy 1.5GB\/day data, unlimited Local, STD & Roaming calls on any network and 100 SMS\/day.  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""319 "", ""desc "": ""Enjoy unlimited Local STD & Roaming calls on any network 2GB\/day Data and 100 SMS\/day for 1 Month  "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""359 "", ""desc "": ""Enjoy Unlimited Local, STD & Roaming calls Local STD & Roaming calls on any network daily 2GB data and 100 SMS. Pack valid for 28 days. Also get Amazon Prime Video Mobile Edition & 1 Airtel Xstream channel for 28 days  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""399 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 2.5GB\/day data and 100 SMS.  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""455 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 6GB data, 900 National SMS for 84 days  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""479 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1.5 GB data per day, 100 National SMS\/day for 56 days  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""549 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, daily 2GB data and 100 SMS  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""599 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 3 GB data per day, 100 National SMS\/day for 28 days and Disney+ Hostar Mobile for 1 year  "", ""validity "": ""28 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""666 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 1.5 GB data per day, 100 National SMS\/day for 77 days  "", ""validity "": ""77 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""699 "", ""desc "": ""Enjoy Unlimited Local, STD & Roaming calls Local STD & Roaming calls on any network daily 3GB data and 100 SMS  "", ""validity "": ""56 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""719 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 1.5GB\/day data and 100 SMS  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""799 "", ""desc "": ""100mins of Incoming\/Outgoing (India+Local) calls. Covered Countries : UAE Saudi Arabia Malaysia USA Oman Qatar UK Kuwait Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""839 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, 2GB\/day data and 100 SMS  "", ""validity "": ""84 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""997 "", ""desc "": ""100 mins incoming, 500 MB, 100 mins local\/India calls, 100 sms - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""1 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1199 "", ""desc "": ""Details: 1 GB Data, 100mins of Incoming\/Outgoing (India+Local) calls. Covered Countries : UAE Saudi Arabia Malaysia USA Oman Qatar UK Kuwait Singapore & more. Visit www.airtel.in\/IR "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""1799 "", ""desc "": ""Enjoy TRULY unlimited Local, STD & Roaming calls on any network, 24GB data, 3600 National SMS for 365 days  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""2999 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network within India, daily 2GB data & 100 SMS  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3359 "", ""desc "": ""Enjoy unlimited Local, STD & Roaming calls on any network, daily 2.5GB & 100 SMS. Pack Valid for 365 days  "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""3995 "", ""desc "": ""250 mins incoming, 3 GB, 250 mins local\/India calls, 100 SMS - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""10 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""6999 "", ""desc "": ""500 mins incoming, 5 GB, 500 mins local\/India calls, 100 sms - Covered Countries : Maldives, Nigeria, South Africa, Kenya, Uganda, Zambia & more countries. Visit www.airtel.in\/ir "", ""validity "": ""30 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8997 "", ""desc "": ""Details: Unlimited incoming, 3600 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8998 "", ""desc "": ""Details: Unlimited incoming, 3600 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, France, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""8999 "", ""desc "": ""Details: 600 mins Incoming, 600 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""180 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14997 "", ""desc "": ""Details: Unlimited incoming, 7200 mins India calls, 100 sms.| Covered Countries : Bangladesh, Bhutan, Myanmar, Nepal, Sri Lanka. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14998 "", ""desc "": ""Details: Unlimited incoming, 7200 mins India calls, 100 sms.| Covered Countries : Australia, Canada, China, France, Japan, Singapore, USA, UK & more. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""},{ ""rs "": ""14999 "", ""desc "": ""Details: 1200 mins Incoming, 1200 mins India calls, 100 sms.| Covered Countries :Iran, Kuwait, Oman, Qatar, Russia, Saudi Arabia, UAE & more. Visit www.airtel.in\/IR "", ""validity "": ""365 days "", ""last_update "": ""30-06-2022 ""}]}, ""message "": ""Fetch Successful ""}"
            Dim json2 As JObject = JObject.Parse(APIResult)
            Dim data1_ As List(Of JToken) = json2.Children().ToList
            For Each item As JProperty In data1_
                item.CreateReader()
                If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
                    strBuild = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
                    APIStatus_Browse_Plan = item.Value
                    strBuild = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
                    strBuild = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "info".ToString.Trim.ToUpper Then
                    Dim data1 As List(Of JToken) = item.Children().ToList
                    Dim data2_ As List(Of JToken) = data1.Children().ToList
                    For Each msg1 As JProperty In data2_
                        '/// Dynamic Name and get its value
                        Dim vType As String = ""
                        Dim vRs As String = ""
                        Dim vDes As String = ""
                        Dim vvalidity As String = ""
                        Dim vlast_update As String = ""

                        vType = msg1.Name
                        Dim data3_ As List(Of JToken) = msg1.Children().ToList
                        For Each pnk As JArray In data3_
                            For k As Integer = 0 To pnk.Count - 1
                                strBuild = pnk(k).ToString
                                Dim json4 As JObject = JObject.Parse(strBuild)
                                Dim datap_ As List(Of JToken) = json4.Children().ToList
                                For Each item2 As JProperty In datap_
                                    If item2.Name.ToString.Trim.ToUpper = "Rs".ToString.Trim.ToUpper Then
                                        vRs = item2.Value
                                    ElseIf item2.Name.ToString.Trim.ToUpper = "Desc".ToString.Trim.ToUpper Then
                                        vDes = item2.Value
                                    ElseIf item2.Name.ToString.Trim.ToUpper = "validity".ToString.Trim.ToUpper Then
                                        vvalidity = item2.Value
                                    ElseIf item2.Name.ToString.Trim.ToUpper = "last_update".ToString.Trim.ToUpper Then
                                        vlast_update = item2.Value
                                    End If
                                Next

                                If Not vRs.Trim = "" Then
                                    Dim dr1 As DataRow = dt.NewRow()
                                    dr1(0) = vType
                                    dr1(1) = vRs
                                    dr1(2) = vDes
                                    dr1(3) = vvalidity
                                    'dr1(4) = vlast_update
                                    dt.Rows.Add(dr1)
                                End If


                            Next

                        Next

                    Next
                End If
            Next


            If APIStatus_Browse_Plan = True Then
                Divgrid_plan.Visible = True
                grdResponseDetails.DataSource = dt.DefaultView
                grdResponseDetails.DataBind()
                grdResponseDetails.Visible = True

            Else
                Divgrid_plan.Visible = False
                grdResponseDetails.DataSource = Nothing
                grdResponseDetails.DataBind()
            End If


        Catch ex As Exception
            lblSearchCustomerError.Text = ex.Message
        End Try
    End Sub



    Dim APIResult As String = ""
    Dim strBuild As String = ""
    Public Sub reset_button_css()
        Try
            btnmobile.CssClass = "btn btn-danger mar_top10"
            btndth.CssClass = "btn btn-danger mar_top10"
            btnpostpaid.CssClass = "btn btn-danger mar_top10"
            btnelectricity.CssClass = "btn btn-danger mar_top10"
            btnbroadband.CssClass = "btn btn-danger mar_top10"
            btngas.CssClass = "btn btn-danger mar_top10"
            btnlandline.CssClass = "btn btn-danger mar_top10"
            btnwaterbill.CssClass = "btn btn-danger mar_top10"


            btnEMI.CssClass = "btn btn-danger mar_top10"
            btnMunicipality.CssClass = "btn btn-danger mar_top10"
            btnLPG.CssClass = "btn btn-danger mar_top10"
            btnCable.CssClass = "btn btn-danger mar_top10"
            btnInsurance.CssClass = "btn btn-danger mar_top10"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnmobile_Click(sender As Object, e As System.EventArgs) Handles btnmobile.Click
        Try

            add_Recharge_Operators(ddlOperators, "mobile")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btndth_Click(sender As Object, e As System.EventArgs) Handles btndth.Click
        Try
            add_Recharge_Operators(ddlOperators, "dth")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnelectricity_Click(sender As Object, e As System.EventArgs) Handles btnelectricity.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "Electricity")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCable_Click(sender As Object, e As System.EventArgs) Handles btnCable.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "Cable")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLPG_Click(sender As Object, e As System.EventArgs) Handles btnLPG.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "LPG")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnMunicipality_Click(sender As Object, e As System.EventArgs) Handles btnMunicipality.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "Municipality")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnEMI_Click(sender As Object, e As System.EventArgs) Handles btnEMI.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "EMI")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnwaterbill_Click(sender As Object, e As System.EventArgs) Handles btnwaterbill.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "Water")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnlandline_Click(sender As Object, e As System.EventArgs) Handles btnlandline.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "Landline")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnbroadband_Click(sender As Object, e As System.EventArgs) Handles btnbroadband.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "broadband")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnpostpaid_Click(sender As Object, e As System.EventArgs) Handles btnpostpaid.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "Postpaid")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnInsurance_Click(sender As Object, e As System.EventArgs) Handles btnInsurance.Click
        Try
            add_Recharge_Operators(ddl_BBPS_Operators, "Insurance")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub fill_BBPS_PS_Operators(ByVal ddl As DropDownList, ByVal APIResult As String, ByVal ServiceName As String)
        Try
            Div_bbps_section.Visible = True

            txt_BBPS_CA_No.Text = ""
            txt_BBPS_Amt.Text = ""
            txt_BBPS_Amt.CssClass = "form-control"
            btn_BBPS_Fetch.Text = "Get Bill"
            div_BBPS_Amt.Visible = False

            ddl_BBPS_Mode.Items.Clear()
            ddl_BBPS_Mode.Items.Add("online")
            ddl_BBPS_Mode.Items.Add("offline")


            Dim json2 As JObject = JObject.Parse(APIResult)
            Dim data1_ As List(Of JToken) = json2.Children().ToList
            For Each item As JProperty In data1_
                item.CreateReader()
                If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
                    strBuild = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
                    strBuild = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
                    strBuild = item.Value.ToString
                ElseIf item.Name.ToString.Trim.ToUpper = "data".ToString.Trim.ToUpper Then
                    Dim data1 As List(Of JToken) = item.Children().ToList
                    Dim data2_ As List(Of JToken) = data1.Children().ToList
                    For k As Integer = 0 To data2_.Count - 1
                        Dim ss As String = data2_(k).ToString
                        Dim json4 As JObject = JObject.Parse(ss)
                        Dim datap_ As List(Of JToken) = json4.Children().ToList
                        Dim vCategory, vName, vID As String
                        vCategory = ""
                        vName = ""
                        vID = ""

                        For Each item2 As JProperty In datap_

                            If item2.Name.ToString.Trim.ToUpper = "category".ToString.Trim.ToUpper Then
                                If Not item2.Value Is Nothing Then
                                    If Not item2.Value.ToString.Trim = "" Then
                                        vCategory = item2.Value
                                    End If
                                End If
                            ElseIf item2.Name.ToString.Trim.ToUpper = "id".ToString.Trim.ToUpper Then
                                If Not item2.Value Is Nothing Then
                                    If Not item2.Value.ToString.Trim = "" Then
                                        vID = item2.Value
                                    End If
                                End If
                            ElseIf item2.Name.ToString.Trim.ToUpper = "name".ToString.Trim.ToUpper Then
                                If Not item2.Value Is Nothing Then
                                    If Not item2.Value.ToString.Trim = "" Then
                                        vName = item2.Value
                                    End If
                                End If
                            ElseIf item2.Name.ToString.Trim.ToUpper = "displayname".ToString.Trim.ToUpper Then
                                If Not item2.Value Is Nothing Then
                                    If Not item2.Value.ToString.Trim = "" Then
                                        'lbl_BBPS_Title_Heading.Text = item2.Value
                                    End If
                                End If
                            End If
                        Next

                        If Not vName.Trim = "" And Not vCategory.Trim = "" And Not vID.Trim = "" Then
                            If vCategory.Trim.ToUpper = ServiceName.Trim.ToUpper Then
                                Dim xitem As ListItem
                                xitem = New ListItem(vName, vID)
                                ddl.Items.Add(xitem)
                            End If

                        End If

                    Next

                End If
            Next
        Catch ex As Exception

        End Try
        If ddl.Items.Count > 0 Then
            ddl.Items.Insert(0, ":: Select ::")
            ddl.SelectedIndex = 0
        Else
            ddl.Items.Add(":: Select ::")
            ddl.SelectedIndex = 0
        End If


    End Sub

    Public Sub add_Recharge_Operators(ByVal ddl As DropDownList, ByVal ServiceName As String)
        Try
            'Clear()

            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""


            lblFetchOperator.Text = ""
            lblFetchOperator.CssClass = ""

            txt_Mobile_CA_No.Text = ""
            txt_Recharge_Amt.Text = ""

            ddl.Items.Clear()

            ddlCircle.Items.Clear()

            div_Circle.Visible = False
            Divgrid_plan.Visible = False
            Div_bbps_section.Visible = False
            Div_SearchCustomer.Visible = False

            'btnSearchCustomerGo.Visible = False

            Dim OperatorCount As Integer = 0
            reset_button_css()
            lblSelectedService.Text = ServiceName.Trim.ToUpper
            If ServiceName.Trim.ToUpper = "Mobile".ToUpper Then
                div_Circle.Visible = True
                btn_Proceed_Recharge.Visible = True
                Div_SearchCustomer.Visible = True

                ddlCircle.Items.Clear()
                ddlCircle.Items.Add(":: Select Circle ::")
                ddlCircle.Items.Add("Andhra Pradesh Telangana")
                ddlCircle.Items.Add("Assam")
                ddlCircle.Items.Add("Bihar Jharkhand")
                ddlCircle.Items.Add("Chennai")
                ddlCircle.Items.Add("Delhi NCR")
                ddlCircle.Items.Add("Gujarat")
                ddlCircle.Items.Add("Haryana")
                ddlCircle.Items.Add("Himachal Pradesh")
                ddlCircle.Items.Add("Jammu Kashmir")
                ddlCircle.Items.Add("Karnataka")
                ddlCircle.Items.Add("Kerala")
                ddlCircle.Items.Add("Kolkata")
                ddlCircle.Items.Add("Madhya Pradesh Chhattisgarh")
                ddlCircle.Items.Add("Maharashtra Goa")
                ddlCircle.Items.Add("Mumbai")
                ddlCircle.Items.Add("North East")
                ddlCircle.Items.Add("Orissa")
                ddlCircle.Items.Add("Punjab")
                ddlCircle.Items.Add("Rajasthan")
                ddlCircle.Items.Add("Tamil Nadu")
                ddlCircle.Items.Add("UP East")
                ddlCircle.Items.Add("UP West")
                ddlCircle.Items.Add("West Bengal")


                btnmobile.CssClass = "btn btn-success mar_top10"
                'lbl_Service_Heading.Text = " ::  Mobile Recharge ::"
                'lbl_Service_History_Heading.Text = " Mobile Recharge History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter Mobile No"

                Try
                    'div_operator.Visible = True
                    APIResult = GetApiResult_NEW("OPERATOR_LIST_API_URL")
                    APIResult = APIResult
                    Dim dd() As String = APIResult.Split("[")
                    Dim pp() As String = dd(1).Split("]")
                    Dim json() As String = pp(0).Replace("[", "").Replace("]", "").Split("},{")
                    For i As Integer = 0 To json.Length - 1
                        If json(i).Contains("name") Then
                            json(i) = json(i).Replace(",{", "{") & "}"
                            Dim jss As New Script.Serialization.JavaScriptSerializer()
                            Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(json(i))
                            If data("category").Trim.ToUpper = "Prepaid".Trim.ToUpper Then
                                Dim xitem As ListItem
                                xitem = New ListItem(data("name"), data("id"))
                                ddl.Items.Add(xitem)
                            End If

                        End If
                    Next
                Catch ex As Exception

                End Try


                If ddl.Items.Count > 0 Then
                    ddl.Items.Insert(0, ":: Select Operator ::")
                    ddl.SelectedIndex = 0
                Else
                    ddl.Items.Add(":: Select Operator ::")
                    ddl.SelectedIndex = 0
                End If

                ''OperatorList = {"AR:AIRTEL", "BS:BSNL", "ID:IDEA ", "VF:VODAFONE ", "RJ:RELIANCE JIO", "TI:TATA INDICOM", "TD:TATA DOCOMO", "AI:AIRCEL", "TE:TELENOR", "VG:VIRGIN GSM", "VC:VIRGIN CDMA", "MTS:MTS", "DL:TATA DOCOMO CDMA LANDLINE", "BR:BSNL VALIDITY/SPECIAL", "TB:DOCOMO GSM SPECIAL", "UN:UNINOR", "NS:UNINOR SPECIAL", "BSK:BSNL TOPUP (J&amp;K)", "BSJ:BSNL SPECIAL ( J&amp;K )", "JKI:J&amp;K ( IDEA EXPRESS )", "JKJ:JIO-JK"}

            ElseIf ServiceName.Trim.ToUpper = "dth".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True
                Div_SearchCustomer.Visible = True

                btndth.CssClass = "btn btn-success mar_top10"


                'lbl_Service_Heading.Text = " ::  DTH Recharge ::"
                'lbl_Service_History_Heading.Text = " DTH Recharge History ::-"
                lbl_Mobile_CA_No_Heading.Text = "Enter CA No"

                Try
                    APIResult = GetApiResult_NEW("OPERATOR_LIST_API_URL")
                    APIResult = APIResult
                    Dim dd() As String = APIResult.Split("[")
                    Dim pp() As String = dd(1).Split("]")
                    '{""id"":""11"",""name"":""Airtel"",""category"":""Prepaid""},
                    '{""id"":""27"",""name"":""Sun Direct"",""category"":""DTH""},
                    Dim json() As String = pp(0).Replace("[", "").Replace("]", "").Split("},{")
                    For i As Integer = 0 To json.Length - 1
                        If json(i).Contains("name") Then
                            json(i) = json(i).Replace(",{", "{") & "}"
                            Dim jss As New Script.Serialization.JavaScriptSerializer()
                            Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(json(i))
                            If data("category") = "DTH" Then
                                Dim xitem As ListItem
                                xitem = New ListItem(data("name"), data("id"))
                                ddl.Items.Add(xitem)
                            End If

                        End If
                    Next
                Catch ex As Exception

                End Try


                If ddl.Items.Count > 0 Then
                    ddl.Items.Insert(0, ":: Select Operator ::")
                    ddl.SelectedIndex = 0
                Else
                    ddl.Items.Add(":: Select Operator ::")
                    ddl.SelectedIndex = 0
                End If
                ''OperatorList = {"AD:AIRTEL DTH", "BT:BIG TV DTH", "DT:DISH TV DTH", "TS:TATA SKY DTH", "VD:VIDEOCON DTH", "ST:SUN TV DTH"}

            ElseIf ServiceName.Trim.ToUpper = "Landline".ToUpper Then
                div_Circle.Visible = False

                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Operator"
                lbl_BBPS_Title_Heading.Text = "No. + STD Code"

                btnlandline.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try

            ElseIf ServiceName.Trim.ToUpper = "Postpaid".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Operator"
                lbl_BBPS_Title_Heading.Text = "Mobile Number"

                btnpostpaid.CssClass = "btn btn-success mar_top10"
                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try

            ElseIf ServiceName.Trim.ToUpper = "Electricity".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Operator"
                lbl_BBPS_Title_Heading.Text = "CA Number"


                btnelectricity.CssClass = "btn btn-success mar_top10"
                'lbl_Service_Heading.Text = " ::  Electricity Payment ::"
                'lbl_Service_History_Heading.Text = " Electricity Payment History ::-"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try

            ElseIf ServiceName.Trim.ToUpper = "Broadband".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Operator"
                lbl_BBPS_Title_Heading.Text = "No. + STD Code"


                btnbroadband.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try

            ElseIf ServiceName.Trim.ToUpper = "LPG".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Operator"
                lbl_BBPS_Title_Heading.Text = "CA Number"


                btnLPG.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try

            ElseIf ServiceName.Trim.ToUpper = "Waterbill".ToUpper Or ServiceName.Trim.ToUpper = "Water bill".ToUpper Or ServiceName.Trim.ToUpper = "Water".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Water Board"
                lbl_BBPS_Title_Heading.Text = "CA / RR No."


                btnwaterbill.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try
            ElseIf ServiceName.Trim.ToUpper = "LPG".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Operator"
                lbl_BBPS_Title_Heading.Text = "CA Number"


                btnLPG.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try
            ElseIf ServiceName.Trim.ToUpper = "EMI".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Lender"
                lbl_BBPS_Title_Heading.Text = "Loan Account No."


                btnEMI.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try
            ElseIf ServiceName.Trim.ToUpper = "Municipality".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True

                lbl_BBPS_Operator_Heading.Text = "Corporation"
                lbl_BBPS_Title_Heading.Text = "Customer ID"


                btnMunicipality.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try
            ElseIf ServiceName.Trim.ToUpper = "Cable".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True


                lbl_BBPS_Operator_Heading.Text = "Operator"
                lbl_BBPS_Title_Heading.Text = "Mobile / Acc No."

                btnCable.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try
            ElseIf ServiceName.Trim.ToUpper = "Insurance".ToUpper Then
                div_Circle.Visible = False
                btn_Proceed_Recharge.Visible = True


                lbl_BBPS_Operator_Heading.Text = "Insurer"
                lbl_BBPS_Title_Heading.Text = "Policy Number"


                btnInsurance.CssClass = "btn btn-success mar_top10"

                Try
                    APIResult = GetApiResult_NEW("BILL_PAYMENT_OPERATOR_LIST_API_URL")
                    fill_BBPS_PS_Operators(ddl, APIResult, ServiceName)

                Catch ex As Exception

                End Try

            End If


            'txt_Mobile_CA_No.Focus()

            Bind()


        Catch ex As Exception

        End Try
    End Sub

    Public Sub Bind()
        Try
            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim qry As String = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            'API_TransId as TransNo,
            Dim btnFalseStatus As Boolean = False

            If lblSelectedService.Text.Trim.ToUpper = "Mobile".Trim.ToUpper Then
                btnFalseStatus = True
                qry = "Select (CONVERT(VARCHAR(11),Recharge_Date,106)) as  'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "dth".Trim.ToUpper Then
                btnFalseStatus = True
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Landline".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'TelephoneNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Postpaid".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Electricity".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Broadband".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'MobileNo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "GAS".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            ElseIf lblSelectedService.Text.Trim.ToUpper = "Waterbill".Trim.ToUpper Or lblSelectedService.Text.Trim.ToUpper = "Water".Trim.ToUpper Then
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            Else
                qry = "Select  (CONVERT(VARCHAR(11),Recharge_Date,106)) as 'Date',CONVERT(varchar(15),CAST(Recharge_Date AS TIME),100) as Time,Recharge_MobileNo_CaNo as 'CANo',Recharge_Operator as 'Operator',Recharge_Amount as 'Amount', API_status as 'Status',API_resText as 'Remarks' from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Recharge_API where RetailerID='" & RetailerID & "' and Recharge_ServiceType='" & lblSelectedService.Text.Trim & "' order by rid desc "
            End If

            If Not qry.Trim = "" Then
                ds = GV.FL.OpenDsWithSelectQuery(qry)
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()
            End If

            If btnFalseStatus = False Then
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    Dim btnRefund As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkBtnRepeat"), LinkButton)
                    btnRefund.Visible = False
                Next
            End If



        Catch ex As Exception
        End Try
    End Sub

    Protected Sub txt_Mobile_CA_No_TextChanged(sender As Object, e As EventArgs) Handles txt_Mobile_CA_No.TextChanged
        Try
            lblSearchCustomerError.Text = ""
            lblSearchCustomerError.CssClass = ""

            lblFetchOperator.Text = ""
            lblFetchOperator.CssClass = ""


            If txt_Mobile_CA_No.Text.Trim = "" Then
                lblSearchCustomerError.Text = "Enter Mobile No."
                lblSearchCustomerError.CssClass = "errorlabels"
                Exit Sub
            End If

            Dim ServiceName As String = lblSelectedService.Text

            If ServiceName.Trim.ToUpper = "Mobile".ToUpper Then
                APIResult = GetApiResult_NEW("HLR_CHK_API_Parameters")

                Dim json1 As JObject = JObject.Parse(APIResult)
                Dim APIStatus As Boolean = False
                Dim APIStatus_Browse_Plan As Boolean = False
                Dim vOp, VCir, VMsg As String
                vOp = ""
                VCir = ""
                VMsg = ""

                Dim data_ As List(Of JToken) = json1.Children().ToList
                For Each item As JProperty In data_
                    item.CreateReader()
                    If item.Name.ToString.Trim.ToUpper = "response_code".ToString.Trim.ToUpper Then
                        strBuild = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "status".ToString.Trim.ToUpper Then
                        APIStatus = item.Value
                        strBuild = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "message".ToString.Trim.ToUpper Then
                        strBuild = item.Value.ToString
                        VMsg = item.Value.ToString
                    ElseIf item.Name.ToString.Trim.ToUpper = "info".ToString.Trim.ToUpper Then
                        Dim data1 As List(Of JToken) = item.Children().ToList
                        For Each msg As JObject In data1
                            '/// Dynamic Name and get its value
                            For Each p In msg
                                strBuild = strBuild & Environment.NewLine & p.Key.ToString & " = " & p.Value.ToString
                                If p.Key.ToString.Trim.ToUpper = "operator".ToUpper Then
                                    vOp = p.Value.ToString.Trim
                                    strBuild = p.Value.ToString.Trim
                                ElseIf p.Key.ToString.Trim.ToUpper = "circle".ToUpper Then
                                    strBuild = p.Value.ToString.Trim
                                    VCir = p.Value.ToString.Trim
                                End If
                            Next
                        Next
                    End If
                Next

                If APIStatus = False Then
                    Divgrid_plan.Visible = False
                Else
                    lblFetchOperator.Text = vOp & " : " & VCir
                    lblFetchOperator.CssClass = "successlabels"

                    For c As Integer = 0 To ddlOperators.Items.Count - 1
                        If ddlOperators.Items(c).Text.Trim.ToUpper = vOp.Trim.ToUpper Then
                            ddlOperators.SelectedIndex = c
                            Exit For
                        End If
                    Next

                    For c As Integer = 0 To ddlCircle.Items.Count - 1
                        If ddlCircle.Items(c).Text.Trim.ToUpper = VCir.Trim.ToUpper Then
                            ddlCircle.SelectedIndex = c
                            Exit For
                        End If
                    Next



                End If

                ''///////////////

            ElseIf ServiceName.Trim.ToUpper = "dth".ToUpper Then
                APIResult = GetApiResult_NEW("DTH_INFO_API_Parameters")
            ElseIf ServiceName.Trim.ToUpper = "Landline".ToUpper Then

            ElseIf ServiceName.Trim.ToUpper = "Postpaid".ToUpper Then

            ElseIf ServiceName.Trim.ToUpper = "Electricity".ToUpper Then
                APIResult = GetApiResult_NEW("BILL_PAYMENT_FETCH_API_Parameters")
                Dim json1 As JObject = JObject.Parse(APIResult)

                '{"response_code":1,"status":true,"amount":100,"name":"DUMMY NAME","duedate":"2021-06-16","bill_fetch":{"amount":100,"name":"DUMMY NAME","duedate":"2021-06-16","ad2":"HDA24080357","ad3":"VDA27697608"},"ad2":"HDA24080357","ad3":"VDA27697608","message":"Bill Fetched Success."}

            ElseIf ServiceName.Trim.ToUpper = "Broadband".ToUpper Then

            ElseIf ServiceName.Trim.ToUpper = "GAS".ToUpper Then

            ElseIf ServiceName.Trim.ToUpper = "Waterbill".ToUpper Then

            End If


        Catch ex As Exception
            lblSearchPlan.Text = APIResult & ex.Message
        End Try
    End Sub

    Protected Sub btnGrdRowGo_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            lblRowIndex.Text = gvrow.RowIndex
            div_BBPS_Amt.Visible = False

            If lblSelectedService.Text.Trim.ToUpper = "MOBILE" Or lblSelectedService.Text.Trim.ToUpper = "DTH" Then
                txt_Recharge_Amt.Text = GV.parseString(grdResponseDetails.Rows(lblRowIndex.Text).Cells(2).Text)
                txt_Recharge_Amt.Focus()
            Else
                div_BBPS_Amt.Visible = True
                txt_BBPS_Amt.Text = GV.parseString(grdResponseDetails.Rows(lblRowIndex.Text).Cells(3).Text)
                btn_BBPS_Fetch.Text = "Pay Bill"
                btn_BBPS_Fetch.Focus()
            End If



            For i As Integer = 0 To grdResponseDetails.Rows.Count - 1
                grdResponseDetails.Rows(i).BackColor = Color.White
            Next

            grdResponseDetails.Rows(lblRowIndex.Text).BackColor = Color.LightGray
            grdResponseDetails.Rows(lblRowIndex.Text).Cells(0).BackColor = Color.White


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkBtnRepeat_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            Dim ServiceName As String = lblSelectedService.Text

            If ServiceName.Trim.ToUpper = "Mobile".ToUpper Then
                txt_Mobile_CA_No.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
                txt_Recharge_Amt.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
                For c As Integer = 0 To ddlOperators.Items.Count - 1
                    If ddlOperators.Items(c).Text.Trim.ToUpper = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text).Trim.ToUpper Then
                        ddlOperators.SelectedIndex = c
                        Exit For
                    End If
                Next
                btn_Proceed_Recharge.Focus()

            ElseIf ServiceName.Trim.ToUpper = "dth".ToUpper Then
                txt_Mobile_CA_No.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(4).Text)
                txt_Recharge_Amt.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text)
                For c As Integer = 0 To ddlOperators.Items.Count - 1
                    If ddlOperators.Items(c).Text.Trim.ToUpper = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(5).Text).Trim.ToUpper Then
                        ddlOperators.SelectedIndex = c
                        Exit For
                    End If
                Next
                btn_Proceed_Recharge.Focus()

            End If

        Catch ex As Exception

        End Try
    End Sub
    'lnkBtnRepeat_Click

    Protected Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Try
            btnPrintReceipt.OnClientClick = "printdiv('DIV_PrintReceipt');"

            'Dim btn As LinkButton = DirectCast(GridView1.Rows(i).Cells(0).FindControl("LinkButton2"), LinkButton)
            'If Not btn Is Nothing Then
            '    btn.OnClientClick = "window.open('../admin/Print_Installment_Report.aspx?PaymentID=" & GV.parseString(GridView1.Rows(i).Cells(7).Text) & "','_blank');"
            'End If
        Catch ex As Exception

        End Try
    End Sub




End Class