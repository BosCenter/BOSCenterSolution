Imports System.Net
Imports System.Web.Http
Imports System.Data.SqlClient
Imports System.Data
Imports Newtonsoft.Json




Public Class LoginAuthController
    Inherits ApiController


    'Function get_people_ContentResult(Optional id As Integer = 0) As String
    '    Dim d As New Dictionary(Of String, String) From {
    '         {"James", 9001},
    '         {"Jo", 3474},
    '         {"Jess", 11926}
    '     }
    '    Return JsonConvert.SerializeObject(d, Formatting.Indented)
    'End Function

    'Function get_people_string(Optional id As Integer = 0) As String
    '    'Dim c As dtb_people = db.dtb_people.Where(Function(x) x.c_Id = id).Single
    '    'Dim m As Object = New With {.firstname = c.c_firstname, .lastname = c.c_lastname, .id = c.c_Id}
    '    'Dim serializer As New Script.Serialization.JavaScriptSerializer()
    '    'Return serializer.Serialize(m)
    'End Function

    'Function get_people_string(Optional id As Integer = 0) As String
    'Dim jsonstr2 As String = $"{{
    '    'firstname': '{c.c_firstname}',
    '    'lastname':'{c.c_lastname}' ,
    '    'id':{c.c_Id.ToString}
    '}}"
    '    Response.Clear()
    '    Response.ContentType = "application/json; charset=utf-8"
    '    Response.Write(jsonstr)
    '    Response.End()
    'End Function


    ' GET api/<controller>
    Public Function GetValues() As IEnumerable(Of String)
        Return New String() {"value1", "value2"}
    End Function

    Public Function GetIPAddress() As String
        Try
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(sIPAddress) Then
                Return context.Request.ServerVariables("REMOTE_ADDR")
            Else
                Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
                Return ipArray(0)
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'https://www.boscenter.in/api/LoginAuth?Companycode=cmp1045&LoginID=RTE-554&UserPassword=banee@123&BillAmount=XXXX&Order_ID=XXXX
    'Save Order For BOS
    Public Function GetValue(ByVal Companycode As String, ByVal LoginID As String, ByVal UserPassword As String, ByVal BillAmount As String, ByVal Order_ID As String) As String
        Dim returnPair As New Dictionary(Of String, String) From {
             {"Status", ""},
             {"CompanyCode", ""},
             {"LoginID", ""},
             {"AgentID", ""},
             {"Order_ID", ""},
             {"Pay_Trans_ID", ""}
         }
        returnPair("LoginID") = LoginID
        Dim result As String = ""
        If Companycode.Trim = "" Then
            result = "Please Enter CompanyCode."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        If LoginID.Trim = "" Then
            result = "Please Enter LoginId."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        If UserPassword.Trim = "" Then
            result = "Please Enter Password."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        If BillAmount.Trim = "" Then
            result = "Please Enter BillAmount."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        If Not IsNumeric(BillAmount.Trim) Then
            result = "Bill Amount Must be Numeric."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        If Order_ID.Trim = "" Then
            result = "Please Enter Order_ID."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        ' IsNumeric(BillAmount.Trim) Then


        Dim DBName As String = AddInVar("DatabaseName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & Companycode.Trim & "' and [Status]='Active' ")
        If DBName.Trim = "" Then
            result = "CompanyCode is Incorrect"
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If
        returnPair("CompanyCode") = Companycode.Trim

        Dim localDS As New DataSet
        If Len(LoginID.Trim) = 10 And IsNumeric(LoginID.Trim) Then
            'Case If user Is Customer
            localDS = OpenDsWithSelectQuery("select * from " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   MobileNo='" & LoginID & "' and AgentType='Customer' and AgentPassword='" & UserPassword & "' and ActiveStatus='Active' ")
        ElseIf LoginID.Trim.Contains("-") Then
            localDS = OpenDsWithSelectQuery("select * from " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & LoginID & "' and AgentPassword='" & UserPassword & "'  and AgentType in ('Customer','Retailer')  and ActiveStatus='Active' ")
        Else
            result = "Invalid LoginID"
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If
        If localDS.Tables.Count > 0 Then
            If localDS.Tables(0).Rows.Count > 0 Then
                If RecCount(" " & DBName.Trim & ".dbo.BOS_TransferAmountToAgents where API_TransId='" & Order_ID & "' ") > 0 Then
                    result = "Order ID Already Exists"
                    returnPair("Status") = result
                    Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
                    Exit Function
                ElseIf Not returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName) >= CDec(BillAmount) Then
                    result = "Insufficient Wallet Balance"
                    returnPair("Status") = result
                    Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
                    Exit Function
                Else
                    '//// Success
                  
                    Dim Pay_Trans_ID, VTransferFrom, VTransferTo As String
                    Pay_Trans_ID = AddInVar("Pay_Trans_Prefix", "[BosCenter_DB].[dbo].[AutoNumber]") & get_AutoNumber_Admin("Pay_Trans_ID")
                    VTransferFrom = LoginID
                    VTransferTo = "Admin"
                    Dim VTransferFromMsg As String = "Your Wallet is Debited by BOS Shopping Order No (" & Order_ID & ") And CompanyCode is (" & Companycode & ") - API"
                    Dim VTransferToMsg As String = "Your Wallet is Credited by BOS Shopping Order No (" & Order_ID & ")  And CompanyCode is (" & Companycode & ") - API"
                    Dim VVRemark As String = " API - BOS CustomerID (" & VTransferFrom & ") and Order No (" & Order_ID & ") and CompanyCode is (" & Companycode & ")"
                    Dim TransactionDate As String = Now.Date
                    Dim bosqry As String = "insert into " & DBName & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GetIPAddress.Trim & "','" & Order_ID & "','" & Pay_Trans_ID.Trim & "','" & VTransferToMsg & "','" & VTransferFromMsg & "','BOS Shopping','" & VVRemark & "','" & TransactionDate & "','" & VTransferFrom & "','" & VTransferTo & "','" & BillAmount & "',getdate(),'" & VTransferFrom & "',getdate() ) ;"

                    If executeDMLQuery(bosqry) > 0 Then
                        result = "Success"
                        returnPair("Status") = result
                        returnPair("AgentID") = localDS.Tables(0).Rows(0).Item("RegistrationId")
                        returnPair("Order_ID") = Order_ID
                        returnPair("Pay_Trans_ID") = Pay_Trans_ID
                    Else
                        result = "Failed"
                        returnPair("Status") = result
                        Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
                        Exit Function
                    End If
                End If

            Else
                result = "Invalid LoginID Or Password"
                returnPair("Status") = result
                Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
                Exit Function
            End If
        Else
            result = "Invalid LoginID Or Password"
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
    End Function



    'Login Auth Function
    'https://www.boscenter.in/api/LoginAuth?LoginID=RTE-554&UserPassword=banee@123&Companycode=cmp1045

    Public Function GetValue(ByVal Companycode As String, ByVal LoginID As String, ByVal UserPassword As String) As String
        Dim returnPair As New Dictionary(Of String, String) From {
             {"Status", ""},
             {"CompanyName", ""},
             {"CompanyCode", ""},
             {"LoginID", ""},
             {"AgentID", ""},
             {"AgentName", ""},
             {"AgentType", ""},
             {"WalletBalance", ""},
             {"ServiceType", ""},
             {"ServiceCharges", ""}
         }
        returnPair("LoginID") = LoginID
        Dim result As String = ""
        If Companycode.Trim = "" Then
            result = "Please Enter CompanyCode."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        If LoginID.Trim = "" Then
            result = "Please Enter LoginId."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        If UserPassword.Trim = "" Then
            result = "Please Enter Password."
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        Dim DBName As String = AddInVar("DatabaseName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & Companycode.Trim & "' and [Status]='Active' ")

        If DBName.Trim = "" Then
            result = "CompanyCode is Incorrect"
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If
        returnPair("CompanyCode") = Companycode.Trim
        returnPair("CompanyName") = AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & Companycode.Trim & "' and [Status]='Active' ")

        Dim localDS As New DataSet

        If Len(LoginID.Trim) = 10 And IsNumeric(LoginID.Trim) Then
            'Case If user Is Customer
            localDS = OpenDsWithSelectQuery("select * from " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   MobileNo='" & LoginID & "' and AgentType='Customer' and AgentPassword='" & UserPassword & "' and ActiveStatus='Active' ")
        ElseIf LoginID.Trim.Contains("-") Then
            localDS = OpenDsWithSelectQuery("select * from " & DBName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where   RegistrationId='" & LoginID & "' and AgentPassword='" & UserPassword & "'  and AgentType in ('Customer','Retailer')  and ActiveStatus='Active' ")
        Else
            result = "Invalid LoginID"
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If
        If localDS.Tables.Count > 0 Then
            If localDS.Tables(0).Rows.Count > 0 Then
                '//// Success
                result = "Success"
                returnPair("Status") = result
                returnPair("AgentID") = localDS.Tables(0).Rows(0).Item("RegistrationId")
                returnPair("AgentName") = localDS.Tables(0).Rows(0).Item("FirstName") & " " & localDS.Tables(0).Rows(0).Item("LastName")
                returnPair("AgentType") = localDS.Tables(0).Rows(0).Item("AgentType")
                returnPair("WalletBalance") = returnWalletBalCalculation(localDS.Tables(0).Rows(0).Item("RegistrationId"), DBName)
                returnPair("ServiceType") = AddInVar("ServiceType", "" & DBName.Trim & ".[dbo].[BOS_ProductServiceMaster] where Title='Flight' ")
                returnPair("ServiceCharges") = AddInVar("ServiceCharge", "" & DBName.Trim & ".[dbo].[BOS_ProductServiceMaster] where Title='Flight' ")
            Else
                result = "Invalid LoginID Or Password"
                returnPair("Status") = result
                Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
                Exit Function
            End If
        Else
            result = "Invalid LoginID Or Password"
            returnPair("Status") = result
            Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
            Exit Function
        End If

        Return JsonConvert.SerializeObject(returnPair, Formatting.Indented)
    End Function





    'https://www.boscenter.in/api/Recharge?OrderId=123&status=Failed&Msg=hello
    'http://www.boscenter.in/api/recharge/?OrderId=xxxx&status=xxx&msg=xxx
    'https://www.boscenter.in/api/RechargeKitCallback/?OrderId=xxxx&status=xxx&Optransid=xxx&Prid=xxx
    Dim GV As New GlobalVariable("ADMIN")

    'Public Function GetValue(ByVal Orderid As String, ByVal Status As String, ByVal Optransid As String, ByVal Prid As String) As String
    '    Dim v_OrderId, v_status, v_msg, msg As String
    '    v_OrderId = parseString(Orderid)
    '    v_status = parseString(Status)
    '    v_msg = ": Optransid=" & parseString(Optransid) & " :Prid= " & Prid & " :Orderid= " & Orderid & " :v_status= " & v_status
    '    Dim result As Integer = 0
    '    Try
    '        Dim cn As SqlConnection
    '        Dim cmd As SqlCommand
    '        Dim qry As String = ""
    '        Dim orderIDFound As Boolean = True
    '        con = New SqlConnection("Server=103.90.242.173;DataBase=BosCenter_DB;user id=Bos_New_User;password=jWr6$n90".ToString())

    '        Dim ds11 As DataSet = OpenDsWithSelectQuery("select * from Recharge_API_DB_Info where API_TransId='" & v_OrderId & "'")
    '        Dim DBName As String = ""
    '        Dim CompanyCode As String = ""

    '        If Not ds11 Is Nothing Then
    '            If ds11.Tables.Count > 0 Then
    '                If ds11.Tables(0).Rows.Count > 0 Then
    '                    DBName = ds11.Tables(0).Rows(0).Item("DBName")
    '                    CompanyCode = ds11.Tables(0).Rows(0).Item("CompanyCode")
    '                    'API_status='Processing'

    '                    Dim API_status As String = ""
    '                    Dim RefundedStatus As Boolean = False

    '                    If Not IsDBNull(ds11.Tables(0).Rows(0).Item("API_status")) Then
    '                        If ds11.Tables(0).Rows(0).Item("API_status").ToString.Trim.ToUpper = "Processing".ToUpper Then

    '                            If v_status.Trim.ToUpper = "3".ToUpper Then
    '                                'QryStr = QryStr & " " & " insert into BosCenter_DB.dbo.Recharge_API_DB_Info (RecordDatetime,API_TransId,Recharge_TransId,API_status,API_resText,CompanyCode,DBName) values(getdate(),'" & VTransId & "','" & GV.parseString(lblTransId.Text.Trim) & "','" & GV.parseString(Vstatus) & "','" & GV.parseString(VresText) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "','" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "') ; "
    '                                If RecCount("  " & DBName & ".dbo.BOS_Recharge_API where API_TransId='" & v_OrderId & "' ") > 0 Then
    '                                    Dim Refund_TransID As String = get_AutoNumber("TransId")
    '                                    qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_Status,Refund_TransID,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "','Yes','" & Refund_TransID & "','" & v_OrderId & "','" & v_status & "','" & v_msg & "',getdate());"
    '                                    qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
    '                                    qry = qry & " update " & DBName & ".dbo.BOS_Recharge_API set API_status='" & v_status & "',API_resText='" & v_msg & "',Refund_Status='Yes',Refund_TransID='" & Refund_TransID & "' where API_TransId='" & v_OrderId & "' ;"
    '                                    qry = qry & " insert into " & DBName & ".dbo.BOS_TransferAmountToAgents (Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) select Amt_Transfer_TransID,API_TransId,Actual_Transaction_Amount, '" & Refund_TransID & "' as Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg=Replace(Replace(Replace(TransferFromMsg,'RECH','RECH-Refund'),'BILLPAY','BILLPAY-Refund'),'Landline','Landline-Refund'),TransferFromMsg=Replace(Replace(Replace(TransferToMsg,'RECH','RECH-Refund'),'BILLPAY','BILLPAY-Refund'),'Landline','Landline-Refund'),Amount_Type=Amount_Type+'-Refund',Remark=Remark+'-Refund',TransactionDate,TransferFrom=TransferTo,TransferTo=TransferFrom,TransferAmt,getdate() as 'RecordDateTime','Admin' as 'UpdatedBy',getdate()  as 'UpdatedOn' from " & DBName & ".dbo.BOS_TransferAmountToAgents where API_TransId='" & v_OrderId & "' ; "
    '                                    RefundedStatus = True

    '                                Else
    '                                    orderIDFound = False
    '                                    qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & " - Initial - 2 -',getdate());"
    '                                End If
    '                            ElseIf v_status.Trim.ToUpper = "1".ToUpper Then
    '                                qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & " - Initial - 1 -',getdate());"
    '                                con = New SqlConnection("Server=103.90.242.173;DataBase=BosCenter_DB;user id=Bos_New_User;password=jWr6$n90".ToString())
    '                                If RecCount("  " & DBName & ".dbo.BOS_Recharge_API where API_TransId='" & v_OrderId & "' ") > 0 Then
    '                                    qry = qry & " update " & DBName & ".dbo.BOS_Recharge_API set API_status='" & v_status & "',API_resText='" & v_msg & "',Refund_Status='No',Refund_TransID=NULL where API_TransId='" & v_OrderId & "' ;"
    '                                    qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
    '                                Else
    '                                    orderIDFound = False
    '                                End If
    '                            Else
    '                                qry = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('" & CompanyCode & "',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & " - Initial - else -',getdate());"
    '                                qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
    '                            End If

    '                            cn = New SqlConnection("Server=103.90.242.173;DataBase=BosCenter_DB;user id=Bos_New_User;password=jWr6$n90".ToString())
    '                            cmd = New SqlCommand(qry, cn)
    '                            cmd.CommandType = CommandType.Text
    '                            cn.Open()
    '                            result = cmd.ExecuteNonQuery
    '                            cn.Close()

    '                            If orderIDFound = False Then
    '                                Return "Order ID Not Found"
    '                            Else
    '                                If result > 0 Then
    '                                    If RefundedStatus = True Then
    '                                        Return "Refunded Successfully"
    '                                    Else
    '                                        Return "Status Updated Successfully"
    '                                    End If

    '                                Else
    '                                    Return "An Error occurred"
    '                                End If
    '                            End If

    '                        Else
    '                            ' Status Already updated
    '                            Return "Order Status Already Updated."
    '                        End If
    '                    Else
    '                        Return "API Status Is Null."
    '                        'Found null
    '                    End If








    '                Else
    '                    Return "Order ID Not Found"
    '                End If
    '            Else
    '                Return "Order ID Not Found"
    '            End If
    '        Else
    '            Return "Order ID Not Found"
    '        End If

    '    Catch ex As Exception
    '        v_msg = v_msg & "- Exception - " & ex.Message
    '        Dim qry As String = "insert into " & GV.DefaultDatabase.Trim & ".dbo.RechargeRefundCallback(CompanyCode,Refund_TransID,Refund_Status,API_OrderId,API_status,API_Msg,RecordDatetime) values ('CMP1045',Null,Null,'" & v_OrderId & "','" & v_status & "','" & v_msg & "',getdate()); "
    '        qry = qry & " update " & GV.DefaultDatabase.Trim & ".dbo.Recharge_API_DB_Info set API_status='" & v_status & "',API_resText='" & v_msg & "' where API_TransId='" & v_OrderId & "' ;"
    '        Dim cn As SqlConnection = New SqlConnection("Server=103.90.242.173;DataBase=BosCenter_DB;user id=Bos_New_User;password=jWr6$n90".ToString())
    '        Dim cmd As New SqlCommand(qry, cn)
    '        cmd.CommandType = CommandType.Text
    '        cn.Open()
    '        result = cmd.ExecuteNonQuery
    '        cn.Close()
    '        Return "An exception occurred"

    '    End Try
    'End Function

    Public Function get_AutoNumber(ByVal fieldname As String) As Integer 'done
        Dim valueadd As Integer = 0
        Try
            Dim dsAutoNumber As New DataSet
            Dim qry As String = ""
            qry = " Declare @result nvarchar(max) "
            qry = qry + " if (select count(*) from AutoNumber)>0"
            qry = qry + " begin"
            qry = qry + " update AutoNumber set " + fieldname + "=ISNULL(" + fieldname + ",0)+1 "
            qry = qry + " set @result=(select " + fieldname + " from AutoNumber); "
            qry = qry + " end"
            qry = qry + " else"
            qry = qry + " begin"
            qry = qry + " insert into AutoNumber(" + fieldname + ") values('1')"
            qry = qry + " set @result=(select 1 as " + fieldname + ");"
            qry = qry + " end"
            qry = qry + " select  @result as " & fieldname

            dsAutoNumber = OpenDsWithSelectQuery(qry)

            If Not dsAutoNumber Is Nothing Then
                If dsAutoNumber.Tables.Count > 0 Then
                    If dsAutoNumber.Tables(0).Rows.Count > 0 Then
                        valueadd = dsAutoNumber.Tables(0).Rows(0).Item("" & fieldname & "")
                    Else
                        Return valueadd
                    End If
                Else
                    Return valueadd
                End If
            Else
                Return valueadd
            End If


        Catch ex As Exception
            Return valueadd
        End Try
        Return valueadd
    End Function

    Public Function get_AutoNumber_Admin(ByVal fieldname As String) As Integer 'done
        Dim valueadd As Integer = 0
        Try
            Dim dsAutoNumber As New DataSet
            Dim qry As String = ""
            qry = " Declare @result nvarchar(max) "
            qry = qry + " if (select count(*) from [BosCenter_DB].[dbo].AutoNumber)>0"
            qry = qry + " begin"
            qry = qry + " update [BosCenter_DB].[dbo].AutoNumber set " + fieldname + "=ISNULL(" + fieldname + ",0)+1 "
            qry = qry + " set @result=(select " + fieldname + " from [BosCenter_DB].[dbo].AutoNumber); "
            qry = qry + " end"
            qry = qry + " else"
            qry = qry + " begin"
            qry = qry + " insert into [BosCenter_DB].[dbo].AutoNumber(" + fieldname + ") values('1')"
            qry = qry + " set @result=(select 1 as " + fieldname + ");"
            qry = qry + " end"
            qry = qry + " select  @result as " & fieldname

            dsAutoNumber = OpenDsWithSelectQuery(qry)

            If Not dsAutoNumber Is Nothing Then
                If dsAutoNumber.Tables.Count > 0 Then
                    If dsAutoNumber.Tables(0).Rows.Count > 0 Then
                        valueadd = dsAutoNumber.Tables(0).Rows(0).Item("" & fieldname & "")
                    Else
                        Return valueadd
                    End If
                Else
                    Return valueadd
                End If
            Else
                Return valueadd
            End If


        Catch ex As Exception
            Return valueadd
        End Try
        Return valueadd
    End Function


    Public con As SqlConnection
    Public Function executeDMLQuery(ByVal Query As String) As Integer   'done
        Dim RowAffected As Integer = 0

        Try
            conOpen()
            Dim cmd As SqlCommand
            cmd = New SqlCommand(Query, con)
            cmd.CommandType = CommandType.Text
            RowAffected = cmd.ExecuteNonQuery
            con.Close()
           
        Catch ex As Exception

        End Try
        Return RowAffected
    End Function
    Public Function RecCount(ByVal Table_Name As String) As Integer  'done
        Dim affectedRows As Integer = 0
        Try

            Dim ds11 As New DataSet
            ds11 = OpenDsWithSelectQuery("select count(*) as 'TotalRow' from " & Table_Name)
            If Not ds11 Is Nothing Then
                If ds11.Tables.Count > 0 Then
                    If ds11.Tables(0).Rows.Count > 0 Then
                        affectedRows = CInt(ds11.Tables(0).Rows(0).Item("TotalRow"))
                    Else
                        affectedRows = 0
                    End If
                Else
                    affectedRows = 0
                End If
            Else
                affectedRows = 0
            End If

            Return affectedRows
            Exit Function
        Catch ex As Exception
            affectedRows = -1
            Return affectedRows
            Exit Function
        End Try
        Return affectedRows
    End Function
    Public Function OpenDsWithSelectQuery(ByVal Query As String) As DataSet  'done
        Try
            conOpen()
            ds = New DataSet
            Dim da As SqlDataAdapter = New SqlDataAdapter(Query, con)
            da.SelectCommand.CommandTimeout = 300000
            da.Fill(ds)
            con.Close()
        Catch ex As Exception

        End Try
        Return ds
    End Function

    Public Function AddInVar(ByVal retrivefield As String, ByVal tablename As String) As String 'done
        Try
            Dim str, variablename As String
            conOpen()
            Dim dsVar As New DataSet
            str = "select " & retrivefield & " from " & tablename
            dsVar = OpenDsWithSelectQuery(str)
            If dsVar.Tables(0).Rows.Count = 0 Then
                variablename = ""
                con.Close()
                Return variablename
                Exit Function
            Else
                If IsDBNull(dsVar.Tables(0).Rows(0).Item(0)) = True Then
                    variablename = ""
                Else
                    variablename = dsVar.Tables(0).Rows(0).Item(0)
                End If
                con.Close()
                Return variablename
                Exit Function
            End If
        Catch ex As Exception

        End Try
        Dim s As String = ""
        Return s
    End Function
    Public Sub conOpen() 'done
        Try
            con = New SqlConnection("Server=103.155.85.146;DataBase=BosCenter_DB;user id=sa;password=Target@123".ToString())
            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function parseString(ByVal str As String) As String
        If str = "&nbsp;" Or str = "" Then
            Return ""
        Else
            Return StrConv(Replace(Replace(Replace(Replace(str, "'", "''"), "&amp;", "&"), "&nbsp;", ""), "&#39;", "''"), VbStrConv.None).Trim().Replace(";", ",")
        End If
    End Function

    ' POST api/<controller>
    Public Sub PostValue(<FromBody()> ByVal value As String)

    End Sub

    ' PUT api/<controller>/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub

    Public Function returnWalletBalCalculation(loginId As String, DatabaseName As String) As Decimal
        Dim BAlAMount As Decimal = 0
        Try

            Dim FromAmount As String = AddInVar("Sum(isnull(TransferAmt,0))", "" & DatabaseName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferFrom='" & loginId & "'")
            Dim ToAmount As String = AddInVar("Sum(isnull(TransferAmt,0))", "" & DatabaseName.Trim & ".dbo.BOS_TransferAmountToAgents where TransferTo='" & loginId & "'")
            Dim HoldAmt As String = AddInVar("HoldAmt", "" & DatabaseName.Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & loginId & "'")

            If FromAmount.Trim = "" Then
                FromAmount = "0"
            End If
            If ToAmount.Trim = "" Then
                ToAmount = "0"
            End If
            If HoldAmt = "" Then
                HoldAmt = "0"
            End If

            BAlAMount = CDec(ToAmount) - CDec(FromAmount) - CDec(HoldAmt)


        Catch ex As Exception

        End Try
        Return BAlAMount
    End Function


End Class
