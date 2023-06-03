Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Net
Imports RestSharp
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Asn1
Imports AjaxControlToolkit
Imports iTextSharp.text.pdf.AcroFields
Imports Newtonsoft
Imports System.Security.Principal
Imports System.Security.Cryptography.X509Certificates
Imports System.Diagnostics.Eventing.Reader
Imports System.Net.WebRequestMethods
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.Drawing
Imports iTextSharp.text.pdf.qrcode

Public Class BOS_FidyPayOut_API
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    Dim VBene_Name, VBene_Mobile, VBene_AccountNumber, VBene_IFSC, VBene_Email, VBene_Address As String
    'UAT Bank Key 
    'Dim bankaccountkey As String = "334PHUKSGDL0NYW"

    'PROD Bank Key 
    Dim bankaccountkey As String = "473PHAIC7IYSV23OXB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Dim Querystring As String = ""
    Protected Sub btn_Search_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            lblError.CssClass = ""
            Dim Payout_API_Status As String = ""
            Dim RechargeAPI As String = ""

            Payout_API_Status = "Payout_API_2_Status"

            '///// Start Check API  STATUS Super Admin Level 

            Payout_API_Status = GV.FL.AddInVar("Payout_API_2_Status", " " & GV.DefaultDatabase.Trim & ".dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "'")
            If Not Payout_API_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! Recharge API Is Inactive At Company Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If
            '///// End Check API  STATUS Super Admin Level  

            '///// Start Check API  STATUS System Settings 


            'Payout_API_Status = ""
            Payout_API_Status = GV.FL.AddInVar("Payout_API_2_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[AutoNumber]")

            If Not Payout_API_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Sorry! Recharge API Is Inactive At Admin Level, Contact to Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level Settings 

            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            '///// Start Check API  STATUS System Settings 
            Payout_API_Status = ""
            Payout_API_Status = GV.FL.AddInVar("Payout_API_2_Status", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

            If Not Payout_API_Status.Trim.ToUpper = "ACTIVE" Then
                lblError.Text = "Service Is Inactive At Your Account, Contact To Administrator"
                lblError.CssClass = "errorlabels"
                Exit Sub
            End If

            '///// End Check API  STATUS Retailer Level  Settings 
            If Payout_API_Status.Trim.ToUpper = "ACTIVE" Then
                Bind()
                DIVGrid.Visible = True
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub gvShowDetails_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            'Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'Dim lbl As Label = DirectCast(gvShowDetails.Rows(gvrow.RowIndex).Cells(0).FindControl("lnkbtnSelect"), Label)

            'txt_bene_Mobile.Text = gvShowDetails.SelectedRow.Cells(1).ToString()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub lnkbtnSelect_click(sender As Object, e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(gvShowDetails.Rows(gvrow.RowIndex).Cells(0).FindControl("lnkbtnSelect"), Label)

            txt_bene_AccountNumber.Text = gvShowDetails.Rows(gvrow.RowIndex).Cells(1).Text
            txt_bene_IFSC.Text = gvShowDetails.Rows(gvrow.RowIndex).Cells(2).Text
            txt_bene_Name.Text = gvShowDetails.Rows(gvrow.RowIndex).Cells(3).Text
            txt_ben_Email.Text = gvShowDetails.Rows(gvrow.RowIndex).Cells(4).Text
            txt_bene_Mobile.Text = gvShowDetails.Rows(gvrow.RowIndex).Cells(5).Text
            txt_bene_Address.Text = gvShowDetails.Rows(gvrow.RowIndex).Cells(6).Text
            txt_bene_AccountNumber.ReadOnly = True
            txt_bene_IFSC.ReadOnly = True
            txt_bene_Name.ReadOnly = True
            txt_ben_Email.ReadOnly = True
            txt_bene_Mobile.ReadOnly = True
            txt_bene_Address.ReadOnly = True

            DivTransacaton.Visible = True
            btn_bene_Save.Text = "Pay"

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub Bind()
        Try
            Dim SearchColumnName As String = ""
            Dim colName As String = ""
            Querystring = "select BID,AccountNumber,IfscCode,AccountName,EmailID,MobileNo,AccountAddress,AccountStatus from BOS_beneficiaryDetails where MobileNo='" & GV.parseString(txt_MobileNo.Text) & "'"
            If Not Querystring = "" Then
                GV.FL.AddInGridViewWithFieldName(gvShowDetails, Querystring)
                gvShowDetails.DataBind()
            Else

            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            'lblNoRecords.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnGrdRowTransfer_Click(sender As Object, e As EventArgs)
        Try

            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            'lblRowIndex.Text = gvrow.RowIndex

            'For i As Integer = 0 To grdAddRecepient.Rows.Count - 1
            '    grdAddRecepient.Rows(i).BackColor = Color.White
            'Next

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



            Dim VadminComAmt, DistributorComAmt, SubDIsComAmt, VRetailerComAmt, VGstAmt As Decimal
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

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Pay Out' and ActiveStatus='Active'"
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

                            Dim Amount1 As String = GV.parseString(txt_Transc_Amount.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Pay Out'; "

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


                                Dim Amount1 As String = GV.parseString(txt_Transc_Amount.Text.Trim)
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



                                qry = " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Pay Out' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & SubDisID & "') ; "
                                qry = qry & " Select  * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_OperatorWiseCommission_agents where APIName='Pay Out' and  RegistrationID in (select RefrenceID from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & RetailerID & "') ; "




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

                                Dim Amount1 As String = GV.parseString(txt_Transc_Amount.Text.Trim)
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

            Dim qryStr As String = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Pay Out' and ActiveStatus='Active'"
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

                            Dim Amount1 As String = GV.parseString(txt_bene_Mobile.Text.Trim)
                            If Amount1.Trim = "" Then
                                Amount1 = "0"
                            End If
                            Dim Amount As Decimal = Amount1


                            qry = " select * from  " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_CommissionSlabwise where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Pay Out'; "

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

                                Dim Amount1 As String = GV.parseString(txt_bene_Mobile.Text.Trim)
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

                                Dim Amount1 As String = GV.parseString(txt_bene_Mobile.Text.Trim)
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

            Dim qryStr As String = "select * from " & GV.DefaultDatabase.Trim & ".dbo.BOS_ProductServiceVsAdmin_SA where Title='Pay Out' and ActiveStatus='Active' and AdminID='" & AdminID & "'"
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

                        Dim Amount1 As String = GV.parseString(txt_Transc_Amount.Text.Trim)
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


                            qry = " select * from  " & GV.DefaultDatabase.Trim & ".dbo.BOS_CommissionSlabwiseVsAdmin_SA where (" & Amount & ">=FromAmount and  " & Amount & "<ToAmount) and APIName='Pay Out' and AdminID='" & AdminID & "'; "

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


    Dim APIResult As String = ""
    Protected Sub btn_bene_Save_Click(sender As Object, e As EventArgs)
        Try

            lbl_Error.Text = ""
            lbl_Error.CssClass = ""

            If txt_bene_Name.Text = "" Then
                lbl_Error.Text = "Please Enter Beneficiary Name"
                lbl_Error.CssClass = "errorlables"
                txt_bene_Name.Focus()
            Else
                VBene_Name = GV.parseString(txt_bene_Name.Text)
            End If


            If txt_bene_Mobile.Text = "" Then
                lbl_Error.Text = "Please Enter Beneficiary Number"
                lbl_Error.CssClass = "errorlabels"
                txt_bene_Mobile.Focus()
            Else
                VBene_Mobile = GV.parseString(txt_bene_Mobile.Text)
            End If

            If txt_bene_AccountNumber.Text = "" Then
                lbl_Error.Text = "Please Enter Beneficiary Account Number"
                lbl_Error.CssClass = "errorlabels"
                txt_bene_AccountNumber.Focus()
            Else
                VBene_AccountNumber = GV.parseString(txt_bene_AccountNumber.Text)
            End If

            If txt_bene_IFSC.Text = "" Then
                lbl_Error.Text = "Please Enter Beneficiary IFSC Code"
                lbl_Error.CssClass = "errorlabels"
                txt_bene_IFSC.Focus()
            Else
                VBene_IFSC = GV.parseString(txt_bene_IFSC.Text)
            End If

            If txt_ben_Email.Text = "" Then
                lbl_Error.Text = "Please Enter Beneficiary Email"
                lbl_Error.CssClass = "errorlabels"
                txt_ben_Email.Focus()
            Else
                VBene_Email = GV.parseString(txt_ben_Email.Text)
            End If

            If txt_bene_Address.Text = "" Then
                lbl_Error.Text = "Please Enter Beneficiary Address"
                lbl_Error.CssClass = "errorlabels"
                txt_bene_Address.Focus()
            Else
                VBene_Address = GV.parseString(txt_bene_Address.Text)
            End If

            Dim VCreatedBy, VAccountStatus As String
            Dim RetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)

            VCreatedBy = GV.get_Admin_SessionVariables("LoginID", Request, Response)

            VAccountStatus = "Active"

            If btn_bene_Save.Text = "Save" Then

                Dim AccountNumber As String = GV.FL_AdminLogin.AddInVar("AccountNumber", " BOS_beneficiaryDetails where AccountNumber='" & GV.parseString(txt_bene_AccountNumber.Text) & "'")

                If AccountNumber = String.Empty Then
                    Dim InsrtQry As String = "Insert into BOS_beneficiaryDetails(AccountNumber,IfscCode,AccountName,AccountAddress,EmailID,MobileNo,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,AccountStatus,RegistrationId,CompanyCode) values('" & VBene_AccountNumber & "','" & VBene_IFSC & "','" & VBene_Name & "','" & VBene_Address & "','" & VBene_Email & "','" & VBene_Mobile & "','" & VCreatedBy & "',getdate(),'" & VCreatedBy & "',getdate(),'" & VAccountStatus & "','" & RetailerID & "','" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "')"
                    GV.FL.DMLQueries(InsrtQry)
                    lbl_Error.Text = "Record Insert Succesfully"
                    lbl_Error.CssClass = "Successlabels"
                    Clear()
                Else
                    lbl_Error.Text = "Account No : " & txt_bene_AccountNumber.Text & " Already Exsits."
                    lbl_Error.Text = "Record Insertion Fail"
                    lbl_Error.CssClass = "errorlabels"
                End If
            End If

            If btn_bene_Save.Text = "Pay" Then
                If txt_Transc_Amount.Text = "0" Then
                    lbl_Error.Text = "Transaction Amount can't be Zero"
                    lbl_Error.CssClass = "errorlabels"
                ElseIf txt_Transc_Amount.Text < Convert.ToDecimal(10000) Or txt_Transc_Amount.Text > Convert.ToDecimal(200000) Then
                    lbl_Error.Text = "Transaction Amount can't be less then 10000 or greater then 200000"
                    lbl_Error.CssClass = "errorlabels"
                ElseIf txt_Transc_Remarks.Text = String.Empty Then
                    lbl_Error.Text = "Remarks can't be Blank"
                    lbl_Error.CssClass = "errorlabels"
                ElseIf txt_Transc_Pin.Text = String.Empty Then
                    lbl_Error1.Text = "Please Enter Your Transaction Pin."
                    lbl_Error1.CssClass = "errorlabels"
                    Exit Sub
                Else
                    Dim holdAmt As String = ""
                    holdAmt = GV.FL.AddInVar(" isnull(HoldAmt,0) ", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")
                    If holdAmt.Trim = "" Then
                        holdAmt = "0"
                    End If

                    lblWalletBal.Text = GV.returnWalletBalCalculation(GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response), GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)

                    If (CDec(lblWalletBal.Text.Trim) - CDec(holdAmt)) >= CDec(txt_Transc_Amount.Text) Then
                    Else
                        lbl_Error1.Text = "You Have Insufficient Balance."
                        lbl_Error1.CssClass = "errorlabels"
                        Exit Sub
                    End If


                    '///// Check For API Balance - Start //////
                    If CDec(txt_Transc_Amount.Text) > GV.returnAPIBalance(GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim) Then
                        lbl_Error1.Text = "Insufficient API Balance."
                        lbl_Error1.CssClass = "errorlabels"
                        Exit Sub
                    End If
                    '///// Check For API Balance - End //////

                    Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                    Dim TransPiNo As String = ""
                    TransPiNo = GV.FL.AddInVar("TransactionPin", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.[BOS_Dis_SubDis_Retailer_Registration] where RegistrationId='" & RetailerID & "'")

                    If TransPiNo.Trim = txt_Transc_Pin.Text.Trim Then
                    Else
                        lbl_Error1.Text = "Invalid transaction Pin"
                        lbl_Error1.CssClass = "errorlabels"
                        Exit Sub
                    End If

                    'Dim VAccount_No, VAccountholder_Name, VIFSC_Code, VEmail, VAddress, VCity, VState, VPincode As String

                    lblWalletBal.Visible = False
                    Dim v_Transfer_Trans_ID As String

                    APIResult = GetAPISParametars("VerifyIfscCode_API_Parameters")
                    Dim json_ As String = APIResult
                    Dim ser_ As JObject = JObject.Parse(json_)
                    Dim branch As String = ser_.SelectToken("branch").ToString.Trim
                    Dim bank As String = ser_.SelectToken("bank").ToString.Trim
                    Dim ifsc As String = ser_.SelectToken("ifsc").ToString.Trim
                    Dim code As String = ser_.SelectToken("code").ToString.Trim
                    Dim status As String = ser_.SelectToken("status").ToString.Trim

                    If status = "Success" Then

                        v_Transfer_Trans_ID = GV.get_AutoNumber("TransId", "BosCenter_DB")


                        'Dim InsrtQry As String = "insert into " & GV.get_Admin_SessionVariables("DBName", Request, Response) & ".dbo.FidyPayoutResponse(amount ,code,baneAddress,beneDescription,beneficiaryIfscCode,merchantTrxnRefId,trxn_id,debitAccNo,utr,beneficiaryAccNo,beneficiaryName,instructionIdentification,bankaccountKey,transactionIdentification,banestatus,creationDateTime,ApiResponse,LoginID,CompanyCode) Values('" & txt_Transc_Amount.Text.ToString.Trim & "','" & sers_.SelectToken("code").ToString.Trim & "','" & sers_.SelectToken("address").ToString.Trim & "','" & sers_.SelectToken("description").ToString.Trim & "',
                        '        '" & sers_.SelectToken("beneficiaryIfscCode").ToString.Trim & "','" & sers_.SelectToken("merchantTrxnRefId").ToString.Trim & "','" & sers_.SelectToken("trxn_id").ToString.Trim & "','" & sers_.SelectToken("debitAccNo").ToString.Trim & "','" & sers_.SelectToken("utr").ToString.Trim & "','" & sers_.SelectToken("beneficiaryAccNo").ToString.Trim & "','" & sers_.SelectToken("beneficiaryName").ToString.Trim & "','" & sers_.SelectToken("instructionIdentification").ToString.Trim & "','" & sers_.SelectToken("bankaccountKey").ToString.Trim & "','" & sers_.SelectToken("transactionIdentification").ToString.Trim & "','" & sers_.SelectToken("status").ToString.Trim & "','" & sers_.SelectToken("creationDateTime").ToString.Trim & "','" & sers_.ToString & "'
                        '        ,'" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "','" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "')"

                        'Dim result As Boolean = GV.FL.DMLQueriesBulk(InsrtQry)

                        Dim VUpdatedBy, VUpdatedOn, VRecord_DateTime As String
                        VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                        VUpdatedOn = "getdate()"
                        VRecord_DateTime = "getdate()"
                        Dim Qry As String = ""
                        Dim TypeName As String = "Pay Out"
                        Dim GRP As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper
                        If GRP = "Retailer".ToUpper Then
                            Dim GstAmt As Decimal = 0
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

                                    Dim typeAmtForm As String = "Your Account is debited by " & txt_Transc_Amount.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txt_Transc_Amount.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & "."


                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."

                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."


                                    Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & TrxnId & "','" & txt_Transc_Amount.Text.Trim & "','" & GV.parseString(TrxnId) & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & RTEID & "','Admin','" & txt_Transc_Amount.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"


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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "', '" & GV.parseString(lblPopTransactionId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "', '" & GV.parseString(lblPopTransactionId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & DisID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "','" & GV.parseString(lblPopTransactionId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END


                                Else
                                    'vCanChange.Trim.ToUpper = "NO"

                                    Dim typeAmtForm As String = "Your Account is debited by " & txt_Transc_Amount.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & "."
                                    Dim typeAmtTo As String = "Your Account is credited by " & txt_Transc_Amount.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & "."

                                    Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim Distypecommfrom As String = "Your Account is debited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim SDtypecommfrom As String = "Your Account is debited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim RTEtypecommfrom As String = "Your Account is debited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."

                                    Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim DistypecommTo As String = "Your Account is credited by commission " & DisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim SDtypecommTo As String = "Your Account is credited by commission " & SUBDisCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                    Dim RTEtypecommTo As String = "Your Account is credited by commission " & RTECom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."


                                    Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID.ToString & "','" & txt_Transc_Amount.Text.Trim & "','" & v_Transfer_Trans_ID & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & RTEID & "','Admin','" & txt_Transc_Amount.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    'Qry = Qry & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "','" & GV.parseString(lblPopTransactionId.Text.Trim) & "','" & typeAmtForm & "','" & typeAmtTo & "','" & TypeName & "','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & RTEID & "','Admin','" & txt_Transc_Amount.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID.ToString & "','" & txt_Transc_Amount.Text.Trim & "','" & v_Transfer_Trans_ID & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','Admin','SUPERADMIN','" & txt_Transc_Amount.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "', '" & v_Transfer_Trans_ID & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & DistypecommTo & "','" & Distypecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & adminID & "','" & DisID & "','" & DisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "', '" & v_Transfer_Trans_ID & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & SDtypecommTo & "','" & SDtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & adminID & "','" & SUBDisID & "','" & SUBDisCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "', '" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "','" & v_Transfer_Trans_ID & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & adminID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If
                                    'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                    '//// Retailer Commission Calculation - END
                                End If
                            End If


                            Dim admin_Cm_ServcCharge As String = adminCommisionCalMoneyTransfer()
                            'Dim GstAmt As Decimal = 0
                            If Not admin_Cm_ServcCharge.Trim = "" Then
                                If admin_Cm_ServcCharge.Contains(":") Then
                                    Dim rawSplit() As String = admin_Cm_ServcCharge.Split(":")
                                    Dim Admin_Com_Amt As String = rawSplit(0)
                                    Dim service_Charge_Amt As String = rawSplit(1)
                                    GstAmt = ((service_Charge_Amt * 18) / 100)
                                    service_Charge_Amt = service_Charge_Amt + GstAmt

                                    'service_Charge_Amt = (service_Charge_Amt * 18) / 100


                                    If Not IsNumeric(service_Charge_Amt.Trim) Then
                                        service_Charge_Amt = 0
                                    End If
                                    'If service_Charge_Amt > 0 And service_Charge_Amt < 10 Then
                                    '    service_Charge_Amt = 10
                                    'End If

                                    If service_Charge_Amt > 0 Then
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & service_Charge_Amt & " Rs. inculding GST Due to " & TypeName & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & service_Charge_Amt & " Rs. inculding GST Due to " & TypeName & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "', '" & v_Transfer_Trans_ID & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date.ToString("MM-dd-yyyy") & "','Admin','Super Admin','" & service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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

                                        Dim Admintypecommfrom As String = "Your Account is debited by commission " & V_Actual_Commission_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                        Dim AdmintypecommTo As String = "Your Account is credited by commission " & V_Actual_Commission_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "', '" & v_Transfer_Trans_ID & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','Super Admin','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If


                                End If
                            End If


                            Dim NetAmount As Decimal = 0
                            Dim Service() As String

                            If GRP = "Retailer".ToUpper Then
                                Service = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Pay Out' and Service_Charge_Type='Retailer'").Split(":")
                            ElseIf GRP = "Customer".ToUpper Then
                                Service = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Pay Out' and Service_Charge_Type='Customer'").Split(":")
                            End If
                            'If CDec(txt_Transc_Amount.Text.Trim) > 1000 Then

                            If Service.Length > 1 Then
                                If Service(1).Trim = "Percentage" Then
                                    'lblService.Text = Service(0) & " %"
                                    NetAmount = (CDec(txt_Transc_Amount.Text.Trim) * CDec(Service(0))) / 100
                                    GstAmt = ((NetAmount * 18) / 100)
                                    NetAmount = NetAmount + GstAmt
                                    'If NetAmount < 25 Then
                                    '    NetAmount = 25
                                    'End If
                                ElseIf Service(1).Trim = "Amount" Then
                                    'lblService.Text = Service(0)
                                    NetAmount = CDec(Service(0))
                                    GstAmt = ((NetAmount * 18) / 100)
                                    NetAmount = NetAmount + GstAmt
                                ElseIf Service(1).Trim = "Not Applicable" Then
                                    'lblService.Text = Service(0)
                                    'NetAmount = CDec(Service(0))
                                    'GstAmt = ((5 * 18) / 100)
                                    'NetAmount = (5) + GstAmt
                                End If
                            End If
                            'Else
                            '    'GstAmt = ((5 * 18) / 100)
                            '    'NetAmount = (5) + GstAmt
                            '    'NetAmount = 10
                            'End If

                            If NetAmount > 0 Then
                                Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount.ToString & " Rs. inculding GST Due to Money Transfer / AMT " & txt_Transc_Amount.Text.Trim & "."
                                Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount.ToString & " Rs. Including GST Due to Money Transfer / AMT " & txt_Transc_Amount.Text.Trim & "."
                                Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "','" & v_Transfer_Trans_ID & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date.ToString("MM-dd-yyyy") & "','" & RTE & "','ADMIN','" & GV.parseString(NetAmount.ToString) & "',getdate(),'Admin',getdate() ) ;"
                            End If


                            Dim Transaction_Charge_Amt As Decimal = 2.5
                            Dim VFrom_Trans As String = "Your Account is debited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                            Dim VTo_Trans As String = "Your Account is credited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                            Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "', '" & v_Transfer_Trans_ID & "','" & VTo_Trans & "','" & VFrom_Trans & "','Transaction Charge','Transaction Charge','" & Now.Date.ToString("MM-dd-yyyy") & "','Admin','Super Admin','" & Transaction_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                            GV.FL.DMLQueriesBulk(Qry)



                            lblDialogMsgInfo.Text = GV.FL.AddInVar("CompanyName", " BosCenter_DB.dbo.BOS_ClientRegistration where CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim & "' ")
                            'lblPopAgencyName.Text = GV.FL.AddInVar("AgencyName", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response) & "'")

                            'lblPopDateTime.Text = Now.ToString("dd/MM/yyyy HH:mm:ss")
                            'lblPopTransactionId.Text = lblPopTransactionId.Text.Trim
                            'lblPopTransferAmt.Text = GV.parseString(txt_Transc_Amount.Text.Trim)
                            'lblPopStatus.Text = VerifyMessage1
                            'lblPopAccountNo.Text = lblRAccountNo.Text.Trim
                            'lblPopServiceCharge.Text = txtServiceCharge.Text.Trim
                            'lblpopMobileNo.Text = txt_bene_Mobile.Text.Trim
                            'lblpopBankName.Text = lblRBankName.Text.Trim
                            'ModalPopupExtender3.Show()
                            'Exit Sub
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


                                Dim typeAmtForm As String = "Your Account is debited by " & txt_Transc_Amount.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & "."
                                Dim typeAmtTo As String = "Your Account is credited by " & txt_Transc_Amount.Text.Trim & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & "."

                                Dim Admintypecommfrom As String = "Your Account is debited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                Dim Custtypecommfrom As String = "Your Account is debited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."

                                Dim AdmintypecommTo As String = "Your Account is credited by commission " & adminCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."
                                Dim CusttypecommTo As String = "Your Account is credited by commission " & CustCom & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & txt_Transc_Amount.Text.Trim & "."


                                Qry = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "','" & v_Transfer_Trans_ID & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & CustID & "','Admin','" & txt_Transc_Amount.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                'Qry = Qry & " " & "insert into BOS_TransferAmountToAgents (TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & AdmintypecommTo & "','" & Admintypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','SuperAdmin','" & adminID & "','" & adminCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Amt_Transfer_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID.ToString & "','" & txt_Transc_Amount.Text.Trim & "','" & v_Transfer_Trans_ID & "','" & typeAmtTo & "','" & typeAmtForm & "','" & TypeName & "','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','Admin','SUPERADMIN','" & txt_Transc_Amount.Text.Trim & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

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
                                    Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "','" & GV.parseString(lblPopTransactionId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & CusttypecommTo & "','" & Custtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & "ADMIN" & "','" & CustID & "','" & CustCom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                End If
                                'QryStr = QryStr & " " & "insert into BOS_TransferAmountToAgents (Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( " & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & RTEtypecommTo & "','" & RTEtypecommfrom & "','Commission','" & TypeName & "','" & Now.Date & "','" & SUBDisID & "','" & RTEID & "','" & RTECom & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                '//// customer Commission Calculation - END
                                Dim NetAmount, GstAmt As Decimal
                                Dim Service() As String
                                If GRP = "Retailer".ToUpper Then
                                    Service = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Pay Out' and Service_Charge_Type='Retailer'").Split(":")
                                ElseIf GRP = "Customer".ToUpper Then
                                    Service = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Pay Out' and Service_Charge_Type='Customer'").Split(":")
                                End If

                                'Dim Service() As String = GV.FL.AddInVar("convert(nvarchar,ServiceCharge)+':'+ServiceType", " " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_ProductServiceMaster where Title='Pay Out'").Split(":")

                                'If CDec(txt_Transc_Amount.Text.Trim) > 1000 Then
                                If Service.Length > 1 Then
                                    If Service(1).Trim = "Percentage" Then
                                        'lblService.Text = Service(0) & " %"
                                        NetAmount = (CDec(txt_Transc_Amount.Text.Trim) * CDec(Service(0))) / 100
                                        GstAmt = ((NetAmount * 18) / 100)
                                        NetAmount = NetAmount + GstAmt
                                        'If NetAmount < 25 Then
                                        '    NetAmount = 25
                                        'End If
                                    ElseIf Service(1).Trim = "Amount" Then
                                        'lblService.Text = Service(0)
                                        NetAmount = CDec(Service(0))
                                        GstAmt = ((NetAmount * 18) / 100)
                                        NetAmount = NetAmount + GstAmt
                                        'ElseIf Service(1).Trim = "Not Applicable" Then
                                        '    'lblService.Text = Service(0)
                                        '    GstAmt = ((5 * 18) / 100)
                                        '    NetAmount = (5) + GstAmt
                                    End If
                                    'End If
                                    'Else
                                    '    GstAmt = ((5 * GST_Per) / 100)
                                    '    NetAmount = (5) + GstAmt
                                    'End If
                                    If NetAmount > 0 Then
                                        Dim RTE As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & NetAmount.ToString & " Rs. inculding GST Due to Money Transfer / AMT " & txt_Transc_Amount.Text.Trim & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & NetAmount.ToString & " Rs. inculding GST Due to Money Transfer / AMT " & txt_Transc_Amount.Text.Trim & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values('" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & txt_Transc_Amount.Text.Trim & "','" & GV.parseString(lblPopTransactionId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date.ToString("MM-dd-yyyy") & "','" & RTE & "','ADMIN','" & GV.parseString(NetAmount.ToString) & "',getdate(),'Admin',getdate() ) ;"
                                    End If




                                End If

                            End If

                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                            '//// Admin & Super Admin Commission Calculation - Start
                            If GRP = "Retailer".ToUpper Or GRP = "Customer".ToUpper Then

                                '//// Admin Commission Calculation - Start
                                Dim V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID, Comm_Result As String
                                Dim VCus_Amount, V_Actual_Commission_Amt, V_GSTAmt, V_Commission_Without_GST, V_TDS_Amt, V_Net_Commission_Amt As Decimal

                                If GV.parseString(txt_Transc_Amount.Text.Trim) = "" Then
                                    V_Amount = "0"
                                Else
                                    V_Amount = txt_Transc_Amount.Text.Trim
                                End If
                                VCus_Amount = V_Amount

                                V_OperatorCategory = ""
                                V_OperatorCode = ""
                                V_APIName = "Pay Out"
                                V_AdminID = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response).Trim

                                Comm_Result = GV.Commision_Calculation_For_Admin(V_Amount, V_OperatorCategory, V_OperatorCode, V_APIName, V_AdminID)

                                Dim Transaction_Charge_Amt As Decimal = 2.5
                                Dim VFrom_Trans As String = "Your Account is debited by TransactionCharge " & Transaction_Charge_Amt & " Rs. Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                Dim VTo_Trans As String = "Your Account is credited by TransactionCharge " & Transaction_Charge_Amt & " Rs. inculding GST Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & VCus_Amount & "', '" & GV.parseString(lblPopTransactionId.Text.Trim) & "','" & VTo_Trans & "','" & VFrom_Trans & "','Transaction Charge','Transaction Charge','" & Now.Date.ToString("MM-dd-yyyy") & "','Admin','Super Admin','" & Transaction_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"

                                If Not GV.parseString(Comm_Result) = "" Then
                                    Dim Result_Arry() As String = Comm_Result.Split("*")
                                    Dim Admin_Com() As String = Result_Arry(0).Split(":")
                                    Dim Admin_Com_ID As String = "Admin"
                                    Dim Admin_Com_Amt As String = Admin_Com(1)
                                    Dim GstAmt As Decimal = 0
                                    Dim Service_Charge() As String = Result_Arry(1).Split(":")
                                    Dim Service_Charge_ID As String = ""
                                    Dim Service_Charge_Amt As String = Service_Charge(1)
                                    GstAmt = ((Service_Charge_Amt * 18) / 100)
                                    Service_Charge_Amt = Service_Charge_Amt + GstAmt
                                    If Not IsNumeric(Service_Charge_Amt.Trim) Then
                                        Service_Charge_Amt = 0
                                    End If
                                    'If Service_Charge_Amt > 0 And Service_Charge_Amt < 10 Then
                                    '    Service_Charge_Amt = 10
                                    'End If

                                    If Service_Charge_Amt > 0 Then
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. inculding GST Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. inculding GST Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & VCus_Amount & "', '" & GV.parseString(lblPopTransactionId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date.ToString("MM-dd-yyyy") & "','Admin','Super Admin','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If



                                    Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & GV.parseString(txt_bene_Mobile.Text.Trim) & " / AMT " & VCus_Amount & "."
                                    Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_bene_Mobile.Text.Trim & " / AMT " & VCus_Amount & "."

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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & v_Transfer_Trans_ID & "','" & VCus_Amount & "', '" & GV.parseString(lblPopTransactionId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & "Super Admin" & "','Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
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
                                        Dim VFrom As String = "Your Account is debited by ServiceCharge " & Service_Charge_Amt & " Rs. inculding GST Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Dim VTo As String = "Your Account is credited by ServiceCharge " & Service_Charge_Amt & " Rs. inculding GST Due to " & TypeName & " / AMT " & VCus_Amount & "."
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "','" & v_Transfer_Trans_ID & "','" & VCus_Amount & "', '" & GV.parseString(lblPopTransactionId.Text.Trim) & "','" & VTo & "','" & VFrom & "','Service Charge','Service Charge','" & Now.Date.ToString("MM-dd-yyyy") & "','Super Admin','API Partner','" & Service_Charge_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                    Dim Admin_Typecommfrom As String = "Your Account is debited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_bene_Mobile.Text.Trim & " / AMT " & VCus_Amount & "."
                                    Dim Admin_TypecommTo As String = "Your Account is credited by commission " & Admin_Com_Amt & " Rs. Due to " & TypeName & " on number " & txt_bene_Mobile.Text.Trim & " / AMT " & VCus_Amount & "."

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
                                        Qry = Qry & " " & "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_TransferAmountToAgents (TransIpAddress,API_TransId,Actual_Transaction_Amount,Ref_TransID,Actual_Commission_Amt,GSTAmt,Commission_Without_GST,TDS_Amt,Net_Commission_Amt,TransferToMsg,TransferFromMsg,Amount_Type,Remark,TransactionDate,TransferFrom,TransferTo,TransferAmt,RecordDateTime,UpdatedBy,UpdatedOn) values( '" & GV.parseString(GV.GetIPAddress) & "', '" & v_Transfer_Trans_ID & "','" & VCus_Amount & "', '" & GV.parseString(lblPopTransactionId.Text.Trim) & "'," & V_Actual_Commission_Amt & "," & V_GSTAmt & "," & V_Commission_Without_GST & "," & V_TDS_Amt & "," & V_Net_Commission_Amt & ",'" & Admin_TypecommTo & "','" & Admin_Typecommfrom & "','Commission','" & TypeName & "','" & Now.Date.ToString("MM-dd-yyyy") & "','" & "API Partner" & "','Super Admin','" & Admin_Com_Amt & "'," & VRecord_DateTime & ",'" & VUpdatedBy & "'," & VUpdatedOn & " ) ;"
                                    End If

                                End If
                                '//// Super Admin Commission Calculation - End
                            End If
                            '//// Admin & Super Admin Commission Calculation - End
                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

                            If Not Qry.Trim = "" Then
                                GV.FL.DMLQueriesBulk(Qry)
                            End If

                        End If

                        '<----------------Approve Amount start --------------------------------->
                        Dim VDatabaseName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim
                        Dim VCompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
                        Dim VRetailerID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
                        'Dim ApproveAmount As String = AddInVar("ApproveAmount", VDatabaseName & ".dbo.BOS_Dis_SubDis_Retailer_Registration Where RegistrationId in ('" & VRetailerID & "')  and ActiveStatus='Active' ")
                        Dim amount As String = txt_Transc_Amount.Text
                        Dim qrys As String = "Insert into " & VDatabaseName & ".dbo.BOS_MakePayemnts_Details (Amount,RegistrationId,DocumentPath,PaymentMode,RefrenceID, PaymentDate, DepositBankName, BranchCode_ChecqueNo, Remarks, TransactionID, RecordDateTime,UpdatedBy,UpdatedOn,ApporvedStatus,CompanyCode,AccountHolder) values (" & txt_Transc_Amount.Text & ",'" & VRetailerID & "','','" & ddl_Transc_Type.SelectedValue & "','" & v_Transfer_Trans_ID & "',getdate(),'','test','','" & v_Transfer_Trans_ID & "',getdate(),'',getdate(),'Pending','" & VCompanyCode & "','" & txt_bene_AccountNumber.Text & "')"
                        GV.FL.DMLQueries(qrys)

                        ModalPopupExtender3.Show()
                        lblPopDateTime.Text = Date.Now
                        lblPopTransactionId.Text = v_Transfer_Trans_ID
                        lblPopbankAccount.Text = txt_bene_AccountNumber.Text
                        lblpopAmount.Text = txt_Transc_Amount.Text
                        lblpopName.Text = txt_bene_Name.Text
                        'lblPopUTR.Text = 
                        lblPopStatus.Text = "Pending for Admin Aprovel"

                        If status = "Failed" Then

                            lbl_Error1.Text = "Please Check Your Bank Account Details"
                            lbl_Error1.CssClass = "errorlabels"

                        End If
                    End If
                End If
            End If
            'End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btn_bene_clear_Click(sender As Object, e As EventArgs)
        Try

            Clear()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Public Sub Clear()
        Try

            txt_bene_Name.Text = String.Empty
            txt_bene_Mobile.Text = String.Empty
            txt_bene_AccountNumber.Text = String.Empty
            txt_bene_IFSC.Text = String.Empty
            txt_ben_Email.Text = String.Empty
            txt_bene_Address.Text = String.Empty

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Class VerifyIfscCode_API_Parameters
        Dim VbeneficiaryIFSCcode As String

        Public Property beneficiaryIFSCcode()
            Get
                Return VbeneficiaryIFSCcode
            End Get
            Set(value)
                VbeneficiaryIFSCcode = value
            End Set
        End Property
    End Class

    Public Class GenerateTokken_API_Parameters
        Dim VAmount As String
        Dim VRegistrationId As String
        Dim VCompanyCode As String

        Public Property RegistrationId()
            Get
                Return VRegistrationId
            End Get
            Set(value)
                VRegistrationId = value
            End Set
        End Property

        Public Property CompanyCode()
            Get
                Return VCompanyCode
            End Get
            Set(value)
                VCompanyCode = value
            End Set
        End Property

        Public Property amount()
            Get
                Return VAmount
            End Get
            Set(value)
                VAmount = value
            End Set
        End Property

    End Class
    Public Class PaymentStatus_API_Parameters
        Dim Vtrxn_id As String

        Public Property trxn_id()
            Get
                Return Vtrxn_id
            End Get
            Set(value)
                Vtrxn_id = value
            End Set
        End Property
    End Class
    Public Class DomesticPayment_API_Parameters
        Dim Vaddress, Vname, Vamount, Vbankaccountkey, VbeneficiaryAccNo, VbeneficiaryIfscCode, VbeneficiaryName, VemailAddress,
                VmerchantTrxnRefId, VmobileNumber, Votp, VtransferType, VtrxnNote As String

        Public Property address()
            Get
                Return Vaddress
            End Get
            Set(value)
                Vaddress = value
            End Set
        End Property
        Public Property name()
            Get
                Return Vname
            End Get
            Set(value)
                Vname = value
            End Set
        End Property
        Public Property amount()
            Get
                Return Vamount
            End Get
            Set(value)
                Vamount = value
            End Set
        End Property
        Public Property bankaccountkey()
            Get
                Return Vbankaccountkey
            End Get
            Set(value)
                Vbankaccountkey = value
            End Set
        End Property
        Public Property beneficiaryAccNo()
            Get
                Return VbeneficiaryAccNo
            End Get
            Set(value)
                VbeneficiaryAccNo = value
            End Set
        End Property
        Public Property beneficiaryIfscCode()
            Get
                Return VbeneficiaryIfscCode
            End Get
            Set(value)
                VbeneficiaryIfscCode = value
            End Set
        End Property
        Public Property beneficiaryName()
            Get
                Return VbeneficiaryName
            End Get
            Set(value)
                VbeneficiaryName = value
            End Set
        End Property
        Public Property emailAddress()
            Get
                Return VemailAddress
            End Get
            Set(value)
                VemailAddress = value
            End Set
        End Property
        Public Property merchantTrxnRefId()
            Get
                Return VmerchantTrxnRefId
            End Get
            Set(value)
                VmerchantTrxnRefId = value
            End Set
        End Property
        Public Property mobileNumber()
            Get
                Return VmobileNumber
            End Get
            Set(value)
                VmobileNumber = value
            End Set
        End Property
        Public Property otp()
            Get
                Return Votp
            End Get
            Set(value)
                Votp = value
            End Set
        End Property
        Public Property transferType()
            Get
                Return VtransferType
            End Get
            Set(value)
                VtransferType = value
            End Set
        End Property
        Public Property trxnNote()
            Get
                Return VtrxnNote
            End Get
            Set(value)
                VtrxnNote = value
            End Set
        End Property
    End Class

    'UAT link
    'Dim DomesticPayment_Api_Url As String = "https://api.bos.center/api/BOS/DomesticPayment"
    'Dim DomesticPaymentStatus_Api_Url As String = "https://api.bos.center/api/BOS/DomesticPaymentStatus"

    'Prod Link
    Dim DomesticPayment_Api_Url As String = "https://api.boscenter.in/api/BOS/DomesticPayment"
    Dim DomesticPaymentStatus_Api_Url As String = "https://api.boscenter.in/api/BOS/DomesticPaymentStatus"
    Dim VerifyIfscCode_Api_Url As String = "https://api.boscenter.in/api/BOS/verifyIfscCode"

    Dim TrxnId As String = ""
    Public Function GetAPISParametars(ApiMethod As String) As String
        Dim FinalApi_URL As String = ""
        Dim Final_Parameters As String = ""
        Dim FinalAPI_Result As String = ""
        Dim ApiMethods As String = ""
        Dim GenerateToken As String = ""
        Dim merchantTrxnRefId As String = ""
        Dim token As String = ""
        Try
            If ApiMethod = "DomesticPayment_API_Parameters" Then
                Dim Set_generatetokken_APIParameters_obj As New GenerateTokken_API_Parameters
                Set_generatetokken_APIParameters_obj.amount = txt_Transc_Amount.Text
                Dim Set_APIParameters_obj As New DomesticPayment_API_Parameters
                Final_Parameters = Newtonsoft.Json.JsonConvert.SerializeObject(Set_generatetokken_APIParameters_obj)
                FinalAPI_Result = "https://api.boscenter.in/api/BOS/GenerateToken"
                ApiMethods = "POST"
                FinalAPI_Result = ReadByRestClient(FinalAPI_Result, Final_Parameters)
                Dim json_ As String = FinalAPI_Result
                Dim ser_ As JObject = JObject.Parse(json_)
                Dim code As String = ser_.SelectToken("code").ToString.Trim
                Dim description As String = ser_.SelectToken("description").ToString.Trim
                merchantTrxnRefId = ser_.SelectToken("merchantTrxnRefId").ToString.Trim
                token = ser_.SelectToken("token").ToString.Trim
                Dim status As String = ser_.SelectToken("status").ToString.Trim
                If status = "Success" Then
                        Set_APIParameters_obj.address = txt_bene_Address.Text
                        Set_APIParameters_obj.amount = txt_Transc_Amount.Text
                        Set_APIParameters_obj.bankaccountkey = bankaccountkey
                        Set_APIParameters_obj.beneficiaryAccNo = txt_bene_AccountNumber.Text
                        Set_APIParameters_obj.beneficiaryIfscCode = txt_bene_IFSC.Text
                        Set_APIParameters_obj.beneficiaryName = txt_bene_Name.Text
                        Set_APIParameters_obj.emailAddress = txt_ben_Email.Text
                        Set_APIParameters_obj.merchantTrxnRefId = merchantTrxnRefId
                        Set_APIParameters_obj.mobileNumber = txt_bene_Mobile.Text
                        Set_APIParameters_obj.otp = token
                        Set_APIParameters_obj.transferType = ddl_Transc_Type.SelectedValue
                        Set_APIParameters_obj.trxnNote = txt_Transc_Remarks.Text
                        Final_Parameters = Newtonsoft.Json.JsonConvert.SerializeObject(Set_APIParameters_obj)
                        ApiMethods = "POST"
                        FinalApi_URL = DomesticPayment_Api_Url
                    End If
                    If ApiMethods = "POST" Then
                        FinalAPI_Result = ReadByRestClient(FinalApi_URL, Final_Parameters)
                    End If
                ElseIf ApiMethod = "PaymentStatus_API_Parameters" Then
                    Dim PaymentStatus_API_Parameters_obj As New PaymentStatus_API_Parameters
                    PaymentStatus_API_Parameters_obj.trxn_id = TrxnId
                    Final_Parameters = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentStatus_API_Parameters_obj)
                    ApiMethods = "POST"
                    FinalApi_URL = DomesticPaymentStatus_Api_Url
                If ApiMethods = "POST" Then
                    FinalAPI_Result = ReadByRestClient(FinalApi_URL, Final_Parameters)
                End If
            ElseIf ApiMethod = "VerifyIfscCode_API_Parameters" Then
            Dim VerifyIfscCode_API_Parameters_obj As New VerifyIfscCode_API_Parameters
                VerifyIfscCode_API_Parameters_obj.beneficiaryIFSCcode = txt_bene_IFSC.Text.Trim.ToUpper
                Final_Parameters = Newtonsoft.Json.JsonConvert.SerializeObject(VerifyIfscCode_API_Parameters_obj)
                ApiMethods = "POST"
                FinalApi_URL = VerifyIfscCode_Api_Url
                If ApiMethods = "POST" Then
                    FinalAPI_Result = ReadByRestClient(FinalApi_URL, Final_Parameters)
                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return FinalAPI_Result
        End Try
        Return FinalAPI_Result
    End Function

    Public Function ReadByRestClient(Url As String, Parameters As String) As String
        Dim API_Name, Trans_ID, Trans_DateTime, get_Request_String, Request_String, get_Response_String, Response_String, Agent_Memb_Adv_Invst_ID, Agent_Memb_Adv_Invst_Type As String

        API_Name = ""
        Trans_ID = ""
        Trans_DateTime = ""
        Request_String = ""
        Response_String = ""
        Agent_Memb_Adv_Invst_ID = ""
        Agent_Memb_Adv_Invst_Type = ""

        Dim strQry As String = ""
        Dim Str As String = ""
        Dim get_str As String = ""
        Dim LogString As String = ""

        Try

            API_Name = "FidyPayPayout API"
            'Trans_ID = GV.parseString(lblTransactionID.Text)
            Trans_DateTime = Now
            Request_String = GV.parseString(Parameters)
            Response_String = ""
            Agent_Memb_Adv_Invst_ID = ""
            Agent_Memb_Adv_Invst_Type = ""
            ' Dim dd As String
            Dim client As New RestClient(Url)
            Dim request As New RestSharp.RestRequest(RestSharp.Method.POST)
            request.AddHeader("Accept", "application/json")
            request.AddHeader("Content-Type", "application/json")
            request.AddParameter("application/json", Request_String, RestSharp.ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)

            Str = response.Content
            Str = Str.Trim
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
            Return Str
        End Try
        Return Str
    End Function


    Public con As SqlConnection
    Public Function AddInVar(ByVal retrivefield As String, ByVal tablename As String) As String 'done
        Try
            Dim str, variablename As String
            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If
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
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
        Dim s As String = ""
        Return s
    End Function

    Public Function OpenDsWithSelectQuery(ByVal Query As String) As DataSet  'done
        Try

            con = New SqlConnection("Server=103.35.121.85,5022;DataBase=BosCenter_DB;user id=sa;password=Boscenter@123".ToString())
            If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                con.Open()
            End If
            ds = New DataSet
            Dim da As SqlDataAdapter = New SqlDataAdapter(Query, con)
            da.SelectCommand.CommandTimeout = 300000
            da.Fill(ds)
            con.Close()
        Catch ex As Exception
            GV.logerrors(ex.Message + " StackTrace : " + ex.StackTrace)

        End Try
        Return ds
    End Function





End Class


