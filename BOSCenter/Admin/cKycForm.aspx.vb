Imports System.IO
Imports System.Net

Public Class cKycForm
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("Admin")
    '//// ----------------Variable Declaration  ----------------
    Dim EditFlag As Integer = 0
    Dim QryStr As String = ""

    Dim DS As New DataSet

    Dim VkCode, VkCodeType, VRequestID, VRequestDate, VApprovedBy, VApprovedDateTime, VApporvedStatus, VApporveRemakrs, VSalutation, VFirstName, VMiddleName, VLastName, VAddress, VPhoneNo, VAadharNumber, VCardNumber, VWebFpData, VWebImgFpData1, VWebImgFpData2, VWebImgFpData3, VCompanyCode, VRecordDateTime, VUpdatedBy, VUpdatedOn As String

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If btnClear.Text = "Close" Then
                Response.Redirect("SearchStudent.aspx") '/Change the name of form
            ElseIf btnClear.Text = "Back" Then
                Response.Redirect("SAM_DashBoard.aspx")
            Else
                VRequestID = ""
                VkCode = ""
                VkCodeType = ""
                VRequestDate = ""
                VApprovedBy = ""
                VApprovedDateTime = ""
                VApporvedStatus = ""
                VApporveRemakrs = ""
                VSalutation = ""
                VFirstName = ""
                VMiddleName = ""
                VLastName = ""
                VAddress = ""
                VPhoneNo = ""
                VAadharNumber = ""
                VCardNumber = ""
                VWebFpData = ""
                VWebImgFpData1 = ""
                VWebImgFpData2 = ""
                VWebImgFpData3 = ""
                VCompanyCode = ""
                VRecordDateTime = ""
                VUpdatedBy = ""
                VUpdatedOn = ""

                lblError.Text = ""
                lblError.CssClass = ""


                txtRequestID.Text = ""

                txtRequestDate.Text = ""
                ddlApprovedStatus.SelectedIndex = 0

                'txtApporveRemakrs.Text = ""

                ddlSalutation.SelectedIndex = 0

                txtFirstName.Text = ""

                txtMiddleName.Text = ""

                txtLastName.Text = ""

                txtAddress.Text = ""

                txtPhoneNo.Text = ""

                txtaadharNo.Text = ""

                txtCardNo.Text = ""



                txtAddress.Text = ""


                txtRequestID.Text = GV.FL.getAutoNumber("SessionId")

                'txtPassword.Text = ""

                txtRequestDate.Text = Now.Date.ToString("dd/MM/YYYY")

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



            If btnSave.Text = "Submit" Then
                btnPopupYes.Text = "Yes"
                btnPopupYes.Attributes("Style") = ""
                btnPopupYes.Visible = True
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Submit?"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            Else
                btnPopupYes.Text = "Yes"
                btnPopupYes.Visible = True

                btnCancel.Attributes("Style") = ""
                btnCancel.Text = "No"
                lblDialogMsg.Text = "Are You sure you want to Update?"
                lblDialogMsg.CssClass = ""
                ModalPopupExtender1.Show()
            End If
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

            If Not txtRequestID.Text.Trim = "" Then
                VRequestID = GV.parseString(txtRequestID.Text.Trim)
            Else
                VRequestID = ""
            End If


            If Not txtRequestDate.Text.Trim = "" Then
                VRequestDate = GV.FL.returnDateMonthWise(GV.parseString(txtRequestDate.Text.Trim))
            Else
                VRequestDate = ""
            End If



            VSalutation = GV.parseString(ddlSalutation.SelectedValue)

            If Not txtFirstName.Text.Trim = "" Then
                VFirstName = GV.parseString(txtFirstName.Text.Trim)
            Else
                VFirstName = ""
            End If

            If Not txtMiddleName.Text.Trim = "" Then
                VMiddleName = GV.parseString(txtMiddleName.Text.Trim)
            Else
                VMiddleName = ""
            End If

            If Not txtLastName.Text.Trim = "" Then
                VLastName = GV.parseString(txtLastName.Text.Trim)
            Else
                VLastName = ""
            End If

            If Not txtAddress.Text.Trim = "" Then
                VAddress = GV.parseString(txtAddress.Text.Trim)
            Else
                VAddress = ""
            End If

            If Not txtPhoneNo.Text.Trim = "" Then
                VPhoneNo = GV.parseString(txtPhoneNo.Text.Trim)
            Else
                VPhoneNo = ""
            End If

            If Not txtaadharNo.Text.Trim = "" Then
                VAadharNumber = GV.parseString(txtaadharNo.Text.Trim)
            Else
                VAadharNumber = ""
            End If

            If Not txtCardNo.Text.Trim = "" Then
                VCardNumber = GV.parseString(txtCardNo.Text.Trim)
            Else
                VCardNumber = ""
            End If


            VRecordDateTime = Now
            VUpdatedOn = Now
            VUpdatedBy = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VCompanyCode = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
            VkCode = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            VkCodeType = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

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

                VApprovedBy = ""
                VApprovedDateTime = ""
                VApporvedStatus = "Pending"
                VApporveRemakrs = ""

                If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details Where RequestID='" & VRequestID & "' ") > 0 Then 'Change where condition according to Criteria 
                    lblError.Text = "Record Already Exists."
                    lblError.CssClass = "errorlabels"
                    Exit Sub
                End If
                Dim QryStr As String = "insert into " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details (kCode,kCodeType,RequestID,RequestDate,ApprovedBy,ApprovedDateTime,ApporvedStatus,ApporveRemakrs,Salutation,FirstName,MiddleName,LastName,Address,PhoneNo,AadharNumber,CardNumber,WebFpData,WebImgFpData1,WebImgFpData2,WebImgFpData3,CompanyCode,RecordDateTime,UpdatedBy,UpdatedOn) values('" & VkCode & "','" & VkCodeType & "', '" & VRequestID & "','" & VRequestDate & "','" & VApprovedBy & "',NULL,'" & VApporvedStatus & "','" & VApporveRemakrs & "','" & VSalutation & "','" & VFirstName & "','" & VMiddleName & "','" & VLastName & "','" & VAddress & "','" & VPhoneNo & "','" & VAadharNumber & "','" & VCardNumber & "','" & VWebFpdata & "','" & VWebFigureData1 & "','" & VWebFigureData2 & "','" & VWebFigureData3 & "','" & VCompanyCode & "','" & VRecordDateTime & "','" & VUpdatedBy & "','" & VUpdatedOn & "' )"

                ' Dim QryStr As String = "insert into CRM_Student_Registration (ParentPassword,ActiveStatus,ImagePath,FatherName,FatherOccupation,FatherMobileNo,MotherName,MotherOccupation,MotherMobileNo,StudentName,DOB,SchoolName,Class,EmailID,MobileNo,ResidenceNo,Address,PreviousInstitute,ReasonForLeaving,StudentID,Password,RegistrationDate,WebFpData,WebImgFpData1,WebImgFpData2,WebImgFpData3) values('" & VPassword & "', '" & VActiveStatus & "', '" & VImagePath & "','" & VFatherName & "','" & VFatherOccupation & "','" & VFatherMobileNo & "','" & VMotherName & "','" & VMotherOccupation & "','" & VMotherMobileNo & "','" & VStudentName & "','" & VDOB & "','" & VSchoolName & "','" & VClass & "','" & VEmailID & "','" & VMobileNo & "','" & VResidenceNo & "','" & VAddress & "','" & VPreviousInstitute & "','" & VReasonForLeaving & "','" & VStudentID & "','" & VPassword & "','" & VRegistrationDate & "' ,'" & VWebFpdata & "','" & VWebFigureData1 & "','" & VWebFigureData2 & "','" & VWebFigureData3 & "' )"
                If GV.FL.DMLQueries(QryStr) = True Then
                    'Clear()

                    FillPreviousData()

                    lblDialogMsg.Text = "Record Saved Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()

                    'Response.Redirect("cKycForm.aspx")
                Else
                    lblDialogMsg.Text = "Record Insertion Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                    btnCancel.Text = "Ok"
                    btnPopupYes.Attributes("style") = "display:none"
                    ModalPopupExtender1.Show()
                End If

            ElseIf btnSave.Text.Trim.ToUpper = "Update".Trim.ToUpper Then


                'Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details set RequestID='" & VRequestID & "', RequestDate='" & VRequestDate & "', ApprovedBy='" & VApprovedBy & "', ApprovedDateTime='" & VApprovedDateTime & "', ApporvedStatus='" & VApporvedStatus & "', ApporveRemakrs='" & VApporveRemakrs & "', Salutation='" & VSalutation & "', FirstName='" & VFirstName & "', MiddleName='" & VMiddleName & "', LastName='" & VLastName & "', Address='" & VAddress & "', PhoneNo='" & VPhoneNo & "', AadharNumber='" & VAadharNumber & "', CardNumber='" & VCardNumber & "', WebFpData='" & VWebFpdata & "', WebImgFpData1='" & VWebFigureData1 & "', WebImgFpData2='" & VWebFigureData2 & "', WebImgFpData3='" & VWebFigureData3 & "', CompanyCode='" & VCompanyCode & "', RecordDateTime='" & VRecordDateTime & "', UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where RID=" & lblRID.Text.Trim & ""
                Dim QryStr As String = "update " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details set  ApporvedStatus='Pending',  Salutation='" & VSalutation & "', FirstName='" & VFirstName & "', MiddleName='" & VMiddleName & "', LastName='" & VLastName & "', Address='" & VAddress & "', PhoneNo='" & VPhoneNo & "', AadharNumber='" & VAadharNumber & "', CardNumber='" & VCardNumber & "', WebFpData='" & VWebFpdata & "', WebImgFpData1='" & VWebFigureData1 & "', WebImgFpData2='" & VWebFigureData2 & "', WebImgFpData3='" & VWebFigureData3 & "' , UpdatedBy='" & VUpdatedBy & "', UpdatedOn='" & VUpdatedOn & "' where RequestID='" & VRequestID & "'"

                'Dim QryStr As String = "update CRM_Student_Registration set  ActiveStatus='" & VActiveStatus & "',ImagePath='" & VImagePath & "', FatherName='" & VFatherName & "', FatherOccupation='" & VFatherOccupation & "', FatherMobileNo='" & VFatherMobileNo & "', MotherName='" & VMotherName & "', MotherOccupation='" & VMotherOccupation & "', MotherMobileNo='" & VMotherMobileNo & "', StudentName='" & VStudentName & "', DOB='" & VDOB & "', SchoolName='" & VSchoolName & "', Class='" & VClass & "', EmailID='" & VEmailID & "', MobileNo='" & VMobileNo & "', ResidenceNo='" & VResidenceNo & "', Address='" & VAddress & "', PreviousInstitute='" & VPreviousInstitute & "', ReasonForLeaving='" & VReasonForLeaving & "', StudentID='" & VStudentID & "', Password='" & VPassword & "', RegistrationDate='" & VRegistrationDate & "' ,WebFpData='" & VWebFpdata & "',WebImgFpData1='" & VWebFigureData1 & "',WebImgFpData2='" & VWebFigureData2 & "',WebImgFpData3='" & VWebFigureData3 & "'  where RID=" & lblRID.Text.Trim & ""
                If GV.FL.DMLQueries(QryStr) = True Then
                    FillPreviousData()

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
            VRequestID = ""
            VRequestDate = ""
            VApprovedBy = ""
            VApprovedDateTime = ""
            VApporvedStatus = ""
            VApporveRemakrs = ""
            VSalutation = ""
            VFirstName = ""
            VMiddleName = ""
            VLastName = ""
            VAddress = ""
            VPhoneNo = ""
            VAadharNumber = ""
            VCardNumber = ""
            VWebFpData = ""
            VWebImgFpData1 = ""
            VWebImgFpData2 = ""
            VWebImgFpData3 = ""
            VCompanyCode = ""
            VRecordDateTime = ""
            VUpdatedBy = ""
            VUpdatedOn = ""
            VkCode = ""
            VkCodeType = ""

            lblError.Text = ""
            lblError.CssClass = ""
            txtRequestID.Text = ""

            txtRequestDate.Text = ""
            ddlApprovedStatus.SelectedIndex = 0
            ddlSalutation.SelectedIndex = 0

            txtFirstName.Text = ""

            txtMiddleName.Text = ""

            txtLastName.Text = ""

            txtAddress.Text = ""

            txtPhoneNo.Text = ""

            txtaadharNo.Text = ""

            txtCardNo.Text = ""


            Session("EditFlag") = 0
            btnSave.Text = "Submit"
            btnClear.Text = "Reset"
            lblError.Text = ""
            btnSave.Enabled = True
            btnDelete.Enabled = False
            btnDelete.Visible = False
            txtRequestID.Text = GV.FL.AddInVar("Prefix_StudentID", "autonumber") & GV.FL.getAutoNumber("StudentID")
            txtRequestDate.Text = Now.Date.ToString("dd/MM/yyyy")


            txtfpdata.Text = ""
            imgFinger.ImageUrl = "..\images\uploadimage.png"

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Text = ""
                lblError.CssClass = ""

                Session("EditFlag") = 0

                If Not Session("RecordID") = "" And Session("RecordEdit") = 1 And Session("RecordEditConfirm") = 9 Then

                    Session("RecordEdit") = 0
                    Session("RecordEditConfirm") = -9
                    Session("EditFlag") = 1

                    '                    Session("Workfor") = "Update"
                    If Session("Workfor") = "Profile" Then
                        btnSave.Visible = False
                        btnClear.Text = "Back"
                        btnDelete.Visible = False


                        txtAddress.ReadOnly = True

                        txtRequestDate.ReadOnly = True
                        txtRequestDate_CalendarExtender.Enabled = False


                        ddlApprovedStatus.Enabled = False
                        ddlApprovedStatus.CssClass = "form-control"

                        btnCapture.Enabled = False
                        btnDelete_Document.Enabled = False

                        btnremove.Enabled = False


                        formheading_1.Text = "::: My Profile ::: "
                        DS = GV.FL.OpenDs("CRM_Student_Registration where StudentID='" & Session("RecordID") & "'")
                    Else
                        Session("Workfor") = "Update"
                        btnDelete.Enabled = True
                        btnDelete.Visible = False
                        btnSave.Text = "Update"
                        btnClear.Text = "Close"
                        formheading_1.Text = "Edit Student Registration "
                        DS = GV.FL.OpenDs("CRM_Student_Registration where RID='" & Session("RecordID") & "'")

                    End If


                    lblRID.Text = Session("RecordID")
                    'UsrRightsDS = FL.OpenDs("OSIL_UserRights_Master where User_ID='" & Session("UserName") & "' and FormName='" & Me.Page.Request.AppRelativeCurrentExecutionFilePath & "'")
                    'applySavingRightOnForm(UsrRightsDS, btnSave)
                    'applyUpdatingRightOnForm(UsrRightsDS, btnSave)

                    If Not DS Is Nothing Then
                        If DS.Tables.Count > 0 Then


                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("ActiveStatus")) Then
                                If Not DS.Tables(0).Rows(0).Item("ActiveStatus").ToString() = "" Then
                                    ddlApprovedStatus.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ActiveStatus").ToString())
                                Else
                                    ddlApprovedStatus.SelectedIndex = 0
                                End If
                            Else
                                ddlApprovedStatus.SelectedIndex = 0
                            End If



                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Address")) Then
                                If Not DS.Tables(0).Rows(0).Item("Address").ToString() = "" Then
                                    txtAddress.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Address").ToString())
                                Else
                                    txtAddress.Text = ""
                                End If
                            Else
                                txtAddress.Text = ""
                            End If



                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("StudentID")) Then
                                If Not DS.Tables(0).Rows(0).Item("StudentID").ToString() = "" Then
                                    txtRequestID.Text = GV.parseString(DS.Tables(0).Rows(0).Item("StudentID").ToString())
                                Else
                                    txtRequestID.Text = ""
                                End If
                            Else
                                txtRequestID.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("Password")) Then
                                If Not DS.Tables(0).Rows(0).Item("Password").ToString() = "" Then
                                    'txtPassword.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Password").ToString())
                                Else
                                    'txtPassword.Text = ""
                                End If
                            Else
                                'txtPassword.Text = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("RegistrationDate")) Then
                                If Not DS.Tables(0).Rows(0).Item("RegistrationDate").ToString() = "" Then
                                    txtRequestDate.Text = CDate(GV.parseString(DS.Tables(0).Rows(0).Item("RegistrationDate").ToString())).ToString("dd/MM/yyyy")

                                Else
                                    txtRequestDate.Text = ""
                                End If
                            Else
                                txtRequestDate.Text = ""
                            End If




                            Dim webfpdata, WebImgFpData1, WebImgFpData2, WebImgFpData3 As String
                            'rahul
                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebFpData")) Then

                                If Not DS.Tables(0).Rows(0).Item("WebFpData").ToString() = "" Then

                                    webfpdata = DS.Tables(0).Rows(0).Item("WebFpData").ToString()
                                Else
                                    webfpdata = ""
                                End If
                            Else
                                webfpdata = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData1")) Then

                                If Not DS.Tables(0).Rows(0).Item("WebImgFpData1").ToString() = "" Then
                                    WebImgFpData1 = DS.Tables(0).Rows(0).Item("WebImgFpData1").ToString()
                                Else
                                    WebImgFpData1 = "~/images/uploadimage.png"
                                End If
                            Else
                                WebImgFpData1 = "~/images/uploadimage.png"
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData2")) Then

                                If Not DS.Tables(0).Rows(0).Item("WebImgFpData2").ToString() = "" Then
                                    WebImgFpData2 = DS.Tables(0).Rows(0).Item("WebImgFpData2").ToString()
                                Else
                                    WebImgFpData2 = ""
                                End If
                            Else
                                WebImgFpData2 = ""
                            End If

                            If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData3")) Then

                                If Not DS.Tables(0).Rows(0).Item("WebImgFpData3").ToString() = "" Then
                                    WebImgFpData3 = DS.Tables(0).Rows(0).Item("WebImgFpData3").ToString()
                                Else
                                    WebImgFpData3 = ""
                                End If
                            Else
                                WebImgFpData3 = ""
                            End If
                            If WebImgFpData1 = "~/images/uploadimage.png" Then
                                txtfpdata.Text = ""
                            Else
                                txtfpdata.Text = webfpdata & "~" & WebImgFpData1 & WebImgFpData2 & WebImgFpData3
                            End If

                            imgFinger.ImageUrl = WebImgFpData1 & WebImgFpData2 & WebImgFpData3



                            '///////////////======= End set Uploaded Image url ===============================================


                        End If
                    End If
                Else
                    'UsrRightsDS = New DataSet
                    'UsrRightsDS = FL.OpenDs("OSIL_UserRights_Master  where User_ID='" & Session("UserName") & "' and FormName='" & Me.Page.Request.AppRelativeCurrentExecutionFilePath & "'")
                    'applySavingRightOnForm(UsrRightsDS, btnSave)

                    FillPreviousData()

                End If
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Sub FillPreviousData()
        Try
            If GV.FL.RecCount(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details Where kCode='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "' ") > 0 Then
                div_req.Visible = True

                DS = GV.FL.OpenDs(" " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Card_Kyc_Details Where kCode='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "' ")
                If Not DS Is Nothing Then
                    If DS.Tables.Count > 0 Then

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("RequestID")) Then
                            If Not DS.Tables(0).Rows(0).Item("RequestID").ToString() = "" Then
                                txtRequestID.Text = GV.parseString(DS.Tables(0).Rows(0).Item("RequestID").ToString())
                            Else
                                txtRequestID.Text = ""
                            End If
                        Else
                            txtRequestID.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("RequestDate")) Then
                            If Not DS.Tables(0).Rows(0).Item("RequestDate").ToString() = "" Then
                                txtRequestDate.Text = CDate(GV.parseString(DS.Tables(0).Rows(0).Item("RequestDate").ToString())).ToString("dd/MM/yyyy")
                            Else
                                txtRequestDate.Text = ""
                            End If
                        Else
                            txtRequestDate.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApporvedStatus")) Then
                            If Not DS.Tables(0).Rows(0).Item("ApporvedStatus").ToString() = "" Then
                                ddlApprovedStatus.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("ApporvedStatus").ToString())
                            Else
                                ddlApprovedStatus.SelectedIndex = 0
                            End If
                        Else
                            ddlApprovedStatus.SelectedIndex = 0
                        End If
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("ApporveRemakrs")) Then
                            If Not DS.Tables(0).Rows(0).Item("ApporveRemakrs").ToString() = "" Then
                                txtAdminRemarks.Text = GV.parseString(DS.Tables(0).Rows(0).Item("ApporveRemakrs").ToString())
                            Else
                                txtAdminRemarks.Text = ""
                            End If
                        Else
                            txtAdminRemarks.Text = ""
                        End If



                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Salutation")) Then
                            If Not DS.Tables(0).Rows(0).Item("Salutation").ToString() = "" Then
                                ddlSalutation.SelectedValue = GV.parseString(DS.Tables(0).Rows(0).Item("Salutation").ToString())
                            Else
                                ddlSalutation.SelectedIndex = 0
                            End If
                        Else
                            ddlSalutation.SelectedIndex = 0
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

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("MiddleName")) Then
                            If Not DS.Tables(0).Rows(0).Item("MiddleName").ToString() = "" Then
                                txtMiddleName.Text = GV.parseString(DS.Tables(0).Rows(0).Item("MiddleName").ToString())
                            Else
                                txtMiddleName.Text = ""
                            End If
                        Else
                            txtMiddleName.Text = ""
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





                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("Address")) Then
                            If Not DS.Tables(0).Rows(0).Item("Address").ToString() = "" Then
                                txtAddress.Text = GV.parseString(DS.Tables(0).Rows(0).Item("Address").ToString())
                            Else
                                txtAddress.Text = ""
                            End If
                        Else
                            txtAddress.Text = ""
                        End If


                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("PhoneNo")) Then
                            If Not DS.Tables(0).Rows(0).Item("PhoneNo").ToString() = "" Then
                                txtPhoneNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("PhoneNo").ToString())
                            Else
                                txtPhoneNo.Text = ""
                            End If
                        Else
                            txtPhoneNo.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("AadharNumber")) Then
                            If Not DS.Tables(0).Rows(0).Item("AadharNumber").ToString() = "" Then
                                txtaadharNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("AadharNumber").ToString())
                            Else
                                txtaadharNo.Text = ""
                            End If
                        Else
                            txtaadharNo.Text = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("CardNumber")) Then
                            If Not DS.Tables(0).Rows(0).Item("CardNumber").ToString() = "" Then
                                txtCardNo.Text = GV.parseString(DS.Tables(0).Rows(0).Item("CardNumber").ToString())
                            Else
                                txtCardNo.Text = ""
                            End If
                        Else
                            txtCardNo.Text = ""
                        End If




                        Dim webfpdata, WebImgFpData1, WebImgFpData2, WebImgFpData3 As String
                        'rahul
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebFpData")) Then

                            If Not DS.Tables(0).Rows(0).Item("WebFpData").ToString() = "" Then

                                webfpdata = DS.Tables(0).Rows(0).Item("WebFpData").ToString()
                            Else
                                webfpdata = ""
                            End If
                        Else
                            webfpdata = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData1")) Then

                            If Not DS.Tables(0).Rows(0).Item("WebImgFpData1").ToString() = "" Then
                                WebImgFpData1 = DS.Tables(0).Rows(0).Item("WebImgFpData1").ToString()
                            Else
                                WebImgFpData1 = "~/images/uploadimage.png"
                            End If
                        Else
                            WebImgFpData1 = "~/images/uploadimage.png"
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData2")) Then

                            If Not DS.Tables(0).Rows(0).Item("WebImgFpData2").ToString() = "" Then
                                WebImgFpData2 = DS.Tables(0).Rows(0).Item("WebImgFpData2").ToString()
                            Else
                                WebImgFpData2 = ""
                            End If
                        Else
                            WebImgFpData2 = ""
                        End If

                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("WebImgFpData3")) Then

                            If Not DS.Tables(0).Rows(0).Item("WebImgFpData3").ToString() = "" Then
                                WebImgFpData3 = DS.Tables(0).Rows(0).Item("WebImgFpData3").ToString()
                            Else
                                WebImgFpData3 = ""
                            End If
                        Else
                            WebImgFpData3 = ""
                        End If

                        If WebImgFpData1 = "~/images/uploadimage.png" Then
                            txtfpdata.Text = ""
                        Else
                            txtfpdata.Text = webfpdata & "~" & WebImgFpData1 & WebImgFpData2 & WebImgFpData3
                        End If

                        imgFinger.ImageUrl = WebImgFpData1 & WebImgFpData2 & WebImgFpData3



                        '///////////////======= End set Uploaded Image url ===============================================

                        If ddlApprovedStatus.SelectedValue = "Approved" Then
                            btnDelete.Visible = False
                            btnClear.Visible = False
                            btnSave.Visible = False
                            lblError.Text = "Your KYC Form Approved Successfully."
                            lblError.CssClass = "successlabels"


                            ddlApprovedStatus.Enabled = False
                            ddlApprovedStatus.CssClass = "form-control"

                            ddlSalutation.Enabled = False
                            ddlSalutation.CssClass = "form-control"

                            txtRequestID.ReadOnly = True
                            txtRequestDate.ReadOnly = True
                            txtFirstName.ReadOnly = True
                            txtMiddleName.ReadOnly = True
                            txtLastName.ReadOnly = True
                            txtAddress.ReadOnly = True
                            txtPhoneNo.ReadOnly = True
                            txtaadharNo.ReadOnly = True
                            txtCardNo.ReadOnly = True
                            txtAdminRemarks.ReadOnly = True

                            btnremove.Visible = False
                            btnCapture.Visible = False


                        ElseIf ddlApprovedStatus.SelectedValue = "Rejected" Then
                            btnDelete.Visible = False
                            btnSave.Text = "Update"
                            btnSave.Enabled = True
                            btnSave.Visible = True
                            btnClear.Visible = False

                            div_req.Visible = True


                            ddlApprovedStatus.Enabled = False
                            ddlApprovedStatus.CssClass = "form-control"
                            txtRequestID.ReadOnly = True
                            txtRequestDate.ReadOnly = True



                            ddlSalutation.Enabled = True
                            ddlSalutation.CssClass = "form-control"


                            txtFirstName.ReadOnly = False
                            txtMiddleName.ReadOnly = False
                            txtLastName.ReadOnly = False
                            txtAddress.ReadOnly = False
                            txtPhoneNo.ReadOnly = False
                            txtaadharNo.ReadOnly = False
                            txtCardNo.ReadOnly = False

                            btnremove.Visible = True
                            btnCapture.Visible = True

                            txtAdminRemarks.ReadOnly = True

                        ElseIf ddlApprovedStatus.SelectedValue = "Pending" Then
                            btnDelete.Visible = False
                            btnClear.Visible = False
                            btnSave.Visible = False

                            ddlApprovedStatus.Enabled = False
                            ddlApprovedStatus.CssClass = "form-control"

                            ddlSalutation.Enabled = False
                            ddlSalutation.CssClass = "form-control"

                            txtRequestID.ReadOnly = True
                            txtRequestDate.ReadOnly = True
                            txtFirstName.ReadOnly = True
                            txtMiddleName.ReadOnly = True
                            txtLastName.ReadOnly = True
                            txtAddress.ReadOnly = True
                            txtPhoneNo.ReadOnly = True
                            txtaadharNo.ReadOnly = True
                            txtCardNo.ReadOnly = True

                            lblError.Text = "Form Already Submitted. Pending For Approvel."
                            lblError.CssClass = "errorlabels"

                            btnremove.Visible = False
                            btnCapture.Visible = False

                            txtAdminRemarks.ReadOnly = True
                            txtAdminRemarks.Text = ""

                        End If




                    End If
                End If



                Exit Sub
            Else
                div_req.Visible = False

                Session("Workfor") = "Save"
                txtRequestID.Text = GV.FL.getAutoNumber("SessionId")
                txtRequestDate.Text = Now.Date.ToString("dd/MM/yyyy")
                btnSave.Text = "Submit"
                btnClear.Text = "Reset"
                btnDelete.Enabled = False
                btnDelete.Visible = False
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

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

    Dim filePath As String = ""
    Dim filename As String = ""
    Dim ext As String = ""

    Public Sub SaveImage(ByVal imagUpload As FileUpload, ByVal UploadButtonName As Button, ByVal RemoveButtonName As Button)
        Try
            Dim imgPath As String = ""
            If imagUpload.HasFile = True Then
                filePath = imagUpload.PostedFile.FileName
                filename = Path.GetFileName(filePath)
                ext = Path.GetExtension(filename)
                Session("ext") = ext
                If (UCase(Session("ext")) = UCase(".jpg")) Or (UCase(Session("ext")) = UCase(".jpeg")) Or (UCase(Session("ext")) = UCase(".png")) Or (UCase(Session("ext")) = UCase(".gif")) Then
                    Dim completeFilePath As String

                    If Not Directory.Exists(Server.MapPath("..\StudentImages")) Then
                        Directory.CreateDirectory(Server.MapPath("..\StudentImages"))
                    End If

                    completeFilePath = Server.MapPath("..\StudentImages\" & txtRequestID.Text & Session("ext"))
                    'imgPath = Server.MapPath("~\Docs\" & txtPatientID.Text & "\" & txtPrescriptionNo.Text & "\Doppler" & Session("ext"))
                    imgPath = "..\StudentImages\" & txtRequestID.Text & Session("ext")

                    imagUpload.PostedFile.SaveAs(completeFilePath)

                    UploadButtonName.ToolTip = imgPath
                    UploadButtonName.Text = "Download"
                    RemoveButtonName.Enabled = True
                Else

                End If
            Else

            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Protected Sub btnDelete_Document_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_Document.Click
        Try
            '===========Delete Uploaded Document Popup=============================
            If btnDelete_Document.Text = "Yes" Then
                Dim qry As String = ""
                If lblDeleteDocumentInfo.Text = "FigurePrint" Then


                    qry = "update CRM_Student_Registration set WebImgFpData1='', WebImgFpData2='', WebImgFpData3='', WebFpData=''  where StudentID='" & GV.parseString(txtRequestID.Text.Trim) & "'"

                    btnCapture.Text = "Capture"

                    txtfpdata.Text = ""
                    btnCapture.Focus()
                    imgFinger.ImageUrl = "~/images/uploadimage.png"

                    lblDeleteInfo.Text = "Figure Print Removed Successfully."
                    lblDeleteInfo.CssClass = "successlabels"

                    If Not qry = "" Then
                        GV.FL.DMLQueries(qry)
                    End If

                    btnDelete_Document.Visible = False
                    btnDelete_Document.Text = "OK"
                    btnCancelDeleteDocument.Text = "OK"


                End If



            Else

            End If
            ModalPopupExtender4.Show()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub DownloadDoc(ByVal DocPath As String)
        Try
            Dim strURL As String = DocPath.ToString().Replace("~", "")
            Dim fi As New FileInfo(Server.MapPath(strURL))
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
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        Try
            'Image1.ImageUrl = "..\images\uploadimage.png"
            'Image1.Visible = True
            'txtfpdata.Text = ""

            If Not GV.parseString(txtfpdata.Text.Trim) = "" Then
                btnDelete_Document.Visible = True
                btnDelete_Document.Text = "Yes"
                btnCancelDeleteDocument.Text = "No"
                lblDeleteInfo.CssClass = ""
                lblDeleteInfo.Text = "This Action will Remove the Figure Print Permanently.<br/> <b>Are You Sure You want To Delete ? </b>"

                lblDeleteDocumentInfo.Text = "FigurePrint"
                ModalPopupExtender4.Show()
            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class