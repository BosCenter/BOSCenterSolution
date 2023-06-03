Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D


Public Class BOS_AgenentProfile
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim VRegisterDate, VAgentType, VAgentName, VFirstName, VEmailID, VDOB, VAlternateMobileNo, VPermanentAddress, VState, VAddharCardNo, VWebSite, VRegistrationId, VPanCardNumber, VMobileNo, VOfficeAddress, VCity, VLastName, VPincode, VBusinessType, VGSTNO As String
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtAgentName.CssClass = "form-control"
            txtPanCardNumber.CssClass = "form-control"
            txtFirstName.CssClass = "form-control"
            txtLastName.CssClass = "form-control"
            txtDOB.CssClass = "form-control"
            txtEmailID.CssClass = "form-control"
            ddlBussinessType.CssClass = "form-control"
            ddlAgentType.CssClass = "form-control"
            txtOfficeAddress.CssClass = "form-control"
            txtAddharCardNo.CssClass = "form-control"
            txtPermanentAddress.CssClass = "form-control"
            txtGSTNO.CssClass = "form-control"
            txtGSTNO.CssClass = "form-control"
            txtWebSite.CssClass = "form-control"
            ddlState.CssClass = "form-control"
            txtCity.CssClass = "form-control"
            ddlAccountType.CssClass = "form-control"
            txtMobileNo.CssClass = "form-control"
            txtAlternateMobileNo.CssClass = "form-control"
            txtPincode.CssClass = "form-control"
            txtBankBranch.CssClass = "form-control"
            txtBankName.CssClass = "form-control"
            txtAccountNumber.CssClass = "form-control"
            If Not IsPostBack Then
                lblError.Text = ""
                lblError.CssClass = ""
                GV.addBussinessType(ddlBussinessType)
                ddlState.Items.Clear()
                GV.FL.AddInDropDownListDistinct(ddlState, "State_Name", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.CRM_StateMaster where Country_Name='INDIA'")
                If ddlState.Items.Count > 0 Then
                    ddlState.Items.Insert(0, ":::: Select State ::::")
                Else
                    ddlState.Items.Add(":::: Select State ::::")
                End If
              
                'Dim group As String = GV.get_Distributor_SessionVariables("Group", Request, Response)
                'If group = "" Then
                '    group = GV.get_SubDistributor_SessionVariables("Group", Request, Response)
                '    If group = "" Then
                '        group = GV.get_Retailer_SessionVariables("Group", Request, Response)
                '    End If
                '    If group = "" Then
                '        group = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
                '    End If

                ' End If
                Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

                
                Dim x As String = ""
                x = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
           

                btnClear.Text = "Close"
                formheading3.Text = "Profileo Info"
                lblformsectionhead3.Text = "My Profile Details"
                DS = GV.FL.OpenDs("" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & x & "'")
                If Not DS Is Nothing Then
                    If DS.Tables.Count > 0 Then
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("AgentType")) Then
                            If Not DS.Tables(0).Rows(0).Item("AgentType").ToString() = "" Then
                                ddlAgentType.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("AgentType").ToString())
                            Else
                                ddlAgentType.SelectedIndex = 0
                            End If
                        Else
                            ddlAgentType.SelectedIndex = 0
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("AgencyName")) Then
                            If Not DS.Tables(0).Rows(0).Item("AgencyName").ToString() = "" Then
                                txtAgentName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AgencyName").ToString())
                            Else
                                txtAgentName.Text = ""
                            End If
                        Else
                            txtAgentName.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("FirstName")) Then
                            If Not DS.Tables(0).Rows(0).Item("FirstName").ToString() = "" Then
                                txtFirstName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("FirstName").ToString())
                            Else
                                txtFirstName.Text = ""
                            End If
                        Else
                            txtFirstName.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("EmailID")) Then
                            If Not DS.Tables(0).Rows(0).Item("EmailID").ToString() = "" Then
                                txtEmailID.Text = GV.parseString(DS.Tables(0).Rows(0).Item("EmailID").ToString())
                            Else
                                txtEmailID.Text = ""
                            End If
                        Else
                            txtEmailID.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("DOB")) Then
                            If Not DS.Tables(0).Rows(0).Item("DOB").ToString() = "" Then
                                txtDOB.Text = GV.parseString(DS.Tables(0).Rows(0).Item("DOB").ToString())
                            Else
                                txtDOB.Text = ""
                            End If
                        Else
                            txtDOB.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("AlternateMobileNo")) Then
                            If Not DS.Tables(0).Rows(0).Item("AlternateMobileNo").ToString() = "" Then
                                txtAlternateMobileNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AlternateMobileNo").ToString())
                            Else
                                txtAlternateMobileNo.Text = ""
                            End If
                        Else
                            txtAlternateMobileNo.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("PermanentAddress")) Then
                            If Not DS.Tables(0).Rows(0).Item("PermanentAddress").ToString() = "" Then
                                txtPermanentAddress.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PermanentAddress").ToString())
                            Else
                                txtPermanentAddress.Text = ""
                            End If
                        Else
                            txtPermanentAddress.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("State")) Then
                            If Not DS.Tables(0).Rows(0).Item("State").ToString() = "" Then
                                ddlState.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("State").ToString())
                            Else
                                ddlState.SelectedIndex = 0
                            End If
                        Else
                            ddlState.SelectedIndex = 0
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("AddharCardNo")) Then
                            If Not DS.Tables(0).Rows(0).Item("AddharCardNo").ToString() = "" Then
                                txtAddharCardNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AddharCardNo").ToString())
                            Else
                                txtAddharCardNo.Text = ""
                            End If
                        Else
                            txtAddharCardNo.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebSite")) Then
                            If Not DS.Tables(0).Rows(0).Item("WebSite").ToString() = "" Then
                                txtWebSite.Text = GV.parseString(DS.Tables(0).Rows(0).Item("WebSite").ToString())
                            Else
                                txtWebSite.Text = ""
                            End If
                        Else
                            txtWebSite.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("RegistrationId")) Then
                            If Not DS.Tables(0).Rows(0).Item("RegistrationId").ToString() = "" Then
                                txtRegistrationId.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RegistrationId").ToString())
                            Else
                                txtRegistrationId.Text = ""
                            End If
                        Else
                            txtRegistrationId.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("PanCardNumber")) Then
                            If Not DS.Tables(0).Rows(0).Item("PanCardNumber").ToString() = "" Then
                                txtPanCardNumber.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PanCardNumber").ToString())
                            Else
                                txtPanCardNumber.Text = ""
                            End If
                        Else
                            txtPanCardNumber.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("MobileNo")) Then
                            If Not DS.Tables(0).Rows(0).Item("MobileNo").ToString() = "" Then
                                txtMobileNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MobileNo").ToString())
                            Else
                                txtMobileNo.Text = ""
                            End If
                        Else
                            txtMobileNo.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("OfficeAddress")) Then
                            If Not DS.Tables(0).Rows(0).Item("OfficeAddress").ToString() = "" Then
                                txtOfficeAddress.Text = GV.parseString(DS.Tables(0).Rows(0).Item("OfficeAddress").ToString())
                            Else
                                txtOfficeAddress.Text = ""
                            End If
                        Else
                            txtOfficeAddress.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("City")) Then
                            If Not DS.Tables(0).Rows(0).Item("City").ToString() = "" Then
                                txtCity.Text = GV.parseString(DS.Tables(0).Rows(0).Item("City").ToString())
                            Else
                                txtCity.Text = ""
                            End If
                        Else
                            txtCity.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("LastName")) Then
                            If Not DS.Tables(0).Rows(0).Item("LastName").ToString() = "" Then
                                txtLastName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("LastName").ToString())
                            Else
                                txtLastName.Text = ""
                            End If
                        Else
                            txtLastName.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Pincode")) Then
                            If Not DS.Tables(0).Rows(0).Item("Pincode").ToString() = "" Then
                                txtPincode.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Pincode").ToString())
                            Else
                                txtPincode.Text = ""
                            End If
                        Else
                            txtPincode.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("BusinessType")) Then
                            If Not DS.Tables(0).Rows(0).Item("BusinessType").ToString() = "" Then
                                ddlBussinessType.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("BusinessType").ToString())
                            Else
                                ddlBussinessType.SelectedIndex = 0
                            End If
                        Else
                            ddlBussinessType.SelectedIndex = 0
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("GSTNO")) Then
                            If Not DS.Tables(0).Rows(0).Item("GSTNO").ToString() = "" Then
                                txtGSTNO.Text = GV.parseString(DS.Tables(0).Rows(0).Item("GSTNO").ToString())
                            Else
                                txtGSTNO.Text = ""
                            End If
                        Else
                            txtGSTNO.Text = ""
                        End If
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("RefrenceID")) Then
                            If Not DS.Tables(0).Rows(0).Item("RefrenceID").ToString() = "" Then
                                txtRefrenceID.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RefrenceID").ToString())
                            Else
                                txtRefrenceID.Text = ""
                            End If
                        Else
                            txtRefrenceID.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("RefrenceType")) Then
                            If Not DS.Tables(0).Rows(0).Item("RefrenceType").ToString() = "" Then
                                txtRefrenceType.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RefrenceType").ToString())
                            Else
                                txtRefrenceType.Text = ""
                            End If
                        Else
                            txtRefrenceType.Text = ""
                        End If

                        '///////////////======= Start set Uploaded Image url ===============================================

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("UploadPanCard")) Then

                            If Not DS.Tables(0).Rows(0).Item("UploadPanCard").ToString() = "" Then
                                filePath = DS.Tables(0).Rows(0).Item("UploadPanCard").ToString()
                                filename = Path.GetFileName(filePath)
                                ext = Path.GetExtension(filename)
                                If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                                    btnUpload_PanCard.ToolTip = GV.parseString(DS.Tables(0).Rows(0).Item("UploadPanCard").ToString())
                                    Image_PanCard.ImageUrl = btnUpload_PanCard.ToolTip
                                End If
                                btnUpload_PanCard.Text = "Download"
                                btnDeleteUpload_PanCard.Enabled = False
                            Else
                                btnUpload_PanCard.ToolTip = ""
                                Image_PanCard.ImageUrl = "~/images/uploadimage.png"
                            End If
                        Else
                            btnUpload_PanCard.ToolTip = ""
                            Image_PanCard.ImageUrl = "~/images/uploadimage.png"
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("UploadAddharCard_Front")) Then

                            If Not DS.Tables(0).Rows(0).Item("UploadAddharCard_Front").ToString() = "" Then
                                filePath = DS.Tables(0).Rows(0).Item("UploadAddharCard_Front").ToString()
                                filename = Path.GetFileName(filePath)
                                ext = Path.GetExtension(filename)
                                If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                                    btnUpload_AddharCardFront.ToolTip = GV.parseString(DS.Tables(0).Rows(0).Item("UploadAddharCard_Front").ToString())
                                    Image_AddharCardFront.ImageUrl = btnUpload_AddharCardFront.ToolTip
                                End If
                                btnUpload_AddharCardFront.Text = "Download"
                                btnDeleteUpload_AddharCardFront.Enabled = False
                            Else
                                btnUpload_AddharCardFront.ToolTip = ""
                                Image_AddharCardFront.ImageUrl = "~/images/uploadimage.png"
                            End If
                        Else
                            btnUpload_AddharCardFront.ToolTip = ""
                            Image_AddharCardFront.ImageUrl = "~/images/uploadimage.png"
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("UploadAddharCard_Back")) Then

                            If Not DS.Tables(0).Rows(0).Item("UploadAddharCard_Back").ToString() = "" Then
                                filePath = DS.Tables(0).Rows(0).Item("UploadAddharCard_Back").ToString()
                                filename = Path.GetFileName(filePath)
                                ext = Path.GetExtension(filename)
                                If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                                    btnUpload_AddharCardBack.ToolTip = GV.parseString(DS.Tables(0).Rows(0).Item("UploadAddharCard_Back").ToString())
                                    Image_AddharCardBack.ImageUrl = btnUpload_AddharCardBack.ToolTip
                                End If
                                btnUpload_AddharCardBack.Text = "Download"
                                btnDeleteUpload_AddharCardBack.Enabled = False
                            Else
                                btnUpload_AddharCardBack.ToolTip = ""
                                Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"
                            End If
                        Else
                            btnUpload_AddharCardBack.ToolTip = ""
                            Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("UploadOtherProof")) Then

                            If Not DS.Tables(0).Rows(0).Item("UploadOtherProof").ToString() = "" Then
                                filePath = DS.Tables(0).Rows(0).Item("UploadOtherProof").ToString()
                                filename = Path.GetFileName(filePath)
                                ext = Path.GetExtension(filename)
                                If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                                    btnUpload_OtherDocuments.ToolTip = GV.parseString(DS.Tables(0).Rows(0).Item("UploadOtherProof").ToString())
                                    Image_OtherDocuments.ImageUrl = btnUpload_OtherDocuments.ToolTip
                                End If
                                btnUpload_OtherDocuments.Text = "Download"
                                btnDeleteUpload_OtherDocuments.Enabled = False
                            Else
                                btnUpload_OtherDocuments.ToolTip = ""
                                Image_OtherDocuments.ImageUrl = "~/images/uploadimage.png"
                            End If
                        Else
                            btnUpload_OtherDocuments.ToolTip = ""
                            Image_OtherDocuments.ImageUrl = "~/images/uploadimage.png"
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("UploadPhoto")) Then

                            If Not DS.Tables(0).Rows(0).Item("UploadPhoto").ToString() = "" Then
                                filePath = DS.Tables(0).Rows(0).Item("UploadPhoto").ToString()
                                filename = Path.GetFileName(filePath)
                                ext = Path.GetExtension(filename)
                                If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                                    btnUpload_Photo.ToolTip = GV.parseString(DS.Tables(0).Rows(0).Item("UploadPhoto").ToString())
                                    Image_Photo.ImageUrl = btnUpload_Photo.ToolTip
                                End If
                                btnUpload_Photo.Text = "Download"
                                btnDeleteUpload_Photo.Enabled = False
                            Else
                                btnUpload_Photo.ToolTip = ""
                                Image_Photo.ImageUrl = "~/images/uploadimage.png"
                            End If
                        Else
                            btnUpload_Photo.ToolTip = ""
                            Image_Photo.ImageUrl = "~/images/uploadimage.png"
                        End If

                        '///////////////======= End set Uploaded Image url ===============================================

                        GridView2.DataSource = Nothing
                        GridView2.DataBind()

                        Dim aa As String = "Select RId as SrNo,BankName,BranchName,AccountNo,AccountType,IFSCCode From " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_BankDetails where RegistrationId='" & GV.parseString(txtRegistrationId.Text.Trim) & "' ; "
                        GV.FL.AddInGridViewWithFieldName(GridView2, aa)
                        If GridView2.Rows.Count > 0 Then
                            Div_Grid.Visible = True
                            GV.FL.showSerialnoOnGridView(GridView2, 0)
                        Else
                            Div_Grid.Visible = False
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Response.Redirect("SuperAdminHome.aspx")
        Catch ex As Exception
        End Try
    End Sub

    
    Dim VRefrenceID, VRefrenceType As String
    


    Protected Sub ddlAgentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgentType.SelectedIndexChanged
        Try
            '" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.
            If ddlAgentType.SelectedValue.Trim.ToUpper = "Master Distributor".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("Distributor_Prefix", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("DistributorID", GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Distributor".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("SubDistributor_Prefix", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("SubDistributorID", GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
            ElseIf ddlAgentType.SelectedValue.Trim.ToUpper = "Retailer".Trim.ToUpper Then
                txtRegistrationId.Text = GV.FL.AddInVar("Retailer_Prefix", "" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.AutoNumber") & GV.get_AutoNumber("RetailerId", GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_PanCard.Click
        Try
            Try
                lblErrorImage_PanCard.Text = ""
                lblErrorImage_PanCard.CssClass = ""
                If (btnUpload_PanCard.Text = "Download") Then
                    DownloadDoc(btnUpload_PanCard.ToolTip)
                Else

                    If FileUpload_PanCard.HasFile = True Then

                        filePath = FileUpload_PanCard.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                            SaveImage(FileUpload_PanCard, btnUpload_PanCard, btnDeleteUpload_PanCard, "PanCard")
                            Dim fi As New FileInfo(btnUpload_PanCard.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_PanCard.ImageUrl = btnUpload_PanCard.ToolTip.ToString()
                                btnUpload_PanCard.Focus()

                            End If
                        Else
                            lblInformation.Text = "Invalid Image Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_PanCard.Focus()
                        End If

                    Else
                        lblErrorImage_PanCard.Text = "Please Select Image To Upload."
                        lblErrorImage_PanCard.CssClass = "errorlabels"
                        btnUpload_PanCard.Focus()
                    End If

                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_AgeProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_AddharCardBack.Click
        Try
            Try
                lblErrorImage_AddharCardBack.Text = ""
                lblErrorImage_AddharCardBack.CssClass = ""
                If (btnUpload_AddharCardBack.Text = "Download") Then
                    DownloadDoc(btnUpload_AddharCardBack.ToolTip)
                Else
                    If FileUpload_AddharCardBack.HasFile = True Then


                        filePath = FileUpload_AddharCardBack.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                            SaveImage(FileUpload_AddharCardBack, btnUpload_AddharCardBack, btnDeleteUpload_AddharCardBack, "AddharCardBack")
                            Dim fi As New FileInfo(btnUpload_AddharCardBack.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_AddharCardBack.ImageUrl = btnUpload_AddharCardBack.ToolTip.ToString()
                                btnUpload_AddharCardBack.Focus()
                            End If
                        Else
                            lblInformation.Text = "Invalid Document Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_AddharCardBack.Focus()
                        End If

                    Else
                        lblErrorImage_AddharCardBack.Text = "Please Select Document To Upload."
                        lblErrorImage_AddharCardBack.CssClass = "errorlabels"
                        btnUpload_AddharCardBack.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_AddressProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_AddharCardFront.Click
        Try
            Try
                lblErrorImage_AddharCardFront.Text = ""
                lblErrorImage_AddharCardFront.CssClass = ""
                If (btnUpload_AddharCardFront.Text = "Download") Then
                    DownloadDoc(btnUpload_AddharCardFront.ToolTip)
                Else
                    If FileUpload_AddharCardFront.HasFile = True Then

                        filePath = FileUpload_AddharCardFront.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                            SaveImage(FileUpload_AddharCardFront, btnUpload_AddharCardFront, btnDeleteUpload_AddharCardFront, "AddharCardFront")
                            Dim fi As New FileInfo(btnUpload_AddharCardFront.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_AddharCardFront.ImageUrl = btnUpload_AddharCardFront.ToolTip.ToString()
                                btnUpload_AddharCardFront.Focus()

                            End If
                        Else
                            lblInformation.Text = "Invalid Image Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_AddharCardFront.Focus()
                        End If


                    Else
                        lblErrorImage_AddharCardFront.Text = "Please Select Image To Upload."
                        lblErrorImage_AddharCardFront.CssClass = "errorlabels"
                        btnUpload_AddharCardFront.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_Photo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_Photo.Click
        Try
            Try
                lblErrorImage_Photo.Text = ""
                lblErrorImage_Photo.CssClass = ""
                If (btnUpload_Photo.Text = "Download") Then
                    DownloadDoc(btnUpload_Photo.ToolTip)
                Else
                    If FileUpload_Photo.HasFile = True Then

                        filePath = FileUpload_Photo.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                            SaveImage(FileUpload_Photo, btnUpload_Photo, btnDeleteUpload_Photo, "Photo")
                            Dim fi As New FileInfo(btnUpload_Photo.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_Photo.ImageUrl = btnUpload_Photo.ToolTip.ToString()
                                btnUpload_Photo.Focus()
                            Else
                                Image_Photo.ImageUrl = "~/images/documents.png"
                                btnUpload_Photo.Focus()
                            End If
                        Else
                            lblInformation.Text = "Invalid Document Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_Photo.Focus()
                        End If

                    Else
                        lblErrorImage_Photo.Text = "No file Selected for photo"
                        lblErrorImage_Photo.CssClass = "errorlabels"
                        btnUpload_Photo.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_Signature_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload_OtherDocuments.Click
        Try
            Try

                lblErrorImage_OtherDocuments.Text = ""
                lblErrorImage_OtherDocuments.CssClass = ""

                If (btnUpload_OtherDocuments.Text = "Download") Then
                    DownloadDoc(btnUpload_OtherDocuments.ToolTip)
                Else
                    If FileUpload_OtherDocuments.HasFile = True Then

                        filePath = FileUpload_OtherDocuments.PostedFile.FileName
                        filename = Path.GetFileName(filePath)
                        ext = Path.GetExtension(filename)

                        If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then

                            SaveImage(FileUpload_OtherDocuments, btnUpload_OtherDocuments, btnDeleteUpload_OtherDocuments, "OtherDocuments")
                            Dim fi As New FileInfo(btnUpload_OtherDocuments.ToolTip.ToString())
                            Dim ext As String = fi.Extension.ToUpper
                            If fi.Extension.ToUpper = UCase(".jpg") Or fi.Extension.ToUpper = UCase(".jpeg") Or fi.Extension.ToUpper = UCase(".png") Or fi.Extension.ToUpper = UCase(".gif") Then
                                Image_OtherDocuments.ImageUrl = btnUpload_OtherDocuments.ToolTip.ToString()
                                btnUpload_OtherDocuments.Focus()
                            Else
                                Image_OtherDocuments.ImageUrl = "~/images/documents.png"
                                btnUpload_OtherDocuments.Focus()
                            End If
                        Else
                            lblInformation.Text = "Invalid Document Type."
                            lblInformation.CssClass = "errorlabels"
                            ModalPopupExtender3.Show()
                            btnUpload_OtherDocuments.Focus()
                        End If


                    Else
                        lblErrorImage_OtherDocuments.Text = "No file Selected for Other Doc."
                        lblErrorImage_OtherDocuments.CssClass = "errorlabels"
                        btnUpload_OtherDocuments.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Sub DownloadDoc(ByVal DocPath As String)
        Try

            Dim fi As New FileInfo(DocPath)
            Dim strURL As String = DocPath
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" & fi.Name & """")
            Dim data As Byte() = req.DownloadData(Server.MapPath(strURL))
            response.BinaryWrite(data)


            response.[End]()
        Catch ex As Exception
        End Try
    End Sub

    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""

    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button, ByVal imgstr As String)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                    Dim completeFilePath As String = ""


                    If Not Directory.Exists(Server.MapPath("~/Temp")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp"))
                    End If
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim)) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim))
                    End If


                    completeFilePath = Server.MapPath("~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & txtRegistrationId.Text.Trim & "_" & imgstr & Session("ext"))
                    imgPath = "~/Temp" & "/" & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & "/" & txtRegistrationId.Text.Trim & "_" & imgstr & Session("ext")

                    imagUpload.PostedFile.SaveAs(completeFilePath)

                    Dim bytesInStream As Byte() = New Byte(imagUpload.PostedFile.InputStream.Length - 1) {}
                    Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(imagUpload.PostedFile.InputStream)

                    Dim bmp As New Bitmap(image)
                    bmp = ImageOptimization(bmp, completeFilePath, filename, "THUMBNAIL")
                    bmp.Save(completeFilePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                    'bmp.Save(completeFilePath + "/Thumbnail_" + filename, System.Drawing.Imaging.ImageFormat.Jpeg)



                    UploadButtonName.ToolTip = imgPath
                    UploadButtonName.Text = "Download"
                    RemoveButtonName.Enabled = True



                Else
                End If
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub



    Private Function ImageOptimization(ByVal image As Image, ByVal targetPath As String, ByVal fileName As String, ByVal imagetype As String) As Bitmap

        fileName = Path.GetFileName(fileName)
        fileName.Replace("\", "")
        Dim namefileName = fileName.Substring(0, fileName.LastIndexOf("."))
        Dim ext1 As String = Path.GetExtension(fileName)
        Dim imgPath As String = ""


        Dim reqWidth As Integer = 0
        Dim reqHeight As Integer = 0
        reqWidth = image.Width
        reqHeight = image.Height

        Dim thumbnailImg = Resize_Image(DirectCast(image, Bitmap), reqWidth, reqHeight)


        imgPath = Convert.ToString((targetPath & Convert.ToString("/"c)) + namefileName) & ext1

        If System.IO.File.Exists(imgPath) Then
            System.IO.File.Delete(imgPath)
        End If
        'thumbnailImg.Save(imgPath, image.RawFormat);

        Return thumbnailImg
    End Function

    Private Function Resize_Image(ByVal streamImage As Bitmap, ByVal maxWidth As Integer, ByVal maxHeight As Integer) As Bitmap
        Dim originalImage As New Bitmap(streamImage)
        Dim newWidth As Integer = originalImage.Width
        Dim newHeight As Integer = originalImage.Height
        Dim aspectRatio As Double = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height)

        If aspectRatio <= 1 AndAlso originalImage.Width > maxWidth Then
            newWidth = maxWidth
            newHeight = Convert.ToInt32(Math.Round(newWidth / aspectRatio))
        ElseIf aspectRatio > 1 AndAlso originalImage.Height > maxHeight Then
            newHeight = maxHeight
            newWidth = Convert.ToInt32(Math.Round(newHeight * aspectRatio))
        End If
        Return New Bitmap(originalImage, newWidth, newHeight)
    End Function


    Protected Sub btnDeleteUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_PanCard.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "IncomeProof"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_AgeProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_AddharCardBack.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "AgeProof"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_AddressProof_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_AddharCardBack.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the document Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "AddharCardBack"
            ModalPopupExtender4.Show()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_Photo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_Photo.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"
            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the Photo Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "Photo"
            ModalPopupExtender4.Show()


        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnDeleteUpload_Signature_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteUpload_OtherDocuments.Click
        Try

            btnDelete_Document.Visible = True
            btnDelete_Document.Text = "Yes"
            btnCancelDeleteDocument.Text = "No"

            lblDeleteInfo.CssClass = ""
            lblDeleteInfo.Text = "This Action will Remove the Signature Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

            lblDeleteDocumentInfo.Text = "OtherDocuments"
            ModalPopupExtender4.Show()


        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                Dim VMemberID As String = GV.parseString(txtRegistrationId.Text.Trim)


                If lblDeleteDocumentInfo.Text = "PanCard" Then

                    Dim imgpath As String = btnUpload_PanCard.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadPanCard=''  where RegistrationId='" & VMemberID & "' "
                    btnUpload_PanCard.ToolTip = ""
                    btnUpload_PanCard.Text = "Upload"
                    btnDeleteUpload_PanCard.Enabled = False
                    Image_PanCard.ImageUrl = "~/images/uploadimage.png"
                    btnDeleteUpload_PanCard.Focus()

                    lblDeleteInfo.Text = "Document Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "AddharCardFront" Then

                    Dim imgpath As String = btnUpload_AddharCardBack.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadAddharCard_Front=''  where RegistrationId='" & VMemberID & "' "

                    btnUpload_AddharCardBack.ToolTip = ""
                    btnUpload_AddharCardBack.Text = "Upload"
                    btnDeleteUpload_AddharCardBack.Enabled = False
                    btnDeleteUpload_AddharCardBack.Focus()
                    Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "Document Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "AddharCardBack" Then

                    Dim imgpath As String = btnUpload_AddharCardBack.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadAddharCard_Back=''  where RegistrationId='" & VMemberID & "'"

                    btnUpload_AddharCardBack.ToolTip = ""
                    btnUpload_AddharCardBack.Text = "Upload"
                    btnDeleteUpload_AddharCardBack.Enabled = False
                    btnDeleteUpload_AddharCardBack.Focus()
                    Image_AddharCardBack.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "Document Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "Photo" Then

                    Dim imgpath As String = btnUpload_Photo.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadPhoto=''  where RegistrationId='" & VMemberID & "' "

                    btnUpload_Photo.ToolTip = ""
                    btnUpload_Photo.Text = "Upload"
                    btnDeleteUpload_Photo.Enabled = False
                    btnDeleteUpload_Photo.Focus()
                    Image_Photo.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "Photo Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                ElseIf lblDeleteDocumentInfo.Text = "OtherDocuments" Then

                    Dim imgpath As String = btnUpload_OtherDocuments.ToolTip
                    File.Delete(Server.MapPath(imgpath))
                    qry = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration set UploadOtherProof=''  where RegistrationId='" & VMemberID & "' "

                    btnUpload_OtherDocuments.ToolTip = ""
                    btnUpload_OtherDocuments.Text = "Upload"
                    btnDeleteUpload_OtherDocuments.Enabled = False
                    btnDeleteUpload_OtherDocuments.Focus()
                    Image_OtherDocuments.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "OtherDocuments Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"





                End If


                If Not qry = "" Then
                    GV.FL.DMLQueries(qry)
                End If

                btnDelete_Document.Visible = False
                btnDelete_Document.Text = "OK"
                btnCancelDeleteDocument.Text = "OK"

                'btnCancelDeleteDocument.Attributes("style") = "display:none"

            Else

            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception

        End Try
    End Sub
    
    
End Class