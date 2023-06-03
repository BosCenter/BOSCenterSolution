Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.Web.Script.Serialization

Public Class BOS_AEPS
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  AEPS API  - Start
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API URL  - Start
    Dim GetState_API_URL As String = "https://www.vacsc.com/AEPS/GetState"
    Dim GetDistrict_API_URL As String = "https://www.vacsc.com/AEPS/GetDistrict"
    Dim BCRegistration_API_URL As String = "https://www.vacsc.com/AEPS/BCRegistration"
    Dim GetBCCode_API_URL As String = "https://www.vacsc.com/AEPS/GetBCCode"
    Dim BCStatus_API_URL As String = "https://www.vacsc.com/AEPS/BCStatus"
    Dim BCInitiate_API_URL As String = "https://www.vacsc.com/AEPS/BCInitiate"
    Dim GetStatus_API_URL As String = "https://www.vacsc.com/AEPS/GetStatus"

    'Need to Develop 2 recall Methods

    '///  AEPS API URL  - END

    Public Class GetState_API_Parameters
        Dim VKey As String

        Public Property Key() As String
            Get
                Return VKey
            End Get
            Set(ByVal value As String)
                VKey = value
            End Set
        End Property
    End Class
    Public Class GetDistrict_API_Parameters
        Dim VKey As String
        Dim Vstateid As Integer

        Public Property stateid() As Integer
            Get
                Return Vstateid
            End Get
            Set(ByVal value As Integer)
                Vstateid = value
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
    Public Class GetBCCode_API_Parameters
        Dim Vemailid, Vphone1, VKey As String


        Public Property emailid() As String
            Get
                Return Vemailid
            End Get
            Set(ByVal value As String)
                Vemailid = value
            End Set
        End Property

        Public Property phone1() As String
            Get
                Return Vphone1
            End Get
            Set(ByVal value As String)
                Vphone1 = value
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
    Public Class BCStatus_API_Parameters
        Dim Vbc_id, VKey As String

        Public Property bc_id() As String
            Get
                Return Vbc_id
            End Get
            Set(ByVal value As String)
                Vbc_id = value
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
    Public Class GetStatus_API_Parameters
        Dim Vstanno, VKey As String

        Public Property stanno() As String
            Get
                Return Vstanno
            End Get
            Set(ByVal value As String)
                Vstanno = value
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
    Public Class BCInitiate_API_Parameters
        Dim Vbc_id, Vphone1, Vip, Vuserid, VKey As String

        Public Property bc_id() As String
            Get
                Return Vbc_id
            End Get
            Set(ByVal value As String)
                Vbc_id = value
            End Set
        End Property

        Public Property phone1() As String
            Get
                Return Vphone1
            End Get
            Set(ByVal value As String)
                Vphone1 = value
            End Set
        End Property
        Public Property ip() As String
            Get
                Return Vip
            End Get
            Set(ByVal value As String)
                Vip = value
            End Set
        End Property

        Public Property userid() As String
            Get
                Return Vuserid
            End Get
            Set(ByVal value As String)
                Vuserid = value
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
    Public Class BCRegistration_API_Parameters
        Dim Vbc_f_name, Vbc_m_name, Vbc_l_name, Vemailid, Vphone1, Vphone2, Vbc_dob, Vbc_address, Vbc_block, Vbc_city, Vbc_landmark, Vbc_loc, Vbc_mohhalla, Vbc_pan, Vbc_pincode, Vshopname, Vkyc1, Vkyc2, Vkyc3, Vkyc4, VshopType, Vqualification, Vpopulation, VlocationType, VKey As String
        Dim Vbc_state, Vbc_district As Integer

        Public Property bc_f_name() As String
            Get
                Return Vbc_f_name
            End Get
            Set(ByVal value As String)
                Vbc_f_name = value
            End Set
        End Property
        Public Property bc_m_name() As String
            Get
                Return Vbc_m_name
            End Get
            Set(ByVal value As String)
                Vbc_m_name = value
            End Set
        End Property
        Public Property bc_l_name() As String
            Get
                Return Vbc_l_name
            End Get
            Set(ByVal value As String)
                Vbc_l_name = value
            End Set
        End Property
        Public Property emailid() As String
            Get
                Return Vemailid
            End Get
            Set(ByVal value As String)
                Vemailid = value
            End Set
        End Property
        Public Property phone1() As String
            Get
                Return Vphone1
            End Get
            Set(ByVal value As String)
                Vphone1 = value
            End Set
        End Property
        Public Property phone2() As String
            Get
                Return Vphone2
            End Get
            Set(ByVal value As String)
                Vphone2 = value
            End Set
        End Property
        Public Property bc_dob() As String
            Get
                Return Vbc_dob
            End Get
            Set(ByVal value As String)
                Vbc_dob = value
            End Set
        End Property
        Public Property bc_state() As Integer  'bc_district
            Get
                Return Vbc_state
            End Get
            Set(ByVal value As Integer)
                Vbc_state = value
            End Set
        End Property
        Public Property bc_district() As Integer  'bc_district
            Get
                Return Vbc_district
            End Get
            Set(ByVal value As Integer)
                Vbc_district = value
            End Set
        End Property
        Public Property bc_address() As String
            Get
                Return Vbc_address
            End Get
            Set(ByVal value As String)
                Vbc_address = value
            End Set
        End Property
        Public Property bc_block() As String
            Get
                Return Vbc_block
            End Get
            Set(ByVal value As String)
                Vbc_block = value
            End Set
        End Property
        Public Property bc_city() As String
            Get
                Return Vbc_city
            End Get
            Set(ByVal value As String)
                Vbc_city = value
            End Set
        End Property
        Public Property bc_landmark() As String
            Get
                Return Vbc_landmark
            End Get
            Set(ByVal value As String)
                Vbc_landmark = value
            End Set
        End Property
        Public Property bc_loc() As String
            Get
                Return Vbc_loc
            End Get
            Set(ByVal value As String)
                Vbc_loc = value
            End Set
        End Property
        Public Property bc_mohhalla() As String
            Get
                Return Vbc_mohhalla
            End Get
            Set(ByVal value As String)
                Vbc_mohhalla = value
            End Set
        End Property
        Public Property bc_pan() As String
            Get
                Return Vbc_pan
            End Get
            Set(ByVal value As String)
                Vbc_pan = value
            End Set
        End Property
        Public Property bc_pincode() As String
            Get
                Return Vbc_pincode
            End Get
            Set(ByVal value As String)
                Vbc_pincode = value
            End Set
        End Property
        Public Property shopname() As String
            Get
                Return Vshopname
            End Get
            Set(ByVal value As String)
                Vshopname = value
            End Set
        End Property
        Public Property kyc1() As String
            Get
                Return Vkyc1
            End Get
            Set(ByVal value As String)
                Vkyc1 = value
            End Set
        End Property
        Public Property kyc2() As String
            Get
                Return Vkyc2
            End Get
            Set(ByVal value As String)
                Vkyc2 = value
            End Set
        End Property
        Public Property kyc3() As String
            Get
                Return Vkyc3
            End Get
            Set(ByVal value As String)
                Vkyc3 = value
            End Set
        End Property
        Public Property kyc4() As String
            Get
                Return Vkyc4
            End Get
            Set(ByVal value As String)
                Vkyc4 = value
            End Set
        End Property
        Public Property shopType() As String
            Get
                Return VshopType
            End Get
            Set(ByVal value As String)
                VshopType = value
            End Set
        End Property
        Public Property qualification() As String
            Get
                Return Vqualification
            End Get
            Set(ByVal value As String)
                Vqualification = value
            End Set
        End Property
        Public Property population() As String
            Get
                Return Vpopulation
            End Get
            Set(ByVal value As String)
                Vpopulation = value
            End Set
        End Property
        Public Property locationType() As String
            Get
                Return VlocationType
            End Get
            Set(ByVal value As String)
                VlocationType = value
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

            If APIMethod = "GetState_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New GetState_API_Parameters()
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = GetState_API_URL
            ElseIf APIMethod = "GetDistrict_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New GetDistrict_API_Parameters()
                setParameter_API_Obj.stateid = GV.parseString(lblStateID.Text.Trim)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = GetDistrict_API_URL
            ElseIf APIMethod = "GetBCCode_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New GetBCCode_API_Parameters()
                setParameter_API_Obj.emailid = GV.parseString(lblEmailID.Text)
                setParameter_API_Obj.phone1 = GV.parseString(lblMobileNo.Text)
                setParameter_API_Obj.Key = APIKey
                'setParameter_API_Obj.Recipient_Id = GV.parseString("")
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = GetBCCode_API_URL
            ElseIf APIMethod = "BCStatus_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New BCStatus_API_Parameters()
                setParameter_API_Obj.bc_id = GV.parseString("BC870291847")
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = BCStatus_API_URL
            ElseIf APIMethod = "GetStatus_API_Parameters" Then 'Error
                Dim setParameter_API_Obj As New GetStatus_API_Parameters()
                setParameter_API_Obj.stanno = GV.parseString("")
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = GetStatus_API_URL
            ElseIf APIMethod = "BCInitiate_API_Parameters" Then
                Dim setParameter_API_Obj As New BCInitiate_API_Parameters()

                setParameter_API_Obj.bc_id = GV.parseString(lblBcCode.Text)
                setParameter_API_Obj.ip = GV.parseString(lblIP.Text)
                setParameter_API_Obj.phone1 = GV.parseString(lblMobileNo.Text)
                setParameter_API_Obj.userid = GV.parseString(lblUserid.Text)
                setParameter_API_Obj.Key = APIKey
                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = BCInitiate_API_URL
            ElseIf APIMethod = "BCRegistration_API_Parameters" Then 'Done
                Dim setParameter_API_Obj As New BCRegistration_API_Parameters


                setParameter_API_Obj.bc_city = GV.parseString(lblCity.Text)
                setParameter_API_Obj.bc_dob = GV.parseString(lblDOB.Text)
                setParameter_API_Obj.bc_f_name = GV.parseString(lblFirstName.Text)
                setParameter_API_Obj.bc_l_name = GV.parseString(lblLastName.Text)
                setParameter_API_Obj.bc_pan = GV.parseString(lblPanCardNumber.Text)
                setParameter_API_Obj.bc_pincode = GV.parseString(lblPincode.Text)
                setParameter_API_Obj.emailid = GV.parseString(lblEmailID.Text)
                setParameter_API_Obj.phone1 = GV.parseString(lblMobileNo.Text)
                setParameter_API_Obj.phone2 = GV.parseString(lblAlternateMobileNo.Text)
                setParameter_API_Obj.bc_state = GV.parseString(lblStateID.Text)
                setParameter_API_Obj.bc_district = GV.parseString(lblDistrictID.Text)
                setParameter_API_Obj.shopname = GV.parseString(lblAgencyName.Text)
                setParameter_API_Obj.bc_address = GV.parseString(lblOfficeAddress.Text)


                setParameter_API_Obj.bc_loc = GV.parseString(lblCity.Text)
                setParameter_API_Obj.bc_m_name = GV.parseString("")
                setParameter_API_Obj.bc_mohhalla = GV.parseString("N/A")
                setParameter_API_Obj.bc_block = GV.parseString("N/A")
                setParameter_API_Obj.bc_landmark = GV.parseString("N/A")

                setParameter_API_Obj.kyc1 = GV.parseString("")
                setParameter_API_Obj.kyc2 = GV.parseString("")
                setParameter_API_Obj.kyc3 = GV.parseString("")
                setParameter_API_Obj.kyc4 = GV.parseString("")

                setParameter_API_Obj.locationType = GV.parseString("Rural")
                setParameter_API_Obj.population = GV.parseString("50000 to 100000")
                setParameter_API_Obj.qualification = GV.parseString("Graduate")
                setParameter_API_Obj.shopType = GV.parseString("Mobile Shop")
                setParameter_API_Obj.Key = APIKey

                StrParameters = Newtonsoft.Json.JsonConvert.SerializeObject(setParameter_API_Obj)
                API_URLS = BCRegistration_API_URL
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
            Dim TrasID As String = GV.FL.getAutoNumber("TransId")
            LogString = Environment.NewLine & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            LogString = LogString & "Trans ID : " & GV.parseString(TrasID) & Environment.NewLine
            LogString = LogString & "Trans DateTime : " & Now & Environment.NewLine
            LogString = LogString & "Request String  : " & Parameter & Environment.NewLine & Environment.NewLine

            API_Name = "AEPS API"
            Trans_ID = GV.parseString(TrasID)
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
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)
            LogString = LogString & "Response String  : " & str & Environment.NewLine
            LogString = LogString & Environment.NewLine & "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & Environment.NewLine
            GV.SaveTextToFile(LogString, Server.MapPath("AEPS_API_LOG.txt"), True)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            strQry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", HttpContext.Current.Request, HttpContext.Current.Response).Trim & ".dbo.BOS_API_Log_Records(API_Name,Trans_ID,Trans_DateTime,Request_String,Response_String,AgentID,AgentType) values('" & API_Name & "' ,'" & Trans_ID & "' ,'" & Trans_DateTime & "' ,'" & Request_String & "' ,'" & Response_String & "' ,'" & AgentID & "' ,'" & AgentType & "')"
            GV.FL.DMLQueriesBulk(strQry)

            GV.SaveTextToFile(LogString, Server.MapPath("AEPS_API_LOG.txt"), True)
            Return str
        End Try
        Return str
        '" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.
    End Function
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    '///  Money Transfer API  - End
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    Dim APIResult As String = ""


    Private Sub lnkbtn_StartAEPS_Click(sender As Object, e As System.EventArgs) Handles lnkbtn_StartAEPS.Click
        Try
            lblError.Text = ""
            lblError.CssClass = ""

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


            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            ds = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & LoginID & "'")
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then



                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FirstName")) Then
                        If Not ds.Tables(0).Rows(0).Item("FirstName").ToString() = "" Then
                            lblFirstName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("FirstName").ToString())
                        Else
                            lblFirstName.Text = ""
                        End If
                    Else
                        lblFirstName.Text = ""
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("LastName")) Then
                        If Not ds.Tables(0).Rows(0).Item("LastName").ToString() = "" Then
                            lblLastName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("LastName").ToString())
                        Else
                            lblLastName.Text = ""
                        End If
                    Else
                        lblLastName.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("EmailID")) Then
                        If Not ds.Tables(0).Rows(0).Item("EmailID").ToString() = "" Then
                            lblEmailID.Text = GV.parseString(ds.Tables(0).Rows(0).Item("EmailID").ToString())
                        Else
                            lblEmailID.Text = ""
                        End If
                    Else
                        lblEmailID.Text = ""
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("MobileNo")) Then
                        If Not ds.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                            lblMobileNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("MobileNo").ToString())
                        Else
                            lblMobileNo.Text = ""
                        End If
                    Else
                        lblMobileNo.Text = ""
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DOB")) Then
                        If Not ds.Tables(0).Rows(0).Item("DOB").ToString() = "" Then
                            lblDOB.Text = CDate(ds.Tables(0).Rows(0).Item("DOB").ToString()).ToString("dd/MM/yyyyy")
                        Else
                            lblDOB.Text = Now.Date.ToString("dd/MM/yyyy")
                        End If
                    Else
                        lblDOB.Text = Now.Date.ToString("dd/MM/yyyy")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("AlternateMobileNo")) Then
                        If Not ds.Tables(0).Rows(0).Item("AlternateMobileNo").ToString() = "" Then
                            lblAlternateMobileNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AlternateMobileNo").ToString())
                        Else
                            lblAlternateMobileNo.Text = ""
                        End If
                    Else
                        lblAlternateMobileNo.Text = ""
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OfficeAddress")) Then
                        If Not ds.Tables(0).Rows(0).Item("OfficeAddress").ToString() = "" Then
                            lblOfficeAddress.Text = GV.parseString(ds.Tables(0).Rows(0).Item("OfficeAddress").ToString())
                        Else
                            lblOfficeAddress.Text = ""
                        End If
                    Else
                        lblOfficeAddress.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("City")) Then
                        If Not ds.Tables(0).Rows(0).Item("City").ToString() = "" Then
                            lblCity.Text = GV.parseString(ds.Tables(0).Rows(0).Item("City").ToString())
                        Else
                            lblCity.Text = ""
                        End If
                    Else
                        lblCity.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pincode")) Then
                        If Not ds.Tables(0).Rows(0).Item("Pincode").ToString() = "" Then
                            lblPincode.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Pincode").ToString())
                        Else
                            lblPincode.Text = ""
                        End If
                    Else
                        lblPincode.Text = ""
                    End If



                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("AgencyName")) Then
                        If Not ds.Tables(0).Rows(0).Item("AgencyName").ToString() = "" Then
                            lblAgencyName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AgencyName").ToString())
                        Else
                            lblAgencyName.Text = ""
                        End If
                    Else
                        lblAgencyName.Text = ""
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("PanCardNumber")) Then
                        If Not ds.Tables(0).Rows(0).Item("PanCardNumber").ToString() = "" Then
                            lblPanCardNumber.Text = GV.parseString(ds.Tables(0).Rows(0).Item("PanCardNumber").ToString())
                        Else
                            lblPanCardNumber.Text = ""
                        End If
                    Else
                        lblPanCardNumber.Text = ""
                    End If



                    '////////////////////////////////////////////




                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("State")) Then
                        If Not ds.Tables(0).Rows(0).Item("State").ToString() = "" Then
                            lblStateName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("State").ToString())
                        Else
                            lblStateName.Text = ""
                        End If
                    Else
                        lblStateName.Text = ""
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("District")) Then
                        If Not ds.Tables(0).Rows(0).Item("District").ToString() = "" Then
                            lblDistrictName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("District").ToString())
                        Else
                            lblDistrictName.Text = ""
                        End If
                    Else
                        lblDistrictName.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("StateID")) Then
                        If Not ds.Tables(0).Rows(0).Item("StateID").ToString() = "" Then
                            lblStateID.Text = GV.parseString(ds.Tables(0).Rows(0).Item("StateID").ToString())
                        Else
                            lblStateID.Text = ""
                        End If
                    Else
                        lblStateID.Text = ""
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DistrictID")) Then
                        If Not ds.Tables(0).Rows(0).Item("DistrictID").ToString() = "" Then
                            lblDistrictID.Text = GV.parseString(ds.Tables(0).Rows(0).Item("DistrictID").ToString())
                        Else
                            lblDistrictID.Text = ""
                        End If
                    Else
                        lblDistrictID.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("BcCode")) Then
                        If Not ds.Tables(0).Rows(0).Item("BcCode").ToString() = "" Then
                            lblBcCode.Text = GV.parseString(ds.Tables(0).Rows(0).Item("BcCode").ToString())
                        Else
                            lblBcCode.Text = ""
                        End If
                    Else
                        lblBcCode.Text = ""
                    End If


                    lblIP.Text = GV.GetIPAddress()

                    lblUserid.Text = LoginID



                    Dim stateID, districtid As String
                    stateID = ""
                    districtid = ""

                    If lblStateID.Text.Trim = "" Then
                        APIResult = GetApiResult("GetState_API_Parameters")
                        '/// parse and read data in list format through json parse
                        Dim json() As String = APIResult.Replace("[", "").Replace("]", "").Split("},{")
                        For i As Integer = 0 To json.Length - 1
                            json(i) = json(i).Replace(",{", "{") & "}"
                            Dim jss As New JavaScriptSerializer()
                            Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(json(i))
                            If data("statename").ToString.Trim.ToUpper = GV.parseString(lblStateName.Text).ToUpper Then
                                stateID = data("stateid")
                                lblStateID.Text = stateID
                                GV.FL.DMLQueriesBulk("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set StateID='" & stateID & "' where RegistrationId='" & LoginID & "'")
                                Exit For
                            End If
                        Next

                    End If

                    If Not lblStateID.Text.Trim = "" And lblDistrictID.Text.Trim = "" Then
                        APIResult = GetApiResult("GetDistrict_API_Parameters")
                        '/// parse and read data in list format through json parse
                        Dim json() As String = APIResult.Replace("[", "").Replace("]", "").Split("},{")
                        For i As Integer = 0 To json.Length - 1
                            json(i) = json(i).Replace(",{", "{") & "}"
                            Dim jss As New JavaScriptSerializer()
                            Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(json(i))
                            If data("districtname").ToString.Trim.ToUpper = GV.parseString(lblDistrictName.Text).ToUpper Then
                                districtid = data("districtid")
                                lblDistrictID.Text = districtid
                                GV.FL.DMLQueriesBulk("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set DistrictID='" & districtid & "' where RegistrationId='" & LoginID & "'")
                                Exit For
                            End If
                        Next
                    End If



                    If Not lblDistrictID.Text.Trim = "" And Not lblStateID.Text.Trim = "" And lblBcCode.Text.Trim = "" Then
                        APIResult = GetApiResult("BCRegistration_API_Parameters")
                        APIResult = APIResult

                        '[{"Message":"There is no row at position 0."}]
                        '[{"Message":"{*} Email Id should be unique."}]

                        '/// parse and read data in list format through json parse
                        Dim json1 As String = APIResult.Replace("[", "").Replace("]", "")
                        json1 = json1 '.Replace(",{", "{") & "}"
                        Dim jss As New JavaScriptSerializer()
                        Dim data As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(json1)

                        Dim VMessage, Vphone1, Vbc_id, Vemailid, VStatuscode As String
                        If data.Count = 1 Then
                            VMessage = data("Message")
                        Else
                            VMessage = data("Message")
                            Vphone1 = data("phone1")
                            Vbc_id = data("bc_id")
                            Vemailid = data("emailid")
                            VStatuscode = data("Statuscode")
                            If Not Vbc_id.Trim = "" Then
                                lblBcCode.Text = Vbc_id.Trim
                                GV.FL.DMLQueriesBulk("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set BcCode='" & Vbc_id & "' where RegistrationId='" & LoginID & "'")
                            End If

                        End If

                        If VMessage.Trim = "There is no row at position 0." Or VMessage.Trim = "{*} Email Id should be unique." Then
                            '//Get BC Code
                            APIResult = GetApiResult("GetBCCode_API_Parameters")
                            '"[{"Message":"Success","StatusCode":"001","BcCode":"BC870291847"}]"

                            APIResult = APIResult
                            json1 = APIResult.Replace("[", "").Replace("]", "")
                            json1 = json1
                            Dim jss1 As New JavaScriptSerializer()
                            Dim data1 As Dictionary(Of String, String) = jss1.Deserialize(Of Dictionary(Of String, String))(json1)
                            If data1.Count > 0 Then
                                Vbc_id = data1("BcCode")
                                If Not Vbc_id.Trim = "" Then
                                    lblBcCode.Text = Vbc_id.Trim
                                    GV.FL.DMLQueriesBulk("update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set BcCode='" & Vbc_id & "' where RegistrationId='" & LoginID & "'")
                                End If
                                VMessage = data1("Message")
                                VStatuscode = data1("StatusCode")
                            End If
                        End If



                        If Not lblDistrictID.Text.Trim = "" And Not lblStateID.Text.Trim = "" And Not lblBcCode.Text.Trim = "" Then
                            'Initialize BC
                            APIResult = GetApiResult("BCInitiate_API_Parameters")
                            APIResult = APIResult
                            json1 = APIResult.Replace("[", "").Replace("]", "")
                            Dim jss1 As New JavaScriptSerializer()
                            Dim data1 As Dictionary(Of String, String) = jss1.Deserialize(Of Dictionary(Of String, String))(json1)
                            If data1.Count > 0 Then
                                Dim EncryptedText As String = data1("Result")
                                Dim str_Url As String = "https://icici.bankmitra.org/Location.aspx?text=" & EncryptedText & ""
                                Response.Redirect(str_Url)
                                'VMessage = data1("Message")
                                'VStatuscode = data1("StatusCode")
                            End If
                        End If




                    ElseIf Not lblDistrictID.Text.Trim = "" And Not lblStateID.Text.Trim = "" And Not lblBcCode.Text.Trim = "" Then
                        'Initialize BC
                        APIResult = GetApiResult("BCInitiate_API_Parameters")
                        APIResult = APIResult
                        Dim json1 As String = APIResult.Replace("[", "").Replace("]", "")
                        Dim jss1 As New JavaScriptSerializer()
                        Dim data1 As Dictionary(Of String, String) = jss1.Deserialize(Of Dictionary(Of String, String))(json1)
                        If data1.Count > 0 Then
                            Dim EncryptedText As String = data1("Result")
                            Dim str_Url As String = "https://icici.bankmitra.org/Location.aspx?text=" & EncryptedText & ""
                            Response.Redirect(str_Url)
                            'VMessage = data1("Message")
                            'VStatuscode = data1("StatusCode")
                        End If

                    End If




                    ''check bc status
                    'APIResult = GetApiResult("BCStatus_API_Parameters")
                    'APIResult = APIResult
                    ''"[{"bc_id":"BC870291847","status":"Rejected","remarks":"KYC DOCUMENT IS NOT UPLOADED"}]"


                End If
            End If






        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Text = ""
                lblError.CssClass = ""

                lblAgentID.Text = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                lblAgentType.Text = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class