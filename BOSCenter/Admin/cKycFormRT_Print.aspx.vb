
Public Class cKycFormRT_Print
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Dim vReqID As String
    Dim MemberDS As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Request.QueryString("ReqID") Is Nothing Then
                    If Not Request.QueryString("ReqID").Trim = "" Then
                        vReqID = Request.QueryString("ReqID").Trim.ToUpper

                        Dim BranchCode As String = GV.get_Admin_SessionVariables("BranchCode", Request, Response)
                        Dim qryStr As String = "select *,CONVERT(VARCHAR(11),RequestDate,106) as RequestDate1 from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_RT_Details where  RequestID='" & vReqID.Trim & "' "
                        ds = New DataSet
                        ds = GV.FL.OpenDsWithSelectQuery(qryStr)

                        If Not ds Is Nothing Then
                            If ds.Tables.Count > 0 Then
                                If ds.Tables(0).Rows.Count > 0 Then
                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("RequestID")) Then
                                        If Not ds.Tables(0).Rows(0).Item("RequestID").ToString() = "" Then
                                            lblRequestID.Text = GV.parseString(ds.Tables(0).Rows(0).Item("RequestID").ToString())
                                        Else
                                            lblRequestID.Text = ""
                                        End If
                                    Else
                                        lblRequestID.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("RequestDate1")) Then
                                        If Not ds.Tables(0).Rows(0).Item("RequestDate1").ToString() = "" Then
                                            lblRequestDate.Text = ds.Tables(0).Rows(0).Item("RequestDate1").ToString().ToUpper  'CDate(GV.parseString(ds.Tables(0).Rows(0).Item("RequestDate1").ToString())).ToString("dd/MM/yyyy")
                                        Else
                                            lblRequestDate.Text = ""
                                        End If
                                    Else
                                        lblRequestDate.Text = ""
                                    End If


                                    'If Not IsDBNull(ds.Tables(0).Rows(0).Item("ApporvedStatus")) Then
                                    '    If Not ds.Tables(0).Rows(0).Item("ApporvedStatus").ToString() = "" Then
                                    '        ddlApprovedStatus.SelectedValue = GV.parseString(ds.Tables(0).Rows(0).Item("ApporvedStatus").ToString())
                                    '    Else
                                    '        ddlApprovedStatus.SelectedIndex = 0
                                    '    End If
                                    'Else
                                    '    ddlApprovedStatus.SelectedIndex = 0
                                    'End If
                                    'If Not IsDBNull(ds.Tables(0).Rows(0).Item("ApporveRemakrs")) Then
                                    '    If Not ds.Tables(0).Rows(0).Item("ApporveRemakrs").ToString() = "" Then
                                    '        txtAdminRemarks.Text = GV.parseString(ds.Tables(0).Rows(0).Item("ApporveRemakrs").ToString())
                                    '    Else
                                    '        txtAdminRemarks.Text = ""
                                    '    End If
                                    'Else
                                    '    txtAdminRemarks.Text = ""
                                    'End If



                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Salutation")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Salutation").ToString() = "" Then
                                            lblName.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Salutation").ToString()).Trim.ToUpper
                                        Else
                                            lblName.Text = ""
                                        End If
                                    Else
                                        lblName.Text = ""
                                    End If


                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FirstName")) Then
                                        If Not ds.Tables(0).Rows(0).Item("FirstName").ToString() = "" Then
                                            lblName.Text = lblName.Text & " " & GV.parseString(ds.Tables(0).Rows(0).Item("FirstName").ToString()).Trim.ToUpper
                                        Else

                                        End If
                                    Else

                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("MiddleName")) Then
                                        If Not ds.Tables(0).Rows(0).Item("MiddleName").ToString() = "" Then
                                            lblName.Text = lblName.Text & " " & GV.parseString(ds.Tables(0).Rows(0).Item("MiddleName").ToString()).ToUpper
                                        Else
                                        End If
                                    Else

                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("LastName")) Then
                                        If Not ds.Tables(0).Rows(0).Item("LastName").ToString() = "" Then
                                            lblName.Text = lblName.Text & " " & GV.parseString(ds.Tables(0).Rows(0).Item("LastName").ToString()).ToUpper
                                        Else

                                        End If
                                    Else

                                    End If





                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address")) Then
                                        If Not ds.Tables(0).Rows(0).Item("Address").ToString() = "" Then
                                            lblAddress.Text = GV.parseString(ds.Tables(0).Rows(0).Item("Address").ToString()).ToUpper
                                        Else
                                            lblAddress.Text = ""
                                        End If
                                    Else
                                        lblAddress.Text = ""
                                    End If


                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("PhoneNo")) Then
                                        If Not ds.Tables(0).Rows(0).Item("PhoneNo").ToString() = "" Then
                                            lblPhoneNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("PhoneNo").ToString()).ToUpper
                                        Else
                                            lblPhoneNo.Text = ""
                                        End If
                                    Else
                                        lblPhoneNo.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("AadharNumber")) Then
                                        If Not ds.Tables(0).Rows(0).Item("AadharNumber").ToString() = "" Then
                                            lblAadharNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("AadharNumber").ToString()).ToUpper
                                        Else
                                            lblAadharNo.Text = ""
                                        End If
                                    Else
                                        lblAadharNo.Text = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ReferenceNumber")) Then
                                        If Not ds.Tables(0).Rows(0).Item("ReferenceNumber").ToString() = "" Then
                                            lblReferenceNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("ReferenceNumber").ToString()).ToUpper
                                        Else
                                            lblReferenceNo.Text = ""
                                        End If
                                    Else
                                        lblReferenceNo.Text = ""
                                    End If


                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("CardNumber")) Then
                                        If Not ds.Tables(0).Rows(0).Item("CardNumber").ToString() = "" Then
                                            lblCardNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("CardNumber").ToString()).ToUpper
                                        Else
                                            lblCardNo.Text = ""
                                        End If
                                    Else
                                        lblCardNo.Text = ""
                                    End If




                                    Dim webfpdata, WebImgFpData1, WebImgFpData2, WebImgFpData3 As String
                                    'rahul
                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("WebFpData")) Then

                                        If Not ds.Tables(0).Rows(0).Item("WebFpData").ToString() = "" Then

                                            webfpdata = ds.Tables(0).Rows(0).Item("WebFpData").ToString()
                                        Else
                                            webfpdata = ""
                                        End If
                                    Else
                                        webfpdata = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("WebImgFpData1")) Then

                                        If Not ds.Tables(0).Rows(0).Item("WebImgFpData1").ToString() = "" Then
                                            WebImgFpData1 = ds.Tables(0).Rows(0).Item("WebImgFpData1").ToString()
                                        Else
                                            WebImgFpData1 = "~/images/uploadimage.png"
                                        End If
                                    Else
                                        WebImgFpData1 = "~/images/uploadimage.png"
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("WebImgFpData2")) Then

                                        If Not ds.Tables(0).Rows(0).Item("WebImgFpData2").ToString() = "" Then
                                            WebImgFpData2 = ds.Tables(0).Rows(0).Item("WebImgFpData2").ToString()
                                        Else
                                            WebImgFpData2 = ""
                                        End If
                                    Else
                                        WebImgFpData2 = ""
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("WebImgFpData3")) Then

                                        If Not ds.Tables(0).Rows(0).Item("WebImgFpData3").ToString() = "" Then
                                            WebImgFpData3 = ds.Tables(0).Rows(0).Item("WebImgFpData3").ToString()
                                        Else
                                            WebImgFpData3 = ""
                                        End If
                                    Else
                                        WebImgFpData3 = ""
                                    End If

                                    'If WebImgFpData1 = "~/images/uploadimage.png" Then
                                    '    txtfpdata.Text = ""
                                    'Else
                                    '    txtfpdata.Text = webfpdata & "~" & WebImgFpData1 & WebImgFpData2 & WebImgFpData3
                                    'End If

                                    imgFinger.ImageUrl = WebImgFpData1 & WebImgFpData2 & WebImgFpData3

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("UploadPhoto")) Then
                                        If Not ds.Tables(0).Rows(0).Item("UploadPhoto").ToString() = "" Then
                                            Image_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("UploadPhoto").ToString()
                                        Else
                                            Image_Photo.ImageUrl = "~/images/uploadimage.png"
                                        End If
                                    Else
                                        Image_Photo.ImageUrl = "~/images/uploadimage.png"
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("UploadAddharCard_Front")) Then
                                        If Not ds.Tables(0).Rows(0).Item("UploadAddharCard_Front").ToString() = "" Then
                                            Image_AddharCardFront.ImageUrl = ds.Tables(0).Rows(0).Item("UploadAddharCard_Front").ToString()
                                        Else
                                            Image_AddharCardFront.ImageUrl = "~/images/uploadimage.png"
                                        End If
                                    Else
                                        Image_AddharCardFront.ImageUrl = "~/images/uploadimage.png"
                                    End If

                                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("UploadAddharCard_Back")) Then
                                        If Not ds.Tables(0).Rows(0).Item("UploadAddharCard_Back").ToString() = "" Then
                                            Image_AddharCardBack.ImageUrl = ds.Tables(0).Rows(0).Item("UploadAddharCard_Back").ToString()
                                        Else
                                            Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"
                                        End If
                                    Else
                                        Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"
                                    End If


                                End If
                            End If
                        End If

                    Else
                        '/// Not Available
                    End If
                Else
                    '/// Not Available
                End If
            End If




            'ds = New DataSet

            'ds = GV.FL.OpenDsWithSelectQuery(qryStr)


            'ListView1.DataSource = ds
            'ListView1.DataBind()

            'If ds.Tables(0).Rows.Count > 0 Then
            '    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

            '        Dim lblSRNo As Label = DirectCast(ListView1.Items(i).FindControl("lblSRNo"), Label)
            '        Dim lblTaskName As Label = DirectCast(ListView1.Items(i).FindControl("lblTaskName"), Label)
            '        Dim lbl_Amount As Label = DirectCast(ListView1.Items(i).FindControl("lbl_Amount"), Label)
            '        Dim lblTaskDescription As Label = DirectCast(ListView1.Items(i).FindControl("lblTaskDescription"), Label)
            '        If Not lblTaskDescription.Text = "" Then
            '            lblTaskDescription.Text = "(" & lblTaskDescription.Text & ")"
            '        End If

            '        lblSRNo.Text = i + 1

            '    Next
            'End If

            'If Not ds Is Nothing Then
            '    If ds.Tables.Count > 0 Then
            '        If ds.Tables(0).Rows.Count > 0 Then

            '            lblVoucherName.Text = Session("TokenHead").ToString().ToUpper

            '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("TokenDate1")) Then
            '                If Not ds.Tables(0).Rows(0).Item("TokenDate1").ToString() = "" Then
            '                    lblTokenDate.Text = ds.Tables(0).Rows(0).Item("TokenDate1").ToString()
            '                    'lblTokenDate.Text = CDate(VOUcherDate).ToString("dd/MM/yyyy")
            '                Else
            '                    lblTokenDate.Text = ""
            '                End If
            '            Else
            '                lblTokenDate.Text = ""
            '            End If

            '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("TokenID")) Then
            '                If Not ds.Tables(0).Rows(0).Item("TokenID").ToString() = "" Then
            '                    lblTokenNo.Text = GV.parseString(ds.Tables(0).Rows(0).Item("TokenID").ToString().ToUpper())

            '                Else
            '                    lblTokenNo.Text = ""
            '                End If
            '            Else
            '                lblTokenNo.Text = ""
            '            End If

            '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("BranchCode")) Then
            '                If Not ds.Tables(0).Rows(0).Item("BranchCode").ToString() = "" Then
            '                    lblBranchCode.Text = GV.parseString(ds.Tables(0).Rows(0).Item("BranchCode").ToString().ToUpper())

            '                Else
            '                    lblBranchCode.Text = ""
            '                End If
            '            Else
            '                lblBranchCode.Text = ""
            '            End If
            '            If Not IsDBNull(ds.Tables(0).Rows(0).Item("EmpCode")) Then
            '                If Not ds.Tables(0).Rows(0).Item("EmpCode").ToString() = "" Then
            '                    lblGeneratedBy.Text = GV.parseString(ds.Tables(0).Rows(0).Item("EmpCode").ToString().ToUpper())
            '                Else
            '                    lblGeneratedBy.Text = ""
            '                End If
            '            Else
            '                lblGeneratedBy.Text = ""
            '            End If

            '        End If

            '    End If

            'End If


            'lblTotalAmount.Text = GV.FL.AddInVar("Cast(round(isnull( sum(TaskAmount),0),0) as numeric(36,2))", "NidhiSoftware_Admin_Token_VS_TaskMaster where  TokenID='" & Session("TokenNo").ToString & "' and BranchCode='" & BranchCode & "' and EmpCode='" & GV.get_Admin_SessionVariables("LoginID", Request, Response) & "' and CompanyCode='" & GV.get_Admin_SessionVariables("CompanyCode", Request, Response) & "'")

            'If Not lblTotalAmount.Text = "0.00" Then
            '    lbl_AmountInWord.Text = StrConv(GV.FL.ConvertAmountInWordFormat(Math.Round(CDec(lblTotalAmount.Text.Trim), 0)), VbStrConv.Uppercase)
            'ElseIf lblTotalAmount.Text = "" Then
            '    lbl_AmountInWord.Text = ""
            'Else
            '    lbl_AmountInWord.Text = "RUPEES ZERO ONLY/-"
            'End If

            'MemberDS = New DataSet
            'Dim MemQry As String = "select * from NidhiSoftware_Admin_BranchMaster where Code='" & GV.parseString(lblBranchCode.Text.Trim) & "' and  CompanyCode='" & GV.get_Admin_SessionVariables("CompanyCode", Request, Response) & "';"
            'MemQry = MemQry & "select * from NidhiSoftware_CLientRegistration where CompanyCode='" & GV.get_Admin_SessionVariables("CompanyCode", Request, Response) & "'"
            'MemberDS = GV.FL.OpenDsWithSelectQuery(MemQry)

            'If Not MemberDS Is Nothing Then
            '    If MemberDS.Tables.Count > 0 Then
            '        If MemberDS.Tables(0).Rows.Count > 0 Then



            '            If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("Company")) Then
            '                lbl_c_name.Text = GV.parseString(MemberDS.Tables(0).Rows(0).Item("Company")).ToUpper
            '            Else
            '                lbl_c_name.Text = ""
            '            End If

            '            If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("BranchName")) Then
            '                lblBranchName.Text = "(" & GV.parseString(MemberDS.Tables(0).Rows(0).Item("BranchName")).ToUpper & ")"
            '            Else
            '                lblBranchName.Text = ""
            '            End If

            '            If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("Address_1")) Then

            '                Dim address As String = ""

            '                If Not GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_1")) = "" Then
            '                    address = GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_1"))
            '                End If
            '                If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("Address_2")) Then
            '                    If Not GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_2")) = "" Then
            '                        If address = "" Then
            '                            address = GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_2"))
            '                        Else
            '                            address = address & ",<br/>" & GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_2"))
            '                        End If
            '                    End If
            '                End If
            '                If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("Address_3")) Then
            '                    If Not GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_3")) = "" Then
            '                        If address = "" Then
            '                            address = GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_3"))
            '                        Else
            '                            address = address & ",<br/>" & GV.parseString(MemberDS.Tables(0).Rows(0).Item("Address_3"))
            '                        End If
            '                    End If
            '                End If
            '                If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("State")) Then
            '                    If Not GV.parseString(MemberDS.Tables(0).Rows(0).Item("State")) = "" Then
            '                        If address = "" Then
            '                            address = GV.parseString(MemberDS.Tables(0).Rows(0).Item("State"))
            '                        Else
            '                            address = address & ", " & GV.parseString(MemberDS.Tables(0).Rows(0).Item("State"))
            '                        End If
            '                    End If
            '                End If
            '                If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("District")) Then
            '                    If Not GV.parseString(MemberDS.Tables(0).Rows(0).Item("District")) = "" Then
            '                        If address = "" Then
            '                            address = GV.parseString(MemberDS.Tables(0).Rows(0).Item("District"))
            '                        Else
            '                            address = address & ", " & GV.parseString(MemberDS.Tables(0).Rows(0).Item("District"))
            '                        End If
            '                    End If
            '                End If

            '                If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("PinCode")) Then
            '                    If Not GV.parseString(MemberDS.Tables(0).Rows(0).Item("PinCode")) = "" Then
            '                        If address = "" Then
            '                            address = GV.parseString(MemberDS.Tables(0).Rows(0).Item("PinCode"))
            '                        Else
            '                            address = address & ", " & GV.parseString(MemberDS.Tables(0).Rows(0).Item("PinCode"))
            '                        End If
            '                    End If
            '                End If


            '                lbl_c_add_1.Text = StrConv(address, VbStrConv.Uppercase)
            '            Else
            '                lbl_c_add_1.Text = ""
            '            End If


            '            If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("MobileNo")) Then
            '                lbl_c_mobile.Text = GV.parseString(MemberDS.Tables(0).Rows(0).Item("MobileNo"))
            '            Else
            '                lbl_c_mobile.Text = ""
            '            End If
            '            lbl_c_email.Text = ""
            '            If Not IsDBNull(MemberDS.Tables(0).Rows(0).Item("EmailID")) Then
            '                lbl_c_email.Text = StrConv(GV.parseString(MemberDS.Tables(0).Rows(0).Item("EmailID")), VbStrConv.Uppercase)
            '            Else
            '                lbl_c_email.Text = ""
            '            End If

            '            If MemberDS.Tables(1).Rows.Count > 0 Then

            '                If Not IsDBNull(MemberDS.Tables(1).Rows(0).Item("Companylogo")) Then
            '                    Image1.ImageUrl = GV.parseString(MemberDS.Tables(1).Rows(0).Item("Companylogo"))
            '                Else
            '                    Image1.ImageUrl = ""
            '                End If
            '            End If
            '        End If
            '    End If
            'End If


        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class